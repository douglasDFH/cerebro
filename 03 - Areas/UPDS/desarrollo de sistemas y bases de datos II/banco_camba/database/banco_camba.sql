-- Crear base de datos
CREATE DATABASE banco_camba;
GO
USE banco_camba;
GO

-- --------------------------------------------------------
-- Tabla: Oficina
-- --------------------------------------------------------
CREATE TABLE Oficina (
  idOficina INT IDENTITY(1,1) PRIMARY KEY,
  central BIT NOT NULL DEFAULT 0,
  nombre NVARCHAR(100) NOT NULL,
  direccion NVARCHAR(200) NOT NULL,
  telefono NVARCHAR(20) NULL
);
GO

-- --------------------------------------------------------
-- Tabla: Persona
-- --------------------------------------------------------
CREATE TABLE Persona (
  idPersona INT IDENTITY(1,1) PRIMARY KEY,
  nombre NVARCHAR(50) NOT NULL,
  apellidoPaterno NVARCHAR(50) NOT NULL,
  apellidoMaterno NVARCHAR(50) NULL,
  direccion NVARCHAR(200) NULL,
  telefono NVARCHAR(20) NULL,
  email NVARCHAR(100) NULL,
  fechaNacimiento DATE NULL,
  ci NVARCHAR(20) NOT NULL,
  idOficina INT NOT NULL,
  FOREIGN KEY (idOficina) REFERENCES Oficina(idOficina)
);
GO

-- --------------------------------------------------------
-- Tabla: Usuario
-- --------------------------------------------------------
CREATE TABLE Usuario (
  idUsuario INT IDENTITY(1,1) PRIMARY KEY,
  ultimoInicioSesion DATETIME NULL,
  intentosFallido INT NOT NULL DEFAULT 0,
  username NVARCHAR(50) NOT NULL UNIQUE,
  password NVARCHAR(255) NOT NULL,
  idPersona INT NOT NULL,
  FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
);
GO

-- --------------------------------------------------------
-- Tabla: Cuenta
-- --------------------------------------------------------
CREATE TABLE Cuenta (
  idCuenta INT IDENTITY(1,1) PRIMARY KEY,
  tipoCuenta SMALLINT NOT NULL, -- 1: Ahorro, 2: Corriente
  tipoMoneda SMALLINT NOT NULL, -- 1: Bolivianos, 2: Dólares
  fechaApertura DATE NOT NULL,
  estado SMALLINT NOT NULL, -- 1: Activa, 2: Inactiva
  nroCuenta NVARCHAR(20) NOT NULL UNIQUE,
  saldo DECIMAL(15,2) NOT NULL DEFAULT 0.00,
  idPersona INT NOT NULL,
  FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
);
GO

-- --------------------------------------------------------
-- Tabla: Tarjeta
-- --------------------------------------------------------
CREATE TABLE Tarjeta (
  idTarjeta INT IDENTITY(1,1) PRIMARY KEY,
  estado SMALLINT NOT NULL, -- 1: Activa, 2: Inactiva
  nroTarjeta NVARCHAR(16) NOT NULL UNIQUE,
  cvv NVARCHAR(3) NOT NULL,
  fechaExpiracion NVARCHAR(5) NOT NULL,
  pin NVARCHAR(255) NOT NULL,
  idCuenta INT NOT NULL,
  FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);
GO

-- --------------------------------------------------------
-- Tabla: Transaccion
-- --------------------------------------------------------
CREATE TABLE Transaccion (
  idTransaccion INT IDENTITY(1,1) PRIMARY KEY,
  tipoTransaccion SMALLINT NOT NULL, -- 1: Retiro, 2: Depósito
  fecha DATE NOT NULL,
  hora TIME NOT NULL,
  descripcion NVARCHAR(200) NULL,
  monto DECIMAL(15,2) NOT NULL,
  idCuenta INT NOT NULL,
  FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);
GO

-- --------------------------------------------------------
-- Tabla: ATM
-- --------------------------------------------------------
CREATE TABLE ATM (
  idATM INT IDENTITY(1,1) PRIMARY KEY,
  ubicacion NVARCHAR(200) NOT NULL
);
GO

-- --------------------------------------------------------
-- Tabla: TransaccionATM
-- --------------------------------------------------------
CREATE TABLE TransaccionATM (
  idTransaccion INT NOT NULL,
  idATM INT NOT NULL,
  PRIMARY KEY (idTransaccion, idATM),
  FOREIGN KEY (idTransaccion) REFERENCES Transaccion(idTransaccion),
  FOREIGN KEY (idATM) REFERENCES ATM(idATM)
);
GO

-- --------------------------------------------------------
-- Datos iniciales
-- --------------------------------------------------------

-- Oficina central
INSERT INTO Oficina (central, nombre, direccion, telefono) VALUES
(1, 'Casa Matriz', 'Av. Irala #123, Santa Cruz de la Sierra', '3-3456789');

-- Agencias
INSERT INTO Oficina (central, nombre, direccion, telefono) VALUES
(0, 'Agencia Norte', 'Av. Banzer Km 5, Santa Cruz de la Sierra', '3-3456790'),
(0, 'Agencia Sur', 'Av. Santos Dumont #500, Santa Cruz de la Sierra', '3-3456791'),
(0, 'Agencia Este', 'Av. Virgen de Cotoca #300, Santa Cruz de la Sierra', '3-3456792');
GO

-- Administrador
INSERT INTO Persona (nombre, apellidoPaterno, apellidoMaterno, direccion, telefono, email, fechaNacimiento, ci, idOficina) VALUES
('Admin', 'Sistema', 'Banco', 'Oficina Central', '70012345', 'admin@bancocamba.com', '1990-01-01', '1234567', 1);
GO

-- Usuario administrador (Contraseña encriptada con SHA2_256)
INSERT INTO Usuario (username, password, idPersona) VALUES
('admin', CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', 'admin123')), 1);
GO

-- Cajeros automáticos
INSERT INTO ATM (ubicacion) VALUES
('Mall Ventura, Segundo Anillo'),
('Supermercado Hipermaxi, Tercer Anillo'),
('Terminal Bimodal, Primer Anillo'),
('Aeropuerto Viru Viru');
GO
