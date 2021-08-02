INSERT INTO [dbo].[ShoppingCart_Users]
           ([Name]
           ,[Email]
           ,[Password])
VALUES
           ('ADMIN'
           ,'admin@gmail.com'
           ,CONVERT(VARCHAR(32), HashBytes('MD5', 'ADMIN'), 2));

GO
INSERT INTO [dbo].[Product]
           ([Code]
           ,[Name]
           ,[Price]
           ,[Type]
           ,[Active])
     VALUES
           ('10001'
           ,'Lord of the Rings'
           ,'10.00'
           ,0
           ,1);
GO
INSERT INTO [dbo].[Product]
           ([Code]
           ,[Name]
           ,[Price]
           ,[Type]
           ,[Active])
     VALUES
           ('10002'
           ,'The Hobbit'
           ,'5.00'
           ,0
           ,1);

GO
INSERT INTO [dbo].[Product]
           ([Code]
           ,[Name]
           ,[Price]
           ,[Type]
           ,[Active])
     VALUES
           ('20001'
           ,'Game of Thrones'
           ,'9.00'
           ,1
           ,1);
GO
INSERT INTO [dbo].[Product]
           ([Code]
           ,[Name]
           ,[Price]
           ,[Type]
           ,[Active])
     VALUES
           ('20110'
           ,'Breaking Bad'
           ,'7.00'
           ,1
           ,1);

