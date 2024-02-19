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

            //if (!IsPostBack)
            //{
            //    CheckUserRole();
            //    HideLogoutLink();

            //}
        }

        //private void CheckUserRole()
        //{
        //    if (Session["UserSessionInfo"] != null)
        //    {
        //        var userSessionInfo = (UserSessionInfo)Session["UserSessionInfo"];
        //        UserDetailsLink.Visible = userSessionInfo.IsAdmin;
        //        UsersListLink.Visible = userSessionInfo.IsAdmin;
        //    }
        //    else
        //    {
        //        UserDetailsLink.Visible = false;
        //        UsersListLink.Visible = false;
        //    }
        //}

        //private void HideLogoutLink()
        //{
        //    if (Session["UserSessionInfo"] == null)
        //        LogoutLink.Visible = false;
        //    else
        //        LogoutLink.Visible = true;

        //}
    }

}

