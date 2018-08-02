using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class MainForm : Form
    {
        private PatientListForm _patientForm;
        private readonly DbEngine _dbEngine;
        private readonly MasterKeyEngine _masterKey;

        private ImportDataForm _importDataForm;

        public MainForm()
        {
            InitializeComponent();
            
            try
            {
                _dbEngine = new DbEngine();
            }
            catch
            {
                MessageBox.Show("Приложение не смогло обнаружить необходимые для работы файлы. Выполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            _masterKey = new MasterKeyEngine();
        }

        

        private void MainForm_Load(object sender, EventArgs e)
        {
            do
            {
                new PassForm(_dbEngine, "Введите пароль").ShowDialog();
                try
                {
                    if (DbEngine.GetHash(_dbEngine.PassStr) == _dbEngine.ConfigEngine.InternalData) 
                    {
                        break;
                    }
                }
                catch
                {
                    MessageBox.Show("Приложение не смогло обнаружить необходимые для работы файлы. Выполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                if (DialogResult.No == MessageBox.Show("Введённый вами пароль - неверен. Хотите попробовать ещё раз?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Environment.Exit(0);
                }
            }
            while (true);

            _patientForm = new PatientListForm(_dbEngine) { MdiParent = this };

            if (_masterKey.HashInfoHardDisks == _masterKey.GetMasterKeyFromFile())
            {
                _dbEngine.LoadData();
            }
            else
            {
                MessageBox.Show("Подходящий мастер-файл не обнаружен. Зарегистрируйте программу для её дальнейшего использования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                menuItemHelpRegistration_Click(null, null);
            }
            
            _patientForm.Show();
        }

        /// <summary>
        /// Выйти из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Показать список пациентов, если его вдруг закрыли
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFilePatientList_Click(object sender, EventArgs e)
        {
            if (_patientForm.IsDisposed)
            {
                _patientForm = new PatientListForm(_dbEngine) { MdiParent = this };
                _patientForm.Show();
            }
            else
            {
                _patientForm.Visible = true;
                _patientForm.Focus();
            }
        }

        /// <summary>
        /// Смена пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileChangePassword_Click(object sender, EventArgs e)
        {
            if (_masterKey.HashInfoHardDisks != _masterKey.GetMasterKeyFromFile())
            {
                MessageBox.Show("Программа не зарегистрирована. Для изменения пароля зарегистрируйте программу.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string saveOldPass = _dbEngine.PassStr;
            new ChangePasswordForm(_dbEngine).ShowDialog();
            try
            {
                if (saveOldPass != _dbEngine.PassStr)
                {
                    _dbEngine.ConfigEngine.InternalData = DbEngine.GetHash(_dbEngine.PassStr);
                    MessageBox.Show("Пароль успешно изменён", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Приложение не смогло обнаружить необходимые для работы файлы. Выполнение завершено.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileImport_Click(object sender, EventArgs e)
        {
            if (_masterKey.HashInfoHardDisks != _masterKey.GetMasterKeyFromFile())
            {
                MessageBox.Show("Программа не зарегистрирована. Для импорта данных зарегистрируйте программу.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_importDataForm == null || _importDataForm.IsDisposed)
            {
                _importDataForm = new ImportDataForm(_patientForm, _dbEngine) { MdiParent = this };
                _importDataForm.Show();
            }
            else
            {
                _importDataForm.Focus();
            }
        }

        /// <summary>
        /// Проверка на открытые окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            menuItemFilePatientList_Click(null, null);

            bool isLockedPatientsExists = false;
            _dbEngine.GeneratePatientList();
            foreach (PatientClass pc in _dbEngine.PatientList)
            {
                if (pc.OpenedPatientViewForm != null && !pc.OpenedPatientViewForm.IsDisposed)
                {
                    isLockedPatientsExists = true;
                    break;
                }
            }

            if (isLockedPatientsExists &&
                DialogResult.No == MessageBox.Show("Вы уверены, что хотите закрыть программу? Все несохранённые данные будут утеряны.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
                return;
            }

            e.Cancel = false;
        }

        private void menuItemFileGlobalSettings_Click(object sender, EventArgs e)
        {
            if (_masterKey.HashInfoHardDisks != _masterKey.GetMasterKeyFromFile())
            {
                MessageBox.Show("Программа не зарегистрирована. Для изменения настроек зарегистрируйте программу.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var globalSettingsForm = new GlobalSettingsForm(_dbEngine);
            globalSettingsForm.ShowDialog();
        }
       
        /// <summary>
        /// Показать информацию о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        /// <summary>
        /// Показать форму с регистрацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemHelpRegistration_Click(object sender, EventArgs e)
        {
            new RegistrationForm(_masterKey).ShowDialog();

            if (_masterKey.HashInfoHardDisks == _masterKey.GetMasterKeyFromFile())
            {
                _dbEngine.LoadData();
                _patientForm.ShowPatients();
            }
        }
    }
}
