using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureWebApplicationTest.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureWebApplicationTest.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public void AddValueButton_Click()
        {
            var databaseEngine = new DatabaseEngine();

            databaseEngine.AddValue(textBoxNewValue.Text);
        }

        public void GetValueButton_Click()
        {
            var databaseEngine = new DatabaseEngine();

            labelValue.Text = databaseEngine.GetValue(textBoxId.Text);
        }
    }
}
