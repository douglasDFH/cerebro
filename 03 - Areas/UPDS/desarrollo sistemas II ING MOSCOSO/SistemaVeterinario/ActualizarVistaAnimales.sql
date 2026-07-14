-- =============================================
-- SCRIPT PARA ACTUALIZAR VISTA VW_AnimalesConPropietarios
-- Sistema Veterinario
-- =============================================

USE SistemaVeterinario;
GO

-- Eliminar la vista existente si existe
IF EXISTS (SELECT * FROM sys.views WHERE name = 'VW_AnimalesConPropietarios')
BEGIN
    DROP VIEW VW_AnimalesConPropietarios;
    PRINT 'Vista VW_AnimalesConPropietarios eliminada';
END

-- Crear la vista actualizada con todas las columnas necesarias
GO
CREATE VIEW VW_AnimalesConPropietarios AS
SELECT 
    a.IdAnimal,
    a.Nombre AS NombreAnimal,
    a.Tipo,
    a.Raza,
    a.Color,
    a.Sexo,
    a.FechaNacimiento,
    a.Edad,
    a.Peso,
    a.Altura,
    a.Microchip,
    a.NumPedigree,
    a.Esterilizado,
    a.Vacunado,
    a.Observaciones,
    p.IdPersona AS IdPropietario,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS NombrePropietario,
    p.Telefono,
    p.Email,
    a.FechaRegistro
FROM Animal a
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
WHERE a.Estado = 1 AND p.Estado = 1;
GO

PRINT 'Vista VW_AnimalesConPropietarios actualizada exitosamente';

-- Verificar la estructura de la vista
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.VIEW_COLUMN_USAGE vcu
INNER JOIN INFORMATION_SCHEMA.COLUMNS c ON vcu.COLUMN_NAME = c.COLUMN_NAME
WHERE vcu.VIEW_NAME = 'VW_AnimalesConPropietarios'
ORDER BY ORDINAL_POSITION;

-- Consulta de prueba
SELECT TOP 3 * FROM VW_AnimalesConPropietarios;