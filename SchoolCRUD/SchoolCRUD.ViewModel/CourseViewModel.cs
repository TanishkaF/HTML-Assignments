using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCRUD.ViewModel
{

     public class CourseViewModel
    {
        [ExportableAttribute(true)]
        public int CourseID { get; set; }

        [ExportableAttribute(true)]
        public string CourseName { get; set; }

        [ExportableAttribute(true)]
        public int Credits { get; set; }

        public List<StudentViewModel> Students { get; set; }
        public List<ClassViewModel> Classes { get; set; }
    }
}
