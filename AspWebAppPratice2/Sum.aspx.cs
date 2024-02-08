using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspWebAppPratice2
{
    public partial class Sum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddNumber(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(input1.Text);
            int b = Convert.ToInt32(input2.Text);

            int sum = a + b;
            showSum.Text = sum.ToString();
        }
    }
}