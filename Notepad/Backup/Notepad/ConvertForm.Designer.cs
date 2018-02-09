namespace Notepad
{
  partial class ConvertForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertForm));
        this.textCurrent = new System.Windows.Forms.TextBox();
        this.textResult = new System.Windows.Forms.TextBox();
        this.comboType = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.comboFrom = new System.Windows.Forms.ComboBox();
        this.comboTo = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.buttonSaveShifr = new System.Windows.Forms.Button();
        this.buttonConvert = new System.Windows.Forms.Button();
        this.buttonSaveAndClose = new System.Windows.Forms.Button();
        this.pictureBoxFrom = new System.Windows.Forms.PictureBox();
        this.pictureBoxTo = new System.Windows.Forms.PictureBox();
        this.buttonOpenShifr = new System.Windows.Forms.Button();
        this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
        this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrom)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTo)).BeginInit();
        this.SuspendLayout();
        // 
        // textCurrent
        // 
        this.textCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textCurrent.HideSelection = false;
        this.textCurrent.Location = new System.Drawing.Point(0, 0);
        this.textCurrent.Multiline = true;
        this.textCurrent.Name = "textCurrent";
        this.textCurrent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.textCurrent.Size = new System.Drawing.Size(611, 254);
        this.textCurrent.TabIndex = 1;
        this.textCurrent.WordWrap = false;
        // 
        // textResult
        // 
        this.textResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textResult.BackColor = System.Drawing.SystemColors.Window;
        this.textResult.HideSelection = false;
        this.textResult.Location = new System.Drawing.Point(0, 330);
        this.textResult.Multiline = true;
        this.textResult.Name = "textResult";
        this.textResult.ReadOnly = true;
        this.textResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.textResult.Size = new System.Drawing.Size(611, 254);
        this.textResult.TabIndex = 12;
        this.textResult.WordWrap = false;
        // 
        // ComboType
        // 
        this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboType.FormattingEnabled = true;
        this.comboType.Items.AddRange(new object[] {
            "Кодировка",
            "Раскладка",
            "Шифрование"});
        this.comboType.Location = new System.Drawing.Point(12, 288);
        this.comboType.Name = "comboType";
        this.comboType.Size = new System.Drawing.Size(135, 21);
        this.comboType.TabIndex = 3;
        this.comboType.SelectedIndexChanged += new System.EventHandler(this.comboType_SelectedIndexChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.label1.Location = new System.Drawing.Point(13, 271);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(131, 13);
        this.label1.TabIndex = 4;
        this.label1.Text = "Тип преобразования";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.label2.Location = new System.Drawing.Point(215, 270);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(21, 13);
        this.label2.TabIndex = 5;
        this.label2.Text = "из";
        // 
        // ComboFrom
        // 
        this.comboFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboFrom.FormattingEnabled = true;
        this.comboFrom.Location = new System.Drawing.Point(238, 267);
        this.comboFrom.Name = "comboFrom";
        this.comboFrom.Size = new System.Drawing.Size(169, 21);
        this.comboFrom.TabIndex = 6;
        this.comboFrom.SelectedIndexChanged += new System.EventHandler(this.comboFrom_SelectedIndexChanged);
        this.comboFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboFromTo_KeyPress);
        // 
        // ComboTo
        // 
        this.comboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboTo.FormattingEnabled = true;
        this.comboTo.Location = new System.Drawing.Point(238, 298);
        this.comboTo.Name = "comboTo";
        this.comboTo.Size = new System.Drawing.Size(169, 21);
        this.comboTo.TabIndex = 7;
        this.comboTo.SelectedIndexChanged += new System.EventHandler(this.comboTo_SelectedIndexChanged);
        this.comboTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboFromTo_KeyPress);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.label3.Location = new System.Drawing.Point(218, 301);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(14, 13);
        this.label3.TabIndex = 8;
        this.label3.Text = "в";
        // 
        // buttonSaveShifr
        // 
        this.buttonSaveShifr.Image = global::Notepad.Properties.Resources.saveShifr32;
        this.buttonSaveShifr.Location = new System.Drawing.Point(155, 274);
        this.buttonSaveShifr.Name = "buttonSaveShifr";
        this.buttonSaveShifr.Size = new System.Drawing.Size(35, 35);
        this.buttonSaveShifr.TabIndex = 4;
        this.buttonSaveShifr.UseVisualStyleBackColor = true;
        this.buttonSaveShifr.Visible = false;
        this.buttonSaveShifr.MouseLeave += new System.EventHandler(this.buttonSaveShifr_MouseLeave);
        this.buttonSaveShifr.Click += new System.EventHandler(this.buttonSaveShifr_Click);
        this.buttonSaveShifr.MouseEnter += new System.EventHandler(this.buttonSaveShifr_MouseEnter);
        // 
        // buttonConvert
        // 
        this.buttonConvert.Image = global::Notepad.Properties.Resources.down;
        this.buttonConvert.Location = new System.Drawing.Point(413, 266);
        this.buttonConvert.Name = "buttonConvert";
        this.buttonConvert.Size = new System.Drawing.Size(35, 53);
        this.buttonConvert.TabIndex = 9;
        this.buttonConvert.UseVisualStyleBackColor = true;
        this.buttonConvert.MouseLeave += new System.EventHandler(this.buttonConvert_MouseLeave);
        this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
        this.buttonConvert.MouseEnter += new System.EventHandler(this.buttonConvert_MouseEnter);
        // 
        // buttonSaveAndClose
        // 
        this.buttonSaveAndClose.Image = global::Notepad.Properties.Resources.copyConvert48;
        this.buttonSaveAndClose.Location = new System.Drawing.Point(550, 266);
        this.buttonSaveAndClose.Name = "buttonSaveAndClose";
        this.buttonSaveAndClose.Size = new System.Drawing.Size(48, 53);
        this.buttonSaveAndClose.TabIndex = 10;
        this.buttonSaveAndClose.UseVisualStyleBackColor = true;
        this.buttonSaveAndClose.MouseLeave += new System.EventHandler(this.buttonSaveAndClose_MouseLeave);
        this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
        this.buttonSaveAndClose.MouseEnter += new System.EventHandler(this.buttonSaveAndClose_MouseEnter);
        // 
        // pictureBoxFrom
        // 
        this.pictureBoxFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBoxFrom.Location = new System.Drawing.Point(0, 0);
        this.pictureBoxFrom.Name = "pictureBoxFrom";
        this.pictureBoxFrom.Size = new System.Drawing.Size(610, 253);
        this.pictureBoxFrom.TabIndex = 14;
        this.pictureBoxFrom.TabStop = false;
        this.pictureBoxFrom.Visible = false;
        // 
        // pictureBoxTo
        // 
        this.pictureBoxTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBoxTo.Location = new System.Drawing.Point(0, 330);
        this.pictureBoxTo.Name = "pictureBoxTo";
        this.pictureBoxTo.Size = new System.Drawing.Size(610, 250);
        this.pictureBoxTo.TabIndex = 13;
        this.pictureBoxTo.TabStop = false;
        this.pictureBoxTo.Visible = false;
        // 
        // buttonOpenShifr
        // 
        this.buttonOpenShifr.Image = global::Notepad.Properties.Resources.openShifr32;
        this.buttonOpenShifr.Location = new System.Drawing.Point(155, 274);
        this.buttonOpenShifr.Name = "buttonOpenShifr";
        this.buttonOpenShifr.Size = new System.Drawing.Size(35, 35);
        this.buttonOpenShifr.TabIndex = 4;
        this.buttonOpenShifr.UseVisualStyleBackColor = true;
        this.buttonOpenShifr.Visible = false;
        this.buttonOpenShifr.MouseLeave += new System.EventHandler(this.buttonOpenShifr_MouseLeave);
        this.buttonOpenShifr.Click += new System.EventHandler(this.buttonOpenShifr_Click);
        this.buttonOpenShifr.MouseEnter += new System.EventHandler(this.buttonOpenShifr_MouseEnter);
        // 
        // openFileDialog
        // 
        this.openFileDialog.Filter = "Шифрованные файлы|*.bmb";
        // 
        // saveFileDialog
        // 
        this.saveFileDialog.Filter = "Шифрованные файлы|*.bmb";
        // 
        // ConvertForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(610, 580);
        this.Controls.Add(this.buttonOpenShifr);
        this.Controls.Add(this.buttonSaveShifr);
        this.Controls.Add(this.buttonConvert);
        this.Controls.Add(this.buttonSaveAndClose);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.comboTo);
        this.Controls.Add(this.comboFrom);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.comboType);
        this.Controls.Add(this.pictureBoxFrom);
        this.Controls.Add(this.pictureBoxTo);
        this.Controls.Add(this.textResult);
        this.Controls.Add(this.textCurrent);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ConvertForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Форма преобразований";
        this.Load += new System.EventHandler(this.ConvertForm_Load);
        this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.ConvertForm_InputLanguageChanged);
        this.LocationChanged += new System.EventHandler(this.ConvertForm_LocationChanged);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrom)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTo)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.TextBox textCurrent;
    public System.Windows.Forms.TextBox textResult;
    private System.Windows.Forms.ComboBox comboType;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox comboFrom;
    private System.Windows.Forms.ComboBox comboTo;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button buttonSaveAndClose;
    private System.Windows.Forms.Button buttonConvert;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.PictureBox pictureBoxTo;
    private System.Windows.Forms.PictureBox pictureBoxFrom;
    private System.Windows.Forms.Button buttonSaveShifr;
    private System.Windows.Forms.Button buttonOpenShifr;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
  }
}