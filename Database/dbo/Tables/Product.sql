CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[CategoryId] INT NOT NULL DEFAULT 0,
    [Name] NVARCHAR(128) NULL, 
    [Description] NVARCHAR(1024) NULL, 
    [Price] DECIMAL(6, 2) NOT NULL, 
    [Barcode] NVARCHAR(50) NULL, 
    [Image] NVARCHAR(2048) NULL, 
    [Username] NVARCHAR(256) NOT NULL, 
    [Created] DATETIME2 NOT NULL,
	
)
