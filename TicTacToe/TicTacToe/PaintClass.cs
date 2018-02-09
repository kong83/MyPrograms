using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace TicTacToe
{
    public class PaintClass
    {
        public const int BitmapWidth = 601;
        public const int BitmapHeight = 501;
        public const int CellSize = 30;

        private int m_LastX;
        private int m_LastY;
        private ObjectType m_LastWhoStep;

        private readonly Bitmap m_FieldBitmap;
        public Bitmap FieldBitmap
        {
            get
            {
                return m_FieldBitmap;
            }
        }


        public PaintClass()
        {
            m_FieldBitmap = new Bitmap(BitmapWidth, BitmapHeight);
            ClearField();
        }


        public void ClearField()
        {
            using (Graphics g = Graphics.FromImage(m_FieldBitmap))
            {
                var pen = new Pen(Color.Black, 1);

                g.Clear(Color.White);
                for (int i = 0; i < BitmapWidth; i += CellSize)
                {
                    g.DrawLine(pen, i, 0, i, BitmapHeight);

                }

                for (int j = 0; j < BitmapHeight; j += CellSize)
                {
                    g.DrawLine(pen, 0, j, BitmapWidth, j);
                }
            }
            m_LastX = m_LastY = -1;
        }


        public void ShowLastStep(MainForm mainForm, int blinkCnt)
        {
            if (m_LastX == -1 || m_LastY == -1)
            {
                return;
            }

            for (int i = 0; i < blinkCnt; i++)
            {
                DrawObject(ObjectType.Empty, m_LastX, m_LastY, false);
                mainForm.PaintBitmap();
                Application.DoEvents();
                Thread.Sleep(150);
                DrawObject(m_LastWhoStep, m_LastX, m_LastY, false);
                mainForm.PaintBitmap();
                Application.DoEvents();
                Thread.Sleep(150);
            }
        }


        public void DrawObject(ObjectType objectType, int x, int y)
        {
            DrawObject(objectType, x, y, true);
        }

        private void DrawObject(ObjectType objectType, int x, int y, bool isSaveParameters)
        {
            if (isSaveParameters)
            {
                m_LastWhoStep = objectType;
                m_LastX = x;
                m_LastY = y;
            }

            var drawedObject = new Bitmap(CellSize, CellSize);
            if (objectType == ObjectType.Cross)
            {
                using (Graphics gCell = Graphics.FromImage(drawedObject))
                {
                    int lx = (CellSize - Properties.Resources.cross.Width) / 2;
                    int ly = (CellSize - Properties.Resources.cross.Height) / 2;                    
                    gCell.DrawImage(Properties.Resources.cross, lx, ly);
                }
            }
            else if (objectType == ObjectType.Nil)
            {
                using (Graphics gCell = Graphics.FromImage(drawedObject))
                {
                    int lx = (CellSize - Properties.Resources.nil.Width) / 2;
                    int ly = (CellSize - Properties.Resources.nil.Height) / 2;
                    gCell.DrawImage(Properties.Resources.nil, lx, ly);
                }
            }
            else
            {
                using (Graphics gCell = Graphics.FromImage(drawedObject))
                {
                    gCell.Clear(Color.White);
                    gCell.DrawRectangle(new Pen(Color.Black), 0, 0, CellSize, CellSize);
                }
            }

            using (Graphics g = Graphics.FromImage(m_FieldBitmap))
            {
                g.DrawImage(drawedObject, x * CellSize, y * CellSize);
            }
        }


        public void DrawWinLine(ObjectType whoWin, int x1, int y1, int x2, int y2)
        {
            var pen = new Pen(Color.Brown)
            {
                Width = 2
            };

            int rx1 = x1 * CellSize + CellSize / 2;
            int ry1 = y1 * CellSize + CellSize / 2;
            int rx2 = x2 * CellSize + CellSize / 2;
            int ry2 = y2 * CellSize + CellSize / 2;

            using (Graphics g = Graphics.FromImage(m_FieldBitmap))
            {
                g.DrawLine(pen, rx1, ry1, rx2, ry2);
            }
        }

        public void DrawCoefficients(int rowsCnt, int columnsCnt, int[,] coeffs)
        {
            var copyCoeff = new double[rowsCnt, columnsCnt];
            for (int i = 0; i < rowsCnt; i++)
            {
                for (int j = 0; j < columnsCnt; j++)
                {
                    copyCoeff[i, j] = coeffs[i, j];
                }
            }
            DrawCoefficients(rowsCnt, columnsCnt, copyCoeff);
        }

        /// <summary>
        /// Вывести на экран список коэффициентов
        /// </summary>
        /// <param name="rowsCnt"></param>
        /// <param name="columnsCnt"></param>
        /// <param name="coeffs"></param>        
        public void DrawCoefficients(int rowsCnt, int columnsCnt, double[,] coeffs)
        {
            using (Graphics g = Graphics.FromImage(m_FieldBitmap))
            {
                var textBrush = new SolidBrush(Color.Black);
                var brush = new SolidBrush(Color.White);
                var font = new Font(FontFamily.GenericSansSerif, 5);
                for (int i = 0; i < rowsCnt; i++)
                {
                    for (int j = 0; j < columnsCnt; j++)
                    {
                        g.FillRectangle(brush, i * CellSize + 1, j * CellSize + 1, 15, 8);
                        g.DrawString(coeffs[i, j].ToString("F1"), font, textBrush, i * CellSize + 1, j * CellSize + 1);
                    }
                }
            }
        }

        public void ClearCoefficients(int rowsCnt, int columnsCnt)
        {
            using (Graphics g = Graphics.FromImage(m_FieldBitmap))
            {
                var brush = new SolidBrush(Color.White);
                for (int i = 0; i < rowsCnt; i++)
                {
                    for (int j = 0; j < columnsCnt; j++)
                    {
                        g.FillRectangle(brush, i * CellSize + 1, j * CellSize + 1, 15, 8);                        
                    }
                }
            }
        }
        
    }
}
