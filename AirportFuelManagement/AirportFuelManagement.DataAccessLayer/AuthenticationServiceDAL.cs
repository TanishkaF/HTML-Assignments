using AirportFuelManagement.UtilityLayer;
using AirportFuelManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportFuelManagement.DataAccessLayer
{
    public class AuthenticationServiceDAL
    {
        public static bool InsertUser(AllViewModel.User userViewModel)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    User userEntity = new User
                    {
                        Name = userViewModel.Name,
                        EmailID = userViewModel.EmailID,
                        Password = userViewModel.Password
                    };

                    context.Users.Add(userEntity);
                    context.SaveChanges();

                    int userId = userEntity.UserID;
                    InsertUserRole(userId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return false;
            }
        }

        public static void InsertUserRole(int userId)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var defaultRole = context.Roles.FirstOrDefault(r => r.IsDefault == true);

                    if (defaultRole != null)
                    {
                        UserRole userRole = new UserRole
                        {
                            UserID = userId,
                            RoleID = defaultRole.RoleID
                        };

                        context.UserRoles.Add(userRole);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("No role found with IsDefault set to 1");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }
        }

        public static bool ValidateUser(string email, string password)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.EmailID == email && u.Password == password);
                    return user != null;
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return false;
            }
        }

        public static bool CheckEmailExists(string email, int userID)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    IQueryable<User> query;
                    if (userID == 0)
                    {
                        query = context.Users.Where(u => u.EmailID == email);
                    }
                    else
                    {
                        query = context.Users.Where(u => u.EmailID == email && u.UserID != userID);
                    }

                    bool emailExists = query.Any();

                    return emailExists;
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return false;
            }
        }

        public static int GetUserID(string email)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.EmailID == email);
                    return user?.UserID ?? -1;
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return -1;
            }
        }

        public static bool IsAdmin(string email)
        {
            try
            {
                using (var context = new AirportDBEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.EmailID == email);
                    if (user == null)
                        return false;

                    var isAdmin = context.UserRoles.Any(ur => ur.UserID == user.UserID && ur.Role.IsAdmin == true);

                    return isAdmin;
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
                return false;
            }
        }
    }
}