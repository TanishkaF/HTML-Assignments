using DemoUserManagement.UtilityLayer;
using DemoUserManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DemoUserManagement.DataAccessLayer
{
    public class NoteUserControlDataAcess
    {
        public static DataTable GetAllNotesData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize, int objectID,int objectType)
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
               WHERE ObjectID = @ObjectID AND ObjectType = @ObjectType
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
                    cmd.Parameters.AddWithValue("@ObjectType", objectType);
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

        public static int GetTotalNotesCount(int objectID,int objectType)
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Note WHERE ObjectID=@ObjectID AND ObjectType = @ObjectType";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ObjectID", objectID);
                    cmd.Parameters.AddWithValue("@ObjectType", objectType);

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

        public static DataTable GetAllDocumentData(string sortExpression, string sortDirection, int currentPageIndex, int pageSize, int objectID,int objectType)
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
             WHERE ObjectID = @ObjectID AND ObjectType = @objectType
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
                    cmd.Parameters.AddWithValue("@ObjectType", objectType);
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

        public static int GetTotalDocumentCount(int objectID, int objectType)
        {
            int totalCount = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Document WHERE ObjectID=@ObjectID AND ObjectType=@ObjectType";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ObjectID", objectID);
                    cmd.Parameters.AddWithValue("@ObjectType", objectType);

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

        public static string GetDocumentUniqueNameById(int documentID)
        {
            string uniqueDocumentName = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

            string query = "SELECT DiskDocumentName FROM Document WHERE DocumentID = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", documentID);

                    try
                    {
                        connection.Open();
                        uniqueDocumentName = (string)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddData(ex);
                    }
                }
            }

            return uniqueDocumentName;
        }

        public static List<int> GetDocumentIDsByObjectID(int objectID)
        {
            List<int> documentIDs = new List<int>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DemoUserManagementConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Your SQL query to retrieve documentIDs based on ObjectID
                    string query = "SELECT DocumentID FROM Document WHERE ObjectID = @ObjectID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ObjectID", objectID);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int documentID = Convert.ToInt32(reader["DocumentID"]);
                            documentIDs.Add(documentID);
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddData(ex);
            }

            return documentIDs;
        }

    }
}
