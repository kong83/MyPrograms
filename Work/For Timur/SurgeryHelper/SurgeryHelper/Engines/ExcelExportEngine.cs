using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SurgeryHelper.Entities;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.Diagnostics;

namespace SurgeryHelper.Engines
{
    public class ExcelExportEngine
    {
        private static Application _oxl;
        private static _Workbook _oxb;
        private static _Worksheet _ows;
        private static Range _owr;

        private static Process[] _excelProcessesBeforeStartWork;
        private static Process[] _excelProcessesAfterStartWork;

        [DllImport("kernel32.dll")]
        private static extern int WinExec(string lpCmdLine, int nCmdShow);

        public static string GetServiceDataFromFile(string servicesFilePath)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _excelProcessesBeforeStartWork = Process.GetProcessesByName("EXCEL");

                // Стартуем Excel-приложение
                _oxl = new Application();

                _oxb = _oxl.Workbooks.Open(servicesFilePath, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                try
                {
                    _ows = (_Worksheet)_oxb.Sheets["Группировщик детальный"];
                }
                catch
                {
                    throw new Exception("Лист с именем 'Группировщик детальный' не найден. Вероятно, указанный файл имеет неверный формат.");
                }

                int rowCnt = 2;

                int shag = 10000;
                while (shag > 0)
                {
                    while (((Range)_ows.Cells[rowCnt, 1]).Value2 != null)
                    {
                        rowCnt += shag;
                    }

                    rowCnt -= shag;

                    shag /= 10;
                }

                var data = (object[,])_ows.get_Range(_ows.Cells[2, 6], _ows.Cells[rowCnt, 14]).Value2;

                var sb = new StringBuilder();
                for (int i = 1; i < rowCnt; i++)
                {
                    string service = data[i, 2]?.ToString();
                    if (string.IsNullOrEmpty(service))
                    {
                        continue;
                    }

                    sb.AppendFormat("{0};{1};{2};{3}^",
                        service,
                        data[i, 1],
                        data[i, 8],
                        data[i, 9]);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("В процессе загрузки данных с услугами произошла ошибка:\r\n" + ex);
            }
            finally
            {
                QuitExcel();
                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }
        
        public static string GetMkbDataFromFile(string mkbFilePath)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _excelProcessesBeforeStartWork = Process.GetProcessesByName("EXCEL");

                // Стартуем Excel-приложение
                _oxl = new Application();

                _oxb = _oxl.Workbooks.Open(mkbFilePath, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                try
                {
                    _ows = (_Worksheet)_oxb.Sheets["МКБ 10"];
                }
                catch                
                {
                    _ows = (_Worksheet)_oxb.Sheets[1];
                }

                int rowCnt = 2;

                int shag = 10000;
                while (shag > 0)
                {
                    while (((Range)_ows.Cells[rowCnt, 1]).Value2 != null)
                    {
                        rowCnt += shag;
                    }

                    rowCnt -= shag;

                    shag /= 10;
                }

                var data = (object[,])_ows.get_Range(_ows.Cells[2, 1], _ows.Cells[rowCnt, 2]).Value2;

                var sb = new StringBuilder();
                for (int i = 1; i < rowCnt; i++)
                {
                    string mkbCode = data[i, 1]?.ToString();
                    if (string.IsNullOrEmpty(mkbCode))
                    {
                        continue;
                    }
                    
                    sb.AppendFormat("Code={0}{2}Name={1}{3}",
                        mkbCode,
                        data[i, 2],
                        DbEngine.DataSplitStr,
                        DbEngine.ObjSplitStr);
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("В процессе загрузки кодов МКБ произошла ошибка:\r\n" + ex);
            }
            finally
            {
                QuitExcel();
                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        private static void QuitExcel()
        {
            if (_oxl != null)
            {
                if (_oxb != null)
                {
                    _oxb.Close(false, Missing.Value, Missing.Value);

                    if (_ows != null)
                    {
                        Marshal.ReleaseComObject(_ows);
                        GC.GetTotalMemory(true);
                        _ows = null;
                    }

                    if (_owr != null)
                    {
                        Marshal.ReleaseComObject(_owr);
                        GC.GetTotalMemory(true);
                        _owr = null;
                    }

                    Marshal.ReleaseComObject(_oxb);
                    GC.GetTotalMemory(true);
                    _oxb = null;
                }

                _oxl.Quit();

                Marshal.ReleaseComObject(_oxl);
                GC.GetTotalMemory(true);
                _oxl = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                Thread.Sleep(100);
            }

            _excelProcessesAfterStartWork = Process.GetProcessesByName("EXCEL");

            //
            // Find created process and kill it, if standart close is not successful
            //
            for (int i = 0; i < _excelProcessesAfterStartWork.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < _excelProcessesBeforeStartWork.Length; j++)
                {
                    if (_excelProcessesBeforeStartWork[j].Id == _excelProcessesAfterStartWork[i].Id)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    WinExec("taskkill /F /PID " + _excelProcessesAfterStartWork[i].Id, 0);
                }
            }
        }

        public static void Export(List<PatientClass> patientList, DbEngine dbEngine)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                // Стартуем Excel-приложение
                _oxl = new Application();

                // Создаем новую книгу
                _oxb = _oxl.Workbooks.Add(Missing.Value);
                
                int len = _oxb.Sheets.Count;
                for (int i = len; i > 1; i--)
                {
                    ((_Worksheet)_oxb.Sheets[i]).Delete();
                }
                
                _ows = (_Worksheet)_oxb.Sheets[1];

                _ows.Cells.WrapText = true;
                _ows.Cells.VerticalAlignment = 2;
                _ows.Cells.HorizontalAlignment = 2;

                _owr = _ows.get_Range("A1", "Q1");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[1, 1] = "Список пациентов на " + ConvertEngine.GetRightDateString(DateTime.Now);

                _owr = _ows.get_Range("R1", "AB1");
                _owr.MergeCells = true;
                _owr.Font.Bold = true;
                _owr.Font.Size = 14;
                _owr.RowHeight = 30;
                _owr.HorizontalAlignment = 3;
                _ows.Cells[1, 18] = "Данные по операциям";

                _owr = _ows.get_Range("A2", "A2");
                _owr.ColumnWidth = 15;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "ФИО пациента";

                _owr = _ows.get_Range("B2", "B2");
                _owr.ColumnWidth = 4;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Возраст";

                _owr = _ows.get_Range("C2", "C2");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата рождения";

                _owr = _ows.get_Range("D2", "D2");
                _owr.ColumnWidth = 14;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Адрес";

                _owr = _ows.get_Range("E2", "E2");
                _owr.ColumnWidth = 14;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Место работы";

                _owr = _ows.get_Range("F2", "F2");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Телефон";

                _owr = _ows.get_Range("G2", "G2");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Тип стационара";

                _owr = _ows.get_Range("H2", "H2");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Код МКБ";

                _owr = _ows.get_Range("I2", "I2");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Код услуги";

                _owr = _ows.get_Range("J2", "J2");
                _owr.ColumnWidth = 20;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Название услуги";

                _owr = _ows.get_Range("K2", "K2");
                _owr.ColumnWidth = 13;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Нозология";

                _owr = _ows.get_Range("L2", "L2");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Лечащий врач";

                _owr = _ows.get_Range("M2", "M2");
                _owr.ColumnWidth = 9;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата поступления";

                _owr = _ows.get_Range("N2", "N2");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата выписки";

                _owr = _ows.get_Range("O2", "O2");
                _owr.ColumnWidth = 7;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "№ истории болезни";

                _owr = _ows.get_Range("P2", "P2");
                _owr.ColumnWidth = 5;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Количество операций";

                _owr = _ows.get_Range("Q2", "Q2");
                _owr.ColumnWidth = 25;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Диагноз";

                _owr = _ows.get_Range("R2", "R2");
                _owr.ColumnWidth = 22;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Название операции";

                _owr = _ows.get_Range("S2", "S2");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Дата операции";

                _owr = _ows.get_Range("T2", "T2");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Время начала операции";

                _owr = _ows.get_Range("U2", "U2");
                _owr.ColumnWidth = 6;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Время окончания операции";

                _owr = _ows.get_Range("V2", "V2");
                _owr.ColumnWidth = 10;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Список хирургов";

                _owr = _ows.get_Range("W2", "W2");
                _owr.ColumnWidth = 12;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Список ассистентов";

                _owr = _ows.get_Range("X2", "X2");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Анестезист";

                _owr = _ows.get_Range("Y2", "Y2");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Анестезистка";

                _owr = _ows.get_Range("Z2", "Z2");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Операц. мед. сестра";

                _owr = _ows.get_Range("AA2", "AA2");
                _owr.ColumnWidth = 8;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Санитар";
                
                _owr = _ows.get_Range("AB2", "AB2");
                _owr.ColumnWidth = 85;
                _owr.Font.Bold = true;
                _owr.HorizontalAlignment = 3;
                _owr.Value2 = "Ход операции";

                int rowCnt = 3;
                for (int i = 0; i < patientList.Count; i++)
                {
                    _ows.Cells[rowCnt, 1] = patientList[i].GetFullName();
                    _ows.Cells[rowCnt, 2] = patientList[i].Age;
                    _ows.Cells[rowCnt, 3] = ConvertEngine.GetRightDateString(patientList[i].Birthday, false);
                    
                    _ows.Cells[rowCnt, 4] = patientList[i].GetAddress();
                    _ows.Cells[rowCnt, 5] = patientList[i].WorkPlace;
                    _ows.Cells[rowCnt, 6] = patientList[i].Phone;

                    _ows.Cells[rowCnt, 7] = patientList[i].TypeOfKSG;
                    _ows.Cells[rowCnt, 8] = patientList[i].MKB;
                    _ows.Cells[rowCnt, 9] = patientList[i].ServiceCode;
                    _ows.Cells[rowCnt, 10] = patientList[i].ServiceName;                    

                    _ows.Cells[rowCnt, 11] = patientList[i].Nosology;
                    _ows.Cells[rowCnt, 12] = patientList[i].DoctorInChargeOfTheCase;
                    _ows.Cells[rowCnt, 13] = ConvertEngine.GetRightDateString(patientList[i].DeliveryDate, true);
                    if (patientList[i].ReleaseDate.HasValue)
                    {
                        _ows.Cells[rowCnt, 14] = ConvertEngine.GetRightDateString(patientList[i].ReleaseDate.Value, true);
                    }

                    _ows.Cells[rowCnt, 15] = patientList[i].NumberOfCaseHistory;
                    _ows.Cells[rowCnt, 16] = patientList[i].Operations.Count.ToString();
                    _ows.Cells[rowCnt, 17] = patientList[i].Diagnose;

                    if (patientList[i].Operations.Count == 0)
                    {
                        rowCnt++;
                    }
                    else
                    {
                        foreach (OperationClass operationInfo in patientList[i].Operations)
                        {
                            _ows.Cells[rowCnt, 18] = operationInfo.Name;
                            _ows.Cells[rowCnt, 19] = ConvertEngine.GetRightDateString(operationInfo.DataOfOperation);
                            _ows.Cells[rowCnt, 20] = ConvertEngine.GetRightTimeString(operationInfo.StartTimeOfOperation);
                            _ows.Cells[rowCnt, 21] = ConvertEngine.GetRightTimeString(operationInfo.EndTimeOfOperation);
                            _ows.Cells[rowCnt, 22] = ListToString(operationInfo.Surgeons);
                            _ows.Cells[rowCnt, 23] = ListToString(operationInfo.Assistents);
                            _ows.Cells[rowCnt, 24] = operationInfo.HeAnaesthetist;
                            _ows.Cells[rowCnt, 25] = operationInfo.SheAnaesthetist;
                            _ows.Cells[rowCnt, 26] = operationInfo.ScrubNurse;
                            _ows.Cells[rowCnt, 27] = operationInfo.Orderly;
                            _ows.Cells[rowCnt, 28] = operationInfo.OperationCourse;

                            rowCnt++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_oxl != null)
                {
                    _oxl.Visible = true;
                    _oxl.UserControl = true;

                    if (_oxb != null)
                    {
                        Marshal.ReleaseComObject(_oxb);
                        _oxb = null;
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

        private static string ListToString(IEnumerable<string> list)
        {
            var listStr = new StringBuilder();
            foreach (string str in list)
            {
                listStr.Append(str + "; ");
            }

            if (listStr.Length > 2)
            {
                listStr.Remove(listStr.Length - 2, 2);
            }

            return listStr.ToString();
        }
    }
}
