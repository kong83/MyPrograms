using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class SheAnestethistForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly OperationViewForm _operationViewForm;

        public SheAnestethistForm(DbEngine dbEngine, OperationViewForm operationViewForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _operationViewForm = operationViewForm;
        }

        private void SheAnestethistForm_Load(object sender, EventArgs e)
        {
            ShowSheAnestethistes();
        }

        /// <summary>
        /// Показать список анестезисток
        /// </summary>
        private void ShowSheAnestethistes()
        {
            int listCnt = 0;
            int sheAnestethistCnt = 0;
            while (listCnt < checkedListBoxSheAnestethistes.Items.Count && sheAnestethistCnt < _dbEngine.SheAnestethistList.Length)
            {
                checkedListBoxSheAnestethistes.Items[listCnt] = _dbEngine.SheAnestethistList[sheAnestethistCnt].LastNameWithInitials;
                listCnt++;
                sheAnestethistCnt++;
            }

            if (sheAnestethistCnt == _dbEngine.SheAnestethistList.Length)
            {
                while (listCnt < checkedListBoxSheAnestethistes.Items.Count)
                {
                    checkedListBoxSheAnestethistes.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (sheAnestethistCnt < _dbEngine.SheAnestethistList.Length)
                {
                    checkedListBoxSheAnestethistes.Items.Add(_dbEngine.SheAnestethistList[sheAnestethistCnt].LastNameWithInitials);
                    sheAnestethistCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить новую анестезистку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new SheAnestethistViewForm(_dbEngine, null).ShowDialog();
            ShowSheAnestethistes();
        }

        /// <summary>
        /// Удалить выделенную анестезистку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSheAnestethistes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxSheAnestethistes.SelectedIndex;
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить анестезистку " + checkedListBoxSheAnestethistes.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveSheAnestethist(_dbEngine.SheAnestethistList[currentNumber].Id);
                }

                ShowSheAnestethistes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенную анестезистку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSheAnestethistes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new SheAnestethistViewForm(_dbEngine, _dbEngine.SheAnestethistList[checkedListBoxSheAnestethistes.SelectedIndex]).ShowDialog();
            ShowSheAnestethistes();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранную анестезистку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSheAnestethistes.SelectedItems.Count == 0)
            {
                Close();
                return;
            }

            _operationViewForm.PutStringToObject("comboBoxSheAnestethist", checkedListBoxSheAnestethistes.SelectedItem.ToString());
            Close();
        }

        /// <summary>
        /// Выбор анестезистку двойным кликом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxSheAnestethistes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBoxSheAnestethistes.SelectedItems.Count != 0)
            {
                buttonOk_Click(null, null);
            }
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить новую анестезистку", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранную анестезистку", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранную анестезистку", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор анестезистки", buttonOk, 15, -20);
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
