CREATE PROCEDURE [dbo].[GetItemListProcedure]
(
	@LangId INT
)
AS



	SELECT Item.Id, Model.BrandId, Brand.BrandName, Model.MachineTypeId, 
	(SELECT TOP (1) MachineType.Text FROM MachineType WHERE Model.MachineTypeId = MachineType.MachineTypes AND MachineType.LangId = @LangId) AS TypeName,
			Item.ModelId, Model.ModelName, Item.CategoryId, Category.CategoryName, Item.CatalogNumber,
			Item.CountryId, Country.CountryName, DescriptionText.Text

	FROM Item 
 LEFT OUTER JOIN	
			
			Model ON Item.ModelId = Model.Id LEFT OUTER JOIN
			Brand ON Model.BrandId = Brand.Id LEFT OUTER JOIN
			
			Category ON Item.CategoryId = Category.Id AND Category.LangId = @LangId LEFT OUTER JOIN
			Country ON Item.CountryId = Country.Id AND Country.LangId = @LangId LEFT OUTER JOIN
			DescriptionText ON Item.DescriptionId = DescriptionText.Id AND DescriptionText.LangId = @LangId
		
RETURN