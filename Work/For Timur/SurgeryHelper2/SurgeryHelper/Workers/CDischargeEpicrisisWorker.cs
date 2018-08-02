using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CDischargeEpicrisisWorker : CBaseWorker
    {
        private List<CDischargeEpicrisis> _dischargeEpicrisisList;
        private readonly string _dischargeEpicrisisPath;

        public CDischargeEpicrisisWorker(string dataPath)
        {
            _dischargeEpicrisisPath = Path.Combine(dataPath, "discharge_epicrisis.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию о выписном эпикризе
        /// </summary>
        /// <param name="dischargeEpicrisisInfo">Информация о выписном эпикризе</param>
        public void Update(CDischargeEpicrisis dischargeEpicrisisInfo)
        {
            int n = GetIndexFromList(dischargeEpicrisisInfo.HospitalizationId);
            dischargeEpicrisisInfo.NotInDatabase = false;
            _dischargeEpicrisisList[n] = new CDischargeEpicrisis(dischargeEpicrisisInfo);
            Save();
        }


        /// <summary>
        /// Удалить выписной эпикриз
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        public void Remove(int hospitalizationId)
        {
            int index = 0;
            while (index < _dischargeEpicrisisList.Count)                
            {
                if (_dischargeEpicrisisList[index].HospitalizationId == hospitalizationId)
                {
                    _dischargeEpicrisisList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            
            Save();
        }


        /// <summary>
        /// Сохранить список выписных эпикризов
        /// </summary>
        private void Save()
        {
            var dischargeEpicrisissStr = new StringBuilder();

            foreach (CDischargeEpicrisis dischargeEpicrisisInfo in _dischargeEpicrisisList)
            {
                dischargeEpicrisissStr.Append(
                    "HospitalizationId=" + dischargeEpicrisisInfo.HospitalizationId + DataSplitStr +
                    "AnalysisDate=" + CConvertEngine.DateTimeToString(dischargeEpicrisisInfo.AnalysisDate) + DataSplitStr +
                    "AfterOperation=" + dischargeEpicrisisInfo.AfterOperation + DataSplitStr +
                    "ConservativeTherapy=" + dischargeEpicrisisInfo.ConservativeTherapy + DataSplitStr +
                    "Ekg=" + dischargeEpicrisisInfo.Ekg + DataSplitStr +
                    "OakEritrocits=" + dischargeEpicrisisInfo.OakEritrocits + DataSplitStr +
                    "OakHb=" + dischargeEpicrisisInfo.OakHb + DataSplitStr +
                    "OakLekocits=" + dischargeEpicrisisInfo.OakLekocits + DataSplitStr +
                    "OakSoe=" + dischargeEpicrisisInfo.OakSoe + DataSplitStr +
                    "OamColor=" + dischargeEpicrisisInfo.OamColor + DataSplitStr +
                    "OamDensity=" + dischargeEpicrisisInfo.OamDensity + DataSplitStr +
                    "OamEritrocits=" + dischargeEpicrisisInfo.OamEritrocits + DataSplitStr +
                    "OamLekocits=" + dischargeEpicrisisInfo.OamLekocits + DataSplitStr +
                    "AdditionalAnalises=" + dischargeEpicrisisInfo.AdditionalAnalises + DataSplitStr +
                    "Recomendations=" + CConvertEngine.ListToString(dischargeEpicrisisInfo.Recomendations) + DataSplitStr +
                    "AdditionalRecomendations=" + CConvertEngine.ListToString(dischargeEpicrisisInfo.AdditionalRecomendations) + ObjSplitStr);
            }

            CDatabaseEngine.PackText(dischargeEpicrisissStr.ToString(), _dischargeEpicrisisPath);
        }


        /// <summary>
        /// Загрузить список выписных эпикризов
        /// </summary>
        private void Load()
        {
            _dischargeEpicrisisList = new List<CDischargeEpicrisis>();
            string allDataStr = CDatabaseEngine.UnpackText(_dischargeEpicrisisPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var dischargeEpicrisisInfo = new CDischargeEpicrisis();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "HospitalizationId":
                            dischargeEpicrisisInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "AnalysisDate":
                            if (string.IsNullOrEmpty(keyValue[1]))
                            {
                                dischargeEpicrisisInfo.AnalysisDate = null;
                            }
                            else
                            {
                                dischargeEpicrisisInfo.AnalysisDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            }
                            break;
                        case "AfterOperation":
                            dischargeEpicrisisInfo.AfterOperation = keyValue[1];
                            break;
                        case "ConservativeTherapy":
                            dischargeEpicrisisInfo.ConservativeTherapy = keyValue[1];
                            break;
                        case "Ekg":
                            dischargeEpicrisisInfo.Ekg = keyValue[1];
                            break;
                        case "OakEritrocits":
                            dischargeEpicrisisInfo.OakEritrocits = keyValue[1];
                            break;
                        case "OakHb":
                            dischargeEpicrisisInfo.OakHb = keyValue[1];
                            break;
                        case "OakLekocits":
                            dischargeEpicrisisInfo.OakLekocits = keyValue[1];
                            break;
                        case "OakSoe":
                            dischargeEpicrisisInfo.OakSoe = keyValue[1];
                            break;
                        case "OamColor":
                            dischargeEpicrisisInfo.OamColor = keyValue[1];
                            break;
                        case "OamDensity":
                            dischargeEpicrisisInfo.OamDensity = keyValue[1];
                            break;
                        case "OamEritrocits":
                            dischargeEpicrisisInfo.OamEritrocits = keyValue[1];
                            break;
                        case "OamLekocits":
                            dischargeEpicrisisInfo.OamLekocits = keyValue[1];
                            break;
                        case "AdditionalAnalises":
                            dischargeEpicrisisInfo.AdditionalAnalises = keyValue[1];
                            break;
                        case "Recomendations":
                            dischargeEpicrisisInfo.Recomendations = CConvertEngine.StringToStringList(keyValue[1]);
                            break;
                        case "AdditionalRecomendations":
                            dischargeEpicrisisInfo.AdditionalRecomendations = CConvertEngine.StringToStringList(keyValue[1]);
                            break;
                    }
                }

                _dischargeEpicrisisList.Add(dischargeEpicrisisInfo);
            }
        }


        /// <summary>
        /// Получить индекс выписного эпикриза в списке для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        private int GetIndexFromList(int hospitalizationId)
        {
            int n = 0;
            while (n < _dischargeEpicrisisList.Count &&
                _dischargeEpicrisisList[n].HospitalizationId != hospitalizationId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли выписной эпикриз для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public bool IsExists(int hospitalizationId)
        {
            return GetIndexFromList(hospitalizationId) != _dischargeEpicrisisList.Count;
        }


        /// <summary>
        /// Получить выписной эпикриз по переданному id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public CDischargeEpicrisis GetByHospitalizationId(int hospitalizationId)
        {
            int n = GetIndexFromList(hospitalizationId);            

            if (n == _dischargeEpicrisisList.Count)
            {
                var newDischargeEpicrisisInfo = new CDischargeEpicrisis(hospitalizationId)
                {
                    NotInDatabase = true
                };
                _dischargeEpicrisisList.Add(newDischargeEpicrisisInfo);
                return new CDischargeEpicrisis(newDischargeEpicrisisInfo);
            }

            return new CDischargeEpicrisis(_dischargeEpicrisisList[n]);
        }


        /// <summary>
        /// Скопировать все выписные эпикризы для указанной госпитализации
        /// </summary>
        /// <param name="hospitalizationId">Id копируемой госпитализации</param>
        /// <param name="newHospitalizationId">Id нового госпитализации</param>
        public void CopyByHospitalizationId(int hospitalizationId, int newHospitalizationId)
        {
            CDischargeEpicrisis newCLineOfCommunication = GetByHospitalizationId(hospitalizationId);
            newCLineOfCommunication.HospitalizationId = newHospitalizationId;
            _dischargeEpicrisisList.Add(newCLineOfCommunication);
            Save();
        }


        /// <summary>
        /// Получить все переводные эпикиирзы с неправильным id госпитализации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <returns></returns>
        public List<CDischargeEpicrisis> GetWrongDischargeEpicrisis(CHospitalizationWorker hospitalizationWorker)
        {
            var wrongDischargeEpicrisiss = new List<CDischargeEpicrisis>();
            foreach (CDischargeEpicrisis dischargeEpicrisis in _dischargeEpicrisisList)
            {
                try
                {
                    hospitalizationWorker.GetById(dischargeEpicrisis.HospitalizationId);
                }
                catch
                {
                    wrongDischargeEpicrisiss.Add(dischargeEpicrisis);
                }
            }

            return wrongDischargeEpicrisiss;
        }
    }
}
