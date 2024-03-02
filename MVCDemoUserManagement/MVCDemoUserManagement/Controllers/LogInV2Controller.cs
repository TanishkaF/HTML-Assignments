using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System.Web.Mvc;

namespace MVCDemoUserManagement.Controllers
{
    public class LogInV2Controller : Controller
    {
        // GET: LogInV2
        public ActionResult LogInV2()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidateUserV2(string email, string password)
        {
            bool isValidUser = AuthenticationServiceBusiness.ValidateUser(email, password);

            if (isValidUser)
            {
                LogInSessionModel userSession = new LogInSessionModel
                {
                    UserID = AuthenticationServiceBusiness.GetUserID(email),
                    IsAdmin = AuthenticationServiceBusiness.IsAdmin(email)
                };

                ConstantValues.SetUserSessionInfo(userSession);

                if (userSession.IsAdmin)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true });
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Invalid combination of Email ID with Password." });
            }
        }
    }
}
