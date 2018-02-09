using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TimeWatcher
{
    public class SettingClass
    {
        /// <summary>
        /// Структура, содержащая все настройки программы
        /// </summary>
        private struct SettingInfoStructure
        {
            public Size MainFormSize;
            public Point MainFormLocation;
            public Size TimesFormSize;
            public Point TimesFormLocation;
            public int[] TimesListColumnsWidth;
            public bool IsShowToolTips;
            public Font ProgramFont;
            public Color ProgramFontColor;
            public int KeyboardLayoutId;
        }

        private SettingInfoStructure m_SettingInfo;


        /// <summary>
        /// Размер главной формы
        /// </summary>
        public Size MainFormSize
        {
            get
            {
                return m_SettingInfo.MainFormSize;
            }

            set
            {
                m_SettingInfo.MainFormSize = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Расположение главной формы
        /// </summary>
        public Point MainFormLocation
        {
            get
            {
                return m_SettingInfo.MainFormLocation;
            }

            set
            {
                m_SettingInfo.MainFormLocation = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Размер формы с временными промежутками
        /// </summary>
        public Size TimesFormSize
        {
            get
            {
                return m_SettingInfo.TimesFormSize;
            }

            set
            {
                m_SettingInfo.TimesFormSize = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Расположение формы с временными промежутками
        /// </summary>
        public Point TimesFormLocation
        {
            get
            {
                return m_SettingInfo.TimesFormLocation;
            }

            set
            {
                m_SettingInfo.TimesFormLocation = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Расположение формы с временными промежутками
        /// </summary>
        public int[] TimesListColumnsWidth
        {
            get
            {
                return m_SettingInfo.TimesListColumnsWidth;
            }

            set
            {
                m_SettingInfo.TimesListColumnsWidth = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Показывать ли подсказки
        /// </summary>
        public bool IsShowToolTips
        {
            get
            {
                return m_SettingInfo.IsShowToolTips;
            }

            set
            {
                m_SettingInfo.IsShowToolTips = value;
                SaveSettingInfo();
            }
        }        


        /// <summary>
        /// Настройки шрифта
        /// </summary>
        public Font ProgramFont
        {
            get
            {
                return m_SettingInfo.ProgramFont;
            }

            set
            {
                m_SettingInfo.ProgramFont = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Настройки шрифта
        /// </summary>
        public Color ProgramFontColor
        {
            get
            {
                return m_SettingInfo.ProgramFontColor;
            }

            set
            {
                m_SettingInfo.ProgramFontColor = value;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Раскладка клавиатуры
        /// </summary>
        public InputLanguage CurrentInputLanguage
        {
            get
            {
                foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                {
                    if (lang.Culture.KeyboardLayoutId == m_SettingInfo.KeyboardLayoutId)
                    {
                        return lang;
                    }
                }
                return InputLanguage.DefaultInputLanguage;
            }

            set
            {
                m_SettingInfo.KeyboardLayoutId = value.Culture.KeyboardLayoutId;
                SaveSettingInfo();
            }
        }


        /// <summary>
        /// Класс для сохранения настроек программы
        /// </summary>
        public SettingClass()
        {
            LoadSettingInfo();
        }


        /// <summary>
        /// Установить значения по умолчанию
        /// </summary>
        private void SetDefault()
        {
            m_SettingInfo.MainFormSize.Height = 540;
            m_SettingInfo.MainFormSize.Width = 420;
            m_SettingInfo.MainFormLocation.X = 150;
            m_SettingInfo.MainFormLocation.Y = 150;

            m_SettingInfo.TimesFormSize.Height = 620;
            m_SettingInfo.TimesFormSize.Width = 370;
            m_SettingInfo.TimesFormLocation.X = 150;
            m_SettingInfo.TimesFormLocation.Y = 150;
            m_SettingInfo.TimesListColumnsWidth = new[] { 130, 130 };

            m_SettingInfo.IsShowToolTips = true;
            
            m_SettingInfo.KeyboardLayoutId = InputLanguage.DefaultInputLanguage.Culture.KeyboardLayoutId;

            m_SettingInfo.ProgramFont = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Regular);
            m_SettingInfo.ProgramFontColor = Color.Black;
        }


        /// <summary>
        /// Сохранение настроек программы
        /// </summary>
        private void LoadSettingInfo()
        {
            m_SettingInfo = new SettingInfoStructure();
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\TimeWatcher");

            if (regKey != null)
            {
                try
                {
                    m_SettingInfo.MainFormSize.Height = (int)regKey.GetValue("MainFormSize.Height");
                    m_SettingInfo.MainFormSize.Width = (int)regKey.GetValue("MainFormSize.Width");
                    m_SettingInfo.MainFormLocation.X = (int)regKey.GetValue("MainFormLocation.X");
                    m_SettingInfo.MainFormLocation.Y = (int)regKey.GetValue("MainFormLocation.Y");

                    m_SettingInfo.TimesFormSize.Height = (int)regKey.GetValue("TimesFormSize.Height");
                    m_SettingInfo.TimesFormSize.Width = (int)regKey.GetValue("TimesFormSize.Width");
                    m_SettingInfo.TimesFormLocation.X = (int)regKey.GetValue("TimesFormLocation.X");
                    m_SettingInfo.TimesFormLocation.Y = (int)regKey.GetValue("TimesFormLocation.Y");
                    
                    string[] timesListColumnsWidthArr = ((string)regKey.GetValue("TimesListColumnsWidth")).Split(new[] { ';' });
                    var timesListColumnsWidth = new int[timesListColumnsWidthArr.Length];
                    for (int i=0; i<timesListColumnsWidthArr.Length; i++)
                    {
                        timesListColumnsWidth[i] = Convert.ToInt32(timesListColumnsWidthArr[i]);
                    }
                    m_SettingInfo.TimesListColumnsWidth = timesListColumnsWidth;

                    var isShowToolTips = (string)regKey.GetValue("IsShowToolTips");
                    m_SettingInfo.IsShowToolTips = isShowToolTips == null || Convert.ToBoolean(isShowToolTips);
                    
                    m_SettingInfo.KeyboardLayoutId = (int)regKey.GetValue("KeyboardLayoutId");                    

                    // Загрузка данных о шрифтах
                    var fontSize = (float)Convert.ToDouble((string)regKey.GetValue("fontSize"));

                    var fontName = (string)regKey.GetValue("fontName");

                    var fontBold = Convert.ToBoolean((string)regKey.GetValue("fontBold"));

                    var fontItalic = Convert.ToBoolean((string)regKey.GetValue("fontItalic"));

                    var fontUnderline = Convert.ToBoolean((string)regKey.GetValue("fontUnderline"));

                    var fontStrikeout = Convert.ToBoolean((string)regKey.GetValue("fontStrikeout"));

                    FontStyle fs = FontStyle.Regular;
                    if (fontBold)
                        fs = fs | FontStyle.Bold;
                    if (fontItalic)
                        fs = fs | FontStyle.Italic;
                    if (fontUnderline)
                        fs = fs | FontStyle.Underline;
                    if (fontStrikeout)
                        fs = fs | FontStyle.Strikeout;
                    m_SettingInfo.ProgramFont = new Font(fontName, fontSize, fs);

                    m_SettingInfo.ProgramFontColor = Color.FromName((string)regKey.GetValue("foreColor"));
                }
                catch
                {
                    SetDefault();
                }
                finally
                {
                    regKey.Close();
                }
            }
            else
            {
                SetDefault();
            }
        }


        /// <summary>
        /// Сохранение настроек программы
        /// </summary>
        private void SaveSettingInfo()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\TimeWatcher");

            if (regKey != null)
            {
                regKey.SetValue("MainFormSize.Height", m_SettingInfo.MainFormSize.Height);
                regKey.SetValue("MainFormSize.Width", m_SettingInfo.MainFormSize.Width);
                regKey.SetValue("MainFormLocation.X", m_SettingInfo.MainFormLocation.X);
                regKey.SetValue("MainFormLocation.Y", m_SettingInfo.MainFormLocation.Y);

                regKey.SetValue("TimesFormSize.Height", m_SettingInfo.TimesFormSize.Height);
                regKey.SetValue("TimesFormSize.Width", m_SettingInfo.TimesFormSize.Width);
                regKey.SetValue("TimesFormLocation.X", m_SettingInfo.TimesFormLocation.X);
                regKey.SetValue("TimesFormLocation.Y", m_SettingInfo.TimesFormLocation.Y);
                var timesListColumnsWidthStr = new StringBuilder();
                foreach (int w in m_SettingInfo.TimesListColumnsWidth)
                {
                    timesListColumnsWidthStr.Append(w + ";");
                }
                regKey.SetValue("TimesListColumnsWidth", timesListColumnsWidthStr.ToString().TrimEnd(';'));

                regKey.SetValue("IsShowToolTips", m_SettingInfo.IsShowToolTips);
                
                regKey.SetValue("KeyboardLayoutId", m_SettingInfo.KeyboardLayoutId);

                // Сохранение шрифтов
                regKey.SetValue("fontSize", m_SettingInfo.ProgramFont.Size);
                regKey.SetValue("fontName", m_SettingInfo.ProgramFont.Name);
                regKey.SetValue("fontBold", m_SettingInfo.ProgramFont.Bold);
                regKey.SetValue("fontItalic", m_SettingInfo.ProgramFont.Italic);
                regKey.SetValue("fontUnderline", m_SettingInfo.ProgramFont.Underline);
                regKey.SetValue("fontStrikeout", m_SettingInfo.ProgramFont.Strikeout);
                regKey.SetValue("foreColor", m_SettingInfo.ProgramFontColor.Name);

                regKey.Close();
            }
        }
    }
}

