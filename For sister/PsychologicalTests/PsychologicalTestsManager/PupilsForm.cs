using System;
using System.Drawing;
using System.Windows.Forms;
using PsychologicalTestsManager.Engines;
using PsychologicalTestsManager.Infos;
using TestResultMaker;

namespace PsychologicalTestsManager
{
    public partial class PupilsForm : Form
    {
        private readonly MainForm _mainForm;
        private readonly ClassForm _classForm;
        private readonly PupilEngine _pupilEngine;
        private readonly TestResultEngine _testResultEngine;
        private readonly AnxietyTestResultMaker _anxietyTestResultMaker;

        private readonly int _classId;
        private TestsForm _testsForm;
        
        public PupilsForm(MainForm mainForm, ClassForm classForm, AnxietyTestResultMaker anxietyTestResultMaker, int classId, 
            PupilEngine pupilEngine, TestResultEngine testResultEngine, string className)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _classForm = classForm;
            _anxietyTestResultMaker = anxietyTestResultMaker;
            _classId = classId;
            labelInfo.Text = "Класс: " + className;

            _pupilEngine = pupilEngine;
            _testResultEngine = testResultEngine;
        }

        private void PupilsForm_Shown(object sender, EventArgs e)
        {
            ShowPupils();
        }

        public void ShowPupils()
        {
            Color anxietyPupil = Color.FromArgb(255, 255, 180, 180);

            PupilsList.Rows.Clear();

            foreach (PupilInfo pupilInfo in _pupilEngine.GetPupilsForClass(_classId))
            {
                PupilsList.Rows.Add(new object[] { 
                    pupilInfo.Id, pupilInfo.FirstName, pupilInfo.LastName, _testResultEngine.GetTestsCountForPupil(pupilInfo.Id) });

                if (_testResultEngine.DoesPupilHasHighAnxiety(pupilInfo.Id))
                {
                    PupilsList.Rows[PupilsList.Rows.Count - 1].DefaultCellStyle.BackColor = anxietyPupil;
                }
            }
        }

        private void buttonTests_Click(object sender, EventArgs e)
        {
            int currentNumber = PupilsList.CurrentCellAddress.Y;

            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(PupilsList.Rows[currentNumber].Cells[0].Value);
            string name = string.Format("{0} {1}", PupilsList.Rows[currentNumber].Cells[1].Value, PupilsList.Rows[currentNumber].Cells[2].Value);

            ShowTestsForm(id, name);
        }

        private void ShowTestsForm(int pupilId, string name)
        {
            if (_testsForm != null && !_testsForm.IsDisposed)
            {
                _testsForm.Close();
            }

            _testsForm = new TestsForm(_classForm, this, _anxietyTestResultMaker, pupilId, _testResultEngine, name)
            {
                MdiParent = _mainForm,
                Location = new Point(840, 50)
            };
            _testsForm.Show();
        }

        private void PupilsList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonTests_Click(null, null);
        }

        private void PupilsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_testsForm != null && !_testsForm.IsDisposed)
            {
                _testsForm.Close();
            }            
        }
    }
}
