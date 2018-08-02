using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class CheckDBForm : Form
    {
        private readonly CWorkersKeeper _workersKeeper;

        public CheckDBForm(CWorkersKeeper workersKeeper)
        {
            _workersKeeper = workersKeeper;

            InitializeComponent();
        }

        private void CheckDBForm_Shown(object sender, EventArgs e)
        {
            RunCheck();
        }

        private void RunCheck()
        {
            WrongItemsList.Items.Clear();

            CheckPatients();

            CheckAnamneses();
            CheckObstetricHistorys();

            CheckCards();
            CheckBrachialPlexusCards();
            CheckObstetricParalysisCards();
            CheckRangeOfMotionCard();

            CheckDischargeEpicrisis();
            CheckLineOfCommunicationEpicrisis();
            CheckTransferableEpicrisis();
            CheckMedicalInspections();

            CheckOperationProtocols();
            CheckOperations();
            CheckHospitalizations();
            CheckVisits();

            labelInfo.Text = WrongItemsList.Items.Count == 0 
                ? "Ошибок в базе данных не найдено" 
                : string.Format("В базе данных найдено {0} ошибок", WrongItemsList.Items.Count);

            WrongItemsList.Focus();
        }

        private void CheckPatients()
        {
            List<CPatient> dubbedPatients = _workersKeeper.PatientWorker.GetDubbedPatients();            
            foreach (CPatient patient in dubbedPatients)
            {
                string info = string.Format(
                    "Дублированный пациент: id пациента={0}, Имя={1}, Нозология={2}",
                    patient.Id,
                    patient.GetFullName(),
                    patient.Nosology);
                WrongItemsList.Items.Add(info);
            }

            List<CPatient> wrongPatients = _workersKeeper.PatientWorker.GetWrongPatients();
            foreach (CPatient patient in wrongPatients)
            {
                string info = string.Format(
                    "Пациент без имени или без нозологии: id пациента={0}, Имя={1}, Нозология={2}",
                    patient.Id,
                    patient.GetFullName(),
                    patient.Nosology);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckOperationProtocols()
        {
            List<COperationProtocol> wrongOperationProtocols = _workersKeeper.OperationProtocolWorker.GetWrongOperationProtocols(_workersKeeper.OperationWorker);

            foreach (COperationProtocol operationProtocol in wrongOperationProtocols)
            {
                string info = string.Format(
                    "Протокол операции: id операции={0}, Время написания={1}, Жалобы={2},  Ход операции={3}",
                    operationProtocol.OperationId,
                    CConvertEngine.DateTimeToString(operationProtocol.TimeWriting),
                    operationProtocol.Complaints,
                    operationProtocol.OperationCourse);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckRangeOfMotionCard()
        {
            List<CRangeOfMotionCard> wrongRangeOfMotionCards = _workersKeeper.RangeOfMotionCardWorker.GetWrongRangeOfMotionCards(_workersKeeper.HospitalizationWorker, _workersKeeper.VisitWorker);

            foreach (CRangeOfMotionCard rangeOfMotionCard in wrongRangeOfMotionCards)
            {
                string info = string.Format(
                    "Карта на объём движений: id госп-и={0}, id консультации={1}, Список полей={2}",
                    rangeOfMotionCard.HospitalizationId,
                    rangeOfMotionCard.VisitId,
                    CConvertEngine.ListToString(rangeOfMotionCard.Fields, ", "));
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckObstetricParalysisCards()
        {
            List<CObstetricParalysisCard> wrongObstetricParalysisCards = _workersKeeper.ObstetricParalysisCardWorker.GetWrongObstetricParalysisCards(_workersKeeper.HospitalizationWorker, _workersKeeper.VisitWorker);

            foreach (CObstetricParalysisCard obstetricParalysisCard in wrongObstetricParalysisCards)
            {
                string info = string.Format(
                    "Акушерский паралич: id госп-и={0}, id консультации={1}, Данные комбобоксов={2}",
                    obstetricParalysisCard.HospitalizationId,
                    obstetricParalysisCard.VisitId,
                    CConvertEngine.ListToString(obstetricParalysisCard.ComboBoxes, ", "));
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckCards()
        {
            List<CCard> wrongCards = _workersKeeper.CardWorker.GetWrongCards(_workersKeeper.HospitalizationWorker, _workersKeeper.VisitWorker);

            foreach (CCard card in wrongCards)
            {
                string info = string.Format(
                    "Карта обследования: id госп-и={0}, id консультации={1}, Файл с картинкой={2}",
                    card.HospitalizationId,
                    card.VisitId,
                    card.GetPictureFileName());
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckBrachialPlexusCards()
        {
            List<CBrachialPlexusCard> wrongBrachialPlexusCards = _workersKeeper.BrachialPlexusCardWorker.GetWrongBrachialPlexusCards(_workersKeeper.HospitalizationWorker, _workersKeeper.VisitWorker);

            foreach (CBrachialPlexusCard brachialPlexusCard in wrongBrachialPlexusCards)
            {
                string info = string.Format(
                    "Карта на плечевое сплетение: id госп-и={0}, id консультации={1}, Файл с картинкой={2}",
                    brachialPlexusCard.HospitalizationId,
                    brachialPlexusCard.VisitId,
                    brachialPlexusCard.GetPictureFileName());
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckObstetricHistorys()
        {
            List<CObstetricHistory> wrongObstetricHistorys = _workersKeeper.ObstetricHistoryWorker.GetWrongObstetricHistorys(_workersKeeper.PatientWorker);

            foreach (CObstetricHistory obstetricHistory in wrongObstetricHistorys)
            {
                string info = string.Format(
                    "Акушерский анамнез: id пациента={0}, Стационарное лечение={1}, Кем диагностирован={2}",
                    obstetricHistory.PatientId,
                    obstetricHistory.HospitalTreatment,
                    obstetricHistory.ObstetricParalysis);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckAnamneses()
        {
            List<CAnamnese> wrongAnamneses = _workersKeeper.AnamneseWorker.GetWrongAnamneses(_workersKeeper.PatientWorker);

            foreach (CAnamnese anamnese in wrongAnamneses)
            {
                string info = string.Format(
                    "Анамнез: id пациента={0}, Дата травмы={1}, AnMorbi={2}",                    
                    anamnese.PatientId,
                    CConvertEngine.DateTimeToString(anamnese.TraumaDate),
                    anamnese.AnMorbi);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckMedicalInspections()
        {
            List<CMedicalInspection> wrongMedicalInspections = _workersKeeper.MedicalInspectionWorker.GetWrongMedicalInspections(_workersKeeper.HospitalizationWorker);

            foreach (CMedicalInspection medicalInspection in wrongMedicalInspections)
            {
                string info = string.Format(
                    "Обследование в отделении: id госп-и={0}, Жалобы={1}, AnMorbi={2}",
                    medicalInspection.HospitalizationId,
                    medicalInspection.Complaints,
                    medicalInspection.AnamneseAnMorbi);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckTransferableEpicrisis()
        {
            List<CTransferableEpicrisis> wrongTransferableEpicrisiss = _workersKeeper.TransferableEpicrisisWorker.GetWrongTransferableEpicrisis(_workersKeeper.HospitalizationWorker);

            foreach (CTransferableEpicrisis transferableEpicrisis in wrongTransferableEpicrisiss)
            {
                string info = string.Format(
                    "Переводной эпикриз: id госп-и={0}, Дата написания={1}, План={2}",
                    transferableEpicrisis.HospitalizationId,
                    CConvertEngine.DateTimeToString(transferableEpicrisis.WritingDate),
                    transferableEpicrisis.Plan);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckLineOfCommunicationEpicrisis()
        {
            List<CLineOfCommunicationEpicrisis> wrongLineOfCommunicationEpicrisiss = _workersKeeper.LineOfCommunicationEpicrisisWorker.GetWrongLineOfCommunicationEpicrisis(_workersKeeper.HospitalizationWorker);

            foreach (CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisis in wrongLineOfCommunicationEpicrisiss)
            {
                string info = string.Format(
                    "Этапный эпикриз: id госп-и={0}, Дата написания={1}, План={2}",
                    lineOfCommunicationEpicrisis.HospitalizationId,
                    CConvertEngine.DateTimeToString(lineOfCommunicationEpicrisis.WritingDate),
                    lineOfCommunicationEpicrisis.Plan);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckDischargeEpicrisis()
        {
            List<CDischargeEpicrisis> wrongDischargeEpicrisiss = _workersKeeper.DischargeEpicrisisWorker.GetWrongDischargeEpicrisis(_workersKeeper.HospitalizationWorker);

            foreach (CDischargeEpicrisis dischargeEpicrisis in wrongDischargeEpicrisiss)
            {
                string info = string.Format(
                    "Выписной эпикриз: id госп-и={0}, После операции={1}, Рекомендации={2}",
                    dischargeEpicrisis.HospitalizationId,
                    dischargeEpicrisis.AfterOperation,
                    dischargeEpicrisis.Recomendations);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckHospitalizations()
        {
            List<CHospitalization> wrongHospitalizations = _workersKeeper.HospitalizationWorker.GetWrongHospitalizations(_workersKeeper.PatientWorker);
            
            foreach (CHospitalization hospitalization in wrongHospitalizations)
            {
                string info = string.Format(
                    "Госпитализация: id={0}, id пациента={1}, Номер ИБ={2}, Лечащий врач={3}, Дата поступления={4}, Диагноз={5}",
                    hospitalization.Id,
                    hospitalization.PatientId,                    
                    hospitalization.NumberOfCaseHistory,
                    hospitalization.DoctorInChargeOfTheCase,
                    CConvertEngine.DateTimeToString(hospitalization.DeliveryDate),
                    hospitalization.DiagnoseOneLine);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckVisits()
        {
            List<CVisit> wrongVisits = _workersKeeper.VisitWorker.GetWrongVisits(_workersKeeper.PatientWorker);

            foreach (CVisit visit in wrongVisits)
            {
                string info = string.Format(
                    "Консультация: id={0}, id пациента={1}, Дата консультации={2}, Лечащий врач={3}, Диагноз={4}",
                    visit.Id,
                    visit.PatientId,
                    CConvertEngine.DateTimeToString(visit.VisitDate),
                    visit.Doctor,
                    visit.DiagnoseOneLine);
                WrongItemsList.Items.Add(info);
            }
        }

        private void CheckOperations()
        {
            List<COperation> wrongOperations = _workersKeeper.OperationWorker.GetWrongOperations(_workersKeeper.PatientWorker, _workersKeeper.HospitalizationWorker);

            foreach (COperation operation in wrongOperations)
            {
                string info = string.Format(
                    "Операция: id={0}, id пациента={1}, id госп-и={2},  Дата операции={3}, Хирург(и)={4}, Название={5}",
                    operation.Id,
                    operation.PatientId,
                    operation.HospitalizationId,
                    CConvertEngine.DateTimeToString(operation.DateOfOperation),
                    CConvertEngine.ListToString(operation.Surgeons, ", "),
                    operation.Name);
                WrongItemsList.Items.Add(info);
            }
        }

        #region Подсказки
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть окно", buttonOK);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOK.FlatStyle = FlatStyle.Flat;
        }


        private void buttonRefresh_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Обновить", buttonRefresh);
            buttonRefresh.FlatStyle = FlatStyle.Popup;
        }

        private void buttonRefresh_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonRefresh.FlatStyle = FlatStyle.Flat;
        }
        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }        

        private void WrongItemsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WrongItemsList.SelectedItems.Count > 0)
            {
                MessageBox.Show(WrongItemsList.SelectedItem.ToString(), "Полный текст");
            }
        }

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RunCheck();
        }

        /// <summary>
        /// Сброс фокуса с кнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            WrongItemsList.Focus();
        }

        private void buttonRemoveSelected_Click(object sender, EventArgs e)
        {
            if (WrongItemsList.SelectedItems.Count == 0)
            {
                MessageBox.ShowDialog("Нет выделенных объектов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (string str in WrongItemsList.SelectedItems)
            {
                string nameOfObject = str.Substring(0, str.IndexOf(':'));
                switch (nameOfObject)
                {
                    case "Дублированный пациент":
                    case "Пациент без имени или без нозологии":
                        RemovePatient(str);
                        break;
                    case "Переводной эпикриз":
                        RemoveTransferableEpicrisis(str);
                        break;
                    case "Протокол операции":
                        RemoveOperationProtocol(str);
                        break;
                    case "Карта на объём движений":
                        RemoveRangeOfMotionCard(str);
                        break;
                    case "Акушерский паралич":
                        RemoveObstetricParalysisCard(str);
                        break;
                    case "Карта обследования":
                        RemoveCard(str);
                        break;
                    case "Карта на плечевое сплетение":
                        RemoveBrachialPlexusCard(str);
                        break;
                    case "Акушерский анамнез":
                        RemoveObstetricHistory(str);
                        break;
                    case "Анамнез":
                        RemoveAnamnese(str);
                        break;
                    case "Обследование в отделении":
                        RemoveMedicalInspection(str);
                        break;
                    case "Этапный эпикриз":
                        RemoveLineOfCommunicationEpicrisis(str);
                        break;
                    case "Выписной эпикриз":
                        RemoveDischargeEpicrisis(str);
                        break;
                    case "Консультация":
                        RemoveVisit(str);
                        break;
                    case "Операция":
                        RemoveOperation(str);
                        break;
                    case "Госпитализация":
                        RemoveHospitalization(str);
                        break;
                        
                }
            }

            buttonRefresh_Click(null, null);
        }

        private static int GetNumberAfterParameter(string str, string param)
        {
            string res = string.Empty;
            int cur = str.IndexOf(param + "=");
            if (cur == -1)
            {
                throw new Exception("В строке '" + str + "' не найдена информация об '" + param + "'");
            }

            cur += param.Length + 1;
            while (cur < str.Length && str[cur] != ',')
            {
                res += str[cur++];
            }

            return Convert.ToInt32(res);
        }

        private static int GetId(string str)
        {
            return GetNumberAfterParameter(str, "id");
        }

        private static int GetHospitalizationId(string str)
        {
            return GetNumberAfterParameter(str, "id госп-и");
        }

        private static int GetPatientId(string str)
        {
            return GetNumberAfterParameter(str, "id пациента");
        }

        private static int GetOperationId(string str)
        {
            return GetNumberAfterParameter(str, "id операции");
        }

        private static int GetVisitId(string str)
        {
            return GetNumberAfterParameter(str, "id консультации");
        }

        private void RemovePatient(string str)
        {
            _workersKeeper.PatientWorker.Remove(GetPatientId(str));
        }

        private void RemoveHospitalization(string str)
        {
            _workersKeeper.HospitalizationWorker.Remove(GetId(str));
        }

        private void RemoveOperation(string str)
        {
            _workersKeeper.OperationWorker.Remove(GetId(str));
        }

        private void RemoveVisit(string str)
        {
            _workersKeeper.VisitWorker.Remove(GetId(str));
        }

        private void RemoveDischargeEpicrisis(string str)
        {            
            _workersKeeper.DischargeEpicrisisWorker.Remove(GetHospitalizationId(str));
        }        

        private void RemoveLineOfCommunicationEpicrisis(string str)
        {
            _workersKeeper.LineOfCommunicationEpicrisisWorker.Remove(GetHospitalizationId(str));
        }

        private void RemoveMedicalInspection(string str)
        {
            _workersKeeper.MedicalInspectionWorker.Remove(GetHospitalizationId(str));
        }

        private void RemoveTransferableEpicrisis(string str)
        {
            _workersKeeper.TransferableEpicrisisWorker.Remove(GetHospitalizationId(str));
        }

        private void RemoveAnamnese(string str)
        {
            _workersKeeper.AnamneseWorker.Remove(GetPatientId(str));
        }

        private void RemoveObstetricHistory(string str)
        {
            _workersKeeper.ObstetricHistoryWorker.Remove(GetPatientId(str));
        }

        private void RemoveBrachialPlexusCard(string str)
        {
            _workersKeeper.BrachialPlexusCardWorker.Remove(GetHospitalizationId(str), GetVisitId(str));
        }

        private void RemoveCard(string str)
        {
            _workersKeeper.CardWorker.Remove(GetHospitalizationId(str), GetVisitId(str));
        }

        private void RemoveObstetricParalysisCard(string str)
        {
            _workersKeeper.ObstetricParalysisCardWorker.Remove(GetHospitalizationId(str), GetVisitId(str));
        }

        private void RemoveRangeOfMotionCard(string str)
        {
            _workersKeeper.RangeOfMotionCardWorker.Remove(GetHospitalizationId(str), GetVisitId(str));
        }

        private void RemoveOperationProtocol(string str)
        {
            _workersKeeper.OperationProtocolWorker.Remove(GetOperationId(str));
        }  
    }
}
