using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ApprendreLeFrançais
{
    public partial class TestForm : Form
    {
        private readonly ParametersEngine _parametersEngine;

        private Parameters _parameters;
        private bool _stopSavingParameters;

        private readonly Dictionary<int, KeyValuePair<Wordbook.WordName, string>> _words;
        private readonly string _toLanguageInfo;
        private int _cursorPosition;
        private int _wordNumber;

        public TestForm(Dictionary<int, KeyValuePair<Wordbook.WordName, string>> words, bool invert)
        {
            _stopSavingParameters = true;
            
            InitializeComponent();

            _words = words;
            _toLanguageInfo = invert ? "русский" : "французский";
            _wordNumber = -1;
            _parametersEngine = new ParametersEngine();
        }

        private void TestForm_Shown(object sender, EventArgs e)
        {
            _parameters = _parametersEngine.LoadTestFormParameters("539;367", "200;100", "130;130");
            this.Size = _parameters.FormSize;
            if (_parameters.FormLocation.X >= 0 && _parameters.FormLocation.Y >= 0 &&
                _parameters.FormLocation.X < Screen.FromControl(this).Bounds.Width - 10 && _parameters.FormLocation.Y < Screen.FromControl(this).Bounds.Height - 10)
            {
                this.Location = _parameters.FormLocation;
            }

            for (int i = 0; i < _parameters.ListColumnsWidth.Length; i++)
            {
                AnswerList.Columns[i].Width = _parameters.ListColumnsWidth[i];
            }

            AnswerList.Rows.Clear();
            ShowNextQuestion();

            _stopSavingParameters = false;
        }

        private void frenchLetters_PressLetterEvent(object sender, FrenchLetters.PressLetterEventArgs letter)
        {
            string newText = textBoxAnswer.Text.Substring(0, _cursorPosition) + letter.Text + textBoxAnswer.Text.Substring(_cursorPosition);
            textBoxAnswer.Text = newText;
            textBoxAnswer.Focus();
            textBoxAnswer.SelectionStart = _cursorPosition + 1;
            textBoxAnswer.SelectionLength = 0;
        }

        private void textBoxAnswer_Leave(object sender, EventArgs e)
        {
            _cursorPosition = ((TextBox)sender).SelectionStart;
        }

        private void ShowNextQuestion()
        {
            _wordNumber++;
            textBoxAnswer.Text = string.Empty;

            if (_wordNumber < _words.Count)
            {
                labelInfo.Text = string.Format("Переведите на {0} язык ({1} из {2}):", _toLanguageInfo, _wordNumber + 1, _words.Count);
                labelRussionWord.Text = _words[_wordNumber].Key.Name;
            }
            else 
            {
                labelInfo.Text = "Упражнение завершено";
                labelRussionWord.Text = string.Empty;
                buttonOk.Enabled = false;
                buttonRepeat.Visible = true;
            }

            textBoxAnswer.Focus();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_wordNumber == _words.Count)
            {
                return;
            }
            AnswerList.Rows.Add(labelRussionWord.Text, textBoxAnswer.Text, GetMarkForAnswerCorrectness());
            ShowNextQuestion();
        }

        private string GetMarkForAnswerCorrectness()
        {
            string dicAnswer = _words[_wordNumber].Value;

            if (dicAnswer.ToLowerInvariant().Trim(' ') == textBoxAnswer.Text.ToLowerInvariant().Trim(' '))
            {
                return "+";
            }

            string[] dicAnswerArr = dicAnswer.Split(',');
            var answerArr = new List<string>(textBoxAnswer.Text.Split(','));

            if(dicAnswerArr.Length != answerArr.Count)
            {
                return dicAnswer;
            }

            for (int i = 0; i < answerArr.Count; i++ )
            {
                answerArr[i] = answerArr[i].ToLowerInvariant().Trim(' ');
            }

            foreach (string oneDicAnswer in dicAnswerArr)
            {
                if (!answerArr.Contains(oneDicAnswer.ToLowerInvariant().Trim(' ')))
                {
                    return dicAnswer;
                }
            }

            return "+";
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (textBoxAnswer.Focused && keyData == Keys.Enter)
            {
                buttonOk_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void buttonRepeat_Click(object sender, EventArgs e)
        {
            buttonRepeat.Visible = false;
            buttonOk.Enabled = true;
            _wordNumber = -1;

            TestForm_Shown(null, null);
        }

        private void TestForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSavingParameters)
            {
                return;
            }

            _parameters.FormSize = this.Size;
            _parametersEngine.SaveTestFormParameters(_parameters);
        }

        private void TestForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSavingParameters || (this.Location.X <= 0 && this.Location.Y <= 0))
            {
                return;
            }

            _parameters.FormLocation = this.Location;
            _parametersEngine.SaveTestFormParameters(_parameters);
        }

        private void AnswerList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_stopSavingParameters)
            {
                return;
            }

            _parameters.ListColumnsWidth[0] = AnswerList.Columns[0].Width;
            _parameters.ListColumnsWidth[1] = AnswerList.Columns[1].Width;
            _parametersEngine.SaveTestFormParameters(_parameters);
        }
    }
}
