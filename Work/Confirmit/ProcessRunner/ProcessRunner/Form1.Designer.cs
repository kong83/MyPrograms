namespace ProcessRunner
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownCopiesCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWorkTime = new System.Windows.Forms.NumericUpDown();
            this.labelWorkingSetMemoryInfo = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelRemainingTimeInfo = new System.Windows.Forms.Label();
            this.labelCommitSizeMemoryInfo = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textBoxFilePath = new System.Windows.Forms.ComboBox();
            this.buttonCopyWorkingSet = new System.Windows.Forms.Button();
            this.buttonCopyCommitSize = new System.Windows.Forms.Button();
            this.checkBoxShowWindows = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCopiesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorkTime)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select program to run";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(15, 70);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(162, 30);
            this.buttonRun.TabIndex = 9;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.ButtonRunClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Set count of progam copies to run";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Set time of running (in minutes)";
            // 
            // numericUpDownCopiesCount
            // 
            this.numericUpDownCopiesCount.Location = new System.Drawing.Point(235, 25);
            this.numericUpDownCopiesCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownCopiesCount.Name = "numericUpDownCopiesCount";
            this.numericUpDownCopiesCount.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownCopiesCount.TabIndex = 5;
            this.numericUpDownCopiesCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownWorkTime
            // 
            this.numericUpDownWorkTime.Location = new System.Drawing.Point(414, 25);
            this.numericUpDownWorkTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownWorkTime.Name = "numericUpDownWorkTime";
            this.numericUpDownWorkTime.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownWorkTime.TabIndex = 7;
            this.numericUpDownWorkTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // labelWorkingSetMemoryInfo
            // 
            this.labelWorkingSetMemoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelWorkingSetMemoryInfo.Location = new System.Drawing.Point(15, 118);
            this.labelWorkingSetMemoryInfo.Name = "labelWorkingSetMemoryInfo";
            this.labelWorkingSetMemoryInfo.Size = new System.Drawing.Size(311, 21);
            this.labelWorkingSetMemoryInfo.TabIndex = 10;
            this.labelWorkingSetMemoryInfo.Text = "Sum of used \"Working Set Memory\"";
            this.labelWorkingSetMemoryInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(400, 70);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(162, 30);
            this.buttonStop.TabIndex = 11;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
            // 
            // labelRemainingTimeInfo
            // 
            this.labelRemainingTimeInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelRemainingTimeInfo.Location = new System.Drawing.Point(400, 118);
            this.labelRemainingTimeInfo.Name = "labelRemainingTimeInfo";
            this.labelRemainingTimeInfo.Size = new System.Drawing.Size(162, 21);
            this.labelRemainingTimeInfo.TabIndex = 12;
            this.labelRemainingTimeInfo.Text = "Remaining Time:";
            this.labelRemainingTimeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelRemainingTimeInfo.Visible = false;
            // 
            // labelCommitSizeMemoryInfo
            // 
            this.labelCommitSizeMemoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCommitSizeMemoryInfo.Location = new System.Drawing.Point(15, 148);
            this.labelCommitSizeMemoryInfo.Name = "labelCommitSizeMemoryInfo";
            this.labelCommitSizeMemoryInfo.Size = new System.Drawing.Size(311, 21);
            this.labelCommitSizeMemoryInfo.TabIndex = 17;
            this.labelCommitSizeMemoryInfo.Text = "Sum of used \"Commit Size\"";
            this.labelCommitSizeMemoryInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(456, 160);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(106, 30);
            this.buttonExit.TabIndex = 13;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExitClick);
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.FormattingEnabled = true;
            this.textBoxFilePath.Items.AddRange(new object[] {
            "ThreadConnector.exe",
            "TimerConnector.exe"});
            this.textBoxFilePath.Location = new System.Drawing.Point(15, 24);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(197, 21);
            this.textBoxFilePath.TabIndex = 1;
            this.textBoxFilePath.Text = "ThreadConnector.exe";
            // 
            // buttonCopyWorkingSet
            // 
            this.buttonCopyWorkingSet.Location = new System.Drawing.Point(332, 118);
            this.buttonCopyWorkingSet.Name = "buttonCopyWorkingSet";
            this.buttonCopyWorkingSet.Size = new System.Drawing.Size(40, 21);
            this.buttonCopyWorkingSet.TabIndex = 18;
            this.buttonCopyWorkingSet.Text = "Copy";
            this.buttonCopyWorkingSet.UseVisualStyleBackColor = true;
            this.buttonCopyWorkingSet.Click += new System.EventHandler(this.ButtonCopyWorkingSetClick);
            // 
            // buttonCopyCommitSize
            // 
            this.buttonCopyCommitSize.Location = new System.Drawing.Point(332, 148);
            this.buttonCopyCommitSize.Name = "buttonCopyCommitSize";
            this.buttonCopyCommitSize.Size = new System.Drawing.Size(40, 21);
            this.buttonCopyCommitSize.TabIndex = 19;
            this.buttonCopyCommitSize.Text = "Copy";
            this.buttonCopyCommitSize.UseVisualStyleBackColor = true;
            this.buttonCopyCommitSize.Click += new System.EventHandler(this.ButtonCopyCommitSizeClick);
            // 
            // checkBoxShowWindows
            // 
            this.checkBoxShowWindows.AutoSize = true;
            this.checkBoxShowWindows.Location = new System.Drawing.Point(15, 47);
            this.checkBoxShowWindows.Name = "checkBoxShowWindows";
            this.checkBoxShowWindows.Size = new System.Drawing.Size(97, 17);
            this.checkBoxShowWindows.TabIndex = 20;
            this.checkBoxShowWindows.Text = "Show windows";
            this.checkBoxShowWindows.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 209);
            this.Controls.Add(this.checkBoxShowWindows);
            this.Controls.Add(this.buttonCopyCommitSize);
            this.Controls.Add(this.buttonCopyWorkingSet);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelCommitSizeMemoryInfo);
            this.Controls.Add(this.labelRemainingTimeInfo);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.labelWorkingSetMemoryInfo);
            this.Controls.Add(this.numericUpDownWorkTime);
            this.Controls.Add(this.numericUpDownCopiesCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Runner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCopiesCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWorkTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownCopiesCount;
        private System.Windows.Forms.NumericUpDown numericUpDownWorkTime;
        private System.Windows.Forms.Label labelWorkingSetMemoryInfo;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelRemainingTimeInfo;
        private System.Windows.Forms.Label labelCommitSizeMemoryInfo;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ComboBox textBoxFilePath;
        private System.Windows.Forms.Button buttonCopyWorkingSet;
        private System.Windows.Forms.Button buttonCopyCommitSize;
        private System.Windows.Forms.CheckBox checkBoxShowWindows;
    }
}

