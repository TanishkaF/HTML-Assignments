using AirportFuelManagement.DataAccessLayer;
using AirportFuelManagement.UtilityLayer;
using AirportFuelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagement.BusinessLayer
{
    public static class AuthenticationServiceBL
    {
        public static bool ValidateUser(string email, string password)
        {
            try
            {
              return AuthenticationServiceDAL.ValidateUser(email, password);
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static bool CheckEmailExists(string email, int userId)
        {
            try
            {
                return AuthenticationServiceDAL.CheckEmailExists(email, userId);
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static bool AddUser(AllViewModel.User userViewModel)
        {
            try
            {
                return AuthenticationServiceDAL.InsertUser(userViewModel);
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }

        public static int GetUserID(string email)
        {
            try
            {
                return AuthenticationServiceDAL.GetUserID(email);
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return -1;
            }
        }

        public static bool IsAdmin(string email)
        {
            try
            {
                return AuthenticationServiceDAL.IsAdmin(email);
            }
            catch (Exception ex)
            {
                Logger.AddLogException(ex);
                return false;
            }
        }
    }
}