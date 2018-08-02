using System;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ServiceWorker
{
    public struct ServiceInfo
    {
        public string ServiceType;
        public string StartType;
        public string ErrorControl;
        public string BinaryPathName;
        public string LoadOrderGroup;
        public int TagID;
        public string Dependencies;
        public string StartName;
        public string DisplayName;
    }

    public enum ServiceAccess
    { 
        Full,
        Read
    }

    class ServiceEngine : IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct QueryServiceConfigStruct
        {
            public int ServiceType;
            public int StartType;
            public int ErrorControl;
            public IntPtr BinaryPathName;
            public IntPtr LoadOrderGroup;
            public int TagID;
            public IntPtr Dependencies;
            public IntPtr StartName;
            public IntPtr DisplayName;
        }

        // ReSharper disable InconsistentNaming
        #region SERVICE_ACCESS
        [Flags]
        private enum SERVICE_ACCESS : uint
        {
            STANDARD_RIGHTS_REQUIRED = 0xF0000,
            SERVICE_QUERY_CONFIG = 0x00001,
            SERVICE_CHANGE_CONFIG = 0x00002,
            SERVICE_QUERY_STATUS = 0x00004,
            SERVICE_ENUMERATE_DEPENDENTS = 0x00008,
            SERVICE_START = 0x00010,
            SERVICE_STOP = 0x00020,
            SERVICE_PAUSE_CONTINUE = 0x00040,
            SERVICE_INTERROGATE = 0x00080,
            SERVICE_USER_DEFINED_CONTROL = 0x00100,
            SERVICE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED |
                SERVICE_QUERY_CONFIG |
                SERVICE_CHANGE_CONFIG |
                SERVICE_QUERY_STATUS |
                SERVICE_ENUMERATE_DEPENDENTS |
                SERVICE_START |
                SERVICE_STOP |
                SERVICE_PAUSE_CONTINUE |
                SERVICE_INTERROGATE |
                SERVICE_USER_DEFINED_CONTROL)
        }
        #endregion

        #region SCM_ACCESS
        [Flags]
        private enum SCM_ACCESS : uint
        {
            STANDARD_RIGHTS_REQUIRED = 0xF0000,
            SC_MANAGER_CONNECT = 0x00001,
            SC_MANAGER_CREATE_SERVICE = 0x00002,
            SC_MANAGER_ENUMERATE_SERVICE = 0x00004,
            SC_MANAGER_LOCK = 0x00008,
            SC_MANAGER_QUERY_LOCK_STATUS = 0x00010,
            SC_MANAGER_MODIFY_BOOT_CONFIG = 0x00020,
            SC_MANAGER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED |
                SC_MANAGER_CONNECT |
                SC_MANAGER_CREATE_SERVICE |
                SC_MANAGER_ENUMERATE_SERVICE |
                SC_MANAGER_LOCK |
                SC_MANAGER_QUERY_LOCK_STATUS |
                SC_MANAGER_MODIFY_BOOT_CONFIG
        }
        #endregion

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeWaitHandle OpenSCManager(string machineName, string databaseName, SCM_ACCESS dwDesiredAccess);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern SafeWaitHandle OpenService(SafeWaitHandle hSCManager, string lpServiceName, SERVICE_ACCESS dwDesiredAccess);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern Boolean ChangeServiceConfig(
            SafeWaitHandle hService, UInt32 nServiceType, UInt32 nStartType, UInt32 nErrorControl,
            String lpBinaryPathName, String lpLoadOrderGroup, IntPtr lpdwTagId,
            String lpDependencies, String lpServiceStartName, String lpPassword, String lpDisplayName);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool DeleteService(SafeWaitHandle hService);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeWaitHandle CreateService(
            SafeWaitHandle hSCManager,
            string lpServiceName,
            string lpDisplayName,
            SERVICE_ACCESS dwDesiredAccess,
            Int32 dwServiceType,
            Int32 dwStartType,
            Int32 dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            Int32 lpdwTagId,
            string lpDependencies,
            string lpServiceStartName,
            string lpPassword);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int QueryServiceConfig(
            SafeWaitHandle service,
            SafeWaitHandle queryServiceConfig,
            int bufferSize,
            ref int bytesNeeded);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CloseServiceHandle(SafeWaitHandle hSCObject);

        private const uint SERVICE_NO_CHANGE = 0xffffffff;
        private const uint SERVICE_DEMAND_START = 0x00000003;
        private const uint SERVICE_DISABLED = 0x00000004;
        private const Int32 SERVICE_WIN32_OWN_PROCESS = 0x00000010;
        private const Int32 SERVICE_AUTO_START = 0x00000002;
        private const Int32 SERVICE_ERROR_NORMAL = 0x00000001;        
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Selected service
        /// </summary>
        private readonly ServiceController _service;

        /// <summary>
        /// Handle to service manager
        /// </summary>
        private readonly SafeWaitHandle _schSCManager;

        /// <summary>
        /// Handle to service
        /// </summary>
        private readonly SafeWaitHandle _schService;

        /// <summary>
        /// true - if this service marked for delete
        /// </summary>
        public readonly bool IsServiceMarkedForDelete;        

        /// <summary>
        /// Delay for waiting (sec)
        /// </summary>
        private const int Delay = 30;

        public ServiceEngine(string serviceName)
            : this(serviceName, ServiceAccess.Read)
        {

        }

        /// <summary>
        /// Class for work with service
        /// </summary>        
        /// <param name="serviceName">Selected service</param>
        /// <param name="serviceAccess">Access to service</param>
        public ServiceEngine(string serviceName, ServiceAccess serviceAccess)
        {
            _service = new ServiceController(serviceName, Environment.MachineName);

            _schSCManager = OpenSCManager(null, null, SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
            if (_schSCManager.IsInvalid)
            {
                throw new Exception(string.Format("OpenSCManager failed {0}", Marshal.GetLastWin32Error()));
            }

            SERVICE_ACCESS sA = SERVICE_ACCESS.SERVICE_ALL_ACCESS;
            if (serviceAccess == ServiceAccess.Read)
                sA = SERVICE_ACCESS.SERVICE_QUERY_CONFIG;
            _schService = OpenService(_schSCManager, serviceName, sA);
            if (_schService.IsInvalid)
            {
                throw new Exception(string.Format("OpenService failed {0}", Marshal.GetLastWin32Error()));
            }

            IsServiceMarkedForDelete = false;
            if (!ChangeServiceConfig(_schService, SERVICE_NO_CHANGE, SERVICE_NO_CHANGE, SERVICE_NO_CHANGE, null, null, IntPtr.Zero, null, null, null, null))
            {
                if (Marshal.GetLastWin32Error() == 1072)
                {
                    IsServiceMarkedForDelete = true;
                }
            }
        }

        private bool m_Disposed;

        private void Dispose(bool disposing)
        {
            if (m_Disposed)
            {
                return;
            }

            if (disposing)
            {
                _service.Dispose();
            }
            CloseServiceHandle(_schSCManager);
            CloseServiceHandle(_schService);

            m_Disposed = true;
        }


        /// <summary>
        /// Dispose this object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServiceEngine()
        {
            Dispose(false);
        }

        /// <summary>
        /// Set service startup type (disabled, manual and etc.)
        /// </summary>
        /// <param name="type">Startup type</param>
        private void ChangeServiceStartup(uint type)
        {
            if (IsServiceMarkedForDelete)
            {
                throw new Exception("A service has been marked for deletion. You need to reboot the computer.");
            }

            if (!ChangeServiceConfig(_schService, SERVICE_NO_CHANGE, type, SERVICE_NO_CHANGE, null, null, IntPtr.Zero, null, null, null, null))
            {
                throw new Exception(string.Format("ChangeServiceConfig failed {0}", Marshal.GetLastWin32Error()));
            }
            _service.Refresh();
        }


        /// <summary>
        /// Enable service
        /// </summary>      
        public void EnableService()
        {
            ChangeServiceStartup(SERVICE_DEMAND_START);
        }


        /// <summary>
        /// Disable service
        /// </summary>
        public void DisableService()
        {
            ChangeServiceStartup(SERVICE_DISABLED);
        }


        /// <summary>
        /// Start service
        /// </summary>
        public void StartService()
        {
            if (IsServiceMarkedForDelete)
            {
                throw new Exception("A service has been marked for deletion. You need to reboot the computer.");
            }

            if (_service.Status != ServiceControllerStatus.Running)
            {
                _service.Start();
                do
                {
                    try
                    {
                        _service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, Delay));
                    }
                    catch (System.ServiceProcess.TimeoutException)
                    {
                        if (MessageBox.Show(_service.DisplayName + " service didn't start in requested timeout.\r\nDo you want to wait for this service to start?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }
                    }
                    _service.Refresh();
                }
                while (_service.Status != ServiceControllerStatus.Running);
            }
        }


        /// <summary>
        /// Stop service
        /// </summary>
        public void StopService()
        {
            if (IsServiceMarkedForDelete)
            {
                throw new Exception("A service has been marked for deletion. You need to reboot the computer.");
            }

            if (_service.Status != ServiceControllerStatus.Stopped)
            {
                _service.Stop();
                do
                {
                    try
                    {
                        _service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, Delay));
                    }
                    catch (System.ServiceProcess.TimeoutException)
                    {
                        if (MessageBox.Show(_service.DisplayName + " service didn't stop in requested timeout.\r\nDo you want to wait for this service to stop?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }
                    }
                    _service.Refresh();
                }
                while (_service.Status != ServiceControllerStatus.Stopped);
            }
        }


        /// <summary>
        /// Pause service
        /// </summary>
        public void PauseService()
        {
            if (IsServiceMarkedForDelete)
            {
                throw new Exception("A service has been marked for deletion. You need to reboot the computer.");
            }

            if (_service.Status != ServiceControllerStatus.Paused)
            {
                _service.Pause();
                do
                {
                    try
                    {
                        _service.WaitForStatus(ServiceControllerStatus.Paused, new TimeSpan(0, 0, Delay));
                    }
                    catch (System.ServiceProcess.TimeoutException)
                    {
                        if (MessageBox.Show(_service.DisplayName + " service didn't pause in requested timeout.\r\nDo you want to wait for this service to pause?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }
                    }
                    _service.Refresh();
                }
                while (_service.Status != ServiceControllerStatus.Stopped);
            }
        }


        /// <summary>
        /// Remove service
        /// </summary>
        public void RemoveService()
        {
            if (IsServiceMarkedForDelete)
            {
                throw new Exception("A service has been marked for deletion. You need to reboot the computer.");
            }

            DisableService();
            StopService();

            //
            // Delete service
            //
            if (!DeleteService(_schService))
            {
                throw new Exception(string.Format("DeleteService failed {0}", Marshal.GetLastWin32Error()));
            }
        }


        /// <summary>
        /// Create service
        /// </summary>
        /// <param name="name">Service name</param>
        /// <param name="displayName">Display name</param>
        /// <param name="pathToBinary">Path to file</param>
        /// <param name="commandLine">Command line</param>
        public static void CreateService(
            string name,
            string displayName,
            string pathToBinary,
            string commandLine)
        {
            SafeWaitHandle schSCManager = OpenSCManager(null, null, SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
            if (schSCManager.IsInvalid)
            {
                throw new Exception(string.Format("OpenSCManager failed {0}", Marshal.GetLastWin32Error()));
            }

            SafeWaitHandle schService = null;

            try
            {
                schService = CreateService(
                    schSCManager,
                    name,
                    displayName,
                    SERVICE_ACCESS.SERVICE_ALL_ACCESS,
                    SERVICE_WIN32_OWN_PROCESS,
                    SERVICE_AUTO_START,
                    SERVICE_ERROR_NORMAL,
                    pathToBinary + " " + commandLine,
                    null,
                    0,
                    null,
                    null,
                    null);

                if (schService.IsInvalid)
                {
                    throw new Exception(
                        String.Format(
                            "Cannot create service {0}. Error {1}",
                            name,
                            Marshal.GetLastWin32Error()));
                }
            }
            finally
            {
                if (schService != null && !schService.IsInvalid)
                {
                    CloseServiceHandle(schService);
                    schService.SetHandleAsInvalid();
                }
            }
        }


        /// <summary>
        /// Get info about service
        /// </summary>
        /// <returns></returns>
        public ServiceInfo GetServiceInfo()
        {
            var qscPtr = new SafeWaitHandle(Marshal.AllocCoTaskMem(0), true);
            try
            {
                int bytesNeeded = 5;

                int retCode = QueryServiceConfig(_schService, qscPtr, 0, ref bytesNeeded);
                if (retCode == 0 && bytesNeeded == 0)
                {
                    throw new Exception("First call of QueryServiceConfig method get error: " + Marshal.GetLastWin32Error());
                }

                qscPtr = new SafeWaitHandle(Marshal.AllocCoTaskMem(bytesNeeded), true);
                retCode = QueryServiceConfig(_schService, qscPtr,bytesNeeded, ref bytesNeeded);
                if (retCode == 0)
                {
                    throw new Exception("Second call of QueryServiceConfig method get error: " + Marshal.GetLastWin32Error());
                }

                var qscs = (QueryServiceConfigStruct)Marshal.PtrToStructure(
                    qscPtr.DangerousGetHandle(),
                    new QueryServiceConfigStruct().GetType());

                var serviceInfo = new ServiceInfo
                {
                    BinaryPathName = Marshal.PtrToStringAuto(qscs.BinaryPathName),
                    Dependencies = Marshal.PtrToStringAuto(qscs.Dependencies),
                    DisplayName = Marshal.PtrToStringAuto(qscs.DisplayName),
                    LoadOrderGroup = Marshal.PtrToStringAuto(qscs.LoadOrderGroup),
                    StartName = Marshal.PtrToStringAuto(qscs.StartName),                    
                    TagID = qscs.TagID
                };
                
                string errorControl = "Ignore";
                if (qscs.ErrorControl == 1)
                {
                    errorControl = "Normal";
                }
                else if (qscs.ErrorControl == 2)
                {
                    errorControl = "Severe";
                }
                else if (qscs.ErrorControl == 3)
                {
                    errorControl = "Critical";
                }
                serviceInfo.ServiceType = errorControl;

                string serviceType = "File system driver service";
                if (qscs.ServiceType == 1)
                {
                    serviceType = "Driver service";
                }
                else if (qscs.ServiceType == 10)
                {
                    serviceType = "Service that runs in its own process";
                }
                else if (qscs.ServiceType == 20)
                {
                    serviceType = "Service that shares a process with other services";
                }
                serviceInfo.ServiceType = serviceType;

                string startType = "BootStart";
                if (qscs.StartType == 1)
                {
                    startType = "SystemStart";
                }
                else if (qscs.StartType == 2)
                {
                    startType = "Automatically";
                }
                else if (qscs.StartType == 3)
                {
                    startType = "Manual";
                }
                else if (qscs.StartType == 4)
                {
                    startType = "Disabled";
                }
                serviceInfo.StartType = startType;

                return serviceInfo;
            }
            finally
            {
                Marshal.FreeCoTaskMem(qscPtr.DangerousGetHandle());
            }            
        }
    }
}

