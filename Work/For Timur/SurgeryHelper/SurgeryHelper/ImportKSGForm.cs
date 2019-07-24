using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using System.IO;

namespace SurgeryHelper
{
    public partial class ImportKSGForm : Form
    {
        private readonly DbEngine _dbEngine;

        public ImportKSGForm(DbEngine dbEngine)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOpenNightKSG_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFilePath.Text = openFileDialog.FileName;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxFilePath.Text))
            {
                MessageBox.Show("Файл с не найден.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFilePath.Focus();
                return;
            }

            try
            {
                switch (comboBoxFileType.Text)
                {
                    case "Путь до xml файла с информацией о дневных кодах КСГ":
                        _dbEngine.Logger.WriteLog("Импортируем дневные коды");
                        string dayServiceData = ExcelExportEngine.GetServiceDataFromFile(textBoxFilePath.Text);

                        _dbEngine.Logger.WriteLog("Создаём объект DayServiceEngine");
                        _dbEngine.DayServiceEngine = new ServiceEngine(dayServiceData);

                        _dbEngine.Logger.WriteLog("Сохраняем новые услуги");
                        _dbEngine.SaveServices();
                        break;
                    case "Путь до xml файла с информацией о ночных кодах КСГ":
                        _dbEngine.Logger.WriteLog("Импортируем ночные коды");
                        string nightServiceData = ExcelExportEngine.GetServiceDataFromFile(textBoxFilePath.Text);

                        _dbEngine.Logger.WriteLog("Создаём объект NightServiceEngine");
                        _dbEngine.NightServiceEngine = new ServiceEngine(nightServiceData);

                        _dbEngine.Logger.WriteLog("Сохраняем новые услуги");
                        _dbEngine.SaveServices();
                        break;
                    case "Путь до xml файла с информацией о МКБ":
                        _dbEngine.Logger.WriteLog("Импортируем коды МКБ");
                        string mkbData = ExcelExportEngine.GetMkbDataFromFile(textBoxFilePath.Text);

                        _dbEngine.LoadMkb(mkbData);

                        _dbEngine.Logger.WriteLog("Сохраняем новые коды МКБ");
                        _dbEngine.SaveMkbs();
                        
                        break;
                    default:
                        MessageBox.Show("Не поддерживаемый режим импорта. Сообщите разработчику.", "Внутренняя ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                MessageBox.Show("Испорт успешно завершён", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOpenFilePath_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Выбрать путь до файла", buttonOpenFilePath, 15, -20);
            buttonOpenFilePath.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpenFilePath_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonOpenFilePath);
            buttonOpenFilePath.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Импортировать данные\r\n(уже существующие данные будут заменены на новые)", buttonOk, 15, -34);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Закрыть форму", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
    }
}
