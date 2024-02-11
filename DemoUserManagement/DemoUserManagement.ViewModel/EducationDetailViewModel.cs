using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class EducationDetailViewModel
    {
        public int EducationID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> EducationType { get; set; }
        public string InstituteName { get; set; }
        public string Board { get; set; }
        public string Marks { get; set; }
        public Nullable<decimal> Aggregate { get; set; }
        public Nullable<int> YearOfCompletion { get; set; }

    }
}
