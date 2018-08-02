using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ProcessRunner
{
    public partial class MainForm : Form
    {
        private int _programCopyCount;
        private int _workTime;
        private string _programPath;
        private Process[] _runnedProcesses;

        public MainForm()
        {
            InitializeComponent();
        }        

        private void ButtonRunClick(object sender, EventArgs e)
        {
            try
            {
                CreateTestDbIfNeeded();

                _programCopyCount = (int)numericUpDownCopiesCount.Value;
                _workTime = (int)numericUpDownWorkTime.Value * 60;
                _programPath = textBoxFilePath.Text;

                if (!File.Exists(_programPath))
                {
                    MessageBox.Show("Program path is wrong.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RunAllInstances();

                buttonStop.Visible = labelRemainingTimeInfo.Visible = true;
                buttonRun.Visible = false;

                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\r\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonStopClick(object sender, EventArgs e)
        {
            _workTime = 0;
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            StopAllInstances();

            Close();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                _workTime--;
                labelRemainingTimeInfo.Text = string.Format("Remaining Time: {0}:{1}", _workTime / 60, (_workTime % 60).ToString("00"));
                labelWorkingSetMemoryInfo.Text = string.Format("Sum of \"Working Set (Memory)\": {0} K", GetUsedWorkingSetMemory());
                labelCommitSizeMemoryInfo.Text = string.Format("Sum of \"Commit Size\": {0} K", GetUsedCommitSize());

                if (_workTime <= 0)
                {
                    timer.Enabled = false;

                    buttonStop.Visible = labelRemainingTimeInfo.Visible = false;
                    buttonRun.Visible = true;

                    StopAllInstances();
                }
            }
            catch (Exception ex)
            {
                timer.Enabled = false;
                MessageBox.Show("Unexpected error:\r\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunAllInstances()
        {
            _runnedProcesses = new Process[_programCopyCount];
            for (int i = 0; i < _programCopyCount; i++)
            {
                _runnedProcesses[i] = RunInstance();
            }
        }

        private Process RunInstance()
        {
            var scriptProcess = new Process();

            var pinfo = new ProcessStartInfo(_programPath)
            {
                CreateNoWindow = !checkBoxShowWindows.Checked,
                UseShellExecute = false
            };

            scriptProcess.StartInfo = pinfo;

            scriptProcess.Start();

            Thread.Sleep(100);

            return scriptProcess;
        }

        private void StopAllInstances()
        {
            for (int i = 0; i < _programCopyCount; i++)
            {
                if (!_runnedProcesses[i].HasExited)
                {
                    _runnedProcesses[i].Kill();
                }
            }
        }

        private int GetUsedWorkingSetMemory()
        {
            long usageMemorySize = 0;

            for (int i = 0; i < _programCopyCount; i++)
            {
                if (!_runnedProcesses[i].HasExited)
                {
                    _runnedProcesses[i].Refresh();
                    usageMemorySize += _runnedProcesses[i].WorkingSet64;
                }
            }

            return (int)(usageMemorySize / 1024);
        }

        private int GetUsedCommitSize()
        {
            long usageMemorySize = 0;

            for (int i = 0; i < _programCopyCount; i++)
            {
                if (!_runnedProcesses[i].HasExited)
                {
                    usageMemorySize += _runnedProcesses[i].PrivateMemorySize64;
                }
            }

            return (int)(usageMemorySize / 1024);
        }

        private void CreateTestDbIfNeeded()
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();

                const string query = "SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE (name = 'TestDatabase_12321')";
                bool isTestTableExists;

                using (var cmd = new SqlCommand(query, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    isTestTableExists = ((int)cmd.ExecuteScalar() == 1);
                }

                if (isTestTableExists)
                {
                    using (var cmd = new SqlCommand("DELETE FROM TestDatabase_12321.dbo.TestTable", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("CREATE DATABASE TestDatabase_12321", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("CREATE TABLE TestDatabase_12321.dbo.TestTable ([id] [int] NOT NULL)", cn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Create connection string for current server settings
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            var cnStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                IntegratedSecurity = true
            };

            return cnStringBuilder.ToString();
        }

        private void ButtonCopyWorkingSetClick(object sender, EventArgs e)
        {
            Clipboard.SetText(labelWorkingSetMemoryInfo.Text);
        }

        private void ButtonCopyCommitSizeClick(object sender, EventArgs e)
        {
            Clipboard.SetText(labelCommitSizeMemoryInfo.Text);
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you shure you want to close the application?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /*
        private void ButtonTestClick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(textBoxProcessName.Text);

            if (processes.Length > 0)
            {
                //PrivateMemorySize64
                //WorkingSet64
                labelTestInfo.Text = "Memory used: " + processes[0].PrivateMemorySize64 / 1024 + ", " + processes[0].WorkingSet64 / 1024;

            }
            else
            {
                labelTestInfo.Text = "No process was found";
            }
        }

        private void buttonTest2_Click(object sender, EventArgs e)
        {
            var performanceCounter = new PerformanceCounter
            {
                CategoryName = "Process",
                CounterName = "Working Set - Private",
                InstanceName = textBoxProcessName.Text
            };

            try
            {
                labelTestInfo.Text = ((uint)performanceCounter.NextValue() / 1024).ToString("N0");
            }
            catch (Exception ex)
            {
                labelTestInfo.Text = ex.Message;
            }
            
        }*/





        /*
         
         */
    }
}
