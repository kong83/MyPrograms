using System.IO;

namespace FilesName
{
    class StringWork
    {
        /// <summary>
        /// Рабочая строка, представляющая собой имя файла или его расширение. Выделяется из полного пути к файлу.
        /// </summary>
        private readonly string _str;

        /// <summary>
        /// Возвращает рабочую строку
        /// </summary>
        /// <returns></returns>
        public string GetStr
        {
            get
            {
                return _str;
            }
        }

        /// <summary>
        /// Индекс для прохождения по рабочей строке. Изменяется только в плюс от 0 при инициализации рабочей строки.
        /// </summary>
        private int _iStr;

        /// <summary>
        /// Возвращает текущее положение _iStr - бегунка по строке
        /// </summary>
        /// <returns></returns>
        public int GetiStr
        {
            get
            {
                return _iStr;
            }
        }

        /// <summary>
        /// Строка для поиска
        /// </summary>
        private readonly string _textFind;


        /// <summary>
        /// Номер в полном пути к файлу, с которого начинается наша рабочая строка
        /// </summary>
        public readonly int Offset;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fullname">Полное имя к файлу</param>
        /// <param name="tf">Срока для поиска</param>
        /// <param name="n">1 - если ищем в имени, 2 - если в расширении файла</param>
        public StringWork(string fullname, string tf, short n)
        {            
            if (n == 1)  // Веделение имени файла
            {
                _str = Path.GetFileNameWithoutExtension(fullname);
                if(Path.HasExtension(fullname))
                    Offset = fullname.LastIndexOf(".") - _str.Length;
                else
                    Offset = fullname.Length - _str.Length;
            }
            else                  // Выделение расширения файла
            {
                _str = Path.GetExtension(fullname).Substring(1);                
                Offset = fullname.LastIndexOf(".") + 1;                
            }

            _iStr = 0;
            _textFind = tf;
        }

        
        /// <summary>
        /// Возвращает подстроку, начинающуюся с индекса n и заканчивающуюся концом строки или символом '*'
        /// </summary>
        /// <param name="n">Начальный индекс</param>
        /// <returns></returns>
        public string GetString(int n)
        {
            int i = n;
            string rez = "";
            while (i < _textFind.Length && _textFind[i] != '*')
            {
                rez += _textFind[i++];
            }
            return rez;
        }

        /// <summary>
        ///  Поиск подстроки sub в рабочей строке начиная с индекса _iStr. 
        /// В случае ненахождения возвращается -1
        /// </summary>
        /// <param name="sub">Подстрока для поиска</param>    
        /// <returns></returns>
        public int SearchNext(string sub)
        {
            string s = _str.Substring(_iStr); 
            int i = s.IndexOf(sub);
            if (i != -1)
            {
                _iStr += i;
                i = _iStr;
                _iStr += sub.Length;
                return i;
            }
            return -1;
        }
    }
}