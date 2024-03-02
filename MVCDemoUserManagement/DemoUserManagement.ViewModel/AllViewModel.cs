using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class AllViewModel
    {
        public UserDetailsViewModel UserDetails { get; set; }
        public AddressDetailViewModel AddressDetail { get; set; }
        public AddressDetailViewModel PermanentAddressDetail { get; set; }
        public EducationDetailViewModel EducationDetail { get; set; }  
        public EducationDetailViewModel EducationDetail10 { get; set; }  
        public EducationDetailViewModel EducationDetail12 { get; set; }  
        public EducationDetailViewModel EducationDetailGraduation { get; set; }  
        public NoteViewModel NoteDetails{ get; set; }  
      

        public class UserDetailsViewModel
        {
           // public int id { get; set; }
            public int StudentID { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public string AadharNumber { get; set; }
            public string Hobbies { get; set; }
            public string DiskDocumentName { get; set; }
            public string OriginalDocumentName { get; set; }
            public int PageIndex { get; set; }
            public int TotalPages { get; set; }
           
        }

        public class AddressDetailViewModel
        {
            public int AddressID { get; set; }
            public int? UserID { get; set; }
            public int? AddressType { get; set; }
            public int? CountryID { get; set; }
            public int? StateID { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string Pincode { get; set; }

            public int? PermanentCountryID { get; set; }
            public int? PermanentStateID { get; set; }
            public string PermanentAddressLine1 { get; set; }
            public string PermanentAddressLine2 { get; set; }
            public string PermanentPincode { get; set; }
        }

        public class EducationDetailViewModel
        {
            public int EducationID { get; set; }
            public int? StudentID { get; set; }
            public int? EducationType { get; set; }
            public string InstituteName { get; set; }
            public string Board { get; set; }
            public string Marks { get; set; }
            public decimal? Aggregate { get; set; }
            public int? YearOfCompletion { get; set; }  
        }
        public class NoteViewModel
        {
            public int NoteID { get; set; }
            public int ObjectID { get; set; }
            public int ObjectType { get; set; }
            public string NoteText { get; set; }
            public DateTime TimeStamp { get; set; }
            public int PageIndex { get; set; }
            public int TotalPages { get; set; }
            public List<NoteViewModel> Notes { get; set; }


        }
    }
}
