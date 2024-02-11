using DemoUserManagement.UtilityLayer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DemoUserManagement.DataAccessLayer
{
    public static class UserListDataAccess
    {
        public static DataTable GetAllUsersData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();
            int totalCount = GetTotalCount(); // Retrieve total count of all users

            string query = $@"SELECT 
                        StudentDetails.StudentID,
                        StudentDetails.FirstName,
                        StudentDetails.LastName,
                        StudentDetails.Phone,
                        StudentDetails.AadharNumber,
                        AddressDetails.Country
                    FROM 
                        StudentDetails 
                    INNER JOIN 
                        AddressDetails ON StudentDetails.StudentID = AddressDetails.UserID
                    ORDER BY 
                        {sortExpression} {sortDirection}
                    OFFSET 
                        (@StartRowIndex) ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            return dt;
        }

        public static DataTable GetFilteredUsersData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();
            int totalCount = GetTotalFilteredCount(); // Retrieve total count of filtered users

            string query = $@"SELECT 
                        StudentDetails.StudentID,
                        StudentDetails.FirstName,
                        StudentDetails.LastName,
                        StudentDetails.Phone,
                        StudentDetails.AadharNumber,
                        AddressDetails.Country
                    FROM 
                        StudentDetails 
                    INNER JOIN 
                        AddressDetails ON StudentDetails.StudentID = AddressDetails.UserID
                    WHERE 
                        AddressDetails.AddressType = 1
                    ORDER BY 
                        {sortExpression} {sortDirection}
                    OFFSET 
                        (@StartRowIndex) ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@StartRowIndex", startRowIndex);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            return dt;
        }

        public static int GetTotalCount()
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM StudentDetails";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        totalCount = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            return totalCount;
        }

        private static int GetTotalFilteredCount()
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM StudentDetails " +
                               "INNER JOIN AddressDetails ON StudentDetails.StudentID = AddressDetails.UserID " +
                               "WHERE AddressDetails.AddressType = 1"; // Assuming 1 represents the current address

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        totalCount = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            return totalCount;
        }

    }
}
