using System;
using System.IO;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using System.Diagnostics;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class MainForm : Form
    {
        private PatientListForm _patientForm;

        private NosologyForm _nosologyForm;
        private SurgeonForm _surgeonForm;
        private ScrubNurseForm _scrubNurseForm;
        private OrderlyForm _orderlyForm;
        private OperationTypeForm _operationTypeForm;
        private ColorInfoForm _colorInfoForm;
        private CheckDBForm _checkDBForm;

        private readonly CMasterKey _masterKey;
        private CWorkersKeeper _workersKeeper;
        private CConfigurationEngine _configurationEngine;

        public CWorkersKeeper ForeinWorkersKeeper;
        public CWorkersKeeper WorkersKeeper
        {
            get
            {
                return _workersKeeper;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            try
            {
                _configurationEngine = new CConfigurationEngine();
            }
            catch
            {
                MessageBox.ShowDialog("Приложение не смогло обнаружить необходимые для работы файлы. Выполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            _masterKey = new CMasterKey();
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            do
            {
                if (_masterKey.HashInfoHardDisks == _masterKey.GetMasterKeyFromFile())
                {
                    break;
                }

                new PassForm("Введите мастер-пароль").ShowDialog();
                try
                {
                    if (CPassHelper.GetHash() == 8689471360457399360 || CPassHelper.GetHash() == 8522300720778874496)
                    {
                        _masterKey.CreateMasterKeyFile();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog(ex.Message, "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                if (DialogResult.No == MessageBox.ShowDialog("Введённый вами пароль - неверен. Хотите попробовать ещё раз?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Environment.Exit(0);
                }
            }
            while (true);

            do
            {
                new PassForm("Введите пароль").ShowDialog();
                try
                {
                    if (CPassHelper.GetHash() == _configurationEngine.InternalData)
                    {
                        break;
                    }
                }
                catch
                {
                    MessageBox.ShowDialog("Приложение не смогло обнаружить необходимые для работы файлы. Выполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                if (DialogResult.No == MessageBox.ShowDialog("Введённый вами пароль - неверен. Хотите попробовать ещё раз?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Environment.Exit(0);
                }
            }
            while (true);

            try
            {
                string dataPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "Data");
                _workersKeeper = new CWorkersKeeper(dataPath);
                _configurationEngine = _workersKeeper.ConfigurationEngine;
                _patientForm = new PatientListForm(_workersKeeper) { MdiParent = this };
                _patientForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog("При загрузке данных произошла непредвиденная ошибка\r\n" + ex + "\r\nВыполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// Выйти из программы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Показать информацию о программе
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }


        /// <summary>
        /// Смена пароля
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemFileChangePassword_Click(object sender, EventArgs e)
        {
            string saveOldPass = CPassHelper.PassStr;
            new ChangePasswordForm().ShowDialog();
            try
            {
                if (saveOldPass != CPassHelper.PassStr)
                {
                    _configurationEngine.InternalData = CPassHelper.GetHash();
                    MessageBox.ShowDialog("Пароль успешно изменён", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.ShowDialog("Приложение не смогло обнаружить необходимые для работы файлы. Выполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemToolsImport_Click(object sender, EventArgs e)
        {
            if (IsLockedPatientsExists())
            {
                MessageBox.ShowDialog("Закройте все окна с пациентами перед запуском мержа.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new MergeLoadForeignDataForm(this, _configurationEngine).ShowDialog();

            if (ForeinWorkersKeeper != null)
            {
                new MergeForm(_workersKeeper, ForeinWorkersKeeper).ShowDialog();
                _patientForm.ShowPatients();
            }
        }

        private bool IsLockedPatientsExists()
        {
            bool isLockedPatientsExists = false;
            foreach (CPatient patient in _workersKeeper.PatientWorker.PatientList)
            {
                if (patient.OpenedPatientViewForm != null && !patient.OpenedPatientViewForm.IsDisposed)
                {
                    isLockedPatientsExists = true;
                    break;
                }
            }

            return isLockedPatientsExists;
        }

        /// <summary>
        /// Проверка на открытые окна
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            menuItemWindowsPatientList_Click(null, null);
            
            if (IsLockedPatientsExists() &&
                DialogResult.No == MessageBox.ShowDialog("Вы уверены, что хотите закрыть программу? Все несохранённые данные будут утеряны.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
                return;
            }

            CWordExportHelper.RemoveOldMifrmFiles();
            e.Cancel = false;
        }


        /// <summary>
        /// Открыть форму с глобальными настройками
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemFileGlobalSettings_Click(object sender, EventArgs e)
        {
            var globalSettingsForm = new GlobalSettingsForm(_workersKeeper, _patientForm);
            globalSettingsForm.ShowDialog();
        }


        /// <summary>
        /// Включить негатоскоп
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemToolsNegatoskop_Click(object sender, EventArgs e)
        {
            new NegatoscopForm().ShowDialog();
        }


        /// <summary>
        /// Открыть папку с сохранёнными версиями баз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileDatabaseSaveFolder_Click(object sender, EventArgs e)
        {
            string saveDataFolder;
            CWorkersKeeper.GetSaveDataLocation(out saveDataFolder);
            if (Directory.Exists(saveDataFolder))
            {
                Process.Start("explorer.exe", saveDataFolder);
            }
            else
            {
                MessageBox.ShowDialog("Папка пока не существует", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Показать список пациентов, если его вдруг закрыли
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemWindowsPatientList_Click(object sender, EventArgs e)
        {
            if (_patientForm == null || _patientForm.IsDisposed)
            {
                _patientForm = new PatientListForm(_workersKeeper) { MdiParent = this };
                _patientForm.Show();
            }
            else
            {
                _patientForm.Visible = true;
                _patientForm.Focus();
            }
        }


        /// <summary>
        /// Показать список нозологий
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemWindowsNoslogyList_Click(object sender, EventArgs e)
        {
            _nosologyForm = new NosologyForm(_workersKeeper, IsLockedPatientsExists());
            _nosologyForm.ShowDialog();

            _patientForm.ShowPatients();
        }


        /// <summary>
        /// Показать список хирургов
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemWindowsSurgeonList_Click(object sender, EventArgs e)
        {
            _surgeonForm = new SurgeonForm(_workersKeeper, this);
            _surgeonForm.ShowDialog();
        }


        /// <summary>
        /// Показать список мед. сестёр
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemWindowsScrubNurseList_Click(object sender, EventArgs e)
        {
            _scrubNurseForm = new ScrubNurseForm(_workersKeeper);
            _scrubNurseForm.ShowDialog();
        }


        /// <summary>
        /// Показать список санитаров
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemWindowsOrderlyList_Click(object sender, EventArgs e)
        {
            _orderlyForm = new OrderlyForm(_workersKeeper);
            _orderlyForm.ShowDialog();
        }


        /// <summary>
        /// Показать список типов операций
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemWindowsOperationTypeList_Click(object sender, EventArgs e)
        {
            _operationTypeForm = new OperationTypeForm(_workersKeeper);
            _operationTypeForm.ShowDialog();
        }


        /// <summary>
        /// Открыть форму с информацией о раскраске строк
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemFileColorInfo_Click(object sender, EventArgs e)
        {
            if (_colorInfoForm == null || _colorInfoForm.IsDisposed)
            {
                _colorInfoForm = new ColorInfoForm(_workersKeeper, _patientForm) { MdiParent = this };
                _colorInfoForm.Show();
            }
            else
            {
                _colorInfoForm.Focus();
            }
        }


        /// <summary>
        /// Проверить базу данных на правильность
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void menuItemToolsCheckDB_Click(object sender, EventArgs e)
        {
            _checkDBForm = new CheckDBForm(_workersKeeper);
            _checkDBForm.ShowDialog();

            _patientForm.ShowPatients();
        }
    }
}
