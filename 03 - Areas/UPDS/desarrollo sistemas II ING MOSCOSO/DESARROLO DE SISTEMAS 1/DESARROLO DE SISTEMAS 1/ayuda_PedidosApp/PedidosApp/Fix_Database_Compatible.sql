-- ===============================================================================
-- SCRIPT COMPATIBLE PARA CORREGIR BASE DE DATOS - PEDIDOS APP
-- Compatible con SQL Server 2008 y versiones superiores
-- ===============================================================================

USE Bike_Store
GO

PRINT 'Iniciando corrección de base de datos (versión compatible)...'

-- ===============================================================================
-- 1. VERIFICAR Y AGREGAR COLUMNAS A PRODUCTS
-- ===============================================================================

-- Verificar si la columna stock existe
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'products' AND COLUMN_NAME = 'stock')
BEGIN
    ALTER TABLE products ADD stock INT NOT NULL DEFAULT 0
    PRINT 'Columna stock agregada a la tabla products'
END
ELSE
BEGIN
    PRINT 'Columna stock ya existe en la tabla products'
END

-- Verificar si la columna stock_minimo existe
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'products' AND COLUMN_NAME = 'stock_minimo')
BEGIN
    ALTER TABLE products ADD stock_minimo INT NOT NULL DEFAULT 5
    PRINT 'Columna stock_minimo agregada a la tabla products'
END
ELSE
BEGIN
    PRINT 'Columna stock_minimo ya existe en la tabla products'
END

-- ===============================================================================
-- 2. VERIFICAR Y AGREGAR COLUMNA ROLE_ID A USERS
-- ===============================================================================

-- Verificar si la columna role_id existe
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'users' AND COLUMN_NAME = 'role_id')
BEGIN
    ALTER TABLE users ADD role_id INT NOT NULL DEFAULT 3
    PRINT 'Columna role_id agregada a la tabla users'
END
ELSE
BEGIN
    PRINT 'Columna role_id ya existe en la tabla users'
END

-- ===============================================================================
-- 3. CREAR TABLAS DEL SISTEMA DE ROLES
-- ===============================================================================

-- Tabla roles
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'roles')
BEGIN
    CREATE TABLE roles (
        role_id INT IDENTITY(1,1) NOT NULL,
        role_name VARCHAR(50) NOT NULL,
        role_description VARCHAR(200) NULL,
        CONSTRAINT PK_roles PRIMARY KEY (role_id)
    )
    PRINT 'Tabla roles creada'
END
ELSE
BEGIN
    PRINT 'Tabla roles ya existe'
END

-- Tabla permissions
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'permissions')
BEGIN
    CREATE TABLE permissions (
        permission_id INT IDENTITY(1,1) NOT NULL,
        permission_name VARCHAR(100) NOT NULL,
        permission_description VARCHAR(200) NULL,
        module_name VARCHAR(50) NOT NULL,
        CONSTRAINT PK_permissions PRIMARY KEY (permission_id)
    )
    PRINT 'Tabla permissions creada'
END
ELSE
BEGIN
    PRINT 'Tabla permissions ya existe'
END

-- Tabla role_permissions
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'role_permissions')
BEGIN
    CREATE TABLE role_permissions (
        role_id INT NOT NULL,
        permission_id INT NOT NULL,
        CONSTRAINT PK_role_permissions PRIMARY KEY (role_id, permission_id)
    )
    PRINT 'Tabla role_permissions creada'
END
ELSE
BEGIN
    PRINT 'Tabla role_permissions ya existe'
END

-- ===============================================================================
-- 4. AGREGAR FOREIGN KEYS SI NO EXISTEN
-- ===============================================================================

-- FK de role_permissions a roles
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_role_permissions_roles')
BEGIN
    ALTER TABLE role_permissions ADD CONSTRAINT FK_role_permissions_roles 
    FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE CASCADE
    PRINT 'FK_role_permissions_roles agregada'
END

-- FK de role_permissions a permissions
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_role_permissions_permissions')
BEGIN
    ALTER TABLE role_permissions ADD CONSTRAINT FK_role_permissions_permissions 
    FOREIGN KEY (permission_id) REFERENCES permissions(permission_id) ON DELETE CASCADE
    PRINT 'FK_role_permissions_permissions agregada'
END

-- FK de users a roles
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_users_roles')
BEGIN
    ALTER TABLE users ADD CONSTRAINT FK_users_roles 
    FOREIGN KEY (role_id) REFERENCES roles(role_id)
    PRINT 'FK_users_roles agregada'
END

-- ===============================================================================
-- 5. INSERTAR DATOS BÁSICOS
-- ===============================================================================

-- Insertar roles si no existen
IF NOT EXISTS (SELECT * FROM roles WHERE role_name = 'Administrator')
BEGIN
    INSERT INTO roles (role_name, role_description) VALUES 
    ('Administrator', 'Acceso completo al sistema'),
    ('Manager', 'Gestión de pedidos, productos y reportes'),
    ('Employee', 'Acceso básico para crear pedidos y ver productos')
    PRINT 'Roles básicos insertados'
END
ELSE
BEGIN
    PRINT 'Roles ya existen'
END

-- Insertar permisos si no existen
IF NOT EXISTS (SELECT * FROM permissions WHERE permission_name = 'VIEW_ORDERS')
BEGIN
    INSERT INTO permissions (permission_name, permission_description, module_name) VALUES 
    ('VIEW_ORDERS', 'Ver lista de pedidos', 'Orders'),
    ('CREATE_ORDERS', 'Crear nuevos pedidos', 'Orders'),
    ('EDIT_ORDERS', 'Editar pedidos existentes', 'Orders'),
    ('DELETE_ORDERS', 'Eliminar pedidos', 'Orders'),
    ('MANAGE_ORDER_STATUS', 'Cambiar estados de pedidos', 'Orders'),
    ('VIEW_PRODUCTS', 'Ver lista de productos', 'Products'),
    ('CREATE_PRODUCTS', 'Crear nuevos productos', 'Products'),
    ('EDIT_PRODUCTS', 'Editar productos existentes', 'Products'),
    ('DELETE_PRODUCTS', 'Eliminar productos', 'Products'),
    ('MANAGE_STOCK', 'Gestionar inventario/stock', 'Products'),
    ('VIEW_CUSTOMERS', 'Ver lista de clientes', 'Customers'),
    ('CREATE_CUSTOMERS', 'Crear nuevos clientes', 'Customers'),
    ('EDIT_CUSTOMERS', 'Editar clientes existentes', 'Customers'),
    ('DELETE_CUSTOMERS', 'Eliminar clientes', 'Customers'),
    ('VIEW_USERS', 'Ver lista de usuarios', 'Users'),
    ('CREATE_USERS', 'Crear nuevos usuarios', 'Users'),
    ('EDIT_USERS', 'Editar usuarios existentes', 'Users'),
    ('DELETE_USERS', 'Eliminar usuarios', 'Users'),
    ('VIEW_REPORTS', 'Ver reportes básicos', 'Reports'),
    ('VIEW_ADVANCED_REPORTS', 'Ver reportes avanzados', 'Reports'),
    ('EXPORT_REPORTS', 'Exportar reportes', 'Reports'),
    ('SYSTEM_CONFIG', 'Configurar sistema', 'System'),
    ('BACKUP_RESTORE', 'Respaldo y restauración', 'System')
    PRINT 'Permisos básicos insertados'
END
ELSE
BEGIN
    PRINT 'Permisos ya existen'
END

-- Limpiar y asignar permisos
DELETE FROM role_permissions;

-- Administrator: Todos los permisos
INSERT INTO role_permissions (role_id, permission_id)
SELECT 1, permission_id FROM permissions;

-- Manager: Sin permisos de sistema
INSERT INTO role_permissions (role_id, permission_id)
SELECT 2, permission_id FROM permissions 
WHERE permission_name NOT IN ('SYSTEM_CONFIG', 'BACKUP_RESTORE', 'CREATE_USERS', 'DELETE_USERS');

-- Employee: Permisos básicos
INSERT INTO role_permissions (role_id, permission_id)
SELECT 3, permission_id FROM permissions 
WHERE permission_name IN ('VIEW_ORDERS', 'CREATE_ORDERS', 'VIEW_PRODUCTS', 'VIEW_CUSTOMERS', 'CREATE_CUSTOMERS', 'EDIT_CUSTOMERS', 'VIEW_REPORTS');

PRINT 'Permisos asignados a roles'

-- Actualizar datos existentes
UPDATE users SET role_id = 1 WHERE usuario_name = 'admin';
UPDATE products SET stock = 50 WHERE stock = 0 OR stock IS NULL;
UPDATE products SET stock_minimo = 10 WHERE stock_minimo = 0 OR stock_minimo IS NULL;

PRINT 'Datos actualizados'

-- ===============================================================================
-- 6. PROCEDIMIENTOS ALMACENADOS (Compatible con SQL 2008+)
-- ===============================================================================

-- Procedimiento para actualizar stock (compatible)
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_update_product_stock')
    DROP PROCEDURE sp_update_product_stock
GO

CREATE PROCEDURE sp_update_product_stock
    @product_id INT,
    @quantity INT,
    @operation VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @current_stock INT;
    DECLARE @error_msg VARCHAR(500);
    
    BEGIN TRY
        IF @operation = 'ADD'
        BEGIN
            UPDATE products 
            SET stock = stock + @quantity
            WHERE product_id = @product_id;
            
            PRINT 'Stock aumentado correctamente';
        END
        ELSE IF @operation = 'SUBTRACT'
        BEGIN
            SELECT @current_stock = stock FROM products WHERE product_id = @product_id;
            
            IF @current_stock >= @quantity
            BEGIN
                UPDATE products 
                SET stock = stock - @quantity
                WHERE product_id = @product_id;
                
                PRINT 'Stock reducido correctamente';
            END
            ELSE
            BEGIN
                SET @error_msg = 'Stock insuficiente para el producto ID: ' + CAST(@product_id AS VARCHAR) + 
                                '. Stock actual: ' + CAST(@current_stock AS VARCHAR) + 
                                ', Solicitado: ' + CAST(@quantity AS VARCHAR);
                RAISERROR(@error_msg, 16, 1);
                RETURN;
            END
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage VARCHAR(500) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- Procedimiento para verificar stock
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_check_product_stock')
    DROP PROCEDURE sp_check_product_stock
GO

CREATE PROCEDURE sp_check_product_stock
    @product_id INT,
    @required_quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.product_id,
        p.product_name,
        p.stock,
        p.stock_minimo,
        CASE 
            WHEN p.stock >= @required_quantity THEN 'DISPONIBLE'
            ELSE 'INSUFICIENTE'
        END AS disponibilidad,
        (p.stock - @required_quantity) AS stock_restante
    FROM products p
    WHERE p.product_id = @product_id;
END
GO

-- Procedimiento para productos con stock bajo
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_productos_stock_bajo')
    DROP PROCEDURE sp_productos_stock_bajo
GO

CREATE PROCEDURE sp_productos_stock_bajo
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.product_id,
        p.product_name,
        c.category_name,
        p.stock,
        p.stock_minimo,
        p.price
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    WHERE p.stock <= p.stock_minimo
    ORDER BY p.stock ASC, p.product_name;
END
GO

-- Procedimiento para login con rol
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_login_with_role')
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
    WHERE u.usuario_name = @usuario AND u.usuario_clave = @clave;
END
GO

-- Procedimiento para verificar permisos
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_check_user_permission')
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
        END AS tiene_permiso;
END
GO

-- Procedimiento para obtener permisos de usuario
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_get_user_permissions')
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
    ORDER BY p.module_name, p.permission_name;
END
GO

-- Procedimiento para actualizar estado de pedido (compatible)
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_update_order_status')
    DROP PROCEDURE sp_update_order_status
GO

CREATE PROCEDURE sp_update_order_status
    @order_id INT,
    @new_status TINYINT,
    @usuario_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @current_status TINYINT;
    DECLARE @error_msg VARCHAR(500);
    
    BEGIN TRY
        SELECT @current_status = order_status FROM orders WHERE order_id = @order_id;
        
        IF @current_status IS NULL
        BEGIN
            RAISERROR('Orden no encontrada', 16, 1);
            RETURN;
        END
        
        -- Validar transiciones válidas
        IF (@current_status = 1 AND @new_status IN (2, 3)) OR  
           (@current_status = 2 AND @new_status IN (3, 4)) OR  
           (@current_status = 3 AND @new_status = 1)
        BEGIN
            UPDATE orders 
            SET order_status = @new_status,
                shipped_date = CASE WHEN @new_status = 4 THEN GETDATE() ELSE shipped_date END
            WHERE order_id = @order_id;
            
            -- Manejo de stock según estado
            IF @new_status = 3 AND @current_status != 3
            BEGIN
                UPDATE p SET stock = stock + oi.quantity
                FROM products p
                INNER JOIN order_items oi ON p.product_id = oi.product_id
                WHERE oi.order_id = @order_id;
            END
            
            IF @new_status = 1 AND @current_status = 3
            BEGIN
                IF EXISTS (
                    SELECT 1 FROM order_items oi
                    INNER JOIN products p ON oi.product_id = p.product_id
                    WHERE oi.order_id = @order_id AND p.stock < oi.quantity
                )
                BEGIN
                    RAISERROR('No hay suficiente stock para reactivar este pedido', 16, 1);
                    RETURN;
                END
                
                UPDATE p SET stock = stock - oi.quantity
                FROM products p
                INNER JOIN order_items oi ON p.product_id = oi.product_id
                WHERE oi.order_id = @order_id;
            END
            
            PRINT 'Estado actualizado correctamente';
        END
        ELSE
        BEGIN
            SET @error_msg = 'Transición no válida. Estado actual: ' + CAST(@current_status AS VARCHAR) + 
                            ', Nuevo estado: ' + CAST(@new_status AS VARCHAR);
            RAISERROR(@error_msg, 16, 1);
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage VARCHAR(500) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END
GO

-- ===============================================================================
-- 7. CREAR VISTAS
-- ===============================================================================

-- Vista para usuarios con roles
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'vw_users_roles')
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

PRINT '========================================='
PRINT 'BASE DE DATOS ACTUALIZADA EXITOSAMENTE!'
PRINT '========================================='
PRINT 'Tablas verificadas:'
PRINT '- products (con columnas stock y stock_minimo)'
PRINT '- users (con columna role_id)'
PRINT '- roles, permissions, role_permissions'
PRINT ''
PRINT 'Procedimientos creados:'
PRINT '- sp_update_product_stock'
PRINT '- sp_check_product_stock'
PRINT '- sp_productos_stock_bajo'
PRINT '- sp_login_with_role'
PRINT '- sp_check_user_permission'
PRINT '- sp_get_user_permissions'
PRINT '- sp_update_order_status'
PRINT ''
PRINT 'Vistas creadas:'
PRINT '- vw_users_roles'
PRINT ''
PRINT 'Usuario admin configurado con rol Administrator'
PRINT 'Productos actualizados con stock inicial'
PRINT 'Sistema listo para usar!'