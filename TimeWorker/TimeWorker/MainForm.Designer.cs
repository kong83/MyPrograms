namespace TimeWorker
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
            this.labelStart1 = new System.Windows.Forms.Label();
            this.buttonGetTime = new System.Windows.Forms.Button();
            this.labelStart2 = new System.Windows.Forms.Label();
            this.dateTimePickerArriveTime1 = new System.Windows.Forms.DateTimePicker();
            this.labelArrive1 = new System.Windows.Forms.Label();
            this.labelArrive2 = new System.Windows.Forms.Label();
            this.labelReturn1 = new System.Windows.Forms.Label();
            this.labelReturn2 = new System.Windows.Forms.Label();
            this.dateTimePickerArriveTime2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerReturnTime1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerReturnTime2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartTime1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartTime2 = new System.Windows.Forms.DateTimePicker();
            this.labelAnswer = new System.Windows.Forms.Label();
            this.textBoxNick1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxNick2 = new System.Windows.Forms.TextBox();
            this.dateTimePickerFirst = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerSecond = new System.Windows.Forms.DateTimePicker();
            this.buttonEvaluate = new System.Windows.Forms.Button();
            this.dateTimePickerResult = new System.Windows.Forms.DateTimePicker();
            this.comboBoxSign = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelWalk1 = new System.Windows.Forms.Label();
            this.labelWalk2 = new System.Windows.Forms.Label();
            this.buttonEvaluate2 = new System.Windows.Forms.Button();
            this.numericUpDownPercent = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSign2 = new System.Windows.Forms.ComboBox();
            this.dateTimePickerWalkTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerResultWalkTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // labelStart1
            // 
            this.labelStart1.AutoSize = true;
            this.labelStart1.Location = new System.Drawing.Point(625, 17);
            this.labelStart1.Name = "labelStart1";
            this.labelStart1.Size = new System.Drawing.Size(80, 26);
            this.labelStart1.TabIndex = 0;
            this.labelStart1.Text = "Время выхода\r\nигрока";
            this.labelStart1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGetTime
            // 
            this.buttonGetTime.Location = new System.Drawing.Point(480, 68);
            this.buttonGetTime.Name = "buttonGetTime";
            this.buttonGetTime.Size = new System.Drawing.Size(88, 23);
            this.buttonGetTime.TabIndex = 12;
            this.buttonGetTime.Text = "Кто раньше?";
            this.buttonGetTime.UseVisualStyleBackColor = true;
            this.buttonGetTime.Click += new System.EventHandler(this.buttonGetTime_Click);
            // 
            // labelStart2
            // 
            this.labelStart2.AutoSize = true;
            this.labelStart2.Location = new System.Drawing.Point(625, 89);
            this.labelStart2.Name = "labelStart2";
            this.labelStart2.Size = new System.Drawing.Size(80, 26);
            this.labelStart2.TabIndex = 3;
            this.labelStart2.Text = "Время выхода\r\nигрока";
            this.labelStart2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerArriveTime1
            // 
            this.dateTimePickerArriveTime1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerArriveTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerArriveTime1.Location = new System.Drawing.Point(145, 46);
            this.dateTimePickerArriveTime1.Name = "dateTimePickerArriveTime1";
            this.dateTimePickerArriveTime1.ShowUpDown = true;
            this.dateTimePickerArriveTime1.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerArriveTime1.TabIndex = 2;
            this.dateTimePickerArriveTime1.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // labelArrive1
            // 
            this.labelArrive1.AutoSize = true;
            this.labelArrive1.Location = new System.Drawing.Point(160, 19);
            this.labelArrive1.Name = "labelArrive1";
            this.labelArrive1.Size = new System.Drawing.Size(92, 26);
            this.labelArrive1.TabIndex = 6;
            this.labelArrive1.Text = "Время прибытия\r\nигрока";
            this.labelArrive1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelArrive2
            // 
            this.labelArrive2.AutoSize = true;
            this.labelArrive2.Location = new System.Drawing.Point(160, 91);
            this.labelArrive2.Name = "labelArrive2";
            this.labelArrive2.Size = new System.Drawing.Size(92, 26);
            this.labelArrive2.TabIndex = 7;
            this.labelArrive2.Text = "Время прибытия\r\nигрока";
            this.labelArrive2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelReturn1
            // 
            this.labelReturn1.AutoSize = true;
            this.labelReturn1.Location = new System.Drawing.Point(319, 19);
            this.labelReturn1.Name = "labelReturn1";
            this.labelReturn1.Size = new System.Drawing.Size(112, 26);
            this.labelReturn1.TabIndex = 8;
            this.labelReturn1.Text = "Время возвращения\r\nигрока";
            this.labelReturn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelReturn2
            // 
            this.labelReturn2.AutoSize = true;
            this.labelReturn2.Location = new System.Drawing.Point(319, 91);
            this.labelReturn2.Name = "labelReturn2";
            this.labelReturn2.Size = new System.Drawing.Size(112, 26);
            this.labelReturn2.TabIndex = 9;
            this.labelReturn2.Text = "Время возвращения\r\nигрока";
            this.labelReturn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerArriveTime2
            // 
            this.dateTimePickerArriveTime2.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerArriveTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerArriveTime2.Location = new System.Drawing.Point(145, 118);
            this.dateTimePickerArriveTime2.Name = "dateTimePickerArriveTime2";
            this.dateTimePickerArriveTime2.ShowUpDown = true;
            this.dateTimePickerArriveTime2.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerArriveTime2.TabIndex = 8;
            this.dateTimePickerArriveTime2.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // dateTimePickerReturnTime1
            // 
            this.dateTimePickerReturnTime1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerReturnTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerReturnTime1.Location = new System.Drawing.Point(313, 46);
            this.dateTimePickerReturnTime1.Name = "dateTimePickerReturnTime1";
            this.dateTimePickerReturnTime1.ShowUpDown = true;
            this.dateTimePickerReturnTime1.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerReturnTime1.TabIndex = 4;
            this.dateTimePickerReturnTime1.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // dateTimePickerReturnTime2
            // 
            this.dateTimePickerReturnTime2.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerReturnTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerReturnTime2.Location = new System.Drawing.Point(313, 118);
            this.dateTimePickerReturnTime2.Name = "dateTimePickerReturnTime2";
            this.dateTimePickerReturnTime2.ShowUpDown = true;
            this.dateTimePickerReturnTime2.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerReturnTime2.TabIndex = 10;
            this.dateTimePickerReturnTime2.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // dateTimePickerStartTime1
            // 
            this.dateTimePickerStartTime1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerStartTime1.Enabled = false;
            this.dateTimePickerStartTime1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime1.Location = new System.Drawing.Point(599, 46);
            this.dateTimePickerStartTime1.Name = "dateTimePickerStartTime1";
            this.dateTimePickerStartTime1.ShowUpDown = true;
            this.dateTimePickerStartTime1.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerStartTime1.TabIndex = 14;
            this.dateTimePickerStartTime1.Value = new System.DateTime(2014, 12, 11, 0, 0, 0, 0);
            // 
            // dateTimePickerStartTime2
            // 
            this.dateTimePickerStartTime2.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerStartTime2.Enabled = false;
            this.dateTimePickerStartTime2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime2.Location = new System.Drawing.Point(599, 118);
            this.dateTimePickerStartTime2.Name = "dateTimePickerStartTime2";
            this.dateTimePickerStartTime2.ShowUpDown = true;
            this.dateTimePickerStartTime2.Size = new System.Drawing.Size(132, 20);
            this.dateTimePickerStartTime2.TabIndex = 16;
            this.dateTimePickerStartTime2.Value = new System.DateTime(2014, 12, 11, 0, 0, 0, 0);
            // 
            // labelAnswer
            // 
            this.labelAnswer.Location = new System.Drawing.Point(455, 96);
            this.labelAnswer.Name = "labelAnswer";
            this.labelAnswer.Size = new System.Drawing.Size(138, 52);
            this.labelAnswer.TabIndex = 15;
            // 
            // textBoxNick1
            // 
            this.textBoxNick1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNick1.Location = new System.Drawing.Point(18, 44);
            this.textBoxNick1.Name = "textBoxNick1";
            this.textBoxNick1.Size = new System.Drawing.Size(89, 22);
            this.textBoxNick1.TabIndex = 0;
            this.textBoxNick1.Text = "Первый";
            this.textBoxNick1.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Ник\r\n";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Ник\r\n";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxNick2
            // 
            this.textBoxNick2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNick2.Location = new System.Drawing.Point(18, 116);
            this.textBoxNick2.Name = "textBoxNick2";
            this.textBoxNick2.Size = new System.Drawing.Size(89, 22);
            this.textBoxNick2.TabIndex = 6;
            this.textBoxNick2.Text = "Второй";
            this.textBoxNick2.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // dateTimePickerFirst
            // 
            this.dateTimePickerFirst.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerFirst.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFirst.Location = new System.Drawing.Point(152, 188);
            this.dateTimePickerFirst.Name = "dateTimePickerFirst";
            this.dateTimePickerFirst.ShowUpDown = true;
            this.dateTimePickerFirst.Size = new System.Drawing.Size(126, 20);
            this.dateTimePickerFirst.TabIndex = 20;
            this.dateTimePickerFirst.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // dateTimePickerSecond
            // 
            this.dateTimePickerSecond.CustomFormat = "HH:mm:ss";
            this.dateTimePickerSecond.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerSecond.Location = new System.Drawing.Point(323, 188);
            this.dateTimePickerSecond.Name = "dateTimePickerSecond";
            this.dateTimePickerSecond.ShowUpDown = true;
            this.dateTimePickerSecond.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerSecond.TabIndex = 24;
            this.dateTimePickerSecond.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // buttonEvaluate
            // 
            this.buttonEvaluate.Location = new System.Drawing.Point(418, 186);
            this.buttonEvaluate.Name = "buttonEvaluate";
            this.buttonEvaluate.Size = new System.Drawing.Size(47, 23);
            this.buttonEvaluate.TabIndex = 26;
            this.buttonEvaluate.Text = "=";
            this.buttonEvaluate.UseVisualStyleBackColor = true;
            this.buttonEvaluate.Click += new System.EventHandler(this.buttonEvaluate_Click);
            // 
            // dateTimePickerResult
            // 
            this.dateTimePickerResult.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dateTimePickerResult.Enabled = false;
            this.dateTimePickerResult.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerResult.Location = new System.Drawing.Point(471, 188);
            this.dateTimePickerResult.Name = "dateTimePickerResult";
            this.dateTimePickerResult.ShowUpDown = true;
            this.dateTimePickerResult.Size = new System.Drawing.Size(126, 20);
            this.dateTimePickerResult.TabIndex = 28;
            this.dateTimePickerResult.Value = new System.DateTime(2014, 12, 11, 18, 42, 0, 0);
            // 
            // comboBoxSign
            // 
            this.comboBoxSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSign.FormattingEnabled = true;
            this.comboBoxSign.Items.AddRange(new object[] {
            "+",
            "-"});
            this.comboBoxSign.Location = new System.Drawing.Point(284, 187);
            this.comboBoxSign.Name = "comboBoxSign";
            this.comboBoxSign.Size = new System.Drawing.Size(33, 21);
            this.comboBoxSign.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Расчёт времени";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWalk1
            // 
            this.labelWalk1.AutoSize = true;
            this.labelWalk1.Location = new System.Drawing.Point(596, 68);
            this.labelWalk1.Name = "labelWalk1";
            this.labelWalk1.Size = new System.Drawing.Size(129, 13);
            this.labelWalk1.TabIndex = 29;
            this.labelWalk1.Text = "Время из A->B: 00:00:00";
            // 
            // labelWalk2
            // 
            this.labelWalk2.AutoSize = true;
            this.labelWalk2.Location = new System.Drawing.Point(596, 141);
            this.labelWalk2.Name = "labelWalk2";
            this.labelWalk2.Size = new System.Drawing.Size(129, 13);
            this.labelWalk2.TabIndex = 30;
            this.labelWalk2.Text = "Время из A->B: 00:00:00";
            // 
            // buttonEvaluate2
            // 
            this.buttonEvaluate2.Location = new System.Drawing.Point(325, 223);
            this.buttonEvaluate2.Name = "buttonEvaluate2";
            this.buttonEvaluate2.Size = new System.Drawing.Size(47, 23);
            this.buttonEvaluate2.TabIndex = 41;
            this.buttonEvaluate2.Text = "=";
            this.buttonEvaluate2.UseVisualStyleBackColor = true;
            this.buttonEvaluate2.Click += new System.EventHandler(this.buttonEvaluate2_Click);
            // 
            // numericUpDownPercent
            // 
            this.numericUpDownPercent.Location = new System.Drawing.Point(272, 224);
            this.numericUpDownPercent.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPercent.Name = "numericUpDownPercent";
            this.numericUpDownPercent.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownPercent.TabIndex = 40;
            this.numericUpDownPercent.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // comboBoxSign2
            // 
            this.comboBoxSign2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSign2.FormattingEnabled = true;
            this.comboBoxSign2.Items.AddRange(new object[] {
            "+",
            "-"});
            this.comboBoxSign2.Location = new System.Drawing.Point(233, 223);
            this.comboBoxSign2.Name = "comboBoxSign2";
            this.comboBoxSign2.Size = new System.Drawing.Size(33, 21);
            this.comboBoxSign2.TabIndex = 39;
            // 
            // dateTimePickerWalkTime
            // 
            this.dateTimePickerWalkTime.CustomFormat = "HH:mm:ss";
            this.dateTimePickerWalkTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerWalkTime.Location = new System.Drawing.Point(152, 224);
            this.dateTimePickerWalkTime.Name = "dateTimePickerWalkTime";
            this.dateTimePickerWalkTime.ShowUpDown = true;
            this.dateTimePickerWalkTime.Size = new System.Drawing.Size(75, 20);
            this.dateTimePickerWalkTime.TabIndex = 38;
            this.dateTimePickerWalkTime.Value = new System.DateTime(2014, 12, 11, 0, 0, 0, 0);
            // 
            // dateTimePickerResultWalkTime
            // 
            this.dateTimePickerResultWalkTime.CustomFormat = "HH:mm:ss";
            this.dateTimePickerResultWalkTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerResultWalkTime.Location = new System.Drawing.Point(376, 225);
            this.dateTimePickerResultWalkTime.Name = "dateTimePickerResultWalkTime";
            this.dateTimePickerResultWalkTime.ShowUpDown = true;
            this.dateTimePickerResultWalkTime.Size = new System.Drawing.Size(75, 20);
            this.dateTimePickerResultWalkTime.TabIndex = 42;
            this.dateTimePickerResultWalkTime.Value = new System.DateTime(2014, 12, 11, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Длительность миссии";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 256);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerResultWalkTime);
            this.Controls.Add(this.dateTimePickerWalkTime);
            this.Controls.Add(this.comboBoxSign2);
            this.Controls.Add(this.numericUpDownPercent);
            this.Controls.Add(this.buttonEvaluate2);
            this.Controls.Add(this.labelWalk2);
            this.Controls.Add(this.labelWalk1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSign);
            this.Controls.Add(this.dateTimePickerResult);
            this.Controls.Add(this.buttonEvaluate);
            this.Controls.Add(this.dateTimePickerSecond);
            this.Controls.Add(this.dateTimePickerFirst);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxNick2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxNick1);
            this.Controls.Add(this.labelAnswer);
            this.Controls.Add(this.dateTimePickerStartTime2);
            this.Controls.Add(this.dateTimePickerStartTime1);
            this.Controls.Add(this.dateTimePickerReturnTime2);
            this.Controls.Add(this.dateTimePickerReturnTime1);
            this.Controls.Add(this.dateTimePickerArriveTime2);
            this.Controls.Add(this.labelReturn2);
            this.Controls.Add(this.labelReturn1);
            this.Controls.Add(this.labelArrive2);
            this.Controls.Add(this.labelArrive1);
            this.Controls.Add(this.dateTimePickerArriveTime1);
            this.Controls.Add(this.labelStart2);
            this.Controls.Add(this.buttonGetTime);
            this.Controls.Add(this.labelStart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Временной помощник для игры MyLands";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStart1;
        private System.Windows.Forms.Button buttonGetTime;
        private System.Windows.Forms.Label labelStart2;
        private System.Windows.Forms.DateTimePicker dateTimePickerArriveTime1;
        private System.Windows.Forms.Label labelArrive1;
        private System.Windows.Forms.Label labelArrive2;
        private System.Windows.Forms.Label labelReturn1;
        private System.Windows.Forms.Label labelReturn2;
        private System.Windows.Forms.DateTimePicker dateTimePickerArriveTime2;
        private System.Windows.Forms.DateTimePicker dateTimePickerReturnTime1;
        private System.Windows.Forms.DateTimePicker dateTimePickerReturnTime2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime1;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime2;
        private System.Windows.Forms.Label labelAnswer;
        private System.Windows.Forms.TextBox textBoxNick1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxNick2;
        private System.Windows.Forms.DateTimePicker dateTimePickerFirst;
        private System.Windows.Forms.DateTimePicker dateTimePickerSecond;
        private System.Windows.Forms.Button buttonEvaluate;
        private System.Windows.Forms.DateTimePicker dateTimePickerResult;
        private System.Windows.Forms.ComboBox comboBoxSign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelWalk1;
        private System.Windows.Forms.Label labelWalk2;
        private System.Windows.Forms.Button buttonEvaluate2;
        private System.Windows.Forms.NumericUpDown numericUpDownPercent;
        private System.Windows.Forms.ComboBox comboBoxSign2;
        private System.Windows.Forms.DateTimePicker dateTimePickerWalkTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerResultWalkTime;
        private System.Windows.Forms.Label label2;
    }
}

