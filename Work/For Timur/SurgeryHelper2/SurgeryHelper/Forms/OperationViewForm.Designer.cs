namespace SurgeryHelper.Forms
{
    partial class OperationViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationViewForm));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerDateOfOperation = new System.Windows.Forms.DateTimePicker();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSurgeons = new System.Windows.Forms.TextBox();
            this.textBoxAssistents = new System.Windows.Forms.TextBox();
            this.textBoxHeAnestethist = new System.Windows.Forms.TextBox();
            this.textBoxSheAnestethist = new System.Windows.Forms.TextBox();
            this.linkLabelSurgeonList = new System.Windows.Forms.LinkLabel();
            this.linkLabelAssistentList = new System.Windows.Forms.LinkLabel();
            this.linkLabelScrubNurseList = new System.Windows.Forms.LinkLabel();
            this.linkLabelOrderlyList = new System.Windows.Forms.LinkLabel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonProtocol = new System.Windows.Forms.Button();
            this.comboBoxScrubNurse = new System.Windows.Forms.ComboBox();
            this.comboBoxOrderly = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerEndTimeOfOperation = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartTimeOfOperation = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxOperationTypes = new System.Windows.Forms.TextBox();
            this.linkLabelOperationType = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 26);
            this.label1.TabIndex = 59;
            this.label1.Text = "* Название\r\n   операции";
            // 
            // dateTimePickerDateOfOperation
            // 
            this.dateTimePickerDateOfOperation.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerDateOfOperation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateOfOperation.Location = new System.Drawing.Point(120, 74);
            this.dateTimePickerDateOfOperation.Name = "dateTimePickerDateOfOperation";
            this.dateTimePickerDateOfOperation.Size = new System.Drawing.Size(88, 20);
            this.dateTimePickerDateOfOperation.TabIndex = 3;
            this.dateTimePickerDateOfOperation.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(120, 12);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxName.Size = new System.Drawing.Size(225, 51);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "* Дата операции";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "Анестезиолог";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "Анестезистка";
            // 
            // textBoxSurgeons
            // 
            this.textBoxSurgeons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSurgeons.Location = new System.Drawing.Point(120, 167);
            this.textBoxSurgeons.MaxLength = 200000;
            this.textBoxSurgeons.Multiline = true;
            this.textBoxSurgeons.Name = "textBoxSurgeons";
            this.textBoxSurgeons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSurgeons.Size = new System.Drawing.Size(225, 67);
            this.textBoxSurgeons.TabIndex = 11;
            this.textBoxSurgeons.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxAssistents
            // 
            this.textBoxAssistents.Location = new System.Drawing.Point(491, 12);
            this.textBoxAssistents.MaxLength = 200000;
            this.textBoxAssistents.Multiline = true;
            this.textBoxAssistents.Name = "textBoxAssistents";
            this.textBoxAssistents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAssistents.Size = new System.Drawing.Size(225, 51);
            this.textBoxAssistents.TabIndex = 15;
            this.textBoxAssistents.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxHeAnestethist
            // 
            this.textBoxHeAnestethist.Location = new System.Drawing.Point(491, 69);
            this.textBoxHeAnestethist.Name = "textBoxHeAnestethist";
            this.textBoxHeAnestethist.Size = new System.Drawing.Size(225, 20);
            this.textBoxHeAnestethist.TabIndex = 17;
            this.textBoxHeAnestethist.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxSheAnestethist
            // 
            this.textBoxSheAnestethist.Location = new System.Drawing.Point(491, 95);
            this.textBoxSheAnestethist.Name = "textBoxSheAnestethist";
            this.textBoxSheAnestethist.Size = new System.Drawing.Size(225, 20);
            this.textBoxSheAnestethist.TabIndex = 19;
            this.textBoxSheAnestethist.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // linkLabelSurgeonList
            // 
            this.linkLabelSurgeonList.AutoSize = true;
            this.linkLabelSurgeonList.Location = new System.Drawing.Point(12, 195);
            this.linkLabelSurgeonList.Name = "linkLabelSurgeonList";
            this.linkLabelSurgeonList.Size = new System.Drawing.Size(99, 13);
            this.linkLabelSurgeonList.TabIndex = 9;
            this.linkLabelSurgeonList.TabStop = true;
            this.linkLabelSurgeonList.Text = "* Список хирургов";
            this.linkLabelSurgeonList.MouseLeave += new System.EventHandler(this.linkLabelSurgeonList_MouseLeave);
            this.linkLabelSurgeonList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSurgeonList_LinkClicked);
            this.linkLabelSurgeonList.MouseEnter += new System.EventHandler(this.linkLabelSurgeonList_MouseEnter);
            // 
            // linkLabelAssistentList
            // 
            this.linkLabelAssistentList.AutoSize = true;
            this.linkLabelAssistentList.Location = new System.Drawing.Point(362, 31);
            this.linkLabelAssistentList.Name = "linkLabelAssistentList";
            this.linkLabelAssistentList.Size = new System.Drawing.Size(111, 13);
            this.linkLabelAssistentList.TabIndex = 13;
            this.linkLabelAssistentList.TabStop = true;
            this.linkLabelAssistentList.Text = "Список ассистентов";
            this.linkLabelAssistentList.MouseLeave += new System.EventHandler(this.linkLabelAssistentList_MouseLeave);
            this.linkLabelAssistentList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAssistentList_LinkClicked);
            this.linkLabelAssistentList.MouseEnter += new System.EventHandler(this.linkLabelAssistentList_MouseEnter);
            // 
            // linkLabelScrubNurseList
            // 
            this.linkLabelScrubNurseList.AutoSize = true;
            this.linkLabelScrubNurseList.Location = new System.Drawing.Point(362, 124);
            this.linkLabelScrubNurseList.Name = "linkLabelScrubNurseList";
            this.linkLabelScrubNurseList.Size = new System.Drawing.Size(119, 13);
            this.linkLabelScrubNurseList.TabIndex = 21;
            this.linkLabelScrubNurseList.TabStop = true;
            this.linkLabelScrubNurseList.Text = "* Операц. мед. сестра";
            this.linkLabelScrubNurseList.MouseLeave += new System.EventHandler(this.linkLabelScrubNurseList_MouseLeave);
            this.linkLabelScrubNurseList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelScrubNurseList_LinkClicked);
            this.linkLabelScrubNurseList.MouseEnter += new System.EventHandler(this.linkLabelScrubNurseList_MouseEnter);
            // 
            // linkLabelOrderlyList
            // 
            this.linkLabelOrderlyList.AutoSize = true;
            this.linkLabelOrderlyList.Location = new System.Drawing.Point(362, 151);
            this.linkLabelOrderlyList.Name = "linkLabelOrderlyList";
            this.linkLabelOrderlyList.Size = new System.Drawing.Size(56, 13);
            this.linkLabelOrderlyList.TabIndex = 25;
            this.linkLabelOrderlyList.TabStop = true;
            this.linkLabelOrderlyList.Text = "* Санитар";
            this.linkLabelOrderlyList.MouseLeave += new System.EventHandler(this.linkLabelOrderlyList_MouseLeave);
            this.linkLabelOrderlyList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOrderlyList_LinkClicked);
            this.linkLabelOrderlyList.MouseEnter += new System.EventHandler(this.linkLabelOrderlyList_MouseEnter);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(501, 240);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 58;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(578, 240);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 57;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonProtocol
            // 
            this.buttonProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonProtocol.BackgroundImage = global::SurgeryHelper.Properties.Resources.DOCUMENT;
            this.buttonProtocol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonProtocol.FlatAppearance.BorderSize = 0;
            this.buttonProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProtocol.Location = new System.Drawing.Point(133, 240);
            this.buttonProtocol.Name = "buttonProtocol";
            this.buttonProtocol.Size = new System.Drawing.Size(40, 40);
            this.buttonProtocol.TabIndex = 67;
            this.buttonProtocol.TabStop = false;
            this.buttonProtocol.UseVisualStyleBackColor = true;
            this.buttonProtocol.MouseLeave += new System.EventHandler(this.buttonProtocol_MouseLeave);
            this.buttonProtocol.Click += new System.EventHandler(this.buttonProtocol_Click);
            this.buttonProtocol.MouseEnter += new System.EventHandler(this.buttonProtocol_MouseEnter);
            // 
            // comboBoxScrubNurse
            // 
            this.comboBoxScrubNurse.FormattingEnabled = true;
            this.comboBoxScrubNurse.Location = new System.Drawing.Point(491, 121);
            this.comboBoxScrubNurse.Name = "comboBoxScrubNurse";
            this.comboBoxScrubNurse.Size = new System.Drawing.Size(225, 21);
            this.comboBoxScrubNurse.TabIndex = 23;
            this.comboBoxScrubNurse.TextChanged += new System.EventHandler(this.comboBox_TextChanged);
            // 
            // comboBoxOrderly
            // 
            this.comboBoxOrderly.FormattingEnabled = true;
            this.comboBoxOrderly.Location = new System.Drawing.Point(491, 148);
            this.comboBoxOrderly.Name = "comboBoxOrderly";
            this.comboBoxOrderly.Size = new System.Drawing.Size(225, 21);
            this.comboBoxOrderly.TabIndex = 27;
            this.comboBoxOrderly.TextChanged += new System.EventHandler(this.comboBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 26);
            this.label3.TabIndex = 70;
            this.label3.Text = "Время окончания \r\nоперации";
            // 
            // dateTimePickerEndTimeOfOperation
            // 
            this.dateTimePickerEndTimeOfOperation.Checked = false;
            this.dateTimePickerEndTimeOfOperation.CustomFormat = "HH:mm";
            this.dateTimePickerEndTimeOfOperation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTimeOfOperation.Location = new System.Drawing.Point(120, 136);
            this.dateTimePickerEndTimeOfOperation.Name = "dateTimePickerEndTimeOfOperation";
            this.dateTimePickerEndTimeOfOperation.ShowCheckBox = true;
            this.dateTimePickerEndTimeOfOperation.ShowUpDown = true;
            this.dateTimePickerEndTimeOfOperation.Size = new System.Drawing.Size(88, 20);
            this.dateTimePickerEndTimeOfOperation.TabIndex = 7;
            this.dateTimePickerEndTimeOfOperation.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // dateTimePickerStartTimeOfOperation
            // 
            this.dateTimePickerStartTimeOfOperation.CustomFormat = "HH:mm";
            this.dateTimePickerStartTimeOfOperation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTimeOfOperation.Location = new System.Drawing.Point(120, 105);
            this.dateTimePickerStartTimeOfOperation.Name = "dateTimePickerStartTimeOfOperation";
            this.dateTimePickerStartTimeOfOperation.ShowUpDown = true;
            this.dateTimePickerStartTimeOfOperation.Size = new System.Drawing.Size(88, 20);
            this.dateTimePickerStartTimeOfOperation.TabIndex = 5;
            this.dateTimePickerStartTimeOfOperation.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 26);
            this.label4.TabIndex = 73;
            this.label4.Text = "* Время начала \r\n   операции";
            // 
            // textBoxOperationTypes
            // 
            this.textBoxOperationTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOperationTypes.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxOperationTypes.Location = new System.Drawing.Point(491, 175);
            this.textBoxOperationTypes.MaxLength = 200000;
            this.textBoxOperationTypes.Multiline = true;
            this.textBoxOperationTypes.Name = "textBoxOperationTypes";
            this.textBoxOperationTypes.ReadOnly = true;
            this.textBoxOperationTypes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOperationTypes.Size = new System.Drawing.Size(225, 59);
            this.textBoxOperationTypes.TabIndex = 74;
            this.textBoxOperationTypes.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // linkLabelOperationType
            // 
            this.linkLabelOperationType.AutoSize = true;
            this.linkLabelOperationType.Location = new System.Drawing.Point(362, 195);
            this.linkLabelOperationType.Name = "linkLabelOperationType";
            this.linkLabelOperationType.Size = new System.Drawing.Size(85, 13);
            this.linkLabelOperationType.TabIndex = 75;
            this.linkLabelOperationType.TabStop = true;
            this.linkLabelOperationType.Text = "Типы операции";
            this.linkLabelOperationType.MouseLeave += new System.EventHandler(this.linkLabelOperationType_MouseLeave);
            this.linkLabelOperationType.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOperationType_LinkClicked);
            this.linkLabelOperationType.MouseEnter += new System.EventHandler(this.linkLabelOperationType_MouseEnter);
            // 
            // OperationViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 284);
            this.Controls.Add(this.linkLabelOperationType);
            this.Controls.Add(this.textBoxOperationTypes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerStartTimeOfOperation);
            this.Controls.Add(this.dateTimePickerEndTimeOfOperation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxOrderly);
            this.Controls.Add(this.comboBoxScrubNurse);
            this.Controls.Add(this.buttonProtocol);
            this.Controls.Add(this.linkLabelOrderlyList);
            this.Controls.Add(this.linkLabelScrubNurseList);
            this.Controls.Add(this.linkLabelAssistentList);
            this.Controls.Add(this.linkLabelSurgeonList);
            this.Controls.Add(this.textBoxSheAnestethist);
            this.Controls.Add(this.textBoxHeAnestethist);
            this.Controls.Add(this.textBoxAssistents);
            this.Controls.Add(this.textBoxSurgeons);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.dateTimePickerDateOfOperation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OperationViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OperationViewForm";
            this.Load += new System.EventHandler(this.OperationViewForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OperationViewForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateOfOperation;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSurgeons;
        private System.Windows.Forms.TextBox textBoxAssistents;
        private System.Windows.Forms.TextBox textBoxHeAnestethist;
        private System.Windows.Forms.TextBox textBoxSheAnestethist;
        private System.Windows.Forms.LinkLabel linkLabelSurgeonList;
        private System.Windows.Forms.LinkLabel linkLabelAssistentList;
        private System.Windows.Forms.LinkLabel linkLabelScrubNurseList;
        private System.Windows.Forms.LinkLabel linkLabelOrderlyList;
        private System.Windows.Forms.Button buttonProtocol;
        private System.Windows.Forms.ComboBox comboBoxScrubNurse;
        private System.Windows.Forms.ComboBox comboBoxOrderly;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTimeOfOperation;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTimeOfOperation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxOperationTypes;
        private System.Windows.Forms.LinkLabel linkLabelOperationType;
    }
}