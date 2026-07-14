-- ===============================================================================
-- SISTEMA DE GESTIÓN DE ESTADOS DE PEDIDOS
-- ===============================================================================

USE Bike_Store
GO

-- Procedimiento para actualizar estado de pedido
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_update_order_status' AND type='P')
    DROP PROCEDURE sp_update_order_status
GO

CREATE PROCEDURE sp_update_order_status
    @order_id INT,
    @new_status TINYINT,
    @usuario_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @current_status TINYINT
        SELECT @current_status = order_status FROM orders WHERE order_id = @order_id
        
        -- Validar transiciones de estado válidas
        IF (@current_status = 1 AND @new_status IN (2, 3)) OR  -- Pendiente -> Procesando o Rechazado
           (@current_status = 2 AND @new_status IN (3, 4)) OR  -- Procesando -> Rechazado o Completado
           (@current_status = 3 AND @new_status = 1)           -- Rechazado -> Pendiente (reactivar)
        BEGIN
            UPDATE orders 
            SET order_status = @new_status,
                shipped_date = CASE WHEN @new_status = 4 THEN GETDATE() ELSE shipped_date END
            WHERE order_id = @order_id
            
            -- Si el pedido se rechaza, devolver stock a los productos
            IF @new_status = 3 AND @current_status != 3
            BEGIN
                UPDATE p SET stock = stock + oi.quantity
                FROM products p
                INNER JOIN order_items oi ON p.product_id = oi.product_id
                WHERE oi.order_id = @order_id
            END
            
            -- Si se reactiva un pedido rechazado, reducir stock nuevamente
            IF @new_status = 1 AND @current_status = 3
            BEGIN
                -- Verificar que hay suficiente stock para todos los productos
                IF EXISTS (
                    SELECT 1 FROM order_items oi
                    INNER JOIN products p ON oi.product_id = p.product_id
                    WHERE oi.order_id = @order_id AND p.stock < oi.quantity
                )
                BEGIN
                    RAISERROR('No hay suficiente stock para reactivar este pedido', 16, 1)
                    RETURN
                END
                
                -- Reducir stock
                UPDATE p SET stock = stock - oi.quantity
                FROM products p
                INNER JOIN order_items oi ON p.product_id = oi.product_id
                WHERE oi.order_id = @order_id
            END
            
            PRINT 'Estado de pedido actualizado correctamente'
        END
        ELSE
        BEGIN
            RAISERROR('Transición de estado no válida', 16, 1)
            RETURN
        END
    END TRY
    BEGIN CATCH
        PRINT 'Error al actualizar estado: ' + ERROR_MESSAGE()
        THROW
    END CATCH
END
GO

-- Procedimiento para obtener estadísticas de pedidos por estado
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_estadisticas_pedidos_estado' AND type='P')
    DROP PROCEDURE sp_estadisticas_pedidos_estado
GO

CREATE PROCEDURE sp_estadisticas_pedidos_estado
    @fecha_inicio DATE = NULL,
    @fecha_fin DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Si no se proporcionan fechas, usar el mes actual
    IF @fecha_inicio IS NULL SET @fecha_inicio = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
    IF @fecha_fin IS NULL SET @fecha_fin = EOMONTH(GETDATE())
    
    SELECT 
        CASE order_status 
            WHEN 1 THEN 'Pendiente'
            WHEN 2 THEN 'Procesando'
            WHEN 3 THEN 'Rechazado'
            WHEN 4 THEN 'Completado'
            ELSE 'Sin Estado'
        END AS estado,
        order_status,
        COUNT(*) AS cantidad_pedidos,
        ISNULL(SUM(total_order.total), 0) AS valor_total
    FROM orders o
    LEFT JOIN (
        SELECT order_id, SUM(quantity * price * (1 - discount)) as total
        FROM order_items 
        GROUP BY order_id
    ) total_order ON o.order_id = total_order.order_id
    WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
    GROUP BY order_status
    ORDER BY order_status
END
GO

-- Procedimiento para obtener pedidos que requieren atención (pendientes por más de X días)
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_pedidos_requieren_atencion' AND type='P')
    DROP PROCEDURE sp_pedidos_requieren_atencion
GO

CREATE PROCEDURE sp_pedidos_requieren_atencion
    @dias_limite INT = 3
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        o.order_id,
        CONCAT(c.first_name, ' ', c.last_name) AS cliente,
        o.order_date,
        DATEDIFF(day, o.order_date, GETDATE()) AS dias_pendientes,
        CASE o.order_status 
            WHEN 1 THEN 'Pendiente'
            WHEN 2 THEN 'Procesando'
        END AS estado,
        u.usuario_name AS usuario_asignado,
        ISNULL(total_order.total, 0) AS valor_pedido
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
    LEFT JOIN (
        SELECT order_id, SUM(quantity * price * (1 - discount)) as total
        FROM order_items 
        GROUP BY order_id
    ) total_order ON o.order_id = total_order.order_id
    WHERE o.order_status IN (1, 2) 
      AND DATEDIFF(day, o.order_date, GETDATE()) > @dias_limite
    ORDER BY dias_pendientes DESC, valor_pedido DESC
END
GO

-- Vista para dashboard de estados de pedidos
IF EXISTS (SELECT * FROM sysobjects WHERE name='vw_dashboard_estados_pedidos' AND type='V')
    DROP VIEW vw_dashboard_estados_pedidos
GO

CREATE VIEW vw_dashboard_estados_pedidos AS
SELECT 
    'Pendientes' as categoria,
    COUNT(*) as cantidad,
    ISNULL(SUM(total_order.total), 0) as valor_total
FROM orders o
LEFT JOIN (
    SELECT order_id, SUM(quantity * price * (1 - discount)) as total
    FROM order_items GROUP BY order_id
) total_order ON o.order_id = total_order.order_id
WHERE o.order_status = 1

UNION ALL

SELECT 
    'Procesando' as categoria,
    COUNT(*) as cantidad,
    ISNULL(SUM(total_order.total), 0) as valor_total
FROM orders o
LEFT JOIN (
    SELECT order_id, SUM(quantity * price * (1 - discount)) as total
    FROM order_items GROUP BY order_id
) total_order ON o.order_id = total_order.order_id
WHERE o.order_status = 2

UNION ALL

SELECT 
    'Completados Hoy' as categoria,
    COUNT(*) as cantidad,
    ISNULL(SUM(total_order.total), 0) as valor_total
FROM orders o
LEFT JOIN (
    SELECT order_id, SUM(quantity * price * (1 - discount)) as total
    FROM order_items GROUP BY order_id
) total_order ON o.order_id = total_order.order_id
WHERE o.order_status = 4 AND CAST(o.shipped_date AS DATE) = CAST(GETDATE() AS DATE)
GO

PRINT 'Sistema de gestión de estados de pedidos implementado exitosamente'
PRINT 'Procedimientos creados:'
PRINT '- sp_update_order_status: Para cambiar estados con validaciones'
PRINT '- sp_estadisticas_pedidos_estado: Estadísticas por estado'
PRINT '- sp_pedidos_requieren_atencion: Pedidos que necesitan atención'
PRINT '- vw_dashboard_estados_pedidos: Vista para dashboard'