namespace SurgeryHelper
{
    partial class ImportKSGForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportKSGForm));
            this.textBoxDayKSGPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNightKSGPath = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpenDayKSG = new System.Windows.Forms.Button();
            this.buttonOpenNightKSG = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // textBoxDayKSGPath
            // 
            this.textBoxDayKSGPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDayKSGPath.Location = new System.Drawing.Point(12, 28);
            this.textBoxDayKSGPath.Name = "textBoxDayKSGPath";
            this.textBoxDayKSGPath.Size = new System.Drawing.Size(519, 20);
            this.textBoxDayKSGPath.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Путь до xml файла с информацией о дневных кодах КСГ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Путь до xml файла с информацией о ночных кодах КСГ";
            // 
            // textBoxNightKSGPath
            // 
            this.textBoxNightKSGPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNightKSGPath.Location = new System.Drawing.Point(12, 90);
            this.textBoxNightKSGPath.Name = "textBoxNightKSGPath";
            this.textBoxNightKSGPath.Size = new System.Drawing.Size(519, 20);
            this.textBoxNightKSGPath.TabIndex = 72;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(224, 118);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 75;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(279, 118);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 74;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonOpenDayKSG
            // 
            this.buttonOpenDayKSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenDayKSG.BackgroundImage = global::SurgeryHelper.Properties.Resources.open16;
            this.buttonOpenDayKSG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOpenDayKSG.FlatAppearance.BorderSize = 0;
            this.buttonOpenDayKSG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenDayKSG.Location = new System.Drawing.Point(537, 27);
            this.buttonOpenDayKSG.Name = "buttonOpenDayKSG";
            this.buttonOpenDayKSG.Size = new System.Drawing.Size(20, 20);
            this.buttonOpenDayKSG.TabIndex = 77;
            this.buttonOpenDayKSG.TabStop = false;
            this.buttonOpenDayKSG.UseVisualStyleBackColor = true;
            this.buttonOpenDayKSG.MouseLeave += new System.EventHandler(this.buttonOpenDayKSG_MouseLeave);
            this.buttonOpenDayKSG.Click += new System.EventHandler(this.buttonOpenDayKSG_Click);
            this.buttonOpenDayKSG.MouseEnter += new System.EventHandler(this.buttonOpenDayKSG_MouseEnter);
            // 
            // buttonOpenNightKSG
            // 
            this.buttonOpenNightKSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenNightKSG.BackgroundImage = global::SurgeryHelper.Properties.Resources.open16;
            this.buttonOpenNightKSG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOpenNightKSG.FlatAppearance.BorderSize = 0;
            this.buttonOpenNightKSG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenNightKSG.Location = new System.Drawing.Point(537, 89);
            this.buttonOpenNightKSG.Name = "buttonOpenNightKSG";
            this.buttonOpenNightKSG.Size = new System.Drawing.Size(20, 20);
            this.buttonOpenNightKSG.TabIndex = 78;
            this.buttonOpenNightKSG.TabStop = false;
            this.buttonOpenNightKSG.UseVisualStyleBackColor = true;
            this.buttonOpenNightKSG.MouseLeave += new System.EventHandler(this.buttonOpenNightKSG_MouseLeave);
            this.buttonOpenNightKSG.Click += new System.EventHandler(this.buttonOpenNightKSG_Click);
            this.buttonOpenNightKSG.MouseEnter += new System.EventHandler(this.buttonOpenNightKSG_MouseEnter);
            // 
            // ImportKSGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 165);
            this.Controls.Add(this.buttonOpenNightKSG);
            this.Controls.Add(this.buttonOpenDayKSG);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNightKSGPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDayKSGPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportKSGForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Импортировать коды КГС";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDayKSGPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNightKSGPath;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOpenDayKSG;
        private System.Windows.Forms.Button buttonOpenNightKSG;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}