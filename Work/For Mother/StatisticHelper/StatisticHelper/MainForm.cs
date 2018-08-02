using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Threading;

namespace StatisticHelper
{
    public partial class MainForm : Form
    {
        private Excel.Application m_OXl;
        private Excel._Workbook m_OWBSvod;        
        private Excel._Workbook m_OWBSvod100;
        private Excel._Workbook m_OWBSvodVUT;
        private Logger m_Logger;
        private bool m_NeedStop;
        private bool m_StopChangeParameters = true;

        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Инициализация логгера и прописывание параметров из конфига
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            m_Logger = new Logger(Path.Combine(
                Application.StartupPath,
                DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss")));

            LoadParameters();
            m_StopChangeParameters = false;
        }


        /// <summary>
        /// Сохранить параметры в конфиге
        /// </summary>
        private void LoadParameters()
        {
            m_Logger.WriteLog("Run LoadParameters");

            m_StopChangeParameters = true;

            textBoxSvodFilePath.Text = Configuration.Default.SvodFilePath;
            textBoxSvod100FilePath.Text = Configuration.Default.Svod100FilePath;
            textBoxSvodVUTFilePath.Text = Configuration.Default.SvodVUTFilePath;
            textBoxHospitalFilesFolder.Text = Configuration.Default.HospitalFilesFolder;
            comboBoxFirstMonth.Text = Configuration.Default.FirstMonth;
            comboBoxSecondMonth.Text = Configuration.Default.SecondMonth;

            m_StopChangeParameters = false;

            m_Logger.WriteLog(
                "textBoxHospitalFilesFolder.Text = {0}\r\n" +
                "textBoxSvod100FilePath.Text = {1}\r\n" +
                "textBoxSvodVUTFilePath.Text = {2}\r\n" +
                "textBoxHospitalFilesFolder.Text = {3}\r\n" +
                "comboBoxFirstMonth.Text = {4}\r\n" +
                "comboBoxSecondMonth.Text = {5}\r\n",
                textBoxHospitalFilesFolder.Text,
                textBoxSvod100FilePath.Text,
                textBoxSvodVUTFilePath.Text,
                textBoxHospitalFilesFolder.Text,
                comboBoxFirstMonth.Text,
                comboBoxSecondMonth.Text);
            m_Logger.WriteLog("Exit LoadParameters");
        }


        /// <summary>
        /// Сохранить параметры в конфиге
        /// </summary>
        private void SaveParameters()
        {
            if (m_StopChangeParameters)
                return;

            m_Logger.WriteLog("Run SaveParameters");

            Configuration.Default.SvodFilePath = textBoxSvodFilePath.Text;
            Configuration.Default.Svod100FilePath = textBoxSvod100FilePath.Text;
            Configuration.Default.SvodVUTFilePath = textBoxSvodVUTFilePath.Text;
            Configuration.Default.HospitalFilesFolder = textBoxHospitalFilesFolder.Text;
            Configuration.Default.FirstMonth = comboBoxFirstMonth.Text;
            Configuration.Default.SecondMonth = comboBoxSecondMonth.Text;
            Configuration.Default.Save();

            m_Logger.WriteLog(
               "Configuration.Default.SvodFilePath = {0}\r\n" +
               "Configuration.Default.Svod100FilePath = {1}\r\n" +
               "Configuration.Default.SvodVUTFilePath = {2}\r\n" +
               "Configuration.Default.HospitalFilesFolder = {3}\r\n" +
               "Configuration.Default.FirstMonth = {4}\r\n" +
               "Configuration.Default.SecondMonth = {5}\r\n",
               Configuration.Default.SvodFilePath,
               Configuration.Default.Svod100FilePath,
               Configuration.Default.SvodVUTFilePath,
               Configuration.Default.HospitalFilesFolder,
               Configuration.Default.FirstMonth,
               Configuration.Default.SecondMonth);
            m_Logger.WriteLog("Exit SaveParameters");
        }


        /// <summary>
        /// Перевести название месяца в число
        /// </summary>
        /// <param name="monthStr"></param>
        /// <returns></returns>
        private static int ConvertMonthToNumber(string monthStr)
        {
            switch (monthStr)
            {
                case "Январь":
                    return 1;
                case "Февраль":
                    return 2;
                case "Март":
                    return 3;
                case "Апрель":
                    return 4;
                case "Май":
                    return 5;
                case "Июнь":
                    return 6;
                case "Июль":
                    return 7;
                case "Август":
                    return 8;
                case "Сентябрь":
                    return 9;
                case "Октябрь":
                    return 10;
                case "Ноябрь":
                    return 11;
                case "Декабрь":
                    return 12;
                default:
                    throw new Exception("Незвестный месяц: " + monthStr + ".");
            }
        }


        /// <summary>
        /// Преобразует число х меньше 10 в строку 0х. Число не меньше 10 осталяет неизменным 
        /// </summary>
        /// <param name="sheetNumber"></param>
        /// <param name="fromSvod100"></param>
        /// <returns></returns>
        private static string GetSheetName(int sheetNumber, bool fromSvod100)
        {
            if (!fromSvod100)
            {
                if (sheetNumber < 10)
                {
                    return "0" + sheetNumber;
                }

                return sheetNumber.ToString();
            }

            if (sheetNumber == 1)
            {
                return "0" + sheetNumber;
            }
            if (sheetNumber == 12)
            {
                return "год";
            }
            return sheetNumber + "м";
        }


        /// <summary>
        /// Проверить, что данные введены верно
        /// </summary>
        private void CheckData()
        {
            m_Logger.WriteLog("Run CheckData");

            if (!File.Exists(textBoxSvodFilePath.Text))
            {
                throw new Exception("Путь до файла с общим сводом не найден");
            }

            if (!File.Exists(textBoxSvod100FilePath.Text))
            {
                throw new Exception("Путь до файла со сводом на 100 человек не найден");
            }

            if (!File.Exists(textBoxSvodVUTFilePath.Text))
            {
                throw new Exception("Путь до файла со сводом ВУТ не найден");
            }

            if (!Directory.Exists(textBoxHospitalFilesFolder.Text))
            {
                throw new Exception("Путь до папки, содержащей информацию по больницам не найден");
            }

            int firstMonth = ConvertMonthToNumber(comboBoxFirstMonth.Text);
            int secondMonth = ConvertMonthToNumber(comboBoxSecondMonth.Text);

            if (firstMonth > secondMonth)
            {
                throw new Exception("Первый месяц больше второго");
            }

            m_Logger.WriteLog(
                "textBoxHospitalFilesFolder.Text = {0}\r\n" +
                "textBoxSvod100FilePath.Text = {1}\r\n" +
                "textBoxSvodVUTFilePath.Text = {2}\r\n" +
                "textBoxHospitalFilesFolder.Text = {3}\r\n" +
                "comboBoxFirstMonth.Text = {4}\r\n" +
                "comboBoxSecondMonth.Text = {5}\r\n" +
                "firstMonth = {6}\r\n" +
                "secondMonth = {7}\r\n",
                textBoxHospitalFilesFolder.Text,
                textBoxSvod100FilePath.Text,
                textBoxSvodVUTFilePath.Text,
                textBoxHospitalFilesFolder.Text,
                comboBoxFirstMonth.Text,
                comboBoxSecondMonth.Text,
                firstMonth.ToString(),
                secondMonth.ToString());

            m_Logger.WriteLog("Exit CheckData");
        }


        /// <summary>
        /// Ищем файлы, у которых стоит от 1 до 5 пробелов перед ВУТ.xml
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="hospitalName"></param>
        /// <returns></returns>
        private static string FindHospitalFile(string folderPath, string hospitalName)
        {
            var di = new DirectoryInfo(folderPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Name.StartsWith(hospitalName) && fi.Name.Contains("ВУТ") && fi.Extension == ".xls")
                {
                    return fi.FullName;
                }
            }

            throw new Exception("Файл для больницы \"" + hospitalName + "\" не найден");
        }


        /// <summary>
        /// Приводит, если можно, value к числу типа double. Если value пустое - то к 0
        /// </summary>
        /// <param name="value">Кандидат на число типа double</param>
        /// <param name="doubleValue">Число типа double, если можно привести</param>
        /// <returns></returns>
        private static bool DoubleTryParse(object value, out double doubleValue)
        {
            doubleValue = 0;
            if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
            {                
                return true;
            }

            if (double.TryParse(value.ToString(), out doubleValue))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Начать выполнение программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartWork_Click(object sender, EventArgs e)
        {
            CultureInfo oldCi = null;

            if (checkBoxCulture.Checked)
            {
                oldCi = Thread.CurrentThread.CurrentCulture;
            }
            try
            {
                m_Logger.WriteLog("Запущено выполнение программы");
                
                Height += 60;
                m_NeedStop = false;
                buttonStartWork.Visible = false;
                buttonStopWork.Visible = true;
                buttonAbout.Enabled = buttonExit.Enabled = false;
                progressBarWorkInfo.Value = 0;
                Application.DoEvents();

                CheckData();

                //
                // Инициализировать введённые значения
                //
                string svodFilePath = textBoxSvodFilePath.Text;
                string svod100FilePath = textBoxSvod100FilePath.Text;
                string svodVUTFilePath = textBoxSvodVUTFilePath.Text;
                string hospitalFilesFolder = textBoxHospitalFilesFolder.Text;
                int firstMonth = ConvertMonthToNumber(comboBoxFirstMonth.Text);
                int secondMonth = ConvertMonthToNumber(comboBoxSecondMonth.Text);

                labelCurrentAction.Text = "Запускаем Excel приложение";
                Application.DoEvents();

                m_Logger.WriteLog(labelCurrentAction.Text);
                m_Logger.WriteLog("1");/////////////////////////////////////////////////////
                if (checkBoxCulture.Checked)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                }

                m_Logger.WriteLog("2");/////////////////////////////////////////////////////
                // Стартуем Excel-приложение
                m_OXl = new Excel.Application
                {
                    DisplayAlerts = false
                };
                m_Logger.WriteLog("Стартовали Excel-приложение");
                
                labelCurrentAction.Text = "Открывается файл \"" + Path.GetFileName(svodFilePath) + "\"";
                Application.DoEvents();
                m_Logger.WriteLog("3 " + labelCurrentAction.Text);/////////////////////////////////////////////////////
                // Открываем файл с общим сводом
                m_OWBSvod = m_OXl.Workbooks.Open(svodFilePath, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                m_Logger.WriteLog("Открыли файл с общим сводом");

                // Определяем количество колонок в файле по первому листу
                var oWS = (Excel._Worksheet)m_OWBSvod.Sheets["01"];
                m_Logger.WriteLog("4");/////////////////////////////////////////////////////
                // Находим, где начинается список больниц
                int strWithNumbers = 1;
                int temp;

                while (strWithNumbers < 1000 && (((Excel.Range)oWS.Cells[strWithNumbers, 1]).Value2 == null ||
                    !int.TryParse(((Excel.Range)oWS.Cells[strWithNumbers, 1]).Value2.ToString(), out temp) ||
                    temp != 1))
                {                    
                    strWithNumbers++;
                    m_Logger.WriteLog("5 " + strWithNumbers);/////////////////////////////////////////////////////
                }
                m_Logger.WriteLog("6");/////////////////////////////////////////////////////
                if (strWithNumbers == 1000)
                {
                    throw new Exception("Ошибка в структуре файла " + svodFilePath + ". Не удалось определить строку с номерами");
                }

                int cntColumns = 1;
                while (cntColumns < 1000 && ((Excel.Range)oWS.Cells[strWithNumbers, cntColumns]).Value2 != null)
                {
                    m_Logger.WriteLog("7 " + cntColumns);/////////////////////////////////////////////////////
                    cntColumns++;
                }
                m_Logger.WriteLog("8");/////////////////////////////////////////////////////
                if (cntColumns == 1000)
                {
                    throw new Exception("Ошибка в структуре файла " + svodFilePath + ". Не удалось определить количество стоблцов");
                }
                cntColumns--;
                m_Logger.WriteLog("Строка с номерами: " + strWithNumbers);
                m_Logger.WriteLog("Последний столбец: " + cntColumns);
                

                int hospitalStr = strWithNumbers;
                var hospitalsString = new StringBuilder("Список больниц:\r\n");
                m_Logger.WriteLog("9");/////////////////////////////////////////////////////
                // Определяем количество больниц, которые надо обработать
                while (((Excel.Range)oWS.Cells[++hospitalStr, 1]).Value2 != null &&
                    ((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString() != "СЖД")
                {
                    m_Logger.WriteLog("10 " + hospitalsString);/////////////////////////////////////////////////////
                    hospitalsString.Append(((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString() + " ");
                }
                m_Logger.WriteLog("11");/////////////////////////////////////////////////////
                m_Logger.WriteLog(hospitalsString.ToString());

                m_Logger.WriteLog("hospitalStr=" + hospitalStr);

                double shag = 100.0 / (hospitalStr - strWithNumbers);
                double progress = 0;
                
                // Открываем файл со сводом ВУТ
                labelCurrentAction.Text = "Открывается файл \"" + Path.GetFileName(svodVUTFilePath) + "\"";
                Application.DoEvents();
                m_Logger.WriteLog("12 " + labelCurrentAction.Text);/////////////////////////////////////////////////////
                m_OWBSvodVUT = m_OXl.Workbooks.Open(svodVUTFilePath, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                m_Logger.WriteLog("Открыли файл со сводом ВУТ");

                // Проходим по файлу со сводом ВУТ и очищаем значения
                for (int sheetNumber = firstMonth; sheetNumber <= secondMonth; sheetNumber++)
                {
                    m_Logger.WriteLog("13 " + sheetNumber);/////////////////////////////////////////////////////
                    string sheetNumberName = GetSheetName(sheetNumber, false);

                    labelCurrentAction.Text = "Очищаем данные в файле \"" + Path.GetFileName(svodVUTFilePath) + "\". Лист №" + sheetNumberName;
                    Application.DoEvents();

                    // Выбираем лист в своде ВУТ
                    var oWSSvodVUT = (Excel._Worksheet)m_OWBSvodVUT.Sheets[sheetNumberName];
                    m_Logger.WriteLog("14");/////////////////////////////////////////////////////
                    // Выбираем все нужные значения из файла с данными по больнице
                    var oRangeSvodVUT = oWSSvodVUT.get_Range(oWSSvodVUT.Cells[strWithNumbers + 1, 2], oWSSvodVUT.Cells[strWithNumbers + 12, cntColumns]);
                    m_Logger.WriteLog("15 ");/////////////////////////////////////////////////////
                    // Преобразуем данные в массив формул
                    var arrSvodVUT = (object[,])oRangeSvodVUT.Value2;
                    m_Logger.WriteLog("16");/////////////////////////////////////////////////////
                    for (int i = 1; i < 12; i++)
                    {
                        m_Logger.WriteLog("17 " + i);/////////////////////////////////////////////////////
                        for (int j = 1; j < cntColumns; j++)
                        {                            
                            arrSvodVUT[i, j] = null;                         
                        }
                    }
                    m_Logger.WriteLog("18");/////////////////////////////////////////////////////
                    oRangeSvodVUT.Orientation = 0;
                    oRangeSvodVUT.Font.Size = 10;
                    oRangeSvodVUT.Value2 = arrSvodVUT;
                    m_Logger.WriteLog("19");/////////////////////////////////////////////////////
                }

                m_Logger.WriteLog("20");/////////////////////////////////////////////////////

                hospitalStr = strWithNumbers;
                // Проходим по всем больницам и заполняем пустые значения
                while (((Excel.Range)oWS.Cells[++hospitalStr, 1]).Value2 != null &&
                    ((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString() != "СЖД")
                {
                    m_Logger.WriteLog("21");/////////////////////////////////////////////////////
                    string hospitalFileName = FindHospitalFile(hospitalFilesFolder, ((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString().Trim(' '));
                    m_Logger.WriteLog("22");/////////////////////////////////////////////////////

                    labelCurrentAction.Text = "Обрабатывается больница \"" + ((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString().Trim(' ') + "\"";
                    Application.DoEvents();

                    if (m_NeedStop)
                    {
                        m_Logger.WriteLog("Exit by m_NeedStop");
                        return;
                    }

                    m_Logger.WriteLog(labelCurrentAction.Text);

                    m_Logger.WriteLog("23");/////////////////////////////////////////////////////

                    // Открываем книгу c информацией по нужной больнице
                    var oWBHospital = (Excel._Workbook)(m_OXl.Workbooks.Open(hospitalFileName, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                    m_Logger.WriteLog("Открыли файл\r\n\"{0}\"\r\nс больницей", hospitalFileName);                    

                    // Проходим по всем нужным листам
                    for (int sheetNumber = firstMonth; sheetNumber <= secondMonth; sheetNumber++)
                    {
                        m_Logger.WriteLog("25");/////////////////////////////////////////////////////
                        string sheetNumberName = GetSheetName(sheetNumber, false);

                        labelCurrentAction.Text = "Обрабатывается больница \"" + ((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString().Trim(' ') + "\". Лист №" + sheetNumber;
                        Application.DoEvents();

                        m_Logger.WriteLog(labelCurrentAction.Text);

                        // Выбираем лист в общем своде
                        var oWSSvod = (Excel._Worksheet)m_OWBSvod.Sheets[sheetNumberName];

                        // Выбираем все нужные значения из файла с общим сводом
                        var oRangeSvod = oWSSvod.get_Range(oWSSvod.Cells[hospitalStr, 2], oWSSvod.Cells[hospitalStr, cntColumns]);

                        // Преобразуем данные в массив формул
                        var arrSvod = (object[,])oRangeSvod.Formula;

                        // Выбираем соответствующий лист в файле из больницы
                        var oWSHospital = (Excel._Worksheet)oWBHospital.Sheets[sheetNumberName];

                        // Выбираем все нужные значения из файла с данными по больнице
                        var oRange = oWSHospital.get_Range(oWSHospital.Cells[strWithNumbers + 1, 2], oWSHospital.Cells[strWithNumbers + 1, cntColumns]);

                        // Преобразуем данные в массив значений
                        var arr = (object[,])oRange.Value2;

                        m_Logger.WriteLog("26");/////////////////////////////////////////////////////

                        // Проходим и заполняем наш массив из свода
                        var sbCheckHospital = new StringBuilder();
                        var sbCheckSvod = new StringBuilder();
                        for (int i = 1; i <= arrSvod.Length; i++)
                        {
                            if (!arrSvod[1, i].ToString().StartsWith("="))
                            {
                                arrSvod[1, i] = arr[1, i];
                                sbCheckHospital.AppendFormat("{0}={1};", i, arr[1, i]);
                            }
                            else
                            {
                                sbCheckHospital.AppendFormat("{0}=miss;", i);
                            }

                            sbCheckSvod.AppendFormat("{0}={1};", i, arrSvod[1, i]);
                        }

                        m_Logger.WriteLog("Hospital values = " + sbCheckHospital.ToString());
                        m_Logger.WriteLog("Svod values = " + sbCheckSvod.ToString());

                        // Записываем данные обратно в файл с общим сводом
                        oRangeSvod.Orientation = 0;
                        oRangeSvod.Font.Size = 10;
                        oRangeSvod.Formula = arrSvod;

                        m_Logger.WriteLog("27");/////////////////////////////////////////////////////

                        // Считаем данные для файла "Свод ВУТ"                        
                        // Выбираем лист в своде ВУТ
                        var oWSSvodVUT = (Excel._Worksheet)m_OWBSvodVUT.Sheets[sheetNumberName];

                        // Выбираем все нужные значения из файла с данными по больнице
                        var oRangeSvodVUT = oWSSvodVUT.get_Range(oWSSvodVUT.Cells[strWithNumbers + 1, 2], oWSSvodVUT.Cells[strWithNumbers + 12, cntColumns]);

                        // Преобразуем данные в массив формул
                        var arrSvodVUT = (object[,])oRangeSvodVUT.Value2;

                        // Выбираем все нужные значения из файла с данными по больнице
                        oRange = oWSHospital.get_Range(oWSHospital.Cells[strWithNumbers + 1, 2], oWSHospital.Cells[strWithNumbers + 12, cntColumns]);

                        // Преобразуем данные в массив значений
                        arr = (object[,])oRange.Value2;

                        m_Logger.WriteLog("28");/////////////////////////////////////////////////////

                        // Проходим и заполняем наш массив из свода ВУТ                        
                        for (int i = 1; i < 12; i++)
                        {
                            for (int j = 1; j < cntColumns; j++)
                            {
                                if (j != 6 && j != 17)
                                {
                                    double vutValue, hospitalValue;
                                    if (DoubleTryParse(arrSvodVUT[i, j], out vutValue) &&
                                        DoubleTryParse(arr[i, j], out hospitalValue))
                                    {
                                        if (vutValue > -2000000 && vutValue < 2000000 &&
                                            hospitalValue > -2000000 && hospitalValue < 2000000)
                                        {
                                            arrSvodVUT[i, j] = vutValue + hospitalValue;
                                        }
                                    }
                                }
                                else
                                {
                                    if ((double)arrSvodVUT[i, 1] != 0)
                                    {
                                        if (j == 6)
                                        {
                                            arrSvodVUT[i, j] = "=F" + (strWithNumbers + i) + "/B" + (strWithNumbers + i) + "*100";
                                        }
                                        else
                                        {
                                            arrSvodVUT[i, j] = "=Q" + (strWithNumbers + i) + "/B" + (strWithNumbers + i) + "*100";
                                        }
                                    }
                                }
                            }
                        }

                        m_Logger.WriteLog("29");/////////////////////////////////////////////////////

                        // Записываем данные обратно в файл со сводом ВУТ                        
                        oRangeSvodVUT.Value2 = arrSvodVUT;
                    }

                    m_Logger.WriteLog("30");/////////////////////////////////////////////////////

                    progress += shag;
                    progressBarWorkInfo.Value = Math.Min((int)progress, 100);
                    Application.DoEvents();
                    oWBHospital.Close(false, Missing.Value, Missing.Value);
                }

                m_Logger.WriteLog("31");/////////////////////////////////////////////////////

                //
                // Заполнение файла свода на 100 работающих
                //
                labelCurrentAction.Text = "Обрабатываем файл \"" + Path.GetFileName(svod100FilePath) + "\"";
                Application.DoEvents();

                if (m_NeedStop)
                {
                    m_Logger.WriteLog("Exit by m_NeedStop");
                    return;
                }

                m_Logger.WriteLog(labelCurrentAction.Text);

                // Ищем, где заканчивается все данные (и по разным ТЧ) в файле с общим сводом
                hospitalsString = new StringBuilder("Список ТЧ:\r\n");
                while (((Excel.Range)oWS.Cells[++hospitalStr, 1]).Value2 == null ||
                    (((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2 != null &&
                    ((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString() != "СЖД"))
                {
                    if (((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2 != null)
                        hospitalsString.Append(((Excel.Range)oWS.Cells[hospitalStr, 1]).Value2.ToString() + " ");
                }

                m_Logger.WriteLog(hospitalsString.ToString());
                m_Logger.WriteLog("hospitalStr=" + hospitalStr);

                // Открываем книгу с данными на 100 работающих
                m_OWBSvod100 = m_OXl.Workbooks.Open(svod100FilePath, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                m_Logger.WriteLog("Открыли файл\r\n\"{0}\"\r\n с данными на 100 работающих", svod100FilePath);

                var oWS100 = (Excel._Worksheet)m_OWBSvod100.Sheets["01"];

                // Определяем номера, с которых начинаются и закончиваются блоки с данными
                // по всем, по машинистам и по помощникам
                const int allStart = 4;
                int allEnd = 4;
                temp = 0;
                while (temp < 2)
                {
                    allEnd++;
                    if (((Excel.Range)oWS100.Cells[allEnd, 1]).Value2 != null &&
                    ((Excel.Range)oWS100.Cells[allEnd, 1]).Value2.ToString() == "СЖД")
                    {
                        temp++;
                    }
                }

                int mashEnd = allEnd + 1;
                while (((Excel.Range)oWS100.Cells[++mashEnd, 1]).Value2 == null ||
                    (((Excel.Range)oWS100.Cells[mashEnd, 1]).Value2 != null &&
                   ((Excel.Range)oWS100.Cells[mashEnd, 1]).Value2.ToString() != "СЖД"))
                {
                }

                int mashStart = mashEnd;
                while (((Excel.Range)oWS100.Cells[--mashStart, 1]).Value2 != null)
                {
                }
                mashStart++;
                while (((Excel.Range)oWS100.Cells[++mashEnd, 1]).Value2 == null ||
                    (((Excel.Range)oWS100.Cells[mashEnd, 1]).Value2 != null &&
                   ((Excel.Range)oWS100.Cells[mashEnd, 1]).Value2.ToString() != "СЖД"))
                {
                }

                int pomEnd = mashEnd + 1;
                while (((Excel.Range)oWS100.Cells[++pomEnd, 1]).Value2 == null ||
                    (((Excel.Range)oWS100.Cells[pomEnd, 1]).Value2 != null &&
                  ((Excel.Range)oWS100.Cells[pomEnd, 1]).Value2.ToString() != "СЖД"))
                {
                }

                int pomStart = pomEnd;
                while (((Excel.Range)oWS100.Cells[--pomStart, 1]).Value2 != null)
                {
                }
                pomStart++;
                while (((Excel.Range)oWS100.Cells[++pomEnd, 1]).Value2 == null ||
                    (((Excel.Range)oWS100.Cells[pomEnd, 1]).Value2 != null &&
                   ((Excel.Range)oWS100.Cells[pomEnd, 1]).Value2.ToString() != "СЖД"))
                {
                }
                m_Logger.WriteLog(
                    "allStart={0}\r\n" +
                    "allEnd={1}\r\n" +
                    "mashStart={2}\r\n" +
                    "mashEnd={3}\r\n" +
                    "pomStart={4}\r\n" +
                    "pomEnd={5}\r\n",
                    allStart.ToString(), allEnd.ToString(), mashStart.ToString(),
                    mashEnd.ToString(), pomStart.ToString(), pomEnd.ToString());

                // Проходим по всем нужным листам
                for (int sheetNumber = firstMonth; sheetNumber <= secondMonth; sheetNumber++)
                {
                    labelCurrentAction.Text = "Обрабатываем файл \"" + Path.GetFileName(svod100FilePath) + "\". Лист №" + sheetNumber;
                    Application.DoEvents();

                    if (m_NeedStop)
                    {
                        m_Logger.WriteLog("Exit by m_NeedStop");
                        return;
                    }

                    m_Logger.WriteLog(labelCurrentAction.Text);

                    string sheetNumberName = GetSheetName(sheetNumber, true);

                    // Выбираем лист в общем своде
                    var oWSSvod = (Excel._Worksheet)m_OWBSvod.Sheets[sheetNumberName];

                    // Выбираем соответствующий лист в своде на 100 работающих
                    var oWSSvod100 = (Excel._Worksheet)m_OWBSvod100.Sheets[sheetNumberName];

                    // Считаем данные для всех
                    var oRangeSvod = oWSSvod.get_Range(oWSSvod.Cells[strWithNumbers + 1, 1/*A*/], oWSSvod.Cells[hospitalStr, 111/*DG*/]);
                    var arrSvod = (object[,])oRangeSvod.Value2;
                    var oRangeSvod100 = oWSSvod100.get_Range(oWSSvod100.Cells[allStart, 1/*A*/], oWSSvod100.Cells[allEnd, 42/*AO*/]);
                    var arrSvod100 = (object[,])oRangeSvod100.Value2;

                    int numSvod100 = 1;
                    for (int numSvod = 1; numSvod <= hospitalStr - strWithNumbers; numSvod++, numSvod100++)
                    {
                        if (arrSvod100[numSvod100, 1] != null)
                        {
                            int colSvod = 36;
                            int colSvod100 = 4;
                            while (colSvod100 < 41)
                            {
                                arrSvod100[numSvod100, colSvod100] = (Convert.ToDouble(arrSvod[numSvod, colSvod]) + Convert.ToDouble(arrSvod[numSvod, colSvod + 2])) / Convert.ToDouble(arrSvod[numSvod, 2 /*B*/]) * 100;
                                arrSvod100[numSvod100, colSvod100 + 1] = (Convert.ToDouble(arrSvod[numSvod, colSvod + 1]) + Convert.ToDouble(arrSvod[numSvod, colSvod + 3])) / Convert.ToDouble(arrSvod[numSvod, 2/*B*/]) * 100;
                                colSvod100 += 2;
                                colSvod += 4;
                            }
                            // Считаем данные по общей заболеваемости для машинистов 
                            arrSvod100[numSvod100, 2/*B*/] = Convert.ToDouble(arrSvod[numSvod, 6/*F*/]) / Convert.ToDouble(arrSvod[numSvod, 2/*B*/]) * 100;
                            arrSvod100[numSvod100, 3/*C*/] = Convert.ToDouble(arrSvod[numSvod, 17/*Q*/]) / Convert.ToDouble(arrSvod[numSvod, 2/*B*/]) * 100;
                        }
                    }
                    oRangeSvod100.Orientation = 0;
                    oRangeSvod100.Font.Size = 10;
                    oRangeSvod100.Value2 = arrSvod100;


                    // Считаем данные для машинистов                    
                    oRangeSvod100 = oWSSvod100.get_Range(oWSSvod100.Cells[mashStart, 1/*A*/], oWSSvod100.Cells[mashEnd, 42/*AO*/]);
                    arrSvod100 = (object[,])oRangeSvod100.Value2;

                    numSvod100 = 1;
                    for (int numSvod = 1; numSvod <= hospitalStr - strWithNumbers; numSvod++, numSvod100++)
                    {
                        if (arrSvod100[numSvod100, 1] != null)
                        {
                            int colSvod = 36;
                            int colSvod100 = 4;
                            while (colSvod100 < 41)
                            {
                                arrSvod100[numSvod100, colSvod100] = Convert.ToDouble(arrSvod[numSvod, colSvod]) / Convert.ToDouble(arrSvod[numSvod, 3/*C*/]) * 100;
                                arrSvod100[numSvod100, colSvod100 + 1] = Convert.ToDouble(arrSvod[numSvod, colSvod + 1]) / Convert.ToDouble(arrSvod[numSvod, 3/*C*/]) * 100;
                                colSvod100 += 2;
                                colSvod += 4;
                            }
                            // Считаем данные по общей заболеваемости для машинистов 
                            arrSvod100[numSvod100, 2/*B*/] = Convert.ToDouble(arrSvod[numSvod, 8/*H*/]) / Convert.ToDouble(arrSvod[numSvod, 3/*C*/]) * 100;
                            arrSvod100[numSvod100, 3/*C*/] = Convert.ToDouble(arrSvod[numSvod, 19/*S*/]) / Convert.ToDouble(arrSvod[numSvod, 3/*C*/]) * 100;
                        }
                    }
                    oRangeSvod100.Orientation = 0;
                    oRangeSvod100.Font.Size = 10;
                    oRangeSvod100.Value2 = arrSvod100;


                    // Считаем данные для помощников                    
                    oRangeSvod100 = oWSSvod100.get_Range(oWSSvod100.Cells[pomStart, 1/*A*/], oWSSvod100.Cells[pomEnd, 42/*AO*/]);
                    arrSvod100 = (object[,])oRangeSvod100.Value2;

                    numSvod100 = 1;
                    for (int numSvod = 1; numSvod <= hospitalStr - strWithNumbers; numSvod++, numSvod100++)
                    {
                        if (arrSvod100[numSvod100, 1] != null)
                        {
                            int colSvod = 36;
                            int colSvod100 = 4;
                            while (colSvod100 < 41)
                            {
                                arrSvod100[numSvod100, colSvod100] = Convert.ToDouble(arrSvod[numSvod, colSvod + 2]) / Convert.ToDouble(arrSvod[numSvod, 5/*E*/]) * 100;
                                arrSvod100[numSvod100, colSvod100 + 1] = Convert.ToDouble(arrSvod[numSvod, colSvod + 3]) / Convert.ToDouble(arrSvod[numSvod, 5/*E*/]) * 100;
                                colSvod100 += 2;
                                colSvod += 4;
                            }
                            // Считаем данные по общей заболеваемости для помощников 
                            arrSvod100[numSvod100, 2/*B*/] = Convert.ToDouble(arrSvod[numSvod, 10/*J*/]) / Convert.ToDouble(arrSvod[numSvod, 5/*E*/]) * 100;
                            arrSvod100[numSvod100, 3/*C*/] = Convert.ToDouble(arrSvod[numSvod, 21/*U*/]) / Convert.ToDouble(arrSvod[numSvod, 5/*E*/]) * 100;
                        }
                    }
                    oRangeSvod100.Orientation = 0;
                    oRangeSvod100.Font.Size = 10;
                    oRangeSvod100.Value2 = arrSvod100;
                }

                progressBarWorkInfo.Value = 100;
                Application.DoEvents();
                m_Logger.WriteLog("Успешно завершили работу");
            }
            catch (Exception ex)
            {
                m_Logger.WriteLog(TraceEventType.Error, ex.ToString());
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                labelCurrentAction.Text = "Сохраняем результат";
                Application.DoEvents();                

                try
                {
                    if (m_OWBSvod != null)
                    {
                        m_OWBSvod.Save();
                        m_Logger.WriteLog("Успешно cохранили общий свод");
                    }
                }
                catch (Exception ex)
                {
                    m_Logger.WriteLog(ex.ToString());
                    MessageBox.Show("Файл\r\n" + textBoxSvodFilePath.Text + "\r\n не сохранён, т.к. файл открыт только для чтения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                try
                {
                    if (m_OWBSvod100 != null)
                    {
                        m_OWBSvod100.Save();
                        m_Logger.WriteLog("Успешно cохранили свод на 100 работающих");
                    }
                }
                catch (Exception ex)
                {
                    m_Logger.WriteLog(ex.ToString());
                    MessageBox.Show("Файл\r\n" + textBoxSvod100FilePath.Text + "\r\n не сохранён, т.к. файл открыт только для чтения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                try
                {
                    if (m_OWBSvodVUT != null)
                    {
                        m_OWBSvodVUT.Save();
                        m_Logger.WriteLog("Успешно cохранили свод ВУТ");
                    }
                }
                catch (Exception ex)
                {
                    m_Logger.WriteLog(ex.ToString());
                    MessageBox.Show("Файл\r\n" + textBoxSvodVUTFilePath.Text + "\r\n не сохранён, т.к. файл открыт только для чтения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                m_NeedStop = false;
                buttonStartWork.Visible = true;
                buttonStopWork.Visible = false;
                buttonAbout.Enabled = buttonExit.Enabled = true;
                Height -= 60;
                Application.DoEvents();
                if (m_OXl != null)
                {
                    m_OXl.DisplayAlerts = true;
                    m_OXl.Visible = true;
                    m_OXl.UserControl = true;
                }

                if (checkBoxCulture.Checked)
                {
                    Thread.CurrentThread.CurrentCulture = oldCi;
                }

                m_Logger.WriteLog("Выполнение программы завершено");
            }
        }


        /// <summary>
        /// Выбрать путь до файла с общим сводом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectSvodFilePath_Click(object sender, EventArgs e)
        {            
            if (!string.IsNullOrEmpty(textBoxSvodFilePath.Text))
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxSvodFilePath.Text);    
            }

            openFileDialog1.FileName = Path.GetFileName(textBoxSvodFilePath.Text);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSvodFilePath.Text = openFileDialog1.FileName;
            }
        }


        /// <summary>
        /// Выбрать путь до файла со сводом на 100 человек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectSvod100FilePath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSvod100FilePath.Text))
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxSvod100FilePath.Text);
            }

            openFileDialog1.FileName = Path.GetFileName(textBoxSvod100FilePath.Text);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSvod100FilePath.Text = openFileDialog1.FileName;
            }
        }


        /// <summary>
        /// Выбрать путь до файла со сводом ВУТ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectSvodVUTFilePath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSvodVUTFilePath.Text))
            {
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(textBoxSvodVUTFilePath.Text);
            }

            openFileDialog1.FileName = Path.GetFileName(textBoxSvodVUTFilePath.Text);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSvodVUTFilePath.Text = openFileDialog1.FileName;
            }
        }


        /// <summary>
        /// Выбрать путь до папки с файлами из больниц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectHospitalFileFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxHospitalFilesFolder.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxHospitalFilesFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        /// <summary>
        /// Сохранение параметров при изменении значений полей для ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            SaveParameters();
        }


        /// <summary>
        /// Сохранение параметров при изменении значений комбо боксов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveParameters();
        }


        /// <summary>
        /// Кнопка выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Кнопка с информацие о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            new InfoForm().ShowDialog();
        }


        /// <summary>
        /// Остановить выполнение программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStopWork_Click(object sender, EventArgs e)
        {
            m_NeedStop = true;
        }

        #region Подсказки
        private void buttonStartWork_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Запустить выполнение программы", buttonStartWork, -10, -17);
            buttonStartWork.FlatStyle = FlatStyle.Popup;
        }

        private void buttonStartWork_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonStartWork);
            buttonStartWork.FlatStyle = FlatStyle.Flat;
        }

        private void buttonStopWork_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Остановить выполнение программы", buttonStopWork, -10, -17);
            buttonStopWork.FlatStyle = FlatStyle.Popup;
        }

        private void buttonStopWork_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonStopWork);
            buttonStopWork.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAbout_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Посмотреть информацию о программе", buttonAbout, -10, -17);
            buttonAbout.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAbout_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonAbout);
            buttonAbout.FlatStyle = FlatStyle.Flat;
        }

        private void buttonExit_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Выйти из программы", buttonExit, -10, -17);
            buttonExit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonExit_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonExit);
            buttonExit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonSelectSvodFilePath_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.FlatStyle = FlatStyle.Popup;
        }

        private void buttonSelectSvodFilePath_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
