CREATE PROC ResetID 
AS

DECLARE @max_seed int = ISNULL((SELECT MAX(ID) FROM [Table]),0)

DBCC CHECKIDENT("Table", RESEED, @max_seed)