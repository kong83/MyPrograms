using System;
using System.Collections.Generic;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Выписной эпикриз
    /// </summary>
    public class CDischargeEpicrisis
    {
        /// <summary>
        /// Указатель на id госпитализации, к которой относится данный выписной эпикриз
        /// </summary>
        public int HospitalizationId;

        /// <summary>
        /// Консервативное лечение
        /// </summary>
        public string ConservativeTherapy;

        /// <summary>
        /// Дата взятия анализов
        /// </summary>
        public DateTime? AnalysisDate;

        /// <summary>
        /// После операции
        /// </summary>
        public string AfterOperation;

        /// <summary>
        /// Общий анализ крови, эритроциты
        /// </summary>
        public string OakEritrocits;

        /// <summary>
        /// Общий анализ крови, лекоциты
        /// </summary>
        public string OakLekocits;

        /// <summary>
        /// Общий анализ крови, Hb
        /// </summary>
        public string OakHb;

        /// <summary>
        /// Общий анализ крови, СОЭ
        /// </summary>
        public string OakSoe;

        /// <summary>
        /// Общий анализ мочи, цвет
        /// </summary>
        public string OamColor;

        /// <summary>
        /// Общий анализ мочи, относительная плотность
        /// </summary>
        public string OamDensity;

        /// <summary>
        /// Общий анализ мочи, эритроциты
        /// </summary>
        public string OamEritrocits;

        /// <summary>
        /// Общий анализ мочи, лейкоциты
        /// </summary>
        public string OamLekocits;

        /// <summary>
        /// Общий анализ мочи, другие анализы
        /// </summary>
        public string AdditionalAnalises;

        /// <summary>
        /// ЭКГ пациента
        /// </summary>
        public string Ekg;

        /// <summary>
        /// Рекомендации при выписке
        /// </summary>
        public List<string> Recomendations;

        /// <summary>
        /// Дополнительные рекомендации при выписке
        /// </summary>
        public List<string> AdditionalRecomendations;

        /// <summary>
        /// Сохранён ли объект в базе
        /// </summary>
        public bool NotInDatabase;

        public CDischargeEpicrisis()
            : this(0)
        {
        }

        public CDischargeEpicrisis(int hospitalizationId)
        {
            HospitalizationId = hospitalizationId;
            AnalysisDate = DateTime.Now;
            AfterOperation = "раны зажили первичным натяжением, швы сняты.";
            ConservativeTherapy = "S. Ceftriaxoni 1,0 – 1 раз в день в/м 3 дня, S. Ketoroli 1,0 – 3 раза в день в/м 3 дня";
            OamColor = "с/ж";
            OamDensity = "1015";
            OamEritrocits = "нет";
            OamLekocits = "нет";
            Ekg = "без патологии";
            Recomendations = new List<string> { "notdefined" };
            AdditionalRecomendations = new List<string>();
        }

        public CDischargeEpicrisis(CDischargeEpicrisis dischargeEpicris)
        {
            HospitalizationId = dischargeEpicris.HospitalizationId;
            AnalysisDate = dischargeEpicris.AnalysisDate;
            AfterOperation = dischargeEpicris.AfterOperation;
            ConservativeTherapy = dischargeEpicris.ConservativeTherapy;
            Ekg = dischargeEpicris.Ekg;
            OakEritrocits = dischargeEpicris.OakEritrocits;
            OakHb = dischargeEpicris.OakHb;
            OakLekocits = dischargeEpicris.OakLekocits;
            OakSoe = dischargeEpicris.OakSoe;
            OamColor = dischargeEpicris.OamColor;
            OamDensity = dischargeEpicris.OamDensity;
            OamEritrocits = dischargeEpicris.OamEritrocits;
            OamLekocits = dischargeEpicris.OamLekocits;
            AdditionalAnalises = dischargeEpicris.AdditionalAnalises;
            Recomendations = new List<string>(dischargeEpicris.Recomendations);
            AdditionalRecomendations = new List<string>(dischargeEpicris.AdditionalRecomendations);
            NotInDatabase = dischargeEpicris.NotInDatabase;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string parameterName,
            string ownValue,
            string foreignValue,
            CDischargeEpicrisis diffDischargeEpicrisis,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'.  Дата госпитализации: '{2}'. Объект: 'Выписной эпикриз'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnHospitalization = HospitalizationId,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignHospitalization = diffDischargeEpicrisis.HospitalizationId,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущим и переданным 
        /// выписным эпикризом
        /// </summary>
        /// <param name="diffDischargeEpicrisis">Импортируемый выписной эпикриз</param>
        /// <param name="patientFio">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата госпитализации</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            CDischargeEpicrisis diffDischargeEpicrisis,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;


            if (AnalysisDate.HasValue && diffDischargeEpicrisis.AnalysisDate.HasValue &&
                CCompareEngine.CompareDate(AnalysisDate.Value, diffDischargeEpicrisis.AnalysisDate.Value) != 0)
            {
            
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisAfterOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дата взятия анализов",
                   CConvertEngine.DateTimeToString(AnalysisDate.Value),
                   CConvertEngine.DateTimeToString(diffDischargeEpicrisis.AnalysisDate.Value),
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (AfterOperation != diffDischargeEpicrisis.AfterOperation)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisAfterOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "После операции",
                   AfterOperation,
                   diffDischargeEpicrisis.AfterOperation,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ConservativeTherapy != diffDischargeEpicrisis.ConservativeTherapy)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisConservativeTherapy,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Консервативное лечение",
                   ConservativeTherapy,
                   diffDischargeEpicrisis.ConservativeTherapy,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Ekg != diffDischargeEpicrisis.Ekg)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisEkg,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "ЭКГ",
                   Ekg,
                   diffDischargeEpicrisis.Ekg,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OakEritrocits != diffDischargeEpicrisis.OakEritrocits)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOakEritrocits,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ крови, эритроциты",
                   OakEritrocits,
                   diffDischargeEpicrisis.OakEritrocits,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OakHb != diffDischargeEpicrisis.OakHb)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOakHb,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ крови, Hb",
                   OakHb,
                   diffDischargeEpicrisis.OakHb,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OakLekocits != diffDischargeEpicrisis.OakLekocits)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOakLekocits,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ крови, лейкоциты",
                   OakLekocits,
                   diffDischargeEpicrisis.OakLekocits,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OakSoe != diffDischargeEpicrisis.OakSoe)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOakSoe,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ крови, СОЭ",
                   OakSoe,
                   diffDischargeEpicrisis.OakSoe,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OamColor != diffDischargeEpicrisis.OamColor)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOamColor,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ мочи, цвет",
                   OamColor,
                   diffDischargeEpicrisis.OamColor,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OamDensity != diffDischargeEpicrisis.OamDensity)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOamDensity,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ мочи, относительная плотность",
                   OamDensity,
                   diffDischargeEpicrisis.OamDensity,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OamEritrocits != diffDischargeEpicrisis.OamEritrocits)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOamEritrocits,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ мочи, эритроциты",
                   OamEritrocits,
                   diffDischargeEpicrisis.OamEritrocits,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (OamLekocits != diffDischargeEpicrisis.OamLekocits)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisOamLekocits,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Общий анализ мочи, лейкоциты",
                   OamLekocits,
                   diffDischargeEpicrisis.OamLekocits,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (AdditionalAnalises != diffDischargeEpicrisis.AdditionalAnalises)
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisAdditionalAnalises,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Другие анализы",
                   AdditionalAnalises,
                   diffDischargeEpicrisis.AdditionalAnalises,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            string ownValue;
            string foreignValue;
            if (!CCompareEngine.IsArraysEqual(Recomendations.ToArray(), diffDischargeEpicrisis.Recomendations.ToArray(), out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisRecomendations,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Рекомендации при выписке",
                   ownValue,
                   foreignValue,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Recomendations;
                foreignPatientMergeInfo.Object = diffDischargeEpicrisis.Recomendations;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(AdditionalRecomendations.ToArray(), diffDischargeEpicrisis.AdditionalRecomendations.ToArray(), out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.DischargeEpicrisisAdditionalRecomendations,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дополнительные рекомендации при выписке",
                   ownValue,
                   foreignValue,
                   diffDischargeEpicrisis,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = AdditionalRecomendations;
                foreignPatientMergeInfo.Object = diffDischargeEpicrisis.AdditionalRecomendations;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
