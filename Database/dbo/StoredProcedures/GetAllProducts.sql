CREATE PROCEDURE [dbo].[GetAllProducts]

AS
	BEGIN
		SELECT	 P.[Id]
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
				INNER JOIN [ProductCategory] PC ON PC.Id = P.CategoryId;
	END
