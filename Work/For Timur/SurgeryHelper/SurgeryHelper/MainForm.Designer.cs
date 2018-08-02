namespace SurgeryHelper
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFilePatientList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileGlobalSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpRegistration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFilePatientList,
            this.menuItemFileImport,
            this.menuItemFileGlobalSettings,
            this.menuItemFileChangePassword,
            this.menuItemFileExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(48, 20);
            this.menuItemFile.Text = "&Файл";
            // 
            // menuItemFilePatientList
            // 
            this.menuItemFilePatientList.Name = "menuItemFilePatientList";
            this.menuItemFilePatientList.Size = new System.Drawing.Size(241, 22);
            this.menuItemFilePatientList.Text = "&Отобразить список пациентов";
            this.menuItemFilePatientList.Click += new System.EventHandler(this.menuItemFilePatientList_Click);
            // 
            // menuItemFileImport
            // 
            this.menuItemFileImport.Name = "menuItemFileImport";
            this.menuItemFileImport.Size = new System.Drawing.Size(241, 22);
            this.menuItemFileImport.Text = "&Импорт данных...";
            this.menuItemFileImport.Click += new System.EventHandler(this.menuItemFileImport_Click);
            // 
            // menuItemFileGlobalSettings
            // 
            this.menuItemFileGlobalSettings.Name = "menuItemFileGlobalSettings";
            this.menuItemFileGlobalSettings.Size = new System.Drawing.Size(241, 22);
            this.menuItemFileGlobalSettings.Text = "&Глобальные настройки...";
            this.menuItemFileGlobalSettings.Click += new System.EventHandler(this.menuItemFileGlobalSettings_Click);
            // 
            // menuItemFileChangePassword
            // 
            this.menuItemFileChangePassword.Name = "menuItemFileChangePassword";
            this.menuItemFileChangePassword.Size = new System.Drawing.Size(241, 22);
            this.menuItemFileChangePassword.Text = "Сменить &пароль...";
            this.menuItemFileChangePassword.Click += new System.EventHandler(this.menuItemFileChangePassword_Click);
            // 
            // menuItemFileExit
            // 
            this.menuItemFileExit.Name = "menuItemFileExit";
            this.menuItemFileExit.Size = new System.Drawing.Size(241, 22);
            this.menuItemFileExit.Text = "&Выход";
            this.menuItemFileExit.Click += new System.EventHandler(this.menuItemFileExit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelpRegistration,
            this.menuItemHelpAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(68, 20);
            this.menuItemHelp.Text = "&Помощь";
            // 
            // menuItemHelpRegistration
            // 
            this.menuItemHelpRegistration.Name = "menuItemHelpRegistration";
            this.menuItemHelpRegistration.Size = new System.Drawing.Size(158, 22);
            this.menuItemHelpRegistration.Text = "&Регистрация...";
            this.menuItemHelpRegistration.Click += new System.EventHandler(this.menuItemHelpRegistration_Click);
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Name = "menuItemHelpAbout";
            this.menuItemHelpAbout.Size = new System.Drawing.Size(158, 22);
            this.menuItemHelpAbout.Text = "&О программе...";
            this.menuItemHelpAbout.Click += new System.EventHandler(this.menuItemHelpAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 486);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Помощник хирурга :)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemFilePatientList;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileChangePassword;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileImport;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileGlobalSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpRegistration;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpAbout;
    }
}

