namespace SurgeryHelper
{
    partial class LineOfCommunicationEpicrisisForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineOfCommunicationEpicrisisForm));
            this.buttonDocuments = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerDateWriting = new System.Windows.Forms.DateTimePicker();
            this.textBoxPlan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAdditionalInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDocuments
            // 
            this.buttonDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDocuments.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonDocuments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDocuments.FlatAppearance.BorderSize = 0;
            this.buttonDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDocuments.Location = new System.Drawing.Point(143, 166);
            this.buttonDocuments.Name = "buttonDocuments";
            this.buttonDocuments.Size = new System.Drawing.Size(40, 40);
            this.buttonDocuments.TabIndex = 90;
            this.buttonDocuments.TabStop = false;
            this.buttonDocuments.UseVisualStyleBackColor = true;
            this.buttonDocuments.MouseLeave += new System.EventHandler(this.buttonDocuments_MouseLeave);
            this.buttonDocuments.Click += new System.EventHandler(this.buttonDocuments_Click);
            this.buttonDocuments.MouseEnter += new System.EventHandler(this.buttonDocuments_MouseEnter);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(241, 166);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 89;
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
            this.buttonClose.Location = new System.Drawing.Point(301, 166);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 88;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 28);
            this.label4.TabIndex = 98;
            this.label4.Text = "Дата создания документа";
            // 
            // dateTimePickerDateWriting
            // 
            this.dateTimePickerDateWriting.Checked = false;
            this.dateTimePickerDateWriting.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerDateWriting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerDateWriting.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDateWriting.Location = new System.Drawing.Point(143, 135);
            this.dateTimePickerDateWriting.Name = "dateTimePickerDateWriting";
            this.dateTimePickerDateWriting.Size = new System.Drawing.Size(95, 20);
            this.dateTimePickerDateWriting.TabIndex = 94;
            // 
            // textBoxPlan
            // 
            this.textBoxPlan.Location = new System.Drawing.Point(143, 101);
            this.textBoxPlan.Name = "textBoxPlan";
            this.textBoxPlan.Size = new System.Drawing.Size(198, 20);
            this.textBoxPlan.TabIndex = 93;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 17);
            this.label3.TabIndex = 97;
            this.label3.Text = "Планируется";
            // 
            // textBoxAdditionalInfo
            // 
            this.textBoxAdditionalInfo.Location = new System.Drawing.Point(143, 12);
            this.textBoxAdditionalInfo.MaxLength = 200000;
            this.textBoxAdditionalInfo.Multiline = true;
            this.textBoxAdditionalInfo.Name = "textBoxAdditionalInfo";
            this.textBoxAdditionalInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxAdditionalInfo.Size = new System.Drawing.Size(198, 78);
            this.textBoxAdditionalInfo.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 32);
            this.label1.TabIndex = 95;
            this.label1.Text = "Дополнительная\r\nинформация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LineOfCommunicationEpicrisisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 215);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerDateWriting);
            this.Controls.Add(this.textBoxPlan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxAdditionalInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDocuments);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LineOfCommunicationEpicrisisForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Этапный эпикриз";
            this.Load += new System.EventHandler(this.LineOfCommunicationEpicrisisForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LineOfCommunicationEpicrisisForm_FormClosing);
            this.LocationChanged += new System.EventHandler(this.LineOfCommunicationEpicrisisForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDocuments;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateWriting;
        private System.Windows.Forms.TextBox textBoxPlan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAdditionalInfo;
        private System.Windows.Forms.Label label1;
    }
}