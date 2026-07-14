-- Consultas de 1 tabla
/*1 .-Mostrar paradas que tienen marquesina*/
SELECT * FROM paradas WHERE marquesina = TRUE;

/* 2.- Mostrar autobuses con capacidad mayor a 25 asientos*/
SELECT * FROM autobus WHERE num_as > 25;

/*3.- Mostrar conductores contratados después del 2010*/
SELECT * FROM conductor WHERE fecha_ini_tra >= '2010-01-01'; 

/*4.-Mostrar paradas con frecuencia menor a 10 minutos*/
SELECT * FROM paradas WHERE frecuencia < 10;

/*5.-Contar número de autobuses por capacidad*/
SELECT num_as, COUNT(*) as cantidad 
FROM autobus 
GROUP BY num_as;

/*6.-Mostrar conductores ordenados alfabéticamente*/
SELECT * FROM conductor ORDER BY apellido, nombre;

/*7.Mostrar conductores contratados antes del 2010*/
SELECT * FROM conductor WHERE fecha_ini_tra < '2010-01-01';

/*8.-Conductores con mayor o igual de 5 años de antigüedad*/
SELECT c.nombre, c.apellido, 
       TIMESTAMPDIFF(YEAR, c.fecha_ini_tra, CURDATE()) as años_experiencia,
       a.num_as, l.detalle as linea
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
WHERE TIMESTAMPDIFF(YEAR, c.fecha_ini_tra, CURDATE()) >= 5;


-- Consultas de 2 tablas
/* 9.- Mostrar paradas con el nombre de su línea correspondiente*/
SELECT p.*, l.detalle AS nombre_linea 
FROM paradas p
JOIN linea l ON p.id_l = l.id;

/*9.- Mostrar autobuses con su línea asignada*/
SELECT a.*, l.detalle AS linea_asignada
FROM autobus a
JOIN linea l ON a.id_l = l.id;

/*10.- Mostrar conductores con el autobús que manejan*/
SELECT c.*, a.num_as AS capacidad_autobus
FROM conductor c
JOIN autobus a ON c.id_a = a.id;

/*11.- Mostrar paradas de la línea "1ro de mayo"*/
SELECT p.* 
FROM paradas p
JOIN linea l ON p.id_l = l.id
WHERE l.detalle = '1ro de mayo';

/*12.-Mostrar autobuses con su línea y capacidad mayor a 25 asientos*/
SELECT a.*, l.detalle 
FROM autobus a
JOIN linea l ON a.id_l = l.id
WHERE a.num_as > 25;

/*13.-Mostrar conductores con su autobús y línea ordenada alfabeticamente (solo para líneas específicas)*/
SELECT c.nombre, c.apellido, a.num_as, l.detalle
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
WHERE l.detalle IN ('1ro de mayo', '2 de agosto')
order by l.detalle ;

/*14.-Mostrar paradas con el nombre de su línea correspondiente*/
SELECT p.*, l.detalle AS nombre_linea 
FROM paradas p JOIN linea l ON p.id_l = l.id;

/*15.-Listar autobuses con el nombre de su línea asignada*/
SELECT a.*, l.detalle AS linea_asignada
FROM autobus a JOIN linea l ON a.id_l = l.id;

/*16.Listar paradas de la línea "1ro de mayo"*/
SELECT p.* 
FROM paradas p JOIN linea l ON p.id_l = l.id
WHERE l.detalle = '1ro de mayo';

/*17. Paradas con su línea correspondiente (paradas + línea)*/
SELECT p.*, l.detalle AS nombre_linea 
FROM paradas p JOIN linea l ON p.id_l = l.id;

/*18. Autobuses con su línea (autobuses + línea)*/
SELECT a.*, l.detalle 
FROM autobus a JOIN linea l ON a.id_l = l.id;

/*19. Conductores con su autobús (conductores + autobuses)*/
SELECT c.*, a.num_as 
FROM conductor c JOIN autobus a ON c.id_a = a.id;


-- Consultas de 3 tablas
/*20.-Mostrar conductores con su autobús y línea asignada*/
SELECT c.nombre, c.apellido, a.num_as, l.detalle AS linea
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id;

/*21.-Mostrar paradas con su línea y autobuses asignados*/
SELECT p.direccion, l.detalle AS linea, a.num_as AS capacidad_autobus
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l;

/*22.-Mostrar conductores con más antigüedad (ordenados) con su línea*/
SELECT c.nombre, c.apellido, c.fecha_ini_tra, l.detalle AS linea
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
ORDER BY c.fecha_ini_tra ASC;

/*23.-Contar cuántos conductores tiene cada línea*/
SELECT l.detalle AS linea, COUNT(c.dni) AS cantidad_conductores
FROM linea l
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;

/*24.-Calcular antigüedad promedio de conductores por línea (esta redondeado a 2 decimales )*/
SELECT 
    l.detalle, 
    ROUND(AVG(DATEDIFF(CURDATE(), c.fecha_ini_tra)/365), 2) AS antiguedad_promedio
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
GROUP BY l.detalle;

/*25.-Mostrar autobuses con su línea y número de conductores asignados*/
SELECT a.id, a.num_as, l.detalle, COUNT(c.dni) as conductores
FROM autobus a
JOIN linea l ON a.id_l = l.id
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY a.id, a.num_as, l.detalle;

/*26.-Mostrar conductores ordenados por antigüedad con su línea*/
SELECT c.nombre, c.apellido, c.fecha_ini_tra, l.detalle AS linea
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
ORDER BY c.fecha_ini_tra;

/*27.-Contar cuántos conductores tiene cada línea*/
SELECT l.detalle AS linea, COUNT(c.dni) AS conductores
FROM linea l
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;

/*28. Conductores con su autobús y línea (conductores + autobuses + líneas)*/
SELECT c.nombre, c.apellido, a.num_as, l.detalle AS linea
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id;

/*29. Paradas con línea y autobús (paradas + líneas + autobuses)*/
SELECT p.direccion, l.detalle, a.num_as
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l;

/*30. Autobuses con línea y conteo de conductores (autobuses + líneas + conductores)*/
SELECT a.id, l.detalle, COUNT(c.dni) AS conductores
FROM autobus a
JOIN linea l ON a.id_l = l.id
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY a.id, l.detalle;

/*31.-Muestra cada parada con el autobús y conductor que la atiende.*/
SELECT p.direccion, a.num_as, CONCAT(c.nombre, ' ', c.apellido) as conductor
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

/*32.-Lista conductores con más de 10 años de servicio y sus vehículos.*/
SELECT c.nombre, c.apellido, l.detalle, a.num_as
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
WHERE DATEDIFF(CURDATE(), c.fecha_ini_tra)/365 > 10;

/*33.-Calcula la capacidad promedio de autobuses por línea.*/
SELECT l.detalle, AVG(a.num_as) as capacidad_promedio
FROM linea l
JOIN autobus a ON l.id = a.id_l
GROUP BY l.detalle;

/*34.-Muestra conductores asignados a autobuses de menor capacidad.*/
SELECT c.nombre, c.apellido, a.num_as, l.detalle
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
WHERE a.num_as < 30;

/*35.-Líneas con cantidad de conductores y autobuses*/
SELECT l.detalle, 
       COUNT(DISTINCT a.id) as autobuses,
       COUNT(DISTINCT c.dni) as conductores
FROM linea l
LEFT JOIN autobus a ON l.id = a.id_l
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;

/*36.-Identifica autobuses manejados por más de un conductor.*/
SELECT a.id, COUNT(c.dni) as conductores, l.detalle
FROM autobus a
JOIN conductor c ON a.id = c.id_a
JOIN linea l ON a.id_l = l.id
GROUP BY a.id
HAVING COUNT(c.dni) > 1;

/*37.-Calcula cuántos años llevan en promedio los conductores de cada línea.*/
SELECT l.detalle, 
       AVG(DATEDIFF(CURDATE(), c.fecha_ini_tra)/365) as antiguedad_promedio
FROM linea l
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;


-- Consultas de 4 tablas
/*38.-Mostrar información completa: conductores, autobuses, líneas y paradas*/
SELECT c.nombre, c.apellido, a.num_as, l.detalle AS linea, p.direccion AS parada
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
JOIN paradas p ON l.id = p.id_l;

/*39.-Mostrar horarios de paradas con información completa*/
SELECT p.direccion, p.hora_ll, p.frecuencia, 
       l.detalle AS linea, a.num_as AS capacidad, 
       CONCAT(c.nombre, ' ', c.apellido) AS conductor
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

/*40.-Mostrar todas las paradas con su línea, autobús asignado y conductor*/
SELECT p.direccion AS parada, 
       l.detalle AS linea, 
       a.num_as AS capacidad_autobus,
       CONCAT(c.nombre, ' ', c.apellido) AS conductor,
       c.fecha_ini_tra AS fecha_contratacion
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

/*41.-Estadísticas completas del sistema*/
SELECT l.detalle AS linea, 
       COUNT(DISTINCT p.id) AS cantidad_paradas,
       COUNT(DISTINCT a.id) AS cantidad_autobuses,
       COUNT(DISTINCT c.dni) AS cantidad_conductores,
       AVG(a.num_as) AS promedio_capacidad
FROM linea l
LEFT JOIN paradas p ON l.id = p.id_l
LEFT JOIN autobus a ON l.id = a.id_l
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;

/*42.-Mostrar paradas con todos sus datos relacionados (formateado para reporte)*/
SELECT 
    p.direccion AS Parada,
    CASE p.marquesina WHEN TRUE THEN 'Sí' ELSE 'No' END AS Marquesina,
    TIME_FORMAT(p.hora_ll, '%h:%i %p') AS Hora,
    CONCAT(p.frecuencia, ' min') AS Frecuencia,
    l.detalle AS Línea,
    CONCAT(a.num_as, ' asientos') AS Capacidad,
    CONCAT(c.nombre, ' ', c.apellido) AS Conductor
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

/*43.-Mostrar información completa: conductores, autobuses, líneas y paradas*/
SELECT c.nombre, c.apellido, a.num_as, l.detalle, p.direccion
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
JOIN paradas p ON l.id = p.id_l;

/*44.-Listar horarios de paradas con información completa*/
SELECT p.direccion, p.hora_ll, l.detalle, a.num_as, c.nombre, c.apellido
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

/*45.-Mostrar todas las paradas con su línea, autobús y conductor asignado*/
SELECT p.direccion, l.detalle, a.num_as, CONCAT(c.nombre, ' ', c.apellido) AS conductor
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

/*46. Info completa: paradas + líneas + autobuses + conductores*/
SELECT p.direccion, l.detalle, a.num_as, CONCAT(c.nombre, ' ', c.apellido) AS conductor
FROM paradas p
JOIN linea l ON p.id_l = l.id  -- Paradas → Línea
JOIN autobus a ON l.id = a.id_l  -- Línea → Autobús
JOIN conductor c ON a.id = c.id_a;  -- Autobús → Conductor

/*47. Reporte estadístico completo (4 tablas con agregaciones)*/
SELECT 
    l.detalle,
    COUNT(DISTINCT p.id) AS paradas,
    COUNT(DISTINCT a.id) AS autobuses,
    COUNT(DISTINCT c.dni) AS conductores
FROM linea l
LEFT JOIN paradas p ON l.id = p.id_l
LEFT JOIN autobus a ON l.id = a.id_l
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;

/*48.-Muestra cada autobús con su conductor y todas sus paradas.*/
SELECT 
    a.id,
    a.num_as,
    l.detalle as linea,
    CONCAT(c.nombre, ' ', c.apellido) as conductor,
    GROUP_CONCAT(p.direccion SEPARATOR '; ') as paradas
FROM autobus a
JOIN linea l ON a.id_l = l.id
LEFT JOIN conductor c ON a.id = c.id_a
JOIN paradas p ON l.id = p.id_l
GROUP BY a.id, l.detalle, conductor;

-- 49.- Contar registros por tabla
SELECT 'Total de líneas:' as DESCRIPCION, COUNT(*) as CANTIDAD FROM linea
UNION ALL
SELECT 'Total de autobuses:', COUNT(*) FROM autobus
UNION ALL
SELECT 'Total de paradas:', COUNT(*) FROM paradas
UNION ALL
SELECT 'Total de conductores:', COUNT(*) FROM conductor;

-- 50.-Consulta con JOINS para ver información completa
SELECT 
    l.detalle as LINEA,
    a.num_as as ASIENTOS,
    CONCAT(c.nombre, ' ', c.apellido) as CONDUCTOR,
    c.fecha_ini_tra as FECHA_INICIO
FROM linea l
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a
ORDER BY l.detalle;
