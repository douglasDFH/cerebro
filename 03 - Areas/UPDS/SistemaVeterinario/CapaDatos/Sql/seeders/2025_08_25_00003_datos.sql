-- =============================================
-- Inserción de datos para tabla historico  
-- =============================================
SET IDENTITY_INSERT [dbo].[historico] ON 

INSERT [dbo].[historico] ([id], [animal_id], [notas_generales], [alergias], [condiciones_medicas], [created_at], [updated_at]) VALUES 
-- PERROS (IDs 1-40 según animal_seeder)
(1, 1, N'Labrador Max - Historial médico completo desde cachorro. Animal saludable con rutinas de vacunación al día.', N'Alérgico a ciertos tipos de polen durante primavera', N'Displasia de cadera leve detectada', CAST(N'2023-01-15T09:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(2, 2, N'Golden Retriever Bella - Perra muy activa, excelente condición física. Propietarios muy responsables.', N'Sin alergias conocidas', N'Artritis en articulaciones posteriores', CAST(N'2023-02-20T10:15:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(3, 3, N'Pastor Alemán Rocky - Temperamento estable. Requiere ejercicio regular intenso.', N'Reacción alérgica a ciertos alimentos con pollo', N'Problemas digestivos ocasionales', CAST(N'2023-03-10T11:45:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(4, 4, N'Rottweiler Duke - Robusto, muy territorial. Excelente para guarda.', N'No presenta alergias', N'Tendencia a problemas cardíacos por raza', CAST(N'2023-04-05T08:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(5, 5, N'Gran Danés Atlas - Gigante gentil. Requiere cuidados especiales por su tamaño.', N'Sensibilidad a productos químicos de limpieza', N'Torsión gástrica en observación', CAST(N'2023-05-18T14:10:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(6, 6, N'San Bernardo Zeus - Muy dócil, excelente con niños. Historial de rescate.', N'Alergia estacional a gramíneas', N'Problemas respiratorios por conformación facial', CAST(N'2023-06-22T16:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(7, 7, N'Husky Luna - Alta energía, requiere mucho ejercicio. Muy inteligente.', N'Intolerancia a lactosa', N'Displasia de codo en seguimiento', CAST(N'2023-07-14T09:00:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(8, 8, N'Border Collie Sasha - Extremadamente inteligente. Excelente para agility.', N'Sin alergias reportadas', N'Tendencia hereditaria a epilepsia', CAST(N'2023-08-30T13:45:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(9, 9, N'Bulldog Francés Charlie - Cariñoso pero con problemas respiratorios típicos de la raza.', N'Reacción a ciertos conservantes en alimentos', N'Síndrome braquicefálico moderado', CAST(N'2023-09-12T10:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(10, 10, N'Cocker Spaniel Milo - Muy sociable y juguetón. Ideal para familias.', N'Alergia a ácaros del polvo', N'Otitis recurrente por conformación de orejas', CAST(N'2023-10-28T15:15:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(11, 11, N'Beagle Lola - Detector nato, muy activa y curiosa. Necesita supervisión.', N'Hipersensibilidad a picaduras de pulgas', N'Sobrepeso en control dietético', CAST(N'2023-11-15T12:00:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(12, 12, N'Bulldog Inglés Buddy - Temperamento calmo, excelente compañía.', N'Alergias respiratorias estacionales', N'Problemas articulares por conformación', CAST(N'2024-01-08T08:45:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(13, 13, N'Boxer Daisy - Energética y protectora, excelente con niños.', N'Sensibilidad a ciertos medicamentos', N'Cardiomiopatía en seguimiento', CAST(N'2024-02-14T11:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(14, 14, N'Springer Spaniel Cooper - Cazador natural, muy activo.', N'Alergia a polen de gramíneas', N'Displasia de cadera leve', CAST(N'2024-03-20T14:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(15, 15, N'Pointer Zoe - Excelente olfato, temperamento estable.', N'Sin alergias conocidas', N'Condición física excelente', CAST(N'2024-04-25T09:50:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(16, 16, N'Setter Irlandés Rex - Elegante y energético, gran compañero.', N'Sensibilidad alimentaria leve', N'Problemas oculares hereditarios', CAST(N'2024-05-30T13:10:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(17, 17, N'Chihuahua Luna - Pequeña pero valiente, gran personalidad.', N'Hipersensibilidad a ruidos', N'Luxación patelar congénita', CAST(N'2025-01-20T08:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(18, 18, N'Yorkshire Princess - Pequeña pero dominante.', N'Sensibilidad digestiva', N'Problemas dentales por raza', CAST(N'2025-02-28T13:35:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(19, 19, N'Maltés Princess - Blanco puro, muy cariñoso.', N'Alergia a colorantes artificiales', N'Luxación patelar bilateral', CAST(N'2025-03-15T10:50:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(20, 20, N'Pomerania Gizmo - Esponjoso y alegre.', N'Alergia a ciertos champús', N'Colapso traqueal leve', CAST(N'2025-04-10T16:05:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),

-- GATOS (IDs 41-70 según animal_seeder)
(21, 41, N'Persa Misu - Gata de pelo largo, muy elegante.', N'Sensibilidad a ciertos alimentos', N'Problemas respiratorios por conformación', CAST(N'2025-01-12T09:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(22, 42, N'Maine Coon Simba - Gigante gentil, excelente cazador.', N'Sin alergias conocidas', N'Cardiomiopatía hipertrófica en seguimiento', CAST(N'2024-09-17T16:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(23, 43, N'Siamés Nala - Vocal y cariñosa, muy apegada.', N'Sensibilidad a cambios de dieta', N'Problemas dentales por raza', CAST(N'2024-08-11T14:35:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(24, 44, N'Común Europeo Garfield - Cazador nato, independiente.', N'Alergia estacional leve', N'Sobrepeso controlado con dieta', CAST(N'2024-07-07T10:45:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(25, 45, N'Bengalí Mittens - Salvaje en apariencia, doméstico en corazón.', N'Sin alergias detectadas', N'Hiperactividad natural de la raza', CAST(N'2024-06-15T12:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),

-- AVES (IDs 71-90 según animal_seeder)
(26, 71, N'Loro Polly - Parlanchín excepcional, muy social.', N'Sensibilidad a aerosoles', N'Problemas respiratorios leves', CAST(N'2024-12-10T10:15:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(27, 72, N'Canario Tweety - Cantante melodioso, muy activo.', N'Sin alergias conocidas', N'Muda estacional normal', CAST(N'2024-11-22T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(28, 73, N'Guacamayo Rio - Colorido espectacular, inteligente.', N'Sensibilidad a metales pesados', N'Comportamiento territorial normal', CAST(N'2024-10-15T08:45:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(29, 74, N'Cacatúa Coco - Elegante y majestuosa.', N'Alergia a polvo excesivo', N'Tendencia al picaje por aburrimiento', CAST(N'2024-09-08T12:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(30, 75, N'Periquito Kiwi - Colorido y parlanchín, muy social.', N'Sin alergias reportadas', N'Hiperactividad natural', CAST(N'2024-08-03T16:55:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),

-- CONEJOS (IDs 91-100 según animal_seeder)  
(31, 91, N'Conejo Holandés Bunny - Pequeño y juguetón, muy cariñoso.', N'Alergia a ciertos henos', N'Problemas dentales en seguimiento', CAST(N'2024-04-07T15:15:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(32, 92, N'Conejo Angora Coco - Pelo largo, requiere cuidados especiales.', N'Sin alergias reportadas', N'Cuidados de pelaje intensivos', CAST(N'2024-03-25T10:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(33, 93, N'Conejo Rex Hoppy - Pelaje aterciopelado único.', N'Sensibilidad a cambios de temperatura', N'Condición física excelente', CAST(N'2024-02-18T14:25:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(34, 94, N'Lop Inglés Cotton - Orejas caídas, muy tranquilo.', N'Sin alergias conocidas', N'Obesidad controlada con dieta', CAST(N'2024-01-12T09:15:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(35, 95, N'Cabeza de León Clover - Melena característica.', N'Alergia a ciertos alimentos comerciales', N'Cuidados dentales preventivos', CAST(N'2023-12-08T11:40:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),

-- ROEDORES (IDs 101-108 según animal_seeder)
(36, 101, N'Hámster Peanut - Pequeño y activo, nocturno.', N'Sin alergias conocidas', N'Cuidados dentales por roer', CAST(N'2023-01-15T09:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(37, 102, N'Hámster Nibbles - Acumulador nato, muy curioso.', N'Sensibilidad a cambios de dieta', N'Obesidad controlada', CAST(N'2023-02-20T10:15:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(38, 103, N'Cobaya Fluffy - Social y vocal, requiere compañía.', N'Alergia a ciertos vegetales', N'Cuidados dentales constantes', CAST(N'2022-11-10T11:45:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(39, 104, N'Cobaya Patches - Colorido y amigable.', N'Sin alergias detectadas', N'Dieta rica en vitamina C', CAST(N'2021-08-17T08:20:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(40, 105, N'Chinchilla Chip - Pelaje sedoso, muy activo.', N'Sensibilidad a humedad', N'Baños de arena esenciales', CAST(N'2021-04-28T14:10:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2));

SET IDENTITY_INSERT [dbo].[historico] OFF
