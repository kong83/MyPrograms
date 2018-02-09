using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Checkers
{
    public class LogClass
    {
        public string logPath = "";
        private StreamWriter writer;

        public LogClass()
        {
            logPath = DateTime.Now.Year.ToString("D4") + DateTime.Now.Day.ToString("D2") + DateTime.Now.Month.ToString("D2") + ".log";

            writer = new StreamWriter(logPath, false, Encoding.GetEncoding(1251));
            writer.Write("");
            writer.Close();
        }

        /// <summary>
        /// Add new line
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
        repeat:
            try
            {
                writer = new StreamWriter(logPath, true, Encoding.GetEncoding(1251));
                string time = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") +
                 ":" + DateTime.Now.Second.ToString("D2") + ":" + DateTime.Now.Millisecond.ToString("D3") + "\t\t";

                writer.WriteLine(time + text);
                writer.Close();
            }
            catch
            {

                goto repeat;
            }
        }
    }
}
