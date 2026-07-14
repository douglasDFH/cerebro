-- ==========================================
-- SCRIPT COMPLETO PARA CLÍNICA VETERINARIA
-- Basado en el diagrama de clases veterinario
-- Fecha: 2025-08-13
-- ==========================================

USE master;
GO

-- Eliminar la base de datos si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaVeterinario')
BEGIN
    ALTER DATABASE SistemaVeterinario SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SistemaVeterinario;
END
GO

-- Crear nueva base de datos
CREATE DATABASE SistemaVeterinario;
GO

USE SistemaVeterinario;
GO

-- ==========================================
-- TABLA PERSONA (CLASE BASE)
-- ==========================================
CREATE TABLE Persona (
    IdPersona INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(20),
    TipoPersona NVARCHAR(10) NOT NULL CHECK (TipoPersona IN ('Fisica', 'Juridica')),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);

-- ==========================================
-- TABLA PERSONA FÍSICA (HERENCIA)
-- ==========================================
CREATE TABLE PersonaFisica (
    IdPersonaFisica INT PRIMARY KEY,
    DNI NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE,
    FOREIGN KEY (IdPersonaFisica) REFERENCES Persona(IdPersona)
);

-- ==========================================
-- TABLA PERSONA JURÍDICA (HERENCIA)
-- ==========================================
CREATE TABLE PersonaJuridica (
    IdPersonaJuridica INT PRIMARY KEY,
    CIF NVARCHAR(20) NOT NULL UNIQUE,
    RazonSocial NVARCHAR(200) NOT NULL,
    FechaConstitucion DATE,
    FOREIGN KEY (IdPersonaJuridica) REFERENCES Persona(IdPersona)
);

-- ==========================================
-- TABLA ANIMAL (MASCOTAS)
-- ==========================================
CREATE TABLE Animal (
    IdAnimal INT IDENTITY(1,1) PRIMARY KEY,
    IdPropietario INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Edad INT,
    Raza NVARCHAR(100),
    Peso DECIMAL(5,2),
    Color NVARCHAR(50),
    Observaciones NVARCHAR(500),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (IdPropietario) REFERENCES Persona(IdPersona)
);

-- ==========================================
-- TABLA PERSONAL (EMPLEADOS)
-- ==========================================
CREATE TABLE Personal (
    IdPersonal INT IDENTITY(1,1) PRIMARY KEY,
    IdPersona INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(200) NOT NULL,
    FechaContratacion DATE NOT NULL,
    TipoPersonal NVARCHAR(20) NOT NULL CHECK (TipoPersonal IN ('Veterinario', 'Auxiliar')),
    NumeroLicencia NVARCHAR(50), -- Para veterinarios
    Especialidad NVARCHAR(100), -- Para veterinarios
    Salario DECIMAL(10,2),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (IdPersona) REFERENCES Persona(IdPersona)
);

-- ==========================================
-- TABLA ELEMENTO HISTÓRICO
-- ==========================================
CREATE TABLE ElementoHistorico (
    IdElementoHistorico INT IDENTITY(1,1) PRIMARY KEY,
    TipoElemento NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- ==========================================
-- TABLA HISTÓRICO
-- ==========================================
CREATE TABLE Historico (
    IdHistorico INT IDENTITY(1,1) PRIMARY KEY,
    IdAnimal INT NOT NULL,
    IdElementoHistorico INT NOT NULL,
    RefHistorico NVARCHAR(100) NOT NULL UNIQUE,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Observaciones NVARCHAR(500),
    FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    FOREIGN KEY (IdElementoHistorico) REFERENCES ElementoHistorico(IdElementoHistorico)
);

-- ==========================================
-- TABLA DIAGNÓSTICO
-- ==========================================
CREATE TABLE Diagnostico (
    IdDiagnostico INT IDENTITY(1,1) PRIMARY KEY,
    IdAnimal INT NOT NULL,
    IdVeterinario INT NOT NULL,
    IdHistorico INT,
    Fecha DATE NOT NULL,
    Descripcion NVARCHAR(1000) NOT NULL,
    Tratamiento NVARCHAR(1000),
    Observaciones NVARCHAR(500),
    Estado NVARCHAR(20) DEFAULT 'Activo' CHECK (Estado IN ('Activo', 'Completado', 'Cancelado')),
    FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    FOREIGN KEY (IdVeterinario) REFERENCES Personal(IdPersonal),
    FOREIGN KEY (IdHistorico) REFERENCES Historico(IdHistorico)
);

-- ==========================================
-- TABLA FACTURA
-- ==========================================
CREATE TABLE Factura (
    IdFactura INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL,
    IdPersonal INT NOT NULL,
    RefFactura NVARCHAR(20) NOT NULL UNIQUE,
    Fecha DATE NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    Impuestos DECIMAL(10,2) DEFAULT 0,
    Total DECIMAL(10,2) NOT NULL,
    Estado NVARCHAR(20) DEFAULT 'Pendiente' CHECK (Estado IN ('Pendiente', 'Pagada', 'Cancelada')),
    FormaPago NVARCHAR(50),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdCliente) REFERENCES Persona(IdPersona),
    FOREIGN KEY (IdPersonal) REFERENCES Personal(IdPersonal)
);

-- ==========================================
-- TABLA ELEMENTO FACTURA
-- ==========================================
CREATE TABLE ElementoFactura (
    IdElementoFactura INT IDENTITY(1,1) PRIMARY KEY,
    IdFactura INT NOT NULL,
    Elemento NVARCHAR(200) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Cantidad INT NOT NULL DEFAULT 1,
    Subtotal DECIMAL(10,2) NOT NULL,
    TipoElemento NVARCHAR(50) DEFAULT 'Servicio' CHECK (TipoElemento IN ('Servicio', 'Producto', 'Medicamento')),
    FOREIGN KEY (IdFactura) REFERENCES Factura(IdFactura)
);

-- ==========================================
-- TABLA PRODUCTOS (INVENTARIO)
-- ==========================================
CREATE TABLE Productos (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0,
    StockMinimo INT DEFAULT 0,
    Categoria NVARCHAR(100),
    Proveedor NVARCHAR(200),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);

-- ==========================================
-- TABLA CITAS
-- ==========================================
CREATE TABLE Citas (
    IdCita INT IDENTITY(1,1) PRIMARY KEY,
    IdAnimal INT NOT NULL,
    IdVeterinario INT NOT NULL,
    FechaCita DATETIME NOT NULL,
    TipoCita NVARCHAR(100) NOT NULL,
    Motivo NVARCHAR(500),
    Estado NVARCHAR(20) DEFAULT 'Programada' CHECK (Estado IN ('Programada', 'Completada', 'Cancelada', 'NoAsistio')),
    Observaciones NVARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    FOREIGN KEY (IdVeterinario) REFERENCES Personal(IdPersonal)
);

-- ==========================================
-- TABLA USUARIOS DEL SISTEMA
-- ==========================================
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    IdPersonal INT,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(20) NOT NULL CHECK (Rol IN ('Admin', 'Veterinario', 'Auxiliar', 'Recepcionista')),
    UltimoAcceso DATETIME,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdPersonal) REFERENCES Personal(IdPersonal)
);

-- ==========================================
-- INSERTAR DATOS DE PRUEBA
-- ==========================================

-- Personas Físicas (Clientes)
INSERT INTO Persona (Email, Direccion, Telefono, TipoPersona) VALUES
('ana.martinez@email.com', 'Calle 10 #123', '555-1001', 'Fisica'),
('carlos.lopez@email.com', 'Avenida 20 #456', '555-1002', 'Fisica'),
('sofia.rodriguez@email.com', 'Carrera 30 #789', '555-1003', 'Fisica'),
('pedro.jimenez@email.com', 'Calle 40 #321', '555-1004', 'Fisica');

INSERT INTO PersonaFisica (IdPersonaFisica, DNI, Nombre, Apellido, FechaNacimiento) VALUES
(1, '12345678A', 'Ana', 'Martínez', '1985-03-15'),
(2, '23456789B', 'Carlos', 'López', '1978-07-22'),
(3, '34567890C', 'Sofía', 'Rodríguez', '1992-11-08'),
(4, '45678901D', 'Pedro', 'Jiménez', '1980-05-30');

-- Personas para el personal
INSERT INTO Persona (Email, Direccion, Telefono, TipoPersona) VALUES
('dr.garcia@veterinaria.com', 'Calle Veterinaria 1', '555-2001', 'Fisica'),
('dra.fernandez@veterinaria.com', 'Calle Veterinaria 2', '555-2002', 'Fisica'),
('aux.martinez@veterinaria.com', 'Calle Auxiliar 1', '555-2003', 'Fisica');

INSERT INTO PersonaFisica (IdPersonaFisica, DNI, Nombre, Apellido, FechaNacimiento) VALUES
(5, '56789012E', 'Juan', 'García', '1975-02-14'),
(6, '67890123F', 'María', 'Fernández', '1982-09-25'),
(7, '78901234G', 'Luis', 'Martínez', '1990-12-03');

-- Personal
INSERT INTO Personal (IdPersona, Nombre, Apellidos, FechaContratacion, TipoPersonal, NumeroLicencia, Especialidad, Salario) VALUES
(5, 'Juan', 'García', '2020-01-15', 'Veterinario', 'VET001', 'Medicina General', 3500.00),
(6, 'María', 'Fernández', '2021-03-10', 'Veterinario', 'VET002', 'Cirugía', 4000.00),
(7, 'Luis', 'Martínez', '2022-06-20', 'Auxiliar', NULL, NULL, 1800.00);

-- Usuarios del sistema
INSERT INTO Usuarios (IdPersonal, Usuario, Contrasena, Rol) VALUES
(1, 'admin', '123456', 'Admin'),
(1, 'dr.garcia', '123456', 'Veterinario'),
(2, 'dra.fernandez', '123456', 'Veterinario'),
(3, 'aux.martinez', '123456', 'Auxiliar');

-- Animales
INSERT INTO Animal (IdPropietario, Tipo, Nombre, Edad, Raza, Peso, Color, Observaciones) VALUES
(1, 'Perro', 'Max', 3, 'Labrador', 25.5, 'Dorado', 'Muy activo y juguetón'),
(1, 'Gato', 'Luna', 2, 'Persa', 4.2, 'Blanco', 'Tranquila, le gusta dormir'),
(2, 'Perro', 'Rocky', 5, 'Pastor Alemán', 30.0, 'Negro', 'Perro guardián, muy leal'),
(3, 'Gato', 'Mimi', 1, 'Siamés', 3.8, 'Crema', 'Joven y curiosa'),
(4, 'Perro', 'Buddy', 4, 'Golden Retriever', 28.2, 'Dorado', 'Excelente con niños');

-- Productos
INSERT INTO Productos (Codigo, Nombre, Descripcion, Precio, Stock, StockMinimo, Categoria, Proveedor) VALUES
('ALIM001', 'Alimento para Perros Premium', 'Alimento balanceado para perros adultos', 25.50, 100, 10, 'Alimentos', 'Proveedor A'),
('ALIM002', 'Alimento para Gatos', 'Alimento balanceado para gatos', 22.00, 75, 15, 'Alimentos', 'Proveedor A'),
('MED001', 'Vitaminas para Mascotas', 'Suplemento vitamínico', 15.75, 30, 5, 'Medicamentos', 'Proveedor B'),
('MED002', 'Antiparasitario', 'Tratamiento antiparasitario', 18.50, 25, 3, 'Medicamentos', 'Proveedor B'),
('SER001', 'Consulta Veterinaria', 'Consulta médica general', 45.00, 999, 0, 'Servicios', 'Interno'),
('SER002', 'Cirugía Menor', 'Procedimiento quirúrgico menor', 150.00, 999, 0, 'Servicios', 'Interno'),
('SER003', 'Vacunación', 'Aplicación de vacunas', 25.00, 999, 0, 'Servicios', 'Interno');
GO

-- ==========================================
-- VISTAS PARA FACILITAR CONSULTAS
-- ==========================================

-- Vista de clientes completa
CREATE VIEW vw_Clientes AS
SELECT 
    p.IdPersona,
    pf.DNI,
    pf.Nombre,
    pf.Apellido,
    p.Email,
    p.Telefono,
    p.Direccion,
    (SELECT COUNT(*) FROM Animal WHERE IdPropietario = p.IdPersona AND Activo = 1) as TotalMascotas
FROM Persona p
INNER JOIN PersonaFisica pf ON p.IdPersona = pf.IdPersonaFisica
WHERE p.TipoPersona = 'Fisica' AND p.Activo = 1;
GO

-- Vista de personal completa
CREATE VIEW vw_Personal AS
SELECT 
    pe.IdPersonal,
    pe.Nombre,
    pe.Apellidos,
    pe.TipoPersonal,
    pe.NumeroLicencia,
    pe.Especialidad,
    pe.FechaContratacion,
    p.Email,
    p.Telefono,
    p.Direccion
FROM Personal pe
INNER JOIN Persona p ON pe.IdPersona = p.IdPersona
WHERE pe.Activo = 1;
GO

-- Vista de animales con propietario
CREATE VIEW vw_Animales AS
SELECT 
    a.IdAnimal,
    a.Nombre as NombreMascota,
    a.Tipo,
    a.Raza,
    a.Edad,
    a.Peso,
    a.Color,
    pf.Nombre + ' ' + pf.Apellido as NombrePropietario,
    p.Telefono,
    p.Email,
    a.FechaRegistro
FROM Animal a
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
INNER JOIN PersonaFisica pf ON p.IdPersona = pf.IdPersonaFisica
WHERE a.Activo = 1;
GO

-- Vista de citas programadas
CREATE VIEW vw_CitasProgramadas AS
SELECT 
    c.IdCita,
    a.Nombre as NombreMascota,
    a.Tipo as TipoAnimal,
    pf.Nombre + ' ' + pf.Apellido as NombrePropietario,
    p.Telefono,
    pe.Nombre + ' ' + pe.Apellidos as NombreVeterinario,
    c.FechaCita,
    c.TipoCita,
    c.Motivo,
    c.Estado
FROM Citas c
INNER JOIN Animal a ON c.IdAnimal = a.IdAnimal
INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
INNER JOIN PersonaFisica pf ON p.IdPersona = pf.IdPersonaFisica
INNER JOIN Personal pe ON c.IdVeterinario = pe.IdPersonal
WHERE c.Estado IN ('Programada');
GO

-- ==========================================
-- PROCEDIMIENTOS ALMACENADOS
-- ==========================================

-- Procedimiento para login
CREATE PROCEDURE sp_Login
    @usuario NVARCHAR(50),
    @contrasena NVARCHAR(255)
AS
BEGIN
    SELECT 
        u.IdUsuario,
        u.Usuario,
        u.Rol,
        pe.Nombre + ' ' + pe.Apellidos as NombreCompleto,
        pe.TipoPersonal
    FROM Usuarios u
    LEFT JOIN Personal pe ON u.IdPersonal = pe.IdPersonal
    WHERE u.Usuario = @usuario 
      AND u.Contrasena = @contrasena 
      AND u.Activo = 1;
END;
GO

-- Procedimiento para registrar cita
CREATE PROCEDURE sp_RegistrarCita
    @IdAnimal INT,
    @IdVeterinario INT,
    @FechaCita DATETIME,
    @TipoCita NVARCHAR(100),
    @Motivo NVARCHAR(500)
AS
BEGIN
    INSERT INTO Citas (IdAnimal, IdVeterinario, FechaCita, TipoCita, Motivo)
    VALUES (@IdAnimal, @IdVeterinario, @FechaCita, @TipoCita, @Motivo);
    
    SELECT SCOPE_IDENTITY() as IdCita;
END;
GO

-- Procedimiento para crear factura
CREATE PROCEDURE sp_CrearFactura
    @IdCliente INT,
    @IdPersonal INT,
    @RefFactura NVARCHAR(20)
AS
BEGIN
    INSERT INTO Factura (IdCliente, IdPersonal, RefFactura, Fecha, Subtotal, Total)
    VALUES (@IdCliente, @IdPersonal, @RefFactura, GETDATE(), 0, 0);
    
    SELECT SCOPE_IDENTITY() as IdFactura;
END;
GO

-- ==========================================
-- ÍNDICES PARA MEJORAR RENDIMIENTO
-- ==========================================

CREATE NONCLUSTERED INDEX IX_Animal_Propietario ON Animal (IdPropietario);
CREATE NONCLUSTERED INDEX IX_Diagnostico_Animal ON Diagnostico (IdAnimal);
CREATE NONCLUSTERED INDEX IX_Diagnostico_Fecha ON Diagnostico (Fecha);
CREATE NONCLUSTERED INDEX IX_Citas_Fecha ON Citas (FechaCita);
CREATE NONCLUSTERED INDEX IX_Citas_Veterinario ON Citas (IdVeterinario);
CREATE NONCLUSTERED INDEX IX_Factura_Cliente ON Factura (IdCliente);
CREATE NONCLUSTERED INDEX IX_Factura_Fecha ON Factura (Fecha);
CREATE NONCLUSTERED INDEX IX_PersonaFisica_DNI ON PersonaFisica (DNI);
CREATE NONCLUSTERED INDEX IX_PersonaJuridica_CIF ON PersonaJuridica (CIF);

PRINT 'Base de datos de Clínica Veterinaria creada exitosamente';
PRINT 'Usuario de prueba: admin / 123456';
GO