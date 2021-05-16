-- Use localhost
USE ScratchSpace
GO

--Start from scratch
/* 
DROP TABLE dbo.DataThatChangesFrequently
DROP TABLE dbo.CollectionTracker
DROP TABLE dbo.ICollectNumbersForFun
*/
GO
/* https://docs.microsoft.com/en-us/sql/t-sql/data-types/rowversion-transact-sql?view=sql-server-ver15
   Don't use timestamp it's deprecated, use rowversion which is its successor and synonym
   A nonnullable rowversion column is semantically equivalent to a binary(8) column. 
   A nullable rowversion column is semantically equivalent to a varbinary(8) column. */

--TRUNCATE TABLE dbo.DataThatChangesFrequently
CREATE TABLE dbo.DataThatChangesFrequently
(
	DataThatChangesFrequentlyId INT IDENTITY(1,1) NOT NULL,
	SomeNumber INT NOT NULL,
	[Version] ROWVERSION NOT NULL,
	CONSTRAINT [PK_dbo.DataThatChangesFrequently_DataThatChangesFrequentlyId] PRIMARY KEY(DataThatChangesFrequentlyId)
)

GO

DECLARE @i INT = 0;
DECLARE @count INT = 10;

WHILE @i < @count
BEGIN
	INSERT INTO dbo.DataThatChangesFrequently
	(
		SomeNumber
	)
	VALUES
	(RAND()*100000)
	
	SET @i += 1;
END

SELECT * FROM dbo.DataThatChangesFrequently

-- There is a method to the madness
SELECT MIN(Version), MAX(Version) FROM dbo.DataThatChangesFrequently

GO

--TRUNCATE TABLE dbo.CollectionTracker
CREATE TABLE dbo.CollectionTracker
(
	CollectionTrackerId INT IDENTITY(1,1) NOT NULL,
	[Key] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(255) NULL,
	LastVersion CHAR(16) NOT NULL, -- Storing as char for the sake of portability
	CollectedOnUtc DATETIME2(0) NOT NULL,
	CONSTRAINT [PK_dbo.CollectionTracker_CollectionTrackerId] PRIMARY KEY (CollectionTrackerId)
)

-- Initializtion is a good first step to not bother with zero rows - zero rows will never matter after the first time so why bother now
-- Make this more sophisticated by adding in what it's tracking and don't track the same thing more than once
INSERT INTO dbo.CollectionTracker
(
    [Key]
   ,LastVersion
   ,CollectedOnUtc
)
VALUES
(   
    'DataThatChangesFrequently'
   ,'0000000000000000' -- Smallest version for now, don't store the 0x it will mess up the conversion
   ,GETUTCDATE() -- CollectedOnUtc - datetime2(0)
)

GO

--TRUNCATE TABLE dbo.ICollectNumbersForFun
CREATE TABLE dbo.ICollectNumbersForFun
(
	ICollectNumbersForFunId INT IDENTITY(1,1) NOT NULL,
	OooANumber INT NOT NULL,
	CONSTRAINT [PK_dbo.ICollectNumbersForFun_ICollectNumbersForFunId] PRIMARY KEY (ICollectNumbersForFunId)
)

GO

-- Ok Let's collect some numbers! First time synchronization usually means let's get everything
DECLARE @lastVersion binary(8);
DECLARE @maxVersion binary(8);

SELECT
	@lastVersion = CONVERT(binary(8), LastVersion, 2)
FROM dbo.CollectionTracker
WHERE [Key] = 'DataThatChangesFrequently'

SELECT
	@maxVersion = MAX([Version])
FROM dbo.DataThatChangesFrequently
WHERE [Version] > @lastVersion

SELECT 
	 @lastVersion AS LastVersion
	,@maxVersion AS MaxVersion

-- This is for demonstration purposes only - you would really want to do a manual upsert or use the merge construct
INSERT INTO dbo.ICollectNumbersForFun
(
    OooANumber
)
SELECT 
	SomeNumber 
FROM dbo.DataThatChangesFrequently
WHERE [Version] > @lastVersion

-- Mark down what the latest version was
UPDATE dbo.CollectionTracker SET
	CollectedOnUtc = GETUTCDATE(),
	LastVersion = CONVERT(CHAR(16), @maxVersion, 2)
WHERE [Key] = 'DataThatChangesFrequently'

SELECT * FROM dbo.CollectionTracker ct
SELECT * FROM dbo.ICollectNumbersForFun

GO

SELECT * FROM dbo.DataThatChangesFrequently dtcf

DECLARE @i INT = 1;
DECLARE @count INT = 4;

WHILE @i <= @count
BEGIN
	UPDATE dbo.DataThatChangesFrequently SET
		SomeNumber = RAND()*100000
	WHERE DataThatChangesFrequentlyId = @i
		
	SET @i += 1;
END

SELECT * FROM dbo.CollectionTracker ct
SELECT * FROM dbo.DataThatChangesFrequently dtcf
SELECT * FROM dbo.ICollectNumbersForFun

GO

-- No way to reprocess unless there is a change
SELECT 
	* 
FROM dbo.DataThatChangesFrequently
WHERE [Version] > 0x000000000005862F