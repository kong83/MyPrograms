using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Fonotec
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
            public Point FindFormLocation;
            public bool IsShowToolTips;
            public bool IsCloseFilmForm;
            public bool IsCloseDiskForm;
            public bool IsFilmFieldsClear;
            public Font ProgramFont;
            public Color ProgramFontColor;
            public int KeyboardLayoutId;
            public Size ExportFormSize;
            public Point ExportFormLocation;
            public bool IsCheckBoxAllChecked;
            public bool IsCheckBoxWithoutNumberChecked;
            public bool IsCheckBoxWithoutDiskInfoChecked;
            public bool IsCheckBoxWithoutFilmInfoChecked;
            public bool IsRadioButtonView1Checked;
            public bool IsRadioButtonView2Checked;
            public bool IsRadioButtonView3Checked;
        }

        private SettingInfoStructure mSettingInfo;

        /// <summary>
        /// Размер главной формы
        /// </summary>
        public Size MainFormSize
        {
            get
            {
                return this.mSettingInfo.MainFormSize;
            }

            set
            {
                this.mSettingInfo.MainFormSize = value;
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
                return this.mSettingInfo.MainFormLocation;
            }

            set
            {
                this.mSettingInfo.MainFormLocation = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Расположение формы поиска
        /// </summary>
        public Point FindFormLocation
        {
            get
            {
                return this.mSettingInfo.FindFormLocation;
            }

            set
            {
                this.mSettingInfo.FindFormLocation = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Размер формы экспорта
        /// </summary>
        public Size ExportFormSize
        {
            get
            {
                return this.mSettingInfo.ExportFormSize;
            }

            set
            {
                this.mSettingInfo.ExportFormSize = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Расположение формы экспорта
        /// </summary>
        public Point ExportFormLocation
        {
            get
            {
                return this.mSettingInfo.ExportFormLocation;
            }

            set
            {
                this.mSettingInfo.ExportFormLocation = value;
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
                return this.mSettingInfo.IsShowToolTips;
            }

            set
            {
                this.mSettingInfo.IsShowToolTips = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Закрывать ли форму добавления нового фильма после нажатия на ОК
        /// </summary>
        public bool IsCloseFilmForm
        {
            get
            {
                return this.mSettingInfo.IsCloseFilmForm;
            }

            set
            {
                this.mSettingInfo.IsCloseFilmForm = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Закрывать ли форму добавления нового диска после нажатия на ОК
        /// </summary>
        public bool IsCloseDiskForm
        {
            get
            {
                return this.mSettingInfo.IsCloseDiskForm;
            }

            set
            {
                this.mSettingInfo.IsCloseDiskForm = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Очищать ли поля после добавления нового фильма
        /// </summary>
        public bool IsFilmFieldsClear
        {
            get
            {
                return this.mSettingInfo.IsFilmFieldsClear;
            }

            set
            {
                this.mSettingInfo.IsFilmFieldsClear = value;
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
                return this.mSettingInfo.ProgramFont;
            }

            set
            {
                this.mSettingInfo.ProgramFont = value;
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
                return this.mSettingInfo.ProgramFontColor;
            }

            set
            {
                this.mSettingInfo.ProgramFontColor = value;
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
                    if (lang.Culture.KeyboardLayoutId == this.mSettingInfo.KeyboardLayoutId)
                    {
                        return lang;
                    }
                }
                return InputLanguage.DefaultInputLanguage;
            }

            set
            {
                this.mSettingInfo.KeyboardLayoutId = value.Culture.KeyboardLayoutId;
                SaveSettingInfo();
            }
        }
        
        /// <summary>
        /// Стоит ли галка для checBoxAll на форме для экспорта
        /// </summary>
        public bool IsCheckBoxAllChecked
        {
            get
            {
                return this.mSettingInfo.IsCheckBoxAllChecked;
            }

            set
            {
                this.mSettingInfo.IsCheckBoxAllChecked = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Стоит ли галка для checkBoxWithoutNumber на форме для экспорта
        /// </summary>
        public bool IsCheckBoxWithoutNumberChecked
        {
            get
            {
                return this.mSettingInfo.IsCheckBoxWithoutNumberChecked;
            }

            set
            {
                this.mSettingInfo.IsCheckBoxWithoutNumberChecked = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Стоит ли галка для checkBoxWithoutDiskInfo на форме для экспорта
        /// </summary>
        public bool IsCheckBoxWithoutDiskInfoChecked
        {
            get
            {
                return this.mSettingInfo.IsCheckBoxWithoutDiskInfoChecked;
            }

            set
            {
                this.mSettingInfo.IsCheckBoxWithoutDiskInfoChecked = value;
                SaveSettingInfo();
            }
        }
        
        /// <summary>
        /// Стоит ли галка для checkBoxWithoutFilmInfo на форме для экспорта
        /// </summary>
        public bool IsCheckBoxWithoutFilmInfoChecked
        {
            get
            {
                return this.mSettingInfo.IsCheckBoxWithoutFilmInfoChecked;
            }

            set
            {
                this.mSettingInfo.IsCheckBoxWithoutFilmInfoChecked = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Стоит ли галка для кadioButtonView1 на форме для экспорта
        /// </summary>
        public bool IsRadioButtonView1Checked
        {
            get
            {
                return this.mSettingInfo.IsRadioButtonView1Checked;
            }

            set
            {
                this.mSettingInfo.IsRadioButtonView1Checked = value;
                SaveSettingInfo();
            }
        }

        /// <summary>
        /// Стоит ли галка для adioButtonView2 на форме для экспорта
        /// </summary>
        public bool IsRadioButtonView2Checked
        {
            get
            {
                return this.mSettingInfo.IsRadioButtonView2Checked;
            }

            set
            {
                this.mSettingInfo.IsRadioButtonView2Checked = value;
                SaveSettingInfo();
            }
        }
        
        /// <summary>
        /// Стоит ли галка для adioButtonView3 на форме для экспорта
        /// </summary>
        public bool IsRadioButtonView3Checked
        {
            get
            {
                return this.mSettingInfo.IsRadioButtonView3Checked;
            }

            set
            {
                this.mSettingInfo.IsRadioButtonView3Checked = value;
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
            this.mSettingInfo.MainFormSize.Height = 500;
            this.mSettingInfo.MainFormSize.Width = 800;
            this.mSettingInfo.MainFormLocation.X = 150;
            this.mSettingInfo.MainFormLocation.Y = 150;
            this.mSettingInfo.FindFormLocation.X = 300;
            this.mSettingInfo.FindFormLocation.Y = 300;
            this.mSettingInfo.IsCloseDiskForm = true;
            this.mSettingInfo.IsCloseFilmForm = true;
            this.mSettingInfo.IsShowToolTips = true;
            this.mSettingInfo.IsFilmFieldsClear = false;
            this.mSettingInfo.KeyboardLayoutId = InputLanguage.DefaultInputLanguage.Culture.KeyboardLayoutId;

            this.mSettingInfo.ProgramFont = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Regular);
            this.mSettingInfo.ProgramFontColor = Color.Black;

            this.mSettingInfo.ExportFormSize.Height = 450;
            this.mSettingInfo.ExportFormSize.Width = 550;
            this.mSettingInfo.ExportFormLocation.X = 150;
            this.mSettingInfo.ExportFormLocation.Y = 150;
            this.mSettingInfo.IsCloseDiskForm = true;

            this.mSettingInfo.IsCheckBoxAllChecked = true;
            this.mSettingInfo.IsCheckBoxWithoutNumberChecked = false;
            this.mSettingInfo.IsCheckBoxWithoutDiskInfoChecked = false;
            this.mSettingInfo.IsCheckBoxWithoutFilmInfoChecked = false;
            this.mSettingInfo.IsRadioButtonView1Checked = true;
            this.mSettingInfo.IsRadioButtonView2Checked = false;
            this.mSettingInfo.IsRadioButtonView3Checked = false;
        }

        /// <summary>
        /// Сохранение настроек программы
        /// </summary>
        private void LoadSettingInfo()
        {
            this.mSettingInfo = new SettingInfoStructure();
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Fonotec"))
            {
                if (regKey != null)
                {
                    try
                    {
                        this.mSettingInfo.MainFormSize.Height = (int)regKey.GetValue("MainFormSize.Height");
                        this.mSettingInfo.MainFormSize.Width = (int)regKey.GetValue("MainFormSize.Width");
                        this.mSettingInfo.MainFormLocation.X = (int)regKey.GetValue("MainFormLocation.X");
                        this.mSettingInfo.MainFormLocation.Y = (int)regKey.GetValue("MainFormLocation.Y");
                        this.mSettingInfo.FindFormLocation.X = (int)regKey.GetValue("FindFormLocation.X");
                        this.mSettingInfo.FindFormLocation.Y = (int)regKey.GetValue("FindFormLocation.Y");

                        var isCloseDiskForm = (string)regKey.GetValue("IsCloseDiskForm");
                        this.mSettingInfo.IsCloseDiskForm = isCloseDiskForm == null || Convert.ToBoolean(isCloseDiskForm);

                        var isCloseFilmForm = (string)regKey.GetValue("IsCloseFilmForm");
                        this.mSettingInfo.IsCloseFilmForm = isCloseFilmForm == null || Convert.ToBoolean(isCloseFilmForm);

                        var isShowToolTips = (string)regKey.GetValue("IsShowToolTips");
                        this.mSettingInfo.IsShowToolTips = isShowToolTips == null || Convert.ToBoolean(isShowToolTips);

                        var isFilmFieldsClear = (string)regKey.GetValue("IsFilmFieldsClear");
                        this.mSettingInfo.IsFilmFieldsClear = isFilmFieldsClear == null || Convert.ToBoolean(isFilmFieldsClear);

                        this.mSettingInfo.KeyboardLayoutId = (int)regKey.GetValue("KeyboardLayoutId");

                        // Загрузка данных о шрифтах
                        var fontSize = (float)Convert.ToDouble((string)regKey.GetValue("fontSize"));

                        var fontName = (string)regKey.GetValue("fontName");

                        var fontBold = Convert.ToBoolean((string)regKey.GetValue("fontBold"));

                        var fontItalic = Convert.ToBoolean((string)regKey.GetValue("fontItalic"));

                        var fontUnderline = Convert.ToBoolean((string)regKey.GetValue("fontUnderline"));

                        var fontStrikeout = Convert.ToBoolean((string)regKey.GetValue("fontStrikeout"));

                        FontStyle fs = FontStyle.Regular;
                        if (fontBold)
                        {
                            fs = fs | FontStyle.Bold;
                        }

                        if (fontItalic)
                        {
                            fs = fs | FontStyle.Italic;
                        }

                        if (fontUnderline)
                        {
                            fs = fs | FontStyle.Underline;
                        }

                        if (fontStrikeout)
                        {
                            fs = fs | FontStyle.Strikeout;
                        }

                        this.mSettingInfo.ProgramFont = new Font(fontName, fontSize, fs);

                        this.mSettingInfo.ProgramFontColor = Color.FromName((string)regKey.GetValue("foreColor"));

                        // Загрузка данных о форме экспорта
                        this.mSettingInfo.ExportFormSize.Height = (int)regKey.GetValue("ExportFormSize.Height");
                        this.mSettingInfo.ExportFormSize.Width = (int)regKey.GetValue("ExportFormSize.Width");
                        this.mSettingInfo.ExportFormLocation.X = (int)regKey.GetValue("ExportFormLocation.X");
                        this.mSettingInfo.ExportFormLocation.Y = (int)regKey.GetValue("ExportFormLocation.Y");

                        var isCheckBoxAllChecked = (string)regKey.GetValue("IsCheckBoxAllChecked");
                        this.mSettingInfo.IsCheckBoxAllChecked = isCheckBoxAllChecked == null || Convert.ToBoolean(isCheckBoxAllChecked);

                        var isCheckBoxWithoutNumberChecked = (string)regKey.GetValue("IsCheckBoxWithoutNumberChecked");
                        this.mSettingInfo.IsCheckBoxWithoutNumberChecked = isCheckBoxWithoutNumberChecked != null && Convert.ToBoolean(isCheckBoxWithoutNumberChecked);

                        var isCheckBoxWithoutDiskInfoChecked = (string)regKey.GetValue("IsCheckBoxWithoutDiskInfoChecked");
                        this.mSettingInfo.IsCheckBoxWithoutDiskInfoChecked = isCheckBoxWithoutDiskInfoChecked != null && Convert.ToBoolean(isCheckBoxWithoutDiskInfoChecked);

                        var isCheckBoxWithoutFilmInfoChecked = (string)regKey.GetValue("IsCheckBoxWithoutFilmInfoChecked");
                        this.mSettingInfo.IsCheckBoxWithoutFilmInfoChecked = isCheckBoxWithoutFilmInfoChecked != null && Convert.ToBoolean(isCheckBoxWithoutFilmInfoChecked);


                        var isRadioButtonView1Checked = (string)regKey.GetValue("IsRadioButtonView1Checked");
                        this.mSettingInfo.IsRadioButtonView1Checked = isRadioButtonView1Checked == null || Convert.ToBoolean(isRadioButtonView1Checked);

                        var isRadioButtonView2Checked = (string)regKey.GetValue("IsRadioButtonView2Checked");
                        this.mSettingInfo.IsRadioButtonView2Checked = isRadioButtonView2Checked != null && Convert.ToBoolean(isRadioButtonView2Checked);

                        var isRadioButtonView3Checked = (string)regKey.GetValue("IsRadioButtonView3Checked");
                        this.mSettingInfo.IsRadioButtonView3Checked = isRadioButtonView3Checked != null && Convert.ToBoolean(isRadioButtonView3Checked);
                    }
                    catch
                    {
                        SetDefault();
                    }

                }
                else
                {
                    SetDefault();
                }
            }
        }


        /// <summary>
        /// Загрузка настроек программы
        /// </summary>
        private void SaveSettingInfo()
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Fonotec"))
            {
                if (regKey != null)
                {
                    regKey.SetValue("MainFormSize.Height", this.mSettingInfo.MainFormSize.Height);
                    regKey.SetValue("MainFormSize.Width", this.mSettingInfo.MainFormSize.Width);
                    regKey.SetValue("MainFormLocation.X", this.mSettingInfo.MainFormLocation.X);
                    regKey.SetValue("MainFormLocation.Y", this.mSettingInfo.MainFormLocation.Y);
                    regKey.SetValue("FindFormLocation.X", this.mSettingInfo.FindFormLocation.X);
                    regKey.SetValue("FindFormLocation.Y", this.mSettingInfo.FindFormLocation.Y);
                    regKey.SetValue("IsCloseDiskForm", this.mSettingInfo.IsCloseDiskForm);
                    regKey.SetValue("IsCloseFilmForm", this.mSettingInfo.IsCloseFilmForm);
                    regKey.SetValue("IsShowToolTips", this.mSettingInfo.IsShowToolTips);
                    regKey.SetValue("IsFilmFieldsClear", this.mSettingInfo.IsFilmFieldsClear);
                    regKey.SetValue("KeyboardLayoutId", this.mSettingInfo.KeyboardLayoutId);

                    // Сохранение шрифтов
                    regKey.SetValue("fontSize", this.mSettingInfo.ProgramFont.Size);
                    regKey.SetValue("fontName", this.mSettingInfo.ProgramFont.Name);
                    regKey.SetValue("fontBold", this.mSettingInfo.ProgramFont.Bold);
                    regKey.SetValue("fontItalic", this.mSettingInfo.ProgramFont.Italic);
                    regKey.SetValue("fontUnderline", this.mSettingInfo.ProgramFont.Underline);
                    regKey.SetValue("fontStrikeout", this.mSettingInfo.ProgramFont.Strikeout);
                    regKey.SetValue("foreColor", this.mSettingInfo.ProgramFontColor.Name);

                    // Сохарнение параметров для формы экспорта
                    regKey.SetValue("ExportFormSize.Height", this.mSettingInfo.ExportFormSize.Height);
                    regKey.SetValue("ExportFormSize.Width", this.mSettingInfo.ExportFormSize.Width);
                    regKey.SetValue("ExportFormLocation.X", this.mSettingInfo.ExportFormLocation.X);
                    regKey.SetValue("ExportFormLocation.Y", this.mSettingInfo.ExportFormLocation.Y);
                    regKey.SetValue("IsCheckBoxAllChecked", this.mSettingInfo.IsCheckBoxAllChecked);
                    regKey.SetValue("IsCheckBoxWithoutNumberChecked", this.mSettingInfo.IsCheckBoxWithoutNumberChecked);
                    regKey.SetValue("IsCheckBoxWithoutDiskInfoChecked", this.mSettingInfo.IsCheckBoxWithoutDiskInfoChecked);
                    regKey.SetValue("IsCheckBoxWithoutFilmInfoChecked", this.mSettingInfo.IsCheckBoxWithoutFilmInfoChecked);
                    regKey.SetValue("IsRadioButtonView1Checked", this.mSettingInfo.IsRadioButtonView1Checked);
                    regKey.SetValue("IsRadioButtonView2Checked", this.mSettingInfo.IsRadioButtonView2Checked);
                    regKey.SetValue("IsRadioButtonView3Checked", this.mSettingInfo.IsRadioButtonView3Checked);
                }
            }
        }
    }
}
