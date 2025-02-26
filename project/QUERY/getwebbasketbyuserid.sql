DECLARE @UserId NVARCHAR(MAX) SET @UserId = 'ARYA'

SELECT 
DATEADD(mi, DATEDIFF(mi, GETUTCDATE(), GETDATE()), WB.[Time Stamp]), 
WB.[Item No_], Item.[Description], WB.[Sales Description], WB.[Quantity], Item.[Base Unit of Measure]
FROM [Bilgitas$Web Basket] WB
INNER JOIN [Bilgitas$Item] Item
ON WB.[Item No_] = Item.[No_]
WHERE WB.[User ID] = @UserId AND WB.[Confirmed] = 1

//WB.CONFIRMED = 0 / 1