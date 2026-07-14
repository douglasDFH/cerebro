-- Script para la base de datos appbike_store
-- Copiar y pegar TODO este código en phpMyAdmin

-- Usar la base de datos
USE appbike_store;

-- Desactivar verificaciones de claves foráneas temporalmente
SET FOREIGN_KEY_CHECKS = 0;

-- Eliminar tablas si existen (en orden correcto)
DROP TABLE IF EXISTS order_items;
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS customers;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS roles;

-- =============================================
-- CREAR TABLAS
-- =============================================

-- Tabla: roles
CREATE TABLE roles (
    rol_id INT NOT NULL AUTO_INCREMENT,
    nombre_rol VARCHAR(50) NOT NULL,
    descripcion TEXT,
    activo TINYINT(1) DEFAULT 1,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (rol_id),
    UNIQUE KEY unique_nombre_rol (nombre_rol)
);

-- Tabla: usuarios
CREATE TABLE usuarios (
    user_id INT NOT NULL AUTO_INCREMENT,
    usuario VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL,
    rol_id INT NOT NULL,
    activo TINYINT(1) DEFAULT 1,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id),
    UNIQUE KEY unique_usuario (usuario),
    UNIQUE KEY unique_email_usuario (email)
);

-- Tabla: customers
CREATE TABLE customers (
    customer_id INT NOT NULL AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(100) NOT NULL,
    street VARCHAR(255),
    city VARCHAR(100),
    state VARCHAR(100),
    activo TINYINT(1) DEFAULT 1,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (customer_id),
    UNIQUE KEY unique_email_customer (email)
);

-- Tabla: products
CREATE TABLE products (
    product_id INT NOT NULL AUTO_INCREMENT,
    product_name VARCHAR(200) NOT NULL,
    description TEXT,
    foto VARCHAR(255),
    tags VARCHAR(500),
    model_year INT,
    price DECIMAL(10,2) NOT NULL,
    stock INT DEFAULT 0,
    activo TINYINT(1) DEFAULT 1,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (product_id)
);

-- Tabla: orders
CREATE TABLE orders (
    order_id INT NOT NULL AUTO_INCREMENT,
    customer_id INT NOT NULL,
    order_date DATE NOT NULL,
    status ENUM('pendiente', 'procesando', 'enviado', 'entregado', 'cancelado') DEFAULT 'pendiente',
    activo TINYINT(1) DEFAULT 1,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (order_id)
);

-- Tabla: order_items
CREATE TABLE order_items (
    order_item_id INT NOT NULL AUTO_INCREMENT,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    price DECIMAL(10,2) NOT NULL,
    discount DECIMAL(5,4) DEFAULT 0.0000,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (order_item_id)
);

-- =============================================
-- INSERTAR DATOS INICIALES
-- =============================================

-- Insertar roles
INSERT INTO roles (nombre_rol, descripcion) VALUES
('administrador', 'Acceso completo al sistema'),
('vendedor', 'Acceso a ventas y consultas'),
('supervisor', 'Acceso a reportes y supervisión');

-- Insertar usuario administrador (contraseña: admin123)
INSERT INTO usuarios (usuario, password, email, rol_id) VALUES
('admin', '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'admin@bikestore.com', 1);

-- Insertar clientes de ejemplo
INSERT INTO customers (first_name, last_name, phone, email, street, city, state) VALUES
('Carlos', 'Rodríguez', '70123456', 'carlos.rodriguez@email.com', 'Av. Libertador 123', 'La Paz', 'La Paz'),
('María', 'González', '71234567', 'maria.gonzalez@email.com', 'Calle Murillo 456', 'Cochabamba', 'Cochabamba'),
('José', 'Mamani', '72345678', 'jose.mamani@email.com', 'Av. 6 de Agosto 789', 'El Alto', 'La Paz'),
('Ana', 'Quispe', '73456789', 'ana.quispe@email.com', 'Calle Ballivián 321', 'Santa Cruz', 'Santa Cruz'),
('Pedro', 'Vargas', '74567890', 'pedro.vargas@email.com', 'Av. América 654', 'Oruro', 'Oruro');

-- Insertar productos de ejemplo
INSERT INTO products (product_name, description, foto, tags, model_year, price, stock) VALUES
('Bicicleta Mountain Trek X1', 'Bicicleta de montaña con suspensión delantera y marco de aluminio', 'trek_x1.jpg', 'montaña,trek,aluminio,suspensión', 2024, 2500.00, 15),
('Bicicleta Urbana City Comfort', 'Bicicleta urbana cómoda para uso diario en la ciudad', 'city_comfort.jpg', 'urbana,ciudad,cómoda,paseo', 2024, 1200.00, 25),
('Bicicleta BMX Freestyle Pro', 'Bicicleta BMX para acrobacias y freestyle', 'bmx_pro.jpg', 'bmx,freestyle,acrobacias,jóvenes', 2023, 800.00, 10),
('Bicicleta Eléctrica E-Bike 500W', 'Bicicleta eléctrica con motor de 500W y batería de larga duración', 'ebike_500w.jpg', 'eléctrica,motor,batería,ecológica', 2024, 4500.00, 8),
('Bicicleta Carrera Speed Racing', 'Bicicleta de carrera ultraligera para competición', 'speed_racing.jpg', 'carrera,velocidad,ligera,competición', 2024, 3200.00, 12),
('Bicicleta Híbrida Urban Explorer', 'Bicicleta híbrida perfecta para ciudad y senderos ligeros', 'urban_explorer.jpg', 'híbrida,versátil,ciudad,senderos', 2023, 1800.00, 20);

-- Insertar pedidos de ejemplo
INSERT INTO orders (customer_id, order_date, status) VALUES
(1, '2024-10-01', 'entregado'),
(2, '2024-10-03', 'enviado'),
(3, '2024-10-05', 'procesando'),
(4, '2024-10-07', 'pendiente'),
(1, '2024-10-08', 'pendiente');

-- Insertar items de pedidos
INSERT INTO order_items (order_id, product_id, quantity, price, discount) VALUES
(1, 1, 1, 2500.00, 0.0000),
(1, 3, 2, 800.00, 0.1000),
(2, 2, 1, 1200.00, 0.0000),
(3, 4, 1, 4500.00, 0.0500),
(3, 6, 1, 1800.00, 0.0000),
(4, 5, 1, 3200.00, 0.0000),
(5, 2, 2, 1200.00, 0.0800);

-- =============================================
-- AGREGAR CLAVES FORÁNEAS
-- =============================================

ALTER TABLE usuarios ADD CONSTRAINT fk_usuarios_rol FOREIGN KEY (rol_id) REFERENCES roles(rol_id);
ALTER TABLE orders ADD CONSTRAINT fk_orders_customer FOREIGN KEY (customer_id) REFERENCES customers(customer_id);
ALTER TABLE order_items ADD CONSTRAINT fk_order_items_order FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE;
ALTER TABLE order_items ADD CONSTRAINT fk_order_items_product FOREIGN KEY (product_id) REFERENCES products(product_id);

-- =============================================
-- CREAR ÍNDICES PARA OPTIMIZACIÓN
-- =============================================

CREATE INDEX idx_customers_email ON customers(email);
CREATE INDEX idx_customers_name ON customers(first_name, last_name);
CREATE INDEX idx_customers_city ON customers(city);

CREATE INDEX idx_products_name ON products(product_name);
CREATE INDEX idx_products_year ON products(model_year);
CREATE INDEX idx_products_price ON products(price);
CREATE INDEX idx_products_stock ON products(stock);

CREATE INDEX idx_orders_customer ON orders(customer_id);
CREATE INDEX idx_orders_date ON orders(order_date);
CREATE INDEX idx_orders_status ON orders(status);

CREATE INDEX idx_order_items_order ON order_items(order_id);
CREATE INDEX idx_order_items_product ON order_items(product_id);

CREATE INDEX idx_usuarios_rol ON usuarios(rol_id);
CREATE INDEX idx_usuarios_activo ON usuarios(activo);

-- Reactivar verificaciones de claves foráneas
SET FOREIGN_KEY_CHECKS = 1;

-- =============================================
-- VERIFICACIÓN FINAL
-- =============================================

SELECT 'MIGRACIÓN COMPLETADA PARA appbike_store' as RESULTADO;
SELECT COUNT(*) as TOTAL_TABLAS FROM information_schema.tables WHERE table_schema = 'appbike_store';

SELECT 'DATOS INSERTADOS:' as SECCION;
SELECT 'roles' as TABLA, COUNT(*) as REGISTROS FROM roles
UNION ALL
SELECT 'usuarios' as TABLA, COUNT(*) as REGISTROS FROM usuarios
UNION ALL
SELECT 'customers' as TABLA, COUNT(*) as REGISTROS FROM customers
UNION ALL
SELECT 'products' as TABLA, COUNT(*) as REGISTROS FROM products
UNION ALL
SELECT 'orders' as TABLA, COUNT(*) as REGISTROS FROM orders
UNION ALL
SELECT 'order_items' as TABLA, COUNT(*) as REGISTROS FROM order_items;

SELECT 'CREDENCIALES DE ACCESO:' as INFO;
SELECT 'admin' as USUARIO, 'admin123' as CONTRASEÑA;