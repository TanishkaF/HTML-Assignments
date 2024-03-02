using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
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
            AddressDetailViewModel currentAddress = GetAddressDetails(studentID, AddressType.CurrentAddress);
            UserDetailsDataAccess.InsertAddressDetails(currentAddress);

            if (!sameAsCurrent)
            {
                AddressDetailViewModel permanentAddress = GetAddressDetails(studentID, AddressType.PermanentAddress);
                UserDetailsDataAccess.InsertAddressDetails(permanentAddress);
            }
            else
            {
                currentAddress.AddressType = AddressType.PermanentAddress;
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

        public static void InsertUserRoll(int userID)
        {
            UserDetailsDataAccess.InsertUserRoll(userID);
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

        public static string GetEmailByUserID(int userID)
        {
            return UserDetailsDataAccess.GetEmailByUserID(userID);
        }


        public static UserDetailsViewModel GetStudentTableDetails(AllViewModel userDetails)
        {
            UserDetailsViewModel studentDetailsTable = new UserDetailsViewModel();
            studentDetailsTable.FirstName = userDetails.UserDetails.FirstName;
            studentDetailsTable.MiddleName = userDetails.UserDetails.MiddleName;
            studentDetailsTable.LastName = userDetails.UserDetails.LastName;
            studentDetailsTable.Email = userDetails.UserDetails.Email;
            studentDetailsTable.Phone = userDetails.UserDetails.Phone;
            studentDetailsTable.AadharNumber = userDetails.UserDetails.AadharNumber;
            studentDetailsTable.DateOfBirth = userDetails.UserDetails.DateOfBirth;
            studentDetailsTable.Gender = userDetails.UserDetails.Gender == "Male" ? "Male" : "Female";
            studentDetailsTable.Hobbies = userDetails.UserDetails.Hobbies;
            studentDetailsTable.DiskDocumentName = "";
            studentDetailsTable.OriginalDocumentName = "";
            studentDetailsTable.Password = userDetails.UserDetails.Password;

            return studentDetailsTable;
        }


        public static AddressDetailViewModel GetAddressDetails(int addressType, AllViewModel.AddressDetailViewModel address)
        {
            AddressDetailViewModel addressDetail = new AddressDetailViewModel();
            addressDetail.AddressType = addressType;
            addressDetail.CountryID = address.CountryID;
            addressDetail.StateID = address.StateID;
            addressDetail.AddressLine1 = address.AddressLine1;
            addressDetail.AddressLine2 = address.AddressLine2;
            addressDetail.Pincode = address.Pincode;
            return addressDetail;
        }

        public static EducationDetailViewModel GetEducationDetails(int educationType, AllViewModel.EducationDetailViewModel education)
        {
            EducationDetailViewModel educationDetailView = new EducationDetailViewModel();
            educationDetailView.EducationType = educationType;

            switch (educationType)
            {
                case EducationType.MatriculationEducation:
                    educationDetailView.InstituteName = education.InstituteName;
                    educationDetailView.Board = education.Board;
                    educationDetailView.Marks = education.Marks;
                    educationDetailView.Aggregate = education.Aggregate;
                    educationDetailView.YearOfCompletion = education.YearOfCompletion;
                    break;
                case EducationType.IntermediateEducation:
                    educationDetailView.InstituteName = education.InstituteName;
                    educationDetailView.Board = education.Board;
                    educationDetailView.Marks = education.Marks;
                    educationDetailView.Aggregate = education.Aggregate;
                    educationDetailView.YearOfCompletion = education.YearOfCompletion;
                    break;
                case EducationType.GraduateEducation:
                    educationDetailView.InstituteName = education.InstituteName;
                    educationDetailView.Board = education.Board;
                    educationDetailView.Marks = education.Marks;
                    educationDetailView.Aggregate = education.Aggregate;
                    educationDetailView.YearOfCompletion = education.YearOfCompletion;
                    break;
            }

            return educationDetailView;
        }


        public static void SubmitDetails(int id, AllViewModel model)
        {
            //if (SessionCheckAttribute.CheckAuthentication(1))
                if (id > 0)
            {
                UserDetailsViewModel studentDetails = GetStudentTableDetails(model);
                UpdateUserDetails(id, studentDetails);

                AddressDetailViewModel addressDetails1 = GetAddressDetails(AddressType.CurrentAddress, model.AddressDetail);
                addressDetails1.AddressType = AddressType.CurrentAddress;
                UpdateAddressDetails(id, AddressType.CurrentAddress, addressDetails1);

                AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(AddressType.PermanentAddress, model.PermanentAddressDetail);
                addressDetailsPermanent.AddressType = AddressType.PermanentAddress;
                UpdateAddressDetails(id, AddressType.PermanentAddress, addressDetailsPermanent);

                EducationDetailViewModel educationDetailViewModel10 = GetEducationDetails(EducationType.MatriculationEducation, model.EducationDetail10);
                UpdateEducationDetails(id, EducationType.MatriculationEducation, educationDetailViewModel10);

                EducationDetailViewModel educationDetailViewModel12 = GetEducationDetails(EducationType.IntermediateEducation, model.EducationDetail12);
                UpdateEducationDetails(id, EducationType.IntermediateEducation, educationDetailViewModel12);

                EducationDetailViewModel educationDetailViewModelG = GetEducationDetails(EducationType.GraduateEducation, model.EducationDetailGraduation);
                UpdateEducationDetails(id, EducationType.GraduateEducation, educationDetailViewModelG);
            }
            if (id == 0)
            {
                UserDetailsViewModel studentDetails = GetStudentTableDetails(model);
                InsertUserDetails(studentDetails);

                int userID = GetLastInsertedUserID();
                InsertUserRoll(userID);

                AddressDetailViewModel addressDetails1 = GetAddressDetails(AddressType.CurrentAddress, model.AddressDetail);
                addressDetails1.AddressType = AddressType.CurrentAddress;
                InsertAddressDetails(addressDetails1);

                AddressDetailViewModel addressDetailsPermanent = GetAddressDetails(AddressType.PermanentAddress, model.PermanentAddressDetail);
                addressDetailsPermanent.AddressType = AddressType.PermanentAddress;
                InsertAddressDetails(addressDetailsPermanent);

                EducationDetailViewModel educationDetailViewModel10 = GetEducationDetails(EducationType.MatriculationEducation, model.EducationDetail10);
                InsertEducationDetails(educationDetailViewModel10);

                EducationDetailViewModel educationDetailViewModel12 = GetEducationDetails(EducationType.IntermediateEducation, model.EducationDetail12);
                InsertEducationDetails(educationDetailViewModel12);

                EducationDetailViewModel educationDetailViewModelG = GetEducationDetails(EducationType.GraduateEducation, model.EducationDetailGraduation);
                InsertEducationDetails(educationDetailViewModelG);
            }
        }

        public static AllViewModel UserDetails(int id)
        {
            if (id == 0)
            {
                AllViewModel allViewModel = new AllViewModel();
                allViewModel.UserDetails = new AllViewModel.UserDetailsViewModel();
                allViewModel.UserDetails.StudentID = 0;
                return allViewModel;
            }
            else
            {
                UserDetailsViewModel userDetails = GetUserDetails(id);
                AddressDetailViewModel currentAddressDetails = GetCurrentAddress(id);
                AddressDetailViewModel permanentAddressDetails = GetPermanentAddress(id);
                EducationDetailViewModel educationDetail10 = GetEducation10(id);
                EducationDetailViewModel educationDetail12 = GetEducation12(id);
                EducationDetailViewModel educationDetailGraduate = GetEducationGraduate(id);

                AllViewModel allViewModel = new AllViewModel
                {
                    UserDetails = new AllViewModel.UserDetailsViewModel
                    {
                        FirstName = userDetails.FirstName,
                        MiddleName = userDetails.MiddleName,
                        LastName = userDetails.LastName,
                        Email = userDetails.Email,
                        Password = userDetails.Password,
                        DateOfBirth = userDetails.DateOfBirth,
                        Gender = userDetails.Gender.ToLower(),
                        Phone = userDetails.Phone,
                        AadharNumber = userDetails.AadharNumber,
                        Hobbies = userDetails.Hobbies
                    },

                    AddressDetail = new AllViewModel.AddressDetailViewModel
                    {
                        CountryID = currentAddressDetails.CountryID,
                        StateID = currentAddressDetails.StateID,
                        AddressLine1 = currentAddressDetails.AddressLine1,
                        AddressLine2 = currentAddressDetails.AddressLine2,
                        Pincode = currentAddressDetails.Pincode,
                    },

                    PermanentAddressDetail = new AllViewModel.AddressDetailViewModel
                    {
                        CountryID = permanentAddressDetails.CountryID,
                        StateID = permanentAddressDetails.StateID,
                        AddressLine1 = permanentAddressDetails.AddressLine1,
                        AddressLine2 = permanentAddressDetails.AddressLine2,
                        Pincode = permanentAddressDetails.Pincode,
                    },

                    EducationDetail10 = new AllViewModel.EducationDetailViewModel
                    {
                        InstituteName = educationDetail10.InstituteName,
                        Board = educationDetail10.Board,
                        Marks = educationDetail10.Marks,
                        Aggregate = educationDetail10.Aggregate,
                        YearOfCompletion = educationDetail10.YearOfCompletion
                    },

                    EducationDetail12 = new AllViewModel.EducationDetailViewModel
                    {
                        InstituteName = educationDetail12.InstituteName,
                        Board = educationDetail12.Board,
                        Marks = educationDetail12.Marks,
                        Aggregate = educationDetail12.Aggregate,
                        YearOfCompletion = educationDetail12.YearOfCompletion
                    },

                    EducationDetailGraduation = new AllViewModel.EducationDetailViewModel
                    {
                        InstituteName = educationDetailGraduate.InstituteName,
                        Board = educationDetailGraduate.Board,
                        Marks = educationDetailGraduate.Marks,
                        Aggregate = educationDetailGraduate.Aggregate,
                        YearOfCompletion = educationDetailGraduate.YearOfCompletion
                    }
                };
                return allViewModel;

            }
        }


    }
}
