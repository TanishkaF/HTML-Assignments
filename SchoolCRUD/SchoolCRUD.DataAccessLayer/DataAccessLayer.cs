using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolCRUD.ViewModel;
using SchoolCRUD.UtilityLayer;
using System.Runtime.Remoting.Contexts;

namespace SchoolCRUD.DataAccessLayer
{
    public class DataAccessLayer
    {
        public static string CreateNewStudent(StudentViewModel studentInfo)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    int currentMaxStudentID = DBEntities.Students.Max(s => (int?)s.StudentID) ?? 0;

                    Student newStudent = new Student
                    {
                        StudentID = currentMaxStudentID + 1,
                        FirstName = studentInfo.FirstName,
                        LastName = studentInfo.LastName,
                        Age = studentInfo.Age,
                        GPA = studentInfo.GPA
                    };

                    DBEntities.Students.Add(newStudent);
                    DBEntities.SaveChanges();
                    return $"New student created. StudentID: {newStudent.StudentID}";
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error creating new student: {ex.Message}";
            }
        }

        public static string CreateNewClass(ClassViewModel classInfo)
        {
            try
            {
                using (var DBEntities = new SCHOOLEntities())
                {
                    int currentMaxClassID = DBEntities.Classes.Max(c => (int?)c.ClassID) ?? 0;

                    // No need to check for existing course since CourseID is not part of Class entity

                    Class newClass = new Class
                    {
                        ClassID = currentMaxClassID + 1,
                        ClassName = classInfo.ClassName                        
                    };

                    DBEntities.Classes.Add(newClass);
                    DBEntities.SaveChanges();
                    return $"New class created. ClassID: {newClass.ClassID}";
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error creating new class: {ex.Message}";
            }
        }

        public static string CreateNewCourse(CourseViewModel courseInfo)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    int currentMaxCourseID = DBEntities.Courses.Max(c => (int?)c.CourseID) ?? 0;

                    Course newCourse = new Course
                    {
                        CourseID = currentMaxCourseID + 1,
                        CourseName = courseInfo.CourseName,
                        Credits = courseInfo.Credits
                    };

                    DBEntities.Courses.Add(newCourse);
                    DBEntities.SaveChanges();
                    return $"New course created. CourseID: {newCourse.CourseID}";
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error creating new course: {ex.Message}";
            }
        }

        public static string CreateNewEnrollment(EnrollmentViewModel enrollmentInfo)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    Enrollment newEnrollment = new Enrollment
                    {
                        StudentID = enrollmentInfo.StudentID,
                        CourseID = enrollmentInfo.CourseID
                    };

                    DBEntities.Enrollments.Add(newEnrollment);
                    DBEntities.SaveChanges();
                    return $"New enrollment created. EnrollmentID: {newEnrollment.EnrollmentID}";
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error creating new enrollment: {ex.Message}";
            }
        }

        public static string CreateNewTeacher(TeacherViewModel teacherInfo)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    int currentMaxTeacherID = DBEntities.Teachers.Max(t => (int?)t.TeacherID) ?? 0;

                    Teacher newTeacher = new Teacher
                    {
                        TeacherID = currentMaxTeacherID + 1,
                        ClassID = teacherInfo.ClassID,
                        CourseID = teacherInfo.CourseID
                    };

                    DBEntities.Teachers.Add(newTeacher);
                    DBEntities.SaveChanges();
                    return $"New teacher created. TeacherID: {newTeacher.TeacherID}";
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error creating new teacher: {ex.Message}";
            }
        }

        public static string UpdateStudentByID(StudentViewModel studentInfoUpdate)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var studentToUpdate = DBEntities.Students.Find(studentInfoUpdate.StudentID);

                    if (studentToUpdate != null)
                    {
                        // Update the properties of the existing student
                        studentToUpdate.FirstName = studentInfoUpdate.FirstName;
                        studentToUpdate.LastName = studentInfoUpdate.LastName;
                        studentToUpdate.Age = studentInfoUpdate.Age;
                        studentToUpdate.GPA = studentInfoUpdate.GPA;

                        DBEntities.SaveChanges();
                        return $"Student with StudentID {studentInfoUpdate.StudentID} updated.";
                    }
                    else
                    {
                        return $"Student with StudentID {studentInfoUpdate.StudentID} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error in updating student: {ex.Message}";
            }
        }

        public static string UpdateClassByID(ClassViewModel classInfoUpdate)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var classToUpdate = DBEntities.Classes.Find(classInfoUpdate.ClassID);

                    if (classToUpdate != null)
                    {
                        classToUpdate.ClassName = classInfoUpdate.ClassName;
                                        

                        DBEntities.SaveChanges();
                        return $"Class with ClassID {classInfoUpdate.ClassID} updated.";
                    }
                    else
                    {
                        return $"Invalid ClassID. Unable to update the class.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error updating the class: {ex.Message}";
            }

        }

        public static string UpdateCourseByID(CourseViewModel courseInfoUpdate)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var courseToUpdate = DBEntities.Courses.Find(courseInfoUpdate.CourseID);

                    if (courseToUpdate != null)
                    {
                        courseToUpdate.CourseName = courseInfoUpdate.CourseName;
                        courseToUpdate.Credits = courseInfoUpdate.Credits;

                        DBEntities.SaveChanges();
                        return $"Course with CourseID {courseInfoUpdate.CourseID} updated.";
                    }

                    else
                    {
                        return $"Course with CourseID {courseInfoUpdate.CourseID} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error updating course: {ex.Message}";
            }

        }

        public static string UpdateEnrollmentByID(EnrollmentViewModel enrollmentInfoUpdate)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var enrollmentToUpdate = DBEntities.Enrollments.Find(enrollmentInfoUpdate.EnrollmentID);

                    if (enrollmentToUpdate != null)
                    {
                        enrollmentToUpdate.StudentID = enrollmentInfoUpdate.StudentID;
                        enrollmentToUpdate.CourseID = enrollmentInfoUpdate.CourseID;

                        DBEntities.SaveChanges();
                        return $"Enrollment with EnrollmentID {enrollmentInfoUpdate.EnrollmentID} updated.";
                    }
                    else
                    {
                        return $"Enrollment with EnrollmentID {enrollmentInfoUpdate.EnrollmentID} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error updating enrollment: {ex.Message}";
            }
        }

        public static string UpdateTeacherByID(TeacherViewModel teacherInfoUpdate)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var teacherToUpdate = DBEntities.Teachers.Find(teacherInfoUpdate.TeacherID);

                    if (teacherToUpdate != null)
                    {
                        teacherToUpdate.TeacherName = teacherInfoUpdate.TeacherName;
                        teacherToUpdate.ClassID = teacherInfoUpdate.ClassID;
                        teacherToUpdate.CourseID = teacherInfoUpdate.CourseID;

                        DBEntities.SaveChanges();
                        return $"Teacher with TeacherID {teacherInfoUpdate.TeacherID} updated.";
                    }
                    else
                    {
                        return $"Teacher with TeacherID {teacherInfoUpdate.TeacherID} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error updating teacher: {ex.Message}";
            }
        }
        
        public static string DeleteStudentByID(int studentIdToDelete)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var studentToDelete = DBEntities.Students.Find(studentIdToDelete);

                    if (studentToDelete != null)
                    {
                        DBEntities.Students.Remove(studentToDelete);
                        DBEntities.SaveChanges();
                        return $"Student with StudentID {studentIdToDelete} deleted.";
                    }
                    else
                    {
                        return $"Student with StudentID {studentIdToDelete} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error deleting the student: {ex.Message}";
            }
        }

        public static string DeleteClassByID(int classIdToDelete)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var classToDelete = DBEntities.Classes.Find(classIdToDelete);

                    if (classToDelete != null)
                    {
                        // Now, delete the class
                        DBEntities.Classes.Remove(classToDelete);
                        DBEntities.SaveChanges();
                        return $"Class with ClassID {classIdToDelete} deleted.";
                    }
                    else
                    {
                        return $"Class with ClassID {classIdToDelete} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error deleting the class: {ex.Message}";
            }
        }

        public static string DeleteCourseByID(int courseIdToDelete)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var courseToDelete = DBEntities.Courses.Find(courseIdToDelete);

                    if (courseToDelete != null)
                    {
                        // Now, delete the course
                        DBEntities.Courses.Remove(courseToDelete);
                        DBEntities.SaveChanges();
                        return $"Course with CourseID {courseIdToDelete} deleted.";
                    }
                    else
                    {
                        return $"Course with CourseID {courseIdToDelete} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error deleting the course: {ex.Message}";
            }
        }

        public static string DeleteEnrollmentByID(int enrollmentIDToDelete)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var enrollmentToDelete = DBEntities.Enrollments.Find(enrollmentIDToDelete);

                    if (enrollmentToDelete != null)
                    {
                        // Now, delete the enrollment
                        DBEntities.Enrollments.Remove(enrollmentToDelete);
                        DBEntities.SaveChanges();
                        return $"Enrollment with EnrollmentID {enrollmentIDToDelete} deleted.";
                    }
                    else
                    {
                        return $"Enrollment with EnrollmentID {enrollmentIDToDelete} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error deleting the enrollment: {ex.Message}";
            }
        }

        public static string DeleteTeacherByID(int teacherIDToDelete)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    var teacherToDelete = DBEntities.Teachers.Find(teacherIDToDelete);

                    if (teacherToDelete != null)
                    {
                        // Now, delete the teacher
                        DBEntities.Teachers.Remove(teacherToDelete);
                        DBEntities.SaveChanges();
                        return $"Teacher with TeacherID {teacherIDToDelete} deleted.";
                    }
                    else
                    {
                        return $"Teacher with TeacherID {teacherIDToDelete} does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return $"Error deleting the teacher: {ex.Message}";
            }
        }


        public static Dictionary<string, List<string>> GetStudentByID(int studentId)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    Dictionary<string, List<string>> stringMap = new Dictionary<string, List<string>>();
                    List<string> studentAttributes = new List<string>();
                    var foundStudent = DBEntities.Students.FirstOrDefault(s => s.StudentID == studentId);

                    if (foundStudent != null)
                    {
                        // Populate the list with attributes
                        studentAttributes.Add(foundStudent.FirstName);
                        studentAttributes.Add(foundStudent.LastName);
                        studentAttributes.Add(foundStudent.Age.ToString());
                        studentAttributes.Add(foundStudent.GPA.ToString());

                        stringMap["Success"] = studentAttributes;
                        //return $"Successfully found the ID";
                        return stringMap;
                    }
                    else
                    {
                        stringMap["Error"] = null;
                        return stringMap;
                        //return $"Not able to find the ID";
                    }

                }
            }

            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return default;
                // return $"Error reading the course: {ex.Message}";
            }
        }

        public static Dictionary<string, List<string>> GetClassByID(int classID)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    Dictionary<string, List<string>> stringMap = new Dictionary<string, List<string>>();
                    List<string> classAttributes = new List<string>();
                    var foundClass = DBEntities.Classes.FirstOrDefault(s => s.ClassID == classID);

                    if (foundClass != null)
                    {
                        classAttributes.Add(foundClass.ClassName);
                       
                        
                        stringMap["Success"] = classAttributes;
                        return stringMap;
                    }
                    else
                    {
                        stringMap["Error"] = null;
                        return stringMap;
                        //return $"Not able to find the ID";
                    }

                }
            }

            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return default;
                // return $"Error reading the course: {ex.Message}";
            }
        }

        public static Dictionary<string, List<string>> GetCourseByID(int courseID)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    Dictionary<string, List<string>> stringMap = new Dictionary<string, List<string>>();
                    List<string> courseAttributes = new List<string>();
                    var foundCourse = DBEntities.Courses.FirstOrDefault(s => s.CourseID == courseID);

                    if (foundCourse != null)
                    {
                        courseAttributes.Add(foundCourse.CourseName);
                        // courseAttributes.Add(foundCourse.CourseID.ToString());
                        courseAttributes.Add(foundCourse.Credits.ToString());

                        stringMap["Success"] = courseAttributes;
                        return stringMap;
                    }
                    else
                    {
                        stringMap["Error"] = null;
                        return stringMap;
                        //return $"Not able to find the ID";
                    }

                }
            }

            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return default;
                // return $"Error reading the course: {ex.Message}";
            }
        }

        public static Dictionary<string, List<string>> GetEnrollmentByID(int enrollmentID)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    Dictionary<string, List<string>> stringMap = new Dictionary<string, List<string>>();
                    List<string> enrollmentAttributes = new List<string>();
                    var foundEnrollment = DBEntities.Enrollments.FirstOrDefault(e => e.EnrollmentID == enrollmentID);

                    if (foundEnrollment != null)
                    {
                        enrollmentAttributes.Add(foundEnrollment.StudentID.ToString());
                        enrollmentAttributes.Add(foundEnrollment.CourseID.ToString());

                        stringMap["Success"] = enrollmentAttributes;
                        return stringMap;
                    }
                    else
                    {
                        stringMap["Error"] = null;
                        return stringMap;
                        // return $"Not able to find the ID";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return default;
                // return $"Error reading the enrollment: {ex.Message}";
            }
        }
        
        public static Dictionary<string, List<string>> GetTeacherByID(int teacherID)
        {
            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    Dictionary<string, List<string>> stringMap = new Dictionary<string, List<string>>();
                    List<string> teacherAttributes = new List<string>();
                    var foundTeacher = DBEntities.Teachers.FirstOrDefault(t => t.TeacherID == teacherID);

                    if (foundTeacher != null)
                    {
                        teacherAttributes.Add(foundTeacher.ClassID.ToString());
                        teacherAttributes.Add(foundTeacher.CourseID.ToString());

                        stringMap["Success"] = teacherAttributes;
                        return stringMap;
                    }
                    else
                    {
                        stringMap["Error"] = null;
                        return stringMap;
                        // return $"Not able to find the ID";
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                return default;
                // return $"Error reading the teacher: {ex.Message}";
            }
        }

        public static Dictionary<string, List<StudentViewModel>> ReadStudent()
        {
            Dictionary<string, List<StudentViewModel>> result = new Dictionary<string, List<StudentViewModel>>();

            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    List<Student> convertedListStudent = DBEntities.Students.ToList();

                    List<StudentViewModel> students = new List<StudentViewModel>();

                    foreach (Student s in convertedListStudent)
                    {
                        StudentViewModel studentViewModel = new StudentViewModel
                        {
                            StudentID = s.StudentID,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            Age = (int)s.Age,
                            GPA = (double) s.GPA
                        };

                        students.Add(studentViewModel);
                    }

                    result.Add("Success", students);
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                result.Add("Error", new List<StudentViewModel>());
            }

            return result;
        }

        public static Dictionary<string, List<ClassViewModel>> ReadClass()
        {
            Dictionary<string, List<ClassViewModel>> result = new Dictionary<string, List<ClassViewModel>>();

            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    List<Class> convertedListClass = DBEntities.Classes.ToList();

                    List<ClassViewModel> classes = new List<ClassViewModel>();

                    foreach (Class c in convertedListClass)
                    {
                        ClassViewModel classViewModel = new ClassViewModel
                        {
                            ClassID = c.ClassID,
                            ClassName = c.ClassName                                    
                        };

                        classes.Add(classViewModel);
                    }

                    result.Add("Success", classes);
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                result.Add("Error", new List<ClassViewModel>());
            }

            return result;
        }

        public static Dictionary<string, List<CourseViewModel>> ReadCourse()
        {
            Dictionary<string, List<CourseViewModel>> result = new Dictionary<string, List<CourseViewModel>>();

            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    List<Course> convertedListCourse = DBEntities.Courses.ToList();

                    List<CourseViewModel> courses = new List<CourseViewModel>();

                    foreach (Course c in convertedListCourse)
                    {
                        CourseViewModel courseViewModel = new CourseViewModel
                        {
                            CourseID = c.CourseID,
                            CourseName = c.CourseName,
                            Credits = (int)c.Credits
                        };

                        courses.Add(courseViewModel);
                    }

                    result.Add("Success", courses);
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                result.Add("Error", new List<CourseViewModel>());
            }

            return result;
        }

        public static Dictionary<string, List<EnrollmentViewModel>> ReadEnrollment()
        {
            Dictionary<string, List<EnrollmentViewModel>> result = new Dictionary<string, List<EnrollmentViewModel>>();

            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    List<Enrollment> convertedListEnrollment = DBEntities.Enrollments.ToList();

                    List<EnrollmentViewModel> enrollments = new List<EnrollmentViewModel>();

                    foreach (Enrollment e in convertedListEnrollment)
                    {
                        EnrollmentViewModel enrollmentViewModel = new EnrollmentViewModel
                        {
                            EnrollmentID = e.EnrollmentID,
                            StudentID = (int) e.StudentID,
                            CourseID = (int) e.CourseID 
                        };

                        enrollments.Add(enrollmentViewModel);
                    }

                    result.Add("Success", enrollments);
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                result.Add("Error", new List<EnrollmentViewModel>());
            }

            return result;
        }

        public static Dictionary<string, List<TeacherViewModel>> ReadTeacher()
        {
            Dictionary<string, List<TeacherViewModel>> result = new Dictionary<string, List<TeacherViewModel>>();

            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    List<Teacher> convertedListTeacher = DBEntities.Teachers.ToList();

                    List<TeacherViewModel> teachers = convertedListTeacher.Select(t => new TeacherViewModel
                    {
                        TeacherID = t.TeacherID,
                        ClassID = (int)t.ClassID,
                        CourseID = (int)t.CourseID
                    }).ToList();

                    result.Add("Success", teachers);
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                result.Add("Error", new List<TeacherViewModel>());
            }

            return result;
        }

        public static Dictionary<string, List<StudentViewModel>> GetStudentsWithCourses()
        {
            Dictionary<string, List<StudentViewModel>> result = new Dictionary<string, List<StudentViewModel>>();

            try
            {
                using (SCHOOLEntities DBEntities = new SCHOOLEntities())
                {
                    List<Student> convertedListStudent = DBEntities.Students.ToList();

                    List<StudentViewModel> studentsWithCourses = new List<StudentViewModel>();

                    foreach (Student studentEntity in convertedListStudent)
                    {
                        // Map Student entity to StudentViewModel
                        StudentViewModel studentViewModel = new StudentViewModel
                        {
                            StudentID = studentEntity.StudentID,
                            FirstName = studentEntity.FirstName,
                            LastName = studentEntity.LastName,
                            Age = (int)studentEntity.Age,
                            GPA = (double)studentEntity.GPA,
                            Classes = new List<ClassViewModel>(), // Initialize classes list
                            Courses = new List<CourseViewModel>() // Initialize courses list
                        };

                        // Retrieve enrolled courses for the current student
                        var enrolledCourses = DBEntities.Enrollments
                            .Where(e => e.StudentID == studentEntity.StudentID)
                            .Select(e => new CourseViewModel
                            {
                                CourseID = e.CourseID,
                                CourseName = e.Course.CourseName,
                                Credits = (int)e.Course.Credits  
                            })
                            .ToList();

                        studentViewModel.Courses.AddRange(enrolledCourses);

                        studentsWithCourses.Add(studentViewModel);
                    }

                    result.Add("Success", studentsWithCourses);
                }
            }
            catch (Exception ex)
            {
                LogManager.DecideLogInput(ex);
                result.Add("Error", new List<StudentViewModel>());
            }

            return result;
        }

    }
}