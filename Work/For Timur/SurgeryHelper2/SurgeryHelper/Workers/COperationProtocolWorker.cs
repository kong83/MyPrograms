using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class COperationProtocolWorker : CBaseWorker
    {
        private List<COperationProtocol> _operationProtocolList;
        private readonly string _operationProtocolPath;

        public COperationProtocolWorker(string dataPath)
        {
            _operationProtocolPath = Path.Combine(dataPath, "operation_protocol.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию об операционном протоколе
        /// </summary>
        /// <param name="operationProtocolInfo">Информация об операционном протоколе</param>
        public void Update(COperationProtocol operationProtocolInfo)
        {
            int n = GetIndexFromList(operationProtocolInfo.OperationId);
            operationProtocolInfo.NotInDatabase = false;
            _operationProtocolList[n] = new COperationProtocol(operationProtocolInfo);
            Save();
        }


        /// <summary>
        /// Удалить операционный протокол
        /// </summary>
        /// <param name="operationId">id операции</param>
        public void Remove(int operationId)
        {
            int index = 0;
            while (index < _operationProtocolList.Count)
            {
                if (_operationProtocolList[index].OperationId == operationId)
                {
                    _operationProtocolList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            
            Save();
        }


        /// <summary>
        /// Сохранить список операционных протоколов
        /// </summary>
        private void Save()
        {
            var operationProtocolsStr = new StringBuilder();

            foreach (COperationProtocol operationProtocolInfo in _operationProtocolList)
            {
                operationProtocolsStr.Append(
                    "OperationId=" + operationProtocolInfo.OperationId + DataSplitStr +
                    "OperationCourse=" + operationProtocolInfo.OperationCourse + DataSplitStr +
                    "TreatmentPlanDate=" + CConvertEngine.DateTimeToString(operationProtocolInfo.TreatmentPlanDate) + DataSplitStr +
                    "TreatmentPlanInspection=" + operationProtocolInfo.TreatmentPlanInspection + DataSplitStr +
                    "IsTreatmentPlanActiveInOperationProtocol=" + operationProtocolInfo.IsTreatmentPlanActiveInOperationProtocol + DataSplitStr + 
                    "ADFirst=" + operationProtocolInfo.ADFirst + DataSplitStr +
                    "ADSecond=" + operationProtocolInfo.ADSecond + DataSplitStr +
                    "Breath=" + operationProtocolInfo.Breath + DataSplitStr +
                    "ChDD=" + operationProtocolInfo.ChDD + DataSplitStr +
                    "Complaints=" + operationProtocolInfo.Complaints + DataSplitStr +
                    "State=" + operationProtocolInfo.State + DataSplitStr +
                    "HeartRhythm=" + operationProtocolInfo.HeartRhythm + DataSplitStr +
                    "HeartSounds=" + operationProtocolInfo.HeartSounds + DataSplitStr +
                    "IsDairyEnabled=" + operationProtocolInfo.IsDairyEnabled + DataSplitStr +
                    "Pulse=" + operationProtocolInfo.Pulse + DataSplitStr +
                    "StLocalis=" + operationProtocolInfo.StLocalis + DataSplitStr +
                    "Stomach=" + operationProtocolInfo.Stomach + DataSplitStr +
                    "Stool=" + operationProtocolInfo.Stool + DataSplitStr +
                    "Temperature=" + operationProtocolInfo.Temperature + DataSplitStr +
                    "TimeWriting=" + CConvertEngine.DateTimeToString(operationProtocolInfo.TimeWriting, true) + DataSplitStr +
                    "Urination=" + operationProtocolInfo.Urination + DataSplitStr +
                    "Wheeze=" + operationProtocolInfo.Wheeze + DataSplitStr + ObjSplitStr);
            }

            CDatabaseEngine.PackText(operationProtocolsStr.ToString(), _operationProtocolPath);
        }


        /// <summary>
        /// Загрузить список операционных протоколов
        /// </summary>
        private void Load()
        {
            _operationProtocolList = new List<COperationProtocol>();
            string allDataStr = CDatabaseEngine.UnpackText(_operationProtocolPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var operationProtocolInfo = new COperationProtocol();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "OperationId":
                            operationProtocolInfo.OperationId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "TreatmentPlanInspection":
                            operationProtocolInfo.TreatmentPlanInspection = keyValue[1];
                            break;
                        case "TreatmentPlanDate":
                            operationProtocolInfo.TreatmentPlanDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "IsTreatmentPlanActiveInOperationProtocol":
                            operationProtocolInfo.IsTreatmentPlanActiveInOperationProtocol = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "ADFirst":
                            operationProtocolInfo.ADFirst = Convert.ToInt32(keyValue[1]);
                            break;
                        case "ADSecond":
                            operationProtocolInfo.ADSecond = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Breath":
                            operationProtocolInfo.Breath = keyValue[1];
                            break;
                        case "ChDD":
                            operationProtocolInfo.ChDD = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Complaints":
                            operationProtocolInfo.Complaints = keyValue[1];
                            break;
                        case "State":
                            operationProtocolInfo.State = keyValue[1];
                            break;
                        case "HeartRhythm":
                            operationProtocolInfo.HeartRhythm = keyValue[1];
                            break;
                        case "HeartSounds":
                            operationProtocolInfo.HeartSounds = keyValue[1];
                            break;
                        case "IsDairyEnabled":
                            operationProtocolInfo.IsDairyEnabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "Pulse":
                            operationProtocolInfo.Pulse = Convert.ToInt32(keyValue[1]);
                            break;
                        case "StLocalis":
                            operationProtocolInfo.StLocalis = keyValue[1];
                            break;
                        case "Stomach":
                            operationProtocolInfo.Stomach = keyValue[1];
                            break;
                        case "Stool":
                            operationProtocolInfo.Stool = keyValue[1];
                            break;
                        case "Temperature":
                            operationProtocolInfo.Temperature = keyValue[1];
                            break;
                        case "TimeWriting":
                            operationProtocolInfo.TimeWriting = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "Urination":
                            operationProtocolInfo.Urination = keyValue[1];
                            break;
                        case "Wheeze":
                            operationProtocolInfo.Wheeze = keyValue[1];
                            break;
                        case "OperationCourse":
                            operationProtocolInfo.OperationCourse = keyValue[1];
                            break;
                    }
                }

                _operationProtocolList.Add(operationProtocolInfo);
            }
        }


        /// <summary>
        /// Получить индекс протокола операции в списке для указанного id операции
        /// </summary>
        /// <param name="operationId">id госпитализации</param>
        /// <returns></returns>
        private int GetIndexFromList(int operationId)
        {
            int n = 0;
            while (n < _operationProtocolList.Count && _operationProtocolList[n].OperationId != operationId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Получить операционный протокол по переданному id операции
        /// </summary>
        /// <param name="operationId">id операции</param>
        /// <returns></returns>
        public COperationProtocol GetByOperationId(int operationId)
        {
            int n = GetIndexFromList(operationId);            

            if (n == _operationProtocolList.Count)
            {
                var newOperationProtocolInfo = new COperationProtocol(operationId)
                {
                    NotInDatabase = true
                };
                _operationProtocolList.Add(newOperationProtocolInfo);
                return new COperationProtocol(newOperationProtocolInfo);
            }

            return new COperationProtocol(_operationProtocolList[n]);
        }


        /// <summary>
        /// Скопировать все операционные протоколы для указанной операции
        /// </summary>
        /// <param name="operationId">Id копируемой операции</param>
        /// <param name="newOperationId">Id нового операции</param>
        public void CopyByOperationId(int operationId, int newOperationId)
        {
            COperationProtocol newOperationProtocol = GetByOperationId(operationId);
            newOperationProtocol.OperationId = newOperationId;
            _operationProtocolList.Add(newOperationProtocol);
            Save();
        }


        /// <summary>
        /// Получить все операционные протоколы с неправильным id операции
        /// </summary>
        /// <param name="operationWorker">Объект для работы с операциями</param>
        /// <returns></returns>
        public List<COperationProtocol> GetWrongOperationProtocols(COperationWorker operationWorker)
        {
            var wrongOperationProtocols = new List<COperationProtocol>();
            foreach (COperationProtocol operationProtocol in _operationProtocolList)
            {
                try
                {
                    operationWorker.GetById(operationProtocol.OperationId);
                }
                catch
                {
                    wrongOperationProtocols.Add(operationProtocol);
                }
            }

            return wrongOperationProtocols;
        }
    }
}
