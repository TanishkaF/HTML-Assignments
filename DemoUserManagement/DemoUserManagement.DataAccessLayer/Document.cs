//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoUserManagement.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int documentID { get; set; }
        public Nullable<int> studentID { get; set; }
        public string DiskDocumentName { get; set; }
        public string OriginalDocumentName { get; set; }
    }
}
