CREATE DATABASE banco_db;
USE banco_db;
-- --------------------------------------------------------
-- Estructura de la base de datos (MySQL/MariaDB)
-- --------------------------------------------------------

-- Tabla Oficina (con restricción de oficina central única)
CREATE TABLE Oficina (
    idOficina INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(255) NOT NULL,
    telefono VARCHAR(20),
    esCentral BOOLEAN NOT NULL DEFAULT FALSE
);

-- Trigger para garantizar una sola oficina central
DELIMITER //
CREATE TRIGGER evitar_oficinas_centrales_multiples
BEFORE INSERT ON Oficina
FOR EACH ROW
BEGIN
    IF NEW.esCentral = TRUE THEN
        IF (SELECT COUNT(*) FROM Oficina WHERE esCentral = TRUE) > 0 THEN
            SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'Solo se permite una oficina central';
        END IF;
    END IF;
END //
DELIMITER ;

-- Tabla Persona (sin cambios)
CREATE TABLE Persona (
    idPersona INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    dni VARCHAR(20) UNIQUE NOT NULL,
    direccion VARCHAR(255),
    telefono VARCHAR(20),
    email VARCHAR(100) UNIQUE,
    fechaNacimiento DATE
);

-- Tabla Usuario (con usuario único por persona)
CREATE TABLE Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    idPersona INT NOT NULL UNIQUE, -- Restricción UNIQUE añadida
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    rol ENUM('admin', 'cliente', 'empleado') NOT NULL,
    last_login TIMESTAMP NULL DEFAULT NULL,
    failed_attempts INT DEFAULT 0,
    locked_until TIMESTAMP NULL DEFAULT NULL,
    FOREIGN KEY (idPersona) REFERENCES Persona(idPersona)
);

-- Tabla Cuenta (con tipo DECIMAL para saldo)
CREATE TABLE Cuenta (
    idCuenta INT AUTO_INCREMENT PRIMARY KEY,
    idPersona INT NOT NULL,
    idOficina INT NOT NULL,
    numeroCuenta VARCHAR(20) UNIQUE NOT NULL,
    tipoCuenta ENUM('ahorro', 'corriente') NOT NULL,
    saldo DECIMAL(15,2) NOT NULL DEFAULT 0.0, -- Cambiado a DECIMAL
    fechaApertura DATE NOT NULL,
    activa BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (idPersona) REFERENCES Persona(idPersona),
    FOREIGN KEY (idOficina) REFERENCES Oficina(idOficina)
);

-- Tabla Tarjeta (sin CVV y con datos encriptados)
CREATE TABLE Tarjeta (
    idTarjeta INT AUTO_INCREMENT PRIMARY KEY,
    idCuenta INT NOT NULL,
    numeroTarjeta VARBINARY(255) NOT NULL, -- Encriptado
    fechaExpiracion CHAR(5) NOT NULL, -- Formato MM/AA
    activa BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);

-- Tabla Transaccion (con cuenta destino y monto DECIMAL)
CREATE TABLE Transaccion (
    idTransaccion INT AUTO_INCREMENT PRIMARY KEY,
    idCuenta INT NOT NULL,
    idCuentaDestino INT NULL, -- Nuevo campo para transferencias
    tipoTransaccion ENUM('deposito', 'retiro', 'transferencia', 'pago') NOT NULL,
    monto DECIMAL(15,2) NOT NULL, -- Cambiado a DECIMAL
    fechaHora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    descripcion TEXT,
    estado ENUM('completada', 'pendiente', 'cancelada') NOT NULL,
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta),
    FOREIGN KEY (idCuentaDestino) REFERENCES Cuenta(idCuenta)
);

-- Tabla ATM (sin cambios)
CREATE TABLE ATM (
    idATM INT AUTO_INCREMENT PRIMARY KEY,
    ubicacion VARCHAR(255) NOT NULL,
    enServicio BOOLEAN NOT NULL DEFAULT TRUE
);

-- Tabla ATM_Transaccion (sin cambios)
CREATE TABLE ATM_Transaccion (
    idATM INT NOT NULL,
    idTransaccion INT NOT NULL,
    PRIMARY KEY (idATM, idTransaccion),
    FOREIGN KEY (idATM) REFERENCES ATM(idATM),
    FOREIGN KEY (idTransaccion) REFERENCES Transaccion(idTransaccion)
);

-- --------------------------------------------------------
-- Índices recomendados
-- --------------------------------------------------------
-- CREATE INDEX idx_transaccion_fecha ON Transaccion(fechaHora);
-- CREATE INDEX idx_cuenta_numero ON Cuenta(numeroCuenta);
-- CREATE INDEX idx_persona_dni ON Persona(dni);