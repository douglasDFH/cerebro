(localdb)\MSSQLLocalDB

create database db_autobus;
use db_autobus;

create table linea(
	id int not null auto_increment,
    detalle varchar(50) not null,
    primary key(id)
);

insert into linea(detalle) 
values('1ro de mayo'),('2 de agosto'),('El mechero ');
select * from linea;

drop table paradas;
create table paradas(
	id int not null auto_increment,
    direccion varchar(50) not null,
    marquesina bool not null,
    hora_ll time not null,
    frecuencia int not null,
    primary key(id),
    id_l int not null,
    foreign key(id_l) references linea(id)
);

insert into paradas(direccion,marquesina,hora_ll,frecuencia,id_l)
values('Barrio el trapiche',false,'09:05:00',5,1),
('Barrio las hamacas',true,'09:20:00',5,1),
('Casco viejo',true,'09:35:00',5,1),
('Barrio los pinos',false,'08:50:00',5,1);

select * from paradas;


create table autobus(
	id int not null auto_increment,
    num_as int not null,
    primary key(id),
    id_l int not null,
    foreign key(id_l) references linea(id)
);

insert into autobus(num_as,id_l)
values(30,1),(40,2),(20,3);
select * from autobus;


create table conductor(
	dni int not null auto_increment,
    nombre varchar(50) not null,
    apellido varchar(50) not null,
    fecha_ini_tra date not null,
    primary key(dni),
    id_a int not null,
    foreign key(id_a) references autobus(id)
);
insert into conductor(nombre,apellido,fecha_ini_tra,id_a)
values
('Mario','Suarez','2010-01-01',2),('Matias','Peña','2020-01-05',2),
('Jose','Dorado','2000-01-01',1),('Marco','Días','2020-01-10',3),
('Pedro','Fernandez','2005-12-01',1),('Juan','Perez','2000-12-20',1);
select * from conductor;

-- mostras los datos
select * from linea;
select * from paradas;
select * from autobus;
select * from conductor;

-- ------------------------------------
-- DATOS ----
INSERT INTO linea(detalle) 
VALUES
('Plan 3000'),
('Equipetrol'),
('Las Palmas'),
('Villa 1ro de Mayo'),
('Barrio San Aurelio'),
('Villa San José'),
('Norte'),
('Sur'),
('Este'),
('Oeste'),
('La Guardia'),
('Warnes'),
('Cotoca'),
('Urubó'),
('Los Pozos'),
('Palmar del Oratorio'),
('Villa Olímpica'),
('Barrio Petrolero'),
('Santos Dumont'),
('Mutualista'),
('Villa Busch');

INSERT INTO autobus(num_as, id_l)
VALUES
(35, 1), (45, 1), (25, 1), -- Más autobuses para línea 1
(30, 2), (35, 2), (40, 2), -- Más autobuses para línea 2
(25, 3), (30, 3), (35, 3), -- Más autobuses para línea 3
(32, 4), (38, 4), (42, 4), -- Autobuses para línea 4
(28, 5), (33, 5), (37, 5), -- Autobuses para línea 5
(29, 6), (34, 6), (39, 6), -- Autobuses para línea 6
(31, 7), (36, 7), (41, 7), -- Autobuses para línea 7
(27, 8), (32, 8), (38, 8), -- Autobuses para línea 8
(26, 9), (31, 9), (36, 9), -- Autobuses para línea 9
(33, 10), (38, 10), (43, 10); -- Autobuses para línea 10

--- ============================================
-- POBLAR TABLA PARADAS (agregar más paradas de Santa Cruz)
-- ============================================
INSERT INTO paradas(direccion, marquesina, hora_ll, frecuencia, id_l)
VALUES
-- Paradas para línea 1 (1ro de mayo)
('Plaza 24 de Septiembre', true, '09:50:00', 5, 1),
('Terminal Bimodal', true, '10:05:00', 5, 1),
('Mercado Los Pozos', false, '10:20:00', 5, 1),

-- Paradas para línea 2 (2 de agosto)
('Av. Cristo Redentor', true, '08:00:00', 8, 2),
('4to Anillo', true, '08:15:00', 8, 2),
('Avenida Banzer', true, '08:30:00', 8, 2),
('Barrio Petrolero', false, '08:45:00', 8, 2),

-- Paradas para línea 3 (El mechero)
('Plan 3000 Terminal', true, '07:30:00', 10, 3),
('Radial 10', false, '07:50:00', 10, 3),
('Radial 13', true, '08:10:00', 10, 3),
('Barrio San Luis', false, '08:30:00', 10, 3),

-- Paradas para línea 4 (Plan 3000)
('Plan 3000 Centro', true, '06:30:00', 12, 4),
('Villa 1ro de Mayo', true, '06:50:00', 12, 4),
('Mercado Mutualista', true, '07:10:00', 12, 4),
('Av. Santos Dumont', false, '07:30:00', 12, 4),

-- Paradas para línea 5 (Equipetrol)
('Equipetrol Norte', true, '07:00:00', 7, 5),
('Las Palmas Shopping', true, '07:15:00', 7, 5),
('Ventura Mall', false, '07:30:00', 7, 5),
('Barrio Hamacas', true, '07:45:00', 7, 5),

-- Paradas para línea 6 (Las Palmas)
('Las Palmas Centro', true, '06:00:00', 15, 6),
('Urubó', false, '06:20:00', 15, 6),
('La Hacienda', true, '06:40:00', 15, 6),
('Palmar del Oratorio', false, '07:00:00', 15, 6),

-- Paradas para línea 7 (Villa 1ro de Mayo)
('Villa 1ro de Mayo Terminal', true, '05:30:00', 6, 7),
('Av. Alemana', true, '05:45:00', 6, 7),
('2do Anillo', false, '06:00:00', 6, 7),
('Centro Histórico', true, '06:15:00', 6, 7),

-- Paradas para línea 8 (Barrio San Aurelio)
('San Aurelio Centro', true, '06:45:00', 8, 8),
('Villa Olímpica', false, '07:00:00', 8, 8),
('Av. Roca y Coronado', true, '07:15:00', 8, 8),
('3er Anillo Interno', true, '07:30:00', 8, 8);

INSERT INTO conductor(nombre, apellido, fecha_ini_tra, id_a)
VALUES
-- Conductores adicionales para los autobuses existentes y nuevos
('Carlos', 'Mendoza', '2018-03-15', 4),
('Luis', 'Vargas', '2019-07-20', 5),
('Roberto', 'Chávez', '2017-11-10', 6),
('Miguel', 'Rojas', '2020-04-05', 7),
('Fernando', 'García', '2016-09-12', 8),
('Andrés', 'López', '2021-01-18', 9),
('Eduardo', 'Morales', '2015-05-30', 10),
('Raúl', 'Herrera', '2019-02-14', 11),
('Diego', 'Silva', '2018-08-25', 12),
('Javier', 'Campos', '2017-12-03', 13),
('Alberto', 'Vaca', '2020-06-08', 14),
('Sergio', 'Ramos', '2016-10-22', 15),
('Daniel', 'Aguirre', '2019-04-17', 16),
('Gabriel', 'Flores', '2018-01-28', 17),
('Arturo', 'Zabala', '2021-09-05', 18),
('Ricardo', 'Justiniano', '2017-03-11', 19),
('Óscar', 'Antelo', '2020-11-19', 20),
('Hugo', 'Ribera', '2015-07-14', 21),
('Víctor', 'Saavedra', '2019-12-02', 22),
('Manuel', 'Costas', '2018-05-16', 23),
('Jesús', 'Mercado', '2016-08-07', 24),
('Antonio', 'Peña', '2021-02-23', 25),
('Francisco', 'Saucedo', '2017-06-09', 26),
('Gonzalo', 'Montero', '2020-03-28', 27);

