using System;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Переводной эпикриз
    /// </summary>
    public class CTransferableEpicrisis
    {
        /// <summary>
        /// Указатель на id госпитализации, к которой относится данный переводной эпикриз
        /// </summary>
        public int HospitalizationId;

        /// <summary>
        /// Дополнительная информация для переводного эпикриза
        /// </summary>
        public string AdditionalInfo;

        /// <summary>
        /// Послеоперационный период для переводного  эпикриза
        /// </summary>
        public string AfterOperationPeriod;

        /// <summary>
        /// Планирующиеся действия для переводного  эпикриза
        /// </summary>
        public string Plan;

        /// <summary>
        /// Дата написания документа для переводного эпикриза
        /// </summary>
        public DateTime WritingDate;

        /// <summary>
        /// Личный номер для переводного эпикриза
        /// </summary>
        public string DisabilityList;

        /// <summary>
        /// Включать ли личный номер в отчёт
        /// </summary>
        public bool IsIncludeDisabilityList;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CTransferableEpicrisis()
            : this(0)
        {
        }

        public CTransferableEpicrisis(int hospitalizationId)
        {
            HospitalizationId = hospitalizationId;
            AfterOperationPeriod = "без особенностей";
            Plan = "перевязки до заживления ран, ЛФК";
            WritingDate = DateTime.Now;
            AdditionalInfo = string.Empty;
            DisabilityList = string.Empty;
        }

        public CTransferableEpicrisis(CTransferableEpicrisis transferEpicris)
        {
            HospitalizationId = transferEpicris.HospitalizationId;
            AfterOperationPeriod = transferEpicris.AfterOperationPeriod;
            Plan = transferEpicris.Plan;
            WritingDate = CConvertEngine.CopyDateTime(transferEpicris.WritingDate);
            AdditionalInfo = transferEpicris.AdditionalInfo;
            DisabilityList = transferEpicris.DisabilityList;
            IsIncludeDisabilityList = transferEpicris.IsIncludeDisabilityList;
            NotInDatabase = transferEpicris.NotInDatabase;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string parameterName,
            string ownValue,
            string foreignValue,
            CTransferableEpicrisis diffTransferableEpicrisis,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'.  Дата госпитализации: '{2}'. Объект: 'Переводной эпикриз'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnHospitalization = HospitalizationId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignHospitalization = diffTransferableEpicrisis.HospitalizationId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и 
        /// переданным переводным эпикризом
        /// </summary>
        /// <param name="diffTransferableEpicrisis">Импортируемый переводной эпикриз</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CTransferableEpicrisis diffTransferableEpicrisis, 
            string patientFio, 
            string nosology, 
            string hospitalizationDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;
            
            if (AfterOperationPeriod != diffTransferableEpicrisis.AfterOperationPeriod)
            {
                CreateMergeInfos(
                   ObjectType.TransferableEpicrisisAfterOperationPeriod,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Послеоперационный период",
                   AfterOperationPeriod,
                   diffTransferableEpicrisis.AfterOperationPeriod,
                   diffTransferableEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Plan != diffTransferableEpicrisis.Plan)
            {
                CreateMergeInfos(
                   ObjectType.TransferableEpicrisisPlan,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Планирующиеся действия",
                   Plan,
                   diffTransferableEpicrisis.Plan,
                   diffTransferableEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(WritingDate, diffTransferableEpicrisis.WritingDate) != 0)
            {
                CreateMergeInfos(
                   ObjectType.TransferableEpicrisisWritingDate,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дата написания документа",
                   CConvertEngine.DateTimeToString(WritingDate),
                   CConvertEngine.DateTimeToString(diffTransferableEpicrisis.WritingDate),
                   diffTransferableEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (AdditionalInfo != diffTransferableEpicrisis.AdditionalInfo)
            {
                CreateMergeInfos(
                   ObjectType.TransferableEpicrisisAdditionalInfo,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дополнительная информация",
                   AdditionalInfo,
                   diffTransferableEpicrisis.AdditionalInfo,
                   diffTransferableEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (DisabilityList != diffTransferableEpicrisis.DisabilityList)
            {
                CreateMergeInfos(
                   ObjectType.TransferableEpicrisisDisabilityList,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Личный номер",
                   DisabilityList,
                   diffTransferableEpicrisis.DisabilityList,
                   diffTransferableEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsIncludeDisabilityList != diffTransferableEpicrisis.IsIncludeDisabilityList)
            {
                CreateMergeInfos(
                   ObjectType.TransferableEpicrisisIsIncludeDisabilityList,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Включать ли личный номер в отчёт",
                   IsIncludeDisabilityList.ToString(),
                   diffTransferableEpicrisis.IsIncludeDisabilityList.ToString(),
                   diffTransferableEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
