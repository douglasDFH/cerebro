-- ========================================
-- Base de Datos: gestion_usuarios
-- 4 Roles: Administrador, Desarrollador, Analista, Auditor
-- CON PRIVILEGIOS PRECISOS Y RESTRICCIONES ESPECÍFICAS
-- ========================================

-- Crear la base de datos gestion_usuarios (como especifica la consigna)
DROP DATABASE IF EXISTS gestion_usuarios;
CREATE DATABASE gestion_usuarios;
USE gestion_usuarios;

-- ========================================
-- DISEÑO DE LA BASE DE DATOS (Tarea del Desarrollador)
-- Al menos 3 tablas: usuarios, roles, permisos
-- ========================================

-- 1. Tabla roles (especificada en la consigna)
CREATE TABLE roles (
    id_rol INT AUTO_INCREMENT PRIMARY KEY,
    nombre_rol VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 2. Tabla permisos (especificada en la consigna)
CREATE TABLE permisos (
    id_permiso INT AUTO_INCREMENT PRIMARY KEY,
    nombre_permiso VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 3. Tabla usuarios (especificada en la consigna)
CREATE TABLE usuarios (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    contrasena VARCHAR(255),
    id_rol INT,
    estado ENUM('activo', 'inactivo') DEFAULT 'activo',
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_rol) REFERENCES roles(id_rol) ON DELETE SET NULL
);

-- 4. Tabla usuario_permiso (relación adicional solicitada al Desarrollador)
CREATE TABLE usuario_permiso (
    id_usuario INT,
    id_permiso INT,
    asignado_por VARCHAR(50),
    fecha_asignacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id_usuario, id_permiso),
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuario) ON DELETE CASCADE,
    FOREIGN KEY (id_permiso) REFERENCES permisos(id_permiso) ON DELETE CASCADE
);

-- 5. Tabla de auditoría (para el rol Auditor - detectar accesos no autorizados)
CREATE TABLE auditoria_logs (
    id_log INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(100),
    ip_origen VARCHAR(45),
    accion VARCHAR(100),
    tabla_afectada VARCHAR(100),
    fecha_hora TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    estado ENUM('exitoso', 'fallido') DEFAULT 'exitoso',
    detalles TEXT
);

-- 6. Tabla adicional (para generar errores controlados - solo ADMIN puede acceder)
CREATE TABLE configuracion_sistema (
    id_config INT AUTO_INCREMENT PRIMARY KEY,
    parametro VARCHAR(100) NOT NULL,
    valor VARCHAR(255),
    descripcion TEXT,
    fecha_modificacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ========================================
-- INSERTAR 15-20 REGISTROS POR TABLA (como especifica la consigna)
-- ========================================

-- Insertar roles (como ejemplifica la consigna: Admin, Editor, Lector)
INSERT INTO roles (nombre_rol, descripcion) VALUES 
('Administrador', 'Administrador con permisos completos del sistema'),
('Editor', 'Usuario que puede editar y modificar contenido'),
('Lector', 'Usuario con permisos de solo lectura'),
('Gerente', 'Gerente general de la empresa'),
('Supervisor', 'Supervisor de área'),
('Secretaria', 'Secretaria administrativa'),
('Contador', 'Contador de la empresa'),
('Vendedor', 'Vendedor de productos'),
('Cajero', 'Cajero de la empresa'),
('Almacenero', 'Encargado de almacén'),
('Recepcionista', 'Recepcionista de la empresa'),
('Técnico', 'Técnico especializado'),
('Asistente', 'Asistente administrativo'),
('Coordinador', 'Coordinador de proyectos'),
('Operador', 'Operador del sistema'),
('Auxiliar', 'Auxiliar de servicios'),
('Jefe_Ventas', 'Jefe del departamento de ventas'),
('Encargado_RRHH', 'Encargado de recursos humanos'),
('Analista_Datos', 'Analista de datos'),
('Programador', 'Programador de sistemas');

-- Insertar permisos (como ejemplifica la consigna: crear, leer, actualizar)
INSERT INTO permisos (nombre_permiso, descripcion) VALUES 
('crear', 'Crear nuevos registros en el sistema'),
('leer', 'Consultar y ver información'),
('actualizar', 'Modificar registros existentes'),
('eliminar', 'Eliminar registros del sistema'),
('administrar_usuarios', 'Gestionar usuarios del sistema'),
('generar_reportes', 'Crear reportes del sistema'),
('realizar_backup', 'Hacer copias de seguridad'),
('configurar_sistema', 'Configurar parámetros del sistema'),
('revisar_auditoria', 'Revisar logs de auditoría'),
('monitorear_sistema', 'Monitorear actividad del sistema'),
('exportar_datos', 'Exportar información a archivos'),
('importar_datos', 'Importar datos al sistema'),
('gestionar_roles', 'Administrar roles y permisos'),
('acceder_reportes', 'Acceso a reportes del sistema'),
('modificar_tablas', 'Modificar estructura de tablas'),
('ejecutar_consultas', 'Ejecutar consultas personalizadas'),
('ver_estadisticas', 'Ver estadísticas del sistema'),
('controlar_acceso', 'Controlar acceso de usuarios'),
('supervisar_procesos', 'Supervisar procesos del sistema'),
('gestionar_inventario', 'Gestionar inventario de productos');

-- Insertar usuarios (15-20 registros como especifica la consigna)
INSERT INTO usuarios (nombre, email, contrasena, id_rol) VALUES 
('Juan Carlos Mendoza', 'jmendoza@empresabolivia.com', 'pass001', 1),
('María Elena Vargas', 'mvargas@empresabolivia.com', 'pass002', 2),
('José Luis Quispe', 'jquispe@empresabolivia.com', 'pass003', 3),
('Ana Patricia Mamani', 'amamani@empresabolivia.com', 'pass004', 4),
('Luis Fernando Apaza', 'lapaza@empresabolivia.com', 'pass005', 5),
('Sofia Alejandra Condori', 'scondori@empresabolivia.com', 'pass006', 6),
('Diego Marcelo Flores', 'dflores@empresabolivia.com', 'pass007', 7),
('Carmen Rosa Choque', 'cchoque@empresabolivia.com', 'pass008', 8),
('Pedro Ramón Huanca', 'phuanca@empresabolivia.com', 'pass009', 9),
('Isabel Verónica Limachi', 'ilimachi@empresabolivia.com', 'pass010', 10),
('Roberto Carlos Ticona', 'rticona@empresabolivia.com', 'pass011', 11),
('Lucía Beatriz Calle', 'lcalle@empresabolivia.com', 'pass012', 12),
('Andrés Gonzalo Nina', 'anina@empresabolivia.com', 'pass013', 13),
('Valentina Esperanza Soto', 'vsoto@empresabolivia.com', 'pass014', 14),
('Gabriel Antonio Rojas', 'grojas@empresabolivia.com', 'pass015', 15),
('Daniela Fernanda Cruz', 'dcruz@empresabolivia.com', 'pass016', 16),
('Sebastián Eduardo Vega', 'svega@empresabolivia.com', 'pass017', 17),
('Camila Soledad Herrera', 'cherrera@empresabolivia.com', 'pass018', 18),
('Mateo Alejandro Pérez', 'mperez@empresabolivia.com', 'pass019', 19),
('Victoria Estefanía Morales', 'vmorales@empresabolivia.com', 'pass020', 20);

-- Insertar relaciones usuario_permiso (15-20 registros)
INSERT INTO usuario_permiso (id_usuario, id_permiso, asignado_por) VALUES 
(1, 1, 'administrador_sistema'), (1, 5, 'administrador_sistema'), (1, 19, 'administrador_sistema'),
(2, 1, 'juan_carlos'), (2, 2, 'juan_carlos'), (2, 3, 'juan_carlos'),
(3, 2, 'maria_elena'), (4, 2, 'maria_elena'), (4, 6, 'maria_elena'),
(5, 2, 'jose_luis'), (5, 6, 'jose_luis'), (6, 2, 'ana_patricia'),
(7, 2, 'luis_fernando'), (7, 11, 'luis_fernando'), (8, 2, 'sofia_alejandra'),
(8, 6, 'sofia_alejandra'), (8, 18, 'sofia_alejandra'), (9, 2, 'diego_marcelo'),
(9, 10, 'diego_marcelo'), (9, 15, 'diego_marcelo'), (10, 1, 'carmen_rosa'),
(10, 16, 'carmen_rosa');

-- Insertar registros de auditoría (para demostrar accesos no autorizados)
INSERT INTO auditoria_logs (usuario, ip_origen, accion, tabla_afectada, estado, detalles) VALUES 
('admin_user', '172.25.3.36', 'CREATE USER', 'mysql.user', 'exitoso', 'Usuario desarrollador creado correctamente'),
('DESARROLLADOR', '172.25.3.2', 'CREATE TABLE', 'gestion_usuarios.usuarios', 'exitoso', 'Tabla usuarios creada por el desarrollador'),
('ANALISTA', '172.25.3.20', 'SELECT', 'gestion_usuarios.usuarios', 'exitoso', 'Consulta de reportes ejecutada por analista'),
('AUDITOR', '172.25.3.34', 'SHOW GRANTS', 'mysql.db', 'exitoso', 'Verificación de permisos realizada por auditor'),
('DESARROLLADOR', '172.25.3.2', 'SELECT', 'gestion_usuarios.auditoria_logs', 'fallido', 'Acceso denegado - sin permisos para auditoría'),
('ANALISTA', '172.25.3.20', 'INSERT', 'gestion_usuarios.usuarios', 'fallido', 'Acceso denegado - solo tiene permisos SELECT'),
('AUDITOR', '172.25.3.34', 'UPDATE', 'gestion_usuarios.roles', 'fallido', 'Acceso denegado - auditor no puede modificar datos'),
('usuario_no_autorizado', '192.168.1.99', 'SELECT', 'gestion_usuarios.usuarios', 'fallido', 'Acceso denegado - IP no autorizada'),
('intento_hackeo', '10.0.0.50', 'DROP TABLE', 'gestion_usuarios.usuarios', 'fallido', 'Intento malicioso bloqueado');

-- Insertar configuración del sistema (solo ADMIN puede acceder)
INSERT INTO configuracion_sistema (parametro, valor, descripcion) VALUES 
('max_conexiones', '100', 'Número máximo de conexiones simultáneas'),
('timeout_sesion', '3600', 'Tiempo de expiración de sesión en segundos'),
('nivel_auditoria', 'completo', 'Nivel de detalle en logs de auditoría'),
('backup_automatico', 'true', 'Activar backups automáticos'),
('encriptacion_passwords', 'SHA256', 'Algoritmo de encriptación para contraseñas');

-- ========================================
-- CREACIÓN DE USUARIOS CON PRIVILEGIOS PRECISOS Y RESTRICCIONES
-- ========================================

-- ELIMINAR USUARIOS EXISTENTES (si existen)
DROP USER IF EXISTS 'ADMINISTRADOR'@'10.68.176.249';
DROP USER IF EXISTS 'DESARROLLADOR'@'192.168.112.1';
DROP USER IF EXISTS 'ANALISTA'@'10.68.176.30';
DROP USER IF EXISTS 'AUDITOR'@'172.25.3.34';


-- ========================================
-- 1. ADMINISTRADOR - IP: 172.25.3.36
-- Privilegios: CREATE USER, GRANT ALL, SHOW PROCESSLIST
-- ========================================
CREATE USER 'ADMINISTRADOR'@'172.25.3.36' IDENTIFIED BY 'Admin123';

-- Privilegios COMPLETOS sobre gestion_usuarios
GRANT ALL PRIVILEGES ON gestion_usuarios.* TO 'ADMINISTRADOR'@'172.25.3.36';

-- Privilegios específicos del ADMINISTRADOR
GRANT CREATE USER ON *.* TO 'ADMINISTRADOR'@'172.25.3.36';
GRANT GRANT OPTION ON *.* TO 'ADMINISTRADOR'@'172.25.3.36';
GRANT PROCESS ON *.* TO 'ADMINISTRADOR'@'172.25.3.36'; -- Para SHOW PROCESSLIST
GRANT SELECT, INSERT, UPDATE, DELETE ON mysql.user TO 'ADMINISTRADOR'@'172.25.3.36';
GRANT SELECT ON mysql.db TO 'ADMINISTRADOR'@'172.25.3.36';

-- ========================================
-- 2. DESARROLLADOR - IP: 172.25.3.2
-- Privilegios: CREATE TABLE, INSERT, SELECT, UPDATE
-- RESTRICCIÓN: Solo tablas usuarios, roles, permisos
-- ========================================
CREATE USER 'DESARROLLADOR'@'172.25.3.15' IDENTIFIED BY 'Desarrollador123';

-- Privilegios ESPECÍFICOS solo en las 3 tablas principales
GRANT CREATE ON gestion_usuarios.* TO 'DESARROLLADOR'@'172.25.3.15';
GRANT INSERT, SELECT, UPDATE ON gestion_usuarios.usuarios TO 'DESARROLLADOR'@'172.25.3.15';
GRANT INSERT, SELECT, UPDATE ON gestion_usuarios.roles TO 'DESARROLLADOR'@'172.25.3.15';
GRANT INSERT, SELECT, UPDATE ON gestion_usuarios.permisos TO 'DESARROLLADOR'@'172.25.3.15';
GRANT ALTER ON gestion_usuarios.usuarios TO 'DESARROLLADOR'@'172.25.3.15';
GRANT ALTER ON gestion_usuarios.roles TO 'DESARROLLADOR'@'172.25.3.15';
GRANT ALTER ON gestion_usuarios.permisos TO 'DESARROLLADOR'@'172.25.3.15';

-- NOTA: NO tiene acceso a usuario_permiso, auditoria_logs, configuracion_sistema

-- ========================================
-- 3. ANALISTA - IP: 172.25.3.20
-- Privilegios: SELECT con JOINs, funciones agregadas
-- RESTRICCIÓN: Solo SELECT, no puede modificar nada
-- ========================================
CREATE USER 'ANALISTA'@'172.25.3.20' IDENTIFIED BY 'Analista123';

-- Solo permisos de SELECT para generar reportes
GRANT SELECT ON gestion_usuarios.usuarios TO 'ANALISTA'@'172.25.3.20';
GRANT SELECT ON gestion_usuarios.roles TO 'ANALISTA'@'172.25.3.20';
GRANT SELECT ON gestion_usuarios.permisos TO 'ANALISTA'@'172.25.3.20';
GRANT SELECT ON gestion_usuarios.usuario_permiso TO 'ANALISTA'@'172.25.3.20';

-- NOTA: NO tiene acceso a auditoria_logs ni configuracion_sistema

-- ========================================
-- 4. AUDITOR - IP: 172.25.3.34
-- Privilegios: SHOW GRANTS, acceso a tablas de auditoría
-- FUNCIÓN: Detectar accesos no autorizados
-- ========================================
CREATE USER 'AUDITOR'@'172.25.3.34' IDENTIFIED BY 'Auditor123!';

-- Acceso de SOLO LECTURA a todas las tablas para auditoría
GRANT SELECT ON gestion_usuarios.* TO 'AUDITOR'@'172.25.3.34';

-- Privilegios especiales para auditoría de usuarios MySQL
GRANT PROCESS ON *.* TO 'AUDITOR'@'172.25.3.34';  -- Para SHOW GRANTS de otros usuarios
GRANT SELECT ON mysql.user TO 'AUDITOR'@'172.25.3.34';
GRANT SELECT ON mysql.db TO 'AUDITOR'@'172.25.3.34';
GRANT SELECT ON mysql.tables_priv TO 'AUDITOR'@'172.25.3.34';
GRANT SELECT ON mysql.columns_priv TO 'AUDITOR'@'172.25.3.34';
GRANT PROCESS ON *.* TO 'AUDITOR'@'172.25.3.34';
GRANT SELECT ON mysql.* TO 'AUDITOR'@'172.25.3.34';

-- Aplicar todos los cambios de privilegios
FLUSH PRIVILEGES;

-- ========================================
-- VERIFICACIONES Y PRUEBAS DE ERRORES CONTROLADOS
-- ========================================

SHOW GRANTS FOR 'ANALISTA'@'172.25.3.20';

SHOW PROCESSLIST;

CREATE USER 'usuario_prueba'@'192.168.1.100' IDENTIFIED BY 'test123';

GRANT SELECT ON gestion_usuarios.* TO 'usuario_prueba'@'192.168.1.100';

SELECT * FROM gestion_usuarios.configuracion_sistema;

DROP USER 'usuario_prueba'@'192.168.1.100';

SHOW GRANTS FOR 'usuario_prueba'@'192.168.1.100';



















-- Verificar que todos los usuarios fueron creados correctamente
SELECT User, Host FROM mysql.user WHERE User IN ('ADMINISTRADOR', 'DESARROLLADOR', 'ANALISTA', 'AUDITOR');

-- Verificar registros en todas las tablas
SELECT 'roles' AS tabla, COUNT(*) AS registros FROM roles
UNION ALL SELECT 'permisos', COUNT(*) FROM permisos  
UNION ALL SELECT 'usuarios', COUNT(*) FROM usuarios
UNION ALL SELECT 'usuario_permiso', COUNT(*) FROM usuario_permiso
UNION ALL SELECT 'auditoria_logs', COUNT(*) FROM auditoria_logs
UNION ALL SELECT 'configuracion_sistema', COUNT(*) FROM configuracion_sistema;

-- ========================================
-- CONSULTAS ESPECÍFICAS POR ROL
-- ========================================

-- ===== CONSULTAS PARA EL ADMINISTRADOR =====
/*
-- El ADMINISTRADOR puede hacer todo:
SHOW PROCESSLIST;
CREATE USER 'usuario_prueba'@'192.168.1.100' IDENTIFIED BY 'test123';
GRANT SELECT ON gestion_usuarios.* TO 'usuario_prueba'@'192.168.1.100';
SELECT * FROM gestion_usuarios.configuracion_sistema;
DROP USER 'usuario_prueba'@'192.168.1.100';
*/

-- ===== CONSULTAS PARA EL DESARROLLADOR =====
/*
-- El DESARROLLADOR puede trabajar solo con usuarios, roles, permisos:
SELECT * FROM gestion_usuarios.usuarios;
INSERT INTO usuarios (nombre, email, contrasena, id_rol) VALUES ('Test User', 'test@test.com', 'pass123', 1);
UPDATE usuarios SET estado = 'inactivo' WHERE id_usuario = 21;

-- ERROR ESPERADO - no puede acceder a auditoria_logs:
SELECT * FROM gestion_usuarios.auditoria_logs; -- Error: Access denied

-- ERROR ESPERADO - no puede acceder a configuracion_sistema:
SELECT * FROM gestion_usuarios.configuracion_sistema; -- Error: Access denied
*/

-- ===== CONSULTAS PARA EL ANALISTA =====
/*
-- El ANALISTA puede generar reportes complejos:
SELECT r.nombre_rol, COUNT(u.id_usuario) AS total_usuarios
FROM roles r 
LEFT JOIN usuarios u ON r.id_rol = u.id_rol 
GROUP BY r.id_rol, r.nombre_rol 
ORDER BY total_usuarios DESC;

SELECT p.nombre_permiso, COUNT(up.id_usuario) AS usuarios_con_permiso
FROM permisos p
LEFT JOIN usuario_permiso up ON p.id_permiso = up.id_permiso
GROUP BY p.id_permiso, p.nombre_permiso
ORDER BY usuarios_con_permiso DESC;

-- ERROR ESPERADO - no puede insertar datos:
INSERT INTO usuarios (nombre, email) VALUES ('Test', 'test@test.com'); -- Error: Access denied

-- ERROR ESPERADO - no puede acceder a logs de auditoría:
SELECT * FROM gestion_usuarios.auditoria_logs; -- Error: Access denied
*/

-- ===== CONSULTAS PARA EL AUDITOR =====
/*
-- El AUDITOR puede revisar permisos y detectar anomalías:
SHOW GRANTS FOR 'ADMINISTRADOR'@'172.25.3.36';
SHOW GRANTS FOR 'DESARROLLADOR'@'172.25.3.2';
SHOW GRANTS FOR 'ANALISTA'@'172.25.3.20';

-- Detectar accesos fallidos:
SELECT usuario, ip_origen, COUNT(*) as intentos_fallidos, MAX(fecha_hora) as ultimo_intento
FROM auditoria_logs 
WHERE estado = 'fallido' 
GROUP BY usuario, ip_origen 
ORDER BY intentos_fallidos DESC;

-- Revisar usuarios MySQL sospechosos:
SELECT User, Host, account_locked, password_expired FROM mysql.user 
WHERE User NOT IN ('ADMINISTRADOR', 'DESARROLLADOR', 'ANALISTA', 'AUDITOR', 'root', 'mysql.session', 'mysql.sys');

-- ERROR ESPERADO - no puede modificar datos:
UPDATE auditoria_logs SET estado = 'revisado' WHERE id_log = 1; -- Error: Access denied
*/

-- ========================================
-- MOSTRAR GRANTS DE TODOS LOS USUARIOS PARA VERIFICACIÓN
-- ========================================
SHOW GRANTS FOR 'ADMINISTRADOR'@'172.25.3.36';
SHOW GRANTS FOR 'DESARROLLADOR'@'172.25.3.2';
SHOW GRANTS FOR 'ANALISTA'@'172.25.3.20';
SHOW GRANTS FOR 'AUDITOR'@'172.25.3.34';