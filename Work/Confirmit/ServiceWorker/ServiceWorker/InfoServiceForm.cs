using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ServiceWorker
{
    public partial class InfoServiceForm : Form
    {
        readonly ServiceController _currentService;

        public InfoServiceForm(string serviceName)
        {
            InitializeComponent();

            _currentService = new ServiceController(serviceName);
        }

        private static string ServiceControllerArrayToString(IEnumerable<ServiceController> services)
        {
            var dependentServices = new StringBuilder();
            foreach (ServiceController sc in services)
            {
                dependentServices.Append(sc.DisplayName + "\r\n");
            }
            return dependentServices.ToString();
        }

        private void InfoServiceForm_Load(object sender, EventArgs e)
        {            
            gridServiceInfo.Rows.Add("MachineName", _currentService.MachineName);
            gridServiceInfo.Rows.Add("ServiceName", _currentService.ServiceName);
            gridServiceInfo.Rows.Add("DisplayName", _currentService.DisplayName);
            gridServiceInfo.Rows.Add("Status", _currentService.Status.ToString());
            gridServiceInfo.Rows.Add("CanShutdown", _currentService.CanShutdown.ToString());
            gridServiceInfo.Rows.Add("CanStop", _currentService.CanStop.ToString());
            gridServiceInfo.Rows.Add("CanPauseAndContinue", _currentService.CanPauseAndContinue.ToString());
            gridServiceInfo.Rows.Add("DependentServices", ServiceControllerArrayToString(_currentService.DependentServices));
            gridServiceInfo.Rows.Add("ServicesDependedOn", ServiceControllerArrayToString(_currentService.ServicesDependedOn));
            gridServiceInfo.Rows.Add("Site", _currentService.Site != null ? _currentService.Site.ToString() : "");

            try
            {
                var serviceEngine = new ServiceEngine(_currentService.ServiceName);
                ServiceInfo si = serviceEngine.GetServiceInfo();

                gridServiceInfo.Rows.Add("MarketForDelete", serviceEngine.IsServiceMarkedForDelete.ToString());
                gridServiceInfo.Rows.Add("BinaryPathName", si.BinaryPathName);
                gridServiceInfo.Rows.Add("StartType", si.StartType);
                gridServiceInfo.Rows.Add("ServiceType", si.ServiceType);
                gridServiceInfo.Rows.Add("ErrorControl", si.ErrorControl);
                gridServiceInfo.Rows.Add("LoadOrderGroup", si.LoadOrderGroup);
                gridServiceInfo.Rows.Add("TagID", si.TagID.ToString());
                gridServiceInfo.Rows.Add("StartName", si.StartName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridServiceInfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && gridServiceInfo.Rows.Count > 0 &&
               gridServiceInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
               gridServiceInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                Clipboard.SetText(gridServiceInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                MessageBox.Show("Скопировано в буфер: " + gridServiceInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }
    }
}
