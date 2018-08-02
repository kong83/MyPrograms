using System;
using System.Windows.Forms;

using SurgeryHelper.Interfaces;
using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;
using System.IO;
using SurgeryHelper.Essences;
using SurgeryHelper.Workers;

namespace SurgeryHelper.Forms
{
    public partial class SelectDocumentForm : Form
    {
        private const string AdditionalDocumentsFolderName = "AdditionalDocuments";

        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalization;
        private readonly COperationWorker _operationWorker;
        private readonly CDischargeEpicrisis _dischargeEpicrisis;

        private readonly CGlobalSettings _globalSettings;
        private readonly ISelectedDocumentForm _selectedForm;
        private readonly bool[] _existInfo;
        private readonly string _additionalDocumentsFolderPath;

        public SelectDocumentForm(
            ISelectedDocumentForm form, 
            bool[] existInfo, 
            CPatient patientInfo, 
            CHospitalization hospitalization,
            COperationWorker operationWorker,
            CDischargeEpicrisis dischargeEpicrisis, 
            CGlobalSettings globalSettings)
        {
            InitializeComponent();

            _patientInfo = patientInfo;
            _hospitalization = hospitalization;
            _operationWorker = operationWorker;
            _dischargeEpicrisis = dischargeEpicrisis;
            _globalSettings = globalSettings;
            _selectedForm = form;
            _selectedForm.SelectedDocument = string.Empty;

            if (existInfo.Length != 4)
            {
                throw new ArgumentException("Lenght of existInfo array should be 4");
            }

            _existInfo = new bool[existInfo.Length];
            existInfo.CopyTo(_existInfo, 0);
            _additionalDocumentsFolderPath = Path.Combine(Application.StartupPath, AdditionalDocumentsFolderName);
        }


        private void SelectDocumentForm_Load(object sender, EventArgs e)
        {
            if (_existInfo[0])
            {
                buttonTransferEpicrisis.Image = Properties.Resources.OK;
            }

            if (_existInfo[1])
            {
                buttonDischargeEpicrisis.Image = Properties.Resources.OK;
            }

            if (_existInfo[2])
            {
                buttonMedicalInspection.Image = Properties.Resources.OK;
            }

            if (_existInfo[3])
            {
                buttonLineOfCommEpicrisis.Image = Properties.Resources.OK;
            }
        }


        /// <summary>
        /// Отправить выбранный документ в форму с данными по пациенту
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            _selectedForm.SelectedDocument = ((Button)sender).Text.Trim();
            Close();
        }


        #region Подсказки
        private void buttonTransferEpicrisis_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Перейти к генерации переводного эпикриза", buttonTransferEpicrisis);
            buttonTransferEpicrisis.FlatStyle = FlatStyle.Popup;
        }

        private void buttonTransferEpicrisis_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonTransferEpicrisis.FlatStyle = FlatStyle.Flat;
        }

        private void buttonMedicalInspection_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Перейти к генерации осмотра в отделении", buttonMedicalInspection);
            buttonMedicalInspection.FlatStyle = FlatStyle.Popup;
        }

        private void buttonMedicalInspection_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonMedicalInspection.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDischargeEpicrisis_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Перейти к генерации выписного эпикриза", buttonDischargeEpicrisis);
            buttonDischargeEpicrisis.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDischargeEpicrisis_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDischargeEpicrisis.FlatStyle = FlatStyle.Flat;
        }

        private void buttonLineOfCommEpicrisis_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Перейти к генерации этапного эпикриза", buttonLineOfCommEpicrisis);
            buttonLineOfCommEpicrisis.FlatStyle = FlatStyle.Popup;
        }

        private void buttonLineOfCommEpicrisis_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonLineOfCommEpicrisis.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAdditionalDocument_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Экспортировать в Word выбранный документ", buttonAdditionalDocument);
            buttonAdditionalDocument.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdditionalDocument_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdditionalDocument.FlatStyle = FlatStyle.Flat;
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
        #endregion

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
                _hospitalization,
                null,
                _operationWorker,
                _dischargeEpicrisis,
                _globalSettings);


            Close();
        }


        /// <summary>
        /// Поместить все документы из папки AdditionalDocuments в комбобокс
        /// </summary>
        private void PutAdditionalDocumentNamesToComboBox()
        {
            string saveText = comboBoxAdditionalDocuments.Text;

            comboBoxAdditionalDocuments.Items.Clear();
            var dirInfo = new DirectoryInfo(_additionalDocumentsFolderPath);
            if(!dirInfo.Exists)
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


        private void SelectDocumentForm_Activated(object sender, EventArgs e)
        {
            PutAdditionalDocumentNamesToComboBox();
        }
    }
}
