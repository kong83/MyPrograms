using System;
using System.Windows.Forms;

using SurgeryHelper.Interfaces;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Forms
{
    public partial class SelectAnamneseForm : Form
    {
        private readonly ISelectedDocumentForm _selectedForm;
        private readonly bool[] _existInfo;

        public SelectAnamneseForm(ISelectedDocumentForm form, bool[] existInfo)
        {
            InitializeComponent();

            _selectedForm = form;
            _selectedForm.SelectedDocument = string.Empty;

            if (existInfo.Length != 2)
            {
                throw new ArgumentException("Lenght of existInfo array should be 2");
            }

            _existInfo = new bool[existInfo.Length];
            existInfo.CopyTo(_existInfo, 0);
        }


        private void SelectAnamneseForm_Load(object sender, EventArgs e)
        {
            if (_existInfo[0])
            {
                buttonAnamnese.Image = Properties.Resources.OK;
            }

            if (_existInfo[1])
            {
                buttonObstetricHistory.Image = Properties.Resources.OK;
            }
        }


        private void button_Click(object sender, EventArgs e)
        {
            _selectedForm.SelectedDocument = ((Button)sender).Text.Trim();
            Close();
        }


        #region Подсказки
        private void buttonAnamnese_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Перейти к генерации анамнеза", buttonAnamnese);
            buttonAnamnese.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAnamnese_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAnamnese.FlatStyle = FlatStyle.Flat;
        }

        private void buttonObstetricHistory_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Перейти к генерации акушерского анамнеза", buttonObstetricHistory);
            buttonObstetricHistory.FlatStyle = FlatStyle.Popup;
        }

        private void buttonObstetricHistory_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonObstetricHistory.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
