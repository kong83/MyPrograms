using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using SurgeryHelper.Forms;
using SurgeryHelper.Workers;

namespace SurgeryHelper.Tools
{
    public class CWorkersKeeper
    {
        private readonly CPatientWorker _patientWorker;

        public CPatientWorker PatientWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _patientWorker;
            }
        }

        private readonly CHospitalizationWorker _hospitalizationWorker;

        public CHospitalizationWorker HospitalizationWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _hospitalizationWorker;
            }
        }

        private readonly CVisitWorker _visitWorker;

        public CVisitWorker VisitWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _visitWorker;
            }
        }

        private readonly COperationWorker _operationWorker;

        public COperationWorker OperationWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _operationWorker;
            }
        }

        private readonly CNosologyWorker _nosologyWorker;

        public CNosologyWorker NosologyWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _nosologyWorker;
            }
        }

        private readonly COperationTypeWorker _operationTypeWorker;

        public COperationTypeWorker OperationTypeWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _operationTypeWorker;
            }
        }

        private readonly COrderlyWorker _orderlyWorker;

        public COrderlyWorker OrderlyWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _orderlyWorker;
            }
        }

        private readonly CScrubNurseWorker _scrubNurseWorker;

        public CScrubNurseWorker ScrubNurseWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _scrubNurseWorker;
            }
        }

        private readonly CSurgeonWorker _surgeonWorker;

        public CSurgeonWorker SurgeonWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _surgeonWorker;
            }
        }

        private readonly CDischargeEpicrisisWorker _dischargeEpicrisisWorker;

        public CDischargeEpicrisisWorker DischargeEpicrisisWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _dischargeEpicrisisWorker;
            }
        }

        private readonly CLineOfCommunicationEpicrisisWorker _lineOfCommunicationEpicrisisWorker;

        public CLineOfCommunicationEpicrisisWorker LineOfCommunicationEpicrisisWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _lineOfCommunicationEpicrisisWorker;
            }
        }

        private readonly CMedicalInspectionWorker _medicalInspectionWorker;

        public CMedicalInspectionWorker MedicalInspectionWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _medicalInspectionWorker;
            }
        }

        private readonly COperationProtocolWorker _operationProtocolWorker;

        public COperationProtocolWorker OperationProtocolWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _operationProtocolWorker;
            }
        }

        private readonly CTransferableEpicrisisWorker _transferableEpicrisisWorker;

        public CTransferableEpicrisisWorker TransferableEpicrisisWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _transferableEpicrisisWorker;
            }
        }

        private readonly CAnamneseWorker _anamneseWorker;

        public CAnamneseWorker AnamneseWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _anamneseWorker;
            }
        }

        private readonly CCardWorker _cardWorker;

        public CCardWorker CardWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _cardWorker;
            }
        }

        private readonly CBrachialPlexusCardWorker _brachialPlexusCardWorker;

        public CBrachialPlexusCardWorker BrachialPlexusCardWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _brachialPlexusCardWorker;
            }
        }

        private readonly CRangeOfMotionCardWorker _rangeOfMotionCardWorker;

        public CRangeOfMotionCardWorker RangeOfMotionCardWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _rangeOfMotionCardWorker;
            }
        }

        private readonly CObstetricParalysisCardWorker _obstetricParalysisCardWorker;

        public CObstetricParalysisCardWorker ObstetricParalysisCardWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _obstetricParalysisCardWorker;
            }
        }
        
        private readonly CObstetricHistoryWorker _obstetricHistoryWorker;

        public CObstetricHistoryWorker ObstetricHistoryWorker
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _obstetricHistoryWorker;
            }
        }

        private readonly CGlobalSettings _globalSettings;

        public CGlobalSettings GlobalSettings
        {
            get
            {
                if (CPassHelper.GetHash() != ConfigurationEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                return _globalSettings;
            }
        }

        public readonly CConfigurationEngine ConfigurationEngine;

        public readonly string ExecutableDirectoryPath;

        public CWorkersKeeper(string dataPath)
            : this(dataPath, true)
        {
        }

        public CWorkersKeeper(string dataPath, bool isNeedToStoreDbFiles)
        {
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            var dataDirectoryInfo = new DirectoryInfo(dataPath);
            ExecutableDirectoryPath = dataDirectoryInfo.Parent != null 
                ? dataDirectoryInfo.Parent.FullName 
                : dataDirectoryInfo.FullName;

            if (isNeedToStoreDbFiles)
            {
                try
                {
                    SaveDbFilesToTempFolder(dataPath);
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog("Не удалось осуществить резервное копирование файлов с данными. Сообщение об ошибке:\r\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            var waitForm = new WaitForm("Загрузка данных...");

            try
            {
                waitForm.Show();
                Application.DoEvents();

                ConfigurationEngine = new CConfigurationEngine();

                waitForm.SetProgress(4);
                _nosologyWorker = new CNosologyWorker(dataPath);
                waitForm.SetProgress(8);
                _operationTypeWorker = new COperationTypeWorker(dataPath);
                waitForm.SetProgress(12);
                _orderlyWorker = new COrderlyWorker(dataPath);
                waitForm.SetProgress(16);
                _scrubNurseWorker = new CScrubNurseWorker(dataPath);
                waitForm.SetProgress(20);
                _surgeonWorker = new CSurgeonWorker(dataPath);

                waitForm.SetProgress(24);
                _patientWorker = new CPatientWorker(this, dataPath);
                waitForm.SetProgress(28);
                _hospitalizationWorker = new CHospitalizationWorker(this, dataPath);
                waitForm.SetProgress(32);
                _visitWorker = new CVisitWorker(this, dataPath);               
                waitForm.SetProgress(36);
                _operationWorker = new COperationWorker(this, dataPath);

                waitForm.SetProgress(40);
                _dischargeEpicrisisWorker = new CDischargeEpicrisisWorker(dataPath);
                waitForm.SetProgress(44);
                _lineOfCommunicationEpicrisisWorker = new CLineOfCommunicationEpicrisisWorker(dataPath);
                waitForm.SetProgress(48);
                _medicalInspectionWorker = new CMedicalInspectionWorker(dataPath);
                waitForm.SetProgress(52);
                _operationProtocolWorker = new COperationProtocolWorker(dataPath);
                waitForm.SetProgress(56);
                _transferableEpicrisisWorker = new CTransferableEpicrisisWorker(dataPath);

                waitForm.SetProgress(60);
                _anamneseWorker = new CAnamneseWorker(dataPath);
                waitForm.SetProgress(66);
                _obstetricHistoryWorker = new CObstetricHistoryWorker(dataPath);

                waitForm.SetProgress(72);
                _cardWorker = new CCardWorker(dataPath);
                waitForm.SetProgress(78);
                _brachialPlexusCardWorker = new CBrachialPlexusCardWorker(dataPath);
                waitForm.SetProgress(84);
                _rangeOfMotionCardWorker = new CRangeOfMotionCardWorker(dataPath);
                waitForm.SetProgress(92);
                _obstetricParalysisCardWorker = new CObstetricParalysisCardWorker(dataPath);

                waitForm.SetProgress(100);
                _globalSettings = new CGlobalSettings(dataPath);
            }
            finally
            {
                waitForm.CloseForm();
            }
        }


        /// <summary>
        /// Получить путь до папки с сохранёнными версиями баз
        /// </summary>
        /// <returns></returns>
        public static string GetSaveDataLocation(out string saveDataPath)
        {
            saveDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SurgeryHelper2");
            return Path.Combine(saveDataPath, CConvertEngine.DateTimeToString(DateTime.Now));
        }


        /// <summary>
        /// Сохранить файлы баз данных во временной папке пользователя
        /// Один раз в день
        /// </summary>
        /// <param name="dataPath">Путь до папки с базами данных</param>
        private static void SaveDbFilesToTempFolder(string dataPath)
        {
            // Скопировать файлы с данными
            var di = new DirectoryInfo(dataPath);
            string saveDataPath;
            string newSaveDataLocation = GetSaveDataLocation(out saveDataPath);

            if (di.GetFiles().Length == 0 || Directory.Exists(newSaveDataLocation))
            {
                return;
            }

            Directory.CreateDirectory(newSaveDataLocation);

            foreach (FileInfo fi in di.GetFiles())
            {
                fi.CopyTo(Path.Combine(newSaveDataLocation, fi.Name));
            }

            // Скопировать файлы c картинками
            di = new DirectoryInfo(Path.Combine(dataPath, "CardPictures"));
            if (di.Exists)
            {
                string newSavePictureLocation = Path.Combine(newSaveDataLocation, "CardPictures");

                Directory.CreateDirectory(newSavePictureLocation);

                foreach (FileInfo fi in di.GetFiles())
                {
                    fi.CopyTo(Path.Combine(newSavePictureLocation, fi.Name));
                }
            }

            // Удаление старых папок с сохранёнными файлами (оставить только 10 последних)
            di = new DirectoryInfo(saveDataPath);
            var savedFolders = new List<DateTime>();
            foreach (DirectoryInfo curDi in di.GetDirectories())
            {
                try
                {
                    savedFolders.Add(CConvertEngine.StringToDateTime(curDi.Name));
                }
                catch
                {
                }
            }

            savedFolders.Sort();

            for (int i = 0; i < savedFolders.Count - 10; i++)
            {
                Directory.Delete(Path.Combine(saveDataPath, CConvertEngine.DateTimeToString(savedFolders[i])), true);
            }
        }
    }
}
