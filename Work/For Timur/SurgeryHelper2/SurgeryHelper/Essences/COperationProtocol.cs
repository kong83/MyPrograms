using System;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Протокол операции
    /// </summary>
    public class COperationProtocol
    {
        /// <summary>
        /// Указатель на id операции, к которой относится данный операционный протокол
        /// </summary>
        public int OperationId;

        /// <summary>
        /// Обследование пациента
        /// </summary>
        public string TreatmentPlanInspection;

        /// <summary>
        /// Дата написания плана обследования
        /// </summary>
        public DateTime TreatmentPlanDate;

        /// <summary>
        /// Активен ли план обследования в операционном протоколе
        /// </summary>
        public bool IsTreatmentPlanActiveInOperationProtocol;

        /// <summary>
        /// Время написания эпискриза
        /// </summary>
        public DateTime TimeWriting;

        /// <summary>
        /// Активен ли дневник
        /// </summary>
        public bool IsDairyEnabled;

        /// <summary>
        /// Температура тела
        /// </summary>
        public string Temperature;

        /// <summary>
        /// Жалобы пациента
        /// </summary>
        public string Complaints;

        /// <summary>
        /// Состояние пациента
        /// </summary>
        public string State;

        /// <summary>
        /// Пульс пациента
        /// </summary>
        public int Pulse;

        /// <summary>
        /// Первое значение АД
        /// </summary>
        public int ADFirst;

        /// <summary>
        /// Второе значение АД
        /// </summary>
        public int ADSecond;

        /// <summary>
        /// ЧДД пациента
        /// </summary>
        public int ChDD;

        /// <summary>
        /// Дыхание пациента
        /// </summary>
        public string Breath;

        /// <summary>
        /// Хрипы пациента
        /// </summary>
        public string Wheeze;

        /// <summary>
        /// Тоны сердца
        /// </summary>
        public string HeartSounds;

        /// <summary>
        /// Ритм сердца
        /// </summary>
        public string HeartRhythm;

        /// <summary>
        /// Живот пациента
        /// </summary>
        public string Stomach;

        /// <summary>
        /// Мочеиспускание пациента
        /// </summary>
        public string Urination;

        /// <summary>
        /// Стул пациента
        /// </summary>
        public string Stool;

        /// <summary>
        /// St. Localis
        /// </summary>
        public string StLocalis;

        /// <summary>
        /// Ход операции
        /// </summary>
        public string OperationCourse;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public COperationProtocol()
            : this(0)
        {
        }

        public COperationProtocol(int operationId)
        {
            OperationId = operationId;
            TreatmentPlanInspection = "ОАК, ОАМ, ЭКГ, биохимический анализ крови";
            IsTreatmentPlanActiveInOperationProtocol = true;
            IsDairyEnabled = true;
            Temperature = "N";
            Pulse = 76;
            ADFirst = 120;
            ADSecond = 70;
            ChDD = 18;
            Wheeze = "нет";
            Stomach = "мягкий, безболезненный";
            Urination = "свободное, регулярное";
            Stool = "регулярный, без особенностей";
            State = "удовлетворительное";
            Breath = "везикулярное";
            HeartSounds = "ясные";
            HeartRhythm = "правильный";
            Complaints = string.Empty;
            StLocalis = string.Empty;
            OperationCourse = string.Empty;
            TimeWriting = DateTime.Now;
            TreatmentPlanDate = DateTime.Now;
        }

        public COperationProtocol(COperationProtocol operationProtocol)
        {
            OperationId = operationProtocol.OperationId;
            TreatmentPlanInspection = operationProtocol.TreatmentPlanInspection;
            TreatmentPlanDate = CConvertEngine.CopyDateTime(operationProtocol.TreatmentPlanDate);
            IsTreatmentPlanActiveInOperationProtocol = operationProtocol.IsTreatmentPlanActiveInOperationProtocol;
            ADFirst = operationProtocol.ADFirst;
            ADSecond = operationProtocol.ADSecond;
            Breath = operationProtocol.Breath;
            ChDD = operationProtocol.ChDD;
            Complaints = operationProtocol.Complaints;
            State = operationProtocol.State;
            HeartRhythm = operationProtocol.HeartRhythm;
            HeartSounds = operationProtocol.HeartSounds;
            IsDairyEnabled = operationProtocol.IsDairyEnabled;
            Pulse = operationProtocol.Pulse;
            StLocalis = operationProtocol.StLocalis;
            Stomach = operationProtocol.Stomach;
            Stool = operationProtocol.Stool;
            Temperature = operationProtocol.Temperature;
            TimeWriting = CConvertEngine.CopyDateTime(operationProtocol.TimeWriting);
            Urination = operationProtocol.Urination;
            Wheeze = operationProtocol.Wheeze;
            OperationCourse = operationProtocol.OperationCourse;
            NotInDatabase = operationProtocol.NotInDatabase;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string operationName,
            string parameterName,
            string ownValue,
            string foreignValue,
            COperationProtocol diffOperationProtocol,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'.  Дата госпитализации: '{2}'. Название операции: '{3}'. Объект: 'Операционный протокол'.\r\nНазвание параметра: '{4}'. Значение: '{5}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOperation = OperationId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, operationName, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdOperation = diffOperationProtocol.OperationId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, operationName, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и переданным 
        /// протоколом операции
        /// </summary>
        /// <param name="diffOperationProtocol">Импортируемый протокол операции</param>
        /// <param name="patientFio">ФИО импортируемого пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата импортируемой госпитализации</param>
        /// <param name="operationName">Название импортируемой операции</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            COperationProtocol diffOperationProtocol,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string operationName,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (TreatmentPlanInspection != diffOperationProtocol.TreatmentPlanInspection)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolTreatmentPlanInspection,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Обследование пациента",
                   TreatmentPlanInspection,
                   diffOperationProtocol.TreatmentPlanInspection,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsTreatmentPlanActiveInOperationProtocol != diffOperationProtocol.IsTreatmentPlanActiveInOperationProtocol)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolIsTreatmentPlanActiveInOperationProtocol,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Активен ли план обследования в операционном протоколе",
                   IsTreatmentPlanActiveInOperationProtocol.ToString(),
                   diffOperationProtocol.IsTreatmentPlanActiveInOperationProtocol.ToString(),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ADFirst != diffOperationProtocol.ADFirst)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolADFirst,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Первое значение AD",
                   ADFirst.ToString(),
                   diffOperationProtocol.ADFirst.ToString(),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ADSecond != diffOperationProtocol.ADSecond)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolADSecond,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Второе значение AD",
                   ADSecond.ToString(),
                   diffOperationProtocol.ADSecond.ToString(),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Breath != diffOperationProtocol.Breath)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolBreath,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Дыхание",
                   Breath,
                   diffOperationProtocol.Breath,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ChDD != diffOperationProtocol.ChDD)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolChDD,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "ЧДД",
                   ChDD.ToString(),
                   diffOperationProtocol.ChDD.ToString(),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Complaints != diffOperationProtocol.Complaints)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolComplaints,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Жалобы",
                   Complaints,
                   diffOperationProtocol.Complaints,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (string.Compare(Complaints, diffOperationProtocol.Complaints) == 0)
            {
            }

            if (State != diffOperationProtocol.State)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolState,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Состояние",
                   State,
                   diffOperationProtocol.State,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (HeartRhythm != diffOperationProtocol.HeartRhythm)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolHeartRhythm,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Ритм сердца",
                   HeartRhythm,
                   diffOperationProtocol.HeartRhythm,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (HeartSounds != diffOperationProtocol.HeartSounds)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolHeartSounds,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Тоны сердца",
                   HeartSounds,
                   diffOperationProtocol.HeartSounds,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsDairyEnabled != diffOperationProtocol.IsDairyEnabled)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolIsDairyEnabled,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Активен ли дневник",
                   IsDairyEnabled.ToString(),
                   diffOperationProtocol.IsDairyEnabled.ToString(),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);

            }

            if (Pulse != diffOperationProtocol.Pulse)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolPulse,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Пульс",
                   Pulse.ToString(),
                   diffOperationProtocol.Pulse.ToString(),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StLocalis != diffOperationProtocol.StLocalis)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolStLocalis,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "StLocalis",
                   StLocalis,
                   diffOperationProtocol.StLocalis,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Stomach != diffOperationProtocol.Stomach)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolStomach,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Живот",
                   Stomach,
                   diffOperationProtocol.Stomach,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Stool != diffOperationProtocol.Stool)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolStool,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Стул",
                   Stool,
                   diffOperationProtocol.Stool,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Temperature != diffOperationProtocol.Temperature)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolTemperature,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Температура тела",
                   Temperature,
                   diffOperationProtocol.Temperature,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Urination != diffOperationProtocol.Urination)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolUrination,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Мочеиспускание",
                   Urination,
                   diffOperationProtocol.Urination,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Wheeze != diffOperationProtocol.Wheeze)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolWheeze,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Хрипы",
                   Wheeze,
                   diffOperationProtocol.Wheeze,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OperationCourse != diffOperationProtocol.OperationCourse)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolOperationCourse,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Ход операции",
                   OperationCourse,
                   diffOperationProtocol.OperationCourse,
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(TreatmentPlanDate, diffOperationProtocol.TreatmentPlanDate) != 0)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolTreatmentPlanDate,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Дата написание плана обследования",
                   CConvertEngine.DateTimeToString(TreatmentPlanDate),
                   CConvertEngine.DateTimeToString(diffOperationProtocol.TreatmentPlanDate),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareTime(TimeWriting, diffOperationProtocol.TimeWriting) != 0)
            {
                CreateMergeInfos(
                   ObjectType.OperationProtocolTimeWriting,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   operationName,
                   "Время написания эпикриза",
                   CConvertEngine.TimeToString(TimeWriting),
                   CConvertEngine.TimeToString(diffOperationProtocol.TimeWriting),
                   diffOperationProtocol,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
