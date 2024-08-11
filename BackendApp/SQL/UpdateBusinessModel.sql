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