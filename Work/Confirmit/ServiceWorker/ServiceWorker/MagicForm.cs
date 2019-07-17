using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ServiceWorker
{
    public partial class MagicForm : Form
    {
        private readonly RemoteServiceEngine _remoteServiceEngine;
        private readonly string _parametersFileName;
        private readonly string _defaultSettingsFilePath;

        public MagicForm()
        {
            InitializeComponent();

            _remoteServiceEngine = new RemoteServiceEngine();
            _parametersFileName = Path.Combine(Application.StartupPath, "ServiceWorker.txt");
            _defaultSettingsFilePath = Path.Combine(Application.StartupPath, "DefaultServers.txt");
    }

        private void MagicForm_Shown(object sender, EventArgs e)
        {
            LoadParameters();            
        }

        private void LoadParameters()
        {
            if (!File.Exists(_parametersFileName))
            {
                return;
            }

            string content = File.ReadAllText(_parametersFileName);

            var data = content.Split('^');

            if (data.Length < 4 || data.Length > 5)
            {
                return;
            }

            richTextBoxServers.Lines = data[0].Split(';');
            richTextBoxServices.Lines = data[1].Split(';');

            textBoxLogin.Text = data[2];            
            checkBoxSavePassword.Checked = Convert.ToBoolean(data[3]);
            if (checkBoxSavePassword.Checked)
            {
                textBoxPassword.Text = data[4];
            }
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Password is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPassword.Focus();
                return;
            }

            SaveParameters();

            var servers = GetServersList();
            var services = GetServicesList();

            var login = textBoxLogin.Text;
            var pass = textBoxPassword.Text;

            var stoppedServices = new Dictionary<string, List<string>>();

            listBoxServicesToStart.Items.Clear();
            labelProcessInfo.Visible = true;
            foreach (var server in servers)
            {
                try
                {
                    ShowCurrentServerInfo(server);
                    stoppedServices.Add(server,
                        server.ToLower() == Environment.MachineName.ToLower()
                            ? _remoteServiceEngine.GetAllStoppedServices(server, services)
                            : _remoteServiceEngine.GetAllStoppedServices(server, login, pass, services));
                }
                catch (Exception ex)
                {
                    stoppedServices.Add(server, new List<string> { ex.Message });
                }
            }

            ShowStoppedService(stoppedServices);
            labelProcessInfo.Text = "Done";
        }

        private void SaveParameters()
        {
            var content = new StringBuilder();

            var servers = richTextBoxServers.Text.Split('\r', '\n');
            var services = richTextBoxServices.Text.Split('\r', '\n');
            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;

            content.Append(string.Join(";", servers) + "^");
            content.Append(string.Join(";", services) + "^");
            content.Append(login + "^" + checkBoxSavePassword.Checked);

            if (checkBoxSavePassword.Checked)
            {
                content.Append("^" + pass);
            }

            File.WriteAllText(_parametersFileName, content.ToString());
        }

        private void ShowCurrentServerInfo(string server)
        {
            labelProcessInfo.Text = "Current server: " + server;
            Application.DoEvents();
        }

        private void ShowStoppedService(Dictionary<string, List<string>> stoppedServices)
        {
            foreach (var stoppedService in stoppedServices)
            {
                foreach (var serviceName in stoppedService.Value)
                {
                    listBoxServicesToStart.Items.Add(stoppedService.Key + " - " + serviceName + "\r\n");
                }
            }
        }

        private List<string> GetServicesList()
        {
            var services = new List<string>();

            services.AddRange(richTextBoxServices.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            return services;
        }

        private List<string> GetServersList()
        {
            var servers = new List<string>();

            servers.AddRange(richTextBoxServers.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

            return servers;
        }

        private void buttonStartSelected_Click(object sender, EventArgs e)
        {
            StartServices(listBoxServicesToStart.SelectedItems);

            for (int i = listBoxServicesToStart.SelectedIndices.Count - 1; i >= 0; i--)
            {
                listBoxServicesToStart.Items.RemoveAt(listBoxServicesToStart.SelectedIndices[i]);
            }
        }

        private void buttonStartAll_Click(object sender, EventArgs e)
        {
            StartServices(listBoxServicesToStart.Items);

            listBoxServicesToStart.Items.Clear();
        }

        private void StartServices(IEnumerable items)
        {
            var servicesToStart = new Dictionary<string, List<string>>();

            foreach (var item in items)
            {
                AddService(item.ToString(), servicesToStart);
            }

            string login = textBoxLogin.Text;
            string pass = textBoxPassword.Text;
            var errors = _remoteServiceEngine.StartServices(servicesToStart, login, pass, labelProcessInfo);
            labelProcessInfo.Text = "Done";

            if (errors.Count > 0)
            {
                var form = new StartServicesErrorsForm(errors);
                form.Show();
            }
        }

        private void AddService(string serverAndServiceInfo, Dictionary<string, List<string>> servicesToStart)
        {
            var data = serverAndServiceInfo.Split(new [] { " - " }, StringSplitOptions.None);

            if (data.Length != 2)
            {
                return;
            }

            string serverName = data[0];
            string serviceName = data[1].TrimEnd('\r', '\n');

            if (servicesToStart.ContainsKey(serverName))
            {
                servicesToStart[serverName].Add(serviceName);
            }
            else
            {
                servicesToStart.Add(serverName, new List<string> { serviceName });
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buttonVerify_Click(sender, null);
            }
        }        

        private void buttonSetDefault_Click(object sender, EventArgs e)
        {
            if (File.Exists(_defaultSettingsFilePath))
            {
                richTextBoxServers.Text = File.ReadAllText(_defaultSettingsFilePath);
            }
            else
            {
                richTextBoxServers.Text = Environment.MachineName.ToLower() +
@"

co-osl-tenta61
co-osl-tenta62
co-osl-tenta63
co-osl-tenta64
co-osl-tenta65
co-osl-tenta66
co-osl-tenta67

co-osl-tst335
co-osl-tst336
co-osl-tenta263

co-osl-tenta05

co-osl-tenta07
co-osl-tenta10

co-osl-catiap01
co-osl-catiap02
co-osl-catiap03
co-osl-coapp02
co-osl-cosql01
co-osl-catsql01";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_defaultSettingsFilePath, richTextBoxServers.Text);
            SaveParameters();
        }
    }
}
