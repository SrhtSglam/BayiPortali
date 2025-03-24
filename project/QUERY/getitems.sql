SELECT BIC.[Description], BI.[Product Group Code], 
BI.[No_], BI.[Description], BI.[No_ 2], 
SP.[Currency Code], SP.[Unit Price], BI.[Picture]
FROM [Bilgitas$Item] BI 
INNER JOIN [Bilgitas$Item Category] BIC on 
BIC.[Code] = BI.[Item Category Code]
LEFT JOIN [Bilgitas$Sales Price] SP on
SP.[Item No_] = BI.[No_]
WHERE SP.[Sales Code] != 'LISTE' OR SP.[Sales Code] IS NULL
ORDER BY BI.[No_]
OFFSET @Offset ROWS FETCH NEXT @ItemPerPage ROWS ONLY