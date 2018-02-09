using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace AnyaArticle
{
    public struct FilterInfo
    {
        public string textBox1;
        public string textBox2;
        public string textBox3;
        public string textBox4;
        public string textBox5;
        public string textBox6;
        public string comboBox1;
    }

    class FilePath
    {
        private string PathStr; // Закрытая переменная с путём к файлу

        // Конструктор по умолчанию

        public FilePath()
        {
            PathStr = "";
        }

        // Функция, возвращающая путь к файлу в нужном формате

        public string GetPath(string str, string initDir)
        {
            string str2 = "";

            int l = str.Length;
            for (int i = l - 1; i >= 0; i--)
                if (str[i].Equals('\\'))
                {
                    l = i + 1;
                    break;
                }

            for (int i = 0; i < l - 1; i++)
                str2 += str[i];

            PathStr = "";
            if (str2.Equals(initDir))
                for (int i = l; i < str.Length; i++)
                    PathStr += str[i];
            else
                PathStr = str;

            return PathStr;
        }


        /// <summary>
        /// Перевод строки в тип size
        /// </summary>
        /// <param name="s">Строка, в которой через ";" или через "; " идут два числа</param>
        /// <returns></returns>
        public Size FromStringToSize(string s)
        {
            Size size = new Size();
            int val;
            int i = 0;

            try
            {
                val = 0;
                while (s[i] != ';')
                {
                    val = val * 10 + Convert.ToInt32(s[i].ToString());
                    i++;
                }
                size.Width = val;

                i++;
                if (s[i] == ' ')
                    i++;
                val = 0;
                while (i < s.Length)
                {
                    val = val * 10 + Convert.ToInt32(s[i].ToString());
                    i++;
                }
                size.Height = val;

                return size;
            }
            catch (Exception)
            {
                return new Size();
            }
        }

        /// <summary>
        /// Перевод строки в тип point
        /// </summary>
        /// <param name="s">Строка, в которой через ";" или через "; " идут два числа</param>
        /// <returns></returns>
        public Point FromStringToPoint(string s)
        {
            Point point = new Point();

            try
            {
                Size size = FromStringToSize(s);

                point.X = size.Width;
                point.Y = size.Height;
                return point;
            }
            catch (Exception)
            {
                return new Point();
            }
        }

        /// <summary>
        /// Разделяет строку из цифр, идущих через символ ch, на элементы
        /// </summary>
        /// <param name="s">Строка с цифрами</param>
        /// <param name="ch">Символ разделения</param>
        /// <returns></returns>
        public int[] FromStringToArray(string s, char ch)
        {
            ArrayList mas = new ArrayList();
            int n = 0,
                    i = 0;
            try
            {
                while (i < s.Length)
                {
                    if (s[i] != ch)
                        n = n * 10 + Convert.ToInt32(s[i].ToString());
                    else
                    {
                        mas.Add(n);
                        n = 0;
                    }
                    i++;
                }
                int[] rez = new int[mas.Count];
                for (i = 0; i < rez.Length; i++)
                    rez[i] = (int)mas[i];
                return rez;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
