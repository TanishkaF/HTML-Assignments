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
            UpdateStudent,
            UpdateClass,
            UpdateCourse,
            UpdateEnrollment,
            DeleteStudent,
            DeleteClass,
            DeleteCourse,
            DeleteEnrollment,
            DisplayStudent,
            DisplayClass,
            DisplayCourse,
            DisplayEnrollment,
            SearchStudentByID,
            SearchClassByID,
            SearchCourseByID,
            SearchEnrollmentByID,
            GetStudentsWithClassesAndCourses,
            ExpoterDecider,
            ALLJOINHELPER,
            //CSVExpoter,
            //ExeclExpoter,
            // GetClassesWithStudentsAndCourses,
            Exit
        }
    }
}
