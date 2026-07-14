-- =============================================
-- SCRIPT PARA AGREGAR COLUMNA GÉNERO A TABLA PERSONA
-- Sistema Veterinario
-- =============================================

USE SistemaVeterinario;
GO

-- Verificar si la columna Genero ya existe
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Persona' AND COLUMN_NAME = 'Genero'
)
BEGIN
    -- Agregar la columna Genero
    ALTER TABLE Persona 
    ADD Genero CHAR(1) NULL CHECK (Genero IN ('F', 'M', 'O'));
    
    PRINT 'Columna Genero agregada exitosamente a la tabla Persona';
    
    -- Actualizar registros existentes de personas físicas con género por defecto
    UPDATE Persona 
    SET Genero = 'F' 
    WHERE TipoPersona = 'F' AND Genero IS NULL;
    
    PRINT 'Registros existentes actualizados con género por defecto (F)';
END
ELSE
BEGIN
    PRINT 'La columna Genero ya existe en la tabla Persona';
END

-- Verificar el resultado
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Persona' AND COLUMN_NAME = 'Genero';

-- Mostrar algunas personas para verificar
SELECT TOP 5
    IdPersona,
    TipoPersona,
    Nombre,
    Apellidos,
    Genero,
    FechaRegistro
FROM Persona
WHERE TipoPersona = 'F'
ORDER BY IdPersona;