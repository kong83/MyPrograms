using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using SurgeryHelper.Tools;

namespace SurgeryHelper.MyControls
{
    public sealed class MultiRowListBox : ListBox
    {
        private bool _stopScrollCatching;
        public event EventHandler<CScrollEventArgs> VScrollingChange;

        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }

            set
            {
                base.Enabled = value;

                Refresh();
            }
        }

        public MultiRowListBox()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
            SelectionMode = SelectionMode.MultiExtended;
            IntegralHeight = false;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            const int cornerRadius = 4;     // Радиус закругления  

            // e - элемент, с которым мы дальше и работаем
            // если текущего элемента нет или в списке нет вообще элементов,
            // значит выходить из метода
            if (e.Index <= -1 || Items.Count == 0)
            {
                return;
            }

            // получаем текст элемента
            string s = Items[e.Index].ToString();

            // формат строки для рисования текста
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Near
            };

            // создаем обычную кисть с заданным цветом
            Brush solidBrush = new SolidBrush(Color.FromArgb(45, 131, 218));

            // создаем кисть с градиентом по вертикали
            Brush gradientBrush = new LinearGradientBrush(
                e.Bounds,
                Color.FromArgb(180, 255, 255),
                Color.FromArgb(100, 255, 255),
                LinearGradientMode.Vertical);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // теперь определяем какой элемент сейчас нужно отрисовать
            if ((e.State & DrawItemState.Selected) == 0 || !base.Enabled) // если не активный
            {
                // заполняем прямоугольник выбранным цветом
                e.Graphics.FillRectangle(
                    new SolidBrush(SystemColors.Window),
                    e.Bounds.X - 1,
                    e.Bounds.Y - 1,
                    e.Bounds.Width + 1,
                    e.Bounds.Height + 1);

                // пишем текст
                e.Graphics.DrawString(
                    s,
                    Font,
                    new SolidBrush(SystemColors.WindowText),
                    new RectangleF(0, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height - 5),
                    sf);
            }
            else // если активный
            {
                // создаем путь который повторит контур с закругленными углами
                var gfxPath = new GraphicsPath();

                // определяем координаты элемента в списке
                // т.к. для каждого элемента они разные
                int x = e.Bounds.X; // вспомогательные переменные для отрисовки
                int y = e.Bounds.Y; // вспомогательные переменные для отрисовки

                // также определяем его ширину и высоту
                int rectWidth = e.Bounds.Width - 2; // вспомогательные переменные для отрисовки
                int rectHeight = e.Bounds.Height - 1; // вспомогательные переменные для отрисовки

                #region Рисуем прямоугольник с закругленными углами
                gfxPath.AddLine(x + cornerRadius, y + 1, x + rectWidth - (cornerRadius * 2), y + 1);
                gfxPath.AddArc(x + rectWidth - (cornerRadius * 2), y + 1, cornerRadius * 2, cornerRadius * 2, 270, 90);
                gfxPath.AddLine(x + rectWidth, y + cornerRadius + 1, x + rectWidth, y + rectHeight - (cornerRadius * 2));
                gfxPath.AddArc(x + rectWidth - (cornerRadius * 2), y + rectHeight - (cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 0, 90);
                gfxPath.AddLine(x + rectWidth - (cornerRadius * 2), y + rectHeight, x + cornerRadius, y + rectHeight);
                gfxPath.AddArc(x, y + rectHeight - (cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 90, 90);
                gfxPath.AddLine(x, y + rectHeight - (cornerRadius * 2), x, y + cornerRadius);
                gfxPath.AddArc(x, y + 1, cornerRadius * 2, cornerRadius * 2, 180, 90);
                gfxPath.CloseFigure();
                e.Graphics.DrawPath(new Pen(solidBrush, 1), gfxPath);

                // закрашиваем область
                e.Graphics.FillPath(gradientBrush, gfxPath);
                gfxPath.Dispose();
                #endregion

                // текст будет над областью заливки
                // пишем текст
                e.Graphics.DrawString(
                    s,
                    Font,
                    new SolidBrush(SystemColors.WindowText),
                    new RectangleF(0, e.Bounds.Y + 5, e.Bounds.Width, e.Bounds.Height - 5),
                    sf);
            }
        }

        // после изменения размера
        protected override void OnSizeChanged(EventArgs e)
        {
            // вызываем обновление компонента
            Refresh();
            base.OnSizeChanged(e);
        }

        // во время задания размеров элемента
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            // если это элемент
            if (e.Index > -1)
            {
                //задаем высоту
                e.ItemHeight = 100;
                //и ширину
                e.ItemWidth = Width;
            }
        }

        // отлов скроллирования
        // при движении ползунка мышью - срабатывает WmVScroll, получается позиция ползунка и кидается событие VScrollingChange
        // текущее событие пробрасывается для движения ползунка на текущем контроле
        // при скроллировании мышью - срабатывает WmMouseWheel, получается текущая позиция ползунка, определяется направление
        // скроллирования, вычисляется правильное положение ползунка после сдвига и кидается событие VScrollingChange. 
        // Так приходится поступать, потому что это событие происходит до того, как ползунок изменит своё положение. Поэтому
        // приходится не пробрасывать текущее событие, а генерить своё, с указанием правильного положения ползунка после сдвига,
        // ибо сдвиг может произойти на любое количество шагов (1, 2, 3 и т.д.). Мы же в итоге сдвигается всегда на один шаг.
        protected override void WndProc(ref Message m)
        {
            if (_stopScrollCatching)
            {
                base.WndProc(ref m);
                return;
            }

            int pos;
            var scrollInfo = new ScrollInfo();
            scrollInfo.Size = (uint)Marshal.SizeOf(scrollInfo);

            switch (m.Msg)
            {
                case (int)ScrollMessage.WmVScroll:
                    if (VScrollingChange != null)
                    {
                        if ((m.WParam.ToInt32() & 0xFF) == (int)ScrollBarCommands.ThumbTrack)
                        {
                            scrollInfo.Mask = (uint)ScrollBarInfo.TrackPos;
                            Win32Engine.GetScrollInfo(Handle, (int)ScrollBarType.SbVert, ref scrollInfo);
                            pos = scrollInfo.TrackPos;
                        }
                        else
                        {
                            scrollInfo.Mask = (uint)ScrollBarInfo.Pos;
                            Win32Engine.GetScrollInfo(Handle, (int)ScrollBarType.SbVert, ref scrollInfo);
                            pos = scrollInfo.Pos;
                        }

                        VScrollingChange(this, new CScrollEventArgs(pos));
                    }
                    break;
                case (int)ScrollMessage.WmMouseWheel:
                    scrollInfo.Mask = (uint)ScrollBarInfo.Pos;
                    Win32Engine.GetScrollInfo(Handle, (int)ScrollBarType.SbVert, ref scrollInfo);
                    pos = scrollInfo.Pos;
                    if (m.WParam.ToInt32() < 0)
                    {
                        pos++;
                    }
                    else if (pos > 0)
                    {
                        pos--;
                    }
                    
                    if (VScrollingChange != null)
                    {
                        VScrollingChange(this, new CScrollEventArgs(pos));
                    }

                    _stopScrollCatching = true;
                    pos <<= 16;
                    uint wParam = (uint)ScrollBarCommands.ThumbPosition | (uint)pos;
                    Win32Engine.SendMessage(Handle, (int)ScrollMessage.WmVScroll, new IntPtr(wParam), new IntPtr(0));

                    _stopScrollCatching = false;

                    return;
            }

            base.WndProc(ref m);
        }
    }
}
