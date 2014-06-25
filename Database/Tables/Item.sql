CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[ModelId] INT NOT NULL,
	[CategoryId] INT NOT NULL,
	[Article] NVARCHAR(50) NOT NULL DEFAULT '', 
	[CountryId] INT NOT NULL,
	[ImageUrl] NVARCHAR(MAX) NULL,
	[CostBase] DECIMAL(16, 2) NOT NULL DEFAULT 0, 
    [CostNative] DECIMAL(16, 2) NOT NULL DEFAULT 0, 
    [IsAvailable] BIT NOT NULL DEFAULT 0, 
    [ManufacturerId] INT NULL, 
    CONSTRAINT [FK_Item_ToModel] FOREIGN KEY ([ModelId]) REFERENCES [Model]([Id]),
    CONSTRAINT [FK_Item_ToCategory] FOREIGN KEY ([CategoryId]) REFERENCES [Category]([Id]),
	CONSTRAINT [FK_Item_ToCountry] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id]), 
    CONSTRAINT [FK_Item_ToManufacturer] FOREIGN KEY ([ManufacturerId]) REFERENCES [Manufacturer]([Id])
)
