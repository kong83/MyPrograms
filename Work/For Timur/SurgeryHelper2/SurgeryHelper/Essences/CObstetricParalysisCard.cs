using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Карта на акушерский паралич
    /// </summary>
    public class CObstetricParalysisCard
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
        /// Сторона карты
        /// </summary>
        public CardSide SideOfCard;

        /// <summary>
        /// Информация о выделенном Global Abduction
        /// </summary>
        public bool[] GlobalAbductionPicturesSelection;

        /// <summary>
        /// Информация о выделенном Global External Rotation
        /// </summary>
        public bool[] GlobalExternalRotationPicturesSelection;

        /// <summary>
        /// Информация о выделенном Hand To Neck
        /// </summary>
        public bool[] HandToNeckPicturesSelection;

        /// <summary>
        /// Информация о выделенном Hand To Spine
        /// </summary>
        public bool[] HandToSpinePicturesSelection;

        /// <summary>
        /// Информация о выделенном Hand To Mouth
        /// </summary>
        public bool[] HandToMouthPicturesSelection;

        /// <summary>
        /// Список комбобоксов
        /// </summary>
        public string[] ComboBoxes;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CObstetricParalysisCard()
            : this(-1, -1)
        {
        }

        public CObstetricParalysisCard(int hospitalizationId, int visitId)
        {
            HospitalizationId = hospitalizationId;
            VisitId = visitId;
            SideOfCard = CardSide.Left;

            GlobalAbductionPicturesSelection = new bool[5];
            GlobalAbductionPicturesSelection[0] = true;

            GlobalExternalRotationPicturesSelection = new bool[5];
            GlobalExternalRotationPicturesSelection[0] = true;

            HandToNeckPicturesSelection = new bool[5];
            HandToNeckPicturesSelection[0] = true;

            HandToSpinePicturesSelection = new bool[5];
            HandToSpinePicturesSelection[0] = true;

            HandToMouthPicturesSelection = new bool[5];
            HandToMouthPicturesSelection[0] = true;

            ComboBoxes = new string[7];
            for (int i = 0; i < ComboBoxes.Length; i++)
            {
                ComboBoxes[i] = "да";
            }
        }

        public CObstetricParalysisCard(CObstetricParalysisCard obstetricParalysisCard)
        {
            HospitalizationId = obstetricParalysisCard.HospitalizationId;
            VisitId = obstetricParalysisCard.VisitId;
            SideOfCard = obstetricParalysisCard.SideOfCard;
            GlobalAbductionPicturesSelection = CConvertEngine.CopyArray(obstetricParalysisCard.GlobalAbductionPicturesSelection);
            GlobalExternalRotationPicturesSelection = CConvertEngine.CopyArray(obstetricParalysisCard.GlobalExternalRotationPicturesSelection);
            HandToNeckPicturesSelection = CConvertEngine.CopyArray(obstetricParalysisCard.HandToNeckPicturesSelection);
            HandToSpinePicturesSelection = CConvertEngine.CopyArray(obstetricParalysisCard.HandToSpinePicturesSelection);
            HandToMouthPicturesSelection = CConvertEngine.CopyArray(obstetricParalysisCard.HandToMouthPicturesSelection);
            ComboBoxes = CConvertEngine.CopyArray(obstetricParalysisCard.ComboBoxes);
            NotInDatabase = obstetricParalysisCard.NotInDatabase;
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
            CObstetricParalysisCard diffObstetricParalysisCard,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            string dateHospitalizationOrVisitInfoStr = string.IsNullOrEmpty(hospitalizationDate)
                           ? "Консультация за: '" + visitDate + "'."
                           : "Госпитализация за: '" + hospitalizationDate + "'.";

            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. {2} Объект: 'Карта для акушерского паралича'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

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
                foreignPatientMergeInfo.IdForeignVisit = diffObstetricParalysisCard.VisitId;
            }
            else
            {
                ownPatientMergeInfo.IdOwnHospitalization = HospitalizationId;
                foreignPatientMergeInfo.IdForeignHospitalization = diffObstetricParalysisCard.HospitalizationId;
            }
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной картой
        /// </summary>
        /// <param name="diffObstetricParalysisCard">Импортируемая карта</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации (если она есть)</param>
        /// <param name="visitDate">Дата консультации (если она есть)</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CObstetricParalysisCard diffObstetricParalysisCard, 
            string patientFio, 
            string nosology, 
            string hospitalizationDate,
            string visitDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (SideOfCard != diffObstetricParalysisCard.SideOfCard)
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardSideOfCard,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Сторона",
                   SideOfCard.ToString(),
                   diffObstetricParalysisCard.SideOfCard.ToString(),
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            string ownValue;
            string foreignValue;
            if (!CCompareEngine.IsArraysEqual(GlobalAbductionPicturesSelection, diffObstetricParalysisCard.GlobalAbductionPicturesSelection, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardGlobalAbduction,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о Global Abduction",
                   ownValue,
                   foreignValue,
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = GlobalAbductionPicturesSelection;
                foreignPatientMergeInfo.Object = diffObstetricParalysisCard.GlobalAbductionPicturesSelection;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(GlobalExternalRotationPicturesSelection, diffObstetricParalysisCard.GlobalExternalRotationPicturesSelection, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardGlobalExternalRotation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о Global External Rotation",
                   ownValue,
                   foreignValue,
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = GlobalExternalRotationPicturesSelection;
                foreignPatientMergeInfo.Object = diffObstetricParalysisCard.GlobalExternalRotationPicturesSelection;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(HandToNeckPicturesSelection, diffObstetricParalysisCard.HandToNeckPicturesSelection, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardHandToNeck,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о Hand To Neck",
                   ownValue,
                   foreignValue,
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = HandToNeckPicturesSelection;
                foreignPatientMergeInfo.Object = diffObstetricParalysisCard.HandToNeckPicturesSelection;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(HandToSpinePicturesSelection, diffObstetricParalysisCard.HandToSpinePicturesSelection, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardHandToSpine,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о Hand To Spine",
                   ownValue,
                   foreignValue,
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = HandToSpinePicturesSelection;
                foreignPatientMergeInfo.Object = diffObstetricParalysisCard.HandToSpinePicturesSelection;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(HandToMouthPicturesSelection, diffObstetricParalysisCard.HandToMouthPicturesSelection, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardHandToMouth,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Информация о Hand To Mouth",
                   ownValue,
                   foreignValue,
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = HandToMouthPicturesSelection;
                foreignPatientMergeInfo.Object = diffObstetricParalysisCard.HandToMouthPicturesSelection;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(ComboBoxes, diffObstetricParalysisCard.ComboBoxes, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.ObstetricParalysisCardComboBoxes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Список комбобоксов",
                   ownValue,
                   foreignValue,
                   diffObstetricParalysisCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = ComboBoxes;
                foreignPatientMergeInfo.Object = diffObstetricParalysisCard.ComboBoxes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
