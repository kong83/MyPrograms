using System.Text;

namespace SurgeryHelper.Tools
{
    public static class CPassHelper
    {
        public static string PassStr { get; set; }

         /// <summary>
        /// Получение хэш кода строки
        /// </summary>
        /// <returns></returns>
        public static long GetHash()
        {
            byte[] arr = Encoding.ASCII.GetBytes(PassStr);
            long s = 1;
            int d = 1;

            foreach (byte b in arr)
            {
                s *= b | d;
                d++;
            }

            return s;
        }
    }
}
