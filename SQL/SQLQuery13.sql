CREATE TABLE StudentDetails (
    StudentID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
	MiddleName VARCHAR(50),
    LastName NVARCHAR(50),
	Email NVARCHAR(20),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
 Phone NVARCHAR(10), 
 AadharNumber NVARCHAR(20)
);
 

CREATE TABLE AddressDetails (
    AddressID INT PRIMARY KEY IDENTITY,
    UserID INT FOREIGN KEY REFERENCES StudentDetailsTable(StudentID),
    AddressType INT, -- 1: Current, 2: Permanent  
    CountryID INT FOREIGN KEY REFERENCES Countries(CountryID),
    StateID INT FOREIGN KEY REFERENCES States(StateID),
    AddressLine1 NVARCHAR(100),
    AddressLine2 NVARCHAR(100),
    Pincode NVARCHAR(20)
);

CREATE TABLE EducationDetails (
    EducationID  INT PRIMARY KEY IDENTITY,
    StudentID INT FOREIGN KEY REFERENCES StudentDetailsTable(StudentID),
    EducationType INT,
    InstituteName NVARCHAR(100),
    Board NVARCHAR(50),
    Marks NVARCHAR(100),
    Aggregate DECIMAL(5, 2),
    YearOfCompletion INT);



CREATE TABLE Countries (
    CountryID INT PRIMARY KEY,
    CountryName NVARCHAR(100)
);

CREATE TABLE States (
    StateID INT PRIMARY KEY,
    StateName NVARCHAR(100),
    CountryID INT,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);

-- Inserting states for India
INSERT INTO States (StateID, StateName, CountryID) VALUES
(1, 'Delhi', 1),
(2, 'Maharashtra', 1),
(3, 'Karnataka', 1);

-- Inserting states for USA
INSERT INTO States (StateID, StateName, CountryID) VALUES
(4, 'California', 2),
(5, 'Texas', 2),
(6, 'New York', 2);
delete from StudentDetails 

CREATE TABLE Note (
    NoteID INT PRIMARY KEY IDENTITY,
    ObjectID INT FOREIGN KEY REFERENCES StudentDetailsTable(StudentID),
    ObjectType INT,
    NoteText NVARCHAR(MAX),
    TimeStamp DATETIME
);


CREATE TABLE StudentDetailsTable (
    StudentID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(50),
    MiddleName VARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(20),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Phone NVARCHAR(10), 
    AadharNumber NVARCHAR(20),
    Hobbies VARCHAR(100), -- Adjust the size according to your needs
    DiskDocumentName VARCHAR(100) UNIQUE, -- Adjust the size according to your needs
    OriginalDocumentName VARCHAR(100)  -- Adjust the size according to your needs
);

