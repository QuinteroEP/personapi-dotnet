-- Script para crear la base de datos persona_db y la tabla Users
-- Este script se ejecutará dentro del contenedor SQL Server

USE master;
GO

-- Crear base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'persona_db')
BEGIN
    CREATE DATABASE persona_db;
    PRINT 'Base de datos persona_db creada exitosamente';
END
ELSE
BEGIN
    PRINT 'La base de datos persona_db ya existe';
END
GO

USE persona_db;
GO

-- Crear tabla Users si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
    PRINT 'Tabla Users creada exitosamente';
END
ELSE
BEGIN
    PRINT 'La tabla Users ya existe';
END
GO

-- Insertar usuario demo (usuario: admin, contraseña: admin123)
-- PasswordHash generado con SHA256 de "admin123"
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    INSERT INTO Users (Username, PasswordHash, CreatedAt)
    VALUES ('admin', 'IsFU7t13NxgI+dTLEXy+p0mEX2Y1/Y0eIh9qX2VvL2o=', GETUTCDATE());
    PRINT 'Usuario demo creado: admin / admin123';
END
ELSE
BEGIN
    PRINT 'El usuario admin ya existe';
END
GO

-- Verificar datos insertados
SELECT * FROM Users;
GO
