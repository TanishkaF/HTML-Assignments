using DemoUserManagement.BusinessLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using DemoUserManagement.UtilityLayer;
using Microsoft.Ajax.Utilities;




namespace DemoUserManagement.web
{
    public partial class UserDetailsV2 : System.Web.UI.Page
    {
        private bool authorizationChecked = false;
        private UserDetailsViewModel userDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }



        }

        [WebMethod]
        public static void ResetButton_Click()
        {
        }

        [WebMethod]
        public static UserDetailsViewModel GetStudentDetails(int studentID)
        {
            UserDetailsViewModel studentDetails = UserBusiness.GetUserDetails(studentID);
            return studentDetails;
        }

        [WebMethod]
        public static bool UpdateStudentDetails(int studentID, UserDetailsViewModel studentDetails)
        {
            return true;
        }

        [WebMethod]
        public static bool CheckEmail(string email)
        {
            return AuthenticationServiceBusiness.CheckEmailExists(email);
        }


        [WebMethod]
        public static string GetSelectedHobbies(bool[] checkboxes)
        {

            string selectedHobbies = "";

            for (int i = 0; i < checkboxes.Length; i++)
            {
                if (checkboxes[i])
                {
                    selectedHobbies += "Hobby " + (i + 1) + ", ";
                }
            }


            if (!string.IsNullOrEmpty(selectedHobbies))
            {
                selectedHobbies = selectedHobbies.TrimEnd(',', ' ');
            }

            return selectedHobbies;
        }

        [WebMethod]
        public static AddressDetailViewModel GetAddressDetails(int studentID, int addressType)
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

        [WebMethod]
        public static EducationDetailViewModel GetEducationDetail(int studentID, string educationType)
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

        [WebMethod]
        public static UserDetailsViewModel GetUserDetails(int userID)
        {
            return UserBusiness.GetUserDetails(userID);
        }

        //protected void UpdateUserDetails(int userID)
        //{
        //    Response.Redirect($"UserDetailsV2.aspx?StudentID={userID}");
        //    // Response.Redirect($"UserList.aspx");
        //}


        [WebMethod]
        public static string GetCountries()
        {
            List<CountryViewModel> countryList = UserBusiness.GetCountries();
            string json = JsonConvert.SerializeObject(countryList);
            return json;
        }

        [WebMethod]
        public static string GetStates(int countryID)
        {
            List<StateViewModel> stateList = UserBusiness.GetStates(countryID);
            string stateJson = JsonConvert.SerializeObject(stateList);
            return stateJson;
        }


        [WebMethod]
        public static void GetLastInsertedUserID()
        {
            UserBusiness.GetLastInsertedUserID();
        }


        [WebMethod]
        public static void InsertDetails(UserDetailsViewModel userDetails)
        {

            UserBusiness.InsertUserDetails(userDetails);
            // UserBusiness.InsertAddressDetails(addressDetailsCurrent);
            //UserBusiness.InsertAddressDetails(addressDetailsPermanent);           
        }

        [WebMethod]
        public static void InsertEducationDetails(EducationDetailViewModel educationDetails, int educationType)
        {
            educationDetails.EducationType = educationType;
            UserBusiness.InsertEducationDetails(educationDetails);
        }


        [WebMethod]
        public static void InsertAddress(AddressDetailViewModel addressDetailView, int addressType)
        {
            addressDetailView.AddressType = addressType;
            UserBusiness.InsertAddressDetails(addressDetailView);
        }

        [WebMethod]
        public static void UpdateUserDetails(int userID, UserDetailsViewModel userDetails)
        {
             UserBusiness.UpdateUserDetails(userID, userDetails);
        }

        [WebMethod]
        public static void UpdateAddressDetails(int userID,int addressType,AddressDetailViewModel addressDetailView)
        {
             UserBusiness.UpdateAddressDetails(userID,addressType,addressDetailView);
        }

         [WebMethod]
        public static void UpdateEducationDetails(int userID,int educationType,AddressDetailViewModel educationDetails)
        {
             UserBusiness.UpdateAddressDetails(userID, educationType, educationDetails);
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

        [WebMethod]
        public static int getLastInsertedUserID()
        {
            return UserBusiness.GetLastInsertedUserID();
        }



    }
}