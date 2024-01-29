using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HI");
            Console.ReadKey();
            using (SCHOOLEntities DBEntities = new SCHOOLEntities())
            {
                while (true)
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Create Student");
                    Console.WriteLine("2. Create Class");
                    Console.WriteLine("3. Create Course");
                    Console.WriteLine("4. Update Student By ID");
                    Console.WriteLine("5. Update Class By ID");
                    Console.WriteLine("6. Update Course By ID");
                    Console.WriteLine("7. Delete Student By ID");
                    Console.WriteLine("8. Delete Classt By ID");
                    Console.WriteLine("9. Delete Course By ID");
                    Console.WriteLine("10. Display Student");
                    Console.WriteLine("11. Display Class");
                    Console.WriteLine("12. Display Course");
                    Console.WriteLine("13. Exit");

                    Console.Write("Enter your choice: ");


                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                CreateNewStudent(DBEntities);
                                break;
                            case 2:
                                CreateNewClass(DBEntities);
                                break;
                            case 3:
                                CreateNewCourse(DBEntities);
                                break;
                            case 4:
                               UpdateStudentByID(DBEntities);
                                break;
                            case 5:
                                UpdateClassByID(DBEntities);
                                break;
                            case 6:
                                UpdateCourseByID(DBEntities);
                                break;
                            case 7:
                                DeleteStudentByID(DBEntities);
                                break;
                            case 8:
                               DeleteClassByID(DBEntities);
                                break;
                            case 9:
                                DeleteCourseByID(DBEntities);
                                break;
                            case 10:
                                ReadStudent(DBEntities);
                                break;
                            case 11:
                               ReadClass(DBEntities);
                                break;
                            case 12:
                                ReadCourse(DBEntities);
                                break;
                            case 13:
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a valid option.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }

                    Console.WriteLine();
                }
            }      
        }

        static void CreateNewStudent(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the number of students to create: ");

            if (int.TryParse(Console.ReadLine(), out int numberOfStudents) && numberOfStudents > 0)
            {
                int currentMaxStudentID = DBEntities.Students.Max(s => (int?)s.StudentID) ?? 0;

                for (int i = 0; i < numberOfStudents; i++)
                {
                    Console.Write($"Enter the first name of student #{i + 1}: ");
                    string firstName = Console.ReadLine();

                    Console.Write($"Enter the last name of student #{i + 1}: ");
                    string lastName = Console.ReadLine();

                    Console.Write($"Enter the age of student #{i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int age))
                    {
                        Console.Write($"Enter the GPA of student #{i + 1}: ");
                        if (double.TryParse(Console.ReadLine(), out double gpa))
                        {
                            Student newStudent = new Student
                            {
                                StudentID = currentMaxStudentID + 1,
                                FirstName = firstName,
                                LastName = lastName,
                                Age = age,
                                GPA = gpa
                            };

                            DBEntities.Students.Add(newStudent);
                            DBEntities.SaveChanges();
                            Console.WriteLine("New student created. StudentID: {0}", newStudent.StudentID);
                        }
                        else
                        {
                            Console.WriteLine("Invalid GPA. Unable to create the student.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid age. Unable to create the student.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
            }
        }

        static void CreateNewClass(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the number of classes to create: ");

            if (int.TryParse(Console.ReadLine(), out int numberOfClasses) && numberOfClasses > 0)
            {
                int currentMaxClassID = DBEntities.Classes.Max(c => (int?)c.ClassID) ?? 0;

                for (int i = 0; i < numberOfClasses; i++)
                {
                    Console.Write($"Enter the name of class #{i + 1}: ");
                    string className = Console.ReadLine();

                    Console.Write($"Enter the instructor for class #{i + 1}: ");
                    string instructor = Console.ReadLine();

                    Console.Write($"Enter the CourseID for class #{i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int courseID))
                    {
                        // Check if the specified CourseID exists in the Course table
                        var existingCourse = DBEntities.Courses.Find(courseID);

                        if (existingCourse != null)
                        {
                            Console.Write($"Enter the StudentID for class #{i + 1}: ");
                            if (int.TryParse(Console.ReadLine(), out int studentID))
                            {
                                Class newClass = new Class
                                {
                                    ClassID = currentMaxClassID + 1,
                                    ClassName = className,
                                    Instructor = instructor,
                                    CourseID = courseID,
                                    StudentID = studentID
                                };

                                DBEntities.Classes.Add(newClass);
                                DBEntities.SaveChanges();
                                Console.WriteLine("New class created. ClassID: {0}", newClass.ClassID);
                            }
                            else
                            {
                                Console.WriteLine("Invalid StudentID. Unable to create the class.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Course with CourseID {0} does not exist. Unable to create the class.", courseID);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid CourseID. Unable to create the class.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
            }
        }

        static void CreateNewCourse(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the number of courses to create: ");

            if (int.TryParse(Console.ReadLine(), out int numberOfCourses) && numberOfCourses > 0)
            {
                int currentMaxCourseID = DBEntities.Courses.Max(c => (int?)c.CourseID) ?? 0;

                for (int i = 0; i < numberOfCourses; i++)
                {
                    Console.Write($"Enter the name of course #{i + 1}: ");
                    string courseName = Console.ReadLine();

                    Console.Write($"Enter the number of credits for course #{i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int credits))
                    {
                        Course newCourse = new Course
                        {
                            CourseID = currentMaxCourseID + 1,
                            CourseName = courseName,
                            Credits = credits
                        };

                        DBEntities.Courses.Add(newCourse);
                        DBEntities.SaveChanges();
                        Console.WriteLine("New course created. CourseID: {0}", newCourse.CourseID);
                    }
                    else
                    {
                        Console.WriteLine("Invalid credits. Unable to create the course.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
            }
        }

        static void ReadStudent(SCHOOLEntities DBEntities)
        {
            // Assuming you have a DataTable with data from the Employees table
            DataTable schoolDataTable = DBEntities.Students.ToDataTable<Student>();

            // Convert DataTable back to List<Employee>
            List<Student> convertedListEmployee = schoolDataTable.ToListFromDataTable<Student>();

            Console.WriteLine("printing Students");

            if (convertedListEmployee.Any())
            {
                Console.WriteLine("Printing all students:");

                foreach (var student in convertedListEmployee)
                {
                    Console.WriteLine($"StudentID: {student.StudentID}, FirstName: {student.FirstName}, LastName: {student.LastName}, Age: {student.Age}, GPA: {student.GPA}");
                }
            }
            else
            {
                Console.WriteLine("No students found.");
            }

        }

        static void ReadClass(SCHOOLEntities DBEntities)
        {
            // Assuming you have a DataTable with data from the Employees table
            DataTable classDataTable = DBEntities.Classes.ToDataTable<Class>();

            // Convert DataTable back to List<Employee>
            List<Class> convertedListClass = classDataTable.ToListFromDataTable<Class>();

            Console.WriteLine("printing class");

            if (convertedListClass.Any())
            {
                Console.WriteLine("Printing all classes:");

                foreach (var classEntity in convertedListClass)
                {
                    Console.WriteLine($"ClassID: {classEntity.ClassID}, ClassName: {classEntity.ClassName}, " +
                        $"Instructor: {classEntity.Instructor}, CourseID: {classEntity.CourseID}, StudentID: {classEntity.StudentID}");
                }
            }
            else
            {
                Console.WriteLine("No class found.");
            }

        }

        static void ReadCourse(SCHOOLEntities DBEntities)
        {
           
            DataTable courseDataTable = DBEntities.Courses.ToDataTable<Course>();
            
            List<Course> convertedListCourse = courseDataTable.ToListFromDataTable<Course>();

            Console.WriteLine("printing Courses");

            if (convertedListCourse.Any())
            {
                Console.WriteLine("Printing all courses:");

                foreach (var course in convertedListCourse)
                {
                    Console.WriteLine($"CourseID: {course.CourseID}, CourseName: {course.CourseName}, Credits: {course.Credits}");
                }
            }
            else
            {
                Console.WriteLine("No courses found.");
            }

        }

        static void  DeleteStudentByID(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the StudentID to delete: ");

            if (int.TryParse(Console.ReadLine(), out int studentIdToDelete))
            {
                // Check if the student exists
                var studentToDelete = DBEntities.Students.Find(studentIdToDelete);

                if (studentToDelete != null)
                {
                    // Now, delete the student
                    DBEntities.Students.Remove(studentToDelete);
                    DBEntities.SaveChanges();
                    Console.WriteLine("Student with StudentID {0} deleted.", studentIdToDelete);
                }
                else
                {
                    Console.WriteLine("Student with StudentID {0} does not exist.", studentIdToDelete);
                }
            }
            else
            {
                Console.WriteLine("Invalid StudentID. Unable to delete the student.");
            }
        }

        static void DeleteCourseByID(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the CourseID to delete: ");

            if (int.TryParse(Console.ReadLine(), out int courseIdToDelete))
            {
                // Check if the course exists
                var courseToDelete = DBEntities.Courses.Find(courseIdToDelete);

                if (courseToDelete != null)
                {
                    // Now, delete the course
                    DBEntities.Courses.Remove(courseToDelete);
                    DBEntities.SaveChanges();
                    Console.WriteLine("Course with CourseID {0} deleted.", courseIdToDelete);
                }
                else
                {
                    Console.WriteLine("Course with CourseID {0} does not exist.", courseIdToDelete);
                }
            }
            else
            {
                Console.WriteLine("Invalid CourseID. Unable to delete the course.");
            }
        }

        static void DeleteClassByID(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the ClassID to delete: ");

            if (int.TryParse(Console.ReadLine(), out int classIdToDelete))
            {
                // Check if the class exists
                var classToDelete = DBEntities.Classes.Find(classIdToDelete);

                if (classToDelete != null)
                {
                    // Now, delete the class
                    DBEntities.Classes.Remove(classToDelete);
                    DBEntities.SaveChanges();
                    Console.WriteLine("Class with ClassID {0} deleted.", classIdToDelete);
                }
                else
                {
                    Console.WriteLine("Class with ClassID {0} does not exist.", classIdToDelete);
                }
            }
            else
            {
                Console.WriteLine("Invalid ClassID. Unable to delete the class.");
            }
        }

        static void UpdateCourseByID(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the CourseID to update: ");

            if (int.TryParse(Console.ReadLine(), out int courseIdToUpdate))
            {
                // Check if the course exists
                var courseToUpdate = DBEntities.Courses.Find(courseIdToUpdate);

                if (courseToUpdate != null)
                {
                    Console.Write("Enter the updated name for the course: ");
                    string updatedCourseName = Console.ReadLine();

                    Console.Write("Enter the updated number of credits for the course: ");
                    if (int.TryParse(Console.ReadLine(), out int updatedCredits))
                    {
                        courseToUpdate.CourseName = updatedCourseName;
                        courseToUpdate.Credits = updatedCredits;

                        DBEntities.SaveChanges();
                        Console.WriteLine("Course with CourseID {0} updated.", courseIdToUpdate);
                    }
                    else
                    {
                        Console.WriteLine("Invalid credits. Unable to update the course.");
                    }
                }
                else
                {
                    Console.WriteLine("Course with CourseID {0} does not exist.", courseIdToUpdate);
                }
            }
            else
            {
                Console.WriteLine("Invalid CourseID. Unable to update the course.");
            }
        }

        static void UpdateClassByID(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the ClassID to update: ");

            if (int.TryParse(Console.ReadLine(), out int classIdToUpdate))
            {
                // Check if the class exists
                var classToUpdate = DBEntities.Classes.Find(classIdToUpdate);

                if (classToUpdate != null)
                {
                    Console.Write("Enter the updated name for the class: ");
                    string updatedClassName = Console.ReadLine();

                    Console.Write("Enter the updated instructor for the class: ");
                    string updatedInstructor = Console.ReadLine();

                    Console.Write("Enter the updated CourseID for the class: ");
                    if (int.TryParse(Console.ReadLine(), out int updatedCourseId))
                    {
                        Console.Write("Enter the updated StudentID for the class: ");
                        if (int.TryParse(Console.ReadLine(), out int updatedStudentId))
                        {
                            classToUpdate.ClassName = updatedClassName;
                            classToUpdate.Instructor = updatedInstructor;
                            classToUpdate.CourseID = updatedCourseId;
                            classToUpdate.StudentID = updatedStudentId;

                            DBEntities.SaveChanges();
                            Console.WriteLine("Class with ClassID {0} updated.", classIdToUpdate);
                        }
                        else
                        {
                            Console.WriteLine("Invalid updated StudentID. Unable to update the class.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid updated CourseID. Unable to update the class.");
                    }
                }
                else
                {
                    Console.WriteLine("Class with ClassID {0} does not exist.", classIdToUpdate);
                }
            }
            else
            {
                Console.WriteLine("Invalid ClassID. Unable to update the class.");
            }
        }

        static void UpdateStudentByID(SCHOOLEntities DBEntities)
        {
            Console.Write("Enter the StudentID to update: ");

            if (int.TryParse(Console.ReadLine(), out int studentIdToUpdate))
            {
                // Check if the student exists
                var studentToUpdate = DBEntities.Students.Find(studentIdToUpdate);

                if (studentToUpdate != null)
                {
                    Console.Write("Enter the updated first name for the student: ");
                    string updatedFirstName = Console.ReadLine();

                    Console.Write("Enter the updated last name for the student: ");
                    string updatedLastName = Console.ReadLine();

                    Console.Write("Enter the updated age for the student: ");
                    if (int.TryParse(Console.ReadLine(), out int updatedAge))
                    {
                        Console.Write("Enter the updated GPA for the student: ");
                        if (double.TryParse(Console.ReadLine(), out double updatedGPA))
                        {
                            studentToUpdate.FirstName = updatedFirstName;
                            studentToUpdate.LastName = updatedLastName;
                            studentToUpdate.Age = updatedAge;
                            studentToUpdate.GPA = updatedGPA;

                            DBEntities.SaveChanges();
                            Console.WriteLine("Student with StudentID {0} updated.", studentIdToUpdate);
                        }
                        else
                        {
                            Console.WriteLine("Invalid updated GPA. Unable to update the student.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid updated age. Unable to update the student.");
                    }
                }
                else
                {
                    Console.WriteLine("Student with StudentID {0} does not exist.", studentIdToUpdate);
                }
            }
            else
            {
                Console.WriteLine("Invalid StudentID. Unable to update the student.");
            }
        }

    }


    internal static class DataTableExtensions
    {
        public static List<T> ToListFromDataTable<T>(this DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();
                foreach (DataColumn column in dataTable.Columns)
                {
                    var property = typeof(T).GetProperty(column.ColumnName);

                    if (property != null && row[column] != DBNull.Value)
                    {
                        Type propertyType = property.PropertyType;

                        // Check if the property is Nullable
                        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        }

                        try
                        {
                            // Attempt to convert the value
                            property.SetValue(obj, Convert.ChangeType(row[column], propertyType));
                        }
                        catch (InvalidCastException)
                        {
                            // Handle the conversion failure (optional)
                            // Console.WriteLine($"Conversion failed for column {column.ColumnName}. Skipping.");
                        }
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable();

            if (items.Any())
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                foreach (var item in items)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

    }
}

  