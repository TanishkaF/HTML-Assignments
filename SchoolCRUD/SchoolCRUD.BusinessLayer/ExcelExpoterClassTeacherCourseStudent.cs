using OfficeOpenXml;
using SchoolCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SchoolCRUD.BusinessLayer
{
    public class ExcelExpoterClassTeacherCourseStudent
    {
        public static void ExportToExcel(string fileName, List<ClassTeacherCourseStudent> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

           
            string folderPathWithDate = Path.Combine(fileName, DateTime.Now.ToString("yyyy-MM-dd"));
            Directory.CreateDirectory(folderPathWithDate);

            string filePath = Path.Combine(folderPathWithDate, $"data_{DateTime.Now.ToString("HH-mm-ss")}.xlsx");

           
            FileInfo fileInfo = new FileInfo(filePath);

            using (var package = new ExcelPackage())
            {
                var groupedData = data.GroupBy(d => d.ClassID);

                foreach (var classGroup in groupedData)
                {
                    var classID = classGroup.Key;
                    var className = classGroup.First().ClassName;

                    var worksheet = package.Workbook.Worksheets.Add($"Class_{classID}_{className}");

                    var teacherHeaders = new List<string>
                    {
                        "Teacher ID", "Teacher Name", "Teacher Course ID"
                    };
                    for (int i = 0; i < teacherHeaders.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = teacherHeaders[i];
                        worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    }

                    int row = 2;
                    foreach (var entry in classGroup)
                    {
                        worksheet.Cells[row, 1].Value = entry.TeacherID;
                        worksheet.Cells[row, 2].Value = entry.TeacherName; // Make sure TeacherName is correctly populated
                        worksheet.Cells[row, 3].Value = entry.CourseID;

                        row++;
                    }
                    row += 2;

                    
                    var courseHeaders = new List<string>
                    {
                        "Course ID", "Course Name", "Credits"
                    };
                    for (int i = 0; i < courseHeaders.Count; i++)
                    {
                        worksheet.Cells[row, i + 1].Value = courseHeaders[i];
                        worksheet.Cells[row, i + 1].Style.Font.Bold = true;
                    }

                    // Write data for Course
                    row++;
                    foreach (var entry in classGroup)
                    {
                        worksheet.Cells[row, 1].Value = entry.CourseID;
                        worksheet.Cells[row, 2].Value = entry.CourseName;
                        worksheet.Cells[row, 3].Value = entry.Credits;

                        row++;
                    }

                    // Add space after Course details
                    row += 2;

                    // Write headers for Student
                    var studentHeaders = new List<string>
                    {
                        "Student ID", "First Name", "Last Name", "Age", "GPA"
                    };
                    for (int i = 0; i < studentHeaders.Count; i++)
                    {
                        worksheet.Cells[row, i + 1].Value = studentHeaders[i];
                        worksheet.Cells[row, i + 1].Style.Font.Bold = true;
                    }

                    // Write data for Student
                    row++;
                    foreach (var entry in classGroup)
                    {
                        worksheet.Cells[row, 1].Value = entry.StudentID;
                        worksheet.Cells[row, 2].Value = entry.FirstName;
                        worksheet.Cells[row, 3].Value = entry.LastName;
                        worksheet.Cells[row, 4].Value = entry.Age;
                        worksheet.Cells[row, 5].Value = entry.GPA;

                        row++;
                    }

                    // Auto-fit columns for better readability
                    worksheet.Cells.AutoFitColumns();
                }

                // Save the Excel package to the specified file
                package.SaveAs(fileInfo);
            }

            // Display a message indicating successful file creation
            Console.WriteLine($"Excel file '{fileInfo}' created successfully.");
        }

        private static bool IsPropertyExportable(PropertyInfo property)
        {
            // Check if the property has the ExportableAttribute set to true
            var exportableAttribute = property.GetCustomAttribute<ExportableAttribute>();
            return exportableAttribute != null && exportableAttribute.IsExportable;
        }
    }
}
