using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Workers
{
    public class CObstetricParalysisCardWorker : CBaseWorker
    {
        private List<CObstetricParalysisCard> _obstetricParalysisCardList;
        private readonly string _obstetricParalysisCardPath;

        public CObstetricParalysisCardWorker(string dataPath)
        {
            _obstetricParalysisCardPath = Path.Combine(dataPath, "obstetric_paralysis_card.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию о карте обследования
        /// </summary>
        /// <param name="obstetricParalysisCardInfo">Информация о карте обследования</param>
        public void Update(CObstetricParalysisCard obstetricParalysisCardInfo)
        {
            int n = GetIndexFromList(obstetricParalysisCardInfo.HospitalizationId, obstetricParalysisCardInfo.VisitId);
            obstetricParalysisCardInfo.NotInDatabase = false;
            _obstetricParalysisCardList[n] = new CObstetricParalysisCard(obstetricParalysisCardInfo);
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
            while (index < _obstetricParalysisCardList.Count)
            {
                if (_obstetricParalysisCardList[index].HospitalizationId == hospitalizationId &&
                    _obstetricParalysisCardList[index].VisitId == visitId)
                {                    
                    _obstetricParalysisCardList.RemoveAt(index);
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
            var obstetricParalysisCardsStr = new StringBuilder();

            foreach (CObstetricParalysisCard obstetricParalysisCardInfo in _obstetricParalysisCardList)
            {
                obstetricParalysisCardsStr.Append(
                    "HospitalizationId=" + obstetricParalysisCardInfo.HospitalizationId + DataSplitStr +
                    "VisitId=" + obstetricParalysisCardInfo.VisitId + DataSplitStr +
                    "CardSide=" + obstetricParalysisCardInfo.SideOfCard + DataSplitStr +
                    "GlobalAbductionPicturesSelection=" + CConvertEngine.ListToString(obstetricParalysisCardInfo.GlobalAbductionPicturesSelection) + DataSplitStr +
                    "GlobalExternalRotationPicturesSelection=" + CConvertEngine.ListToString(obstetricParalysisCardInfo.GlobalExternalRotationPicturesSelection) + DataSplitStr +
                    "HandToMouthPicturesSelection=" + CConvertEngine.ListToString(obstetricParalysisCardInfo.HandToMouthPicturesSelection) + DataSplitStr +
                    "HandToNeckPicturesSelection=" + CConvertEngine.ListToString(obstetricParalysisCardInfo.HandToNeckPicturesSelection) + DataSplitStr +
                    "HandToSpinePicturesSelection=" + CConvertEngine.ListToString(obstetricParalysisCardInfo.HandToSpinePicturesSelection) + DataSplitStr +
                    "ComboBoxes=" + CConvertEngine.ListToString(obstetricParalysisCardInfo.ComboBoxes) + ObjSplitStr);
            }

            CDatabaseEngine.PackText(obstetricParalysisCardsStr.ToString(), _obstetricParalysisCardPath);
        }


        /// <summary>
        /// Загрузить список карт обследования
        /// </summary>
        private void Load()
        {
            _obstetricParalysisCardList = new List<CObstetricParalysisCard>();
            string allDataStr = CDatabaseEngine.UnpackText(_obstetricParalysisCardPath);

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

                    var obstetricParalysisCardInfo = new CObstetricParalysisCard();
                    foreach (string dataStr in datasStr)
                    {
                        string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                        switch (keyValue[0])
                        {
                            case "HospitalizationId":
                                obstetricParalysisCardInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "VisitId":
                                obstetricParalysisCardInfo.VisitId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "CardSide":
                                obstetricParalysisCardInfo.SideOfCard = CConvertEngine.StringToCardSide(keyValue[1]);
                                break;
                            case "GlobalAbductionPicturesSelection":
                                obstetricParalysisCardInfo.GlobalAbductionPicturesSelection = CConvertEngine.StringToBoolArray(keyValue[1]);
                                break;
                            case "GlobalExternalRotationPicturesSelection":
                                obstetricParalysisCardInfo.GlobalExternalRotationPicturesSelection = CConvertEngine.StringToBoolArray(keyValue[1]);
                                break;
                            case "HandToMouthPicturesSelection":
                                obstetricParalysisCardInfo.HandToMouthPicturesSelection = CConvertEngine.StringToBoolArray(keyValue[1]);
                                break;
                            case "HandToNeckPicturesSelection":
                                obstetricParalysisCardInfo.HandToNeckPicturesSelection = CConvertEngine.StringToBoolArray(keyValue[1]);
                                break;
                            case "HandToSpinePicturesSelection":
                                obstetricParalysisCardInfo.HandToSpinePicturesSelection = CConvertEngine.StringToBoolArray(keyValue[1]);
                                break; 
                            case "ComboBoxes":
                                obstetricParalysisCardInfo.ComboBoxes = CConvertEngine.StringToStringArray(keyValue[1]);
                                break;                            
                        }
                    }
                    
                    _obstetricParalysisCardList.Add(obstetricParalysisCardInfo);
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
        /// Получить индекс карты обследования в списке для указанного id госпитализации и id консультации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <returns></returns>
        private int GetIndexFromList(int hospitalizationId, int visitId)
        {
            int n = 0;
            while (n < _obstetricParalysisCardList.Count &&
                (_obstetricParalysisCardList[n].HospitalizationId != hospitalizationId ||
                _obstetricParalysisCardList[n].VisitId != visitId))
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
            if (GetIndexFromList(hospitalizationId, visitId) != _obstetricParalysisCardList.Count)
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
        public CObstetricParalysisCard GetByHospitalizationAndVisitId(
            int hospitalizationId, int visitId)
        {
            int n = GetIndexFromList(hospitalizationId, visitId);

            if (n == _obstetricParalysisCardList.Count)
            {
                var newObstetricParalysisCardInfo = new CObstetricParalysisCard(hospitalizationId, visitId)
                {
                    NotInDatabase = true
                };
                _obstetricParalysisCardList.Add(newObstetricParalysisCardInfo);
                return new CObstetricParalysisCard(newObstetricParalysisCardInfo);
            }

            return new CObstetricParalysisCard(_obstetricParalysisCardList[n]);
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
            int obstetricParalysisCardListCnt = _obstetricParalysisCardList.Count;
            for (int i = 0; i < obstetricParalysisCardListCnt; i++)
            {
                if (_obstetricParalysisCardList[i].HospitalizationId == hospitalizationId &&
                    _obstetricParalysisCardList[i].VisitId == visitId)
                {
                    var newObstetricParalysisCard = new CObstetricParalysisCard(_obstetricParalysisCardList[i])
                    { 
                        HospitalizationId = newHospitalizationId, VisitId = newVisitId 
                    };
                    _obstetricParalysisCardList.Add(newObstetricParalysisCard);
                }
            }

            Save();
        }


        /// <summary>
        /// Получить все карты на акушерский паралич с неправильным id госпитализации и консультации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <param name="visitWorker">Объект для работы с консультациями</param>
        /// <returns></returns>
        public List<CObstetricParalysisCard> GetWrongObstetricParalysisCards(
            CHospitalizationWorker hospitalizationWorker, CVisitWorker visitWorker)
        {
            var wrongObstetricParalysisCards = new List<CObstetricParalysisCard>();
            foreach (CObstetricParalysisCard obstetricParalysisCard in _obstetricParalysisCardList)
            {
                try
                {
                    hospitalizationWorker.GetById(obstetricParalysisCard.HospitalizationId);
                }
                catch
                {
                    try
                    {
                        visitWorker.GetById(obstetricParalysisCard.VisitId);
                    }
                    catch
                    {
                        wrongObstetricParalysisCards.Add(obstetricParalysisCard);
                    }
                }
            }

            return wrongObstetricParalysisCards;
        }
    }
}
