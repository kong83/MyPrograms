using System;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class EditProjectForm : Form
    {
        private readonly DatabaseTools m_DatabaseTools;
        private Exchange.ProjectInfo m_ProjectInfo;
        private readonly SettingClass m_SettingClass;

        public EditProjectForm(DatabaseTools databaseTools, SettingClass settingClass, Exchange.ProjectInfo projectInfo)
        {
            InitializeComponent();

            m_SettingClass = settingClass;
            m_DatabaseTools = databaseTools;
            m_ProjectInfo = projectInfo;

            textBoxName.Text = m_ProjectInfo.Name;
            textBoxPay.Text = m_ProjectInfo.Pay.ToString();
            textBoxInfo.Text = m_ProjectInfo.Info;
        }

        /// <summary>
        ///  Закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Подтвердить редактирование данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBoxPay.Text);
            }
            catch
            {
                MessageBox.Show("Неверное значение стоимости часа оплаты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPay.Focus();
                return;
            }

            if (m_ProjectInfo.Name != textBoxName.Text || 
                m_ProjectInfo.Info != textBoxInfo.Text ||
                m_ProjectInfo.Pay != Convert.ToInt32(textBoxPay.Text))
            {
                try
                {
                    m_DatabaseTools.EditProject(m_ProjectInfo.ID, textBoxName.Text, Convert.ToInt32(textBoxPay.Text), textBoxInfo.Text);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Close();
            }
        }

        #region Работа с кнопочками
        private void Drop_Focus(object sender, EventArgs e)
        {
            textBoxName.Focus();
        }
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Подтвердить изменения", buttonOK, 10, -18);
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
                toolTip.Show("Отменить изменения", buttonClose, 10, -18);
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
        private void EditProjectForm_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = m_SettingClass.CurrentInputLanguage;
            textBoxName.Font = textBoxPay.Font = textBoxInfo.Font = m_SettingClass.ProgramFont;
            textBoxName.ForeColor = textBoxPay.ForeColor = textBoxInfo.ForeColor = m_SettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditProjectForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
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
            if (keyData == Keys.Enter && !textBoxInfo.Focused)
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
