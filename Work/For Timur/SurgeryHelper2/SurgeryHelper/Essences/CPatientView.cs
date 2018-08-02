using System;
using System.Drawing;
using System.Globalization;
using SurgeryHelper.Tools;
using System.Text;

namespace SurgeryHelper.Essences
{
    public class CPatientView
    {
        /// <summary>
        /// Уникальный эдентификатор пациента
        /// </summary>
        public string Id;

        /// <summary>
        /// Нозология пациента
        /// </summary>
        public string Nosology;

        /// <summary>
        /// Полное имя пациента
        /// </summary>
        public string FullName;

        /// <summary>
        /// Возраст
        /// </summary>
        public string Age;

        /// <summary>
        /// Диагноз из последней госпитализации, если она есть
        /// </summary>
        public string Diagnose;

        /// <summary>
        /// Дата последнего консультации, если он есть
        /// </summary>
        public string VisitDateString;

        /// <summary>
        /// Дата поступления из последней госпитализации, если она есть
        /// </summary>
        public string DeliveryDateString;

        /// <summary>
        /// Дата выписки из последней госпитализации, если она есть
        /// </summary>
        public string ReleaseDateString;

        /// <summary>
        /// Срок нахождения в больнице из последней госпитализации, если она есть
        /// </summary>
        public string KD;

        /// <summary>
        /// Количество госпитализаций
        /// </summary>
        public string HospitalizationCnt;

        /// <summary>
        /// Количество консультаций
        /// </summary>
        public string VisitCnt;

        /// <summary>
        /// Количество операций
        /// </summary>
        public string OperationCnt;

        /// <summary>
        /// Типы операций, через запятую
        /// </summary>
        public string OperationTypes;

        /// <summary>
        /// Цвет строки в списке пациентов
        /// </summary>
        public Color RowColor;

        /// <summary>
        /// Надо ли писать этапный эпикриз
        /// </summary>
        public bool IsNeedWriteLineOfCommEpicris;

        public CPatientView(CPatient patientInfo, CWorkersKeeper workersKeeper)
        {
            Id = patientInfo.Id.ToString(CultureInfo.InvariantCulture);
            Nosology = patientInfo.Nosology;
            FullName = patientInfo.GetFullName();            
            HospitalizationCnt = workersKeeper.HospitalizationWorker.GetCountByPatientId(patientInfo.Id).ToString(CultureInfo.InvariantCulture);
            VisitCnt = workersKeeper.VisitWorker.GetCountByPatientId(patientInfo.Id).ToString(CultureInfo.InvariantCulture);
            COperation[] operations = workersKeeper.OperationWorker.GetListByPatientId(patientInfo.Id);
            OperationCnt = operations.Length.ToString(CultureInfo.InvariantCulture);
            Age = CConvertEngine.GetAge(patientInfo.Birthday);

            var operationTypeSB = new StringBuilder();
            foreach (COperation operation in operations)
            {
                foreach (string operationType in operation.OperationTypes)
                {
                    if (!operationTypeSB.ToString().Contains(operationType))
                    {
                        operationTypeSB.Append(operationType + ", ");
                    }
                }                
            }

            OperationTypes = operationTypeSB.Length > 0
                ? operationTypeSB.ToString().Substring(0, operationTypeSB.Length - 2)
                : operationTypeSB.ToString();
            
            int hospitalizationCnt = Convert.ToInt32(HospitalizationCnt);
            int visitCnt = Convert.ToInt32(VisitCnt);
            CVisit lastVisit = null;
            if (visitCnt > 0)
            {
                lastVisit = workersKeeper.VisitWorker.GetListByPatientId(patientInfo.Id)[visitCnt - 1];
            }

            CHospitalization lastHospitalization = null;
            if (hospitalizationCnt > 0)
            {
                lastHospitalization = workersKeeper.HospitalizationWorker.GetListByPatientId(patientInfo.Id)[hospitalizationCnt - 1];
            }
 
            if (lastHospitalization != null)
            {                
                DeliveryDateString = CConvertEngine.DateTimeToString(lastHospitalization.DeliveryDate, true);

                ReleaseDateString = CConvertEngine.DateTimeToString(lastHospitalization.ReleaseDate, false);

                KD = lastHospitalization.KD;

                if (lastVisit == null)
                {
                    Diagnose = lastHospitalization.DiagnoseOneLine;
                }
            }

            if (lastVisit != null)
            {
                VisitDateString = CConvertEngine.DateTimeToString(lastVisit.VisitDate, true);
            }

            if (lastHospitalization == null && lastVisit != null)
            {
                Diagnose = lastVisit.DiagnoseOneLine;
            }
            else if (lastHospitalization != null && lastVisit != null)
            {
                int dateCompareResult = CCompareEngine.CompareDateTime(lastHospitalization.DeliveryDate, lastVisit.VisitDate);
                Diagnose = dateCompareResult >= 0 
                    ? lastHospitalization.DiagnoseOneLine 
                    : lastVisit.DiagnoseOneLine;
            }
        }

        public static int CompareByName(CPatientView patientInfo1, CPatientView patientInfo2)
        {
            return string.CompareOrdinal(patientInfo1.FullName, patientInfo2.FullName);
        }

        public static int CompareById(CPatientView patientInfo1, CPatientView patientInfo2)
        {
            int id1 = Convert.ToInt32(patientInfo1.Id);
            int id2 = Convert.ToInt32(patientInfo2.Id);
            if(id1 > id2)
            {
                return 1;
            }

            if(id1 < id2)
            {
                return -1;
            }

            return CompareByName(patientInfo1, patientInfo2);
        }
        
        private static int CompareDateString(string date1Str, string date2Str, CPatientView patientInfo1, CPatientView patientInfo2)
        {
            if (!string.IsNullOrEmpty(date1Str) && !string.IsNullOrEmpty(date2Str))
            {
                try
                {
                    DateTime dateTime1 = DateTime.Parse(date1Str);
                    DateTime dateTime2 = DateTime.Parse(date2Str);
                    int res = DateTime.Compare(dateTime1, dateTime2);
                    if (res != 0)
                    {
                        return res;
                    }

                    return CompareByName(patientInfo1, patientInfo2);
                }
                catch
                {
                    return string.CompareOrdinal(date1Str, date2Str);
                }
            }

            if (!string.IsNullOrEmpty(date1Str))
            {
                return 1;
            }

            if (!string.IsNullOrEmpty(date2Str))
            {
                return -1;
            }

            return CompareByName(patientInfo1, patientInfo2);
        }

        public static int CompareByDeliveryDate(CPatientView patientInfo1, CPatientView patientInfo2)
        {
            return CompareDateString(patientInfo1.DeliveryDateString, patientInfo2.DeliveryDateString, patientInfo1, patientInfo2);
        }

        public static int CompareByVisitDate(CPatientView patientInfo1, CPatientView patientInfo2)
        {
            return CompareDateString(patientInfo1.VisitDateString, patientInfo2.VisitDateString, patientInfo1, patientInfo2);
        }
    }
}
