using System;

namespace Calculator
{
    class Perevod
    {
        private const string E = "0.00000001";		// ��� ������ ����

        private readonly string _str = string.Empty;          // ������ ��� �������� ����� ��� ��������
        private readonly bool _minus;				// ��������� �������������� �����        

        /// <summary>
        /// �����������
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
        /// ���������� ���������� ����� ����� ������� -1
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
        /// ������� ������� � �����
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
        /// ������� ����� � ������
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
        /// ��������� ����� ����� ����� �� str
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
        /// ��������� ������� ����� ����� �� str  
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
        /// ��������� ����� �� ������� ��������� currentNotation � ���������� ������� ���������
        /// </summary>
        /// <param name="currentNotation">������� ������� ��������� �����</param>
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
        /// ��������� ����� �� ���������� ������� � ������� ��������� targetNotation
        /// </summary>
        /// <param name="targetNotation">������� ���������, � ������� ���� ��������� �����</param>
        /// <returns></returns>
        public string ConvertNumberFromDecToTargetNotation(int targetNotation)
        {
            if (targetNotation != 10)
            {
                var div = new BigNumber(GetDiv(_str));  // ��������� ����� ����� �����                       
                var mod = new BigNumber(GetMod(_str));  // ��������� ����� ��� ����� �����                
                string s1 = string.Empty,               // ����� ����� ������� � ����� ������� ���������
                       s2 = string.Empty;               // ����� ����� ������� � ����� ������� ���������
                int kol = 0;							// ���������� ��������� � ������� mas
                var mas = new string[25];               // ������, ��� ������ ����� ��� ��������� ������� �����               

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