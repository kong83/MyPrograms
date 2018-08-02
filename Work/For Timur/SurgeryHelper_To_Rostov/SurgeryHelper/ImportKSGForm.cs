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

        private void buttonOpenDayKSG_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxDayKSGPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonOpenNightKSG_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxNightKSGPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxDayKSGPath.Text))
            {
                MessageBox.Show("Файл с информацией о дневных кодах КСГ не найден.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxDayKSGPath.Focus();
                return;
            }

            if (!File.Exists(textBoxNightKSGPath.Text))
            {
                MessageBox.Show("Файл с информацией о ночных кодах КСГ не найден.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNightKSGPath.Focus();
                return;
            }

            try
            {
                _dbEngine.Logger.WriteLog("Импортируем дневные коды");
                string dayKSGData = ExcelExportEngine.GetKSGDataFromFile(textBoxDayKSGPath.Text);
                _dbEngine.Logger.WriteLog("Импортируем ночные коды");
                string nightKSGData = ExcelExportEngine.GetKSGDataFromFile(textBoxNightKSGPath.Text);

                _dbEngine.Logger.WriteLog("Создаём объект DayMKBEngine");
                _dbEngine.DayMKBEngine = new MKBEngine(dayKSGData);
                _dbEngine.Logger.WriteLog("Создаём объект NightMKBEngine");
                _dbEngine.NightMKBEngine = new MKBEngine(nightKSGData);

                _dbEngine.Logger.WriteLog("Сохраняем новые объекты");
                _dbEngine.SaveKSG();

                _dbEngine.Logger.WriteLog("Закрываем окно");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonOpenDayKSG_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Выбрать путь до файла с дневными кодами КСГ", buttonOpenDayKSG, 15, -20);
            buttonOpenDayKSG.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpenDayKSG_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonOpenDayKSG);
            buttonOpenDayKSG.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOpenNightKSG_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Выбрать путь до файла с ночными кодами КСГ", buttonOpenNightKSG, 15, -20);
            buttonOpenNightKSG.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpenNightKSG_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonOpenNightKSG);
            buttonOpenNightKSG.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Импортировать новые коды КСГ\r\n(уже существующие будут заменены на новые)", buttonOk, 15, -34);
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
