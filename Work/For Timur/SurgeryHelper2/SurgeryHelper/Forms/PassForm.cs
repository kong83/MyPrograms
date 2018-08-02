using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class PassForm : Form
    {
        private bool _isFormClosingByButton;

        public PassForm(string infoText)
        {
            InitializeComponent();

            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
            labelInfo.Text = infoText;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку OK
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void OK_Click(object sender, EventArgs e)
        {
            if (textPass.Text.Length < 3)
            {                
                MessageBox.ShowDialog("Пароль должен состоять минимум из трёх символов", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            CPassHelper.PassStr = textPass.Text;
            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Cancel
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OK_Click(null, null);
                return true;
            }

            if (keyData == Keys.Escape)
            {
                Cancel_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }


        private void PassForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
        }


        private void PassForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                Environment.Exit(0);
            }
        }


        #region Подсказки
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить ввод пароля", buttonOK);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonOK.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отказаться от ввода пароля", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
