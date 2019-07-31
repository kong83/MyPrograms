using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class SurveyForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly PrescriptionForm _prescriptionForm;

        public SurveyForm(DbEngine dbEngine, PrescriptionForm prescriptionForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _prescriptionForm = prescriptionForm;
        }

        private void SurveyForm_Load(object sender, EventArgs e)
        {
            ShowSurveyes();
        }

        /// <summary>
        /// Показать список обследований
        /// </summary>
        private void ShowSurveyes()
        {
            int listCnt = 0;
            int surveyCnt = 0;
            while (listCnt < checkedListBoxSurveyes.Items.Count && surveyCnt < _dbEngine.SurveyList.Count)
            {
                checkedListBoxSurveyes.Items[listCnt] = _dbEngine.SurveyList[surveyCnt];
                listCnt++;
                surveyCnt++;
            }

            if (surveyCnt == _dbEngine.SurveyList.Count)
            {
                while (listCnt < checkedListBoxSurveyes.Items.Count)
                {
                    checkedListBoxSurveyes.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (surveyCnt < _dbEngine.SurveyList.Count)
                {
                    checkedListBoxSurveyes.Items.Add(_dbEngine.SurveyList[surveyCnt]);
                    surveyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить новое обследование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new SurveyViewForm(_dbEngine, null).ShowDialog();
            ShowSurveyes();
        }

        /// <summary>
        /// Удалить выделенное обследование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurveyes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxSurveyes.SelectedIndex;
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить обследование " + checkedListBoxSurveyes.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveSurvey(_dbEngine.SurveyList[currentNumber]);
                }

                ShowSurveyes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенное обследование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurveyes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new SurveyViewForm(_dbEngine, _dbEngine.SurveyList[checkedListBoxSurveyes.SelectedIndex]).ShowDialog();
            ShowSurveyes();
        }

        /// <summary>
        /// Отобразить на форме с назначениями выбранне обследование
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurveyes.SelectedItems.Count == 0)
            {
                Close();
                return;
            }
            
            var surveys = new List<string[]>();
            foreach (var item in checkedListBoxSurveyes.CheckedItems)
            {
                surveys.Add(new string[] { item.ToString(), ConvertEngine.GetRightDateString(dateTimePickerStartDate.Value) });
            }

            _prescriptionForm.PutSurveysToList(surveys);            
            Close();
        }

        /// <summary>
        /// Выбор обследования двойным кликом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxSurveyes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBoxSurveyes.SelectedItems.Count != 0)
            {
                buttonOk_Click(null, null);
            }
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить новое обследование", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранное обследование", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранное обследование", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор обследования", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
