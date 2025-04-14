DECLARE @itemCode NVARCHAR(MAX) SET @itemCode = '03EPSL210'

SELECT [Picture], [Base Unit of Measure], [Aylık Baskı Hacmi], [Baskı Kapasitesi (Num)] FROM [Bilgitas$Item]
WHERE [No_] = @itemCode