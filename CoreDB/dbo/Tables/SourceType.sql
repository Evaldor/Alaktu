CREATE TABLE [dbo].[SourceType]
(
	[Id] BIGINT NOT NULL,
	[Name] nvarchar(500) NOT NULL,
	CONSTRAINT [PK_SourceType] PRIMARY KEY CLUSTERED ([Id] ASC)
)
