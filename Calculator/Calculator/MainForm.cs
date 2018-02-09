using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Calculator
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Текущая система счисления, в которой проводятся вычисления
        /// </summary>
        private int _notation = 10;

        private bool _showHint = true;

        private readonly int _startHeight;

        public MainForm()
        {
            InitializeComponent();

            _startHeight = Height;
        }

        // Кнопка "Вычислить"
        private void buttonRun_Click(object sender, EventArgs e)
        {
            Perevod perevod;               // Переменная для класса перевода кодировок            
            string text = textOperation.Text.ToUpper();  // строка пользователя без пробелов и в верхнем регистре
            var ptext = new string[100];   // массив элементов строки в обратной польской записи
            int ptn = 0;                   // указатель для работы с этим массивом              
            var stec = new char[100];      // стек для (, ), +, -, * и / (исп. при получении ptext)
            int stn = 0;                   // укзатель для работы с этим стеком  
            var stecStr = new string[100]; // стек для чисел и знаков (исп. для получения rezult)
            int ssn = 0;                   // укзатель для работы с этим стеком                         

            // Убирание пробелов из выражения
            while(text.IndexOf(' ') > -1)
            {
                text = text.Replace(" ", string.Empty);
            }

            // Если строка для вычисления не пуста, то начинаем с ней работать
            if (text.Length > 0)
            {
                // Вставление 0 перед унарным минусом в начале выражения     
                if (text[0] == '-' || text[0] == '+')
                {
                    text = "0" + text;
                }               
                
                string temp = text[0].ToString();
                // Вставление 0 перед унарным минусом после открывающей скобки
                for (int i = 1; i < text.Length; i++)
                {
                    if (ExpressionVerifier.IsOpenBracket(text[i - 1]) && (text[i] == '-' || text[i] == '+'))
                    {
                        temp += "0" + text[i];
                    }
                    else
                    {
                        temp += text[i];
                    }
                }

                text = temp;
            }
            else
            { // Если строка оказалась пустой, то выходим			
                textOperation.Focus();
                textResult.Text = string.Empty;
                return;
            }

            // Проверяем наше выражение на синтаксическую правильность
            var verifier = new ExpressionVerifier(text);
            switch (verifier.Verify())
            {
                case 1:
                    textResult.Text = "Error: placing brackets";
                    textOperation.Focus();
                    return;
                case 2:
                    textResult.Text = "Error: quantity character";
                    textOperation.Focus();
                    return;
                case 3:
                    textResult.Text = "Error: writing numbers";
                    textOperation.Focus();
                    return;
                case 4:
                    textResult.Text = "Error: unknown symbol";
                    textOperation.Focus();
                    return;
                case 5:
                    textResult.Text = "Error: syntax expression";
                    textOperation.Focus();
                    return;
            }

            // Получаем строку в обратной польской записи
            for (int i = 0; i < text.Length; i++)
            {
                if (ExpressionVerifier.IsDigit(text[i]))
                {
                    while (i < text.Length && (ExpressionVerifier.IsDigit(text[i]) || ExpressionVerifier.IsSeparator(text[i])))
                    {
                        ptext[ptn] += text[i++];
                    }

                    perevod = new Perevod(ptext[ptn]);
                    ptext[ptn] = perevod.ConvertNumberFromCurrentNotationToDec(_notation);
                    if (ptext[ptn].Length > 5 && ptext[ptn].Substring(0, 5).Equals("Error"))
                    {
                        textResult.Text = ptext[ptn];
                        textOperation.Focus();
                        return;
                    }

                    ptn++;
                    i--;
                }
                else if (ExpressionVerifier.IsOpenBracket(text[i]))
                {
                    stec[stn++] = text[i];
                }
                else if (ExpressionVerifier.IsCloseBracket(text[i]))
                {
                    stn--;
                    while (!ExpressionVerifier.IsOpenBracket(stec[stn]))
                        ptext[ptn++] += stec[stn--];
                }
                else
                {
                    switch (text[i])
                    {
                        case '+':
                        case '-':
                            if (stn == 0 || ExpressionVerifier.IsOpenBracket(stec[stn - 1]))
                                stec[stn++] = text[i];
                            else
                            {

                                stn--;
                                while (stn != -1 && !ExpressionVerifier.IsOpenBracket(stec[stn]))
                                    ptext[ptn++] += stec[stn--];
                                stn++;
                                stec[stn++] = text[i];
                            }
                            break;
                        case '*':
                        case '/':
                        case '%':
                            if (stn == 0 || ExpressionVerifier.IsOpenBracket(stec[stn - 1]) || stec[stn - 1] == '+' || stec[stn - 1] == '-')
                                stec[stn++] = text[i];
                            else
                            {

                                stn--;
                                while (stn != -1 && !ExpressionVerifier.IsOpenBracket(stec[stn]) && stec[stn] != '+' && stec[stn] != '-')
                                    ptext[ptn++] += stec[stn--];
                                stn++;
                                stec[stn++] = text[i];
                            }
                            break;
                        case '^':
                            if (stn == 0 || ExpressionVerifier.IsOpenBracket(stec[stn - 1]) || stec[stn - 1] == '+' || stec[stn - 1] == '-' || stec[stn - 1] == '*' || stec[stn - 1] == '/')
                                stec[stn++] = text[i];
                            else
                            {

                                stn--;
                                while (stn != 0 && !ExpressionVerifier.IsOpenBracket(stec[stn]) && stec[stn] != '+' && stec[stn] != '-' && stec[stn] != '*' && stec[stn] != '/')
                                    ptext[ptn++] += stec[stn--];
                                stn++;
                                stec[stn++] = text[i];
                            }
                            break;
                    }
                }
            }

            for (int i = stn - 1; i >= 0; i--)
            {
                ptext[ptn++] += stec[i];
            }            

            // Вычисляем выражение, записанное в обратной польской записи
            BigNumber c;
            for (int i = 0; i < ptn; i++)
            {
                if (ExpressionVerifier.IsDigit(Convert.ToChar(ptext[i].Substring(0, 1))))
                {
                    stecStr[ssn++] = ptext[i];
                }
                else
                {
                    var a = new BigNumber(ExpressionVerifier.RemoveBeganZerosFromNumber(stecStr[ssn - 2]));
                    var b = new BigNumber(ExpressionVerifier.RemoveBeganZerosFromNumber(stecStr[ssn - 1]));

                    switch (Convert.ToChar(ptext[i].Substring(0, 1)))
                    {
                        case '+':
                            c = a + b;
                            break;
                        case '-':
                            c = a - b;
                            break;
                        case '*':
                            c = a * b;
                            break;
                        case '/':
                            c = a / b;
                            if (c.Value.StartsWith("Error"))
                            {
                                textResult.Text = c.Value;
                                textOperation.Focus();
                                return;
                            }
                            break;
                        case '%':
                            c = a % b;
                            if (c.Value.StartsWith("Error"))
                            {
                                textResult.Text = c.Value;
                                textOperation.Focus();
                                return;
                            }
                            break;
                        case '^':
                            c = a ^ b;
                            break;
                        default:
                            textResult.Text = "Error: RUNTIME ERROR";
                            textOperation.Focus();
                            return;
                    }

                    stecStr[ssn - 2] = c.Value;
                    ssn--;
                }
            }

            if (ssn != 1)
            {
                textResult.Text = "Error: syntax expression";
                textOperation.Focus();
                return;
            }

            perevod = new Perevod(stecStr[0]);
            textResult.Text = perevod.ConvertNumberFromDecToTargetNotation(_notation);
            textOperation.Focus();
            if (!textResult.Text.StartsWith("Error"))
            {
                var param = new string[2];
                param[0] = textOperation.Text;
                param[1] = textResult.Text;
                List.Rows.Insert(0, param);
            }
        }

        // Кнопка "Выход"
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Изменений текущей кодировки
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton) sender;
            string[] nameParts = radioButton.Name.Split(('_'));

            try
            {
                _notation = Convert.ToInt32(nameParts[1]);
            }
            catch
            {
                textResult.Text = "Error: Internal error";
                return;
            }            
            
            textOperation.Focus();
        }

        // Вычисление при нажатии кнопки Enter
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (textOperation.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    buttonRun_Click(null, null);
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                buttonExit_Click(null, null);
            }

            return base.ProcessDialogKey(keyData);
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point position);


        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        // Показ подсказки (всех систем счисления) при наведении мыши на textBox2.Text
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (textResult.Text.Length > 5 && textResult.Text.Substring(0, 5).Equals("Error"))
            {
                toolTip1.Show(textResult.Text, textResult);
                return;
            }

            if (_showHint && textResult.Text.Length > 0)
            {
                var perevod = new Perevod(textResult.Text);
                string dec = perevod.ConvertNumberFromCurrentNotationToDec(_notation);

                if (dec.Length > 5)
                {

                    if (dec.Substring(0, 5).Equals("Error"))
                    {

                        toolTip1.Show(dec, textResult);
                        return;
                    }
                }

                perevod = new Perevod(dec);

                string s = "  2:  " + perevod.ConvertNumberFromDecToTargetNotation(2) + "\n  8:  " + perevod.ConvertNumberFromDecToTargetNotation(8) + "\n10:  " + dec + "\n16:  " + perevod.ConvertNumberFromDecToTargetNotation(16);

                _showHint = false;
                Point p;
                GetCursorPos(out p);
                toolTip1.Show(s, this, p.X - Left + 5, p.Y - Top + 5);
            }
        }

        // Завершение показа подсказки для результата
        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            _showHint = true;
            toolTip1.Hide(this);
        }

        /// <summary>
        /// Показ числа во всех системах счисления
        /// </summary>
        private void CreateViewForm()
        {
            var viewForm = new ViewForm();
            if (textResult.Text.Length > 5)
            {
                if (textResult.Text.Substring(0, 5).Equals("Error"))
                {
                    MessageBox.Show("Result value is corrupt", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (textResult.Text.Length > 0)
            {
                var perevod = new Perevod(textResult.Text);
                string dec = perevod.ConvertNumberFromCurrentNotationToDec(_notation);

                if (dec.Length > 5)
                {
                    if (dec.Substring(0, 5).Equals("Error"))
                    {
                        MessageBox.Show("Result value is corrupt", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                perevod = new Perevod(dec);
                viewForm.textBox1.Text = perevod.ConvertNumberFromDecToTargetNotation(2);
                viewForm.textBox2.Text = perevod.ConvertNumberFromDecToTargetNotation(8);
                viewForm.textBox3.Text = perevod.ConvertNumberFromDecToTargetNotation(10);
                viewForm.textBox4.Text = perevod.ConvertNumberFromDecToTargetNotation(16);
                viewForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Result value is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }        

        // Показ числа во всех системах счисления
        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CreateViewForm();
        }

        // Показ списка истории
        private void buttonShowHistory_Click(object sender, EventArgs e)
        {
            if (Height == _startHeight)
            {
                buttonShowHistory.FlatStyle = FlatStyle.Flat;
                Height = _startHeight + 145;
            }
            else
            {
                buttonShowHistory.FlatStyle = FlatStyle.Standard;
                Height = _startHeight;
            }
            textOperation.Focus();
        }

        #region Подсказки
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Вычислить", buttonRun, 15, -17);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonRun);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Показать историю", buttonShowHistory, 15, -17);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonShowHistory);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выход", buttonExit, 15, -17);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonExit);
        }
        #endregion

        // Показ контекстного меню
        private void List_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    List.CurrentCell = List.Rows[e.RowIndex].Cells[0];
                    Point p;
                    GetCursorPos(out p);
                    contextMenuStrip1.Show(p.X, p.Y);
                }
            }
        }

        // Взятие результата или операции
        private void GetResult(int n)
        {
            textOperation.Text += List.Rows[List.CurrentCellAddress.Y].Cells[n].Value.ToString();
            textOperation.SelectionStart = textOperation.Text.Length;
            textOperation.SelectionLength = 0;
        }

        // Очистка списка истории
        private void очиститьСписокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List.Rows.Clear();
        }

        // Взятие результата
        private void взятьРезультатToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (List.Rows.Count > 0)
                GetResult(1);
        }

        private void List_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            GetResult(List.CurrentCellAddress.X == 0 ? 0 : 1);
        }

        // Взятие операции
        private void взятьОперациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (List.Rows.Count > 0)
            {
                GetResult(0);
            }
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            textOperation.Focus();
        }

        private void List_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p;
                GetCursorPos(out p);
                contextMenuStrip1.Show(p.X, p.Y);
            }
            textOperation.Focus();
        }

        private void toolStripMenu_InformationAbout_Click(object sender, EventArgs e)
        {
            new InfoForm().ShowDialog();
        }

        private void oolStripMenu_FileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Показ числа во всех системах счисления
        private void toolStripMenu_FileAllNotationResults_Click(object sender, EventArgs e)
        {
            CreateViewForm();
        }
    }
}