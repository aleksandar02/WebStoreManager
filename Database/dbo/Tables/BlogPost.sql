CREATE TABLE [dbo].[BlogPost]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	[UserName] NVARCHAR(256) NOT NULL,
	[CategoryId] INT NOT NULL,
    [Title] NVARCHAR(512) NOT NULL, 
    [Description] NVARCHAR(1024) NOT NULL, 
	[Content] NVARCHAR(MAX) NOT NULL, 
    [MainImage] NVARCHAR(2048) NOT NULL, 
    [Promote] BIT NOT NULL, 
    [Created] DATETIME2 NOT NULL,     
    CONSTRAINT [FK_BlogPost_BlogCategory] FOREIGN KEY ([CategoryId]) REFERENCES [BlogCategory]([Id])
    
    
)
