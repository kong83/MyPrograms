using SurgeryHelper.Forms;
using System.Windows.Forms;

namespace SurgeryHelper.Tools
{
    public static class MessageBox
    {
        public static DialogResult ShowDialog(string text)
        {
            return ShowDialog(text, string.Empty);
        }

        public static DialogResult ShowDialog(string text, string caption)
        {
            return ShowDialog(text, caption, MessageBoxButtons.OK);
        }

        public static DialogResult ShowDialog(string text, string caption, MessageBoxButtons buttons)
        {
            return ShowDialog(text, caption, buttons, MessageBoxIcon.None);
        }

        public static DialogResult ShowDialog(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return (new MyMessageBox(text, caption, buttons, icon)).ShowDialog();
        }

        public static void Show(string text)
        {
            Show(text, string.Empty);
        }

        public static void Show(string text, string caption)
        {
            Show(text, caption, MessageBoxButtons.OK);
        }

        public static void Show(string text, string caption, MessageBoxButtons buttons)
        {
            Show(text, caption, buttons, MessageBoxIcon.None);
        }

        public static void Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            (new MyMessageBox(text, caption, buttons, icon)).Show();
        }
    }
}
