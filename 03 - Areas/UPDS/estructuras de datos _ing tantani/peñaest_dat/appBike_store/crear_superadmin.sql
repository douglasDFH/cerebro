-- Script para crear el usuario superadmin
-- Ejecutar en phpMyAdmin en la base de datos appbike_store

USE appbike_store;

-- Insertar el nuevo usuario superadmin
-- Usuario: superadmin
-- Contraseña: admin123
-- Rol: administrador (rol_id = 1)
INSERT INTO usuarios (usuario, password, email, rol_id, activo) VALUES
('superadmin', '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'superadmin@bikestore.com', 1, 1);

-- Verificar que el usuario se creó correctamente
SELECT 
    u.user_id,
    u.usuario,
    u.email,
    r.nombre_rol,
    u.activo,
    u.fecha_creacion
FROM usuarios u
INNER JOIN roles r ON u.rol_id = r.rol_id
WHERE u.usuario = 'superadmin';

-- Mostrar todos los usuarios administrativos
SELECT 
    u.usuario,
    u.email,
    r.nombre_rol as rol
FROM usuarios u
INNER JOIN roles r ON u.rol_id = r.rol_id
WHERE r.nombre_rol = 'administrador'
ORDER BY u.fecha_creacion DESC;