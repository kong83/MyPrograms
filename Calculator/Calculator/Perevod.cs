using System;

namespace Calculator
{
    class Perevod
    {
        private const string E = "0.00000001";		// Для отлова шума

        private readonly string _str = string.Empty;          // Строка для хранения числа при переводе
        private readonly bool _minus;				// Индикатор отрицатльности числа        

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="str"></param>
        public Perevod(string str)
        {
            str.Trim();
            if (str[0] == '-')
            {
                _minus = true;
                _str = str.Substring(1);
            }
            else
            {
                _str = str;
            }
        }


        /// <summary>
        /// Возвращает количество чисел перед запятой -1
        /// </summary>
        /// <returns></returns>
        private int GetDigitCountBeforeComma()
        {
            int i = Math.Max(_str.IndexOf(","), _str.IndexOf("."));

            if (i != -1)
            {
                return i;
            }
            
            return _str.Length;
        }

        /// <summary>
        /// Перевод символа в число
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static double ToDigit(char ch)
        {
            switch (ch)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Перевод числа в символ
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string ToStr(int n)
        {
            switch (n)
            {
                case 0:
                    return "0";
                case 1:
                    return "1";
                case 2:
                    return "2";
                case 3:
                    return "3";
                case 4:
                    return "4";
                case 5:
                    return "5";
                case 6:
                    return "6";
                case 7:
                    return "7";
                case 8:
                    return "8";
                case 9:
                    return "9";
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Получение целой части числа из str
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string GetDiv(string str)
        {
            try
            {
                string rez = BigNumber.Div(str);
                foreach (char ch in rez)
                {
                    if(ch < '0' || ch > '9')
                    {
                        throw new Exception();
                    }
                }                

                return rez;
            }
            catch
            {
                return "-1";
            }
        }

        /// <summary>
        /// Получение дробной части числа из str  
        /// </summary>
        /// <returns></returns>
        private static string GetMod(string str)
        {
            try
            {
                string rez = BigNumber.Mod(str);                
                if (!ExpressionVerifier.IsDecNumber(rez))
                {
                    throw new Exception();
                }

                return rez;
            }
            catch
            {
                return "-1";
            }
        }
        
        /// <summary>
        /// Переводит число из системы счисления currentNotation в десятичную систему счисления
        /// </summary>
        /// <param name="currentNotation">Текущая система счисления числа</param>
        /// <returns></returns>
        public string ConvertNumberFromCurrentNotationToDec(int currentNotation)
        {
            double d;
            if (currentNotation != 10)
            {
                int st = GetDigitCountBeforeComma() - 1;
                double rez = 0;

                for (int i = 0; i < _str.Length; i++)
                {
                    if (_str[i] == ',' || _str[i] == '.')
                    {
                        continue;
                    }

                    d = ToDigit(_str[i]);
                    if (d != -1 && d < currentNotation)
                    {
                        rez += d * Math.Pow(currentNotation, st--);
                    }
                    else
                    {
                        if (d == -1)
                        {
                            return "Error. Unknown symbol.";
                        }
                        
                        if (d >= currentNotation)
                        {
                            return "Error: Scale of notation is '" + currentNotation + "'. One of your digits is '" + _str[i] + "'";
                        }
                    }
                }

                if (_minus)
                {
                    return Convert.ToString(-rez);
                }
                
                return Convert.ToString(rez);
            }
            
            for (int i = 0; i < _str.Length; i++)
            {
                if (_str[i] == ',' || _str[i] == '.')
                {
                    continue;
                }

                d = ToDigit(_str[i]);
                if (d == -1)
                {
                    return "Error. Unknown symbol.";
                }
                
                if (d >= currentNotation)
                {

                    return "Error: Scale of notation is '" + currentNotation + "'. One of your digits is '" + _str[i] + "'";
                }
            }
            if (_minus)
            {
                return "-" + _str;
            }
            
            return _str;
        }

        /// <summary>
        /// Переводит число из десятичной системы в систему счисления targetNotation
        /// </summary>
        /// <param name="targetNotation">Систему счисления, в которую надо перевести число</param>
        /// <returns></returns>
        public string ConvertNumberFromDecToTargetNotation(int targetNotation)
        {
            if (targetNotation != 10)
            {
                var div = new BigNumber(GetDiv(_str));  // Получение целой части числа                       
                var mod = new BigNumber(GetMod(_str));  // Получение числа без целой части                
                string s1 = string.Empty,               // Число перед запятой в новой системе счисления
                       s2 = string.Empty;               // Число после запятой в новой системе счисления
                int kol = 0;							// Количество элементов в массиве mas
                var mas = new string[25];               // Массив, для отлова цикла при получении дробной части               

                if (div.Value == "-1" || mod.Value == "-1")
                {
                    return "Error: Unknown symbol.";
                }

                while (div > "0")
                {
                    int d = Convert.ToInt32((div % targetNotation).Value);
                    div.Value = BigNumber.QuotientDiv(div.Value, targetNotation.ToString());
                    s1 = ToStr(d) + s1;
                }

                if (_minus)
                {
                    s1 = "-" + s1;
                }

                do
                {
                    mod = mod * targetNotation;
                    int gfd = Convert.ToInt32(GetDiv(mod.Value));

                    if (gfd < targetNotation)
                    {
                        s2 += ToStr(gfd);
                    }
                    else
                    {
                        s2 += "0";
                    }

                    mod = mod % "1";
                    mas[kol++] = mod.Value;

                    for (int i = 0; i < kol - 1; i++)
                    {
                        if (mas[kol - 1] == mas[i])
                        {
                            mod.Value = "0";
                        }
                    }
                }
                while (mod > E && kol < 25);

                if (s1.Equals(string.Empty))
                {
                    s1 = "0";
                }
                else if (s1.Equals("-"))
                {
                    s1 = "-0";
                }

                return s1 + "," + s2;
            }
            
            if (_minus)
            {
                return "-" + _str;
            }

            return _str;
        }
    }
}