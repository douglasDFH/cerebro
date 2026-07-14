-- ===============================================================================
-- SCRIPT DE DIAGNÓSTICO DE CONEXIÓN A BASE DE DATOS
-- Este script verifica la configuración y estado de la base de datos
-- ===============================================================================

-- Verificar qué instancia de SQL Server estamos usando
PRINT '=== INFORMACIÓN DEL SERVIDOR ==='
SELECT 
    @@SERVERNAME as 'Nombre_Servidor',
    @@VERSION as 'Versión_SQL_Server'

-- Verificar que estamos en la base de datos correcta
PRINT ''
PRINT '=== BASE DE DATOS ACTUAL ==='
SELECT DB_NAME() as 'Base_Datos_Actual'

-- Verificar si existe la base de datos Bike_Store
PRINT ''
PRINT '=== BASES DE DATOS DISPONIBLES ==='
SELECT name as 'Bases_Datos_Disponibles' 
FROM sys.databases 
WHERE name IN ('Bike_Store', 'master', 'tempdb', 'model', 'msdb')
ORDER BY name

-- Cambiar a la base de datos Bike_Store si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Bike_Store')
BEGIN
    USE Bike_Store
    PRINT ''
    PRINT '=== CAMBIANDO A BASE DE DATOS BIKE_STORE ==='
    PRINT 'Base de datos cambiada exitosamente a: ' + DB_NAME()
    
    -- Verificar tablas principales
    PRINT ''
    PRINT '=== TABLAS PRINCIPALES ==='
    SELECT TABLE_NAME as 'Tablas_Disponibles'
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_TYPE = 'BASE TABLE'
    AND TABLE_NAME IN ('users', 'customers', 'products', 'categories', 'orders', 'order_items', 'stores', 'staffs')
    ORDER BY TABLE_NAME
    
    -- Verificar procedimientos almacenados clave
    PRINT ''
    PRINT '=== PROCEDIMIENTOS ALMACENADOS CLAVE ==='
    SELECT ROUTINE_NAME as 'Procedimientos_Disponibles'
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_TYPE = 'PROCEDURE'
    AND ROUTINE_NAME IN ('splogin', 'spmostrar_customers', 'spmostrar_orders', 'spreporte_factura')
    ORDER BY ROUTINE_NAME
    
    -- Verificar específicamente el procedimiento del reporte
    PRINT ''
    PRINT '=== VERIFICACIÓN DEL PROCEDIMIENTO DE REPORTE ==='
    IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spreporte_factura')
    BEGIN
        PRINT 'El procedimiento spreporte_factura EXISTE'
        
        -- Mostrar información del procedimiento
        SELECT 
            p.name as 'Nombre_Procedimiento',
            p.create_date as 'Fecha_Creación',
            p.modify_date as 'Fecha_Modificación'
        FROM sys.procedures p
        WHERE p.name = 'spreporte_factura'
        
        -- Verificar parámetros del procedimiento
        PRINT ''
        PRINT 'Parámetros del procedimiento:'
        SELECT 
            par.name as 'Nombre_Parámetro',
            t.name as 'Tipo_Dato',
            par.max_length as 'Longitud_Máxima'
        FROM sys.parameters par
        INNER JOIN sys.types t ON par.system_type_id = t.system_type_id
        INNER JOIN sys.objects o ON par.object_id = o.object_id
        WHERE o.name = 'spreporte_factura'
        AND par.name != ''
        
    END
    ELSE
    BEGIN
        PRINT 'ERROR: El procedimiento spreporte_factura NO EXISTE'
        PRINT 'Ejecuta el script Create_Report_Procedure.sql para crearlo'
    END
    
    -- Verificar datos de prueba
    PRINT ''
    PRINT '=== DATOS DE PRUEBA ==='
    SELECT 
        'users' as Tabla,
        COUNT(*) as 'Cantidad_Registros'
    FROM users
    UNION ALL
    SELECT 
        'customers' as Tabla,
        COUNT(*) as 'Cantidad_Registros'
    FROM customers
    UNION ALL
    SELECT 
        'orders' as Tabla,
        COUNT(*) as 'Cantidad_Registros'
    FROM orders
    UNION ALL
    SELECT 
        'order_items' as Tabla,
        COUNT(*) as 'Cantidad_Registros'
    FROM order_items
    ORDER BY Tabla
    
    -- Probar el procedimiento de reporte si hay datos
    IF EXISTS (SELECT * FROM orders) AND EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spreporte_factura')
    BEGIN
        PRINT ''
        PRINT '=== PRUEBA DEL PROCEDIMIENTO DE REPORTE ==='
        DECLARE @test_order_id INT = (SELECT TOP 1 order_id FROM orders ORDER BY order_id)
        PRINT 'Probando con order_id = ' + CAST(@test_order_id AS VARCHAR(10))
        
        EXEC spreporte_factura @order_id = @test_order_id
    END
    
END
ELSE
BEGIN
    PRINT ''
    PRINT '=== ERROR: BASE DE DATOS NO ENCONTRADA ==='
    PRINT 'La base de datos Bike_Store NO EXISTE'
    PRINT 'Ejecuta el script Complete_Database_Script.sql para crearla'
    
    -- Mostrar todas las bases de datos disponibles
    PRINT ''
    PRINT 'Bases de datos disponibles en esta instancia:'
    SELECT name as 'Todas_Las_Bases_Datos' FROM sys.databases ORDER BY name
END

PRINT ''
PRINT '=== DIAGNÓSTICO COMPLETADO ==='
PRINT 'Si hay errores arriba, corrígelos antes de usar los reportes'
PRINT ''
PRINT '=== CADENAS DE CONEXIÓN RECOMENDADAS ==='
PRINT 'Para LocalDB: Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Bike_Store;Integrated Security=True;TrustServerCertificate=True'
PRINT 'Para SQL Express: Data Source=.\SQLEXPRESS;Initial Catalog=Bike_Store;Integrated Security=True;TrustServerCertificate=True'
PRINT 'Para instancia nombrada: Data Source=NOMBRE_PC\NOMBRE_INSTANCIA;Initial Catalog=Bike_Store;Integrated Security=True;TrustServerCertificate=True'