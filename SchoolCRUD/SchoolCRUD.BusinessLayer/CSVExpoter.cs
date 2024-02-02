using SchoolCRUD.UtilityLayer;
using SchoolCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SchoolCRUD.BusinessLayer
{
    public class CSVExporter
    {
        public static void WriteToCsv<T>(string folderPath, List<T> data)
        {
            try
            {
                string folderPathWithDate = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(folderPathWithDate);

                string filePath = Path.Combine(folderPathWithDate, $"data_{DateTime.Now.ToString("HH-mm-ss")}.csv");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    var properties = typeof(T).GetProperties();

                    var exportableProperties = properties
                        .Where(p => IsPropertyExportable(p))
                        .ToArray();

                    var header = string.Join(",", exportableProperties.Select(p => p.Name));
                    writer.WriteLine(header);

                    foreach (var obj in data)
                    {
                        var values = exportableProperties.Select(p => ConvertToString(p.GetValue(obj))).ToArray();
                        var line = string.Join("|", values);
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                // Console.WriteLine($"Error writing to CSV file: {ex.Message}");
            }
        }

        private static bool IsPropertyExportable(PropertyInfo property)
        {
            var exportableAttribute = property.GetCustomAttribute<ExportableAttribute>();
            return exportableAttribute != null && exportableAttribute.IsExportable;
        }

        private static string ConvertToString(object value)
        {
            // Convert value to string directly without additional formatting
            return value?.ToString() ?? "";
        }
    }
}
