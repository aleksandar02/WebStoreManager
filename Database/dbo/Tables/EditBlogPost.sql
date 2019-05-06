CREATE PROCEDURE [dbo].[EditBlogPost]
   @Id          INT,
   @UserName    NVARCHAR(256), 
   @CategoryId  INT,
   @Title       NVARCHAR(512),
   @Description NVARCHAR(1024),
   @Content     NVARCHAR(MAX),
   @MainImage   NVARCHAR(2048),
   @Promote     BIT
AS
	BEGIN
		UPDATE BlogPost 
					SET UserName = @UserName,
						CategoryId = @CategoryId,
						Title = @Title,
						Description = @Description,
						Content = @Content,
						MainImage = @MainImage,
						Promote = @Promote

					WHERE Id = @Id;
	END
