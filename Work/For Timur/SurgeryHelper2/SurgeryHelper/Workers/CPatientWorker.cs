using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CPatientWorker : CBaseWorker
    {
        private List<CPatient> _patientList;
        private readonly string _patientPath;
        private readonly CWorkersKeeper _workersKeeper;
        private readonly CConfigurationEngine _configurationEngine;

        public CPatientWorker(CWorkersKeeper workersKeeper, string dataPath)
        {
            _patientPath = Path.Combine(dataPath, "patients.save");
            _workersKeeper = workersKeeper;
            _configurationEngine = _workersKeeper.ConfigurationEngine;
            Load();
        }


        /// <summary>
        /// Получить новый id для пациента
        /// </summary>
        /// <returns></returns>
        public int GetNewID()
        {
            return GetNewID(_patientList.ToArray());
        }


        /// <summary>
        /// Добавить информацию о пациенте без сохранения в базу
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void AddWithoutSaving(CPatient patientInfo)
        {
            _patientList.Add(patientInfo);
        }


        /// <summary>
        /// Обновить информацию о пациенте
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void Update(CPatient patientInfo)
        {
            CPatient tempPatient = GetByGeneralData(
                patientInfo.GetFullName(),
                patientInfo.Nosology,
                patientInfo.Id);
            if (tempPatient != null)
            {
                throw new Exception("Пациент с такими ФИО и нозологией уже содержится в базе. Измените ФИО или нозологию пациента.");
            }

            int n = 0;
            while (n < _patientList.Count && _patientList[n].Id != patientInfo.Id)
            {
                n++;
            }

            if (n == _patientList.Count)
            {
                _patientList.Add(patientInfo);
            }
            else
            {
                _patientList[n] = patientInfo;
            }

            Save();
        }


        /// <summary>
        /// Удалить пациента
        /// </summary>
        /// <param name="patientId">Id пациента</param>
        public void Remove(int patientId)
        {
            int index = 0;
            while (index < _patientList.Count)
            {
                if (_patientList[index].Id == patientId)
                {
                    _patientList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            _workersKeeper.HospitalizationWorker.RemoveByPatientId(patientId);
            _workersKeeper.VisitWorker.RemoveByPatientId(patientId);
            _workersKeeper.AnamneseWorker.Remove(patientId);
            _workersKeeper.ObstetricHistoryWorker.Remove(patientId);

            Save();
        }


        /// <summary>
        /// Сохранить список пациентов
        /// </summary>
        private void Save()
        {
            _patientList.Sort(CPatient.Compare);

            var patientsStr = new StringBuilder();

            foreach (CPatient patientInfo in _patientList)
            {
                patientsStr.Append(
                    "Birthday=" + CConvertEngine.DateTimeToString(patientInfo.Birthday) + DataSplitStr +
                    "BuildingNumber=" + patientInfo.BuildingNumber + DataSplitStr +
                    "CityName=" + patientInfo.CityName + DataSplitStr +
                    "FlatNumber=" + patientInfo.FlatNumber + DataSplitStr +
                    "HomeNumber=" + patientInfo.HomeNumber + DataSplitStr +
                    "Id=" + patientInfo.Id + DataSplitStr +
                    "LastName=" + patientInfo.LastName + DataSplitStr +
                    "Name=" + patientInfo.Name + DataSplitStr +
                    "NosologyList=" + CConvertEngine.ListToString(patientInfo.NosologyList) + DataSplitStr +
                    "Patronymic=" + patientInfo.Patronymic + DataSplitStr +
                    "Phone=" + patientInfo.Phone + DataSplitStr +
                    "PrivateFolder=" + patientInfo.PrivateFolder + DataSplitStr +
                    "Relatives=" + patientInfo.Relatives + DataSplitStr +
                    "IsSpecifyLegalRepresent=" + patientInfo.IsSpecifyLegalRepresent + DataSplitStr +
                    "LegalRepresent=" + patientInfo.LegalRepresent + DataSplitStr +
                    "WorkPlace=" + patientInfo.WorkPlace + DataSplitStr +
                    "EMail=" + patientInfo.EMail + DataSplitStr +
                    "InsuranceSeries=" + patientInfo.InsuranceSeries + DataSplitStr +
                    "InsuranceNumber=" + patientInfo.InsuranceNumber + DataSplitStr +
                    "InsuranceType=" + patientInfo.InsuranceType + DataSplitStr +
                    "InsuranceName=" + patientInfo.InsuranceName + DataSplitStr +
                    "PassInformation=" + patientInfo.PassInformation + DataSplitStr +
                    "StreetName=" + patientInfo.StreetName + ObjSplitStr);
            }

            CDatabaseEngine.PackText(patientsStr.ToString(), _patientPath);
        }


        /// <summary>
        /// Загрузить список пациентов
        /// </summary>
        private void Load()
        {
            _patientList = new List<CPatient>();
            string allDataStr = CDatabaseEngine.UnpackText(_patientPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var patientInfo = new CPatient();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Birthday":
                            patientInfo.Birthday = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "BuildingNumber":
                            patientInfo.BuildingNumber = keyValue[1];
                            break;
                        case "CityName":
                            patientInfo.CityName = keyValue[1];
                            break;
                        case "FlatNumber":
                            patientInfo.FlatNumber = keyValue[1];
                            break;
                        case "HomeNumber":
                            patientInfo.HomeNumber = keyValue[1];
                            break;
                        case "Id":
                            patientInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastName":
                            patientInfo.LastName = keyValue[1];
                            break;
                        case "Name":
                            patientInfo.Name = keyValue[1];
                            break;
                        case "Nosology":
                            patientInfo.NosologyList = new List<string> { keyValue[1] };
                            break;
                        case "NosologyList":
                            patientInfo.NosologyList = CConvertEngine.StringToStringList(keyValue[1]);
                            break;
                        case "Patronymic":
                            patientInfo.Patronymic = keyValue[1];
                            break;
                        case "Phone":
                            patientInfo.Phone = keyValue[1];
                            break;
                        case "PrivateFolder":
                            patientInfo.PrivateFolder = keyValue[1];
                            break;
                        case "Relatives":
                            patientInfo.Relatives = keyValue[1];
                            break;
                        case "IsSpecifyLegalRepresent":
                            patientInfo.IsSpecifyLegalRepresent = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "LegalRepresent":
                            patientInfo.LegalRepresent = keyValue[1];
                            break;
                        case "WorkPlace":
                            patientInfo.WorkPlace = keyValue[1];
                            break;
                        case "EMail":
                            patientInfo.EMail = keyValue[1];
                            break;
                        case "InsuranceSeries":
                            patientInfo.InsuranceSeries = keyValue[1];
                            break;
                        case "InsuranceNumber":
                            patientInfo.InsuranceNumber = keyValue[1];
                            break;
                        case "InsuranceType":
                            patientInfo.InsuranceType = keyValue[1];
                            break;
                        case "InsuranceName":
                            patientInfo.InsuranceName = keyValue[1];
                            break;
                        case "PassInformation":
                            patientInfo.PassInformation = new CPassportInformation(keyValue[1]);
                            break;
                        case "StreetName":
                            patientInfo.StreetName = keyValue[1];
                            break;
                    }
                }

                patientInfo.Nosology = _workersKeeper.NosologyWorker.GetNosologyDisplayName(patientInfo.NosologyList);
                _patientList.Add(patientInfo);
            }
        }


        /// <summary>
        /// Получить список пациентов
        /// </summary>
        public CPatient[] PatientList
        {
            get
            {
                return _patientList.ToArray();
            }
        }


        /// <summary>
        /// Получить список пациентов, где сначала идут зелёные, потом серые, потом белые пациенты
        /// </summary>
        public CPatientView[] PatientListView
        {
            get
            {
                var patientListViewInit = new CPatientView[_patientList.Count];
                for (int i = 0; i < _patientList.Count; i++)
                {
                    patientListViewInit[i] = new CPatientView(_patientList[i], _workersKeeper);
                }

                switch (_configurationEngine.PatientFormFilterColumnName)
                {
                    case "id":
                        Array.Sort(patientListViewInit, CPatientView.CompareById);
                        break;
                    case "DeliveryDate":
                        Array.Sort(patientListViewInit, CPatientView.CompareByDeliveryDate);
                        break;
                    case "VisitDate":
                        Array.Sort(patientListViewInit, CPatientView.CompareByVisitDate);
                        break;
                    default:
                        Array.Sort(patientListViewInit, CPatientView.CompareByName);
                        break;
                }

                if (_configurationEngine.PatientFormFilterDirection == SortOrder.Descending)
                {
                    Array.Reverse(patientListViewInit);
                }

                if (_configurationEngine.PatientFormFilterColumnName == "id")
                {
                    return patientListViewInit;
                }

                Color lightColor = _configurationEngine.RowLightColor;
                Color releaseDateColor = _configurationEngine.RowReleaseDateColor;
                Color noColor = _configurationEngine.RowNoColor;
                Color lineOfCommunicationColor = _configurationEngine.RowLineOfCommunicationColor;

                var patientListView = new CPatientView[patientListViewInit.Length];
                int cnt = 0;

                var addedPatientIds = new List<int>();
                // Добавляем в массив пациентов только тех, у которых дата выписки - сегодня
                foreach (CPatientView patientInfoView in patientListViewInit)
                {
                    int patientId = Convert.ToInt32(patientInfoView.Id);
                    foreach (CHospitalization hospitalizationInfo in _workersKeeper.HospitalizationWorker.GetListByPatientId(patientId))
                    {
                        if (hospitalizationInfo.ReleaseDate.HasValue &&
                                CCompareEngine.CompareDate(hospitalizationInfo.ReleaseDate.Value, DateTime.Now) == 0)
                        {
                            patientListView[cnt] = patientInfoView;
                            patientListView[cnt].RowColor = releaseDateColor;

                            addedPatientIds.Add(patientId);
                            cnt++;
                            break;
                        }
                    }
                }

                // Добавляем в массив пациентов только тех, у которых дата выписки 
                // не указана или в будущем
                foreach (CPatientView patientInfoView in patientListViewInit)
                {
                    int patientId = Convert.ToInt32(patientInfoView.Id);
                    if (addedPatientIds.Contains(patientId))
                    {
                        continue;
                    }

                    foreach (CHospitalization hospitalizationInfo in _workersKeeper.HospitalizationWorker.GetListByPatientId(patientId))
                    {
                        if (!hospitalizationInfo.ReleaseDate.HasValue ||
                            CCompareEngine.CompareDate(hospitalizationInfo.ReleaseDate.Value, DateTime.Now) > 0)
                        {
                            patientListView[cnt] = patientInfoView;
                            DateTime tempDate = DateTime.Now.AddDays(-14);
                            while (CCompareEngine.CompareDate(tempDate, hospitalizationInfo.DeliveryDate) > 0)
                            {
                                tempDate = tempDate.AddDays(-14);
                            }

                            if (CCompareEngine.CompareDate(tempDate, hospitalizationInfo.DeliveryDate) == 0)
                            {
                                patientListView[cnt].RowColor = lineOfCommunicationColor;
                                patientListView[cnt].IsNeedWriteLineOfCommEpicris = true;
                            }
                            else
                            {
                                patientListView[cnt].RowColor = lightColor;
                            }

                            addedPatientIds.Add(patientId);
                            cnt++;
                            break;
                        }
                    }
                }

                // Добавляем в массив пациентов только тех, у которых дата выписки 
                // в прошлом
                foreach (CPatientView patientInfoView in patientListViewInit)
                {
                    int patientId = Convert.ToInt32(patientInfoView.Id);
                    if (addedPatientIds.Contains(patientId))
                    {
                        continue;
                    }

                    bool allReleaseDateArePast = true;
                    foreach (CHospitalization hospitalizationInfo in _workersKeeper.HospitalizationWorker.GetListByPatientId(patientId))
                    {
                        if (!hospitalizationInfo.ReleaseDate.HasValue ||
                            CCompareEngine.CompareDate(hospitalizationInfo.ReleaseDate.Value, DateTime.Now) >= 0)
                        {
                            allReleaseDateArePast = false;
                            break;
                        }
                    }

                    if (allReleaseDateArePast)
                    {
                        patientListView[cnt] = patientInfoView;
                        patientListView[cnt].RowColor = noColor;
                        cnt++;
                    }
                }

                return patientListView;
            }
        }


        /// <summary>
        /// Получить пациента по id
        /// </summary>
        /// <param name="patientId">ID пациента</param>
        /// <returns></returns>
        public CPatient GetById(int patientId)
        {
            foreach (CPatient patient in _patientList)
            {
                if (patient.Id == patientId)
                {
                    return patient;
                }
            }

            throw new ArgumentException("Внутренняя ошибка программы. Не найден пациент с id=" + patientId);
        }


        /// <summary>
        /// Получить список пациентов, у которых прописана указанная нозология
        /// </summary>
        /// <param name="nosology">Нозология</param>
        /// <returns></returns>
        public int GetCountContainedNosology(string nosology)
        {
            int cnt = 0;

            foreach (CPatient patientInfo in _patientList)
            {
                if (patientInfo.NosologyList.Contains(nosology))
                {
                    cnt++;
                }
            }

            return cnt;
        }


        /// <summary>
        /// Изменить id нозологии у всех пациентов
        /// </summary>
        /// <param name="oldNosology">Старая нозология (которая удаляется/изменяется)</param>
        /// <param name="newNosology">Новая нозология (которая выбрана вместо удаляемой/изменяемой)</param>
        public void ChangeNosology(string oldNosology, string newNosology)
        {
            for (int i = 0; i < _patientList.Count; i++)
            {
                for (int j = 0; j < _patientList[i].NosologyList.Count; j++)
                {
                    if (_patientList[i].NosologyList[j] == oldNosology)
                    {
                        _patientList[i].NosologyList[j] = newNosology;
                        break;
                    }
                }

                _patientList[i].Nosology = _workersKeeper.NosologyWorker.GetNosologyDisplayName(_patientList[i].NosologyList);
            }

            Save();
        }


        /// <summary>
        /// Скопировать данные о пациенте в переданного пациента
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>        
        public void Copy(CPatient patientInfo)
        {
            var newPatient = new CPatient(patientInfo)
            {
                Id = GetNewID()
            };

            do
            {
                newPatient.LastName += "_copy";
            }
            while (GetByGeneralData(newPatient.GetFullName(), newPatient.Nosology, -1) != null);

            Update(newPatient);

            _workersKeeper.HospitalizationWorker.CopyByPatientId(patientInfo.Id, newPatient.Id);
            _workersKeeper.VisitWorker.CopyByPatientId(patientInfo.Id, newPatient.Id);
        }


        /// <summary>
        /// Получить пациента с указанным ФИО и нозологией
        /// </summary>
        /// <param name="fullName">ФИО пациента</param>
        /// <param name="nosology">Нозология</param>
        /// <param name="wrongPatientId">id пациента, который игнорируется</param>
        /// <returns></returns>
        public CPatient GetByGeneralData(string fullName, string nosology, int wrongPatientId)
        {
            foreach (CPatient patient in _patientList)
            {
                if (patient.GetFullName() == fullName &&
                    patient.Nosology == nosology &&
                    patient.Id != wrongPatientId)
                {
                    return patient;
                }
            }

            return null;
        }


        /// <summary>
        /// Получить всех пациентов с одинаковым именем и нозологией
        /// </summary>
        /// <returns></returns>
        public List<CPatient> GetDubbedPatients()
        {
            var dubbedPatients = new List<CPatient>();
            foreach (CPatient patient in _patientList)
            {
                if (GetByGeneralData(patient.GetFullName(), patient.Nosology, patient.Id) != null)
                {
                    dubbedPatients.Add(patient);
                }
            }

            return dubbedPatients;
        }


        /// <summary>
        /// Получить всех пациентов без имени или без нозологии
        /// </summary>
        /// <returns></returns>
        public List<CPatient> GetWrongPatients()
        {
            var wrongPatients = new List<CPatient>();
            foreach (CPatient patient in _patientList)
            {
                if (string.IsNullOrEmpty(patient.GetFullName().Trim(' ')) || string.IsNullOrEmpty(patient.Nosology))
                {
                    wrongPatients.Add(patient);
                }
            }

            return wrongPatients;
        }
    }
}
