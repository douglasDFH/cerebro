CREATE OR ALTER PROCEDURE SP_ContarMascotasActivas
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT COUNT(*) as total_mascotas
    FROM animal 
    WHERE activo = 1;
END;
GO

CREATE OR ALTER PROCEDURE SP_ContarProductosActivos
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT COUNT(*) as total_productos
    FROM producto 
    WHERE activo = 1;
END;
GO

CREATE OR ALTER PROCEDURE SP_ContarProductosBajoStock
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT COUNT(*) as productos_bajo_stock
    FROM producto 
    WHERE activo = 1 AND stock_actual <= stock_minimo;
END;
GO

CREATE OR ALTER PROCEDURE SP_CalcularValorInventarioTotal
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ISNULL(SUM(stock_actual * precio), 0) as valor_inventario_total
    FROM producto 
    WHERE activo = 1;
END;
GO

CREATE OR ALTER PROCEDURE SP_EstadisticasMascotasPorEspecie
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        especie,
        COUNT(*) as cantidad,
        ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM animal WHERE activo = 1), 2) as porcentaje
    FROM animal 
    WHERE activo = 1
    GROUP BY especie
    ORDER BY cantidad DESC;
END;
GO

CREATE OR ALTER PROCEDURE SP_EstadisticasProductosPorCategoria
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.nombre as categoria,
        COUNT(p.id) as cantidad_productos,
        SUM(p.stock_actual) as stock_total,
        SUM(p.stock_actual * p.precio) as valor_inventario,
        SUM(CASE WHEN p.stock_actual <= p.stock_minimo THEN 1 ELSE 0 END) as productos_bajo_stock
    FROM categoria c
    LEFT JOIN producto p ON c.id = p.categoria_id AND p.activo = 1
    WHERE c.activo = 1
    GROUP BY c.id, c.nombre
    ORDER BY valor_inventario DESC;
END;
GO

PRINT 'Procedimientos almacenados para estadísticas creados correctamente.';
