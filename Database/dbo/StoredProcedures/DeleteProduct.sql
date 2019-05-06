CREATE PROCEDURE [dbo].[DeleteProduct]
	@Id INT
AS
	BEGIN
		DELETE FROM Product WHERE Id = @Id;
	END