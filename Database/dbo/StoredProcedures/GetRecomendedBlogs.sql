CREATE PROCEDURE [dbo].[GetRecomendedBlogs]
(
	@Id INT
)
	
AS
	BEGIN
		SELECT TOP 3 BP.[Id] , 
					 BP.[UserName] ,
					 BP.[CategoryId],
					 BP.[Title], 
					 BP.[Description], 
					 BP.[Content], 
					 BP.[MainImage], 
					 BP.[Promote], 
					 BP.[Created],
					 BC.[Category] AS CategoryDescription
				     
					 FROM BlogPost AS BP
					 INNER JOIN BlogCategory BC ON BC.Id = BP.CategoryId
					 WHERE BP.[Id] <> @Id
					 ORDER BY Created DESC;
	END
