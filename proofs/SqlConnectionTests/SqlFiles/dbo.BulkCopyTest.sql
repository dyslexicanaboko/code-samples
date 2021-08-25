CREATE TABLE dbo.BulkCopyTest
(
	BulkCopyTestId INT IDENTITY(1,1) NOT NULL,
	CreatedOnUtc DATETIME2(7) NOT NULL,
	CONSTRAINT [PK_dbo.BulkCopyTest_BulkCopyTestId] PRIMARY KEY ([BulkCopyTestId])
)

SELECT * FROM dbo.BulkCopyTest
