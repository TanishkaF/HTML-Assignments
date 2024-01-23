CREATE table student(
	Age int,
	FirstName varchar(255),
	Class int,
	section char
);

insert into student values (1,'tanishka',10,'a'); 
insert into student values (2,'aman',10,'c'); 
insert into student values (3,'Dinesh',10,'b'); 
insert into student values (4,'Bucky',10,'c'); 
insert into student values (5,'Ashi',10,'b'); 
insert into student values (6,'Ganga',10,'b'); 
insert into student values (7,'Ram',10,'b'); 
insert into student values (9,'Sita',10,'c'); 


select * from student;

delete from student where age = 6;

alter table STUDENT RENAME column age to roll_no;

EXEC sp_rename 'STUDENT.Age', 'roll_no', 'COLUMN';

SHOW DATABASES;





