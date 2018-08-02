using System;
using System.Windows.Forms;

namespace SurgeryHelper.Forms
{
    public partial class MergePatientDifferenceForm : Form
    {
        public MergePatientDifferenceForm(
            string fio, string nosology, string difference, bool showPrivateFolderDiffs)
        {
            InitializeComponent();

            labelInfo.Text = "Список различий в данных для пациента " + fio + " с нозологией " + nosology;
            MergeList.Items.Clear();
            MergeList.SelectionMode = SelectionMode.One;

            string[] diffs = difference.Split(new [] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach(string diff in diffs)
            {
                if (!showPrivateFolderDiffs && diff.ToLower().Contains("содержимое личной папки"))
                {
                    continue;
                }

                MergeList.Items.Add(diff);
            }
        }        

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
             buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
             buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
