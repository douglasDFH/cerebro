-- =============================================
-- SCRIPT PARA INSERTAR PERSONAS JURÍDICAS
-- Sistema Veterinario
-- =============================================

USE SistemaVeterinario;
GO

-- =============================================
-- EJEMPLOS DE PERSONAS JURÍDICAS
-- =============================================

-- Clínicas Veterinarias
EXEC SP_InsertarPersonaJuridica 'A12345678', 'Clínica Veterinaria San Francisco S.L.', 'info@veterinariasf.com', 'Calle Veterinarios 15, 28001 Madrid', '915123456', 'Clínica especializada en animales domésticos con 20 años de experiencia';

EXEC SP_InsertarPersonaJuridica 'G98765432', 'Hospital Veterinario Central S.A.', 'administracion@hospitalvet.com', 'Plaza Mayor 7, 41001 Sevilla', '955678901', 'Hospital veterinario 24 horas con servicio de urgencias y cirugía';

EXEC SP_InsertarPersonaJuridica 'J55443322', 'Clínica Veterinaria Los Robles', 'contacto@vetrobles.com', 'Avenida Los Robles 234, 03001 Alicante', '965789123', 'Especialistas en medicina felina y canina';

-- Refugios y Protectoras
EXEC SP_InsertarPersonaJuridica 'B87654321', 'Refugio de Animales Esperanza', 'contacto@refugioesperanza.org', 'Avenida de la Paz 89, 08015 Barcelona', '934567890', 'Refugio sin ánimo de lucro para animales abandonados';

EXEC SP_InsertarPersonaJuridica 'H11223344', 'Fundación Protectora de Animales', 'info@fundacionprotectora.org', 'Calle Solidaridad 12, 48001 Bilbao', '944567123', 'Fundación dedicada a la protección y cuidado de animales desde 1995';

EXEC SP_InsertarPersonaJuridica 'N99887766', 'Asociación Patitas Felices', 'adopciones@patitasfelices.org', 'Calle Esperanza 88, 18001 Granada', '958123456', 'Asociación de voluntarios para el rescate y adopción de animales';

-- Tiendas de Mascotas
EXEC SP_InsertarPersonaJuridica 'F55667788', 'Tienda de Mascotas PetWorld', 'ventas@petworld.es', 'Calle Comercio 45, 46001 Valencia', '963456789', 'Tienda especializada en productos para mascotas y alimentación';

EXEC SP_InsertarPersonaJuridica 'K77889900', 'Pet Store Central S.L.', 'info@petstorecentral.com', 'Centro Comercial Norte, Local 15, 50001 Zaragoza', '976789012', 'Gran superficie especializada en productos para animales';

-- Criaderos y Granjas
EXEC SP_InsertarPersonaJuridica 'L33445566', 'Criadero Canino Elite', 'info@criaderoelite.com', 'Finca El Paraíso, Km 5, 29600 Marbella', '952345678', 'Criadero especializado en razas puras con certificado oficial';

EXEC SP_InsertarPersonaJuridica 'M22334455', 'Granja Avícola Los Pinos', 'administracion@granjalosinos.com', 'Carretera Rural Km 8, 37001 Salamanca', '923456789', 'Granja avícola con servicio veterinario especializado';

-- Empresas de Servicios Veterinarios
EXEC SP_InsertarPersonaJuridica 'P88776655', 'Servicios Veterinarios Móviles S.L.', 'emergencias@vetmovil.com', 'Polígono Industrial Norte, Nave 12, 33001 Oviedo', '985567890', 'Servicio veterinario a domicilio 24h para grandes animales';

EXEC SP_InsertarPersonaJuridica 'Q44556677', 'Laboratorio Veterinario Diagnóstico', 'laboratorio@vetdiagnostico.com', 'Calle Ciencia 25, 47001 Valladolid', '983678901', 'Laboratorio especializado en análisis clínicos veterinarios';

-- Seguros y Servicios Financieros
EXEC SP_InsertarPersonaJuridica 'R66778899', 'Seguros Veterinarios Protección', 'clientes@segurosvetproteccion.com', 'Gran Vía 100, 28013 Madrid', '917890123', 'Compañía de seguros especializada en cobertura veterinaria';

-- Zoológicos y Parques Naturales
EXEC SP_InsertarPersonaJuridica 'S11224466', 'Parque Zoológico Municipal', 'direccion@zoomunicipal.gov', 'Parque de la Ciudad s/n, 15001 A Coruña', '981234567', 'Zoológico municipal con programa de conservación de especies';

-- Universidades y Centros de Formación
EXEC SP_InsertarPersonaJuridica 'T99887755', 'Universidad de Veterinaria Campus Sur', 'secretaria@univetsur.edu', 'Campus Universitario, Edificio C, 30001 Murcia', '968345678', 'Facultad de Veterinaria con hospital clínico universitario';

-- =============================================
-- CONSULTA PARA VERIFICAR INSERCIONES
-- =============================================

-- Ver todas las personas jurídicas insertadas
SELECT 
    IdPersona,
    CIF,
    Nombre AS NombreComercial,
    RazonSocial,
    Email,
    Direccion,
    Telefono,
    Observaciones,
    FechaRegistro
FROM Persona 
WHERE TipoPersona = 'J'
ORDER BY FechaRegistro DESC;

-- Contar total de personas jurídicas
SELECT COUNT(*) as TotalPersonasJuridicas 
FROM Persona 
WHERE TipoPersona = 'J';