using System;
using System.Collections.Generic;

namespace DemoUserManagement.ViewModel
{
    public class DocumentViewModel
    {
        public int DocumentID { get; set; }
        public int ObjectID { get; set; }
        public int ObjectType { get; set; }
        public int DocumentType { get; set; }
        public string DiskDocumentName { get; set; }
        public string OriginalDocumentName { get; set; }
        public DateTime Timestamp { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
    }

    public class DocumentViewModel1
    {
        //public int DocumentID { get; set; }
        public int ObjectID { get; set; }
    //public int ObjectType { get; set; }
    //public int DocumentType { get; set; }
    //public string DiskDocumentName { get; set; }
    //public string OriginalDocumentName { get; set; }
    //public DateTime? Timestamp { get; set; }
    //public int PageIndex { get; set; }
    //public int TotalPages { get; set; }
}
}


