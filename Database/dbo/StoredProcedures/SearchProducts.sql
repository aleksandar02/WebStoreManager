CREATE PROCEDURE [dbo].[SearchProducts]
	@Search NVARCHAR(128)
AS
	BEGIN
		SELECT TOP 100
				 P.[Id]
				,P.[CategoryId]
				,P.[Name]
				,P.[Description]
				,P.[Price]
				,P.[Barcode]
				,p.[Image]
				,P.[Username]
				,P.[Created]
				,PC.[Description] AS CategoryDescription

				FROM [Product] AS P
				INNER JOIN [ProductCategory] PC ON PC.Id = P.CategoryId
				WHERE ([Name] LIKE @Search + '%' OR @Search = '')
				ORDER BY Created;
	END
