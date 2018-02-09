using System;
using System.Collections.Generic;

namespace Fonotec
{
    /// <summary>
    /// Возможные типы дисков
    /// </summary>
    public enum DiskType
    {
        /// <summary>
        /// Наш неперезаписываемый диск
        /// </summary>
        OwnR,

        /// <summary>
        /// Наш перезаписываемый диск
        /// </summary>
        OwnRW,

        /// <summary>
        /// Купленный диск
        /// </summary>
        Buy,

        /// <summary>
        /// Неопределённое значение для типа диска
        /// </summary>
        Empty
    }

    /// <summary>
    /// Возможные типы сортировки
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// Сортировка по возрастанию
        /// </summary>
        Increase,

        /// <summary>
        /// Сортировка по убыванию
        /// </summary>
        Decrease
    }

    public enum GenreType
    {
        /// <summary>
        /// Фильмы про жизнь, без ярко выраженного жанра
        /// </summary>
        AboutLife,

        /// <summary>
        /// Сказка (не мультик, но и не фэнтэзи :) )
        /// </summary>
        Tail,

        /// <summary>
        /// Детективные фильмы
        /// </summary>
        Detective,

        /// <summary>
        /// Комедийный фильм
        /// </summary>
        Comedy,

        /// <summary>
        /// Фильм боевик
        /// </summary>
        Action,

        /// <summary>
        /// Мультипликационный фильм
        /// </summary>
        Cartoon,

        /// <summary>
        /// Фильм ужасов
        /// </summary>
        Horror,

        /// <summary>
        /// Фильм триллер
        /// </summary>
        Triller,

        /// <summary>
        /// Мелодраматический фильм
        /// </summary>
        Melodrama,

        /// <summary>
        /// Исторические фильмы, которые нравятся только Ане
        /// </summary>
        History,

        /// <summary>
        /// Фильмы в жанре фэнтэзи
        /// </summary>
        Fantasy,

        /// <summary>
        /// Фильмы в жанре фантастики
        /// </summary>
        Fantastic,

        /// <summary>
        /// Фильм, не подходящий под какую-то определённую категорию
        /// </summary>
        Other
    }

    /// <summary>
    /// Информация о диске
    /// </summary>
    public struct DiskInfo
    {
        public int Number;

        public DiskType Info;
    }

    /// <summary>
    /// Информация о фильме
    /// </summary>
    public struct FilmInfo
    {
        public int Number;

        public string Name;

        public string Info;

        public GenreType Genre;
    }

    public class Helper
    {
        /// <summary>
        /// Кновертирует строку с названием жанра в переменную типа GenreType
        /// </summary>
        /// <param name="genreStr">Красивое или точное название жанра</param>
        /// <returns></returns>
        public static GenreType StringToGenre(string genreStr)
        {
            GenreType genreType;
            switch (genreStr.ToLower())
            {
                case "про жизнь":                
                case "aboutlife":
                    genreType = GenreType.AboutLife;
                    break;
                case "сказка":
                case "сказки":
                case "tail":
                    genreType = GenreType.Tail;
                    break;
                case "детектив":
                case "детективы":
                case "detective":
                    genreType = GenreType.Detective;
                    break;
                case "комедия":
                case "комедии":
                case "comedy":
                    genreType = GenreType.Comedy;
                    break;
                case "боевик":
                case "боевики":
                case "action":
                    genreType = GenreType.Action;
                    break;
                case "мультик":
                case "мультики":
                case "cartoon":
                    genreType = GenreType.Cartoon;
                    break;
                case "ужас":
                case "ужасы":
                case "horror":
                    genreType = GenreType.Horror;
                    break;
                case "триллер":
                case "триллеры":
                case "triller":
                    genreType = GenreType.Triller;
                    break;
                case "мелодрама":
                case "мелодрамы":
                case "melodrama":
                    genreType = GenreType.Melodrama;
                    break;
                case "исторический":
                case "исторические":
                case "history":
                    genreType = GenreType.History;
                    break;
                case "фэнтэзи":
                case "fantasy":
                    genreType = GenreType.Fantasy;
                    break;
                case "фантастика":
                case "fantastic":
                    genreType = GenreType.Fantastic;
                    break;
                default:
                    genreType = GenreType.Other;
                    break;
            }

            return genreType;
        }

        /// <summary>
        /// Конвертирует перменную типа GenreType в красивое название
        /// </summary>
        /// <param name="genre">Переменная для конвертирования</param>
        /// <returns></returns>
        public static string GenreToString(GenreType genre)
        {
            string genreStr;

            switch (genre)
            {
                case GenreType.AboutLife:
                    genreStr = "Про жизнь";
                    break;
                case GenreType.Tail:
                    genreStr = "Сказка";
                    break;
                case GenreType.Detective:
                    genreStr = "Детектив";
                    break;
                case GenreType.Comedy:
                    genreStr = "Комедия";
                    break;
                case GenreType.Action:
                    genreStr = "Боевик";
                    break;
                case GenreType.Cartoon:
                    genreStr = "Мультик";
                    break;
                case GenreType.Horror:
                    genreStr = "Ужас";
                    break;
                case GenreType.Triller:
                    genreStr = "Триллер";
                    break;
                case GenreType.Melodrama:
                    genreStr = "Мелодрама";
                    break;
                case GenreType.History:
                    genreStr = "Исторический";
                    break;
                case GenreType.Fantasy:
                    genreStr = "Фэнтэзи";
                    break;
                case GenreType.Fantastic:
                    genreStr = "Фантастика";
                    break;
                default:
                    genreStr = "Другое";
                    break;
            }

            return genreStr;
        }

        /// <summary>
        /// Конвертирует строку с номерами в массив строк
        /// </summary>
        /// <param name="diskNumbersStr">Строка в виде '1, 3,10-12 , 15, 21 - 23'</param>
        /// <returns></returns>        
        public static string[] ConvertDiskNumbersStringToArray(string diskNumbersStr)
        {
            var res = new List<string>();

            string[] parts = diskNumbersStr.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string t in parts)
            {
                string path = t.Trim();
                int diskNumber;
                if (int.TryParse(path, out diskNumber))
                {
                    res.Add(diskNumber.ToString());
                    continue;
                }

                string[] numbers = path.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length != 2)
                {
                    continue;
                }

                int firstDiskNumber;
                int secondDiskNumber;
                if (int.TryParse(numbers[0].Trim(), out firstDiskNumber) &&
                    int.TryParse(numbers[1].Trim(), out secondDiskNumber))
                {
                    for (int j = firstDiskNumber; j <= secondDiskNumber; j++)
                    {
                        res.Add(j.ToString());
                    }
                }
            }

            return res.ToArray();
        }
    }
}