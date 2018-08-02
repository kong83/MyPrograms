using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

using SurgeryHelper.Essences;
using SurgeryHelper.Forms;

using Application = Microsoft.Office.Interop.Excel.Application;

namespace SurgeryHelper.Tools
{
    public class CExcelExportHelper
    {
        private static Application _oxl;
        private static _Workbook _owb;
        private static _Worksheet _ows;
        private static Range _owr;

        private static WaitForm _waitForm;

        /// <summary>
        /// Импортировать в Word всех пациентов с переданными id
        /// </summary>
        /// <param name="workersKeeper">Класс с воркерами</param>
        /// <param name="importedPatientIds">Список id пациентов, которых надо импортировать</param>
        public static void Export(CWorkersKeeper workersKeeper, List<int> importedPatientIds)
        {
            _waitForm = new WaitForm();

            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();

                CPatient[] patientList = workersKeeper.PatientWorker.PatientList;

                // Стартуем Excel-приложение
                _oxl = new Application();

                _waitForm.SetProgress(10);

                // Создаем новую книгу
                _owb = _oxl.Workbooks.Add(Missing.Value);

                if (_owb.Sheets.Count > 2)
                {
                    _ows = (_Worksheet)_owb.Sheets[3];
                    _ows.Delete();
                }

                if (_owb.Sheets.Count > 1)
                {
                    _ows = (_Worksheet)_owb.Sheets[2];
                    _ows.Delete();
                }

                if (_owb.Sheets.Count > 0)
                {
                    _ows = (_Worksheet)_owb.Sheets[1];
                }
                else
                {
                    _ows = (_Worksheet)_owb.Sheets.Add();
                }

                _ows.Cells.WrapText = true;
                _ows.Cells.VerticalAlignment = 2;
                _ows.Cells.HorizontalAlignment = 2;

                _waitForm.SetProgress(20);

                #region Глобальные заголовки
                _owr = _ows.get_Range("A1", "AH1");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[1, 1] = "СПИСОК ПАЦИЕНТОВ НА " + CConvertEngine.DateTimeToString(DateTime.Now);

                _owr = _ows.get_Range("A2", "M2");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[2, 1] = "ОБЩИЕ ДАННЫЕ";

                _owr = _ows.get_Range("N2", "T2");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[2, 14] = "ГОСПИТАЛИЗАЦИИ";

                _owr = _ows.get_Range("U2", "AE2");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[2, 21] = "Данные по операциям";

                _owr = _ows.get_Range("AF2", "AH2");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[2, 32] = "КОНСУЛЬТАЦИИ";
                #endregion
                
                #region Заголовки
                _owr = _ows.get_Range("A3", "A3");
                _owr.ColumnWidth = 4;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "№";

                _owr = _ows.get_Range("B3", "B3");
                _owr.ColumnWidth = 15;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "ФИО пациента";

                _owr = _ows.get_Range("C3", "C3");
                _owr.ColumnWidth = 4;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Возраст";

                _owr = _ows.get_Range("D3", "D3");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата рождения";

                _owr = _ows.get_Range("E3", "E3");
                _owr.ColumnWidth = 21;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Адрес";

                _owr = _ows.get_Range("F3", "F3");
                _owr.ColumnWidth = 11;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Телефон";

                _owr = _ows.get_Range("G3", "G3");
                _owr.ColumnWidth = 11;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "E-mail";

                _owr = _ows.get_Range("H3", "H3");
                _owr.ColumnWidth = 11;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Нозология";

                _owr = _ows.get_Range("I3", "I3");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата травмы";

                _owr = _ows.get_Range("J3", "J3");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Количество госпитализаций";

                _owr = _ows.get_Range("K3", "K3");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Количество консультаций в офисе";

                _owr = _ows.get_Range("L3", "L3");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Количество операций";

                _owr = _ows.get_Range("M3", "M3");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата поступления";

                _owr = _ows.get_Range("N3", "N3");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата выписки";

                _owr = _ows.get_Range("O3", "O3");
                _owr.ColumnWidth = 5;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "К/д";

                _owr = _ows.get_Range("P3", "P3");
                _owr.ColumnWidth = 7;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "№ ИБ";

                _owr = _ows.get_Range("Q3", "Q3");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Лечащий врач";

                _owr = _ows.get_Range("R3", "R3");
                _owr.ColumnWidth = 22;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Диагноз";

                _owr = _ows.get_Range("S3", "S3");
                _owr.ColumnWidth = 14;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Карты обследования";

                _owr = _ows.get_Range("T3", "T3");
                _owr.ColumnWidth = 28;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Название операции";

                _owr = _ows.get_Range("U3", "U3");
                _owr.ColumnWidth = 11;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Тип операции";

                _owr = _ows.get_Range("V3", "V3");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата операции";

                _owr = _ows.get_Range("W3", "W3");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Время начала операции";

                _owr = _ows.get_Range("X3", "X3");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Время окончания операции";

                _owr = _ows.get_Range("Y3", "Y3");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Список хирургов";

                _owr = _ows.get_Range("Z3", "Z3");
                _owr.ColumnWidth = 12;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Список ассистентов";

                _owr = _ows.get_Range("AA3", "AA3");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Анестезиолог";

                _owr = _ows.get_Range("AB3", "AB3");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Анестезистка";

                _owr = _ows.get_Range("AC3", "AC3");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Операц. мед. сестра";

                _owr = _ows.get_Range("AD3", "AD3");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Санитар";

                _owr = _ows.get_Range("AE3", "AE3");
                _owr.ColumnWidth = 140;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Ход операции";

                _owr = _ows.get_Range("AF3", "AF3");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата консультации в офисе";

                _owr = _ows.get_Range("AG3", "AG3");
                _owr.ColumnWidth = 50;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Диагноз";

                _owr = _ows.get_Range("AH3", "AH3");
                _owr.ColumnWidth = 14;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Карты обследования";
                #endregion

                int rowCnt = 4;
                int n = 1;
                double progressStep = patientList.Length == 0
                    ? 0
                    : 70.0 / patientList.Length;
                double progress = 30;
                for (int i = 0; i < patientList.Length; i++)
                {
                    _waitForm.SetProgress(progress);
                    progress += progressStep;

                    if (!importedPatientIds.Contains(patientList[i].Id))
                    {
                        continue;
                    }

                    // Общие данные о пациенте
                    _ows.Cells[rowCnt, 1] = n.ToString();
                    n++;
                    _ows.Cells[rowCnt, 2] = patientList[i].GetFullName();
                    _ows.Cells[rowCnt, 3] = CConvertEngine.GetAge(patientList[i].Birthday);
                    _ows.Cells[rowCnt, 4] = CConvertEngine.DateTimeToString(patientList[i].Birthday);
                    _ows.Cells[rowCnt, 5] = patientList[i].GetAddress();
                    _ows.Cells[rowCnt, 6] = patientList[i].Phone;
                    _ows.Cells[rowCnt, 7] = patientList[i].EMail;
                    _ows.Cells[rowCnt, 8] = patientList[i].Nosology;
                    if (workersKeeper.AnamneseWorker.IsExists(patientList[i].Id))
                    {
                        _ows.Cells[rowCnt, 9] = CConvertEngine.DateTimeToString(workersKeeper.AnamneseWorker.GetByPatientId(patientList[i].Id).TraumaDate);
                    }

                    _ows.Cells[rowCnt, 10] = workersKeeper.HospitalizationWorker.GetCountByPatientId(patientList[i].Id).ToString();
                    _ows.Cells[rowCnt, 11] = workersKeeper.VisitWorker.GetCountByPatientId(patientList[i].Id).ToString();
                    _ows.Cells[rowCnt, 12] = workersKeeper.OperationWorker.GetCountByPatientId(patientList[i].Id).ToString();

                    // Госпитализации
                    int saveRowCnt = rowCnt;
                    foreach (CHospitalization hospitalization in workersKeeper.HospitalizationWorker.GetListByPatientId(patientList[i].Id))
                    {
                        _ows.Cells[rowCnt, 13] = CConvertEngine.DateTimeToString(hospitalization.DeliveryDate, true);
                        _ows.Cells[rowCnt, 14] = CConvertEngine.DateTimeToString(hospitalization.ReleaseDate);
                        _ows.Cells[rowCnt, 15] = hospitalization.KD;
                        _ows.Cells[rowCnt, 16] = hospitalization.NumberOfCaseHistory;
                        _ows.Cells[rowCnt, 17] = hospitalization.DoctorInChargeOfTheCase;
                        _ows.Cells[rowCnt, 18] = hospitalization.Diagnose;
                        _ows.Cells[rowCnt, 10] = GetExistingCardsInfo(workersKeeper, hospitalization.Id, -1);

                        // Операции
                        foreach (COperation operation in workersKeeper.OperationWorker.GetListByHospitalizationId(hospitalization.Id))
                        {
                            _ows.Cells[rowCnt, 20] = operation.Name;
                            _ows.Cells[rowCnt, 21] = CConvertEngine.ListToString(operation.OperationTypes, ", ");
                            _ows.Cells[rowCnt, 22] = CConvertEngine.DateTimeToString(operation.DateOfOperation);
                            _ows.Cells[rowCnt, 23] = CConvertEngine.TimeToString(operation.StartTimeOfOperation);
                            _ows.Cells[rowCnt, 24] = CConvertEngine.TimeToString(operation.EndTimeOfOperation);
                            _ows.Cells[rowCnt, 25] = CConvertEngine.ListToString(operation.Surgeons, ", ");
                            _ows.Cells[rowCnt, 26] = CConvertEngine.ListToString(operation.Assistents, ", ");
                            _ows.Cells[rowCnt, 27] = operation.HeAnaesthetist;
                            _ows.Cells[rowCnt, 28] = operation.SheAnaesthetist;
                            _ows.Cells[rowCnt, 29] = operation.ScrubNurse;
                            _ows.Cells[rowCnt, 30] = operation.Orderly;
                            _ows.Cells[rowCnt++, 31] = workersKeeper.OperationProtocolWorker.GetByOperationId(operation.Id).OperationCourse;
                        }

                        if (workersKeeper.OperationWorker.GetCountByHospitalizationId(hospitalization.Id) == 0)
                        {
                            rowCnt++;
                        }
                    }

                    // Консультации
                    rowCnt = saveRowCnt;
                    foreach (CVisit visit in workersKeeper.VisitWorker.GetListByPatientId(patientList[i].Id))
                    {
                        _ows.Cells[rowCnt, 32] = CConvertEngine.DateTimeToString(visit.VisitDate);
                        _ows.Cells[rowCnt, 33] = visit.Diagnose;
                        _ows.Cells[rowCnt++, 34] = GetExistingCardsInfo(workersKeeper, -1, visit.Id);
                    }

                    int maxCnt = Math.Max(
                        workersKeeper.OperationWorker.GetCountByPatientId(patientList[i].Id),
                        workersKeeper.VisitWorker.GetCountByPatientId(patientList[i].Id));

                    rowCnt = saveRowCnt + Math.Max(1, maxCnt);
                }

                _waitForm.SetProgress(100);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                if (_oxl != null)
                {
                    _oxl.Visible = true;
                    _oxl.UserControl = true;

                    if (_owb != null)
                    {
                        Marshal.ReleaseComObject(_owb);
                        _owb = null;
                    }

                    if (_ows != null)
                    {
                        Marshal.ReleaseComObject(_ows);
                        _ows = null;
                    }

                    if (_owr != null)
                    {
                        Marshal.ReleaseComObject(_owr);
                        _owr = null;
                    }

                    Marshal.ReleaseComObject(_oxl);
                    _oxl = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }


        /// <summary>
        /// Получить описание карт, которые есть для указанного id госпитализации и id консультации
        /// </summary>
        /// <param name="workersKeeper">Класс с воркерами</param>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <param name="visitId">id консультации</param>
        /// <returns></returns>
        private static string GetExistingCardsInfo(CWorkersKeeper workersKeeper, int hospitalizationId, int visitId)
        {
            var cards = new StringBuilder();
            if (workersKeeper.ObstetricParalysisCardWorker.IsExists(hospitalizationId, visitId))
            {
                cards.Append("Акушерский паралич\r\n");
            }

            if (workersKeeper.BrachialPlexusCardWorker.IsExists(hospitalizationId, visitId))
            {
                cards.Append("Плечевое сплетение\r\n");
            }

            if (workersKeeper.CardWorker.IsExists(hospitalizationId, visitId, CardType.SacriplexCard))
            {
                cards.Append("Кресцовое сплетение\r\n");
            }

            if (workersKeeper.RangeOfMotionCardWorker.IsExists(hospitalizationId, visitId))
            {
                cards.Append("Объём движений\r\n");
            }

            if (workersKeeper.CardWorker.IsExists(hospitalizationId, visitId, CardType.HandCutaneousNerves))
            {
                cards.Append("Кожные нервы руки\r\n");
            }

            if (workersKeeper.CardWorker.IsExists(hospitalizationId, visitId, CardType.HandDermatome))
            {
                cards.Append("Дерматомы руки\r\n");
            }

            if (workersKeeper.CardWorker.IsExists(hospitalizationId, visitId, CardType.LegCutaneousNerves))
            {
                cards.Append("Кожные нервы ноги\r\n");
            }

            if (workersKeeper.CardWorker.IsExists(hospitalizationId, visitId, CardType.LegDermatome))
            {
                cards.Append("Дерматомы ноги\r\n");
            }

            if (workersKeeper.CardWorker.IsExists(hospitalizationId, visitId, CardType.PamplegiaCard))
            {
                cards.Append("Тетраплегия\r\n");
            }

            if (cards.Length > 2)
            {
                return cards.ToString().Substring(0, cards.Length - 2);
            }

            return cards.ToString();
        }
    }
}
