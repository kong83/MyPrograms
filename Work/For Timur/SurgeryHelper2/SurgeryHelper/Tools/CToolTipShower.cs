using System;
using System.Drawing;
using System.Windows.Forms;

namespace SurgeryHelper.Tools
{
    public static class CToolTipShower
    {
        private static readonly ToolTip LocalToolTip;
        private static readonly Timer LocalTimer;
        private static string _text;
        private static IWin32Window _window;
        private static Point _point;

        static CToolTipShower()
        {
            LocalToolTip = new ToolTip();
            LocalTimer = new Timer 
            {
                Interval = 500
            };
            LocalTimer.Tick += LocalTimerTick;
        }

        public static void Show(string text, IWin32Window window)
        {
            Show(text, window, 15, -21);
        }

        public static void Show(string text, IWin32Window window, int x, int y)
        {
            LocalTimer.Enabled = true;
            _text = text;
            _window = window;
            _point = new Point(x, y);
        }

        public static void Hide()
        {
            LocalTimer.Enabled = false;
            try
            {
                LocalToolTip.Hide(_window);
            }
            catch { }
        }

        private static void LocalTimerTick(object sender, EventArgs e)
        {
            LocalTimer.Enabled = false;
            try
            {
                LocalToolTip.Show(_text, _window, _point.X, _point.Y);
            }
            catch { }
        }
    }
}
