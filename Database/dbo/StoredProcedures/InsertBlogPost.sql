CREATE PROCEDURE [dbo].[InsertBlogPost]
(
   @UserName    NVARCHAR(256), 
   @CategoryId  INT,
   @Title       NVARCHAR(512),
   @Description NVARCHAR(1024),
   @Content     NVARCHAR(MAX),
   @MainImage   NVARCHAR(2048),
   @Promote      BIT,
   @Created      DATETIME2(7)

)
AS
	BEGIN
	     INSERT INTO BlogPost
		 (
			[UserName],
			[CategoryId],
			[Title],
			[Description],
			[Content],
			[MainImage],
			[Promote],
			[Created]
		 )

		 VALUES
		 (
			 @UserName,   
			 @CategoryId,
			 @Title, 
			 @Description,
			 @Content,
			 @MainImage,
			 @Promote,
			 @Created    
		 )
END
