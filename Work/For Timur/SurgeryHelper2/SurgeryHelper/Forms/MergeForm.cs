using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.MyControls;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class MergeForm : Form
    {
        private readonly CWorkersKeeper _ownWorkersKeeper;
        private readonly CWorkersKeeper _foreignWorkersKeeper;
        private readonly CConfigurationEngine _configurationEngine;

        private CDatabasesMerger _mergeDatabases;

        private bool _stopSaveParameters;
        private bool _stopSynchronizing;

        public MergeForm(CWorkersKeeper ownWorkersKeeper, CWorkersKeeper foreignWorkersKeeper)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _ownWorkersKeeper = ownWorkersKeeper;
            _foreignWorkersKeeper = foreignWorkersKeeper;
            _configurationEngine = ownWorkersKeeper.ConfigurationEngine;
        }

        /// <summary>
        /// Восстановление позиции формы и её размера
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void MergeForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.MergeFormLocation.X >= 0 &&
                _configurationEngine.MergeFormLocation.Y >= 0)
            {
                Location = _configurationEngine.MergeFormLocation;
            }

            Size = _configurationEngine.MergeFormSize;
            checkBoxShowPrivateFolderDiffs.Checked = _configurationEngine.MergeFormCheckBoxShowPrivateFolderDiffs;
            checkBoxCopyPrivateFolderData.Checked = _configurationEngine.MergeFormCheckBoxCopyPrivateFolderData;

            labelMove.Top = Convert.ToInt32(_configurationEngine.MergeFormLabelMoveTop);
            _stopSaveParameters = false;
        }


        /// <summary>
        /// Сравнение данных в двух базах
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void MergeForm_Shown(object sender, EventArgs e)
        {
            try
            {
                buttonOk.Enabled = buttonRefresh.Enabled = checkBoxShowPrivateFolderDiffs.Enabled =
                checkBoxCopyPrivateFolderData.Enabled = false;

                _mergeDatabases = new CDatabasesMerger(_ownWorkersKeeper, _foreignWorkersKeeper, this);

                ShowMergeInfo();

                buttonOk.Enabled = buttonRefresh.Enabled = checkBoxShowPrivateFolderDiffs.Enabled =
                checkBoxCopyPrivateFolderData.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Отобразить списков пациентов, готовых к экспорту/импорту, и не готовых к оному
        /// </summary>
        private void ShowMergeInfo()
        {
            _stopSynchronizing = true;
            OwnMergeList.Items.Clear();
            foreach (CMergeInfo mergeInfo in _mergeDatabases.OwnMergeList)
            {
                OwnMergeList.Items.Add(mergeInfo.Difference);
            }

            buttonCopySelectedToForeign.Enabled = buttonCopyAllNewToForeign.Enabled = _mergeDatabases.OwnMergeList.Length != 0;
            
            ForeignMergeList.Items.Clear();
            foreach (CMergeInfo mergeInfo in _mergeDatabases.ForeignMergeList)
            {
                ForeignMergeList.Items.Add(mergeInfo.Difference);
            }

            if (ForeignMergeList.Items.Count > 0)
            {
                ForeignMergeList.Focus();
                ForeignMergeList.SelectedIndex = 0;
            }

            if (OwnMergeList.Items.Count > 0)
            {
                OwnMergeList.Focus();
                OwnMergeList.SelectedIndex = 0;
            }

            buttonCopySelectedToOwn.Enabled = buttonCopyAllNewToOwn.Enabled = _mergeDatabases.ForeignMergeList.Length != 0;

            BothMergeList.Rows.Clear();
            foreach (CMergeInfo mergeInfo in _mergeDatabases.BothMergeList)
            {
                if (IsDiffsForPrivateFolderOnly(mergeInfo))
                {
                    continue;
                }

                var param = new object[]
                {                    
                    mergeInfo.FIO,
                    mergeInfo.Nosology,
                    mergeInfo.Difference
                };

                BothMergeList.Rows.Add(param);
            }

            buttonInfo.Enabled = BothMergeList.Rows.Count != 0;

            if (buttonCopySelectedToOwn.Enabled || buttonCopySelectedToForeign.Enabled)
            {
                labelInfo.Text = "Будьте внимательны! Нажатие на стрелочки сразу изменяет данные в базах.";
            }
            else
            {
                labelInfo.Text = "Сравнение пациентов завершено, отличий не найдено.";
            }

            _stopSynchronizing = false;
        }


        /// <summary>
        /// Определяет, содержат ли отличия для пациента что-нибудь, кроме отличий в личной папке
        /// </summary>
        /// <param name="mergeInfo"></param>
        /// <returns></returns>
        private bool IsDiffsForPrivateFolderOnly(CMergeInfo mergeInfo)
        {
            if (checkBoxShowPrivateFolderDiffs.Checked)
            {
                return false;
            }

            string[] diffs = mergeInfo.Difference.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string diff in diffs)
            {
                if (!diff.ToLower().Contains("содержимое личной папки"))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Добавить выделенные записи из нашей базы во внешнюю базу
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopySelectedToForeign_Click(object sender, EventArgs e)
        {
            ButtonCopyPressed(false, OwnMergeList);
        }


        /// <summary>
        /// Добавить выделенные записи из внешней базы в нашу
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopySelectedToOwn_Click(object sender, EventArgs e)
        {
            ButtonCopyPressed(true, ForeignMergeList);
        }


        /// <summary>
        /// Добавить все новые объекты из нашей базы во внешнюю базу
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopyAllNewToForeign_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.ShowDialog("Вы уверены, что хотие скопировать ВСЕ новые объекты из нашей базы во внешнюю?\r\nВНИМАНИЕ: выделенность строк не учитывается", "Копирование данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            ButtonCopyAllNewPressed(false, OwnMergeList);
        }


        /// <summary>
        /// Добавить все новые объекты из внешней базы в нашу
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopyAllNewToOwn_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.ShowDialog("Вы уверены, что хотие скопировать ВСЕ новые объекты из внешней базы в нашу?\r\nВНИМАНИЕ: выделенность строк не учитывается", "Копирование данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            ButtonCopyAllNewPressed(true, ForeignMergeList);
        }


        /// <summary>
        /// Вызов копирования всех новых объектов и удаления скопированных записей из списка
        /// </summary>
        /// <param name="copyToOwn">true - если копируем в нашу базу, false - в противном случае</param>
        /// <param name="fromMultiRowList">Список, откуда копируем пациетов</param>
        private void ButtonCopyAllNewPressed(bool copyToOwn, MultiRowListBox fromMultiRowList)
        {
            CMergeInfo[] ownMergeInfoList = _mergeDatabases.OwnMergeList;
            CMergeInfo[] foreignMergeInfoList = _mergeDatabases.ForeignMergeList;
            var rowIndexForDelete = new List<int>();

            try
            {
                for (int rowIndex = 0; rowIndex < fromMultiRowList.Items.Count; rowIndex++)
                {
                    CMergeInfo ownMergeInfo = ownMergeInfoList[rowIndex];
                    CMergeInfo foreignMergeInfo = foreignMergeInfoList[rowIndex];
                    // Запускаем копирование данных только если в той базе, откуда копируем - объект есть,
                    // а в той базе, куда копируем - объекта нет
                    if ((copyToOwn && ownMergeInfo.TypeOfObject != ObjectType.Empty) ||
                       (!copyToOwn && foreignMergeInfo.TypeOfObject != ObjectType.Empty))
                    {
                        continue;
                    }

                    ObjectType typeOfObject = ownMergeInfo.TypeOfObject == ObjectType.Empty ? foreignMergeInfo.TypeOfObject : ownMergeInfo.TypeOfObject;
                    switch (typeOfObject)
                    {
                        case ObjectType.Patient:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddPatientToOwnDatabase(foreignMergeInfo.IdForeignPatient, checkBoxCopyPrivateFolderData.Checked);
                            }
                            else
                            {
                                _mergeDatabases.AddPatientToForeignDatabase(ownMergeInfo.IdOwnPatient, checkBoxCopyPrivateFolderData.Checked);
                            }
                            break;
                        case ObjectType.Hospitalization:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddHospitalizationToOwnDatabase(
                                    foreignMergeInfo.IdForeignPatient,
                                    foreignMergeInfo.IdOwnPatient,
                                    foreignMergeInfo.IdForeignHospitalization);
                            }
                            else
                            {
                                _mergeDatabases.AddHospitalizationToForeignDatabase(
                                    ownMergeInfo.IdOwnPatient,
                                    ownMergeInfo.IdForeignPatient,
                                    ownMergeInfo.IdOwnHospitalization);
                            }
                            break;
                        case ObjectType.Visit:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddVisitToOwnDatabase(
                                    foreignMergeInfo.IdForeignPatient,
                                    foreignMergeInfo.IdOwnPatient,
                                    foreignMergeInfo.IdForeignVisit);
                            }
                            else
                            {
                                _mergeDatabases.AddVisitToForeignDatabase(
                                    ownMergeInfo.IdOwnPatient,
                                    ownMergeInfo.IdForeignPatient,
                                    ownMergeInfo.IdOwnVisit);
                            }
                            break;
                        case ObjectType.Anamnese:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddAnamneseToOwnDatabase(
                                    foreignMergeInfo.IdForeignPatient,
                                    foreignMergeInfo.IdOwnPatient);
                            }
                            else
                            {
                                _mergeDatabases.AddAnamneseToForeignDatabase(
                                    ownMergeInfo.IdOwnPatient,
                                    ownMergeInfo.IdForeignPatient);
                            }
                            break;
                        case ObjectType.ObstetricHistory:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddObstetricHistoryToOwnDatabase(
                                    foreignMergeInfo.IdForeignPatient,
                                    foreignMergeInfo.IdOwnPatient);
                            }
                            else
                            {
                                _mergeDatabases.AddObstetricHistoryToForeignDatabase(
                                    ownMergeInfo.IdOwnPatient,
                                    ownMergeInfo.IdForeignPatient);
                            }
                            break;
                        case ObjectType.DischargeEpicrisis:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddDischargeEpicrisisToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization);
                            }
                            else
                            {
                                _mergeDatabases.AddDischargeEpicrisisToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization);
                            }
                            break;
                        case ObjectType.LineOfCommunicationEpicrisis:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddLineOfCommunicationEpicrisisToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization);
                            }
                            else
                            {
                                _mergeDatabases.AddLineOfCommunicationEpicrisisToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization);
                            }
                            break;
                        case ObjectType.MedicalInspection:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddMedicalInspectionToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization);
                            }
                            else
                            {
                                _mergeDatabases.AddMedicalInspectionToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization);
                            }
                            break;
                        case ObjectType.TransferableEpicrisis:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddTransferableEpicrisisToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization);
                            }
                            else
                            {
                                _mergeDatabases.AddTransferableEpicrisisToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization);
                            }
                            break;
                        case ObjectType.Operation:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddOperationAndProtocolToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization,
                                    foreignMergeInfo.IdOperation);
                            }
                            else
                            {
                                _mergeDatabases.AddOperationAndProtocolToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization,
                                    ownMergeInfo.IdOperation);
                            }
                            break;
                        case ObjectType.BrachialPlexusCard:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddBrachialPlexusCardToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization,
                                    foreignMergeInfo.IdForeignVisit,
                                    foreignMergeInfo.IdOwnVisit);
                            }
                            else
                            {
                                _mergeDatabases.AddBrachialPlexusCardToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization,
                                    ownMergeInfo.IdOwnVisit,
                                    ownMergeInfo.IdForeignVisit);
                            }
                            break;
                        case ObjectType.ObstetricParalysisCard:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddObstetricParalysisCardToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization,
                                    foreignMergeInfo.IdForeignVisit,
                                    foreignMergeInfo.IdOwnVisit);
                            }
                            else
                            {
                                _mergeDatabases.AddObstetricParalysisCardToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization,
                                    ownMergeInfo.IdOwnVisit,
                                    ownMergeInfo.IdForeignVisit);
                            }
                            break;
                        case ObjectType.RangeOfMotionCard:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddRangeOfMotionCardToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization,
                                    foreignMergeInfo.IdForeignVisit,
                                    foreignMergeInfo.IdOwnVisit);
                            }
                            else
                            {
                                _mergeDatabases.AddRangeOfMotionCardToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization,
                                    ownMergeInfo.IdOwnVisit,
                                    ownMergeInfo.IdForeignVisit);
                            }
                            break;
                        case ObjectType.LeftRightCard:
                            if (copyToOwn)
                            {
                                _mergeDatabases.AddLeftRightCardToOwnDatabase(
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdOwnHospitalization,
                                    foreignMergeInfo.IdForeignVisit,
                                    foreignMergeInfo.IdOwnVisit,
                                    foreignMergeInfo.TypeOfCard);
                            }
                            else
                            {
                                _mergeDatabases.AddLeftRightCardToForeignDatabase(
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdForeignHospitalization,
                                    ownMergeInfo.IdOwnVisit,
                                    ownMergeInfo.IdForeignVisit,
                                    ownMergeInfo.TypeOfCard);
                            }
                            break;
                    }

                    if ((OwnMergeList.Items[rowIndex].ToString() != string.Empty && ForeignMergeList.Items[rowIndex].ToString() != string.Empty) ||
                        (OwnMergeList.Items[rowIndex].ToString() != string.Empty && !copyToOwn) ||
                        (ForeignMergeList.Items[rowIndex].ToString() != string.Empty && copyToOwn))
                    {
                        rowIndexForDelete.Add(rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RemoveItemsFromLists(rowIndexForDelete);
        }


        /// <summary>
        /// Вызов копирования записей и удаление скопированных записей из списка
        /// </summary>
        /// <param name="copyToOwn">true - если копируем в нашу базу, false - в противном случае</param>
        /// <param name="fromMultiRowList">Список, откуда копируем пациетов</param>
        private void ButtonCopyPressed(bool copyToOwn, MultiRowListBox fromMultiRowList)
        {
            CMergeInfo[] ownMergeInfoList = _mergeDatabases.OwnMergeList;
            CMergeInfo[] foreignMergeInfoList = _mergeDatabases.ForeignMergeList;
            var rowIndexForDelete = new List<int>();

            try
            {
                foreach (int rowIndex in fromMultiRowList.SelectedIndices)
                {
                    CMergeInfo ownMergeInfo = ownMergeInfoList[rowIndex];
                    CMergeInfo foreignMergeInfo = foreignMergeInfoList[rowIndex];
                    ObjectType typeOfObject = ownMergeInfo.TypeOfObject == ObjectType.Empty ? foreignMergeInfo.TypeOfObject : ownMergeInfo.TypeOfObject;
                    switch (typeOfObject)
                    {
                        case ObjectType.Patient:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignPatient == -1)
                                {
                                    _mergeDatabases.RemovePatientFromOwnDatabase(ownMergeInfo.IdOwnPatient);
                                }
                                else
                                {
                                    _mergeDatabases.AddPatientToOwnDatabase(foreignMergeInfo.IdForeignPatient, checkBoxCopyPrivateFolderData.Checked);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnPatient == -1)
                                {
                                    _mergeDatabases.RemovePatientFromForeignDatabase(foreignMergeInfo.IdForeignPatient);
                                }
                                else
                                {
                                    _mergeDatabases.AddPatientToForeignDatabase(ownMergeInfo.IdOwnPatient, checkBoxCopyPrivateFolderData.Checked);
                                }
                            }
                            break;
                        case ObjectType.PatientBirthday:
                        case ObjectType.PatientBuildingNumber:
                        case ObjectType.PatientCityName:
                        case ObjectType.PatientEMail:
                        case ObjectType.PatientFlatNumber:
                        case ObjectType.PatientHomeNumber:
                        case ObjectType.PatientInsuranceName:
                        case ObjectType.PatientInsuranceNumber:
                        case ObjectType.PatientInsuranceSeries:
                        case ObjectType.PatientInsuranceType:
                        case ObjectType.PatientPassInformationDeliveryDate:
                        case ObjectType.PatientPassInformationNumber:
                        case ObjectType.PatientPassInformationOrganization:
                        case ObjectType.PatientPassInformationSeries:
                        case ObjectType.PatientPassInformationSubdivisionCode:
                        case ObjectType.PatientPhone:
                        case ObjectType.PatientRelatives:
                        case ObjectType.PatientIsSpecifyLegalRepresent:
                        case ObjectType.PatientLegalRepresent:
                        case ObjectType.PatientWorkPlace:
                        case ObjectType.PatientStreetName:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangePatientParameterInOwnDatabase(typeOfObject, ownMergeInfo.IdOwnPatient, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangePatientParameterInForeignDatabase(typeOfObject, foreignMergeInfo.IdForeignPatient, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.Hospitalization:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveHospitalizationFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddHospitalizationToOwnDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdOwnPatient,
                                        foreignMergeInfo.IdForeignHospitalization);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveHospitalizationFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddHospitalizationToForeignDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdForeignPatient,
                                        ownMergeInfo.IdOwnHospitalization);
                                }
                            }
                            break;
                        case ObjectType.HospitalizationDiagnose:
                        case ObjectType.HospitalizationDoctorInChargeOfTheCase:
                        case ObjectType.HospitalizationFotoFolderName:
                        case ObjectType.HospitalizationNumberOfCaseHistory:
                        case ObjectType.HospitalizationReleaseDate:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeHospitalizationParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnPatient, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeHospitalizationParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignPatient, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.Visit:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignVisit == -1)
                                {
                                    _mergeDatabases.RemoveVisitFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddVisitToOwnDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdOwnPatient,
                                        foreignMergeInfo.IdForeignVisit);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnVisit == -1)
                                {
                                    _mergeDatabases.RemoveVisitFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddVisitToForeignDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdForeignPatient,
                                        ownMergeInfo.IdOwnVisit);
                                }
                            }
                            break;
                        case ObjectType.VisitComments:
                        case ObjectType.VisitDiagnose:
                        case ObjectType.VisitDoctor:
                        case ObjectType.VisitEvenly:
                        case ObjectType.VisitRecommendation:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeVisitParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnPatient, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeVisitParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignPatient, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.Anamnese:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignPatient == -1)
                                {
                                    _mergeDatabases.RemoveObstetricHistoryFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient);
                                }
                                else
                                {
                                    _mergeDatabases.AddAnamneseToOwnDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdOwnPatient);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnPatient == -1)
                                {
                                    _mergeDatabases.RemoveObstetricHistoryFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient);
                                }
                                else
                                {
                                    _mergeDatabases.AddAnamneseToForeignDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdForeignPatient);
                                }
                            }

                            break;
                        case ObjectType.AnamneseAnMorbi:
                        case ObjectType.AnamneseTraumaDate:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeAnamneseParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnPatient, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeAnamneseParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignPatient, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.ObstetricHistory:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignPatient == -1)
                                {
                                    _mergeDatabases.RemoveObstetricHistoryFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient);
                                }
                                else
                                {
                                    _mergeDatabases.AddObstetricHistoryToOwnDatabase(
                                       foreignMergeInfo.IdForeignPatient,
                                       foreignMergeInfo.IdOwnPatient);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnPatient == -1)
                                {
                                    _mergeDatabases.RemoveObstetricHistoryFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient);
                                }
                                else
                                {
                                    _mergeDatabases.AddObstetricHistoryToForeignDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdForeignPatient);
                                }
                            }
                            break;
                        case ObjectType.ObstetricHistoryApgarScore:
                        case ObjectType.ObstetricHistoryBirthInjury:
                        case ObjectType.ObstetricHistoryChildbirthWeeks:
                        case ObjectType.ObstetricHistoryComplicationsDuringChildbirth:
                        case ObjectType.ObstetricHistoryComplicationsInPregnancy:
                        case ObjectType.ObstetricHistoryDrugsInPregnancy:
                        case ObjectType.ObstetricHistoryDurationOfLabor:
                        case ObjectType.ObstetricHistoryFetal:
                        case ObjectType.ObstetricHistoryHeightAtBirth:
                        case ObjectType.ObstetricHistoryHospitalTreatment:
                        case ObjectType.ObstetricHistoryIsTongsUsing:
                        case ObjectType.ObstetricHistoryIsVacuumUsing:
                        case ObjectType.ObstetricHistoryObstetricParalysis:
                        case ObjectType.ObstetricHistoryOutpatientCare:
                        case ObjectType.ObstetricHistoryWeightAtBirth:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeObstetricHistoryParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnPatient, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeObstetricHistoryParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignPatient, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.ObstetricHistoryChronology:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeObstetricHistoryParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnPatient, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeObstetricHistoryParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignPatient, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.DischargeEpicrisis:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveDischargeEpicrisisFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddDischargeEpicrisisToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveDischargeEpicrisisFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddDischargeEpicrisisToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization);
                                }
                            }
                            break;
                        case ObjectType.DischargeEpicrisisAdditionalAnalises:
                        case ObjectType.DischargeEpicrisisAfterOperation:
                        case ObjectType.DischargeEpicrisisConservativeTherapy:
                        case ObjectType.DischargeEpicrisisEkg:
                        case ObjectType.DischargeEpicrisisOakEritrocits:
                        case ObjectType.DischargeEpicrisisOakHb:
                        case ObjectType.DischargeEpicrisisOakLekocits:
                        case ObjectType.DischargeEpicrisisOakSoe:
                        case ObjectType.DischargeEpicrisisOamColor:
                        case ObjectType.DischargeEpicrisisOamDensity:
                        case ObjectType.DischargeEpicrisisOamEritrocits:
                        case ObjectType.DischargeEpicrisisOamLekocits:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeDischargeEpicrisisParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeDischargeEpicrisisParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.DischargeEpicrisisAdditionalRecomendations:
                        case ObjectType.DischargeEpicrisisRecomendations:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeDischargeEpicrisisParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeDischargeEpicrisisParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.LineOfCommunicationEpicrisis:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveLineOfCommunicationEpicrisisFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddLineOfCommunicationEpicrisisToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveLineOfCommunicationEpicrisisFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddLineOfCommunicationEpicrisisToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization);
                                }
                            }
                            break;
                        case ObjectType.LineOfCommunicationEpicrisisAdditionalInfo:
                        case ObjectType.LineOfCommunicationEpicrisisPlan:
                        case ObjectType.LineOfCommunicationEpicrisisWritingDate:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeLineOfCommunicationEpicrisisParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeLineOfCommunicationEpicrisisParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.MedicalInspection:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveMedicalInspectionFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddMedicalInspectionToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveMedicalInspectionFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddMedicalInspectionToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization);
                                }
                            }
                            break;
                        case ObjectType.MedicalInspectionAnamneseAnMorbi:
                        case ObjectType.MedicalInspectionComplaints:
                        case ObjectType.MedicalInspectionExpertAnamnese:
                        case ObjectType.MedicalInspectionInspectionPlan:
                        case ObjectType.MedicalInspectionIsAnamneseActive:
                        case ObjectType.MedicalInspectionIsPlanEnabled:
                        case ObjectType.MedicalInspectionIsStLocalisPart1Enabled:
                        case ObjectType.MedicalInspectionIsStLocalisPart2Enabled:
                        case ObjectType.MedicalInspectionLnFirstDateStart:
                        case ObjectType.MedicalInspectionLnWithNumberDateEnd:
                        case ObjectType.MedicalInspectionLnWithNumberDateStart:
                        case ObjectType.MedicalInspectionStLocalisDescription:
                        case ObjectType.MedicalInspectionStLocalisPart1OppositionFinger:
                        case ObjectType.MedicalInspectionStLocalisPart2NumericUpDown:
                        case ObjectType.MedicalInspectionStLocalisPart2WhichHand:
                        case ObjectType.MedicalInspectionStLocalisRentgen:
                        case ObjectType.MedicalInspectionTeoRisk:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeMedicalInspectionParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeMedicalInspectionParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.MedicalInspectionAnamneseAnVitae:
                        case ObjectType.MedicalInspectionAnamneseCheckboxes:
                        case ObjectType.MedicalInspectionAnamneseTextBoxes:
                        case ObjectType.MedicalInspectionStLocalisPart1Fields:
                        case ObjectType.MedicalInspectionStLocalisPart2ComboBoxes:
                        case ObjectType.MedicalInspectionStLocalisPart2LeftHand:
                        case ObjectType.MedicalInspectionStLocalisPart2RightHand:
                        case ObjectType.MedicalInspectionStLocalisPart2TextBoxes:
                        case ObjectType.MedicalInspectionStPraesensComboBoxes:
                        case ObjectType.MedicalInspectionStPraesensNumericUpDowns:
                        case ObjectType.MedicalInspectionStPraesensTextBoxes:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeMedicalInspectionParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeMedicalInspectionParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.TransferableEpicrisis:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveTransferableEpicrisisFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddTransferableEpicrisisToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveTransferableEpicrisisFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization);
                                }
                                else
                                {
                                    _mergeDatabases.AddTransferableEpicrisisToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization);
                                }
                            }
                            break;
                        case ObjectType.TransferableEpicrisisAdditionalInfo:
                        case ObjectType.TransferableEpicrisisAfterOperationPeriod:
                        case ObjectType.TransferableEpicrisisDisabilityList:
                        case ObjectType.TransferableEpicrisisIsIncludeDisabilityList:
                        case ObjectType.TransferableEpicrisisPlan:
                        case ObjectType.TransferableEpicrisisWritingDate:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeTransferableEpicrisisParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeTransferableEpicrisisParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.Operation:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveOperationAndProtocolFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdOperation);
                                }
                                else
                                {
                                    _mergeDatabases.AddOperationAndProtocolToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization,
                                        foreignMergeInfo.IdOperation);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1)
                                {
                                    _mergeDatabases.RemoveOperationAndProtocolFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOperation);
                                }
                                else
                                {
                                    _mergeDatabases.AddOperationAndProtocolToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization,
                                        ownMergeInfo.IdOperation);
                                }
                            }
                            break;
                        case ObjectType.OperationDateOfOperation:
                        case ObjectType.OperationEndTimeOfOperation:
                        case ObjectType.OperationHeAnaesthetist:
                        case ObjectType.OperationName:
                        case ObjectType.OperationOrderly:
                        case ObjectType.OperationScrubNurse:
                        case ObjectType.OperationSheAnaesthetist:
                        case ObjectType.OperationStartTimeOfOperation:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeOperationParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOperation, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeOperationParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdOperation, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.OperationAssistents:
                        case ObjectType.OperationSurgeons:
                        case ObjectType.OperationTypes:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeOperationParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOperation, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeOperationParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdOperation, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.OperationProtocolADFirst:
                        case ObjectType.OperationProtocolADSecond:
                        case ObjectType.OperationProtocolBreath:
                        case ObjectType.OperationProtocolChDD:
                        case ObjectType.OperationProtocolComplaints:
                        case ObjectType.OperationProtocolHeartRhythm:
                        case ObjectType.OperationProtocolHeartSounds:
                        case ObjectType.OperationProtocolIsDairyEnabled:
                        case ObjectType.OperationProtocolIsTreatmentPlanActiveInOperationProtocol:
                        case ObjectType.OperationProtocolOperationCourse:
                        case ObjectType.OperationProtocolPulse:
                        case ObjectType.OperationProtocolState:
                        case ObjectType.OperationProtocolStLocalis:
                        case ObjectType.OperationProtocolStomach:
                        case ObjectType.OperationProtocolStool:
                        case ObjectType.OperationProtocolTemperature:
                        case ObjectType.OperationProtocolTimeWriting:
                        case ObjectType.OperationProtocolTreatmentPlanDate:
                        case ObjectType.OperationProtocolTreatmentPlanInspection:
                        case ObjectType.OperationProtocolUrination:
                        case ObjectType.OperationProtocolWheeze:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeOperationProtocolParameterInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOperation, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeOperationProtocolParameterInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdOperation, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.BrachialPlexusCard:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1 && foreignMergeInfo.IdForeignVisit == -1)
                                {
                                    _mergeDatabases.RemoveBrachialPlexusCardFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdOwnVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddBrachialPlexusCardToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization,
                                        foreignMergeInfo.IdForeignVisit,
                                        foreignMergeInfo.IdOwnVisit);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1 && ownMergeInfo.IdOwnVisit == -1)
                                {
                                    _mergeDatabases.RemoveBrachialPlexusCardFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdForeignVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddBrachialPlexusCardToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization,
                                        ownMergeInfo.IdOwnVisit,
                                        ownMergeInfo.IdForeignVisit);
                                }
                            }
                            break;
                        case ObjectType.BrachialPlexusCardSideOfCard:
                        case ObjectType.BrachialPlexusCardVascularStatus:
                        case ObjectType.BrachialPlexusCardDiaphragm:
                        case ObjectType.BrachialPlexusCardHornersSyndrome:
                        case ObjectType.BrachialPlexusCardTinelsSymptom:
                        case ObjectType.BrachialPlexusCardIsMyelographyEnabled:
                        case ObjectType.BrachialPlexusCardMyelographyType:
                        case ObjectType.BrachialPlexusCardMyelographyDate:
                        case ObjectType.BrachialPlexusCardMyelography:
                        case ObjectType.BrachialPlexusCardIsEMNGEnabled:
                        case ObjectType.BrachialPlexusCardEMNGDate:
                        case ObjectType.BrachialPlexusCardEMNG:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeBrachialPlexusCardInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeBrachialPlexusCardInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.BrachialPlexusCardPicture:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeBrachialPlexusCardInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeBrachialPlexusCardInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.ObstetricParalysisCard:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1 && foreignMergeInfo.IdForeignVisit == -1)
                                {
                                    _mergeDatabases.RemoveObstetricParalysisCardFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdOwnVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddObstetricParalysisCardToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization,
                                        foreignMergeInfo.IdForeignVisit,
                                        foreignMergeInfo.IdOwnVisit);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1 && ownMergeInfo.IdOwnVisit == -1)
                                {
                                    _mergeDatabases.RemoveObstetricParalysisCardFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdForeignVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddObstetricParalysisCardToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization,
                                        ownMergeInfo.IdOwnVisit,
                                        ownMergeInfo.IdForeignVisit);
                                }
                            }
                            break;
                        case ObjectType.ObstetricParalysisCardSideOfCard:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeObstetricParalysisCardInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeObstetricParalysisCardInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.ObstetricParalysisCardComboBoxes:
                        case ObjectType.ObstetricParalysisCardGlobalAbduction:
                        case ObjectType.ObstetricParalysisCardGlobalExternalRotation:
                        case ObjectType.ObstetricParalysisCardHandToMouth:
                        case ObjectType.ObstetricParalysisCardHandToNeck:
                        case ObjectType.ObstetricParalysisCardHandToSpine:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeObstetricParalysisCardInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeObstetricParalysisCardInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.RangeOfMotionCard:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1 && foreignMergeInfo.IdForeignVisit == -1)
                                {
                                    _mergeDatabases.RemoveRangeOfMotionCardFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdOwnVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddRangeOfMotionCardToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization,
                                        foreignMergeInfo.IdForeignVisit,
                                        foreignMergeInfo.IdOwnVisit);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1 && ownMergeInfo.IdOwnVisit == -1)
                                {
                                    _mergeDatabases.RemoveRangeOfMotionCardFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdForeignVisit);
                                }
                                else
                                {
                                    _mergeDatabases.AddRangeOfMotionCardToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization,
                                        ownMergeInfo.IdOwnVisit,
                                        ownMergeInfo.IdForeignVisit);
                                }
                            }
                            break;
                        case ObjectType.RangeOfMotionCardOppositionFinger:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeRangeOfMotionCardInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Value);
                            }
                            else
                            {
                                _mergeDatabases.ChangeRangeOfMotionCardInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Value);
                            }
                            break;
                        case ObjectType.RangeOfMotionCardFields:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeRangeOfMotionCardInOwnDatabase(
                                    typeOfObject, ownMergeInfo.IdOwnHospitalization, ownMergeInfo.IdOwnVisit, foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeRangeOfMotionCardInForeignDatabase(
                                    typeOfObject, foreignMergeInfo.IdForeignHospitalization, foreignMergeInfo.IdForeignVisit, ownMergeInfo.Object);
                            }
                            break;
                        case ObjectType.LeftRightCard:
                            if (copyToOwn)
                            {
                                if (foreignMergeInfo.IdForeignHospitalization == -1 && foreignMergeInfo.IdForeignVisit == -1)
                                {
                                    _mergeDatabases.RemoveLeftRightCardFromOwnDatabase(
                                        ownMergeInfo.IdOwnPatient,
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdOwnVisit,
                                        ownMergeInfo.TypeOfCard);
                                }
                                else
                                {
                                    _mergeDatabases.AddLeftRightCardToOwnDatabase(
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdOwnHospitalization,
                                        foreignMergeInfo.IdForeignVisit,
                                        foreignMergeInfo.IdOwnVisit,
                                        foreignMergeInfo.TypeOfCard);
                                }
                            }
                            else
                            {
                                if (ownMergeInfo.IdOwnHospitalization == -1 && ownMergeInfo.IdOwnVisit == -1)
                                {
                                    _mergeDatabases.RemoveLeftRightCardFromForeignDatabase(
                                        foreignMergeInfo.IdForeignPatient,
                                        foreignMergeInfo.IdForeignHospitalization,
                                        foreignMergeInfo.IdForeignVisit,
                                        foreignMergeInfo.TypeOfCard);
                                }
                                else
                                {
                                    _mergeDatabases.AddLeftRightCardToForeignDatabase(
                                        ownMergeInfo.IdOwnHospitalization,
                                        ownMergeInfo.IdForeignHospitalization,
                                        ownMergeInfo.IdOwnVisit,
                                        ownMergeInfo.IdForeignVisit,
                                        ownMergeInfo.TypeOfCard);
                                }
                            }
                            break;
                        case ObjectType.LeftRightCardPicture:
                            if (copyToOwn)
                            {
                                _mergeDatabases.ChangeLeftRightCardInOwnDatabase(
                                    typeOfObject,
                                    ownMergeInfo.IdOwnHospitalization,
                                    ownMergeInfo.IdOwnVisit,
                                    ownMergeInfo.SideOfCard,
                                    ownMergeInfo.TypeOfCard,
                                    foreignMergeInfo.Object);
                            }
                            else
                            {
                                _mergeDatabases.ChangeLeftRightCardInForeignDatabase(
                                    typeOfObject,
                                    foreignMergeInfo.IdForeignHospitalization,
                                    foreignMergeInfo.IdForeignVisit,
                                    foreignMergeInfo.SideOfCard,
                                    foreignMergeInfo.TypeOfCard,
                                    ownMergeInfo.Object);
                            }
                            break;
                    }

                    rowIndexForDelete.Add(rowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RemoveItemsFromLists(rowIndexForDelete);
        }

        private void RemoveItemsFromLists(List<int> rowIndexForDelete)
        {
            // Сделан отдельный try/catch чтобы убрать те выделенные объекты, которые уже были
            // смержены до ошибки (либо все объекты, если ошибки не было)
            try
            {
                int saveSelectedIndex = OwnMergeList.SelectedIndex;
                rowIndexForDelete.Sort();
                _mergeDatabases.RemoveIndexesFromForeignList(rowIndexForDelete);
                _mergeDatabases.RemoveIndexesFromOwnList(rowIndexForDelete);

                for (int i = rowIndexForDelete.Count - 1; i >= 0; i--)
                {
                    OwnMergeList.Items.RemoveAt(rowIndexForDelete[i]);
                    ForeignMergeList.Items.RemoveAt(rowIndexForDelete[i]);
                }

                if (OwnMergeList.Items.Count > 0)
                {
                    if (saveSelectedIndex >= OwnMergeList.Items.Count)
                    {
                        OwnMergeList.SelectedIndex = ForeignMergeList.SelectedIndex = OwnMergeList.Items.Count - 1;
                    }
                    else
                    {
                        OwnMergeList.SelectedIndex = ForeignMergeList.SelectedIndex = saveSelectedIndex;
                    }
                }
                else
                {
                    buttonCopySelectedToForeign.Enabled = buttonCopySelectedToOwn.Enabled = buttonCopyAllNewToForeign.Enabled = buttonCopyAllNewToOwn.Enabled = false;
                    labelInfo.Text = "Сравнение пациентов завершено, отличий не найдено.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отобразить различия в разных базах для выделенного пациента
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonInfo_Click(object sender, EventArgs e)
        {
            int currentNumber = BothMergeList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new MergePatientDifferenceForm(
                BothMergeList.Rows[currentNumber].Cells[0].Value.ToString(),
                BothMergeList.Rows[currentNumber].Cells[1].Value.ToString(),
                BothMergeList.Rows[currentNumber].Cells[2].Value.ToString(),
                checkBoxShowPrivateFolderDiffs.Checked).Show();
        }


        /// <summary>
        /// Обработка двойного клика для отображения различия в разных базах для выделенного пациента
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void BothMergeList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonInfo_Click(null, null);
        }


        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            labelInfo.Text = "Запущено сравнение пациентов из нашей и внешней баз...";
            MergeForm_Shown(null, null);
        }


        /// <summary>
        /// Очистить лог
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите очистить лог?", "Очистка лога", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                textBoxStatus.Text = string.Empty;
            }
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Показать пользователю, что сейчас делает программа
        /// </summary>
        /// <param name="text">Текст, который надо вывести на экран</param>
        public void SetStatus(string text)
        {
            if (string.IsNullOrEmpty(textBoxStatus.Text))
            {
                textBoxStatus.Text = text;
            }
            else
            {
                textBoxStatus.Text = text + "\r\n" + textBoxStatus.Text;
            }

            Application.DoEvents();
        }


        #region Сохранение размера и позиции формы
        private void MergeForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.MergeFormLocation = Location;
        }

        private void MergeForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            if (groupBoxBoth.Height < 191)
            {
                labelMove.Top = labelMove.Top + (191 - groupBoxBoth.Height);
            }

            _configurationEngine.MergeFormSize = Size;
            _configurationEngine.MergeFormLabelMoveTop = labelMove.Top.ToString();
        }
        #endregion


        #region Подсказки
        private void buttonCopySelectedToForeign_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Экспортировать выделенные записи из нашей базы во внешнюю", buttonCopySelectedToForeign);
            buttonCopySelectedToForeign.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopySelectedToForeign_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonCopySelectedToForeign.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopySelectedToOwn_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Импортировать выделенные записи из внешней базы в нашу", buttonCopySelectedToOwn);
            buttonCopySelectedToOwn.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopySelectedToOwn_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonCopySelectedToOwn.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopyAllNewToForeign_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Экспортировать все новые объекты из нашей базы во внешнюю", buttonCopyAllNewToForeign);
            buttonCopyAllNewToForeign.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopyAllNewToForeign_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonCopyAllNewToForeign.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopyAllNewToOwn_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Экспортировать все новые объекты из внешней базы в нашу", buttonCopyAllNewToOwn);
            buttonCopyAllNewToOwn.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopyAllNewToOwn_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonCopyAllNewToOwn.FlatStyle = FlatStyle.Flat;
        }

        private void buttonInfo_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Посмотреть список отличий для выбранного пациента", buttonInfo);
            buttonInfo.FlatStyle = FlatStyle.Popup;
        }

        private void buttonInfo_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonInfo.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть форму сравнения данных", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonRefresh_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Повторить сравнение данных в базах", buttonRefresh);
            buttonRefresh.FlatStyle = FlatStyle.Popup;
        }

        private void buttonRefresh_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonRefresh.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClearLog_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Очистить лог", buttonClearLog);
            buttonClearLog.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClearLog_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonClearLog.FlatStyle = FlatStyle.Flat;
        }
        #endregion


        #region Сброс фокуса с кнопок
        private void buttonInfo_Enter(object sender, EventArgs e)
        {
            BothMergeList.Focus();
        }

        private void buttonRefresh_Enter(object sender, EventArgs e)
        {
            OwnMergeList.Focus();
        }

        private void buttonCopySelectedToForeign_Enter(object sender, EventArgs e)
        {
            OwnMergeList.Focus();
        }

        private void buttonCopySelectedToOwn_Enter(object sender, EventArgs e)
        {
            ForeignMergeList.Focus();
        }
        #endregion


        #region Изменение пропорций для формы (высота окошек с информацией и окошка с логом)
        /// <summary>
        /// Нажата ли кнопка мыши на метке для перетаскивания
        /// </summary>
        private bool _isMousePressedOnMoveLabel;

        /// <summary>
        /// Сохранённые координаты курсора в момент нажатия
        /// </summary>
        private int _saveY;

        /// <summary>
        /// Сохранённое значение верхнего угла метки для перетаскивания в момент нажатия кнопки мыши
        /// </summary>
        private int _saveLabelMoveTop;

        /// <summary>
        /// Нажатие кнопки мыши. Сохранение начальных значений
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void labelMove_MouseDown(object sender, MouseEventArgs e)
        {
            _isMousePressedOnMoveLabel = true;
            _saveY = Cursor.Position.Y;
            _saveLabelMoveTop = labelMove.Top;
        }

        /// <summary>
        /// Отпускание кнопки мыши. Завершение перетаскиваний
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void labelMove_MouseUp(object sender, MouseEventArgs e)
        {
            _isMousePressedOnMoveLabel = false;
        }


        /// <summary>
        /// Движение мыши. Если кнопка нажата - то происходит изменение расположения элементов
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void labelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMousePressedOnMoveLabel)
            {
                return;
            }

            int shift = Cursor.Position.Y - _saveY;

            if (Height - 45 - (_saveLabelMoveTop + shift + labelMove.Height) < 47 ||
                _saveLabelMoveTop + shift - groupBoxBoth.Top < 191)
            {
                return;
            }

            labelMove.Top = _saveLabelMoveTop + shift;
            _configurationEngine.MergeFormLabelMoveTop = labelMove.Top.ToString();
        }


        /// <summary>
        /// Изменение положения элементов при изменении положения метки для перетаскивания
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void labelMove_LocationChanged(object sender, EventArgs e)
        {
            textBoxStatus.Top = labelMove.Top + labelMove.Height;
            textBoxStatus.Height = Height - 45 - textBoxStatus.Top;

            groupBoxMerge.Height = groupBoxBoth.Height = labelMove.Top - groupBoxBoth.Top;

            OwnMergeList.Height = ForeignMergeList.Height = groupBoxBoth.Height - 72;
            BothMergeList.Height = groupBoxBoth.Height - 56;
        }
        #endregion


        /// <summary>
        /// Отобразить различия при изменении чекбокса для отображения различий в личных папках
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxShowPrivateFolderDiffs_CheckedChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.MergeFormCheckBoxShowPrivateFolderDiffs = checkBoxShowPrivateFolderDiffs.Checked;
            ShowMergeInfo();
        }


        /// <summary>
        /// Обработка двойного клика
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void OwnMergeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OwnMergeList.SelectedItems.Count > 0)
            {
                CMergeInfo ownMergeInfo = _mergeDatabases.OwnMergeList[OwnMergeList.SelectedIndex];
                CMergeInfo foreignMergeInfo = _mergeDatabases.ForeignMergeList[OwnMergeList.SelectedIndex];

                new MergeShowDifferenceForm(
                    ownMergeInfo, foreignMergeInfo, OwnMergeList.SelectedItem.ToString(), ForeignMergeList.SelectedItem.ToString(), _configurationEngine).ShowDialog();
            }
        }


        /// <summary>
        /// Обработка двойного клика
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ForeignMergeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ForeignMergeList.SelectedItems.Count > 0)
            {
                CMergeInfo ownMergeInfo = _mergeDatabases.OwnMergeList[OwnMergeList.SelectedIndex];
                CMergeInfo foreignMergeInfo = _mergeDatabases.ForeignMergeList[OwnMergeList.SelectedIndex];

                new MergeShowDifferenceForm(
                    ownMergeInfo, foreignMergeInfo, OwnMergeList.SelectedItem.ToString(), ForeignMergeList.SelectedItem.ToString(), _configurationEngine).ShowDialog();
            }
        }


        /// <summary>
        /// Синхронизировать выделенные строки в списках изменений
        /// </summary>
        /// <param name="fromMergeList">Список, откуда брать выделенные строки</param>
        /// <param name="toMergeList">Список, в котором надо выделить нужные строки</param>
        private void SynchronizeSelectedItems(MultiRowListBox fromMergeList, MultiRowListBox toMergeList)
        {
            _stopSynchronizing = true;
            for (int i = 0; i < toMergeList.Items.Count; i++)
            {
                bool isCurrentSelected = toMergeList.SelectedIndices.Contains(i);
                bool isNewSelected = fromMergeList.SelectedIndices.Contains(i);

                if (isCurrentSelected != isNewSelected)
                {
                    toMergeList.SetSelected(i, isNewSelected);
                }
            }

            toMergeList.TopIndex = fromMergeList.TopIndex;

            _stopSynchronizing = false;
        }


        /// <summary>
        /// Изменение в выделенных строках в списке изменений для внешней базы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ForeignMergeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_stopSynchronizing)
            {
                return;
            }

            SynchronizeSelectedItems(ForeignMergeList, OwnMergeList);
        }


        /// <summary>
        /// Изменение в выделенных строках в списке изменений для нашей базы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void OwnMergeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_stopSynchronizing)
            {
                return;
            }

            SynchronizeSelectedItems(OwnMergeList, ForeignMergeList);
        }

        private bool _doNotSendToForeign;
        private bool _doNotSendToOwn;

        private void OwnMergeList_VScrollingChange(object sender, CScrollEventArgs e)
        {
            if (_doNotSendToForeign)
            {
                return;
            }

            int pos = e.Position;
            pos <<= 16;
            uint wParam = (uint)ScrollBarCommands.ThumbPosition | (uint)pos;
            _doNotSendToOwn = true;
            Win32Engine.SendMessage(ForeignMergeList.Handle, (int)ScrollMessage.WmVScroll, new IntPtr(wParam), new IntPtr(0));
            _doNotSendToOwn = false;
        }

        private void ForeignMergeList_VScrollingChange(object sender, CScrollEventArgs e)
        {
            if (_doNotSendToOwn)
            {
                return;
            }

            int pos = e.Position;
            pos <<= 16;
            uint wParam = (uint)ScrollBarCommands.ThumbPosition | (uint)pos;
            _doNotSendToForeign = true;
            Win32Engine.SendMessage(OwnMergeList.Handle, (int)ScrollMessage.WmVScroll, new IntPtr(wParam), new IntPtr(0));
            _doNotSendToForeign = false;
        }

        private void checkBoxCopyPrivateFolderData_CheckedChanged(object sender, EventArgs e)
        {
            _configurationEngine.MergeFormCheckBoxCopyPrivateFolderData = checkBoxCopyPrivateFolderData.Checked;
        }
    }
}
