using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Interfaces;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;
using System.IO;

namespace SurgeryHelper.Forms
{
    public partial class VisitViewForm : Form, ISelectedDocumentForm, IPutStringToSurgeonComboBoxForm
    {
        private string _selectedDocument;

        public string SelectedDocument
        {
            set { _selectedDocument = value; }
        }

        private const string NoHeaderInfo = "Шапка для выбранного врача не указана.\r\nЗайдите в редактирование выбранного врача и укажите шапку.";
        private const string AdditionalDocumentsFolderName = "AdditionalDocuments";

        private readonly CWorkersKeeper _workersKeeper;
        private readonly CVisitWorker _visitWorker;

        private readonly CPatient _patientInfo;
        private CVisit _visitInfo;
        private readonly CVisit _saveVisitInfo;

        private readonly PatientViewForm _patientViewForm;

        private bool _isFormClosingByButton;
        private bool _isNeedSaveData;
        private bool _stopSaveParameters;

        private readonly CConfigurationEngine _configurationEngine;
        private readonly AddUpdate _action;

        private Control _selectedTextControl;
        private readonly string _additionalDocumentsFolderPath;

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

        public VisitViewForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CVisit visitInfo,
            PatientViewForm patientViewForm,
            AddUpdate action)
        {            
            _stopSaveParameters = true;

            InitializeComponent();

            _additionalDocumentsFolderPath = Path.Combine(Application.StartupPath, AdditionalDocumentsFolderName);
            _workersKeeper = workersKeeper;
            _visitWorker = workersKeeper.VisitWorker;

            _patientInfo = patientInfo;
            _patientViewForm = patientViewForm;

            _configurationEngine = workersKeeper.ConfigurationEngine;

            _action = action;
            _visitInfo = visitInfo;
            _saveVisitInfo = new CVisit(_visitInfo);

            PutSurgeonsToComboBox();

            dateTimePickerVisitDate.Value = _visitInfo.VisitDate;
            textBoxDiagnose.Text = _visitInfo.Diagnose;
            textBoxRecommendation.Text = _visitInfo.Recommendation;
            textBoxComments.Text = _visitInfo.Comments;
            textBoxEvenly.Text = _visitInfo.Evenly;
            checkBoxLastParagraph.Checked = _visitInfo.IsLastParagraphForCertificateNeeded;
            checkBoxLastParagraphOdkb.Checked = _visitInfo.IsLastOdkbParagraphForCertificateNeeded;
            comboBoxDoctor.Text = _visitInfo.Doctor;

            textBoxPassInfoSeries.Text = _patientInfo.PassInformation.Series;
            textBoxPassInfoNumber.Text = _patientInfo.PassInformation.Number;
            textBoxPassInfoSubdivisionCode.Text = _patientInfo.PassInformation.SubdivisionCode;
            textBoxPassInfoOrganization.Text = _patientInfo.PassInformation.Organization;

            if (_patientInfo.PassInformation.DeliveryDate.HasValue)
            {
                dateTimePickerPassInfoDeliveryDate.Checked = true;
                dateTimePickerPassInfoDeliveryDate.Value = _patientInfo.PassInformation.DeliveryDate.Value;
            }

            Text = _action == AddUpdate.Add
                ? "Добавление новой консультации"
                : "Просмотр данных о консультации";
        }


        private void VisitViewForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.VisitViewFormLocation.X >= 0 &&
                   _configurationEngine.VisitViewFormLocation.Y >= 0)
            {
                Location = _configurationEngine.VisitViewFormLocation;
            }

            Size = _configurationEngine.VisitViewFormSize;

            _stopSaveParameters = false;
        }


        /// <summary>
        /// Открытие формы с картами обследования
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCards_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDiagnose.Text))
            {
                MessageBox.ShowDialog("Поля 'Дата консультации' и 'Диагноз' должны быть заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existInfo = new[]
            {
                _workersKeeper.ObstetricParalysisCardWorker.IsExists(-1, _visitInfo.Id),
                _workersKeeper.BrachialPlexusCardWorker.IsExists(-1, _visitInfo.Id),
                _workersKeeper.CardWorker.IsExists(-1, _visitInfo.Id, CardType.SacriplexCard),
                _workersKeeper.RangeOfMotionCardWorker.IsExists(-1, _visitInfo.Id),
                _workersKeeper.CardWorker.IsExists(-1, _visitInfo.Id, CardType.HandCutaneousNerves),
                _workersKeeper.CardWorker.IsExists(-1, _visitInfo.Id, CardType.HandDermatome),
                _workersKeeper.CardWorker.IsExists(-1, _visitInfo.Id, CardType.LegCutaneousNerves),
                _workersKeeper.CardWorker.IsExists(-1, _visitInfo.Id, CardType.LegDermatome),
                _workersKeeper.CardWorker.IsExists(-1, _visitInfo.Id, CardType.PamplegiaCard)
            };
            new SelectCardForm(this, existInfo).ShowDialog();

            switch (_selectedDocument.ToLower())
            {
                case "карта на акушерский паралич":
                    new ObstetricParalysisCardForm(
                        _workersKeeper,
                        _workersKeeper.ObstetricParalysisCardWorker.GetByHospitalizationAndVisitId(-1, _visitInfo.Id)).ShowDialog();
                    break;
                case "карта на плечевое сплетение":
                    new BrachialPlexusCardForm(
                        _workersKeeper,
                        _workersKeeper.BrachialPlexusCardWorker.GetByHospitalizationAndVisitId(-1, _visitInfo.Id)).ShowDialog();
                    break;
                case "карта на крестцовое сплетение":
                    new PaintDoublePictureForm(
                        "Карта обследования пациента с повреждением пояснично-крестцового  сплетения",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Left, CardType.SacriplexCard),
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Right, CardType.SacriplexCard)).ShowDialog();
                    break;
                case "объём движений":
                    new RangeOfMotionCardForm(
                        _workersKeeper,
                        _workersKeeper.RangeOfMotionCardWorker.GetByHospitalizationAndVisitId(-1, _visitInfo.Id)).ShowDialog();

                    break;
                case "кожные нервы - рука":
                    new PaintDoublePictureForm(
                        "Кожные нервы верхней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Left, CardType.HandCutaneousNerves),
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Right, CardType.HandCutaneousNerves)).ShowDialog();
                    break;
                case "дерматомы - рука":
                    new PaintDoublePictureForm(
                        "Дерматомы верхней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Left, CardType.HandDermatome),
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Right, CardType.HandDermatome)).ShowDialog();
                    break;
                case "кожные нервы - нога":
                    new PaintDoublePictureForm(
                        "Кожные нервы нижней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Left, CardType.LegCutaneousNerves),
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Right, CardType.LegCutaneousNerves)).ShowDialog();
                    break;
                case "дерматомы - нога":
                    new PaintDoublePictureForm(
                        "Дерматомы нижней конечности",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Left, CardType.LegDermatome),
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Right, CardType.LegDermatome)).ShowDialog();
                    break;
                case "карта на тетраплегию":
                    new PaintDoublePictureForm(
                        "Карта обследования пациента с тетраплегией",
                        _workersKeeper,
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Left, CardType.PamplegiaCard),
                        _workersKeeper.CardWorker.GetByGeneralData(-1, _visitInfo.Id, CardSide.Right, CardType.PamplegiaCard)).ShowDialog();
                    break;
            }
        }


        /// <summary>
        /// Закрытие формы с сохранением данных
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
        /// Закрытие формы без сохранения
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
        private void VisitViewForm_FormClosing(object sender, FormClosingEventArgs e)
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

                if (string.IsNullOrEmpty(textBoxDiagnose.Text))
                {
                    MessageBox.ShowDialog("Поля 'Дата консультации' и 'Диагноз' должны быть заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                try
                {
                    _visitWorker.Update(_visitInfo, _patientInfo);
                    _workersKeeper.PatientWorker.Update(_patientInfo);
                    _patientViewForm.SetNewPassInformationToTextBox(_patientInfo.PassInformation, true);

                    _patientViewForm.ShowVisits();
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
                        _visitWorker.Remove(_visitInfo.Id);
                    }
                    else
                    {
                        _visitInfo = new CVisit(_saveVisitInfo);
                        _visitWorker.Update(_visitInfo, _patientInfo);
                    }

                    _patientViewForm.ShowVisits();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _configurationEngine.VisitViewFormLocation = Location;
        }


        /// <summary>
        /// Положить введённые данные в консультацию и пациента
        /// </summary>
        private void PutDataToVisitAndPatient()
        {
            _visitInfo.VisitDate = dateTimePickerVisitDate.Value;
            _visitInfo.Diagnose = textBoxDiagnose.Text;
            _visitInfo.Evenly = textBoxEvenly.Text;
            _visitInfo.Recommendation = textBoxRecommendation.Text;
            _visitInfo.Comments = textBoxComments.Text;
            _visitInfo.IsLastParagraphForCertificateNeeded = checkBoxLastParagraph.Checked;
            _visitInfo.IsLastOdkbParagraphForCertificateNeeded = checkBoxLastParagraphOdkb.Checked;
            _visitInfo.Doctor = comboBoxDoctor.Text;
            _visitInfo.Header = textBoxHeader.Text;

            _patientInfo.PassInformation.Series = textBoxPassInfoSeries.Text;
            _patientInfo.PassInformation.Number = textBoxPassInfoNumber.Text;
            _patientInfo.PassInformation.SubdivisionCode = textBoxPassInfoSubdivisionCode.Text;
            _patientInfo.PassInformation.Organization = textBoxPassInfoOrganization.Text;

            if (dateTimePickerPassInfoDeliveryDate.Checked)
            {
                _patientInfo.PassInformation.DeliveryDate = CConvertEngine.CopyDateTime(dateTimePickerPassInfoDeliveryDate.Value);
            }
            else
            {
                _patientInfo.PassInformation.DeliveryDate = null;
            }
        }


        #region Сохранение параметров формы
        private void VisitViewForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.VisitViewFormSize = Size;
        }

        #endregion


        #region Подсказки
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

        private void buttonWordExportConsultation_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Генерировать справку в Word", buttonWordExportConsultation);
            buttonWordExportConsultation.FlatStyle = FlatStyle.Popup;
        }

        private void buttonWordExportConsultation_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonWordExportConsultation.FlatStyle = FlatStyle.Flat;
        }

        private void buttonWordExportInformedConsent_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Генерировать документ \"Информированное согласие\" в Word", buttonWordExportInformedConsent);
            buttonWordExportInformedConsent.FlatStyle = FlatStyle.Popup;
        }

        private void buttonWordExportInformedConsent_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonWordExportInformedConsent.FlatStyle = FlatStyle.Flat;
        }

        private void buttonWordExportContract_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Генерировать документ \"Договор о возмездном оказании медицинских услуг\" в Word", buttonWordExportContract);
            buttonWordExportContract.FlatStyle = FlatStyle.Popup;
        }

        private void buttonWordExportContract_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonWordExportContract.FlatStyle = FlatStyle.Flat;
        }

        private void buttonHelp_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Показать ключевые слова, используемые при работе с дополнительными документами", buttonHelp);
            buttonHelp.FlatStyle = FlatStyle.Popup;
        }

        private void buttonHelp_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonHelp.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAdditionalDocument_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Генерировать выбранный документ в Word", buttonAdditionalDocument);
            buttonAdditionalDocument.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdditionalDocument_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdditionalDocument.FlatStyle = FlatStyle.Flat;
        }
        #endregion


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToVisitAndPatient();

                var textBox = (TextBox)sender;
                if (textBox.Name.Contains("PassInfo"))
                {
                    _patientViewForm.SetNewPassInformationToTextBox(_patientInfo.PassInformation, false);
                }
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToVisitAndPatient();
            }

            var dateTimePicker = (DateTimePicker)sender;
            if (dateTimePicker.Name.Contains("PassInfo"))
            {
                _patientViewForm.SetNewPassInformationToTextBox(_patientInfo.PassInformation, false);
            }
        }

        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToVisitAndPatient();
            }

            textBoxHeader.Text = _workersKeeper.SurgeonWorker.GetHeaderByName(comboBoxDoctor.Text);
            if (string.IsNullOrEmpty(textBoxHeader.Text))
            {
                textBoxHeader.Text = NoHeaderInfo;
            }
        }

        /// <summary>
        /// Генерация справки
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonWordExportConsultation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDiagnose.Text) || textBoxHeader.Text == NoHeaderInfo)
            {
                MessageBox.ShowDialog("Поля 'Дата консультации', 'Диагноз' и 'Шапка для справки' должны быть заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PutDataToVisitAndPatient();

                CWordExportHelper.CreateVisitCertificate(
                    _patientInfo,
                    _visitInfo);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Генерация информированного согласия
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonWordExportInformedConsent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDiagnose.Text) || string.IsNullOrEmpty(comboBoxDoctor.Text))
            {
                MessageBox.ShowDialog("Поля 'Дата консультации', 'Диагноз' и 'Врач' должны быть заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PutDataToVisitAndPatient();

                CWordExportHelper.CreateVisitInformedConsent(
                    _patientInfo,
                    _visitInfo);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Генерация договора о возмездном оказании мед. услуг
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonWordExportContract_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDiagnose.Text) || string.IsNullOrEmpty(comboBoxDoctor.Text))
            {
                MessageBox.ShowDialog("Поля 'Дата консультации', 'Диагноз' и 'Врач' должны быть заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PutDataToVisitAndPatient();

                CWordExportHelper.CreateVisitContract(
                    _patientInfo,
                    _visitInfo);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void linkLabelDoctor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurgeonForm(_workersKeeper, this).ShowDialog();
            PutSurgeonsToComboBox();
        }


        /// <summary>
        /// Поместить список хирургов в комбобокс
        /// </summary>
        private void PutSurgeonsToComboBox()
        {
            string saveText = comboBoxDoctor.Text;
            comboBoxDoctor.Items.Clear();

            foreach (CSurgeon surgeon in _workersKeeper.SurgeonWorker.SurgeonList)
            {
                comboBoxDoctor.Items.Add(surgeon.Name);
            }

            comboBoxDoctor.Text = saveText;
        }


        /// <summary>
        /// Поместить строку в комбобокс с врачом
        /// </summary>
        /// <param name="str">Строка, которую туда надо положить</param>
        public void PutStringToSurgeonComboBox(string str)
        {
            comboBoxDoctor.Text = str;
        }

        /// <summary>
        /// Поместить все документы из папки AdditionalDocuments в комбобокс
        /// </summary>
        private void PutAdditionalDocumentNamesToComboBox()
        {
            string saveText = comboBoxAdditionalDocuments.Text;

            comboBoxAdditionalDocuments.Items.Clear();
            var dirInfo = new DirectoryInfo(_additionalDocumentsFolderPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
            {
                comboBoxAdditionalDocuments.Items.Add(fileInfo.Name);
            }

            if (comboBoxAdditionalDocuments.Items.Contains(saveText))
            {
                comboBoxAdditionalDocuments.Text = saveText;
            }
            else if (comboBoxAdditionalDocuments.Items.Count > 0)
            {
                comboBoxAdditionalDocuments.SelectedIndex = 0;
            }
        }

        private void VisitViewForm_Activated(object sender, EventArgs e)
        {
            PutAdditionalDocumentNamesToComboBox();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            new AdditionalDocumentsInfoForm().ShowDialog();
        }

        private void buttonAdditionalDocument_Click(object sender, EventArgs e)
        {
            if (comboBoxAdditionalDocuments.Items.Count == 0)
            {
                MessageBox.ShowDialog("Сначала добавьте документы в папку " + AdditionalDocumentsFolderName, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(comboBoxAdditionalDocuments.Text))
            {
                MessageBox.ShowDialog("Укажите документ, который надо экпортировать в Word", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CWordExportHelper.ExportAdditionalDocument(
                Path.Combine(_additionalDocumentsFolderPath, comboBoxAdditionalDocuments.Text),
                _patientInfo,
                null,
                _visitInfo,
                _workersKeeper.OperationWorker,
                null,
                _workersKeeper.GlobalSettings);


            Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!_stopSaveParameters)
            {
                PutDataToVisitAndPatient();
            }
        }
    }
}
