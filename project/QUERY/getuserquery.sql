DECLARE @name nvarchar(50) = 'ERDEN_ADMIN'
DECLARE @password nvarchar(50) = '9319'

SELECT [User ID], [Password], [Web Visibility], [Customer No_] from [Bilgitas$Light User] WHERE
[User ID] = @name AND [Password] = @password