CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(MAX),
    EmailID NVARCHAR(255) UNIQUE,
    Password NVARCHAR(255) 
);


CREATE TABLE Airport (
    AirportUID INT IDENTITY(1,1) NOT NULL,
	AirportID NVARCHAR(30) PRIMARY KEY NOT NULL,
	AirportName NVARCHAR(MAX) NOT NULL,
    FuelCapacity DECIMAL,
    FuelAvailable DECIMAL,
);


CREATE TABLE Aircraft (
    AircraftUID INT IDENTITY(1,1) NOT NULL,
	AircraftID NVARCHAR(30) PRIMARY KEY NOT NULL,    
	AircraftNumber NVARCHAR(30),
    AirLine NVARCHAR(255) NOT NULL,
    Source NVARCHAR(255) NOT NULL,
    Destination NVARCHAR(255) NOT NULL	
);

CREATE TABLE Role (
    RoleID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleName NVARCHAR(MAX),
    IsDefault BIT,
    IsAdmin BIT
);

CREATE TABLE UserRole (
    UserRoleID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    UserID INT FOREIGN KEY REFERENCES Users(userID),
    RoleID INT FOREIGN KEY REFERENCES Role(RoleID)
);

CREATE TABLE FuelTransaction (
    TransactionID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    TimeStamp DATETIME NOT NULL,
    TransactionType INT NOT NULL CHECK (TransactionType IN (1, 2)),
    AirportID NVARCHAR(30) NOT NULL,
    AircraftID NVARCHAR(30),
    Quantity DECIMAL NOT NULL,
    TransactionIDParent INT,
    FOREIGN KEY (AirportID) REFERENCES Airport(AirportID),
    FOREIGN KEY (AircraftID) REFERENCES Aircraft(AircraftID)
);


INSERT INTO Users (Name, EmailID, Password) VALUES 
('John Doe', 'john.doe@example.com', 'password123'),
('Alice Smith', 'alice.smith@example.com', 'p@ssw0rd'),
('Bob Patel', 'bob.patel@example.com', 'securepassword'),
('Emma Kumar', 'emma.kumar@example.com', 'password123'),
('Michael Gupta', 'michael.gupta@example.com', 'mypass123');

INSERT INTO Airport (AirportID, AirportName, FuelCapacity, FuelAvailable) VALUES 
('DEL', 'Indira Gandhi International Airport, Delhi', 100000, 50000),
('BOM', 'Chhatrapati Shivaji International Airport, Mumbai', 80000, 60000),
('MAA', 'Chennai International Airport, Chennai', 70000, 55000),
('BLR', 'Kempegowda International Airport, Bangalore', 90000, 45000),
('HYD', 'Rajiv Gandhi International Airport, Hyderabad', 85000, 70000);

INSERT INTO Aircraft (AircraftID, AircraftNumber, AirLine, Source, Destination) VALUES 
('6E101', '6E-101', 'IndiGo', 'DEL', 'BOM'),
('AI202', 'AI-202', 'Air India', 'BOM', 'DEL'),
('SG303', 'SG-303', 'SpiceJet', 'MAA', 'BLR'),
('G812', 'G8-12', 'Go Air', 'BLR', 'MAA'),
('AI404', 'AI-404', 'Air India', 'HYD', 'BOM');

INSERT INTO FuelTransaction (TimeStamp, TransactionType, AirportID, AircraftID, Quantity)
VALUES ('2024-03-06 09:00:00', 1, 'DEL', '6E101', 20000);
INSERT INTO FuelTransaction (TimeStamp, TransactionType, AirportID, AircraftID, Quantity)
VALUES ('2024-03-06 10:30:00', 2, 'DEL', '6E101', 15000);
INSERT INTO FuelTransaction (TimeStamp, TransactionType, AirportID, AircraftID, Quantity)
VALUES ('2024-03-06 11:45:00', 1, 'BOM', 'AI202', 30000);
INSERT INTO FuelTransaction (TimeStamp, TransactionType, AirportID, AircraftID, Quantity)
VALUES ('2024-03-06 13:15:00', 2, 'BOM', 'AI202', 25000);
INSERT INTO FuelTransaction (TimeStamp, TransactionType, AirportID, AircraftID, Quantity)
VALUES ('2024-03-06 14:30:00', 1, 'BLR', 'SG303', 18000);


INSERT INTO Role (RoleName, IsDefault, IsAdmin) VALUES ('User', 1, 0); -- User role, IsDefault set to 1, IsAdmin set to 0
INSERT INTO Role (RoleName, IsDefault, IsAdmin) VALUES ('Admin', 0, 1); -- Admin role, IsDefault set to 0, IsAdmin set to 1


INSERT INTO UserRole (UserID, RoleID) VALUES 
(1, 2), -- John Doe - Admin (RoleID 2)
(2, 1); 

SELECT * FROM Users;
SELECT * FROM Aircraft;

SELECT * FROM Airport ORDER BY AirportUID ASC;
SELECT * FROM FuelTransaction 
WHERE AirportID  LIKE 'HYD' ORDER BY TransactionType ASC; 

SELECT * FROM Role;
SELECT * FROM UserRole;

DELETE FROM FuelTransaction WHERE TransactionID  BETWEEN 105 AND 118;
DELETE FROM Airport WHERE AirportUID BETWEEN 6 AND 15
DELETE FROM Aircraft WHERE AircraftUID BETWEEN 6 AND 15

