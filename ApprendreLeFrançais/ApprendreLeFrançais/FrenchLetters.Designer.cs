namespace ApprendreLeFrançais
{
    partial class FrenchLetters
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonA = new System.Windows.Forms.Button();
            this.buttonE = new System.Windows.Forms.Button();
            this.buttonO = new System.Windows.Forms.Button();
            this.buttonI = new System.Windows.Forms.Button();
            this.buttonGrave = new System.Windows.Forms.Button();
            this.buttonC = new System.Windows.Forms.Button();
            this.buttonU = new System.Windows.Forms.Button();
            this.buttonTrema = new System.Windows.Forms.Button();
            this.buttonCirconflexe = new System.Windows.Forms.Button();
            this.buttonAigu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonA
            // 
            this.buttonA.Location = new System.Drawing.Point(0, 27);
            this.buttonA.Name = "buttonA";
            this.buttonA.Size = new System.Drawing.Size(26, 23);
            this.buttonA.TabIndex = 3;
            this.buttonA.Text = "a";
            this.buttonA.UseVisualStyleBackColor = true;
            this.buttonA.Click += new System.EventHandler(this.buttonLetter_Click);
            // 
            // buttonE
            // 
            this.buttonE.Location = new System.Drawing.Point(30, 27);
            this.buttonE.Name = "buttonE";
            this.buttonE.Size = new System.Drawing.Size(26, 23);
            this.buttonE.TabIndex = 4;
            this.buttonE.Text = "e";
            this.buttonE.UseVisualStyleBackColor = true;
            this.buttonE.Click += new System.EventHandler(this.buttonLetter_Click);
            // 
            // buttonO
            // 
            this.buttonO.Location = new System.Drawing.Point(90, 27);
            this.buttonO.Name = "buttonO";
            this.buttonO.Size = new System.Drawing.Size(26, 23);
            this.buttonO.TabIndex = 6;
            this.buttonO.Text = "o";
            this.buttonO.UseVisualStyleBackColor = true;
            this.buttonO.Click += new System.EventHandler(this.buttonLetter_Click);
            // 
            // buttonI
            // 
            this.buttonI.Location = new System.Drawing.Point(60, 27);
            this.buttonI.Name = "buttonI";
            this.buttonI.Size = new System.Drawing.Size(26, 23);
            this.buttonI.TabIndex = 5;
            this.buttonI.Text = "i";
            this.buttonI.UseVisualStyleBackColor = true;
            this.buttonI.Click += new System.EventHandler(this.buttonLetter_Click);
            // 
            // buttonGrave
            // 
            this.buttonGrave.Location = new System.Drawing.Point(0, 0);
            this.buttonGrave.Name = "buttonGrave";
            this.buttonGrave.Size = new System.Drawing.Size(26, 23);
            this.buttonGrave.TabIndex = 10;
            this.buttonGrave.Text = "´";
            this.buttonGrave.UseVisualStyleBackColor = true;
            this.buttonGrave.Click += new System.EventHandler(this.buttonGrave_Click);
            // 
            // buttonC
            // 
            this.buttonC.Location = new System.Drawing.Point(119, -1);
            this.buttonC.Name = "buttonC";
            this.buttonC.Size = new System.Drawing.Size(26, 23);
            this.buttonC.TabIndex = 8;
            this.buttonC.Text = "ç";
            this.buttonC.UseVisualStyleBackColor = true;
            this.buttonC.Click += new System.EventHandler(this.buttonLetter_Click);
            // 
            // buttonU
            // 
            this.buttonU.Location = new System.Drawing.Point(120, 27);
            this.buttonU.Name = "buttonU";
            this.buttonU.Size = new System.Drawing.Size(26, 23);
            this.buttonU.TabIndex = 7;
            this.buttonU.Text = "u";
            this.buttonU.UseVisualStyleBackColor = true;
            this.buttonU.Click += new System.EventHandler(this.buttonLetter_Click);
            // 
            // buttonTrema
            // 
            this.buttonTrema.Location = new System.Drawing.Point(81, 0);
            this.buttonTrema.Name = "buttonTrema";
            this.buttonTrema.Size = new System.Drawing.Size(26, 23);
            this.buttonTrema.TabIndex = 13;
            this.buttonTrema.Text = "¨";
            this.buttonTrema.UseVisualStyleBackColor = true;
            this.buttonTrema.Click += new System.EventHandler(this.buttonTrema_Click);
            // 
            // buttonCirconflexe
            // 
            this.buttonCirconflexe.Location = new System.Drawing.Point(54, 0);
            this.buttonCirconflexe.Name = "buttonCirconflexe";
            this.buttonCirconflexe.Size = new System.Drawing.Size(26, 23);
            this.buttonCirconflexe.TabIndex = 12;
            this.buttonCirconflexe.Text = "^";
            this.buttonCirconflexe.UseVisualStyleBackColor = true;
            this.buttonCirconflexe.Click += new System.EventHandler(this.buttonCirconflexe_Click);
            // 
            // buttonAigu
            // 
            this.buttonAigu.Location = new System.Drawing.Point(27, 0);
            this.buttonAigu.Name = "buttonAigu";
            this.buttonAigu.Size = new System.Drawing.Size(26, 23);
            this.buttonAigu.TabIndex = 11;
            this.buttonAigu.Text = "`";
            this.buttonAigu.UseVisualStyleBackColor = true;
            this.buttonAigu.Click += new System.EventHandler(this.buttonAigu_Click);
            // 
            // FrenchLetters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonTrema);
            this.Controls.Add(this.buttonCirconflexe);
            this.Controls.Add(this.buttonAigu);
            this.Controls.Add(this.buttonGrave);
            this.Controls.Add(this.buttonC);
            this.Controls.Add(this.buttonU);
            this.Controls.Add(this.buttonO);
            this.Controls.Add(this.buttonI);
            this.Controls.Add(this.buttonE);
            this.Controls.Add(this.buttonA);
            this.Name = "FrenchLetters";
            this.Size = new System.Drawing.Size(148, 53);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonA;
        private System.Windows.Forms.Button buttonE;
        private System.Windows.Forms.Button buttonO;
        private System.Windows.Forms.Button buttonI;
        private System.Windows.Forms.Button buttonGrave;
        private System.Windows.Forms.Button buttonC;
        private System.Windows.Forms.Button buttonU;
        private System.Windows.Forms.Button buttonTrema;
        private System.Windows.Forms.Button buttonCirconflexe;
        private System.Windows.Forms.Button buttonAigu;
    }
}
