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
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize, int studentID)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
                   NoteID,
                   ObjectID,
                   ObjectType,
                   NoteText,
                   FORMAT(TimeStamp, 'MM/dd/yyyy hh:mm:ss tt') AS TimeStampFormatted
               FROM 
                   Note
               WHERE ObjectID = @StudentID
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

            string query = "INSERT INTO Note (ObjectID, ObjectType, NoteText, TimeStamp) VALUES (@ObjectID, @ObjectType, @NoteText, @TimeStamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ObjectID", note.ObjectID);
                    command.Parameters.AddWithValue("@ObjectType", note.ObjectType); // Use note.ObjectType directly
                    command.Parameters.AddWithValue("@NoteText", note.NoteText);
                    command.Parameters.AddWithValue("@TimeStamp", DateTime.Now);

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

        public static void InsertDocument(DocumentViewModel document)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

            string query = "INSERT INTO Document (ObjectID, ObjectType, DocumentType, DiskDocumentName, OriginalDocumentName, Timestamp) VALUES (@ObjectID, @ObjectType, @DocumentType, @DiskDocumentName, @OriginalDocumentName, @Timestamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ObjectID", document.ObjectID);
                    command.Parameters.AddWithValue("@ObjectType", document.ObjectType);
                    command.Parameters.AddWithValue("@DocumentType", document.DocumentType);
                    command.Parameters.AddWithValue("@DiskDocumentName", document.DiskDocumentName);
                    command.Parameters.AddWithValue("@OriginalDocumentName", document.OriginalDocumentName);
                    command.Parameters.AddWithValue("@TimeStamp", DateTime.Now);

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

        public static DataTable GetAllDocumentData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize, int objectID)
        {
            DataTable dt = new DataTable();

            string query = $@"SELECT 
             DocumentID,
             ObjectID,
             ObjectType,
             DocumentType,
             DiskDocumentName,
             OriginalDocumentName,
             FORMAT(Timestamp, 'MM/dd/yyyy hh:mm:ss tt') AS TimestampFormatted
             FROM 
                 Document
             WHERE ObjectID = @ObjectID
             ORDER BY 
                 {sortExpression} {sortDirection}
             OFFSET 
             (@StartRowIndex) ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    int startRowIndex = currentPageIndex * pageSize;
                    cmd.Parameters.AddWithValue("@ObjectID", objectID);
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

        public static int GetTotalDocumentCount(int studentID)
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Document WHERE ObjectID=@StudentID";

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

    }
}
