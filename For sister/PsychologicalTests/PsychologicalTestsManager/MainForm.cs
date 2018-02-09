using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PsychologicalTestsManager.Engines;
using TestResultMaker;

namespace PsychologicalTestsManager
{
    public partial class MainForm : Form
    {
        private ClassForm _classForm;
        private readonly DatabaseEngine _databaseEngine;
        private readonly AnxietyTestResultMaker _anxietyTestResultMaker;

        private readonly ClassEngine _classEngine;
        private readonly PupilEngine _pupilEngine;
        private readonly TestResultEngine _testResultEngine;

        public MainForm()
        {
            InitializeComponent();

            string dbFolderPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "Database");
            if (!Directory.Exists(dbFolderPath))
            {
                Directory.CreateDirectory(dbFolderPath);
            }

            _classEngine = new ClassEngine(dbFolderPath);
            _pupilEngine = new PupilEngine(dbFolderPath);
            _testResultEngine = new TestResultEngine(dbFolderPath);

            _anxietyTestResultMaker = new AnxietyTestResultMaker();
            _databaseEngine = new DatabaseEngine(_anxietyTestResultMaker, _classEngine, _pupilEngine, _testResultEngine);

        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _databaseEngine.LoadDatabase();

            ShowClassForm();
        }

        private void ShowClassForm()
        {
            if (_classForm != null && !_classForm.IsDisposed)
            {
                _classForm.Show();
                return;
            }

            _classForm = new ClassForm(this, _anxietyTestResultMaker, _classEngine, _pupilEngine, _testResultEngine)
            {
                MdiParent = this,
                Location = new Point(50, 50)
            };
            _classForm.Show();
        }

        private void menuItemLoadResults_Click(object sender, EventArgs e)
        {
            var addNewResultForm = new AddNewResultForm(_databaseEngine);
            addNewResultForm.ShowDialog();

            _classForm.Close();
            ShowClassForm();
        }

        private void menuItemShowClassList_Click(object sender, EventArgs e)
        {
            ShowClassForm();
        }

        private void menuItemTuneConfig_Click(object sender, EventArgs e)
        {
            new ConfigureConfigForm().ShowDialog();
        }
    }
}
