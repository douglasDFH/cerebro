CREATE PROCEDURE SP_EliminarPersona
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM persona WHERE id = @id)
        BEGIN
            UPDATE persona 
            SET activo = 0 
            WHERE id = @id;
            
            SELECT 1 as resultado, 'Persona eliminada correctamente' as mensaje;
        END
        ELSE
        BEGIN
            SELECT 0 as resultado, 'No existe persona con el ID especificado' as mensaje;
        END
    END TRY
    BEGIN CATCH
        SELECT 0 as resultado, ERROR_MESSAGE() as mensaje;
    END CATCH
END;
GO

CREATE PROCEDURE SP_BuscarPersonaPorTexto
    @textoBuscar VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.id, p.tipo, p.email, p.direccion, p.telefono, p.activo,
        pf.ci, pf.nombre, pf.apellido, pf.fecha_nacimiento, pf.genero,
        pj.razon_social, pj.nit, pj.encargado_nombre, pj.encargado_cargo,
        p.created_at, p.updated_at
    FROM persona p
    LEFT JOIN persona_fisica pf ON p.id = pf.id
    LEFT JOIN persona_juridica pj ON p.id = pj.id
    WHERE p.activo = 1 
    AND (
        pf.nombre LIKE '%' + @textoBuscar + '%' OR 
        pf.apellido LIKE '%' + @textoBuscar + '%' OR 
        pf.ci LIKE '%' + @textoBuscar + '%' OR
        pj.razon_social LIKE '%' + @textoBuscar + '%' OR 
        pj.nit LIKE '%' + @textoBuscar + '%' OR
        p.email LIKE '%' + @textoBuscar + '%' OR
        p.telefono LIKE '%' + @textoBuscar + '%'
    )
    ORDER BY 
        CASE WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
             ELSE pj.razon_social END;
END;
GO

CREATE PROCEDURE SP_BuscarPersonaPorTipo
    @tipoPersona VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.id, p.tipo, p.email, p.direccion, p.telefono, p.activo,
        pf.ci, pf.nombre, pf.apellido, pf.fecha_nacimiento, pf.genero,
        pj.razon_social, pj.nit, pj.encargado_nombre, pj.encargado_cargo,
        p.created_at, p.updated_at
    FROM persona p
    LEFT JOIN persona_fisica pf ON p.id = pf.id
    LEFT JOIN persona_juridica pj ON p.id = pj.id
    WHERE p.activo = 1 
    AND p.tipo = @tipoPersona
    ORDER BY 
        CASE WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
             ELSE pj.razon_social END;
END;
GO

CREATE PROCEDURE SP_MostrarPersonas
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.id, p.tipo, p.email, p.direccion, p.telefono, p.activo,
        pf.ci, pf.nombre, pf.apellido, pf.fecha_nacimiento, pf.genero,
        pj.razon_social, pj.nit, pj.encargado_nombre, pj.encargado_cargo,
        p.created_at, p.updated_at
    FROM persona p
    LEFT JOIN persona_fisica pf ON p.id = pf.id
    LEFT JOIN persona_juridica pj ON p.id = pj.id
    WHERE p.activo = 1
    ORDER BY 
        CASE WHEN p.tipo = 'Física' THEN CONCAT(pf.nombre, ' ', pf.apellido)
             ELSE pj.razon_social END;
END;
GO

CREATE PROCEDURE SP_ObtenerPersonaPorId
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        p.id, p.tipo, p.email, p.direccion, p.telefono, p.activo,
        pf.ci, pf.nombre, pf.apellido, pf.fecha_nacimiento, pf.genero,
        pj.razon_social, pj.nit, pj.encargado_nombre, pj.encargado_cargo,
        p.created_at, p.updated_at
    FROM persona p
    LEFT JOIN persona_fisica pf ON p.id = pf.id
    LEFT JOIN persona_juridica pj ON p.id = pj.id
    WHERE p.id = @id;
END;
GO

CREATE PROCEDURE SP_ValidarCIUnico
    @ci VARCHAR(15),
    @idPersona INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @existe BIT = 0;
    
    IF EXISTS (
        SELECT 1 FROM persona_fisica pf
        INNER JOIN persona p ON pf.id = p.id
        WHERE pf.ci = @ci 
        AND p.activo = 1
        AND (@idPersona IS NULL OR pf.id != @idPersona)
    )
    BEGIN
        SET @existe = 1;
    END
    
    SELECT @existe as existe;
END;
GO

CREATE PROCEDURE SP_ValidarNITUnico
    @nit VARCHAR(20),
    @idPersona INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @existe BIT = 0;
    
    IF EXISTS (
        SELECT 1 FROM persona_juridica pj
        INNER JOIN persona p ON pj.id = p.id
        WHERE pj.nit = @nit 
        AND p.activo = 1
        AND (@idPersona IS NULL OR pj.id != @idPersona)
    )
    BEGIN
        SET @existe = 1;
    END
    
    SELECT @existe as existe;
END;
GO

CREATE PROCEDURE SP_ValidarEmailUnico
    @email VARCHAR(255),
    @idPersona INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @existe BIT = 0;
    
    IF EXISTS (
        SELECT 1 FROM persona 
        WHERE email = @email 
        AND activo = 1
        AND (@idPersona IS NULL OR id != @idPersona)
    )
    BEGIN
        SET @existe = 1;
    END
    
    SELECT @existe as existe;
END;
GO

PRINT 'Procedimientos almacenados adicionales para Persona creados correctamente.';
