CREATE TABLE [dbo].[Country]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[LangId] INT NOT NULL, 
    [CountryName] NVARCHAR(100) NOT NULL,
	
    CONSTRAINT [FK_Country_ToLanguage] FOREIGN KEY ([LangId]) REFERENCES [Language]([Id])
)
