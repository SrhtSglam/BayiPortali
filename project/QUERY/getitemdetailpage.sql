SELECT 
IAC.[Item No_], 
IAC.[Component Item No_], 
IAC.[Description],
*
FROM [Bilgitas$Item Applicable Components] IAC

SELECT * FROM [Bilgitas$Item] WHERE No_ = '03KYFS2020D'

select IC.[Description] ,IT.[Product Group Code], IT.[No_], IT.[Description], IT.[No_ 2] 
,SP.[Currency Code] ,SP.[Unit Price] 
			from [Bilgitas$Item Applicable Components] IAC
INNER JOIN [Bilgitas$Item] IT ON IT.No_= IAC.[Component Item No_]
INNER JOIN [Bilgitas$Item Category] IC on IC.[Code] = IT.[Item Category Code]
LEFT JOIN [Bilgitas$Sales Price] SP on SP.[Item No_] = IT.[No_] AND SP.[Sales Code] = 'BAYI'
where IAC.[Item No_]='04TAS2554CI'

select * from [Bilgitas$Item] where No_ IN(
'03C5300DN1',
'03C5300DN2',
'03C5300DN3',
'03C5300DN4',
'39KM1702KV8NL0'
)
