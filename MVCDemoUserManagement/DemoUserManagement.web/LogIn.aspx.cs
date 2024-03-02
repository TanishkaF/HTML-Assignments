using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement.web
{
    public partial class LogIn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static LogInSessionModel ValidateUser(string email, string password)
        {
            if (AuthenticationServiceBusiness.ValidateUser(email, password))
            {
                LogInSessionModel userSession = new LogInSessionModel
                {
                    UserID = AuthenticationServiceBusiness.GetUserID(email),
                    IsAdmin = AuthenticationServiceBusiness.IsAdmin(email)
                };

                UtilityLayer.ConstantValues.SetUserSessionInfo(userSession);
                return userSession; 
            }
            return null;
        }
    }
}