using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class OperationViewForm : Form
    {
        private readonly OperationClass _saveOperationInfo;

        private readonly OperationForm _operationForm;
        private readonly OperationClass _operationInfo;
        private readonly PatientClass _patientInfo;
        private readonly DbEngine _dbEngine;
        private bool _stopSaveParameters;
        private bool _isFormClosingByButton;

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

        public OperationViewForm(OperationForm operationForm, DbEngine dbEngine, OperationClass operationInfo, PatientClass patientInfo)
        {
            _stopSaveParameters = true;
            InitializeComponent();

            _dbEngine = dbEngine;
            _patientInfo = patientInfo;
            _operationForm = operationForm;            

            PutObjectsToComboBox(_dbEngine.OrderlyList, comboBoxOrderly);
            PutObjectsToComboBox(_dbEngine.ScrubNurseList, comboBoxScrubNurse);
            PutObjectsToComboBox(_dbEngine.HeAnestethistList, comboBoxHeAnestethist);
            PutObjectsToComboBox(_dbEngine.SheAnestethistList, comboBoxSheAnestethist);

            if (operationInfo == null)
            {
                Text = "Добавление операции";
                _operationInfo = new OperationClass();

                textBoxSurgeons.Text = _dbEngine.ConfigEngine.OperationViewFormTextBoxSurgeons;
                textBoxAssistents.Text = _dbEngine.ConfigEngine.OperationViewFormTextBoxAssistents;
                comboBoxHeAnestethist.Text = _dbEngine.ConfigEngine.OperationViewFormTextBoxHeAnestethist;
                comboBoxSheAnestethist.Text = _dbEngine.ConfigEngine.OperationViewFormTextBoxSheAnestethist;
                comboBoxScrubNurse.Text = _dbEngine.ConfigEngine.OperationViewFormComboBoxScrubNurse;
                comboBoxOrderly.Text = _dbEngine.ConfigEngine.OperationViewFormComboBoxOrderly;
            }
            else
            {
                Text = "Просмотр данных об операции";
                _operationInfo = operationInfo;

                _saveOperationInfo = new OperationClass(operationInfo);

                textBoxName.Text = _operationInfo.Name;
                textBoxAssistents.Text = ListToMultilineString(_operationInfo.Assistents);
                textBoxSurgeons.Text = ListToMultilineString(_operationInfo.Surgeons);
                comboBoxScrubNurse.Text = _operationInfo.ScrubNurse;
                comboBoxSheAnestethist.Text = _operationInfo.SheAnaesthetist;
                comboBoxHeAnestethist.Text = _operationInfo.HeAnaesthetist;
                comboBoxOrderly.Text = _operationInfo.Orderly;

                dateTimePickerDataOfOperation.Value = _operationInfo.DataOfOperation;
                dateTimePickerStartTimeOfOperation.Value = _operationInfo.StartTimeOfOperation;
                dateTimePickerEndTimeOfOperation.Value = _operationInfo.EndTimeOfOperation;
            }
        }

        private void OperationViewForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.OperationViewFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.OperationViewFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.OperationViewFormLocation;
            }

            _stopSaveParameters = false;
        }

        /// <summary>
        /// Конвертируем список из строк в строку из нескольких строк
        /// </summary>
        /// <param name="list"></param>
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
        /// <param name="multilineStr"></param>
        /// <returns></returns>
        private static List<string> MultilineStringToList(string multilineStr)
        {
            string[] arrOfStrings = multilineStr.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return new List<string>(arrOfStrings);
        }

        #region Подсказки
        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сохранить изменения", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Закрыть форму без сохранения изменений", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelSurgeonList_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Открыть список хирургов", linkLabelSurgeonList, 15, -20);
        }

        private void linkLabelSurgeonList_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelSurgeonList);
        }

        private void linkLabelAssistentList_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать ассистентов из списка хирургов", linkLabelAssistentList, 15, -20);
        }

        private void linkLabelAssistentList_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelAssistentList);
        }

        private void linkLabelScrubNurseList_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Окрыть список операционных мед. сестёр", linkLabelScrubNurseList, 15, -20);
        }

        private void linkLabelScrubNurseList_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelScrubNurseList);
        }

        private void linkLabelHeAnestethist_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Окрыть список анестезиологов", linkLabelHeAnestethist, 15, -20);
        }

        private void linkLabelHeAnestethist_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelHeAnestethist);
        }  

        private void linkLabelSheAnestethistList_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Окрыть список анестезисток", linkLabelSheAnestethistList, 15, -20);
        }

        private void linkLabelSheAnestethistList_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelSheAnestethistList);
        } 

        private void linkLabelOrderlyList_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Окрыть список санитаров", linkLabelOrderlyList, 15, -20);
        }

        private void linkLabelOrderlyList_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelOrderlyList);
        }

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
        #endregion

        /// <summary>
        /// Поместить список хирургов в комбобокс с лечащим врачом
        /// </summary>
        private static void PutObjectsToComboBox(IEnumerable<MedicalClass> medicalList, ComboBox comboBox)
        {
            comboBox.Items.Clear();

            foreach (MedicalClass medicalInfo in medicalList)
            {
                comboBox.Items.Add(medicalInfo.LastNameWithInitials);
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
                case "comboBoxHeAnestethist":
                    comboBoxHeAnestethist.Text = str;
                    break;
                case "comboBoxSheAnestethist":
                    comboBoxSheAnestethist.Text = str;
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

                    break;
            }
        }

        /// <summary>
        /// Открыть список с хирургами для заполнения списка хирургов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelSurgeonList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_dbEngine, this, "textBoxSurgeons").ShowDialog();
        }

        /// <summary>
        /// Открыть список с хирургами для заполнения списка ассистентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelAssistentList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_dbEngine, this, "textBoxAssistents").ShowDialog();
        }

        /// <summary>
        /// Открыть список с операционными мед. сёстрами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelScrubNurseList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ScrubNurseForm(_dbEngine, this).ShowDialog();
            PutObjectsToComboBox(_dbEngine.ScrubNurseList, comboBoxScrubNurse);
        }

        /// <summary>
        /// Открыть список с санитарами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelOrderlyList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new OrderlyForm(_dbEngine, this).ShowDialog();
            PutObjectsToComboBox(_dbEngine.OrderlyList, comboBoxOrderly);
        }

        /// <summary>
        /// Открыть список с анестизиологами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelHeAnestethist_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new HeAnestethistForm(_dbEngine, this).ShowDialog();
            PutObjectsToComboBox(_dbEngine.HeAnestethistList, comboBoxHeAnestethist);
        }    

        /// <summary>
        /// Открыть список с анестизистками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelSheAnestethistList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SheAnestethistForm(_dbEngine, this).ShowDialog();
            PutObjectsToComboBox(_dbEngine.SheAnestethistList, comboBoxSheAnestethist);
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) ||
                string.IsNullOrEmpty(textBoxName.Text) ||
                string.IsNullOrEmpty(textBoxSurgeons.Text) ||
                string.IsNullOrEmpty(comboBoxScrubNurse.Text) ||
                string.IsNullOrEmpty(comboBoxScrubNurse.Text))
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PutDataToOperation(_operationInfo);

                if (_operationInfo.Id == 0)
                {
                    _patientInfo.AddOperation(_operationInfo);
                }
                else
                {
                    _patientInfo.UpdateOperation(_operationInfo);
                }

                _operationForm.ShowOperations();

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрыть форму без сохранения изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;

            try
            {
                if (_operationInfo.Id != 0)
                {
                    _operationInfo.Name = _saveOperationInfo.Name;
                    _operationInfo.Assistents = _saveOperationInfo.Assistents;
                    _operationInfo.Surgeons = _saveOperationInfo.Surgeons;
                    _operationInfo.ScrubNurse = _saveOperationInfo.ScrubNurse;
                    _operationInfo.SheAnaesthetist = _saveOperationInfo.SheAnaesthetist;
                    _operationInfo.HeAnaesthetist = _saveOperationInfo.HeAnaesthetist;
                    _operationInfo.Orderly = _saveOperationInfo.Orderly;
                    _operationInfo.DataOfOperation = _saveOperationInfo.DataOfOperation;
                    _operationInfo.StartTimeOfOperation = _saveOperationInfo.StartTimeOfOperation;
                    _operationInfo.EndTimeOfOperation = _saveOperationInfo.EndTimeOfOperation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        /// <summary>
        /// Положить введённые данные в операцию
        /// </summary>
        /// <param name="operationInfo">Данные про операцию</param>
        private void PutDataToOperation(OperationClass operationInfo)
        {
            operationInfo.Name = textBoxName.Text;
            operationInfo.Assistents = MultilineStringToList(textBoxAssistents.Text);
            operationInfo.Surgeons = MultilineStringToList(textBoxSurgeons.Text);
            operationInfo.ScrubNurse = comboBoxScrubNurse.Text;
            operationInfo.SheAnaesthetist = comboBoxSheAnestethist.Text;
            operationInfo.HeAnaesthetist = comboBoxHeAnestethist.Text;
            operationInfo.Orderly = comboBoxOrderly.Text;

            operationInfo.DataOfOperation = dateTimePickerDataOfOperation.Value;
            operationInfo.StartTimeOfOperation = dateTimePickerStartTimeOfOperation.Value;
            operationInfo.EndTimeOfOperation = dateTimePickerEndTimeOfOperation.Value;
        }

        /// <summary>
        /// Открыть форму с протоколом операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProtocol_Click(object sender, EventArgs e)
        {
            PutDataToOperation(_operationInfo);

            if (_operationInfo.OpenedOperationProtocolForm == null || _operationInfo.OpenedOperationProtocolForm.IsDisposed)
            {
                _operationInfo.OpenedOperationProtocolForm = new OperationProtocolForm(_operationInfo, _patientInfo, _dbEngine) { MdiParent = MdiParent };
                _operationInfo.OpenedOperationProtocolForm.Show();
            }
            else
            {
                _operationInfo.OpenedOperationProtocolForm.Focus();
            }            
        }

        /// <summary>
        /// Заполнение даты окончания операции, при изменеии даты начала операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePickerDataOfOperation_ValueChanged(object sender, EventArgs e)
        {
            if (_operationInfo.Id == 0)
            {
                dateTimePickerEndTimeOfOperation.Value = dateTimePickerDataOfOperation.Value;
            }
        }

        private void OperationViewForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormLocation = Location;
        }

        private void OperationViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }

        private void textBoxSurgeons_TextChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || Text != "Добавление операции")
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormTextBoxSurgeons = textBoxSurgeons.Text;
        }

        private void textBoxAssistents_TextChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || Text != "Добавление операции")
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormTextBoxAssistents = textBoxAssistents.Text;
        }
       
        private void comboBoxSheAnestethist_TextChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || Text != "Добавление операции")
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormTextBoxSheAnestethist = comboBoxSheAnestethist.Text;
        }

        private void comboBoxHeAnestethist_TextChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || Text != "Добавление операции")
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormTextBoxHeAnestethist = comboBoxHeAnestethist.Text;
        }

        private void comboBoxScrubNurse_TextChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || Text != "Добавление операции")
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormComboBoxScrubNurse = comboBoxScrubNurse.Text;
        }

        private void comboBoxOrderly_TextChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || Text != "Добавление операции")
            {
                return;
            }

            _dbEngine.ConfigEngine.OperationViewFormComboBoxOrderly = comboBoxOrderly.Text;
        }              
    }
}
