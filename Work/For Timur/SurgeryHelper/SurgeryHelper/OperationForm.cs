using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class OperationForm : Form
    {
        private bool _isFormClosingByButton;

        private readonly PatientViewForm _patientViewForm;
        private readonly PatientClass _patientInfo;
        private readonly DbEngine _dbEngine;
        private bool _stopSaveParameters;

        private OperationViewForm _addNewOperationForm;

        public override sealed string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        public OperationForm(PatientViewForm patientViewForm, DbEngine dbEngine, PatientClass patientInfo)
        {
            _stopSaveParameters = true;
            InitializeComponent();
            
            _patientInfo = patientInfo;
            _dbEngine = dbEngine;
            _patientViewForm = patientViewForm;

            Text = "Список операций для " + _patientInfo.GetFullName();
        }

        private void OperationForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.OperationFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.OperationFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.OperationFormLocation;
            }

            Size = _dbEngine.ConfigEngine.OperationFormSize;

            string[] widthsList = _dbEngine.ConfigEngine.OperationFormListWidths.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                OperationList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }            

            ShowOperations();

            _stopSaveParameters = false;
        }

        public void ShowOperations()
        {
            int listCnt = 0;
            int operationCnt = 0;
            while (listCnt < OperationList.Rows.Count && operationCnt < _patientInfo.Operations.Count)
            {
                OperationList.Rows[listCnt].Cells[0].Value = _patientInfo.Operations[operationCnt].Id.ToString();
                OperationList.Rows[listCnt].Cells[1].Value = ConvertEngine.GetRightDateString(_patientInfo.Operations[operationCnt].DataOfOperation) +
                                        " " + ConvertEngine.GetRightTimeString(_patientInfo.Operations[operationCnt].StartTimeOfOperation);
                OperationList.Rows[listCnt].Cells[2].Value = _patientInfo.Operations[operationCnt].Name;
                listCnt++;
                operationCnt++;
            }

            if (operationCnt == _patientInfo.Operations.Count)
            {
                while (listCnt < OperationList.Rows.Count)
                {
                    OperationList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (operationCnt < _patientInfo.Operations.Count)
                {
                    var param = new[] 
                    {
                        _patientInfo.Operations[operationCnt].Id.ToString(),
                        ConvertEngine.GetRightDateString(_patientInfo.Operations[operationCnt].DataOfOperation) +
                        " " + ConvertEngine.GetRightTimeString(_patientInfo.Operations[operationCnt].StartTimeOfOperation),
                        _patientInfo.Operations[operationCnt].Name
                    };
                    OperationList.Rows.Add(param);
                    operationCnt++;
                }
            }
        }

        /// <summary>
        /// Добавить новую операцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (_addNewOperationForm == null || _addNewOperationForm.IsDisposed)
            {
                _addNewOperationForm = new OperationViewForm(this, _dbEngine, null, _patientInfo) { MdiParent = MdiParent };
                _addNewOperationForm.Show();
            }
            else
            {
                _addNewOperationForm.Focus();
            }
        }

        /// <summary>
        /// Удалить выделенную операцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = OperationList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                OperationClass operationInfo = GetSelectedOperation();
                if (operationInfo.OpenedOperationViewForm != null && !operationInfo.OpenedOperationViewForm.IsDisposed)
                {
                    MessageBox.Show("Данная операция заблокирована для удаления,\r\nтак как она в данный момент редактируется.\r\nЗакройте окно просмотра информации по данной операции\r\nи попробуйте ещё раз.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operationInfo.OpenedOperationViewForm.Focus();
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить все данные об операции " + OperationList.Rows[currentNumber].Cells[1].Value + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _patientInfo.DeleteOperation(Convert.ToInt32(OperationList.Rows[currentNumber].Cells[0].Value));
                }

                ShowOperations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Получить выделенную операцию
        /// </summary>
        /// <returns></returns>
        private OperationClass GetSelectedOperation()
        {
            int currentNumber = OperationList.CurrentCellAddress.Y;
            int selectedId = Convert.ToInt32(OperationList.Rows[currentNumber].Cells[0].Value);

            foreach (OperationClass operationInfo in _patientInfo.Operations)
            {
                if (operationInfo.Id == selectedId)
                {
                    return operationInfo;
                }
            }

            throw new Exception("Внутренняя ошибка программы. Операция с id=" + id + " не найдена. Обратитесь к разработчику.");
        }
        

        /// <summary>
        /// Редактировать выделенную операцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonView_Click(object sender, EventArgs e)
        {
            int currentNumber = OperationList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OperationClass selectedOperation = GetSelectedOperation();
            if (selectedOperation.OpenedOperationViewForm == null || selectedOperation.OpenedOperationViewForm.IsDisposed)
            {
                selectedOperation.OpenedOperationViewForm = new OperationViewForm(this, _dbEngine, selectedOperation, _patientInfo) { MdiParent = MdiParent };
                selectedOperation.OpenedOperationViewForm.Show();
            }
            else
            {
                selectedOperation.OpenedOperationViewForm.Focus();
            }
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            bool isOperationViewFormOpened = false;
            var openedOperationForm = new OperationClass();
            foreach (OperationClass oc in _patientInfo.Operations)
            {
                if (oc.OpenedOperationViewForm != null && !oc.OpenedOperationViewForm.IsDisposed)
                {
                    openedOperationForm = oc;
                    isOperationViewFormOpened = true;
                    break;
                }
            }

            if (isOperationViewFormOpened)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете все формы \"Просмотр данных об операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                openedOperationForm.OpenedOperationViewForm.Focus();
                return;
            }

            if (_addNewOperationForm != null && !_addNewOperationForm.IsDisposed)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Добавление операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _addNewOperationForm.Focus();
                return;
            }

            bool isOperationProtocolFormOpened = false;            
            foreach (OperationClass oc in _patientInfo.Operations)
            {
                if (oc.OpenedOperationProtocolForm != null && !oc.OpenedOperationProtocolForm.IsDisposed)
                {
                    openedOperationForm = oc;
                    isOperationProtocolFormOpened = true;
                    break;
                }
            }

            if (isOperationProtocolFormOpened)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете формы \"Протокол операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                openedOperationForm.OpenedOperationProtocolForm.Focus();
                return;
            }

            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Открыть форму с протоколом операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProtocol_Click(object sender, EventArgs e)
        {
            if (OperationList.CurrentCellAddress.Y < 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OperationClass operationInfo = GetSelectedOperation();            
            if (operationInfo.OpenedOperationProtocolForm == null || operationInfo.OpenedOperationProtocolForm.IsDisposed)
            {
                operationInfo.OpenedOperationProtocolForm = new OperationProtocolForm(operationInfo, _patientInfo, _dbEngine) { MdiParent = MdiParent };
                operationInfo.OpenedOperationProtocolForm.Show();
            }
            else
            {
                operationInfo.OpenedOperationProtocolForm.Focus();
            }
        }

        /// <summary>
        /// Просмотр пациента при двойном клике по нему
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                buttonView_Click(null, null);
            }
        }

        #region Сохранение параметров формы
        private void OperationForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationFormLocation = Location;
        }

        private void OperationForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationFormSize = Size;
        }

        private void OperationList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < OperationList.ColumnCount; i++)
            {
                widths += OperationList.Columns[i].Width + ";";
            }

            _dbEngine.ConfigEngine.OperationFormListWidths = widths;
        }
        #endregion

        private void OperationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
                return;
            }

            _patientViewForm.SetOperationCount();
        }

        #region Подсказки
        private void buttonProtocol_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать протокол операции", buttonProtocol, 15, -20);
            buttonProtocol.FlatStyle = FlatStyle.Popup;
        }

        private void buttonProtocol_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonProtocol);
            buttonProtocol.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить операцию", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выделенную операцию", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonView_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Просмотреть данные по выбранной операции", buttonView, 15, -20);
            buttonView.FlatStyle = FlatStyle.Popup;
        }

        private void buttonView_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonView);
            buttonView.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Вернуться к окну с данными о пациенте", buttonOK, 15, -20);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOK);
            buttonOK.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
