using DemoUserManagement.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static void SetSessionVariables(string email)
        {
            int userID = GetLoggedInUserID(email);
            HttpContext.Current.Session["AuthenticatedUserID"] = userID;

            bool isAdmin = IsAdmin(email);
            HttpContext.Current.Session["IsAdmin"] = isAdmin;
        }

        [WebMethod]
        public static bool ValidateUser(string email, string password)
        {
           return AuthenticationServiceBusiness.ValidateUser(email, password);
        }

        [WebMethod]
        public static bool CheckEmail(string email)
        {
            return AuthenticationServiceBusiness.CheckEmailExists(email);
        }

        [WebMethod]
        public static bool IsAdmin(string email)
        {
            return AuthenticationServiceBusiness.IsAdmin(email);
        }

        [WebMethod]
        public static int GetUserID(string email)
        {
            return AuthenticationServiceBusiness.GetUserID(email);
        }

        [WebMethod]
        public static int GetLoggedInUserID(string email)
        {
            return AuthenticationServiceBusiness.GetUserID(email);
        }
    }
}
