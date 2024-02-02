using OfficeOpenXml;
using SchoolCRUD.UtilityLayer;
using SchoolCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SchoolCRUD.BusinessLayer
{
    public class ExcelExporter
    {
        public static void ExportToExcel<T>(string folderPath, List<T> data)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                string folderPathWithDate = Path.Combine(folderPath, DateTime.Now.ToString("yyyy-MM-dd"));
                Directory.CreateDirectory(folderPathWithDate);

                string filePath = Path.Combine(folderPathWithDate, $"data_{DateTime.Now.ToString("HH-mm-ss")}.xlsx");

                FileInfo fileInfo = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(typeof(T).Name);

                    var exportableProperties = typeof(T).GetProperties()
                        .Where(p => IsPropertyExportable(p))
                        .ToArray();

                    for (int col = 1; col <= exportableProperties.Length; col++)
                    {
                        worksheet.Cells[1, col].Value = exportableProperties[col - 1].Name;
                    }

                    for (int row = 0; row < data.Count; row++)
                    {
                        for (int col = 1; col <= exportableProperties.Length; col++)
                        {
                            worksheet.Cells[row + 2, col].Value = exportableProperties[col - 1].GetValue(data[row]);
                        }
                    }

                    package.Save();
                }

               // Console.WriteLine($"Data exported to {filePath} successfully.");
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
               // Console.WriteLine($"Error exporting data to Excel: {ex.Message}");
            }
        }

        private static bool IsPropertyExportable(PropertyInfo property)
        {
            var exportableAttribute = property.GetCustomAttribute<ExportableAttribute>();
            return exportableAttribute != null && exportableAttribute.IsExportable;
        }
    }
}
