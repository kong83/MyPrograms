using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Акушерский анамнез
    /// </summary>
    public class CObstetricHistory
    {
        /// <summary>
        /// Указатель на id пациента, к которой относится данный анамнез
        /// </summary>
        public int PatientId;

        /// <summary>
        /// Роды в срок (количество недель)
        /// </summary>
        public string ChildbirthWeeks;

        /// <summary>
        /// Предлежание
        /// </summary>
        public string Fetal;

        /// <summary>
        /// Осложнения в период беременности
        /// </summary>
        public string ComplicationsInPregnancy;

        /// <summary>
        /// Лекарственные препараты и хронические интоксикации в период беременности
        /// </summary>
        public string DrugsInPregnancy;

        /// <summary>
        /// Длительность родов (в часах)
        /// </summary>
        public string DurationOfLabor;

        /// <summary>
        /// Родовая травма
        /// </summary>
        public string BirthInjury;

        /// <summary>
        /// Осложнения в ходе родов
        /// </summary>
        public string ComplicationsDuringChildbirth;

        /// <summary>
        /// Использование щипцов в ходе родов
        /// </summary>
        public bool IsTongsUsing;

        /// <summary>
        /// Использование вакуума в ходе родов
        /// </summary>
        public bool IsVacuumUsing;

        /// <summary>
        /// Шкала Апгар (баллов)
        /// </summary>
        public string ApgarScore;

        /// <summary>
        /// Вес при рождении (г)
        /// </summary>
        public string WeightAtBirth;

        /// <summary>
        /// Рост при рождении (см)
        /// </summary>
        public string HeightAtBirth;

        /// <summary>
        /// Когда и кем диагностирован акушерский паралич
        /// </summary>
        public string ObstetricParalysis;

        /// <summary>
        /// Стационарное лечение (даты госпитализаций) и проводимое лечение (включая операции)
        /// </summary>
        public string HospitalTreatment;

        /// <summary>
        /// Амбулаторное лечение (разработка объема пассивных движений, лечебная физкультура, шинирование и т.д.)
        /// </summary>
        public string OutpatientCare;

        /// <summary>
        /// Хронология восстановления активных движений верхней конечности
        /// </summary>
        public bool[] Chronology;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CObstetricHistory()
            : this(0)
        {
        }

        public CObstetricHistory(int patientId)
        {
            PatientId = patientId;
            Fetal = "головное";
            Chronology = new bool[42];
        }

        public CObstetricHistory(CObstetricHistory obstetricHistory)
        {
            PatientId = obstetricHistory.PatientId;
            ApgarScore = obstetricHistory.ApgarScore;
            BirthInjury = obstetricHistory.BirthInjury;
            ChildbirthWeeks = obstetricHistory.ChildbirthWeeks;
            ComplicationsDuringChildbirth = obstetricHistory.ComplicationsDuringChildbirth;
            ComplicationsInPregnancy = obstetricHistory.ComplicationsInPregnancy;
            DrugsInPregnancy = obstetricHistory.DrugsInPregnancy;
            DurationOfLabor = obstetricHistory.DurationOfLabor;
            Fetal = obstetricHistory.Fetal;
            HeightAtBirth = obstetricHistory.HeightAtBirth;
            HospitalTreatment = obstetricHistory.HospitalTreatment;
            IsTongsUsing = obstetricHistory.IsTongsUsing;
            IsVacuumUsing = obstetricHistory.IsVacuumUsing;
            ObstetricParalysis = obstetricHistory.ObstetricParalysis;
            OutpatientCare = obstetricHistory.OutpatientCare;
            WeightAtBirth = obstetricHistory.WeightAtBirth;
            Chronology = CConvertEngine.CopyArray(obstetricHistory.Chronology);
            NotInDatabase = obstetricHistory.NotInDatabase;
        }


        private void CreateMergeInfos(
           ObjectType objectType,
           string patientFio,
           string nosology,
           string parameterName,
           string ownValue,
           string foreignValue,
           CObstetricHistory diffObstetricHistory,
           out CMergeInfo ownPatientMergeInfo,
           out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. Объект: 'Акушерский анамнез'.\r\nНазвание параметра: '{2}'. Значение: '{3}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnPatient = PatientId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignPatient = diffObstetricHistory.PatientId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, parameterName, foreignValue)
            };
        }

        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и 
        /// переданным анамнезом
        /// </summary>
        /// <param name="diffObstetricHistory">Импортируемый анамнез</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CObstetricHistory diffObstetricHistory, string patientFio, string nosology, CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (ApgarScore != diffObstetricHistory.ApgarScore)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryApgarScore,
                   patientFio,
                   nosology,
                   "Шкала Апгар",
                   ApgarScore,
                   diffObstetricHistory.ApgarScore,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (BirthInjury != diffObstetricHistory.BirthInjury)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryBirthInjury,
                   patientFio,
                   nosology,
                   "Родовая травма",
                   BirthInjury,
                   diffObstetricHistory.BirthInjury,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ChildbirthWeeks != diffObstetricHistory.ChildbirthWeeks)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryChildbirthWeeks,
                   patientFio,
                   nosology,
                   "Роды в срок",
                   ChildbirthWeeks,
                   diffObstetricHistory.ChildbirthWeeks,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ComplicationsDuringChildbirth != diffObstetricHistory.ComplicationsDuringChildbirth)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryComplicationsDuringChildbirth,
                   patientFio,
                   nosology,
                   "Осложнения в ходе родов",
                   ComplicationsDuringChildbirth,
                   diffObstetricHistory.ComplicationsDuringChildbirth,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ComplicationsInPregnancy != diffObstetricHistory.ComplicationsInPregnancy)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryComplicationsInPregnancy,
                   patientFio,
                   nosology,
                   "Осложнения в период беременности",
                   ComplicationsInPregnancy,
                   diffObstetricHistory.ComplicationsInPregnancy,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (DrugsInPregnancy != diffObstetricHistory.DrugsInPregnancy)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryDrugsInPregnancy,
                   patientFio,
                   nosology,
                   "Лекарственные препараты и хронические интоксикации в период беременности",
                   DrugsInPregnancy,
                   diffObstetricHistory.DrugsInPregnancy,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (DurationOfLabor != diffObstetricHistory.DurationOfLabor)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryDurationOfLabor,
                   patientFio,
                   nosology,
                   "Длительности родов",
                   DurationOfLabor,
                   diffObstetricHistory.DurationOfLabor,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Fetal != diffObstetricHistory.Fetal)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryFetal,
                   patientFio,
                   nosology,
                   "Предлежание",
                   Fetal,
                   diffObstetricHistory.Fetal,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (HeightAtBirth != diffObstetricHistory.HeightAtBirth)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryHeightAtBirth,
                   patientFio,
                   nosology,
                   "Рост при рождении",
                   HeightAtBirth,
                   diffObstetricHistory.HeightAtBirth,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (HospitalTreatment != diffObstetricHistory.HospitalTreatment)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryHospitalTreatment,
                   patientFio,
                   nosology,
                   "Стационарное лечение",
                   HospitalTreatment,
                   diffObstetricHistory.HospitalTreatment,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsTongsUsing != diffObstetricHistory.IsTongsUsing)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryIsTongsUsing,
                   patientFio,
                   nosology,
                   "Использование щипцов в ходе родов",
                   IsTongsUsing.ToString(),
                   diffObstetricHistory.IsTongsUsing.ToString(),
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsVacuumUsing != diffObstetricHistory.IsVacuumUsing)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryIsVacuumUsing,
                   patientFio,
                   nosology,
                   "Использование ваккума в ходе родов",
                   IsVacuumUsing.ToString(),
                   diffObstetricHistory.IsVacuumUsing.ToString(),
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ObstetricParalysis != diffObstetricHistory.ObstetricParalysis)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryObstetricParalysis,
                   patientFio,
                   nosology,
                   "Кем и когда диагностирован акушерский паралич",
                   ObstetricParalysis,
                   diffObstetricHistory.ObstetricParalysis,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OutpatientCare != diffObstetricHistory.OutpatientCare)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryOutpatientCare,
                   patientFio,
                   nosology,
                   "Амбулаторное лечение",
                   OutpatientCare,
                   diffObstetricHistory.OutpatientCare,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (WeightAtBirth != diffObstetricHistory.WeightAtBirth)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryWeightAtBirth,
                   patientFio,
                   nosology,
                   "Вес при рождении",
                   WeightAtBirth,
                   diffObstetricHistory.WeightAtBirth,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            string ownValue;
            string foreignValue;
            if (!CCompareEngine.IsArraysEqual(Chronology, diffObstetricHistory.Chronology, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricHistoryChronology,
                   patientFio,
                   nosology,
                   "Хронология восстановления активных движений верхней конечности",
                   ownValue,
                   foreignValue,
                   diffObstetricHistory,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Chronology;
                foreignPatientMergeInfo.Object = diffObstetricHistory.Chronology;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
