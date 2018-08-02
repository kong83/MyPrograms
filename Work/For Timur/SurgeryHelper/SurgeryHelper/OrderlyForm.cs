using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class OrderlyForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly OperationViewForm _operationViewForm;

        public OrderlyForm(DbEngine dbEngine, OperationViewForm operationViewForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _operationViewForm = operationViewForm;
        }

        private void OrderlyForm_Load(object sender, EventArgs e)
        {
            ShowOrderlyes();
        }

        /// <summary>
        /// Показать список санитаров
        /// </summary>
        private void ShowOrderlyes()
        {
            int listCnt = 0;
            int orderlyCnt = 0;
            while (listCnt < checkedListBoxOrderlyes.Items.Count && orderlyCnt < _dbEngine.OrderlyList.Length)
            {
                checkedListBoxOrderlyes.Items[listCnt] = _dbEngine.OrderlyList[orderlyCnt].LastNameWithInitials;
                listCnt++;
                orderlyCnt++;
            }

            if (orderlyCnt == _dbEngine.OrderlyList.Length)
            {
                while (listCnt < checkedListBoxOrderlyes.Items.Count)
                {
                    checkedListBoxOrderlyes.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (orderlyCnt < _dbEngine.OrderlyList.Length)
                {
                    checkedListBoxOrderlyes.Items.Add(_dbEngine.OrderlyList[orderlyCnt].LastNameWithInitials);
                    orderlyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить нового санитара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new OrderlyViewForm(_dbEngine, null).ShowDialog();
            ShowOrderlyes();
        }

        /// <summary>
        /// Удалить выделенного санитара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxOrderlyes.SelectedIndex;
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить санитара " + checkedListBoxOrderlyes.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveOrderly(_dbEngine.OrderlyList[currentNumber].Id);
                }

                ShowOrderlyes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенного санитара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new OrderlyViewForm(_dbEngine, _dbEngine.OrderlyList[checkedListBoxOrderlyes.SelectedIndex]).ShowDialog();
            ShowOrderlyes();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранного санитара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedItems.Count == 0)
            {
                Close();
                return;
            }

            _operationViewForm.PutStringToObject("comboBoxOrderly", checkedListBoxOrderlyes.SelectedItem.ToString());
            Close();
        }

        /// <summary>
        /// Выбор санитара двойным кликом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxOrderlyes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedItems.Count != 0)
            {
                buttonOk_Click(null, null);
            }
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить нового санитара", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранного санитара", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранного санитара", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор санитара", buttonOk, 15, -20);
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
