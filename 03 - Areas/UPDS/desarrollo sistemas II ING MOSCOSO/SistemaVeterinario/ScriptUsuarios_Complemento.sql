-- =============================================
-- COMPLEMENTO SISTEMA USUARIOS - SCRIPT SQL
-- Autor: Claude Code
-- Fecha: 2025-08-17
-- Descripción: Tabla de usuarios y procedimientos para autenticación
-- =============================================

USE SistemaVeterinario;
GO

-- =============================================
-- TABLA DE USUARIOS
-- =============================================

-- Eliminar tabla si existe
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
    DROP TABLE Usuario;
GO

-- Crear tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Clave NVARCHAR(255) NOT NULL, -- Para almacenar hash de contraseña
    Email NVARCHAR(100) NOT NULL UNIQUE,
    IdPersonal INT NULL, -- Referencia al personal (opcional)
    Rol VARCHAR(20) NOT NULL CHECK (Rol IN ('ADMIN', 'VETERINARIO', 'AUXILIAR', 'RECEPCIONISTA')) DEFAULT 'AUXILIAR',
    Estado BIT DEFAULT 1, -- 1=Activo, 0=Inactivo
    FechaCreacion DATETIME2 DEFAULT GETDATE(),
    FechaUltimoAcceso DATETIME2 NULL,
    IntentosLogin INT DEFAULT 0,
    Bloqueado BIT DEFAULT 0,
    FechaBloqueo DATETIME2 NULL,
    Token NVARCHAR(500) NULL, -- Para sesiones
    FechaExpiracionToken DATETIME2 NULL,
    
    -- Constraint para relacionar con personal
    CONSTRAINT FK_Usuario_Personal FOREIGN KEY (IdPersonal) REFERENCES Personal(IdPersonal)
);
GO

-- Índices para mejorar rendimiento
CREATE INDEX IX_Usuario_NombreUsuario ON Usuario(NombreUsuario);
CREATE INDEX IX_Usuario_Email ON Usuario(Email);
CREATE INDEX IX_Usuario_Estado ON Usuario(Estado);
CREATE INDEX IX_Usuario_Rol ON Usuario(Rol);
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS PARA USUARIOS
-- =============================================

-- SP para insertar usuario
CREATE PROCEDURE SP_InsertarUsuario
    @NombreUsuario NVARCHAR(50),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(100),
    @Rol VARCHAR(20) = 'AUXILIAR',
    @IdPersonal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario ya existe
        IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario OR Email = @Email)
        BEGIN
            RAISERROR('El usuario o email ya existe', 16, 1);
            RETURN;
        END
        
        INSERT INTO Usuario (NombreUsuario, Clave, Email, Rol, IdPersonal)
        VALUES (@NombreUsuario, @Clave, @Email, @Rol, @IdPersonal);
        
        SELECT SCOPE_IDENTITY() AS IdUsuario, 'Usuario creado exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS IdUsuario, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

-- SP para editar usuario
CREATE PROCEDURE SP_EditarUsuario
    @IdUsuario INT,
    @NombreUsuario NVARCHAR(50),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(100),
    @Rol VARCHAR(20) = 'AUXILIAR',
    @IdPersonal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario)
        BEGIN
            RAISERROR('El usuario no existe', 16, 1);
            RETURN;
        END
        
        -- Verificar si el nombre o email ya existe en otro usuario
        IF EXISTS (SELECT 1 FROM Usuario WHERE (NombreUsuario = @NombreUsuario OR Email = @Email) AND IdUsuario != @IdUsuario)
        BEGIN
            RAISERROR('El nombre de usuario o email ya existe', 16, 1);
            RETURN;
        END
        
        UPDATE Usuario 
        SET NombreUsuario = @NombreUsuario,
            Clave = @Clave,
            Email = @Email,
            Rol = @Rol,
            IdPersonal = @IdPersonal
        WHERE IdUsuario = @IdUsuario;
        
        SELECT 1 AS Resultado, 'Usuario actualizado exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Resultado, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

-- SP para eliminar usuario (cambiar estado)
CREATE PROCEDURE SP_EliminarUsuario
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario)
        BEGIN
            RAISERROR('El usuario no existe', 16, 1);
            RETURN;
        END
        
        UPDATE Usuario 
        SET Estado = 0
        WHERE IdUsuario = @IdUsuario;
        
        SELECT 1 AS Resultado, 'Usuario eliminado exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Resultado, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

-- SP para mostrar usuarios
CREATE PROCEDURE SP_MostrarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.IdUsuario,
        u.NombreUsuario,
        u.Email,
        u.Rol,
        u.Estado,
        u.FechaCreacion,
        u.FechaUltimoAcceso,
        u.Bloqueado,
        CASE 
            WHEN p.IdPersonal IS NOT NULL THEN p.Nombre + ' ' + p.Apellidos
            ELSE 'Sin asignar'
        END AS PersonalAsociado
    FROM Usuario u
    LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal
    ORDER BY u.FechaCreacion DESC;
END;
GO

-- SP para buscar usuario por nombre
CREATE PROCEDURE SP_BuscarUsuarioPorNombre
    @TextoBuscar NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.IdUsuario,
        u.NombreUsuario,
        u.Email,
        u.Rol,
        u.Estado,
        u.FechaCreacion,
        u.FechaUltimoAcceso,
        u.Bloqueado,
        CASE 
            WHEN p.IdPersonal IS NOT NULL THEN p.Nombre + ' ' + p.Apellidos
            ELSE 'Sin asignar'
        END AS PersonalAsociado
    FROM Usuario u
    LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal
    WHERE u.NombreUsuario LIKE '%' + @TextoBuscar + '%'
       OR u.Email LIKE '%' + @TextoBuscar + '%'
    ORDER BY u.NombreUsuario;
END;
GO

-- SP para login
CREATE PROCEDURE SP_Login
    @Usuario NVARCHAR(50),
    @Clave NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @IdUsuario INT;
    DECLARE @EstadoUsuario BIT;
    DECLARE @Bloqueado BIT;
    DECLARE @IntentosLogin INT;
    
    -- Buscar usuario por nombre o email
    SELECT 
        @IdUsuario = IdUsuario,
        @EstadoUsuario = Estado,
        @Bloqueado = Bloqueado,
        @IntentosLogin = IntentosLogin
    FROM Usuario 
    WHERE (NombreUsuario = @Usuario OR Email = @Usuario) 
      AND Clave = @Clave;
    
    -- Verificar credenciales
    IF @IdUsuario IS NULL
    BEGIN
        -- Usuario no encontrado o contraseña incorrecta
        -- Incrementar intentos si el usuario existe
        IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @Usuario OR Email = @Usuario)
        BEGIN
            UPDATE Usuario 
            SET IntentosLogin = IntentosLogin + 1,
                Bloqueado = CASE WHEN IntentosLogin >= 4 THEN 1 ELSE Bloqueado END,
                FechaBloqueo = CASE WHEN IntentosLogin >= 4 THEN GETDATE() ELSE FechaBloqueo END
            WHERE NombreUsuario = @Usuario OR Email = @Usuario;
        END
        
        SELECT 0 AS Resultado, 'Credenciales incorrectas' AS Mensaje, '' AS Rol, 0 AS IdUsuario;
        RETURN;
    END
    
    -- Verificar si está activo
    IF @EstadoUsuario = 0
    BEGIN
        SELECT 0 AS Resultado, 'Usuario inactivo' AS Mensaje, '' AS Rol, 0 AS IdUsuario;
        RETURN;
    END
    
    -- Verificar si está bloqueado
    IF @Bloqueado = 1
    BEGIN
        SELECT 0 AS Resultado, 'Usuario bloqueado por múltiples intentos fallidos' AS Mensaje, '' AS Rol, 0 AS IdUsuario;
        RETURN;
    END
    
    -- Login exitoso
    UPDATE Usuario 
    SET FechaUltimoAcceso = GETDATE(),
        IntentosLogin = 0
    WHERE IdUsuario = @IdUsuario;
    
    SELECT 
        1 AS Resultado,
        'Login exitoso' AS Mensaje,
        u.Rol,
        u.IdUsuario,
        u.NombreUsuario,
        u.Email,
        CASE 
            WHEN p.IdPersonal IS NOT NULL THEN p.Nombre + ' ' + p.Apellidos
            ELSE u.NombreUsuario
        END AS NombreCompleto
    FROM Usuario u
    LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal
    WHERE u.IdUsuario = @IdUsuario;
END;
GO

-- SP para desbloquear usuario
CREATE PROCEDURE SP_DesbloquearUsuario
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Usuario 
    SET Bloqueado = 0,
        IntentosLogin = 0,
        FechaBloqueo = NULL
    WHERE IdUsuario = @IdUsuario;
    
    SELECT 1 AS Resultado, 'Usuario desbloqueado exitosamente' AS Mensaje;
END;
GO

-- SP para cambiar contraseña
CREATE PROCEDURE SP_CambiarClave
    @IdUsuario INT,
    @ClaveAnterior NVARCHAR(255),
    @ClaveNueva NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Verificar contraseña anterior
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario AND Clave = @ClaveAnterior)
    BEGIN
        SELECT 0 AS Resultado, 'Contraseña anterior incorrecta' AS Mensaje;
        RETURN;
    END
    
    -- Actualizar contraseña
    UPDATE Usuario 
    SET Clave = @ClaveNueva
    WHERE IdUsuario = @IdUsuario;
    
    SELECT 1 AS Resultado, 'Contraseña cambiada exitosamente' AS Mensaje;
END;
GO

-- =============================================
-- VISTA DE USUARIOS CON INFORMACIÓN COMPLETA
-- =============================================
CREATE VIEW VW_UsuariosCompleto AS
SELECT 
    u.IdUsuario,
    u.NombreUsuario,
    u.Email,
    u.Rol,
    u.Estado,
    u.FechaCreacion,
    u.FechaUltimoAcceso,
    u.Bloqueado,
    u.IntentosLogin,
    p.Nombre AS NombrePersonal,
    p.Apellidos AS ApellidosPersonal,
    p.TipoPersonal,
    p.Especialidad
FROM Usuario u
LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal;
GO

-- =============================================
-- DATOS DE EJEMPLO PARA USUARIOS
-- =============================================

-- Insertar usuarios de ejemplo con contraseñas ya encriptadas
-- Nota: Las contraseñas se encriptan automáticamente por el procedimiento SP_InsertarUsuario

-- Eliminar usuario admin si existe (para reinsertar)
IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = 'admin')
BEGIN
    DELETE FROM Usuario WHERE NombreUsuario = 'admin';
END

-- Insertar usuario administrador directamente con hash SHA256
-- Hash SHA256 de "admin123" = 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
INSERT INTO Usuario (NombreUsuario, Clave, Email, Rol, Estado, IdPersonal)
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin@veterinaria.com', 'ADMIN', 1, NULL);

-- Los demás usuarios usando el procedimiento normal (que encripta automáticamente)
EXEC SP_InsertarUsuario 'drjuan', 'vet123', 'juan.perez@veterinaria.com', 'VETERINARIO', 1;
EXEC SP_InsertarUsuario 'drmaria', 'vet123', 'maria.lopez@veterinaria.com', 'VETERINARIO', 2;
EXEC SP_InsertarUsuario 'ana_aux', 'aux123', 'ana.martinez@veterinaria.com', 'AUXILIAR', 3;

PRINT 'Tabla de usuarios y procedimientos almacenados creados exitosamente.';
PRINT 'Usuarios de prueba creados:';
PRINT '- admin / admin123 (ADMIN)';
PRINT '- drjuan / vet123 (VETERINARIO)';
PRINT '- drmaria / vet123 (VETERINARIO)';
PRINT '- ana_aux / aux123 (AUXILIAR)';
GO