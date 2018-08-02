using System;
using System.Text;
using System.IO;
using zlib;
using Microsoft.Win32;
using System.Windows.Forms;

namespace PassStore
{
    class ZlibClass
    {
        /// <summary>
        /// Запаковывает массив байт
        /// </summary>
        /// <param name="xmlInfo">Массив байт для запаковки</param>
        /// <returns></returns>
        private static byte[] PackBytes(byte[] xmlInfo)
        {
            byte[] newByffer = new byte[xmlInfo.Length * 100];
            MemoryStream stream = new MemoryStream(newByffer);
            ZOutputStream zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }

            byte[] rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }

            return rez;
        }

        /// <summary>
        /// Распаковывает массив байт
        /// </summary>
        /// <param name="xmlInfo">Массив байт для распаковки</param>
        /// <returns></returns>
        private static byte[] UnpackBytes(byte[] xmlInfo)
        {
            byte[] newByffer = new byte[xmlInfo.Length * 100];
            MemoryStream stream = new MemoryStream(newByffer);
            ZOutputStream zStream = new ZOutputStream(stream);

            try
            {
                zStream.Write(xmlInfo, 0, xmlInfo.Length);
            }
            catch
            {
            }
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }

            byte[] rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }
            return rez;
        }

        /// <summary>
        /// Упаковывание строки и сохранение данных в реестр
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static void SaveString(string str)
        {
            byte[] byteArr = Encoding.GetEncoding("windows-1251").GetBytes(str);
            byte[] packArr = PackBytes(byteArr);
            string passData = "";
            foreach (byte b in packArr)
            {
                passData += b + ";";
            }
            passData = passData.Substring(0, passData.Length - 1);

            SetPassData(passData);
        }

        /// <summary>
        /// Извлечение массива байт из реестра и распаковка его
        /// </summary>
        /// <returns></returns>
        public static string GetSavedString()
        {
            var passData = GetPassData();

            if (passData == "")
            {
                return "";
            }

            string[] strByte = passData.Split(new char[1] { ';' });

            byte[] byteArr = new byte[strByte.Length];
            try
            {
                for (int i = 0; i < strByte.Length; i++)
                {
                    byteArr[i] = Convert.ToByte(strByte[i]);
                }
            }
            catch
            {
                MessageBox.Show("Неправильные значения в реестре", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            byte[] unpackArr = UnpackBytes(byteArr);
            return Encoding.GetEncoding("windows-1251").GetString(unpackArr);
        }

        /// <summary>
        /// Извлечение данных из реестра
        /// </summary>
        /// <returns></returns>
        public static string GetPassData()
        {
            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");

            return (string)regKey.GetValue("Data", "");
        }

        /// <summary>
        /// Помещение данные в реестр
        /// </summary>
        /// <returns></returns>
        public static void SetPassData(string passData)
        {
            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");

            regKey.SetValue("Data", passData);
        }
    }
}
