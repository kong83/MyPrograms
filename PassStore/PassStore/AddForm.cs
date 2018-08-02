using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PassStore
{
    public partial class AddForm : Form
    {
        /// <summary>
        /// Указатель на то, какой кнопкой закрыли окно
        /// </summary>
        bool isOk = false;

        /// <summary>
        /// Указатель на главную форму
        /// </summary>
        MainForm mainForm;

        /// <summary>
        /// Форма для добавления новой записи
        /// </summary>
        /// <param name="mf"></param>
        public AddForm(MainForm mf)
        {
            InitializeComponent();

            mainForm = mf;
        }

        /// <summary>
        /// Обработчик закрытия окна. 
        /// Если окно закрылось кнопкой OK - то отправляем данные в MainForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOk)
            {
                mainForm.OneRow = new string[3] { textParth.Text, textLogin.Text, textPassword.Text };
            }
            else
            {
                mainForm.OneRow = new string[3];
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            isOk = true;
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            isOk = false;
            this.Close();
        }

        #region Подсказки
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить добавление новой записи", buttonOK, 15, -17);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }
        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOK);
            buttonOK.FlatStyle = FlatStyle.Flat;
        }
        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отменить добавление новой записи", buttonClose, 15, -17);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }
        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
        #endregion

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
            else if (keyData == Keys.Escape)
            {
                Cancel_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
