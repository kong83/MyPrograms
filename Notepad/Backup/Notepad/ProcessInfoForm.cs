using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Notepad
{
    public partial class ProcessInfoForm : Form
    {
        Process selectedProcess;

        public ProcessInfoForm(Process proc )
        {
            InitializeComponent();

            selectedProcess = proc;
        }

        private void ProcessInfoForm_Load(object sender, EventArgs e)
        {
            Text = "Процесс: " + selectedProcess.ProcessName;

            var addParams = new string[2];

            addParams[0] = "ExitCode";
            try
            {
                addParams[1] = selectedProcess.ExitCode.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "Id";
            try
            {
                addParams[1] = selectedProcess.Id.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);
            
            addParams[0] = "MainModule.FileVersionInfo.Comments";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.Comments;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.CompanyName";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.CompanyName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.FileDescription";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.FileDescription;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.FileMajorPart";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.FileMajorPart.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.FileMinorPart";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.FileMinorPart.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.FileName";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.FileName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.FileVersion";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.FileVersion;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.InternalName";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.InternalName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.IsDebug";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.IsDebug.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.IsPatched";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.IsPatched.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.ProductName";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.ProductName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.ProductVersion";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.ProductVersion;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.OriginalFilename";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.OriginalFilename;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileVersionInfo.Language";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileVersionInfo.Language;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.ModuleName";
            try
            {
                addParams[1] = selectedProcess.MainModule.ModuleName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainWindowTitle";
            try
            {
                addParams[1] = selectedProcess.MainWindowTitle;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "ProcessName";
            try
            {
                addParams[1] = selectedProcess.ProcessName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "MainModule.FileName";
            try
            {
                addParams[1] = selectedProcess.MainModule.FileName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "Responding";
            try
            {
                addParams[1] = selectedProcess.Responding.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            } 
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "SessionId";
            try
            {
                addParams[1] = selectedProcess.SessionId.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartTime";
            try
            {
                addParams[1] = selectedProcess.StartTime.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "Threads.Count";
            try
            {
                addParams[1] = selectedProcess.Threads.Count.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartInfo.Arguments";
            try
            {
                addParams[1] = selectedProcess.StartInfo.Arguments;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartInfo.FileName";
            try
            {
                addParams[1] = selectedProcess.StartInfo.FileName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartInfo.Verb";
            try
            {
                addParams[1] = selectedProcess.StartInfo.Verb;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartInfo.UserName";
            try
            {
                addParams[1] = selectedProcess.StartInfo.UserName;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartInfo.WorkingDirectory";
            try
            {
                addParams[1] = selectedProcess.StartInfo.WorkingDirectory;
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);

            addParams[0] = "StartInfo.CreateNoWindow";
            try
            {
                addParams[1] = selectedProcess.StartInfo.CreateNoWindow.ToString();
            }
            catch
            {
                addParams[1] = "Недоступно";
            }
            ProcessInfoList.Rows.Add(addParams);
        }


        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProcessInfoList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (ProcessInfoList.CurrentCellAddress.Y >= 0 && ProcessInfoList.CurrentCellAddress.Y < ProcessInfoList.Rows.Count)
            {
                Clipboard.SetText(ProcessInfoList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                labelInfo.Text = "Скопировано в буфер:\r\n" + Clipboard.GetText();
            }
        }

        private void ProcessInfoForm_Activated(object sender, EventArgs e)
        {
            string textFromBuffer;
            try
            {
                textFromBuffer = Clipboard.GetText();
            }
            catch
            {
                textFromBuffer = string.Empty;
            }

            if (string.IsNullOrEmpty(textFromBuffer))
            {
                labelInfo.Text = "Кликните дважды для копирования информации в буфер";
            }
            else
            {
                labelInfo.Text = "В буфере содержится:\r\n" + Clipboard.GetText();
            }
        }
    }
}
