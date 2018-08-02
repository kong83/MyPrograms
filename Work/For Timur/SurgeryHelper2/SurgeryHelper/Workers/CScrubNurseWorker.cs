using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CScrubNurseWorker : CBaseWorker
    {
        private List<CScrubNurse> _scrubNurseList;
        private readonly string _scrubNursePath;

        public CScrubNurseWorker(string dataPath)
        {
            _scrubNursePath = Path.Combine(dataPath, "scrub_nurses.save");
            Load();
        }


        /// <summary>
        /// Получить список операционных мед. сестёр
        /// </summary>
        public CScrubNurse[] ScrubNurseList
        {
            get
            {
                return _scrubNurseList.ToArray();
            }
        }


        /// <summary>
        /// Добавить новую операционную мед. сестру к списку операционных мед. сестёр
        /// </summary>
        /// <param name="scrubNurseInfo">Информация по опер. мед. сестре</param>
        public void Add(CScrubNurse scrubNurseInfo)
        {
            var newScrubNurseInfo = new CScrubNurse(scrubNurseInfo) { Id = GetNewID(ScrubNurseList) };
            _scrubNurseList.Add(newScrubNurseInfo);
            Save();
        }


        /// <summary>
        /// Обновить информацию о операционной мед. сестре
        /// </summary>
        /// <param name="scrubNurseInfo">Информация по опер. мед. сестре</param>
        public void Update(CScrubNurse scrubNurseInfo)
        {
            int n = 0;
            while (_scrubNurseList[n].Id != scrubNurseInfo.Id)
            {
                n++;
            }

            _scrubNurseList[n] = new CScrubNurse(scrubNurseInfo);
            Save();
        }


        /// <summary>
        /// Удалить операционную мед. сестру
        /// </summary>
        /// <param name="scrubNurseInfoId">ID опер. мед. сестры</param>
        public void Remove(int scrubNurseInfoId)
        {
            int n = 0;
            while (_scrubNurseList[n].Id != scrubNurseInfoId)
            {
                n++;
            }

            _scrubNurseList.RemoveAt(n);
            Save();
        }


        /// <summary>
        /// Сохранить список операционных мед. сестёр
        /// </summary>
        private void Save()
        {
            _scrubNurseList.Sort(CScrubNurse.Compare);

            var scrubNursesStr = new StringBuilder();

            foreach (CScrubNurse scrubNurseInfo in _scrubNurseList)
            {
                scrubNursesStr.Append(
                    "Id=" + scrubNurseInfo.Id + DataSplitStr +
                    "Name=" + scrubNurseInfo.Name + ObjSplitStr);
            }

            CDatabaseEngine.PackText(scrubNursesStr.ToString(), _scrubNursePath);
        }


        /// <summary>
        /// Загрузить список операционных сестёр
        /// </summary>
        private void Load()
        {
            _scrubNurseList = new List<CScrubNurse>();
            string allDataStr = CDatabaseEngine.UnpackText(_scrubNursePath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var scrubNurseInfo = new CScrubNurse();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            scrubNurseInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Name":
                            scrubNurseInfo.Name = keyValue[1];
                            break;
                    }
                }

                _scrubNurseList.Add(scrubNurseInfo);
            }
        }
    }
}
