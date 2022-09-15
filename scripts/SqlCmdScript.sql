IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PJ-Example-MS')
  CREATE database [PJ-Example-MS];
GO

-- Create the default dB user
USE [master]
GO

IF NOT EXISTS(SELECT 1 FROM [PJ-Example-MS].sys.database_principals WHERE name = 'pjexample')
  CREATE LOGIN [pjexample] WITH PASSWORD=N'!Q@W3e4r', DEFAULT_DATABASE=[PJ-Example-MS], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [PJ-Example-MS]
GO

IF NOT EXISTS(SELECT 1 FROM [PJ-Example-MS].sys.database_principals WHERE name = 'pjexample')
BEGIN
  CREATE USER [pjexample] FOR LOGIN [pjexample]
  ALTER USER [pjexample] WITH DEFAULT_SCHEMA=[dbo]
  ALTER ROLE [db_owner] ADD MEMBER [pjexample]
END
GO