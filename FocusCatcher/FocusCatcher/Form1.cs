using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Diagnostics;
using System.IO;

namespace FocusCatcher
{
    public partial class Form1 : Form
    {
        private string _lastMessage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            richTextBox.Focus();
            SubscribeToFocusChange();

            if (File.Exists("log.txt"))
            {
                File.Delete("log.txt");
            }
        }

        public void SubscribeToFocusChange()
        {
            AutomationFocusChangedEventHandler focusHandler = new AutomationFocusChangedEventHandler(OnFocusChanged);
            Automation.AddAutomationFocusChangedEventHandler(focusHandler);
        }

        private delegate void ChangeRichTextBoxDelegate(AutomationElement focusedElement);

        private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            AutomationElement focusedElement = sender as AutomationElement;
            try
            {
                this.BeginInvoke(new ChangeRichTextBoxDelegate(ChangeRichTextBox), focusedElement);
            }
            catch { }
        }

        public void ChangeRichTextBox(AutomationElement focusedElement)
        {
            if (focusedElement != null)
            {
                string newMessage = "";
                Process process = Process.GetProcessById(focusedElement.Current.ProcessId);
                try
                {
                    newMessage += process.ProcessName;
                }
                catch (Exception ex)
                {
                    newMessage += "Cann't get process name: " + ex.Message;
                }

                try
                {
                    newMessage += " - " + process.Modules[0].FileName;
                }
                catch (Exception ex)
                {
                    newMessage += "Cann't get process path: " + ex.Message;
                }

                newMessage = newMessage + "\r\n";
                if (newMessage != _lastMessage)
                {
                    richTextBox.Text += newMessage;
                    _lastMessage = newMessage;
                    File.AppendAllText("log.txt", newMessage);
                }
            }
        }
    }
}
