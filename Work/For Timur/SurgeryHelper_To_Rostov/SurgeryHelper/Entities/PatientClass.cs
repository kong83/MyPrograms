using System;
using System.Collections.Generic;
using SurgeryHelper.Engines;

namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с данными по пациенту
    /// </summary>
    public class PatientClass
    {
        /// <summary>
        /// Открытая для этого пациента форма с данными, если она есть
        /// </summary>
        public PatientViewForm OpenedPatientViewForm;

        /// <summary>
        /// Уникальный эдентификатор пользователя
        /// </summary>
        public int Id;

        /// <summary>
        /// Фамилия пациента
        /// </summary>
        public string LastName;

        /// <summary>
        /// Имя пациента
        /// </summary>
        public string Name;

        /// <summary>
        /// Отчество пациента
        /// </summary>
        public string Patronymic;

        /// <summary>
        /// Возраст пациента
        /// </summary>
        public int Age;

        /// <summary>
        /// День рождения пациента
        /// </summary>
        public DateTime? Birthday;

        /// <summary>
        /// Город проживания
        /// </summary>
        public string CityName;

        /// <summary>
        /// Улица проживания
        /// </summary>
        public string StreetName;

        /// <summary>
        /// Номер дома
        /// </summary>
        public string HomeNumber;

        /// <summary>
        /// Номер корпуса
        /// </summary>
        public string BuildingNumber;

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string FlatNumber;

        /// <summary>
        /// Место работы
        /// </summary>
        public string WorkPlace;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone;

        /// <summary>
        /// Тип стационара
        /// </summary>
        public string TypeOfKSG;

        /// <summary>
        /// Код МКБ
        /// </summary>
        public string MKB;

        /// <summary>
        /// Код КСГ вместе с информацией о детях
        /// </summary>
        public string KSG;

        /// <summary>
        /// Дата поступления
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
        /// Лечащий врач
        /// </summary>
        public string DoctorInChargeOfTheCase;

        /// <summary>
        /// Нозология пациента
        /// </summary>
        public string Nosology;

        /// <summary>
        /// Поле с путём до файлов данного пациента
        /// </summary>
        public string PrivateFolder;

        /// <summary>
        /// Список операций
        /// </summary>
        public List<OperationClass> Operations;

        /// <summary>
        /// Дополнительная информация для переводного эпикриза
        /// </summary>
        public string TransferEpicrisAdditionalInfo;

        /// <summary>
        /// Послеоперационный период для переводного  эпикриза
        /// </summary>
        public string TransferEpicrisAfterOperationPeriod;

        /// <summary>
        /// Планирующиеся действия для переводного  эпикриза
        /// </summary>
        public string TransferEpicrisPlan;

        /// <summary>
        /// Дата написания документа для переводного эпикриза
        /// </summary>
        public DateTime TransferEpicrisWritingDate;

        /// <summary>
        /// Личный номер для переводного эпикриза
        /// </summary>
        public string TransferEpicrisDisabilityList;

        /// <summary>
        /// Включать ли личный номер в отчёт
        /// </summary>
        public bool TransferEpicrisIsIncludeDisabilityList;

        /// <summary>
        /// Дополнительная информация для этапного эпикриза
        /// </summary>
        public string LineOfCommEpicrisAdditionalInfo;

        /// <summary>
        /// Планирующиеся действия для этапного эпикриза
        /// </summary>
        public string LineOfCommEpicrisPlan;

        /// <summary>
        /// Дата написания документа для этапного эпикриза
        /// </summary>
        public DateTime LineOfCommEpicrisWritingDate;

        /// <summary>
        /// Консервативное лечение
        /// </summary>
        public string DischargeEpicrisConservativeTherapy;

        /// <summary>
        /// После операции
        /// </summary>
        public string DischargeEpicrisAfterOperation;

        /// <summary>
        /// Общий анализ крови, эритроциты
        /// </summary>
        public string DischargeEpicrisOakEritrocits;

        /// <summary>
        /// Общий анализ крови, лекоциты
        /// </summary>
        public string DischargeEpicrisOakLekocits;

        /// <summary>
        /// Общий анализ крови, Hb
        /// </summary>
        public string DischargeEpicrisOakHb;

        /// <summary>
        /// Общий анализ крови, СОЭ
        /// </summary>
        public string DischargeEpicrisOakSoe;

        /// <summary>
        /// Общий анализ мочи, цвет
        /// </summary>
        public string DischargeEpicrisOamColor;

        /// <summary>
        /// Общий анализ мочи, относительная плотность
        /// </summary>
        public string DischargeEpicrisOamDensity;

        /// <summary>
        /// Общий анализ мочи, эритроциты
        /// </summary>
        public string DischargeEpicrisOamEritrocits;

        /// <summary>
        /// Общий анализ мочи, лейкоциты
        /// </summary>
        public string DischargeEpicrisOamLekocits;

        /// <summary>
        /// Биохимический анализ крови, биллирубин
        /// </summary>
        public string DischargeEpicrisBakBillirubin;

        /// <summary>
        /// Биохимический анализ крови, общий белок
        /// </summary>
        public string DischargeEpicrisBakGeneralProtein;

        /// <summary>
        /// Биохимический анализ крови, сахар
        /// </summary>
        public string DischargeEpicrisBakSugar;

        /// <summary>
        /// Биохимический анализ крови, ПТИ
        /// </summary>
        public string DischargeEpicrisBakPTI;

        /// <summary>
        /// Общий анализ мочи, другие анализы
        /// </summary>
        public string DischargeEpicrisAdditionalAnalises;

        /// <summary>
        /// ЭКГ пациента
        /// </summary>
        public string DischargeEpicrisEkg;

        /// <summary>
        /// Рекомендации при выписке
        /// </summary>
        public List<string> DischargeEpicrisRecomendations;

        /// <summary>
        /// Дополнительные рекомендации при выписке
        /// </summary>
        public List<string> DischargeEpicrisAdditionalRecomendations;

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
        /// Осмотр при поступлении, общие данные, включен ли план осмотра в отчёт
        /// </summary>
        public bool MedicalInspectionIsPlanEnabled;

        /// <summary>
        /// Осмотр при поступлении, общие данные, обследование
        /// </summary>
        public string MedicalInspectionInspectionPlan;

        /// <summary>
        /// Осмотр при поступлении, общие данные, жалобы
        /// </summary>
        public string MedicalInspectionComplaints;

        /// <summary>
        /// Осмотр при поступлении, общие данные, риск ТЭО
        /// </summary>
        public string MedicalInspectionTeoRisk;

        /// <summary>
        /// Осмотр при поступлении, общие данные, 1, 2 или 3 лист нетрудоспособности
        /// </summary>
        public int MedicalInspectionExpertAnamnese;

        /// <summary>
        /// Осмотр при поступлении, общие данные, выдан амбулаторно с
        /// </summary>
        public DateTime MedicalInspectionLnWithNumberDateStart;

        /// <summary>
        /// Осмотр при поступлении, общие данные, выдан амбулаторно до
        /// </summary>
        public DateTime MedicalInspectionLnWithNumberDateEnd;

        /// <summary>
        /// Осмотр при поступлении, общие данные, выдан первично с
        /// </summary>
        public DateTime MedicalInspectionLnFirstDateStart;        

        /// <summary>
        /// Включён ли анамнез в общий отчёт
        /// </summary>
        public bool MedicalInspectionIsAnamneseActive;

        /// <summary>
        /// Осмотр при поступлении, анамнез, дата травмы (если есть)
        /// </summary>
        public DateTime? MedicalInspectionAnamneseTraumaDate;

        /// <summary>
        /// Осмотр при поступлении, анамнез, AnMorbi
        /// </summary>
        public string MedicalInspectionAnamneseAnMorbi;

        /// <summary>
        /// Осмотр при поступлении, анамнез, AnVitae
        /// </summary>
        public bool[] MedicalInspectionAnamneseAnVitae;

        /// <summary>
        /// Осмотр при поступлении, анамнез, поля
        /// </summary>
        public string[] MedicalInspectionAnamneseTextBoxes;

        /// <summary>
        /// Осмотр при поступлении, анамнез, checkbox-ы
        /// </summary>
        public bool[] MedicalInspectionAnamneseCheckboxes;

        /// <summary>
        /// Осмотр при поступлении, st.praesens, текстовые поля
        /// </summary>
        public string[] MedicalInspectionStPraesensTextBoxes;

        /// <summary>
        /// Осмотр при поступлении, st.praesens, комбобоксы
        /// </summary>
        public string[] MedicalInspectionStPraesensComboBoxes;

        /// <summary>
        /// Осмотр при поступлении, st.praesens, числовые поля
        /// </summary>
        public int[] MedicalInspectionStPraesensNumericUpDowns;

        /// <summary>
        /// Осмотр при поступлении, st.praesens, разное
        /// </summary>
        public string MedicalInspectionStPraesensOthers;

        /// <summary>
        /// Осмотр при поступлении, описание St. localis-а
        /// </summary>
        public string MedicalInspectionStLocalisDescription;

        /// <summary>
        /// Осмотр при поступлении, тип рентгена
        /// </summary>
        public string MedicalInspectionStLocalisRentgen;

        /// <summary>
        /// Включён ли st.localis часть 1 в общий отчёт
        /// </summary>
        public bool MedicalInspectionIsStLocalisPart1Enabled;

        /// <summary>
        /// Осмотр при поступлении, st.localis part1, поля
        /// </summary>
        public string[] MedicalInspectionStLocalisPart1Fields;

        /// <summary>
        /// Осмотр при поступлении, st.localis part1, номер пальца в оппозиции
        /// </summary>
        public string MedicalInspectionStLocalisPart1OppositionFinger;

        /// <summary>
        /// Включён ли st.localis часть 2 в общий отчёт
        /// </summary>
        public bool MedicalInspectionIsStLocalisPart2Enabled;

        /// <summary>
        /// Осмотр при поступлении, st.localis part2, какая рука(левая/правая/правая, левая)
        /// </summary>
        public string MedicalInspectionStLocalisPart2WhichHand;

        /// <summary>
        /// Осмотр при поступлении, st.localis part2, текст боксы
        /// </summary>
        public string[] MedicalInspectionStLocalisPart2TextBoxes;

        /// <summary>
        /// Осмотр при поступлении, st.localis part2, комбобоксы
        /// </summary>
        public string[] MedicalInspectionStLocalisPart2ComboBoxes;

        /// <summary>
        /// Осмотр при поступлении, st.localis part2, комбобоксы для левой руки
        /// </summary>
        public string[] MedicalInspectionStLocalisPart2LeftHand;

        /// <summary>
        /// Осмотр при поступлении, st.localis part2, комбобоксы для правой руки
        /// </summary>
        public string[] MedicalInspectionStLocalisPart2RightHand;

        /// <summary>
        /// Осмотр при поступлении, st.localis part2, числовое поле
        /// </summary>
        public int MedicalInspectionStLocalisPart2NumericUpDown;


        /// <summary>
        /// Вернуть полный адрес
        /// </summary>
        /// <returns></returns>
        public string GetAddress()
        {
            string address = string.Empty;

            if (!string.IsNullOrEmpty(CityName))
            {
                address += ", " + CityName;
            }

            if (!string.IsNullOrEmpty(StreetName))
            {
                address += ", улица " + StreetName;
            }

            if (!string.IsNullOrEmpty(HomeNumber))
            {
                address += ", дом №" + HomeNumber;
            }

            if (!string.IsNullOrEmpty(BuildingNumber))
            {
                address += ", корпус " + BuildingNumber;
            }

            if (!string.IsNullOrEmpty(FlatNumber))
            {
                address += ", квартира №" + FlatNumber;
            }

            if (address.Length > 2)
            {
                address = address.Substring(2);
            }

            return address;
        }

        /// <summary>
        /// Д/Н
        /// </summary>
        /// <returns></returns>
        public string GetDN()
        {
            if (NumberOfCaseHistory.ToLower().Contains("д"))
            {
                return "Д";
            }

            return "Н";
        }

        /// <summary>
        /// к/д
        /// </summary>
        /// <returns></returns>
        public string GetKD()
        {
            int tempKD;
            if (ReleaseDate.HasValue && ConvertEngine.CompareDateTimes(ReleaseDate.Value.Date, DateTime.Now.Date, true) <= 0)
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

        /// <summary>
        /// Вернуть полное имя пациента (Фалимия Имя Отчество)
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return LastName + " " + Name + " " + Patronymic;
        }

        public PatientClass()
        {
            Id = 0;
            Operations = new List<OperationClass>();

            TransferEpicrisAfterOperationPeriod = "без особенностей";
            TransferEpicrisPlan = "перевязки до заживления ран, ЛФК";
            TransferEpicrisWritingDate = DateTime.Now;

            LineOfCommEpicrisPlan = "перевязки до заживления ран, ЛФК";
            LineOfCommEpicrisWritingDate = DateTime.Now;

            DischargeEpicrisAfterOperation = "раны зажили первичным натяжением, швы сняты.";
            DischargeEpicrisConservativeTherapy = "Анальгин 50%-2.0 + Димедрол 1%-1,0 2 раза в день в/м";
            DischargeEpicrisOamColor = "с/ж";
            DischargeEpicrisOamDensity = "1015";
            DischargeEpicrisOamEritrocits = "нет";
            DischargeEpicrisOamLekocits = "нет";
            DischargeEpicrisEkg = "без патологии";
            DischargeEpicrisRecomendations = new List<string> { "notdefined" };
            DischargeEpicrisAdditionalRecomendations = new List<string> { "notdefined" };            
            TreatmentPlanInspection = "ОАК, ОАМ, ЭКГ,  биохимический анализ крови, кровь на алкоголь, группа крови, резус фактор, коагулограмма";
            IsTreatmentPlanActiveInOperationProtocol = true;

            MedicalInspectionExpertAnamnese = 3;
            MedicalInspectionIsPlanEnabled = true;
            MedicalInspectionInspectionPlan = "ОАК, ОАМ, ЭКГ,  биохимический анализ крови, кровь на алкоголь, группа крови, резус фактор, коагулограмма";
            MedicalInspectionTeoRisk = "отсутствует";
            MedicalInspectionExpertAnamnese = 3;
            MedicalInspectionStLocalisRentgen = "без костной патологии";

            MedicalInspectionAnamneseAnVitae = new bool[4];
            MedicalInspectionAnamneseTextBoxes = new[] // string[8]
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
            MedicalInspectionAnamneseCheckboxes = new bool[12];

            MedicalInspectionStPraesensTextBoxes = new[] // string[17]
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
            MedicalInspectionStPraesensComboBoxes = new[] // string[4]
            {
                "удовлетворительное",
                "удовлетворительное",
                "везикулярное",
                "ясные",
            };
            MedicalInspectionStPraesensNumericUpDowns = new[] // int[4]
            {
                18,
                76,
                120, 
                70
            };

            MedicalInspectionStLocalisPart1OppositionFinger = "I";
            MedicalInspectionStLocalisPart1Fields = new string[62];
            for (int i = 0; i < MedicalInspectionStLocalisPart1Fields.Length; i++)
            {
                MedicalInspectionStLocalisPart1Fields[i] = "N";
            }

            MedicalInspectionStLocalisPart2NumericUpDown = 3;
            MedicalInspectionStLocalisPart2WhichHand = "левая";
            MedicalInspectionStLocalisPart2TextBoxes = new[] // string[11] 
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
            MedicalInspectionStLocalisPart2ComboBoxes = new[] // string[10] 
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
            MedicalInspectionStLocalisPart2LeftHand = new string[24];
            MedicalInspectionStLocalisPart2RightHand = new string[24];

            for (int i = 0; i < 22; i++)
            {
                MedicalInspectionStLocalisPart2LeftHand[i] =
                MedicalInspectionStLocalisPart2RightHand[i] = "есть";
            }

            MedicalInspectionStLocalisPart2LeftHand[22] =
            MedicalInspectionStLocalisPart2RightHand[22] = "розовый";
            MedicalInspectionStLocalisPart2LeftHand[23] =
            MedicalInspectionStLocalisPart2RightHand[23] = "теплая";

            MedicalInspectionLnFirstDateStart = DateTime.Now;
            MedicalInspectionLnWithNumberDateEnd = DateTime.Now;
            MedicalInspectionLnWithNumberDateStart = DateTime.Now;
        }
        
        public PatientClass(PatientClass patientClass)
        {
            Id = patientClass.Id;
            LastName = patientClass.LastName;
            Name = patientClass.Name;
            Patronymic = patientClass.Patronymic;
            Age = patientClass.Age;
            if (patientClass.Birthday.HasValue)
            {
                Birthday = CopyDateTime(patientClass.Birthday.Value);
            }
            else
            {
                Birthday = null;
            }

            BuildingNumber = patientClass.BuildingNumber;
            CityName = patientClass.CityName;
            Diagnose = patientClass.Diagnose;
            DoctorInChargeOfTheCase = patientClass.DoctorInChargeOfTheCase;
            FlatNumber = patientClass.FlatNumber;
            WorkPlace = patientClass.WorkPlace;
            Phone = patientClass.Phone;
            TypeOfKSG = patientClass.TypeOfKSG;
            MKB = patientClass.MKB;
            KSG = patientClass.KSG;
            HomeNumber = patientClass.HomeNumber;
            Nosology = patientClass.Nosology;
            NumberOfCaseHistory = patientClass.NumberOfCaseHistory;
            StreetName = patientClass.StreetName;
            PrivateFolder = patientClass.PrivateFolder;

            DeliveryDate = CopyDateTime(patientClass.DeliveryDate);
            if (patientClass.ReleaseDate.HasValue)
            {
                ReleaseDate = CopyDateTime(patientClass.ReleaseDate.Value);
            }
            else
            {
                ReleaseDate = null;
            }

            Operations = new List<OperationClass>();
            foreach (OperationClass operation in patientClass.Operations)
            {
                var newOperationClass = new OperationClass(operation);
                Operations.Add(newOperationClass);
            }

            TransferEpicrisAfterOperationPeriod = patientClass.TransferEpicrisAfterOperationPeriod;
            TransferEpicrisPlan = patientClass.TransferEpicrisPlan;
            TransferEpicrisWritingDate = CopyDateTime(patientClass.TransferEpicrisWritingDate);
            TransferEpicrisAdditionalInfo = patientClass.TransferEpicrisAdditionalInfo;
            TransferEpicrisDisabilityList = patientClass.TransferEpicrisDisabilityList;
            TransferEpicrisIsIncludeDisabilityList = patientClass.TransferEpicrisIsIncludeDisabilityList;

            LineOfCommEpicrisAdditionalInfo = patientClass.LineOfCommEpicrisAdditionalInfo;
            LineOfCommEpicrisPlan = patientClass.LineOfCommEpicrisPlan;
            LineOfCommEpicrisWritingDate = CopyDateTime(patientClass.LineOfCommEpicrisWritingDate);

            DischargeEpicrisAfterOperation = patientClass.DischargeEpicrisAfterOperation;
            DischargeEpicrisConservativeTherapy = patientClass.DischargeEpicrisConservativeTherapy;
            DischargeEpicrisEkg = patientClass.DischargeEpicrisEkg;
            DischargeEpicrisOakEritrocits = patientClass.DischargeEpicrisOakEritrocits;
            DischargeEpicrisOakHb = patientClass.DischargeEpicrisOakHb;
            DischargeEpicrisOakLekocits = patientClass.DischargeEpicrisOakLekocits;
            DischargeEpicrisOakSoe = patientClass.DischargeEpicrisOakSoe;
            DischargeEpicrisOamColor = patientClass.DischargeEpicrisOamColor;
            DischargeEpicrisOamDensity = patientClass.DischargeEpicrisOamDensity;
            DischargeEpicrisOamEritrocits = patientClass.DischargeEpicrisOamEritrocits;
            DischargeEpicrisOamLekocits = patientClass.DischargeEpicrisOamLekocits;
            DischargeEpicrisBakBillirubin = patientClass.DischargeEpicrisBakBillirubin;
            DischargeEpicrisBakGeneralProtein = patientClass.DischargeEpicrisBakGeneralProtein;
            DischargeEpicrisBakPTI = patientClass.DischargeEpicrisBakPTI;
            DischargeEpicrisBakSugar = patientClass.DischargeEpicrisBakSugar;

            DischargeEpicrisAdditionalAnalises = patientClass.DischargeEpicrisAdditionalAnalises;

            DischargeEpicrisRecomendations = new List<string>(patientClass.DischargeEpicrisRecomendations);            

            DischargeEpicrisAdditionalRecomendations = new List<string>(patientClass.DischargeEpicrisAdditionalRecomendations);

            TreatmentPlanInspection = patientClass.TreatmentPlanInspection;
            TreatmentPlanDate = CopyDateTime(patientClass.TreatmentPlanDate);
            IsTreatmentPlanActiveInOperationProtocol = patientClass.IsTreatmentPlanActiveInOperationProtocol;

            MedicalInspectionAnamneseTraumaDate = CopyDateTime(patientClass.MedicalInspectionAnamneseTraumaDate);
            MedicalInspectionAnamneseAnMorbi = patientClass.MedicalInspectionAnamneseAnMorbi;
            MedicalInspectionAnamneseAnVitae = CopyBoolArray(patientClass.MedicalInspectionAnamneseAnVitae);
            MedicalInspectionAnamneseCheckboxes = CopyBoolArray(patientClass.MedicalInspectionAnamneseCheckboxes);
            MedicalInspectionAnamneseTextBoxes = CopyStringArray(patientClass.MedicalInspectionAnamneseTextBoxes);
            MedicalInspectionComplaints = patientClass.MedicalInspectionComplaints;
            MedicalInspectionExpertAnamnese = patientClass.MedicalInspectionExpertAnamnese;
            MedicalInspectionStLocalisDescription = patientClass.MedicalInspectionStLocalisDescription;
            MedicalInspectionStLocalisRentgen = patientClass.MedicalInspectionStLocalisRentgen;
            MedicalInspectionInspectionPlan = patientClass.MedicalInspectionInspectionPlan;
            MedicalInspectionIsAnamneseActive = patientClass.MedicalInspectionIsAnamneseActive;
            MedicalInspectionIsPlanEnabled = patientClass.MedicalInspectionIsPlanEnabled;
            MedicalInspectionIsStLocalisPart1Enabled = patientClass.MedicalInspectionIsStLocalisPart1Enabled;
            MedicalInspectionIsStLocalisPart2Enabled = patientClass.MedicalInspectionIsStLocalisPart2Enabled;
            MedicalInspectionLnFirstDateStart = CopyDateTime(patientClass.MedicalInspectionLnFirstDateStart);
            MedicalInspectionLnWithNumberDateEnd = CopyDateTime(patientClass.MedicalInspectionLnWithNumberDateEnd);
            MedicalInspectionLnWithNumberDateStart = CopyDateTime(patientClass.MedicalInspectionLnWithNumberDateStart);
            MedicalInspectionStLocalisPart1Fields = CopyStringArray(patientClass.MedicalInspectionStLocalisPart1Fields);
            MedicalInspectionStLocalisPart1OppositionFinger = patientClass.MedicalInspectionStLocalisPart1OppositionFinger;
            MedicalInspectionStLocalisPart2ComboBoxes = CopyStringArray(patientClass.MedicalInspectionStLocalisPart2ComboBoxes);
            MedicalInspectionStLocalisPart2LeftHand = CopyStringArray(patientClass.MedicalInspectionStLocalisPart2LeftHand);
            MedicalInspectionStLocalisPart2NumericUpDown = patientClass.MedicalInspectionStLocalisPart2NumericUpDown;
            MedicalInspectionStLocalisPart2RightHand = CopyStringArray(patientClass.MedicalInspectionStLocalisPart2RightHand);
            MedicalInspectionStLocalisPart2TextBoxes = CopyStringArray(patientClass.MedicalInspectionStLocalisPart2TextBoxes);
            MedicalInspectionStLocalisPart2WhichHand = patientClass.MedicalInspectionStLocalisPart2WhichHand;
            MedicalInspectionStPraesensComboBoxes = CopyStringArray(patientClass.MedicalInspectionStPraesensComboBoxes);
            MedicalInspectionStPraesensNumericUpDowns = CopyIntArray(patientClass.MedicalInspectionStPraesensNumericUpDowns);
            MedicalInspectionStPraesensOthers = patientClass.MedicalInspectionStPraesensOthers;
            MedicalInspectionStPraesensTextBoxes = CopyStringArray(patientClass.MedicalInspectionStPraesensTextBoxes);
            MedicalInspectionTeoRisk = patientClass.MedicalInspectionTeoRisk;
        }

        /// <summary>
        /// Скопировать данные о пациенте в переданного пациента (без использования new)
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void Copy(PatientClass patientInfo)
        {
            patientInfo.Id = Id;
            patientInfo.LastName = LastName;
            patientInfo.Name = Name;
            patientInfo.Patronymic = Patronymic;
            patientInfo.Age = Age;
            if (patientInfo.Birthday.HasValue)
            {
                Birthday = CopyDateTime(patientInfo.Birthday.Value);
            }
            else
            {
                Birthday = null;
            }

            patientInfo.BuildingNumber = BuildingNumber;
            patientInfo.CityName = CityName;
            patientInfo.Diagnose = Diagnose;
            patientInfo.DoctorInChargeOfTheCase = DoctorInChargeOfTheCase;
            patientInfo.FlatNumber = FlatNumber;
            patientInfo.WorkPlace = WorkPlace;
            patientInfo.Phone = Phone;
            patientInfo.TypeOfKSG = TypeOfKSG;
            patientInfo.MKB = MKB;
            patientInfo.KSG = KSG;
            patientInfo.HomeNumber = HomeNumber;
            patientInfo.Nosology = Nosology;
            patientInfo.NumberOfCaseHistory = NumberOfCaseHistory;
            patientInfo.StreetName = StreetName;
            patientInfo.PrivateFolder = PrivateFolder;

            patientInfo.DeliveryDate = CopyDateTime(DeliveryDate);
            if (ReleaseDate.HasValue)
            {
                ReleaseDate = CopyDateTime(ReleaseDate.Value);
            }
            else
            {
                ReleaseDate = null;
            }

            patientInfo.Operations = new List<OperationClass>();
            foreach (OperationClass operation in Operations)
            {
                var newOperationClass = new OperationClass(operation);
                patientInfo.Operations.Add(newOperationClass);
            }

            patientInfo.TransferEpicrisAfterOperationPeriod = TransferEpicrisAfterOperationPeriod;
            patientInfo.TransferEpicrisPlan = TransferEpicrisPlan;
            patientInfo.TransferEpicrisWritingDate = CopyDateTime(TransferEpicrisWritingDate);
            patientInfo.TransferEpicrisAdditionalInfo = TransferEpicrisAdditionalInfo;
            patientInfo.TransferEpicrisDisabilityList = TransferEpicrisDisabilityList;
            patientInfo.TransferEpicrisIsIncludeDisabilityList = TransferEpicrisIsIncludeDisabilityList;

            patientInfo.LineOfCommEpicrisAdditionalInfo = LineOfCommEpicrisAdditionalInfo;
            patientInfo.LineOfCommEpicrisPlan = LineOfCommEpicrisPlan;
            patientInfo.LineOfCommEpicrisWritingDate = CopyDateTime(LineOfCommEpicrisWritingDate);

            patientInfo.DischargeEpicrisAfterOperation = DischargeEpicrisAfterOperation;
            patientInfo.DischargeEpicrisConservativeTherapy = DischargeEpicrisConservativeTherapy;
            patientInfo.DischargeEpicrisEkg = DischargeEpicrisEkg;
            patientInfo.DischargeEpicrisOakEritrocits = DischargeEpicrisOakEritrocits;
            patientInfo.DischargeEpicrisOakHb = DischargeEpicrisOakHb;
            patientInfo.DischargeEpicrisOakLekocits = DischargeEpicrisOakLekocits;
            patientInfo.DischargeEpicrisOakSoe = DischargeEpicrisOakSoe;
            patientInfo.DischargeEpicrisOamColor = DischargeEpicrisOamColor;
            patientInfo.DischargeEpicrisOamDensity = DischargeEpicrisOamDensity;
            patientInfo.DischargeEpicrisOamEritrocits = DischargeEpicrisOamEritrocits;
            patientInfo.DischargeEpicrisOamLekocits = DischargeEpicrisOamLekocits;
            patientInfo.DischargeEpicrisBakBillirubin = DischargeEpicrisBakBillirubin;
            patientInfo.DischargeEpicrisBakGeneralProtein = DischargeEpicrisBakGeneralProtein;
            patientInfo.DischargeEpicrisBakPTI = DischargeEpicrisBakPTI;
            patientInfo.DischargeEpicrisBakSugar = DischargeEpicrisBakSugar;

            patientInfo.DischargeEpicrisAdditionalAnalises = DischargeEpicrisAdditionalAnalises;

            patientInfo.DischargeEpicrisRecomendations = new List<string>(DischargeEpicrisRecomendations);

            patientInfo.DischargeEpicrisAdditionalRecomendations = new List<string>(DischargeEpicrisAdditionalRecomendations);

            patientInfo.TreatmentPlanInspection = TreatmentPlanInspection;
            patientInfo.TreatmentPlanDate = CopyDateTime(TreatmentPlanDate);
            patientInfo.IsTreatmentPlanActiveInOperationProtocol = IsTreatmentPlanActiveInOperationProtocol;

            patientInfo.MedicalInspectionAnamneseTraumaDate = CopyDateTime(MedicalInspectionAnamneseTraumaDate);
            patientInfo.MedicalInspectionAnamneseAnMorbi = MedicalInspectionAnamneseAnMorbi;
            patientInfo.MedicalInspectionAnamneseAnVitae = CopyBoolArray(MedicalInspectionAnamneseAnVitae);
            patientInfo.MedicalInspectionAnamneseCheckboxes = CopyBoolArray(MedicalInspectionAnamneseCheckboxes);
            patientInfo.MedicalInspectionAnamneseTextBoxes = CopyStringArray(MedicalInspectionAnamneseTextBoxes);
            patientInfo.MedicalInspectionComplaints = MedicalInspectionComplaints;
            patientInfo.MedicalInspectionExpertAnamnese = MedicalInspectionExpertAnamnese;
            patientInfo.MedicalInspectionStLocalisDescription = MedicalInspectionStLocalisDescription;
            patientInfo.MedicalInspectionStLocalisRentgen = MedicalInspectionStLocalisRentgen;
            patientInfo.MedicalInspectionInspectionPlan = MedicalInspectionInspectionPlan;
            patientInfo.MedicalInspectionIsAnamneseActive = MedicalInspectionIsAnamneseActive;
            patientInfo.MedicalInspectionIsPlanEnabled = MedicalInspectionIsPlanEnabled;
            patientInfo.MedicalInspectionIsStLocalisPart1Enabled = MedicalInspectionIsStLocalisPart1Enabled;
            patientInfo.MedicalInspectionIsStLocalisPart2Enabled = MedicalInspectionIsStLocalisPart2Enabled;
            patientInfo.MedicalInspectionLnFirstDateStart = CopyDateTime(MedicalInspectionLnFirstDateStart);
            patientInfo.MedicalInspectionLnWithNumberDateEnd = CopyDateTime(MedicalInspectionLnWithNumberDateEnd);
            patientInfo.MedicalInspectionLnWithNumberDateStart = CopyDateTime(MedicalInspectionLnWithNumberDateStart);
            patientInfo.MedicalInspectionStLocalisPart1Fields = CopyStringArray(MedicalInspectionStLocalisPart1Fields);
            patientInfo.MedicalInspectionStLocalisPart1OppositionFinger = MedicalInspectionStLocalisPart1OppositionFinger;
            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes = CopyStringArray(MedicalInspectionStLocalisPart2ComboBoxes);
            patientInfo.MedicalInspectionStLocalisPart2LeftHand = CopyStringArray(MedicalInspectionStLocalisPart2LeftHand);
            patientInfo.MedicalInspectionStLocalisPart2NumericUpDown = MedicalInspectionStLocalisPart2NumericUpDown;
            patientInfo.MedicalInspectionStLocalisPart2RightHand = CopyStringArray(MedicalInspectionStLocalisPart2RightHand);
            patientInfo.MedicalInspectionStLocalisPart2TextBoxes = CopyStringArray(MedicalInspectionStLocalisPart2TextBoxes);
            patientInfo.MedicalInspectionStLocalisPart2WhichHand = MedicalInspectionStLocalisPart2WhichHand;
            patientInfo.MedicalInspectionStPraesensComboBoxes = CopyStringArray(MedicalInspectionStPraesensComboBoxes);
            patientInfo.MedicalInspectionStPraesensNumericUpDowns = CopyIntArray(MedicalInspectionStPraesensNumericUpDowns);
            patientInfo.MedicalInspectionStPraesensOthers = MedicalInspectionStPraesensOthers;
            patientInfo.MedicalInspectionStPraesensTextBoxes = CopyStringArray(MedicalInspectionStPraesensTextBoxes);
            patientInfo.MedicalInspectionTeoRisk = MedicalInspectionTeoRisk;
        }

        private static DateTime? CopyDateTime(DateTime? fromObj)
        {
            if (fromObj.HasValue)
            {
                return CopyDateTime(fromObj.Value);
            }

            return null;
        }

        private static DateTime CopyDateTime(DateTime fromObj)
        {
            return new DateTime(fromObj.Year, fromObj.Month, fromObj.Day, fromObj.Hour, fromObj.Minute, fromObj.Second);
        }

        private static string[] CopyStringArray(string[] fromObj)
        {
            var temp = new string[fromObj.Length];
            fromObj.CopyTo(temp, 0);
            return temp;
        }

        private static int[] CopyIntArray(int[] fromObj)
        {
            var temp = new int[fromObj.Length];
            fromObj.CopyTo(temp, 0);
            return temp;
        }

        private static bool[] CopyBoolArray(bool[] fromObj)
        {
            var temp = new bool[fromObj.Length];
            fromObj.CopyTo(temp, 0);
            return temp;
        }

        /// <summary>
        /// Сгенерировать новый ID для операции
        /// </summary>
        /// <returns></returns>
        private int GetNewOperationID()
        {
            int max = 0;
            foreach (OperationClass operationInfo in Operations)
            {
                if (operationInfo.Id > max)
                {
                    max = operationInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// Добавить новую операцию в список операций
        /// </summary>
        /// <param name="operationInfo">Информация про операцию</param>
        public void AddOperation(OperationClass operationInfo)
        {
            operationInfo.Id = GetNewOperationID();
            Operations.Add(operationInfo);
            Operations.Sort(OperationClass.Compare);
        }

        /// <summary>
        /// Изменение данных по операции
        /// </summary>
        /// <param name="operationInfo">Информация про операцию</param>
        public void UpdateOperation(OperationClass operationInfo)
        {
            int n = 0;
            while (Operations[n].Id != operationInfo.Id)
            {
                n++;
            }

            operationInfo.Copy(Operations[n]);
            Operations.Sort(OperationClass.Compare);
        }

        /// <summary>
        /// Удаление операции с указанным id
        /// </summary>
        /// <param name="operationId">ID операции</param>
        public void DeleteOperation(int operationId)
        {
            int n = 0;
            while (Operations[n].Id != operationId)
            {
                n++;
            }

            Operations.RemoveAt(n);
        }

        public static int CompareByName(PatientClass patientInfo1, PatientClass patientInfo2)
        {
            return string.Compare(patientInfo1.GetFullName(), patientInfo2.GetFullName());
        }

        public static int CompareByDeliveryDate(PatientClass patientInfo1, PatientClass patientInfo2)
        {
            return DateTime.Compare(patientInfo1.DeliveryDate, patientInfo2.DeliveryDate);
        }

        public static int CompareByReleaseDate(PatientClass patientInfo1, PatientClass patientInfo2)
        {
            if (patientInfo1.ReleaseDate.HasValue && patientInfo2.ReleaseDate.HasValue)
            {
                return DateTime.Compare(patientInfo1.ReleaseDate.Value, patientInfo2.ReleaseDate.Value);
            }

            if (patientInfo1.ReleaseDate.HasValue)
            {
                return 1;
            }

            if (patientInfo2.ReleaseDate.HasValue)
            {
                return -1;
            }

            return CompareByName(patientInfo1, patientInfo2);
        }

        public static int CompareByOperationDate(PatientClass patientInfo1, PatientClass patientInfo2)
        {
            int operationLastIndex1 = patientInfo1.Operations.Count - 1;
            int operationLastIndex2 = patientInfo2.Operations.Count - 1;
            if (operationLastIndex1 > -1 && operationLastIndex2 > -1)
            {
                return DateTime.Compare(
                    patientInfo1.Operations[operationLastIndex1].DataOfOperation, 
                    patientInfo2.Operations[operationLastIndex2].DataOfOperation);
            }

            if (operationLastIndex1 > -1)
            {
                return 1;
            }

            if (operationLastIndex2 > -1)
            {
                return -1;
            }

            return CompareByName(patientInfo1, patientInfo2);
        }
    }
}
