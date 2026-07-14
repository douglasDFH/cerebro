-- ===============================================================================
-- SCRIPT PARA INSERTAR DATOS DE PRUEBA PARA EL DASHBOARD
-- Este script crea datos de ejemplo para que el dashboard muestre estadísticas
-- ===============================================================================

USE Bike_Store
GO

-- Insertar algunos pedidos de prueba si no existen
IF NOT EXISTS (SELECT * FROM orders WHERE customer_id = 1)
BEGIN
    -- Crear algunos pedidos para el primer cliente
    INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
    VALUES 
    (1, 4, GETDATE()-30, GETDATE()-23, 1, 1, 1),
    (1, 4, GETDATE()-25, GETDATE()-18, 1, 1, 1),
    (2, 4, GETDATE()-20, GETDATE()-13, 1, 1, 1),
    (2, 1, GETDATE()-15, GETDATE()-8, 1, 1, 1),
    (3, 4, GETDATE()-10, GETDATE()-3, 1, 1, 1),
    (3, 2, GETDATE()-5, GETDATE()+2, 1, 1, 1),
    (1, 1, GETDATE()-2, GETDATE()+5, 1, 1, 1),
    (2, 3, GETDATE()-1, GETDATE()+6, 1, 1, 1)
END

-- Insertar algunos items de pedidos si no existen
IF NOT EXISTS (SELECT * FROM order_items WHERE order_id = 1)
BEGIN
    -- Items para el pedido 1
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES 
    (1, 1, 2, 15000.00, 0.05),  -- 2 bicicletas de montaña con 5% descuento
    (1, 4, 1, 1200.00, 0.00),   -- 1 casco
    
    -- Items para el pedido 2
    (2, 2, 1, 18000.00, 0.10),  -- 1 bicicleta de carretera con 10% descuento
    (2, 5, 2, 800.00, 0.00),    -- 2 llantas
    
    -- Items para el pedido 3
    (3, 3, 1, 35000.00, 0.15),  -- 1 bicicleta eléctrica con 15% descuento
    (3, 4, 1, 1200.00, 0.05),   -- 1 casco con 5% descuento
    
    -- Items para el pedido 4
    (4, 1, 1, 15000.00, 0.00),  -- 1 bicicleta de montaña
    (4, 5, 4, 800.00, 0.10),    -- 4 llantas con 10% descuento
    
    -- Items para el pedido 5
    (5, 2, 2, 18000.00, 0.08),  -- 2 bicicletas de carretera con 8% descuento
    (5, 4, 2, 1200.00, 0.00),   -- 2 cascos
    
    -- Items para el pedido 6
    (6, 3, 1, 35000.00, 0.12),  -- 1 bicicleta eléctrica con 12% descuento
    
    -- Items para el pedido 7
    (7, 1, 3, 15000.00, 0.20),  -- 3 bicicletas de montaña con 20% descuento
    (7, 5, 6, 800.00, 0.15),    -- 6 llantas con 15% descuento
    
    -- Items para el pedido 8
    (8, 2, 1, 18000.00, 0.05),  -- 1 bicicleta de carretera con 5% descuento
    (8, 4, 3, 1200.00, 0.10)    -- 3 cascos con 10% descuento
END

-- Crear algunos pedidos adicionales para diferentes meses (para el gráfico)
IF NOT EXISTS (SELECT * FROM orders WHERE order_date < GETDATE()-60)
BEGIN
    INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
    VALUES 
    -- Pedidos de hace 2 meses
    (1, 4, GETDATE()-60, GETDATE()-53, 1, 1, 1),
    (2, 4, GETDATE()-65, GETDATE()-58, 1, 1, 1),
    (3, 4, GETDATE()-70, GETDATE()-63, 1, 1, 1),
    
    -- Pedidos de hace 3 meses
    (1, 4, GETDATE()-90, GETDATE()-83, 1, 1, 1),
    (2, 4, GETDATE()-95, GETDATE()-88, 1, 1, 1),
    (3, 4, GETDATE()-100, GETDATE()-93, 1, 1, 1),
    
    -- Pedidos de hace 4 meses
    (1, 4, GETDATE()-120, GETDATE()-113, 1, 1, 1),
    (2, 4, GETDATE()-125, GETDATE()-118, 1, 1, 1)
    
    -- Obtener los IDs de los pedidos recién insertados para agregar items
    DECLARE @order_id_60 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-60 AS DATE))
    DECLARE @order_id_65 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-65 AS DATE))
    DECLARE @order_id_70 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-70 AS DATE))
    DECLARE @order_id_90 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-90 AS DATE))
    DECLARE @order_id_95 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-95 AS DATE))
    DECLARE @order_id_100 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-100 AS DATE))
    DECLARE @order_id_120 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-120 AS DATE))
    DECLARE @order_id_125 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-125 AS DATE))
    
    -- Items para pedidos históricos
    IF @order_id_60 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_60, 1, 1, 15000.00, 0.05)
        
    IF @order_id_65 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_65, 2, 2, 18000.00, 0.10)
        
    IF @order_id_70 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_70, 3, 1, 35000.00, 0.15)
        
    IF @order_id_90 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_90, 1, 3, 15000.00, 0.08)
        
    IF @order_id_95 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_95, 2, 1, 18000.00, 0.12)
        
    IF @order_id_100 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_100, 4, 5, 1200.00, 0.20)
        
    IF @order_id_120 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_120, 1, 2, 15000.00, 0.00)
        
    IF @order_id_125 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_125, 5, 8, 800.00, 0.25)
END

PRINT '=== DATOS DE PRUEBA INSERTADOS ==='
PRINT ''

-- Mostrar estadísticas actuales
PRINT '--- ESTADÍSTICAS ACTUALES ---'
PRINT 'Total de pedidos: ' + CAST((SELECT COUNT(*) FROM orders) AS VARCHAR(10))
PRINT 'Total de clientes: ' + CAST((SELECT COUNT(*) FROM customers) AS VARCHAR(10))  
PRINT 'Total de productos: ' + CAST((SELECT COUNT(*) FROM products) AS VARCHAR(10))
PRINT 'Total de items vendidos: ' + CAST((SELECT ISNULL(SUM(quantity), 0) FROM order_items) AS VARCHAR(10))
PRINT 'Ventas totales: $' + CAST((SELECT ISNULL(SUM(quantity * price * (1 - discount)), 0) FROM order_items) AS VARCHAR(20))

PRINT ''
PRINT '=== DATOS LISTOS PARA EL DASHBOARD ==='
PRINT 'Ya puedes abrir el Dashboard y ver las estadísticas y gráficos.'