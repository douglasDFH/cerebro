create database db_actividad2;
USE db_actividad2;
CREATE TABLE fabricante (
    codigo INT(10) PRIMARY KEY,
    nombre VARCHAR(100)
);
INSERT INTO fabricante (nombre) VALUES
('Asus'),
('Lenovo'),
('Hewlett-Packard'),
('Samsung'),
('Seagate'),
('Crucial'),
('Gigabyte'),
('Huawei'),
('Xiaomi');

/*para cambiar a Aunto incrementado en la tabla fabricante */
ALTER TABLE fabricante MODIFY codigo INT(10) NOT NULL AUTO_INCREMENT;

CREATE TABLE producto (
    codigo INT(10) NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    precio double,
    codigo_fabricante INT(10),
    FOREIGN KEY (codigo_fabricante) REFERENCES fabricante(codigo)
);
select *from producto;

INSERT INTO producto (nombre, precio, codigo_fabricante) VALUES 
('Disco duro SATA3 1TB', 86.99, 5),
('Memoria RAM DDR4 8GB', 120.00, 6),
('Disco SSD 1 TB', 150.99, 4),
('GeForce GTX 1050Ti', 185.00, 7),
('GeForce GTX 1080 Xtreme', 755.00, 6),
('Monitor 24 LED Full HD', 202.00, 1),
('Monitor 27 LED Full HD', 245.99, 1),
('Portátil Yoga 520', 559.00, 2),
('Portátil Ideapad 320', 444.00, 2),
('Impresora HP Deskjet 3720', 59.99, 3),
('Impresora HP Laserjet Pro M26nw', 180.00, 3);
DROP TABLE producto;

-- 1  lista el nombre de todos los productos que hay en una tabla producto 
select nombre from producto ;

-- 2 lista los nombres y los precios de todos los productos de la tabla producto
select nombre,precio from producto;

-- 3 lista de todas las columnas de la tabla producto
select * from producto;

-- 4 lista el nombre de los productos, el precio en euros y el precio en dolares estadounidense USD
select nombre,precio as precio_euro, (precio*1.08) as precio_dolares from producto;

-- 5 lista el nomnbre de los productos el precio en euro y el precio en dolares estadounidense utiliza alias para las columnas : nombre de producto ,euro dolares 
select nombre as 'nombre de producto',
       precio as 'euro',
(precio*1.8)  as 'dolares' from producto;

-- 6 lista los nombres y los precios de todos los productos de la tabla producto convirtiendo los nombres a mayuscula 
select upper (nombre) as nombre,precio from producto;

-- 7 lista los productos y los precios de todos ñlos productosnde la twbla producto convirtiendo los nombre a minuscula
select lower(nombre) as nombre,precio from producto;

-- 8 lista el nombre de todos los fabricante en una columna y en orra columna obtenga en mayuscula los dos primeros caracteres del nombre del fabricante 
select nombre,upper(left(nombre,2)) as dos_primeros_caracteres from fabricante ;

-- 9 lista los nombres y los precios de todso los productos de latabla producto redondeando el valos del precio
select nombre,round(precio) as precio_redondeado from producto;


-- 10 lista los nombre y precio de tosos los productos de la tabla producto truncando el valos del precio para mostrarlo sin ninguna cifra decimal 
select nombre,truncate(precio,0) as precio_sin_decimales from producto;

-- 11 lista el identificador de los fabricante que tiene producto en la tabla producto
select codigo_fabricante from producto;

-- 12 lista el identificador de los fabricante que tienen productos en la tabla productos eliminando los identificadores que aparecen repetidos
select distinct codigo_fabricante from producto; 

-- 13  lista los nombres de los fabricantes qoedenado de forma ascendente 
select nombre from fabricante order by nombre asc;

-- 14 lista los nombre de los fabricantes ordenado de forma descendente 
select nombre from fabricante order by nombre desc;

-- 15 lita los nombre de los producto ordenado en primer lugar por el nomnre de forma ascendente  y en segun do lugar por el precio de forma descendente 
select nombre,precio from producto order by nombre asc, precio desc;

-- 16 devuelve la lista con las 5 primeras filas de la tabla fabricante 
select * from fabricante limit 5;

-- 17 devuelve una lista con 2 filas a partir de la cuarta fila de la tabla fabricante la cuarta fila tambien se dene incluir en la respueste 
select * from fabricante limit 3,2 ;

-- 18 lista el nombre y el precio del producto mas caro (utilize las clausulas orderby y limit )
select nombre,precio from producto order by precio asc limit 1;

-- 19 lista el nomnre y el precio del producto mas caro 
select nombre,precio from producto order by precio desc limit 1;

-- 20 lisrta el nombre de todos los productos del fabricante cuyo identificador de fabricante es ifual a 2
select nombre from producto where codigo_fabricante  =2 ;

-- 21 lista el nombre de los productos que tiene un precio menor o igual a 120 
select nombre from producto where precio <=120 ;

-- 22 lista el nombre de los productos que tienen un precio mayo o igual a 400
select nombre from producto where precio >= 400 ;

-- 23 lista el nombre de los producto que no tiene un precio mayor o igual a 400
select nombre from producto where precio <=400;

-- 24 lista todos los productos que tengan un precio entre 80 y 300 sin utilizar el operador between 
select nombre,precio from producto where precio >=80 and precio<=300;

-- 25 lista todos los productos que tenga un precio entre 60 y 200 utilizando el operador betwwen
select nombre,precio from producto where precio between 60 and 200;

-- 26 lista todos los producots que tengan un precio mayor que 200 y que el identificador de fabricante sea igual a 6
select nombre,precio from producto where precio>200 and codigo_fabricante = 6;

-- 27 lista todos los productos donde el identificador de fabricante sea 1.2 5 sin utilizar el operados in
select * from producto
where codigo_fabricante = 1 or codigo_fabricante = 3 or codigo_fabricante = 5;

-- 28 lista todos los producots donde el identificador de favbricante sea 1.3 o 5 utilizando el operador in
select *from producto
where codigo_fabricante in (1,3,5);

-- 29 lista el nomnbre y el precio de los producots en centimos (habra que multiplicar por 100el valor del precio) cree un alias para la columna que contiene el precio que se llame centrimos 
select nombre,(precio*100)as centimos from producto;

-- 30 lista los nombre de los fabricants cuyo nombre empieze por la letra s 
select nombre from fabricante where nombre like's%';

-- 31 lista los nombres de los fabricantres cuyo nombre contenga el caracrter w 
select nombre  from fabricante where nombre like '%e';

-- 32lista los nombres de los fabricantres cuyo nombre contenga el caracrter w 
select nombre  from fabricante where nombre like '%w%';

-- 33lista los nombre de los fabricanrtes cuyo nombre sea de 4 caracteres 
select nombre from fabricante where length(nombre) =4;

-- 34evuelve una lista con el nombre de todos los productos que contiene la cadena portatil en el nombre 
select nombre from producto
where nombre like '%portatil%';

-- 35 devuelve una lista con el nombre de todos los productos que contienen la cadena monitor en el nombre y tienen un precio infeerior a  215
select nombre from producto
where nombre like '%monitor%' and precio < 215;

-- 36 lista el producto y el precio de todos los producto que tengan un precio mayor p igual 18 ordene el resultado en primer lugar por el precio (en orden descendente) y en segundo lugar po el monbre (en orden ascendente )
select * from producto where precio >=180 order by precio desc,nombre asc;
