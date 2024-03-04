using DemoUserManagement.BusinessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace MVCDemoUserManagement.Controllers
{
    public class UserDetailsV2Controller : Controller
    {
        // GET: UserDetailsV2
        public ActionResult UserDetailsV2()
        {
            return View();
        }

        public string GetCountriesJson()
        {
            List<CountryViewModel> countries = UserBusiness.GetCountries();
            return JsonConvert.SerializeObject(countries);
        }

        public string GetStatesJson(int countryId)
        {
            List<StateViewModel> states = UserBusiness.GetStates(countryId);
            return JsonConvert.SerializeObject(states);
        }

        [SessionCheckV2]
        [HttpPost]
        public ActionResult CheckAdmin()
        {
            bool isAdmin = false;
            return Json(new { isAdmin = isAdmin });
        }

        [HttpPost]
        public ActionResult ResetForm()
        {
            return Json(new { success = true });
        }

        [HttpPost]
        [SessionCheckV2]
        public JsonResult GetStudentDetails(int studentID)
        {
            try
            {
                UserDetailsViewModel studentDetails = UserBusiness.GetUserDetails(studentID);

                if (studentDetails == null)
                {
                    return Json(new { success = false, message = "Student details not found" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, studentDetails = studentDetails }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "An error occurred while retrieving student details" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult GetAddressDetails(int studentID, int addressType)
        {
            try
            {
                if (!SessionCheckV2Attribute.CheckAuthenticationV2(studentID))
                {
                    switch (addressType)
                    {
                        case 1:
                            return Json(new { addressDetails = UserBusiness.GetCurrentAddress(studentID) });
                        case 2:
                            return Json(new { addressDetails = UserBusiness.GetPermanentAddress(studentID) });
                        default:
                            return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }

            return Json(null);
        }



        public JsonResult GetEducationDetail(int studentID, string educationType)
        {
            try
            {
                if (!SessionCheckV2Attribute.CheckAuthenticationV2(studentID))
                {
                    switch (educationType)
                    {
                        case "10":
                            return Json(UserBusiness.GetEducation10(studentID));
                        case "12":
                            return Json(UserBusiness.GetEducation12(studentID));
                        case "graduate":
                            return Json(UserBusiness.GetEducationGraduate(studentID));
                        default:
                            return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
            return Json(new { success = false, message = "Unauthorized access" });
        }


        public static UserDetailsViewModel GetUserDetails(int userID)
        {
            try
            {
                if (SessionCheckV2Attribute.CheckAuthenticationV2(userID))
                {
                    return UserBusiness.GetUserDetails(userID);
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
            return null;
        }


        public static List<CountryViewModel> GetCountry()
        {
            return UserBusiness.GetCountries();
        }

        public static List<StateViewModel> GetStates(int countryID)
        {
            return UserBusiness.GetStates(countryID);
        }

        [HttpPost]
        public ActionResult InsertDetails(UserDetailsViewModel userDetails)
        {
            try
            {
                UserBusiness.InsertUserDetails(userDetails);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "Error occurred while inserting user details" });
            }
        }

        [HttpPost]
        public ActionResult InsertAddress(AddressDetailViewModel addressDetailView, int addressType)
        {
            try
            {
                addressDetailView.AddressType = addressType;
                UserBusiness.InsertAddressDetails(addressDetailView);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "Error occurred while inserting address details" });
            }
        }

        [HttpPost]
        public ActionResult InsertEducationDetails(EducationDetailViewModel educationDetails, int educationType)
        {
            try
            {
                educationDetails.EducationType = educationType;
                UserBusiness.InsertEducationDetails(educationDetails);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "Error occurred while inserting education details" });
            }
        }


        [HttpPost]
        public ActionResult UpdateUserDetails(int userID, UserDetailsViewModel userDetails)
        {
            try
            {
                if (SessionCheckV2Attribute.CheckAuthenticationV2(userID))
                {
                    UserBusiness.UpdateUserDetails(userID, userDetails);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "Error occurred while updating user details" });
            }
        }

        [HttpPost]
        public ActionResult UpdateAddressDetails(int userID, int addressType, AddressDetailViewModel addressDetailView)
        {
            try
            {
                if (SessionCheckV2Attribute.CheckAuthenticationV2(userID))
                {
                    UserBusiness.UpdateAddressDetails(userID, addressType, addressDetailView);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "Error occurred while updating address details" });
            }
        }

        [HttpPost]
        public ActionResult UpdateEducationDetails(int userID, int educationType, EducationDetailViewModel educationDetails)
        {
            try
            {
                if (SessionCheckV2Attribute.CheckAuthenticationV2(userID))
                {
                    UserBusiness.UpdateEducationDetails(userID, educationType, educationDetails);
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return Json(new { success = false, message = "Error occurred while updating education details" });
            }
        }



        //public static void CopyAddress(string cCountry, string cState, string c1Address, string c2Address, string cPinCode)
        //{
        //    HttpContext.Current.Session["PrevPCountry"] = cCountry;
        //    HttpContext.Current.Session["PrevPState"] = cState;
        //    HttpContext.Current.Session["PrevP1Address"] = c1Address;
        //    HttpContext.Current.Session["PrevP2Address"] = c2Address;
        //    HttpContext.Current.Session["PrevPPinCode"] = cPinCode;
        //}

        public class UploadResponse
        {
            public string DiskDocumentName { get; set; }
            public string OriginalFileName { get; set; }
        }

        [HttpPost]
        public ActionResult ProcessRequest(HttpContextBase context)
        {
            var response = new UploadResponse();

            if (context.Request.Files.Count > 0)
            {
                HttpPostedFileBase uploadedFile = context.Request.Files[0];

                string uploadedFileName = UploadFileToServer(uploadedFile);

                if (!string.IsNullOrEmpty(uploadedFileName))
                {
                    response.DiskDocumentName = uploadedFileName;
                    response.OriginalFileName = uploadedFile.FileName;
                }
                else
                {
                }
            }
            else
            {
            }

            return Json(response);
        }



        public static string UploadFileToServer(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                try
                {
                    string uploadFolderPath = ConfigurationManager.AppSettings["UploadFolderPath"];

                    string fileName = Path.GetFileName(uploadedFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    string filePath = Path.Combine(uploadFolderPath, uniqueFileName);
                    uploadedFile.SaveAs(filePath);
                    return uniqueFileName;
                }
                catch (Exception ex)
                {
                    Logger.AddData(ex);
                    return null;
                }
            }
            return null;
        }

        [HttpPost]
        public JsonResult CheckEmail(string email, int userID)
        {
            bool emailExists = AuthenticationServiceBusiness.CheckEmailExists(email, userID);
            return Json(emailExists);
        }


    }
}