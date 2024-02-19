using DemoUserManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public class AuthenticationServiceDataAccess
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

        public static bool ValidateUser(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM UserDetails WHERE Email = @Email AND BINARY_CHECKSUM(Password) = BINARY_CHECKSUM(@Password)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return count > 0;
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT StudentID FROM UserDetails WHERE Email = @Email";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string getUserIdQuery = "SELECT StudentID FROM UserDetails WHERE Email = @Email";
                SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection);
                getUserIdCommand.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int userID = (int)getUserIdCommand.ExecuteScalar();

                string isAdminQuery = @"
                            SELECT R.isAdmin 
                            FROM UserRoll UR 
                            INNER JOIN Roll R ON UR.RollID = R.RollID 
                            WHERE UR.UserID = @UserID AND R.isAdmin = 1";

                SqlCommand isAdminCommand = new SqlCommand(isAdminQuery, connection);
                isAdminCommand.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader reader = isAdminCommand.ExecuteReader();
                bool isAdmin = reader.HasRows;

                return isAdmin;
            }
        }

        public static bool CheckEmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM UserDetails WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
