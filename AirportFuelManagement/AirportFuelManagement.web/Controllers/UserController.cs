using AirportFuelManagement.BusinessLayer;
using AirportFuelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirportFuelManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(AllViewModel.User user)
        {
            if (ModelState.IsValid)
            {
                if (AuthenticationServiceBL.AddUser(user))
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to add user to the database." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid user data. Please check the provided information." });
            }
        }

        [HttpPost]
        public JsonResult CheckEmail(string email, int userID)
        {
            bool emailExists = AuthenticationServiceBL.CheckEmailExists(email, userID);
            return Json(emailExists);
        }

    }
}