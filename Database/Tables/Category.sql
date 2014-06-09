CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[LangId] INT NOT NULL, 
    [CategoryName] NVARCHAR(100) NOT NULL,
	
    CONSTRAINT [FK_Category_ToLanguage] FOREIGN KEY ([LangId]) REFERENCES [Language]([Id])
)
