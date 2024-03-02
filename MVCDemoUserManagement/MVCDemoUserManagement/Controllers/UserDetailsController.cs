using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MVCDemoUserManagement.Controllers
{
    public class UserDetailsController : Controller
    {
        [HttpPost]
        public ActionResult UserDetails()
        {
            var userDetailsList = new List<AllViewModel.UserDetailsViewModel>();

            var allViewModel = new AllViewModel
            {
                UserDetails = userDetailsList.FirstOrDefault()
            };

            CommonFunction();

            return View(allViewModel);
        }

        [SessionCheck]
        [HttpPost]
        public ActionResult SubmitForm(AllViewModel model, int? studentID)
        {
            string currentUrl = Request.Url.ToString();

            if (currentUrl.Contains("/Details") && Request.QueryString["id"] != null)
            {
                studentID = Convert.ToInt32(Request.QueryString["id"]);
            }

            int id = (int)studentID;

            if (SessionCheckAttribute.CheckAuthentication(id))
            {
                if (ModelState.IsValid)
                {
                    model.UserDetails.Hobbies = HobbySelected();
                    UserBusiness.SubmitDetails(id, model);
                }
            }

            CommonFunction();


            if (ConstantValues.GetUserSessionInfo().IsAdmin)
            {
                return RedirectToAction("GetUsers", "UserList");

            }
            else
            {
                return RedirectToAction("UserDetails", "UserDetails", new { studentid = ConstantValues.GetUserSessionInfo().UserID });
            }
            // return View("UserDetails", model);
        }

        [SessionCheck]
        [HttpGet]
        public ActionResult UserDetails(int studentID)
        {
            int id = studentID;
            CommonFunction();

            if (id > 0)
            {
                AllViewModel allViewModel = UserBusiness.UserDetails(id);

                string hobbies = allViewModel.UserDetails.Hobbies.ToString();
                string[] hobbyArray = hobbies.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string hobby in hobbyArray)
                {
                    switch (hobby)
                    {
                        case "Dancing":
                            ViewBag.checkbox1Checked = true;
                            break;
                        case "Singing":
                            ViewBag.checkbox2Checked = true;
                            break;
                        case "Coding":
                            ViewBag.checkbox3Checked = true;
                            break;
                        case "Web Designing":
                            ViewBag.checkbox4Checked = true;
                            break;
                        case "Board Games":
                            ViewBag.checkbox5Checked = true;
                            break;
                        case "Camping":
                            ViewBag.checkbox6Checked = true;
                            break;
                        case "Running":
                            ViewBag.checkbox7Checked = true;
                            break;
                        case "Sleeping":
                            ViewBag.checkbox8Checked = true;
                            break;
                        case "Reading":
                            ViewBag.checkbox9Checked = true;
                            break;
                    }
                }
                return View("UserDetails", allViewModel);
            }
            else
            {
                AllViewModel allViewModel = UserBusiness.UserDetails(id);
                return View("UserDetails", allViewModel);
            }
        }

        [HttpPost]
        public ActionResult ResetForm(AllViewModel model)
        {
            model.UserDetails.FirstName = "";
            model.UserDetails.MiddleName = "";
            model.UserDetails.LastName = "";
            model.UserDetails.Email = "";
            model.UserDetails.Password = "";
            model.UserDetails.ConfirmPassword = "";
            model.UserDetails.DateOfBirth = "";
            model.UserDetails.Gender = "";
            model.UserDetails.Phone = "";
            model.UserDetails.AadharNumber = "";

            model.AddressDetail.AddressLine1 = "";
            model.AddressDetail.AddressLine2 = "";
            model.AddressDetail.Pincode = "";

            model.EducationDetail.InstituteName = "";
            model.EducationDetail.Board = "";
            model.EducationDetail.Marks = "";
            model.EducationDetail.Aggregate = null;
            model.EducationDetail.YearOfCompletion = null;

            return RedirectToAction("UserDetails");
        }

        [HttpPost]
        public JsonResult CheckEmail(string email, int userID)
        {
            bool emailExists = AuthenticationServiceBusiness.CheckEmailExists(email, userID);
            return Json(emailExists);
        }

        private void CommonFunction()
        {
            var countries = GetCountries();
            if (countries != null)
            {
                ViewBag.Countries = countries;
            }
            else
            {
                ViewBag.Countries = new List<CountryViewModel>();
            }
            ViewBag.checkbox1Checked = false;
            ViewBag.checkbox2Checked = false;
            ViewBag.checkbox3Checked = false;
            ViewBag.checkbox4Checked = false;
            ViewBag.checkbox5Checked = false;
            ViewBag.checkbox6Checked = false;
            ViewBag.checkbox7Checked = false;
            ViewBag.checkbox8Checked = false;
            ViewBag.checkbox9Checked = false;
        }

        public List<CountryViewModel> GetCountries()
        {
            List<CountryViewModel> countries = UserBusiness.GetCountries();
            return countries;
        }

        public ActionResult GetStates(int countryId)
        {
            List<StateViewModel> states = UserBusiness.GetStates(countryId);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        private string HobbySelected()
        {
            StringBuilder selectedHobbies = new StringBuilder();

            if (Request.Form["checkbox1"] != null)
            {
                selectedHobbies.Append("Dancing, ");
            }
            if (Request.Form["checkbox2"] != null)
            {
                selectedHobbies.Append("Singing, ");
            }
            if (Request.Form["checkbox3"] != null)
            {
                selectedHobbies.Append("Coding, ");
            }
            if (Request.Form["checkbox4"] != null)
            {
                selectedHobbies.Append("Web Designing, ");
            }
            if (Request.Form["checkbox5"] != null)
            {
                selectedHobbies.Append("Board Games, ");
            }
            if (Request.Form["checkbox6"] != null)
            {
                selectedHobbies.Append("Camping, ");
            }
            if (Request.Form["checkbox7"] != null)
            {
                selectedHobbies.Append("Running, ");
            }
            if (Request.Form["checkbox8"] != null)
            {
                selectedHobbies.Append("Sleeping, ");
            }
            if (Request.Form["checkbox9"] != null)
            {
                selectedHobbies.Append("Reading, ");
            }

            if (selectedHobbies.Length > 0)
            {
                selectedHobbies.Remove(selectedHobbies.Length - 2, 2);
            }

            return selectedHobbies.ToString();
        }

    }
}