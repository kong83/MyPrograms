namespace ServiceWorker
{
    partial class MagicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicForm));
            this.buttonVerify = new System.Windows.Forms.Button();
            this.richTextBoxServers = new System.Windows.Forms.RichTextBox();
            this.labelServersList = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxServices = new System.Windows.Forms.RichTextBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonStartAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxServicesToStart = new System.Windows.Forms.ListBox();
            this.buttonStartSelected = new System.Windows.Forms.Button();
            this.labelProcessInfo = new System.Windows.Forms.Label();
            this.checkBoxSavePassword = new System.Windows.Forms.CheckBox();
            this.buttonSetDefault = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(302, 260);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 23);
            this.buttonVerify.TabIndex = 0;
            this.buttonVerify.Text = "Verify";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // richTextBoxServers
            // 
            this.richTextBoxServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxServers.Location = new System.Drawing.Point(12, 25);
            this.richTextBoxServers.Name = "richTextBoxServers";
            this.richTextBoxServers.Size = new System.Drawing.Size(197, 355);
            this.richTextBoxServers.TabIndex = 1;
            this.richTextBoxServers.Text = resources.GetString("richTextBoxServers.Text");
            // 
            // labelServersList
            // 
            this.labelServersList.AutoSize = true;
            this.labelServersList.Location = new System.Drawing.Point(12, 9);
            this.labelServersList.Name = "labelServersList";
            this.labelServersList.Size = new System.Drawing.Size(58, 13);
            this.labelServersList.TabIndex = 2;
            this.labelServersList.Text = "Servers list";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Services to check (start symbols in display name)";
            // 
            // richTextBoxServices
            // 
            this.richTextBoxServices.Location = new System.Drawing.Point(233, 25);
            this.richTextBoxServices.Name = "richTextBoxServices";
            this.richTextBoxServices.Size = new System.Drawing.Size(234, 110);
            this.richTextBoxServices.TabIndex = 3;
            this.richTextBoxServices.Text = "MSSQL\nSQL\nMsDtsServer\nMSOLAP\nConfirmit\nOctopus";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(289, 160);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(178, 20);
            this.textBoxLogin.TabIndex = 5;
            this.textBoxLogin.Text = "$grigoryk";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(289, 189);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(178, 20);
            this.textBoxPassword.TabIndex = 6;
            this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // buttonStartAll
            // 
            this.buttonStartAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartAll.Location = new System.Drawing.Point(864, 385);
            this.buttonStartAll.Name = "buttonStartAll";
            this.buttonStartAll.Size = new System.Drawing.Size(75, 23);
            this.buttonStartAll.TabIndex = 10;
            this.buttonStartAll.Text = "Start all";
            this.buttonStartAll.UseVisualStyleBackColor = true;
            this.buttonStartAll.Click += new System.EventHandler(this.buttonStartAll_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(490, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Not started services";
            // 
            // listBoxServicesToStart
            // 
            this.listBoxServicesToStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxServicesToStart.FormattingEnabled = true;
            this.listBoxServicesToStart.Location = new System.Drawing.Point(493, 25);
            this.listBoxServicesToStart.Name = "listBoxServicesToStart";
            this.listBoxServicesToStart.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxServicesToStart.Size = new System.Drawing.Size(446, 355);
            this.listBoxServicesToStart.TabIndex = 12;
            // 
            // buttonStartSelected
            // 
            this.buttonStartSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartSelected.Location = new System.Drawing.Point(766, 385);
            this.buttonStartSelected.Name = "buttonStartSelected";
            this.buttonStartSelected.Size = new System.Drawing.Size(92, 23);
            this.buttonStartSelected.TabIndex = 13;
            this.buttonStartSelected.Text = "Start Selected";
            this.buttonStartSelected.UseVisualStyleBackColor = true;
            this.buttonStartSelected.Click += new System.EventHandler(this.buttonStartSelected_Click);
            // 
            // labelProcessInfo
            // 
            this.labelProcessInfo.Location = new System.Drawing.Point(230, 287);
            this.labelProcessInfo.Name = "labelProcessInfo";
            this.labelProcessInfo.Size = new System.Drawing.Size(237, 77);
            this.labelProcessInfo.TabIndex = 14;
            this.labelProcessInfo.Text = "Current server: ";
            this.labelProcessInfo.Visible = false;
            // 
            // checkBoxSavePassword
            // 
            this.checkBoxSavePassword.AutoSize = true;
            this.checkBoxSavePassword.Location = new System.Drawing.Point(289, 215);
            this.checkBoxSavePassword.Name = "checkBoxSavePassword";
            this.checkBoxSavePassword.Size = new System.Drawing.Size(99, 17);
            this.checkBoxSavePassword.TabIndex = 15;
            this.checkBoxSavePassword.Text = "Save password";
            this.checkBoxSavePassword.UseVisualStyleBackColor = true;
            // 
            // buttonSetDefault
            // 
            this.buttonSetDefault.Location = new System.Drawing.Point(12, 385);
            this.buttonSetDefault.Name = "buttonSetDefault";
            this.buttonSetDefault.Size = new System.Drawing.Size(75, 23);
            this.buttonSetDefault.TabIndex = 16;
            this.buttonSetDefault.Text = "Set Default";
            this.buttonSetDefault.UseVisualStyleBackColor = true;
            this.buttonSetDefault.Click += new System.EventHandler(this.buttonSetDefault_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Save as Default";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MagicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSetDefault);
            this.Controls.Add(this.checkBoxSavePassword);
            this.Controls.Add(this.labelProcessInfo);
            this.Controls.Add(this.buttonStartSelected);
            this.Controls.Add(this.listBoxServicesToStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonStartAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBoxServices);
            this.Controls.Add(this.labelServersList);
            this.Controls.Add(this.richTextBoxServers);
            this.Controls.Add(this.buttonVerify);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MagicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check and run needed  services on specifyed servers";
            this.Shown += new System.EventHandler(this.MagicForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.RichTextBox richTextBoxServers;
        private System.Windows.Forms.Label labelServersList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxServices;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonStartAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxServicesToStart;
        private System.Windows.Forms.Button buttonStartSelected;
        private System.Windows.Forms.Label labelProcessInfo;
        private System.Windows.Forms.CheckBox checkBoxSavePassword;
        private System.Windows.Forms.Button buttonSetDefault;
        private System.Windows.Forms.Button button1;
    }
}