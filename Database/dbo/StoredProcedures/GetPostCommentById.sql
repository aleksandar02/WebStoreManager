CREATE PROCEDURE [dbo].[GetPostCommentById]
	@Id INT
AS
	BEGIN
		SELECT [Id]
			  ,[UserName]
			  ,[BlogId]
			  ,[Comment]
			  ,[LastUpdate]
			  ,[Created]

			  FROM BlogComment
			  WHERE Id = @Id;
	END
