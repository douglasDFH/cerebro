BEGIN TRANSACTION;

CREATE TABLE persona (
    id INT IDENTITY(1, 1) NOT NULL,
    tipo VARCHAR(20) NOT NULL,
    email VARCHAR(255) NULL,
    direccion VARCHAR(255) NULL,
    telefono VARCHAR(20) NULL,
    activo BIT NOT NULL DEFAULT 1,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_persona PRIMARY KEY (id),
    CONSTRAINT CK_persona_tipo CHECK (tipo IN ('Física', 'Jurídica')),
    CONSTRAINT CK_persona_email CHECK (
        email LIKE '%@%'
        OR email IS NULL
    )
);

CREATE TABLE persona_fisica (
    id INT NOT NULL,
    ci VARCHAR(15) NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    fecha_nacimiento DATE NULL,
    genero CHAR(1) NULL,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_persona_fisica PRIMARY KEY (id),
    CONSTRAINT FK_persona_fisica_persona FOREIGN KEY (id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT CK_persona_fisica_genero CHECK (
        genero IN ('M', 'F')
        OR genero IS NULL
    ),
    CONSTRAINT UK_persona_fisica_ci UNIQUE (ci)
);

CREATE TABLE persona_juridica (
    id INT NOT NULL,
    razon_social VARCHAR(255) NOT NULL,
    nit VARCHAR(20) NULL,
    encargado_nombre VARCHAR(255) NULL,
    encargado_cargo VARCHAR(100) NULL,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_persona_juridica PRIMARY KEY (id),
    CONSTRAINT FK_persona_juridica_persona FOREIGN KEY (id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT UK_persona_juridica_nit UNIQUE (nit)
);

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
    CONSTRAINT PK_personal PRIMARY KEY (id),
    CONSTRAINT UK_personal_usuario UNIQUE (usuario),
    CONSTRAINT UK_personal_email UNIQUE (email),
    CONSTRAINT CK_personal_salario CHECK (
        salario >= 0
        OR salario IS NULL
    ),
    CONSTRAINT CK_personal_email CHECK (email LIKE '%@%')
);

CREATE TABLE personal_veterinario (
    id INT NOT NULL,
    num_licencia VARCHAR(50) NOT NULL,
    especialidad VARCHAR(100) NULL,
    universidad VARCHAR(200) NULL,
    anios_experiencia INT NULL DEFAULT 0,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_personal_veterinario PRIMARY KEY (id),
    CONSTRAINT FK_personal_veterinario_personal FOREIGN KEY (id) REFERENCES personal(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT UK_personal_veterinario_licencia UNIQUE (num_licencia),
    CONSTRAINT CK_veterinario_experiencia CHECK (anios_experiencia >= 0)
);

CREATE TABLE personal_auxiliar (
    id INT NOT NULL,
    area VARCHAR(100) NULL,
    turno VARCHAR(10) NOT NULL,
    nivel VARCHAR(20) NULL DEFAULT 'Básico',
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_personal_auxiliar PRIMARY KEY (id),
    CONSTRAINT FK_personal_auxiliar_personal FOREIGN KEY (id) REFERENCES personal(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT CK_personal_auxiliar_turno CHECK (turno IN ('Mañana', 'Tarde', 'Noche')),
    CONSTRAINT CK_auxiliar_nivel CHECK (nivel IN ('Básico', 'Intermedio', 'Avanzado'))
);

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
    CONSTRAINT PK_animal PRIMARY KEY (id),
    CONSTRAINT FK_animal_persona FOREIGN KEY (persona_id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT CK_animal_genero CHECK (
        genero IN ('M', 'F')
        OR genero IS NULL
    ),
    CONSTRAINT CK_animal_peso CHECK (
        peso > 0
        OR peso IS NULL
    )
);

CREATE TABLE historico (
    id INT IDENTITY(1, 1) NOT NULL,
    animal_id INT NOT NULL,
    notas_generales TEXT NULL,
    alergias TEXT NULL,
    condiciones_medicas TEXT NULL,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_historico PRIMARY KEY (id),
    CONSTRAINT FK_historico_animal FOREIGN KEY (animal_id) REFERENCES animal(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT UK_historico_animal UNIQUE (animal_id)
);

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
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_detalle_historico PRIMARY KEY (id),
    CONSTRAINT FK_detalle_historico_historico FOREIGN KEY (historico_id) REFERENCES historico(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_detalle_historico_veterinario FOREIGN KEY (veterinario_id) REFERENCES personal_veterinario(id) ON DELETE
    SET NULL ON UPDATE CASCADE,
    CONSTRAINT CK_detalle_historico_tipo CHECK (
        tipo_evento IN (
            'Diagnostico',
            'Tratamiento',
            'Control',
            'Vacunacion',
            'Cirugia',
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

CREATE TABLE categoria (
    id INT IDENTITY(1, 1) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255) NULL,
    activo BIT NOT NULL DEFAULT 1,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_categoria PRIMARY KEY (id),
    CONSTRAINT UK_categoria_nombre UNIQUE (nombre)
);

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
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_producto PRIMARY KEY (id),
    CONSTRAINT FK_producto_categoria FOREIGN KEY (categoria_id) REFERENCES categoria(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT CK_producto_precio CHECK (precio >= 0),
    CONSTRAINT CK_producto_stock CHECK (
        stock_actual >= 0
        AND stock_minimo >= 0
    ),
    CONSTRAINT UK_producto_codigo UNIQUE (codigo)
);

CREATE TABLE diagnostico (
    id INT IDENTITY(1, 1) NOT NULL,
    codigo VARCHAR(20) NULL,
    nombre VARCHAR(200) NOT NULL,
    descripcion TEXT NULL,
    precio_base DECIMAL(10, 2) NOT NULL DEFAULT 0,
    categoria_diagnostico VARCHAR(100) NULL,
    requiere_equipamiento BIT NOT NULL DEFAULT 0,
    activo BIT NOT NULL DEFAULT 1,
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_diagnostico PRIMARY KEY (id),
    CONSTRAINT CK_diagnostico_precio CHECK (precio_base >= 0),
    CONSTRAINT UK_diagnostico_codigo UNIQUE (codigo)
);

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
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_factura PRIMARY KEY (id),
    CONSTRAINT FK_factura_persona FOREIGN KEY (persona_id) REFERENCES persona(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT UK_factura_numero UNIQUE (numero_factura),
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
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_detalle_productos PRIMARY KEY (id),
    CONSTRAINT FK_detalle_productos_factura FOREIGN KEY (factura_id) REFERENCES factura(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_detalle_productos_producto FOREIGN KEY (producto_id) REFERENCES producto(id) ON DELETE NO ACTION ON UPDATE CASCADE,
    CONSTRAINT CK_detalle_productos_cantidad CHECK (cantidad > 0),
    CONSTRAINT CK_detalle_productos_precios CHECK (
        precio_unitario >= 0
        AND descuento_unitario >= 0
        AND subtotal >= 0
    )
);

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
    created_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    updated_at DATETIME2(3) NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_detalle_servicios PRIMARY KEY (id),
    CONSTRAINT FK_detalle_servicios_factura FOREIGN KEY (factura_id) REFERENCES factura(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_detalle_servicios_diagnostico FOREIGN KEY (diagnostico_id) REFERENCES diagnostico(id) ON DELETE NO ACTION ON UPDATE CASCADE,
    CONSTRAINT FK_detalle_servicios_historico FOREIGN KEY (detalle_historico_id) REFERENCES detalle_historico(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_detalle_servicios_veterinario FOREIGN KEY (veterinario_id) REFERENCES personal_veterinario(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT CK_detalle_servicios_cantidad CHECK (cantidad > 0),
    CONSTRAINT CK_detalle_servicios_precios CHECK (
        precio_unitario >= 0
        AND descuento_unitario >= 0
        AND subtotal >= 0
    )
);

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

PRINT 'Base de datos creada exitosamente Solamente Tablas';

COMMIT TRANSACTION;