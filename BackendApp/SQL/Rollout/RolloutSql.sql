Use Data

IF OBJECT_ID (N'BusinessModels', N'U') IS NULL
BEGIN
CREATE TABLE BusinessModels
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000),
    BusinessType NVARCHAR(255) NOT NULL,
    City NVARCHAR(255) NOT NULL,
    PostalCode NVARCHAR(50) NOT NULL,
    Street NVARCHAR(255) NOT NULL,
    BuildingNumber NVARCHAR(50) NOT NULL,
	OwnerId uniqueidentifier NOT NULL
);
END
GO

  CREATE OR ALTER PROCEDURE usp_GetBusinessModels(@offset int)
AS
BEGIN
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[BusinessType]
      ,[City]
      ,[PostalCode]
      ,[Street]
      ,[BuildingNumber]
      ,[OwnerId]
  FROM [Data].[dbo].[BusinessModels] WITH NOLOCK
  ORDER BY Id
  OFFSET @offset*10 ROWS
  FETCH NEXT 10 ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE usp_InsertBusinessModel
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @BusinessType NVARCHAR(255),
    @City NVARCHAR(255),
    @PostalCode NVARCHAR(50),
    @Street NVARCHAR(255),
    @BuildingNumber NVARCHAR(50),
	@OwnerId uniqueidentifier
AS
BEGIN
BEGIN TRANSACTION
    INSERT INTO BusinessModels (Name, Description, BusinessType, City, PostalCode, Street, BuildingNumber, OwnerId)
    VALUES (@Name, @Description, @BusinessType, @City, @PostalCode, @Street, @BuildingNumber, @OwnerId)
COMMIT TRANSACTION
END

GO

CREATE OR ALTER PROCEDURE usp_UpdateBusinessModel
    @Id INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @BusinessType NVARCHAR(255),
    @City NVARCHAR(255),
    @PostalCode NVARCHAR(50),
    @Street NVARCHAR(255),
    @BuildingNumber NVARCHAR(50),
	@OwnerId uniqueidentifier
AS
BEGIN
BEGIN TRANSACTION
    UPDATE BusinessModels
    SET Name = @Name,
        Description = @Description,
        BusinessType = @BusinessType,
        City = @City,
        PostalCode = @PostalCode,
        Street = @Street,
        BuildingNumber = @BuildingNumber,
		OwnerId = @OwnerId
    WHERE Id = @Id
COMMIT TRANSACTION
END
GO

CREATE OR ALTER PROCEDURE usp_DeleteBusinessModel
    @Id INT
AS
BEGIN
BEGIN TRANSACTION
    DELETE FROM BusinessModels
    WHERE Id = @Id
COMMIT TRANSACTION
END

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


CREATE OR ALTER PROCEDURE usp_GetVisits(@offset int)
AS
BEGIN
    SELECT 
	Id, 
	UserId, 
	BusinessId, 
	VisitTime, 
	Email, 
	PhoneNumber, 
	IsConfirmed
    FROM Visits WITH NOLOCK
	ORDER BY Id
  OFFSET @offset*10 ROWS
  FETCH NEXT 10 ROWS ONLY
END;
GO

CREATE OR ALTER PROCEDURE usp_InsertVisit
    @UserId UNIQUEIDENTIFIER,
    @BusinessId INT,
    @VisitTime DATETIME,
    @Email NVARCHAR(200) = NULL,
    @PhoneNumber NVARCHAR(9) = NULL,
    @IsConfirmed BIT
AS
BEGIN
BEGIN TRANSACTION
    INSERT INTO Visits (UserId, BusinessId, VisitTime, Email, PhoneNumber, IsConfirmed)
    VALUES (@UserId, @BusinessId, @VisitTime, @Email, @PhoneNumber, @IsConfirmed);
COMMIT TRANSACTION
END;
GO

CREATE OR ALTER PROCEDURE usp_UpdateVisit
    @Id INT,
    @UserId UNIQUEIDENTIFIER,
    @BusinessId INT,
    @VisitTime DATETIME,
    @Email NVARCHAR(200) = NULL,
    @PhoneNumber NVARCHAR(9) = NULL,
    @IsConfirmed BIT
AS
BEGIN
BEGIN TRANSACTION
    UPDATE Visits
    SET UserId = @UserId,
        BusinessId = @BusinessId,
        VisitTime = @VisitTime,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        IsConfirmed = @IsConfirmed
    WHERE Id = @Id;
COMMIT TRANSACTION
END;
GO

CREATE OR ALTER PROCEDURE usp_DeleteVisit
    @Id INT
AS
BEGIN
BEGIN TRANSACTION
    DELETE FROM Visits
    WHERE Id = @Id;
COMMIT TRANSACTION
END;
go

CREATE OR ALTER PROCEDURE usp_GetVisitsByDateRange
    @DateFrom DATE,
    @DateTo DATE,
    @offset int
AS
BEGIN
    SELECT Id, UserId, BusinessId, VisitTime, Email, PhoneNumber, IsConfirmed
    FROM Visits WITH NOLOCK
    WHERE CAST(VisitTime AS DATE) BETWEEN @DateFrom AND @DateTo
    ORDER BY Id
  OFFSET @offset*10 ROWS
  FETCH NEXT 10 ROWS ONLY
END;