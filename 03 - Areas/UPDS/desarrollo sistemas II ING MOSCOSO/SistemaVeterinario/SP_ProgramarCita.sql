-- =============================================
-- Procedimiento para programar una nueva cita
-- Autor: Claude
-- Fecha: 2025-08-27
-- =============================================

USE SistemaVeterinario;
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ProgramarCita')
    DROP PROCEDURE SP_ProgramarCita;
GO

CREATE PROCEDURE SP_ProgramarCita
    @IdAnimal INT,
    @IdVeterinario INT,
    @FechaCita DATETIME2,
    @Motivo NVARCHAR(500),
    @Tipo VARCHAR(20) = 'PRIMERA_VEZ',
    @EsUrgente BIT = 0,
    @CostoConsulta DECIMAL(10,2) = NULL,
    @Observaciones NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validar que el animal existe y está activo
    IF NOT EXISTS (SELECT 1 FROM Animal WHERE IdAnimal = @IdAnimal AND Estado = 1)
    BEGIN
        RAISERROR('El animal seleccionado no existe o no está activo.', 16, 1);
        RETURN;
    END
    
    -- Validar que el veterinario existe
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdVeterinario AND Estado = 1)
    BEGIN
        RAISERROR('El veterinario seleccionado no existe o no está activo.', 16, 1);
        RETURN;
    END
    
    -- Validar que la fecha no sea en el pasado
    IF @FechaCita < DATEADD(MINUTE, -5, GETDATE()) -- 5 minutos de tolerancia
    BEGIN
        RAISERROR('No se pueden programar citas en el pasado.', 16, 1);
        RETURN;
    END
    
    -- Validar disponibilidad del veterinario (no debe tener otra cita en la misma hora ± 30 min)
    IF EXISTS (
        SELECT 1 FROM Diagnostico 
        WHERE IdVeterinario = @IdVeterinario 
        AND ABS(DATEDIFF(MINUTE, Fecha, @FechaCita)) < 30
        AND Estado IN ('PROGRAMADO', 'CONFIRMADO', 'EN_PROCESO')
    )
    BEGIN
        RAISERROR('El veterinario no está disponible en el horario seleccionado. Ya tiene una cita programada cerca de esta hora.', 16, 1);
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
        'Cita programada - ' + @Tipo,
        NULL, -- Se llenará durante la consulta
        NULL, -- Se llenará durante la consulta
        NULL, -- Se llenará durante la consulta
        NULL, -- Se puede programar seguimiento después
        CASE WHEN @EsUrgente = 1 THEN 'ALTA' ELSE 'NORMAL' END,
        'PROGRAMADO',
        @CostoConsulta,
        GETDATE(),
        @Observaciones
    );
    
    SELECT SCOPE_IDENTITY() AS IdDiagnostico;
END;
GO

PRINT 'Procedimiento SP_ProgramarCita creado exitosamente';