using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Notepad
{
    public partial class PerfCounterForm : Form
    {
        public PerfCounterForm()
        {
            InitializeComponent();
        }

        private void buttonRemove_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Удалить выделенные счётчики производительности", buttonRemove, -10, -17);
        }

        private void buttonRemove_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRemove);
        }

        private void buttonRefrsh_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Обновить список", buttonRefrsh, -10, -17);
        }

        private void buttonRefrsh_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRefrsh);
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Закрыть окно", buttonClose, -10, -17);
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PerfCounterForm_Shown(object sender, EventArgs e)
        {
            buttonRefrsh_Click(sender, e);
        }

        /// <summary>
        /// Обновить список счётчиков производительности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRefrsh_Click(object sender, EventArgs e)
        {
            PerfCounterList.Rows.Clear();

            foreach (PerformanceCounterCategory category in PerformanceCounterCategory.GetCategories())
            {
                var param = new object[] { category.CategoryName, category.CategoryHelp };
                PerfCounterList.Rows.Add(param);
            }

            PerfCounterList.Sort(PerfCounterList.Columns[0], ListSortDirection.Ascending);          
        }

        /// <summary>
        /// Удалить выделенные счётчики производительности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Вы действительно хотите удалить все выделенные счётчики производительности?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            foreach (DataGridViewRow dr in PerfCounterList.Rows)
            {
                if (dr.Selected)
                {
                    PerformanceCounterCategory.Delete(dr.Cells[0].Value.ToString());
                }
            }

            buttonRefrsh_Click(sender, e);
        }
    }
}
