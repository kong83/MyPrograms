using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Interfaces;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class PatientViewForm : Form, ISelectedDocumentForm
    {
        public string SelectedDocument
        {
            set { _selectedDocument = value; }
        }

        private string _selectedDocument;

        private CPatient _patientInfo;
        private readonly CPatient _savePatientInfo;

        private readonly PatientListForm _patientForm;
        private readonly CWorkersKeeper _workersKeeper;
        private readonly CPatientWorker _patientWorker;
        private readonly CHospitalizationWorker _hospitalizationWorker;
        private readonly CVisitWorker _visitWorker;
        private bool _stopSaveParameters;
        private bool _isFormClosingByButton;
        private bool _isNeedSaveData;
        private bool _stopChangingPrivateFolderText;

        private HospitalizationViewForm _addNewHospitalizationForm;
        private VisitViewForm _addNewVisitForm;
        private AnamneseForm _anamneseForm;
        private ObstetricHistoryForm _obstetricHistoryForm;
        private DictophoneForm _dictophoneForm;
        private readonly CConfigurationEngine _configurationEngine;
        private readonly AddUpdate _action;

        private Control _selectedTextControl;

        private List<string> _selectedNosologyList;

        public List<string> SelectedNosologyList
        {
            set
            {
                _selectedNosologyList = value;
                textBoxNosology.Text = _workersKeeper.NosologyWorker.GetNosologyDisplayName(value);
            }
        }

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

        public PatientViewForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            PatientListForm patientForm,
            AddUpdate action)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _workersKeeper = workersKeeper;
            _patientWorker = _workersKeeper.PatientWorker;
            _hospitalizationWorker = _workersKeeper.HospitalizationWorker;
            _visitWorker = _workersKeeper.VisitWorker;

            _patientForm = patientForm;
            _configurationEngine = workersKeeper.ConfigurationEngine;

            _action = action;
            _patientInfo = patientInfo;
            _savePatientInfo = new CPatient(_patientInfo);

            textBoxLastName.Text = _patientInfo.LastName;
            textBoxName.Text = _patientInfo.Name;
            textBoxPatronymic.Text = _patientInfo.Patronymic;
            textBoxAge.Text = CConvertEngine.GetAge(_patientInfo.Birthday);
            dateTimePickerBirthday.Value = _patientInfo.Birthday;
            textBoxCity.Text = _patientInfo.CityName;
            textBoxStreet.Text = _patientInfo.StreetName;
            textBoxHome.Text = _patientInfo.HomeNumber;
            textBoxBuilding.Text = _patientInfo.BuildingNumber;
            textBoxFlat.Text = _patientInfo.FlatNumber;
            textBoxPhone.Text = _patientInfo.Phone;
            textBoxRelatives.Text = _patientInfo.Relatives;
            checkBoxLegalRepresent.Checked = textBoxLegalRepresent.Enabled = _patientInfo.IsSpecifyLegalRepresent;
            textBoxLegalRepresent.Text = _patientInfo.LegalRepresent;
            textBoxWorkPlace.Text = _patientInfo.WorkPlace;            
            SelectedNosologyList = new List<string>(_patientInfo.NosologyList);
            textBoxPrivateFolder.Text = _patientInfo.PrivateFolder;
            textBoxEMail.Text = _patientInfo.EMail;
            textBoxPassInfoSeries.Text = _patientInfo.PassInformation.Series;
            textBoxPassInfoNumber.Text = _patientInfo.PassInformation.Number;
            textBoxPassInfoSubdivisionCode.Text = _patientInfo.PassInformation.SubdivisionCode;
            textBoxPassInfoOrganization.Text = _patientInfo.PassInformation.Organization;
            textBoxInsuranceSeries.Text = _patientInfo.InsuranceSeries;
            textBoxInsuranceNumber.Text = _patientInfo.InsuranceNumber;
            textBoxInsuranceName.Text = _patientInfo.InsuranceName;
            comboBoxInsuranceType.Text = _patientInfo.InsuranceType;

            if (_patientInfo.PassInformation.DeliveryDate.HasValue)
            {
                dateTimePickerPassInfoDeliveryDate.Checked = true;                
                try
                {
                    dateTimePickerPassInfoDeliveryDate.Value = _patientInfo.PassInformation.DeliveryDate.Value;
                }
                catch
                {
                    dateTimePickerPassInfoDeliveryDate.Checked = false;
                }
            }

            Text = action == AddUpdate.Add 
                ? "Добавление нового пациента" 
                : "Просмотр данных о пациенте";
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientViewForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.PatientViewFormLocation.X >= 0 &&
                _configurationEngine.PatientViewFormLocation.Y >= 0)
            {
                Location = _configurationEngine.PatientViewFormLocation;
            }

            Size = _configurationEngine.PatientViewFormSize;

            ShowHospitalizations();
            ShowVisits();

            textBoxPrivateFolder_TextChanged(null, null);
            _stopSaveParameters = false;
        }


        /// <summary>
        /// Поместить новое значение паспортных данных, изменённых на форме с консультациями и
        /// изменить savePatientInfo объект, если надо (чтобы изменения этого параметра не откатывались)
        /// </summary>
        /// <param name="newPassInformation"></param>
        /// <param name="modifySavePatientInfo"></param>
        public void SetNewPassInformationToTextBox(
            CPassportInformation newPassInformation, bool modifySavePatientInfo)
        {
            textBoxPassInfoSeries.Text = newPassInformation.Series;
            textBoxPassInfoNumber.Text = newPassInformation.Number;
            textBoxPassInfoSubdivisionCode.Text = newPassInformation.SubdivisionCode;
            textBoxPassInfoOrganization.Text = newPassInformation.Organization;

            if (newPassInformation.DeliveryDate.HasValue)
            {
                dateTimePickerPassInfoDeliveryDate.Checked = true;
                dateTimePickerPassInfoDeliveryDate.Value = newPassInformation.DeliveryDate.Value;
            }

            if (modifySavePatientInfo)
            {
                _savePatientInfo.PassInformation = new CPassportInformation(newPassInformation);
            }
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

            string realDirectoryName = CConvertEngine.GetFullPrivateFolderPath(textBoxPrivateFolder.Text);

            if (!Directory.Exists(realDirectoryName))
            {
                linkLabelPrivateFolder.Enabled = false;
            }
            else
            {
                linkLabelPrivateFolder.Enabled = !string.IsNullOrEmpty(textBoxPrivateFolder.Text);
            }

            _stopChangingPrivateFolderText = false;

            if (!_stopSaveParameters)
            {
                PutDataToPatient();
            }
        }


        /// <summary>
        /// Получить выделенную консультацию
        /// </summary>
        /// <returns></returns>
        private CVisit GetSelectedVisit()
        {
            int currentNumber = VisitList.CurrentCellAddress.Y;
            int hospId = Convert.ToInt32(VisitList.Rows[currentNumber].Cells[0].Value);

            try
            {
                return _visitWorker.GetById(hospId);
            }
            catch (Exception ex)
            {
                throw new Exception("Внутренняя ошибка программы. Пациент с id=" + id + " не найден. Обратитесь к разработчику.", ex);
            }
        }


        /// <summary>
        /// Отобразить список консультаций
        /// </summary>
        public void ShowVisits()
        {
            try
            {
                int listCnt = 0;
                int visitCnt = 0;
                CVisit[] visitList = _visitWorker.GetListByPatientId(_patientInfo.Id);
                while (listCnt < VisitList.Rows.Count && visitCnt < visitList.Length)
                {
                    VisitList.Rows[listCnt].Cells[0].Value = visitList[visitCnt].Id.ToString();
                    VisitList.Rows[listCnt].Cells[1].Value = CConvertEngine.DateTimeToString(visitList[visitCnt].VisitDate, true);
                    VisitList.Rows[listCnt].Cells[2].Value = visitList[visitCnt].DiagnoseOneLine;

                    listCnt++;
                    visitCnt++;
                }

                if (visitCnt == visitList.Length)
                {
                    while (listCnt < VisitList.Rows.Count)
                    {
                        VisitList.Rows.RemoveAt(listCnt);
                    }
                }
                else
                {
                    while (visitCnt < visitList.Length)
                    {
                        var param = new[] 
                    {
                        visitList[visitCnt].Id.ToString(),
                        CConvertEngine.DateTimeToString(visitList[visitCnt].VisitDate, true),
                        visitList[visitCnt].DiagnoseOneLine
                    };
                        VisitList.Rows.Add(param);

                        visitCnt++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Получить выделенную госпитализацию
        /// </summary>
        /// <returns></returns>
        private CHospitalization GetSelectedHospitalization()
        {
            int currentNumber = HospitalizationList.CurrentCellAddress.Y;
            int hospId = Convert.ToInt32(HospitalizationList.Rows[currentNumber].Cells[0].Value);

            try
            {
                return _hospitalizationWorker.GetById(hospId);
            }
            catch (Exception ex)
            {
                throw new Exception("Внутренняя ошибка программы. Пациент с id=" + id + " не найден. Обратитесь к разработчику.", ex);
            }
        }


        /// <summary>
        /// Отобразить список госпитализаций
        /// </summary>
        public void ShowHospitalizations()
        {
            try
            {
                int listCnt = 0;
                int hospitalizationCnt = 0;
                CHospitalization[] hospitalizationList = _hospitalizationWorker.GetListByPatientId(_patientInfo.Id);
                while (listCnt < HospitalizationList.Rows.Count && hospitalizationCnt < hospitalizationList.Length)
                {
                    HospitalizationList.Rows[listCnt].Cells[0].Value = hospitalizationList[hospitalizationCnt].Id.ToString();
                    HospitalizationList.Rows[listCnt].Cells[1].Value = CConvertEngine.DateTimeToString(hospitalizationList[hospitalizationCnt].DeliveryDate, true);
                    HospitalizationList.Rows[listCnt].Cells[2].Value = hospitalizationList[hospitalizationCnt].DiagnoseOneLine;
                        
                    listCnt++;
                    hospitalizationCnt++;
                }

                if (hospitalizationCnt == hospitalizationList.Length)
                {
                    while (listCnt < HospitalizationList.Rows.Count)
                    {
                        HospitalizationList.Rows.RemoveAt(listCnt);
                    }
                }
                else
                {
                    while (hospitalizationCnt < hospitalizationList.Length)
                    {
                        var param = new[] 
                    {
                        hospitalizationList[hospitalizationCnt].Id.ToString(),
                        CConvertEngine.DateTimeToString(hospitalizationList[hospitalizationCnt].DeliveryDate, true),
                        hospitalizationList[hospitalizationCnt].DiagnoseOneLine
                    };
                        HospitalizationList.Rows.Add(param);

                        hospitalizationCnt++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Положить введённые данные в пациента
        /// </summary>
        private void PutDataToPatient()
        {
            _patientInfo.LastName = textBoxLastName.Text;
            _patientInfo.Name = textBoxName.Text;
            _patientInfo.Patronymic = textBoxPatronymic.Text;
            _patientInfo.Birthday = dateTimePickerBirthday.Value;
            _patientInfo.CityName = textBoxCity.Text;
            _patientInfo.StreetName = textBoxStreet.Text;
            _patientInfo.HomeNumber = textBoxHome.Text;
            _patientInfo.BuildingNumber = textBoxBuilding.Text;
            _patientInfo.FlatNumber = textBoxFlat.Text;
            _patientInfo.Phone = textBoxPhone.Text;
            _patientInfo.PrivateFolder = textBoxPrivateFolder.Text;
            _patientInfo.Nosology = textBoxNosology.Text;
            _patientInfo.NosologyList = _selectedNosologyList;
            _patientInfo.Relatives = textBoxRelatives.Text;
            _patientInfo.IsSpecifyLegalRepresent = checkBoxLegalRepresent.Checked;
            _patientInfo.LegalRepresent = textBoxLegalRepresent.Text;
            _patientInfo.WorkPlace = textBoxWorkPlace.Text;
            _patientInfo.EMail = textBoxEMail.Text;            
            _patientInfo.PassInformation.Series = textBoxPassInfoSeries.Text;
            _patientInfo.PassInformation.Number = textBoxPassInfoNumber.Text;
            _patientInfo.PassInformation.SubdivisionCode = textBoxPassInfoSubdivisionCode.Text;
            _patientInfo.PassInformation.Organization = textBoxPassInfoOrganization.Text;
            _patientInfo.InsuranceSeries = textBoxInsuranceSeries.Text;
            _patientInfo.InsuranceNumber = textBoxInsuranceNumber.Text;
            _patientInfo.InsuranceName = textBoxInsuranceName.Text;
            _patientInfo.InsuranceType = comboBoxInsuranceType.Text;

            if (dateTimePickerPassInfoDeliveryDate.Checked)
            {
                _patientInfo.PassInformation.DeliveryDate = CConvertEngine.CopyDateTime(dateTimePickerPassInfoDeliveryDate.Value);
            }
            else
            {
                _patientInfo.PassInformation.DeliveryDate = null;
            }
        }


        /// <summary>
        /// Проверка, есть ли незаполненные поля, который надо заполнять
        /// </summary>
        /// <returns></returns>
        private bool IsFormHasEmptyNeededFields()
        {
            if (string.IsNullOrEmpty(textBoxLastName.Text) ||
                string.IsNullOrEmpty(textBoxName.Text) ||
                string.IsNullOrEmpty(textBoxPatronymic.Text) ||
                string.IsNullOrEmpty(textBoxNosology.Text))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Сохранить изменения или добавить нового пациента в список пациентов
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
        /// Выбор нозологий из списка нозологий
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelNosology_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new NosologyForm(_workersKeeper, this, _selectedNosologyList).ShowDialog();
        }


        /// <summary>
        ///  Разблокировать пациента для удаления
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Проверки на то, можно ли закрывать форму
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

            bool isHospitalizationViewFormOpened = false;
            var hospitalizationInfo = new CHospitalization();
            foreach (CHospitalization hospInfo in _hospitalizationWorker.GetListByPatientId(_patientInfo.Id))
            {
                if (hospInfo.OpenedHospitalizationViewForm != null && !hospInfo.OpenedHospitalizationViewForm.IsDisposed)
                {
                    hospitalizationInfo = hospInfo;
                    isHospitalizationViewFormOpened = true;
                    break;
                }
            }

            if (isHospitalizationViewFormOpened)
            {
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете все формы \"Просмотр данных о госпитализации\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                hospitalizationInfo.OpenedHospitalizationViewForm.Focus();
                return;
            }

            bool isVisitViewFormOpened = false;
            var visitInfo = new CVisit();
            foreach (CVisit hospInfo in _visitWorker.GetListByPatientId(_patientInfo.Id))
            {
                if (hospInfo.OpenedVisitViewForm != null && !hospInfo.OpenedVisitViewForm.IsDisposed)
                {
                    visitInfo = hospInfo;
                    isVisitViewFormOpened = true;
                    break;
                }
            }

            if (isVisitViewFormOpened)
            {
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете все формы \"Просмотр данных о виизте\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                visitInfo.OpenedVisitViewForm.Focus();
                return;
            }

            if (_anamneseForm != null && !_anamneseForm.IsDisposed)
            {
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Анамнез\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _anamneseForm.Focus();
                return;
            }

            if (_obstetricHistoryForm != null && !_obstetricHistoryForm.IsDisposed)
            {
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Акушерский анамнез\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _obstetricHistoryForm.Focus();
                return;
            }

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

                if (!string.IsNullOrEmpty(textBoxPrivateFolder.Text) &&
                    !Directory.Exists(CConvertEngine.GetFullPrivateFolderPath(textBoxPrivateFolder.Text)))
                {
                    MessageBox.ShowDialog("Указанной личной папки не существует.\r\nИсправьте название папки или удалите информацию о ней.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                try
                {
                    _patientWorker.Update(_patientInfo);

                    _patientForm.ShowPatients();
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    if (_action == AddUpdate.Add)
                    {
                        _patientWorker.Remove(_patientInfo.Id);
                    }
                    else
                    {
                        _patientInfo = new CPatient(_savePatientInfo);
                        _patientWorker.Update(_patientInfo);
                    }

                    _patientForm.ShowPatients();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Выбрать личную папку
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPrivateFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = CConvertEngine.GetFullPrivateFolderPath(textBoxPrivateFolder.Text);
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPrivateFolder.Text = folderBrowserDialog1.SelectedPath;
            }

            textBoxPrivateFolder.Focus();
        }


        /// <summary>
        /// Открыть папку в Windows Explorer
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelPrivateFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", CConvertEngine.GetFullPrivateFolderPath(textBoxPrivateFolder.Text));
        }


        /// <summary>
        /// Сгенерировать папку по умолчанию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonGenerateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxLastName.Text) ||
                string.IsNullOrEmpty(textBoxPatronymic.Text))
            {
                MessageBox.ShowDialog("Введите фамилию, имя и отчество пациента", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fullName = textBoxLastName.Text + " " + textBoxName.Text + " " + textBoxPatronymic.Text;
            string dataPath = Path.Combine(_workersKeeper.ExecutableDirectoryPath, "PatientsData\\" + fullName);

            if (Directory.Exists(dataPath))
            {
                MessageBox.ShowDialog("Папка для этого пациента уже создана", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Directory.CreateDirectory(dataPath);
                MessageBox.ShowDialog("Папка для пациента создана успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            textBoxPrivateFolder.Text = dataPath;
            textBoxPrivateFolder.Focus();
        }


        /// <summary>
        /// Открыть форму с анамнезом
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAnamnez_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existInfo = new[]
            {
                _workersKeeper.AnamneseWorker.IsExists(_patientInfo.Id),
                _workersKeeper.ObstetricHistoryWorker.IsExists(_patientInfo.Id)
            };
            new SelectAnamneseForm(this, existInfo).ShowDialog();

            switch (_selectedDocument)
            {
                case "Анамнез":
                    if (_anamneseForm == null || _anamneseForm.IsDisposed)
                    {
                        _anamneseForm = new AnamneseForm(
                            _workersKeeper,
                            _workersKeeper.AnamneseWorker.GetByPatientId(_patientInfo.Id))
                        {
                            MdiParent = MdiParent
                        };
                        _anamneseForm.Show();
                    }
                    else
                    {
                        _anamneseForm.Focus();
                    }

                    break;
                case "Акушерский анамнез":
                    if (_obstetricHistoryForm == null || _obstetricHistoryForm.IsDisposed)
                    {
                        _obstetricHistoryForm = new ObstetricHistoryForm(
                            _workersKeeper,
                            _workersKeeper.ObstetricHistoryWorker.GetByPatientId(_patientInfo.Id))
                        {
                            MdiParent = MdiParent
                        };
                        _obstetricHistoryForm.Show();
                    }
                    else
                    {
                        _obstetricHistoryForm.Focus();
                    }

                    break;
            }
        }


        #region Кнопки для работы с госпитализациями
        /// <summary>
        /// Добавление госпитализации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAddHospitalization_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_addNewHospitalizationForm == null || _addNewHospitalizationForm.IsDisposed)
            {
                try
                {
                    var hospitalizationInfo = new CHospitalization(_hospitalizationWorker.GetNewID(), _patientInfo.Id);
                    _hospitalizationWorker.AddWithoutSaving(hospitalizationInfo);
                    _addNewHospitalizationForm = new HospitalizationViewForm(_workersKeeper, _patientInfo, hospitalizationInfo, this, AddUpdate.Add) { MdiParent = MdiParent };
                    _addNewHospitalizationForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog("Внутренняя ошибка программы при добавлении новой госпитализации:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _addNewHospitalizationForm.Focus();
            }
        }


        /// <summary>
        /// Удаление госпитализации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDeleteHospitalization_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = HospitalizationList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CHospitalization hospitalizationInfo = GetSelectedHospitalization();
                if (hospitalizationInfo.OpenedHospitalizationViewForm != null && !hospitalizationInfo.OpenedHospitalizationViewForm.IsDisposed)
                {
                    MessageBox.ShowDialog("Данная госпитализация заблокирована для удаления,\r\nтак как она в данный момент редактируется.\r\nЗакройте окно просмотра информации по данной госпитализации\r\nи попробуйте ещё раз.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить все данные о госпитализации " + CConvertEngine.DateTimeToString(hospitalizationInfo.DeliveryDate, true) + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _hospitalizationWorker.Remove(hospitalizationInfo.Id);
                }

                ShowHospitalizations();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Редактирование госпитализации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonViewHospitalization_Click(object sender, EventArgs e)
        {
            if (HospitalizationList.CurrentCellAddress.Y < 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CHospitalization hospitalizationInfo = GetSelectedHospitalization();
            if (hospitalizationInfo.OpenedHospitalizationViewForm == null || hospitalizationInfo.OpenedHospitalizationViewForm.IsDisposed)
            {
                hospitalizationInfo.OpenedHospitalizationViewForm = new HospitalizationViewForm(_workersKeeper, _patientInfo, hospitalizationInfo, this, AddUpdate.Update) { MdiParent = MdiParent };
                hospitalizationInfo.OpenedHospitalizationViewForm.Show();
            }
            else
            {
                hospitalizationInfo.OpenedHospitalizationViewForm.Focus();
            }
        }


        /// <summary>
        /// Сделать копию выделенной госпитализации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopyHospitalization_Click(object sender, EventArgs e)
        {
            try
            {
                if (HospitalizationList.CurrentCellAddress.Y < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CHospitalization hospitalizationInfo = GetSelectedHospitalization();
                _hospitalizationWorker.Copy(hospitalizationInfo, _patientInfo);
                ShowHospitalizations();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Кнопки для работы с консультациями
        /// <summary>
        /// Добавление консультации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAddVisit_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_addNewVisitForm == null || _addNewVisitForm.IsDisposed)
            {
                try
                {
                    var visitInfo = new CVisit(_visitWorker.GetNewID(), _patientInfo.Id);
                    _visitWorker.AddWithoutSaving(visitInfo);
                    _addNewVisitForm = new VisitViewForm(_workersKeeper, _patientInfo, visitInfo, this, AddUpdate.Add) { MdiParent = MdiParent };
                    _addNewVisitForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog("Внутренняя ошибка программы при добавлении новой консультации:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _addNewVisitForm.Focus();
            }
        }


        /// <summary>
        /// Удаление консультации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDeleteVisit_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = VisitList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CVisit visitInfo = GetSelectedVisit();
                if (visitInfo.OpenedVisitViewForm != null && !visitInfo.OpenedVisitViewForm.IsDisposed)
                {
                    MessageBox.ShowDialog("Данная госпитализация заблокирована для удаления,\r\nтак как она в данный момент редактируется.\r\nЗакройте окно просмотра информации по данной консультации\r\nи попробуйте ещё раз.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить все данные о консультации " + CConvertEngine.DateTimeToString(visitInfo.VisitDate) + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _visitWorker.Remove(visitInfo.Id);
                }

                ShowVisits();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Редактирование консультации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonEditVisit_Click(object sender, EventArgs e)
        {
            if (VisitList.CurrentCellAddress.Y < 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CVisit visitInfo = GetSelectedVisit();
            if (visitInfo.OpenedVisitViewForm == null || visitInfo.OpenedVisitViewForm.IsDisposed)
            {
                visitInfo.OpenedVisitViewForm = new VisitViewForm(_workersKeeper, _patientInfo, visitInfo, this, AddUpdate.Update) { MdiParent = MdiParent };
                visitInfo.OpenedVisitViewForm.Show();
            }
            else
            {
                visitInfo.OpenedVisitViewForm.Focus();
            }
        }


        /// <summary>
        /// Сделать копию выделенной консультации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopyVisit_Click(object sender, EventArgs e)
        {
            try
            {
                if (VisitList.CurrentCellAddress.Y < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CVisit visitInfo = GetSelectedVisit();
                _visitWorker.Copy(visitInfo, _patientInfo);

                ShowVisits();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        /// <summary>
        /// Изменение количества лет при изменении даты
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            textBoxAge.Text = CConvertEngine.GetAge(dateTimePickerBirthday.Value);
            
            if (!_stopSaveParameters)
            {
                PutDataToPatient();
            }
        }


        /// <summary>
        /// Открытие окна просмотра госпитализации при двойном клике
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void HospitalizationList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                buttonViewHospitalization_Click(null, null);
            }
        }


        /// <summary>
        ///  Открытие окна просмотра консультации при двойном клике
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void VisitList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                buttonEditVisit_Click(null, null);
            }
        }


        /// <summary>
        /// Экспортировать данные в ворд
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonWordExport_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CWordExportHelper.CreateMifrmDocument(_patientInfo);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Сохранение данных при изменении текста в полях для ввода
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToPatient();
            }
        }


        /// <summary>
        /// Сохранение данных при изменении текста в комбобоксах
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void comboBoxInsuranceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToPatient();
            }
        }
        /// <summary>
        /// Открыть форму для записи звука
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDictophone_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_patientInfo.PrivateFolder))
            {
                MessageBox.ShowDialog("Личная папка пациента не найдена. Использование диктофона невозможно.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_dictophoneForm == null || _dictophoneForm.IsDisposed)
            {
                _dictophoneForm = new DictophoneForm(_workersKeeper, CConvertEngine.GetFullPrivateFolderPath(_patientInfo.PrivateFolder)) { MdiParent = MdiParent };
                _dictophoneForm.Show();
            }
            else
            {
                _dictophoneForm.Focus();
            }            
        }


        /// <summary>
        /// Запоминание контрола, который сейчас в фокусе
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void box_Focused(object sender, EventArgs e)
        {
            _selectedTextControl = (Control)sender;
        }


        /// <summary>
        /// Выделение контрола, который был в фокусе, при нажатии на кнопку
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            if (_selectedTextControl != null)
            {
                _selectedTextControl.Focus();
            }
        }


        /// <summary>
        /// Сохранение размера формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientViewForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.PatientViewFormSize = Size;

            const int leftShift = 12;
            const int betweenShift = 9;
            const int rigthShift = 21;

            int listWidth = (Width - leftShift - betweenShift - rigthShift) / 2;
            VisitList.Left = leftShift + listWidth + betweenShift;
            HospitalizationList.Width = VisitList.Width = listWidth;

        }


        /// <summary>
        /// Сохранение местоположения формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientViewForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.PatientViewFormLocation = Location;
        }


        /// <summary>
        /// Сдвинуть правильно все контролы после открытия формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientViewForm_Shown(object sender, EventArgs e)
        {
            PatientViewForm_SizeChanged(null, null);
        }


        /// <summary>
        /// Отображение или скрытие поля для законного представителя
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxLegalRepresent_CheckedChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                textBoxLegalRepresent.Enabled = checkBoxLegalRepresent.Checked;
                PutDataToPatient();
            }
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
            CToolTipShower.Show("Закрыть окно без сохранения изменений", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOpen_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Выбрать личную папку пациента", buttonOpen);
            buttonOpen.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpen_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOpen.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelNosology_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть список нозологий", linkLabelNosology);
        }

        private void linkLabelNosology_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void linkLabelPrivateFolder_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть личную папку пациента в Проводнике", linkLabelPrivateFolder);
        }

        private void linkLabelPrivateFolder_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void buttonGenerateFolder_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сгенерировать папку по умолчанию", buttonGenerateFolder);
            buttonGenerateFolder.FlatStyle = FlatStyle.Popup;
        }

        private void buttonGenerateFolder_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonGenerateFolder.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAnamnez_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть анамнез", buttonAnamnez);
            buttonAnamnez.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAnamnez_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAnamnez.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAddHospitalization_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить госпитализацию", buttonAddHospitalization);
            buttonAddHospitalization.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAddHospitalization_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAddHospitalization.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDeleteHospitalization_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить госпитализацию", buttonDeleteHospitalization);
            buttonDeleteHospitalization.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDeleteHospitalization_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDeleteHospitalization.FlatStyle = FlatStyle.Flat;
        }

        private void buttonViewHospitalization_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Посмотреть информацию по выделенной госпитализации", buttonViewHospitalization);
            buttonViewHospitalization.FlatStyle = FlatStyle.Popup;
        }

        private void buttonViewHospitalization_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonViewHospitalization.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAddVisit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить консультацию", buttonAddVisit);
            buttonAddVisit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAddVisit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAddVisit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDeleteVisit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выделенную консультацию", buttonDeleteVisit);
            buttonDeleteVisit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDeleteVisit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDeleteVisit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEditVisit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Посмотреть информацию по выделенной консультации", buttonEditVisit);
            buttonEditVisit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEditVisit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonEditVisit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopyHospitalization_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сделать копию выделенной госпитализации", buttonCopyHospitalization);
            buttonCopyHospitalization.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopyHospitalization_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonCopyHospitalization.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopyVisit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сделать копию выделенной консультации", buttonCopyVisit);
            buttonCopyVisit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopyVisit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonCopyVisit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonWordExport_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Создать медицинскую карту амбулаторного больного", buttonWordExport);
            buttonWordExport.FlatStyle = FlatStyle.Popup;
        }

        private void buttonWordExport_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonWordExport.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDictophone_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сделать голосовую запись, относящуюся к пациенту", buttonDictophone);
            buttonDictophone.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDictophone_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDictophone.FlatStyle = FlatStyle.Flat;
        }
        #endregion                
    }
}
