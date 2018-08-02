using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace StatisticHelper
{
    class Logger
    {
        /// <summary>
        /// Path to the working folder
        /// </summary>
        private readonly string m_WorkPath;

        public Logger(string workPath)
        {
            m_WorkPath = workPath;

            //
            // Check that folder with db utility results does not exist and create it
            //
            if (Directory.Exists(m_WorkPath))
            {
                throw new Exception(string.Format("Folder {0} already exists", m_WorkPath));
            }
            Directory.CreateDirectory(m_WorkPath);
        }


        /// <summary>
        /// Write information to a log and to the screen
        /// </summary>
        /// <param name="message">Format information string</param>
        /// <param name="parameters">Parameters</param>
        public void WriteLog(string message, params string[] parameters)
        {
            WriteLog(TraceEventType.Information, message, parameters);
        }


        /// <summary>
        /// Write log information with type of message
        /// </summary>
        /// <param name="traceType">Type of message</param>
        /// <param name="message">Format information string</param>
        /// <param name="parameters">Parameters</param>
        public void WriteLog(TraceEventType traceType, string message, params string[] parameters)
        {
            message = string.Format(message, parameters);                     

            if (Directory.Exists(m_WorkPath))
            {
                using (var sw = new StreamWriter(Path.Combine(m_WorkPath, "StatisticHelper.log"), true, Encoding.GetEncoding("windows-1251")))
                {
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond + ": " + traceType + ": " + message);
                }
            }
        }        
    }
}
