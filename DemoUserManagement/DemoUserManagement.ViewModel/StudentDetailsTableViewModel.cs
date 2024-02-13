using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class StudentDetailsTableViewModel
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string AadharNumber { get; set; }
        public string Hobbies { get; set; }
        public string DiskDocumentName { get; set; }
        public string OriginalDocumentName { get; set; }
    }



}
