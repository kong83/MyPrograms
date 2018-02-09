using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    public partial class ConvertForm : Form
    {
        /// <summary>
        /// Указатель на главную форму
        /// </summary>
        readonly MainForm _mainForm;

        /// <summary>
        /// Файл с различными функциями
        /// </summary>
        private readonly ActionClass _actClass;

        /// <summary>
        /// Информация с данными для сохранения в реестре
        /// </summary>
        private ConvertParametersInfo _paramInfo;

        /// <summary>
        /// Класс для шифрования человечками
        /// </summary>
        private readonly CryptClass _cryptCls;


        /// <summary>
        /// Разрешение обработки событий типа ХХХ_Changed
        /// </summary>
        private bool _appStart;

        public ConvertForm(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            textCurrent.Text = mainForm.textWindow.SelectedText == "" ? 
                mainForm.textWindow.Text : mainForm.textWindow.SelectedText;
            textCurrent.SelectionStart = 0;
            _actClass = new ActionClass();
            _actClass.LoadParameter(out _paramInfo);
            _cryptCls = new CryptClass(pictureBoxTo.Width, pictureBoxTo.Height);
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertForm_Load(object sender, EventArgs e)
        {
            Location = _paramInfo.CfLocation;
            comboType.Text = _paramInfo.ComboType;
            comboFrom.Text = _paramInfo.ComboFrom;
            comboTo.Text = _paramInfo.ComboTo;
            _appStart = true;
        }


        /// <summary>
        /// Сохранить результат и закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            _mainForm.textWindow.SelectedText = textResult.Text;
            Close();
        }


        /// <summary>
        /// Преобразовать верхний текст в нижний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (comboType.Text == "Шифрование")
            {
                if (comboFrom.Text == "Текст")
                {
                    if (textCurrent.Text.Length == 0)
                    {
                        MessageBox.Show("Для конвертации необходимо напечатать сообщение\r\n(поддерживаются только русские символы)", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    pictureBoxTo.Image = _cryptCls.Crypted(textCurrent.Text);
                }
                else if (comboFrom.Text == "Шифровка")
                {
                    if (pictureBoxFrom.BackgroundImage == null)
                    {
                        MessageBox.Show("Для конвертации необходимо загрузить шифрованное изображение", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    textResult.Text = _cryptCls.DeCrypted(new Bitmap(pictureBoxFrom.BackgroundImage));
                }
            }
            else
            {
                if (comboFrom.Text == comboTo.Text)
                {
                    textResult.Text = textCurrent.Text;
                    return;
                }
                if (comboType.Text == "Кодировка")
                {
                    byte[] b = Encoding.GetEncoding(comboFrom.Text).GetBytes(textCurrent.Text);
                    textResult.Text = Encoding.GetEncoding(comboTo.Text).GetString(b);
                }
                else if (comboType.Text == "Раскладка")
                {
                    if (comboFrom.Text == "Русский текст")
                    {
                        textResult.Text = RusToEnTranslit(textCurrent.Text);
                    }
                    else if (comboFrom.Text == "Транслит")
                    {
                        textResult.Text = EnToRusTranslit(textCurrent.Text);
                    }
                    else if (comboFrom.Text == "Русский текст (английская раскладка)")
                    {
                        textResult.Text = RusToEn(textCurrent.Text);
                    }
                    else if (comboFrom.Text == "Английский текст (русская раскладка)")
                    {
                        textResult.Text = EnToRus(textCurrent.Text);
                    }
                }
            }
        }


        /// <summary>
        /// Изменение типа преобразования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboFrom.Items.Clear();
            comboTo.Items.Clear();
            if (comboType.Text == "Кодировка")
            {
                comboFrom.Items.Add("Windows-1251");
                comboFrom.Items.Add("KOI8-R");
                comboFrom.Items.Add("UTF-7");
                comboFrom.Items.Add("cp866");
                comboTo.Items.Add("Windows-1251");
                comboTo.Items.Add("KOI8-R");
                comboTo.Items.Add("UTF-7");
                comboTo.Items.Add("cp866");
                comboFrom.SelectedIndex = 1;
                comboTo.SelectedIndex = 0;
                buttonOpenShifr.Visible = pictureBoxFrom.Visible =
                buttonSaveShifr.Visible = pictureBoxTo.Visible = false;
            }
            else if (comboType.Text == "Раскладка")
            {
                comboFrom.Items.Add("Русский текст");
                comboFrom.Items.Add("Транслит");
                comboFrom.Items.Add("Русский текст (английская раскладка)");
                comboFrom.Items.Add("Английский текст (русская раскладка)");
                comboFrom.SelectedIndex = 0;
                buttonOpenShifr.Visible = pictureBoxFrom.Visible =
                buttonSaveShifr.Visible = pictureBoxTo.Visible = false;
            }
            else if (comboType.Text == "Шифрование")
            {
                comboFrom.Items.Add("Текст");
                comboFrom.Items.Add("Шифровка");
                comboFrom.SelectedIndex = 0;
            }
            if (!_appStart)
                return;

            _paramInfo.ComboType = comboType.Text;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Выбор результирующей раскладки при смене текущей раскладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboType.Text == "Раскладка" || comboType.Text == "Шифрование")
            {
                comboTo.Items.Clear();
            }

            if (comboFrom.Text == "Русский текст")
            {
                comboTo.Items.Add("Транслит");
                comboTo.SelectedIndex = 0;
            }
            else if (comboFrom.Text == "Транслит")
            {
                comboTo.Items.Add("Русский текст");
                comboTo.SelectedIndex = 0;
            }
            else if (comboFrom.Text == "Русский текст (английская раскладка)")
            {
                comboTo.Items.Add("Русский текст");
                comboTo.SelectedIndex = 0;
            }
            else if (comboFrom.Text == "Английский текст (русская раскладка)")
            {
                comboTo.Items.Add("Английский текст");
                comboTo.SelectedIndex = 0;
            }
            else if (comboFrom.Text == "Текст")
            {
                comboTo.Items.Add("Шифровка");
                comboTo.SelectedIndex = 0;
                buttonOpenShifr.Visible = pictureBoxFrom.Visible = false;
                buttonSaveShifr.Visible = pictureBoxTo.Visible = true;
            }
            else if (comboFrom.Text == "Шифровка")
            {
                comboTo.Items.Add("Текст");
                comboTo.SelectedIndex = 0;
                buttonOpenShifr.Visible = pictureBoxFrom.Visible = true;
                buttonSaveShifr.Visible = pictureBoxTo.Visible = false;
            }
            if (!_appStart)
                return;

            _paramInfo.ComboFrom = comboFrom.Text;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение результирующей раскладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;

            _paramInfo.ComboTo = comboTo.Text;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение позиции формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertForm_LocationChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;

            _paramInfo.CfLocation = Location;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        ///  Изменение языка ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
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
            if (textCurrent.Focused && (keyData == (Keys)131085))
            {
                buttonConvert_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #region Сервисы
        // Русский текст, насписанный в английской раскладке
        private static string RusToEn(IEnumerable<char> text)
        {
            string res = "";
            foreach (char ch in text)
                switch (ch)
                {
                    case 'q': res += 'й'; break;
                    case 'w': res += 'ц'; break;
                    case 'e': res += 'у'; break;
                    case 'r': res += 'к'; break;
                    case 't': res += 'е'; break;
                    case 'y': res += 'н'; break;
                    case 'u': res += 'г'; break;
                    case 'i': res += 'ш'; break;
                    case 'o': res += 'щ'; break;
                    case 'p': res += 'з'; break;
                    case '[': res += 'х'; break;
                    case ']': res += 'ъ'; break;
                    case 'a': res += 'ф'; break;
                    case 's': res += 'ы'; break;
                    case 'd': res += 'в'; break;
                    case 'f': res += 'а'; break;
                    case 'g': res += 'п'; break;
                    case 'h': res += 'р'; break;
                    case 'j': res += 'о'; break;
                    case 'k': res += 'л'; break;
                    case 'l': res += 'д'; break;
                    case ';': res += 'ж'; break;
                    case '\'': res += 'э'; break;
                    case 'z': res += 'я'; break;
                    case 'x': res += 'ч'; break;
                    case 'c': res += 'с'; break;
                    case 'v': res += 'м'; break;
                    case 'b': res += 'и'; break;
                    case 'n': res += 'т'; break;
                    case 'm': res += 'ь'; break;
                    case ',': res += 'б'; break;
                    case '.': res += 'ю'; break;
                    case '/': res += '.'; break;

                    case 'Q': res += 'Й'; break;
                    case 'W': res += 'Ц'; break;
                    case 'E': res += 'У'; break;
                    case 'R': res += 'К'; break;
                    case 'T': res += 'Е'; break;
                    case 'Y': res += 'Н'; break;
                    case 'U': res += 'Г'; break;
                    case 'I': res += 'Ш'; break;
                    case 'O': res += 'Щ'; break;
                    case 'P': res += 'З'; break;
                    case '{': res += 'Х'; break;
                    case '}': res += 'Ъ'; break;
                    case 'A': res += 'Ф'; break;
                    case 'S': res += 'Ы'; break;
                    case 'D': res += 'В'; break;
                    case 'F': res += 'А'; break;
                    case 'G': res += 'П'; break;
                    case 'H': res += 'Р'; break;
                    case 'J': res += 'О'; break;
                    case 'K': res += 'Л'; break;
                    case 'L': res += 'Д'; break;
                    case ':': res += 'Ж'; break;
                    case '"': res += 'Э'; break;
                    case '|': res += '/'; break;
                    case 'Z': res += 'Я'; break;
                    case 'X': res += 'Ч'; break;
                    case 'C': res += 'С'; break;
                    case 'V': res += 'М'; break;
                    case 'B': res += 'И'; break;
                    case 'N': res += 'Т'; break;
                    case 'M': res += 'Ь'; break;
                    case '<': res += 'Б'; break;
                    case '>': res += 'Ю'; break;
                    case '?': res += ','; break;
                    default: res += ch; break;
                }
            return res;
        }

        // Английский текст, написанный в русской раскладке
        private static string EnToRus(IEnumerable<char> text)
        {
            string res = "";
            foreach (char ch in text)
                switch (ch)
                {
                    case 'й': res += 'q'; break;
                    case 'ц': res += 'w'; break;
                    case 'у': res += 'e'; break;
                    case 'к': res += 'r'; break;
                    case 'е': res += 't'; break;
                    case 'н': res += 'y'; break;
                    case 'г': res += 'u'; break;
                    case 'ш': res += 'i'; break;
                    case 'щ': res += 'o'; break;
                    case 'з': res += 'p'; break;
                    case 'х': res += '['; break;
                    case 'ъ': res += ']'; break;
                    case 'ф': res += 'a'; break;
                    case 'ы': res += 's'; break;
                    case 'в': res += 'd'; break;
                    case 'а': res += 'f'; break;
                    case 'п': res += 'g'; break;
                    case 'р': res += 'h'; break;
                    case 'о': res += 'j'; break;
                    case 'л': res += 'k'; break;
                    case 'д': res += 'l'; break;
                    case 'ж': res += ';'; break;
                    case 'э': res += '\''; break;
                    case 'я': res += 'z'; break;
                    case 'ч': res += 'x'; break;
                    case 'с': res += 'c'; break;
                    case 'м': res += 'v'; break;
                    case 'и': res += 'b'; break;
                    case 'т': res += 'n'; break;
                    case 'ь': res += 'm'; break;
                    case 'б': res += ','; break;
                    case 'ю': res += '.'; break;
                    case '.': res += '/'; break;

                    case 'Й': res += 'Q'; break;
                    case 'Ц': res += 'W'; break;
                    case 'У': res += 'E'; break;
                    case 'К': res += 'R'; break;
                    case 'Е': res += 'T'; break;
                    case 'Н': res += 'Y'; break;
                    case 'Г': res += 'U'; break;
                    case 'Ш': res += 'I'; break;
                    case 'Щ': res += 'O'; break;
                    case 'З': res += 'P'; break;
                    case 'Х': res += '{'; break;
                    case 'Ъ': res += '}'; break;
                    case 'Ф': res += 'A'; break;
                    case 'Ы': res += 'S'; break;
                    case 'В': res += 'D'; break;
                    case 'А': res += 'F'; break;
                    case 'П': res += 'G'; break;
                    case 'Р': res += 'H'; break;
                    case 'О': res += 'J'; break;
                    case 'Л': res += 'K'; break;
                    case 'Д': res += 'L'; break;
                    case 'Ж': res += ':'; break;
                    case 'Э': res += '"'; break;
                    case '/': res += '|'; break;
                    case 'Я': res += 'Z'; break;
                    case 'Ч': res += 'X'; break;
                    case 'С': res += 'C'; break;
                    case 'М': res += 'V'; break;
                    case 'И': res += 'B'; break;
                    case 'Т': res += 'N'; break;
                    case 'Ь': res += 'M'; break;
                    case 'Б': res += '<'; break;
                    case 'Ю': res += '>'; break;
                    case ',': res += '?'; break;
                    default: res += ch; break;
                }
            return res;
        }

        // Русский текст в латиницу
        private static string RusToEnTranslit(IEnumerable<char> text)
        {
            string res = "";
            foreach (char ch in text)
                switch (ch)
                {
                    case 'а': res += 'a'; break;
                    case 'б': res += 'b'; break;
                    case 'в': res += 'v'; break;
                    case 'г': res += 'g'; break;
                    case 'д': res += 'd'; break;
                    case 'е':
                    case 'ё': res += 'e'; break;
                    case 'ж': res += "zh"; break;
                    case 'з': res += 'z'; break;
                    case 'и': res += 'i'; break;
                    case 'й': res += 'j'; break;
                    case 'к': res += 'k'; break;
                    case 'л': res += 'l'; break;
                    case 'м': res += 'm'; break;
                    case 'н': res += 'n'; break;
                    case 'о': res += 'o'; break;
                    case 'п': res += 'p'; break;
                    case 'р': res += 'r'; break;
                    case 'с': res += 's'; break;
                    case 'т': res += 't'; break;
                    case 'у': res += 'u'; break;
                    case 'ф': res += 'f'; break;
                    case 'х': res += 'h'; break;
                    case 'ц': res += 'c'; break;
                    case 'ч': res += "ch"; break;
                    case 'ш': res += "sh"; break;
                    case 'щ': res += "csh"; break;
                    case 'ъ': res += '"'; break;
                    case 'ы': res += 'y'; break;
                    case 'ь': res += '\''; break;
                    case 'э': res += 'e'; break;
                    case 'ю': res += "yu"; break;
                    case 'я': res += "ya"; break;

                    case 'А': res += 'A'; break;
                    case 'Б': res += 'B'; break;
                    case 'В': res += 'V'; break;
                    case 'Г': res += 'G'; break;
                    case 'Д': res += 'D'; break;
                    case 'Е':
                    case 'Ё': res += 'E'; break;
                    case 'Ж': res += "Zh"; break;
                    case 'З': res += 'Z'; break;
                    case 'И': res += 'I'; break;
                    case 'Й': res += 'J'; break;
                    case 'К': res += 'K'; break;
                    case 'Л': res += 'L'; break;
                    case 'М': res += 'M'; break;
                    case 'Н': res += 'N'; break;
                    case 'О': res += 'O'; break;
                    case 'П': res += 'P'; break;
                    case 'Р': res += 'R'; break;
                    case 'С': res += 'S'; break;
                    case 'Т': res += 'T'; break;
                    case 'У': res += 'U'; break;
                    case 'Ф': res += 'F'; break;
                    case 'Х': res += 'H'; break;
                    case 'Ц': res += 'C'; break;
                    case 'Ч': res += "Ch"; break;
                    case 'Ш': res += "Sh"; break;
                    case 'Щ': res += "Csh"; break;
                    case 'Ы': res += 'Y'; break;
                    case 'Э': res += 'E'; break;
                    case 'Ю': res += "Yu"; break;
                    case 'Я': res += "Ya"; break;
                    default: res += ch; break;
                }
            return res;
        }

        // Латиницу в русский текст
        private static string EnToRusTranslit(string text)
        {
            string res = "";
            int i = 0;
            while (i < text.Length)
            {
                if (i < text.Length - 1 && text[i] == 'z' && text[i + 1] == 'h')
                {
                    res += 'ж'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 'c' && text[i + 1] == 'h')
                {
                    res += 'ч'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 's' && text[i + 1] == 'h')
                {
                    res += 'ш'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 'c' && text[i] == 's' && text[i + 1] == 'h')
                {
                    res += 'щ'; i += 2;
                }
                else if (i < text.Length - 1 && text[i] == 'y' && text[i + 1] == 'u')
                {
                    res += 'ю'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 'y' && text[i + 1] == 'a')
                {
                    res += 'я'; i++;
                }
                else if (text[i] == 'a')
                    res += 'а';
                else if (text[i] == 'b')
                    res += 'б';
                else if (text[i] == 'v')
                    res += 'в';
                else if (text[i] == 'g')
                    res += 'г';
                else if (text[i] == 'd')
                    res += 'д';
                else if (text[i] == 'e')
                    res += 'е';
                else if (text[i] == 'z')
                    res += 'з';
                else if (text[i] == 'i')
                    res += 'и';
                else if (text[i] == 'j')
                    res += 'й';
                else if (text[i] == 'k')
                    res += 'к';
                else if (text[i] == 'l')
                    res += 'л';
                else if (text[i] == 'm')
                    res += 'м';
                else if (text[i] == 'n')
                    res += 'н';
                else if (text[i] == 'o')
                    res += 'о';
                else if (text[i] == 'p')
                    res += 'п';
                else if (text[i] == 'r')
                    res += 'р';
                else if (text[i] == 's')
                    res += 'с';
                else if (text[i] == 't')
                    res += 'т';
                else if (text[i] == 'u')
                    res += 'у';
                else if (text[i] == 'f')
                    res += 'ф';
                else if (text[i] == 'h')
                    res += 'х';
                else if (text[i] == 'c')
                    res += 'ц';
                else if (text[i] == '"')
                    res += 'ъ';
                else if (text[i] == '\'')
                    res += 'ь';
                else if (text[i] == 'y')
                    res += 'ы';
                else if (i < text.Length - 1 && text[i] == 'Z' && text[i + 1] == 'h')
                {

                    res += 'Ж';
                    i++;
                }
                else if (i < text.Length - 1 && text[i] == 'C' && text[i + 1] == 'h')
                {

                    res += 'Ч';
                    i++;
                }
                else if (i < text.Length - 1 && text[i] == 'S' && text[i + 1] == 'h')
                {

                    res += 'Ш';
                    i++;
                }
                else if (i < text.Length - 1 && text[i] == 'C' && text[i] == 's' && text[i + 1] == 'h')
                {

                    res += 'Щ';
                    i += 2;
                }
                else if (i < text.Length - 1 && text[i] == 'Y' && text[i + 1] == 'u')
                {

                    res += 'Ю';
                    i++;
                }
                else if (i < text.Length - 1 && text[i] == 'Y' && text[i + 1] == 'a')
                {

                    res += 'Я';
                    i++;
                }
                else if (text[i] == 'A')
                    res += 'А';
                else if (text[i] == 'B')
                    res += 'Б';
                else if (text[i] == 'V')
                    res += 'В';
                else if (text[i] == 'G')
                    res += 'Г';
                else if (text[i] == 'D')
                    res += 'Д';
                else if (text[i] == 'E')
                    res += 'Е';
                else if (text[i] == 'Z')
                    res += 'З';
                else if (text[i] == 'I')
                    res += 'И';
                else if (text[i] == 'J')
                    res += 'Й';
                else if (text[i] == 'K')
                    res += 'К';
                else if (text[i] == 'L')
                    res += 'Л';
                else if (text[i] == 'M')
                    res += 'М';
                else if (text[i] == 'N')
                    res += 'Н';
                else if (text[i] == 'O')
                    res += 'О';
                else if (text[i] == 'P')
                    res += 'П';
                else if (text[i] == 'R')
                    res += 'Р';
                else if (text[i] == 'S')
                    res += 'С';
                else if (text[i] == 'T')
                    res += 'Т';
                else if (text[i] == 'U')
                    res += 'У';
                else if (text[i] == 'F')
                    res += 'Ф';
                else if (text[i] == 'H')
                    res += 'Х';
                else if (text[i] == 'C')
                    res += 'Ц';
                else if (text[i] == 'Y')
                    res += 'Ы';
                else
                    res += text[i];
                i++;
            }
            return res;
        }
        #endregion

        #region Подсказки
        private void buttonConvert_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Выполнить выбранное преобразование (Ctrl+Enter)", buttonConvert, -10, -17);
        }
        private void buttonConvert_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonConvert);
        }
        private void buttonSaveAndClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Скопировать полученные в данные в блокнот", buttonSaveAndClose, -10, -17);
        }
        private void buttonSaveAndClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonSaveAndClose);
        }
        private void buttonOpenShifr_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Загрузить шифрованный файл", buttonOpenShifr, -10, -17);
        }
        private void buttonOpenShifr_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonOpenShifr);
        }
        private void buttonSaveShifr_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Сохранить шифрованный файл", buttonSaveShifr, -10, -17);
        }
        private void buttonSaveShifr_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonSaveShifr);
        }
        #endregion
        

        /// <summary>
        /// Запуск преобразования при нажатии на Enter в комбобоксах to или from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboFromTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonConvert_Click(null, null);
            }
        }


        /// <summary>
        /// Открыть зашифрованную картинку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenShifr_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var bit = new Bitmap(openFileDialog.FileName);
                pictureBoxFrom.BackgroundImage = bit;
            }
        }


        /// <summary>
        /// Сохранить полученную зашифрованную картинку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveShifr_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(saveFileDialog.FileName))
                    {
                        File.Delete(saveFileDialog.FileName);
                    }
                    var bit = new Bitmap(pictureBoxTo.Image);
                    bit.Save(saveFileDialog.FileName);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
