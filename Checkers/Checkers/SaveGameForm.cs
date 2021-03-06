using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using zlib;
using System.IO;

namespace Checkers
{
    public partial class SaveGameForm : Form
    {
        ArrayList historyMas;
        ColorCheck whoFirst;
        ObjectCheck[,] masSave;

        public SaveGameForm(ArrayList historyM, ColorCheck whoF, ObjectCheck[,] masS)
        {
            InitializeComponent();

            historyMas = historyM;
            whoFirst = whoF;
            masSave = masS;
            saveFileDialog1.InitialDirectory = textPath.Text = Application.StartupPath;

        }

        /// <summary>
        /// Упаковывает массив байт
        /// </summary>
        /// <param name="xmlInfo">Массив для упаковки</param>
        /// <returns></returns>
        private byte[] PackXML(byte[] xmlInfo)
        {
            byte[] newByffer = new byte[1000000];
            MemoryStream stream = new MemoryStream(newByffer);
            ZOutputStream zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = 1000000 - 1;
            while (i >= 0 && newByffer[i] == 0)
                i--;
            byte[] rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
                rez[j] = newByffer[j];

            return rez;
        }

        // Сохранение истории в указанный файл
        private void button2_Click(object sender, EventArgs e)
        {
            string color = "white";
            if (whoFirst == ColorCheck.black)
                color = "black";

            string str = "<?xml version=\"1.0\" standalone=\"yes\"?><dataroot><whoFirst>" + color + "</whoFirst><board>";

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    string type;
                    if (masSave[i, j] == ObjectCheck.check_black)
                        type = "check_black";
                    else if (masSave[i, j] == ObjectCheck.check_black_dam)
                        type = "check_black_dam";
                    else if (masSave[i, j] == ObjectCheck.check_white)
                        type = "check_white";
                    else if (masSave[i, j] == ObjectCheck.check_white_dam)
                        type = "check_white_dam";
                    else if (masSave[i, j] == ObjectCheck.empty)
                        type = "empty";
                    else
                        type = "full";
                    str += "<cell>" + type + "</cell>";
                }
            str += "</board>";

            for (int i = 0; i < historyMas.Count; i++)
            {
                string[] strMas = (string[])historyMas[i];
                for (int j = 0; j < strMas.Length; j++)
                    if (strMas[j] == "")
                        strMas[j] = " ";
                str += "<parameter><par0>" + strMas[0] + "</par0><par1>" + strMas[1] + "</par1><par2>"
                 + strMas[2] + "</par2><par3>" + strMas[3] + "</par3><par4>" + strMas[4] + "</par4><par5>"
                    + strMas[5] + "</par5></parameter>";
            }
            str += "</dataroot>";
            byte[] xmlParameters = Encoding.Unicode.GetBytes(str);
            byte[] rez = PackXML(xmlParameters);

            try
            {
                FileInfo f = new FileInfo(textPath.Text);
                f.Delete();
            }
            catch { }
            try
            {
                FileStream fs = File.OpenWrite(textPath.Text);
                fs.Write(rez, 0, rez.Length);
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Файл не сохранён.");
            }
            this.Close();
        }

        // Выбор файла
        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                textPath.Text = saveFileDialog1.FileName;
        }

        // Закрыть
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}