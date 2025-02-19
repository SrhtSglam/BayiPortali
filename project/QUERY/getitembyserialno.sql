DECLARE @ItemCode nvarchar(MAX) SET @ItemCode = '4NY019664'

SELECT SNI.[Item No_], SNI.[Serial No_], BI.[Description]
FROM [Bilgitas$Serial No_ Information] SNI
LEFT JOIN [Bilgitas$Item] BI ON BI.No_ = SNI.[Item No_]
WHERE [Serial No_] = @ItemCode