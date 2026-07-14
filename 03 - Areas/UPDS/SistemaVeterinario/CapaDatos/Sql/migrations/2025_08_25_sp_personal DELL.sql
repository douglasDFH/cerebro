USE Sistema_Veterinario;
GO

-- ============================================
-- PROCEDIMIENTOS ALMACENADOS PARA PERSONAL
-- ============================================

-- 1. SP_InsertarPersonalAuxiliar
IF OBJECT_ID('SP_InsertarPersonalAuxiliar', 'P') IS NOT NULL
    DROP PROCEDURE SP_InsertarPersonalAuxiliar;
GO

CREATE PROCEDURE SP_InsertarPersonalAuxiliar
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @email VARCHAR(100),
    @usuario VARCHAR(50),
    @contrasena VARCHAR(255),
    @telefono VARCHAR(20) = NULL,
    @direccion VARCHAR(255) = NULL,
    @fecha_contratacion DATE,
    @salario DECIMAL(10,2) = NULL,
    @rol VARCHAR(20) = 'Usuario',
    @area VARCHAR(100) = NULL,
    @turno VARCHAR(10) = 'Mañana',
    @nivel VARCHAR(20) = 'Básico'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @personal_id INT;
    DECLARE @mensaje NVARCHAR(MAX) = '';

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar unicidad de usuario
        IF EXISTS(SELECT 1 FROM personal WHERE usuario = @usuario)
        BEGIN
            SELECT 0 AS id, 'Ya existe un usuario con este nombre de usuario' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de email
        IF EXISTS(SELECT 1 FROM personal WHERE email = @email)
        BEGIN
            SELECT 0 AS id, 'Ya existe una persona con este email' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar en tabla personal
        INSERT INTO personal (
            nombre, apellido, email, usuario, contrasena, telefono, direccion,
            fecha_contratacion, salario, rol, activo, created_at, updated_at
        ) VALUES (
            @nombre, @apellido, @email, @usuario, @contrasena, @telefono, @direccion,
            @fecha_contratacion, @salario, @rol, 1, GETDATE(), GETDATE()
        );

        SET @personal_id = SCOPE_IDENTITY();

        -- Insertar en tabla personal_auxiliar
        INSERT INTO personal_auxiliar (
            id, area, turno, nivel, created_at, updated_at
        ) VALUES (
            @personal_id, @area, @turno, @nivel, GETDATE(), GETDATE()
        );

        COMMIT TRANSACTION;
        SELECT @personal_id AS id, 'Personal auxiliar registrado correctamente' AS mensaje;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS id, ERROR_MESSAGE() AS mensaje;
    END CATCH
END
GO

-- 2. SP_InsertarPersonalVeterinario
IF OBJECT_ID('SP_InsertarPersonalVeterinario', 'P') IS NOT NULL
    DROP PROCEDURE SP_InsertarPersonalVeterinario;
GO

CREATE PROCEDURE SP_InsertarPersonalVeterinario
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @email VARCHAR(100),
    @usuario VARCHAR(50),
    @contrasena VARCHAR(255),
    @telefono VARCHAR(20) = NULL,
    @direccion VARCHAR(255) = NULL,
    @fecha_contratacion DATE,
    @salario DECIMAL(10,2) = NULL,
    @rol VARCHAR(20) = 'Usuario',
    @num_licencia VARCHAR(50) = NULL,
    @especialidad VARCHAR(100) = NULL,
    @universidad VARCHAR(200) = NULL,
    @anios_experiencia INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @personal_id INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar unicidad de usuario
        IF EXISTS(SELECT 1 FROM personal WHERE usuario = @usuario)
        BEGIN
            SELECT 0 AS id, 'Ya existe un usuario con este nombre de usuario' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de email
        IF EXISTS(SELECT 1 FROM personal WHERE email = @email)
        BEGIN
            SELECT 0 AS id, 'Ya existe una persona con este email' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de licencia
        IF @num_licencia IS NOT NULL AND EXISTS(SELECT 1 FROM personal_veterinario WHERE num_licencia = @num_licencia)
        BEGIN
            SELECT 0 AS id, 'Ya existe un veterinario con este número de licencia' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar en tabla personal
        INSERT INTO personal (
            nombre, apellido, email, usuario, contrasena, telefono, direccion,
            fecha_contratacion, salario, rol, activo, created_at, updated_at
        ) VALUES (
            @nombre, @apellido, @email, @usuario, @contrasena, @telefono, @direccion,
            @fecha_contratacion, @salario, @rol, 1, GETDATE(), GETDATE()
        );

        SET @personal_id = SCOPE_IDENTITY();

        -- Insertar en tabla personal_veterinario
        INSERT INTO personal_veterinario (
            id, num_licencia, especialidad, universidad, anios_experiencia, created_at, updated_at
        ) VALUES (
            @personal_id, @num_licencia, @especialidad, @universidad, @anios_experiencia, GETDATE(), GETDATE()
        );

        COMMIT TRANSACTION;
        SELECT @personal_id AS id, 'Personal veterinario registrado correctamente' AS mensaje;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS id, ERROR_MESSAGE() AS mensaje;
    END CATCH
END
GO

-- 3. SP_EditarPersonalAuxiliar
IF OBJECT_ID('SP_EditarPersonalAuxiliar', 'P') IS NOT NULL
    DROP PROCEDURE SP_EditarPersonalAuxiliar;
GO

CREATE PROCEDURE SP_EditarPersonalAuxiliar
    @id INT,
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @email VARCHAR(100),
    @usuario VARCHAR(50),
    @telefono VARCHAR(20) = NULL,
    @direccion VARCHAR(255) = NULL,
    @salario DECIMAL(10,2) = NULL,
    @rol VARCHAR(20) = 'Usuario',
    @area VARCHAR(100) = NULL,
    @turno VARCHAR(10) = 'Mañana',
    @nivel VARCHAR(20) = 'Básico'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar que el personal existe
        IF NOT EXISTS(SELECT 1 FROM personal WHERE id = @id)
        BEGIN
            SELECT 'El personal no existe' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de usuario
        IF EXISTS(SELECT 1 FROM personal WHERE usuario = @usuario AND id != @id)
        BEGIN
            SELECT 'Ya existe un usuario con este nombre de usuario' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de email
        IF EXISTS(SELECT 1 FROM personal WHERE email = @email AND id != @id)
        BEGIN
            SELECT 'Ya existe una persona con este email' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Actualizar tabla personal
        UPDATE personal SET
            nombre = @nombre,
            apellido = @apellido,
            email = @email,
            usuario = @usuario,
            telefono = @telefono,
            direccion = @direccion,
            salario = @salario,
            rol = @rol,
            modificado_por = 'Sistema',
            fecha_modificacion = GETDATE(),
            updated_at = GETDATE()
        WHERE id = @id;

        -- Actualizar tabla personal_auxiliar (o crear si no existe)
        IF EXISTS(SELECT 1 FROM personal_auxiliar WHERE id = @id)
        BEGIN
            UPDATE personal_auxiliar SET
                area = @area,
                turno = @turno,
                nivel = @nivel,
                updated_at = GETDATE()
            WHERE id = @id;
        END
        ELSE
        BEGIN
            INSERT INTO personal_auxiliar (id, area, turno, nivel, created_at, updated_at)
            VALUES (@id, @area, @turno, @nivel, GETDATE(), GETDATE());
        END

        COMMIT TRANSACTION;
        SELECT 'Personal auxiliar actualizado correctamente' AS mensaje;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS mensaje;
    END CATCH
END
GO

-- 4. SP_EditarPersonalVeterinario
IF OBJECT_ID('SP_EditarPersonalVeterinario', 'P') IS NOT NULL
    DROP PROCEDURE SP_EditarPersonalVeterinario;
GO

CREATE PROCEDURE SP_EditarPersonalVeterinario
    @id INT,
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @email VARCHAR(100),
    @usuario VARCHAR(50),
    @telefono VARCHAR(20) = NULL,
    @direccion VARCHAR(255) = NULL,
    @salario DECIMAL(10,2) = NULL,
    @rol VARCHAR(20) = 'Usuario',
    @num_licencia VARCHAR(50) = NULL,
    @especialidad VARCHAR(100) = NULL,
    @universidad VARCHAR(200) = NULL,
    @anios_experiencia INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Validar que el personal existe
        IF NOT EXISTS(SELECT 1 FROM personal WHERE id = @id)
        BEGIN
            SELECT 'El personal no existe' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de usuario
        IF EXISTS(SELECT 1 FROM personal WHERE usuario = @usuario AND id != @id)
        BEGIN
            SELECT 'Ya existe un usuario con este nombre de usuario' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de email
        IF EXISTS(SELECT 1 FROM personal WHERE email = @email AND id != @id)
        BEGIN
            SELECT 'Ya existe una persona con este email' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Validar unicidad de licencia
        IF @num_licencia IS NOT NULL AND EXISTS(SELECT 1 FROM personal_veterinario WHERE num_licencia = @num_licencia AND id != @id)
        BEGIN
            SELECT 'Ya existe un veterinario con este número de licencia' AS mensaje;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Actualizar tabla personal
        UPDATE personal SET
            nombre = @nombre,
            apellido = @apellido,
            email = @email,
            usuario = @usuario,
            telefono = @telefono,
            direccion = @direccion,
            salario = @salario,
            rol = @rol,
            modificado_por = 'Sistema',
            fecha_modificacion = GETDATE(),
            updated_at = GETDATE()
        WHERE id = @id;

        -- Actualizar tabla personal_veterinario (o crear si no existe)
        IF EXISTS(SELECT 1 FROM personal_veterinario WHERE id = @id)
        BEGIN
            UPDATE personal_veterinario SET
                num_licencia = @num_licencia,
                especialidad = @especialidad,
                universidad = @universidad,
                anios_experiencia = @anios_experiencia,
                updated_at = GETDATE()
            WHERE id = @id;
        END
        ELSE
        BEGIN
            INSERT INTO personal_veterinario (id, num_licencia, especialidad, universidad, anios_experiencia, created_at, updated_at)
            VALUES (@id, @num_licencia, @especialidad, @universidad, @anios_experiencia, GETDATE(), GETDATE());
        END

        COMMIT TRANSACTION;
        SELECT 'Personal veterinario actualizado correctamente' AS mensaje;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS mensaje;
    END CATCH
END
GO

-- 5. SP_EliminarPersonal
IF OBJECT_ID('SP_EliminarPersonal', 'P') IS NOT NULL
    DROP PROCEDURE SP_EliminarPersonal;
GO

CREATE PROCEDURE SP_EliminarPersonal
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Validar que el personal existe
        IF NOT EXISTS(SELECT 1 FROM personal WHERE id = @id)
        BEGIN
            SELECT 0 AS resultado, 'El personal no existe' AS mensaje;
            RETURN;
        END

        -- Marcar como inactivo en lugar de eliminar
        UPDATE personal 
        SET activo = 0, 
            modificado_por = 'Sistema',
            fecha_modificacion = GETDATE(),
            updated_at = GETDATE()
        WHERE id = @id;

        SELECT 1 AS resultado, 'Personal eliminado correctamente' AS mensaje;
    END TRY
    BEGIN CATCH
        SELECT 0 AS resultado, ERROR_MESSAGE() AS mensaje;
    END CATCH
END
GO

-- 6. SP_MostrarPersonal
IF OBJECT_ID('SP_MostrarPersonal', 'P') IS NOT NULL
    DROP PROCEDURE SP_MostrarPersonal;
GO

CREATE PROCEDURE SP_MostrarPersonal
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.nombre,
        p.apellido,
        p.email,
        p.usuario,
        p.telefono,
        p.direccion,
        p.fecha_contratacion,
        p.salario,
        p.rol,
        p.activo,
        p.fecha_ultimo_acceso,
        p.created_at,
        p.updated_at,
        -- Campos de veterinario
        pv.num_licencia,
        pv.especialidad,
        pv.universidad,
        pv.anios_experiencia,
        -- Campos de auxiliar
        pa.area,
        pa.turno,
        pa.nivel,
        -- Tipo determinado dinámicamente
        CASE 
            WHEN pv.id IS NOT NULL THEN 'Veterinario'
            WHEN pa.id IS NOT NULL THEN 'Auxiliar'
            ELSE 'General'
        END AS tipo_personal
    FROM personal p
    LEFT JOIN personal_veterinario pv ON p.id = pv.id
    LEFT JOIN personal_auxiliar pa ON p.id = pa.id
    WHERE p.activo = 1
    ORDER BY p.apellido, p.nombre;
END
GO

-- 7. SP_BuscarPersonalPorTexto
IF OBJECT_ID('SP_BuscarPersonalPorTexto', 'P') IS NOT NULL
    DROP PROCEDURE SP_BuscarPersonalPorTexto;
GO

CREATE PROCEDURE SP_BuscarPersonalPorTexto
    @textoBuscar NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.nombre,
        p.apellido,
        p.email,
        p.usuario,
        p.telefono,
        p.direccion,
        p.fecha_contratacion,
        p.salario,
        p.rol,
        p.activo,
        p.fecha_ultimo_acceso,
        p.created_at,
        p.updated_at,
        -- Campos de veterinario
        pv.num_licencia,
        pv.especialidad,
        pv.universidad,
        pv.anios_experiencia,
        -- Campos de auxiliar
        pa.area,
        pa.turno,
        pa.nivel,
        -- Tipo determinado dinámicamente
        CASE 
            WHEN pv.id IS NOT NULL THEN 'Veterinario'
            WHEN pa.id IS NOT NULL THEN 'Auxiliar'
            ELSE 'General'
        END AS tipo_personal
    FROM personal p
    LEFT JOIN personal_veterinario pv ON p.id = pv.id
    LEFT JOIN personal_auxiliar pa ON p.id = pa.id
    WHERE p.activo = 1
    AND (
        p.nombre LIKE '%' + @textoBuscar + '%' OR
        p.apellido LIKE '%' + @textoBuscar + '%' OR
        p.email LIKE '%' + @textoBuscar + '%' OR
        p.usuario LIKE '%' + @textoBuscar + '%' OR
        p.telefono LIKE '%' + @textoBuscar + '%' OR
        pv.num_licencia LIKE '%' + @textoBuscar + '%' OR
        pv.especialidad LIKE '%' + @textoBuscar + '%' OR
        pa.area LIKE '%' + @textoBuscar + '%'
    )
    ORDER BY p.apellido, p.nombre;
END
GO

-- 8. SP_BuscarPersonalPorTipo
IF OBJECT_ID('SP_BuscarPersonalPorTipo', 'P') IS NOT NULL
    DROP PROCEDURE SP_BuscarPersonalPorTipo;
GO

CREATE PROCEDURE SP_BuscarPersonalPorTipo
    @tipoPersonal VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.nombre,
        p.apellido,
        p.email,
        p.usuario,
        p.telefono,
        p.direccion,
        p.fecha_contratacion,
        p.salario,
        p.rol,
        p.activo,
        p.fecha_ultimo_acceso,
        p.created_at,
        p.updated_at,
        -- Campos de veterinario
        pv.num_licencia,
        pv.especialidad,
        pv.universidad,
        pv.anios_experiencia,
        -- Campos de auxiliar
        pa.area,
        pa.turno,
        pa.nivel,
        -- Tipo determinado dinámicamente
        CASE 
            WHEN pv.id IS NOT NULL THEN 'Veterinario'
            WHEN pa.id IS NOT NULL THEN 'Auxiliar'
            ELSE 'General'
        END AS tipo_personal
    FROM personal p
    LEFT JOIN personal_veterinario pv ON p.id = pv.id
    LEFT JOIN personal_auxiliar pa ON p.id = pa.id
    WHERE p.activo = 1
    AND (
        (@tipoPersonal = 'Veterinario' AND pv.id IS NOT NULL) OR
        (@tipoPersonal = 'Auxiliar' AND pa.id IS NOT NULL)
    )
    ORDER BY p.apellido, p.nombre;
END
GO

-- 9. SP_ObtenerPersonalPorId
IF OBJECT_ID('SP_ObtenerPersonalPorId', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerPersonalPorId;
GO

CREATE PROCEDURE SP_ObtenerPersonalPorId
    @id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id,
        p.nombre,
        p.apellido,
        p.email,
        p.usuario,
        p.telefono,
        p.direccion,
        p.fecha_contratacion,
        p.salario,
        p.rol,
        p.activo,
        p.fecha_ultimo_acceso,
        p.created_at,
        p.updated_at,
        -- Campos de veterinario
        pv.num_licencia,
        pv.especialidad,
        pv.universidad,
        pv.anios_experiencia,
        -- Campos de auxiliar
        pa.area,
        pa.turno,
        pa.nivel,
        -- Tipo determinado dinámicamente
        CASE 
            WHEN pv.id IS NOT NULL THEN 'Veterinario'
            WHEN pa.id IS NOT NULL THEN 'Auxiliar'
            ELSE 'General'
        END AS tipo_personal
    FROM personal p
    LEFT JOIN personal_veterinario pv ON p.id = pv.id
    LEFT JOIN personal_auxiliar pa ON p.id = pa.id
    WHERE p.id = @id;
END
GO

-- 10. SP_ValidarUsuarioUnico
IF OBJECT_ID('SP_ValidarUsuarioUnico', 'P') IS NOT NULL
    DROP PROCEDURE SP_ValidarUsuarioUnico;
GO

CREATE PROCEDURE SP_ValidarUsuarioUnico
    @usuario VARCHAR(50),
    @idPersonal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @existe BIT = 0;

    IF EXISTS(
        SELECT 1 FROM personal 
        WHERE usuario = @usuario 
        AND (@idPersonal IS NULL OR id != @idPersonal)
    )
        SET @existe = 1;

    SELECT @existe AS existe;
END
GO

-- 11. SP_ValidarEmailPersonalUnico
IF OBJECT_ID('SP_ValidarEmailPersonalUnico', 'P') IS NOT NULL
    DROP PROCEDURE SP_ValidarEmailPersonalUnico;
GO

CREATE PROCEDURE SP_ValidarEmailPersonalUnico
    @email VARCHAR(100),
    @idPersonal INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @existe BIT = 0;

    IF EXISTS(
        SELECT 1 FROM personal 
        WHERE email = @email 
        AND (@idPersonal IS NULL OR id != @idPersonal)
    )
        SET @existe = 1;

    SELECT @existe AS existe;
END
GO

PRINT 'Procedimientos almacenados para Personal creados correctamente.';