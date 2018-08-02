using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CSurgeonWorker : CBaseWorker
    {
        private List<CSurgeon> _surgeonList;
        private readonly string _surgeonPath;

        public CSurgeonWorker(string dataPath)
        {
            _surgeonPath = Path.Combine(dataPath, "surgeons.save");
            Load();
        }


        /// <summary>
        /// Получить список хирургов
        /// </summary>
        public CSurgeon[] SurgeonList
        {
            get
            {
                return _surgeonList.ToArray();
            }
        }


        /// <summary>
        /// Добавить нового хирурга к списку хирургов
        /// </summary>
        /// <param name="surgeonInfo">Информация о хирурге</param>
        public void Add(CSurgeon surgeonInfo)
        {
            var newSurgeonInfo = new CSurgeon(surgeonInfo) { Id = GetNewID(SurgeonList) };
            _surgeonList.Add(newSurgeonInfo);
            Save();
        }


        /// <summary>
        /// Обновить информацию о хирурге
        /// </summary>
        /// <param name="surgeonInfo">Информация о хирурге</param>
        public void Update(CSurgeon surgeonInfo)
        {
            int n = 0;
            while (_surgeonList[n].Id != surgeonInfo.Id)
            {
                n++;
            }

            _surgeonList[n] = new CSurgeon(surgeonInfo);
            Save();
        }


        /// <summary>
        /// Удалить хирурга
        /// </summary>
        /// <param name="surgeonInfoId">ID хирурга</param>
        public void Remove(int surgeonInfoId)
        {
            int n = 0;
            while (_surgeonList[n].Id != surgeonInfoId)
            {
                n++;
            }

            _surgeonList.RemoveAt(n);
            Save();
        }


        /// <summary>
        /// Сохранить список хирургов
        /// </summary>
        private void Save()
        {
            _surgeonList.Sort(CSurgeon.Compare);

            var surgeonsStr = new StringBuilder();

            foreach (CSurgeon surgeonInfo in _surgeonList)
            {
                surgeonsStr.Append(
                    "Id=" + surgeonInfo.Id + DataSplitStr +
                    "Header=" + surgeonInfo.Header + DataSplitStr +
                    "Name=" + surgeonInfo.Name + ObjSplitStr);
            }

            CDatabaseEngine.PackText(surgeonsStr.ToString(), _surgeonPath);
        }


        /// <summary>
        /// Загрузить список хирургов
        /// </summary>
        private void Load()
        {
            _surgeonList = new List<CSurgeon>();
            string allDataStr = CDatabaseEngine.UnpackText(_surgeonPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var surgeonInfo = new CSurgeon();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            surgeonInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Name":
                            surgeonInfo.Name = keyValue[1];
                            break;
                        case "Header":
                            surgeonInfo.Header = keyValue[1];
                            break;
                    }
                }

                _surgeonList.Add(surgeonInfo);
            }
        }


        /// <summary>
        /// Получить значение шапки для указанного врача
        /// </summary>
        /// <param name="surgeonName">Имя хирурга</param>
        /// <returns></returns>
        public string GetHeaderByName(string surgeonName)
        {
            foreach (CSurgeon surgeon in _surgeonList)
            {
                if (surgeon.Name == surgeonName)
                {
                    return surgeon.Header;
                }
            }

            return string.Empty;
        }
    }
}
