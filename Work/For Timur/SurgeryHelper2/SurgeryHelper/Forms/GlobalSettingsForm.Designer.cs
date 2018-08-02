namespace SurgeryHelper.Forms
{
    partial class GlobalSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalSettingsForm));
            this.textBoxBranchManager = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDepartmentName = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHeAnaesthetist = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSheAnaesthetist = new System.Windows.Forms.TextBox();
            this.checkBoxShowDbIndexes = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDischargeEpicrisisHeader = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxBranchManager
            // 
            this.textBoxBranchManager.Location = new System.Drawing.Point(12, 25);
            this.textBoxBranchManager.Name = "textBoxBranchManager";
            this.textBoxBranchManager.Size = new System.Drawing.Size(261, 20);
            this.textBoxBranchManager.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "* Фамилия и инициалы заведующего отделением";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "* Название (номер) отделения";
            // 
            // textBoxDepartmentName
            // 
            this.textBoxDepartmentName.Location = new System.Drawing.Point(12, 75);
            this.textBoxDepartmentName.Name = "textBoxDepartmentName";
            this.textBoxDepartmentName.Size = new System.Drawing.Size(261, 20);
            this.textBoxDepartmentName.TabIndex = 2;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(75, 291);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 10;
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
            this.buttonClose.Location = new System.Drawing.Point(160, 291);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "* Анестезиолог (по умолчанию)";
            // 
            // textBoxHeAnaesthetist
            // 
            this.textBoxHeAnaesthetist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxHeAnaesthetist.Location = new System.Drawing.Point(12, 175);
            this.textBoxHeAnaesthetist.Name = "textBoxHeAnaesthetist";
            this.textBoxHeAnaesthetist.Size = new System.Drawing.Size(261, 20);
            this.textBoxHeAnaesthetist.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "* Анестезистка (по умолчанию)";
            // 
            // textBoxSheAnaesthetist
            // 
            this.textBoxSheAnaesthetist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSheAnaesthetist.Location = new System.Drawing.Point(12, 225);
            this.textBoxSheAnaesthetist.Name = "textBoxSheAnaesthetist";
            this.textBoxSheAnaesthetist.Size = new System.Drawing.Size(261, 20);
            this.textBoxSheAnaesthetist.TabIndex = 6;
            // 
            // checkBoxShowDbIndexes
            // 
            this.checkBoxShowDbIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowDbIndexes.AutoSize = true;
            this.checkBoxShowDbIndexes.Location = new System.Drawing.Point(12, 265);
            this.checkBoxShowDbIndexes.Name = "checkBoxShowDbIndexes";
            this.checkBoxShowDbIndexes.Size = new System.Drawing.Size(225, 17);
            this.checkBoxShowDbIndexes.TabIndex = 71;
            this.checkBoxShowDbIndexes.Text = "Отображать индексы пациентов из БД";
            this.checkBoxShowDbIndexes.UseVisualStyleBackColor = true;
            this.checkBoxShowDbIndexes.MouseLeave += new System.EventHandler(this.checkBoxShowDbIndexes_MouseLeave);
            this.checkBoxShowDbIndexes.MouseEnter += new System.EventHandler(this.checkBoxShowDbIndexes_MouseEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 13);
            this.label5.TabIndex = 73;
            this.label5.Text = "* Имя файла с шапкой для выписного эпикриза";
            // 
            // textBoxDischargeEpicrisisHeader
            // 
            this.textBoxDischargeEpicrisisHeader.Location = new System.Drawing.Point(15, 125);
            this.textBoxDischargeEpicrisisHeader.Name = "textBoxDischargeEpicrisisHeader";
            this.textBoxDischargeEpicrisisHeader.Size = new System.Drawing.Size(261, 20);
            this.textBoxDischargeEpicrisisHeader.TabIndex = 72;
            // 
            // GlobalSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 343);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDischargeEpicrisisHeader);
            this.Controls.Add(this.checkBoxShowDbIndexes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSheAnaesthetist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxHeAnaesthetist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDepartmentName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxBranchManager);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Глобальные настройки программы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxBranchManager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDepartmentName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHeAnaesthetist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSheAnaesthetist;
        private System.Windows.Forms.CheckBox checkBoxShowDbIndexes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDischargeEpicrisisHeader;
    }
}