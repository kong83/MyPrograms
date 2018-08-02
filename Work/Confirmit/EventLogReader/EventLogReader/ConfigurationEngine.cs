using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Drawing;

namespace EventLogReader
{
    public enum Addressee
    {
        Excel,
        TextFile
    }

    public class ConfigurationEngine
    {
        private Configuration mConfig;
        private AppSettingsSection mAppSettings;

        public ConfigurationEngine()
        {
            mConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            mAppSettings = (AppSettingsSection)mConfig.GetSection("appSettings");
        }

        /// <summary>
        /// Дата начала промежутка
        /// </summary>
        public DateTime? DateStart
        {
            get
            {
                string dateStart = mAppSettings.Settings["DateStart"].Value;
                if (string.IsNullOrEmpty(dateStart))
                {
                    return null;
                }

                return DateTime.Parse(dateStart);
            }

            set
            {
                if (value.HasValue)
                {
                    mAppSettings.Settings["DateStart"].Value = value.Value.ToString("dd.MM.yyyy HH:mm:ss");
                }
                else
                { 
                
                }

                mConfig.Save();
            }
        }

        /// <summary>
        /// Дата окончания промежутка
        /// </summary>
        public DateTime? DateEnd
        {
            get
            {
                string dateStart = mAppSettings.Settings["DateEnd"].Value;
                if (string.IsNullOrEmpty(dateStart))
                {
                    return null;
                }

                return DateTime.Parse(dateStart);
            }

            set
            {
                if (value.HasValue)
                {
                    mAppSettings.Settings["DateEnd"].Value = value.Value.ToString("dd.MM.yyyy HH:mm:ss");
                }
                else
                {

                }

                mConfig.Save();
            }
        }       

        /// <summary>
        /// Имя компьютера
        /// </summary>
        public string ComputerName
        {
            get
            {
                return mAppSettings.Settings["ComputerName"].Value;
            }

            set
            {
                mAppSettings.Settings["ComputerName"].Value = value;
                mConfig.Save();
            }
        }

        /// <summary>
        /// Название Eventlog-а
        /// </summary>
        public string EventLogName
        {
            get
            {
                return mAppSettings.Settings["EventLogName"].Value;
            }

            set
            {
                mAppSettings.Settings["EventLogName"].Value = value;
                mConfig.Save();
            }
        }

        /// <summary>
        /// Название текстового файла
        /// </summary>
        public string TextFileName
        {
            get
            {
                return mAppSettings.Settings["TextFileName"].Value;
            }

            set
            {
                mAppSettings.Settings["TextFileName"].Value = value;
                mConfig.Save();
            }
        }

        /// <summary>
        /// Куда класть - в файл или в excel
        /// </summary>
        public Addressee PutToExcelOrTextFile
        {
            get
            {
                string putToFileOrexcel = mAppSettings.Settings["PutToExcelOrTextFile"].Value;

                if (putToFileOrexcel == "Excel")
                {
                    return Addressee.Excel;
                }

                return Addressee.TextFile;
            }

            set
            {
                mAppSettings.Settings["PutToExcelOrTextFile"].Value = value.ToString();
                mConfig.Save();
            }
        }
    }
}
