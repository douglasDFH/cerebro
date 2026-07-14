
BEGIN TRANSACTION;

IF NOT EXISTS (SELECT 1 FROM categoria WHERE nombre = 'Medicamentos')
    INSERT INTO categoria (nombre, descripcion, activo) VALUES ('Medicamentos', 'Medicamentos veterinarios', 1);

IF NOT EXISTS (SELECT 1 FROM categoria WHERE nombre = 'Alimentos')
    INSERT INTO categoria (nombre, descripcion, activo) VALUES ('Alimentos', 'Alimentos para mascotas', 1);

IF NOT EXISTS (SELECT 1 FROM categoria WHERE nombre = 'Accesorios')
    INSERT INTO categoria (nombre, descripcion, activo) VALUES ('Accesorios', 'Accesorios y juguetes', 1);

IF NOT EXISTS (SELECT 1 FROM categoria WHERE nombre = 'Higiene')
    INSERT INTO categoria (nombre, descripcion, activo) VALUES ('Higiene', 'Productos de higiene', 1);

COMMIT TRANSACTION;

-- Declarar variables para las categorías
DECLARE @CatMedicamentos INT, @CatAlimentos INT, @CatAccesorios INT, @CatHigiene INT;
SELECT @CatMedicamentos = id FROM categoria WHERE nombre = 'Medicamentos';
SELECT @CatAlimentos = id FROM categoria WHERE nombre = 'Alimentos';
SELECT @CatAccesorios = id FROM categoria WHERE nombre = 'Accesorios';
SELECT @CatHigiene = id FROM categoria WHERE nombre = 'Higiene';

-- =============================================
-- 2. CREAR DATOS BASE - PRODUCTOS
-- =============================================

BEGIN TRANSACTION;

-- Medicamentos
INSERT INTO producto (codigo, nombre, descripcion, precio, stock_minimo, stock_actual, requiere_receta, categoria_id, activo) VALUES
('MED001', 'Antibiótico Amoxicilina 500mg', 'Antibiótico de amplio espectro', 25.50, 10, 1000, 1, @CatMedicamentos, 1),
('MED002', 'Antiparasitario Interno', 'Desparasitante para uso interno', 18.75, 15, 800, 1, @CatMedicamentos, 1),
('MED003', 'Vacuna Antirrábica', 'Vacuna contra la rabia', 35.00, 20, 500, 1, @CatMedicamentos, 1),
('MED004', 'Vitaminas B Complex', 'Complejo vitamínico B', 12.25, 25, 600, 0, @CatMedicamentos, 1),
('MED005', 'Antiinflamatorio Meloxicam', 'Antiinflamatorio no esteroideo', 28.90, 12, 400, 1, @CatMedicamentos, 1),
('MED006', 'Desparasitante Externo', 'Antipulgas y garrapatas', 15.60, 18, 700, 0, @CatMedicamentos, 1),
('MED007', 'Suplemento Nutricional', 'Suplemento vitamínico general', 22.40, 8, 350, 0, @CatMedicamentos, 1),
('MED008', 'Analgésico Tramadol', 'Analgésico para dolor moderado a severo', 45.80, 5, 300, 1, @CatMedicamentos, 1),
('MED009', 'Antibiótico Cefalexina', 'Cefalosporina de primera generación', 38.25, 15, 450, 1, @CatMedicamentos, 1),
('MED010', 'Probiótico Veterinario', 'Probióticos para salud digestiva', 32.50, 10, 250, 0, @CatMedicamentos, 1);

COMMIT TRANSACTION;
BEGIN TRANSACTION;

-- Alimentos
INSERT INTO producto (codigo, nombre, descripcion, precio, stock_minimo, stock_actual, requiere_receta, categoria_id, activo) VALUES
('ALI001', 'Alimento Premium Perro Adulto 15kg', 'Alimento balanceado para perros adultos', 85.00, 5, 150, 0, @CatAlimentos, 1),
('ALI002', 'Alimento Premium Gato Adulto 7.5kg', 'Alimento balanceado para gatos adultos', 65.00, 8, 120, 0, @CatAlimentos, 1),
('ALI003', 'Alimento Cachorro 10kg', 'Alimento especial para cachorros', 75.00, 6, 100, 0, @CatAlimentos, 1),
('ALI004', 'Snacks Dentales Perro', 'Premios para higiene dental', 18.50, 20, 300, 0, @CatAlimentos, 1),
('ALI005', 'Leche Maternizada Cachorros', 'Sustituto de leche materna', 45.00, 10, 80, 0, @CatAlimentos, 1);

COMMIT TRANSACTION;
BEGIN TRANSACTION;

-- Accesorios
INSERT INTO producto (codigo, nombre, descripcion, precio, stock_minimo, stock_actual, requiere_receta, categoria_id, activo) VALUES
('ACC001', 'Collar Ajustable Mediano', 'Collar de nylon ajustable', 12.00, 15, 200, 0, @CatAccesorios, 1),
('ACC002', 'Correa Retráctil 5m', 'Correa retráctil para paseos', 35.00, 10, 150, 0, @CatAccesorios, 1),
('ACC003', 'Juguete Pelota Resistente', 'Pelota de goma resistente', 8.50, 25, 400, 0, @CatAccesorios, 1),
('ACC004', 'Cama Perro Grande', 'Cama acolchada para perros grandes', 95.00, 5, 50, 0, @CatAccesorios, 1),
('ACC005', 'Transportadora Gato', 'Transportadora plástica para gatos', 125.00, 3, 30, 0, @CatAccesorios, 1);

-- Higiene
INSERT INTO producto (codigo, nombre, descripcion, precio, stock_minimo, stock_actual, requiere_receta, categoria_id, activo) VALUES
('HIG001', 'Shampoo Antipulgas 500ml', 'Shampoo medicado antipulgas', 28.00, 12, 180, 0, @CatHigiene, 1),
('HIG002', 'Toallitas Húmedas Mascotas', 'Toallitas para limpieza diaria', 15.00, 20, 250, 0, @CatHigiene, 1),
('HIG003', 'Desodorante Ambiental', 'Neutralizador de olores para mascotas', 22.50, 15, 160, 0, @CatHigiene, 1),
('HIG004', 'Cepillo Dental Pet', 'Cepillo de dientes para mascotas', 12.75, 18, 220, 0, @CatHigiene, 1),
('HIG005', 'Pasta Dental Veterinaria', 'Pasta dental especial para mascotas', 18.90, 15, 140, 0, @CatHigiene, 1);

COMMIT TRANSACTION;



-- =============================================
-- 3. CREAR DATOS BASE - SERVICIOS MEDICOS
-- =============================================


INSERT INTO diagnostico (codigo, nombre, descripcion, precio_base, categoria_diagnostico, requiere_equipamiento, activo) VALUES
('SERV001', 'Consulta Veterinaria General', 'Consulta médica general', 50.00, 'Consultas', 0, 1),
('SERV002', 'Vacunación', 'Aplicación de vacunas', 40.00, 'Prevención', 0, 1),
('SERV003', 'Desparasitación', 'Tratamiento antiparasitario', 30.00, 'Prevención', 0, 1),
('SERV004', 'Cirugía Menor', 'Procedimientos quirúrgicos menores', 200.00, 'Cirugía', 1, 1),
('SERV005', 'Examen de Laboratorio', 'Análisis clínicos básicos', 80.00, 'Diagnóstico', 1, 1),
('SERV006', 'Radiografía', 'Estudio radiológico', 120.00, 'Diagnóstico', 1, 1),
('SERV007', 'Limpieza Dental', 'Profilaxis dental veterinaria', 150.00, 'Odontología', 1, 1),
('SERV008', 'Esterilización', 'Castración/esterilización', 300.00, 'Cirugía', 1, 1),
('SERV009', 'Ecografía', 'Ultrasonido diagnóstico', 100.00, 'Diagnóstico', 1, 1),
('SERV010', 'Hospitalización (día)', 'Internación por día', 75.00, 'Hospitalización', 0, 1),
('SERV011', 'Curación de Heridas', 'Tratamiento de heridas menores', 35.00, 'Tratamiento', 0, 1),
('SERV012', 'Control Post-operatorio', 'Seguimiento post-cirugía', 45.00, 'Seguimiento', 0, 1),
('SERV013', 'Terapia con Suero', 'Fluidoterapia intravenosa', 60.00, 'Tratamiento', 0, 1),
('SERV014', 'Aplicación de Inyecciones', 'Administración de medicamentos inyectables', 25.00, 'Tratamiento', 0, 1),
('SERV015', 'Corte de Uñas', 'Recorte de uñas profesional', 20.00, 'Estética', 0, 1);



-- =============================================
-- 4. CREAR DATOS BASE - PERSONAS
-- =============================================

-- Personas Físicas
DECLARE @i INT = 1;
WHILE @i <= 50
BEGIN
    DECLARE @persona_id INT;
    DECLARE @nombre VARCHAR(100), @apellido VARCHAR(100), @ci VARCHAR(15), @email VARCHAR(255);
    DECLARE @telefono VARCHAR(20), @direccion VARCHAR(255);
    
    -- Generar datos aleatorios
    SET @nombre = CASE (ABS(CHECKSUM(NEWID())) % 20)
        WHEN 0 THEN 'Juan' WHEN 1 THEN 'Maria' WHEN 2 THEN 'Carlos' WHEN 3 THEN 'Ana'
        WHEN 4 THEN 'Luis' WHEN 5 THEN 'Carmen' WHEN 6 THEN 'Pedro' WHEN 7 THEN 'Rosa'
        WHEN 8 THEN 'Diego' WHEN 9 THEN 'Elena' WHEN 10 THEN 'Miguel' WHEN 11 THEN 'Laura'
        WHEN 12 THEN 'José' WHEN 13 THEN 'Patricia' WHEN 14 THEN 'Antonio' WHEN 15 THEN 'Isabel'
        WHEN 16 THEN 'Francisco' WHEN 17 THEN 'Mónica' WHEN 18 THEN 'Javier' ELSE 'Claudia'
    END;
    
    SET @apellido = CASE (ABS(CHECKSUM(NEWID())) % 20)
        WHEN 0 THEN 'García López' WHEN 1 THEN 'Rodríguez Silva' WHEN 2 THEN 'Martínez Quispe'
        WHEN 3 THEN 'López Mamani' WHEN 4 THEN 'González Vargas' WHEN 5 THEN 'Pérez Condori'
        WHEN 6 THEN 'Sánchez Morales' WHEN 7 THEN 'Ramírez Choque' WHEN 8 THEN 'Cruz Apaza'
        WHEN 9 THEN 'Flores Torrez' WHEN 10 THEN 'Herrera Nina' WHEN 11 THEN 'Jiménez Calle'
        WHEN 12 THEN 'Morales Quisbert' WHEN 13 THEN 'Ruiz Limachi' WHEN 14 THEN 'Díaz Huanca'
        WHEN 15 THEN 'Álvarez Chura' WHEN 16 THEN 'Romero Condori' WHEN 17 THEN 'Torres Inca'
        WHEN 18 THEN 'Domínguez Copa' ELSE 'Gutiérrez Mamani'
    END;
    
    SET @ci = CAST((ABS(CHECKSUM(NEWID())) % 10000000) + 1000000 AS VARCHAR) + ' LP';
    SET @email = LOWER(@nombre) + '.' + LOWER(REPLACE(@apellido, ' ', '')) + '@email.com';
    SET @telefono = '7' + CAST((ABS(CHECKSUM(NEWID())) % 10000000) + 1000000 AS VARCHAR(7));
    SET @direccion = 'Av. ' + @apellido + ' #' + CAST((ABS(CHECKSUM(NEWID())) % 999) + 100 AS VARCHAR);

    -- Insertar persona
    INSERT INTO persona (tipo, email, direccion, telefono, activo)
    VALUES ('Física', @email, @direccion, @telefono, 1);
    
    SET @persona_id = SCOPE_IDENTITY();
    
    -- Insertar persona física
    INSERT INTO persona_fisica (id, ci, nombre, apellido, fecha_nacimiento, genero)
    VALUES (@persona_id, @ci, @nombre, @apellido, 
            DATEADD(YEAR, -(ABS(CHECKSUM(NEWID())) % 50) - 18, GETDATE()),
            CASE ABS(CHECKSUM(NEWID())) % 2 WHEN 0 THEN 'M' ELSE 'F' END);
    
    SET @i = @i + 1;
END;

-- Personas Jurídicas
SET @i = 1;
WHILE @i <= 10
BEGIN
    DECLARE @razon_social VARCHAR(255), @nit VARCHAR(20);
    
    SET @razon_social = CASE (ABS(CHECKSUM(NEWID())) % 10)
        WHEN 0 THEN 'Veterinaria San Martín SRL' WHEN 1 THEN 'Clínica Animal Care LTDA'
        WHEN 2 THEN 'Pet Shop Los Amigos SRL' WHEN 3 THEN 'Hospital Veterinario Central SA'
        WHEN 4 THEN 'Farmacia Veterinaria Salud Animal' WHEN 5 THEN 'Refugio de Mascotas Esperanza'
        WHEN 6 THEN 'Tienda de Mascotas El Paraíso' WHEN 7 THEN 'Centro de Adopción Animal'
        WHEN 8 THEN 'Cuidados Veterinarios Integrales' ELSE 'Servicios Veterinarios Premium'
    END;
    
    SET @nit = CAST((ABS(CHECKSUM(NEWID())) % 900000000) + 100000000 AS VARCHAR);
    SET @email = 'info@' + REPLACE(LOWER(@razon_social), ' ', '') + '.com';
    SET @direccion = 'Zona ' + CAST(@i AS VARCHAR) + ', Calle Comercio #' + CAST((@i * 100) AS VARCHAR);
    SET @telefono = '2' + CAST((ABS(CHECKSUM(NEWID())) % 1000000) + 100000 AS VARCHAR(6));

    -- Insertar persona jurídica
    INSERT INTO persona (tipo, email, direccion, telefono, activo)
    VALUES ('Jurídica', @email, @direccion, @telefono, 1);
    
    SET @persona_id = SCOPE_IDENTITY();
    
    INSERT INTO persona_juridica (id, razon_social, nit, encargado_nombre, encargado_cargo)
    VALUES (@persona_id, @razon_social, @nit, 
            'Encargado ' + CAST(@i AS VARCHAR), 'Gerente General');
    
    SET @i = @i + 1;
END;


-- =============================================
-- 4.1. CREAR MASCOTAS PARA LAS PERSONAS FÍSICAS
-- =============================================


-- Solo crear mascotas para personas físicas (no jurídicas)
DECLARE persona_cursor CURSOR FOR 
    SELECT p.id 
    FROM persona p 
    INNER JOIN persona_fisica pf ON p.id = pf.id 
    WHERE p.activo = 1;

OPEN persona_cursor;
DECLARE @persona_fisica_id INT;

FETCH NEXT FROM persona_cursor INTO @persona_fisica_id;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Crear 1-3 mascotas por persona física (70% probabilidad de tener mascota)
    IF (ABS(CHECKSUM(NEWID())) % 100) < 70 
    BEGIN
        DECLARE @num_mascotas INT = (ABS(CHECKSUM(NEWID())) % 3) + 1;
        DECLARE @k INT = 1;
        
        WHILE @k <= @num_mascotas
        BEGIN
            DECLARE @animal_nombre VARCHAR(100), @especie VARCHAR(50), @raza VARCHAR(100);
            DECLARE @fecha_nac DATE, @peso DECIMAL(5,2), @color VARCHAR(50), @genero CHAR(1);
            DECLARE @animal_id INT, @historico_id INT;
            
            -- Generar datos aleatorios para la mascota
            SET @especie = CASE (ABS(CHECKSUM(NEWID())) % 4)
                WHEN 0 THEN 'Perro' WHEN 1 THEN 'Gato' 
                WHEN 2 THEN 'Ave' ELSE 'Conejo'
            END;
            
            SET @animal_nombre = CASE (ABS(CHECKSUM(NEWID())) % 20)
                WHEN 0 THEN 'Max' WHEN 1 THEN 'Bella' WHEN 2 THEN 'Charlie' WHEN 3 THEN 'Luna'
                WHEN 4 THEN 'Rocky' WHEN 5 THEN 'Lola' WHEN 6 THEN 'Buddy' WHEN 7 THEN 'Molly'
                WHEN 8 THEN 'Jack' WHEN 9 THEN 'Sophie' WHEN 10 THEN 'Rex' WHEN 11 THEN 'Coco'
                WHEN 12 THEN 'Duke' WHEN 13 THEN 'Zoe' WHEN 14 THEN 'Bear' WHEN 15 THEN 'Princess'
                WHEN 16 THEN 'Toby' WHEN 17 THEN 'Daisy' WHEN 18 THEN 'Oscar' ELSE 'Ruby'
            END;
            
            SET @raza = CASE @especie
                WHEN 'Perro' THEN CASE (ABS(CHECKSUM(NEWID())) % 8)
                    WHEN 0 THEN 'Labrador' WHEN 1 THEN 'Golden Retriever' WHEN 2 THEN 'Bulldog'
                    WHEN 3 THEN 'Pastor Alemán' WHEN 4 THEN 'Chihuahua' WHEN 5 THEN 'Mestizo'
                    WHEN 6 THEN 'Poodle' ELSE 'Beagle'
                END
                WHEN 'Gato' THEN CASE (ABS(CHECKSUM(NEWID())) % 6)
                    WHEN 0 THEN 'Persa' WHEN 1 THEN 'Siamés' WHEN 2 THEN 'Angora'
                    WHEN 3 THEN 'Maine Coon' WHEN 4 THEN 'Mestizo' ELSE 'Bengalí'
                END
                WHEN 'Ave' THEN CASE (ABS(CHECKSUM(NEWID())) % 4)
                    WHEN 0 THEN 'Canario' WHEN 1 THEN 'Periquito' WHEN 2 THEN 'Loro' ELSE 'Cacatúa'
                END
                ELSE CASE (ABS(CHECKSUM(NEWID())) % 3)
                    WHEN 0 THEN 'Holandés' WHEN 1 THEN 'Angora' ELSE 'Rex'
                END
            END;
            
            SET @genero = CASE ABS(CHECKSUM(NEWID())) % 2 WHEN 0 THEN 'M' ELSE 'F' END;
            SET @color = CASE (ABS(CHECKSUM(NEWID())) % 8)
                WHEN 0 THEN 'Negro' WHEN 1 THEN 'Blanco' WHEN 2 THEN 'Marrón' WHEN 3 THEN 'Gris'
                WHEN 4 THEN 'Dorado' WHEN 5 THEN 'Tricolor' WHEN 6 THEN 'Atigrado' ELSE 'Mixto'
            END;
            
            SET @fecha_nac = DATEADD(YEAR, -(ABS(CHECKSUM(NEWID())) % 12) - 1, GETDATE());
            SET @peso = CASE @especie
                WHEN 'Perro' THEN (ABS(CHECKSUM(NEWID())) % 400) / 10.0 + 5  -- 5-45 kg
                WHEN 'Gato' THEN (ABS(CHECKSUM(NEWID())) % 50) / 10.0 + 2   -- 2-7 kg
                WHEN 'Ave' THEN (ABS(CHECKSUM(NEWID())) % 20) / 10.0 + 0.1  -- 0.1-2 kg
                ELSE (ABS(CHECKSUM(NEWID())) % 30) / 10.0 + 1  -- 1-4 kg
            END;
            
            -- Generar microchip único (opcional) - 30% de los animales tienen microchip
            DECLARE @microchip VARCHAR(50) = NULL;
            IF (ABS(CHECKSUM(NEWID())) % 100) < 30 
            BEGIN
                -- Generar microchip único basado en timestamp y IDs únicos
                SET @microchip = 'MC' + FORMAT(@persona_fisica_id, '0000') + 
                                FORMAT(@k, '00') + 
                                FORMAT(ABS(CHECKSUM(NEWID())) % 10000, '0000') +
                                FORMAT(DATEDIFF(SECOND, '2020-01-01', GETDATE()) % 10000, '0000');
            END;
            
            -- Insertar animal con microchip único o NULL
            INSERT INTO animal (nombre, especie, raza, fecha_nacimiento, peso, color, genero, 
                              esterilizado, microchip, persona_id, activo)
            VALUES (@animal_nombre, @especie, @raza, @fecha_nac, @peso, @color, @genero, 
                    ABS(CHECKSUM(NEWID())) % 2, @microchip, @persona_fisica_id, 1);
            
            SET @animal_id = SCOPE_IDENTITY();
            
            -- Verificar si ya existe un historico para este animal (no deberia, pero por seguridad)
            IF NOT EXISTS (SELECT 1 FROM historico WHERE animal_id = @animal_id)
            BEGIN
                -- Crear histórico médico para el animal
                INSERT INTO historico (animal_id, notas_generales, alergias, condiciones_medicas)
                VALUES (@animal_id, 'Animal saludable', NULL, NULL);
                
                SET @historico_id = SCOPE_IDENTITY();
            END
            ELSE
            BEGIN
                -- Si ya existe, obtener el ID del histórico existente
                SELECT @historico_id = id FROM historico WHERE animal_id = @animal_id;
            END;
            
            SET @k = @k + 1;
        END;
    END;
    
    FETCH NEXT FROM persona_cursor INTO @persona_fisica_id;
END;

CLOSE persona_cursor;
DEALLOCATE persona_cursor;

-- Contar mascotas creadas
DECLARE @total_mascotas INT;
SELECT @total_mascotas = COUNT(*) FROM animal WHERE activo = 1;

-- =============================================
-- 5. CREAR DATOS BASE - PERSONAL VETERINARIO
-- =============================================


INSERT INTO personal (nombre, apellido, email, usuario, contrasena, telefono, direccion, salario, rol, activo) VALUES
('Dr. Carlos', 'Mendoza Silva', 'cmendoza@vet.com', 'cmendoza', 'vet123', '78123456', 'Av. América #123', 8000.00, 'Veterinario', 1),
('Dra. Ana', 'Rodríguez López', 'arodriguez@vet.com', 'arodriguez', 'vet456', '69234567', 'Calle Sucre #456', 9000.00, 'Veterinario', 1),
('Dr. Miguel', 'Vargas Quispe', 'mvargas@vet.com', 'mvargas', 'vet789', '71345678', 'Av. 6 de Agosto #789', 7500.00, 'Veterinario', 1),
('Dra. Patricia', 'Morales Choque', 'pmorales@vet.com', 'pmorales', 'vet321', '75456789', 'Calle Comercio #321', 8500.00, 'Veterinario', 1),
('Dr. Roberto', 'Jiménez Mamani', 'rjimenez@vet.com', 'rjimenez', 'vet654', '68567890', 'Av. Ballivián #654', 7800.00, 'Veterinario', 1);

-- Insertar datos de veterinarios
DECLARE @vet_ids TABLE (id INT, licencia VARCHAR(50), especialidad VARCHAR(100));

DECLARE @id1 INT = (SELECT id FROM personal WHERE usuario = 'cmendoza');
DECLARE @id2 INT = (SELECT id FROM personal WHERE usuario = 'arodriguez');
DECLARE @id3 INT = (SELECT id FROM personal WHERE usuario = 'mvargas');
DECLARE @id4 INT = (SELECT id FROM personal WHERE usuario = 'pmorales');
DECLARE @id5 INT = (SELECT id FROM personal WHERE usuario = 'rjimenez');

INSERT INTO personal_veterinario (id, num_licencia, especialidad, universidad, anios_experiencia) VALUES
(@id1, 'VET001', 'Medicina General', 'Universidad Veterinaria', 5),
(@id2, 'VET002', 'Cirugía', 'Universidad Veterinaria', 8),
(@id3, 'VET003', 'Pequeños Animales', 'Universidad Veterinaria', 3),
(@id4, 'VET004', 'Medicina Interna', 'Universidad Veterinaria', 10),
(@id5, 'VET005', 'Dermatología', 'Universidad Veterinaria', 7);

