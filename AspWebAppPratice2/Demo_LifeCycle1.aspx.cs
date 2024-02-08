using System;
using System.Web.UI;

namespace AspWebAppPratice2
{
    public partial class Demo_LifeCycle1 : Page
    {       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Assign values to label with line breaks using HTML markup
            lblName.Text = "PreInit" + "<br/>" +
                           "Init" + "<br/>" +
                           "InitComplete" + "<br/>" +
                           "PreLoad";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Append additional lifecycle events on button click
            lblName.Text += "<br/>" + "btnSubmit_Click";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Append additional lifecycle events
            lblName.Text += "<br/>" + "LoadComplete";
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Append additional lifecycle events
            lblName.Text += "<br/>" + "PreRender";
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            // Append additional lifecycle events
            lblName.Text += "<br/>" + "SaveStateComplete";
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            // Append additional lifecycle events
            lblName.Text += "<br/>" + "UnLoad";
        }
    }
}
