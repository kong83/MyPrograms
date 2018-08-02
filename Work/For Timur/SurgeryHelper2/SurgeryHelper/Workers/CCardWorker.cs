using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Workers
{
    public class CCardWorker : CBaseWorker
    {
        private List<CCard> _cardList;
        private readonly string _cardPath;

        public CCardWorker(string dataPath)
        {
            _cardPath = Path.Combine(dataPath, "card.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию о карте обследования
        /// </summary>
        /// <param name="cardInfo">Информация о карте обследования</param>
        public void Update(CCard cardInfo)
        {
            int n = GetIndexFromList(cardInfo.HospitalizationId, cardInfo.VisitId, cardInfo.CurrentSideOfCard, cardInfo.CurrentCardType);
            cardInfo.NotInDatabase = false;
            _cardList[n] = new CCard(cardInfo);
            Save(cardInfo);
        }


        /// <summary>
        /// Удалить карту обследования
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        public void Remove(int hospitalizationId, int visitId)
        {
            int index = 0;
            while (index < _cardList.Count)
            {
                if (_cardList[index].HospitalizationId == hospitalizationId &&
                    _cardList[index].VisitId == visitId)
                {
                    string fileName = GetPicturePath(_cardList[index].GetPictureFileName());
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    _cardList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save(null);
        }


        /// <summary>
        /// Получить полный путь до файла с картинкой по имени файла
        /// </summary>
        /// <param name="pictureFileName">Имя файла с картинкой</param>
        /// <returns></returns>
        private string GetPicturePath(string pictureFileName)
        {
            return Path.Combine(Path.GetDirectoryName(_cardPath) ?? string.Empty, "CardPictures\\" + pictureFileName);
        }


        /// <summary>
        /// Сохранить список карт обследования
        /// </summary>
        /// <param name="saveCardInfo">Информация о карте, которую сохраняем</param>
        private void Save(CCard saveCardInfo)
        {
            var cardsStr = new StringBuilder();

            foreach (CCard cardInfo in _cardList)
            {
                cardsStr.Append(
                    "HospitalizationId=" + cardInfo.HospitalizationId + DataSplitStr +
                    "VisitId=" + cardInfo.VisitId + DataSplitStr +
                    "CardSide=" + cardInfo.CurrentSideOfCard + DataSplitStr +
                    "CurrentCardType=" + cardInfo.CurrentCardType + ObjSplitStr);

                if (saveCardInfo != null && saveCardInfo.IsEqual(cardInfo))
                {
                    CDatabaseEngine.SaveBitmapToFile(
                        cardInfo.Picture,
                       GetPicturePath(cardInfo.GetPictureFileName()));
                }
            }

            CDatabaseEngine.PackText(cardsStr.ToString(), _cardPath);
        }


        /// <summary>
        /// Загрузить список карт обследования
        /// </summary>
        private void Load()
        {
            _cardList = new List<CCard>();
            string allDataStr = CDatabaseEngine.UnpackText(_cardPath);

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

                    var cardInfo = new CCard();
                    foreach (string dataStr in datasStr)
                    {
                        string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                        switch (keyValue[0])
                        {
                            case "HospitalizationId":
                                cardInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "VisitId":
                                cardInfo.VisitId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "CardSide":
                                cardInfo.CurrentSideOfCard = CConvertEngine.StringToCardSide(keyValue[1]);
                                break;
                            case "CurrentCardType":
                                cardInfo.CurrentCardType = CConvertEngine.StringToCardType(keyValue[1]);
                                break;
                        }
                    }

                    string picturePath = GetPicturePath(cardInfo.GetPictureFileName());
                    if (!File.Exists(picturePath))
                    {
                        PutDefaultPictureFile(cardInfo.CurrentCardType, picturePath);
                    }

                    cardInfo.Picture = CDatabaseEngine.GetBitmapFromFile(picturePath);

                    _cardList.Add(cardInfo);
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
        /// Поместить в файл с картинкой дефолтную картинку, для указанного типа карты
        /// </summary>
        /// <param name="cardType">Тип карты</param>
        /// <param name="picturePath">Полный путь до файла с картинкой</param>
        private static void PutDefaultPictureFile(CardType cardType, string picturePath)
        {
            switch (cardType)
            {
                case CardType.HandCutaneousNerves:
                    CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardHandCutaneousNerves, picturePath);
                    break;
                case CardType.HandDermatome:
                    CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardHandDermatome, picturePath);
                    break;
                case CardType.LegCutaneousNerves:
                    CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardLegCutaneousNerves, picturePath);
                    break;
                case CardType.LegDermatome:
                    CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardLegDermatome, picturePath);
                    break;
                case CardType.PamplegiaCard:
                    CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardPamplegiaCard, picturePath);
                    break;
                case CardType.SacriplexCard:
                    CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardSacriplexCard, picturePath);
                    break;
            }
        }


        /// <summary>
        /// Получить индекс карты обследования в списке для указанного id госпитализации,
        /// id консультации, типа карты и стороны
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <param name="cardSide">Сторона карты</param>
        /// <param name="cardType">Тип карты</param>
        /// <returns></returns>
        private int GetIndexFromList(
            int hospitalizationId, int visitId, CardSide cardSide, CardType cardType)
        {
            int n = 0;
            while (n < _cardList.Count &&
                (_cardList[n].HospitalizationId != hospitalizationId ||
                _cardList[n].VisitId != visitId ||
                _cardList[n].CurrentSideOfCard != cardSide ||
                _cardList[n].CurrentCardType != cardType))
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
        /// <param name="cardType">Тип карты</param>
        /// <returns></returns>
        public bool IsExists(int hospitalizationId, int visitId, CardType cardType)
        {
            if (GetIndexFromList(hospitalizationId, visitId, CardSide.Left, cardType) != _cardList.Count ||
                GetIndexFromList(hospitalizationId, visitId, CardSide.Right, cardType) != _cardList.Count)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Получить карту обследования по переданному id госпитализации,
        /// id консультации, типа карты и стороны
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <param name="cardSide">Сторона карты</param>
        /// <param name="cardType">Тип карты</param>
        /// <returns></returns>
        public CCard GetByGeneralData(
            int hospitalizationId, int visitId, CardSide cardSide, CardType cardType)
        {
            int n = GetIndexFromList(hospitalizationId, visitId, cardSide, cardType);

            if (n == _cardList.Count)
            {
                var newCardInfo = new CCard
                {
                    HospitalizationId = hospitalizationId,
                    VisitId = visitId,
                    CurrentSideOfCard = cardSide,
                    CurrentCardType = cardType,
                    NotInDatabase = true
                };

                Bitmap packedPicture;
                switch (cardType)
                { 
                    case CardType.HandCutaneousNerves:
                        packedPicture = Properties.Resources.CardHandCutaneousNerves;
                        break;
                    case CardType.HandDermatome:
                        packedPicture = Properties.Resources.CardHandDermatome;
                        break;
                    case CardType.LegCutaneousNerves:
                        packedPicture = Properties.Resources.CardLegCutaneousNerves;
                        break;
                    case CardType.LegDermatome:
                        packedPicture = Properties.Resources.CardLegDermatome;
                        break;                    
                    case CardType.PamplegiaCard:
                        packedPicture = Properties.Resources.CardPamplegiaCard;
                        break;
                    case CardType.SacriplexCard:
                        packedPicture = Properties.Resources.CardSacriplexCard;
                        break;
                    default:
                        throw new ArgumentException(cardType + " неизвестный тип карты обследования");
                }

                newCardInfo.Picture = new Bitmap(packedPicture);
                _cardList.Add(newCardInfo);
                return new CCard(newCardInfo);
            }

            return new CCard(_cardList[n]);
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
            int cardListCnt = _cardList.Count;
            for (int i = 0; i < cardListCnt; i++)
            {
                if (_cardList[i].HospitalizationId == hospitalizationId &&
                    _cardList[i].VisitId == visitId)
                {
                    var newCard = new CCard(_cardList[i]);
                    string oldFilePath = GetPicturePath(newCard.GetPictureFileName());
                    newCard.HospitalizationId = newHospitalizationId;
                    newCard.VisitId = newVisitId;
                    _cardList.Add(newCard);
                    string newFilePath = GetPicturePath(newCard.GetPictureFileName());

                    if (File.Exists(oldFilePath))
                    {
                        File.Copy(oldFilePath, newFilePath);
                    }
                    else
                    {
                        PutDefaultPictureFile(newCard.CurrentCardType, newFilePath);
                    }
                }
            }

            Save(null);
        }


        /// <summary>
        /// Получить все карты на плечевое сплетение с неправильным id госпитализации и консультации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <param name="visitWorker">Объект для работы с консультациями</param>
        /// <returns></returns>
        public List<CCard> GetWrongCards(
            CHospitalizationWorker hospitalizationWorker, CVisitWorker visitWorker)
        {
            var wrongCards = new List<CCard>();
            foreach (CCard card in _cardList)
            {
                try
                {
                    hospitalizationWorker.GetById(card.HospitalizationId);
                }
                catch
                {
                    try
                    {
                        visitWorker.GetById(card.VisitId);
                    }
                    catch
                    {
                        wrongCards.Add(card);
                    }
                }
            }

            return wrongCards;
        }
    }
}
