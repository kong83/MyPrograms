using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class ServiceSelectForm : Form
    {
        private readonly PatientViewForm _patientViewForm;
        private readonly DbEngine _dbEngine;
        private readonly ServiceEngine _serviceEngine;
        private bool _stopSaveParameters;

        public ServiceSelectForm(PatientViewForm patientViewForm, DbEngine dbEngine, ServiceEngine serviceEngine)
        {
            _stopSaveParameters = true;
            InitializeComponent();

            _dbEngine = dbEngine;
            _patientViewForm = patientViewForm;
            _serviceEngine = serviceEngine;
        }

        private void ServiceSelectForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.ServiceSelectFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.ServiceSelectFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.ServiceSelectFormLocation;
            }

            Size = _dbEngine.ConfigEngine.ServiceSelectFormSize;

            string[] widthsList = _dbEngine.ConfigEngine.ServiceSelectFormListWidths.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                ServiceCodesList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            _stopSaveParameters = false;
        }

        private void ServiceSelectForm_Shown(object sender, EventArgs e)
        {
            textBoxFilterServiceName.Top = textBoxFilterServiceCode.Top = textBoxFilterKsgCode.Top =
                textBoxFilterKsgDecoding.Top = ServiceCodesList.Top + ServiceCodesList.Height + 9;

            ShowServiceCodes();
        }

        private void ShowServiceCodes()
        {
            if(checkBoxDoNotShowAll.Checked && string.IsNullOrEmpty(textBoxFilterServiceName.Text) && string.IsNullOrEmpty(textBoxFilterServiceCode.Text) &&
                string.IsNullOrEmpty(textBoxFilterKsgCode.Text) && string.IsNullOrEmpty(textBoxFilterKsgDecoding.Text))
            {
                ServiceCodesList.Rows.Clear();
                return;
            }
            
            int listCnt = 0;
            int ServiceCodeCnt = 0;
            while (listCnt < ServiceCodesList.Rows.Count && ServiceCodeCnt < _serviceEngine.Services.Count)
            {
                ServiceClass Service = _serviceEngine.Services[ServiceCodeCnt];
                if (IsDataSatisfyByFilters(Service))
                {
                    ServiceCodesList.Rows[listCnt].Cells[0].Value = Service.ServiceName;
                    ServiceCodesList.Rows[listCnt].Cells[1].Value = Service.ServiceCode;
                    ServiceCodesList.Rows[listCnt].Cells[2].Value = Service.KsgCode;
                    ServiceCodesList.Rows[listCnt].Cells[3].Value = Service.KsgDecoding;
                    listCnt++;
                }

                ServiceCodeCnt++;
            }

            if (ServiceCodeCnt == _serviceEngine.Services.Count)
            {
                while (listCnt < ServiceCodesList.Rows.Count)
                {
                    ServiceCodesList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (ServiceCodeCnt < _serviceEngine.Services.Count)
                {
                    if (IsDataSatisfyByFilters(_serviceEngine.Services[ServiceCodeCnt]))
                    {
                        ServiceCodesList.Rows.Add(new object[] 
                        {
                            _serviceEngine.Services[ServiceCodeCnt].ServiceName,
                            _serviceEngine.Services[ServiceCodeCnt].ServiceCode,
                            _serviceEngine.Services[ServiceCodeCnt].KsgCode,
                            _serviceEngine.Services[ServiceCodeCnt].KsgDecoding,
                        });
                    }

                    ServiceCodeCnt++;
                }
            }
        }

        private bool IsDataSatisfyByFilters(ServiceClass Service)
        {
            if (!Service.ServiceName.ToLower().Contains(textBoxFilterServiceName.Text.ToLower()))
            {
                return false;
            }

            if (!Service.ServiceCode.ToLower().Contains(textBoxFilterServiceCode.Text.ToLower()))
            {
                return false;
            }

            if (!Service.KsgCode.ToLower().Contains(textBoxFilterKsgCode.Text.ToLower()))
            {
                return false;
            }

            if (!Service.KsgDecoding.ToLower().Contains(textBoxFilterKsgDecoding.Text.ToLower()))
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
            int currentNumber = ServiceCodesList.CurrentCellAddress.Y;

            if (currentNumber >= 0)
            {
                _patientViewForm.ServiceInfoFromServiceSelectForm = new ServiceClass()
                {
                    ServiceName = ServiceCodesList.Rows[currentNumber].Cells[0].Value.ToString(),
                    ServiceCode = ServiceCodesList.Rows[currentNumber].Cells[1].Value.ToString(),
                    KsgCode = ServiceCodesList.Rows[currentNumber].Cells[2].Value.ToString(),
                    KsgDecoding = ServiceCodesList.Rows[currentNumber].Cells[3].Value.ToString()
                };
                Close();
            }
            else
            {
                MessageBox.Show("Название услуги не выбрано", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ServiceCodesList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonOk_Click(null, null);
        }

        private void ServiceCodesList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            const int distance = 4;
            textBoxFilterServiceName.Left = ServiceCodesList.Left + distance / 2;
            textBoxFilterServiceName.Width = ServiceCodesList.Columns[0].Width - distance;

            textBoxFilterServiceCode.Left = textBoxFilterServiceName.Left + textBoxFilterServiceName.Width + distance;
            textBoxFilterServiceCode.Width = ServiceCodesList.Columns[1].Width - distance;

            textBoxFilterKsgCode.Left = textBoxFilterServiceCode.Left + textBoxFilterServiceCode.Width + distance;
            textBoxFilterKsgCode.Width = ServiceCodesList.Columns[2].Width - distance;

            textBoxFilterKsgDecoding.Left = textBoxFilterKsgCode.Left + textBoxFilterKsgCode.Width + distance;
            textBoxFilterKsgDecoding.Width = ServiceCodesList.Columns[3].Width - distance;

            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < ServiceCodesList.ColumnCount; i++)
            {
                widths += ServiceCodesList.Columns[i].Width + ";";
            }

            _dbEngine.ConfigEngine.ServiceSelectFormListWidths = widths;
        }

        private void ServiceSelectForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.ServiceSelectFormLocation = Location;
        }

        private void ServiceSelectForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.ServiceSelectFormSize = Size;
            textBoxFilterServiceName.Top = ServiceCodesList.Top + ServiceCodesList.Height + 9;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать услугу", buttonOk, 15, -20);
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
            ShowServiceCodes();
        }

        private void checkBoxDoNotShowAll_CheckedChanged(object sender, EventArgs e)
        {
            ShowServiceCodes();
        }
    }
}
