using System;
using System.Windows.Forms;
using System.IO;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class DocumentsForm : Form
    {
        private const string AdditionalDocumentsFolderName = "AdditionalDocuments";

        private readonly PatientViewForm _patientViewForm;
        private readonly PatientClass _patientInfo;
        private readonly GlobalSettingsClass _globalSettings;
        private readonly string _additionalDocumentsFolderPath;

        public DocumentsForm(PatientViewForm patientViewForm, PatientClass patientInfo, GlobalSettingsClass globalSettings)
        {
            InitializeComponent();

            _patientViewForm = patientViewForm;
            _patientInfo = patientInfo;
            _globalSettings = globalSettings;
            _patientViewForm.SelectedDocument = string.Empty;
            _additionalDocumentsFolderPath = Path.Combine(Application.StartupPath, AdditionalDocumentsFolderName);
        }

        /// <summary>
        /// Поместить все документы из папки AdditionalDocuments в комбобокс
        /// </summary>
        private void PutAdditionalDocumentNamesToComboBox()
        {
            comboBoxAdditionalDocuments.Items.Clear();
            var dirInfo = new DirectoryInfo(_additionalDocumentsFolderPath);
            foreach(FileInfo fileInfo in dirInfo.GetFiles())
            {                
                comboBoxAdditionalDocuments.Items.Add(fileInfo.Name);
            }

            if (comboBoxAdditionalDocuments.Items.Count > 0)
            {
                comboBoxAdditionalDocuments.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Отправить выбранный документ в форму с данными по пациенту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            _patientViewForm.SelectedDocument = ((Button)sender).Text.Trim();
            Close();
        }

        /// <summary>
        /// Сгенерировать выбранный документ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdditionalDocument_Click(object sender, EventArgs e)
        {
            if (comboBoxAdditionalDocuments.Items.Count == 0)
            {
                MessageBox.Show("Сначала добавьте документы в папку " + AdditionalDocumentsFolderName, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(comboBoxAdditionalDocuments.Text))
            {
                MessageBox.Show("Укажите документ, который надо экпортировать в Word", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WordExportEngine.ExportAdditionalDocument(
                Path.Combine(_additionalDocumentsFolderPath, comboBoxAdditionalDocuments.Text), _patientInfo, _globalSettings);

            Close();
        }

        #region Подсказки
        private void buttonTransferEpicrisis_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Перейти к генерации переводного эпикриза", buttonTransferEpicrisis, 15, -20);
            buttonTransferEpicrisis.FlatStyle = FlatStyle.Popup;
        }

        private void buttonTransferEpicrisis_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonTransferEpicrisis);
            buttonTransferEpicrisis.FlatStyle = FlatStyle.Flat;
        }

        private void buttonMedicalInspection_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Перейти к генерации осмотра при поступлении", buttonMedicalInspection, 15, -20);
            buttonMedicalInspection.FlatStyle = FlatStyle.Popup;
        }

        private void buttonMedicalInspection_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonMedicalInspection);
            buttonMedicalInspection.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDischargeEpicrisis_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Перейти к генерации выписного эпикриза", buttonDischargeEpicrisis, 15, -20);
            buttonDischargeEpicrisis.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDischargeEpicrisis_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDischargeEpicrisis);
            buttonDischargeEpicrisis.FlatStyle = FlatStyle.Flat;
        }

        private void buttonLineOfCommEpicrisis_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Перейти к генерации этапного эпикриза", buttonLineOfCommEpicrisis, 15, -20);
            buttonLineOfCommEpicrisis.FlatStyle = FlatStyle.Popup;
        }

        private void buttonLineOfCommEpicrisis_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonLineOfCommEpicrisis);
            buttonLineOfCommEpicrisis.FlatStyle = FlatStyle.Flat;
        }


        private void labelAdditionalDocument_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Для добавления документа к списку поместите его в папку " + AdditionalDocumentsFolderName, labelAdditionalDocument, 15, -20);
        }

        private void labelAdditionalDocument_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(labelAdditionalDocument);
        }

        private void buttonAdditionalDocument_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Экспортировать в Word выбранный документ", buttonAdditionalDocument, 15, -20);
            buttonAdditionalDocument.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdditionalDocument_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdditionalDocument);
            buttonAdditionalDocument.FlatStyle = FlatStyle.Flat;
        }

        private void buttonHelp_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Показать ключевые слова, используемые при работе с дополнительными документами", buttonHelp, 15, -20);
            buttonHelp.FlatStyle = FlatStyle.Popup;
        }

        private void buttonHelp_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonHelp);
            buttonHelp.FlatStyle = FlatStyle.Flat;
        }

        #endregion

        private void DocumentsForm_Activated(object sender, EventArgs e)
        {
            PutAdditionalDocumentNamesToComboBox();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            new AdditionalDocumentsInfoForm().ShowDialog();
        }
    }
}
