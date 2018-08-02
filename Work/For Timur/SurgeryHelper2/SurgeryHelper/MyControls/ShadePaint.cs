using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.MyControls
{
    public partial class ShadePaint : UserControl
    {
        private Bitmap _picture;
        private Bitmap _tempPicture;
        private readonly Bitmap _savePicture;

        private Color _fillColor;
        
        private readonly int[] _mColors = new[] { 5, 65, 145, 227, 248, 255 };

        /// <summary>
        /// Вернуть текущую картинку
        /// </summary>
        public Bitmap CurrentPicture
        {
            get
            {
                return _picture;
            }
        }

        /// <summary>
        /// true - если картинку надо экспортировать в ворд
        /// </summary>
        public bool IsExportEnabled
        {
            get
            {
                return checkBoxWordExport.Checked;
            }
        }

        public ShadePaint(Bitmap picture, string caption)
        {
            InitializeComponent();

            labelCaption.Text = caption;
            _picture = new Bitmap(picture);
            _savePicture = new Bitmap(picture);
            panelPaint.Width = picture.Width;
            panelPaint.Height = picture.Height;
            Width = panelPaint.Width + 70;
            Height = panelPaint.Height + 21;
        }


        /// <summary>
        /// Получить цвет, у которого все составляющие равны переданному значению
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        private static Color GetGreyColor(int value)
        {
            return Color.FromArgb(255, value, value, value);
        }


        /// <summary>
        /// Отображение картинки на форме
        /// Раскрашивание панелек с цветами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ShadePaint_Load(object sender, EventArgs e)
        {
            panelM5.BackColor = GetGreyColor(_mColors[5]);
            panelM4.BackColor = GetGreyColor(_mColors[4]);
            panelM3.BackColor = GetGreyColor(_mColors[3]);
            panelM2.BackColor = GetGreyColor(_mColors[2]);
            panelM1.BackColor = GetGreyColor(_mColors[1]);
            panelM0.BackColor = GetGreyColor(_mColors[0]);

            _fillColor = panelM0.BackColor;
        }


        /// <summary>
        /// Кнопка отмены последнего изменения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            _picture = new Bitmap(_tempPicture);
            using (Graphics g = panelPaint.CreateGraphics())
            {
                g.DrawImage(_picture, 0, 0);
            }

            buttonUndo.Visible = false;
        }


        /// <summary>
        /// Кнопка возврата исходного изображения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            _picture = new Bitmap(_savePicture);
            using (Graphics g = panelPaint.CreateGraphics())
            {
                g.DrawImage(_picture, 0, 0);
            }
        }


        /// <summary>
        /// Выбор цвета при клике мышью по панельке с цветом
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelM_MouseClick(object sender, MouseEventArgs e)
        {
            var panelM = (Panel)sender;

            _fillColor = panelM.BackColor;

            string panelNumber = panelM.Name.Substring(panelM.Name.Length - 1);

            foreach (Control control in Controls)
            {
                if (!(control is Label) ||
                    !control.Name.StartsWith("labelM"))
                {
                    continue;
                }

                var labelM = (Label)control;
                labelM.BorderStyle = panelNumber == labelM.Name.Substring(labelM.Name.Length - 1) 
                    ? BorderStyle.FixedSingle 
                    : BorderStyle.None;
            }
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.D0)
            {
                panelM_MouseClick(panelM0, null);
                return true;
            }

            if (keyData == Keys.D1)
            {
                panelM_MouseClick(panelM1, null);
                return true;
            }

            if (keyData == Keys.D2)
            {
                panelM_MouseClick(panelM2, null);
                return true;
            }

            if (keyData == Keys.D3)
            {
                panelM_MouseClick(panelM3, null);
                return true;
            }

            if (keyData == Keys.D4)
            {
                panelM_MouseClick(panelM4, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }


        #region Подсказки
        private void buttonUndo_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отменить последнее действие", buttonUndo);
            buttonUndo.FlatStyle = FlatStyle.Popup;
        }

        private void buttonUndo_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonUndo.FlatStyle = FlatStyle.Flat;
        }

        private void buttonRefresh_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Вернуть исходное изображение", buttonRefresh);
            buttonRefresh.FlatStyle = FlatStyle.Popup;
        }

        private void buttonRefresh_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonRefresh.FlatStyle = FlatStyle.Flat;
        }
        #endregion


        /// <summary>
        /// Заливка части изображения выбранным цветом
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelPaint_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.X < 0 && e.Y < 0)
                {
                    return;
                }

                _tempPicture = new Bitmap(_picture);

                Graphics g = panelPaint.CreateGraphics();

                Fill(g, new Point(e.X, e.Y), _fillColor, _mColors);

                g.DrawImage(_picture, 0, 0);
                g.Dispose();                
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Перерисовка панели с картинкой
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelPaint_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = panelPaint.CreateGraphics())
            {
                g.DrawImage(_picture, 0, 0);
            }
        }


        /// <summary>
        /// Заливка области
        /// </summary>
        /// <param name="g">Графикс отображаемого объекта (например, панели)</param>
        /// <param name="pos">Точка, в которой начинается заливка</param>    
        /// <param name="colorFill">Цвет заливки</param>
        /// <param name="possibleColors">Список серых цветов, которые можно закрашивать</param>
        private void Fill(Graphics g, Point pos, Color colorFill, IEnumerable<int> possibleColors)
        {
            // Цвет в точке, с которой начинается заливка
            Color colorBegin = _picture.GetPixel(pos.X, pos.Y);

            bool isColorBeginPossible = false;
            foreach (int colorValue in possibleColors)
            {
                if (colorBegin.R == colorBegin.B &&
                    colorBegin.R == colorBegin.G && colorBegin.R == colorValue)
                {
                    isColorBeginPossible = true;
                    break;
                }
            }

            if (!isColorBeginPossible)
            {
                return;
            }

            // DC панели
            IntPtr panelDC = g.GetHdc();

            // DC в памяти, совместимый с панелью
            IntPtr memDC = Win32Engine.CreateCompatibleDC(panelDC);

            // Создаем и подсовываем свою кисть
            IntPtr hBrush = Win32Engine.CreateSolidBrush((uint)ColorTranslator.ToWin32(colorFill));
            IntPtr hOldBr = Win32Engine.SelectObject(memDC, hBrush);

            // Подсовываем свой битмап
            IntPtr hBmp = _picture.GetHbitmap();
            IntPtr hOldBmp = Win32Engine.SelectObject(memDC, hBmp);

            // Заливаем (заливается благодаря совместимости с панелью, в противном случае 
            // заливки на битмапе не произойдет)
            Win32Engine.ExtFloodFill(memDC, pos.X, pos.Y, (uint)ColorTranslator.ToWin32(colorBegin), 1);

            // Записываем полученный залитый битмап в наш битмап
            _picture.Dispose();
            _picture = Image.FromHbitmap(hBmp);

            // Возвращаем на место предыдущие кисть и битмап
            Win32Engine.SelectObject(memDC, hOldBr);
            Win32Engine.SelectObject(memDC, hOldBmp);

            // Освобождаем использованные ресурсы
            Win32Engine.DeleteObject(hBmp);
            Win32Engine.DeleteObject(hBrush);
            Win32Engine.DeleteObject(memDC);
            g.ReleaseHdc(panelDC);

            buttonUndo.Visible = true;
            checkBoxWordExport.Checked = true;
        }


        private int _saveX;
        private int _saveY;

        /// <summary>
        /// Запуск таймера, чтобы через некоторое время он отобразил, есть ли что-то под мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelPaint_MouseMove(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
            _saveX = e.X;
            _saveY = e.Y;
        }


        /// <summary>
        /// Отображение цвета под курсором
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Цвет в точке, с которой начинается заливка
            labelCurrentM.Text = string.Empty;
            Color colorBegin = _picture.GetPixel(_saveX, _saveY);
            for (int i = 0; i < _mColors.Length; i++)
            {
                if (colorBegin.R == _mColors[i])
                {
                    labelCurrentM.Text = "M" + i;
                }
            }
            
        }
    }
}
