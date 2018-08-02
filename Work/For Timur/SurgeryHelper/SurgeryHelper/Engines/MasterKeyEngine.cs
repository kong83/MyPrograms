using System.IO;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SurgeryHelper.Engines
{
    internal class MasterKeyEngine
    {
        private readonly string _hashInfoHardDisks;
        private readonly string _masterKeyFilePath = "master.key";

        public string HashInfoHardDisks
        {
            get
            {
                return _hashInfoHardDisks;
            }
        }

        /// <summary>
        /// Конструктор, внутри которого создаётся мастер ключ и путь до файла с ним
        /// </summary>
        public MasterKeyEngine()
        {
            _masterKeyFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "master.key");

            string infoToGenerateMasterKey = GetInfoToGenerateMasterKey();

            var sr = new StringBuilder();
            foreach (byte b in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes("SecretWord" + infoToGenerateMasterKey + "Some Numbers 123")))
            {
                sr.Append(b.ToString("x2"));
            }

            _hashInfoHardDisks = sr.ToString();
        }

        public string GetInfoToGenerateMasterKey()
        {
            var sb = new StringBuilder();

            try
            {
                foreach (ManagementObject mdd in new ManagementObjectSearcher(new SelectQuery("Win32_DiskDrive")).Get())
                {
                    if (Regex.IsMatch((string)mdd.Properties["MediaType"].Value, @"^(?i:FIXED[\s\t]+HARD\sDISK[\s\t\w]*)", RegexOptions.Compiled))
                    {
                        sb.AppendLine(mdd.Properties["Model"].Value.ToString());
                        sb.AppendLine(mdd.Properties["Size"].Value.ToString());

                        try
                        {
                            sb.AppendLine(mdd.Properties["SerialNumber"].Value.ToString());
                        }
                        catch
                        {
                            foreach (ManagementObject mpm in new ManagementObjectSearcher(new SelectQuery("Win32_PhysicalMedia")).Get())
                            {
                                if ((string)mdd.Properties["DeviceID"].Value == (string)mpm.Properties["Tag"].Value)
                                {
                                    sb.AppendLine(mpm.Properties["SerialNumber"].Value.ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                sb.Append(Dns.GetHostName());
            }

            return sb.ToString().TrimEnd(new[] { '\r', '\n' });
        }

        /// <summary>
        /// Получить мастер-ключ из файла
        /// </summary>
        /// <returns></returns>
        public string GetMasterKeyFromFile()
        {
            return File.Exists(_masterKeyFilePath) ? File.ReadAllText(_masterKeyFilePath) : string.Empty;
        }


        /// <summary>
        /// Создать файл с мастер-ключом
        /// </summary>
        public void CreateMasterKeyFile(string masterKey)
        {
            File.WriteAllText(_masterKeyFilePath, masterKey);
        }
    }
}
