CREATE TABLE Players (
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
);

CREATE TABLE CasinoWagers (
	Id uniqueidentifier PRIMARY KEY,
	AccountId uniqueidentifier,
	GameId uniqueidentifier,
	NumberOfBets int,
	CountryCode char(2),
	Amount DECIMAL(30,12),
	Duration INT,
	SesstionData NVARCHAR,
	ExternalReferenceId uniqueidentifier,
	TransactionTypeId uniqueidentifier,
	TransactionId uniqueidentifier,
	CreatedDate DATE,
	CONTRAINT FK_AccountId FOREIGN KEY (AccountId) REFERENCES Players(Id),
	CONTRAINT FK_GameId FOREIGN KEY (GameId) REFERENCES Games(Id),
);



