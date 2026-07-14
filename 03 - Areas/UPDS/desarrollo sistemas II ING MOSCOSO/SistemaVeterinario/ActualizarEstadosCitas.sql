-- =============================================
-- Script para actualizar los estados válidos en la tabla Diagnostico
-- para soportar el módulo de citas
-- Autor: Claude
-- Fecha: 2025-08-27
-- =============================================

USE SistemaVeterinario;
GO

-- Primero, vamos a verificar si hay registros con estados que vamos a cambiar
SELECT Estado, COUNT(*) as Cantidad
FROM Diagnostico
GROUP BY Estado;

-- Actualizar la restricción CHECK para permitir los nuevos estados
ALTER TABLE Diagnostico
DROP CONSTRAINT IF EXISTS CK__Diagnosti__Estad__52593CB8;

-- Agregar la nueva restricción CHECK con los estados de citas
ALTER TABLE Diagnostico
ADD CONSTRAINT CK_Diagnostico_Estado 
CHECK (Estado IN (
    'PENDIENTE', 'EN_TRATAMIENTO', 'RESUELTO', 'DERIVADO', -- Estados originales para diagnósticos
    'PROGRAMADO', 'CONFIRMADO', 'EN_PROCESO', 'COMPLETADO', 'CANCELADO', 'NO_ASISTIO' -- Estados para citas
));

-- Verificar que la restricción se aplicó correctamente
SELECT 
    CONSTRAINT_NAME, 
    CHECK_CLAUSE 
FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS 
WHERE TABLE_NAME = 'Diagnostico' AND COLUMN_NAME = 'Estado';

PRINT 'Estados de Diagnostico actualizados exitosamente para soportar el módulo de citas';