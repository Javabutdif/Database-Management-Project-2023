USE [myDb]
GO

/****** Object: Table [dbo].[items] Script Date: 17/12/2023 3:26:01 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[items] (
    [item_id]     INT             NOT NULL,
    [item_isbn]   INT             NULL,
    [item_title]  NVARCHAR (100)  NULL,
    [item_author] NVARCHAR (100)  NULL,
    [item_genre]  NVARCHAR (50)   NULL,
    [item_price]  DECIMAL (10, 2) NULL,
    [item_type]   NVARCHAR (50)   NULL
);


