using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class ChangePasswordForm : Form
    {
        private readonly DbEngine _dbEngine;
        private bool _isFormClosingByButton;

        public ChangePasswordForm(DbEngine dbEngine)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
        }

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonOK_Click(null, null);
                return true;
            }

            if (keyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void PassForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
        }

        /// <summary>
        /// Подтвердить смену пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textPass.Text != _dbEngine.PassStr)
            {
                MessageBox.Show("Введён неверный текущий пароль", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textPass.Text = string.Empty;
                textPass.Focus();
                return;
            }

            if (textBoxNewPass.Text != textBoxNewPass2.Text)
            {
                MessageBox.Show("Новый пароль должен быть одинаковым в обоих полях", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPass2.Text = string.Empty;
                textBoxNewPass2.Focus();
                return;
            }

            if (textBoxNewPass.Text.Length < 3)
            {
                MessageBox.Show("Пароль должен состоять из не менее чем трёх символов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPass.Text = string.Empty;
                textBoxNewPass.Focus();
                return;
            }

            if (textPass.Text == textBoxNewPass.Text)
            {
                MessageBox.Show("Новый пароль должен отличаться от старого", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPass.Text = string.Empty;
                textBoxNewPass2.Text = string.Empty;
                textBoxNewPass.Focus();
                return;
            }

            _dbEngine.PassStr = textBoxNewPass.Text;

            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Отменить смену пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
        }

        #region Подсказки
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить изменение пароля", buttonOK, 15, -20);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOK);
            buttonOK.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отказаться от изменения пароля", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void textBoxNewPass_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Пароль должен состоять из не менее чем трёх символов", textBoxNewPass, 15, -20);
        }

        private void textBoxNewPass_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(textBoxNewPass);
        }

        private void textBoxNewPass2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Пароль должен состоять из не менее чем трёх символов", textBoxNewPass2, 15, -20);
        }

        private void textBoxNewPass2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(textBoxNewPass2);
        }
        #endregion       

        private void ChangePasswordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }
    }
}
