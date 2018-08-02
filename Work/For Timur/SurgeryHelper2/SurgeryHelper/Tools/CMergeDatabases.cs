using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Forms;
using SurgeryHelper.Workers;

namespace SurgeryHelper.Tools
{
    public class CMergeDatabases
    {
        private readonly CWorkersKeeper _ownWorkersKeeper;
        private readonly CWorkersKeeper _foreignWorkersKeeper;

        private readonly List<CMergeInfo> _ownMergeList;

        /// <summary>
        /// Список тех пациентов из нашей базы, которых нет во внешней и которые туда могут быть экспортированы
        /// </summary>
        public CMergeInfo[] OwnMergeList
        {
            get
            {
                return _ownMergeList.ToArray();
            }
        }

        private readonly List<CMergeInfo> _foreignMergeList;

        /// <summary>
        /// Список тех пациентов из внешней базы, которых нет в нашей и которые к нам могут быть импортированы
        /// </summary>
        public CMergeInfo[] ForeignMergeList
        {
            get
            {
                return _foreignMergeList.ToArray();
            }
        }

        private readonly List<CMergeInfo> _bothMergeList;

        /// <summary>
        /// Список тех пациентов, которые есть в обоих базах, но данные у них различаются
        /// </summary>
        public CMergeInfo[] BothMergeList
        {
            get
            {
                return _bothMergeList.ToArray();
            }
        }

        /// <summary>
        /// Список замен для нозологий из внешней базы
        /// </summary>
        private readonly Dictionary<string, string> _nosologyChangesForOwnDB;

        /// <summary>
        /// Добавить замену для нолозогии из внешней базы
        /// </summary>
        /// <param name="foreignNosologyName">Название нозологии во внешней базе</param>
        /// <param name="ownNosologyName">Название нозологии в нашей базе</param>
        public void AddNosologyChangesForOwnDB(string foreignNosologyName, string ownNosologyName)
        {
            _nosologyChangesForOwnDB.Add(foreignNosologyName, ownNosologyName);
        }

        /// <summary>
        /// Список замен для нозологий из нашей базы
        /// </summary>
        private readonly Dictionary<string, string> _nosologyChangesForForeignDB;

        /// <summary>
        /// Добавить замену для нолозогии из нашей базы
        /// </summary>
        /// <param name="ownNosologyName">Название нозологии в нашей базе</param>
        /// <param name="foreignNosologyName">Название нозологии во внешней базе</param>
        public void AddNosologyChangesForForeignDB(string ownNosologyName, string foreignNosologyName)
        {
            _nosologyChangesForForeignDB.Add(ownNosologyName, foreignNosologyName);
        }


        /// <summary>
        /// Удалить информацию об объектах из внешней базы для указанных индексов
        /// </summary>
        /// <param name="rowIndexForDelete">Индексы для удаления, отсортированные в порядке возрастания</param>
        public void RemoveIndexesFromForeignList(List<int> rowIndexForDelete)
        {
            RemoveIndexesFromList(_foreignMergeList, rowIndexForDelete);
        }

        /// <summary>
        /// Удалить информацию об объектах из нашей базы для указанных индексов
        /// </summary>
        /// <param name="rowIndexForDelete">Индексы для удаления, отсортированные в порядке возрастания</param>
        public void RemoveIndexesFromOwnList(List<int> rowIndexForDelete)
        {
            RemoveIndexesFromList(_ownMergeList, rowIndexForDelete);
        }

        /// <summary>
        /// Удалить информацию об объектах из списка объектов для указанных индексов
        /// </summary>
        /// <param name="mergeList">Список объектов для нашей или внешней базы</param>
        /// <param name="rowIndexForDelete">Индексы для удаления, отсортированные в порядке возрастания</param>
        private static void RemoveIndexesFromList(List<CMergeInfo> mergeList, List<int> rowIndexForDelete)
        {
            for (int i = rowIndexForDelete.Count - 1; i >= 0; i--)
            {
                mergeList.RemoveAt(rowIndexForDelete[i]);
            }
        }


        // Определяем воркеров для работы с нашей базой
        private readonly CPatientWorker _ownPatientWorker;
        private readonly CHospitalizationWorker _ownHospitalizationWorker;
        private readonly CVisitWorker _ownVisitWorker;
        private readonly COperationWorker _ownOperationWorker;

        private readonly COperationProtocolWorker _ownOperationProtocolWorker;
        private readonly CDischargeEpicrisisWorker _ownDischargeEpicrisisWorker;
        private readonly CLineOfCommunicationEpicrisisWorker _ownLineOfCommunicationEpicrisisWorker;
        private readonly CMedicalInspectionWorker _ownMedicalInspectionWorker;
        private readonly CTransferableEpicrisisWorker _ownTransferableEpicrisisWorker;

        private readonly CNosologyWorker _ownNosologyWorker;

        private readonly CBrachialPlexusCardWorker _ownBrachialPlexusCardWorker;
        private readonly CCardWorker _ownCardWorker;
        private readonly CObstetricParalysisCardWorker _ownObstetricParalysisCardWorker;
        private readonly CRangeOfMotionCardWorker _ownRangeOfMotionCardWorker;

        private readonly CAnamneseWorker _ownAnamneseWorker;
        private readonly CObstetricHistoryWorker _ownObstetricHistoryWorker;

        // Определяем воркеров для работы с импортируемой базой
        private readonly CPatientWorker _foreignPatientWorker;
        private readonly CHospitalizationWorker _foreignHospitalizationWorker;
        private readonly CVisitWorker _foreignVisitWorker;
        private readonly COperationWorker _foreignOperationWorker;

        private readonly COperationProtocolWorker _foreignOperationProtocolWorker;
        private readonly CDischargeEpicrisisWorker _foreignDischargeEpicrisisWorker;
        private readonly CLineOfCommunicationEpicrisisWorker _foreignLineOfCommunicationEpicrisisWorker;
        private readonly CMedicalInspectionWorker _foreignMedicalInspectionWorker;
        private readonly CTransferableEpicrisisWorker _foreignTransferableEpicrisisWorker;

        private readonly CNosologyWorker _foreignNosologyWorker;

        private readonly CBrachialPlexusCardWorker _foreignBrachialPlexusCardWorker;
        private readonly CCardWorker _foreignCardWorker;
        private readonly CObstetricParalysisCardWorker _foreignObstetricParalysisCardWorker;
        private readonly CRangeOfMotionCardWorker _foreignRangeOfMotionCardWorker;

        private readonly CAnamneseWorker _foreignAnamneseWorker;
        private readonly CObstetricHistoryWorker _foreignObstetricHistoryWorker;

        private readonly MergeForm _mergeForm;

        public CMergeDatabases(
            CWorkersKeeper ownWorkersKeeper, CWorkersKeeper foreignWorkersKeeper, MergeForm mergeForm)
        {
            _mergeForm = mergeForm;

            _nosologyChangesForOwnDB = new Dictionary<string, string>();
            _nosologyChangesForForeignDB = new Dictionary<string, string>();

            _ownMergeList = new List<CMergeInfo>();
            _foreignMergeList = new List<CMergeInfo>();
            _bothMergeList = new List<CMergeInfo>();

            _ownWorkersKeeper = ownWorkersKeeper;
            _foreignWorkersKeeper = foreignWorkersKeeper;

            // Определяем воркеров для работы с нашей базой
            _ownPatientWorker = _ownWorkersKeeper.PatientWorker;
            _ownHospitalizationWorker = _ownWorkersKeeper.HospitalizationWorker;
            _ownVisitWorker = _ownWorkersKeeper.VisitWorker;
            _ownOperationWorker = _ownWorkersKeeper.OperationWorker;

            _ownOperationProtocolWorker = _ownWorkersKeeper.OperationProtocolWorker;
            _ownDischargeEpicrisisWorker = _ownWorkersKeeper.DischargeEpicrisisWorker;
            _ownLineOfCommunicationEpicrisisWorker = _ownWorkersKeeper.LineOfCommunicationEpicrisisWorker;
            _ownMedicalInspectionWorker = _ownWorkersKeeper.MedicalInspectionWorker;
            _ownTransferableEpicrisisWorker = _ownWorkersKeeper.TransferableEpicrisisWorker;

            _ownNosologyWorker = _ownWorkersKeeper.NosologyWorker;

            _ownBrachialPlexusCardWorker = _ownWorkersKeeper.BrachialPlexusCardWorker;
            _ownCardWorker = _ownWorkersKeeper.CardWorker;
            _ownObstetricParalysisCardWorker = _ownWorkersKeeper.ObstetricParalysisCardWorker;
            _ownRangeOfMotionCardWorker = _ownWorkersKeeper.RangeOfMotionCardWorker;

            _ownAnamneseWorker = _ownWorkersKeeper.AnamneseWorker;
            _ownObstetricHistoryWorker = _ownWorkersKeeper.ObstetricHistoryWorker;

            // Определяем воркеров для работы с импортируемой базой
            _foreignPatientWorker = _foreignWorkersKeeper.PatientWorker;
            _foreignHospitalizationWorker = _foreignWorkersKeeper.HospitalizationWorker;
            _foreignVisitWorker = _foreignWorkersKeeper.VisitWorker;
            _foreignOperationWorker = _foreignWorkersKeeper.OperationWorker;

            _foreignOperationProtocolWorker = _foreignWorkersKeeper.OperationProtocolWorker;
            _foreignDischargeEpicrisisWorker = _foreignWorkersKeeper.DischargeEpicrisisWorker;
            _foreignLineOfCommunicationEpicrisisWorker = _foreignWorkersKeeper.LineOfCommunicationEpicrisisWorker;
            _foreignMedicalInspectionWorker = _foreignWorkersKeeper.MedicalInspectionWorker;
            _foreignTransferableEpicrisisWorker = _foreignWorkersKeeper.TransferableEpicrisisWorker;

            _foreignNosologyWorker = _foreignWorkersKeeper.NosologyWorker;

            _foreignBrachialPlexusCardWorker = _foreignWorkersKeeper.BrachialPlexusCardWorker;
            _foreignCardWorker = _foreignWorkersKeeper.CardWorker;
            _foreignObstetricParalysisCardWorker = _foreignWorkersKeeper.ObstetricParalysisCardWorker;
            _foreignRangeOfMotionCardWorker = _foreignWorkersKeeper.RangeOfMotionCardWorker;

            _foreignAnamneseWorker = _foreignWorkersKeeper.AnamneseWorker;
            _foreignObstetricHistoryWorker = _foreignWorkersKeeper.ObstetricHistoryWorker;

            CreateListsOfPatients();
        }


        #region Получить описание отличий для пациента из нашей и из внешней базы
        /// <summary>
        /// Добавить пару изменений в список с изменениями для нашей и внешней баз
        /// </summary>
        /// <param name="ownMergeInfo">Информация по изменениям для нашей базы</param>
        /// <param name="foreignMergeInfo">Информация по изменениям для внешней базы</param>
        public void AddMergeInfo(CMergeInfo ownMergeInfo, CMergeInfo foreignMergeInfo)
        {
            if (ownMergeInfo == null)
            {
                ownMergeInfo = new CMergeInfo();
            }

            if (foreignMergeInfo == null)
            {
                foreignMergeInfo = new CMergeInfo();
            }

            _ownMergeList.Add(ownMergeInfo);
            _foreignMergeList.Add(foreignMergeInfo);
        }

        /// <summary>
        /// Создание списка с пациентами, которые готовы к импроту/экспорту, и списка присутствующих
        /// в обеих базах пациетов с отличающимися данными 
        /// </summary>
        private void CreateListsOfPatients()
        {
            _mergeForm.SetStatus("Старт сравнения пациентов");

            foreach (CPatient foreignPatient in _foreignPatientWorker.PatientList)
            {
                CPatient ownPatient = _ownPatientWorker.GetByGeneralData(
                    foreignPatient.GetFullName(), foreignPatient.Nosology, -1);

                // Добавление в список котовых к импорту пациентов нового пациента
                if (ownPatient == null)
                {
                    var paientMergeInfo = new CMergeInfo
                    {
                        IdForeignPatient = foreignPatient.Id,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'.",
                            foreignPatient.GetFullName(),
                            foreignPatient.Nosology),
                        TypeOfObject = ObjectType.Patient
                    };
                    AddMergeInfo(null, paientMergeInfo);
                    continue;
                }

                // Проверка, совпадаются ли данные у нашего пациента и у импортируемого
                _mergeForm.SetStatus("Сравнение данных для пациента " + foreignPatient.GetFullName() + " с нозологией " + foreignPatient.Nosology);
                string differences = GetDifferenceBetweenTwoPatients(ownPatient, foreignPatient);
                if (differences.Length > 0)
                {
                    var paientMergeInfo = new CMergeInfo
                    {
                        FIO = foreignPatient.GetFullName(),
                        Nosology = foreignPatient.Nosology,
                        Difference = differences,
                        TypeOfObject = ObjectType.Patient
                    };
                    _bothMergeList.Add(paientMergeInfo);
                }
            }

            foreach (CPatient ownPatient in _ownPatientWorker.PatientList)
            {
                CPatient foreignPatient = _foreignPatientWorker.GetByGeneralData(
                    ownPatient.GetFullName(), ownPatient.Nosology, -1);

                // Добавление в список котовых к экспорту пациентов нового пациента
                if (foreignPatient == null)
                {
                    var paientMergeInfo = new CMergeInfo
                    {
                        IdOwnPatient = ownPatient.Id,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'.",
                            ownPatient.GetFullName(),
                            ownPatient.Nosology),
                        TypeOfObject = ObjectType.Patient
                    };

                    AddMergeInfo(paientMergeInfo, null);
                }
            }

            if (OwnMergeList.Length == 0 && ForeignMergeList.Length == 0 && BothMergeList.Length == 0)
            {
                _mergeForm.SetStatus("Сравнение пациентов завершено, отличий не найдено");
            }
            else
            {
                _mergeForm.SetStatus("Сравнение пациентов завершено");
            }
        }


        /// <summary>
        /// Получить строку с информацией о разнице между пациентом 
        /// из нашей базы и из импортируемой
        /// </summary>
        /// <param name="fio">фио пациента в обеих базах</param>
        /// <param name="nosology">нозология пациента в обеих базах</param>
        /// <returns></returns>
        public string GetDifferenceBetweenTwoPatients(string fio, string nosology)
        {
            CPatient ownPatient = _ownPatientWorker.GetByGeneralData(fio, nosology, -1);
            CPatient foreignPatient = _foreignPatientWorker.GetByGeneralData(fio, nosology, -1);

            if (ownPatient == null)
            {
                throw new Exception("Пациент " + fio + " с нозологией " + nosology + " не найден в нашей базе");
            }

            if (foreignPatient == null)
            {
                throw new Exception("Пациент " + fio + " с нозологией " + nosology + " не найден во внешней базе");
            }

            return GetDifferenceBetweenTwoPatients(ownPatient, foreignPatient);
        }


        /// <summary>
        /// Получить строку с информацией о разнице между пациентом 
        /// из нашей базы и из импортируемой
        /// </summary>
        /// <param name="ownPatient">Информация о пациенте из нашей базы</param>
        /// <param name="foreignPatient">Информация о пациенте из внешей базы</param>
        /// <returns></returns>
        private string GetDifferenceBetweenTwoPatients(CPatient ownPatient, CPatient foreignPatient)
        {
            var diffs = new StringBuilder();
            CMergeInfo mergeInfo;

            ownPatient.GetDifference(foreignPatient, this);

            string patientFIO = ownPatient.GetFullName();
            string patientNosology = ownPatient.Nosology;

            // Сравниваем данные в личных папках
            if (!string.IsNullOrEmpty(ownPatient.PrivateFolder) && !string.IsNullOrEmpty(foreignPatient.PrivateFolder))
            {
                string fullPrivaleFolderPathOwn = Path.Combine(_ownWorkersKeeper.ExecutableDirectoryPath, ownPatient.PrivateFolder);
                string fullPrivaleFolderPathForeign = Path.Combine(_foreignWorkersKeeper.ExecutableDirectoryPath, foreignPatient.PrivateFolder);
                diffs.Append(GetDifferenceBetweenContentOfTwoPrivateFolders(fullPrivaleFolderPathOwn, fullPrivaleFolderPathForeign, patientFIO, patientNosology));
            }

            // Сравниваем анамнезы 
            bool isOwnObjectExists = _ownAnamneseWorker.IsExists(ownPatient.Id);
            bool isForeignObjectExists = _foreignAnamneseWorker.IsExists(foreignPatient.Id);
            if (isOwnObjectExists && !isForeignObjectExists)
            {
                mergeInfo = new CMergeInfo
                {
                    IdOwnPatient = ownPatient.Id,
                    IdForeignPatient = foreignPatient.Id,
                    TypeOfObject = ObjectType.Anamnese,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Анамнез'",
                        foreignPatient.GetFullName(),
                        foreignPatient.Nosology)
                };

                AddMergeInfo(mergeInfo, null);
            }
            else if (!isOwnObjectExists && isForeignObjectExists)
            {
                mergeInfo = new CMergeInfo
                {
                    IdOwnPatient = ownPatient.Id,
                    IdForeignPatient = foreignPatient.Id,
                    TypeOfObject = ObjectType.Anamnese,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Анамнез'",
                        foreignPatient.GetFullName(),
                        foreignPatient.Nosology)
                };

                AddMergeInfo(null, mergeInfo);
            }
            else if (isOwnObjectExists)
            {
                CAnamnese ownAnamnese = _ownAnamneseWorker.GetByPatientId(ownPatient.Id);
                CAnamnese foreignAnamnese = _foreignAnamneseWorker.GetByPatientId(foreignPatient.Id);
                ownAnamnese.GetDifference(foreignAnamnese, patientFIO, patientNosology, this);
            }

            // Сравниваем акушерские анамнезы
            isOwnObjectExists = _ownObstetricHistoryWorker.IsExists(ownPatient.Id);
            isForeignObjectExists = _foreignObstetricHistoryWorker.IsExists(foreignPatient.Id);
            if (isOwnObjectExists && !isForeignObjectExists)
            {
                mergeInfo = new CMergeInfo
                {
                    IdOwnPatient = ownPatient.Id,
                    IdForeignPatient = foreignPatient.Id,
                    TypeOfObject = ObjectType.ObstetricHistory,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Акушерский анамнез'",
                        foreignPatient.GetFullName(),
                        foreignPatient.Nosology)
                };

                AddMergeInfo(mergeInfo, null);
            }
            else if (!isOwnObjectExists && isForeignObjectExists)
            {
                mergeInfo = new CMergeInfo
                {
                    IdOwnPatient = ownPatient.Id,
                    IdForeignPatient = foreignPatient.Id,
                    TypeOfObject = ObjectType.ObstetricHistory,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Акушерский анамнез'",
                        foreignPatient.GetFullName(),
                        foreignPatient.Nosology)
                };

                AddMergeInfo(null, mergeInfo);
            }
            else if (isOwnObjectExists)
            {
                CObstetricHistory ownObstetricHistory = _ownObstetricHistoryWorker.GetByPatientId(ownPatient.Id);
                CObstetricHistory foreignObstetricHistory = _foreignObstetricHistoryWorker.GetByPatientId(foreignPatient.Id);
                ownObstetricHistory.GetDifference(foreignObstetricHistory, patientFIO, patientNosology, this);
            }

            // Сравниваем списки госпитализаций
            CHospitalization[] foreignHospitalizations = _foreignHospitalizationWorker.GetListByPatientId(foreignPatient.Id);
            CHospitalization[] ownHospitalizations = _ownHospitalizationWorker.GetListByPatientId(ownPatient.Id);

            // Сравниваем все госпитализации из импортируемого списка
            foreach (CHospitalization foreignHospitalization in foreignHospitalizations)
            {
                CHospitalization ownHospitalization = _ownHospitalizationWorker.GetByGeneralData(
                    foreignHospitalization.DeliveryDate,
                    patientFIO,
                    foreignPatient.Nosology,
                    -1);

                if (ownHospitalization == null)
                {
                    mergeInfo = new CMergeInfo
                    {
                        IdOwnPatient = ownPatient.Id,
                        IdForeignPatient = foreignPatient.Id,
                        IdForeignHospitalization = foreignHospitalization.Id,
                        TypeOfObject = ObjectType.Hospitalization,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Госпитализация за {2}'",
                            foreignPatient.GetFullName(),
                            foreignPatient.Nosology,
                            CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true))
                    };

                    AddMergeInfo(null, mergeInfo);
                }
                else
                {
                    // Просматриваем все данные по госпитализации и ищем отличия
                    ownHospitalization.GetDifference(foreignHospitalization, patientFIO, patientNosology, this);

                    string hospitalizationDeliveryDate = CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate);

                    // Сравниваем выписные эпикризы, относящиеся к госпитализации
                    isOwnObjectExists = _ownDischargeEpicrisisWorker.IsExists(ownHospitalization.Id);
                    isForeignObjectExists = _foreignDischargeEpicrisisWorker.IsExists(foreignHospitalization.Id);
                    if (isOwnObjectExists && !isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.DischargeEpicrisis,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Выписной эпикриз'",
                                ownPatient.GetFullName(),
                                ownPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(mergeInfo, null);
                    }
                    else if (!isOwnObjectExists && isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.DischargeEpicrisis,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Выписной эпикриз'",
                                foreignPatient.GetFullName(),
                                foreignPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(null, mergeInfo);
                    }
                    else if (isOwnObjectExists)
                    {
                        CDischargeEpicrisis ownDischargeEpicrisis = _ownDischargeEpicrisisWorker.GetByHospitalizationId(ownHospitalization.Id);
                        CDischargeEpicrisis foreignDischargeEpicrisis = _foreignDischargeEpicrisisWorker.GetByHospitalizationId(foreignHospitalization.Id);
                        ownDischargeEpicrisis.GetDifference(foreignDischargeEpicrisis, patientFIO, patientNosology, hospitalizationDeliveryDate, this);
                    }

                    // Сравниваем этапные эпикризы, относящиеся к госпитализации
                    isOwnObjectExists = _ownLineOfCommunicationEpicrisisWorker.IsExists(ownHospitalization.Id);
                    isForeignObjectExists = _foreignLineOfCommunicationEpicrisisWorker.IsExists(foreignHospitalization.Id);
                    if (isOwnObjectExists && !isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.LineOfCommunicationEpicrisis,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Этапный эпикриз'",
                                ownPatient.GetFullName(),
                                ownPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(mergeInfo, null);
                    }
                    else if (!isOwnObjectExists && isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.LineOfCommunicationEpicrisis,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Этапный эпикриз'",
                                foreignPatient.GetFullName(),
                                foreignPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(null, mergeInfo);
                    }
                    else if (isOwnObjectExists)
                    {
                        CLineOfCommunicationEpicrisis ownLineOfCommunicationEpicrisis = _ownLineOfCommunicationEpicrisisWorker.GetByHospitalizationId(ownHospitalization.Id);
                        CLineOfCommunicationEpicrisis foreignLineOfCommunicationEpicrisis = _foreignLineOfCommunicationEpicrisisWorker.GetByHospitalizationId(foreignHospitalization.Id);
                        ownLineOfCommunicationEpicrisis.GetDifference(foreignLineOfCommunicationEpicrisis, patientFIO, patientNosology, hospitalizationDeliveryDate, this);
                    }

                    // Сравниваем переводные эпикризы, относящиеся к госпитализации
                    isOwnObjectExists = _ownTransferableEpicrisisWorker.IsExists(ownHospitalization.Id);
                    isForeignObjectExists = _foreignTransferableEpicrisisWorker.IsExists(foreignHospitalization.Id);
                    if (isOwnObjectExists && !isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.TransferableEpicrisis,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Переводной эпикриз'",
                                ownPatient.GetFullName(),
                                ownPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(mergeInfo, null);
                    }
                    else if (!isOwnObjectExists && isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.TransferableEpicrisis,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Переводной эпикриз'",
                                foreignPatient.GetFullName(),
                                foreignPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(null, mergeInfo);
                    }
                    else if (isOwnObjectExists)
                    {
                        CTransferableEpicrisis ownTransferableEpicrisis = _ownTransferableEpicrisisWorker.GetByHospitalizationId(ownHospitalization.Id);
                        CTransferableEpicrisis foreignTransferableEpicrisis = _foreignTransferableEpicrisisWorker.GetByHospitalizationId(foreignHospitalization.Id);
                        ownTransferableEpicrisis.GetDifference(foreignTransferableEpicrisis, patientFIO, patientNosology, hospitalizationDeliveryDate, this);
                    }

                    // Сравниваем осмотры в отделении, относящиеся к госпитализации
                    isOwnObjectExists = _ownMedicalInspectionWorker.IsExists(ownHospitalization.Id);
                    isForeignObjectExists = _foreignMedicalInspectionWorker.IsExists(foreignHospitalization.Id);
                    if (isOwnObjectExists && !isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.MedicalInspection,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Осмотр в отделении'",
                                ownPatient.GetFullName(),
                                ownPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(mergeInfo, null);
                    }
                    else if (!isOwnObjectExists && isForeignObjectExists)
                    {
                        mergeInfo = new CMergeInfo
                        {
                            IdOwnHospitalization = ownHospitalization.Id,
                            IdForeignHospitalization = foreignHospitalization.Id,
                            TypeOfObject = ObjectType.MedicalInspection,
                            Difference = string.Format(
                                "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Осмотр в отделении'",
                                foreignPatient.GetFullName(),
                                foreignPatient.Nosology,
                                hospitalizationDeliveryDate)
                        };

                        AddMergeInfo(null, mergeInfo);
                    }
                    else if (isOwnObjectExists)
                    {
                        CMedicalInspection ownMedicalInspection = _ownMedicalInspectionWorker.GetByHospitalizationId(ownHospitalization.Id);
                        CMedicalInspection foreignMedicalInspection = _foreignMedicalInspectionWorker.GetByHospitalizationId(foreignHospitalization.Id);
                        ownMedicalInspection.GetDifference(foreignMedicalInspection, patientFIO, patientNosology, hospitalizationDeliveryDate, this);
                    }

                    // Сравниваем карты обследования
                    diffs.Append(CompareCards(
                        patientFIO,
                        patientNosology,
                        hospitalizationDeliveryDate,
                        null,
                        ownHospitalization.Id,
                        -1,
                        foreignHospitalization.Id,
                        -1));

                    // Просматриваем операции, относящиеся к госпитализации
                    // Сравниваем списки операций
                    COperation[] foreignOperations = _foreignOperationWorker.GetListByHospitalizationId(foreignHospitalization.Id);
                    COperation[] ownOperations = _ownOperationWorker.GetListByHospitalizationId(ownHospitalization.Id);

                    // Сравниваем все операции из импортируемого списка
                    foreach (COperation foreignOperation in foreignOperations)
                    {
                        COperation ownOperation = _ownOperationWorker.GetByGeneralData(
                            foreignOperation.Name,
                            foreignHospitalization.DeliveryDate,
                            patientFIO,
                            foreignPatient.Nosology,
                            -1);
                        if (ownOperation == null)
                        {
                            mergeInfo = new CMergeInfo
                            {
                                IdOwnHospitalization = ownHospitalization.Id,
                                IdForeignHospitalization = foreignHospitalization.Id,
                                IdOperation = foreignOperation.Id,
                                TypeOfObject = ObjectType.Operation,
                                Difference = string.Format(
                                    "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Операция {3}'",
                                    foreignPatient.GetFullName(),
                                    foreignPatient.Nosology,
                                    hospitalizationDeliveryDate,
                                    foreignOperation.Name)
                            };

                            AddMergeInfo(null, mergeInfo);
                        }
                        else
                        {
                            // Просматриваем все данные по операции и ищем отличия
                            ownOperation.GetDifference(foreignOperation, patientFIO, patientNosology, hospitalizationDeliveryDate, this);

                            // Сравниваем операционные протоколы, относящиеся к операции
                            COperationProtocol ownOperationProtocol = _ownOperationProtocolWorker.GetByOperationId(ownOperation.Id);
                            COperationProtocol foreignOperationProtocol = _foreignOperationProtocolWorker.GetByOperationId(foreignOperation.Id);
                            ownOperationProtocol.GetDifference(foreignOperationProtocol, patientFIO, patientNosology, hospitalizationDeliveryDate, ownOperation.Name, this);
                        }
                    }

                    // Ищем операции, которые есть в нашем списке и нету в импортируемом
                    foreach (COperation ownOperation in ownOperations)
                    {
                        COperation foreignOperation = _foreignOperationWorker.GetByGeneralData(
                            ownOperation.Name,
                            ownHospitalization.DeliveryDate,
                            patientFIO,
                            ownPatient.Nosology,
                            -1);
                        if (foreignOperation == null)
                        {
                            mergeInfo = new CMergeInfo
                            {
                                IdOwnHospitalization = ownHospitalization.Id,
                                IdForeignHospitalization = foreignHospitalization.Id,
                                IdOperation = ownOperation.Id,
                                TypeOfObject = ObjectType.Operation,
                                Difference = string.Format(
                                    "Пациент: '{0}'. Нозология:'{1}'. Госпитализация за: '{2}'. Название объекта: 'Операция {3}'",
                                    ownPatient.GetFullName(),
                                    ownPatient.Nosology,
                                    hospitalizationDeliveryDate,
                                    ownOperation.Name)
                            };

                            AddMergeInfo(mergeInfo, null);
                        }
                    }
                }
            }

            // Ищем госпитализации, которые есть в нашем списке и нету в импортируемом
            foreach (CHospitalization ownHospitalization in ownHospitalizations)
            {
                CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetByGeneralData(
                    ownHospitalization.DeliveryDate,
                    patientFIO,
                    ownPatient.Nosology,
                    -1);
                if (foreignHospitalization == null)
                {
                    mergeInfo = new CMergeInfo
                    {
                        IdOwnPatient = ownPatient.Id,
                        IdForeignPatient = foreignPatient.Id,
                        IdOwnHospitalization = ownHospitalization.Id,
                        TypeOfObject = ObjectType.Hospitalization,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Госпитализация за {2}'",
                            ownPatient.GetFullName(),
                            ownPatient.Nosology,
                            CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true))
                    };

                    AddMergeInfo(mergeInfo, null);
                }
            }

            // Сравниваем списки консультаций
            CVisit[] foreignVisits = _foreignVisitWorker.GetListByPatientId(foreignPatient.Id);
            CVisit[] ownVisits = _ownVisitWorker.GetListByPatientId(ownPatient.Id);

            // Сравниваем все консультации из импортируемого списка
            foreach (CVisit foreignVisit in foreignVisits)
            {
                CVisit ownVisit = _ownVisitWorker.GetByGeneralData(
                    foreignVisit.VisitDate,
                    patientFIO,
                    foreignPatient.Nosology,
                    -1);
                if (ownVisit == null)
                {
                    mergeInfo = new CMergeInfo
                    {
                        IdOwnPatient = ownPatient.Id,
                        IdForeignPatient = foreignPatient.Id,
                        IdForeignVisit = foreignVisit.Id,
                        TypeOfObject = ObjectType.Visit,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Консультация за {2}'",
                            foreignPatient.GetFullName(),
                            foreignPatient.Nosology,
                            CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true))
                    };

                    AddMergeInfo(null, mergeInfo);
                }
                else
                {
                    // Просматриваем все данные по консультациям и ищем отличия
                    ownVisit.GetDifference(foreignVisit, patientFIO, patientNosology, this);

                    // Сравниваем карты обследования
                    diffs.Append(CompareCards(
                        patientFIO,
                        patientNosology,
                        null,
                        CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true),
                        -1,
                        ownVisit.Id,
                        -1,
                        foreignVisit.Id));
                }
            }

            // Ищем консультации, которые есть в нашем списке и нету в импортируемом
            foreach (CVisit ownVisit in ownVisits)
            {
                CVisit foreignVisit = _foreignVisitWorker.GetByGeneralData(
                    ownVisit.VisitDate,
                    patientFIO,
                    ownPatient.Nosology,
                    -1);
                if (foreignVisit == null)
                {
                    mergeInfo = new CMergeInfo
                    {
                        IdOwnPatient = ownPatient.Id,
                        IdForeignPatient = foreignPatient.Id,
                        IdOwnVisit = ownVisit.Id,
                        TypeOfObject = ObjectType.Visit,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'. Название объекта: 'Консультация за {2}'",
                            ownPatient.GetFullName(),
                            ownPatient.Nosology,
                            CConvertEngine.DateTimeToString(ownVisit.VisitDate, true))
                    };

                    AddMergeInfo(mergeInfo, null);
                }
            }

            return diffs.ToString();
        }


        /// <summary>
        /// Сравнение карт обследование
        /// </summary>
        /// <param name="patientFIO">ФИО пациента</param>
        /// <param name="patientNosology">Нозология пациента</param>
        /// <param name="hospitalizationDeliveryDate">Дата госпитализации</param>
        /// <param name="visitDeliveryDate">Дата консультации</param>
        /// <param name="ownHospitalizationId">id госпитализации в нашей базе</param>
        /// <param name="ownVisitId">id консультации в нашей базе</param>
        /// <param name="foreignHospitalizationId">id госпитализации во внешней базе</param>
        /// <param name="foreignVisitId">id консультации во внешней базе</param>
        /// <returns></returns>
        private string CompareCards(
            string patientFIO,
            string patientNosology,
            string hospitalizationDeliveryDate,
            string visitDeliveryDate,
            int ownHospitalizationId,
            int ownVisitId,
            int foreignHospitalizationId,
            int foreignVisitId)
        {
            var diffs = new StringBuilder();
            CMergeInfo paientMergeInfo;

            string dateHospitalizationOrVisitInfoStr = string.IsNullOrEmpty(hospitalizationDeliveryDate)
                            ? "Консультация за: '" + visitDeliveryDate + "'"
                            : "Госпитализация за: '" + hospitalizationDeliveryDate + "'";

            // Карта плечевого сплетения
            bool isOwnObjectExists = _ownBrachialPlexusCardWorker.IsExists(ownHospitalizationId, ownVisitId);
            bool isForeignObjectExists = _foreignBrachialPlexusCardWorker.IsExists(foreignHospitalizationId, foreignVisitId);
            if (isOwnObjectExists && !isForeignObjectExists)
            {
                paientMergeInfo = new CMergeInfo
                {
                    IdOwnHospitalization = ownHospitalizationId,
                    IdForeignHospitalization = foreignHospitalizationId,
                    IdOwnVisit = ownVisitId,
                    IdForeignVisit = foreignVisitId,
                    TypeOfObject = ObjectType.BrachialPlexusCard,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта плечевого сплетения'",
                        patientFIO,
                        patientNosology,
                        dateHospitalizationOrVisitInfoStr)
                };

                AddMergeInfo(paientMergeInfo, null);
            }
            else if (!isOwnObjectExists && isForeignObjectExists)
            {
                paientMergeInfo = new CMergeInfo
                {
                    IdOwnHospitalization = ownHospitalizationId,
                    IdForeignHospitalization = foreignHospitalizationId,
                    IdOwnVisit = ownVisitId,
                    IdForeignVisit = foreignVisitId,
                    TypeOfObject = ObjectType.BrachialPlexusCard,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта плечевого сплетения'",
                        patientFIO,
                        patientNosology,
                        dateHospitalizationOrVisitInfoStr)
                };

                AddMergeInfo(null, paientMergeInfo);
            }
            else if (isOwnObjectExists)
            {
                CBrachialPlexusCard ownBrachialPlexusCard = _ownBrachialPlexusCardWorker.GetByHospitalizationAndVisitId(ownHospitalizationId, ownVisitId);
                CBrachialPlexusCard foreignBrachialPlexusCard = _foreignBrachialPlexusCardWorker.GetByHospitalizationAndVisitId(foreignHospitalizationId, foreignVisitId);
                ownBrachialPlexusCard.GetDifference(foreignBrachialPlexusCard, patientFIO, patientNosology, hospitalizationDeliveryDate, visitDeliveryDate, this);
            }

            // Карта объёма движений
            isOwnObjectExists = _ownRangeOfMotionCardWorker.IsExists(ownHospitalizationId, ownVisitId);
            isForeignObjectExists = _foreignRangeOfMotionCardWorker.IsExists(foreignHospitalizationId, foreignVisitId);
            if (isOwnObjectExists && !isForeignObjectExists)
            {
                paientMergeInfo = new CMergeInfo
                {
                    IdOwnHospitalization = ownHospitalizationId,
                    IdForeignHospitalization = foreignHospitalizationId,
                    IdOwnVisit = ownVisitId,
                    IdForeignVisit = foreignVisitId,
                    TypeOfObject = ObjectType.RangeOfMotionCard,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта объёма движений'",
                        patientFIO,
                        patientNosology,
                        dateHospitalizationOrVisitInfoStr)
                };

                AddMergeInfo(paientMergeInfo, null);
            }
            else if (!isOwnObjectExists && isForeignObjectExists)
            {
                paientMergeInfo = new CMergeInfo
                {
                    IdOwnHospitalization = ownHospitalizationId,
                    IdForeignHospitalization = foreignHospitalizationId,
                    IdOwnVisit = ownVisitId,
                    IdForeignVisit = foreignVisitId,
                    TypeOfObject = ObjectType.RangeOfMotionCard,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта объёма движений'",
                        patientFIO,
                        patientNosology,
                        dateHospitalizationOrVisitInfoStr)
                };

                AddMergeInfo(null, paientMergeInfo);
            }
            else if (isOwnObjectExists)
            {
                CRangeOfMotionCard ownRangeOfMotionCard = _ownRangeOfMotionCardWorker.GetByHospitalizationAndVisitId(ownHospitalizationId, ownVisitId);
                CRangeOfMotionCard foreignRangeOfMotionCard = _foreignRangeOfMotionCardWorker.GetByHospitalizationAndVisitId(foreignHospitalizationId, foreignVisitId);
                ownRangeOfMotionCard.GetDifference(foreignRangeOfMotionCard, patientFIO, patientNosology, hospitalizationDeliveryDate, visitDeliveryDate, this);
            }

            // Карта на акушерский паралич
            isOwnObjectExists = _ownObstetricParalysisCardWorker.IsExists(ownHospitalizationId, ownVisitId);
            isForeignObjectExists = _foreignObstetricParalysisCardWorker.IsExists(foreignHospitalizationId, foreignVisitId);
            if (isOwnObjectExists && !isForeignObjectExists)
            {
                paientMergeInfo = new CMergeInfo
                {
                    IdOwnHospitalization = ownHospitalizationId,
                    IdForeignHospitalization = foreignHospitalizationId,
                    IdOwnVisit = ownVisitId,
                    IdForeignVisit = foreignVisitId,
                    TypeOfObject = ObjectType.ObstetricParalysisCard,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта на акушерский паралич'",
                        patientFIO,
                        patientNosology,
                        dateHospitalizationOrVisitInfoStr)
                };

                AddMergeInfo(paientMergeInfo, null);
            }
            else if (!isOwnObjectExists && isForeignObjectExists)
            {
                paientMergeInfo = new CMergeInfo
                {
                    IdOwnHospitalization = ownHospitalizationId,
                    IdForeignHospitalization = foreignHospitalizationId,
                    IdOwnVisit = ownVisitId,
                    IdForeignVisit = foreignVisitId,
                    TypeOfObject = ObjectType.ObstetricParalysisCard,
                    Difference = string.Format(
                        "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта на акушерский паралич'",
                        patientFIO,
                        patientNosology,
                        dateHospitalizationOrVisitInfoStr)
                };

                AddMergeInfo(null, paientMergeInfo);
            }
            else if (isOwnObjectExists)
            {
                CObstetricParalysisCard ownObstetricParalysisCard = _ownObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(ownHospitalizationId, ownVisitId);
                CObstetricParalysisCard foreignObstetricParalysisCard = _foreignObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(foreignHospitalizationId, foreignVisitId);
                ownObstetricParalysisCard.GetDifference(foreignObstetricParalysisCard, patientFIO, patientNosology, hospitalizationDeliveryDate, visitDeliveryDate, this);
            }

            var cardTypes = (CardType[])Enum.GetValues(typeof(CardType));
            foreach (CardType cardType in cardTypes)
            {
                isOwnObjectExists = _ownCardWorker.IsExists(ownHospitalizationId, ownVisitId, cardType);
                isForeignObjectExists = _foreignCardWorker.IsExists(foreignHospitalizationId, foreignVisitId, cardType);
                if (isOwnObjectExists && !isForeignObjectExists)
                {
                    paientMergeInfo = new CMergeInfo
                    {
                        IdOwnHospitalization = ownHospitalizationId,
                        IdForeignHospitalization = foreignHospitalizationId,
                        IdOwnVisit = ownVisitId,
                        IdForeignVisit = foreignVisitId,
                        TypeOfObject = ObjectType.LeftRightCard,
                        TypeOfCard = cardType,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта осмотра {3}'",
                            patientFIO,
                            patientNosology,
                            dateHospitalizationOrVisitInfoStr,
                            cardType)
                    };

                    AddMergeInfo(paientMergeInfo, null);
                }
                else if (!isOwnObjectExists && isForeignObjectExists)
                {
                    paientMergeInfo = new CMergeInfo
                    {
                        IdOwnHospitalization = ownHospitalizationId,
                        IdForeignHospitalization = foreignHospitalizationId,
                        IdOwnVisit = ownVisitId,
                        IdForeignVisit = foreignVisitId,
                        TypeOfObject = ObjectType.LeftRightCard,
                        TypeOfCard = cardType,
                        Difference = string.Format(
                            "Пациент: '{0}'. Нозология:'{1}'. {2}. Название объекта: 'Карта осмотра {3}'",
                            patientFIO,
                            patientNosology,
                            dateHospitalizationOrVisitInfoStr,
                            cardType)
                    };

                    AddMergeInfo(null, paientMergeInfo);
                }
                else if (isOwnObjectExists)
                {
                    CCard ownCard = _ownCardWorker.GetByGeneralData(ownHospitalizationId, ownVisitId, CardSide.Left, cardType);
                    CCard foreignCard = _foreignCardWorker.GetByGeneralData(foreignHospitalizationId, foreignVisitId, CardSide.Left, cardType);
                    ownCard.GetDifference(foreignCard, patientFIO, patientNosology, hospitalizationDeliveryDate, visitDeliveryDate, this);

                    ownCard = _ownCardWorker.GetByGeneralData(ownHospitalizationId, ownVisitId, CardSide.Right, cardType);
                    foreignCard = _foreignCardWorker.GetByGeneralData(foreignHospitalizationId, foreignVisitId, CardSide.Right, cardType);
                    ownCard.GetDifference(foreignCard, patientFIO, patientNosology, hospitalizationDeliveryDate, visitDeliveryDate, this);
                }
            }

            return diffs.ToString();
        }


        /// <summary>
        /// Получить разницу между содержимым личных папок
        /// </summary>
        /// <param name="folderPathOwn">Путь до личной папки пациента из нашей базы</param>
        /// <param name="folderPathForeign">Путь до личной папки пациента из внешней базы</param>
        /// <param name="patientFIO"></param>
        /// <param name="patientNosology"></param>
        /// <returns></returns>
        private static string GetDifferenceBetweenContentOfTwoPrivateFolders(string folderPathOwn, string folderPathForeign, string patientFIO, string patientNosology)
        {
            var diffs = new StringBuilder();

            if (!Directory.Exists(folderPathOwn) || !Directory.Exists(folderPathForeign))
            {
                if (!Directory.Exists(folderPathOwn))
                {
                    diffs.AppendFormat(
                        "Пациент: '" + patientFIO + "'. Нозология: '" + patientNosology +
                        "'.\r\n  Название параметра: 'Содержимое личной папки'. Личная папка '{0}' у пациента из нашей базы не найдена.\r\n\r\n",
                        folderPathOwn);
                }

                if (!Directory.Exists(folderPathForeign))
                {
                    diffs.AppendFormat(
                        "Пациент: '" + patientFIO + "'. Нозология: '" + patientNosology +
                        "'.\r\n  Название параметра: 'Содержимое личной папки'. Личная папка '{0}' у пациента из внешней базы не найдена.\r\n\r\n",
                        folderPathForeign);
                }

                return diffs.ToString();
            }

            // Получаем списки файлов из личных папок
            var ownFilePathsList = new List<string>();
            CAddObjectsByMerge.GetFilesPathsFromAllFolders(folderPathOwn, ownFilePathsList);

            var foreignFilePathsList = new List<string>();
            CAddObjectsByMerge.GetFilesPathsFromAllFolders(folderPathForeign, foreignFilePathsList);

            // Сравниваем файлы нашего пациента с файлами пациента из внешней базы 
            foreach (string ownFullPath in ownFilePathsList)
            {
                string ownRelativePath = CAddObjectsByMerge.GetRelativePath(folderPathOwn, ownFullPath);

                string foreignFullPathSave = string.Empty;
                foreach (string foreignFullPath in foreignFilePathsList)
                {
                    if (ownRelativePath.ToLower() == CAddObjectsByMerge.GetRelativePath(folderPathForeign, foreignFullPath).ToLower())
                    {
                        foreignFullPathSave = foreignFullPath;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(foreignFullPathSave))
                {
                    diffs.AppendFormat(
                        "Пациент: '" + patientFIO + "'. Нозология: '" + patientNosology +
                        "'.\r\n  Название параметра: 'Содержимое личной папки'. У пациента из внешней базы в личной папке нет файла '{0}'.\r\n\r\n",
                        ownFullPath);
                }
                else
                {
                    if (new FileInfo(ownFullPath).Length != new FileInfo(foreignFullPathSave).Length)
                    {
                        diffs.AppendFormat(
                        "Пациент: '" + patientFIO + "'. Нозология: '" + patientNosology +
                        "'.\r\n  Название параметра: 'Содержимое личной папки'. Файлы '{0}' и '{1}' различны.\r\n\r\n",
                        ownFullPath,
                        foreignFullPathSave);
                    }
                }
            }

            foreach (string foreignFullPath in foreignFilePathsList)
            {
                string foreignRelativePath = CAddObjectsByMerge.GetRelativePath(folderPathForeign, foreignFullPath);

                string ownFullPathSave = string.Empty;
                foreach (string ownFullPath in ownFilePathsList)
                {
                    if (foreignRelativePath.ToLower() == CAddObjectsByMerge.GetRelativePath(folderPathOwn, ownFullPath).ToLower())
                    {
                        ownFullPathSave = ownFullPath;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(ownFullPathSave))
                {
                    diffs.AppendFormat(
                        "Пациент: '" + patientFIO + "'. Нозология: '" + patientNosology +
                        "'.\r\n  Название параметра: 'Содержимое личной папки'. У пациента из нашей базы в личной папке нет файла '{0}'.\r\n\r\n",
                        foreignFullPath);
                }
            }

            return diffs.ToString();
        }
        #endregion


        #region Добавление разных объектов в нашу и внешнюю базы

        #region Добавление пациентов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу пациента из внешней базы
        /// </summary>
        /// <param name="foreignPatientId">id пациента из внешней базы, которого надо добавить в нашу базу</param>
        public void AddPatientToOwnDatabase(int foreignPatientId)
        {
            if (foreignPatientId == -1)
            {
                return;
            }

            CPatient foreignPatient = _foreignPatientWorker.GetById(foreignPatientId);
            CPatient ownPatient = _ownPatientWorker.GetByGeneralData(
                    foreignPatient.GetFullName(), foreignPatient.Nosology, -1);

            if (ownPatient != null)
            {
                throw new Exception("Пациент " + foreignPatient.GetFullName() + " с нозологией " + foreignPatient.Nosology + " уже содержится в нашей базе");
            }

            _mergeForm.SetStatus("Добавление данных о пациенте '" + foreignPatient.GetFullName() + "' с нозологией '" + foreignPatient.Nosology + "' в нашу базу");
            CAddObjectsByMerge.AddPatient(
                _mergeForm, "own", _nosologyChangesForOwnDB, _ownNosologyWorker, _foreignWorkersKeeper, _ownWorkersKeeper, foreignPatient,
                _foreignPatientWorker, _ownPatientWorker, _foreignAnamneseWorker, _ownAnamneseWorker,
                _foreignObstetricHistoryWorker, _ownObstetricHistoryWorker, _foreignHospitalizationWorker, _ownHospitalizationWorker,
                _foreignMedicalInspectionWorker, _ownMedicalInspectionWorker, _foreignDischargeEpicrisisWorker, _ownDischargeEpicrisisWorker,
                _foreignLineOfCommunicationEpicrisisWorker, _ownLineOfCommunicationEpicrisisWorker,
                _foreignTransferableEpicrisisWorker, _ownTransferableEpicrisisWorker, _foreignOperationWorker, _ownOperationWorker,
                _foreignOperationProtocolWorker, _ownOperationProtocolWorker, _foreignVisitWorker, _ownVisitWorker,
                _foreignBrachialPlexusCardWorker, _ownBrachialPlexusCardWorker, _foreignObstetricParalysisCardWorker, _ownObstetricParalysisCardWorker,
                _foreignRangeOfMotionCardWorker, _ownRangeOfMotionCardWorker, _foreignCardWorker, _ownCardWorker);
            _mergeForm.SetStatus("\r\nДанные о пациенте '" + foreignPatient.GetFullName() + "' с нозологией '" + foreignPatient.Nosology + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу пациента из нашей базы
        /// </summary>
        /// <param name="ownPatientId">id пациента из нашей базы, которого надо добавить во внешнюю базу</param>
        public void AddPatientToForeignDatabase(int ownPatientId)
        {
            if (ownPatientId == -1)
            {
                return;
            }

            CPatient ownPatient = _ownPatientWorker.GetById(ownPatientId);
            CPatient foreignPatient = _foreignPatientWorker.GetByGeneralData(
                    ownPatient.GetFullName(), ownPatient.Nosology, -1);

            if (foreignPatient != null)
            {
                throw new Exception("Пациент " + ownPatient.GetFullName() + " с нозологией " + ownPatient.Nosology + " уже содержится во внешней базе");
            }

            _mergeForm.SetStatus("Добавление данных о пациенте " + ownPatient.GetFullName() + " с нозологией " + ownPatient.Nosology + " во внешнюю базу");
            CAddObjectsByMerge.AddPatient(
                _mergeForm, "foreign", _nosologyChangesForForeignDB, _foreignNosologyWorker, _ownWorkersKeeper, _foreignWorkersKeeper, ownPatient,
                _ownPatientWorker, _foreignPatientWorker, _ownAnamneseWorker, _foreignAnamneseWorker,
                _ownObstetricHistoryWorker, _foreignObstetricHistoryWorker, _ownHospitalizationWorker, _foreignHospitalizationWorker,
                _ownMedicalInspectionWorker, _foreignMedicalInspectionWorker, _ownDischargeEpicrisisWorker, _foreignDischargeEpicrisisWorker,
                _ownLineOfCommunicationEpicrisisWorker, _foreignLineOfCommunicationEpicrisisWorker,
                _ownTransferableEpicrisisWorker, _foreignTransferableEpicrisisWorker, _ownOperationWorker, _foreignOperationWorker,
                _ownOperationProtocolWorker, _foreignOperationProtocolWorker, _ownVisitWorker, _foreignVisitWorker,
                _ownBrachialPlexusCardWorker, _foreignBrachialPlexusCardWorker, _ownObstetricParalysisCardWorker, _foreignObstetricParalysisCardWorker,
                _ownRangeOfMotionCardWorker, _foreignRangeOfMotionCardWorker, _ownCardWorker, _foreignCardWorker);
            _mergeForm.SetStatus("\r\nДанные о пациенте '" + ownPatient.GetFullName() + "' с нозологией '" + ownPatient.Nosology + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление анамнезов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу анамнез пациента из внешней базы
        /// </summary>
        /// <param name="foreignPatientId">id пациента из внешней базы, анамнез которого надо добавить пациенту из нашей базы</param>
        /// <param name="ownPatientId">id пациента из нашей базы, которому надо добавить анамнез</param>
        public void AddAnamneseToOwnDatabase(int foreignPatientId, int ownPatientId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1)
            {
                return;
            }

            CPatient foreignPatient = _foreignPatientWorker.GetById(foreignPatientId);

            if (!_foreignAnamneseWorker.IsExists(foreignPatientId))
            {
                throw new Exception("У пациента " + foreignPatient.GetFullName() + " с нозологией " + foreignPatient.Nosology + " не обнаружен анамнез");
            }

            _mergeForm.SetStatus("Добавление данных об анамнезе пациента '" + foreignPatient.GetFullName() + "' с нозологией '" + foreignPatient.Nosology + "' в нашу базу");
            CAddObjectsByMerge.AddAnamnese(
                _foreignAnamneseWorker, _ownAnamneseWorker, foreignPatientId, ownPatientId);
            _mergeForm.SetStatus("\r\nДанные об анамнезе пациента '" + foreignPatient.GetFullName() + "' с нозологией '" + foreignPatient.Nosology + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу анамнез пациента из нашей базы
        /// </summary>
        /// <param name="ownPatientId">id пациента из нашей базы, анамнез которого надо добавить пациенту из внешней базы</param>
        /// <param name="foreignPatientId">id пациента из внешней базы, которому надо добавить анамнез</param>
        public void AddAnamneseToForeignDatabase(int ownPatientId, int foreignPatientId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1)
            {
                return;
            }

            CPatient ownPatient = _ownPatientWorker.GetById(ownPatientId);

            if (!_ownAnamneseWorker.IsExists(ownPatientId))
            {
                throw new Exception("У пациента " + ownPatient.GetFullName() + " с нозологией " + ownPatient.Nosology + " не обнаружен анамнез");
            }

            _mergeForm.SetStatus("Добавление данных об анамнезе пациента '" + ownPatient.GetFullName() + "' с нозологией '" + ownPatient.Nosology + "' во внешнюю базу");
            CAddObjectsByMerge.AddAnamnese(
                _ownAnamneseWorker, _foreignAnamneseWorker, ownPatientId, foreignPatientId);
            _mergeForm.SetStatus("\r\nДанные об анамнезе пациента '" + ownPatient.GetFullName() + "' с нозологией '" + ownPatient.Nosology + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление акушерских анамнезов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу акушерский анамнез пациента из внешней базы
        /// </summary>
        /// <param name="foreignPatientId">id пациента из внешней базы, акушерский анамнез которого надо добавить пациенту из нашей базы</param>
        /// <param name="ownPatientId">id пациента из нашей базы, которому надо добавить акушерский анамнез</param>
        public void AddObstetricHistoryToOwnDatabase(int foreignPatientId, int ownPatientId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1)
            {
                return;
            }

            CPatient foreignPatient = _foreignPatientWorker.GetById(foreignPatientId);

            if (!_foreignObstetricHistoryWorker.IsExists(foreignPatientId))
            {
                throw new Exception("У пациента " + foreignPatient.GetFullName() + " с нозологией " + foreignPatient.Nosology + " не обнаружен акушерский анамнез");
            }

            _mergeForm.SetStatus("Добавление данных об акушерском анамнезе пациента '" + foreignPatient.GetFullName() + "' с нозологией '" + foreignPatient.Nosology + "' в нашу базу");
            CAddObjectsByMerge.AddObstetricHistory(
                _foreignObstetricHistoryWorker, _ownObstetricHistoryWorker, foreignPatientId, ownPatientId);
            _mergeForm.SetStatus("\r\nДанные об акушерском анамнезе пациента '" + foreignPatient.GetFullName() + "' с нозологией '" + foreignPatient.Nosology + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу акушерский анамнез пациента из нашей базы
        /// </summary>
        /// <param name="ownPatientId">id пациента из нашей базы, акушерский анамнез которого надо добавить пациенту из внешней базы</param>
        /// <param name="foreignPatientId">id пациента из внешней базы, которому надо добавить акушерский анамнез</param>
        public void AddObstetricHistoryToForeignDatabase(int ownPatientId, int foreignPatientId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1)
            {
                return;
            }

            CPatient ownPatient = _ownPatientWorker.GetById(ownPatientId);

            if (!_ownObstetricHistoryWorker.IsExists(ownPatientId))
            {
                throw new Exception("У пациента " + ownPatient.GetFullName() + " с нозологией " + ownPatient.Nosology + " не обнаружен акушерский анамнез");
            }

            _mergeForm.SetStatus("Добавление данных об акушерском анамнезе пациента '" + ownPatient.GetFullName() + "' с нозологией '" + ownPatient.Nosology + "' во внешнюю базу");
            CAddObjectsByMerge.AddObstetricHistory(
                _ownObstetricHistoryWorker, _foreignObstetricHistoryWorker, ownPatientId, foreignPatientId);
            _mergeForm.SetStatus("\r\nДанные об акушерском анамнезе пациента '" + ownPatient.GetFullName() + "' с нозологией '" + ownPatient.Nosology + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление госпитализации в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу госпитализацию пациента из внешней базы
        /// </summary>
        /// <param name="foreignPatientId">id пациента из внешней базы, госпитализацию которого надо добавить пациенту из нашей базы</param>
        /// <param name="ownPatientId">id пациента из нашей базы, которому надо добавить госпитализацию</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которую надо добавить в нашу базу</param>
        public void AddHospitalizationToOwnDatabase(
            int foreignPatientId, int ownPatientId, int foreignHospitalizationId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CPatient foreignPatient = _foreignPatientWorker.GetById(foreignPatientId);
            CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о госпитализации за {0} пациента '{1}' с нозологией '{2}' в нашу базу.",
                CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true),
                foreignPatient.GetFullName(),
                foreignPatient.Nosology));

            CAddObjectsByMerge.AddHospitalization(
                _foreignPatientWorker,
                _ownPatientWorker,
                _foreignHospitalizationWorker,
                _ownHospitalizationWorker,
                _foreignMedicalInspectionWorker,
                _ownMedicalInspectionWorker,
                _foreignDischargeEpicrisisWorker,
                _ownDischargeEpicrisisWorker,
                _foreignLineOfCommunicationEpicrisisWorker,
                _ownLineOfCommunicationEpicrisisWorker,
                _foreignTransferableEpicrisisWorker,
                _ownTransferableEpicrisisWorker,
                _foreignOperationWorker,
                _ownOperationWorker,
                _foreignOperationProtocolWorker,
                _ownOperationProtocolWorker,
                _foreignBrachialPlexusCardWorker,
                _ownBrachialPlexusCardWorker,
                _foreignObstetricParalysisCardWorker,
                _ownObstetricParalysisCardWorker,
                _foreignRangeOfMotionCardWorker,
                _ownRangeOfMotionCardWorker,
                _foreignCardWorker,
                _ownCardWorker,
                foreignPatientId,
                ownPatientId,
                foreignHospitalizationId);

            _mergeForm.SetStatus(string.Format(
                "Данные о госпитализации за {0} пациента '{1}' с нозологией '{2}' успешно добавлены в нашу базу.",
                CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true),
                foreignPatient.GetFullName(),
                foreignPatient.Nosology));
        }


        /// <summary>
        /// Добавить во внешнюю базу госпитализацию пациента из нашей базы
        /// </summary>
        /// <param name="ownPatientId">id пациента из нашей базы, госпитализацию которого надо добавить пациенту из внешней базы</param>
        /// <param name="foreignPatientId">id пациента из внешней базы, которому надо добавить госпитализацию</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которую надо добавить во внешнеюю базу</param>
        public void AddHospitalizationToForeignDatabase(
            int ownPatientId, int foreignPatientId, int ownHospitalizationId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1 || ownHospitalizationId == -1)
            {
                return;
            }

            CPatient ownPatient = _ownPatientWorker.GetById(ownPatientId);
            CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о госпитализации за {0} пациента '{1}' с нозологией '{2}' во внешнюю базу.",
                CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true),
                ownPatient.GetFullName(),
                ownPatient.Nosology));

            CAddObjectsByMerge.AddHospitalization(
                _ownPatientWorker,
                _foreignPatientWorker,
                _ownHospitalizationWorker,
                _foreignHospitalizationWorker,
                _ownMedicalInspectionWorker,
                _foreignMedicalInspectionWorker,
                _ownDischargeEpicrisisWorker,
                _foreignDischargeEpicrisisWorker,
                _ownLineOfCommunicationEpicrisisWorker,
                _foreignLineOfCommunicationEpicrisisWorker,
                _ownTransferableEpicrisisWorker,
                _foreignTransferableEpicrisisWorker,
                _ownOperationWorker,
                _foreignOperationWorker,
                _ownOperationProtocolWorker,
                _foreignOperationProtocolWorker,
                _ownBrachialPlexusCardWorker,
                _foreignBrachialPlexusCardWorker,
                _ownObstetricParalysisCardWorker,
                _foreignObstetricParalysisCardWorker,
                _ownRangeOfMotionCardWorker,
                _foreignRangeOfMotionCardWorker,
                _ownCardWorker,
                _foreignCardWorker,
                ownPatientId,
                foreignPatientId,
                ownHospitalizationId);

            _mergeForm.SetStatus(string.Format(
                "Данные о госпитализации за {0} пациента '{1}' с нозологией '{2}' успешно добавлены во внешнюю базу.",
                CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true),
                ownPatient.GetFullName(),
                ownPatient.Nosology));
        }
        #endregion


        #region Добавление выписных эпикризов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу выписной эпикриз госпитализации из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, выписной эпикриз которого надо добавить госпитализации из нашей базы</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которому надо добавить выписной эпикриз</param>
        public void AddDischargeEpicrisisToOwnDatabase(
            int foreignHospitalizationId, int ownHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            string date = CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true);

            if (!_foreignDischargeEpicrisisWorker.IsExists(foreignHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен выписной эпикриз");
            }

            _mergeForm.SetStatus("Добавление данных о выписном эпикризе госпитализации за '" + date + "' в нашу базу");
            CAddObjectsByMerge.AddDischargeEpicrisis(
                _foreignDischargeEpicrisisWorker,
                _ownDischargeEpicrisisWorker,
                foreignHospitalizationId,
                ownHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные о выписном эпикризе госпитализации за '" + date + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу выписной эпикриз госпитализации из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, выписной эпикриз которого надо добавить госпитализации из внешней базы</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которому надо добавить выписной эпикриз</param>
        public void AddDischargeEpicrisisToForeignDatabase(
            int ownHospitalizationId, int foreignHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            string date = CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true);

            if (!_ownDischargeEpicrisisWorker.IsExists(ownHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен выписной эпикриз");
            }

            _mergeForm.SetStatus("Добавление данных о выписном эпикризе госпитализации за '" + date + "' во внешнюю базу");
            CAddObjectsByMerge.AddDischargeEpicrisis(
                _ownDischargeEpicrisisWorker,
                _foreignDischargeEpicrisisWorker,
                ownHospitalizationId,
                foreignHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные о выписном эпикризе госпитализации за '" + date + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление этапных эпикризов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу этапный эпикриз госпитализации из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, этапный эпикриз которого надо добавить госпитализации из нашей базы</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которому надо добавить этапный эпикриз</param>
        public void AddLineOfCommunicationEpicrisisToOwnDatabase(
            int foreignHospitalizationId, int ownHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            string date = CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true);

            if (!_foreignLineOfCommunicationEpicrisisWorker.IsExists(foreignHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен этапный эпикриз");
            }

            _mergeForm.SetStatus("Добавление данных об этапном эпикризе госпитализации за '" + date + "' в нашу базу");
            CAddObjectsByMerge.AddLineOfCommunicationEpicrisis(
                _foreignLineOfCommunicationEpicrisisWorker,
                _ownLineOfCommunicationEpicrisisWorker,
                foreignHospitalizationId,
                ownHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные об этапном эпикризе госпитализации за '" + date + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу этапный эпикриз госпитализации из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, этапный эпикриз которого надо добавить госпитализации из внешней базы</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которому надо добавить этапный эпикриз</param>
        public void AddLineOfCommunicationEpicrisisToForeignDatabase(
            int ownHospitalizationId, int foreignHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            string date = CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true);

            if (!_ownLineOfCommunicationEpicrisisWorker.IsExists(ownHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен этапный эпикриз");
            }

            _mergeForm.SetStatus("Добавление данных об этапном эпикризе госпитализации за '" + date + "' во внешнюю базу");
            CAddObjectsByMerge.AddLineOfCommunicationEpicrisis(
                _ownLineOfCommunicationEpicrisisWorker,
                _foreignLineOfCommunicationEpicrisisWorker,
                ownHospitalizationId,
                foreignHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные об этапном эпикризе госпитализации за '" + date + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление переводных эпикризов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу переводной эпикриз госпитализации из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, переводной эпикриз которого надо добавить госпитализации из нашей базы</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которому надо добавить переводной эпикриз</param>
        public void AddTransferableEpicrisisToOwnDatabase(
            int foreignHospitalizationId, int ownHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            string date = CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true);

            if (!_foreignTransferableEpicrisisWorker.IsExists(foreignHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен переводной эпикриз");
            }

            _mergeForm.SetStatus("Добавление данных о переводном эпикризе госпитализации за '" + date + "' в нашу базу");
            CAddObjectsByMerge.AddTransferableEpicrisis(
                _foreignTransferableEpicrisisWorker,
                _ownTransferableEpicrisisWorker,
                foreignHospitalizationId,
                ownHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные о переводном эпикризе госпитализации за '" + date + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу переводной эпикриз госпитализации из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, переводной эпикриз которого надо добавить госпитализации из внешней базы</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которому надо добавить переводной эпикриз</param>
        public void AddTransferableEpicrisisToForeignDatabase(
            int ownHospitalizationId, int foreignHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            string date = CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true);

            if (!_ownTransferableEpicrisisWorker.IsExists(ownHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен переводной эпикриз");
            }

            _mergeForm.SetStatus("Добавление данных о переводном эпикризе госпитализации за '" + date + "' во внешнюю базу");
            CAddObjectsByMerge.AddTransferableEpicrisis(
                _ownTransferableEpicrisisWorker,
                _foreignTransferableEpicrisisWorker,
                ownHospitalizationId,
                foreignHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные о переводном эпикризе госпитализации за '" + date + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление осмотров в отделении в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу осмотр в отделении для госпитализации из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, осмотр в отделении которого надо добавить госпитализации из нашей базы</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которому надо добавить осмотр в отделении</param>
        public void AddMedicalInspectionToOwnDatabase(
            int foreignHospitalizationId, int ownHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            string date = CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true);

            if (!_foreignMedicalInspectionWorker.IsExists(foreignHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен осмотр в отделении");
            }

            _mergeForm.SetStatus("Добавление данных об осмотре в отделении для госпитализации за '" + date + "' в нашу базу");
            CAddObjectsByMerge.AddMedicalInspection(
                _foreignMedicalInspectionWorker,
                _ownMedicalInspectionWorker,
                foreignHospitalizationId,
                ownHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные об осмотре в отделении для госпитализации за '" + date + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу осмотр в отделении для госпитализации из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, осмотр в отделении которого надо добавить госпитализации из внешней базы</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которому надо добавить осмотр в отделении</param>
        public void AddMedicalInspectionToForeignDatabase(
            int ownHospitalizationId, int foreignHospitalizationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1)
            {
                return;
            }

            CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            string date = CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true);

            if (!_ownMedicalInspectionWorker.IsExists(ownHospitalizationId))
            {
                throw new Exception("У госпитализации " + date + " не обнаружен осмотр в отделении");
            }

            _mergeForm.SetStatus("Добавление данных об осмотре в отделении для госпитализации за '" + date + "' во внешнюю базу");
            CAddObjectsByMerge.AddMedicalInspection(
                _ownMedicalInspectionWorker,
                _foreignMedicalInspectionWorker,
                ownHospitalizationId,
                foreignHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные об осмотре в отделении для госпитализации за '" + date + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление операций и предоперационных протоколов в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу операцию и протокол из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, операцию и протокол которой надо добавить госпитализации из нашей базы</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которой надо добавить операцию и протокол</param>
        /// <param name="foreignOperationId">id операции из внешней базы, которую надо добавить в нашу базу</param>
        public void AddOperationAndProtocolToOwnDatabase(
            int foreignHospitalizationId, int ownHospitalizationId, int foreignOperationId)
        {
            if (foreignHospitalizationId == -1 || ownHospitalizationId == -1 || foreignOperationId == -1)
            {
                return;
            }

            CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            string date = CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true);
            COperation operation = _foreignOperationWorker.GetById(foreignOperationId);

            if (operation == null)
            {
                throw new Exception("У госпитализации за " + date + " не обнаружена операция с id " + foreignOperationId);
            }

            _mergeForm.SetStatus("Добавление данных об операции '" + operation.Name + "' и предоперационном протоколе для госпитализации за '" + date + "' в нашу базу");
            CAddObjectsByMerge.AddOperationAndProtocol(
                _ownHospitalizationWorker,
                _foreignOperationWorker,
                _ownOperationWorker,
                _foreignOperationProtocolWorker,
                _ownOperationProtocolWorker,
                foreignOperationId,
                ownHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные об операции '" + operation.Name + "' и предоперационном протоколе для госпитализации за '" + date + "' успешно добавлены в нашу базу");
        }


        /// <summary>
        /// Добавить во внешнюю базу операцию и протокол госпитализации из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, операцию и протокол которой надо добавить госпитализации из внешней базы</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которой надо добавить операцию и протокол</param>
        /// <param name="ownOperationId">id операции из нашей базы, которую надо добавить во внешнюю базу</param>
        public void AddOperationAndProtocolToForeignDatabase(
            int ownHospitalizationId, int foreignHospitalizationId, int ownOperationId)
        {
            if (ownHospitalizationId == -1 || foreignHospitalizationId == -1 || ownOperationId == -1)
            {
                return;
            }

            CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            string date = CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true);
            COperation operation = _ownOperationWorker.GetById(ownOperationId);

            if (operation == null)
            {
                throw new Exception("У госпитализации за " + date + " не обнаружена операция с id " + ownOperationId);
            }

            _mergeForm.SetStatus("Добавление данных об операции '" + operation.Name + "' и предоперационном протоколе для госпитализации за '" + date + "' во внешнюю базу");
            CAddObjectsByMerge.AddOperationAndProtocol(
                _foreignHospitalizationWorker,
                _ownOperationWorker,
                _foreignOperationWorker,
                _ownOperationProtocolWorker,
                _foreignOperationProtocolWorker,
                ownOperationId,
                foreignHospitalizationId);
            _mergeForm.SetStatus("\r\nДанные об операции '" + operation.Name + "' и предоперационном протоколе для госпитализации за '" + date + "' успешно добавлены во внешнюю базу");
        }
        #endregion


        #region Добавление консультации в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу консультацию пациента из внешней базы
        /// </summary>
        /// <param name="foreignPatientId">id пациента из внешней базы, консультацию которого надо добавить пациенту из нашей базы</param>
        /// <param name="ownPatientId">id пациента из нашей базы, которому надо добавить консультацию</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, который надо добавить в нашу базу</param>
        public void AddVisitToOwnDatabase(
            int foreignPatientId, int ownPatientId, int foreignVisitId)
        {
            if (foreignPatientId == -1 || ownPatientId == -1 || foreignVisitId == -1)
            {
                return;
            }

            CPatient foreignPatient = _foreignPatientWorker.GetById(foreignPatientId);
            CVisit foreignVisit = _foreignVisitWorker.GetById(foreignVisitId);

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о консультации за {0} пациента '{1}' с нозологией '{2}' в нашу базу.",
                CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true),
                foreignPatient.GetFullName(),
                foreignPatient.Nosology));

            CAddObjectsByMerge.AddVisit(
                _foreignPatientWorker,
                _ownPatientWorker,
                _foreignVisitWorker,
                _ownVisitWorker,
                _foreignBrachialPlexusCardWorker,
                _ownBrachialPlexusCardWorker,
                _foreignObstetricParalysisCardWorker,
                _ownObstetricParalysisCardWorker,
                _foreignRangeOfMotionCardWorker,
                _ownRangeOfMotionCardWorker,
                _foreignCardWorker,
                _ownCardWorker,
                foreignPatientId,
                ownPatientId,
                foreignVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о консультации за {0} пациента '{1}' с нозологией '{2}' успешно добавлены в нашу базу.",
                CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true),
                foreignPatient.GetFullName(),
                foreignPatient.Nosology));
        }


        /// <summary>
        /// Добавить во внешнюю базу консультацию пациента из нашей базы
        /// </summary>
        /// <param name="ownPatientId">id пациента из нашей базы, консультацию которого надо добавить пациенту из внешней базы</param>
        /// <param name="foreignPatientId">id пациента из внешней базы, которому надо добавить консультацию</param>
        /// <param name="ownVisitId">id консультации из нашей базы, который надо добавить во внешнеюю базу</param>
        public void AddVisitToForeignDatabase(
            int ownPatientId, int foreignPatientId, int ownVisitId)
        {
            if (ownPatientId == -1 || foreignPatientId == -1 || ownVisitId == -1)
            {
                return;
            }

            CPatient ownPatient = _ownPatientWorker.GetById(ownPatientId);
            CVisit ownVisit = _ownVisitWorker.GetById(ownVisitId);

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о консультации за {0} пациента '{1}' с нозологией '{2}' во внешнюю базу.",
                CConvertEngine.DateTimeToString(ownVisit.VisitDate, true),
                ownPatient.GetFullName(),
                ownPatient.Nosology));

            CAddObjectsByMerge.AddVisit(
                _ownPatientWorker,
                _foreignPatientWorker,
                _ownVisitWorker,
                _foreignVisitWorker,
                _ownBrachialPlexusCardWorker,
                _foreignBrachialPlexusCardWorker,
                _ownObstetricParalysisCardWorker,
                _foreignObstetricParalysisCardWorker,
                _ownRangeOfMotionCardWorker,
                _foreignRangeOfMotionCardWorker,
                _ownCardWorker,
                _foreignCardWorker,
                ownPatientId,
                foreignPatientId,
                ownVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о консультации за {0} пациента '{1}' с нозологией '{2}' успешно добавлены во внешнюю базу.",
                CConvertEngine.DateTimeToString(ownVisit.VisitDate, true),
                ownPatient.GetFullName(),
                ownPatient.Nosology));
        }
        #endregion


        #region Добавление карты на плечевое сплетение в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу карту на плечевое сплетение из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, карту на плечевое сплетение которой надо добавить в нашу базу</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которой надо добавить карту на плечевое сплетение</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, карту на плечевое сплетение которого надо добавить в нашу базу</param>
        /// <param name="ownVisitId">id консультации из нашей базы, которой надо добавить карту на плечевое сплетение</param>
        public void AddBrachialPlexusCardToOwnDatabase(
            int foreignHospitalizationId,
            int ownHospitalizationId,
            int foreignVisitId,
            int ownVisitId)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (foreignHospitalizationId == -1)
            {
                CVisit foreignVisit = _foreignVisitWorker.GetById(foreignVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте на плечевое сплетение для {0} в нашу базу.", infoStr));

            CAddObjectsByMerge.AddBrachialPlexusCard(
                _foreignBrachialPlexusCardWorker,
                _ownBrachialPlexusCardWorker,
                foreignHospitalizationId,
                ownHospitalizationId,
                foreignVisitId,
                ownVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте на плечевое сплетение для {0} успешно добавлены в нашу базу.", infoStr));
        }


        /// <summary>
        /// Добавить во внешнюю базу карту на плечевое сплетение из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, карту на плечевое сплетение которой надо добавить во внешнюю базу</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которой надо добавить карту на плечевое сплетение</param>
        /// <param name="ownVisitId">id консультации из нашей базы, карту на плечевое сплетение которого надо добавить во внешнюю базу</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, которому надо добавить карту на плечевое сплетение</param>
        public void AddBrachialPlexusCardToForeignDatabase(
            int ownHospitalizationId,
            int foreignHospitalizationId,
            int ownVisitId,
            int foreignVisitId)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (ownHospitalizationId == -1)
            {
                CVisit ownVisit = _ownVisitWorker.GetById(ownVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(ownVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте на плечевое сплетение для {0} во внешнюю базу.", infoStr));

            CAddObjectsByMerge.AddBrachialPlexusCard(
                _ownBrachialPlexusCardWorker,
                _foreignBrachialPlexusCardWorker,
                ownHospitalizationId,
                foreignHospitalizationId,
                ownVisitId,
                foreignVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте на плечевое сплетение для {0} успешно добавлены во внешнюю базу.", infoStr));
        }
        #endregion


        #region Добавление карты на акушерский паралич в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу карту на акушерский паралич из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, карту на акушерский паралич которой надо добавить в нашу базу</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которой надо добавить карту на акушерский паралич</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, карту на акушерский паралич которого надо добавить в нашу базу</param>
        /// <param name="ownVisitId">id консультации из нашей базы, которому надо добавить карту на акушерский паралич</param>
        public void AddObstetricParalysisCardToOwnDatabase(
            int foreignHospitalizationId,
            int ownHospitalizationId,
            int foreignVisitId,
            int ownVisitId)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (foreignHospitalizationId == -1)
            {
                CVisit foreignVisit = _foreignVisitWorker.GetById(foreignVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте на акушерский паралич для {0} в нашу базу.", infoStr));

            CAddObjectsByMerge.AddObstetricParalysisCard(
                _foreignObstetricParalysisCardWorker,
                _ownObstetricParalysisCardWorker,
                foreignHospitalizationId,
                ownHospitalizationId,
                foreignVisitId,
                ownVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте на акушерский паралич для {0} успешно добавлены в нашу базу.", infoStr));
        }


        /// <summary>
        /// Добавить во внешнюю базу карту на акушерский паралич из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, карту на акушерский паралич которой надо добавить во внешнюю базу</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которой надо добавить карту на акушерский паралич</param>
        /// <param name="ownVisitId">id консультации из нашей базы, карту на акушерский паралич которого надо добавить во внешнюю базу</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, которому надо добавить карту на акушерский паралич</param>
        public void AddObstetricParalysisCardToForeignDatabase(
            int ownHospitalizationId,
            int foreignHospitalizationId,
            int ownVisitId,
            int foreignVisitId)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (ownHospitalizationId == -1)
            {
                CVisit ownVisit = _ownVisitWorker.GetById(ownVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(ownVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте на акушерский паралич для {0} во внешнюю базу.", infoStr));

            CAddObjectsByMerge.AddObstetricParalysisCard(
                _ownObstetricParalysisCardWorker,
                _foreignObstetricParalysisCardWorker,
                ownHospitalizationId,
                foreignHospitalizationId,
                ownVisitId,
                foreignVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте на акушерский паралич для {0} успешно добавлены во внешнюю базу.", infoStr));
        }
        #endregion


        #region Добавление карты на объём движений в нашу и внешнюю базы
        /// <summary>
        /// Добавить в нашу базу карту на объём движений из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, карту на объём движений которой надо добавить в нашу базу</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которой надо добавить карту на объём движений</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, карту на объём движений которого надо добавить в нашу базу</param>
        /// <param name="ownVisitId">id консультации из нашей базы, которому надо добавить карту на объём движений</param>
        public void AddRangeOfMotionCardToOwnDatabase(
            int foreignHospitalizationId,
            int ownHospitalizationId,
            int foreignVisitId,
            int ownVisitId)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (foreignHospitalizationId == -1)
            {
                CVisit foreignVisit = _foreignVisitWorker.GetById(foreignVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте на объём движений для {0} в нашу базу.", infoStr));

            CAddObjectsByMerge.AddRangeOfMotionCard(
                _foreignRangeOfMotionCardWorker,
                _ownRangeOfMotionCardWorker,
                foreignHospitalizationId,
                ownHospitalizationId,
                foreignVisitId,
                ownVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте на объём движений для {0} успешно добавлены в нашу базу.", infoStr));
        }


        /// <summary>
        /// Добавить во внешнюю базу карту на объём движений из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, карту на объём движений которой надо добавить во внешнюю базу</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которой надо добавить карту на объём движений</param>
        /// <param name="ownVisitId">id консультации из нашей базы, карту на объём движений которого надо добавить во внешнюю базу</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, которому надо добавить карту на объём движений</param>
        public void AddRangeOfMotionCardToForeignDatabase(
            int ownHospitalizationId,
            int foreignHospitalizationId,
            int ownVisitId,
            int foreignVisitId)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (ownHospitalizationId == -1)
            {
                CVisit ownVisit = _ownVisitWorker.GetById(ownVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(ownVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте на объём движений для {0} во внешнюю базу.", infoStr));

            CAddObjectsByMerge.AddRangeOfMotionCard(
                _ownRangeOfMotionCardWorker,
                _foreignRangeOfMotionCardWorker,
                ownHospitalizationId,
                foreignHospitalizationId,
                ownVisitId,
                foreignVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте на объём движений для {0} успешно добавлены во внешнюю базу.", infoStr));
        }
        #endregion


        #region Добавление карты с двумя частями в нашу и внешнюю базы

        /// <summary>
        /// Добавить в нашу базу карту с двумя частями из внешней базы
        /// </summary>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, карту с двумя частями которой надо добавить в нашу базу</param>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, которой надо добавить карту с двумя частями</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, карту с двумя частями которого надо добавить в нашу базу</param>
        /// <param name="ownVisitId">id консультации из нашей базы, которому надо добавить карту с двумя частями</param>
        /// <param name="cardType"></param>
        public void AddLeftRightCardToOwnDatabase(
            int foreignHospitalizationId,
            int ownHospitalizationId,
            int foreignVisitId,
            int ownVisitId,
            CardType cardType)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (foreignHospitalizationId == -1)
            {
                CVisit foreignVisit = _foreignVisitWorker.GetById(foreignVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(foreignVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization foreignHospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(foreignHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте с двумя частями для {0} в нашу базу.", infoStr));

            CAddObjectsByMerge.AddLeftRightCard(
                _foreignCardWorker,
                _ownCardWorker,
                cardType,
                foreignHospitalizationId,
                ownHospitalizationId,
                foreignVisitId,
                ownVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте с двумя частями для {0} успешно добавлены в нашу базу.", infoStr));
        }


        /// <summary>
        /// Добавить во внешнюю базу карту с двумя частями из нашей базы
        /// </summary>
        /// <param name="ownHospitalizationId">id госпитализации из нашей базы, карту с двумя частями которой надо добавить во внешнюю базу</param>
        /// <param name="foreignHospitalizationId">id госпитализации из внешней базы, которой надо добавить карту с двумя частями</param>
        /// <param name="ownVisitId">id консультации из нашей базы, карту с двумя частями которого надо добавить во внешнюю базу</param>
        /// <param name="foreignVisitId">id консультации из внешней базы, которому надо добавить карту с двумя частями</param>
        /// <param name="cardType"></param>
        public void AddLeftRightCardToForeignDatabase(
            int ownHospitalizationId,
            int foreignHospitalizationId,
            int ownVisitId,
            int foreignVisitId,
            CardType cardType)
        {
            if ((ownHospitalizationId == -1 && ownVisitId == -1) || (foreignHospitalizationId == -1 && foreignVisitId == -1))
            {
                return;
            }

            string infoStr;
            if (ownHospitalizationId == -1)
            {
                CVisit ownVisit = _ownVisitWorker.GetById(ownVisitId);
                infoStr = "консультации за '" + CConvertEngine.DateTimeToString(ownVisit.VisitDate, true) + "'";
            }
            else
            {
                CHospitalization ownHospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                infoStr = "госпитализации за '" + CConvertEngine.DateTimeToString(ownHospitalization.DeliveryDate, true) + "'";
            }

            _mergeForm.SetStatus(string.Format(
                "Добавление данных о карте {0} для {1} во внешнюю базу.", cardType, infoStr));

            CAddObjectsByMerge.AddLeftRightCard(
                _ownCardWorker,
                _foreignCardWorker,
                cardType,
                ownHospitalizationId,
                foreignHospitalizationId,
                ownVisitId,
                foreignVisitId);

            _mergeForm.SetStatus(string.Format(
                "Данные о карте {0} для {1} успешно добавлены во внешнюю базу.", cardType, infoStr));
        }
        #endregion


        #region Изменение параметров у пациента
        private void ChangePatient(ObjectType typeOfObject, CPatient patient, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.PatientBirthday:
                    patient.Birthday = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Информация о дне рождения" + messagePart);
                    break;
                case ObjectType.PatientBuildingNumber:
                    patient.BuildingNumber = value;
                    _mergeForm.SetStatus("Информация о корпусе дома проживания" + messagePart);
                    break;
                case ObjectType.PatientCityName:
                    patient.CityName = value;
                    _mergeForm.SetStatus("Информация о названии города проживания" + messagePart);
                    break;
                case ObjectType.PatientEMail:
                    patient.EMail = value;
                    _mergeForm.SetStatus("Информация о e-mail" + messagePart);
                    break;
                case ObjectType.PatientFlatNumber:
                    patient.FlatNumber = value;
                    _mergeForm.SetStatus("Информация о номере квартиры проживания" + messagePart);
                    break;
                case ObjectType.PatientHomeNumber:
                    patient.HomeNumber = value;
                    _mergeForm.SetStatus("Информация о номере дома проживания" + messagePart);
                    break;
                case ObjectType.PatientInsuranceName:
                    patient.InsuranceName = value;
                    _mergeForm.SetStatus("Информация о названии страховой компании" + messagePart);
                    break;
                case ObjectType.PatientInsuranceNumber:
                    patient.InsuranceNumber = value;
                    _mergeForm.SetStatus("Информация о номере страховой компании" + messagePart);
                    break;
                case ObjectType.PatientInsuranceSeries:
                    patient.InsuranceSeries = value;
                    _mergeForm.SetStatus("Информация о серии страховой компании" + messagePart);
                    break;
                case ObjectType.PatientInsuranceType:
                    patient.InsuranceType = value;
                    _mergeForm.SetStatus("Информация о типе страховой компании" + messagePart);
                    break;
                case ObjectType.PatientPassInformationDeliveryDate:
                    if (value == "Нет значения")
                    {
                        patient.PassInformation.DeliveryDate = null;
                    }
                    else
                    {
                        patient.PassInformation.DeliveryDate = CConvertEngine.StringToDateTime(value);
                    }

                    _mergeForm.SetStatus("Информация о паспорте (дата выдачи)" + messagePart);
                    break;
                case ObjectType.PatientPassInformationNumber:
                    patient.PassInformation.Number = value;
                    _mergeForm.SetStatus("Информация о паспорте (номер)" + messagePart);
                    break;
                case ObjectType.PatientPassInformationOrganization:
                    patient.PassInformation.Organization = value;
                    _mergeForm.SetStatus("Информация о паспорте (организация, выдавшая паспорт)" + messagePart);
                    break;
                case ObjectType.PatientPassInformationSeries:
                    patient.PassInformation.Series = value;
                    _mergeForm.SetStatus("Информация о паспорте (серия)" + messagePart);
                    break;
                case ObjectType.PatientPassInformationSubdivisionCode:
                    patient.PassInformation.SubdivisionCode = value;
                    _mergeForm.SetStatus("Информация о паспорте (код подразделения)" + messagePart);
                    break;
                case ObjectType.PatientPhone:
                    patient.Phone = value;
                    _mergeForm.SetStatus("Информация о телефоне" + messagePart);
                    break;
                case ObjectType.PatientRelatives:
                    patient.Relatives = value;
                    _mergeForm.SetStatus("Информация о родственниках" + messagePart);
                    break;
                case ObjectType.PatientStreetName:
                    patient.StreetName = value;
                    _mergeForm.SetStatus("Улица проживания" + messagePart);
                    break;
            }
        }

        public void ChangePatientPrameterInOwnDatabase(ObjectType typeOfObject, int ownPatientId, string value)
        {
            CPatient patient = _ownPatientWorker.GetById(ownPatientId);
            string messagePart = string.Format(
                " пациента '{0}' с нозологией '{1}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology);
            ChangePatient(typeOfObject, patient, value, messagePart);
            _ownPatientWorker.Update(patient);
        }

        public void ChangePatientPrameterInForeignDatabase(ObjectType typeOfObject, int foreignPatientId, string value)
        {
            CPatient patient = _foreignPatientWorker.GetById(foreignPatientId);
            string messagePart = string.Format(
                " пациента '{0}' с нозологией '{1}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology);
            ChangePatient(typeOfObject, patient, value, messagePart);
            _foreignPatientWorker.Update(patient);
        }
        #endregion


        #region Изменение параметров у анамнеза
        private void ChangeAnamnese(ObjectType typeOfObject, CAnamnese anamnese, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.AnamneseAnMorbi:
                    anamnese.AnMorbi = value;
                    _mergeForm.SetStatus("Описание" + messagePart);
                    break;
                case ObjectType.AnamneseTraumaDate:
                    if (value == "Нет значения")
                    {
                        anamnese.TraumaDate = null;
                    }
                    else
                    {
                        anamnese.TraumaDate = CConvertEngine.StringToDateTime(value);
                    }

                    _mergeForm.SetStatus("Дата травмы" + messagePart);
                    break;
            }
        }

        public void ChangeAnamnesePrameterInOwnDatabase(ObjectType typeOfObject, int ownPatientId, string value)
        {
            CPatient patient = _ownPatientWorker.GetById(ownPatientId);
            string messagePart = string.Format(
                " для анамнеза пациента '{0}' с нозологией '{1}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology);
            CAnamnese anamnese = _ownAnamneseWorker.GetByPatientId(ownPatientId);
            ChangeAnamnese(typeOfObject, anamnese, value, messagePart);
            _ownAnamneseWorker.Update(anamnese);
        }

        public void ChangeAnamnesePrameterInForeignDatabase(ObjectType typeOfObject, int foreignPatientId, string value)
        {
            CPatient patient = _foreignPatientWorker.GetById(foreignPatientId);
            string messagePart = string.Format(
                " для анамнеза пациента '{0}' с нозологией '{1}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology);
            CAnamnese anamnese = _foreignAnamneseWorker.GetByPatientId(foreignPatientId);
            ChangeAnamnese(typeOfObject, anamnese, value, messagePart);
            _foreignAnamneseWorker.Update(anamnese);
        }
        #endregion


        #region Изменение параметров у акушерского анамнеза
        private void ChangeObstetricHistory(ObjectType typeOfObject, CObstetricHistory obstetricHistory, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.ObstetricHistoryApgarScore:
                    obstetricHistory.ApgarScore = value;
                    _mergeForm.SetStatus("Информация о шкале Апгар" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryBirthInjury:
                    obstetricHistory.BirthInjury = value;
                    _mergeForm.SetStatus("Информация о родовых травмах" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryChildbirthWeeks:
                    obstetricHistory.ChildbirthWeeks = value;
                    _mergeForm.SetStatus("Информация о сроке родов" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryComplicationsDuringChildbirth:
                    obstetricHistory.ComplicationsDuringChildbirth = value;
                    _mergeForm.SetStatus("Информация об осложнениях в ходе родов" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryComplicationsInPregnancy:
                    obstetricHistory.ComplicationsInPregnancy = value;
                    _mergeForm.SetStatus("Информация об осложнениях в период беременности" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryDrugsInPregnancy:
                    obstetricHistory.DrugsInPregnancy = value;
                    _mergeForm.SetStatus("Информация о лекарственных препаратах и хронических интоксикациях в период беременности" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryDurationOfLabor:
                    obstetricHistory.DurationOfLabor = value;
                    _mergeForm.SetStatus("Информация о длительности родов" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryFetal:
                    obstetricHistory.Fetal = value;
                    _mergeForm.SetStatus("Информация о предлежании" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryHeightAtBirth:
                    obstetricHistory.HeightAtBirth = value;
                    _mergeForm.SetStatus("Информация о росте при рождении" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryHospitalTreatment:
                    obstetricHistory.HospitalTreatment = value;
                    _mergeForm.SetStatus("Информация о стационарном лечении" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryIsTongsUsing:
                    obstetricHistory.IsTongsUsing = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Информация об использовании щипцов в ходе родов" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryIsVacuumUsing:
                    obstetricHistory.IsVacuumUsing = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Информация об использовании ваккума в ходе родов" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryObstetricParalysis:
                    obstetricHistory.ObstetricParalysis = value;
                    _mergeForm.SetStatus("Информация о том, когда и кем диагностирован акушерский паралич" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryOutpatientCare:
                    obstetricHistory.OutpatientCare = value;
                    _mergeForm.SetStatus("Информация об амбулаторном лечении" + messagePart);
                    break;
                case ObjectType.ObstetricHistoryWeightAtBirth:
                    obstetricHistory.WeightAtBirth = value;
                    _mergeForm.SetStatus("Информация о весе при рождении" + messagePart);
                    break;
            }
        }

        private void ChangeObstetricHistory(ObjectType typeOfObject, CObstetricHistory obstetricHistory, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.ObstetricHistoryChronology:
                    obstetricHistory.Chronology = (bool[])value;
                    _mergeForm.SetStatus("Хронология восстановления активных движений верхней конечности" + messagePart);
                    break;
            }
        }

        public void ChangeObstetricHistoryPrameterInOwnDatabase(ObjectType typeOfObject, int ownPatientId, object value)
        {
            CPatient patient = _ownPatientWorker.GetById(ownPatientId);
            string messagePart = string.Format(
                " для акушерского анамнеза пациента '{0}' с нозологией '{1}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology);

            CObstetricHistory obstetricHistory = _ownObstetricHistoryWorker.GetByPatientId(ownPatientId);
            if (value is string)
            {
                ChangeObstetricHistory(typeOfObject, obstetricHistory, (string)value, messagePart);
            }
            else
            {
                ChangeObstetricHistory(typeOfObject, obstetricHistory, value, messagePart);
            }

            _ownObstetricHistoryWorker.Update(obstetricHistory);
        }

        public void ChangeObstetricHistoryPrameterInForeignDatabase(ObjectType typeOfObject, int foreignPatientId, object value)
        {
            CPatient patient = _foreignPatientWorker.GetById(foreignPatientId);
            string messagePart = string.Format(
                " для акушерского анамнеза пациента '{0}' с нозологией '{1}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology);

            CObstetricHistory obstetricHistory = _foreignObstetricHistoryWorker.GetByPatientId(foreignPatientId);
            if (value is string)
            {
                ChangeObstetricHistory(typeOfObject, obstetricHistory, (string)value, messagePart);
            }
            else
            {
                ChangeObstetricHistory(typeOfObject, obstetricHistory, value, messagePart);
            }

            _foreignObstetricHistoryWorker.Update(obstetricHistory);
        }
        #endregion


        #region Изменение параметров у госпитализации
        private void ChangeHospitalization(ObjectType typeOfObject, CHospitalization hospitalization, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.HospitalizationDiagnose:
                    hospitalization.Diagnose = value;
                    _mergeForm.SetStatus("Диагоноз" + messagePart);
                    break;
                case ObjectType.HospitalizationDoctorInChargeOfTheCase:
                    hospitalization.DoctorInChargeOfTheCase = value;
                    _mergeForm.SetStatus("Лечащий врач" + messagePart);
                    break;
                case ObjectType.HospitalizationFotoFolderName:
                    hospitalization.FotoFolderName = value;
                    _mergeForm.SetStatus("Название папки с фотографиями" + messagePart);
                    break;
                case ObjectType.HospitalizationNumberOfCaseHistory:
                    hospitalization.NumberOfCaseHistory = value;
                    _mergeForm.SetStatus("Номер истории болезни" + messagePart);
                    break;
                case ObjectType.HospitalizationReleaseDate:
                    if (value == "Нет значения")
                    {
                        hospitalization.ReleaseDate = null;
                    }
                    else
                    {
                        hospitalization.ReleaseDate = CConvertEngine.StringToDateTime(value);
                    }

                    _mergeForm.SetStatus("Дата выписки" + messagePart);
                    break;
            }
        }

        public void ChangeHospitalizationPrameterInOwnDatabase(ObjectType typeOfObject, int ownPatientId, int ownHospitalizationId, string value)
        {
            CPatient patient = _ownPatientWorker.GetById(ownPatientId);
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            string messagePart = string.Format(
                " для госпитализации за '{0}' для пациента '{1}' с нозологией '{2}' успешно изменена в нашей базе.",
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true),
                patient.GetFullName(),
                patient.Nosology);
            ChangeHospitalization(typeOfObject, hospitalization, value, messagePart);
            _ownHospitalizationWorker.Update(hospitalization, patient);
        }

        public void ChangeHospitalizationPrameterInForeignDatabase(ObjectType typeOfObject, int foreignPatientId, int foreignHospitalizationId, string value)
        {
            CPatient patient = _foreignPatientWorker.GetById(foreignPatientId);
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            string messagePart = string.Format(
                " для госпитализации за '{0}' для пациента '{1}' с нозологией '{2}' успешно изменена во внешней базе.",
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true),
                patient.GetFullName(),
                patient.Nosology);
            ChangeHospitalization(typeOfObject, hospitalization, value, messagePart);
            _foreignHospitalizationWorker.Update(hospitalization, patient);
        }
        #endregion


        #region Изменение параметров у выписного эпикриза
        private void ChangeDischargeEpicrisis(ObjectType typeOfObject, CDischargeEpicrisis dischargeEpicrisis, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.DischargeEpicrisisAdditionalAnalises:
                    dischargeEpicrisis.AdditionalAnalises = value;
                    _mergeForm.SetStatus("Общий анализ мочи, другие анализы" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisAfterOperation:
                    dischargeEpicrisis.AfterOperation = value;
                    _mergeForm.SetStatus("После операции" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisConservativeTherapy:
                    dischargeEpicrisis.ConservativeTherapy = value;
                    _mergeForm.SetStatus("Консервативное лечение" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisEkg:
                    dischargeEpicrisis.Ekg = value;
                    _mergeForm.SetStatus("ЭКГ пациента" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOakEritrocits:
                    dischargeEpicrisis.OakEritrocits = value;
                    _mergeForm.SetStatus("Общий анализ крови, эритроциты" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOakHb:
                    dischargeEpicrisis.OakHb = value;
                    _mergeForm.SetStatus("Общий анализ крови, Hb" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOakLekocits:
                    dischargeEpicrisis.OakLekocits = value;
                    _mergeForm.SetStatus("Общий анализ крови, лекоциты" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOakSoe:
                    dischargeEpicrisis.OakSoe = value;
                    _mergeForm.SetStatus("Общий анализ крови, СОЭ" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOamColor:
                    dischargeEpicrisis.OamColor = value;
                    _mergeForm.SetStatus("Общий анализ мочи, цвет" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOamDensity:
                    dischargeEpicrisis.OamDensity = value;
                    _mergeForm.SetStatus("Общий анализ мочи, относительная плотность" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOamEritrocits:
                    dischargeEpicrisis.OamEritrocits = value;
                    _mergeForm.SetStatus("Общий анализ мочи, эритроциты" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisOamLekocits:
                    dischargeEpicrisis.OamLekocits = value;
                    _mergeForm.SetStatus("Общий анализ мочи, лейкоциты" + messagePart);
                    break;
            }
        }

        private void ChangeDischargeEpicrisis(ObjectType typeOfObject, CDischargeEpicrisis dischargeEpicrisis, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.DischargeEpicrisisAdditionalRecomendations:
                    dischargeEpicrisis.AdditionalRecomendations = (List<string>)value;
                    _mergeForm.SetStatus("Дополнительные рекомендации при выписке" + messagePart);
                    break;
                case ObjectType.DischargeEpicrisisRecomendations:
                    dischargeEpicrisis.Recomendations = (List<string>)value;
                    _mergeForm.SetStatus("Рекомендации при выписке" + messagePart);
                    break;
            }
        }

        public void ChangeDischargeEpicrisisPrameterInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, object value)
        {
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            CPatient patient = _ownPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для акушерского анамнеза пациента '{0}' с нозологией '{1}' для госпитализации за '{2}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CDischargeEpicrisis dischargeEpicrisis = _ownDischargeEpicrisisWorker.GetByHospitalizationId(ownHospitalizationId);
            if (value is string)
            {
                ChangeDischargeEpicrisis(typeOfObject, dischargeEpicrisis, (string)value, messagePart);
            }
            else
            {
                ChangeDischargeEpicrisis(typeOfObject, dischargeEpicrisis, value, messagePart);
            }

            _ownDischargeEpicrisisWorker.Update(dischargeEpicrisis);
        }

        public void ChangeDischargeEpicrisisPrameterInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, object value)
        {
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            CPatient patient = _foreignPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для акушерского анамнеза пациента '{0}' с нозологией '{1}' для госпитализации за '{2}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CDischargeEpicrisis dischargeEpicrisis = _foreignDischargeEpicrisisWorker.GetByHospitalizationId(foreignHospitalizationId);
            if (value is string)
            {
                ChangeDischargeEpicrisis(typeOfObject, dischargeEpicrisis, (string)value, messagePart);
            }
            else
            {
                ChangeDischargeEpicrisis(typeOfObject, dischargeEpicrisis, value, messagePart);
            }

            _foreignDischargeEpicrisisWorker.Update(dischargeEpicrisis);
        }
        #endregion


        #region Изменение параметров у этапного эпикриза
        private void ChangeLineOfCommunicationEpicrisis(ObjectType typeOfObject, CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisis, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.LineOfCommunicationEpicrisisAdditionalInfo:
                    lineOfCommunicationEpicrisis.AdditionalInfo = value;
                    _mergeForm.SetStatus("Дополнительная информация" + messagePart);
                    break;
                case ObjectType.LineOfCommunicationEpicrisisPlan:
                    lineOfCommunicationEpicrisis.Plan = value;
                    _mergeForm.SetStatus("Планирующиеся действия" + messagePart);
                    break;
                case ObjectType.LineOfCommunicationEpicrisisWritingDate:
                    lineOfCommunicationEpicrisis.WritingDate = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Дата написания документа" + messagePart);
                    break;
            }
        }

        public void ChangeLineOfCommunicationEpicrisisPrameterInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, string value)
        {
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            CPatient patient = _ownPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для этапного эпикриза пациента '{0}' с нозологией '{1}' для госпитализации '{2}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisis = _ownLineOfCommunicationEpicrisisWorker.GetByHospitalizationId(ownHospitalizationId);
            ChangeLineOfCommunicationEpicrisis(typeOfObject, lineOfCommunicationEpicrisis, value, messagePart);
            _ownLineOfCommunicationEpicrisisWorker.Update(lineOfCommunicationEpicrisis);
        }

        public void ChangeLineOfCommunicationEpicrisisPrameterInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, string value)
        {
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            CPatient patient = _foreignPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для этапного эпикриза пациента '{0}' с нозологией '{1}' для госпитализации '{2}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisis = _foreignLineOfCommunicationEpicrisisWorker.GetByHospitalizationId(foreignHospitalizationId);
            ChangeLineOfCommunicationEpicrisis(typeOfObject, lineOfCommunicationEpicrisis, value, messagePart);
            _foreignLineOfCommunicationEpicrisisWorker.Update(lineOfCommunicationEpicrisis);
        }
        #endregion


        #region Изменение параметров у переводного эпикриза
        private void ChangeTransferableEpicrisis(ObjectType typeOfObject, CTransferableEpicrisis transferableEpicrisis, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.TransferableEpicrisisAdditionalInfo:
                    transferableEpicrisis.AdditionalInfo = value;
                    _mergeForm.SetStatus("Дополнительная информация" + messagePart);
                    break;
                case ObjectType.TransferableEpicrisisAfterOperationPeriod:
                    transferableEpicrisis.AfterOperationPeriod = value;
                    _mergeForm.SetStatus("Послеоперационный период" + messagePart);
                    break;
                case ObjectType.TransferableEpicrisisDisabilityList:
                    transferableEpicrisis.DisabilityList = value;
                    _mergeForm.SetStatus("Личный номер" + messagePart);
                    break;
                case ObjectType.TransferableEpicrisisIsIncludeDisabilityList:
                    transferableEpicrisis.IsIncludeDisabilityList = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Информация о том, включать ли личный номер в отчёт" + messagePart);
                    break;
                case ObjectType.TransferableEpicrisisPlan:
                    transferableEpicrisis.Plan = value;
                    _mergeForm.SetStatus("Планирующиеся действия" + messagePart);
                    break;
                case ObjectType.TransferableEpicrisisWritingDate:
                    transferableEpicrisis.WritingDate = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Дата написания документа" + messagePart);
                    break;
            }
        }

        public void ChangeTransferableEpicrisisPrameterInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, string value)
        {
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            CPatient patient = _ownPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для переводного эпикриза пациента '{0}' с нозологией '{1}' для госпитализации '{2}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CTransferableEpicrisis transferableEpicrisis = _ownTransferableEpicrisisWorker.GetByHospitalizationId(ownHospitalizationId);
            ChangeTransferableEpicrisis(typeOfObject, transferableEpicrisis, value, messagePart);
            _ownTransferableEpicrisisWorker.Update(transferableEpicrisis);
        }

        public void ChangeTransferableEpicrisisPrameterInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, string value)
        {
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            CPatient patient = _foreignPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для переводного эпикриза пациента '{0}' с нозологией '{1}' для госпитализации '{2}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CTransferableEpicrisis transferableEpicrisis = _foreignTransferableEpicrisisWorker.GetByHospitalizationId(foreignHospitalizationId);
            ChangeTransferableEpicrisis(typeOfObject, transferableEpicrisis, value, messagePart);
            _foreignTransferableEpicrisisWorker.Update(transferableEpicrisis);
        }
        #endregion


        #region Изменение параметров у осмотра в отделении
        private void ChangeMedicalInspection(ObjectType typeOfObject, CMedicalInspection medicalInspection, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.MedicalInspectionAnamneseAnMorbi:
                    medicalInspection.AnamneseAnMorbi = value;
                    _mergeForm.SetStatus("Осмотр в отделении, анамнез, AnMorbi" + messagePart);
                    break;
                case ObjectType.MedicalInspectionComplaints:
                    medicalInspection.Complaints = value;
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, жалобы" + messagePart);
                    break;
                case ObjectType.MedicalInspectionExpertAnamnese:
                    medicalInspection.ExpertAnamnese = Convert.ToInt32(value);
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, 1, 2 или 3 лист нетрудоспособности" + messagePart);
                    break;
                case ObjectType.MedicalInspectionInspectionPlan:
                    medicalInspection.InspectionPlan = value;
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, обследование" + messagePart);
                    break;
                case ObjectType.MedicalInspectionIsAnamneseActive:
                    medicalInspection.IsAnamneseActive = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Включён ли анамнез в общий отчёт" + messagePart);
                    break;
                case ObjectType.MedicalInspectionIsPlanEnabled:
                    medicalInspection.IsPlanEnabled = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, включен ли план осмотра в отчёт" + messagePart);
                    break;
                case ObjectType.MedicalInspectionIsStLocalisPart1Enabled:
                    medicalInspection.IsStLocalisPart1Enabled = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Включён ли st.localis часть 1 в общий отчёт" + messagePart);
                    break;
                case ObjectType.MedicalInspectionIsStLocalisPart2Enabled:
                    medicalInspection.IsStLocalisPart2Enabled = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Включён ли st.localis часть 2 в общий отчёт" + messagePart);
                    break;
                case ObjectType.MedicalInspectionLnFirstDateStart:
                    medicalInspection.LnFirstDateStart = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, выдан первично с даты" + messagePart);
                    break;
                case ObjectType.MedicalInspectionLnNumber:
                    medicalInspection.LnNumber = value;
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, номер л/н" + messagePart);
                    break;
                case ObjectType.MedicalInspectionLnWithNumberDateEnd:
                    medicalInspection.LnWithNumberDateEnd = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, выдан амбулаторно до даты" + messagePart);
                    break;
                case ObjectType.MedicalInspectionLnWithNumberDateStart:
                    medicalInspection.LnWithNumberDateStart = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, выдан амбулаторно с даты" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisDescription:
                    medicalInspection.StLocalisDescription = value;
                    _mergeForm.SetStatus("Осмотр в отделении, описание St. localis-а" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart1OppositionFinger:
                    medicalInspection.StLocalisPart1OppositionFinger = value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part1, номер пальца в оппозиции" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart2NumericUpDown:
                    medicalInspection.StLocalisPart2NumericUpDown = Convert.ToInt32(value);
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part2, числовое поле" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart2WhichHand:
                    medicalInspection.StLocalisPart2WhichHand = value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part2, какая рука(левая/правая/правая, левая)" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisRentgen:
                    medicalInspection.StLocalisRentgen = value;
                    _mergeForm.SetStatus("Осмотр в отделении, тип рентгена" + messagePart);
                    break;
                case ObjectType.MedicalInspectionTeoRisk:
                    medicalInspection.TeoRisk = value;
                    _mergeForm.SetStatus("Осмотр в отделении, общие данные, риск ТЭО" + messagePart);
                    break;
            }
        }

        private void ChangeMedicalInspection(ObjectType typeOfObject, CMedicalInspection medicalInspection, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.MedicalInspectionAnamneseAnVitae:
                    medicalInspection.AnamneseAnVitae = (bool[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, анамнез, AnVitae" + messagePart);
                    break;
                case ObjectType.MedicalInspectionAnamneseCheckboxes:
                    medicalInspection.AnamneseCheckboxes = (bool[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, анамнез, checkbox-ы" + messagePart);
                    break;
                case ObjectType.MedicalInspectionAnamneseTextBoxes:
                    medicalInspection.AnamneseTextBoxes = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, анамнез, поля" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart1Fields:
                    medicalInspection.StLocalisPart1Fields = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part1, поля" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart2ComboBoxes:
                    medicalInspection.StLocalisPart2ComboBoxes = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part2, комбобоксы" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart2LeftHand:
                    medicalInspection.StLocalisPart2LeftHand = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part2, комбобоксы для левой руки" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart2RightHand:
                    medicalInspection.StLocalisPart2RightHand = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part2, комбобоксы для правой руки" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStLocalisPart2TextBoxes:
                    medicalInspection.StLocalisPart2TextBoxes = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.localis part2, текст боксы" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStPraesensComboBoxes:
                    medicalInspection.StPraesensComboBoxes = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.praesens, комбобоксы" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStPraesensNumericUpDowns:
                    medicalInspection.StPraesensNumericUpDowns = (int[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.praesens, числовые поля" + messagePart);
                    break;
                case ObjectType.MedicalInspectionStPraesensTextBoxes:
                    medicalInspection.StPraesensTextBoxes = (string[])value;
                    _mergeForm.SetStatus("Осмотр в отделении, st.praesens, текстовые поля" + messagePart);
                    break;
            }
        }

        public void ChangeMedicalInspectionPrameterInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, object value)
        {
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
            CPatient patient = _ownPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для осмотра в отделении пациента '{0}' с нозологией '{1}' для госпитализации за '{2}' успешно изменена в нашей базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CMedicalInspection medicalInspection = _ownMedicalInspectionWorker.GetByHospitalizationId(ownHospitalizationId);
            if (value is string)
            {
                ChangeMedicalInspection(typeOfObject, medicalInspection, (string)value, messagePart);
            }
            else
            {
                ChangeMedicalInspection(typeOfObject, medicalInspection, value, messagePart);
            }

            _ownMedicalInspectionWorker.Update(medicalInspection);
        }

        public void ChangeMedicalInspectionPrameterInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, object value)
        {
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
            CPatient patient = _foreignPatientWorker.GetById(hospitalization.PatientId);
            string messagePart = string.Format(
                " для осмотра в отделении пациента '{0}' с нозологией '{1}' для госпитализации за '{2}' успешно изменена во внешней базе.",
                patient.GetFullName(),
                patient.Nosology,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true));

            CMedicalInspection medicalInspection = _foreignMedicalInspectionWorker.GetByHospitalizationId(foreignHospitalizationId);
            if (value is string)
            {
                ChangeMedicalInspection(typeOfObject, medicalInspection, (string)value, messagePart);
            }
            else
            {
                ChangeMedicalInspection(typeOfObject, medicalInspection, value, messagePart);
            }

            _foreignMedicalInspectionWorker.Update(medicalInspection);
        }
        #endregion


        #region Изменение параметров у операции
        private void ChangeOperation(ObjectType typeOfObject, COperation operation, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.OperationDateOfOperation:
                    operation.DateOfOperation = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Дата операции" + messagePart);
                    break;
                case ObjectType.OperationEndTimeOfOperation:
                    if (value == "Нет значения")
                    {
                        operation.EndTimeOfOperation = null;
                    }
                    else
                    {
                        operation.EndTimeOfOperation = CConvertEngine.StringToDateTime(value);
                    }

                    operation.EndTimeOfOperation = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Время окончания операции" + messagePart);
                    break;
                case ObjectType.OperationHeAnaesthetist:
                    operation.HeAnaesthetist = value;
                    _mergeForm.SetStatus("Анестезист" + messagePart);
                    break;
                case ObjectType.OperationName:
                    operation.Name = value;
                    _mergeForm.SetStatus("Название" + messagePart);
                    break;
                case ObjectType.OperationOrderly:
                    operation.Orderly = value;
                    _mergeForm.SetStatus("Санитар" + messagePart);
                    break;
                case ObjectType.OperationScrubNurse:
                    operation.ScrubNurse = value;
                    _mergeForm.SetStatus("Операционная мед. сестра" + messagePart);
                    break;
                case ObjectType.OperationSheAnaesthetist:
                    operation.SheAnaesthetist = value;
                    _mergeForm.SetStatus("Анестезистка" + messagePart);
                    break;
                case ObjectType.OperationStartTimeOfOperation:
                    operation.StartTimeOfOperation = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Время начала операции" + messagePart);
                    break;
            }
        }

        private void ChangeOperation(ObjectType typeOfObject, COperation operation, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.OperationAssistents:
                    operation.Assistents = (List<string>)value;
                    _mergeForm.SetStatus("Список ассистентов" + messagePart);
                    break;
                case ObjectType.OperationSurgeons:
                    operation.Surgeons = (List<string>)value;
                    _mergeForm.SetStatus("Список хирургов" + messagePart);
                    break;
                case ObjectType.OperationTypes:
                    operation.OperationTypes = (List<string>)value;
                    _mergeForm.SetStatus("Список типов" + messagePart);
                    break;
            }
        }

        public void ChangeOperationParameterInOwnDatabase(ObjectType typeOfObject, int ownOperationId, object value)
        {
            COperation operation = _ownOperationWorker.GetById(ownOperationId);
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(operation.HospitalizationId);
            CPatient patient = _ownPatientWorker.GetById(operation.PatientId);
            string messagePart = string.Format(
                " для операции '{0}' во время госпитализации за '{1}' для пациента '{2}' с нозологией '{3}' успешно изменена в нашей базе.",
                operation.Name,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true),
                patient.GetFullName(),
                patient.Nosology);

            if (value is string)
            {
                ChangeOperation(typeOfObject, operation, (string)value, messagePart);
            }
            else
            {
                ChangeOperation(typeOfObject, operation, value, messagePart);
            }

            _ownOperationWorker.Update(operation);
        }

        public void ChangeOperationParameterInForeignDatabase(ObjectType typeOfObject, int foreignOperationId, object value)
        {
            COperation operation = _foreignOperationWorker.GetById(foreignOperationId);
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(operation.HospitalizationId);
            CPatient patient = _foreignPatientWorker.GetById(operation.PatientId);
            string messagePart = string.Format(
                " для операции '{0}' во время госпитализации за '{1}' для пациента '{2}' с нозологией '{3}' успешно изменена во внешней базе.",
                operation.Name,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true),
                patient.GetFullName(),
                patient.Nosology);

            if (value is string)
            {
                ChangeOperation(typeOfObject, operation, (string)value, messagePart);
            }
            else
            {
                ChangeOperation(typeOfObject, operation, value, messagePart);
            }

            _foreignOperationWorker.Update(operation);
        }
        #endregion


        #region Изменение параметров у протокола операции
        private void ChangeOperationProtocol(ObjectType typeOfObject, COperationProtocol operationProtocol, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.OperationProtocolADFirst:
                    operationProtocol.ADFirst = Convert.ToInt32(value);
                    _mergeForm.SetStatus("Первое значение AD" + messagePart);
                    break;
                case ObjectType.OperationProtocolADSecond:
                    operationProtocol.ADSecond = Convert.ToInt32(value);
                    _mergeForm.SetStatus("Второе значение AD" + messagePart);
                    break;
                case ObjectType.OperationProtocolBreath:
                    operationProtocol.Breath = value;
                    _mergeForm.SetStatus("Дыхание" + messagePart);
                    break;
                case ObjectType.OperationProtocolChDD:
                    operationProtocol.ChDD = Convert.ToInt32(value);
                    _mergeForm.SetStatus("ЧДД" + messagePart);
                    break;
                case ObjectType.OperationProtocolComplaints:
                    operationProtocol.Complaints = value;
                    _mergeForm.SetStatus("Жалобы" + messagePart);
                    break;
                case ObjectType.OperationProtocolHeartRhythm:
                    operationProtocol.HeartRhythm = value;
                    _mergeForm.SetStatus("Ритм сердца" + messagePart);
                    break;
                case ObjectType.OperationProtocolHeartSounds:
                    operationProtocol.HeartSounds = value;
                    _mergeForm.SetStatus("Тоны сердца" + messagePart);
                    break;
                case ObjectType.OperationProtocolIsDairyEnabled:
                    operationProtocol.IsDairyEnabled = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Значение того, активен ли дневник" + messagePart);
                    break;
                case ObjectType.OperationProtocolIsTreatmentPlanActiveInOperationProtocol:
                    operationProtocol.IsTreatmentPlanActiveInOperationProtocol = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Значение того, активен ли план обследования" + messagePart);
                    break;
                case ObjectType.OperationProtocolOperationCourse:
                    operationProtocol.OperationCourse = value;
                    _mergeForm.SetStatus("Ход операции" + messagePart);
                    break;
                case ObjectType.OperationProtocolPulse:
                    operationProtocol.Pulse = Convert.ToInt32(value);
                    _mergeForm.SetStatus("Пульс" + messagePart);
                    break;
                case ObjectType.OperationProtocolState:
                    operationProtocol.State = value;
                    _mergeForm.SetStatus("Состояние пациента" + messagePart);
                    break;
                case ObjectType.OperationProtocolStLocalis:
                    operationProtocol.StLocalis = value;
                    _mergeForm.SetStatus("St. Localis" + messagePart);
                    break;
                case ObjectType.OperationProtocolStomach:
                    operationProtocol.Stomach = value;
                    _mergeForm.SetStatus("Информация о животе" + messagePart);
                    break;
                case ObjectType.OperationProtocolStool:
                    operationProtocol.Stool = value;
                    _mergeForm.SetStatus("Информация о стуле" + messagePart);
                    break;
                case ObjectType.OperationProtocolTemperature:
                    operationProtocol.Temperature = value;
                    _mergeForm.SetStatus("Температура" + messagePart);
                    break;
                case ObjectType.OperationProtocolTimeWriting:
                    operationProtocol.TimeWriting = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Время написания эпикриза" + messagePart);
                    break;
                case ObjectType.OperationProtocolTreatmentPlanDate:
                    operationProtocol.TreatmentPlanDate = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Дата написания плана обследования" + messagePart);
                    break;
                case ObjectType.OperationProtocolTreatmentPlanInspection:
                    operationProtocol.TreatmentPlanInspection = value;
                    _mergeForm.SetStatus("План обследования" + messagePart);
                    break;
                case ObjectType.OperationProtocolUrination:
                    operationProtocol.Urination = value;
                    _mergeForm.SetStatus("Информация о мочеиспускании" + messagePart);
                    break;
                case ObjectType.OperationProtocolWheeze:
                    operationProtocol.Wheeze = value;
                    _mergeForm.SetStatus("Информация о хрипах" + messagePart);
                    break;
            }
        }

        public void ChangeOperationProtocolParameterInOwnDatabase(ObjectType typeOfObject, int ownOperationId, string value)
        {
            COperation operation = _ownOperationWorker.GetById(ownOperationId);
            CHospitalization hospitalization = _ownHospitalizationWorker.GetById(operation.HospitalizationId);
            CPatient patient = _ownPatientWorker.GetById(operation.PatientId);
            string messagePart = string.Format(
                " для протокола операции '{0}' во время госпитализации за '{1}' для пациента '{2}' с нозологией '{3}' успешно изменена в нашей базе.",
                operation.Name,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true),
                patient.GetFullName(),
                patient.Nosology);

            COperationProtocol operationProtocol = _ownOperationProtocolWorker.GetByOperationId(ownOperationId);
            ChangeOperationProtocol(typeOfObject, operationProtocol, value, messagePart);
            _ownOperationProtocolWorker.Update(operationProtocol);
        }

        public void ChangeOperationProtocolParameterInForeignDatabase(ObjectType typeOfObject, int foreignOperationId, string value)
        {
            COperation operation = _foreignOperationWorker.GetById(foreignOperationId);
            CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(operation.HospitalizationId);
            CPatient patient = _foreignPatientWorker.GetById(operation.PatientId);
            string messagePart = string.Format(
                " для протокола операции '{0}' во время госпитализации за '{1}' для пациента '{2}' с нозологией '{3}' успешно изменена во внешней базе.",
                operation.Name,
                CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true),
                patient.GetFullName(),
                patient.Nosology);

            COperationProtocol operationProtocol = _foreignOperationProtocolWorker.GetByOperationId(foreignOperationId);
            ChangeOperationProtocol(typeOfObject, operationProtocol, value, messagePart);
            _foreignOperationProtocolWorker.Update(operationProtocol);
        }
        #endregion


        #region Изменение параметров у визита
        private void ChangeVisit(ObjectType typeOfObject, CVisit visit, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.VisitComments:
                    visit.Comments = value;
                    _mergeForm.SetStatus("Комментарии" + messagePart);
                    break;
                case ObjectType.VisitDiagnose:
                    visit.Diagnose = value;
                    _mergeForm.SetStatus("Диагноз" + messagePart);
                    break;
                case ObjectType.VisitDoctor:
                    visit.Doctor = value;
                    _mergeForm.SetStatus("Информация о враче" + messagePart);
                    break;
                case ObjectType.VisitEvenly:
                    visit.Evenly = value;
                    _mergeForm.SetStatus("Информация в поле 'Объективно'" + messagePart);
                    break;
                case ObjectType.VisitRecommendation:
                    visit.Recommendation = value;
                    _mergeForm.SetStatus("Описание" + messagePart);
                    break;
            }
        }

        public void ChangeVisitPrameterInOwnDatabase(ObjectType typeOfObject, int ownPatientId, int ownVisitId, string value)
        {
            CPatient patient = _ownPatientWorker.GetById(ownPatientId);
            CVisit visit = _ownVisitWorker.GetById(ownVisitId);
            string messagePart = string.Format(
                " для консультации за '{0}' для пациента '{1}' с нозологией '{2}' успешно изменена в нашей базе.",
                CConvertEngine.DateTimeToString(visit.VisitDate, true),
                patient.GetFullName(),
                patient.Nosology);
            ChangeVisit(typeOfObject, visit, value, messagePart);
            _ownVisitWorker.Update(visit, patient);
        }


        public void ChangeVisitPrameterInForeignDatabase(ObjectType typeOfObject, int foreignPatientId, int foreignVisitId, string value)
        {
            CPatient patient = _foreignPatientWorker.GetById(foreignPatientId);
            CVisit visit = _foreignVisitWorker.GetById(foreignVisitId);
            string messagePart = string.Format(
                " для консультации за '{0}' для пациента '{1}' с нозологией '{2}' успешно изменена во внешней базе.",
                CConvertEngine.DateTimeToString(visit.VisitDate, true),
                patient.GetFullName(),
                patient.Nosology);
            ChangeVisit(typeOfObject, visit, value, messagePart);
            _foreignVisitWorker.Update(visit, patient);
        }
        #endregion


        #region Изменение параметров у карты на плечевое сплетение
        private void ChangeBrachialPlexusCard(ObjectType typeOfObject, CBrachialPlexusCard brachialPlexusCard, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.BrachialPlexusCardSideOfCard:
                    brachialPlexusCard.SideOfCard = (CardSide)Enum.Parse(typeof(CardSide), value);
                    _mergeForm.SetStatus("Информация о стороне, к которой относится карта," + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardVascularStatus:
                    brachialPlexusCard.VascularStatus = value;
                    _mergeForm.SetStatus("Информация о сосудистом статусе" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardDiaphragm:
                    brachialPlexusCard.Diaphragm = value;
                    _mergeForm.SetStatus("Информация о диафрагме" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardHornersSyndrome:
                    brachialPlexusCard.HornersSyndrome = value;
                    _mergeForm.SetStatus("Информация о синдроме Горнера" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardTinelsSymptom:
                    brachialPlexusCard.TinelsSymptom = value;
                    _mergeForm.SetStatus("Информация о симптоме Тиннеля" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardIsMyelographyEnabled:
                    brachialPlexusCard.IsMyelographyEnabled = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Информация о том, включена ли миелография" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardMyelographyType:
                    brachialPlexusCard.MyelographyType = value;
                    _mergeForm.SetStatus("Информация о типе миелографии" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardMyelographyDate:
                    brachialPlexusCard.MyelographyDate = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Информация о дате миелографии" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardMyelography:
                    brachialPlexusCard.Myelography = value;
                    _mergeForm.SetStatus("Информация о миелографии" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardIsEMNGEnabled:
                    brachialPlexusCard.IsEMNGEnabled = Convert.ToBoolean(value);
                    _mergeForm.SetStatus("Информация о том, включено ли ЭМНГ" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardEMNGDate:
                    brachialPlexusCard.EMNGDate = CConvertEngine.StringToDateTime(value);
                    _mergeForm.SetStatus("Информация о дате ЭМНГ" + messagePart);
                    break;
                case ObjectType.BrachialPlexusCardEMNG:
                    brachialPlexusCard.EMNG = value;
                    _mergeForm.SetStatus("Информация о ЭМНГ" + messagePart);
                    break;
            }
        }

        private void ChangeBrachialPlexusCard(ObjectType typeOfObject, CBrachialPlexusCard brachialPlexusCard, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.BrachialPlexusCardPicture:
                    brachialPlexusCard.Picture = (Bitmap)value;
                    _mergeForm.SetStatus("Изображение" + messagePart);
                    break;                
            }
        }

        public void ChangeBrachialPlexusCardInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, int ownVisitId, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (ownHospitalizationId > -1)
            {
                CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _ownVisitWorker.GetById(ownVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _ownPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на плечевое сплетение для {0} для пациента '{1}' с нозологией '{2}' успешно изменена в нашей базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CBrachialPlexusCard brachialPlexusCard = _ownBrachialPlexusCardWorker.GetByHospitalizationAndVisitId(ownHospitalizationId, ownVisitId);
            if (value is string)
            {
                ChangeBrachialPlexusCard(typeOfObject, brachialPlexusCard, (string)value, messagePart);
            }
            else
            {
                ChangeBrachialPlexusCard(typeOfObject, brachialPlexusCard, value, messagePart);
            }

            _ownBrachialPlexusCardWorker.Update(brachialPlexusCard);
        }

        public void ChangeBrachialPlexusCardInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, int foreignVisitId, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (foreignHospitalizationId > -1)
            {
                CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _foreignVisitWorker.GetById(foreignVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _foreignPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на плечевое сплетение для {0} для пациента '{1}' с нозологией '{2}' успешно изменена во внешней базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CBrachialPlexusCard brachialPlexusCard = _foreignBrachialPlexusCardWorker.GetByHospitalizationAndVisitId(foreignHospitalizationId, foreignVisitId);
            if (value is string)
            {
                ChangeBrachialPlexusCard(typeOfObject, brachialPlexusCard, (string)value, messagePart);
            }
            else
            {
                ChangeBrachialPlexusCard(typeOfObject, brachialPlexusCard, value, messagePart);
            }

            _foreignBrachialPlexusCardWorker.Update(brachialPlexusCard);
        }
        #endregion


        #region Изменение параметров у карты на акушерский паралич
        private void ChangeObstetricParalysisCard(ObjectType typeOfObject, CObstetricParalysisCard obstetricParalysisCard, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.ObstetricParalysisCardSideOfCard:
                    obstetricParalysisCard.SideOfCard = (CardSide)Enum.Parse(typeof(CardSide), value);
                    _mergeForm.SetStatus("Информация о стороне, к которой относится карта," + messagePart);
                    break;
            }
        }

        private void ChangeObstetricParalysisCard(ObjectType typeOfObject, CObstetricParalysisCard obstetricParalysisCard, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.ObstetricParalysisCardComboBoxes:
                    obstetricParalysisCard.ComboBoxes = (string[])value;
                    _mergeForm.SetStatus("Список комбобоксов" + messagePart);
                    break;

                case ObjectType.ObstetricParalysisCardGlobalAbduction:
                    obstetricParalysisCard.GlobalAbductionPicturesSelection = (bool[])value;
                    _mergeForm.SetStatus("Информация о Global Abduction" + messagePart);
                    break;
                case ObjectType.ObstetricParalysisCardGlobalExternalRotation:
                    obstetricParalysisCard.GlobalExternalRotationPicturesSelection = (bool[])value;
                    _mergeForm.SetStatus("Информация о Global External Rotation" + messagePart);
                    break;
                case ObjectType.ObstetricParalysisCardHandToMouth:
                    obstetricParalysisCard.HandToMouthPicturesSelection = (bool[])value;
                    _mergeForm.SetStatus("Информация о Hand To Mouth" + messagePart);
                    break;
                case ObjectType.ObstetricParalysisCardHandToNeck:
                    obstetricParalysisCard.HandToNeckPicturesSelection = (bool[])value;
                    _mergeForm.SetStatus("Информация о Hand To Neck" + messagePart);
                    break;
                case ObjectType.ObstetricParalysisCardHandToSpine:
                    obstetricParalysisCard.HandToSpinePicturesSelection = (bool[])value;
                    _mergeForm.SetStatus("Информация о Hand To Spine" + messagePart);
                    break;
            }
        }

        public void ChangeObstetricParalysisCardInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, int ownVisitId, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (ownHospitalizationId > -1)
            {
                CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _ownVisitWorker.GetById(ownVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _ownPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на акушерский паралич для {0} для пациента '{1}' с нозологией '{2}' успешно изменена в нашей базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CObstetricParalysisCard obstetricParalysisCard = _ownObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(ownHospitalizationId, ownVisitId);
            if (value is string)
            {
                ChangeObstetricParalysisCard(typeOfObject, obstetricParalysisCard, (string)value, messagePart);
            }
            else
            {
                ChangeObstetricParalysisCard(typeOfObject, obstetricParalysisCard, value, messagePart);
            }

            _ownObstetricParalysisCardWorker.Update(obstetricParalysisCard);
        }

        public void ChangeObstetricParalysisCardInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, int foreignVisitId, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (foreignHospitalizationId > -1)
            {
                CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _foreignVisitWorker.GetById(foreignVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _foreignPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на акушерский паралич для {0} для пациента '{1}' с нозологией '{2}' успешно изменена во внешней базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CObstetricParalysisCard obstetricParalysisCard = _foreignObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(foreignHospitalizationId, foreignVisitId);
            if (value is string)
            {
                ChangeObstetricParalysisCard(typeOfObject, obstetricParalysisCard, (string)value, messagePart);
            }
            else
            {
                ChangeObstetricParalysisCard(typeOfObject, obstetricParalysisCard, value, messagePart);
            }

            _foreignObstetricParalysisCardWorker.Update(obstetricParalysisCard);
        }
        #endregion


        #region Изменение параметров у карты на объём движений
        private void ChangeRangeOfMotionCard(ObjectType typeOfObject, CRangeOfMotionCard rangeOfMotionCard, string value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.RangeOfMotionCardOppositionFinger:
                    rangeOfMotionCard.OppositionFinger = value;
                    _mergeForm.SetStatus("Информация о пальце в оппозиции" + messagePart);
                    break;
            }
        }

        private void ChangeRangeOfMotionCard(ObjectType typeOfObject, CRangeOfMotionCard rangeOfMotionCard, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.RangeOfMotionCardFields:
                    rangeOfMotionCard.Fields = (string[])value;
                    _mergeForm.SetStatus("Список полей" + messagePart);
                    break;
            }
        }

        public void ChangeRangeOfMotionCardInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, int ownVisitId, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (ownHospitalizationId > -1)
            {
                CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _ownVisitWorker.GetById(ownVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _ownPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на объём движений для {0} для пациента '{1}' с нозологией '{2}' успешно изменена в нашей базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CRangeOfMotionCard rangeOfMotionCard = _ownRangeOfMotionCardWorker.GetByHospitalizationAndVisitId(ownHospitalizationId, ownVisitId);
            if (value is string)
            {
                ChangeRangeOfMotionCard(typeOfObject, rangeOfMotionCard, (string)value, messagePart);
            }
            else
            {
                ChangeRangeOfMotionCard(typeOfObject, rangeOfMotionCard, value, messagePart);
            }

            _ownRangeOfMotionCardWorker.Update(rangeOfMotionCard);
        }

        public void ChangeRangeOfMotionCardInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, int foreignVisitId, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (foreignHospitalizationId > -1)
            {
                CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _foreignVisitWorker.GetById(foreignVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _foreignPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на объём движений для {0} для пациента '{1}' с нозологией '{2}' успешно изменена во внешней базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CRangeOfMotionCard rangeOfMotionCard = _foreignRangeOfMotionCardWorker.GetByHospitalizationAndVisitId(foreignHospitalizationId, foreignVisitId);
            if (value is string)
            {
                ChangeRangeOfMotionCard(typeOfObject, rangeOfMotionCard, (string)value, messagePart);
            }
            else
            {
                ChangeRangeOfMotionCard(typeOfObject, rangeOfMotionCard, value, messagePart);
            }

            _foreignRangeOfMotionCardWorker.Update(rangeOfMotionCard);
        }
        #endregion


        #region Изменение параметров у карты на объём движений
        private void ChangeCard(ObjectType typeOfObject, CCard card, object value, string messagePart)
        {
            switch (typeOfObject)
            {
                case ObjectType.LeftRightCardPicture:
                    card.Picture = (Bitmap)value;
                    _mergeForm.SetStatus("Изображение" + messagePart);
                    break;
            }
        }

        public void ChangeLeftRightCardInOwnDatabase(ObjectType typeOfObject, int ownHospitalizationId, int ownVisitId, CardSide ownSideOfCard, CardType ownTypeOfCard, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (ownHospitalizationId > -1)
            {
                CHospitalization hospitalization = _ownHospitalizationWorker.GetById(ownHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _ownVisitWorker.GetById(ownVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _ownPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на объём движений для {0} для пациента '{1}' с нозологией '{2}' успешно изменена в нашей базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CCard card = _ownCardWorker.GetByGeneralData(ownHospitalizationId, ownVisitId, ownSideOfCard, ownTypeOfCard);
            ChangeCard(typeOfObject, card, value, messagePart);

            _ownCardWorker.Update(card);
        }

        public void ChangeLeftRightCardInForeignDatabase(ObjectType typeOfObject, int foreignHospitalizationId, int foreignVisitId, CardSide foreignSideOfCard, CardType foreignTypeOfCard, object value)
        {
            string dateOfHospitalizationOrVisitInfo;
            int patientId;
            if (foreignHospitalizationId > -1)
            {
                CHospitalization hospitalization = _foreignHospitalizationWorker.GetById(foreignHospitalizationId);
                patientId = hospitalization.PatientId;
                dateOfHospitalizationOrVisitInfo = "госпитализации за '" + CConvertEngine.DateTimeToString(hospitalization.ReleaseDate) + "'";
            }
            else
            {
                CVisit visit = _foreignVisitWorker.GetById(foreignVisitId);
                patientId = visit.PatientId;
                dateOfHospitalizationOrVisitInfo = "визита за '" + CConvertEngine.DateTimeToString(visit.VisitDate, true) + "'";
            }

            CPatient patient = _foreignPatientWorker.GetById(patientId);
            string messagePart = string.Format(
                " для карты на объём движений для {0} для пациента '{1}' с нозологией '{2}' успешно изменена во внешней базе.",
                dateOfHospitalizationOrVisitInfo,
                patient.GetFullName(),
                patient.Nosology);

            CCard card = _foreignCardWorker.GetByGeneralData(foreignHospitalizationId, foreignVisitId, foreignSideOfCard, foreignTypeOfCard);
            ChangeCard(typeOfObject, card, value, messagePart);

            _foreignCardWorker.Update(card);
        }
        #endregion
        #endregion        
    }
}
