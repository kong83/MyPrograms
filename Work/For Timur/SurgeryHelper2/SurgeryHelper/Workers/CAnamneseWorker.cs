using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CAnamneseWorker : CBaseWorker
    {
        private List<CAnamnese> _anamneseList;
        private readonly string _anamnesePath;

        public CAnamneseWorker(string dataPath)
        {
            _anamnesePath = Path.Combine(dataPath, "anamnese.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию об анамнезе
        /// </summary>
        /// <param name="anamneseInfo">Информация об анамнезе</param>
        public void Update(CAnamnese anamneseInfo)
        {
            int n = GetIndexFromList(anamneseInfo.PatientId);
            anamneseInfo.NotInDatabase = false;
            _anamneseList[n] = new CAnamnese(anamneseInfo);
            Save();
        }


        /// <summary>
        /// Удалить анамнез
        /// </summary>
        /// <param name="patientId">id пациента</param>
        public void Remove(int patientId)
        {
            int index = 0;
            while (index < _anamneseList.Count)
            {
                if (_anamneseList[index].PatientId == patientId)
                {
                    _anamneseList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список анамнезов
        /// </summary>
        private void Save()
        {
            var anamnesesStr = new StringBuilder();

            foreach (CAnamnese anamneseInfo in _anamneseList)
            {
                anamnesesStr.Append(
                    "PatientId=" + anamneseInfo.PatientId + DataSplitStr +
                    "AnMorbi=" + anamneseInfo.AnMorbi + DataSplitStr +
                    "TraumaDate=" + CConvertEngine.DateTimeToString(anamneseInfo.TraumaDate) + ObjSplitStr);
            }

            CDatabaseEngine.PackText(anamnesesStr.ToString(), _anamnesePath);
        }


        /// <summary>
        /// Загрузить список анамнезов
        /// </summary>
        private void Load()
        {
            _anamneseList = new List<CAnamnese>();
            string allDataStr = CDatabaseEngine.UnpackText(_anamnesePath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var anamneseInfo = new CAnamnese();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "PatientId":
                            anamneseInfo.PatientId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "AnMorbi":
                            anamneseInfo.AnMorbi = keyValue[1];
                            break;
                        case "TraumaDate":
                            if (!string.IsNullOrEmpty(keyValue[1]))
                            {
                                anamneseInfo.TraumaDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            }
                            else
                            {
                                anamneseInfo.TraumaDate = null;
                            }

                            break;                        
                    }
                }

                _anamneseList.Add(anamneseInfo);
            }
        }


        /// <summary>
        /// Получить индекс анамнеза в списке для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        private int GetIndexFromList(int patientId)
        {
            int n = 0;
            while (n < _anamneseList.Count &&
                _anamneseList[n].PatientId != patientId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли анамнез для указанного id пациента
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public bool IsExists(int patientId)
        {
            return GetIndexFromList(patientId) != _anamneseList.Count;
        }


        /// <summary>
        /// Получить анамнез по переданному id пациента. Если его нет - то создать новый
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public CAnamnese GetByPatientId(int patientId)
        {
            int n = GetIndexFromList(patientId);

            if (n == _anamneseList.Count)
            {
                var newAnamneseInfo = new CAnamnese(patientId)
                {
                    NotInDatabase = true
                };
                _anamneseList.Add(newAnamneseInfo);
                return new CAnamnese(newAnamneseInfo);
            }

            return new CAnamnese(_anamneseList[n]);
        }


        /// <summary>
        /// Скопировать анаменз для указанного пациента
        /// </summary>
        /// <param name="patientId">Id копируемого пациента</param>
        /// <param name="newPatientId">Id нового пациента</param>
        public void CopyByPatientId(int patientId, int newPatientId)
        {
            CAnamnese newAnamnese = GetByPatientId(patientId);
            newAnamnese.PatientId = newPatientId;
            _anamneseList.Add(newAnamnese);
            Save();
        }


        /// <summary>
        /// Получить все анамнезы с неправильным id пациента
        /// </summary>
        /// <param name="patientWorker">Объект для работы с пациентами</param>
        /// <returns></returns>
        public List<CAnamnese> GetWrongAnamneses(CPatientWorker patientWorker)
        {
            var wrongAnamneses = new List<CAnamnese>();
            foreach (CAnamnese anamnese in _anamneseList)
            {
                try
                {
                    patientWorker.GetById(anamnese.PatientId);
                }
                catch
                {
                    wrongAnamneses.Add(anamnese);
                }
            }

            return wrongAnamneses;
        }
    }
}
