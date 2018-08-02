using System.Windows.Forms;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class WaitForm : Form
    {
        private bool _isFormCloseCorrect;

        public WaitForm()
            : this("Идёт построение отчёта...")
        {
        }

        public WaitForm(string caption)
        {
            InitializeComponent();
            labelCaption.Text = caption;
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


        /// <summary>
        /// Запретить закрывать форму, пока мы её сами не закроем
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void WaitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormCloseCorrect)
            {
                MessageBox.Show("Ожидайте, идёт построение отчёта", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }
    }
}
