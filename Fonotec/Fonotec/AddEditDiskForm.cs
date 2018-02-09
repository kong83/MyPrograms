using System;
using System.Windows.Forms;

namespace Fonotec
{
    public partial class AddEditDiskForm : Form
    {
        private readonly MainForm m_MainForm;
        private readonly AddEditState m_CurrentState;
        private readonly FilmotecClass m_FilmotecClass;
        private readonly SettingClass m_SettingClass;
        private DiskInfo m_OldDiskInfo;
        private int m_SelectedIndex;

        public AddEditDiskForm(MainForm mainForm, AddEditState currentState, FilmotecClass filmotecClass, SettingClass settingClass, DiskInfo editDiskInfo, int selectedIndex)
        {
            InitializeComponent();

            m_MainForm = mainForm;
            m_CurrentState = currentState;
            m_FilmotecClass = filmotecClass;
            m_SettingClass = settingClass;            
            m_OldDiskInfo = editDiskInfo;
            m_SelectedIndex = selectedIndex;
        }


        /// <summary>
        /// Кнопка подтверждения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBoxDiskNumber.Text);
            }
            catch
            {
                MessageBox.Show("Непонятный номер диска", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxDiskNumber.Focus();
                return;
            }

            var newDiskInfo = new DiskInfo
            {
                Number = Convert.ToInt32(textBoxDiskNumber.Text)
            };

            if (comboBoxDiskInfo.SelectedIndex == 0)
            {
                newDiskInfo.Info = DiskType.Buy;
            }
            else if (comboBoxDiskInfo.SelectedIndex == 1)
            {
                newDiskInfo.Info = DiskType.OwnR;
            }
            else
            {
                newDiskInfo.Info = DiskType.OwnRW;
            }

            try
            {
                if (m_CurrentState == AddEditState.Add)
                {
                    m_FilmotecClass.AddDisk(newDiskInfo);
                    if (m_SettingClass.IsCloseDiskForm)
                    {
                        Close();
                    }
                    else
                    {
                        textBoxDiskNumber.Text = (Convert.ToInt32(textBoxDiskNumber.Text) + 1).ToString();
                        comboBoxDiskInfo.Focus();
                    }
                }
                else
                {
                    m_FilmotecClass.EditDisk(m_OldDiskInfo, newDiskInfo);
                    if (checkBoxIsChangeFilms.Checked)
                    {
                        FilmInfo[] changeFilms = m_FilmotecClass.GetFilmInfo(m_OldDiskInfo.Number, string.Empty, string.Empty);
                        foreach (FilmInfo fi in changeFilms)
                        {
                            m_FilmotecClass.EditFilm(fi, new FilmInfo
                                                            {
                                                                Info = fi.Info,
                                                                Name = fi.Name,
                                                                Number = newDiskInfo.Number
                                                            });
                        }
                    }
                    m_MainForm.NewDiskNumber = newDiskInfo.Number;
                    Close();
                }

            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка: \r\n\r\n" + ex.Message + "\r\n\r\nСообщите разработчикам", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Кнопка закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            m_MainForm.NewDiskNumber = -1;
            Close();
        }


        /// <summary>
        /// Инициализация полей при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditDiskForm_Load(object sender, EventArgs e)
        {
            if (m_CurrentState == AddEditState.Add)
            {
                Text = "Добавление диска";

                int startNumber = m_SelectedIndex;
                if (m_FilmotecClass.CurrentSortDiskType == SortType.Increase)
                {
                    while (startNumber < m_FilmotecClass.DiskInfo.Length - 1 && m_FilmotecClass.DiskInfo[startNumber + 1].Number - m_FilmotecClass.DiskInfo[startNumber].Number == 1)
                    {
                        startNumber++;
                    }
                    textBoxDiskNumber.Text = (m_FilmotecClass.DiskInfo[startNumber].Number + 1).ToString();
                }                
                else
                {
                    while (startNumber > 0  && m_FilmotecClass.DiskInfo[startNumber - 1].Number - m_FilmotecClass.DiskInfo[startNumber].Number == 1)
                    {
                        startNumber--;
                    }
                    if (startNumber == 0)
                    {
                        textBoxDiskNumber.Text = m_FilmotecClass.DiskInfo.Length > 1 
                            ? (m_FilmotecClass.DiskInfo[1].Number + 1).ToString() 
                            : "1";
                    }
                    else
                    {
                        textBoxDiskNumber.Text = (m_FilmotecClass.DiskInfo[startNumber].Number + 1).ToString();
                    }                   
                }

                comboBoxDiskInfo.SelectedIndex = 1;
            }
            else
            {
                Text = "Редактирование диска";
                textBoxDiskNumber.Text = m_OldDiskInfo.Number.ToString();
                if (m_OldDiskInfo.Info == DiskType.Buy)
                {
                    comboBoxDiskInfo.SelectedIndex = 0;
                }
                else if (m_OldDiskInfo.Info == DiskType.OwnR)
                {
                    comboBoxDiskInfo.SelectedIndex = 1;
                }
                else
                {
                    comboBoxDiskInfo.SelectedIndex = 2;
                }
            }
            SetFont();
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {
            textBoxDiskNumber.Font =
            comboBoxDiskInfo.Font = m_SettingClass.ProgramFont;

            textBoxDiskNumber.ForeColor =
            comboBoxDiskInfo.ForeColor = m_SettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Показ или скрытие checkBox-а для изменения номера диска у фильмов этого диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxDiskNumber_TextChanged(object sender, EventArgs e)
        {
            if (m_CurrentState == AddEditState.Edit && m_OldDiskInfo.Number.ToString() != textBoxDiskNumber.Text)
            {
                checkBoxIsChangeFilms.Visible = true;
            }
            else
            {
                checkBoxIsChangeFilms.Visible = false;
            }
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditDiskForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
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
            if (!buttonOK.Focused && !buttonCancel.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    buttonOK_Click(null, null);
                    return true;
                }

                if (keyData == Keys.Escape)
                {
                    buttonCancel_Click(null, null);
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
