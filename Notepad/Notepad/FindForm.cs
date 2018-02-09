using System;
using System.Text;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FindForm : Form
    {
        /// <summary>
        /// Тип формы
        /// </summary>
        private TypeFindForm _typeForm;

        /// <summary>
        /// Информация, сохранённая в реестре
        /// </summary>
        private FindParametersInfo _paramInfo;

        /// <summary>
        /// Класс с разными функциями
        /// </summary>
        private readonly ActionClass _actClass;

        /// <summary>
        /// Указатель на главную форму с textWindow
        /// </summary>
        private readonly MainForm _mainForm;

        /// <summary>
        /// Разрешение обработки событий типа ХХХ_Changed
        /// </summary>
        private bool _appStart;


        public FindForm(TypeFindForm typeForm, MainForm mainForm)
        {
            InitializeComponent();
            _typeForm = typeForm;
            _mainForm = mainForm;
            _actClass = new ActionClass();
            _actClass.LoadParameter(out _paramInfo);
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindForm_Load(object sender, EventArgs e)
        {
            if (_typeForm == TypeFindForm.Find)
            {
                SetType(TypeFindForm.Find);
            }
            else
            {
                SetType(TypeFindForm.Replace);
            }

            Location = _paramInfo.FfLocation;
            checkBoxRegistr.Checked = _paramInfo.CheckRegistr;
            checkBoxFindUp.Checked = _paramInfo.CheckSearchUp;
            checkBoxAllWord.Checked = _paramInfo.CheckWordAll;

            LoadComboBoxList(comboFind, "findVals");
            comboFind.Text = _paramInfo.FindText;

            LoadComboBoxList(comboReplace, "replaceVals");
            comboReplace.Text = _paramInfo.ReplaceText;

            TopMost = _paramInfo.TopMost;
            if (_paramInfo.TopMost)
            {
                buttonTopMostOn.Visible = true;
                buttonTopMostOff.Visible = false;
            }
            else
            {
                buttonTopMostOff.Visible = true;
                buttonTopMostOn.Visible = false;
            }
            _appStart = true;
        }


        /// <summary>
        /// Заполнение истории строк поиска или замены
        /// </summary>
        /// <param name="comboBox">ComboBox для хранения строк</param>
        /// <param name="keyReg">findVals или replaceVals</param>
        private void LoadComboBoxList(ComboBox comboBox, string keyReg)
        {
            string[] vals;
            _actClass.LoadValues(keyReg, out vals);
            comboBox.Items.Clear();
            for (int i = 0; i < vals.Length; i++)
            {
                comboBox.Items.Add(vals[i]);
            }
        }


        /// <summary>
        /// Установить нужный тип формы
        /// </summary>
        /// <param name="newType"></param>
        public void SetType(TypeFindForm newType)
        {
            _typeForm = newType;
            if (_typeForm == TypeFindForm.Replace)
            {
                buttonChangePlus.Visible = false;
                buttonChangeMinus.Visible = true;
                Height = 190;
                labelReplace.Visible = comboReplace.Visible = buttonReplace.Visible = buttonReplaceAll.Visible = true;
                comboReplace.Focus();
                Text = "Замена";
            }
            else
            {
                buttonChangePlus.Visible = true;
                buttonChangeMinus.Visible = false;
                Height = 160;
                labelReplace.Visible = comboReplace.Visible = buttonReplace.Visible = buttonReplaceAll.Visible = false;
                comboFind.Focus();
                Text = "Поиск";
            }
        }


        /// <summary>
        /// Изменение типа формы на поисковую
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangePlus_Click(object sender, EventArgs e)
        {
            SetType(TypeFindForm.Replace);
        }


        /// <summary>
        /// Изменение типа формы на замену
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangeMinus_Click(object sender, EventArgs e)
        {
            SetType(TypeFindForm.Find);
        }


        /// <summary>
        /// Проверить данный символ на букву и цифру
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static bool CheckChar(int ch)
        {
            if ((ch >= 48 && ch <= 57) || (ch >= 1072 && ch <= 1103) || (ch >= 1040 && ch <= 1071) ||
              (ch >= 97 && ch <= 122) || (ch >= 65 && ch <= 90) || ch == 1105 || ch == 1025)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Проверить, является ли найденное слово - отдельным (если это указано в опциях)
        /// </summary>
        /// <param name="sel"></param>
        /// <returns></returns>
        private bool CheckWord(int sel)
        {
            if (checkBoxAllWord.Checked)
            {
                char first = ' ';
                if (sel > 0)
                    first = _mainForm.textWindow.Text[sel - 1];
                char last = ' ';
                if (_mainForm.textWindow.Text.Length > sel + comboFind.Text.Length + 1)
                    last = _mainForm.textWindow.Text[sel + comboFind.Text.Length];

                if (CheckChar(first) || CheckChar(last))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Найти следующее вхождение
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int FindNext(string text, int start, int length)
        {
            int sel = 0;
            if (!checkBoxRegistr.Checked && !checkBoxFindUp.Checked)
            {
                sel = _mainForm.textWindow.Text.ToLower().IndexOf(text.ToLower(), start + length);
            }
            else if (checkBoxRegistr.Checked && !checkBoxFindUp.Checked)
            {
                sel = _mainForm.textWindow.Text.IndexOf(text, start + length);
            }
            else if (!checkBoxRegistr.Checked && checkBoxFindUp.Checked)
            {
                sel = _mainForm.textWindow.Text.ToLower().LastIndexOf(text.ToLower(), start);
            }
            else if (checkBoxRegistr.Checked && checkBoxFindUp.Checked)
            {
                sel = _mainForm.textWindow.Text.LastIndexOf(text, start);
            }
            return sel;
        }


        /// <summary>
        /// Найти дальше
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            if (comboFind.Text == "")
            {
                comboFind.Focus();
                return;
            }
            bool callRepeat = false;

            _appStart = false;
            if (comboFind.Items.Count == 0 || comboFind.Text != comboFind.Items[0].ToString())
            {
                _actClass.AddValue("findVals", comboFind.Text);
                LoadComboBoxList(comboFind, "findVals");
                if (comboFind.Items.Count > 0)
                {
                    comboFind.Text = comboFind.Items[0].ToString();
                }
            }
            if (comboReplace.Visible && (comboReplace.Items.Count == 0 || comboReplace.Text != comboReplace.Items[0].ToString()))
            {
                _actClass.AddValue("replaceVals", comboReplace.Text);
                LoadComboBoxList(comboReplace, "replaceVals");
                if (comboReplace.Items.Count > 0)
                {
                    comboReplace.Text = comboReplace.Items[0].ToString();
                }
            }
            _appStart = true;

            statusStrip1.Items[0].Text = "";
            int start = _mainForm.textWindow.SelectionStart;
            int len = _mainForm.textWindow.SelectionLength;
        repeat:
            int sel = FindNext(comboFind.Text, start, len);

            if (sel > -1)
            {
                if (!CheckWord(sel))
                {
                    start = sel;
                    len = comboFind.Text.Length;
                    goto repeat;
                }
                _mainForm.textWindow.SelectionStart = sel;
                _mainForm.textWindow.SelectionLength = comboFind.Text.Length;
                _mainForm.textWindow.ScrollToCaret();
                _mainForm.ShowPosition();
            }
            else
            {
                if (checkBoxFindUp.Checked)
                {
                    statusStrip1.Items[0].Text = "Достигнуто начало документа. Поиск продолжен с конца";
                    sel = FindNext(comboFind.Text, _mainForm.textWindow.Text.Length, 0);
                }
                else
                {
                    statusStrip1.Items[0].Text = "Достигнут конец документа. Поиск продолжен с начала";
                    sel = FindNext(comboFind.Text, 0, 0);
                }
                if (sel > -1 && !callRepeat)
                {
                    if (!CheckWord(sel))
                    {
                        callRepeat = true;
                        start = sel;
                        len = comboFind.Text.Length;
                        goto repeat;
                    }
                    _mainForm.textWindow.SelectionStart = sel;
                    _mainForm.textWindow.SelectionLength = comboFind.Text.Length;
                    _mainForm.textWindow.ScrollToCaret();
                    _mainForm.ShowPosition();
                }
                else
                {
                    statusStrip1.Items[0].Text = "Строка не найдена";
                }
            }
        }


        /// <summary>
        /// Заменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            if (_mainForm.textWindow.SelectedText == "")
            {
                buttonFindNext_Click(null, null);
            }
            else
            {
                _mainForm.textWindow.SelectedText = comboReplace.Text;                
                buttonFindNext_Click(null, null);
            }
        }


        /// <summary>
        /// Заменить всё
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            _appStart = false;
            if (comboFind.Items.Count == 0 || comboFind.Text != comboFind.Items[0].ToString())
            {
                _actClass.AddValue("findVals", comboFind.Text);
                LoadComboBoxList(comboFind, "findVals");
                if (comboFind.Items.Count > 0)
                {
                    comboFind.Text = comboFind.Items[0].ToString();
                }
            }
            if (comboReplace.Visible && (comboReplace.Items.Count == 0 || comboReplace.Text != comboReplace.Items[0].ToString()))
            {
                _actClass.AddValue("replaceVals", comboReplace.Text);
                LoadComboBoxList(comboReplace, "replaceVals");
                if (comboReplace.Items.Count > 0)
                {
                    comboReplace.Text = comboReplace.Items[0].ToString();
                }
            }
            _appStart = true;

            var textBuilder = new StringBuilder(_mainForm.textWindow.Text);
            int cnt = 0;
            //
            // Подсчёт количества будущих замен
            //
            string text = _mainForm.textWindow.Text;
            int num = text.IndexOf(comboFind.Text);
            while (num != -1)
            {
                cnt++;
                num = text.IndexOf(comboFind.Text, num + comboFind.Text.Length);
            }
            num = text.LastIndexOf(comboFind.Text);
            
            if (cnt > 0)
            {
                //
                // Собственно, замена
                //
                textBuilder.Replace(comboFind.Text, comboReplace.Text, 0, num);

                //
                // Замена последнего
                //
                num = (textBuilder.ToString()).IndexOf(comboFind.Text);

                textBuilder.Replace(comboFind.Text, comboReplace.Text, num, comboFind.Text.Length);
                _mainForm.textWindow.Text = textBuilder.ToString();
                _mainForm.textWindow.SelectionStart = num + comboReplace.Text.Length;
            }
            statusStrip1.Items[0].Text = "Произведено замен: " + cnt;
        }


        /// <summary>
        /// Изменение местоположения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindForm_LocationChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;
            _paramInfo.FfLocation = Location;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение текста для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textFind_TextChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;
            _paramInfo.FindText = comboFind.Text;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Измение текста для замены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textReplace_TextChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;
            _paramInfo.ReplaceText = comboReplace.Text;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение параметра учёта регистра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxRegistr_CheckedChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;
            _paramInfo.CheckRegistr = checkBoxRegistr.Checked;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение параметра поиска слова целиком
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAllWord_CheckedChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;
            _paramInfo.CheckWordAll = checkBoxAllWord.Checked;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение параметра поиска вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxFindUp_CheckedChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;
            _paramInfo.CheckSearchUp = checkBoxFindUp.Checked;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение языка ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            _mainForm.MainForm_InputLanguageChanged(sender, e);
        }


        /// <summary>
        /// Выход при нажатии Esc
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            if (keyData == (Keys)131144)
            {
                SetType(TypeFindForm.Replace);
                return true;
            }
            if (keyData == (Keys)131142)
            {
                SetType(TypeFindForm.Find);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// Поиск следующего значения при нажатии на Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonFindNext_Click(null, null);
            }
        }


        /// <summary>
        /// Очередная замена при нажатии на Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboReplace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonReplace_Click(null, null);
            }
        }


        /// <summary>
        /// Отмена свойства "поверх всех"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTopMostOn_Click(object sender, EventArgs e)
        {
            _paramInfo.TopMost = false;
            buttonTopMostOn.Visible = false;
            buttonTopMostOff.Visible = true;
            TopMost = false;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Включение свойства "поверх всех"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTopMostOff_Click(object sender, EventArgs e)
        {
            _paramInfo.TopMost = true;
            buttonTopMostOn.Visible = true;
            buttonTopMostOff.Visible = false;
            TopMost = true;
            _actClass.SaveParameter(_paramInfo);
        }

        #region Подсказки
        private void buttonChangeMinus_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Поиск текста", buttonChangeMinus, -10, -17);
        }
        private void buttonChangeMinus_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonChangeMinus);
        }
        private void buttonChangePlus_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Замена текста", buttonChangePlus, -10, -17);
        }
        private void buttonChangePlus_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonChangePlus);
        }
        private void buttonTopMostOff_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Располагать поверх других окон", buttonTopMostOff, -10, -17);
        }
        private void buttonTopMostOff_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonTopMostOff);
        }
        private void buttonTopMostOn_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Отменить расположение поверх других окон", buttonTopMostOn, -10, -17);
        }
        private void buttonTopMostOn_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonTopMostOn);
        }
        #endregion
    }
}
