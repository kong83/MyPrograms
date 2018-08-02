using System;
using System.IO;
using System.Threading;
using System.Xml;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Win32;
using Microsoft.Web.Administration;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

using Application = System.Windows.Forms.Application;

namespace DeleteAllWixInstallation
{
    public partial class CheckInstallV16Form : Form
    {
        private const string SplitStr = "^!$&^";

        public CheckInstallV16Form()
        {
            InitializeComponent();
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


        #region Verify configs
        private void CheckXPaths(XmlNode doc, XmlNamespaceManager nsmgr, Dictionary<string, string> changeLines, string configName)
        {
            // Don't check config for this values, if form's fieilds for them are empty
            var dontCheckConfigList = new List<string>();
            dontCheckConfigList.Add("//configuration/applicationSettings/Confirmit.CATI.Core.Properties.Settings/setting[@name='SqlServerDataPath']/value");
            dontCheckConfigList.Add("//configuration/applicationSettings/Confirmit.CATI.Core.Properties.Settings/setting[@name='SqlServerLogPath']/value");

            foreach (string xPathQuery in changeLines.Keys)
            {
                XmlNode node = doc.SelectSingleNode(xPathQuery, nsmgr);
                if (node != null)
                {
                    if (node.InnerText != changeLines[xPathQuery])
                    {
                        AddResultValue("\"" + xPathQuery + "\" has wrong value in " + configName + ". Currect value is " + node.InnerText + ", expected value is " + changeLines[xPathQuery]);
                    }
                }
                else
                {
                    if (dontCheckConfigList.Contains(xPathQuery) & String.IsNullOrEmpty(changeLines[xPathQuery]))
                    {
                        AddResultValue("\"" + xPathQuery + "\" not found in Confirmit.CATI.Backend.exe.config");
                    }
                }
            }
        }

        private void CheckNodeExists(XmlNode doc, XmlNamespaceManager nsmgr, IEnumerable<string> xpathStr, string configName)
        {
            foreach (string xPathQuery in xpathStr)
            {
                XmlNode node = doc.SelectSingleNode(xPathQuery, nsmgr);
                if (node == null)
                {
                    AddResultValue("\"" + xPathQuery + "\" not found in " + configName);
                }
            }
        }

        private void CheckNodeDoNotExist(XmlNode doc, XmlNamespaceManager nsmgr, IEnumerable<string> xpathStr, string configName)
        {
            foreach (string xPathQuery in xpathStr)
            {
                XmlNode node = doc.SelectSingleNode(xPathQuery, nsmgr);
                if (node != null)
                {
                    AddResultValue("\"" + xPathQuery + "\" found in " + configName + " but it shouln't exist");
                }
            }
        }

        /// <summary>
        /// Verify, that congis for Backend and Supervisor were changed correct
        /// </summary>
        private void VerifyConfigs()
        {
            var doc = new XmlDocument();
            XmlNamespaceManager nsmgr;
            Dictionary<string, string> changeLines;
            List<string> xpathStr;

            //
            // Check backend config Confirmit.CATI.Backend.exe.config
            //
            string configName = "Confirmit.CATI.Backend.exe.config";
            string backendConfigPath = Path.Combine(textBoxCatiLocation.Text, configName);
            if (!File.Exists(backendConfigPath))
            {
                AddResultValue("\"" + backendConfigPath + "\" path not found");
            }
            else
            {
                doc.Load(backendConfigPath);
                nsmgr = new XmlNamespaceManager(doc.NameTable);

                changeLines = new Dictionary<string, string>();
                changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='PublishMetadataForExternalWCFServices']/value"] = checkBoxIsNeedPublishForExternalWCFSettings.Checked.ToString();
                changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='PublishMetadataForInternalWCFServices']/value"] = checkBoxIsNeedPublishForInternalWCFSettings.Checked.ToString();
                changeLines["//configuration/appSettings/add[@key='TemplateDBFileName']/@value"] = textBoxBackupPath.Text;
                //changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='SqlServerDataPath']/value"] = textBoxPathToDataFiles.Text;
                changeLines["//configuration/applicationSettings/Confirmit.CATI.Core.Properties.Settings/setting[@name='SqlServerDataPath']/value"] = textBoxPathToDataFiles.Text;
                //changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='SqlServerLogPath']/value"] = textBoxPathToLogFiles.Text;
                changeLines["//configuration/applicationSettings/Confirmit.CATI.Core.Properties.Settings/setting[@name='SqlServerLogPath']/value"] = textBoxPathToLogFiles.Text;
                changeLines["//configuration/appSettings/add[@key='TemplateDBName']/@value"] = textBoxCatiDatabaseName.Text;
                changeLines["//configuration/appSettings/add[@key='AccessAllowedIPAddresses']/@value"] = textBoxValidIPAddressesList.Text;
                changeLines["//configuration/appSettings/add[@key='BvSurveyURL']/@value"] = "http://" + textBoxDeploymentAddress.Text + "/wix/cati_";
                changeLines["//configuration/system.serviceModel/client/endpoint[@name='ConsoleToSurveyEngineEndpoint']/@address"] = "http://" + textBoxWebServiceAddress.Text + "/Confirmit/InternalWebServices/14.0/ConsoleToSurveyEngine.svc";

                if (radioButtonUseSSLAccelerator.Checked)
                {
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='ConsoleServiceBaseAddress']/value"] = "http://localhost:81/MultimodeInstance";
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='MonitoringInterviewerServiceBaseAddress']/value"] = "http://localhost:81/MonitoringInterviewerMultimodeInstance";
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='MonitoringSupervisorServiceBaseAddress']/value"] = "http://localhost:81/MonitoringSupervisorMultimodeInstance";
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='DialerEventsHandlerServiceBaseAddress']/value"] = "http://localhost:81/DialerMultimodeInstance";
                }
                else
                {
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='ConsoleServiceBaseAddress']/value"] = "https://localhost/MultimodeInstance";
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='MonitoringInterviewerServiceBaseAddress']/value"] = "https://localhost/MonitoringInterviewerMultimodeInstance";
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='MonitoringSupervisorServiceBaseAddress']/value"] = "https://localhost/MonitoringSupervisorMultimodeInstance";
                    changeLines["//configuration/applicationSettings/Confirmit.CATI.Backend.Properties.Settings/setting[@name='DialerEventsHandlerServiceBaseAddress']/value"] = "https://localhost/DialerMultimodeInstance";

                }

                CheckXPaths(doc, nsmgr, changeLines, configName);

                xpathStr = new List<string>
                {
                    "//configuration/system.diagnostics/sources/source[@name='System.ServiceModel']/listeners/add[@name='DatabaseTraceListener']",
                    "//configuration/system.diagnostics/trace/listeners/add[@name='DatabaseTraceListener']"
                };

                if (checkBoxIsNeedLoggingToDatabase.Checked)
                {
                    CheckNodeExists(doc, nsmgr, xpathStr, configName);
                }
                else
                {
                    CheckNodeDoNotExist(doc, nsmgr, xpathStr, configName);
                }

                xpathStr = new List<string>
                {
                    "//configuration/system.diagnostics/sources/source[@name='System.ServiceModel']/listeners/add[@name='EventLogTraceListener']",
                    "//configuration/system.diagnostics/trace/listeners/add[@name='EventLogTraceListener']"
                };

                if (checkBoxIsNeedLoggingToEventlog.Checked)
                {
                    CheckNodeExists(doc, nsmgr, xpathStr, configName);
                }
                else
                {
                    CheckNodeDoNotExist(doc, nsmgr, xpathStr, configName);
                }

                xpathStr = new List<string>
                {
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.ConsoleService.ConsoleService']/endpoint[@binding='customBinding']",
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.MonitoringService.ContractImplementation.InterviewerProcessor']/endpoint[@binding='customBinding']",
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.MonitoringService.ContractImplementation.SupervisorProcessor']/endpoint[@binding='customBinding']",
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.DialerEventsHandlerService.DialerEventsHandlerService']/endpoint[@binding='customBinding']"
                };

                if (radioButtonUseSSLAccelerator.Checked)
                {
                    CheckNodeExists(doc, nsmgr, xpathStr, configName);
                }
                else
                {
                    CheckNodeDoNotExist(doc, nsmgr, xpathStr, configName);
                }

                xpathStr = new List<string>
                {
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.ConsoleService.ConsoleService']/endpoint[@binding='wsHttpBinding']",
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.MonitoringService.ContractImplementation.InterviewerProcessor']/endpoint[@binding='wsHttpBinding']",
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.MonitoringService.ContractImplementation.SupervisorProcessor']/endpoint[@binding='wsHttpBinding']",
                    "//configuration/system.serviceModel/services/service[@name='Confirmit.CATI.Backend.WcfServices.External.DialerEventsHandlerService.DialerEventsHandlerService']/endpoint[@binding='wsHttpBinding']"
                };

                if (radioButtonUseSSLAccelerator.Checked)
                {
                    CheckNodeDoNotExist(doc, nsmgr, xpathStr, configName);
                }
                else
                {
                    CheckNodeExists(doc, nsmgr, xpathStr, configName);
                }
            }


            //
            // Check supervisor config Web.config
            //
            configName = "Web.config";
            string supervisorConfigPath = Path.Combine(textBoxCatiLocation.Text, "Supervisor\\" + configName);
            if (!File.Exists(supervisorConfigPath))
            {
                AddResultValue("\"" + supervisorConfigPath + "\" path not found");
                return;
            }

            doc = new XmlDocument();
            doc.Load(supervisorConfigPath);
            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("prefix", "http://schemas.microsoft.com/.NetConfiguration/v2.0");

            changeLines = new Dictionary<string, string>();

            //changeLines["//prefix:configuration/prefix:appSettings/prefix:add[@key='ConfirmitKeepSessionAspUrl']/@value"] = textBoxSessionASPUrl.Text;
            changeLines["//prefix:configuration/prefix:appSettings/prefix:add[@key='ConfirmitKeepSessionAspxUrl']/@value"] = textBoxSessionASPXUrl.Text;
            if (radioButtonInprocMode.Checked)
            {
                changeLines["//prefix:configuration/prefix:system.web/prefix:sessionState/@mode"] = "InProc";
            }
            else
            {
                changeLines["//prefix:configuration/prefix:system.web/prefix:sessionState/@mode"] = "SQLServer";
                changeLines["//prefix:configuration/prefix:system.web/prefix:sessionState/@cookieName"] = textBoxSessionStateCookieName.Text;
                changeLines["//prefix:configuration/prefix:system.web/prefix:sessionState/@sqlConnectionString"] = string.Format("data source={0};User ID={1};Password={2}", textBoxSessionStateServerName.Text, textBoxSessionStateLogin.Text, textBoxSessionStatePassword.Text);
            }

            CheckXPaths(doc, nsmgr, changeLines, configName);

            xpathStr = new List<string>
            {                    
                "//prefix:configuration/prefix:system.diagnostics/prefix:trace/prefix:listeners/prefix:add[@name=\"DatabaseTraceListener\"]"
            };

            if (checkBoxIsNeedLoggingToDatabase.Checked)
            {
                CheckNodeExists(doc, nsmgr, xpathStr, configName);
            }
            else
            {
                CheckNodeDoNotExist(doc, nsmgr, xpathStr, configName);
            }

            xpathStr = new List<string>
            {                    
                "//prefix:configuration/prefix:system.diagnostics/prefix:trace/prefix:listeners/prefix:add[@name=\"EventLogTraceListener\"]"
            };

            if (checkBoxIsNeedLoggingToEventlog.Checked)
            {
                CheckNodeExists(doc, nsmgr, xpathStr, configName);
            }
            else
            {
                CheckNodeDoNotExist(doc, nsmgr, xpathStr, configName);
            }
        }
        #endregion


        #region Verify registry
        private void CheckRegistryKey(RegistryKey regKey, string parameterName, string expectedValue)
        {
            var s = (string)regKey.GetValue(parameterName, "");

            if (s != expectedValue)
            {
                AddResultValue("\"" + parameterName + "\" parameter is wrong. Currect value is " + s + ". Expected is " + expectedValue);
            }
        }

        /// <summary>
        /// Verify, that registry was changed correct
        /// </summary>
        private void VerifyRegistry()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\Confirmit\CATI");

            if (regKey == null)
            {
                AddResultValue(@"The registry key 'HKLM\Software\Confirmit\CATI' didn't find");
            }
            else
            {
                string catiConnectionString = "server=" + textBoxCatiServerName.Text + ";database=" + textBoxCatiDatabaseName.Text + ";uid=" + textBoxCatiLoginForWorkWithDatabase.Text + ";password=" + textBoxCatiPasswordForWorkWithDatabase.Text;
                string confirmlogConnectionString = "server=" + textBoxConfirmlogServerName.Text + ";database=Confirmlog;uid=" + textBoxConfirmlogLogin.Text + ";password=" + textBoxConfirmlogPassword.Text;

                CheckRegistryKey(regKey, "DB Connection String", catiConnectionString);
                CheckRegistryKey(regKey, "Confirmlog Connection String", confirmlogConnectionString);
                CheckRegistryKey(regKey, "Path", textBoxCatiLocation.Text);
                CheckRegistryKey(regKey, "Confirmit.AuthoringWS.WebServiceUrl", textBoxAuthoringUrl.Text);
                CheckRegistryKey(regKey, "Confirmit.SurveyDataWS.WebServiceUrl", textBoxSurveyDataUrl.Text);

                regKey = Registry.LocalMachine.OpenSubKey(@"Software\Confirmit\CATI\InstallationPaths");
            }

            if (regKey == null)
            {
                AddResultValue(@"The registry key 'HKLM\Software\Confirmit\CATI\InstallationPaths' didn't find");
                return;
            }

            CheckRegistryKey(regKey, "CONTROLPANELWEBSERVICEALIAS", textBoxSupervisorAliase.Text);
            CheckRegistryKey(regKey, "POOLNAME", textBoxAppPoolName.Text);
            CheckRegistryKey(regKey, "SITEID", GetSiteId(textBoxSupervisorSiteName.Text));
        }
        #endregion


        #region Verify files
        private void CheckFileAvailability(IEnumerable<string> filesList, IEnumerable<FileInfo> files, string folderPath)
        {
            foreach (string str in filesList)
            {
                string s = str;
                if (!files.Where(x => x.Name.ToLower() == s.ToLower()).Any())
                {
                    AddResultValue("File \"" + Path.Combine(folderPath, str) + "\" doesn't find");
                }
            }
        }

        private void CheckFileVersions(string path, IEnumerable<string> files)
        {
            // Don't check version for 3rdpart files
            var dontCheckVersionFilesList = new List<string>();
            dontCheckVersionFilesList.Add("STRUCTUREMAP.DLL");

            foreach (string str in files)
            {
                var fi = new FileInfo(Path.Combine(path, str));
                if (fi.Exists && (fi.Extension.ToLower() == ".dll" || fi.Extension.ToLower() == ".exe"))
                {
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(path, str));
                    if (fileVersionInfo.FileVersion != textBoxVersion.Text & !dontCheckVersionFilesList.Contains(str.ToUpper()))
                    {
                        AddResultValue("File \"" + Path.Combine(path, str) + "\" has wrong version: " + fileVersionInfo.FileVersion);
                    }
                }
            }
        }

        /// <summary>
        /// Verify, that files were changed correct
        /// </summary>
        private void VerifyFiles()
        {
            var di = new DirectoryInfo(textBoxCatiLocation.Text);
            if (!di.Exists)
            {
                AddResultValue("Path \"" + textBoxCatiLocation.Text + "\" isn't find");
                return;
            }
            FileInfo[] files = di.GetFiles();

            var backendFilesList = new[]
                {
                    "BvTciDialerService.ServiceLibrary.Contract.DLL",
                    "BvTciLibrary.DLL",
                    "Confirmit.CATI.Backend.exe",
                    "Confirmit.CATI.Backend.exe.config",
                    "Confirmit.CATI.Common.DLL",
                    "Confirmit.CATI.Console.Common.dll",
                    "Confirmit.CATI.Core.dll",
                    "Confirmit.CATI.Management.exe",
                    "Confirmit.CATI.Monitoring.Common.dll",
                    "Confirmit.CATI.Telephony.PROTSDialerService.Contract.dll",
                    "DialerCommon.DLL",
                    "DialerConfig.xml",
                    "DialerConfigurationUtility.exe",
                    "MnTciLibrary.DLL",
                    "PROTSLibrary.dll",
                    "StructureMap.dll"
                };

            CheckFileAvailability(backendFilesList, files, textBoxCatiLocation.Text);

            CheckFileVersions(textBoxCatiLocation.Text, backendFilesList);

            string supervisorLocation = Path.Combine(textBoxCatiLocation.Text, @"Supervisor\bin");
            di = new DirectoryInfo(supervisorLocation);
            if (!di.Exists)
            {
                AddResultValue("Path \"" + supervisorLocation + "\" isn't find");
                return;
            }
            files = di.GetFiles();

            var supervisorFilesList = new[]
                {
                    "Confirmit.CATI.Common.dll",
                    "Confirmit.CATI.Core.dll",
                    //"Confirmit.CATI.Supervisor.Backend.dll",
                    "Confirmit.CATI.Supervisor.Core.dll",
                    "Confirmit.CATI.Supervisor.dll",
                    "Confirmit.CATI.Supervisor.Resources.dll",
                    "DialerCommon.dll",
                    "StructureMap.dll"
                };

            CheckFileAvailability(supervisorFilesList, files, supervisorLocation);

            CheckFileVersions(supervisorLocation, supervisorFilesList);
        }
        #endregion


        #region Verify certificates and configuration of http listener
        /// <summary>
        /// Run external script
        /// </summary>
        /// <param name="scriptPath">Path of sctipt</param>
        /// <param name="args">Arguments</param>
        /// <param name="delay">Max delay (millisecond)</param>
        /// <returns></returns>
        public string InvokeExternalScript(string scriptPath, string args, int delay)
        {
            using (var scriptProcess = new Process())
            {
                var pinfo = new ProcessStartInfo(scriptPath, args)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                scriptProcess.StartInfo = pinfo;

                scriptProcess.Start();

                string outputString = scriptProcess.StandardOutput.ReadToEnd();
                string errorString = scriptProcess.StandardError.ReadToEnd();

                if (delay > 0)
                {
                    scriptProcess.WaitForExit(delay);
                }
                else
                {
                    scriptProcess.WaitForExit();
                }

                if (scriptProcess.ExitCode != 0)
                {
                    throw new Exception("Error executing " + scriptPath + " with parameters:\n" + args + ". Error message:\r\n" + errorString);
                }

                return outputString;
            }
        }


        /// <summary>
        /// Verify, that certificate was installed and HttpListener was configured correct
        /// </summary>
        private void VerifyCertificateAndConfigurationOfHttpListener()
        {
            if (radioButtonUseSSLAccelerator.Checked)
            {
                return;
            }

            string thumbprint = "";
            if (radioButtonUseTestCertificate.Checked)
            {
                const string testRootCertName = "Confirmit CATI Root Test Certificate";

                var storeMy = (StoreName)Enum.Parse(typeof(StoreName), "My", true);
                var storeRoot = (StoreName)Enum.Parse(typeof(StoreName), "Root", true);
                var storeLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), "LocalMachine", true);

                var store = new X509Store(storeMy, storeLocation);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection cers = store.Certificates;
                cers = cers.Find(X509FindType.FindByIssuerName, testRootCertName, false);
                if (cers.Count == 0)
                {
                    AddResultValue("Test certificate doesn't find in My/LocalMachine store");
                }
                else
                {
                    thumbprint = cers[0].Thumbprint;
                }
                store.Close();

                store = new X509Store(storeRoot, storeLocation);
                store.Open(OpenFlags.ReadOnly);
                cers = store.Certificates;
                cers = cers.Find(X509FindType.FindByIssuerName, testRootCertName, false);
                cers = cers.Find(X509FindType.FindBySubjectDistinguishedName, "CN=" + testRootCertName, false);
                if (cers.Count == 0)
                {
                    AddResultValue("Test certificate doesn't find in Root/LocalMachine store");
                }                
                store.Close();

                string certPath = Path.Combine(textBoxCatiLocation.Text, @"TestCertificates\" + textBoxTestCertificateName.Text + ".cer");
                if (!File.Exists(certPath))
                {
                    AddResultValue("Test certificate file \"" + certPath + "\" doesn't find");
                }
            }
            else
            {
                thumbprint = textBoxRealCertificateThumbprint.Text;
            }

            string sslCertsStr = InvokeExternalScript("netsh.exe", "http show sslcert", 3000);

            if (!sslCertsStr.Contains("0.0.0.0:443"))
            {
                AddResultValue("IP:port=0.0.0.0:443 doesn't find. SslCertInfo=" + sslCertsStr);
            }            
            
            if (!string.IsNullOrEmpty(thumbprint))
            {
                if (!sslCertsStr.Contains(thumbprint.ToLower()))
                {
                    AddResultValue("Certificate Hash=" + thumbprint + " doesn't find. SslCertInfo=" + sslCertsStr);
                }
            }
            else
            {
                AddResultValue("Thumbprint isn't defined");
            }

            if (!sslCertsStr.Contains("{df179e9a-676b-4153-aa92-e589568c7d41}"))
            {
                AddResultValue("Application ID={df179e9a-676b-4153-aa92-e589568c7d41} doesn't find. SslCertInfo=" + sslCertsStr);
            }
        }
        #endregion


        #region Verify application pools and virtual directory
        /// <summary>
        /// Verify, that application pool and virtaul directory were created correct 
        /// </summary>
        private void VerifyAppPools()
        {
            using (var serverManager = new ServerManager())
            {
                if (!serverManager.ApplicationPools.Where(x => x.Name == textBoxAppPoolName.Text).Any())
                {
                    AddResultValue("Application Pool \"" + textBoxAppPoolName.Text + "\" doesn't find");
                }
                else
                {
                    if (serverManager.ApplicationPools[textBoxAppPoolName.Text].Enable32BitAppOnWin64)
                    {
                        AddResultValue("Enable32BitAppOnWin64 for application pool \"" + textBoxAppPoolName.Text + "\" is true, but should be false");
                    }
                }

                if (serverManager.ApplicationPoolDefaults.Enable32BitAppOnWin64)
                {
                    AddResultValue("Enable32BitAppOnWin64 for default application pool is true, but should be false");
                }

                bool isFindAliase = false;
                foreach (Microsoft.Web.Administration.Application applic in serverManager.Sites[0].Applications)
                {
                    if (applic.Path.Contains(textBoxSupervisorAliase.Text))
                    {
                        isFindAliase = true;
                        if (applic.ApplicationPoolName != textBoxAppPoolName.Text)
                        {
                            AddResultValue("Virtual directory \"" + textBoxSupervisorAliase.Text + "\" has wrong application pool. It is " + applic.VirtualDirectories[0].Path + ". It should be " + textBoxAppPoolName.Text);
                        }

                        if (applic.VirtualDirectories[0].PhysicalPath != Path.Combine(textBoxCatiLocation.Text, "Supervisor"))
                        {
                            AddResultValue("Physical path of virtual directory \"" + textBoxSupervisorAliase.Text + "\" is wrong. It is " + applic.VirtualDirectories[0].PhysicalPath + ". It should be " + Path.Combine(textBoxCatiLocation.Text, "Supervisor"));
                        }
                    }
                }
                if (!isFindAliase)
                {
                    AddResultValue("Virtual directory \"" + textBoxSupervisorAliase.Text + "\" doesn't find");
                }
            }
        }
        #endregion


        #region Verify databases
        public static bool IsDatabaseExist(
            string server, string database,
            string login, string password)
        {
            if (database == "")
            {
                return false;
            }

            var myServer = new Server(server);
            ServerConnection conn = myServer.ConnectionContext;

            conn.LoginSecure = false;
            conn.Login = login;
            conn.Password = password;

            conn.Connect();

            return myServer.Databases[database] != null;
        }

        /// <summary>
        /// Verify, that database was created correct 
        /// </summary>
        private void VerifyDatabase()
        {
            if (!IsDatabaseExist(textBoxCatiServerName.Text, textBoxCatiDatabaseName.Text, textBoxCatiLoginForWorkWithDatabase.Text, textBoxCatiPasswordForWorkWithDatabase.Text))
            {
                AddResultValue("\"" + textBoxCatiDatabaseName.Text + "\" database doesn't find");
            }

            if (!File.Exists(textBoxBackupPath.Text))
            {
                AddResultValue("\"" + textBoxBackupPath.Text + "\" database backup file doesn't find");
            }

            if (!string.IsNullOrEmpty(textBoxPathToDataFiles.Text))
            {
                if (!Directory.Exists(textBoxPathToDataFiles.Text))
                {
                    AddResultValue("\"" + textBoxPathToDataFiles.Text + "\" directory doesn't exist");
                }
                else if (!Directory.GetFiles(textBoxPathToDataFiles.Text).Any())
                {
                    AddResultValue("\"" + textBoxPathToDataFiles.Text + "\" directory empty. It should contains mdf file for default database");
                }

                if (!Directory.Exists(textBoxPathToLogFiles.Text))
                {
                    AddResultValue("\"" + textBoxPathToLogFiles.Text + "\" directory doesn't exist");
                }
                else if (!Directory.GetFiles(textBoxPathToLogFiles.Text).Any())
                {
                    AddResultValue("\"" + textBoxPathToLogFiles.Text + "\" directory empty. It should contains ldf file for default database");
                }
            }
        }
        #endregion


        #region Verify others
        private void VerifyOthers()
        {
            /*
            const string performanceName = "CATI Confirmit";

            if (!PerformanceCounterCategory.Exists(performanceName))
            {
                AddResultValue("Performance category " + performanceName + " doesn't find.");
            }
            else if (!PerformanceCounterCategory.CounterExists("Time between calls", performanceName))
            {
                AddResultValue("Performance counter \"Time between calls\" doesn't find.");
            }
            */

            const string logName = "CATI Confirmit";
            const string sourseName = "Backend Default Instance";

            if (!EventLog.SourceExists(sourseName))
            {
                AddResultValue("Event log " + sourseName + " doesn't find.");
            }
            else
            {
                var curLog = new EventLog(logName, Environment.MachineName, sourseName);
                if (curLog.MaximumKilobytes != 128 * 1024)
                {
                    AddResultValue("Event log max size is" + curLog.MaximumKilobytes + ", but it should be" + (128 * 1024));
                }
                
                if (curLog.OverflowAction != OverflowAction.OverwriteAsNeeded)
                {
                    AddResultValue("Event log owerflow action is" + curLog.OverflowAction + ", but it should be OverwriteAsNeeded");
                }
            }
        }

        #endregion


        /// <summary>
        /// Check installation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheck_Click(object sender, EventArgs e)
        {
            buttonCheck.Enabled = false;
            listBoxResult.Items.Clear();
            Application.DoEvents();
            Thread.Sleep(200);

            #region Call verification functions
            try
            {
                VerifyConfigs();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }

            try
            {
                VerifyRegistry();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }

            try
            {
                VerifyFiles();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }

            try
            {
                if (radioButtonDoNotUseSLLAccelerator.Checked)
                {
                    VerifyCertificateAndConfigurationOfHttpListener();
                }
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }

            try
            {
                VerifyAppPools();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }

            try
            {
                VerifyDatabase();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }

            try
            {
                VerifyOthers();
            }
            catch (Exception ex)
            {
                AddResultValue(ex.ToString());
            }
            #endregion


            if (listBoxResult.Items.Count == 0)
            {
                AddResultValue("All correct");
            }
            else
            {
                AddResultValue("Total: " + listBoxResult.Items.Count + " errors");
            }

            buttonCheck.Enabled = true;
        }

        /// <summary>
        /// Save all parameters to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //
            // Save in format: 
            // <groupBox.Name>^!$&^<control type>^!$&^<control name>^!$&^<control value 
            // (text for TextBox, checked for CheckBox and RadioButton)>
            //
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding("windows-1251")))
                {
                    foreach (Control controlGroupBoxOrPanel in Controls)
                    {
                        if (controlGroupBoxOrPanel is GroupBox || controlGroupBoxOrPanel is Panel)
                        {
                            foreach (Control control in controlGroupBoxOrPanel.Controls)
                            {
                                if (control is TextBox)
                                {
                                    sw.WriteLine(controlGroupBoxOrPanel.Name + SplitStr + "TextBox" + SplitStr + control.Name + SplitStr + control.Text);
                                }
                                else if (control is CheckBox)
                                {
                                    sw.WriteLine(controlGroupBoxOrPanel.Name + SplitStr + "CheckBox" + SplitStr + control.Name + SplitStr + ((CheckBox)control).Checked);
                                }
                                else if (control is RadioButton)
                                {
                                    sw.WriteLine(controlGroupBoxOrPanel.Name + SplitStr + "RadioButton" + SplitStr + control.Name + SplitStr + ((RadioButton)control).Checked);
                                }
                            }
                        }
                    }

                    sw.Close();
                }
                AddResultValue("Data save successful");
            }
        }

        /// <summary>
        /// Load all parameters from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBoxResult.Items.Clear();
                string[] paramsString;
                using (var sr = new StreamReader(openFileDialog1.FileName, Encoding.GetEncoding("windows-1251")))
                {
                    paramsString = sr.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    sr.Close();
                }

                foreach (string paramString in paramsString)
                {
                    string[] splitParams = paramString.Split(new[] { SplitStr }, StringSplitOptions.None);

                    if (splitParams.Length != 4)
                    {
                        AddResultValue("Wrong file with parameters");
                        return;
                    }

                    try
                    {
                        if (splitParams[1] == "TextBox")
                        {
                            Controls[splitParams[0]].Controls[splitParams[2]].Text = splitParams[3];
                        }
                        else if (splitParams[1] == "CheckBox")
                        {
                            ((CheckBox)Controls[splitParams[0]].Controls[splitParams[2]]).Checked = Convert.ToBoolean(splitParams[3]);
                        }
                        else
                        {
                            ((RadioButton)Controls[splitParams[0]].Controls[splitParams[2]]).Checked = Convert.ToBoolean(splitParams[3]);
                        }
                    }
                    catch
                    {
                        AddResultValue("Wrong parameters line " + paramString);
                        continue;
                    }
                }
                AddResultValue("Data load successful");
            }
        }


        /// <summary>
        /// Copy results to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (string str in listBoxResult.Items)
            {
                sb.Append(str + "\r\n");
            }
            Clipboard.SetText(sb.ToString());
        }

        private void radioButtonSQLServerMode_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSessionStateServerName.Enabled = textBoxSessionStateLogin.Enabled =
            textBoxSessionStatePassword.Enabled = textBoxSessionStateCookieName.Enabled =
            label14.Enabled = label15.Enabled = label16.Enabled = label17.Enabled =
            radioButtonSQLServerMode.Checked;
        }

        private void radioButtonDoNotUseSLLAccelerator_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonUseTestCertificate.Enabled = radioButtonUseRealCertificate.Enabled = radioButtonDoNotUseSLLAccelerator.Checked;
            radioButtonUseTestCertificate_CheckedChanged(null, null);
        }

        private void radioButtonUseTestCertificate_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTestCertificateName.Enabled = label8.Enabled = radioButtonUseTestCertificate.Checked && radioButtonUseTestCertificate.Enabled;
            textBoxRealCertificateThumbprint.Enabled = label7.Enabled = !radioButtonUseTestCertificate.Checked && radioButtonUseTestCertificate.Enabled;
        }

        private void textBoxWebServiceAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAuthoringUrl.Text.StartsWith("http://") && textBoxAuthoringUrl.Text.EndsWith("/Confirmit/InternalWebServices/14.0/FusionAuthoring.asmx"))
            {
                textBoxAuthoringUrl.Text = "http://" + textBoxWebServiceAddress.Text + "/Confirmit/InternalWebServices/14.0/FusionAuthoring.asmx";
            }
            if (textBoxSurveyDataUrl.Text.StartsWith("http://") && textBoxSurveyDataUrl.Text.EndsWith("/confirmit/InternalWebServices/14.0/FusionSurveyData.asmx"))
            {
                textBoxSurveyDataUrl.Text = "http://" + textBoxWebServiceAddress.Text + "/confirmit/InternalWebServices/14.0/FusionSurveyData.asmx";
            }
        }

        private void textBoxAuthoringAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSessionASPUrl.Text.StartsWith("http://") && textBoxSessionASPUrl.Text.EndsWith("/confirm/keepsession.asp"))
            {
                textBoxSessionASPUrl.Text = "http://" + textBoxAuthoringAddress.Text + "/confirm/keepsession.asp";
            }
            if (textBoxSessionASPXUrl.Text.StartsWith("http://") && textBoxSessionASPXUrl.Text.EndsWith("/confirm/authoring/KeepSession.aspx"))
            {
                textBoxSessionASPXUrl.Text = "http://" + textBoxAuthoringAddress.Text + "/confirm/authoring/KeepSession.aspx";
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private string GetSiteId(string siteName)
        {
            using (var serverManager = new ServerManager())
            {
                foreach (var site in serverManager.Sites)
                {
                    if (site.Name == siteName)
                    {
                        return site.Id.ToString();
                    }
                }
            }

            AddResultValue("Site with name " + siteName + "not found.");
            return "";
        }
    }
}
