SELECT DISTINCT
    c.ClassID ,
    c.ClassName,
    t.TeacherID,   
    t.CourseID AS TeacherCourseID,
    co.CourseID,
    co.CourseName,
    co.Credits,
    s.StudentID,
    s.FirstName,
    s.LastName,
    s.Age,
    s.GPA
FROM
    Class c
JOIN
    Teacher t ON c.ClassID = t.ClassID
JOIN
    Course co ON t.CourseID = co.CourseID
JOIN
    Enrollment e ON co.CourseID = e.CourseID
JOIN
    Student s ON e.StudentID = s.StudentID
ORDER BY
    c.ClassID;
