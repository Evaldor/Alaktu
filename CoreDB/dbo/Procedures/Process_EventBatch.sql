CREATE PROCEDURE [dbo].[Process_EventBatch]
	@PiplineRegisterId BIGINT
AS
BEGIN 

	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb..#EventBatch') IS NULL
		CREATE TABLE #EventBatch  
		([eventBody] NVARCHAR (MAX) NULL
		);

	INSERT INTO [dbo].[EventBuffer] 
		([CreatedAt]
		,[PiplineRegisterId]
		,[EventBody]
		)
	SELECT 
		SYSDATETIME()
		,@PiplineRegisterId
		,eventBody
	FROM #EventBatch

END
