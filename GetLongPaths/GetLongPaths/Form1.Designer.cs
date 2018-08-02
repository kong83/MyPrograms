namespace GetLongPaths
{
    partial class Form1
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
            this.richTextBoxPaths = new System.Windows.Forms.RichTextBox();
            this.textBoxRootPath = new System.Windows.Forms.TextBox();
            this.buttonGetLongPaths = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // richTextBoxPaths
            // 
            this.richTextBoxPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxPaths.Location = new System.Drawing.Point(12, 32);
            this.richTextBoxPaths.Name = "richTextBoxPaths";
            this.richTextBoxPaths.Size = new System.Drawing.Size(679, 498);
            this.richTextBoxPaths.TabIndex = 0;
            this.richTextBoxPaths.Text = "";
            // 
            // textBoxRootPath
            // 
            this.textBoxRootPath.Location = new System.Drawing.Point(47, 6);
            this.textBoxRootPath.Name = "textBoxRootPath";
            this.textBoxRootPath.Size = new System.Drawing.Size(423, 20);
            this.textBoxRootPath.TabIndex = 1;
            // 
            // buttonGetLongPaths
            // 
            this.buttonGetLongPaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetLongPaths.Location = new System.Drawing.Point(585, 3);
            this.buttonGetLongPaths.Name = "buttonGetLongPaths";
            this.buttonGetLongPaths.Size = new System.Drawing.Size(106, 23);
            this.buttonGetLongPaths.TabIndex = 2;
            this.buttonGetLongPaths.Text = "Get Long Paths";
            this.buttonGetLongPaths.UseVisualStyleBackColor = true;
            this.buttonGetLongPaths.Click += new System.EventHandler(this.buttonGetLongPaths_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Path";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(476, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(47, 23);
            this.buttonOpen.TabIndex = 4;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 542);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGetLongPaths);
            this.Controls.Add(this.textBoxRootPath);
            this.Controls.Add(this.richTextBoxPaths);
            this.MinimumSize = new System.Drawing.Size(670, 125);
            this.Name = "Form1";
            this.Text = "Long Path Finder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxPaths;
        private System.Windows.Forms.TextBox textBoxRootPath;
        private System.Windows.Forms.Button buttonGetLongPaths;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

