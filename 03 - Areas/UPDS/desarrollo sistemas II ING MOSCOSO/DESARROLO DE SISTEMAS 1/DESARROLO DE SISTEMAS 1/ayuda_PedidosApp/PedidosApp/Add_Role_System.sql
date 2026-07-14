-- ===============================================================================
-- SISTEMA DE ROLES Y PERMISOS
-- ===============================================================================

USE Bike_Store
GO

-- Agregar columna role_id a la tabla users si no existe
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'role_id')
BEGIN
    ALTER TABLE users ADD role_id INT NOT NULL DEFAULT 3 -- Por defecto Employee
    PRINT 'Columna role_id agregada a la tabla users'
END
GO

-- Crear tabla de roles si no existe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='roles' AND xtype='U')
BEGIN
    CREATE TABLE roles (
        role_id INT IDENTITY(1,1) NOT NULL,
        role_name VARCHAR(50) NOT NULL,
        role_description VARCHAR(200) NULL,
        CONSTRAINT PK_roles PRIMARY KEY (role_id)
    )
    PRINT 'Tabla roles creada'
END
GO

-- Crear tabla de permisos si no existe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='permissions' AND xtype='U')
BEGIN
    CREATE TABLE permissions (
        permission_id INT IDENTITY(1,1) NOT NULL,
        permission_name VARCHAR(100) NOT NULL,
        permission_description VARCHAR(200) NULL,
        module_name VARCHAR(50) NOT NULL, -- Módulo al que pertenece (Orders, Products, Users, etc.)
        CONSTRAINT PK_permissions PRIMARY KEY (permission_id)
    )
    PRINT 'Tabla permissions creada'
END
GO

-- Crear tabla de relación roles-permisos si no existe
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='role_permissions' AND xtype='U')
BEGIN
    CREATE TABLE role_permissions (
        role_id INT NOT NULL,
        permission_id INT NOT NULL,
        CONSTRAINT PK_role_permissions PRIMARY KEY (role_id, permission_id),
        CONSTRAINT FK_role_permissions_roles FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE CASCADE,
        CONSTRAINT FK_role_permissions_permissions FOREIGN KEY (permission_id) REFERENCES permissions(permission_id) ON DELETE CASCADE
    )
    PRINT 'Tabla role_permissions creada'
END
GO

-- Agregar foreign key de users a roles si no existe
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_users_roles')
BEGIN
    ALTER TABLE users ADD CONSTRAINT FK_users_roles FOREIGN KEY (role_id) REFERENCES roles(role_id)
    PRINT 'Foreign key FK_users_roles agregada'
END
GO

-- Insertar roles básicos
IF NOT EXISTS (SELECT * FROM roles WHERE role_name = 'Administrator')
BEGIN
    INSERT INTO roles (role_name, role_description) VALUES 
    ('Administrator', 'Acceso completo al sistema'),
    ('Manager', 'Gestión de pedidos, productos y reportes'),
    ('Employee', 'Acceso básico para crear pedidos y ver productos')
    PRINT 'Roles básicos insertados'
END
GO

-- Insertar permisos básicos
IF NOT EXISTS (SELECT * FROM permissions WHERE permission_name = 'VIEW_ORDERS')
BEGIN
    INSERT INTO permissions (permission_name, permission_description, module_name) VALUES 
    -- Permisos de Pedidos
    ('VIEW_ORDERS', 'Ver lista de pedidos', 'Orders'),
    ('CREATE_ORDERS', 'Crear nuevos pedidos', 'Orders'),
    ('EDIT_ORDERS', 'Editar pedidos existentes', 'Orders'),
    ('DELETE_ORDERS', 'Eliminar pedidos', 'Orders'),
    ('MANAGE_ORDER_STATUS', 'Cambiar estados de pedidos', 'Orders'),
    
    -- Permisos de Productos
    ('VIEW_PRODUCTS', 'Ver lista de productos', 'Products'),
    ('CREATE_PRODUCTS', 'Crear nuevos productos', 'Products'),
    ('EDIT_PRODUCTS', 'Editar productos existentes', 'Products'),
    ('DELETE_PRODUCTS', 'Eliminar productos', 'Products'),
    ('MANAGE_STOCK', 'Gestionar inventario/stock', 'Products'),
    
    -- Permisos de Clientes
    ('VIEW_CUSTOMERS', 'Ver lista de clientes', 'Customers'),
    ('CREATE_CUSTOMERS', 'Crear nuevos clientes', 'Customers'),
    ('EDIT_CUSTOMERS', 'Editar clientes existentes', 'Customers'),
    ('DELETE_CUSTOMERS', 'Eliminar clientes', 'Customers'),
    
    -- Permisos de Usuarios
    ('VIEW_USERS', 'Ver lista de usuarios', 'Users'),
    ('CREATE_USERS', 'Crear nuevos usuarios', 'Users'),
    ('EDIT_USERS', 'Editar usuarios existentes', 'Users'),
    ('DELETE_USERS', 'Eliminar usuarios', 'Users'),
    
    -- Permisos de Reportes
    ('VIEW_REPORTS', 'Ver reportes básicos', 'Reports'),
    ('VIEW_ADVANCED_REPORTS', 'Ver reportes avanzados', 'Reports'),
    ('EXPORT_REPORTS', 'Exportar reportes', 'Reports'),
    
    -- Permisos de Configuración
    ('SYSTEM_CONFIG', 'Configurar sistema', 'System'),
    ('BACKUP_RESTORE', 'Respaldo y restauración', 'System')
    
    PRINT 'Permisos básicos insertados'
END
GO

-- Asignar permisos a roles
DELETE FROM role_permissions; -- Limpiar asignaciones existentes

-- Administrator: Todos los permisos
INSERT INTO role_permissions (role_id, permission_id)
SELECT 1, permission_id FROM permissions;

-- Manager: Permisos de gestión (sin configuración del sistema)
INSERT INTO role_permissions (role_id, permission_id)
SELECT 2, permission_id FROM permissions 
WHERE permission_name NOT IN ('SYSTEM_CONFIG', 'BACKUP_RESTORE', 'CREATE_USERS', 'DELETE_USERS');

-- Employee: Permisos básicos
INSERT INTO role_permissions (role_id, permission_id)
SELECT 3, permission_id FROM permissions 
WHERE permission_name IN ('VIEW_ORDERS', 'CREATE_ORDERS', 'VIEW_PRODUCTS', 'VIEW_CUSTOMERS', 'CREATE_CUSTOMERS', 'EDIT_CUSTOMERS', 'VIEW_REPORTS');

PRINT 'Permisos asignados a roles'
GO

-- Actualizar usuarios existentes con roles
UPDATE users SET role_id = 1 WHERE usuario_name = 'admin'; -- Administrator
-- Los demás usuarios quedan como Employee (3) por defecto

-- Procedimiento para verificar permisos de usuario
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_check_user_permission' AND type='P')
    DROP PROCEDURE sp_check_user_permission
GO

CREATE PROCEDURE sp_check_user_permission
    @usuario_id INT,
    @permission_name VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM users u
                INNER JOIN role_permissions rp ON u.role_id = rp.role_id
                INNER JOIN permissions p ON rp.permission_id = p.permission_id
                WHERE u.usuario_id = @usuario_id AND p.permission_name = @permission_name
            ) THEN 1 
            ELSE 0 
        END AS tiene_permiso
END
GO

-- Procedimiento para obtener permisos de usuario
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_get_user_permissions' AND type='P')
    DROP PROCEDURE sp_get_user_permissions
GO

CREATE PROCEDURE sp_get_user_permissions
    @usuario_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.usuario_id,
        u.usuario_name,
        r.role_name,
        r.role_description,
        p.permission_name,
        p.permission_description,
        p.module_name
    FROM users u
    INNER JOIN roles r ON u.role_id = r.role_id
    INNER JOIN role_permissions rp ON r.role_id = rp.role_id
    INNER JOIN permissions p ON rp.permission_id = p.permission_id
    WHERE u.usuario_id = @usuario_id
    ORDER BY p.module_name, p.permission_name
END
GO

-- Vista para información completa de usuarios con roles
IF EXISTS (SELECT * FROM sysobjects WHERE name='vw_users_roles' AND type='V')
    DROP VIEW vw_users_roles
GO

CREATE VIEW vw_users_roles AS
SELECT 
    u.usuario_id,
    u.usuario_name,
    u.usuario_email,
    r.role_id,
    r.role_name,
    r.role_description
FROM users u
INNER JOIN roles r ON u.role_id = r.role_id
GO

-- Procedimiento mejorado de login con información de rol
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_login_with_role' AND type='P')
    DROP PROCEDURE sp_login_with_role
GO

CREATE PROCEDURE sp_login_with_role
    @usuario VARCHAR(50),
    @clave VARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.usuario_id, 
        u.usuario_name, 
        u.usuario_email,
        r.role_id,
        r.role_name,
        r.role_description
    FROM users u
    INNER JOIN roles r ON u.role_id = r.role_id
    WHERE u.usuario_name = @usuario AND u.usuario_clave = @clave
END
GO

PRINT 'Sistema de roles y permisos implementado exitosamente'
PRINT 'Roles creados: Administrator, Manager, Employee'
PRINT 'Permisos organizados por módulos: Orders, Products, Customers, Users, Reports, System'
PRINT 'Procedimientos creados:'
PRINT '- sp_check_user_permission: Verificar permiso específico'
PRINT '- sp_get_user_permissions: Obtener todos los permisos de un usuario'
PRINT '- sp_login_with_role: Login con información de rol'