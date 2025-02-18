SELECT TOP 40 BIC.[Description], BI.[Product Group Code], BI.[No_], BI.[Description], [No_ 2], BI.[Item Transfer Type]
FROM [Bilgitas$Item] BI 
INNER JOIN [Bilgitas$Item Category] BIC on BIC.[Code] = BI.[Item Category Code]
ORDER BY BI.[No_]