USE SCHOOL;

-- Create Student table
CREATE TABLE Student (
    StudentID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Age INT,
    GPA FLOAT
);

-- Insert data into Student table
INSERT INTO Student VALUES (1, 'John', 'Doe', 20, 3.5);
INSERT INTO Student VALUES (2, 'Jane', 'Smith', 22, 3.8);
INSERT INTO Student VALUES (3, 'Bob', 'Johnson', 21, 3.2);
INSERT INTO Student VALUES (4, 'Alice', 'Williams', 19, 3.9);
INSERT INTO Student VALUES (5, 'Chris', 'Davis', 20, 3.6);

-- Create Course table
CREATE TABLE Course (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(50),
    Credits INT
);

-- Insert data into Course table
INSERT INTO Course VALUES (101, 'Mathematics', 3);
INSERT INTO Course VALUES (102, 'English', 4);
INSERT INTO Course VALUES (103, 'History', 3);
INSERT INTO Course VALUES (104, 'Computer Science', 5);
INSERT INTO Course VALUES (105, 'Physics', 4);

-- Create Class table
CREATE TABLE Class (
    ClassID INT PRIMARY KEY,
    ClassName VARCHAR(50),
    Instructor VARCHAR(50),
    CourseID INT FOREIGN KEY REFERENCES Course(CourseID),
    StudentID INT FOREIGN KEY REFERENCES Student(StudentID)
);

-- Insert data into Class table
INSERT INTO Class VALUES (1, 'Class A', 'Prof. Johnson', 101, 1);
INSERT INTO Class VALUES (2, 'Class B', 'Prof. Smith', 102, 2);
INSERT INTO Class VALUES (3, 'Class C', 'Prof. Williams', 103, 3);
INSERT INTO Class VALUES (4, 'Class D', 'Prof. Davis', 104, 4);
INSERT INTO Class VALUES (5, 'Class E', 'Prof. Anderson', 105, 5);



CREATE TABLE SchoolErrorLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    Timestamp DATETIME,
    Message VARCHAR(MAX),
    StackTrace VARCHAR(MAX),
    Source VARCHAR(MAX),
    TargetSite VARCHAR(MAX)
);


SELECT * from course;
select * from student;
select * from class;
SELECT * FROM SchoolErrorLogs;

