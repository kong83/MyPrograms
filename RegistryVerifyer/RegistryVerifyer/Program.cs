using System;
using System.Windows.Forms;
using Microsoft.Win32;
using Timer = System.Timers.Timer;

namespace RegistryVerifyer
{
    public class Program
    {
        private const string AutostartRegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private Timer _timerNotification;

        private void CheckScreenSaverParameter()
        {
            try
            {
                const string regPath = @"Software\Policies\Microsoft\Windows\Control Panel\Desktop";
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(regPath, true);

                if (regKey == null)
                {
                    Console.WriteLine(@"Can't find " + regPath);
                    return;
                }

                const string timeOut = "999999";
                string s = string.Empty;
                s = (string)regKey.GetValue("ScreenSaveTimeOut", s);

                if (s != timeOut)
                {
                    regKey.SetValue("ScreenSaveTimeOut", timeOut);
                    regKey.DeleteValue("ScreenSaveActive");
                    regKey.DeleteValue("ScreenSaverIsSecure");
                    regKey.DeleteValue("SCRNSAVE.EXE");
                    Console.WriteLine("Screensaver was set to the correct state");
                }

                regKey.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CheckUACParameter()
        {
            try
            {
                const string regPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(regPath, true);

                if (regKey == null)
                {
                    Console.WriteLine(@"Can't find " + regPath);
                    return;
                }

                const int disabelUACValue = 0;
                int s = 0;
                s = (int)regKey.GetValue("EnableLUA", s);

                if (s != disabelUACValue)
                {
                    regKey.SetValue("EnableLUA", disabelUACValue);
                    Console.WriteLine("UAC was set to the correct state");
                }

                regKey.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CheckFirewallParameters()
        {
            try
            {
                const int disabelFirewallValue = 0;
                const int disabelNotificationValue = 1;

                const string baseRegPath = @"SYSTEM\CurrentControlSet\services\SharedAccess\Parameters\FirewallPolicy\";
                var subFolderNames = new[] { "DomainProfile", "PublicProfile", "StandardProfile" };

                foreach (var subFolderName in subFolderNames)
                {
                    string regPath = baseRegPath + subFolderName;

                    RegistryKey regKey = Registry.LocalMachine.OpenSubKey(regPath, true);

                    if (regKey == null)
                    {
                        Console.WriteLine(@"Can't find " + regPath);
                        continue;
                    }

                    int currentValue = 10;
                    currentValue = (int)regKey.GetValue("EnableFirewall", currentValue);

                    if (currentValue != disabelFirewallValue)
                    {
                        regKey.SetValue("EnableFirewall", disabelFirewallValue);
                        Console.WriteLine("EnableFirewall was disabled for " + subFolderName);
                    }

                    currentValue = 10;
                    currentValue = (int)regKey.GetValue("DisableNotifications", currentValue);

                    if (currentValue != disabelNotificationValue)
                    {
                        regKey.SetValue("DisableNotifications", disabelNotificationValue);
                        Console.WriteLine("DisableNotifications was disabled for " + subFolderName);
                    }

                    regKey.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void timerNotification_Tick(object sender, EventArgs e)
        {
            try
            {
                _timerNotification.Enabled = false;

                CheckScreenSaverParameter();

                CheckUACParameter();

                CheckFirewallParameters();
            }
            finally
            {
                _timerNotification.Enabled = true;
            }
        }

        private void StartProgram()
        {
            try
            {
                Console.WriteLine("Registry verifyer is loking to registry and fix some incorrect values to correct values.\r\nType 'exit' to exit.\r\nType 'rem_auto' to remove autostart for this program.\r\nVersion: 2.0");
                 
                // Add this programm to autorun
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(AutostartRegistryPath, true);

                if (regKey == null)
                {
                    Console.WriteLine(@"Can't find HKLM\" + AutostartRegistryPath);
                }
                else
                {
                    regKey.SetValue("RegistryVerifyer", Application.ExecutablePath);
                    regKey.Close();
                }

                _timerNotification = new Timer();
                _timerNotification.Elapsed += timerNotification_Tick;
                _timerNotification.Interval = 60000;
                _timerNotification.Enabled = true;
                timerNotification_Tick(null, null);

                while (true)
                {
                    string str = Console.ReadLine();
                    if (!string.IsNullOrEmpty(str) && str.ToLower().Contains("exit"))
                    {
                        return;
                    }

                    if (!string.IsNullOrEmpty(str) && str.ToLower().Contains("rem_auto"))
                    {
                        regKey = Registry.LocalMachine.OpenSubKey(AutostartRegistryPath, true);

                        if (regKey != null)
                        {
                            regKey.DeleteValue("RegistryVerifyer");
                            Console.WriteLine("Autostart was removed");
                            regKey.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        static void Main()
        {
            new Program().StartProgram();
        }
    }
}
