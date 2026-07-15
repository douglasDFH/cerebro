create database ejercicio_10
use ejercicio_10

-- Tabla: alma
CREATE TABLE alma (
    calm INT PRIMARY KEY,
    noma CHAR(40),
    ciud CHAR(2)
);
-- Tabla: pventas
CREATE TABLE pventas (
    nvta INT PRIMARY KEY,
    nomc CHAR(40),
    calm INT,
    fra DATE,
    nota DECIMAL(12, 2),
    ides DECIMAL(12, 2),
    FOREIGN KEY (calm) REFERENCES alma(calm)
);

-- Tabla: log_pventas
CREATE TABLE log_pventas (
    ntra INT PRIMARY KEY,
	nvta INT,
    fra DATETIME,
    usua CHAR(15),
    tpo CHAR(1),
    FOREIGN KEY (ntra) REFERENCES pventas(nvta)
);

-- Tabla: prov
CREATE TABLE prov (
    cprv INT PRIMARY KEY,
    nomb CHAR(40),
    ciud CHAR(2)
);
-- Tabla: prod
CREATE TABLE prod (
    cprd INT PRIMARY KEY,
    nomp CHAR(40),
    colo CHAR(15)
	);

-- Tabla: sumi
CREATE TABLE sumi (
    cprv INT,
    calm INT,
    cprd INT,
    fra DATE,
    cant DECIMAL(12, 2),
    prec DECIMAL(12, 2),
    impt DECIMAL(12, 2),
    PRIMARY KEY (cprv, calm, cprd, fra),
    FOREIGN KEY (cprv) REFERENCES prov(cprv),
    FOREIGN KEY (calm) REFERENCES alma(calm),
    FOREIGN KEY (cprd) REFERENCES prod(cprd)
);

-- Tabla: dventas
CREATE TABLE dventas (
    nvta INT,
    cprd INT,
    cant DECIMAL(12, 2),
    prec DECIMAL(12, 2),
    impt DECIMAL(12, 2),
    PRIMARY KEY (nvta, cprd),
    FOREIGN KEY (nvta) REFERENCES pventas(nvta),
    FOREIGN KEY (cprd) REFERENCES prod(cprd)
);


drop trigger upd_pventas
-- Crear trigger upd_pventas
CREATE TRIGGER upd_pventas
ON dventas
FOR INSERT
AS
BEGIN
    DECLARE @nvta INT;
    DECLARE @nomc CHAR(40);
    DECLARE @ides DECIMAL(12, 2);
    DECLARE @itot DECIMAL(12, 2);

    -- Suponiendo que sólo hay una fila insertada
    SELECT @nvta = nvta FROM inserted;

    SELECT @nomc = nomc FROM pventas WHERE nvta = @nvta;

    SET @ides = dbo.ImporteDescuento(@nvta, @nomc);
    SET @itot = dbo.ImporteTotal(@nvta);
    SET @itot = @itot - @ides;
	update pventas set ides = @ides 
	where nvta = @nvta
end


-- Crear función ImporteDescuento
CREATE FUNCTION ImporteDescuento(@nvta INT, @nomc CHAR(30))
RETURNS DECIMAL(12, 2)
AS
BEGIN
    DECLARE @TotAcum DECIMAL(12, 2);
    DECLARE @cant INT;
    DECLARE @ides DECIMAL(12, 2);

    SELECT 
        @TotAcum = ISNULL(SUM(impt), 0), 
        @cant = ISNULL(COUNT(DISTINCT pventas.nvta), 0)
    FROM 
        pventas
    JOIN 
        dventas ON pventas.nvta = dventas.nvta
    WHERE 
        pventas.nvta != @nvta AND nomc = @nomc;

    SET @ides = 0;
    IF (@cant > 3 OR @TotAcum = 25)
    BEGIN
        SET @ides = dbo.ImporteTotal(@nvta) * 0.10;
    END

    RETURN @ides;
END;

-- Crear función ImporteTotal
CREATE FUNCTION ImporteTotal(@nvta INT)
RETURNS DECIMAL(12, 2)
AS
BEGIN
    RETURN (SELECT ISNULL(SUM(impt), 0) FROM dventas WHERE nvta = @nvta);
END;

Select*from pventas
Select*from dventas

Insert into pventas Values (1,'David',1,Getdate(),0,0)
Insert into dventas Values (1,1,10,3,30)
Insert into pventas Values (2,'David',1,Getdate(),0,0)
Insert into dventas Values (2,1,10,3,30)
Insert into pventas Values (3,'David',1,Getdate(),0,0)
Insert into dventas Values (3,1,10,3,30)
Insert into pventas Values (4,'David',1,Getdate(),0,0)
Insert into dventas Values (4,1,10,3,30)

Insert into pventas Values (10,'David',1,Getdate(),0,0)
Insert into dventas Values (10,1,10,3,30)
Insert into dventas Values (10,2,5,2,10)