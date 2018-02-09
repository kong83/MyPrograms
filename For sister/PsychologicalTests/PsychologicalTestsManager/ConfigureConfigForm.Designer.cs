namespace PsychologicalTestsManager
{
    partial class ConfigureConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureConfigForm));
            this.buttonConfigure = new System.Windows.Forms.Button();
            this.textBoxTestDisplayName = new System.Windows.Forms.TextBox();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.comboBoxTestName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonConfigure
            // 
            this.buttonConfigure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConfigure.Location = new System.Drawing.Point(221, 222);
            this.buttonConfigure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConfigure.Name = "buttonConfigure";
            this.buttonConfigure.Size = new System.Drawing.Size(171, 36);
            this.buttonConfigure.TabIndex = 0;
            this.buttonConfigure.Text = "Сконфигурировать";
            this.buttonConfigure.UseVisualStyleBackColor = true;
            this.buttonConfigure.Click += new System.EventHandler(this.buttonConfigure_Click);
            // 
            // textBoxTestDisplayName
            // 
            this.textBoxTestDisplayName.Location = new System.Drawing.Point(221, 162);
            this.textBoxTestDisplayName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTestDisplayName.Name = "textBoxTestDisplayName";
            this.textBoxTestDisplayName.Size = new System.Drawing.Size(443, 22);
            this.textBoxTestDisplayName.TabIndex = 2;
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(221, 192);
            this.textBoxSavePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(443, 22);
            this.textBoxSavePath.TabIndex = 3;
            // 
            // comboBoxTestName
            // 
            this.comboBoxTestName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTestName.FormattingEnabled = true;
            this.comboBoxTestName.Items.AddRange(new object[] {
            "Test_Phillipsa"});
            this.comboBoxTestName.Location = new System.Drawing.Point(221, 127);
            this.comboBoxTestName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxTestName.Name = "comboBoxTestName";
            this.comboBoxTestName.Size = new System.Drawing.Size(195, 24);
            this.comboBoxTestName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Имя теста";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Отображаемое название";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Путь сохранения результатов";
            // 
            // labelInfo
            // 
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(6, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(666, 114);
            this.labelInfo.TabIndex = 8;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfigureConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 269);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTestName);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.textBoxTestDisplayName);
            this.Controls.Add(this.buttonConfigure);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ConfigureConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Конфигурация конфига для программы PsychologicalTests";
            this.Shown += new System.EventHandler(this.ConfigureConfigForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConfigure;
        private System.Windows.Forms.TextBox textBoxTestDisplayName;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.ComboBox comboBoxTestName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelInfo;
    }
}