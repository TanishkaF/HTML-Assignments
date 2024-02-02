using System.Collections.Generic;
using SchoolCRUD.DataAccessLayer;
using SchoolCRUD.ViewModel;

namespace SchoolCRUD.BusinessLayer
{
    public class Business
    {
        public static string CreateNewStudent(StudentViewModel studentInfo)
        {
            return DataAccessLayer.DataAccessLayer.CreateNewStudent(studentInfo);
        }

        public static string CreateNewClass(ClassViewModel classInfo)
        {
            return DataAccessLayer.DataAccessLayer.CreateNewClass(classInfo);
        }

        public static string CreateNewCourse(CourseViewModel courseInfo)
        {
            return DataAccessLayer.DataAccessLayer.CreateNewCourse(courseInfo);
        }

        public static string CreateNewEnrollment(EnrollmentViewModel enrollmentInfo)
        {
            return DataAccessLayer.DataAccessLayer.CreateNewEnrollment(enrollmentInfo);
        }

        public static string UpdateStudentByID(StudentViewModel studentInfoUpdate)
        {
            return DataAccessLayer.DataAccessLayer.UpdateStudentByID(studentInfoUpdate);
        }

        public static string UpdateClassById(ClassViewModel classInfoUpdate)
        {
            return DataAccessLayer.DataAccessLayer.UpdateClassByID(classInfoUpdate);
        }

        public static string UpdateCourseByID(CourseViewModel courseInfoUpdate)
        {
            return DataAccessLayer.DataAccessLayer.UpdateCourseByID(courseInfoUpdate);
        } 
        
        public static string UpdateEnrollmentByID(EnrollmentViewModel enrollmentViewModel)
        {
            return DataAccessLayer.DataAccessLayer.UpdateEnrollmentByID(enrollmentViewModel);
        }

        public static string DeleteStudentByID(int id)
        {
            return DataAccessLayer.DataAccessLayer.DeleteStudentByID(id);
        }

        public static string DeleteClassByID(int id)
        {
            return DataAccessLayer.DataAccessLayer.DeleteClassByID(id);
        }

        public static string DeleteCourseByID(int id)
        {
            return DataAccessLayer.DataAccessLayer.DeleteCourseByID(id);
        }
        
        public static string DeleteEnrollmentByID(int id)
        {
            return DataAccessLayer.DataAccessLayer.DeleteEnrollmentByID(id);
        }

        public static Dictionary<string, List<string>> GetStudentByID(int studentId)
        {
            return DataAccessLayer.DataAccessLayer.GetStudentByID(studentId);
        }

        public static Dictionary<string, List<string>> GetClassByID(int classId)
        {
            return DataAccessLayer.DataAccessLayer.GetClassByID(classId);
        }

        public static Dictionary<string, List<string>> GetCourseByID(int classId)
        {
            return DataAccessLayer.DataAccessLayer.GetCourseByID(classId);
        }  
        
        public static Dictionary<string, List<string>> GetEnrollmentByID(int enrollmentID)
        {
            return DataAccessLayer.DataAccessLayer.GetEnrollmentByID(enrollmentID);
        }

        public static Dictionary<string, List<StudentViewModel>> ReadStudent()
        {
            return DataAccessLayer.DataAccessLayer.ReadStudent();
        }

        public static Dictionary<string, List<ClassViewModel>> ReadClass()
        {
            return DataAccessLayer.DataAccessLayer.ReadClass();
        }

        public static Dictionary<string, List<CourseViewModel>> ReadCourse()
        {
            return DataAccessLayer.DataAccessLayer.ReadCourse();
        }

        public static Dictionary<string, List<EnrollmentViewModel>> ReadEnrollment()
        {
            return DataAccessLayer.DataAccessLayer.ReadEnrollment();
        }

        public static Dictionary<string, List<StudentViewModel>> GetStudentsWithCourses()
        {
            return DataAccessLayer.DataAccessLayer.GetStudentsWithCourses();
        }

        public static List<ClassTeacherCourseStudent> GetClassTeacherCourseStudents()
        {
            return   SchoolCRUD.DataAccessLayer.GetAllDetails.GetClassTeacherCourseStudents();
            //  JoinAll.resultJoinAll();
        }
    }
}
