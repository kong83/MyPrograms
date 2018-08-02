using System;
using System.Collections.Generic;
using System.Diagnostics;

using SurgeryHelper.Forms;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Класс с данными по операции
    /// </summary>
    [DebuggerDisplay("id={Id} HospId={HospitalizationId} PatientId={PatientId} Name={Name}")]
    public class COperation : CIdEssence
    {
        /// <summary>
        /// Открытая для этой операции форма с данными, если она есть
        /// </summary>
        public OperationViewForm OpenedOperationViewForm;

        /// <summary>
        /// Открытая для этой операции форма с протоколом операции, если она есть
        /// </summary>
        public OperationProtocolForm OpenedOperationProtocolForm;

        /// <summary>
        /// Указатель на id госпитализации, к которой относится данная операция
        /// </summary>
        public int HospitalizationId;

        /// <summary>
        /// Указатель на id пациента, к которому относится данная операция
        /// </summary>
        public int PatientId;

        /// <summary>
        /// Дата операции
        /// </summary>
        public DateTime DateOfOperation;

        /// <summary>
        /// Время начала операции
        /// </summary>
        public DateTime StartTimeOfOperation;

        /// <summary>
        /// Время окончания операции
        /// </summary>
        public DateTime? EndTimeOfOperation;

        /// <summary>
        /// Название операции
        /// </summary>
        public string Name;

        /// <summary>
        /// Список хирургов
        /// </summary>
        public List<string> Surgeons;

        /// <summary>
        /// Список ассистентов
        /// </summary>
        public List<string> Assistents;

        /// <summary>
        /// Анестезист операции
        /// </summary>
        public string HeAnaesthetist;

        /// <summary>
        /// Анестезистка операции
        /// </summary>
        public string SheAnaesthetist;

        /// <summary>
        /// Операционная мед. сестра
        /// </summary>
        public string ScrubNurse;

        /// <summary>
        /// Список типов операций
        /// </summary>
        public List<string> OperationTypes;

        /// <summary>
        /// Санитар операции
        /// </summary>
        public string Orderly;

        public COperation()
            : this(0, 0, 0)
        {
        }

        public COperation(int operationId, int hospitalizationId, int patientId)
        {
            Id = operationId;
            HospitalizationId = hospitalizationId;
            PatientId = patientId;
            Surgeons = new List<string>();
            Assistents = new List<string>();
            OperationTypes = new List<string>();
            DateOfOperation = DateTime.Now;
            StartTimeOfOperation = DateTime.Now;
        }

        public COperation(COperation operationInfo)
        {
            Id = operationInfo.Id;
            PatientId = operationInfo.PatientId;
            HospitalizationId = operationInfo.HospitalizationId;
            DateOfOperation = CConvertEngine.CopyDateTime(operationInfo.DateOfOperation);
            StartTimeOfOperation = CConvertEngine.CopyDateTime(operationInfo.StartTimeOfOperation);
            EndTimeOfOperation = CConvertEngine.CopyDateTime(operationInfo.EndTimeOfOperation);
            Name = operationInfo.Name;
            HeAnaesthetist = operationInfo.HeAnaesthetist;
            SheAnaesthetist = operationInfo.SheAnaesthetist;

            Surgeons = new List<string>();
            foreach (string surgeon in operationInfo.Surgeons)
            {
                Surgeons.Add(surgeon);
            }

            Assistents = new List<string>();
            foreach (string assistent in operationInfo.Assistents)
            {
                Assistents.Add(assistent);
            }

            OperationTypes = new List<string>();
            foreach (string operationType in operationInfo.OperationTypes)
            {
                OperationTypes.Add(operationType);
            }

            ScrubNurse = operationInfo.ScrubNurse;
            Orderly = operationInfo.Orderly;
        }


        public static int Compare(COperation operationInfo1, COperation operationInfo2)
        {
            int dateResult = DateTime.Compare(operationInfo1.DateOfOperation, operationInfo2.DateOfOperation);

            if (dateResult == 0)
            {
                return DateTime.Compare(operationInfo1.StartTimeOfOperation, operationInfo2.StartTimeOfOperation);
            }

            return dateResult;
        }


        private void CreateMergeInfos(
            ObjectType objectType,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            string parameterName,
            string ownValue,
            string foreignValue,
            COperation diffOperation,
            out CMergeInfo ownPatientMergeInfo,
            out CMergeInfo foreignPatientMergeInfo)
        {
            const string differenceStr = "Пациент: '{0}'. Нозология: '{1}'.  Дата госпитализации: '{2}'. Название операции: '{3}'.\r\nНазвание параметра: '{4}'. Значение: '{5}'";

            ownPatientMergeInfo = new CMergeInfo
            {
                IdOwnHospitalization = HospitalizationId,
                IdOperation = Id,
                TypeOfObject = objectType,
                Value = ownValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, Name, parameterName, ownValue)
            };

            foreignPatientMergeInfo = new CMergeInfo
            {
                IdForeignHospitalization = diffOperation.HospitalizationId,
                IdOperation = diffOperation.Id,
                TypeOfObject = objectType,
                Value = foreignValue,
                Difference = string.Format(differenceStr, patientFio, nosology, hospitalizationDate, Name, parameterName, foreignValue)
            };
        }


        /// <summary>
        /// Получить строку с описанием разницы в полях между текущей и переданной операции
        /// </summary>
        /// <param name="diffOperation">Импортируемая операция</param>
        /// <param name="patientFio">ФИО импортируемого пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="hospitalizationDate">Дата импортируемой госпитализации</param>
        /// <param name="databasesMerger">Указатель на класс для мержа объектов</param>
        /// <returns></returns>
        public void GetDifference(
            COperation diffOperation,
            string patientFio,
            string nosology,
            string hospitalizationDate,
            CDatabasesMerger databasesMerger)
        {
            CMergeInfo ownPatientMergeInfo;
            CMergeInfo foreignPatientMergeInfo;

            if (HeAnaesthetist != diffOperation.HeAnaesthetist)
            {
                CreateMergeInfos(
                   ObjectType.OperationHeAnaesthetist,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Анестезист",
                   HeAnaesthetist,
                   diffOperation.HeAnaesthetist,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (SheAnaesthetist != diffOperation.SheAnaesthetist)
            {
                CreateMergeInfos(
                   ObjectType.OperationSheAnaesthetist,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Анестезистка",
                   SheAnaesthetist,
                   diffOperation.SheAnaesthetist,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (ScrubNurse != diffOperation.ScrubNurse)
            {
                CreateMergeInfos(
                   ObjectType.OperationScrubNurse,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Операционная мед. сестра",
                   ScrubNurse,
                   diffOperation.ScrubNurse,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (Orderly != diffOperation.Orderly)
            {
                CreateMergeInfos(
                   ObjectType.OperationOrderly,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Санитар",
                   Orderly,
                   diffOperation.Orderly,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareDate(DateOfOperation, diffOperation.DateOfOperation) != 0)
            {
                CreateMergeInfos(
                   ObjectType.OperationDateOfOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Дата операции",
                   CConvertEngine.DateTimeToString(DateOfOperation),
                   CConvertEngine.DateTimeToString(diffOperation.DateOfOperation),
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (CCompareEngine.CompareTime(StartTimeOfOperation, diffOperation.StartTimeOfOperation) != 0)
            {
                CreateMergeInfos(
                   ObjectType.OperationStartTimeOfOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Время начала операции",
                   CConvertEngine.TimeToString(StartTimeOfOperation),
                   CConvertEngine.TimeToString(diffOperation.StartTimeOfOperation),
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (EndTimeOfOperation.HasValue && !diffOperation.EndTimeOfOperation.HasValue)
            {
                CreateMergeInfos(
                   ObjectType.OperationEndTimeOfOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Время окончания операции",
                   CConvertEngine.TimeToString(EndTimeOfOperation.Value),
                   "Нет значения",
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (!EndTimeOfOperation.HasValue && diffOperation.EndTimeOfOperation.HasValue)
            {
                CreateMergeInfos(
                   ObjectType.OperationEndTimeOfOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Время окончания операции",
                   "Нет значения",
                   CConvertEngine.TimeToString(diffOperation.EndTimeOfOperation.Value),
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
            else if (EndTimeOfOperation.HasValue && diffOperation.EndTimeOfOperation.HasValue &&
                CCompareEngine.CompareDate(EndTimeOfOperation.Value, diffOperation.EndTimeOfOperation.Value) != 0)
            {
                CreateMergeInfos(
                   ObjectType.OperationEndTimeOfOperation,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Время окончания операции",
                   CConvertEngine.TimeToString(EndTimeOfOperation.Value),
                   CConvertEngine.TimeToString(diffOperation.EndTimeOfOperation.Value),
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            string ownValue;
            string foreignValue;
            if (!CCompareEngine.IsArraysEqual(Surgeons.ToArray(), diffOperation.Surgeons.ToArray(), out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.OperationSurgeons,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Список хирургов",
                   ownValue,
                   foreignValue,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Surgeons;
                foreignPatientMergeInfo.Object = diffOperation.Surgeons;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(Assistents.ToArray(), diffOperation.Assistents.ToArray(), out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.OperationAssistents,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Список ассистентов",
                   ownValue,
                   foreignValue,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = Assistents;
                foreignPatientMergeInfo.Object = diffOperation.Assistents;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }

            if (!CCompareEngine.IsArraysEqual(OperationTypes.ToArray(), diffOperation.OperationTypes.ToArray(), out ownValue, out foreignValue))
            {
                CreateMergeInfos(
                   ObjectType.OperationTypes,
                   patientFio,
                   nosology,
                   hospitalizationDate,
                   "Список типов операций",
                   ownValue,
                   foreignValue,
                   diffOperation,
                   out ownPatientMergeInfo,
                   out foreignPatientMergeInfo);

                ownPatientMergeInfo.Object = OperationTypes;
                foreignPatientMergeInfo.Object = diffOperation.OperationTypes;
                databasesMerger.AddMergeInfo(ownPatientMergeInfo, foreignPatientMergeInfo);
            }
        }
    }
}
