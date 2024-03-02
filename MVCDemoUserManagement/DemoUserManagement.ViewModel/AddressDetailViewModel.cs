using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class AddressDetailViewModel
    {     
        public int AddressID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> AddressType { get; set; }
        public Nullable<int> CountryID { get; set; }  // Changed from string to int
        public Nullable<int> StateID { get; set; }    // Changed from string to int
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Pincode { get; set; }
    }
}
