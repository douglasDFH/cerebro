-- =============================================
-- MÓDULO DE CITAS - SISTEMA VETERINARIO
-- Fecha: 2025-08-27
-- Descripción: Script para crear tabla de citas y procedimientos
-- =============================================

USE SistemaVeterinario;
GO

-- =============================================
-- CREAR TABLA CITA
-- =============================================
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Cita')
    DROP TABLE Cita;
GO

CREATE TABLE Cita (
    IdCita INT IDENTITY(1,1) PRIMARY KEY,
    IdAnimal INT NOT NULL,
    IdVeterinario INT NULL, -- Puede asignarse después
    IdUsuarioRegistro INT NOT NULL, -- Usuario que registra la cita
    FechaHora DATETIME2 NOT NULL,
    TipoCita VARCHAR(50) NOT NULL CHECK (TipoCita IN ('CONSULTA', 'VACUNACION', 'CIRUGIA', 'REVISION', 'URGENCIA', 'ESTETICA', 'OTRO')),
    EstadoCita VARCHAR(20) NOT NULL DEFAULT 'PROGRAMADA' CHECK (EstadoCita IN ('PROGRAMADA', 'CONFIRMADA', 'ENPROGRESO', 'COMPLETADA', 'CANCELADA', 'NOASISTIO')),
    MotivoConsulta NVARCHAR(500) NOT NULL,
    Observaciones NVARCHAR(1000) NULL,
    TiempoEstimado INT DEFAULT 30, -- Duración en minutos
    Urgente BIT DEFAULT 0,
    Costo DECIMAL(10,2) NULL,
    FechaRegistro DATETIME2 DEFAULT GETDATE(),
    FechaModificacion DATETIME2 DEFAULT GETDATE(),
    Estado BIT DEFAULT 1, -- 1=Activo, 0=Eliminado
    
    -- Claves foráneas
    CONSTRAINT FK_Cita_Animal FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    CONSTRAINT FK_Cita_Veterinario FOREIGN KEY (IdVeterinario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Cita_UsuarioRegistro FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario),
    
    -- Índices para optimizar consultas
    INDEX IX_Cita_FechaHora (FechaHora),
    INDEX IX_Cita_Animal (IdAnimal),
    INDEX IX_Cita_Veterinario (IdVeterinario),
    INDEX IX_Cita_Estado (EstadoCita)
);
GO

-- =============================================
-- VISTA PARA MOSTRAR CITAS CON INFORMACIÓN RELACIONADA
-- =============================================
IF EXISTS (SELECT * FROM sys.views WHERE name = 'VW_CitasCompletas')
    DROP VIEW VW_CitasCompletas;
GO

CREATE VIEW VW_CitasCompletas AS
SELECT 
    c.IdCita,
    c.FechaHora,
    c.TipoCita,
    c.EstadoCita,
    c.MotivoConsulta,
    c.Observaciones,
    c.TiempoEstimado,
    c.Urgente,
    c.Costo,
    c.FechaRegistro,
    -- Información del animal
    a.IdAnimal,
    a.Nombre AS NombreAnimal,
    a.Tipo AS TipoAnimal,
    a.Raza,
    -- Información del propietario
    p.IdPersona AS IdPropietario,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS NombrePropietario,
    p.Telefono AS TelefonoPropietario,
    p.Email AS EmailPropietario,
    -- Información del veterinario
    v.IdUsuario AS IdVeterinario,
    ISNULL(pv.Nombre + ' ' + ISNULL(pv.Apellidos, ''), v.NombreUsuario) AS NombreVeterinario,
    -- Información del usuario que registró
    ur.NombreUsuario AS UsuarioRegistro
FROM Cita c
    INNER JOIN Animal a ON c.IdAnimal = a.IdAnimal
    INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
    LEFT JOIN Usuario v ON c.IdVeterinario = v.IdUsuario
    LEFT JOIN Personal pv ON v.IdPersonal = pv.IdPersonal
    INNER JOIN Usuario ur ON c.IdUsuarioRegistro = ur.IdUsuario
WHERE c.Estado = 1 AND a.Estado = 1 AND p.Estado = 1;
GO

-- =============================================
-- STORED PROCEDURES
-- =============================================

-- Procedimiento para insertar cita
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_InsertarCita')
    DROP PROCEDURE SP_InsertarCita;
GO

CREATE PROCEDURE SP_InsertarCita
    @IdAnimal INT,
    @IdVeterinario INT = NULL,
    @IdUsuarioRegistro INT,
    @FechaHora DATETIME2,
    @TipoCita VARCHAR(50),
    @MotivoConsulta NVARCHAR(500),
    @Observaciones NVARCHAR(1000) = NULL,
    @TiempoEstimado INT = 30,
    @Urgente BIT = 0,
    @Costo DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validar que el animal existe y está activo
    IF NOT EXISTS (SELECT 1 FROM Animal WHERE IdAnimal = @IdAnimal AND Estado = 1)
    BEGIN
        SELECT 0 AS IdCita, 'El animal especificado no existe o está inactivo' AS Mensaje;
        RETURN;
    END
    
    -- Validar que el veterinario existe (si se especifica)
    IF @IdVeterinario IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdVeterinario AND Estado = 1)
    BEGIN
        SELECT 0 AS IdCita, 'El veterinario especificado no existe o está inactivo' AS Mensaje;
        RETURN;
    END
    
    -- Validar que la fecha no sea en el pasado (permitir mismo día)
    IF @FechaHora < CAST(GETDATE() AS DATE)
    BEGIN
        SELECT 0 AS IdCita, 'No se pueden programar citas en fechas pasadas' AS Mensaje;
        RETURN;
    END
    
    -- Validar disponibilidad del veterinario (si se especifica)
    IF @IdVeterinario IS NOT NULL
    BEGIN
        IF EXISTS (
            SELECT 1 FROM Cita 
            WHERE IdVeterinario = @IdVeterinario 
            AND Estado = 1 
            AND EstadoCita IN ('PROGRAMADA', 'CONFIRMADA', 'ENPROGRESO')
            AND ABS(DATEDIFF(MINUTE, FechaHora, @FechaHora)) < @TiempoEstimado
        )
        BEGIN
            SELECT 0 AS IdCita, 'El veterinario no está disponible en el horario solicitado' AS Mensaje;
            RETURN;
        END
    END
    
    -- Insertar la cita
    INSERT INTO Cita (
        IdAnimal, IdVeterinario, IdUsuarioRegistro, FechaHora, TipoCita,
        MotivoConsulta, Observaciones, TiempoEstimado, Urgente, Costo
    )
    VALUES (
        @IdAnimal, @IdVeterinario, @IdUsuarioRegistro, @FechaHora, @TipoCita,
        @MotivoConsulta, @Observaciones, @TiempoEstimado, @Urgente, @Costo
    );
    
    SELECT SCOPE_IDENTITY() AS IdCita, 'OK' AS Mensaje;
END;
GO

-- Procedimiento para actualizar estado de cita
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ActualizarEstadoCita')
    DROP PROCEDURE SP_ActualizarEstadoCita;
GO

CREATE PROCEDURE SP_ActualizarEstadoCita
    @IdCita INT,
    @NuevoEstado VARCHAR(20),
    @Observaciones NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Cita WHERE IdCita = @IdCita AND Estado = 1)
    BEGIN
        SELECT 0 AS Resultado, 'La cita especificada no existe' AS Mensaje;
        RETURN;
    END
    
    UPDATE Cita 
    SET EstadoCita = @NuevoEstado,
        Observaciones = ISNULL(@Observaciones, Observaciones),
        FechaModificacion = GETDATE()
    WHERE IdCita = @IdCita;
    
    SELECT 1 AS Resultado, 'OK' AS Mensaje;
END;
GO

-- Procedimiento para buscar citas por fecha
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_BuscarCitasPorFecha')
    DROP PROCEDURE SP_BuscarCitasPorFecha;
GO

CREATE PROCEDURE SP_BuscarCitasPorFecha
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdVeterinario INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * FROM VW_CitasCompletas
    WHERE CAST(FechaHora AS DATE) BETWEEN @FechaInicio AND @FechaFin
    AND (@IdVeterinario IS NULL OR IdVeterinario = @IdVeterinario)
    ORDER BY FechaHora;
END;
GO

-- Procedimiento para obtener citas del día
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_CitasDelDia')
    DROP PROCEDURE SP_CitasDelDia;
GO

CREATE PROCEDURE SP_CitasDelDia
    @Fecha DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SET @Fecha = ISNULL(@Fecha, CAST(GETDATE() AS DATE));
    
    SELECT * FROM VW_CitasCompletas
    WHERE CAST(FechaHora AS DATE) = @Fecha
    AND EstadoCita NOT IN ('CANCELADA')
    ORDER BY FechaHora;
END;
GO

PRINT 'Módulo de Citas creado exitosamente';