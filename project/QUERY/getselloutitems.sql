SELECT [Item No_], [Serial No_], [Bilgitaş Invoice No_], [Bilgitaş Invoice Date] FROM [Bilgitas$Sell-out Ledger Entry]
ORDER BY [Invoice Date]
OFFSET 1 ROWS FETCH NEXT 40 ROWS ONLY