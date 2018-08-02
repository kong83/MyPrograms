using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LtuErrorsParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetErrors();
        }

        private void GetErrors()
        {
            var dataTable = new DataTable();

            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand("select ServerName, EventTime, Text from CatiEventLog where CompanyId = 1 and EventTypeName = 'error' order by Text", sqlConnection))
                using (var reader = sqlCommand.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }
            
            var messages = new Dictionary<string, Message>();
            foreach (DataRow dr in dataTable.Rows)
            {
                string realMessage = GetRealErorText(dr.ItemArray[2].ToString());
                if (!messages.ContainsKey(realMessage))
                {
                    messages[realMessage] = new Message(dr.ItemArray[0].ToString(), dr.ItemArray[1].ToString(), dr.ItemArray[2].ToString());                   
                }
                else
                {
                    messages[realMessage].AddCount();
                }
            }

            dataGridView.Rows.Clear();
            foreach (string realError in messages.Keys)
            {
                var array = new object[5]
                {
                    messages[realError].ServerName,
                    messages[realError].EventTime,
                    messages[realError].FullError,
                    realError,
                    messages[realError].ErrorCount
                };
                dataGridView.Rows.Add(array);
            }

            labelErrorCount.Text = "Errors count: " + dataTable.Rows.Count;
            labelErrorTypes.Text = "Error types count: " + dataGridView.Rows.Count;
        }

        private string GetRealErorText(string text)
        {
            text = RemoveChangebleThings(text);
            int startIndex;
            int length;

            startIndex = text.IndexOf("<Message>", StringComparison.Ordinal) + "<Message>".Length;
            if (startIndex > "<Message>".Length)
            {
                length = text.IndexOf("</Message>", StringComparison.Ordinal) - startIndex;
                return text.Substring(startIndex, length);
            }



            return text;
        }

        private string RemoveChangebleThings(string text)
        {
            // change ' 9/27/2013 4:47:21 ' to ' <time> '
            var regex = new Regex(@" \d+/d+/d+ \d+:\d+:\d+ ");
            text = regex.Replace(text, " <time> ");

            // change 'Thread: 149 from ' to 'Thread: <number> from '
            regex = new Regex(@"Thread: \d+ from ");
            text = regex.Replace(text, "Thread: <number> from ");

            // change 'personSID=111,' to 'personSID=<number>,'
            regex = new Regex(@"personSID=\d+,");
            text = regex.Replace(text, "personSID=<number>,");

            // change ' 00:00:00.00000.' to 'timespan'
            regex = new Regex(@" \d+:\d+:\d+.\d+.");
            text = regex.Replace(text, "<timespan>");

            return text;
        }

        private bool IsNewError(string text, List<string[]> messages)
        {
            foreach (var message in messages)
            {
                if (message[3] == text)
                {
                    return false;
                }
            }

            return true;
        }

        private string GetConnectionString()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "co-osl-cosql01",
                UserID = "sa",
                Password = "firm",
                InitialCatalog = "confirmlog"
            };
            return connectionStringBuilder.ConnectionString;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            richTextBoxDetails.Text = dataGridView.Rows[dataGridView.CurrentCellAddress.Y].Cells[2].Value.ToString();
        }

        // Changing of size of dataGrid and richTextBox
        int _saveCursorPos;
        int _saveMovingPanelPos;
        bool _startMoving;
        private void panelMove_MouseDown(object sender, MouseEventArgs e)
        {
            _startMoving = true;
            _saveCursorPos = Cursor.Position.Y;
            _saveMovingPanelPos = panelMove.Location.Y;
        }

        private void panelMove_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_startMoving)
            {
                return;
            }

            ChangeLocation();

            _startMoving = false;
        }

        private void panelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_startMoving)
            {
                return;
            }

            ChangeLocation();
        }

        private void ChangeLocation()
        {
            int shift = Cursor.Position.Y - _saveCursorPos;

            if (_saveMovingPanelPos + shift - dataGridView.Location.Y < 100 ||
                Height - (_saveMovingPanelPos + shift + panelMove.Height) - 39 < 100)
            {
                return;
            }

            panelMove.Top = _saveMovingPanelPos + shift;
            dataGridView.Size = new Size(dataGridView.Size.Width, panelMove.Location.Y - dataGridView.Location.Y);
            richTextBoxDetails.Top = panelMove.Top + panelMove.Height;
            richTextBoxDetails.Height = Height - richTextBoxDetails.Top - 39;
        }        
    }
}
