SELECT BIC.[Description], BI.[Product Group Code], BI.[No_], BI.[Description], [No_ 2] 
FROM [Bilgitas$Item] BI 
INNER JOIN [Bilgitas$Item Category] BIC on BIC.[Code] = BI.[Item Category Code]
ORDER BY BI.[No_]
OFFSET 1 ROWS FETCH NEXT 20 ROWS ONLY