CREATE TABLE [dbo].[DescriptionText]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL, 
    [LangId] INT NOT NULL, 
    [Text] NVARCHAR(100) NOT NULL,
	
    CONSTRAINT [FK_DescriptionText_ToLanguage] FOREIGN KEY ([LangId]) REFERENCES [Language]([Id]),
)
