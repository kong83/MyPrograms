namespace SurgeryHelper
{
    partial class DocumentsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentsForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxAdditionalDocuments = new System.Windows.Forms.ComboBox();
            this.labelAdditionalDocument = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonAdditionalDocument = new System.Windows.Forms.Button();
            this.buttonMedicalInspection = new System.Windows.Forms.Button();
            this.buttonDischargeEpicrisis = new System.Windows.Forms.Button();
            this.buttonLineOfCommEpicrisis = new System.Windows.Forms.Button();
            this.buttonTransferEpicrisis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxAdditionalDocuments
            // 
            this.comboBoxAdditionalDocuments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdditionalDocuments.FormattingEnabled = true;
            this.comboBoxAdditionalDocuments.Location = new System.Drawing.Point(65, 136);
            this.comboBoxAdditionalDocuments.MaxDropDownItems = 20;
            this.comboBoxAdditionalDocuments.Name = "comboBoxAdditionalDocuments";
            this.comboBoxAdditionalDocuments.Size = new System.Drawing.Size(277, 21);
            this.comboBoxAdditionalDocuments.TabIndex = 64;
            // 
            // labelAdditionalDocument
            // 
            this.labelAdditionalDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAdditionalDocument.Location = new System.Drawing.Point(66, 117);
            this.labelAdditionalDocument.Name = "labelAdditionalDocument";
            this.labelAdditionalDocument.Size = new System.Drawing.Size(276, 13);
            this.labelAdditionalDocument.TabIndex = 65;
            this.labelAdditionalDocument.Text = "Другой документ";
            this.labelAdditionalDocument.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAdditionalDocument.MouseLeave += new System.EventHandler(this.labelAdditionalDocument_MouseLeave);
            this.labelAdditionalDocument.MouseEnter += new System.EventHandler(this.labelAdditionalDocument_MouseEnter);
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackgroundImage = global::SurgeryHelper.Properties.Resources.help;
            this.buttonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHelp.Location = new System.Drawing.Point(20, 119);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonHelp.Size = new System.Drawing.Size(40, 40);
            this.buttonHelp.TabIndex = 66;
            this.buttonHelp.TabStop = false;
            this.buttonHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.MouseLeave += new System.EventHandler(this.buttonHelp_MouseLeave);
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            this.buttonHelp.MouseEnter += new System.EventHandler(this.buttonHelp_MouseEnter);
            // 
            // buttonAdditionalDocument
            // 
            this.buttonAdditionalDocument.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonAdditionalDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAdditionalDocument.FlatAppearance.BorderSize = 0;
            this.buttonAdditionalDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdditionalDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdditionalDocument.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdditionalDocument.Location = new System.Drawing.Point(348, 119);
            this.buttonAdditionalDocument.Name = "buttonAdditionalDocument";
            this.buttonAdditionalDocument.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonAdditionalDocument.Size = new System.Drawing.Size(40, 40);
            this.buttonAdditionalDocument.TabIndex = 65;
            this.buttonAdditionalDocument.TabStop = false;
            this.buttonAdditionalDocument.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAdditionalDocument.UseVisualStyleBackColor = true;
            this.buttonAdditionalDocument.MouseLeave += new System.EventHandler(this.buttonAdditionalDocument_MouseLeave);
            this.buttonAdditionalDocument.Click += new System.EventHandler(this.buttonAdditionalDocument_Click);
            this.buttonAdditionalDocument.MouseEnter += new System.EventHandler(this.buttonAdditionalDocument_MouseEnter);
            // 
            // buttonMedicalInspection
            // 
            this.buttonMedicalInspection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonMedicalInspection.FlatAppearance.BorderSize = 0;
            this.buttonMedicalInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMedicalInspection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMedicalInspection.Image = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonMedicalInspection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMedicalInspection.Location = new System.Drawing.Point(12, 68);
            this.buttonMedicalInspection.Name = "buttonMedicalInspection";
            this.buttonMedicalInspection.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonMedicalInspection.Size = new System.Drawing.Size(202, 36);
            this.buttonMedicalInspection.TabIndex = 62;
            this.buttonMedicalInspection.TabStop = false;
            this.buttonMedicalInspection.Text = "Осмотр при поступлении";
            this.buttonMedicalInspection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonMedicalInspection.UseVisualStyleBackColor = true;
            this.buttonMedicalInspection.MouseLeave += new System.EventHandler(this.buttonMedicalInspection_MouseLeave);
            this.buttonMedicalInspection.Click += new System.EventHandler(this.okButton_Click);
            this.buttonMedicalInspection.MouseEnter += new System.EventHandler(this.buttonMedicalInspection_MouseEnter);
            // 
            // buttonDischargeEpicrisis
            // 
            this.buttonDischargeEpicrisis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonDischargeEpicrisis.FlatAppearance.BorderSize = 0;
            this.buttonDischargeEpicrisis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDischargeEpicrisis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDischargeEpicrisis.Image = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonDischargeEpicrisis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDischargeEpicrisis.Location = new System.Drawing.Point(223, 12);
            this.buttonDischargeEpicrisis.Name = "buttonDischargeEpicrisis";
            this.buttonDischargeEpicrisis.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonDischargeEpicrisis.Size = new System.Drawing.Size(165, 36);
            this.buttonDischargeEpicrisis.TabIndex = 61;
            this.buttonDischargeEpicrisis.TabStop = false;
            this.buttonDischargeEpicrisis.Text = "Выписной эпикриз";
            this.buttonDischargeEpicrisis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDischargeEpicrisis.UseVisualStyleBackColor = true;
            this.buttonDischargeEpicrisis.MouseLeave += new System.EventHandler(this.buttonDischargeEpicrisis_MouseLeave);
            this.buttonDischargeEpicrisis.Click += new System.EventHandler(this.okButton_Click);
            this.buttonDischargeEpicrisis.MouseEnter += new System.EventHandler(this.buttonDischargeEpicrisis_MouseEnter);
            // 
            // buttonLineOfCommEpicrisis
            // 
            this.buttonLineOfCommEpicrisis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonLineOfCommEpicrisis.FlatAppearance.BorderSize = 0;
            this.buttonLineOfCommEpicrisis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLineOfCommEpicrisis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLineOfCommEpicrisis.Image = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonLineOfCommEpicrisis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLineOfCommEpicrisis.Location = new System.Drawing.Point(223, 68);
            this.buttonLineOfCommEpicrisis.Name = "buttonLineOfCommEpicrisis";
            this.buttonLineOfCommEpicrisis.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonLineOfCommEpicrisis.Size = new System.Drawing.Size(165, 36);
            this.buttonLineOfCommEpicrisis.TabIndex = 63;
            this.buttonLineOfCommEpicrisis.TabStop = false;
            this.buttonLineOfCommEpicrisis.Text = "Этапный эпикриз  ";
            this.buttonLineOfCommEpicrisis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLineOfCommEpicrisis.UseVisualStyleBackColor = true;
            this.buttonLineOfCommEpicrisis.MouseLeave += new System.EventHandler(this.buttonLineOfCommEpicrisis_MouseLeave);
            this.buttonLineOfCommEpicrisis.Click += new System.EventHandler(this.okButton_Click);
            this.buttonLineOfCommEpicrisis.MouseEnter += new System.EventHandler(this.buttonLineOfCommEpicrisis_MouseEnter);
            // 
            // buttonTransferEpicrisis
            // 
            this.buttonTransferEpicrisis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTransferEpicrisis.FlatAppearance.BorderSize = 0;
            this.buttonTransferEpicrisis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTransferEpicrisis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTransferEpicrisis.Image = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonTransferEpicrisis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTransferEpicrisis.Location = new System.Drawing.Point(12, 12);
            this.buttonTransferEpicrisis.Name = "buttonTransferEpicrisis";
            this.buttonTransferEpicrisis.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonTransferEpicrisis.Size = new System.Drawing.Size(202, 36);
            this.buttonTransferEpicrisis.TabIndex = 59;
            this.buttonTransferEpicrisis.TabStop = false;
            this.buttonTransferEpicrisis.Text = "Переводной эпикриз      ";
            this.buttonTransferEpicrisis.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonTransferEpicrisis.UseVisualStyleBackColor = true;
            this.buttonTransferEpicrisis.MouseLeave += new System.EventHandler(this.buttonTransferEpicrisis_MouseLeave);
            this.buttonTransferEpicrisis.Click += new System.EventHandler(this.okButton_Click);
            this.buttonTransferEpicrisis.MouseEnter += new System.EventHandler(this.buttonTransferEpicrisis_MouseEnter);
            // 
            // DocumentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 172);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.labelAdditionalDocument);
            this.Controls.Add(this.buttonAdditionalDocument);
            this.Controls.Add(this.comboBoxAdditionalDocuments);
            this.Controls.Add(this.buttonMedicalInspection);
            this.Controls.Add(this.buttonDischargeEpicrisis);
            this.Controls.Add(this.buttonLineOfCommEpicrisis);
            this.Controls.Add(this.buttonTransferEpicrisis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор документа для генерации";
            this.Activated += new System.EventHandler(this.DocumentsForm_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonTransferEpicrisis;
        private System.Windows.Forms.Button buttonLineOfCommEpicrisis;
        private System.Windows.Forms.Button buttonDischargeEpicrisis;
        private System.Windows.Forms.Button buttonMedicalInspection;
        private System.Windows.Forms.ComboBox comboBoxAdditionalDocuments;
        private System.Windows.Forms.Button buttonAdditionalDocument;
        private System.Windows.Forms.Label labelAdditionalDocument;
        private System.Windows.Forms.Button buttonHelp;
    }
}