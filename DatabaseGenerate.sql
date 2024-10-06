/*CREATE TABLE Players (
	Id uniqueidentifier PRIMARY KEY,
	Username varchar(50),
	Constraint AK_Username UNIQUE(Username)
);

CREATE TABLE Providers(
	Id uniqueidentifier PRIMARY KEY,
	Name varchar(50),
	Constraint AK_Name UNIQUE(Name)
);

CREATE TABLE Games(
	Id uniqueidentifier PRIMARY KEY,
	ProviderId uniqueidentifier,
	Name varchar(50),
	Theme varchar(50),
	CONTRAINT FK_ProviderId FOREIGN KEY (ProviderId) REFERENCES Providers(Id),
);*/

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



