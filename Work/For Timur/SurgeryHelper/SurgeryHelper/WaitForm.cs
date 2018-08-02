using System.Windows.Forms;

namespace SurgeryHelper
{
    public partial class WaitForm : Form
    {
        private bool _isFormCloseCorrect;

        public WaitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Установить прогресс для действия
        /// </summary>
        /// <param name="value">Значение для текущего прогресса</param>
        public void SetProgress(double value)
        {
            SetProgress((int)value);
        }

        /// <summary>
        /// Установить прогресс для действия
        /// </summary>
        /// <param name="value">Значение для текущего прогресса</param>
        public void SetProgress(int value)
        {
            progressBar1.Value = 100;
            progressBar1.Value = value < 100
                ? value
                : 100;
            Application.DoEvents();
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        public void CloseForm()
        {
            _isFormCloseCorrect = true;
            Close();
        }

        private void WaitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormCloseCorrect)
            {
                e.Cancel = true;
            }
        }
    }
}
