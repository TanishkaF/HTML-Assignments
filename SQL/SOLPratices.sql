CREATE TABLE Student (
    Student_ID INT PRIMARY KEY,
    First_Name VARCHAR(50),
    Last_Name VARCHAR(50),
    Age INT,
    Grade VARCHAR(2)
);
select * from student;

INSERT INTO Student values
	(1, 'John', 'Doe', 20, 'A'),
(2, 'Emily', 'Johnson', 22, 'B'),
(3, 'Michael', 'Smith', 21, 'A-'),
(4, 'Sarah', 'Williams', 19, 'B+');

insert into student values
(5, 'Alex', 'Singh', 20, 'A'),
(6, 'Pranab', 'Rath', 22, 'B-'),
(7, 'Seba', 'Shah', 23, 'C+'),
(8, 'Shruti', 'Gupta', 19, 'A'),
(9, 'Akash', 'Das', 21, 'B+'),
(10, 'Zunki', 'Mishra', 20, 'A-');



CREATE TABLE Courses (
    Course_ID INT PRIMARY KEY,
    Course_Name VARCHAR(50),
    Credits INT,
    Instructor VARCHAR(50),
    Department VARCHAR(50)
);
select * from courses;
select * from student;


INSERT INTO Courses VALUES
(101, 'Mathematics', 3, 'Prof. Devi', 'Math'),
(102, 'English Literature', 4, 'Prof. Indra', 'English'),
(103, 'Computer Science', 4, 'Prof. PK', 'Computer Sci'),
(104, 'History', 3, 'Prof. Meera', 'History'),
(105, 'Chemistry', 4, 'Prof. Basa', 'Chemistry'),
(106, 'Physics', 4, 'Prof. Basa', 'Physics'),
(107, 'Economics', 3, 'Prof. Pihu', 'Economics'),
(108, 'Psychology', 3, 'Prof. Jones', 'Psychology'),
(109, 'Biology', 4, 'Prof. Simon', 'Biology'),
(110, 'Art History', 3, 'Prof. Pratik', 'Art History');