using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace Notepad
{
    public partial class QuickReplaceForm : Form
    {
        /// <summary>
        /// Указатель на главную форму с textWindow
        /// </summary>
        private readonly MainForm m_MainForm;

        /// <summary>
        /// Информация, сохранённая в реестре
        /// </summary>
        private QuickReplaceParametersInfo m_ParamInfo;

        /// <summary>
        /// Класс с разными функциями
        /// </summary>
        private readonly ActionClass m_ActClass;

        /// <summary>
        /// Разрешение обработки событий типа ХХХ_Changed
        /// </summary>
        private bool m_AppStart;


        public QuickReplaceForm(MainForm mainForm)
        {
            InitializeComponent();

            m_MainForm = mainForm;
            m_ActClass = new ActionClass();
            m_ActClass.LoadParameter(out m_ParamInfo);
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickReplaceForm_Load(object sender, EventArgs e)
        {
            Location = m_ParamInfo.QrLocation;

            LoadComboBoxList(comboFind, "qrFindVals");
            comboFind.Text = m_ParamInfo.FindText;

            LoadComboBoxList(comboReplace, "qrReplaceVals");
            comboReplace.Text = m_ParamInfo.ReplaceText;

            TopMost = m_ParamInfo.TopMost;
            if (m_ParamInfo.TopMost)
            {
                buttonTopMostOn.Visible = true;
                buttonTopMostOff.Visible = false;
            }
            else
            {
                buttonTopMostOff.Visible = true;
                buttonTopMostOn.Visible = false;
            }
            m_AppStart = true;
            checkUseRegular.Checked = m_ParamInfo.UseRegExp;
        }


        /// <summary>
        /// Заполнение истории строк поиска или замены
        /// </summary>
        /// <param name="comboBox">ComboBox для хранения строк</param>
        /// <param name="keyReg">findVals или replaceVals</param>
        private void LoadComboBoxList(ComboBox comboBox, string keyReg)
        {
            string[] vals;
            m_ActClass.LoadValues(keyReg, out vals);
            comboBox.Items.Clear();
            for (int i = 0; i < vals.Length; i++)
            {
                comboBox.Items.Add(vals[i]);
            }
        }


        /// <summary>
        /// Замена символов переноса строк и слешей. Слеши перед * оставляем 
        /// (этим они будут отличаться от * в смысле "любое количество символов")
        /// </summary>
        /// <returns></returns>
        private static string ParseSmartString(string smartString)
        {           
            // Разбор слешей
            var sb = new StringBuilder("");
            int i;
            for (i = 0; i < smartString.Length; i++)
            {
                if (smartString[i] == '\\')
                {
                    i++;
                    if (i == smartString.Length)
                    {
                        throw new WrongSmartStringException("Ошибка в записи строки");
                    }

                    if (smartString[i] == 'n')
                    {
                        sb.Append("\r\n");
                    }
                    else if (smartString[i] == '\\')
                    {
                        sb.Append("\\");
                    }
                    else if (smartString[i] == '*')
                    {
                        sb.Append("\\");
                        i--;
                    }
                    else
                    {
                        throw new WrongSmartStringException("Ошибка в записи строки");
                    }
                }
                else
                {
                    sb.Append(smartString[i]);
                }
            }

            // Проверка на то, чтобы в строке не было двух подряд идущих символов *, 
            // обозначающих любое количество символов
            i = 0;
            while (i < smartString.Length - 1)
            {
                if (smartString[i] == '*' && !(i > 0 && smartString[i - 1] == '\\') && smartString[i + 1] == '*')
                {
                    throw new WrongSmartStringException("Ошибка в записи строки");
                }
                i++;
            }
            return sb.ToString();
        }


        /// <summary>
        /// Замена выражения типа asdf*ghjk на qwer (где * - любое количество символов)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            statusStrip1.Items[0].Text = "";
            Application.DoEvents();

            if (comboFind.Text == "")
            {
                comboFind.Focus();
                statusStrip1.Items[0].Text = "Строка для поиска не задана";
                return;
            }

            //
            // Запоминание введённых значений для поиска и замены в реестре 
            //
            m_ActClass.AddValue("qrFindVals", comboFind.Text);
            LoadComboBoxList(comboFind, "qrFindVals");
            if (comboFind.Items.Count > 0)
            {
                comboFind.Text = comboFind.Items[0].ToString();
            }

            m_ActClass.AddValue("qrReplaceVals", comboReplace.Text);
            LoadComboBoxList(comboReplace, "qrReplaceVals");
            if (comboReplace.Items.Count > 0)
            {
                comboReplace.Text = comboReplace.Items[0].ToString();
            }

            statusStrip1.Items[0].Text = "";

            var textBuilder = new StringBuilder(m_MainForm.textWindow.Text);

            //
            // Если используются рег. выражения - то разбираем строку для их поиска и корректной обработки
            //
            int cntRepl = 0;
            if (checkUseRegular.Checked)
            {
                //
                // Замена символов вида \n в строке для поиска на спец. символы перевода строки
                //
                var arrPart = new ArrayList();
                string textFind;
                try
                {
                    textFind = ParseSmartString(comboFind.Text);
                }
                catch (WrongSmartStringException ex)
                {
                    statusStrip1.Items[0].Text = ex.Message + " для поиска";
                    comboFind.Focus();
                    return;
                }
                string textReplace;
                try
                {
                    textReplace = ParseSmartString(comboReplace.Text);
                }
                catch (WrongSmartStringException ex)
                {
                    statusStrip1.Items[0].Text = ex.Message + " для замены";
                    comboReplace.Focus();
                    return;
                }
               
                //
                // Проверка на корректность строки для поиска
                //
                int n;

                //
                // Обработка первого символа в строке для поиска
                //
                if (textFind[0] == '*')
                {
                    arrPart.Add("");
                    n = 1;
                }
                else
                {
                    n = 0;
                }

                //
                // Обработка остальных символов в строке для поиска
                // Создание массива из частей того, что надо искать
                //
                string part = "";
                while (n < textFind.Length)
                {
                    if (textFind[n] == '*' && textFind[n - 1] != '\\')
                    {
                        arrPart.Add(part);
                        part = "";
                    }
                    else
                    {
                        if (textFind[n] != '\\')
                        {
                            part += textFind[n];
                        }
                        else
                        {
                            if (n + 1 < textFind.Length && textFind[n + 1] == '*')
                            {
                                part += "*";
                                n++;
                            }
                            else
                            {
                                part += "\\";
                            }
                            /*else
                            {
                                statusStrip1.Items[0].Text = "Ошибка в записи строки для поиска";
                                comboFind.Focus();
                                return;
                            }*/
                        }
                    }
                    n++;
                }
                arrPart.Add(part);

                var parts = new string[arrPart.Count];
                arrPart.CopyTo(parts);

                /*string q = "";
                for(int i = 0; i < parts.Length; i++) {

                  q += parts[i] + "\r\n";
                }
                MessageBox.Show(q);
                return;*/

                //
                // Если в строке для поиска нет * (или только * и ничего больше)
                //
                if (parts.Length == 1)
                {
                    if (parts[0] == "")
                    {
                        statusStrip1.Items[0].Text = "Произведено замен: 1";
                        m_MainForm.textWindow.Text = textReplace;
                        m_MainForm.textWindow.SelectionStart = 0;
                    }
                    else
                    {
                        //           
                        // Подсчёт количества будущих замен
                        //
                        string text = m_MainForm.textWindow.Text;
                        int num = text.IndexOf(textFind);
                        while (num != -1)
                        {
                            cntRepl++;
                            num = text.IndexOf(textFind, num + textFind.Length);
                        }

                        //
                        // Собственно, замена
                        //
                        textBuilder.Replace(textFind, textReplace);
                    }
                }
                else
                {
                    //
                    // Если строка для поиска содержит * - то ищутся по порядку все возможных части
                    // строки для поиска (между *)
                    //
                    int num1 = 0;
                    if (parts[0] != "")
                    {
                        num1 = (textBuilder.ToString()).IndexOf(parts[0], 0);
                    }

                    while (num1 != -1 && num1 < textBuilder.Length)
                    {
                        int num2 = num1 + parts[0].Length;
                        for (int i = 1; i < parts.Length; i++)
                        {
                            if (parts[i] != "")
                            {
                                num2 = (textBuilder.ToString()).IndexOf(parts[i], num2) + parts[i].Length;
                                if (num2 == parts[i].Length - 1)
                                {
                                    goto quit;
                                }
                            }
                            else
                            {
                                num2 = textBuilder.Length;
                                break;
                            }
                        }

                        textBuilder.Remove(num1, num2 - num1);
                        textBuilder.Insert(num1, textReplace);
                        cntRepl++;
                        statusStrip1.Items[0].Text = "Произведено замен: " + cntRepl;
                        Application.DoEvents();
                        num1 += textReplace.Length;
                        if (parts[0] != "")
                        {
                            num1 = (textBuilder.ToString()).IndexOf(parts[0], num1);
                        }
                    }
                }
            }
            else
            {
                //
                // Замена без использования рег. выражений
                // Подсчёт количества будущих замен
                //
                string text = m_MainForm.textWindow.Text;
                int num = text.IndexOf(comboFind.Text);
                while (num != -1)
                {

                    cntRepl++;
                    num = text.IndexOf(comboFind.Text, num + comboFind.Text.Length);
                }

                //
                // Собственно, замена
                //
                textBuilder.Replace(comboFind.Text, comboReplace.Text);
            }
        quit:

            statusStrip1.Items[0].Text = "Произведено замен: " + cntRepl;
            m_MainForm.textWindow.Text = textBuilder.ToString();
            m_MainForm.textWindow.SelectionStart = 0;
        }


        /// <summary>
        /// Отмена свойства "поверх всех"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTopMostOn_Click(object sender, EventArgs e)
        {
            m_ParamInfo.TopMost = false;
            buttonTopMostOn.Visible = false;
            buttonTopMostOff.Visible = true;
            TopMost = false;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Включение свойства "поверх всех"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTopMostOff_Click(object sender, EventArgs e)
        {
            m_ParamInfo.TopMost = true;
            buttonTopMostOn.Visible = true;
            buttonTopMostOff.Visible = false;
            TopMost = true;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Изменение использования регулярных выражений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkUseRegular_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
            {
                return;
            }

            if (checkUseRegular.Checked)
            {
                Size = new Size(Width, 220);
                groupBoxUseRegular.Height = 80;
            }
            else
            {
                Size = new Size(Width, 160);
                groupBoxUseRegular.Height = 0;
            }
            m_ParamInfo.UseRegExp = checkUseRegular.Checked;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Изменение местоположения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickReplaceForm_LocationChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
            {
                return;
            }

            m_ParamInfo.QrLocation = Location;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Изменение текста для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboFind_TextChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
            {
                return;
            }

            m_ParamInfo.FindText = comboFind.Text;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Измение текста для замены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboReplace_TextChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
            {
                return;
            }

            m_ParamInfo.ReplaceText = comboReplace.Text;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Ввод специальных символов в поле для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ll = (LinkLabel)sender;
            switch (ll.Name)
            {
                case "linkLabelAny":
                    comboFind.Text += "*";
                    break;
                case "linkLabelCaret":
                    comboFind.Text += "\\n";
                    break;
                case "linkLabelStar":
                    comboFind.Text += "\\*";
                    break;
                case "linkLabelSlash":
                    comboFind.Text += "\\\\";
                    break;
            }
            comboFind.Focus();
            comboFind.SelectionStart = comboFind.Text.Length;
            comboFind.SelectionLength = 0;
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
            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// Переход к редактированию строки для замены при нажатии на Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboReplace.Focus();
            }
        }


        /// <summary>
        /// Запуск замены при нажатии на Enter
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

        #region Подсказки
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

