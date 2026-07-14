-- ===============================================================================
-- SCRIPT PARA DIAGNOSTICAR PROBLEMAS DE RESTRICCIONES EN EL DATASET
-- Este script verifica problemas comunes que causan errores en el reporte
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== DIAGNÓSTICO DE RESTRICCIONES DEL DATASET ==='
PRINT ''

-- 1. Verificar datos NULL en campos requeridos
PRINT '--- 1. VERIFICANDO DATOS NULL ---'

SELECT 'orders con usuario_id NULL' AS problema, COUNT(*) AS cantidad
FROM orders WHERE usuario_id IS NULL
UNION ALL
SELECT 'customers con first_name NULL', COUNT(*)
FROM customers WHERE first_name IS NULL
UNION ALL
SELECT 'customers con last_name NULL', COUNT(*)
FROM customers WHERE last_name IS NULL
UNION ALL
SELECT 'order_items con product_id NULL', COUNT(*)
FROM order_items WHERE product_id IS NULL
UNION ALL
SELECT 'order_items con quantity NULL', COUNT(*)
FROM order_items WHERE quantity IS NULL
UNION ALL
SELECT 'order_items con price NULL', COUNT(*)
FROM order_items WHERE price IS NULL

PRINT ''
PRINT '--- 2. VERIFICANDO FOREIGN KEYS INVÁLIDAS ---'

-- Verificar órdenes con customer_id inválido
SELECT o.order_id, o.customer_id, 'customer_id no existe' AS problema
FROM orders o
LEFT JOIN customers c ON o.customer_id = c.customer_id
WHERE c.customer_id IS NULL

-- Verificar órdenes con usuario_id inválido
SELECT o.order_id, o.usuario_id, 'usuario_id no existe' AS problema
FROM orders o
LEFT JOIN users u ON o.usuario_id = u.usuario_id
WHERE u.usuario_id IS NULL

-- Verificar order_items con order_id inválido
SELECT oi.order_item_id, oi.order_id, 'order_id no existe' AS problema
FROM order_items oi
LEFT JOIN orders o ON oi.order_id = o.order_id
WHERE o.order_id IS NULL

-- Verificar order_items con product_id inválido
SELECT oi.order_item_id, oi.product_id, 'product_id no existe' AS problema
FROM order_items oi
LEFT JOIN products p ON oi.product_id = p.product_id
WHERE p.product_id IS NULL

PRINT ''
PRINT '--- 3. PROBANDO EL PROCEDIMIENTO spreporte_factura ---'

-- Probar con diferentes órdenes
DECLARE @test_orders TABLE (order_id INT)
INSERT INTO @test_orders SELECT TOP 5 order_id FROM orders ORDER BY order_id

DECLARE @current_order INT
DECLARE order_cursor CURSOR FOR SELECT order_id FROM @test_orders

OPEN order_cursor
FETCH NEXT FROM order_cursor INTO @current_order

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Probando orden: ' + CAST(@current_order AS VARCHAR(10))
    
    BEGIN TRY
        -- Ejecutar el procedimiento
        EXEC spreporte_factura @order_id = @current_order
        PRINT '✓ Orden ' + CAST(@current_order AS VARCHAR(10)) + ' - OK'
    END TRY
    BEGIN CATCH
        PRINT '✗ Orden ' + CAST(@current_order AS VARCHAR(10)) + ' - ERROR: ' + ERROR_MESSAGE()
    END CATCH
    
    FETCH NEXT FROM order_cursor INTO @current_order
END

CLOSE order_cursor
DEALLOCATE order_cursor

PRINT ''
PRINT '--- 4. VERIFICANDO ESTRUCTURA DE DATOS DEL REPORTE ---'

-- Verificar que todos los campos requeridos estén presentes
SELECT TOP 1
    o.order_id,
    u.usuario_name as users,
    CONCAT(c.first_name, ' ', c.last_name) as Cliente,
    c.street,
    c.phone,
    c.state,
    c.city,
    o.order_date,
    p.product_name,
    p.model_year,
    oi.quantity,
    oi.price,
    oi.discount,
    (oi.quantity * oi.price * (1 - oi.discount)) as Subtotal,
    -- Verificar que todos los campos calculados funcionen
    '1025874639-2' as nit_organizacion,
    CONCAT('FAC-', FORMAT(o.order_id, '000000')) as factura_numero,
    'AUT-2024-001234567890' as codigo_autorizacion,
    CASE 
        WHEN c.phone IS NOT NULL AND LEN(c.phone) >= 7 
        THEN CONCAT('CI-', RIGHT('0000000' + c.phone, 7))
        ELSE CONCAT('CI-', FORMAT(c.customer_id, '0000000'))
    END as nit_ci_cliente
FROM orders o
INNER JOIN customers c ON o.customer_id = c.customer_id
INNER JOIN users u ON o.usuario_id = u.usuario_id
INNER JOIN order_items oi ON o.order_id = oi.order_id
INNER JOIN products p ON oi.product_id = p.product_id
WHERE o.order_id = 1

PRINT ''
PRINT '--- 5. VERIFICANDO INTEGRIDAD DE DATOS AGREGADOS ---'

-- Verificar los nuevos datos agregados
SELECT 
    'Órdenes con más de 3 items' AS descripcion,
    COUNT(*) AS cantidad
FROM (
    SELECT o.order_id
    FROM orders o
    INNER JOIN order_items oi ON o.order_id = oi.order_id
    GROUP BY o.order_id
    HAVING COUNT(oi.order_item_id) > 3
) AS subquery

-- Verificar productos usados en los nuevos items
SELECT DISTINCT
    p.product_id,
    p.product_name,
    p.price,
    COUNT(oi.order_item_id) AS veces_usado
FROM products p
INNER JOIN order_items oi ON p.product_id = oi.product_id
GROUP BY p.product_id, p.product_name, p.price
ORDER BY p.product_id

PRINT ''
PRINT '=== RECOMENDACIONES ==='
PRINT 'Si hay problemas:'
PRINT '1. Ejecutar: DELETE FROM order_items WHERE product_id NOT IN (SELECT product_id FROM products)'
PRINT '2. Ejecutar: DELETE FROM order_items WHERE order_id NOT IN (SELECT order_id FROM orders)'
PRINT '3. Verificar que todos los usuarios existan: SELECT * FROM users'
PRINT '4. Regenerar el DataSet en Visual Studio'
PRINT ''
PRINT '=== DIAGNÓSTICO COMPLETADO ==='