using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

using View = Microsoft.SqlServer.Management.Smo.View;

namespace DatabaseWorker
{
    public partial class Form1 : Form
    {
        private bool _changeServerSettings = true;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create connection string for current server settings
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            SqlConnectionStringBuilder cnStringBuilder;
            if (checkBoxWindowsAuthorization.Checked)
            {
                cnStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = textBoxServerName.Text,
                    IntegratedSecurity = true
                };
            }
            else
            {
                cnStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = textBoxServerName.Text,
                    IntegratedSecurity = false,
                    UserID = textBoxLogin.Text,
                    Password = textBoxPassword.Text
                };
            }


            return cnStringBuilder.ToString();
        }


        /// <summary>
        /// Put databases into current dbComboBox
        /// </summary>
        /// <param name="dbComboBox">Combobox for databases</param>
        /// <param name="server"></param>
        private static void PutDatabasesList(ComboBox dbComboBox, Server server)
        {
            int currentItem = dbComboBox.SelectedIndex;
            string currentText = dbComboBox.Text;

            dbComboBox.Items.Clear();
            foreach (Database database in server.Databases)
            {
                dbComboBox.Items.Add(database.Name);
            }

            if (dbComboBox.Items.Contains(currentText))
            {
                dbComboBox.Text = currentText;
                return;
            }

            if (dbComboBox.Items.Count > currentItem)
            {
                dbComboBox.SelectedIndex = currentItem > -1 ? currentItem : 0;
                return;
            }

            dbComboBox.SelectedIndex = dbComboBox.Items.Count - 1;
        }

        /// <summary>
        /// Put databases into all comboboxes
        /// </summary>
        private void PutDatabasesList()
        {
            try
            {
                using (var cn = new SqlConnection(CreateConnectionString()))
                {
                    cn.Open();

                    var sc = new ServerConnection(cn);

                    var server = new Server(sc);

                    PutDatabasesList(comboBoxBackupDatabaseName, server);
                    PutDatabasesList(comboBoxRestoreDatabaseName, server);
                    PutDatabasesList(comboBoxDropDatabaseName, server);
                    PutDatabasesList(comboBoxExecutedDatabaseName, server);
                    PutDatabasesList(comboBoxFromDatabaseName, server);
                    PutDatabasesList(comboBoxToDatabaseName, server);
                    PutDatabasesList(comboBoxSelectDatabaseName, server);
                    var saveTexts = new[] { comboBoxFindString.Text, comboBoxReplaceString.Text };
                    PutDatabasesList(comboBoxFindString, server);
                    PutDatabasesList(comboBoxReplaceString, server);
                    comboBoxFindString.Text = saveTexts[0];
                    comboBoxReplaceString.Text = saveTexts[1];
                    PutDatabasesList(comboBoxAttachDatabaseName, server);
                    PutDatabasesList(comboBoxDetachDatabaseName, server);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Check connection
        /// </summary>        
        /// <returns></returns>
        private void CheckConnection()
        {
            try
            {
                using (var cn = new SqlConnection(CreateConnectionString()))
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Connection is wrong. See details:\r\n" + ex.Message);
            }
        }


        /// <summary>
        /// Check connection to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckConnection_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Application.DoEvents();
            try
            {
                CheckConnection();
                PutDatabasesList();
                MessageBox.Show("Connection is ok", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Select backup path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenBackupDatabase_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathBackup.Text = saveFileDialog1.FileName;
                comboBoxBackupDatabaseName.Focus();
            }
        }


        /// <summary>
        /// Select restore path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenRestoreDatabase_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Backup files|*.bak|All files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathRestore.Text = openFileDialog1.FileName;
                comboBoxRestoreDatabaseName.Focus();
            }
        }


        /// <summary>
        /// Backup database
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="path"></param>
        private void BackupDatabase(string databaseName, string path)
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var server = new Server(sc);

                var bkp = new Backup
                {
                    Action = BackupActionType.Database,
                    Checksum = true,
                    Initialize = true,
                    Incremental = false,
                    Database = databaseName
                };

                if (checkBoxUseBackupCompression.Checked)
                {
                    bkp.CompressionOption = BackupCompressionOptions.On;
                }

                bkp.Devices.Add(new BackupDeviceItem(path, DeviceType.File));
                bkp.SqlBackup(server);
            }
        }


        /// <summary>
        /// Start backup database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBackupDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();
            try
            {
                BackupDatabase(comboBoxBackupDatabaseName.Text, textBoxPathBackup.Text);

                PutDatabasesList();
                MessageBox.Show(string.Format("Database {0} backup successfully", comboBoxBackupDatabaseName.Text), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Restore database
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="path"></param>
        private void RestoreDatabase(string databaseName, string path)
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var server = new Server(sc);

                if (server.Databases[databaseName] != null)
                {
                    DialogResult dRes = MessageBox.Show(string.Format("Database {0} already exist. Replace it?", databaseName), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dRes == DialogResult.No)
                    {
                        return;
                    }

                    DropDatabase(databaseName, false);
                }

                var res = new Restore
                {
                    NoRecovery = false,
                    Database = databaseName
                };
                res.Devices.AddDevice(path, DeviceType.File);

                //
                // Db files path (on the SQL server!)
                //
                DataTable fileList = res.ReadFileList(server);

                foreach (DataRow file2Relocate in fileList.Rows)
                {
                    res.RelocateFiles.Add(new RelocateFile(
                        // Logical name in the backup
                        (string)file2Relocate["LogicalName"],

                        // New physical name
                        Path.Combine(
                            server.Databases["master"].PrimaryFilePath,
                            Path.ChangeExtension(
                                databaseName,
                                Path.GetExtension((string)file2Relocate["PhysicalName"])))));
                }

                res.SqlRestore(server);
            }
        }

        private void ExecuteNonQuery(string command)
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                var sc = new ServerConnection(cn) { StatementTimeout = 0 };

                sc.ExecuteNonQuery(command);
            }
        }

        private T ExecuteScalar<T>(string command)
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                var sc = new ServerConnection(cn) { StatementTimeout = 0 };

                return (T)sc.ExecuteScalar(command);
            }
        }

        /// <summary>
        /// Start restore database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();
            try
            {
                Dictionary<string, string> databasesForRestore = GetDatabasesForRestore();

                int allCount = databasesForRestore.Keys.Count;
                int cur = 1;
                foreach (string databaseName in databasesForRestore.Keys)
                {
                    if (allCount > 1)
                    {
                        labelAction.Text = string.Format("Action: restoring {0} database...({1}/{2})", databaseName, cur, allCount);
                        Application.DoEvents();
                    }

                    RestoreDatabase(databaseName, databasesForRestore[databaseName]);

                    if (checkBoxChangeDBOwner.Checked && !checkBoxWindowsAuthorization.Checked)
                    {
                        ExecuteNonQuery(string.Format("USE {0}\r\nexec sp_changedbowner '{1}'", databaseName, textBoxLogin.Text));
                    }

                    if (checkBoxSetTrustworthy.Checked)
                    {
                        ExecuteNonQuery(string.Format("alter database {0} set trustworthy on", databaseName));
                    }

                    if (checkBoxSetNewBrokerOnResore.Checked)
                    {
                        ExecuteNonQuery(string.Format("alter database {0} set new_broker", databaseName));
                    }

                    cur++;
                }

                PutDatabasesList();
                MessageBox.Show(
                    allCount == 1 ? string.Format("Database {0} was restored successfully", this.comboBoxRestoreDatabaseName.Text) : "Databases were restored successfully",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelAction.Text = "";
                Enabled = true;
            }
        }

        private Dictionary<string, string> GetDatabasesForRestore()
        {
            var databasesForRestore = new Dictionary<string, string>();

            string initialPath = textBoxPathRestore.Text;

            if (!checkBoxRestoreAllDb.Checked)
            {
                databasesForRestore[comboBoxRestoreDatabaseName.Text] = initialPath;
            }
            else
            {
                var di = new DirectoryInfo(Path.GetDirectoryName(initialPath) ?? string.Empty);
                foreach (FileInfo fi in di.GetFiles())
                {
                    databasesForRestore[Path.GetFileNameWithoutExtension(fi.FullName) ?? fi.Name] = fi.FullName;
                }
            }

            return databasesForRestore;
        }


        /// <summary>
        /// Kill database
        /// </summary>
        /// <param name="srv">Connection to  server</param>
        /// <param name="dbName">The database name</param>
        private static void KillDatabase(Server srv, string dbName)
        {
            try
            {
                srv.KillDatabase(dbName);
            }
            catch
            {
                srv.KillAllProcesses(dbName);
                srv.KillDatabase(dbName);
            }
        }

        /// <summary>
        /// Drop existed database
        /// </summary>
        /// <param name="dbName">The database name</param>
        /// <param name="isStartWith">If true - all databases started with dbName will be deleted</param>
        private void DropDatabase(string dbName, bool isStartWith)
        {
            SqlConnection.ClearAllPools();

            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var srv = new Server(sc);

                if (!isStartWith)
                {
                    if (srv.Databases[dbName] != null)
                    {
                        KillDatabase(srv, dbName);
                    }
                }
                else
                {
                    var databases = new Database[srv.Databases.Count];
                    srv.Databases.CopyTo(databases, 0);
                    foreach (Database database in databases)
                    {
                        if (database.Name.StartsWith(dbName))
                        {
                            KillDatabase(srv, database.Name);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Drop database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDropDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();
            string databaseName = comboBoxDropDatabaseName.Text;
            try
            {
                DropDatabase(databaseName, checkBoxDropDatabaseStartWith.Checked);

                PutDatabasesList();
                MessageBox.Show(string.Format("Database {0} drop successfully", databaseName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Copy database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopyDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();
            string tempPath = @"C:\Temp\tempDatabase";
            try
            {
                int n = 1;
                while (File.Exists(tempPath + n + ".bak"))
                {
                    n++;
                }

                tempPath += n + ".bak";

                BackupDatabase(comboBoxFromDatabaseName.Text, tempPath);

                RestoreDatabase(comboBoxToDatabaseName.Text, tempPath);

                if (checkBoxSetNewBroker.Checked)
                {
                    ExecuteNonQuery(string.Format("alter database [{0}] set new_broker", comboBoxToDatabaseName.Text));
                }

                if (checkBoxSetTrustworthyOn.Checked)
                {
                    ExecuteNonQuery(string.Format("alter database [{0}] set trustworthy on", comboBoxToDatabaseName.Text));
                }

                PutDatabasesList();
                MessageBox.Show(string.Format("Database {0} copyed successfully to database {1}", comboBoxFromDatabaseName.Text, comboBoxToDatabaseName.Text), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }

                Enabled = true;
            }
        }


        /// <summary>
        /// Change name of databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();

            try
            {
                using (var cn = new SqlConnection(CreateConnectionString()))
                {
                    cn.Open();

                    var sc = new ServerConnection(cn);

                    var srv = new Server(sc);

                    foreach (Database database in srv.Databases)
                    {
                        if (database.Name.Contains(comboBoxFindString.Text))
                        {
                            string newName = database.Name.Replace(comboBoxFindString.Text, comboBoxReplaceString.Text);

                            sc.ExecuteNonQuery(string.Format("ALTER DATABASE {0} MODIFY NAME = {1}", database.Name, newName));
                        }
                    }
                }

                PutDatabasesList();
                MessageBox.Show(string.Format("Databases replace successfully"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Detach database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDetach_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();
            string databaseName = comboBoxDetachDatabaseName.Text;
            try
            {
                using (var connection = new SqlConnection(CreateConnectionString()))
                {
                    connection.Open();
                    var sc = new ServerConnection(connection);
                    var srv = new Server(sc);

                    srv.DetachDatabase(databaseName, false, false);
                }

                PutDatabasesList();
                MessageBox.Show(string.Format("Database {0} detach successfully", databaseName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Select MDF file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenMdfFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Database filed|*.mdf|All files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathMdf.Text = openFileDialog1.FileName;
                string ldfPath = Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName) ?? string.Empty, Path.GetFileNameWithoutExtension(openFileDialog1.FileName) ?? string.Empty) + ".LDF";
                if (File.Exists(ldfPath))
                {
                    textBoxPathLdf.Text = ldfPath;
                }
                else
                {
                    ldfPath = Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName) ?? string.Empty, Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + "_log") + ".LDF";
                    if (File.Exists(ldfPath))
                    {
                        textBoxPathLdf.Text = ldfPath;
                    }
                }
                comboBoxAttachDatabaseName.Focus();
            }
        }


        /// <summary>
        /// Select LDF file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenLdfFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Database log filed|*.ldf|All files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathLdf.Text = openFileDialog1.FileName;
                comboBoxAttachDatabaseName.Focus();
            }
        }


        /// <summary>
        /// Attach database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttach_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();
            string databaseName = comboBoxAttachDatabaseName.Text;
            try
            {
                using (var connection = new SqlConnection(CreateConnectionString()))
                {
                    connection.Open();
                    var sc = new ServerConnection(connection);
                    var srv = new Server(sc);

                    srv.AttachDatabase(
                        databaseName,
                        new StringCollection { textBoxPathMdf.Text, textBoxPathLdf.Text });
                }

                PutDatabasesList();
                MessageBox.Show(string.Format("Database {0} attach successfully", databaseName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Get list of Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetListOfItems_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();

            int tableCnt = 0;
            int itemsCnt = 0;
            try
            {
                textBoxListOfItems.Text = string.Empty;

                using (var cn = new SqlConnection(CreateConnectionString()))
                {
                    cn.Open();

                    var sc = new ServerConnection(cn);

                    var srv = new Server(sc);

                    Database database = srv.Databases[comboBoxSelectDatabaseName.Text];

                    var sb = new StringBuilder();
                    if (comboBoxItemList.Text == "Tables")
                    {
                        foreach (Table item in database.Tables)
                        {
                            sb.Append(item.Name + "\r\n");
                        }

                        itemsCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Columns")
                    {
                        foreach (Table table in database.Tables)
                        {
                            sb.Append(table.Name + "\r\n");

                            foreach (Column item in database.Tables[table.Name].Columns)
                            {
                                sb.Append("  " + item.Name + "\r\n");
                            }

                            itemsCnt += database.Tables[table.Name].Columns.Count;
                        }

                        tableCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Checks")
                    {
                        foreach (Table table in database.Tables)
                        {
                            sb.Append(table.Name + "\r\n");

                            foreach (Check item in database.Tables[table.Name].Checks)
                            {
                                sb.Append("  " + item.Name + "\r\n");
                            }

                            itemsCnt += database.Tables[table.Name].Checks.Count;
                        }

                        tableCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Foreign Keys")
                    {
                        foreach (Table table in database.Tables)
                        {
                            sb.Append(table.Name + "\r\n");

                            foreach (ForeignKey item in database.Tables[table.Name].ForeignKeys)
                            {
                                sb.Append("  " + item.Name + "\r\n");
                            }

                            itemsCnt += database.Tables[table.Name].ForeignKeys.Count;
                        }

                        tableCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Table Properties")
                    {
                        foreach (Table table in database.Tables)
                        {
                            sb.Append(table.Name + "\r\n");

                            foreach (Property item in database.Tables[table.Name].Properties)
                            {
                                sb.AppendFormat("  {0} = {1} \r\n", item.Name, item.Value);
                            }

                            itemsCnt += database.Tables[table.Name].Properties.Count;
                        }

                        tableCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Triggers")
                    {
                        foreach (Table table in database.Tables)
                        {
                            sb.Append(table.Name + "\r\n");

                            foreach (Trigger item in database.Tables[table.Name].Triggers)
                            {
                                sb.Append("  " + item.Name + "\r\n");
                            }

                            itemsCnt += database.Tables[table.Name].Triggers.Count;
                        }

                        tableCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Indexes")
                    {
                        foreach (Table table in database.Tables)
                        {
                            sb.Append(table.Name + "\r\n");

                            foreach (Index item in database.Tables[table.Name].Indexes)
                            {
                                sb.Append("  " + item.Name + "\r\n");
                            }

                            itemsCnt += database.Tables[table.Name].Indexes.Count;
                        }

                        tableCnt = database.Tables.Count;
                    }
                    else if (comboBoxItemList.Text == "Stored Proccedures")
                    {
                        foreach (StoredProcedure item in database.StoredProcedures)
                        {
                            sb.Append(item.Name + "\r\n");
                        }

                        itemsCnt = database.StoredProcedures.Count;
                    }
                    else if (comboBoxItemList.Text == "Users")
                    {
                        foreach (User item in database.Users)
                        {
                            sb.Append(item.Name + "\r\n");
                        }

                        itemsCnt = database.Users.Count;
                    }
                    else if (comboBoxItemList.Text == "Views")
                    {
                        foreach (View item in database.Views)
                        {
                            sb.Append(item.Name + "\r\n");
                        }

                        itemsCnt = database.Views.Count;
                    }
                    else if (comboBoxItemList.Text == "Database Properties")
                    {
                        foreach (Property item in database.Properties)
                        {
                            sb.Append(item.Name + " = " + item.Value + "\r\n");
                        }

                        itemsCnt = database.Properties.Count;
                    }
                    else if (comboBoxItemList.Text == "Database List")
                    {
                        foreach (string item in comboBoxSelectDatabaseName.Items)
                        {
                            sb.Append(item + "\r\n");
                        }

                        itemsCnt = comboBoxSelectDatabaseName.Items.Count;
                    }
                    else
                    {
                        MessageBox.Show("Unknown type of item ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        labelGetInfo.Text = "Results:\r\n0 rows";
                        return;
                    }

                    labelGetInfo.Text = "Results:\r\n";
                    if (tableCnt > 0)
                    {
                        labelGetInfo.Text += "Tables = " + tableCnt + "\r\n";
                    }
                    labelGetInfo.Text += comboBoxItemList.Text + " = " + itemsCnt;

                    textBoxListOfItems.Text = sb.ToString().TrimEnd(new[] { '\r', '\n' });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Выполнить SQL команду
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExecuteSql_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            Application.DoEvents();

            try
            {
                ExecuteNonQuery(string.Format("USE {0}\r\n{1}", comboBoxExecutedDatabaseName.Text, textBoxSqlCommand.Text));

                PutDatabasesList();
                MessageBox.Show(string.Format("SQL command has executed successfully"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }


        /// <summary>
        /// Exit from program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Refresh button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PutDatabasesList();

            _changeServerSettings = false;
        }


        /// <summary>
        /// Fill databases list to all ComboBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_changeServerSettings)
            {
                PutDatabasesList();
                _changeServerSettings = false;
            }
        }


        /// <summary>
        /// Mark, that server setting was change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerSetting_TextChanges(object sender, EventArgs e)
        {
            _changeServerSettings = true;
        }


        #region Save params
        /// <summary>
        /// Save parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey.CreateSubKey(@"Software\DatabaseWorker");

            regKey.SetValue("textBoxServerName", textBoxServerName.Text);
            regKey.SetValue("textBoxLogin", textBoxLogin.Text);
            regKey.SetValue("textBoxPassword", textBoxPassword.Text);
            regKey.SetValue("textBoxPathBackup", textBoxPathBackup.Text);
            regKey.SetValue("comboBoxBackupDatabaseName", comboBoxBackupDatabaseName.Text);
            regKey.SetValue("textBoxPathRestore", textBoxPathRestore.Text);
            regKey.SetValue("comboBoxRestoreDatabaseName", comboBoxRestoreDatabaseName.Text);
            regKey.SetValue("comboBoxDropDatabaseName", comboBoxDropDatabaseName.Text);
            regKey.SetValue("comboBoxExecutedDatabaseName", comboBoxExecutedDatabaseName.Text);
            regKey.SetValue("comboBoxFromDatabaseName", comboBoxFromDatabaseName.Text);
            regKey.SetValue("comboBoxToDatabaseName", comboBoxToDatabaseName.Text);
            regKey.SetValue("comboBoxSelectDatabaseName", comboBoxSelectDatabaseName.Text);
            regKey.SetValue("comboBoxItemList", comboBoxItemList.Text);
            regKey.SetValue("textBoxListOfItems", textBoxListOfItems.Text);
            regKey.SetValue("labelGetInfo", labelGetInfo.Text);
            regKey.SetValue("comboBoxFindString", comboBoxFindString.Text);
            regKey.SetValue("comboBoxReplaceString", comboBoxReplaceString.Text);
            regKey.SetValue("comboBoxAttachDatabaseName", comboBoxAttachDatabaseName.Text);
            regKey.SetValue("comboBoxDetachDatabaseName", comboBoxDetachDatabaseName.Text);
            regKey.SetValue("textBoxPathMdf", textBoxPathMdf.Text);
            regKey.SetValue("textBoxPathLdf", textBoxPathLdf.Text);
            regKey.SetValue("textBoxSqlCommand", textBoxSqlCommand.Text);
            regKey.SetValue("checkBoxWindowsAuthorization", checkBoxWindowsAuthorization.Checked);
            regKey.SetValue("checkBoxUseBackupCompression", checkBoxUseBackupCompression.Checked);
            regKey.SetValue("checkBoxSetTrustworthyOn", checkBoxSetTrustworthyOn.Checked);
            regKey.SetValue("checkBoxSetNewBroker", checkBoxSetNewBroker.Checked);
            regKey.SetValue("checkBoxChangeDBOwner", checkBoxChangeDBOwner.Checked);
            regKey.SetValue("checkBoxSetNewBrokerOnResore", checkBoxSetNewBrokerOnResore.Checked);
            regKey.SetValue("checkBoxSetTrustworthy", checkBoxSetTrustworthy.Checked);
            regKey.SetValue("checkBoxDropDatabaseStartWith", checkBoxDropDatabaseStartWith.Checked);
            regKey.SetValue("checkBoxRestoreAllDb", checkBoxRestoreAllDb.Checked);
        }


        /// <summary>
        /// Load parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey.CreateSubKey(@"Software\DatabaseWorker");

            var s = (string)regKey.GetValue("textBoxServerName", string.Empty);
            textBoxServerName.Text = string.IsNullOrEmpty(s) ? Environment.MachineName : s;

            s = (string)regKey.GetValue("textBoxLogin", string.Empty);
            textBoxLogin.Text = string.IsNullOrEmpty(s) ? "sa" : s;

            s = (string)regKey.GetValue("textBoxPassword", string.Empty);
            textBoxPassword.Text = string.IsNullOrEmpty(s) ? "firm" : s;

            try
            {
                CheckConnection();
                PutDatabasesList();
                _changeServerSettings = false;
            }
            catch
            {
            }

            s = (string)regKey.GetValue("textBoxPathBackup", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                textBoxPathBackup.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxBackupDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxBackupDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("textBoxPathRestore", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                textBoxPathRestore.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxRestoreDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxRestoreDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxDropDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxDropDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxExecutedDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxExecutedDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxFromDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxFromDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxToDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxToDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxSelectDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxSelectDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxItemList", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxItemList.Text = s;
            }
            else
            {
                if (comboBoxItemList.Items.Count > 0)
                {
                    comboBoxItemList.SelectedIndex = 0;
                }
            }

            s = (string)regKey.GetValue("textBoxListOfItems", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                textBoxListOfItems.Text = s;
            }

            s = (string)regKey.GetValue("labelGetInfo", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                labelGetInfo.Text = s;
            }

            s = (string)regKey.GetValue("comboBoxFindString", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxFindString.Text = s;
            }
            else
            {
                if (comboBoxFindString.Items.Count > 0)
                {
                    comboBoxFindString.SelectedIndex = 0;
                }
            }

            s = (string)regKey.GetValue("comboBoxReplaceString", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxReplaceString.Text = s;
            }
            else
            {
                if (comboBoxReplaceString.Items.Count > 0)
                {
                    comboBoxReplaceString.SelectedIndex = 0;
                }
            }

            s = (string)regKey.GetValue("comboBoxDetachDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxDetachDatabaseName.Text = s;
            }
            else
            {
                if (comboBoxDetachDatabaseName.Items.Count > 0)
                {
                    comboBoxDetachDatabaseName.SelectedIndex = 0;
                }
            }

            s = (string)regKey.GetValue("comboBoxAttachDatabaseName", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                comboBoxAttachDatabaseName.Text = s;
            }

            s = (string)regKey.GetValue("textBoxPathMdf", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                textBoxPathMdf.Text = s;
            }

            s = (string)regKey.GetValue("textBoxPathLdf", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                textBoxPathLdf.Text = s;
            }

            s = (string)regKey.GetValue("textBoxSqlCommand", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                textBoxSqlCommand.Text = s;
            }

            s = (string)regKey.GetValue("checkBoxWindowsAuthorization", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxWindowsAuthorization.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxUseBackupCompression", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxUseBackupCompression.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxSetTrustworthyOn", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxSetTrustworthyOn.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxSetNewBroker", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxSetNewBroker.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxChangeDBOwner", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxChangeDBOwner.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxSetNewBrokerOnResore", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxSetNewBrokerOnResore.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxSetTrustworthy", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxSetTrustworthy.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxDropDatabaseStartWith", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxDropDatabaseStartWith.Checked = Convert.ToBoolean(s);
            }

            s = (string)regKey.GetValue("checkBoxRestoreAllDb", string.Empty);
            if (!string.IsNullOrEmpty(s))
            {
                checkBoxRestoreAllDb.Checked = Convert.ToBoolean(s);
            }
        }
        #endregion

        #region Show tooltips
        private void textBoxPathMdf_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxPathMdf.Text.Trim().Length > 50)
            {
                toolTip1.Show(textBoxPathMdf.Text, textBoxPathMdf, 10, -18);
            }
        }

        private void textBoxPathMdf_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(textBoxPathMdf);
        }

        private void textBoxPathLdf_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxPathLdf.Text.Trim().Length > 50)
            {
                toolTip1.Show(textBoxPathLdf.Text, textBoxPathLdf, 10, -18);
            }
        }

        private void textBoxPathLdf_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(textBoxPathLdf);
        }


        private void checkBoxDropDatabaseStartWith_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("If cheched program will remove all databases, starts with selected name", checkBoxDropDatabaseStartWith, 10, -18);
        }

        private void checkBoxDropDatabaseStartWith_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(checkBoxDropDatabaseStartWith);
        }
        #endregion


        /// <summary>
        /// Выделение всего текста в поле textBoxListOfItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxListOfItems.SelectionStart = 0;
            textBoxListOfItems.SelectionLength = textBoxListOfItems.Text.Length;
        }

        /// <summary>
        /// Убрать/показать поля для ввода логина/пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxWindowsAuthorization_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLogin.Enabled = textBoxPassword.Enabled = !checkBoxWindowsAuthorization.Checked;
        }

        private void checkBoxRestoreAllDb_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxRestoreDatabaseName.Enabled = !checkBoxRestoreAllDb.Checked;
        }

        /// <summary>
        /// Создать указанное количество копий дефолтной катишной базы данных
        /// Для новых копий надо выставить новый сервис брокер и trustworthy on
        /// Кроме того, в дефолтную базу в таблицу [BvBackendInstance] надо добавить записи о новых базах данных
        /// Если база с очередным индексом уже существует - то пересоздавать её не надо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreateCATIInstances_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int firstIndex;
            if (!int.TryParse(textBoxFirstIndex.Text, out firstIndex))
            {
                MessageBox.Show("First index is wrong. It should be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstIndex.Focus();
                return;
            }
            int lastIndex;
            if (!int.TryParse(textBoxLastIndex.Text, out lastIndex))
            {
                MessageBox.Show("Last index is wrong. It should be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstIndex.Focus();
                return;
            }

            const string defaultCATIDatabaseName = "ConfirmitCATIV15";
            progressBarCATICreation.Maximum = lastIndex - firstIndex + 1;
            progressBarCATICreation.Value = 0;

            Enabled = false;
            Application.DoEvents();
            string tempPath = @"C:\Temp\tempDatabase";
            try
            {
                int n = 1;
                while (File.Exists(tempPath + n + ".bak"))
                {
                    n++;
                }

                tempPath += n + ".bak";

                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    string newDatabaseName = defaultCATIDatabaseName + "_" + i;
                    if (!IsDatabaseExisted(newDatabaseName))
                    {
                        BackupDatabase(defaultCATIDatabaseName, tempPath);

                        RestoreDatabase(newDatabaseName, tempPath);

                        ExecuteNonQuery(string.Format("alter database {0} set new_broker", newDatabaseName));
                        ExecuteNonQuery(string.Format("alter database {0} set trustworthy on", newDatabaseName));                        
                    }

                    ExecuteNonQuery(string.Format(@"
IF NOT EXISTS(SELECT 1 FROM [{0}].[dbo].[BvBackendInstance] WHERE ServiceName = 'Confirmit.CATI.Backend${1}')
BEGIN
INSERT INTO [{0}].[dbo].[BvBackendInstance] VALUES ('Confirmit.CATI.Backend${1}')
END", defaultCATIDatabaseName, i));

                    progressBarCATICreation.Value = i - firstIndex + 1;
                    Application.DoEvents();
                }

                PutDatabasesList();
                MessageBox.Show(string.Format("Instances were created successfully"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }

                progressBarCATICreation.Value = 0;
                Enabled = true;
            }
        }

        /// <summary>
        /// Для удаления инстанса достаточно удалить запись об удаляемом инстансе в дефолтной базе в таблице [BvBackendInstance],
        /// а потом удалить ненужные базы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveCatiInstances_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int firstIndex;
            if (!int.TryParse(textBoxFirstIndexForRemoving.Text, out firstIndex))
            {
                MessageBox.Show("First index is wrong. It should be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstIndexForRemoving.Focus();
                return;
            }
            int lastIndex;
            if (!int.TryParse(textBoxLastIndexForRemoving.Text, out lastIndex))
            {
                MessageBox.Show("Last index is wrong. It should be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstIndexForRemoving.Focus();
                return;
            }

            const string defaultCATIDatabaseName = "ConfirmitCATIV15";
            const string defaultCATIServiceName = "Confirmit.CATI.Backend.Rel";
            labelCatiInstancesRemovingAction.Visible = true;
            labelCatiInstancesRemovingAction.Text = "Action: Removing services";

            Enabled = false;
            Application.DoEvents();

            try
            {
                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    string removedDatabaseName = defaultCATIDatabaseName + "_" + i;
                    
                    ExecuteNonQuery(string.Format("DELETE [{0}].[dbo].[BvBackendInstance] WHERE ServiceName='Confirmit.CATI.Backend${1}'", defaultCATIDatabaseName, i));
                }

                // Wait until all services will be removed
                bool isAnyCatiServicesRunned;
                do
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    isAnyCatiServicesRunned = false;
                    var services = ServiceController.GetServices();
                    for (int i = firstIndex; i <= lastIndex; i++)
                    {
                        if (services.Any(x => x.ServiceName == defaultCATIServiceName + "$" + i))
                        {
                            isAnyCatiServicesRunned = true;
                            break;
                        }
                    }
                } while(isAnyCatiServicesRunned);

                labelCatiInstancesRemovingAction.Text = "Action: Removing databases";
                Application.DoEvents();

                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    string removedDatabaseName = defaultCATIDatabaseName + "_" + i;
                    DropDatabase(removedDatabaseName, false);
                }

                labelCatiInstancesRemovingAction.Visible = false;
                Application.DoEvents();

                PutDatabasesList();
                MessageBox.Show(string.Format("Instances were removed successfully"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelCatiInstancesRemovingAction.Visible = false;
                Enabled = true;
            }
        }

        private bool IsDatabaseExisted(string databaseName)
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                var sc = new ServerConnection(cn);

                var server = new Server(sc);

                return server.Databases[databaseName] != null;
            }
        }

        /// <summary>
        /// Generate profiling report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonProfilingReportGenerateClick(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (checkedListBoxTestRunList.CheckedItems.Count == 0)
                {
                    MessageBox.Show("You must select one or more test runs to generate report", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var whereStringBuilder = new StringBuilder("where ");
                foreach (string checkedItem in checkedListBoxTestRunList.CheckedItems)
                {
                    whereStringBuilder.AppendFormat("TestRunIdentifier = '{0}' or ", checkedItem);

                }

                string whereString = whereStringBuilder.ToString().Substring(0, whereStringBuilder.Length - 4);

                string cmdText = "select action, SUM(Duration) as duration from  ProfileDatabase.dbo.ProfileTable " + whereString + " group by action";
                var dataTable = new DataTable();

                using (var cn = new SqlConnection(CreateConnectionString()))
                using (var cmd = new SqlCommand(cmdText, cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }

                int allDuration = -100;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (dataRow.ItemArray[0].ToString() == "All Test Execution Time")
                    {
                        allDuration = Convert.ToInt32(dataRow.ItemArray[1]);
                        break;
                    }
                }

                if (allDuration < 0)
                {
                    MessageBox.Show("Action 'All Test Execution Time' isn't found. Selected test runs are wrong or profiling table is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var exportedDataList = new List<ExportedData>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    exportedDataList.Add(new ExportedData(dataRow.ItemArray[0].ToString(), Convert.ToInt32(dataRow.ItemArray[1]), allDuration));
                }

                new ExcelExporter().Export(exportedDataList.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get all test runs from profiling database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetTestRuns_Click(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            checkedListBoxTestRunList.Items.Clear();

            try
            {
                var testRunList = new List<string>();

                using (var cn = new SqlConnection(CreateConnectionString()))
                using (var cmd = new SqlCommand("select distinct TestRunIdentifier from ProfileDatabase.dbo.ProfileTable", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.Text;
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testRunList.Add((string)reader[0]);
                        }
                    }
                }

                foreach (string testRun in testRunList)
                {
                    checkedListBoxTestRunList.Items.Add(testRun);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Remove selected test runs from profiling table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRemoveTestRunsClick(object sender, EventArgs e)
        {
            try
            {
                CheckConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (checkedListBoxTestRunList.CheckedItems.Count == 0)
                {
                    MessageBox.Show("You must select one or more test runs to remove them", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DialogResult.Yes != MessageBox.Show("Are you sure you want to remove selected test runs?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }

                var whereStringBuilder = new StringBuilder("where ");
                foreach (string checkedItem in checkedListBoxTestRunList.CheckedItems)
                {
                    whereStringBuilder.AppendFormat("TestRunIdentifier = '{0}' or ", checkedItem);
                }

                string whereString = whereStringBuilder.ToString().Substring(0, whereStringBuilder.Length - 4);

                string cmdText = "delete from ProfileDatabase.dbo.ProfileTable " + whereString;

                using (var cn = new SqlConnection(CreateConnectionString()))
                using (var cmd = new SqlCommand(cmdText, cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                buttonGetTestRuns_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool _stopMonitoring;

        private void checkBoxSelectAllTestRuns_CheckedChanged(object sender, EventArgs e)
        {
            if (_stopMonitoring)
            {
                return;
            }

            _stopMonitoring = true;
            for(int i = 0; i < checkedListBoxTestRunList.Items.Count; i++)
            {
                checkedListBoxTestRunList.SetItemChecked (i, checkBoxSelectAllTestRuns.Checked);
            }
            _stopMonitoring = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (_stopMonitoring)
            {
                return;
            }

            _stopMonitoring = true;
            checkBoxSelectAllTestRuns.Checked = checkedListBoxTestRunList.Items.Count == checkedListBoxTestRunList.CheckedItems.Count;
            _stopMonitoring = false;
        }

        private void checkedListBoxTestRunList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
