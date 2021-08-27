CREATE TABLE dbo.NumberCollection
(
	NumberCollectionId INT IDENTITY(1,1) NOT NULL,
	Number1 INT NOT NULL,
	Number2 INT NULL,
	Number3 INT NULL,
	Number4 INT NULL,
	Number5 INT NULL,
	Number6 INT NULL,
	Number7 INT NULL,
	Number8 INT NULL,
	Number9 INT NULL,
	Number10 INT NULL,
	CreatedOnUtc DATETIME2(7) NOT NULL CONSTRAINT [DF_dbo.NumberCollection_CreatedOnUtc] DEFAULT (GETUTCDATE()),
	CONSTRAINT [PK_dbo.NumberCollection_NumberCollectionId] PRIMARY KEY ([NumberCollectionId])
)

SELECT * FROM dbo.NumberCollection
