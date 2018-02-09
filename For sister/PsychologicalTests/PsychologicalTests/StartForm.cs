using System;
using System.IO;
using System.Windows.Forms;

namespace PsychologicalTests
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();

            labelInfo.Text = string.Format(
                "Вас приветствует программа прохождения психологических тестов.\r\n\r\nВам предстоит пройти тест: '{0}'.\r\n\r\nУкажите ваше имя, фамилию, класс и нажмите на кнопку 'Пройти тест'.",
                Properties.Settings.Default.TestDisplayName);

            comboBoxClassNumber.Text = "5";
            comboBoxClassAlfa.Text = "А";
        }

        private void buttonStartTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Имя не указано. Необходимо заполнить все поля перед началом прохождения теста.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                MessageBox.Show("Фамилия не указана. Необходимо заполнить все поля перед началом прохождения теста.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLastName.Focus();
                return;
            }            

            Program.TestGeneralInfo = new TestGeneralInfo(
                textBoxName.Text, textBoxLastName.Text, comboBoxClassNumber.Text + comboBoxClassAlfa.Text, Properties.Settings.Default.TestName, Properties.Settings.Default.TestSavePath);

            string resultFolderPath = Path.Combine(Program.TestGeneralInfo.TestSavePath, Properties.Settings.Default.TestName + "\\" + Program.TestGeneralInfo.ClassInfo);
            string resultFilePath = Path.Combine(resultFolderPath, string.Format("{0} {1}.txt", Program.TestGeneralInfo.LastName, Program.TestGeneralInfo.FirstName));

            if (File.Exists(resultFilePath))
            {
                if (DialogResult.No == MessageBox.Show("Программа обнаружила результат прохождения Вами данного теста. Хотите пройти тест ещё раз? Предыдущие результаты будут удалены.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    Program.TestGeneralInfo = null;
                    return;    
                }
            }

            Close();
        }
    }
}
