-- --------------------------------------------------------
-- Banco Camba - Script de base de datos
-- --------------------------------------------------------

-- Crear base de datos
CREATE DATABASE IF NOT EXISTS banco_camba;
USE banco_camba;

-- --------------------------------------------------------
-- Tabla: Oficina
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Oficina (
  idOficina INT NOT NULL AUTO_INCREMENT,
  central BOOLEAN NOT NULL DEFAULT FALSE,
  nombre VARCHAR(100) NOT NULL,
  direccion VARCHAR(200) NOT NULL,
  telefono VARCHAR(20),
  PRIMARY KEY (idOficina)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: Persona
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Persona (
  idPersona INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(50) NOT NULL,
  apellidoPaterno VARCHAR(50) NOT NULL,
  apellidoMaterno VARCHAR(50),
  direccion VARCHAR(200),
  telefono VARCHAR(20),
  email VARCHAR(100),
  fechaNacimiento DATE,
  ci VARCHAR(20) NOT NULL,
  idOficina INT NOT NULL,
  PRIMARY KEY (idPersona),
  FOREIGN KEY (idOficina) REFERENCES Oficina(idOficina)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: Usuario
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Usuario (
  idUsuario INT NOT NULL AUTO_INCREMENT,
  ultimoInicioSesion TIMESTAMP NULL,
  intentosFallido INT NOT NULL DEFAULT 0,
  username VARCHAR(50) NOT NULL,
  password VARCHAR(255) NOT NULL,
  idPersona INT NOT NULL,
  PRIMARY KEY (idUsuario),
  UNIQUE KEY username (username),
  FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: Cuenta
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Cuenta (
  idCuenta INT NOT NULL AUTO_INCREMENT,
  tipoCuenta TINYINT NOT NULL COMMENT '1: Cuenta de ahorro, 2: Cuenta corriente',
  tipoMoneda TINYINT NOT NULL COMMENT '1: Bolivianos, 2: Dólares',
  fechaApertura DATE NOT NULL,
  estado TINYINT NOT NULL COMMENT '1: Activa, 2: Inactiva',
  nroCuenta VARCHAR(20) NOT NULL,
  saldo DECIMAL(15,2) NOT NULL DEFAULT 0.00,
  idPersona INT NOT NULL,
  PRIMARY KEY (idCuenta),
  UNIQUE KEY nroCuenta (nroCuenta),
  FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: Tarjeta
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Tarjeta (
  idTarjeta INT NOT NULL AUTO_INCREMENT,
  estado TINYINT NOT NULL COMMENT '1: Activa, 2: Inactiva',
  nroTarjeta VARCHAR(16) NOT NULL,
  cvv VARCHAR(3) NOT NULL,
  fechaExpiracion VARCHAR(5) NOT NULL,
  pin VARCHAR(255) NOT NULL,
  idCuenta INT NOT NULL,
  PRIMARY KEY (idTarjeta),
  UNIQUE KEY nroTarjeta (nroTarjeta),
  FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: Transaccion
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Transaccion (
  idTransaccion INT NOT NULL AUTO_INCREMENT,
  tipoTransaccion TINYINT NOT NULL COMMENT '1: Retiro, 2: Depósito',
  fecha DATE NOT NULL,
  hora TIME NOT NULL,
  descripcion VARCHAR(200),
  monto DECIMAL(15,2) NOT NULL,
  idCuenta INT NOT NULL,
  PRIMARY KEY (idTransaccion),
  FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: ATM
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS ATM (
  idATM INT NOT NULL AUTO_INCREMENT,
  ubicacion VARCHAR(200) NOT NULL,
  PRIMARY KEY (idATM)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Tabla: TransaccionATM
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS TransaccionATM (
  idTransaccion INT NOT NULL,
  idATM INT NOT NULL,
  PRIMARY KEY (idTransaccion),
  FOREIGN KEY (idTransaccion) REFERENCES Transaccion(idTransaccion),
  FOREIGN KEY (idATM) REFERENCES ATM(idATM)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- Datos iniciales
-- --------------------------------------------------------

-- Oficina central
INSERT INTO Oficina (central, nombre, direccion, telefono) VALUES
(TRUE, 'Casa Matriz', 'Av. Irala #123, Santa Cruz de la Sierra', '3-3456789');

-- Agencias
INSERT INTO Oficina (central, nombre, direccion, telefono) VALUES
(FALSE, 'Agencia Norte', 'Av. Banzer Km 5, Santa Cruz de la Sierra', '3-3456790'),
(FALSE, 'Agencia Sur', 'Av. Santos Dumont #500, Santa Cruz de la Sierra', '3-3456791'),
(FALSE, 'Agencia Este', 'Av. Virgen de Cotoca #300, Santa Cruz de la Sierra', '3-3456792');

-- Administrador
INSERT INTO Persona (nombre, apellidoPaterno, apellidoMaterno, direccion, telefono, email, fechaNacimiento, ci, idOficina) VALUES
('Admin', 'Sistema', 'Banco', 'Oficina Central', '70012345', 'admin@bancocamba.com', '1990-01-01', '1234567', 1);

-- Usuario administrador (Contraseña: admin123)
INSERT INTO Usuario (username, password, idPersona) VALUES
('admin', '$2y$10$HMjGTQT0VjG8aI/5E4fdTeKBIxXB2Sw0p7iVYPUX/tEk.McgJ64mK', 1);

-- Cajeros automáticos
INSERT INTO ATM (ubicacion) VALUES
('Mall Ventura, Segundo Anillo'),
('Supermercado Hipermaxi, Tercer Anillo'),
('Terminal Bimodal, Primer Anillo'),
('Aeropuerto Viru Viru');