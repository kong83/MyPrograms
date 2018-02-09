using System;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Notepad
{
    /// <summary>
    /// Структура, содержащая все сохраняемые в реестре параметры
    /// </summary>
    public struct ParametersInfo
    {
        public FormWindowState MfWindState;
        public Font TextFont;
        public Color TextForeColor;
        public Size MfSize;
        public Point MfLocation;
        public bool WordWrap;
        public bool HistoryWrite;
        public int KeyboardLayoutId;
    }

    /// <summary>
    /// Информация о настройках печати
    /// </summary>
    public struct PrintInfo
    {
        public Margins Margins;
        public int PaperNumber;
        public bool Landscape;
    }

    /// <summary>
    /// Структура, содержащая все сохраняемые в реестре параметры для формы поиска
    /// </summary>
    public struct FindParametersInfo
    {
        public Point FfLocation;
        public string FindText;
        public string ReplaceText;
        public bool CheckRegistr;
        public bool CheckWordAll;
        public bool CheckSearchUp;
        public bool TopMost;
    }

    /// <summary>
    /// Структура, содержащая все сохраняемые в реестре параметры для формы быстрой замены
    /// </summary>
    public struct QuickReplaceParametersInfo
    {
        public Point QrLocation;
        public string FindText;
        public string ReplaceText;
        public bool UseRegExp;
        public bool TopMost;
    }

    /// <summary>
    /// Структура, содержащая все сохраняемые в реестре параметры для формы конвертации
    /// </summary>
    public struct ConvertParametersInfo
    {
        public Point CfLocation;
        public string ComboType;
        public string ComboFrom;
        public string ComboTo;
    }

    /// <summary>
    /// Структура, содержащая все сохраняемые в реестре параметры для формы с установленными программами
    /// </summary>
    public struct UninstallParametersInfo
    {
        public Point UninstLocation;
        public Size UninstSize;
        public string[] UninstColumnsWidth;
    }

    /// <summary>
    /// Структура, содержащая все сохраняемые в реестре параметры для формы с установленными программами
    /// </summary>
    public struct ProcessParametersInfo
    {
        public Point ProcessLocation;
        public Size ProcessSize;
        public string[] ProcessColumnsWidth;
    }

    /// <summary>
    /// Тип формы для поиска
    /// </summary>
    public enum TypeFindForm
    {
        Find,
        Replace
    }

    /// <summary>
    /// Информация о выделенном тексте
    /// </summary>
    public struct SelectionInfo
    {
        public int SelectionStart;
        public int SelectionLength;
        public int SelectionText;
    }

    class ActionClass
    {
        /// <summary>
        /// Можно ли использовать реестр
        /// </summary>
        private bool _useRegister = true;

        /// <summary>
        /// Путь в реестре
        /// </summary>
        private const string RegPath = "Software\\SuperNotepad\\";

        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(ParametersInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Сохранение размера и позиции формы
            if (regKey != null)
            {
                regKey.SetValue("MfWindState", paramInfo.MfWindState.ToString());
                if (paramInfo.MfLocation.X >= 0 && paramInfo.MfLocation.Y >= 0)
                {

                    regKey.SetValue("MfSize", paramInfo.MfSize.Width + ";" + paramInfo.MfSize.Height);
                    regKey.SetValue("MfLocation", paramInfo.MfLocation.X + ";" + paramInfo.MfLocation.Y);
                }

                // Сохранение опций
                regKey.SetValue("WordWrap", paramInfo.WordWrap.ToString());
                regKey.SetValue("HistoryWrite", paramInfo.HistoryWrite.ToString());
                regKey.SetValue("language", paramInfo.KeyboardLayoutId.ToString());

                // Сохранение шрифтов
                regKey.SetValue("fontSize", paramInfo.TextFont.Size.ToString());
                regKey.SetValue("fontName", paramInfo.TextFont.Name);
                regKey.SetValue("fontBold", paramInfo.TextFont.Bold.ToString());
                regKey.SetValue("fontItalic", paramInfo.TextFont.Italic.ToString());
                regKey.SetValue("fontUnderline", paramInfo.TextFont.Underline.ToString());
                regKey.SetValue("fontStrikeout", paramInfo.TextFont.Strikeout.ToString());
                regKey.SetValue("foreColor", paramInfo.TextForeColor.Name);
            }
        }

        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out ParametersInfo paramInfo)
        {
            paramInfo = new ParametersInfo
            {
                MfWindState = FormWindowState.Normal,
                MfSize = new Size(250, 250),
                MfLocation = new Point(150, 150),
                WordWrap = false,
                HistoryWrite = true,
                KeyboardLayoutId = 1049,
                TextFont = new Font("Microsoft Sans Serif", (float)8.25),
                TextForeColor = Color.Black
            };

            if (!_useRegister)
                return;

            RegistryKey regKey;
            // Чтение значений из реестра
            string s = "";
            try
            {
                regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey(RegPath);
                // ReSharper disable PossibleNullReferenceException
                s = (string)regKey.GetValue("MfWindState", s);
                // ReSharper restore PossibleNullReferenceException
            }
            catch
            {
                _useRegister = false;
                return;
            }
            if (s == "Maximized")
            {
                paramInfo.MfWindState = FormWindowState.Maximized;
            }
            else if (s == "Minimized")
            {
                paramInfo.MfWindState = FormWindowState.Minimized;
            }

            try
            {
                s = "";
                s = (string)regKey.GetValue("MfSize", s);
                string[] arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 2)
                {
                    paramInfo.MfSize = new Size(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                }

                s = "";
                s = (string)regKey.GetValue("MfLocation", s);
                arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 2 && Convert.ToInt32(arr[0]) > 0 && Convert.ToInt32(arr[1]) > 0)
                {
                    paramInfo.MfLocation = new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                }

                s = "";
                s = (string)regKey.GetValue("WordWrap", s);
                if (s != "")
                {
                    paramInfo.WordWrap = Convert.ToBoolean(s);
                }

                s = "";
                s = (string)regKey.GetValue("HistoryWrite", s);
                if (s != "")
                {
                    paramInfo.HistoryWrite = Convert.ToBoolean(s);
                }

                s = "";
                s = (string)regKey.GetValue("language", s);
                if (s != "")
                {
                    paramInfo.KeyboardLayoutId = Convert.ToInt32(s);
                }

                // Загрузка данных о шрифтах
                float fontSize = paramInfo.TextFont.Size;
                s = "";
                s = (string)regKey.GetValue("fontSize", s);
                if (s != "")
                {
                    fontSize = (float)Convert.ToDouble(s);
                }

                string fontName = paramInfo.TextFont.Name;
                s = "";
                s = (string)regKey.GetValue("fontName", s);
                if (s != "")
                {
                    fontName = s;
                }

                bool fontBold = paramInfo.TextFont.Bold;
                s = "";
                s = (string)regKey.GetValue("fontBold", s);
                if (s != "")
                {
                    fontBold = Convert.ToBoolean(s);
                }

                bool fontItalic = paramInfo.TextFont.Italic;
                s = "";
                s = (string)regKey.GetValue("fontItalic", s);
                if (s != "")
                {
                    fontItalic = Convert.ToBoolean(s);
                }

                bool fontUnderline = paramInfo.TextFont.Underline;
                s = "";
                s = (string)regKey.GetValue("fontUnderline", s);
                if (s != "")
                {
                    fontUnderline = Convert.ToBoolean(s);
                }

                bool fontStrikeout = paramInfo.TextFont.Strikeout;
                s = "";
                s = (string)regKey.GetValue("fontStrikeout", s);
                if (s != "")
                {
                    fontStrikeout = Convert.ToBoolean(s);
                }

                FontStyle fs = FontStyle.Regular;
                if (fontBold)
                    fs = fs | FontStyle.Bold;
                if (fontItalic)
                    fs = fs | FontStyle.Italic;
                if (fontUnderline)
                    fs = fs | FontStyle.Underline;
                if (fontStrikeout)
                    fs = fs | FontStyle.Strikeout;
                paramInfo.TextFont = new Font(fontName, fontSize, fs);

                s = "";
                s = (string)regKey.GetValue("foreColor", s);
                if (s != "")
                {
                    paramInfo.TextForeColor = Color.FromName(s);
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }

        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(PrintInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Сохранение размеров отступов
            if (regKey != null)
            {
                regKey.SetValue("marginsBottom", paramInfo.Margins.Bottom.ToString());
                regKey.SetValue("marginsLeft", paramInfo.Margins.Left.ToString());
                regKey.SetValue("marginsRight", paramInfo.Margins.Right.ToString());
                regKey.SetValue("marginsTop", paramInfo.Margins.Top.ToString());

                // Сохранение настроек листа
                regKey.SetValue("psNumber", paramInfo.PaperNumber.ToString());
                regKey.SetValue("psLandscape", paramInfo.Landscape.ToString());
            }
        }

        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out PrintInfo paramInfo)
        {
            paramInfo = new PrintInfo { Margins = new Margins(30, 30, 30, 30), PaperNumber = 4 };

            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            try
            {
                // Чтение значений из реестра            
                if (regKey != null)
                {
                    string s = "";
                    s = (string)regKey.GetValue("marginsBottom", s);
                    paramInfo.Margins.Bottom = Convert.ToInt32(s);

                    s = "";
                    s = (string)regKey.GetValue("marginsLeft", s);
                    paramInfo.Margins.Left = Convert.ToInt32(s);

                    s = "";
                    s = (string)regKey.GetValue("marginsRight", s);
                    paramInfo.Margins.Right = Convert.ToInt32(s);

                    s = "";
                    s = (string)regKey.GetValue("marginsTop", s);
                    paramInfo.Margins.Top = Convert.ToInt32(s);

                    s = "";
                    s = (string)regKey.GetValue("psNumber", s);
                    paramInfo.PaperNumber = Convert.ToInt32(s);

                    s = "";
                    s = (string)regKey.GetValue("psLandscape", s);
                    paramInfo.Landscape = Convert.ToBoolean(s);
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }

        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(FindParametersInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            if (regKey != null)
            {
                // Сохранение размера и позиции формы            
                regKey.SetValue("FfLocation", paramInfo.FfLocation.X + ";" + paramInfo.FfLocation.Y);

                // Сохранение опций
                regKey.SetValue("FindText", paramInfo.FindText);
                regKey.SetValue("ReplaceText", paramInfo.ReplaceText);
                regKey.SetValue("register", paramInfo.CheckRegistr.ToString());
                regKey.SetValue("searchUp", paramInfo.CheckSearchUp.ToString());
                regKey.SetValue("wordAll", paramInfo.CheckWordAll.ToString());
                regKey.SetValue("TopMost", paramInfo.TopMost.ToString());
            }
        }

        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out FindParametersInfo paramInfo)
        {
            paramInfo = new FindParametersInfo
            {
                FfLocation = new Point(250, 150),
                FindText = "",
                ReplaceText = "",
                CheckRegistr = false,
                CheckSearchUp = false,
                CheckWordAll = false,
                TopMost = true
            };

            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Чтение значений из реестра            
            try
            {
                if (regKey != null)
                {
                    string s = "";
                    s = (string)regKey.GetValue("FfLocation", s);
                    string[] arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2 && Convert.ToInt32(arr[0]) > 0 && Convert.ToInt32(arr[1]) > 0)
                    {
                        paramInfo.FfLocation = new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("FindText", s);
                    paramInfo.FindText = s;

                    s = "";
                    s = (string)regKey.GetValue("ReplaceText", s);
                    paramInfo.ReplaceText = s;

                    s = "";
                    s = (string)regKey.GetValue("register", s);
                    paramInfo.CheckRegistr = Convert.ToBoolean(s);

                    s = "";
                    s = (string)regKey.GetValue("searchUp", s);
                    paramInfo.CheckSearchUp = Convert.ToBoolean(s);

                    s = "";
                    s = (string)regKey.GetValue("wordAll", s);
                    paramInfo.CheckWordAll = Convert.ToBoolean(s);

                    s = "";
                    s = (string)regKey.GetValue("TopMost", s);
                    paramInfo.TopMost = Convert.ToBoolean(s);
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }

        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(QuickReplaceParametersInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            if (regKey != null)
            {
                // Сохранение размера и позиции формы            
                regKey.SetValue("QrLocation", paramInfo.QrLocation.X + ";" + paramInfo.QrLocation.Y);

                // Сохранение опций
                regKey.SetValue("qrFindText", paramInfo.FindText);
                regKey.SetValue("qrReplaceText", paramInfo.ReplaceText);
                regKey.SetValue("qrUseRegExp", paramInfo.UseRegExp.ToString());
                regKey.SetValue("qrTopMost", paramInfo.TopMost.ToString());
            }
        }

        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out QuickReplaceParametersInfo paramInfo)
        {
            paramInfo = new QuickReplaceParametersInfo
            {
                QrLocation = new Point(250, 150),
                FindText = "",
                ReplaceText = "",
                TopMost = true
            };

            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Чтение значений из реестра            
            try
            {
                if (regKey != null)
                {
                    string s = "";
                    s = (string)regKey.GetValue("QrLocation", s);
                    string[] arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2 && Convert.ToInt32(arr[0]) > 0 && Convert.ToInt32(arr[1]) > 0)
                    {
                        paramInfo.QrLocation = new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("qrFindText", s);
                    paramInfo.FindText = s;

                    s = "";
                    s = (string)regKey.GetValue("qrReplaceText", s);
                    paramInfo.ReplaceText = s;

                    s = "";
                    s = (string)regKey.GetValue("qrUseRegExp", s);
                    paramInfo.UseRegExp = Convert.ToBoolean(s);

                    s = "";
                    s = (string)regKey.GetValue("qrTopMost", s);
                    paramInfo.TopMost = Convert.ToBoolean(s);
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }


        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(ConvertParametersInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            if (regKey != null)
            {
                // Сохранение размера и позиции формы            
                regKey.SetValue("CfLocation", paramInfo.CfLocation.X + ";" + paramInfo.CfLocation.Y);

                // Сохранение опций
                regKey.SetValue("ComboType", paramInfo.ComboType);
                regKey.SetValue("ComboFrom", paramInfo.ComboFrom);
                regKey.SetValue("ComboTo", paramInfo.ComboTo);
            }
        }

        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out ConvertParametersInfo paramInfo)
        {
            paramInfo = new ConvertParametersInfo
            {
                CfLocation = new Point(250, 150),
                ComboType = "",
                ComboFrom = "",
                ComboTo = ""
            };

            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Чтение значений из реестра
            try
            {
                if (regKey != null)
                {
                    string s = "";
                    s = (string)regKey.GetValue("CfLocation", s);
                    string[] arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2 && Convert.ToInt32(arr[0]) > 0 && Convert.ToInt32(arr[1]) > 0)
                    {
                        paramInfo.CfLocation = new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("ComboType", s);
                    paramInfo.ComboType = s;

                    s = "";
                    s = (string)regKey.GetValue("ComboFrom", s);
                    paramInfo.ComboFrom = s;

                    s = "";
                    s = (string)regKey.GetValue("ComboTo", s);
                    paramInfo.ComboTo = s;
                }

            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }


        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(UninstallParametersInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            if (regKey != null)
            {
                // Сохранение размера и позиции формы            
                regKey.SetValue("UninstLocation", paramInfo.UninstLocation.X + ";" + paramInfo.UninstLocation.Y);
                regKey.SetValue("UninstSize", paramInfo.UninstSize.Width + ";" + paramInfo.UninstSize.Height);

                string res = "";
                for (int i = 0; i < paramInfo.UninstColumnsWidth.Length; i++)
                {
                    res += paramInfo.UninstColumnsWidth[i] + ";";
                }
                regKey.SetValue("UninstColumnsWidth", res);
            }
        }


        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out UninstallParametersInfo paramInfo)
        {
            paramInfo = new UninstallParametersInfo
            {
                UninstLocation = new Point(44, 123),
                UninstSize = new Size(1099, 621),
                UninstColumnsWidth = new[] { "370", "251", "100", "308", "18" }
            };


            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Чтение значений из реестра
            try
            {
                if (regKey != null)
                {
                    string s = "";
                    s = (string)regKey.GetValue("UninstLocation", s);
                    string[] arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2 && Convert.ToInt32(arr[0]) > 0 && Convert.ToInt32(arr[1]) > 0)
                    {
                        paramInfo.UninstLocation = new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("UninstSize", s);
                    arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2)
                    {
                        paramInfo.UninstSize = new Size(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("UninstColumnsWidth", s);
                    arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 1)
                    {
                        paramInfo.UninstColumnsWidth = arr;
                    }
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }


        /// <summary>
        /// Сохранение параметров в реестре
        /// </summary>
        /// <param name="paramInfo">Структура, содержащая все сохраняемые параметры</param>
        public void SaveParameter(ProcessParametersInfo paramInfo)
        {
            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            if (regKey != null)
            {
                // Сохранение размера и позиции формы            
                regKey.SetValue("ProcessLocation", paramInfo.ProcessLocation.X + ";" + paramInfo.ProcessLocation.Y);
                regKey.SetValue("ProcessSize", paramInfo.ProcessSize.Width + ";" + paramInfo.ProcessSize.Height);

                string res = "";
                for (int i = 0; i < paramInfo.ProcessColumnsWidth.Length; i++)
                {
                    res += paramInfo.ProcessColumnsWidth[i] + ";";
                }
                regKey.SetValue("ProcessColumnsWidth", res);
            }
        }


        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter(out ProcessParametersInfo paramInfo)
        {
            paramInfo = new ProcessParametersInfo
            {
                ProcessLocation = new Point(198, 152),
                ProcessSize = new Size(936, 719),
                ProcessColumnsWidth = new[] { "182", "496", "70", "127", "11" }
            };

            if (!_useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Чтение значений из реестра
            try
            {
                if (regKey != null)
                {
                    string s = "";
                    s = (string)regKey.GetValue("ProcessLocation", s);
                    string[] arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2 && Convert.ToInt32(arr[0]) > 0 && Convert.ToInt32(arr[1]) > 0)
                    {
                        paramInfo.ProcessLocation = new Point(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("ProcessSize", s);
                    arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length == 2)
                    {
                        paramInfo.ProcessSize = new Size(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                    }

                    s = "";
                    s = (string)regKey.GetValue("ProcessColumnsWidth", s);
                    arr = s.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr.Length > 1)
                    {
                        paramInfo.ProcessColumnsWidth = arr;
                    }
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
        }


        /// <summary>
        /// Строка для разделения значений в истории строк поиска и замены
        /// </summary>
        private const string SeparateVals = "*!^&*";

        /// <summary>
        /// Добавление нового значения сторки поиска или замены в историю
        /// </summary>
        /// <param name="regKeyStr">findVals или replaceVals</param>
        /// <param name="val">Новое значение</param>
        public void AddValue(string regKeyStr, string val)
        {
            if (!_useRegister)
                return;

            string[] oldVals;
            LoadValues(regKeyStr, out oldVals);

            int cnt = 0;
            string res = val;
            for (int i = 0; i < oldVals.Length; i++)
            {
                if (oldVals[i] != val)
                {
                    res += SeparateVals + oldVals[i];
                    cnt++;
                    if (cnt == 20)
                    {
                        break;
                    }
                }
            }

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);
            if (regKey != null) regKey.SetValue(regKeyStr, res);
        }

        /// <summary>
        /// Загрузка списка строк истории поиска или замены
        /// </summary>
        /// <param name="regKeyStr">findVals или replaceVals</param>
        /// <param name="vals">Список строк, запомненных в истории</param>
        public void LoadValues(string regKeyStr, out string[] vals)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(RegPath);

            // Чтение значений из реестра      
            string s = "";
            if (regKey != null) s = (string)regKey.GetValue(regKeyStr, s);
            vals = s.Split(new[] { SeparateVals }, StringSplitOptions.None);
        }
    }
}
