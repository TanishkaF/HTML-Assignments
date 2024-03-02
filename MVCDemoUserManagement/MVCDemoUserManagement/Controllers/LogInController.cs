using System.Web.Mvc;
using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using DemoUserManagement.UtilityLayer;
using MVCDemoUserManagement;

namespace YourNamespace.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult ValidateUser(string email, string password)
        {
            if (AuthenticationServiceBusiness.ValidateUser(email, password))
            {
                LogInSessionModel userSession = new LogInSessionModel
                {
                    UserID = AuthenticationServiceBusiness.GetUserID(email),
                    IsAdmin = AuthenticationServiceBusiness.IsAdmin(email)
                };

               ConstantValues.SetUserSessionInfo(userSession);
                if (userSession.IsAdmin)
                {
                    return RedirectToAction("GetUsers", "UserList"); // Redirect admin to userList
                }
                else
                {
                    return RedirectToAction("UserDetails", "UserDetails", new { studentid = userSession.UserID });
                }
            }
            else
            {
                ViewBag.Error = "Invalid combination of Email ID with Password.";
                return View("Index");
            }
        }

        [HttpPost]
        [Route("LogIn/SignUp")]
        public ActionResult SignUp()
        {
            return RedirectToAction("UserDetails", "UserDetails", new { studentid = 0 });
        }


    }
}
