using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class NotesPageViewModel
    {
        public List<NoteViewModel> Notes { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int ObjectID { get; set; }
        public int ObjectType { get; set; }
    }
}
