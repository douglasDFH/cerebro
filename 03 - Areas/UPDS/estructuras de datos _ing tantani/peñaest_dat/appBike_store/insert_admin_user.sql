-- Script para insertar el usuario administrador con contraseña hasheada correctamente
-- Ejecutar este script DESPUÉS de crear la base de datos principal

USE bike_store;

-- Eliminar el usuario admin temporal si existe
DELETE FROM usuarios WHERE usuario = 'admin';

-- Insertar el usuario administrador con contraseña hasheada
-- Contraseña: admin123
-- Hash generado con PASSWORD_DEFAULT de PHP
INSERT INTO usuarios (usuario, password, email, rol_id) VALUES
('admin', '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'admin@bikestore.com', 1);

-- Verificar que el usuario se creó correctamente
SELECT u.user_id, u.usuario, u.email, r.nombre_rol 
FROM usuarios u 
INNER JOIN roles r ON u.rol_id = r.rol_id 
WHERE u.usuario = 'admin';

-- Información de login:
-- Usuario: admin
-- Contraseña: admin123
-- ¡IMPORTANTE: Cambiar esta contraseña después del primer login!