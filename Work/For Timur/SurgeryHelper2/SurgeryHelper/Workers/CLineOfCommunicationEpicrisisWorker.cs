using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CLineOfCommunicationEpicrisisWorker : CBaseWorker
    {
        private List<CLineOfCommunicationEpicrisis> _lineOfCommunicationEpicrisisList;
        private readonly string _lineOfCommunicationEpicrisisPath;

        public CLineOfCommunicationEpicrisisWorker(string dataPath)
        {
            _lineOfCommunicationEpicrisisPath = Path.Combine(dataPath, "line_of_communication_epicrisis.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию об этапном эпикризе
        /// </summary>
        /// <param name="lineOfCommunicationEpicrisisInfo">Информация об этапном эпикризе</param>
        public void Update(CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisisInfo)
        {
            int n = GetIndexFromList(lineOfCommunicationEpicrisisInfo.HospitalizationId);
            lineOfCommunicationEpicrisisInfo.NotInDatabase = false;
            _lineOfCommunicationEpicrisisList[n] = new CLineOfCommunicationEpicrisis(lineOfCommunicationEpicrisisInfo);
            Save();
        }


        /// <summary>
        /// Удалить этапный эпикриз
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        public void Remove(int hospitalizationId)
        {
            int index = 0;
            while (index < _lineOfCommunicationEpicrisisList.Count)
            {
                if (_lineOfCommunicationEpicrisisList[index].HospitalizationId == hospitalizationId)
                {
                    _lineOfCommunicationEpicrisisList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список этапных эпикризов
        /// </summary>
        private void Save()
        {
            var lineOfCommunicationEpicrisissStr = new StringBuilder();

            foreach (CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisisInfo in _lineOfCommunicationEpicrisisList)
            {
                lineOfCommunicationEpicrisissStr.Append(
                    "HospitalizationId=" + lineOfCommunicationEpicrisisInfo.HospitalizationId + DataSplitStr +
                    "AdditionalInfo=" + lineOfCommunicationEpicrisisInfo.AdditionalInfo + DataSplitStr +
                    "Plan=" + lineOfCommunicationEpicrisisInfo.Plan + DataSplitStr +
                    "WritingDate=" + CConvertEngine.DateTimeToString(lineOfCommunicationEpicrisisInfo.WritingDate) + ObjSplitStr);
            }

            CDatabaseEngine.PackText(lineOfCommunicationEpicrisissStr.ToString(), _lineOfCommunicationEpicrisisPath);
        }


        /// <summary>
        /// Загрузить список этапных эпикризов
        /// </summary>
        private void Load()
        {
            _lineOfCommunicationEpicrisisList = new List<CLineOfCommunicationEpicrisis>();
            string allDataStr = CDatabaseEngine.UnpackText(_lineOfCommunicationEpicrisisPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var lineOfCommunicationEpicrisisInfo = new CLineOfCommunicationEpicrisis();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "HospitalizationId":
                            lineOfCommunicationEpicrisisInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "AdditionalInfo":
                            lineOfCommunicationEpicrisisInfo.AdditionalInfo = keyValue[1];
                            break;
                        case "Plan":
                            lineOfCommunicationEpicrisisInfo.Plan = keyValue[1];
                            break;
                        case "WritingDate":
                            lineOfCommunicationEpicrisisInfo.WritingDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                    }
                }

                _lineOfCommunicationEpicrisisList.Add(lineOfCommunicationEpicrisisInfo);
            }
        }


        /// <summary>
        /// Получить индекс этапного эпикриза в списке для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        private int GetIndexFromList(int hospitalizationId)
        {
            int n = 0;
            while (n < _lineOfCommunicationEpicrisisList.Count &&
                _lineOfCommunicationEpicrisisList[n].HospitalizationId != hospitalizationId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли этапный эпикриз для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public bool IsExists(int hospitalizationId)
        {
            return GetIndexFromList(hospitalizationId) != _lineOfCommunicationEpicrisisList.Count;
        }



        /// <summary>
        /// Получить этапный эпикриз по переданному id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public CLineOfCommunicationEpicrisis GetByHospitalizationId(int hospitalizationId)
        {
            int n = GetIndexFromList(hospitalizationId);

            if (n == _lineOfCommunicationEpicrisisList.Count)
            {
                var newLineOfCommunicationEpicrisis = new CLineOfCommunicationEpicrisis(hospitalizationId)
                {
                    NotInDatabase = true
                };
                _lineOfCommunicationEpicrisisList.Add(newLineOfCommunicationEpicrisis);
                return new CLineOfCommunicationEpicrisis(newLineOfCommunicationEpicrisis);
            }

            return new CLineOfCommunicationEpicrisis(_lineOfCommunicationEpicrisisList[n]);
        }


        /// <summary>
        /// Скопировать все этапные эпикризы для указанной госпитализации
        /// </summary>
        /// <param name="hospitalizationId">Id копируемой госпитализации</param>
        /// <param name="newHospitalizationId">Id нового госпитализации</param>
        public void CopyByHospitalizationId(int hospitalizationId, int newHospitalizationId)
        {
            CLineOfCommunicationEpicrisis newCLineOfCommunication = GetByHospitalizationId(hospitalizationId);
            newCLineOfCommunication.HospitalizationId = newHospitalizationId;
            _lineOfCommunicationEpicrisisList.Add(newCLineOfCommunication);
            Save();
        }


        /// <summary>
        /// Получить все переводные эпикиирзы с неправильным id госпитализации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <returns></returns>
        public List<CLineOfCommunicationEpicrisis> GetWrongLineOfCommunicationEpicrisis(CHospitalizationWorker hospitalizationWorker)
        {
            var wrongLineOfCommunicationEpicrisiss = new List<CLineOfCommunicationEpicrisis>();
            foreach (CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisis in _lineOfCommunicationEpicrisisList)
            {
                try
                {
                    hospitalizationWorker.GetById(lineOfCommunicationEpicrisis.HospitalizationId);
                }
                catch
                {
                    wrongLineOfCommunicationEpicrisiss.Add(lineOfCommunicationEpicrisis);
                }
            }

            return wrongLineOfCommunicationEpicrisiss;
        }
    }
}
