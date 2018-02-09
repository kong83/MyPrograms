namespace Fonotec
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxIsShowToolTips = new System.Windows.Forms.CheckBox();
            this.checkBoxIsCloseDiskForm = new System.Windows.Forms.CheckBox();
            this.checkBoxIsCloseFilmForm = new System.Windows.Forms.CheckBox();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.buttonFont = new System.Windows.Forms.Button();
            this.labelFont = new System.Windows.Forms.Label();
            this.textBoxFont = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.BackgroundImage = global::Fonotec.Properties.Resources.close;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(159, 124);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(40, 40);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.BackgroundImage = global::Fonotec.Properties.Resources.ok;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(84, 124);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(40, 40);
            this.buttonOK.TabIndex = 20;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxIsShowToolTips
            // 
            this.checkBoxIsShowToolTips.AutoSize = true;
            this.checkBoxIsShowToolTips.Location = new System.Drawing.Point(17, 14);
            this.checkBoxIsShowToolTips.Name = "checkBoxIsShowToolTips";
            this.checkBoxIsShowToolTips.Size = new System.Drawing.Size(146, 17);
            this.checkBoxIsShowToolTips.TabIndex = 0;
            this.checkBoxIsShowToolTips.Text = "Показывать подсказки";
            this.checkBoxIsShowToolTips.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsCloseDiskForm
            // 
            this.checkBoxIsCloseDiskForm.AutoSize = true;
            this.checkBoxIsCloseDiskForm.Location = new System.Drawing.Point(17, 37);
            this.checkBoxIsCloseDiskForm.Name = "checkBoxIsCloseDiskForm";
            this.checkBoxIsCloseDiskForm.Size = new System.Drawing.Size(276, 17);
            this.checkBoxIsCloseDiskForm.TabIndex = 2;
            this.checkBoxIsCloseDiskForm.Text = "Закрывать окно после добавления нового диска";
            this.checkBoxIsCloseDiskForm.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsCloseFilmForm
            // 
            this.checkBoxIsCloseFilmForm.AutoSize = true;
            this.checkBoxIsCloseFilmForm.Location = new System.Drawing.Point(17, 60);
            this.checkBoxIsCloseFilmForm.Name = "checkBoxIsCloseFilmForm";
            this.checkBoxIsCloseFilmForm.Size = new System.Drawing.Size(286, 17);
            this.checkBoxIsCloseFilmForm.TabIndex = 4;
            this.checkBoxIsCloseFilmForm.Text = "Закрывать окно после добавления нового фильма";
            this.checkBoxIsCloseFilmForm.UseVisualStyleBackColor = true;
            // 
            // fontDialog
            // 
            this.fontDialog.AllowScriptChange = false;
            this.fontDialog.ShowColor = true;
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(262, 83);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(31, 23);
            this.buttonFont.TabIndex = 8;
            this.buttonFont.Text = "...";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(14, 88);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(41, 13);
            this.labelFont.TabIndex = 18;
            this.labelFont.Text = "Шрифт";
            // 
            // textBoxFont
            // 
            this.textBoxFont.Location = new System.Drawing.Point(61, 85);
            this.textBoxFont.Name = "textBoxFont";
            this.textBoxFont.Size = new System.Drawing.Size(195, 20);
            this.textBoxFont.TabIndex = 6;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 176);
            this.Controls.Add(this.textBoxFont);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.buttonFont);
            this.Controls.Add(this.checkBoxIsCloseFilmForm);
            this.Controls.Add(this.checkBoxIsCloseDiskForm);
            this.Controls.Add(this.checkBoxIsShowToolTips);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки программы";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.SettingForm_InputLanguageChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxIsShowToolTips;
        private System.Windows.Forms.CheckBox checkBoxIsCloseDiskForm;
        private System.Windows.Forms.CheckBox checkBoxIsCloseFilmForm;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.TextBox textBoxFont;
    }
}