using System;

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
    }
}


