using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCRUD.ViewModel
{
    public class EnrollmentViewModel
    {
        [ExportableAttribute(true)]
        public int EnrollmentID { get; set; }

        [ExportableAttribute(true)]
        public int StudentID { get; set; }

        [ExportableAttribute(true)]
        public int CourseID { get; set; }

    }
}
