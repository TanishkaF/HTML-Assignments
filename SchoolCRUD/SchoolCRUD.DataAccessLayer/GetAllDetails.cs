using SchoolCRUD.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace SchoolCRUD.DataAccessLayer
{
    public class GetAllDetails
    {

        public static List<SchoolCRUD.ViewModel.ClassTeacherCourseStudent> GetClassTeacherCourseStudents()
        {
            using (var context = new SCHOOLEntities())
            {
                //           var query = @"public static string sqlQuery = @""
                //SELECT DISTINCT
                //    c.ClassID,
                //    c.ClassName,
                //    t.TeacherID,
                //    t.CourseID AS TeacherCourseID,
                //    co.CourseID,
                //    co.CourseName,
                //    co.Credits,
                //    s.StudentID,
                //    s.FirstName,
                //    s.LastName,
                //    s.Age,
                //    s.GPA
                //FROM
                //    Class c
                //JOIN
                //    Teacher t ON c.ClassID = t.ClassID
                //JOIN
                //    Course co ON t.CourseID = co.CourseID
                //JOIN
                //    Enrollment e ON co.CourseID = e.CourseID
                //JOIN
                //    Student s ON e.StudentID = s.StudentID
                //ORDER BY
                //    c.ClassID;";


                var query = (from c in context.Classes
                             join t in context.Teachers on c.ClassID equals t.ClassID
                             join co in context.Courses on t.CourseID equals co.CourseID
                             join e in context.Enrollments on co.CourseID equals e.CourseID
                             join s in context.Students on e.StudentID equals s.StudentID
                             orderby c.ClassID
                             select new ClassTeacherCourseStudent
                             {
                                 ClassID = c.ClassID,
                                 ClassName = c.ClassName,
                                 TeacherID = t.TeacherID,
                                 TeacherCourseID = (int) t.CourseID,
                                 CourseID = co.CourseID,
                                 CourseName = co.CourseName,
                                 Credits = (int)co.Credits,
                                 StudentID = s.StudentID,
                                 FirstName = s.FirstName,
                                 LastName = s.LastName,
                                 Age = (int)s.Age,
                                 GPA = (int)s.GPA
                             }).Distinct().ToString();

                

                var resultQuery = context.Database.SqlQuery<ClassTeacherCourseStudent>(query).ToList();

                // Call the ExcelExporter.ExportToExcel method to export the data to an Excel file
                // ExcelExporter.ExportToExcel("yourFileName.xlsx", resultQuery);
                return resultQuery;
            }
        }
    }
}
