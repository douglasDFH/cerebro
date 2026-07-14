-- ===============================================================================
-- PROCEDIMIENTO ALMACENADO PARA EL REPORTE DE FACTURA
-- Este procedimiento genera los datos necesarios para el reporte de factura
-- ===============================================================================

USE Bike_Store
GO

-- Eliminar el procedimiento si existe
IF EXISTS (SELECT * FROM sysobjects WHERE name='spreporte_factura' AND type='P')
    DROP PROCEDURE spreporte_factura
GO

CREATE PROCEDURE spreporte_factura
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
        -- Campos adicionales para la factura
        '1025874639-2' as nit_organizacion,
        CONCAT('FAC-', FORMAT(o.order_id, '000000')) as factura_numero,
        'AUT-2024-001234567890' as codigo_autorizacion,
        CASE 
            WHEN c.phone IS NOT NULL AND LEN(c.phone) >= 7 
            THEN CONCAT('CI-', RIGHT('0000000' + c.phone, 7))
            ELSE CONCAT('CI-', FORMAT(c.customer_id, '0000000'))
        END as nit_ci_cliente,
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

PRINT '=== PROCEDIMIENTO CREADO EXITOSAMENTE ==='
PRINT 'Procedimiento: spreporte_factura'
PRINT 'Parámetro: @order_id INT'
PRINT 'Campos devueltos:'
PRINT '- order_id: ID de la orden'
PRINT '- users: Nombre del usuario que creó la orden'
PRINT '- Cliente: Nombre completo del cliente'
PRINT '- street: Dirección del cliente'
PRINT '- phone: Teléfono del cliente'
PRINT '- state: Estado del cliente'
PRINT '- city: Ciudad del cliente'
PRINT '- order_date: Fecha de la orden'
PRINT '- product_name: Nombre del producto'
PRINT '- model_year: Año del modelo'
PRINT '- quantity: Cantidad'
PRINT '- price: Precio unitario'
PRINT '- discount: Descuento aplicado'
PRINT '- Subtotal: Total por línea (cantidad * precio * (1 - descuento))'

-- Probar el procedimiento si hay datos
PRINT ''
PRINT '=== PRUEBA DEL PROCEDIMIENTO ==='
IF EXISTS (SELECT * FROM orders WHERE order_id = 1)
BEGIN
    PRINT 'Probando con order_id = 1:'
    EXEC spreporte_factura @order_id = 1
END
ELSE
BEGIN
    PRINT 'No hay datos de prueba. Crear una orden primero para probar el reporte.'
    PRINT 'El procedimiento está listo para usar.'
END

PRINT ''
PRINT '=== PROCEDIMIENTO LISTO ==='
PRINT 'Ya puedes generar reportes de factura desde la aplicación.'