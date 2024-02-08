using System;

namespace AspWebAppPratice2
{
    public partial class Comapre : System.Web.UI.Page
    {
        protected void clickCompare(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                showSum.Text = "Valid: First value is less than the second one";
            }           
        }
    }
}