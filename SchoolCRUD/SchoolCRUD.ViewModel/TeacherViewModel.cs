using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCRUD.ViewModel
{
    public class TeacherViewModel
    {       
            public int TeacherID { get; set; }
            public string TeacherName { get; set; }
            public Nullable<int> ClassID { get; set; }
            public Nullable<int> CourseID { get; set; }

            
    }

}

