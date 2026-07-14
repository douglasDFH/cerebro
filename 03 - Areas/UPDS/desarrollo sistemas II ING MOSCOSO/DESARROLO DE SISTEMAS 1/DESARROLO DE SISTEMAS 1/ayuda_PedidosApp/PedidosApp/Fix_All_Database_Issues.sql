-- ===============================================================================
-- SCRIPT COMPLETO PARA CORREGIR TODOS LOS PROBLEMAS DE LA BASE DE DATOS
-- ===============================================================================

USE Bike_Store
GO

PRINT 'Iniciando corrección de base de datos...'

-- ===============================================================================
-- 1. CORREGIR ESTRUCTURA DE TABLAS
-- ===============================================================================

-- Verificar y corregir tabla products
IF EXISTS (SELECT * FROM sysobjects WHERE name='products' AND xtype='U')
BEGIN
    -- Agregar columna stock si no existe
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('products') AND name = 'stock')
    BEGIN
        ALTER TABLE products ADD stock INT NOT NULL DEFAULT 0
        PRINT 'Columna stock agregada a productos'
    END

    -- Agregar columna stock_minimo si no existe
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('products') AND name = 'stock_minimo')
    BEGIN
        ALTER TABLE products ADD stock_minimo INT NOT NULL DEFAULT 5
        PRINT 'Columna stock_minimo agregada a productos'
    END
END
GO

-- Verificar y corregir tabla users
IF EXISTS (SELECT * FROM sysobjects WHERE name='users' AND xtype='U')
BEGIN
    -- Agregar columna role_id si no existe
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'role_id')
    BEGIN
        ALTER TABLE users ADD role_id INT NOT NULL DEFAULT 3
        PRINT 'Columna role_id agregada a usuarios'
    END
END
GO

-- ===============================================================================
-- 2. CREAR TABLAS DEL SISTEMA DE ROLES
-- ===============================================================================

-- Tabla roles
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

-- Tabla permissions
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='permissions' AND xtype='U')
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
GO

-- Tabla role_permissions
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

-- Agregar foreign key de users a roles
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_users_roles')
BEGIN
    ALTER TABLE users ADD CONSTRAINT FK_users_roles FOREIGN KEY (role_id) REFERENCES roles(role_id)
    PRINT 'Foreign key FK_users_roles agregada'
END
GO

-- ===============================================================================
-- 3. INSERTAR DATOS BÁSICOS
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
GO

-- Insertar permisos si no existen
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

-- Limpiar y volver a asignar permisos
DELETE FROM role_permissions;

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
UPDATE users SET role_id = 1 WHERE usuario_name = 'admin';
UPDATE users SET stock = 50, stock_minimo = 10 WHERE EXISTS (SELECT 1 FROM products WHERE products.product_id = products.product_id AND (stock IS NULL OR stock = 0));

-- Actualizar productos con stock inicial si no tienen
UPDATE products SET stock = 50 WHERE stock = 0 OR stock IS NULL;
UPDATE products SET stock_minimo = 10 WHERE stock_minimo = 0 OR stock_minimo IS NULL;

PRINT 'Datos iniciales actualizados'
GO

-- ===============================================================================
-- 4. PROCEDIMIENTOS ALMACENADOS PARA STOCK
-- ===============================================================================

-- Procedimiento para actualizar stock
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_update_product_stock' AND type='P')
    DROP PROCEDURE sp_update_product_stock
GO

CREATE PROCEDURE sp_update_product_stock
    @product_id INT,
    @quantity INT,
    @operation VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF @operation = 'ADD'
        BEGIN
            UPDATE products 
            SET stock = stock + @quantity
            WHERE product_id = @product_id
        END
        ELSE IF @operation = 'SUBTRACT'
        BEGIN
            DECLARE @current_stock INT
            SELECT @current_stock = stock FROM products WHERE product_id = @product_id
            
            IF @current_stock >= @quantity
            BEGIN
                UPDATE products 
                SET stock = stock - @quantity
                WHERE product_id = @product_id
            END
            ELSE
            BEGIN
                RAISERROR('Stock insuficiente para el producto ID: %d. Stock actual: %d, Solicitado: %d', 16, 1, @product_id, @current_stock, @quantity)
                RETURN
            END
        END
        
        PRINT 'Stock actualizado correctamente'
    END TRY
    BEGIN CATCH
        PRINT 'Error al actualizar stock: ' + ERROR_MESSAGE()
        THROW
    END CATCH
END
GO

-- Procedimiento para verificar stock
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_check_product_stock' AND type='P')
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
    WHERE p.product_id = @product_id
END
GO

-- Procedimiento para productos con stock bajo
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_productos_stock_bajo' AND type='P')
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
    ORDER BY p.stock ASC, p.product_name
END
GO

-- ===============================================================================
-- 5. PROCEDIMIENTOS PARA ESTADOS DE PEDIDOS
-- ===============================================================================

-- Procedimiento para actualizar estado de pedido
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_update_order_status' AND type='P')
    DROP PROCEDURE sp_update_order_status
GO

CREATE PROCEDURE sp_update_order_status
    @order_id INT,
    @new_status TINYINT,
    @usuario_id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @current_status TINYINT
        SELECT @current_status = order_status FROM orders WHERE order_id = @order_id
        
        IF @current_status IS NULL
        BEGIN
            RAISERROR('Orden no encontrada', 16, 1)
            RETURN
        END
        
        -- Validar transiciones de estado válidas
        IF (@current_status = 1 AND @new_status IN (2, 3)) OR  -- Pendiente -> Procesando o Rechazado
           (@current_status = 2 AND @new_status IN (3, 4)) OR  -- Procesando -> Rechazado o Completado
           (@current_status = 3 AND @new_status = 1)           -- Rechazado -> Pendiente (reactivar)
        BEGIN
            UPDATE orders 
            SET order_status = @new_status,
                shipped_date = CASE WHEN @new_status = 4 THEN GETDATE() ELSE shipped_date END
            WHERE order_id = @order_id
            
            -- Si el pedido se rechaza, devolver stock a los productos
            IF @new_status = 3 AND @current_status != 3
            BEGIN
                UPDATE p SET stock = stock + oi.quantity
                FROM products p
                INNER JOIN order_items oi ON p.product_id = oi.product_id
                WHERE oi.order_id = @order_id
            END
            
            -- Si se reactiva un pedido rechazado, reducir stock nuevamente
            IF @new_status = 1 AND @current_status = 3
            BEGIN
                -- Verificar que hay suficiente stock para todos los productos
                IF EXISTS (
                    SELECT 1 FROM order_items oi
                    INNER JOIN products p ON oi.product_id = p.product_id
                    WHERE oi.order_id = @order_id AND p.stock < oi.quantity
                )
                BEGIN
                    RAISERROR('No hay suficiente stock para reactivar este pedido', 16, 1)
                    RETURN
                END
                
                -- Reducir stock
                UPDATE p SET stock = stock - oi.quantity
                FROM products p
                INNER JOIN order_items oi ON p.product_id = oi.product_id
                WHERE oi.order_id = @order_id
            END
            
            PRINT 'Estado de pedido actualizado correctamente'
        END
        ELSE
        BEGIN
            RAISERROR('Transición de estado no válida. Estado actual: %d, Nuevo estado: %d', 16, 1, @current_status, @new_status)
            RETURN
        END
    END TRY
    BEGIN CATCH
        PRINT 'Error al actualizar estado: ' + ERROR_MESSAGE()
        THROW
    END CATCH
END
GO

-- ===============================================================================
-- 6. PROCEDIMIENTOS PARA ROLES Y PERMISOS
-- ===============================================================================

-- Procedimiento para login con rol
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

-- Procedimiento para verificar permisos
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

-- ===============================================================================
-- 7. VISTAS ÚTILES
-- ===============================================================================

-- Vista para usuarios con roles
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

-- Vista para dashboard de estados
IF EXISTS (SELECT * FROM sysobjects WHERE name='vw_dashboard_estados_pedidos' AND type='V')
    DROP VIEW vw_dashboard_estados_pedidos
GO

CREATE VIEW vw_dashboard_estados_pedidos AS
SELECT 
    'Pendientes' as categoria,
    COUNT(*) as cantidad,
    ISNULL(SUM(total_order.total), 0) as valor_total
FROM orders o
LEFT JOIN (
    SELECT order_id, SUM(quantity * price * (1 - discount)) as total
    FROM order_items GROUP BY order_id
) total_order ON o.order_id = total_order.order_id
WHERE o.order_status = 1

UNION ALL

SELECT 
    'Procesando' as categoria,
    COUNT(*) as cantidad,
    ISNULL(SUM(total_order.total), 0) as valor_total
FROM orders o
LEFT JOIN (
    SELECT order_id, SUM(quantity * price * (1 - discount)) as total
    FROM order_items GROUP BY order_id
) total_order ON o.order_id = total_order.order_id
WHERE o.order_status = 2

UNION ALL

SELECT 
    'Completados Hoy' as categoria,
    COUNT(*) as cantidad,
    ISNULL(SUM(total_order.total), 0) as valor_total
FROM orders o
LEFT JOIN (
    SELECT order_id, SUM(quantity * price * (1 - discount)) as total
    FROM order_items GROUP BY order_id
) total_order ON o.order_id = total_order.order_id
WHERE o.order_status = 4 AND CAST(o.shipped_date AS DATE) = CAST(GETDATE() AS DATE)
GO

-- Actualizar el procedimiento de mostrar productos para incluir stock
IF EXISTS (SELECT * FROM sysobjects WHERE name='spmostrar_products' AND type='P')
    DROP PROCEDURE spmostrar_products
GO

CREATE PROCEDURE spmostrar_products
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.product_id, 
        p.product_name, 
        p.model_year, 
        p.price, 
        p.imagen,
        p.category_id, 
        p.stock, 
        p.stock_minimo,
        c.category_name AS category
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    ORDER BY p.product_name
END
GO

PRINT 'Base de datos corregida exitosamente!'
PRINT 'Tablas verificadas: products (con stock), users (con roles), roles, permissions, role_permissions'
PRINT 'Procedimientos creados: sp_update_product_stock, sp_check_product_stock, sp_productos_stock_bajo'
PRINT 'Procedimientos creados: sp_update_order_status, sp_login_with_role, sp_check_user_permission, sp_get_user_permissions'
PRINT 'Vistas creadas: vw_users_roles, vw_dashboard_estados_pedidos'
PRINT 'Sistema listo para usar!'