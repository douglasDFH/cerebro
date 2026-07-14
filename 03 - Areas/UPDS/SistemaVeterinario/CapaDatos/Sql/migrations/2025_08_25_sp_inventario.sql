USE Sistema_Veterinario;
GO

-- ============================================
-- TABLAS ADICIONALES PARA INVENTARIO
-- ============================================

-- Tabla para ubicaciones de almacén
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ubicacion_almacen')
BEGIN
    CREATE TABLE ubicacion_almacen (
        id INT IDENTITY(1,1) NOT NULL,
        codigo VARCHAR(20) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        descripcion VARCHAR(255) NULL,
        activo BIT NOT NULL DEFAULT 1,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT PK_ubicacion_almacen PRIMARY KEY (id),
        CONSTRAINT UK_ubicacion_almacen_codigo UNIQUE (codigo)
    );
END
GO

-- Tabla para movimientos de inventario
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'movimiento_inventario')
BEGIN
    CREATE TABLE movimiento_inventario (
        id INT IDENTITY(1,1) NOT NULL,
        producto_id INT NOT NULL,
        tipo_movimiento VARCHAR(20) NOT NULL, -- 'ENTRADA', 'SALIDA', 'AJUSTE', 'TRANSFERENCIA'
        cantidad INT NOT NULL,
        stock_anterior INT NOT NULL,
        stock_nuevo INT NOT NULL,
        motivo VARCHAR(255) NULL,
        referencia VARCHAR(100) NULL, -- Número de factura, orden de compra, etc.
        ubicacion_origen_id INT NULL,
        ubicacion_destino_id INT NULL,
        usuario VARCHAR(50) DEFAULT 'Sistema',
        fecha_movimiento DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT PK_movimiento_inventario PRIMARY KEY (id),
        CONSTRAINT FK_movimiento_producto FOREIGN KEY (producto_id) REFERENCES producto(id),
        CONSTRAINT FK_movimiento_ubicacion_origen FOREIGN KEY (ubicacion_origen_id) REFERENCES ubicacion_almacen(id),
        CONSTRAINT FK_movimiento_ubicacion_destino FOREIGN KEY (ubicacion_destino_id) REFERENCES ubicacion_almacen(id),
        CONSTRAINT CK_movimiento_tipo CHECK (tipo_movimiento IN ('ENTRADA', 'SALIDA', 'AJUSTE', 'TRANSFERENCIA')),
        CONSTRAINT CK_movimiento_cantidad CHECK (cantidad != 0)
    );
END
GO

-- Tabla para stock por ubicación
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'stock_ubicacion')
BEGIN
    CREATE TABLE stock_ubicacion (
        id INT IDENTITY(1,1) NOT NULL,
        producto_id INT NOT NULL,
        ubicacion_id INT NOT NULL,
        stock_actual INT NOT NULL DEFAULT 0,
        stock_reservado INT NOT NULL DEFAULT 0,
        fecha_ultimo_movimiento DATETIME2(3) NULL,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT PK_stock_ubicacion PRIMARY KEY (id),
        CONSTRAINT FK_stock_ubicacion_producto FOREIGN KEY (producto_id) REFERENCES producto(id),
        CONSTRAINT FK_stock_ubicacion_ubicacion FOREIGN KEY (ubicacion_id) REFERENCES ubicacion_almacen(id),
        CONSTRAINT UK_stock_ubicacion_producto_ubicacion UNIQUE (producto_id, ubicacion_id),
        CONSTRAINT CK_stock_ubicacion_stock CHECK (stock_actual >= 0 AND stock_reservado >= 0)
    );
END
GO

-- Insertar ubicaciones por defecto
IF NOT EXISTS (SELECT * FROM ubicacion_almacen WHERE codigo = 'PRINCIPAL')
BEGIN
    INSERT INTO ubicacion_almacen (codigo, nombre, descripcion) VALUES 
    ('PRINCIPAL', 'Almacén Principal', 'Ubicación principal de almacenamiento'),
    ('FARMACIA', 'Farmacia', 'Medicamentos y productos farmacéuticos'),
    ('CONSULTA', 'Sala de Consulta', 'Productos para consultas veterinarias'),
    ('CIRUGIA', 'Quirófano', 'Instrumental y medicamentos quirúrgicos');
END
GO

-- ============================================
-- PROCEDIMIENTOS ALMACENADOS PARA INVENTARIO
-- ============================================

-- 1. SP_MostrarInventario - Vista completa del inventario
IF OBJECT_ID('SP_MostrarInventario', 'P') IS NOT NULL
    DROP PROCEDURE SP_MostrarInventario;
GO

CREATE PROCEDURE SP_MostrarInventario
    @soloStockBajo BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.codigo,
        p.nombre,
        p.descripcion,
        c.nombre as categoria,
        p.precio,
        p.stock_actual,
        p.stock_minimo,
        CASE 
            WHEN p.stock_actual <= p.stock_minimo THEN 'CRITICO'
            WHEN p.stock_actual <= (p.stock_minimo * 1.5) THEN 'BAJO'
            ELSE 'NORMAL'
        END as estado_stock,
        p.requiere_receta,
        p.activo,
        p.created_at,
        p.updated_at,
        -- Calcular valor total del stock
        (p.stock_actual * p.precio) as valor_stock
    FROM producto p
    INNER JOIN categoria c ON p.categoria_id = c.id
    WHERE p.activo = 1
    AND (@soloStockBajo = 0 OR p.stock_actual <= p.stock_minimo)
    ORDER BY 
        CASE 
            WHEN p.stock_actual <= p.stock_minimo THEN 1
            WHEN p.stock_actual <= (p.stock_minimo * 1.5) THEN 2
            ELSE 3
        END,
        p.nombre;
END
GO

-- 2. SP_BuscarInventarioPorTexto
IF OBJECT_ID('SP_BuscarInventarioPorTexto', 'P') IS NOT NULL
    DROP PROCEDURE SP_BuscarInventarioPorTexto;
GO

CREATE PROCEDURE SP_BuscarInventarioPorTexto
    @textoBuscar NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.codigo,
        p.nombre,
        p.descripcion,
        c.nombre as categoria,
        p.precio,
        p.stock_actual,
        p.stock_minimo,
        CASE 
            WHEN p.stock_actual <= p.stock_minimo THEN 'CRITICO'
            WHEN p.stock_actual <= (p.stock_minimo * 1.5) THEN 'BAJO'
            ELSE 'NORMAL'
        END as estado_stock,
        p.requiere_receta,
        p.activo,
        (p.stock_actual * p.precio) as valor_stock
    FROM producto p
    INNER JOIN categoria c ON p.categoria_id = c.id
    WHERE p.activo = 1
    AND (
        p.nombre LIKE '%' + @textoBuscar + '%' OR
        p.codigo LIKE '%' + @textoBuscar + '%' OR
        p.descripcion LIKE '%' + @textoBuscar + '%' OR
        c.nombre LIKE '%' + @textoBuscar + '%'
    )
    ORDER BY p.nombre;
END
GO

-- 3. SP_ActualizarStock - Actualizar stock de un producto
IF OBJECT_ID('SP_ActualizarStock', 'P') IS NOT NULL
    DROP PROCEDURE SP_ActualizarStock;
GO

CREATE PROCEDURE SP_ActualizarStock
    @producto_id INT,
    @nuevo_stock INT,
    @motivo VARCHAR(255) = NULL,
    @referencia VARCHAR(100) = NULL,
    @usuario VARCHAR(50) = 'Sistema'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @stock_anterior INT;
    DECLARE @diferencia INT;
    DECLARE @tipo_movimiento VARCHAR(20);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Obtener stock actual
        SELECT @stock_anterior = stock_actual 
        FROM producto 
        WHERE id = @producto_id;

        IF @stock_anterior IS NULL
        BEGIN
            SELECT 0 as resultado, 'Producto no encontrado' as mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Calcular diferencia y tipo de movimiento
        SET @diferencia = @nuevo_stock - @stock_anterior;
        
        IF @diferencia > 0
            SET @tipo_movimiento = 'ENTRADA';
        ELSE IF @diferencia < 0
            SET @tipo_movimiento = 'SALIDA';
        ELSE
            SET @tipo_movimiento = 'AJUSTE';

        -- Actualizar stock del producto
        UPDATE producto 
        SET stock_actual = @nuevo_stock, 
            updated_at = GETDATE()
        WHERE id = @producto_id;

        -- Registrar movimiento de inventario
        INSERT INTO movimiento_inventario (
            producto_id, tipo_movimiento, cantidad, 
            stock_anterior, stock_nuevo, motivo, referencia, usuario
        ) VALUES (
            @producto_id, @tipo_movimiento, ABS(@diferencia),
            @stock_anterior, @nuevo_stock, @motivo, @referencia, @usuario
        );

        COMMIT TRANSACTION;
        SELECT 1 as resultado, 'Stock actualizado correctamente' as mensaje;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 as resultado, ERROR_MESSAGE() as mensaje;
    END CATCH
END
GO

-- 4. SP_RegistrarMovimientoInventario
IF OBJECT_ID('SP_RegistrarMovimientoInventario', 'P') IS NOT NULL
    DROP PROCEDURE SP_RegistrarMovimientoInventario;
GO

CREATE PROCEDURE SP_RegistrarMovimientoInventario
    @producto_id INT,
    @tipo_movimiento VARCHAR(20),
    @cantidad INT,
    @motivo VARCHAR(255) = NULL,
    @referencia VARCHAR(100) = NULL,
    @ubicacion_origen_id INT = NULL,
    @ubicacion_destino_id INT = NULL,
    @usuario VARCHAR(50) = 'Sistema'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @stock_anterior INT;
    DECLARE @stock_nuevo INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Obtener stock actual
        SELECT @stock_anterior = stock_actual 
        FROM producto 
        WHERE id = @producto_id;

        IF @stock_anterior IS NULL
        BEGIN
            SELECT 0 as resultado, 'Producto no encontrado' as mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Calcular nuevo stock según tipo de movimiento
        IF @tipo_movimiento = 'ENTRADA'
            SET @stock_nuevo = @stock_anterior + @cantidad;
        ELSE IF @tipo_movimiento = 'SALIDA'
        BEGIN
            SET @stock_nuevo = @stock_anterior - @cantidad;
            IF @stock_nuevo < 0
            BEGIN
                SELECT 0 as resultado, 'Stock insuficiente para realizar la salida' as mensaje;
                ROLLBACK TRANSACTION;
                RETURN;
            END
        END
        ELSE IF @tipo_movimiento = 'AJUSTE'
            SET @stock_nuevo = @cantidad; -- La cantidad es el nuevo stock total
        ELSE
            SET @stock_nuevo = @stock_anterior; -- Para transferencias no cambia el stock total

        -- Actualizar stock del producto
        IF @tipo_movimiento != 'TRANSFERENCIA'
        BEGIN
            UPDATE producto 
            SET stock_actual = @stock_nuevo, 
                updated_at = GETDATE()
            WHERE id = @producto_id;
        END

        -- Registrar movimiento
        INSERT INTO movimiento_inventario (
            producto_id, tipo_movimiento, cantidad, 
            stock_anterior, stock_nuevo, motivo, referencia, 
            ubicacion_origen_id, ubicacion_destino_id, usuario
        ) VALUES (
            @producto_id, @tipo_movimiento, @cantidad,
            @stock_anterior, @stock_nuevo, @motivo, @referencia,
            @ubicacion_origen_id, @ubicacion_destino_id, @usuario
        );

        COMMIT TRANSACTION;
        SELECT 1 as resultado, 'Movimiento registrado correctamente' as mensaje;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 as resultado, ERROR_MESSAGE() as mensaje;
    END CATCH
END
GO

-- 5. SP_ObtenerMovimientosInventario
IF OBJECT_ID('SP_ObtenerMovimientosInventario', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerMovimientosInventario;
GO

CREATE PROCEDURE SP_ObtenerMovimientosInventario
    @producto_id INT = NULL,
    @dias INT = 30
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        m.id,
        m.producto_id,
        p.codigo as codigo_producto,
        p.nombre as nombre_producto,
        m.tipo_movimiento,
        m.cantidad,
        m.stock_anterior,
        m.stock_nuevo,
        m.motivo,
        m.referencia,
        uo.nombre as ubicacion_origen,
        ud.nombre as ubicacion_destino,
        m.usuario,
        m.fecha_movimiento,
        m.created_at
    FROM movimiento_inventario m
    INNER JOIN producto p ON m.producto_id = p.id
    LEFT JOIN ubicacion_almacen uo ON m.ubicacion_origen_id = uo.id
    LEFT JOIN ubicacion_almacen ud ON m.ubicacion_destino_id = ud.id
    WHERE (@producto_id IS NULL OR m.producto_id = @producto_id)
    AND m.fecha_movimiento >= DATEADD(DAY, -@dias, GETDATE())
    ORDER BY m.fecha_movimiento DESC, m.id DESC;
END
GO

-- 6. SP_ObtenerAlertasStock
IF OBJECT_ID('SP_ObtenerAlertasStock', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerAlertasStock;
GO

CREATE PROCEDURE SP_ObtenerAlertasStock
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.codigo,
        p.nombre,
        c.nombre as categoria,
        p.stock_actual,
        p.stock_minimo,
        CASE 
            WHEN p.stock_actual = 0 THEN 'SIN_STOCK'
            WHEN p.stock_actual <= p.stock_minimo THEN 'CRITICO'
            WHEN p.stock_actual <= (p.stock_minimo * 1.5) THEN 'BAJO'
        END as nivel_alerta,
        (p.stock_minimo - p.stock_actual + 10) as cantidad_sugerida_compra
    FROM producto p
    INNER JOIN categoria c ON p.categoria_id = c.id
    WHERE p.activo = 1
    AND p.stock_actual <= (p.stock_minimo * 1.5)
    ORDER BY 
        CASE 
            WHEN p.stock_actual = 0 THEN 1
            WHEN p.stock_actual <= p.stock_minimo THEN 2
            ELSE 3
        END,
        p.stock_actual ASC;
END
GO

-- 7. SP_ObtenerUbicaciones
IF OBJECT_ID('SP_ObtenerUbicaciones', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerUbicaciones;
GO

CREATE PROCEDURE SP_ObtenerUbicaciones
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        id,
        codigo,
        nombre,
        descripcion,
        activo
    FROM ubicacion_almacen
    WHERE activo = 1
    ORDER BY nombre;
END
GO

-- 8. SP_ObtenerStockPorUbicacion
IF OBJECT_ID('SP_ObtenerStockPorUbicacion', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerStockPorUbicacion;
GO

CREATE PROCEDURE SP_ObtenerStockPorUbicacion
    @ubicacion_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        su.id,
        p.codigo,
        p.nombre as producto,
        u.nombre as ubicacion,
        su.stock_actual,
        su.stock_reservado,
        (su.stock_actual - su.stock_reservado) as stock_disponible,
        su.fecha_ultimo_movimiento
    FROM stock_ubicacion su
    INNER JOIN producto p ON su.producto_id = p.id
    INNER JOIN ubicacion_almacen u ON su.ubicacion_id = u.id
    WHERE (@ubicacion_id IS NULL OR su.ubicacion_id = @ubicacion_id)
    AND p.activo = 1
    ORDER BY u.nombre, p.nombre;
END
GO

-- 9. SP_ObtenerProductoPorId
IF OBJECT_ID('SP_ObtenerInventarioPorId', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerInventarioPorId;
GO

CREATE PROCEDURE SP_ObtenerInventarioPorId
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.codigo,
        p.nombre,
        p.descripcion,
        p.categoria_id,
        c.nombre as categoria,
        p.precio,
        p.stock_actual,
        p.stock_minimo,
        p.requiere_receta,
        p.activo,
        CASE 
            WHEN p.stock_actual <= p.stock_minimo THEN 'CRITICO'
            WHEN p.stock_actual <= (p.stock_minimo * 1.5) THEN 'BAJO'
            ELSE 'NORMAL'
        END as estado_stock,
        (p.stock_actual * p.precio) as valor_stock
    FROM producto p
    INNER JOIN categoria c ON p.categoria_id = c.id
    WHERE p.id = @id;
END
GO

-- 10. SP_ReporteValorInventario
IF OBJECT_ID('SP_ReporteValorInventario', 'P') IS NOT NULL
    DROP PROCEDURE SP_ReporteValorInventario;
GO

CREATE PROCEDURE SP_ReporteValorInventario
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.nombre as categoria,
        COUNT(p.id) as total_productos,
        SUM(p.stock_actual) as total_unidades,
        SUM(p.stock_actual * p.precio) as valor_total,
        AVG(p.precio) as precio_promedio,
        SUM(CASE WHEN p.stock_actual <= p.stock_minimo THEN 1 ELSE 0 END) as productos_stock_bajo
    FROM producto p
    INNER JOIN categoria c ON p.categoria_id = c.id
    WHERE p.activo = 1
    GROUP BY c.id, c.nombre
    ORDER BY valor_total DESC;
END
GO

PRINT 'Procedimientos almacenados para Inventario creados correctamente.';