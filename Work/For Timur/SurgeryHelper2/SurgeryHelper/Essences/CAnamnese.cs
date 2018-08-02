using System;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Анамнез
    /// </summary>
    public class CAnamnese
    {
        /// <summary>
        /// Указатель на id пациента, к которой относится данный анамнез
        /// </summary>
        public int PatientId;

        /// <summary>
        /// Описание анамнеза
        /// </summary>
        public string AnMorbi;

        /// <summary>
        /// Дата травмы, если есть
        /// </summary>
        public DateTime? TraumaDate;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CAnamnese()
            : this(0)
        {
        }

        public CAnamnese(int patientId)
        {
            PatientId = patientId;
        }

        public CAnamnese(CAnamnese anamnese)
        {
            PatientId = anamnese.PatientId;
            AnMorbi = anamnese.AnMorbi;
            TraumaDate = CConvertEngine.CopyDateTime(anamnese.TraumaDate);
            NotInDatabase = anamnese.NotInDatabase;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string parameterName,
            string ownValue,
            string foreignValue,
            CAnamnese diffAnamnese,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. Объект: 'Анамнез'.\r\nНазвание параметра: '{2}'. Значение: '{3}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnPatient = PatientId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignPatient = diffAnamnese.PatientId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и 
        /// переданным анамнезом
        /// </summary>
        /// <param name="diffAnamnese">Импортируемый анамнез</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(CAnamnese diffAnamnese, string patientFio, string nosology, CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (AnMorbi != diffAnamnese.AnMorbi)
            {
                CreateMergeInfos(
                   ObjectType.AnamneseAnMorbi,
                   patientFio,
                   nosology,
                   "Описание анамнеза",
                   AnMorbi,
                   diffAnamnese.AnMorbi,
                   diffAnamnese,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (TraumaDate.HasValue && !diffAnamnese.TraumaDate.HasValue)
            {
                CreateMergeInfos(
                   ObjectType.AnamneseTraumaDate,
                   patientFio,
                   nosology,
                   "Дата травмы",
                   CConvertEngine.DateTimeToString(TraumaDate.Value, false),
                   "Нет значения",
                   diffAnamnese,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (!TraumaDate.HasValue && diffAnamnese.TraumaDate.HasValue)
            {
                CreateMergeInfos(
                   ObjectType.AnamneseTraumaDate,
                   patientFio,
                   nosology,
                   "Дата травмы",
                   "Нет значения",
                   CConvertEngine.DateTimeToString(diffAnamnese.TraumaDate.Value, false),
                   diffAnamnese,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (TraumaDate.HasValue && diffAnamnese.TraumaDate.HasValue &&
                CCompareEngine.CompareDate(TraumaDate.Value, diffAnamnese.TraumaDate.Value) != 0)
            {
                CreateMergeInfos(
                   ObjectType.AnamneseTraumaDate,
                   patientFio,
                   nosology,
                   "Дата травмы",
                   CConvertEngine.DateTimeToString(TraumaDate.Value, false),
                   CConvertEngine.DateTimeToString(diffAnamnese.TraumaDate.Value, false),
                   diffAnamnese,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
