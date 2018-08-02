using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    public partial class ScrubNurseForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly OperationViewForm _operationViewForm;

        public ScrubNurseForm(DbEngine dbEngine, OperationViewForm operationViewForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _operationViewForm = operationViewForm;
        }


        private void ScrubNurseForm_Load(object sender, EventArgs e)
        {
            ShowScrubNurces();
        }

        /// <summary>
        /// Показать список операционных мед. сестёр
        /// </summary>
        private void ShowScrubNurces()
        {
            int listCnt = 0;
            int orderlyCnt = 0;
            while (listCnt < listBoxScrubNurce.Items.Count && orderlyCnt < _dbEngine.ScrubNurseList.Length)
            {
                listBoxScrubNurce.Items[listCnt] = _dbEngine.ScrubNurseList[orderlyCnt].LastNameWithInitials;
                listCnt++;
                orderlyCnt++;
            }

            if (orderlyCnt == _dbEngine.ScrubNurseList.Length)
            {
                while (listCnt < listBoxScrubNurce.Items.Count)
                {
                    listBoxScrubNurce.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (orderlyCnt < _dbEngine.ScrubNurseList.Length)
                {
                    listBoxScrubNurce.Items.Add(_dbEngine.ScrubNurseList[orderlyCnt].LastNameWithInitials);
                    orderlyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить новую операционную мед. сестру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new ScrubNurseViewForm(_dbEngine, null).ShowDialog();
            ShowScrubNurces();
        }

        /// <summary>
        /// Удалить выделенную операционную мед. сестру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxScrubNurce.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = listBoxScrubNurce.SelectedIndex;
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить операционную мед. сестру " + listBoxScrubNurce.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveScrubNurse(_dbEngine.ScrubNurseList[currentNumber].Id);
                }

                ShowScrubNurces();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенную операционную мед. сестру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxScrubNurce.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new ScrubNurseViewForm(_dbEngine, _dbEngine.ScrubNurseList[listBoxScrubNurce.SelectedIndex]).ShowDialog();
            ShowScrubNurces();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранной операционной мед. сестры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (listBoxScrubNurce.SelectedItems.Count == 0)
            {
                Close();
                return;
            }

            _operationViewForm.PutStringToObject("comboBoxScrubNurse", listBoxScrubNurce.SelectedItem.ToString());
            Close();
        }

        /// <summary>
        /// Выбор операционной мед. сестры двойным кликом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxScrubNurce_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxScrubNurce.SelectedItems.Count != 0)
            {
                buttonOk_Click(null, null);
            }
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить новую операционную мед. сестру", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранную операционную мед. сестру", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранную операционную мед. сестру", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор операционной мед. сестры", buttonOk, 15, -20);
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
