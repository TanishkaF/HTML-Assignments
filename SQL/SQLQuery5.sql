use student;
select * from student;
SELECT * FROM backUpTable;

CREATE TABLE backUpTable (
	roll_no	int,
	FirstName varchar(255),
	Class INT,
	section CHAR
);

DELETE FROM Student WHERE roll_no = 7;
select * from student;


CREATE TRIGGER T1
ON student
INSTEAD OF DELETE
AS
BEGIN
    INSERT INTO backUpTable (roll_no, FirstName, Class, section)
    SELECT roll_no, FirstName, Class, section
    FROM deleted;
END;

use student
select * from teacher;

INSERT INTO teacher values(2,'aman','aman','1200-10-21');
DELETE FROM teacher WHERE id=2;
UPDATE Teacher SET email='amanemail' WHERE id= 2;


