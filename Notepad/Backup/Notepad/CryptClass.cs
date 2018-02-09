using System.Drawing;
using System.Drawing.Imaging;

namespace Notepad
{
    public class CryptClass
    {
        private int _m;		// Количество координат в массивах ismas и tomas
        private int _n;		// Код символа

        private int[,] _ismas; // Два массива для координат точек. 1 - откуда вести линию, 2 - куда её вести
        private int[,] _tomas;

        private readonly int _bitWidth; // Размер получаемого битмапа
        private readonly int _bitHeight;

        private readonly int[,] _masDecr = new int[6, 7]; // Массив для дешифрования

        public CryptClass(int bW, int bH)
        {
            _bitWidth = bW;
            _bitHeight = bH;

            int k = 1078;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    _masDecr[i, j] = k;
                    k++;
                }
            }
            _masDecr[4, 5] = 44;
            _masDecr[4, 6] = 46;
            _masDecr[0, 6] = 1105;
            k = 1072;
            for (int j = 0; j < 6; j++)
            {
                _masDecr[0, j] = k;
                k++;
            }
            _masDecr[5, 0] = 13;
            _masDecr[5, 1] = 10;
            _masDecr[5, 2] = 32;
            _masDecr[5, 3] = 33;
            _masDecr[5, 4] = 63;
            _masDecr[5, 5] = 34;
            _masDecr[5, 6] = 58;
        }

        /// <summary>
        /// Получение точек для рисования человечка
        /// </summary>
        private void Body()
        {
            _ismas = new int[2, 6];
            _tomas = new int[2, 6];
            _m = 0;
            if (_n >= 1040 && _n <= 1071)
            {
                _n += 32;
            }
            else if (_n == 1025)
            {
                _n = 1105;
            }
            if ((_n < 1072 || _n > 1103) && _n != 1105 && _n != 46 && _n != 44 && _n != 13 && _n != 10 &&
              (_n < 32 || _n > 34) && _n != 63 && _n != 58)
            {
                return;
            }

            _n -= 1072;
            if (_n >= 0 && _n <= 5 || _n == 33)
            {
                Legs(1);
            }
            else if (_n >= 6 && _n <= 12)
            {
                Legs(2);
            }
            else if (_n >= 13 && _n <= 19)
            {
                Legs(3);
            }
            else if (_n >= 20 && _n <= 26)
            {
                Legs(4);
            }
            else if (_n >= 27 && _n <= 31 || _n == -1028 || _n == -1026)
            {
                Legs(5);
            }
            else
            {
                Legs(6);
            }

            if (_n == -1059)
            {
                Arms(1);
            }
            else if (_n == -1062)
            {
                Arms(2);
            }
            else if (_n == -1040)
            {
                Arms(3);
            }
            else if (_n == -1039)
            {
                Arms(4);
            }
            else if (_n == -1009)
            {
                Arms(5);
            }
            else if (_n == -1028 || _n == -1038)
            {
                Arms(6);
            }
            else if (_n == -1026 || _n == 33 || _n == -1014)
            {
                Arms(7);
            }
            else if (_n < 6)
            {
                Arms(_n + 1);
            }
            else
            {
                while (_n >= 6)
                {
                    _n -= 7;
                }
                Arms(_n + 2);
            }
        }

        /// <summary>
        /// Получение точек для рисования рук человечка
        /// </summary>
        /// <param name="nType"></param>
        private void Arms(int nType)
        {
            switch (nType)
            {
                case 1:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 3;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 6;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 3;
                    _m++;
                    break;
                case 2:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 3;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 6;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 6;
                    _m++;
                    break;
                case 3:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 6;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 3;
                    _m++;
                    break;
                case 4:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 6;
                    _m++;
                    break;
                case 5:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 6;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 9;
                    _m++;
                    break;
                case 6:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 9;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 6;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 6;
                    _m++;
                    break;
                case 7:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 9;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 6;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 6;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 9;
                    _m++;
                    break;
            }
        }

        /// <summary>
        /// Получение точек для рисования ног человечка
        /// </summary>
        /// <param name="nType"></param>
        private void Legs(int nType)
        {
            switch (nType)
            {
                case 1:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 14;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 5;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 14;
                    _m++;
                    break;
                case 2:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 14;
                    _tomas[0, _m] = 0;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 8;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 14;
                    _m++;
                    break;
                case 3:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 14;
                    _tomas[0, _m] = 0;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 9;
                    _tomas[1, _m] = 11;
                    _m++;
                    break;
                case 4:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 8;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 14;
                    _m++;
                    break;
                case 5:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 14;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 3;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 8;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 14;
                    _m++;
                    break;
                case 6:
                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 14;
                    _tomas[0, _m] = 0;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 0;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 3;
                    _tomas[1, _m] = 11;
                    _m++;

                    _ismas[0, _m] = 3;
                    _ismas[1, _m] = 11;
                    _tomas[0, _m] = 8;
                    _tomas[1, _m] = 14;
                    _m++;
                    break;
            }
        }

        /// <summary>
        /// Шифрование надписи 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Bitmap Crypted(string text)
        {
            var poBit = new Bitmap(_bitWidth, _bitHeight, PixelFormat.Format32bppArgb);
            Graphics poGraphics = Graphics.FromImage(poBit);

            var poPen = new Pen(Color.FromArgb(255, 0, 0, 0));
            int a = -1, b = 0;     // Переменные для определения места на картинке, куда надо
            // рисовать человечка (зависит от номера буквы)

            var poBrush = new SolidBrush(Color.White);
            poGraphics.FillRectangle(poBrush, 0, 0, _bitWidth - 1, _bitHeight - 1);

            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    _n = text[i];
                    Body();

                    if (_m == 0)
                    {
                        if (text[i] == 32 && a + b >= 0 && (i < 0 || text[i] != 32))
                        {
                            int j;

                            if (poBit.GetPixel(a * 15 + 8, b * 20 + 3) == Color.FromArgb(255, 0, 0, 0))
                            {
                                j = 1;
                            }
                            else if (poBit.GetPixel(a * 15 + 8, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0))
                            {
                                j = 2;
                            }
                            else
                            {
                                j = 3;
                            }

                            switch (j)
                            {
                                case 1:
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 2, a * 15 + 8, b * 20 + 0);
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 0, a * 15 + 9, b * 20 + 0);
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 1, a * 15 + 9, b * 20 + 1);
                                    break;
                                case 2:
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 5, a * 15 + 8, b * 20 + 2);
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 2, a * 15 + 9, b * 20 + 2);
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 3, a * 15 + 9, b * 20 + 3);
                                    break;
                                case 3:
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 8, a * 15 + 8, b * 20 + 5);
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 5, a * 15 + 9, b * 20 + 5);
                                    poGraphics.DrawLine(poPen, a * 15 + 8, b * 20 + 6, a * 15 + 9, b * 20 + 6);
                                    break;
                            }
                        }
                        continue;
                    }
                    a++;
                    if (a == _bitWidth / 15)
                    {
                        a = 0;
                        b++;
                    }
                    if (b == _bitHeight / 20)
                    {
                        return poBit;
                    }
                    poGraphics.DrawLine(poPen, a * 15 + 4, b * 20 + 2, a * 15 + 4, b * 20 + 10);
                    poGraphics.DrawLine(poPen, a * 15 + 3, b * 20 + 3, a * 15 + 5, b * 20 + 3);
                    poGraphics.DrawLine(poPen, a * 15 + 3, b * 20 + 4, a * 15 + 5, b * 20 + 4);

                    for (int j = 0; j < _m; j++)
                    {
                        poGraphics.DrawLine(poPen, a * 15 + _ismas[0, j], b * 20 + _ismas[1, j], a * 15 + _tomas[0, j], b * 20 + _tomas[1, j]);
                    }
                }
                return poBit;
            }
            finally
            {
                poPen.Dispose();
                poBrush.Dispose();
                poGraphics.Dispose();
            }
        }

        /// <summary>
        /// Дешифрование рисунка
        /// </summary>
        /// <param name="poBit"></param>
        /// <returns></returns>
        public string DeCrypted(Bitmap poBit)
        {
            string res = "";
            int a = 0, b = 0;

            while (b <= _bitHeight / 20 && poBit.GetPixel(a * 15 + 4, b * 20 + 2) == Color.FromArgb(255, 0, 0, 0))
            {
                int x, // Ноги
                    y; // Руки

                if (poBit.GetPixel(a * 15 + 2, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0) &&
                    poBit.GetPixel(a * 15 + 6, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0))
                {
                    x = 0;
                }
                else if (poBit.GetPixel(a * 15 + 0, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 8, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0))
                {
                    x = 1;
                }
                else if (poBit.GetPixel(a * 15 + 9, b * 20 + 11) == Color.FromArgb(255, 0, 0, 0))
                {
                    x = 2;
                }
                else if (poBit.GetPixel(a * 15 + 2, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 8, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0))
                {
                    x = 4;
                }
                else if (poBit.GetPixel(a * 15 + 0, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 4, b * 20 + 12) == Color.FromArgb(255, 0, 0, 0))
                {
                    x = 5;
                }
                else
                {
                    x = 3;
                }

                if (poBit.GetPixel(a * 15 + 1, b * 20 + 4) == Color.FromArgb(255, 0, 0, 0) &&
                    poBit.GetPixel(a * 15 + 7, b * 20 + 4) == Color.FromArgb(255, 0, 0, 0))
                {
                    y = 0;
                }
                else if (poBit.GetPixel(a * 15 + 1, b * 20 + 4) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 7, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0))
                {
                    y = 1;
                }
                else if (poBit.GetPixel(a * 15 + 1, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 7, b * 20 + 4) == Color.FromArgb(255, 0, 0, 0))
                {
                    y = 2;
                }
                else if (poBit.GetPixel(a * 15 + 1, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 7, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0))
                {
                    y = 3;
                }
                else if (poBit.GetPixel(a * 15 + 1, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 7, b * 20 + 8) == Color.FromArgb(255, 0, 0, 0))
                {
                    y = 4;
                }
                else if (poBit.GetPixel(a * 15 + 1, b * 20 + 8) == Color.FromArgb(255, 0, 0, 0) &&
                         poBit.GetPixel(a * 15 + 7, b * 20 + 6) == Color.FromArgb(255, 0, 0, 0))
                {
                    y = 5;
                }
                else
                {
                    y = 6;
                }

                res += (char)_masDecr[x, y];
                if (poBit.GetPixel(a * 15 + 9, b * 20 + 0) == Color.FromArgb(255, 0, 0, 0) ||
                    poBit.GetPixel(a * 15 + 9, b * 20 + 2) == Color.FromArgb(255, 0, 0, 0) ||
                    poBit.GetPixel(a * 15 + 9, b * 20 + 5) == Color.FromArgb(255, 0, 0, 0))
                {
                    res += " ";
                }

                a++;
                if (a == _bitWidth / 15)
                {
                    a = 0;
                    b++;
                }
            }
            return res;
        }
    }
}
