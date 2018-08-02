using System;
using System.IO;
using System.Text;

namespace SurgeryHelper.Entities
{
    public class LoggerEngine
    {
        private const string LogFileName = @"c:\SurgeryHelper.log";
        private readonly GlobalSettingsClass _globalSettingsClass;

        public LoggerEngine(GlobalSettingsClass globalSettingsClass)
        {
            _globalSettingsClass = globalSettingsClass;
        }

        public void WriteLog(string text)
        {
            if (_globalSettingsClass == null || !_globalSettingsClass.IsLoggingEnabled)
            {
                return;
            }

            File.AppendAllText(LogFileName, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + DateTime.Now.Millisecond + ": " + text + "\r\n", Encoding.GetEncoding("windows-1251"));
        }
    }
}
