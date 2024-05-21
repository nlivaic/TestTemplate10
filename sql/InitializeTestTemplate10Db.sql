USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TestTemplate10Db')
BEGIN
  CREATE DATABASE TestTemplate10Db;
END;
GO

USE TestTemplate10Db;
GO

IF NOT EXISTS (SELECT 1
                 FROM sys.server_principals
                WHERE [name] = N'TestTemplate10Db_Login' 
                  AND [type] IN ('C','E', 'G', 'K', 'S', 'U'))
BEGIN
    CREATE LOGIN TestTemplate10Db_Login
        WITH PASSWORD = '<DB_PASSWORD>';
END;
GO  

IF NOT EXISTS (select * from sys.database_principals where name = 'TestTemplate10Db_User')
BEGIN
    CREATE USER TestTemplate10Db_User FOR LOGIN TestTemplate10Db_Login;
END;
GO  


EXEC sp_addrolemember N'db_datareader', N'TestTemplate10Db_User';
GO

EXEC sp_addrolemember N'db_datawriter', N'TestTemplate10Db_User';
GO

EXEC sp_addrolemember N'db_ddladmin', N'TestTemplate10Db_User';
GO
