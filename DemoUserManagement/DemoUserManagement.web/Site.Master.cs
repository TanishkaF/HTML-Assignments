using System;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.IO;

namespace DemoUserManagement.web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            currentFormTitle.Text = Page.Title;                   
        }
    }
}
