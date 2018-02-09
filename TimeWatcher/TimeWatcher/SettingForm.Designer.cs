namespace TimeWatcher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxFont = new System.Windows.Forms.TextBox();
            this.labelFont = new System.Windows.Forms.Label();
            this.buttonFont = new System.Windows.Forms.Button();
            this.checkBoxIsShowToolTips = new System.Windows.Forms.CheckBox();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Image = global::TimeWatcher.Properties.Resources.OK;
            this.buttonOK.Location = new System.Drawing.Point(30, 72);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(40, 40);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.TabStop = false;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.MouseLeave += new System.EventHandler(this.buttonOK_MouseLeave);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            this.buttonOK.MouseEnter += new System.EventHandler(this.buttonOK_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Image = global::TimeWatcher.Properties.Resources.close;
            this.buttonClose.Location = new System.Drawing.Point(213, 72);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // textBoxFont
            // 
            this.textBoxFont.Location = new System.Drawing.Point(56, 39);
            this.textBoxFont.Name = "textBoxFont";
            this.textBoxFont.Size = new System.Drawing.Size(195, 20);
            this.textBoxFont.TabIndex = 20;
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(9, 42);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(41, 13);
            this.labelFont.TabIndex = 22;
            this.labelFont.Text = "Шрифт";
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(257, 37);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(31, 23);
            this.buttonFont.TabIndex = 21;
            this.buttonFont.Text = "...";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // checkBoxIsShowToolTips
            // 
            this.checkBoxIsShowToolTips.AutoSize = true;
            this.checkBoxIsShowToolTips.Location = new System.Drawing.Point(12, 12);
            this.checkBoxIsShowToolTips.Name = "checkBoxIsShowToolTips";
            this.checkBoxIsShowToolTips.Size = new System.Drawing.Size(146, 17);
            this.checkBoxIsShowToolTips.TabIndex = 19;
            this.checkBoxIsShowToolTips.Text = "Показывать подсказки";
            this.checkBoxIsShowToolTips.UseVisualStyleBackColor = true;
            // 
            // fontDialog
            // 
            this.fontDialog.ShowColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 124);
            this.Controls.Add(this.textBoxFont);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.buttonFont);
            this.Controls.Add(this.checkBoxIsShowToolTips);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.SettingForm_InputLanguageChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox textBoxFont;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.CheckBox checkBoxIsShowToolTips;
        private System.Windows.Forms.FontDialog fontDialog;
    }
}