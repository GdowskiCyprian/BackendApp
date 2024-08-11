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