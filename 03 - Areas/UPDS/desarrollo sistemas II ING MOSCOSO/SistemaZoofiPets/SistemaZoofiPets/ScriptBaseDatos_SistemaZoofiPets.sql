-- ==========================================
-- SCRIPT COMPLETO PARA SISTEMA ZOOFI PETS
-- Autor: Sistema Generado
-- Fecha: 2025-08-12
-- Descripción: Script completo para crear la base de datos del Sistema Veterinario ZoofiPets
-- ==========================================

-- Crear la base de datos
USE master;
GO

-- Eliminar la base de datos si existe (opcional - descomenta si necesitas recrear)
/*
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaVeterinario')
BEGIN
    ALTER DATABASE SistemaVeterinario SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SistemaVeterinario;
END
GO
*/

-- Crear nueva base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaVeterinario')
BEGIN
    CREATE DATABASE SistemaVeterinario;
END
GO

-- Usar la base de datos
USE SistemaVeterinario;
GO

-- ==========================================
-- TABLA PERSONA (USUARIOS DEL SISTEMA)
-- ==========================================

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persona' AND xtype='U')
BEGIN
    CREATE TABLE Persona (
        id INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(100) NOT NULL,
        apellido NVARCHAR(100) NOT NULL,
        email NVARCHAR(150) NOT NULL UNIQUE,
        usuario NVARCHAR(50) NOT NULL UNIQUE,
        contrasena NVARCHAR(255) NOT NULL,
        telefono NVARCHAR(20),
        direccion NVARCHAR(255),
        fecha_creacion DATETIME DEFAULT GETDATE(),
        activo BIT DEFAULT 1
    );
    PRINT 'Tabla Persona creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'Tabla Persona ya existe.';
END
GO

-- ==========================================
-- TABLA PRODUCTOS/INVENTARIO
-- ==========================================

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Productos' AND xtype='U')
BEGIN
    CREATE TABLE Productos (
        id INT IDENTITY(1,1) PRIMARY KEY,
        codigo NVARCHAR(20) NOT NULL UNIQUE,
        nombre NVARCHAR(200) NOT NULL,
        descripcion NVARCHAR(500),
        precio DECIMAL(10,2) NOT NULL,
        stock INT NOT NULL DEFAULT 0,
        stock_minimo INT DEFAULT 0,
        categoria NVARCHAR(100),
        proveedor NVARCHAR(200),
        fecha_creacion DATETIME DEFAULT GETDATE(),
        activo BIT DEFAULT 1
    );
    PRINT 'Tabla Productos creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'Tabla Productos ya existe.';
END
GO

-- ==========================================
-- TABLA CLIENTES
-- ==========================================

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clientes' AND xtype='U')
BEGIN
    CREATE TABLE Clientes (
        id INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(100) NOT NULL,
        apellido NVARCHAR(100) NOT NULL,
        telefono NVARCHAR(20),
        email NVARCHAR(150),
        direccion NVARCHAR(255),
        fecha_registro DATETIME DEFAULT GETDATE(),
        activo BIT DEFAULT 1
    );
    PRINT 'Tabla Clientes creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'Tabla Clientes ya existe.';
END
GO

-- ==========================================
-- TABLA MASCOTAS
-- ==========================================

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Mascotas' AND xtype='U')
BEGIN
    CREATE TABLE Mascotas (
        id INT IDENTITY(1,1) PRIMARY KEY,
        cliente_id INT,
        nombre NVARCHAR(100) NOT NULL,
        especie NVARCHAR(50) NOT NULL,
        raza NVARCHAR(100),
        edad INT,
        peso DECIMAL(5,2),
        color NVARCHAR(50),
        observaciones NVARCHAR(500),
        fecha_registro DATETIME DEFAULT GETDATE(),
        activo BIT DEFAULT 1,
        CONSTRAINT FK_Mascotas_Clientes FOREIGN KEY (cliente_id) REFERENCES Clientes(id)
    );
    PRINT 'Tabla Mascotas creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'Tabla Mascotas ya existe.';
END
GO

-- ==========================================
-- TABLA VENTAS
-- ==========================================

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Ventas' AND xtype='U')
BEGIN
    CREATE TABLE Ventas (
        id INT IDENTITY(1,1) PRIMARY KEY,
        cliente_id INT,
        usuario_id INT,
        fecha_venta DATETIME DEFAULT GETDATE(),
        subtotal DECIMAL(10,2) NOT NULL,
        impuesto DECIMAL(10,2) DEFAULT 0,
        total DECIMAL(10,2) NOT NULL,
        estado NVARCHAR(20) DEFAULT 'Completada',
        CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (cliente_id) REFERENCES Clientes(id),
        CONSTRAINT FK_Ventas_Persona FOREIGN KEY (usuario_id) REFERENCES Persona(id)
    );
    PRINT 'Tabla Ventas creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'Tabla Ventas ya existe.';
END
GO

-- ==========================================
-- TABLA DETALLE DE VENTAS
-- ==========================================

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DetalleVenta' AND xtype='U')
BEGIN
    CREATE TABLE DetalleVenta (
        id INT IDENTITY(1,1) PRIMARY KEY,
        venta_id INT,
        producto_id INT,
        cantidad INT NOT NULL,
        precio_unitario DECIMAL(10,2) NOT NULL,
        subtotal DECIMAL(10,2) NOT NULL,
        CONSTRAINT FK_DetalleVenta_Ventas FOREIGN KEY (venta_id) REFERENCES Ventas(id),
        CONSTRAINT FK_DetalleVenta_Productos FOREIGN KEY (producto_id) REFERENCES Productos(id)
    );
    PRINT 'Tabla DetalleVenta creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'Tabla DetalleVenta ya existe.';
END
GO

-- ==========================================
-- DATOS DE PRUEBA - USUARIOS
-- ==========================================

-- Verificar si ya existen usuarios
IF NOT EXISTS (SELECT * FROM Persona WHERE usuario = 'admin')
BEGIN
    INSERT INTO Persona (nombre, apellido, email, usuario, contrasena, telefono, direccion)
    VALUES 
        ('Admin', 'Sistema', 'admin@zoofipets.com', 'admin', '123456', '555-0001', 'Oficina Principal'),
        ('Juan', 'Pérez', 'juan@email.com', 'jperez', '123456', '555-0002', 'Calle 123'),
        ('María', 'González', 'maria@email.com', 'mgonzalez', '123456', '555-0003', 'Avenida 456'),
        ('Carlos', 'Veterinario', 'carlos@zoofipets.com', 'cveterinario', '123456', '555-0004', 'Clínica Principal');
    
    PRINT 'Usuarios de prueba insertados exitosamente.';
END
ELSE
BEGIN
    PRINT 'Usuarios de prueba ya existen.';
END
GO

-- ==========================================
-- DATOS DE PRUEBA - PRODUCTOS
-- ==========================================

IF NOT EXISTS (SELECT * FROM Productos WHERE codigo = 'ALIM001')
BEGIN
    INSERT INTO Productos (codigo, nombre, descripcion, precio, stock, stock_minimo, categoria, proveedor)
    VALUES 
        ('ALIM001', 'Alimento para Perros Premium', 'Alimento balanceado para perros adultos', 25.50, 100, 10, 'Alimentos', 'Proveedor A'),
        ('ALIM002', 'Alimento para Gatos', 'Alimento balanceado para gatos', 22.00, 75, 15, 'Alimentos', 'Proveedor A'),
        ('ALIM003', 'Alimento para Cachorros', 'Alimento especial para cachorros', 28.00, 50, 8, 'Alimentos', 'Proveedor A'),
        ('JUG001', 'Pelota de Goma', 'Juguete para mascotas pequeñas', 8.50, 50, 5, 'Juguetes', 'Proveedor B'),
        ('JUG002', 'Cuerda para Jugar', 'Cuerda resistente para perros', 12.75, 30, 5, 'Juguetes', 'Proveedor B'),
        ('JUG003', 'Ratón de Juguete', 'Juguete para gatos', 6.25, 40, 8, 'Juguetes', 'Proveedor B'),
        ('MED001', 'Vitaminas para Mascotas', 'Suplemento vitamínico', 15.75, 30, 5, 'Medicamentos', 'Proveedor C'),
        ('MED002', 'Antiparasitario', 'Tratamiento antiparasitario', 18.50, 25, 3, 'Medicamentos', 'Proveedor C'),
        ('ACC001', 'Collar para Perro', 'Collar ajustable', 14.00, 35, 5, 'Accesorios', 'Proveedor D'),
        ('ACC002', 'Correa Retráctil', 'Correa de 5 metros', 22.50, 20, 3, 'Accesorios', 'Proveedor D');
    
    PRINT 'Productos de prueba insertados exitosamente.';
END
ELSE
BEGIN
    PRINT 'Productos de prueba ya existen.';
END
GO

-- ==========================================
-- DATOS DE PRUEBA - CLIENTES
-- ==========================================

IF NOT EXISTS (SELECT * FROM Clientes WHERE email = 'ana@email.com')
BEGIN
    INSERT INTO Clientes (nombre, apellido, telefono, email, direccion)
    VALUES 
        ('Ana', 'Martínez', '555-1001', 'ana@email.com', 'Calle 10 #123'),
        ('Carlos', 'López', '555-1002', 'carlos@email.com', 'Avenida 20 #456'),
        ('Sofia', 'Rodríguez', '555-1003', 'sofia@email.com', 'Carrera 30 #789'),
        ('Pedro', 'Jiménez', '555-1004', 'pedro@email.com', 'Calle 40 #321'),
        ('Laura', 'Fernández', '555-1005', 'laura@email.com', 'Avenida 50 #654');
    
    PRINT 'Clientes de prueba insertados exitosamente.';
END
ELSE
BEGIN
    PRINT 'Clientes de prueba ya existen.';
END
GO

-- ==========================================
-- DATOS DE PRUEBA - MASCOTAS
-- ==========================================

IF NOT EXISTS (SELECT * FROM Mascotas WHERE nombre = 'Max')
BEGIN
    INSERT INTO Mascotas (cliente_id, nombre, especie, raza, edad, peso, color, observaciones)
    VALUES 
        (1, 'Max', 'Perro', 'Labrador', 3, 25.5, 'Dorado', 'Muy activo y juguetón'),
        (1, 'Luna', 'Gato', 'Persa', 2, 4.2, 'Blanco', 'Tranquila, le gusta dormir'),
        (2, 'Rocky', 'Perro', 'Pastor Alemán', 5, 30.0, 'Negro', 'Perro guardián, muy leal'),
        (3, 'Mimi', 'Gato', 'Siamés', 1, 3.8, 'Crema', 'Joven y curiosa'),
        (4, 'Buddy', 'Perro', 'Golden Retriever', 4, 28.2, 'Dorado', 'Excelente con niños'),
        (5, 'Whiskers', 'Gato', 'Común', 3, 4.5, 'Gris', 'Gato rescatado, muy cariñoso');
    
    PRINT 'Mascotas de prueba insertadas exitosamente.';
END
ELSE
BEGIN
    PRINT 'Mascotas de prueba ya existen.';
END
GO

-- ==========================================
-- PROCEDIMIENTOS ALMACENADOS SEGUROS
-- ==========================================

-- Procedimiento para login seguro (reemplazar el método actual)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ValidarLogin')
    DROP PROCEDURE sp_ValidarLogin;
GO

CREATE PROCEDURE sp_ValidarLogin
    @usuario NVARCHAR(50),
    @contrasena NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @resultado INT = 0;
    
    SELECT @resultado = COUNT(*)
    FROM Persona 
    WHERE usuario = @usuario 
      AND contrasena = @contrasena 
      AND activo = 1;
    
    SELECT @resultado as resultado;
END
GO

-- Procedimiento para insertar usuario de forma segura
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertarUsuario')
    DROP PROCEDURE sp_InsertarUsuario;
GO

CREATE PROCEDURE sp_InsertarUsuario
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @email NVARCHAR(150),
    @usuario NVARCHAR(50),
    @contrasena NVARCHAR(255),
    @telefono NVARCHAR(20),
    @direccion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario o email ya existen
        IF EXISTS (SELECT 1 FROM Persona WHERE usuario = @usuario OR email = @email)
        BEGIN
            SELECT 0 as resultado, 'Usuario o email ya existe' as mensaje;
            RETURN;
        END
        
        INSERT INTO Persona (nombre, apellido, email, usuario, contrasena, telefono, direccion)
        VALUES (@nombre, @apellido, @email, @usuario, @contrasena, @telefono, @direccion);
        
        SELECT 1 as resultado, 'Usuario creado exitosamente' as mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 as resultado, ERROR_MESSAGE() as mensaje;
    END CATCH
END
GO

-- Procedimiento para modificar usuario
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ModificarUsuario')
    DROP PROCEDURE sp_ModificarUsuario;
GO

CREATE PROCEDURE sp_ModificarUsuario
    @nombre NVARCHAR(100),
    @apellido NVARCHAR(100),
    @email NVARCHAR(150),
    @usuario NVARCHAR(50),
    @contrasena NVARCHAR(255),
    @telefono NVARCHAR(20),
    @direccion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Persona 
        SET nombre = @nombre,
            apellido = @apellido,
            email = @email,
            contrasena = @contrasena,
            telefono = @telefono,
            direccion = @direccion
        WHERE usuario = @usuario;
        
        SELECT @@ROWCOUNT as resultado;
    END TRY
    BEGIN CATCH
        SELECT 0 as resultado;
    END CATCH
END
GO

-- Procedimiento para eliminar usuario (eliminación lógica)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_EliminarUsuario')
    DROP PROCEDURE sp_EliminarUsuario;
GO

CREATE PROCEDURE sp_EliminarUsuario
    @usuario NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE Persona 
        SET activo = 0
        WHERE usuario = @usuario;
        
        SELECT @@ROWCOUNT as resultado;
    END TRY
    BEGIN CATCH
        SELECT 0 as resultado;
    END CATCH
END
GO

-- ==========================================
-- VISTAS PARA REPORTES
-- ==========================================

-- Vista de usuarios activos
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_UsuariosActivos')
    DROP VIEW vw_UsuariosActivos;
GO

CREATE VIEW vw_UsuariosActivos AS
SELECT 
    id,
    nombre,
    apellido,
    email,
    usuario,
    telefono,
    direccion,
    fecha_creacion
FROM Persona 
WHERE activo = 1;
GO

-- Vista de productos con stock bajo
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_ProductosStockBajo')
    DROP VIEW vw_ProductosStockBajo;
GO

CREATE VIEW vw_ProductosStockBajo AS
SELECT 
    codigo,
    nombre,
    stock,
    stock_minimo,
    categoria,
    precio,
    (stock_minimo - stock) as deficit
FROM Productos 
WHERE stock <= stock_minimo AND activo = 1;
GO

-- Vista de ventas del día
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_VentasHoy')
    DROP VIEW vw_VentasHoy;
GO

CREATE VIEW vw_VentasHoy AS
SELECT 
    v.id,
    c.nombre + ' ' + c.apellido as cliente,
    p.nombre + ' ' + p.apellido as vendedor,
    v.fecha_venta,
    v.total,
    v.estado
FROM Ventas v
INNER JOIN Clientes c ON v.cliente_id = c.id
INNER JOIN Persona p ON v.usuario_id = p.id
WHERE CAST(v.fecha_venta AS DATE) = CAST(GETDATE() AS DATE);
GO

-- ==========================================
-- ÍNDICES PARA MEJOR RENDIMIENTO
-- ==========================================

-- Índices para tabla Persona
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Persona_Usuario')
    CREATE NONCLUSTERED INDEX IX_Persona_Usuario ON Persona (usuario);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Persona_Email')
    CREATE NONCLUSTERED INDEX IX_Persona_Email ON Persona (email);

-- Índices para tabla Productos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Productos_Codigo')
    CREATE NONCLUSTERED INDEX IX_Productos_Codigo ON Productos (codigo);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Productos_Categoria')
    CREATE NONCLUSTERED INDEX IX_Productos_Categoria ON Productos (categoria);

-- Índices para tabla Ventas
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Ventas_Fecha')
    CREATE NONCLUSTERED INDEX IX_Ventas_Fecha ON Ventas (fecha_venta);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Ventas_Cliente')
    CREATE NONCLUSTERED INDEX IX_Ventas_Cliente ON Ventas (cliente_id);

GO

-- ==========================================
-- FUNCIÓN PARA REPORTES DE VENTAS
-- ==========================================

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'FN' AND name = 'fn_VentasPorPeriodo')
    DROP FUNCTION fn_VentasPorPeriodo;
GO

CREATE FUNCTION fn_VentasPorPeriodo(@fechaInicio DATE, @fechaFin DATE)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        v.id,
        v.fecha_venta,
        c.nombre + ' ' + c.apellido as cliente,
        p.nombre + ' ' + p.apellido as vendedor,
        v.subtotal,
        v.impuesto,
        v.total,
        v.estado
    FROM Ventas v
    INNER JOIN Clientes c ON v.cliente_id = c.id
    INNER JOIN Persona p ON v.usuario_id = p.id
    WHERE CAST(v.fecha_venta AS DATE) BETWEEN @fechaInicio AND @fechaFin
);
GO

-- ==========================================
-- TRIGGERS PARA AUDITORÍA
-- ==========================================

-- Trigger para actualizar stock en ventas
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_ActualizarStock')
    DROP TRIGGER trg_ActualizarStock;
GO

CREATE TRIGGER trg_ActualizarStock
ON DetalleVenta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE p
    SET stock = p.stock - i.cantidad
    FROM Productos p
    INNER JOIN inserted i ON p.id = i.producto_id;
END
GO

-- ==========================================
-- VERIFICACIÓN FINAL
-- ==========================================

PRINT '==========================================';
PRINT 'VERIFICACIÓN DE INSTALACIÓN';
PRINT '==========================================';

-- Verificar tablas creadas
SELECT 'Tabla: ' + TABLE_NAME + ' - Creada ✓' as Estado
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'SistemaVeterinario'
ORDER BY TABLE_NAME;

-- Verificar datos de prueba
SELECT 'Usuarios creados: ' + CAST(COUNT(*) AS NVARCHAR) as Resumen FROM Persona;
SELECT 'Productos creados: ' + CAST(COUNT(*) AS NVARCHAR) as Resumen FROM Productos;
SELECT 'Clientes creados: ' + CAST(COUNT(*) AS NVARCHAR) as Resumen FROM Clientes;
SELECT 'Mascotas registradas: ' + CAST(COUNT(*) AS NVARCHAR) as Resumen FROM Mascotas;

-- Verificar procedimientos
SELECT 'Procedimiento: ' + name + ' - Creado ✓' as Estado
FROM sys.procedures 
WHERE name LIKE 'sp_%'
ORDER BY name;

-- Verificar vistas
SELECT 'Vista: ' + name + ' - Creada ✓' as Estado
FROM sys.views 
WHERE name LIKE 'vw_%'
ORDER BY name;

PRINT '==========================================';
PRINT 'INSTALACIÓN COMPLETADA EXITOSAMENTE';
PRINT '==========================================';
PRINT 'Usuarios de prueba disponibles:';
PRINT '- Usuario: admin     | Contraseña: 123456';
PRINT '- Usuario: jperez    | Contraseña: 123456';
PRINT '- Usuario: mgonzalez | Contraseña: 123456';
PRINT '- Usuario: cveterinario | Contraseña: 123456';
PRINT '==========================================';

-- Mostrar string de conexión recomendado
PRINT 'String de conexión recomendado:';
PRINT 'server=DESKTOP-S0REQAM\SQLEXPRESS;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;';