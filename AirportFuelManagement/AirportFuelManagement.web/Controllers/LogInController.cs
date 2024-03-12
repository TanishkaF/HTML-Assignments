using AirportFuelManagement.BusinessLayer;
using AirportFuelManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using static AirportFuelManagement.ViewModel.AllViewModel;

namespace AirportFuelManagement.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogInView()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ValidateUser(string email, string password)
        {
            bool isValidUser = AuthenticationServiceBL.ValidateUser(email, password);
            string errorMessage = isValidUser ? "" : "Invalid combination of Email ID with Password.";

            LogInSessionModel userSession = new LogInSessionModel
            {
                UserID = AuthenticationServiceBL.GetUserID(email),
                IsAdmin = AuthenticationServiceBL.IsAdmin(email)
            };

            ConstantValues.UserSessionInfo = userSession;
            // ConstantValues.SetUserSessionInfo(userSession);

            return Json(new { success = isValidUser, errorMessage = errorMessage });
        }

    }
}