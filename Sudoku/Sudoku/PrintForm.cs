using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Sudoku
{
    public partial class PrintForm : Form
    {
        MainForm mainForm;

        public PrintForm(MainForm mf)
        {
            InitializeComponent();

            mainForm = mf;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Предварительный просмотр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPreview_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.WindowState = FormWindowState.Maximized;
                printPreviewDialog1.Text = "Предварительный просмотр";
                printPreviewDialog1.Icon = this.Icon;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Параметры страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                pageSetupDialog1.PageSettings = printDocument1.DefaultPageSettings;
                pageSetupDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Печать
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    //printDocument1.PrinterSettings = printDialog1.PrinterSettings;        
                    try
                    {
                        printDocument1.Print();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Произошла ошибка: \r\n" + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: \r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Рисование текста для печати
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            int cellWidth = 0;
            try
            {
                cellWidth = Convert.ToInt32(textBoxWidth.Text);
                if (cellWidth < 20 || cellWidth > 100)
                {
                    MessageBox.Show("Ширина ячейки может принимать значения от 20 до 100", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в записи ширины ячейки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int digitSize = 0;
            try
            {
                digitSize = Convert.ToInt32(textBoxSize.Text);
                if (digitSize < 8 || digitSize > 50)
                {
                    MessageBox.Show("Размер шрифта может принимать значения от 8 до 50", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в записи размера шрифта", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            Graphics g = ev.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            SolidBrush brush = new SolidBrush(Color.LightGray);
            SolidBrush stringBrush = new SolidBrush(Color.Black);
            Font smartLabelFont = new Font(FontFamily.GenericSansSerif, digitSize / 3, FontStyle.Regular); ;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Font font;

                    if (mainForm.cells[i, j].CellType == CellType.closed)
                    {
                        g.FillRectangle(brush, leftMargin + j * cellWidth, topMargin + i * cellWidth, cellWidth, cellWidth);
                        font = new Font(FontFamily.GenericSansSerif, digitSize, FontStyle.Bold);
                    }
                    else
                    {
                        font = new Font(FontFamily.GenericSansSerif, digitSize, FontStyle.Regular);
                    }
                    g.DrawRectangle(pen, leftMargin + j * cellWidth, topMargin + i * cellWidth, cellWidth, cellWidth);

                    // Рисование значения ячейки
                    if (mainForm.cells[i, j].CellText != 0)
                    {
                        double d1 = ((topMargin + i * cellWidth) + (topMargin + (i + 1) * cellWidth)) / 2 - digitSize / 1.3;
                        double d2 = ((leftMargin + j * cellWidth) + (leftMargin + (j + 1) * cellWidth)) / 2 - digitSize / 2.5;
                        g.DrawString(mainForm.cells[i, j].CellText.ToString(), font, stringBrush, (float)d2, (float)d1, StringFormat.GenericTypographic);
                    }

                    // Рисование дополнительных меток   
                    if (mainForm.cells[i, j].CellType == CellType.smartLabel)
                    {
                        for (int q = 0; q < 3; q++)
                        {
                            for (int p = 0; p < 2; p++)
                            {
                                int smartLabel = mainForm.cells[i, j].GetSmartLabel(q, p);
                                if (smartLabel != 0)
                                {
                                    double shift = cellWidth / 16;
                                    double leftX = leftMargin + j * cellWidth + shift + p * (cellWidth / 4) * 2.65;
                                    double leftY = topMargin + i * cellWidth + shift + q * (cellWidth / 4 + shift * 1.3);
                                    g.DrawRectangle(pen, (float)leftX, (float)leftY, (float)cellWidth / 4, (float)cellWidth / 4);
                                    g.DrawString(smartLabel.ToString(), smartLabelFont, stringBrush, (float)(leftX + (cellWidth / 8) - ((digitSize / 3) / 3)), (float)(leftY + (cellWidth / 8) - ((digitSize / 3) / 1.5)), StringFormat.GenericTypographic);
                                }
                            }
                        }
                    }
                }
            }
            ev.HasMorePages = false;
        }
    }
}
