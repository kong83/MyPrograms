using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using SurgeryHelper.Tools;

namespace SurgeryHelper.MyControls
{
    public sealed class AlphaBlendTextBox : TextBox
    {
        private readonly UPictureBox _myPictureBox;
        private bool _myUpToDate;
        private Bitmap _myBitmap;
        private Bitmap _myAlphaBitmap;
        private bool _myPaintedFirstTime;
        private Color _myBackColor = Color.White;
        private int _myBackAlpha = 10;

        public AlphaBlendTextBox()
        {
            BackColor = _myBackColor;

            SetStyle(ControlStyles.UserPaint, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            _myPictureBox = new UPictureBox();
            Controls.Add(_myPictureBox);
            _myPictureBox.Dock = DockStyle.Fill;
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _myBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height); // (Width,Height);
            _myAlphaBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height); // (Width,Height);
            _myUpToDate = false;
            Invalidate();
        }

        // Some of these should be moved to the WndProc later
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate();
        }


        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            base.OnGiveFeedback(gfbevent);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            Point ptCursor = Cursor.Position;

            Form form = FindForm();
            if (form != null)
            {
                ptCursor = form.PointToClient(ptCursor);
            }

            if (!Bounds.Contains(ptCursor))
            {
                base.OnMouseLeave(e);
            }
        }

        protected override void OnChangeUICues(UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnFontChanged(EventArgs e)
        {
            if (_myPaintedFirstTime)
            {
                SetStyle(ControlStyles.UserPaint, false);
            }

            base.OnFontChanged(e);

            if (_myPaintedFirstTime)
            {
                SetStyle(ControlStyles.UserPaint, true);
            }

            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            _myUpToDate = false;
            Invalidate();
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // need to rewrite as a big switch
            if (m.Msg == Win32Engine.WM_PAINT)
            {
                _myPaintedFirstTime = true;

                if (!_myUpToDate)
                {
                    GetBitmaps();
                }

                _myUpToDate = true;

                if (_myPictureBox.Image != null)
                {
                    _myPictureBox.Image.Dispose();
                }

                _myPictureBox.Image = (Image)_myAlphaBitmap.Clone();
            }
            else if (m.Msg == Win32Engine.WM_HSCROLL || m.Msg == Win32Engine.WM_VSCROLL)
            {
                _myUpToDate = false;
                Invalidate();
            }
            else if (m.Msg == Win32Engine.WM_LBUTTONDOWN || m.Msg == Win32Engine.WM_RBUTTONDOWN || m.Msg == Win32Engine.WM_LBUTTONDBLCLK)
            {
                _myUpToDate = false;
                Invalidate();
            }
            else if (m.Msg == Win32Engine.WM_MOUSEMOVE)
            {
                // shift key or other buttons
                if (m.WParam.ToInt32() != 0)
                {
                    _myUpToDate = false;
                    Invalidate();
                }
            }
        }


        public new BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }

            set
            {
                if (_myPaintedFirstTime)
                {
                    SetStyle(ControlStyles.UserPaint, false);
                }

                base.BorderStyle = value;

                if (_myPaintedFirstTime)
                {
                    SetStyle(ControlStyles.UserPaint, true);
                }

                _myBitmap = null;
                _myAlphaBitmap = null;
                _myUpToDate = false;
                Invalidate();
            }
        }


        public new Color BackColor
        {
            get
            {
                return Color.FromArgb(base.BackColor.R, base.BackColor.G, base.BackColor.B);
            }

            set
            {
                _myBackColor = value;
                base.BackColor = value;
                _myUpToDate = false;
            }
        }


        public override bool Multiline
        {
            get
            {
                return base.Multiline;
            }

            set
            {
                if (_myPaintedFirstTime)
                {
                    SetStyle(ControlStyles.UserPaint, false);
                }

                base.Multiline = value;

                if (_myPaintedFirstTime)
                {
                    SetStyle(ControlStyles.UserPaint, true);
                }

                _myBitmap = null;
                _myAlphaBitmap = null;
                _myUpToDate = false;
                Invalidate();
            }
        }


        public new HorizontalAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }

            set
            {
                if (_myPaintedFirstTime)
                {
                    SetStyle(ControlStyles.UserPaint, false);
                }

                base.TextAlign = value;

                if (_myPaintedFirstTime)
                {
                    SetStyle(ControlStyles.UserPaint, true);
                }

                _myBitmap = null;
                _myAlphaBitmap = null;
                _myUpToDate = false;
                Invalidate();
            }
        }


        private void GetBitmaps()
        {
            if (_myBitmap == null
                || _myAlphaBitmap == null
                || _myBitmap.Width != Width
                || _myBitmap.Height != Height
                || _myAlphaBitmap.Width != Width
                || _myAlphaBitmap.Height != Height)
            {
                _myBitmap = null;
                _myAlphaBitmap = null;
            }

            if (_myBitmap == null)
            {
                _myBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height); // (Width,Height);
                _myUpToDate = false;
            }


            if (!_myUpToDate)
            {
                // Capture the TextBox control window
                SetStyle(ControlStyles.UserPaint, false);

                CaptureWindow(this, ref _myBitmap);

                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.FromArgb(_myBackAlpha, _myBackColor);
            }

            var r2 = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            var tempImageAttr = new ImageAttributes();

            // Found the color map code in the MS Help
            var tempColorMap = new ColorMap[1];
            tempColorMap[0] = new ColorMap
            {
                OldColor = Color.FromArgb(255, _myBackColor),
                NewColor = Color.FromArgb(_myBackAlpha, _myBackColor)
            };

            tempImageAttr.SetRemapTable(tempColorMap);

            if (_myAlphaBitmap != null)
            {
                _myAlphaBitmap.Dispose();
            }

            _myAlphaBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height); // (Width,Height);

            Graphics tempGraphics1 = Graphics.FromImage(_myAlphaBitmap);

            tempGraphics1.DrawImage(_myBitmap, r2, 0, 0, ClientRectangle.Width, ClientRectangle.Height, GraphicsUnit.Pixel, tempImageAttr);

            tempGraphics1.Dispose();

            if (Focused && (SelectionLength == 0))
            {
                Graphics.FromImage(_myAlphaBitmap);
            }
        }


        private sealed class UPictureBox : PictureBox
        {
            public UPictureBox()
            {
                SetStyle(ControlStyles.Selectable, false);
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(ControlStyles.DoubleBuffer, true);

                Cursor = null;
                Enabled = true;
                SizeMode = PictureBoxSizeMode.Normal;
            }

            // UPictureBox
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == Win32Engine.WM_LBUTTONDOWN
                    || m.Msg == Win32Engine.WM_RBUTTONDOWN
                    || m.Msg == Win32Engine.WM_LBUTTONDBLCLK
                    || m.Msg == Win32Engine.WM_MOUSELEAVE
                    || m.Msg == Win32Engine.WM_MOUSEMOVE)
                {
                    // Send the above messages back to the parent control
                    Win32Engine.PostMessage(Parent.Handle, (uint)m.Msg, m.WParam, m.LParam);
                }
                else if (m.Msg == Win32Engine.WM_LBUTTONUP)
                {
                    // ??  for selects and such
                    Parent.Invalidate();
                }


                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// Capture the contents of a window or control
        /// </summary>
        /// <param name="control">Control</param>
        /// <param name="bitmap">Picture</param>
        private static void CaptureWindow(Control control, ref Bitmap bitmap)
        {
            using (Graphics g2 = Graphics.FromImage(bitmap))
            {
                const int meint = (int)(Win32Engine.PRF_CLIENT | Win32Engine.PRF_ERASEBKGND);
                var meptr = new IntPtr(meint);

                IntPtr hdc = g2.GetHdc();
                Win32Engine.SendMessage(control.Handle, Win32Engine.WM_PRINT, hdc, meptr);

                g2.ReleaseHdc(hdc);
            }
        }


        [Category("Appearance"),
         Description("The alpha value used to blend the control's background. Valid values are 0 through 255."),
         Browsable(true),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BackAlpha
        {
            get
            {
                return _myBackAlpha;
            }

            set
            {
                int v = value;
                if (v > 255)
                {
                    v = 255;
                }

                _myBackAlpha = v;
                _myUpToDate = false;
                Invalidate();
            }
        }
    }
}
