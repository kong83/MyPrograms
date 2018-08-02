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
        private static Application _wordApp;
        private static Document _wordDoc;
        private static Paragraph _paragraph;
        private static Range _wordRange;
        private static Table _wordTable;
        private static Shape _wordShape;

        private static object _missingObject = Type.Missing;

        private static WaitForm _waitForm;

        /// <summary>
        /// �������������� � Word ���������� �������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        /// <param name="globalSettings">���������� ���������</param>
        public static void ExportTransferableEpicrisis(PatientClass patientInfo, GlobalSettingsClass globalSettings)
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
                    // ������� ��� 2007 ����� ��������� ����� ���������� 2003 �����.
                    // ��� ������ ������, ��������, ��������� � �������, �� ��� ��� � ��
                    // ���� ������ ������.
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
                _paragraph.Range.Text = "���������� �������\r\n";

                _waitForm.SetProgress(20);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                _paragraph.Range.Text = string.Format(
                    "������� {0}, {1} ���, ��������� �� ������� � {2} �.�. � {3} � ���������: {4}\r\n",
                    patientInfo.GetFullName(),
                    patientInfo.Age,
                    globalSettings.DepartmentName,
                    ConvertEngine.GetRightDateString(patientInfo.DeliveryDate),
                    patientInfo.Diagnose);

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "��������� �������:";

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
                _paragraph.Range.Text = "����������������� ������ " + patientInfo.TransferEpicrisAfterOperationPeriod;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "�����������:";

                _waitForm.SetProgress(60);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = patientInfo.TransferEpicrisPlan;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "��� ����������� ������� � ������������������ ��������� ����������� �� ������� ���������.";

                _waitForm.SetProgress(70);

                if (patientInfo.TransferEpicrisIsIncludeDisabilityList)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "�/� � {0} ������� � {1} �� {2}.\r\n",
                        patientInfo.TransferEpicrisDisabilityList,
                        ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate.AddDays(1)),
                        ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate.AddDays(10)));
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "� ������� ���������� _____________________________\r\n";

                _waitForm.SetProgress(80);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���� " + ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���. ����������\t\t\t\t\t\t\t" + globalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _waitForm.SetProgress(90);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "������� ����\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// �������������� � Word ������� �������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        /// <param name="globalSettings">���������� ���������</param>
        public static void ExportLineOfCommunicationEpicrisis(PatientClass patientInfo, GlobalSettingsClass globalSettings)
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
                    // ������� ��� 2007 ����� ��������� ����� ���������� 2003 �����.
                    // ��� ������ ������, ��������, ��������� � �������, �� ��� ��� � ��
                    // ���� ������ ������.
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
                _paragraph.Range.Text = "������� �������\r\n";

                _waitForm.SetProgress(20);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                _paragraph.Range.Text = string.Format(
                    "������� {0}, {1} ���, ��������� �� ������� � {2} �.�. � {3} � ���������: {4}\r\n",
                    patientInfo.GetFullName(),
                    patientInfo.Age,
                    globalSettings.DepartmentName,
                    ConvertEngine.GetRightDateString(patientInfo.DeliveryDate),
                    patientInfo.Diagnose);

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "��������� �������:";

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
                _paragraph.Range.Text = "�����������:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = patientInfo.LineOfCommEpicrisPlan + "\r\n";

                _waitForm.SetProgress(60);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���� " + ConvertEngine.GetRightDateString(patientInfo.LineOfCommEpicrisWritingDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _waitForm.SetProgress(80);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���. ����������\t\t\t\t\t\t\t" + globalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _waitForm.SetProgress(90);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "������� ����\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordRange = _wordDoc.Range(ref _missingObject, ref _missingObject);
                _wordRange.Font.Size = 12;

                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// �������������� � Word �������� �������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        /// <param name="globalSettings">���������� ���������</param>
        /// <param name="dischargeEpicrisisHeaderFilePath">���� �� ����� � ������ ��� ��������� ��������</param>
        public static void ExportDischargeEpicrisisFor8Department(PatientClass patientInfo, GlobalSettingsClass globalSettings, object dischargeEpicrisisHeaderFilePath)
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
                        patientInfo,
                        globalSettings);
                }

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Size = 14;
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = "�.�.�. " + patientInfo.GetFullName() + ", " + patientInfo.Age + " ���";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4, 5, 6 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���� ����������� " + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _waitForm.SetProgress(40);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                if (patientInfo.ReleaseDate.HasValue)
                {
                    _paragraph.Range.Text = "���� ������� " + ConvertEngine.GetRightDateString(patientInfo.ReleaseDate.Value);
                }
                else
                {
                    _paragraph.Range.Text = "���� ������� " + "�� �������";
                }

                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] diagnoseLines = patientInfo.Diagnose.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "������� " + diagnoseLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                if (diagnoseLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnoseLines[1];
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] complaints = patientInfo.MedicalInspectionComplaints.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "������ " + complaints[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                if (complaints.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = complaints[1];
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] anMorbi = patientInfo.MedicalInspectionAnamneseAnMorbi.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "An. morbi. " + anMorbi[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                if (anMorbi.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = anMorbi[1];
                }

                AddEmptyParagraph();

                _waitForm.SetProgress(50);

                // ��������� ���������� �� ���������
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 1;
                _paragraph.Range.Text = "����������� �������:";

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
                _paragraph.Range.Text = "\t��������:\t" + textStrFirstLine;
                SetWordsInRangeBold(_paragraph.Range, new[] { 2 });

                if (textStr.Length > 0)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = textStr.ToString();
                }

                // ��������� ���������� � �������������� �������
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "\t�������������� �������: " + patientInfo.DischargeEpicrisConservativeTherapy;
                SetWordsInRangeBold(_paragraph.Range, new[] { 2, 3, 4 });

                AddEmptyParagraph();

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] afterOperationLines = patientInfo.DischargeEpicrisAfterOperation.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "�����  ��������  " + afterOperationLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (afterOperationLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = afterOperationLines[1];
                }

                AddEmptyParagraph();

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "���({0}): ����������-{1}�1012/�, ���������-{2}�109/�, Hb-{3} �/�, ���-{4} ��/�;",
                    patientInfo.DischargeEpicrisAnalysisDate.HasValue ? patientInfo.DischargeEpicrisAnalysisDate.Value.ToString("dd.MM.yyyy") : DateTime.Now.ToString("dd.MM.yyyy"),
                    patientInfo.DischargeEpicrisOakEritrocits,
                    patientInfo.DischargeEpicrisOakLekocits,
                    patientInfo.DischargeEpicrisOakHb,
                    patientInfo.DischargeEpicrisOakSoe);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _waitForm.SetProgress(70);

                // �������� � ������� 10 � 12-�� � 10 � 9-��.
                int charNum = _paragraph.Range.Text.IndexOf("�1012/�");
                _paragraph.Range.Characters[charNum + 4].Font.Superscript =
                _paragraph.Range.Characters[charNum + 5].Font.Superscript = 1;

                charNum = _paragraph.Range.Text.IndexOf("�109/�");
                _paragraph.Range.Characters[charNum + 4].Font.Superscript = 1;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "���({0}): ���� {1}, �������. ��������� {2}, ���������� {3}, ��������� {4}",
                    patientInfo.DischargeEpicrisAnalysisDate.HasValue ? patientInfo.DischargeEpicrisAnalysisDate.Value.ToString("dd.MM.yyyy") : DateTime.Now.ToString("dd.MM.yyyy"),
                    patientInfo.DischargeEpicrisOamColor,
                    patientInfo.DischargeEpicrisOamDensity,
                    patientInfo.DischargeEpicrisOamEritrocits,
                    patientInfo.DischargeEpicrisOamLekocits);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakBillirubin) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakGeneralProtein) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakPTI) ||
                    !string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakSugar))
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];

                    string info = "���: ";

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakBillirubin))
                    {
                        info += "���������� " + patientInfo.DischargeEpicrisBakBillirubin + ", ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakGeneralProtein))
                    {
                        info += "����� ����� " + patientInfo.DischargeEpicrisBakGeneralProtein + ", ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakSugar))
                    {
                        info += "����� " + patientInfo.DischargeEpicrisBakSugar + ", ";
                    }

                    if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisBakPTI))
                    {
                        info += "��� " + patientInfo.DischargeEpicrisBakPTI + ", ";
                    }

                    _paragraph.Range.Text = info.Substring(0, info.Length - 2);
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });
                }

                if (!string.IsNullOrEmpty(patientInfo.DischargeEpicrisAdditionalAnalises))
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = patientInfo.DischargeEpicrisAdditionalAnalises;
                }

                _waitForm.SetProgress(80);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���: " + patientInfo.DischargeEpicrisEkg + "      Eml �";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "��������������� � �����������������.";                

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 1;
                _paragraph.Range.Text = "������������ ��� �������:";

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
                    _paragraph.Range.Text = "\t��� ������������\r\n";
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
                _paragraph.Range.Text = "������� ����\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���. ����������\t\t\t\t\t\t\t" + globalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// �������������� � Word �������� ������� ��� �������� ���������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        /// <param name="globalSettings">���������� ���������</param>
        /// <param name="dischargeEpicrisisHeaderFilePath">���� �� ����� � ������ ��� ��������� ��������</param>
        public static void ExportDischargeEpicrisisForAllDepartment(PatientClass patientInfo, GlobalSettingsClass globalSettings, object dischargeEpicrisisHeaderFilePath)
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
                        patientInfo,
                        globalSettings);
                }

                _waitForm.SetProgress(30);               

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = "���: " + patientInfo.GetFullName() + ", " + patientInfo.Age + " ���";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "����������������(�) {0}. �������(�) {1}.",
                    ConvertEngine.GetRightDateString(patientInfo.DeliveryDate),
                    patientInfo.ReleaseDate.HasValue ? ConvertEngine.GetRightDateString(patientInfo.ReleaseDate.Value) : "�� �������");

                _waitForm.SetProgress(40);

                string anamneseTraumaDate = patientInfo.MedicalInspectionIsAnamneseActive && patientInfo.MedicalInspectionAnamneseTraumaDate.HasValue
                    ? ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionAnamneseTraumaDate, false)
                    : "�� �������";
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format("���������������: ��������. ������: � ����, {0}.", anamneseTraumaDate);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "�������������� ������: {0}",
                     patientInfo.MedicalInspectionAnamneseAnMorbi);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] diagnoseLines = patientInfo.Diagnose.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "�������: " + diagnoseLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (diagnoseLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnoseLines[1];
                }

                _waitForm.SetProgress(50);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "����������: ���";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "�������������: ���";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 1;
                _paragraph.Range.Text = "�������������� ������ ������������.";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = "\t����� ������ �����:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordRange = _paragraph.Range;
                object defaultTableBehavior = WdDefaultTableBehavior.wdWord9TableBehavior;
                object autoFitBehavior = WdAutoFitBehavior.wdAutoFitFixed;
                _wordTable = _wordDoc.Tables.Add(_wordRange, 3, 6, ref defaultTableBehavior, ref autoFitBehavior);

                _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Range.Font.Size = 10;
                _wordTable.Rows.SetLeftIndent((float)8.5, WdRulerStyle.wdAdjustNone);

                _waitForm.SetProgress(60);

                for (int i = 1; i <= _wordTable.Rows.Count; i++)
                {
                    _wordTable.Rows[i].Cells[1].Width = 50;
                    _wordTable.Rows[i].Cells[2].Width = 110;
                    _wordTable.Rows[i].Cells[3].Width = 100;
                    _wordTable.Rows[i].Cells[4].Width = 70;
                    _wordTable.Rows[i].Cells[5].Width = 100;
                    _wordTable.Rows[i].Cells[6].Width = 70;
                }

                _wordTable.Rows[1].Cells[1].Range.Text = "����";

                _wordTable.Rows[1].Cells[2].Range.Text = "�������., �1012/�";
                int charNum = _wordTable.Rows[1].Cells[2].Range.Text.IndexOf("12");
                _wordTable.Rows[1].Cells[2].Range.Characters[charNum + 1].Font.Superscript =
                _wordTable.Rows[1].Cells[2].Range.Characters[charNum + 2].Font.Superscript = 1;

                _wordTable.Rows[1].Cells[3].Range.Text = "������., �109/�";
                charNum = _wordTable.Rows[1].Cells[3].Range.Text.IndexOf("9");
                _wordTable.Rows[1].Cells[3].Range.Characters[charNum + 1].Font.Superscript = 1;

                _wordTable.Rows[1].Cells[4].Range.Text = "Hb, �/�";

                _wordTable.Rows[1].Cells[5].Range.Text = "�����., �109/�";
                charNum = _wordTable.Rows[1].Cells[5].Range.Text.IndexOf("9");
                _wordTable.Rows[1].Cells[5].Range.Characters[charNum + 1].Font.Superscript = 1;

                _wordTable.Rows[1].Cells[6].Range.Text = "���, ��/�";

                _wordTable.Rows[2].Cells[2].Range.Text = patientInfo.DischargeEpicrisOakEritrocits;
                _wordTable.Rows[2].Cells[3].Range.Text = patientInfo.DischargeEpicrisOakLekocits;
                _wordTable.Rows[2].Cells[4].Range.Text = patientInfo.DischargeEpicrisOakHb;
                _wordTable.Rows[2].Cells[6].Range.Text = patientInfo.DischargeEpicrisOakSoe;

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "\t����� ������ ����:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordRange = _paragraph.Range;
                _wordTable = _wordDoc.Tables.Add(_wordRange, 3, 6, ref defaultTableBehavior, ref autoFitBehavior);

                _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Range.Font.Size = 10;
                _wordTable.Rows.SetLeftIndent((float)8.5, WdRulerStyle.wdAdjustNone);

                _waitForm.SetProgress(70);

                for (int i = 1; i <= _wordTable.Rows.Count; i++)
                {
                    _wordTable.Rows[i].Cells[1].Width = 50;
                    _wordTable.Rows[i].Cells[2].Width = 110;
                    _wordTable.Rows[i].Cells[3].Width = 100;
                    _wordTable.Rows[i].Cells[4].Width = 70;
                    _wordTable.Rows[i].Cells[5].Width = 100;
                    _wordTable.Rows[i].Cells[6].Width = 70;
                }

                _wordTable.Rows[1].Cells[1].Range.Text = "����";
                _wordTable.Rows[1].Cells[2].Range.Text = "�������� ���";
                _wordTable.Rows[1].Cells[3].Range.Text = "�����";
                _wordTable.Rows[1].Cells[4].Range.Text = "�����";
                _wordTable.Rows[1].Cells[5].Range.Text = "���������";
                _wordTable.Rows[1].Cells[6].Range.Text = "����������";

                _wordTable.Rows[2].Cells[2].Range.Text = patientInfo.DischargeEpicrisOamDensity;
                _wordTable.Rows[2].Cells[3].Range.Text = "���";
                _wordTable.Rows[2].Cells[4].Range.Text = "���";
                _wordTable.Rows[2].Cells[5].Range.Text = patientInfo.DischargeEpicrisOamEritrocits;
                _wordTable.Rows[2].Cells[6].Range.Text = patientInfo.DischargeEpicrisOamLekocits;

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "\t������������� ������ �����:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordRange = _paragraph.Range;
                _wordTable = _wordDoc.Tables.Add(_wordRange, 4, 4, ref defaultTableBehavior, ref autoFitBehavior);

                _wordTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                _wordTable.Range.Font.Size = 10;
                _wordTable.Rows.SetLeftIndent((float)8.5, WdRulerStyle.wdAdjustNone);

                for (int i = 1; i <= _wordTable.Rows.Count; i++)
                {
                    _wordTable.Rows[i].Cells[1].Width = 160;
                    _wordTable.Rows[i].Cells[2].Width = 90;
                    _wordTable.Rows[i].Cells[3].Width = 160;
                    _wordTable.Rows[i].Cells[4].Width = 90;
                }

                _wordTable.Rows[1].Cells[1].Range.Text = "��������� �����, ������/�";
                _wordTable.Rows[1].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[2].Cells[1].Range.Text = "����� �����, �/�";
                _wordTable.Rows[2].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[3].Cells[1].Range.Text = "���, ��/�";
                _wordTable.Rows[3].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[4].Cells[1].Range.Text = "���, ��/�";
                _wordTable.Rows[4].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[1].Cells[3].Range.Text = "�����, �����/�";
                _wordTable.Rows[1].Cells[3].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[2].Cells[3].Range.Text = "���, %";
                _wordTable.Rows[2].Cells[3].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[3].Cells[3].Range.Text = "��������, �����/�";
                _wordTable.Rows[3].Cells[3].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _wordTable.Rows[4].Cells[3].Range.Text = "���������, ������/�";
                _wordTable.Rows[4].Cells[3].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

                _waitForm.SetProgress(80);

                _wordTable.Rows[1].Cells[2].Range.Text = patientInfo.DischargeEpicrisBakBillirubin;
                _wordTable.Rows[2].Cells[2].Range.Text = patientInfo.DischargeEpicrisBakGeneralProtein;
                _wordTable.Rows[1].Cells[4].Range.Text = patientInfo.DischargeEpicrisBakSugar;
                _wordTable.Rows[2].Cells[4].Range.Text = patientInfo.DischargeEpicrisBakPTI;

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = patientInfo.DischargeEpicrisAdditionalAnalises;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���: " + patientInfo.DischargeEpicrisEkg;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���������������: �� �����������\r\n��� ������� �/�����������: �� �����������\r\n��� ������� ������� �������: �� �����������\r\n�������������� ������� ������� ������: �� �����������";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] therapyLines = patientInfo.DischargeEpicrisConservativeTherapy.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "��������������� �������: " + therapyLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });
                if (therapyLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = therapyLines[1];
                }

                // ��������� ���������� �� ���������
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];

                var textStr = new StringBuilder();
                foreach (OperationClass operationInfo in patientInfo.Operations)
                {
                    textStr.AppendFormat("{0} - {1}\r\n", ConvertEngine.GetRightDateString(operationInfo.DataOfOperation), operationInfo.Name);
                }

                if (textStr.Length > 2)
                {
                    textStr.Remove(textStr.Length - 2, 2);
                }

                string[] operationInfoLines = textStr.ToString().Split(new[] { "\r\n" }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (operationInfoLines.Length > 0)
                {
                    _paragraph.Range.Text = "����������� �������: " + operationInfoLines[0];
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                    if (operationInfoLines.Length > 1)
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = operationInfoLines[1];
                    }
                }

                _waitForm.SetProgress(90);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = patientInfo.DischargeEpicrisAfterOperation + "\r\n����������: ���";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 1;
                _paragraph.Range.Text = "������������:";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Bold = 0;

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
                    _paragraph.Range.Text = "��� ������������\r\n";
                }
                else
                {
                    _paragraph.Range.ListFormat.ApplyNumberDefault(ref _missingObject);
                    _paragraph.Range.Text = recomendations.ToString();
                    _paragraph.Range.ListFormat.ApplyNumberDefaultOld();
                }

                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.ParagraphFormat.LeftIndent = 0;
                _paragraph.Range.Bold = 1;
                _paragraph.Range.Text = "�������(��) ������������ �� ������������ ������� � ������������������ ���������\r\n�������������� ������ �� ����\r\n������ �������� ��� ������� ���� �������. �������� � ������ ����� ������ �� �������� ������������.";                

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "������� ����\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���. ����������\t\t\t\t\t\t\t" + globalSettings.BranchManager;

                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// �������������� � Word ������ � ���������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        /// <param name="globalSettings">���������� ���������</param>
        public static void ExportMedicalInspection(PatientClass patientInfo, GlobalSettingsClass globalSettings)
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
                    // ������� ��� 2007 ����� ��������� ����� ���������� 2003 �����.
                    // ��� ������ ������, ��������, ��������� � �������, �� ��� ��� � ��
                    // ���� ������ ������.
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
                _paragraph.Range.Text = "������ ���. ���������� � ������� ������";

                AddEmptyParagraph();

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = ConvertEngine.GetRightDateString(patientInfo.DeliveryDate, true);

                string[] complaints = patientInfo.MedicalInspectionComplaints.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "������: " + complaints[0];
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
                        "An. vitae. ����������: {0}, �������: {1}, ���. �����������: {2}, �������������: {3}",
                        patientInfo.MedicalInspectionAnamneseAnVitae[0] ? "����" : "���",
                        patientInfo.MedicalInspectionAnamneseAnVitae[1] ? "����" : "���",
                        patientInfo.MedicalInspectionAnamneseAnVitae[2] ? "����" : "���",
                        patientInfo.MedicalInspectionAnamneseAnVitae[3] ? "����" : "���");
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3, 4 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                    "��������: {0}. ������: {1}\r\n" +
                    "����. �����������:{2}. ���������� ������: {3}; ���������� �������: {4}. ����������� �����: {5}. ��������� � ������������� �������� � ��������� ����� {6}.\r\n" +
                    "������������� ������� �� ������������� ���������: {7}.",
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

                    _wordTable.Rows[1].Cells[1].Range.Text = "������� ����� ��� � ����";
                    _wordTable.Rows[1].Cells[1].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[2].Range.Text = "��";
                    _wordTable.Rows[1].Cells[2].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[3].Range.Text = "���";
                    _wordTable.Rows[1].Cells[3].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[5].Range.Text = "��";
                    _wordTable.Rows[1].Cells[5].Range.Font.Bold = 1;

                    _wordTable.Rows[1].Cells[6].Range.Text = "���";
                    _wordTable.Rows[1].Cells[6].Range.Font.Bold = 1;

                    _wordTable.Rows[2].Cells[1].Range.Text = "1. �������� ������� � ���� � �������� � �������� (�����������)";
                    _wordTable.Rows[2].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[0])
                    {
                        _wordTable.Rows[2].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[2].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[2].Cells[4].Range.Text = "7. ����������� ��������������� ����������� ������";
                    _wordTable.Rows[2].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[6])
                    {
                        _wordTable.Rows[2].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[2].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[3].Cells[1].Range.Text = "2. ����������������� ������� (�����������)";
                    _wordTable.Rows[3].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[1])
                    {
                        _wordTable.Rows[3].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[3].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[3].Cells[4].Range.Text = "8. ��������";
                    _wordTable.Rows[3].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[7])
                    {
                        _wordTable.Rows[3].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[3].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[4].Cells[1].Range.Text = "3. �������� ������� � ���� � ������������� ������������� (�����������)";
                    _wordTable.Rows[4].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[2])
                    {
                        _wordTable.Rows[4].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[4].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[4].Cells[4].Range.Text = "9. ������������� ������ ���������� � ����������� � ������� 3 � ����� ����";
                    _wordTable.Rows[4].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[8])
                    {
                        _wordTable.Rows[4].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[4].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[5].Cells[1].Range.Text = "4. ����� ���������������";
                    _wordTable.Rows[5].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[3])
                    {
                        _wordTable.Rows[5].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[5].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[5].Cells[4].Range.Text = "10. �������� ������";
                    _wordTable.Rows[5].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[9])
                    {
                        _wordTable.Rows[5].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[5].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[6].Cells[1].Range.Text = "5. ���������� ���������� ���";
                    _wordTable.Rows[6].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[4])
                    {
                        _wordTable.Rows[6].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[6].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[6].Cells[4].Range.Text = "11. ����� ����������";
                    _wordTable.Rows[6].Cells[4].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[10])
                    {
                        _wordTable.Rows[6].Cells[5].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[6].Cells[6].Range.Text = "x";
                    }

                    _wordTable.Rows[7].Cells[1].Range.Text = "6. ������� ��������";
                    _wordTable.Rows[7].Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    if (patientInfo.MedicalInspectionAnamneseCheckboxes[5])
                    {
                        _wordTable.Rows[7].Cells[2].Range.Text = "x";
                    }
                    else
                    {
                        _wordTable.Rows[7].Cells[3].Range.Text = "x";
                    }

                    _wordTable.Rows[7].Cells[4].Range.Text = "12. ���������������";
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
                    "St. praesens. ����� ���������: {0}. " +
                    "��������: {1}, ���������: {2}. �������: {3}. " +
                    "������ ������ � ������� ��������� (��� ���� �����������): {4}. " +
                    "���������� ������: {5}. ������������� ����: {6}.",
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
                    "��� {0} � ���. � ������ ������� {1}, {2}, �����: {3}. " +
                    "��� {4} � ���. ���� ������ {5}; ���� {6}. PS {7}.",
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
                    "�� {0}/{1} ��.��.��. ����� {2}. ���������� ������� {3}, " +
                    "����������� � ������� ������ {4}, ������������� {5}. " +
                    "Per rectum: {6}. ��������������� �����������: {7}. " +
                    "�������� �� ����� ����, ����������� {8}. �������� �������� � " +
                    "�������������� ����������� {9}.",
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
                    _paragraph.Range.Text = "����� �������� � �������� ������� ����������";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Size = 10;
                    _paragraph.Range.Text = string.Format(
                        "�������� ����:\t�������� / ��������� (F: 20-0-10): ��� - {0}, ���� � {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[0],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[1]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t��������/���������� (�: 20-0-20): ��� - {0}, ���� � {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[2],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[3]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "�������� ������:  ����������/�������� (S: 50-0-180): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[4],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[5]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   ���������/���������� (F: 180-0-0): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[6],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[7]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   �������������� ���������� � �������� (�: 30-0-135): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[8],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[9]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   ���. � ��. ������� ��� ���������� �� 90� ����� (R: 90-0-90): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[10],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[11]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t   ���. � ��. ������� ��� ����������� ����� (R: 65-0-70): ��� - {0}, ���� - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[12],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[13]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "�������� ������: ���������� � �������� (S: 0-0-150): ��� - {0}, ���� - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[14],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[15]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "����-�������� �������: ��������� � �������� (R: 90-0-90): ��� - {0}, ���� - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[16],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[17]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3, 4, 5 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "������������� ������:\t���������� � �������� (S: 70-0-80): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[18],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[19]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t��������� � ���������� (F: 25-0-55): ��� - {0}, ���� - {1}.",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[20],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[21]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "������� 1-�� ������:";
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3, 4, 5, 6 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t���:\t������� ��������� � ���������� (F: 35-0-15): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[22],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[23]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t�������� ��������� � ���������� (S: 40-0-0): ��� - {0}, ���� - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[24],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[25]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t���.\t���������� � �������� (S: 5-0-50): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[26],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[27]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2, 3 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t���.\t���������� � �������� (S: 15-0-85): ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[28],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[29]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2, 3 });

                    _waitForm.SetProgress(50);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t���������: {0} �����",
                        patientInfo.MedicalInspectionStLocalisPart1OppositionFinger);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "������� II-V-�� �������:";
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 1, 2, 3, 4, 5, 6, 7, 8 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t���: ���������� � �������� (S: 35-0-90): II�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[30],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[31]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t  III�. - ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[32],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[33]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t  IV�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[34],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[35]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t  V - ��� - {0}, ���� - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[36],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[37]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t��������� � ���������� (F: 30-0-25): II�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[38],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[39]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t       III�. - ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[40],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[41]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t       IV�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[42],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[43]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t       V - ��� - {0}, ���� - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[44],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[45]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t����: ���������� � �������� (S: 0-0-100): II�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[46],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[47]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t     III�. - ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[48],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[49]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t     IV�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[50],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[51]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t     V - ��� - {0}, ���� - {1}",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[52],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[53]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t����: ���������� � �������� (S: 0-0-80): II�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[54],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[55]);
                    SetWordsInRangeUnderline(_paragraph.Range, new[] { 2 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t   III�. - ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[56],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[57]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t   IV�. ��� - {0}, ���� - {1};",
                        patientInfo.MedicalInspectionStLocalisPart1Fields[58],
                        patientInfo.MedicalInspectionStLocalisPart1Fields[59]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "\t\t\t\t\t\t   V - ��� - {0}, ���� - {1}",
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
                    if (patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[1] == "���")
                    {
                        axisViolation = "���";
                    }
                    else
                    {
                        string rigthComboBox1Value = patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[1] == "�������"
                            ? "������"
                            : "������� �����";

                        axisViolation = string.Format(
                            "{0} {1} {2}",
                            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[2],
                            rigthComboBox1Value,
                            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[3]);
                    }

                    _paragraph.Range.Text = string.Format(
                        "�����: {0}. ���� {1}. ��������� ��� {2}.",
                        patientInfo.MedicalInspectionStLocalisPart2WhichHand,
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[0],
                        axisViolation);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "�����������: {0}, ����������: {1}, ���� ��� �������� �� ���: {2}, ���� ��� ���������: {3}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[0],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[1],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[2],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[3]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "�������������� �����������: {0}. ����������� �����������: {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[4],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[5]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "����: {0}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[6]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "����� ����: {0}. ���� ����: {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[4],
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[5]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "������� ���(�) {0}; {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[7],
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[6]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "������������: {0}. �������: {1}.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[7],
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[8]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "����� �� ������� �������: {0}.",
                        patientInfo.MedicalInspectionStLocalisPart2TextBoxes[9]);

                    _waitForm.SetProgress(70);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _paragraph.Range.Text = "���������� �����������:";

                    if (patientInfo.MedicalInspectionStLocalisPart2WhichHand == "������, �����" ||
                       patientInfo.MedicalInspectionStLocalisPart2WhichHand == "�����")
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        _paragraph.Range.Text = "����� �����";

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "�������� �������� ����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[0],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[1],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[2],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[3],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[4]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\t����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[5],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[6],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[7],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[8],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[9]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "�������� ���������� ����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[10],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[11],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[12],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[13],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[14]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\t����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[15],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[16],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[17],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[18],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[19]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "����������, ��������� I ������: {0}. ��������/���������� �������: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[20],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[21]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "���� ����: {0}; ����: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[22],
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand[23]);
                    }

                    _waitForm.SetProgress(80);

                    if (patientInfo.MedicalInspectionStLocalisPart2WhichHand == "������, �����" ||
                       patientInfo.MedicalInspectionStLocalisPart2WhichHand == "������")
                    {
                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        _paragraph.Range.Text = "������ �����";

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "�������� ��������  ����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[0],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[1],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[2],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[3],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[4]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\t\t����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[5],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[6],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[7],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[8],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[9]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "�������� ���������� ����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[10],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[11],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[12],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[13],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[14]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "\t\t\t  ����: I � {0}, II � {1}, III � {2}, IV � {3}, V � {4};",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[15],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[16],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[17],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[18],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[19]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "����������, ��������� I ������: {0}. ��������/���������� �������: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[20],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[21]);

                        _wordDoc.Paragraphs.Add(ref _missingObject);
                        _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                        _paragraph.Range.Text = string.Format(
                            "���� ����: {0}; ����: {1}.",
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[22],
                            patientInfo.MedicalInspectionStLocalisPart2RightHand[23]);
                    }

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "����������: {0}.",
                        patientInfo.MedicalInspectionStLocalisPart2ComboBoxes[8]);

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Format(
                        "������ �� ����: {0}. ������� ����� ��� ������������ �� {1} �������� ����� {2} ���.",
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

                _paragraph.Range.Text = "�������������� � ���� ���������: " + patientInfo.MedicalInspectionStLocalisRentgen + ".";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���� ���: " + patientInfo.MedicalInspectionTeoRisk + ".";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string expertAnamnes;
                if (patientInfo.MedicalInspectionExpertAnamnese == 1)
                {
                    expertAnamnes = string.Format(
                            "�/� ����� ����������� � {0} �� {1}, ����� ���� ������������������ {2}",                            
                            ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnWithNumberDateStart),
                            ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnWithNumberDateEnd),
                            ConvertEngine.GetDiffInDays(patientInfo.MedicalInspectionLnWithNumberDateEnd, patientInfo.MedicalInspectionLnWithNumberDateStart) + 1);
                }
                else if (patientInfo.MedicalInspectionExpertAnamnese == 2)
                {
                    expertAnamnes = string.Format(
                        "�/� ������ �������� � {0}",
                        ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnFirstDateStart));
                }
                else
                {
                    expertAnamnes = "�/� �� ���������.";
                }

                _paragraph.Range.Text = "���������� �������: " + expertAnamnes;

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                string[] diagnose = patientInfo.Diagnose.Split(new[] { "\r\n" }, 2, StringSplitOptions.None);
                _paragraph.Range.Text = "����������� �������: " + diagnose[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                if (diagnose.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnose[1];
                }

                AddEmptyParagraph();

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = string.Format(
                    "����:______________{0}                  �/�:_______________{1}",
                    patientInfo.DoctorInChargeOfTheCase,
                    globalSettings.BranchManager);

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
                    _paragraph.Range.Text = "���� ������������ � �������";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 0;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    _paragraph.Range.ListFormat.ApplyNumberDefault(ref _missingObject);
                    _paragraph.Range.Text = "������������: " + patientInfo.MedicalInspectionInspectionPlan + ".\r\n" +
                        "����������� �������.\r\n" +
                        "����������������� �������������� �������:\r\n";
                    _paragraph.Range.ListFormat.ApplyNumberDefaultOld();
                    _paragraph.Range.ListFormat.ApplyBulletDefault(ref _missingObject);
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = 0;
                    object index = 2;
                    _paragraph.Range.ParagraphFormat.TabStops.get_Item(ref index).Position = 50;
                    _paragraph.Range.Text = "��������������� �������: �����������, �����������\r\n" +
                        "���������, ���\r\n";
                    _paragraph.Range.ListFormat.ApplyBulletDefaultOld();
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = -18;
                    _paragraph.Range.Text = "4.\t������������ �����������.";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Empty;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "���� " + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate);
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "������� ���� " + patientInfo.DoctorInChargeOfTheCase;
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });
                    AddEmptyParagraph();
                }

                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        /// <summary>
        /// �������� ������ �� ��������. � ��������� �������, ������ ��� ���������� 2 ����
        /// </summary>
        /// <param name="operationInfo">������ �� ��������</param>
        /// <param name="patientInfo">������ � ��������</param>
        private static void InsertTableForOperation(OperationClass operationInfo, PatientClass patientInfo)
        {
            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Font.Bold = 1;
            _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            _paragraph.Range.Text = "��������";

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
                _wordRange = _wordDoc.Range(ref begCell, ref endCell);
                _wordRange.Select();
                _wordApp.Selection.Cells.Merge();
            }

            _wordTable.Rows[1].Cells[1].Range.Text = "������� " + patientInfo.LastName;
            SetWordsInRangeBold(_wordTable.Rows[1].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[2].Cells[1].Range.Text = "��� " + patientInfo.Name;
            SetWordsInRangeBold(_wordTable.Rows[2].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[3].Cells[1].Range.Text = "�������� " + patientInfo.Patronymic;
            SetWordsInRangeBold(_wordTable.Rows[3].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[4].Cells[1].Range.Text = "������� " + patientInfo.Age;
            SetWordsInRangeBold(_wordTable.Rows[4].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[5].Cells[1].Range.Text = "� ���. �-�� " + patientInfo.NumberOfCaseHistory;
            SetWordsInRangeBold(_wordTable.Rows[5].Cells[1].Range, new[] { 1, 2, 3, 4, 5, 6 });

            _wordTable.Rows[6].Cells[1].Range.Text = "���� ����������� " + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate);
            SetWordsInRangeBold(_wordTable.Rows[6].Cells[1].Range, new[] { 1, 2 });

            _wordTable.Rows[7].Cells[1].Range.Text = string.Format(
                "���� �������� {0} {1}-{2}",
                ConvertEngine.GetRightDateString(operationInfo.DataOfOperation),
                ConvertEngine.GetRightTimeString(operationInfo.StartTimeOfOperation),
                ConvertEngine.GetRightTimeString(operationInfo.EndTimeOfOperation));
            SetWordsInRangeBold(_wordTable.Rows[7].Cells[1].Range, new[] { 1, 2 });

            _wordTable.Rows[8].Cells[1].Range.Text = "����� " + patientInfo.GetAddress();
            SetWordsInRangeBold(_wordTable.Rows[8].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[9].Cells[1].Range.Text = "������� " + patientInfo.Diagnose;
            SetWordsInRangeBold(_wordTable.Rows[9].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[10].Cells[1].Range.Text = "�������� " + operationInfo.Name;
            SetWordsInRangeBold(_wordTable.Rows[10].Cells[1].Range, new[] { 1 });

            _wordTable.Rows[11].Cells[1].Range.Text = "��� ��������: " + operationInfo.OperationCourse;
            SetWordsInRangeBold(_wordTable.Rows[11].Cells[1].Range, new[] { 1, 2 });

            int rowNum = 1;
            _wordTable.Rows[rowNum].Cells[2].Range.Text = "������";
            _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

            foreach (string surgeoun in operationInfo.Surgeons)
            {
                _wordTable.Rows[rowNum].Cells[3].Range.Text = surgeoun;
                rowNum++;
            }

            if (operationInfo.Assistents.Count > 0)
            {
                _wordTable.Rows[rowNum].Cells[2].Range.Text = "���������";
                _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

                foreach (string assistent in operationInfo.Assistents)
                {
                    _wordTable.Rows[rowNum].Cells[3].Range.Text = assistent;
                    rowNum++;
                }
            }

            if (!string.IsNullOrEmpty(operationInfo.HeAnaesthetist))
            {
                _wordTable.Rows[rowNum].Cells[2].Range.Text = "������������";
                _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

                _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.HeAnaesthetist;
                rowNum++;
            }

            _wordTable.Rows[rowNum].Cells[2].Range.Text = "����. �/������";
            _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

            _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.ScrubNurse;
            rowNum++;

            if (!string.IsNullOrEmpty(operationInfo.SheAnaesthetist))
            {
                _wordTable.Rows[rowNum].Cells[2].Range.Text = "������������";
                _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

                _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.SheAnaesthetist;
                rowNum++;
            }

            _wordTable.Rows[rowNum].Cells[2].Range.Text = "�������";
            _wordTable.Rows[rowNum].Cells[2].Range.Font.Bold = 1;

            _wordTable.Rows[rowNum].Cells[3].Range.Text = operationInfo.Orderly;

            string surgeon = operationInfo.Surgeons.Count > 0 ? operationInfo.Surgeons[0] : "                          ";
            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Text = string.Format("���� {0}, {1}\t\t\t\t{2} _________________",
                operationInfo.DataOfOperation.ToString("dd.MM.yyyy"),
                operationInfo.EndTimeOfOperation.AddMinutes(15).ToString("HH:mm"),
                surgeon);
            SetWordsInRangeBold(_paragraph.Range, new[] { 1 });
        }

        /// <summary>
        /// �������������� � Word �������� ��������
        /// </summary>
        /// <param name="operationInfo">���������� �� ��������</param>
        /// <param name="patientInfo">���������� � ��������</param>
        /// <param name="globalSettings">���������� ���������</param>
        public static void ExportOperationProtocol(OperationClass operationInfo, PatientClass patientInfo, GlobalSettingsClass globalSettings)
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
                    // ������� ��� 2007 ����� ��������� ����� ���������� 2003 �����.
                    // ��� ������ ������, ��������, ��������� � �������, �� ��� ��� � ��
                    // ���� ������ ������.
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

                // �������� ����� ������������ � �������, ���� ����
                if (patientInfo.IsTreatmentPlanActiveInOperationProtocol)
                {
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 1;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    _paragraph.Range.Text = "���� ������������ � �������\r\n";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Font.Bold = 0;
                    _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                    _paragraph.Range.ListFormat.ApplyNumberDefault(ref _missingObject);
                    _paragraph.Range.Text = "������������: " + patientInfo.TreatmentPlanInspection + ".\r\n" +
                        "����������� �������.\r\n" +
                        "����������������� �������������� �������:\r\n";
                    _paragraph.Range.ListFormat.ApplyNumberDefaultOld();
                    _paragraph.Range.ListFormat.ApplyBulletDefault(ref _missingObject);
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = 0;
                    object index = 2;
                    _paragraph.Range.ParagraphFormat.TabStops.get_Item(ref index).Position = 50;
                    _paragraph.Range.Text = "��������������� �������: �����������, �����������\r\n" +
                        "���������, ���\r\n";
                    _paragraph.Range.ListFormat.ApplyBulletDefaultOld();
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = -18;
                    _paragraph.Range.Text = "4.\t������������ �����������.";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = string.Empty;
                    _paragraph.Range.ParagraphFormat.FirstLineIndent = 0;
                    _paragraph.Range.ParagraphFormat.LeftIndent = 0;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "���� " + ConvertEngine.GetRightDateString(patientInfo.TreatmentPlanDate);
                    SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "������� ���� " + patientInfo.DoctorInChargeOfTheCase;
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
                _paragraph.Range.Text = "���������������� �������";

                // �������� ����� ����� � �������
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
                    "���������� ���� � ����� ������������� �������, ���������� ������ � ����� ������������� ������� � ������� ������, �������� �������� ����������� (���������� ��� �������) �  ������������� ���������� ��������\r\n\r\n" +
                    "���������� ����������\r\n" +
                    "(������������� �������� ����)\r\n\r\n" +
                    "������� ���� (�������� ����)";

                _wordShape.TextFrame.TextRange.Paragraphs[1].Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                /*_wordShape.TextFrame.TextRange.Paragraphs[2].Range.Font.Size = 8;
                _wordShape.TextFrame.TextRange.Paragraphs[5].Range.Font.Size = 4;*/

                _waitForm.SetProgress(30);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Font.Bold = 0;
                _paragraph.Range.Text = "������ ���. ����������";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                _paragraph.Range.Text = "������� " + patientInfo.GetFullName() + ", " + patientInfo.Age + " ���";
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                if (operationInfo.BeforeOperationEpicrisisIsDairyEnabled)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "����������� ���� - " + operationInfo.BeforeOperationEpicrisisTemperature +
                        ". ������: " + operationInfo.BeforeOperationEpicrisisComplaints;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "��������� " + operationInfo.BeforeOperationEpicrisisState +
                        ". ����� " + operationInfo.BeforeOperationEpicrisisPulse +
                        " � ���., �� " + operationInfo.BeforeOperationEpicrisisADFirst + "/" +
                        operationInfo.BeforeOperationEpicrisisADSecond + " ��.��.��., ��� " +
                        operationInfo.BeforeOperationEpicrisisChDD + " � ���.";

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "� ������ ������� " + operationInfo.BeforeOperationEpicrisisBreath +
                        ", ����� - " + operationInfo.BeforeOperationEpicrisisWheeze;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "���� ������ " + operationInfo.BeforeOperationEpicrisisHeartSounds +
                        ", ���� " + operationInfo.BeforeOperationEpicrisisHeartRhythm +
                        ". ����� " + operationInfo.BeforeOperationEpicrisisStomach;

                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = "�������������� " + operationInfo.BeforeOperationEpicrisisUrination +
                        ". ���� " + operationInfo.BeforeOperationEpicrisisStool;

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
                _paragraph.Range.Text = "�������: " + diagnoseLines[0];
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                if (diagnoseLines.Length > 1)
                {
                    _wordDoc.Paragraphs.Add(ref _missingObject);
                    _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                    _paragraph.Range.Text = diagnoseLines[1];
                }

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "�������� ����������� �������. ����������� ��������: " + operationInfo.Name +
                    ".\r\n������� �������� �� ��������. ���������������� ���.\r\n";

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���� " + ConvertEngine.GetRightDateString(operationInfo.DataOfOperation) +
                    ", " + ConvertEngine.GetRightTimeString(operationInfo.BeforeOperationEpicrisisTimeWriting);
                SetWordsInRangeBold(_paragraph.Range, new[] { 1 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "���. ����������\t\t\t\t\t\t\t" + globalSettings.BranchManager;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2, 3 });

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                _paragraph.Range.Text = "������� ����\t\t\t\t\t\t\t" + patientInfo.DoctorInChargeOfTheCase;
                SetWordsInRangeBold(_paragraph.Range, new[] { 1, 2 });

                AddEmptyParagraph();

                _waitForm.SetProgress(50);

                InsertTableForOperation(operationInfo, patientInfo);

                _waitForm.SetProgress(75);

                _wordDoc.Paragraphs.Add(ref _missingObject);
                _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
                object typeBreak = WdBreakType.wdPageBreak;
                _paragraph.Range.InsertBreak(ref typeBreak);

                InsertTableForOperation(operationInfo, patientInfo);
 
                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }


        /// <summary>
        /// ������� � ����� ��������� �������� � �������� ������ ����������� �������� � �������
        /// </summary>
        /// <param name="filePath">���� �� �����</param>
        /// <param name="patientInfo">���������� � ��������</param>        
        /// /// <param name="globalSettings">���������� ���������</param>
        public static void ExportAdditionalDocument(object filePath, PatientClass patientInfo,GlobalSettingsClass globalSettings)
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
                        patientInfo,
                        globalSettings);
                }

                foreach (Shape shape in _wordDoc.Shapes)
                {
                    FindMarkAndReplace(
                        shape.TextFrame.TextRange.Text,
                        shape,
                        shift,
                        ref previousValue,
                        ref currentValue,
                        patientInfo,                        
                        globalSettings);
                }

                _waitForm.SetProgress(100);

                // ��������� � ������ ���������
                object unit = WdUnits.wdStory;
                object extend = WdMovementType.wdMove;
                _wordApp.Selection.HomeKey(ref unit, ref extend);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _waitForm.CloseForm();

                ReleaseComObject();

                Thread.CurrentThread.CurrentCulture = oldCi;
            }
        }

        private static void FindMarkAndReplace(
           string rangeText,
           Shape shape,
           double shift,
           ref double previousValue,
           ref double currentValue,
           PatientClass patientInfo,
           GlobalSettingsClass globalSettings)
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
                    string bracketNewText = GetRealParameterInsteadSpecialMark(bracketText, patientInfo, globalSettings);

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

        private static int GetOperationNumber(PatientClass patientInfo, string mark, string name)
        {
            // �������� ����� ��������, ������������ � 1
            int number = Convert.ToInt32(mark.Substring(name.Length).Trim(' '));
            if (patientInfo.Operations.Count < number)
            {
                throw new Exception("��� �������� � ������� " + number);
            }

            // ���������� ����� ��������, ������������ � 0
            return number - 1;
        }

        /// <summary>
        /// ������� ������ �������� ������ ��������� � ���������
        /// </summary>
        /// <param name="mark">����� � ���������</param>
        /// <param name="patientInfo">���������� � ��������</param>        
        /// <param name="globalSettings">���������� ���������</param>
        /// <returns></returns>
        private static string GetRealParameterInsteadSpecialMark(string mark, PatientClass patientInfo, GlobalSettingsClass globalSettings)
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
                if (mark.StartsWith("���� ��������"))
                {
                    operationNumber = GetOperationNumber(patientInfo, mark, "���� ��������");
                    CheckOperationNumber(patientInfo, operationNumber);
                    return patientInfo.Operations[operationNumber].DataOfOperation.ToString("dd.MM.yyyy");
                }

                if (mark.StartsWith("�������� ��������"))
                {
                    operationNumber = GetOperationNumber(patientInfo, mark, "�������� ��������");
                    CheckOperationNumber(patientInfo, operationNumber);
                    return patientInfo.Operations[operationNumber].Name;
                }
            }
            catch (Exception ex)
            {
                return "{" + ex.Message.ToUpper() + "}";
            }

            switch (mark)
            {
                case "��� ��������":
                    return patientInfo.GetFullName();
                case "�������":
                    return patientInfo.Age.ToString();
                case "�����":
                    return patientInfo.GetAddress();
                case "���� �����������":
                    return patientInfo.DeliveryDate.ToString("dd.MM.yyyy");
                case "���� �������":
                    if (patientInfo.ReleaseDate.HasValue)
                    {
                        return patientInfo.ReleaseDate.Value.ToString("dd.MM.yyyy");
                    }

                    return "{���� ������� �� �������}";
                case "�������":
                    return patientInfo.Diagnose;
                case "�������������� �������":
                    return patientInfo.DischargeEpicrisConservativeTherapy;
                case "� ��":
                    return patientInfo.NumberOfCaseHistory;
                case "� ���������":
                    return globalSettings.DepartmentName;
                case "��� �������� �����":
                    return patientInfo.DoctorInChargeOfTheCase;
                case "���":
                    return patientInfo.MKB;
                case "���":
                    return patientInfo.KSG;
                case "������":
                    return patientInfo.WorkPlace;
                case "��� ���. ����������":
                    return globalSettings.BranchManager;
                case "c���������� ����":
                    return DateTime.Now.ToString("dd.MM.yyyy");
                case "������":
                    if (patientInfo.Operations.Count > 0)
                    {
                        return ConvertEngine.ListToString(patientInfo.Operations[0].Surgeons, ",");
                    }

                    return "{������ �� ������, �.�. ��� ��������}";
                case "������������":
                    if (patientInfo.Operations.Count > 0)
                    {
                        return patientInfo.Operations[0].HeAnaesthetist;
                    }

                    return "{������������ �� ������, �.�. ��� ��������}";
                default:
                    return "{" + mark.ToUpper() + "}";                    
            }
        }

        private static void CheckOperationNumber(PatientClass patientInfo, int realOperationNumber)
        {
            if (patientInfo.Operations.Count <= realOperationNumber)
            {
                throw new Exception("��� �������� � ������� " + (realOperationNumber + 1));
            }
        }

        /// <summary>
        /// ����� � �������� � ���������
        /// </summary>
        /// <param name="findText">����� ��� ������</param>
        /// <param name="replaceText">����� ��� ������</param>
        private static void FindAndReplace(string findText, string replaceText)
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
        /// ���������� ������� ����� ��������� ���������
        /// </summary>
        private static void ReleaseComObject()
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
        /// ��������� � ������� �������� ������ ��������
        /// </summary>
        private static void AddEmptyParagraph()
        {
            _wordDoc.Paragraphs.Add(ref _missingObject);
            _paragraph = _wordDoc.Paragraphs[_wordDoc.Paragraphs.Count];
            _paragraph.Range.Bold = 0;
            _paragraph.Range.Text = string.Empty;
        }

        /// <summary>
        /// ������� ������ ���������� ������ ���� � ������� Range
        /// </summary>
        /// <param name="range">Range, � ������� �������� �����</param>
        /// <param name="wordNumbers">������ ���� � Range, ������� ���� ������� �������</param>
        private static void SetWordsInRangeBold(Range range, IEnumerable<int> wordNumbers)
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
        /// ������� ������������� ���������� ������ ���� � ������� Range
        /// </summary>
        /// <param name="range">Range, � ������� �������� �����</param>
        /// <param name="wordNumbers">������ ���� � Range, ������� ���� ������� �������</param>
        private static void SetWordsInRangeUnderline(Range range, IEnumerable<int> wordNumbers)
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
