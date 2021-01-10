CREATE PARTITION FUNCTION [Pf_EventBuffer](DATETIME2 (7))
    AS RANGE RIGHT
    FOR VALUES (
			convert(NVARCHAR(36), dateadd(month, 0, sysutcdatetime()),112),
			convert(NVARCHAR(36), dateadd(month, 1, sysutcdatetime()),112)
			);

