-- Script para actualizar el stored procedure de reportes de ventas
-- Ejecuta este script en tu base de datos Bike_Store

USE Bike_Store
GO

-- Eliminar el procedimiento existente si existe
IF EXISTS (SELECT * FROM sysobjects WHERE name='sp_reporte_ventas_por_periodo' AND type='P')
    DROP PROCEDURE sp_reporte_ventas_por_periodo
GO

-- Crear el procedimiento actualizado
CREATE PROCEDURE sp_reporte_ventas_por_periodo
    @tipo_periodo VARCHAR(20),
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @tipo_periodo = 'DIARIO'
    BEGIN
        SELECT CAST(o.order_date AS DATE) AS periodo,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY CAST(o.order_date AS DATE)
        ORDER BY periodo DESC
    END
    ELSE IF @tipo_periodo = 'SEMANAL'
    BEGIN
        SELECT CONCAT('Semana ', DATEPART(week, o.order_date), ' - ', YEAR(o.order_date)) AS periodo,
               MIN(o.order_date) AS fecha_inicio_semana,
               MAX(o.order_date) AS fecha_fin_semana,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(o.order_date), DATEPART(week, o.order_date)
        ORDER BY YEAR(o.order_date) DESC, DATEPART(week, o.order_date) DESC
    END
    ELSE IF @tipo_periodo = 'MENSUAL'
    BEGIN
        SELECT CONCAT(DATENAME(month, o.order_date), ' ', YEAR(o.order_date)) AS periodo,
               MONTH(o.order_date) AS mes,
               YEAR(o.order_date) AS año,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(o.order_date), MONTH(o.order_date), DATENAME(month, o.order_date)
        ORDER BY año DESC, mes DESC
    END
    ELSE IF @tipo_periodo = 'ANUAL'
    BEGIN
        SELECT CAST(YEAR(o.order_date) AS VARCHAR) AS periodo,
               YEAR(o.order_date) AS año,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(o.order_date)
        ORDER BY año DESC
    END
    ELSE
    BEGIN
        -- Por defecto, usar DIARIO si no se especifica un período válido
        SELECT CAST(o.order_date AS DATE) AS periodo,
               COUNT(DISTINCT o.order_id) AS total_ordenes,
               SUM(oi.quantity) AS productos_vendidos,
               SUM(oi.quantity * oi.price * (1 - oi.discount)) AS total_ventas
        FROM orders o
        INNER JOIN order_items oi ON o.order_id = oi.order_id
        WHERE o.order_date BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY CAST(o.order_date AS DATE)
        ORDER BY periodo DESC
    END
END
GO

-- Verificar que el procedimiento se creó correctamente
SELECT 
    ROUTINE_NAME as 'Stored Procedure',
    CREATED as 'Fecha Creación',
    LAST_ALTERED as 'Última Modificación'
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_TYPE = 'PROCEDURE' 
AND ROUTINE_NAME = 'sp_reporte_ventas_por_periodo'

-- Mensaje de confirmación
PRINT '✓ Stored procedure sp_reporte_ventas_por_periodo actualizado correctamente'
PRINT '✓ Columnas disponibles: periodo, total_ordenes, productos_vendidos, total_ventas'
PRINT '✓ Períodos soportados: DIARIO, SEMANAL, MENSUAL, ANUAL'