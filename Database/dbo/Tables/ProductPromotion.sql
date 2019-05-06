CREATE TABLE [dbo].[ProductPromotion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [ProductId] INT NOT NULL, 
    [Lifetime] DATETIME2 NOT NULL, 
    [Created] DATETIME2 NOT NULL, 
    [Percent] DECIMAL(5, 2) NOT NULL
)
