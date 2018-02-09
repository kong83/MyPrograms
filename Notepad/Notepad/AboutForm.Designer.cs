namespace Notepad
{
  partial class AboutForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
        this.buttonSetDefault = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.buttonInfo = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // buttonSetDefault
        // 
        this.buttonSetDefault.Location = new System.Drawing.Point(225, 7);
        this.buttonSetDefault.Name = "buttonSetDefault";
        this.buttonSetDefault.Size = new System.Drawing.Size(107, 52);
        this.buttonSetDefault.TabIndex = 0;
        this.buttonSetDefault.Text = "Сделать блокнотом по умолчанию";
        this.buttonSetDefault.UseVisualStyleBackColor = true;
        this.buttonSetDefault.MouseLeave += new System.EventHandler(this.buttonSetDefault_MouseLeave);
        this.buttonSetDefault.Click += new System.EventHandler(this.buttonSetDefault_Click);
        this.buttonSetDefault.MouseEnter += new System.EventHandler(this.buttonSetDefault_MouseEnter);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 7);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(213, 78);
        this.label1.TabIndex = 1;
        this.label1.Text = "Программа \"Блокнот\" v. 1.9.4.0\r\nНаписана на .NET C# Framework 2.0\r\nАвтор: kong83\r" +
            "\nРаспространяется свободно\r\nПожелания и предложения направляйте\r\nпо адресу: kong" +
            "83@qip.ru";
        // 
        // buttonInfo
        // 
        this.buttonInfo.Location = new System.Drawing.Point(225, 65);
        this.buttonInfo.Name = "buttonInfo";
        this.buttonInfo.Size = new System.Drawing.Size(107, 35);
        this.buttonInfo.TabIndex = 2;
        this.buttonInfo.Text = "Информация для \r\nWindows 7 и Vista";
        this.buttonInfo.UseVisualStyleBackColor = true;
        this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
        // 
        // AboutForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(339, 107);
        this.Controls.Add(this.buttonInfo);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.buttonSetDefault);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "AboutForm";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "О программе";
        this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.AboutForm_InputLanguageChanged);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonSetDefault;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Button buttonInfo;
  }
}