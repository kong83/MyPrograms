using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FastCache;
using SlowCache;

namespace CacheTestApp
{
    public partial class Form1 : Form
    {
        private IFastCacheEngine _fastCache;
        private readonly ISlowCacheEngine _slowCache;

        public Form1()
        {
            InitializeComponent();
            _slowCache = new SlowCacheEngine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCache();
        }

        private void InitCache()
        {
            _fastCache = new FastCacheEngine(Convert.ToInt32(numericUpDownTimeout.Value), _slowCache);
        }

        private void buttonInitCache_Click(object sender, EventArgs e)
        {
            InitCache();
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            textBoxValue.Text = _fastCache.GetValue(textBoxName.Text);
        }
    }
}
