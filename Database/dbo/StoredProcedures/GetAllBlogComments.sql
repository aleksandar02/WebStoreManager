CREATE PROCEDURE [dbo].[GetAllBlogComments]
	@Id INT
AS
	BEGIN
		SELECT [Id],
			   [BlogId],
			   [UserName],
			   [Comment],
			   [LastUpdate],
			   [Created]
				
			   FROM BlogComment					   
			   WHERE BlogId = @Id
			   ORDER BY Created DESC;
	END
