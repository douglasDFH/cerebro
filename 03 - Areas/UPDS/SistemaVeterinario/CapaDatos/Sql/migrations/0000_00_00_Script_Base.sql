-- Eliminar la base de datos si existe
IF DB_ID('Sistema_Veterinario') IS NOT NULL 
BEGIN 
    ALTER DATABASE Sistema_Veterinario SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Sistema_Veterinario;
END
GO

-- Crear y usar la base de datos
CREATE DATABASE Sistema_Veterinario;
GO

USE Sistema_Veterinario;
GO -- ============================================
    -- TABLA: PERSONA (Tabla principal de personas)
    -- ============================================
    CREATE TABLE persona (
        id INT IDENTITY(1, 1) NOT NULL,
        tipo VARCHAR(20) NOT NULL,
        email VARCHAR(255) NULL,
        direccion VARCHAR(255) NULL,
        telefono VARCHAR(20) NULL,
        activo BIT NOT NULL DEFAULT 1,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_persona PRIMARY KEY (id),
        -- ENUM coherente mejorado
        CONSTRAINT CK_persona_tipo CHECK (tipo IN ('Física', 'Jurídica')),
        CONSTRAINT CK_persona_email CHECK (
            email LIKE '%@%'
            OR email IS NULL
        )
    );
GO -- ============================================
    -- TABLA: PERSONA_FISICA (Herencia de persona)
    -- ============================================
    CREATE TABLE persona_fisica (
        id INT NOT NULL,
        ci VARCHAR(15) NULL,
        nombre VARCHAR(100) NOT NULL,
        apellido VARCHAR(100) NOT NULL,
        fecha_nacimiento DATE NULL,
        genero CHAR(1) NULL,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_persona_fisica PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_persona_fisica_persona FOREIGN KEY (id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- CHECK constraints para validaciones
        CONSTRAINT CK_persona_fisica_genero CHECK (
            genero IN ('M', 'F')
            OR genero IS NULL
        ),
        CONSTRAINT UK_persona_fisica_ci UNIQUE (ci)
    );
GO -- ============================================
    -- TABLA: PERSONA_JURIDICA (Herencia de persona)
    -- ============================================
    CREATE TABLE persona_juridica (
        id INT NOT NULL,
        razon_social VARCHAR(255) NOT NULL,
        nit VARCHAR(20) NULL,
        encargado_nombre VARCHAR(255) NULL,
        encargado_cargo VARCHAR(100) NULL,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_persona_juridica PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_persona_juridica_persona FOREIGN KEY (id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- Unique constraints
        CONSTRAINT UK_persona_juridica_nit UNIQUE (nit)
    );
GO -- ============================================
    -- TABLA: PERSONAL (Empleados del sistema)
    -- ============================================
    CREATE TABLE personal (
        id INT IDENTITY(1, 1) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        apellido VARCHAR(100) NOT NULL,
        email VARCHAR(100) NOT NULL,
        usuario VARCHAR(50) NOT NULL,
        contrasena VARCHAR(255) NOT NULL,
        telefono VARCHAR(20) NULL,
        direccion VARCHAR(255) NULL,
        fecha_contratacion DATE NOT NULL DEFAULT GETDATE(),
        salario DECIMAL(10, 2) NULL,
        rol VARCHAR(20) NOT NULL DEFAULT 'Usuario',
        activo BIT NOT NULL DEFAULT 1,
        fecha_ultimo_acceso DATETIME NULL,
        creado_por VARCHAR(50) DEFAULT 'Sistema',
        modificado_por VARCHAR(50) NULL,
        fecha_modificacion DATETIME NULL,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_personal PRIMARY KEY (id),
        -- Definir Unique Constraints
        CONSTRAINT UK_personal_usuario UNIQUE (usuario),
        CONSTRAINT UK_personal_email UNIQUE (email),
        -- CHECK constraints
        CONSTRAINT CK_personal_salario CHECK (
            salario >= 0
            OR salario IS NULL
        ),
        CONSTRAINT CK_personal_email CHECK (email LIKE '%@%')
    );
GO -- ============================================
    -- TABLA: PERSONAL_VETERINARIO (Herencia de personal)
    -- ============================================
    CREATE TABLE personal_veterinario (
        id INT NOT NULL,
        num_licencia VARCHAR(50) NOT NULL,
        especialidad VARCHAR(100) NULL,
        universidad VARCHAR(200) NULL,
        anios_experiencia INT NULL DEFAULT 0,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_personal_veterinario PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_personal_veterinario_personal FOREIGN KEY (id) REFERENCES personal(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- Definir Unique Constraints
        CONSTRAINT UK_personal_veterinario_licencia UNIQUE (num_licencia),
        -- CHECK constraints
        CONSTRAINT CK_veterinario_experiencia CHECK (anios_experiencia >= 0)
    );
GO -- ============================================
    -- TABLA: PERSONAL_AUXILIAR (Herencia de personal)
    -- ============================================
    CREATE TABLE personal_auxiliar (
        id INT NOT NULL,
        area VARCHAR(100) NULL,
        turno VARCHAR(10) NOT NULL,
        nivel VARCHAR(20) NULL DEFAULT 'Básico',
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_personal_auxiliar PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_personal_auxiliar_personal FOREIGN KEY (id) REFERENCES personal(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- Definir Check Constraints para ENUM coherente
        CONSTRAINT CK_personal_auxiliar_turno CHECK (turno IN ('Mañana', 'Tarde', 'Noche')),
        CONSTRAINT CK_auxiliar_nivel CHECK (nivel IN ('Básico', 'Intermedio', 'Avanzado'))
    );
GO -- ============================================
    -- TABLA: ANIMAL (Mascotas registradas)
    -- ============================================
    CREATE TABLE animal (
        id INT IDENTITY(1, 1) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        especie VARCHAR(50) NOT NULL,
        raza VARCHAR(100) NULL,
        fecha_nacimiento DATE NULL,
        peso DECIMAL(5, 2) NULL,
        color VARCHAR(50) NULL,
        genero CHAR(1) NULL,
        esterilizado BIT NULL DEFAULT 0,
        microchip VARCHAR(50) NULL,
        persona_id INT NOT NULL,
        activo BIT NOT NULL DEFAULT 1,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_animal PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_animal_persona FOREIGN KEY (persona_id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- CHECK constraints
        CONSTRAINT CK_animal_genero CHECK (
            genero IN ('M', 'F')
            OR genero IS NULL
        ),
        CONSTRAINT CK_animal_peso CHECK (
            peso > 0
            OR peso IS NULL
        ),
        CONSTRAINT UK_animal_microchip UNIQUE (microchip)
    );
GO -- ============================================
    -- TABLA: HISTORICO (Historial clinico de animales)
    -- ============================================
    CREATE TABLE historico (
        id INT IDENTITY(1, 1) NOT NULL,
        animal_id INT NOT NULL,
        notas_generales TEXT NULL,
        alergias TEXT NULL,
        condiciones_medicas TEXT NULL,
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_historico PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_historico_animal FOREIGN KEY (animal_id) REFERENCES animal(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- Definir Unique Constraints (un historial por animal)
        CONSTRAINT UK_historico_animal UNIQUE (animal_id)
    );
GO -- ============================================
    -- TABLA: DETALLE_HISTORICO (Eventos del historial)
    -- ============================================
    CREATE TABLE detalle_historico (
        id INT IDENTITY(1, 1) NOT NULL,
        historico_id INT NOT NULL,
        tipo_evento VARCHAR(20) NOT NULL,
        fecha_evento DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        observaciones TEXT NULL,
        tratamiento TEXT NULL,
        medicamentos TEXT NULL,
        dosis VARCHAR(255) NULL,
        veterinario_id INT NULL,
        peso_animal DECIMAL(5, 2) NULL,
        temperatura DECIMAL(4, 1) NULL,
        cobrado BIT NOT NULL DEFAULT 0,
        costo DECIMAL(10, 2) NULL DEFAULT 0,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_detalle_historico PRIMARY KEY (id),
        -- Definir Foreign Keys
        CONSTRAINT FK_detalle_historico_historico FOREIGN KEY (historico_id) REFERENCES historico(id) ON DELETE CASCADE ON UPDATE CASCADE,
        CONSTRAINT FK_detalle_historico_veterinario FOREIGN KEY (veterinario_id) REFERENCES personal_veterinario(id) ON DELETE
        SET NULL ON UPDATE CASCADE,
            -- Definir Check Constraints para ENUM coherente mejorado
            CONSTRAINT CK_detalle_historico_tipo CHECK (
                tipo_evento IN (
                    'Diagnóstico',
                    'Tratamiento',
                    'Control',
                    'Vacunación',
                    'Cirugía',
                    'Consulta'
                )
            ),
            CONSTRAINT CK_detalle_costo CHECK (
                costo >= 0
                OR costo IS NULL
            ),
            CONSTRAINT CK_detalle_peso CHECK (
                peso_animal > 0
                OR peso_animal IS NULL
            ),
            CONSTRAINT CK_detalle_temperatura CHECK (
                temperatura BETWEEN 35.0 AND 45.0
                OR temperatura IS NULL
            )
    );
GO -- ============================================
    -- TABLA: CATEGORIA (Categorías de productos)
    -- ============================================
    CREATE TABLE categoria (
        id INT IDENTITY(1, 1) NOT NULL,
        nombre VARCHAR(100) NOT NULL,
        descripcion VARCHAR(255) NULL,
        activo BIT NOT NULL DEFAULT 1,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_categoria PRIMARY KEY (id),
        -- Definir Unique Constraints
        CONSTRAINT UK_categoria_nombre UNIQUE (nombre)
    );
GO -- ============================================
    -- TABLA: PRODUCTO (Productos vendidos)
    -- ============================================
    CREATE TABLE producto (
        id INT IDENTITY(1, 1) NOT NULL,
        codigo VARCHAR(50) NULL,
        nombre VARCHAR(255) NOT NULL,
        descripcion TEXT NULL,
        precio DECIMAL(10, 2) NOT NULL,
        stock_minimo INT NOT NULL DEFAULT 5,
        stock_actual INT NOT NULL DEFAULT 0,
        requiere_receta BIT NOT NULL DEFAULT 1,
        categoria_id INT NOT NULL,
        activo BIT NOT NULL DEFAULT 1,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_producto PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_producto_categoria FOREIGN KEY (categoria_id) REFERENCES categoria(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- Definir Check Constraints
        CONSTRAINT CK_producto_precio CHECK (precio >= 0),
        CONSTRAINT CK_producto_stock CHECK (
            stock_actual >= 0
            AND stock_minimo >= 0
        ),
        CONSTRAINT UK_producto_codigo UNIQUE (codigo)
    );
GO -- ============================================
    -- TABLA: DIAGNOSTICO (Diagnósticos realizados)
    -- ============================================
    CREATE TABLE diagnostico (
        id INT IDENTITY(1, 1) NOT NULL,
        codigo VARCHAR(20) NULL,
        nombre VARCHAR(200) NOT NULL,
        descripcion TEXT NULL,
        precio_base DECIMAL(10, 2) NOT NULL DEFAULT 0,
        categoria_diagnostico VARCHAR(100) NULL,
        requiere_equipamiento BIT NOT NULL DEFAULT 0,
        activo BIT NOT NULL DEFAULT 1,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_diagnostico PRIMARY KEY (id),
        -- Definir Check Constraints
        CONSTRAINT CK_diagnostico_precio CHECK (precio_base >= 0),
        CONSTRAINT UK_diagnostico_codigo UNIQUE (codigo)
    );
GO -- ============================================
    -- TABLA: FACTURA (Facturas emitidas)
    -- ============================================
    CREATE TABLE factura (
        id INT IDENTITY(1, 1) NOT NULL,
        numero_factura VARCHAR(50) NOT NULL,
        fecha_emision DATE NOT NULL DEFAULT GETDATE(),
        fecha_vencimiento DATE NULL,
        persona_id INT NOT NULL,
        subtotal DECIMAL(10, 2) NOT NULL DEFAULT 0,
        impuestos DECIMAL(10, 2) NOT NULL DEFAULT 0,
        descuentos DECIMAL(10, 2) NOT NULL DEFAULT 0,
        total DECIMAL(10, 2) NOT NULL DEFAULT 0,
        estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
        notas TEXT NULL,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_factura PRIMARY KEY (id),
        -- Definir Foreign Key
        CONSTRAINT FK_factura_persona FOREIGN KEY (persona_id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
        -- Definir Unique Constraints
        CONSTRAINT UK_factura_numero UNIQUE (numero_factura),
        -- Definir Check Constraints
        CONSTRAINT CK_factura_montos CHECK (
            subtotal >= 0
            AND impuestos >= 0
            AND descuentos >= 0
            AND total >= 0
        ),
        CONSTRAINT CK_factura_estado CHECK (
            estado IN ('Pendiente', 'Pagada', 'Cancelada', 'Anulada')
        )
    );
GO -- ============================================
    -- TABLA: DETALLE_PRODUCTOS (Detalle de productos en factura)
    -- ============================================
    CREATE TABLE detalle_productos (
        id INT IDENTITY(1, 1) NOT NULL,
        factura_id INT NOT NULL,
        producto_id INT NOT NULL,
        cantidad INT NOT NULL,
        precio_unitario DECIMAL(10, 2) NOT NULL,
        descuento_unitario DECIMAL(10, 2) NOT NULL DEFAULT 0,
        subtotal DECIMAL(10, 2) NOT NULL DEFAULT 0,
        receta_verificada BIT NOT NULL DEFAULT 0,
        lote VARCHAR(50) NULL,
        fecha_vencimiento_producto DATE NULL,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_detalle_productos PRIMARY KEY (id),
        -- Definir Foreign Keys
        CONSTRAINT FK_detalle_productos_factura FOREIGN KEY (factura_id) REFERENCES factura(id) ON DELETE CASCADE ON UPDATE CASCADE,
        CONSTRAINT FK_detalle_productos_producto FOREIGN KEY (producto_id) REFERENCES producto(id) ON DELETE NO ACTION ON UPDATE CASCADE,
        -- Definir Check Constraints
        CONSTRAINT CK_detalle_productos_cantidad CHECK (cantidad > 0),
        CONSTRAINT CK_detalle_productos_precios CHECK (
            precio_unitario >= 0
            AND descuento_unitario >= 0
            AND subtotal >= 0
        )
    );
GO -- ============================================
    -- TABLA: DETALLE_SERVICIOS (Detalle de servicios en factura)
    -- ============================================
    CREATE TABLE detalle_servicios (
        id INT IDENTITY(1, 1) NOT NULL,
        factura_id INT NOT NULL,
        diagnostico_id INT NOT NULL,
        detalle_historico_id INT NULL,
        cantidad INT NOT NULL DEFAULT 1,
        precio_unitario DECIMAL(10, 2) NOT NULL,
        descuento_unitario DECIMAL(10, 2) NOT NULL DEFAULT 0,
        subtotal DECIMAL(10, 2) NOT NULL DEFAULT 0,
        veterinario_id INT NULL,
        -- TIMESTAMPS estilo Laravel
        created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        -- Definir Primary Key
        CONSTRAINT PK_detalle_servicios PRIMARY KEY (id),
        -- Definir Foreign Keys
        CONSTRAINT FK_detalle_servicios_factura FOREIGN KEY (factura_id) REFERENCES factura(id) ON DELETE CASCADE ON UPDATE CASCADE,
        CONSTRAINT FK_detalle_servicios_diagnostico FOREIGN KEY (diagnostico_id) REFERENCES diagnostico(id) ON DELETE NO ACTION ON UPDATE CASCADE,
        CONSTRAINT FK_detalle_servicios_historico FOREIGN KEY (detalle_historico_id) REFERENCES detalle_historico(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
        CONSTRAINT FK_detalle_servicios_veterinario FOREIGN KEY (veterinario_id) REFERENCES personal_veterinario(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
            -- Definir Check Constraints
            CONSTRAINT CK_detalle_servicios_cantidad CHECK (cantidad > 0),
            CONSTRAINT CK_detalle_servicios_precios CHECK (
                precio_unitario >= 0
                AND descuento_unitario >= 0
                AND subtotal >= 0
            )
    );
GO -- ============================================
    -- INDICES BASICOS PARA RENDIMIENTO
    -- ============================================
    -- Indices para busquedas frecuentes
    CREATE NONCLUSTERED INDEX IX_persona_tipo ON persona(tipo);
CREATE NONCLUSTERED INDEX IX_persona_fisica_ci ON persona_fisica(ci);
CREATE NONCLUSTERED INDEX IX_persona_fisica_nombre_apellido ON persona_fisica(nombre, apellido);
CREATE NONCLUSTERED INDEX IX_persona_juridica_nit ON persona_juridica(nit);
CREATE NONCLUSTERED INDEX IX_personal_usuario ON personal(usuario);
CREATE NONCLUSTERED INDEX IX_animal_persona_id ON animal(persona_id);
CREATE NONCLUSTERED INDEX IX_animal_nombre ON animal(nombre);
CREATE NONCLUSTERED INDEX IX_historico_animal_id ON historico(animal_id);
CREATE NONCLUSTERED INDEX IX_detalle_historico_historico_id ON detalle_historico(historico_id);
CREATE NONCLUSTERED INDEX IX_detalle_historico_tipo_evento ON detalle_historico(tipo_evento);
CREATE NONCLUSTERED INDEX IX_producto_categoria_id ON producto(categoria_id);
CREATE NONCLUSTERED INDEX IX_producto_nombre ON producto(nombre);
CREATE NONCLUSTERED INDEX IX_factura_persona_id ON factura(persona_id);
CREATE NONCLUSTERED INDEX IX_factura_fecha_emision ON factura(fecha_emision);
CREATE NONCLUSTERED INDEX IX_detalle_productos_factura_id ON detalle_productos(factura_id);
CREATE NONCLUSTERED INDEX IX_detalle_servicios_factura_id ON detalle_servicios(factura_id);
GO -- ============================================
    -- TRIGGERS PARA ACTUALIZACIÓN AUTOMÁTICA DE updated_at
    -- Estos triggers actualizan automáticamente el campo updated_at
    -- ============================================
    -- Trigger para actualizar updated_at en PERSONA
    CREATE TRIGGER TR_UpdatedAt_Persona ON persona
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE persona
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en PERSONA_FISICA
    CREATE TRIGGER TR_UpdatedAt_PersonaFisica ON persona_fisica
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE persona_fisica
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en PERSONA_JURIDICA
    CREATE TRIGGER TR_UpdatedAt_PersonaJuridica ON persona_juridica
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE persona_juridica
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en PERSONAL
    CREATE TRIGGER TR_UpdatedAt_Personal ON personal
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE personal
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en PERSONAL_VETERINARIO
    CREATE TRIGGER TR_UpdatedAt_PersonalVeterinario ON personal_veterinario
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE personal_veterinario
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en PERSONAL_AUXILIAR
    CREATE TRIGGER TR_UpdatedAt_PersonalAuxiliar ON personal_auxiliar
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE personal_auxiliar
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en ANIMAL
    CREATE TRIGGER TR_UpdatedAt_Animal ON animal
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE animal
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en HISTORICO
    CREATE TRIGGER TR_UpdatedAt_Historico ON historico
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE historico
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en DETALLE_HISTORICO
    CREATE TRIGGER TR_UpdatedAt_DetalleHistorico ON detalle_historico
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE detalle_historico
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en CATEGORIA
    CREATE TRIGGER TR_UpdatedAt_Categoria ON categoria
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE categoria
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en PRODUCTO
    CREATE TRIGGER TR_UpdatedAt_Producto ON producto
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE producto
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en DIAGNOSTICO
    CREATE TRIGGER TR_UpdatedAt_Diagnostico ON diagnostico
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE diagnostico
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en FACTURA
    CREATE TRIGGER TR_UpdatedAt_Factura ON factura
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE factura
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en DETALLE_PRODUCTOS
    CREATE TRIGGER TR_UpdatedAt_DetalleProductos ON detalle_productos
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE detalle_productos
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- Trigger para actualizar updated_at en DETALLE_SERVICIOS
    CREATE TRIGGER TR_UpdatedAt_DetalleServicios ON detalle_servicios
AFTER
UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE detalle_servicios
SET updated_at = GETDATE()
WHERE id IN (
        SELECT id
        FROM inserted
    );
END;
GO -- ============================================
    -- TRIGGERS ESENCIALES PARA CALCULOS AUTOMATICOS
    -- ============================================
    -- Trigger para calcular subtotal automaticamente en detalle_productos
    CREATE TRIGGER TR_CalcularSubtotal_DetalleProductos ON detalle_productos
AFTER
INSERT,
    UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE dp
SET subtotal = i.cantidad * i.precio_unitario - (i.cantidad * i.descuento_unitario)
FROM detalle_productos dp
    INNER JOIN inserted i ON dp.id = i.id;
END;
GO -- Trigger para calcular subtotal automaticamente en detalle_servicios
    CREATE TRIGGER TR_CalcularSubtotal_DetalleServicios ON detalle_servicios
AFTER
INSERT,
    UPDATE AS BEGIN
SET NOCOUNT ON;
UPDATE ds
SET subtotal = i.cantidad * i.precio_unitario - (i.cantidad * i.descuento_unitario)
FROM detalle_servicios ds
    INNER JOIN inserted i ON ds.id = i.id;
END;
GO -- Trigger para actualizar total de factura (productos)
    CREATE TRIGGER TR_ActualizarTotal_Factura_Productos ON detalle_productos
AFTER
INSERT,
    UPDATE,
    DELETE AS BEGIN
SET NOCOUNT ON;
DECLARE @facturas_afectadas TABLE (factura_id INT);
INSERT INTO @facturas_afectadas (factura_id)
SELECT DISTINCT factura_id
FROM inserted
UNION
SELECT DISTINCT factura_id
FROM deleted;
UPDATE f
SET subtotal = ISNULL(productos.total_productos, 0) + ISNULL(servicios.total_servicios, 0),
    total = ISNULL(productos.total_productos, 0) + ISNULL(servicios.total_servicios, 0) + f.impuestos - f.descuentos
FROM factura f
    LEFT JOIN (
        SELECT factura_id,
            SUM(subtotal) as total_productos
        FROM detalle_productos
        WHERE factura_id IN (
                SELECT factura_id
                FROM @facturas_afectadas
            )
        GROUP BY factura_id
    ) productos ON f.id = productos.factura_id
    LEFT JOIN (
        SELECT factura_id,
            SUM(subtotal) as total_servicios
        FROM detalle_servicios
        WHERE factura_id IN (
                SELECT factura_id
                FROM @facturas_afectadas
            )
        GROUP BY factura_id
    ) servicios ON f.id = servicios.factura_id
WHERE f.id IN (
        SELECT factura_id
        FROM @facturas_afectadas
    );
END;
GO -- Trigger para actualizar total de factura (servicios)
    CREATE TRIGGER TR_ActualizarTotal_Factura_Servicios ON detalle_servicios
AFTER
INSERT,
    UPDATE,
    DELETE AS BEGIN
SET NOCOUNT ON;
DECLARE @facturas_afectadas TABLE (factura_id INT);
INSERT INTO @facturas_afectadas (factura_id)
SELECT DISTINCT factura_id
FROM inserted
UNION
SELECT DISTINCT factura_id
FROM deleted;
UPDATE f
SET subtotal = ISNULL(productos.total_productos, 0) + ISNULL(servicios.total_servicios, 0),
    total = ISNULL(productos.total_productos, 0) + ISNULL(servicios.total_servicios, 0) + f.impuestos - f.descuentos
FROM factura f
    LEFT JOIN (
        SELECT factura_id,
            SUM(subtotal) as total_productos
        FROM detalle_productos
        WHERE factura_id IN (
                SELECT factura_id
                FROM @facturas_afectadas
            )
        GROUP BY factura_id
    ) productos ON f.id = productos.factura_id
    LEFT JOIN (
        SELECT factura_id,
            SUM(subtotal) as total_servicios
        FROM detalle_servicios
        WHERE factura_id IN (
                SELECT factura_id
                FROM @facturas_afectadas
            )
        GROUP BY factura_id
    ) servicios ON f.id = servicios.factura_id
WHERE f.id IN (
        SELECT factura_id
        FROM @facturas_afectadas
    );
END;
GO -- ============================================
    -- VISTAS BASICAS PARA CONSULTAS COMUNES
    -- ============================================
    -- Vista consolidada de todas las personas (físicas y jurídicas)
    CREATE VIEW VW_PersonasCompletas AS
SELECT p.id,
    p.tipo,
    p.email,
    p.direccion,
    p.telefono,
    p.activo,
    p.created_at,
    p.updated_at,
    -- Campos específicos de persona física (NULL si es jurídica)
    pf.ci,
    pf.nombre,
    pf.apellido,
    pf.fecha_nacimiento,
    pf.genero,
    CASE
        WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
        ELSE NULL
    END as nombre_completo,
    -- Campos específicos de persona jurídica (NULL si es física)
    pj.razon_social,
    pj.nit,
    pj.encargado_nombre,
    pj.encargado_cargo,
    -- Campo unificado para mostrar el nombre/razón social
    CASE
        WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
        WHEN p.tipo = 'Jurídica' THEN pj.razon_social
        ELSE 'Sin nombre'
    END as nombre_mostrar
FROM persona p
    LEFT JOIN persona_fisica pf ON p.id = pf.id
    AND p.tipo = 'Física'
    LEFT JOIN persona_juridica pj ON p.id = pj.id
    AND p.tipo = 'Jurídica';
GO -- Vista para consultas de animales con propietario mejorada
    CREATE VIEW VW_AnimalesConPropietario AS
SELECT a.id,
    a.nombre as animal_nombre,
    a.especie,
    a.raza,
    a.fecha_nacimiento,
    a.peso,
    a.color,
    a.genero,
    a.esterilizado,
    a.microchip,
    a.activo,
    a.created_at,
    a.updated_at,
    -- Información del propietario
    p.tipo as tipo_propietario,
    CASE
        WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
        WHEN p.tipo = 'Jurídica' THEN pj.razon_social
        ELSE 'Sin propietario'
    END as propietario,
    -- Contacto del propietario
    p.telefono as telefono_propietario,
    p.email as email_propietario,
    p.direccion as direccion_propietario,
    -- Campos específicos según tipo de propietario
    pf.ci as ci_propietario,
    pj.nit as nit_propietario
FROM animal a
    INNER JOIN persona p ON a.persona_id = p.id
    LEFT JOIN persona_fisica pf ON p.id = pf.id
    AND p.tipo = 'Física'
    LEFT JOIN persona_juridica pj ON p.id = pj.id
    AND p.tipo = 'Jurídica'
WHERE a.activo = 1;
GO -- Vista de personal completo (veterinarios y auxiliares)
    CREATE VIEW VW_PersonalCompleto AS
SELECT p.id,
    p.nombre,
    p.apellido,
    CONCAT(p.nombre, ' ', p.apellido) AS nombre_completo,
    p.email,
    p.usuario,
    p.telefono,
    p.direccion,
    p.fecha_contratacion,
    p.salario,
    p.activo,
    p.created_at,
    p.updated_at,
    -- Determinar tipo de personal
    CASE
        WHEN pv.id IS NOT NULL THEN 'Veterinario'
        WHEN pa.id IS NOT NULL THEN 'Auxiliar'
        ELSE 'Sin especialidad'
    END as tipo_personal,
    -- Campos específicos de veterinario (NULL si es auxiliar)
    pv.num_licencia,
    pv.especialidad,
    pv.universidad,
    pv.anios_experiencia,
    -- Campos específicos de auxiliar (NULL si es veterinario)
    pa.area,
    pa.turno,
    pa.nivel
FROM personal p
    LEFT JOIN personal_veterinario pv ON p.id = pv.id
    LEFT JOIN personal_auxiliar pa ON p.id = pa.id
WHERE p.activo = 1;
GO -- ============================================
    -- PROCEDIMIENTOS ALMACENADOS CONSECUTIVOS (CRUD)
    -- ============================================
    -- SP01: Crear o actualizar Persona Física
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
OR @id = 0 BEGIN -- CREAR nueva persona física
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
ELSE BEGIN -- ACTUALIZAR persona física existente
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
GO -- SP02: Crear o actualizar Persona Jurídica
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
OR @id = 0 BEGIN -- CREAR nueva persona jurídica
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
ELSE BEGIN -- ACTUALIZAR persona jurídica existente
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
GO -- SP03: Crear o actualizar Veterinario
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
OR @id = 0 BEGIN -- CREAR nuevo veterinario
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
ELSE BEGIN -- ACTUALIZAR veterinario existente
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
GO -- SP04: Crear o actualizar Personal Auxiliar
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
OR @id = 0 BEGIN -- CREAR nuevo auxiliar
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
ELSE BEGIN -- ACTUALIZAR auxiliar existente
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
GO -- SP05: Crear o actualizar Animal
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
BEGIN TRY -- Verificar que la persona existe
IF NOT EXISTS (
    SELECT 1
    FROM persona
    WHERE id = @persona_id
) BEGIN RAISERROR('La persona especificada no existe', 16, 1);
RETURN;
END IF @id IS NULL
OR @id = 0 BEGIN -- CREAR nuevo animal
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
-- Crear historial clínico automáticamente
INSERT INTO historico (animal_id, notas_generales)
VALUES (
        @animal_id,
        'Historial clínico creado automáticamente'
    );
SELECT @animal_id as id,
    'Animal registrado exitosamente con historial clínico' as mensaje;
END
ELSE BEGIN -- ACTUALIZAR animal existente
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
GO -- SP06: Crear o actualizar Categoría
    CREATE PROCEDURE SP06_CreateOrUpdateCategoria @id INT = NULL,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255) = NULL,
    @activo BIT = 1 AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
BEGIN TRY IF @id IS NULL
OR @id = 0 BEGIN -- CREAR nueva categoría
INSERT INTO categoria (nombre, descripcion, activo)
VALUES (@nombre, @descripcion, @activo);
SELECT SCOPE_IDENTITY() as id,
    'Categoría creada exitosamente' as mensaje;
END
ELSE BEGIN -- ACTUALIZAR categoría existente
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
GO -- SP07: Crear o actualizar Producto
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
BEGIN TRY -- Verificar que la categoría existe
IF NOT EXISTS (
    SELECT 1
    FROM categoria
    WHERE id = @categoria_id
) BEGIN RAISERROR('La categoría especificada no existe', 16, 1);
RETURN;
END IF @id IS NULL
OR @id = 0 BEGIN -- CREAR nuevo producto
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
ELSE BEGIN -- ACTUALIZAR producto existente
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
GO -- SP08: Crear o actualizar Diagnóstico
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
OR @id = 0 BEGIN -- CREAR nuevo diagnóstico
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
ELSE BEGIN -- ACTUALIZAR diagnóstico existente
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
GO -- PROCEDIMIENTO UNIFICADO: crear/actualizar factura + agregar productos/servicios + calcular totales
    CREATE
    OR ALTER PROCEDURE SP_SaveFactura @factura_id INT = NULL,
    @persona_id INT = NULL,
    @numero_factura VARCHAR(50) = NULL,
    @fecha_vencimiento DATE = NULL,
    @notas NVARCHAR(MAX) = NULL,
    @productos NVARCHAR(MAX) = NULL,
    -- JSON: [{"id":1,"cantidad":2,"precio":50.00,"descuento":0,"lote":"L001","fecha_vencimiento":"2025-12-01"}]
    @servicios NVARCHAR(MAX) = NULL,
    -- JSON: [{"id":1,"cantidad":1,"precio":100.00,"descuento":0,"veterinario_id":1,"detalle_historico_id":null}]
    @impuestos DECIMAL(10, 2) = 0,
    @descuentos DECIMAL(10, 2) = 0,
    @estado VARCHAR(20) = 'Pendiente',
    -- 'Pendiente','Pagada','Cancelada','Anulada'
    @finalizar BIT = 0 -- si =1 podemos marcar estado 'Pagada' automáticamente
    AS BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION;
DECLARE @new_factura_id INT;
DECLARE @subtotal DECIMAL(18, 2) = 0;
DECLARE @total DECIMAL(18, 2) = 0;
BEGIN TRY -- Validaciones básicas
IF @persona_id IS NULL
OR NOT EXISTS (
    SELECT 1
    FROM persona
    WHERE id = @persona_id
) BEGIN RAISERROR('Persona especificada no existe.', 16, 1);
ROLLBACK TRANSACTION;
RETURN;
END -- Crear o actualizar factura (cabecera)
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
ELSE BEGIN -- Verificar factura existe
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
END -----------------------------
-- PROCESAR PRODUCTOS (JSON)
-----------------------------
IF @productos IS NOT NULL
AND LTRIM(RTRIM(@productos)) <> '' BEGIN -- 1) Si estamos actualizando, restaurar stock de productos previos y borrar detalles previos de productos
IF @factura_id IS NOT NULL
AND @factura_id <> 0 BEGIN
UPDATE p
SET p.stock_actual = p.stock_actual + dp.cantidad
FROM producto p
    JOIN detalle_productos dp ON p.id = dp.producto_id
WHERE dp.factura_id = @new_factura_id;
DELETE FROM detalle_productos
WHERE factura_id = @new_factura_id;
END -- 2) Parsear JSON a tabla temporal
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
-- 3) Verificar cantidades requeridas por producto (sumado) contra stock
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
END -- 4) Insertar detalles de producto (tomando precio si no es provisto)
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
-- 5) Actualizar stock (restar sum(cantidad) por producto)
UPDATE p
SET p.stock_actual = p.stock_actual - s.req
FROM producto p
    JOIN (
        SELECT producto_id,
            SUM(cantidad) AS req
        FROM @prod_tab
        GROUP BY producto_id
    ) s ON p.id = s.producto_id;
END -----------------------------
-- PROCESAR SERVICIOS (JSON)
-----------------------------
IF @servicios IS NOT NULL
AND LTRIM(RTRIM(@servicios)) <> '' BEGIN -- Si actualizando, borrar detalles previos de servicios (no hay stock que restaurar)
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
-- Insertar detalles de servicios (si precio nulo usar precio_base del diagnostico)
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
END -----------------------------
-- CALCULAR SUBTOTAL, TOTAL y ACTUALIZAR FACTURA
-----------------------------
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
-- Si el usuario pidió finalizar y @finalizar=1 se marca como 'Pagada' (puedes cambiar lógica si lo deseas)
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

-- ============================================
-- PROCEDIMIENTOS ALMACENADOS PARA REPORTES DE VENTAS
-- Módulo de Reportes - Análisis de ventas por períodos
-- ============================================

-- SP_ReporteVentasPorRango: Reporte de ventas por rango de fechas específico
CREATE OR ALTER PROCEDURE SP_ReporteVentasPorRango
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @estado VARCHAR(20) = NULL -- NULL = todos, 'Pagada', 'Pendiente', etc.
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validar fechas
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
        -- Calcular días transcurridos
        DATEDIFF(DAY, f.fecha_emision, GETDATE()) AS dias_transcurridos,
        -- Información adicional
        (SELECT COUNT(*) FROM detalle_productos dp WHERE dp.factura_id = f.id) AS total_productos,
        (SELECT COUNT(*) FROM detalle_servicios ds WHERE ds.factura_id = f.id) AS total_servicios
    FROM factura f
    INNER JOIN VW_PersonasCompletas p ON f.persona_id = p.id
    WHERE f.fecha_emision BETWEEN @fecha_inicio AND @fecha_fin
        AND (@estado IS NULL OR f.estado = @estado)
    ORDER BY f.fecha_emision DESC, f.id DESC;
END;
GO

-- SP_ReporteVentasResumen: Resumen general de ventas por período
CREATE OR ALTER PROCEDURE SP_ReporteVentasResumen
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @agrupacion VARCHAR(10) = 'DIA' -- 'DIA', 'SEMANA', 'MES', 'AÑO'
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validar parámetros
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
    
    -- Resumen según agrupación
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

-- SP_ReporteVentasDetalle: Detalle completo de ventas (productos y servicios)
CREATE OR ALTER PROCEDURE SP_ReporteVentasDetalle
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @factura_id INT = NULL -- NULL = todas las facturas del rango
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Detalle de productos vendidos
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
    
    -- Detalle de servicios prestados
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

-- SP_ReporteVentasPeriodosPredefinidos: Reportes para períodos predefinidos comunes
CREATE OR ALTER PROCEDURE SP_ReporteVentasPeriodosPredefinidos
    @periodo VARCHAR(20) -- 'HOY', 'AYER', 'ULTIMOS_7_DIAS', 'MES_ACTUAL', 'ULTIMOS_30_DIAS', 'AÑO_ACTUAL'
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @fecha_inicio DATE;
    DECLARE @fecha_fin DATE;
    
    -- Calcular fechas según período
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
    
    -- Ejecutar reporte con las fechas calculadas
    EXEC SP_ReporteVentasPorRango @fecha_inicio, @fecha_fin;
END;
GO

-- SP_ReporteVentasTopClientes: Top clientes por ventas en un período
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

-- SP_ReporteVentasProductosTop: Productos más vendidos en un período
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

-- SP_ReporteVentasServiciosTop: Servicios más prestados en un período
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

-- SP_ReporteVentasEstadisticasGenerales: Estadísticas generales del sistema de ventas
CREATE OR ALTER PROCEDURE SP_ReporteVentasEstadisticasGenerales
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Estadísticas generales
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
    
    -- Estadísticas del mes actual
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
    
    -- Estadísticas del año actual
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

-- ============================================
-- DATOS INICIALES
-- ============================================

-- Insertar usuario administrador por defecto
INSERT INTO personal (
    nombre, 
    apellido, 
    email, 
    usuario, 
    contrasena, 
    telefono, 
    rol, 
    activo,
    creado_por
) VALUES (
    'Administrador',
    'del Sistema',
    'admin@veterinaria.com',
    'admin',
    'admin123',
    '0000000000',
    'Administrador',
    1,
    'Sistema'
);
GO

PRINT 'Base de datos Sistema_Veterinario creada exitosamente con usuario administrador por defecto';
PRINT 'Usuario: admin';
PRINT 'Contrasena: admin123';
PRINT '';
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