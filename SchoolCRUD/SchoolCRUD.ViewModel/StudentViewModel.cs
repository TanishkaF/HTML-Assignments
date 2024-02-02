using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolCRUD.ViewModel
{
    public class StudentViewModel
    {
        [ExportableAttribute(true)]
        public int StudentID { get; set; }

        [ExportableAttribute(true)]
        public string FirstName { get; set; }

        [ExportableAttribute(true)]
        public string LastName { get; set; }

        [ExportableAttribute(true)]
        public int Age { get; set; }

        [ExportableAttribute(true)]
        public double GPA { get; set; }

      
        public object Course { get; set; }        
        public object Class { get; set; }
        public List<ClassViewModel> Classes { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
