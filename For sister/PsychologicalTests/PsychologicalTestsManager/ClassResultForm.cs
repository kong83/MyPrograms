using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PsychologicalTestsManager.Engines;
using PsychologicalTestsManager.Infos;
using TestResultMaker;
using System.Drawing.Printing;
using System.Drawing;

namespace PsychologicalTestsManager
{
    public partial class ClassResultForm : Form
    {
        private readonly AnxietyTestResultMaker _anxietyTestResultMaker;
        private readonly ClassEngine _classEngine;
        private readonly TestResultEngine _testResultEngine;
        private readonly ClassInfo _classInfo;
        private string _originalNote;

        public ClassResultForm(AnxietyTestResultMaker anxietyTestResultMaker, ClassEngine classEngine, int classId, TestResultEngine testResultEngine)
        {
            InitializeComponent();

            _anxietyTestResultMaker = anxietyTestResultMaker;
            _classEngine = classEngine;
            _testResultEngine = testResultEngine;
            _classInfo = _classEngine.GetClassInfoById(classId);

            labelInfo.Text = "Класс: " + _classInfo.Name;
            printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
        }

        private void ShowTestResults()
        {
            double generalAnxiety = 0, generalAnxietyForScool = 0, experienceOfSocialStress = 0, frustrationNeedForSuccess = 0, fearOfExpression = 0,
                fearSituationKnowledgeTest = 0, fearDoesNotMeetTheExpectationsOfOthers = 0, lowPhysiologicalResistanceToStress = 0,
                problemsAndFearsInRelationshipWithTeachers = 0;

            List<TestResultInfo> testResultInfos = _testResultEngine.GetTestResultsForClass(_classInfo.Id, "Test_Phillipsa");

            foreach (TestResultInfo testResultInfo in testResultInfos)
            {
                AnxietyResults anxietyResults = _anxietyTestResultMaker.MakeResults(testResultInfo.Result);

                generalAnxiety += Convert.ToDouble(anxietyResults.GeneralAnxiety);
                generalAnxietyForScool += Convert.ToDouble(anxietyResults.GeneralAnxietyForScool);
                experienceOfSocialStress += Convert.ToDouble(anxietyResults.ExperienceOfSocialStress);
                frustrationNeedForSuccess += Convert.ToDouble(anxietyResults.FrustrationNeedForSuccess);
                fearOfExpression += Convert.ToDouble(anxietyResults.FearOfExpression);
                fearSituationKnowledgeTest += Convert.ToDouble(anxietyResults.FearSituationKnowledgeTest);
                fearDoesNotMeetTheExpectationsOfOthers += Convert.ToDouble(anxietyResults.FearDoesNotMeetTheExpectationsOfOthers);
                lowPhysiologicalResistanceToStress += Convert.ToDouble(anxietyResults.LowPhysiologicalResistanceToStress);
                problemsAndFearsInRelationshipWithTeachers += Convert.ToDouble(anxietyResults.ProblemsAndFearsInRelationshipWithTeachers);
            }

            dataGridViewResults.Rows.Clear();

            dataGridViewResults.Rows.Add(new object[] { "ОБЩАЯ ТРЕВОЖНОСТЬ", string.Format("{0:0.#}", generalAnxiety / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Общая тревожность в школе", string.Format("{0:0.#}", generalAnxietyForScool / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Переживание социального стресса", string.Format("{0:0.#}", experienceOfSocialStress / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Фрустрация потребности в достижение успеха", string.Format("{0:0.#}", frustrationNeedForSuccess / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Страх самовыражения", string.Format("{0:0.#}", fearOfExpression / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Страх ситуации проверки знаний", string.Format("{0:0.#}", fearSituationKnowledgeTest / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Страх не соответствовать ожиданиям окружающих", string.Format("{0:0.#}", fearDoesNotMeetTheExpectationsOfOthers / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Низкая физиологическая сопротивляемость стрессу", string.Format("{0:0.#}", lowPhysiologicalResistanceToStress / testResultInfos.Count) });
            dataGridViewResults.Rows.Add(new object[] { "Проблемы и страхи в отношениях с учителями", string.Format("{0:0.#}", problemsAndFearsInRelationshipWithTeachers / testResultInfos.Count) });

            _originalNote = _classInfo.Note;
            richTextBoxNote.Text = _classInfo.Note;
        }

        private void ClassResultForm_Shown(object sender, EventArgs e)
        {
            ShowTestResults();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            var printFont = new Font(Font.FontFamily, (float)14.0, FontStyle.Bold);

            // Печать заголовка с информацией о классе
            string header = labelInfo.Text;
            SizeF lineSize = ev.Graphics.MeasureString(header, printFont);
            ev.Graphics.DrawString(header, printFont, new SolidBrush(Color.Black), leftMargin + (int)((ev.MarginBounds.Width - lineSize.Width) / 2), topMargin + 20, new StringFormat());

            // Печать таблицы с результатом теста
            var pen = new Pen(Color.Black, 1);
            var brush = new SolidBrush(Color.Black);
            ev.Graphics.DrawRectangle(pen, 40, 100, 740, 250);
            ev.Graphics.DrawLine(pen, 600, 100, 600, 350);

            for (int i = 125; i < 350; i += 25)
            {
                ev.Graphics.DrawLine(pen, 40, i, 780, i);
            }

            // Заполнение таблицы записями
            printFont = new Font(Font.FontFamily, (float)12.0, FontStyle.Bold);
            float yPos = 102;
            ev.Graphics.DrawString("Название шкалы", printFont, brush, 220, yPos, new StringFormat());
            ev.Graphics.DrawString("Тревожность", printFont, brush, 630, yPos, new StringFormat());

            printFont = new Font(Font.FontFamily, (float)12.0);
            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                yPos += 25;
                ev.Graphics.DrawString(row.Cells[0].Value.ToString(), printFont, brush, 45, yPos, new StringFormat());
                ev.Graphics.DrawString(row.Cells[1].Value.ToString(), printFont, brush, 605, yPos, new StringFormat());
            }

            // Печать примечания
            if (string.IsNullOrEmpty(richTextBoxNote.Text))
            {
                return;
            }

            printFont = new Font(Font.FontFamily, (float)12.0, FontStyle.Bold);
            header = "Примечание";
            lineSize = ev.Graphics.MeasureString(header, printFont);
            ev.Graphics.DrawString(header, printFont, new SolidBrush(Color.Black), leftMargin + (int)((ev.MarginBounds.Width - lineSize.Width) / 2), 380, new StringFormat());

            printFont = new Font(Font.FontFamily, (float)12.0);
            int index = 19;
            foreach (string note in richTextBoxNote.Lines)
            {
                lineSize = ev.Graphics.MeasureString(note, printFont);
                int len = 1;
                int lenTextString = note.Length;
                if (lineSize.Width != 0)
                {
                    len = (int)(ev.MarginBounds.Width / lineSize.Width * lenTextString);
                }
                int step = 0;

                if (lenTextString == 0)
                {
                    lenTextString++;
                }

                while (step < lenTextString)
                {
                    yPos = topMargin + (index * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(
                        step + len >= lenTextString
                            ? note.Substring(step)
                            : note.Substring(step, len), printFont, brush, leftMargin,
                        yPos, new StringFormat());
                    index++;
                    step += len;
                }
            }

            ev.HasMorePages = false;
        }

        private void buttonTests_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.Text = "Результаты теста класса";
                printPreviewDialog1.Icon = Icon;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            string content = string.Empty;

            for (int i = 0; i < dataGridViewResults.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewResults.Rows[i].Cells.Count; j++)
                {
                    if (dataGridViewResults.Rows[i].Cells[j].Selected)
                    {
                        content += string.Format("{0}\t", dataGridViewResults.Rows[i].Cells[j].Value);
                    }
                }

                if (content.EndsWith("\t"))
                {
                    content = content.TrimEnd('\t') + "\r\n";
                }
            }

            Clipboard.SetText(content.TrimEnd(new[] { '\r', '\n' }));

            MessageBox.Show("Данные успешно скопированы в буфер обмена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void richTextBoxNote_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = richTextBoxNote.Text != _originalNote;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _classEngine.UpdateNote(_classInfo.Id, richTextBoxNote.Text);

                _originalNote = richTextBoxNote.Text;
                buttonSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
