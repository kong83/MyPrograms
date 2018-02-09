using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class SettingsForm : Form
    {
        MainForm mainForm;

        public SettingsForm(MainForm mf)
        {
            mainForm = mf;

            InitializeComponent();

            textBoxSleep.Text = (mainForm.sleepTime / 1000).ToString();
            checkBoxUseRunning.Checked = mainForm.useRunning;
        }


        /// <summary>
        /// Кнопка "OK"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int n = 0;
            try
            {
                n = Convert.ToInt32(textBoxSleep.Text);
                if (n < 1 || n > 10)
                {
                    MessageBox.Show("Время подсветки может принимать значения от 1 до 10", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                mainForm.sleepTime = n * 1000;
                mainForm.useRunning = checkBoxUseRunning.Checked;
                SaveSettingsClass settingsClass = new SaveSettingsClass();
                Dictionary<string, string> settings = new Dictionary<string, string>();
                settings["sleepTime"] = "" + mainForm.sleepTime;
                settings["useRunning"] = "" + mainForm.useRunning;
                settingsClass.SaveParameters(settings);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неправильная запись времени подсветки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
        }


        /// <summary>
        /// Кнопка "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                Help.ShowHelp(new Control(), mainForm.helpPath, HelpNavigator.Topic, "settingDelay.html");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
