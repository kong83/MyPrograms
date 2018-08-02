namespace SampleCreator
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
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFirstColumnName = new System.Windows.Forms.TextBox();
            this.textBoxSecondColumnName = new System.Windows.Forms.TextBox();
            this.textBoxThirdColumnName = new System.Windows.Forms.TextBox();
            this.textBoxSecondValue = new System.Windows.Forms.TextBox();
            this.textBoxFirstValue = new System.Windows.Forms.TextBox();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxFirstNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSecondNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxMaxCntInFile = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxSingleNames = new System.Windows.Forms.TextBox();
            this.textBoxNumericNames = new System.Windows.Forms.TextBox();
            this.textBoxOpenNames = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxSingleFrom = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxSingleTo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxNumericTo = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxNumericFrom = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxOpenPrefix = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxOpenFrom = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(85, 14);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(121, 20);
            this.textBoxFileName.TabIndex = 0;
            this.textBoxFileName.Text = "sample";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя файла";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(598, 135);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Название первой колонки";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Название второй колонки";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Название третьей колонки";
            // 
            // textBoxFirstColumnName
            // 
            this.textBoxFirstColumnName.Location = new System.Drawing.Point(182, 50);
            this.textBoxFirstColumnName.Name = "textBoxFirstColumnName";
            this.textBoxFirstColumnName.Size = new System.Drawing.Size(152, 20);
            this.textBoxFirstColumnName.TabIndex = 6;
            this.textBoxFirstColumnName.Text = "RespondentName";
            // 
            // textBoxSecondColumnName
            // 
            this.textBoxSecondColumnName.Location = new System.Drawing.Point(182, 76);
            this.textBoxSecondColumnName.Name = "textBoxSecondColumnName";
            this.textBoxSecondColumnName.Size = new System.Drawing.Size(152, 20);
            this.textBoxSecondColumnName.TabIndex = 7;
            this.textBoxSecondColumnName.Text = "email";
            // 
            // textBoxThirdColumnName
            // 
            this.textBoxThirdColumnName.Location = new System.Drawing.Point(182, 102);
            this.textBoxThirdColumnName.Name = "textBoxThirdColumnName";
            this.textBoxThirdColumnName.Size = new System.Drawing.Size(152, 20);
            this.textBoxThirdColumnName.TabIndex = 8;
            this.textBoxThirdColumnName.Text = "TelephoneNumber";
            // 
            // textBoxSecondValue
            // 
            this.textBoxSecondValue.Location = new System.Drawing.Point(521, 102);
            this.textBoxSecondValue.Name = "textBoxSecondValue";
            this.textBoxSecondValue.Size = new System.Drawing.Size(152, 20);
            this.textBoxSecondValue.TabIndex = 14;
            this.textBoxSecondValue.Text = "1234567";
            // 
            // textBoxFirstValue
            // 
            this.textBoxFirstValue.Location = new System.Drawing.Point(521, 76);
            this.textBoxFirstValue.Name = "textBoxFirstValue";
            this.textBoxFirstValue.Size = new System.Drawing.Size(152, 20);
            this.textBoxFirstValue.TabIndex = 13;
            this.textBoxFirstValue.Text = "user@mail.ru";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(521, 50);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(98, 20);
            this.textBoxPrefix.TabIndex = 12;
            this.textBoxPrefix.Text = "User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(351, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Значение для третьей колонки";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(351, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Значение для второй колонки";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(351, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Префикс для перовой колонки";
            // 
            // textBoxFirstNumber
            // 
            this.textBoxFirstNumber.Location = new System.Drawing.Point(291, 14);
            this.textBoxFirstNumber.Name = "textBoxFirstNumber";
            this.textBoxFirstNumber.Size = new System.Drawing.Size(55, 20);
            this.textBoxFirstNumber.TabIndex = 15;
            this.textBoxFirstNumber.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(228, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "номера от";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(348, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "до";
            // 
            // textBoxSecondNumber
            // 
            this.textBoxSecondNumber.Location = new System.Drawing.Point(371, 14);
            this.textBoxSecondNumber.Name = "textBoxSecondNumber";
            this.textBoxSecondNumber.Size = new System.Drawing.Size(73, 20);
            this.textBoxSecondNumber.TabIndex = 18;
            this.textBoxSecondNumber.Text = "100000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(461, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "в файле не больше";
            // 
            // textBoxMaxCntInFile
            // 
            this.textBoxMaxCntInFile.Location = new System.Drawing.Point(571, 13);
            this.textBoxMaxCntInFile.Name = "textBoxMaxCntInFile";
            this.textBoxMaxCntInFile.Size = new System.Drawing.Size(51, 20);
            this.textBoxMaxCntInFile.TabIndex = 20;
            this.textBoxMaxCntInFile.Text = "100000";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(626, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "записей";
            // 
            // textBoxSingleNames
            // 
            this.textBoxSingleNames.Location = new System.Drawing.Point(85, 137);
            this.textBoxSingleNames.Name = "textBoxSingleNames";
            this.textBoxSingleNames.Size = new System.Drawing.Size(96, 20);
            this.textBoxSingleNames.TabIndex = 22;
            this.textBoxSingleNames.Text = "q1,q4,q7,q10";
            // 
            // textBoxNumericNames
            // 
            this.textBoxNumericNames.Location = new System.Drawing.Point(85, 163);
            this.textBoxNumericNames.Name = "textBoxNumericNames";
            this.textBoxNumericNames.Size = new System.Drawing.Size(96, 20);
            this.textBoxNumericNames.TabIndex = 23;
            this.textBoxNumericNames.Text = "q2,q6,q9";
            // 
            // textBoxOpenNames
            // 
            this.textBoxOpenNames.Location = new System.Drawing.Point(85, 189);
            this.textBoxOpenNames.Name = "textBoxOpenNames";
            this.textBoxOpenNames.Size = new System.Drawing.Size(96, 20);
            this.textBoxOpenNames.TabIndex = 24;
            this.textBoxOpenNames.Text = "q3,q5,q8";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Single";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 166);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Numeric";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Open";
            // 
            // textBoxSingleFrom
            // 
            this.textBoxSingleFrom.Location = new System.Drawing.Point(238, 137);
            this.textBoxSingleFrom.Name = "textBoxSingleFrom";
            this.textBoxSingleFrom.Size = new System.Drawing.Size(30, 20);
            this.textBoxSingleFrom.TabIndex = 28;
            this.textBoxSingleFrom.Text = "1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(196, 140);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "от";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(288, 140);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "до";
            // 
            // textBoxSingleTo
            // 
            this.textBoxSingleTo.Location = new System.Drawing.Point(330, 137);
            this.textBoxSingleTo.Name = "textBoxSingleTo";
            this.textBoxSingleTo.Size = new System.Drawing.Size(30, 20);
            this.textBoxSingleTo.TabIndex = 30;
            this.textBoxSingleTo.Text = "2";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(288, 166);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 13);
            this.label17.TabIndex = 35;
            this.label17.Text = "до";
            // 
            // textBoxNumericTo
            // 
            this.textBoxNumericTo.Location = new System.Drawing.Point(330, 163);
            this.textBoxNumericTo.Name = "textBoxNumericTo";
            this.textBoxNumericTo.Size = new System.Drawing.Size(30, 20);
            this.textBoxNumericTo.TabIndex = 34;
            this.textBoxNumericTo.Text = "99";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(196, 166);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(18, 13);
            this.label18.TabIndex = 33;
            this.label18.Text = "от";
            // 
            // textBoxNumericFrom
            // 
            this.textBoxNumericFrom.Location = new System.Drawing.Point(238, 163);
            this.textBoxNumericFrom.Name = "textBoxNumericFrom";
            this.textBoxNumericFrom.Size = new System.Drawing.Size(30, 20);
            this.textBoxNumericFrom.TabIndex = 32;
            this.textBoxNumericFrom.Text = "-99";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(183, 192);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(51, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "префикс";
            // 
            // textBoxOpenPrefix
            // 
            this.textBoxOpenPrefix.Location = new System.Drawing.Point(238, 189);
            this.textBoxOpenPrefix.Name = "textBoxOpenPrefix";
            this.textBoxOpenPrefix.Size = new System.Drawing.Size(66, 20);
            this.textBoxOpenPrefix.TabIndex = 36;
            this.textBoxOpenPrefix.Text = "value";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(314, 192);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(62, 13);
            this.label20.TabIndex = 38;
            this.label20.Text = "+ номер от";
            // 
            // textBoxOpenFrom
            // 
            this.textBoxOpenFrom.Location = new System.Drawing.Point(394, 189);
            this.textBoxOpenFrom.Name = "textBoxOpenFrom";
            this.textBoxOpenFrom.Size = new System.Drawing.Size(30, 20);
            this.textBoxOpenFrom.TabIndex = 39;
            this.textBoxOpenFrom.Text = "1";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(598, 182);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 40;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(625, 53);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(48, 13);
            this.label21.TabIndex = 41;
            this.label21.Text = "+ номер";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 218);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBoxOpenFrom);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textBoxOpenPrefix);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBoxNumericTo);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBoxNumericFrom);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxSingleTo);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBoxSingleFrom);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxOpenNames);
            this.Controls.Add(this.textBoxNumericNames);
            this.Controls.Add(this.textBoxSingleNames);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxMaxCntInFile);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxSecondNumber);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxFirstNumber);
            this.Controls.Add(this.textBoxSecondValue);
            this.Controls.Add(this.textBoxFirstValue);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxThirdColumnName);
            this.Controls.Add(this.textBoxSecondColumnName);
            this.Controls.Add(this.textBoxFirstColumnName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFirstColumnName;
        private System.Windows.Forms.TextBox textBoxSecondColumnName;
        private System.Windows.Forms.TextBox textBoxThirdColumnName;
        private System.Windows.Forms.TextBox textBoxSecondValue;
        private System.Windows.Forms.TextBox textBoxFirstValue;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxFirstNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSecondNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxMaxCntInFile;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxSingleNames;
        private System.Windows.Forms.TextBox textBoxNumericNames;
        private System.Windows.Forms.TextBox textBoxOpenNames;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxSingleFrom;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxSingleTo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxNumericTo;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxNumericFrom;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxOpenPrefix;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxOpenFrom;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label21;
    }
}

