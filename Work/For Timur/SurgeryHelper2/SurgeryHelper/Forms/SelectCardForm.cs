using System;
using System.Windows.Forms;

using SurgeryHelper.Interfaces;

namespace SurgeryHelper.Forms
{
    public partial class SelectCardForm : Form
    {
        private readonly ISelectedDocumentForm _selectedForm;
        private readonly bool[] _existInfo;

        public SelectCardForm(ISelectedDocumentForm form, bool[] existInfo)
        {
            InitializeComponent();

            _selectedForm = form;
            _selectedForm.SelectedDocument = string.Empty;

            if (existInfo.Length != 9)
            {
                throw new ArgumentException("Длина массива должна быть равна 9");
            }

            _existInfo = new bool[existInfo.Length];
            existInfo.CopyTo(_existInfo, 0);
        }


        private void SelectCardForm_Load(object sender, EventArgs e)
        {
            if (_existInfo[0])
            {
                buttonObstetricHistoryCard.Image = Properties.Resources.OK;
            }

            if (_existInfo[1])
            {
                buttonMainSchema.Image = Properties.Resources.OK;                
            }

            if (_existInfo[2])
            {
                buttonSacriplexCard.Image = Properties.Resources.OK;
            }

            if (_existInfo[3])
            {
                buttonRangeOfMotion.Image = Properties.Resources.OK;
            }

            if (_existInfo[4])
            {
                buttonHandCutaneousNerves.Image = Properties.Resources.OK;
            }

            if (_existInfo[5])
            {
                buttonHandDermatome.Image = Properties.Resources.OK; 
            }

            if (_existInfo[6])
            {
                buttonLegCutaneousNerves.Image = Properties.Resources.OK;
            }

            if (_existInfo[7])
            {
                buttonLegDermatome.Image = Properties.Resources.OK;
            }

            if (_existInfo[8])
            {
                buttonPamplegiaCard.Image = Properties.Resources.OK;
            }            
        }


        private void button_Click(object sender, EventArgs e)
        {
            var pressedButton = (Button)sender;
            _selectedForm.SelectedDocument = pressedButton.Text.Trim();
            Close();
        }


        private static void button_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).FlatStyle = FlatStyle.Popup;
        }


        private static void button_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).FlatStyle = FlatStyle.Flat;
        }
    }
}
