using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DemoUserManagement.DataAccessLayer
{
    public class NoteUserControlDataAcess
    {
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize,int studentID)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
                        NoteID,
                        ObjectID,
                        ObjectType,
                        NoteText,
                        TimeStamp
                    FROM 
                        Note
                    WHERE ObjectID=@StudentID
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
                    cmd.Parameters.AddWithValue("@TimeStamp", DateTime.Now);

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
                string query = "SELECT COUNT(*) FROM Note WHERE ObjectID=@StudentID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    try
                    {
                        con.Open();
                        totalCount = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex); ;
                    }
                }
            }

            return totalCount;
        }


        public static void InsertNote(NoteViewModel note)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

           // string formattedTimeStamp = DateTime.Now.ToString("d");

            string query = "INSERT INTO Note (ObjectID, ObjectType, NoteText, TimeStamp) VALUES (@ObjectID, @ObjectType, @NoteText, @TimeStamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                    command.Parameters.AddWithValue("@ObjectID", note.ObjectID);
                    command.Parameters.AddWithValue("@ObjectType", NoteType.ObjectType);
                    command.Parameters.AddWithValue("@NoteText", note.NoteText);
                    command.Parameters.AddWithValue("@TimeStamp", DateTime.Now.ToString("d"));

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }
        }

    }
}
