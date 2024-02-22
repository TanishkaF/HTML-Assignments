using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using DemoUserManagement.UtilityLayer;
using System.Text;


namespace DemoUserManagement.web
{
    public partial class UserDetailsV2 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                if (!string.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    if (int.TryParse(Request.QueryString["StudentID"], out int studentID))
                    {
                        NoteUserControlV2.ObjectID = studentID;
                        NoteUserControlV2.ObjectType = NoteType.ObjectType;
                        DocumentUserControlV2.ObjectID = studentID;
                        DocumentUserControlV2.ObjectType = StudentDocumentType.ObjectType;
                    }
                }
            }
        }
         
        [WebMethod]
        public static bool CheckEmail(string email,int userID)
        {
            return AuthenticationServiceBusiness.CheckEmailExists(email, userID);
        }

        [WebMethod]
        public static UserDetailsViewModel GetStudentDetails(int studentID)
        {
            if (CheckAuthentication(studentID))
            {
                UserDetailsViewModel studentDetails = UserBusiness.GetUserDetails(studentID);
                return studentDetails;
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public static AddressDetailViewModel GetAddressDetails(int studentID, int addressType)
        {
            if (CheckAuthentication(studentID))
            {
                switch (addressType)
                {
                    case 1:
                        return UserBusiness.GetCurrentAddress(studentID);
                    case 2:
                        return UserBusiness.GetPermanentAddress(studentID);
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public static string GetCountryName(int countryID)
        {
            return UserBusiness.GetCountryName(countryID);
        }

        [WebMethod]
        public static string GetStateName(int stateID)
        {
            return UserBusiness.GetStateName(stateID);
        }

        [WebMethod]
        public static EducationDetailViewModel GetEducationDetail(int studentID, string educationType)
        {
            if (CheckAuthentication(studentID))
            {
                switch (educationType)
                {
                    case "10":
                        return UserBusiness.GetEducation10(studentID);
                    case "12":
                        return UserBusiness.GetEducation12(studentID);
                    case "graduate":
                        return UserBusiness.GetEducationGraduate(studentID);
                    default:
                        return null;
                }

            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public static UserDetailsViewModel GetUserDetails(int userID)
        {
            if (CheckAuthentication(userID))
            {
                return UserBusiness.GetUserDetails(userID);
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        public static List<CountryViewModel> GetCountry()
        {
            return UserBusiness.GetCountries();           
        } 

        [WebMethod]
        public static List<StateViewModel> GetStates(int countryID)
        {
            return UserBusiness.GetStates(countryID);            
        }

        [WebMethod]
        public static void InsertDetails(UserDetailsViewModel userDetails)
        {
            UserBusiness.InsertUserDetails(userDetails);
        }

        [WebMethod]
        public static void InsertAddress(AddressDetailViewModel addressDetailView, int addressType)
        {
            addressDetailView.AddressType = addressType;
            UserBusiness.InsertAddressDetails(addressDetailView);
        }

        [WebMethod]
        public static void InsertEducationDetails(EducationDetailViewModel educationDetails, int educationType)
        {
            educationDetails.EducationType = educationType;
            UserBusiness.InsertEducationDetails(educationDetails);
        }


        [WebMethod]
        public static void UpdateUserDetails(int userID, UserDetailsViewModel userDetails)
        {
            if (CheckAuthentication(userID))
            {
                UserBusiness.UpdateUserDetails(userID, userDetails);
            }
        }

        [WebMethod]
        public static void UpdateAddressDetails(int userID, int addressType, AddressDetailViewModel addressDetailView)
        {
            if (CheckAuthentication(userID))
            {
                UserBusiness.UpdateAddressDetails(userID, addressType, addressDetailView);
            }
        }

        [WebMethod]
        public static void UpdateEducationDetails(int userID, int educationType, EducationDetailViewModel educationDetails)
        {
            if (CheckAuthentication(userID))
            {
                UserBusiness.UpdateEducationDetails(userID, educationType, educationDetails);
            }
        }

        [WebMethod]
        public static void CopyAddress(string cCountry, string cState, string c1Address, string c2Address, string cPinCode)
        {
            HttpContext.Current.Session["PrevPCountry"] = cCountry;
            HttpContext.Current.Session["PrevPState"] = cState;
            HttpContext.Current.Session["PrevP1Address"] = c1Address;
            HttpContext.Current.Session["PrevP2Address"] = c2Address;
            HttpContext.Current.Session["PrevPPinCode"] = cPinCode;
        }

      
    }
}