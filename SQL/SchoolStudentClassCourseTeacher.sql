USE SCHOOL;

DELETE FROM Student;
DELETE FROM Course;
DELETE FROM Class;
DELETE FROM SchoolErrorLogs;
DELETE FROM Enrollment;
DELETE FROM Teacher;
DROP TABLE  Class;

CREATE TABLE Student (
    StudentID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Age INT,
    GPA FLOAT
);

-- Insert data into Student table
INSERT INTO Student VALUES
(1, 'John', 'Doe', 20, 3.5),
(2, 'Jane', 'Smith', 22, 3.8),
(3, 'Bob', 'Johnson', 21, 3.2),
(4, 'Alice', 'Williams', 23, 3.9),
(5, 'Charlie', 'Brown', 20, 3.0);

-- Create Course table
CREATE TABLE Course (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(50),
    Credits INT
);

-- Insert data into Course table
INSERT INTO Course VALUES
(101, 'Mathematics', 3),
(102, 'History', 4),
(103, 'Computer Science', 3),
(104, 'Physics', 4),
(105, 'English', 3);

CREATE TABLE Class (
    ClassID INT PRIMARY KEY,
    ClassName VARCHAR(50),
  
);

INSERT INTO Class VALUES
(1, 'ClassA'),
(2, 'ClassB'),
(3, 'ClassC'),
(4, 'ClassD'),
(5, 'ClassE');

-- Create SchoolErrorLogs table
CREATE TABLE SchoolErrorLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    Timestamp DATETIME,
    Message VARCHAR(MAX),
    StackTrace VARCHAR(MAX),
    Source VARCHAR(MAX),
    TargetSite VARCHAR(MAX)
);

INSERT INTO SchoolErrorLogs (Timestamp, Message, StackTrace, Source, TargetSite) VALUES
('2024-02-01 08:00:00', 'Error message 1', 'Stack trace 1', 'Source 1', 'Target Site 1'),
('2024-02-01 09:15:00', 'Error message 2', 'Stack trace 2', 'Source 2', 'Target Site 2'),
('2024-02-01 10:30:00', 'Error message 3', 'Stack trace 3', 'Source 3', 'Target Site 3'),
('2024-02-01 11:45:00', 'Error message 4', 'Stack trace 4', 'Source 4', 'Target Site 4'),
('2024-02-01 12:00:00', 'Error message 5', 'Stack trace 5', 'Source 5', 'Target Site 5');

CREATE TABLE Enrollment (
	EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT FOREIGN KEY REFERENCES Student(StudentID) NOT NULL,
    CourseID INT FOREIGN KEY REFERENCES Course(CourseID) NOT NULL,
	   CONSTRAINT FK_Enrollment_Student FOREIGN KEY (StudentID) REFERENCES Student(StudentID) ON DELETE CASCADE,
    CONSTRAINT FK_Enrollment_Course FOREIGN KEY (CourseID) REFERENCES Course(CourseID) ON DELETE CASCADE
);

-- Reset the identity seed for the Enrollment table
DBCC CHECKIDENT('Enrollment', RESEED, 0);


INSERT INTO Enrollment (StudentID, CourseID) VALUES (1, 101);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (1, 104);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (2, 103);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (2, 102);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (3, 101);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (4, 104);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (4, 105);
INSERT INTO Enrollment (StudentID, CourseID) VALUES (5, 102);



SELECT * FROM STUDENT;
SELECT * FROM CLASS;
SELECT * FROM COURSE;
SELECT * FROM ENROLLMENT;
SELECT * FROM TEACHER;

CREATE TABLE Teacher (
    TeacherID INT PRIMARY KEY,
    ClassID INT,
    CourseID INT,
    FOREIGN KEY (ClassID) REFERENCES Class(ClassID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
);

SELECT * FROM Teacher;

INSERT INTO Teacher values (3,'alpha',3,102);