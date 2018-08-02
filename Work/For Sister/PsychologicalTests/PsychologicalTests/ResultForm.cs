using System;
using System.Windows.Forms;

namespace PsychologicalTests
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();

            dataGridViewResults.Rows.Clear();

            dataGridViewResults.Rows.Add(new object[] { "ОБЩАЯ ТРЕВОЖНОСТЬ", Program.AnxietyResults.GeneralAnxiety });
            dataGridViewResults.Rows.Add(new object[] { "Общая тревожность в школе", Program.AnxietyResults.GeneralAnxietyForScool });
            dataGridViewResults.Rows.Add(new object[] { "Переживание социального стресса", Program.AnxietyResults.ExperienceOfSocialStress });
            dataGridViewResults.Rows.Add(new object[] { "Фрустрация потребности в достижение успеха", Program.AnxietyResults.FrustrationNeedForSuccess });
            dataGridViewResults.Rows.Add(new object[] { "Страх самовыражения", Program.AnxietyResults.FearOfExpression });
            dataGridViewResults.Rows.Add(new object[] { "Страх ситуации проверки знаний", Program.AnxietyResults.FearSituationKnowledgeTest });
            dataGridViewResults.Rows.Add(new object[] { "Страх не соответствовать ожиданиям окружающих", Program.AnxietyResults.FearDoesNotMeetTheExpectationsOfOthers });
            dataGridViewResults.Rows.Add(new object[] { "Низкая физиологическая сопротивляемость стрессу", Program.AnxietyResults.LowPhysiologicalResistanceToStress });
            dataGridViewResults.Rows.Add(new object[] { "Проблемы и страхи в отношениях с учителями", Program.AnxietyResults.ProblemsAndFearsInRelationshipWithTeachers });
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
