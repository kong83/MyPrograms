using System;
using System.Windows.Forms;

namespace Fonotec
{
    public partial class AddEditFilmForm : Form
    {
        private readonly MainForm mMainForm;
        private readonly AddEditState mCurrentState;
        private readonly FilmotecClass mFilmotecClass;
        private readonly SettingClass mSettingClass;
        private FilmInfo mOldFilmInfo;

        public AddEditFilmForm(MainForm mainForm, AddEditState currentState, FilmotecClass filmotecClass, SettingClass settingClass, FilmInfo editFilmInfo)
        {
            InitializeComponent();

            this.mMainForm = mainForm;
            this.mCurrentState = currentState;
            this.mFilmotecClass = filmotecClass;
            this.mSettingClass = settingClass;
            this.mOldFilmInfo = editFilmInfo;
        }


        /// <summary>
        /// Кнопка закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
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

            GenreType genreType = Helper.StringToGenre(comboBoxFilterGenre.Text);            

            var newFilmInfo = new FilmInfo
            {
                Number = Convert.ToInt32(textBoxDiskNumber.Text),
                Name = textBoxFilmName.Text,
                Info = textBoxFilmInfo.Text,
                Genre = genreType
            };

            try
            {
                if (this.mCurrentState == AddEditState.Add)
                {
                    this.mFilmotecClass.AddFilm(newFilmInfo);
                    if (this.mSettingClass.IsCloseFilmForm)
                    {
                        Close();
                    }
                    else
                    {
                        if (checkBoxClearFields.Checked)
                        {
                            textBoxFilmInfo.Text = string.Empty;
                            textBoxFilmName.Text = string.Empty;
                        }

                        textBoxFilmName.Focus();
                        textBoxFilmName.SelectionStart = 0;
                        textBoxFilmName.SelectionLength = textBoxFilmName.Text.Length;
                    }
                }
                else
                {
                    this.mFilmotecClass.EditFilm(this.mOldFilmInfo, newFilmInfo);
                    this.mMainForm.NewFilmName = newFilmInfo.Name;
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
        /// Инициализация полей при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditFilmForm_Load(object sender, EventArgs e)
        {
            Text = this.mCurrentState == AddEditState.Add ? "Добавление фильма" : "Редактирование фильма";
            if (this.mOldFilmInfo.Number != 0)
            {
                textBoxDiskNumber.Text = this.mOldFilmInfo.Number.ToString();
                textBoxDiskNumber.TabIndex = 20;
            }

            textBoxFilmName.Text = this.mOldFilmInfo.Name;
            textBoxFilmInfo.Text = this.mOldFilmInfo.Info;
            comboBoxFilterGenre.Text = Helper.GenreToString(this.mOldFilmInfo.Genre);
            checkBoxClearFields.Checked = this.mSettingClass.IsFilmFieldsClear;

            if (this.mCurrentState == AddEditState.Add && this.mSettingClass.IsCloseFilmForm == false)
            {
                checkBoxClearFields.Visible = true;
            }
            else
            {
                checkBoxClearFields.Visible = false;
            }

            SetFont();
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {            
            textBoxDiskNumber.Font = 
            textBoxFilmName.Font = 
            textBoxFilmInfo.Font = this.mSettingClass.ProgramFont;

            textBoxDiskNumber.ForeColor = 
            textBoxFilmName.ForeColor = 
            textBoxFilmInfo.ForeColor = this.mSettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Сохранения режима очистки полей при добавлении фильма без закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxClearFields_CheckedChanged(object sender, EventArgs e)
        {
            this.mSettingClass.IsFilmFieldsClear = checkBoxClearFields.Checked;
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditFilmForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            this.mSettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (!buttonOK.Focused && !buttonCancel.Focused && !textBoxFilmInfo.Focused)
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
