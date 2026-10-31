
USE [POS_SuperMarcket]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CreatedBy] NVARCHAR(100) NOT NULL,
    [LastModifiedBy] NVARCHAR(100) NULL,
    [LastModifiedAtUtc] DATETIME2 NULL,

    [BarcodeNumber] NVARCHAR(50) NOT NULL,
    [ProductName] NVARCHAR(100) NOT NULL,
    [CategoryId] INT NOT NULL,
    [Price] DECIMAL NOT NULL,

    [IsDeleted] BIT NOT NULL
        CONSTRAINT [DF_Products_IsDeleted] DEFAULT 0,

    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [UQ_Products_BarcodeNumber] UNIQUE ([BarcodeNumber])
);
GO
