CREATE PROCEDURE SP01_CreateOrUpdatePFisica @id INT = NULL,
@ci VARCHAR(15) = NULL,
@nombre VARCHAR(100),
@apellido VARCHAR(100),
@email VARCHAR(255) = NULL,
@direccion VARCHAR(255) = NULL,
@telefono VARCHAR(20) = NULL,
@fecha_nacimiento DATE = NULL,
@genero CHAR(1) = NULL AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @persona_id INT;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN
INSERT INTO persona (tipo, email, direccion, telefono)
VALUES ('Física', @email, @direccion, @telefono);
SET @persona_id = SCOPE_IDENTITY();
INSERT INTO persona_fisica (
        id,
        ci,
        nombre,
        apellido,
        fecha_nacimiento,
        genero
    )
VALUES (
        @persona_id,
        @ci,
        @nombre,
        @apellido,
        @fecha_nacimiento,
        @genero
    );
SELECT @persona_id as id,
    'Persona física creada exitosamente' as mensaje;
END
ELSE BEGIN
IF EXISTS (
    SELECT 1
    FROM persona
    WHERE id = @id
        AND tipo = 'Física'
) BEGIN
UPDATE persona
SET email = ISNULL(@email, email),
    direccion = ISNULL(@direccion, direccion),
    telefono = ISNULL(@telefono, telefono)
WHERE id = @id;
UPDATE persona_fisica
SET ci = ISNULL(@ci, ci),
    nombre = @nombre,
    apellido = @apellido,
    fecha_nacimiento = ISNULL(@fecha_nacimiento, fecha_nacimiento),
    genero = ISNULL(@genero, genero)
WHERE id = @id;
SELECT @id as id,
    'Persona física actualizada exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe persona física con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO

CREATE PROCEDURE SP02_CreateOrUpdatePJuridica @id INT = NULL,
@razon_social VARCHAR(255),
@nit VARCHAR(20) = NULL,
@encargado_nombre VARCHAR(255) = NULL,
@encargado_cargo VARCHAR(100) = NULL,
@email VARCHAR(255) = NULL,
@direccion VARCHAR(255) = NULL,
@telefono VARCHAR(20) = NULL AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @persona_id INT;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN
INSERT INTO persona (tipo, email, direccion, telefono)
VALUES ('Jurídica', @email, @direccion, @telefono);
SET @persona_id = SCOPE_IDENTITY();
INSERT INTO persona_juridica (
        id,
        razon_social,
        nit,
        encargado_nombre,
        encargado_cargo
    )
VALUES (
        @persona_id,
        @razon_social,
        @nit,
        @encargado_nombre,
        @encargado_cargo
    );
SELECT @persona_id as id,
    'Persona jurídica creada exitosamente' as mensaje;
END
ELSE BEGIN
IF EXISTS (
    SELECT 1
    FROM persona
    WHERE id = @id
        AND tipo = 'Jurídica'
) BEGIN
UPDATE persona
SET email = ISNULL(@email, email),
    direccion = ISNULL(@direccion, direccion),
    telefono = ISNULL(@telefono, telefono)
WHERE id = @id;
UPDATE persona_juridica
SET razon_social = @razon_social,
    nit = ISNULL(@nit, nit),
    encargado_nombre = ISNULL(@encargado_nombre, encargado_nombre),
    encargado_cargo = ISNULL(@encargado_cargo, encargado_cargo)
WHERE id = @id;
SELECT @id as id,
    'Persona jurídica actualizada exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR(
    'No existe persona jurídica con ID %d',
    16,
    1,
    @id
);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO

CREATE PROCEDURE SP03_CreateOrUpdateVeterinario @id INT = NULL,
@nombre VARCHAR(100),
@apellido VARCHAR(100),
@email VARCHAR(100),
@usuario VARCHAR(50),
@contrasena VARCHAR(255),
@telefono VARCHAR(20) = NULL,
@direccion VARCHAR(255) = NULL,
@num_licencia VARCHAR(50),
@especialidad VARCHAR(100) = NULL,
@universidad VARCHAR(200) = NULL,
@salario DECIMAL(10, 2) = NULL AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @personal_id INT;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN
INSERT INTO personal (
        nombre,
        apellido,
        email,
        usuario,
        contrasena,
        telefono,
        direccion,
        salario
    )
VALUES (
        @nombre,
        @apellido,
        @email,
        @usuario,
        @contrasena,
        @telefono,
        @direccion,
        @salario
    );
SET @personal_id = SCOPE_IDENTITY();
INSERT INTO personal_veterinario (id, num_licencia, especialidad, universidad)
VALUES (
        @personal_id,
        @num_licencia,
        @especialidad,
        @universidad
    );
SELECT @personal_id as id,
    'Veterinario creado exitosamente' as mensaje;
END
ELSE BEGIN
IF EXISTS (
    SELECT 1
    FROM personal p
        INNER JOIN personal_veterinario pv ON p.id = pv.id
    WHERE p.id = @id
) BEGIN
UPDATE personal
SET nombre = @nombre,
    apellido = @apellido,
    email = @email,
    usuario = @usuario,
    contrasena = ISNULL(@contrasena, contrasena),
    telefono = ISNULL(@telefono, telefono),
    direccion = ISNULL(@direccion, direccion),
    salario = ISNULL(@salario, salario)
WHERE id = @id;
UPDATE personal_veterinario
SET num_licencia = @num_licencia,
    especialidad = ISNULL(@especialidad, especialidad),
    universidad = ISNULL(@universidad, universidad)
WHERE id = @id;
SELECT @id as id,
    'Veterinario actualizado exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe veterinario con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO
    CREATE PROCEDURE SP04_CreateOrUpdateAuxiliar @id INT = NULL,
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @email VARCHAR(100),
    @usuario VARCHAR(50),
    @contrasena VARCHAR(255),
    @telefono VARCHAR(20) = NULL,
    @direccion VARCHAR(255) = NULL,
    @area VARCHAR(100) = NULL,
    @turno VARCHAR(10),
    @nivel VARCHAR(20) = 'Básico',
    @salario DECIMAL(10, 2) = NULL AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @personal_id INT;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN 
INSERT INTO personal (
        nombre,
        apellido,
        email,
        usuario,
        contrasena,
        telefono,
        direccion,
        salario
    )
VALUES (
        @nombre,
        @apellido,
        @email,
        @usuario,
        @contrasena,
        @telefono,
        @direccion,
        @salario
    );
SET @personal_id = SCOPE_IDENTITY();
INSERT INTO personal_auxiliar (id, area, turno, nivel)
VALUES (@personal_id, @area, @turno, @nivel);
SELECT @personal_id as id,
    'Auxiliar creado exitosamente' as mensaje;
END
ELSE BEGIN 
IF EXISTS (
    SELECT 1
    FROM personal p
        INNER JOIN personal_auxiliar pa ON p.id = pa.id
    WHERE p.id = @id
) BEGIN
UPDATE personal
SET nombre = @nombre,
    apellido = @apellido,
    email = @email,
    usuario = @usuario,
    contrasena = ISNULL(@contrasena, contrasena),
    telefono = ISNULL(@telefono, telefono),
    direccion = ISNULL(@direccion, direccion),
    salario = ISNULL(@salario, salario)
WHERE id = @id;
UPDATE personal_auxiliar
SET area = ISNULL(@area, area),
    turno = @turno,
    nivel = ISNULL(@nivel, nivel)
WHERE id = @id;
SELECT @id as id,
    'Auxiliar actualizado exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe auxiliar con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO 
    CREATE PROCEDURE SP05_CreateOrUpdateAnimal @id INT = NULL,
    @nombre VARCHAR(100),
    @especie VARCHAR(50),
    @persona_id INT,
    @raza VARCHAR(100) = NULL,
    @fecha_nacimiento DATE = NULL,
    @peso DECIMAL(5, 2) = NULL,
    @color VARCHAR(50) = NULL,
    @genero CHAR(1) = NULL,
    @esterilizado BIT = 0,
    @microchip VARCHAR(50) = NULL AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @animal_id INT,
    @historico_id INT;
BEGIN TRY 
IF NOT EXISTS (
    SELECT 1
    FROM persona
    WHERE id = @persona_id
) BEGIN RAISERROR('La persona especificada no existe', 16, 1);
RETURN;
END IF @id IS NULL
OR @id = 0 BEGIN 
INSERT INTO animal (
        nombre,
        especie,
        raza,
        fecha_nacimiento,
        peso,
        color,
        genero,
        esterilizado,
        microchip,
        persona_id
    )
VALUES (
        @nombre,
        @especie,
        @raza,
        @fecha_nacimiento,
        @peso,
        @color,
        @genero,
        @esterilizado,
        @microchip,
        @persona_id
    );
SET @animal_id = SCOPE_IDENTITY();
INSERT INTO historico (animal_id, notas_generales)
VALUES (
        @animal_id,
        'Historial clínico creado automáticamente'
    );
SELECT @animal_id as id,
    'Animal registrado exitosamente con historial clínico' as mensaje;
END
ELSE BEGIN 
IF EXISTS (
    SELECT 1
    FROM animal
    WHERE id = @id
) BEGIN
UPDATE animal
SET nombre = @nombre,
    especie = @especie,
    raza = ISNULL(@raza, raza),
    fecha_nacimiento = ISNULL(@fecha_nacimiento, fecha_nacimiento),
    peso = ISNULL(@peso, peso),
    color = ISNULL(@color, color),
    genero = ISNULL(@genero, genero),
    esterilizado = ISNULL(@esterilizado, esterilizado),
    microchip = ISNULL(@microchip, microchip),
    persona_id = @persona_id
WHERE id = @id;
SELECT @id as id,
    'Animal actualizado exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe animal con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO
    CREATE PROCEDURE SP06_CreateOrUpdateCategoria @id INT = NULL,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255) = NULL,
    @activo BIT = 1 AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN 
INSERT INTO categoria (nombre, descripcion, activo)
VALUES (@nombre, @descripcion, @activo);
SELECT SCOPE_IDENTITY() as id,
    'Categoría creada exitosamente' as mensaje;
END
ELSE BEGIN 
IF EXISTS (
    SELECT 1
    FROM categoria
    WHERE id = @id
) BEGIN
UPDATE categoria
SET nombre = @nombre,
    descripcion = ISNULL(@descripcion, descripcion),
    activo = ISNULL(@activo, activo)
WHERE id = @id;
SELECT @id as id,
    'Categoría actualizada exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe categoría con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO
    CREATE PROCEDURE SP07_CreateOrUpdateProducto @id INT = NULL,
    @codigo VARCHAR(50) = NULL,
    @nombre VARCHAR(255),
    @descripcion TEXT = NULL,
    @precio DECIMAL(10, 2),
    @stock_minimo INT = 5,
    @stock_actual INT = 0,
    @requiere_receta BIT = 1,
    @categoria_id INT,
    @activo BIT = 1 AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
BEGIN TRY 
IF NOT EXISTS (
    SELECT 1
    FROM categoria
    WHERE id = @categoria_id
) BEGIN RAISERROR('La categoría especificada no existe', 16, 1);
RETURN;
END IF @id IS NULL
OR @id = 0 BEGIN
INSERT INTO producto (
        codigo,
        nombre,
        descripcion,
        precio,
        stock_minimo,
        stock_actual,
        requiere_receta,
        categoria_id,
        activo
    )
VALUES (
        @codigo,
        @nombre,
        @descripcion,
        @precio,
        @stock_minimo,
        @stock_actual,
        @requiere_receta,
        @categoria_id,
        @activo
    );
SELECT SCOPE_IDENTITY() as id,
    'Producto creado exitosamente' as mensaje;
END
ELSE BEGIN 
IF EXISTS (
    SELECT 1
    FROM producto
    WHERE id = @id
) BEGIN
UPDATE producto
SET codigo = ISNULL(@codigo, codigo),
    nombre = @nombre,
    descripcion = ISNULL(@descripcion, descripcion),
    precio = @precio,
    stock_minimo = ISNULL(@stock_minimo, stock_minimo),
    stock_actual = ISNULL(@stock_actual, stock_actual),
    requiere_receta = ISNULL(@requiere_receta, requiere_receta),
    categoria_id = @categoria_id,
    activo = ISNULL(@activo, activo)
WHERE id = @id;
SELECT @id as id,
    'Producto actualizado exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe producto con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO
    CREATE PROCEDURE SP08_CreateOrUpdateDiagnostico @id INT = NULL,
    @codigo VARCHAR(20) = NULL,
    @nombre VARCHAR(200),
    @descripcion TEXT = NULL,
    @precio_base DECIMAL(10, 2) = 0,
    @categoria_diagnostico VARCHAR(100) = NULL,
    @requiere_equipamiento BIT = 0,
    @activo BIT = 1 AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN 
INSERT INTO diagnostico (
        codigo,
        nombre,
        descripcion,
        precio_base,
        categoria_diagnostico,
        requiere_equipamiento,
        activo
    )
VALUES (
        @codigo,
        @nombre,
        @descripcion,
        @precio_base,
        @categoria_diagnostico,
        @requiere_equipamiento,
        @activo
    );
SELECT SCOPE_IDENTITY() as id,
    'Diagnóstico creado exitosamente' as mensaje;
END
ELSE BEGIN 
IF EXISTS (
    SELECT 1
    FROM diagnostico
    WHERE id = @id
) BEGIN
UPDATE diagnostico
SET codigo = ISNULL(@codigo, codigo),
    nombre = @nombre,
    descripcion = ISNULL(@descripcion, descripcion),
    precio_base = @precio_base,
    categoria_diagnostico = ISNULL(@categoria_diagnostico, categoria_diagnostico),
    requiere_equipamiento = ISNULL(@requiere_equipamiento, requiere_equipamiento),
    activo = ISNULL(@activo, activo)
WHERE id = @id;
SELECT @id as id,
    'Diagnóstico actualizado exitosamente' as mensaje;
END
ELSE BEGIN RAISERROR('No existe diagnóstico con ID %d', 16, 1, @id);
RETURN;
END
END COMMIT TRANSACTION;
END TRY BEGIN CATCH ROLLBACK TRANSACTION;
THROW;
END CATCH
END;
GO
    CREATE
    OR ALTER PROCEDURE SP_SaveFactura @factura_id INT = NULL,
    @persona_id INT = NULL,
    @numero_factura VARCHAR(50) = NULL,
    @fecha_vencimiento DATE = NULL,
    @notas NVARCHAR(MAX) = NULL,
    @productos NVARCHAR(MAX) = NULL,
    /* JSON: [{"id":1,"cantidad":2,"precio":50.00,"descuento":0,"lote":"L001","fecha_vencimiento":"2025-12-01"}] */
    @servicios NVARCHAR(MAX) = NULL,
    /* JSON: [{"id":1,"cantidad":1,"precio":100.00,"descuento":0,"veterinario_id":1,"detalle_historico_id":null}] */
    @impuestos DECIMAL(10, 2) = 0,
    @descuentos DECIMAL(10, 2) = 0,
    @estado VARCHAR(20) = 'Pendiente',
    @finalizar BIT = 0 
    AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @new_factura_id INT;
DECLARE @subtotal DECIMAL(18, 2) = 0;
DECLARE @total DECIMAL(18, 2) = 0;
BEGIN TRY 
IF @persona_id IS NULL
OR NOT EXISTS (
    SELECT 1
    FROM persona
    WHERE id = @persona_id
) BEGIN RAISERROR('Persona especificada no existe.', 16, 1);
ROLLBACK TRANSACTION;
RETURN;
END
IF @factura_id IS NULL
OR @factura_id = 0 BEGIN
INSERT INTO factura (
        numero_factura,
        persona_id,
        fecha_vencimiento,
        notas,
        estado
    )
VALUES (
        @numero_factura,
        @persona_id,
        @fecha_vencimiento,
        @notas,
        @estado
    );
SET @new_factura_id = SCOPE_IDENTITY();
END
ELSE BEGIN 
IF NOT EXISTS (
    SELECT 1
    FROM factura
    WHERE id = @factura_id
) BEGIN RAISERROR('Factura a actualizar no existe.', 16, 1);
ROLLBACK TRANSACTION;
RETURN;
END
UPDATE factura
SET numero_factura = ISNULL(@numero_factura, numero_factura),
    persona_id = ISNULL(@persona_id, persona_id),
    fecha_vencimiento = ISNULL(@fecha_vencimiento, fecha_vencimiento),
    notas = ISNULL(@notas, notas)
WHERE id = @factura_id;
SET @new_factura_id = @factura_id;
END

IF @productos IS NOT NULL
AND LTRIM(RTRIM(@productos)) <> '' BEGIN
IF @factura_id IS NOT NULL
AND @factura_id <> 0 BEGIN
UPDATE p
SET p.stock_actual = p.stock_actual + dp.cantidad
FROM producto p
    JOIN detalle_productos dp ON p.id = dp.producto_id
WHERE dp.factura_id = @new_factura_id;
DELETE FROM detalle_productos
WHERE factura_id = @new_factura_id;
END 
DECLARE @prod_tab TABLE (
        producto_id INT,
        cantidad INT,
        precio_unitario DECIMAL(18, 2),
        descuento_unitario DECIMAL(18, 2),
        lote VARCHAR(50),
        fecha_vencimiento_producto DATE
    );
INSERT INTO @prod_tab (
        producto_id,
        cantidad,
        precio_unitario,
        descuento_unitario,
        lote,
        fecha_vencimiento_producto
    )
SELECT p.producto_id,
    p.cantidad,
    p.precio_unitario,
    ISNULL(p.descuento_unitario, 0),
    p.lote,
    TRY_CONVERT(date, p.fecha_vencimiento_producto)
FROM OPENJSON(@productos) WITH (
        producto_id INT '$.id',
        cantidad INT '$.cantidad',
        precio_unitario DECIMAL(18, 2) '$.precio',
        descuento_unitario DECIMAL(18, 2) '$.descuento',
        lote VARCHAR(50) '$.lote',
        fecha_vencimiento_producto VARCHAR(30) '$.fecha_vencimiento'
    ) p;
;
WITH req AS (
    SELECT producto_id,
        SUM(ISNULL(cantidad, 0)) AS necesario
    FROM @prod_tab
    GROUP BY producto_id
)
SELECT 1
FROM req r
    LEFT JOIN producto p ON p.id = r.producto_id
WHERE p.id IS NULL
    OR p.stock_actual < r.necesario;
IF @@ROWCOUNT > 0 BEGIN RAISERROR(
    'Stock insuficiente para uno o varios productos o producto no existe.',
    16,
    1
);
ROLLBACK TRANSACTION;
RETURN;
END 
INSERT INTO detalle_productos (
        factura_id,
        producto_id,
        cantidad,
        precio_unitario,
        descuento_unitario,
        lote,
        fecha_vencimiento_producto
    )
SELECT @new_factura_id,
    pt.producto_id,
    pt.cantidad,
    ISNULL(pt.precio_unitario, pr.precio),
    pt.descuento_unitario,
    pt.lote,
    pt.fecha_vencimiento_producto
FROM @prod_tab pt
    LEFT JOIN producto pr ON pr.id = pt.producto_id;
UPDATE p
SET p.stock_actual = p.stock_actual - s.req
FROM producto p
    JOIN (
        SELECT producto_id,
            SUM(cantidad) AS req
        FROM @prod_tab
        GROUP BY producto_id
    ) s ON p.id = s.producto_id;
END 

IF @servicios IS NOT NULL
AND LTRIM(RTRIM(@servicios)) <> '' BEGIN 
IF @factura_id IS NOT NULL
AND @factura_id <> 0 BEGIN
DELETE FROM detalle_servicios
WHERE factura_id = @new_factura_id;
END
DECLARE @serv_tab TABLE (
        diagnostico_id INT,
        detalle_historico_id INT,
        cantidad INT,
        precio_unitario DECIMAL(18, 2),
        descuento_unitario DECIMAL(18, 2),
        veterinario_id INT
    );
INSERT INTO @serv_tab (
        diagnostico_id,
        detalle_historico_id,
        cantidad,
        precio_unitario,
        descuento_unitario,
        veterinario_id
    )
SELECT s.id,
    s.detalle_historico_id,
    ISNULL(s.cantidad, 1),
    s.precio,
    ISNULL(s.descuento, 0),
    s.veterinario_id
FROM OPENJSON(@servicios) WITH (
        id INT '$.id',
        detalle_historico_id INT '$.detalle_historico_id',
        cantidad INT '$.cantidad',
        precio DECIMAL(18, 2) '$.precio',
        descuento DECIMAL(18, 2) '$.descuento',
        veterinario_id INT '$.veterinario_id'
    ) s;
INSERT INTO detalle_servicios (
        factura_id,
        diagnostico_id,
        detalle_historico_id,
        cantidad,
        precio_unitario,
        descuento_unitario,
        veterinario_id
    )
SELECT @new_factura_id,
    st.diagnostico_id,
    st.detalle_historico_id,
    st.cantidad,
    ISNULL(st.precio_unitario, d.precio_base),
    st.descuento_unitario,
    st.veterinario_id
FROM @serv_tab st
    LEFT JOIN diagnostico d ON d.id = st.diagnostico_id;
END 

SELECT @subtotal = ISNULL(
        (
            SELECT SUM(
                    dpc.cantidad * dpc.precio_unitario - dpc.cantidad * dpc.descuento_unitario
                )
            FROM detalle_productos dpc
            WHERE dpc.factura_id = @new_factura_id
        ),
        0
    ) + ISNULL(
        (
            SELECT SUM(
                    dsc.cantidad * dsc.precio_unitario - dsc.cantidad * dsc.descuento_unitario
                )
            FROM detalle_servicios dsc
            WHERE dsc.factura_id = @new_factura_id
        ),
        0
    );
SET @total = @subtotal + ISNULL(@impuestos, 0) - ISNULL(@descuentos, 0);

IF @finalizar = 1
SET @estado = 'Pagada';
UPDATE factura
SET subtotal = @subtotal,
    impuestos = ISNULL(@impuestos, 0),
    descuentos = ISNULL(@descuentos, 0),
    total = @total,
    estado = @estado
WHERE id = @new_factura_id;
COMMIT TRANSACTION;
SELECT @new_factura_id AS factura_id,
    @subtotal AS subtotal,
    @impuestos AS impuestos,
    @descuentos AS descuentos,
    @total AS total,
    @estado AS estado,
    'OK' AS mensaje;
END TRY BEGIN CATCH
DECLARE @err_msg NVARCHAR(4000) = ERROR_MESSAGE();
ROLLBACK TRANSACTION;
RAISERROR('Error en SP_SaveFactura: %s', 16, 1, @err_msg);
RETURN;
END CATCH
END;
GO
GO


CREATE OR ALTER PROCEDURE SP_ReporteVentasPorRango
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @estado VARCHAR(20) = NULL /* NULL = todos, 'Pagada', 'Pendiente', etc. */
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @fecha_inicio > @fecha_fin
    BEGIN
        RAISERROR('La fecha de inicio no puede ser mayor a la fecha fin', 16, 1);
        RETURN;
    END
    
    SELECT 
        f.id,
        f.numero_factura,
        f.fecha_emision,
        f.fecha_vencimiento,
        p.nombre_mostrar AS cliente,
        p.tipo AS tipo_cliente,
        f.subtotal,
        f.impuestos,
        f.descuentos,
        f.total,
        f.estado,
        f.notas,
        DATEDIFF(DAY, f.fecha_emision, GETDATE()) AS dias_transcurridos,
        (SELECT COUNT(*) FROM detalle_productos dp WHERE dp.factura_id = f.id) AS total_productos,
        (SELECT COUNT(*) FROM detalle_servicios ds WHERE ds.factura_id = f.id) AS total_servicios
    FROM factura f
    INNER JOIN VW_PersonasCompletas p ON f.persona_id = p.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND (@estado IS NULL OR f.estado = @estado)
    ORDER BY f.fecha_emision DESC, f.id DESC;
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasResumen
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @agrupacion VARCHAR(10) = 'DIA' /* 'DIA', 'SEMANA', 'MES', 'AÑO' */
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @fecha_inicio > @fecha_fin
    BEGIN
        RAISERROR('La fecha de inicio no puede ser mayor a la fecha fin', 16, 1);
        RETURN;
    END
    
    IF @agrupacion NOT IN ('DIA', 'SEMANA', 'MES', 'AÑO')
    BEGIN
        RAISERROR('Agrupación debe ser: DIA, SEMANA, MES o AÑO', 16, 1);
        RETURN;
    END
    
    IF @agrupacion = 'DIA'
    BEGIN
        SELECT 
            f.fecha_emision AS periodo,
            DATENAME(WEEKDAY, f.fecha_emision) AS dia_semana,
            COUNT(*) AS total_facturas,
            COUNT(CASE WHEN f.estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
            COUNT(CASE WHEN f.estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
            SUM(f.subtotal) AS subtotal_total,
            SUM(f.impuestos) AS impuestos_total,
            SUM(f.descuentos) AS descuentos_total,
            SUM(f.total) AS ventas_netas,
            AVG(f.total) AS promedio_venta,
            MIN(f.total) AS venta_minima,
            MAX(f.total) AS venta_maxima
        FROM factura f
        WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY f.fecha_emision
        ORDER BY f.fecha_emision DESC;
    END
    ELSE IF @agrupacion = 'SEMANA'
    BEGIN
        SELECT 
            YEAR(f.fecha_emision) AS año,
            DATEPART(WEEK, f.fecha_emision) AS semana,
            MIN(f.fecha_emision) AS fecha_inicio_semana,
            MAX(f.fecha_emision) AS fecha_fin_semana,
            COUNT(*) AS total_facturas,
            COUNT(CASE WHEN f.estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
            COUNT(CASE WHEN f.estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
            SUM(f.subtotal) AS subtotal_total,
            SUM(f.impuestos) AS impuestos_total,
            SUM(f.descuentos) AS descuentos_total,
            SUM(f.total) AS ventas_netas,
            AVG(f.total) AS promedio_venta
        FROM factura f
        WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(f.fecha_emision), DATEPART(WEEK, f.fecha_emision)
        ORDER BY año DESC, semana DESC;
    END
    ELSE IF @agrupacion = 'MES'
    BEGIN
        SELECT 
            YEAR(f.fecha_emision) AS año,
            MONTH(f.fecha_emision) AS mes,
            DATENAME(MONTH, f.fecha_emision) AS nombre_mes,
            COUNT(*) AS total_facturas,
            COUNT(CASE WHEN f.estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
            COUNT(CASE WHEN f.estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
            SUM(f.subtotal) AS subtotal_total,
            SUM(f.impuestos) AS impuestos_total,
            SUM(f.descuentos) AS descuentos_total,
            SUM(f.total) AS ventas_netas,
            AVG(f.total) AS promedio_venta
        FROM factura f
        WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(f.fecha_emision), MONTH(f.fecha_emision), DATENAME(MONTH, f.fecha_emision)
        ORDER BY año DESC, mes DESC;
    END
    ELSE IF @agrupacion = 'AÑO'
    BEGIN
        SELECT 
            YEAR(f.fecha_emision) AS año,
            COUNT(*) AS total_facturas,
            COUNT(CASE WHEN f.estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
            COUNT(CASE WHEN f.estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
            SUM(f.subtotal) AS subtotal_total,
            SUM(f.impuestos) AS impuestos_total,
            SUM(f.descuentos) AS descuentos_total,
            SUM(f.total) AS ventas_netas,
            AVG(f.total) AS promedio_venta
        FROM factura f
        WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        GROUP BY YEAR(f.fecha_emision)
        ORDER BY año DESC;
    END
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasDetalle
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @factura_id INT = NULL /* NULL = todas las facturas del rango */
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        'PRODUCTO' AS tipo_item,
        f.id AS factura_id,
        f.numero_factura,
        f.fecha_emision,
        p.nombre_mostrar AS cliente,
        prod.codigo AS codigo_item,
        prod.nombre AS nombre_item,
        cat.nombre AS categoria,
        dp.cantidad,
        dp.precio_unitario,
        dp.descuento_unitario,
        dp.subtotal,
        dp.lote,
        dp.fecha_vencimiento_producto,
        f.estado AS estado_factura
    FROM factura f
    INNER JOIN VW_PersonasCompletas p ON f.persona_id = p.id
    INNER JOIN detalle_productos dp ON f.id = dp.factura_id
    INNER JOIN producto prod ON dp.producto_id = prod.id
    INNER JOIN categoria cat ON prod.categoria_id = cat.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND (@factura_id IS NULL OR f.id = @factura_id)
    
    UNION ALL
    
    SELECT 
        'SERVICIO' AS tipo_item,
        f.id AS factura_id,
        f.numero_factura,
        f.fecha_emision,
        p.nombre_mostrar AS cliente,
        diag.codigo AS codigo_item,
        diag.nombre AS nombre_item,
        diag.categoria_diagnostico AS categoria,
        ds.cantidad,
        ds.precio_unitario,
        ds.descuento_unitario,
        ds.subtotal,
        NULL AS lote,
        NULL AS fecha_vencimiento_producto,
        f.estado AS estado_factura
    FROM factura f
    INNER JOIN VW_PersonasCompletas p ON f.persona_id = p.id
    INNER JOIN detalle_servicios ds ON f.id = ds.factura_id
    INNER JOIN diagnostico diag ON ds.diagnostico_id = diag.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND (@factura_id IS NULL OR f.id = @factura_id)
    
    ORDER BY fecha_emision DESC, factura_id, tipo_item;
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasPeriodosPredefinidos
    @periodo VARCHAR(20) /* 'HOY', 'AYER', 'ULTIMOS_7_DIAS', 'MES_ACTUAL', 'ULTIMOS_30_DIAS', 'AÑO_ACTUAL' */
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @fecha_inicio DATE;
    DECLARE @fecha_fin DATE;
    
    IF @periodo = 'HOY'
    BEGIN
        SET @fecha_inicio = CAST(GETDATE() AS DATE);
        SET @fecha_fin = CAST(GETDATE() AS DATE);
    END
    ELSE IF @periodo = 'AYER'
    BEGIN
        SET @fecha_inicio = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE);
        SET @fecha_fin = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE);
    END
    ELSE IF @periodo = 'ULTIMOS_7_DIAS'
    BEGIN
        SET @fecha_inicio = CAST(DATEADD(DAY, -6, GETDATE()) AS DATE);
        SET @fecha_fin = CAST(GETDATE() AS DATE);
    END
    ELSE IF @periodo = 'MES_ACTUAL'
    BEGIN
        SET @fecha_inicio = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
        SET @fecha_fin = CAST(GETDATE() AS DATE);
    END
    ELSE IF @periodo = 'ULTIMOS_30_DIAS'
    BEGIN
        SET @fecha_inicio = CAST(DATEADD(DAY, -29, GETDATE()) AS DATE);
        SET @fecha_fin = CAST(GETDATE() AS DATE);
    END
    ELSE IF @periodo = 'AÑO_ACTUAL'
    BEGIN
        SET @fecha_inicio = DATEFROMPARTS(YEAR(GETDATE()), 1, 1);
        SET @fecha_fin = CAST(GETDATE() AS DATE);
    END
    ELSE
    BEGIN
        RAISERROR('Período no válido. Use: HOY, AYER, ULTIMOS_7_DIAS, MES_ACTUAL, ULTIMOS_30_DIAS, AÑO_ACTUAL', 16, 1);
        RETURN;
    END
    
    EXEC SP_ReporteVentasPorRango @fecha_inicio, @fecha_fin;
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasTopClientes
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @top_count INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP (@top_count)
        p.id AS cliente_id,
        p.nombre_mostrar AS cliente,
        p.tipo AS tipo_cliente,
        p.email,
        p.telefono,
        COUNT(f.id) AS total_facturas,
        SUM(f.total) AS total_compras,
        AVG(f.total) AS promedio_compra,
        MAX(f.fecha_emision) AS ultima_compra,
        MIN(f.fecha_emision) AS primera_compra_periodo
    FROM factura f
    INNER JOIN VW_PersonasCompletas p ON f.persona_id = p.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND f.estado IN ('Pagada', 'Pendiente')
    GROUP BY p.id, p.nombre_mostrar, p.tipo, p.email, p.telefono
    ORDER BY total_compras DESC;
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasProductosTop
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @top_count INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP (@top_count)
        prod.id AS producto_id,
        prod.codigo,
        prod.nombre AS producto,
        cat.nombre AS categoria,
        SUM(dp.cantidad) AS total_vendido,
        COUNT(DISTINCT f.id) AS facturas_involucradas,
        SUM(dp.subtotal) AS ingresos_generados,
        AVG(dp.precio_unitario) AS precio_promedio,
        prod.stock_actual,
        prod.stock_minimo,
        CASE 
            WHEN prod.stock_actual <= prod.stock_minimo THEN 'STOCK BAJO'
            WHEN prod.stock_actual <= (prod.stock_minimo * 2) THEN 'STOCK MEDIO'
            ELSE 'STOCK OK'
        END AS estado_stock
    FROM detalle_productos dp
    INNER JOIN factura f ON dp.factura_id = f.id
    INNER JOIN producto prod ON dp.producto_id = prod.id
    INNER JOIN categoria cat ON prod.categoria_id = cat.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND f.estado IN ('Pagada', 'Pendiente')
    GROUP BY prod.id, prod.codigo, prod.nombre, cat.nombre, 
             prod.stock_actual, prod.stock_minimo
    ORDER BY total_vendido DESC;
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasServiciosTop
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @top_count INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP (@top_count)
        diag.id AS servicio_id,
        diag.codigo,
        diag.nombre AS servicio,
        diag.categoria_diagnostico AS categoria,
        SUM(ds.cantidad) AS total_prestado,
        COUNT(DISTINCT f.id) AS facturas_involucradas,
        SUM(ds.subtotal) AS ingresos_generados,
        AVG(ds.precio_unitario) AS precio_promedio,
        diag.precio_base,
        COUNT(DISTINCT ds.veterinario_id) AS veterinarios_involucrados
    FROM detalle_servicios ds
    INNER JOIN factura f ON ds.factura_id = f.id
    INNER JOIN diagnostico diag ON ds.diagnostico_id = diag.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND f.estado IN ('Pagada', 'Pendiente')
    GROUP BY diag.id, diag.codigo, diag.nombre, diag.categoria_diagnostico, diag.precio_base
    ORDER BY total_prestado DESC;
END;
GO

CREATE OR ALTER PROCEDURE SP_ReporteVentasEstadisticasGenerales
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        'ESTADISTICAS_GENERALES' AS tipo_estadistica,
        COUNT(*) AS total_facturas,
        COUNT(CASE WHEN estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
        COUNT(CASE WHEN estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
        COUNT(CASE WHEN estado = 'Cancelada' THEN 1 END) AS facturas_canceladas,
        SUM(total) AS ingresos_totales,
        AVG(total) AS promedio_venta,
        MIN(fecha_emision) AS primera_venta,
        MAX(fecha_emision) AS ultima_venta
    FROM factura
    
    UNION ALL
    
    SELECT 
        'MES_ACTUAL' AS tipo_estadistica,
        COUNT(*) AS total_facturas,
        COUNT(CASE WHEN estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
        COUNT(CASE WHEN estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
        COUNT(CASE WHEN estado = 'Cancelada' THEN 1 END) AS facturas_canceladas,
        SUM(total) AS ingresos_totales,
        AVG(total) AS promedio_venta,
        MIN(fecha_emision) AS primera_venta,
        MAX(fecha_emision) AS ultima_venta
    FROM factura
    WHERE YEAR(fecha_emision) = YEAR(GETDATE()) 
        AND MONTH(fecha_emision) = MONTH(GETDATE())
    
    UNION ALL
    
    SELECT 
        'AÑO_ACTUAL' AS tipo_estadistica,
        COUNT(*) AS total_facturas,
        COUNT(CASE WHEN estado = 'Pagada' THEN 1 END) AS facturas_pagadas,
        COUNT(CASE WHEN estado = 'Pendiente' THEN 1 END) AS facturas_pendientes,
        COUNT(CASE WHEN estado = 'Cancelada' THEN 1 END) AS facturas_canceladas,
        SUM(total) AS ingresos_totales,
        AVG(total) AS promedio_venta,
        MIN(fecha_emision) AS primera_venta,
        MAX(fecha_emision) AS ultima_venta
    FROM factura
    WHERE YEAR(fecha_emision) = YEAR(GETDATE());
END;
GO

PRINT 'Procedimientos almacenados para reportes de ventas agregados:';
PRINT '- SP_ReporteVentasPorRango';
PRINT '- SP_ReporteVentasResumen';
PRINT '- SP_ReporteVentasDetalle';
PRINT '- SP_ReporteVentasPeriodosPredefinidos';
PRINT '- SP_ReporteVentasTopClientes';
PRINT '- SP_ReporteVentasProductosTop';
PRINT '- SP_ReporteVentasServiciosTop';
PRINT '- SP_ReporteVentasEstadisticasGenerales';
GO