USE [Data]
GO

IF OBJECT_ID (N'Visits', N'U') IS NULL
BEGIN
CREATE TABLE Visits
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId uniqueidentifier NOT NULL,
    BusinessId int not null,
    VisitTime DateTime not null,
	Email nvarchar(200) null,
	PhoneNumber nvarchar(9) null,
	IsConfirmed bit null
);
END
GO