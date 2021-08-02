
IF OBJECT_ID(N'dbo.ShoppingCart_Users', N'U') IS  NULL
CREATE TABLE ShoppingCart_Users ( 
[UserId] int identity(1,1) PRIMARY KEY,
[Name] varchar(100) NOT NULL,
[Email] varchar(100) NOT NULL,
[Password] varchar(max) NOT NULL,
)	
GO
IF OBJECT_ID(N'dbo.Product', N'U') IS  NULL
CREATE TABLE Product ( 
[ProductId] int identity(1,1) PRIMARY KEY,
[Code] varchar(100) NOT NULL,
[Name] varchar(100) NOT NULL,
[Price] decimal(18,2) NOT NULL,
[Type]  INT  NULL,
[Active] bit NOT NULL,
)	