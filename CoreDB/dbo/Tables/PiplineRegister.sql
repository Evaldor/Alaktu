CREATE TABLE [dbo].[PiplineRegister] (
    [Id] BIGINT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NULL,
    [SourceTypeId] INT NULL,
    [SourseConnection] nvarchar(max),
    [SourseCredentials] nvarchar(max),
    [IsEnabled] BIT NULL,
    [LastProcessedAt] DATETIME2 (7)  NULL,
    [ActualRowVersion] VARBINARY(8) NULL,
    [PrimaryKeyIsUsed] BIT NULL,
    [PrimaryKeySchemeJson] NVARCHAR (MAX) NULL,
    [VersioningIsUsed] BIT NULL,
    [VersioningSchemeJson] NVARCHAR (MAX) NULL,
    [TransferToFlatTableIsEnabled] BIT NULL,
    [TransferToFlatTableSchemeJson] NVARCHAR (MAX) NULL,
    [LastProcessedStatus] NVARCHAR(MAX),
    CONSTRAINT [PK_PiplineRegister] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_PiplineRegister] UNIQUE([Name]),   
    CONSTRAINT [FK_SourceType] FOREIGN KEY ([SourceTypeId]) REFERENCES [dbo].[SourceType] ([Id])
);

