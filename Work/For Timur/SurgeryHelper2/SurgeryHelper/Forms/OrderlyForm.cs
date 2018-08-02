using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class OrderlyForm : Form
    {
        private COrderlyWorker _orderlyWorker;
        private readonly OperationViewForm _operationViewForm;

        public OrderlyForm(CWorkersKeeper workersKeeper)
        {
            Initialize(workersKeeper);
        }

        public OrderlyForm(CWorkersKeeper workersKeeper, OperationViewForm operationViewForm)
        {
            Initialize(workersKeeper);

            _operationViewForm = operationViewForm;
        }

        private void Initialize(CWorkersKeeper workersKeeper)
        {
            InitializeComponent();

            _orderlyWorker = workersKeeper.OrderlyWorker;            
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
            while (listCnt < checkedListBoxOrderlyes.Items.Count && orderlyCnt < _orderlyWorker.OrderlyList.Length)
            {
                checkedListBoxOrderlyes.Items[listCnt] = _orderlyWorker.OrderlyList[orderlyCnt].Name;
                listCnt++;
                orderlyCnt++;
            }

            if (orderlyCnt == _orderlyWorker.OrderlyList.Length)
            {
                while (listCnt < checkedListBoxOrderlyes.Items.Count)
                {
                    checkedListBoxOrderlyes.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (orderlyCnt < _orderlyWorker.OrderlyList.Length)
                {
                    checkedListBoxOrderlyes.Items.Add(_orderlyWorker.OrderlyList[orderlyCnt].Name);
                    orderlyCnt++;
                }
            }
        }


        /// <summary>
        /// Добавить нового санитара
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new OrderlyViewForm(_orderlyWorker, null).ShowDialog();
            ShowOrderlyes();
        }


        /// <summary>
        /// Удалить выделенного санитара
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedIndices.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxOrderlyes.SelectedIndex;
                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить санитара " + checkedListBoxOrderlyes.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _orderlyWorker.Remove(_orderlyWorker.OrderlyList[currentNumber].Id);
                }

                ShowOrderlyes();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Редактировать выделенного санитара
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedIndices.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new OrderlyViewForm(_orderlyWorker, _orderlyWorker.OrderlyList[checkedListBoxOrderlyes.SelectedIndex]).ShowDialog();
            ShowOrderlyes();
        }


        /// <summary>
        /// Отобразить на форме с операциями выбранного санитара
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedItems.Count == 0)
            {
                Close();
                return;
            }

            if (_operationViewForm != null)
            {
                _operationViewForm.PutStringToObject("comboBoxOrderly", checkedListBoxOrderlyes.SelectedItem.ToString());
            }

            Close();
        }


        /// <summary>
        /// Выбор санитара двойным кликом
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkedListBoxOrderlyes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBoxOrderlyes.SelectedItems.Count != 0)
            {
                buttonOk_Click(null, null);
            }
        }


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            checkedListBoxOrderlyes.Focus();
        }


        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить нового санитара", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выбранного санитара", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Редактировать выбранного санитара", buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить выбор санитара", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
