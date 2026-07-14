-- --------------------------------------------------------
-- Script completo para la base de datos 'banco_camba'
-- --------------------------------------------------------

-- Crear la base de datos
CREATE DATABASE banco_camba;
USE banco_camba;

-- --------------------------------------------------------
-- Estructura de la base de datos 
-- --------------------------------------------------------

-- Tabla Oficina (ampliada con más campos para el formulario)
CREATE TABLE oficina (
    idOficina INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    central BOOLEAN NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    telefono VARCHAR(20),
    ciudad VARCHAR(100) NOT NULL DEFAULT 'Santa Cruz de la Sierra',
    departamento VARCHAR(100) NOT NULL DEFAULT 'Santa Cruz',
    pais VARCHAR(100) NOT NULL DEFAULT 'Bolivia',
    horarioAtencion VARCHAR(255),
    gerenteEncargado VARCHAR(100),
    fechaInauguracion DATE,
    estado ENUM('activa', 'inactiva') NOT NULL DEFAULT 'activa',
    CONSTRAINT PK_Oficina PRIMARY KEY (idOficina)
) Engine = InnoDB Charset = utf8;

-- Tabla Persona
CREATE TABLE persona (
    idPersona INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    nombre VARCHAR(100) NOT NULL,
    apellidoMaterno VARCHAR(100) NOT NULL,
    apellidoPaterno VARCHAR(100) NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(20),
    email VARCHAR(100),
    fechaNacimiento DATE,
    ci VARCHAR(20),
    idOficina INT,
    CONSTRAINT PK_Persona PRIMARY KEY (idPersona),
    CONSTRAINT FK_Persona FOREIGN KEY (idOficina) REFERENCES oficina (idOficina)
) Engine = InnoDB Charset = utf8;

-- Tabla Usuario (contiene FK a Persona)
CREATE TABLE usuario (
    idUsuario INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    ultimoInicioSesion TIMESTAMP NULL DEFAULT NULL,
    intentosFallido INT DEFAULT 0,
    username VARCHAR(50) NOT NULL,
    password BLOB NOT NULL,
    idPersona INT NOT NULL,
    CONSTRAINT PK_Usuario PRIMARY KEY (idUsuario),
    CONSTRAINT FK_Usuario FOREIGN KEY (idPersona) REFERENCES persona (idPersona)
) Engine = InnoDB Charset = utf8;

-- Tabla Cuenta (contiene FK a Persona)
CREATE TABLE cuenta (
    idCuenta INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    tipoCuenta ENUM('cuentaAhorro', 'cuentaCorriente') NOT NULL,
    tipoMoneda ENUM('bolivianos', 'dolares') NOT NULL,
    fechaApertura DATE NOT NULL,
    estado ENUM('activa', 'inactiva') NOT NULL DEFAULT 'activa',
    nroCuenta VARCHAR(20) NOT NULL,
    saldo DECIMAL(15,2) NOT NULL DEFAULT 0.0,
    idPersona INT NOT NULL,
    CONSTRAINT PK_Cuenta PRIMARY KEY (idCuenta),
    CONSTRAINT FK_Cuenta FOREIGN KEY (idPersona) REFERENCES persona (idPersona)
) Engine = InnoDB Charset = utf8;

-- Tabla Tarjeta (contiene FK a Cuenta)
CREATE TABLE tarjeta (
    idTarjeta INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    estado ENUM('activa', 'inactiva') NOT NULL DEFAULT 'activa',
    tipoTarjeta ENUM('debito', 'credito') NOT NULL,
    nroTarjeta VARCHAR(20) NOT NULL,
    cvv VARCHAR(3) NOT NULL,
    fechaExpiracion VARCHAR(7) NOT NULL,
    pin VARCHAR(4) NOT NULL,
    idCuenta INT NOT NULL,
    CONSTRAINT PK_Tarjeta PRIMARY KEY (idTarjeta),
    CONSTRAINT FK_Tarjeta FOREIGN KEY (idCuenta) REFERENCES cuenta (idCuenta)
) Engine = InnoDB Charset = utf8;

-- Tabla ATM (con campos necesarios para el registro)
CREATE TABLE atm (
    idATM INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    codigo VARCHAR(20) NOT NULL,
    marca VARCHAR(100),
    modelo VARCHAR(100),
    ubicacion VARCHAR(255) NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    ciudad VARCHAR(100) NOT NULL DEFAULT 'Santa Cruz de la Sierra',
    departamento VARCHAR(100) NOT NULL DEFAULT 'Santa Cruz',
    tipoATM ENUM('interno', 'externo') NOT NULL DEFAULT 'interno',
    estado ENUM('activo', 'inactivo', 'mantenimiento') NOT NULL DEFAULT 'activo',
    idOficina INT,
    fechaInstalacion DATE,
    CONSTRAINT PK_Atm PRIMARY KEY (idATM),
    CONSTRAINT FK_ATM_Oficina FOREIGN KEY (idOficina) REFERENCES oficina (idOficina)
) Engine = InnoDB Charset = utf8;

-- Tabla Transaccion (mantiene los campos originales)
CREATE TABLE transaccion (
    idTransaccion INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    tipoTransaccion ENUM('retiro', 'deposito') NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    descripcion VARCHAR(255),
    monto DECIMAL(15,2) NOT NULL,
    idCuenta INT NOT NULL,
    cuentaOrigen varchar(50) DEFAULT NULL,
    cuentaDestino varchar(50) DEFAULT NULL,
    saldoResultante decimal(15,2) DEFAULT NULL,
    CONSTRAINT PK_Transaccion PRIMARY KEY (idTransaccion),
    CONSTRAINT FK_Transaccion FOREIGN KEY (idCuenta) REFERENCES cuenta (idCuenta)
) Engine = InnoDB Charset = utf8;

-- Tabla TransaccionATM (similar a transaccion pero con campo para ATM)
CREATE TABLE transaccion_atm (
    idTransaccionATM INT not null AUTO_INCREMENT,
    hash varchar(50) COMMENT 'hash',
    tipoTransaccion ENUM('retiro', 'deposito', 'consulta') NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    descripcion VARCHAR(255),
    monto DECIMAL(15,2) NOT NULL,
    idCuenta INT NOT NULL,
    cuentaOrigen varchar(50) DEFAULT NULL,
    cuentaDestino varchar(50) DEFAULT NULL,
    saldoResultante decimal(15,2) DEFAULT NULL,
    idATM INT NOT NULL,
    idTarjeta INT NOT NULL,
    CONSTRAINT PK_TransaccionATM PRIMARY KEY (idTransaccionATM),
    CONSTRAINT FK_TransaccionATM_Cuenta FOREIGN KEY (idCuenta) REFERENCES cuenta (idCuenta),
    CONSTRAINT FK_TransaccionATM_ATM FOREIGN KEY (idATM) REFERENCES atm (idATM),
    CONSTRAINT FK_TransaccionATM_Tarjeta FOREIGN KEY (idTarjeta) REFERENCES tarjeta (idTarjeta)
) Engine = InnoDB Charset = utf8;

-- --------------------------------------------------------
-- Datos iniciales
-- --------------------------------------------------------

-- Oficina central
INSERT INTO Oficina (central, nombre, direccion, telefono, ciudad, departamento, horarioAtencion, fechaInauguracion) VALUES
(TRUE, 'Casa Matriz', 'Av. Irala #123', '3-3456789', 'Santa Cruz de la Sierra', 'Santa Cruz', 'Lunes a Viernes: 8:30 - 16:30, Sábados: 8:30 - 12:30', '2000-01-01');

-- Agencias
INSERT INTO Oficina (central, nombre, direccion, telefono, horarioAtencion) VALUES
(FALSE, 'Agencia Norte', 'Av. Banzer Km 5, Santa Cruz de la Sierra', '3-3456790', 'Lunes a Viernes: 8:30 - 16:30'),
(FALSE, 'Agencia Sur', 'Av. Santos Dumont #500, Santa Cruz de la Sierra', '3-3456791', 'Lunes a Viernes: 8:30 - 16:30'),
(FALSE, 'Agencia Este', 'Av. Virgen de Cotoca #300, Santa Cruz de la Sierra', '3-3456792', 'Lunes a Viernes: 8:30 - 16:30');

-- Administrador
INSERT INTO Persona (nombre, apellidoPaterno, apellidoMaterno, direccion, telefono, email, fechaNacimiento, ci, idOficina) VALUES
('Admin', 'Sistema', 'Banco', 'Oficina Central', '70012345', 'admin@bancocamba.com', '1990-01-01', '1234567', 1);

-- Usuario administrador (Contraseña: admin123)
INSERT INTO Usuario (username, password, idPersona) VALUES
('admin', '$2y$10$HMjGTQT0VjG8aI/5E4fdTeKBIxXB2Sw0p7iVYPUX/tEk.McgJ64mK', 1);

-- Ejemplos de cajeros automáticos
INSERT INTO ATM (codigo, marca, modelo, ubicacion, direccion, tipoATM, idOficina, fechaInstalacion) VALUES
('ATM001', 'NCR', 'SelfServ 84', 'Centro Comercial Ventura Mall', 'Av. San Martín esquina 4to anillo, Piso 1', 'interno', 1, '2020-01-15'),
('ATM002', 'Diebold', 'CS 678', 'Supermercado Hipermaxi Sur', 'Av. Santos Dumont entre 2do y 3er anillo', 'interno', 3, '2021-03-10'),
('ATM003', 'Wincor Nixdorf', 'ProCash 280', 'Cañoto Mall Norte', 'Av. Cañoto y 3er anillo interno', 'externo', 2, '2022-05-20');