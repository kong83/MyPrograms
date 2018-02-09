using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using zlib;

namespace Notepad
{
    public partial class ZLibForm : Form
    {
        public ZLibForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Выбор файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = openFileDialog1.FileName;
            }
        }


        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Упаковка текста в выбранный файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPack_Click(object sender, EventArgs e)
        {
            try
            {
                string coding = "windows-1251";
                if (_isExtraParametersVisible)
                {
                    if (!string.IsNullOrEmpty(comboBoxCoding.Text) && comboBoxCoding.Text != "По умолчанию")
                    {
                        coding = comboBoxCoding.Text;
                    }
                }

                byte[] byteArr = Encoding.GetEncoding(coding).GetBytes(textBoxText.Text);
                byte[] packArr = PackBytes(byteArr);

                if (File.Exists(textBoxPath.Text))
                    File.Delete(textBoxPath.Text);

                using (var fs = new FileStream(textBoxPath.Text, FileMode.CreateNew))
                {
                    fs.Write(packArr, 0, packArr.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Распаковка текста из выбранного файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUnpack_Click(object sender, EventArgs e)
        {
            try
            {
                string coding = "windows-1251";
                int skipFirst = 0;
                int skipLast = 0;

                if (_isExtraParametersVisible)
                {
                    if (!string.IsNullOrEmpty(comboBoxCoding.Text) && comboBoxCoding.Text != "По умолчанию")
                    {
                        coding = comboBoxCoding.Text;
                    }
                    skipFirst = Convert.ToInt32(textBoxSkipFirst.Text);
                    skipLast = Convert.ToInt32(textBoxSkipLast.Text);
                }
               
                using (var fs = new FileStream(textBoxPath.Text, FileMode.Open))
                {
                    var byteArr = new byte[fs.Length];

                    fs.Read(byteArr, skipFirst, (int)fs.Length - skipLast);
                    fs.Close();
                    byte[] unpackArr = UnpackBytes(byteArr);
                    textBoxText.Text = Encoding.GetEncoding(coding).GetString(unpackArr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// True - когда доп. параметры видны, False - в противном случае
        /// </summary>
        private bool _isExtraParametersVisible;

        /// <summary>
        /// Показ и убирание дополнительных параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonParameters_Click(object sender, EventArgs e)
        {
            MinimumSize = new System.Drawing.Size(0, 0);
            groupBoxExtraParameters.Anchor = textBoxPath.Anchor = textBoxText.Anchor = buttonHideExtraParameters.Anchor = 
            buttonClose.Anchor = buttonPack.Anchor = buttonSelect.Anchor = buttonShowExtraParameters.Anchor =
            buttonUnpack.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            if (_isExtraParametersVisible)
            {
                Height -= 120;
            }
            else
            {
                Height += 120;
            }

            buttonHideExtraParameters.Visible = !buttonHideExtraParameters.Visible;
            buttonShowExtraParameters.Visible = !buttonShowExtraParameters.Visible;
            _isExtraParametersVisible = !_isExtraParametersVisible;
            groupBoxExtraParameters.Visible = !groupBoxExtraParameters.Visible;

            buttonClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            buttonPack.Anchor = buttonShowExtraParameters.Anchor =
            buttonHideExtraParameters.Anchor = buttonUnpack.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            textBoxText.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            textBoxPath.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            buttonSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxExtraParameters.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            MinimumSize = new System.Drawing.Size(650, 350);
        }


        /// <summary>
        /// Запаковывает массив байт
        /// </summary>
        /// <param name="xmlInfo">Массив байт для запаковки</param>
        /// <returns></returns>
        private static byte[] PackBytes(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }

            var rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }

            return rez;
        }


        /// <summary>
        /// Распаковывает массив байт
        /// </summary>
        /// <param name="xmlInfo">Массив байт для распаковки</param>
        /// <returns></returns>
        private static byte[] UnpackBytes(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream);

            zStream.Write(xmlInfo, 0, xmlInfo.Length);

            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }

            var rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }
            return rez;
        }

        private void buttonPack_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Запаковать введённый текст в указанный файл. Внимание! Существующий файл перезатрётся", buttonPack, -10, -17);
        }

        private void buttonPack_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonPack);
        }

        private void buttonUnpack_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Извлечь текст из указанного файла", buttonUnpack, -10, -17);
        }

        private void buttonUnpack_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonUnpack);
        }

        private void buttonShowExtraParameters_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Показать дополнительные параметры распаковки", buttonShowExtraParameters, -10, -17);
        }

        private void buttonShowExtraParameters_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonShowExtraParameters);
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Закрыть окно", buttonClose, -10, -17);
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
        }
    }
}
