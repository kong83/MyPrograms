using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;
using System.Collections.Generic;

namespace SurgeryHelper
{
    public partial class PatientViewForm : Form
    {
        public string SelectedDocument;

        private readonly PatientClass _savePatientInfo;

        public string MkbCodeFromMkbSelectForm { set; private get; }

        public ServiceClass ServiceInfoFromServiceSelectForm { set; private get; }

        private readonly PatientListForm _patientForm;
        private readonly DbEngine _dbEngine;
        private readonly PatientClass _patientInfo;
        private OperationForm _operationForm;
        private bool _stopSaveParameters;
        private bool _stopComboBoxServiceNameItemsChanged = false;
        private bool _isFormClosingByButton;

        private TransferableEpicrisisForm _transferableEpicrisisForm;
        private LineOfCommunicationEpicrisisForm _lineOfCommunicationEpicrisisForm;
        private DischargeEpicrisisForm _dischargeEpicrisisForm;
        private MedicalInspectionForm _medicalInspectionForm;

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

        private List<LastServiceComboBoxItem> LastUsedServices
        {
            get
            {
                return comboBoxTypeKSG.Text.ToLower() == "н" ? _dbEngine.ConfigEngine.PatientViewFormLastNightServices : _dbEngine.ConfigEngine.PatientViewFormLastDayServices;
            }

            set
            {
                if (comboBoxTypeKSG.Text.ToLower() == "н")
                {
                    _dbEngine.ConfigEngine.PatientViewFormLastNightServices = value;
                }
                else
                {
                    _dbEngine.ConfigEngine.PatientViewFormLastDayServices = value;
                }
            }
        }

        public PatientViewForm(PatientListForm patientListForm, DbEngine dbEngine, PatientClass patientInfo)
        {
            _stopSaveParameters = true;            

            InitializeComponent();

            _dbEngine = dbEngine;
            _patientForm = patientListForm;

            PutObjectsToComboBox(_dbEngine.SurgeonList.ToArray(), comboBoxDoctorInChargeOfTheCase);
            PutObjectsToComboBox(_dbEngine.NosologyList.ToArray(), comboBoxNosology);

            comboBoxTypeKSG.SelectedIndex = 0;

            comboBoxMKB.Items.Clear();
            comboBoxMKB.Items.AddRange(_dbEngine.ConfigEngine.PatientViewFormLastMKB);

            if (patientInfo == null)
            {
                Text = "Добавление нового пациента";
                _patientInfo = new PatientClass();

                FillComboBoxServiceName();
            }
            else
            {
                Text = "Просмотр данных о пациенте";
                _patientInfo = patientInfo;
                _savePatientInfo = new PatientClass(patientInfo);
                textBoxLastName.Text = _patientInfo.LastName;
                textBoxName.Text = _patientInfo.Name;
                textBoxPatronymic.Text = _patientInfo.Patronymic;                
                dateTimePickerBirthday.Value = _patientInfo.Birthday;
                comboBoxMKB.Text = _patientInfo.MKB;

                textBoxCity.Text = _patientInfo.CityName;
                textBoxStreet.Text = _patientInfo.StreetName;
                textBoxHome.Text = _patientInfo.HomeNumber;
                textBoxBuilding.Text = _patientInfo.BuildingNumber;
                textBoxFlat.Text = _patientInfo.FlatNumber;
                textBoxWorkPlace.Text = _patientInfo.WorkPlace;
                textBoxPassport.Text = _patientInfo.PassportNumber;
                textBoxPolis.Text = _patientInfo.PolisNumber;
                textBoxSnils.Text = _patientInfo.SnilsNumber;
                textBoxPhone.Text = _patientInfo.Phone;
                comboBoxTypeKSG.Text = _patientInfo.TypeOfKSG;

                // Заполняем последние использованные услуги после установления типа стационара
                FillComboBoxServiceName();

                // Если задано название услуги то прописываем её первой в списке использованных услуг, чтобы текст мог отобразиться
                if (!string.IsNullOrEmpty(_patientInfo.ServiceName))
                {
                    SaveLastUsedServices(
                        new LastServiceComboBoxItem(string.Format("{0};{1};{2};{3}", _patientInfo.ServiceName, _patientInfo.ServiceCode, _patientInfo.KsgCode, _patientInfo.KsgDecoding)));
                }

                textBoxDiagnose.Text = _patientInfo.Diagnose;
                textBoxConcomitantDiagnose.Text = patientInfo.ConcomitantDiagnose;
                textBoxComplications.Text = patientInfo.Complications;
                textBoxCaseHistory.Text = _patientInfo.NumberOfCaseHistory;
                comboBoxNosology.Text = _patientInfo.Nosology;
                dateTimePickerDeliveryDate.Value = _patientInfo.DeliveryDate;
                if (_patientInfo.ReleaseDate.HasValue)
                {
                    dateTimePickerReleaseDate.Checked = true;
                    dateTimePickerReleaseDate.Value = _patientInfo.ReleaseDate.Value;
                }
                else
                {
                    dateTimePickerReleaseDate.Checked = false;
                }

                comboBoxDoctorInChargeOfTheCase.Text = _patientInfo.DoctorInChargeOfTheCase;
                textBoxPrivateFolder.Text = _patientInfo.PrivateFolder;
            }

            textBoxOperationCount.Text = _patientInfo.Operations.Count.ToString();
        }

        private void PatientViewForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.PatientViewFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.PatientViewFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.PatientViewFormLocation;
            }

            Size = _dbEngine.ConfigEngine.PatientViewFormSize;

            _stopSaveParameters = false;
            textBoxPrivateFolder_TextChanged(null, null);
        }


        #region Валидация данных
        private bool _stopChangingPrivateFolderText;

        private string GetRealPrivateFolderPath()
        {
            string realDirectoryName;
            if (textBoxPrivateFolder.Text.Length > 1 && textBoxPrivateFolder.Text[1] == ':')
            {
                realDirectoryName = textBoxPrivateFolder.Text;
            }
            else
            {
                realDirectoryName = Path.Combine(Application.StartupPath, textBoxPrivateFolder.Text);
            }

            return realDirectoryName;
        }

        private void textBoxPrivateFolder_TextChanged(object sender, EventArgs e)
        {
            if (_stopChangingPrivateFolderText)
            {
                return;
            }

            _stopChangingPrivateFolderText = true;

            if (textBoxPrivateFolder.Text.ToLower().StartsWith(Application.StartupPath.ToLower()))
            {
                textBoxPrivateFolder.Text = textBoxPrivateFolder.Text.Remove(0, Application.StartupPath.Length).TrimStart('\\');
            }

            string realDirectoryName = GetRealPrivateFolderPath();

            if (!Directory.Exists(realDirectoryName))
            {
                errorProvider1.SetError(textBoxPrivateFolder, "Указанная папка не существует");
                linkLabelPrivateFolder.Enabled = false;
            }
            else
            {
                errorProvider1.SetError(textBoxPrivateFolder, string.Empty);
                linkLabelPrivateFolder.Enabled = !string.IsNullOrEmpty(textBoxPrivateFolder.Text);
            }

            _stopChangingPrivateFolderText = false;
        }
        #endregion

        /// <summary>
        /// Открыть форму со списком операций
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOperations_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PutDataToPatient(_patientInfo);

            if (_operationForm == null || _operationForm.IsDisposed)
            {                
                _operationForm = new OperationForm(this, _dbEngine, _patientInfo) { MdiParent = MdiParent };
                _operationForm.Show();
            }
            else
            {
                _operationForm.Focus();
            }
        }

        /// <summary>
        /// Открыть форму с назначениями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrescription_Click(object sender, EventArgs e)
        {
            new PrescriptionForm(_dbEngine, _patientInfo).ShowDialog();
        }

        /// <summary>
        /// Открыть список с документами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PutDataToPatient(_patientInfo);

            new DocumentsForm(this, _patientInfo, _dbEngine).ShowDialog();

            switch (SelectedDocument)
            {
                case "Переводной эпикриз":
                    if (_transferableEpicrisisForm == null || _transferableEpicrisisForm.IsDisposed)
                    {
                        _transferableEpicrisisForm = new TransferableEpicrisisForm(_patientInfo, _dbEngine) { MdiParent = MdiParent };
                        _transferableEpicrisisForm.Show();
                    }
                    else
                    {
                        _transferableEpicrisisForm.Focus();
                    }
                    
                    break;
                case "Этапный эпикриз":
                    if (_lineOfCommunicationEpicrisisForm == null || _lineOfCommunicationEpicrisisForm.IsDisposed)
                    {
                        _lineOfCommunicationEpicrisisForm = new LineOfCommunicationEpicrisisForm(_patientInfo, _dbEngine) { MdiParent = MdiParent };
                        _lineOfCommunicationEpicrisisForm.Show();
                    }
                    else
                    {
                        _lineOfCommunicationEpicrisisForm.Focus();
                    }
                    
                    break;
                case "Выписной эпикриз":
                    if (_dischargeEpicrisisForm == null || _dischargeEpicrisisForm.IsDisposed)
                    {
                        _dischargeEpicrisisForm = new DischargeEpicrisisForm(_patientInfo, _dbEngine) { MdiParent = MdiParent };
                        _dischargeEpicrisisForm.Show();
                    }
                    else
                    {
                        _dischargeEpicrisisForm.Focus();
                    }

                    break;
                case "Осмотр в отделении":
                    if (_medicalInspectionForm == null || _medicalInspectionForm.IsDisposed)
                    {
                        _medicalInspectionForm = new MedicalInspectionForm(_patientInfo, _dbEngine) { MdiParent = MdiParent };
                        _medicalInspectionForm.Show();
                    }
                    else
                    {
                        _medicalInspectionForm.Focus();
                    }
                    
                    break;
            }
        }

        /// <summary>
        /// Положить введённые данные в пациента
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        private void PutDataToPatient(PatientClass patientInfo)
        {
            patientInfo.LastName = textBoxLastName.Text;
            patientInfo.Name = textBoxName.Text;
            patientInfo.Patronymic = textBoxPatronymic.Text;
            patientInfo.Birthday = dateTimePickerBirthday.Value;
            patientInfo.CityName = textBoxCity.Text;
            patientInfo.StreetName = textBoxStreet.Text;
            patientInfo.HomeNumber = textBoxHome.Text;
            patientInfo.BuildingNumber = textBoxBuilding.Text;
            patientInfo.FlatNumber = textBoxFlat.Text;
            patientInfo.WorkPlace = textBoxWorkPlace.Text;
            patientInfo.PassportNumber = textBoxPassport.Text;
            patientInfo.PolisNumber = textBoxPolis.Text;
            patientInfo.SnilsNumber = textBoxSnils.Text;
            patientInfo.Phone = textBoxPhone.Text;
            patientInfo.TypeOfKSG = comboBoxTypeKSG.Text;
            patientInfo.MKB = comboBoxMKB.Text;
            patientInfo.ServiceName = comboBoxServiceName.Text;
            patientInfo.ServiceCode = textBoxServiceCode.Text;
            patientInfo.KsgCode = textBoxKsgCode.Text;
            patientInfo.KsgDecoding = textBoxKsgDecoding.Text;

            patientInfo.Diagnose = textBoxDiagnose.Text;
            patientInfo.ConcomitantDiagnose = textBoxConcomitantDiagnose.Text;
            patientInfo.Complications = textBoxComplications.Text;
            
            patientInfo.NumberOfCaseHistory = textBoxCaseHistory.Text;
            patientInfo.Nosology = comboBoxNosology.Text;
            patientInfo.DeliveryDate = dateTimePickerDeliveryDate.Value;            
            if (dateTimePickerReleaseDate.Checked)
            {
                patientInfo.ReleaseDate = dateTimePickerReleaseDate.Value;
            }
            else
            {
                patientInfo.ReleaseDate = null;
            }

            patientInfo.DoctorInChargeOfTheCase = comboBoxDoctorInChargeOfTheCase.Text;
            patientInfo.PrivateFolder = textBoxPrivateFolder.Text;

            SaveLastUsedMKB();
        }

        /// <summary>
        /// Сохранить 20 последних использованных кодов МКБ
        /// </summary>
        private void SaveLastUsedMKB()
        {
            var lastMKBList = new List<string> { comboBoxMKB.Text };
            foreach (string mkb in comboBoxMKB.Items)
            {
                if (lastMKBList.Count >= 20)
                {
                    break;
                }

                if (!lastMKBList.Contains(mkb) && !string.IsNullOrEmpty(mkb))
                {
                    lastMKBList.Add(mkb);
                }
            }

            _dbEngine.ConfigEngine.PatientViewFormLastMKB = lastMKBList.ToArray();
        }

        /// <summary>
        /// Проверка на то, есть ли какое-нибудь незаполненное обязательное поле
        /// </summary>
        /// <returns></returns>
        private bool IsFormHasEmptyNeededFields()
        {
            if (string.IsNullOrEmpty(textBoxLastName.Text) ||
                string.IsNullOrEmpty(textBoxName.Text) ||
                string.IsNullOrEmpty(textBoxPatronymic.Text) ||
                string.IsNullOrEmpty(textBoxAge.Text) ||
                string.IsNullOrEmpty(comboBoxMKB.Text) ||
                string.IsNullOrEmpty(textBoxDiagnose.Text) ||
                string.IsNullOrEmpty(textBoxCaseHistory.Text) ||
                string.IsNullOrEmpty(comboBoxNosology.Text) ||
                string.IsNullOrEmpty(comboBoxDoctorInChargeOfTheCase.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Сохранить изменения или добавить нового пациента в список пациентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_operationForm != null && !_operationForm.IsDisposed)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму со списком операций", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _operationForm.Focus();
                return;
            }

            if (_transferableEpicrisisForm != null && !_transferableEpicrisisForm.IsDisposed)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Переводной эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _transferableEpicrisisForm.Focus();
                return;
            }

            if (_lineOfCommunicationEpicrisisForm != null && !_lineOfCommunicationEpicrisisForm.IsDisposed)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Этапный эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _lineOfCommunicationEpicrisisForm.Focus();
                return;
            }

            if (_dischargeEpicrisisForm != null && !_dischargeEpicrisisForm.IsDisposed)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Выписной эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dischargeEpicrisisForm.Focus();
                return;
            }

            if (_medicalInspectionForm != null && !_medicalInspectionForm.IsDisposed)
            {
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Осмотр в отделении\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _medicalInspectionForm.Focus();
                return;
            }

            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PutDataToPatient(_patientInfo);

                if (_patientInfo.Id == 0)
                {
                    _dbEngine.AddPatient(_patientInfo);
                }
                else
                {
                    _dbEngine.UpdatePatient(_patientInfo);
                }

                _patientForm.ShowPatients();

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
            if (_patientInfo.Id != 0)
            {
                _savePatientInfo.Copy(_patientInfo);
            }

            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Открыть форму с хирургами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelDoctorInCase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_dbEngine, this, "comboBoxDoctorInChargeOfTheCase").ShowDialog();
            PutObjectsToComboBox(_dbEngine.SurgeonList.ToArray(), comboBoxDoctorInChargeOfTheCase);
        }

        /// <summary>
        /// Поместить список медицинских работников в указанный комбобокс
        /// </summary>
        public void PutObjectsToComboBox(MedicalClass[] medicalList, ComboBox comboBox)
        {
            string saveText = comboBox.Text;
            comboBox.Items.Clear();

            foreach (MedicalClass medicalInfo in medicalList)
            {
                comboBox.Items.Add(medicalInfo.LastNameWithInitials);
            }

            comboBox.Text = saveText;
        }

        /// <summary>
        /// Поместить строку комбобокс с лечащим врачом
        /// </summary>
        /// <param name="objectBoxNameOnForm">Название комбобокса</param>
        /// <param name="str">Строка, которую туда надо положить</param>
        public void PutStringToObject(string objectBoxNameOnForm, string str)
        {
            switch (objectBoxNameOnForm)
            {
                case "comboBoxDoctorInChargeOfTheCase":
                    comboBoxDoctorInChargeOfTheCase.Text = str;
                    break;
                case "comboBoxNosology":
                    comboBoxNosology.Text = str;
                    break;
            }
        }

        /// <summary>
        /// Указать количество операций для пациента после редактирования операций
        /// </summary>
        public void SetOperationCount()
        {
            textBoxOperationCount.Text = _patientInfo.Operations.Count.ToString();
        }

        /// <summary>
        /// Выбор нозологий из списка нозологий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelNosology_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new NosologyForm(_dbEngine, this).ShowDialog();
        }

        #region Сохранение параметров формы
        private void PatientViewForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.PatientViewFormLocation = Location;
        }

        private void PatientViewForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.PatientViewFormSize = Size;
        }
        #endregion

        /// <summary>
        ///  Разблокировать пользователя для удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
                return;
            }

            if (_operationForm != null && !_operationForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Cписок операций\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _operationForm.Focus();
                return;
            }

            if (_transferableEpicrisisForm != null && !_transferableEpicrisisForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Переводной эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _transferableEpicrisisForm.Focus();
                return;
            }

            if (_lineOfCommunicationEpicrisisForm != null && !_lineOfCommunicationEpicrisisForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Этапный эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _lineOfCommunicationEpicrisisForm.Focus();
                return;
            }

            if (_dischargeEpicrisisForm != null && !_dischargeEpicrisisForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Выписной эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _dischargeEpicrisisForm.Focus();
                return;
            }

            if (_medicalInspectionForm != null && !_medicalInspectionForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.Show("Вы не можете закрыть эту форму, пока не закроете форму \"Осмотр в отделении\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _medicalInspectionForm.Focus();
                return;
            }
        }

        /// <summary>
        /// Выбрать личную папку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPrivateFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = GetRealPrivateFolderPath();
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPrivateFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// Открыть папку в Windows Explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelPrivateFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", GetRealPrivateFolderPath());
        }

        /// <summary>
        /// Сгенерировать папку по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGenerateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxLastName.Text) ||
                string.IsNullOrEmpty(textBoxPatronymic.Text))
            {
                MessageBox.Show("Введите фамилию, имя и отчество пациента", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fullName = textBoxLastName.Text + " " + textBoxName.Text + " " + textBoxPatronymic.Text;
            string directoryName = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;
            string dataPath = Path.Combine(directoryName, "PatientDatas\\" + fullName);
            if (Directory.Exists(dataPath))
            {
                MessageBox.Show("Папка для этого пациента уже создана", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Directory.CreateDirectory(dataPath);
                MessageBox.Show("Папка для пациента создана успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            textBoxPrivateFolder.Text = dataPath;
        }

        /// <summary>
        /// Выделение возраста при переходе в numericUpDownAge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownAge_Enter(object sender, EventArgs e)
        {
            textBoxAge.Select(0, 10);
        }

        /// <summary>
        /// Изменение типа даных для КСГ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTypeKSG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            comboBoxServiceName.Text = textBoxServiceCode.Text = textBoxKsgCode.Text = textBoxKsgDecoding.Text = string.Empty;

            FillComboBoxServiceName();
        }

        /// <summary>
        /// Выбрать код МКБ из списка всех кодов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelMKB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MkbCodeFromMkbSelectForm = "";
            new MKBSelectForm(this, _dbEngine).ShowDialog();
            if (!string.IsNullOrEmpty(MkbCodeFromMkbSelectForm))
            {
                comboBoxMKB.Text = MkbCodeFromMkbSelectForm;
            }
        }

        /// <summary>
        /// Сохранить 20 последних выбранных услуг
        /// </summary>
        /// <param name="currentItem"></param>
        private void SaveLastUsedServices(LastServiceComboBoxItem currentItem)
        {
            if (string.IsNullOrEmpty(currentItem.HiddenValue))
            {
                return;
            }

            var lastServiceList = new List<LastServiceComboBoxItem> { currentItem };
            foreach (LastServiceComboBoxItem service in comboBoxServiceName.Items)
            {
                if (string.IsNullOrEmpty(service.HiddenValue))
                {
                    continue;
                }

                if (lastServiceList.Count >= 20)
                {
                    break;
                }

                if (!lastServiceList.Contains(service))
                {
                    lastServiceList.Add(service);
                }
            }

            LastUsedServices = lastServiceList;

            FillComboBoxServiceName();
            comboBoxServiceName.SelectedIndex = 1;
        }

        /// <summary>
        /// Заполнить списк услуг последними выбранными услугами
        /// </summary>
        private void FillComboBoxServiceName()
        {
            comboBoxServiceName.Items.Clear();
            comboBoxServiceName.Items.Add(new LastServiceComboBoxItem(""));
            comboBoxServiceName.Items.AddRange(LastUsedServices.ToArray());
        }

        /// <summary>
        /// Выбрать название услуги из списка услуг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelServiceName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            new ServiceSelectForm(this, _dbEngine, _dbEngine.GetCorrectServiceEngine(comboBoxTypeKSG.Text)).ShowDialog();

            if (ServiceInfoFromServiceSelectForm == null)
            {
                return;
            }

            SaveLastUsedServices(new LastServiceComboBoxItem(ServiceInfoFromServiceSelectForm.ToString()));            
        }

        /// <summary>
        /// Изменение кодов КСГ и других зависимых данных при смене имени услуги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxServiceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_stopComboBoxServiceNameItemsChanged)
            {
                return;
            }

            if (string.IsNullOrEmpty(comboBoxServiceName.Text))
            {
                textBoxServiceCode.Text =
                textBoxKsgCode.Text =
                textBoxKsgDecoding.Text = string.Empty;
                return;
            }

            LastServiceComboBoxItem item = (LastServiceComboBoxItem)comboBoxServiceName.SelectedItem;
            ServiceClass service = new ServiceClass(item.HiddenValue);

            textBoxServiceCode.Text = service.ServiceCode;
            textBoxKsgCode.Text = service.KsgCode;
            textBoxKsgDecoding.Text = service.KsgDecoding;

            _stopComboBoxServiceNameItemsChanged = true;
            SaveLastUsedServices((LastServiceComboBoxItem)comboBoxServiceName.SelectedItem);
            _stopComboBoxServiceNameItemsChanged = false;
        }

        private void DateTimePickerBirthday_ValueChanged(object sender, EventArgs e)
        {
            textBoxAge.Text = ConvertEngine.GetAge(dateTimePickerBirthday.Value);
        }

        #region Подсказки
        private void buttonOperations_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Открыть список операций", buttonOperations, 15, -20);
            buttonOperations.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOperations_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOperations);
            buttonOperations.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDocuments_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Открыть список документов", buttonDocuments, 15, -20);
            buttonDocuments.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDocuments_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDocuments);
            buttonDocuments.FlatStyle = FlatStyle.Flat;
        }

        private void buttonPrescription_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Открыть список назначений и дополнительных обследований", buttonPrescription, 15, -20);
            buttonPrescription.FlatStyle = FlatStyle.Popup;
        }

        private void buttonPrescription_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonPrescription);
            buttonPrescription.FlatStyle = FlatStyle.Flat;
        }

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
            toolTip1.Show("Закрыть окно без сохранения изменений\r\nВнимание! Изменения в операциях и назначениях также будут отменены!", buttonClose, 15, -40);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabel1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать лечащего врача из списка хирургов", linkLabelDoctorInCase, 15, -20);
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelDoctorInCase);
        }

        private void linkLabelNosology_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать нозологию из списка нозологий", linkLabelDoctorInCase, 15, -20);
        }

        private void linkLabelNosology_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelDoctorInCase);
        }

        private void buttonOpen_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать личную папку пациента", buttonOpen, 15, -20);
            buttonOpen.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpen_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOpen);
            buttonOpen.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelPrivateFolder_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Открыть папку в Windows Explorer", linkLabelPrivateFolder, 15, -20);
        }

        private void linkLabelPrivateFolder_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelPrivateFolder);
        }

        private void buttonGenerateFolder_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сгенерировать папку по умолчанию", buttonGenerateFolder, 15, -20);
            buttonGenerateFolder.FlatStyle = FlatStyle.Popup;
        }

        private void buttonGenerateFolder_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonGenerateFolder);
            buttonGenerateFolder.FlatStyle = FlatStyle.Flat;
        }

        private void textBoxKSGDecoding_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show(textBoxKsgDecoding.Text, textBoxKsgDecoding, 15, -20);
        }

        private void textBoxKSGDecoding_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(textBoxKsgDecoding);
        }
                      
        private void linkLabelMKB_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать код из списка кодов", linkLabelMKB, 15, -20);
        }

        private void linkLabelMKB_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelMKB);
        }

        private void comboBoxServiceName_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show(comboBoxServiceName.Text, comboBoxServiceName, 15, -20);
        }

        private void comboBoxServiceName_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBoxServiceName);
        }

        private void comboBoxMKB_MouseEnter(object sender, EventArgs e)
        {
            string mkbName = _dbEngine.GetMkbName(comboBoxMKB.Text);

            if (!string.IsNullOrEmpty(mkbName))
            {
                toolTip1.Show(mkbName, comboBoxMKB, 15, -20);
            }
        }

        private void comboBoxMKB_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBoxMKB);
        }

        private void linkLabelServiceName_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать услугу и соответствующий код КСГ из списка услуг", linkLabelServiceName, 15, -20);
        }

        private void linkLabelServiceName_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelServiceName);
        }
        #endregion
    }
}
