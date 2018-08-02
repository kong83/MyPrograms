using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace ProcessorLoader
{
    public class DatabaseEngine
    {
        private string mDatabaseName;
        private string mConnectionString;

        public DatabaseEngine(
            string databaseName)
        {
            Initialise(databaseName);
        }


        public string ConnectionString
        {
            get { return this.mConnectionString; }
        }


        public string DatabaseName
        {
            get { return this.mDatabaseName; }
        }

        /// <summary>
        /// Execute query without return any data
        /// </summary>        
        /// <param name="cmdText">Sql command</param>
        /// <param name="parameters">Parameters for command</param>
        public void ExecuteNonQuery(
            string cmdText,
            params SqlParameter[] parameters)
        {
            if (string.IsNullOrEmpty(this.mConnectionString))
            {
                this.mConnectionString = CreateConnectionString(null);
            }

            using (var cn = new SqlConnection(this.mConnectionString))
            using (var cmd = new SqlCommand(cmdText, cn))
            {
                cn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddRange(parameters);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Drop database
        /// </summary>
        /// <param name="databaseName">Database name</param>
        public void DropDatabase(
           string databaseName)
        {
            SqlConnection.ClearAllPools();

            using (var cn = new SqlConnection(CreateConnectionString(null)))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                if (srv.Databases[databaseName] == null)
                {
                    return;
                }

                try
                {
                    srv.KillAllProcesses(databaseName);
                }
                catch
                {                   
                }
                
                srv.KillDatabase(databaseName);
            }
        }

        private static bool IsConnectionStringValid(
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

        private static string CreateConnectionString(string databaseName)
        {
            var cnStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = Environment.MachineName,
                InitialCatalog = databaseName ?? "master",
                IntegratedSecurity = false,
                UserID = "sa",
                Password = "firm"
            };

            return cnStringBuilder.ToString();
        }
        

        private void Initialise(
            string databaseName)
        {
            ExecuteNonQuery(
                string.Format(
                    "if DB_ID('{0}') is not null " +
                    "drop database {0}",
                    databaseName));

            ExecuteNonQuery("Create database " + databaseName);       

            this.mDatabaseName = databaseName;
            this.mConnectionString = CreateConnectionString(this.mDatabaseName);

            Exception connect2SqlServerException;
            if (!IsConnectionStringValid(
                this.mConnectionString,
                out connect2SqlServerException))
            {
                throw new Exception(
                    string.Format(
                        "Cannot connect to the SQL server\r\n" +
                        "Exception: {0}\r\n\r\n",
                        connect2SqlServerException));
            }
            
            ExecuteNonQuery(
                " create table [TestTable] " +
                "( column1 int PRIMARY KEY IDENTITY(0,1), column2 varchar(10), column3 varchar(10), " +
                "column4 varchar(10), column5 varchar(10), column6 varchar(10),column7 varchar(10), " +
                "column8 varchar(10), column9 varchar(10), column10 varchar(10))");            
        }
    }
}
