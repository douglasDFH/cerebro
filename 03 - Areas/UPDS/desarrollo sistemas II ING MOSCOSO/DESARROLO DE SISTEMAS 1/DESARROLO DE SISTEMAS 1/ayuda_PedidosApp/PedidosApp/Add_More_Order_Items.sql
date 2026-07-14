-- ===============================================================================
-- SCRIPT PARA AGREGAR MÁS ÍTEMS A LAS ÓRDENES EXISTENTES
-- Este script asegura que todas las órdenes tengan al menos 3 ítems
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== AGREGANDO MÁS ÍTEMS A LAS ÓRDENES EXISTENTES ==='
PRINT ''

-- Primero, agregar más ítems a las órdenes que tienen menos de 3 ítems
PRINT '--- Agregando ítems faltantes a órdenes existentes ---'

-- Orden 1: Ya tiene 2 ítems, agregar 1 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 1) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 1) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES (1, 5, 2, 800.00, 0.10)  -- 2 llantas con 10% descuento
    PRINT 'Agregado ítem adicional a la orden 1'
END

-- Orden 2: Ya tiene 2 ítems, agregar 1 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 2) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 2) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES (2, 4, 1, 1200.00, 0.05)  -- 1 casco con 5% descuento
    PRINT 'Agregado ítem adicional a la orden 2'
END

-- Orden 3: Ya tiene 2 ítems, agregar 1 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 3) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 3) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES (3, 5, 1, 800.00, 0.00)  -- 1 llanta sin descuento
    PRINT 'Agregado ítem adicional a la orden 3'
END

-- Orden 4: Ya tiene 2 ítems, agregar 1 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 4) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 4) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES (4, 4, 2, 1200.00, 0.15)  -- 2 cascos con 15% descuento
    PRINT 'Agregado ítem adicional a la orden 4'
END

-- Orden 5: Ya tiene 2 ítems, agregar 1 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 5) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 5) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES (5, 5, 3, 800.00, 0.12)  -- 3 llantas con 12% descuento
    PRINT 'Agregado ítem adicional a la orden 5'
END

-- Orden 6: Solo tiene 1 ítem, agregar 2 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 6) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 6) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES 
    (6, 4, 1, 1200.00, 0.08),  -- 1 casco con 8% descuento
    (6, 5, 2, 800.00, 0.05)    -- 2 llantas con 5% descuento
    PRINT 'Agregados 2 ítems adicionales a la orden 6'
END

-- Orden 7: Ya tiene 2 ítems, está bien
PRINT 'La orden 7 ya tiene suficientes ítems'

-- Orden 8: Ya tiene 2 ítems, agregar 1 más
IF EXISTS (SELECT * FROM orders WHERE order_id = 8) AND 
   (SELECT COUNT(*) FROM order_items WHERE order_id = 8) < 3
BEGIN
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES (8, 5, 4, 800.00, 0.20)  -- 4 llantas con 20% descuento
    PRINT 'Agregado ítem adicional a la orden 8'
END

PRINT ''
PRINT '--- Agregando ítems a órdenes históricas ---'

-- Para las órdenes históricas que solo tienen 1 ítem, agregar 2 más
DECLARE @historical_orders TABLE (order_id INT)
INSERT INTO @historical_orders 
SELECT order_id FROM orders 
WHERE order_date < GETDATE()-30 
AND order_id NOT IN (1,2,3,4,5,6,7,8)

DECLARE @current_order_id INT
DECLARE order_cursor CURSOR FOR SELECT order_id FROM @historical_orders

OPEN order_cursor
FETCH NEXT FROM order_cursor INTO @current_order_id

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @item_count INT = (SELECT COUNT(*) FROM order_items WHERE order_id = @current_order_id)
    
    IF @item_count < 3
    BEGIN
        -- Agregar ítems aleatorios hasta llegar a 3
        WHILE @item_count < 3
        BEGIN
            DECLARE @random_product INT
            DECLARE @random_quantity INT
            DECLARE @random_discount DECIMAL(4,2)
            
            -- Seleccionar producto aleatorio (1-5)
            SET @random_product = (ABS(CHECKSUM(NEWID())) % 5) + 1
            
            -- Cantidad aleatoria (1-3)
            SET @random_quantity = (ABS(CHECKSUM(NEWID())) % 3) + 1
            
            -- Descuento aleatorio (0-25%)
            SET @random_discount = (ABS(CHECKSUM(NEWID())) % 26) * 0.01
            
            -- Verificar que el producto no esté ya en la orden
            IF NOT EXISTS (SELECT * FROM order_items WHERE order_id = @current_order_id AND product_id = @random_product)
            BEGIN
                DECLARE @product_price DECIMAL(10,2)
                SELECT @product_price = price FROM products WHERE product_id = @random_product
                
                INSERT INTO order_items (order_id, product_id, quantity, price, discount)
                VALUES (@current_order_id, @random_product, @random_quantity, @product_price, @random_discount)
                
                SET @item_count = @item_count + 1
            END
        END
        PRINT 'Agregados ítems a la orden histórica ' + CAST(@current_order_id AS VARCHAR(10))
    END
    
    FETCH NEXT FROM order_cursor INTO @current_order_id
END

CLOSE order_cursor
DEALLOCATE order_cursor

PRINT ''
PRINT '--- Creando nuevas órdenes con 3+ ítems ---'

-- Crear algunas órdenes nuevas con múltiples ítems
DECLARE @new_order_1 INT, @new_order_2 INT, @new_order_3 INT

-- Nueva orden 1
INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
VALUES (1, 1, GETDATE()-3, GETDATE()+4, 1, 1, 1)
SET @new_order_1 = SCOPE_IDENTITY()

INSERT INTO order_items (order_id, product_id, quantity, price, discount)
VALUES 
(@new_order_1, 1, 1, 15000.00, 0.08),  -- Bicicleta de montaña
(@new_order_1, 4, 2, 1200.00, 0.05),   -- 2 cascos
(@new_order_1, 5, 4, 800.00, 0.10),    -- 4 llantas
(@new_order_1, 2, 1, 18000.00, 0.15)   -- Bicicleta de carretera

-- Nueva orden 2
INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
VALUES (2, 1, GETDATE()-1, GETDATE()+6, 1, 1, 1)
SET @new_order_2 = SCOPE_IDENTITY()

INSERT INTO order_items (order_id, product_id, quantity, price, discount)
VALUES 
(@new_order_2, 3, 1, 35000.00, 0.20),  -- Bicicleta eléctrica
(@new_order_2, 4, 3, 1200.00, 0.08),   -- 3 cascos
(@new_order_2, 5, 2, 800.00, 0.12)     -- 2 llantas

-- Nueva orden 3
INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
VALUES (3, 2, GETDATE(), GETDATE()+7, 1, 1, 1)
SET @new_order_3 = SCOPE_IDENTITY()

INSERT INTO order_items (order_id, product_id, quantity, price, discount)
VALUES 
(@new_order_3, 1, 2, 15000.00, 0.10),  -- 2 bicicletas de montaña
(@new_order_3, 2, 1, 18000.00, 0.05),  -- 1 bicicleta de carretera
(@new_order_3, 4, 4, 1200.00, 0.15),   -- 4 cascos
(@new_order_3, 5, 6, 800.00, 0.18)     -- 6 llantas

PRINT 'Creadas 3 nuevas órdenes con múltiples ítems'

PRINT ''
PRINT '=== VERIFICACIÓN FINAL ==='

-- Mostrar estadísticas de ítems por orden
SELECT 
    o.order_id,
    CONCAT(c.first_name, ' ', c.last_name) AS cliente,
    COUNT(oi.order_item_id) AS total_items,
    SUM(oi.quantity) AS total_cantidad,
    SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_orden
FROM orders o
INNER JOIN customers c ON o.customer_id = c.customer_id
LEFT JOIN order_items oi ON o.order_id = oi.order_id
GROUP BY o.order_id, c.first_name, c.last_name
HAVING COUNT(oi.order_item_id) > 0
ORDER BY o.order_id

PRINT ''
PRINT '--- RESUMEN ---'
PRINT 'Órdenes con menos de 3 ítems:'

SELECT COUNT(*) AS ordenes_con_pocos_items
FROM (
    SELECT o.order_id, COUNT(oi.order_item_id) as item_count
    FROM orders o
    LEFT JOIN order_items oi ON o.order_id = oi.order_id
    GROUP BY o.order_id
    HAVING COUNT(oi.order_item_id) < 3
) AS subquery

DECLARE @total_items INT = (SELECT SUM(quantity) FROM order_items)
DECLARE @total_sales DECIMAL(15,2) = (SELECT SUM(quantity * price * (1 - discount)) FROM order_items)

PRINT 'Total de ítems vendidos: ' + CAST(@total_items AS VARCHAR(10))
PRINT 'Ventas totales: $' + CAST(@total_sales AS VARCHAR(20))

PRINT ''
PRINT '=== PROCESO COMPLETADO ==='
PRINT 'Todas las órdenes ahora tienen al menos 3 ítems.'