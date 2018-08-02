using System;
using System.Drawing;
using System.Windows.Forms;
using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Forms
{
    public partial class MergeShowDifferenceForm : Form
    {
        private readonly CMergeInfo _ownMergeInfo;
        private readonly CMergeInfo _foreignMergeInfo;
        private readonly ObjectType _typeOfObject;
        private readonly string _ownDescribe;
        private readonly string _foreignDescribe;
        private bool _stopSaveParameters;

        private readonly CConfigurationEngine _configurationEngine;

        private bool _isMousePressedOnMovePanel;
        private int _saveX;
        private int _savePanelMoveLeft;

        public MergeShowDifferenceForm(CMergeInfo ownMergeInfo, CMergeInfo foreignMergeInfo, string ownDescribe, string foreignDescribe, CConfigurationEngine configurationEngine)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _configurationEngine = configurationEngine;

            _typeOfObject = ownMergeInfo.TypeOfObject == ObjectType.Empty
                 ? foreignMergeInfo.TypeOfObject
                 : ownMergeInfo.TypeOfObject;

            _ownMergeInfo = ownMergeInfo;
            _foreignMergeInfo = foreignMergeInfo;
            _ownDescribe = ownDescribe;
            _foreignDescribe = foreignDescribe;
        }

        private void MergeShowDifferenceForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.PatientViewFormLocation.X >= 0 &&
                _configurationEngine.PatientViewFormLocation.Y >= 0)
            {
                Location = _configurationEngine.MergeShowDifferenceFormLocation;
            }

            Size = _configurationEngine.MergeShowDifferenceFormSize;

            _stopSaveParameters = false;
        }

        private void MergeShowDifferenceForm_Shown(object sender, EventArgs e)
        {
            richTextBoxOwnValue.Text = _ownDescribe;
            richTextBoxForeignValue.Text = _foreignDescribe;

            switch (_typeOfObject)
            {
                case ObjectType.BrachialPlexusCardPicture:
                case ObjectType.LeftRightCardPicture:
                    var ownPicture = (Bitmap)_ownMergeInfo.Object;
                    var foreignPicture = (Bitmap)_foreignMergeInfo.Object;

                    richTextBoxOwnValue.Text += "\r\n\r\n";
                    richTextBoxForeignValue.Text += "\r\n\r\n";

                    richTextBoxOwnValue.SelectionStart = richTextBoxOwnValue.Text.Length;
                    Clipboard.SetDataObject(ownPicture);
                    richTextBoxOwnValue.Paste();

                    richTextBoxForeignValue.SelectionStart = richTextBoxForeignValue.Text.Length;
                    Clipboard.SetDataObject(foreignPicture);
                    richTextBoxForeignValue.Paste();
                    Clipboard.Clear();

                    richTextBoxOwnValue.ReadOnly = richTextBoxForeignValue.ReadOnly = true;
                    break;
            }
        }

        private void panelMove_MouseDown(object sender, MouseEventArgs e)
        {
            _isMousePressedOnMovePanel = true;
            _saveX = Cursor.Position.X;
            _savePanelMoveLeft = panelMove.Left;
        }

        private void panelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMousePressedOnMovePanel)
            {
                return;
            }

            int shift = Cursor.Position.X - _saveX;

            if (_savePanelMoveLeft + shift < 70 ||
                Width - (_savePanelMoveLeft + shift + panelMove.Width) < 70)
            {
                return;
            }

            panelMove.Left = _savePanelMoveLeft + shift;
        }

        private void panelMove_MouseUp(object sender, MouseEventArgs e)
        {
            _isMousePressedOnMovePanel = false;
        }

        private void panelMove_LocationChanged(object sender, EventArgs e)
        {
            richTextBoxOwnValue.Width = panelMove.Left - 2;
            richTextBoxForeignValue.Left = panelMove.Left + panelMove.Width;
            richTextBoxForeignValue.Width = Width - 20 - richTextBoxForeignValue.Left;
            labelOwnInfo.Width = richTextBoxOwnValue.Width;
            labelForeignInfo.Left = richTextBoxForeignValue.Left;
            labelForeignInfo.Width = richTextBoxForeignValue.Width;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть форму", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MergeShowDifferenceForm_SizeChanged(object sender, EventArgs e)
        {            
            panelMove.Left = Width / 2 - 36;

            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.MergeShowDifferenceFormSize = Size;
        }

        private void MergeShowDifferenceForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.MergeShowDifferenceFormLocation = Location;

        }
    }
}
