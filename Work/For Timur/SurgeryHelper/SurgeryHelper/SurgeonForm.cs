using System;
using System.Text;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class SurgeonForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly OperationViewForm _operationViewForm;
        private readonly PatientViewForm _patientViewForm;
        private readonly string _objectBoxNameOnForm;

        public SurgeonForm(DbEngine dbEngine, OperationViewForm operationViewForm, string objectBoxNameOnForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _operationViewForm = operationViewForm;
            _objectBoxNameOnForm = objectBoxNameOnForm;
        }

        public SurgeonForm(DbEngine dbEngine, PatientViewForm patientViewForm, string objectBoxNameOnForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _patientViewForm = patientViewForm;
            _objectBoxNameOnForm = objectBoxNameOnForm;
        }

        private void SurgeonForm_Load(object sender, EventArgs e)
        {
            ShowSurgeons();
        }

        /// <summary>
        /// Показать список хирургов
        /// </summary>
        private void ShowSurgeons()
        {
            int listCnt = 0;
            int orderlyCnt = 0;
            while (listCnt < checkedListBoxSurgeons.Items.Count && orderlyCnt < _dbEngine.SurgeonList.Length)
            {
                checkedListBoxSurgeons.Items[listCnt] = _dbEngine.SurgeonList[orderlyCnt].LastNameWithInitials;
                listCnt++;
                orderlyCnt++;
            }

            if (orderlyCnt == _dbEngine.SurgeonList.Length)
            {
                while (listCnt < checkedListBoxSurgeons.Items.Count)
                {
                    checkedListBoxSurgeons.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (orderlyCnt < _dbEngine.SurgeonList.Length)
                {
                    checkedListBoxSurgeons.Items.Add(_dbEngine.SurgeonList[orderlyCnt].LastNameWithInitials);
                    orderlyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить нового хирурга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new SurgeonViewForm(_dbEngine, null).ShowDialog();
            ShowSurgeons();
        }

        /// <summary>
        /// Удалить выделенного хирурга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurgeons.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxSurgeons.SelectedIndex;
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить хирурга " + checkedListBoxSurgeons.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveSurgeon(_dbEngine.SurgeonList[currentNumber].Id);
                }

                ShowSurgeons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенного хирурга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurgeons.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new SurgeonViewForm(_dbEngine, _dbEngine.SurgeonList[checkedListBoxSurgeons.SelectedIndices[0]]).ShowDialog();
            ShowSurgeons();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранных хирургов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurgeons.CheckedItems.Count == 0)
            {
                Close();
                return;
            }

            var orderlyMultilineStr = new StringBuilder();
            for (int i = 0; i < checkedListBoxSurgeons.CheckedItems.Count; i++)
            {
                orderlyMultilineStr.Append(checkedListBoxSurgeons.CheckedItems[i] + "\r\n");
            }

            if (orderlyMultilineStr.Length > 2)
            {
                orderlyMultilineStr.Remove(orderlyMultilineStr.Length - 2, 2);
            }

            if (_operationViewForm != null)
            {
                _operationViewForm.PutStringToObject(_objectBoxNameOnForm, orderlyMultilineStr.ToString());
            }
            else
            {
                _patientViewForm.PutStringToObject(_objectBoxNameOnForm, orderlyMultilineStr.ToString());
            }

            Close();
        }

        /// <summary>
        /// Убираем все галочки, если выбираем только одного хирурга для формы с пациентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxSurgeons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_patientViewForm == null)
            {
                return;
            }

            int currentNumber = checkedListBoxSurgeons.SelectedIndex;

            for (int i = 0; i < checkedListBoxSurgeons.Items.Count; i++)
            {
                if (i != currentNumber)
                {
                    checkedListBoxSurgeons.SetItemCheckState(i, CheckState.Unchecked);
                }
            }            
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить нового хирурга", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранного хирурга", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранного хирурга", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор хирургов", buttonOk, 15, -20);
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