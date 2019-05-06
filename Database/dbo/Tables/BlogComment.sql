CREATE TABLE [dbo].[BlogComment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [UserName] NVARCHAR(256) NOT NULL, 
    [BlogId] INT NOT NULL, 
    [Comment] NVARCHAR(2048) NOT NULL, 
    [LastUpdate] DATETIME2 NOT NULL, 
    [Created] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_BlogComment_BlogPost] FOREIGN KEY ([BlogId]) REFERENCES [BlogPost]([Id]) ON DELETE CASCADE
)
