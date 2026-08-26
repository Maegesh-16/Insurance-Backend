IF DB_ID(N'premium_db') IS NULL
BEGIN
    CREATE DATABASE premium_db;
END;
GO

IF DB_ID(N'payment_db') IS NULL
BEGIN
    CREATE DATABASE payment_db;
END;
GO

IF DB_ID(N'notification_db') IS NULL
BEGIN
    CREATE DATABASE notification_db;
END;
GO
