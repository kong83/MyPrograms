using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using SurgeryHelper.Entities;
using Application = Microsoft.Office.Interop.Word.Application;
using Shape = Microsoft.Office.Interop.Word.Shape;

namespace SurgeryHelper.Engines
{
    public class WordExportEngine
    {
        private Application _wordApp;
        private Document _wordDoc;
        private Paragraph _paragraph;
        private Range _wordRange;
        private Table _wordTable;
        private Shape _wordShape;

        private object _missingObject = Type.Missing;

        private WaitForm _waitForm;
        private DbEngine _dbEngine;

        public WordExportEngine(DbEngine dbEngine)
        {
            _dbEngine = dbEngine;
        }

        /// <summary>
        /// Экспортировать в Word переводной эпикриз
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void ExportTransferableEpicrisis(PatientClass patientInfo)
        {
            _waitForm = new WaitForm();

            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);

                try
                {
                    // Пробуем для 2007 офиса выставить стиль документов 2003 офиса.
                    // Для других офисов, вероятно, отвалимся с ошибкой, но для них и не
                    // надо ничего делать.
                    _wordDoc.ApplyQuickStyleSet("Word 2003");
                }
                catch
                {
                }

                _waitForm.SetProgress(10);

                _wordDoc.PageSetup.TopMargin = 30;
                _wordDoc.PageSetup.LeftMargin = 50;
                _wordDoc.PageSetup.RightMargin = 30;
                _wordDoc.PageSetup.BottomMargin = 30;

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;
                _wordRange.Font.Name = "Times New Roman";

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _paragraph.Range.Text = "ПЕРЕВОДНОЙ ЭПИКРИЗ\r\n";

                _waitForm.SetProgress(20);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                _paragraph.Range.Text = string.Format(
                    "Пациент {0}, {1} {2}, находится на лечении в {3} х.о. с {4} с диагнозом: {5}\r\n",
                    patientInfo.GetFullName(),
                    patientInfo.Age,
                    ConvertEngine.GetAgeString(patientInfo.Age),
                    _dbEngine.GlobalSettings.DepartmentName,
                    ConvertEngine.GetRightDateString(patientInfo.DeliveryDate),
                    patientInfo.Diagnose);

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "Проведено лечение:";

                var textStr = new StringBuilder();
                foreach (OperationClass operationInfo in patientInfo.Operations)
                {
                    textStr.AppendFormat("{0} - {1}\r\n", ConvertEngine.GetRightDateString(operationInfo.DataOfOperation), operationInfo.Name);
                }

                if (textStr.Length > 2)
                {
                    textStr.Remove(textStr.Length - 2, 2);
                }

                _waitForm.SetProgress(40);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = textStr.ToString();

                if (!string.IsNullOrEmpty(patientInfo.TransferEpicrisAdditionalInfo))
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = patientInfo.TransferEpicrisAdditionalInfo + "\r\n";
                }

                _waitForm.SetProgress(50);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Послеоперационный период " + patientInfo.TransferEpicrisAfterOperationPeriod;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "Планируется:";

                _waitForm.SetProgress(60);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = patientInfo.TransferEpicrisPlan;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Для дальнейшего лечения в удовлетворительном состоянии переводится на дневной стационар.";

                _waitForm.SetProgress(70);

                if (patientInfo.TransferEpicrisIsIncludeDisabilityList)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Л/н № {0} продлен с {1} по {2}.\r\n",
                        patientInfo.TransferEpicrisDisabilityList,
                        ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate.AddDays(1)),
                        ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate.AddDays(10)));
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "С режимом ознакомлен _____________________________\r\n";

                _waitForm.SetProgress(80);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Дата " + ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Зав. отделением\t\t\t\t\t\t\t" + _dbEngine.GlobalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _waitForm.SetProgress(90);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Лечащий врач\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// Экспортировать в Word этапный эпикриз
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void ExportLineOfCommunicationEpicrisis(PatientClass patientInfo)
        {
            _waitForm = new WaitForm();

            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);

                try
                {
                    // Пробуем для 2007 офиса выставить стиль документов 2003 офиса.
                    // Для других офисов, вероятно, отвалимся с ошибкой, но для них и не
                    // надо ничего делать.
                    _wordDoc.ApplyQuickStyleSet("Word 2003");
                }
                catch
                {
                }

                _waitForm.SetProgress(10);

                _wordDoc.PageSetup.TopMargin = 30;
                _wordDoc.PageSetup.LeftMargin = 50;
                _wordDoc.PageSetup.RightMargin = 30;
                _wordDoc.PageSetup.BottomMargin = 30;

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;
                _wordRange.Font.Name = "Times New Roman";

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _paragraph.Range.Text = "ЭТАПНЫЙ ЭПИКРИЗ\r\n";

                _waitForm.SetProgress(20);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                _paragraph.Range.Text = string.Format(
                    "Пациент {0}, {1} {2}, находится на лечении в {3} х.о. с {4} с диагнозом: {5}\r\n",
                    patientInfo.GetFullName(),
                    patientInfo.Age,
                    ConvertEngine.GetAgeString(patientInfo.Age),
                    _dbEngine.GlobalSettings.DepartmentName,
                    ConvertEngine.GetRightDateString(patientInfo.DeliveryDate),
                    patientInfo.Diagnose);

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "Проведено лечение:";

                var textStr = new StringBuilder();
                foreach (OperationClass operationInfo in patientInfo.Operations)
                {
                    textStr.AppendFormat("{0} - {1}\r\n", ConvertEngine.GetRightDateString(operationInfo.DataOfOperation), operationInfo.Name);
                }

                _waitForm.SetProgress(40);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = textStr.ToString();

                if (!string.IsNullOrEmpty(patientInfo.LineOfCommEpicrisAdditionalInfo))
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = patientInfo.LineOfCommEpicrisAdditionalInfo + "\r\n";
                }

                _waitForm.SetProgress(50);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "Планируется:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = patientInfo.LineOfCommEpicrisPlan + "\r\n";

                _waitForm.SetProgress(60);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Дата " + ConvertEngine.GetRightDateString(patientInfo.LineOfCommEpicrisWritingDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _waitForm.SetProgress(80);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Зав. отделением\t\t\t\t\t\t\t" + _dbEngine.GlobalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _waitForm.SetProgress(90);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Лечащий врач\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// Экспортировать в Word выписной эпикриз
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        /// <param name="dischargeEpicrisisHeaderFilePath">Путь до файла с шапкой для выписного эпикриза</param>
        public void ExportDischargeEpicrisis(PatientClass patientInfo, object dischargeEpicrisisHeaderFilePath)
        {
            _waitForm = new WaitForm();

            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref dischargeEpicrisisHeaderFilePath, ref _missingObject, ref _missingObject, ref _missingObject);

                double previousValue = 20;
                double currentValue = previousValue;

                for (int num = 1; num <= _wordDoc.Content.Paragraphs.Count; num++)
                {
                    Paragraph paragraph = _wordDoc.Content.Paragraphs[num];
                    FindMarkAndReplace(
                        paragraph.Range.Text,
                        null,
                        0,
                        ref previousValue,
                        ref currentValue,
                        patientInfo);
                }

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 14;
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = "Ф.И.О. " + patientInfo.GetFullName() + ", " + patientInfo.Age + " " + ConvertEngine.GetAgeString(patientInfo.Age);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4, 5, 6 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Дата поступления " + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate) + 
                    "\t" + ConvertEngine.GetRightTimeString(patientInfo.DeliveryDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _waitForm.SetProgress(40);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                if (patientInfo.ReleaseDate.HasValue)
                {
                    _paragraph.Range.Text = "Дата выписки " + ConvertEngine.GetRightDateString(patientInfo.ReleaseDate.Value) + "\t\t12:00";
                }
                else
                {
                    _paragraph.Range.Text = "Дата выписки " + "НЕ УКАЗАНА";
                }

                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] diagnoseLines = patientInfo.Diagnose.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "Диагноз " + diagnoseLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                if (diagnoseLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnoseLines[1];
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Сопутствующий: " + patientInfo.ConcomitantDiagnose;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Осложнения: " + patientInfo.Complications;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];                
                _paragraph.Range.Text = "МКБ - " + patientInfo.MKB;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] complaints = patientInfo.MedicalInspectionComplaints.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "Жалобы " + complaints[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                if (complaints.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = complaints[1];
                }

                if (patientInfo.MedicalInspectionIsAnamneseActive)
                {
                    string[] anMorbi = patientInfo.MedicalInspectionAnamneseAnMorbi.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    string traumaDate = string.Empty;
                    if (patientInfo.MedicalInspectionAnamneseTraumaDate.HasValue)
                    {
                        traumaDate += ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionAnamneseTraumaDate.Value);
                    }

                    _paragraph.Range.Text = "An. morbi. " + traumaDate + " " + anMorbi[0];
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                    if (anMorbi.Length > 1)
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = anMorbi[1];
                    }
                }

                _waitForm.SetProgress(50);

                // Добавляем информацию об операциях
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 1;
                _paragraph.Range.Text = "Проведенное лечение:";

                string textStrFirstLine = string.Empty;
                var textStr = new StringBuilder();
                foreach (OperationClass operationInfo in patientInfo.Operations)
                {
                    if (string.IsNullOrEmpty(textStrFirstLine))
                    {
                        textStrFirstLine = string.Format("{0} - {1}", ConvertEngine.GetRightDateString(operationInfo.DataOfOperation), operationInfo.Name);
                    }
                    else
                    {
                        textStr.AppendFormat("\t\t\t{0} - {1}\r\n", ConvertEngine.GetRightDateString(operationInfo.DataOfOperation), operationInfo.Name);
                    }
                }

                if (textStr.Length > 2)
                {
                    textStr.Remove(textStr.Length - 2, 2);
                }

                _waitForm.SetProgress(60);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 0;
                _paragraph.Range.Text = "\tоперация:\t" + textStrFirstLine;
                SetWordsInRangeBold(_paragraph.Range, new[] { 2 });

                if (textStr.Length > 0)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = textStr.ToString();
                }

                // Добавляем информацию о консервативном лечении
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "\tконсервативное лечение: " + patientInfo.GetDischargeEpicrisConservativeTherapy();
                SetWordsInRangeBold(_paragraph.Range, new[] { 2, 3, 4 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] afterOperationLines = patientInfo.DischargeEpicrisAfterOperation.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "После  операции  " + afterOperationLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (afterOperationLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = afterOperationLines[1];
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 12;
                _paragraph.Range.Text = string.Format(
                    "ОАК({0}): эритроциты-{1}х1012/л, лейкоциты-{2}х109/л, Hb-{3} г/л, СОЭ-{4} мм/ч;",
                    ConvertEngine.GetRightDateString((patientInfo.DischargeEpicrisAnalysisDate.HasValue ? patientInfo.DischargeEpicrisAnalysisDate.Value : DateTime.Now)),
                    patientInfo.DischargeEpicrisOakEritrocits,
                    patientInfo.DischargeEpicrisOakLekocits,
                    patientInfo.DischargeEpicrisOakHb,
                    patientInfo.DischargeEpicrisOakSoe);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });                

                _waitForm.SetProgress(70);

                // Возводим в степень 10 в 12-ой и 10 в 9-ой.
                int charNum = _paragraph.Range.Text.IndexOf("х1012/л");
                _paragraph.Range.Characters[charNum + 4].Font.Superscript =
                _paragraph.Range.Characters[charNum + 5].Font.Superscript = 1;

                charNum = _paragraph.Range.Text.IndexOf("х109/л");
                _paragraph.Range.Characters[charNum + 4].Font.Superscript = 1;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Eml отрицательный";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "ОАМ({0}): цвет {1}, относит. плотность {2}, эритроциты {3}, лейкоциты {4}",
                    ConvertEngine.GetRightDateString(patientInfo.DischargeEpicrisAnalysisDate.HasValue ? patientInfo.DischargeEpicrisAnalysisDate.Value : DateTime.Now),
                    patientInfo.DischargeEpicrisOamColor,
                    patientInfo.DischargeEpicrisOamDensity,
                    patientInfo.DischargeEpicrisOamEritrocits,
                    patientInfo.DischargeEpicrisOamLekocits);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakBillirubin) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakGeneralProtein) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakPTI) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakSugar) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBloodGroup))
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];

                    string info = string.Format("БАК({0}): ", ConvertEngine.GetRightDateString(patientInfo.DischargeEpicrisAnalysisDate.HasValue ? patientInfo.DischargeEpicrisAnalysisDate.Value : DateTime.Now));

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakBillirubin))
                    {
                        info += "билирубин " + patientInfo.DischargeEpicrisBakBillirubin + " мкмоль/л, ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakGeneralProtein))
                    {
                        info += "креатинин " + patientInfo.DischargeEpicrisBakGeneralProtein + " мкмоль/л, ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakSugar))
                    {
                        info += "глюкоза " + patientInfo.DischargeEpicrisBakSugar + " ммоль/л, ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakPTI))
                    {
                        info += "ПТИ " + patientInfo.DischargeEpicrisBakPTI + "%, ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBloodGroup))
                    {
                        info += "группа крови " + patientInfo.DischargeEpicrisBloodGroup + " резус фактор " + patientInfo.DischargeEpicrisRhesusFactor + ", ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisAdditionalAnalises))
                    {
                        info += patientInfo.DischargeEpicrisAdditionalAnalises + ", ";
                    }

                    _paragraph.Range.Text = info.Substring(0, info.Length - 2);
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });
                }

                _waitForm.SetProgress(80);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format("ЭКГ({0}): {1}",
                    ConvertEngine.GetRightDateString(patientInfo.DischargeEpicrisAnalysisDate.HasValue ? patientInfo.DischargeEpicrisAnalysisDate.Value : DateTime.Now), patientInfo.DischargeEpicrisEkg);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 14;
                _paragraph.Range.Text = "Рентгенконтроль – удовлетворительно.";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "В удовлетворительном состоянии с улучшением выписывается под наблюдение хирурга по месту жительства.";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 1;
                _paragraph.Range.Text = "Рекомендации при выписке:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 0;

                _waitForm.SetProgress(90);

                var recomendations = new StringBuilder();
                for (int i = 0; i < patientInfo.DischargeEpicrisRecomendations.Count; i++)
                {
                    recomendations.Append(patientInfo.DischargeEpicrisRecomendations[i] + "\r\n");
                }

                for (int i = 0; i < patientInfo.DischargeEpicrisAdditionalRecomendations.Count; i++)
                {
                    recomendations.Append(patientInfo.DischargeEpicrisAdditionalRecomendations[i] + "\r\n");
                }

                if (string.IsNullOrEmpty(recomendations.ToString()))
                {
                    _paragraph.Range.Text = "\tНет рекомендаций\r\n";
                }
                else
                {
                    _paragraph.Range.ListFormat.ApplyNumberDefault(ref _missingObject);
                    _paragraph.Range.Text = recomendations.ToString();
                    _paragraph.Range.ListFormat.ApplyNumberDefaultOld();                    
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.ParagraphFormat.LeftIndent = 0;
                _paragraph.Range.Text = "Лечащий врач\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count - 1].Range.Text = "";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Зав. отделением\t\t\t\t\t\t\t" + _dbEngine.GlobalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }        

        /// <summary>
        /// Экспортировать в Word осмотр в отделении
        /// </summary>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void ExportMedicalInspection(PatientClass patientInfo)
        {
            _waitForm = new WaitForm();

            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);

                try
                {
                    // Пробуем для 2007 офиса выставить стиль документов 2003 офиса.
                    // Для других офисов, вероятно, отвалимся с ошибкой, но для них и не
                    // надо ничего делать.
                    _wordDoc.ApplyQuickStyleSet("Word 2003");
                }
                catch
                {
                }

                _waitForm.SetProgress(10);

                _wordDoc.PageSetup.TopMargin = 30;
                _wordDoc.PageSetup.LeftMargin = 50;
                _wordDoc.PageSetup.RightMargin = 30;
                _wordDoc.PageSetup.BottomMargin = 30;

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;
                _wordRange.Font.Name = "Times New Roman";

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _paragraph.Range.Text = "Осмотр зав. отделением и лечащим врачом";

                AddEmptyParagraph();

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = ConvertEngine.GetRightDateString(patientInfo.DeliveryDate, true);

                string[] complaints = patientInfo.MedicalInspectionComplaints.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Жалобы: " + complaints[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _waitForm.SetProgress(20);

                if (complaints.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = complaints[1];
                }

                if (patientInfo.MedicalInspectionIsAnamneseActive)
                {
                    string[] anMorbi = patientInfo.MedicalInspectionAnamneseAnMorbi.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    string traumaDate = string.Empty;
                    if (patientInfo.MedicalInspectionAnamneseTraumaDate.HasValue)
                    {
                        traumaDate += ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionAnamneseTraumaDate.Value, false);
                    }

                    _paragraph.Range.Text = "An. morbi. " + traumaDate + " " + anMorbi[0];
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                    if (anMorbi.Length > 1)
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = anMorbi[1];
                    }

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "An. vitae. туберкулез: {0}, желтуха: {1}, вен. заболевания: {2}, острозаразные: {3}",
                        patientInfo.MedicalInspectionAnamneseAnVitae[0] ? "есть" : "нет",
                        patientInfo.MedicalInspectionAnamneseAnVitae[1] ? "есть" : "нет",
                        patientInfo.MedicalInspectionAnamneseAnVitae[2] ? "есть" : "нет",
                        patientInfo.MedicalInspectionAnamneseAnVitae[3] ? "есть" : "нет");
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                    "Операции: {0}. Травмы: {1}\r\n" +
                    "Хрон. заболевания:{2}. Воспаление легких: {3}; варикозная болезнь: {4}. Переливание крови: {5}. Контактов с инфекционными больными в последний месяц {6}.\r\n" +
                    "Аллергические реакции на лекарственные препараты: {7}.",
                    patientInfo.MedicalInspectionAnamneseTextBoxes[2],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[6],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[0],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[1],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[4],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[5],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[3],
                    patientInfo.MedicalInspectionAnamneseTextBoxes[7]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 0;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    _wordRange = _paragraph.Range;
                    object defaultTableBehavior = WdDefaultTableBehavior.wdWord9TableBehavior;
                    object autoFitBehavior = WdAutoFitBehavior.wdAutoFitFixed;
                    _wordTable = _wordDoc.Tables.Add(_wordRange, 7, 6, ref defaultTableBehavior, ref autoFitBehavior);

                    _wordTable.Range.Font.Name = "Times New Roman";
                    _wordTable.Range.Font.Size = 10;
                    _wordTable.Range.Font.Bold = 0;
                    _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _wordTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                    _wordTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                    _wordTable.Rows.SetLeftIndent((float)8.5, WdRulerStyle.wdAdjustNone);

                    for (int i = 1; i <= _wordTable.Rows.Count; i++)
                    {
                        _wordTable.Rows[i].Cells[1].Width = 215;
                        _wordTable.Rows[i].Cells[2].Width = 20;
                        _wordTable.Rows[i].Cells[3].Width = 25;
                        _wordTable.Rows[i].Cells[4].Width = 185;
                        _wordTable.Rows[i].Cells[5].Width = 20;
                        _wordTable.Rows[i].Cells[6].Width = 25;
                    }

                    _wordTable.Rows[1].Cells[1].Range.Text = "Факторы риска ТГВ и ТЭЛА";
                    _wordTable.Rows[1].Cells[1].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[2].Range.Text = "да";
                    _wordTable.Rows[1].Cells[2].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[3].Range.Text = "нет";
                    _wordTable.Rows[1].Cells[3].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[5].Range.Text = "да";
                    _wordTable.Rows[1].Cells[5].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[6].Range.Text = "нет";
                    _wordTable.Rows[1].Cells[6].Range.Font.Bold = 1;

                    _wordTable.Rows[2].Cells[1].Range.Text = "1. Венозный тромбоз и ТЭЛА в анамнезе у пациента (тромбофилия)";
                    _wordTable.Rows[2].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[0])
                    {
                        _wordTable.Rows[2].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[2].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[2].Cells[4].Range.Text = "7. Хроническое неспецифическое заболевание легких";
                    _wordTable.Rows[2].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[6])
                    {
                        _wordTable.Rows[2].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[2].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[3].Cells[1].Range.Text = "2. Постромботическая болезнь (тромбофилия)";
                    _wordTable.Rows[3].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[1])
                    {
                        _wordTable.Rows[3].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[3].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[3].Cells[4].Range.Text = "8. Ожирение";
                    _wordTable.Rows[3].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[7])
                    {
                        _wordTable.Rows[3].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[3].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[4].Cells[1].Range.Text = "3. Венозный тромбоз и ТЭЛА у биологических родственников (тромбофилия)";
                    _wordTable.Rows[4].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[2])
                    {
                        _wordTable.Rows[4].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[4].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[4].Cells[4].Range.Text = "9. Иммобилизация нижней конечности с пребыванием в постели 3 и более дней";
                    _wordTable.Rows[4].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[8])
                    {
                        _wordTable.Rows[4].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[4].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[5].Cells[1].Range.Text = "4. Прием антикоагулянтов";
                    _wordTable.Rows[5].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[3])
                    {
                        _wordTable.Rows[5].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[5].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[5].Cells[4].Range.Text = "10. Сахарный диабет";
                    _wordTable.Rows[5].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[9])
                    {
                        _wordTable.Rows[5].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[5].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[6].Cells[1].Range.Text = "5. Варикозное расширение вен";
                    _wordTable.Rows[6].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[4])
                    {
                        _wordTable.Rows[6].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[6].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[6].Cells[4].Range.Text = "11. Прием эстрогенов";
                    _wordTable.Rows[6].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[10])
                    {
                        _wordTable.Rows[6].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[6].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[7].Cells[1].Range.Text = "6. Инфаркт миокарда";
                    _wordTable.Rows[7].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[5])
                    {
                        _wordTable.Rows[7].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[7].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[7].Cells[4].Range.Text = "12. Онкозаболевание";
                    _wordTable.Rows[7].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[11])
                    {
                        _wordTable.Rows[7].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[7].Cells[6].Range.Text = "x";
                    }
                }
                else
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                }
                _waitForm.SetProgress(30);

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "St. praesens. Общее состояние: {0}. " +
                    "Сознание: {1}, положение: {2}. Питание: {3}. " +
                    "Кожный покров и видимые слизистые (вне зоны повреждения): {4}. " +
                    "Щитовидная железа: {5}. Лимфатические узлы: {6}.",
                    patientInfo.MedicalInspectionStPraesensComboBoxes[0],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[0],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[1],
                    patientInfo.MedicalInspectionStPraesensComboBoxes[1],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[2],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[3],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[4]);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "ЧДД {0} в мин. В легких дыхание {1}, {2}, хрипы: {3}. " +
                    "ЧСС {4} в мин. Тоны сердца {5}; ритм {6}. PS {7}.",
                    patientInfo.MedicalInspectionStPraesensNumericUpDowns[0],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[5],
                    patientInfo.MedicalInspectionStPraesensComboBoxes[2],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[6],
                    patientInfo.MedicalInspectionStPraesensNumericUpDowns[1],
                    patientInfo.MedicalInspectionStPraesensComboBoxes[3],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[7],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[8]);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "АД {0}/{1} мм.рт.ст. Живот {2}. Печеночная тупость {3}, " +
                    "притупления в отлогих местах {4}, перистальтика {5}. " +
                    "Per rectum: {6}. Физиологические отправления: {7}. " +
                    "Нагрузка на кости таза, позвоночник {8}. Активные движения в " +
                    "неповрежденных конечностях {9}.",
                    patientInfo.MedicalInspectionStPraesensNumericUpDowns[2],
                    patientInfo.MedicalInspectionStPraesensNumericUpDowns[3],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[9],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[10],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[11],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[12],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[13],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[14],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[15],
                    patientInfo.MedicalInspectionStPraesensTextBoxes[16]);

                if (!string.IsNullOrEmpty(patientInfo.MedicalInspectionStPraesensOthers))
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = patientInfo.MedicalInspectionStPraesensOthers;
                }

                _waitForm.SetProgress(40);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "St. localis:";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4, 5 });

                if (patientInfo.MedicalInspectionIsStLocalisPart1Enabled)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Объем движений в суставах верхней конечности";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Size = 10;
                    _paragraph.Range.Text = string.Format(
                        "Плечевой пояс:\tэлевация / депрессия (F: 20-0-10): акт - {0}, пасс – {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[0],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[1]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\tсгибание/разгибание (Т: 20-0-20): акт - {0}, пасс – {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[2],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[3]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Плечевой сустав:  разгибание/сгибание (S: 50-0-180): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[4],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[5]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   отведение/приведение (F: 180-0-0): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[6],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[7]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   горизонтальное разгибание и сгибание (Т: 30-0-135): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[8],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[9]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   нар. и вн. ротация при отведенном на 90° плече (R: 90-0-90): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[10],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[11]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   нар. и вн. ротация при приведенном плече (R: 65-0-70): акт - {0}, пасс - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[12],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[13]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Локтевой сустав: разгибание и сгибание (S: 0-0-150): акт - {0}, пасс - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[14],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[15]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Луче-локтевые суставы: супинация и пронация (R: 90-0-90): акт - {0}, пасс - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[16],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[17]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3, 4, 5 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Лучезапястный сустав:\tразгибание и сгибание (S: 70-0-80): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[18],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[19]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\tотведение и приведение (F: 25-0-55): акт - {0}, пасс - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[20],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[21]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Суставы 1-го пальца:";
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3, 4, 5, 6 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tЗПС:\tлучевое отведение и приведение (F: 35-0-15): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[22],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[23]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\tладонное отведение и приведение (S: 40-0-0): акт - {0}, пасс - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[24],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[25]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tПФС.\tразгибание и сгибание (S: 5-0-50): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[26],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[27]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tМФС.\tразгибание и сгибание (S: 15-0-85): акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[28],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[29]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2, 3 });

                    _waitForm.SetProgress(50);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\tоппозиция: {0} палец",
                        patientInfo.MedicalInspectionStLocalisPart1OppositionFinger);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Суставы II-V-го пальцев:";
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3, 4, 5, 6, 7, 8 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tПФС: разгибание и сгибание (S: 35-0-90): IIп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[30],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[31]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t  IIIп. - акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[32],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[33]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t  IVп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[34],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[35]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t  V - акт - {0}, пасс - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[36],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[37]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tотведение и приведение (F: 30-0-25): IIп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[38],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[39]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t       IIIп. - акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[40],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[41]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t       IVп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[42],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[43]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t       V - акт - {0}, пасс - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[44],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[45]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tПМФС: разгибание и сгибание (S: 0-0-100): IIп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[46],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[47]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t     IIIп. - акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[48],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[49]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t     IVп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[50],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[51]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t     V - акт - {0}, пасс - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[52],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[53]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\tДМФС: разгибание и сгибание (S: 0-0-80): IIп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[54],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[55]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t   IIIп. - акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[56],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[57]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t   IVп. акт - {0}, пасс - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[58],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[59]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t   V - акт - {0}, пасс - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[60],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[61]);

                    AddEmptyParagraph();
                }

                _waitForm.SetProgress(60);

                if (patientInfo.MedicalInspectionIsStLocalisPart2Enabled)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Size = 12;
                    string axisViolation;
                    if (patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[1] == "нет")
                    {
                        axisViolation = "нет";
                    }
                    else
                    {
                        string rigthComboBox1Value = patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[1] == "пальцев"
                            ? "пальца"
                            : "пястной кости";

                        axisViolation = string.Format(
                            "{0} {1} {2}",
                            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[2],
                            rigthComboBox1Value,
                            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[3]);
                    }

                    _paragraph.Range.Text = string.Format(
                        "Кисть: {0}. Кожа {1}. Нарушение оси {2}.",
                        patientInfo.MedicalInspectionStLocalisPart2WhichHand,
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[0],
                        axisViolation);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Дефигурация: {0}, деформация: {1}, боль при нагрузке по оси: {2}, боль при пальпации: {3}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[0],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[1],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[2],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[3]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Патологическая подвижность: {0}. Пружинистая подвижность: {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[4],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[5]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Раны: {0}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[6]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Форма раны: {0}. Края раны: {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[4],
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[5]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Размеры ран(ы) {0}; {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[7],
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[6]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Кровотечение: {0}. Некрозы: {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[7],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[8]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Пульс на лучевой артерии: {0}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[9]);

                    _waitForm.SetProgress(70);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _paragraph.Range.Text = "Дистальнее повреждения:";

                    if (patientInfo.MedicalInspectionStLocalisPart2WhichHand == "правая, левая" ||
                       patientInfo.MedicalInspectionStLocalisPart2WhichHand == "левая")
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        _paragraph.Range.Text = "Левая кисть";

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Активное сгибание ДМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[0],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[1],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[2],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[3],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[4]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\tПМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[5],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[6],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[7],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[8],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[9]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Активное разгибание ДМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[10],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[11],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[12],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[13],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[14]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\tПМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[15],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[16],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[17],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[18],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[19]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Приведение, отведение I пальца: {0}. Сведение/разведение пальцев: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[20],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[21]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Цвет кожи: {0}; кожа: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[22],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[23]);
                    }

                    _waitForm.SetProgress(80);

                    if (patientInfo.MedicalInspectionStLocalisPart2WhichHand == "правая, левая" ||
                       patientInfo.MedicalInspectionStLocalisPart2WhichHand == "правая")
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        _paragraph.Range.Text = "Правая кисть";

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Активное сгибание  ДМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[0],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[1],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[2],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[3],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[4]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\t\tПМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[5],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[6],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[7],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[8],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[9]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Активное разгибание ДМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[10],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[11],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[12],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[13],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[14]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\t\t  ПМФС: I – {0}, II – {1}, III – {2}, IV – {3}, V – {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[15],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[16],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[17],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[18],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[19]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Приведение, отведение I пальца: {0}. Сведение/разведение пальцев: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[20],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[21]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "Цвет кожи: {0}; кожа: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[22],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[23]);
                    }

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Иннервация: {0}.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[8]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "Пузыри на коже: {0}. Бледное пятно при надавливании на {1} исчезает через {2} сек.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[9],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[10],
                        patientInfo.MedicalInspectionStLocalisPart2NumericUpDown);

                    AddEmptyParagraph();
                }

                _waitForm.SetProgress(90);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 12;

                if (!string.IsNullOrEmpty(patientInfo.MedicalInspectionStLocalisDescription))
                {
                    _paragraph.Range.Text = patientInfo.MedicalInspectionStLocalisDescription;
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                }

                _paragraph.Range.Text = "Рентгенограммы в двух проекциях: " + patientInfo.MedicalInspectionStLocalisRentgen + ".";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Риск ТЭО: " + patientInfo.MedicalInspectionTeoRisk + ".";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string expertAnamnes;
                if (patientInfo.MedicalInspectionExpertAnamnese == 1)
                {
                    expertAnamnes = string.Format(
                            "л/н выдан амбулаторно с {0} по {1}, всего дней нетрудоспособности {2}",
                            ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnWithNumberDateStart),
                            ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnWithNumberDateEnd),
                            ConvertEngine.GetDiffInDays(patientInfo.MedicalInspectionLnWithNumberDateEnd, patientInfo.MedicalInspectionLnWithNumberDateStart) + 1);
                }
                else if (patientInfo.MedicalInspectionExpertAnamnese == 2)
                {
                    expertAnamnes = string.Format(
                        "л/н открыт первично с {0}",
                        ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnFirstDateStart));
                }
                else
                {
                    expertAnamnes = "л/н не требуется.";
                }

                _paragraph.Range.Text = "Экспертный анамнез: " + expertAnamnes;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "На основании анамнеза, жалоб и данных осмотра, ставлю:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] diagnose = patientInfo.Diagnose.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "Клинический диагноз: " + diagnose[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                if (diagnose.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnose[1];
                }

                if (!string.IsNullOrEmpty(patientInfo.ConcomitantDiagnose) && patientInfo.ConcomitantDiagnose.ToLowerInvariant() != "нет")
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = patientInfo.ConcomitantDiagnose;
                }

                AddEmptyParagraph();

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "Врач:______________{0}                  З/о:_______________{1}",
                    patientInfo.DoctorInChargeOfTheCase,
                    _dbEngine.GlobalSettings.BranchManager);

                _waitForm.SetProgress(95);

                if (patientInfo.MedicalInspectionIsPlanEnabled)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        AddEmptyParagraph();
                    }

                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 1;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _paragraph.Range.Text = "ПЛАН ОБСЛЕДОВАНИЯ И ЛЕЧЕНИЯ";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 0;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    _paragraph.Range.ListFormat.ApplyNumberDefault(ref _missingObject);
                    _paragraph.Range.Text = "Обследование: " + patientInfo.MedicalInspectionInspectionPlan + ".\r\n" +
                        "Оперативное лечение - " + patientInfo.ServiceName + ".\r\n" + 
                        "Послеоперационное консервативное лечение:\r\n";
                    _paragraph.Range.ListFormat.ApplyNumberDefaultOld();
                    _paragraph.Range.ListFormat.ApplyBulletDefault(ref _missingObject);
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = 0;
                    object index = 2;
                    _paragraph.Range.ParagraphFormat.TabStops.get_Item(ref index).Position = 50;
                    _paragraph.Range.Text = "медикаментозное лечение: анальгетики, антибиотики\r\n" +
                        "перевязки, ЛФК\r\n";
                    _paragraph.Range.ListFormat.ApplyBulletDefaultOld();
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = -18;
                    _paragraph.Range.Text = "4.\tАмбулаторное долечивание.";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Empty;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Дата " + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate);
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Лечащий врач " + patientInfo.DoctorInChargeOfTheCase;
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });
                    AddEmptyParagraph();
                }

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// Добавить данные по операции. В отдельной функции, потому что печатается 2 раза
        /// </summary>
        /// <param name="operationInfo">Данные по операции</param>
        /// <param name="patientInfo">Данные о пациенте</param>
        private void InsertTableForOperation(OperationClass operationInfo, PatientClass patientInfo)
        {
            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Font.Bold = 1;
            _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            _paragraph.Range.Text = "ОПЕРАЦИЯ";

            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Font.Bold = 0;
            _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            _wordRange = _paragraph.Range;
            _wordTable = _wordDoc.Tables.Add(_wordRange, 11, 3, ref _missingObject, ref _missingObject);

            _wordTable.Range.Font.Name = "Times New Roman";
            _wordTable.Range.Font.Size = 12;
            _wordTable.Range.Font.Bold = 0;
            _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            _wordTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            _wordTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
            
            for (int i = 1; i <= _wordTable.Rows.Count; i++)
            {
                _wordTable.Rows[i].Cells[1].Width = 250;
                _wordTable.Rows[i].Cells[2].Width = 100;
                _wordTable.Rows[i].Cells[3].Width = 130;
            }

            int startRowForMerge = operationInfo.Surgeons.Count + operationInfo.Assistents.Count + 5;

            for (int i = startRowForMerge; i <= 11; i++)
            {
                object begCell = _wordTable.Cell(i, 1).Range.Start;
                object endCell = _wordTable.Cell(i, 3).Range.End;
                MergeCells(begCell, endCell);
            }

            _wordTable.Rows[1].Cells[1].Range.Text = "Фамилия " + patientInfo.LastName;
            SetWordsInRangeBold(_wordTable.Rows[1].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[2].Cells[1].Range.Text = "Имя " + patientInfo.Name;
            SetWordsInRangeBold(_wordTable.Rows[2].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[3].Cells[1].Range.Text = "Отчество " + patientInfo.Patronymic;
            SetWordsInRangeBold(_wordTable.Rows[3].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[4].Cells[1].Range.Text = "Возраст " + patientInfo.Age;
            SetWordsInRangeBold(_wordTable.Rows[4].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[5].Cells[1].Range.Text = "№ ист. б-ни " + patientInfo.NumberOfCaseHistory;
            SetWordsInRangeBold(_wordTable.Rows[5].Cells[1].Range, new[] { 1, 2, 3, 4, 5, 6 });

            _wordTable.Rows[6].Cells[1].Range.Text = "Дата поступления " + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate);
            SetWordsInRangeBold(_wordTable.Rows[6].Cells[1].Range, new[] { 1, 2 });

            _wordTable.Rows[7].Cells[1].Range.Text = string.Format(
                "Дата операции {0} {1}-{2}",
                ConvertEngine.GetRightDateString(operationInfo.DataOfOperation),
                ConvertEngine.GetRightTimeString(operationInfo.StartTimeOfOperation),
                ConvertEngine.GetRightTimeString(operationInfo.EndTimeOfOperation));
            SetWordsInRangeBold(_wordTable.Rows[7].Cells[1].Range, new[] { 1, 2 });

            _wordTable.Rows[8].Cells[1].Range.Text = "Адрес " + patientInfo.GetAddress();
            SetWordsInRangeBold(_wordTable.Rows[8].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[9].Cells[1].Range.Text = "Диагноз " + patientInfo.Diagnose;
            SetWordsInRangeBold(_wordTable.Rows[9].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[10].Cells[1].Range.Text = "Операция " + operationInfo.Name;
            SetWordsInRangeBold(_wordTable.Rows[10].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[11].Cells[1].Range.Text = "Ход операции: " + operationInfo.OperationCourse;
            SetWordsInRangeBold(_wordTable.Rows[11].Cells[1].Range, new[] { 1, 2 });

            int rowNum = 1;
            _wordTable.Rows[rowNum].Cells[2].Range.Text = "Хирург";
            _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

            foreach (string surgeoun in operationInfo.Surgeons)
            {
                _wordTable.Rows[rowNum].Cells[3].Range.Text = surgeoun;
                rowNum++;
            }

            if (operationInfo.Assistents.Count > 0)
            {
                _wordTable.Rows[rowNum].Cells[2].Range.Text = "Ассистент";
                _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

                foreach (string assistent in operationInfo.Assistents)
                {
                    _wordTable.Rows[rowNum].Cells[3].Range.Text = assistent;
                    rowNum++;
                }
            }

            if (!string.IsNullOrEmpty(operationInfo.HeAnaesthetist))
            {
                _wordTable.Rows[rowNum].Cells[2].Range.Text = "Анестезиолог";
                _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

                _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.HeAnaesthetist;
                rowNum++;
            }

            _wordTable.Rows[rowNum].Cells[2].Range.Text = "Опер. м/сестра";
            _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

            _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.ScrubNurse;
            rowNum++;

            if (!string.IsNullOrEmpty(operationInfo.SheAnaesthetist))
            {
                _wordTable.Rows[rowNum].Cells[2].Range.Text = "Анестезистка";
                _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

                _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.SheAnaesthetist;
                rowNum++;
            }

            _wordTable.Rows[rowNum].Cells[2].Range.Text = "Санитар";
            _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

            _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.Orderly;

            string surgeon = operationInfo.Surgeons.Count > 0 ? operationInfo.Surgeons[0] : "                          ";
            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Text = string.Format("Дата {0}, {1}\t\t\t\t{2} _________________",
                ConvertEngine.GetRightDateString(operationInfo.DataOfOperation),
                ConvertEngine.GetRightTimeString(operationInfo.EndTimeOfOperation.AddMinutes(15)),
                surgeon);
            SetWordsInRangeBold(_paragraph.Range, new[] { 1 });
        }

        /// <summary>
        /// Экспортировать в Word протокол операции
        /// </summary>
        /// <param name="operationInfo">Информация об операции</param>
        /// <param name="patientInfo">Информация о пациенте</param>
        public void ExportOperationProtocol(OperationClass operationInfo, PatientClass patientInfo)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;
            _waitForm = new WaitForm();
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);

                try
                {
                    // Пробуем для 2007 офиса выставить стиль документов 2003 офиса.
                    // Для других офисов, вероятно, отвалимся с ошибкой, но для них и не
                    // надо ничего делать.
                    _wordDoc.ApplyQuickStyleSet("Word 2003");
                }
                catch
                {
                }

                _waitForm.SetProgress(10);

                _wordDoc.PageSetup.TopMargin = 30;
                _wordDoc.PageSetup.LeftMargin = 90;
                _wordDoc.PageSetup.RightMargin = 30;
                _wordDoc.PageSetup.BottomMargin = 30;

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;
                _wordRange.Font.Name = "Times New Roman";

                // Создание плана обследования и лечения, если надо
                if (patientInfo.IsTreatmentPlanActiveInOperationProtocol)
                {
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 1;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _paragraph.Range.Text = "ПЛАН ОБСЛЕДОВАНИЯ И ЛЕЧЕНИЯ\r\n";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 0;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    _paragraph.Range.ListFormat.ApplyNumberDefault(ref _missingObject);
                    _paragraph.Range.Text = "Обследование: " + patientInfo.TreatmentPlanInspection + ".\r\n" +
                        "Оперативное лечение.\r\n" +
                        "Послеоперационное консервативное лечение:\r\n";
                    _paragraph.Range.ListFormat.ApplyNumberDefaultOld();
                    _paragraph.Range.ListFormat.ApplyBulletDefault(ref _missingObject);
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = 0;
                    object index = 2;
                    _paragraph.Range.ParagraphFormat.TabStops.get_Item(ref index).Position = 50;
                    _paragraph.Range.Text = "медикаментозное лечение: анальгетики, антибиотики\r\n" +
                        "перевязки, ЛФК\r\n";
                    _paragraph.Range.ListFormat.ApplyBulletDefaultOld();
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = -18;
                    _paragraph.Range.Text = "4.\tАмбулаторное долечивание.";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Empty;
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = 0;
                    _paragraph.Range.ParagraphFormat.LeftIndent = 0;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Дата " + ConvertEngine.GetRightDateString(patientInfo.TreatmentPlanDate);
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Лечащий врач " + patientInfo.DoctorInChargeOfTheCase;
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });
                    AddEmptyParagraph();
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        AddEmptyParagraph();
                    }
                }

                _waitForm.SetProgress(20);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _paragraph.Range.Text = "ПРЕДОПЕРАЦИОННЫЙ ЭПИКРИЗ";

                // Создание текст бокса с печатью
                _wordShape = _wordDoc.Shapes.AddTextbox(
                    MsoTextOrientation.msoTextOrientationHorizontal,
                    320,
                    patientInfo.IsTreatmentPlanActiveInOperationProtocol ? 80 : 50,
                    238,
                    124,
                    ref _missingObject);

                _wordShape.TextFrame.TextRange.Font.Name = "Times New Roman";
                _wordShape.TextFrame.TextRange.Font.Size = 10;
                _wordShape.TextFrame.TextRange.Font.Bold = 1;
                _wordShape.TextFrame.TextRange.Font.Spacing = -1;
                _wordShape.Line.Weight = 2;

                _wordShape.TextFrame.MarginLeft = (float)1.42;
                _wordShape.TextFrame.MarginRight = (float)1.42;
                _wordShape.TextFrame.MarginTop = (float)0.85;
                _wordShape.TextFrame.MarginBottom = (float)0.85;

                _wordShape.WrapFormat.Side = WdWrapSideType.wdWrapLeft;
                _wordShape.WrapFormat.Type = WdWrapType.wdWrapTight;
                _wordShape.WrapFormat.DistanceLeft = 3;
                _wordShape.WrapFormat.DistanceTop = 3;

                _wordShape.TextFrame.TextRange.Text =
                    "Назначение пяти и более лекарственных средств, назначение десяти и более лекарственных средств в течение месяца, является жизненно необходимым (необходимо для лечения) и  соответствует стандартам качества\r\n\r\n" +
                    "Заведующий отделением\r\n" +
                    "(ответственный дежурный врач)\r\n\r\n" +
                    "Лечащий врач (дежурный врач)";

                _wordShape.TextFrame.TextRange.Paragraphs[1].Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                /*_wordShape.TextFrame.TextRange.Paragraphs[2].Range.Font.Size = 8;
                _wordShape.TextFrame.TextRange.Paragraphs[5].Range.Font.Size = 4;*/

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = "Осмотр зав. отделением";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = "Пациент " + patientInfo.GetFullName() + ", " + patientInfo.Age + " " + ConvertEngine.GetAgeString(patientInfo.Age);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                if (operationInfo.BeforeOperationEpicrisisIsDairyEnabled)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Температура тела - " + operationInfo.BeforeOperationEpicrisisTemperature +
                        ". Жалобы: " + operationInfo.BeforeOperationEpicrisisComplaints;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Состояние " + operationInfo.BeforeOperationEpicrisisState +
                        ". Пульс " + operationInfo.BeforeOperationEpicrisisPulse +
                        " в мин., АД " + operationInfo.BeforeOperationEpicrisisADFirst + "/" +
                        operationInfo.BeforeOperationEpicrisisADSecond + " мм.рт.ст., ЧДД " +
                        operationInfo.BeforeOperationEpicrisisChDD + " в мин.";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "В легких дыхание " + operationInfo.BeforeOperationEpicrisisBreath +
                        ", хрипы - " + operationInfo.BeforeOperationEpicrisisWheeze;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Тоны сердца " + operationInfo.BeforeOperationEpicrisisHeartSounds +
                        ", ритм " + operationInfo.BeforeOperationEpicrisisHeartRhythm +
                        ". Живот " + operationInfo.BeforeOperationEpicrisisStomach;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Мочеиспускание " + operationInfo.BeforeOperationEpicrisisUrination +
                        ". Стул " + operationInfo.BeforeOperationEpicrisisStool;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    string[] stLocalisLines = operationInfo.BeforeOperationEpicrisisStLocalis.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                    _paragraph.Range.Text = "St.localis: " + stLocalisLines[0];
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                    if (stLocalisLines.Length > 1)
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = stLocalisLines[1];
                    }
                }

                _waitForm.SetProgress(40);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] diagnoseLines = patientInfo.Diagnose.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "Диагноз: " + diagnoseLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (diagnoseLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnoseLines[1];
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Показано оперативное лечение. Планируется операция: " + operationInfo.Name +
                    ".\r\nПациент согласен на операцию. Противопоказаний нет.";                

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Дата " + ConvertEngine.GetRightDateString(operationInfo.DataOfOperation) +
                    ", " + ConvertEngine.GetRightTimeString(operationInfo.StartTimeOfOperation.AddMinutes(-30));
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Зав. отделением\t\t\t\t\t\t\t" + _dbEngine.GlobalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "Лечащий врач\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                AddEmptyParagraph();

                if (operationInfo.BeforeOperationEpicrisisIsAntibioticProphylaxisExist)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _paragraph.Range.Font.Bold = 1;
                    _paragraph.Range.Text = "Протокол периоперационной антибиотикопрофилактики";

                    AddEmptyParagraph();

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    _paragraph.Range.Font.Bold = 0;
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Дата: " + ConvertEngine.GetRightDateString(operationInfo.DataOfOperation) +
                        ", время " + ConvertEngine.GetRightTimeString(operationInfo.StartTimeOfOperation.AddMinutes(-30));                    

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Введен антибиотик: " + operationInfo.BeforeOperationEpicrisisAntibioticProphylaxis + "\r\n";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "Подпись врача____________\t\t\tПодпись м/с___________\r\n";
                }

                _waitForm.SetProgress(50);

                InsertTableForOperation(operationInfo, patientInfo);

                _waitForm.SetProgress(75);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                object typeBreak = WdBreakType.wdPageBreak;
                _paragraph.Range.InsertBreak(ref typeBreak);

                InsertTableForOperation(operationInfo, patientInfo);

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        private void MergeCells(object begCell, object endCell)
        {
            _wordRange = _wordDoc.Range(ref begCell, ref endCell);
            _wordRange.Select();
            _wordApp.Selection.Cells.Merge();
        }

        /// <summary>
        /// Экспорт листа назначений
        /// </summary>
        /// <param name="patientInfo">Данные о пациенте</param>
        public void ExportPrescriptionTherapy(PatientClass patientInfo)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;
            _waitForm = new WaitForm();
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);

                try
                {
                    // Пробуем для 2007 офиса выставить стиль документов 2003 офиса.
                    // Для других офисов, вероятно, отвалимся с ошибкой, но для них и не
                    // надо ничего делать.
                    _wordDoc.ApplyQuickStyleSet("Word 2003");
                }
                catch
                {
                }

                _waitForm.SetProgress(10);

                _wordDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                _wordDoc.PageSetup.TopMargin = 30;
                _wordDoc.PageSetup.LeftMargin = 90;
                _wordDoc.PageSetup.RightMargin = 30;
                _wordDoc.PageSetup.BottomMargin = 30;

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;
                _wordRange.Font.Name = "Times New Roman";

                _waitForm.SetProgress(30);

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Font.Size = 16;
                _paragraph.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _paragraph.Range.Text = "ЛИСТ НАЗНАЧЕНИЙ ЛЕКАРСТВЕННЫХ ПРЕПАРАТОВ № 1";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 18;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                _paragraph.Range.Text = string.Format("И.Б. {0} {1} {2} {3}, {4} отделение", 
                    patientInfo.NumberOfCaseHistory, patientInfo.GetFullName(), patientInfo.Age, ConvertEngine.GetAgeString(patientInfo.Age), _dbEngine.GlobalSettings.DepartmentName);

                _waitForm.SetProgress(50);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Font.Size = 14;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordRange = _paragraph.Range;
                _wordTable = _wordDoc.Tables.Add(_wordRange, 25, 7, ref _missingObject, ref _missingObject);

                _wordTable.Range.Font.Name = "Times New Roman";
                _wordTable.Range.Font.Size = 14;
                _wordTable.Range.Font.Bold = 0;
                _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                _wordTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                for (int i = 1; i <= _wordTable.Rows.Count; i++)
                {
                    _wordTable.Rows[i].Cells[1].Width = 275;
                    _wordTable.Rows[i].Cells[2].Width = 75;
                    _wordTable.Rows[i].Cells[3].Width = 60;
                    _wordTable.Rows[i].Cells[4].Width = 60;
                    _wordTable.Rows[i].Cells[5].Width = 75;
                    _wordTable.Rows[i].Cells[6].Width = 60;
                    _wordTable.Rows[i].Cells[7].Width = 60;
                }

                object begCell;
                object endCell;

                _wordTable.Cell(1, 1).Range.Text = "ПРЕПАРАТ (название, доза, способ и время введения)";                
                begCell = _wordTable.Cell(1, 1).Range.Start;
                endCell = _wordTable.Cell(3, 1).Range.End;
                MergeCells(begCell, endCell);
                _wordTable.Cell(1, 1).Range.Font.Bold = 1;
                _wordTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _wordTable.Cell(1, 2).Range.Text = "Назначение";
                begCell = _wordTable.Cell(1, 2).Range.Start;
                endCell = _wordTable.Cell(2, 4).Range.End;
                MergeCells(begCell, endCell);
                _wordTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _wordTable.Cell(3, 2).Range.Text = "Дата";
                _wordTable.Cell(3, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Cell(3, 3).Range.Text = "Врач";
                _wordTable.Cell(3, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Cell(3, 4).Range.Text = "м/с";
                _wordTable.Cell(3, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _wordTable.Cell(1, 3).Range.Text = "Отмена";
                begCell = _wordTable.Cell(1, 3).Range.Start;
                endCell = _wordTable.Cell(2, 5).Range.End;
                MergeCells(begCell, endCell);
                _wordTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _wordTable.Cell(2, 5).Range.Text = "Дата";
                _wordTable.Cell(2, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Cell(2, 6).Range.Text = "Врач";
                _wordTable.Cell(2, 6).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Cell(2, 7).Range.Text = "м/с";
                _wordTable.Cell(2, 7).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _waitForm.SetProgress(70);

                for (int i = 0; i < patientInfo.PrescriptionTherapy.Count; i++)
                {
                    string[] data = patientInfo.PrescriptionTherapy[i].Split('&');
                    _wordTable.Cell(i + 3, 1).Range.Text = data[0];
                    _wordTable.Cell(i + 3, 2).Range.Text = data[2];

                    if (!string.IsNullOrEmpty(data[1]))
                    {
                        DateTime endDate = ConvertEngine.GetDateTimeFromString(data[2]);
                        _wordTable.Cell(i + 3, 5).Range.Text = ConvertEngine.GetRightDateString(endDate.AddDays(Convert.ToInt32(data[1])));
                    }
                }

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// Экспорт листа дополнительных методов обследования
        /// </summary>
        /// <param name="patientInfo">Данные о пациенте</param>
        public void ExportSurveys(PatientClass patientInfo)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;
            _waitForm = new WaitForm();
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.Add(ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);

                try
                {
                    // Пробуем для 2007 офиса выставить стиль документов 2003 офиса.
                    // Для других офисов, вероятно, отвалимся с ошибкой, но для них и не
                    // надо ничего делать.
                    _wordDoc.ApplyQuickStyleSet("Word 2003");
                }
                catch
                {
                }

                _waitForm.SetProgress(10);

                _wordDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                _wordDoc.PageSetup.TopMargin = 30;
                _wordDoc.PageSetup.LeftMargin = 90;
                _wordDoc.PageSetup.RightMargin = 20;
                _wordDoc.PageSetup.BottomMargin = 30;

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;
                _wordRange.Font.Name = "Times New Roman";

                _waitForm.SetProgress(30);

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Font.Size = 16;
                _paragraph.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _paragraph.Range.Text = "ЛИСТ НАЗНАЧЕНИЙ ДОПОЛНИТЕЛЬНЫХ МЕТОДОВ ОБСЛЕДОВАНИЯ";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 18;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                _paragraph.Range.Text = string.Format("И.Б. {0} {1} {2} {3}, {4} отделение",
                    patientInfo.NumberOfCaseHistory, patientInfo.GetFullName(), patientInfo.Age, ConvertEngine.GetAgeString(patientInfo.Age), _dbEngine.GlobalSettings.DepartmentName);

                _waitForm.SetProgress(50);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Font.Size = 14;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordRange = _paragraph.Range;
                _wordTable = _wordDoc.Tables.Add(_wordRange, 22, 4, ref _missingObject, ref _missingObject);

                _wordTable.Range.Font.Name = "Times New Roman";
                _wordTable.Range.Font.Size = 14;
                _wordTable.Range.Font.Bold = 0;
                _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                _wordTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                for (int i = 1; i <= _wordTable.Rows.Count; i++)
                {
                    _wordTable.Rows[i].Cells[1].Width = 470;
                    _wordTable.Rows[i].Cells[2].Width = 75;
                    _wordTable.Rows[i].Cells[3].Width = 65;
                    _wordTable.Rows[i].Cells[4].Width = 65;
                }

                object begCell;
                object endCell;

                _wordTable.Cell(1, 1).Range.Text = "Анализы, дополнительные методы обследования, консультации, др. назначения";
                begCell = _wordTable.Cell(1, 1).Range.Start;
                endCell = _wordTable.Cell(2, 1).Range.End;
                MergeCells(begCell, endCell);
                _wordTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _wordTable.Cell(1, 2).Range.Text = "Назначение";
                begCell = _wordTable.Cell(1, 2).Range.Start;
                endCell = _wordTable.Cell(1, 4).Range.End;
                MergeCells(begCell, endCell);
                _wordTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                _wordTable.Cell(2, 2).Range.Text = "Дата";
                _wordTable.Cell(2, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Cell(2, 3).Range.Text = "Врач";
                _wordTable.Cell(2, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Cell(2, 4).Range.Text = "м/с";
                _wordTable.Cell(2, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;               

                _waitForm.SetProgress(70);

                for (int i = 0; i < patientInfo.PrescriptionSurveys.Count; i++)
                {
                    string[] data = patientInfo.PrescriptionSurveys[i].Split('&');
                    _wordTable.Cell(i + 3, 1).Range.Text = data[0];
                    _wordTable.Cell(i + 3, 2).Range.Text = data[1];
                }

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }

        }

        /// <summary>
        /// Открыть в ворде указанный документ и провести замену специальных значений в скобках
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <param name="patientInfo">Информация о пациенте</param>        
        public void ExportAdditionalDocument(object filePath, PatientClass patientInfo)
        {
            CultureInfo oldCi = Thread.CurrentThread.CurrentCulture;
            _waitForm = new WaitForm();
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                _waitForm.Show();
                System.Windows.Forms.Application.DoEvents();

                _wordApp = new Application();

                _wordDoc = _wordApp.Documents.OpenOld(ref filePath, ref _missingObject, ref _missingObject,
                    ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject,
                    ref _missingObject, ref _missingObject, ref _missingObject);

                _waitForm.SetProgress(10);
                double shift = 90.0 / (_wordDoc.Content.Paragraphs.Count + _wordDoc.Shapes.Count);
                double previousValue = 10;
                double currentValue = previousValue;

                for (int num = 1; num <= _wordDoc.Content.Paragraphs.Count; num++)
                {
                    Paragraph paragraph = _wordDoc.Content.Paragraphs[num];
                    FindMarkAndReplace(
                        paragraph.Range.Text,
                        null,
                        shift,
                        ref previousValue,
                        ref currentValue,
                        patientInfo);
                }

                foreach (Shape shape in _wordDoc.Shapes)
                {
                    FindMarkAndReplace(
                        shape.TextFrame.TextRange.Text,
                        shape,
                        shift,
                        ref previousValue,
                        ref currentValue,
                        patientInfo);
                }

                _waitForm.SetProgress(100);

                // Переходим к началу документа
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        private void FindMarkAndReplace(
           string rangeText,
           Shape shape,
           double shift,
           ref double previousValue,
           ref double currentValue,
           PatientClass patientInfo)
        {
            int closeBracketNumber = -1;
            bool isTextChanged = false;
            for (int i = rangeText.Length - 1; i >= 0; i--)
            {
                if (rangeText[i] == '}')
                {
                    closeBracketNumber = i;
                    continue;
                }

                if (rangeText[i] == '\n')
                {
                    closeBracketNumber = -1;
                    continue;
                }

                if (closeBracketNumber > -1 && rangeText[i] == '{')
                {
                    isTextChanged = true;
                    int startIndex = i;
                    int endIndex = closeBracketNumber + 1;

                    string bracketText = rangeText.Substring(startIndex, endIndex - startIndex);
                    string bracketNewText = GetRealParameterInsteadSpecialMark(bracketText, patientInfo);

                    if (shape == null)
                    {
                        FindAndReplace(bracketText, bracketNewText);
                    }

                    rangeText = rangeText.Substring(0, startIndex) + bracketNewText + rangeText.Substring(endIndex);

                    closeBracketNumber = -1;
                }
            }

            if (isTextChanged && shape != null)
            {
                rangeText = rangeText.TrimEnd('\r', '\n', '\a', ' ');
                shape.TextFrame.TextRange.Text = rangeText;
            }

            currentValue += shift;
            if (currentValue > previousValue)
            {
                previousValue = currentValue;
                _waitForm.SetProgress(previousValue);
            }
        }

        private int GetOperationNumber(PatientClass patientInfo, string mark)
        {
            // Получаем номер операции, начинающийся с 1
            string[] data = mark.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int number = Convert.ToInt32(data[data.Length - 1].Trim(' '));
            if (patientInfo.Operations.Count < number || number < 1)
            {
                throw new Exception("НЕТ ОПЕРАЦИИ С НОМЕРОМ " + number);
            }

            // Возвращаем номер операции, начинающийся с 0
            return number - 1;
        }

        /// <summary>
        /// Вернуть нужное значение вместо параметра в документе
        /// </summary>
        /// <param name="mark">Метка в документе</param>
        /// <param name="patientInfo">Информация о пациенте</param>        
        /// <returns></returns>
        private string GetRealParameterInsteadSpecialMark(string mark, PatientClass patientInfo)
        {
            if (!mark.StartsWith("{") || !mark.EndsWith("}"))
            {
                return string.Empty;
            }

            mark = mark.Trim(new[] { '{', '}', ' ' }).ToLower();
            var sb = new StringBuilder();
            foreach (char ch in mark)
            {
                if (ch != ' ' || sb[sb.Length - 1] != ' ')
                {
                    sb.Append(ch);
                }
            }

            mark = sb.ToString();

            try
            {
                int operationNumber;
                if (mark.StartsWith("дата операции"))
                {
                    operationNumber = GetOperationNumber(patientInfo, mark);
                    return ConvertEngine.GetRightDateString(patientInfo.Operations[operationNumber].DataOfOperation);
                }

                if (mark.StartsWith("время начала операции"))
                {
                    operationNumber = GetOperationNumber(patientInfo, mark);
                    return ConvertEngine.GetRightTimeString(patientInfo.Operations[operationNumber].StartTimeOfOperation);
                }

                if (mark.StartsWith("время окончания операции"))
                {
                    operationNumber = GetOperationNumber(patientInfo, mark);
                    return ConvertEngine.GetRightTimeString(patientInfo.Operations[operationNumber].EndTimeOfOperation);
                }

                if (mark.StartsWith("название операции"))
                {
                    operationNumber = GetOperationNumber(patientInfo, mark);
                    return patientInfo.Operations[operationNumber].Name;
                }
            }
            catch (Exception ex)
            {
                return "{" + ex.Message.ToUpper() + "}";
            }

            switch (mark)
            {
                case "фио пациента":
                    return patientInfo.GetFullName();
                case "возраст":
                    return patientInfo.Age.ToString();
                case "адрес":
                    return patientInfo.GetAddress();
                case "дата рождения":
                    return ConvertEngine.GetRightDateString(patientInfo.Birthday);
                case "дата поступления":
                    return ConvertEngine.GetRightDateString(patientInfo.DeliveryDate);
                case "время поступления":
                    return ConvertEngine.GetRightTimeString(patientInfo.DeliveryDate);
                case "дата выписки":
                    if (patientInfo.ReleaseDate.HasValue)
                    {
                        return ConvertEngine.GetRightDateString(patientInfo.ReleaseDate.Value);
                    }

                    return "{ДАТА ВЫПИСКИ НЕ УКАЗАНА}";
                case "койко дни":
                    if (patientInfo.ReleaseDate.HasValue)
                    {
                        TimeSpan threatmentPeriod = patientInfo.ReleaseDate.Value - patientInfo.DeliveryDate;
                        return threatmentPeriod.Days.ToString();
                    }

                    return "{КОЙКО ДНИ НЕВЫЧИСЛИМЫ Т.К. ДАТА ВЫПИСКИ НЕ УКАЗАНА}";
                case "диагноз":
                    return patientInfo.Diagnose;
                case "консервативная терапия":
                    return patientInfo.GetDischargeEpicrisConservativeTherapy();
                case "№ иб":
                    return patientInfo.NumberOfCaseHistory;
                case "№ отделения":
                    return _dbEngine.GlobalSettings.DepartmentName;
                case "фио лечащего врача":
                    return patientInfo.DoctorInChargeOfTheCase;
                case "мкб":
                    return patientInfo.MKB;
                case "название услуги":
                    if (!string.IsNullOrEmpty(patientInfo.ServiceName))
                    {
                        return patientInfo.ServiceName;
                    }

                    return "{НАЗВАНИЕ УСЛУГИ НЕ УКАЗАНО}";
                case "код услуги":
                    if (!string.IsNullOrEmpty(patientInfo.ServiceCode))
                    {
                        return patientInfo.ServiceCode;
                    }

                    return "{КОД УСЛУГИ НЕ УКАЗАН}";
                case "код ксг":
                    if (!string.IsNullOrEmpty(patientInfo.KsgCode))
                    {
                        return patientInfo.KsgCode;
                    }

                    return "{КОД КСГ НЕ УКАЗАН}";
                case "расшифровка ксг":
                    if (!string.IsNullOrEmpty(patientInfo.KsgDecoding))
                    {
                        return patientInfo.KsgDecoding;
                    }

                    return "{РАСШИФРОВКА КСГ НЕ УКАЗАНА}";
                case "работа":
                    return patientInfo.WorkPlace;
                case "паспорт":
                    return patientInfo.PassportNumber;
                case "полис":
                    return patientInfo.PolisNumber;
                case "снилс":
                    return patientInfo.SnilsNumber;
                case "фио зав. отделением":
                    return _dbEngine.GlobalSettings.BranchManager;
                case "cегодняшняя дата":
                    return ConvertEngine.GetRightDateString(DateTime.Now);
                case "хирург":
                    if (patientInfo.Operations.Count > 0)
                    {
                        return ConvertEngine.ListToString(patientInfo.Operations[0].Surgeons, ",");
                    }

                    return "{ХИРУРГ НЕ НАЙДЕН, Т.К. НЕТ ОПЕРАЦИЙ}";
                case "анестезиолог":
                    if (patientInfo.Operations.Count > 0)
                    {
                        return patientInfo.Operations[0].HeAnaesthetist;
                    }

                    return "{АНЕСТЕЗИОЛОГ НЕ НАЙДЕН, Т.К. НЕТ ОПЕРАЦИЙ}";
                default:
                    return "{" + mark.ToUpper() + "}";
            }
        }

        /// <summary>
        /// Найти и заменить в документе
        /// </summary>
        /// <param name="findText">Текст для поиска</param>
        /// <param name="replaceText">Текст для замены</param>
        private void FindAndReplace(string findText, string replaceText)
        {
            _wordApp.Selection.Find.ClearFormatting();
            _wordApp.Selection.Find.Text = findText;

            _wordApp.Selection.Find.Replacement.ClearFormatting();
            _wordApp.Selection.Find.Replacement.Text = replaceText;

            object replaceAll = WdReplace.wdReplaceAll;

            _wordApp.Selection.Find.Execute(
                ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject,
                ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject,
                ref replaceAll, ref _missingObject, ref _missingObject, ref _missingObject, ref _missingObject);
        }

        /// <summary>
        /// Освободить ресурсы после генерации документа
        /// </summary>
        private void ReleaseComObject()
        {
            if (_wordApp != null)
            {
                _wordApp.Visible = true;
                _wordApp.Activate();

                if (_wordDoc != null)
                {
                    Marshal.ReleaseComObject(_wordDoc);
                    _wordDoc = null;
                }

                if (_wordShape != null)
                {
                    Marshal.ReleaseComObject(_wordShape);
                    _wordShape = null;
                }

                if (_paragraph != null)
                {
                    Marshal.ReleaseComObject(_paragraph);
                    _paragraph = null;
                }

                if (_wordRange != null)
                {
                    Marshal.ReleaseComObject(_wordRange);
                    _wordRange = null;
                }

                if (_wordTable != null)
                {
                    Marshal.ReleaseComObject(_wordTable);
                    _wordTable = null;
                }

                Marshal.ReleaseComObject(_wordApp);
                _wordApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        /// <summary>
        /// Добавляем в текущий документ пустой параграф
        /// </summary>
        private void AddEmptyParagraph()
        {
            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Bold = 0;
            _paragraph.Range.Text = string.Empty;
        }

        /// <summary>
        /// Сделать жирным переданные номера слов в текущем Range
        /// </summary>
        /// <param name="range">Range, в котором выделяем слова</param>
        /// <param name="wordNumbers">Номера слов в Range, которые надо сделать жирными</param>
        private void SetWordsInRangeBold(Range range, IEnumerable<int> wordNumbers)
        {
            foreach (int wordNumber in wordNumbers)
            {
                if (range.Words.Count > wordNumber)
                {
                    range.Words[wordNumber].Bold = 1;
                }
            }
        }

        /// <summary>
        /// Сделать подчёркнутыми переданные номера слов в текущем Range
        /// </summary>
        /// <param name="range">Range, в котором выделяем слова</param>
        /// <param name="wordNumbers">Номера слов в Range, которые надо сделать жирными</param>
        private void SetWordsInRangeUnderline(Range range, IEnumerable<int> wordNumbers)
        {
            foreach (int wordNumber in wordNumbers)
            {
                if (range.Words.Count > wordNumber)
                {
                    range.Words[wordNumber].Underline = WdUnderline.wdUnderlineSingle;
                }
            }
        }
    }
}
