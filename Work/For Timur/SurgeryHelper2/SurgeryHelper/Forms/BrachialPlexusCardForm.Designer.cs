namespace SurgeryHelper.Forms
{
    partial class BrachialPlexusCardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrachialPlexusCardForm));
            this.buttonDocuments = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSide = new System.Windows.Forms.ComboBox();
            this.textBoxEMNG = new System.Windows.Forms.TextBox();
            this.comboBoxHornersSyndrom = new System.Windows.Forms.ComboBox();
            this.comboBoxMielographyType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerEMNGDate = new System.Windows.Forms.DateTimePicker();
            this.checkBoxIsEMNGEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerMielographiDate = new System.Windows.Forms.DateTimePicker();
            this.checkBoxIsMielographyEnabled = new System.Windows.Forms.CheckBox();
            this.textBoxMielography = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDiafragm = new System.Windows.Forms.TextBox();
            this.textBoxTinnelsSymptome = new System.Windows.Forms.TextBox();
            this.textBoxVascularStatus = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDocuments
            // 
            this.buttonDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDocuments.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonDocuments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDocuments.FlatAppearance.BorderSize = 0;
            this.buttonDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDocuments.Location = new System.Drawing.Point(8, 358);
            this.buttonDocuments.Name = "buttonDocuments";
            this.buttonDocuments.Size = new System.Drawing.Size(40, 40);
            this.buttonDocuments.TabIndex = 96;
            this.buttonDocuments.TabStop = false;
            this.buttonDocuments.UseVisualStyleBackColor = true;
            this.buttonDocuments.Click += new System.EventHandler(this.buttonDocuments_Click);
            this.buttonDocuments.MouseEnter += new System.EventHandler(this.buttonDocuments_MouseEnter);
            this.buttonDocuments.MouseLeave += new System.EventHandler(this.buttonDocuments_MouseLeave);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(272, 358);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 95;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(337, 358);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 94;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(390, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 392);
            this.panel1.TabIndex = 99;
            this.panel1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Сторона";
            // 
            // comboBoxSide
            // 
            this.comboBoxSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSide.FormattingEnabled = true;
            this.comboBoxSide.Items.AddRange(new object[] {
            "Левая сторона",
            "Правая сторона"});
            this.comboBoxSide.Location = new System.Drawing.Point(117, 6);
            this.comboBoxSide.Name = "comboBoxSide";
            this.comboBoxSide.Size = new System.Drawing.Size(110, 21);
            this.comboBoxSide.TabIndex = 0;
            // 
            // textBoxEMNG
            // 
            this.textBoxEMNG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEMNG.Enabled = false;
            this.textBoxEMNG.Location = new System.Drawing.Point(88, 13);
            this.textBoxEMNG.Multiline = true;
            this.textBoxEMNG.Name = "textBoxEMNG";
            this.textBoxEMNG.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEMNG.Size = new System.Drawing.Size(281, 59);
            this.textBoxEMNG.TabIndex = 4;
            // 
            // comboBoxHornersSyndrom
            // 
            this.comboBoxHornersSyndrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHornersSyndrom.FormattingEnabled = true;
            this.comboBoxHornersSyndrom.Items.AddRange(new object[] {
            "нет",
            "S",
            "D"});
            this.comboBoxHornersSyndrom.Location = new System.Drawing.Point(117, 33);
            this.comboBoxHornersSyndrom.Name = "comboBoxHornersSyndrom";
            this.comboBoxHornersSyndrom.Size = new System.Drawing.Size(44, 21);
            this.comboBoxHornersSyndrom.TabIndex = 2;
            // 
            // comboBoxMielographyType
            // 
            this.comboBoxMielographyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMielographyType.Enabled = false;
            this.comboBoxMielographyType.FormattingEnabled = true;
            this.comboBoxMielographyType.Items.AddRange(new object[] {
            "КТ",
            "МРТ"});
            this.comboBoxMielographyType.Location = new System.Drawing.Point(6, 21);
            this.comboBoxMielographyType.Name = "comboBoxMielographyType";
            this.comboBoxMielographyType.Size = new System.Drawing.Size(78, 21);
            this.comboBoxMielographyType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Сосудистый статус";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerEMNGDate);
            this.groupBox1.Controls.Add(this.checkBoxIsEMNGEnabled);
            this.groupBox1.Controls.Add(this.textBoxEMNG);
            this.groupBox1.Location = new System.Drawing.Point(8, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 78);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // dateTimePickerEMNGDate
            // 
            this.dateTimePickerEMNGDate.CustomFormat = "dd.MM.yyy";
            this.dateTimePickerEMNGDate.Enabled = false;
            this.dateTimePickerEMNGDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEMNGDate.Location = new System.Drawing.Point(6, 31);
            this.dateTimePickerEMNGDate.Name = "dateTimePickerEMNGDate";
            this.dateTimePickerEMNGDate.Size = new System.Drawing.Size(79, 20);
            this.dateTimePickerEMNGDate.TabIndex = 2;
            // 
            // checkBoxIsEMNGEnabled
            // 
            this.checkBoxIsEMNGEnabled.AutoSize = true;
            this.checkBoxIsEMNGEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxIsEMNGEnabled.Location = new System.Drawing.Point(6, -1);
            this.checkBoxIsEMNGEnabled.Name = "checkBoxIsEMNGEnabled";
            this.checkBoxIsEMNGEnabled.Size = new System.Drawing.Size(60, 17);
            this.checkBoxIsEMNGEnabled.TabIndex = 0;
            this.checkBoxIsEMNGEnabled.Text = "ЭМНГ";
            this.checkBoxIsEMNGEnabled.UseVisualStyleBackColor = true;
            this.checkBoxIsEMNGEnabled.CheckedChanged += new System.EventHandler(this.checkBoxIsEMNGEnabled_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimePickerMielographiDate);
            this.groupBox2.Controls.Add(this.checkBoxIsMielographyEnabled);
            this.groupBox2.Controls.Add(this.textBoxMielography);
            this.groupBox2.Controls.Add(this.comboBoxMielographyType);
            this.groupBox2.Location = new System.Drawing.Point(10, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 87);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // dateTimePickerMielographiDate
            // 
            this.dateTimePickerMielographiDate.CustomFormat = "dd.MM.yyy";
            this.dateTimePickerMielographiDate.Enabled = false;
            this.dateTimePickerMielographiDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerMielographiDate.Location = new System.Drawing.Point(6, 55);
            this.dateTimePickerMielographiDate.Name = "dateTimePickerMielographiDate";
            this.dateTimePickerMielographiDate.Size = new System.Drawing.Size(78, 20);
            this.dateTimePickerMielographiDate.TabIndex = 4;
            // 
            // checkBoxIsMielographyEnabled
            // 
            this.checkBoxIsMielographyEnabled.AutoSize = true;
            this.checkBoxIsMielographyEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxIsMielographyEnabled.Location = new System.Drawing.Point(6, -1);
            this.checkBoxIsMielographyEnabled.Name = "checkBoxIsMielographyEnabled";
            this.checkBoxIsMielographyEnabled.Size = new System.Drawing.Size(107, 17);
            this.checkBoxIsMielographyEnabled.TabIndex = 0;
            this.checkBoxIsMielographyEnabled.Text = "Миелография";
            this.checkBoxIsMielographyEnabled.UseVisualStyleBackColor = true;
            this.checkBoxIsMielographyEnabled.CheckedChanged += new System.EventHandler(this.checkBoxIsMielographyEnabled_CheckedChanged);
            // 
            // textBoxMielography
            // 
            this.textBoxMielography.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMielography.Enabled = false;
            this.textBoxMielography.Location = new System.Drawing.Point(90, 19);
            this.textBoxMielography.Multiline = true;
            this.textBoxMielography.Name = "textBoxMielography";
            this.textBoxMielography.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMielography.Size = new System.Drawing.Size(277, 59);
            this.textBoxMielography.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 110;
            this.label4.Text = "Синдром Горнера";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "Диафрагма";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 112;
            this.label6.Text = "Симптом Тинеля";
            // 
            // textBoxDiafragm
            // 
            this.textBoxDiafragm.Location = new System.Drawing.Point(117, 92);
            this.textBoxDiafragm.Name = "textBoxDiafragm";
            this.textBoxDiafragm.Size = new System.Drawing.Size(266, 20);
            this.textBoxDiafragm.TabIndex = 6;
            // 
            // textBoxTinnelsSymptome
            // 
            this.textBoxTinnelsSymptome.Location = new System.Drawing.Point(117, 121);
            this.textBoxTinnelsSymptome.Name = "textBoxTinnelsSymptome";
            this.textBoxTinnelsSymptome.Size = new System.Drawing.Size(266, 20);
            this.textBoxTinnelsSymptome.TabIndex = 8;
            // 
            // textBoxVascularStatus
            // 
            this.textBoxVascularStatus.Location = new System.Drawing.Point(117, 63);
            this.textBoxVascularStatus.Name = "textBoxVascularStatus";
            this.textBoxVascularStatus.Size = new System.Drawing.Size(266, 20);
            this.textBoxVascularStatus.TabIndex = 4;
            // 
            // BrachialPlexusCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 405);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxVascularStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxTinnelsSymptome);
            this.Controls.Add(this.textBoxDiafragm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxHornersSyndrom);
            this.Controls.Add(this.comboBoxSide);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonDocuments);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrachialPlexusCardForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Карта обследования пациента с повреждением плечевого сплетения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrachialPlexusCardForm_FormClosing);
            this.Load += new System.EventHandler(this.BrachialPlexusCardForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDocuments;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSide;
        private System.Windows.Forms.TextBox textBoxEMNG;
        private System.Windows.Forms.ComboBox comboBoxHornersSyndrom;
        private System.Windows.Forms.ComboBox comboBoxMielographyType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxIsEMNGEnabled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxIsMielographyEnabled;
        private System.Windows.Forms.DateTimePicker dateTimePickerEMNGDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerMielographiDate;
        private System.Windows.Forms.TextBox textBoxMielography;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDiafragm;
        private System.Windows.Forms.TextBox textBoxTinnelsSymptome;
        private System.Windows.Forms.TextBox textBoxVascularStatus;
    }
}