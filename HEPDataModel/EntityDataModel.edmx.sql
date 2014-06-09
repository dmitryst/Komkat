
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/23/2013 15:51:06
-- Generated from EDMX file: C:\Users\Дмитрий\Desktop\HEP\HEPDataModel\EntityDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HEPDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[MachineType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MachineType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Brands'
CREATE TABLE [dbo].[Brands] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LangId] int  NOT NULL,
    [CategoryName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LangId] int  NOT NULL,
    [CountryName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Currencies'
CREATE TABLE [dbo].[Currencies] (
    [Id] smallint  NOT NULL,
    [CurrencyDesignation] nchar(3)  NOT NULL
);
GO

-- Creating table 'DescriptionTexts'
CREATE TABLE [dbo].[DescriptionTexts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LangId] int  NOT NULL,
    [Text] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModelId] int  NOT NULL,
    [CategoryId] int  NOT NULL,
    [DescriptionId] int  NOT NULL,
    [CatalogNumber] nvarchar(15)  NOT NULL,
    [CountryId] int  NOT NULL,
    [ImageUrl] nvarchar(max)  NULL
);
GO

-- Creating table 'ItemPriceRegs'
CREATE TABLE [dbo].[ItemPriceRegs] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Period] datetime  NOT NULL,
    [CurrencyId] smallint  NOT NULL,
    [ItemId] int  NOT NULL,
    [Value] decimal(16,2)  NOT NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Models'
CREATE TABLE [dbo].[Models] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MachineTypeId] int  NOT NULL,
    [BrandId] int  NOT NULL,
    [ModelName] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'MachineTypes'
CREATE TABLE [dbo].[MachineTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MachineType] tinyint  NOT NULL,
    [LangId] int  NOT NULL,
    [Text] nvarchar(30)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Brands'
ALTER TABLE [dbo].[Brands]
ADD CONSTRAINT [PK_Brands]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Currencies'
ALTER TABLE [dbo].[Currencies]
ADD CONSTRAINT [PK_Currencies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DescriptionTexts'
ALTER TABLE [dbo].[DescriptionTexts]
ADD CONSTRAINT [PK_DescriptionTexts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemPriceRegs'
ALTER TABLE [dbo].[ItemPriceRegs]
ADD CONSTRAINT [PK_ItemPriceRegs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Models'
ALTER TABLE [dbo].[Models]
ADD CONSTRAINT [PK_Models]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MachineTypes'
ALTER TABLE [dbo].[MachineTypes]
ADD CONSTRAINT [PK_MachineTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BrandId] in table 'Models'
ALTER TABLE [dbo].[Models]
ADD CONSTRAINT [FK_Model_ToBrand]
    FOREIGN KEY ([BrandId])
    REFERENCES [dbo].[Brands]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Model_ToBrand'
CREATE INDEX [IX_FK_Model_ToBrand]
ON [dbo].[Models]
    ([BrandId]);
GO

-- Creating foreign key on [LangId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Category_ToLanguage]
    FOREIGN KEY ([LangId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Category_ToLanguage'
CREATE INDEX [IX_FK_Category_ToLanguage]
ON [dbo].[Categories]
    ([LangId]);
GO

-- Creating foreign key on [CategoryId] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_ToCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_ToCategory'
CREATE INDEX [IX_FK_Item_ToCategory]
ON [dbo].[Items]
    ([CategoryId]);
GO

-- Creating foreign key on [LangId] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [FK_Country_ToLanguage]
    FOREIGN KEY ([LangId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Country_ToLanguage'
CREATE INDEX [IX_FK_Country_ToLanguage]
ON [dbo].[Countries]
    ([LangId]);
GO

-- Creating foreign key on [CountryId] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_ToCountry]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_ToCountry'
CREATE INDEX [IX_FK_Item_ToCountry]
ON [dbo].[Items]
    ([CountryId]);
GO

-- Creating foreign key on [CurrencyId] in table 'ItemPriceRegs'
ALTER TABLE [dbo].[ItemPriceRegs]
ADD CONSTRAINT [FK_ItemPriceReg_ToCurrency]
    FOREIGN KEY ([CurrencyId])
    REFERENCES [dbo].[Currencies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemPriceReg_ToCurrency'
CREATE INDEX [IX_FK_ItemPriceReg_ToCurrency]
ON [dbo].[ItemPriceRegs]
    ([CurrencyId]);
GO

-- Creating foreign key on [LangId] in table 'DescriptionTexts'
ALTER TABLE [dbo].[DescriptionTexts]
ADD CONSTRAINT [FK_DescriptionText_ToLanguage]
    FOREIGN KEY ([LangId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DescriptionText_ToLanguage'
CREATE INDEX [IX_FK_DescriptionText_ToLanguage]
ON [dbo].[DescriptionTexts]
    ([LangId]);
GO

-- Creating foreign key on [DescriptionId] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_ToDescription]
    FOREIGN KEY ([DescriptionId])
    REFERENCES [dbo].[DescriptionTexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_ToDescription'
CREATE INDEX [IX_FK_Item_ToDescription]
ON [dbo].[Items]
    ([DescriptionId]);
GO

-- Creating foreign key on [ModelId] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_Item_ToModel]
    FOREIGN KEY ([ModelId])
    REFERENCES [dbo].[Models]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Item_ToModel'
CREATE INDEX [IX_FK_Item_ToModel]
ON [dbo].[Items]
    ([ModelId]);
GO

-- Creating foreign key on [ItemId] in table 'ItemPriceRegs'
ALTER TABLE [dbo].[ItemPriceRegs]
ADD CONSTRAINT [FK_ItemPriceReg_ToItem]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemPriceReg_ToItem'
CREATE INDEX [IX_FK_ItemPriceReg_ToItem]
ON [dbo].[ItemPriceRegs]
    ([ItemId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------