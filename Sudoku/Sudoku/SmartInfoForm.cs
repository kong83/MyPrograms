using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class SmartInfoForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        Cell cell;

        /// <summary>
        /// Строка выделенной метки
        /// </summary>
        int selX;
        
        /// <summary>
        /// Столбец выделенной метки
        /// </summary>
        int selY;

        /// <summary>
        /// Форма для установки дополнительных меток
        /// </summary>
        /// <param name="mf">Указатель на форму Main</param>
        /// <param name="c">Выделенная ячейка</param>
        public SmartInfoForm(Cell c)
        {
            cell = c;
            InitializeComponent();

            selX = selY = 0;
            ((Label)(panel.Controls["label00"])).BackColor = Color.LightGray;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    ((Label)(panel.Controls["label" + i + j])).Text = cell.GetSmartLabelText(i, j);
                    
                }
            }
        }


        /// <summary>
        /// Применение изменений и закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Cell oldCell = cell.Clone();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int text = 0;
                    if (((Label)(panel.Controls["label" + i + j])).Text != "")
                    {
                        text = Convert.ToInt32(((Label)(panel.Controls["label" + i + j])).Text);                        
                    }                    
                    cell.SetSmartLabel(i, j, text);                    
                }
            }
            Cell newCell = cell.Clone();
            cell.mainForm.historyClass.AddStep(oldCell, newCell);
            this.Close();
        }


        /// <summary>
        /// Отмена изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Перевод фокуса на невидимую кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drop_Focus(object sender, EventArgs e)
        {
            buttonFocus.Focus();
        }


        /// <summary>
        /// Выделение цветом новой ячейки
        /// </summary>
        private void SelectCell(int newX, int newY)
        {
            ((Label)(panel.Controls["label" + selX + selY])).BackColor = Color.White;
            selX = newX;
            selY = newY;
            ((Label)(panel.Controls["label" + selX + selY])).BackColor = Color.LightGray;
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonOK_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                buttonCancel_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Right)
            {
                if (selY + 1 < 2)
                {
                    SelectCell(selX, selY + 1);
                }
                else
                {
                    SelectCell(selX, 0);
                }
                return true;
            }
            else if (keyData == Keys.Left)
            {
                if (selY - 1 > -1)
                {
                    SelectCell(selX, selY - 1);
                }
                else
                {
                    SelectCell(selX, 1);
                }
                return true;
            }
            else if (keyData == Keys.Down)
            {
                if (selX + 1 < 3)
                {
                    SelectCell(selX + 1, selY);
                }
                else
                {
                    SelectCell(0, selY);
                }
                return true;
            }
            else if (keyData == Keys.Up)
            {
                if (selX - 1 > -1)
                {
                    SelectCell(selX - 1, selY);
                }
                else
                {
                    SelectCell(2, selY);
                }
                return true;
            }
            else if (keyData == Keys.Space || keyData == Keys.D0 || keyData == Keys.NumPad0 ||
                keyData == Keys.Delete || keyData == Keys.Back)
            {
                ((Label)(panel.Controls["label" + selX + selY])).Text = "";
                return true;
            }
            else if (keyData == Keys.D1 || keyData == Keys.D2
                 || keyData == Keys.D3 || keyData == Keys.D4
                 || keyData == Keys.D5 || keyData == Keys.D6
                 || keyData == Keys.D7 || keyData == Keys.D8
                 || keyData == Keys.D9)
            {
                string s = keyData.ToString("D");
                ((Label)(panel.Controls["label" + selX + selY])).Text = (Convert.ToInt32(s) - 48).ToString();
                return true;
            }
            else if (keyData == Keys.NumPad1 || keyData == Keys.NumPad2
                 || keyData == Keys.NumPad3 || keyData == Keys.NumPad4
                 || keyData == Keys.NumPad5 || keyData == Keys.NumPad6
                 || keyData == Keys.NumPad7 || keyData == Keys.NumPad8
                 || keyData == Keys.NumPad9)
            {
                string s = keyData.ToString("D");
                ((Label)(panel.Controls["label" + selX + selY])).Text = (Convert.ToInt32(s) - 96).ToString();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                Help.ShowHelp(new Control(), cell.mainForm.helpPath, HelpNavigator.Topic, "smartInfo.html");
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// Выделение метки нажатием мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_MouseClick(object sender, MouseEventArgs e)
        {
            string s = ((Label)sender).Name.Replace("label", "");

            SelectCell(Convert.ToInt32(s.Substring(0, 1)), Convert.ToInt32(s.Substring(1, 1)));
        }

    }
}
