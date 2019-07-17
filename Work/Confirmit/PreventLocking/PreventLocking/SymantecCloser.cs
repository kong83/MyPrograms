using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PreventLocking
{
    public class SymantecCloser
    {
        public static class WinAPI
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetWindow(IntPtr HWnd, GetWindow_Cmd cmd);
            /*
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, [Out] StringBuilder lParam);
            */
            [DllImport("user32.dll")]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int GetWindowTextLength(IntPtr hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int SetForegroundWindow(IntPtr hWnd);

            public const int WM_SETTEXT = 0xC;
            public const int WM_LBUTTONDOWN = 0x0201;
            public const int WM_LBUTTONUP = 0x0202;
            public const int WM_LBUTTONDBLCLK = 0x0203;
            public const int BM_CLICK = 0x00F5;

            public enum GetWindow_Cmd : uint
            {
                GW_HWNDFIRST = 0,
                GW_HWNDLAST = 1,
                GW_HWNDNEXT = 2,
                GW_HWNDPREV = 3,
                GW_OWNER = 4,
                GW_CHILD = 5,
                GW_ENABLEDPOPUP = 6,
                WM_GETTEXT = 0x000D
            }
        }

        public void FindWidowAndAllowFile()
        {
            Process process = Process.GetProcesses().FirstOrDefault(x => x.ProcessName == "SavUI");

            if (process == null)
            {
                return;
            }

            try
            {
                IntPtr hWnd = process.MainWindowHandle;

                IntPtr childHandle = WinAPI.GetWindow(hWnd, WinAPI.GetWindow_Cmd.GW_CHILD);

                StringBuilder buttonCaption;
                while (childHandle != IntPtr.Zero)
                {
                    int textLength = WinAPI.GetWindowTextLength(childHandle);

                    buttonCaption = new StringBuilder(textLength);
                    WinAPI.GetWindowText(childHandle, buttonCaption, textLength);
                    if (buttonCaption.ToString() == "   Allow this fil")
                    {
                        WinAPI.SetForegroundWindow(hWnd);
                        WinAPI.PostMessage(childHandle, WinAPI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);

                        Thread.Sleep(200);
                        CloseConfirmationWindow(hWnd);
                        break;
                    }

                    childHandle = WinAPI.GetWindow(childHandle, WinAPI.GetWindow_Cmd.GW_HWNDNEXT);
                }                
            }
            catch { }
        }

        private void CloseConfirmationWindow(IntPtr hWnd)
        {
            IntPtr childHandle = WinAPI.GetWindow(hWnd, WinAPI.GetWindow_Cmd.GW_HWNDFIRST);

            StringBuilder buttonCaption;
            int textLength;

            while (childHandle != IntPtr.Zero)
            {
                if (hWnd == WinAPI.GetWindow(childHandle, WinAPI.GetWindow_Cmd.GW_OWNER))
                {
                    textLength = WinAPI.GetWindowTextLength(childHandle);

                    buttonCaption = new StringBuilder(textLength);
                    WinAPI.GetWindowText(childHandle, buttonCaption, textLength);

                    if (buttonCaption.ToString() == "Symantec Endpoint Protectio")
                    {
                        break;
                    }
                }

                childHandle = WinAPI.GetWindow(childHandle, WinAPI.GetWindow_Cmd.GW_HWNDNEXT);
            }

            childHandle = WinAPI.GetWindow(childHandle, WinAPI.GetWindow_Cmd.GW_CHILD);

            while (childHandle != IntPtr.Zero)
            {
                textLength = WinAPI.GetWindowTextLength(childHandle);

                buttonCaption = new StringBuilder(textLength);
                WinAPI.GetWindowText(childHandle, buttonCaption, textLength);
                if (buttonCaption.ToString() == "O")
                {
                    WinAPI.PostMessage(childHandle, WinAPI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);

                    break;
                }

                childHandle = WinAPI.GetWindow(childHandle, WinAPI.GetWindow_Cmd.GW_HWNDNEXT);
            }
        }
    }
}
