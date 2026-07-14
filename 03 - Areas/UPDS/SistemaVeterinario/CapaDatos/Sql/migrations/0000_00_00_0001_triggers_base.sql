-- =============================================
-- TRIGGERS SIMPLES UPDATED_AT - TODAS LAS TABLAS CON UPDATED_AT
-- =============================================
-- 1. persona
CREATE TRIGGER TR_UpdatedAt_Persona ON persona
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE persona
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 2. persona_fisica
CREATE TRIGGER TR_UpdatedAt_PersonaFisica ON persona_fisica
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE persona_fisica
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 3. persona_juridica
CREATE TRIGGER TR_UpdatedAt_PersonaJuridica ON persona_juridica
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE persona_juridica
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 4. personal
CREATE TRIGGER TR_UpdatedAt_Personal ON personal
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE personal
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 5. personal_veterinario
CREATE TRIGGER TR_UpdatedAt_PersonalVeterinario ON personal_veterinario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE personal_veterinario
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 6. personal_auxiliar
CREATE TRIGGER TR_UpdatedAt_PersonalAuxiliar ON personal_auxiliar
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE personal_auxiliar
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 7. animal
CREATE TRIGGER TR_UpdatedAt_Animal ON animal
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE animal
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 8. historico
CREATE TRIGGER TR_UpdatedAt_Historico ON historico
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE historico
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 9. detalle_historico
CREATE TRIGGER TR_UpdatedAt_DetalleHistorico ON detalle_historico
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE detalle_historico
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 10. categoria
CREATE TRIGGER TR_UpdatedAt_Categoria ON categoria
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE categoria
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 11. producto
CREATE TRIGGER TR_UpdatedAt_Producto ON producto
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE producto
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 12. diagnostico
CREATE TRIGGER TR_UpdatedAt_Diagnostico ON diagnostico
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE diagnostico
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 13. factura
CREATE TRIGGER TR_UpdatedAt_Factura ON factura
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE factura
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 14. detalle_productos
CREATE TRIGGER TR_UpdatedAt_DetalleProductos ON detalle_productos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE detalle_productos
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- 15. detalle_servicios
CREATE TRIGGER TR_UpdatedAt_DetalleServicios ON detalle_servicios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE detalle_servicios
    SET updated_at = GETDATE()
    WHERE id IN (SELECT id FROM inserted);
END;
GO

-- Vista de personas completas - CRÍTICA PARA VENTAS
CREATE VIEW VW_PersonasCompletas AS
SELECT 
    p.id,
    p.tipo,
    p.email,
    p.direccion,
    p.telefono,
    p.activo,
    p.created_at,
    p.updated_at,
    pf.ci,
    pf.nombre,
    pf.apellido,
    pf.fecha_nacimiento,
    pf.genero,
    CASE
        WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
        ELSE NULL
    END as nombre_completo,
    pj.razon_social,
    pj.nit,
    pj.encargado_nombre,
    pj.encargado_cargo,
    CASE
        WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
        WHEN p.tipo = 'Jurídica' THEN pj.razon_social
        ELSE 'Sin nombre'
    END as nombre_mostrar
FROM persona p
    LEFT JOIN persona_fisica pf ON p.id = pf.id AND p.tipo = 'Física'
    LEFT JOIN persona_juridica pj ON p.id = pj.id AND p.tipo = 'Jurídica';
GO

-- Vista de animales con propietario
CREATE VIEW VW_AnimalesConPropietario AS
SELECT 
    a.id,
    a.nombre as animal_nombre,
    a.especie,
    a.raza,
    a.fecha_nacimiento,
    a.peso,
    a.color,
    a.genero,
    a.esterilizado,
    a.microchip,
    a.activo,
    a.created_at,
    a.updated_at,
    p.tipo as tipo_propietario,
    CASE
        WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
        WHEN p.tipo = 'Jurídica' THEN pj.razon_social
        ELSE 'Sin propietario'
    END as propietario,
    p.telefono as telefono_propietario,
    p.email as email_propietario,
    p.direccion as direccion_propietario,
    pf.ci as ci_propietario,
    pj.nit as nit_propietario
FROM animal a
    INNER JOIN persona p ON a.persona_id = p.id
    LEFT JOIN persona_fisica pf ON p.id = pf.id AND p.tipo = 'Física'
    LEFT JOIN persona_juridica pj ON p.id = pj.id AND p.tipo = 'Jurídica'
WHERE a.activo = 1;
GO

-- Vista de personal completo
CREATE VIEW VW_PersonalCompleto AS
SELECT 
    p.id,
    p.nombre,
    p.apellido,
    CONCAT(p.nombre, ' ', p.apellido) AS nombre_completo,
    p.email,
    p.usuario,
    p.telefono,
    p.direccion,
    p.fecha_contratacion,
    p.salario,
    p.activo,
    p.created_at,
    p.updated_at,
    CASE
        WHEN pv.id IS NOT NULL THEN 'Veterinario'
        WHEN pa.id IS NOT NULL THEN 'Auxiliar'
        ELSE 'Sin especialidad'
    END as tipo_personal,
    pv.num_licencia,
    pv.especialidad,
    pv.universidad,
    pv.anios_experiencia,
    pa.area,
    pa.turno,
    pa.nivel
FROM personal p
    LEFT JOIN personal_veterinario pv ON p.id = pv.id
    LEFT JOIN personal_auxiliar pa ON p.id = pa.id
WHERE p.activo = 1;
GO

-- RESUMEN 
PRINT 'Triggers de updated_at creados exitosamente para 15 tablas:'
PRINT '1. persona, 2. persona_fisica, 3. persona_juridica'
PRINT '4. personal, 5. personal_veterinario, 6. personal_auxiliar'
PRINT '7. animal, 8. historico, 9. detalle_historico'
PRINT '10. categoria, 11. producto, 12. diagnostico'
PRINT '13. factura, 14. detalle_productos, 15. detalle_servicios'
PRINT ''


PRINT 'Creando vistas del sistema...'


PRINT 'Vistas del sistema creadas:'
PRINT '- VW_PersonasCompletas (crítica para ventas)'
PRINT '- VW_AnimalesConPropietario'  
PRINT '- VW_PersonalCompleto'
PRINT ''
PRINT 'Sistema listo para funcionar correctamente.'
GO
