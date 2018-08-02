using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CHospitalizationWorker : CBaseWorker
    {
        private readonly CWorkersKeeper _workersKeeper;
        private List<CHospitalization> _hospitalizationList;
        private readonly string _hospitalizationPath;

        public CHospitalizationWorker(CWorkersKeeper workersKeeper, string dataPath)
        {
            _workersKeeper = workersKeeper;
            _hospitalizationPath = Path.Combine(dataPath, "hospitalizations.save");
            Load();
        }


        /// <summary>
        /// Получить новый id для госпитализации
        /// </summary>
        /// <returns></returns>
        public int GetNewID()
        {
            return GetNewID(_hospitalizationList.ToArray());
        }


        /// <summary>
        /// Добавить информацию о госпитализации без сохранения в базу
        /// </summary>
        /// <param name="hospitalizationInfo">Информация о госпитализации</param>
        public void AddWithoutSaving(CHospitalization hospitalizationInfo)
        {
            _hospitalizationList.Add(hospitalizationInfo);
        }


        /// <summary>
        /// Обновить информацию о госпитализации
        /// </summary>
        /// <param name="hospitalizationInfo">Информация о госпитализации</param>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void Update(CHospitalization hospitalizationInfo, CPatient patientInfo)
        {
            CHospitalization tempHospitalization = GetByGeneralData(
                hospitalizationInfo.DeliveryDate,
                patientInfo.GetFullName(),
                patientInfo.Nosology,
                hospitalizationInfo.Id);
            if (tempHospitalization != null)
            {
                throw new Exception("Госпитализация для этого пациента с такой датой поступления уже содержится в базе. Измените дату или время поступления.");
            }

            int n = 0;
            while (n < _hospitalizationList.Count && _hospitalizationList[n].Id != hospitalizationInfo.Id)
            {
                n++;
            }

            if (n == _hospitalizationList.Count)
            {
                _hospitalizationList.Add(hospitalizationInfo);
            }
            else
            {
                _hospitalizationList[n] = hospitalizationInfo;
            }
            
            Save();
        }


        /// <summary>
        /// Удаление все относящиеся к госпитализации объекты
        /// </summary>
        /// <param name="hospitalizationId">Id госпитализации</param>
        private void RemoveHospitalizationEntitys(int hospitalizationId)
        {
            _workersKeeper.DischargeEpicrisisWorker.Remove(hospitalizationId);
            _workersKeeper.LineOfCommunicationEpicrisisWorker.Remove(hospitalizationId);
            _workersKeeper.MedicalInspectionWorker.Remove(hospitalizationId);
            _workersKeeper.TransferableEpicrisisWorker.Remove(hospitalizationId);
            _workersKeeper.OperationWorker.RemoveByHospitalizationId(hospitalizationId);
            _workersKeeper.CardWorker.Remove(hospitalizationId, -1);
            _workersKeeper.BrachialPlexusCardWorker.Remove(hospitalizationId, -1);
            _workersKeeper.ObstetricParalysisCardWorker.Remove(hospitalizationId, -1);
            _workersKeeper.RangeOfMotionCardWorker.Remove(hospitalizationId, -1);
        }


        /// <summary>
        /// Удалить госпитализацию
        /// </summary>
        /// <param name="hospitalizationId">ID госпитализации</param>
        public void Remove(int hospitalizationId)
        {
            int index = 0;
            while (index < _hospitalizationList.Count)
            {
                if (_hospitalizationList[index].Id == hospitalizationId)
                {
                    _hospitalizationList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            RemoveHospitalizationEntitys(hospitalizationId);

            Save();
        }


        /// <summary>
        /// Удалить все госпитализации для удаляемого пациента
        /// </summary>
        /// <param name="patientId">Id пациента</param>
        public void RemoveByPatientId(int patientId)
        {
            int index = 0;
            while (index < _hospitalizationList.Count)
            {
                if (_hospitalizationList[index].PatientId == patientId)
                {
                    RemoveHospitalizationEntitys(_hospitalizationList[index].Id);
                    _hospitalizationList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список госпитализаций
        /// </summary>
        private void Save()
        {
            _hospitalizationList.Sort(CHospitalization.Compare);

            var hospitalizationsStr = new StringBuilder();

            foreach (CHospitalization hospitalizationInfo in _hospitalizationList)
            {
                hospitalizationsStr.Append(
                    "Id=" + hospitalizationInfo.Id + DataSplitStr +
                    "PatientId=" + hospitalizationInfo.PatientId + DataSplitStr +
                    "DeliveryDate=" + CConvertEngine.DateTimeToString(hospitalizationInfo.DeliveryDate, true) + DataSplitStr +
                    "ReleaseDate=" + CConvertEngine.DateTimeToString(hospitalizationInfo.ReleaseDate) + DataSplitStr +
                    "NumberOfCaseHistory=" + hospitalizationInfo.NumberOfCaseHistory + DataSplitStr +
                    "Diagnose=" + hospitalizationInfo.Diagnose + DataSplitStr +
                    "FotoFolderName=" + hospitalizationInfo.FotoFolderName + DataSplitStr +
                    "DoctorInChargeOfTheCase=" + hospitalizationInfo.DoctorInChargeOfTheCase + ObjSplitStr);
            }

            CDatabaseEngine.PackText(hospitalizationsStr.ToString(), _hospitalizationPath);
        }


        /// <summary>
        /// Загрузить список госпитализаций
        /// </summary>
        private void Load()
        {
            _hospitalizationList = new List<CHospitalization>();
            string allDataStr = CDatabaseEngine.UnpackText(_hospitalizationPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var hospitalizationInfo = new CHospitalization();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            hospitalizationInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "PatientId":
                            hospitalizationInfo.PatientId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "DeliveryDate":
                            hospitalizationInfo.DeliveryDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "ReleaseDate":
                            if (string.IsNullOrEmpty(keyValue[1]))
                            {
                                hospitalizationInfo.ReleaseDate = null;
                            }
                            else
                            {
                                hospitalizationInfo.ReleaseDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            }

                            break;
                        case "NumberOfCaseHistory":
                            hospitalizationInfo.NumberOfCaseHistory = keyValue[1];
                            break;
                        case "Diagnose":
                            hospitalizationInfo.Diagnose = keyValue[1];
                            break;
                        case "DoctorInChargeOfTheCase":
                            hospitalizationInfo.DoctorInChargeOfTheCase = keyValue[1];
                            break;
                        case "FotoFolderName":
                            hospitalizationInfo.FotoFolderName = keyValue[1];
                            break;
                    }
                }

                _hospitalizationList.Add(hospitalizationInfo);
            }
        }


        /// <summary>
        /// Получить госпитализацию по указанному id
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public CHospitalization GetById(int hospitalizationId)
        {
            foreach (CHospitalization hospitalization in _hospitalizationList)
            {
                if (hospitalization.Id == hospitalizationId)
                {
                    return hospitalization;
                }
            }

            throw new ArgumentException("Внутренняя ошибка программы. Не найдена госпитализация с id=" + hospitalizationId);
        }


        /// <summary>
        /// Получить количество госпитализаций для переданного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public int GetCountByPatientId(int patientId)
        {
            int result = 0;

            foreach (CHospitalization hospitalization in _hospitalizationList)
            {
                if (hospitalization.PatientId == patientId)
                {
                    result++;
                }
            }

            return result;
        }


        /// <summary>
        /// Получить список госпитализаций для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public CHospitalization[] GetListByPatientId(int patientId)
        {
            var hospitalizations = new List<CHospitalization>();

            foreach (CHospitalization hospitalization in _hospitalizationList)
            {
                if (hospitalization.PatientId == patientId)
                {
                    hospitalizations.Add(hospitalization);
                }
            }

            return hospitalizations.ToArray();
        }


        /// <summary>
        /// Вызвать копирование всех сущностей, входящих в госпитализацию
        /// </summary>
        /// <param name="hospitalizationId">Id старой госпитализации</param>
        /// <param name="newHospitalizationId">Id новой госпиализации</param>
        /// <param name="newPatientId">Id нового пациента</param>
        private void CopyHospitalizationsEssenses(
            int hospitalizationId,
            int newHospitalizationId,
            int newPatientId)
        {
            _workersKeeper.OperationWorker.CopyByHospitalizationId(
                hospitalizationId, newHospitalizationId, newPatientId);
            _workersKeeper.DischargeEpicrisisWorker.CopyByHospitalizationId(
                hospitalizationId, newHospitalizationId);
            _workersKeeper.LineOfCommunicationEpicrisisWorker.CopyByHospitalizationId(
                hospitalizationId, newHospitalizationId);
            _workersKeeper.MedicalInspectionWorker.CopyByHospitalizationId(
                hospitalizationId, newHospitalizationId);
            _workersKeeper.TransferableEpicrisisWorker.CopyByHospitalizationId(
                hospitalizationId, newHospitalizationId);
            _workersKeeper.CardWorker.CopyByHospitalizationAndVisitId(
                hospitalizationId, -1, newHospitalizationId, -1);
            _workersKeeper.BrachialPlexusCardWorker.CopyByHospitalizationAndVisitId(
                hospitalizationId, -1, newHospitalizationId, -1);
            _workersKeeper.RangeOfMotionCardWorker.CopyByHospitalizationAndVisitId(
                hospitalizationId, -1, newHospitalizationId, -1);
            _workersKeeper.ObstetricParalysisCardWorker.CopyByHospitalizationAndVisitId(
                hospitalizationId, -1, newHospitalizationId, -1);
        }


        /// <summary>
        /// Скопировать переданную госпитализацию
        /// </summary>
        /// <param name="hospitalizationInfo">Госпитализация, которую надо скопировать</param>
        /// <param name="patient">Пациент, к которому относится госпитализация</param>
        public void Copy(CHospitalization hospitalizationInfo, CPatient patient)
        {
            var newHospitalization = new CHospitalization(hospitalizationInfo) 
            { 
                Id = GetNewID() 
            };

            do
            {
                newHospitalization.DeliveryDate = newHospitalization.DeliveryDate.AddDays(1);
            }
            while (GetByGeneralData(
                newHospitalization.DeliveryDate,
                patient.GetFullName(),
                patient.Nosology,
                newHospitalization.Id) != null);

            _hospitalizationList.Add(newHospitalization);

            CopyHospitalizationsEssenses(
                hospitalizationInfo.Id,
                newHospitalization.Id,
                hospitalizationInfo.PatientId);

            Save();
        }


        /// <summary>
        /// Скопировать все госпитализации для указанного пациента
        /// </summary>
        /// <param name="patientId">Id копируемого пациента</param>
        /// <param name="newPatientId">Id нового пациента</param>
        public void CopyByPatientId(int patientId, int newPatientId)
        {
            foreach (CHospitalization hospitalization in GetListByPatientId(patientId))
            {
                var newHospitalization = new CHospitalization(hospitalization)
                { 
                    Id = GetNewID(), PatientId = newPatientId 
                };
                _hospitalizationList.Add(newHospitalization);

                CopyHospitalizationsEssenses(
                    hospitalization.Id,
                    newHospitalization.Id,
                    newPatientId);
            }

            Save();
        }


        /// <summary>
        /// Получить госпитализацию по дате поступления
        /// </summary>
        /// <param name="deliveryDate">Дата поступления</param>
        /// <param name="patientFIO">ФИО пациента</param>
        /// <param name="patientNosologyName">Название нозологии</param>
        /// <param name="wrongHospitalizationId">id госпитализации, которая игнорируется</param>
        /// <returns></returns>
        public CHospitalization GetByGeneralData(
            DateTime deliveryDate, string patientFIO, string patientNosologyName, int wrongHospitalizationId)
        {
            foreach (CHospitalization hospitalization in _hospitalizationList)
            {
                CPatient patient = _workersKeeper.PatientWorker.GetById(hospitalization.PatientId);

                if (CCompareEngine.CompareDateTime(hospitalization.DeliveryDate, deliveryDate) == 0 &&
                    patient.GetFullName() == patientFIO &&
                    patient.Nosology == patientNosologyName &&
                    hospitalization.Id != wrongHospitalizationId)
                {
                    return hospitalization;
                }
            }

            return null;
        }

        /// <summary>
        /// Получить все госпитализации с неправильным id пациента
        /// </summary>
        /// <param name="patientWorker">Объект для работы с пациентами</param>
        /// <returns></returns>
        public List<CHospitalization> GetWrongHospitalizations(CPatientWorker patientWorker)
        {
            var wrongHospitalizations = new List<CHospitalization>();
            foreach (CHospitalization hospitalization in _hospitalizationList)
            {
                try
                {
                    patientWorker.GetById(hospitalization.PatientId);
                }
                catch
                {
                    wrongHospitalizations.Add(hospitalization);
                }
            }

            return wrongHospitalizations;
        }
    }
}
