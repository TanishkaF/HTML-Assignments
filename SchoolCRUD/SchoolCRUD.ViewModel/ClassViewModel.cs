using System;
using System.Collections.Generic;

namespace SchoolCRUD.ViewModel
{
    public class ClassViewModel
    {
        [Exportable(true)]
        public int ClassID { get; set; }

        [Exportable(true)]
        public string ClassName { get; set; }

        [Exportable(true)]
        public string Instructor { get; set; }        
        
    }
}
