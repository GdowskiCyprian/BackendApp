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