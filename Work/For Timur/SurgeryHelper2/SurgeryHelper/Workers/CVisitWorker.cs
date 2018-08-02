using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CVisitWorker : CBaseWorker
    {
        private readonly CWorkersKeeper _workersKeeper;
        private List<CVisit> _visitList;
        private readonly string _visitPath;

        public CVisitWorker(CWorkersKeeper workersKeeper, string dataPath)
        {
            _workersKeeper = workersKeeper;
            _visitPath = Path.Combine(dataPath, "visits.save");
            Load();
        }

        /// <summary>
        /// Получить новый id для госпитализации
        /// </summary>
        /// <returns></returns>
        public int GetNewID()
        {
            return GetNewID(_visitList.ToArray());
        }


        /// <summary>
        /// Добавить информацию о консультации без сохранения в базу
        /// </summary>
        /// <param name="visitInfo">Информация о консультации</param>
        public void AddWithoutSaving(CVisit visitInfo)
        {
            _visitList.Add(visitInfo);
        }


        /// <summary>
        /// Обновить информацию о консультации
        /// </summary>
        /// <param name="visitInfo">Информация о консультации</param>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void Update(CVisit visitInfo, CPatient patientInfo)
        {
            CVisit tempVisit = GetByGeneralData(
                visitInfo.VisitDate,
                patientInfo.GetFullName(),
                patientInfo.Nosology,
                visitInfo.Id);
            if (tempVisit != null)
            {
                throw new Exception("Консультация с такой датой для этого пациента уже содержится в базе. Измените дату или время консультации.");
            }

            int n = 0;
            while (n < _visitList.Count && _visitList[n].Id != visitInfo.Id)
            {
                n++;
            }

            if (n == _visitList.Count)
            {
                _visitList.Add(visitInfo);
            }
            else
            {
                _visitList[n] = visitInfo;
            }

            Save();
        }


        /// <summary>
        /// Удалить все относящиеся к консультации объекты
        /// </summary>
        /// <param name="visitId">Id консультации</param>
        private void RemoveVisitEntitys(int visitId)
        {
            _workersKeeper.CardWorker.Remove(-1, visitId);
            _workersKeeper.BrachialPlexusCardWorker.Remove(-1, visitId);
            _workersKeeper.ObstetricParalysisCardWorker.Remove(-1, visitId);
            _workersKeeper.RangeOfMotionCardWorker.Remove(-1, visitId);
        }


        /// <summary>
        /// Удалить консультацию
        /// </summary>
        /// <param name="visitId">ID консультации</param>
        public void Remove(int visitId)
        {
            int index = 0;
            while (index < _visitList.Count)
            {
                if (_visitList[index].Id == visitId)
                {                    
                    _visitList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            RemoveVisitEntitys(visitId);

            Save();
        }


        /// <summary>
        /// Удалить все консультации для удаляемого пациента
        /// </summary>
        /// <param name="patientId">Id пациента</param>
        public void RemoveByPatientId(int patientId)
        {
            int index = 0;
            while (index < _visitList.Count)
            {
                if (_visitList[index].PatientId == patientId)
                {
                    RemoveVisitEntitys(_visitList[index].Id);
                    _visitList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список консультаций
        /// </summary>
        private void Save()
        {
            _visitList.Sort(CVisit.Compare);

            var visitsStr = new StringBuilder();

            foreach (CVisit visitInfo in _visitList)
            {
                visitsStr.Append(
                    "Id=" + visitInfo.Id + DataSplitStr +
                    "PatientId=" + visitInfo.PatientId + DataSplitStr +
                    "VisitDate=" + CConvertEngine.DateTimeToString(visitInfo.VisitDate, true) + DataSplitStr +
                    "Evenly=" + visitInfo.Evenly + DataSplitStr +
                    "Recommendation=" + visitInfo.Recommendation + DataSplitStr +
                    "Comments=" + visitInfo.Comments + DataSplitStr +
                    "Doctor=" + visitInfo.Doctor + DataSplitStr +
                    "IsLastParagraphForCertificateNeeded=" + visitInfo.IsLastParagraphForCertificateNeeded + DataSplitStr +
                    "IsLastOdkbParagraphForCertificateNeeded=" + visitInfo.IsLastOdkbParagraphForCertificateNeeded + DataSplitStr +
                    "Diagnose=" + visitInfo.Diagnose + ObjSplitStr);
            }

            CDatabaseEngine.PackText(visitsStr.ToString(), _visitPath);
        }


        /// <summary>
        /// Загрузить список консультаций
        /// </summary>
        private void Load()
        {
            _visitList = new List<CVisit>();
            string allDataStr = CDatabaseEngine.UnpackText(_visitPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var visitInfo = new CVisit();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            visitInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "PatientId":
                            visitInfo.PatientId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "VisitDate":
                            visitInfo.VisitDate = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "Diagnose":
                            visitInfo.Diagnose = keyValue[1];
                            break;
                        case "Evenly":
                            visitInfo.Evenly = keyValue[1];
                            break;
                        case "Recommendation":
                            visitInfo.Recommendation = keyValue[1];
                            break;
                        case "Comments":
                            visitInfo.Comments = keyValue[1];
                            break;
                        case "Doctor":
                            visitInfo.Doctor = keyValue[1];
                            break;
                        case "IsLastParagraphForCertificateNeeded":
                            visitInfo.IsLastParagraphForCertificateNeeded = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "IsLastOdkbParagraphForCertificateNeeded":
                            visitInfo.IsLastOdkbParagraphForCertificateNeeded = Convert.ToBoolean(keyValue[1]);
                            break;
                    }
                }

                _visitList.Add(visitInfo);
            }
        }


        /// <summary>
        /// Получить консультацию по указанному id
        /// </summary>
        /// <param name="visitId">id консультации</param>
        /// <returns></returns>
        public CVisit GetById(int visitId)
        {
            foreach (CVisit visit in _visitList)
            {
                if (visit.Id == visitId)
                {
                    return visit;
                }
            }

            throw new ArgumentException("Внутренняя ошибка программы. Не найдена консультация с id=" + visitId);
        }


        /// <summary>
        /// Получить список консультаций для указанного id консультации
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public int GetCountByPatientId(int patientId)
        {
            int result = 0;

            foreach (CVisit visit in _visitList)
            {
                if (visit.PatientId == patientId)
                {
                    result++;
                }
            }

            return result;
        }


        /// <summary>
        /// Получить список консультаций для указанного id консультации
        /// </summary>
        /// <param name="patientId">id пациента</param>
        /// <returns></returns>
        public CVisit[] GetListByPatientId(int patientId)
        {
            var visits = new List<CVisit>();

            foreach (CVisit visit in _visitList)
            {
                if (visit.PatientId == patientId)
                {
                    visits.Add(visit);
                }
            }

            return visits.ToArray();
        }


        /// <summary>
        /// Вызвать копирование всех сущностей, входящих в консультацию
        /// </summary>
        /// <param name="visitId">Id старой консультации</param>
        /// <param name="newVisitId">Id новой консультации</param>
        private void CopyVisitEssenses(int visitId, int newVisitId)
        {
            _workersKeeper.CardWorker.CopyByHospitalizationAndVisitId(
                    -1, visitId, -1, newVisitId);
            _workersKeeper.BrachialPlexusCardWorker.CopyByHospitalizationAndVisitId(
                -1, visitId, -1, newVisitId);
            _workersKeeper.RangeOfMotionCardWorker.CopyByHospitalizationAndVisitId(
                -1, visitId, -1, newVisitId);
            _workersKeeper.ObstetricParalysisCardWorker.CopyByHospitalizationAndVisitId(
                -1, visitId, -1, newVisitId);
        }


        /// <summary>
        /// Скопировать переданную консультацию
        /// </summary>
        /// <param name="visitInfo">Информация о консультации</param>
        /// <param name="patient">Информация о пациенте</param>
        public void Copy(CVisit visitInfo, CPatient patient)
        {
            var newVisitInfo = new CVisit(visitInfo)
            {
                Id = GetNewID(),
                VisitDate = DateTime.Now
            };

            while (GetByGeneralData(newVisitInfo.VisitDate, patient.GetFullName(), patient.Nosology, newVisitInfo.Id) != null)
            {
                newVisitInfo.VisitDate = newVisitInfo.VisitDate.AddMinutes(1);
            }

            _visitList.Add(newVisitInfo);

            CopyVisitEssenses(visitInfo.Id, newVisitInfo.Id);

            Save();
        }


        /// <summary>
        /// Скопировать все консультации для указанного пациента
        /// </summary>
        /// <param name="patientId">Id копируемого пациента</param>
        /// <param name="newPatientId">Id нового пациента</param>
        public void CopyByPatientId(int patientId, int newPatientId)
        {
            foreach (CVisit visitInfo in GetListByPatientId(patientId))
            {
                var newVisitInfo = new CVisit(visitInfo)
                {
                    Id = GetNewID(),
                    PatientId = newPatientId
                };
                _visitList.Add(newVisitInfo);

                CopyVisitEssenses(visitInfo.Id, newVisitInfo.Id);
            }

            Save();
        }


        /// <summary>
        /// Получить консультацию по уникальным данным
        /// </summary>
        /// <param name="visitDate">Дата консультации</param>
        /// <param name="patientFIO">ФИО пациента</param>
        /// <param name="patientNosologyName">Название нозологии</param>
        /// <param name="wrongVisitId">id консультации, который надо игнорировать</param>
        /// <returns></returns>
        public CVisit GetByGeneralData(
            DateTime visitDate, string patientFIO, string patientNosologyName, int wrongVisitId)
        {
            foreach (CVisit visit in _visitList)
            {
                CPatient patient = _workersKeeper.PatientWorker.GetById(visit.PatientId);

                if (CCompareEngine.CompareDateTime(visit.VisitDate, visitDate) == 0 &&
                    patient.GetFullName() == patientFIO &&
                    patient.Nosology == patientNosologyName &&
                    visit.Id != wrongVisitId)
                {
                    return visit;
                }
            }

            return null;
        }


        /// <summary>
        /// Получить все консультации с неправильным id пациента
        /// </summary>
        /// <param name="patientWorker">Объект для работы с пациентами</param>
        /// <returns></returns>
        public List<CVisit> GetWrongVisits(CPatientWorker patientWorker)
        {
            var wrongVisits = new List<CVisit>();
            foreach (CVisit visit in _visitList)
            {
                try
                {
                    patientWorker.GetById(visit.PatientId);
                }
                catch
                {
                    wrongVisits.Add(visit);
                }
            }

            return wrongVisits;
        }
    }
}
