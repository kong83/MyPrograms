using SurgeryHelper.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SurgeryHelper.Engines
{
    public class ConfigurationEngine
    {
        private readonly Configuration _config;
        private readonly AppSettingsSection _appSettings;

        public ConfigurationEngine()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _appSettings = (AppSettingsSection)_config.GetSection("appSettings");

            Console.Write(_appSettings.Settings["InternalData"].Value);
        }

        /// <summary>
        /// Внутренняя переменная для работы
        /// </summary>
        public long InternalData
        {
            get
            {
                return Convert.ToInt64(_appSettings.Settings["InternalData"].Value);
            }

            set
            { 
                _appSettings.Settings["InternalData"].Value = value.ToString();
                _config.Save();
            }
        }

        private static Size GetSizeFromString(string sizeStr)
        {
            string[] sizeArr = sizeStr.Split(',');
            return new Size(Convert.ToInt32(sizeArr[0].Trim()), Convert.ToInt32(sizeArr[1].Trim()));
        }

        private static string GetStringFromSize(Size size)
        {
            return size.Width + "," + size.Height;
        }

        /// <summary>
        /// Размер формы со списком пациентов
        /// </summary>
        public Size PatientFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["PatientFormSize"].Value);                
            }

            set
            {
                _appSettings.Settings["PatientFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Размер формы со списком операций
        /// </summary>
        public Size OperationFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["OperationFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["OperationFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Размер формы с кодами МКБ
        /// </summary>
        public Size MKBSelectFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["MKBSelectFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["MKBSelectFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Размер формы с назначениями
        /// </summary>
        public Size PrescriptionFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["PrescriptionFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["PrescriptionFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Размер формы с услугами
        /// </summary>
        public Size ServiceSelectFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["ServiceSelectFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["ServiceSelectFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Размер формы с информацией по пациенту
        /// </summary>
        public Size PatientViewFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["PatientViewFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["PatientViewFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Список последних выбранных МКБ
        /// </summary>
        public string[] PatientViewFormLastMKB
        {
            get
            {
                string[] lastList = _appSettings.Settings["PatientViewFormLastMKB"].Value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                return lastList;
            }

            set
            {
                _appSettings.Settings["PatientViewFormLastMKB"].Value = string.Join(";", value);
                _config.Save();
            }
        }

        /// <summary>
        /// Список последних выбранных сервисов для дневного стационара
        /// </summary>
        public List<LastServiceComboBoxItem> PatientViewFormLastDayServices
        {
            get
            {
                string[] lastList = _appSettings.Settings["PatientViewFormLastDayServices"].Value.Split(new[] { "^&" }, StringSplitOptions.RemoveEmptyEntries);
                var comboBoxItems = new List<LastServiceComboBoxItem>();
                foreach (string lastValue in lastList)
                {
                    comboBoxItems.Add(new LastServiceComboBoxItem(lastValue));
                }

                return comboBoxItems;
            }

            set
            {
                var result = new StringBuilder();
                foreach (var item in value)
                {
                    result.Append(item.HiddenValue + "^&");
                }

                _appSettings.Settings["PatientViewFormLastDayServices"].Value = result.ToString();
                _config.Save();
            }
        }

        /// <summary>
        /// Список последних выбранных сервисов для ночного стационара
        /// </summary>
        public List<LastServiceComboBoxItem> PatientViewFormLastNightServices
        {
            get
            {
                string[] lastList = _appSettings.Settings["PatientViewFormLastNightServices"].Value.Split(new[] { "^&" }, StringSplitOptions.RemoveEmptyEntries);
                var comboBoxItems = new List<LastServiceComboBoxItem>();
                foreach (string lastValue in lastList)
                {
                    comboBoxItems.Add(new LastServiceComboBoxItem(lastValue));
                }

                return comboBoxItems;
            }

            set
            {
                var result = new StringBuilder();
                foreach (var item in value)
                {
                    result.Append(item.HiddenValue + "^&");
                }

                _appSettings.Settings["PatientViewFormLastNightServices"].Value = result.ToString();
                _config.Save();
            }
        }

        private static Point GetPointFromString(string pointStr)
        {
            string[] pointArr = pointStr.Split(',');
            return new Point(Convert.ToInt32(pointArr[0].Trim()), Convert.ToInt32(pointArr[1].Trim()));
        }

        private static string GetStringFromPoint(Point point)
        {
            return point.X + "," + point.Y;
        }

        /// <summary>
        /// Расположение формы со списком пациентов
        /// </summary>
        public Point PatientFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["PatientFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["PatientFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы со списком операций
        /// </summary>
        public Point OperationFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["OperationFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["OperationFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с информацией по операции
        /// </summary>
        public Point OperationViewFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["OperationViewFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["OperationViewFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с назначениями
        /// </summary>
        public Point PrescriptionFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["PrescriptionFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["PrescriptionFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }
        

        /// <summary>
        /// Расположение формы с кодами МКБ
        /// </summary>
        public Point MKBSelectFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["MKBSelectFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["MKBSelectFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с услугами
        /// </summary>
        public Point ServiceSelectFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["ServiceSelectFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["ServiceSelectFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Сохранённое значение для поля со списком хирургов
        /// </summary>
        public string OperationViewFormTextBoxSurgeons
        {
            get
            {
                return _appSettings.Settings["OperationViewFormTextBoxSurgeons"].Value;
            }

            set
            {
                _appSettings.Settings["OperationViewFormTextBoxSurgeons"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Сохранённое значение для поля со списком ассистентов
        /// </summary>
        public string OperationViewFormTextBoxAssistents
        {
            get
            {
                return _appSettings.Settings["OperationViewFormTextBoxAssistents"].Value;
            }

            set
            {
                _appSettings.Settings["OperationViewFormTextBoxAssistents"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Сохранённое значение для поля с информацией об анестезиологе
        /// </summary>
        public string OperationViewFormTextBoxHeAnestethist
        {
            get
            {
                return _appSettings.Settings["OperationViewFormTextBoxHeAnestethist"].Value;
            }

            set
            {
                _appSettings.Settings["OperationViewFormTextBoxHeAnestethist"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Сохранённое значение для поля с информацией об анестезистке
        /// </summary>
        public string OperationViewFormTextBoxSheAnestethist
        {
            get
            {
                return _appSettings.Settings["OperationViewFormTextBoxSheAnestethist"].Value;
            }

            set
            {
                _appSettings.Settings["OperationViewFormTextBoxSheAnestethist"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Сохранённое значение для поля с информацией об операционной мед. сестре
        /// </summary>
        public string OperationViewFormComboBoxScrubNurse
        {
            get
            {
                return _appSettings.Settings["OperationViewFormComboBoxScrubNurse"].Value;
            }

            set
            {
                _appSettings.Settings["OperationViewFormComboBoxScrubNurse"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Сохранённое значение для поля с информацией о санитаре
        /// </summary>
        public string OperationViewFormComboBoxOrderly
        {
            get
            {
                return _appSettings.Settings["OperationViewFormComboBoxOrderly"].Value;
            }

            set
            {
                _appSettings.Settings["OperationViewFormComboBoxOrderly"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с информацией по пациенту
        /// </summary>
        public Point PatientViewFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["PatientViewFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["PatientViewFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с выписным эпикризом
        /// </summary>
        public Point DischargeEpicrisisFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["DischargeEpicrisisFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["DischargeEpicrisisFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с переводным эпикризом
        /// </summary>
        public Point TransferableEpicrisisFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["TransferableEpicrisisFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["TransferableEpicrisisFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с этапным эпикризом
        /// </summary>
        public Point LineOfCommunicationFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["LineOfCommunicationFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["LineOfCommunicationFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с осмотром в отделении
        /// </summary>
        public Point MedicalInspectionFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["MedicalInspectionFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["MedicalInspectionFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Расположение формы с протоколом операции
        /// </summary>
        public Point OperationProtocolFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["OperationProtocolFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["OperationProtocolFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        /// <summary>
        /// Ширина колонок в таблице со списком операций
        /// </summary>
        public string PatientFormListWidths
        {
            get
            {
                return _appSettings.Settings["PatientFormListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["PatientFormListWidths"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Отображаются ли фильтры на форме с пациентами
        /// </summary>
        public bool PatientFormIsFilterShowed
        {
            get
            {
                return Convert.ToBoolean(_appSettings.Settings["PatientFormIsFilterShowed"].Value);
            }

            set
            {
                _appSettings.Settings["PatientFormIsFilterShowed"].Value = value.ToString();
                _config.Save();
            }
        }

        /// <summary>
        /// Ширина колонок в таблице со списком операций
        /// </summary>
        public string OperationFormListWidths
        {
            get
            {
                return _appSettings.Settings["OperationFormListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["OperationFormListWidths"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Ширина колонок в таблице с кодами МКБ
        /// </summary>
        public string MKBSelectFormListWidths
        {
            get
            {
                return _appSettings.Settings["MKBSelectFormListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["MKBSelectFormListWidths"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Ширина колонок в таблице с терапией на форме с назначениями
        /// </summary>
        public string PrescriptionFormTherapyListWidths
        {
            get
            {
                return _appSettings.Settings["PrescriptionFormTherapyListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["PrescriptionFormTherapyListWidths"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Ширина колонок в таблице с дополнительными методами исследования на форме с назначениями
        /// </summary>
        public string PrescriptionFormSurveyListWidths
        {
            get
            {
                return _appSettings.Settings["PrescriptionFormSurveyListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["PrescriptionFormSurveyListWidths"].Value = value;
                _config.Save();
            }
        }


        /// <summary>
        /// Ширина колонок в таблице с услугами
        /// </summary>
        public string ServiceSelectFormListWidths
        {
            get
            {
                return _appSettings.Settings["ServiceSelectFormListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["ServiceSelectFormListWidths"].Value = value;
                _config.Save();
            }
        }

        /// <summary>
        /// Имя колонки для фильтрации на форме со списком пациентов
        /// </summary>
        public string PatientFormFilterColumnName
        {
            get
            {
                return _appSettings.Settings["PatientFormFilterColumnName"].Value;
            }

            set
            {
                _appSettings.Settings["PatientFormFilterColumnName"].Value = value;
                _config.Save();
            }
        }   

        /// <summary>
        /// Направление фильтрации на форме со списком пациентов
        /// </summary>
        public SortOrder PatientFormFilterDirection
        {
            get
            {
                return (SortOrder)Enum.Parse(typeof(SortOrder), _appSettings.Settings["PatientFormFilterDirection"].Value);
            }

            set
            {
                _appSettings.Settings["PatientFormFilterDirection"].Value = value.ToString();
                _config.Save();
            }
        }         
    }
}
