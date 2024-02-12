using DemoUserManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagement.DataAccessLayer
{
    public class NoteUserControlDataAcess
    {
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize,int studentID)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
                        NoteID,
                        StudentID,
                        NoteType,
                        NoteData,
                        TimeStamp
                    FROM 
                        Note
                    WHERE StudentID=@StudentID
                    ORDER BY 
                        {sortExpression} {sortDirection}
                    OFFSET 
                        (@StartRowIndex) ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
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

        public static int GetTotalNotesCount(int studentID)
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Note WHERE StudentID=@StudentID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter for studentID
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    try
                    {
                        con.Open();
                        totalCount = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        throw;
                    }
                }
            }

            return totalCount;
        }


        public static void InsertNote(string studentID, string noteType, string noteData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

            // Format the current timestamp in the desired format
            string formattedTimeStamp = DateTime.Now.ToString("hh:mm:ss tt");

            string query = "INSERT INTO Note (StudentID, NoteType, NoteData, TimeStamp) VALUES (@StudentID, @NoteType, @NoteData, @TimeStamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    command.Parameters.AddWithValue("@NoteType", noteType);
                    command.Parameters.AddWithValue("@NoteData", noteData);
                    command.Parameters.AddWithValue("@TimeStamp", formattedTimeStamp);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log error, throw exception)
                        throw;
                    }
                }
            }
        }






    }
}
