-- ===============================================================================
-- SOLUCION RAPIDA PARA EL ERROR DE COMPILACION
-- Este script crea una version temporal del procedimiento con campos adicionales
-- ===============================================================================

USE Bike_Store
GO

-- Eliminar procedimiento anterior si existe
IF EXISTS (SELECT * FROM sysobjects WHERE name='spreporte_factura_temp' AND type='P')
    DROP PROCEDURE spreporte_factura_temp
GO

-- Crear procedimiento temporal con los nuevos campos
CREATE PROCEDURE spreporte_factura_temp
    @order_id INT
AS
BEGIN
    SELECT 
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
        -- Campos adicionales simplificados
        '1025874639-2' as nit_organizacion,
        CONCAT('FAC-', RIGHT('000000' + CAST(o.order_id AS VARCHAR), 6)) as factura_numero,
        'AUT-2024-001234567890' as codigo_autorizacion,
        CONCAT('CI-', RIGHT('0000000' + ISNULL(c.phone, CAST(c.customer_id AS VARCHAR)), 7)) as nit_ci_cliente,
        'BIKE STORE BOLIVIA S.R.L.' as razon_social_organizacion,
        'Av. Principales #123, Zona Central' as direccion_organizacion,
        '76436834' as telefono_organizacion,
        'Santa Cruz de la Sierra' as ciudad_organizacion,
        'Sucursal Central - Local 001' as sucursal_info
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
    INNER JOIN order_items oi ON o.order_id = oi.order_id
    INNER JOIN products p ON oi.product_id = p.product_id
    WHERE o.order_id = @order_id
    ORDER BY oi.order_item_id
END
GO

PRINT 'Procedimiento temporal creado exitosamente'
PRINT 'Usar spreporte_factura_temp para pruebas'