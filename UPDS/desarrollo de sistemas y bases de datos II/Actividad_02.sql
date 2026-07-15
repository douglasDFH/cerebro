CREATE DATABASE db_actividad2;
USE db_actividad2;


CREATE TABLE fabricante (
    codigo INT(10) PRIMARY KEY,
    nombre VARCHAR(100)
);

CREATE TABLE producto (
    codigo INT(10) PRIMARY KEY,
    nombre VARCHAR(100),
    precio DOUBLE,
    codigo_fabricante INT(10),
    FOREIGN KEY (codigo_fabricante) REFERENCES fabricante(codigo)
);


INSERT INTO fabricante (codigo, nombre) VALUES 
(1, 'Asus'),
(2, 'Lenovo'),
(3, 'Hewlett-Packard'),
(4, 'Samsung'),
(5, 'Seagate'),
(6, 'Crucial'),
(7, 'Gigabyte'),
(8, 'Huawei'),
(9, 'Xiaomi');



INSERT INTO producto (codigo, nombre, precio, codigo_fabricante) VALUES 
(1, 'Disco duro SATA3 1TB', 86.99, 5),
(2, 'Memoria RAM DDR4 8GB', 120.00, 6),
(3, 'Disco SSD 1 TB', 150.99, 4),
(4, 'GeForce GTX 1050Ti', 185.00, 7),
(5, 'GeForce GTX 1080 Xtreme', 755.00, 6),
(6, 'Monitor 24 LED Full HD', 202.00, 1),
(7, 'Monitor 27 LED Full HD', 245.99, 1),
(8, 'Portátil Yoga 520', 559.00, 2),
(9, 'Portátil Ideapad 320', 444.00, 2),
(10, 'Impresora HP Deskjet 3720', 59.99, 3),
(11, 'Impresora HP Laserjet Pro M26nw', 180.00, 3);



SELECT nombre FROM producto;


SELECT nombre, precio FROM producto;


SELECT * FROM producto;


SELECT nombre, precio AS precio_euros, (precio * 1.08) AS precio_dolares 
FROM producto;


SELECT nombre AS 'nombre de producto', 
       precio AS 'euros', 
       (precio * 1.08) AS 'dólares'
FROM producto;



SELECT UPPER(nombre) AS nombre, precio 
FROM producto;



SELECT LOWER(nombre) AS nombre, precio 
FROM producto;



SELECT nombre, UPPER(LEFT(nombre, 2)) AS dos_primeros_caracteres 
FROM fabricante;



SELECT nombre, ROUND(precio) AS precio_redondeado 
FROM producto;



SELECT nombre, TRUNCATE(precio, 0) AS precio_sin_decimales 
FROM producto;



SELECT codigo_fabricante 
FROM producto;



SELECT DISTINCT codigo_fabricante 
FROM producto;



SELECT nombre 
FROM fabricante 
ORDER BY nombre ASC;



SELECT nombre 
FROM fabricante 
ORDER BY nombre DESC;



SELECT nombre, precio 
FROM producto 
ORDER BY nombre ASC, precio DESC;



SELECT * FROM fabricante 
LIMIT 5;



SELECT * FROM fabricante 
LIMIT 3,2;



SELECT nombre, precio FROM producto 
ORDER BY precio ASC 
LIMIT 1;



SELECT nombre, precio FROM producto 
ORDER BY precio DESC 
LIMIT 1;



SELECT nombre FROM producto 
WHERE codigo_fabricante = 2;



SELECT nombre FROM producto 
WHERE precio <= 120;



SELECT nombre FROM producto 
WHERE precio >= 400;



SELECT nombre FROM producto 
WHERE precio <= 400;



SELECT nombre, precio FROM producto 
WHERE precio >= 80 AND precio <= 300;



SELECT nombre, precio FROM producto 
WHERE precio BETWEEN 60 AND 200;



SELECT nombre, precio FROM producto 
WHERE precio > 200 AND codigo_fabricante = 6;



SELECT nombre FROM producto 
WHERE codigo_fabricante = 1 OR codigo_fabricante = 3 OR codigo_fabricante = 5;



SELECT nombre FROM producto 
WHERE codigo_fabricante IN (1, 3, 5);



SELECT nombre, (precio * 100) AS céntimos 
FROM producto;



SELECT nombre FROM fabricante 
WHERE nombre LIKE 'S%';



SELECT nombre FROM fabricante 
WHERE nombre LIKE '%e';



SELECT nombre FROM fabricante 
WHERE nombre LIKE '%w%';



SELECT nombre FROM fabricante 
WHERE LENGTH(nombre) = 4;



SELECT nombre FROM producto 
WHERE nombre LIKE '%Portátil%';



SELECT nombre FROM producto 
WHERE nombre LIKE '%Monitor%' 
AND precio < 215;



SELECT nombre, precio FROM producto 
WHERE precio >= 180 
ORDER BY precio DESC, nombre ASC;