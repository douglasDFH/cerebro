-- ===============================================================================
-- SCRIPT PARA CORREGIR ERROR DE FOREIGN KEY EN ORDERS
-- Este script verifica y corrige los problemas de usuario_id en las órdenes
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== VERIFICANDO USUARIOS EXISTENTES ==='
SELECT usuario_id, usuario_name, usuario_email FROM users ORDER BY usuario_id

PRINT ''
PRINT '=== VERIFICANDO SI EXISTE USUARIO CON ID = 1 ==='
IF EXISTS (SELECT * FROM users WHERE usuario_id = 1)
BEGIN
    PRINT 'El usuario con ID = 1 existe:'
    SELECT usuario_id, usuario_name, usuario_email FROM users WHERE usuario_id = 1
END
ELSE
BEGIN
    PRINT 'ERROR: No existe usuario con ID = 1'
    PRINT 'El usuario con ID = 1 es necesario como usuario por defecto'
    
    -- Si no existe, verificar cuál es el usuario con menor ID
    DECLARE @min_user_id INT = (SELECT MIN(usuario_id) FROM users)
    PRINT 'Usuario con menor ID disponible: ' + CAST(@min_user_id AS VARCHAR(10))
    
    -- Mostrar información del primer usuario disponible
    SELECT TOP 1 usuario_id, usuario_name, usuario_email 
    FROM users 
    ORDER BY usuario_id
END

PRINT ''
PRINT '=== VERIFICANDO ÓRDENES CON PROBLEMAS ==='
-- Verificar si hay órdenes con usuario_id que no existe
SELECT o.order_id, o.usuario_id, 'PROBLEMA: usuario_id no existe' as Estado
FROM orders o 
LEFT JOIN users u ON o.usuario_id = u.usuario_id
WHERE u.usuario_id IS NULL

PRINT ''
PRINT '=== SOLUCIÓN RECOMENDADA ==='
PRINT ''

-- Si el usuario admin (ID=1) no existe, sugerir crearlo
IF NOT EXISTS (SELECT * FROM users WHERE usuario_id = 1)
BEGIN
    PRINT '1. Crear usuario con ID = 1 (recomendado):'
    PRINT '   INSERT INTO users (usuario_name, usuario_clave, usuario_email)'
    PRINT '   VALUES (''admin'', ''123456'', ''admin@sistema.com'')'
    PRINT ''
    
    -- O sugerir actualizar el código para usar el primer usuario disponible
    DECLARE @first_user_id INT = (SELECT MIN(usuario_id) FROM users)
    IF @first_user_id IS NOT NULL
    BEGIN
        PRINT '2. Alternativa - Cambiar el código para usar usuario ID = ' + CAST(@first_user_id AS VARCHAR(10))
        PRINT '   En FrmOrders.cs, cambiar la línea:'
        PRINT '   public string Usuario_id = "' + CAST(@first_user_id AS VARCHAR(10)) + '";'
    END
END
ELSE
BEGIN
    PRINT 'El usuario con ID = 1 existe. El problema puede estar en:'
    PRINT '1. El formulario no está pasando correctamente el usuario_id'
    PRINT '2. Verificar que FrmPrincipal.Iduser tenga un valor válido'
    PRINT '3. Verificar que se ejecute: frmOrders.Usuario_id = this.Iduser'
END

PRINT ''
PRINT '=== APLICANDO CORRECCIÓN AUTOMÁTICA ==='

-- Crear usuario admin con ID = 1 si no existe
IF NOT EXISTS (SELECT * FROM users WHERE usuario_id = 1)
BEGIN
    -- Si hay usuarios, crear uno nuevo con ID específico
    IF EXISTS (SELECT * FROM users)
    BEGIN
        PRINT 'Intentando crear usuario admin con ID = 1...'
        
        -- Habilitar INSERT con IDENTITY_INSERT
        SET IDENTITY_INSERT users ON
        
        INSERT INTO users (usuario_id, usuario_name, usuario_clave, usuario_email)
        VALUES (1, 'admin', '123456', 'admin@sistema.com')
        
        -- Deshabilitar IDENTITY_INSERT
        SET IDENTITY_INSERT users OFF
        
        PRINT 'Usuario admin creado exitosamente con ID = 1'
    END
    ELSE
    BEGIN
        -- Si no hay usuarios, insertar el primero
        INSERT INTO users (usuario_name, usuario_clave, usuario_email)
        VALUES ('admin', '123456', 'admin@sistema.com')
        PRINT 'Usuario admin creado como primer usuario'
    END
END

-- Corregir órdenes con usuario_id inválido
IF EXISTS (SELECT o.order_id FROM orders o LEFT JOIN users u ON o.usuario_id = u.usuario_id WHERE u.usuario_id IS NULL)
BEGIN
    PRINT 'Corrigiendo órdenes con usuario_id inválido...'
    
    DECLARE @valid_user_id INT = (SELECT MIN(usuario_id) FROM users)
    
    UPDATE orders 
    SET usuario_id = @valid_user_id
    WHERE usuario_id NOT IN (SELECT usuario_id FROM users)
    
    PRINT 'Órdenes corregidas para usar usuario_id = ' + CAST(@valid_user_id AS VARCHAR(10))
END

PRINT ''
PRINT '=== VERIFICACIÓN FINAL ==='
PRINT 'Usuarios disponibles:'
SELECT usuario_id, usuario_name, usuario_email FROM users ORDER BY usuario_id

PRINT ''
PRINT 'Estado de órdenes:'
SELECT 
    COUNT(*) as TotalOrdenes,
    COUNT(CASE WHEN u.usuario_id IS NOT NULL THEN 1 END) as OrdenesValidas,
    COUNT(CASE WHEN u.usuario_id IS NULL THEN 1 END) as OrdenesConProblemas
FROM orders o 
LEFT JOIN users u ON o.usuario_id = u.usuario_id

PRINT ''
PRINT '=== CORRECCIÓN COMPLETADA ==='
PRINT 'Ahora deberías poder crear órdenes sin problemas de FK.'
PRINT 'Usuario por defecto: ID = 1 (admin/123456)'