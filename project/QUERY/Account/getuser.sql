DECLARE @name nvarchar(50) = 'ERDEN_ADMIN'
DECLARE @password nvarchar(50) = '9319'
SELECT [User ID], [Password], [Web Visibility], [E-mail], [Customer No_], 
[Sipariş], [Sipariş Geç], [Sipariş Kontrol], [Sipariş Onayla],
[İşlevler], [Sell-out Giriş], [Numaratör Giriş], [Garanti], [Garanti İstek Giriş], [Garanti Süreç Takibi],
[Raporlar], [Mizan], [Ekstre],
[Duyuru Düzenle], [Download]
FROM [Bilgitas$Light User] WHERE [User ID] = @name AND [Password] = @password