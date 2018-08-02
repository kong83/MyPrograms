using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class PassForm : Form
    {
        private readonly DbEngine _dbEngine;
        private bool _isFormClosingByButton;

        public PassForm(DbEngine dbEngine, string infoText)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
            labelInfo.Text = infoText;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            if (textPass.Text.Length < 3)
            {
                MessageBox.Show("Пароль должен состоять минимум из трёх символов", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _dbEngine.PassStr = textPass.Text;
            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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
            toolTip1.Show("Подтвердить ввод пароля", buttonOK, 15, -20);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOK);
            buttonOK.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отказаться от ввода пароля", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
