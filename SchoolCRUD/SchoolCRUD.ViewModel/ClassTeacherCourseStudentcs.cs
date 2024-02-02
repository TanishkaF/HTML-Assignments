using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCRUD.ViewModel
{
    public class ClassTeacherCourseStudent
    {
        [Exportable(true)]
        public int ClassID { get; set; }
        [Exportable(true)]
        public string ClassName { get; set; }
        [Exportable(true)]
        public int TeacherID { get; set; }

        [Exportable(true)]
        public int TeacherCourseID { get; set; }

        [Exportable(true)]
        public int CourseID { get; set; }

        [Exportable(true)]
        public string CourseName { get; set; }

        [Exportable(true)]
        public int Credits { get; set; }

        [Exportable(true)]
        public int StudentID { get; set; }

        [Exportable(true)]
        public string FirstName { get; set; }

        [Exportable(true)]
        public string LastName { get; set; }

        [Exportable(true)]
        public int Age { get; set; }

        [Exportable(true)]
        public double GPA { get; set; }
    }

}
