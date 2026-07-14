-- =============================================
-- SISTEMA VETERINARIO - SCRIPT COMPLETO SQL SERVER
-- Autor: Claude
-- Fecha: 2025-08-17
-- Descripci�n: Base de datos completa para sistema veterinario
-- =============================================

USE master;
GO

-- Crear base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaVeterinario')
BEGIN
    CREATE DATABASE SistemaVeterinario;
END
GO

USE SistemaVeterinario;
GO

-- =============================================
-- ELIMINAR TABLAS EXISTENTES (para reiniciar)
-- =============================================
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ElementoFactura')
    DROP TABLE ElementoFactura;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Factura')
    DROP TABLE Factura;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'ElementoHistorico')
    DROP TABLE ElementoHistorico;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Historico')
    DROP TABLE Historico;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Diagnostico')
    DROP TABLE Diagnostico;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Animal')
    DROP TABLE Animal;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Persona')
    DROP TABLE Usuario;
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Personal')
    DROP TABLE Personal;
GO

-- =============================================
-- CREACI�N DE TABLAS
-- =============================================

-- Tabla Persona (Incluye personas f�sicas y jur�dicas)
CREATE TABLE Persona (
    IdPersona INT IDENTITY(1,1) PRIMARY KEY,
    TipoPersona CHAR(1) NOT NULL CHECK (TipoPersona IN ('F', 'J')), -- F=F�sica, J=Jur�dica
    DNI VARCHAR(20) NULL, -- Para personas f�sicas
    CIF VARCHAR(20) NULL, -- Para personas jur�dicas
    Nombre NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NULL, -- Solo para personas f�sicas
    RazonSocial NVARCHAR(200) NULL, -- Solo para personas jur�dicas
    Genero CHAR(1) NULL CHECK (Genero IN ('F', 'M', 'O')), -- F=Femenino, M=Masculino, O=Otro (solo personas f�sicas)
    Email NVARCHAR(100) NULL,
    Direccion NVARCHAR(300) NULL,
    Telefono VARCHAR(20) NULL,
    TelefonoAlternativo VARCHAR(20) NULL,
    Foto VARBINARY(MAX) NULL,
    FechaRegistro DATETIME2 DEFAULT GETDATE(),
    FechaModificacion DATETIME2 DEFAULT GETDATE(),
    Estado BIT DEFAULT 1, -- 1=Activo, 0=Inactivo
    Observaciones NVARCHAR(500) NULL,
    
    -- Constraints
    CONSTRAINT CK_Persona_DNI_CIF CHECK (
        (TipoPersona = 'F' AND DNI IS NOT NULL AND CIF IS NULL) OR
        (TipoPersona = 'J' AND CIF IS NOT NULL AND DNI IS NULL)
    ),
    CONSTRAINT CK_Persona_Nombres CHECK (
        (TipoPersona = 'F' AND Apellidos IS NOT NULL AND RazonSocial IS NULL) OR
        (TipoPersona = 'J' AND RazonSocial IS NOT NULL)
    )
);

-- Tabla Personal (Veterinarios y Auxiliares)
CREATE TABLE Personal (
    IdPersonal INT IDENTITY(1,1) PRIMARY KEY,
    TipoPersonal VARCHAR(20) NOT NULL CHECK (TipoPersonal IN ('VETERINARIO', 'AUXILIAR')),
    Nombre NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    DNI VARCHAR(20) NOT NULL UNIQUE,
    Email NVARCHAR(100) NULL,
    Telefono VARCHAR(20) NULL,
    Direccion NVARCHAR(300) NULL,
    NumColegiado VARCHAR(50) NULL, -- Para veterinarios
    Especialidad NVARCHAR(100) NULL,
    FechaContratacion DATE NOT NULL DEFAULT GETDATE(),
    FechaNacimiento DATE NULL,
    Salario DECIMAL(10,2) NULL,
    Foto VARBINARY(MAX) NULL,
    Estado BIT DEFAULT 1,
    FechaRegistro DATETIME2 DEFAULT GETDATE(),
    FechaModificacion DATETIME2 DEFAULT GETDATE(),
    Observaciones NVARCHAR(500) NULL
);

-- Tabla Animal
CREATE TABLE Animal (
    IdAnimal INT IDENTITY(1,1) PRIMARY KEY,
    IdPropietario INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL, -- Perro, Gato, P�jaro, etc.
    Raza NVARCHAR(100) NULL,
    Color NVARCHAR(50) NULL,
    Sexo CHAR(1) CHECK (Sexo IN ('M', 'H', 'I')), -- M=Macho, H=Hembra, I=Indefinido
    FechaNacimiento DATE NULL,
    Edad INT NULL, -- Calculado o estimado
    Peso DECIMAL(5,2) NULL,
    Altura DECIMAL(5,2) NULL,
    Microchip VARCHAR(50) NULL UNIQUE,
    NumPedigree VARCHAR(50) NULL,
    Esterilizado BIT DEFAULT 0,
    Vacunado BIT DEFAULT 0,
    Foto VARBINARY(MAX) NULL,
    FechaRegistro DATETIME2 DEFAULT GETDATE(),
    FechaModificacion DATETIME2 DEFAULT GETDATE(),
    Estado BIT DEFAULT 1,
    Observaciones NVARCHAR(500) NULL,
    
    CONSTRAINT FK_Animal_Propietario FOREIGN KEY (IdPropietario) REFERENCES Persona(IdPersona)
);

-- Tabla Diagn�stico
CREATE TABLE Diagnostico (
    IdDiagnostico INT IDENTITY(1,1) PRIMARY KEY,
    IdAnimal INT NOT NULL,
    IdVeterinario INT NOT NULL,
    Fecha DATETIME2 DEFAULT GETDATE(),
    Descripcion NVARCHAR(1000) NOT NULL,
    Sintomas NVARCHAR(500) NULL,
    Tratamiento NVARCHAR(1000) NULL,
    Medicamentos NVARCHAR(500) NULL,
    Dosis NVARCHAR(300) NULL,
    ProximaVisita DATE NULL,
    Urgencia VARCHAR(20) CHECK (Urgencia IN ('BAJA', 'MEDIA', 'ALTA', 'CRITICA')) DEFAULT 'MEDIA',
    Estado VARCHAR(20) CHECK (Estado IN ('PENDIENTE', 'EN_TRATAMIENTO', 'RESUELTO', 'DERIVADO')) DEFAULT 'PENDIENTE',
    Costo DECIMAL(10,2) NULL,
    FechaRegistro DATETIME2 DEFAULT GETDATE(),
    Observaciones NVARCHAR(500) NULL,
    
    CONSTRAINT FK_Diagnostico_Animal FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    CONSTRAINT FK_Diagnostico_Veterinario FOREIGN KEY (IdVeterinario) REFERENCES Personal(IdPersonal)
);

-- Tabla Hist�rico
CREATE TABLE Historico (
    IdHistorico INT IDENTITY(1,1) PRIMARY KEY,
    IdAnimal INT NOT NULL,
    RefHistorico VARCHAR(50) NOT NULL UNIQUE,
    FechaCreacion DATETIME2 DEFAULT GETDATE(),
    TipoHistorial VARCHAR(30) CHECK (TipoHistorial IN ('CONSULTA', 'CIRUGIA', 'VACUNACION', 'EMERGENCIA', 'REVISION')) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Estado BIT DEFAULT 1,
    
    CONSTRAINT FK_Historico_Animal FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal)
);

-- Tabla ElementoHist�rico
CREATE TABLE ElementoHistorico (
    IdElementoHistorico INT IDENTITY(1,1) PRIMARY KEY,
    IdHistorico INT NOT NULL,
    IdDiagnostico INT NULL,
    TipoElemento VARCHAR(30) NOT NULL,
    Descripcion NVARCHAR(300) NOT NULL,
    Valor NVARCHAR(100) NULL,
    Unidad VARCHAR(20) NULL,
    FechaRegistro DATETIME2 DEFAULT GETDATE(),
    Observaciones NVARCHAR(300) NULL,
    
    CONSTRAINT FK_ElementoHistorico_Historico FOREIGN KEY (IdHistorico) REFERENCES Historico(IdHistorico),
    CONSTRAINT FK_ElementoHistorico_Diagnostico FOREIGN KEY (IdDiagnostico) REFERENCES Diagnostico(IdDiagnostico)
);

-- Tabla Factura
CREATE TABLE Factura (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    RefFactura VARCHAR(50) NOT NULL UNIQUE,
    IdCliente INT NOT NULL,
    IdAnimal INT NULL,
    FechaFactura DATETIME2 DEFAULT GETDATE(),
    FechaVencimiento DATE NULL,
    Subtotal DECIMAL(12,2) DEFAULT 0,
    Impuestos DECIMAL(12,2) DEFAULT 0,
    Total DECIMAL(12,2) DEFAULT 0,
    Estado VARCHAR(20) CHECK (Estado IN ('PENDIENTE', 'PAGADA', 'VENCIDA', 'ANULADA')) DEFAULT 'PENDIENTE',
    FormaPago VARCHAR(30) CHECK (FormaPago IN ('EFECTIVO', 'TARJETA', 'TRANSFERENCIA', 'CHEQUE')) NULL,
    FechaPago DATETIME2 NULL,
    Observaciones NVARCHAR(300) NULL,
    
    CONSTRAINT FK_Factura_Cliente FOREIGN KEY (IdCliente) REFERENCES Persona(IdPersona),
    CONSTRAINT FK_Factura_Animal FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal)
);

-- Tabla ElementoFactura
CREATE TABLE ElementoFactura (
    IdElementoFactura INT IDENTITY(1,1) PRIMARY KEY,
    IdFactura INT NOT NULL,
    Elemento NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    Cantidad DECIMAL(8,2) NOT NULL DEFAULT 1,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Descuento DECIMAL(5,2) DEFAULT 0, -- Porcentaje
    Subtotal AS (Cantidad * PrecioUnitario * (1 - Descuento/100)) PERSISTED,
    TipoServicio VARCHAR(30) CHECK (TipoServicio IN ('CONSULTA', 'MEDICAMENTO', 'CIRUGIA', 'VACUNA', 'PRODUCTO', 'OTRO')),
    
    CONSTRAINT FK_ElementoFactura_Factura FOREIGN KEY (IdFactura) REFERENCES Factura(IdFactura)
);

-- =============================================
-- �NDICES PARA MEJORAR RENDIMIENTO
-- =============================================
CREATE INDEX IX_Persona_DNI ON Persona(DNI);
CREATE INDEX IX_Persona_CIF ON Persona(CIF);
CREATE INDEX IX_Persona_TipoPersona ON Persona(TipoPersona);
CREATE INDEX IX_Animal_Propietario ON Animal(IdPropietario);
CREATE INDEX IX_Animal_Microchip ON Animal(Microchip);
CREATE INDEX IX_Diagnostico_Animal ON Diagnostico(IdAnimal);
CREATE INDEX IX_Diagnostico_Fecha ON Diagnostico(Fecha);
CREATE INDEX IX_Factura_Cliente ON Factura(IdCliente);
CREATE INDEX IX_Factura_Estado ON Factura(Estado);
CREATE INDEX IX_Personal_TipoPersonal ON Personal(TipoPersonal);

-- =============================================
-- TRIGGERS
-- =============================================

-- Trigger para actualizar total de factura
CREATE TRIGGER TR_ElementoFactura_UpdateTotal
ON ElementoFactura
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Actualizar subtotal de facturas afectadas
    UPDATE f SET 
        Subtotal = (
            SELECT ISNULL(SUM(ef.Subtotal), 0) 
            FROM ElementoFactura ef 
            WHERE ef.IdFactura = f.IdFactura
        ),
        Total = (
            SELECT ISNULL(SUM(ef.Subtotal), 0) 
            FROM ElementoFactura ef 
            WHERE ef.IdFactura = f.IdFactura
        ) + f.Impuestos
    FROM Factura f
    WHERE f.IdFactura IN (
        SELECT DISTINCT IdFactura FROM inserted
        UNION
        SELECT DISTINCT IdFactura FROM deleted
    );
END;
GO

-- Trigger para actualizar fecha de modificaci�n
CREATE TRIGGER TR_UpdateModificationDate
ON Persona
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Persona 
    SET FechaModificacion = GETDATE()
    WHERE IdPersona IN (SELECT IdPersona FROM inserted);
END;
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS
-- =============================================

-- SP para insertar persona f�sica
CREATE PROCEDURE SP_InsertarPersonaFisica
    @DNI VARCHAR(20),
    @Nombre NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @Genero CHAR(1) = 'F',
    @Email NVARCHAR(100) = NULL,
    @Direccion NVARCHAR(300) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Observaciones NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Persona (TipoPersona, DNI, Nombre, Apellidos, Genero, Email, Direccion, Telefono, Observaciones)
    VALUES ('F', @DNI, @Nombre, @Apellidos, @Genero, @Email, @Direccion, @Telefono, @Observaciones);
    
    SELECT SCOPE_IDENTITY() AS IdPersona;
END;
GO

-- SP para insertar persona jur�dica
CREATE PROCEDURE SP_InsertarPersonaJuridica
    @CIF VARCHAR(20),
    @RazonSocial NVARCHAR(200),
    @Email NVARCHAR(100) = NULL,
    @Direccion NVARCHAR(300) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Observaciones NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Persona (TipoPersona, CIF, Nombre, RazonSocial, Email, Direccion, Telefono, Observaciones)
    VALUES ('J', @CIF, @RazonSocial, @RazonSocial, @Email, @Direccion, @Telefono, @Observaciones);
    
    SELECT SCOPE_IDENTITY() AS IdPersona;
END;
GO

-- SP para insertar animal
CREATE PROCEDURE SP_InsertarAnimal
    @IdPropietario INT,
    @Nombre NVARCHAR(100),
    @Tipo NVARCHAR(50),
    @Raza NVARCHAR(100) = NULL,
    @Color NVARCHAR(50) = NULL,
    @Sexo CHAR(1) = NULL,
    @FechaNacimiento DATE = NULL,
    @Peso DECIMAL(5,2) = NULL,
    @Microchip VARCHAR(50) = NULL,
    @Observaciones NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Edad INT = NULL;
    IF @FechaNacimiento IS NOT NULL
        SET @Edad = DATEDIFF(YEAR, @FechaNacimiento, GETDATE());
    
    INSERT INTO Animal (IdPropietario, Nombre, Tipo, Raza, Color, Sexo, FechaNacimiento, Edad, Peso, Microchip, Observaciones)
    VALUES (@IdPropietario, @Nombre, @Tipo, @Raza, @Color, @Sexo, @FechaNacimiento, @Edad, @Peso, @Microchip, @Observaciones);
    
    SELECT SCOPE_IDENTITY() AS IdAnimal;
END;
GO

-- SP para insertar diagn�stico
CREATE PROCEDURE SP_InsertarDiagnostico
    @IdAnimal INT,
    @IdVeterinario INT,
    @Descripcion NVARCHAR(1000),
    @Sintomas NVARCHAR(500) = NULL,
    @Tratamiento NVARCHAR(1000) = NULL,
    @Medicamentos NVARCHAR(500) = NULL,
    @Urgencia VARCHAR(20) = 'MEDIA',
    @Costo DECIMAL(10,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Diagnostico (IdAnimal, IdVeterinario, Descripcion, Sintomas, Tratamiento, Medicamentos, Urgencia, Costo)
    VALUES (@IdAnimal, @IdVeterinario, @Descripcion, @Sintomas, @Tratamiento, @Medicamentos, @Urgencia, @Costo);
    
    SELECT SCOPE_IDENTITY() AS IdDiagnostico;
END;
GO

-- SP para crear factura
CREATE PROCEDURE SP_CrearFactura
    @IdCliente INT,
    @IdAnimal INT = NULL,
    @RefFactura VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @RefFactura IS NULL
        SET @RefFactura = 'FAC-' + FORMAT(GETDATE(), 'yyyyMMdd') + '-' + FORMAT(NEXT VALUE FOR FacturaSequence, '0000');
    
    INSERT INTO Factura (RefFactura, IdCliente, IdAnimal)
    VALUES (@RefFactura, @IdCliente, @IdAnimal);
    
    SELECT SCOPE_IDENTITY() AS IdFactura, @RefFactura AS RefFactura;
END;
GO

-- Crear secuencia para facturas
CREATE SEQUENCE FacturaSequence START WITH 1 INCREMENT BY 1;
GO

-- SP para agregar elemento a factura
CREATE PROCEDURE SP_AgregarElementoFactura
    @IdFactura INT,
    @Elemento NVARCHAR(200),
    @Descripcion NVARCHAR(300) = NULL,
    @Cantidad DECIMAL(8,2),
    @PrecioUnitario DECIMAL(10,2),
    @TipoServicio VARCHAR(30) = 'OTRO',
    @Descuento DECIMAL(5,2) = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO ElementoFactura (IdFactura, Elemento, Descripcion, Cantidad, PrecioUnitario, TipoServicio, Descuento)
    VALUES (@IdFactura, @Elemento, @Descripcion, @Cantidad, @PrecioUnitario, @TipoServicio, @Descuento);
    
    SELECT SCOPE_IDENTITY() AS IdElementoFactura;
END;
GO

-- SP para buscar animales por propietario
CREATE PROCEDURE SP_BuscarAnimalesPorPropietario
    @IdPropietario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.*,
        p.Nombre + ISNULL(' ' + p.Apellidos, '') AS NombrePropietario
    FROM Animal a
    INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
    WHERE a.IdPropietario = @IdPropietario AND a.Estado = 1
    ORDER BY a.Nombre;
END;
GO

-- =============================================
-- VISTAS �TILES
-- =============================================

-- Vista resumen de animales con propietarios
CREATE VIEW VW_AnimalesConPropietarios AS
SELECT 
    a.IdAnimal,
    a.Nombre AS NombreAnimal,
    a.Tipo,
    a.Raza,
    a.Edad,
    a.Sexo,
    a.Microchip,
    p.IdPersona AS IdPropietario,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS NombrePropietario,
    p.Telefono,
    p.Email,
    a.FechaRegistro
FROM Animal a
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
WHERE a.Estado = 1 AND p.Estado = 1;
GO

-- Vista historial m�dico completo
CREATE VIEW VW_HistorialMedico AS
SELECT 
    d.IdDiagnostico,
    a.Nombre AS NombreAnimal,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS Propietario,
    per.Nombre + ' ' + per.Apellidos AS Veterinario,
    d.Fecha,
    d.Descripcion,
    d.Sintomas,
    d.Tratamiento,
    d.Estado,
    d.Urgencia,
    d.ProximaVisita
FROM Diagnostico d
INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
INNER JOIN Personal per ON d.IdVeterinario = per.IdPersonal;
GO

-- Vista facturaci�n
CREATE VIEW VW_ResumenFacturacion AS
SELECT 
    f.IdFactura,
    f.RefFactura,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS Cliente,
    ISNULL(a.Nombre, 'Sin especificar') AS Animal,
    f.FechaFactura,
    f.Total,
    f.Estado,
    f.FormaPago
FROM Factura f
INNER JOIN Persona p ON f.IdCliente = p.IdPersona
LEFT JOIN Animal a ON f.IdAnimal = a.IdAnimal;
GO

-- Vista citas pr�ximas
CREATE VIEW VW_CitasProximas AS
SELECT 
    d.IdDiagnostico,
    a.Nombre AS NombreAnimal,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS Propietario,
    p.Telefono,
    per.Nombre + ' ' + per.Apellidos AS Veterinario,
    d.ProximaVisita,
    d.Descripcion AS UltimoDiagnostico,
    DATEDIFF(DAY, GETDATE(), d.ProximaVisita) AS DiasHasta
FROM Diagnostico d
INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
INNER JOIN Personal per ON d.IdVeterinario = per.IdPersonal
WHERE d.ProximaVisita IS NOT NULL 
    AND d.ProximaVisita >= GETDATE()
    AND d.Estado IN ('PENDIENTE', 'EN_TRATAMIENTO');
GO

-- =============================================
-- DATOS DE EJEMPLO
-- =============================================

-- Insertar personal
INSERT INTO Personal (TipoPersonal, Nombre, Apellidos, DNI, Email, NumColegiado, Especialidad)
VALUES 
    ('VETERINARIO', 'Dr. Juan', 'P�rez Garc�a', '12345678A', 'juan.perez@veterinaria.com', 'VET001', 'Medicina General'),
    ('VETERINARIO', 'Dra. Mar�a', 'L�pez Ruiz', '87654321B', 'maria.lopez@veterinaria.com', 'VET002', 'Cirug�a'),
    ('AUXILIAR', 'Ana', 'Mart�nez Torres', '11111111C', 'ana.martinez@veterinaria.com', NULL, NULL);

-- Insertar personas (clientes)
EXEC SP_InsertarPersonaFisica '98765432D', 'Carlos', 'Gonz�lez S�nchez', 'M', 'carlos.gonzalez@email.com', 'Calle Mayor 123', '666777888';
EXEC SP_InsertarPersonaFisica '55555555E', 'Laura', 'Fern�ndez D�az', 'F', 'laura.fernandez@email.com', 'Avenida Central 456', '999888777';

-- Insertar animales
EXEC SP_InsertarAnimal 1, 'Rex', 'Perro', 'Pastor Alem�n', 'Marr�n', 'M', '2020-03-15', 25.5, 'CHIP001', 'Muy activo y juguet�n';
EXEC SP_InsertarAnimal 2, 'Miau', 'Gato', 'Persa', 'Blanco', 'H', '2019-07-22', 4.2, 'CHIP002', 'Tranquila, le gusta dormir';

-- Insertar diagn�sticos
EXEC SP_InsertarDiagnostico 1, 1, 'Revisi�n general anual', 'Ninguno aparente', 'Vacunaci�n al d�a', 'Vitaminas', 'BAJA', 45.00;
EXEC SP_InsertarDiagnostico 2, 2, 'Infecci�n urinaria leve', 'Dificultad para orinar', 'Antibi�tico por 7 d�as', 'Amoxicilina 250mg', 'MEDIA', 65.00;

-- Insertar usuario administrador directamente con hash SHA256
-- Hash SHA256 de "admin123" = 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
INSERT INTO Usuario (NombreUsuario, Clave, Email, Rol, Estado, IdPersonal)
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin@veterinaria.com', 'ADMIN', 1, NULL);

PRINT 'Base de datos del Sistema Veterinario creada exitosamente con datos de ejemplo.';
PRINT 'Usuario administrador creado: admin / admin123';
PRINT 'Incluye: Tablas, �ndices, Triggers, Procedimientos Almacenados, Vistas y Datos de Prueba';
GO

-- =============================================
-- TABLA DE USUARIOS
-- =============================================

-- Eliminar tabla si existe
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
    DROP TABLE Usuario;
GO

-- Crear tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Clave NVARCHAR(255) NOT NULL, -- Para almacenar hash de contraseña
    Email NVARCHAR(100) NOT NULL UNIQUE,
    IdPersonal INT NULL, -- Referencia al personal (opcional)
    Rol VARCHAR(20) NOT NULL CHECK (Rol IN ('ADMIN', 'VETERINARIO', 'AUXILIAR', 'RECEPCIONISTA')) DEFAULT 'AUXILIAR',
    Estado BIT DEFAULT 1, -- 1=Activo, 0=Inactivo
    FechaCreacion DATETIME2 DEFAULT GETDATE(),
    FechaUltimoAcceso DATETIME2 NULL,
    IntentosLogin INT DEFAULT 0,
    Bloqueado BIT DEFAULT 0,
    FechaBloqueo DATETIME2 NULL,
    Token NVARCHAR(500) NULL, -- Para sesiones
    FechaExpiracionToken DATETIME2 NULL,
    
    -- Constraint para relacionar con personal
    CONSTRAINT FK_Usuario_Personal FOREIGN KEY (IdPersonal) REFERENCES Personal(IdPersonal)
);
GO

-- Índices para mejorar rendimiento
CREATE INDEX IX_Usuario_NombreUsuario ON Usuario(NombreUsuario);
CREATE INDEX IX_Usuario_Email ON Usuario(Email);
CREATE INDEX IX_Usuario_Estado ON Usuario(Estado);
CREATE INDEX IX_Usuario_Rol ON Usuario(Rol);
GO



-- =============================================
-- PROCEDIMIENTOS ALMACENADOS PARA USUARIOS
-- =============================================

-- SP para insertar usuario
CREATE PROCEDURE SP_InsertarUsuario
    @NombreUsuario NVARCHAR(50),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(100),
    @Rol VARCHAR(20) = 'AUXILIAR',
    @IdPersonal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario ya existe
        IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @NombreUsuario OR Email = @Email)
        BEGIN
            RAISERROR('El usuario o email ya existe', 16, 1);
            RETURN;
        END
        
        INSERT INTO Usuario (NombreUsuario, Clave, Email, Rol, IdPersonal)
        VALUES (@NombreUsuario, @Clave, @Email, @Rol, @IdPersonal);
        
        SELECT SCOPE_IDENTITY() AS IdUsuario, 'Usuario creado exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS IdUsuario, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

-- SP para editar usuario
CREATE PROCEDURE SP_EditarUsuario
    @IdUsuario INT,
    @NombreUsuario NVARCHAR(50),
    @Clave NVARCHAR(255),
    @Email NVARCHAR(100),
    @Rol VARCHAR(20) = 'AUXILIAR',
    @IdPersonal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario)
        BEGIN
            RAISERROR('El usuario no existe', 16, 1);
            RETURN;
        END
        
        -- Verificar si el nombre o email ya existe en otro usuario
        IF EXISTS (SELECT 1 FROM Usuario WHERE (NombreUsuario = @NombreUsuario OR Email = @Email) AND IdUsuario != @IdUsuario)
        BEGIN
            RAISERROR('El nombre de usuario o email ya existe', 16, 1);
            RETURN;
        END
        
        UPDATE Usuario 
        SET NombreUsuario = @NombreUsuario,
            Clave = @Clave,
            Email = @Email,
            Rol = @Rol,
            IdPersonal = @IdPersonal
        WHERE IdUsuario = @IdUsuario;
        
        SELECT 1 AS Resultado, 'Usuario actualizado exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Resultado, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

-- SP para eliminar usuario (cambiar estado)
CREATE PROCEDURE SP_EliminarUsuario
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario)
        BEGIN
            RAISERROR('El usuario no existe', 16, 1);
            RETURN;
        END
        
        UPDATE Usuario 
        SET Estado = 0
        WHERE IdUsuario = @IdUsuario;
        
        SELECT 1 AS Resultado, 'Usuario eliminado exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Resultado, ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

-- SP para mostrar usuarios
CREATE PROCEDURE SP_MostrarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.IdUsuario,
        u.NombreUsuario,
        u.Email,
        u.Rol,
        u.Estado,
        u.FechaCreacion,
        u.FechaUltimoAcceso,
        u.Bloqueado,
        CASE 
            WHEN p.IdPersonal IS NOT NULL THEN p.Nombre + ' ' + p.Apellidos
            ELSE 'Sin asignar'
        END AS PersonalAsociado
    FROM Usuario u
    LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal
    ORDER BY u.FechaCreacion DESC;
END;
GO

-- SP para buscar usuario por nombre
CREATE PROCEDURE SP_BuscarUsuarioPorNombre
    @TextoBuscar NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        u.IdUsuario,
        u.NombreUsuario,
        u.Email,
        u.Rol,
        u.Estado,
        u.FechaCreacion,
        u.FechaUltimoAcceso,
        u.Bloqueado,
        CASE 
            WHEN p.IdPersonal IS NOT NULL THEN p.Nombre + ' ' + p.Apellidos
            ELSE 'Sin asignar'
        END AS PersonalAsociado
    FROM Usuario u
    LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal
    WHERE u.NombreUsuario LIKE '%' + @TextoBuscar + '%'
       OR u.Email LIKE '%' + @TextoBuscar + '%'
    ORDER BY u.NombreUsuario;
END;
GO

-- SP para login
CREATE PROCEDURE SP_Login
    @Usuario NVARCHAR(50),
    @Clave NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @IdUsuario INT;
    DECLARE @EstadoUsuario BIT;
    DECLARE @Bloqueado BIT;
    DECLARE @IntentosLogin INT;
    
    -- Buscar usuario por nombre o email
    SELECT 
        @IdUsuario = IdUsuario,
        @EstadoUsuario = Estado,
        @Bloqueado = Bloqueado,
        @IntentosLogin = IntentosLogin
    FROM Usuario 
    WHERE (NombreUsuario = @Usuario OR Email = @Usuario) 
      AND Clave = @Clave;
    
    -- Verificar credenciales
    IF @IdUsuario IS NULL
    BEGIN
        -- Usuario no encontrado o contraseña incorrecta
        -- Incrementar intentos si el usuario existe
        IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = @Usuario OR Email = @Usuario)
        BEGIN
            UPDATE Usuario 
            SET IntentosLogin = IntentosLogin + 1,
                Bloqueado = CASE WHEN IntentosLogin >= 4 THEN 1 ELSE Bloqueado END,
                FechaBloqueo = CASE WHEN IntentosLogin >= 4 THEN GETDATE() ELSE FechaBloqueo END
            WHERE NombreUsuario = @Usuario OR Email = @Usuario;
        END
        
        SELECT 0 AS Resultado, 'Credenciales incorrectas' AS Mensaje, '' AS Rol, 0 AS IdUsuario;
        RETURN;
    END
    
    -- Verificar si está activo
    IF @EstadoUsuario = 0
    BEGIN
        SELECT 0 AS Resultado, 'Usuario inactivo' AS Mensaje, '' AS Rol, 0 AS IdUsuario;
        RETURN;
    END
    
    -- Verificar si está bloqueado
    IF @Bloqueado = 1
    BEGIN
        SELECT 0 AS Resultado, 'Usuario bloqueado por múltiples intentos fallidos' AS Mensaje, '' AS Rol, 0 AS IdUsuario;
        RETURN;
    END
    
    -- Login exitoso
    UPDATE Usuario 
    SET FechaUltimoAcceso = GETDATE(),
        IntentosLogin = 0
    WHERE IdUsuario = @IdUsuario;
    
    SELECT 
        1 AS Resultado,
        'Login exitoso' AS Mensaje,
        u.Rol,
        u.IdUsuario,
        u.NombreUsuario,
        u.Email,
        CASE 
            WHEN p.IdPersonal IS NOT NULL THEN p.Nombre + ' ' + p.Apellidos
            ELSE u.NombreUsuario
        END AS NombreCompleto
    FROM Usuario u
    LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal
    WHERE u.IdUsuario = @IdUsuario;
END;
GO

-- SP para desbloquear usuario
CREATE PROCEDURE SP_DesbloquearUsuario
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Usuario 
    SET Bloqueado = 0,
        IntentosLogin = 0,
        FechaBloqueo = NULL
    WHERE IdUsuario = @IdUsuario;
    
    SELECT 1 AS Resultado, 'Usuario desbloqueado exitosamente' AS Mensaje;
END;
GO

-- SP para cambiar contraseña
CREATE PROCEDURE SP_CambiarClave
    @IdUsuario INT,
    @ClaveAnterior NVARCHAR(255),
    @ClaveNueva NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Verificar contraseña anterior
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE IdUsuario = @IdUsuario AND Clave = @ClaveAnterior)
    BEGIN
        SELECT 0 AS Resultado, 'Contraseña anterior incorrecta' AS Mensaje;
        RETURN;
    END
    
    -- Actualizar contraseña
    UPDATE Usuario 
    SET Clave = @ClaveNueva
    WHERE IdUsuario = @IdUsuario;
    
    SELECT 1 AS Resultado, 'Contraseña cambiada exitosamente' AS Mensaje;
END;
GO

-- =============================================
-- VISTA DE USUARIOS CON INFORMACIÓN COMPLETA
-- =============================================
CREATE VIEW VW_UsuariosCompleto AS
SELECT 
    u.IdUsuario,
    u.NombreUsuario,
    u.Email,
    u.Rol,
    u.Estado,
    u.FechaCreacion,
    u.FechaUltimoAcceso,
    u.Bloqueado,
    u.IntentosLogin,
    p.Nombre AS NombrePersonal,
    p.Apellidos AS ApellidosPersonal,
    p.TipoPersonal,
    p.Especialidad
FROM Usuario u
LEFT JOIN Personal p ON u.IdPersonal = p.IdPersonal;
GO

-- =============================================
-- DATOS DE EJEMPLO PARA USUARIOS
-- =============================================

-- Insertar usuarios de ejemplo con contraseñas ya encriptadas
-- Nota: Las contraseñas se encriptan automáticamente por el procedimiento SP_InsertarUsuario

-- Eliminar usuario admin si existe (para reinsertar)
IF EXISTS (SELECT 1 FROM Usuario WHERE NombreUsuario = 'admin')
BEGIN
    DELETE FROM Usuario WHERE NombreUsuario = 'admin';
END

-- Insertar usuario administrador directamente con hash SHA256
-- Hash SHA256 de "admin123" = 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
INSERT INTO Usuario (NombreUsuario, Clave, Email, Rol, Estado, IdPersonal)
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'admin@veterinaria.com', 'ADMIN', 1, NULL);

-- Los demás usuarios usando el procedimiento normal (que encripta automáticamente)
EXEC SP_InsertarUsuario 'drjuan', 'vet123', 'juan.perez@veterinaria.com', 'VETERINARIO', 1;
EXEC SP_InsertarUsuario 'drmaria', 'vet123', 'maria.lopez@veterinaria.com', 'VETERINARIO', 2;
EXEC SP_InsertarUsuario 'ana_aux', 'aux123', 'ana.martinez@veterinaria.com', 'AUXILIAR', 3;

PRINT 'Tabla de usuarios y procedimientos almacenados creados exitosamente.';
PRINT 'Usuarios de prueba creados:';
PRINT '- admin / admin123 (ADMIN)';
PRINT '- drjuan / vet123 (VETERINARIO)';
PRINT '- drmaria / vet123 (VETERINARIO)';
PRINT '- ana_aux / aux123 (AUXILIAR)';
GO


-- Clínicas Veterinarias
EXEC SP_InsertarPersonaJuridica 'A12345678', 'Clínica Veterinaria San Francisco S.L.', 'info@veterinariasf.com', 'Calle Veterinarios 15, 28001 Madrid', '915123456', 'Clínica especializada en animales domésticos con 20 años de experiencia';

EXEC SP_InsertarPersonaJuridica 'G98765432', 'Hospital Veterinario Central S.A.', 'administracion@hospitalvet.com', 'Plaza Mayor 7, 41001 Sevilla', '955678901', 'Hospital veterinario 24 horas con servicio de urgencias y cirugía';

EXEC SP_InsertarPersonaJuridica 'J55443322', 'Clínica Veterinaria Los Robles', 'contacto@vetrobles.com', 'Avenida Los Robles 234, 03001 Alicante', '965789123', 'Especialistas en medicina felina y canina';

-- Refugios y Protectoras
EXEC SP_InsertarPersonaJuridica 'B87654321', 'Refugio de Animales Esperanza', 'contacto@refugioesperanza.org', 'Avenida de la Paz 89, 08015 Barcelona', '934567890', 'Refugio sin ánimo de lucro para animales abandonados';

EXEC SP_InsertarPersonaJuridica 'H11223344', 'Fundación Protectora de Animales', 'info@fundacionprotectora.org', 'Calle Solidaridad 12, 48001 Bilbao', '944567123', 'Fundación dedicada a la protección y cuidado de animales desde 1995';

EXEC SP_InsertarPersonaJuridica 'N99887766', 'Asociación Patitas Felices', 'adopciones@patitasfelices.org', 'Calle Esperanza 88, 18001 Granada', '958123456', 'Asociación de voluntarios para el rescate y adopción de animales';

-- Tiendas de Mascotas
EXEC SP_InsertarPersonaJuridica 'F55667788', 'Tienda de Mascotas PetWorld', 'ventas@petworld.es', 'Calle Comercio 45, 46001 Valencia', '963456789', 'Tienda especializada en productos para mascotas y alimentación';

EXEC SP_InsertarPersonaJuridica 'K77889900', 'Pet Store Central S.L.', 'info@petstorecentral.com', 'Centro Comercial Norte, Local 15, 50001 Zaragoza', '976789012', 'Gran superficie especializada en productos para animales';

-- Criaderos y Granjas
EXEC SP_InsertarPersonaJuridica 'L33445566', 'Criadero Canino Elite', 'info@criaderoelite.com', 'Finca El Paraíso, Km 5, 29600 Marbella', '952345678', 'Criadero especializado en razas puras con certificado oficial';

EXEC SP_InsertarPersonaJuridica 'M22334455', 'Granja Avícola Los Pinos', 'administracion@granjalosinos.com', 'Carretera Rural Km 8, 37001 Salamanca', '923456789', 'Granja avícola con servicio veterinario especializado';

-- Empresas de Servicios Veterinarios
EXEC SP_InsertarPersonaJuridica 'P88776655', 'Servicios Veterinarios Móviles S.L.', 'emergencias@vetmovil.com', 'Polígono Industrial Norte, Nave 12, 33001 Oviedo', '985567890', 'Servicio veterinario a domicilio 24h para grandes animales';

EXEC SP_InsertarPersonaJuridica 'Q44556677', 'Laboratorio Veterinario Diagnóstico', 'laboratorio@vetdiagnostico.com', 'Calle Ciencia 25, 47001 Valladolid', '983678901', 'Laboratorio especializado en análisis clínicos veterinarios';

-- Seguros y Servicios Financieros
EXEC SP_InsertarPersonaJuridica 'R66778899', 'Seguros Veterinarios Protección', 'clientes@segurosvetproteccion.com', 'Gran Vía 100, 28013 Madrid', '917890123', 'Compañía de seguros especializada en cobertura veterinaria';

-- Zoológicos y Parques Naturales
EXEC SP_InsertarPersonaJuridica 'S11224466', 'Parque Zoológico Municipal', 'direccion@zoomunicipal.gov', 'Parque de la Ciudad s/n, 15001 A Coruña', '981234567', 'Zoológico municipal con programa de conservación de especies';

-- Universidades y Centros de Formación
EXEC SP_InsertarPersonaJuridica 'T99887755', 'Universidad de Veterinaria Campus Sur', 'secretaria@univetsur.edu', 'Campus Universitario, Edificio C, 30001 Murcia', '968345678', 'Facultad de Veterinaria con hospital clínico universitario';

-- =============================================
-- CONSULTA PARA VERIFICAR INSERCIONES
-- =============================================

-- Ver todas las personas jurídicas insertadas
SELECT 
    IdPersona,
    CIF,
    Nombre AS NombreComercial,
    RazonSocial,
    Email,
    Direccion,
    Telefono,
    Observaciones,
    FechaRegistro
FROM Persona 
WHERE TipoPersona = 'J'
ORDER BY FechaRegistro DESC;

-- Contar total de personas jurídicas
SELECT COUNT(*) as TotalPersonasJuridicas 
FROM Persona 
WHERE TipoPersona = 'J';






-- Eliminar la vista existente si existe
IF EXISTS (SELECT * FROM sys.views WHERE name = 'VW_AnimalesConPropietarios')
BEGIN
    DROP VIEW VW_AnimalesConPropietarios;
    PRINT 'Vista VW_AnimalesConPropietarios eliminada';
END

-- Crear la vista actualizada con todas las columnas necesarias
GO
CREATE VIEW VW_AnimalesConPropietarios AS
SELECT 
    a.IdAnimal,
    a.Nombre AS NombreAnimal,
    a.Tipo,
    a.Raza,
    a.Color,
    a.Sexo,
    a.FechaNacimiento,
    a.Edad,
    a.Peso,
    a.Altura,
    a.Microchip,
    a.NumPedigree,
    a.Esterilizado,
    a.Vacunado,
    a.Observaciones,
    p.IdPersona AS IdPropietario,
    CASE 
        WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
        ELSE p.RazonSocial
    END AS NombrePropietario,
    p.Telefono,
    p.Email,
    a.FechaRegistro
FROM Animal a
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
WHERE a.Estado = 1 AND p.Estado = 1;
GO

PRINT 'Vista VW_AnimalesConPropietarios actualizada exitosamente';

-- Verificar la estructura de la vista
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.VIEW_COLUMN_USAGE vcu
INNER JOIN INFORMATION_SCHEMA.COLUMNS c ON vcu.COLUMN_NAME = c.COLUMN_NAME
WHERE vcu.VIEW_NAME = 'VW_AnimalesConPropietarios'
ORDER BY ORDINAL_POSITION;

-- Consulta de prueba
SELECT TOP 3 * FROM VW_AnimalesConPropietarios;