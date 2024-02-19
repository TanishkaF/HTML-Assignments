using DemoUserManagement.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.BusinessLayer
{
    public class AuthenticationServiceBusiness
    {
        public static bool ValidateUser(string email, string password)
        {
            return AuthenticationServiceDataAccess.ValidateUser(email, password);
        }

        public static int GetUserID(string email)
        {
            return AuthenticationServiceDataAccess.GetUserID(email);
        }

        public static bool IsAdmin(string email)
        {
            return AuthenticationServiceDataAccess.IsAdmin(email);
        }

        public static bool CheckEmailExists(string email)
        {
            return AuthenticationServiceDataAccess.CheckEmailExists(email);
        }
    }

}
