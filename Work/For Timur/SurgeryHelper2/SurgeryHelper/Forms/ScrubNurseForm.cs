using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class ScrubNurseForm : Form
    {
        private CWorkersKeeper _workersKeeper;
        private CScrubNurseWorker _scrubNurseWorker;
        private readonly OperationViewForm _operationViewForm;

        public ScrubNurseForm(CWorkersKeeper workersKeeper)
        {
            Initialize(workersKeeper);
        }

        public ScrubNurseForm(CWorkersKeeper workersKeeper, OperationViewForm operationViewForm)
        {
            Initialize(workersKeeper);

            _operationViewForm = operationViewForm;            
        }

        private void Initialize(CWorkersKeeper workersKeeper)
        {
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _scrubNurseWorker = workersKeeper.ScrubNurseWorker;
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
            while (listCnt < listBoxScrubNurce.Items.Count && orderlyCnt < _scrubNurseWorker.ScrubNurseList.Length)
            {
                listBoxScrubNurce.Items[listCnt] = _scrubNurseWorker.ScrubNurseList[orderlyCnt].Name;
                listCnt++;
                orderlyCnt++;
            }

            if (orderlyCnt == _scrubNurseWorker.ScrubNurseList.Length)
            {
                while (listCnt < listBoxScrubNurce.Items.Count)
                {
                    listBoxScrubNurce.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (orderlyCnt < _scrubNurseWorker.ScrubNurseList.Length)
                {
                    listBoxScrubNurce.Items.Add(_scrubNurseWorker.ScrubNurseList[orderlyCnt].Name);
                    orderlyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить новую операционную мед. сестру
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new ScrubNurseViewForm(_workersKeeper, null).ShowDialog();
            ShowScrubNurces();
        }

        /// <summary>
        /// Удалить выделенную операционную мед. сестру
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxScrubNurce.SelectedIndices.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = listBoxScrubNurce.SelectedIndex;
                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить операционную мед. сестру " + listBoxScrubNurce.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _scrubNurseWorker.Remove(_scrubNurseWorker.ScrubNurseList[currentNumber].Id);
                }

                ShowScrubNurces();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенную операционную мед. сестру
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxScrubNurce.SelectedIndices.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new ScrubNurseViewForm(_workersKeeper, _scrubNurseWorker.ScrubNurseList[listBoxScrubNurce.SelectedIndex]).ShowDialog();
            ShowScrubNurces();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранной операционной мед. сестры
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (listBoxScrubNurce.SelectedItems.Count == 0)
            {
                Close();
                return;
            }

            if (_operationViewForm != null)
            {
                _operationViewForm.PutStringToObject("comboBoxScrubNurse", listBoxScrubNurce.SelectedItem.ToString());
            }

            Close();
        }

        /// <summary>
        /// Выбор операционной мед. сестры двойным кликом
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
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
            CToolTipShower.Show("Добавить новую операционную мед. сестру", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выбранную операционную мед. сестру", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Редактировать выбранную операционную мед. сестру", buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить выбор операционной мед. сестры", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }
        #endregion   


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            listBoxScrubNurce.Focus();
        }
    }
}
