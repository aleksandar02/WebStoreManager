CREATE PROCEDURE [dbo].[DeleteBlogPost]
	@Id INT
AS
	BEGIN
		DELETE FROM BlogPost WHERE Id = @Id;
	END