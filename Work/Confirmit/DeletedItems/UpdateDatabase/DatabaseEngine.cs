using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace UpdateDatabase
{
    public class DatabaseEngine
    {
        private string m_DatabaseName;
        private string m_ConnectionString;

        public DatabaseEngine()
        {
            Initialise(null);
        }

        public DatabaseEngine(
            string databaseName)
        {
            Initialise(databaseName);
        }

        public void Initialise(
            string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
                databaseName = "master";

            m_DatabaseName = databaseName;

            var cnStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource =
                string.IsNullOrEmpty(Configuration.Default.SqlServerName) == false
                    ? Configuration.Default.SqlServerName
                    : Environment.MachineName,
                InitialCatalog = m_DatabaseName,
                IntegratedSecurity = false,
                UserID = Configuration.Default.SqlUserName,
                Password = Configuration.Default.SqlPassword
            };

            m_ConnectionString = cnStringBuilder.ToString();

            Exception connect2SqlServerException;
            if (!IsConnectionStringValid(
                m_ConnectionString,
                out connect2SqlServerException))
            {
                throw new Exception(
                    string.Format(
                        "Cannot connect to the SQL server\r\n" +
                        "Exception: {0}\r\n\r\n",
                        connect2SqlServerException));
            }
        }

        public string ConnectionString
        {
            get { return m_ConnectionString; }
        }

        public string DatabaseName
        {
            get { return m_DatabaseName; }
        }

        public string[] GetDatabasesForUpgrade(
            string productionDatabaseName)
        {
            if (productionDatabaseName == "*")
            {
                var databaseEngine = new DatabaseEngine();

                using (var cn = new SqlConnection(databaseEngine.ConnectionString))
                {
                    cn.Open();

                    var sc = new ServerConnection(cn);

                    var srv = new Server(sc);

                    var databasesForUpgrade = new List<string>();

                    foreach (Microsoft.SqlServer.Management.Smo.Database database in srv.Databases)
                    {
                        //
                        // Update all databases by specefied regular expression mode
                        //
                        var regEx = new Regex(Configuration.Default.DatabaseNamePattern);

                        if (regEx.IsMatch(database.Name))
                            databasesForUpgrade.Add(database.Name);
                    }

                    return databasesForUpgrade.ToArray();
                }
            }

            return new[] { productionDatabaseName };
        }


        public void CreateDatabaseFromBackupFile(
            string databaseName,
            string backupFilePath)
        {
            DropDatabase(databaseName);

            using (var cn = new SqlConnection(m_ConnectionString))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                var res = new Restore
                {
                    NoRecovery = false,
                    Database = databaseName
                };
                res.Devices.AddDevice(backupFilePath, DeviceType.File);

                //
                // Db files path (on the SQL server!)
                //
                DataTable fileList = res.ReadFileList(srv);

                foreach (DataRow file2Relocate in fileList.Rows)
                {
                    res.RelocateFiles.Add(new RelocateFile(
                        // Logical name in the backup
                        (string)file2Relocate["LogicalName"],

                        // New physical name
                        Path.Combine(
                            srv.Databases["master"].PrimaryFilePath,
                            Path.ChangeExtension(
                                databaseName,
                                Path.GetExtension((string)file2Relocate["PhysicalName"])))));
                }


                res.SqlRestore(srv);
            }
        }

        public void CreateDatabaseFromScriptFile(
            string dbName,
            string scriptFilePath,
            StringCollection assembliesToDeploy)
        {
            if (!File.Exists(scriptFilePath))
                throw new Exception(
                    string.Format(
                        "Cannot create database, database script file {0} does not exist.",
                        scriptFilePath));

            DropDatabase(dbName);

            using (var cn = new SqlConnection(m_ConnectionString))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                var db = new Microsoft.SqlServer.Management.Smo.Database(srv, dbName);

                //
                // Set trustworthy options, if we have assemblies
                //
                db.DatabaseOptions.Trustworthy = true;

                db.Create();

                foreach (string assemblyPath in assembliesToDeploy)
                {
                    var assembly = new SqlAssembly(db, Path.GetFileNameWithoutExtension(assemblyPath))
                    {
                        AssemblySecurityLevel = AssemblySecurityLevel.Unrestricted
                    };
                    assembly.Create(Environment.ExpandEnvironmentVariables(assemblyPath));
                }

                using (var sr = new StreamReader(scriptFilePath))
                {
                    string createDatabaseScript = sr.ReadToEnd();
                    db.ExecuteNonQuery(createDatabaseScript);
                }
            }
        }

        public bool IsDatabaseExists(
            string dbName)
        {
            using (var cn = new SqlConnection(m_ConnectionString))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                return srv.Databases[dbName] != null;
            }
        }

        public void DropDatabase(
            string dbName)
        {
            SqlConnection.ClearAllPools();

            using (var cn = new SqlConnection(m_ConnectionString))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                if (srv.Databases[dbName] != null)
                {
                    try
                    {
                        srv.KillAllProcesses(dbName);
                    }
                    // ReSharper disable EmptyGeneralCatchClause
                    catch
                    // ReSharper restore EmptyGeneralCatchClause
                    { }

                    try
                    {
                        srv.KillDatabase(dbName);
                    }
                    // ReSharper disable EmptyGeneralCatchClause
                    catch
                    // ReSharper restore EmptyGeneralCatchClause
                    { }
                }
            }
        }

        public void CreateBackup(
            string dbName,
            string backupFilePath)
        {
            if (File.Exists(backupFilePath))
                File.Delete(backupFilePath);

            using (var cn = new SqlConnection(m_ConnectionString))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                if (srv.Databases[dbName] == null)
                    throw new Exception(string.Format(
                        "Backup cannot be created. Database {0} not found or access denied.",
                        dbName));

                var backup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = dbName
                };
                backup.Devices.Add(new BackupDeviceItem(backupFilePath, DeviceType.File));

                backup.Initialize = true;
                backup.Checksum = true;
                backup.ContinueAfterError = true;
                backup.Incremental = false;

                backup.SqlBackup(srv);
            }
        }

        public static bool IsConnectionStringValid(
            string connectionString,
            out Exception exceptionThrown)
        {
            exceptionThrown = null;

            try
            {
                using (var cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    return true;
                }
            }
            catch (Exception e)
            {
                exceptionThrown = e;
                return false;
            }
        }

        public void ExecuteBatch(
            string batchText)
        {
            using (var cn = new SqlConnection(m_ConnectionString))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                var db = new Microsoft.SqlServer.Management.Smo.Database(
                    srv,
                    m_DatabaseName);

                db.ExecuteNonQuery(batchText);
            }
        }

        public void ExecuteNonQuery(
            string cmdText,
            CommandType cmdType,
            params SqlParameter[] parameters)
        {
            using (var cn = new SqlConnection(m_ConnectionString))
            using (var cmd = new SqlCommand(cmdText, cn))
            {
                cn.Open();

                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
            }
        }

        public T ExecuteScalar<T>(
            string cmdText,
            CommandType cmdType,
            params SqlParameter[] parameters)
        {
            using (var cn = new SqlConnection(m_ConnectionString))
            using (var cmd = new SqlCommand(cmdText, cn))
            {
                cn.Open();

                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(parameters);
                return (T)cmd.ExecuteScalar();
            }
        }

        public T ExecuteDataTable<T>(
            string cmdText,
            CommandType cmdType,
            params SqlParameter[] parameters) where T : DataTable, new()
        {
            using (var cn = new SqlConnection(m_ConnectionString))
            using (var cmd = new SqlCommand(cmdText, cn))
            using (var dataAdapter = new SqlDataAdapter())
            {
                cn.Open();

                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(parameters);

                var dataTable = new T();
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataTable);

                return dataTable;
            }
        }
    }
}