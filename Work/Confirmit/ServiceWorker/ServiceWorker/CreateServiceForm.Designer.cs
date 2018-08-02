namespace ServiceWorker
{
    partial class CreateServiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateServiceForm));
            this.Create = new System.Windows.Forms.Button();
            this.textBoxServiceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxStartAfterCreate = new System.Windows.Forms.CheckBox();
            this.textBoxDisplayName = new System.Windows.Forms.TextBox();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.textBoxCommandLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Create
            // 
            this.Create.Location = new System.Drawing.Point(35, 145);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(75, 23);
            this.Create.TabIndex = 10;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // textBoxServiceName
            // 
            this.textBoxServiceName.Location = new System.Drawing.Point(105, 12);
            this.textBoxServiceName.Name = "textBoxServiceName";
            this.textBoxServiceName.Size = new System.Drawing.Size(175, 20);
            this.textBoxServiceName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Service name";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(183, 145);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxStartAfterCreate
            // 
            this.checkBoxStartAfterCreate.AutoSize = true;
            this.checkBoxStartAfterCreate.Checked = true;
            this.checkBoxStartAfterCreate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStartAfterCreate.Location = new System.Drawing.Point(105, 118);
            this.checkBoxStartAfterCreate.Name = "checkBoxStartAfterCreate";
            this.checkBoxStartAfterCreate.Size = new System.Drawing.Size(122, 17);
            this.checkBoxStartAfterCreate.TabIndex = 1;
            this.checkBoxStartAfterCreate.Text = "Start after a creation";
            this.checkBoxStartAfterCreate.UseVisualStyleBackColor = true;
            // 
            // textBoxDisplayName
            // 
            this.textBoxDisplayName.Location = new System.Drawing.Point(105, 38);
            this.textBoxDisplayName.Name = "textBoxDisplayName";
            this.textBoxDisplayName.Size = new System.Drawing.Size(175, 20);
            this.textBoxDisplayName.TabIndex = 12;
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(105, 64);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(175, 20);
            this.textBoxFilePath.TabIndex = 13;
            // 
            // textBoxCommandLine
            // 
            this.textBoxCommandLine.Location = new System.Drawing.Point(105, 90);
            this.textBoxCommandLine.Name = "textBoxCommandLine";
            this.textBoxCommandLine.Size = new System.Drawing.Size(175, 20);
            this.textBoxCommandLine.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Display name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "File path";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Command line";
            // 
            // CreateServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 181);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCommandLine);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.textBoxDisplayName);
            this.Controls.Add(this.checkBoxStartAfterCreate);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxServiceName);
            this.Controls.Add(this.Create);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateServiceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.TextBox textBoxServiceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxStartAfterCreate;
        private System.Windows.Forms.TextBox textBoxDisplayName;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.TextBox textBoxCommandLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}