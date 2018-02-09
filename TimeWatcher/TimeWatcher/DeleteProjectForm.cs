using System;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class DeleteProjectForm : Form
    {
        private readonly DatabaseTools m_DatabaseTools;
        private Exchange.ProjectInfo m_ProjectInfo;
        private readonly SettingClass m_SettingClass;

        public DeleteProjectForm(DatabaseTools databaseTools, Exchange.ProjectInfo projectInfo, SettingClass settingClass)
        {
            InitializeComponent();

            m_DatabaseTools = databaseTools;
            m_ProjectInfo = projectInfo;
            m_SettingClass = settingClass;

            textBoxName.Text = m_ProjectInfo.Name;
        }

        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Удалить проект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_DatabaseTools.DeleteProject(m_ProjectInfo.ID);
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
            textBoxName.Focus();
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
        private void DeleteProjectForm_Load(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = m_SettingClass.CurrentInputLanguage;
            textBoxName.Font = m_SettingClass.ProgramFont;
            textBoxName.ForeColor = m_SettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProjectForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
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
