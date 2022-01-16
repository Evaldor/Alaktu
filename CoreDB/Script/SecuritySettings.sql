
IF NOT EXISTS(SELECT name FROM sys.server_principals WHERE name = 'AlaktuAppUser')
BEGIN

	CREATE LOGIN AlaktuAppUser WITH PASSWORD = 'ShareWare724', DEFAULT_DATABASE = [AlaktuCoreDB]

END

IF NOT EXISTS(SELECT name FROM sys.database_principals WHERE name = 'AlaktuAppUser')
BEGIN

	CREATE USER  KPappUser FOR LOGIN  KPappUser WITH DEFAULT_SCHEMA = [db_datawriter,  db_datareader]
	EXEC sp_addrolemember db_datawriter, AlaktuAppUser
	EXEC sp_addrolemember db_datareader, AlaktuAppUser

END
