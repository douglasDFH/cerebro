-- =============================================
-- Script de instalación completa del módulo de citas
-- Este script debe ejecutarse para corregir el problema
-- con la inserción de citas
-- Autor: Claude
-- Fecha: 2025-08-27
-- =============================================

USE SistemaVeterinario;
GO

PRINT '=== INSTALANDO MÓDULO DE CITAS ===';
PRINT '';

-- Paso 1: Mostrar estados actuales
PRINT '1. Estados actuales en la tabla Diagnostico:';
SELECT Estado, COUNT(*) as Cantidad
FROM Diagnostico
GROUP BY Estado;
PRINT '';

-- Paso 2: Actualizar la restricción CHECK para permitir los nuevos estados
PRINT '2. Actualizando restricción de estados...';

-- Buscar y eliminar las restricciones CHECK existentes para Estado
DECLARE @sql NVARCHAR(MAX);
DECLARE @constraint_name NVARCHAR(256);

DECLARE constraint_cursor CURSOR FOR 
SELECT name 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('Diagnostico')
AND definition LIKE '%Estado%';

OPEN constraint_cursor;
FETCH NEXT FROM constraint_cursor INTO @constraint_name;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = 'ALTER TABLE Diagnostico DROP CONSTRAINT ' + QUOTENAME(@constraint_name);
    EXEC sp_executesql @sql;
    PRINT '   - Eliminada restricción: ' + @constraint_name;
    FETCH NEXT FROM constraint_cursor INTO @constraint_name;
END

CLOSE constraint_cursor;
DEALLOCATE constraint_cursor;

-- Agregar la nueva restricción CHECK con los estados de citas
ALTER TABLE Diagnostico
ADD CONSTRAINT CK_Diagnostico_Estado 
CHECK (Estado IN (
    'PENDIENTE', 'EN_TRATAMIENTO', 'RESUELTO', 'DERIVADO', -- Estados originales para diagnósticos
    'PROGRAMADO', 'CONFIRMADO', 'EN_PROCESO', 'COMPLETADO', 'CANCELADO', 'NO_ASISTIO' -- Estados para citas
));

PRINT '   - Agregada nueva restricción con estados de citas';
PRINT '';

-- Paso 3: Verificar que la restricción se aplicó correctamente
PRINT '3. Verificando nueva restricción:';
SELECT 
    CONSTRAINT_NAME, 
    CHECK_CLAUSE 
FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS 
WHERE TABLE_NAME = 'Diagnostico' AND CONSTRAINT_NAME = 'CK_Diagnostico_Estado';
PRINT '';

-- Paso 4: Recrear el procedimiento almacenado
PRINT '4. Actualizando procedimiento SP_ProgramarCita...';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ProgramarCita')
    DROP PROCEDURE SP_ProgramarCita;

EXEC('
CREATE PROCEDURE SP_ProgramarCita
    @IdAnimal INT,
    @IdVeterinario INT,
    @FechaCita DATETIME2,
    @Motivo NVARCHAR(500),
    @Tipo VARCHAR(20) = ''PRIMERA_VEZ'',
    @EsUrgente BIT = 0,
    @CostoConsulta DECIMAL(10,2) = NULL,
    @Observaciones NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validar que el animal existe y está activo
    IF NOT EXISTS (SELECT 1 FROM Animal WHERE IdAnimal = @IdAnimal AND Estado = 1)
    BEGIN
        RAISERROR(''El animal seleccionado no existe o no está activo.'', 16, 1);
        RETURN;
    END
    
    -- Validar que el veterinario existe
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdVeterinario AND Estado = 1)
    BEGIN
        RAISERROR(''El veterinario seleccionado no existe o no está activo.'', 16, 1);
        RETURN;
    END
    
    -- Validar que la fecha no sea en el pasado
    IF @FechaCita < DATEADD(MINUTE, -5, GETDATE()) -- 5 minutos de tolerancia
    BEGIN
        RAISERROR(''No se pueden programar citas en el pasado.'', 16, 1);
        RETURN;
    END
    
    -- Validar disponibilidad del veterinario (no debe tener otra cita en la misma hora ± 30 min)
    IF EXISTS (
        SELECT 1 FROM Diagnostico 
        WHERE IdVeterinario = @IdVeterinario 
        AND ABS(DATEDIFF(MINUTE, Fecha, @FechaCita)) < 30
        AND Estado IN (''PROGRAMADO'', ''CONFIRMADO'', ''EN_PROCESO'')
    )
    BEGIN
        RAISERROR(''El veterinario no está disponible en el horario seleccionado. Ya tiene una cita programada cerca de esta hora.'', 16, 1);
        RETURN;
    END
    
    -- Crear el diagnóstico como cita programada
    INSERT INTO Diagnostico (
        IdAnimal,
        IdVeterinario,
        Fecha,
        Descripcion,
        Sintomas,
        Tratamiento,
        Medicamentos,
        Dosis,
        ProximaVisita,
        Urgencia,
        Estado,
        Costo,
        FechaRegistro,
        Observaciones
    )
    VALUES (
        @IdAnimal,
        @IdVeterinario,
        @FechaCita,
        @Motivo,
        ''Cita programada - '' + @Tipo,
        NULL, -- Se llenará durante la consulta
        NULL, -- Se llenará durante la consulta
        NULL, -- Se llenará durante la consulta
        NULL, -- Se puede programar seguimiento después
        CASE WHEN @EsUrgente = 1 THEN ''ALTA'' ELSE ''NORMAL'' END,
        ''PROGRAMADO'',
        @CostoConsulta,
        GETDATE(),
        @Observaciones
    );
    
    SELECT SCOPE_IDENTITY() AS IdDiagnostico;
END;
');

PRINT '   - Procedimiento actualizado exitosamente';
PRINT '';

-- Paso 5: Crear algunos datos de prueba si no existen
PRINT '5. Verificando datos de prueba...';

-- Verificar si hay animales activos
DECLARE @cantidadAnimales INT;
SELECT @cantidadAnimales = COUNT(*) FROM Animal WHERE Estado = 1;

IF @cantidadAnimales = 0
BEGIN
    PRINT '   - ADVERTENCIA: No hay animales activos en el sistema. Es necesario crear animales para poder programar citas.';
END
ELSE
BEGIN
    PRINT '   - Animales disponibles: ' + CAST(@cantidadAnimales AS VARCHAR);
END

-- Verificar si hay veterinarios activos
DECLARE @cantidadVeterinarios INT;
SELECT @cantidadVeterinarios = COUNT(*) 
FROM Usuario 
WHERE Estado = 1 AND (Rol = 'VETERINARIO' OR Rol = 'ADMIN');

IF @cantidadVeterinarios = 0
BEGIN
    PRINT '   - ADVERTENCIA: No hay veterinarios activos en el sistema. Es necesario crear usuarios veterinarios para poder programar citas.';
END
ELSE
BEGIN
    PRINT '   - Veterinarios disponibles: ' + CAST(@cantidadVeterinarios AS VARCHAR);
END

PRINT '';

-- Paso 6: Probar el procedimiento con una cita de ejemplo
PRINT '6. Probando el módulo de citas...';

DECLARE @IdAnimalPrueba INT, @IdVeterinarioPrueba INT;

-- Obtener el primer animal activo
SELECT TOP 1 @IdAnimalPrueba = IdAnimal FROM Animal WHERE Estado = 1;

-- Obtener el primer veterinario activo
SELECT TOP 1 @IdVeterinarioPrueba = IdUsuario 
FROM Usuario 
WHERE Estado = 1 AND (Rol = 'VETERINARIO' OR Rol = 'ADMIN');

IF @IdAnimalPrueba IS NOT NULL AND @IdVeterinarioPrueba IS NOT NULL
BEGIN
    BEGIN TRY
        DECLARE @FechaPrueba DATETIME2 = DATEADD(HOUR, 2, GETDATE()); -- 2 horas desde ahora
        
        EXEC SP_ProgramarCita 
            @IdAnimal = @IdAnimalPrueba,
            @IdVeterinario = @IdVeterinarioPrueba,
            @FechaCita = @FechaPrueba,
            @Motivo = 'Cita de prueba del sistema',
            @Tipo = 'PRIMERA_VEZ',
            @EsUrgente = 0,
            @CostoConsulta = 50.00,
            @Observaciones = 'Cita creada durante la instalación del módulo';
        
        PRINT '   - ✓ Cita de prueba creada exitosamente';
        
        -- Eliminar la cita de prueba
        DELETE FROM Diagnostico 
        WHERE IdAnimal = @IdAnimalPrueba 
        AND IdVeterinario = @IdVeterinarioPrueba 
        AND Descripcion = 'Cita de prueba del sistema'
        AND Estado = 'PROGRAMADO';
        
        PRINT '   - ✓ Cita de prueba eliminada';
        
    END TRY
    BEGIN CATCH
        PRINT '   - ❌ Error en la prueba: ' + ERROR_MESSAGE();
    END CATCH
END
ELSE
BEGIN
    PRINT '   - ⚠ No se puede probar: faltan animales o veterinarios en el sistema';
END

PRINT '';
PRINT '=== INSTALACIÓN COMPLETADA ===';
PRINT '';
PRINT 'El módulo de citas ha sido instalado correctamente.';
PRINT 'Los estados disponibles para citas son:';
PRINT '- PROGRAMADO: Cita programada, pendiente de confirmación';
PRINT '- CONFIRMADO: Cita confirmada por el cliente';
PRINT '- EN_PROCESO: Consulta en curso';
PRINT '- COMPLETADO: Consulta terminada';
PRINT '- CANCELADO: Cita cancelada';
PRINT '- NO_ASISTIO: Cliente no se presentó a la cita';
PRINT '';
PRINT 'Estados para diagnósticos tradicionales:';
PRINT '- PENDIENTE: Diagnóstico pendiente';
PRINT '- EN_TRATAMIENTO: Tratamiento en curso';  
PRINT '- RESUELTO: Caso resuelto';
PRINT '- DERIVADO: Derivado a especialista';
PRINT '';