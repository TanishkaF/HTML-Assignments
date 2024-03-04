using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemoUserManagement
{
    public class SessionCheckV2Attribute : ActionFilterAttribute
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

                else if (actionName != "loginv2") 
                {
                    filterContext.Result = new RedirectResult("~/LogInV2/LogInV2");
                }
            }
            else if (actionName == "loginv2")
            {
                if (logInSessionModel.IsAdmin)
                {
                    filterContext.Result = new RedirectResult("~/UserListV2/GetUsersV2");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/UserDetailsV2/UserDetailsV2?StudentID" + logInSessionModel.UserID);
                }
            }
            else if (actionName == "getusersv2" && !logInSessionModel.IsAdmin)
            {
                filterContext.Result = new RedirectResult("~/LogInV2/LogInV2");
            }
            else if (string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.QueryString["StudentID"]))
            {
                CheckAuthorizationAndLoadUserDetails(filterContext);
            }
            else if (actionName == "userdetailsv2" && !logInSessionModel.IsAdmin)
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
                        filterContext.Result = new RedirectResult("~/UserDetailsV2/UserDetailsV2?StudentID=" + authenticatedUserID);
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/UserDetailsV2/UserDetailsV2?StudentID=0");
                }
                authorizationChecked = true;
            }
        }

        public static bool CheckAuthenticationV2(int userID)
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

