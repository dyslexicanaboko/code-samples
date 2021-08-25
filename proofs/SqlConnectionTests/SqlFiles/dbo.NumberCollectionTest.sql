CREATE TABLE dbo.NumberCollectionTest
(
	NumberCollectionTestId INT IDENTITY(1,1) NOT NULL,
	Number INT NOT NULL,
	CreatedOnUtc DATETIME2(7) NOT NULL CONSTRAINT [DF_dbo.NumberCollectionTest_CreatedOnUtc] DEFAULT (GETUTCDATE()),
	CONSTRAINT [PK_dbo.NumberCollectionTest_NumberCollectionTestId] PRIMARY KEY ([NumberCollectionTestId])
)

SELECT * FROM dbo.NumberCollectionTest
