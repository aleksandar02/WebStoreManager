CREATE PROCEDURE [dbo].[EditProduct]
	@Id INT,
	@CategoryId INT,
	@UserName NVARCHAR(256),
	@Name NVARCHAR(128),
	@Description NVARCHAR(1024),
	@Price DECIMAL(6,2),
	@Barcode NVARCHAR(50),
	@Image NVARCHAR(2048)

AS
	BEGIN
		UPDATE Product 
					SET CategoryId = @CategoryId,
						Username = @UserName,
						Name = @Name,
						Description = @Description,
						Price = @Price,
						Barcode = @Barcode,
						Image = @Image
						
						WHERE Id = @Id;
	END