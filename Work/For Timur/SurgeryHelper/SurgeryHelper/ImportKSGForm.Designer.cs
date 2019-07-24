namespace SurgeryHelper
{
    partial class ImportKSGForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportKSGForm));
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpenFilePath = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxFileType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilePath.Location = new System.Drawing.Point(12, 39);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(519, 20);
            this.textBoxFilePath.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(224, 73);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(279, 73);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            // 
            // buttonOpenNightKSG
            // 
            this.buttonOpenFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenFilePath.BackgroundImage = global::SurgeryHelper.Properties.Resources.open16;
            this.buttonOpenFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOpenFilePath.FlatAppearance.BorderSize = 0;
            this.buttonOpenFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenFilePath.Location = new System.Drawing.Point(537, 38);
            this.buttonOpenFilePath.Name = "buttonOpenNightKSG";
            this.buttonOpenFilePath.Size = new System.Drawing.Size(20, 20);
            this.buttonOpenFilePath.TabIndex = 4;
            this.buttonOpenFilePath.TabStop = false;
            this.buttonOpenFilePath.UseVisualStyleBackColor = true;
            this.buttonOpenFilePath.Click += new System.EventHandler(this.buttonOpenNightKSG_Click);
            this.buttonOpenFilePath.MouseEnter += new System.EventHandler(this.buttonOpenFilePath_MouseEnter);
            this.buttonOpenFilePath.MouseLeave += new System.EventHandler(this.buttonOpenFilePath_MouseLeave);
            // 
            // comboBoxFileType
            // 
            this.comboBoxFileType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFileType.FormattingEnabled = true;
            this.comboBoxFileType.Items.AddRange(new object[] {
            "Путь до xml файла с информацией о дневных кодах КСГ",
            "Путь до xml файла с информацией о ночных кодах КСГ",
            "Путь до xml файла с информацией о МКБ"});
            this.comboBoxFileType.Location = new System.Drawing.Point(12, 12);
            this.comboBoxFileType.Name = "comboBoxFileType";
            this.comboBoxFileType.Size = new System.Drawing.Size(389, 21);
            this.comboBoxFileType.TabIndex = 0;
            // 
            // ImportKSGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 120);
            this.Controls.Add(this.comboBoxFileType);
            this.Controls.Add(this.buttonOpenFilePath);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportKSGForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Импорт информации о КГС, услугах и МКБ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOpenFilePath;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox comboBoxFileType;
    }
}