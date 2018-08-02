using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();

            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
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


        /// <summary>
        /// Обработка изменения раскладки клавиатуры
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PassForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
        }


        /// <summary>
        /// Подтвердить смену пароля
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textPass.Text != CPassHelper.PassStr)
            {
                MessageBox.ShowDialog("Введён неверный текущий пароль", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textPass.Text = string.Empty;
                textPass.Focus();
                return;
            }

            if (textBoxNewPass.Text != textBoxNewPass2.Text)
            {
                MessageBox.ShowDialog("Новый пароль должен быть одинаковым в обоих полях", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPass2.Text = string.Empty;
                textBoxNewPass2.Focus();
                return;
            }

            if (textBoxNewPass.Text.Length < 3)
            {
                MessageBox.ShowDialog("Пароль должен состоять из не менее чем трёх символов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPass.Text = string.Empty;
                textBoxNewPass.Focus();
                return;
            }

            if (textPass.Text == textBoxNewPass.Text)
            {
                MessageBox.ShowDialog("Новый пароль должен отличаться от старого", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPass.Text = string.Empty;
                textBoxNewPass2.Text = string.Empty;
                textBoxNewPass.Focus();
                return;
            }

            CPassHelper.PassStr = textBoxNewPass.Text;

            Close();
        }


        /// <summary>
        /// Отменить смену пароля
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        #region Подсказки
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить изменение пароля", buttonOK);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOK.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отказаться от изменения пароля", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void textBoxNewPass_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Пароль должен состоять из не менее чем трёх символов", textBoxNewPass);
        }

        private void textBoxNewPass_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void textBoxNewPass2_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Пароль должен состоять из не менее чем трёх символов", textBoxNewPass2);
        }

        private void textBoxNewPass2_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
        }
        #endregion

        private void textBox_EnterFocus(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            labelLang.Top = textBox.Top + 4;
        }
    }
}
