CREATE PROCEDURE [dbo].[GetModelListProcedure]
(
	@LangId INT
)
AS



	SELECT Model.Id, Model.ModelName, Brand.BrandName, Model.MachineTypeId
	FROM Model 
 LEFT OUTER JOIN	
			
			
			Brand ON Model.BrandId = Brand.Id
			
			
		
RETURN