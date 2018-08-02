using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class COperationWorker : CBaseWorker
    {
        private readonly CWorkersKeeper _workersKeeper;
        private List<COperation> _operationList;
        private readonly string _operationPath;

        public COperationWorker(CWorkersKeeper workersKeeper, string dataPath)
        {
            _workersKeeper = workersKeeper;
            _operationPath = Path.Combine(dataPath, "operations.save");
            Load();
        }


        /// <summary>
        /// Получить новый id для госпитализации
        /// </summary>
        /// <returns></returns>
        public int GetNewID()
        {
            return GetNewID(_operationList.ToArray());
        }


        /// <summary>
        /// Добавить информацию об операции без сохранения в базу
        /// </summary>
        /// <param name="operationInfo">Информация об операции</param>
        public void AddWithoutSaving(COperation operationInfo)
        {
            _operationList.Add(operationInfo);
        }


        /// <summary>
        /// Изменение данных об операции
        /// </summary>
        /// <param name="operationInfo">Информация про операцию</param>
        public void Update(COperation operationInfo)
        {
            CPatient patientInfo = _workersKeeper.PatientWorker.GetById(operationInfo.PatientId);
            CHospitalization hospitalizationInfo = _workersKeeper.HospitalizationWorker.GetById(operationInfo.HospitalizationId);
            
            COperation tempOperation = GetByGeneralData(
                operationInfo.Name,
                hospitalizationInfo.DeliveryDate,
                patientInfo.GetFullName(),
                patientInfo.Nosology,
                operationInfo.Id);
            if (tempOperation != null)
            {
                throw new Exception("Операция с таким названием для этой госпитализации уже содержится в базе. Измените название операции.");
            }

            int n = 0;
            while (n < _operationList.Count && _operationList[n].Id != operationInfo.Id)
            {
                n++;
            }

            if (n == _operationList.Count)
            {
                _operationList.Add(operationInfo);
            }
            else
            {
                _operationList[n] = operationInfo;
            }

            Save();
        }


        /// <summary>
        /// Удалить операцию
        /// </summary>
        /// <param name="operationId">ID операции</param>
        public void Remove(int operationId)
        {
            int index = 0;
            while (index < _operationList.Count)
            {
                if (_operationList[index].Id == operationId)
                {
                    _operationList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            _workersKeeper.OperationProtocolWorker.Remove(operationId);

            Save();
        }


        /// <summary>
        /// Удалить все операции для удаляемой госпитализации
        /// </summary>
        /// <param name="hospitalizationId">Id госпитализации</param>
        public void RemoveByHospitalizationId(int hospitalizationId)
        {
            int index = 0;
            while (index < _operationList.Count)
            {
                if (_operationList[index].HospitalizationId == hospitalizationId)
                {
                    _workersKeeper.OperationProtocolWorker.Remove(_operationList[index].Id);
                    _operationList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список операций
        /// </summary>
        private void Save()
        {
            _operationList.Sort(COperation.Compare);

            var operationsStr = new StringBuilder();

            foreach (COperation operationInfo in _operationList)
            {
                operationsStr.Append(
                    "Id=" + operationInfo.Id + DataSplitStr +
                    "HospitalizationId=" + operationInfo.HospitalizationId + DataSplitStr +
                    "PatientId=" + operationInfo.PatientId + DataSplitStr +
                    "DateOfOperation=" + CConvertEngine.DateTimeToString(operationInfo.DateOfOperation) + DataSplitStr +
                    "StartTimeOfOperation=" + CConvertEngine.DateTimeToString(operationInfo.StartTimeOfOperation, true) + DataSplitStr +
                    "EndTimeOfOperation=" + CConvertEngine.DateTimeToString(operationInfo.EndTimeOfOperation, true) + DataSplitStr +
                    "Name=" + operationInfo.Name + DataSplitStr +                    
                    "Surgeons=" + CConvertEngine.ListToString(operationInfo.Surgeons) + DataSplitStr +
                    "Assistents=" + CConvertEngine.ListToString(operationInfo.Assistents) + DataSplitStr +
                    "OperationTypes=" + CConvertEngine.ListToString(operationInfo.OperationTypes) + DataSplitStr +
                    "HeAnaesthetist=" + operationInfo.HeAnaesthetist + DataSplitStr +
                    "SheAnaesthetist=" + operationInfo.SheAnaesthetist + DataSplitStr +
                    "ScrubNurse=" + operationInfo.ScrubNurse + DataSplitStr +
                    "Orderly=" + operationInfo.Orderly + ObjSplitStr);
            }

            CDatabaseEngine.PackText(operationsStr.ToString(), _operationPath);
        }

        /// <summary>
        /// Загрузить список операций
        /// </summary>
        private void Load()
        {
            _operationList = new List<COperation>();
            string allDataStr = CDatabaseEngine.UnpackText(_operationPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var operationInfo = new COperation();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            operationInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "HospitalizationId":
                            operationInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "PatientId":
                            operationInfo.PatientId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "DateOfOperation":
                            operationInfo.DateOfOperation = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "StartTimeOfOperation":
                            operationInfo.StartTimeOfOperation = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "EndTimeOfOperation":
                            if (string.IsNullOrEmpty(keyValue[1]))
                            {
                                operationInfo.EndTimeOfOperation = null;
                            }
                            else
                            {
                                operationInfo.EndTimeOfOperation = CConvertEngine.StringToDateTime(keyValue[1]);
                            }

                            break;
                        case "Name":
                            operationInfo.Name = keyValue[1];
                            break;
                        case "Surgeons":
                            operationInfo.Surgeons = CConvertEngine.StringToStringList(keyValue[1]);
                            break;
                        case "Assistents":
                            operationInfo.Assistents = CConvertEngine.StringToStringList(keyValue[1]);
                            break;
                        case "OperationTypes":
                            operationInfo.OperationTypes = CConvertEngine.StringToStringList(keyValue[1]);
                            break;
                        case "HeAnaesthetist":
                            operationInfo.HeAnaesthetist = keyValue[1];
                            break;
                        case "SheAnaesthetist":
                            operationInfo.SheAnaesthetist = keyValue[1];
                            break;
                        case "ScrubNurse":
                            operationInfo.ScrubNurse = keyValue[1];
                            break;
                        case "Orderly":
                            operationInfo.Orderly = keyValue[1];
                            break;
                    }
                }

                _operationList.Add(operationInfo);
            }
        }


        /// <summary>
        /// Получить список операций для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public int GetCountByHospitalizationId(int hospitalizationId)
        {
            int result = 0;

            foreach (COperation operation in _operationList)
            {
                if (operation.HospitalizationId == hospitalizationId)
                {
                    result++;
                }
            }

            return result;
        }


        /// <summary>
        /// Получить список операций для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public int GetCountByPatientId(int patientId)
        {
            int result = 0;

            foreach (COperation operation in _operationList)
            {
                if (operation.PatientId == patientId)
                {
                    result++;
                }
            }

            return result;
        }


        /// <summary>
        /// Получить список операций для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public COperation[] GetListByPatientId(int patientId)
        {
            var operations = new List<COperation>();

            foreach (COperation operation in _operationList)
            {
                if (operation.PatientId == patientId)
                {
                    operations.Add(operation);
                }
            }

            return operations.ToArray();
        }


        /// <summary>
        /// Получить список операций для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public COperation[] GetListByHospitalizationId(int hospitalizationId)
        {
            var operations = new List<COperation>();

            foreach (COperation operation in _operationList)
            {
                if (operation.HospitalizationId == hospitalizationId)
                {
                    operations.Add(operation);
                }
            }

            return operations.ToArray();
        }


        /// <summary>
        /// Получить операцию по указанному id операции
        /// </summary>
        /// <param name="operationId">id операци</param>
        /// <returns></returns>
        public COperation GetById(int operationId)
        {
            foreach (COperation operation in _operationList)
            {
                if (operation.Id == operationId)
                {
                    return operation;
                }
            }

            throw new ArgumentException("Внутренняя ошибка программы. Не найдена операция с id=" + operationId);
        }


        /// <summary>
        /// Скопировать все операции для указанного пациента и госпитализации
        /// </summary>
        /// <param name="hospitalizationId">Id копируемой госпитализации</param>
        /// <param name="newHospitalizationId">Id новой госпитализации</param>
        /// <param name="newPatientId">Id нового пациента</param>
        public void CopyByHospitalizationId(
            int hospitalizationId, int newHospitalizationId, int newPatientId)
        {
            foreach (COperation operation in GetListByHospitalizationId(hospitalizationId))
            {
                var newOperation = new COperation(operation)
                { 
                    Id = GetNewID(), 
                    PatientId = newPatientId, 
                    HospitalizationId = newHospitalizationId 
                };
                _operationList.Add(newOperation);

                _workersKeeper.OperationProtocolWorker.CopyByOperationId(
                                    operation.Id, newOperation.Id);
            }

            Save();
        }


        /// <summary>
        /// Получить операцию с указанным названием
        /// </summary>
        /// <param name="name">Название операции</param>
        /// <param name="hospitalizationDeliveryDate">Дата госпитализации</param>
        /// <param name="patientFIO">ФИО пациента</param>
        /// <param name="patientNosologyName">Название нозологии</param>
        /// <param name="wrongOperationId">id операции, которая игнорируется</param>
        /// <returns></returns>
        public COperation GetByGeneralData(            
            string name, 
            DateTime hospitalizationDeliveryDate, 
            string patientFIO, 
            string patientNosologyName,
            int wrongOperationId)
        {
            foreach (COperation operation in _operationList)
            {
                CHospitalization hospitalization = _workersKeeper.HospitalizationWorker.GetById(operation.HospitalizationId);
                CPatient patient = _workersKeeper.PatientWorker.GetById(hospitalization.PatientId);

                if (operation.Name == name &&
                    CCompareEngine.CompareDateTime(hospitalization.DeliveryDate, hospitalizationDeliveryDate) == 0 &&
                    patient.GetFullName() == patientFIO &&
                    patient.Nosology == patientNosologyName &&
                    operation.Id != wrongOperationId)
                {
                    return operation;
                }
            }

            return null;
        }


        /// <summary>
        /// Получить количество операции, у которых прописан указанный тип операции в списке
        /// типов операций
        /// </summary>
        /// <param name="searchedOperationType">Тип операции</param>
        /// <returns></returns>
        public int GetCountContainedOperationType(string searchedOperationType)
        {
            int cnt = 0;
            foreach (COperation operation in _operationList)
            {
                if (operation.OperationTypes.Contains(searchedOperationType)) 
                {
                    cnt++;
                }
            }

            return cnt;
        }


        /// <summary>
        /// Изменить удаляемый тип операции у всех операций на указанный тип операции, если
        /// этот тип операции ещё не прописан у данной операции
        /// </summary>
        /// <param name="odlOperationType">Старый тип операции</param>
        /// <param name="newOperationType">Новый тип операции</param>
        public void ChangeOperationType(string odlOperationType, string newOperationType)
        {
            foreach (COperation operation in _operationList)
            {
                if (operation.OperationTypes.Contains(odlOperationType))
                {
                    operation.OperationTypes.Remove(odlOperationType);

                    if (!operation.OperationTypes.Contains(newOperationType))
                    {
                        operation.OperationTypes.Add(newOperationType);
                    }
                }
            }

            Save();
        }


        /// <summary>
        /// Получить все операции с неправильным id пациента или id госпитализации
        /// </summary>
        /// <param name="patientWorker">Объект для работы с пациентами</param>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <returns></returns>
        public List<COperation> GetWrongOperations(CPatientWorker patientWorker, CHospitalizationWorker hospitalizationWorker)
        {
            var wrongOperations = new List<COperation>();
            foreach (COperation operation in _operationList)
            {
                try
                {
                    patientWorker.GetById(operation.PatientId);
                    hospitalizationWorker.GetById(operation.HospitalizationId);
                }
                catch
                {
                    wrongOperations.Add(operation);
                }
            }

            return wrongOperations;
        }
    }
}
