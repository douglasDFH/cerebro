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

PRINT 'Script de base de datos ejecutado exitosamente.'
PRINT 'Base de datos: Bike_Store'
PRINT 'Tablas creadas: categories, products, customers, users, stores, staffs, orders, order_items'
PRINT 'Procedimientos almacenados: 30+ procedimientos para CRUD y reportes'
PRINT 'Datos iniciales: Categorías, usuario admin, tienda principal, productos y clientes de ejemplo'
PRINT 'El sistema está listo para usar.'
GO




-- ===============================================================================
-- SCRIPT DE CORRECCIÓN PARA LA TABLA USERS
-- Este script corrige el problema de la columna de nombre de usuario
-- ===============================================================================

USE Bike_Store
GO

-- Verificar qué columnas tiene la tabla users actualmente
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION
GO

-- Opción 1: Si la columna se llama 'name' en lugar de 'usuario_name'
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'name')
   AND NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_name')
BEGIN
    PRINT 'Renombrando columna "name" a "usuario_name"...'
    EXEC sp_rename 'users.name', 'usuario_name', 'COLUMN'
    PRINT 'Columna renombrada exitosamente.'
END

-- Opción 2: Si no existe ninguna columna para el nombre de usuario
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_name')
   AND NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'name')
BEGIN
    PRINT 'Agregando columna usuario_name...'
    ALTER TABLE users ADD usuario_name VARCHAR(50) NOT NULL DEFAULT ''
    PRINT 'Columna agregada exitosamente.'
END

-- Verificar si existe la columna usuario_clave
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_clave')
BEGIN
    PRINT 'Agregando columna usuario_clave...'
    ALTER TABLE users ADD usuario_clave VARCHAR(250) NOT NULL DEFAULT ''
    PRINT 'Columna usuario_clave agregada.'
END

-- Verificar si existe la columna usuario_email
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name = 'usuario_email')
BEGIN
    PRINT 'Agregando columna usuario_email...'
    ALTER TABLE users ADD usuario_email VARCHAR(100) NOT NULL DEFAULT ''
    PRINT 'Columna usuario_email agregada.'
END

-- Insertar usuario admin si no existe
IF NOT EXISTS (SELECT * FROM users WHERE usuario_name = 'admin')
BEGIN
    INSERT INTO users (usuario_name, usuario_clave, usuario_email)
    VALUES ('admin', '123456', 'admin@sistema.com')
    PRINT 'Usuario admin creado exitosamente.'
END
ELSE
BEGIN
    PRINT 'El usuario admin ya existe.'
END

-- Mostrar la estructura final de la tabla
PRINT 'Estructura actual de la tabla users:'
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION

-- Mostrar los usuarios existentes
PRINT 'Usuarios en la tabla:'
SELECT * FROM users
GO

PRINT 'Script de corrección completado.'

 SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS
  WHERE TABLE_NAME = 'users'



    BEGIN
            -- Si la tabla existe, verificar si la columna existe con el nombre correcto
           IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('users') AND name =
          'usuario_name')
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







         PRINT '=== VERIFICACIÓN DE ESTRUCTURA DE TABLAS ==='
PRINT ''

-- Tabla categories
PRINT '--- TABLA: categories ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'categories'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla products
PRINT '--- TABLA: products ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'products'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla customers
PRINT '--- TABLA: customers ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'customers'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla users
PRINT '--- TABLA: users ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'users'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla orders
PRINT '--- TABLA: orders ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'orders'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla order_items
PRINT '--- TABLA: order_items ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'order_items'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla staffs
PRINT '--- TABLA: staffs ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'staffs'
ORDER BY ORDINAL_POSITION
PRINT ''

-- Tabla stores
PRINT '--- TABLA: stores ---'
SELECT 
    ROW_NUMBER() OVER (ORDER BY ORDINAL_POSITION) - 1 as 'Índice',
    COLUMN_NAME as 'Nombre Columna', 
    DATA_TYPE as 'Tipo', 
    CHARACTER_MAXIMUM_LENGTH as 'Longitud'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'stores'
ORDER BY ORDINAL_POSITION
PRINT ''

PRINT '=== PRUEBAS DE PROCEDIMIENTOS ALMACENADOS ==='
PRINT ''

-- Probar procedimiento de customers
PRINT '--- RESULTADO DE spmostrar_customers ---'
EXEC spmostrar_customers
PRINT ''

-- Probar procedimiento de login
PRINT '--- RESULTADO DE splogin ---'
EXEC splogin @usuario = 'admin', @clave = '123456'
PRINT ''

PRINT '=== VERIFICACIÓN COMPLETADA ==='
PRINT 'Revisa los índices de columnas arriba para asegurar que tu código coincida'



-- ===============================================================================
-- SCRIPT PARA INSERTAR DATOS DE PRUEBA PARA EL DASHBOARD
-- Este script crea datos de ejemplo para que el dashboard muestre estadísticas
-- ===============================================================================

USE Bike_Store
GO

-- Insertar algunos pedidos de prueba si no existen
IF NOT EXISTS (SELECT * FROM orders WHERE customer_id = 1)
BEGIN
    -- Crear algunos pedidos para el primer cliente
    INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
    VALUES 
    (1, 4, GETDATE()-30, GETDATE()-23, 1, 1, 1),
    (1, 4, GETDATE()-25, GETDATE()-18, 1, 1, 1),
    (2, 4, GETDATE()-20, GETDATE()-13, 1, 1, 1),
    (2, 1, GETDATE()-15, GETDATE()-8, 1, 1, 1),
    (3, 4, GETDATE()-10, GETDATE()-3, 1, 1, 1),
    (3, 2, GETDATE()-5, GETDATE()+2, 1, 1, 1),
    (1, 1, GETDATE()-2, GETDATE()+5, 1, 1, 1),
    (2, 3, GETDATE()-1, GETDATE()+6, 1, 1, 1)
END

-- Insertar algunos items de pedidos si no existen
IF NOT EXISTS (SELECT * FROM order_items WHERE order_id = 1)
BEGIN
    -- Items para el pedido 1
    INSERT INTO order_items (order_id, product_id, quantity, price, discount)
    VALUES 
    (1, 1, 2, 15000.00, 0.05),  -- 2 bicicletas de montaña con 5% descuento
    (1, 4, 1, 1200.00, 0.00),   -- 1 casco
    
    -- Items para el pedido 2
    (2, 2, 1, 18000.00, 0.10),  -- 1 bicicleta de carretera con 10% descuento
    (2, 5, 2, 800.00, 0.00),    -- 2 llantas
    
    -- Items para el pedido 3
    (3, 3, 1, 35000.00, 0.15),  -- 1 bicicleta eléctrica con 15% descuento
    (3, 4, 1, 1200.00, 0.05),   -- 1 casco con 5% descuento
    
    -- Items para el pedido 4
    (4, 1, 1, 15000.00, 0.00),  -- 1 bicicleta de montaña
    (4, 5, 4, 800.00, 0.10),    -- 4 llantas con 10% descuento
    
    -- Items para el pedido 5
    (5, 2, 2, 18000.00, 0.08),  -- 2 bicicletas de carretera con 8% descuento
    (5, 4, 2, 1200.00, 0.00),   -- 2 cascos
    
    -- Items para el pedido 6
    (6, 3, 1, 35000.00, 0.12),  -- 1 bicicleta eléctrica con 12% descuento
    
    -- Items para el pedido 7
    (7, 1, 3, 15000.00, 0.20),  -- 3 bicicletas de montaña con 20% descuento
    (7, 5, 6, 800.00, 0.15),    -- 6 llantas con 15% descuento
    
    -- Items para el pedido 8
    (8, 2, 1, 18000.00, 0.05),  -- 1 bicicleta de carretera con 5% descuento
    (8, 4, 3, 1200.00, 0.10)    -- 3 cascos con 10% descuento
END

-- Crear algunos pedidos adicionales para diferentes meses (para el gráfico)
IF NOT EXISTS (SELECT * FROM orders WHERE order_date < GETDATE()-60)
BEGIN
    INSERT INTO orders (customer_id, order_status, order_date, required_date, store_id, staff_id, usuario_id)
    VALUES 
    -- Pedidos de hace 2 meses
    (1, 4, GETDATE()-60, GETDATE()-53, 1, 1, 1),
    (2, 4, GETDATE()-65, GETDATE()-58, 1, 1, 1),
    (3, 4, GETDATE()-70, GETDATE()-63, 1, 1, 1),
    
    -- Pedidos de hace 3 meses
    (1, 4, GETDATE()-90, GETDATE()-83, 1, 1, 1),
    (2, 4, GETDATE()-95, GETDATE()-88, 1, 1, 1),
    (3, 4, GETDATE()-100, GETDATE()-93, 1, 1, 1),
    
    -- Pedidos de hace 4 meses
    (1, 4, GETDATE()-120, GETDATE()-113, 1, 1, 1),
    (2, 4, GETDATE()-125, GETDATE()-118, 1, 1, 1)
    
    -- Obtener los IDs de los pedidos recién insertados para agregar items
    DECLARE @order_id_60 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-60 AS DATE))
    DECLARE @order_id_65 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-65 AS DATE))
    DECLARE @order_id_70 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-70 AS DATE))
    DECLARE @order_id_90 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-90 AS DATE))
    DECLARE @order_id_95 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-95 AS DATE))
    DECLARE @order_id_100 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-100 AS DATE))
    DECLARE @order_id_120 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-120 AS DATE))
    DECLARE @order_id_125 INT = (SELECT order_id FROM orders WHERE order_date = CAST(GETDATE()-125 AS DATE))
    
    -- Items para pedidos históricos
    IF @order_id_60 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_60, 1, 1, 15000.00, 0.05)
        
    IF @order_id_65 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_65, 2, 2, 18000.00, 0.10)
        
    IF @order_id_70 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_70, 3, 1, 35000.00, 0.15)
        
    IF @order_id_90 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_90, 1, 3, 15000.00, 0.08)
        
    IF @order_id_95 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_95, 2, 1, 18000.00, 0.12)
        
    IF @order_id_100 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_100, 4, 5, 1200.00, 0.20)
        
    IF @order_id_120 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_120, 1, 2, 15000.00, 0.00)
        
    IF @order_id_125 IS NOT NULL
        INSERT INTO order_items (order_id, product_id, quantity, price, discount)
        VALUES (@order_id_125, 5, 8, 800.00, 0.25)
END

PRINT '=== DATOS DE PRUEBA INSERTADOS ==='
PRINT ''

-- Mostrar estadísticas actuales
PRINT '--- ESTADÍSTICAS ACTUALES ---'
PRINT 'Total de pedidos: ' + CAST((SELECT COUNT(*) FROM orders) AS VARCHAR(10))
PRINT 'Total de clientes: ' + CAST((SELECT COUNT(*) FROM customers) AS VARCHAR(10))  
PRINT 'Total de productos: ' + CAST((SELECT COUNT(*) FROM products) AS VARCHAR(10))
PRINT 'Total de items vendidos: ' + CAST((SELECT ISNULL(SUM(quantity), 0) FROM order_items) AS VARCHAR(10))
PRINT 'Ventas totales: $' + CAST((SELECT ISNULL(SUM(quantity * price * (1 - discount)), 0) FROM order_items) AS VARCHAR(20))

PRINT ''
PRINT '=== DATOS LISTOS PARA EL DASHBOARD ==='
PRINT 'Ya puedes abrir el Dashboard y ver las estadísticas y gráficos.'



-- ===============================================================================
-- SCRIPT PARA CORREGIR ERROR DE FOREIGN KEY EN ORDERS
-- Este script verifica y corrige los problemas de usuario_id en las órdenes
-- ===============================================================================

USE Bike_Store
GO

PRINT '=== VERIFICANDO USUARIOS EXISTENTES ==='
SELECT usuario_id, usuario_name, usuario_email FROM users ORDER BY usuario_id

PRINT ''
PRINT '=== VERIFICANDO SI EXISTE USUARIO CON ID = 1 ==='
IF EXISTS (SELECT * FROM users WHERE usuario_id = 1)
BEGIN
    PRINT 'El usuario con ID = 1 existe:'
    SELECT usuario_id, usuario_name, usuario_email FROM users WHERE usuario_id = 1
END
ELSE
BEGIN
    PRINT 'ERROR: No existe usuario con ID = 1'
    PRINT 'El usuario con ID = 1 es necesario como usuario por defecto'
    
    -- Si no existe, verificar cuál es el usuario con menor ID
    DECLARE @min_user_id INT = (SELECT MIN(usuario_id) FROM users)
    PRINT 'Usuario con menor ID disponible: ' + CAST(@min_user_id AS VARCHAR(10))
    
    -- Mostrar información del primer usuario disponible
    SELECT TOP 1 usuario_id, usuario_name, usuario_email 
    FROM users 
    ORDER BY usuario_id
END

PRINT ''
PRINT '=== VERIFICANDO ÓRDENES CON PROBLEMAS ==='
-- Verificar si hay órdenes con usuario_id que no existe
SELECT o.order_id, o.usuario_id, 'PROBLEMA: usuario_id no existe' as Estado
FROM orders o 
LEFT JOIN users u ON o.usuario_id = u.usuario_id
WHERE u.usuario_id IS NULL

PRINT ''
PRINT '=== SOLUCIÓN RECOMENDADA ==='
PRINT ''

-- Si el usuario admin (ID=1) no existe, sugerir crearlo
IF NOT EXISTS (SELECT * FROM users WHERE usuario_id = 1)
BEGIN
    PRINT '1. Crear usuario con ID = 1 (recomendado):'
    PRINT '   INSERT INTO users (usuario_name, usuario_clave, usuario_email)'
    PRINT '   VALUES (''admin'', ''123456'', ''admin@sistema.com'')'
    PRINT ''
    
    -- O sugerir actualizar el código para usar el primer usuario disponible
    DECLARE @first_user_id INT = (SELECT MIN(usuario_id) FROM users)
    IF @first_user_id IS NOT NULL
    BEGIN
        PRINT '2. Alternativa - Cambiar el código para usar usuario ID = ' + CAST(@first_user_id AS VARCHAR(10))
        PRINT '   En FrmOrders.cs, cambiar la línea:'
        PRINT '   public string Usuario_id = "' + CAST(@first_user_id AS VARCHAR(10)) + '";'
    END
END
ELSE
BEGIN
    PRINT 'El usuario con ID = 1 existe. El problema puede estar en:'
    PRINT '1. El formulario no está pasando correctamente el usuario_id'
    PRINT '2. Verificar que FrmPrincipal.Iduser tenga un valor válido'
    PRINT '3. Verificar que se ejecute: frmOrders.Usuario_id = this.Iduser'
END

PRINT ''
PRINT '=== APLICANDO CORRECCIÓN AUTOMÁTICA ==='

-- Crear usuario admin con ID = 1 si no existe
IF NOT EXISTS (SELECT * FROM users WHERE usuario_id = 1)
BEGIN
    -- Si hay usuarios, crear uno nuevo con ID específico
    IF EXISTS (SELECT * FROM users)
    BEGIN
        PRINT 'Intentando crear usuario admin con ID = 1...'
        
        -- Habilitar INSERT con IDENTITY_INSERT
        SET IDENTITY_INSERT users ON
        
        INSERT INTO users (usuario_id, usuario_name, usuario_clave, usuario_email)
        VALUES (1, 'admin', '123456', 'admin@sistema.com')
        
        -- Deshabilitar IDENTITY_INSERT
        SET IDENTITY_INSERT users OFF
        
        PRINT 'Usuario admin creado exitosamente con ID = 1'
    END
    ELSE
    BEGIN
        -- Si no hay usuarios, insertar el primero
        INSERT INTO users (usuario_name, usuario_clave, usuario_email)
        VALUES ('admin', '123456', 'admin@sistema.com')
        PRINT 'Usuario admin creado como primer usuario'
    END
END

-- Corregir órdenes con usuario_id inválido
IF EXISTS (SELECT o.order_id FROM orders o LEFT JOIN users u ON o.usuario_id = u.usuario_id WHERE u.usuario_id IS NULL)
BEGIN
    PRINT 'Corrigiendo órdenes con usuario_id inválido...'
    
    DECLARE @valid_user_id INT = (SELECT MIN(usuario_id) FROM users)
    
    UPDATE orders 
    SET usuario_id = @valid_user_id
    WHERE usuario_id NOT IN (SELECT usuario_id FROM users)
    
    PRINT 'Órdenes corregidas para usar usuario_id = ' + CAST(@valid_user_id AS VARCHAR(10))
END

PRINT ''
PRINT '=== VERIFICACIÓN FINAL ==='
PRINT 'Usuarios disponibles:'
SELECT usuario_id, usuario_name, usuario_email FROM users ORDER BY usuario_id

PRINT ''
PRINT 'Estado de órdenes:'
SELECT 
    COUNT(*) as TotalOrdenes,
    COUNT(CASE WHEN u.usuario_id IS NOT NULL THEN 1 END) as OrdenesValidas,
    COUNT(CASE WHEN u.usuario_id IS NULL THEN 1 END) as OrdenesConProblemas
FROM orders o 
LEFT JOIN users u ON o.usuario_id = u.usuario_id

PRINT ''
PRINT '=== CORRECCIÓN COMPLETADA ==='
PRINT 'Ahora deberías poder crear órdenes sin problemas de FK.'
PRINT 'Usuario por defecto: ID = 1 (admin/123456)'