using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Карта на объём движений
    /// </summary>
    public class CRangeOfMotionCard
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
        /// Палец в оппозиции
        /// </summary>
        public string OppositionFinger;

        /// <summary>
        /// Список полей
        /// </summary>
        public string[] Fields;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CRangeOfMotionCard()
            : this(-1, -1)
        {
        }

        public CRangeOfMotionCard(int hospitalizationId, int visitId)
        {
            HospitalizationId = hospitalizationId;
            VisitId = visitId;

            OppositionFinger = "V";
            Fields = new string[62];
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = "N";
            }
        }

        public CRangeOfMotionCard(CRangeOfMotionCard rangeOfMotionCard)
        {
            HospitalizationId = rangeOfMotionCard.HospitalizationId;
            VisitId = rangeOfMotionCard.VisitId;
            OppositionFinger = rangeOfMotionCard.OppositionFinger;
            Fields = CConvertEngine.CopyArray(rangeOfMotionCard.Fields);
            NotInDatabase = rangeOfMotionCard.NotInDatabase;
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
            CRangeOfMotionCard diffRangeOfMotionCard,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            string dateHospitalizationOrVisitInfoStr = string.IsNullOrEmpty(hospitalizationDate)
                           ? "Консультация за: '" + visitDate + "'."
                           : "Госпитализация за: '" + hospitalizationDate + "'.";

            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. {2} Объект: 'Карта на объём движений'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

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
                foreignPatientMergeInfo.IdForeignVisit = diffRangeOfMotionCard.VisitId;
            }
            else
            {
                ownPatientMergeInfo.IdOwnHospitalization = HospitalizationId;
                foreignPatientMergeInfo.IdForeignHospitalization = diffRangeOfMotionCard.HospitalizationId;
            }
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной картой
        /// </summary>
        /// <param name="diffRangeOfMotionCard">Импортируемая карта объёма движений</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации (если она есть)</param>
        /// <param name="visitDate">Дата консультации (если она есть)</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CRangeOfMotionCard diffRangeOfMotionCard, 
            string patientFio, 
            string nosology, 
            string hospitalizationDate,
            string visitDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (OppositionFinger != diffRangeOfMotionCard.OppositionFinger)
            {
                CreateMergeInfos(
                   ObjectType.RangeOfMotionCardOppositionFinger,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   visitDate,
                   "Палец в оппозиции",
                   OppositionFinger,
                   diffRangeOfMotionCard.OppositionFinger,
                   diffRangeOfMotionCard,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            string ownValue;
            string foreignValue;
            if (!CCompareEngine.IsArraysEqual(Fields, diffRangeOfMotionCard.Fields, out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                  ObjectType.RangeOfMotionCardFields,
                  patientFio,
                  nosology,
                  hospitalizationDate,
                  visitDate,
                  "Список полей",
                  ownValue,
                  foreignValue,
                  diffRangeOfMotionCard,
                  out ownPatientMergeInfo,
                  out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Fields;
                foreignPatientMergeInfo.Object = diffRangeOfMotionCard.Fields;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
