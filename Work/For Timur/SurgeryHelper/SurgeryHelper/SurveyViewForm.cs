using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class SurveyViewForm : Form
    {
        private readonly DbEngine _dbEngine;
        private bool _isFormClosingByButton;

        private readonly string _surveySaveName;

        public override sealed string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        public SurveyViewForm(DbEngine dbEngine, string surveyName)
        {
            InitializeComponent();

            _dbEngine = dbEngine;

            _surveySaveName = surveyName;
            if (surveyName == null)
            {
                Text = "Добавление нового обследования";
            }
            else
            {
                Text = "Редактирование обследования";
                textBoxSurveyName.Text = surveyName;
            }
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отменить", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSurveyName.Text))
            {
                MessageBox.Show("Поле должно быть заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_surveySaveName == null)
                {
                    if (_dbEngine.SurveyList.Contains(textBoxSurveyName.Text))
                    {
                        MessageBox.Show("Обследование с таким именем уже существует. Используйте другое имя.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (textBoxSurveyName.Text.Contains("&"))
                    {
                        MessageBox.Show("Использование символа '&' в имени запрещено т.к. может привести к внутренней ошибке программы. Используйте другой символ.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _dbEngine.AddSurvey(textBoxSurveyName.Text);
                }
                else
                {
                    if (_surveySaveName != textBoxSurveyName.Text && _dbEngine.SurveyList.Contains(textBoxSurveyName.Text))
                    {
                        MessageBox.Show("Обследование с таким именем уже существует. Используйте другое имя.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (textBoxSurveyName.Text.Contains("&"))
                    {
                        MessageBox.Show("Использование символа '&' в имени запрещено т.к. может привести к внутренней ошибке программы. Используйте другой символ.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _dbEngine.UpdateSurvey(_surveySaveName, textBoxSurveyName.Text);
                }

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonOk_Click(null, null);
                return true;
            }

            if (keyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void SurveyViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }
    }
}
