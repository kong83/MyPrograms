using System;
using System.Drawing;
using System.Windows.Forms;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Forms
{
    public partial class MyMessageBox : Form
    {
        private readonly MessageBoxIcon _icon;

        public MyMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();

            _icon = icon;
            Text = caption;

            int buttonTop = Height - 67;

            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    buttonYes.Text = "Прекратить";
                    buttonNo.Text = "Повторить";
                    buttonCancel.Text = "Игнорировать";
                    buttonYes.DialogResult = DialogResult.Abort;
                    buttonNo.DialogResult = DialogResult.Retry;
                    buttonCancel.DialogResult = DialogResult.Ignore;
                    buttonYes.Location = new Point(17, buttonTop);
                    buttonNo.Location = new Point(117, buttonTop);
                    buttonCancel.Location = new Point(217, buttonTop);
                    buttonYes.Size = buttonNo.Size = buttonCancel.Size = new Size(94, 23);
                    break;
                case MessageBoxButtons.OK:
                    buttonYes.Text = "ОК";
                    buttonNo.Visible = false;
                    buttonCancel.Visible = false;
                    buttonYes.DialogResult = DialogResult.OK;
                    buttonYes.Location = new Point(247, buttonTop);
                    buttonYes.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                    buttonYes.Size = new Size(75, 23);
                    break;
                case MessageBoxButtons.OKCancel:
                    buttonYes.Text = "ОК";
                    buttonNo.Visible = false;
                    buttonCancel.Text = "Отмена";
                    buttonYes.DialogResult = DialogResult.OK;
                    buttonCancel.DialogResult = DialogResult.Cancel;
                    buttonYes.Location = new Point(69, buttonTop);
                    buttonCancel.Location = new Point(186, buttonTop);
                    buttonYes.Size = buttonCancel.Size = new Size(75, 23);
                    break;
                case MessageBoxButtons.RetryCancel:
                    buttonYes.Text = "Повторить";
                    buttonNo.Visible = false;
                    buttonCancel.Text = "Отмена";
                    buttonYes.DialogResult = DialogResult.Retry;
                    buttonCancel.DialogResult = DialogResult.Cancel;
                    buttonYes.Location = new Point(69, buttonTop);
                    buttonCancel.Location = new Point(186, buttonTop);
                    buttonYes.Size = buttonCancel.Size = new Size(75, 23);
                    break;
                case MessageBoxButtons.YesNo:
                    buttonYes.Text = "Да";
                    buttonNo.Text = "Нет";
                    buttonCancel.Visible = false;
                    buttonYes.DialogResult = DialogResult.Yes;
                    buttonNo.DialogResult = DialogResult.No;
                    buttonYes.Location = new Point(69, buttonTop);
                    buttonNo.Location = new Point(186, buttonTop);
                    buttonYes.Size = buttonNo.Size = new Size(75, 23);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    buttonYes.Text = "Да";
                    buttonNo.Text = "Нет";
                    buttonCancel.Text = "Отмена";
                    buttonYes.DialogResult = DialogResult.Yes;
                    buttonNo.DialogResult = DialogResult.No;
                    buttonCancel.DialogResult = DialogResult.Cancel;
                    buttonYes.Location = new Point(33, buttonTop);
                    buttonNo.Location = new Point(126, buttonTop);
                    buttonCancel.Location = new Point(217, buttonTop);
                    buttonYes.Size = buttonNo.Size = buttonCancel.Size = new Size(75, 23);
                    break;
            }

            panelWhiteBackColor.Width = Width + 2;

            switch (icon)
            {
                case MessageBoxIcon.Error:                    
                    panelIcon.BackgroundImage = Properties.Resources.mb_error32;
                    break;
                case MessageBoxIcon.Question:                    
                    panelIcon.BackgroundImage = Properties.Resources.mb_question32;
                    break;
                case MessageBoxIcon.Warning:                    
                    panelIcon.BackgroundImage = Properties.Resources.mb_warning32;
                    break;
                case MessageBoxIcon.Information:                    
                    panelIcon.BackgroundImage = Properties.Resources.mb_info32;
                    break;
            }

            if (icon == MessageBoxIcon.None)
            {
                labelInfo.Left = 0;
                labelInfo.Width = Width - 15;
            }
            else
            {
                labelInfo.Left = 50;
                labelInfo.Width = Width - 65;
            }
            
            if (text.Contains("\r\n"))
            {
                labelInfo.TextAlign = ContentAlignment.TopLeft;
            }

            Size textSize = TextRenderer.MeasureText(text, labelInfo.Font);
            Size = new Size(textSize.Width + 30, textSize.Height + 120);
            if (textSize.Width > labelInfo.Width)
            {
                int height = 0;
                foreach (string s in text.Split(new[] { "\r\n" }, StringSplitOptions.None))
                {
                    textSize = TextRenderer.MeasureText(s, labelInfo.Font);
                    height += (textSize.Width / labelInfo.Width + 1) * labelInfo.Font.Height;
                }
                Height = height + 120;
            }
            
            labelInfo.Text = text;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }


        private void labelInfo_MouseEnter(object sender, EventArgs e)
        {
            if (_icon != MessageBoxIcon.Warning && _icon != MessageBoxIcon.Error)
            {
                return;
            }

            CToolTipShower.Show(Clipboard.GetText() != labelInfo.Text
                    ? "Для копирования сообщения кликните на нём дважды"
                    : "Сообщение скопировано", labelInfo, 20, -25);
        }


        private void labelInfo_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }


        private void labelInfo_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(labelInfo.Text);
            CToolTipShower.Show("Сообщение скопировано", labelInfo, 20, -25);
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter && buttonYes.Visible)
            {
                DialogResult = buttonYes.DialogResult;
                Close();
                return true;
            }

            if (keyData == Keys.Escape && buttonCancel.Visible)
            {
                DialogResult = buttonCancel.DialogResult;
                Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
