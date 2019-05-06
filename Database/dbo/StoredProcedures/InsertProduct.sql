CREATE PROCEDURE [dbo].[InsertProduct]
	@CategoryId INT,
	@UserName NVARCHAR(256),
	@Name NVARCHAR(128),
	@Description NVARCHAR(1024),
	@Price DECIMAL(6,2),
	@Barcode NVARCHAR(50),
	@Image NVARCHAR(2048),
	@Created DATETIME2(7)
AS
	BEGIN
		INSERT INTO Product([CategoryId],
							[Username],
							[Name],
							[Description],
							[Price],
							[Barcode],
							[Image],
							[Created])

		            VALUES(@CategoryId,
						   @UserName,
						   @Name,
						   @Description,
						   @Price,
						   @Barcode,
						   @Image,
						   @Created);
	END
