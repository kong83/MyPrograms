using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Istrib.Sound;
using Istrib.Sound.Formats;

using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class DictophoneForm : Form
    {
        private readonly string _privateFolder;
        private readonly Mp3SoundCapture _mp3SoundCapture;
        private readonly CConfigurationEngine _configurationEngine;

        public DictophoneForm(CWorkersKeeper workersKeeper, string privateFolder)
        {
            InitializeComponent();

            _mp3SoundCapture = new Mp3SoundCapture
            {
                NormalizeVolume = false,
                OutputType = Mp3SoundCapture.Outputs.Wav,
                UseSynchronizationContext = true,
                WaitOnStop = true
            };
            _mp3SoundCapture.Starting += mp3SoundCapture_Starting;
            _mp3SoundCapture.Stopped += mp3SoundCapture_Stopped;

            _privateFolder = privateFolder;

            _configurationEngine = workersKeeper.ConfigurationEngine;
        }


        /// <summary>
        /// Инициализация при загрузке формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void DictophoneForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (_configurationEngine.DictophoneFormLocation.X >= 0 &&
                _configurationEngine.DictophoneFormLocation.Y >= 0)
                {
                    Location = _configurationEngine.DictophoneFormLocation;
                }

                textBoxPrivateFolderPath.Text = _privateFolder;

                textBoxNewFileName.Text = GetNewFileName();

                foreach (SoundCaptureDevice device in SoundCaptureDevice.AllAvailable)
                {
                    comboBoxDevices.Items.Add(device);
                }

                if (comboBoxDevices.Items.Count > 0)
                {
                    comboBoxDevices.SelectedIndex = 0;
                    labelInfo.Text = "Диктофон готов начать запись";
                }
                else
                {
                    labelInfo.Text = "Запись невозможна";
                    buttonStartRecording.Visible = false;
                    MessageBox.ShowDialog("В системе не найдено звукозаписывающее устройство. Работа диктофона невозможна.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Начало записи
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void mp3SoundCapture_Starting(object sender, EventArgs e)
        {
            buttonStartRecording.Visible = false;
            buttonStopRecording.Visible = true;

            labelInfo.Text = "Идёт запись";
            labelInfo.Font = new Font(labelInfo.Font, FontStyle.Bold);
        }


        /// <summary>
        /// Окончание записи
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void mp3SoundCapture_Stopped(object sender, Mp3SoundCapture.StoppedEventArgs e)
        {
            buttonStartRecording.Visible = true;
            buttonStopRecording.Visible = false;
            labelInfo.Text = "Диктофон готов начать запись";
            labelInfo.Font = new Font(labelInfo.Font, FontStyle.Regular);
        }


        /// <summary>
        /// Получить название файла с новой записью, которого ещё нет в личной папке
        /// </summary>
        /// <returns></returns>
        private string GetNewFileName()
        {
            const string baseFileName = "Record";
            int n = 1;

            foreach (string fullFilePth in Directory.GetFiles(_privateFolder))
            {
                string fileName = Path.GetFileNameWithoutExtension(fullFilePth) ?? string.Empty;

                if (fileName.StartsWith(baseFileName))
                {
                    int newNumber;
                    if (int.TryParse(fileName.Substring(baseFileName.Length), out newNumber) && newNumber >= n)
                    {
                        n = newNumber + 1;
                    }
                }
            }

            return baseFileName + n + ".mp3";
        }


        /// <summary>
        /// Начать запись
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonStartRecording_Click(object sender, EventArgs e)
        {
            try
            {
                string newFilePath = Path.Combine(_privateFolder, textBoxNewFileName.Text);
                if (File.Exists(newFilePath))
                {
                    if (DialogResult.Yes == MessageBox.ShowDialog("Указанный файл уже существует. Перезаписать?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    {
                        File.Delete(newFilePath);
                    }
                    else
                    {
                        return;
                    }
                }

                _mp3SoundCapture.CaptureDevice = (SoundCaptureDevice)comboBoxDevices.SelectedItem;
                _mp3SoundCapture.NormalizeVolume = true;
                _mp3SoundCapture.OutputType = Mp3SoundCapture.Outputs.Mp3;
                _mp3SoundCapture.WaveFormat =  PcmSoundFormat.Pcm22KHz16BitStereo;
                _mp3SoundCapture.Mp3BitRate = Mp3BitRate.BitRate24;
                _mp3SoundCapture.WaitOnStop = true;
                _mp3SoundCapture.Start(newFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Остановить запись
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonStopRecording_Click(object sender, EventArgs e)
        {
            try
            {
                _mp3SoundCapture.Stop();

                textBoxNewFileName.Text = GetNewFileName();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Кнопка выхода
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Остановка записи, если она идёт, при закрытии формы и сохранение местоположения формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void DictophoneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonStopRecording.Visible)
            {
                _mp3SoundCapture.Stop();
            }

            _configurationEngine.DictophoneFormLocation = Location;
        }


        #region Подсказки
        private void buttonStartRecording_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Начать запись", buttonStartRecording);
            buttonStartRecording.FlatStyle = FlatStyle.Popup;
        }

        private void buttonStartRecording_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonStartRecording.FlatStyle = FlatStyle.Flat;
        }

        private void buttonStopRecording_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Остановить запись", buttonStopRecording);
            buttonStopRecording.FlatStyle = FlatStyle.Popup;
        }

        private void buttonStopRecording_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonStopRecording.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть окно", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void textBoxPrivateFolderPath_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxPrivateFolderPath.Text.Length > 85)
            {
                CToolTipShower.Show(textBoxPrivateFolderPath.Text, textBoxPrivateFolderPath, 0, -21);
            }
        }

        private void textBoxPrivateFolderPath_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }
        #endregion
    }
}
