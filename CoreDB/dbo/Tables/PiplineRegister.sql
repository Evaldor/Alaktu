CREATE TABLE [dbo].[PiplineRegister] (
    [Id]          BIGINT IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NULL,
    [IsEnabled]   BIT            NULL,
    [ProcessedAt] DATETIME2 (7)  NULL,
    [ActualRowVersion] VARBINARY(8) NULL,
    [PrimaryKeyIsUsed] BIT NULL,
    [PrimaryKeySchemeJson] NVARCHAR (MAX) NULL,
    [TransferToFlatTableIsEnabled] BIT NULL,
    [TransferToFlatTableSchemeJson] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_PiplineRegister] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_PiplineRegister] UNIQUE([Name])   
);

