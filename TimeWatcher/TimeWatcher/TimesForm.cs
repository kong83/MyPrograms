using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class TimesForm : Form
    {
        private readonly DatabaseTools m_DatabaseTools;
        private readonly SettingClass m_SettingClass;
        private bool m_LockChangeSettings;
        private Exchange.ProjectInfo m_ProjectInfo;
        private Exchange.TimesInfo[] m_TimesInfos;

        public TimesForm(DatabaseTools databaseTools, SettingClass settingClass, Exchange.ProjectInfo projectInfo)
        {
            m_LockChangeSettings = true;
            InitializeComponent();

            m_DatabaseTools = databaseTools;
            m_ProjectInfo = projectInfo;
            m_SettingClass = settingClass;

            labelCaption.Text = "Выбранный проект:\r\n" + projectInfo.Name;
        }


        /// <summary>
        /// Отобразить рабочие промежутки
        /// </summary>
        private void ShowTimes()
        {
            int selectNumber = TimesList.CurrentRow != null ? TimesList.CurrentRow.Index : 0;

            m_TimesInfos = m_DatabaseTools.GetTimesRows(m_ProjectInfo.ID);
            TimesList.Rows.Clear();
            labelInfo.Text = "";
            foreach (Exchange.TimesInfo timeInfo in m_TimesInfos)
            {
                var param = new object[]
                {
                    timeInfo.DateStart.ToString(),
                    timeInfo.DateStop != null ? timeInfo.DateStop.ToString() : ""
                };
                TimesList.Rows.Add(param);
            }
            TimesList.Focus();

            if (TimesList.Rows.Count > 0)
            {
                if (selectNumber < TimesList.Rows.Count)
                {
                    TimesList.Rows[selectNumber].Cells[0].Selected = true;
                }
                else
                {
                    TimesList.Rows[TimesList.Rows.Count - 1].Cells[0].Selected = true;
                }
            }

            Color col = Color.FromArgb(255, 240, 240, 240);
            for (int i = 0; i < TimesList.Rows.Count; i += 2)
            {
                TimesList.Rows[i].DefaultCellStyle.BackColor = col;
            }

            if (m_TimesInfos.Length > 0 && m_TimesInfos[m_TimesInfos.Length - 1].DateStop == null)
            {
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                timerWork.Enabled = true;
            }
            else
            {
                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
                timerWork.Enabled = false;
            }

            var timesTools = new TimeTools();

            labelInfo.Text = "Общее время работы над проектом: " + timesTools.GetTimeFromSecond(GetAllSeconds(null));
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {
            TimesList.Font = m_SettingClass.ProgramFont;
            TimesList.ForeColor = m_SettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Отображение списка времён при открытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesForm_Load(object sender, EventArgs e)
        {
            ShowTimes();

            if (m_SettingClass.TimesFormLocation.X > 0 && m_SettingClass.TimesFormLocation.Y > 0)
            {
                Location = m_SettingClass.TimesFormLocation;
            }
            Size = m_SettingClass.TimesFormSize;

            var arr = m_SettingClass.TimesListColumnsWidth;
            for (int i = 0; i < arr.Length; i++)
            {
                TimesList.Columns[i].Width = arr[i];
            }

            InputLanguage.CurrentInputLanguage = m_SettingClass.CurrentInputLanguage;

            SetFont();

            m_LockChangeSettings = false;
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
        /// Кнопка начала работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            m_DatabaseTools.AddStartTimes(m_ProjectInfo.ID, DateTime.Now);

            ShowTimes();
        }


        /// <summary>
        /// Кнопка окончания работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            m_DatabaseTools.AddStopTimes(m_TimesInfos[m_TimesInfos.Length - 1].ID, DateTime.Now);

            ShowTimes();
        }


        /// <summary>
        /// Получить суммарное количество секунд, потраченное на проект
        /// </summary>
        /// <param name="rowIndex">Выделенный номер в списке времён</param>
        /// <returns></returns>        
        private long GetAllSeconds(int? rowIndex)
        {
            var timesTools = new TimeTools();
            long allSec = 0;

            if (rowIndex == null)
            {
                foreach (Exchange.TimesInfo timeInfo in m_TimesInfos)
                {
                    DateTime stopTime = timeInfo.DateStop.HasValue ? timeInfo.DateStop.Value : DateTime.Now;
                    allSec += timesTools.GetSeconds(timeInfo.DateStart, stopTime);
                }
            }
            else
            {
                DateTime stopTime = m_TimesInfos[rowIndex.Value].DateStop.HasValue ? m_TimesInfos[rowIndex.Value].DateStop.Value : DateTime.Now;
                allSec += timesTools.GetSeconds(m_TimesInfos[rowIndex.Value].DateStart, stopTime);
            }
            return allSec;
        }

        /// <summary>
        /// Изменение времени работы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerWork_Tick(object sender, EventArgs e)
        {
            var timesTools = new TimeTools();

            labelInfo.Text = "Общее время работы над проектом: " + timesTools.GetTimeFromSecond(GetAllSeconds(null));
        }


        /// <summary>
        /// Показать заработанные деньги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMoney_Click(object sender, EventArgs e)
        {
            double payForSeconds = (double)m_ProjectInfo.Pay / 3600;
            MessageBox.Show(
                string.Format("На данный момент Вы заработали на проекте {0} руб.",
                    (GetAllSeconds(null) * payForSeconds).ToString("F2")),
                "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (TimesList.CurrentRow != null && m_TimesInfos[TimesList.CurrentRow.Index].DateStop != null)
            {
                var editTimes = new EditTimesForm(TimesList.CurrentRow.Index, m_DatabaseTools, m_SettingClass, m_TimesInfos);
                editTimes.ShowDialog();

                ShowTimes();
            }
        }


        /// <summary>
        /// Удаление данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (TimesList.CurrentRow != null && m_TimesInfos[TimesList.CurrentRow.Index].DateStop != null)
            {
                var deleteTimes = new DeleteTimesForm(m_TimesInfos[TimesList.CurrentRow.Index], m_DatabaseTools, m_SettingClass);
                deleteTimes.ShowDialog();

                ShowTimes();
            }
        }


        #region Работа с кнопочками
        private void Drop_Focus(object sender, EventArgs e)
        {
            TimesList.Focus();
        }
        private void buttonStart_MouseEnter(object sender, EventArgs e)
        {
            buttonStart.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Начать работу", buttonStart, 10, -18);
        }
        private void buttonStart_MouseLeave(object sender, EventArgs e)
        {
            buttonStart.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonStart);
        }
        private void buttonStop_MouseEnter(object sender, EventArgs e)
        {
            buttonStop.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Окончить работу", buttonStop, 10, -18);
        }
        private void buttonStop_MouseLeave(object sender, EventArgs e)
        {
            buttonStop.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonStop);
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Закрыть окно", buttonClose, 10, -18);
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonClose);
        }
        private void buttonMoney_MouseEnter(object sender, EventArgs e)
        {
            buttonMoney.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show(string.Format("Показать заработанные деньги ({0} руб/час)", m_ProjectInfo.Pay), buttonMoney, 10, -18);
        }
        private void buttonMoney_MouseLeave(object sender, EventArgs e)
        {
            buttonMoney.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonMoney);
        }
        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            buttonEdit.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Редактировать данные", buttonEdit, 10, -18);
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
                toolTip.Show("Удалить данные", buttonDelete, 10, -18);
        }
        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonDelete.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDelete);
        }
        #endregion


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            m_SettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }


        /// <summary>
        /// Изменение размера формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesForm_SizeChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            m_SettingClass.TimesFormSize = Size;
        }


        /// <summary>
        /// Изменение местоположения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesForm_LocationChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (Location.X > 0 && Location.Y > 0)
            {
                m_SettingClass.TimesFormLocation = Location;
            }
        }


        /// <summary>
        /// Изменение ширины колонок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            var arr = new int[TimesList.Columns.Count - 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = TimesList.Columns[i].Width;
            }
            m_SettingClass.TimesListColumnsWidth = arr;
        }


        /// <summary>
        /// Двойное нажатие на ячейке для показа стоимости работы 
        /// для выдыленного промежутка времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TimesList.CurrentRow != null)
            {
                double payForSeconds = (double)m_ProjectInfo.Pay / 3600;
                MessageBox.Show(
                    string.Format("За выбранный промежуток времени Вы заработали на проекте {0} руб.",
                        (GetAllSeconds(TimesList.CurrentRow.Index) * payForSeconds).ToString("F2")),
                    "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Отлов нажатия кнопок на таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimesList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buttonEdit_Click(null, null);
            }
            else if (e.KeyData == Keys.Delete)
            {
                buttonDelete_Click(null, null);
            }
            else if (e.KeyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                    e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
