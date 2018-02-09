using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TestResultMaker;

namespace PsychologicalTests
{
    public partial class AnxietyTestForm : Form
    {
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private List<string> _questionList;
        private readonly bool?[] _answertList;
        private int _questionCount;
        private bool _automaticClosing;

        public AnxietyTestForm()
        {
            InitializeComponent();

            CreateQuestionList();
            _questionCount = 0;
            labelQuestion.Text = _questionList[0];
            _answertList = new bool?[_questionList.Count];

            buttonPrevious.Enabled = false;
            Text = Properties.Settings.Default.TestDisplayName;
            _automaticClosing = false;
        }

        private void CreateQuestionList()
        {
            _questionList = new List<string> 
            {
                "Трудно ли тебе держаться на одном уровне со всем классом?", 
                "Волнуешься ли ты, когда учитель говорит, что собирается проверить, насколько ты знаешь материал?",
                "Трудно ли тебе работать в классе так, как этого хочет учитель?",
                "Снится ли тебе временами, что учитель в ярости от того, что ты не знаешь урок?",
                "Случалось ли, что кто-нибудь из твоего класса бил или ударял тебя?",
                "Часто ли тебе хочется, чтобы учитель не торопился при объяснении нового материала, пока ты не поймешь, что он говорит?",
                "Сильно ли ты волнуешься при ответе или выполнении задания?",
                "Случается ли с тобой, что ты боишься высказываться на уроке, потому что боишься сделать глупую ошибку?",
                "Дрожат ли у тебя колени, когда тебя вызывают отвечать?",
                "Часто ли твои одноклассники смеются над тобой, когда вы играете в разные игры?",
                "Случается ли, что тебе ставят более низкую оценку, чем ты ожидал?",
                "Волнует ли тебя вопрос о том, не оставят ли тебя на второй год?",
                "Стараешься ли ты избегать игр, в которых делается выбор, потому что тебя, как правило, не выбирают?",
                "Бывает ли временами, что ты весь дрожишь, когда тебя вызывают отвечать?",
                "Часто ли у тебя возникает ощущение, что никто из твоих одноклассников не хочет делать то, чего хочешь ты?",
                "Сильно ли ты волнуешься перед тем, как начать выполнять задание?",
                "Трудно ли тебе получать такие отметки, каких ждут от тебя родители?",
                "Боишься ли ты временами, что тебе станет дурно в классе?",
                "Будут ли твои одноклассники смеяться над тобой, ли ты сделаешь ошибку при ответе?",
                "Похож ли ты на своих одноклассников?",
                "Выполнив задание, беспокоишься ли ты о том, хорошо ли с ним справился?",
                "Когда ты работаешь в классе, уверен ли ты в том, что все хорошо запомнишь?",
                "Снится ли тебе иногда, что ты в школе и не можешь ответить на вопрос учителя?",
                "Верно ли, что большинство ребят относится к тебе по-дружески?",
                "Работаешь ли ты более усердно, если знаешь, что результаты твоей работы будут сравниваться в классе с результатами твоих одноклассников?",
                "Часто ли ты мечтаешь о том, чтобы поменьше волноваться, когда тебя спрашивают?",
                "Боишься ли ты временами вступать в спор?",
                "Чувствуешь ли ты, что твое сердце начинает сильно биться, когда учитель говорит, что собирается проверить твою готовность к уроку?",
                "Когда ты получаешь хорошие отметки, думает ли кто-нибудь из твоих друзей, что ты хочешь выслужиться?",
                "Хорошо ли ты себя чувствуешь с теми из твоих одноклассников, к которым ребята относятся с особым вниманием?",
                "Бывает ли, что некоторые ребята в классе говорят что-то, что тебя задевает?",
                "Как ты думаешь, теряют ли расположение те из учеников, которые не справляются с учебой?",
                "Похоже ли на то, что большинство твоих одноклассников не обращают на тебя внимание?",
                "Часто ли ты боишься выглядеть нелепо?",
                "Доволен ли ты тем, как к тебе относятся учителя?",
                "Помогает ли твоя мама в организации вечеров, как другие мамы твоих одноклассников?",
                "Волновало ли тебя когда-нибудь, что думают о тебе окружающие?",
                "Надеешься ли ты в будущем учиться лучше, чем раньше?",
                "Считаешь ли ты, что одеваешься в школу так же хорошо, как и твои одноклассники?",
                "Часто ли ты задумываешься, отвечая на уроке, что думают о тебе в это время другие?",
                "Обладают ли способные ученики какими-то особыми правами, которых нет у других ребят в классе?",
                "Злятся ли некоторые из твоих одноклассников, когда тебе удается быть лучше их?",
                "Доволен ли ты тем, как к тебе относятся одноклассники?",
                "Хорошо ли ты себя чувствуешь, когда остаешься один на один с учителем?",
                "Высмеивают ли временами твои одноклассники твою внешность и поведение?",
                "Думаешь ли ты, что беспокоишься о своих школьных делах больше, чем другие ребята?",
                "Если ты не можешь ответить, когда тебя спрашивают, чувствуешь ли ты, что вот-вот расплачешься?",
                "Когда вечером ты лежишь в постели, думаешь ли ты временами с беспокойством о том, что будет завтра в школе?",
                "Работая над трудным заданием, чувствуешь ли ты порой, что совершенно забыл вещи, которые хорошо знал раньше?",
                "Дрожит ли слегка твоя рука, когда ты работаешь над заданием?",
                "Чувствуешь ли ты, что начинаешь нервничать, когда учитель говорит, что собирается дать классу задание?",
                "Пугает ли тебя проверка твоих знаний в школе?",
                "Когда учитель говорит, что собирается дать классу задание, чувствуешь ли ты страх, что не справишься с ним?",
                "Снилось ли тебе временами, что твои одноклассники могут сделать то, чего не можешь ты?",
                "Когда учитель объясняет материал, кажется ли тебе, что твои одноклассники понимают его лучше, чем ты?",
                "Беспокоишься ли ты по дороге в школу, что учитель может дать классу проверочную работу?",
                "Когда ты выполняешь задание, чувствуешь ли ты обычно, что делаешь это плохо?",
                "Дрожит ли слегка твоя рука, когда учитель просит сделать задание на доске перед всем классом?"
            };
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!radioButtonYes.Checked && !radioButtonNo.Checked)
            {
                MessageBox.Show("Для продолжениея теста необходимо указать ответ на вопрос.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _answertList[_questionCount] = radioButtonYes.Checked;
            _questionCount++;
            buttonPrevious.Enabled = true;

            if (_questionCount >= _questionList.Count)
            {
                SaveResults();
                _automaticClosing = true;
                Close();
                return;
            }

            labelQuestion.Text = _questionList[_questionCount];
            SetRadioButtons();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            _questionCount--;
            if (_questionCount == 0)
            {
                buttonPrevious.Enabled = false;
            }

            labelQuestion.Text = _questionList[_questionCount];
            SetRadioButtons();
        }

        private void SetRadioButtons()
        {
            labelQuestionNumberInfo.Text = string.Format("Вопрос {0} из {1}", _questionCount + 1, _questionList.Count);
            if (!_answertList[_questionCount].HasValue)
            {
                radioButtonYes.Checked = radioButtonNo.Checked = false;
            }
            else
            {
                radioButtonYes.Checked = _answertList[_questionCount].Value;
                radioButtonNo.Checked = !_answertList[_questionCount].Value;
            }
        }

        private void SaveResults()
        {
            var answersStr = new StringBuilder();
            foreach (bool answert in _answertList)
            {
                answersStr.AppendFormat("{0};", answert ? "1" : "0");
            }

            string resultFolderPath = Path.Combine(Program.TestGeneralInfo.TestSavePath, Properties.Settings.Default.TestName + "\\" + Program.TestGeneralInfo.ClassInfo);
            string resultFilePath = Path.Combine(resultFolderPath, string.Format("{0} {1}.txt", Program.TestGeneralInfo.LastName, Program.TestGeneralInfo.FirstName));

            if (!Directory.Exists(resultFolderPath))
            {
                Directory.CreateDirectory(resultFolderPath);
            }

            File.WriteAllText(resultFilePath, answersStr.ToString().TrimEnd(';'));

            Program.AnxietyResults = new AnxietyTestResultMaker().MakeResultsFromFile(resultFilePath);
        }

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Код клавиши</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonNext_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void AnxietyTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_automaticClosing)
            {
                return;
            }

            if (DialogResult.No == MessageBox.Show("Вы уверены, что хотите прервать тест? Результаты незавершённого теста не сохраняются.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
            }
        }
    }
}

