using System;
using System.Diagnostics;

namespace Calculator
{
    [DebuggerDisplay("{Value}")]
    public class BigNumber
    {
        public string Value { get; set; }

        private const string Eps = "0,000001";
        private const string E = "2,718281828";
        
        public BigNumber(object value)
        {            
            Value = value.ToString();
        }        

        #region +
        public static BigNumber operator +(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return new BigNumber(Sum(bigNumber1.Value, bigNumber2.Value));
        }

        public static BigNumber operator +(BigNumber bigNumber1, object number2)
        {
            return new BigNumber(Sum(bigNumber1.Value, number2.ToString()));
        }

        public static BigNumber operator +(object number1, BigNumber bigNumber2)
        {
            return new BigNumber(Sum(number1.ToString(), bigNumber2.Value));
        }
        #endregion

        #region -
        public static BigNumber operator -(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return new BigNumber(Diff(bigNumber1.Value, bigNumber2.Value));
        }

        public static BigNumber operator -(BigNumber bigNumber1, object number2)
        {
            return new BigNumber(Diff(bigNumber1.Value, number2.ToString()));
        }

        public static BigNumber operator -(object number1, BigNumber bigNumber2)
        {
            return new BigNumber(Diff(number1.ToString(), bigNumber2.Value));
        }
        #endregion

        #region *
        public static BigNumber operator *(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return new BigNumber(Product(bigNumber1.Value, bigNumber2.Value));
        }

        public static BigNumber operator *(BigNumber bigNumber1, object number2)
        {
            return new BigNumber(Product(bigNumber1.Value, number2.ToString()));
        }

        public static BigNumber operator *(object number1, BigNumber bigNumber2)
        {
            return new BigNumber(Product(number1.ToString(), bigNumber2.Value));
        }
        #endregion

        #region /
        public static BigNumber operator /(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return new BigNumber(Quotient(bigNumber1.Value, bigNumber2.Value));
        }

        public static BigNumber operator /(BigNumber bigNumber1, object number2)
        {
            return new BigNumber(Quotient(bigNumber1.Value, number2.ToString()));
        }

        public static BigNumber operator /(object number1, BigNumber bigNumber2)
        {
            return new BigNumber(Quotient(number1.ToString(), bigNumber2.Value));
        }
        #endregion

        #region %
        public static BigNumber operator %(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return new BigNumber(QuotientMod(bigNumber1.Value, bigNumber2.Value));
        }

        public static BigNumber operator %(BigNumber bigNumber1, object number2)
        {
            return new BigNumber(QuotientMod(bigNumber1.Value, number2.ToString()));
        }

        public static BigNumber operator %(object number1, BigNumber bigNumber2)
        {
            return new BigNumber(QuotientMod(number1.ToString(), bigNumber2.Value));
        }
        #endregion

        #region ^
        public static BigNumber operator ^(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return new BigNumber(Pow(bigNumber1.Value, bigNumber2.Value));
        }

        public static BigNumber operator ^(BigNumber bigNumber1, object number2)
        {
            return new BigNumber(Pow(bigNumber1.Value, number2.ToString()));
        }        
        #endregion

        #region < >
        public static bool operator >(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return Compare(bigNumber1.Value, bigNumber2.Value) == 1;
        }

        public static bool operator <(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return Compare(bigNumber1.Value, bigNumber2.Value) == -1;
        }

        public static bool operator >(BigNumber bigNumber1, object number2)
        {
            return Compare(bigNumber1.Value, number2.ToString()) == 1;
        }

        public static bool operator <(BigNumber bigNumber1, object number2)
        {
            return Compare(bigNumber1.Value, number2.ToString()) == -1;
        }

        public static bool operator >(object number1, BigNumber bigNumber2)
        {
            return Compare(number1.ToString(), bigNumber2.Value) == 1;
        }

        public static bool operator <(object number1, BigNumber bigNumber2)
        {
            return Compare(number1.ToString(), bigNumber2.Value) == -1;
        }
        #endregion

        #region >= <=
        public static bool operator >=(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return Compare(bigNumber1.Value, bigNumber2.Value) >= 0;
        }

        public static bool operator <=(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return Compare(bigNumber1.Value, bigNumber2.Value) <= 0;
        }

        public static bool operator >=(BigNumber bigNumber1, object number2)
        {
            return Compare(bigNumber1.Value, number2.ToString()) >= 0;
        }

        public static bool operator <=(BigNumber bigNumber1, object number2)
        {
            return Compare(bigNumber1.Value, number2.ToString()) <= 0;
        }

        public static bool operator >=(object number1, BigNumber bigNumber2)
        {
            return Compare(number1.ToString(), bigNumber2.Value) >= 0;
        }

        public static bool operator <=(object number1, BigNumber bigNumber2)
        {
            return Compare(number1.ToString(), bigNumber2.Value) <= 0;
        }
        #endregion

        #region == !=
        public static bool operator ==(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return Compare(bigNumber1.Value, bigNumber2.Value) == 0;
        }

        public static bool operator !=(BigNumber bigNumber1, BigNumber bigNumber2)
        {
            return Compare(bigNumber1.Value, bigNumber2.Value) != 0;
        }

        public static bool operator ==(BigNumber bigNumber1, object number2)
        {
            return Compare(bigNumber1.Value, number2.ToString()) == 0;
        }

        public static bool operator !=(BigNumber bigNumber1, object number2)
        {
            return Compare(bigNumber1.Value, number2.ToString()) != 0;
        }

        public static bool operator ==(object number1, BigNumber bigNumber2)
        {
            return Compare(number1.ToString(), bigNumber2.Value) == 0;
        }

        public static bool operator !=(object number1, BigNumber bigNumber2)
        {
            return Compare(number1.ToString(), bigNumber2.Value) != 0;
        }
        #endregion

        #region Equals/GetHashCode
        public bool Equals(BigNumber other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Value, Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(BigNumber)) return false;
            return Equals((BigNumber)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
        #endregion

        /// <summary>
        /// Получение модуля числа
        /// </summary>
        /// <param name="s">Число, модуль которого надо получить</param>
        /// <returns></returns>
        private static string Abs(string s)
        {
            if (s[0] == '-')
            {
                return s.Substring(1);
            }

            return s;
        }

        /// <summary>
        /// Получение целой части числа
        /// </summary>
        /// <param name="s">Число, целую часть которого надо получить</param>
        /// <returns></returns>
        public static string Div(string s)
        {
            int max = Math.Max(s.IndexOf('.'), s.IndexOf(','));
            if (max < 0)
            {
                return s;
            }

            return s.Substring(0, max);
        }

        /// <summary>
        /// Получение дробной части числа
        /// </summary>
        /// <param name="s">Число, дробную часть которого надо получить</param>
        /// <returns></returns>
        public static string Mod(string s)
        {
            int max = Math.Max(s.IndexOf('.'), s.IndexOf(','));
            if (max < 0 && max == s.Length - 1)
            {
                return "0";
            }

            return "0," + s.Substring(max + 1);
        }

        /// <summary>
        /// Вычисление факториала
        /// </summary>
        /// <param name="s">Целое число, факториал которого надо вычислить</param>
        /// <returns></returns>
        private static string Fact(string s)
        {
            s = Div(s);
            if (s == "1")
            {
                return "1";
            }

            string rez = "1";
            for (string i = "2"; Compare(i, s) <= 0; i = Sum(i, "1"))
            {
                rez = Product(rez, i);
            }

            return DelFirstNull(rez);
        }

        /// <summary>
        /// Количество цифр после запятой
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns> 
        private static int QuantityAfterComma(string s)
        {
            int max = Math.Max(s.IndexOf('.'), s.IndexOf(','));
            if (max < 0)
            {
                return 0;
            }

            return s.Length - max - 1;
            /*int i = 0;
            while (i < s.Length && s[i] != ',' && s[i] != '.' )			
              i++;
            if (i < s.Length)
              return s.Length - i - 1;
            else
              return 0;*/
        }

        /// <summary>
        /// Количество цифр перед запятой
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns> 
        private static int QuantityBeforeComma(string s)
        {
            int i = 0;
            while (i < s.Length && s[i] != ',' && s[i] != '.')
            {
                i++;
            }

            return i;
        }

        /// <summary>
        /// Удаление первых нулей из числа
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string DelFirstNull(string s)
        {
            if (s.Length < 2)
            {
                return s;
            }

            // Запоминание "-" перед числом
            string minus = string.Empty;
            if (s[0] == '-')
            {
                minus = "-";
                s = s.Substring(1);
            }

            while (s.Length > 1 && s[0] == '0' && s[1] != ',' && s[1] != '.')
            {
                s = s.Substring(1);
            }

            s = ShiftLeft(s, 0);
            return minus + s;
        }

        // Сдвиг запятой в числе s на shift позиций влево
        private static string ShiftLeft(string s, int shift)
        {
            int ls = QuantityBeforeComma(s);
            if (ls != s.Length)
            {
                s = s.Remove(ls, 1);
            }

            if (ls - shift <= 0)
            {
                int i = 0;
                while (i >= ls - shift)
                {
                    s = "0" + s;
                    i--;
                }

                ls = shift + 1;
            }

            s = s.Substring(0, ls - shift) + "," + s.Substring(ls - shift);
            while (s.Length > 1 && (s[s.Length - 1] == '0'))
            {
                s = s.Remove(s.Length - 1);
            }

            if (s[s.Length - 1] == ',' || s[s.Length - 1] == '.')
            {
                s = s.Remove(s.Length - 1);
            }

            return s;
        }

        /// <summary>
        /// Сравнение двух чисел неограниченной длинны 
        /// (если первое больше - то функция вернёт 1, если числа равны - то 0, если первое меньше - то -1)
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns></returns>
        private static short Compare(string a, string b)
        {
            // Убираем первые нули из чисел
            a = DelFirstNull(a);
            b = DelFirstNull(b);

            int la = QuantityBeforeComma(a),
                lb = QuantityBeforeComma(b);

            if (la > lb)
            {
                return 1;
            }

            if (lb > la)
            {
                return -1;
            }

            for (int i = 0; i < la; i++)
            {
                if (a[i] > b[i])
                {
                    return 1;
                }

                if (b[i] > a[i])
                {
                    return -1;
                }
            }

            int k = la + 1;

            la = QuantityAfterComma(a);
            lb = QuantityAfterComma(b);

            if (la == 0)
            {
                a = a + ",0";
                la = 1;
            }

            if (lb == 0)
            {
                b = b + ",0";
                lb = 1;
            }

            int min = Math.Min(la, lb);

            for (int i = k; i < k + min; i++)
            {
                if (a[i] > b[i])
                {
                    return 1;
                }

                if (b[i] > a[i])
                {
                    return -1;
                }
            }

            if (la > lb)
            {
                return 1;
            }

            if (lb > la)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Сравнение двух чисел неограниченной длинны учитывая первые нули в числах
        /// (т.е. число 01 больше чем 9, но меньше, чем 02)
        /// (если первое больше - то функция вернёт 1, если числа равны - то 0, если первое меньше - то -1)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static short CompareWithLength(string a, string b)
        {
            int la = QuantityBeforeComma(a),
                lb = QuantityBeforeComma(b);

            if (la > lb)
            {
                return 1;
            }

            if (lb > la)
            {
                return -1;
            }

            for (int i = 0; i < la; i++)
            {
                if (a[i] > b[i])
                {
                    return 1;
                }

                if (b[i] > a[i])
                {
                    return -1;
                }
            }

            int k = la + 1;
            la = QuantityAfterComma(a);
            lb = QuantityAfterComma(b);

            if (la == 0)
            {
                a = a + ",0";
                la = 1;
            }

            if (lb == 0)
            {
                b = b + ",0";
                lb = 1;
            }

            int max = Math.Max(la, lb);

            for (int i = k; i < k + max; i++)
            {
                if (a[i] > b[i])
                {
                    return 1;
                }

                if (b[i] > a[i])
                {

                    return -1;
                }
            }

            if (la > lb)
            {
                return 1;
            }

            if (lb > la)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Сумма двух чисел неограниченной длины
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns></returns>
        private static string Sum(string number1, string number2)
        {
            number1 = DelFirstNull(number1);
            number2 = DelFirstNull(number2);

            // Узнаем, отрицательно ли второе число
            if (number2[0] == '-')
            {
                number2 = number2.Substring(1);
                return Diff(number1, number2);
            }

            // Узнаем, отрицательно ли первое число
            if (number1[0] == '-')
            {
                number1 = number1.Substring(1);
                return Diff(number2, number1);
            }

            int la = QuantityAfterComma(number1),
                lb = QuantityAfterComma(number2);

            if (la == 0)
            {
                number1 = number1 + ",0";
                la = 1;
            }

            if (lb == 0)
            {
                number2 = number2 + ",0";
                lb = 1;
            }

            // Добавление 0 в конец строки, чтобы длины чисел после запятой были равны
            if (la > lb)
            {
                for (int i = 0; i < la - lb; i++)
                {
                    number2 += "0";
                }
            }
            else if (la < lb)
            {
                for (int i = 0; i < lb - la; i++)
                {
                    number1 += "0";
                }
            }

            // Добавление 0 в начало строки, чтобы длины чисел до запятой были равны
            la = QuantityBeforeComma(number1);
            lb = QuantityBeforeComma(number2);
            if (la > lb)
            {
                for (int i = 0; i < la - lb; i++)
                {
                    number2 = "0" + number2;
                }
            }
            else if (la < lb)
            {
                for (int i = 0; i < lb - la; i++)
                {
                    number1 = "0" + number1;
                }
            }

            short save = 0;
            string result = string.Empty;

            // Складываем столбиком
            for (int i = number1.Length - 1; i >= 0; i--)
            {
                if (number1[i] != ',' && number1[i] != '.')
                {
                    int val = Convert.ToInt32(number1[i].ToString()) + Convert.ToInt32(number2[i].ToString()) + save;
                    short newSave;
                    if (val >= 10)
                    {
                        newSave = 1;
                        val = val - 10;
                    }
                    else
                    {
                        newSave = 0;
                    }

                    result = val + result;
                    save = newSave;
                }
                else
                {
                    result = ',' + result;
                }
            }

            if (save > 0)
            {
                result = Convert.ToString(save) + result;
            }

            return DelFirstNull(result);
        }

        /// <summary>
        /// Разность двух чисел неограниченной длины
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns></returns>
        private static string Diff(string number1, string number2)
        {
            //return Convert.ToString(Convert.ToDouble(number1) - Convert.ToDouble(number2));
            number1 = DelFirstNull(number1);
            number2 = DelFirstNull(number2);

            if (number2[0] == '-')
            {
                number2 = number2.Substring(1);
                return Sum(number1, number2);
            }

            if (number1[0] == '-')
            {
                number1 = number1.Substring(1);
                return "-" + Sum(number1, number2);
            }

            int la = QuantityAfterComma(number1),
                lb = QuantityAfterComma(number2);

            if (la == 0)
            {
                number1 = number1 + ",0";
                la = 1;
            }

            if (lb == 0)
            {
                number2 = number2 + ",0";
                lb = 1;
            }

            if (Compare(number2, number1) == 1)
            {
                return "-" + Diff(number2, number1);
            }

            // Добавление 0 в конец строки, чтобы длины чисел после запятой были равны
            if (la > lb)
            {
                for (int i = 0; i < la - lb; i++)
                {
                    number2 += "0";
                }
            }
            else if (la < lb)
            {
                for (int i = 0; i < lb - la; i++)
                {
                    number1 += "0";
                }
            }

            // Добавление 0 в начало строки, чтобы длины чисел до запятой были равны
            la = QuantityBeforeComma(number1);
            lb = QuantityBeforeComma(number2);

            if (la > lb)
            {
                for (int i = 0; i < la - lb; i++)
                {
                    number2 = "0" + number2;
                }
            }
            else if (la < lb)
            {
                for (int i = 0; i < lb - la; i++)
                {
                    number1 = "0" + number1;
                }
            }

            string result = string.Empty;

            // Вычитаем столбиком
            for (int i = number1.Length - 1; i >= 0; i--)
            {
                if (number1[i] != ',' && number1[i] != '.')
                {

                    int val = Convert.ToInt32(number1[i].ToString()) - Convert.ToInt32(number2[i].ToString());
                    if (val < 0)
                    {
                        val += 10;
                        bool flag;
                        int j = i;
                        do
                        {
                            if (number1[j - 1] == ',' || number1[j - 1] == '.')
                            {
                                j--;
                            }

                            int save = Convert.ToInt32(number1[j - 1].ToString()) - 1;
                            if (save < 0)
                            {
                                flag = true;
                                save = 9;
                            }
                            else
                            {
                                flag = false;
                            }

                            number1 = number1.Substring(0, j - 1) + save + number1.Substring(j);
                            j--;
                        }
                        while (flag);
                    }

                    result = val + result;
                }
                else
                {
                    result = ',' + result;
                }
            }

            // Убираем первые нули			
            return DelFirstNull(result);
        }

        /// <summary>
        /// Произведение двух чисел неограниченной длины
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns></returns>
        private static string Product(string number1, string number2)
        {
            //return Convert.ToString(Convert.ToDouble(number1) * Convert.ToDouble(number2));
            number1 = DelFirstNull(number1);
            number2 = DelFirstNull(number2);

            // Если оба числа отрицательны
            if (number1[0] == '-' && number2[0] == '-')
            {
                number1 = number1.Substring(1);
                number2 = number2.Substring(1);
                return Product(number1, number2);
            }

            // Если первое из чисел отрицательно
            if (number1[0] == '-')
            {
                number1 = number1.Substring(1);
                return "-" + Product(number1, number2);
            }

            // Если второе из чисел отрицательно
            if (number2[0] == '-')
            {
                number2 = number2.Substring(1);
                return "-" + Product(number1, number2);
            }

            int la = QuantityAfterComma(number1),
                lb = QuantityAfterComma(number2);

            if (la > 50)
            {
                la = 50;
                number1 = number1.Substring(0, QuantityBeforeComma(number1) + 51);
            }

            if (lb > 50)
            {
                lb = 50;
                number2 = number2.Substring(0, QuantityBeforeComma(number2) + 51);
            }

            // Удаление запятых из чисел
            if (la > 0)
            {
                number1 = number1.Remove(number1.Length - la - 1, 1);
            }

            if (lb > 0)
            {
                number2 = number2.Remove(number2.Length - lb - 1, 1);
            }

            int after = la + lb; // Количество чисел после запятой после перемножения

            var mas = new string[number2.Length];
            int save = 0;

            // Перемножение чисел
            for (int i = 0; i < number2.Length; i++)
            {
                for (int j = 0; j < number2.Length - i - 1; j++)
                {
                    mas[i] += "0";
                }

                for (int j = number1.Length - 1; j >= 0; j--)
                {
                    int val = Convert.ToInt32(number1[j].ToString()) * Convert.ToInt32(number2[i].ToString()) + save;

                    if (val > 9)
                    {
                        save = val / 10;
                        val %= 10;
                    }
                    else
                    {
                        save = 0;
                    }

                    mas[i] = val + mas[i];
                }

                if (save != 0)
                {
                    mas[i] = save + mas[i];
                    save = 0;
                }
            }

            // Складывание чисел из массива
            string result = mas[0];

            for (int i = 1; i < number2.Length; i++)
            {
                result = Sum(result, mas[i]);
            }

            // Выставление дробной части
            if (after > 0)
            {
                result = ShiftLeft(result, after);
            }

            return DelFirstNull(result);
        }

        /// <summary>
        /// Частное двух чисел неограниченной длины
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns></returns>
        private static string Quotient(string number1, string number2)
        {
            number1 = DelFirstNull(number1);
            number2 = DelFirstNull(number2);

            if (number2 == "0")
            {
                return "Error: divide by zero";
            }

            //return Convert.ToString(Convert.ToDouble(number1) / Convert.ToDouble(number2));
            // Если оба числа отрицательны
            if (number1[0] == '-' && number2[0] == '-')
            {
                number1 = number1.Substring(1);
                number2 = number2.Substring(1);
                return Quotient(number1, number2);
            }

            // Если первое из чисел отрицательно
            if (number1[0] == '-')
            {
                number1 = number1.Substring(1);
                return "-" + Quotient(number1, number2);
            }

            // Если второе из чисел отрицательно
            if (number2[0] == '-')
            {
                number2 = number2.Substring(1);
                return "-" + Quotient(number1, number2);
            }

            int la = QuantityAfterComma(number1),
                lb = QuantityAfterComma(number2);

            // Удаление запятых из чисел
            if (la != 0 || lb != 0)
            {
                int max = Math.Max(la, lb);

                if (la != 0)
                {
                    number1 = number1.Remove(number1.Length - la - 1, 1);
                }

                if (lb != 0)
                {
                    number2 = number2.Remove(number2.Length - lb - 1, 1);
                }

                // Добавление недостающих нулей
                if (la < max)
                {
                    for (int i = 0; i < max - la; i++)
                    {
                        number1 += "0";
                    }
                }

                if (lb < max)
                {
                    for (int i = 0; i < max - lb; i++)
                    {
                        number2 += "0";
                    }
                }

                // Убираем первые нули
                number1 = DelFirstNull(number1);
                number2 = DelFirstNull(number2);
            }

            string result = string.Empty;
            int level = -1;
            string val = string.Empty;

            // Получение наименьшей части числа а (начиная с начала числа), 
            // не длиннее, чем number2
            for (int i = 0; i < number2.Length && number1.Length > 0; i++)
            {
                string xxx = val + number1[0];
                if (Compare(xxx, number2) == -1)
                {
                    val += number1[0];
                    number1 = number1.Substring(1);
                }
                else
                {
                    break;
                }
            }

            //Вычисления
            while (level < 50)
            {
                if (number1 == string.Empty)
                {
                    if (level == -1)
                    {
                        if (result == string.Empty)
                        {
                            result = "0,";
                        }
                        else
                        {
                            result += ",";
                        }

                        level = 0;
                    }
                    else
                    {
                        level++;
                    }

                    if (Compare(val, "0") == 0)
                    {
                        /*if (result[result.Length - 1] == ',')
                          return result + "0";
                        else*/
                        return DelFirstNull(result);
                    }

                    number1 = "0";
                }
                else
                {
                    val += number1[0];
                    number1 = number1.Substring(1);
                    if (Compare(val, number2) == -1)
                    {
                        result += "0";
                        continue;
                    }

                    val = DelFirstNull(val);

                    // Получение числа раз, которое число number2 входит в полученную часть
                    int cnt = 0;
                    while (Compare(val, number2) != -1)
                    {
                        val = Diff(val, number2);
                        cnt++;
                    }

                    if (QuantityAfterComma(val) != 0)
                    {
                        val = val.Remove(val.Length - 2, 2);
                    }
                    result += cnt.ToString();
                }
            }

            if (result == string.Empty)
            {
                return "0";
            }

            return DelFirstNull(result);
        }

        /// <summary>
        /// Частное двух чисел неограниченной длины (целая часть)
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns></returns>
        public static string QuotientDiv(string a, string b)
        {
            a = DelFirstNull(a);
            b = DelFirstNull(b);

            if (b == "0")
            {
                return "Error: divide by zero";
            }

            //return Convert.ToString(Convert.ToDouble(number1) / Convert.ToDouble(number2));
            // Если оба числа отрицательны
            if (a[0] == '-' && b[0] == '-')
            {
                a = a.Substring(1);
                b = b.Substring(1);
                return QuotientDiv(a, b);
            }

            // Если первое из чисел отрицательно
            if (a[0] == '-')
            {
                a = a.Substring(1);
                return "-" + QuotientDiv(a, b);
            }

            // Если второе из чисел отрицательно
            if (b[0] == '-')
            {
                b = b.Substring(1);
                return "-" + QuotientDiv(a, b);
            }

            int la = QuantityAfterComma(a),
                lb = QuantityAfterComma(b);

            // Удаление запятых из чисел
            if (la != 0 || lb != 0)
            {
                int max = Math.Max(la, lb);

                if (la != 0)
                {
                    a = a.Remove(a.Length - la - 1, 1);
                }

                if (lb != 0)
                {
                    b = b.Remove(b.Length - lb - 1, 1);
                }

                // Добавление недостающих нулей
                if (la < max)
                {
                    for (int i = 0; i < max - la; i++)
                    {
                        a += "0";
                    }
                }

                if (lb < max)
                {
                    for (int i = 0; i < max - lb; i++)
                    {
                        b += "0";
                    }
                }

                // Убираем первые нули
                a = DelFirstNull(a);
                b = DelFirstNull(b);
            }

            string result = "0";
            string val = string.Empty;

            // Получение наименьшей части числа а (начиная с начала числа), 
            // не длиннее, чем number2
            for (int i = 0; i < b.Length && a.Length > 0; i++)
            {
                string xxx = val + a[0];
                if (Compare(xxx, b) == -1)
                {
                    val += a[0];
                    a = a.Substring(1);
                }
                else
                {
                    break;
                }
            }

            while (a != string.Empty)
            {
                val += a[0];
                a = a.Substring(1);
                if (Compare(val, b) == -1)
                {
                    result += "0";
                    continue;
                }
                val = DelFirstNull(val);

                // Получение числа раз, которое число number2 входит в полученную часть
                int cnt = 0;
                while (Compare(val, b) != -1)
                {
                    val = Diff(val, b);
                    cnt++;
                }

                if (QuantityAfterComma(val) != 0)
                {
                    val = val.Remove(val.Length - 2, 2);
                }
                result += cnt.ToString();
            }            

            if (result == string.Empty)
            {
                return "0";
            }

            return DelFirstNull(result);
        }


        /// <summary>
        /// Частное двух чисел неограниченной длины (остаток от деления)
        /// </summary>
        /// <param name="number1">Первое число</param>
        /// <param name="number2">Второе число</param>
        /// <returns></returns>
        private static string QuotientMod(string number1, string number2)
        {
            number1 = DelFirstNull(number1);
            number2 = DelFirstNull(number2);

            if (number2 == "0")
            {
                return "Error: divide by zero";
            }

            //return Convert.ToString(Convert.ToDouble(number1) / Convert.ToDouble(number2));
            // Если оба числа отрицательны
            if (number1[0] == '-' && number2[0] == '-')
            {
                number1 = number1.Substring(1);
                number2 = number2.Substring(1);
                return QuotientMod(number1, number2);
            }

            // Если первое из чисел отрицательно
            if (number1[0] == '-')
            {
                number1 = number1.Substring(1);
                return "-" + QuotientMod(number1, number2);
            }

            // Если второе из чисел отрицательно
            if (number2[0] == '-')
            {
                number2 = number2.Substring(1);
                return "-" + QuotientMod(number1, number2);
            }

            int la = QuantityAfterComma(number1),
                lb = QuantityAfterComma(number2);

            int shift = 0;
            // Удаление запятых из чисел
            if (la != 0 || lb != 0)
            {
                int max = Math.Max(la, lb);
                shift = Math.Max(max - la, max - lb);

                if (la != 0)
                {
                    number1 = number1.Remove(number1.Length - la - 1, 1);
                }

                if (lb != 0)
                {
                    number2 = number2.Remove(number2.Length - lb - 1, 1);
                }

                // Добавление недостающих нулей
                if (la < max)
                {
                    for (int i = 0; i < max - la; i++)
                    {
                        number1 += "0";
                    }
                }

                if (lb < max)
                {
                    for (int i = 0; i < max - lb; i++)
                    {
                        number2 += "0";
                    }
                }

                // Убираем первые нули
                number1 = DelFirstNull(number1);
                number2 = DelFirstNull(number2);
            }

            //Вычисления
            while (number1 != "0")
            {
                if (CompareWithLength(number1, number2) == -1)
                {
                    return ShiftLeft(DelFirstNull(number1), shift);
                }

                // Получение наименьшей части числа а (начиная с начала числа), 
                // большей чем number2
                string val = string.Empty;
                for (int i = 0; i < number2.Length && number1.Length > 0; i++)
                {
                    val += number1[0];
                    number1 = number1.Substring(1);
                }

                if (Compare(val, number2) == -1 && number1.Length > 0)
                {
                    val += number1[0];
                    number1 = number1.Substring(1);
                }
                val = DelFirstNull(val);

                // Получение числа раз, которое числа number2 входит в полученную часть
                while (Compare(val, number2) != -1)
                {
                    val = Diff(val, number2);
                }

                if (QuantityAfterComma(val) != 0)
                {
                    val = val.Remove(val.Length - 2, 2);
                }

                number1 = val + number1;

                bool f = true;
                for (int i = 0; i < number1.Length; i++)
                {
                    if (number1[i] != '0')
                    {
                        f = false;
                        break;
                    }
                }

                if (f)
                {
                    return "0";
                }
            }
            return ShiftLeft(DelFirstNull(number1), shift);
        }

        /// <summary>
        /// Возведение неограниченного числа в неограниченную степень
        /// </summary>
        /// <param name="number">Число</param>
        /// <param name="pow">Степень</param>
        /// <returns></returns>
        private static string Pow(string number, string pow)
        {            
            pow = DelFirstNull(pow);
            number = DelFirstNull(number);
            if (number == "0")
            {
                return "0";
            }

            if (pow == "0")
            {
                return "1";
            }

            if (pow[0] == '-')
            {
                return Quotient("1", Pow(number, pow.Substring(1)));
            }

            if (QuantityAfterComma(pow) == 0)
            {
                return PowDiv(number, pow);
            }

            if (Compare(number, "0") < 0)
            {
                return "Error: pow is incorrect";
            }

            try
            {
                return Math.Pow(Convert.ToDouble(number), Convert.ToDouble(pow)).ToString();
            }
            catch
            {
                return "Error: erection of long numbers in a fractional pow is not implemented";
            }
            //return Exp(Product(pow, Log(E, number)));
        }

        /// <summary>
        /// Возведение неограниченного числа в неограниченную целую степень
        /// </summary>
        /// <param name="x">Число</param>
        /// <param name="a">Степень</param>
        /// <returns></returns>
        private static string PowDiv(string x, string a)
        {
            x = DelFirstNull(x);
            a = DelFirstNull(a);
            string result = x;
            string i = "1";

            while (Compare(i, a) < 0)
            {
                result = Product(result, x);
                i = Sum(i, "1");
            }

            return DelFirstNull(result);
        }

        /// <summary>
        /// Вычисление числа е в неограниченной степени
        /// </summary>
        /// <param name="a">Степень, в которую надо возводить число е</param>
        /// <returns></returns>
        private static string Exp(string a)
        {            
            a = DelFirstNull(a);
            if (a == "0")
            {
                return "1";
            }

            if (a == "0")
            {
                return E;
            }

            if (a[0] == '-')
            {
                return Quotient("1", Exp(a.Substring(1)));
            }
            /*
            string modA = Mod(a);
            string rez = PowDiv(E, Div(a));

            string add = Sum("1", modA);
            string node = "1";
            string i = "2";

            while (Compare(node, Eps) > 0)
            {
                node = Quotient(PowDiv(modA, i), Fact(i));
                i = Sum(i, "1");
                add = Sum(add, node);
            }

            rez = Product(rez, add);
            return DelFirstNull(rez);*/

            string modA = Mod(a);            

            string rez = Sum("1", modA);
            string node = "1";
            string i = "2";

            while (Compare(node, Eps) > 0)
            {
                node = Quotient(PowDiv(modA, i), Fact(i));
                i = Sum(i, "1");
                rez = Sum(rez, node);
            }

            return DelFirstNull(rez);
        }

        /// <summary>
        /// Вычисление логарифма числа
        /// </summary>
        /// <param name="a">Основание логарифма</param>
        /// <param name="x">Число</param>
        /// <returns></returns>
        private static string Log(string a, string x)
        {
            x = DelFirstNull(x);
            a = DelFirstNull(a);

            if (Compare(a, "0") <= 0 || Compare(x, "0") <= 0)
            {
                return "Error: logariphm is incorrect";
            }

            if (Compare(a, "1") < 0)
            {

                return Log(Quotient("1", a), x);
            }

            string y = "0";
            string z = x;
            string t = "1";

            while (Compare(Abs(t), Eps) >= 0 || Compare(z, Quotient("1", a)) <= 0 || Compare(z, a) >= 0)
            {
                if (Compare(z, a) >= 0)
                {
                    z = Quotient(z, a);
                    y = Sum(y, t);
                }
                else if (Compare(z, Quotient("1", a)) <= 0)
                {
                    z = Product(z, a);
                    y = Diff(y, t);
                }
                else
                {
                    z = Product(z, z);
                    t = Quotient(t, "2");
                }
            }

            return DelFirstNull(y);
        }        
    }
}
