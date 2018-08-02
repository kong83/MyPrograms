using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class COperationTypeWorker : CBaseWorker
    {
        private List<COperationType> _operationTypeList;
        private readonly string _operationTypePath;

        public COperationTypeWorker(string dataPath)
        {
            _operationTypePath = Path.Combine(dataPath, "operation_types.save");
            Load();
        }

        
        /// <summary>
        /// Добавить новый тип операции к списку типов операций
        /// </summary>
        /// <param name="operationTypeInfo">Информация о типе операции</param>
        public void Add(COperationType operationTypeInfo)
        {
            if (GetByGeneralData(operationTypeInfo.Name) != null)
            {
                throw new Exception("Тип операции с таким названием уже существует");                
            }

            var newOperationTypeInfo = new COperationType(operationTypeInfo) { Id = GetNewID(_operationTypeList.ToArray()) };
            _operationTypeList.Add(newOperationTypeInfo);
            Save();
        }


        /// <summary>
        /// Обновить информацию о типе операции
        /// </summary>
        /// <param name="operationTypeInfo">Информация о типе операции</param>
        public void Update(COperationType operationTypeInfo)
        {
            COperationType tempOperationType = GetByGeneralData(operationTypeInfo.Name);
            if (tempOperationType != null && tempOperationType.Id != operationTypeInfo.Id)
            {
                throw new Exception("Тип операции с таким названием уже существует");
            }

            int n = 0;
            while (_operationTypeList[n].Id != operationTypeInfo.Id)
            {
                n++;
            }
            
            _operationTypeList[n] = new COperationType(operationTypeInfo);
            Save();
        }


        /// <summary>
        /// Удалить тип операции
        /// </summary>
        /// <param name="operationTypeInfoId">Информация о типе операции</param>
        public void Remove(int operationTypeInfoId)
        {
            int n = 0;
            while (_operationTypeList[n].Id != operationTypeInfoId)
            {
                n++;
            }

            _operationTypeList.RemoveAt(n);
            Save();
        }


        /// <summary>
        /// Сохранить список типов операций
        /// </summary>
        private void Save()
        {
            _operationTypeList.Sort(COperationType.Compare);

            var operationTypesStr = new StringBuilder();

            foreach (COperationType operationTypeInfo in _operationTypeList)
            {
                operationTypesStr.Append(
                    "Id=" + operationTypeInfo.Id + DataSplitStr +
                    "IdParent=" + operationTypeInfo.IdParent + DataSplitStr +
                    "Type=" + operationTypeInfo.Type + DataSplitStr +
                    "NodeFolderStated=" + operationTypeInfo.NodeFolderStated + DataSplitStr +
                    "Name=" + operationTypeInfo.Name + ObjSplitStr);
            }

            CDatabaseEngine.PackText(operationTypesStr.ToString(), _operationTypePath);
        }


        /// <summary>
        /// Загрузить список типов операций
        /// </summary>
        private void Load()
        {
            _operationTypeList = new List<COperationType>();
            string allDataStr = CDatabaseEngine.UnpackText(_operationTypePath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var operationTypeInfo = new COperationType();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            operationTypeInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Name":
                            operationTypeInfo.Name = keyValue[1];
                            break;
                        case "IdParent":
                            operationTypeInfo.IdParent = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Type":
                            operationTypeInfo.Type = (NodeType)Enum.Parse(typeof(NodeType), keyValue[1]);
                            break;
                        case "NodeFolderStated":
                            operationTypeInfo.NodeFolderStated = (NodeFolderStated)Enum.Parse(typeof(NodeFolderStated), keyValue[1]);
                            break;
                    }
                }

                _operationTypeList.Add(operationTypeInfo);
            }
        }


        /// <summary>
        /// Получить тип операции по id
        /// </summary>
        /// <param name="operationTypeId">ID типа операции</param>
        /// <returns></returns>
        public COperationType GetById(int operationTypeId)
        {
            foreach (COperationType operationType in _operationTypeList)
            {
                if (operationType.Id == operationTypeId)
                {
                    return new COperationType(operationType);
                }
            }

            throw new ArgumentException("Внутренняя ошибка программы. Нет типа операции с id=" + operationTypeId);
        }


        /// <summary>
        /// Получить список типов операций
        /// </summary>
        public COperationType[] OperationTypeList
        {
            get
            {
                return _operationTypeList.ToArray();
            }
        }


        /// <summary>
        /// Получить тип операции с указанным названием
        /// </summary>
        /// <param name="operationTypeName">Название типа операции</param>
        /// <returns></returns>
        public COperationType GetByGeneralData(string operationTypeName)
        {
            foreach (COperationType operationType in _operationTypeList)
            {
                if (operationType.Name == operationTypeName)
                {
                    return new COperationType(operationType);
                }
            }

            return null;
        }

        /// <summary>
        /// Проверяется, есть ли у папки с типом операции какие-либо вложенные операции или папки
        /// </summary>
        /// <param name="id">Id типа операции</param>
        /// <returns></returns>
        public bool IsFolderHasChilds(int id)
        {
            foreach (COperationType operationType in _operationTypeList)
            {
                if (operationType.IdParent == id)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Возвращается список вложенных операций и папок (сначала папки, потом операции)
        /// </summary>
        /// <param name="id">Id типа операции</param>
        /// <returns></returns>
        public COperationType[] GetChilds(int id)
        {
            var operations = new List<COperationType>();
            var folders = new List<COperationType>();
            foreach (COperationType operationType in _operationTypeList)
            {
                if (operationType.IdParent == id)
                {
                    if (operationType.Type == NodeType.Folder)
                    {
                        folders.Add(operationType);
                    }
                    else
                    {
                        operations.Add(operationType);
                    }
                }
            }

            foreach (COperationType operationType in operations)
            {
                folders.Add(operationType);
            }

            return folders.ToArray();
        }
    }
}
