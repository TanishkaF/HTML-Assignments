using System;
using System.Collections.Generic;
using DemoUserManagement.DataAccessLayer;
using DemoUserManagement.ViewModel;

namespace DemoUserManagement.BusinessLayer
{
    public class UserBusiness
    {
        public static int GetLastInsertedUserID()
        {
            return UserDetailsDataAccess.GetLastInsertedUserID();
        }

        public static List<CountryViewModel> GetCountries()
        {
            return UserDetailsDataAccess.GetCountries();
        }

        public static List<StateViewModel> GetStates(int countryID)
        {
            return UserDetailsDataAccess.GetStates(countryID);
        }

       
            public static void CopyAddress(int userID, bool sameAsCurrent)
            {
                // Get current address details
                AddressDetailViewModel currentAddress = GetAddressDetails(userID, 1);
            // Insert current address details
                 UserDetailsDataAccess.InsertAddressDetails(currentAddress);

                if (!sameAsCurrent)
                {
                    // Get permanent address details
                    AddressDetailViewModel permanentAddress = GetAddressDetails(userID, 2);
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

            public static AddressDetailViewModel GetAddressDetails(int userID, int addressType)
            {
                // Implement logic to fetch address details based on userID and addressType
                AddressDetailViewModel addressDetails = new AddressDetailViewModel();
                // Populate addressDetails with data from database
                // Example:
                // addressDetails = AddressDAL.GetAddressDetails(userID, addressType);
                return addressDetails;
            }

            public static void InsertAddressDetails(AddressDetailViewModel addressDetails)
            {
            // Implement logic to insert address details into the database
            // Example:
            // AddressDAL.InsertAddressDetails(addressDetails);
            UserDetailsDataAccess.InsertAddressDetails(addressDetails);
            }
        



        public static void InsertStudentDetails(StudentDetailViewModel studentDetails)
        {
            UserDetailsDataAccess.InsertStudentDetails(studentDetails);
        }

        //public static void InsertAddressDetails(AddressDetailViewModel addressDetails)
        //{
        //    UserDetailsDataAccess.InsertAddressDetails(addressDetails);
        //}

        public static void InsertEducationDetails(EducationDetailViewModel educationDetailViewModel)
        {
            UserDetailsDataAccess.InsertEducationDetails(educationDetailViewModel);
        }

        public static void UpdateStudentDetails(int userID, StudentDetailViewModel studentDetails)
        {
            UserDetailsDataAccess.UpdateStudentDetails(userID, studentDetails);
        }

        public static void UpdateAddressDetails(int userID, int addressType, AddressDetailViewModel addressDetails)
        {
            UserDetailsDataAccess.UpdateAddressDetails(userID, addressType, addressDetails);
        }

        public static void UpdateEducationDetails(int userID, int educationType, EducationDetailViewModel educationDetails)
        {
            UserDetailsDataAccess.UpdateEducationDetails(userID, educationType, educationDetails);
        }

        public static StudentDetailViewModel GetStudentDetails(int userID)
        {
            return UserDetailsDataAccess.GetStudentDetails(userID);
        }

        public static AddressDetailViewModel GetCurrentAddress(int userID)
        {
            return UserDetailsDataAccess.GetCurrentAddress(userID);
        }

        public static AddressDetailViewModel GetPermanentAddress(int userID)
        {
            return UserDetailsDataAccess.GetPermanentAddress(userID);
        }

        public static EducationDetailViewModel GetEducation10(int userID)
        {
            return UserDetailsDataAccess.GetEducation10(userID);
        }

        public static EducationDetailViewModel GetEducation12(int userID)
        {
            return UserDetailsDataAccess.GetEducation12(userID);
        }

        public static EducationDetailViewModel GetEducationGraduate(int userID)
        {
            return UserDetailsDataAccess.GetEducationGraduate(userID);
        }
    }
}
