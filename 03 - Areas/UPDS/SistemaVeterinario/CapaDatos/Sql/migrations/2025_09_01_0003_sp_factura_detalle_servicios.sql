-- =============================================
-- Procedimiento: sp_factura_detalle_servicios
-- Descripción: Obtiene el detalle de servicios prestados en una factura
-- Parámetros: @factura_id INT - ID de la factura a consultar
-- Autor: Sistema Veterinario
-- Fecha: 2025-09-01
-- =============================================

IF OBJECT_ID('sp_factura_detalle_servicios', 'P') IS NOT NULL
    DROP PROCEDURE sp_factura_detalle_servicios;
GO

CREATE PROCEDURE sp_factura_detalle_servicios
    @factura_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Datos del detalle del servicio
        ds.id AS detalle_id,
        ds.cantidad,
        ds.precio_unitario,
        ds.descuento_unitario,
        ds.subtotal,
        
        -- Datos del diagnóstico/servicio
        d.id AS diagnostico_id,
        ISNULL(d.codigo, '') AS diagnostico_codigo,
        d.nombre AS diagnostico_nombre,
        ISNULL(d.descripcion, '') AS diagnostico_descripcion,
        d.precio_base AS diagnostico_precio_base,
        ISNULL(d.categoria_diagnostico, '') AS diagnostico_categoria,
        d.requiere_equipamiento AS diagnostico_requiere_equipamiento,
        
        -- Datos del veterinario (si aplica)
        pv.id AS veterinario_id,
        ISNULL(pv.num_licencia, '') AS veterinario_licencia,
        ISNULL(pv.especialidad, '') AS veterinario_especialidad,
        ISNULL(CONCAT(per.nombre, ' ', per.apellido), '') AS veterinario_nombre_completo,
        
        -- Datos del detalle histórico (si está vinculado)
        dh.id AS historico_detalle_id,
        ISNULL(dh.tipo_evento, '') AS historico_tipo_evento,
        dh.fecha_evento AS historico_fecha_evento,
        ISNULL(dh.observaciones, '') AS historico_observaciones,
        ISNULL(dh.tratamiento, '') AS historico_tratamiento,
        
        -- Datos del animal (si hay detalle histórico)
        ISNULL(a.id, 0) AS animal_id,
        ISNULL(a.nombre, '') AS animal_nombre,
        ISNULL(a.especie, '') AS animal_especie,
        ISNULL(a.raza, '') AS animal_raza,
        
        -- Cálculos adicionales
        (ds.cantidad * ds.precio_unitario) AS subtotal_bruto,
        (ds.cantidad * ds.descuento_unitario) AS descuento_total,
        ds.subtotal AS subtotal_neto
        
    FROM detalle_servicios ds
    INNER JOIN diagnostico d ON ds.diagnostico_id = d.id
    LEFT JOIN personal_veterinario pv ON ds.veterinario_id = pv.id
    LEFT JOIN personal per ON pv.id = per.id
    LEFT JOIN detalle_historico dh ON ds.detalle_historico_id = dh.id
    LEFT JOIN historico h ON dh.historico_id = h.id
    LEFT JOIN animal a ON h.animal_id = a.id
    WHERE ds.factura_id = @factura_id
        AND d.activo = 1
    ORDER BY ds.id;
END
GO

PRINT 'Procedimiento sp_factura_detalle_servicios creado exitosamente';
