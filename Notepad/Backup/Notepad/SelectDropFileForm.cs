using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    public partial class SelectDropFileForm : Form
    {
        readonly MainForm _mainForm;
        readonly string[] _filePaths;
        bool _closeOk;

        public SelectDropFileForm(MainForm mf, string[] fP)
        {
            InitializeComponent();
            _mainForm = mf;
            _filePaths = fP;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _closeOk = true;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectDropFileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_closeOk)
            {
                _mainForm.SelectItem = -1;
            }
        }

        /// <summary>
        /// Формирование всех радиокнопок при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectDropFileForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < _filePaths.Length; i++)
            {
                var rb = new RadioButton
                {
                    Parent = this,
                    AutoSize = true,
                    Name = "rbtn" + i,
                    Text = _filePaths[i],
                    Location = new Point(20, 13 + 23*i)
                };
                rb.CheckedChanged += RbCheckedChanged;
                if (i == 0)
                {
                    rb.Checked = true;
                }
            }

            timer1.Enabled = true;
        }

        /// <summary>
        /// Смена выделенного файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbCheckedChanged(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;
            _mainForm.SelectItem = Convert.ToInt32(rb.Name.Substring(4));
        }

        /// <summary>
        /// Изменение расположения и размера формы после формирования всех радиокнопок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            int maxWidth = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                try
                {
                    var rb = (RadioButton)Controls[i];
                    if (rb.Width > maxWidth && rb.Width < 1200)
                    {
                        maxWidth = rb.Width;
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }
            Size = new Size(maxWidth + 20 + 20, 13 + 70 + _filePaths.Length * 23);
            int x = Math.Max(_mainForm.Location.X + _mainForm.Width / 2 - Width / 2, 0);
            int y = Math.Max(_mainForm.Location.Y + _mainForm.Height / 2 - Height / 2, 0);
            Location = new Point(x, y);
            buttonOK.Location = new Point(Width / 2 - 20 - buttonOK.Width, buttonOK.Location.Y);
            buttonCancel.Location = new Point(Width / 2 + 20, buttonCancel.Location.Y);
        }
    }
}
