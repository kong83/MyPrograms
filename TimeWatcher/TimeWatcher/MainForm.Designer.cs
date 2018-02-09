namespace TimeWatcher
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
            this.listBoxProjectList = new System.Windows.Forms.ListBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.labelBorder = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonShowTimesForm = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxProjectList
            // 
            this.listBoxProjectList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxProjectList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxProjectList.FormattingEnabled = true;
            this.listBoxProjectList.HorizontalScrollbar = true;
            this.listBoxProjectList.IntegralHeight = false;
            this.listBoxProjectList.ItemHeight = 16;
            this.listBoxProjectList.Location = new System.Drawing.Point(78, 39);
            this.listBoxProjectList.Name = "listBoxProjectList";
            this.listBoxProjectList.Size = new System.Drawing.Size(326, 422);
            this.listBoxProjectList.TabIndex = 0;
            this.listBoxProjectList.SelectedIndexChanged += new System.EventHandler(this.listBoxProjectList_SelectedIndexChanged);
            this.listBoxProjectList.DoubleClick += new System.EventHandler(this.listBoxProjectList_DoubleClick);
            // 
            // labelCaption
            // 
            this.labelCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaption.Location = new System.Drawing.Point(78, 9);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(326, 26);
            this.labelCaption.TabIndex = 1;
            this.labelCaption.Text = "Список проектов";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBorder
            // 
            this.labelBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBorder.Location = new System.Drawing.Point(6, 39);
            this.labelBorder.Name = "labelBorder";
            this.labelBorder.Size = new System.Drawing.Size(66, 422);
            this.labelBorder.TabIndex = 39;
            this.labelBorder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(78, 464);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(326, 44);
            this.labelInfo.TabIndex = 40;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelInfo.MouseLeave += new System.EventHandler(this.labelInfo_MouseLeave);
            this.labelInfo.MouseEnter += new System.EventHandler(this.labelInfo_MouseEnter);
            // 
            // buttonShowTimesForm
            // 
            this.buttonShowTimesForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonShowTimesForm.FlatAppearance.BorderSize = 0;
            this.buttonShowTimesForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowTimesForm.Image = global::TimeWatcher.Properties.Resources.stopwatch;
            this.buttonShowTimesForm.Location = new System.Drawing.Point(19, 202);
            this.buttonShowTimesForm.Name = "buttonShowTimesForm";
            this.buttonShowTimesForm.Size = new System.Drawing.Size(40, 40);
            this.buttonShowTimesForm.TabIndex = 42;
            this.buttonShowTimesForm.TabStop = false;
            this.buttonShowTimesForm.UseVisualStyleBackColor = true;
            this.buttonShowTimesForm.MouseLeave += new System.EventHandler(this.buttonShowTimesForm_MouseLeave);
            this.buttonShowTimesForm.Click += new System.EventHandler(this.buttonShowTimesForm_Click);
            this.buttonShowTimesForm.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonShowTimesForm.MouseEnter += new System.EventHandler(this.buttonShowTimesForm_MouseEnter);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Image = global::TimeWatcher.Properties.Resources.properties;
            this.buttonSettings.Location = new System.Drawing.Point(19, 370);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(40, 40);
            this.buttonSettings.TabIndex = 41;
            this.buttonSettings.TabStop = false;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.MouseLeave += new System.EventHandler(this.buttonSettings_MouseLeave);
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            this.buttonSettings.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonSettings.MouseEnter += new System.EventHandler(this.buttonSettings_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Image = global::TimeWatcher.Properties.Resources.close;
            this.buttonClose.Location = new System.Drawing.Point(19, 416);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 38;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEdit.FlatAppearance.BorderSize = 0;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Image = global::TimeWatcher.Properties.Resources.edit;
            this.buttonEdit.Location = new System.Drawing.Point(19, 88);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(40, 40);
            this.buttonEdit.TabIndex = 37;
            this.buttonEdit.TabStop = false;
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.MouseLeave += new System.EventHandler(this.buttonEdit_MouseLeave);
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            this.buttonEdit.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonEdit.MouseEnter += new System.EventHandler(this.buttonEdit_MouseEnter);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlText;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.buttonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Image = global::TimeWatcher.Properties.Resources.add;
            this.buttonAdd.Location = new System.Drawing.Point(19, 42);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(40, 40);
            this.buttonAdd.TabIndex = 35;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Image = global::TimeWatcher.Properties.Resources.delete;
            this.buttonDelete.Location = new System.Drawing.Point(19, 134);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 36;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 509);
            this.Controls.Add(this.buttonShowTimesForm);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.listBoxProjectList);
            this.Controls.Add(this.labelBorder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(275, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Распределение времени";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.MainForm_InputLanguageChanged);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxProjectList;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelBorder;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonShowTimesForm;
    }
}

