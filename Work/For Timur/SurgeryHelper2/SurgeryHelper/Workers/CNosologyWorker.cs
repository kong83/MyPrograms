using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CNosologyWorker : CBaseWorker
    {
        private List<CNosology> _nosologyList;
        private readonly string _nosologyPath;

        /// <summary>
        /// Получить список нозологий
        /// </summary>
        public CNosology[] NosologyList
        {
            get
            {
                return _nosologyList.ToArray();
            }
        }


        public CNosologyWorker(string dataPath)
        {
            _nosologyPath = Path.Combine(dataPath, "nosologys.save");
            Load();
        }

        
        /// <summary>
        /// Добавить новую нозологию к списку нозологий
        /// </summary>
        /// <param name="nosologyInfo">Информация по нозологии</param>
        public void Add(CNosology nosologyInfo)
        {
            if (GetByGeneralData(nosologyInfo.Name) != null)
            {
                throw new Exception("Нозология с таким названием уже существует");                
            }

            var newNosologyInfo = new CNosology(nosologyInfo) { Id = GetNewID(NosologyList) };
            _nosologyList.Add(newNosologyInfo);

            UpdateParentType(nosologyInfo.IdParent);

            Save();
        }


        /// <summary>
        /// Обновить информацию о нозологии
        /// </summary>
        /// <param name="nosologyInfo">Информация по нозологии</param>
        public void Update(CNosology nosologyInfo)
        {
            CNosology tempNosology = GetByGeneralData(nosologyInfo.Name);
            if (tempNosology != null && tempNosology.Id != nosologyInfo.Id)
            {
                throw new Exception("Нозология с таким названием уже существует");
            }

            int n = GetIndexFromNosologyListById(nosologyInfo.Id);
            int idParent = _nosologyList[n].IdParent;
            _nosologyList[n] = new CNosology(nosologyInfo);

            UpdateParentType(idParent);
            if (idParent != nosologyInfo.IdParent)
            {
                UpdateParentType(nosologyInfo.IdParent);
            }

            Save();
        }
        

        /// <summary>
        /// Удалить нозологию
        /// </summary>
        /// <param name="nosologyInfoId">Информация по нозологии</param>
        public void Remove(int nosologyInfoId)
        {
            int n = GetIndexFromNosologyListById(nosologyInfoId);

            int idParent = _nosologyList[n].IdParent;

            _nosologyList.RemoveAt(n);

            UpdateParentType(idParent);

            Save();
        }

        private int GetIndexFromNosologyListById(int nosologyInfoId)
        {
            int n = 0;
            while (_nosologyList[n].Id != nosologyInfoId)
            {
                n++;
            }

            return n;
        }

        private void UpdateParentType(int idParent)
        {
            if (idParent == -1)
            {
                return;
            }

            int n = GetIndexFromNosologyListById(idParent);

            if (GetChilds(_nosologyList[n].Id).Length == 0)
            {
                
                _nosologyList[n].Type = NodeType.Type;
                _nosologyList[n].NodeFolderStated = NodeFolderStated.Closed;
            }
            else
            {
                _nosologyList[n].Type = NodeType.Folder;
            }
        }

        /// <summary>
        /// Получить нозологию по id
        /// </summary>
        /// <param name="nosologyId">ID нозологии</param>
        /// <returns></returns>
        public CNosology GetById(int nosologyId)
        {
            foreach (CNosology nosology in _nosologyList)
            {
                if (nosology.Id == nosologyId)
                {
                    return new CNosology(nosology);
                }
            }

            throw new ArgumentException("Внутренняя ошибка программы. Нет нозологии с id=" + nosologyId);
        }


        /// <summary>
        /// Сохранить список нозологий
        /// </summary>
        private void Save()
        {
            _nosologyList.Sort(CNosology.Compare);

            var nosologysStr = new StringBuilder();

            foreach (CNosology nosologyInfo in _nosologyList)
            {
                nosologysStr.Append(
                    "Id=" + nosologyInfo.Id + DataSplitStr +
                    "IdParent=" + nosologyInfo.IdParent + DataSplitStr +
                    "Type=" + nosologyInfo.Type + DataSplitStr +
                    "NodeFolderStated=" + nosologyInfo.NodeFolderStated + DataSplitStr +
                    "Name=" + nosologyInfo.Name + ObjSplitStr);
            }

            CDatabaseEngine.PackText(nosologysStr.ToString(), _nosologyPath);
        }


        /// <summary>
        /// Загрузить список нозологий
        /// </summary>
        private void Load()
        {
            _nosologyList = new List<CNosology>();
            string allDataStr = CDatabaseEngine.UnpackText(_nosologyPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var nosologyInfo = new CNosology();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            nosologyInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Name":
                            nosologyInfo.Name = keyValue[1];
                            break;
                        case "IdParent":
                            nosologyInfo.IdParent = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Type":
                            nosologyInfo.Type = (NodeType)Enum.Parse(typeof(NodeType), keyValue[1]);
                            break;
                        case "NodeFolderStated":
                            nosologyInfo.NodeFolderStated = (NodeFolderStated)Enum.Parse(typeof(NodeFolderStated), keyValue[1]);
                            break;
                    }
                }

                _nosologyList.Add(nosologyInfo);
            }
        }


        /// <summary>
        /// Получить нозологию с указанным названием
        /// </summary>
        /// <param name="nosologyName">Название нозологии</param>
        /// <returns></returns>
        public CNosology GetByGeneralData(string nosologyName)
        {
            foreach (CNosology nosology in _nosologyList)
            {
                if (nosology.Name == nosologyName)
                {
                    return new CNosology(nosology);
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращается список вложенных нозологий и папок (сначала папки, потом нозологии)
        /// </summary>
        /// <param name="id">Id нозологии</param>
        /// <returns></returns>
        public CNosology[] GetChilds(int id)
        {
            var nosologies = new List<CNosology>();
            var folders = new List<CNosology>();
            foreach (CNosology operationType in _nosologyList)
            {
                if (operationType.IdParent == id)
                {
                    if (operationType.Type == NodeType.Folder)
                    {
                        folders.Add(operationType);
                    }
                    else
                    {
                        nosologies.Add(operationType);
                    }
                }
            }

            foreach (CNosology nosology in nosologies)
            {
                folders.Add(nosology);
            }

            return folders.ToArray();
        }

        
        /// <summary>
        /// Проверяется, есть ли у папки с типом операции какие-либо вложенные операции или папки
        /// </summary>
        /// <param name="id">Id типа операции</param>
        /// <returns></returns>
        public bool IsFolderHasChilds(int id)
        {
            foreach (CNosology nosology in _nosologyList)
            {
                if (nosology.IdParent == id)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получить полный список нозологий для пациента по переданным ключевым нозологиям
        /// </summary>
        /// <param name="keyNosologies">Ключевые (конечный) нозологии</param>
        /// <returns></returns>
        public string GetNosologyDisplayName(List<string> keyNosologies)
        {
            string displayName = ", ";

            foreach (string keyNosology in keyNosologies)
            {
                var nameParts = new List<string> { keyNosology };
                CNosology nosology = GetByGeneralData(keyNosology);
                
                if (nosology == null)
                {
                    return string.Empty;
                }

                while (nosology.IdParent != -1)
                {
                    nosology = GetById(nosology.IdParent);
                    nameParts.Add(nosology.Name);
                }

                for (int i = nameParts.Count - 1; i >= 0; i--)
                {
                    if (!displayName.Contains(", " + nameParts[i] + ","))
                    {
                        displayName += string.Format("{0}, ", nameParts[i]);
                    }
                }
            }

            if (displayName.Length > 2)
            {
                return displayName.Substring(2, displayName.Length - 4);
            }

            return string.Empty;
        }
    }
}
