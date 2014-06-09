CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[ModelId] INT NOT NULL,
	[CategoryId] INT NOT NULL,
	[DescriptionId] INT NOT NULL,
	[CatalogNumber] NVARCHAR(15) NOT NULL,
	[CountryId] INT NOT NULL,
	[ImageUrl] NVARCHAR(MAX) NULL

	
	CONSTRAINT [FK_Item_ToModel] FOREIGN KEY ([ModelId]) REFERENCES [Model]([Id]),
	CONSTRAINT [FK_Item_ToCategory] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]),
	CONSTRAINT [FK_Item_ToDescription] FOREIGN KEY ([DescriptionId]) REFERENCES [DescriptionText]([Id]),
	CONSTRAINT [FK_Item_ToCountry] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id])
)
