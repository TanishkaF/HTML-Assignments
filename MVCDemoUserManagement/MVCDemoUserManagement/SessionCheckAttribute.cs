using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;

namespace MVCDemoUserManagement
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        private bool authorizationChecked = false;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            authorizationChecked = false;
            LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            if (logInSessionModel == null)
            {
                if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString["StudentID"]) && filterContext.HttpContext.Request.QueryString["StudentID"] == "0")
                {
                    // Your code here
                }

                else if (actionName != "index") // Assuming "Index" action is the login page
                {
                    filterContext.Result = new RedirectResult("~/LogIn/Index");
                }
            }
            else if(actionName == "index")
            {
                if (logInSessionModel.IsAdmin)
                {
                    filterContext.Result = new RedirectResult("~/UserList/GetUsers");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/UserDetails/UserDetails?StudentID"+logInSessionModel.UserID);
                }
            }
            else if (actionName == "getusers" && !logInSessionModel.IsAdmin)
            {
                filterContext.Result = new RedirectResult("~/LogIn/Index");
            }
            else if (string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString["StudentID"]))
            {
                CheckAuthorizationAndLoadUserDetails(filterContext);
            }
            else if(actionName=="userdetails" && !logInSessionModel.IsAdmin)
            {
                CheckAuthorizationAndLoadUserDetails(filterContext);
            }        
        }

        private void CheckAuthorizationAndLoadUserDetails(ActionExecutingContext filterContext)
        {
            LogInSessionModel userSessionInfo = ConstantValues.GetUserSessionInfo();
            if (!authorizationChecked)
            {
                if (userSessionInfo != null)
                {
                    int authenticatedUserID = userSessionInfo.UserID;
                    bool isAdmin = userSessionInfo.IsAdmin;

                    bool urlParsedSuccessfully = int.TryParse(filterContext.HttpContext.Request.QueryString["StudentID"], out int urlUpdatedStudentID);

                    if (isAdmin || (urlParsedSuccessfully && urlUpdatedStudentID == authenticatedUserID))
                    {
                        // Allow admin or the authenticated user to access the requested data
                        // No redirection necessary here
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/UserDetails/UserDetails?studentid=" + authenticatedUserID);
                        
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/UserDetails/UserDetails?studentid=0");
                }
                authorizationChecked = true;
            }
        }

        public static bool CheckAuthentication(int userID)
        {
            LogInSessionModel logInSessionModel = ConstantValues.GetUserSessionInfo();
            if (userID == logInSessionModel.UserID || logInSessionModel.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}