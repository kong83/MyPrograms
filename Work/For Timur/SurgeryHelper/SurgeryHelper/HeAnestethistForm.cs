using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class HeAnestethistForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly OperationViewForm _operationViewForm;

        public HeAnestethistForm(DbEngine dbEngine, OperationViewForm operationViewForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _operationViewForm = operationViewForm;
        }

        private void HeAnestethistForm_Load(object sender, EventArgs e)
        {
            ShowHeAnestethistes();
        }

        /// <summary>
        /// Показать список анестезиологов
        /// </summary>
        private void ShowHeAnestethistes()
        {
            int listCnt = 0;
            int heAnestethistCnt = 0;
            while (listCnt < checkedListBoxHeAnestethistes.Items.Count && heAnestethistCnt < _dbEngine.HeAnestethistList.Length)
            {
                checkedListBoxHeAnestethistes.Items[listCnt] = _dbEngine.HeAnestethistList[heAnestethistCnt].LastNameWithInitials;
                listCnt++;
                heAnestethistCnt++;
            }

            if (heAnestethistCnt == _dbEngine.HeAnestethistList.Length)
            {
                while (listCnt < checkedListBoxHeAnestethistes.Items.Count)
                {
                    checkedListBoxHeAnestethistes.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (heAnestethistCnt < _dbEngine.HeAnestethistList.Length)
                {
                    checkedListBoxHeAnestethistes.Items.Add(_dbEngine.HeAnestethistList[heAnestethistCnt].LastNameWithInitials);
                    heAnestethistCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить нового анестезиолога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new HeAnestethistViewForm(_dbEngine, null).ShowDialog();
            ShowHeAnestethistes();
        }

        /// <summary>
        /// Удалить выделенного анестезиолога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxHeAnestethistes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxHeAnestethistes.SelectedIndex;
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить анестезиолога " + checkedListBoxHeAnestethistes.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveHeAnestethist(_dbEngine.HeAnestethistList[currentNumber].Id);
                }

                ShowHeAnestethistes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенного анестезиолога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxHeAnestethistes.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new HeAnestethistViewForm(_dbEngine, _dbEngine.HeAnestethistList[checkedListBoxHeAnestethistes.SelectedIndex]).ShowDialog();
            ShowHeAnestethistes();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранного анестезиолога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkedListBoxHeAnestethistes.SelectedItems.Count == 0)
            {
                Close();
                return;
            }

            _operationViewForm.PutStringToObject("comboBoxHeAnestethist", checkedListBoxHeAnestethistes.SelectedItem.ToString());
            Close();
        }

        /// <summary>
        /// Выбор анестезиолога двойным кликом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxHeAnestethistes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBoxHeAnestethistes.SelectedItems.Count != 0)
            {
                buttonOk_Click(null, null);
            }
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить нового анестезиолога", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранного анестезиолога", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранного анестезиолога", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор анестезиолога", buttonOk, 15, -20);
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
