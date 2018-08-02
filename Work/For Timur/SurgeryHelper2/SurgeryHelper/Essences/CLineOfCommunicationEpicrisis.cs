using System;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Этапный эпикриз
    /// </summary>
    public class CLineOfCommunicationEpicrisis
    {
        /// <summary>
        /// Указатель на id госпитализации, к которой относится данный этапный эпикриз
        /// </summary>
        public int HospitalizationId;

        /// <summary>
        /// Дополнительная информация для этапного эпикриза
        /// </summary>
        public string AdditionalInfo;

        /// <summary>
        /// Планирующиеся действия для этапного эпикриза
        /// </summary>
        public string Plan;

        /// <summary>
        /// Дата написания документа для этапного эпикриза
        /// </summary>
        public DateTime WritingDate;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CLineOfCommunicationEpicrisis()
            : this(0)
        {
        }

        public CLineOfCommunicationEpicrisis(int hospitalizationId)
        {
            HospitalizationId = hospitalizationId;
            Plan = "перевязки до заживления ран, ЛФК";
            WritingDate = DateTime.Now;
        }

        public CLineOfCommunicationEpicrisis(CLineOfCommunicationEpicrisis lineOfCommEpicris)
        {
            HospitalizationId = lineOfCommEpicris.HospitalizationId;
            AdditionalInfo = lineOfCommEpicris.AdditionalInfo;
            Plan = lineOfCommEpicris.Plan;
            WritingDate = CConvertEngine.CopyDateTime(lineOfCommEpicris.WritingDate);
            NotInDatabase = lineOfCommEpicris.NotInDatabase;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string parameterName,
            string ownValue,
            string foreignValue,
            CLineOfCommunicationEpicrisis diffLineOfCommunicationEpicrisis,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'.  Дата госпитализации: '{2}'. Объект: 'Этапный эпикриз'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnHospitalization = HospitalizationId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignHospitalization = diffLineOfCommunicationEpicrisis.HospitalizationId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и переданным 
        /// этапным эпикризом
        /// </summary>
        /// <param name="diffLineOfCommunicationEpicrisis">Импортируемый этапный эпикриз</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CLineOfCommunicationEpicrisis diffLineOfCommunicationEpicrisis,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (AdditionalInfo != diffLineOfCommunicationEpicrisis.AdditionalInfo)
            {
                CreateMergeInfos(
                   ObjectType.LineOfCommunicationEpicrisisAdditionalInfo,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дополнительная информация",
                   AdditionalInfo,
                   diffLineOfCommunicationEpicrisis.AdditionalInfo,
                   diffLineOfCommunicationEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Plan != diffLineOfCommunicationEpicrisis.Plan)
            {
                CreateMergeInfos(
                   ObjectType.LineOfCommunicationEpicrisisPlan,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Планирующиеся действия",
                   Plan,
                   diffLineOfCommunicationEpicrisis.Plan,
                   diffLineOfCommunicationEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(WritingDate, diffLineOfCommunicationEpicrisis.WritingDate) != 0)
            {
                CreateMergeInfos(
                   ObjectType.LineOfCommunicationEpicrisisWritingDate,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дата написания документа",
                   CConvertEngine.DateTimeToString(WritingDate),
                   CConvertEngine.DateTimeToString(diffLineOfCommunicationEpicrisis.WritingDate),
                   diffLineOfCommunicationEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
