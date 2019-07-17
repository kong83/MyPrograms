using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PreventLocking
{
    public partial class MainForm : Form
    {
        private readonly SymantecCloser _symantecCloser;

        public MainForm()
        {
            InitializeComponent();

            _symantecCloser = new SymantecCloser();
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);

            try
            {
                AddToAutoStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"App cannot be added to autoraun because an error: {ex.Message}. App continue to work as usual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            timer.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        private void buttonExcludeFromAutostart_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFromAutostart();

                MessageBox.Show("App was removed from autorun. It will be added to autorun after next start.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("App cannot be removed from autoraun because an error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        const string AutostartRegPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        private void AddToAutoStart()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutostartRegPath, true))
            {
                key.SetValue("PreventLockingApp", Application.ExecutablePath);
            }
        }

        private void RemoveFromAutostart()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutostartRegPath, true))
            {
                key.DeleteValue("PreventLockingApp", false);
            }
        }

        /// <summary>
        /// Check if symantek window is appeared and press "Allow the file" button on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            _symantecCloser.FindWidowAndAllowFile();
        }
    }
}
