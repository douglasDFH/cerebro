-- =============================================
-- CORREGIR USUARIO ADMINISTRADOR
-- Descripción: Script para insertar/corregir usuario admin con hash correcto
-- =============================================

USE SistemaVeterinario;
GO

-- Eliminar usuario admin si existe
IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = 'admin')
BEGIN
    DELETE FROM Usuario WHERE NombreUsuario = 'admin';
    PRINT 'Usuario admin anterior eliminado';
END

-- Insertar usuario administrador con hash SHA256 correcto
-- Contraseña: admin123
-- Hash SHA256: 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
INSERT INTO Usuario (NombreUsuario, Clave, Email, Rol, Estado, IdPersonal)
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin@veterinaria.com', 'ADMIN', 1, NULL);

-- Verificar inserción
IF @@ROWCOUNT > 0
BEGIN
    PRINT '✓ Usuario administrador creado exitosamente';
    PRINT 'Usuario: admin';
    PRINT 'Contraseña: admin123';
    PRINT 'Email: admin@veterinaria.com';
    
    -- Mostrar información del usuario
    SELECT 
        IdUsuario,
        NombreUsuario,
        Email,
        Rol,
        Estado,
        FechaCreacion
    FROM Usuario 
    WHERE NombreUsuario = 'admin';
END
ELSE
BEGIN
    PRINT '✗ Error al crear el usuario administrador';
END

GO