using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.ViewModel
{
    public class LogInSessionModel
    {
        public int UserID {  get; set; }
        public bool IsAdmin {  get; set; }
    }
}
