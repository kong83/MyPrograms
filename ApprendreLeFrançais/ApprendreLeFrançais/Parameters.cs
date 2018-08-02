using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ApprendreLeFrançais
{
    public class Parameters
    {
        public Size FormSize { get; set; }
        public Point FormLocation { get; set; }
        public int[] ListColumnsWidth { get; set; }

        public Parameters(string size, string location, string columnsWidth)
        {
            string[] tempArr;

            tempArr = size.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (tempArr.Length == 2)
            {
                FormSize = new Size(Convert.ToInt32(tempArr[0]), Convert.ToInt32(tempArr[1]));
            }

            tempArr = location.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (tempArr.Length == 2)
            {
                FormLocation = new Point(Convert.ToInt32(tempArr[0]), Convert.ToInt32(tempArr[1]));
            }

            ListColumnsWidth = columnsWidth.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
        }
    }
}
