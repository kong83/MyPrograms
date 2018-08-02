using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace DatabaseWorker
{
    public class ExcelExporter
    {
        private Excel.Application _application;
        private Excel._Workbook _workbook;
        private Excel._Worksheet _worksheet;
        private Excel.Range _range;

        public void Export(ExportedData[] exportedDataList)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                // Стартуем Excel-приложение
                _application = new Excel.Application();
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                // Создаем новую книгу
                _workbook = _application.Workbooks.Add(Missing.Value);

                _worksheet = (Excel._Worksheet)_workbook.Sheets[3];
                _worksheet.Delete();
                _worksheet = (Excel._Worksheet)_workbook.Sheets[2];
                _worksheet.Delete();

                _worksheet = (Excel._Worksheet)_workbook.Sheets[1];

                _worksheet.Cells.WrapText = true;
                _worksheet.Cells.VerticalAlignment = 2;
                _worksheet.Cells.HorizontalAlignment = 3;

                _range = _worksheet.Range["A1", "D1"];                
                _range.Font.Bold = true;

                _range = _worksheet.Range["A1", "A1"];
                _range.Font.Size = 14;
                _range.RowHeight = 30;
                _range.ColumnWidth = 90;
                _range.Value2 = "Название";

                _range = _worksheet.Range["B1", "C1"];
                _range.Font.Size = 12;
                _range.ColumnWidth = 7;                
                _worksheet.Cells[1, 2] = "Время в сек";
                _worksheet.Cells[1, 3] = "Время в мин";

                _range = _worksheet.Range["D1", "D1"];
                _range.Font.Size = 12;
                _range.ColumnWidth = 14;
                _range.Value2 = "% от общего выполнения";

                _range = _worksheet.Range["A2", "A" + (exportedDataList.Length + 1)];
                _range.HorizontalAlignment = 2;

                for (int i = 0; i < exportedDataList.Length; i++)                
                {
                    _worksheet.Cells[i + 2, 1] = exportedDataList[i].Name;
                    _worksheet.Cells[i + 2, 2] = exportedDataList[i].TimeInSec;
                    _worksheet.Cells[i + 2, 3] = exportedDataList[i].TimeInMin;
                    _worksheet.Cells[i + 2, 4] = exportedDataList[i].Percent;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_application != null)
                {
                    _application.Visible = true;
                    _application.UserControl = true;

                    if (_workbook != null)
                    {
                        Marshal.ReleaseComObject(_workbook);
                        _workbook = null;
                    }
                    if (_worksheet != null)
                    {
                        Marshal.ReleaseComObject(_worksheet);
                        _worksheet = null;
                    }
                    if (_range != null)
                    {
                        Marshal.ReleaseComObject(_range);
                        _range = null;
                    }

                    Marshal.ReleaseComObject(_application);
                    _application = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    Thread.CurrentThread.CurrentCulture = oldCi;
                }
            }
        }
    }
}
