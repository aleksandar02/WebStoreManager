CREATE PROCEDURE [dbo].[GetAllBlogPosts]
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
			  INNER JOIN [BlogCategory] BC ON BC.Id = BP.CategoryId
			  ORDER BY Created DESC;
	END
