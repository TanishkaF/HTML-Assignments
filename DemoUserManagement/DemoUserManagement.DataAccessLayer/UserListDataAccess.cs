 using DemoUserManagement.UtilityLayer;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;

namespace DemoUserManagement.DataAccessLayer
{
    public static class UserListDataAccess
    {
        public static DataTable GetAllUserListData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
                        StudentID, FirstName, LastName, Phone, AadharNumber, OriginalDocumentName
                      FROM 
                        StudentDetailsTable
                      ORDER BY {sortExpression} {sortDirection}
                      OFFSET @StartRowIndex ROWS FETCH NEXT @PageSize ROWS ONLY";

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


        public static int GetTotalUserCount()
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = @"SELECT COUNT(*) FROM StudentDetailsTable"; 

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
