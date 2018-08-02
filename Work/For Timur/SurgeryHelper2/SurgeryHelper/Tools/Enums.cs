using System;

namespace SurgeryHelper.Tools
{
    public enum NodeType
    {
        Type = 0,
        Folder = 1
    }
    
    public enum NodeFolderStated
    {
        Closed = 0,
        Opened = 1
    }

    public enum ScrollBarType
    {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }

    public enum ScrollMessage : uint
    {
        WmVScroll = 0x0115,
        WmMouseWheel = 0x020A
    }

    public enum ScrollBarCommands : uint
    {
        ThumbPosition = 4,
        ThumbTrack = 5
    }

    [Flags]
    public enum ScrollBarInfo : uint
    {
        Range = 0x0001,
        Page = 0x0002,
        Pos = 0x0004,
        DisableNoScroll = 0x0008,
        TrackPos = 0x0010,
        All = (Range | Page | Pos | TrackPos)
    }

    public enum AddUpdate
    {
        /// <summary>
        /// Форма view открыта для добавления объекта
        /// </summary>
        Add,

        /// <summary>
        /// Форма view открыта для редактирования объекта
        /// </summary>
        Update
    }

    public class DisplayText : Attribute
    {
        public string Text { get; set; }

        public DisplayText(string text)
        {
            Text = text;
        }
    }

    public enum CardType
    {
        [DisplayText("Карта на кресцовое сплетение")]
        SacriplexCard,

        [DisplayText("Карта на кожные нервы руки")]
        HandCutaneousNerves,

        [DisplayText("Карта на кожные нервы ноги")]
        LegCutaneousNerves,

        [DisplayText("Карта на дерматомы руки")]
        HandDermatome,

        [DisplayText("Карта на дерматомы ноги")]
        LegDermatome,

        [DisplayText("Карта на тетраплегию")]
        PamplegiaCard
    }

    public enum CardSide
    {
        [DisplayText("Левая")]
        Left,

        [DisplayText("Правая")]
        Right
    }

    public enum ObjectType
    {
        /// <summary>
        /// Пустое значение
        /// </summary>
        Empty,

        /// <summary>
        /// Пациент
        /// </summary>
        Patient,

        /// <summary>
        /// Город проживания пациента
        /// </summary>
        PatientCityName,

        /// <summary>
        /// День рождения пациента
        /// </summary>
        PatientBirthday,

        /// <summary>
        /// Улица проживания пациента
        /// </summary>
        PatientStreetName,

        /// <summary>
        /// Номер дома пациента
        /// </summary>
        PatientHomeNumber,

        /// <summary>
        /// Корпус пациента
        /// </summary>
        PatientBuildingNumber,

        /// <summary>
        /// Квартира пациента
        /// </summary>
        PatientFlatNumber,

        /// <summary>
        /// Телефон пациента
        /// </summary>
        PatientPhone,

        /// <summary>
        /// Родственники пациента
        /// </summary>
        PatientRelatives,

        /// <summary>
        /// Указан ли законный представитель пациента
        /// </summary>
        PatientIsSpecifyLegalRepresent,

        /// <summary>
        /// ФИО законного представителя пациента
        /// </summary>
        PatientLegalRepresent,

        /// <summary>
        /// Место работы
        /// </summary>
        PatientWorkPlace,

        /// <summary>
        /// Адрес электронной почты пациента
        /// </summary>
        PatientEMail,

        /// <summary>
        /// Серия страховой компании
        /// </summary>
        PatientInsuranceSeries,

        /// <summary>
        /// Номер страховой компании
        /// </summary>
        PatientInsuranceNumber,

        /// <summary>
        /// Название страховой компании
        /// </summary>
        PatientInsuranceName,

        /// <summary>
        /// Тип страховой компании
        /// </summary>
        PatientInsuranceType,

        /// <summary>
        /// Серия паспорта пациента
        /// </summary>
        PatientPassInformationSeries,

        /// <summary>
        /// Номер паспорта пациента
        /// </summary>
        PatientPassInformationNumber,

        /// <summary>
        /// Код подразделения пациента
        /// </summary>
        PatientPassInformationSubdivisionCode,

        /// <summary>
        /// Организация, выдавшая паспорт пациенту
        /// </summary>
        PatientPassInformationOrganization,

        /// <summary>
        /// Дата выдачи паспорта пациенту
        /// </summary>
        PatientPassInformationDeliveryDate,

        /// <summary>
        /// Анамнез
        /// </summary>
        Anamnese,

        /// <summary>
        /// Дата травмы для анамнеза
        /// </summary>
        AnamneseTraumaDate,

        /// <summary>
        /// Описание для анамнеза
        /// </summary>
        AnamneseAnMorbi,

        /// <summary>
        /// Акушерский анамнез
        /// </summary>
        ObstetricHistory,

        /// <summary>
        /// Шкала Апгар для акушерского анамнеза
        /// </summary>
        ObstetricHistoryApgarScore,

        /// <summary>
        /// Родовая травма для акушерского анамнеза
        /// </summary>
        ObstetricHistoryBirthInjury,

        /// <summary>
        /// Роды в срок (количество недель) для акушерского анамнеза
        /// </summary>
        ObstetricHistoryChildbirthWeeks,

        /// <summary>
        /// Осложнения в ходе родов для акушерского анамнеза
        /// </summary>
        ObstetricHistoryComplicationsDuringChildbirth,

        /// <summary>
        /// Осложнения в период беременности для акушерского анамнеза
        /// </summary>
        ObstetricHistoryComplicationsInPregnancy,

        /// <summary>
        /// Лекарственные препараты и хронические интоксикации в период беременности для акушерского анамнеза
        /// </summary>
        ObstetricHistoryDrugsInPregnancy,

        /// <summary>
        /// Длительность родов (в часах) для акушерского анамнеза
        /// </summary>
        ObstetricHistoryDurationOfLabor,

        /// <summary>
        /// Предлежание для акушерского анамнеза
        /// </summary>
        ObstetricHistoryFetal,

        /// <summary>
        /// Рост при рождении (см) для акушерского анамнеза
        /// </summary>
        ObstetricHistoryHeightAtBirth,

        /// <summary>
        /// Стационарное лечение (даты госпитализаций) и проводимое лечение (включая операции) для акушерского анамнеза
        /// </summary>
        ObstetricHistoryHospitalTreatment,

        /// <summary>
        /// Использование щипцов в ходе родов для акушерского анамнеза
        /// </summary>
        ObstetricHistoryIsTongsUsing,

        /// <summary>
        /// Использование вакуума в ходе родов для акушерского анамнеза
        /// </summary>
        ObstetricHistoryIsVacuumUsing,

        /// <summary>
        /// Когда и кем диагностирован акушерский паралич для акушерского анамнеза
        /// </summary>
        ObstetricHistoryObstetricParalysis,

        /// <summary>
        ///  Амбулаторное лечение (разработка объема пассивных движений, лечебная физкультура, шинирование и т.д.) для акушерского анамнеза
        /// </summary>
        ObstetricHistoryOutpatientCare,

        /// <summary>
        /// Вес при рождении (г) для акушерского анамнеза
        /// </summary>
        ObstetricHistoryWeightAtBirth,

        /// <summary>
        /// Хронология восстановления активных движений верхней конечности для акушерского анамнеза
        /// </summary>
        ObstetricHistoryChronology,

        /// <summary>
        /// Госпитализация
        /// </summary>
        Hospitalization,

        /// <summary>
        /// Диагноз пациента для госпитализации
        /// </summary>
        HospitalizationDiagnose,

        /// <summary>
        /// Лечащий врач для госпитализации
        /// </summary>
        HospitalizationDoctorInChargeOfTheCase,

        /// <summary>
        /// Название папки с фотографиями для госпитализации
        /// </summary>
        HospitalizationFotoFolderName,

        /// <summary>
        /// Номер истории болезни для госпитализации
        /// </summary>
        HospitalizationNumberOfCaseHistory,

        /// <summary>
        /// Дата выписки для госпитализации
        /// </summary>
        HospitalizationReleaseDate,

        /// <summary>
        /// Выписной эпикриз
        /// </summary>
        DischargeEpicrisis,

        /// <summary>
        /// Консервативное лечение для выписного эпикриза
        /// </summary>
        DischargeEpicrisisConservativeTherapy,

        /// <summary>
        /// Время взятия анализов для выписного эпикриза
        /// </summary>
        DischargeEpicrisisAnalysisDate,

        /// <summary>
        /// После операции для выписного эпикриза
        /// </summary>
        DischargeEpicrisisAfterOperation,

        /// <summary>
        /// Общий анализ крови, эритроциты для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOakEritrocits,

        /// <summary>
        /// Общий анализ крови, лекоциты для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOakLekocits,

        /// <summary>
        /// Общий анализ крови, Hb для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOakHb,

        /// <summary>
        /// Общий анализ крови, СОЭ для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOakSoe,

        /// <summary>
        /// Общий анализ мочи, цвет для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOamColor,

        /// <summary>
        /// Общий анализ мочи, относительная плотность для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOamDensity,

        /// <summary>
        /// Общий анализ мочи, эритроциты для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOamEritrocits,

        /// <summary>
        /// Общий анализ мочи, лейкоциты для выписного эпикриза
        /// </summary>
        DischargeEpicrisisOamLekocits,

        /// <summary>
        /// Общий анализ мочи, другие анализы для выписного эпикриза
        /// </summary>
        DischargeEpicrisisAdditionalAnalises,

        /// <summary>
        /// ЭКГ пациента для выписного эпикриза
        /// </summary>
        DischargeEpicrisisEkg,

        /// <summary>
        /// Рекомендации при выписке для выписного эпикриза
        /// </summary>
        DischargeEpicrisisRecomendations,

        /// <summary>
        /// Дополнительные рекомендации при выписке для выписного эпикриза
        /// </summary>
        DischargeEpicrisisAdditionalRecomendations,

        /// <summary>
        /// Этапный эпикриз
        /// </summary>
        LineOfCommunicationEpicrisis,

        /// <summary>
        /// Дополнительная информация для этапного эпикриза
        /// </summary>
        LineOfCommunicationEpicrisisAdditionalInfo,

        /// <summary>
        /// Планирующиеся действия для этапного эпикриза
        /// </summary>
        LineOfCommunicationEpicrisisPlan,

        /// <summary>
        /// Дата написания документа для этапного эпикриза
        /// </summary>
        LineOfCommunicationEpicrisisWritingDate,

        /// <summary>
        /// Переводной эпикриз
        /// </summary>
        TransferableEpicrisis,

        /// <summary>
        /// Дополнительная информация для переводного эпикриза
        /// </summary>
        TransferableEpicrisisAdditionalInfo,

        /// <summary>
        /// Послеоперационный период для переводного  эпикриза
        /// </summary>
        TransferableEpicrisisAfterOperationPeriod,

        /// <summary>
        /// Планирующиеся действия для переводного  эпикриза
        /// </summary>
        TransferableEpicrisisPlan,

        /// <summary>
        /// Дата написания документа для переводного эпикриза
        /// </summary>
        TransferableEpicrisisWritingDate,

        /// <summary>
        /// Личный номер для переводного эпикриза
        /// </summary>
        TransferableEpicrisisDisabilityList,

        /// <summary>
        /// Включать ли личный номер в отчёт
        /// </summary>
        TransferableEpicrisisIsIncludeDisabilityList,

        /// <summary>
        /// Осмотр в отделении
        /// </summary>
        MedicalInspection,

        /// <summary>
        /// Осмотр в отделении, общие данные, включен ли план осмотра в отчёт
        /// </summary>
        MedicalInspectionIsPlanEnabled,

        /// <summary>
        /// Осмотр в отделении, общие данные, обследование
        /// </summary>
        MedicalInspectionInspectionPlan,

        /// <summary>
        /// Осмотр в отделении, общие данные, жалобы
        /// </summary>
        MedicalInspectionComplaints,

        /// <summary>
        /// Осмотр в отделении, общие данные, риск ТЭО
        /// </summary>
        MedicalInspectionTeoRisk,

        /// <summary>
        /// Осмотр в отделении, общие данные, 1, 2 или 3 лист нетрудоспособности
        /// </summary>
        MedicalInspectionExpertAnamnese,

        /// <summary>
        /// Осмотр в отделении, общие данные, выдан амбулаторно с даты
        /// </summary>
        MedicalInspectionLnWithNumberDateStart,

        /// <summary>
        /// Осмотр в отделении, общие данные, выдан амбулаторно до даты
        /// </summary>
        MedicalInspectionLnWithNumberDateEnd,

        /// <summary>
        /// Осмотр в отделении, общие данные, выдан первично с даты
        /// </summary>
        MedicalInspectionLnFirstDateStart,

        /// <summary>
        /// Включён ли анамнез в общий отчёт
        /// </summary>
        MedicalInspectionIsAnamneseActive,

        /// <summary>
        /// Осмотр в отделении, анамнез, AnMorbi
        /// </summary>
        MedicalInspectionAnamneseAnMorbi,

        /// <summary>
        /// Осмотр в отделении, анамнез, AnVitae
        /// </summary>
        MedicalInspectionAnamneseAnVitae,

        /// <summary>
        /// Осмотр в отделении, анамнез, поля
        /// </summary>
        MedicalInspectionAnamneseTextBoxes,

        /// <summary>
        /// Осмотр в отделении, анамнез, checkbox-ы
        /// </summary>
        MedicalInspectionAnamneseCheckboxes,

        /// <summary>
        /// Осмотр в отделении, st.praesens, текстовые поля
        /// </summary>
        MedicalInspectionStPraesensTextBoxes,

        /// <summary>
        /// Осмотр в отделении, st.praesens, комбобоксы
        /// </summary>
        MedicalInspectionStPraesensComboBoxes,

        /// <summary>
        /// Осмотр в отделении, st.praesens, числовые поля
        /// </summary>
        MedicalInspectionStPraesensNumericUpDowns,

        /// <summary>
        /// Осмотр в отделении, описание St. localis-а
        /// </summary>
        MedicalInspectionStLocalisDescription,

        /// <summary>
        /// Осмотр в отделении, тип рентгена
        /// </summary>
        MedicalInspectionStLocalisRentgen,

        /// <summary>
        /// Включён ли st.localis часть 1 в общий отчёт
        /// </summary>
        MedicalInspectionIsStLocalisPart1Enabled,

        /// <summary>
        /// Осмотр в отделении, st.localis part1, поля
        /// </summary>
        MedicalInspectionStLocalisPart1Fields,

        /// <summary>
        /// Осмотр в отделении, st.localis part1, номер пальца в оппозиции
        /// </summary>
        MedicalInspectionStLocalisPart1OppositionFinger,

        /// <summary>
        /// Включён ли st.localis часть 2 в общий отчёт
        /// </summary>
        MedicalInspectionIsStLocalisPart2Enabled,

        /// <summary>
        /// Осмотр в отделении, st.localis part2, какая рука(левая/правая/правая, левая)
        /// </summary>
        MedicalInspectionStLocalisPart2WhichHand,

        /// <summary>
        /// Осмотр в отделении, st.localis part2, текст боксы
        /// </summary>
        MedicalInspectionStLocalisPart2TextBoxes,

        /// <summary>
        /// Осмотр в отделении, st.localis part2, комбобоксы
        /// </summary>
        MedicalInspectionStLocalisPart2ComboBoxes,

        /// <summary>
        /// Осмотр в отделении, st.localis part2, комбобоксы для левой руки
        /// </summary>
        MedicalInspectionStLocalisPart2LeftHand,

        /// <summary>
        /// Осмотр в отделении, st.localis part2, комбобоксы для правой руки
        /// </summary>
        MedicalInspectionStLocalisPart2RightHand,

        /// <summary>
        /// Осмотр в отделении, st.localis part2, числовое поле
        /// </summary>
        MedicalInspectionStLocalisPart2NumericUpDown,

        /// <summary>
        /// Операция
        /// </summary>
        Operation,

        /// <summary>
        /// Дата операции
        /// </summary>
        OperationDateOfOperation,

        /// <summary>
        /// Время начала операции
        /// </summary>
        OperationStartTimeOfOperation,

        /// <summary>
        /// Время окончания операции
        /// </summary>
        OperationEndTimeOfOperation,

        /// <summary>
        /// Название операции
        /// </summary>
        OperationName,

        /// <summary>
        /// Список хирургов для операции
        /// </summary>
        OperationSurgeons,

        /// <summary>
        /// Список ассистентов для операции
        /// </summary>
        OperationAssistents,

        /// <summary>
        /// Анестезист операции
        /// </summary>
        OperationHeAnaesthetist,

        /// <summary>
        /// Анестезистка операции
        /// </summary>
        OperationSheAnaesthetist,

        /// <summary>
        /// Операционная мед. сестра
        /// </summary>
        OperationScrubNurse,

        /// <summary>
        /// Список типов операций
        /// </summary>
        OperationTypes,

        /// <summary>
        /// Санитар операции
        /// </summary>
        OperationOrderly,

        /// <summary>
        /// Обследование пациента
        /// </summary>
        OperationProtocolTreatmentPlanInspection,

        /// <summary>
        /// Дата написания плана обследования
        /// </summary>
        OperationProtocolTreatmentPlanDate,

        /// <summary>
        /// Активен ли план обследования в операционном протоколе
        /// </summary>
        OperationProtocolIsTreatmentPlanActiveInOperationProtocol,

        /// <summary>
        /// Время написания эпискриза
        /// </summary>
        OperationProtocolTimeWriting,

        /// <summary>
        /// Активен ли дневник
        /// </summary>
        OperationProtocolIsDairyEnabled,

        /// <summary>
        /// Температура тела
        /// </summary>
        OperationProtocolTemperature,

        /// <summary>
        /// Жалобы пациента
        /// </summary>
        OperationProtocolComplaints,

        /// <summary>
        /// Состояние пациента
        /// </summary>
        OperationProtocolState,

        /// <summary>
        /// Пульс пациента
        /// </summary>
        OperationProtocolPulse,

        /// <summary>
        /// Первое значение АД
        /// </summary>
        OperationProtocolADFirst,

        /// <summary>
        /// Второе значение АД
        /// </summary>
        OperationProtocolADSecond,

        /// <summary>
        /// ЧДД пациента
        /// </summary>
        OperationProtocolChDD,

        /// <summary>
        /// Дыхание пациента
        /// </summary>
        OperationProtocolBreath,

        /// <summary>
        /// Хрипы пациента
        /// </summary>
        OperationProtocolWheeze,

        /// <summary>
        /// Тоны сердца
        /// </summary>
        OperationProtocolHeartSounds,

        /// <summary>
        /// Ритм сердца
        /// </summary>
        OperationProtocolHeartRhythm,

        /// <summary>
        /// Живот пациента
        /// </summary>
        OperationProtocolStomach,

        /// <summary>
        /// Мочеиспускание пациента
        /// </summary>
        OperationProtocolUrination,

        /// <summary>
        /// Стул пациента
        /// </summary>
        OperationProtocolStool,

        /// <summary>
        /// St. Localis
        /// </summary>
        OperationProtocolStLocalis,

        /// <summary>
        /// Ход операции
        /// </summary>
        OperationProtocolOperationCourse,

        /// <summary>
        /// Консультация
        /// </summary>
        Visit,

        /// <summary>
        /// Диагноз для консультации
        /// </summary>
        VisitDiagnose,

        /// <summary>
        /// Поле "Объективно" для консультации
        /// </summary>
        VisitEvenly,

        /// <summary>
        /// Комментарии для консультации
        /// </summary>
        VisitComments,

        /// <summary>
        /// Врач для консультации
        /// </summary>
        VisitDoctor,

        /// <summary>
        /// Рекомендации для консультации
        /// </summary>
        VisitRecommendation,

        /// <summary>
        /// Плечевое сплетение
        /// </summary>
        BrachialPlexusCard,

        /// <summary>
        /// Картинка для этой карты
        /// </summary>
        BrachialPlexusCardPicture,

        /// <summary>
        /// К какой стороне относится карта
        /// </summary>
        BrachialPlexusCardSideOfCard,

        /// <summary>
        /// Сосудистый статус
        /// </summary>
        BrachialPlexusCardVascularStatus,

        /// <summary>
        /// Диафрагма
        /// </summary>
        BrachialPlexusCardDiaphragm,

        /// <summary>
        /// Синдром Горнера (нет/S/D)
        /// </summary>
        BrachialPlexusCardHornersSyndrome,

        /// <summary>
        /// Симптом Тинеля
        /// </summary>
        BrachialPlexusCardTinelsSymptom,

        /// <summary>
        /// Включена ли миелография
        /// </summary>
        BrachialPlexusCardIsMyelographyEnabled,

        /// <summary>
        /// Тип миелографии (КТ/ЯМРТ)
        /// </summary>
        BrachialPlexusCardMyelographyType,

        /// <summary>
        /// Дата миелографии
        /// </summary>
        BrachialPlexusCardMyelographyDate,

        /// <summary>
        /// Миелография
        /// </summary>
        BrachialPlexusCardMyelography,

        /// <summary>
        /// Включено ли ЭМНГ
        /// </summary>
        BrachialPlexusCardIsEMNGEnabled,

        /// <summary>
        /// Дата ЭМНГ
        /// </summary>
        BrachialPlexusCardEMNGDate,

        /// <summary>
        /// ЭМНГ
        /// </summary>
        BrachialPlexusCardEMNG,

        /// <summary>
        /// Карта на ккушерский паралич
        /// </summary>
        ObstetricParalysisCard,

         /// <summary>
        /// Сторона карты для акушерского паралича
        /// </summary>
        ObstetricParalysisCardSideOfCard,

        /// <summary>
        /// Информация о выделенном Global Abduction для акушерского паралича
        /// </summary>
        ObstetricParalysisCardGlobalAbduction,

        /// <summary>
        /// Информация о выделенном Global External Rotation для акушерского паралича
        /// </summary>
        ObstetricParalysisCardGlobalExternalRotation,

        /// <summary>
        /// Информация о выделенном Hand To Neck для акушерского паралича
        /// </summary>
        ObstetricParalysisCardHandToNeck,

        /// <summary>
        /// Информация о выделенном Hand To Spine для акушерского паралича
        /// </summary>
        ObstetricParalysisCardHandToSpine,

        /// <summary>
        /// Информация о выделенном Hand To Mouth для акушерского паралича
        /// </summary>
        ObstetricParalysisCardHandToMouth,

        /// <summary>
        /// Список комбобоксов для акушерского паралича
        /// </summary>
        ObstetricParalysisCardComboBoxes,

        /// <summary>
        /// Карта на объём движений
        /// </summary>
        RangeOfMotionCard,

        /// <summary>
        /// Палец в оппозиции для карты на объём движений
        /// </summary>
        RangeOfMotionCardOppositionFinger,

        /// <summary>
        /// Список полей для карты на объём движений
        /// </summary>
        RangeOfMotionCardFields,

        /// <summary>
        /// 6 однотипных карт с правой и левой половиной
        /// </summary>
        LeftRightCard,

        /// <summary>
        /// Изображение для 6-ти однотипных карт с правой и левой половиной
        /// </summary>
        LeftRightCardPicture
    }
}
