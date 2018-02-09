using System;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class EditTimesForm : Form
    {
        private readonly int m_CurrentNumber;
        private readonly DatabaseTools m_DatabaseTools;
        private readonly Exchange.TimesInfo[] m_TimesInfos;
        private readonly SettingClass m_SettingClass;

        public EditTimesForm(int currentNumber, DatabaseTools dayabaseTools, SettingClass settingClass, Exchange.TimesInfo[] timesInfos)
        {
            InitializeComponent();

            m_SettingClass = settingClass;
            m_CurrentNumber = currentNumber;
            m_DatabaseTools = dayabaseTools;
            m_TimesInfos = timesInfos;

            textBoxDateStart.Text = m_TimesInfos[m_CurrentNumber].DateStart.ToString();
            textBoxDateStop.Text = m_TimesInfos[m_CurrentNumber].DateStop.ToString();
        }


        /// <summary>
        /// Проверка на правильность введённых дат
        /// </summary>
        private void ValidateDate()
        {
            DateTime dateStart;
            try
            {
                dateStart = Convert.ToDateTime(textBoxDateStart.Text);
            }
            catch
            {
                textBoxDateStart.Focus();
                throw new Exception("Неверное значение даты начала");
            }

            DateTime dateStop;
            try
            {
                dateStop = Convert.ToDateTime(textBoxDateStop.Text);
            }
            catch
            {
                textBoxDateStop.Focus();
                throw new Exception("Неверное значение даты окончания");
            }

            if (dateStart.CompareTo(dateStop) > 0)
            {
                textBoxDateStart.Focus();
                throw new Exception("Дата начала промежутка должна быть меньше, чем дата окончания промежутка.");
            }

            if (m_CurrentNumber > 0)
            {
                if (dateStart.CompareTo(m_TimesInfos[m_CurrentNumber - 1].DateStop) < 0)
                {
                    textBoxDateStart.Focus();
                    throw new Exception("Дата начала промежутка должна быть больше, чем дата окончания предыдущего промежутка.");
                }
            }

            if (m_CurrentNumber + 1 < m_TimesInfos.Length)
            {
                if (dateStop.CompareTo(m_TimesInfos[m_CurrentNumber + 1].DateStart) > 0)
                {
                    textBoxDateStart.Focus();
                    throw new Exception("Дата окончания промежутка должна быть меньше, чем дата начала следующего промежутка.");
                }
            }  
        }


        /// <summary>
        /// Подтверждение редактирования временных промежутков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateDate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }            

            try
            {
                m_DatabaseTools.EditTimes(m_TimesInfos[m_CurrentNumber].ID, Convert.ToDateTime(textBoxDateStart.Text), Convert.ToDateTime(textBoxDateStop.Text));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
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
        private void EditTimesForm_Load(object sender, EventArgs e)
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
        private void EditTimesForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
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
