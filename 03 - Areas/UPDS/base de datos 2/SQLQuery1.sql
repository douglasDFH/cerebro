create database ejercicio_9
use ejercicio_9


CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    FechaRegistro DATETIME DEFAULT GETDATE()
);
CREATE TABLE ClienteAudit (
    IdAudit INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT,
    Operacion NVARCHAR(10),
    Nombre NVARCHAR(100),
    Email NVARCHAR(100),
    FechaOperacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Producto (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Precio DECIMAL(10, 2),
    Stock INT
);
CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT FOREIGN KEY REFERENCES Cliente(IdCliente),
    FechaPedido DATETIME DEFAULT GETDATE(),
    Estado NVARCHAR(50)
);
CREATE TABLE DetallePedido (
    IdDetallePedido INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT FOREIGN KEY REFERENCES Pedido(IdPedido),
    IdProducto INT FOREIGN KEY REFERENCES Producto(IdProducto),
    Cantidad INT,
    Precio DECIMAL(10, 2)
);
CREATE TABLE PedidoAudit (
    IdAudit INT PRIMARY KEY IDENTITY(1,1),
    IdPedido INT,
    IdCliente INT,
    Operacion NVARCHAR(10),
    Estado NVARCHAR(50),
    FechaOperacion DATETIME DEFAULT GETDATE()
);

 

CREATE TRIGGER trgAuditCliente
ON Cliente
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Operacion NVARCHAR(10), @IdCliente INT, @Nombre NVARCHAR(100), @Email NVARCHAR(100);

    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Operacion = 'UPDATE';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @Operacion = 'INSERT';
    ELSE
        SET @Operacion = 'DELETE';

    SELECT @IdCliente = COALESCE(inserted.IdCliente, deleted.IdCliente),
           @Nombre = COALESCE(inserted.Nombre, deleted.Nombre),
           @Email = COALESCE(inserted.Email, deleted.Email)
    FROM inserted
    FULL OUTER JOIN deleted ON inserted.IdCliente = deleted.IdCliente;

    INSERT INTO ClienteAudit (IdCliente, Operacion, Nombre, Email)
    VALUES (@IdCliente, @Operacion, @Nombre, @Email);
END;

CREATE TRIGGER trgControlPrecioProducto
ON Producto
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
	    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON i.IdProducto = d.IdProducto
        WHERE i.Precio != d.Precio
    )
    BEGIN
        RAISERROR ('Modificación de precio no autorizada.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;

CREATE TRIGGER trgUpdateStock
ON DetallePedido
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdProducto INT, @Cantidad INT;

    SELECT @IdProducto = inserted.IdProducto, @Cantidad = inserted.Cantidad
    FROM inserted;

    UPDATE Producto
    SET Stock = Stock - @Cantidad
    WHERE IdProducto = @IdProducto;

    IF EXISTS (SELECT * FROM Producto WHERE IdProducto = @IdProducto AND Stock < 0)
    BEGIN
        RAISERROR ('Stock insuficiente para el producto.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;



CREATE TRIGGER trgAuditPedido
ON Pedido
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Operacion NVARCHAR(10), @IdPedido INT, @IdCliente INT, @Estado NVARCHAR(50);

    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Operacion = 'UPDATE';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @Operacion = 'INSERT';
    ELSE
        SET @Operacion = 'DELETE';

    SELECT @IdPedido = COALESCE(inserted.IdPedido, deleted.IdPedido),
           @IdCliente = COALESCE(inserted.IdCliente, deleted.IdCliente),
           @Estado = COALESCE(inserted.Estado, deleted.Estado)
    FROM inserted
    FULL OUTER JOIN deleted ON inserted.IdPedido = deleted.IdPedido;

    INSERT INTO PedidoAudit (IdPedido, IdCliente, Operacion, Estado)
    VALUES (@IdPedido, @IdCliente, @Operacion, @Estado);
END;

INSERT INTO Cliente (Nombre, Email, FechaRegistro)
VALUES ('Juan', 'juan.perez@example.com', GETDATE());

UPDATE Cliente
SET Nombre = 'Carlos', 
    Email = 'carlos.sanchez@example.com', 
    FechaRegistro = GETDATE()
WHERE IdCliente = 1;

DELETE FROM Cliente
WHERE IdCliente = 1;
SELECT * FROM Cliente;