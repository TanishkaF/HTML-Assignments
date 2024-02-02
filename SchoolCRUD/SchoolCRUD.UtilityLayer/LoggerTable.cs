using System.Configuration;
using System.Data.SqlClient;
using System;

namespace SchoolCRUD.UtilityLayer
{
    public class LoggerTable
    {
        public static void AddData(Exception inputData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO SchoolErrorLogs (Timestamp, Message, StackTrace, Source, TargetSite)
                        VALUES (@Timestamp, @Message, @StackTrace, @Source, @TargetSite)
                    ";

                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    command.Parameters.AddWithValue("@Message", inputData.Message);
                    command.Parameters.AddWithValue("@StackTrace", inputData.StackTrace);
                    command.Parameters.AddWithValue("@Source", inputData.Source);
                    command.Parameters.AddWithValue("@TargetSite", inputData.TargetSite?.ToString());

                    command.ExecuteNonQuery();
                }

                
                Exception innerException = inputData.InnerException;

                while (innerException != null)
                {
                    using (SqlCommand innerCommand = connection.CreateCommand())
                    {
                        innerCommand.CommandText = @"
                            INSERT INTO SchoolErrorLogs (Timestamp, Message, StackTrace, Source, TargetSite)
                            VALUES (@Timestamp, @Message, @StackTrace, @Source, @TargetSite)
                        ";

                        innerCommand.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                        innerCommand.Parameters.AddWithValue("@Message", innerException.Message);
                        innerCommand.Parameters.AddWithValue("@StackTrace", innerException.StackTrace);
                        innerCommand.Parameters.AddWithValue("@Source", innerException.Source);
                        innerCommand.Parameters.AddWithValue("@TargetSite", innerException.TargetSite?.ToString());

                        innerCommand.ExecuteNonQuery();
                    }

                    innerException = innerException.InnerException;
                }
            }
        }
    }
}
