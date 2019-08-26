using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class OperationViewForm : Form
    {
        private readonly CWorkersKeeper _workersKeeper;
        private readonly COperationWorker _operationWorker;
        private readonly CScrubNurseWorker _scrubNurseWorker;
        private readonly COrderlyWorker _orderlyWorker;

        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalizationInfo;
        private COperation _operationInfo;
        private readonly COperation _saveOperationInfo;

        private readonly HospitalizationViewForm _hospitalizationViewForm;

        private bool _stopSaveParameters;
        private bool _isFormClosingByButton;
        private bool _isNeedSaveData;
        private readonly CConfigurationEngine _configurationEngine;
        private readonly AddUpdate _action;

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

        public OperationViewForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            COperation operationInfo,
            HospitalizationViewForm hospitalizationViewForm,
            AddUpdate action)
        {
            _stopSaveParameters = true;
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _operationWorker = workersKeeper.OperationWorker;
            _orderlyWorker = workersKeeper.OrderlyWorker;
            _scrubNurseWorker = workersKeeper.ScrubNurseWorker;

            _patientInfo = patientInfo;
            _hospitalizationInfo = hospitalizationInfo;
            _hospitalizationViewForm = hospitalizationViewForm;

            _configurationEngine = workersKeeper.ConfigurationEngine;

            PutObjectsToComboBox(_orderlyWorker.OrderlyList, comboBoxOrderly);
            PutObjectsToComboBox(_scrubNurseWorker.ScrubNurseList, comboBoxScrubNurse);

            _action = action;
            _operationInfo = operationInfo;
            _saveOperationInfo = new COperation(_operationInfo);

            textBoxName.Text = _operationInfo.Name;
            textBoxAssistents.Text = ListToMultilineString(_operationInfo.Assistents);
            textBoxSurgeons.Text = ListToMultilineString(_operationInfo.Surgeons);
            textBoxOperationTypes.Text = ListToMultilineString(_operationInfo.OperationTypes);
            comboBoxScrubNurse.Text = _operationInfo.ScrubNurse;
            textBoxSheAnestethist.Text = _operationInfo.SheAnaesthetist;
            textBoxHeAnestethist.Text = _operationInfo.HeAnaesthetist;
            comboBoxOrderly.Text = _operationInfo.Orderly;

            dateTimePickerDateOfOperation.Value = _operationInfo.DateOfOperation;
            dateTimePickerStartTimeOfOperation.Value = _operationInfo.StartTimeOfOperation;
            dateTimePickerEndTimeOfOperation.Checked = _operationInfo.EndTimeOfOperation.HasValue;
            if (_operationInfo.EndTimeOfOperation.HasValue)
            {
                dateTimePickerEndTimeOfOperation.Value = _operationInfo.EndTimeOfOperation.Value;
            }

            Text = action == AddUpdate.Add
                ? "Добавление операции" 
                : "Просмотр данных об операции";
        }


        private void OperationViewForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.OperationViewFormLocation.X >= 0 &&
                _configurationEngine.OperationViewFormLocation.Y >= 0)
            {
                Location = _configurationEngine.OperationViewFormLocation;
            }

            _stopSaveParameters = false;
        }


        /// <summary>
        /// Конвертируем список из строк в строку из нескольких строк
        /// </summary>
        /// <param name="list">Список из строк</param>
        /// <returns></returns>
        private static string ListToMultilineString(IEnumerable<string> list)
        {
            var multilineStr = new StringBuilder();
            foreach (string str in list)
            {
                multilineStr.Append(str + "\r\n");
            }

            if (multilineStr.Length > 2)
            {
                multilineStr.Remove(multilineStr.Length - 2, 2);
            }

            return multilineStr.ToString();
        }


        /// <summary>
        /// Конвертируем строку из нескольких строк в список строк
        /// </summary>
        /// <param name="multilineStr">Строка из нескольких строк</param>
        /// <returns></returns>
        private static List<string> MultilineStringToList(string multilineStr)
        {
            string[] arrOfStrings = multilineStr.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return new List<string>(arrOfStrings);
        }

        
        /// <summary>
        /// Поместить список хирургов в комбобокс с лечащим врачом
        /// </summary>
        /// <param name="medicalList">Список хирургов</param>
        /// <param name="comboBox">Комбобокс со списком хирургов</param>
        private static void PutObjectsToComboBox(IEnumerable<CBaseMedical> medicalList, ComboBox comboBox)
        {
            comboBox.Items.Clear();

            foreach (CBaseMedical medicalInfo in medicalList)
            {
                comboBox.Items.Add(medicalInfo.Name);
            }
        }


        /// <summary>
        /// Поместить строку в указанный объект
        /// </summary>
        /// <param name="objectName">Название объекта, куда класть текст</param>
        /// <param name="str">Текст, который надо положить в объект</param>
        public void PutStringToObject(string objectName, string str)
        {
            switch (objectName)
            {
                case "comboBoxOrderly":
                    comboBoxOrderly.Text = str;
                    break;
                case "comboBoxScrubNurse":
                    comboBoxScrubNurse.Text = str;
                    break;
                case "textBoxSurgeons":
                    if (!string.IsNullOrEmpty(textBoxSurgeons.Text))
                    {
                        if (!textBoxSurgeons.Text.EndsWith("\r\n"))
                        {
                            textBoxSurgeons.Text += "\r\n";
                        }

                        textBoxSurgeons.Text += str;
                    }
                    else
                    {
                        textBoxSurgeons.Text = str;
                    }

                    textBoxSurgeons.Text = CConvertEngine.RemoveDuplicates(textBoxSurgeons.Text);
                    break;
                case "textBoxAssistents":
                    if (!string.IsNullOrEmpty(textBoxAssistents.Text))
                    {
                        if (!textBoxAssistents.Text.EndsWith("\r\n"))
                        {
                            textBoxAssistents.Text += "\r\n";
                        }

                        textBoxAssistents.Text += str;
                    }
                    else
                    {
                        textBoxAssistents.Text = str;
                    }

                    textBoxAssistents.Text = CConvertEngine.RemoveDuplicates(textBoxAssistents.Text);
                    break;
                case "textBoxOperationTypes":
                    textBoxOperationTypes.Text = str;
                    break;
            }
        }


        /// <summary>
        /// Открыть список с хирургами для заполнения списка хирургов
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelSurgeonList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_workersKeeper, this, "textBoxSurgeons").ShowDialog();
        }


        /// <summary>
        /// Открыть список с хирургами для заполнения списка ассистентов
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelAssistentList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_workersKeeper, this, "textBoxAssistents").ShowDialog();
        }


        /// <summary>
        /// Открыть список с операционными мед. сёстрами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelScrubNurseList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ScrubNurseForm(_workersKeeper, this).ShowDialog();
            PutObjectsToComboBox(_scrubNurseWorker.ScrubNurseList, comboBoxScrubNurse);
        }


        /// <summary>
        /// Открыть список с санитарами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelOrderlyList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new OrderlyForm(_workersKeeper, this).ShowDialog();
            PutObjectsToComboBox(_orderlyWorker.OrderlyList, comboBoxOrderly);
        }


        /// <summary>
        /// Открыть список с типами операций
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelOperationType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new OperationTypeForm(_workersKeeper, this, textBoxOperationTypes.Text).ShowDialog();
        }


        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            _isNeedSaveData = true;
            Close();
        }


        /// <summary>
        /// Закрыть форму без сохранения изменений
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            _isNeedSaveData = false;
            Close();
        }


        /// <summary>
        /// Положить введённые данные в операцию
        /// </summary>        
        private void PutDataToOperation()
        {
            _operationInfo.Name = textBoxName.Text;
            _operationInfo.Assistents = MultilineStringToList(textBoxAssistents.Text);
            _operationInfo.Surgeons = MultilineStringToList(textBoxSurgeons.Text);
            _operationInfo.OperationTypes = MultilineStringToList(textBoxOperationTypes.Text);
            _operationInfo.ScrubNurse = comboBoxScrubNurse.Text;
            _operationInfo.SheAnaesthetist = textBoxSheAnestethist.Text;
            _operationInfo.HeAnaesthetist = textBoxHeAnestethist.Text;
            _operationInfo.Orderly = comboBoxOrderly.Text;

            _operationInfo.DateOfOperation = dateTimePickerDateOfOperation.Value;
            _operationInfo.StartTimeOfOperation = dateTimePickerStartTimeOfOperation.Value;
            if (dateTimePickerEndTimeOfOperation.Checked)
            {
                _operationInfo.EndTimeOfOperation = dateTimePickerEndTimeOfOperation.Value;
            }
            else
            {
                _operationInfo.EndTimeOfOperation = null;
            }
        }


        /// <summary>
        /// Проверка, есть ли незаполненные поля, который надо заполнять
        /// </summary>
        /// <returns></returns>
        private bool IsFormHasEmptyNeededFields()
        {
            return string.IsNullOrEmpty(textBoxName.Text) ||
                   string.IsNullOrEmpty(textBoxSurgeons.Text) ||
                   string.IsNullOrEmpty(comboBoxScrubNurse.Text) ||
                   string.IsNullOrEmpty(comboBoxOrderly.Text);
        }


        /// <summary>
        /// Открыть форму с протоколом операции
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonProtocol_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_operationInfo.OpenedOperationProtocolForm == null || _operationInfo.OpenedOperationProtocolForm.IsDisposed)
            {
                _operationInfo.OpenedOperationProtocolForm = new OperationProtocolForm(
                    _workersKeeper,
                    _patientInfo,
                    _hospitalizationInfo,
                    _operationInfo,
                    _workersKeeper.OperationProtocolWorker.GetByOperationId(_operationInfo.Id))
                {
                    MdiParent = MdiParent
                };
                _operationInfo.OpenedOperationProtocolForm.Show();
            }
            else
            {
                _operationInfo.OpenedOperationProtocolForm.Focus();
            }
        }


        /// <summary>
        /// Сохранение изменений при закрытие формы, если надо
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void OperationViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                DialogResult dialogResult = MessageBox.ShowDialog("Вы хотите сохранить изменения?", "Закрытие окна", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    _isNeedSaveData = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    _isNeedSaveData = false;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

            _isFormClosingByButton = false;

            // Если все проверки при закрытии формы пройдены и форму закрыли с сохранением данных - 
            // то сохраняем данные
            if (_isNeedSaveData)
            {
                _isNeedSaveData = false;

                if (IsFormHasEmptyNeededFields())
                {
                    MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                if (_operationInfo.OpenedOperationProtocolForm != null &&
                    !_operationInfo.OpenedOperationProtocolForm.IsDisposed)
                {
                    MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Протокол операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _operationInfo.OpenedOperationProtocolForm.Focus();
                    e.Cancel = true;
                    return;
                }

                try
                {
                    _operationWorker.Update(_operationInfo);

                    _hospitalizationViewForm.ShowOperations();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                try
                {
                    if (_action == AddUpdate.Add)
                    {
                        _operationWorker.Remove(_operationInfo.Id);
                    }
                    else
                    {
                        _operationInfo = new COperation(_saveOperationInfo);
                        _operationWorker.Update(_operationInfo);
                    }

                    _hospitalizationViewForm.ShowOperations();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _configurationEngine.OperationViewFormLocation = Location;
        }


        #region Подсказки
        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сохранить изменения", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть форму без сохранения изменений", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelSurgeonList_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть список хирургов", linkLabelSurgeonList);
        }

        private void linkLabelSurgeonList_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void linkLabelAssistentList_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Выбрать ассистентов из списка хирургов", linkLabelAssistentList);
        }

        private void linkLabelAssistentList_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void linkLabelScrubNurseList_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Окрыть список операционных мед. сестёр", linkLabelScrubNurseList);
        }

        private void linkLabelScrubNurseList_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void linkLabelOrderlyList_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Окрыть список санитаров", linkLabelOrderlyList);
        }

        private void linkLabelOrderlyList_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void buttonProtocol_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Редактировать протокол операции", buttonProtocol);
            buttonProtocol.FlatStyle = FlatStyle.Popup;
        }

        private void buttonProtocol_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonProtocol.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelOperationType_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Окрыть список типов операций", linkLabelOperationType);
        }

        private void linkLabelOperationType_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }
        #endregion

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToOperation();
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToOperation();
            }
        }

        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToOperation();
            }
        }
    }
}
