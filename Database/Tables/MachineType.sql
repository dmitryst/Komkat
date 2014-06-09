CREATE TABLE [dbo].[MachineType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[MachineTypes] SMALLINT NOT NULL, 
	[LangId] INT NOT NULL,
	[Text] NVARCHAR(30) NOT NULL,

	CONSTRAINT [FK_TypeName_ToLanguage] FOREIGN KEY ([LangId]) REFERENCES [Language]([Id]),
)
