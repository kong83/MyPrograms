namespace SurgeryHelper.Forms
{
    partial class ColorInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorInfoForm));
            this.panelNoColor = new System.Windows.Forms.Panel();
            this.panelLightColor = new System.Windows.Forms.Panel();
            this.panelReleaseDateColor = new System.Windows.Forms.Panel();
            this.panelLineOfCommunicationColor = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelNoColor
            // 
            this.panelNoColor.BackColor = System.Drawing.Color.White;
            this.panelNoColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNoColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelNoColor.Location = new System.Drawing.Point(12, 12);
            this.panelNoColor.Name = "panelNoColor";
            this.panelNoColor.Size = new System.Drawing.Size(97, 28);
            this.panelNoColor.TabIndex = 0;
            this.panelNoColor.BackColorChanged += new System.EventHandler(this.panel_BackColorChanged);
            this.panelNoColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelNoColor_MouseClick);
            // 
            // panelLightColor
            // 
            this.panelLightColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelLightColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLightColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelLightColor.Location = new System.Drawing.Point(12, 46);
            this.panelLightColor.Name = "panelLightColor";
            this.panelLightColor.Size = new System.Drawing.Size(97, 28);
            this.panelLightColor.TabIndex = 1;
            this.panelLightColor.BackColorChanged += new System.EventHandler(this.panel_BackColorChanged);
            this.panelLightColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelNoColor_MouseClick);
            // 
            // panelReleaseDateColor
            // 
            this.panelReleaseDateColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(50)))));
            this.panelReleaseDateColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReleaseDateColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelReleaseDateColor.Location = new System.Drawing.Point(12, 80);
            this.panelReleaseDateColor.Name = "panelReleaseDateColor";
            this.panelReleaseDateColor.Size = new System.Drawing.Size(97, 28);
            this.panelReleaseDateColor.TabIndex = 1;
            this.panelReleaseDateColor.BackColorChanged += new System.EventHandler(this.panel_BackColorChanged);
            this.panelReleaseDateColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelNoColor_MouseClick);
            // 
            // panelLineOfCommunicationColor
            // 
            this.panelLineOfCommunicationColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.panelLineOfCommunicationColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLineOfCommunicationColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelLineOfCommunicationColor.Location = new System.Drawing.Point(12, 114);
            this.panelLineOfCommunicationColor.Name = "panelLineOfCommunicationColor";
            this.panelLineOfCommunicationColor.Size = new System.Drawing.Size(97, 28);
            this.panelLineOfCommunicationColor.TabIndex = 1;
            this.panelLineOfCommunicationColor.BackColorChanged += new System.EventHandler(this.panel_BackColorChanged);
            this.panelLineOfCommunicationColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelNoColor_MouseClick);
            // 
            // buttonOK
            // 
            this.buttonOK.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(12, 146);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(97, 40);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.TabStop = false;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.MouseLeave += new System.EventHandler(this.buttonOK_MouseLeave);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            this.buttonOK.Enter += new System.EventHandler(this.DropFocus);
            this.buttonOK.MouseEnter += new System.EventHandler(this.buttonOK_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выписанный пациент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Пациент, находящийся \r\nна лечении в стационаре";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пациент, который \r\nсегодня выписывается";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 26);
            this.label4.TabIndex = 6;
            this.label4.Text = "Пациент, которому нужно\r\nнаписать \"Этапный эпикриз\"";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            // 
            // buttonDefault
            // 
            this.buttonDefault.BackgroundImage = global::SurgeryHelper.Properties.Resources.undo;
            this.buttonDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDefault.FlatAppearance.BorderSize = 0;
            this.buttonDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDefault.Location = new System.Drawing.Point(152, 146);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(60, 40);
            this.buttonDefault.TabIndex = 7;
            this.buttonDefault.TabStop = false;
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Visible = false;
            this.buttonDefault.MouseLeave += new System.EventHandler(this.buttonDefault_MouseLeave);
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            this.buttonDefault.Enter += new System.EventHandler(this.DropFocus);
            this.buttonDefault.MouseEnter += new System.EventHandler(this.buttonDefault_MouseEnter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "buttonDropFocus";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ColorInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 189);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.panelReleaseDateColor);
            this.Controls.Add(this.panelLineOfCommunicationColor);
            this.Controls.Add(this.panelLightColor);
            this.Controls.Add(this.panelNoColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Настройка подсветки строк";
            this.Load += new System.EventHandler(this.ColorInfoForm_Load);
            this.LocationChanged += new System.EventHandler(this.ColorInfoForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelNoColor;
        private System.Windows.Forms.Panel panelLightColor;
        private System.Windows.Forms.Panel panelReleaseDateColor;
        private System.Windows.Forms.Panel panelLineOfCommunicationColor;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.Button button1;
    }
}