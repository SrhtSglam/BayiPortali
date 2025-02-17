declare @itemCode nvarchar(50) SET @itemCode = '2EL.004'

SELECT [Item Category Code], [Description]
FROM [Bilgitas$Product Group] IC
WHERE [Item Category Code] = @itemCode