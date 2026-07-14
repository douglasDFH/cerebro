-- ===============================================================================
-- AGREGAR CONTROL DE INVENTARIO/STOCK AL SISTEMA
-- ===============================================================================

USE Bike_Store
GO

-- Agregar columna stock a la tabla products si no existe
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('products') AND name = 'stock')
BEGIN
    ALTER TABLE products ADD stock INT NOT NULL DEFAULT 0
    PRINT 'Columna stock agregada a la tabla products'
END
GO

-- Agregar columna stock mínimo para alertas
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('products') AND name = 'stock_minimo')
BEGIN
    ALTER TABLE products ADD stock_minimo INT NOT NULL DEFAULT 5
    PRINT 'Columna stock_minimo agregada a la tabla products'
END
GO

-- Procedimiento para actualizar stock de productos
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_update_product_stock' AND type='P')
    DROP PROCEDURE sp_update_product_stock
GO

CREATE PROCEDURE sp_update_product_stock
    @product_id INT,
    @quantity INT,
    @operation VARCHAR(10) -- 'ADD' para sumar, 'SUBTRACT' para restar
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF @operation = 'ADD'
        BEGIN
            UPDATE products 
            SET stock = stock + @quantity
            WHERE product_id = @product_id
        END
        ELSE IF @operation = 'SUBTRACT'
        BEGIN
            -- Verificar que hay suficiente stock antes de restar
            IF (SELECT stock FROM products WHERE product_id = @product_id) >= @quantity
            BEGIN
                UPDATE products 
                SET stock = stock - @quantity
                WHERE product_id = @product_id
            END
            ELSE
            BEGIN
                RAISERROR('Stock insuficiente para el producto ID: %d', 16, 1, @product_id)
                RETURN
            END
        END
        
        PRINT 'Stock actualizado correctamente'
    END TRY
    BEGIN CATCH
        PRINT 'Error al actualizar stock: ' + ERROR_MESSAGE()
        THROW
    END CATCH
END
GO

-- Procedimiento para verificar stock disponible
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_check_product_stock' AND type='P')
    DROP PROCEDURE sp_check_product_stock
GO

CREATE PROCEDURE sp_check_product_stock
    @product_id INT,
    @required_quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        product_id,
        product_name,
        stock,
        stock_minimo,
        CASE 
            WHEN stock >= @required_quantity THEN 'DISPONIBLE'
            ELSE 'INSUFICIENTE'
        END AS disponibilidad,
        (stock - @required_quantity) AS stock_restante
    FROM products 
    WHERE product_id = @product_id
END
GO

-- Procedimiento para obtener productos con stock bajo
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_productos_stock_bajo' AND type='P')
    DROP PROCEDURE sp_productos_stock_bajo
GO

CREATE PROCEDURE sp_productos_stock_bajo
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.product_id,
        p.product_name,
        c.category_name,
        p.stock,
        p.stock_minimo,
        p.price
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    WHERE p.stock <= p.stock_minimo
    ORDER BY p.stock ASC, p.product_name
END
GO

-- Modificar el procedimiento de insertar order_items para actualizar stock
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_order_items_with_stock' AND type='P')
    DROP PROCEDURE spinsertar_order_items_with_stock
GO

CREATE PROCEDURE spinsertar_order_items_with_stock
    @order_item_id INT OUTPUT,
    @order_id INT,
    @product_id INT,
    @quantity INT,
    @price DECIMAL(10,2),
    @discount DECIMAL(4,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION
    
    BEGIN TRY
        -- Verificar stock disponible
        DECLARE @stock_actual INT
        SELECT @stock_actual = stock FROM products WHERE product_id = @product_id
        
        IF @stock_actual < @quantity
        BEGIN
            RAISERROR('Stock insuficiente. Stock actual: %d, Cantidad solicitada: %d', 16, 1, @stock_actual, @quantity)
            ROLLBACK TRANSACTION
            RETURN
        END
        
        -- Insertar el item del pedido
        INSERT INTO order_items(order_id, product_id, quantity, price, discount)
        VALUES(@order_id, @product_id, @quantity, @price, @discount)
        SET @order_item_id = SCOPE_IDENTITY()
        
        -- Actualizar stock del producto
        UPDATE products 
        SET stock = stock - @quantity
        WHERE product_id = @product_id
        
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- Actualizar productos existentes con stock inicial
UPDATE products SET stock = 50, stock_minimo = 10 WHERE stock = 0
GO

PRINT 'Sistema de control de inventario implementado exitosamente'
PRINT 'Procedimientos creados:'
PRINT '- sp_update_product_stock: Para actualizar stock manualmente'
PRINT '- sp_check_product_stock: Para verificar disponibilidad'
PRINT '- sp_productos_stock_bajo: Para alertas de stock bajo'
PRINT '- spinsertar_order_items_with_stock: Para insertar items con control de stock'