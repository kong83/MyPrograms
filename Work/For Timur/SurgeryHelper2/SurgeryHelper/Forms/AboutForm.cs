using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace SurgeryHelper.Forms
{
    public partial class AboutForm : Form
    {        
        public AboutForm()
        {
            InitializeComponent();

            alphaBlendTextBox1.BackColor = SystemColors.Control;
            labelCaption.Text = "Программа \"Электронный ординатор\" v. " + Assembly.GetExecutingAssembly().GetName().Version;
        }


        /// <summary>
        /// Выход при нажатии Esc
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();            
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Flat;
        }
    }
}
