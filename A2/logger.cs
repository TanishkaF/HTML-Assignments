using System;
using System.Configuration;
using System.IO;

namespace CRUDLOG
{
    internal class Logger
    {
        public static void AddData(Exception inputData)
        {
            //string fileName = @"C:\Users\tanishkaf\Desktop\dotnetprac\A2\logFile.txt";
            string logFilePath = ConfigurationManager.AppSettings["LogFilePath"];


            string logFolderPath = ConfigurationManager.AppSettings["LogFolderPath"];

            // Create a folder based on the current date
            string folderPath = Path.Combine(logFolderPath, DateTime.Now.ToString("yyyy-MM-dd"));

            // Ensure the folder exists, create it if necessary
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);

            }
            string logFileName = $"logFile_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";
            string fullLogFilePath = Path.Combine(folderPath, logFileName);


            string logMessage = $"Time: {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")}{Environment.NewLine}" +
                                "-----------------------------------------------------------" +
                                $"{Environment.NewLine}Exception Details:{Environment.NewLine}" +
                                $"Message: {inputData.Message}{Environment.NewLine}" +
                                $"StackTrace: {inputData.StackTrace}{Environment.NewLine}" +
                                $"Source: {inputData.Source}{Environment.NewLine}" +
                                $"TargetSite: {inputData.TargetSite?.ToString()}{Environment.NewLine}" +
                                "-----------------------------------------------------------" +
                                $"{Environment.NewLine}";

            using (StreamWriter writer = new StreamWriter(fullLogFilePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
    }

}

