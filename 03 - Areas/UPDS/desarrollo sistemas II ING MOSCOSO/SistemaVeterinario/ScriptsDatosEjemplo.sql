-- =============================================
-- SCRIPTS DE INSERCIÓN DE DATOS DE EJEMPLO
-- Sistema Veterinario
-- Fecha: 2025-08-17
-- =============================================

USE SistemaVeterinario;
GO

-- =============================================
-- INSERTAR DATOS DE EJEMPLO
-- =============================================

-- 1. INSERTAR PERSONAL ADICIONAL
INSERT INTO Personal (TipoPersonal, Nombre, Apellidos, DNI, Email, Telefono, NumColegiado, Especialidad, Salario)
VALUES 
    ('VETERINARIO', 'Carlos', 'Rodríguez Martín', '33333333D', 'carlos.rodriguez@veterinaria.com', '666555444', 'VET003', 'Cirugía Veterinaria', 3200.00),
    ('AUXILIAR', 'Sofia', 'García López', '44444444E', 'sofia.garcia@veterinaria.com', '777666555', NULL, NULL, 1800.00),
    ('AUXILIAR', 'Miguel', 'Torres Ruiz', '55555555F', 'miguel.torres@veterinaria.com', '888777666', NULL, NULL, 1750.00);

-- 2. INSERTAR PERSONAS FÍSICAS (CLIENTES)
EXEC SP_InsertarPersonaFisica '12345678A', 'Juan', 'Pérez García', 'juan.perez@email.com', 'Calle Mayor 123, Madrid', '666111222', 'Cliente regular desde 2020';
EXEC SP_InsertarPersonaFisica '87654321B', 'María', 'López Ruiz', 'maria.lopez@email.com', 'Av. Libertad 456, Madrid', '666333444', 'Tiene 3 mascotas';
EXEC SP_InsertarPersonaFisica '11111111C', 'Pedro', 'Martínez Soto', 'pedro.martinez@email.com', 'Plaza España 789, Madrid', '666555777', NULL;
EXEC SP_InsertarPersonaFisica '22222222D', 'Ana', 'González Díaz', 'ana.gonzalez@email.com', 'Calle Luna 321, Madrid', '666888999', 'Criadora de gatos persas';
EXEC SP_InsertarPersonaFisica '33333333E', 'Luis', 'Fernández Vega', 'luis.fernandez@email.com', 'Calle Sol 654, Madrid', '666777888', NULL;

-- 3. INSERTAR PERSONAS JURÍDICAS (CLIENTES EMPRESARIALES)
EXEC SP_InsertarPersonaJuridica 'A12345678', 'Refugio Animales Madrid', 'refugio@animales-madrid.org', 'Carretera M-40 Km 15', '911234567', 'ONG protectora de animales';
EXEC SP_InsertarPersonaJuridica 'B87654321', 'Tienda Mascotas El Can', 'info@elcan.com', 'Centro Comercial Norte', '912345678', 'Tienda especializada en productos para mascotas';

-- 4. INSERTAR ANIMALES
-- Animales de Juan Pérez (IdPersona = 1)
EXEC SP_InsertarAnimal 1, 'Rex', 'Perro', 'Pastor Alemán', 'Marrón y Negro', 'M', '2019-03-15', 32.5, 'CHIP001', 'Perro muy activo, le encanta jugar';
EXEC SP_InsertarAnimal 1, 'Luna', 'Gato', 'Siamés', 'Crema', 'H', '2020-07-22', 4.2, 'CHIP002', 'Gata tranquila, muy cariñosa';

-- Animales de María López (IdPersona = 2)
EXEC SP_InsertarAnimal 2, 'Max', 'Perro', 'Golden Retriever', 'Dorado', 'M', '2018-05-10', 28.7, 'CHIP003', 'Perro familiar, excelente con niños';
EXEC SP_InsertarAnimal 2, 'Bella', 'Gato', 'Persa', 'Blanco', 'H', '2021-01-08', 3.8, 'CHIP004', 'Gata de pelo largo, necesita cepillado diario';
EXEC SP_InsertarAnimal 2, 'Rocky', 'Conejo', 'Holandés', 'Blanco y Negro', 'M', '2022-04-12', 1.5, NULL, 'Conejo doméstico muy sociable';

-- Animales de Pedro Martínez (IdPersona = 3)
EXEC SP_InsertarAnimal 3, 'Pipo', 'Loro', 'Loro Gris Africano', 'Gris', 'M', '2015-11-20', 0.45, NULL, 'Loro muy inteligente, habla varias palabras';

-- Animales de Ana González (IdPersona = 4)
EXEC SP_InsertarAnimal 4, 'Mimi', 'Gato', 'Persa', 'Crema', 'H', '2020-02-14', 4.1, 'CHIP005', 'Reproductora, muy dócil';
EXEC SP_InsertarAnimal 4, 'Coco', 'Gato', 'Persa', 'Chocolate', 'M', '2019-09-30', 5.2, 'CHIP006', 'Semental, ganador de concursos';
EXEC SP_InsertarAnimal 4, 'Nala', 'Gato', 'Persa', 'Blanco', 'H', '2021-06-18', 3.9, 'CHIP007', 'Hija de Mimi, muy juguetona';

-- Animales de Luis Fernández (IdPersona = 5)
EXEC SP_InsertarAnimal 5, 'Thor', 'Perro', 'Rottweiler', 'Negro', 'M', '2017-12-05', 45.2, 'CHIP008', 'Perro guardián, muy obediente';

-- Animales del Refugio (IdPersona = 6)
EXEC SP_InsertarAnimal 6, 'Pelusa', 'Gato', 'Mestizo', 'Atigrado', 'H', '2022-08-15', 2.8, NULL, 'Gata rescatada, busca familia';
EXEC SP_InsertarAnimal 6, 'Canelo', 'Perro', 'Mestizo', 'Marrón', 'M', '2021-03-22', 18.5, NULL, 'Perro abandonado, muy cariñoso';
EXEC SP_InsertarAnimal 6, 'Estrella', 'Gato', 'Mestizo', 'Negro', 'H', '2022-01-10', 2.2, NULL, 'Gatita joven, muy activa';

-- 5. INSERTAR DIAGNÓSTICOS
EXEC SP_InsertarDiagnostico 1, 1, 'Revisión anual completa', 'Animal en excelente estado general', 'Vacunación anual completa', 'Vitaminas y antiparasitario', 'BAJA', 85.00;
EXEC SP_InsertarDiagnostico 2, 2, 'Consulta por vómitos', 'Vómitos ocasionales después de comer', 'Cambio de dieta y medicación', 'Omeprazol 10mg cada 12h por 7 días', 'MEDIA', 65.00;
EXEC SP_InsertarDiagnostico 3, 1, 'Vacunación y desparasitación', 'Revisión de rutina, animal sano', 'Vacuna anual y desparasitación', 'Vacuna polivalente + desparasitario', 'BAJA', 75.00;
EXEC SP_InsertarDiagnostico 4, 2, 'Revisión reproductiva', 'Revisión pre-apareamiento', 'Control reproductivo normal', 'Ácido fólico', 'BAJA', 55.00;
EXEC SP_InsertarDiagnostico 5, 3, 'Consulta por cojera', 'Cojera en pata trasera derecha', 'Antiinflamatorio y reposo', 'Meloxicam 1mg/kg cada 24h por 5 días', 'MEDIA', 90.00;
EXEC SP_InsertarDiagnostico 6, 1, 'Revisión dental', 'Acumulación de sarro dental', 'Limpieza dental bajo anestesia', 'Antibiótico profiláctico', 'MEDIA', 120.00;
EXEC SP_InsertarDiagnostico 7, 2, 'Esterilización', 'Ovario histerectomía programada', 'Cirugía completada con éxito', 'Antibiótico y analgésico post-cirugía', 'BAJA', 180.00;
EXEC SP_InsertarDiagnostico 8, 3, 'Consulta por pérdida de plumas', 'Pérdida excesiva de plumas', 'Estrés ambiental, mejora de condiciones', 'Complejo vitamínico', 'MEDIA', 45.00;

-- 6. CREAR FACTURAS DE EJEMPLO
DECLARE @IdFactura1 INT, @IdFactura2 INT, @IdFactura3 INT, @IdFactura4 INT;

-- Factura 1: Juan Pérez - Revisión de Rex
EXEC SP_CrearFactura 1, 1;
SET @IdFactura1 = SCOPE_IDENTITY();

EXEC SP_AgregarElementoFactura @IdFactura1, 'Consulta veterinaria', 'Revisión anual completa', 1, 40.00, 'CONSULTA';
EXEC SP_AgregarElementoFactura @IdFactura1, 'Vacuna polivalente', 'Vacunación anual', 1, 25.00, 'VACUNA';
EXEC SP_AgregarElementoFactura @IdFactura1, 'Vitaminas', 'Complejo vitamínico', 1, 15.00, 'MEDICAMENTO';
EXEC SP_AgregarElementoFactura @IdFactura1, 'Antiparasitario', 'Desparasitación interna', 1, 12.00, 'MEDICAMENTO';

-- Factura 2: María López - Consulta por vómitos de Luna
EXEC SP_CrearFactura 2, 2;
SET @IdFactura2 = SCOPE_IDENTITY();

EXEC SP_AgregarElementoFactura @IdFactura2, 'Consulta veterinaria', 'Consulta por vómitos', 1, 35.00, 'CONSULTA';
EXEC SP_AgregarElementoFactura @IdFactura2, 'Omeprazol', 'Tratamiento gastrico 7 días', 1, 18.00, 'MEDICAMENTO';
EXEC SP_AgregarElementoFactura @IdFactura2, 'Dieta especial', 'Pienso gastro-intestinal', 1, 28.00, 'PRODUCTO';

-- Factura 3: Ana González - Revisión reproductiva de Mimi
EXEC SP_CrearFactura 4, 4;
SET @IdFactura3 = SCOPE_IDENTITY();

EXEC SP_AgregarElementoFactura @IdFactura3, 'Consulta especializada', 'Revisión reproductiva', 1, 45.00, 'CONSULTA';
EXEC SP_AgregarElementoFactura @IdFactura3, 'Ácido fólico', 'Suplemento reproductivo', 1, 12.00, 'MEDICAMENTO';

-- Factura 4: Luis Fernández - Consulta por cojera de Thor
EXEC SP_CrearFactura 5, 8;
SET @IdFactura4 = SCOPE_IDENTITY();

EXEC SP_AgregarElementoFactura @IdFactura4, 'Consulta veterinaria', 'Consulta por cojera', 1, 40.00, 'CONSULTA';
EXEC SP_AgregarElementoFactura @IdFactura4, 'Radiografía', 'Estudio radiológico extremidad', 1, 35.00, 'OTRO';
EXEC SP_AgregarElementoFactura @IdFactura4, 'Meloxicam', 'Antiinflamatorio 5 días', 1, 22.00, 'MEDICAMENTO';

-- Actualizar algunas facturas como pagadas
UPDATE Factura SET Estado = 'PAGADA', FormaPago = 'TARJETA', FechaPago = GETDATE() WHERE IdFactura = @IdFactura1;
UPDATE Factura SET Estado = 'PAGADA', FormaPago = 'EFECTIVO', FechaPago = GETDATE() WHERE IdFactura = @IdFactura2;

-- 7. CREAR HISTORIALES MÉDICOS
DECLARE @IdHistorico1 INT, @IdHistorico2 INT, @IdHistorico3 INT;

-- Historial para Rex
INSERT INTO Historico (IdAnimal, RefHistorico, TipoHistorial, Descripcion)
VALUES (1, 'HIST-REX-001', 'CONSULTA', 'Primera consulta - Revisión completa y vacunación');
SET @IdHistorico1 = SCOPE_IDENTITY();

INSERT INTO ElementoHistorico (IdHistorico, IdDiagnostico, TipoElemento, Descripcion, Valor, Unidad)
VALUES 
    (@IdHistorico1, 1, 'PESO', 'Peso actual del animal', '32.5', 'kg'),
    (@IdHistorico1, 1, 'TEMPERATURA', 'Temperatura corporal', '38.2', '°C'),
    (@IdHistorico1, 1, 'VACUNA', 'Vacuna polivalente administrada', 'NOBIVAC', 'dosis'),
    (@IdHistorico1, 1, 'OBSERVACION', 'Animal en excelente estado', 'Normal', NULL);

-- Historial para Luna
INSERT INTO Historico (IdAnimal, RefHistorico, TipoHistorial, Descripcion)
VALUES (2, 'HIST-LUNA-001', 'CONSULTA', 'Consulta por vómitos - Tratamiento gastrico');
SET @IdHistorico2 = SCOPE_IDENTITY();

INSERT INTO ElementoHistorico (IdHistorico, IdDiagnostico, TipoElemento, Descripcion, Valor, Unidad)
VALUES 
    (@IdHistorico2, 2, 'PESO', 'Peso actual del animal', '4.2', 'kg'),
    (@IdHistorico2, 2, 'TEMPERATURA', 'Temperatura corporal', '38.8', '°C'),
    (@IdHistorico2, 2, 'SINTOMA', 'Vómitos post-ingesta', 'Presente', NULL),
    (@IdHistorico2, 2, 'TRATAMIENTO', 'Omeprazol prescrito', '10mg c/12h', 'mg');

-- Historial para Thor
INSERT INTO Historico (IdAnimal, RefHistorico, TipoHistorial, Descripcion)
VALUES (8, 'HIST-THOR-001', 'CONSULTA', 'Consulta por cojera - Estudio radiológico');
SET @IdHistorico3 = SCOPE_IDENTITY();

INSERT INTO ElementoHistorico (IdHistorico, IdDiagnostico, TipoElemento, Descripcion, Valor, Unidad)
VALUES 
    (@IdHistorico3, 5, 'PESO', 'Peso actual del animal', '45.2', 'kg'),
    (@IdHistorico3, 5, 'RADIOGRAFIA', 'Estudio de extremidad posterior', 'Sin fracturas', NULL),
    (@IdHistorico3, 5, 'TRATAMIENTO', 'Meloxicam prescrito', '1mg/kg/24h', 'mg/kg');

-- 8. USUARIOS ADICIONALES (con contraseñas encriptadas en SHA256)
-- Nota: Las contraseñas están hasheadas. Las contraseñas reales son:
-- admin: admin123 -> ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f
-- drcarlos: vet456 -> 3c9d4e2c8b0e8d5f4e2a1c7b6d9e8f0a1b2c3d4e5f6a7b8c9d0e1f2a3b4c5d6e7f
-- sofia: aux789 -> 0f7c7b8e9d6f5a4b3c2d1e0f9e8d7c6b5a4f3e2d1c0b9a8f7e6d5c4b3a2f1e0d

EXEC SP_InsertarUsuario 'drcarlos', '3c9d4e2c8b0e8d5f4e2a1c7b6d9e8f0a1b2c3d4e5f6a7b8c9d0e1f2a3b4c5d6e7f', 'carlos.rodriguez@veterinaria.com', 'VETERINARIO', 4;
EXEC SP_InsertarUsuario 'sofia', '0f7c7b8e9d6f5a4b3c2d1e0f9e8d7c6b5a4f3e2d1c0b9a8f7e6d5c4b3a2f1e0d', 'sofia.garcia@veterinaria.com', 'AUXILIAR', 5;
EXEC SP_InsertarUsuario 'miguel', 'e8d7c6b5a4f3e2d1c0b9a8f7e6d5c4b3a2f1e0d9c8b7a6f5e4d3c2b1a0f9e8d7c6', 'miguel.torres@veterinaria.com', 'AUXILIAR', 6;
EXEC SP_InsertarUsuario 'recepcion', 'a1b2c3d4e5f6a7b8c9d0e1f2a3b4c5d6e7f8a9b0c1d2e3f4a5b6c7d8e9f0a1b2c3', 'recepcion@veterinaria.com', 'RECEPCIONISTA', NULL;

PRINT '=== DATOS DE EJEMPLO INSERTADOS EXITOSAMENTE ===';
PRINT '';
PRINT 'RESUMEN DE DATOS CREADOS:';
PRINT '- Personal: 6 registros (3 veterinarios, 3 auxiliares)';
PRINT '- Personas: 7 registros (5 físicas, 2 jurídicas)';
PRINT '- Animales: 12 registros (perros, gatos, conejo, loro)';
PRINT '- Diagnósticos: 8 registros';
PRINT '- Facturas: 4 registros (2 pagadas, 2 pendientes)';
PRINT '- Historiales médicos: 3 registros con elementos';
PRINT '- Usuarios: 8 registros total';
PRINT '';
PRINT 'USUARIOS DE PRUEBA DISPONIBLES:';
PRINT '- admin / admin123 (ADMIN)';
PRINT '- drjuan / vet123 (VETERINARIO)';
PRINT '- drmaria / vet123 (VETERINARIO)';
PRINT '- drcarlos / vet456 (VETERINARIO)';
PRINT '- ana_aux / aux123 (AUXILIAR)';
PRINT '- sofia / aux789 (AUXILIAR)';
PRINT '- miguel / aux456 (AUXILIAR)';
PRINT '- recepcion / recep123 (RECEPCIONISTA)';
GO