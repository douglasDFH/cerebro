-- ===============================================================================
-- SCRIPT DE PRUEBA Y CORRECCIÓN DEL LOGIN
-- ===============================================================================

USE Bike_Store
GO

-- 1. Verificar estructura de la tabla users
PRINT '=== ESTRUCTURA ACTUAL DE LA TABLA USERS ==='
SELECT 
    COLUMN_NAME, 
    DATA_TYPE, 
    CHARACTER_MAXIMUM_LENGTH, 
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION
GO

-- 2. Verificar si existe el usuario admin
PRINT '=== VERIFICANDO USUARIO ADMIN ==='
IF EXISTS (SELECT * FROM users WHERE usuario_name = 'admin')
    PRINT 'El usuario admin existe'
ELSE
    PRINT 'El usuario admin NO existe'
GO

-- 3. Mostrar todos los usuarios existentes
PRINT '=== USUARIOS EXISTENTES ==='
SELECT * FROM users
GO

-- 4. Probar el procedimiento almacenado de login
PRINT '=== PROBANDO PROCEDIMIENTO DE LOGIN ==='
EXEC splogin @usuario = 'admin', @clave = '123456'
GO

-- 5. Si el usuario admin no existe, crearlo
IF NOT EXISTS (SELECT * FROM users WHERE usuario_name = 'admin')
BEGIN
    PRINT 'Creando usuario admin...'
    INSERT INTO users (usuario_name, usuario_clave, usuario_email)
    VALUES ('admin', '123456', 'admin@sistema.com')
    PRINT 'Usuario admin creado exitosamente'
END
GO

-- 6. Verificar nuevamente el login
PRINT '=== VERIFICACIÓN FINAL DEL LOGIN ==='
EXEC splogin @usuario = 'admin', @clave = '123456'
GO

PRINT '=== PRUEBA COMPLETADA ==='
PRINT 'Si ves datos arriba, el login debería funcionar correctamente'
PRINT 'Usuario: admin'
PRINT 'Contraseña: 123456'