using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class GoToForm : Form
    {
        readonly MainForm _mf;
        readonly int _max;

        public GoToForm(MainForm mf, int max)
        {
            InitializeComponent();

            _mf = mf;
            _max = max;
            labelInfo.Text = "от 1 до " + max;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int gt = Convert.ToInt32(textGoTo.Text);
                if (gt > _max || gt < 1)
                {
                    MessageBox.Show("Номер строки должен быть больше 0 и меньше " + (_max + 1), "Неверное значение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textGoTo.Focus();
                    return;
                }
                _mf.GoToLine = gt;
            }
            catch
            {
                MessageBox.Show("Ошибка в записи номера строки", "Ошибка в записи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textGoTo.Focus();
                return;
            }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _mf.GoToLine = 0;
            Close();
        }

        private void textGoTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                button1_Click(null, null);
            }
        }
    }
}
