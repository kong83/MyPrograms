namespace TicTacToe
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
            this.panelField = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxAllTime = new System.Windows.Forms.ComboBox();
            this.comboBoxAddSec = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBoxCross = new System.Windows.Forms.PictureBox();
            this.pictureBoxNil = new System.Windows.Forms.PictureBox();
            this.boxTimeCross = new System.Windows.Forms.TextBox();
            this.boxTimeNil = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelNil = new System.Windows.Forms.Label();
            this.labelCross = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxNetInfo = new System.Windows.Forms.TextBox();
            this.buttonGetComputerStep = new System.Windows.Forms.Button();
            this.buttonViewLastStep = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.deliverButton = new System.Windows.Forms.Button();
            this.remiButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.serverButton = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNil)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelField
            // 
            this.panelField.Location = new System.Drawing.Point(12, 12);
            this.panelField.Name = "panelField";
            this.panelField.Size = new System.Drawing.Size(601, 601);
            this.panelField.TabIndex = 6;
            this.panelField.Paint += new System.Windows.Forms.PaintEventHandler(this.panelField_Paint);
            this.panelField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelField_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxAllTime);
            this.groupBox3.Controls.Add(this.comboBoxAddSec);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((204)));
            this.groupBox3.Location = new System.Drawing.Point(619, 541);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 72);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Контроль времени";
            // 
            // comboBoxAllTime
            // 
            this.comboBoxAllTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((204)));
            this.comboBoxAllTime.FormattingEnabled = true;
            this.comboBoxAllTime.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10",
            "20",
            "30",
            "60"});
            this.comboBoxAllTime.Location = new System.Drawing.Point(6, 32);
            this.comboBoxAllTime.MaxDropDownItems = 10;
            this.comboBoxAllTime.Name = "comboBoxAllTime";
            this.comboBoxAllTime.Size = new System.Drawing.Size(40, 21);
            this.comboBoxAllTime.TabIndex = 3;
            this.comboBoxAllTime.Text = "5";
            // 
            // comboBoxAddSec
            // 
            this.comboBoxAddSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((204)));
            this.comboBoxAddSec.FormattingEnabled = true;
            this.comboBoxAddSec.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxAddSec.Location = new System.Drawing.Point(102, 32);
            this.comboBoxAddSec.Name = "comboBoxAddSec";
            this.comboBoxAddSec.Size = new System.Drawing.Size(40, 21);
            this.comboBoxAddSec.TabIndex = 4;
            this.comboBoxAddSec.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((204)));
            this.label1.Location = new System.Drawing.Point(48, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "мин.   +";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((204)));
            this.label2.Location = new System.Drawing.Point(143, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "сек.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBoxCross);
            this.groupBox2.Controls.Add(this.pictureBoxNil);
            this.groupBox2.Controls.Add(this.boxTimeCross);
            this.groupBox2.Controls.Add(this.boxTimeNil);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((204)));
            this.groupBox2.Location = new System.Drawing.Point(619, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 72);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Время";
            // 
            // pictureBoxCross
            // 
            this.pictureBoxCross.Image = global::TicTacToe.Properties.Resources.cross;
            this.pictureBoxCross.Location = new System.Drawing.Point(6, 16);
            this.pictureBoxCross.Name = "pictureBoxCross";
            this.pictureBoxCross.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxCross.TabIndex = 35;
            this.pictureBoxCross.TabStop = false;
            // 
            // pictureBoxNil
            // 
            this.pictureBoxNil.Image = global::TicTacToe.Properties.Resources.nil;
            this.pictureBoxNil.Location = new System.Drawing.Point(6, 42);
            this.pictureBoxNil.Name = "pictureBoxNil";
            this.pictureBoxNil.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxNil.TabIndex = 36;
            this.pictureBoxNil.TabStop = false;
            // 
            // boxTimeCross
            // 
            this.boxTimeCross.BackColor = System.Drawing.SystemColors.Window;
            this.boxTimeCross.CausesValidation = false;
            this.boxTimeCross.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((204)));
            this.boxTimeCross.Location = new System.Drawing.Point(38, 16);
            this.boxTimeCross.Name = "boxTimeCross";
            this.boxTimeCross.ReadOnly = true;
            this.boxTimeCross.Size = new System.Drawing.Size(66, 20);
            this.boxTimeCross.TabIndex = 0;
            this.boxTimeCross.TabStop = false;
            // 
            // boxTimeNil
            // 
            this.boxTimeNil.BackColor = System.Drawing.SystemColors.Window;
            this.boxTimeNil.CausesValidation = false;
            this.boxTimeNil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((204)));
            this.boxTimeNil.Location = new System.Drawing.Point(38, 42);
            this.boxTimeNil.Name = "boxTimeNil";
            this.boxTimeNil.ReadOnly = true;
            this.boxTimeNil.Size = new System.Drawing.Size(66, 20);
            this.boxTimeNil.TabIndex = 1;
            this.boxTimeNil.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelNil);
            this.groupBox1.Controls.Add(this.labelCross);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((204)));
            this.groupBox1.Location = new System.Drawing.Point(740, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(55, 72);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Счёт";
            // 
            // labelNil
            // 
            this.labelNil.AutoSize = true;
            this.labelNil.Location = new System.Drawing.Point(19, 45);
            this.labelNil.Name = "labelNil";
            this.labelNil.Size = new System.Drawing.Size(14, 13);
            this.labelNil.TabIndex = 13;
            this.labelNil.Text = "0";
            // 
            // labelCross
            // 
            this.labelCross.AutoSize = true;
            this.labelCross.Location = new System.Drawing.Point(19, 19);
            this.labelCross.Name = "labelCross";
            this.labelCross.Size = new System.Drawing.Size(14, 13);
            this.labelCross.TabIndex = 14;
            this.labelCross.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBoxNetInfo
            // 
            this.textBoxNetInfo.BackColor = System.Drawing.Color.White;
            this.textBoxNetInfo.Location = new System.Drawing.Point(619, 154);
            this.textBoxNetInfo.Multiline = true;
            this.textBoxNetInfo.Name = "textBoxNetInfo";
            this.textBoxNetInfo.ReadOnly = true;
            this.textBoxNetInfo.Size = new System.Drawing.Size(176, 289);
            this.textBoxNetInfo.TabIndex = 52;
            this.textBoxNetInfo.TabStop = false;
            // 
            // buttonGetComputerStep
            // 
            this.buttonGetComputerStep.Location = new System.Drawing.Point(651, 449);
            this.buttonGetComputerStep.Name = "buttonGetComputerStep";
            this.buttonGetComputerStep.Size = new System.Drawing.Size(110, 23);
            this.buttonGetComputerStep.TabIndex = 0;
            this.buttonGetComputerStep.Text = "Get Computer Step";
            this.buttonGetComputerStep.UseVisualStyleBackColor = true;
            this.buttonGetComputerStep.Click += new System.EventHandler(this.buttonGetComputerStep_Click);
            // 
            // buttonViewLastStep
            // 
            this.buttonViewLastStep.BackgroundImage = global::TicTacToe.Properties.Resources.ShowLastStep;
            this.buttonViewLastStep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonViewLastStep.Location = new System.Drawing.Point(733, 486);
            this.buttonViewLastStep.Name = "buttonViewLastStep";
            this.buttonViewLastStep.Size = new System.Drawing.Size(40, 40);
            this.buttonViewLastStep.TabIndex = 53;
            this.buttonViewLastStep.UseVisualStyleBackColor = true;
            this.buttonViewLastStep.Visible = false;
            this.buttonViewLastStep.MouseLeave += new System.EventHandler(this.buttonViewLastStep_MouseLeave);
            this.buttonViewLastStep.Click += new System.EventHandler(this.buttonViewLastStep_Click);
            this.buttonViewLastStep.MouseEnter += new System.EventHandler(this.buttonViewLastStep_MouseEnter);
            // 
            // startGameButton
            // 
            this.startGameButton.BackgroundImage = global::TicTacToe.Properties.Resources.start;
            this.startGameButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.startGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((204)));
            this.startGameButton.Location = new System.Drawing.Point(687, 486);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(40, 40);
            this.startGameButton.TabIndex = 49;
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.MouseLeave += new System.EventHandler(this.startGameButton_MouseLeave);
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            this.startGameButton.MouseEnter += new System.EventHandler(this.startGameButton_MouseEnter);
            // 
            // deliverButton
            // 
            this.deliverButton.BackgroundImage = global::TicTacToe.Properties.Resources.stop;
            this.deliverButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.deliverButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((204)));
            this.deliverButton.Location = new System.Drawing.Point(686, 486);
            this.deliverButton.Name = "deliverButton";
            this.deliverButton.Size = new System.Drawing.Size(40, 40);
            this.deliverButton.TabIndex = 50;
            this.deliverButton.UseVisualStyleBackColor = true;
            this.deliverButton.Visible = false;
            this.deliverButton.MouseLeave += new System.EventHandler(this.deliverButton_MouseLeave);
            this.deliverButton.Click += new System.EventHandler(this.deliverButton_Click);
            this.deliverButton.MouseEnter += new System.EventHandler(this.deliverButton_MouseEnter);
            // 
            // remiButton
            // 
            this.remiButton.BackgroundImage = global::TicTacToe.Properties.Resources.remi;
            this.remiButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.remiButton.Location = new System.Drawing.Point(640, 486);
            this.remiButton.Name = "remiButton";
            this.remiButton.Size = new System.Drawing.Size(40, 40);
            this.remiButton.TabIndex = 51;
            this.remiButton.UseVisualStyleBackColor = true;
            this.remiButton.Visible = false;
            this.remiButton.MouseLeave += new System.EventHandler(this.remiButton_MouseLeave);
            this.remiButton.Click += new System.EventHandler(this.remiButton_Click);
            this.remiButton.MouseEnter += new System.EventHandler(this.remiButton_MouseEnter);
            // 
            // disconnectButton
            // 
            this.disconnectButton.BackgroundImage = global::TicTacToe.Properties.Resources.connectDel;
            this.disconnectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.disconnectButton.Location = new System.Drawing.Point(686, 99);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(40, 40);
            this.disconnectButton.TabIndex = 45;
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Visible = false;
            this.disconnectButton.MouseLeave += new System.EventHandler(this.disconnectButton_MouseLeave);
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            this.disconnectButton.MouseEnter += new System.EventHandler(this.disconnectButton_MouseEnter);
            // 
            // connectButton
            // 
            this.connectButton.BackgroundImage = global::TicTacToe.Properties.Resources.connect;
            this.connectButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.connectButton.Location = new System.Drawing.Point(717, 99);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(40, 40);
            this.connectButton.TabIndex = 44;
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.MouseLeave += new System.EventHandler(this.connectButton_MouseLeave);
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            this.connectButton.MouseEnter += new System.EventHandler(this.connectButton_MouseEnter);
            // 
            // serverButton
            // 
            this.serverButton.BackgroundImage = global::TicTacToe.Properties.Resources.setServer;
            this.serverButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.serverButton.Location = new System.Drawing.Point(657, 99);
            this.serverButton.Name = "serverButton";
            this.serverButton.Size = new System.Drawing.Size(40, 40);
            this.serverButton.TabIndex = 43;
            this.serverButton.UseVisualStyleBackColor = true;
            this.serverButton.MouseLeave += new System.EventHandler(this.serverButton_MouseLeave);
            this.serverButton.Click += new System.EventHandler(this.serverButton_Click);
            this.serverButton.MouseEnter += new System.EventHandler(this.serverButton_MouseEnter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 624);
            this.Controls.Add(this.buttonViewLastStep);
            this.Controls.Add(this.buttonGetComputerStep);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.textBoxNetInfo);
            this.Controls.Add(this.deliverButton);
            this.Controls.Add(this.remiButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.serverButton);
            this.Controls.Add(this.panelField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNil)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelField;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button serverButton;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox comboBoxAllTime;
        private System.Windows.Forms.ComboBox comboBoxAddSec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.PictureBox pictureBoxCross;
        public System.Windows.Forms.PictureBox pictureBoxNil;
        private System.Windows.Forms.TextBox boxTimeCross;
        private System.Windows.Forms.TextBox boxTimeNil;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelNil;
        private System.Windows.Forms.Label labelCross;
        private System.Windows.Forms.Button deliverButton;
        private System.Windows.Forms.Button remiButton;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.TextBox textBoxNetInfo;
        private System.Windows.Forms.Button buttonGetComputerStep;
        private System.Windows.Forms.Button buttonViewLastStep;
    }
}

