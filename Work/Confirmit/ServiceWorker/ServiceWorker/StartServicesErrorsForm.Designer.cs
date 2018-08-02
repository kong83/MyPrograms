namespace ServiceWorker
{
    partial class StartServicesErrorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartServicesErrorsForm));
            this.listBoxErrors = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxErrors.FormattingEnabled = true;
            this.listBoxErrors.Location = new System.Drawing.Point(3, 1);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxErrors.Size = new System.Drawing.Size(516, 355);
            this.listBoxErrors.TabIndex = 13;
            // 
            // StartServicesErrorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 361);
            this.Controls.Add(this.listBoxErrors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartServicesErrorsForm";
            this.Text = "StartServicesErrorsForm";
            this.Shown += new System.EventHandler(this.StartServicesErrorsForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxErrors;
    }
}