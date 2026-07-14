-- ===============================================================================
-- SCRIPT PARA ARREGLAR RESTRICCIONES DEL DATASET
-- Este script limpia datos problemáticos que causan errores en el reporte
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== ARREGLANDO RESTRICCIONES DEL DATASET ==='
PRINT ''

-- 1. Limpiar order_items huérfanos (sin producto válido)
PRINT '--- 1. ELIMINANDO ORDER_ITEMS CON PRODUCTOS INVÁLIDOS ---'

DELETE FROM order_items 
WHERE product_id NOT IN (SELECT product_id FROM products)
PRINT 'Items con productos inválidos eliminados: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- 2. Limpiar order_items huérfanos (sin orden válida)
PRINT '--- 2. ELIMINANDO ORDER_ITEMS CON ÓRDENES INVÁLIDAS ---'

DELETE FROM order_items 
WHERE order_id NOT IN (SELECT order_id FROM orders)
PRINT 'Items con órdenes inválidas eliminados: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- 3. Actualizar precios NULL o inválidos
PRINT '--- 3. CORRIGIENDO PRECIOS NULL O INVÁLIDOS ---'

UPDATE oi 
SET oi.price = p.price
FROM order_items oi
INNER JOIN products p ON oi.product_id = p.product_id
WHERE oi.price IS NULL OR oi.price <= 0
PRINT 'Precios corregidos: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- 4. Corregir cantidades NULL o inválidas
PRINT '--- 4. CORRIGIENDO CANTIDADES NULL O INVÁLIDAS ---'

UPDATE order_items 
SET quantity = 1
WHERE quantity IS NULL OR quantity <= 0
PRINT 'Cantidades corregidas: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- 5. Corregir descuentos NULL
PRINT '--- 5. CORRIGIENDO DESCUENTOS NULL ---'

UPDATE order_items 
SET discount = 0.0
WHERE discount IS NULL
PRINT 'Descuentos corregidos: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- 6. Verificar y corregir datos de clientes
PRINT '--- 6. CORRIGIENDO DATOS DE CLIENTES NULL ---'

UPDATE customers 
SET first_name = 'Cliente'
WHERE first_name IS NULL OR first_name = ''

UPDATE customers 
SET last_name = 'Sin Nombre'
WHERE last_name IS NULL OR last_name = ''

UPDATE customers 
SET street = 'Dirección no especificada'
WHERE street IS NULL OR street = ''

UPDATE customers 
SET city = 'Ciudad no especificada'
WHERE city IS NULL OR city = ''

UPDATE customers 
SET state = 'Estado no especificado'
WHERE state IS NULL OR state = ''

UPDATE customers 
SET phone = '00000000'
WHERE phone IS NULL OR phone = ''

PRINT 'Datos de clientes corregidos'

-- 7. Verificar usuarios
PRINT '--- 7. VERIFICANDO USUARIOS ---'

-- Si hay órdenes con usuario_id inválido, asignar al usuario 1
UPDATE orders 
SET usuario_id = 1
WHERE usuario_id NOT IN (SELECT usuario_id FROM users)
PRINT 'Órdenes con usuarios inválidos corregidas: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

PRINT ''
PRINT '=== VERIFICACIÓN POST-CORRECCIÓN ==='

-- Probar el procedimiento con las primeras 3 órdenes
DECLARE @test_order INT = 1

WHILE @test_order <= 3
BEGIN
    PRINT 'Probando orden: ' + CAST(@test_order AS VARCHAR(10))
    
    BEGIN TRY
        -- Solo verificar que la consulta funcione, no ejecutar el procedimiento completo
        IF EXISTS (SELECT * FROM orders WHERE order_id = @test_order)
        BEGIN
            DECLARE @count INT
            SELECT @count = COUNT(*)
            FROM orders o
            INNER JOIN customers c ON o.customer_id = c.customer_id
            INNER JOIN users u ON o.usuario_id = u.usuario_id
            INNER JOIN order_items oi ON o.order_id = oi.order_id
            INNER JOIN products p ON oi.product_id = p.product_id
            WHERE o.order_id = @test_order
            
            PRINT '✓ Orden ' + CAST(@test_order AS VARCHAR(10)) + ' - OK (' + CAST(@count AS VARCHAR(10)) + ' items)'
        END
        ELSE
        BEGIN
            PRINT '- Orden ' + CAST(@test_order AS VARCHAR(10)) + ' no existe'
        END
    END TRY
    BEGIN CATCH
        PRINT '✗ Orden ' + CAST(@test_order AS VARCHAR(10)) + ' - ERROR: ' + ERROR_MESSAGE()
    END CATCH
    
    SET @test_order = @test_order + 1
END

PRINT ''
PRINT '=== ESTADÍSTICAS FINALES ==='

SELECT 
    'Total órdenes' AS descripcion,
    COUNT(*) AS cantidad
FROM orders
UNION ALL
SELECT 
    'Total order_items',
    COUNT(*)
FROM order_items
UNION ALL
SELECT 
    'Órdenes con 3+ items',
    COUNT(*)
FROM (
    SELECT order_id
    FROM order_items
    GROUP BY order_id
    HAVING COUNT(*) >= 3
) AS subquery

PRINT ''
PRINT '=== CORRECCIÓN COMPLETADA ==='
PRINT 'Ahora intenta generar la factura nuevamente.'
PRINT 'Si persiste el error, regenera el DataSet en Visual Studio:'
PRINT '1. Click derecho en dsPrincipal.xsd'
PRINT '2. Configurar DataSet con asistente'
PRINT '3. Volver a probar'