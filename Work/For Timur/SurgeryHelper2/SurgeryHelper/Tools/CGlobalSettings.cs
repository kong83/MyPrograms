using System;
using System.IO;

using SurgeryHelper.Workers;

namespace SurgeryHelper.Tools
{
    public class CGlobalSettings : CBaseWorker
    {
        /// <summary>
        /// Название отделения
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return _departmentName;
            }

            set
            {
                _departmentName = value;
                SaveGlobalSettings();
            }
        }

        /// <summary>
        /// Название отделения
        /// </summary>
        public string DischargeEpicrisisHeaderFileName
        {
            get
            {
                return _dischargeEpicrisisHeaderFileName;
            }

            set
            {
                _dischargeEpicrisisHeaderFileName = value;
                SaveGlobalSettings();
            }
        }

        /// <summary>
        /// Заведующая отделением (по умолчанию)
        /// </summary>
        public string BranchManager
        {
            get
            {
                return _branchManager;
            }

            set
            {
                _branchManager = value;
                SaveGlobalSettings();
            }
        }

        /// <summary>
        /// Анестезист (по умолчанию)
        /// </summary>
        public string HeAnaesthetist
        {
            get
            {
                return _heAnaesthetist;
            }

            set
            {
                _heAnaesthetist = value;
                SaveGlobalSettings();
            }
        }

        /// <summary>
        /// Анестезистка (по умолчанию)
        /// </summary>
        public string SheAnaesthetist
        {
            get
            {
                return _sheAnaesthetist;
            }

            set
            {
                _sheAnaesthetist = value;
                SaveGlobalSettings();
            }
        }

        /// <summary>
        /// Отображать ли индексы базы данных в таблицах
        /// </summary>
        public bool ShowDbIndexes
        {
            get
            {
                return _showDbIndexes;
            }

            set
            {
                _showDbIndexes = value;
                SaveGlobalSettings();
            }
        }

        private string _departmentName;
        private string _dischargeEpicrisisHeaderFileName;
        private string _branchManager;
        private string _heAnaesthetist;
        private string _sheAnaesthetist;
        private bool _showDbIndexes;

        private readonly string _globalSettingsPath;

        public CGlobalSettings(string dataPath)
        {
            _globalSettingsPath = Path.Combine(dataPath, "global_settings.save");

            _departmentName = "8";
            _dischargeEpicrisisHeaderFileName = "DischargeEpicrisisHeader.doc";
            _branchManager = "В.А.Калантырская";
            _heAnaesthetist = "А.С. Седякин";
            _sheAnaesthetist = "Е.Е. Николаева";
            _showDbIndexes = false;

            LoadGlobalSettings();
        }


        /// <summary>
        /// Сохранение глобальных настроек
        /// </summary>
        private void SaveGlobalSettings()
        {
            string globalSettingsStr =
                "BranchManager=" + _branchManager + DataSplitStr +
                "DischargeEpicrisisHeaderFileName=" + _dischargeEpicrisisHeaderFileName + DataSplitStr +
                "HeAnaesthetist=" + _heAnaesthetist + DataSplitStr +
                "SheAnaesthetist=" + _sheAnaesthetist + DataSplitStr +
                "DepartmentName=" + _departmentName +  DataSplitStr +
                "ShowDbIndexes=" + _showDbIndexes;

            CDatabaseEngine.PackText(globalSettingsStr, _globalSettingsPath);
        }


        /// <summary>
        /// Сохранение глобальных настроек
        /// </summary>
        private void LoadGlobalSettings()
        {
            string allDataStr = CDatabaseEngine.UnpackText(_globalSettingsPath);

            // Для каждого объекта получаем список значений
            string[] datasStr = allDataStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string dataStr in datasStr)
            {
                string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                switch (keyValue[0])
                {
                    case "BranchManager":
                        _branchManager = keyValue[1];
                        break;
                    case "DischargeEpicrisisHeaderFileName":
                        _dischargeEpicrisisHeaderFileName = keyValue[1];
                        break;
                    case "HeAnaesthetist":
                        _heAnaesthetist = keyValue[1];
                        break;
                    case "SheAnaesthetist":
                        _sheAnaesthetist = keyValue[1];
                        break;
                    case "DepartmentName":
                        _departmentName = keyValue[1];
                        break;
                    case "ShowDbIndexes":
                        _showDbIndexes = Convert.ToBoolean(keyValue[1]);
                        break;
                }
            }
        }
    }
}
