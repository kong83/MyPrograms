namespace SurgeryHelper.Forms
{
    partial class SelectAnamneseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAnamneseForm));
            this.buttonObstetricHistory = new System.Windows.Forms.Button();
            this.buttonAnamnese = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonObstetricHistory
            // 
            this.buttonObstetricHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonObstetricHistory.FlatAppearance.BorderSize = 0;
            this.buttonObstetricHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonObstetricHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonObstetricHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonObstetricHistory.Location = new System.Drawing.Point(158, 14);
            this.buttonObstetricHistory.Name = "buttonObstetricHistory";
            this.buttonObstetricHistory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonObstetricHistory.Size = new System.Drawing.Size(179, 36);
            this.buttonObstetricHistory.TabIndex = 63;
            this.buttonObstetricHistory.TabStop = false;
            this.buttonObstetricHistory.Text = "         Акушерский анамнез";
            this.buttonObstetricHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonObstetricHistory.UseVisualStyleBackColor = true;
            this.buttonObstetricHistory.MouseLeave += new System.EventHandler(this.buttonObstetricHistory_MouseLeave);
            this.buttonObstetricHistory.Click += new System.EventHandler(this.button_Click);
            this.buttonObstetricHistory.MouseEnter += new System.EventHandler(this.buttonObstetricHistory_MouseEnter);
            // 
            // buttonAnamnese
            // 
            this.buttonAnamnese.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonAnamnese.FlatAppearance.BorderSize = 0;
            this.buttonAnamnese.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnamnese.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAnamnese.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAnamnese.Location = new System.Drawing.Point(12, 14);
            this.buttonAnamnese.Name = "buttonAnamnese";
            this.buttonAnamnese.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonAnamnese.Size = new System.Drawing.Size(110, 36);
            this.buttonAnamnese.TabIndex = 62;
            this.buttonAnamnese.TabStop = false;
            this.buttonAnamnese.Text = "         Анамнез";
            this.buttonAnamnese.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAnamnese.UseVisualStyleBackColor = true;
            this.buttonAnamnese.MouseLeave += new System.EventHandler(this.buttonAnamnese_MouseLeave);
            this.buttonAnamnese.Click += new System.EventHandler(this.button_Click);
            this.buttonAnamnese.MouseEnter += new System.EventHandler(this.buttonAnamnese_MouseEnter);
            // 
            // SelectAnamneseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 68);
            this.Controls.Add(this.buttonObstetricHistory);
            this.Controls.Add(this.buttonAnamnese);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectAnamneseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор документа для генерации";
            this.Load += new System.EventHandler(this.SelectAnamneseForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonObstetricHistory;
        private System.Windows.Forms.Button buttonAnamnese;
    }
}