using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

using zlib;

namespace SurgeryHelper.Tools
{
    /// <summary>
    /// ����� ��� ���������� ������
    /// </summary>
    public static class CDatabaseHelper
    {
        /// <summary>
        /// ������������ ������ � ����
        /// </summary>
        /// <param name="dataStr">������ � �������</param>
        /// <param name="filePath">���� �� �����</param>
        public static void PackText(string dataStr, string filePath)
        {
            byte[] bytesBuffer = Encoding.GetEncoding("windows-1251").GetBytes(dataStr);
            byte[] rez = PackData(bytesBuffer);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(rez, 0, rez.Length);
            }
        }


        /// <summary>
        /// �������� ����� �� ������������� �����
        /// </summary>
        /// <param name="filePath">���� �� �����</param>
        /// <returns></returns>
        public static string UnpackText(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            byte[] bytesBuffer;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                bytesBuffer = new byte[fs.Length];
                fs.Read(bytesBuffer, 0, bytesBuffer.Length);
            }

            byte[] rez = UnpackData(bytesBuffer);
            if (rez.Length == 0)
            {
                return string.Empty;
            }

            return Encoding.GetEncoding("windows-1251").GetString(rez);
        }


        /// <summary>
        /// ���������� �������� � ��������� � � ����
        /// </summary>
        /// <param name="picture">Bitmap ��� ���������</param>
        /// <param name="filePath">���� �� �����</param>
        public static void SaveBitmapToFile(Bitmap picture, string filePath)
        {
            string folderName = Path.GetDirectoryName(filePath) ?? string.Empty;
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            picture.Save(filePath, ImageFormat.Png);
        }


        /// <summary>
        /// ����������� ������ �� �����
        /// </summary>
        /// <param name="filePath">���� �� �����</param>
        /// <returns></returns>
        public static Bitmap GetBitmapFromFile(string filePath)
        {            
            var picture = new Bitmap(filePath);

            return new Bitmap(picture);
        }        


        /// <summary>
        /// ���������� ������ ����
        /// </summary>
        /// <param name="byteBuffer">������ ��� ���������</param>
        /// <returns></returns>
        private static byte[] PackData(byte[] byteBuffer)
        {
            byte[] rez;

            using (var stream = new MemoryStream())
            {
                var zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
                zStream.Write(byteBuffer, 0, byteBuffer.Length);
                zStream.Close();
                
                rez = stream.ToArray();
            }

            return rez;
        }


        /// <summary>
        /// ����������� ������ ����
        /// </summary>
        /// <param name="byteBuffer">������ ��� ����������</param>
        /// <returns></returns>
        private static byte[] UnpackData(byte[] byteBuffer)
        {
            byte[] rez;

            using (var stream = new MemoryStream())
            {
                var zStream = new ZOutputStream(stream);

                try
                {
                    zStream.Write(byteBuffer, 0, byteBuffer.Length);
                }
                catch
                {
                    return new byte[0];
                }

                zStream.Close();

                rez = stream.ToArray();
            }

            return rez;
        }
    }
}
