using System;
using System.Windows.Forms;

using SurgeryHelper.Forms;
using System.Diagnostics;

namespace SurgeryHelper
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Process[] prc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

            if (prc.Length > 1)
            {
                MessageBox.Show("Программа уже запущена. Запуск двух версий программы на одном компьютере запрещён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.GetCurrentProcess().Kill();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
