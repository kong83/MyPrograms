using System.Drawing;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Карта обследования
    /// </summary>
    public class CCard
    {
        /// <summary>
        /// id госпитализации, если эта карта относится к госпитализации, иначе = -1
        /// </summary>
        public int HospitalizationId;

        /// <summary>
        /// id консультации, если эта карта относится к консультации, иначе = -1
        /// </summary>
        public int VisitId;

        /// <summary>
        /// Картинка для этой карты
        /// </summary>
        public Bitmap Picture;

        /// <summary>
        /// Тип карты
        /// </summary>
        public CardType CurrentCardType;

        /// <summary>
        /// К какой стороне относится карта
        /// </summary>
        public CardSide CurrentSideOfCard;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        /// <summary>
        /// Вернуть имя файла, в котором должна храниться картинка
        /// </summary>
        /// <returns></returns>
        public string GetPictureFileName()
        {
            return HospitalizationId + "_" + VisitId + "_" + CurrentSideOfCard + "_" + CurrentCardType + ".png";
        }

        public CCard()
        {
            HospitalizationId = -1;
            VisitId = -1;
        }

        public CCard(CCard cardInfo)
        {
            HospitalizationId = cardInfo.HospitalizationId;
            VisitId = cardInfo.VisitId;
            CurrentCardType = cardInfo.CurrentCardType;
            CurrentSideOfCard = cardInfo.CurrentSideOfCard;
            Picture = new Bitmap(cardInfo.Picture);
            NotInDatabase = cardInfo.NotInDatabase;
        }

        public static string GetDescription<T>(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayText), false) as DisplayText[];

            if (descriptionAttributes == null || descriptionAttributes.Length == 0)
            {
                return value.ToString();
            }

            return descriptionAttributes[0].Text;
        }

        /// <summary>
        /// Возвращает true, если у переданного объекта все данные (кроме картинки) совпадают
        /// </summary>
        /// <param name="cardInfo">Объект для сравнения</param>
        /// <returns></returns>
        public bool IsEqual(CCard cardInfo)
        { 
            return HospitalizationId == cardInfo.HospitalizationId &&
                   VisitId == cardInfo.VisitId &&
                   CurrentCardType == cardInfo.CurrentCardType &&
                   CurrentSideOfCard == cardInfo.CurrentSideOfCard;
        }


        private void CreateMergeInfos(
           ObjectType objectType,
           string patientFio,
           string nosology,
           string hospitalizationDate,
           string visitDate,
           string parameterName,
           string ownValue,
           string foreignValue,
           CCard diffCard,
           out CMergeInfo ownPatientMergeInfo,
           out CMergeInfo foreignPatientMergeInfo)
        {
            string dateHospitalizationOrVisitInfoStr = string.IsNullOrEmpty(hospitalizationDate)
                           ? "Консультация за: '" + visitDate + "'."
                           : "Госпитализация за: '" + hospitalizationDate + "'.";

            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. {2} Объект: '{3}. {4} сторона'.\r\nНазвание параметра: '{5}'. Значение: '{6}'";
           
            ownPatientMergeInfo = new CMergeInfo
            {
                TypeOfObject = objectType,
                Value = ownValue,
                TypeOfCard = CurrentCardType,
                SideOfCard = CurrentSideOfCard,
                Difference = string.Format(differenceStr, patientFio, nosology, dateHospitalizationOrVisitInfoStr, GetDescription(CurrentCardType), GetDescription(CurrentSideOfCard), parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                TypeOfObject = objectType,
                Value = foreignValue,
                TypeOfCard = diffCard.CurrentCardType,
                SideOfCard = diffCard.CurrentSideOfCard,
                Difference = string.Format(differenceStr, patientFio, nosology, dateHospitalizationOrVisitInfoStr, GetDescription(CurrentCardType), GetDescription(CurrentSideOfCard), parameterName, foreignValue)
            };

            if (string.IsNullOrEmpty(hospitalizationDate))
            {
                ownPatientMergeInfo.IdOwnVisit = VisitId;
                foreignPatientMergeInfo.IdForeignVisit = diffCard.VisitId;
            }
            else
            {
                ownPatientMergeInfo.IdOwnHospitalization = HospitalizationId;
                foreignPatientMergeInfo.IdForeignHospitalization = diffCard.HospitalizationId;
            }
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной картой
        /// </summary>
        /// <param name="diffCard">Импортируемая карта</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации (если она есть)</param>
        /// <param name="visitDate">Дата консультации (если она есть)</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CCard diffCard, 
            string patientFio, 
            string nosology, 
            string hospitalizationDate,
            string visitDate,
            CDatabasesMerger databasesMerger)
        {
            if (CCompareEngine.IsBitmapsDifferent(Picture, diffCard.Picture))
            {
                CMergeInfo ownPatientMergeInfo;
                CMergeInfo foreignPatientMergeInfo;

                CreateMergeInfos(
                   ObjectType.LeftRightCardPicture,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Изображение",
                   "Смотри картинку",
                   "Смотри картинку",
                   diffCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Picture;
                foreignPatientMergeInfo.Object = diffCard.Picture;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
