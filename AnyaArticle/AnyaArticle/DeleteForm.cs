using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AnyaArticle
{
    public partial class DeleteForm : Form
    {
        private Form1 copyF;

        public DeleteForm(Form1 f1)
        {
            InitializeComponent();
            copyF = f1;
            DataGridViewRowCollection drc = copyF.ArticleList.Rows;
            DataGridViewRow dr = drc[copyF.ArticleList.CurrentCellAddress.Y];

            textBox1.Text = dr.Cells[0].Value.ToString();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        // Выход

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Удаление строки

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList mas = new ArrayList();
            ArrayList minus = new ArrayList();

            string s = textBox1.Text;
            string sOK = "";

            int a, b, n;

            // Удаление пробелов и проверка на допустимые символы (цифры, минусы и запятые)
            for (int i = 0; i < s.Length; i++)
            { // - = 45
                // , = 44
                //   = 32
                // 0 = 48
                // 9 = 57				
                n = s[i];
                if (n == 32)
                    continue;
                if ((n >= 48 && n <= 57) || n == 45 || n == 44)
                    sOK += s[i];
                else
                {
                    MessageBox.Show("Ошибка в записи номеров строк.");
                    return;
                }
            }

            // Разбор нашей строки и формирование массивов с номерами удаляемых строк
            s = sOK;
            n = 0;
            while (n < s.Length)
            {
                // Получаем первое число до запятой
                if ((int)s[n] < 48 || (int)s[n] > 57)
                {
                    MessageBox.Show("Ошибка в записи номеров строк.");
                    return;
                }
                sOK = "";
                while (n < s.Length && (int)s[n] >= 48 && (int)s[n] <= 57)
                {
                    sOK += s[n];
                    n++;
                }

                // Проверяем его на правильность
                try
                {
                    Convert.ToInt32(sOK);
                }
                catch
                {
                    MessageBox.Show("Ошибка в записи номера строки.");
                    return;
                }

                // Смотрим, если это последнее число - то заканчиваем разбор
                if (n == s.Length)
                {
                    mas.Add(sOK);
                    break;
                }

                // Если после числа идет запятая - то начинаем разбирать новый блок, 
                // до следующей запятой
                if ((int)s[n] == 44)
                {
                    mas.Add(sOK);
                    n++;
                    if (n == s.Length)
                    {
                        MessageBox.Show("Ошибка в записи номеров строк.");
                        return;
                    }
                    continue;
                }

                // Если дошли до сюда - значит после числа должен идти минус. 
                // Получаем число после минуса
                minus.Add(sOK);
                n++;
                if (n == s.Length || (int)s[n] < 48 || (int)s[n] > 57)
                {
                    MessageBox.Show("Ошибка в записи номеров строк.");
                    return;
                }
                sOK = "";
                while (n < s.Length && (int)s[n] >= 48 && (int)s[n] <= 57)
                {
                    sOK += s[n];
                    n++;
                }

                // Проверяем его на правильность
                try
                {
                    Convert.ToInt32(sOK);
                    minus.Add(sOK);
                }
                catch
                {
                    MessageBox.Show("Ошибка в записи номеров строк.");
                    return;
                }
                a = Convert.ToInt32((string)minus[minus.Count - 2]);
                b = Convert.ToInt32((string)minus[minus.Count - 1]);
                if (a > b)
                {
                    MessageBox.Show("Ошибка в записи номеров строк.");
                    return;
                }


                // Смотрим, чтобы после числа была запятая или конец строки
                if (n == s.Length)
                    break;

                if ((int)s[n] != 44)
                {
                    MessageBox.Show("Ошибка в записи номеров строк.");
                    return;
                }
                n++;
            }

            // Получаем список чисел из массива minus

            n = 0;
            while (n < minus.Count)
            {
                a = Convert.ToInt32((string)minus[n]);
                b = Convert.ToInt32((string)minus[n + 1]);
                for (int i = a; i <= b; i++)
                    mas.Add(i.ToString());
                n += 2;
            }

            // Сортируем массив в порядке возрастания
            string[] masString = new string[mas.Count];
            mas.CopyTo(masString);
            Array.Sort(masString);

            for (int j = masString.Length - 1; j >= 0; j--)
            {
                DataRowCollection Rows = copyF.dataSet1.Tables[0].Rows;
                for (int i = 0; i < copyF.dataSet1.Tables[0].Rows.Count; i++)
                {
                    DataRow CurrRow = Rows[i];
                    try
                    {
                        if (CurrRow.RowState == DataRowState.Deleted)
                            continue;

                        if (Convert.ToInt32(CurrRow[0]) == Convert.ToInt32(masString[j]))
                        {
                            copyF.dataSet1.Tables[0].Rows[i].Delete();
                            copyF.save = false;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Невозможно удалить строку №" + masString[j] + ".\r\nОшибка: " + ex.Message + "Нажмите кнопку 'Ok' и повторите попытку.", "Ошибка");
                        return;
                    }
                }
            }
            this.Close();
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Допустима запись вида: 1-5,10,13,7-8,16-20", this.textBox1, 15, -17);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.textBox1);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить указанные строки", this.button1, 15, -17);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button1);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Закрыть окно", this.button4, 15, -17);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button4);
        }
    }
}