--data seeding dbo.SourceType
MERGE [dbo].[SourceType] AS target
USING (VALUES 
            (1,'CSV')
            ,(2,'Excel')
    ) AS source ([Id],[Name])
ON target.[Id] = source.[Id]
WHEN MATCHED THEN
    UPDATE 
        SET target.[Name] = source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT  ([Id]
            ,[Name])
    VALUES  (source.[Id]
            ,Source.[Name]
            )
;