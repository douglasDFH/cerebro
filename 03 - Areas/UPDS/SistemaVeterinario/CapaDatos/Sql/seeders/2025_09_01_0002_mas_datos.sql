
-- =============================================
-- 6. GENERAR 100 FACTURAS CON PRODUCTOS Y SERVICIOS
-- =============================================
DECLARE @i INT, @persona_id INT;
DECLARE @factura_id INT, @numero_factura VARCHAR(50);
DECLARE @fecha_emision DATE, @fecha_vencimiento DATE;
DECLARE @subtotal DECIMAL(10,2), @impuestos DECIMAL(10,2), @descuentos DECIMAL(10,2), @total DECIMAL(10,2);
DECLARE @estado VARCHAR(20);
DECLARE @num_productos INT, @j INT;
DECLARE @producto_id INT, @cantidad INT, @precio_unitario DECIMAL(10,2);
DECLARE @descuento_unitario DECIMAL(10,2), @subtotal_producto DECIMAL(10,2);
DECLARE @cliente_tiene_mascotas BIT;
DECLARE @num_servicios INT;
DECLARE @diagnostico_id INT, @precio_servicio DECIMAL(10,2);
DECLARE @descuento_servicio DECIMAL(10,2), @subtotal_servicio DECIMAL(10,2);
DECLARE @veterinario_id INT, @animal_servicio_id INT, @detalle_historico_id INT;
DECLARE @historico_animal_id INT;

BEGIN TRANSACTION;

SET @i = 1;
WHILE @i <= 100
BEGIN
    SET @subtotal = 0;
    SET @descuentos = 0;
    SET @numero_factura = 'FAC-' + RIGHT('0000' + CAST(@i AS VARCHAR), 4);
    
    -- Seleccionar persona aleatoria
    SELECT TOP 1 @persona_id = id FROM persona WHERE activo = 1 ORDER BY NEWID();
    
    -- Fecha aleatoria en los últimos 365 días
    SET @fecha_emision = DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 365), GETDATE());
    SET @fecha_vencimiento = DATEADD(DAY, 30, @fecha_emision);
    
    -- Estado aleatorio
    SET @estado = CASE ABS(CHECKSUM(NEWID())) % 100
        WHEN 0 THEN 'Cancelada' 
        WHEN 1 THEN 'Anulada'
        ELSE CASE WHEN ABS(CHECKSUM(NEWID())) % 100 < 85 THEN 'Pagada' ELSE 'Pendiente' END
    END;
    
    -- Crear factura
    INSERT INTO factura (numero_factura, fecha_emision, fecha_vencimiento, persona_id, 
                        subtotal, impuestos, descuentos, total, estado, notas)
    VALUES (@numero_factura, @fecha_emision, @fecha_vencimiento, @persona_id,
            0, 0, 0, 0, @estado, 'Factura generada automáticamente');
    
    SET @factura_id = SCOPE_IDENTITY();
    
    -- Agregar productos (1-4 productos por factura)
    SET @num_productos = (ABS(CHECKSUM(NEWID())) % 4) + 1;
    SET @j = 1;
    
    WHILE @j <= @num_productos
    BEGIN
        SET @descuento_unitario = 0;
        
        -- Seleccionar producto aleatorio
        SELECT TOP 1 @producto_id = id, @precio_unitario = precio 
        FROM producto WHERE activo = 1 ORDER BY NEWID();
        
        SET @cantidad = (ABS(CHECKSUM(NEWID())) % 5) + 1;
        
        -- 20% probabilidad de descuento
        IF (ABS(CHECKSUM(NEWID())) % 100) < 20
            SET @descuento_unitario = @precio_unitario * 0.1; -- 10% descuento
        
        SET @subtotal_producto = (@precio_unitario - @descuento_unitario) * @cantidad;
        SET @subtotal = @subtotal + @subtotal_producto;
        
        -- Insertar detalle producto
        INSERT INTO detalle_productos (factura_id, producto_id, cantidad, precio_unitario, 
                                     descuento_unitario, subtotal, receta_verificada)
        VALUES (@factura_id, @producto_id, @cantidad, @precio_unitario, 
                @descuento_unitario, @subtotal_producto, 
                CASE WHEN (SELECT requiere_receta FROM producto WHERE id = @producto_id) = 1 
                     THEN ABS(CHECKSUM(NEWID())) % 2 ELSE 0 END);
        
        -- Actualizar stock SOLO si hay suficiente stock disponible
        UPDATE producto 
        SET stock_actual = stock_actual - @cantidad 
        WHERE id = @producto_id 
          AND stock_actual >= @cantidad; -- Validación para evitar stock negativo
        
        SET @j = @j + 1;
    END;
    
    -- Agregar servicios (1-3 servicios por factura)
    -- IMPORTANTE: Solo agregar servicios si el cliente tiene mascotas
    SET @cliente_tiene_mascotas = 0;
    SELECT @cliente_tiene_mascotas = CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END
    FROM animal WHERE persona_id = @persona_id AND activo = 1;
    
    IF @cliente_tiene_mascotas = 1
    BEGIN
        SET @num_servicios = (ABS(CHECKSUM(NEWID())) % 3) + 1;
        SET @j = 1;
        
        WHILE @j <= @num_servicios
        BEGIN
            SET @descuento_servicio = 0;
            
            -- Seleccionar servicio aleatorio
            SELECT TOP 1 @diagnostico_id = id, @precio_servicio = precio_base 
            FROM diagnostico WHERE activo = 1 ORDER BY NEWID();
            
            -- Seleccionar veterinario aleatorio
            SELECT TOP 1 @veterinario_id = id FROM personal_veterinario ORDER BY NEWID();
            
            -- SELECCIONAR UNA MASCOTA DEL CLIENTE PARA ASOCIAR EL SERVICIO
            SELECT TOP 1 @animal_servicio_id = id 
            FROM animal 
            WHERE persona_id = @persona_id AND activo = 1 
            ORDER BY NEWID();
            
            -- Crear detalle histórico para vincular el servicio con la mascota
            SELECT @historico_animal_id = id FROM historico WHERE animal_id = @animal_servicio_id;
            
            -- Verificar que existe el histórico del animal
            IF @historico_animal_id IS NOT NULL
            BEGIN
                INSERT INTO detalle_historico (
                    historico_id, tipo_evento, fecha_evento, observaciones, 
                    tratamiento, veterinario_id, peso_animal, cobrado, costo
                ) VALUES (
                    @historico_animal_id, 'Consulta', @fecha_emision,
                    'Servicio facturado: ' + (SELECT nombre FROM diagnostico WHERE id = @diagnostico_id),
                    'Pendiente según diagnóstico', @veterinario_id,
                    (SELECT peso FROM animal WHERE id = @animal_servicio_id), 1, @precio_servicio
                );
                
                SET @detalle_historico_id = SCOPE_IDENTITY();
            END
            ELSE
            BEGIN
                -- Si no existe histórico, crear uno primero
                INSERT INTO historico (animal_id, notas_generales, alergias, condiciones_medicas)
                VALUES (@animal_servicio_id, 'Histórico creado automáticamente', NULL, NULL);
                
                SET @historico_animal_id = SCOPE_IDENTITY();
                
                INSERT INTO detalle_historico (
                    historico_id, tipo_evento, fecha_evento, observaciones, 
                    tratamiento, veterinario_id, peso_animal, cobrado, costo
                ) VALUES (
                    @historico_animal_id, 'Consulta', @fecha_emision,
                    'Servicio facturado: ' + (SELECT nombre FROM diagnostico WHERE id = @diagnostico_id),
                    'Pendiente según diagnóstico', @veterinario_id,
                    (SELECT peso FROM animal WHERE id = @animal_servicio_id), 1, @precio_servicio
                );
                
                SET @detalle_historico_id = SCOPE_IDENTITY();
            END;
            
            -- 15% probabilidad de descuento en servicios
            IF (ABS(CHECKSUM(NEWID())) % 100) < 15
                SET @descuento_servicio = @precio_servicio * 0.05; -- 5% descuento
            
            SET @subtotal_servicio = @precio_servicio - @descuento_servicio;
            SET @subtotal = @subtotal + @subtotal_servicio;
            
            -- Insertar detalle servicio VINCULADO A LA MASCOTA
            INSERT INTO detalle_servicios (factura_id, diagnostico_id, detalle_historico_id,
                                         cantidad, precio_unitario, descuento_unitario, 
                                         subtotal, veterinario_id)
            VALUES (@factura_id, @diagnostico_id, @detalle_historico_id, 1, @precio_servicio, 
                    @descuento_servicio, @subtotal_servicio, @veterinario_id);
            
            SET @j = @j + 1;
        END;
    END
    -- Si el cliente no tiene mascotas, solo se agregan productos (sin servicios)
    
    -- Calcular totales
    SET @impuestos = @subtotal * 0.13; -- 13% IVA
    SET @total = @subtotal + @impuestos - @descuentos;
    
    -- Actualizar factura con totales
    UPDATE factura 
    SET subtotal = @subtotal, impuestos = @impuestos, descuentos = @descuentos, total = @total
    WHERE id = @factura_id;
    
    SET @i = @i + 1;
END;


COMMIT TRANSACTION;
