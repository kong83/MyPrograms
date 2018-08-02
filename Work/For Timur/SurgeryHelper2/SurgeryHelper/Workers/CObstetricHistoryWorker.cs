using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CObstetricHistoryWorker : CBaseWorker
    {
        private List<CObstetricHistory> _obstetricHistoryList;
        private readonly string _obstetricHistoryPath;

        public CObstetricHistoryWorker(string dataPath)
        {
            _obstetricHistoryPath = Path.Combine(dataPath, "obstetric_history.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию об акушерском анамнезе
        /// </summary>
        /// <param name="obstetricHistoryInfo">Информация об акушерском анамнезе</param>
        public void Update(CObstetricHistory obstetricHistoryInfo)
        {
            int n = GetIndexFromList(obstetricHistoryInfo.PatientId);
            obstetricHistoryInfo.NotInDatabase = false;
            _obstetricHistoryList[n] = new CObstetricHistory(obstetricHistoryInfo);
            Save();
        }


        /// <summary>
        /// Удалить акушерский анамнез
        /// </summary>
        /// <param name="patientId">id пациента</param>
        public void Remove(int patientId)
        {
            int index = 0;
            while (index < _obstetricHistoryList.Count)
            {
                if (_obstetricHistoryList[index].PatientId == patientId)
                {
                    _obstetricHistoryList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список акушерских анамнезов
        /// </summary>
        private void Save()
        {
            var obstetricHistorysStr = new StringBuilder();

            foreach (CObstetricHistory obstetricHistoryInfo in _obstetricHistoryList)
            {
                obstetricHistorysStr.Append(
                    "PatientId=" + obstetricHistoryInfo.PatientId + DataSplitStr +
                    "Chronology=" + CConvertEngine.ListToString(obstetricHistoryInfo.Chronology) + DataSplitStr +
                    "ApgarScore=" + obstetricHistoryInfo.ApgarScore + DataSplitStr +
                    "BirthInjury=" + obstetricHistoryInfo.BirthInjury + DataSplitStr +
                    "ChildbirthWeeks=" + obstetricHistoryInfo.ChildbirthWeeks + DataSplitStr +
                    "ComplicationsDuringChildbirth=" + obstetricHistoryInfo.ComplicationsDuringChildbirth + DataSplitStr +
                    "ComplicationsInPregnancy=" + obstetricHistoryInfo.ComplicationsInPregnancy + DataSplitStr +
                    "DrugsInPregnancy=" + obstetricHistoryInfo.DrugsInPregnancy + DataSplitStr +
                    "DurationOfLabor=" + obstetricHistoryInfo.DurationOfLabor + DataSplitStr +
                    "Fetal=" + obstetricHistoryInfo.Fetal + DataSplitStr +
                    "HeightAtBirth=" + obstetricHistoryInfo.HeightAtBirth + DataSplitStr +
                    "HospitalTreatment=" + obstetricHistoryInfo.HospitalTreatment + DataSplitStr +
                    "IsTongsUsing=" + obstetricHistoryInfo.IsTongsUsing + DataSplitStr +
                    "IsVacuumUsing=" + obstetricHistoryInfo.IsVacuumUsing + DataSplitStr +
                    "ObstetricParalysis=" + obstetricHistoryInfo.ObstetricParalysis + DataSplitStr +
                    "OutpatientCare=" + obstetricHistoryInfo.OutpatientCare + DataSplitStr +
                    "WeightAtBirth=" + obstetricHistoryInfo.WeightAtBirth + ObjSplitStr);
            }

            CDatabaseEngine.PackText(obstetricHistorysStr.ToString(), _obstetricHistoryPath);
        }


        /// <summary>
        /// Загрузить список акушерских анамнезов
        /// </summary>
        private void Load()
        {
            _obstetricHistoryList = new List<CObstetricHistory>();
            string allDataStr = CDatabaseEngine.UnpackText(_obstetricHistoryPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var obstetricHistoryInfo = new CObstetricHistory();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "PatientId":
                            obstetricHistoryInfo.PatientId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Chronology":
                            obstetricHistoryInfo.Chronology = CConvertEngine.StringToBoolArray(keyValue[1]);
                            break;
                        case "ApgarScore":
                            obstetricHistoryInfo.ApgarScore = keyValue[1];
                            break;
                        case "BirthInjury":
                            obstetricHistoryInfo.BirthInjury = keyValue[1];
                            break;
                        case "ChildbirthWeeks":
                            obstetricHistoryInfo.ChildbirthWeeks = keyValue[1];
                            break;
                        case "ComplicationsDuringChildbirth":
                            obstetricHistoryInfo.ComplicationsDuringChildbirth = keyValue[1];
                            break;
                        case "ComplicationsInPregnancy":
                            obstetricHistoryInfo.ComplicationsInPregnancy = keyValue[1];
                            break;
                        case "DrugsInPregnancy":
                            obstetricHistoryInfo.DrugsInPregnancy = keyValue[1];
                            break;
                        case "DurationOfLabor":
                            obstetricHistoryInfo.DurationOfLabor = keyValue[1];
                            break;
                        case "Fetal":
                            obstetricHistoryInfo.Fetal = keyValue[1];
                            break;
                        case "HeightAtBirth":
                            obstetricHistoryInfo.HeightAtBirth = keyValue[1];
                            break;
                        case "HospitalTreatment":
                            obstetricHistoryInfo.HospitalTreatment = keyValue[1];
                            break;
                        case "IsTongsUsing":
                            obstetricHistoryInfo.IsTongsUsing = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "IsVacuumUsing":
                            obstetricHistoryInfo.IsVacuumUsing = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "ObstetricParalysis":
                            obstetricHistoryInfo.ObstetricParalysis = keyValue[1];
                            break;
                        case "OutpatientCare":
                            obstetricHistoryInfo.OutpatientCare = keyValue[1];
                            break;
                        case "WeightAtBirth":
                            obstetricHistoryInfo.WeightAtBirth = keyValue[1];
                            break;
                    }
                }

                _obstetricHistoryList.Add(obstetricHistoryInfo);
            }
        }


        /// <summary>
        /// Получить индекс акушерского анамнеза в списке для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        private int GetIndexFromList(int patientId)
        {
            int n = 0;
            while (n < _obstetricHistoryList.Count &&
                _obstetricHistoryList[n].PatientId != patientId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли акушерский анамнез для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public bool IsExists(int patientId)
        {
            return GetIndexFromList(patientId) != _obstetricHistoryList.Count;
        }


        /// <summary>
        /// Получить акушерский анамнез по переданному id пациента. Если его нет - то создать новый
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public CObstetricHistory GetByPatientId(int patientId)
        {
            int n = GetIndexFromList(patientId);

            if (n == _obstetricHistoryList.Count)
            {
                var newObstetricHistoryInfo = new CObstetricHistory(patientId)
                {
                    NotInDatabase = true
                };
                _obstetricHistoryList.Add(newObstetricHistoryInfo);
                return new CObstetricHistory(newObstetricHistoryInfo);
            }

            return new CObstetricHistory(_obstetricHistoryList[n]);
        }


        /// <summary>
        /// Скопировать акушерский анаменз для указанного пациента
        /// </summary>
        /// <param name="patientId">Id копируемого пациента</param>
        /// <param name="newPatientId">Id нового пациента</param>
        public void CopyByPatientId(int patientId, int newPatientId)
        {
            CObstetricHistory newObstetricHistory = GetByPatientId(patientId);
            newObstetricHistory.PatientId = newPatientId;
            _obstetricHistoryList.Add(newObstetricHistory);
            Save();
        }


        /// <summary>
        /// Получить все акушерские анамнезы с неправильным id пациента
        /// </summary>
        /// <param name="patientWorker">Объект для работы с пациентами</param>
        /// <returns></returns>
        public List<CObstetricHistory> GetWrongObstetricHistorys(CPatientWorker patientWorker)
        {
            var wrongObstetricHistorys = new List<CObstetricHistory>();
            foreach (CObstetricHistory obstetricHistory in _obstetricHistoryList)
            {
                try
                {
                    patientWorker.GetById(obstetricHistory.PatientId);
                }
                catch
                {
                    wrongObstetricHistorys.Add(obstetricHistory);
                }
            }

            return wrongObstetricHistorys;
        }
    }
}
