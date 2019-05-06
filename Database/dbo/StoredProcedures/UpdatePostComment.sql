CREATE PROCEDURE [dbo].[UpdatePostComment]
	@Id INT,
	@Comment NVARCHAR(2048),
	@LastUpdate DATETIME2(7)
AS
	BEGIN
		UPDATE BlogComment
		SET 
           Comment = @Comment,
		   LastUpdate = @LastUpdate

		   WHERE Id = @Id;
	END