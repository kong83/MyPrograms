using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CTransferableEpicrisisWorker : CBaseWorker
    {
        private List<CTransferableEpicrisis> _transferableEpicrisisList;
        private readonly string _transferableEpicrisisPath;

        public CTransferableEpicrisisWorker(string dataPath)
        {
            _transferableEpicrisisPath = Path.Combine(dataPath, "transferable_epicrisis.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию о переводном эпикризе
        /// </summary>
        /// <param name="transferableEpicrisisInfo">Информация о переводном эпикризе</param>
        public void Update(CTransferableEpicrisis transferableEpicrisisInfo)
        {
            int n = GetIndexFromList(transferableEpicrisisInfo.HospitalizationId);
            transferableEpicrisisInfo.NotInDatabase = false;
            _transferableEpicrisisList[n] = new CTransferableEpicrisis(transferableEpicrisisInfo);
            Save();
        }


        /// <summary>
        /// Удалить переводной эпикриз
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        public void Remove(int hospitalizationId)
        {
            int index = 0;
            while (index < _transferableEpicrisisList.Count)
            {
                if (_transferableEpicrisisList[index].HospitalizationId == hospitalizationId)
                {
                    _transferableEpicrisisList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список переводных эпикризов
        /// </summary>
        private void Save()
        {
            var transferableEpicrisissStr = new StringBuilder();

            foreach (CTransferableEpicrisis transferableEpicrisisInfo in _transferableEpicrisisList)
            {
                transferableEpicrisissStr.Append(
                    "HospitalizationId=" + transferableEpicrisisInfo.HospitalizationId + DataSplitStr +
                    "AfterOperationPeriod=" + transferableEpicrisisInfo.AfterOperationPeriod + DataSplitStr +
                    "Plan=" + transferableEpicrisisInfo.Plan + DataSplitStr +
                    "WritingDate=" + CConvertEngine.DateTimeToString(transferableEpicrisisInfo.WritingDate) + DataSplitStr +
                    "AdditionalInfo=" + transferableEpicrisisInfo.AdditionalInfo + DataSplitStr +
                    "DisabilityList=" + transferableEpicrisisInfo.DisabilityList + DataSplitStr +
                    "IsIncludeDisabilityList=" + transferableEpicrisisInfo.IsIncludeDisabilityList + ObjSplitStr);
            }

            CDatabaseEngine.PackText(transferableEpicrisissStr.ToString(), _transferableEpicrisisPath);
        }


        /// <summary>
        /// Загрузить список переводных эпикризов
        /// </summary>
        private void Load()
        {
            _transferableEpicrisisList = new List<CTransferableEpicrisis>();
            string allDataStr = CDatabaseEngine.UnpackText(_transferableEpicrisisPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var transferableEpicrisisInfo = new CTransferableEpicrisis();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "HospitalizationId":
                            transferableEpicrisisInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "AfterOperationPeriod":
                            transferableEpicrisisInfo.AfterOperationPeriod = keyValue[1];
                            break;
                        case "Plan":
                            transferableEpicrisisInfo.Plan = keyValue[1];
                            break;
                        case "WritingDate":
                            transferableEpicrisisInfo.WritingDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "AdditionalInfo":
                            transferableEpicrisisInfo.AdditionalInfo = keyValue[1];
                            break;
                        case "DisabilityList":
                            transferableEpicrisisInfo.DisabilityList = keyValue[1];
                            break;
                        case "IsIncludeDisabilityList":
                            transferableEpicrisisInfo.IsIncludeDisabilityList = Convert.ToBoolean(keyValue[1]);
                            break;
                    }
                }

                _transferableEpicrisisList.Add(transferableEpicrisisInfo);
            }
        }


        /// <summary>
        /// Получить индекс переводного эпикриза в списке для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        private int GetIndexFromList(int hospitalizationId)
        {
            int n = 0;
            while (n < _transferableEpicrisisList.Count &&
                _transferableEpicrisisList[n].HospitalizationId != hospitalizationId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли переводной эпикриз для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public bool IsExists(int hospitalizationId)
        {
            return GetIndexFromList(hospitalizationId) != _transferableEpicrisisList.Count;
        }


        /// <summary>
        /// Получить переводной эпикриз по переданному id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public CTransferableEpicrisis GetByHospitalizationId(int hospitalizationId)
        {
            int n = GetIndexFromList(hospitalizationId);

            if (n == _transferableEpicrisisList.Count)
            {
                var newTransferableEpicrisisInfo = new CTransferableEpicrisis(hospitalizationId)
                {
                    NotInDatabase = true
                };
                _transferableEpicrisisList.Add(newTransferableEpicrisisInfo);
                return new CTransferableEpicrisis(newTransferableEpicrisisInfo);
            }

            return new CTransferableEpicrisis(_transferableEpicrisisList[n]);
        }


        /// <summary>
        /// Скопировать все переводные эпикризы для указанной госпитализации
        /// </summary>
        /// <param name="hospitalizationId">Id копируемой госпитализации</param>
        /// <param name="newHospitalizationId">Id нового госпитализации</param>
        public void CopyByHospitalizationId(int hospitalizationId, int newHospitalizationId)
        {
            CTransferableEpicrisis newTransferableEpicrisis = GetByHospitalizationId(hospitalizationId);
            newTransferableEpicrisis.HospitalizationId = newHospitalizationId;
            _transferableEpicrisisList.Add(newTransferableEpicrisis);
            Save();
        }


        /// <summary>
        /// Получить все переводные эпикиирзы с неправильным id госпитализации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <returns></returns>
        public List<CTransferableEpicrisis> GetWrongTransferableEpicrisis(CHospitalizationWorker hospitalizationWorker)
        {
            var wrongTransferableEpicrisiss = new List<CTransferableEpicrisis>();
            foreach (CTransferableEpicrisis transferableEpicrisis in _transferableEpicrisisList)
            {
                try
                {
                    hospitalizationWorker.GetById(transferableEpicrisis.HospitalizationId);
                }
                catch
                {
                    wrongTransferableEpicrisiss.Add(transferableEpicrisis);
                }
            }

            return wrongTransferableEpicrisiss;
        }
    }
}
