using System;

using SurgeryHelper.Forms;
using SurgeryHelper.Tools;
using System.Diagnostics;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Информация о консультации
    /// </summary>
    [DebuggerDisplay("id={Id} PatientId={PatientId} DiagnoseOneLine={DiagnoseOneLine}")]
    public class CVisit : CIdEssence
    {
        public VisitViewForm OpenedVisitViewForm;

        /// <summary>
        /// Ссылка на id пациента, к которому относится данная консультация
        /// </summary>
        public int PatientId;

        /// <summary>
        /// Дата и время консультации
        /// </summary>
        public DateTime VisitDate;

        /// <summary>
        /// Диагноз
        /// </summary>
        public string Diagnose;

        /// <summary>
        /// Диагноз пациента без переносов строк
        /// </summary>
        public string DiagnoseOneLine
        {
            get
            {
                if (!string.IsNullOrEmpty(Diagnose))
                {
                    return Diagnose.Replace("\r\n", " ");
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Объективно
        /// </summary>
        public string Evenly;

        /// <summary>
        /// Рекомендации
        /// </summary>
        public string Recommendation;

        /// <summary>
        /// Комментарии
        /// </summary>
        public string Comments;

        /// <summary>
        /// Доктор
        /// </summary>
        public string Doctor;

        /// <summary>
        /// Шапка для доктора (не сохраняется в базу, используется только для генерации вордовского
        /// документа)
        /// </summary>
        public string Header;

        /// <summary>
        /// Нужен ли последний параграф в документе со справкой (Необходимое обследование для оперативного лечения)
        /// </summary>
        public bool IsLastParagraphForCertificateNeeded;

        /// <summary>
        /// Нужен ли последний параграф в документе со справкой (Анализы для ОДКБ)
        /// </summary>
        public bool IsLastOdkbParagraphForCertificateNeeded;

        public CVisit()
            : this(0, 0)
        {
        }

        public CVisit(int visitId, int patientId)
        {
            Id = visitId;
            PatientId = patientId;
            VisitDate = DateTime.Now;
            IsLastParagraphForCertificateNeeded = false;
            IsLastOdkbParagraphForCertificateNeeded = false;
        }

        public CVisit(CVisit visit)
        {
            Id = visit.Id;
            PatientId = visit.PatientId;
            Diagnose = visit.Diagnose;
            Evenly = visit.Evenly;
            Recommendation = visit.Recommendation;
            Comments = visit.Comments;
            VisitDate = CConvertEngine.CopyDateTime(visit.VisitDate);
            IsLastParagraphForCertificateNeeded = visit.IsLastParagraphForCertificateNeeded;
            IsLastOdkbParagraphForCertificateNeeded = visit.IsLastOdkbParagraphForCertificateNeeded;
            Doctor = visit.Doctor;
            Header = visit.Header;
        }

        public static int Compare(CVisit visit1, CVisit visit2)
        {
            return CCompareEngine.CompareDateTime(visit1.VisitDate, visit2.VisitDate);
        }

        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string parameterName,
            string ownValue,
            string foreignValue,
            CVisit diffVisit,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'. Дата консультации: '{2}'.\r\nНазвание параметра: '{3}'. Значение: '{4}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnPatient = PatientId,
                IdOwnVisit = Id,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, CConvertEngine.DateTimeToString(VisitDate, true), parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignPatient = diffVisit.PatientId,
                IdForeignVisit = diffVisit.Id,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, CConvertEngine.DateTimeToString(diffVisit.VisitDate, true), parameterName, foreignValue)
            };
        }

        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной консультацией
        /// </summary>
        /// <param name="diffVisit">Импортируемая консультация</param>
        /// <param name="patientFio">ФИО импортируемого пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(CVisit diffVisit, string patientFio, string nosology, CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (Diagnose != diffVisit.Diagnose)
            {
                CreateMergeInfos(
                   ObjectType.VisitDiagnose,
                   patientFio,
                   nosology,
                   "Диагноз",
                   Diagnose,
                   diffVisit.Diagnose,
                   diffVisit,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Evenly != diffVisit.Evenly)
            {
                CreateMergeInfos(
                   ObjectType.VisitEvenly,
                   patientFio,
                   nosology,
                   "Объективно",
                   Evenly,
                   diffVisit.Evenly,
                   diffVisit,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Comments != diffVisit.Comments)
            {
                CreateMergeInfos(
                   ObjectType.VisitComments,
                   patientFio,
                   nosology,
                   "Комментарии",
                   Comments,
                   diffVisit.Comments,
                   diffVisit,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Recommendation != diffVisit.Recommendation)
            {
                CreateMergeInfos(
                   ObjectType.VisitRecommendation,
                   patientFio,
                   nosology,
                   "Рекомендации",
                   Recommendation,
                   diffVisit.Recommendation,
                   diffVisit,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Doctor != diffVisit.Doctor)
            {
                CreateMergeInfos(
                   ObjectType.VisitDoctor,
                   patientFio,
                   nosology,
                   "Врач",
                   Doctor,
                   diffVisit.Doctor,
                   diffVisit,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }           
        }
    }
}
