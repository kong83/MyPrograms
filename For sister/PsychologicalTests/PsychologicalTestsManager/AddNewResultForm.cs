using System;
using System.Windows.Forms;

namespace PsychologicalTestsManager
{
    public partial class AddNewResultForm : Form
    {
        private readonly DatabaseEngine _databaseEngine;

        public AddNewResultForm(DatabaseEngine databaseEngine)
        {
            InitializeComponent();

            dateTimePickerPassingTime.Value = DateTime.Now;
            _databaseEngine = databaseEngine;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPath.Text))
            {
                folderBrowserDialog.SelectedPath = textBoxPath.Text;
            }

            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                textBoxPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPath.Text))
            {
                MessageBox.Show("Путь до папки не указан", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPath.Focus();
                return;
            }

            string testName = _databaseEngine.GetLastPathPart(textBoxPath.Text);

            if (testName != "Test_Phillipsa")
            {
                MessageBox.Show("Имя теста не опознано. Необходимо указать путь до папки с именем теста.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPath.Focus();
                return;
            }

            try
            {
                _databaseEngine.AddNewResults(textBoxPath.Text, dateTimePickerPassingTime.Value);
                MessageBox.Show("Новые данные успешно загружены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данные не были загружены, так как в процессе загрузки новых результатов произошла ошибка:\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
