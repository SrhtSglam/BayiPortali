DECLARE @UserId NVARCHAR(MAX) SET @UserId = 'BAŞAK'
DECLARE @ItemNo NVARCHAR(MAX) SET @ItemNo = '31KM1702K38NLO'
DECLARE @DateTime DATETIME SET @DateTime = '2013-12-18 14:01:07'

DELETE FROM [Bilgitas$Web Basket]
WHERE [User ID] = @UserId AND [Item No_] = @ItemNo AND [Time Stamp] = @DateTime AND [Confirmed] = 0