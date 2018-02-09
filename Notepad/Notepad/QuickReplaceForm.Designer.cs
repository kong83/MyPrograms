namespace Notepad {
  partial class QuickReplaceForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if(disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickReplaceForm));
        this.buttonReplace = new System.Windows.Forms.Button();
        this.comboReplace = new System.Windows.Forms.ComboBox();
        this.comboFind = new System.Windows.Forms.ComboBox();
        this.labelReplace = new System.Windows.Forms.Label();
        this.labelFind = new System.Windows.Forms.Label();
        this.buttonTopMostOff = new System.Windows.Forms.Button();
        this.buttonTopMostOn = new System.Windows.Forms.Button();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
        this.groupBoxUseRegular = new System.Windows.Forms.GroupBox();
        this.linkLabelStar = new System.Windows.Forms.LinkLabel();
        this.label4 = new System.Windows.Forms.Label();
        this.linkLabelSlash = new System.Windows.Forms.LinkLabel();
        this.label3 = new System.Windows.Forms.Label();
        this.linkLabelCaret = new System.Windows.Forms.LinkLabel();
        this.label5 = new System.Windows.Forms.Label();
        this.linkLabelAny = new System.Windows.Forms.LinkLabel();
        this.label1 = new System.Windows.Forms.Label();
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.checkUseRegular = new System.Windows.Forms.CheckBox();
        this.label2 = new System.Windows.Forms.Label();
        this.statusStrip1.SuspendLayout();
        this.groupBoxUseRegular.SuspendLayout();
        this.SuspendLayout();
        // 
        // buttonReplace
        // 
        this.buttonReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.buttonReplace.Location = new System.Drawing.Point(140, 144);
        this.buttonReplace.Name = "buttonReplace";
        this.buttonReplace.Size = new System.Drawing.Size(87, 23);
        this.buttonReplace.TabIndex = 15;
        this.buttonReplace.Text = "Заменить всё";
        this.buttonReplace.UseVisualStyleBackColor = true;
        this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
        // 
        // comboReplace
        // 
        this.comboReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.comboReplace.FormattingEnabled = true;
        this.comboReplace.Location = new System.Drawing.Point(81, 117);
        this.comboReplace.MaxDropDownItems = 20;
        this.comboReplace.Name = "comboReplace";
        this.comboReplace.Size = new System.Drawing.Size(235, 21);
        this.comboReplace.TabIndex = 1;
        this.comboReplace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboReplace_KeyPress);
        this.comboReplace.TextChanged += new System.EventHandler(this.comboReplace_TextChanged);
        // 
        // comboFind
        // 
        this.comboFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.comboFind.FormattingEnabled = true;
        this.comboFind.Location = new System.Drawing.Point(81, 91);
        this.comboFind.MaxDropDownItems = 20;
        this.comboFind.Name = "comboFind";
        this.comboFind.Size = new System.Drawing.Size(235, 21);
        this.comboFind.TabIndex = 0;
        this.comboFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboFind_KeyPress);
        this.comboFind.TextChanged += new System.EventHandler(this.comboFind_TextChanged);
        // 
        // labelReplace
        // 
        this.labelReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.labelReplace.AutoSize = true;
        this.labelReplace.Location = new System.Drawing.Point(5, 120);
        this.labelReplace.Name = "labelReplace";
        this.labelReplace.Size = new System.Drawing.Size(72, 13);
        this.labelReplace.TabIndex = 10;
        this.labelReplace.Text = "Заменить на";
        // 
        // labelFind
        // 
        this.labelFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.labelFind.AutoSize = true;
        this.labelFind.Location = new System.Drawing.Point(6, 94);
        this.labelFind.Name = "labelFind";
        this.labelFind.Size = new System.Drawing.Size(38, 13);
        this.labelFind.TabIndex = 8;
        this.labelFind.Text = "Найти";
        // 
        // buttonTopMostOff
        // 
        this.buttonTopMostOff.BackgroundImage = global::Notepad.Properties.Resources.topMostOn;
        this.buttonTopMostOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonTopMostOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.buttonTopMostOff.Location = new System.Drawing.Point(8, 145);
        this.buttonTopMostOff.Name = "buttonTopMostOff";
        this.buttonTopMostOff.Size = new System.Drawing.Size(20, 20);
        this.buttonTopMostOff.TabIndex = 10;
        this.buttonTopMostOff.UseVisualStyleBackColor = true;
        this.buttonTopMostOff.MouseLeave += new System.EventHandler(this.buttonTopMostOff_MouseLeave);
        this.buttonTopMostOff.Click += new System.EventHandler(this.buttonTopMostOff_Click);
        this.buttonTopMostOff.MouseEnter += new System.EventHandler(this.buttonTopMostOff_MouseEnter);
        // 
        // buttonTopMostOn
        // 
        this.buttonTopMostOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.buttonTopMostOn.BackgroundImage = global::Notepad.Properties.Resources.topMostOff;
        this.buttonTopMostOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonTopMostOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.buttonTopMostOn.Location = new System.Drawing.Point(8, 145);
        this.buttonTopMostOn.Name = "buttonTopMostOn";
        this.buttonTopMostOn.Size = new System.Drawing.Size(20, 20);
        this.buttonTopMostOn.TabIndex = 10;
        this.buttonTopMostOn.UseVisualStyleBackColor = true;
        this.buttonTopMostOn.MouseLeave += new System.EventHandler(this.buttonTopMostOn_MouseLeave);
        this.buttonTopMostOn.Click += new System.EventHandler(this.buttonTopMostOn_Click);
        this.buttonTopMostOn.MouseEnter += new System.EventHandler(this.buttonTopMostOn_MouseEnter);
        // 
        // statusStrip1
        // 
        this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
        this.statusStrip1.Location = new System.Drawing.Point(0, 173);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new System.Drawing.Size(323, 22);
        this.statusStrip1.TabIndex = 14;
        // 
        // toolStripStatusLabel1
        // 
        this.toolStripStatusLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
        this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
        // 
        // groupBoxUseRegular
        // 
        this.groupBoxUseRegular.Controls.Add(this.linkLabelStar);
        this.groupBoxUseRegular.Controls.Add(this.label4);
        this.groupBoxUseRegular.Controls.Add(this.linkLabelSlash);
        this.groupBoxUseRegular.Controls.Add(this.label3);
        this.groupBoxUseRegular.Controls.Add(this.linkLabelCaret);
        this.groupBoxUseRegular.Controls.Add(this.label5);
        this.groupBoxUseRegular.Controls.Add(this.linkLabelAny);
        this.groupBoxUseRegular.Controls.Add(this.label1);
        this.groupBoxUseRegular.Location = new System.Drawing.Point(8, 5);
        this.groupBoxUseRegular.Name = "groupBoxUseRegular";
        this.groupBoxUseRegular.Size = new System.Drawing.Size(308, 80);
        this.groupBoxUseRegular.TabIndex = 30;
        this.groupBoxUseRegular.TabStop = false;
        this.groupBoxUseRegular.Text = "      Использовать регулярные выражения";
        // 
        // linkLabelStar
        // 
        this.linkLabelStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.linkLabelStar.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
        this.linkLabelStar.Location = new System.Drawing.Point(6, 44);
        this.linkLabelStar.Name = "linkLabelStar";
        this.linkLabelStar.Size = new System.Drawing.Size(19, 16);
        this.linkLabelStar.TabIndex = 3;
        this.linkLabelStar.TabStop = true;
        this.linkLabelStar.Text = "\\*";
        this.linkLabelStar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.linkLabelStar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(22, 45);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(92, 13);
        this.label4.TabIndex = 12;
        this.label4.Text = "Просто символ *";
        // 
        // linkLabelSlash
        // 
        this.linkLabelSlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.linkLabelSlash.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
        this.linkLabelSlash.Location = new System.Drawing.Point(6, 58);
        this.linkLabelSlash.Name = "linkLabelSlash";
        this.linkLabelSlash.Size = new System.Drawing.Size(19, 16);
        this.linkLabelSlash.TabIndex = 4;
        this.linkLabelSlash.TabStop = true;
        this.linkLabelSlash.Text = "\\\\";
        this.linkLabelSlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.linkLabelSlash.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(22, 60);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(87, 13);
        this.label3.TabIndex = 10;
        this.label3.Text = "Обратный слэш";
        // 
        // linkLabelCaret
        // 
        this.linkLabelCaret.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.linkLabelCaret.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
        this.linkLabelCaret.Location = new System.Drawing.Point(6, 28);
        this.linkLabelCaret.Name = "linkLabelCaret";
        this.linkLabelCaret.Size = new System.Drawing.Size(19, 16);
        this.linkLabelCaret.TabIndex = 2;
        this.linkLabelCaret.TabStop = true;
        this.linkLabelCaret.Text = "\\n";
        this.linkLabelCaret.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.linkLabelCaret.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(22, 30);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(88, 13);
        this.label5.TabIndex = 8;
        this.label5.Text = "Переход строки";
        // 
        // linkLabelAny
        // 
        this.linkLabelAny.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
        this.linkLabelAny.Location = new System.Drawing.Point(6, 18);
        this.linkLabelAny.Name = "linkLabelAny";
        this.linkLabelAny.Size = new System.Drawing.Size(17, 10);
        this.linkLabelAny.TabIndex = 1;
        this.linkLabelAny.TabStop = true;
        this.linkLabelAny.Text = " * ";
        this.linkLabelAny.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.linkLabelAny.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(22, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(272, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Любое количество символов (возможно, ни одного)";
        // 
        // checkUseRegular
        // 
        this.checkUseRegular.AutoSize = true;
        this.checkUseRegular.Checked = true;
        this.checkUseRegular.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkUseRegular.Location = new System.Drawing.Point(19, 5);
        this.checkUseRegular.Name = "checkUseRegular";
        this.checkUseRegular.Size = new System.Drawing.Size(15, 14);
        this.checkUseRegular.TabIndex = 20;
        this.checkUseRegular.UseVisualStyleBackColor = true;
        this.checkUseRegular.CheckedChanged += new System.EventHandler(this.checkUseRegular_CheckedChanged);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(34, 5);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(204, 13);
        this.label2.TabIndex = 17;
        this.label2.Text = "Использовать регулярные выражения";
        // 
        // QuickReplaceForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(323, 195);
        this.Controls.Add(this.checkUseRegular);
        this.Controls.Add(this.groupBoxUseRegular);
        this.Controls.Add(this.statusStrip1);
        this.Controls.Add(this.buttonTopMostOn);
        this.Controls.Add(this.buttonTopMostOff);
        this.Controls.Add(this.comboReplace);
        this.Controls.Add(this.comboFind);
        this.Controls.Add(this.labelReplace);
        this.Controls.Add(this.buttonReplace);
        this.Controls.Add(this.labelFind);
        this.Controls.Add(this.label2);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "QuickReplaceForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Быстрая замена";
        this.Load += new System.EventHandler(this.QuickReplaceForm_Load);
        this.LocationChanged += new System.EventHandler(this.QuickReplaceForm_LocationChanged);
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.groupBoxUseRegular.ResumeLayout(false);
        this.groupBoxUseRegular.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonReplace;
    private System.Windows.Forms.ComboBox comboReplace;
    private System.Windows.Forms.ComboBox comboFind;
    private System.Windows.Forms.Label labelReplace;
    private System.Windows.Forms.Label labelFind;
    private System.Windows.Forms.Button buttonTopMostOff;
    private System.Windows.Forms.Button buttonTopMostOn;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    private System.Windows.Forms.GroupBox groupBoxUseRegular;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.LinkLabel linkLabelAny;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.LinkLabel linkLabelCaret;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox checkUseRegular;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.LinkLabel linkLabelStar;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.LinkLabel linkLabelSlash;
    private System.Windows.Forms.Label label3;
  }
}