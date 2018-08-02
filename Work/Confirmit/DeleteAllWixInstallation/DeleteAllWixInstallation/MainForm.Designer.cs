namespace DeleteAllWixInstallation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDatabases = new System.Windows.Forms.TextBox();
            this.buttonDatabases = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonAll = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPerformance = new System.Windows.Forms.Button();
            this.textBoxPerformans = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonEventLogs = new System.Windows.Forms.Button();
            this.textBoxEventLogs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRegistry = new System.Windows.Forms.Button();
            this.textBoxRegistry = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonDirectoryes = new System.Windows.Forms.Button();
            this.textBoxDirectoryes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonBackup = new System.Windows.Forms.Button();
            this.textBoxBackup = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonCheckLog = new System.Windows.Forms.Button();
            this.textBoxCheckLog = new System.Windows.Forms.TextBox();
            this.labelCheckLog = new System.Windows.Forms.Label();
            this.buttonPermanentComponents = new System.Windows.Forms.Button();
            this.textBoxPermanentComponents = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonCheckInstall = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Базы данных";
            // 
            // textBoxDatabases
            // 
            this.textBoxDatabases.Location = new System.Drawing.Point(92, 102);
            this.textBoxDatabases.Multiline = true;
            this.textBoxDatabases.Name = "textBoxDatabases";
            this.textBoxDatabases.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDatabases.Size = new System.Drawing.Size(458, 37);
            this.textBoxDatabases.TabIndex = 1;
            this.textBoxDatabases.Text = "ConfirmitCATIV15";
            // 
            // buttonDatabases
            // 
            this.buttonDatabases.Location = new System.Drawing.Point(556, 107);
            this.buttonDatabases.Name = "buttonDatabases";
            this.buttonDatabases.Size = new System.Drawing.Size(75, 23);
            this.buttonDatabases.TabIndex = 2;
            this.buttonDatabases.Text = "Удалить";
            this.buttonDatabases.UseVisualStyleBackColor = true;
            this.buttonDatabases.Click += new System.EventHandler(this.buttonDatabases_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(602, 48);
            this.labelInfo.TabIndex = 3;
            this.labelInfo.Text = "Это утилита для удаления всего, что создаёт инсталяция Fusion. \r\nПеред её использ" +
                "ованием необходимо сначала деинсталлировать программу. \r\nУтилита удаляет то, что" +
                " не удалятеся при деинсталляции.";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAll
            // 
            this.buttonAll.Location = new System.Drawing.Point(499, 342);
            this.buttonAll.Name = "buttonAll";
            this.buttonAll.Size = new System.Drawing.Size(135, 27);
            this.buttonAll.TabIndex = 4;
            this.buttonAll.Text = "Удалить всё";
            this.buttonAll.UseVisualStyleBackColor = true;
            this.buttonAll.Click += new System.EventHandler(this.buttonAll_Click);
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(92, 77);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(181, 20);
            this.textBoxServer.TabIndex = 5;
            this.textBoxServer.Text = "CO-OSL-DEVB24";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "SQL сервер";
            // 
            // buttonPerformance
            // 
            this.buttonPerformance.Location = new System.Drawing.Point(559, 528);
            this.buttonPerformance.Name = "buttonPerformance";
            this.buttonPerformance.Size = new System.Drawing.Size(75, 23);
            this.buttonPerformance.TabIndex = 9;
            this.buttonPerformance.Text = "Удалить";
            this.buttonPerformance.UseVisualStyleBackColor = true;
            this.buttonPerformance.Click += new System.EventHandler(this.buttonPerformance_Click);
            // 
            // textBoxPerformans
            // 
            this.textBoxPerformans.Location = new System.Drawing.Point(95, 522);
            this.textBoxPerformans.Multiline = true;
            this.textBoxPerformans.Name = "textBoxPerformans";
            this.textBoxPerformans.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPerformans.Size = new System.Drawing.Size(458, 37);
            this.textBoxPerformans.TabIndex = 8;
            this.textBoxPerformans.Text = "CATI Confirmit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 525);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Performance\r\ncounter";
            // 
            // buttonEventLogs
            // 
            this.buttonEventLogs.Location = new System.Drawing.Point(556, 163);
            this.buttonEventLogs.Name = "buttonEventLogs";
            this.buttonEventLogs.Size = new System.Drawing.Size(75, 23);
            this.buttonEventLogs.TabIndex = 12;
            this.buttonEventLogs.Text = "Удалить";
            this.buttonEventLogs.UseVisualStyleBackColor = true;
            this.buttonEventLogs.Click += new System.EventHandler(this.buttonEventLogs_Click);
            // 
            // textBoxEventLogs
            // 
            this.textBoxEventLogs.Location = new System.Drawing.Point(92, 156);
            this.textBoxEventLogs.Multiline = true;
            this.textBoxEventLogs.Name = "textBoxEventLogs";
            this.textBoxEventLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEventLogs.Size = new System.Drawing.Size(458, 37);
            this.textBoxEventLogs.TabIndex = 11;
            this.textBoxEventLogs.Text = "Confirmlog";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Event logs";
            // 
            // buttonRegistry
            // 
            this.buttonRegistry.Location = new System.Drawing.Point(559, 405);
            this.buttonRegistry.Name = "buttonRegistry";
            this.buttonRegistry.Size = new System.Drawing.Size(75, 23);
            this.buttonRegistry.TabIndex = 15;
            this.buttonRegistry.Text = "Удалить";
            this.buttonRegistry.UseVisualStyleBackColor = true;
            this.buttonRegistry.Click += new System.EventHandler(this.buttonRegistry_Click);
            // 
            // textBoxRegistry
            // 
            this.textBoxRegistry.Location = new System.Drawing.Point(95, 407);
            this.textBoxRegistry.Name = "textBoxRegistry";
            this.textBoxRegistry.Size = new System.Drawing.Size(458, 20);
            this.textBoxRegistry.TabIndex = 14;
            this.textBoxRegistry.Text = "Software\\Confirmit\\CATI";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Registry key";
            // 
            // buttonDirectoryes
            // 
            this.buttonDirectoryes.Location = new System.Drawing.Point(559, 464);
            this.buttonDirectoryes.Name = "buttonDirectoryes";
            this.buttonDirectoryes.Size = new System.Drawing.Size(75, 23);
            this.buttonDirectoryes.TabIndex = 18;
            this.buttonDirectoryes.Text = "Удалить";
            this.buttonDirectoryes.UseVisualStyleBackColor = true;
            this.buttonDirectoryes.Click += new System.EventHandler(this.buttonDirectoryes_Click);
            // 
            // textBoxDirectoryes
            // 
            this.textBoxDirectoryes.Location = new System.Drawing.Point(95, 447);
            this.textBoxDirectoryes.Multiline = true;
            this.textBoxDirectoryes.Name = "textBoxDirectoryes";
            this.textBoxDirectoryes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDirectoryes.Size = new System.Drawing.Size(458, 59);
            this.textBoxDirectoryes.TabIndex = 17;
            this.textBoxDirectoryes.Text = "C:\\inetpub\\wwwroot\\MonitoringWS\r\nC:\\inetpub\\wwwroot\\CP\r\nC:\\inetpub\\wwwroot\\bvfmws" +
                "\r\nC:\\inetpub\\wwwroot\\CATIConsoleWS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 470);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Directoryes";
            // 
            // buttonBackup
            // 
            this.buttonBackup.Location = new System.Drawing.Point(556, 211);
            this.buttonBackup.Name = "buttonBackup";
            this.buttonBackup.Size = new System.Drawing.Size(75, 23);
            this.buttonBackup.TabIndex = 21;
            this.buttonBackup.Text = "Удалить";
            this.buttonBackup.UseVisualStyleBackColor = true;
            this.buttonBackup.Click += new System.EventHandler(this.buttonBackup_Click);
            // 
            // textBoxBackup
            // 
            this.textBoxBackup.Location = new System.Drawing.Point(92, 213);
            this.textBoxBackup.Name = "textBoxBackup";
            this.textBoxBackup.Size = new System.Drawing.Size(458, 20);
            this.textBoxBackup.TabIndex = 20;
            this.textBoxBackup.Text = "C:\\backupConfirmitCATI.bak";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Backup";
            // 
            // buttonCheckLog
            // 
            this.buttonCheckLog.Location = new System.Drawing.Point(559, 609);
            this.buttonCheckLog.Name = "buttonCheckLog";
            this.buttonCheckLog.Size = new System.Drawing.Size(86, 23);
            this.buttonCheckLog.TabIndex = 27;
            this.buttonCheckLog.Text = "Поправить";
            this.buttonCheckLog.UseVisualStyleBackColor = true;
            this.buttonCheckLog.Click += new System.EventHandler(this.buttonCheckLog_Click);
            // 
            // textBoxCheckLog
            // 
            this.textBoxCheckLog.Location = new System.Drawing.Point(95, 611);
            this.textBoxCheckLog.Name = "textBoxCheckLog";
            this.textBoxCheckLog.Size = new System.Drawing.Size(458, 20);
            this.textBoxCheckLog.TabIndex = 28;
            this.textBoxCheckLog.Text = "C:\\MyLogFile.txt";
            // 
            // labelCheckLog
            // 
            this.labelCheckLog.AutoSize = true;
            this.labelCheckLog.Location = new System.Drawing.Point(12, 614);
            this.labelCheckLog.Name = "labelCheckLog";
            this.labelCheckLog.Size = new System.Drawing.Size(82, 13);
            this.labelCheckLog.TabIndex = 29;
            this.labelCheckLog.Text = "Поправить лог";
            // 
            // buttonPermanentComponents
            // 
            this.buttonPermanentComponents.Location = new System.Drawing.Point(556, 274);
            this.buttonPermanentComponents.Name = "buttonPermanentComponents";
            this.buttonPermanentComponents.Size = new System.Drawing.Size(75, 23);
            this.buttonPermanentComponents.TabIndex = 32;
            this.buttonPermanentComponents.Text = "Удалить";
            this.buttonPermanentComponents.UseVisualStyleBackColor = true;
            this.buttonPermanentComponents.Click += new System.EventHandler(this.buttonPermanentComponents_Click);
            // 
            // textBoxPermanentComponents
            // 
            this.textBoxPermanentComponents.Location = new System.Drawing.Point(92, 257);
            this.textBoxPermanentComponents.Multiline = true;
            this.textBoxPermanentComponents.Name = "textBoxPermanentComponents";
            this.textBoxPermanentComponents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPermanentComponents.Size = new System.Drawing.Size(458, 59);
            this.textBoxPermanentComponents.TabIndex = 31;
            this.textBoxPermanentComponents.Text = "626F6620-00D2-4d79-A58E-859C162AC762";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 274);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 26);
            this.label10.TabIndex = 30;
            this.label10.Text = "Permanent \r\ncomponents";
            // 
            // buttonCheckInstall
            // 
            this.buttonCheckInstall.Location = new System.Drawing.Point(15, 332);
            this.buttonCheckInstall.Name = "buttonCheckInstall";
            this.buttonCheckInstall.Size = new System.Drawing.Size(135, 47);
            this.buttonCheckInstall.TabIndex = 33;
            this.buttonCheckInstall.Text = "Проверить\r\nправильность\r\nустановки V14";
            this.buttonCheckInstall.UseVisualStyleBackColor = true;
            this.buttonCheckInstall.Click += new System.EventHandler(this.buttonCheckInstall_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(546, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "Версия 2.0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 47);
            this.button1.TabIndex = 35;
            this.button1.Text = "Проверить\r\nправильность\r\nустановки V15";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(330, 332);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 47);
            this.button2.TabIndex = 36;
            this.button2.Text = "Проверить\r\nправильность\r\nустановки V16.5";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 652);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonCheckInstall);
            this.Controls.Add(this.buttonPermanentComponents);
            this.Controls.Add(this.textBoxPermanentComponents);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelCheckLog);
            this.Controls.Add(this.textBoxCheckLog);
            this.Controls.Add(this.buttonCheckLog);
            this.Controls.Add(this.buttonBackup);
            this.Controls.Add(this.textBoxBackup);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonDirectoryes);
            this.Controls.Add(this.textBoxDirectoryes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonRegistry);
            this.Controls.Add(this.textBoxRegistry);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonEventLogs);
            this.Controls.Add(this.textBoxEventLogs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonPerformance);
            this.Controls.Add(this.textBoxPerformans);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.buttonAll);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonDatabases);
            this.Controls.Add(this.textBoxDatabases);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Удаление лишнего";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDatabases;
        private System.Windows.Forms.Button buttonDatabases;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonAll;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPerformance;
        private System.Windows.Forms.TextBox textBoxPerformans;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonEventLogs;
        private System.Windows.Forms.TextBox textBoxEventLogs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRegistry;
        private System.Windows.Forms.TextBox textBoxRegistry;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonDirectoryes;
        private System.Windows.Forms.TextBox textBoxDirectoryes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonBackup;
        private System.Windows.Forms.TextBox textBoxBackup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonCheckLog;
        private System.Windows.Forms.TextBox textBoxCheckLog;
        private System.Windows.Forms.Label labelCheckLog;
        private System.Windows.Forms.Button buttonPermanentComponents;
        private System.Windows.Forms.TextBox textBoxPermanentComponents;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonCheckInstall;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

