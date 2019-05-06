CREATE PROCEDURE [dbo].[InsertBlogComment]
	@BlogId INT,
	@UserName NVARCHAR(256),
	@Comment NVARCHAR(2048),
	@LastUpdate DATETIME2(7),
	@Created DATETIME2(7)

AS
	BEGIN
		INSERT INTO BlogComment ([BlogId], [UserName], [Comment], [LastUpdate], [Created])
						   VALUES(@BlogId, @UserName, @Comment, @LastUpdate, @Created);
	END