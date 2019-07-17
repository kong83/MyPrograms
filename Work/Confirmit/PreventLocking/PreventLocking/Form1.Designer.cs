namespace PreventLocking
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonExcludeFromAutostart = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.Location = new System.Drawing.Point(11, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(276, 111);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            // 
            // buttonExcludeFromAutostart
            // 
            this.buttonExcludeFromAutostart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExcludeFromAutostart.Location = new System.Drawing.Point(12, 123);
            this.buttonExcludeFromAutostart.Name = "buttonExcludeFromAutostart";
            this.buttonExcludeFromAutostart.Size = new System.Drawing.Size(75, 23);
            this.buttonExcludeFromAutostart.TabIndex = 1;
            this.buttonExcludeFromAutostart.Text = "Remove";
            this.buttonExcludeFromAutostart.UseVisualStyleBackColor = true;
            this.buttonExcludeFromAutostart.Click += new System.EventHandler(this.buttonExcludeFromAutostart_Click);
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 153);
            this.Controls.Add(this.buttonExcludeFromAutostart);
            this.Controls.Add(this.labelInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Prevent Locking App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonExcludeFromAutostart;
        private System.Windows.Forms.Timer timer;
    }
}

