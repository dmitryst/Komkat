CREATE TABLE [dbo].[ItemPriceReg]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Period] DATETIME2 NOT NULL,    
    [CurrencyId] SMALLINT NOT NULL, 
    [ItemId] INT NOT NULL,	
	[Value] DECIMAL(16, 2) NOT NULL,  
	CONSTRAINT [FK_ItemPriceReg_ToCurrency] FOREIGN KEY ([CurrencyId]) REFERENCES [Currency]([Id]),
    CONSTRAINT [FK_ItemPriceReg_ToItem] FOREIGN KEY ([ItemId]) REFERENCES [Item]([Id]), 
)
