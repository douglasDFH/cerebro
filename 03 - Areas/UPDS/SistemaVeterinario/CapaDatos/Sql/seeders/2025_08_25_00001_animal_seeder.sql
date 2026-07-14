-- ========================================
-- Seeder para tabla Animal (Mascotas)
-- Fecha: 2025-08-25
-- Descripción: 120 registros de prueba para mascotas
-- ========================================

-- Insertar 120 mascotas de diferentes especies y razas
INSERT INTO animal (nombre, especie, raza, fecha_nacimiento, peso, color, genero, esterilizado, microchip, activo, persona_id) VALUES

-- ============ PERROS (40 registros) ============
-- Perros grandes
('Max', 'Perro', 'Labrador', '2020-03-15', 25.5, 'Dorado', 'M', 1, 'MC001234567890', 1, 1),
('Bella', 'Perro', 'Golden Retriever', '2019-07-22', 28.0, 'Dorado', 'F', 1, 'MC001234567891', 1, 2),
('Rocky', 'Perro', 'Pastor Alemán', '2021-01-10', 32.0, 'Negro y Marrón', 'M', 0, 'MC001234567892', 1, 3),
('Duke', 'Perro', 'Rottweiler', '2020-09-14', 38.0, 'Negro', 'M', 1, 'MC001234567893', 1, 4),
('Atlas', 'Perro', 'Gran Danés', '2019-12-03', 45.0, 'Arlequín', 'M', 1, 'MC001234567894', 1, 5),
('Zeus', 'Perro', 'San Bernardo', '2020-06-18', 42.0, 'Marrón y Blanco', 'M', 0, 'MC001234567895', 1, 6),
('Luna', 'Perro', 'Husky Siberiano', '2021-04-25', 23.0, 'Gris y Blanco', 'F', 1, 'MC001234567896', 1, 7),
('Sasha', 'Perro', 'Border Collie', '2020-11-08', 20.0, 'Negro y Blanco', 'F', 1, 'MC001234567897', 1, 8),

-- Perros medianos
('Charlie', 'Perro', 'Bulldog Francés', '2020-11-30', 12.0, 'Atigrado', 'M', 1, 'MC001234567898', 1, 9),
('Milo', 'Perro', 'Cocker Spaniel', '2021-08-12', 15.0, 'Dorado', 'M', 1, 'MC001234567899', 1, 10),
('Lola', 'Perro', 'Beagle', '2022-02-28', 13.0, 'Tricolor', 'F', 0, 'MC001234567900', 1, 1),
('Buddy', 'Perro', 'Bulldog Inglés', '2019-10-15', 18.0, 'Blanco', 'M', 1, 'MC001234567901', 1, 2),
('Daisy', 'Perro', 'Boxer', '2020-05-20', 22.0, 'Atigrado', 'F', 1, 'MC001234567902', 1, 3),
('Cooper', 'Perro', 'Springer Spaniel', '2021-12-07', 16.0, 'Hígado y Blanco', 'M', 0, 'MC001234567903', 1, 4),
('Zoe', 'Perro', 'Pointer', '2020-07-13', 19.0, 'Blanco y Naranja', 'F', 1, 'MC001234567904', 1, 5),
('Rex', 'Perro', 'Setter Irlandés', '2019-09-05', 24.0, 'Castaño', 'M', 1, 'MC001234567905', 1, 6),

-- Perros pequeños
('Luna', 'Perro', 'Chihuahua', '2022-05-08', 2.5, 'Blanco', 'F', 1, 'MC001234567906', 1, 7),
('Peanut', 'Perro', 'Yorkshire Terrier', '2021-11-22', 3.2, 'Dorado y Negro', 'M', 0, 'MC001234567907', 1, 8),
('Princess', 'Perro', 'Maltés', '2020-04-17', 2.8, 'Blanco', 'F', 1, 'MC001234567908', 1, 9),
('Gizmo', 'Perro', 'Pomerania', '2021-07-01', 3.5, 'Naranja', 'M', 1, 'MC001234567909', 1, 10),
('Coco', 'Perro', 'Shih Tzu', '2020-12-14', 4.2, 'Dorado y Blanco', 'F', 1, 'MC001234567910', 1, 1),
('Bingo', 'Perro', 'Jack Russell', '2021-03-30', 5.5, 'Blanco y Marrón', 'M', 0, 'MC001234567911', 1, 2),
('Nala', 'Perro', 'Cavalier King Charles', '2020-08-25', 6.0, 'Tricolor', 'F', 1, 'MC001234567912', 1, 3),
('Oscar', 'Perro', 'Dachshund', '2021-06-18', 7.2, 'Negro y Fuego', 'M', 1, 'MC001234567913', 1, 4),

-- Más perros diversos
('Kira', 'Perro', 'Akita', '2019-11-12', 26.0, 'Blanco', 'F', 1, 'MC001234567914', 1, 5),
('Thor', 'Perro', 'Doberman', '2020-01-28', 30.0, 'Negro y Fuego', 'M', 1, 'MC001234567915', 1, 6),
('Maya', 'Perro', 'Weimaraner', '2021-09-15', 21.0, 'Plateado', 'F', 0, 'MC001234567916', 1, 7),
('Bruno', 'Perro', 'Mastín Español', '2019-05-03', 55.0, 'Leonado', 'M', 1, 'MC001234567917', 1, 8),
('Amber', 'Perro', 'Vizsla', '2020-10-20', 18.0, 'Dorado Rojizo', 'F', 1, 'MC001234567918', 1, 9),
('Rocco', 'Perro', 'Pitbull', '2021-02-12', 27.0, 'Blanco y Gris', 'M', 0, 'MC001234567919', 1, 10),
('Lily', 'Perro', 'Caniche', '2020-06-07', 8.0, 'Negro', 'F', 1, 'MC001234567920', 1, 1),
('Diesel', 'Perro', 'Bullmastiff', '2019-08-14', 48.0, 'Atigrado', 'M', 1, 'MC001234567921', 1, 2),
('Sophie', 'Perro', 'Bichón Frisé', '2021-12-25', 4.8, 'Blanco', 'F', 1, 'MC001234567922', 1, 3),
('Tyson', 'Perro', 'Cane Corso', '2020-03-09', 41.0, 'Negro', 'M', 0, 'MC001234567923', 1, 4),
('Ruby', 'Perro', 'Basenji', '2021-05-16', 9.5, 'Castaño y Blanco', 'F', 1, 'MC001234567924', 1, 5),
('Storm', 'Perro', 'Pastor Belga', '2020-07-28', 25.0, 'Negro', 'M', 1, 'MC001234567925', 1, 6),
('Mia', 'Perro', 'Galgo', '2019-12-11', 22.0, 'Blanco y Marrón', 'F', 1, 'MC001234567926', 1, 7),
('Oreo', 'Perro', 'Boston Terrier', '2021-10-04', 6.8, 'Negro y Blanco', 'M', 0, 'MC001234567927', 1, 8),
('Stella', 'Perro', 'Schnauzer', '2020-04-21', 11.0, 'Sal y Pimienta', 'F', 1, 'MC001234567928', 1, 9),
('Bandit', 'Perro', 'Australian Shepherd', '2021-01-18', 23.0, 'Azul Mirlo', 'M', 1, 'MC001234567929', 1, 10),

-- ============ GATOS (30 registros) ============
('Misu', 'Gato', 'Persa', '2021-04-20', 4.2, 'Gris', 'F', 1, 'MC002234567895', 1, 1),
('Simba', 'Gato', 'Maine Coon', '2019-12-05', 6.8, 'Naranja', 'M', 1, 'MC002234567896', 1, 2),
('Nala', 'Gato', 'Siamés', '2022-02-14', 3.5, 'Crema', 'F', 1, 'MC002234567897', 1, 3),
('Garfield', 'Gato', 'Común Europeo', '2020-08-18', 5.0, 'Naranja y Blanco', 'M', 0, 'MC002234567898', 1, 4),
('Mittens', 'Gato', 'Bengalí', '2021-10-12', 4.8, 'Leopardo', 'F', 1, 'MC002234567899', 1, 5),
('Shadow', 'Gato', 'Bombay', '2020-06-30', 4.0, 'Negro', 'M', 1, 'MC002234567900', 1, 6),
('Luna', 'Gato', 'Ragdoll', '2021-03-17', 5.5, 'Colorpoint', 'F', 1, 'MC002234567901', 1, 7),
('Felix', 'Gato', 'British Shorthair', '2019-11-08', 5.8, 'Azul', 'M', 0, 'MC002234567902', 1, 8),
('Whiskers', 'Gato', 'Scottish Fold', '2020-09-22', 4.3, 'Plateado', 'M', 1, 'MC002234567903', 1, 9),
('Princess', 'Gato', 'Angora Turco', '2021-07-05', 3.8, 'Blanco', 'F', 1, 'MC002234567904', 1, 10),
('Tiger', 'Gato', 'Tabby', '2020-12-19', 4.5, 'Atigrado', 'M', 0, 'MC002234567905', 1, 1),
('Chloe', 'Gato', 'Russian Blue', '2021-05-28', 3.9, 'Azul', 'F', 1, 'MC002234567906', 1, 2),
('Oliver', 'Gato', 'Manx', '2020-02-11', 4.7, 'Marrón', 'M', 1, 'MC002234567907', 1, 3),
('Bella', 'Gato', 'Abisinio', '2021-08-14', 3.2, 'Ruddy', 'F', 0, 'MC002234567908', 1, 4),
('Smokey', 'Gato', 'Chartreux', '2019-10-27', 5.2, 'Gris Azulado', 'M', 1, 'MC002234567909', 1, 5),
('Zara', 'Gato', 'Oriental Shorthair', '2020-04-15', 3.1, 'Negro', 'F', 1, 'MC002234567910', 1, 6),
('Max', 'Gato', 'Sphynx', '2021-11-20', 3.6, 'Rosa', 'M', 1, 'MC002234567911', 1, 7),
('Jade', 'Gato', 'Burmés', '2020-07-03', 4.1, 'Champagne', 'F', 0, 'MC002234567912', 1, 8),
('Romeo', 'Gato', 'Devon Rex', '2021-01-09', 2.8, 'Chocolate', 'M', 1, 'MC002234567913', 1, 9),
('Cleo', 'Gato', 'Egipcio Mau', '2020-05-26', 3.7, 'Plateado', 'F', 1, 'MC002234567914', 1, 10),
('Milo', 'Gato', 'Tonkinés', '2021-09-12', 4.0, 'Natural Mink', 'M', 0, 'MC002234567915', 1, 1),
('Lola', 'Gato', 'Cornish Rex', '2020-11-07', 3.0, 'Blanco', 'F', 1, 'MC002234567916', 1, 2),
('Jasper', 'Gato', 'Selkirk Rex', '2021-06-23', 4.6, 'Crema', 'M', 1, 'MC002234567917', 1, 3),
('Luna', 'Gato', 'Himalayo', '2019-08-16', 5.1, 'Colorpoint', 'F', 1, 'MC002234567918', 1, 4),
('Oscar', 'Gato', 'Savannah', '2020-10-01', 7.5, 'Dorado Manchado', 'M', 0, 'MC002234567919', 1, 5),
('Misty', 'Gato', 'LaPerm', '2021-04-08', 3.4, 'Tortoiseshell', 'F', 1, 'MC002234567920', 1, 6),
('Bruno', 'Gato', 'Ragamuffin', '2020-01-24', 6.2, 'Bicolor', 'M', 1, 'MC002234567921', 1, 7),
('Sasha', 'Gato', 'Balinés', '2021-12-10', 3.3, 'Seal Point', 'F', 0, 'MC002234567922', 1, 8),
('Gizmo', 'Gato', 'Munchkin', '2020-03-29', 2.9, 'Calico', 'M', 1, 'MC002234567923', 1, 9),
('Neko', 'Gato', 'Bobtail Japonés', '2021-07-17', 3.8, 'Mi-ke', 'F', 1, 'MC002234567924', 1, 10),

-- ============ AVES (20 registros) ============
('Polly', 'Ave', 'Loro Amazonas', '2018-06-10', 0.45, 'Verde', 'F', 0, 'MC004234567001', 1, 1),
('Tweety', 'Ave', 'Canario', '2022-07-08', 0.025, 'Amarillo', 'M', 0, 'MC004234567002', 1, 2),
('Rio', 'Ave', 'Guacamayo', '2017-03-22', 1.2, 'Azul y Amarillo', 'M', 0, 'MC004234567003', 1, 3),
('Coco', 'Ave', 'Cacatúa', '2019-11-15', 0.6, 'Blanco', 'F', 0, 'MC004234567004', 1, 4),
('Kiwi', 'Ave', 'Periquito', '2021-09-03', 0.035, 'Verde y Azul', 'M', 0, 'MC004234567005', 1, 5),
('Sunny', 'Ave', 'Conure Solar', '2020-05-18', 0.12, 'Naranja', 'F', 0, 'MC004234567006', 1, 6),
('Charlie', 'Ave', 'Gris Africano', '2016-12-07', 0.5, 'Gris', 'M', 0, 'MC004234567007', 1, 7),
('Ruby', 'Ave', 'Cardenal', '2022-04-12', 0.045, 'Rojo', 'F', 0, 'MC004234567008', 1, 8),
('Pepe', 'Ave', 'Agapornis', '2021-08-25', 0.055, 'Verde y Rosa', 'M', 0, 'MC004234567009', 1, 9),
('Sky', 'Ave', 'Ninfa', '2020-10-30', 0.09, 'Gris y Amarillo', 'F', 0, 'MC004234567010', 1, 10),
('Phoenix', 'Ave', 'Fénix Dorado', '2019-02-14', 0.08, 'Dorado', 'M', 0, 'MC004234567011', 1, 1),
('Luna', 'Ave', 'Diamante Mandarín', '2022-01-20', 0.012, 'Blanco y Negro', 'F', 0, 'MC004234567012', 1, 2),
('Echo', 'Ave', 'Loro Eclectus', '2018-09-11', 0.4, 'Verde', 'M', 0, 'MC004234567013', 1, 3),
('Pearl', 'Ave', 'Canario Belga', '2021-06-05', 0.02, 'Blanco', 'F', 0, 'MC004234567014', 1, 4),
('Mango', 'Ave', 'Loro de Meyer', '2020-03-28', 0.15, 'Verde y Azul', 'M', 0, 'MC004234567015', 1, 5),
('Stella', 'Ave', 'Jilguero', '2022-11-02', 0.015, 'Amarillo y Negro', 'F', 0, 'MC004234567016', 1, 6),
('Captain', 'Ave', 'Loro Pirata', '2017-07-19', 0.35, 'Verde y Rojo', 'M', 0, 'MC004234567017', 1, 7),
('Iris', 'Ave', 'Arcoíris Lorikeet', '2021-12-16', 0.13, 'Multicolor', 'F', 0, 'MC004234567018', 1, 8),
('Bolt', 'Ave', 'Halcón Peregrino', '2019-04-07', 0.75, 'Gris y Blanco', 'M', 0, 'MC004234567019', 1, 9),
('Angel', 'Ave', 'Paloma Blanca', '2020-08-23', 0.25, 'Blanco', 'F', 0, 'MC004234567020', 1, 10),

-- ============ CONEJOS (10 registros) ============
('Bunny', 'Conejo', 'Holandés', '2022-03-25', 1.8, 'Blanco y Negro', 'F', 1, 'MC003234567900', 1, 1),
('Coco', 'Conejo', 'Angora', '2021-09-30', 2.2, 'Blanco', 'M', 0, 'MC003234567901', 1, 2),
('Hoppy', 'Conejo', 'Rex', '2020-11-14', 1.9, 'Chinchilla', 'F', 1, 'MC003234567902', 1, 3),
('Cotton', 'Conejo', 'Lop Inglés', '2021-05-07', 2.5, 'Gris', 'M', 1, 'MC003234567903', 1, 4),
('Clover', 'Conejo', 'Cabeza de León', '2022-01-19', 1.6, 'Dorado', 'F', 0, 'MC003234567904', 1, 5),
('Nibbles', 'Conejo', 'Mini Lop', '2021-07-28', 1.4, 'Marrón', 'M', 1, 'MC003234567905', 1, 6),
('Daisy', 'Conejo', 'Gigante Flandes', '2020-04-03', 4.2, 'Gris Acero', 'F', 1, 'MC003234567906', 1, 7),
('Pepper', 'Conejo', 'Nueva Zelanda', '2021-10-15', 3.1, 'Blanco', 'M', 0, 'MC003234567907', 1, 8),
('Snowball', 'Conejo', 'Californiano', '2022-08-22', 2.8, 'Blanco y Negro', 'F', 1, 'MC003234567908', 1, 9),
('Oreo', 'Conejo', 'Holandés Enano', '2021-02-11', 1.1, 'Negro y Blanco', 'M', 1, 'MC003234567909', 1, 10),

-- ============ ROEDORES (8 registros) ============
('Peanut', 'Hámster', 'Dorado', '2023-01-15', 0.15, 'Dorado', 'M', 0, 'MC005234567001', 1, 1),
('Nibbles', 'Hámster', 'Ruso', '2023-02-20', 0.12, 'Gris', 'F', 0, 'MC005234567002', 1, 2),
('Squeaky', 'Hámster', 'Chino', '2022-12-05', 0.08, 'Gris y Blanco', 'M', 0, 'MC005234567003', 1, 3),
('Fluffy', 'Cobaya', 'Peruana', '2022-11-10', 0.9, 'Tricolor', 'F', 1, 'MC005234567004', 1, 4),
('Patches', 'Cobaya', 'Americana', '2021-08-17', 1.1, 'Blanco y Marrón', 'M', 0, 'MC005234567005', 1, 5),
('Ginger', 'Cobaya', 'Abisinia', '2022-06-03', 0.85, 'Rojizo', 'F', 1, 'MC005234567006', 1, 6),
('Chip', 'Chinchilla', 'Estándar', '2021-04-28', 0.6, 'Gris', 'M', 0, 'MC005234567007', 1, 7),
('Dusty', 'Chinchilla', 'Beige', '2022-09-14', 0.55, 'Beige', 'F', 0, 'MC005234567008', 1, 8),

-- ============ REPTILES (8 registros) ============
('Spike', 'Reptil', 'Iguana Verde', '2019-04-15', 2.8, 'Verde', 'M', 0, 'MC006234567001', 1, 1),
('Shelly', 'Reptil', 'Tortuga Rusa', '2017-05-20', 0.8, 'Marrón', 'F', 0, 'MC006234567002', 1, 2),
('Rex', 'Reptil', 'Dragón Barbudo', '2020-08-12', 0.4, 'Marrón y Amarillo', 'M', 0, 'MC006234567003', 1, 3),
('Slinky', 'Reptil', 'Pitón Bola', '2018-11-30', 1.5, 'Marrón y Negro', 'F', 0, 'MC006234567004', 1, 4),
('Gecko', 'Reptil', 'Gecko Leopardo', '2021-07-18', 0.06, 'Amarillo y Negro', 'M', 0, 'MC006234567005', 1, 5),
('Emerald', 'Reptil', 'Camaleón', '2020-02-25', 0.15, 'Verde', 'F', 0, 'MC006234567006', 1, 6),
('Slider', 'Reptil', 'Tortuga de Orejas Rojas', '2019-09-07', 1.2, 'Verde y Amarillo', 'M', 0, 'MC006234567007', 1, 7),
('Scales', 'Reptil', 'Monitor de Sabana', '2018-06-14', 3.5, 'Gris y Negro', 'F', 0, 'MC006234567008', 1, 8),

-- ============ PECES (4 registros) ============
('Nemo', 'Pez', 'Pez Payaso', '2023-03-01', 0.05, 'Naranja y Blanco', 'M', 0, 'MC007234567001', 1, 1),
('Dory', 'Pez', 'Tang Azul', '2023-03-01', 0.08, 'Azul', 'F', 0, 'MC007234567002', 1, 2),
('Goldie', 'Pez', 'Goldfish', '2022-11-20', 0.12, 'Dorado', 'F', 0, 'MC007234567003', 1, 3),
('Bubbles', 'Pez', 'Betta', '2023-01-10', 0.03, 'Azul y Rojo', 'M', 0, 'MC007234567004', 1, 4);

-- Verificar la inserción
SELECT COUNT(*) as 'Total de mascotas insertadas' FROM animal;

-- Mostrar estadísticas por especie
SELECT 
    especie,
    COUNT(*) as cantidad,
    ROUND(AVG(peso), 2) as peso_promedio_kg,
    COUNT(CASE WHEN genero = 'M' THEN 1 END) as machos,
    COUNT(CASE WHEN genero = 'F' THEN 1 END) as hembras,
    COUNT(CASE WHEN esterilizado = 1 THEN 1 END) as esterilizados
FROM animal 
GROUP BY especie
ORDER BY cantidad DESC;

-- Estadísticas por género
SELECT 
    genero,
    COUNT(*) as cantidad,
    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM animal), 1) as porcentaje
FROM animal 
GROUP BY genero;

-- Top 10 razas más comunes
SELECT TOP 10
    raza,
    especie,
    COUNT(*) as cantidad
FROM animal
GROUP BY raza, especie
ORDER BY cantidad DESC;

PRINT 'Seeder de 120 animales completado exitosamente!';
PRINT 'Distribución: 40 Perros, 30 Gatos, 20 Aves, 10 Conejos, 8 Roedores, 8 Reptiles, 4 Peces';