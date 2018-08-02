using System;
using System.Runtime.InteropServices;

namespace SurgeryHelper.Tools
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ScrollInfo
    {
        public uint Size;
        public uint Mask;
        public int Min;
        public int Max;
        public uint Page;
        public int Pos;
        public int TrackPos;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Ramp
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Blue;
    }

    public static class Win32Engine
    {
        #region Константы
        public const int WM_MOUSEMOVE = 0x0200;

        public const int WM_LBUTTONDOWN = 0x0201;

        public const int WM_LBUTTONUP = 0x0202;

        public const int WM_RBUTTONDOWN = 0x0204;

        public const int WM_LBUTTONDBLCLK = 0x0203;

        public const int WM_MOUSELEAVE = 0x02A3;

        public const int WM_PAINT = 0x000F;

        public const int WM_PRINT = 0x0317;

        public const int WM_HSCROLL = 0x0114;

        public const int WM_VSCROLL = 0x0115;

        public const long PRF_CLIENT = 0x00000004L;

        public const long PRF_ERASEBKGND = 0x00000008L;

        public const int SB_THUMBPOSITION = 4;
        #endregion

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public extern static int GetScrollInfo(IntPtr hWnd, int fnBar, ref ScrollInfo lpsi);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceGammaRamp(IntPtr hDC, ref Ramp lpRamp);

        [DllImport("gdi32.dll")]
        public static extern int SetDeviceGammaRamp(IntPtr hDC, ref Ramp lpRamp);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32", CharSet = CharSet.Auto)]
        public static extern bool ExtFloodFill(IntPtr hDC, int x, int y, uint сolorRefColor, uint nFillType);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
