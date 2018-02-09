using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimeWatcher
{
    public partial class SettingForm : Form
    {
        private readonly SettingClass m_SettingClass;
        private Font m_SelectFont;
        private Color m_SelectColor;

        public SettingForm(SettingClass settingClass)
        {
            InitializeComponent();

            m_SettingClass = settingClass;
        }


        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Подтвердить изменение настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            m_SettingClass.IsShowToolTips = checkBoxIsShowToolTips.Checked;            
            m_SettingClass.ProgramFont = m_SelectFont;
            m_SettingClass.ProgramFontColor = m_SelectColor;
            Close();
        }

        #region Работа с кнопочками
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips)
                toolTip.Show("Сохранить изменения", buttonOK, 10, -18);
        }
        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonOK);
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Popup;
            if (m_SettingClass.IsShowToolTips) 
                toolTip.Show("Закрыть окно", buttonClose, 10, -18);
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            buttonClose.FlatStyle = FlatStyle.Flat;
            toolTip.Hide(buttonClose);
        }
        #endregion


        /// <summary>
        /// Отображение текущих настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            checkBoxIsShowToolTips.Checked = m_SettingClass.IsShowToolTips;
            

            m_SelectFont = m_SettingClass.ProgramFont;
            m_SelectColor = m_SettingClass.ProgramFontColor;
            SetFont();
        }


        /// <summary>
        /// Установка шрифта для текстовых полей
        /// </summary>
        private void SetFont()
        {
            textBoxFont.Text = m_SelectFont.Name + "; " + m_SelectFont.Size + "; " + m_SelectColor.Name;
            textBoxFont.Font = m_SelectFont;
            textBoxFont.ForeColor = m_SelectColor;
            fontDialog.Font = m_SelectFont;
            fontDialog.Color = m_SelectColor;
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
                m_SelectFont = fontDialog.Font;
                m_SelectColor = fontDialog.Color;
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
            m_SettingClass.CurrentInputLanguage = InputLanguage.CurrentInputLanguage;
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonOK_Click(null, null);
            }
            else if (keyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }

            return true;
        }
    }
}
