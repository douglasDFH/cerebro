-- ===============================================================================
-- SCRIPT DE VERIFICACIÓN DE COLUMNAS DE BASE DE DATOS
-- Este script muestra todas las columnas de cada tabla para verificar la estructura
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== VERIFICACIÓN DE ESTRUCTURA DE TABLAS ==='
PRINT ''

-- Tabla categories
PRINT '--- TABLA: categories ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'categories'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla products
PRINT '--- TABLA: products ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'products'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla customers
PRINT '--- TABLA: customers ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'customers'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla users
PRINT '--- TABLA: users ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla orders
PRINT '--- TABLA: orders ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'orders'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla order_items
PRINT '--- TABLA: order_items ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'order_items'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla staffs
PRINT '--- TABLA: staffs ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'staffs'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla stores
PRINT '--- TABLA: stores ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'stores'
ORDER BY ORDINAL_POSITION
PRINT ''

PRINT '=== PRUEBAS DE PROCEDIMIENTOS ALMACENADOS ==='
PRINT ''

-- Probar procedimiento de customers
PRINT '--- RESULTADO DE spmostrar_customers ---'
EXEC spmostrar_customers
PRINT ''

-- Probar procedimiento de login
PRINT '--- RESULTADO DE splogin ---'
EXEC splogin @usuario = 'admin', @clave = '123456'
PRINT ''

PRINT '=== VERIFICACIÓN COMPLETADA ==='
PRINT 'Revisa los índices de columnas arriba para asegurar que tu código coincida'