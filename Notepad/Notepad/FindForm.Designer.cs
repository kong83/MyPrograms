namespace Notepad
{
  partial class FindForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
      this.labelFind = new System.Windows.Forms.Label();
      this.buttonFindNext = new System.Windows.Forms.Button();
      this.buttonReplace = new System.Windows.Forms.Button();
      this.buttonReplaceAll = new System.Windows.Forms.Button();
      this.labelReplace = new System.Windows.Forms.Label();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.checkBoxRegistr = new System.Windows.Forms.CheckBox();
      this.checkBoxAllWord = new System.Windows.Forms.CheckBox();
      this.checkBoxFindUp = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.comboFind = new System.Windows.Forms.ComboBox();
      this.comboReplace = new System.Windows.Forms.ComboBox();
      this.buttonTopMostOff = new System.Windows.Forms.Button();
      this.buttonTopMostOn = new System.Windows.Forms.Button();
      this.buttonChangePlus = new System.Windows.Forms.Button();
      this.buttonChangeMinus = new System.Windows.Forms.Button();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.statusStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // labelFind
      // 
      this.labelFind.AutoSize = true;
      this.labelFind.Location = new System.Drawing.Point(3, 92);
      this.labelFind.Name = "labelFind";
      this.labelFind.Size = new System.Drawing.Size(38, 13);
      this.labelFind.TabIndex = 1;
      this.labelFind.Text = "Найти";
      // 
      // buttonFindNext
      // 
      this.buttonFindNext.Location = new System.Drawing.Point(220, 8);
      this.buttonFindNext.Name = "buttonFindNext";
      this.buttonFindNext.Size = new System.Drawing.Size(93, 23);
      this.buttonFindNext.TabIndex = 2;
      this.buttonFindNext.Text = "Найти дальше";
      this.buttonFindNext.UseVisualStyleBackColor = true;
      this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
      // 
      // buttonReplace
      // 
      this.buttonReplace.Location = new System.Drawing.Point(221, 35);
      this.buttonReplace.Name = "buttonReplace";
      this.buttonReplace.Size = new System.Drawing.Size(92, 23);
      this.buttonReplace.TabIndex = 3;
      this.buttonReplace.Text = "Заменить";
      this.buttonReplace.UseVisualStyleBackColor = true;
      this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
      // 
      // buttonReplaceAll
      // 
      this.buttonReplaceAll.Location = new System.Drawing.Point(221, 62);
      this.buttonReplaceAll.Name = "buttonReplaceAll";
      this.buttonReplaceAll.Size = new System.Drawing.Size(92, 23);
      this.buttonReplaceAll.TabIndex = 4;
      this.buttonReplaceAll.Text = "Заменить всё";
      this.buttonReplaceAll.UseVisualStyleBackColor = true;
      this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
      // 
      // labelReplace
      // 
      this.labelReplace.AutoSize = true;
      this.labelReplace.Location = new System.Drawing.Point(2, 118);
      this.labelReplace.Name = "labelReplace";
      this.labelReplace.Size = new System.Drawing.Size(72, 13);
      this.labelReplace.TabIndex = 6;
      this.labelReplace.Text = "Заменить на";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 143);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(319, 22);
      this.statusStrip1.TabIndex = 9;
      // 
      // toolStripStatusLabel1
      // 
      this.toolStripStatusLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
      // 
      // checkBoxRegistr
      // 
      this.checkBoxRegistr.AutoSize = true;
      this.checkBoxRegistr.Location = new System.Drawing.Point(8, 17);
      this.checkBoxRegistr.Name = "checkBoxRegistr";
      this.checkBoxRegistr.Size = new System.Drawing.Size(119, 17);
      this.checkBoxRegistr.TabIndex = 0;
      this.checkBoxRegistr.Text = "с учётом регистра";
      this.checkBoxRegistr.UseVisualStyleBackColor = true;
      this.checkBoxRegistr.CheckedChanged += new System.EventHandler(this.checkBoxRegistr_CheckedChanged);
      // 
      // checkBoxAllWord
      // 
      this.checkBoxAllWord.AutoSize = true;
      this.checkBoxAllWord.Location = new System.Drawing.Point(8, 37);
      this.checkBoxAllWord.Name = "checkBoxAllWord";
      this.checkBoxAllWord.Size = new System.Drawing.Size(103, 17);
      this.checkBoxAllWord.TabIndex = 1;
      this.checkBoxAllWord.Text = "слово целиком";
      this.checkBoxAllWord.UseVisualStyleBackColor = true;
      this.checkBoxAllWord.CheckedChanged += new System.EventHandler(this.checkBoxAllWord_CheckedChanged);
      // 
      // checkBoxFindUp
      // 
      this.checkBoxFindUp.AutoSize = true;
      this.checkBoxFindUp.Location = new System.Drawing.Point(8, 57);
      this.checkBoxFindUp.Name = "checkBoxFindUp";
      this.checkBoxFindUp.Size = new System.Drawing.Size(93, 17);
      this.checkBoxFindUp.TabIndex = 2;
      this.checkBoxFindUp.Text = "искать вверх";
      this.checkBoxFindUp.UseVisualStyleBackColor = true;
      this.checkBoxFindUp.CheckedChanged += new System.EventHandler(this.checkBoxFindUp_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.checkBoxRegistr);
      this.groupBox1.Controls.Add(this.checkBoxFindUp);
      this.groupBox1.Controls.Add(this.checkBoxAllWord);
      this.groupBox1.Location = new System.Drawing.Point(36, 4);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(179, 81);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Опции поиска";
      // 
      // comboFind
      // 
      this.comboFind.FormattingEnabled = true;
      this.comboFind.Location = new System.Drawing.Point(78, 89);
      this.comboFind.MaxDropDownItems = 20;
      this.comboFind.Name = "comboFind";
      this.comboFind.Size = new System.Drawing.Size(235, 21);
      this.comboFind.TabIndex = 0;
      this.comboFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboFind_KeyPress);
      this.comboFind.TextChanged += new System.EventHandler(this.textFind_TextChanged);
      // 
      // comboReplace
      // 
      this.comboReplace.FormattingEnabled = true;
      this.comboReplace.Location = new System.Drawing.Point(78, 115);
      this.comboReplace.MaxDropDownItems = 20;
      this.comboReplace.Name = "comboReplace";
      this.comboReplace.Size = new System.Drawing.Size(235, 21);
      this.comboReplace.TabIndex = 1;
      this.comboReplace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboReplace_KeyPress);
      this.comboReplace.TextChanged += new System.EventHandler(this.textReplace_TextChanged);
      // 
      // buttonTopMostOff
      // 
      this.buttonTopMostOff.BackgroundImage = global::Notepad.Properties.Resources.topMostOn;
      this.buttonTopMostOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.buttonTopMostOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonTopMostOff.Location = new System.Drawing.Point(6, 9);
      this.buttonTopMostOff.Name = "buttonTopMostOff";
      this.buttonTopMostOff.Size = new System.Drawing.Size(20, 20);
      this.buttonTopMostOff.TabIndex = 11;
      this.buttonTopMostOff.UseVisualStyleBackColor = true;
      this.buttonTopMostOff.MouseLeave += new System.EventHandler(this.buttonTopMostOff_MouseLeave);
      this.buttonTopMostOff.Click += new System.EventHandler(this.buttonTopMostOff_Click);
      this.buttonTopMostOff.MouseEnter += new System.EventHandler(this.buttonTopMostOff_MouseEnter);
      // 
      // buttonTopMostOn
      // 
      this.buttonTopMostOn.BackgroundImage = global::Notepad.Properties.Resources.topMostOff;
      this.buttonTopMostOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.buttonTopMostOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonTopMostOn.Location = new System.Drawing.Point(6, 9);
      this.buttonTopMostOn.Name = "buttonTopMostOn";
      this.buttonTopMostOn.Size = new System.Drawing.Size(20, 20);
      this.buttonTopMostOn.TabIndex = 10;
      this.buttonTopMostOn.UseVisualStyleBackColor = true;
      this.buttonTopMostOn.MouseLeave += new System.EventHandler(this.buttonTopMostOn_MouseLeave);
      this.buttonTopMostOn.Click += new System.EventHandler(this.buttonTopMostOn_Click);
      this.buttonTopMostOn.MouseEnter += new System.EventHandler(this.buttonTopMostOn_MouseEnter);
      // 
      // buttonChangePlus
      // 
      this.buttonChangePlus.BackgroundImage = global::Notepad.Properties.Resources.plus16;
      this.buttonChangePlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.buttonChangePlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonChangePlus.Location = new System.Drawing.Point(5, 60);
      this.buttonChangePlus.Name = "buttonChangePlus";
      this.buttonChangePlus.Size = new System.Drawing.Size(23, 23);
      this.buttonChangePlus.TabIndex = 5;
      this.buttonChangePlus.UseVisualStyleBackColor = true;
      this.buttonChangePlus.MouseLeave += new System.EventHandler(this.buttonChangePlus_MouseLeave);
      this.buttonChangePlus.Click += new System.EventHandler(this.buttonChangePlus_Click);
      this.buttonChangePlus.MouseEnter += new System.EventHandler(this.buttonChangePlus_MouseEnter);
      // 
      // buttonChangeMinus
      // 
      this.buttonChangeMinus.BackgroundImage = global::Notepad.Properties.Resources.minus16;
      this.buttonChangeMinus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.buttonChangeMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.buttonChangeMinus.Location = new System.Drawing.Point(5, 60);
      this.buttonChangeMinus.Name = "buttonChangeMinus";
      this.buttonChangeMinus.Size = new System.Drawing.Size(23, 23);
      this.buttonChangeMinus.TabIndex = 12;
      this.buttonChangeMinus.UseVisualStyleBackColor = true;
      this.buttonChangeMinus.MouseLeave += new System.EventHandler(this.buttonChangeMinus_MouseLeave);
      this.buttonChangeMinus.Click += new System.EventHandler(this.buttonChangeMinus_Click);
      this.buttonChangeMinus.MouseEnter += new System.EventHandler(this.buttonChangeMinus_MouseEnter);
      // 
      // FindForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(319, 165);
      this.Controls.Add(this.buttonChangeMinus);
      this.Controls.Add(this.buttonTopMostOff);
      this.Controls.Add(this.buttonTopMostOn);
      this.Controls.Add(this.comboReplace);
      this.Controls.Add(this.comboFind);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.buttonChangePlus);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.labelReplace);
      this.Controls.Add(this.buttonReplaceAll);
      this.Controls.Add(this.buttonReplace);
      this.Controls.Add(this.buttonFindNext);
      this.Controls.Add(this.labelFind);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FindForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Замена";
      this.Load += new System.EventHandler(this.FindForm_Load);
      this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.FindForm_InputLanguageChanged);
      this.LocationChanged += new System.EventHandler(this.FindForm_LocationChanged);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelFind;
    private System.Windows.Forms.Button buttonFindNext;
    private System.Windows.Forms.Button buttonReplace;
    private System.Windows.Forms.Button buttonReplaceAll;
    private System.Windows.Forms.Label labelReplace;
    private System.Windows.Forms.Button buttonChangePlus;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.CheckBox checkBoxRegistr;
    private System.Windows.Forms.CheckBox checkBoxAllWord;
    private System.Windows.Forms.CheckBox checkBoxFindUp;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ComboBox comboFind;
    private System.Windows.Forms.ComboBox comboReplace;
    private System.Windows.Forms.Button buttonTopMostOn;
    private System.Windows.Forms.Button buttonTopMostOff;
    private System.Windows.Forms.Button buttonChangeMinus;
    private System.Windows.Forms.ToolTip toolTip;
  }
}