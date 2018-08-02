using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class COrderlyWorker : CBaseWorker
    {
        private List<COrderly> _orderlyList;
        private readonly string _orderlyPath;

        public COrderlyWorker(string dataPath)
        {
            _orderlyPath = Path.Combine(dataPath, "orderlys.save");
            Load();
        }


        /// <summary>
        /// Получить список санитаров
        /// </summary>
        public COrderly[] OrderlyList
        {
            get
            {
                return _orderlyList.ToArray();
            }
        }


        /// <summary>
        /// Добавить нового санитара к списку санитаров
        /// </summary>
        /// <param name="orderlyInfo">Информация о санитре</param>
        public void Add(COrderly orderlyInfo)
        {
            var newOrderlyInfo = new COrderly(orderlyInfo) { Id = GetNewID(OrderlyList) };
            _orderlyList.Add(newOrderlyInfo);
            Save();
        }


        /// <summary>
        /// Обновить информацию о санитаре
        /// </summary>
        /// <param name="orderlyInfo">Информация о санитаре</param>
        public void Update(COrderly orderlyInfo)
        {
            int n = 0;
            while (_orderlyList[n].Id != orderlyInfo.Id)
            {
                n++;
            }

            _orderlyList[n] = new COrderly(orderlyInfo);
            Save();
        }


        /// <summary>
        /// Удалить санитара
        /// </summary>
        /// <param name="orderlyInfoId">ID санитара</param>
        public void Remove(int orderlyInfoId)
        {
            int n = 0;
            while (_orderlyList[n].Id != orderlyInfoId)
            {
                n++;
            }

            _orderlyList.RemoveAt(n);
            Save();
        }


        /// <summary>
        /// Сохранить список санитаров
        /// </summary>
        private void Save()
        {
            _orderlyList.Sort(COrderly.Compare);

            var orderlysStr = new StringBuilder();

            foreach (COrderly orderlyInfo in _orderlyList)
            {
                orderlysStr.Append(
                    "Id=" + orderlyInfo.Id + DataSplitStr +
                    "Name=" + orderlyInfo.Name + ObjSplitStr);
            }

            CDatabaseEngine.PackText(orderlysStr.ToString(), _orderlyPath);
        }


        /// <summary>
        /// Загрузить список санитаров
        /// </summary>
        private void Load()
        {
            _orderlyList = new List<COrderly>();
            string allDataStr = CDatabaseEngine.UnpackText(_orderlyPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var orderlyInfo = new COrderly();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            orderlyInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Name":
                            orderlyInfo.Name = keyValue[1];
                            break;
                    }
                }

                _orderlyList.Add(orderlyInfo);
            }
        }
    }
}
