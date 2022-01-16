CREATE TABLE [dbo].[EventBuffer] (
    [Id]            BIGINT IDENTITY(1,1) NOT NULL,
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [Partition]     INT             NULL,
    [Offeset]       BIGINT         NULL,
    [SourceName]    NVARCHAR (500) NULL,
    [RowVersion]    ROWVERSION NOT NULL,
    [EventJson]     NVARCHAR (MAX) NULL,  
    CONSTRAINT [PK_EventBuffer] PRIMARY KEY CLUSTERED ([Id] DESC, [CreatedAt] DESC) ON [Ps_EventBuffer] ([CreatedAt])
);

