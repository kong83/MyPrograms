using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using PsychologicalTestsManager.Engines;
using PsychologicalTestsManager.Infos;
using TestResultMaker;

namespace PsychologicalTestsManager
{
    public partial class TestsForm : Form
    {
        private readonly ClassForm _classForm;
        private readonly PupilsForm _pupilsForm;
        private readonly TestResultEngine _testResultEngine;
        private readonly AnxietyTestResultMaker _anxietyTestResultMaker;

        private readonly int _pupilId;
        private string _originalNote;

        public TestsForm(ClassForm classForm, PupilsForm pupilsForm, AnxietyTestResultMaker anxietyTestResultMaker, int pupilId, TestResultEngine testResultEngine, string name)
        {
            InitializeComponent();

            _classForm = classForm;
            _pupilsForm = pupilsForm;
            _anxietyTestResultMaker = anxietyTestResultMaker;
            _testResultEngine = testResultEngine;
            _pupilId = pupilId;

            labelInfo.Text = "Ученик: " + name;
            printDocument1.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
        }

        private void TestsForm_Shown(object sender, EventArgs e)
        {
            TestsList.Rows.Clear();

            foreach (TestResultInfo testResultInfo in _testResultEngine.GetTestResultsForPupils(_pupilId))
            {
                TestsList.Rows.Add(new object[] { 
                    testResultInfo.Id, testResultInfo.Name, testResultInfo.PassingDate });
            }

            ShowTestResults();
        }

      
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            var printFont = new Font(Font.FontFamily, (float)14.0, FontStyle.Bold); 

            // Печать заголовка с информацией об ученике
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

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.Text = "Результаты теста ученика";
                printPreviewDialog1.Icon = Icon;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TestsList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowTestResults();
        }

        private void ShowTestResults()
        {
            dataGridViewResults.Rows.Clear();

            int currentNumber = TestsList.CurrentCellAddress.Y;

            if (currentNumber < 0)
            {
                return;
            }

            int id = Convert.ToInt32(TestsList.Rows[currentNumber].Cells[0].Value);
            TestResultInfo testResultInfo = _testResultEngine.GetTestResultById(id);

            AnxietyResults anxietyResults = _anxietyTestResultMaker.MakeResults(testResultInfo.Result);

            dataGridViewResults.Rows.Add(new object[] { "ОБЩАЯ ТРЕВОЖНОСТЬ", anxietyResults.GeneralAnxiety });
            dataGridViewResults.Rows.Add(new object[] { "Общая тревожность в школе", anxietyResults.GeneralAnxietyForScool });
            dataGridViewResults.Rows.Add(new object[] { "Переживание социального стресса", anxietyResults.ExperienceOfSocialStress });
            dataGridViewResults.Rows.Add(new object[] { "Фрустрация потребности в достижение успеха", anxietyResults.FrustrationNeedForSuccess });
            dataGridViewResults.Rows.Add(new object[] { "Страх самовыражения", anxietyResults.FearOfExpression });
            dataGridViewResults.Rows.Add(new object[] { "Страх ситуации проверки знаний", anxietyResults.FearSituationKnowledgeTest });
            dataGridViewResults.Rows.Add(new object[] { "Страх не соответствовать ожиданиям окружающих", anxietyResults.FearDoesNotMeetTheExpectationsOfOthers });
            dataGridViewResults.Rows.Add(new object[] { "Низкая физиологическая сопротивляемость стрессу", anxietyResults.LowPhysiologicalResistanceToStress });
            dataGridViewResults.Rows.Add(new object[] { "Проблемы и страхи в отношениях с учителями", anxietyResults.ProblemsAndFearsInRelationshipWithTeachers });

            _originalNote = testResultInfo.Note;
            richTextBoxNote.Text = testResultInfo.Note;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы уверены, что хотите удалить выделенный результат теста?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            try
            {
                int id = GetIdForSelectedRow();

                _testResultEngine.RemoveById(id);

                _pupilsForm.ShowPupils();
                _classForm.ShowClass();

                TestsForm_Shown(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetIdForSelectedRow()
        {
            int currentNumber = TestsList.CurrentCellAddress.Y;

            if (currentNumber < 0)
            {
                throw new Exception("Нет выделенных записей");
            }

            return Convert.ToInt32(TestsList.Rows[currentNumber].Cells[0].Value);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                int id = GetIdForSelectedRow();

                _testResultEngine.UpdateNote(id, richTextBoxNote.Text);

                _originalNote = richTextBoxNote.Text;
                buttonSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richTextBoxNote_TextChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = richTextBoxNote.Text != _originalNote;
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
    }
}
