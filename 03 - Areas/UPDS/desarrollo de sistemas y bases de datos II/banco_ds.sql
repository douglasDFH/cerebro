CREATE DATABASE banco_DS;
USE banco_DS;

-- Tabla Oficina
CREATE TABLE Oficina (
    idOficina INT AUTO_INCREMENT PRIMARY KEY,
    central BOOLEAN NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    telefono VARCHAR(20)
);

-- Tabla Persona
CREATE TABLE Persona (
    idPersona INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellidoMaterno VARCHAR(100) NOT NULL,
    apellidoPaterno VARCHAR(100) NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(20),
    email VARCHAR(100),
    fechaNacimiento DATE,
    ci VARCHAR(20),
    idOficina INT,
    FOREIGN KEY (idOficina) REFERENCES Oficina(idOficina)
);

-- Tabla Usuario (contiene FK a Persona)
CREATE TABLE Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    ultimoInicioSesion TIMESTAMP NULL DEFAULT NULL,
    intentosFallido INT DEFAULT 0,
    username VARCHAR(50) NOT NULL,
    password BLOB NOT NULL,
    idPersona INT NOT NULL,
    FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
);

-- Tabla Cuenta (contiene FK a Persona)
CREATE TABLE Cuenta (
    idCuenta INT AUTO_INCREMENT PRIMARY KEY,
    tipoCuenta ENUM('cuentaAhorro', 'cuentaCorriente') NOT NULL,
    tipoMoneda ENUM('bolivianos', 'dolares') NOT NULL,
    fechaApertura DATE NOT NULL,
    estado ENUM('activa', 'inactiva') NOT NULL DEFAULT 'activa',
    nroCuenta VARCHAR(20) NOT NULL,
    saldo DECIMAL(15,2) NOT NULL DEFAULT 0.0,
    idPersona INT NOT NULL,
    FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
);

-- Tabla Tarjeta (contiene FK a Cuenta)
CREATE TABLE Tarjeta (
    idTarjeta INT AUTO_INCREMENT PRIMARY KEY,
    estado ENUM('activa', 'inactiva') NOT NULL DEFAULT 'activa',
    nroTarjeta VARCHAR(20) NOT NULL,
    cvv VARCHAR(3) NOT NULL,
    fechaExpiracion VARCHAR(7) NOT NULL,
    pin VARCHAR(4) NOT NULL,
    idCuenta INT NOT NULL,
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);

-- Tabla Transaccion (contiene FK a Cuenta)
CREATE TABLE Transaccion (
    idTransaccion INT AUTO_INCREMENT PRIMARY KEY,
    tipoTransaccion ENUM('retiro', 'deposito') NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    descripcion VARCHAR(255),
    monto DECIMAL(15,2) NOT NULL,
    idCuenta INT NOT NULL,
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);

-- Tabla ATM
CREATE TABLE ATM (
    idATM INT AUTO_INCREMENT PRIMARY KEY,
    ubicacion VARCHAR(255) NOT NULL
);

-- Tabla TransaccionATM (tabla de relación entre Transaccion y ATM)
CREATE TABLE TransaccionATM (
    idTransaccion INT NOT NULL,
    idATM INT NOT NULL,
    PRIMARY KEY (idTransaccion, idATM),
    FOREIGN KEY (idTransaccion) REFERENCES Transaccion(idTransaccion),
    FOREIGN KEY (idATM) REFERENCES ATM(idATM)
);