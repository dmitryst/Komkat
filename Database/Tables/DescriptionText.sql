CREATE TABLE [dbo].[DescriptionText]
(
	[Id] INT PRIMARY KEY IDENTITY (1,1) NOT NULL, 
	[ItemId] INT NOT NULL, 
    [LangId] INT NOT NULL, 
    [Text] NVARCHAR(300) NOT NULL,
	CONSTRAINT [FK_DescriptionText_ToLanguage] FOREIGN KEY ([LangId]) REFERENCES [Language]([Id]), 
    CONSTRAINT [FK_DescriptionText_ToItem] FOREIGN KEY ([ItemId]) REFERENCES [Item]([Id]), 
    CONSTRAINT [AK_DescriptionText_LangId_ItemId] UNIQUE ([LangId],[ItemId]),
)
