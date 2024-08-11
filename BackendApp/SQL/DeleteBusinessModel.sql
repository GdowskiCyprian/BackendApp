CREATE OR ALTER PROCEDURE usp_DeleteBusinessModel
    @Id INT
AS
BEGIN
    DELETE FROM BusinessModels
    WHERE Id = @Id
END
GO
