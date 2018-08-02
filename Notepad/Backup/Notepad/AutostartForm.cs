using System;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Notepad
{
    public partial class AutostartForm : Form
    {
        private const string RegPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public AutostartForm()
        {
            InitializeComponent();
        }

        private void AutostartForm_Load(object sender, EventArgs e)
        {
            buttonRefrsh_Click(sender, e);
        }


        /// <summary>
        /// Обновить список программ в автозагрузке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefrsh_Click(object sender, EventArgs e)
        {
            ProgramList.Rows.Clear();

            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey(RegPath);

            if (regKey == null)
            {
                MessageBox.Show(@"Can't find HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            foreach (string name in regKey.GetValueNames())
            {
                var value = (string)regKey.GetValue(name, string.Empty);
                var param = new[] { name, value };
                ProgramList.Rows.Add(param);
            }

            regKey.Close();
        }


        /// <summary>
        /// Добавить новую программу в автозагрузку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new AddAutostartForm(RegPath).ShowDialog();

            buttonRefrsh_Click(sender, e);
        }


        /// <summary>
        /// Удалить программы из автозагрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите удалить все выделенные программы из автозагрузки?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey(RegPath, true);

            foreach (DataGridViewRow dr in ProgramList.Rows)
            {
                if (dr.Selected)
                {
                    regKey.DeleteValue(dr.Cells[0].Value.ToString());
                }
            }

            regKey.Close();

            buttonRefrsh_Click(sender, e);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Добавить программу в автозагрузку", buttonAdd, -10, -17);
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonAdd);
        }

        private void buttonRemove_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Удалить выделенные программы из автозагрузки", buttonRemove, -10, -17);
        }

        private void buttonRemove_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRemove);
        }

        private void buttonRefrsh_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Обновить список", buttonRefrsh, -10, -17);
        }

        private void buttonRefrsh_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRefrsh);
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Закрыть окно", buttonClose, -10, -17);
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
        }
    }
}
