using System.IO;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SurgeryHelper.Tools
{
    internal class CMasterKey
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
        /// Initializes a new instance of the <see cref="CMasterKey"/> class. 
        /// Конструктор, внутри которого создаётся мастер ключ и путь до файла с ним
        /// </summary>
        public CMasterKey()
        {
            _masterKeyFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "master.key");

            var sb = new StringBuilder();

            try
            {
                foreach (ManagementObject mdd in new ManagementObjectSearcher(new SelectQuery("Win32_DiskDrive")).Get())
                {
                    string mediaType = mdd.Properties["MediaType"].Value.ToString().ToLower();
                    string interfaceType = mdd.Properties["InterfaceType"].Value.ToString().ToLower();
                    string caption = mdd.Properties["Caption"].Value.ToString().ToLower();
                    if (mediaType.Contains("fixed") && mediaType.Contains("hard") && mediaType.Contains("disk") &&
                        !interfaceType.Contains("usb") && !caption.Contains(" usb "))
                    {
                        sb.Append("up");
                        sb.Append(mdd.Properties["Model"].Value);
                        sb.Append(mdd.Properties["Size"].Value);

                        foreach (ManagementObject mpm in new ManagementObjectSearcher(new SelectQuery("Win32_PhysicalMedia")).Get())
                        {
                            if ((string)mdd.Properties["DeviceID"].Value == (string)mpm.Properties["Tag"].Value)
                            {
                                sb.Append(mpm.Properties["SerialNumber"].Value);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                sb.Append("down" + Dns.GetHostName());
            }

            var sr = new StringBuilder();
            foreach (byte b in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(sb.ToString())))
            {
                sr.Append(b.ToString("x2"));
            }

            _hashInfoHardDisks = sr.ToString();
        }


        /// <summary>
        /// Получить мастер-ключ из файла
        /// </summary>
        /// <returns></returns>
        public string GetMasterKeyFromFile()
        {
            string masterKey = string.Empty;

            if (!File.Exists(_masterKeyFilePath))
            {
                return masterKey;
            }

            using (var sr = new StreamReader(_masterKeyFilePath))
            {
                masterKey = sr.ReadToEnd();
            }

            return masterKey;
        }


        /// <summary>
        /// Создать файл с мастер-ключом
        /// </summary>
        public void CreateMasterKeyFile()
        {
            using (var sw = new StreamWriter(_masterKeyFilePath, false))
            {
                sw.Write(_hashInfoHardDisks);
            }
        }
    }
}
