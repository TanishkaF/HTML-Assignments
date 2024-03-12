using AirportFuelManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static AirportFuelManagement.ViewModel.AllViewModel;
using System.Web.Mvc;

namespace AirportFuelManagement.Attribute
{

    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //LogInSessionModel logInSessionModel = ConstantValues.UserSessionInfo();
            LogInSessionModel logInSessionModel = ConstantValues.UserSessionInfo;

            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            if (logInSessionModel == null)
            {
                 if (actionName != "loginview")
                {
                    filterContext.Result = new RedirectResult("~/LogIn/LogInView");
                }
            }
        }
    }
}