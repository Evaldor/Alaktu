
IF NOT EXISTS(SELECT name FROM sys.server_principals WHERE name = 'KPappUser')
BEGIN

	CREATE LOGIN KPappUser WITH PASSWORD = 'ShareWare724', DEFAULT_DATABASE = [AlaktuCoreDB]

END

IF NOT EXISTS(SELECT name FROM sys.database_principals WHERE name = 'KPappUser')
BEGIN

	CREATE USER  KPappUser FOR LOGIN  KPappUser WITH DEFAULT_SCHEMA = [db_datawriter,  db_datareader]
	EXEC sp_addrolemember db_datawriter, KPappUser
	EXEC sp_addrolemember db_datareader, KPappUser

END
