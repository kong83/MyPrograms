using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

using SurgeryHelper.Workers;

namespace SurgeryHelper.Tools
{
    public static class CConvertEngine
    {
        /// <summary>
        /// Преобразует объект типа DateTime в строку с датой вида 11.12.1111
        /// </summary>
        /// <param name="dateTime">Объект DateTime для преобразования</param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime dateTime)
        {
            return DateTimeToString(dateTime, false);
        }


        /// <summary>
        /// Преобразует объект типа DateTime? в строку с датой вида 11.12.1111
        /// </summary>
        /// <param name="dateTime">Объект DateTime? для преобразования</param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime? dateTime)
        {
            return DateTimeToString(dateTime, false);
        }


        /// <summary>
        /// Преобразует объект типа DateTime? в строку с датой вида 11.12.1111 11:11
        /// </summary>
        /// <param name="dateTime">Объект DateTime? для преобразования</param>
        /// <param name="isNeedTime">Надо ли включать в строку время</param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime? dateTime, bool isNeedTime)
        {
            if (dateTime.HasValue)
            {
                return DateTimeToString(dateTime.Value, isNeedTime);
            }

            return string.Empty;
        }


        /// <summary>
        /// Преобразует объект типа DateTime в строку с датой вида 11.12.1111 11:11
        /// </summary>
        /// <param name="dateTime">Объект DateTime для преобразования</param>
        /// <param name="isNeedTime">Надо ли включать в строку время</param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime dateTime, bool isNeedTime)
        {
            var res = new StringBuilder();

            res.Append(dateTime.Day.ToString("D2") + "." + dateTime.Month.ToString("D2") + "." + dateTime.Year.ToString("D4"));

            if (isNeedTime)
            {
                res.Append(" " + TimeToString(dateTime));
            }

            return res.ToString();
        }


        /// <summary>
        /// Преобразует объект типа DateTime в строку со временем вида 11:11
        /// </summary>
        /// <param name="dateTime">Объект DateTime для преобразования</param>
        /// <returns></returns>
        public static string TimeToString(DateTime dateTime)
        {
            return dateTime.Hour.ToString("D2") + ":" + dateTime.Minute.ToString("D2");
        }


        /// <summary>
        /// Преобразует объект типа DateTime в строку со временем вида 11:11
        /// </summary>
        /// <param name="dateTime">Объект DateTime для преобразования</param>
        /// <returns></returns>
        public static string TimeToString(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return TimeToString(dateTime.Value);
            }

            return string.Empty;
        }
       

        /// <summary>
        /// Возвращает объект DateTime из строки, содержащей дату, время или дату и время в обычном или в английском представлении 
        /// </summary>
        /// <param name="dateTimeStr">Строка с датой и временем</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string dateTimeStr)
        {
            var dateTimeparser = new CDateTimeParser();
            string[] dateAndTime = dateTimeStr.Split(' ');
            
            if (dateAndTime[0].Contains(".") && dateAndTime.Length == 1)
            {
                return dateTimeparser.ParseRusDate(dateAndTime[0]);
            }

            if (dateAndTime[0].Contains(".") && dateAndTime.Length == 2)
            {
                return dateTimeparser.ParseRusDateTime(dateAndTime[0], dateAndTime[1]);
            }

            if (dateAndTime[0].Contains("/") && dateAndTime.Length == 1)
            {
                return dateTimeparser.ParseEngDate(dateAndTime[0]);
            }

            if (dateAndTime[0].Contains("/") && dateAndTime.Length == 2)
            {
                return dateTimeparser.ParseEngDateTime(dateAndTime[0], dateAndTime[1]);
            }

            if (dateAndTime[0].Contains(":") && dateAndTime.Length == 1)
            {
                return dateTimeparser.ParseTime(dateAndTime[0]);
            }

            throw new Exception("Строка " + dateTimeStr + " не распознана как тип DateTime");
        }       


        /// <summary>
        /// Перевести строку, разделённую listSplitStr в список строк
        /// </summary>
        /// <param name="data">Данные, разделённые listSplitStr</param>
        /// <returns></returns>
        public static List<string> StringToStringList(string data)
        {
            var list = new List<string>();

            if (!string.IsNullOrEmpty(data))
            {
                string[] parts = data.Split(new[] { CBaseWorker.ListSplitStr }, StringSplitOptions.None);

                foreach (string part in parts)
                {
                    list.Add(part);
                }
            }

            return list;
        }


        /// <summary>
        /// Перевести строку, разделённую listSplitStr в массив строк
        /// </summary>
        /// <param name="data">Данные, разделённые listSplitStr</param>
        /// <returns></returns>
        public static string[] StringToStringArray(string data)
        {
            List<string> list = StringToStringList(data);
            return list.ToArray();
        }


        /// <summary>
        /// Перевести строку, разделённую listSplitStr в массив с bool-ами
        /// </summary>
        /// <param name="data">Данные, разделённые listSplitStr</param>
        /// <returns></returns>
        public static bool[] StringToBoolArray(string data)
        {
            var list = new List<bool>();
            if (!string.IsNullOrEmpty(data))
            {
                string[] parts = data.Split(new[] { CBaseWorker.ListSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    list.Add(Convert.ToBoolean(part));
                }
            }

            return list.ToArray();
        }


        /// <summary>
        /// Перевести строку, разделённую listSplitStr в массив с числами
        /// </summary>
        /// <param name="data">Данные, разделённые listSplitStr</param>
        /// <returns></returns>
        public static int[] StringToIntArray(string data)
        {
            var list = new List<int>();
            if (!string.IsNullOrEmpty(data))
            {
                string[] parts = data.Split(new[] { CBaseWorker.ListSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string part in parts)
                {
                    list.Add(Convert.ToInt32(part));
                }
            }

            return list.ToArray();
        }


        /// <summary>
        /// Перевести список строк в строку, разделённую listSplitStr
        /// </summary>
        /// <param name="list">Массив строк</param>
        /// <returns></returns>
        public static string ListToString(IEnumerable<string> list)
        {
            return ListToString(list, CBaseWorker.ListSplitStr);
        }


        /// <summary>
        /// Перевести список строк в строку, разделённую listSplitStr
        /// </summary>
        /// <param name="list">Массив строк</param>
        /// <param name="separator">Строка, через которую будут записаны элементы массива</param>
        /// <returns></returns>
        public static string ListToString(IEnumerable<string> list, string separator)
        {
            var tempList = new List<string>(list);
            return string.Join(separator, tempList.ToArray());
        }


        /// <summary>
        /// Перевести список bool-ов в строку, разделённую listSplitStr
        /// </summary>
        /// <param name="list">Список из элементов типа bool</param>
        /// <returns></returns>
        public static string ListToString(IEnumerable<bool> list)
        {
            var tempList = new List<string>();
            foreach (bool str in list)
            {
                tempList.Add(str.ToString(CultureInfo.InvariantCulture));
            }

            return string.Join(CBaseWorker.ListSplitStr, tempList.ToArray());
        }


        /// <summary>
        /// Перевести список чисел в строку, разделённую listSplitStr
        /// </summary>
        /// <param name="list">Cписок чисел</param>
        /// <returns></returns>
        public static string ListToString(IEnumerable<int> list)
        {
            var tempList = new List<string>();
            foreach (int str in list)
            {
                tempList.Add(str.ToString(CultureInfo.InvariantCulture));
            }

            return string.Join(CBaseWorker.ListSplitStr, tempList.ToArray());
        }


        /// <summary>
        /// Сделать копию переменной с датой и временем
        /// </summary>
        /// <param name="value">Переменная с датой и временем</param>
        /// <returns></returns>
        public static DateTime? CopyDateTime(DateTime? value)
        {
            if (value.HasValue)
            {
                return CopyDateTime(value.Value);
            }

            return null;
        }


        /// <summary>
        /// Сделать копию переменной с датой и временем
        /// </summary>
        /// <param name="value">Переменная с датой и временем</param>
        /// <returns></returns>
        public static DateTime CopyDateTime(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
        }


        /// <summary>
        /// Скопировать массив строк
        /// </summary>
        /// <param name="fromObj">Массив строк</param>
        /// <returns></returns>
        public static string[] CopyArray(string[] fromObj)
        {
            var temp = new string[fromObj.Length];
            fromObj.CopyTo(temp, 0);
            return temp;
        }


        /// <summary>
        /// Скопировать массив чисел
        /// </summary>
        /// <param name="fromObj">Массив чисел</param>
        /// <returns></returns>
        public static int[] CopyArray(int[] fromObj)
        {
            var temp = new int[fromObj.Length];
            fromObj.CopyTo(temp, 0);
            return temp;
        }


        /// <summary>
        /// Скопировать массив с данными типа bool
        /// </summary>
        /// <param name="fromObj">Массив с данными типа bool</param>
        /// <returns></returns>
        public static bool[] CopyArray(bool[] fromObj)
        {
            var temp = new bool[fromObj.Length];
            fromObj.CopyTo(temp, 0);
            return temp;
        }


        /// <summary>
        /// Получить полный путь для указанного пути. Если он относительный - то в начало 
        /// добавляется путь до папки с исполняемым файлом
        /// </summary>
        /// <param name="privateFolderPath">Полный или относительный путь до личной папки</param>
        /// <returns></returns>
        public static string GetFullPrivateFolderPath(string privateFolderPath)
        {
            string realDirectoryName;
            if (privateFolderPath.Length > 1 && privateFolderPath[1] == ':')
            {
                realDirectoryName = privateFolderPath;
            }
            else
            {
                realDirectoryName = Path.Combine(Application.StartupPath, privateFolderPath);
            }

            return realDirectoryName;
        }


        /// <summary>
        /// Удаление одинаковых записей из строки, разделённой переносами строк
        /// </summary>
        /// <param name="str">Строка, разделённая переносами строк</param>
        /// <returns></returns>
        public static string RemoveDuplicates(string str)
        {
            var words = new List<string>(str.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

            if (words.Count == 0)
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            int i = 0;
            while (i < words.Count)
            {
                result.Append(words[i] + "\r\n");
                int j = i + 1;
                while (j < words.Count)
                {
                    if (words[i] == words[j])
                    {
                        words.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }
                }

                i++;
            }

            return result.ToString().Remove(result.Length - 2);
        }


        /// <summary>
        /// Сконвертировать строку в тип CardLeftRight
        /// </summary>
        /// <param name="str">Строка для конвертации</param>
        /// <returns></returns>
        public static CardSide StringToCardSide(string str)
        {
            switch (str.ToLower())
            {
                case "left":
                    return CardSide.Left;
                case "right":
                    return CardSide.Right;
                default:
                    throw new ArgumentException(str + " не является типом CardSide");
            }
        }


        /// <summary>
        /// Сконвертировать строку в тип CardType
        /// </summary>
        /// <param name="str">Строка для конвертации</param>
        /// <returns></returns>
        public static CardType StringToCardType(string str)
        {
            switch (str.ToLower())
            {
                case "handcutaneousnerves":
                    return CardType.HandCutaneousNerves;
                case "handdermatome":
                    return CardType.HandDermatome;
                case "legcutaneousnerves":
                    return CardType.LegCutaneousNerves;
                case "legdermatome":
                    return CardType.LegDermatome;
                case "pamplegiacard":
                    return CardType.PamplegiaCard;
                case "sacriplexcard":
                    return CardType.SacriplexCard;
                default:
                    throw new ArgumentException(str + " не является типом CardType");
            }
        }


        /// <summary>
        /// Получить возраст из даты рождения
        /// </summary>
        /// <param name="dateBirthday">Дата рождения</param>
        /// <returns></returns>
        public static string GetAge(DateTime dateBirthday)
        {
            DateTime temp = CopyDateTime(dateBirthday);
            int yearCnt = 0;
            while (temp.Date < DateTime.Now.Date)
            {
                temp = temp.AddYears(1);
                yearCnt++;
            }

            if (yearCnt > 0)
            {
                return (--yearCnt).ToString(CultureInfo.InvariantCulture);
            }

            return "0";
        }
    }
}
