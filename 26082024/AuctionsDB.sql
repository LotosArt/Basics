CREATE DATABASE AuctionsDb;
GO

USE AuctionsDb;
GO

CREATE TABLE [Auctions] (
    [AuctionID] INT PRIMARY KEY IDENTITY,
    [ItemName] NVARCHAR(100) NOT NULL,
    [CurrentBid] DECIMAL(10, 2) NOT NULL,
    [AuctionEndTime] DATETIME NOT NULL
);

CREATE TABLE [Bidders] (
    [BidderID] INT PRIMARY KEY IDENTITY,
    [BidderName] NVARCHAR(50) NOT NULL
);

CREATE TABLE [Bids] (
    [BidID] INT PRIMARY KEY IDENTITY,
    [AuctionID] INT,
    [BidderID] INT,
    [BidAmount] DECIMAL(10, 2) NOT NULL,
    [BidTime] DATETIME NOT NULL,
    CONSTRAINT FK_Bids_Auctions FOREIGN KEY ([AuctionID]) REFERENCES [Auctions]([AuctionID]),
    CONSTRAINT FK_Bids_Bidders FOREIGN KEY ([BidderID]) REFERENCES [Bidders]([BidderID])
);
GO

INSERT INTO [Auctions] (ItemName, CurrentBid, AuctionEndTime)
VALUES
    ('Laptop', 100.00, '2024-09-01 12:00:00'),
    ('Smartphone', 200.00, '2024-09-05 15:00:00'),
    ('Tablet', 150.00, '2024-09-07 18:00:00');
GO

INSERT INTO [Bidders] (BidderName)
VALUES
    ('John Doe'),
    ('Jane Smith'),
    ('Alice Johnson');
GO

INSERT INTO [Bids] (AuctionID, BidderID, BidAmount, BidTime)
VALUES
    (1, 1, 120.00, GETDATE()),  
    (2, 2, 250.00, GETDATE()),  
    (3, 3, 180.00, GETDATE());  
GO

