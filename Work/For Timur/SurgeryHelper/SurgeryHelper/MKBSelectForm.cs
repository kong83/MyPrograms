using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class MKBSelectForm : Form
    {
        private readonly PatientViewForm _patientViewForm;
        private readonly DbEngine _dbEngine;
        private bool _stopSaveParameters;

        public MKBSelectForm(PatientViewForm patientViewForm, DbEngine dbEngine)
        {
            _stopSaveParameters = true;
            InitializeComponent();

            _dbEngine = dbEngine;
            _patientViewForm = patientViewForm;
        }

        private void MKBSelectForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.MKBSelectFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.MKBSelectFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.MKBSelectFormLocation;
            }

            Size = _dbEngine.ConfigEngine.MKBSelectFormSize;

            string[] widthsList = _dbEngine.ConfigEngine.MKBSelectFormListWidths.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                MKBCodesList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            _stopSaveParameters = false;
        }

        private void MKBSelectForm_Shown(object sender, EventArgs e)
        {
            textBoxFilterCode.Top = textBoxFilterName.Top = MKBCodesList.Top + MKBCodesList.Height + 9;

            ShowMKBCodes();
        }

        private void ShowMKBCodes()
        {
            if(checkBoxDoNotShowAll.Checked && string.IsNullOrEmpty(textBoxFilterCode.Text) && string.IsNullOrEmpty(textBoxFilterName.Text))
            {
                MKBCodesList.Rows.Clear();
                return;
            }
            
            int listCnt = 0;
            int mkbCodeCnt = 0;
            while (listCnt < MKBCodesList.Rows.Count && mkbCodeCnt < _dbEngine.MkbList.Count)
            {
                MkbClass mkb = _dbEngine.MkbList[mkbCodeCnt];
                if (IsDataSatisfyByFilters(mkb))
                {
                    MKBCodesList.Rows[listCnt].Cells[0].Value = mkb.MkbCode;
                    MKBCodesList.Rows[listCnt].Cells[1].Value = mkb.MkbName;
                    listCnt++;
                }

                mkbCodeCnt++;
            }

            if (mkbCodeCnt == _dbEngine.MkbList.Count)
            {
                while (listCnt < MKBCodesList.Rows.Count)
                {
                    MKBCodesList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (mkbCodeCnt < _dbEngine.MkbList.Count)
                {
                    if (IsDataSatisfyByFilters(_dbEngine.MkbList[mkbCodeCnt]))
                    {
                        MKBCodesList.Rows.Add(new object[] { _dbEngine.MkbList[mkbCodeCnt].MkbCode, _dbEngine.MkbList[mkbCodeCnt].MkbName });
                    }

                    mkbCodeCnt++;
                }
            }
        }

        private bool IsDataSatisfyByFilters(MkbClass mkb)
        {
            if (!mkb.MkbCode.Contains(textBoxFilterCode.Text))
            {
                return false;
            }

            if (!mkb.MkbName.ToLower().Contains(textBoxFilterName.Text.ToLower()))
            {
                return false;
            }

            return true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int currentNumber = MKBCodesList.CurrentCellAddress.Y;

            if (currentNumber >= 0)
            {
                _patientViewForm.MkbCodeFromMkbSelectForm = MKBCodesList.Rows[currentNumber].Cells[0].Value.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("Код МКБ не выбран", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MKBCodesList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonOk_Click(null, null);
        }

        private void MKBCodesList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            const int distance = 4;
            textBoxFilterCode.Left = MKBCodesList.Left + distance / 2;
            textBoxFilterCode.Width = MKBCodesList.Columns[0].Width - distance;

            textBoxFilterName.Left = textBoxFilterCode.Left + textBoxFilterCode.Width + distance;
            textBoxFilterName.Width = MKBCodesList.Columns[1].Width - distance;

            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < MKBCodesList.ColumnCount; i++)
            {
                widths += MKBCodesList.Columns[i].Width + ";";
            }

            _dbEngine.ConfigEngine.MKBSelectFormListWidths = widths;
        }

        private void MKBSelectForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.MKBSelectFormLocation = Location;
        }

        private void MKBSelectForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.MKBSelectFormSize = Size;
            textBoxFilterCode.Top = textBoxFilterName.Top = MKBCodesList.Top + MKBCodesList.Height + 9;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать код МКБ", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Закрыть окно без выбора кода", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ShowMKBCodes();
        }

        private void checkBoxDoNotShowAll_CheckedChanged(object sender, EventArgs e)
        {
            ShowMKBCodes();
        }
    }
}
