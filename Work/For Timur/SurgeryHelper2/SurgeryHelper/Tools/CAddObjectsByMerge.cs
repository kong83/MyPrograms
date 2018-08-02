using System;
using System.Collections.Generic;
using SurgeryHelper.Workers;
using SurgeryHelper.Essences;
using SurgeryHelper.Forms;
using System.IO;

namespace SurgeryHelper.Tools
{
    public static class CAddObjectsByMerge
    {
        /// <summary>
        /// Добавить пациента из одной базы в другую
        /// </summary>
        /// <param name="mergeForm">Указатель на MergeForm</param>
        /// <param name="to">Куда будет вставлять пациент (own или foreign)</param>
        /// <param name="nosologyChangesForDB">Список замен для нозологий</param>
        /// <param name="toNosologyWorker">Класс для работы с нозологиями в базе, куда копируется пациент</param>
        /// <param name="fromWorkersKeeper">Класс для хранения классов для работы с данными в базе, откуда копируется пациент</param>
        /// <param name="toWorkersKeeper">Класс для хранения классов для работы с данными в базе, куда копируется пациент</param>
        /// <param name="patientInfo">Данные копируемого пациента</param>
        /// <param name="fromPatientWorker">Класс для работы с пациентами в базе, откуда копируется пациент</param>
        /// <param name="toPatientWorker">Класс для работы с пациентами в базе, куда копируется пациент</param>
        /// <param name="fromAnamneseWorker">Класс для работы с анамнезами в базе, откуда копируется пациент</param>
        /// <param name="toAnamneseWorker">Класс для работы с анамнезами в базе, куда копируется пациент</param>
        /// <param name="fromObstetricHistoryWorker">Класс для работы с акушерскими анамнезами в базе, откуда копируется пациент</param>
        /// <param name="toObstetricHistoryWorker">Класс для работы с акушерскими анамнезами в базе, куда копируется пациент</param>
        /// <param name="fromHospitalizationWorker">Класс для работы с госпитализациями в базе, откуда копируется пациент</param>
        /// <param name="toHospitalizationWorker">Класс для работы с госпитализациями в базе, куда копируется пациент</param>
        /// <param name="fromMedicalInspectionWorker">Класс для работы с осмотрами в отделении в базе, откуда копируется пациент</param>
        /// <param name="toMedicalInspectionWorker">Класс для работы с осмотрами в отделении в базе, куда копируется пациент</param>
        /// <param name="fromDischargeEpicrisisWorker">Класс для работы с выписными эпикризами в базе, откуда копируется пациент</param>
        /// <param name="toDischargeEpicrisisWorker">Класс для работы с выписными эпикризами в базе, куда копируется пациент</param>
        /// <param name="fromLineOfCommunicationEpicrisisWorker">Класс для работы с этапными эпикризами в базе, откуда копируется пациент</param>
        /// <param name="toLineOfCommunicationEpicrisisWorker">Класс для работы с этапными эпикризами в базе, куда копируется пациент</param>
        /// <param name="fromTransferableEpicrisisWorker">Класс для работы с переводными эпикризами в базе, откуда копируется пациент</param>
        /// <param name="toTransferableEpicrisisWorker">Класс для работы с переводными эпикризами в базе, куда копируется пациент</param>
        /// <param name="fromOperationWorker">Класс для работы с операциями в базе, откуда копируется пациент</param>
        /// <param name="toOperationWorker">Класс для работы с операциями в базе, куда копируется пациент</param>
        /// <param name="fromOperationProtocolWorker">Класс для работы с операционными протоколами в базе, откуда копируется пациент</param>
        /// <param name="toOperationProtocolWorker">Класс для работы с операционными протоколами в базе, куда копируется пациент</param>
        /// <param name="fromVisitWorker">Класс для работы с консультациями в базе, откуда копируется пациент</param>
        /// <param name="toVisitWorker">Класс для работы с консультациями в базе, куда копируется пациент</param>
        /// <param name="fromBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, откуда копируется пациент</param>
        /// <param name="toBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, куда копируется пациент</param>
        /// <param name="fromObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, откуда копируется пациент</param>
        /// <param name="toObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, куда копируется пациент</param>
        /// <param name="fromRangeOfMotionCardWorker">Класс для работы с картами на объём движений в базе, откуда копируется пациент</param>
        /// <param name="toRangeOfMotionCardWorker">Класс для работы с картами на объём движений в базе, куда копируется пациент</param>
        /// <param name="fromCardWorker">Класс для работы с картами из двух частей в базе, откуда копируется пациент</param>
        /// <param name="toCardWorker">Класс для работы с картами из двух частей в базе, куда копируется пациент</param>
        public static void AddPatient(
            MergeForm mergeForm,
            string to,
            Dictionary<string, string> nosologyChangesForDB,
            CNosologyWorker toNosologyWorker,
            CWorkersKeeper fromWorkersKeeper,
            CWorkersKeeper toWorkersKeeper,
            CPatient patientInfo,
            CPatientWorker fromPatientWorker,
            CPatientWorker toPatientWorker,
            CAnamneseWorker fromAnamneseWorker,
            CAnamneseWorker toAnamneseWorker,
            CObstetricHistoryWorker fromObstetricHistoryWorker,
            CObstetricHistoryWorker toObstetricHistoryWorker,
            CHospitalizationWorker fromHospitalizationWorker,
            CHospitalizationWorker toHospitalizationWorker,
            CMedicalInspectionWorker fromMedicalInspectionWorker,
            CMedicalInspectionWorker toMedicalInspectionWorker,
            CDischargeEpicrisisWorker fromDischargeEpicrisisWorker,
            CDischargeEpicrisisWorker toDischargeEpicrisisWorker,
            CLineOfCommunicationEpicrisisWorker fromLineOfCommunicationEpicrisisWorker,
            CLineOfCommunicationEpicrisisWorker toLineOfCommunicationEpicrisisWorker,
            CTransferableEpicrisisWorker fromTransferableEpicrisisWorker,
            CTransferableEpicrisisWorker toTransferableEpicrisisWorker,
            COperationWorker fromOperationWorker,
            COperationWorker toOperationWorker,
            COperationProtocolWorker fromOperationProtocolWorker,
            COperationProtocolWorker toOperationProtocolWorker,
            CVisitWorker fromVisitWorker,
            CVisitWorker toVisitWorker,
            CBrachialPlexusCardWorker fromBrachialPlexusCardWorker,
            CBrachialPlexusCardWorker toBrachialPlexusCardWorker,
            CObstetricParalysisCardWorker fromObstetricParalysisCardWorker,
            CObstetricParalysisCardWorker toObstetricParalysisCardWorker,
            CRangeOfMotionCardWorker fromRangeOfMotionCardWorker,
            CRangeOfMotionCardWorker toRangeOfMotionCardWorker,
            CCardWorker fromCardWorker,
            CCardWorker toCardWorker)
        {
            // Добавляем пациента
            var newPatient = new CPatient(patientInfo)
            {
                Id = toPatientWorker.GetNewID()
            };

            // Решаем конфликт с нозологией, если в базе нету нозологии, указанной у пациента
            if (toNosologyWorker.GetByGeneralData(patientInfo.Nosology) == null)
            {
                if (nosologyChangesForDB.ContainsKey(patientInfo.Nosology))
                {
                    newPatient.Nosology = nosologyChangesForDB[patientInfo.Nosology];
                    mergeForm.SetStatus("Прописываем нозологию '" + nosologyChangesForDB[patientInfo.Nosology] + "' вместо нозологии '" + patientInfo.Nosology + "'");
                }
                else
                {
                    new AddNosologyByMergeForm(toNosologyWorker, nosologyChangesForDB, newPatient, to, mergeForm).ShowDialog();
                }
            }

            toPatientWorker.Update(newPatient);

            // Создание личной папки, если она указана
            if (newPatient.PrivateFolder.Length > 0)
            {
                if (!(newPatient.PrivateFolder.Length > 1 && newPatient.PrivateFolder[1] == ':'))
                {
                    string fullPrivateFolderPathTo = Path.Combine(toWorkersKeeper.ExecutableDirectoryPath, newPatient.PrivateFolder);
                    string fullPrivateFolderPathFrom = Path.Combine(fromWorkersKeeper.ExecutableDirectoryPath, newPatient.PrivateFolder);

                    CopyContent(mergeForm, fullPrivateFolderPathFrom, fullPrivateFolderPathTo);
                }
            }

            AddAnamnese(fromAnamneseWorker, toAnamneseWorker, patientInfo.Id, newPatient.Id);

            AddObstetricHistory(fromObstetricHistoryWorker, toObstetricHistoryWorker, patientInfo.Id, newPatient.Id);

            // Проходим по всем госпитализациям и добавляем все данные о госпитализациях,
            // операциях и всех эпикризах
            foreach (CHospitalization fromHospitalization in fromHospitalizationWorker.GetListByPatientId(patientInfo.Id))
            {
                AddHospitalization(
                    fromPatientWorker,
                    toPatientWorker,
                    fromHospitalizationWorker,
                    toHospitalizationWorker,
                    fromMedicalInspectionWorker,
                    toMedicalInspectionWorker,
                    fromDischargeEpicrisisWorker,
                    toDischargeEpicrisisWorker,
                    fromLineOfCommunicationEpicrisisWorker,
                    toLineOfCommunicationEpicrisisWorker,
                    fromTransferableEpicrisisWorker,
                    toTransferableEpicrisisWorker,
                    fromOperationWorker,
                    toOperationWorker,
                    fromOperationProtocolWorker,
                    toOperationProtocolWorker,
                    fromBrachialPlexusCardWorker,
                    toBrachialPlexusCardWorker,
                    fromObstetricParalysisCardWorker,
                    toObstetricParalysisCardWorker,
                    fromRangeOfMotionCardWorker,
                    toRangeOfMotionCardWorker,
                    fromCardWorker,
                    toCardWorker,
                    patientInfo.Id,
                    newPatient.Id,
                    fromHospitalization.Id);
            }

            // Проходим по всем консультациям и добавляем все данные о консультациях
            foreach (CVisit fromVisit in fromVisitWorker.GetListByPatientId(patientInfo.Id))
            {
                AddVisit(
                    fromPatientWorker,
                    toPatientWorker,
                    fromVisitWorker,
                    toVisitWorker,
                    fromBrachialPlexusCardWorker,
                    toBrachialPlexusCardWorker,
                    fromObstetricParalysisCardWorker,
                    toObstetricParalysisCardWorker,
                    fromRangeOfMotionCardWorker,
                    toRangeOfMotionCardWorker,
                    fromCardWorker,
                    toCardWorker,
                    patientInfo.Id,
                    newPatient.Id,
                    fromVisit.Id);
            }
        }


        /// <summary>
        /// Добавить консультацию и все вложенные объекты
        /// </summary>
        /// <param name="fromPatientWorker">Класс для работы с пациентами в базе, откуда копируется консультация</param>
        /// <param name="toPatientWorker">Класс для работы с пациентами в базе, куда копируется консультация</param>
        /// <param name="fromVisitWorker">Класс для работы с консультациями в базе, откуда копируется консультация</param>
        /// <param name="toVisitWorker">Класс для работы с консультациями в базе, куда копируется консультация</param>
        /// <param name="fromBrachialPlexusCardWorker"></param>
        /// <param name="toBrachialPlexusCardWorker"></param>
        /// <param name="fromObstetricParalysisCardWorker"></param>
        /// <param name="toObstetricParalysisCardWorker"></param>
        /// <param name="fromRangeOfMotionCardWorker"></param>
        /// <param name="toRangeOfMotionCardWorker"></param>
        /// <param name="fromCardWorker"></param>
        /// <param name="toCardWorker"></param>
        /// <param name="fromPatientId">id пациента из базы, откуда копируется консультация</param>
        /// <param name="toPaientId">id пациента из базы, куда копируется консультация</param>
        /// <param name="visitId">id консультации, который надо скопировать</param>
        public static void AddVisit(
            CPatientWorker fromPatientWorker,
            CPatientWorker toPatientWorker,
            CVisitWorker fromVisitWorker,
            CVisitWorker toVisitWorker,
            CBrachialPlexusCardWorker fromBrachialPlexusCardWorker,
            CBrachialPlexusCardWorker toBrachialPlexusCardWorker,
            CObstetricParalysisCardWorker fromObstetricParalysisCardWorker,
            CObstetricParalysisCardWorker toObstetricParalysisCardWorker,
            CRangeOfMotionCardWorker fromRangeOfMotionCardWorker,
            CRangeOfMotionCardWorker toRangeOfMotionCardWorker,
            CCardWorker fromCardWorker,
            CCardWorker toCardWorker,
            int fromPatientId,
            int toPaientId,
            int visitId)
        {
            CVisit fromVisit = fromVisitWorker.GetById(visitId);

            // Добавляем консультацию
            var newVisit = new CVisit(fromVisit)
            {
                Id = toVisitWorker.GetNewID(),
                PatientId = toPaientId
            };
            toVisitWorker.Update(newVisit, toPatientWorker.GetById(toPaientId));

            // Добавляем карты
            AddCardsToDatabase(-1, fromVisit.Id, -1, newVisit.Id,
                fromBrachialPlexusCardWorker, toBrachialPlexusCardWorker, fromObstetricParalysisCardWorker, toObstetricParalysisCardWorker,
                fromRangeOfMotionCardWorker, toRangeOfMotionCardWorker, fromCardWorker, toCardWorker);
        }


        /// <summary>
        /// Добавить госпитализацю и все вложенные объекты
        /// </summary>
        /// <param name="fromPatientWorker">Класс для работы с пациентами в базе, откуда копируется госпитализация</param>
        /// <param name="toPatientWorker">Класс для работы с пациентами в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationWorker">Класс для работы с госпитализациями в базе, откуда копируется госпитализация</param>
        /// <param name="toHospitalizationWorker">Класс для работы с госпитализациями в базе, куда копируется госпитализация</param>
        /// <param name="fromMedicalInspectionWorker">Класс для работы с осмотрами в отделении в базе, откуда копируется госпитализация</param>
        /// <param name="toMedicalInspectionWorker">Класс для работы с осмотрами в отделении в базе, куда копируется госпитализация</param>
        /// <param name="fromDischargeEpicrisisWorker">Класс для работы с выписными эпикризами в базе, откуда копируется госпитализация</param>
        /// <param name="toDischargeEpicrisisWorker">Класс для работы с выписными эпикризами в базе, куда копируется госпитализация</param>
        /// <param name="fromLineOfCommunicationEpicrisisWorker">Класс для работы с этапными эпикризами в базе, откуда копируется госпитализация</param>
        /// <param name="toLineOfCommunicationEpicrisisWorker">Класс для работы с этапными эпикризами в базе, куда копируется госпитализация</param>
        /// <param name="fromTransferableEpicrisisWorker">Класс для работы с переводными эпикризами в базе, откуда копируется госпитализация</param>
        /// <param name="toTransferableEpicrisisWorker">Класс для работы с переводными эпикризами в базе, куда копируется госпитализация</param>
        /// <param name="fromOperationWorker">Класс для работы с операциями в базе, откуда копируется госпитализация</param>
        /// <param name="toOperationWorker">Класс для работы с операциями в базе, куда копируется госпитализация</param>
        /// <param name="fromOperationProtocolWorker">Класс для работы с операционными протоколами в базе, откуда копируется госпитализация</param>
        /// <param name="toOperationProtocolWorker">Класс для работы с операционными протоколами в базе, куда копируется госпитализация</param>
        /// <param name="fromBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, откуда копируется госпитализация</param>
        /// <param name="toBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, куда копируется госпитализация</param>
        /// <param name="fromObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, откуда копируется госпитализация</param>
        /// <param name="toObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, куда копируется госпитализация</param>
        /// <param name="fromRangeOfMotionCardWorker">Класс для работы с картами на объём движений в базе, откуда копируется госпитализация</param>
        /// <param name="toRangeOfMotionCardWorker">Класс для работы с картами на объём движений в базе, куда копируется госпитализация</param>
        /// <param name="fromCardWorker">Класс для работы с картами из двух частей в базе, откуда копируется госпитализация</param>
        /// <param name="toCardWorker">Класс для работы с картами из двух частей в базе, куда копируется госпитализация</param>
        /// <param name="fromPatientId">id пациента из базы, откуда копируется госпитализация</param>
        /// <param name="toPaientId">id пациента из базы, куда копируется госпитализация</param>
        /// <param name="hospitalizationId">id госпитализации, которую надо скопировать</param>
        public static void AddHospitalization(
            CPatientWorker fromPatientWorker,
            CPatientWorker toPatientWorker,
            CHospitalizationWorker fromHospitalizationWorker,
            CHospitalizationWorker toHospitalizationWorker,
            CMedicalInspectionWorker fromMedicalInspectionWorker,
            CMedicalInspectionWorker toMedicalInspectionWorker,
            CDischargeEpicrisisWorker fromDischargeEpicrisisWorker,
            CDischargeEpicrisisWorker toDischargeEpicrisisWorker,
            CLineOfCommunicationEpicrisisWorker fromLineOfCommunicationEpicrisisWorker,
            CLineOfCommunicationEpicrisisWorker toLineOfCommunicationEpicrisisWorker,
            CTransferableEpicrisisWorker fromTransferableEpicrisisWorker,
            CTransferableEpicrisisWorker toTransferableEpicrisisWorker,
            COperationWorker fromOperationWorker,
            COperationWorker toOperationWorker,
            COperationProtocolWorker fromOperationProtocolWorker,
            COperationProtocolWorker toOperationProtocolWorker,
            CBrachialPlexusCardWorker fromBrachialPlexusCardWorker,
            CBrachialPlexusCardWorker toBrachialPlexusCardWorker,
            CObstetricParalysisCardWorker fromObstetricParalysisCardWorker,
            CObstetricParalysisCardWorker toObstetricParalysisCardWorker,
            CRangeOfMotionCardWorker fromRangeOfMotionCardWorker,
            CRangeOfMotionCardWorker toRangeOfMotionCardWorker,
            CCardWorker fromCardWorker,
            CCardWorker toCardWorker,
            int fromPatientId,
            int toPaientId,
            int hospitalizationId)
        {
            CHospitalization fromHospitalization = fromHospitalizationWorker.GetById(hospitalizationId);

            // Добавляем госпитализацию
            var newHospitalization = new CHospitalization(fromHospitalization)
            {
                Id = toHospitalizationWorker.GetNewID(),
                PatientId = toPaientId
            };
            toHospitalizationWorker.Update(newHospitalization, toPatientWorker.GetById(toPaientId));

            AddMedicalInspection(
                fromMedicalInspectionWorker,
                toMedicalInspectionWorker,
                hospitalizationId,
                newHospitalization.Id);

            AddDischargeEpicrisis(
                fromDischargeEpicrisisWorker,
                toDischargeEpicrisisWorker,
                hospitalizationId,
                newHospitalization.Id);

            AddLineOfCommunicationEpicrisis(
                fromLineOfCommunicationEpicrisisWorker,
                toLineOfCommunicationEpicrisisWorker,
                hospitalizationId,
                newHospitalization.Id);

            AddTransferableEpicrisis(
                fromTransferableEpicrisisWorker,
                toTransferableEpicrisisWorker,
                hospitalizationId,
                newHospitalization.Id);

            // Добавляем карты
            AddCardsToDatabase(fromHospitalization.Id, -1, newHospitalization.Id, -1,
                fromBrachialPlexusCardWorker, toBrachialPlexusCardWorker, fromObstetricParalysisCardWorker, toObstetricParalysisCardWorker,
                fromRangeOfMotionCardWorker, toRangeOfMotionCardWorker, fromCardWorker, toCardWorker);

            // Добавляем операции
            foreach (COperation fromOperation in fromOperationWorker.GetListByHospitalizationId(fromHospitalization.Id))
            {
                AddOperationAndProtocol(
                    toHospitalizationWorker,
                    fromOperationWorker,
                    toOperationWorker,
                    fromOperationProtocolWorker,
                    toOperationProtocolWorker,
                    fromOperation.Id,
                    newHospitalization.Id);
            }
        }


        /// <summary>
        /// Добавляем операцию и предоперационный протокол
        /// </summary>
        /// <param name="toHospitalizationWorker"></param>
        /// <param name="fromOperationWorker"></param>
        /// <param name="toOperationWorker"></param>
        /// <param name="fromOperationProtocolWorker"></param>
        /// <param name="toOperationProtocolWorker"></param>
        /// <param name="fromOperationId"></param>
        /// <param name="toHospitalizationId"></param>
        public static void AddOperationAndProtocol(
            CHospitalizationWorker toHospitalizationWorker,
            COperationWorker fromOperationWorker,
            COperationWorker toOperationWorker,
            COperationProtocolWorker fromOperationProtocolWorker,
            COperationProtocolWorker toOperationProtocolWorker,
            int fromOperationId,
            int toHospitalizationId)
        {
            CHospitalization toHospitalization = toHospitalizationWorker.GetById(toHospitalizationId);

            var toOperation = new COperation(fromOperationWorker.GetById(fromOperationId))
            {
                Id = toOperationWorker.GetNewID(),
                PatientId = toHospitalization.PatientId,
                HospitalizationId = toHospitalizationId
            };
            toOperationWorker.Update(toOperation);

            // Добавляем предоперационный протокол
            toOperationProtocolWorker.GetByOperationId(toOperation.Id);
            var toOperationProtocol = new COperationProtocol(
               fromOperationProtocolWorker.GetByOperationId(fromOperationId))
            {
                OperationId = toOperation.Id
            };
            toOperationProtocolWorker.Update(toOperationProtocol);
        }


        /// <summary>
        /// Добавляем переводной эпикриз
        /// </summary>        
        /// <param name="fromTransferableEpicrisisWorker">Класс для работы с переводными эпикризами в базе, откуда копируется госпитализация</param>
        /// <param name="toTransferableEpicrisisWorker">Класс для работы с переводными эпикризами в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationId">id госпитализации, из которой берётся переводной эпикриз</param>
        /// <param name="toHospitalizationId">id госпитализации, в которую копируется переводной эпикриз</param>
        public static void AddTransferableEpicrisis(
            CTransferableEpicrisisWorker fromTransferableEpicrisisWorker,
            CTransferableEpicrisisWorker toTransferableEpicrisisWorker,
            int fromHospitalizationId,
            int toHospitalizationId)
        {
            if (fromTransferableEpicrisisWorker.IsExists(fromHospitalizationId))
            {
                toTransferableEpicrisisWorker.GetByHospitalizationId(toHospitalizationId);
                var newTransferableEpicrisis = new CTransferableEpicrisis(
                    fromTransferableEpicrisisWorker.GetByHospitalizationId(fromHospitalizationId))
                {
                    HospitalizationId = toHospitalizationId
                };
                toTransferableEpicrisisWorker.Update(newTransferableEpicrisis);
            }
        }


        /// <summary>
        /// Добавляем этапный эпикриз
        /// </summary>        
        /// <param name="fromLineOfCommunicationEpicrisisWorker">Класс для работы с этапными эпикризами в базе, откуда копируется госпитализация</param>
        /// <param name="toLineOfCommunicationEpicrisisWorker">Класс для работы с этапными эпикризами в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationId">id госпитализации, из которой берётся этапный эпикриз</param>
        /// <param name="toHospitalizationId">id госпитализации, в которую копируется этапный эпикриз</param>
        public static void AddLineOfCommunicationEpicrisis(
            CLineOfCommunicationEpicrisisWorker fromLineOfCommunicationEpicrisisWorker,
            CLineOfCommunicationEpicrisisWorker toLineOfCommunicationEpicrisisWorker,
            int fromHospitalizationId,
            int toHospitalizationId)
        {
            if (fromLineOfCommunicationEpicrisisWorker.IsExists(fromHospitalizationId))
            {
                toLineOfCommunicationEpicrisisWorker.GetByHospitalizationId(toHospitalizationId);
                var newLineOfCommunicationEpicrisis = new CLineOfCommunicationEpicrisis(
                    fromLineOfCommunicationEpicrisisWorker.GetByHospitalizationId(fromHospitalizationId))
                {
                    HospitalizationId = toHospitalizationId
                };
                toLineOfCommunicationEpicrisisWorker.Update(newLineOfCommunicationEpicrisis);
            }
        }


        /// <summary>
        /// Добавляем выписной эпикриз
        /// </summary>        
        /// <param name="fromDischargeEpicrisisWorker">Класс для работы с выписными эпикризами в базе, откуда копируется госпитализация</param>
        /// <param name="toDischargeEpicrisisWorker">Класс для работы с выписными эпикризами в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationId">id госпитализации, из которой берётся выписной эпикриз</param>
        /// <param name="toHospitalizationId">id госпитализации, в которую копируется выписной эпикриз</param>
        public static void AddDischargeEpicrisis(
            CDischargeEpicrisisWorker fromDischargeEpicrisisWorker,
            CDischargeEpicrisisWorker toDischargeEpicrisisWorker,
            int fromHospitalizationId,
            int toHospitalizationId)
        {
            if (fromDischargeEpicrisisWorker.IsExists(fromHospitalizationId))
            {
                toDischargeEpicrisisWorker.GetByHospitalizationId(toHospitalizationId);
                var newDischargeEpicrisis = new CDischargeEpicrisis(
                    fromDischargeEpicrisisWorker.GetByHospitalizationId(fromHospitalizationId))
                {
                    HospitalizationId = toHospitalizationId
                };
                toDischargeEpicrisisWorker.Update(newDischargeEpicrisis);
            }
        }


        /// <summary>
        /// Добавляем осмотр в отделении
        /// </summary>
        /// <param name="fromMedicalInspectionWorker">Класс для работы с осмотрами в отделении в базе, откуда копируется госпитализация</param>
        /// <param name="toMedicalInspectionWorker">Класс для работы с осмотрами в отделении в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationId">id госпитализации, из которой берётся осмотр в отделении</param>
        /// <param name="toHospitalizationId">id госпитализации, в которую копируется осмотр в отделении</param>
        public static void AddMedicalInspection(
            CMedicalInspectionWorker fromMedicalInspectionWorker,
            CMedicalInspectionWorker toMedicalInspectionWorker,
            int fromHospitalizationId,
            int toHospitalizationId)
        {
            if (fromMedicalInspectionWorker.IsExists(fromHospitalizationId))
            {
                toMedicalInspectionWorker.GetByHospitalizationId(toHospitalizationId);
                var newMedicalInspection = new CMedicalInspection(
                    fromMedicalInspectionWorker.GetByHospitalizationId(fromHospitalizationId))
                {
                    HospitalizationId = toHospitalizationId
                };
                toMedicalInspectionWorker.Update(newMedicalInspection);
            }
        }


        /// <summary>
        /// Добавить анамнез
        /// </summary>
        /// <param name="fromAnamneseWorker">Класс для работы с анамнезами в базе, откуда копируется пациент</param>
        /// <param name="toAnamneseWorker">Класс для работы с анамнезами в базе, куда копируется пациент</param>
        /// <param name="fromPatientId">id пациента, у которого мы берём анамнез</param>
        /// <param name="toPatientId">id пациента, которому мы вставляем анамнез</param>
        public static void AddAnamnese(
            CAnamneseWorker fromAnamneseWorker,
            CAnamneseWorker toAnamneseWorker,
            int fromPatientId,
            int toPatientId)
        {
            if (fromAnamneseWorker.IsExists(fromPatientId))
            {
                toAnamneseWorker.GetByPatientId(toPatientId);
                var newAnamnese = new CAnamnese(fromAnamneseWorker.GetByPatientId(fromPatientId))
                {
                    PatientId = toPatientId
                };
                toAnamneseWorker.Update(newAnamnese);
            }
        }


        /// <summary>
        /// Добавить акушерский анамнез
        /// </summary>
        /// <param name="fromObstetricHistoryWorker"></param>
        /// <param name="toObstetricHistoryWorker"></param>
        /// <param name="fromPatientId">id пациента, у которого мы берём ???</param>
        /// <param name="toPatientId">id пациента, которому мы вставляем ???</param>
        public static void AddObstetricHistory(
            CObstetricHistoryWorker fromObstetricHistoryWorker,
            CObstetricHistoryWorker toObstetricHistoryWorker,
            int fromPatientId,
            int toPatientId)
        {
            if (fromObstetricHistoryWorker.IsExists(fromPatientId))
            {
                toObstetricHistoryWorker.GetByPatientId(toPatientId);
                var newObstetricHistory = new CObstetricHistory(fromObstetricHistoryWorker.GetByPatientId(fromPatientId))
                {
                    PatientId = toPatientId
                };
                toObstetricHistoryWorker.Update(newObstetricHistory);
            }
        }


        /// <summary>
        /// Добавить карты обследований в базу
        /// </summary>
        /// <param name="fromHospitalizationId">id госпитализации в базе, откуда копируются карты</param>
        /// <param name="fromVisitId">id консультации в базе, откуда копируются карты</param>
        /// <param name="toHospitalizationId">id новой госпитализации в базе, куда копируются карты</param>
        /// <param name="toVisitId">id новой консультации в базе, куда копируются карты</param>
        /// <param name="fromBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, откуда копируется госпитализация</param>
        /// <param name="toBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, куда копируется госпитализация</param>
        /// <param name="fromObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, откуда копируется госпитализация</param>
        /// <param name="toObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, куда копируется госпитализация</param>
        /// <param name="fromRangeOfMotionCardWorker">Класс для работы с картами на объём движений в базе, откуда копируется госпитализация</param>
        /// <param name="toRangeOfMotionCardWorker">Класс для работы с картами на объём движений в базе, куда копируется госпитализация</param>
        /// <param name="fromCardWorker">Класс для работы с картами из двух частей в базе, откуда копируется госпитализация</param>
        /// <param name="toCardWorker">Класс для работы с картами из двух частей в базе, куда копируется госпитализация</param>
        public static void AddCardsToDatabase(
            int fromHospitalizationId,
            int fromVisitId,
            int toHospitalizationId,
            int toVisitId,
            CBrachialPlexusCardWorker fromBrachialPlexusCardWorker,
            CBrachialPlexusCardWorker toBrachialPlexusCardWorker,
            CObstetricParalysisCardWorker fromObstetricParalysisCardWorker,
            CObstetricParalysisCardWorker toObstetricParalysisCardWorker,
            CRangeOfMotionCardWorker fromRangeOfMotionCardWorker,
            CRangeOfMotionCardWorker toRangeOfMotionCardWorker,
            CCardWorker fromCardWorker,
            CCardWorker toCardWorker)
        {
            AddBrachialPlexusCard(
                fromBrachialPlexusCardWorker,
                toBrachialPlexusCardWorker,
                fromHospitalizationId,
                toHospitalizationId,
                fromVisitId,
                toVisitId);

            AddObstetricParalysisCard(
                fromObstetricParalysisCardWorker,
                toObstetricParalysisCardWorker,
                fromHospitalizationId,
                toHospitalizationId,
                fromVisitId,
                toVisitId);

            AddRangeOfMotionCard(
                fromRangeOfMotionCardWorker,
                toRangeOfMotionCardWorker,
                fromHospitalizationId,
                toHospitalizationId,
                fromVisitId,
                toVisitId);

            var cardTypes = (CardType[])Enum.GetValues(typeof(CardType));
            foreach (CardType cardType in cardTypes)
            {
                AddLeftRightCard(
                    fromCardWorker,
                    toCardWorker,
                    cardType,
                    fromHospitalizationId,
                    toHospitalizationId,
                    fromVisitId,
                    toVisitId);
            }
        }


        /// <summary>
        /// Добавить карту обследования с двумя частями
        /// </summary>
        /// <param name="fromCardWorker">Класс для работы с картами из двух частей в базе, откуда копируется госпитализация</param>
        /// <param name="toCardWorker">Класс для работы с картами из двух частей в базе, куда копируется госпитализация</param>
        /// <param name="cardType">Тип карты</param>
        /// <param name="fromHospitalizationId">id госпитализации в базе, откуда копируются карты</param>
        /// <param name="toHospitalizationId">id новой госпитализации в базе, куда копируются карты</param>
        /// <param name="fromVisitId">id консультации в базе, откуда копируются карты</param>
        /// <param name="toVisitId">id новой консультации в базе, куда копируются карты</param>
        public static void AddLeftRightCard(
            CCardWorker fromCardWorker,
            CCardWorker toCardWorker,
            CardType cardType,
            int fromHospitalizationId,
            int toHospitalizationId,
            int fromVisitId,
            int toVisitId)
        {
            if (fromCardWorker.IsExists(fromHospitalizationId, fromVisitId, cardType))
            {
                toCardWorker.GetByGeneralData(toHospitalizationId, toVisitId, CardSide.Left, cardType);
                var newCard = new CCard(
                    fromCardWorker.GetByGeneralData(fromHospitalizationId, fromVisitId, CardSide.Left, cardType))
                {
                    HospitalizationId = toHospitalizationId,
                    VisitId = toVisitId
                };
                toCardWorker.Update(newCard);

                toCardWorker.GetByGeneralData(toHospitalizationId, toVisitId, CardSide.Right, cardType);
                newCard = new CCard(
                    fromCardWorker.GetByGeneralData(fromHospitalizationId, fromVisitId, CardSide.Right, cardType))
                {
                    HospitalizationId = toHospitalizationId,
                    VisitId = toVisitId
                };
                toCardWorker.Update(newCard);
            }
        }


        /// <summary>
        /// Добавить карту на объём движений
        /// </summary>
        /// <param name="fromRangeOfMotionCardWorker"></param>
        /// <param name="toRangeOfMotionCardWorker"></param>
        /// <param name="fromHospitalizationId">id госпитализации в базе, откуда копируются карты</param>
        /// <param name="toHospitalizationId">id новой госпитализации в базе, куда копируются карты</param>
        /// <param name="fromVisitId">id консультации в базе, откуда копируются карты</param>
        /// <param name="toVisitId">id новой консультации в базе, куда копируются карты</param>
        public static void AddRangeOfMotionCard(
            CRangeOfMotionCardWorker fromRangeOfMotionCardWorker,
            CRangeOfMotionCardWorker toRangeOfMotionCardWorker,
            int fromHospitalizationId,
            int toHospitalizationId,
            int fromVisitId,
            int toVisitId)
        {
            if (fromRangeOfMotionCardWorker.IsExists(fromHospitalizationId, fromVisitId))
            {
                toRangeOfMotionCardWorker.GetByHospitalizationAndVisitId(toHospitalizationId, toVisitId);
                var newRangeOfMotionCard = new CRangeOfMotionCard(
                    fromRangeOfMotionCardWorker.GetByHospitalizationAndVisitId(fromHospitalizationId, fromVisitId))
                {
                    HospitalizationId = toHospitalizationId,
                    VisitId = toVisitId
                };
                toRangeOfMotionCardWorker.Update(newRangeOfMotionCard);
            }
        }


        /// <summary>
        /// Добавить карту на акушерский паралич
        /// </summary>
        /// <param name="fromObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, откуда копируется госпитализация</param>
        /// <param name="toObstetricParalysisCardWorker">Класс для работы с картами на акушерский паралич в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationId">id госпитализации в базе, откуда копируются карты</param>
        /// <param name="toHospitalizationId">id новой госпитализации в базе, куда копируются карты</param>
        /// <param name="fromVisitId">id консультации в базе, откуда копируются карты</param>
        /// <param name="toVisitId">id новой консультации в базе, куда копируются карты</param>
        public static void AddObstetricParalysisCard(
            CObstetricParalysisCardWorker fromObstetricParalysisCardWorker,
            CObstetricParalysisCardWorker toObstetricParalysisCardWorker,
            int fromHospitalizationId,
            int toHospitalizationId,
            int fromVisitId,
            int toVisitId)
        {
            if (fromObstetricParalysisCardWorker.IsExists(fromHospitalizationId, fromVisitId))
            {
                toObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(toHospitalizationId, toVisitId);
                var newObstetricParalysisCard = new CObstetricParalysisCard(
                    fromObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(fromHospitalizationId, fromVisitId))
                {
                    HospitalizationId = toHospitalizationId,
                    VisitId = toVisitId
                };
                toObstetricParalysisCardWorker.Update(newObstetricParalysisCard);
            }
        }


        /// <summary>
        /// Добавить карту на плечевое сплетение
        /// </summary>
        /// <param name="fromBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, откуда копируется госпитализация</param>
        /// <param name="toBrachialPlexusCardWorker">Класс для работы с картами на плечевое сплетение в базе, куда копируется госпитализация</param>
        /// <param name="fromHospitalizationId">id госпитализации в базе, откуда копируются карты</param>
        /// <param name="toHospitalizationId">id новой госпитализации в базе, куда копируются карты</param>
        /// <param name="fromVisitId">id консультации в базе, откуда копируются карты</param>
        /// <param name="toVisitId">id новой консультации в базе, куда копируются карты</param>
        public static void AddBrachialPlexusCard(
            CBrachialPlexusCardWorker fromBrachialPlexusCardWorker,
            CBrachialPlexusCardWorker toBrachialPlexusCardWorker,
            int fromHospitalizationId,
            int toHospitalizationId,
            int fromVisitId,
            int toVisitId)
        {
            if (fromBrachialPlexusCardWorker.IsExists(fromHospitalizationId, fromVisitId))
            {
                toBrachialPlexusCardWorker.GetByHospitalizationAndVisitId(toHospitalizationId, toVisitId);
                var newBrachialPlexusCard = new CBrachialPlexusCard(
                    fromBrachialPlexusCardWorker.GetByHospitalizationAndVisitId(fromHospitalizationId, fromVisitId))
                {
                    HospitalizationId = toHospitalizationId,
                    VisitId = toVisitId
                };
                toBrachialPlexusCardWorker.Update(newBrachialPlexusCard);
            }
        }


        /// <summary>
        /// Копирование содержимого личной папки копируемого пациента
        /// </summary>
        /// <param name="mergeForm">Указатель на форму MergeForm</param>
        /// <param name="fromFolder">Путь до папки, откуда надо брать данные</param>
        /// <param name="toFolder">Путь до папки, куда надо класть данные</param>
        public static void CopyContent(MergeForm mergeForm, string fromFolder, string toFolder)
        {
            if (Directory.Exists(toFolder))
            {
                mergeForm.SetStatus("ВНИМАНИЕ!!! Личная папка '" + toFolder + "' уже существует. Копирование данных из личной папки осуществляться не будет.");
                return;
            }

            if (!Directory.Exists(fromFolder))
            {
                mergeForm.SetStatus("ВНИМАНИЕ!!! Личная папка '" + fromFolder + "' не найдена. Копирование данных из личной папки осуществляться не будет.");
                return;
            }

            Directory.CreateDirectory(toFolder);
            mergeForm.SetStatus("Создали личную папку '" + toFolder + "'");

            var fromFilePathsList = new List<string>();
            GetFilesPathsFromAllFolders(fromFolder, fromFilePathsList);

            foreach (string fromFullPath in fromFilePathsList)
            {
                string relativePath = GetRelativePath(fromFolder, fromFullPath);
                string toFullPath = Path.Combine(toFolder, relativePath);

                string toFolderPath = Path.GetDirectoryName(toFullPath);
                if (toFolderPath != null)
                {
                    if (!Directory.Exists(toFolderPath))
                    {
                        Directory.CreateDirectory(toFolderPath);
                    }

                    File.Copy(fromFullPath, toFullPath);
                    mergeForm.SetStatus("Скопировали '" + relativePath + "'");
                }
            }
        }


        /// <summary>
        /// Получить кусок пути после базового пути
        /// </summary>
        /// <param name="fromFolder">Путь до базовой папки</param>
        /// <param name="fromFullPath">Полный путь до файла</param>
        /// <returns></returns>
        public static string GetRelativePath(string fromFolder, string fromFullPath)
        {
            string relativePath = fromFullPath.Substring(fromFolder.Length);

            return relativePath.TrimStart('\\');
        }


        /// <summary>
        /// Рекурсивная функция получения списка путей до всех файлов из всех директорий, находящихся в переданной директории
        /// </summary>
        /// <param name="folderName">Путь до папки</param>
        /// <param name="filePathsList">Список найденных путей</param>
        /// <returns></returns>
        public static void GetFilesPathsFromAllFolders(string folderName, List<string> filePathsList)
        {
            var diParent = new DirectoryInfo(folderName);
            foreach (DirectoryInfo di in diParent.GetDirectories())
            {
                GetFilesPathsFromAllFolders(di.FullName, filePathsList);
            }

            foreach (FileInfo fi in diParent.GetFiles())
            {
                filePathsList.Add(fi.FullName);
            }
        }
    }
}
