-- =============================================
-- Procedimiento: sp_factura_datos_principales
-- Descripción: Obtiene los datos principales de una factura incluyendo información del cliente
-- Parámetros: @factura_id INT - ID de la factura a consultar
-- Autor: Sistema Veterinario
-- Fecha: 2025-09-01
-- =============================================

IF OBJECT_ID('sp_factura_datos_principales', 'P') IS NOT NULL
    DROP PROCEDURE sp_factura_datos_principales;
GO

CREATE PROCEDURE sp_factura_datos_principales
    @factura_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        -- Datos de la factura
        f.id AS factura_id,
        f.numero_factura,
        f.fecha_emision,
        f.fecha_vencimiento,
        f.subtotal,
        f.impuestos,
        f.descuentos,
        f.total,
        f.estado,
        ISNULL(f.notas, '') AS notas,
        
        -- Datos del cliente (persona)
        p.id AS cliente_id,
        p.tipo AS cliente_tipo,
        ISNULL(p.email, '') AS cliente_email,
        ISNULL(p.direccion, '') AS cliente_direccion,
        ISNULL(p.telefono, '') AS cliente_telefono,
        
        -- Datos específicos según el tipo de persona
        CASE 
            WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
            WHEN p.tipo = 'Jurídica' THEN pj.razon_social
            ELSE 'Cliente No Identificado'
        END AS cliente_nombre_completo,
        
        CASE 
            WHEN p.tipo = 'Física' THEN ISNULL(pf.ci, '')
            WHEN p.tipo = 'Jurídica' THEN ISNULL(pj.nit, '')
            ELSE ''
        END AS cliente_documento,
        
        -- Datos adicionales para persona física
        ISNULL(pf.nombre, '') AS cliente_nombres,
        ISNULL(pf.apellido, '') AS cliente_apellidos,
        ISNULL(pf.ci, '') AS cliente_ci,
        pf.fecha_nacimiento AS cliente_fecha_nacimiento,
        ISNULL(pf.genero, '') AS cliente_genero,
        
        -- Datos adicionales para persona jurídica
        ISNULL(pj.razon_social, '') AS cliente_razon_social,
        ISNULL(pj.nit, '') AS cliente_nit,
        ISNULL(pj.encargado_nombre, '') AS cliente_encargado_nombre,
        ISNULL(pj.encargado_cargo, '') AS cliente_encargado_cargo
        
    FROM factura f
    INNER JOIN persona p ON f.persona_id = p.id
    LEFT JOIN persona_fisica pf ON p.id = pf.id AND p.tipo = 'Física'
    LEFT JOIN persona_juridica pj ON p.id = pj.id AND p.tipo = 'Jurídica'
    WHERE f.id = @factura_id
        AND f.estado != 'Anulada'
        AND p.activo = 1;
END
GO

PRINT 'Procedimiento sp_factura_datos_principales creado exitosamente';
