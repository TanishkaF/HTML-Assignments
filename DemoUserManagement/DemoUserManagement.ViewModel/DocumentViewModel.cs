using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class DocumentViewModel
    {
        public int DocumentID { get; set; }
        public int StudentID { get; set; }
        public string DiskDocumentName { get; set; }
        public string OriginalDocumentName { get; set; }
    }
}
