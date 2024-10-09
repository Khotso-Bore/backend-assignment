IF EXISTS (select * from sys.databases where name = 'OT_Assessment_DB')
BEGIN 
    DROP DATABASE OT_Assessment_DB;
END 
	Create Database OT_Assessment_DB;
Go
USE OT_Assessment_DB;

CREATE TABLE CasinoWagers (
	WagerId uniqueidentifier PRIMARY KEY,
	Theme varchar(50),
	Provider varchar(50),
	GameName varchar(50),
	TransactionId uniqueidentifier,
	BrandId uniqueidentifier,
	AccountId uniqueidentifier,
	Username varchar(50),
	ExternalReferenceId uniqueidentifier,
	TransactionTypeId uniqueidentifier,
	Amount DECIMAL(30,12),
	CreatedDateTime DATE,
	NumberOfBets INT,
	CountryCode char(2),
	SessionData VARCHAR(MAX),
	Duration INT,
	INDEX IX_AccountId (AccountId),
);
Go

CREATE PROCEDURE TopSpenders
    @Count INT         
AS
BEGIN
    SELECT TOP (@Count)
      [AccountId]
      ,MAX([Username]) Username
      ,SUM([Amount]) TotalAmount
  FROM [OT_Assessment_DB].[dbo].[CasinoWagers]
  GROUP BY [AccountId]
  ORDER BY [TotalAmount] DESC
END;



