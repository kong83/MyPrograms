using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace PsychologicalTestsManager
{
    public partial class ConfigureConfigForm : Form
    {
        private readonly string _configFilePath;

        public ConfigureConfigForm()
        {
            InitializeComponent();

            _configFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, @"PsychologicalTests\PsychologicalTests.exe.config");
        }

        private void buttonConfigure_Click(object sender, EventArgs e)
        {
            ConfigureConfig();
        }

        private void ConfigureConfig()
        {
            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };
            xmlDocument.Load(_configFilePath);

            var nsMgr = new XmlNamespaceManager(xmlDocument.NameTable);

            SetValue(xmlDocument, "//configuration/userSettings/PsychologicalTests.Properties.Settings/setting[@name='TestName']/value", comboBoxTestName.Text, nsMgr);
            SetValue(xmlDocument, "//configuration/userSettings/PsychologicalTests.Properties.Settings/setting[@name='TestDisplayName']/value", textBoxTestDisplayName.Text, nsMgr);
            SetValue(xmlDocument, "//configuration/userSettings/PsychologicalTests.Properties.Settings/setting[@name='TestSavePath']/value", textBoxSavePath.Text, nsMgr);

            xmlDocument.PreserveWhitespace = false;
            xmlDocument.Save(_configFilePath);

            MessageBox.Show("Настройка конфиг файла успешно завершена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void SetValue(XmlDocument xmlDocument, string xpath, string attributeValue, XmlNamespaceManager nsmgr)
        {
            var selectedNode = (XmlElement)xmlDocument.SelectSingleNode(xpath, nsmgr);

            if (selectedNode != null)
            {
                selectedNode.InnerText = attributeValue;
            }
        }

        private string GetValue(XmlDocument xmlDocument, string xpath, XmlNamespaceManager nsmgr)
        {
            var selectedNode = (XmlElement)xmlDocument.SelectSingleNode(xpath, nsmgr);

            if (selectedNode != null)
            {
                return selectedNode.InnerText;
            }

            return null;
        }

        private void ConfigureConfigForm_Shown(object sender, EventArgs e)
        {
            if (!File.Exists(_configFilePath))
            {
                buttonConfigure.Enabled = false;
                MessageBox.Show("Конфигурационный файл для программы PsychologicalTests не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };
            xmlDocument.Load(_configFilePath);

            var nsMgr = new XmlNamespaceManager(xmlDocument.NameTable);

            comboBoxTestName.Text = GetValue(xmlDocument, "//configuration/userSettings/PsychologicalTests.Properties.Settings/setting[@name='TestName']/value", nsMgr);
            textBoxTestDisplayName.Text = GetValue(xmlDocument, "//configuration/userSettings/PsychologicalTests.Properties.Settings/setting[@name='TestDisplayName']/value", nsMgr);
            textBoxSavePath.Text = GetValue(xmlDocument, "//configuration/userSettings/PsychologicalTests.Properties.Settings/setting[@name='TestSavePath']/value", nsMgr);
        }
    }
}
