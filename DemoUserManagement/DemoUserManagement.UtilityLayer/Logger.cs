using System;
using System.Configuration;
using System.IO;

namespace DemoUserManagement.UtilityLayer
{
    public class Logger
    {
        public static void AddData(Exception inputData)
        {
            string logFolderPath = ConfigurationManager.AppSettings["LogFolderPath"];
            string folderPath = Path.Combine(logFolderPath, DateTime.Now.ToString("yyyy-MM-dd"));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string logFileName = $"logFile_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";
            string fullLogFilePath = Path.Combine(folderPath, logFileName);

            string logMessage = GetExceptionDetails(inputData);

            using (StreamWriter writer = new StreamWriter(fullLogFilePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }

        private static string GetExceptionDetails(Exception exception)
        {
            string logMessage = $"Time: {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")}{Environment.NewLine}" +
                                "-----------------------------------------------------------" +
                                $"{Environment.NewLine}Exception Details:{Environment.NewLine}" +
                                $"Message: {exception.Message}{Environment.NewLine}" +
                                $"StackTrace: {exception.StackTrace}{Environment.NewLine}" +
                                $"Source: {exception.Source}{Environment.NewLine}" +
                                $"TargetSite: {exception.TargetSite?.ToString()}{Environment.NewLine}" +
                                "-----------------------------------------------------------" +
                                $"{Environment.NewLine}";

            if (exception.InnerException != null)
            {
                logMessage += $"Inner Exception:{Environment.NewLine}" +
                              $"{GetExceptionDetails(exception.InnerException)}";
            }

            return logMessage;
        }
    }
}
