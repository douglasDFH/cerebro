-- =============================================
-- CUESTIONARIO DE CONSULTAS SQL
-- Sistema de Gestión de Autobuses
-- 20 Preguntas con sus respuestas
-- =============================================

-- 1. Mostrar todas las líneas de autobuses
SELECT * FROM linea;

-- 2. Listar solo los nombres de las líneas ordenadas alfabéticamente
SELECT detalle FROM linea ORDER BY detalle;

-- 3. Mostrar las paradas que tienen marquesina
SELECT * FROM paradas WHERE marquesina = TRUE;

-- 4. Contar cuántas paradas hay en total
SELECT COUNT(*) AS total_paradas FROM paradas;

-- 5. Mostrar autobuses con capacidad entre 20 y 35 asientos
SELECT * FROM autobus WHERE num_as BETWEEN 20 AND 35;

-- 6. Listar conductores ordenados por fecha de contratación (más antiguos primero)
SELECT * FROM conductor ORDER BY fecha_ini_tra ASC;

-- 7. Mostrar paradas con su línea correspondiente (2 tablas)
SELECT p.*, l.detalle AS nombre_linea 
FROM paradas p JOIN linea l ON p.id_l = l.id;

-- 8. Listar autobuses con su línea y capacidad mayor a 25 asientos
SELECT a.*, l.detalle 
FROM autobus a JOIN linea l ON a.id_l = l.id 
WHERE a.num_as > 25;

-- 9. Mostrar conductores con el modelo y capacidad de su autobús
SELECT c.*, a.num_as 
FROM conductor c JOIN autobus a ON c.id_a = a.id;

-- 10. Contar cuántas paradas tiene cada línea
SELECT l.detalle, COUNT(p.id) AS total_paradas
FROM linea l LEFT JOIN paradas p ON l.id = p.id_l
GROUP BY l.detalle;

-- 11. Mostrar conductores con su autobús y línea asignada (3 tablas)
SELECT c.nombre, c.apellido, a.num_as, l.detalle AS linea
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id;

-- 12. Listar paradas con su línea y capacidad del autobús (ordenadas por hora)
SELECT p.direccion, p.hora_ll, l.detalle, a.num_as
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
ORDER BY p.hora_ll;

-- 13. Calcular antigüedad promedio de conductores por línea (en años)
SELECT l.detalle, AVG(DATEDIFF(CURDATE(), c.fecha_ini_tra)/365) AS antiguedad_promedio
FROM conductor c
JOIN autobus a ON c.id_a = a.id
JOIN linea l ON a.id_l = l.id
GROUP BY l.detalle;

-- 14. Mostrar autobuses con su línea y número de conductores asignados
SELECT a.id, a.num_as, l.detalle, COUNT(c.dni) AS conductores
FROM autobus a
JOIN linea l ON a.id_l = l.id
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY a.id, a.num_as, l.detalle;

-- 15. Mostrar información completa de paradas con todos sus datos (4 tablas)
SELECT p.direccion, l.detalle, a.num_as, CONCAT(c.nombre, ' ', c.apellido) AS conductor
FROM paradas p
JOIN linea l ON p.id_l = l.id
JOIN autobus a ON l.id = a.id_l
JOIN conductor c ON a.id = c.id_a;

-- 16. Generar reporte de líneas con sus estadísticas completas
SELECT 
    l.detalle AS linea,
    COUNT(DISTINCT p.id) AS paradas,
    COUNT(DISTINCT a.id) AS autobuses,
    COUNT(DISTINCT c.dni) AS conductores,
    AVG(a.num_as) AS capacidad_promedio
FROM linea l
LEFT JOIN paradas p ON l.id = p.id_l
LEFT JOIN autobus a ON l.id = a.id_l
LEFT JOIN conductor c ON a.id = c.id_a
GROUP BY l.detalle;

-- 17. Mostrar las 3 paradas más frecuentes (menor tiempo entre pasadas)
SELECT * FROM paradas ORDER BY frecuencia ASC LIMIT 3;

-- 18. Listar conductores que manejan autobuses de más de 30 asientos
SELECT c.* 
FROM conductor c
JOIN autobus a ON c.id_a = a.id
WHERE a.num_as > 30;

-- 19. Mostrar la línea con mayor cantidad de paradas
SELECT l.detalle, COUNT(p.id) AS total_paradas
FROM linea l
JOIN paradas p ON l.id = p.id_l
GROUP BY l.detalle
ORDER BY total_paradas DESC
LIMIT 1;

-- 20. Calcular la capacidad total de pasajeros por línea
SELECT l.detalle, SUM(a.num_as) AS capacidad_total
FROM linea l
JOIN autobus a ON l.id = a.id_l
GROUP BY l.detalle;

-- =============================================
-- FIN DEL CUESTIONARIO
-- =============================================