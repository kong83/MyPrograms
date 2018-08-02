using System;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Обследование в отделении
    /// </summary>
    public class CMedicalInspection
    {
        /// <summary>
        /// Указатель на id госпитализации, к которой относится данное обследование в отделении
        /// </summary>
        public int HospitalizationId;

        /// <summary>
        /// Осмотр в отделении, общие данные, включен ли план осмотра в отчёт
        /// </summary>
        public bool IsPlanEnabled;

        /// <summary>
        /// Осмотр в отделении, общие данные, обследование
        /// </summary>
        public string InspectionPlan;

        /// <summary>
        /// Осмотр в отделении, общие данные, жалобы
        /// </summary>
        public string Complaints;

        /// <summary>
        /// Осмотр в отделении, общие данные, риск ТЭО
        /// </summary>
        public string TeoRisk;

        /// <summary>
        /// Осмотр в отделении, общие данные, 1, 2 или 3 лист нетрудоспособности
        /// </summary>
        public int ExpertAnamnese;

        /// <summary>
        /// Осмотр в отделении, общие данные, выдан амбулаторно с даты
        /// </summary>
        public DateTime LnWithNumberDateStart;

        /// <summary>
        /// Осмотр в отделении, общие данные, выдан амбулаторно до даты
        /// </summary>
        public DateTime LnWithNumberDateEnd;

        /// <summary>
        /// Осмотр в отделении, общие данные, выдан первично с даты
        /// </summary>
        public DateTime LnFirstDateStart;

        /// <summary>
        /// Включён ли анамнез в общий отчёт
        /// </summary>
        public bool IsAnamneseActive;

        /// <summary>
        /// Осмотр в отделении, анамнез, AnMorbi
        /// </summary>
        public string AnamneseAnMorbi;

        /// <summary>
        /// Осмотр в отделении, анамнез, AnVitae
        /// </summary>
        public bool[] AnamneseAnVitae;

        /// <summary>
        /// Осмотр в отделении, анамнез, поля
        /// </summary>
        public string[] AnamneseTextBoxes;

        /// <summary>
        /// Осмотр в отделении, анамнез, checkbox-ы
        /// </summary>
        public bool[] AnamneseCheckboxes;

        /// <summary>
        /// Осмотр в отделении, st.praesens, текстовые поля
        /// </summary>
        public string[] StPraesensTextBoxes;

        /// <summary>
        /// Осмотр в отделении, st.praesens, комбобоксы
        /// </summary>
        public string[] StPraesensComboBoxes;

        /// <summary>
        /// Осмотр в отделении, st.praesens, числовые поля
        /// </summary>
        public int[] StPraesensNumericUpDowns;

        /// <summary>
        /// Осмотр в отделении, описание St. localis-а
        /// </summary>
        public string StLocalisDescription;

        /// <summary>
        /// Осмотр в отделении, тип рентгена
        /// </summary>
        public string StLocalisRentgen;

        /// <summary>
        /// Включён ли st.localis часть 1 в общий отчёт
        /// </summary>
        public bool IsStLocalisPart1Enabled;

        /// <summary>
        /// Осмотр в отделении, st.localis part1, поля
        /// </summary>
        public string[] StLocalisPart1Fields;

        /// <summary>
        /// Осмотр в отделении, st.localis part1, номер пальца в оппозиции
        /// </summary>
        public string StLocalisPart1OppositionFinger;

        /// <summary>
        /// Включён ли st.localis часть 2 в общий отчёт
        /// </summary>
        public bool IsStLocalisPart2Enabled;

        /// <summary>
        /// Осмотр в отделении, st.localis part2, какая рука(левая/правая/правая, левая)
        /// </summary>
        public string StLocalisPart2WhichHand;

        /// <summary>
        /// Осмотр в отделении, st.localis part2, текст боксы
        /// </summary>
        public string[] StLocalisPart2TextBoxes;

        /// <summary>
        /// Осмотр в отделении, st.localis part2, комбобоксы
        /// </summary>
        public string[] StLocalisPart2ComboBoxes;

        /// <summary>
        /// Осмотр в отделении, st.localis part2, комбобоксы для левой руки
        /// </summary>
        public string[] StLocalisPart2LeftHand;

        /// <summary>
        /// Осмотр в отделении, st.localis part2, комбобоксы для правой руки
        /// </summary>
        public string[] StLocalisPart2RightHand;

        /// <summary>
        /// Осмотр в отделении, st.localis part2, числовое поле
        /// </summary>
        public int StLocalisPart2NumericUpDown;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CMedicalInspection()
            : this(0)
        {
        }

        public CMedicalInspection(int hospitalizationId)
        {
            HospitalizationId = hospitalizationId;
            ExpertAnamnese = 3;
            IsPlanEnabled = true;
            InspectionPlan = "ОАК, ОАМ, ЭКГ, биохимический анализ крови";
            TeoRisk = "отсутствует";
            ExpertAnamnese = 3;
            StLocalisRentgen = "без костной патологии";

            AnamneseAnVitae = new bool[4];
            AnamneseTextBoxes = new[]
            {
                "отрицает",
                "не болел",
                "не выполнялись",
                "не имеет",
                "нет",
                "не выполнялось",
                "не было",
                "отрицает"
            };
            AnamneseCheckboxes = new bool[12];

            StPraesensTextBoxes = new[]
            {
                "ясное",
                "активное",
                "розовые, чистые",                
                "не пальпируется",
                "не пальпируются",
                "проводится во всех отделах",
                "нет",
                "правильный",
                "76 хорошего наполнения и напряжения",
                "участвует в дыхании, мягкий, безболезненный во всех отделах",
                "сохранена",
                "нет",
                "выслушивается",
                "без патологий",
                "без особенностей",
                "безболезненная",
                "безболезненные, в полном объеме"
            };
            StPraesensComboBoxes = new[]
            {
                "удовлетворительное",
                "удовлетворительное",
                "везикулярное",
                "ясные",
            };
            StPraesensNumericUpDowns = new[]
            {
                18,
                76,
                120, 
                70
            };

            StLocalisPart1OppositionFinger = "V";
            StLocalisPart1Fields = new string[62];
            for (int i = 0; i < StLocalisPart1Fields.Length; i++)
            {
                StLocalisPart1Fields[i] = "N";
            }

            StLocalisPart2NumericUpDown = 3;
            StLocalisPart2WhichHand = "левая";
            StLocalisPart2TextBoxes = new[] 
            {
                "нет",
                "нет",
                "нет",
                "нет",
                "не исследовалась",
                "нет",
                "нет",
                string.Empty,
                "нет",
                "есть",
                "ногтевую пластинку"
            };
            StLocalisPart2ComboBoxes = new[] 
            {
                "чистая",
                "нет",
                "I",
                "угловое",
                "линейная",
                "ровные",
                "в пределах кожи",
                "нет",
                "не нарушена",
                "нет"                
            };
            StLocalisPart2LeftHand = new string[24];
            StLocalisPart2RightHand = new string[24];

            for (int i = 0; i < 22; i++)
            {
                StLocalisPart2LeftHand[i] =
                StLocalisPart2RightHand[i] = "есть";
            }

            StLocalisPart2LeftHand[22] =
            StLocalisPart2RightHand[22] = "розовый";
            StLocalisPart2LeftHand[23] =
            StLocalisPart2RightHand[23] = "теплая";
            Complaints = string.Empty;
            AnamneseAnMorbi = string.Empty;

            LnFirstDateStart = DateTime.Now;
            LnWithNumberDateEnd = DateTime.Now;
            LnWithNumberDateStart = DateTime.Now;
        }

        public CMedicalInspection(CMedicalInspection medicalInspection)
        {
            HospitalizationId = medicalInspection.HospitalizationId;
            AnamneseAnMorbi = medicalInspection.AnamneseAnMorbi;
            AnamneseAnVitae = CConvertEngine.CopyArray(medicalInspection.AnamneseAnVitae);
            AnamneseCheckboxes = CConvertEngine.CopyArray(medicalInspection.AnamneseCheckboxes);
            AnamneseTextBoxes = CConvertEngine.CopyArray(medicalInspection.AnamneseTextBoxes);
            Complaints = medicalInspection.Complaints;
            ExpertAnamnese = medicalInspection.ExpertAnamnese;
            StLocalisDescription = medicalInspection.StLocalisDescription;
            StLocalisRentgen = medicalInspection.StLocalisRentgen;
            InspectionPlan = medicalInspection.InspectionPlan;
            IsAnamneseActive = medicalInspection.IsAnamneseActive;
            IsPlanEnabled = medicalInspection.IsPlanEnabled;
            IsStLocalisPart1Enabled = medicalInspection.IsStLocalisPart1Enabled;
            IsStLocalisPart2Enabled = medicalInspection.IsStLocalisPart2Enabled;
            LnFirstDateStart = CConvertEngine.CopyDateTime(medicalInspection.LnFirstDateStart);
            LnWithNumberDateEnd = CConvertEngine.CopyDateTime(medicalInspection.LnWithNumberDateEnd);
            LnWithNumberDateStart = CConvertEngine.CopyDateTime(medicalInspection.LnWithNumberDateStart);
            StLocalisPart1Fields = CConvertEngine.CopyArray(medicalInspection.StLocalisPart1Fields);
            StLocalisPart1OppositionFinger = medicalInspection.StLocalisPart1OppositionFinger;
            StLocalisPart2ComboBoxes = CConvertEngine.CopyArray(medicalInspection.StLocalisPart2ComboBoxes);
            StLocalisPart2LeftHand = CConvertEngine.CopyArray(medicalInspection.StLocalisPart2LeftHand);
            StLocalisPart2NumericUpDown = medicalInspection.StLocalisPart2NumericUpDown;
            StLocalisPart2RightHand = CConvertEngine.CopyArray(medicalInspection.StLocalisPart2RightHand);
            StLocalisPart2TextBoxes = CConvertEngine.CopyArray(medicalInspection.StLocalisPart2TextBoxes);
            StLocalisPart2WhichHand = medicalInspection.StLocalisPart2WhichHand;
            StPraesensComboBoxes = CConvertEngine.CopyArray(medicalInspection.StPraesensComboBoxes);
            StPraesensNumericUpDowns = CConvertEngine.CopyArray(medicalInspection.StPraesensNumericUpDowns);
            StPraesensTextBoxes = CConvertEngine.CopyArray(medicalInspection.StPraesensTextBoxes);
            TeoRisk = medicalInspection.TeoRisk;
            NotInDatabase = medicalInspection.NotInDatabase;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string parameterName,
            string ownValue,
            string foreignValue,
            CMedicalInspection diffMedicalInspection,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'.  Дата госпитализации: '{2}'. Объект: 'Обследование в отделении'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnHospitalization = HospitalizationId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignHospitalization = diffMedicalInspection.HospitalizationId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и переданным 
        /// осмотром в отделении
        /// </summary>
        /// <param name="diffMedicalInspection">Импортируемый осмотр в отделении</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CMedicalInspection diffMedicalInspection,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (AnamneseAnMorbi != diffMedicalInspection.AnamneseAnMorbi)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionAnamneseAnMorbi,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Анамнез, AnMorbi",
                   AnamneseAnMorbi,
                   diffMedicalInspection.AnamneseAnMorbi,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Complaints != diffMedicalInspection.Complaints)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionComplaints,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общие данные, жалобы",
                   Complaints,
                   diffMedicalInspection.Complaints,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ExpertAnamnese != diffMedicalInspection.ExpertAnamnese)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionExpertAnamnese,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общие данные, 1, 2 или 3 лист нетрудоспособности",
                   ExpertAnamnese.ToString(),
                   diffMedicalInspection.ExpertAnamnese.ToString(),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StLocalisDescription != diffMedicalInspection.StLocalisDescription)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisDescription,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Описание StLocalis-а",
                   StLocalisDescription,
                   diffMedicalInspection.StLocalisDescription,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StLocalisRentgen != diffMedicalInspection.StLocalisRentgen)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisRentgen,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Тип рентгена",
                   StLocalisRentgen,
                   diffMedicalInspection.StLocalisRentgen,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (InspectionPlan != diffMedicalInspection.InspectionPlan)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionInspectionPlan,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общие данные, обследование",
                   InspectionPlan,
                   diffMedicalInspection.InspectionPlan,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsAnamneseActive != diffMedicalInspection.IsAnamneseActive)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionIsAnamneseActive,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Включён ли анамнез в общий отчёт",
                   IsAnamneseActive.ToString(),
                   diffMedicalInspection.IsAnamneseActive.ToString(),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsPlanEnabled != diffMedicalInspection.IsPlanEnabled)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionIsPlanEnabled,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Включён ли план осмотра в отчёт",
                   IsPlanEnabled.ToString(),
                   diffMedicalInspection.IsPlanEnabled.ToString(),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsStLocalisPart1Enabled != diffMedicalInspection.IsStLocalisPart1Enabled)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionIsStLocalisPart1Enabled,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Включён ли st.localis часть 1 в общий отчёт",
                   IsStLocalisPart1Enabled.ToString(),
                   diffMedicalInspection.IsStLocalisPart1Enabled.ToString(),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsStLocalisPart2Enabled != diffMedicalInspection.IsStLocalisPart2Enabled)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionIsStLocalisPart2Enabled,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Включён ли st.localis часть 2 в общий отчёт",
                   IsStLocalisPart2Enabled.ToString(),
                   diffMedicalInspection.IsStLocalisPart2Enabled.ToString(),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StLocalisPart1OppositionFinger != diffMedicalInspection.StLocalisPart1OppositionFinger)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart1OppositionFinger,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "st.localis часть 1, номер пальца в оппозиции",
                   StLocalisPart1OppositionFinger,
                   diffMedicalInspection.StLocalisPart1OppositionFinger,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StLocalisPart2NumericUpDown != diffMedicalInspection.StLocalisPart2NumericUpDown)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart2NumericUpDown,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "st.localis часть 2, числовое поле",
                   StLocalisPart2NumericUpDown.ToString(),
                   diffMedicalInspection.StLocalisPart2NumericUpDown.ToString(),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StLocalisPart2WhichHand != diffMedicalInspection.StLocalisPart2WhichHand)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart2WhichHand,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "st.localis часть 2, выбор повреждённой руки",
                   StLocalisPart2WhichHand,
                   diffMedicalInspection.StLocalisPart2WhichHand,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (TeoRisk != diffMedicalInspection.TeoRisk)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionTeoRisk,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общие данные, риск ТЭО",
                   TeoRisk,
                   diffMedicalInspection.TeoRisk,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(LnFirstDateStart, diffMedicalInspection.LnFirstDateStart) != 0)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionLnFirstDateStart,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "общие данные, выдан первично с",
                   CConvertEngine.DateTimeToString(LnFirstDateStart),
                   CConvertEngine.DateTimeToString(diffMedicalInspection.LnFirstDateStart),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(LnWithNumberDateEnd, diffMedicalInspection.LnWithNumberDateEnd) != 0)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionLnWithNumberDateEnd,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "общие данные, выдан амбулаторно до",
                    CConvertEngine.DateTimeToString(LnWithNumberDateEnd),
                   CConvertEngine.DateTimeToString(diffMedicalInspection.LnWithNumberDateEnd),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(LnWithNumberDateStart, diffMedicalInspection.LnWithNumberDateStart) != 0)
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionLnWithNumberDateStart,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "общие данные, выдан амбулаторно с",
                   CConvertEngine.DateTimeToString(LnWithNumberDateStart),
                   CConvertEngine.DateTimeToString(diffMedicalInspection.LnWithNumberDateStart),
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            string ownValue;
            string foreignValue;
            if (!CCompareEngine.IsArraysEqual(AnamneseAnVitae, diffMedicalInspection.AnamneseAnVitae, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionAnamneseAnVitae,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "осмотр в отделении, анамнез, AnVitae",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = AnamneseAnVitae;
                foreignPatientMergeInfo.Object = diffMedicalInspection.AnamneseAnVitae;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StPraesensTextBoxes, diffMedicalInspection.StPraesensTextBoxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStPraesensTextBoxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StPraesens, текстовые поля",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StPraesensTextBoxes;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StPraesensTextBoxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(AnamneseCheckboxes, diffMedicalInspection.AnamneseCheckboxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionAnamneseCheckboxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "анамнез, checkbox-ы",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = AnamneseCheckboxes;
                foreignPatientMergeInfo.Object = diffMedicalInspection.AnamneseCheckboxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(AnamneseTextBoxes, diffMedicalInspection.AnamneseTextBoxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionAnamneseTextBoxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "анамнез, текстовые поля",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = AnamneseTextBoxes;
                foreignPatientMergeInfo.Object = diffMedicalInspection.AnamneseTextBoxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StLocalisPart1Fields, diffMedicalInspection.StLocalisPart1Fields, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart1Fields,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StLocalis часть 1, текстовые поля",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StLocalisPart1Fields;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StLocalisPart1Fields;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StLocalisPart2ComboBoxes, diffMedicalInspection.StLocalisPart2ComboBoxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart2ComboBoxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StLocalis часть 2, comboBox-ы",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StLocalisPart2ComboBoxes;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StLocalisPart2ComboBoxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StLocalisPart2LeftHand, diffMedicalInspection.StLocalisPart2LeftHand, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart2LeftHand,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StLocalis часть 2, comboBox-ы для левой руки",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StLocalisPart2LeftHand;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StLocalisPart2LeftHand;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StLocalisPart2RightHand, diffMedicalInspection.StLocalisPart2RightHand, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart2RightHand,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StLocalis часть 2, comboBox-ы для правой руки",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StLocalisPart2RightHand;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StLocalisPart2RightHand;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StLocalisPart2TextBoxes, diffMedicalInspection.StLocalisPart2TextBoxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStLocalisPart2TextBoxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StLocalis часть 2, текстовые поля",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StLocalisPart2TextBoxes;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StLocalisPart2TextBoxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StPraesensComboBoxes, diffMedicalInspection.StPraesensComboBoxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStPraesensComboBoxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StPraesens, comboBox-ы",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StPraesensComboBoxes;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StPraesensComboBoxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(StPraesensNumericUpDowns, diffMedicalInspection.StPraesensNumericUpDowns, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.MedicalInspectionStPraesensNumericUpDowns,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "StPraesens, числовые поля",
                   ownValue,
                   foreignValue,
                   diffMedicalInspection,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = StPraesensNumericUpDowns;
                foreignPatientMergeInfo.Object = diffMedicalInspection.StPraesensNumericUpDowns;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
