namespace SurgeryHelper.Forms
{
    partial class PaintDoublePictureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintDoublePictureForm));
            this.checkBoxCreatePalette = new System.Windows.Forms.CheckBox();
            this.buttonDocuments = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxCaption = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxCreatePalette
            // 
            this.checkBoxCreatePalette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCreatePalette.AutoSize = true;
            this.checkBoxCreatePalette.Location = new System.Drawing.Point(78, 422);
            this.checkBoxCreatePalette.Name = "checkBoxCreatePalette";
            this.checkBoxCreatePalette.Size = new System.Drawing.Size(152, 17);
            this.checkBoxCreatePalette.TabIndex = 92;
            this.checkBoxCreatePalette.Text = "Экспортировать палитру";
            this.checkBoxCreatePalette.UseVisualStyleBackColor = true;
            // 
            // buttonDocuments
            // 
            this.buttonDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDocuments.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonDocuments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDocuments.FlatAppearance.BorderSize = 0;
            this.buttonDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDocuments.Location = new System.Drawing.Point(32, 401);
            this.buttonDocuments.Name = "buttonDocuments";
            this.buttonDocuments.Size = new System.Drawing.Size(40, 40);
            this.buttonDocuments.TabIndex = 91;
            this.buttonDocuments.TabStop = false;
            this.buttonDocuments.UseVisualStyleBackColor = true;
            this.buttonDocuments.MouseLeave += new System.EventHandler(this.buttonDocuments_MouseLeave);
            this.buttonDocuments.Click += new System.EventHandler(this.buttonDocuments_Click);
            this.buttonDocuments.MouseEnter += new System.EventHandler(this.buttonDocuments_MouseEnter);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(404, 401);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 81;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(469, 401);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 80;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // checkBoxCaption
            // 
            this.checkBoxCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCaption.AutoSize = true;
            this.checkBoxCaption.Location = new System.Drawing.Point(78, 403);
            this.checkBoxCaption.Name = "checkBoxCaption";
            this.checkBoxCaption.Size = new System.Drawing.Size(165, 17);
            this.checkBoxCaption.TabIndex = 93;
            this.checkBoxCaption.Text = "Экспортировать заголовок";
            this.checkBoxCaption.UseVisualStyleBackColor = true;
            // 
            // PaintDoublePictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 443);
            this.Controls.Add(this.checkBoxCaption);
            this.Controls.Add(this.checkBoxCreatePalette);
            this.Controls.Add(this.buttonDocuments);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaintDoublePictureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PictureForm";
            this.Load += new System.EventHandler(this.PaintDoublePictureForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PaintPictureForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDocuments;
        private System.Windows.Forms.CheckBox checkBoxCreatePalette;
        private System.Windows.Forms.CheckBox checkBoxCaption;

    }
}