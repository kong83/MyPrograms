using System;
using System.Drawing;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class ObstetricHistoryForm : Form
    {
        private readonly CObstetricHistory _obstetricHistoryInfo;
        private readonly CObstetricHistoryWorker _obstetricHistoryWorker;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public ObstetricHistoryForm(
            CWorkersKeeper workersKeeper,
            CObstetricHistory obstetricHistory)
        {
            InitializeComponent();

            _obstetricHistoryInfo = obstetricHistory;
            _obstetricHistoryWorker = workersKeeper.ObstetricHistoryWorker;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }


        /// <summary>
        /// Сконвертировать номер по порядку в тот номер, который должен отобразиться в табличке
        /// </summary>
        /// <param name="i">Номер по порядку</param>
        /// <returns></returns>
        private static string ConvertIToCorrectNumber(int i)
        {
            switch (i)
            {
                case 0:
                    return "1";
                case 1:
                    return "2";
                case 2:
                    return "3";
                case 3:
                    return "4";
                case 4:
                    return "6";
                case 5:
                    return "10";
                default:
                    return "12";
            }
        }


        /// <summary>
        /// Обработка событий при загрузке формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ObstetricHistoryForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.ObstetricHistoryFormLocation.X >= 0 &&
                _configurationEngine.ObstetricHistoryFormLocation.Y >= 0)
            {
                Location = _configurationEngine.ObstetricHistoryFormLocation;
            }
            
            for (int i = 0; i < 7; i++)
            {
                var param = new object[7];
                param[0] = ConvertIToCorrectNumber(i);
                for (int j = 1; j < 7; j++)
                {
                    if (_obstetricHistoryInfo.Chronology[GetIndexForCell(i, j)])
                    {
                        param[j] = Properties.Resources.mark;
                    }
                    else
                    {
                        param[j] = new Bitmap(1, 1);
                    }
                }

                dataGridChronology.Rows.Add(param);
            }

            textBoxApgarScore.Text = _obstetricHistoryInfo.ApgarScore;
            textBoxChildbirthWeeks.Text = _obstetricHistoryInfo.ChildbirthWeeks;
            textBoxComplicationsInPregnancy.Text = _obstetricHistoryInfo.ComplicationsInPregnancy;
            textBoxDrugsInPregnancy.Text = _obstetricHistoryInfo.DrugsInPregnancy;
            textBoxDurationOfLabor.Text = _obstetricHistoryInfo.DurationOfLabor;
            textBoxHeightAtBirth.Text = _obstetricHistoryInfo.HeightAtBirth;
            textBoxHospitalTreatment.Text = _obstetricHistoryInfo.HospitalTreatment;
            textBoxObstetricParalysis.Text = _obstetricHistoryInfo.ObstetricParalysis;
            textBoxOutpatientCare.Text = _obstetricHistoryInfo.OutpatientCare;
            textBoxWeightAtBirth.Text = _obstetricHistoryInfo.WeightAtBirth;

            comboBoxBirthInjury.Text = _obstetricHistoryInfo.BirthInjury;
            comboBoxComplicationsDuringChildbirth.Text = _obstetricHistoryInfo.ComplicationsDuringChildbirth;
            comboBoxFetal.Text = _obstetricHistoryInfo.Fetal;
            comboBoxIsTongsUsing.Text = _obstetricHistoryInfo.IsTongsUsing
                ? "да"
                : "нет";
            comboBoxIsVacuumUsing.Text = _obstetricHistoryInfo.IsVacuumUsing
                ? "да"
                : "нет";
        }



        /// <summary>
        /// Делаем небольшой финт ушами, чтобы красиво отображалась табличка после создания
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ObstetricHistoryForm_Shown(object sender, EventArgs e)
        {
            dataGridChronology.CurrentCell = dataGridChronology.Rows[1].Cells[0];
            dataGridChronology.Refresh();

            dataGridChronology.CurrentCell = dataGridChronology.Rows[0].Cells[0];
            dataGridChronology.Refresh();
        }


        /// <summary>
        /// Положить введённые данные в акушерский анамнез
        /// </summary>        
        private void PutDataToObstetricHistory()
        {
            _obstetricHistoryInfo.ApgarScore = textBoxApgarScore.Text;
            _obstetricHistoryInfo.ChildbirthWeeks = textBoxChildbirthWeeks.Text;
            _obstetricHistoryInfo.ComplicationsInPregnancy = textBoxComplicationsInPregnancy.Text;
            _obstetricHistoryInfo.DrugsInPregnancy = textBoxDrugsInPregnancy.Text;
            _obstetricHistoryInfo.DurationOfLabor = textBoxDurationOfLabor.Text;
            _obstetricHistoryInfo.HeightAtBirth = textBoxHeightAtBirth.Text;
            _obstetricHistoryInfo.HospitalTreatment = textBoxHospitalTreatment.Text;
            _obstetricHistoryInfo.ObstetricParalysis = textBoxObstetricParalysis.Text;
            _obstetricHistoryInfo.OutpatientCare = textBoxOutpatientCare.Text;
            _obstetricHistoryInfo.WeightAtBirth = textBoxWeightAtBirth.Text;

            _obstetricHistoryInfo.BirthInjury = comboBoxBirthInjury.Text;
            _obstetricHistoryInfo.ComplicationsDuringChildbirth = comboBoxComplicationsDuringChildbirth.Text;
            _obstetricHistoryInfo.Fetal = comboBoxFetal.Text;
            _obstetricHistoryInfo.IsTongsUsing = comboBoxIsTongsUsing.Text == "да"
                ? true
                : false;
            _obstetricHistoryInfo.IsVacuumUsing = comboBoxIsVacuumUsing.Text == "да"
                ? true
                : false;
        }


        /// <summary>
        /// Сохранить информацию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToObstetricHistory();

                _obstetricHistoryWorker.Update(_obstetricHistoryInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_obstetricHistoryInfo.NotInDatabase)
                {
                    _obstetricHistoryWorker.Remove(_obstetricHistoryInfo.PatientId);
                }

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Предотвращение закрытия формы по нажатию на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ObstetricHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                DialogResult dialogResult = MessageBox.ShowDialog("Вы хотите сохранить изменения?", "Закрытие окна", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    buttonOk_Click(sender, e);
                }
                else if (dialogResult == DialogResult.No)
                {
                    buttonClose_Click(sender, e);
                }
                else
                {
                    e.Cancel = true;
                }
            }

            _configurationEngine.ObstetricHistoryFormLocation = Location;
        }


        #region Подсказки
        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сохранить изменения", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть форму без сохранения изменений", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDocuments_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сгенерировать отчёт в Word", buttonDocuments);
            buttonDocuments.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDocuments_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDocuments.FlatStyle = FlatStyle.Flat;
        }
        #endregion


        /// <summary>
        /// Перевести координаты ячейки в координаты массива с данными о галочках
        /// </summary>
        /// <param name="rowIndex">Номер строки</param>
        /// <param name="columnIndex">Номер столбца</param>
        /// <returns></returns>
        private static int GetIndexForCell(int rowIndex, int columnIndex)
        {
            return (rowIndex * 6) + columnIndex - 1;
        }


        /// <summary>
        /// Обработка клика мыши по ячейкам таблицы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void dataGridChronology_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1)
            {
                return;
            }

            int index = GetIndexForCell(e.RowIndex, e.ColumnIndex);
            dataGridChronology.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _obstetricHistoryInfo.Chronology[index]
                ? new Bitmap(1, 1)
                : Properties.Resources.mark;

            _obstetricHistoryInfo.Chronology[index] = !_obstetricHistoryInfo.Chronology[index];

            dataGridChronology.CurrentCell = dataGridChronology.Rows[0].Cells[0];
            dataGridChronology.Refresh();
        }


        /// <summary>
        /// Создание документа в ворде
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToObstetricHistory();

                CWordExportHelper.ExportObstetricHistory(_obstetricHistoryInfo);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
