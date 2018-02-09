using System;
using System.Collections.Generic;
using System.Linq;
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
      string res = "";
      foreach (byte b in packArr)
      {
        res += b + ";";
      }
      res = res.Substring(0, res.Length - 1);

      //
      // Сохранение данных в реестре
      //
      RegistryKey regKey = Registry.CurrentUser;
      regKey = regKey.CreateSubKey("Software\\PassStore\\");            
      regKey.SetValue("Data", res);
    }

    /// <summary>
    /// Извлечение массива байт из реестра и распаковка его
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetSavedString()
    {
      //
      // Извлечение данных из реестра
      //
      RegistryKey regKey = Registry.CurrentUser;
      regKey = regKey.CreateSubKey("Software\\PassStore\\");
      string s = "";
      s = (string)regKey.GetValue("Data", s);

      if (s == "")
      {
        return "";
      }

      string[] strByte = s.Split(new char[1] { ';' });

      byte[] byteArr = new byte[strByte.Length];
      try
      {
        for (int i = 0; i < strByte.Length; i++ )
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
  }
}
