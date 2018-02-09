using System;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class DeleteTimesForm : Form
    {
        Exchange.TimesInfo m_TimesInfo;
        readonly DatabaseTools m_DatabaseTools;
        private readonly SettingClass m_SettingClass;

        public DeleteTimesForm(Exchange.TimesInfo timesInfo, DatabaseTools dayabaseTools, SettingClass settingClass)
        {
            InitializeComponent();

            m_TimesInfo = timesInfo;
            m_DatabaseTools = dayabaseTools;
            m_SettingClass = settingClass;

            textBoxDateStart.Text = timesInfo.DateStart.ToString();
            textBoxDateStop.Text = timesInfo.DateStop.ToString();
        }


        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Подтверждение удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_DatabaseTools.DeleteTimes(m_TimesInfo.ID);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Работа с кнопочками
        private void Drop_Focus(object sender, EventArgs e)
        {
            textBoxDateStart.Focus();
        }
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Подтвердить удаление", buttonOK, 10, -18);
        }
        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonOK);
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Отменить удаление", buttonClose, 10, -18);
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonClose);
        }
        #endregion

        
        /// <summary>
        /// Загрузка данных при открытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTimesForm_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = m_SettingClass.CurrentInputLanguage;
            textBoxDateStart.Font = textBoxDateStop.Font = m_SettingClass.ProgramFont;
            textBoxDateStart.ForeColor = textBoxDateStop.ForeColor = m_SettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTimesForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            m_SettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
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
            }
            else if (keyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }

            return true;
        }
    }
}
