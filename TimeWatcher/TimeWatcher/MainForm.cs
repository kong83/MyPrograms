using System;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class MainForm : Form
    {
        private DatabaseTools m_DatabaseTools;
        private Exchange.ProjectInfo[] m_ProjectInfos;

        private SettingClass m_SettingClass;
        private bool m_LockChangeSettings;        

        public MainForm()
        {
            InitializeComponent();

            m_SettingClass = new SettingClass();
            m_DatabaseTools = new DatabaseTools();
            m_LockChangeSettings = true;
        }
        

        /// <summary>
        /// Открытие окна с временными промежутками при двойном клике на списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxProjectList_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxProjectList.SelectedIndex < 0)
            {
                return;
            }

            var timesForm = new TimesForm(m_DatabaseTools, m_SettingClass, GetProjecInfoByName());
            Visible = false;
            ShowInTaskbar = false;
            timesForm.ShowDialog();

            buttonShowTimesForm.Enabled = false;
            listBoxProjectList.Focus();
            buttonShowTimesForm.FlatStyle = FlatStyle.Flat;
            buttonShowTimesForm.Enabled = true;

            Visible = true;
            ShowInTaskbar = true;
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
                listBoxProjectList_DoubleClick(null, null);
            }
            else if (keyData == Keys.Insert)
            {
                buttonAdd_Click(null, null);
            }
            else if (keyData == Keys.Delete)
            {
                buttonDelete_Click(null, null);
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


        /// <summary>
        /// Нажатие на кнопку для показа времён по проекту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowTimesForm_Click(object sender, EventArgs e)
        {
            if (listBoxProjectList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Нет выделенных проектов", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            listBoxProjectList_DoubleClick(null, null);
        }


        /// <summary>
        ///  Кнопка выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Кнопка добавления нового проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var addProject = new AddProjectForm(m_DatabaseTools, m_SettingClass);
            addProject.ShowDialog();
            ShowProjects();
        }


        /// <summary>
        /// Кнопка редактирования нового проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxProjectList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Нет выделенных проектов", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var editProject = new EditProjectForm(m_DatabaseTools, m_SettingClass, GetProjecInfoByName());
            editProject.ShowDialog();
            ShowProjects();
        }


        /// <summary>
        /// Кнопка удаления проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxProjectList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Нет выделенных проектов", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var deleteProject = new DeleteProjectForm(m_DatabaseTools, GetProjecInfoByName(), m_SettingClass);
            deleteProject.ShowDialog();
            ShowProjects();
        }


        /// <summary>
        /// Отобразить на экране список названий проектов
        /// </summary>
        private void ShowProjects()
        {
            int selectNumber = Math.Max(listBoxProjectList.SelectedIndex, 0);

            m_ProjectInfos = m_DatabaseTools.GetProjectRows();
            listBoxProjectList.Items.Clear();
            labelInfo.Text = "";
            foreach (Exchange.ProjectInfo projectInfo in m_ProjectInfos)
            {
                listBoxProjectList.Items.Add(projectInfo.Name);
            }
            listBoxProjectList.Focus();

            if (listBoxProjectList.Items.Count > 0)
            {
                if (selectNumber < listBoxProjectList.Items.Count)
                {
                    listBoxProjectList.SelectedIndex = selectNumber;
                }
                else
                {
                    listBoxProjectList.SelectedIndex = listBoxProjectList.Items.Count - 1;
                }
            }
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {
            listBoxProjectList.Font = m_SettingClass.ProgramFont;
            listBoxProjectList.ForeColor = m_SettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Загрузка данных при открытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowProjects();
                                   
            Location = m_SettingClass.MainFormLocation;
            Size = m_SettingClass.MainFormSize;

            InputLanguage.CurrentInputLanguage = m_SettingClass.CurrentInputLanguage;

            SetFont();

            m_LockChangeSettings = false;
        }


        /// <summary>
        /// Показать информацию о проекте при смене выделенного проекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var project = GetProjecInfoByName();

                labelInfo.Text = "Стоимость часа работы: " + project.Pay + ".\r\n" + project.Info;
            }
            catch (Exception ex)
            {
                labelInfo.Text = ex.Message;
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Получить информацию о проекте по указанному имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Exchange.ProjectInfo GetProjecInfoByName(string name)
        {
            foreach (Exchange.ProjectInfo projectInfo in m_ProjectInfos)
            {
                if (projectInfo.Name == name)
                {
                    return projectInfo;
                }
            }
            throw new Exception("Не найдена информация для " + name);
        }

        /// <summary>
        /// Получить информацию о текущем проекте
        /// </summary>
        /// <returns></returns>
        private Exchange.ProjectInfo GetProjecInfoByName()
        {
            return GetProjecInfoByName(listBoxProjectList.Text);
        }


        #region Работа с кнопочками
        private void Drop_Focus(object sender, EventArgs e)
        {
            listBoxProjectList.Focus();
        }
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            buttonAdd.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Добавить новый проект", buttonAdd, 10, -18);
        }
        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            buttonAdd.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonAdd);
        }
        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            buttonEdit.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Редактировать проект", buttonEdit, 10, -18);
        }
        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            buttonEdit.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonEdit);
        }
        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            buttonDelete.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Удалить проект", buttonDelete, 10, -18);
        }
        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonDelete.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDelete);
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Закрыть программу", buttonClose, 10, -18);
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonClose);
        }
        private void buttonSettings_MouseEnter(object sender, EventArgs e)
        {
            buttonSettings.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Настройки программы", buttonSettings, 10, -18);
        }
        private void buttonSettings_MouseLeave(object sender, EventArgs e)
        {
            buttonSettings.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonSettings);
        }
        private void labelInfo_MouseEnter(object sender, EventArgs e)
        {
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show(labelInfo.Text, labelInfo, -10, 50);
        }
        private void labelInfo_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(labelInfo);
        }
        private void buttonShowTimesForm_MouseEnter(object sender, EventArgs e)
        {
            buttonShowTimesForm.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Показать распределение времени для выбранного проекта", buttonShowTimesForm, 10, -18);
        }
        private void buttonShowTimesForm_MouseLeave(object sender, EventArgs e)
        {
            buttonShowTimesForm.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonShowTimesForm);
        }
        #endregion


        /// <summary>
        /// Открыть окно с настройками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            var settingForm = new SettingForm(m_SettingClass);
            settingForm.ShowDialog();

            SetFont();
        }


        /// <summary>
        /// Сохранение размера формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            m_SettingClass.MainFormSize = Size;
        }


        /// <summary>
        /// Сохранение позиции формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            m_SettingClass.MainFormLocation = Location;
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            m_SettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }
    }
}
