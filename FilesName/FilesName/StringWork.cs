using System.IO;

namespace FilesName
{
    class StringWork
    {
        /// <summary>
        /// ������� ������, �������������� ����� ��� ����� ��� ��� ����������. ���������� �� ������� ���� � �����.
        /// </summary>
        private readonly string _str;

        /// <summary>
        /// ���������� ������� ������
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
        /// ������ ��� ����������� �� ������� ������. ���������� ������ � ���� �� 0 ��� ������������� ������� ������.
        /// </summary>
        private int _iStr;

        /// <summary>
        /// ���������� ������� ��������� _iStr - ������� �� ������
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
        /// ������ ��� ������
        /// </summary>
        private readonly string _textFind;


        /// <summary>
        /// ����� � ������ ���� � �����, � �������� ���������� ���� ������� ������
        /// </summary>
        public readonly int Offset;

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="fullname">������ ��� � �����</param>
        /// <param name="tf">����� ��� ������</param>
        /// <param name="n">1 - ���� ���� � �����, 2 - ���� � ���������� �����</param>
        public StringWork(string fullname, string tf, short n)
        {            
            if (n == 1)  // ��������� ����� �����
            {
                _str = Path.GetFileNameWithoutExtension(fullname);
                if(Path.HasExtension(fullname))
                    Offset = fullname.LastIndexOf(".") - _str.Length;
                else
                    Offset = fullname.Length - _str.Length;
            }
            else                  // ��������� ���������� �����
            {
                _str = Path.GetExtension(fullname).Substring(1);                
                Offset = fullname.LastIndexOf(".") + 1;                
            }

            _iStr = 0;
            _textFind = tf;
        }

        
        /// <summary>
        /// ���������� ���������, ������������ � ������� n � ��������������� ������ ������ ��� �������� '*'
        /// </summary>
        /// <param name="n">��������� ������</param>
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
        ///  ����� ��������� sub � ������� ������ ������� � ������� _iStr. 
        /// � ������ ������������ ������������ -1
        /// </summary>
        /// <param name="sub">��������� ��� ������</param>    
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