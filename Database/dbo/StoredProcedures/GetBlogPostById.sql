CREATE PROCEDURE [dbo].[GetBlogPostById]
	@Id INT
AS
	BEGIN
		SELECT BP.[Id]
			  ,BP.[UserName]
			  ,BP.[CategoryId]
			  ,BP.[Title]
			  ,BP.[Description]
			  ,BP.[Content]
			  ,BP.[MainImage]
			  ,BP.[Promote]
			  ,BP.[Created]
			  ,BC.[Category] AS CategoryDescription
					  
			  FROM [BlogPost] AS BP
			  INNER JOIN BlogCategory BC ON BC.Id = BP.CategoryId
			  WHERE BP.Id = @Id;
	END
