using System;
using System.Diagnostics;

using SurgeryHelper.Forms;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Информация о госпитализации
    /// </summary>
    [DebuggerDisplay("id={Id} PatientId={PatientId} Diagnose={DiagnoseOneLine}")]
    public class CHospitalization : CIdEssence
    {
        public HospitalizationViewForm OpenedHospitalizationViewForm;

        /// <summary>
        /// Ссылка на id пациента, к которому относится данная госпитализация
        /// </summary>
        public int PatientId;

        /// <summary>
        /// Дата и время поступления
        /// </summary>
        public DateTime DeliveryDate;

        /// <summary>
        /// Дата выписки
        /// </summary>
        public DateTime? ReleaseDate;

        /// <summary>
        /// Номер истории болезни
        /// </summary>
        public string NumberOfCaseHistory;

        /// <summary>
        /// Диагноз пациента
        /// </summary>
        public string Diagnose;        

        /// <summary>
        /// Диагноз пациента без переносов строк
        /// </summary>
        public string DiagnoseOneLine
        {
            get
            {
                if (!string.IsNullOrEmpty(Diagnose))
                {
                    return Diagnose.Replace("\r\n", " ");
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Лечащий врач
        /// </summary>
        public string DoctorInChargeOfTheCase;

        /// <summary>
        /// Название папки с фотографиями
        /// </summary>
        public string FotoFolderName;

        /// <summary>
        /// к/д
        /// </summary>
        public string KD
        {
            get
            {
                int tempKD;
                if (ReleaseDate.HasValue && CCompareEngine.CompareDate(ReleaseDate.Value.Date, DateTime.Now.Date) <= 0)
                {
                    tempKD = (ReleaseDate.Value.Date - DeliveryDate.Date).Days;
                }
                else
                {
                    tempKD = (DateTime.Now.Date - DeliveryDate.Date).Days;
                }

                if (NumberOfCaseHistory.ToLower().Contains("д"))
                {
                    return (tempKD + 1).ToString();
                }

                return tempKD.ToString();
            }
        }

        public CHospitalization()
            : this(0, 0)
        {
        }

        public CHospitalization(int hospitalizationId, int patientId)
        {
            Id = hospitalizationId;
            PatientId = patientId;
            DeliveryDate = DateTime.Now;
        }

        public CHospitalization(CHospitalization hospitalization)
        {
            Id = hospitalization.Id;
            PatientId = hospitalization.PatientId;
            Diagnose = hospitalization.Diagnose;
            DoctorInChargeOfTheCase = hospitalization.DoctorInChargeOfTheCase;
            FotoFolderName = hospitalization.FotoFolderName;
            NumberOfCaseHistory = hospitalization.NumberOfCaseHistory;
            DeliveryDate = CConvertEngine.CopyDateTime(hospitalization.DeliveryDate);
            if (hospitalization.ReleaseDate.HasValue)
            {
                ReleaseDate = CConvertEngine.CopyDateTime(hospitalization.ReleaseDate.Value);
            }
            else
            {
                ReleaseDate = null;
            }
        }


        public static int Compare(CHospitalization hospitalization1, CHospitalization hospitalization2)
        {
            return CCompareEngine.CompareDateTime(hospitalization1.DeliveryDate, hospitalization2.DeliveryDate);
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string parameterName,
            string ownValue,
            string foreignValue,
            CHospitalization diffHospitalization,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. Дата госпитализации: '{2}'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";
            
            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnPatient = PatientId,
                IdOwnHospitalization = Id,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, CConvertEngine.DateTimeToString(DeliveryDate, true), parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignPatient = diffHospitalization.PatientId,
                IdForeignHospitalization = diffHospitalization.Id,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, CConvertEngine.DateTimeToString(diffHospitalization.DeliveryDate, true), parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной госпитализацией
        /// </summary>
        /// <param name="diffHospitalization">Импортируемая госпитализация</param>
        /// <param name="patientFio">ФИО импортируемого пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(CHospitalization diffHospitalization, string patientFio, string nosology, CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (Diagnose != diffHospitalization.Diagnose)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationDiagnose,
                   patientFio,
                   nosology,
                   "Диагноз",
                   Diagnose,
                   diffHospitalization.Diagnose,
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (DoctorInChargeOfTheCase != diffHospitalization.DoctorInChargeOfTheCase)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationDoctorInChargeOfTheCase,
                   patientFio,
                   nosology,
                   "Лечащий врач",
                   DoctorInChargeOfTheCase,
                   diffHospitalization.DoctorInChargeOfTheCase,
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (FotoFolderName != diffHospitalization.FotoFolderName)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationFotoFolderName,
                   patientFio,
                   nosology,
                   "Название папки с фотографиями",
                   FotoFolderName,
                   diffHospitalization.FotoFolderName,
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (NumberOfCaseHistory != diffHospitalization.NumberOfCaseHistory)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationNumberOfCaseHistory,
                   patientFio,
                   nosology,
                   "Номер истории болезни",
                   NumberOfCaseHistory,
                   diffHospitalization.NumberOfCaseHistory,
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ReleaseDate.HasValue && diffHospitalization.ReleaseDate.HasValue &&
                CCompareEngine.CompareDate(ReleaseDate.Value, diffHospitalization.ReleaseDate.Value) != 0)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationReleaseDate,
                   patientFio,
                   nosology,
                   "Дата выписки",
                    CConvertEngine.DateTimeToString(ReleaseDate.Value),
                    CConvertEngine.DateTimeToString(diffHospitalization.ReleaseDate.Value),
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (ReleaseDate.HasValue && !diffHospitalization.ReleaseDate.HasValue)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationReleaseDate,
                   patientFio,
                   nosology,
                   "Дата выписки",
                   CConvertEngine.DateTimeToString(ReleaseDate.Value),
                   "Нет значения",
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (!ReleaseDate.HasValue && diffHospitalization.ReleaseDate.HasValue)
            {
                CreateMergeInfos(
                   ObjectType.HospitalizationReleaseDate,
                   patientFio,
                   nosology,
                   "Дата выписки",
                   "Нет значения",
                   CConvertEngine.DateTimeToString(diffHospitalization.ReleaseDate.Value),
                   diffHospitalization,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
