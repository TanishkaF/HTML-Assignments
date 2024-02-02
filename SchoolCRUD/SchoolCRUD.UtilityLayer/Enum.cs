namespace SchoolCRUD.UtilityLayer
{
    public class Enum
    {
        public enum MenuOption
        {
            CreateStudent = 1,
            CreateClass,
            CreateCourse,
            CreateEnrollment,
            CreateTeacher,
            UpdateStudent,
            UpdateClass,
            UpdateCourse,
            UpdateEnrollment,
            UpdateTeacher,
            DeleteStudent,
            DeleteClass,
            DeleteCourse,
            DeleteEnrollment,
            DeleteTeacher,
            DisplayStudent,
            DisplayClass,
            DisplayCourse,
            DisplayEnrollment,
            DisplayTeacher,
            SearchStudentByID,
            SearchClassByID,
            SearchCourseByID,
            SearchEnrollmentByID,
            SearchTeacherByID,
            GetStudentsWithClassesAndCourses,
            ExpoterDecider,
            DisplayAllData,
            //CSVExpoter,
            //ExeclExpoter,
            // GetClassesWithStudentsAndCourses,
            Exit
        }
    }
}
