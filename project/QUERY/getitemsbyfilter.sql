SELECT BIC.[Description], 
       BI.[Product Group Code], 
       BI.[No_], 
       BI.[Description], 
       BI.[No_ 2], 
       SP.[Currency Code], 
       SP.[Unit Price]
FROM [Bilgitas$Item] BI
INNER JOIN [Bilgitas$Item Category] BIC 
    ON BIC.[Code] = BI.[Item Category Code]
LEFT JOIN [Bilgitas$Sales Price] SP 
    ON SP.[Item No_] = BI.[No_]
WHERE BI.[Product Group Code] = 'EPS' 
  AND (SP.[Sales Code] != 'LISTE' OR SP.[Sales Code] IS NULL)
ORDER BY BI.[No_];
