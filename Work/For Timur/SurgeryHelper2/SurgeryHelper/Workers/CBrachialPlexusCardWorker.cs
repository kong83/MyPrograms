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
    public class CBrachialPlexusCardWorker : CBaseWorker
    {
        private List<CBrachialPlexusCard> _brachialPlexusCardList;
        private readonly string _brachialPlexusCardPath;

        public CBrachialPlexusCardWorker(string dataPath)
        {
            _brachialPlexusCardPath = Path.Combine(dataPath, "brachial_plexus_card.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию о карте обследования
        /// </summary>
        /// <param name="brachialPlexusCardInfo">Информация о карте обследования</param>
        public void Update(CBrachialPlexusCard brachialPlexusCardInfo)
        {
            int n = GetIndexFromList(brachialPlexusCardInfo.HospitalizationId, brachialPlexusCardInfo.VisitId);
            brachialPlexusCardInfo.NotInDatabase = false;
            _brachialPlexusCardList[n] = new CBrachialPlexusCard(brachialPlexusCardInfo);
            Save(brachialPlexusCardInfo);
        }


        /// <summary>
        /// Удалить карту обследования
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        public void Remove(int hospitalizationId, int visitId)
        {
            int index = 0;
            while (index < _brachialPlexusCardList.Count)
            {
                if (_brachialPlexusCardList[index].HospitalizationId == hospitalizationId &&
                    _brachialPlexusCardList[index].VisitId == visitId)
                {
                    string fileName = GetPicturePath(_brachialPlexusCardList[index].GetPictureFileName());
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    _brachialPlexusCardList.RemoveAt(index);
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
            return Path.Combine(Path.GetDirectoryName(_brachialPlexusCardPath) ?? string.Empty, "CardPictures\\" + pictureFileName);
        }


        /// <summary>
        /// Сохранить список карт обследования
        /// </summary>
        /// <param name="saveBrachialPlexusCardInfo">Информация о карте, которую сохраняем</param>
        private void Save(CBrachialPlexusCard saveBrachialPlexusCardInfo)
        {
            var brachialPlexusCardsStr = new StringBuilder();

            foreach (CBrachialPlexusCard brachialPlexusCardInfo in _brachialPlexusCardList)
            {
                brachialPlexusCardsStr.Append(
                    "HospitalizationId=" + brachialPlexusCardInfo.HospitalizationId + DataSplitStr +
                    "VisitId=" + brachialPlexusCardInfo.VisitId + DataSplitStr +
                    "Diaphragm=" + brachialPlexusCardInfo.Diaphragm + DataSplitStr +
                    "EMNG=" + brachialPlexusCardInfo.EMNG + DataSplitStr +
                    "EMNGDate=" + CConvertEngine.DateTimeToString(brachialPlexusCardInfo.EMNGDate) + DataSplitStr +
                    "HornersSyndrome=" + brachialPlexusCardInfo.HornersSyndrome + DataSplitStr +
                    "IsEMNGEnabled=" + brachialPlexusCardInfo.IsEMNGEnabled + DataSplitStr +
                    "IsMyelographyEnabled=" + brachialPlexusCardInfo.IsMyelographyEnabled + DataSplitStr +
                    "Myelography=" + brachialPlexusCardInfo.Myelography + DataSplitStr +
                    "MyelographyDate=" + CConvertEngine.DateTimeToString(brachialPlexusCardInfo.MyelographyDate) + DataSplitStr +
                    "MyelographyType=" + brachialPlexusCardInfo.MyelographyType + DataSplitStr +
                    "TinelsSymptom=" + brachialPlexusCardInfo.TinelsSymptom + DataSplitStr +
                    "VascularStatus=" + brachialPlexusCardInfo.VascularStatus + DataSplitStr +                    
                    "CardSide=" + brachialPlexusCardInfo.SideOfCard + ObjSplitStr);

                if (saveBrachialPlexusCardInfo != null && saveBrachialPlexusCardInfo.IsEqual(brachialPlexusCardInfo))
                {
                    CDatabaseEngine.SaveBitmapToFile(
                        brachialPlexusCardInfo.Picture,
                        GetPicturePath(brachialPlexusCardInfo.GetPictureFileName()));
                }
            }

            CDatabaseEngine.PackText(brachialPlexusCardsStr.ToString(), _brachialPlexusCardPath);
        }


        /// <summary>
        /// Загрузить список карт обследования
        /// </summary>
        private void Load()
        {
            _brachialPlexusCardList = new List<CBrachialPlexusCard>();
            string allDataStr = CDatabaseEngine.UnpackText(_brachialPlexusCardPath);

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

                    var brachialPlexusCardInfo = new CBrachialPlexusCard();
                    foreach (string dataStr in datasStr)
                    {
                        string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                        switch (keyValue[0])
                        {
                            case "HospitalizationId":
                                brachialPlexusCardInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "VisitId":
                                brachialPlexusCardInfo.VisitId = Convert.ToInt32(keyValue[1]);
                                break;
                            case "Diaphragm":
                                brachialPlexusCardInfo.Diaphragm = keyValue[1];
                                break;
                            case "EMNG":
                                brachialPlexusCardInfo.EMNG = keyValue[1];
                                break;
                            case "EMNGDate":
                                brachialPlexusCardInfo.EMNGDate = CConvertEngine.StringToDateTime(keyValue[1]);
                                break;
                            case "HornersSyndrome":
                                brachialPlexusCardInfo.HornersSyndrome = keyValue[1];
                                break;
                            case "IsEMNGEnabled":
                                brachialPlexusCardInfo.IsEMNGEnabled = Convert.ToBoolean(keyValue[1]);
                                break;
                            case "IsMyelographyEnabled":
                                brachialPlexusCardInfo.IsMyelographyEnabled = Convert.ToBoolean(keyValue[1]);
                                break;
                            case "Myelography":
                                brachialPlexusCardInfo.Myelography = keyValue[1];
                                break;
                            case "MyelographyDate":
                                brachialPlexusCardInfo.MyelographyDate = CConvertEngine.StringToDateTime(keyValue[1]);
                                break;
                            case "MyelographyType":
                                brachialPlexusCardInfo.MyelographyType = keyValue[1];
                                break;
                            case "TinelsSymptom":
                                brachialPlexusCardInfo.TinelsSymptom = keyValue[1];
                                break;
                            case "VascularStatus":
                                brachialPlexusCardInfo.VascularStatus = keyValue[1];
                                break;
                            case "CardSide":
                                brachialPlexusCardInfo.SideOfCard = CConvertEngine.StringToCardSide(keyValue[1]);
                                break;                            
                        }
                    }

                    string picturePath = GetPicturePath(brachialPlexusCardInfo.GetPictureFileName());
                    if (!File.Exists(picturePath))
                    {
                        PutDefaultPictureFile(picturePath);
                    }

                    brachialPlexusCardInfo.Picture = CDatabaseEngine.GetBitmapFromFile(picturePath);

                    _brachialPlexusCardList.Add(brachialPlexusCardInfo);
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
        /// Поместить в файл с картинкой дефолтную картинку
        /// </summary>
        /// <param name="picturePath">Полный путь до файла с картинкой</param>
        private static void PutDefaultPictureFile(string picturePath)
        {
            CDatabaseEngine.SaveBitmapToFile(Properties.Resources.CardMainSchema, picturePath);
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
            while (n < _brachialPlexusCardList.Count &&
                (_brachialPlexusCardList[n].HospitalizationId != hospitalizationId ||
                _brachialPlexusCardList[n].VisitId != visitId))
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
            if (GetIndexFromList(hospitalizationId, visitId) != _brachialPlexusCardList.Count)
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
        public CBrachialPlexusCard GetByHospitalizationAndVisitId(
            int hospitalizationId, int visitId)
        {
            int n = GetIndexFromList(hospitalizationId, visitId);

            if (n == _brachialPlexusCardList.Count)
            {
                var newBrachialPlexusCardInfo = new CBrachialPlexusCard(hospitalizationId, visitId)
                { 
                    Picture = new Bitmap(Properties.Resources.CardMainSchema),
                    NotInDatabase = true
                
                };
                _brachialPlexusCardList.Add(newBrachialPlexusCardInfo);
                return new CBrachialPlexusCard(newBrachialPlexusCardInfo);
            }

            return new CBrachialPlexusCard(_brachialPlexusCardList[n]);
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
            int brachialPlexusCardListCnt = _brachialPlexusCardList.Count;
            for (int i = 0; i < brachialPlexusCardListCnt; i++)
            {
                if (_brachialPlexusCardList[i].HospitalizationId == hospitalizationId &&
                    _brachialPlexusCardList[i].VisitId == visitId)
                {
                    var newBrachialPlexusCard = new CBrachialPlexusCard(_brachialPlexusCardList[i]);
                    string oldFilePath = GetPicturePath(newBrachialPlexusCard.GetPictureFileName());
                    newBrachialPlexusCard.HospitalizationId = newHospitalizationId;
                    newBrachialPlexusCard.VisitId = newVisitId;
                    _brachialPlexusCardList.Add(newBrachialPlexusCard);
                    string newFilePath = GetPicturePath(newBrachialPlexusCard.GetPictureFileName());

                    if (File.Exists(oldFilePath))
                    {
                        File.Copy(oldFilePath, newFilePath);
                    }
                    else
                    {
                        PutDefaultPictureFile(newFilePath);
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
        public List<CBrachialPlexusCard> GetWrongBrachialPlexusCards(
            CHospitalizationWorker hospitalizationWorker, CVisitWorker visitWorker)
        {
            var wrongBrachialPlexusCards = new List<CBrachialPlexusCard>();
            foreach (CBrachialPlexusCard brachialPlexusCard in _brachialPlexusCardList)
            {
                try
                {
                    hospitalizationWorker.GetById(brachialPlexusCard.HospitalizationId);
                }
                catch
                {
                    try
                    {
                        visitWorker.GetById(brachialPlexusCard.VisitId);
                    }
                    catch
                    {
                        wrongBrachialPlexusCards.Add(brachialPlexusCard);
                    }
                }
            }

            return wrongBrachialPlexusCards;
        }
    }
}
