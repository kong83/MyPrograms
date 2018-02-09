namespace Calculator
{
    class ExpressionVerifier
    {
        /// <summary>
        /// ������, ���������� ���������
        /// </summary>
        private readonly string _expressionString;

        public ExpressionVerifier(string expressionString)
        {
            _expressionString = expressionString;
        }

        public static bool IsDecNumber(string str)
        {
            foreach (char ch in str)
            {
                if ((ch < '0' || ch > '9') && !IsSeparator(ch))
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// �������� �� ������ ��������
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool IsOperation(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '%':
                case '^':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// ���������, ����� �����, ��������� �� ����� ����� ���������� �� 0 � 
        /// ������� ������ ���� �� ����� (02 -> 2) 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveBeganZerosFromNumber(string s)
        {
            int n = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0' || IsSeparator(s[i]))
                {
                    n++;
                }
                else
                {
                    break;
                }
            }

            if (n == s.Length)
            {
                return "0";
            }

            while (1 < s.Length && s[0] == '0' && !IsSeparator(s[1]))
            {
                s = s.Substring(1);
            }

            return s;
        }

        /// <summary>
        /// �������� �� ������������� ������
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsOpenBracket(char ch)
        {
            switch (ch)
            {
                case '<':
                case '{':
                case '(':
                case '[':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// �������� �� ������������� ������
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsCloseBracket(char ch)
        {
            switch (ch)
            {
                case '>':
                case '}':
                case ')':
                case ']':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// �������� �� �����
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsDigit(char ch)
        {
            switch (ch)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// �������� �� ����������� ����� � ������� ����� 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsSeparator(char ch)
        {
            switch (ch)
            {
                case '.':
                case ',':
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// �������� �� ������������ ������������ ������ 
        /// ������������� ������� ��, ������� �������������, ����� ����������� ����� ���� �����������
        /// </summary>
        /// <returns></returns>
        private bool CheckBrackets()
        {
            string bracketsOnly = string.Empty;
            int count = 0;

            // �������� ������ ������ �� ������
            for (int i = 0; i < _expressionString.Length; i++)
            {    
                if (IsOpenBracket(_expressionString[i]) || IsCloseBracket(_expressionString[i]))
                {
                    bracketsOnly += _expressionString[i];
                }
            }

            // �������� �� ������������ ����������� ������
            for (int i = 0; i < bracketsOnly.Length; i++)
            {   
                if (IsOpenBracket(bracketsOnly[i]))
                {
                    count++;
                }
                else
                {
                    if (count == 0)
                    {
                        return false;
                    }
                    
                    count--;
                }
            }

            if (count != 0)
            {
                return false;
            }

            // �������� ������������ ����������� ������ (����� "(" ����, "(" ��� ������ ������, ����� - ����� ��� "(".
            // ����� ")" �����, ����� ����, ")" ��� ����� ������ 
            for (int i = 0; i < _expressionString.Length; i++)
            {
                if (IsOpenBracket(_expressionString[i]))
                {
                    if (i > 0 && !IsOperation(_expressionString[i - 1]) && !IsOpenBracket(_expressionString[i - 1]))
                    {
                        return false;
                    }

                    if (!IsDigit(_expressionString[i + 1]) && !IsOpenBracket(_expressionString[i + 1]))
                    {
                        return false;
                    }
                }
                else if (IsCloseBracket(_expressionString[i]))
                {
                    if (i < _expressionString.Length - 1 && !IsOperation(_expressionString[i + 1]) && !IsCloseBracket(_expressionString[i + 1]))
                    {
                        return false;
                    }

                    if (!IsDigit(_expressionString[i - 1]) && !IsCloseBracket(_expressionString[i - 1]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// �������� �� ������������ ������ ����� 
        /// (�� ������� � ����� �� ����� ����� ����� ��� ������� + ����� �� ������������ � �� ���������� � . ��� ,)
        /// </summary>
        /// <returns></returns>
        private bool CheckNumbers()
        {
            for (int i = 0; i < _expressionString.Length; i++)
            {
                if (IsDigit(_expressionString[i]))
                {
                    if (i > 0 && IsSeparator(_expressionString[i - 1]))
                    {
                        return false;
                    }

                    while (i < _expressionString.Length && IsDigit(_expressionString[i]))
                    {
                        i++;
                    }

                    if (i < _expressionString.Length && IsSeparator(_expressionString[i]))
                    {
                        if (i >= _expressionString.Length - 1 || !IsDigit(_expressionString[i + 1]))
                        {
                            return false;
                        }

                        i++;
                        while (i < _expressionString.Length && IsDigit(_expressionString[i]))
                        {
                            i++;
                        }

                        if (i < _expressionString.Length && IsSeparator(_expressionString[i]))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// �������� ������������ ����������� ������
        /// ����� �� ���� ������� ��, ������� ����� ����� 1
        /// </summary>
        /// <returns></returns>
        private bool CheckSigns()
        {
            int singsCount = 0;             // ���������� ������
            int numbersCount = 0;           // ���������� �����

            // ������� ���������� ������
            for (int i = 0; i < _expressionString.Length; i++)
            {       
                if (IsOperation(_expressionString[i]))
                {
                    singsCount++;
                }
            }

            // ������� ���������� �����
            for (int i = 0; i < _expressionString.Length; i++)
            {       
                if (IsDigit(_expressionString[i]))
                {
                    numbersCount++;
                    while (i < _expressionString.Length && (IsDigit(_expressionString[i]) || IsSeparator(_expressionString[i])))
                    {
                        i++;
                    }
                }
            }

            if (singsCount != numbersCount - 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����� � ������ ������������� �������� 
        /// </summary>
        /// <returns></returns>
        private bool CheckCorrectCharacters()
        {
            for (int i = 0; i < _expressionString.Length; i++)
            {
                if (!IsDigit(_expressionString[i]) && !IsSeparator(_expressionString[i]) && !IsOperation(_expressionString[i]) && 
                    !IsOpenBracket(_expressionString[i]) && !IsCloseBracket(_expressionString[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �������� ���������� ���������
        /// ����, � ����� ������� ����������� ������, ����� ���������� � �����
        /// ������ - �����, ������ - ����, ����� �����, ����� ���� (������ �� ���������)
        /// </summary>
        /// <returns></returns>
        private bool CheckSyntax()
        {
            string noBrackets = string.Empty;

            // �������� ������ ��� ������
            for (int i = 0; i < _expressionString.Length; i++)
            {   
                if (!IsOpenBracket(_expressionString[i]) && !IsCloseBracket(_expressionString[i]))
                {
                    noBrackets += _expressionString[i];
                }
            }

            int l = 0;                            
            // �������� ��������� ��� ������
            while (l < noBrackets.Length)
            {
                if (IsDigit(noBrackets[l]))
                {
                    while (l < noBrackets.Length && (IsDigit(noBrackets[l]) || IsSeparator(noBrackets[l])))
                    {
                        l++;
                    }

                    if (l == noBrackets.Length)
                    {
                        break;
                    }

                    if (!IsOperation(noBrackets[l]))
                    {
                        return false;
                    }
                    
                    l++;
                }
                else
                {
                    return false;
                }
            }

            if (IsOperation(noBrackets[noBrackets.Length - 1]))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// �������� ��������� �� �������������� ������������
        /// 0 - ��� ���������
        /// 1 - ����������� ����������� ������
        /// 2 - ������ � ���������� ������
        /// 3 - ������ � ������ �����
        /// 4 - ������������ ������ � ���������
        /// 5 - ������ � ���������� ���������
        /// </summary>
        /// <returns></returns>
        public byte Verify()
        {
            if (!CheckCorrectCharacters())
            {
                return 4;
            }

            if (!CheckBrackets())
            {
                return 1;
            }

            if (!CheckNumbers())
            {
                return 3;
            }

            if (!CheckSigns())
            {
                return 2;
            }

            if (!CheckSyntax())
            {
                return 5;
            }

            return 0;
        }
    }
}
