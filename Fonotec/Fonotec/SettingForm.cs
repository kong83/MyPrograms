using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fonotec
{
    public partial class SettingForm : Form
    {
        private readonly SettingClass mSettingClass;

        private Font mSelectFont;
        private Color mSelectColor;        

        public SettingForm(SettingClass settingClass)
        {
            InitializeComponent();

            this.mSettingClass = settingClass;
        }

        /// <summary>
        /// Отображение текущих настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            checkBoxIsShowToolTips.Checked = this.mSettingClass.IsShowToolTips;
            checkBoxIsCloseDiskForm.Checked = this.mSettingClass.IsCloseDiskForm;
            checkBoxIsCloseFilmForm.Checked = this.mSettingClass.IsCloseFilmForm;

            this.mSelectFont = this.mSettingClass.ProgramFont;
            this.mSelectColor = this.mSettingClass.ProgramFontColor;
            SetFont();
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {
            textBoxFont.Text = this.mSelectFont.Name + "; " + this.mSelectFont.Size + "; " + this.mSelectColor.Name;
            textBoxFont.Font = this.mSelectFont;
            textBoxFont.ForeColor = this.mSelectColor;
            fontDialog.Font = this.mSelectFont;
            fontDialog.Color = this.mSelectColor;
        }


        /// <summary>
        /// Кнопка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Кнопка сохранения настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.mSettingClass.IsShowToolTips = checkBoxIsShowToolTips.Checked;
            this.mSettingClass.IsCloseDiskForm = checkBoxIsCloseDiskForm.Checked;
            this.mSettingClass.IsCloseFilmForm = checkBoxIsCloseFilmForm.Checked;
            this.mSettingClass.ProgramFont = this.mSelectFont;
            this.mSettingClass.ProgramFontColor = this.mSelectColor;
            Close();
        }


        /// <summary>
        /// Настройка шрифта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFont_Click(object sender, EventArgs e)
        {            
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (fontDialog.Font.Size > 15)
                {
                    MessageBox.Show("Шрифт не может быть больше 14", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.mSelectFont = fontDialog.Font;
                this.mSelectColor = fontDialog.Color;
                SetFont();
            }
        }


        /// <summary>
        /// Изменение раскладки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            this.mSettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }
    }
}
