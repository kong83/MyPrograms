using System;
using System.IO;

using System.Xml;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Text;

namespace DeleteAllWixInstallation
{
    public partial class CheckInstallV14Form : Form
    {
        readonly string[] _beFiles;
        readonly string[] _catiFiles;
        readonly string[] _catiFilesBin;
        readonly string[] _monitoringFiles;
        readonly string[] _monitoringFilesBin;
        readonly string[] _managementFiles;
        readonly string[] _managementFilesBin;

        public CheckInstallV14Form()
        {
            InitializeComponent();

            #region Arrays initialization
            _beFiles = new[]
            {
                "BECommon.DLL",
                "BvCallHandlerLibrary.DLL",
                "bvcallhandlerlibrary.tlb",
                "BVCALLQUEUEu.DLL",
                "BvCallQueueu.Interop.dll",
                "BVCFGu.EXE",
                "BVDBSINSTANCEREGu.DLL",
                "BvDbsInstanceRegu.Interop.dll",
                "BVDBSSVCu.EXE",
                "bvdbssvcu.exe.config",
                "BVDBSu.DLL",
                "BvDbsu.Interop.dll",
                "BvDotNetEngine.DLL",
                "bvdotnetengine.tlb",
                "BvDotNetScript.DLL",
                "BVDUMPSUTILu.EXE",
                "BVEVENTu.DLL",
                "BvEventu.Interop.dll",
                "BvInterpreter.dll",
                "BVINTERVIEWu.DLL",
                "BvInterviewu.Interop.dll",
                "BVOBJECTu.DLL",
                "BVPARAMu.DLL",
                "BVQMDCOMu.DLL",
                "BVQSLTRANSLATORu.DLL",
                "BVQSLTRANSLATORu.Interop.dll",
                "BvSchScriptGen.DLL",
                "bvschscriptgen.tlb",
                "bvsp.dat",
                "BVSYNCMANAGERu.DLL",
                "BVTASKSPSu.DLL",
                "BVTASKSu.DLL",
                "BvTasksu.Interop.dll",
                "BVTOOLSu.DLL",
                "BVUNIHOSTu.DLL",
                "BvWebServicesTools.DLL",
                "ConfirmitInternalWSAccess.DLL",
                "confirmitinternalwsaccess.tlb",
                "dbghelp.dll",
                "EPROCu.DLL",
                "MaximiseApiLibrary.DLL",
                "MnTciLibrary.DLL",
                "mntcilibrary.tlb",
                "sitekey.txt",
                "SmartLogger.DLL",
                "stlport_vc646.dll",
                "zipu.dll",
                "zlib.dll"
            };

            _managementFiles = new[] 
            { 
                "Service.asmx",
                "Web.config",                
            };
            _managementFilesBin = new[] 
            { 
                "BECommon.dll",
                "BvDbsInstanceRegu.Interop.Dll",
                "BvDbsServices.dll",
                "BvDbsu.Interop.Dll",
                "BVDUMPSUTILu.EXE",
                "BvFMWS.dll",
                "BvWebServicesTools.dll",
                "ConfirmitInternalWSAccess.dll",
                "dbghelp.dll",
                "MaximiseApiLibrary.DLL",
                "SmartLogger.DLL"
            };

            _monitoringFiles = new[] 
            { 
                "Global.asax",
                "InterviewerService.svc",
                "SupervisorService.svc",
                "Web.config",                
            };
            _monitoringFilesBin = new[] 
            { 
                "BvDbsInstanceRegu.Interop.Signed.dll",
                "BvDbsu.Interop.Signed.dll",
                "CatiInterviewerConsole.Monitoring.dll",
                "MonitoringService.ServiceLibrary.dll",
                "MonitoringService.WebService.dll",
                "SmartLogger.Signed.DLL"
            };

            _catiFiles = new[] 
            { 
                "CATIConsoleWebServ.asmx",
                "Web.config",
                "CATIConsoleWebServ.asmx",
                "Web.config",
                
            };
            _catiFilesBin = new[] 
            { 
                "BECommon.DLL",
                "BvCallHandlerLibrary.DLL",
                "BvDbsInstanceRegu.Interop.dll",
                "BvDbsServices.DLL",
                "BvDbsu.Interop.dll",
                "BvInterviewu.Interop.dll",
                "BvMonitoring.DLL",
                "BvTasksu.Interop.dll",
                "BvWebServicesTools.DLL",
                "CATIConsoleWS.DLL",
                "MnTciLibrary.DLL",
                "SmartLogger.DLL"
            };
            #endregion
        }


        /// <summary>
        /// Check button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            buttonCheck.Enabled = false;
            listBoxResult.Items.Clear();

            try
            {
                CheckPaths();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            try
            {
                CheckRegistryKey();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            try
            {
                CheckSomeParameters();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            try
            {
                if (checkBoxDatabase.Checked)
                    CheckDatabase();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            try
            {
                if (checkBoxCP.Checked)
                    CheckCP();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            try
            {
                if (checkBoxCATI.Checked)
                    CheckCATI();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            try
            {
                if (checkBoxManagement.Checked)
                    CheckBvFMWS();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.Message);
            }


            if (listBoxResult.Items.Count == 0)
            {
                AddResultValue("All right");
            }
            else
            {
                AddResultValue("Total: " + listBoxResult.Items.Count + " errors");
            }
            buttonCheck.Enabled = true;
        }


        /// <summary>
        /// Add new vlue to checkedListBoxResult
        /// </summary>
        /// <param name="value">String</param>       
        private void AddResultValue(string value)
        {
            listBoxResult.Items.Add(value);
            Application.DoEvents();
        }


        private void CheckFiles(IEnumerable<string> arrayOfTestFiles, string path, string feauture)
        {
            var di = new DirectoryInfo(path);

            if (di.Exists)
            {
                FileInfo[] fileInfos = di.GetFiles();
                var realFiles = new string[fileInfos.Length];
                int i = 0;
                foreach (FileInfo fi in fileInfos)
                {
                    if (!arrayOfTestFiles.Contains(fi.Name))
                    {
                        AddResultValue(feauture + " file \"" + fi.Name + "\" is excess");
                    }
                    realFiles[i++] = fi.Name;
                }

                foreach (string file in arrayOfTestFiles)
                {
                    if (!realFiles.Contains(file))
                    {
                        AddResultValue(feauture + " file \"" + file + "\" not found");
                    }                    
                }
            }
            else
            {
                AddResultValue("\"" + feauture + "\" path not found");
            }
        }

        /// <summary>
        /// Check Paths
        /// </summary>
        private void CheckPaths()
        {
            CheckFiles(_beFiles, textBoxBEPath.Text, "BE");
            CheckFiles(_catiFiles, textBoxCATIPath.Text, "CATI");
            CheckFiles(_catiFilesBin, Path.Combine(textBoxCATIPath.Text, "bin"), "CATI bin");
            CheckFiles(_managementFiles, textBoxManagementPath.Text, "Management");
            CheckFiles(_managementFilesBin, Path.Combine(textBoxManagementPath.Text, "bin"), "Management bin");
            CheckFiles(_monitoringFiles, textBoxMonitoringPath.Text, "Monitoring");
            CheckFiles(_monitoringFilesBin, Path.Combine(textBoxMonitoringPath.Text, "bin"), "Monitoring bin");
        }


        /// <summary>
        /// Check Database
        /// </summary>
        private void CheckDatabase()
        {
            try
            {
                var myServer = new Server(textBoxDatabase_ServerName.Text);
                ServerConnection conn = myServer.ConnectionContext;

                conn.LoginSecure = false;
                conn.Login = textBoxDatabase_Login.Text;
                conn.Password = textBoxDatabase_Password.Text;

                conn.Connect();

                if (myServer.Databases[textBoxDatabase_DatabaseName.Text] == null)
                {
                    AddResultValue("Database \"" + textBoxDatabase_DatabaseName.Text + "\" not found");
                }
            }
            catch
            {
                AddResultValue("Not connection to server \"" + textBoxDatabase_ServerName.Text + "\"");
            }
        }


        /// <summary>
        /// Check Control Panel
        /// </summary>
        private void CheckCP()
        {
            if (!Directory.Exists(textBoxCPPath.Text))
            {
                AddResultValue("\"" + textBoxCPPath.Text + "\" path not found");
                return;
            }
            var doc = new XmlDocument();
            doc.Load(Path.Combine(textBoxCPPath.Text, "Web.config"));
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("prefix", "http://schemas.microsoft.com/.NetConfiguration/v2.0");

            var changeLines = new Dictionary<string, string>();
            changeLines["//prefix:appSettings/prefix:add[@key=\"ConfirmitErrorPageUrl\"]/@value"] = textBoxCP_ErrorPage.Text;
            changeLines["//prefix:appSettings/prefix:add[@key=\"ConfirmitCPWSUrl\"]/@value"] = textBoxCP_CPWS.Text;

            changeLines["//prefix:appSettings/prefix:add[@key=\"ConfirmitKeepSessionAspUrl\"]/@value"] = textBoxCP_SessionASP.Text;
            changeLines["//prefix:appSettings/prefix:add[@key=\"ConfirmitKeepSessionAspxUrl\"]/@value"] = textBoxCP_SessionASPX.Text;

            string mode, cookieName, sqlConnectionString;

            if (radioButtonCP_SQLServer.Checked)
            {
                mode = "SQLServer";
                cookieName = textBoxCP_CookieName.Text;
                sqlConnectionString = string.Format("data source={0};User ID={1};Password={2}", textBoxCP_ServerName.Text, textBoxCP_Login.Text, textBoxCP_Password.Text);
            }
            else
            {
                mode = "Inproc";
                cookieName = "";
                sqlConnectionString = "";
            }

            changeLines["//prefix:system.web/prefix:sessionState/@mode"] = mode;
            changeLines["//prefix:system.web/prefix:sessionState/@cookieName"] = cookieName;
            changeLines["//prefix:system.web/prefix:sessionState/@sqlConnectionString"] = sqlConnectionString;

            foreach (string xPathQuery in changeLines.Keys)
            {
                XmlNode node = doc.SelectSingleNode(xPathQuery, nsmgr);
                if (node != null)
                {
                    if (node.Value != changeLines[xPathQuery])
                    {
                        AddResultValue("CP web config \"" + xPathQuery + "\" not valid");
                    }
                }
                else
                {
                    AddResultValue("CP web config \"" + xPathQuery + "\" not found");
                }
            }
        }


        /// <summary>
        /// Check CATI WS
        /// </summary>
        private void CheckCATI()
        {
            if (!Directory.Exists(textBoxCATIPath.Text))
            {
                AddResultValue("\"" + textBoxCATIPath.Text + "\" path not found");
                return;
            }
            var doc = new XmlDocument();
            doc.Load(Path.Combine(textBoxCATIPath.Text, "Web.config"));
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("prefix", "");

            var changeLines = new Dictionary<string, string>();
            changeLines["//prefix:appSettings/prefix:add[@key=\"BvSurveyURL\"]/@value"] = "http://" + textBoxDeploymentAddress.Text + "/wix/cati_";
            changeLines["//prefix:system.serviceModel/prefix:client/prefix:endpoint/@address"] = "http://" + textBoxWSAddress.Text + "/Confirmit/InternalWebServices/14.0/ConsoleToSurveyEngine.svc";

            foreach (string xPathQuery in changeLines.Keys)
            {
                XmlNode node = doc.SelectSingleNode(xPathQuery, nsmgr);
                if (node != null)
                {
                    if (node.Value != changeLines[xPathQuery])
                    {
                        AddResultValue("CATI web config \"" + xPathQuery + "\" not valid");
                    }
                }
                else
                {
                    AddResultValue("CATI web config \"" + xPathQuery + "\" not found");
                }
            }
        }


        /// <summary>
        /// Check Management WS
        /// </summary>
        private void CheckBvFMWS()
        {
            if (!Directory.Exists(textBoxManagementPath.Text))
            {
                AddResultValue("\"" + textBoxManagementPath.Text + "\" path not found");
                return;
            }
            var doc = new XmlDocument();
            doc.Load(Path.Combine(textBoxManagementPath.Text, "Web.config"));
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("prefix", "");

            var changeLines = new Dictionary<string, string>();

            changeLines["//prefix:appSettings/prefix:add[@key=\"TemplateDBFileName\"]/@value"] = textBoxDatabase_BackupPath.Text;
            changeLines["//prefix:appSettings/prefix:add[@key=\"TemplateDBName\"]/@value"] = textBoxDatabase_DatabaseName.Text;
            changeLines["//prefix:connectionStrings/prefix:add[@name=\"MasterSqlServer\"]/@connectionString"] = string.Format("server={0};uid={1};password={2}", textBoxDatabase_ServerName.Text, textBoxDatabase_Login.Text, textBoxDatabase_Password.Text);

            string mnDialerURL = "", maximizeAPIURL = "", webProxyURL = "", webProxyLogin = "",
                webProxyPassword = "", maximizeAPICustomerGroup = "", maximizeAPIUserName = "",
                maximizeAPIUserPassword = "", maximizeRecordingAPIURL = "", maximizeRecordingAPIUserPassword = "";

            if (radioButtonManagem_UseDialer.Checked)
            {
                mnDialerURL = textBoxManagem_Dialer.Text;
                maximizeAPIURL = textBoxManagem_MaximizeUrl.Text;
                maximizeAPICustomerGroup = textBoxManagem_MaximizeGroup.Text;
                maximizeAPIUserName = textBoxManagem_MaximizeName.Text;
                maximizeAPIUserPassword = textBoxManagem_MaximizePassw.Text;
                maximizeRecordingAPIURL = textBoxManagem_MaximizeRecordUrl.Text;
                maximizeRecordingAPIUserPassword = textBoxManagem_MaximizeRecordPassw.Text;
            }

            if (radioButtonManagem_UseDialer.Checked && radioButtonManagem_UseProxy.Checked)
            {
                webProxyURL = textBoxManagem_ProxyUrl.Text;
                webProxyLogin = textBoxManagem_ProxyLogin.Text;
                webProxyPassword = textBoxManagem_ProxyPassw.Text;
            }

            changeLines["//prefix:appSettings/prefix:add[@key=\"MNDialerURL\"]/@value"] = mnDialerURL;
            changeLines["//prefix:appSettings/prefix:add[@key=\"MaximizeAPIURL\"]/@value"] = maximizeAPIURL;
            changeLines["//prefix:appSettings/prefix:add[@key=\"MaximizeAPICustomerGroup\"]/@value"] = maximizeAPICustomerGroup;
            changeLines["//prefix:appSettings/prefix:add[@key=\"MaximizeAPIUserName\"]/@value"] = maximizeAPIUserName;
            changeLines["//prefix:appSettings/prefix:add[@key=\"MaximizeAPIUserPassword\"]/@value"] = maximizeAPIUserPassword;
            changeLines["//prefix:appSettings/prefix:add[@key=\"MaximizeRecordingAPIURL\"]/@value"] = maximizeRecordingAPIURL;
            changeLines["//prefix:appSettings/prefix:add[@key=\"MaximizeRecordingAPIUserPassword\"]/@value"] = maximizeRecordingAPIUserPassword;

            changeLines["//prefix:appSettings/prefix:add[@key=\"WebProxyURL\"]/@value"] = webProxyURL;
            changeLines["//prefix:appSettings/prefix:add[@key=\"WebProxyLogin\"]/@value"] = webProxyLogin;
            changeLines["//prefix:appSettings/prefix:add[@key=\"WebProxyPassword\"]/@value"] = webProxyPassword;

            foreach (string xPathQuery in changeLines.Keys)
            {
                XmlNode node = doc.SelectSingleNode(xPathQuery, nsmgr);
                if (node != null)
                {
                    if (node.Value != changeLines[xPathQuery])
                    {
                        AddResultValue("BvFMWS web config \"" + xPathQuery + "\" not valid");
                    }
                }
                else
                {
                    AddResultValue("BvFMWS web config \"" + xPathQuery + "\" not found");
                }
            }
        }


        /// <summary>
        /// Check registry records with default path "Software\Pulse Train\Bv\7.00"
        /// </summary>
        /// <param name="key">Key name</param>
        /// <param name="value">Expected value</param>
        private void CheckRegistry(string key, string value)
        {
            CheckRegistry(@"Software\Pulse Train\Bv\7.00", key, value);
        }

        /// <summary>
        /// Check registry records
        /// </summary>
        /// <param name="path">Path in the registry</param>
        /// <param name="key">Key name</param>
        /// <param name="value">Expected value</param>
        private void CheckRegistry(string path, string key, string value)
        {
            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey(path);

            if (regKey != null)
            {
                var s = regKey.GetValue(key);
                if (s.ToString() != value)
                {
                    AddResultValue("Registry key \"" + key + "\" not valid");
                }
            }
            else
            {
                AddResultValue("Registry key \"" + key + "\" not found");
            }
        }

        /// <summary>
        /// Check Registry Key
        /// </summary>
        private void CheckRegistryKey()
        {
            string oleConnectString = "Provider=SQLOLEDB.1;Data Source=" + textBoxDatabase_ServerName.Text +
                ";Initial Catalog=" + textBoxDatabase_DatabaseName.Text +
                ";User ID=" + textBoxDatabase_Login.Text + ";PASSWORD=" + textBoxDatabase_Password.Text;

            CheckRegistry("DB Connection String", oleConnectString);

            CheckRegistry("Dumps.Disabled", "1");
            CheckRegistry("Path", textBoxBEPath.Text);
            CheckRegistry("Confirmit.AuthoringWS.WebServiceUrl", textBoxCATI_Authoring.Text);
            CheckRegistry("Confirmit.SurveyDataWS.WebServiceUrl", textBoxCATI_SurveyData.Text);
            CheckRegistry("Confirmit.LoginWS.WebServiceUrl", textBoxCATI_UrlLogin.Text);
            CheckRegistry("Confirmit.LoginWS.Login", textBoxCATI_LoginLogin.Text);
            CheckRegistry("Confirmit.LoginWS.Password", textBoxCATI_PasswordLogin.Text);
            CheckRegistry("DB Number Of Connections", "32");
            CheckRegistry("RemoteSites.Enabled", "0");
            CheckRegistry("RemoteSites.IsCentralSite", "1");
            CheckRegistry("Dumps.CreateFromVectored", "1");

            const string path = @"Software\Pulse Train\Bv\7.00\InstallationPaths";
            CheckRegistry(path, "INSTALLLOCATION", textBoxBEPath.Text);
            CheckRegistry(path, "BVFMWSROOTPATH", textBoxManagementPath.Text);
            CheckRegistry(path, "CPROOTPATH", textBoxCPPath.Text);
            CheckRegistry(path, "CATICONSOLEWSROOTPATH", textBoxCATIPath.Text);
            CheckRegistry(path, "MONITORINGWSROOTPATH", textBoxMonitoringPath.Text);
            CheckRegistry(path, "BVFMWEBSERVICEALIAS", textBoxManagementAliase.Text);
            CheckRegistry(path, "CATICONSOLEWEBSERVICEALIAS", textBoxCATIAliase.Text);
            CheckRegistry(path, "CONTROLPANELWEBSERVICEALIAS", textBoxCPAliase.Text);
            CheckRegistry(path, "MONITORINGWEBSERVICEALIAS", textBoxMonitoringAliase.Text);
        }


        /// <summary>
        /// Check backup file, event log, DbsSvc service and performance counter
        /// </summary>
        private void CheckSomeParameters()
        {
            if (!EventLog.SourceExists("CATI Confirmit"))
            {
                AddResultValue("Event log \"CATI Confirmit\" not found");
            }

            if (!File.Exists(textBoxDatabase_BackupPath.Text))
            {
                AddResultValue("Backup \"" + textBoxDatabase_BackupPath.Text + "\" not found");
            }

            bool isServiceContains = false;
            foreach (ServiceController service in ServiceController.GetServices())
            {
                if (service.ServiceName == "BvDbsSvc")
                {
                    isServiceContains = true;
                    if (service.Status != ServiceControllerStatus.Running)
                    {
                        AddResultValue("Service \"BvDbsSvc\" not running");
                    }
                    break;
                }
            }
            if (!isServiceContains)
            {
                AddResultValue("Service \"BvDbsSvc\" not found");
            }

            if (!PerformanceCounterCategory.CounterExists("Time between calls", "CATI Confirmit"))
            {
                AddResultValue("Performance counter \"CATI Confirmit\" not found");
            }
        }

        #region Show and hide some fields
        private void radioButtonManagem_UseDialer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonManagem_UseDialer.Checked)
            {
                textBoxManagem_Dialer.Enabled = textBoxManagem_MaximizeGroup.Enabled =
                textBoxManagem_MaximizeName.Enabled = textBoxManagem_MaximizePassw.Enabled =
                textBoxManagem_MaximizeRecordPassw.Enabled = textBoxManagem_MaximizeRecordUrl.Enabled =
                textBoxManagem_MaximizeUrl.Enabled = panelProxy.Enabled = true;
                if (radioButtonManagem_UseProxy.Checked)
                {
                    textBoxManagem_ProxyLogin.Enabled =
                    textBoxManagem_ProxyPassw.Enabled = textBoxManagem_ProxyUrl.Enabled = true;
                }
            }
        }

        private void radioButtonManagem_NoDialer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonManagem_NoDialer.Checked)
            {
                textBoxManagem_Dialer.Enabled = textBoxManagem_MaximizeGroup.Enabled =
                textBoxManagem_MaximizeName.Enabled = textBoxManagem_MaximizePassw.Enabled =
                textBoxManagem_MaximizeRecordPassw.Enabled = textBoxManagem_MaximizeRecordUrl.Enabled =
                textBoxManagem_MaximizeUrl.Enabled = textBoxManagem_ProxyLogin.Enabled =
                textBoxManagem_ProxyPassw.Enabled = textBoxManagem_ProxyUrl.Enabled =
                panelProxy.Enabled = false;
            }
        }

        private void radioButtonManagem_UseProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonManagem_UseProxy.Checked)
            {
                textBoxManagem_ProxyLogin.Enabled =
                textBoxManagem_ProxyPassw.Enabled = textBoxManagem_ProxyUrl.Enabled = true;
            }
        }

        private void radioButtonManagem_NoProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonManagem_NoProxy.Checked)
            {
                textBoxManagem_ProxyLogin.Enabled =
                textBoxManagem_ProxyPassw.Enabled = textBoxManagem_ProxyUrl.Enabled = false;
            }
        }

        private void radioButtonCP_SQLServer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCP_SQLServer.Checked)
            {
                textBoxCP_CookieName.Enabled = textBoxCP_Login.Enabled =
                textBoxCP_Password.Enabled = textBoxCP_ServerName.Enabled = true;
            }

        }

        private void radioButtonCP_Inproc_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCP_Inproc.Checked)
            {
                textBoxCP_CookieName.Enabled = textBoxCP_Login.Enabled =
                textBoxCP_Password.Enabled = textBoxCP_ServerName.Enabled = false;
            }
        }
        #endregion


        /// <summary>
        /// Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding("windows-1251"));
                const string splitStr = "^!&^";

                sw.WriteLine("textBoxAuthoringAddress" + splitStr + textBoxAuthoringAddress.Text + splitStr);
                sw.WriteLine("textBoxBEPath" + splitStr + textBoxBEPath.Text + splitStr);
                sw.WriteLine("textBoxCATI_Authoring" + splitStr + textBoxCATI_Authoring.Text + splitStr);
                sw.WriteLine("textBoxCATI_LoginLogin" + splitStr + textBoxCATI_LoginLogin.Text + splitStr);
                sw.WriteLine("textBoxCATI_PasswordLogin" + splitStr + textBoxCATI_PasswordLogin.Text + splitStr);
                sw.WriteLine("textBoxCATI_SurveyData" + splitStr + textBoxCATI_SurveyData.Text + splitStr);
                sw.WriteLine("textBoxCATI_UrlLogin" + splitStr + textBoxCATI_UrlLogin.Text + splitStr);
                sw.WriteLine("textBoxCATIAliase" + splitStr + textBoxCATIAliase.Text + splitStr);
                sw.WriteLine("textBoxCATIPath" + splitStr + textBoxCATIPath.Text + splitStr);
                sw.WriteLine("textBoxCP_CookieName" + splitStr + textBoxCP_CookieName.Text + splitStr);
                sw.WriteLine("textBoxCP_CPWS" + splitStr + textBoxCP_CPWS.Text + splitStr);
                sw.WriteLine("textBoxCP_ErrorPage" + splitStr + textBoxCP_ErrorPage.Text + splitStr);
                sw.WriteLine("textBoxCP_Login" + splitStr + textBoxCP_Login.Text + splitStr);
                sw.WriteLine("textBoxCP_Password" + splitStr + textBoxCP_Password.Text + splitStr);
                sw.WriteLine("textBoxCP_ServerName" + splitStr + textBoxCP_ServerName.Text + splitStr);
                sw.WriteLine("textBoxCP_SessionASP" + splitStr + textBoxCP_SessionASP.Text + splitStr);
                sw.WriteLine("textBoxCP_SessionASPX" + splitStr + textBoxCP_SessionASPX.Text + splitStr);
                sw.WriteLine("textBoxCPAliase" + splitStr + textBoxCPAliase.Text + splitStr);
                sw.WriteLine("textBoxCPPath" + splitStr + textBoxCPPath.Text + splitStr);
                sw.WriteLine("textBoxDatabase_BackupPath" + splitStr + textBoxDatabase_BackupPath.Text + splitStr);
                sw.WriteLine("textBoxDatabase_DatabaseName" + splitStr + textBoxDatabase_DatabaseName.Text + splitStr);
                sw.WriteLine("textBoxDatabase_Login" + splitStr + textBoxDatabase_Login.Text + splitStr);
                sw.WriteLine("textBoxDatabase_Password" + splitStr + textBoxDatabase_Password.Text + splitStr);
                sw.WriteLine("textBoxDatabase_ServerName" + splitStr + textBoxDatabase_ServerName.Text + splitStr);
                sw.WriteLine("textBoxDeploymentAddress" + splitStr + textBoxDeploymentAddress.Text + splitStr);
                sw.WriteLine("textBoxManagem_Dialer" + splitStr + textBoxManagem_Dialer.Text + splitStr);
                sw.WriteLine("textBoxManagem_MaximizeGroup" + splitStr + textBoxManagem_MaximizeGroup.Text + splitStr);
                sw.WriteLine("textBoxManagem_MaximizeName" + splitStr + textBoxManagem_MaximizeName.Text + splitStr);
                sw.WriteLine("textBoxManagem_MaximizePassw" + splitStr + textBoxManagem_MaximizePassw.Text + splitStr);
                sw.WriteLine("textBoxManagem_MaximizeRecordPassw" + splitStr + textBoxManagem_MaximizeRecordPassw.Text + splitStr);
                sw.WriteLine("textBoxManagem_MaximizeRecordUrl" + splitStr + textBoxManagem_MaximizeRecordUrl.Text + splitStr);
                sw.WriteLine("textBoxManagem_MaximizeUrl" + splitStr + textBoxManagem_MaximizeUrl.Text + splitStr);
                sw.WriteLine("textBoxManagem_ProxyLogin" + splitStr + textBoxManagem_ProxyLogin.Text + splitStr);
                sw.WriteLine("textBoxManagem_ProxyPassw" + splitStr + textBoxManagem_ProxyPassw.Text + splitStr);
                sw.WriteLine("textBoxManagem_ProxyUrl" + splitStr + textBoxManagem_ProxyUrl.Text + splitStr);
                sw.WriteLine("textBoxManagementAliase" + splitStr + textBoxManagementAliase.Text + splitStr);
                sw.WriteLine("textBoxManagementPath" + splitStr + textBoxManagementPath.Text + splitStr);
                sw.WriteLine("textBoxMonitoringAliase" + splitStr + textBoxMonitoringAliase.Text + splitStr);
                sw.WriteLine("textBoxMonitoringPath" + splitStr + textBoxMonitoringPath.Text + splitStr);
                sw.WriteLine("textBoxWSAddress" + splitStr + textBoxWSAddress.Text);
                
                sw.Close();
            }
        }


        /// <summary>
        /// Load button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var sr = new StreamReader(openFileDialog1.FileName, Encoding.GetEncoding("windows-1251"));
                const string splitStr = "^!&^";
                string[] paramsString = sr.ReadToEnd().Replace("\r\n", "").Split(new[] { splitStr }, StringSplitOptions.None);
                sr.Close();               

                for (int i = 0; i < paramsString.Length; i += 2)
                {
                    switch (paramsString[i])
                    {
                        case "textBoxAuthoringAddress":
                            textBoxAuthoringAddress.Text = paramsString[i + 1];
                            break;
                        case "textBoxBEPath":
                            textBoxBEPath.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATI_Authoring":
                            textBoxCATI_Authoring.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATI_LoginLogin":
                            textBoxCATI_LoginLogin.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATI_PasswordLogin":
                            textBoxCATI_PasswordLogin.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATI_SurveyData":
                            textBoxCATI_SurveyData.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATI_UrlLogin":
                            textBoxCATI_UrlLogin.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATIAliase":
                            textBoxCATIAliase.Text = paramsString[i + 1];
                            break;
                        case "textBoxCATIPath":
                            textBoxCATIPath.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_CookieName":
                            textBoxCP_CookieName.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_CPWS":
                            textBoxCP_CPWS.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_ErrorPage":
                            textBoxCP_ErrorPage.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_Login":
                            textBoxCP_Login.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_Password":
                            textBoxCP_Password.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_ServerName":
                            textBoxCP_ServerName.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_SessionASP":
                            textBoxCP_SessionASP.Text = paramsString[i + 1];
                            break;
                        case "textBoxCP_SessionASPX":
                            textBoxCP_SessionASPX.Text = paramsString[i + 1];
                            break;
                        case "textBoxCPAliase":
                            textBoxCPAliase.Text = paramsString[i + 1];
                            break;
                        case "textBoxCPPath":
                            textBoxCPPath.Text = paramsString[i + 1];
                            break;
                        case "textBoxDatabase_BackupPath":
                            textBoxDatabase_BackupPath.Text = paramsString[i + 1];
                            break;
                        case "textBoxDatabase_DatabaseName":
                            textBoxDatabase_DatabaseName.Text = paramsString[i + 1];
                            break;
                        case "textBoxDatabase_Login":
                            textBoxDatabase_Login.Text = paramsString[i + 1];
                            break;
                        case "textBoxDatabase_Password":
                            textBoxDatabase_Password.Text = paramsString[i + 1];
                            break;
                        case "textBoxDatabase_ServerName":
                            textBoxDatabase_ServerName.Text = paramsString[i + 1];
                            break;
                        case "textBoxDeploymentAddress":
                            textBoxDeploymentAddress.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_Dialer":
                            textBoxManagem_Dialer.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_MaximizeGroup":
                            textBoxManagem_MaximizeGroup.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_MaximizeName":
                            textBoxManagem_MaximizeName.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_MaximizePassw":
                            textBoxManagem_MaximizePassw.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_MaximizeRecordPassw":
                            textBoxManagem_MaximizeRecordPassw.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_MaximizeRecordUrl":
                            textBoxManagem_MaximizeRecordUrl.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_MaximizeUrl":
                            textBoxManagem_MaximizeUrl.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_ProxyLogin":
                            textBoxManagem_ProxyLogin.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_ProxyPassw":
                            textBoxManagem_ProxyPassw.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagem_ProxyUrl":
                            textBoxManagem_ProxyUrl.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagementAliase":
                            textBoxManagementAliase.Text = paramsString[i + 1];
                            break;
                        case "textBoxManagementPath":
                            textBoxManagementPath.Text = paramsString[i + 1];
                            break;
                        case "textBoxMonitoringAliase":
                            textBoxMonitoringAliase.Text = paramsString[i + 1];
                            break;
                        case "textBoxMonitoringPath":
                            textBoxMonitoringPath.Text = paramsString[i + 1];
                            break;
                        case "textBoxWSAddress":
                            textBoxWSAddress.Text = paramsString[i + 1];
                            break;
                        default:
                            MessageBox.Show("Неизвестный параметр " + paramsString[i], "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                    }
                }
            }            
        }


        /// <summary>
        /// Change authoring address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxAuthoringAddress_TextChanged(object sender, EventArgs e)
        {
            textBoxCP_ErrorPage.Text = "http://" + textBoxAuthoringAddress.Text + "/confirm/authoring/ErrorPage.aspx";
            textBoxCP_SessionASP.Text = "http://" + textBoxAuthoringAddress.Text + "/confirm/keepsession.asp";
            textBoxCP_SessionASPX.Text = "http://" + textBoxAuthoringAddress.Text + "/confirm/authoring/KeepSession.aspx";
        }


        /// <summary>
        /// Change WS address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxWSAddress_TextChanged(object sender, EventArgs e)
        {
            textBoxCP_CPWS.Text = "http://" + textBoxWSAddress.Text + "/Confirmit/InternalWebServices/14.0/ControlPanel.asmx";
            textBoxCATI_Authoring.Text = "http://" + textBoxWSAddress.Text + "/Confirmit/InternalWebServices/14.0/FusionAuthoring.asmx";
            textBoxCATI_SurveyData.Text = "http://" + textBoxWSAddress.Text + "/confirmit/InternalWebServices/14.0/FusionSurveyData.asmx";
            textBoxCATI_UrlLogin.Text = "http://" + textBoxWSAddress.Text + "/Confirmit/InternalWebServices/14.0/Logon.asmx";
        }
    }
}
