namespace SurgeryHelper.Forms
{
    partial class MergeShowDifferenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeShowDifferenceForm));
            this.labelOwnInfo = new System.Windows.Forms.Label();
            this.labelForeignInfo = new System.Windows.Forms.Label();
            this.richTextBoxOwnValue = new System.Windows.Forms.RichTextBox();
            this.richTextBoxForeignValue = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelMove = new System.Windows.Forms.Panel();
            this.panelMove.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelOwnInfo
            // 
            this.labelOwnInfo.Location = new System.Drawing.Point(2, 9);
            this.labelOwnInfo.Name = "labelOwnInfo";
            this.labelOwnInfo.Size = new System.Drawing.Size(390, 13);
            this.labelOwnInfo.TabIndex = 2;
            this.labelOwnInfo.Text = "Значение в нашей базе";
            this.labelOwnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelForeignInfo
            // 
            this.labelForeignInfo.Location = new System.Drawing.Point(445, 9);
            this.labelForeignInfo.Name = "labelForeignInfo";
            this.labelForeignInfo.Size = new System.Drawing.Size(387, 13);
            this.labelForeignInfo.TabIndex = 3;
            this.labelForeignInfo.Text = "Значение во внешней базе";
            this.labelForeignInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxOwnValue
            // 
            this.richTextBoxOwnValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxOwnValue.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxOwnValue.Location = new System.Drawing.Point(2, 25);
            this.richTextBoxOwnValue.Name = "richTextBoxOwnValue";
            this.richTextBoxOwnValue.Size = new System.Drawing.Size(385, 425);
            this.richTextBoxOwnValue.TabIndex = 4;
            this.richTextBoxOwnValue.Text = "";
            // 
            // richTextBoxForeignValue
            // 
            this.richTextBoxForeignValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBoxForeignValue.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxForeignValue.Location = new System.Drawing.Point(437, 25);
            this.richTextBoxForeignValue.Name = "richTextBoxForeignValue";
            this.richTextBoxForeignValue.Size = new System.Drawing.Size(392, 425);
            this.richTextBoxForeignValue.TabIndex = 5;
            this.richTextBoxForeignValue.Text = "";
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(7, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 47;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // panelMove
            // 
            this.panelMove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMove.Controls.Add(this.buttonClose);
            this.panelMove.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.panelMove.Location = new System.Drawing.Point(386, 25);
            this.panelMove.Name = "panelMove";
            this.panelMove.Size = new System.Drawing.Size(53, 425);
            this.panelMove.TabIndex = 48;
            this.panelMove.LocationChanged += new System.EventHandler(this.panelMove_LocationChanged);
            this.panelMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMove_MouseMove);
            this.panelMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMove_MouseDown);
            this.panelMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMove_MouseUp);
            // 
            // MergeShowDifferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 462);
            this.Controls.Add(this.richTextBoxForeignValue);
            this.Controls.Add(this.richTextBoxOwnValue);
            this.Controls.Add(this.labelForeignInfo);
            this.Controls.Add(this.labelOwnInfo);
            this.Controls.Add(this.panelMove);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MergeShowDifferenceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сравнение значений";
            this.Load += new System.EventHandler(this.MergeShowDifferenceForm_Load);
            this.SizeChanged += new System.EventHandler(this.MergeShowDifferenceForm_SizeChanged);
            this.Shown += new System.EventHandler(this.MergeShowDifferenceForm_Shown);
            this.LocationChanged += new System.EventHandler(this.MergeShowDifferenceForm_LocationChanged);
            this.panelMove.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelOwnInfo;
        private System.Windows.Forms.Label labelForeignInfo;
        private System.Windows.Forms.RichTextBox richTextBoxOwnValue;
        private System.Windows.Forms.RichTextBox richTextBoxForeignValue;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelMove;
    }
}