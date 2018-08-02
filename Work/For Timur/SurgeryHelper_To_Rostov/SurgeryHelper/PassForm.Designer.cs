namespace SurgeryHelper
{
  partial class PassForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassForm));
        this.labelInfo = new System.Windows.Forms.Label();
        this.textPass = new System.Windows.Forms.TextBox();
        this.buttonOK = new System.Windows.Forms.Button();
        this.buttonClose = new System.Windows.Forms.Button();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.labelLang = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // labelInfo
        // 
        this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.labelInfo.Location = new System.Drawing.Point(12, 12);
        this.labelInfo.Name = "labelInfo";
        this.labelInfo.Size = new System.Drawing.Size(188, 13);
        this.labelInfo.TabIndex = 47;
        this.labelInfo.Text = "Введите пароль";
        this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // textPass
        // 
        this.textPass.Location = new System.Drawing.Point(12, 31);
        this.textPass.Name = "textPass";
        this.textPass.PasswordChar = '*';
        this.textPass.Size = new System.Drawing.Size(188, 20);
        this.textPass.TabIndex = 1;
        // 
        // buttonOK
        // 
        this.buttonOK.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
        this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonOK.FlatAppearance.BorderSize = 0;
        this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.buttonOK.Location = new System.Drawing.Point(48, 67);
        this.buttonOK.Name = "buttonOK";
        this.buttonOK.Size = new System.Drawing.Size(40, 40);
        this.buttonOK.TabIndex = 45;
        this.buttonOK.TabStop = false;
        this.buttonOK.UseVisualStyleBackColor = true;
        this.buttonOK.MouseLeave += new System.EventHandler(this.buttonOK_MouseLeave);
        this.buttonOK.Click += new System.EventHandler(this.OK_Click);
        this.buttonOK.MouseEnter += new System.EventHandler(this.buttonOK_MouseEnter);
        // 
        // buttonClose
        // 
        this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
        this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonClose.FlatAppearance.BorderSize = 0;
        this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.buttonClose.Location = new System.Drawing.Point(121, 67);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(40, 40);
        this.buttonClose.TabIndex = 46;
        this.buttonClose.TabStop = false;
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
        this.buttonClose.Click += new System.EventHandler(this.Cancel_Click);
        this.buttonClose.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
        // 
        // labelLang
        // 
        this.labelLang.AutoSize = true;
        this.labelLang.Location = new System.Drawing.Point(200, 35);
        this.labelLang.Name = "labelLang";
        this.labelLang.Size = new System.Drawing.Size(19, 13);
        this.labelLang.TabIndex = 48;
        this.labelLang.Text = "en";
        // 
        // PassForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(221, 121);
        this.Controls.Add(this.labelInfo);
        this.Controls.Add(this.textPass);
        this.Controls.Add(this.buttonOK);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.labelLang);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "PassForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Аутентификация";
        this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.PassForm_InputLanguageChanged);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PassForm_FormClosing);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelInfo;
    private System.Windows.Forms.TextBox textPass;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Label labelLang;
  }
}