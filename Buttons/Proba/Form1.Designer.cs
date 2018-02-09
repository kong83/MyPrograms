namespace Proba
{
    partial class Form1
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
          this.button1 = new System.Windows.Forms.Button();
          this.button2 = new System.Windows.Forms.Button();
          this.button3 = new System.Windows.Forms.Button();
          this.button4 = new System.Windows.Forms.Button();
          this.button5 = new System.Windows.Forms.Button();
          this.textBox1 = new System.Windows.Forms.TextBox();
          this.timer1 = new System.Windows.Forms.Timer(this.components);
          this.menuStrip1 = new System.Windows.Forms.MenuStrip();
          this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.button6 = new System.Windows.Forms.Button();
          this.menuStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // button1
          // 
          this.button1.AccessibleDescription = null;
          this.button1.AccessibleName = null;
          resources.ApplyResources(this.button1, "button1");
          this.button1.BackColor = System.Drawing.SystemColors.Control;
          this.button1.BackgroundImage = null;
          this.button1.Name = "button1";
          this.button1.UseVisualStyleBackColor = false;          
          this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
          // 
          // button2
          // 
          this.button2.AccessibleDescription = null;
          this.button2.AccessibleName = null;
          resources.ApplyResources(this.button2, "button2");
          this.button2.BackgroundImage = null;
          this.button2.Name = "button2";
          this.button2.UseVisualStyleBackColor = true;
          this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
          // 
          // button3
          // 
          this.button3.AccessibleDescription = null;
          this.button3.AccessibleName = null;
          resources.ApplyResources(this.button3, "button3");
          this.button3.BackgroundImage = null;
          this.button3.Name = "button3";
          this.button3.UseVisualStyleBackColor = true;
          this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button3_MouseDown);
          // 
          // button4
          // 
          this.button4.AccessibleDescription = null;
          this.button4.AccessibleName = null;
          resources.ApplyResources(this.button4, "button4");
          this.button4.BackgroundImage = null;
          this.button4.Name = "button4";
          this.button4.UseVisualStyleBackColor = true;
          this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button4_MouseDown);
          // 
          // button5
          // 
          this.button5.AccessibleDescription = null;
          this.button5.AccessibleName = null;
          resources.ApplyResources(this.button5, "button5");
          this.button5.BackgroundImage = null;
          this.button5.Name = "button5";
          this.button5.UseVisualStyleBackColor = true;
          this.button5.Click += new System.EventHandler(this.button5_Click);
          // 
          // textBox1
          // 
          this.textBox1.AccessibleDescription = null;
          this.textBox1.AccessibleName = null;
          resources.ApplyResources(this.textBox1, "textBox1");
          this.textBox1.BackgroundImage = null;
          this.textBox1.Font = null;
          this.textBox1.Name = "textBox1";
          // 
          // timer1
          // 
          this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
          // 
          // menuStrip1
          // 
          this.menuStrip1.AccessibleDescription = null;
          this.menuStrip1.AccessibleName = null;
          resources.ApplyResources(this.menuStrip1, "menuStrip1");
          this.menuStrip1.BackgroundImage = null;
          this.menuStrip1.Font = null;
          this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
          this.menuStrip1.Name = "menuStrip1";
          // 
          // файлToolStripMenuItem
          // 
          this.файлToolStripMenuItem.AccessibleDescription = null;
          this.файлToolStripMenuItem.AccessibleName = null;
          resources.ApplyResources(this.файлToolStripMenuItem, "файлToolStripMenuItem");
          this.файлToolStripMenuItem.BackgroundImage = null;
          this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
          this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
          this.файлToolStripMenuItem.ShortcutKeyDisplayString = null;
          // 
          // выходToolStripMenuItem
          // 
          this.выходToolStripMenuItem.AccessibleDescription = null;
          this.выходToolStripMenuItem.AccessibleName = null;
          resources.ApplyResources(this.выходToolStripMenuItem, "выходToolStripMenuItem");
          this.выходToolStripMenuItem.BackgroundImage = null;
          this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
          this.выходToolStripMenuItem.ShortcutKeyDisplayString = null;
          this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
          // 
          // button6
          // 
          this.button6.AccessibleDescription = null;
          this.button6.AccessibleName = null;
          resources.ApplyResources(this.button6, "button6");
          this.button6.BackgroundImage = null;
          this.button6.Name = "button6";
          this.button6.UseVisualStyleBackColor = true;
          this.button6.Click += new System.EventHandler(this.button6_Click);
          // 
          // Form1
          // 
          this.AccessibleDescription = null;
          this.AccessibleName = null;
          resources.ApplyResources(this, "$this");
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackgroundImage = null;
          this.Controls.Add(this.button6);
          this.Controls.Add(this.textBox1);
          this.Controls.Add(this.button5);
          this.Controls.Add(this.button4);
          this.Controls.Add(this.button3);
          this.Controls.Add(this.button2);
          this.Controls.Add(this.button1);
          this.Controls.Add(this.menuStrip1);
          this.Font = null;
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.MainMenuStrip = this.menuStrip1;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "Form1";
          this.menuStrip1.ResumeLayout(false);
          this.menuStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
      private System.Windows.Forms.Button button6;
    }
}

