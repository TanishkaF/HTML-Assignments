using System;

namespace AspWebAppPratice2
{
    public partial class ViewTest : System.Web.UI.Page
    {       
            public string a, b;

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["name"] = TextBox1.Text;
            ViewState["password"] = TextBox2.Text;
            //a = TextBox1.Text;
            //b = TextBox2.Text;
            TextBox1.Text = TextBox2.Text = string.Empty;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (ViewState["name"] != null)
            {
                TextBox1.Text = ViewState["name"].ToString();
            }

            if (ViewState["password"] != null)
            {
                TextBox2.Text = ViewState["password"].ToString();
            }
            //TextBox1.Text = a;
            //TextBox2.Text = b;
        }
     }
}