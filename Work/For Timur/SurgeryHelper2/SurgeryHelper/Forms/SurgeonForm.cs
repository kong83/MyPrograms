using System;
using System.Text;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using SurgeryHelper.Interfaces;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class SurgeonForm : Form
    {
        /// <summary>
        /// Переменная для предотвращения переполнения стека при изменении состояния item-ом
        /// </summary>
        private bool _stopChanging;

        private CWorkersKeeper _workersKeeper;
        private CSurgeonWorker _surgeonWorker;
        private readonly OperationViewForm _operationViewForm;
        private readonly IPutStringToSurgeonComboBoxForm _hospitalizationOrVisitViewForm;
        private readonly MainForm _mainForm;
        private readonly string _objectBoxNameOnForm;

        public SurgeonForm(
            CWorkersKeeper workersKeeper, OperationViewForm operationViewForm, string objectBoxNameOnForm)
        {
            Initialize(workersKeeper);
            _operationViewForm = operationViewForm;
            _objectBoxNameOnForm = objectBoxNameOnForm;
        }

        public SurgeonForm(CWorkersKeeper workersKeeper, IPutStringToSurgeonComboBoxForm hospitalizationOrVisitViewForm)
        {
            Initialize(workersKeeper);
            _hospitalizationOrVisitViewForm = hospitalizationOrVisitViewForm;
        }

        public SurgeonForm(CWorkersKeeper workersKeeper, MainForm mainForm)
        {
            Initialize(workersKeeper);
            _mainForm = mainForm;
            checkedListBoxSurgeons.CheckOnClick = false;
        }

        private void Initialize(CWorkersKeeper workersKeeper)
        {
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _surgeonWorker = _workersKeeper.SurgeonWorker;
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить нового хирурга", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выбранного хирурга", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Редактировать выбранного хирурга", buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            string text;
            if (_hospitalizationOrVisitViewForm != null)
            {
                text = "Подтвердить выбор хирурга";
            }
            else if (_mainForm != null)
            {
                text = "Закрыть окно";
            }
            else
            {
                text = "Подтвердить выбор хирургов";
            }

            CToolTipShower.Show(text, buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }
        #endregion

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
            while (listCnt < checkedListBoxSurgeons.Items.Count && orderlyCnt < _surgeonWorker.SurgeonList.Length)
            {
                checkedListBoxSurgeons.Items[listCnt] = _surgeonWorker.SurgeonList[orderlyCnt].Name;
                listCnt++;
                orderlyCnt++;
            }

            if (orderlyCnt == _surgeonWorker.SurgeonList.Length)
            {
                while (listCnt < checkedListBoxSurgeons.Items.Count)
                {
                    checkedListBoxSurgeons.Items.RemoveAt(listCnt);
                }
            }
            else
            {
                while (orderlyCnt < _surgeonWorker.SurgeonList.Length)
                {
                    checkedListBoxSurgeons.Items.Add(_surgeonWorker.SurgeonList[orderlyCnt].Name);
                    orderlyCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить нового хирурга
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            new SurgeonViewForm(_workersKeeper, null).ShowDialog();
            ShowSurgeons();
        }

        /// <summary>
        /// Удалить выделенного хирурга
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurgeons.SelectedIndices.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int currentNumber = checkedListBoxSurgeons.SelectedIndex;
                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить хирурга " + checkedListBoxSurgeons.Items[currentNumber] + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _surgeonWorker.Remove(_surgeonWorker.SurgeonList[currentNumber].Id);
                }

                ShowSurgeons();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактировать выделенного хирурга
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (checkedListBoxSurgeons.SelectedIndices.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new SurgeonViewForm(_workersKeeper, _surgeonWorker.SurgeonList[checkedListBoxSurgeons.SelectedIndices[0]]).ShowDialog();
            ShowSurgeons();
        }

        /// <summary>
        /// Отобразить на форме с операциями выбранных хирургов
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
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
            else if (_hospitalizationOrVisitViewForm != null)
            {
                _hospitalizationOrVisitViewForm.PutStringToSurgeonComboBox(orderlyMultilineStr.ToString());
            }

            Close();
        }


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            checkedListBoxSurgeons.Focus();
        }

        
        /// <summary>
        /// Запускаем таймер для убирания всех нужных галочек, которые появятся после завершения
        /// этого обработчика
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkedListBoxSurgeons_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            if (_stopChanging || (_hospitalizationOrVisitViewForm == null && _mainForm == null))
            {
                return;
            }

            _stopChanging = true;
            timerRemoveMarks.Enabled = true;
        }

        /// <summary>
        /// Убираем все лишние галочки, если выбираем только одного хирурга для формы 
        /// с госпитализацями или консультациями.
        /// Убираем все галочки, если открыли список хирургов из менюшки главного окна
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void timerRemoveMarks_Tick(object sender, EventArgs e)
        {
            int currentNumber = checkedListBoxSurgeons.SelectedIndex;

            for (int i = 0; i < checkedListBoxSurgeons.Items.Count; i++)
            {
                if (checkedListBoxSurgeons.GetItemCheckState(i) == CheckState.Checked &&
                    ((_hospitalizationOrVisitViewForm != null && i != currentNumber)))
                {
                    checkedListBoxSurgeons.SetItemCheckState(i, CheckState.Unchecked);
                }
            }

            _stopChanging = false;
        }
    }
}