CREATE TABLE [dbo].[EventBuffer] (
    [Id]            BIGINT IDENTITY(1,1) NOT NULL,
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [PiplineId]    BIGINT NULL,
    [RowVersion]    ROWVERSION NOT NULL,
    [EventBody]     NVARCHAR (MAX) NULL,  
    CONSTRAINT [PK_EventBuffer] PRIMARY KEY CLUSTERED ([Id] DESC, [CreatedAt] DESC) ON [Ps_EventBuffer] ([CreatedAt])
);

