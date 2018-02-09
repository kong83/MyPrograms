using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Fonotec
{
    public partial class FindForm : Form
    {
        private readonly MainForm mMainForm;
        private readonly FilmotecClass mFilmotecClass;
        private readonly SettingClass mSettingClass;

        private bool mLockChangeSettings = true;

        public FindForm(MainForm mainForm, FilmotecClass filmotecClass, SettingClass settingClass)
        {
            InitializeComponent();

            this.mMainForm = mainForm;
            this.mFilmotecClass = filmotecClass;
            this.mSettingClass = settingClass;
        }

        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        public void SetFont()
        {
            textBoxDiskNumber.Font = comboBoxDiskInfo.Font = textBoxFilmName.Font =
            textBoxFilmInfo.Font = dataGridViewFindFilms.Font = this.mSettingClass.ProgramFont;

            textBoxDiskNumber.ForeColor = comboBoxDiskInfo.ForeColor = textBoxFilmName.ForeColor =
            textBoxFilmInfo.ForeColor = dataGridViewFindFilms.ForeColor = this.mSettingClass.ProgramFontColor;
        }


        /// <summary>
        /// Кнопка закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private struct AllInfo
        {
            public int DiskNumber;
            public DiskType DiskInfo;
            public string FilmName;
            public string FilmInfo;
            public GenreType Genre;
        }


        private static DiskType GetDiskInfo(IEnumerable<DiskInfo> diskInfo, int diskNumber)
        {
            foreach (DiskInfo di in diskInfo)
            {
                if (di.Number == diskNumber)
                {
                    return di.Info;
                }
            }

            return DiskType.Empty;
        }

        /// <summary>
        /// Сравнивает информацию о дисках. Возвращает false - если различны
        /// </summary>
        /// <param name="diskInfo"></param>
        /// <param name="filterDiskInfo"></param>
        /// <returns></returns>
        private static bool DiskInfoCompare(DiskType diskInfo, string filterDiskInfo)
        {
            switch (filterDiskInfo)
            {
                case "Купленный":
                    return diskInfo == DiskType.Buy;
                case "Диск DVD-R":
                    return diskInfo == DiskType.OwnR;
                case "Диск DVD-RW":
                    return diskInfo == DiskType.OwnRW;
                default:
                    return true;
            }
        }


        /// <summary>
        /// Ищет findStr в str
        /// </summary>
        /// <param name="str">Основная строка</param>
        /// <param name="findStr">Строка для поиска</param>
        /// <param name="isAll">Нужно ли полное совпадение</param>
        /// <param name="isRegistry">Учитывать ли реестр</param>
        /// <returns></returns>
        private static bool IsRemoveFilm(string str, string findStr, bool isAll, bool isRegistry)
        {
            if (!isRegistry)
            {
                str = str.ToLower();
                findStr = findStr.ToLower();
            }

            if (isAll)
            {
                return str != findStr;
            }

            return !str.Contains(findStr);
        }



        /// <summary>
        /// Кнопка поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFind_Click(object sender, EventArgs e)
        {
            DiskInfo[] diskInfos = this.mFilmotecClass.DiskInfo;
            FilmInfo[] filmInfos = this.mFilmotecClass.GetFilmInfo(0, string.Empty, string.Empty);
            dataGridViewFindFilms.Rows.Clear();

            //
            // Создание полного списка фильмов и дисков
            //
            var result = new List<AllInfo>();
            for (int i = 0; i < filmInfos.Length; i++)
            {
                var temp = new AllInfo
                {
                    DiskNumber = filmInfos[i].Number,
                    DiskInfo = GetDiskInfo(diskInfos, filmInfos[i].Number),
                    FilmInfo = filmInfos[i].Info,
                    FilmName = filmInfos[i].Name,
                    Genre = filmInfos[i].Genre
                };
                result.Add(temp);
            }

            //
            // Выбрасывание фильмов с неподходящим номером диска
            //            
            if (!string.IsNullOrEmpty(textBoxDiskNumber.Text))
            {
                string[] diskNumbers = Helper.ConvertDiskNumbersStringToArray(textBoxDiskNumber.Text);
                for (int i = 0; i < result.Count; i++)
                {
                    bool isNeedRemove = true;
                    foreach (string diskNumber in diskNumbers)
                    {
                        if (result[i].DiskNumber.ToString() == diskNumber)
                        {
                            isNeedRemove = false;
                            break;
                        }
                    }

                    if (isNeedRemove)
                    {
                        result.RemoveAt(i--); 
                    }
                }
            }

            //
            // Выбрасывание фильмов с неподходящим типом диска
            //
            if (comboBoxDiskInfo.SelectedIndex > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (!DiskInfoCompare(result[i].DiskInfo, comboBoxDiskInfo.Text))
                    {
                        result.RemoveAt(i--);
                    }
                }
            }

            //
            // Выбрасывание фильмов с неподходящим названием фильма
            //
            if (!string.IsNullOrEmpty(textBoxFilmName.Text))
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (IsRemoveFilm(result[i].FilmName, textBoxFilmName.Text, checkBoxNameAll.Checked, checkBoxNameRegistry.Checked))
                    {
                        result.RemoveAt(i--);
                    }
                }
            }

            //
            // Выбрасывание фильмов с неподходящим информацией о фильме
            //
            if (!string.IsNullOrEmpty(textBoxFilmInfo.Text))
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (IsRemoveFilm(result[i].FilmInfo, textBoxFilmInfo.Text, checkBoxInfoAll.Checked, checkBoxInfoRegistry.Checked))
                    {
                        result.RemoveAt(i--);
                    }
                }
            }

            //
            // Выбрасывание фильмов с неподходящим жанром
            //
            if (comboBoxFilterGenre.Text != "Все жанры")
            {
                GenreType genreType = Helper.StringToGenre(comboBoxFilterGenre.Text);
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].Genre != genreType)
                    {
                        result.RemoveAt(i--);
                    }
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                var addParams = new[]
                    {
                        result[i].DiskNumber.ToString(),
                        DiskTypeToString(result[i].DiskInfo),
                        result[i].FilmName,
                        result[i].FilmInfo
                    };
                dataGridViewFindFilms.Rows.Add(addParams);
            }

            labelFindResult.Text = "Результат поиска: " + GetRightNumberCount(result.Count);
        }


        /// <summary>
        /// Красивое словосочетание с количеством фильмов (0 фильмов, 1 фильм, 2-4 фильма, 5-9 фильмов)
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static string GetRightNumberCount(int count)
        {
            int saveCount = count;
            while (count >= 10)
            {
                count -= 10;
            }

            if (count == 0 || (count >= 5 && count <= 9))
            {
                return saveCount + " фильмов";
            }

            if (count >= 2 && count <= 4)
            {
                return saveCount + " фильма";
            }

            return saveCount + " фильм";
        }


        /// <summary>
        /// Перевод DiskType в соответствующую тестовую строку
        /// </summary>
        /// <param name="diskType"></param>
        /// <returns></returns>
        private static string DiskTypeToString(DiskType diskType)
        { 
            switch (diskType)
            {
                case DiskType.Buy:
                    return "Купленный";
                case DiskType.OwnR:
                    return "DVD-R";
                case DiskType.OwnRW:
                    return "DVD-RW";
                default:
                    return "Неизвестный";
            }            
        }


        /// <summary>
        /// Выставление настроек для формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindForm_Load(object sender, EventArgs e)
        {
            Location = this.mSettingClass.FindFormLocation;

            comboBoxDiskInfo.SelectedIndex = 0;
            comboBoxFilterGenre.SelectedIndex = 0;

            SetFont();

            this.mLockChangeSettings = false;
        }

        /// <summary>
        /// Сохранение позиции формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindForm_LocationChanged(object sender, EventArgs e)
        {
            if (this.mLockChangeSettings)
                return;

            this.mSettingClass.FindFormLocation = Location;
        }


        /// <summary>
        /// Выделение информации на главной форме при двойном нажатии мышкой на строчке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewFindFilms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.mMainForm.SelectDiskAndFilm(dataGridViewFindFilms.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridViewFindFilms.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        #region Подсказки
        private void dataGridViewFindFilms_MouseEnter(object sender, EventArgs e)
        {
            if (this.mSettingClass.IsShowToolTips && dataGridViewFindFilms.Rows.Count > 0)
            {
                toolTip.Show("Нажмите дважды для выделения фильма на главной форме", labelFindResult, 30, 14);
            }
        }

        private void dataGridViewFindFilms_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(labelFindResult);
        }

        private void buttonFind_MouseEnter(object sender, EventArgs e)
        {
            if (this.mSettingClass.IsShowToolTips)
            {
                toolTip.Show("Начать поиск фильмов", buttonFind, 10, -18);
            }
        }

        private void buttonFind_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonFind);
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            if (this.mSettingClass.IsShowToolTips)
            {
                toolTip.Show("Закрыть форму поиска", buttonClose, 10, -18);
            }
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
        }

        private void buttonExport_MouseEnter(object sender, EventArgs e)
        {
            if (this.mSettingClass.IsShowToolTips)
            {
                toolTip.Show("Экспорт в Excel", buttonExport, 10, -18);
            }
        }

        private void buttonExport_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonExport);
        }

        private void textBoxDiskNumber_MouseEnter(object sender, EventArgs e)
        {
            if (this.mSettingClass.IsShowToolTips)
            {
                toolTip.Show("Введите номера дисков через ',' и '-'. Например: 1,3,5-9", textBoxDiskNumber, 10, -18);
            }
        }

        private void textBoxDiskNumber_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(textBoxDiskNumber);
        }
        #endregion


        /// <summary>
        /// Сброс фокуса с кнопок после нажатия
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drop_Focus(object sender, EventArgs e)
        {
            dataGridViewFindFilms.Focus();
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            this.mSettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }


        /// <summary>
        /// Экспортировать данные в Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            var dataToExport = new List<string[]>();
            foreach (DataGridViewRow row in dataGridViewFindFilms.Rows)
            {
                dataToExport.Add(new[] 
                    { 
                        row.Cells[0].Value.ToString(),
                        row.Cells[1].Value.ToString(),
                        row.Cells[2].Value.ToString(),
                        row.Cells[3].Value.ToString()
                    });
            }

            var exportForm = new ExportForm(dataToExport, this.mSettingClass);
            exportForm.ShowDialog();
        }

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (!buttonExport.Focused && !buttonClose.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    buttonFind_Click(null, null);
                    return true;
                }

                if (keyData == Keys.Escape)
                {
                    buttonClose_Click(null, null);
                    return true;
                }
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
