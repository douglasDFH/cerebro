-- =============================================
-- Procedimiento: sp_factura_detalle_productos
-- Descripción: Obtiene el detalle de productos vendidos en una factura
-- Parámetros: @factura_id INT - ID de la factura a consultar
-- Autor: Sistema Veterinario
-- Fecha: 2025-09-01
-- =============================================

IF OBJECT_ID('sp_factura_detalle_productos', 'P') IS NOT NULL
    DROP PROCEDURE sp_factura_detalle_productos;
GO

CREATE PROCEDURE sp_factura_detalle_productos
    @factura_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Datos del detalle
        dp.id AS detalle_id,
        dp.cantidad,
        dp.precio_unitario,
        dp.descuento_unitario,
        dp.subtotal,
        dp.receta_verificada,
        ISNULL(dp.lote, '') AS lote,
        dp.fecha_vencimiento_producto,
        
        -- Datos del producto
        p.id AS producto_id,
        ISNULL(p.codigo, '') AS producto_codigo,
        p.nombre AS producto_nombre,
        ISNULL(p.descripcion, '') AS producto_descripcion,
        p.precio AS producto_precio_catalogo,
        p.requiere_receta AS producto_requiere_receta,
        
        -- Datos de la categoría
        c.id AS categoria_id,
        c.nombre AS categoria_nombre,
        ISNULL(c.descripcion, '') AS categoria_descripcion,
        
        -- Cálculos adicionales
        (dp.cantidad * dp.precio_unitario) AS subtotal_bruto,
        (dp.cantidad * dp.descuento_unitario) AS descuento_total,
        dp.subtotal AS subtotal_neto
        
    FROM detalle_productos dp
    INNER JOIN producto p ON dp.producto_id = p.id
    INNER JOIN categoria c ON p.categoria_id = c.id
    WHERE dp.factura_id = @factura_id
        AND p.activo = 1
        AND c.activo = 1
    ORDER BY dp.id;
END
GO

PRINT 'Procedimiento sp_factura_detalle_productos creado exitosamente';
