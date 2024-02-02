using SchoolCRUD.BusinessLayer;
using SchoolCRUD.UtilityLayer;
using SchoolCRUD.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using Enum = SchoolCRUD.UtilityLayer.Enum;

namespace SchoolCRUD
{
    public class Helper
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine($"{(int)Enum.MenuOption.CreateStudent}. Create Student");
            Console.WriteLine($"{(int)Enum.MenuOption.CreateClass}. Create Class");
            Console.WriteLine($"{(int)Enum.MenuOption.CreateCourse}. Create Course");
            Console.WriteLine($"{(int)Enum.MenuOption.CreateEnrollment}. Create Enrollment");
            Console.WriteLine($"{(int)Enum.MenuOption.CreateTeacher}. Create Teacher");
            Console.WriteLine($"{(int)Enum.MenuOption.UpdateStudent}. Update Student By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.UpdateClass}. Update Class By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.UpdateCourse}. Update Course By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.UpdateEnrollment}. Update Enrollment By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.UpdateTeacher}. Update Teacher By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.DeleteStudent}. Delete Student By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.DeleteClass}. Delete Class By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.DeleteCourse}. Delete Course By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.DeleteEnrollment}. Delete Enrollment By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.DeleteTeacher}. Delete Teacher By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.DisplayStudent}. Display Student");
            Console.WriteLine($"{(int)Enum.MenuOption.DisplayClass}. Display Class");
            Console.WriteLine($"{(int)Enum.MenuOption.DisplayCourse}. Display Course");
            Console.WriteLine($"{(int)Enum.MenuOption.DisplayEnrollment}. Display Enrollment");
            Console.WriteLine($"{(int)Enum.MenuOption.DisplayTeacher}. Display Teacher");
            Console.WriteLine($"{(int)Enum.MenuOption.SearchStudentByID}. Search Student By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.SearchClassByID}. Search Class By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.SearchCourseByID}. Search Course By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.SearchEnrollmentByID}. Search Enrollment By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.SearchTeacherByID}. Search Teacher By ID");
            Console.WriteLine($"{(int)Enum.MenuOption.GetStudentsWithClassesAndCourses}. Display Students with their respective classes and courses.");
            Console.WriteLine($"{(int)Enum.MenuOption.ExpoterDecider}. Where You want to write data?.");
            Console.WriteLine($"{(int)Enum.MenuOption.DisplayAllData}. ALL JOIN DATA DISPLAY.");
            Console.WriteLine($"{(int)Enum.MenuOption.Exit}. Exit");
        }

        public static void DisplayAllData()
        {
            List<ClassTeacherCourseStudent> classT = Business.GetClassTeacherCourseStudents();
            string enrollmentExcelFolderPath1 = ConfigurationManager.AppSettings["EnrollmentExcelFolderPath"];            
            ExcelExpoterClassTeacherCourseStudent.ExportToExcel(enrollmentExcelFolderPath1, classT);
        }

        public static void ExpoterDecider()
        {
            Console.WriteLine("Press 1 for Writting in CSV File");
            Console.WriteLine("Press 2 for Writting in Excel Sheet");
            Console.WriteLine("Press 3 for Writting in both CSV File and Excel Sheet");
            int input = ValidateData.GetValidIntegerInput("Export");
            if (input == 1)
            {
                CSVExpoterHelper();
            }
            else if (input == 2)
            {
                ExcelExpoterHelper();
            }
            else if (input == 3)
            {
                CSVExpoterHelper();
                ExcelExpoterHelper();
            }
            else
            {
                Console.WriteLine("Enter a valid input");
                Console.ReadLine();
            }
        }

        public static StudentViewModel GetStudentInformation()
        {
            Console.Write("Enter the first name of student: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name of student: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter the age of student: ");
            int age = ValidateData.GetValidIntegerInput("Age");

            Console.Write("Enter the GPA of student: ");
            double gpa = ValidateData.GetValidDoubleInput("GPA");

            return new StudentViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                GPA = gpa
            };
        }

        public static ClassViewModel GetClassInformation()
        {
            Console.Write($"Enter the name of class: ");
            string className = Console.ReadLine();

            return new ClassViewModel
            {
               // ClassID = classID,
                ClassName = className             
            };
        }

        public static CourseViewModel GetCourseInformation()
        {
            Console.Write($"Enter the CourseName for course: ");
            string courseName = Console.ReadLine();

            Console.Write($"Enter the Credits for the course: ");
            int credits = ValidateData.GetValidIntegerInput("Credit");

            return new CourseViewModel
            {
                CourseName = courseName,
                Credits = credits
            };
        }

        public static EnrollmentViewModel GetEnrollmentInformation()
        {
            Console.Write("Enter Course ID: ");
            int courseId = ValidateData.GetValidIntegerInput("CourseID");

            Console.Write("Enter Student ID: ");
            int studentId = ValidateData.GetValidIntegerInput("studentID");

            return new EnrollmentViewModel
            {
                CourseID = courseId,
                StudentID = studentId
            };

        }

        public static TeacherViewModel GetTeacher()
        {
            Console.WriteLine("Enter Teacher Name");
            string teacherName = Console.ReadLine();

            Console.WriteLine("Enter Class ID");
            int classID = ValidateData.GetValidIntegerInput("classID");

            Console.WriteLine("Enter Course ID");
            int courseId = ValidateData.GetValidIntegerInput("courseID");

            return new TeacherViewModel
            {
                TeacherName = teacherName,
                CourseID = courseId,
                ClassID = classID
            };
        }

        public static void CreateStudent()
        {
            try
            {
                StudentViewModel studentInfo = GetStudentInformation();
                string msg = Business.CreateNewStudent(studentInfo);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void CreateClass()
        {
            try
            {
                ClassViewModel classInfo = GetClassInformation();
                string msg = Business.CreateNewClass(classInfo);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void CreateCourse()
        {
            try
            {
                CourseViewModel courseInfo = GetCourseInformation();
                string msg = Business.CreateNewCourse(courseInfo);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
        
        public static void CreateEnrollment()
        {
            try
            {
                EnrollmentViewModel classInfo = GetEnrollmentInformation();
                string msg = Business.CreateNewEnrollment(classInfo);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void CreateTeacher()
        {
            try
            {
                TeacherViewModel teacherViewModel = GetTeacher();
                string msg = Business.CreateNewTeacher(teacherViewModel);
                Console.WriteLine(msg);
            }catch(Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void UpdateStudent()
        {
            try
            {
                Console.Write("Enter the student ID to update: ");
                int studentIdToUpdate = ValidateData.GetValidIntegerInput("Student ID");

                StudentViewModel studentInfoUpdate = GetStudentInformation();
                studentInfoUpdate.StudentID = studentIdToUpdate;

                string msg = Business.UpdateStudentByID(studentInfoUpdate);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void UpdateClass()
        {
            try
            {
                Console.Write("Enter the ClassID to update: ");
                int classIDUpdate = ValidateData.GetValidIntegerInput("ClassID");

                ClassViewModel classInfoUpdate = GetClassInformation();
                classInfoUpdate.ClassID = classIDUpdate;

                string msg = Business.UpdateClassById(classInfoUpdate);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void UpdateCourse()
        {
            try
            {
                Console.Write("Enter the CourseID to update: ");
                int courseIDUpdate = ValidateData.GetValidIntegerInput("CourseID");

                CourseViewModel courseInfoUpdate = Helper.GetCourseInformation();
                courseInfoUpdate.CourseID = courseIDUpdate;

                string msg = Business.UpdateCourseByID(courseInfoUpdate);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
       
        public static void UpdateTeacher()
        {
            try
            {
                Console.Write("Enter the TeacherID to update: ");
                int teacherIDUpdate = ValidateData.GetValidIntegerInput("TeacherID");

                TeacherViewModel enrollmentInfoUpdate = Helper.GetTeacher();
                enrollmentInfoUpdate.TeacherID = teacherIDUpdate;

                string msg = Business.UpdateTeacherByID(enrollmentInfoUpdate);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
        
        public static void UpdateEnrollment()
        {
            try
            {
                Console.Write("Enter the EnrollmentID to update: ");
                int enrollmentIDUpdate = ValidateData.GetValidIntegerInput("EnrollmentID");

                EnrollmentViewModel enrollmentInfoUpdate = Helper.GetEnrollmentInformation();
                enrollmentInfoUpdate.EnrollmentID = enrollmentIDUpdate;

                string msg = Business.UpdateEnrollmentByID(enrollmentInfoUpdate);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DeleteStudent()
        {
            try
            {

                Console.Write("Enter the StudentID to delete: ");
                int studentID = ValidateData.GetValidIntegerInput("StudentID");
                string msg = Business.DeleteStudentByID(studentID);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DeleteClass()
        {
            try
            {
                Console.Write("Enter the ClassID to delete: ");
                int classID = ValidateData.GetValidIntegerInput("ClassID");

                string msg = Business.DeleteClassByID(classID);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DeleteCourse()
        {
            try
            {
                Console.Write("Enter the CourseID to delete: ");
                int courseID = ValidateData.GetValidIntegerInput("CourseID");

                string msg = Business.DeleteCourseByID(courseID);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
        
        public static void DeleteEnrollment()
        {
            try
            {
                Console.Write("Enter the EnrollmentID to delete: ");
                int EnrollmentID = ValidateData.GetValidIntegerInput("EnrollmentID");

                string msg = Business.DeleteEnrollmentByID(EnrollmentID);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
        
        public static void DeleteTeacher()
        {
            try
            {
                Console.Write("Enter the TeacherID to delete: ");
                int teacherID = ValidateData.GetValidIntegerInput("teacherID");

                string msg = Business.DeleteTeacherByID(teacherID);
                Console.WriteLine(msg);
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DisplayStudent()
        {
            try
            {
                Console.WriteLine("Displaying students");

                Dictionary<string, List<StudentViewModel>> studentsResult = Business.ReadStudent();

                if (studentsResult.ContainsKey("Success"))
                {
                    List<StudentViewModel> students = studentsResult["Success"];

                    if (students.Count > 0)
                    {
                        Console.WriteLine("Students:");
                        foreach (var student in students)
                        {
                            Console.WriteLine($"StudentID: {student.StudentID}, FirstName: {student.FirstName}, LastName: {student.LastName}, Age: {student.Age}, GPA: {student.GPA}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No students found.");
                    }
                }
                else if (studentsResult.ContainsKey("Error"))
                {
                    Console.WriteLine($"Error: {studentsResult["Error"]}");

                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DisplayClasses()
        {
            try
            {
                Console.WriteLine("Displaying classes");

                Dictionary<string, List<ClassViewModel>> classesResult = Business.ReadClass();

                if (classesResult.ContainsKey("Success"))
                {
                    List<ClassViewModel> classes = classesResult["Success"];

                    if (classes.Count > 0)
                    {
                        Console.WriteLine("Classes:");
                        foreach (var classObj in classes)
                        {
                            Console.WriteLine($"ClassID: {classObj.ClassID}, ClassName: {classObj.ClassName}");
                            // Add other properties as needed
                        }
                    }
                    else
                    {
                        Console.WriteLine("No classes found.");
                    }
                }
                else if (classesResult.ContainsKey("Error"))
                {
                    Console.WriteLine($"Error: {classesResult["Error"]}");
                    // Log or handle the error message
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DisplayCourses()
        {
            try
            {
                Console.WriteLine("Displaying courses");

                Dictionary<string, List<CourseViewModel>> coursesResult = Business.ReadCourse();

                if (coursesResult.ContainsKey("Success"))
                {
                    List<CourseViewModel> courses = coursesResult["Success"];

                    if (courses.Count > 0)
                    {
                        Console.WriteLine("Courses:");
                        foreach (var course in courses)
                        {
                            Console.WriteLine($"CourseID: {course.CourseID}, CourseName: {course.CourseName}, Credits: {course.Credits}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No courses found.");
                    }
                }
                else if (coursesResult.ContainsKey("Error"))
                {
                    Console.WriteLine($"Error: {coursesResult["Error"]}");
                    // Log or handle the error message
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void DisplayEnrollments()
        {
            try
            {
                Console.WriteLine("Displaying enrollments");

                Dictionary<string, List<EnrollmentViewModel>> enrollmentsResult = Business.ReadEnrollment();

                if (enrollmentsResult.ContainsKey("Success"))
                {
                    List<EnrollmentViewModel> enrollments = enrollmentsResult["Success"];

                    if (enrollments.Count > 0)
                    {
                        Console.WriteLine("Enrollments:");
                        foreach (var enrollment in enrollments)
                        {
                            Console.WriteLine($"EnrollmentID: {enrollment.EnrollmentID}, StudentID: {enrollment.StudentID}, CourseID: {enrollment.CourseID}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No enrollments found.");
                    }
                }
                else if (enrollmentsResult.ContainsKey("Error"))
                {
                    Console.WriteLine($"Error: {enrollmentsResult["Error"]}");
                    // Log or handle the error message
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
        
        public static void DisplayTeachers()
        {
            try
            {
                Console.WriteLine("Displaying teacher");

                Dictionary<string, List<TeacherViewModel>> teacherResult = Business.ReadTeacher();

                if (teacherResult.ContainsKey("Success"))
                {
                    List<TeacherViewModel> teachers = teacherResult["Success"];

                    if (teachers.Count > 0)
                    {
                        Console.WriteLine("Enrollments:");
                        foreach (var teacher in teachers)
                        {
                            Console.WriteLine($"TeacherID: {teacher.TeacherID}, TeacherName: {teacher.TeacherName}, ClassID: {teacher.ClassID}, CourseID: {teacher.CourseID}");
                        }

                    }
                    else
                    {
                        Console.WriteLine("No enrollments found.");
                    }
                }
                else if (teacherResult.ContainsKey("Error"))
                {
                    Console.WriteLine($"Error: {teacherResult["Error"]}");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void SearchStudentByID()
        {
            try
            {
                Console.WriteLine("Enter the StudentID to Search");

                int studentId = ValidateData.GetValidIntegerInput("studentId");

                Dictionary<string, List<string>> stringMapStudent = Business.GetStudentByID(studentId);

                if (stringMapStudent.TryGetValue("Success", out var successAttributes))
                {
                    Console.WriteLine("Successfully found the ID. Enrollment Details:");
                    Console.WriteLine($"StudentID: {studentId}");
                    Console.WriteLine($"FirstName: {successAttributes[0]}");
                    Console.WriteLine($"LastName: {successAttributes[1]}");
                    Console.WriteLine($"Age: {successAttributes[2]}");
                    Console.WriteLine($"GPA: {successAttributes[3]}");
                }            
                else
                {
                    Console.WriteLine("Not able to find the ID.");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }

        }

        public static void SearchClassByID()
        {
            try
            {
                Console.WriteLine("Enter the ClassID to Search");

                int searchClassID = ValidateData.GetValidIntegerInput("searchClassID");

                Dictionary<string, List<string>> stringMapClass = Business.GetClassByID(searchClassID);

                if (stringMapClass.TryGetValue("Success", out var successAttributes))
                {
                    Console.WriteLine("Successfully found the ID. Enrollment Details:");
                    Console.WriteLine($"ClassID: {searchClassID}");
                    Console.WriteLine($"ClassName: {successAttributes[0]}");
                    Console.WriteLine($"Instructor: {successAttributes[1]}");
                    Console.WriteLine($"StudentID: {successAttributes[2]}");
                    Console.WriteLine($"CourseID: {successAttributes[3]}");
                }
                else
                {
                    Console.WriteLine("Not able to find the ID.");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void SearchCourseByID()
        {
            try
            {
                Console.WriteLine("Enter the CourseID to Search");

                int searchCourseID = ValidateData.GetValidIntegerInput("searchCourseID");

                Dictionary<string, List<string>> stringMapCourse = Business.GetCourseByID(searchCourseID);

                if (stringMapCourse.TryGetValue("Success", out var successAttributes))
                {
                    Console.WriteLine("Successfully found the ID. Enrollment Details:");
                    Console.WriteLine($"CourseID: {successAttributes[0]}");
                    Console.WriteLine($"CourseName: {successAttributes[1]}");
                    Console.WriteLine($"Credits: {successAttributes[2]}");
                }
                else
                {
                    Console.WriteLine("Not able to find the ID.");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void SearchEnrollmentByID()
        {
            try
            {
                Console.WriteLine("Enter the EnrollmentID to Search");

                int searchEnrollmentID = ValidateData.GetValidIntegerInput("searchEnrollmentID");

                Dictionary<string, List<string>> stringMapEnrollment = Business.GetEnrollmentByID(searchEnrollmentID);

                if (stringMapEnrollment.TryGetValue("Success", out var successAttributes))
                {
                    Console.WriteLine("Successfully found the ID. Enrollment Details:");
                    Console.WriteLine($"EnrollmentID: {searchEnrollmentID}");
                    Console.WriteLine($"StudentID: {successAttributes[0]}");
                    Console.WriteLine($"CourseID: {successAttributes[1]}");
                }
                else
                {
                    Console.WriteLine("Not able to find the ID.");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
       
        public static void SearchTeacherByID()
        {
            try
            {
                Console.WriteLine("Enter the TeacherID to Search");

                int searchTeacherID = ValidateData.GetValidIntegerInput("searchTeacherID");

                Dictionary<string, List<string>> stringMapTeacher = Business.GetEnrollmentByID(searchTeacherID);

                if (stringMapTeacher.TryGetValue("Success", out var successAttributes))
                {
                    Console.WriteLine("Successfully found the ID. Teacher Details:");
                    Console.WriteLine($"TeacherID: {searchTeacherID}");
                    Console.WriteLine($"TeacherName: {successAttributes[0]}");
                    Console.WriteLine($"ClassID: {successAttributes[1]}");
                    Console.WriteLine($"CourseID: {successAttributes[1]}");
                }
                else
                {
                    Console.WriteLine("Not able to find the ID.");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void GetStudentWithCourses()
        {
            try
            {
                Dictionary<string, List<StudentViewModel>> studentsWithCourses = Business.GetStudentsWithCourses();

                if (studentsWithCourses.ContainsKey("Success"))
                {
                    List<StudentViewModel> students = studentsWithCourses["Success"];

                    foreach (var student in students)
                    {
                        // Print student details
                        Console.WriteLine($"Student Details:\n" +
                                          $"ID: {student.StudentID}  " +
                                          $"Name: {student.FirstName} {student.LastName}" +
                                          $"Age: {student.Age}  " +
                                          $"GPA: {student.GPA}");

                        // Print enrolled courses
                        Console.WriteLine("Enrolled Courses:");
                        foreach (var course in student.Courses)
                        {
                            Console.WriteLine($"  CourseID: {course.CourseID}  " +
                                              $"Course Name: {course.CourseName}  " +
                                              $"Credits: {course.Credits}");
                        }

                        Console.WriteLine(); // Add a line break between students
                    }
                }
                else
                {
                    string errorMessage = "Something went wrong while fetching students with courses.";
                    Console.WriteLine($"Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }
    

    public static void ExcelExpoterHelper()
        {
            try
            {
                Dictionary<string, List<StudentViewModel>> studentsResult = Business.ReadStudent();
                List<StudentViewModel> students = studentsResult["Success"];
                string studentExcelFolderPath = ConfigurationManager.AppSettings["StudentExcelFolderPath"];
                ExcelExporter.ExportToExcel(studentExcelFolderPath, students);

                //Dictionary<string, List<CourseViewModel>> courseResult = Business.ReadCourse();
                //List<CourseViewModel> courses = courseResult["Success"];
                //string courseExcelFolderPath = ConfigurationManager.AppSettings["CourseExcelFolderPath"];
                //ExcelExporter.ExportToExcel(courseExcelFolderPath, courses);

                //Dictionary<string, List<ClassViewModel>> classResult = Business.ReadClass();
                //List<ClassViewModel> classes = classResult["Success"];
                //string classExcelFolderPath = ConfigurationManager.AppSettings["ClassExcelFolderPath"];
                //ExcelExporter.ExportToExcel(classExcelFolderPath, classes);

                //Dictionary<string, List<EnrollmentViewModel>> enrollmentResult = Business.ReadEnrollment();
                //List<EnrollmentViewModel> enrolls = enrollmentResult["Success"];
                //string enrollmentExcelFolderPath = ConfigurationManager.AppSettings["EnrollmentExcelFolderPath"];
                //ExcelExporter.ExportToExcel(enrollmentExcelFolderPath, enrolls);



                Console.WriteLine("Data written to Excel file successfully.");
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

        public static void CSVExpoterHelper()
        {
            try
            {
                Dictionary<string, List<StudentViewModel>> studentsResult = Business.ReadStudent();
                List<StudentViewModel> students = studentsResult["Success"];

                Dictionary<string, List<ClassViewModel>> classResult = Business.ReadClass();
                List<ClassViewModel> classes = classResult["Success"];

                Dictionary<string, List<CourseViewModel>> courseResult = Business.ReadCourse();
                List<CourseViewModel> courses = courseResult["Success"]; 
                
                Dictionary<string, List<EnrollmentViewModel>> enrollmentResult = Business.ReadEnrollment();
                List<EnrollmentViewModel> enrollments = enrollmentResult["Success"];

                string studentCSVFolderPath = ConfigurationManager.AppSettings["StudentCSVFolderPath"];
                string classCSVFolderPath = ConfigurationManager.AppSettings["ClassCSVFolderPath"];
                string courseCSVFolderPath = ConfigurationManager.AppSettings["CourseCSVFolderPath"];
                string enrollmentCSVFolderPath = ConfigurationManager.AppSettings["EnrollmentCSVFolderPath"];

                CSVExporter.WriteToCsv(studentCSVFolderPath, students);
                CSVExporter.WriteToCsv(classCSVFolderPath, classes);
                CSVExporter.WriteToCsv(courseCSVFolderPath, courses);
                CSVExporter.WriteToCsv(enrollmentCSVFolderPath, enrollments);

                Console.WriteLine("Data written to CSV file successfully.");
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
            }
        }

    }

}
