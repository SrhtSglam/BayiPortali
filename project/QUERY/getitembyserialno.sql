SELECT ILE.[Item No_], ILE.[Posting Date], ILE.[Serial No_], ILE.[Description] , ILE.[Customer No_]
,CUS.Name FROM [Bilgitas$Item Ledger Entry] ILE
INNER JOIN [Bilgitas$Customer] CUS ON CUS.No_ = ILE.[Customer No_]

WHERE ILE.[Serial No_] = 'NXCY089975' and ILE.[Entry Type]=1