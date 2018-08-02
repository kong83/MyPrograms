using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ServiceWorker
{
    public partial class StartServicesErrorsForm : Form
    {
        private readonly List<string> _errors;

        public StartServicesErrorsForm(List<string> errors)
        {
            InitializeComponent();

            _errors = errors;
        }

        private void StartServicesErrorsForm_Shown(object sender, EventArgs e)
        {
            foreach (var error in _errors)
            {
                listBoxErrors.Items.Add(error);
            }
        }
    }
}
