
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
[Type]  int  NULL,
[Active] bit NOT NULL,
)	
IF OBJECT_ID(N'dbo.ShoppingCart', N'U') IS  NULL
CREATE TABLE ShoppingCart ( 
[ShoppingCartId] int identity(1,1) PRIMARY KEY,
[ProductId]      int NOT NULL,
[UserId]         int NOT NULL,
[Quantity]       int NOT NULL,
[CreationDate]   datetime NOT NULL,
FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
FOREIGN KEY (UserId) REFERENCES ShoppingCart_Users(UserId)
)	
