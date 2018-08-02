namespace SurgeryHelper.Forms
{
    partial class DictophoneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictophoneForm));
            this.textBoxNewFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPrivateFolderPath = new System.Windows.Forms.TextBox();
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.buttonStopRecording = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNewFileName
            // 
            this.textBoxNewFileName.Location = new System.Drawing.Point(182, 50);
            this.textBoxNewFileName.Name = "textBoxNewFileName";
            this.textBoxNewFileName.Size = new System.Drawing.Size(114, 20);
            this.textBoxNewFileName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "Папка, содержащая файлы с записями";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 96;
            this.label2.Text = "Имя файла для новой записи";
            // 
            // textBoxPrivateFolderPath
            // 
            this.textBoxPrivateFolderPath.Location = new System.Drawing.Point(12, 21);
            this.textBoxPrivateFolderPath.Name = "textBoxPrivateFolderPath";
            this.textBoxPrivateFolderPath.ReadOnly = true;
            this.textBoxPrivateFolderPath.Size = new System.Drawing.Size(493, 20);
            this.textBoxPrivateFolderPath.TabIndex = 0;
            this.textBoxPrivateFolderPath.MouseLeave += new System.EventHandler(this.textBoxPrivateFolderPath_MouseLeave);
            this.textBoxPrivateFolderPath.MouseEnter += new System.EventHandler(this.textBoxPrivateFolderPath_MouseEnter);
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(85, 79);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(211, 21);
            this.comboBoxDevices.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 99;
            this.label3.Text = "Устройство";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(311, 48);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(166, 13);
            this.labelInfo.TabIndex = 101;
            this.labelInfo.Text = "Диктофон готов начать запись";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(457, 66);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 98;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // buttonStartRecording
            // 
            this.buttonStartRecording.BackgroundImage = global::SurgeryHelper.Properties.Resources.start;
            this.buttonStartRecording.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonStartRecording.FlatAppearance.BorderSize = 0;
            this.buttonStartRecording.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStartRecording.Location = new System.Drawing.Point(328, 66);
            this.buttonStartRecording.Name = "buttonStartRecording";
            this.buttonStartRecording.Size = new System.Drawing.Size(40, 40);
            this.buttonStartRecording.TabIndex = 93;
            this.buttonStartRecording.TabStop = false;
            this.buttonStartRecording.UseVisualStyleBackColor = true;
            this.buttonStartRecording.MouseLeave += new System.EventHandler(this.buttonStartRecording_MouseLeave);
            this.buttonStartRecording.Click += new System.EventHandler(this.buttonStartRecording_Click);
            this.buttonStartRecording.MouseEnter += new System.EventHandler(this.buttonStartRecording_MouseEnter);
            // 
            // buttonStopRecording
            // 
            this.buttonStopRecording.BackgroundImage = global::SurgeryHelper.Properties.Resources.stop;
            this.buttonStopRecording.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonStopRecording.FlatAppearance.BorderSize = 0;
            this.buttonStopRecording.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStopRecording.Location = new System.Drawing.Point(374, 66);
            this.buttonStopRecording.Name = "buttonStopRecording";
            this.buttonStopRecording.Size = new System.Drawing.Size(40, 40);
            this.buttonStopRecording.TabIndex = 92;
            this.buttonStopRecording.TabStop = false;
            this.buttonStopRecording.UseVisualStyleBackColor = true;
            this.buttonStopRecording.Visible = false;
            this.buttonStopRecording.MouseLeave += new System.EventHandler(this.buttonStopRecording_MouseLeave);
            this.buttonStopRecording.Click += new System.EventHandler(this.buttonStopRecording_Click);
            this.buttonStopRecording.MouseEnter += new System.EventHandler(this.buttonStopRecording_MouseEnter);
            // 
            // DictophoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 109);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxPrivateFolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNewFileName);
            this.Controls.Add(this.buttonStartRecording);
            this.Controls.Add(this.buttonStopRecording);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DictophoneForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Диктофон";
            this.Shown += new System.EventHandler(this.DictophoneForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DictophoneForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartRecording;
        private System.Windows.Forms.Button buttonStopRecording;
        private System.Windows.Forms.TextBox textBoxNewFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPrivateFolderPath;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelInfo;
    }
}