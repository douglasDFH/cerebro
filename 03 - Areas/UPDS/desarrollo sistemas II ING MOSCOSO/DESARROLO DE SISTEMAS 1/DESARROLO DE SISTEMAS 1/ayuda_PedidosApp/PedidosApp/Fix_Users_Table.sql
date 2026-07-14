-- ===============================================================================
-- SCRIPT DE CORRECCIÓN PARA LA TABLA USERS
-- Este script corrige el problema de la columna de nombre de usuario
-- ===============================================================================

USE Bike_Store
GO

-- Verificar qué columnas tiene la tabla users actualmente
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION
GO

-- Opción 1: Si la columna se llama 'name' en lugar de 'usuario_name'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'name')
   AND NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_name')
BEGIN
    PRINT 'Renombrando columna "name" a "usuario_name"...'
    EXEC sp_rename 'users.name', 'usuario_name', 'COLUMN'
    PRINT 'Columna renombrada exitosamente.'
END

-- Opción 2: Si no existe ninguna columna para el nombre de usuario
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_name')
   AND NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'name')
BEGIN
    PRINT 'Agregando columna usuario_name...'
    ALTER TABLE users ADD usuario_name VARCHAR(50) NOT NULL DEFAULT ''
    PRINT 'Columna agregada exitosamente.'
END

-- Verificar si existe la columna usuario_clave
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_clave')
BEGIN
    PRINT 'Agregando columna usuario_clave...'
    ALTER TABLE users ADD usuario_clave VARCHAR(250) NOT NULL DEFAULT ''
    PRINT 'Columna usuario_clave agregada.'
END

-- Verificar si existe la columna usuario_email
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_email')
BEGIN
    PRINT 'Agregando columna usuario_email...'
    ALTER TABLE users ADD usuario_email VARCHAR(100) NOT NULL DEFAULT ''
    PRINT 'Columna usuario_email agregada.'
END

-- Insertar usuario admin si no existe
IF NOT EXISTS (SELECT * FROM users WHERE usuario_name = 'admin')
BEGIN
    INSERT INTO users (usuario_name, usuario_clave, usuario_email)
    VALUES ('admin', '123456', 'admin@sistema.com')
    PRINT 'Usuario admin creado exitosamente.'
END
ELSE
BEGIN
    PRINT 'El usuario admin ya existe.'
END

-- Mostrar la estructura final de la tabla
PRINT 'Estructura actual de la tabla users:'
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION

-- Mostrar los usuarios existentes
PRINT 'Usuarios en la tabla:'
SELECT * FROM users
GO

PRINT 'Script de corrección completado.'