using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace SurgeryHelper.Tools
{
    public class CConfigurationEngine
    {
        private readonly Configuration _config;
        private readonly AppSettingsSection _appSettings;

        public CConfigurationEngine()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _appSettings = (AppSettingsSection)_config.GetSection("appSettings");

            Console.Write(_appSettings.Settings["InternalData"].Value);
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


        private static Point GetPointFromString(string pointStr)
        {
            string[] pointArr = pointStr.Split(',');
            return new Point(Convert.ToInt32(pointArr[0].Trim()), Convert.ToInt32(pointArr[1].Trim()));
        }


        private static string GetStringFromPoint(Point point)
        {
            return point.X + "," + point.Y;
        }


        private static Color GetColorFromString(string str)
        {
            string[] colorArr = str.Split(',');
            if (colorArr.Length != 3)
            {
                throw new Exception("Неправильный параметр '" + str + "'. Должно быть три цифры через ','");
            }

            return Color.FromArgb(255, Convert.ToInt32(colorArr[0]), Convert.ToInt32(colorArr[1]), Convert.ToInt32(colorArr[2]));
        }


        private static string GetStringFromColor(Color color)
        {
            return color.R + "," + color.G + "," + color.B;
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
        /// Размер формы с отличиями в объектах при мерже
        /// </summary>
        public Size MergeShowDifferenceFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["MergeShowDifferenceFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["MergeShowDifferenceFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Размер формы с информацией о госпитализации
        /// </summary>
        public Size HospitalizationViewFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["HospitalizationViewFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["HospitalizationViewFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Размер формы с информацией о консультации
        /// </summary>
        public Size VisitViewFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["VisitViewFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["VisitViewFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Размер формы для мержа двух баз
        /// </summary>
        public Size MergeFormSize
        {
            get
            {
                return GetSizeFromString(_appSettings.Settings["MergeFormSize"].Value);
            }

            set
            {
                _appSettings.Settings["MergeFormSize"].Value = GetStringFromSize(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Пропорции элементов на форме для мержа двух баз
        /// </summary>
        public string MergeFormLabelMoveTop
        {
            get
            {
                return _appSettings.Settings["MergeFormLabelMoveTop"].Value;
            }

            set
            {
                _appSettings.Settings["MergeFormLabelMoveTop"].Value = value;
                _config.Save();
            }
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
        /// Расположение формы с информацией о раскраске строк
        /// </summary>
        public Point ColorInfoFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["ColorInfoFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["ColorInfoFormLocation"].Value = GetStringFromPoint(value);
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
        /// Расположение формы с отличиями в объектах при мерже
        /// </summary>
        public Point MergeShowDifferenceFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["MergeShowDifferenceFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["MergeShowDifferenceFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы с информацией по пациенту
        /// </summary>
        public Point DictophoneFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["DictophoneFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["DictophoneFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }

        
        /// <summary>
        /// Расположение формы с информацией о госпитализации
        /// </summary>
        public Point HospitalizationViewFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["HospitalizationViewFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["HospitalizationViewFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы с информацией о консультации
        /// </summary>
        public Point VisitViewFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["VisitViewFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["VisitViewFormLocation"].Value = GetStringFromPoint(value);
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
        /// Расположение формы с анамнезом
        /// </summary>
        public Point AnamneseFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["AnamneseFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["AnamneseFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы с акушерским анамнезом
        /// </summary>
        public Point ObstetricHistoryFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["ObstetricHistoryFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["ObstetricHistoryFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы с двумя картинками
        /// </summary>
        public Point PaintDoublePictureFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["PaintDoublePictureFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["PaintDoublePictureFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы с объёмом движений
        /// </summary>
        public Point RangeOfMotionCardFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["RangeOfMotionCardFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["RangeOfMotionCardFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы с объёмом движений
        /// </summary>
        public Point ObstetricParalysisCardFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["ObstetricParalysisCardFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["ObstetricParalysisCardFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Расположение формы для мержа данных из двух баз
        /// </summary>
        public Point MergeFormLocation
        {
            get
            {
                return GetPointFromString(_appSettings.Settings["MergeFormLocation"].Value);
            }

            set
            {
                _appSettings.Settings["MergeFormLocation"].Value = GetStringFromPoint(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Ширина колонок в таблице со списком пациентов
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
        /// Ширина колонок в таблице со списком операций
        /// </summary>
        public string HospitalizationViewFormListWidths
        {
            get
            {
                return _appSettings.Settings["HospitalizationViewFormListWidths"].Value;
            }

            set
            {
                _appSettings.Settings["HospitalizationViewFormListWidths"].Value = value;
                _config.Save();
            }
        }


        /// <summary>
        /// Путь до папки с данными внешней базы данных
        /// </summary>
        public string ForeignDataFolderPath
        {
            get
            {
                return _appSettings.Settings["ForeignDataFolderPath"].Value;
            }

            set
            {
                _appSettings.Settings["ForeignDataFolderPath"].Value = value;
                _config.Save();
            }
        }


        /// <summary>
        /// Форма мержа данных из двух баз. Надо ли отображать различия в личных данных пациентов
        /// </summary>
        public bool MergeFormCheckBoxShowPrivateFolderDiffs
        {
            get
            {
                return Convert.ToBoolean(_appSettings.Settings["MergeFormCheckBoxShowPrivateFolderDiffs"].Value);
            }

            set
            {
                _appSettings.Settings["MergeFormCheckBoxShowPrivateFolderDiffs"].Value = value.ToString();
                _config.Save();
            }
        }
        

        /// <summary>
        /// Форма мержа данных из двух баз. Надо ли копировать личные файлы при добавлении пациента
        /// </summary>
        public bool MergeFormCheckBoxCopyPrivateFolderData
        {
            get
            {
                return Convert.ToBoolean(_appSettings.Settings["MergeFormCheckBoxCopyPrivateFolderData"].Value);
            }

            set
            {
                _appSettings.Settings["MergeFormCheckBoxCopyPrivateFolderData"].Value = value.ToString();
                _config.Save();
            }
        }

        
        /// <summary>
        /// Цвет пациента, находящегося на лечении в стационаре
        /// </summary>
        public Color RowLightColor
        {
            get
            {
                return GetColorFromString(_appSettings.Settings["RowLightColor"].Value);
            }

            set
            {
                _appSettings.Settings["RowLightColor"].Value = GetStringFromColor(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Цвет пациента, выписывающегося сегодня
        /// </summary>
        public Color RowReleaseDateColor
        {
            get
            {
                return GetColorFromString(_appSettings.Settings["RowReleaseDateColor"].Value);
            }

            set
            {
                _appSettings.Settings["RowReleaseDateColor"].Value = GetStringFromColor(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Цвет выписанного пациента
        /// </summary>
        public Color RowNoColor
        {
            get
            {
                return GetColorFromString(_appSettings.Settings["RowNoColor"].Value);
            }

            set
            {
                _appSettings.Settings["RowNoColor"].Value = GetStringFromColor(value);
                _config.Save();
            }
        }


        /// <summary>
        /// Цвет пациента, которому надо написать этапный эпикриз
        /// </summary>
        public Color RowLineOfCommunicationColor
        {
            get
            {
                return GetColorFromString(_appSettings.Settings["RowLineOfCommunicationColor"].Value);
            }

            set
            {
                _appSettings.Settings["RowLineOfCommunicationColor"].Value = GetStringFromColor(value);
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
