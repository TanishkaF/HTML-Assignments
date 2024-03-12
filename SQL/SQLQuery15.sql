CREATE TABLE EducationDetails (
    EducationID  INT PRIMARY KEY IDENTITY,
    StudentID INT FOREIGN KEY REFERENCES StudentDetails(StudentID),
    EducationType INT,
    InstituteName NVARCHAR(100),
    Board NVARCHAR(50),
    Marks NVARCHAR(100),
    Aggregate DECIMAL(5, 2),
    YearOfCompletion INT
);

DELETE FROM StudentDetails;
DELETE FROM AddressDetails;
DELETE FROM EducationDetails;