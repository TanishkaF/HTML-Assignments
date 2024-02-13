using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class NoteViewModel
    {   
        public int NoteId { get; set; }
        public int ObjectID { get; set; } // StudentID
        public string NoteText { get; set; }

        public string TimeStamp { get; set; }

    }
}
