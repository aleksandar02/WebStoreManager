CREATE PROCEDURE [dbo].[DeletePostComment]
	@Id INT
AS

	BEGIN
		DELETE FROM BlogComment WHERE Id = @Id;
	END