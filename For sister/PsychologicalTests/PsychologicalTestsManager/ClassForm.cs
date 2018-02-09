using System;
using System.Drawing;
using System.Windows.Forms;
using PsychologicalTestsManager.Engines;
using PsychologicalTestsManager.Infos;
using TestResultMaker;

namespace PsychologicalTestsManager
{
    public partial class ClassForm : Form
    {
        private readonly MainForm _mainForm;
        private readonly ClassEngine _classEngine;
        private readonly PupilEngine _pupilEngine;
        private readonly TestResultEngine _testResultEngine;
        private readonly AnxietyTestResultMaker _anxietyTestResultMaker;

        private PupilsForm _pupilsForm;
        private ClassResultForm _classResultForm;

        public ClassForm(MainForm mainForm, AnxietyTestResultMaker anxietyTestResultMaker, ClassEngine classEngine, PupilEngine pupilEngine, TestResultEngine testResultEngine)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _anxietyTestResultMaker = anxietyTestResultMaker;
            _classEngine = classEngine;
            _pupilEngine = pupilEngine;
            _testResultEngine = testResultEngine;
        }

        private void ClassForm_Shown(object sender, EventArgs e)
        {
            ShowClass();
        }

        public void ShowClass()
        {
            ClassList.Rows.Clear();

            foreach (ClassInfo classInfo in _classEngine.ClassInfos)
            {
                ClassList.Rows.Add(new object[] { 
                    classInfo.Id, classInfo.Name, _pupilEngine.GetPupilCountInClass(classInfo.Id), _testResultEngine.GetPupilsWithHighAnxietyForClass(classInfo.Id) });
            }
        }

        private void ClassList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonPupils_Click(null, null);
        }

        private void ShowPupilsForm(int classId, string className)
        {
            if (_pupilsForm != null && !_pupilsForm.IsDisposed)
            {
                _pupilsForm.Close();
            }

            _pupilsForm = new PupilsForm(_mainForm, this, _anxietyTestResultMaker, classId, _pupilEngine, _testResultEngine, className)
            {
                MdiParent = _mainForm,
                Location = new Point(470, 50)
            };
            _pupilsForm.Show();
        }

        private void ClassForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pupilsForm != null && !_pupilsForm.IsDisposed)
            {
                _pupilsForm.Close();
            }

            if (_classResultForm != null && !_classResultForm.IsDisposed)
            {
                _classResultForm.Close();
            }
        }

        private void buttonPupils_Click(object sender, EventArgs e)
        {
            int currentNumber = ClassList.CurrentCellAddress.Y;

            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(ClassList.Rows[currentNumber].Cells[0].Value);
            string name = ClassList.Rows[currentNumber].Cells[1].Value.ToString();

            ShowPupilsForm(id, name);
        }

        private void buttonGraph_Click(object sender, EventArgs e)
        {
            int currentNumber = ClassList.CurrentCellAddress.Y;

            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(ClassList.Rows[currentNumber].Cells[0].Value);
            string name = ClassList.Rows[currentNumber].Cells[1].Value.ToString();

            if (_classResultForm != null && !_classResultForm.IsDisposed)
            {
                _classResultForm.Close();
            }

            _classResultForm = new ClassResultForm(_anxietyTestResultMaker, _classEngine, id, _testResultEngine)
            {
                MdiParent = _mainForm,
                Location = new Point(300, 400)
            };
            _classResultForm.Show();
        }
    }
}
