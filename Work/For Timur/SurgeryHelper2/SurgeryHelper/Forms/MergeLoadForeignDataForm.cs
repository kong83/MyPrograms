using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class MergeLoadForeignDataForm : Form
    {
        private readonly MainForm _mainForm;
        private readonly CConfigurationEngine _configurationEngine;

        public MergeLoadForeignDataForm(MainForm mainForm, CConfigurationEngine configurationEngine)
        {
            InitializeComponent();

            _mainForm = mainForm;
            _mainForm.ForeinWorkersKeeper = null;

            _configurationEngine = configurationEngine;
            textBoxPath.Text = _configurationEngine.ForeignDataFolderPath;
        }


        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(textBoxPath.Text))
                {
                    MessageBox.ShowDialog("Указанный Вами путь не существует.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPath.Focus();
                    return;
                }

                var di = new DirectoryInfo(textBoxPath.Text);

                FileInfo[] fi = di.GetFiles("*.save");

                if (fi.Length == 0)
                {
                    MessageBox.ShowDialog("В указанной папке не обнаружено ни одного файла с данными.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPath.Focus();
                    return;
                }
            
                _mainForm.ForeinWorkersKeeper = new CWorkersKeeper(textBoxPath.Text, false);

                string diffInfo = GetDifferenсesInNosologies();
                if (!string.IsNullOrEmpty(diffInfo))
                {
                    MessageBox.ShowDialog("Сравнение баз данных невозможно, т.к. обнаружены отличия в нозологиях. Вам необходимо вручную устранить все отличия." + diffInfo, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _mainForm.ForeinWorkersKeeper = null;
                    return;
                }

                _configurationEngine.ForeignDataFolderPath = textBoxPath.Text;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDifferenсesInNosologies()
        {
            CNosologyWorker ownNosologyWorker = _mainForm.WorkersKeeper.NosologyWorker;
            CNosologyWorker foreinNosologyWorker = _mainForm.ForeinWorkersKeeper.NosologyWorker;

            var ownNewInfo = new StringBuilder("\r\nНаша база содержит следуюущие уникальные нозологии:\r\n");
            var diffInfo = new StringBuilder("\r\nСледующие нозологии имеют различия в типе или расположении:\r\n");
            foreach (var ownNosology in ownNosologyWorker.NosologyList)
            {
                CNosology foreinNosology = foreinNosologyWorker.GetByGeneralData(ownNosology.Name);
                if (foreinNosology == null)
                {
                    ownNewInfo.Append(ownNosology.Name + ", ");
                    continue;
                }

                if (ownNosology.Type != foreinNosology.Type)
                {
                    diffInfo.Append(ownNosology.Name + ", ");
                    continue;
                }

                CNosology parentOwnNosology = ownNosology;
                CNosology parentForeinNosology = foreinNosology;
                while (parentOwnNosology.IdParent != -1 && parentForeinNosology.IdParent != -1)
                {
                    parentOwnNosology = ownNosologyWorker.GetById(parentOwnNosology.IdParent);
                    parentForeinNosology = foreinNosologyWorker.GetById(parentForeinNosology.IdParent);
                    if (parentOwnNosology.Name != parentForeinNosology.Name)
                    {
                        break;
                    }
                }

                if (parentOwnNosology.IdParent != -1 || parentForeinNosology.IdParent != -1)
                {
                    diffInfo.Append(ownNosology.Name + ", ");
                }
            }

            var foreinNewInfo = new StringBuilder("\r\nВнешняя база содержит следуюущие уникальные нозологии:\r\n");
            foreach (var foreinNosology in foreinNosologyWorker.NosologyList)
            {
                CNosology ownNosology = ownNosologyWorker.GetByGeneralData(foreinNosology.Name);
                if (ownNosology == null)
                {
                    foreinNewInfo.Append(foreinNosology.Name + ", ");
                    continue;
                }

                if (ownNosology.Type != foreinNosology.Type)
                {
                    diffInfo.Append(foreinNosology.Name + ", ");
                }
            }

            string diffResult = string.Empty;

            if (ownNewInfo.ToString().EndsWith(", "))
            {
                diffResult += ownNewInfo.ToString().Substring(0, ownNewInfo.Length - 2);
            }

            if (foreinNewInfo.ToString().EndsWith(", "))
            {
                diffResult += foreinNewInfo.ToString().Substring(0, foreinNewInfo.Length - 2);
            }

            if (diffInfo.ToString().EndsWith(", "))
            {
                diffResult += diffInfo.ToString().Substring(0, diffInfo.Length - 2);
            }

            return diffResult;
        }


        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Выбор папки с файлами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string path = textBoxPath.Text;
                while (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                {
                    path = Directory.GetParent(path).FullName;
                }

                folderBrowserDialog1.SelectedPath = path;
            }
            catch
            {
                folderBrowserDialog1.SelectedPath = string.Empty;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonOk_Click(null, null);
                return true;
            }

            if (keyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #region Подсказки
        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Загрузить данные из внешней базы данных", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отказаться от объединения данных", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOpen_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Выбрать путь до папки с данными внешней базы данных", buttonOpen);
            buttonOpen.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpen_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOpen.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
