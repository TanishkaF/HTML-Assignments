using System.Collections.Generic;
using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;

namespace DemoUserManagement.BusinessLayer
{
    public class UserBusiness
    {

        public static List<CountryViewModel> GetCountries()
        {
            return UserDetailsDataAccess.GetCountries();
        }       

        public static List<StateViewModel> GetStates(int countryID)
        {
            return UserDetailsDataAccess.GetStates(countryID);
        }

        public static string GetStateName(int stateID)
        {
            return UserDetailsDataAccess.GetStateName(stateID);
        }

        public static string GetCountryName(int countryID)
        {
            return UserDetailsDataAccess.GetCountryName(countryID);
        }

        public static void CopyAddress(int studentID, bool sameAsCurrent)
        {
            // Get current address details
            AddressDetailViewModel currentAddress = GetAddressDetails(studentID, AddressType.CurrentAddress);   
            // Insert current address details
            UserDetailsDataAccess.InsertAddressDetails(currentAddress);

            if (!sameAsCurrent)
            {
                // Get permanent address details
                AddressDetailViewModel permanentAddress = GetAddressDetails(studentID, AddressType.PermanentAddress);
                // Insert permanent address details
                UserDetailsDataAccess.InsertAddressDetails(permanentAddress);
            }
            else
            {
                // Insert current address details with AddressType 2 (permanent address)
                currentAddress.AddressType = 2;
                UserDetailsDataAccess.InsertAddressDetails(currentAddress);
            }
        }
        
        public static int GetLastInsertedUserID()
        {
            return UserDetailsDataAccess.GetLastInsertedUserID();
        }

        public static void InsertUserDetails(UserDetailsViewModel studentDetails)
        {
            UserDetailsDataAccess.InsertUserDetails(studentDetails);
        }

        public static void InsertAddressDetails(AddressDetailViewModel addressDetails)
        {
            UserDetailsDataAccess.InsertAddressDetails(addressDetails);
        }      

        public static void InsertEducationDetails(EducationDetailViewModel educationDetailViewModel)
        {
            UserDetailsDataAccess.InsertEducationDetails(educationDetailViewModel);
        }

        public static void UpdateUserDetails(int studentID, UserDetailsViewModel studentDetails)
        {
            UserDetailsDataAccess.UpdateUserDetails(studentID, studentDetails);
        }

        public static void UpdateAddressDetails(int studentID, int addressType, AddressDetailViewModel addressDetails)
        {
            UserDetailsDataAccess.UpdateAddressDetails(studentID, addressType, addressDetails);
        }

        public static void UpdateEducationDetails(int studentID, int educationType, EducationDetailViewModel educationDetails)
        {
            UserDetailsDataAccess.UpdateEducationDetails(studentID, educationType, educationDetails);
        }

        public static UserDetailsViewModel GetUserDetails(int studentID)
        {
            return UserDetailsDataAccess.GetUserDetails(studentID);
        }
       
        public static AddressDetailViewModel GetAddressDetails(int studentID, int addressType)
        {
            AddressDetailViewModel addressDetails = new AddressDetailViewModel();          
            return addressDetails;
        }

        public static AddressDetailViewModel GetCurrentAddress(int studentID)
        {
            return UserDetailsDataAccess.GetCurrentAddress(studentID);
        }

        public static AddressDetailViewModel GetPermanentAddress(int studentID)
        {
            return UserDetailsDataAccess.GetPermanentAddress(studentID);
        }

        public static EducationDetailViewModel GetEducation10(int studentID)
        {
            return UserDetailsDataAccess.GetEducation10(studentID);
        }

        public static EducationDetailViewModel GetEducation12(int studentID)
        {
            return UserDetailsDataAccess.GetEducation12(studentID);
        }

        public static EducationDetailViewModel GetEducationGraduate(int studentID)
        {
            return UserDetailsDataAccess.GetEducationGraduate(studentID);
        }    
       
    }
}
