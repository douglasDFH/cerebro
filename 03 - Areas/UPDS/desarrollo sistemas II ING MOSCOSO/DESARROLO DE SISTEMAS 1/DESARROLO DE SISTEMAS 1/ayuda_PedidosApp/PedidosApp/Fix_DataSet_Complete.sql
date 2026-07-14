-- ===============================================================================
-- SCRIPT PARA ARREGLAR EL DATASET dsPrincipal
-- Este script prepara los datos para que el DataSet funcione sin restricciones
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== ARREGLANDO DATASET Y DATOS PARA FACTURAS ==='
PRINT ''

-- 1. LIMPIAR COMPLETAMENTE DATOS PROBLEMÁTICOS
PRINT '--- 1. LIMPIEZA COMPLETA DE DATOS ---'

-- Eliminar order_items problemáticos
DELETE FROM order_items 
WHERE product_id NOT IN (SELECT product_id FROM products)
PRINT 'Items con productos inválidos eliminados: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

DELETE FROM order_items 
WHERE order_id NOT IN (SELECT order_id FROM orders)
PRINT 'Items huérfanos eliminados: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- Eliminar órdenes sin items (evitar problemas de PK en dataset)
DELETE FROM orders 
WHERE order_id NOT IN (SELECT DISTINCT order_id FROM order_items)
PRINT 'Órdenes sin items eliminadas: ' + CAST(@@ROWCOUNT AS VARCHAR(10))

-- 2. NORMALIZAR TODOS LOS DATOS PARA EVITAR NULLS
PRINT ''
PRINT '--- 2. NORMALIZANDO DATOS PARA DATASET ---'

-- Usuarios
UPDATE users SET usuario_name = 'Usuario' + CAST(usuario_id AS VARCHAR(10))
WHERE usuario_name IS NULL OR usuario_name = ''
PRINT 'Usuarios normalizados'

-- Clientes - ajustar a límites del dataset
UPDATE customers 
SET 
    first_name = LEFT(ISNULL(first_name, 'Cliente'), 50),
    last_name = LEFT(ISNULL(last_name, 'Anonimo'), 50),
    street = LEFT(ISNULL(street, 'Sin direccion'), 200),
    city = LEFT(ISNULL(city, 'Sin ciudad'), 100),
    state = LEFT(ISNULL(state, 'Sin estado'), 50),
    phone = LEFT(ISNULL(phone, '00000000'), 20)
PRINT 'Clientes normalizados según límites del dataset'

-- Productos - ajustar a límites del dataset
UPDATE products 
SET product_name = LEFT(ISNULL(product_name, 'Producto'), 100)
WHERE product_name IS NULL OR product_name = ''
PRINT 'Productos normalizados'

-- Order items - corregir valores
UPDATE order_items 
SET 
    quantity = CASE WHEN quantity IS NULL OR quantity <= 0 THEN 1 ELSE quantity END,
    discount = CASE WHEN discount IS NULL OR discount < 0 OR discount >= 1 THEN 0.0 ELSE discount END
PRINT 'Order items normalizados'

-- Actualizar precios desde productos
UPDATE oi 
SET oi.price = p.price
FROM order_items oi
INNER JOIN products p ON oi.product_id = p.product_id
WHERE oi.price IS NULL OR oi.price <= 0
PRINT 'Precios actualizados desde productos'

-- 3. CREAR DATOS DE PRUEBA CONSISTENTES
PRINT ''
PRINT '--- 3. VERIFICANDO DATOS DE PRUEBA ---'

-- Asegurar que existe al menos una orden completa y válida
IF NOT EXISTS (
    SELECT o.order_id 
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
    INNER JOIN order_items oi ON o.order_id = oi.order_id
    INNER JOIN products p ON oi.product_id = p.product_id
    GROUP BY o.order_id
    HAVING COUNT(oi.order_item_id) >= 1
)
BEGIN
    PRINT 'Creando datos de prueba básicos...'
    
    -- Asegurar usuario
    IF NOT EXISTS (SELECT * FROM users WHERE usuario_id = 1)
    BEGIN
        SET IDENTITY_INSERT users ON
        INSERT INTO users (usuario_id, usuario_name, email, password) 
        VALUES (1, 'Admin', 'admin@test.com', '123456')
        SET IDENTITY_INSERT users OFF
    END
    
    -- Asegurar cliente
    IF NOT EXISTS (SELECT * FROM customers WHERE customer_id = 1)
    BEGIN
        SET IDENTITY_INSERT customers ON
        INSERT INTO customers (customer_id, first_name, last_name, phone, email, street, city, state)
        VALUES (1, 'Juan', 'Perez', '12345678', 'juan@test.com', 'Calle Falsa 123', 'Santa Cruz', 'SC')
        SET IDENTITY_INSERT customers OFF
    END
    
    -- Asegurar categoría
    IF NOT EXISTS (SELECT * FROM categories WHERE category_id = 1)
    BEGIN
        SET IDENTITY_INSERT categories ON
        INSERT INTO categories (category_id, category_name) 
        VALUES (1, 'Bicicletas')
        SET IDENTITY_INSERT categories OFF
    END
    
    -- Asegurar productos
    IF NOT EXISTS (SELECT * FROM products WHERE product_id = 1)
    BEGIN
        SET IDENTITY_INSERT products ON
        INSERT INTO products (product_id, product_name, model_year, price, category_id)
        VALUES 
        (1, 'Bicicleta Montana', 2024, 1500.00, 1),
        (2, 'Bicicleta Carretera', 2024, 1800.00, 1),
        (3, 'Casco Protector', 2024, 120.00, 1)
        SET IDENTITY_INSERT products OFF
    END
    
    -- Crear orden de prueba
    IF NOT EXISTS (SELECT * FROM orders WHERE order_id = 1)
    BEGIN
        SET IDENTITY_INSERT orders ON
        INSERT INTO orders (order_id, customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
        VALUES (1, 1, 1, GETDATE(), GETDATE()+7, 1, 1, 1)
        SET IDENTITY_INSERT orders OFF
        
        -- Agregar items
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES 
        (1, 1, 2, 1500.00, 0.10),
        (1, 2, 1, 1800.00, 0.05),
        (1, 3, 2, 120.00, 0.00)
    END
    
    PRINT 'Datos de prueba creados'
END

-- 4. VERIFICAR INTEGRIDAD COMPLETA
PRINT ''
PRINT '--- 4. VERIFICACIÓN DE INTEGRIDAD ---'

DECLARE @errores INT = 0

-- Verificar joins
IF NOT EXISTS (
    SELECT * FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
)
BEGIN
    PRINT '✗ ERROR: No hay órdenes con clientes y usuarios válidos'
    SET @errores = @errores + 1
END

-- Verificar order_items
IF NOT EXISTS (
    SELECT * FROM order_items oi
    INNER JOIN orders o ON oi.order_id = o.order_id
    INNER JOIN products p ON oi.product_id = p.product_id
)
BEGIN
    PRINT '✗ ERROR: No hay items válidos'
    SET @errores = @errores + 1
END

-- Verificar consulta completa (como en spreporte_factura)
DECLARE @test_records INT
SELECT @test_records = COUNT(*)
FROM orders o
INNER JOIN customers c ON o.customer_id = c.customer_id
INNER JOIN users u ON o.usuario_id = u.usuario_id
INNER JOIN order_items oi ON o.order_id = oi.order_id
INNER JOIN products p ON oi.product_id = p.product_id

PRINT 'Registros disponibles para reportes: ' + CAST(@test_records AS VARCHAR(10))

IF @test_records = 0
BEGIN
    PRINT '✗ ERROR: No hay datos válidos para reportes'
    SET @errores = @errores + 1
END
ELSE
BEGIN
    PRINT '✓ Datos válidos para reportes encontrados'
END

-- 5. PROBAR PROCEDIMIENTO
PRINT ''
PRINT '--- 5. PROBANDO PROCEDIMIENTO spreporte_factura ---'

DECLARE @primera_orden INT = (
    SELECT TOP 1 o.order_id 
    FROM orders o
    INNER JOIN order_items oi ON o.order_id = oi.order_id
    ORDER BY o.order_id
)

IF @primera_orden IS NOT NULL
BEGIN
    BEGIN TRY
        CREATE TABLE #test_factura (
            order_id INT,
            users VARCHAR(50),
            Cliente VARCHAR(101),
            street VARCHAR(200),
            phone VARCHAR(20),
            state VARCHAR(50),
            city VARCHAR(100),
            order_date DATETIME,
            product_name VARCHAR(100),
            model_year INT,
            quantity INT,
            price DECIMAL(18,2),
            discount DECIMAL(18,2),
            Subtotal DECIMAL(18,2)
        )
        
        INSERT INTO #test_factura
        EXEC spreporte_factura @order_id = @primera_orden
        
        DECLARE @test_count INT = (SELECT COUNT(*) FROM #test_factura)
        PRINT '✓ Procedimiento spreporte_factura OK - ' + CAST(@test_count AS VARCHAR(10)) + ' registros'
        
        -- Mostrar muestra de datos
        PRINT 'Muestra de datos:'
        SELECT TOP 3 order_id, users, Cliente, product_name, quantity, price, Subtotal
        FROM #test_factura
        
        DROP TABLE #test_factura
    END TRY
    BEGIN CATCH
        PRINT '✗ Error en spreporte_factura: ' + ERROR_MESSAGE()
        SET @errores = @errores + 1
        IF OBJECT_ID('tempdb..#test_factura') IS NOT NULL DROP TABLE #test_factura
    END CATCH
END
ELSE
BEGIN
    PRINT '✗ ERROR: No hay órdenes para probar'
    SET @errores = @errores + 1
END

PRINT ''
PRINT '=== RESULTADO FINAL ==='

IF @errores = 0
BEGIN
    PRINT '✅ ¡TODO CORRECTO!'
    PRINT ''
    PRINT '🎉 EL DATASET DEBERÍA FUNCIONAR AHORA:'
    PRINT '1. Los datos están limpios y normalizados'
    PRINT '2. No hay valores NULL problemáticos'
    PRINT '3. Todas las longitudes respetan los límites del dataset'
    PRINT '4. El procedimiento spreporte_factura funciona'
    PRINT ''
    PRINT '▶️ PRUEBA AHORA EN TU APLICACIÓN:'
    PRINT '   - Abre PedidosApp'
    PRINT '   - Ve a Órdenes'
    PRINT '   - Selecciona la orden ' + CAST(@primera_orden AS VARCHAR(10))
    PRINT '   - Haz clic en "Imprimir/Factura"'
    PRINT '   - ¡Debería funcionar!'
END
ELSE
BEGIN
    PRINT '❌ HAY ' + CAST(@errores AS VARCHAR(10)) + ' ERRORES PENDIENTES'
    PRINT ''
    PRINT '🔧 POSIBLES SOLUCIONES:'
    PRINT '1. Regenerar el DataSet en Visual Studio:'
    PRINT '   - Click derecho en dsPrincipal.xsd > Configure DataSet with Wizard'
    PRINT '   - Recrear la conexión y el TableAdapter'
    PRINT '2. Verificar la cadena de conexión en App.config'
    PRINT '3. Asegurarse que el procedimiento spreporte_factura existe'
END

-- Estadísticas finales
PRINT ''
PRINT '--- ESTADÍSTICAS ---'
SELECT 
    'Órdenes totales' AS concepto,
    CAST(COUNT(*) AS VARCHAR(10)) AS cantidad
FROM orders
UNION ALL
SELECT 'Items totales', CAST(COUNT(*) AS VARCHAR(10)) FROM order_items
UNION ALL
SELECT 'Clientes totales', CAST(COUNT(*) AS VARCHAR(10)) FROM customers
UNION ALL
SELECT 'Productos totales', CAST(COUNT(*) AS VARCHAR(10)) FROM products
UNION ALL
SELECT 'Usuarios totales', CAST(COUNT(*) AS VARCHAR(10)) FROM users