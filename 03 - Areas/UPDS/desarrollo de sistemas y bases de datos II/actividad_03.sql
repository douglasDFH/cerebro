create database db_actividad3;
use db_actividad3;

create table departamento(
codigo INT(10) not null auto_increment primary key,
 nombre varchar(100),
 presupuesto double
);
select * from departamento;

-- comando para addicionar gastos 
ALTER TABLE departamento
ADD COLUMN gastos DOUBLE;
  
create table empleado (
codigo int(10) not null auto_increment primary key,
nit varchar(9),
nombre varchar(100),
apellido1 varchar(100),
apellido2 varchar(100),
codigo_departamento int (10),
foreign key (codigo_departamento) references departamento(codigo)
);
select * from empleado;

insert into empleado (nit, nombre, apellido1, apellido2, codigo_departamento) 
values  ('32481596F','Aaron','Rivero','Gomez', 1 ), 
        ('Y5575632D','Adela','Salas','Diaz', 2 ),
        ('R6970642B','Adolfo','Rubio','Flores', 3 ), 
        ('77705545E','Adrian','Suarez',null, 4 ), 
        ('17087203C','Marcos','Loyola','Mendez', 5 ),
        ('28382980M','Maria','Santana','Moreno', 1 ),
        ('80576669X','Pilar','ruiz',null, 2  ),
        ('71651431Z','Pepe','Ruiz','Santana', 3 ),
        ('56399183D','Juan','Gomez','Lopez', 2  ),
        ('46384486H','Diego','Flores','Salas', 5 ),
        ('67389283A','Marta','Herrera','Gil',1 ),
        ('41234836R','Irene','Salas','Flores', null ),
        ('82635162B','Juan Antonio','Saez','Guerrero', null );
        
  -- hay que adicionar gastos en la tabla antes de insertar los datos 
  insert into departamento (nombre, presupuesto, gastos) 
values  ('Desarrollo', 12000 ,6000 ), 
        ('Sistemas',15000,21000 ),
        ('Recursos Humanos',280000,25000), 
        ('Contabilidad',110000,3000),
        ('I+D',375000,380000),
        ('Proyectos',0,0),
        ('Publicidad',0,1000 );
        
      -- devuelve una lista con el nombre y el gasto de los 2 departamentos que tiene menor gastos 
        select nombre, gastos from departamento order by gastos asc limit 2;
        
        -- 2 devuelce una lista con 5 filas a partir de la tercera fila de la tabla empleado 
        -- latercera fila se debe incluir en la respuesta, las respuestas debe incluir todas las columnas de la tabla empleado 7
        select *from empleado limit 5 offset 2;
        
      -- 3 devuelve una lista con el nombre de los departamentos y el presupuesto de aquellos que tienen un presupuesto mayor o igual 
        -- 150000euros 
        select nombre,presupuesto from departamento where presupuesto>=150000;
        
        -- 4 devuelve una lista con el nombre de los departamentos y el gasto de aquellos que tienen menos de 5000euros de gastos 
        select nombre,gastos from departamento where gastos < 5000 ;
        
        -- 5 devuelve una lista con el nombre de los departamentos y el presupuesto 
        -- de aquellos que tiene un presupuestos entre 100000 y 200000 euros sin utilizar el operador between 
        select nombre,presupuesto from departamento where presupuesto >=100000 and presupuesto <=200000;
        
        -- 6 devuelve una lista con el nombre de los departamentos que no tienen un presupuestos entre 100000 y 200000euros 
        -- sin utilizar el operador betwween
        select nombre from departamento where presupuesto < 100000 or presupuesto >200000;
        
        -- 7 devuelve una lista con el nombre de los departamentos que tiene un presupuestos entrwe 100000 y 200000eutos utilizando el oerados between
        select nombre from departamento where presupuesto between 100000 and 200000;
        
        -- 8 devuelve una lista con el nombre de los departamentos que no tiene  un presupuesro entre 100000y 2000000euros utilizando el between 
        select nombre from departamento where presupuesto not between 100000 and 200000;
        
        -- 9 devuelve una lista con el nombre de los departamentos gastos y presupuestos de aquellos departamentos donde los f¿gastos sean mayores que el presupuesto del que disponen
        select * from departamento where gastos > presupuesto;
        
        -- 10 devuelve una lista con el nombre de los departamentos gastos y presupuesto  de aquellos departamentos donde los gastos sean menores que le presupuesto del que disponen
        select* from departamento where gastos < presupuesto;
        
        -- 11 devuelve una lista con el nombre de los departamentos gastos y presupuesto de aquellos departamento donde los gastos sean iguales al presupuesto del que disponen 
        select * from departamento where gastos = presupuesto;
        
        -- 12 lista todos los datos de los empleados cuyo segundo apelliddo sea null 
        select * from empleado where apellido2 is null; 
        
        -- 13 lista todos los datos de los empleados cuyo segundo apellido no sea null
        select * from empleado where apellido2 is not null;
        
        -- 14 lista todos los datos de los empleados cuyo segundo apellido sea <lopez
        select * from empleado where apellido2 ='Lopez';
        
        -- 15 lista todos los datos de los empleados cuyo segundo apellido dea Diaz o moreno sin utilizar el operador in 
        select * from empleado where apellido2 = 'Diaz' or apellido2 ='Moreno';
        
        -- 16 lista todos los datos de los empleados cuyo segundo apellido sea diaz o moreno utilizando el operados in 
        select * from empleado where apellido2 in ('Diaz','Moreno');
        
        -- 17 lista los nombres apellidos y nif de los empleados que trabajen en el departamento 3
        select * from empleado where codigo_departamento =3;
        
        -- 18 lista los nombres apellidos y nit de los empledos que trabajan en los departamentos 2.4 o 5
        select * from empleado where codigo_departamento in (2,4,5);
        
        -- 19 devuelve un listado con los empleados y los datos de los departamentos donde trabaja cada uno 
        select e.*,d.* from empleado e join departamento d on e.codigo_departamento = d.codigo;
        
        -- 20 devuelve un listado con los empleados y los datos de los departamentos donde trabaja cada uno 
        -- oredena el resultado en primer lugar por el nombre del departamento (orden alfabetico ) en segundo lugar 7
        -- por los apellidos y nombre de los empleados 
        select e. *, d.* from empleado e join departamento d on e.codigo_departamento = d.codigo
        order by d.nombre, e.apellido1, e.apellido2, e.nombre;
        
        -- 21 devuelve un listado con el identificador y el nombre del departamento solamente de aquelos depaertamento que tiene empleados 
        select distinct d.codigo, d.nombre from departamento d join empleado e on d.codigo = e.codigo_departamento;
	
    
        -- 22 devuelve un listado con el identificados y el nombre del departamento y el valor del presupuesto actual del que dspone 
        -- solamente de aquellos departamento que tiene empleados el valor del presupuesto actual lo puede calcular restando el valor del presupuesto 
        -- inicial columna presupuesto el valor de los gastos que ah generado columna gastps 
        select  d.codigo, d.nombre, (d.presupuesto - d.gastos) as presupuesto_actual from departamento d join  empleado e ON d.codigo = e.codigo_departamento;
        
        -- 23 devuelve en nombre del departamento donde trabaja el empleado que tiene el nit 38382980m
        select d.nombre from departamento d join empleado e ON d.codigo = e.codigo_departamento 
        where e.nit = '28382980M';
        
        -- 24 devuelve el nombre del departamento donde trabaja el empleado pepe ruiz santana 
        SELECT d.nombre
FROM departamento d
JOIN empleado e ON d.codigo = e.codigo_departamento
WHERE e.nombre = 'Pepe' AND e.apellido1 = 'Ruiz' AND e.apellido2 = 'Santana';

-- 25 devuelve un listado con los datos de los empleados que trabajan en el departamento i+s¿d ordena el resultado alfabeticamente 
     SELECT e.*
FROM empleado e
JOIN departamento d ON e.codigo_departamento = d.codigo
WHERE d.nombre = 'I+D'
ORDER BY e.apellido1, e.apellido2, e.nombre; 

-- 26 devuelve un listado con los deatos de los empleados que trabajan en el departamentod e sistemas contabilidad o i+d ordena el resultado de forma alfabetica 
select e.* from empleado e join departamento d on e.codigo_departamento = d.codigo
 where d.nombre in ('Sistemas', 'Contabilidad', 'I+D')
order by e.apellido1, e.apellido2, e.nombre;


-- 27 devuelve una lista con el nombre de los empleados que teinen los departamentos que no tine  presupuestos entre 10000 y200000euros
select e.* from empleado e join departamento d on e.codigo_departamento =d.codigo 
where d.presupuesto not between 10000 and 20000;


-- 28 	Devuelve un listado con el nombre de los departamentos donde existe algún empleado cuyo segundo apellido sea NULL.
-- nga en cuenta que no debe mostrar nombres de departamentos que estén repetidos.

select e.* from empleado e join departamento d on e.codigo_departamento = d.codigo
where e.apellido2 is null;

--  29.	Devuelve un listado con todos los empleados junto con los datos de los departamentos donde trabajan 
-- te listado también debe incluir los empleados que no tienen ningún departamento asociado.
select e.* from empleado e join departamento d on e.codigo_departamento = d.codigo;
 
SELECT e.*, d.*
FROM empleado e
LEFT JOIN departamento d ON e.codigo_departamento = d.codigo;

-- 30.	Devuelve un listado donde sólo aparezcan aquellos empleados que no tienen ningún departamento asociado.

SELECT *
FROM empleado
WHERE codigo_departamento IS NULL;

-- 31.	Devuelve un listado donde sólo aparezcan aquellos departamentos que no tienen ningún empleado asociado.
SELECT d.*
FROM departamento d
LEFT JOIN empleado e ON d.codigo = e.codigo_departamento
WHERE e.codigo IS NULL;

-- 32.	Devuelve un listado con todos los empleados junto con los datos de los departamentos donde trabajan. El listado debe incluir los empleados que no tienen ningún departamento asociado y los departamentos que no tienen ningún empleado asociado. Ordene el listado alfabéticamente por el nombre del departamento.
SELECT e.*, d.*
FROM empleado e
LEFT JOIN departamento d ON e.codigo_departamento = d.codigo
UNION
SELECT e.*, d.*
FROM empleado e
RIGHT JOIN departamento d ON e.codigo_departamento = d.codigo
WHERE e.codigo IS NULL
ORDER BY d.nombre;

 
 -- 33.	Devuelve un listado con los empleados que no tienen ningún departamento asociado y los departamentos que no tienen ningún empleado asociado. Ordene el listado alfabéticamente por el nombre del departamento.
 
 SELECT e.*, d.*
FROM empleado e
LEFT JOIN departamento d ON e.codigo_departamento = d.codigo
WHERE d.codigo IS NULL
UNION
SELECT e.*, d.*
FROM empleado e
RIGHT JOIN departamento d ON e.codigo_departamento = d.codigo
WHERE e.codigo IS NULL
ORDER BY d.nombre;
 
 
 -- 34.	Calcula la suma del presupuesto de todos los departamentos.
 SELECT MIN(presupuesto) as min_presupuesto
FROM departamento;

-- 35.	Calcula la media del presupuesto de todos los departamentos.
SELECT AVG(presupuesto) as media_presupuesto
FROM departamento;


-- 	36.	Calcula el valor mínimo del presupuesto de todos los departamentos.
SELECT MIN(presupuesto) as min_presupuesto
FROM departamento;


--  37Calcula el nombre del departamento y el presupuesto que tiene asignado, del departamento con menor presupuesto.
SELECT nombre, presupuesto
FROM departamento
WHERE presupuesto = (SELECT MIN(presupuesto) FROM departamento);


-- 38.	Calcula el valor máximo del presupuesto de todos los departamentos.
SELECT MAX(presupuesto) as max_presupuesto
FROM departamento;

-- 	39.	Calcula el nombre del departamento y el presupuesto que tiene asignado, del departamento con mayor presupuesto.
SELECT nombre, presupuesto
FROM departamento
WHERE presupuesto = (SELECT MAX(presupuesto) FROM departamento);


-- 40.	Calcula el número total de empleados que hay en la tabla empleado
SELECT COUNT(*) as total_empleados
FROM empleado;

-- 41.	Calcula el número de empleados que no tienen NULL en su segundo apellido.
SELECT COUNT(*) as empleados_con_segundo_apellido
FROM empleado
WHERE apellido2 IS NOT NULL;


-- 42.	Calcula el número de empleados que hay en cada departamento. Tienes que devolver dos columnas, una con el nombre del departamento y otra con el número de empleados que tiene asignados.
SELECT d.nombre, COUNT(e.codigo) as num_empleados
FROM departamento d
LEFT JOIN empleado e ON d.codigo = e.codigo_departamento
GROUP BY d.codigo, d.nombre;

-- 43.	Calcula el nombre de los departamentos que tienen más de 2 empleados. El resultado debe tener dos columnas, una con el nombre del departamento y otra con el número de empleados que tiene asignados.
SELECT d.nombre, COUNT(e.codigo) as num_empleados
FROM departamento d
JOIN empleado e ON d.codigo = e.codigo_departamento
GROUP BY d.codigo, d.nombre
HAVING COUNT(e.codigo) > 2;


--  44.	Calcula el número de empleados que trabajan en cada uno de los departamentos. El resultado de esta consulta también tiene que incluir aquellos departamentos que no tienen ningún empleado asociado.
SELECT d.nombre, COUNT(e.codigo) as num_empleados
FROM departamento d
LEFT JOIN empleado e ON d.codigo = e.codigo_departamento
GROUP BY d.codigo, d.nombre;

-- 45.	Calcula el número de empleados que trabajan en cada unos de los departamentos que tienen un presupuesto mayor a 200000 euros.
SELECT d.nombre, COUNT(e.codigo) as num_empleados
FROM departamento d
JOIN empleado e ON d.codigo = e.codigo_departamento
WHERE d.presupuesto > 200000
GROUP BY d.codigo, d.nombre;