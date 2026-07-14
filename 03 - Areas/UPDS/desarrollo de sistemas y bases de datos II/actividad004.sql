create database BD_ventas;
use BD_ventas;
CREATE TABLE cliente (
  id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  apellido1 VARCHAR(100) NOT NULL,
  apellido2 VARCHAR(100),
  ciudad VARCHAR(100),
  categoria INT UNSIGNED
);

CREATE TABLE comercial (
  id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  apellido1 VARCHAR(100) NOT NULL,
  apellido2 VARCHAR(100),
  comision FLOAT
);

CREATE TABLE pedido (
  id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
  total DOUBLE NOT NULL,
  fecha DATE,
  id_cliente INT UNSIGNED NOT NULL,
  id_comercial INT UNSIGNED NOT NULL,
  FOREIGN KEY (id_cliente) REFERENCES cliente(id),
  FOREIGN KEY (id_comercial) REFERENCES comercial(id)
);

INSERT INTO cliente VALUES(1, 'Aarón', 'Rivero', 'Gómez', 'Almería', 100);
INSERT INTO cliente VALUES(2, 'Adela', 'Salas', 'Díaz', 'Granada', 200);
INSERT INTO cliente VALUES(3, 'Adolfo', 'Rubio', 'Flores', 'Sevilla', NULL);
INSERT INTO cliente VALUES(4, 'Adrián', 'Suárez', NULL, 'Jaén', 300);
INSERT INTO cliente VALUES(5, 'Marcos', 'Loyola', 'Méndez', 'Almería', 200);
INSERT INTO cliente VALUES(6, 'María', 'Santana', 'Moreno', 'Cádiz', 100);
INSERT INTO cliente VALUES(7, 'Pilar', 'Ruiz', NULL, 'Sevilla', 300);
INSERT INTO cliente VALUES(8, 'Pepe', 'Ruiz', 'Santana', 'Huelva', 200);
INSERT INTO cliente VALUES(9, 'Guillermo', 'López', 'Gómez', 'Granada', 225);
INSERT INTO cliente VALUES(10, 'Daniel', 'Santana', 'Loyola', 'Sevilla', 125);

INSERT INTO comercial VALUES(1, 'Daniel', 'Sáez', 'Vega', 0.15);
INSERT INTO comercial VALUES(2, 'Juan', 'Gómez', 'López', 0.13);
INSERT INTO comercial VALUES(3, 'Diego','Flores', 'Salas', 0.11);
INSERT INTO comercial VALUES(4, 'Marta','Herrera', 'Gil', 0.14);
INSERT INTO comercial VALUES(5, 'Antonio','Carretero', 'Ortega', 0.12);
INSERT INTO comercial VALUES(6, 'Manuel','Domínguez', 'Hernández', 0.13);
INSERT INTO comercial VALUES(7, 'Antonio','Vega', 'Hernández', 0.11);
INSERT INTO comercial VALUES(8, 'Alfredo','Ruiz', 'Flores', 0.05);

INSERT INTO pedido VALUES(1, 150.5, '2017-10-05', 5, 2);
INSERT INTO pedido VALUES(2, 270.65, '2016-09-10', 1, 5);
INSERT INTO pedido VALUES(3, 65.26, '2017-10-05', 2, 1);
INSERT INTO pedido VALUES(4, 110.5, '2016-08-17', 8, 3);
INSERT INTO pedido VALUES(5, 948.5, '2017-09-10', 5, 2);
INSERT INTO pedido VALUES(6, 2400.6, '2016-07-27', 7, 1);
INSERT INTO pedido VALUES(7, 5760, '2015-09-10', 2, 1);
INSERT INTO pedido VALUES(8, 1983.43, '2017-10-10', 4, 6);
INSERT INTO pedido VALUES(9, 2480.4, '2016-10-10', 8, 3);
INSERT INTO pedido VALUES(10, 250.45, '2015-06-27', 8, 2);
INSERT INTO pedido VALUES(11, 75.29, '2016-08-17', 3, 7);
INSERT INTO pedido VALUES(12, 3045.6, '2017-04-25', 2, 1);
INSERT INTO pedido VALUES(13, 545.75, '2019-01-25', 6, 1);
INSERT INTO pedido VALUES(14, 145.82, '2017-02-02', 6, 1);
INSERT INTO pedido VALUES(15, 370.85, '2019-03-11', 1, 5);
INSERT INTO pedido VALUES(16, 2389.23, '2019-03-11', 1, 5);

select * from pedido;
select * from comercial;
select * from cliente;
SELECT 
    c.nombre 
   
FROM pedido p
INNER JOIN cliente c ON p.id_cliente = c.id;

-- CONSULTAS QUE RELACIONAN UNA TABLA

-- 1. Consulta de una tabla sin parámetro: 
-- Mostrar a los clientes con categoría mayor a 200
DELIMITER //
CREATE PROCEDURE clientes_categoria_alta()
BEGIN
    SELECT id, nombre, apellido1, apellido2, ciudad, categoria 
    FROM cliente 
    WHERE categoria > 200;
END //
DELIMITER ;

-- Llamar a los clientes con categoria altal
CALL clientes_categoria_alta();


-- 1. Consulta clientes con parámetro de categoría:
-- Muestra clientes con categoría superior a la especificada
DELIMITER //
CREATE PROCEDURE clientes_por_categoria(IN categoria_min INT)
BEGIN
    SELECT *
    FROM cliente 
    WHERE categoria > categoria_min
    order by categoria asc ;
END //
DELIMITER ;
CALL clientes_por_categoria(100);


-- 2. Consulta de una tabla con parámetro de ciudad:
-- Busca clientes por ciudad
DELIMITER //
CREATE PROCEDURE buscar_clientes_ciudad(IN ciudad_param VARCHAR(100))
BEGIN
    SELECT id,nombre,ciudad
    FROM cliente
    WHERE ciudad = ciudad_param;
END //
DELIMITER ;

-- Llamada al procedimiento con parámetro
CALL buscar_clientes_ciudad('granada');

-- /////////////////////////////////////////////////////////////////////////////////--
-- CONSULTAS QUE RELACIONAN DOS TABLAS

-- 3. Consulta de dos tablas con parámetro de rango de fechas:
-- Muestra los clientes y sus pedidos de un año específico
DELIMITER //
CREATE PROCEDURE clientes_pedidos_por_año(IN año INT)
BEGIN
    SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
    FROM cliente c
    INNER JOIN pedido p ON c.id = p.id_cliente
    WHERE YEAR(p.fecha) = año
    ORDER BY c.nombre;
END //
DELIMITER ;

-- Llamada al procedimiento con parámetro
CALL clientes_pedidos_por_anio(2017);

-- POR RANGO DE FECHAS ----
DELIMITER //
CREATE PROCEDURE clientes_pedidos_por_rango_fechas( IN fecha_inicio DATE, IN fecha_fin DATE) 
BEGIN
    SELECT 
        c.nombre, c.apellido1, p.id , p.total, p.fecha
    FROM cliente c
    INNER JOIN pedido p ON c.id = p.id_cliente
    WHERE p.fecha BETWEEN fecha_inicio AND fecha_fin
    ORDER BY p.fecha;
END //
DELIMITER ;
CALL clientes_pedidos_por_rango_fechas('2016-01-01', '2018-12-31');

SELECT * FROM pedido;

-- 4. Consulta de dos tablas con parámetro de cliente:
-- Muestra los pedidos de un cliente específico
DELIMITER //
CREATE PROCEDURE pedidos_por_cliente(IN cliente_id INT)
BEGIN
    SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
    FROM cliente c
    INNER JOIN pedido p ON c.id = p.id_cliente
    WHERE c.id = cliente_id;
END //
DELIMITER ;

-- Llamada al procedimiento con parámetro
CALL pedidos_por_cliente(5);

-- ///////////////////////////////////////////////////////////////////////

-- CONSULTAS QUE RELACIONAN TRES TABLAS

-- 5. Consulta de tres tablas con parámetro de monto mínimo:
-- Muestra pedidos que superen un monto específico
DELIMITER //
CREATE PROCEDURE pedidos_mayor_monto(IN monto_min DOUBLE)
BEGIN
    SELECT p.id AS id_pedido, p.total, p.fecha,c.nombre AS nombre_cliente, c.apellido1 AS apellido_cliente,co.comision
    FROM pedido p
    INNER JOIN cliente c ON p.id_cliente = c.id
    INNER JOIN comercial co ON p.id_comercial = co.id
    WHERE p.total > monto_min
    ORDER BY p.total DESC;
END //
DELIMITER ;

-- Llamada al procedimiento con parámetro
CALL pedidos_mayor_monto(1000);

DELIMITER //
CREATE PROCEDURE pedidos_por_rango_monto(  IN monto_min DOUBLE, IN monto_max DOUBLE) 
BEGIN
    SELECT p.id AS id_pedido, p.total, p.fecha,
        CONCAT(c.nombre, ' ', c.apellido1) AS cliente,
        CONCAT(co.nombre, ' ', co.apellido1) AS comercial,
        co.comision
    FROM pedido p
    INNER JOIN cliente c ON p.id_cliente = c.id
    INNER JOIN comercial co ON p.id_comercial = co.id
    WHERE p.total BETWEEN monto_min AND monto_max  
    ORDER BY p.total DESC;
END //
DELIMITER ;

CALL pedidos_por_rango_monto(200, 500);

-- 6. Consulta de tres tablas con parámetros de fecha:
-- Busca pedidos por rango de fechas
DELIMITER //
CREATE PROCEDURE pedidos_por_fecha(IN fecha_inicio DATE, IN fecha_fin DATE)
BEGIN
    SELECT 
        p.id AS id_pedido, 
        p.total, 
        p.fecha,
        c.nombre AS nombre_cliente, 
        c.apellido1 AS apellido_cliente,
        co.nombre AS nombre_comercial, 
        co.apellido1 AS apellido_comercial
    FROM pedido p
    INNER JOIN cliente c ON p.id_cliente = c.id
    INNER JOIN comercial co ON p.id_comercial = co.id
    WHERE p.fecha BETWEEN fecha_inicio AND fecha_fin;
END //
DELIMITER ;

-- Llamada al procedimiento con parámetros
CALL pedidos_por_fecha('2023-01-01', '2018-12-31');

SHOW CREATE PROCEDURE pedidos_por_rango_monto;

/*DELIMITER //
CREATE PROCEDURE aplicar_descuento(IN monto DECIMAL(10,2), IN porcentaje DECIMAL(5,2))
BEGIN
    SET @descuento = monto * (porcentaje / 100);
    SET @monto_final = monto - @descuento;
    SELECT @monto_final AS total_con_descuento;
END //
DELIMITER ;
call aplicar_descuento(5,2);*/

DELIMITER //
CREATE PROCEDURE clientes_sin_Filtro(IN categoria_min INT)
BEGIN
    IF categoria_min = 0 THEN
        SELECT *
        FROM cliente;
    ELSE
        SELECT * 
        FROM cliente 
        WHERE categoria > categoria_min;
    END IF;
END //
DELIMITER ;  -- <-- ¡Espacio después de DELIMITER!
CALL clientes_sin_Filtro(100); 

-- elimina el almacenamiento
DROP PROCEDURE IF EXISTS nombre_del_procedimiento ;   -- elimina el almacenamiento 

DELIMITER //
CREATE PROCEDURE clientes_pedidos_por_rango_fechas_handlerException(IN fecha_inicio DATE, IN fecha_fin DATE) 
BEGIN
    DECLARE CONTINUE HANDLER FOR SQLEXCEPTION 
    BEGIN
        SELECT 'Error: Ha ocurrido un problema con la consulta.' AS Mensaje;
    END;
    IF fecha_inicio > fecha_fin THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error: La fecha de inicio debe ser anterior a la fecha final';
    ELSE
        SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
        FROM cliente c
        INNER JOIN pedido p ON c.id = p.id_cliente
        WHERE p.fecha BETWEEN fecha_inicio AND fecha_fin
        ORDER BY p.fecha;
    END IF;
END //
DELIMITER ;
CALL clientes_pedidos_por_rango_fechas_handlerException('2023-01-01', '2018-12-31');  -- Mostrará error
CALL clientes_pedidos_por_rango_fechas_handlerException('2016-01-01', '2018-12-31');  -- Funcionará correctamente

DELIMITER //
CREATE PROCEDURE clientes_pedidos_por_rango_fechas( IN fecha_inicio DATE, 
   
    IN fecha_fin DATE
)
BEGIN
    SELECT 
        c.nombre, 
        c.apellido1, 
        p.id AS id_pedido, 
        p.total, 
        p.fecha
    FROM cliente c
    INNER JOIN pedido p ON c.id = p.id_cliente
    WHERE p.fecha BETWEEN fecha_inicio AND fecha_fin
    ORDER BY p.fecha;
END //
DELIMITER ;

CALL clientes_pedidos_por_rango_fechas(NULL, NULL);  -- 🔹 Devuelve todos los pedidos.
CALL clientes_pedidos_por_rango_fechas(NULL, '2017-12-31');  -- 🔹 Devuelve pedidos hasta 2017-12-31.
CALL clientes_pedidos_por_rango_fechas('2016-01-01', NULL);  -- 🔹 Devuelve pedidos desde 2016-01-01.
CALL clientes_pedidos_por_rango_fechas('2016-01-01', '2017-12-31');  -- 🔹 Devuelve pedidos en ese rango.

DELIMITER //

CREATE PROCEDURE clientes_pedidos_por_rango_fechas_condicional( IN fecha_inicio DATE,IN fecha_fin DATE)
BEGIN
    -- Manejo de diferentes casos con IF
    IF fecha_inicio IS NULL AND fecha_fin IS NULL THEN
        -- Si ambas fechas son NULL, mostrar todos los pedidos
        SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
        FROM cliente c
        INNER JOIN pedido p ON c.id = p.id_cliente
        ORDER BY p.fecha;
        
    ELSEIF fecha_inicio IS NULL THEN
        -- Si solo fecha_inicio es NULL, mostrar pedidos hasta fecha_fin
        SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
        FROM cliente c
        INNER JOIN pedido p ON c.id = p.id_cliente
        WHERE p.fecha <= fecha_fin
        ORDER BY p.fecha;

    ELSEIF fecha_fin IS NULL THEN
        -- Si solo fecha_fin es NULL, mostrar pedidos desde fecha_inicio
        SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
        FROM cliente c
        INNER JOIN pedido p ON c.id = p.id_cliente
        WHERE p.fecha >= fecha_inicio
        ORDER BY p.fecha;
        
    ELSE
        -- Si ambas fechas son válidas, filtrar por rango
        SELECT c.nombre, c.apellido1, p.id AS id_pedido, p.total, p.fecha
        FROM cliente c
        INNER JOIN pedido p ON c.id = p.id_cliente
        WHERE p.fecha BETWEEN fecha_inicio AND fecha_fin
        ORDER BY p.fecha;
    END IF;
END //
DELIMITER ;
CALL clientes_pedidos_por_rango_fechas_condicional(NULL, NULL);  -- 🔹 Devuelve todos los pedidos.
CALL clientes_pedidos_por_rango_fechas_condicional(NULL, '2017-12-31');  -- 🔹 Devuelve pedidos hasta 2017-12-31.
CALL clientes_pedidos_por_rango_fechas_condicional('2016-01-01', NULL);  -- 🔹 Devuelve pedidos desde 2016-01-01.
CALL clientes_pedidos_por_rango_fechas_condicional('2016-01-01', '2017-12-31');  -- 🔹 Devuelve pedidos en ese rango.



