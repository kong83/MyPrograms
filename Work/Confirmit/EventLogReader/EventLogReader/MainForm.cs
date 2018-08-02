using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Threading;

namespace EventLogReader
{
    public partial class MainForm : Form
    {
        const string SplitStr = "^!@#^";
        private ConfigurationEngine mConfigurationEngine;

        public MainForm()
        {
            InitializeComponent();

            mConfigurationEngine = new ConfigurationEngine();
        }

        private int FindEntryNumberBeforeNeeded(int step, int firstNumber, EventLog eventLog)
        { 
            while (firstNumber < eventLog.Entries.Count &&
                !IsDateMatch(eventLog.Entries[firstNumber].TimeWritten, Condition.Start))
            {
                firstNumber += step;
            }

            return Math.Max(0, firstNumber - step);
        }

        private int FindEntryNumberAfterNeeded(int step, int lastNumber, EventLog eventLog)
        {            
            while (lastNumber > 0 &&
                !IsDateMatch(eventLog.Entries[lastNumber].TimeWritten, Condition.End))
            {
                lastNumber -= step;
            }
            
            return Math.Min(eventLog.Entries.Count - 1, lastNumber + step);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                string eventLogName = comboBoxEventlogName.Text;
                string computerName = textBoxComputerName.Text;
                string textFileName = textBoxFileName.Text;

                if (!EventLog.Exists(eventLogName, computerName))
                {
                    MessageBox.Show("Event Log " + eventLogName + " on computer " + computerName + "  isn't finded.");
                    return;
                }

                if (radioButton1.Checked)
                {
                    if (File.Exists(textFileName))
                    {
                        if (DialogResult.No == MessageBox.Show("This file already exists, override it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            return;
                        }
                        File.Delete(textFileName);
                    }
                    else
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(textFileName)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(textFileName));
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(textBoxFileName.Text))
                    {
                        sw.Write("");
                    }
                }

                // Get first number and last number from event log for our date

                labelInfo.Text = "Action: Search of needed events";
                Application.DoEvents();
                var errorsList = new Dictionary<string, int>();
                var eventLog = new EventLog(eventLogName, computerName);
                int firstNumber = 0;
                for (int i = 10000; i >= 1; i /= 10)
                {
                    firstNumber = FindEntryNumberBeforeNeeded(i, firstNumber, eventLog);
                }

                if (!IsDateMatch(eventLog.Entries[firstNumber].TimeWritten, Condition.Both))
                {
                    firstNumber++;
                }

                if (firstNumber == eventLog.Entries.Count)
                {
                    MessageBox.Show(firstNumber + ": Event log hasn't mesages for selected period", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int lastMumber = eventLog.Entries.Count - 1;
                for (int i = 10000; i >= 1; i /= 10)
                {
                    lastMumber = FindEntryNumberAfterNeeded(i, lastMumber, eventLog);
                }

                if (!IsDateMatch(eventLog.Entries[lastMumber].TimeWritten, Condition.Both))
                {
                    lastMumber--;
                }

                if (lastMumber == 0)
                {
                    MessageBox.Show(lastMumber + ": Event log hasn't mesages for selected period", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show(string.Format(
                    "Start searching beetween {0} (number {1}) and {2} (number {3})", 
                    eventLog.Entries[firstNumber].TimeWritten, 
                    firstNumber,
                    eventLog.Entries[lastMumber].TimeWritten,
                    lastMumber));

                // Get errors
                labelInfo.Text = "Action: Get errors";
                Application.DoEvents();
                int cnt = 0;
                progressBar1.Value = 0;
                progressBar1.Maximum = lastMumber - firstNumber + 1;

                for (int i = firstNumber; i <= lastMumber && i < eventLog.Entries.Count; i++)
                {
                    EventLogEntry eventLogEntry = eventLog.Entries[i];

                    if (eventLogEntry.EntryType == EventLogEntryType.Error)
                    {
                        string message = GetNormalizeMessage(eventLogEntry.Message);                        

                        if (errorsList.ContainsKey(message))
                        {
                            errorsList[message]++;
                        }
                        else
                        {
                            errorsList.Add(message, 1);
                        }
                    }

                    cnt++;
                    progressBar1.Value = cnt;
                    if (cnt % 10 == 0)
                    {
                        Application.DoEvents();
                    }
                }

                // Sort errors
                labelInfo.Text = "Action: Sort errors";
                Application.DoEvents();
                var sortErrorsList = new Dictionary<string, int>();
                var keys = new List<string>(errorsList.Keys.ToArray());
                var values = new List<int>(errorsList.Values.ToArray());
                while (keys.Count > 0)
                {
                    int n = 0;
                    for (int j = 1; j < keys.Count; j++)
                    {
                        if (string.Compare(keys[n], keys[j]) < 0)
                        {
                            n = j;
                        }
                    }

                    sortErrorsList.Add(keys[n], values[n]);
                    keys.RemoveAt(n);
                    values.RemoveAt(n);                    
                }
                
                if (radioButton1.Checked)
                {
                    PutDateToTextFile(sortErrorsList);
                }
                else
                {
                    PutDataToExcel(sortErrorsList);
                }

                progressBar1.Value = 0;
                labelInfo.Text = "Action: Have done";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReplaceStr(ref string message, string findStartStr, string[] findEndStr, string newStr)
        {
            int a = message.IndexOf(findStartStr);
            int b = message.IndexOf(findEndStr[0], a);
            for (int i = 0; i < findEndStr.Length; i++)
            {
                if (message.IndexOf(findEndStr[i], a) != -1)
                {
                    b = Math.Min(b, message.IndexOf(findEndStr[i], a));
                }
            }

            if (b == -1 || a == -1)
            {
                throw new Exception("Problem with message:\r\n" + message + "\r\n\r\nfindStartStr:\r\n" + findStartStr);
            }

            string oldStr = message.Substring(a, b - a);
            if (oldStr.Length - newStr.Length > 100)
            {
                throw new Exception("message = " + message + "\r\noldStr = " + oldStr + "\r\nnewStr = " + newStr);
            }

            message = message.Replace(oldStr, newStr);
        }

        private string GetNormalizeMessage(string originalMessage)
        {
            string message;
            if (originalMessage.Contains("----------"))
            {
                message = originalMessage.Substring(0, originalMessage.IndexOf("----------")).Trim(new[] { '\r', '\n' });
            }
            else
            {
                message = originalMessage;
            }

            RemoveDate(ref message, 0);

            if (message.EndsWith("]") &&
                message.Length - message.LastIndexOf('[') < 5)
            {
                message = message.Substring(0, message.LastIndexOf('['));
            }

            var endStrs = new[] { ",", ")" };

            if (message.Contains("contactId"))
            {
                ReplaceStr(ref message, "contactId", endStrs, "contactId=<number>"); 
            }

            if (message.Contains("agentId"))
            {
                ReplaceStr(ref message, "agentId", endStrs, "agentId=<number>");
            }

            if (message.Contains("callId"))
            {
                ReplaceStr(ref message, "callId", endStrs, "callId=<number>");
            }

            if (message.Contains("surveyId"))
            {
                ReplaceStr(ref message, "surveyId", endStrs, "surveyId=<number>");
            }

            if (message.Contains("userId"))
            {
                ReplaceStr(ref message, "userId", endStrs, "userId=<number>");
            }

            if (message.Contains("agentExtension"))
            {
                ReplaceStr(ref message, "agentExtension", endStrs, "agentExtension=<number>");
            }

            if (message.Contains("campaignId"))
            {
                ReplaceStr(ref message, "campaignId", endStrs, "campaignId=<number>");
            }
            
            if (message.Contains("resourceNameOrPhoneNumber"))
            {
                ReplaceStr(ref message, "resourceNameOrPhoneNumber", endStrs, "resourceNameOrPhoneNumber=<number>");
            }

            if (message.Contains("terminalName"))
            {
                ReplaceStr(ref message, "terminalName", endStrs, "terminalName=<number>");
            }
             
            if (message.Contains("User Identity Name: "))
            {
                ReplaceStr(ref message, "User Identity Name: ", new[] { "\r\n" }, "User Identity Name: <interName>");
            }           

            if (message.Contains("Interviewer ") &&
                message.Contains(" is not logged in."))
            {
                ReplaceStr(ref message, "Interviewer ", new[] { " is not logged in." }, "Interviewer <number>");
            }

            string findStr = "The following information is part of the event:'";
            if (message.Contains(findStr))
            {
                RemoveDate(ref message, message.IndexOf(findStr) + findStr.Length);
            }
            
            return message;
        }

        private void RemoveDate(ref string message, int n)
        {
            int temp;
            if (int.TryParse(message.Substring(n, 4), out temp) &&
                message[n + 4] == '-' &&
                int.TryParse(message.Substring(n + 5, 2), out temp) &&
                message[n + 7] == '-' &&
                int.TryParse(message.Substring(n + 8, 2), out temp) &&
                message[n + 10] == ' ' &&
                int.TryParse(message.Substring(n + 11, 2), out temp) &&
                message[n + 13] == ':' &&
                int.TryParse(message.Substring(n + 14, 2), out temp) &&
                message[n + 16] == ':' &&
                int.TryParse(message.Substring(n + 17, 2), out temp))
            {
                int end = n + 20;
                while (int.TryParse(message[end].ToString(), out temp))
                {
                    end++;
                }

                message = message.Substring(0, n) + "<dateTime>" + message.Substring(end);
            }
        }

        /// <summary>
        /// Put collected errors to text file
        /// </summary>
        /// <param name="errorsList"></param>
        private void PutDateToTextFile(Dictionary<string, int> errorsList)
        {
            if (errorsList.Count == 0)
            {
                MessageBox.Show("No errors for selected period", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            labelInfo.Text = "Action: Put errors to text file";
            Application.DoEvents();
            using (StreamWriter sw = new StreamWriter(textBoxFileName.Text, false, Encoding.GetEncoding("Windows-1251")))
            {
                foreach (string message in errorsList.Keys)
                {
                    sw.Write(message + SplitStr + errorsList[message] + SplitStr);
                }
            }
        }

        Excel.Application m_OXl;
        Excel._Workbook m_OWb;
        Excel._Worksheet m_OWs;
        Excel.Range m_OWr;

        /// <summary>
        /// Create excel document and put collected errors to it
        /// </summary>
        /// <param name="errorsList"></param>
        private void PutDataToExcel(Dictionary<string, int> errorsList)
        {
            if (errorsList.Count == 0)
            {
                MessageBox.Show("No errors for selected period", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            labelInfo.Text = "Action: Put errors to excel document";
            Application.DoEvents();
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");     

                // Стартуем Excel-приложение
                m_OXl = new Excel.Application();

                // Создаем новую книгу
                m_OWb = m_OXl.Workbooks.Add(Missing.Value);

                m_OWs = (Excel._Worksheet)m_OWb.Sheets[3];
                m_OWs.Delete();
                m_OWs = (Excel._Worksheet)m_OWb.Sheets[2];
                m_OWs.Delete();

                m_OWs = (Excel._Worksheet)m_OWb.Sheets[1];

                m_OWs.Cells.WrapText = false;
                m_OWs.Cells.VerticalAlignment = 1;
                m_OWs.Cells.HorizontalAlignment = 2;                

                m_OWr = m_OWs.get_Range("A1", "B1");
                m_OWr.MergeCells = true;
                m_OWr.Font.Bold = true;
                m_OWr.Font.Size = 14;
                m_OWr.RowHeight = 30;
                m_OWr.HorizontalAlignment = 3;
                string computerName = textBoxComputerName.Text;
                if (computerName == "." || computerName.ToLower() == "localhost")
                {
                    computerName = Environment.MachineName;
                }

                m_OWs.Cells[1, 1] = "Errors on computer \"" + computerName + "\" between " + dateTimePickerStartDate.Value.ToString("dd.MM.yyyy HH:mm:ss") + " and " + dateTimePickerEndDate.Value.ToString("dd.MM.yyyy HH:mm:ss");

                m_OWr = m_OWs.get_Range("A2", "A2");
                m_OWr.ColumnWidth = 146;                
                m_OWr.Font.Bold = true;
                m_OWr.HorizontalAlignment = 3;
                m_OWr.Value2 = "Error message";

                m_OWr = m_OWs.get_Range("B2", "B2");
                m_OWr.ColumnWidth = 12;                
                m_OWr.Font.Bold = true;
                m_OWr.HorizontalAlignment = 3;
                m_OWr.Value2 = "Count of error";

                int i = 3;
                foreach (string error in errorsList.Keys)
                {
                    m_OWr = m_OWs.get_Range(m_OWs.Cells[i, 1], m_OWs.Cells[i, 1]);
                    m_OWr.RowHeight = 70;
                    m_OWs.Cells[i, 1] = error;
                    m_OWs.Cells[i, 2] = errorsList[error];
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (m_OXl != null)
                {
                    m_OXl.Visible = true;
                    m_OXl.UserControl = true;

                    if (m_OWb != null)
                    {
                        Marshal.ReleaseComObject(m_OWb);
                        m_OWb = null;
                    }

                    if (m_OWs != null)
                    {
                        Marshal.ReleaseComObject(m_OWs);
                        m_OWs = null;
                    }

                    if (m_OWr != null)
                    {
                        Marshal.ReleaseComObject(m_OWr);
                        m_OWr = null;
                    }

                    Marshal.ReleaseComObject(m_OXl);
                    m_OXl = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }
        private enum Condition
        {
            Start,
            End,
            Both
        }

        /// <summary>
        /// Check if current dateTime matchs between dateTimePickerStartDate and dateTimePickerEndDate
        /// </summary>
        /// <param name="dateTime">Current date time value</param>
        /// <returns></returns>
        private bool IsDateMatch(DateTime dateTime, Condition condition)
        {
            if ((condition == Condition.Both || condition == Condition.Start) && 
                dateTimePickerStartDate.Checked &&
                DateTime.Compare(dateTimePickerStartDate.Value, dateTime) > 0)
            {
                return false;
            }

            if ((condition == Condition.Both || condition == Condition.End) && 
                dateTimePickerEndDate.Checked &&
                DateTime.Compare(dateTimePickerEndDate.Value, dateTime) < 0)
            {
                return false;
            }

            return true;
        }

        string mSaveComputerName = "";

        /// <summary>
        /// Get avalaible even logs from selected computer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            try
            {
                if (mSaveComputerName != textBoxComputerName.Text)
                {
                    mSaveComputerName = textBoxComputerName.Text;

                    comboBoxEventlogName.Items.Clear();

                    foreach (EventLog eventLog in EventLog.GetEventLogs(mSaveComputerName))
                    {
                        comboBoxEventlogName.Items.Add(eventLog.Log);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get errors from text file and put it to excel document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {                
                string allText;
                using (StreamReader sr = new StreamReader(textBoxFileName.Text, Encoding.GetEncoding("Windows-1251")))
                {
                    allText = sr.ReadToEnd();                    
                }

                string[] allItems = allText.Split(new [] { SplitStr }, StringSplitOptions.RemoveEmptyEntries);

                if (allItems.Length % 2 != 0)
                {
                    throw new Exception("Wrong entry counts");
                }

                var errorsList = new Dictionary<string, int>();
                for (int i = 0; i < allItems.Length; i+=2)
                {
                    //string res = GetNormalizeMessage(allItems[i]);
                    errorsList.Add(allItems[i], Convert.ToInt32(allItems[i + 1]));
                }

                PutDataToExcel(errorsList);

                labelInfo.Text = "Action: Have Done";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool mStopSaving;

        private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            if (dateTimePickerStartDate.Checked)
            {
                mConfigurationEngine.DateStart = dateTimePickerStartDate.Value;
            }
            else
            {
                mConfigurationEngine.DateStart = null;
            }
        }

        private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            if (dateTimePickerEndDate.Checked)
            {
                mConfigurationEngine.DateEnd = dateTimePickerEndDate.Value;
            }
            else
            {
                mConfigurationEngine.DateEnd = null;
            }
        }

        private void textBoxComputerName_TextChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            mConfigurationEngine.ComputerName = textBoxComputerName.Text;
        }

        private void comboBoxEventlogName_TextChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            mConfigurationEngine.EventLogName = comboBoxEventlogName.Text;
        }

        private void textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            mConfigurationEngine.TextFileName = textBoxFileName.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            mConfigurationEngine.PutToExcelOrTextFile = Addressee.Excel;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (mStopSaving)
            {
                return;
            }

            mConfigurationEngine.PutToExcelOrTextFile = Addressee.TextFile;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mStopSaving = true;

            if (!mConfigurationEngine.DateStart.HasValue)
            {
                dateTimePickerStartDate.Checked = false;
            }
            else
            {
                dateTimePickerStartDate.Value = mConfigurationEngine.DateStart.Value;
            }

            if (!mConfigurationEngine.DateEnd.HasValue)
            {
                dateTimePickerEndDate.Checked = false;
            }
            else
            {
                dateTimePickerEndDate.Value = mConfigurationEngine.DateEnd.Value;
            }

            textBoxComputerName.Text = mConfigurationEngine.ComputerName;
            textBoxFileName.Text = mConfigurationEngine.TextFileName;
            comboBox1_Enter(null, null);
            comboBoxEventlogName.Text = mConfigurationEngine.EventLogName;

            if (mConfigurationEngine.PutToExcelOrTextFile == Addressee.Excel)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            mStopSaving = false;
        }
    }
}
