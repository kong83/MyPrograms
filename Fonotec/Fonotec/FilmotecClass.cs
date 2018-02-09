using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using zlib;

namespace Fonotec
{
    /// <summary>
    /// Класс для работы с фильмотекой
    /// </summary>
    public class FilmotecClass
    {
        /// <summary>
        /// Путь до базы данных
        /// </summary>
        private readonly string mPathDb;

        /// <summary>
        /// Текущий тип сортировки дисков
        /// </summary>
        private SortType mCurrentSortDiskType;

        /// <summary>
        /// Текущий тип сортировки фильмов
        /// </summary>
        private SortType mCurrentSortFilmType;

        /// <summary>
        /// Gets or sets текущий тип сортировки дисков
        /// </summary>
        public SortType CurrentSortDiskType
        {
            get
            {
                return this.mCurrentSortDiskType;
            }

            set
            {
                this.mCurrentSortDiskType = value;
            }
        }

        /// <summary>
        /// Gets or sets текущий тип сортировки фильмов
        /// </summary>
        public SortType CurrentSortFilmType
        {
            get
            {
                return this.mCurrentSortFilmType;
            }

            set
            {
                this.mCurrentSortFilmType = value;
            }
        }

        #region     /************************ Работа с дисками *****************/

        /// <summary>
        /// Информация о дисках
        /// </summary>
        private List<DiskInfo> mDiskInfo;

        /// <summary>
        /// Gets информации о дисках
        /// </summary>
        public DiskInfo[] DiskInfo
        {
            get
            {
                SortDisks();
                var returnArray = new DiskInfo[this.mDiskInfo.Count + 1];
                returnArray[0].Number = 0;
                returnArray[0].Info = DiskType.Empty;
                for (int i = 0; i < this.mDiskInfo.Count; i++)
                {
                    returnArray[i + 1].Number = this.mDiskInfo[i].Number;
                    returnArray[i + 1].Info = this.mDiskInfo[i].Info;
                }

                return returnArray;
            }
        }

        /// <summary>
        /// Сортировка списка дисков
        /// </summary>       
        private void SortDisks()
        {
            var newDiskInfo = new List<DiskInfo>();

            while (this.mDiskInfo.Count > 0)
            {
                int selIndex = 0;
                for (int i = 1; i < this.mDiskInfo.Count; i++)
                {
                    if (this.mCurrentSortDiskType == SortType.Increase &&
                        this.mDiskInfo[i].Number < this.mDiskInfo[selIndex].Number)
                    {
                        selIndex = i;
                    }
                    else if (this.mCurrentSortDiskType == SortType.Decrease &&
                             this.mDiskInfo[i].Number > this.mDiskInfo[selIndex].Number)
                    {
                        selIndex = i;
                    }
                }

                newDiskInfo.Add(this.mDiskInfo[selIndex]);
                this.mDiskInfo.RemoveAt(selIndex);
            }

            this.mDiskInfo = new List<DiskInfo>(newDiskInfo);
        }

        /// <summary>
        /// Найти фильм по номеру диска и названию
        /// </summary>
        private int FindDisk(DiskInfo findDiskInfo)
        {
            return FindDisk(findDiskInfo.Number);
        }

        /// <summary>
        /// Найти фильм по номеру диска и названию
        /// </summary>
        private int FindDisk(int findDiskNumber)
        {
            int findIndex = this.mDiskInfo.TakeWhile(diskInfo => diskInfo.Number != findDiskNumber).Count();

            if (findIndex == this.mDiskInfo.Count)
            {
                throw new MyException(string.Format("Диск с номером {0} не найден", findDiskNumber));
            }

            return findIndex;
        }

        /// <summary>
        /// Добавление нового диска
        /// </summary>
        /// <param name="newDiskInfo"></param>
        public void AddDisk(DiskInfo newDiskInfo)
        {
            int testIndex;
            try
            {
                testIndex = FindDisk(newDiskInfo);
            }
            catch (MyException)
            {
                testIndex = -1;
            }

            if (testIndex != -1)
            {
                throw new MyException(string.Format("Диск номером {0} уже существует", newDiskInfo.Number));
            }

            this.mDiskInfo.Add(newDiskInfo);
            SortDisks();
            Save();
        }

        /// <summary>
        /// Редактирование диска
        /// </summary>
        /// <param name="oldDiskNumber"></param>
        /// <param name="newDiskInfo"></param>
        public void EditDisk(DiskInfo oldDiskNumber, DiskInfo newDiskInfo)
        {
            if (oldDiskNumber.Number != newDiskInfo.Number)
            {
                int testIndex;
                try
                {
                    testIndex = FindDisk(newDiskInfo);
                }
                catch (MyException)
                {
                    testIndex = -1;
                }

                if (testIndex != -1)
                {
                    throw new MyException(string.Format("Диск номером {0} уже существует", newDiskInfo.Number));
                }
            }

            this.mDiskInfo.RemoveAt(FindDisk(oldDiskNumber));
            this.mDiskInfo.Add(newDiskInfo);
            SortDisks();
            Save();
        }

        /// <summary>
        /// Удаление диска
        /// </summary>
        /// <param name="deleteDiskInfo"></param>
        public void DeleteDisk(DiskInfo deleteDiskInfo)
        {
            int deleteIndex = FindDisk(deleteDiskInfo);

            this.mDiskInfo.RemoveAt(deleteIndex);

            deleteIndex = 0;
            while (deleteIndex < this.mFilmInfo.Count)
            {
                if (this.mFilmInfo[deleteIndex].Number == deleteDiskInfo.Number)
                {
                    this.mFilmInfo.RemoveAt(deleteIndex);
                }
                else
                {
                    deleteIndex++;
                }
            }

            SortDisks();
            Save();
        }

        #endregion

        #region     /************************ Работа с фильмами *****************/

        /// <summary>
        /// Информация о фильмах
        /// </summary>
        private List<FilmInfo> mFilmInfo;

        /// <summary>
        /// Информация о фильмах
        /// </summary>
        public FilmInfo[] GetFilmInfo(int diskNumber, string filterName, string genreStr)
        {
            SortFilms();

            GenreType genreType = Helper.StringToGenre(genreStr);

            return this.mFilmInfo.Where(filmInfo =>
                (filmInfo.Number == diskNumber || diskNumber == 0) && 
                filmInfo.Name.ToLower().Contains(filterName.ToLower()) && 
                (genreStr == "Все жанры" || genreStr == string.Empty || genreType == filmInfo.Genre)).ToArray();
        }

        /// <summary>
        /// Сортировка списка фильмов
        /// </summary>        
        private void SortFilms()
        {
            var newFilmInfo = new List<FilmInfo>();

            while (this.mFilmInfo.Count > 0)
            {
                int selIndex = 0;
                for (int i = 1; i < this.mFilmInfo.Count; i++)
                {
                    if (this.mCurrentSortFilmType == SortType.Increase &&
                        this.mFilmInfo[i].Name.CompareTo(this.mFilmInfo[selIndex].Name) < 0)
                    {
                        selIndex = i;
                    }
                    else if (this.mCurrentSortFilmType == SortType.Decrease &&
                             this.mFilmInfo[i].Name.CompareTo(this.mFilmInfo[selIndex].Name) > 0)
                    {
                        selIndex = i;
                    }
                }

                newFilmInfo.Add(this.mFilmInfo[selIndex]);
                this.mFilmInfo.RemoveAt(selIndex);
            }

            this.mFilmInfo = new List<FilmInfo>(newFilmInfo);
        }

        /// <summary>
        /// Найти фильм по номеру диска и названию
        /// </summary>
        private int FindFilm(FilmInfo findFilmInfo)
        {
            int findIndex = this.mFilmInfo.TakeWhile(filmInfo => filmInfo.Number != findFilmInfo.Number || filmInfo.Name != findFilmInfo.Name).Count();

            if (findIndex == this.mFilmInfo.Count)
            {
                throw new MyException(
                    string.Format("Фильм {0} на диске номер {1} не найден", findFilmInfo.Name, findFilmInfo.Number));
            }

            return findIndex;
        }

        /// <summary>
        /// Добавление нового фильма
        /// </summary>
        /// <param name="newFilmInfo"></param>
        public void AddFilm(FilmInfo newFilmInfo)
        {
            FindDisk(newFilmInfo.Number);

            this.mFilmInfo.Add(newFilmInfo);
            SortFilms();
            Save();
        }

        /// <summary>
        /// Редактирование фильма
        /// </summary>
        /// <param name="oldFilmInfo"></param>
        /// <param name="newFilmInfo"></param>
        public void EditFilm(FilmInfo oldFilmInfo, FilmInfo newFilmInfo)
        {
            FindDisk(newFilmInfo.Number);

            this.mFilmInfo.RemoveAt(FindFilm(oldFilmInfo));
            this.mFilmInfo.Add(newFilmInfo);
            SortFilms();
            Save();
        }

        /// <summary>
        /// Удаление фильма
        /// </summary>
        /// <param name="deleteFilmInfo"></param>
        public void DeleteFilm(FilmInfo deleteFilmInfo)
        {
            this.mFilmInfo.RemoveAt(FindFilm(deleteFilmInfo));
            SortFilms();
            Save();
        }

        #endregion

        #region     /************************ Внутренние функции *****************/

        /// <summary>
        /// Конструктов для FilmotecClass
        /// </summary>
        public FilmotecClass(string pathDataBase)
        {
            this.mPathDb = pathDataBase;

            Load();
        }

        /// <summary>
        ///  Сохранить базы данных в файл
        /// </summary>
        private void Save()
        {
            var ms = new MemoryStream();
            byte[] tempArray;
            foreach (DiskInfo diskInfo in this.mDiskInfo)
            {
                tempArray =
                    Encoding.GetEncoding("windows-1251").GetBytes(diskInfo.Number + "^!&^" + diskInfo.Info + "^!&^");
                ms.Write(tempArray, 0, tempArray.Length);
            }

            tempArray = Encoding.GetEncoding("windows-1251").GetBytes("^^!!&&^^");
            ms.Write(tempArray, 0, tempArray.Length);

            foreach (FilmInfo filmInfo in this.mFilmInfo)
            {
                tempArray =
                    Encoding.GetEncoding("windows-1251").GetBytes(
                        filmInfo.Number + "^!&^" + filmInfo.Name + "^!&^" + filmInfo.Info + "^!&^" + filmInfo.Genre + "^!&^");
                ms.Write(tempArray, 0, tempArray.Length);
            }

            var bytesBuffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(bytesBuffer, 0, bytesBuffer.Length);
            ms.Close();
            ms.Dispose();

            byte[] rez = PackXML(bytesBuffer);

            var fi = new FileInfo(this.mPathDb);
            if (!fi.Exists)
            {
                if (string.IsNullOrEmpty(fi.DirectoryName))
                {
                    throw new MyException("Неверный путь к базе фонотеки");
                }

                var di = new DirectoryInfo(fi.DirectoryName);
                if (!di.Exists)
                {
                    di.Create();
                }

                StreamWriter sW = fi.CreateText();
                sW.Close();
            }
            else
            {
                fi.Delete();
            }

            FileStream fs = File.OpenWrite(this.mPathDb);
            fs.Write(rez, 0, rez.Length);
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// Загрузить базы данных из файла
        /// </summary>
        /// <returns>Success - true, oherwise - false</returns>
        private void Load()
        {
            this.mDiskInfo = new List<DiskInfo>();
            this.mFilmInfo = new List<FilmInfo>();

            var fi = new FileInfo(this.mPathDb);
            if (!fi.Exists)
            {
                return;
            }

            FileStream fs = File.OpenRead(this.mPathDb);
            var bytesBuffer = new byte[fs.Length];
            fs.Read(bytesBuffer, 0, (int)fs.Length);
            fs.Close();
            fs.Dispose();

            byte[] rez = UnpackXML(bytesBuffer);

            if (rez.Length > 0)
            {
                string text = Encoding.GetEncoding("windows-1251").GetString(rez);

                string[] parseText = text.Split(new[] { "^^!!&&^^" }, StringSplitOptions.None);

                var diskStr = new string[0];
                if (parseText.Length > 0 && parseText[0].Length > 0)
                {
                    diskStr = parseText[0].Split(new[] { "^!&^" }, StringSplitOptions.None);
                }

                var filmStr = new string[0];
                if (parseText.Length > 1 && parseText[1].Length > 0)
                {
                    filmStr = parseText[1].Split(new[] { "^!&^" }, StringSplitOptions.None);
                }

                for (int i = 0; i < diskStr.Length; i += 2)
                {
                    if (i + 1 < diskStr.Length)
                    {
                        var diskInfo = new DiskInfo { Number = Convert.ToInt32(diskStr[i]) };
                        if (diskStr[i + 1] == "Buy")
                        {
                            diskInfo.Info = DiskType.Buy;
                        }
                        else if (diskStr[i + 1] == "OwnR")
                        {
                            diskInfo.Info = DiskType.OwnR;
                        }
                        else if (diskStr[i + 1] == "OwnRW")
                        {
                            diskInfo.Info = DiskType.OwnRW;
                        }
                        else
                        {
                            diskInfo.Info = DiskType.Empty;
                        }

                        this.mDiskInfo.Add(diskInfo);
                    }
                }
                
                for (int i = 0; i < filmStr.Length; i += 4)
                {
                    if (i + 3 < filmStr.Length)
                    {
                        GenreType genre = Helper.StringToGenre(filmStr[i + 3]);                        

                        var filmInfo = new FilmInfo
                            { 
                                Number = Convert.ToInt32(filmStr[i]), 
                                Name = filmStr[i + 1], 
                                Info = filmStr[i + 2],
                                Genre = genre
                            };
                        this.mFilmInfo.Add(filmInfo);
                    }
                }
            }
            else
            {
                throw new MyException("Файл с сохранённой информацией повреждён");
            }
        }

        /// <summary>
        /// Упаковать массив байт
        /// </summary>
        /// <param name="xmlInfo">Array for packing</param>
        /// <returns></returns>
        private static byte[] PackXML(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }

            var rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }

            return rez;
        }

        /// <summary>
        /// Распаковать массив байт
        /// </summary>
        /// <param name="xmlInfo">Array for unpacking</param>
        /// <returns></returns>
        private static byte[] UnpackXML(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream);

            try
            {
                zStream.Write(xmlInfo, 0, xmlInfo.Length);
            }
            catch
            {
                MessageBox.Show("Ошибка в файле с базой фонотеки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }

            var rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }

            return rez;
        }

        #endregion
    }
}