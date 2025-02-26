SELECT [Entry No_], [Item No_], [Serial No_], [Bilgitaş Invoice No_], [Bilgitaş Invoice Date] 
FROM [Bilgitas$Sell-out Ledger Entry]
ORDER BY [Entry No_]
OFFSET 1 ROWS FETCH NEXT 40 ROWS ONLY