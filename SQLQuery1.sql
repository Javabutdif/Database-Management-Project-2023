CREATE DATABASE jamesBookStore


CREATE TABLE Customer (
    c_id INT PRIMARY KEY IDENTITY(1,1),
    c_name NVARCHAR(50),
    c_email NVARCHAR(100),
    c_address NVARCHAR(100),
    c_status NVARCHAR(10),
    c_password NVARCHAR(20)
);


CREATE TABLE items (
    item_id INT PRIMARY KEY IDENTITY(1,1),
    item_isbn INT UNIQUE,
    item_title NVARCHAR(50),
    item_author NVARCHAR(50),
    item_genre NVARCHAR(50),
    item_price DECIMAL,
    item_type NVARCHAR(50)
);



SET IDENTITY_INSERT [dbo].[Items] ON;

INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (1, 123, N'Data Structures and Algorithms', N'Malik', N'Education', CAST(980.00 AS Decimal(10, 2)), N'Book');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (2, 124, N'Harry Potter', N'JK Rowling', N'Fantasy, Drama, Fiction', CAST(500.00 AS Decimal(10, 2)), N'AudioBook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (3, 125, N'Snow White and the Seven Dwarfs', N'Grimm', N'Fantasy, Drama, Fiction', CAST(300.00 AS Decimal(10, 2)), N'Ebook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (4, 126, N'The Legends End', N'Keishi Ōtomo', N'Action, Fantasy', CAST(350.00 AS Decimal(10, 2)), N'AudioBook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (5, 127, N'Snow White and the Seven Dwarfs', N'Grimm', N'Fantasy, Drama, Fiction', CAST(350.00 AS Decimal(10, 2)), N'AudioBook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (6, 128, N'Harry Potter', N'JK Rowling', N'Fantasy, Drama, Fiction', CAST(700.00 AS Decimal(10, 2)), N'Book');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (7, 129, N'Harry Potter', N'JK Rowling', N'Fantasy, Drama, Fiction', CAST(400.00 AS Decimal(10, 2)), N'Ebook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (8, 130, N'The Legends End', N'Keishi Ōtomo', N'Action, Fantasy', CAST(300.00 AS Decimal(10, 2)), N'Ebook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (9, 131, N'The Legends End', N'Keishi Ōtomo', N'Action, Fantasy', CAST(350.00 AS Decimal(10, 2)), N'AudioBook');
INSERT INTO [dbo].[Items] ([item_id], [item_isbn], [item_title], [item_author], [item_genre], [item_price], [item_type]) VALUES (10, 132, N'The Legends End', N'Keishi Ōtomo', N'Action, Fantasy', CAST(450.00 AS Decimal(10, 2)), NULL);

SET IDENTITY_INSERT [dbo].[Items] OFF;

CREATE TABLE [dbo].[Cart] (
    [cart_id]      INT  NOT NULL,
    [date_created] DATE NULL,
    PRIMARY KEY CLUSTERED ([cart_id] ASC),
    FOREIGN KEY ([cart_id]) REFERENCES [dbo].[Customers] ([c_id])
);


CREATE TABLE [dbo].[Customers] (
    [c_id]       INT            IDENTITY (1, 1) NOT NULL,
    [c_name]     NVARCHAR (50)  NULL,
    [c_email]    NVARCHAR (100) NULL,
    [c_address]  NVARCHAR (100) NULL,
    [c_status]   NVARCHAR (10)  NULL,
    [c_password] NVARCHAR (60)  NULL,
    PRIMARY KEY CLUSTERED ([c_id] ASC)
);

CREATE TABLE [dbo].[items] (
    [item_id]     INT             NOT NULL,
    [item_isbn]   INT             NULL,
    [item_title]  NVARCHAR (100)  NULL,
    [item_author] NVARCHAR (100)  NULL,
    [item_genre]  NVARCHAR (50)   NULL,
    [item_price]  DECIMAL (10, 2) NULL,
    [item_type]   NVARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([item_id] ASC)
);

CREATE TABLE [dbo].[OrderDetails] (
    [order_id] INT NOT NULL,
    [item_id]  INT NOT NULL,
    [od_qty]   INT NULL,
    PRIMARY KEY CLUSTERED ([order_id] ASC, [item_id] ASC),
    FOREIGN KEY ([item_id]) REFERENCES [dbo].[items] ([item_id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Orders] (
    [order_id]    INT             NOT NULL,
    [order_date]  DATE            NULL,
    [order_total] DECIMAL (10, 2) NULL,
    [order_add]   NVARCHAR (100)  NULL,
    [c_id]        INT             NULL,
    PRIMARY KEY CLUSTERED ([order_id] ASC),
    FOREIGN KEY ([c_id]) REFERENCES [dbo].[Customers] ([c_id])
);

CREATE TABLE [dbo].[Review] (
    [review_id]   INT            NOT NULL,
    [review_rate] INT            NULL,
    [review_com]  NVARCHAR (200) NULL,
    [review_date] DATE           NULL,
    [item.id]     INT            NULL,
    PRIMARY KEY CLUSTERED ([review_id] ASC),
    FOREIGN KEY ([item.id]) REFERENCES [dbo].[items] ([item_id])
);

CREATE TABLE OrderDetails(
    [orderDetails_id] INT NOT NULL,
    [orderDetails_item_name] NVARCHAR(50) NOT NULL,
    [orderDetails_item_qty] INT NOT NULL,
    [orderDetails_item_subTotal] INT NOT NULL,
    PRIMARY KEY CLUSTERED([orderDetails_id] ASC),
    



)

SELECT c_id,c_name, c_email, c_address, cart_id FROM Customers , Cart WHERE c_email = 'jamesgenabio@gmail.com' 
and c_password = '123' and c_id = cart_id;

SELECT items.item_title , items.item_type , Cart_Item.quantity , Cart_Item.total   FROM items, Cart , Cart_Item , Customers WHERE Cart.cart_id = c_id AND items.item_id = Cart_Item.item_id AND Cart.cart_id = Cart_Item.cart_id


SELECT item_title, item_type, quantity, total FROM items,Customers JOIN Cart_Item on Cart_Item.cart_id = Customers.c_id WHERE items.item_id = Cart_Item.item_id AND Customers.c_id = 3  ;


SELECT * FROM Cart_Item , Customers Where cart_id = c_id; 


