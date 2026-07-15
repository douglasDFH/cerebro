-- Habilita la ejecución continua en caso de errores
SET XACT_ABORT ON
GO

-- 1. Crear base de datos y seleccionarla
IF DB_ID('Bike_Store') IS NOT NULL
    DROP DATABASE Bike_Store;
GO

CREATE DATABASE Bike_Store;
GO

USE Bike_Store;
GO
select *from customers;
-- 2. Crear tabla de clientes
CREATE TABLE customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    phone VARCHAR(25) NULL,
    email VARCHAR(255) NOT NULL,
    street VARCHAR(255) NULL,
    city VARCHAR(50) NULL,
    state VARCHAR(25) NULL,
    create_date DATE NOT NULL DEFAULT GETDATE()
);
GO
select * from categories;
-- 3. Crear tabla de categorías
CREATE TABLE categories (
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    category_name VARCHAR(255) NOT NULL
);
GO

-- 4. Insertar categorías de ejemplo
INSERT INTO categories (category_name) VALUES
('Mountain Bikes'),
('Road Bikes'),
('Electric Bikes'),
('Accessories'),
('Clothing');
GO

-- 5. Insertar cliente de ejemplo
INSERT INTO customers (first_name, last_name, phone, email, street, city, state, create_date)
VALUES ('Juanito Juan', 'Melgar Soto', '77712345', 'jj@gmail.com', 'Ave. Beni #567', 'Santa Cruz', 'Santa Cruz', GETDATE());
GO

-- 6. Crear tabla de productos
CREATE TABLE products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    product_name VARCHAR(200) NOT NULL,
    model_year SMALLINT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    imagen IMAGE NULL,
    category_id INT NOT NULL,
    create_date DATE NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_categoriesProducts FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
GO

-- 7. Crear tabla de usuarios
CREATE TABLE users (
    usuario_id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    clave VARCHAR(250) NOT NULL,
    email VARCHAR(100) NOT NULL
);
GO

-- 8. PROCEDIMIENTOS ALMACENADOS

-- Insertar producto
CREATE PROC spinsertar_products
    @product_id INT OUTPUT,
    @product_name VARCHAR(200),
    @model_year SMALLINT,
    @price DECIMAL(10,2),
    @imagen IMAGE,
    @category_id INT
AS
BEGIN
    INSERT INTO products (product_name, model_year, price, imagen, category_id)
    VALUES (@product_name, @model_year, @price, @imagen, @category_id);

    SET @product_id = SCOPE_IDENTITY();
END;
GO

-- Editar producto
CREATE PROC speditar_products
    @product_id INT,
    @product_name VARCHAR(200),
    @model_year SMALLINT,
    @price DECIMAL(10,2),
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
    WHERE product_id = @product_id;
END;
GO

-- Eliminar producto
CREATE PROC speliminar_products
    @product_id INT
AS
BEGIN
    DELETE FROM products WHERE product_id = @product_id;
END;
GO

-- Mostrar productos
CREATE PROC spmostrar_products
AS
BEGIN
    SELECT p.product_id, p.product_name, p.model_year, p.price, p.imagen,
           p.category_id, c.category_name AS Category
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    ORDER BY p.product_id DESC;
END;
GO

-- Buscar producto por nombre
CREATE PROC spbuscar_product_name
    @textbuscar VARCHAR(50)
AS
BEGIN
    SELECT p.product_id, p.product_name, p.model_year, p.price, p.imagen,
           p.category_id, c.category_name AS Category
    FROM products p
    INNER JOIN categories c ON p.category_id = c.category_id
    WHERE product_name LIKE '%' + @textbuscar + '%'
    ORDER BY p.product_name;
END;
GO

-- Insertar categoría
CREATE PROC spinsertar_categories
    @category_id INT OUTPUT,
    @category_name VARCHAR(255)
AS
BEGIN
    INSERT INTO categories (category_name)
    VALUES (@category_name);

    SET @category_id = SCOPE_IDENTITY();
END;
GO

-- Editar categoría
CREATE PROC speditar_categories
    @category_id INT,
    @category_name VARCHAR(255)
AS
BEGIN
    UPDATE categories
    SET category_name = @category_name
    WHERE category_id = @category_id;
END;
GO

-- Eliminar categoría
CREATE PROC speliminar_categories
    @category_id INT
AS
BEGIN
    DELETE FROM categories WHERE category_id = @category_id;
END;
GO

-- Mostrar categorías
CREATE PROC spmostrar_categories
AS
BEGIN
    SELECT * FROM categories ORDER BY category_id DESC;
END;
GO

-- Buscar categoría por nombre
CREATE PROC spbuscar_category_name
    @textbuscar VARCHAR(50)
AS
BEGIN
    SELECT * FROM categories
    WHERE category_name LIKE '%' + @textbuscar + '%'
    ORDER BY category_name;
END;
GO
