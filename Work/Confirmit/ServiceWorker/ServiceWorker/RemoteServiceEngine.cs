using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace ServiceWorker
{
    public class RemoteServiceEngine
    {
        private ServiceEngine _serviceEngine;      

        public List<string> GetAllStoppedServices(string serverName, List<string> servicesFirstPartDisplayNames)
        {
            var result = new List<string>();
            var allServices = ServiceController.GetServices(serverName);

            foreach (var serviceController in allServices)
            {
                _serviceEngine = new ServiceEngine(serviceController.ServiceName);
                var serviceInfo = _serviceEngine.GetServiceInfo();
                if (serviceInfo.StartType != "Automatically" || serviceController.Status != ServiceControllerStatus.Stopped)
                {
                    continue;
                }

                if (servicesFirstPartDisplayNames.Any(x => serviceController.ServiceName.StartsWith(x)))
                {
                    result.Add(serviceController.ServiceName);
                }
            }

            return result;
        }

        public List<string> GetAllStoppedServices(string serverName, string login, string password, List<string> servicesFirstPartDisplayNames)
        {
            var result = new List<string>();

            ConnectionOptions connectoptions = new ConnectionOptions
            {
                Username = login,
                Password = password
            };

            var scope = new ManagementScope($"\\\\{serverName}\\root\\cimv2")//
            {
                Options = connectoptions
            };
            scope.Connect();

            foreach (var servicesFirstPartDisplayName in servicesFirstPartDisplayNames)
            {
                try
                {
                    SelectQuery query = new SelectQuery($"select * from Win32_Service where name like '{servicesFirstPartDisplayName}%'");

                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                    {
                        ManagementObjectCollection collection = searcher.Get();

                        foreach (ManagementObject service in collection.OfType<ManagementObject>())
                        {
                            if (service["started"].Equals(false) && service["startMode"].Equals("Auto"))
                            {
                                result.Add(service["name"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Add(servicesFirstPartDisplayName + ": " + ex.Message);
                }
            }

            return result;
        }

        public void StartServices(List<string> servicesToStart, Label labelProcessInfo)
        {
            foreach (var serviceName in servicesToStart)
            {
                var serviceController = new ServiceController(serviceName);

                if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    ShowCurrentServiceInfo(Environment.MachineName, serviceName, labelProcessInfo);

                    serviceController.Start();
                }
            }
        }

        private void ShowCurrentServiceInfo(string serverName, string serviceName, Label labelProcessInfo)
        {
            labelProcessInfo.Text = $"Starting service: {serviceName} on server {serverName}";
            Application.DoEvents();
        }

        public List<string> StartServices(Dictionary<string, List<string>> servicesToStart, string login, string password, Label labelProcessInfo)
        {
            var errors = new List<string>();

            ConnectionOptions connectoptions = new ConnectionOptions
            {
                Username = login,
                Password = password
            };

            foreach (var serverName in servicesToStart.Keys)
            {
                try
                {
                    if (serverName.ToLower() == Environment.MachineName.ToLower())
                    {
                        StartServices(servicesToStart[serverName], labelProcessInfo);
                        continue;
                    }

                    var scope = new ManagementScope($"\\\\{serverName}\\root\\cimv2")//
                    {
                        Options = connectoptions
                    };
                    scope.Connect();

                    foreach (var serviceName in servicesToStart[serverName])
                    {
                        try
                        {
                            SelectQuery query = new SelectQuery($"select * from Win32_Service where name = '{serviceName}'");

                            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                            {
                                ManagementObjectCollection collection = searcher.Get();

                                foreach (ManagementObject service in collection.OfType<ManagementObject>())
                                {
                                    if (service["started"].Equals(false))
                                    {
                                        ShowCurrentServiceInfo(serverName, serviceName, labelProcessInfo);
                                        service.InvokeMethod("StartService", null);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add(serverName + ": " + serviceName + ": " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(serverName + ": " + ex.Message);
                }
            }

            return errors;
        }
    }
}