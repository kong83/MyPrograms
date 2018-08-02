using System;
using System.Drawing;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Карта на плечевое сплетение
    /// </summary>
    public class CBrachialPlexusCard
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
        /// К какой стороне относится карта
        /// </summary>
        public CardSide SideOfCard;

        /// <summary>
        /// Сосудистый статус
        /// </summary>
        public string VascularStatus;

        /// <summary>
        /// Диафрагма
        /// </summary>
        public string Diaphragm;

        /// <summary>
        /// Синдром Горнера (нет/S/D)
        /// </summary>
        public string HornersSyndrome;

        /// <summary>
        /// Симптом Тинеля
        /// </summary>
        public string TinelsSymptom;

        /// <summary>
        /// Включена ли миелография
        /// </summary>
        public bool IsMyelographyEnabled;

        /// <summary>
        /// Тип миелографии (КТ/ЯМРТ)
        /// </summary>
        public string MyelographyType;

        /// <summary>
        /// Дата миелографии
        /// </summary>
        public DateTime MyelographyDate;

        /// <summary>
        /// Миелография
        /// </summary>
        public string Myelography;

        /// <summary>
        /// Включено ли ЭМНГ
        /// </summary>
        public bool IsEMNGEnabled;

        /// <summary>
        /// Дата ЭМНГ
        /// </summary>
        public DateTime EMNGDate;

        /// <summary>
        /// ЭМНГ
        /// </summary>
        public string EMNG;

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
            return HospitalizationId + "_" + VisitId + "_" + "BrachialPlexus.png";
        }

        public CBrachialPlexusCard()
            : this(-1, -1)
        {
        }

        public CBrachialPlexusCard(int hospitalizationId, int visitId)
        {
            HospitalizationId = hospitalizationId;
            VisitId = visitId;
            SideOfCard = CardSide.Left;
            VascularStatus = "N";
            Diaphragm = "N";
            HornersSyndrome = "нет";
            TinelsSymptom = "N";
            IsMyelographyEnabled = false;
            Myelography = "N";
            MyelographyType = "КТ";
            IsEMNGEnabled = false;
            EMNG = "N";
            MyelographyDate = DateTime.Now;
            EMNGDate = DateTime.Now;
        }

        public CBrachialPlexusCard(CBrachialPlexusCard brachialPlexusCardInfo)
        {
            HospitalizationId = brachialPlexusCardInfo.HospitalizationId;
            VisitId = brachialPlexusCardInfo.VisitId;            
            SideOfCard = brachialPlexusCardInfo.SideOfCard;
            Picture = new Bitmap(brachialPlexusCardInfo.Picture);
            Diaphragm = brachialPlexusCardInfo.Diaphragm;
            EMNG = brachialPlexusCardInfo.EMNG;
            EMNGDate = CConvertEngine.CopyDateTime(brachialPlexusCardInfo.EMNGDate);
            HornersSyndrome = brachialPlexusCardInfo.HornersSyndrome;
            IsEMNGEnabled = brachialPlexusCardInfo.IsEMNGEnabled;
            IsMyelographyEnabled = brachialPlexusCardInfo.IsMyelographyEnabled;
            Myelography = brachialPlexusCardInfo.Myelography;
            MyelographyDate = CConvertEngine.CopyDateTime(brachialPlexusCardInfo.MyelographyDate);
            MyelographyType = brachialPlexusCardInfo.MyelographyType;
            TinelsSymptom = brachialPlexusCardInfo.TinelsSymptom;
            VascularStatus = brachialPlexusCardInfo.VascularStatus;
            NotInDatabase = brachialPlexusCardInfo.NotInDatabase;
        }

        /// <summary>
        /// Возвращает true, если у переданного объекта все данные (кроме картинки) совпадают
        /// </summary>
        /// <param name="brachialPlexusCardInfo">Объект для сравнения</param>
        /// <returns></returns>
        public bool IsEqual(CBrachialPlexusCard brachialPlexusCardInfo)
        {
            return HospitalizationId == brachialPlexusCardInfo.HospitalizationId &&
                   VisitId == brachialPlexusCardInfo.VisitId &&
                   SideOfCard == brachialPlexusCardInfo.SideOfCard &&
                   Diaphragm == brachialPlexusCardInfo.Diaphragm &&
                   EMNG == brachialPlexusCardInfo.EMNG &&
                   CCompareEngine.CompareDate(EMNGDate, brachialPlexusCardInfo.EMNGDate) == 0 &&
                   HornersSyndrome == brachialPlexusCardInfo.HornersSyndrome &&
                   IsEMNGEnabled == brachialPlexusCardInfo.IsEMNGEnabled &&
                   IsMyelographyEnabled == brachialPlexusCardInfo.IsMyelographyEnabled &&
                   Myelography == brachialPlexusCardInfo.Myelography &&
                   CCompareEngine.CompareDate(MyelographyDate, brachialPlexusCardInfo.MyelographyDate) == 0 &&
                   MyelographyType == brachialPlexusCardInfo.MyelographyType &&
                   TinelsSymptom == brachialPlexusCardInfo.TinelsSymptom &&
                   VascularStatus == brachialPlexusCardInfo.VascularStatus;
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
            CBrachialPlexusCard diffBrachialPlexusCard,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            string dateHospitalizationOrVisitInfoStr = string.IsNullOrEmpty(hospitalizationDate)
                           ? "Консультация за: '" + visitDate + "'."
                           : "Госпитализация за: '" + hospitalizationDate + "'.";

            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. {2} Объект: 'Карта для плечевого сплетения'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, dateHospitalizationOrVisitInfoStr, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, dateHospitalizationOrVisitInfoStr, parameterName, foreignValue)
            };

            if (string.IsNullOrEmpty(hospitalizationDate))
            {
                ownPatientMergeInfo.IdOwnVisit = VisitId;
                foreignPatientMergeInfo.IdForeignVisit = diffBrachialPlexusCard.VisitId;
            }
            else
            {
                ownPatientMergeInfo.IdOwnHospitalization = HospitalizationId;
                foreignPatientMergeInfo.IdForeignHospitalization = diffBrachialPlexusCard.HospitalizationId;
            }
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной картой
        /// </summary>
        /// <param name="diffBrachialPlexusCard">Импортируемая карта плечевого сплетения</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации (если она есть)</param>
        /// <param name="visitDate">Дата консультации (если она есть)</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CBrachialPlexusCard diffBrachialPlexusCard, 
            string patientFio, 
            string nosology, 
            string hospitalizationDate, 
            string visitDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (SideOfCard != diffBrachialPlexusCard.SideOfCard)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardSideOfCard,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Сторона",
                   SideOfCard.ToString(),
                   diffBrachialPlexusCard.SideOfCard.ToString(),
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (VascularStatus != diffBrachialPlexusCard.VascularStatus)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardVascularStatus,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Сосудистый статус",
                   VascularStatus,
                   diffBrachialPlexusCard.VascularStatus,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Diaphragm != diffBrachialPlexusCard.Diaphragm)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardDiaphragm,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Диафрагма",
                   Diaphragm,
                   diffBrachialPlexusCard.Diaphragm,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (HornersSyndrome != diffBrachialPlexusCard.HornersSyndrome)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardHornersSyndrome,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Синдром Горнера",
                   HornersSyndrome,
                   diffBrachialPlexusCard.HornersSyndrome,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (TinelsSymptom != diffBrachialPlexusCard.TinelsSymptom)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardTinelsSymptom,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Симптом Тинеля",
                   TinelsSymptom,
                   diffBrachialPlexusCard.TinelsSymptom,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsMyelographyEnabled != diffBrachialPlexusCard.IsMyelographyEnabled)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardIsMyelographyEnabled,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о том, включена ли миелография",
                   IsMyelographyEnabled.ToString(),
                   diffBrachialPlexusCard.IsMyelographyEnabled.ToString(),
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (MyelographyType != diffBrachialPlexusCard.MyelographyType)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardMyelographyType,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Тип миелографии",
                   MyelographyType,
                   diffBrachialPlexusCard.MyelographyType,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(MyelographyDate, diffBrachialPlexusCard.MyelographyDate) != 0)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardMyelographyDate,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Дата миелографии",
                   CConvertEngine.DateTimeToString(MyelographyDate),
                   CConvertEngine.DateTimeToString(diffBrachialPlexusCard.MyelographyDate),
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Myelography != diffBrachialPlexusCard.Myelography)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardMyelography,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Миелография",
                   Myelography,
                   diffBrachialPlexusCard.Myelography,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (IsEMNGEnabled != diffBrachialPlexusCard.IsEMNGEnabled)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardIsEMNGEnabled,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о том, включено ли ЭМНГ",
                   IsEMNGEnabled.ToString(),
                   diffBrachialPlexusCard.IsEMNGEnabled.ToString(),
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(EMNGDate, diffBrachialPlexusCard.EMNGDate) != 0)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardEMNGDate,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Дата ЭМНГ",
                   CConvertEngine.DateTimeToString(EMNGDate),
                   CConvertEngine.DateTimeToString(diffBrachialPlexusCard.EMNGDate),
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (EMNG != diffBrachialPlexusCard.EMNG)
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardEMNG,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "ЭМНГ",
                   EMNG,
                   diffBrachialPlexusCard.EMNG,
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.IsBitmapsDifferent(Picture, diffBrachialPlexusCard.Picture))
            {
                CreateMergeInfos(
                   ObjectType.BrachialPlexusCardPicture,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Изображение",
                   "Смотри картинку",
                   "Смотри картинку",
                   diffBrachialPlexusCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Picture;
                foreignPatientMergeInfo.Object = diffBrachialPlexusCard.Picture;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
