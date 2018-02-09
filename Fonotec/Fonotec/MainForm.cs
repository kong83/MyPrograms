using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Fonotec
{
    public enum AddEditState
    {
        /// <summary>
        /// Добавить новый фильм
        /// </summary>
        Add,

        /// <summary>
        /// Редактировать данные по выбранному фильму
        /// </summary>
        Edit
    }

    public partial class MainForm : Form
    {
        private FilmotecClass mFilmotecClass;
        private SettingClass mSettingClass;
        private FindForm mFindForm;

        private DiskInfo[] mDiskInfos;
        private FilmInfo[] mFilmInfos;

        private bool mLockChangeInfo;

        private bool mLockChangeSettings = true;


        /// <summary>
        /// Выделение указанных диска и фильма
        /// </summary>
        /// <param name="diskNumber">Номер диска</param>
        /// <param name="filmName">Название фильма</param>
        public void SelectDiskAndFilm(string diskNumber, string filmName)
        {
            bool isFound = false;
            for (int i = 0; i < listBoxDiskNumber.Items.Count; i++)
            {
                if (listBoxDiskNumber.Items[i].ToString() == diskNumber)
                {
                    listBoxDiskNumber.SelectedIndex = i;
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                MessageBox.Show("Диск номер \"" + diskNumber + "\" не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            isFound = false;
            for (int i = 0; i < listBoxFilmName.Items.Count; i++)
            {
                if (listBoxFilmName.Items[i].ToString() == filmName)
                {
                    listBoxFilmName.SelectedIndex = i;
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                MessageBox.Show("Фильм \"" + filmName + "\" не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }


        public MainForm()
        {
            InitializeComponent();
            comboBoxFilterGenre.SelectedIndex = 0;
        }


        /// <summary>
        /// Инициализация и установка настроек при начале работы программы 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.mLockChangeInfo = false;
            this.mFilmotecClass = new FilmotecClass(Path.Combine(Application.StartupPath, "Data\\Filmotec.flm"));
            this.mSettingClass = new SettingClass();
            Location = this.mSettingClass.MainFormLocation;
            Size = this.mSettingClass.MainFormSize;

            InputLanguage.CurrentInputLanguage = this.mSettingClass.CurrentInputLanguage;

            this.mLockChangeSettings = false;
            this.mDiskInfos = this.mFilmotecClass.DiskInfo;
            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(0, string.Empty, string.Empty);
            DisplayInfos();

            SetFont();
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {
            listBoxDiskNumber.Font = textBoxDiskInfo.Font = listBoxFilmName.Font =
            textBoxFilmInfo.Font = textBoxFilterFilmName.Font = this.mSettingClass.ProgramFont;

            listBoxDiskNumber.ForeColor = textBoxDiskInfo.ForeColor = listBoxFilmName.ForeColor =
            textBoxFilmInfo.ForeColor = textBoxFilterFilmName.ForeColor = this.mSettingClass.ProgramFontColor;
            if (this.mFindForm != null && !this.mFindForm.IsDisposed)
            {
                this.mFindForm.SetFont();
            }
        }


        /// <summary>
        /// Отображение текущей информации
        /// </summary>
        private void DisplayInfos()
        {
            this.mLockChangeInfo = true;
            int saveDiskIndex = listBoxDiskNumber.SelectedIndex < 0 ? 0 : listBoxDiskNumber.SelectedIndex;
            int saveFilmIndex = listBoxFilmName.SelectedIndex < 0 ? 0 : listBoxFilmName.SelectedIndex;

            //
            // Переинициализировать список дисков
            //
            int n = 0;
            foreach (DiskInfo diskInfo in this.mDiskInfos)
            {
                if (n < listBoxDiskNumber.Items.Count)
                {
                    listBoxDiskNumber.Items[n] = diskInfo.Number;
                }
                else
                {
                    listBoxDiskNumber.Items.Add(diskInfo.Number);
                }

                n++;                
            }

            while (n < listBoxDiskNumber.Items.Count)
            {
                listBoxDiskNumber.Items.RemoveAt(n);
            }

            //
            // Переинициализировать список фильмов
            //
            n = 0;
            foreach (FilmInfo filmInfo in this.mFilmInfos)
            {                
                if (n < listBoxFilmName.Items.Count)
                {
                    listBoxFilmName.Items[n] = filmInfo.Name;
                }
                else
                {
                    listBoxFilmName.Items.Add(filmInfo.Name);
                }

                n++;
            }

            while (n < listBoxFilmName.Items.Count)
            {
                listBoxFilmName.Items.RemoveAt(n);
            }

            listBoxDiskNumber.SelectedIndex = saveDiskIndex < listBoxDiskNumber.Items.Count ?
                saveDiskIndex :
                listBoxDiskNumber.Items.Count - 1;

            if (saveFilmIndex < listBoxFilmName.Items.Count)
            {
                listBoxFilmName.SelectedIndex = saveFilmIndex;
            }
            else
            {
                listBoxFilmName.SelectedIndex = listBoxFilmName.Items.Count - 1;
            }

            if (listBoxFilmName.SelectedIndex > -1 && this.mFilmInfos.Length > 0)
            {
                textBoxFilmInfo.Text = Helper.GenreToString(this.mFilmInfos[listBoxFilmName.SelectedIndex].Genre) +
                    "\r\n" + this.mFilmInfos[listBoxFilmName.SelectedIndex].Info;
            }

            this.mLockChangeInfo = false;
        }


        #region Добавление, удаление, изменение дисков
        /// <summary>
        /// Добавление диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiskAdd_Click(object sender, EventArgs e)
        {
            var addDiskForm = new AddEditDiskForm(this, AddEditState.Add, this.mFilmotecClass, this.mSettingClass, this.mDiskInfos[listBoxDiskNumber.SelectedIndex], listBoxDiskNumber.SelectedIndex);
            addDiskForm.ShowDialog();
            this.mDiskInfos = this.mFilmotecClass.DiskInfo;
            DisplayInfos();
        }

        /// <summary>
        /// Новый номер диска после его изменения
        /// </summary>
        public int NewDiskNumber;

        /// <summary>
        /// Редактирование диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiskEdit_Click(object sender, EventArgs e)
        {
            if (listBoxDiskNumber.SelectedIndex < 0)
            {
                MessageBox.Show("Диск не выделен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (listBoxDiskNumber.SelectedIndex == 0)
            {
                MessageBox.Show("Нельзя редактировать данную запись", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var editDiskForm = new AddEditDiskForm(this, AddEditState.Edit, this.mFilmotecClass, this.mSettingClass, this.mDiskInfos[listBoxDiskNumber.SelectedIndex], listBoxDiskNumber.SelectedIndex);
            editDiskForm.ShowDialog();
            this.mDiskInfos = this.mFilmotecClass.DiskInfo;
            for (int i = 0; i < this.mDiskInfos.Length; i++)
            {
                if (NewDiskNumber == this.mDiskInfos[i].Number)
                {
                    listBoxDiskNumber.SelectedIndex = i;
                    break;
                }
            }
            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(this.mDiskInfos[listBoxDiskNumber.SelectedIndex].Number, string.Empty, string.Empty);
            DisplayInfos();
        }


        /// <summary>
        /// Удаление диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiskDelete_Click(object sender, EventArgs e)
        {
            if (listBoxDiskNumber.SelectedIndex < 0)
            {
                MessageBox.Show("Диск не выделен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (listBoxDiskNumber.SelectedIndex == 0)
            {
                MessageBox.Show("Нельзя удалить данную запись", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult res = MessageBox.Show("Вы действительно хотите удалить этот диск?\r\nВсе фильмы, ассоциированные с ним также будет удалены.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.No)
            {
                return;
            }

            this.mFilmotecClass.DeleteDisk(this.mDiskInfos[listBoxDiskNumber.SelectedIndex]);
            this.mDiskInfos = this.mFilmotecClass.DiskInfo;
            DisplayInfos();
        }
        #endregion


        #region Добавление, удаление, изменение фильмов
        /// <summary>
        /// Добавление фильма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilmAdd_Click(object sender, EventArgs e)
        {
            var addFilmForm = new AddEditFilmForm(this, AddEditState.Add, this.mFilmotecClass, this.mSettingClass, new FilmInfo { Number = Convert.ToInt32(listBoxDiskNumber.Items[listBoxDiskNumber.SelectedIndex].ToString()), Info = string.Empty, Name = string.Empty });
            addFilmForm.ShowDialog();
            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(this.mDiskInfos[listBoxDiskNumber.SelectedIndex].Number, textBoxFilterFilmName.Text, comboBoxFilterGenre.Text);
            DisplayInfos();
            listBoxFilmName.Focus();
        }


        /// <summary>
        /// Новый номер диска после его изменения
        /// </summary>
        public string NewFilmName;


        /// <summary>
        /// Редактирование фильма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilmEdit_Click(object sender, EventArgs e)
        {
            if (listBoxFilmName.SelectedIndex < 0)
            {
                MessageBox.Show("Фильм не выделен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var editFilmForm = new AddEditFilmForm(this, AddEditState.Edit, this.mFilmotecClass, this.mSettingClass, this.mFilmInfos[listBoxFilmName.SelectedIndex]);
            editFilmForm.ShowDialog();
            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(this.mDiskInfos[listBoxDiskNumber.SelectedIndex].Number, textBoxFilterFilmName.Text, comboBoxFilterGenre.Text);
            for (int i = 0; i < this.mFilmInfos.Length; i++)
            {
                if (NewFilmName == this.mFilmInfos[i].Name)
                {
                    listBoxFilmName.SelectedIndex = i;
                    break;
                }
            }

            DisplayInfos();
            listBoxFilmName.Focus();
        }


        /// <summary>
        /// Удаление фильма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilmDelete_Click(object sender, EventArgs e)
        {
            if (listBoxFilmName.SelectedIndex < 0)
            {
                MessageBox.Show("Фильм не выделен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.mFilmotecClass.DeleteFilm(this.mFilmInfos[listBoxFilmName.SelectedIndex]);
            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(this.mDiskInfos[listBoxDiskNumber.SelectedIndex].Number, textBoxFilterFilmName.Text, comboBoxFilterGenre.Text);
            DisplayInfos();
            listBoxFilmName.Focus();
        }
        #endregion


        /// <summary>
        /// Измение выделенного диска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxDiskNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDiskNumber.SelectedIndex == -1)
            {
                return;
            }

            switch (this.mDiskInfos[listBoxDiskNumber.SelectedIndex].Info)
            {
                case DiskType.Buy:
                    textBoxDiskInfo.Text = "Купленный";
                    break;
                case DiskType.OwnR:
                    textBoxDiskInfo.Text = "Диск    DVD-R";
                    break;
                case DiskType.OwnRW:
                    textBoxDiskInfo.Text = "Диск DVD-RW";
                    break;
                default:
                    textBoxDiskInfo.Text = "Все фильмы";
                    break;
            }

            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(this.mDiskInfos[listBoxDiskNumber.SelectedIndex].Number, textBoxFilterFilmName.Text, comboBoxFilterGenre.Text);
            if (!this.mLockChangeInfo)
            {
                DisplayInfos();
            }
        }


        /// <summary>
        /// Изменение выделенного фильма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFilmName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mLockChangeInfo)
            {
                return;
            }

            textBoxFilmInfo.Text = Helper.GenreToString(this.mFilmInfos[listBoxFilmName.SelectedIndex].Genre) + 
                "\r\n" + this.mFilmInfos[listBoxFilmName.SelectedIndex].Info;
        }


        /// <summary>
        /// Выделение диска при двойном нажатии на фильм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFilmName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string saveSelectedName = this.mFilmInfos[listBoxFilmName.SelectedIndex].Name;
            listBoxDiskNumber.Text = this.mFilmInfos[listBoxFilmName.SelectedIndex].Number.ToString();
            listBoxFilmName.Text = saveSelectedName;
            toolTip.Hide(listBoxFilmName);
        }


        #region Сортировка фильмов и дисков
        /// <summary>
        /// Сортировка дисков по возрастанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiskIncrease_Click(object sender, EventArgs e)
        {
            this.mLockChangeInfo = true;
            string saveDisk = listBoxDiskNumber.Items[listBoxDiskNumber.SelectedIndex].ToString();

            this.mFilmotecClass.CurrentSortDiskType = SortType.Increase;
            this.mDiskInfos = this.mFilmotecClass.DiskInfo;

            int diskIndex = 0;
            for (int i = 1; i < this.mDiskInfos.Length; i++)
            {
                if (this.mDiskInfos[i].Number.ToString() == saveDisk)
                {
                    diskIndex = i;
                    break;
                }
            }

            listBoxDiskNumber.SelectedIndex = diskIndex;
            DisplayInfos();
            this.mLockChangeInfo = false;
        }


        /// <summary>
        /// Сортировка дисков по убыванию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDiskDecrease_Click(object sender, EventArgs e)
        {
            this.mLockChangeInfo = true;
            string saveDisk = listBoxDiskNumber.Items[listBoxDiskNumber.SelectedIndex].ToString();

            this.mFilmotecClass.CurrentSortDiskType = SortType.Decrease;

            this.mDiskInfos = this.mFilmotecClass.DiskInfo;

            int diskIndex = 0;
            for (int i = 1; i < this.mDiskInfos.Length; i++)
            {
                if (this.mDiskInfos[i].Number.ToString() == saveDisk)
                {
                    diskIndex = i;
                    break;
                }
            }

            listBoxDiskNumber.SelectedIndex = diskIndex;

            DisplayInfos();
            this.mLockChangeInfo = false;
        }


        /// <summary>
        /// Сортировка фильмов по возрастанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilmIncrease_Click(object sender, EventArgs e)
        {
            this.mFilmotecClass.CurrentSortFilmType = SortType.Increase;
            if (listBoxFilmName.Items.Count <= 0)
            {
                return;
            }

            string saveFilm = listBoxFilmName.Items[listBoxFilmName.SelectedIndex].ToString();

            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(Convert.ToInt32(listBoxDiskNumber.Items[listBoxDiskNumber.SelectedIndex].ToString()), textBoxFilterFilmName.Text, comboBoxFilterGenre.Text);

            int filmIndex = -1;
            for (int i = 0; i < this.mFilmInfos.Length; i++)
            {
                if (this.mFilmInfos[i].Name == saveFilm)
                {
                    filmIndex = i;
                    break;
                }
            }

            listBoxFilmName.SelectedIndex = filmIndex;

            DisplayInfos();
        }


        /// <summary>
        /// Сортировка фильмов по убыванию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilmDecrease_Click(object sender, EventArgs e)
        {
            this.mFilmotecClass.CurrentSortFilmType = SortType.Decrease;
            if (listBoxFilmName.Items.Count <= 0)
            {
                return;
            }

            string saveFilm = listBoxFilmName.Items[listBoxFilmName.SelectedIndex].ToString();

            this.mFilmInfos = this.mFilmotecClass.GetFilmInfo(Convert.ToInt32(listBoxDiskNumber.Items[listBoxDiskNumber.SelectedIndex].ToString()), textBoxFilterFilmName.Text, comboBoxFilterGenre.Text);

            int filmIndex = -1;
            for (int i = 0; i < this.mFilmInfos.Length; i++)
            {
                if (this.mFilmInfos[i].Name == saveFilm)
                {
                    filmIndex = i;
                    break;
                }
            }

            listBoxFilmName.SelectedIndex = filmIndex;

            DisplayInfos();
        }
        #endregion


        #region Подсказки и стили кнопок
        private void buttonDiskIncrease_MouseEnter(object sender, EventArgs e)
        {
            buttonDiskIncrease.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Сортировать по возрастанию", buttonDiskIncrease, 10, -18);
        }
        private void buttonDiskIncrease_MouseLeave(object sender, EventArgs e)
        {
            buttonDiskIncrease.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDiskIncrease);
        }
        private void buttonDiskDecrease_MouseEnter(object sender, EventArgs e)
        {
            buttonDiskDecrease.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Сортировать по убыванию", buttonDiskDecrease, 10, -18);
        }
        private void buttonDiskDecrease_MouseLeave(object sender, EventArgs e)
        {
            buttonDiskDecrease.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDiskDecrease);
        }
        private void buttonDiskAdd_MouseEnter(object sender, EventArgs e)
        {
            buttonDiskAdd.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Добавить новый диск", buttonDiskAdd, 10, -18);
        }
        private void buttonDiskAdd_MouseLeave(object sender, EventArgs e)
        {
            buttonDiskAdd.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDiskAdd);
        }
        private void buttonDiskEdit_MouseEnter(object sender, EventArgs e)
        {
            buttonDiskEdit.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Редактировать выделенный диск", buttonDiskEdit, 10, -18);
        }
        private void buttonDiskEdit_MouseLeave(object sender, EventArgs e)
        {
            buttonDiskEdit.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDiskEdit);
        }
        private void buttonDiskDelete_MouseEnter(object sender, EventArgs e)
        {
            buttonDiskDelete.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Удалить выделенный диск", buttonDiskDelete, 10, -18);
        }
        private void buttonDiskDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonDiskDelete.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonDiskDelete);
        }
        private void buttonFilmIncrease_MouseEnter(object sender, EventArgs e)
        {
            buttonFilmIncrease.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Сортировать по возрастанию", buttonFilmIncrease, 10, -18);
        }
        private void buttonFilmIncrease_MouseLeave(object sender, EventArgs e)
        {
            buttonFilmIncrease.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonFilmIncrease);
        }
        private void buttonFilmDecrease_MouseEnter(object sender, EventArgs e)
        {
            buttonFilmDecrease.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Сортировать по убыванию", buttonFilmDecrease, 10, -18);
        }

        private void buttonFilmDecrease_MouseLeave(object sender, EventArgs e)
        {
            buttonFilmDecrease.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonFilmDecrease);
        }
        private void buttonFilmAdd_MouseEnter(object sender, EventArgs e)
        {
            buttonFilmAdd.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Добавить новый фильм", buttonFilmAdd, 10, -18);
        }
        private void buttonFilmAdd_MouseLeave(object sender, EventArgs e)
        {
            buttonFilmAdd.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonFilmAdd);
        }
        private void buttonFilmEdit_MouseEnter(object sender, EventArgs e)
        {
            buttonFilmEdit.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Редактировать выделенный фильм", buttonFilmEdit, 10, -18);
        }
        private void buttonFilmEdit_MouseLeave(object sender, EventArgs e)
        {
            buttonFilmEdit.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonFilmEdit);
        }
        private void buttonFilmDelete_MouseEnter(object sender, EventArgs e)
        {
            buttonFilmDelete.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Удалить выделенный фильм", buttonFilmDelete, 10, -18);
        }
        private void buttonFilmDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonFilmDelete.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonFilmDelete);
        }
        private void listBoxFilmName_MouseEnter(object sender, EventArgs e)
        {
            if (listBoxDiskNumber.Text == "0" && this.mSettingClass.IsShowToolTips)
                toolTip.Show("Нажмите дважды для выделения диска", listBoxFilmName, 10, -18);
        }
        private void listBoxFilmName_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(listBoxFilmName);
        }
        private void buttonFindFilm_MouseEnter(object sender, EventArgs e)
        {
            buttonFindFilm.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Поиск фильмов", buttonFindFilm, 10, -18);
        }
        private void buttonFindFilm_MouseLeave(object sender, EventArgs e)
        {
            buttonFindFilm.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonFindFilm);
        }
        private void buttonExit_MouseEnter(object sender, EventArgs e)
        {
            buttonExit.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Выход из программы", buttonExit, 10, -18);
        }
        private void buttonExit_MouseLeave(object sender, EventArgs e)
        {
            buttonExit.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonExit);
        }
        private void buttonSetting_MouseEnter(object sender, EventArgs e)
        {
            buttonSetting.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Настройки программы", buttonSetting, 10, -18);
        }
        private void buttonSetting_MouseLeave(object sender, EventArgs e)
        {
            buttonSetting.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonSetting);
        }
        private void buttonExport_MouseEnter(object sender, EventArgs e)
        {
            buttonExport.FlatStyle = FlatStyle.Popup;
            if (this.mSettingClass.IsShowToolTips)
                toolTip.Show("Экспорт в Excel", buttonExport, 10, -18);
        }
        private void buttonExport_MouseLeave(object sender, EventArgs e)
        {
            buttonExport.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonExport);
        }
        #endregion


        /// <summary>
        /// Кнопка выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Кнопка поиска фильмов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFindFilm_Click(object sender, EventArgs e)
        {
            if (this.mFindForm == null || this.mFindForm.IsDisposed)
            {
                this.mFindForm = new FindForm(this, this.mFilmotecClass, this.mSettingClass);
                this.mFindForm.Show();
            }
            else
            {
                this.mFindForm.Focus();
            }
        }


        /// <summary>
        /// Запуск таймера для задерки фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFilterFilmName_TextChanged(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Enabled = true;
        }


        /// <summary>
        /// Запуск таймера для задерки фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxFilterGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Enabled = true;
        }


        /// <summary>
        /// Запуск фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            listBoxDiskNumber_SelectedIndexChanged(null, null);
        }


        /// <summary>
        /// Сброс фокуса после нажатия на кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drop_Focus(object sender, EventArgs e)
        {
            listBoxDiskNumber.Focus();
        }


        /// <summary>
        /// Показать окно с настройками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetting_Click(object sender, EventArgs e)
        {
            var saveFont = new Font(mSettingClass.ProgramFont, mSettingClass.ProgramFont.Style);            
            Color saveColor = mSettingClass.ProgramFontColor;

            var settingForm = new SettingForm(this.mSettingClass);
            settingForm.ShowDialog();

            if (saveColor != mSettingClass.ProgramFontColor ||
                saveFont.FontFamily.Name != mSettingClass.ProgramFont.FontFamily.Name ||
                saveFont.Height != mSettingClass.ProgramFont.Height ||
                saveFont.IsSystemFont != mSettingClass.ProgramFont.IsSystemFont ||
                saveFont.Name != mSettingClass.ProgramFont.Name ||
                saveFont.Size != mSettingClass.ProgramFont.Size ||
                saveFont.SizeInPoints != mSettingClass.ProgramFont.SizeInPoints ||
                saveFont.Style != mSettingClass.ProgramFont.Style)
            {
                SetFont();
            }
        }


        /// <summary>
        /// Экспортировать данные в Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            FilmInfo[] tempFilmInfo;

            var dataToExport = new List<string[]>();
            for (int i = 1; i < this.mDiskInfos.Length; i++)
            {
                tempFilmInfo = this.mFilmotecClass.GetFilmInfo(this.mDiskInfos[i].Number, string.Empty, string.Empty);
                for (int j = 0; j < tempFilmInfo.Length; j++)
                {
                    string diskInfo;
                    switch (this.mDiskInfos[i].Info)
                    {
                        case DiskType.Buy:
                            diskInfo = "Купленный";
                            break;
                        case DiskType.OwnR:
                            diskInfo = "Диск DVD-R";
                            break;
                        case DiskType.OwnRW:
                            diskInfo = "Диск DVD-RW";
                            break;
                        default:
                            diskInfo = "Неизвестно";
                            break;
                    }

                    dataToExport.Add(new[]
                        {
                            this.mDiskInfos[i].Number.ToString(), 
                            diskInfo,
                            tempFilmInfo[j].Name,
                            tempFilmInfo[j].Info
                        });
                }
            }
            var exportForm = new ExportForm(dataToExport, this.mSettingClass);
            exportForm.ShowDialog();
        }


        /// <summary>
        /// Сохранение размера формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.mLockChangeSettings)
                return;

            this.mSettingClass.MainFormSize = Size;
        }


        /// <summary>
        /// Сохранение позиции формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (this.mLockChangeSettings)
                return;

            this.mSettingClass.MainFormLocation = Location;
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            this.mSettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }


        /// <summary>
        /// Начать фильтрацию, если нажата клавиша на любом из контролов на форме
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void StartFilteringByKeyDown(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')
                || (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == ' '
                || ch == '\'' || ch == '"' || ch == '?' || ch == '!' || ch == '(' || ch == ')'
                || ch == '-' || ch == '+' || ch == '=')
            {
                textBoxFilterFilmName.Focus();
                textBoxFilterFilmName.SelectionLength = 0;
                textBoxFilterFilmName.Text += ch;
                textBoxFilterFilmName.SelectionStart = textBoxFilterFilmName.Text.Length;
            }
            e.Handled = true;
        }
    }
}
