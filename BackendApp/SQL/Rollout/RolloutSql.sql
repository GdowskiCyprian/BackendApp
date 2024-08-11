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
    BuildingNumber NVARCHAR(50) NOT NULL
);
END
GO

CREATE OR ALTER PROCEDURE usp_GetBusinessModels
AS
BEGIN
    SELECT
		Id,
        Name,
        Description,
        BusinessType,
        City,
        PostalCode,
        Street,
        BuildingNumber
    FROM 
        BusinessModels
END

GO

CREATE OR ALTER PROCEDURE usp_InsertBusinessModel
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @BusinessType NVARCHAR(255),
    @City NVARCHAR(255),
    @PostalCode NVARCHAR(50),
    @Street NVARCHAR(255),
    @BuildingNumber NVARCHAR(50)
AS
BEGIN
    INSERT INTO BusinessModels (Name, Description, BusinessType, City, PostalCode, Street, BuildingNumber)
    VALUES (@Name, @Description, @BusinessType, @City, @PostalCode, @Street, @BuildingNumber)
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
    @BuildingNumber NVARCHAR(50)
AS
BEGIN
    UPDATE BusinessModels
    SET Name = @Name,
        Description = @Description,
        BusinessType = @BusinessType,
        City = @City,
        PostalCode = @PostalCode,
        Street = @Street,
        BuildingNumber = @BuildingNumber
    WHERE Id = @Id
END
GO

CREATE OR ALTER PROCEDURE usp_DeleteBusinessModel
    @Id INT
AS
BEGIN
    DELETE FROM BusinessModels
    WHERE Id = @Id
END

