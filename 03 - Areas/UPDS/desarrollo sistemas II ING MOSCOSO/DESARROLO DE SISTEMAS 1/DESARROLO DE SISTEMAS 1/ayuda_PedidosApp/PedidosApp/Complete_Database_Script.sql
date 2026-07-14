-- ===============================================================================
-- SCRIPT COMPLETO DE BASE DE DATOS PARA PEDIDOS APP (BIKE STORE)
-- Sistema de Gestion de Pedidos desarrollado por Jorge Saucedo
-- Base de datos: Bike_Store
-- ===============================================================================

USE master
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Bike_Store')
BEGIN
    CREATE DATABASE Bike_Store
END
GO

USE Bike_Store
GO

-- ===============================================================================
-- CREACIÓN DE TABLAS
-- ===============================================================================

-- Tabla: categories (Categorias de productos)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='categories' AND xtype='U')
BEGIN
    CREATE TABLE categories (
        category_id INT IDENTITY(1,1) NOT NULL,
        category_name VARCHAR(255) NOT NULL,
        CONSTRAINT PK_categories PRIMARY KEY (category_id)
    )
END
GO

-- Tabla: products (Productos)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='products' AND xtype='U')
BEGIN
    CREATE TABLE products (
        product_id INT IDENTITY(1,1) NOT NULL,
        product_name VARCHAR(200) NOT NULL,
        model_year SMALLINT NOT NULL,
        price MONEY NOT NULL,
        imagen IMAGE NULL,
        category_id INT NOT NULL,
        CONSTRAINT PK_products PRIMARY KEY (product_id),
        CONSTRAINT FK_products_categories FOREIGN KEY (category_id) REFERENCES categories(category_id)
    )
END
GO

-- Tabla: customers (Clientes)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='customers' AND xtype='U')
BEGIN
    CREATE TABLE customers (
        customer_id INT IDENTITY(1,1) NOT NULL,
        first_name VARCHAR(255) NOT NULL,
        last_name VARCHAR(255) NOT NULL,
        phone VARCHAR(25) NULL,
        email VARCHAR(255) NOT NULL,
        street VARCHAR(255) NULL,
        city VARCHAR(50) NULL,
        state VARCHAR(25) NULL,
        CONSTRAINT PK_customers PRIMARY KEY (customer_id)
    )
END
GO

-- Tabla: users (Usuarios del sistema)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='users' AND xtype='U')
BEGIN
    CREATE TABLE users (
        usuario_id INT IDENTITY(1,1) NOT NULL,
        usuario_name VARCHAR(50) NOT NULL,
        usuario_clave VARCHAR(250) NOT NULL,
        usuario_email VARCHAR(100) NOT NULL,
        CONSTRAINT PK_users PRIMARY KEY (usuario_id)
    )
END
ELSE
BEGIN
    -- Si la tabla existe, verificar si la columna existe con el nombre correcto
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_name')
    BEGIN
        -- Si no existe usuario_name, verificar si existe 'name' y renombrarla
        IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'name')
        BEGIN
            EXEC sp_rename 'users.name', 'usuario_name', 'COLUMN'
        END
        ELSE
        BEGIN
            -- Si no existe ninguna, agregarla
            ALTER TABLE users ADD usuario_name VARCHAR(50) NOT NULL DEFAULT ''
        END
    END
END
GO

-- Tabla: stores (Tiendas)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='stores' AND xtype='U')
BEGIN
    CREATE TABLE stores (
        store_id INT IDENTITY(1,1) NOT NULL,
        store_name VARCHAR(255) NOT NULL,
        phone VARCHAR(25) NULL,
        email VARCHAR(255) NULL,
        street VARCHAR(255) NULL,
        city VARCHAR(255) NULL,
        state VARCHAR(10) NULL,
        zip_code VARCHAR(5) NULL,
        CONSTRAINT PK_stores PRIMARY KEY (store_id)
    )
END
GO

-- Tabla: staffs (Personal/Empleados)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='staffs' AND xtype='U')
BEGIN
    CREATE TABLE staffs (
        staff_id INT IDENTITY(1,1) NOT NULL,
        first_name VARCHAR(50) NOT NULL,
        last_name VARCHAR(50) NOT NULL,
        email VARCHAR(255) UNIQUE NOT NULL,
        phone VARCHAR(25) NULL,
        active TINYINT NOT NULL,
        store_id INT NOT NULL,
        manager_id INT NULL,
        CONSTRAINT PK_staffs PRIMARY KEY (staff_id),
        CONSTRAINT FK_staffs_stores FOREIGN KEY (store_id) REFERENCES stores(store_id) ON DELETE CASCADE,
        CONSTRAINT FK_staffs_staffs FOREIGN KEY (manager_id) REFERENCES staffs(staff_id)
    )
END
GO

-- Tabla: orders (Ordenes/Pedidos)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='orders' AND xtype='U')
BEGIN
    CREATE TABLE orders (
        order_id INT IDENTITY(1,1) NOT NULL,
        customer_id INT NOT NULL,
        order_status TINYINT NOT NULL DEFAULT 1,
        order_date DATE NOT NULL DEFAULT GETDATE(),
        required_date DATE NOT NULL,
        shipped_date DATE NULL,
        store_id INT NOT NULL DEFAULT 1,
        staff_id INT NOT NULL DEFAULT 1,
        usuario_id INT NOT NULL,
        CONSTRAINT PK_orders PRIMARY KEY (order_id),
        CONSTRAINT FK_orders_customers FOREIGN KEY (customer_id) REFERENCES customers(customer_id) ON DELETE CASCADE,
        CONSTRAINT FK_orders_stores FOREIGN KEY (store_id) REFERENCES stores(store_id),
        CONSTRAINT FK_orders_staffs FOREIGN KEY (staff_id) REFERENCES staffs(staff_id),
        CONSTRAINT FK_orders_users FOREIGN KEY (usuario_id) REFERENCES users(usuario_id)
    )
END
GO

-- Tabla: order_items (Detalles de ordenes)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='order_items' AND xtype='U')
BEGIN
    CREATE TABLE order_items (
        order_item_id INT IDENTITY(1,1) NOT NULL,
        order_id INT NOT NULL,
        product_id INT NOT NULL,
        quantity INT NOT NULL,
        price DECIMAL(10,2) NOT NULL,
        discount DECIMAL(4,2) NOT NULL DEFAULT 0,
        CONSTRAINT PK_order_items PRIMARY KEY (order_item_id),
        CONSTRAINT FK_order_items_orders FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE,
        CONSTRAINT FK_order_items_products FOREIGN KEY (product_id) REFERENCES products(product_id)
    )
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA CATEGORIES
-- ===============================================================================

-- Insertar Categoria
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_categories' AND type='P')
    DROP PROCEDURE spinsertar_categories
GO

CREATE PROCEDURE spinsertar_categories
    @category_id INT OUTPUT,
    @category_name VARCHAR(255)
AS
BEGIN
    INSERT INTO categories(category_name)
    VALUES(@category_name)
    SET @category_id = SCOPE_IDENTITY()
END
GO

-- Editar Categoria
IF EXISTS (SELECT * FROM sysobjects WHERE name='speditar_categories' AND type='P')
    DROP PROCEDURE speditar_categories
GO

CREATE PROCEDURE speditar_categories
    @category_id INT,
    @category_name VARCHAR(255)
AS
BEGIN
    UPDATE categories 
    SET category_name = @category_name
    WHERE category_id = @category_id
END
GO

-- Eliminar Categoria
IF EXISTS (SELECT * FROM sysobjects WHERE name='speliminar_categories' AND type='P')
    DROP PROCEDURE speliminar_categories
GO

CREATE PROCEDURE speliminar_categories
    @category_id INT
AS
BEGIN
    DELETE FROM categories WHERE category_id = @category_id
END
GO

-- Mostrar Categorias
IF EXISTS (SELECT * FROM sysobjects WHERE name='spmostrar_categories' AND type='P')
    DROP PROCEDURE spmostrar_categories
GO

CREATE PROCEDURE spmostrar_categories
AS
BEGIN
    SELECT category_id, category_name FROM categories ORDER BY category_name
END
GO

-- Buscar Categoria por nombre
IF EXISTS (SELECT * FROM sysobjects WHERE name='spbuscar_category_name' AND type='P')
    DROP PROCEDURE spbuscar_category_name
GO

CREATE PROCEDURE spbuscar_category_name
    @textbuscar VARCHAR(200)
AS
BEGIN
    SELECT category_id, category_name 
    FROM categories 
    WHERE category_name LIKE '%' + @textbuscar + '%'
    ORDER BY category_name
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA PRODUCTS
-- ===============================================================================

-- Insertar Producto
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_products' AND type='P')
    DROP PROCEDURE spinsertar_products
GO

CREATE PROCEDURE spinsertar_products
    @product_id INT OUTPUT,
    @product_name VARCHAR(200),
    @model_year SMALLINT,
    @price MONEY,
    @imagen IMAGE,
    @category_id INT
AS
BEGIN
    INSERT INTO products(product_name, model_year, price, imagen, category_id)
    VALUES(@product_name, @model_year, @price, @imagen, @category_id)
    SET @product_id = SCOPE_IDENTITY()
END
GO

-- Editar Producto
IF EXISTS (SELECT * FROM sysobjects WHERE name='speditar_products' AND type='P')
    DROP PROCEDURE speditar_products
GO

CREATE PROCEDURE speditar_products
    @product_id INT,
    @product_name VARCHAR(200),
    @model_year SMALLINT,
    @price MONEY,
    @imagen IMAGE,
    @category_id INT
AS
BEGIN
    UPDATE products 
    SET product_name = @product_name,
        model_year = @model_year,
        price = @price,
        imagen = @imagen,
        category_id = @category_id
    WHERE product_id = @product_id
END
GO

-- Eliminar Producto
IF EXISTS (SELECT * FROM sysobjects WHERE name='speliminar_products' AND type='P')
    DROP PROCEDURE speliminar_products
GO

CREATE PROCEDURE speliminar_products
    @product_id INT
AS
BEGIN
    DELETE FROM products WHERE product_id = @product_id
END
GO

-- Buscar Producto por nombre
IF EXISTS (SELECT * FROM sysobjects WHERE name='spbuscar_product_name' AND type='P')
    DROP PROCEDURE spbuscar_product_name
GO

CREATE PROCEDURE spbuscar_product_name
    @textbuscar VARCHAR(200)
AS
BEGIN
    SELECT p.product_id, p.product_name, p.model_year, p.price, p.imagen, 
           p.category_id, c.category_name AS category
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    WHERE p.product_name LIKE '%' + @textbuscar + '%'
    ORDER BY p.product_name
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA CUSTOMERS
-- ===============================================================================

-- Insertar Cliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_customers' AND type='P')
    DROP PROCEDURE spinsertar_customers
GO

CREATE PROCEDURE spinsertar_customers
    @customer_id INT OUTPUT,
    @first_name VARCHAR(255),
    @last_name VARCHAR(255),
    @phone VARCHAR(25),
    @email VARCHAR(255),
    @street VARCHAR(255),
    @city VARCHAR(50),
    @state VARCHAR(25)
AS
BEGIN
    INSERT INTO customers(first_name, last_name, phone, email, street, city, state)
    VALUES(@first_name, @last_name, @phone, @email, @street, @city, @state)
    SET @customer_id = SCOPE_IDENTITY()
END
GO

-- Editar Cliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='speditar_customers' AND type='P')
    DROP PROCEDURE speditar_customers
GO

CREATE PROCEDURE speditar_customers
    @customer_id INT,
    @first_name VARCHAR(255),
    @last_name VARCHAR(255),
    @phone VARCHAR(25),
    @email VARCHAR(255),
    @street VARCHAR(255),
    @city VARCHAR(50),
    @state VARCHAR(25)
AS
BEGIN
    UPDATE customers 
    SET first_name = @first_name,
        last_name = @last_name,
        phone = @phone,
        email = @email,
        street = @street,
        city = @city,
        state = @state
    WHERE customer_id = @customer_id
END
GO

-- Eliminar Cliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='speliminar_customers' AND type='P')
    DROP PROCEDURE speliminar_customers
GO

CREATE PROCEDURE speliminar_customers
    @customer_id INT
AS
BEGIN
    DELETE FROM customers WHERE customer_id = @customer_id
END
GO

-- Mostrar Clientes
IF EXISTS (SELECT * FROM sysobjects WHERE name='spmostrar_customers' AND type='P')
    DROP PROCEDURE spmostrar_customers
GO

CREATE PROCEDURE spmostrar_customers
AS
BEGIN
    SELECT customer_id, first_name, last_name, phone, email, street, city, state 
    FROM customers 
    ORDER BY first_name, last_name
END
GO

-- Buscar Cliente por nombre
IF EXISTS (SELECT * FROM sysobjects WHERE name='spbuscar_customer_name' AND type='P')
    DROP PROCEDURE spbuscar_customer_name
GO

CREATE PROCEDURE spbuscar_customer_name
    @textobuscar VARCHAR(50)
AS
BEGIN
    SELECT customer_id, first_name, last_name, phone, email, street, city, state 
    FROM customers 
    WHERE first_name LIKE '%' + @textobuscar + '%' OR last_name LIKE '%' + @textobuscar + '%'
    ORDER BY first_name, last_name
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA USERS
-- ===============================================================================

-- Insertar Usuario
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_users' AND type='P')
    DROP PROCEDURE spinsertar_users
GO

CREATE PROCEDURE spinsertar_users
    @usuario_id INT OUTPUT,
    @usuario_name VARCHAR(50),
    @usuario_clave VARCHAR(250),
    @usuario_email VARCHAR(100)
AS
BEGIN
    INSERT INTO users(usuario_name, usuario_clave, usuario_email)
    VALUES(@usuario_name, @usuario_clave, @usuario_email)
    SET @usuario_id = SCOPE_IDENTITY()
END
GO

-- Editar Usuario
IF EXISTS (SELECT * FROM sysobjects WHERE name='speditar_users' AND type='P')
    DROP PROCEDURE speditar_users
GO

CREATE PROCEDURE speditar_users
    @usuario_id INT,
    @usuario_name VARCHAR(50),
    @usuario_clave VARCHAR(250),
    @usuario_email VARCHAR(100)
AS
BEGIN
    UPDATE users 
    SET usuario_name = @usuario_name,
        usuario_clave = @usuario_clave,
        usuario_email = @usuario_email
    WHERE usuario_id = @usuario_id
END
GO

-- Eliminar Usuario
IF EXISTS (SELECT * FROM sysobjects WHERE name='speliminar_users' AND type='P')
    DROP PROCEDURE speliminar_users
GO

CREATE PROCEDURE speliminar_users
    @usuario_id INT
AS
BEGIN
    DELETE FROM users WHERE usuario_id = @usuario_id
END
GO

-- Mostrar Usuarios
IF EXISTS (SELECT * FROM sysobjects WHERE name='spmostrar_users' AND type='P')
    DROP PROCEDURE spmostrar_users
GO

CREATE PROCEDURE spmostrar_users
AS
BEGIN
    SELECT usuario_id, usuario_name, usuario_email 
    FROM users 
    ORDER BY usuario_name
END
GO

-- Buscar Usuario por nombre
IF EXISTS (SELECT * FROM sysobjects WHERE name='spbuscar_user_name' AND type='P')
    DROP PROCEDURE spbuscar_user_name
GO

CREATE PROCEDURE spbuscar_user_name
    @textbuscar VARCHAR(50)
AS
BEGIN
    SELECT usuario_id, usuario_name, usuario_email 
    FROM users 
    WHERE usuario_name LIKE '%' + @textbuscar + '%'
    ORDER BY usuario_name
END
GO

-- Login de Usuario
IF EXISTS (SELECT * FROM sysobjects WHERE name='splogin' AND type='P')
    DROP PROCEDURE splogin
GO

CREATE PROCEDURE splogin
    @usuario VARCHAR(50),
    @clave VARCHAR(250)
AS
BEGIN
    SELECT usuario_id, usuario_name, usuario_email 
    FROM users 
    WHERE usuario_name = @usuario AND usuario_clave = @clave
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA ORDERS
-- ===============================================================================

-- Insertar Orden
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_orders' AND type='P')
    DROP PROCEDURE spinsertar_orders
GO

CREATE PROCEDURE spinsertar_orders
    @order_id INT OUTPUT,
    @customer_id INT,
    @usuario_id INT
AS
BEGIN
    INSERT INTO orders(customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
    VALUES(@customer_id, 1, GETDATE(), DATEADD(day, 7, GETDATE()), 1, 1, @usuario_id)
    SET @order_id = SCOPE_IDENTITY()
END
GO

-- Mostrar Ordenes
IF EXISTS (SELECT * FROM sysobjects WHERE name='spmostrar_order' AND type='P')
    DROP PROCEDURE spmostrar_order
GO

CREATE PROCEDURE spmostrar_order
AS
BEGIN
    SELECT o.order_id, 
           CONCAT(c.first_name, ' ', c.last_name) AS cliente,
           o.order_date, 
           o.required_date,
           o.shipped_date,
           CASE o.order_status 
               WHEN 1 THEN 'Pendiente'
               WHEN 2 THEN 'Procesando'
               WHEN 3 THEN 'Rechazado'
               WHEN 4 THEN 'Completado'
               ELSE 'Sin Estado'
           END AS estado,
           u.usuario_name AS usuario,
           ISNULL(SUM(oi.quantity * oi.price * (1 - oi.discount)), 0) AS total
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
    LEFT JOIN order_items oi ON o.order_id = oi.order_id
    GROUP BY o.order_id, c.first_name, c.last_name, o.order_date, 
             o.required_date, o.shipped_date, o.order_status, u.usuario_name
    ORDER BY o.order_date DESC
END
GO

-- Buscar Ordenes por fecha
IF EXISTS (SELECT * FROM sysobjects WHERE name='spbuscar_order_fecha' AND type='P')
    DROP PROCEDURE spbuscar_order_fecha
GO

CREATE PROCEDURE spbuscar_order_fecha
    @textobuscar1 VARCHAR(50),
    @textobuscar2 VARCHAR(50)
AS
BEGIN
    SELECT o.order_id, 
           CONCAT(c.first_name, ' ', c.last_name) AS cliente,
           o.order_date, 
           o.required_date,
           o.shipped_date,
           CASE o.order_status 
               WHEN 1 THEN 'Pendiente'
               WHEN 2 THEN 'Procesando'
               WHEN 3 THEN 'Rechazado'
               WHEN 4 THEN 'Completado'
               ELSE 'Sin Estado'
           END AS estado,
           u.usuario_name AS usuario,
           ISNULL(SUM(oi.quantity * oi.price * (1 - oi.discount)), 0) AS total
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
    LEFT JOIN order_items oi ON o.order_id = oi.order_id
    WHERE o.order_date >= CAST(@textobuscar1 AS DATE) 
      AND o.order_date <= CAST(@textobuscar2 AS DATE)
    GROUP BY o.order_id, c.first_name, c.last_name, o.order_date, 
             o.required_date, o.shipped_date, o.order_status, u.usuario_name
    ORDER BY o.order_date DESC
END
GO

-- Mostrar detalles de orden
IF EXISTS (SELECT * FROM sysobjects WHERE name='spmostrar_order_items' AND type='P')
    DROP PROCEDURE spmostrar_order_items
GO

CREATE PROCEDURE spmostrar_order_items
    @textobuscar VARCHAR(50)
AS
BEGIN
    SELECT oi.order_item_id,
           oi.order_id,
           p.product_name AS producto,
           oi.quantity AS cantidad,
           oi.price AS precio,
           oi.discount AS descuento,
           (oi.quantity * oi.price * (1 - oi.discount)) AS subtotal
    FROM order_items oi
    INNER JOIN products p ON oi.product_id = p.product_id
    WHERE oi.order_id = CAST(@textobuscar AS INT)
    ORDER BY oi.order_item_id
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA ORDER_ITEMS
-- ===============================================================================

-- Insertar Item de Orden
IF EXISTS (SELECT * FROM sysobjects WHERE name='spinsertar_order_items' AND type='P')
    DROP PROCEDURE spinsertar_order_items
GO

CREATE PROCEDURE spinsertar_order_items
    @order_item_id INT OUTPUT,
    @order_id INT,
    @product_id INT,
    @quantity INT,
    @price DECIMAL(10,2),
    @discount DECIMAL(4,2)
AS
BEGIN
    INSERT INTO order_items(order_id, product_id, quantity, price, discount)
    VALUES(@order_id, @product_id, @quantity, @price, @discount)
    SET @order_item_id = SCOPE_IDENTITY()
END
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA REPORTES
-- ===============================================================================

-- Reporte de Pedidos por Cliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_reporte_pedidos_por_cliente' AND type='P')
    DROP PROCEDURE sp_reporte_pedidos_por_cliente
GO

CREATE PROCEDURE sp_reporte_pedidos_por_cliente
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SELECT c.customer_id,
           CONCAT(c.first_name, ' ', c.last_name) AS cliente,
           c.email,
           c.phone,
           COUNT(o.order_id) AS total_pedidos,
           ISNULL(SUM(oi.quantity * oi.price * (1 - oi.discount)), 0) AS total_ventas
    FROM customers c
    LEFT JOIN orders o ON c.customer_id = o.customer_id 
                      AND o.order_date BETWEEN @fecha_inicio AND @fecha_fin
    LEFT JOIN order_items oi ON o.order_id = oi.order_id
    GROUP BY c.customer_id, c.first_name, c.last_name, c.email, c.phone
    HAVING COUNT(o.order_id) > 0
    ORDER BY total_ventas DESC, total_pedidos DESC
END
GO

-- Reporte de Productos más Vendidos
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_reporte_productos_mas_vendidos' AND type='P')
    DROP PROCEDURE sp_reporte_productos_mas_vendidos
GO

CREATE PROCEDURE sp_reporte_productos_mas_vendidos
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SELECT p.product_id,
           p.product_name AS producto,
           c.category_name AS categoria,
           p.model_year AS año_modelo,
           p.price AS precio_unitario,
           SUM(oi.quantity) AS cantidad_vendida,
           SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ingresos
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    INNER JOIN order_items oi ON p.product_id = oi.product_id
    INNER JOIN orders o ON oi.order_id = o.order_id
    WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
    GROUP BY p.product_id, p.product_name, c.category_name, p.model_year, p.price
    ORDER BY cantidad_vendida DESC, total_ingresos DESC
END
GO

-- Reporte de Ventas por Período
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_reporte_ventas_por_periodo' AND type='P')
    DROP PROCEDURE sp_reporte_ventas_por_periodo
GO

CREATE PROCEDURE sp_reporte_ventas_por_periodo
    @tipo_periodo VARCHAR(20),
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    IF @tipo_periodo = 'DIARIO'
    BEGIN
        SELECT CAST(o.order_date AS DATE) AS periodo,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY CAST(o.order_date AS DATE)
        ORDER BY periodo DESC
    END
    ELSE IF @tipo_periodo = 'SEMANAL'
    BEGIN
        SELECT CONCAT('Semana ', DATEPART(week, o.order_date), ' - ', YEAR(o.order_date)) AS periodo,
               MIN(o.order_date) AS fecha_inicio_semana,
               MAX(o.order_date) AS fecha_fin_semana,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(o.order_date), DATEPART(week, o.order_date)
        ORDER BY YEAR(o.order_date) DESC, DATEPART(week, o.order_date) DESC
    END
    ELSE IF @tipo_periodo = 'MENSUAL'
    BEGIN
        SELECT CONCAT(DATENAME(month, o.order_date), ' ', YEAR(o.order_date)) AS periodo,
               MONTH(o.order_date) AS mes,
               YEAR(o.order_date) AS año,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(o.order_date), MONTH(o.order_date), DATENAME(month, o.order_date)
        ORDER BY año DESC, mes DESC
    END
    ELSE IF @tipo_periodo = 'ANUAL'
    BEGIN
        SELECT CAST(YEAR(o.order_date) AS VARCHAR) AS periodo,
               YEAR(o.order_date) AS año,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(o.order_date)
        ORDER BY año DESC
    END
    ELSE
    BEGIN
        -- Por defecto, usar DIARIO si no se especifica un período válido
        SELECT CAST(o.order_date AS DATE) AS periodo,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY CAST(o.order_date AS DATE)
        ORDER BY periodo DESC
    END
END
GO

-- ===============================================================================
-- DATOS INICIALES
-- ===============================================================================

-- Insertar datos iniciales en stores
IF NOT EXISTS (SELECT * FROM stores WHERE store_id = 1)
BEGIN
    INSERT INTO stores (store_name, phone, email, street, city, state, zip_code)
    VALUES ('Tienda Principal', '555-0123', 'principal@bikestore.com', 'Av. Principal 123', 'Ciudad', 'Estado', '12345')
END
GO

-- Insertar datos iniciales en staffs
IF NOT EXISTS (SELECT * FROM staffs WHERE staff_id = 1)
BEGIN
    INSERT INTO staffs (first_name, last_name, email, phone, active, store_id, manager_id)
    VALUES ('Admin', 'Sistema', 'admin@bikestore.com', '555-0100', 1, 1, NULL)
END
GO

-- Insertar categorías iniciales
IF NOT EXISTS (SELECT * FROM categories WHERE category_name = 'Bicicletas de Montaña')
BEGIN
    INSERT INTO categories (category_name) VALUES 
    ('Bicicletas de Montaña'),
    ('Bicicletas de Carretera'),
    ('Bicicletas Eléctricas'),
    ('Accesorios'),
    ('Repuestos'),
    ('Ropa Ciclismo')
END
GO

-- Insertar usuario administrador inicial
IF NOT EXISTS (SELECT * FROM users WHERE usuario_name = 'admin')
BEGIN
    INSERT INTO users (usuario_name, usuario_clave, usuario_email)
    VALUES ('admin', '123456', 'admin@sistema.com')
END
GO

-- Insertar algunos clientes de ejemplo
IF NOT EXISTS (SELECT * FROM customers WHERE email = 'juan.perez@email.com')
BEGIN
    INSERT INTO customers (first_name, last_name, phone, email, street, city, state)
    VALUES 
    ('Juan', 'Pérez', '555-1234', 'juan.perez@email.com', 'Calle 1 #123', 'Ciudad A', 'Estado A'),
    ('María', 'González', '555-5678', 'maria.gonzalez@email.com', 'Calle 2 #456', 'Ciudad B', 'Estado B'),
    ('Carlos', 'Rodríguez', '555-9876', 'carlos.rodriguez@email.com', 'Calle 3 #789', 'Ciudad C', 'Estado C')
END
GO

-- Insertar algunos productos de ejemplo
IF NOT EXISTS (SELECT * FROM products WHERE product_name = 'Bicicleta Montaña Trek X-Caliber')
BEGIN
    INSERT INTO products (product_name, model_year, price, category_id)
    VALUES 
    ('Bicicleta Montaña Trek X-Caliber', 2024, 15000.00, 1),
    ('Bicicleta Carretera Specialized Allez', 2024, 18000.00, 2),
    ('Bicicleta Eléctrica Giant Explore E+', 2024, 35000.00, 3),
    ('Casco Specialized Align', 2024, 1200.00, 4),
    ('Llanta Continental Grand Prix', 2024, 800.00, 5)
END
GO

-- ===============================================================================
-- VISTAS ÚTILES
-- ===============================================================================

-- Vista para productos con categoría
IF EXISTS (SELECT * FROM sysobjects WHERE name='vw_productos_categoria' AND type='V')
    DROP VIEW vw_productos_categoria
GO

CREATE VIEW vw_productos_categoria AS
SELECT p.product_id, 
       p.product_name, 
       p.model_year, 
       p.price, 
       c.category_name
FROM products p
INNER JOIN categories c ON p.category_id = c.category_id
GO

-- Vista para órdenes con información del cliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='vw_ordenes_cliente' AND type='V')
    DROP VIEW vw_ordenes_cliente
GO

CREATE VIEW vw_ordenes_cliente AS
SELECT o.order_id,
       o.order_date,
       CONCAT(c.first_name, ' ', c.last_name) AS cliente,
       c.email AS cliente_email,
       c.phone AS cliente_phone,
       u.usuario_name AS usuario_sistema,
       CASE o.order_status 
           WHEN 1 THEN 'Pendiente'
           WHEN 2 THEN 'Procesando'
           WHEN 3 THEN 'Rechazado'
           WHEN 4 THEN 'Completado'
           ELSE 'Sin Estado'
       END AS estado
FROM orders o
INNER JOIN customers c ON o.customer_id = c.customer_id
INNER JOIN users u ON o.usuario_id = u.usuario_id
GO

-- ===============================================================================
-- PROCEDIMIENTOS ALMACENADOS PARA REPORTES ESPECÍFICOS
-- ===============================================================================

-- Procedimiento para reporte de factura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spreporte_factura' AND type='P')
    DROP PROCEDURE spreporte_factura
GO

CREATE PROCEDURE spreporte_factura
    @order_id INT
AS
BEGIN
    SELECT 
        o.order_id,
        u.usuario_name as users,
        CONCAT(c.first_name, ' ', c.last_name) as Cliente,
        ISNULL(c.street, '') as street,
        ISNULL(c.phone, '') as phone,
        ISNULL(c.state, '') as state,
        ISNULL(c.city, '') as city,
        o.order_date,
        p.product_name,
        p.model_year,
        oi.quantity,
        oi.price,
        oi.discount,
        (oi.quantity * oi.price * (1 - oi.discount)) as Subtotal
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    INNER JOIN users u ON o.usuario_id = u.usuario_id
    INNER JOIN order_items oi ON o.order_id = oi.order_id
    INNER JOIN products p ON oi.product_id = p.product_id
    WHERE o.order_id = @order_id
    ORDER BY oi.order_item_id
END
GO

PRINT 'Script de base de datos ejecutado exitosamente.'
PRINT 'Base de datos: Bike_Store'
PRINT 'Tablas creadas: categories, products, customers, users, stores, staffs, orders, order_items'
PRINT 'Procedimientos almacenados: 30+ procedimientos para CRUD y reportes'
PRINT 'Procedimiento de reporte: spreporte_factura agregado'
PRINT 'Datos iniciales: Categorías, usuario admin, tienda principal, productos y clientes de ejemplo'
PRINT 'El sistema está listo para usar, incluidos los reportes de factura.'
GO