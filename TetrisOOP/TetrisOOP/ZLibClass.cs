using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zlib;
using System.IO;

namespace TetrisOOP
{
  public class ZLibClass
  {
    /// <summary>
    /// Pack byte array
    /// </summary>
    /// <param name="xmlInfo">Array for packing</param>
    /// <returns></returns>
    public byte[] Pack(byte[] xmlInfo)
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
    /// Unpack byte array
    /// </summary>
    /// <param name="xmlInfo">Array for unpacking</param>
    /// <returns></returns>
    public byte[] Unpack(byte[] xmlInfo)
    {
      byte[] newByffer = new byte[xmlInfo.Length * 100];
      MemoryStream stream = new MemoryStream(newByffer);
      ZOutputStream zStream = new ZOutputStream(stream);

      try
      {
        zStream.Write(xmlInfo, 0, xmlInfo.Length);
      }
      catch { }
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

  }
}
