namespace Fonotec
{
    partial class AddEditFilmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditFilmForm));
            this.labelFilmName = new System.Windows.Forms.Label();
            this.textBoxDiskNumber = new System.Windows.Forms.TextBox();
            this.labelDiskNumber = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxFilmName = new System.Windows.Forms.TextBox();
            this.textBoxFilmInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxClearFields = new System.Windows.Forms.CheckBox();
            this.comboBoxFilterGenre = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelFilmName
            // 
            this.labelFilmName.AutoSize = true;
            this.labelFilmName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFilmName.Location = new System.Drawing.Point(12, 39);
            this.labelFilmName.Name = "labelFilmName";
            this.labelFilmName.Size = new System.Drawing.Size(115, 13);
            this.labelFilmName.TabIndex = 11;
            this.labelFilmName.Text = "Название фильма";
            // 
            // textBoxDiskNumber
            // 
            this.textBoxDiskNumber.Location = new System.Drawing.Point(130, 6);
            this.textBoxDiskNumber.Name = "textBoxDiskNumber";
            this.textBoxDiskNumber.Size = new System.Drawing.Size(51, 20);
            this.textBoxDiskNumber.TabIndex = 0;
            // 
            // labelDiskNumber
            // 
            this.labelDiskNumber.AutoSize = true;
            this.labelDiskNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDiskNumber.Location = new System.Drawing.Point(12, 9);
            this.labelDiskNumber.Name = "labelDiskNumber";
            this.labelDiskNumber.Size = new System.Drawing.Size(85, 13);
            this.labelDiskNumber.TabIndex = 9;
            this.labelDiskNumber.Text = "Номер диска";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackgroundImage = global::Fonotec.Properties.Resources.close;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(273, 268);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(40, 40);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BackgroundImage = global::Fonotec.Properties.Resources.ok;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(182, 268);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(40, 40);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxFilmName
            // 
            this.textBoxFilmName.Location = new System.Drawing.Point(130, 36);
            this.textBoxFilmName.Name = "textBoxFilmName";
            this.textBoxFilmName.Size = new System.Drawing.Size(327, 20);
            this.textBoxFilmName.TabIndex = 1;
            // 
            // textBoxFilmInfo
            // 
            this.textBoxFilmInfo.Location = new System.Drawing.Point(130, 92);
            this.textBoxFilmInfo.MaxLength = 2000000;
            this.textBoxFilmInfo.Multiline = true;
            this.textBoxFilmInfo.Name = "textBoxFilmInfo";
            this.textBoxFilmInfo.Size = new System.Drawing.Size(327, 165);
            this.textBoxFilmInfo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 26);
            this.label1.TabIndex = 13;
            this.label1.Text = "Дополнительная \r\nинформация";
            // 
            // checkBoxClearFields
            // 
            this.checkBoxClearFields.AutoSize = true;
            this.checkBoxClearFields.Location = new System.Drawing.Point(15, 284);
            this.checkBoxClearFields.Name = "checkBoxClearFields";
            this.checkBoxClearFields.Size = new System.Drawing.Size(98, 17);
            this.checkBoxClearFields.TabIndex = 15;
            this.checkBoxClearFields.Text = "Очищать поля";
            this.checkBoxClearFields.UseVisualStyleBackColor = true;
            this.checkBoxClearFields.CheckedChanged += new System.EventHandler(this.checkBoxClearFields_CheckedChanged);
            // 
            // comboBoxFilterGenre
            // 
            this.comboBoxFilterGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterGenre.FormattingEnabled = true;
            this.comboBoxFilterGenre.Items.AddRange(new object[] {
            "Боевик",
            "Комедия",
            "Мультик",
            "Фэнтэзи",
            "Мелодрама",
            "Фантастика",
            "Триллер",
            "Ужас",
            "Детектив",
            "Сказка",
            "Исторический",
            "Про жизнь",
            "Другое"});
            this.comboBoxFilterGenre.Location = new System.Drawing.Point(130, 62);
            this.comboBoxFilterGenre.Name = "comboBoxFilterGenre";
            this.comboBoxFilterGenre.Size = new System.Drawing.Size(263, 21);
            this.comboBoxFilterGenre.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Жанр фильма";
            // 
            // AddEditFilmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 316);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFilterGenre);
            this.Controls.Add(this.checkBoxClearFields);
            this.Controls.Add(this.textBoxFilmInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFilmName);
            this.Controls.Add(this.labelFilmName);
            this.Controls.Add(this.textBoxDiskNumber);
            this.Controls.Add(this.labelDiskNumber);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditFilmForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление/редактирование фильма";
            this.Load += new System.EventHandler(this.AddEditFilmForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.AddEditFilmForm_InputLanguageChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFilmName;
        private System.Windows.Forms.TextBox textBoxDiskNumber;
        private System.Windows.Forms.Label labelDiskNumber;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxFilmName;
        private System.Windows.Forms.TextBox textBoxFilmInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxClearFields;
        private System.Windows.Forms.ComboBox comboBoxFilterGenre;
        private System.Windows.Forms.Label label2;
    }
}