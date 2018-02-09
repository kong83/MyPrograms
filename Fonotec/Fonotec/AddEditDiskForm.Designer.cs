namespace Fonotec
{
    partial class AddEditDiskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditDiskForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDiskNumber = new System.Windows.Forms.Label();
            this.textBoxDiskNumber = new System.Windows.Forms.TextBox();
            this.labelDiskInfo = new System.Windows.Forms.Label();
            this.comboBoxDiskInfo = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxIsChangeFilms = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackgroundImage = global::Fonotec.Properties.Resources.close;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(171, 105);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(40, 40);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDiskNumber
            // 
            this.labelDiskNumber.AutoSize = true;
            this.labelDiskNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDiskNumber.Location = new System.Drawing.Point(12, 22);
            this.labelDiskNumber.Name = "labelDiskNumber";
            this.labelDiskNumber.Size = new System.Drawing.Size(85, 13);
            this.labelDiskNumber.TabIndex = 2;
            this.labelDiskNumber.Text = "Номер диска";
            // 
            // textBoxDiskNumber
            // 
            this.textBoxDiskNumber.Location = new System.Drawing.Point(109, 19);
            this.textBoxDiskNumber.Name = "textBoxDiskNumber";
            this.textBoxDiskNumber.Size = new System.Drawing.Size(51, 20);
            this.textBoxDiskNumber.TabIndex = 0;
            this.textBoxDiskNumber.TextChanged += new System.EventHandler(this.textBoxDiskNumber_TextChanged);
            // 
            // labelDiskInfo
            // 
            this.labelDiskInfo.AutoSize = true;
            this.labelDiskInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDiskInfo.Location = new System.Drawing.Point(12, 71);
            this.labelDiskInfo.Name = "labelDiskInfo";
            this.labelDiskInfo.Size = new System.Drawing.Size(68, 13);
            this.labelDiskInfo.TabIndex = 5;
            this.labelDiskInfo.Text = "Тип диска";
            // 
            // comboBoxDiskInfo
            // 
            this.comboBoxDiskInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiskInfo.FormattingEnabled = true;
            this.comboBoxDiskInfo.Items.AddRange(new object[] {
            "Купленный",
            "Диск DVD-R",
            "Диск DVD-RW"});
            this.comboBoxDiskInfo.Location = new System.Drawing.Point(109, 68);
            this.comboBoxDiskInfo.Name = "comboBoxDiskInfo";
            this.comboBoxDiskInfo.Size = new System.Drawing.Size(157, 21);
            this.comboBoxDiskInfo.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.BackgroundImage = global::Fonotec.Properties.Resources.ok;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(96, 105);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(40, 40);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxIsChangeFilms
            // 
            this.checkBoxIsChangeFilms.AutoSize = true;
            this.checkBoxIsChangeFilms.Checked = true;
            this.checkBoxIsChangeFilms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsChangeFilms.Location = new System.Drawing.Point(109, 45);
            this.checkBoxIsChangeFilms.Name = "checkBoxIsChangeFilms";
            this.checkBoxIsChangeFilms.Size = new System.Drawing.Size(134, 17);
            this.checkBoxIsChangeFilms.TabIndex = 12;
            this.checkBoxIsChangeFilms.Text = "Изменить у фильмов";
            this.checkBoxIsChangeFilms.UseVisualStyleBackColor = true;
            this.checkBoxIsChangeFilms.Visible = false;
            // 
            // AddEditDiskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 157);
            this.Controls.Add(this.checkBoxIsChangeFilms);
            this.Controls.Add(this.comboBoxDiskInfo);
            this.Controls.Add(this.labelDiskInfo);
            this.Controls.Add(this.textBoxDiskNumber);
            this.Controls.Add(this.labelDiskNumber);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditDiskForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление/редактирование диска";
            this.Load += new System.EventHandler(this.AddEditDiskForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.AddEditDiskForm_InputLanguageChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDiskNumber;
        private System.Windows.Forms.TextBox textBoxDiskNumber;
        private System.Windows.Forms.Label labelDiskInfo;
        private System.Windows.Forms.ComboBox comboBoxDiskInfo;
        private System.Windows.Forms.CheckBox checkBoxIsChangeFilms;
    }
}