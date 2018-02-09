namespace Notepad
{
    partial class GuidForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuidForm));
            this.textBoxGuid = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelGuid = new System.Windows.Forms.Label();
            this.labelInvolvedGuid = new System.Windows.Forms.Label();
            this.textBoxInvolvedGuid = new System.Windows.Forms.TextBox();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.textBoxValueNames = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonGotoReplaceForm = new System.Windows.Forms.Button();
            this.buttonConvertBack = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxGuid
            // 
            this.textBoxGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGuid.Location = new System.Drawing.Point(101, 6);
            this.textBoxGuid.Name = "textBoxGuid";
            this.textBoxGuid.Size = new System.Drawing.Size(450, 20);
            this.textBoxGuid.TabIndex = 0;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.Location = new System.Drawing.Point(257, 203);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(84, 34);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            // 
            // labelGuid
            // 
            this.labelGuid.AutoSize = true;
            this.labelGuid.Location = new System.Drawing.Point(12, 9);
            this.labelGuid.Name = "labelGuid";
            this.labelGuid.Size = new System.Drawing.Size(79, 13);
            this.labelGuid.TabIndex = 3;
            this.labelGuid.Text = "Обычный гуид";
            // 
            // labelInvolvedGuid
            // 
            this.labelInvolvedGuid.AutoSize = true;
            this.labelInvolvedGuid.Location = new System.Drawing.Point(12, 76);
            this.labelInvolvedGuid.Name = "labelInvolvedGuid";
            this.labelInvolvedGuid.Size = new System.Drawing.Size(80, 13);
            this.labelInvolvedGuid.TabIndex = 4;
            this.labelInvolvedGuid.Text = "\"Хитрый\" гуид";
            // 
            // textBoxInvolvedGuid
            // 
            this.textBoxInvolvedGuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInvolvedGuid.Location = new System.Drawing.Point(101, 73);
            this.textBoxInvolvedGuid.Name = "textBoxInvolvedGuid";
            this.textBoxInvolvedGuid.Size = new System.Drawing.Size(450, 20);
            this.textBoxInvolvedGuid.TabIndex = 5;
            // 
            // buttonCheck
            // 
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(15, 203);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(84, 34);
            this.buttonCheck.TabIndex = 6;
            this.buttonCheck.Text = "Проверить";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.MouseLeave += new System.EventHandler(this.buttonCheck_MouseLeave);
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            this.buttonCheck.MouseEnter += new System.EventHandler(this.buttonCheck_MouseEnter);
            // 
            // textBoxValueNames
            // 
            this.textBoxValueNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxValueNames.Location = new System.Drawing.Point(15, 107);
            this.textBoxValueNames.MaxLength = 2000000;
            this.textBoxValueNames.Multiline = true;
            this.textBoxValueNames.Name = "textBoxValueNames";
            this.textBoxValueNames.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxValueNames.Size = new System.Drawing.Size(536, 86);
            this.textBoxValueNames.TabIndex = 8;
            this.textBoxValueNames.WordWrap = false;
            // 
            // buttonGotoReplaceForm
            // 
            this.buttonGotoReplaceForm.BackgroundImage = global::Notepad.Properties.Resources._goto32;
            this.buttonGotoReplaceForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonGotoReplaceForm.Location = new System.Drawing.Point(511, 32);
            this.buttonGotoReplaceForm.Name = "buttonGotoReplaceForm";
            this.buttonGotoReplaceForm.Size = new System.Drawing.Size(40, 35);
            this.buttonGotoReplaceForm.TabIndex = 28;
            this.buttonGotoReplaceForm.UseVisualStyleBackColor = true;
            this.buttonGotoReplaceForm.MouseLeave += new System.EventHandler(this.buttonReplaceGuid_MouseLeave);
            this.buttonGotoReplaceForm.Click += new System.EventHandler(this.buttonGotoReplaceForm_Click);
            this.buttonGotoReplaceForm.MouseEnter += new System.EventHandler(this.buttonReplaceGuid_MouseEnter);
            // 
            // buttonConvertBack
            // 
            this.buttonConvertBack.BackgroundImage = global::Notepad.Properties.Resources.up;
            this.buttonConvertBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonConvertBack.Location = new System.Drawing.Point(257, 32);
            this.buttonConvertBack.Name = "buttonConvertBack";
            this.buttonConvertBack.Size = new System.Drawing.Size(35, 35);
            this.buttonConvertBack.TabIndex = 9;
            this.buttonConvertBack.UseVisualStyleBackColor = true;
            this.buttonConvertBack.MouseLeave += new System.EventHandler(this.buttonConvertBack_MouseLeave);
            this.buttonConvertBack.Click += new System.EventHandler(this.buttonConvertBack_Click);
            this.buttonConvertBack.MouseEnter += new System.EventHandler(this.buttonConvertBack_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::Notepad.Properties.Resources.cansel24;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Location = new System.Drawing.Point(516, 202);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonConvert
            // 
            this.buttonConvert.BackgroundImage = global::Notepad.Properties.Resources.down;
            this.buttonConvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonConvert.Location = new System.Drawing.Point(306, 32);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(35, 35);
            this.buttonConvert.TabIndex = 1;
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.MouseLeave += new System.EventHandler(this.buttonConvert_MouseLeave);
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            this.buttonConvert.MouseEnter += new System.EventHandler(this.buttonConvert_MouseEnter);
            // 
            // GuidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 244);
            this.Controls.Add(this.buttonGotoReplaceForm);
            this.Controls.Add(this.buttonConvertBack);
            this.Controls.Add(this.textBoxValueNames);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.textBoxInvolvedGuid);
            this.Controls.Add(this.labelInvolvedGuid);
            this.Controls.Add(this.labelGuid);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.textBoxGuid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GuidForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Поиск хитрых гуидов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGuid;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelGuid;
        private System.Windows.Forms.Label labelInvolvedGuid;
        private System.Windows.Forms.TextBox textBoxInvolvedGuid;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxValueNames;
        private System.Windows.Forms.Button buttonConvertBack;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonGotoReplaceForm;
    }
}