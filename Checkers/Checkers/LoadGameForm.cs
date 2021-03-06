using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using zlib;
using System.Collections;
using System.Xml;

namespace Checkers
{
    public partial class LoadGameForm : Form
    {
        MainForm mf;

        public LoadGameForm(MainForm mainForm)
        {
            InitializeComponent();

            mf = mainForm;
            openFileDialog1.InitialDirectory = textPath.Text = Application.StartupPath;
        }

        // Закрыть
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Выбор пути
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textPath.Text = openFileDialog1.FileName;
        }

        /// <summary>
        /// Распаковывает массив байт
        /// </summary>
        /// <param name="xmlInfo">Массив байт для распаковки</param>
        /// <returns></returns>
        private byte[] UnpackXML(byte[] xmlInfo)
        {
            byte[] newByffer = new byte[xmlInfo.Length * 100];
            MemoryStream stream = new MemoryStream(newByffer);
            ZOutputStream zStream = new ZOutputStream(stream);

            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
                i--;
            byte[] rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
                rez[j] = newByffer[j];

            return rez;
        }

        //Загрузить
        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(textPath.Text, FileMode.Open, FileAccess.Read);
            byte[] xmlParameter = new byte[fs.Length];
            fs.Read(xmlParameter, 0, xmlParameter.Length);
            fs.Close();

            byte[] rez;
            try
            {
                // Обработка и сохранение полученного массива xmlInfo			
                rez = UnpackXML(xmlParameter);
            }
            catch
            {
                MessageBox.Show("Файл повреждён.");
                return;
            }

            ArrayList masArray = new ArrayList();
            MemoryStream ms = new MemoryStream(rez);
            XmlTextReader reader = new XmlTextReader(ms);
            ColorCheck whoF = ColorCheck.disable;
            ObjectCheck[,] saveMas = new ObjectCheck[8, 8];

            try
            {
                while (!reader.EOF)
                {
                    reader.Read();
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "whofirst")
                    {
                        reader.Read();
                        if (reader.Value == "black")
                            whoF = ColorCheck.black;
                        else
                            whoF = ColorCheck.white;
                        reader.Read();
                    }

                    if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "board")
                    {
                        int w = 0, q = 0;
                        reader.Read();
                        string str;
                        while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "board"))
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                reader.Read();
                                str = reader.Value;
                                if (str == "check_black")
                                    saveMas[w, q] = ObjectCheck.check_black;
                                else if (str == "check_black_dam")
                                    saveMas[w, q] = ObjectCheck.check_black_dam;
                                else if (str == "check_white")
                                    saveMas[w, q] = ObjectCheck.check_white;
                                else if (str == "check_white_dam")
                                    saveMas[w, q] = ObjectCheck.check_white_dam;
                                else if (str == "empty")
                                    saveMas[w, q] = ObjectCheck.empty;
                                else
                                    saveMas[w, q] = ObjectCheck.full;

                                q++;
                                if (q > 7)
                                {
                                    w++;
                                    q = 0;
                                }
                            }
                            reader.Read();
                        }

                    }

                    if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "parameter")
                    {
                        reader.Read();
                        string[] str = new string[6];
                        while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "parameter"))
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                string type = reader.Name;
                                reader.Read();
                                switch (type.ToLower())
                                {
                                    case "par0":
                                        str[0] = reader.Value;
                                        reader.Read();
                                        break;
                                    case "par1":
                                        str[1] = reader.Value;
                                        reader.Read();
                                        break;
                                    case "par2":
                                        if (reader.Value != " ")
                                            str[2] = reader.Value;
                                        else
                                            str[2] = "";
                                        reader.Read();
                                        break;
                                    case "par3":
                                        str[3] = reader.Value;
                                        reader.Read();
                                        break;
                                    case "par4":
                                        str[4] = reader.Value;
                                        reader.Read();
                                        break;
                                    case "par5":
                                        if (reader.Value != " ")
                                            str[5] = reader.Value;
                                        else
                                            str[5] = "";
                                        reader.Read();
                                        break;
                                }
                            }
                            reader.Read();
                        }
                        masArray.Add(str);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            mf.whoFirst = whoF;
            mf.masSave = saveMas;
            mf.historyMas = masArray;
            this.Close();
        }
    }
}