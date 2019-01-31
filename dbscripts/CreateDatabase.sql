:setvar DatabaseName "ProductionDatabase"
GO

CREATE DATABASE [$(DatabaseName)];
GO

ALTER AUTHORIZATION ON DATABASE::[$(DatabaseName)] TO sa
GO

Print "Attempting to create the Database."

USE [$(DatabaseName)];

:setvar TableName "Dentist"
GO

CREATE TABLE [dbo].[$(TableName)] (
    [Name]         VARCHAR(200) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
    [Id]           UNIQUEIDENTIFIER           DEFAULT NEWID() UNIQUE,
	[Birthday]          DATE                    NOT NULL,
	[Address]      VARCHAR(300) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[City]         VARCHAR(150) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[StateCode]    [nchar](2) NOT NULL,
	[ZipCode]      VARCHAR(11) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[Profile]      VARBINARY(MAX)
    PRIMARY KEY CLUSTERED ([Id]),
);

INSERT INTO [dbo].[$(TableName)]
     (Name, Id, Birthday, Address, City, StateCode, ZipCode, Profile)
VALUES 
	("John Allen", "69121893-3AFC-4F92-85F3-40BB5E7C7E29", '1990-05-11', "10541 Schooner Avenue", "Garden Grove", "CA", "92843", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock1.PNG', SINGLE_BLOB) AS PROFILE_FILE)),
    ("Jane Smith", "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756", '1970-01-22',"927 S Euclid St Ste B", "Anaheim", "CA", "92802", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\StockPhoto2.PNG', SINGLE_BLOB) AS PROFILE_FILE)),
	("Phil Nguyen", "DCD6FD4A-2400-47E3-8C50-0B0890AC91C5", '1993-02-14', "125 E Wilshire Ave", "Fullerton", "CA", "92832", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\StockPhoto3.PNG', SINGLE_BLOB) AS PROFILE_FILE)),
	("Xavier Salgado", "E9E5C10D-A43A-4F0C-B8BB-BE91C3285E01", '1995-07-25', "10737 Beverly Blvd", "Whittier", "CA", "90601", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\StockPhoto4.PNG', SINGLE_BLOB) AS PROFILE_FILE)),
	("Albert Wesker", "20B33428-8287-4B37-94DB-D72B34C50975", '1986-03-14', "1600 S Azusa Ave", "Rowland Heights", "CA", "91748", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\StockPhoto5.PNG', SINGLE_BLOB) AS PROFILE_FILE)),
	("Clare Redfield", "65BF4C05-DD7F-4000-8AC6-050827DE580D", '1950-11-11', "1030 N Citrus Ave", "Covina", "CA", "91722", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\StockPhoto6.PNG', SINGLE_BLOB) AS PROFILE_FILE)),
	("Vanessa Calgary", "EE1D3E62-C2BF-40B9-AE3E-1324B4D97F80", '1964-06-02', "1051 Burbank Blvd", "Burbank", "CA", "91506", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\StockPhoto7.PNG', SINGLE_BLOB) AS PROFILE_FILE));
Print "Successfully added data to the table."
GO

:setvar TableName "Patient"
GO

CREATE TABLE [dbo].[$(TableName)] (
    [Name]         VARCHAR(200) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
    [Id]           UNIQUEIDENTIFIER           DEFAULT NEWID() UNIQUE,
	[Birthday]          DATE                    NOT NULL,
	[Address]      VARCHAR(300) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[City]         VARCHAR(150) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[StateCode]    [nchar](2) NOT NULL,
	[ZipCode]      VARCHAR(11) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	[Profile]      VARBINARY(MAX),
	[DoctorId]     UNIQUEIDENTIFIER           DEFAULT NEWID(),
    PRIMARY KEY CLUSTERED ([Id]),
	FOREIGN KEY(DoctorId) REFERENCES Dentist([Id]),
);

Print "Created the table successfully."

INSERT INTO [dbo].[$(TableName)]
     (Name, Id, Birthday, Address, City, StateCode, ZipCode, Profile, DoctorId)
VALUES 
	("Jason Truong", "8B51D6E8-BD92-4663-B609-27761E2D5493", '1993-05-20', "10541 Schooner Avenue", "Garden Grove", "CA", "92843", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock1.PNG', SINGLE_BLOB) AS PROFILE_FILE), "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756"),
	("Jonathan Tran", "83B232EA-C477-4BFE-A01D-088591D7D1BE", '1995-01-21', "10222 Brighton Street", "Westminster", "CA", "92626", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock2.PNG', SINGLE_BLOB) AS PROFILE_FILE), "69121893-3AFC-4F92-85F3-40BB5E7C7E29"),
	("Stan Edge", "FEB55D73-2AB5-4592-A541-1C239F2D1D82", '1961-06-29', "883 North Chapel St.", "Clinton", "MD", "20735", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock1.PNG', SINGLE_BLOB) AS PROFILE_FILE), "DCD6FD4A-2400-47E3-8C50-0B0890AC91C5"),
	("Emilie Brandt", "E727E873-9F18-4A3A-8EBF-CF95AA8B7661", '1966-04-06', "7785 South East Rd.", "Sebastian", "FL", "32958", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock3.PNG', SINGLE_BLOB) AS PROFILE_FILE), "E9E5C10D-A43A-4F0C-B8BB-BE91C3285E01"),
	("Lennie Goff", "CA7CB977-C892-4A14-A46B-D1B897E0836A", '1978-02-21', "81 South Carriage Court", "Cranston", "RI", "02920", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock2.PNG', SINGLE_BLOB) AS PROFILE_FILE), "20B33428-8287-4B37-94DB-D72B34C50975"),
	("Andreas Stamp", "BF6B4A7E-6132-47CE-9907-B955962F444F", '2000-10-11', "221 Front Circle", "Little Falls", "NJ", "07424", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock1.PNG', SINGLE_BLOB) AS PROFILE_FILE), "65BF4C05-DD7F-4000-8AC6-050827DE580D"),
	("Kaila Shepard", "70A221CB-5BC4-4462-B5A2-F61C5747911F", '1971-12-28', "8212 Wayne St.", "Hattiesburg", "MS", "39401", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock4.PNG', SINGLE_BLOB) AS PROFILE_FILE), "EE1D3E62-C2BF-40B9-AE3E-1324B4D97F80"),
	("Heidi Chase", "8320D5D8-DC98-4183-9CFF-B1ABC4E05CF1", '1965-10-22', "427 Sugar Ave.", "Pewaukee", "WI", "53072", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock3.PNG', SINGLE_BLOB) AS PROFILE_FILE), "69121893-3AFC-4F92-85F3-40BB5E7C7E29"),
	("Barbara Seymour", "33404899-C63D-4FEC-9789-E5A218A09671", '1991-02-25', "78 West St Louis St.", "Falls Church", "VA", "22041", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock4.PNG', SINGLE_BLOB) AS PROFILE_FILE), "69121893-3AFC-4F92-85F3-40BB5E7C7E29"),
	("Corban Craig", "F5FD3EF0-B37C-4738-B3B6-E6C6159D244F", '1987-01-19', "9946 Marsh St.", "Ithaca", "NY", "14850", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock2.PNG', SINGLE_BLOB) AS PROFILE_FILE), "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756"),
	("Tiegan Cartwright", "16CCAB60-90F6-4FE1-86E9-14FEBB1B3FFF", '1983-10-03', "8996 Birchpond St.", "Riverdale", "GA", "30274", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock3.PNG', SINGLE_BLOB) AS PROFILE_FILE), "65BF4C05-DD7F-4000-8AC6-050827DE580D"),
	("Hadley East", "01240347-69C2-421E-9AED-AFE456BA656E", '1954-05-31', "904 Greenview Street", "Fairborn", "OH", "45324", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock4.PNG', SINGLE_BLOB) AS PROFILE_FILE), "E9E5C10D-A43A-4F0C-B8BB-BE91C3285E01"),
	("Celeste Horton", "EBACAF2E-D6A4-4D00-B96E-E26EB735A34A", '1984-07-06', "333 Kent Street", "Media", "PA", "19063", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock3.PNG', SINGLE_BLOB) AS PROFILE_FILE), "20B33428-8287-4B37-94DB-D72B34C50975"),
	("Devin Proctor", "D194B19E-BE8A-4EDF-889E-885BFF07D772", '1989-04-07', "45 Jackson Lane", "Collierville", "TN", "38017", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock1.PNG', SINGLE_BLOB) AS PROFILE_FILE), "DCD6FD4A-2400-47E3-8C50-0B0890AC91C5"),
	("Layton Sweeney", "A5A1370B-7B1E-4AD0-812B-A6467EE3B73E", '1998-08-21', "32 SE. 8th Street", "Valparaiso", "IN", "46383", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock2.PNG', SINGLE_BLOB) AS PROFILE_FILE), "65BF4C05-DD7F-4000-8AC6-050827DE580D"),
	("Dina Wise", "133BE222-730B-4505-AA95-DE5E70A4AF0A", '1987-08-24', "19 Ivy Street ", "Grayslake", "IL", "60030", (SELECT * FROM OPENROWSET(BULK N'C:\Users\Jason Truong\Pictures\PatientStock4.PNG', SINGLE_BLOB) AS PROFILE_FILE), "DCD6FD4A-2400-47E3-8C50-0B0890AC91C5");
Print "Successfully added data to the table."
GO

:setvar TableName "Schedule"
GO
CREATE TABLE [dbo].[$(TableName)] (
	[Subject]			 VARCHAR(300) COLLATE SQL_Latin1_General_CP1_CS_AS,
	[EventId]			 UNIQUEIDENTIFIER			DEFAULT NEWID() NOT NULL,
    [DoctorId]           UNIQUEIDENTIFIER           DEFAULT NEWID(),
    [PatientId]          UNIQUEIDENTIFIER           DEFAULT NEWID() NOT NULL,
	[StartDate]          DATETIME                    NOT NULL,
	[EndDate]            DATETIME                    NOT NULL,
	[Description]        VARCHAR(300) COLLATE SQL_Latin1_General_CP1_CS_AS
    PRIMARY KEY CLUSTERED ([DoctorId], [EventId]),
	FOREIGN KEY(DoctorId) REFERENCES Dentist([Id]),
	FOREIGN KEY(PatientId) REFERENCES Patient([Id])
);
GO

Print "Created the table successfully."

INSERT INTO [dbo].[$(TableName)]
     (Subject, EventId, PatientId, StartDate, EndDate, DoctorId, Description)
VALUES
	("Dentist Appointment", "5394ADF1-2110-4229-B3C1-FD5A383F4EF2", "8B51D6E8-BD92-4663-B609-27761E2D5493", '2019-01-15 10:30:00', '2019-01-15 13:30:00', "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756", "Monthly Checkup"),
	("Rescheduled Appointment", "3B0D0151-0D24-46CC-A7B8-46D536167E97", "8B51D6E8-BD92-4663-B609-27761E2D5493", '2019-02-01 11:00:00', '2019-02-01 12:00:00', "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756", "Recurring Cavity Issue"),
	("Dentist Appointment", "4055FA46-8492-46F9-B361-157F18E420B0", "8B51D6E8-BD92-4663-B609-27761E2D5493", '2019-02-11 16:00:00', '2019-02-11 18:00:00', "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756", "Braces Appointment"),
	("Dentist Appointment", "E956D9F5-5C84-4593-8B07-DBB2FE73F267", "8B51D6E8-BD92-4663-B609-27761E2D5493", '2019-02-15 18:45:00', '2019-02-15 20:45:00', "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756", "Teeth Cleaning and X-Ray"),
	("Dentist Appointment", "DE56D321-2AD4-47EE-AE62-6ED6E506DE3C", "8B51D6E8-BD92-4663-B609-27761E2D5493", '2019-02-22 15:30:00', '2019-02-22 19:30:00', "CB77CCE6-C2CB-471B-BDD4-5DAC8C93B756", "Braces Appointment"),
	("Dentist Appointment", "34B2B1BD-CBA8-48C9-A902-0CF87FBD2112", "83B232EA-C477-4BFE-A01D-088591D7D1BE", '2019-02-20 11:30:00', '2019-02-20 13:30:00', "69121893-3AFC-4F92-85F3-40BB5E7C7E29", "Cavity Issue Follow up"),
	("Dentist Appointment", "AD062A4F-F972-4494-BF3C-0D4B2608E64C", "133BE222-730B-4505-AA95-DE5E70A4AF0A", '2019-02-20 12:30:00', '2019-02-20 14:30:00', "DCD6FD4A-2400-47E3-8C50-0B0890AC91C5", "Teeth Cleaning Follow up");
	