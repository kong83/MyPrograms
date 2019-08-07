using System;
using System.Threading;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Linq;

namespace ServiceWorker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string GetSelectedServiceName()
        {
            return gridServices.Rows[gridServices.CurrentCellAddress.Y].Cells[0].Value.ToString();
        }


        private void RefreshServices()
        {
            int saveIndex = gridServices.CurrentCellAddress.Y;

            gridServices.Rows.Clear();
            foreach (ServiceController sc in ServiceController.GetServices().OrderBy(x => x.ServiceName))
            {
                var param = new string[7];
                param[0] = sc.ServiceName;
                param[1] = sc.DisplayName;
                param[4] = sc.Status.ToString();
                try
                {
                    var serEng = new ServiceEngine(sc.ServiceName);
                    ServiceInfo si = serEng.GetServiceInfo();

                    param[2] = serEng.IsServiceMarkedForDelete.ToString();
                    param[3] = si.BinaryPathName;
                    param[5] = si.ServiceType;
                    param[6] = si.StartType;
                }
                catch (Exception ex)
                {
                    param[3] = ex.Message;
                }
                gridServices.Rows.Add(param);
            }

            if (gridServices.Rows.Count > saveIndex)
            {
                gridServices.CurrentCell = saveIndex >= 0
                    ? gridServices.Rows[saveIndex].Cells[0]
                    : gridServices.Rows[0].Cells[0];
            }
            else if (gridServices.Rows.Count > 0)
            {
                gridServices.CurrentCell = gridServices.Rows[gridServices.Rows.Count - 1].Cells[0];
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Обновить список сервисов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Показать информацию о выбранном сервисе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInfo_Click(object sender, EventArgs e)
        {
            try
            {
                new InfoServiceForm(GetSelectedServiceName()).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Удалить выбранный сервис
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var se = new ServiceEngine(GetSelectedServiceName(), ServiceAccess.Full);
                se.RemoveService();

                Thread.Sleep(1000);
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Создать новый сервис
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                new CreateServiceForm().ShowDialog();

                Thread.Sleep(1000);
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Stop service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                new ServiceEngine(GetSelectedServiceName()).StartService();

                Thread.Sleep(1000);
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Start service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                new ServiceEngine(GetSelectedServiceName()).StopService();

                Thread.Sleep(1000);
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Pause service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPause_Click(object sender, EventArgs e)
        {
            try
            {
                new ServiceEngine(GetSelectedServiceName()).PauseService();

                Thread.Sleep(1000);
                RefreshServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonMagic_Click(object sender, EventArgs e)
        {
            var magicForm = new MagicForm();
            magicForm.ShowDialog();
        }
    }
}
