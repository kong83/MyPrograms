using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

namespace StudyMath
{
    enum TypeExercises
    {
        Adding,
        Subtraction,
        Multiplication,
        Division
    }

    struct Settings
    {
        public TypeExercises TypeExercise;
        public int TimeAnswer;
        public int CountExercise;
        public int MinDigit;
        public int MaxDigit;
        public bool IsPositiveValueExpected;
    }

    struct RunInfo
    {
        public int Number;
        public string Exercise;
        public string Answer;
        public string RightAnswer;
    }

    public partial class Form1 : Form
    {
        Settings _settings;

        List<RunInfo> _runInfo;

        Random _random;
        bool _isStarted;
        int _sumSecond;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            linkLabelAll.Enabled = linkLabelNotRight.Enabled = 
                linkLabelRight.Enabled = textBoxAnswer.Enabled = false;
            comboBoxTimeAnswer.SelectedIndex = 2;
            comboBoxCountExercise.SelectedIndex = 1;
            textBoxDiapasonMin.Text = "0";
            textBoxDiapasonMax.Text = "20";
            checkBoxPositiveResult.Checked = true;

            _isStarted = false;            
        }

        /// <summary>
        /// Проверка границ диапазона на правильность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxDiapason_Validating(object sender, CancelEventArgs e)
        {
            var textBox = (TextBox)sender;

            try
            {
                int min = Convert.ToInt32(textBox.Text);
                if(min < -1000 || min > 1000)
                    throw new Exception();

                errorProvider1.SetError(textBox, "");
            }
            catch
            {
                errorProvider1.SetError(textBox, "Ожидается число от -1000 до 1000");
            }
        }


        /// <summary>
        /// Проверка времени ответа на правильность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTimeAnswer_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(comboBoxTimeAnswer.Text);
                if (temp < 1 || temp > 60)
                    throw new Exception();

                errorProvider1.SetError(comboBoxTimeAnswer, "");
            }
            catch
            {
                errorProvider1.SetError(comboBoxTimeAnswer, "Ожидается число от 1 до 60");
            }
        }


        /// <summary>
        /// Проверка количества вопросов на правильность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCountExercise_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int temp = Convert.ToInt32(comboBoxCountExercise.Text);
                if (temp < 1 || temp > 100)
                    throw new Exception();

                errorProvider1.SetError(comboBoxCountExercise, "");
            }
            catch
            {
                errorProvider1.SetError(comboBoxCountExercise, "Ожидается число от 1 до 100");
            }
        }

        /// <summary>
        /// Проверка на правильность введенных настроек
        /// </summary>
        /// <returns></returns>
        private bool CheckSettings()
        {
            if (string.IsNullOrEmpty(comboBoxType.Text))
            {
                MessageBox.Show("Не выбран тип заданий", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboBoxType.Focus();
                return false; 
            }

            try
            {
                int temp = Convert.ToInt32(comboBoxCountExercise.Text);
                if (temp < 1 || temp > 100)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Ошибка в записи количества заданий", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                comboBoxCountExercise.Focus();
                return false;
            }

            try
            {
                int temp = Convert.ToInt32(comboBoxTimeAnswer.Text);
                if (temp < 1 || temp > 60)
                    throw new Exception();                
            }
            catch
            {
                MessageBox.Show("Ошибка в записи времени на ответ", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboBoxTimeAnswer.Focus();
                return false;
            }

            int min;
            int max;
            try
            {                
                min = Convert.ToInt32(textBoxDiapasonMin.Text);
                if (min < -1000 || min > 1000)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Ошибка в записи минимального числа диапазона", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxDiapasonMin.Focus();
                return false;
            }

            try
            {
                max = Convert.ToInt32(textBoxDiapasonMax.Text);
                if (max < -1000 || max > 1000)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Ошибка в записи максимального числа диапазона", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxDiapasonMax.Focus();
                return false;
            }

            if (max <= min)
            {
                MessageBox.Show("Максимальное число диапазона должно быть больше минимального", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxDiapasonMin.Focus();
                return false;
            }

            if (checkBoxPositiveResult.Checked)
            {
                if (min < 0)
                {
                    MessageBox.Show("Ошибка в записи минимального числа диапазона.\r\nОтключите опцию \"Результат всегда больше 0\" или введите положительное число", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxDiapasonMin.Focus();
                    return false; 
                }
                if (max < 0)
                {
                    MessageBox.Show("Ошибка в записи максимального числа диапазона.\r\nОтключите опцию \"Результат всегда больше 0\" или введите положительное число", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxDiapasonMax.Focus();
                    return false;
                }
            }        

            return true;
        }
        

        /// <summary>
        /// Запуск/остановка выполнения упражнений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (!_isStarted)
            {
                if (!CheckSettings())
                    return;

                _settings = new Settings
                               {
                                   CountExercise = Convert.ToInt32(comboBoxCountExercise.Text),                                   
                                   TimeAnswer = Convert.ToInt32(comboBoxTimeAnswer.Text) * 1000,
                                   MinDigit = Convert.ToInt32(textBoxDiapasonMin.Text),
                                   MaxDigit = Convert.ToInt32(textBoxDiapasonMax.Text),
                                   IsPositiveValueExpected = checkBoxPositiveResult.Checked
                               };
                switch (comboBoxType.Text)
                { 
                    case "Сложение":
                        _settings.TypeExercise = TypeExercises.Adding;
                        break;
                    case "Вычитание":
                        _settings.TypeExercise = TypeExercises.Subtraction;
                        break;
                    case "Умножение":
                        _settings.TypeExercise = TypeExercises.Multiplication;
                        break;
                    case "Деление":
                        _settings.TypeExercise = TypeExercises.Division;
                        break;
                }

                _runInfo = new List<RunInfo>();
                _random = new Random();

                buttonStartStop.Text = "Стоп";
                GenerateNewExercise();
                textBoxAnswer.Enabled = true;
                _sumSecond = 0;
                labelRightInfo.Font = new Font(labelRightInfo.Font, FontStyle.Regular);
                labelNotRightInfo.Font = new Font(labelNotRightInfo.Font, FontStyle.Regular);
                labelAllInfo.Font = new Font(labelAllInfo.Font, FontStyle.Regular);
                progressBarTime.Maximum = _settings.TimeAnswer;
                dataViewInfo.Rows.Clear();
                labelEquals.Text = "=";
                labelRightInfo.Text = "0";
                labelNotRightInfo.Text = "0";
                labelAllInfo.Text = "0";
                textBoxAnswer.Focus();

                _isStarted = true;
                timer1.Enabled = true;
            }
            else
            {                
                timer1.Enabled = false;
                _isStarted = false;

                if (sender != null)
                {
                    _runInfo.RemoveAt(_runInfo.Count - 1);
                }

                buttonStartStop.Text = "Старт";
                textBoxAnswer.Enabled = false;
                textBoxAnswer.Text = "";
                _sumSecond = 0;
                progressBarTime.Value = 0;
                panelRightAnswerIcon.Visible = false;
                panelNotRightAnswerIcon.Visible = false;
                linkLabelAll.Enabled = linkLabelNotRight.Enabled = linkLabelRight.Enabled = true;
                labelCalculateFirst.Text = labelCalculateSecond.Text = labelEquals.Text = 
                    labelCalculateSign.Text = labelNumberInfo.Text = "";                

                linkLabelAll_LinkClicked(null, null);
            }
        }


        /// <summary>
        /// Генерация нового задания
        /// </summary>
        private void GenerateNewExercise()
        {
            string rightAnswer = "";
            do
            {
                labelCalculateFirst.Text = _random.Next(_settings.MinDigit, _settings.MaxDigit + 1).ToString();
                labelCalculateSecond.Text = _random.Next(_settings.MinDigit, _settings.MaxDigit + 1).ToString();


                switch (_settings.TypeExercise)
                {
                    case TypeExercises.Adding:
                        labelCalculateSign.Text = "+";
                        rightAnswer = (Convert.ToInt32(labelCalculateFirst.Text) +
                            Convert.ToInt32(labelCalculateSecond.Text)).ToString();
                        break;
                    case TypeExercises.Subtraction:
                        labelCalculateSign.Text = "-";
                        rightAnswer = (Convert.ToInt32(labelCalculateFirst.Text) -
                            Convert.ToInt32(labelCalculateSecond.Text)).ToString();
                        break;
                    case TypeExercises.Multiplication:
                        labelCalculateSign.Text = "*";
                        rightAnswer = (Convert.ToInt32(labelCalculateFirst.Text) *
                            Convert.ToInt32(labelCalculateSecond.Text)).ToString();
                        break;
                    case TypeExercises.Division:
                        labelCalculateSign.Text = "/";
                        if (labelCalculateSecond.Text != "0")                            
                        {
                            if (Convert.ToInt32(labelCalculateFirst.Text) % Convert.ToInt32(labelCalculateSecond.Text) == 0)
                            {
                                rightAnswer = (Convert.ToInt32(labelCalculateFirst.Text) /
                                    Convert.ToInt32(labelCalculateSecond.Text)).ToString();
                            }
                            else
                            {
                                rightAnswer = "-1";
                            }
                        }
                        break;
                }
            }
            while (_settings.IsPositiveValueExpected && rightAnswer != "" &&
                Convert.ToInt32(rightAnswer) < 0);

            var runInfo = new RunInfo
            {               
                Exercise = labelCalculateFirst.Text + " " +
                           labelCalculateSign.Text + " " + labelCalculateSecond.Text,
                RightAnswer = rightAnswer
            };

            if (_runInfo.Count == 0)
            {
                runInfo.Number = 1;
            }
            else
            {
                runInfo.Number = _runInfo.Count + 1;                
            }

            labelNumberInfo.Text = runInfo.Number + " из " + _settings.CountExercise;
            _runInfo.Add(runInfo);
        }


        /// <summary>
        /// Срабатывание таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            _sumSecond += timer1.Interval;
            progressBarTime.Value = _sumSecond;            

            if (_sumSecond == _settings.TimeAnswer)
            {
                _sumSecond = 0;
                if (_runInfo[_runInfo.Count - 1].RightAnswer == textBoxAnswer.Text.Trim())
                {
                    panelRightAnswerIcon.Visible = true;
                    panelNotRightAnswerIcon.Visible = false;
                }
                else
                {
                    panelRightAnswerIcon.Visible = false;
                    panelNotRightAnswerIcon.Visible = true;
                }
                Application.DoEvents();
                Thread.Sleep(500);

                panelRightAnswerIcon.Visible = false;
                panelNotRightAnswerIcon.Visible = false;

                RunInfo runInfo = _runInfo[_runInfo.Count - 1];
                runInfo.Answer = textBoxAnswer.Text;

                labelAllInfo.Text = (Convert.ToInt32(labelAllInfo.Text) + 1).ToString();
                if (runInfo.Answer == runInfo.RightAnswer)
                {
                    labelRightInfo.Text = (Convert.ToInt32(labelRightInfo.Text) + 1).ToString();
                }
                else
                {
                    labelNotRightInfo.Text = (Convert.ToInt32(labelNotRightInfo.Text) + 1).ToString();
                }

                textBoxAnswer.Text = "";
                
                progressBarTime.Value = 0;

                _runInfo.RemoveAt(_runInfo.Count - 1);
                _runInfo.Add(runInfo);

                if (runInfo.Number >= _settings.CountExercise)
                {
                    MessageBox.Show("Выполнение задания завершено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    buttonStartStop_Click(null, null);
                    return;
                }
                GenerateNewExercise();
                
            }
            timer1.Enabled = true;
        }


        /// <summary>
        /// Показ информации о правильных ответах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            labelRightInfo.Font = new Font(labelRightInfo.Font, FontStyle.Bold);
            labelNotRightInfo.Font = new Font(labelNotRightInfo.Font, FontStyle.Regular);
            labelAllInfo.Font = new Font(labelAllInfo.Font, FontStyle.Regular);

            dataViewInfo.Rows.Clear();

            foreach (RunInfo runInfo in _runInfo)
            {
                if (runInfo.RightAnswer == runInfo.Answer)
                {
                    var values = new string[4];
                    values[0] = runInfo.Number.ToString();
                    values[1] = runInfo.Exercise;
                    values[2] = runInfo.Answer;
                    values[3] = runInfo.RightAnswer;
                    dataViewInfo.Rows.Add(values);
                    dataViewInfo.Rows[dataViewInfo.Rows.Count - 1].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }


        /// <summary>
        /// Показ информации о неправильных ответах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelNotRight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            labelRightInfo.Font = new Font(labelRightInfo.Font, FontStyle.Regular);
            labelNotRightInfo.Font = new Font(labelNotRightInfo.Font, FontStyle.Bold);
            labelAllInfo.Font = new Font(labelAllInfo.Font, FontStyle.Regular);
           
            dataViewInfo.Rows.Clear();

            foreach (RunInfo runInfo in _runInfo)
            {
                if (runInfo.RightAnswer != runInfo.Answer)
                {
                    var values = new string[4];
                    values[0] = runInfo.Number.ToString();
                    values[1] = runInfo.Exercise;
                    values[2] = runInfo.Answer;
                    values[3] = runInfo.RightAnswer;
                    dataViewInfo.Rows.Add(values);
                    dataViewInfo.Rows[dataViewInfo.Rows.Count - 1].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }


        /// <summary>
        /// Показ информации обо всех ответах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            labelRightInfo.Font = new Font(labelRightInfo.Font, FontStyle.Regular);
            labelNotRightInfo.Font = new Font(labelNotRightInfo.Font, FontStyle.Regular);
            labelAllInfo.Font = new Font(labelAllInfo.Font, FontStyle.Bold);

            dataViewInfo.Rows.Clear();

            foreach (RunInfo runInfo in _runInfo)
            {
                var values = new string[4];
                values[0] = runInfo.Number.ToString();
                values[1] = runInfo.Exercise;
                values[2] = runInfo.Answer;
                values[3] = runInfo.RightAnswer;
                dataViewInfo.Rows.Add(values);

                if (runInfo.RightAnswer != runInfo.Answer)
                {
                    dataViewInfo.Rows[dataViewInfo.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void textBoxAnswer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _sumSecond = _settings.TimeAnswer - timer1.Interval;
            }
        }
       
    }
}
