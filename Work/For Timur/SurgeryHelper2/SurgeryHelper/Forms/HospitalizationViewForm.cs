using System;
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
    public partial class HospitalizationViewForm : Form, ISelectedDocumentForm, IPutStringToSurgeonComboBoxForm
    {
        public string SelectedDocument
        {
            set
            {
                _selectedDocument = value;
            }
        }

        private string _selectedDocument;

        private readonly CWorkersKeeper _workersKeeper;
        private readonly CHospitalizationWorker _hospitalizationWorker;
        private readonly COperationWorker _operationWorker;

        private readonly CPatient _patientInfo;
        private CHospitalization _hospitalizationInfo;
        private readonly CHospitalization _saveHospitalizationInfo;

        private readonly PatientViewForm _patientViewForm;
        private OperationViewForm _addNewOperationForm;

        private TransferableEpicrisisForm _transferableEpicrisisForm;
        private LineOfCommunicationEpicrisisForm _lineOfCommunicationEpicrisisForm;
        private DischargeEpicrisisForm _dischargeEpicrisisForm;
        private MedicalInspectionForm _medicalInspectionForm;

        private bool _isFormClosingByButton;
        private bool _isNeedSaveData;
        private bool _stopSaveParameters;

        private readonly CConfigurationEngine _configurationEngine;
        private readonly AddUpdate _action;

        private Control _selectedTextControl;

        private readonly string _realPrivateFolder;

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

        public HospitalizationViewForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            PatientViewForm patientviewForm,
            AddUpdate action)
        {
            _stopSaveParameters = true;

            InitializeComponent();
            
            _workersKeeper = workersKeeper;
            _hospitalizationWorker = workersKeeper.HospitalizationWorker;
            _operationWorker = workersKeeper.OperationWorker;

            _patientInfo = patientInfo;
            _patientViewForm = patientviewForm;

            _configurationEngine = workersKeeper.ConfigurationEngine;

            PutSurgeonsToComboBox();

            _realPrivateFolder = CConvertEngine.GetFullPrivateFolderPath(_patientInfo.PrivateFolder);

            _action = action;
            _hospitalizationInfo = hospitalizationInfo;
            _saveHospitalizationInfo = new CHospitalization(_hospitalizationInfo);

            dateTimePickerDeliveryDate.Value = _hospitalizationInfo.DeliveryDate;
            if (_hospitalizationInfo.ReleaseDate.HasValue)
            {
                dateTimePickerReleaseDate.Checked = true;
                dateTimePickerReleaseDate.Value = _hospitalizationInfo.ReleaseDate.Value;
            }
            else
            {
                dateTimePickerReleaseDate.Checked = false;
            }

            textBoxFotoFolderName.Text = _hospitalizationInfo.FotoFolderName;
            textBoxCaseHistory.Text = _hospitalizationInfo.NumberOfCaseHistory;
            textBoxDiagnose.Text = _hospitalizationInfo.Diagnose;
            comboBoxDoctorInChargeOfTheCase.Text = _hospitalizationInfo.DoctorInChargeOfTheCase;

            Text = _action == AddUpdate.Add
                ? "Добавление новой госпитализации"
                : "Просмотр данных о госпитализации";
        }


        private void HospitalizationViewForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.HospitalizationViewFormLocation.X >= 0 &&
                _configurationEngine.HospitalizationViewFormLocation.Y >= 0)
            {
                Location = _configurationEngine.HospitalizationViewFormLocation;
            }

            Size = _configurationEngine.HospitalizationViewFormSize;

            string[] widthsList = _configurationEngine.HospitalizationViewFormListWidths.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                OperationList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            ShowOperations();

            _stopSaveParameters = false;
        }



        /// <summary>
        /// Поместить строку в комбобокс с лечащим врачом
        /// </summary>
        /// <param name="str">Строка, которую туда надо положить</param>
        public void PutStringToSurgeonComboBox(string str)
        {
            comboBoxDoctorInChargeOfTheCase.Text = str;
        }


        /// <summary>
        /// Открыть список с документами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existInfo = new[]
            {
                _workersKeeper.TransferableEpicrisisWorker.IsExists(_hospitalizationInfo.Id),
                _workersKeeper.DischargeEpicrisisWorker.IsExists(_hospitalizationInfo.Id),
                _workersKeeper.MedicalInspectionWorker.IsExists(_hospitalizationInfo.Id),
                _workersKeeper.LineOfCommunicationEpicrisisWorker.IsExists(_hospitalizationInfo.Id)
            };

            CDischargeEpicrisis dischargeEpicrisis = existInfo[1] 
                ? _workersKeeper.DischargeEpicrisisWorker.GetByHospitalizationId(_hospitalizationInfo.Id) 
                : null;

            new SelectDocumentForm(
                this, existInfo, _patientInfo, _hospitalizationInfo, _workersKeeper.OperationWorker, dischargeEpicrisis, _workersKeeper.GlobalSettings).ShowDialog();

            switch (_selectedDocument)
            {
                case "Переводной эпикриз":
                    if (_transferableEpicrisisForm == null || _transferableEpicrisisForm.IsDisposed)
                    {
                        _transferableEpicrisisForm = new TransferableEpicrisisForm(
                            _workersKeeper,
                            _patientInfo,
                            _hospitalizationInfo,
                            _workersKeeper.TransferableEpicrisisWorker.GetByHospitalizationId(_hospitalizationInfo.Id))
                            {
                                MdiParent = MdiParent
                            };
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
                        _lineOfCommunicationEpicrisisForm = new LineOfCommunicationEpicrisisForm(
                            _workersKeeper,
                            _patientInfo,
                            _hospitalizationInfo,
                            _workersKeeper.LineOfCommunicationEpicrisisWorker.GetByHospitalizationId(_hospitalizationInfo.Id))
                            {
                                MdiParent = MdiParent
                            };
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
                        _dischargeEpicrisisForm = new DischargeEpicrisisForm(
                            _workersKeeper,
                            _patientInfo,
                            _hospitalizationInfo,
                            _workersKeeper.MedicalInspectionWorker.GetByHospitalizationId(_hospitalizationInfo.Id),
                            _workersKeeper.DischargeEpicrisisWorker.GetByHospitalizationId(_hospitalizationInfo.Id)) 
                            { 
                                MdiParent = MdiParent 
                            };
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
                        _medicalInspectionForm = new MedicalInspectionForm(
                            _workersKeeper,
                            _patientInfo,
                            _hospitalizationInfo,
                            _workersKeeper.MedicalInspectionWorker.GetByHospitalizationId(_hospitalizationInfo.Id)) 
                            { 
                                MdiParent = MdiParent 
                            };
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
        /// Проверка, есть ли незаполненные поля, который надо заполнять
        /// </summary>
        /// <returns></returns>
        private bool IsFormHasEmptyNeededFields()
        {
            if (string.IsNullOrEmpty(textBoxCaseHistory.Text) ||
                string.IsNullOrEmpty(textBoxDiagnose.Text) ||
                string.IsNullOrEmpty(comboBoxDoctorInChargeOfTheCase.Text))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Положить введённые данные в госпитализацию
        /// </summary>
        private void PutDataToHospitalization()
        {
            _hospitalizationInfo.DeliveryDate = dateTimePickerDeliveryDate.Value;
            _hospitalizationInfo.Diagnose = textBoxDiagnose.Text;
            _hospitalizationInfo.DoctorInChargeOfTheCase = comboBoxDoctorInChargeOfTheCase.Text;
            _hospitalizationInfo.NumberOfCaseHistory = textBoxCaseHistory.Text;
            _hospitalizationInfo.FotoFolderName = textBoxFotoFolderName.Text;
            if (dateTimePickerReleaseDate.Checked)
            {
                _hospitalizationInfo.ReleaseDate = dateTimePickerReleaseDate.Value;
            }
            else
            {
                _hospitalizationInfo.ReleaseDate = null;
            }
        }


        /// <summary>
        /// Поместить список хирургов в комбобокс
        /// </summary>
        private void PutSurgeonsToComboBox()
        {
            string saveText = comboBoxDoctorInChargeOfTheCase.Text;
            comboBoxDoctorInChargeOfTheCase.Items.Clear();

            foreach (CSurgeon surgeon in _workersKeeper.SurgeonWorker.SurgeonList)
            {
                comboBoxDoctorInChargeOfTheCase.Items.Add(surgeon.Name);
            }

            comboBoxDoctorInChargeOfTheCase.Text = saveText;
        }


        /// <summary>
        /// Открыть форму с хирургами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelDoctorInCase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_workersKeeper, this).ShowDialog();
            PutSurgeonsToComboBox();
        }


        /// <summary>
        /// Закрытие формы с сохранением изменений
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
        /// Закрытие формы без сохранения изменений
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
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void HospitalizationViewForm_FormClosing(object sender, FormClosingEventArgs e)
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

            bool isOperationViewFormOpened = false;
            var openedOperationForm = new COperation();
            COperation[] operations = _operationWorker.GetListByHospitalizationId(_hospitalizationInfo.Id);
            foreach (COperation oc in operations)
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
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете все формы \"Просмотр данных об операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                openedOperationForm.OpenedOperationViewForm.Focus();
                return;
            }

            if (_addNewOperationForm != null && !_addNewOperationForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Добавление операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _addNewOperationForm.Focus();
                return;
            }

            bool isOperationProtocolFormOpened = false;
            foreach (COperation oc in operations)
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
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете формы \"Протокол операции\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                openedOperationForm.OpenedOperationProtocolForm.Focus();
                return;
            }

            if (_transferableEpicrisisForm != null && !_transferableEpicrisisForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Переводной эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _transferableEpicrisisForm.Focus();
                return;
            }

            if (_lineOfCommunicationEpicrisisForm != null && !_lineOfCommunicationEpicrisisForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Этапный эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _lineOfCommunicationEpicrisisForm.Focus();
                return;
            }

            if (_dischargeEpicrisisForm != null && !_dischargeEpicrisisForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Выписной эпикриз\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _dischargeEpicrisisForm.Focus();
                return;
            }

            if (_medicalInspectionForm != null && !_medicalInspectionForm.IsDisposed)
            {
                _isFormClosingByButton = false;
                MessageBox.ShowDialog("Вы не можете закрыть эту форму, пока не закроете форму \"Осмотр в отделении\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _medicalInspectionForm.Focus();
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

                if (!string.IsNullOrEmpty(textBoxFotoFolderName.Text) &&
                    !Directory.Exists(Path.Combine(_realPrivateFolder, textBoxFotoFolderName.Text)))
                {
                    MessageBox.ShowDialog("Указанной папки для фотографий не существует.\r\nИсправьте название папки или удалите информацию о ней.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxFotoFolderName.Focus();
                    e.Cancel = true;
                    return;
                }

                if (dateTimePickerReleaseDate.Checked &&
                    CCompareEngine.CompareDate(dateTimePickerReleaseDate.Value.Date, dateTimePickerDeliveryDate.Value.Date) < 0)
                {
                    MessageBox.ShowDialog("Дата выписки не может быть меньше, чем дата поступления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePickerReleaseDate.Focus();
                    e.Cancel = true;
                    return;
                }

                try
                {
                    _hospitalizationWorker.Update(_hospitalizationInfo, _patientInfo);

                    _patientViewForm.ShowHospitalizations();
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
                        _hospitalizationWorker.Remove(_hospitalizationInfo.Id);
                    }
                    else
                    {
                        _hospitalizationInfo = new CHospitalization(_saveHospitalizationInfo);
                        _hospitalizationWorker.Update(_hospitalizationInfo, _patientInfo);
                    }

                    _patientViewForm.ShowHospitalizations();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _configurationEngine.HospitalizationViewFormLocation = Location;
        }


        /// <summary>
        /// Проверить, существует ли такая папка в личной папке пациента. Если не существует - 
        /// то сделать неактивной ссылку для открытия папки
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void textBoxFotoFolderName_TextChanged(object sender, EventArgs e)
        {
            string realDirectoryName = Path.Combine(_realPrivateFolder, textBoxFotoFolderName.Text);

            linkLabelFoto.Enabled = !Directory.Exists(realDirectoryName)
                ? false
                : !string.IsNullOrEmpty(textBoxFotoFolderName.Text);

            if (!_stopSaveParameters)
            {
                PutDataToHospitalization();
            }
        }


        /// <summary>
        /// Открыть папку с фотографиями
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string realDirectoryName = Path.Combine(_realPrivateFolder, textBoxFotoFolderName.Text);

            Process.Start("explorer.exe", realDirectoryName);
        }


        /// <summary>
        /// Сгенерировать папку с фотографиями
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonGenerateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_patientInfo.PrivateFolder) ||
                !Directory.Exists(CConvertEngine.GetFullPrivateFolderPath(_patientInfo.PrivateFolder)))
            {
                MessageBox.ShowDialog("Личная папка не указана или её не существует");
                return;
            }

            string fotoFolderName = CConvertEngine.DateTimeToString(dateTimePickerDeliveryDate.Value, true).Replace(":", "_").Replace(".", "_");
            string fotoPath = Path.Combine(_realPrivateFolder, fotoFolderName);

            if (Directory.Exists(fotoPath))
            {
                MessageBox.ShowDialog("Папка для этой госпитализации уже создана", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Directory.CreateDirectory(fotoPath);
                MessageBox.ShowDialog("Папка для этой госпитализации создана успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            textBoxFotoFolderName.Text = fotoFolderName;
            textBoxFotoFolderName.Focus();
        }


        /// <summary>
        /// Открыть список карт
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCards_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existInfo = new[]
            {
                _workersKeeper.ObstetricParalysisCardWorker.IsExists(_hospitalizationInfo.Id, -1),
                _workersKeeper.BrachialPlexusCardWorker.IsExists(_hospitalizationInfo.Id, -1),
                _workersKeeper.CardWorker.IsExists(_hospitalizationInfo.Id, -1, CardType.SacriplexCard),
                _workersKeeper.RangeOfMotionCardWorker.IsExists(_hospitalizationInfo.Id, -1),
                _workersKeeper.CardWorker.IsExists(_hospitalizationInfo.Id, -1, CardType.HandCutaneousNerves),
                _workersKeeper.CardWorker.IsExists(_hospitalizationInfo.Id, -1, CardType.HandDermatome),
                _workersKeeper.CardWorker.IsExists(_hospitalizationInfo.Id, -1, CardType.LegCutaneousNerves),
                _workersKeeper.CardWorker.IsExists(_hospitalizationInfo.Id, -1, CardType.LegDermatome),
                _workersKeeper.CardWorker.IsExists(_hospitalizationInfo.Id, -1, CardType.PamplegiaCard)
            };
            new SelectCardForm(this, existInfo).ShowDialog();

            switch (_selectedDocument.ToLower())
            {
                case "карта на акушерский паралич":
                    new ObstetricParalysisCardForm(
                        _workersKeeper,
                        _workersKeeper.ObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(_hospitalizationInfo.Id, -1)).ShowDialog();
                    break;
                case "карта на плечевое сплетение":
                    new BrachialPlexusCardForm(
                        _workersKeeper,
                        _workersKeeper.BrachialPlexusCardWorker.GetByHospitalizationAndVisitId(_hospitalizationInfo.Id, -1)).ShowDialog();
                    break;
                case "карта на крестцовое сплетение":
                    new PaintDoublePictureForm(
                        "Карта обследования пациента с повреждением пояснично-крестцового  сплетения",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Left, CardType.SacriplexCard),
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Right, CardType.SacriplexCard)).ShowDialog();
                    break;
                case "объём движений":
                    new RangeOfMotionCardForm(
                        _workersKeeper,
                        _workersKeeper.RangeOfMotionCardWorker.GetByHospitalizationAndVisitId(_hospitalizationInfo.Id, -1)).ShowDialog();

                    break;
                case "кожные нервы - рука":
                    new PaintDoublePictureForm(
                        "Кожные нервы верхней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Left, CardType.HandCutaneousNerves),
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Right, CardType.HandCutaneousNerves)).ShowDialog();
                    break;
                case "дерматомы - рука":
                    new PaintDoublePictureForm(
                        "Дерматомы верхней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Left, CardType.HandDermatome),
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Right, CardType.HandDermatome)).ShowDialog();
                    break;
                case "кожные нервы - нога":
                    new PaintDoublePictureForm(
                        "Кожные нервы нижней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Left, CardType.LegCutaneousNerves),
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Right, CardType.LegCutaneousNerves)).ShowDialog();
                    break;
                case "дерматомы - нога":
                    new PaintDoublePictureForm(
                        "Дерматомы нижней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Left, CardType.LegDermatome),
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Right, CardType.LegDermatome)).ShowDialog();
                    break;
                case "карта на тетраплегию":
                    new PaintDoublePictureForm(
                        "Карта обследования пациента с тетраплегией",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Left, CardType.PamplegiaCard),
                        _workersKeeper.CardWorker.GetByGeneralData(_hospitalizationInfo.Id, -1, CardSide.Right, CardType.PamplegiaCard)).ShowDialog();
                    break;
            }
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToHospitalization();
            }
        }

        private void dateTime_ValueChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToHospitalization();
            }
        }

        private void comboBox_TextChenged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToHospitalization();
            }
        }


        /// <summary>
        /// Запоминание контрола, который сейчас в фокусе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void box_Focused(object sender, EventArgs e)
        {
            _selectedTextControl = (Control)sender;
        }


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            _selectedTextControl.Focus();
        }


        /// <summary>
        /// Копировать поле диагноза из последней косультации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelDiagnose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CVisit[] visits = _workersKeeper.VisitWorker.GetListByPatientId(_patientInfo.Id);

            if (visits.Length == 0)
            {
                MessageBox.Show("У пациента нет консультаций", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CVisit visit = visits[visits.Length - 1];
            if (string.IsNullOrEmpty(visit.Diagnose))
            {
                MessageBox.Show("Поле 'Диагноз' в последней консультации не задано", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            textBoxDiagnose.Text = visit.Diagnose;
        }


        #region Работа с операциями
        /// <summary>
        ///  Получить выделенную операцию
        /// </summary>
        /// <returns></returns>
        private COperation GetSelectedOperation()
        {
            int currentNumber = OperationList.CurrentCellAddress.Y;
            int selectedId = Convert.ToInt32(OperationList.Rows[currentNumber].Cells[0].Value);

            return _operationWorker.GetById(selectedId);
        }


        /// <summary>
        /// Открыть форму с протоколом операции
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonProtocol_Click(object sender, EventArgs e)
        {
            if (OperationList.CurrentCellAddress.Y < 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            COperation operationInfo = GetSelectedOperation();
            if (operationInfo.OpenedOperationProtocolForm == null || operationInfo.OpenedOperationProtocolForm.IsDisposed)
            {
                operationInfo.OpenedOperationProtocolForm = new OperationProtocolForm(
                    _workersKeeper,
                    _patientInfo,
                    _hospitalizationInfo,
                    operationInfo,
                    _workersKeeper.OperationProtocolWorker.GetByOperationId(operationInfo.Id))
                {
                    MdiParent = MdiParent
                };
                operationInfo.OpenedOperationProtocolForm.Show();
            }
            else
            {
                operationInfo.OpenedOperationProtocolForm.Focus();
            }
        }

        /// <summary>
        /// Добавить новую операцию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (_addNewOperationForm == null || _addNewOperationForm.IsDisposed)
            {
                try
                {
                    var operationInfo = new COperation(_operationWorker.GetNewID(), _hospitalizationInfo.Id, _patientInfo.Id)
                    {
                        HeAnaesthetist = _workersKeeper.GlobalSettings.HeAnaesthetist,
                        SheAnaesthetist = _workersKeeper.GlobalSettings.SheAnaesthetist
                    };
                    _operationWorker.AddWithoutSaving(operationInfo);
                    _addNewOperationForm = new OperationViewForm(_workersKeeper, _patientInfo, _hospitalizationInfo, operationInfo, this, AddUpdate.Add) { MdiParent = MdiParent };
                    _addNewOperationForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog("Внутренняя ошибка программы при добавлении новой операции:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _addNewOperationForm.Focus();
            }
        }


        /// <summary>
        /// Удалить выделенную операцию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = OperationList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                COperation operationInfo = GetSelectedOperation();
                if (operationInfo.OpenedOperationViewForm != null && !operationInfo.OpenedOperationViewForm.IsDisposed)
                {
                    MessageBox.ShowDialog("Данная операция заблокирована для удаления,\r\nтак как она в данный момент редактируется.\r\nЗакройте окно просмотра информации по данной операции\r\nи попробуйте ещё раз.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    operationInfo.OpenedOperationViewForm.Focus();
                    return;
                }

                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить все данные об операции " + OperationList.Rows[currentNumber].Cells[1].Value + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _operationWorker.Remove(Convert.ToInt32(OperationList.Rows[currentNumber].Cells[0].Value));
                }

                ShowOperations();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Редактировать выделенную операцию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonView_Click(object sender, EventArgs e)
        {
            int currentNumber = OperationList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            COperation selectedOperation = GetSelectedOperation();
            if (selectedOperation.OpenedOperationViewForm == null || selectedOperation.OpenedOperationViewForm.IsDisposed)
            {
                selectedOperation.OpenedOperationViewForm = new OperationViewForm(_workersKeeper, _patientInfo, _hospitalizationInfo, selectedOperation, this, AddUpdate.Update)
                {
                    MdiParent = MdiParent
                };
                selectedOperation.OpenedOperationViewForm.Show();
            }
            else
            {
                selectedOperation.OpenedOperationViewForm.Focus();
            }
        }


        /// <summary>
        /// Просмотр пациента при двойном клике по нему
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void OperationList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                buttonView_Click(null, null);
            }
        }


        /// <summary>
        /// Отобразить список операций
        /// </summary>
        public void ShowOperations()
        {
            int listCnt = 0;
            int operationCnt = 0;
            COperation[] operations = _operationWorker.GetListByHospitalizationId(_hospitalizationInfo.Id);
            while (listCnt < OperationList.Rows.Count && operationCnt < operations.Length)
            {
                OperationList.Rows[listCnt].Cells[0].Value = operations[operationCnt].Id.ToString();
                OperationList.Rows[listCnt].Cells[1].Value = operations[operationCnt].DateOfOperation.ToString("dd.MM.yyyy") +
                                        " " + operations[operationCnt].StartTimeOfOperation.ToString("HH:mm");
                OperationList.Rows[listCnt].Cells[2].Value = operations[operationCnt].Name;
                listCnt++;
                operationCnt++;
            }

            if (operationCnt == operations.Length)
            {
                while (listCnt < OperationList.Rows.Count)
                {
                    OperationList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (operationCnt < operations.Length)
                {
                    var param = new[] 
                    {
                        operations[operationCnt].Id.ToString(),
                        operations[operationCnt].DateOfOperation.ToString("dd.MM.yyyy") +
                        " " + operations[operationCnt].StartTimeOfOperation.ToString("HH:mm"),
                        operations[operationCnt].Name
                    };
                    OperationList.Rows.Add(param);
                    operationCnt++;
                }
            }
        }
        #endregion


        #region Сохранение параметров формы
        private void HospitalizationViewForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.HospitalizationViewFormSize = Size;
        }


        /// <summary>
        /// Сохранение ширины колонок
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
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

            _configurationEngine.HospitalizationViewFormListWidths = widths;
        }
        #endregion


        #region Подсказки
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

        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить операцию", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выделенную операцию", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonView_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Просмотреть данные по выбранной операции", buttonView);
            buttonView.FlatStyle = FlatStyle.Popup;
        }

        private void buttonView_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonView.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDocuments_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть список документов", buttonDocuments);
            buttonDocuments.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDocuments_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDocuments.FlatStyle = FlatStyle.Flat;
        }

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

        private void linkLabelDoctorInCase_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть список хирургов", linkLabelDoctorInCase);
        }

        private void linkLabelDoctorInCase_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void buttonGenerateFolder_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сгенерировать папку для фотографий по умолчанию", buttonGenerateFolder);
            buttonGenerateFolder.FlatStyle = FlatStyle.Popup;
        }

        private void buttonGenerateFolder_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonGenerateFolder.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelFoto_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть папку с фотографиями", linkLabelFoto);
        }

        private void linkLabelFoto_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void buttonCards_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Открыть список карт обследования", buttonCards);
            buttonCards.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCards_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonCards.FlatStyle = FlatStyle.Flat;
        }

        private void linkLabelDiagnose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Скопировать данные из диагноза последней консультации пациента\r\n(текущее значение будет заменено)", linkLabelDiagnose, 15, -42);
        }

        private void linkLabelDiagnose_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
        }
        #endregion
    }
}
