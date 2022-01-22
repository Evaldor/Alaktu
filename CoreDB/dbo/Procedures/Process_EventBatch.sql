CREATE PROCEDURE [dbo].[Process_EventBatch]
	@PiplineRegisterId BIGINT
	,@LastRowNumber BIGINT
AS
BEGIN 

	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb..#EventBatch') IS NULL
		CREATE TABLE #EventBatch  
		([eventBody] NVARCHAR (MAX) NULL
		);


	BEGIN TRY

		BEGIN TRAN

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

			UPDATE  [dbo].[PiplineRegister]
				SET [LastProcessedAt] = SYSDATETIME()
					,[LastProcessedStatus] = 'SUCCESS'
					,[CurrentRow] = @LastRowNumber
			WHERE [Id] = @PiplineRegisterId

		COMMIT TRAN

	END TRY
	BEGIN CATCH

		ROLLBACK TRAN

		UPDATE  [dbo].[PiplineRegister]
			SET [LastProcessedAt] = SYSDATETIME()
				,[LastProcessedStatus] = 'FAILED'
		WHERE [Id] = @PiplineRegisterId

	END CATCH

END
