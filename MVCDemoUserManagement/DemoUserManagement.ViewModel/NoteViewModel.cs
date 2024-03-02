using System;
using System.Collections.Generic;

namespace DemoUserManagement.ViewModel
{
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