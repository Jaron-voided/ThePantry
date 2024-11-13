-- Check if the database exists, and only create it if it doesn't
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'the_pantry')
    BEGIN
        CREATE DATABASE the_pantry;
    END
GO

-- Switch to the new database context
USE the_pantry;
GO

-- Check if the login exists, and only create it if it doesn't
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'izaya')
    BEGIN
        CREATE LOGIN izaya WITH PASSWORD = 'N0t0ri0us!';
    END
GO

-- Check if the user exists in the database, and only create it if it doesn't
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'izaya')
    BEGIN
        CREATE USER izaya FOR LOGIN izaya;
    END
GO

-- Grant necessary permissions to the new user
IF NOT EXISTS (SELECT * FROM sys.database_role_members WHERE member_principal_id = DATABASE_PRINCIPAL_ID('izaya') AND role_principal_id = DATABASE_PRINCIPAL_ID('db_owner'))
    BEGIN
        ALTER ROLE db_owner ADD MEMBER izaya;
    END
GO
