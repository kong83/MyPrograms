namespace StudyMath
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBoxRules = new System.Windows.Forms.GroupBox();
            this.checkBoxPositiveResult = new System.Windows.Forms.CheckBox();
            this.labelSec = new System.Windows.Forms.Label();
            this.textBoxDiapasonMax = new System.Windows.Forms.TextBox();
            this.textBoxDiapasonMin = new System.Windows.Forms.TextBox();
            this.labelDiapason = new System.Windows.Forms.Label();
            this.comboBoxCountExercise = new System.Windows.Forms.ComboBox();
            this.comboBoxTimeAnswer = new System.Windows.Forms.ComboBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelTimeAnswer = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.labelCountExercise = new System.Windows.Forms.Label();
            this.groupBoxRuns = new System.Windows.Forms.GroupBox();
            this.panelRightAnswerIcon = new System.Windows.Forms.Panel();
            this.panelNotRightAnswerIcon = new System.Windows.Forms.Panel();
            this.labelTimeProgress = new System.Windows.Forms.Label();
            this.progressBarTime = new System.Windows.Forms.ProgressBar();
            this.labelEquals = new System.Windows.Forms.Label();
            this.labelCalculateSecond = new System.Windows.Forms.Label();
            this.labelCalculateSign = new System.Windows.Forms.Label();
            this.labelCalculateFirst = new System.Windows.Forms.Label();
            this.labelCalculate = new System.Windows.Forms.Label();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.labelNumberInfo = new System.Windows.Forms.Label();
            this.labelNumbers = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.dataViewInfo = new System.Windows.Forms.DataGridView();
            this.ColumnNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnExample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAnswer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRightAnswer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkLabelAll = new System.Windows.Forms.LinkLabel();
            this.linkLabelNotRight = new System.Windows.Forms.LinkLabel();
            this.linkLabelRight = new System.Windows.Forms.LinkLabel();
            this.labelAllInfo = new System.Windows.Forms.Label();
            this.labelNotRightInfo = new System.Windows.Forms.Label();
            this.labelRightInfo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelVersion = new System.Windows.Forms.Label();
            this.groupBoxRules.SuspendLayout();
            this.groupBoxRuns.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxRules
            // 
            this.groupBoxRules.Controls.Add(this.checkBoxPositiveResult);
            this.groupBoxRules.Controls.Add(this.labelSec);
            this.groupBoxRules.Controls.Add(this.textBoxDiapasonMax);
            this.groupBoxRules.Controls.Add(this.textBoxDiapasonMin);
            this.groupBoxRules.Controls.Add(this.labelDiapason);
            this.groupBoxRules.Controls.Add(this.comboBoxCountExercise);
            this.groupBoxRules.Controls.Add(this.comboBoxTimeAnswer);
            this.groupBoxRules.Controls.Add(this.comboBoxType);
            this.groupBoxRules.Controls.Add(this.labelTimeAnswer);
            this.groupBoxRules.Controls.Add(this.labelType);
            this.groupBoxRules.Controls.Add(this.labelCountExercise);
            this.groupBoxRules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxRules.Location = new System.Drawing.Point(12, 12);
            this.groupBoxRules.Name = "groupBoxRules";
            this.groupBoxRules.Size = new System.Drawing.Size(426, 115);
            this.groupBoxRules.TabIndex = 0;
            this.groupBoxRules.TabStop = false;
            this.groupBoxRules.Text = "Условия";
            // 
            // checkBoxPositiveResult
            // 
            this.checkBoxPositiveResult.AutoSize = true;
            this.checkBoxPositiveResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxPositiveResult.Location = new System.Drawing.Point(16, 88);
            this.checkBoxPositiveResult.Name = "checkBoxPositiveResult";
            this.checkBoxPositiveResult.Size = new System.Drawing.Size(166, 17);
            this.checkBoxPositiveResult.TabIndex = 11;
            this.checkBoxPositiveResult.Text = "Результат всегда больше 0";
            this.checkBoxPositiveResult.UseVisualStyleBackColor = true;
            // 
            // labelSec
            // 
            this.labelSec.AutoSize = true;
            this.labelSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSec.Location = new System.Drawing.Point(144, 58);
            this.labelSec.Name = "labelSec";
            this.labelSec.Size = new System.Drawing.Size(28, 13);
            this.labelSec.TabIndex = 10;
            this.labelSec.Text = "сек.";
            // 
            // textBoxDiapasonMax
            // 
            this.textBoxDiapasonMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDiapasonMax.Location = new System.Drawing.Point(367, 55);
            this.textBoxDiapasonMax.Name = "textBoxDiapasonMax";
            this.textBoxDiapasonMax.Size = new System.Drawing.Size(38, 20);
            this.textBoxDiapasonMax.TabIndex = 8;
            this.textBoxDiapasonMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDiapason_Validating);
            // 
            // textBoxDiapasonMin
            // 
            this.textBoxDiapasonMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDiapasonMin.Location = new System.Drawing.Point(304, 55);
            this.textBoxDiapasonMin.Name = "textBoxDiapasonMin";
            this.textBoxDiapasonMin.Size = new System.Drawing.Size(38, 20);
            this.textBoxDiapasonMin.TabIndex = 6;
            this.textBoxDiapasonMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDiapason_Validating);
            // 
            // labelDiapason
            // 
            this.labelDiapason.AutoSize = true;
            this.labelDiapason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDiapason.Location = new System.Drawing.Point(226, 58);
            this.labelDiapason.Name = "labelDiapason";
            this.labelDiapason.Size = new System.Drawing.Size(138, 13);
            this.labelDiapason.TabIndex = 9;
            this.labelDiapason.Text = "Диапазон: от                 до";
            // 
            // comboBoxCountExercise
            // 
            this.comboBoxCountExercise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCountExercise.FormattingEnabled = true;
            this.comboBoxCountExercise.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50"});
            this.comboBoxCountExercise.Location = new System.Drawing.Point(359, 18);
            this.comboBoxCountExercise.Name = "comboBoxCountExercise";
            this.comboBoxCountExercise.Size = new System.Drawing.Size(46, 21);
            this.comboBoxCountExercise.TabIndex = 4;
            this.comboBoxCountExercise.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCountExercise_Validating);
            // 
            // comboBoxTimeAnswer
            // 
            this.comboBoxTimeAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxTimeAnswer.FormattingEnabled = true;
            this.comboBoxTimeAnswer.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxTimeAnswer.Location = new System.Drawing.Point(98, 55);
            this.comboBoxTimeAnswer.Name = "comboBoxTimeAnswer";
            this.comboBoxTimeAnswer.Size = new System.Drawing.Size(40, 21);
            this.comboBoxTimeAnswer.TabIndex = 2;
            this.comboBoxTimeAnswer.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxTimeAnswer_Validating);
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Сложение",
            "Вычитание",
            "Умножение",
            "Деление"});
            this.comboBoxType.Location = new System.Drawing.Point(98, 18);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(100, 21);
            this.comboBoxType.TabIndex = 0;
            // 
            // labelTimeAnswer
            // 
            this.labelTimeAnswer.AutoSize = true;
            this.labelTimeAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTimeAnswer.Location = new System.Drawing.Point(6, 58);
            this.labelTimeAnswer.Name = "labelTimeAnswer";
            this.labelTimeAnswer.Size = new System.Drawing.Size(86, 13);
            this.labelTimeAnswer.TabIndex = 3;
            this.labelTimeAnswer.Text = "Время на ответ";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelType.Location = new System.Drawing.Point(6, 21);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(71, 13);
            this.labelType.TabIndex = 2;
            this.labelType.Text = "Тип заданий";
            // 
            // labelCountExercise
            // 
            this.labelCountExercise.AutoSize = true;
            this.labelCountExercise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountExercise.Location = new System.Drawing.Point(226, 21);
            this.labelCountExercise.Name = "labelCountExercise";
            this.labelCountExercise.Size = new System.Drawing.Size(111, 13);
            this.labelCountExercise.TabIndex = 1;
            this.labelCountExercise.Text = "Количество заданий";
            // 
            // groupBoxRuns
            // 
            this.groupBoxRuns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxRuns.Controls.Add(this.panelRightAnswerIcon);
            this.groupBoxRuns.Controls.Add(this.panelNotRightAnswerIcon);
            this.groupBoxRuns.Controls.Add(this.labelTimeProgress);
            this.groupBoxRuns.Controls.Add(this.progressBarTime);
            this.groupBoxRuns.Controls.Add(this.labelEquals);
            this.groupBoxRuns.Controls.Add(this.labelCalculateSecond);
            this.groupBoxRuns.Controls.Add(this.labelCalculateSign);
            this.groupBoxRuns.Controls.Add(this.labelCalculateFirst);
            this.groupBoxRuns.Controls.Add(this.labelCalculate);
            this.groupBoxRuns.Controls.Add(this.textBoxAnswer);
            this.groupBoxRuns.Controls.Add(this.labelNumberInfo);
            this.groupBoxRuns.Controls.Add(this.labelNumbers);
            this.groupBoxRuns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxRuns.Location = new System.Drawing.Point(12, 175);
            this.groupBoxRuns.Name = "groupBoxRuns";
            this.groupBoxRuns.Size = new System.Drawing.Size(426, 130);
            this.groupBoxRuns.TabIndex = 2;
            this.groupBoxRuns.TabStop = false;
            this.groupBoxRuns.Text = "Выполнение";
            // 
            // panelRightAnswerIcon
            // 
            this.panelRightAnswerIcon.BackgroundImage = global::StudyMath.Properties.Resources.RightAnswer;
            this.panelRightAnswerIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelRightAnswerIcon.Location = new System.Drawing.Point(363, 49);
            this.panelRightAnswerIcon.Name = "panelRightAnswerIcon";
            this.panelRightAnswerIcon.Size = new System.Drawing.Size(35, 35);
            this.panelRightAnswerIcon.TabIndex = 10;
            this.panelRightAnswerIcon.Visible = false;
            // 
            // panelNotRightAnswerIcon
            // 
            this.panelNotRightAnswerIcon.BackgroundImage = global::StudyMath.Properties.Resources.NotRightAnswer;
            this.panelNotRightAnswerIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelNotRightAnswerIcon.Location = new System.Drawing.Point(363, 49);
            this.panelNotRightAnswerIcon.Name = "panelNotRightAnswerIcon";
            this.panelNotRightAnswerIcon.Size = new System.Drawing.Size(35, 35);
            this.panelNotRightAnswerIcon.TabIndex = 13;
            this.panelNotRightAnswerIcon.Visible = false;
            // 
            // labelTimeProgress
            // 
            this.labelTimeProgress.AutoSize = true;
            this.labelTimeProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTimeProgress.Location = new System.Drawing.Point(9, 98);
            this.labelTimeProgress.Name = "labelTimeProgress";
            this.labelTimeProgress.Size = new System.Drawing.Size(43, 13);
            this.labelTimeProgress.TabIndex = 12;
            this.labelTimeProgress.Text = "Время:";
            // 
            // progressBarTime
            // 
            this.progressBarTime.Location = new System.Drawing.Point(80, 92);
            this.progressBarTime.Name = "progressBarTime";
            this.progressBarTime.Size = new System.Drawing.Size(197, 23);
            this.progressBarTime.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTime.TabIndex = 11;
            // 
            // labelEquals
            // 
            this.labelEquals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelEquals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEquals.Location = new System.Drawing.Point(246, 55);
            this.labelEquals.Name = "labelEquals";
            this.labelEquals.Size = new System.Drawing.Size(31, 22);
            this.labelEquals.TabIndex = 9;
            this.labelEquals.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCalculateSecond
            // 
            this.labelCalculateSecond.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCalculateSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCalculateSecond.Location = new System.Drawing.Point(175, 55);
            this.labelCalculateSecond.Name = "labelCalculateSecond";
            this.labelCalculateSecond.Size = new System.Drawing.Size(52, 22);
            this.labelCalculateSecond.TabIndex = 8;
            this.labelCalculateSecond.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCalculateSign
            // 
            this.labelCalculateSign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCalculateSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCalculateSign.Location = new System.Drawing.Point(138, 55);
            this.labelCalculateSign.Name = "labelCalculateSign";
            this.labelCalculateSign.Size = new System.Drawing.Size(31, 22);
            this.labelCalculateSign.TabIndex = 7;
            this.labelCalculateSign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCalculateFirst
            // 
            this.labelCalculateFirst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCalculateFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCalculateFirst.Location = new System.Drawing.Point(80, 55);
            this.labelCalculateFirst.Name = "labelCalculateFirst";
            this.labelCalculateFirst.Size = new System.Drawing.Size(52, 22);
            this.labelCalculateFirst.TabIndex = 6;
            this.labelCalculateFirst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCalculate
            // 
            this.labelCalculate.AutoSize = true;
            this.labelCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCalculate.Location = new System.Drawing.Point(9, 59);
            this.labelCalculate.Name = "labelCalculate";
            this.labelCalculate.Size = new System.Drawing.Size(65, 13);
            this.labelCalculate.TabIndex = 5;
            this.labelCalculate.Text = "Вычислите:";
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAnswer.Location = new System.Drawing.Point(297, 56);
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(42, 20);
            this.textBoxAnswer.TabIndex = 0;
            this.textBoxAnswer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxAnswer_KeyUp);
            // 
            // labelNumberInfo
            // 
            this.labelNumberInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNumberInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNumberInfo.Location = new System.Drawing.Point(101, 20);
            this.labelNumberInfo.Name = "labelNumberInfo";
            this.labelNumberInfo.Size = new System.Drawing.Size(59, 20);
            this.labelNumberInfo.TabIndex = 2;
            this.labelNumberInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNumbers
            // 
            this.labelNumbers.AutoSize = true;
            this.labelNumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNumbers.Location = new System.Drawing.Point(9, 24);
            this.labelNumbers.Name = "labelNumbers";
            this.labelNumbers.Size = new System.Drawing.Size(86, 13);
            this.labelNumbers.TabIndex = 1;
            this.labelNumbers.Text = "Номер задания";
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxResults.Controls.Add(this.linkLabelAll);
            this.groupBoxResults.Controls.Add(this.linkLabelNotRight);
            this.groupBoxResults.Controls.Add(this.linkLabelRight);
            this.groupBoxResults.Controls.Add(this.labelAllInfo);
            this.groupBoxResults.Controls.Add(this.labelNotRightInfo);
            this.groupBoxResults.Controls.Add(this.labelRightInfo);
            this.groupBoxResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxResults.Location = new System.Drawing.Point(12, 321);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(426, 166);
            this.groupBoxResults.TabIndex = 4;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Результаты";
            // 
            // dataViewInfo
            // 
            this.dataViewInfo.AllowUserToAddRows = false;
            this.dataViewInfo.AllowUserToDeleteRows = false;
            this.dataViewInfo.AllowUserToResizeColumns = false;
            this.dataViewInfo.AllowUserToResizeRows = false;
            this.dataViewInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataViewInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataViewInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumber,
            this.ColumnExample,
            this.ColumnAnswer,
            this.ColumnRightAnswer});
            this.dataViewInfo.Location = new System.Drawing.Point(187, 338);
            this.dataViewInfo.MultiSelect = false;
            this.dataViewInfo.Name = "dataViewInfo";
            this.dataViewInfo.ReadOnly = true;
            this.dataViewInfo.RowHeadersVisible = false;
            this.dataViewInfo.RowTemplate.Height = 16;
            this.dataViewInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataViewInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataViewInfo.ShowCellToolTips = false;
            this.dataViewInfo.Size = new System.Drawing.Size(245, 136);
            this.dataViewInfo.TabIndex = 12;
            // 
            // ColumnNumber
            // 
            this.ColumnNumber.HeaderText = "№";
            this.ColumnNumber.Name = "ColumnNumber";
            this.ColumnNumber.ReadOnly = true;
            this.ColumnNumber.Width = 30;
            // 
            // ColumnExample
            // 
            this.ColumnExample.HeaderText = "Задание";
            this.ColumnExample.Name = "ColumnExample";
            this.ColumnExample.ReadOnly = true;
            this.ColumnExample.Width = 60;
            // 
            // ColumnAnswer
            // 
            this.ColumnAnswer.HeaderText = "Ответ";
            this.ColumnAnswer.Name = "ColumnAnswer";
            this.ColumnAnswer.ReadOnly = true;
            this.ColumnAnswer.Width = 50;
            // 
            // ColumnRightAnswer
            // 
            this.ColumnRightAnswer.HeaderText = "Правильный ответ";
            this.ColumnRightAnswer.Name = "ColumnRightAnswer";
            this.ColumnRightAnswer.ReadOnly = true;
            this.ColumnRightAnswer.Width = 80;
            // 
            // linkLabelAll
            // 
            this.linkLabelAll.AutoSize = true;
            this.linkLabelAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelAll.Location = new System.Drawing.Point(11, 139);
            this.linkLabelAll.Name = "linkLabelAll";
            this.linkLabelAll.Size = new System.Drawing.Size(40, 13);
            this.linkLabelAll.TabIndex = 11;
            this.linkLabelAll.TabStop = true;
            this.linkLabelAll.Text = "Всего:";
            this.linkLabelAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAll_LinkClicked);
            // 
            // linkLabelNotRight
            // 
            this.linkLabelNotRight.AutoSize = true;
            this.linkLabelNotRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelNotRight.Location = new System.Drawing.Point(11, 83);
            this.linkLabelNotRight.Name = "linkLabelNotRight";
            this.linkLabelNotRight.Size = new System.Drawing.Size(78, 13);
            this.linkLabelNotRight.TabIndex = 10;
            this.linkLabelNotRight.TabStop = true;
            this.linkLabelNotRight.Text = "Неправильно:";
            this.linkLabelNotRight.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNotRight_LinkClicked);
            // 
            // linkLabelRight
            // 
            this.linkLabelRight.AutoSize = true;
            this.linkLabelRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelRight.Location = new System.Drawing.Point(11, 25);
            this.linkLabelRight.Name = "linkLabelRight";
            this.linkLabelRight.Size = new System.Drawing.Size(66, 13);
            this.linkLabelRight.TabIndex = 9;
            this.linkLabelRight.TabStop = true;
            this.linkLabelRight.Text = "Правильно:";
            this.linkLabelRight.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRight_LinkClicked);
            // 
            // labelAllInfo
            // 
            this.labelAllInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAllInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAllInfo.Location = new System.Drawing.Point(97, 135);
            this.labelAllInfo.Name = "labelAllInfo";
            this.labelAllInfo.Size = new System.Drawing.Size(59, 20);
            this.labelAllInfo.TabIndex = 8;
            this.labelAllInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNotRightInfo
            // 
            this.labelNotRightInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNotRightInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNotRightInfo.Location = new System.Drawing.Point(98, 79);
            this.labelNotRightInfo.Name = "labelNotRightInfo";
            this.labelNotRightInfo.Size = new System.Drawing.Size(59, 20);
            this.labelNotRightInfo.TabIndex = 6;
            this.labelNotRightInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelRightInfo
            // 
            this.labelRightInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRightInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRightInfo.Location = new System.Drawing.Point(98, 21);
            this.labelRightInfo.Name = "labelRightInfo";
            this.labelRightInfo.Size = new System.Drawing.Size(59, 20);
            this.labelRightInfo.TabIndex = 5;
            this.labelRightInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartStop.Location = new System.Drawing.Point(164, 140);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(75, 29);
            this.buttonStartStop.TabIndex = 1;
            this.buttonStartStop.Text = "Старт";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(379, 130);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(62, 13);
            this.labelVersion.TabIndex = 13;
            this.labelVersion.Text = "Версия 1.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 499);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.dataViewInfo);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBoxRuns);
            this.Controls.Add(this.groupBoxRules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изучение математики";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxRules.ResumeLayout(false);
            this.groupBoxRules.PerformLayout();
            this.groupBoxRuns.ResumeLayout(false);
            this.groupBoxRuns.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRules;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelTimeAnswer;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label labelCountExercise;
        private System.Windows.Forms.Label labelDiapason;
        private System.Windows.Forms.ComboBox comboBoxCountExercise;
        private System.Windows.Forms.ComboBox comboBoxTimeAnswer;
        private System.Windows.Forms.TextBox textBoxDiapasonMin;
        private System.Windows.Forms.TextBox textBoxDiapasonMax;
        private System.Windows.Forms.GroupBox groupBoxRuns;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Label labelNumberInfo;
        private System.Windows.Forms.Label labelNumbers;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelCalculate;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.Panel panelRightAnswerIcon;
        private System.Windows.Forms.Label labelEquals;
        private System.Windows.Forms.Label labelCalculateSecond;
        private System.Windows.Forms.Label labelCalculateSign;
        private System.Windows.Forms.Label labelCalculateFirst;
        private System.Windows.Forms.ProgressBar progressBarTime;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Label labelTimeProgress;
        private System.Windows.Forms.DataGridView dataViewInfo;
        private System.Windows.Forms.LinkLabel linkLabelAll;
        private System.Windows.Forms.LinkLabel linkLabelNotRight;
        private System.Windows.Forms.LinkLabel linkLabelRight;
        private System.Windows.Forms.Label labelAllInfo;
        private System.Windows.Forms.Label labelNotRightInfo;
        private System.Windows.Forms.Label labelRightInfo;
        private System.Windows.Forms.Panel panelNotRightAnswerIcon;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label labelSec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnExample;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRightAnswer;
        private System.Windows.Forms.CheckBox checkBoxPositiveResult;
        private System.Windows.Forms.Label labelVersion;
    }
}

