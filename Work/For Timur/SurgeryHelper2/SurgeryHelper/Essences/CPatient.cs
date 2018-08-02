using System;
using System.Collections.Generic;
using System.Diagnostics;

using SurgeryHelper.Forms;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Класс с данными по пациенту
    /// </summary>
    [DebuggerDisplay("id={Id} Name={GetFullName()}")]
    public class CPatient : CIdEssence
    {
        /// <summary>
        /// Открытая для этого пациента форма с данными, если она есть
        /// </summary>
        public PatientViewForm OpenedPatientViewForm;

        /// <summary>
        /// Нозология пациента для отображения и сравнения пациентов
        /// </summary>
        public string Nosology;

        /// <summary>
        /// Список ключевых нозологий пациента
        /// </summary>
        public List<string> NosologyList;

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
        /// Дата рождения пациента
        /// </summary>
        public DateTime Birthday;

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
        /// Телефон
        /// </summary>
        public string Phone;

        /// <summary>
        /// Поле с путём до файлов данного пациента
        /// </summary>
        public string PrivateFolder;

        /// <summary>
        /// Родственники
        /// </summary>
        public string Relatives;

        /// <summary>
        /// Указан ли законный представитель
        /// </summary>
        public bool IsSpecifyLegalRepresent;

        /// <summary>
        /// ФИО законного представителя
        /// </summary>
        public string LegalRepresent;

        /// <summary>
        /// Место работы
        /// </summary>
        public string WorkPlace;        

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string EMail;

        /// <summary>
        /// Паспортные данные
        /// </summary>
        public CPassportInformation PassInformation;

        /// <summary>
        /// Серия страховой компании
        /// </summary>
        public string InsuranceSeries;

        /// <summary>
        /// Номер страховой компании
        /// </summary>
        public string InsuranceNumber;

        /// <summary>
        /// Название страховой компании
        /// </summary>
        public string InsuranceName;

        /// <summary>
        /// Вид страховой компании
        /// </summary>
        public string InsuranceType;

        /// <summary>
        /// Вернуть адрес пациента, состоящий из заполненных полей
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
        /// Вернуть полный адрес
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return LastName + " " + Name + " " + Patronymic;
        }

        public CPatient()
            : this(0)
        {
        }

        public CPatient(int patientId)
        {
            Id = patientId;
            Birthday = DateTime.Now;
            WorkPlace = string.Empty;
            IsSpecifyLegalRepresent = false;
            LegalRepresent = string.Empty;
            PassInformation = new CPassportInformation();
            NosologyList = new List<string>();
        }

        public CPatient(CPatient patientInfo)
        {
            Id = patientInfo.Id;
            LastName = patientInfo.LastName;
            Name = patientInfo.Name;
            Patronymic = patientInfo.Patronymic;
            Birthday = CConvertEngine.CopyDateTime(patientInfo.Birthday);

            BuildingNumber = patientInfo.BuildingNumber;
            CityName = patientInfo.CityName;
            FlatNumber = patientInfo.FlatNumber;
            Phone = patientInfo.Phone;
            HomeNumber = patientInfo.HomeNumber;
            Nosology = patientInfo.Nosology;
            NosologyList = new List<string>(patientInfo.NosologyList);
            StreetName = patientInfo.StreetName;
            PrivateFolder = patientInfo.PrivateFolder;
            Relatives = patientInfo.Relatives;
            IsSpecifyLegalRepresent = patientInfo.IsSpecifyLegalRepresent;
            LegalRepresent = patientInfo.LegalRepresent;
            WorkPlace = patientInfo.WorkPlace;
            EMail = patientInfo.EMail;
            PassInformation = new CPassportInformation(patientInfo.PassInformation);
            InsuranceSeries = patientInfo.InsuranceSeries;
            InsuranceNumber = patientInfo.InsuranceNumber;
            InsuranceName = patientInfo.InsuranceName;
            InsuranceType = patientInfo.InsuranceType;
        }


        public static int Compare(CPatient patientInfo1, CPatient patientInfo2)
        {
            return string.CompareOrdinal(patientInfo1.GetFullName(), patientInfo2.GetFullName());
        }

        private void CreateMergeInfos(
            ObjectType objectType, 
            string parameterName, 
            string ownValue,
            string foreignValue, 
            CPatient diffPatient, 
            out CMergeInfo ownPatientMergeInfo, 
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология:'{1}'.\r\nНазвание параметра: '{2}'. Значение: '{3}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnPatient = Id,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, GetFullName(), Nosology, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignPatient = diffPatient.Id,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, diffPatient.GetFullName(), diffPatient.Nosology, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и переданным пациентом
        /// </summary>
        /// <param name="diffPatient">Переданный (импортируемый) пациент</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(CPatient diffPatient, CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;
            if (CCompareEngine.CompareDate(Birthday, diffPatient.Birthday) != 0)
            {
                CreateMergeInfos(
                    ObjectType.PatientBirthday, 
                    "День рождения", 
                    CConvertEngine.DateTimeToString(Birthday), 
                    CConvertEngine.DateTimeToString(diffPatient.Birthday), 
                    diffPatient, 
                    out ownPatientMergeInfo, 
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CityName != diffPatient.CityName)
            {
                CreateMergeInfos(
                   ObjectType.PatientCityName,
                   "Город проживания",
                   CityName,
                   diffPatient.CityName,
                   diffPatient,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (StreetName != diffPatient.StreetName)
            {
                CreateMergeInfos(
                    ObjectType.PatientStreetName,
                    "Улица проживания",
                    StreetName,
                    diffPatient.StreetName,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (HomeNumber != diffPatient.HomeNumber)
            {
                CreateMergeInfos(
                    ObjectType.PatientHomeNumber,
                    "Номер дома",
                    HomeNumber,
                    diffPatient.HomeNumber,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (BuildingNumber != diffPatient.BuildingNumber)
            {
                CreateMergeInfos(
                    ObjectType.PatientBuildingNumber,
                    "Номер корпуса",
                    BuildingNumber,
                    diffPatient.BuildingNumber,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (FlatNumber != diffPatient.FlatNumber)
            {
                CreateMergeInfos(
                    ObjectType.PatientFlatNumber,
                    "Номер квартиры",
                    FlatNumber,
                    diffPatient.FlatNumber,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Phone != diffPatient.Phone)
            {
                CreateMergeInfos(
                    ObjectType.PatientPhone,
                    "Телефон",
                    Phone,
                    diffPatient.Phone,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Relatives != diffPatient.Relatives)
            {
                CreateMergeInfos(
                    ObjectType.PatientRelatives,
                    "Родственники",
                    Relatives,
                    diffPatient.Relatives,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsSpecifyLegalRepresent != diffPatient.IsSpecifyLegalRepresent)
            {
                CreateMergeInfos(
                    ObjectType.PatientIsSpecifyLegalRepresent,
                    "Наличие легального представителя",
                    IsSpecifyLegalRepresent ? "Указан" : "Не указан",
                    diffPatient.IsSpecifyLegalRepresent ? "Указан" : "Не указан",
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (LegalRepresent != diffPatient.LegalRepresent)
            {
                CreateMergeInfos(
                    ObjectType.PatientLegalRepresent,
                    "Законный представитель",
                    LegalRepresent,
                    diffPatient.LegalRepresent,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (WorkPlace != diffPatient.WorkPlace)
            {
                CreateMergeInfos(
                    ObjectType.PatientWorkPlace,
                    "Место работы",
                    WorkPlace,
                    diffPatient.WorkPlace,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (EMail != diffPatient.EMail)
            {
                CreateMergeInfos(
                    ObjectType.PatientEMail,
                    "Адрес электронной почты",
                    EMail,
                    diffPatient.EMail,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (InsuranceSeries != diffPatient.InsuranceSeries)
            {
                CreateMergeInfos(
                    ObjectType.PatientInsuranceSeries,
                    "Серия страховой компании",
                    InsuranceSeries,
                    diffPatient.InsuranceSeries,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (InsuranceNumber != diffPatient.InsuranceNumber)
            {
                CreateMergeInfos(
                    ObjectType.PatientInsuranceNumber,
                    "Номер страховой компании",
                    InsuranceNumber,
                    diffPatient.InsuranceNumber,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (InsuranceName != diffPatient.InsuranceName)
            {
                CreateMergeInfos(
                    ObjectType.PatientInsuranceName,
                    "Название страховой компании",
                    InsuranceName,
                    diffPatient.InsuranceName,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (InsuranceType != diffPatient.InsuranceType)
            {
                CreateMergeInfos(
                    ObjectType.PatientInsuranceType,
                    "Вид страховой компании",
                    InsuranceType,
                    diffPatient.InsuranceType,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (PassInformation.Series != diffPatient.PassInformation.Series)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationSeries,
                    "Серия паспорта",
                    PassInformation.Series,
                    diffPatient.PassInformation.Series,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (PassInformation.Number != diffPatient.PassInformation.Number)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationNumber,
                    "Номер паспорта",
                    PassInformation.Number,
                    diffPatient.PassInformation.Number,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (PassInformation.SubdivisionCode != diffPatient.PassInformation.SubdivisionCode)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationSubdivisionCode,
                    "Код подразделения",
                    PassInformation.SubdivisionCode,
                    diffPatient.PassInformation.SubdivisionCode,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (PassInformation.Organization != diffPatient.PassInformation.Organization)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationOrganization,
                    "Организация, выдавшая паспорт",
                    PassInformation.Organization,
                    diffPatient.PassInformation.Organization,
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (PassInformation.DeliveryDate.HasValue && !diffPatient.PassInformation.DeliveryDate.HasValue)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationDeliveryDate,
                    "Дата выдачи паспотра",
                    CConvertEngine.DateTimeToString(PassInformation.DeliveryDate.Value, false),
                    "Нет значения",
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (!PassInformation.DeliveryDate.HasValue && diffPatient.PassInformation.DeliveryDate.HasValue)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationDeliveryDate,
                    "Дата выдачи паспотра",
                    "Нет значения",
                    CConvertEngine.DateTimeToString(diffPatient.PassInformation.DeliveryDate.Value, false),
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (PassInformation.DeliveryDate.HasValue && diffPatient.PassInformation.DeliveryDate.HasValue &&
                CCompareEngine.CompareDate(PassInformation.DeliveryDate.Value, diffPatient.PassInformation.DeliveryDate.Value) != 0)
            {
                CreateMergeInfos(
                    ObjectType.PatientPassInformationDeliveryDate,
                    "Дата выдачи паспотра",
                    CConvertEngine.DateTimeToString(PassInformation.DeliveryDate.Value, false),
                    CConvertEngine.DateTimeToString(diffPatient.PassInformation.DeliveryDate.Value, false),
                    diffPatient,
                    out ownPatientMergeInfo,
                    out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }       
    }
}
