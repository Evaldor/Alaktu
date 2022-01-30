CREATE PROCEDURE [dbo].[Process_EventBatch]
    @PiplineId BIGINT
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
                ,[PiplineId]
                ,[EventBody]
                )
            SELECT 
                SYSDATETIME()
                ,@PiplineId
                ,eventBody
            FROM #EventBatch

            UPDATE  [dbo].[Pipline]
                SET [LastProcessedAt] = SYSDATETIME()
                    ,[LastProcessedStatus] = 'SUCCESS'
                    ,[CurrentRow] = @LastRowNumber
            WHERE [Id] = @PiplineId

        COMMIT TRAN

    END TRY
    BEGIN CATCH

        ROLLBACK TRAN

        UPDATE  [dbo].[Pipline]
            SET [LastProcessedAt] = SYSDATETIME()
                ,[LastProcessedStatus] = 'FAILED'
        WHERE [Id] = @PiplineId

    END CATCH

END
