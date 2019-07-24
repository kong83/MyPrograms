using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class CureForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly PrescriptionForm _prescriptionForm;

        public CureForm(DbEngine dbEngine, PrescriptionForm prescriptionForm)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _prescriptionForm = prescriptionForm;
        }

        private void CureForm_Load(object sender, EventArgs e)
        {
            ShowCures();
        }

        /// <summary>
        /// Показать список лекарств
        /// </summary>
        private void ShowCures()
        {
            int listCnt = 0;
            int therapyCnt = 0;
            while (listCnt < CureList.Rows.Count && therapyCnt < _dbEngine.CureList.Count)
            {
                CureClass cureInfo = _dbEngine.CureList[therapyCnt];

                CureList.Rows[listCnt].Cells[0].Value = cureInfo.Name;
                CureList.Rows[listCnt].Cells[1].Value = cureInfo.DefaultPerDayCount;
                CureList.Rows[listCnt].Cells[2].Value = cureInfo.DefaultReceivingMethod;
                CureList.Rows[listCnt].Cells[3].Value = cureInfo.DefaultDuration;
                listCnt++;
                therapyCnt++;
            }

            if (therapyCnt == _dbEngine.CureList.Count)
            {
                while (listCnt < CureList.Rows.Count)
                {
                    CureList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (therapyCnt < _dbEngine.CureList.Count)
                {
                    CureClass cureInfo = _dbEngine.CureList[therapyCnt];

                    CureList.Rows.Add(new object[] { cureInfo.Name, cureInfo.DefaultPerDayCount, cureInfo.DefaultReceivingMethod, cureInfo.DefaultDuration });
                    therapyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить новое лекарство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new CureViewForm(_dbEngine, null).ShowDialog();
            ShowCures();
        }

        /// <summary>
        /// Удалить выделенное лекарство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int currentIndex = CureList.CurrentCellAddress.Y;
            if (currentIndex == -1)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string name = CureList.Rows[currentIndex].Cells[0].Value.ToString();
                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить лекарство '" + name + "'?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemoveCure(name);
                }

                ShowCures();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенное лекарство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int currentIndex = CureList.CurrentCellAddress.Y;
            if (currentIndex == -1)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new CureViewForm(_dbEngine, _dbEngine.CureList[currentIndex]).ShowDialog();
            ShowCures();
        }

        /// <summary>
        /// Отобразить на форме с назначениями выбранное лекарство
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            int currentIndex = CureList.CurrentCellAddress.Y;
            if (currentIndex == -1)
            {
                Close();
                return;
            }

            _prescriptionForm.PutStringToObject("comboBoxCure", CureList.Rows[currentIndex].Cells[0].Value.ToString());
            Close();
        }

        /// <summary>
        /// Выбор лекарства двойным кликом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void curesList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int currentIndex = CureList.CurrentCellAddress.Y;
            if (currentIndex != -1)
            {
                buttonOk_Click(null, null);
            }
        }

        /// <summary>
        /// Сдвиг выделенной строки с терапией на шаг вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTherapyUp_Click(object sender, EventArgs e)
        {
            int currentNumber = CureList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных строк", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentNumber == 0)
            {
                return;
            }

            var name = CureList.Rows[currentNumber].Cells[0].Value.ToString();
            _dbEngine.MoveCureUp(name);

            ShowCures();

            CureList.CurrentCell = CureList.Rows[currentNumber - 1].Cells[0];
        }

        /// <summary>
        ///  Сдвиг выделенной строки с терапией на шаг вниз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTherapyDown_Click(object sender, EventArgs e)
        {
            int currentNumber = CureList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных строк", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentNumber == CureList.Rows.Count - 1)
            {
                return;
            }

            var name = CureList.Rows[currentNumber].Cells[0].Value.ToString();
            _dbEngine.MoveCureDown(name);

            ShowCures();

            CureList.CurrentCell = CureList.Rows[currentNumber + 1].Cells[0];
        }

        /// <summary>
        /// Сдвинуть строку в начало списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMakeFirst_Click(object sender, EventArgs e)
        {
            int currentNumber = CureList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных строк", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentNumber == 0)
            {
                return;
            }

            var name = CureList.Rows[currentNumber].Cells[0].Value.ToString();
            _dbEngine.MoveCureToFirst(name);

            ShowCures();

            CureList.CurrentCell = CureList.Rows[0].Cells[0];
        }

        /// <summary>
        /// Сдвинуть строку в конец списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonmakeLast_Click(object sender, EventArgs e)
        {
            int currentNumber = CureList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных строк", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentNumber == CureList.Rows.Count - 1)
            {
                return;
            }

            var name = CureList.Rows[currentNumber].Cells[0].Value.ToString();
            _dbEngine.MoveCureToLast(name);

            ShowCures();

            CureList.CurrentCell = CureList.Rows[CureList.Rows.Count - 1].Cells[0];
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить новое лекарство", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выбранное лекарство", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выбранное лекарство", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить выбор лекарства", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonTherapyUp_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдвинуть выделенную строку на шаг вверх", buttonTherapyUp, 15, -20);
            buttonTherapyUp.FlatStyle = FlatStyle.Popup;
        }

        private void buttonTherapyUp_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonTherapyUp);
            buttonTherapyUp.FlatStyle = FlatStyle.Flat;
        }

        private void buttonTherapyDown_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдвинуть выделенную строку на шаг вниз", buttonTherapyDown, 15, -20);
            buttonTherapyDown.FlatStyle = FlatStyle.Popup;
        }

        private void buttonTherapyDown_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonTherapyDown);
            buttonTherapyDown.FlatStyle = FlatStyle.Flat;
        }

        private void buttonMakeFirst_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдвинуть строку в начало списка", buttonMakeFirst, 15, -20);
            buttonMakeFirst.FlatStyle = FlatStyle.Popup;
        }

        private void buttonMakeFirst_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonMakeFirst);
            buttonMakeFirst.FlatStyle = FlatStyle.Flat;
        }

        private void buttonmakeLast_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдвинуть строку в конец списка", buttonmakeLast, 15, -20);
            buttonmakeLast.FlatStyle = FlatStyle.Popup;
        }

        private void buttonmakeLast_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonmakeLast);
            buttonmakeLast.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
