using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Notepad
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Информация с данными для сохранения в реестре
        /// </summary>
        private ParametersInfo m_ParamInfo;

        /// <summary>
        /// Информация с данными для сохранения в реестре
        /// </summary>
        private PrintInfo m_PrintInfo;

        /// <summary>
        /// Путь к текущему файлу
        /// </summary>
        private string m_FilePath = "";

        /// <summary>
        /// Было ли изменение текста после последнего сохранения (загрузки) файла
        /// </summary>
        private bool m_IsTextChange;

        /// <summary>
        /// Файл с различными функциями
        /// </summary>
        private readonly ActionClass m_ActClass;

        /// <summary>
        /// Класс для ведения истории
        /// </summary>   
        private HistoryClass m_HistoryClass;

        /// <summary>
        /// Указатель на форму поиска/замены
        /// </summary>
        private FindForm m_FindForm;

        /// <summary>
        /// Указатель на форму быстрой замены
        /// </summary>
        private QuickReplaceForm m_QuickReplForm;

        /// <summary>
        /// Указатель на форму конвертации
        /// </summary>
        private ConvertForm m_ConvertForm;

        /// <summary>
        /// Указатель на форму с установленными программами
        /// </summary>
        private UninstallForm m_UninstallForm;

        /// <summary>
        /// Указатель на форму с установленными программами
        /// </summary>
        private ProcessForm m_ProcessForm;

        /// <summary>
        /// Разрешение обработки событий типа ХХХ_Changed
        /// </summary>
        private bool m_AppStart;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);


        /// <summary>
        /// Реальное количество строк в textWindow
        /// </summary>
        private string LineCount
        {
            get
            {
                //Message msg = Message.Create(textWindow.Handle, 0x00BA, IntPtr.Zero, IntPtr.Zero);
                //this.WndProc(ref msg);
                //int res = msg.Result.ToInt32();
                int res = SendMessage(textWindow.Handle, 0x00BA/*EM_GETLINECOUNT*/, 0, 0);
                return res.ToString();
            }
        }


        public MainForm(string[] args)
        {
            m_IsTextChange = false;
            InitializeComponent();
            m_ActClass = new ActionClass();
            m_ActClass.LoadParameter(out m_ParamInfo);
            m_ActClass.LoadParameter(out m_PrintInfo);

            for (int i = 0; i < args.Length; i++)
                m_FilePath += args[i] + " ";
        }


        /// <summary>
        /// Установка настроек принтера
        /// </summary>
        private void PrintSet()
        {
            try
            {
                printDocument1.DefaultPageSettings.Margins = m_PrintInfo.Margins;
                if (m_PrintInfo.PaperNumber < pageSetupDialog1.PageSettings.PrinterSettings.PaperSizes.Count)
                {
                    printDocument1.DefaultPageSettings.PaperSize = pageSetupDialog1.PageSettings.PrinterSettings.PaperSizes[m_PrintInfo.PaperNumber];
                }
                printDocument1.DefaultPageSettings.Landscape = m_PrintInfo.Landscape;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {

            }
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            textWindow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            Location = m_ParamInfo.MfLocation;
            Size = m_ParamInfo.MfSize;
            WindowState = m_ParamInfo.MfWindState;
            textWindow.WordWrap = menuEditWrap.Checked = m_ParamInfo.WordWrap;
            menuEditWriteHistory.Checked = m_ParamInfo.HistoryWrite;
            textWindow.Font = m_ParamInfo.TextFont;
            textWindow.ForeColor = m_ParamInfo.TextForeColor;

            var newThread = new Thread(PrintSet);
            newThread.Start();

            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.KeyboardLayoutId == m_ParamInfo.KeyboardLayoutId)
                {
                    InputLanguage.CurrentInputLanguage = lang;
                    break;
                }
            }

            if (m_FilePath != "")
            {
                LoadFile();
                Text = "Блокнот - " + m_FilePath.Substring(m_FilePath.LastIndexOf('\\') + 1);
            }
            SetTextChange(false);
            m_AppStart = true;
            menuHistoryWrite_CheckedChanged(null, null);
            textWindow.Focus();
            ShowPosition();
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            
        }


        /// <summary>
        /// Отлавливание смена языка ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            if (!m_AppStart)
                return;
            m_ParamInfo.KeyboardLayoutId = InputLanguage.CurrentInputLanguage.Culture.KeyboardLayoutId;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Устанавливает новое значение переменной isTextChange и показывает или скрывает слово "Изменён"
        /// в строке состояния
        /// </summary>
        /// <param name="state"></param>
        private void SetTextChange(bool state)
        {
            statusStrip1.Items[2].Visible = m_IsTextChange = state;
        }


        /// <summary>
        /// Показывает диалог сохранения текущего документа при его закрытии
        /// </summary>
        /// <returns></returns>
        private bool SaveChange()
        {
            try
            {
                if (m_IsTextChange)
                {
                    DialogResult res = MessageBox.Show("Текущий документ будет закрыт. Сохранить изменения?", "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        if (m_FilePath == "")
                        {
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                m_FilePath = saveFileDialog1.FileName;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        try
                        {
                            var sw = new StreamWriter(m_FilePath, false, Encoding.GetEncoding("windows-1251"));
                            sw.Write(textWindow.Text);
                            SetTextChange(false);
                            sw.Close();
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (res == DialogResult.Cancel)
                        return false;
                }
                return true;
            }
            finally
            {
                MainForm_InputLanguageChanged(null, null);
            }
        }


        /// <summary>
        /// Новый
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileNew_Click(object sender, EventArgs e)
        {
            if (!SaveChange())
            {
                return;
            }

            textWindow.Text = "";
            m_FilePath = "";
            SetTextChange(false);
            Text = "Блокнот";
            textWindow.SelectionStart = 0;
        }


        /// <summary>
        /// Загрузка файла в textWindow
        /// </summary>
        private void LoadFile()
        {
            try
            {
                var sr = new StreamReader(m_FilePath, Encoding.GetEncoding("windows-1251"));
                // Обрабатываем данные для 
                // '\n' -> '\r\n'
                // '\r' -> '\r\n'
                // '\0' -> ''
                // '\b' -> ''
                var fi = new FileInfo(m_FilePath);
                var buff = new char[fi.Length];
                sr.ReadBlock(buff, 0, (int)fi.Length);
                sr.Close();
                var s = new StringBuilder("");
                for (int i = 0; i < buff.Length; i++)
                {
                    if (buff[i] != '\0' && buff[i] != '\b')
                    {
                        if (buff[i] == '\n')
                        {
                            if (i == 0)
                            {
                                s.Append('\r');
                            }
                            else if (buff[i - 1] != '\r')
                            {
                                s.Append('\r');
                            }
                        }
                        s.Append(buff[i]);
                        if (buff[i] == '\r')
                        {
                            if (i == buff.Length)
                            {
                                s.Append('\n');
                            }
                            else if (buff[i + 1] != '\n')
                            {
                                s.Append('\n');
                            }
                        }
                    }
                }
                textWindow.Text = s.ToString();
                textWindow.SelectionStart = 0;
                SetTextChange(false);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Открыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            if (!SaveChange())
            {
                return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    m_FilePath = openFileDialog1.FileName;
                    LoadFile();
                    Text = "Блокнот - " + m_FilePath.Substring(m_FilePath.LastIndexOf('\\') + 1);
                    SetTextChange(false);
                }
                else
                {
                    MessageBox.Show("Файл не найден", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            MainForm_InputLanguageChanged(null, null);
        }


        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_FilePath == "")
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        m_FilePath = saveFileDialog1.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                try
                {
                    //
                    // Смена аттрибута, если он только для чтения
                    //
                    var fi = new FileInfo(m_FilePath);
                    FileAttributes saveFileAttrib = fi.Attributes;

                    if (fi.Attributes.ToString().Contains(FileAttributes.ReadOnly.ToString()))
                    {
                        if (MessageBox.Show("Файл помечен аттрибутом \"Только для чтения\". Перезаписать?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            fi.Attributes = FileAttributes.Normal;
                        }
                        else
                        {
                            return;
                        }
                    }

                    //
                    // Сохранение файла
                    //
                    using (var sw = new StreamWriter(m_FilePath, false, Encoding.GetEncoding("windows-1251")))
                    {
                        sw.Write(textWindow.Text);
                    }

                    //
                    // Возврат аттрибута только для чтения, если он был изменён
                    //
                    if (saveFileAttrib != fi.Attributes)
                    {
                        fi.Attributes = saveFileAttrib;
                    }

                    Text = "Блокнот - " + m_FilePath.Substring(m_FilePath.LastIndexOf('\\') + 1);
                    SetTextChange(false);

                    var hisInfo = new HistoryInfo
                    {
                        SelectionStart = textWindow.SelectionStart,
                        SelectionLength = textWindow.SelectionLength,
                        ScrinText = textWindow.Text,
                        VertScrollPosition = GetScrollPos(textWindow.Handle, 1 /* SB_VERT */),
                        HorizScrollPosition = GetScrollPos(textWindow.Handle, 0 /* SB_HORIZ */)
                    };
                    m_HistoryClass.Dispose();
                    m_HistoryClass = new HistoryClass(hisInfo);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                MainForm_InputLanguageChanged(null, null);
            }
        }


        /// <summary>
        /// Сохранить как...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            if (m_FilePath != "")
            {
                saveFileDialog1.FileName = m_FilePath.Substring(m_FilePath.LastIndexOf("\\") + 1);
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_FilePath = saveFileDialog1.FileName;
                try
                {
                    var sw = new StreamWriter(m_FilePath, false, Encoding.GetEncoding("windows-1251"));
                    sw.Write(textWindow.Text);
                    sw.Close();
                    Text = "Блокнот - " + m_FilePath.Substring(m_FilePath.LastIndexOf('\\') + 1);
                    SetTextChange(false);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            MainForm_InputLanguageChanged(null, null);
        }


        /// <summary>
        /// Включение/выключение переноса строк
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionWrap_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
                return;
            m_ParamInfo.WordWrap = textWindow.WordWrap = menuEditWrap.Checked;
            m_ActClass.SaveParameter(m_ParamInfo);
            ShowPosition();
        }


        /// <summary>
        /// Показ формы для поиска/замены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionFindReplace_Click(object sender, EventArgs e)
        {
            var tsmi = (ToolStripMenuItem)sender;
            TypeFindForm typeForm = tsmi.Name == menuActionFind.Name ? TypeFindForm.Find : TypeFindForm.Replace;
            if (m_FindForm == null || m_FindForm.IsDisposed)
            {
                m_FindForm = new FindForm(typeForm, this);
                m_FindForm.Show();
            }
            else
            {
                m_FindForm.SetType(typeForm);
                m_FindForm.Focus();
            }

        }


        /// <summary>
        /// Вызов окна с хитрой заменой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionQuickReplace_Click(object sender, EventArgs e)
        {
            if (m_QuickReplForm == null || m_QuickReplForm.IsDisposed)
            {
                m_QuickReplForm = new QuickReplaceForm(this);
                m_QuickReplForm.Show();
            }
            else
            {
                m_QuickReplForm.Focus();
            }
        }


        /// <summary>
        /// Изменение размеров формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
                return;
            ShowPosition();
            m_ParamInfo.MfSize = Size;
            m_ParamInfo.MfWindState = WindowState;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Изменение местоположения формы на экране
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
                return;
            m_ParamInfo.MfLocation = Location;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveChange())
            {
                e.Cancel = true;
            }
        }


        /// <summary>
        /// Смена ведения истории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHistoryWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
                return;
            m_ParamInfo.HistoryWrite = menuEditWriteHistory.Checked;
            m_ActClass.SaveParameter(m_ParamInfo);

            var hisInfo = new HistoryInfo
            {
                SelectionStart = textWindow.SelectionStart,
                SelectionLength = textWindow.SelectionLength,
                ScrinText = textWindow.Text,
                VertScrollPosition = GetScrollPos(textWindow.Handle, 1 /* SB_VERT */),
                HorizScrollPosition = GetScrollPos(textWindow.Handle, 0 /* SB_HORIZ */)
            };
            m_HistoryClass = new HistoryClass(hisInfo);
        }


        /// <summary>
        /// Конвертация текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionConvert_Click(object sender, EventArgs e)
        {
            if (m_ConvertForm == null || m_ConvertForm.IsDisposed)
            {
                m_ConvertForm = new ConvertForm(this);
                m_ConvertForm.Show();
            }
            else
            {
                m_ConvertForm.Focus();
            }
        }


        /// <summary>
        /// Устанавливает возможность отмены/возрата изменений
        /// </summary>
        private void SetViewHistory()
        {
            menuEditUndo.Enabled = m_HistoryClass.IsUndoValid();
            menuEditRedo.Enabled = m_HistoryClass.IsRedoValid();
        }


        [DllImport("User32.dll")]
        extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        extern static int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);


        /// <summary>
        /// Изменение текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textWindow_TextChanged(object sender, EventArgs e)
        {
            if (!m_AppStart || !m_ParamInfo.HistoryWrite)
                return;
            SetTextChange(true);
            ShowPosition();

            var hisInfo = new HistoryInfo
            {
                SelectionStart = textWindow.SelectionStart,
                SelectionLength = textWindow.SelectionLength,
                ScrinText = textWindow.Text,
                VertScrollPosition = GetScrollPos(textWindow.Handle, SB_VERT),
                HorizScrollPosition = GetScrollPos(textWindow.Handle, SB_HORIZ)
            };
            try
            {
                m_HistoryClass.AddNode(hisInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                menuEditWriteHistory.Checked = false;
            }
            SetViewHistory();
        }

        // ReSharper disable InconsistentNaming
        const int WM_VSCROLL = 0x0115;
        /*
                const int WM_HSCROLL = 0x0114;
        */

        const int SB_HORIZ = 0;
        const int SB_VERT = 1;

        const int SB_LINEDOWN = 1;
        // ReSharper restore InconsistentNaming

        /*
                [DllImport("coredll")]
                extern static int SendMessage(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);        
        */


        /// <summary>
        /// Отменить изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHistoryUndo_Click(object sender, EventArgs e)
        {
            HistoryInfo hisInfo = m_HistoryClass.Undo();
            if (!hisInfo.Valid || !m_ParamInfo.HistoryWrite)
                return;

            m_AppStart = false;
            textWindow.Text = hisInfo.ScrinText;
            textWindow.SelectionStart = hisInfo.SelectionStart;
            textWindow.SelectionLength = hisInfo.SelectionLength;

            // Наиболее подходящий способ
            textWindow.ScrollToCaret();
            int gVert = GetScrollPos(textWindow.Handle, SB_VERT);
            for (int i = 0; i < hisInfo.VertScrollPosition - gVert; i++)
            {
                SendMessage(textWindow.Handle, WM_VSCROLL, SB_LINEDOWN, 0);
            }

            SetScrollPos(textWindow.Handle, SB_HORIZ, hisInfo.HorizScrollPosition, true);

            m_AppStart = true;
            SetViewHistory();
        }


        /// <summary>
        /// Вернуть изменение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHistoryRedo_Click(object sender, EventArgs e)
        {
            HistoryInfo hisInfo = m_HistoryClass.Redo();
            if (!hisInfo.Valid || !m_ParamInfo.HistoryWrite)
                return;

            m_AppStart = false;
            textWindow.Text = hisInfo.ScrinText;
            textWindow.SelectionStart = hisInfo.SelectionStart;
            textWindow.SelectionLength = hisInfo.SelectionLength;
            textWindow.ScrollToCaret();
            SetScrollPos(textWindow.Handle, 1 /* SB_VERT */, hisInfo.VertScrollPosition, true);
            SetScrollPos(textWindow.Handle, 0 /* SB_HORIZ */, hisInfo.HorizScrollPosition, true);
            m_AppStart = true;
            SetViewHistory();
        }


        /// <summary>
        /// Показать окно с информацие о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuInformAbout_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm(this);
            aboutForm.ShowDialog();
        }


        /// <summary>
        /// Показать текущие координаты столбца и строки
        /// </summary>
        public void ShowPosition()
        {
            int line = 0;
            try
            {
                line = textWindow.GetLineFromCharIndex(textWindow.SelectionStart);
                if (textWindow.Lines.Length == 0)
                {

                    statusStrip1.Items[0].Text = "Строка: 1/1";
                    statusStrip1.Items[1].Text = "Столбец: 0/0";
                    return;
                }

                statusStrip1.Items[0].Text = "Строка: " + (line + 1) + "/" + LineCount;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {

            }

            try
            {
                int a1 = textWindow.GetFirstCharIndexFromLine(line);
                int pos = textWindow.SelectionStart - a1;
                if (pos < 0)
                    pos = textWindow.SelectionStart;

                statusStrip1.Items[1].Text = "Столбец: " + pos;
                int a2 = textWindow.GetFirstCharIndexFromLine(line + 1);
                int dec = 2;
                if (a2 < 0 && textWindow.SelectionStart < 300000)
                {
                    TextBox tb = CopyTextBox(textWindow);
                    tb.Text = textWindow.Text + "\r\n";
                    a2 = tb.GetFirstCharIndexFromLine(line + 1);
                    tb.Dispose();
                }
                else if (textWindow.Text[a2 - 1] != '\n')
                    dec = 1;
                int posLast = a2 - a1 - dec;
                if (posLast >= 0)
                    statusStrip1.Items[1].Text += "/" + posLast;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {

            }
        }


        /// <summary>
        /// Создаёт новый TextBox на основе переданного
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private static TextBox CopyTextBox(TextBoxBase textBox)
        {
            var tb = new TextBox
            {
                Multiline = true,
                WordWrap = textBox.WordWrap,
                Size = textBox.Size,
                Font = textBox.Font,
                ForeColor = textBox.ForeColor
            };
            return tb;
        }


        /// <summary>
        /// Строка для перехода
        /// </summary>
        public int GoToLine;


        /// <summary>
        /// Перейти на указанную строку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionGoTo_Click(object sender, EventArgs e)
        {
            var gotoForm = new GoToForm(this, Convert.ToInt32(LineCount));
            gotoForm.ShowDialog();

            if (GoToLine < 1)
            {
                return;
            }
            GoToLine--;

            int max = textWindow.Text.Length;
            int step = max / 10;
            int line = 0;
            int ind = 0;
            while (line != GoToLine)
            {
                ind += step;
                line = textWindow.GetLineFromCharIndex(ind);
                if (line > GoToLine)
                {
                    ind -= step;
                    step /= 10;
                    if (step == 0)
                    {
                        step++;
                    }
                }
            }

            textWindow.SelectionStart = textWindow.GetFirstCharIndexFromLine(line);
            textWindow.ScrollToCaret();
        }


        /// <summary>
        /// Показать положение курсора при кликанье мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textWindow_MouseUp(object sender, MouseEventArgs e)
        {
            ShowPosition();
        }
        private void textWindow_MouseDown(object sender, MouseEventArgs e)
        {
            ShowPosition();
        }


        /// <summary>
        /// Показать положение курсора при нажатии клавиш
        /// </summary>        
        /// <returns></returns>
        private void textWindow_KeyUp(object sender, KeyEventArgs e)
        {
            ShowPosition();
        }
        private void textWindow_KeyDown(object sender, KeyEventArgs e)
        {
            ShowPosition();
        }


        /// <summary>
        /// Изменить шрифт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHistoryFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = m_ParamInfo.TextFont;
            fontDialog1.Color = m_ParamInfo.TextForeColor;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                m_ParamInfo.TextFont = textWindow.Font = fontDialog1.Font;
                m_ParamInfo.TextForeColor = textWindow.ForeColor = fontDialog1.Color;
                m_ActClass.SaveParameter(m_ParamInfo);
            }
            MainForm_InputLanguageChanged(null, null);
        }


        /// <summary>
        /// Печать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFilePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    //printDocument1.PrinterSettings = printDialog1.PrinterSettings;        
                    try
                    {
                        printDocument1.Print();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                MainForm_InputLanguageChanged(null, null);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Номер обрабатываемой строки в textWindow
        /// </summary>
        private int m_TextLineNumber;


        /// <summary>
        /// Рисование текста для печати
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs ev)
        {
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            // Calculate the number of lines per page.
            float linesPerPage = ev.MarginBounds.Height / textWindow.Font.GetHeight(ev.Graphics);

            // Print each line of the file.
            while (count < linesPerPage && m_TextLineNumber < textWindow.Lines.Length)
            {
                SizeF lineSize = ev.Graphics.MeasureString(textWindow.Lines[m_TextLineNumber], textWindow.Font);
                int len = 1;
                if (lineSize.Width != 0)
                {
                    len = (int)(ev.MarginBounds.Width / lineSize.Width * textWindow.Lines[m_TextLineNumber].Length);
                }
                int step = 0;
                int lenTextString = textWindow.Lines[m_TextLineNumber].Length;
                if (lenTextString == 0)
                {
                    lenTextString++;
                }
                while (step < lenTextString)
                {
                    float yPos = topMargin + (count * textWindow.Font.GetHeight(ev.Graphics));
                    if (step + len > textWindow.Lines[m_TextLineNumber].Length)
                        ev.Graphics.DrawString(textWindow.Lines[m_TextLineNumber].Substring(step), textWindow.Font, new SolidBrush(textWindow.ForeColor), leftMargin, yPos, new StringFormat());
                    else
                        ev.Graphics.DrawString(textWindow.Lines[m_TextLineNumber].Substring(step, len), textWindow.Font, new SolidBrush(textWindow.ForeColor), leftMargin, yPos, new StringFormat());
                    count++;
                    step += len;
                }
                m_TextLineNumber++;
            }

            if (m_TextLineNumber < textWindow.Lines.Length)
            {
                ev.HasMorePages = true;
            }
            else
            {
                ev.HasMorePages = false;
                m_TextLineNumber = 0;
            }
        }


        /// <summary>
        /// Предварительный просмотр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFilePreview_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                string fileName = "";
                if (m_FilePath.LastIndexOf("\\") != -1)
                {
                    fileName = " - " + m_FilePath.Substring(m_FilePath.LastIndexOf("\\") + 1);
                }
                printPreviewDialog1.Text = "Предварительный просмотр" + fileName;
                printPreviewDialog1.Icon = Icon;
                printPreviewDialog1.ShowDialog();
                MainForm_InputLanguageChanged(null, null);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Параметры страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFilePageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                pageSetupDialog1.PageSettings = printDocument1.DefaultPageSettings;
                if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
                {
                    m_PrintInfo.Margins = pageSetupDialog1.PageSettings.Margins;
                    int n = 4; //A4
                    PrinterSettings.PaperSizeCollection psCol = pageSetupDialog1.PageSettings.PrinterSettings.PaperSizes;
                    PaperKind pk = pageSetupDialog1.PageSettings.PaperSize.Kind;
                    for (int i = 0; i < psCol.Count; i++)
                    {
                        if (psCol[i].Kind == pk)
                        {
                            n = i;
                            break;
                        }
                    }
                    m_PrintInfo.PaperNumber = n;
                    m_PrintInfo.Landscape = pageSetupDialog1.PageSettings.Landscape;
                    m_ActClass.SaveParameter(m_PrintInfo);
                }
                MainForm_InputLanguageChanged(null, null);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Копировать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copy_Click(object sender, EventArgs e)
        {
            textWindow.Copy();
        }


        /// <summary>
        /// Вырезать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cut_Click(object sender, EventArgs e)
        {
            textWindow.Cut();
            textWindow.ScrollToCaret();
        }


        /// <summary>
        /// Вставить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paste_Click(object sender, EventArgs e)
        {
            textWindow.Paste();
            textWindow.ScrollToCaret();
        }


        /// <summary>
        /// Выделить всё
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAll_Click(object sender, EventArgs e)
        {
            textWindow.SelectAll();
            textWindow.ScrollToCaret();
        }


        /// <summary>
        /// Изменение вида курсора при перетаскивании на форму внешнего файла или текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public int SelectItem;

        /// <summary>
        /// Копирование данных из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textWindow_DragDrop(object sender, DragEventArgs e)
        {
            Focus();
            /*this.BringToFront();
            Application.DoEvents();*/
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //
                // Получаем из перетащенных папок и фалов общий список всех файлов
                //
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

                var filePathsList = new ArrayList();
                for (int i = 0; i < paths.Length; i++)
                {
                    if (Directory.Exists(paths[i]))
                    {
                        string[] files = Directory.GetFiles(paths[i]);
                        for (int j = 0; j < files.Length; j++)
                        {
                            filePathsList.Add(files[i]);
                        }
                    }
                    else
                    {
                        filePathsList.Add(paths[i]);
                    }
                }

                var filePaths = new string[filePathsList.Count];
                filePathsList.CopyTo(filePaths);

                //
                // Если файлов больше одного - то даём пользователю возможность выбрать один нужный
                //
                if (filePaths.Length == 1)
                {
                    SelectItem = 0;
                }
                else
                {
                    if (filePaths.Length > 30)
                    {
                        MessageBox.Show("Слишком много файлов", "Невозможное действие", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    SelectItem = -1;
                    var selForm = new SelectDropFileForm(this, filePaths);
                    selForm.ShowDialog();

                    if (SelectItem == -1)
                    {
                        return;
                    }
                }

                //
                // Загружаем этот файл
                //
                if (!SaveChange())
                {
                    return;
                }

                m_FilePath = filePaths[SelectItem];
                LoadFile();
                Text = "Блокнот - " + m_FilePath.Substring(m_FilePath.LastIndexOf('\\') + 1);
                SetTextChange(false);
                MainForm_InputLanguageChanged(null, null);

            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                var text = (string)e.Data.GetData(DataFormats.Text);

                textWindow.SelectedText = text;
            }
        }


        /// <summary>
        /// Показать форму с установленными программами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionUninstall_Click(object sender, EventArgs e)
        {
            if (m_UninstallForm == null || m_UninstallForm.IsDisposed)
            {
                m_UninstallForm = new UninstallForm(this);
                m_UninstallForm.Show();
            }
            else
            {
                m_UninstallForm.Focus();
            }
        }


        /// <summary>
        /// Работа с процессами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionProcess_Click(object sender, EventArgs e)
        {
            if (m_ProcessForm == null || m_ProcessForm.IsDisposed)
            {
                m_ProcessForm = new ProcessForm(this);
                m_ProcessForm.Show();
            }
            else
            {
                m_ProcessForm.Focus();
            }            
        }


        /// <summary>
        /// Изменение аттрибутов файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuEditAttrib_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(m_FilePath))
            {
                MessageBox.Show("Не выбран рабочий файл", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var attribForm = new AttributesForm(m_FilePath);
            attribForm.ShowDialog();
        }

        /// <summary>
        /// Работа с неправильными гуидами (компонентами Windows Installer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionInvolvedGuids_Click(object sender, EventArgs e)
        {
            var guidForm = new GuidForm();
            guidForm.ShowDialog();
        }


        /// <summary>
        /// Работа с гуидами в нескольких файлах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionReplaceGuids_Click(object sender, EventArgs e)
        {
            new ReplaceFilesTextForm().ShowDialog();
        }


        /// <summary>
        /// Работа с zlib архивами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionZlibArchive_Click(object sender, EventArgs e)
        {
            new ZLibForm().ShowDialog();
        }

        /// <summary>
        /// Управление программами в автозагрузке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionAutostart_Click(object sender, EventArgs e)
        {
            new AutostartForm().ShowDialog();
        }
    }
}
