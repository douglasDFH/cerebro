

-- Estadísticas de facturas por estado
SELECT estado, COUNT(*) as cantidad, FORMAT(SUM(total), 'C', 'es-BO') as total_monto
FROM factura GROUP BY estado ORDER BY COUNT(*) DESC;

-- Top 5 productos más vendidos
SELECT TOP 5 p.codigo, p.nombre, SUM(dp.cantidad) as total_vendido,
       FORMAT(SUM(dp.subtotal), 'C', 'es-BO') as ingresos
FROM detalle_productos dp
INNER JOIN producto p ON dp.producto_id = p.id
GROUP BY p.codigo, p.nombre
ORDER BY SUM(dp.cantidad) DESC;

-- Top 5 servicios más prestados
SELECT TOP 5 d.codigo, d.nombre, COUNT(*) as veces_prestado,
       FORMAT(SUM(ds.subtotal), 'C', 'es-BO') as ingresos
FROM detalle_servicios ds
INNER JOIN diagnostico d ON ds.diagnostico_id = d.id
GROUP BY d.codigo, d.nombre
ORDER BY COUNT(*) DESC;

-- OBTENEMOS UN ID DE UNA FACTURA GENERADA PARA PRUEBAS
DECLARE @FacturaID INT;
SELECT @FacturaID = MAX(id) FROM factura;

EXEC sp_factura_datos_principales @factura_id = @FacturaID;
EXEC sp_factura_detalle_productos @factura_id = @FacturaID;
EXEC sp_factura_detalle_servicios @factura_id = @FacturaID;
