CREATE PROCEDURE [dbo].[GetItemListProcedure]
(
	@LangId INT
)
AS



	SELECT Item.Id, Model.BrandId, Brand.BrandName, Model.MachineTypeId, 
			(SELECT TOP (1) MachineType.Text FROM MachineType WHERE Model.MachineTypeId = MachineType.MachineTypes AND MachineType.LangId = @LangId) AS TypeName,
			Item.ModelId, Model.ModelName, Item.CategoryId, Category.CategoryName, 
			(SELECT TOP (1) OEM.OEMCode FROM OEM WHERE OEM.ItemId = Item.Id) AS OEMCode,
			Item.CountryId, Country.CountryName, DescriptionText.Text, Manufacturer.ManufacturerName, Item.CostNative, Item.IsAvailable

	FROM Item 
 LEFT OUTER JOIN	
			
			Model ON Item.ModelId = Model.Id LEFT OUTER JOIN
			Brand ON Model.BrandId = Brand.Id LEFT OUTER JOIN
			

			Category ON Item.CategoryId = Category.Id AND Category.LangId = @LangId LEFT OUTER JOIN
			Country ON Item.CountryId = Country.Id AND Country.LangId = @LangId LEFT OUTER JOIN
			DescriptionText ON Item.Id = DescriptionText.ItemId AND DescriptionText.LangId = @LangId LEFT OUTER JOIN
			Manufacturer ON Item.ManufacturerId = Manufacturer.Id
		
RETURN