CREATE PROCEDURE sp_ReporteVentasAgrupadas
    @FechaInicio DATE,
    @FechaFin DATE,
    @Agrupamiento VARCHAR(10) -- 'dia', 'semana', 'mes', 'año'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @agg VARCHAR(10) = LOWER(LTRIM(RTRIM(@Agrupamiento)));

    IF @agg = 'dia'
    BEGIN
        SELECT 
            CONVERT(VARCHAR(10), CONVERT(DATE, f.fecha_emision)) AS periodo,
            CONVERT(DATE, f.fecha_emision) AS fecha_inicio,
            CONVERT(DATE, f.fecha_emision) AS fecha_fin,
            COUNT(*) AS cantidad_facturas,
            SUM(f.total) AS total_ventas,
            AVG(f.total) AS promedio_ventas
        FROM factura f
        WHERE f.fecha_emision BETWEEN @FechaInicio AND @FechaFin
        GROUP BY CONVERT(DATE, f.fecha_emision)
        ORDER BY MIN(CONVERT(DATE, f.fecha_emision));
    END
    ELSE IF @agg = 'semana'
    BEGIN
        SELECT 
            CONVERT(VARCHAR(12), CONCAT('Sem ', DATEPART(WEEK, f.fecha_emision), ' - ', DATEPART(YEAR, f.fecha_emision))) AS periodo,
            MIN(CONVERT(DATE, f.fecha_emision)) AS fecha_inicio,
            MAX(CONVERT(DATE, f.fecha_emision)) AS fecha_fin,
            COUNT(*) AS cantidad_facturas,
            SUM(f.total) AS total_ventas,
            AVG(f.total) AS promedio_ventas
        FROM factura f
        WHERE f.fecha_emision BETWEEN @FechaInicio AND @FechaFin
        GROUP BY DATEPART(YEAR, f.fecha_emision), DATEPART(WEEK, f.fecha_emision)
        ORDER BY MIN(CONVERT(DATE, f.fecha_emision));
    END
    ELSE IF @agg = 'mes'
    BEGIN
        SELECT 
            CONVERT(VARCHAR(15), FORMAT(f.fecha_emision, 'MMMM yyyy')) AS periodo,
            MIN(CONVERT(DATE, f.fecha_emision)) AS fecha_inicio,
            MAX(CONVERT(DATE, f.fecha_emision)) AS fecha_fin,
            COUNT(*) AS cantidad_facturas,
            SUM(f.total) AS total_ventas,
            AVG(f.total) AS promedio_ventas
        FROM factura f
        WHERE f.fecha_emision BETWEEN @FechaInicio AND @FechaFin
        GROUP BY YEAR(f.fecha_emision), MONTH(f.fecha_emision), FORMAT(f.fecha_emision, 'MMMM yyyy')
        ORDER BY MIN(CONVERT(DATE, f.fecha_emision));
    END
    ELSE IF @agg = 'año' OR @agg = 'ano'
    BEGIN
        SELECT 
            CONVERT(VARCHAR(4), CAST(YEAR(f.fecha_emision) AS VARCHAR(4))) AS periodo,
            MIN(CONVERT(DATE, f.fecha_emision)) AS fecha_inicio,
            MAX(CONVERT(DATE, f.fecha_emision)) AS fecha_fin,
            COUNT(*) AS cantidad_facturas,
            SUM(f.total) AS total_ventas,
            AVG(f.total) AS promedio_ventas
        FROM factura f
        WHERE f.fecha_emision BETWEEN @FechaInicio AND @FechaFin
        GROUP BY YEAR(f.fecha_emision)
        ORDER BY MIN(CONVERT(DATE, f.fecha_emision));
    END
    ELSE
    BEGIN
        RAISERROR('Valor de agrupamiento no válido. Use: dia, semana, mes o año.', 16, 1);
    END
END