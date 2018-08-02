using System;
using System.Windows.Forms;

namespace ApprendreLeFrançais
{
    public partial class FrenchLetters : UserControl
    {
        public class PressLetterEventArgs
        {
            public PressLetterEventArgs(string s) { Text = s; }
            public string Text { get; private set; } // readonly
        }

        public delegate void PressLetterEventHandler(object sender, PressLetterEventArgs letter);

        public event PressLetterEventHandler PressLetterEvent;

        protected virtual void RaisePressLetterEvent(string text)
        {
            if (PressLetterEvent != null)
            {
                PressLetterEvent(this, new PressLetterEventArgs(text));
            }
        }

        public FrenchLetters()
        {
            InitializeComponent();
        }

        private void buttonLetter_Click(object sender, EventArgs e)
        {
            RaisePressLetterEvent(((Button)sender).Text);
        }

        private void InitiateButtons()
        {
            buttonGrave.FlatStyle = buttonAigu.FlatStyle = buttonCirconflexe.FlatStyle = buttonTrema.FlatStyle = FlatStyle.Standard;
            buttonGrave.Enabled = buttonA.Enabled = buttonI.Enabled = buttonO.Enabled = buttonU.Enabled = true;
            buttonA.Text = "a";
            buttonE.Text = "e";
            buttonI.Text = "i";
            buttonO.Text = "o";
            buttonU.Text = "u";
        }

        private void buttonGrave_Click(object sender, EventArgs e)
        {
            var saveFlatStyle = buttonGrave.FlatStyle;
            InitiateButtons();

            if (saveFlatStyle == FlatStyle.Standard)
            {
                buttonGrave.FlatStyle = FlatStyle.Flat;
                buttonA.Enabled = buttonI.Enabled = buttonO.Enabled = buttonU.Enabled = false;
                buttonE.Text = Letters.é;
            }
        }

        private void buttonAigu_Click(object sender, EventArgs e)
        {
            var saveFlatStyle = buttonAigu.FlatStyle;
            InitiateButtons();

            if (saveFlatStyle == FlatStyle.Standard)
            {
                buttonAigu.FlatStyle = FlatStyle.Flat;
                buttonI.Enabled = buttonO.Enabled = false;
                buttonA.Text = Letters.à;
                buttonE.Text = Letters.è;
                buttonU.Text = Letters.ù;
            }
        }

        private void buttonCirconflexe_Click(object sender, EventArgs e)
        {
            var saveFlatStyle = buttonCirconflexe.FlatStyle;
            InitiateButtons();

            if (saveFlatStyle == FlatStyle.Standard)
            {
                buttonCirconflexe.FlatStyle = FlatStyle.Flat;
                buttonA.Text = Letters.â;
                buttonE.Text = Letters.ê;
                buttonI.Text = Letters.î;
                buttonO.Text = Letters.ô;
                buttonU.Text = Letters.û;
            }
        }

        private void buttonTrema_Click(object sender, EventArgs e)
        {
            var saveFlatStyle = buttonTrema.FlatStyle;
            InitiateButtons();

            if (saveFlatStyle == FlatStyle.Standard)
            {
                buttonTrema.FlatStyle = FlatStyle.Flat;
                buttonA.Enabled = buttonO.Enabled = false;
                buttonE.Text = Letters.ë;
                buttonI.Text = Letters.ï;
                buttonU.Text = Letters.ü;
            }
        }
    }
}
