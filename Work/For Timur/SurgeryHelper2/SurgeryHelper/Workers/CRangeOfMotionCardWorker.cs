using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Workers
{
    public class CRangeOfMotionCardWorker : CBaseWorker
    {
        private List<CRangeOfMotionCard> _rangeOfMotionCardList;
        private readonly string _rangeOfMotionCardPath;

        public CRangeOfMotionCardWorker(string dataPath)
        {
            _rangeOfMotionCardPath = Path.Combine(dataPath, "range_of_motion_card.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию о карте обследования
        /// </summary>
        /// <param name="rangeOfMotionCardInfo">Информация о карте обследования</param>
        public void Update(CRangeOfMotionCard rangeOfMotionCardInfo)
        {
            int n = GetIndexFromList(rangeOfMotionCardInfo.HospitalizationId, rangeOfMotionCardInfo.VisitId);
            rangeOfMotionCardInfo.NotInDatabase = false;
            _rangeOfMotionCardList[n] = new CRangeOfMotionCard(rangeOfMotionCardInfo);
            Save();
        }


        /// <summary>
        /// Удалить карту обследования
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        public void Remove(int hospitalizationId, int visitId)
        {
            int index = 0;
            while (index < _rangeOfMotionCardList.Count)
            {
                if (_rangeOfMotionCardList[index].HospitalizationId == hospitalizationId &&
                    _rangeOfMotionCardList[index].VisitId == visitId)
                {                    
                    _rangeOfMotionCardList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список карт обследования
        /// </summary>
        private void Save()
        {
            var rangeOfMotionCardsStr = new StringBuilder();

            foreach (CRangeOfMotionCard rangeOfMotionCardInfo in _rangeOfMotionCardList)
            {
                rangeOfMotionCardsStr.Append(
                    "HospitalizationId=" + rangeOfMotionCardInfo.HospitalizationId + DataSplitStr +
                    "VisitId=" + rangeOfMotionCardInfo.VisitId + DataSplitStr +
                    "OppositionFinger=" + rangeOfMotionCardInfo.OppositionFinger + DataSplitStr +
                    "Fields=" + CConvertEngine.ListToString(rangeOfMotionCardInfo.Fields) + ObjSplitStr);
            }

            CDatabaseEngine.PackText(rangeOfMotionCardsStr.ToString(), _rangeOfMotionCardPath);
        }


        /// <summary>
        /// Загрузить список карт обследования
        /// </summary>
        private void Load()
        {
            _rangeOfMotionCardList = new List<CRangeOfMotionCard>();
            string allDataStr = CDatabaseEngine.UnpackText(_rangeOfMotionCardPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            var errorStr = new StringBuilder();
            try
            {
                // Проходим по всем объектам
                foreach (string objectStr in objectsStr)
                {
                    // Для каждого объекта получаем список значений
                    string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                    var rangeOfMotionCardInfo = new CRangeOfMotionCard();
                    foreach (string dataStr in datasStr)
                    {
                        string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                        switch (keyValue[0])
                        {
                            case "HospitalizationId":
                                rangeOfMotionCardInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "VisitId":
                                rangeOfMotionCardInfo.VisitId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "OppositionFinger":
                                rangeOfMotionCardInfo.OppositionFinger = keyValue[1];
                                break;
                            case "Fields":
                                rangeOfMotionCardInfo.Fields = CConvertEngine.StringToStringArray(keyValue[1]);
                                break;                            
                        }
                    }
                    
                    _rangeOfMotionCardList.Add(rangeOfMotionCardInfo);
                }
            }
            catch (Exception ex)
            {
                errorStr.Append(ex.Message + "\r\n");
            }

            if (errorStr.Length != 0)
            {
                MessageBox.ShowDialog(errorStr.ToString());
            }
        }


        /// <summary>
        /// Получить индекс карты обследования в списке для указанного id госпитализации,
        /// id консультации, типа карты и стороны
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <returns></returns>
        private int GetIndexFromList(int hospitalizationId, int visitId)
        {
            int n = 0;
            while (n < _rangeOfMotionCardList.Count &&
                (_rangeOfMotionCardList[n].HospitalizationId != hospitalizationId ||
                _rangeOfMotionCardList[n].VisitId != visitId))
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли карта обследования для указанных id госпитализации,
        /// id консультации, типа карты и стороны
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <returns></returns>
        public bool IsExists(int hospitalizationId, int visitId)
        {
            if (GetIndexFromList(hospitalizationId, visitId) != _rangeOfMotionCardList.Count)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Получить карту обследования по переданному id госпитализации и id консультации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <returns></returns>
        public CRangeOfMotionCard GetByHospitalizationAndVisitId(
            int hospitalizationId, int visitId)
        {
            int n = GetIndexFromList(hospitalizationId, visitId);

            if (n == _rangeOfMotionCardList.Count)
            {
                var newRangeOfMotionCardInfo = new CRangeOfMotionCard(hospitalizationId, visitId)
                {
                    NotInDatabase = true
                };
                _rangeOfMotionCardList.Add(newRangeOfMotionCardInfo);
                return new CRangeOfMotionCard(newRangeOfMotionCardInfo);
            }

            return new CRangeOfMotionCard(_rangeOfMotionCardList[n]);
        }


        /// <summary>
        /// Скопировать все карты обследования для указанных id госпитализации и консультации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <param name="newHospitalizationId">id новой госпитализации</param>
        /// <param name="newVisitId">id новой консультации</param>        
        public void CopyByHospitalizationAndVisitId(
            int hospitalizationId, int visitId, int newHospitalizationId, int newVisitId)
        {
            int rangeOfMotionCardListCnt = _rangeOfMotionCardList.Count;
            for (int i = 0; i < rangeOfMotionCardListCnt; i++)
            {
                if (_rangeOfMotionCardList[i].HospitalizationId == hospitalizationId &&
                    _rangeOfMotionCardList[i].VisitId == visitId)
                {
                    var newRangeOfMotionCard = new CRangeOfMotionCard(_rangeOfMotionCardList[i])
                    { 
                        HospitalizationId = newHospitalizationId, 
                        VisitId = newVisitId 
                    };
                    _rangeOfMotionCardList.Add(newRangeOfMotionCard);
                }
            }

            Save();
        }


        /// <summary>
        /// Получить все карты на объём движений с неправильным id госпитализации и консультации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <param name="visitWorker">Объект для работы с консультациями</param>
        /// <returns></returns>
        public List<CRangeOfMotionCard> GetWrongRangeOfMotionCards(
            CHospitalizationWorker hospitalizationWorker, CVisitWorker visitWorker)
        {
            var wrongRangeOfMotionCards = new List<CRangeOfMotionCard>();
            foreach (CRangeOfMotionCard rangeOfMotionCard in _rangeOfMotionCardList)
            {
                try
                {
                    hospitalizationWorker.GetById(rangeOfMotionCard.HospitalizationId);
                }
                catch
                {
                    try
                    {
                        visitWorker.GetById(rangeOfMotionCard.VisitId);
                    }
                    catch
                    {
                        wrongRangeOfMotionCards.Add(rangeOfMotionCard);
                    }
                }
            }

            return wrongRangeOfMotionCards;
        }
    }
}
