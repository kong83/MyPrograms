﻿namespace SurgeryHelper.Forms
{
    partial class SurgeonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SurgeonForm));
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkedListBoxSurgeons = new System.Windows.Forms.CheckedListBox();
            this.timerRemoveMarks = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.BackgroundImage = global::SurgeryHelper.Properties.Resources.edit;
            this.buttonEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonEdit.FlatAppearance.BorderSize = 0;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Location = new System.Drawing.Point(104, 230);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(40, 40);
            this.buttonEdit.TabIndex = 68;
            this.buttonEdit.TabStop = false;
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.MouseLeave += new System.EventHandler(this.buttonEdit_MouseLeave);
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            this.buttonEdit.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonEdit.MouseEnter += new System.EventHandler(this.buttonEdit_MouseEnter);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.BackgroundImage = global::SurgeryHelper.Properties.Resources.delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(58, 230);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 67;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackgroundImage = global::SurgeryHelper.Properties.Resources.add;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(12, 230);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(40, 40);
            this.buttonAdd.TabIndex = 66;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(186, 230);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 64;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // checkedListBoxSurgeons
            // 
            this.checkedListBoxSurgeons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxSurgeons.CheckOnClick = true;
            this.checkedListBoxSurgeons.FormattingEnabled = true;
            this.checkedListBoxSurgeons.HorizontalScrollbar = true;
            this.checkedListBoxSurgeons.IntegralHeight = false;
            this.checkedListBoxSurgeons.Items.AddRange(new object[] {
            "checkedListBoxSurgeons"});
            this.checkedListBoxSurgeons.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxSurgeons.Name = "checkedListBoxSurgeons";
            this.checkedListBoxSurgeons.Size = new System.Drawing.Size(214, 212);
            this.checkedListBoxSurgeons.TabIndex = 70;
            this.checkedListBoxSurgeons.ThreeDCheckBoxes = true;
            this.checkedListBoxSurgeons.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxSurgeons_ItemCheck_1);
            // 
            // timerRemoveMarks
            // 
            this.timerRemoveMarks.Interval = 1;
            this.timerRemoveMarks.Tick += new System.EventHandler(this.timerRemoveMarks_Tick);
            // 
            // SurgeonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 282);
            this.Controls.Add(this.checkedListBoxSurgeons);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(220, 220);
            this.Name = "SurgeonForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список хирургов";
            this.Load += new System.EventHandler(this.SurgeonForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckedListBox checkedListBoxSurgeons;
        private System.Windows.Forms.Timer timerRemoveMarks;
    }
}