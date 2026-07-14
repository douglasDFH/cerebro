-- =============================================
-- Script: Consultas_seeder.sql
-- Descripción: Datos de prueba para la tabla diagnostico
-- Fecha: 01/09/2025
-- Cantidad: 100 registros de servicios veterinarios
-- Nota: IDs van del 11 al 110 para evitar conflictos con datos existentes
-- =============================================

USE [Sistema_Veterinario]
GO

-- Insertar datos de prueba para la tabla diagnostico
SET IDENTITY_INSERT [dbo].[diagnostico] ON 

INSERT [dbo].[diagnostico] ([id], [codigo], [nombre], [descripcion], [precio_base], [categoria_diagnostico], [requiere_equipamiento], [activo], [created_at], [updated_at]) VALUES 
(11, N'CONS-001', N'Consulta General', N'Consulta veterinaria general para evaluación del estado de salud del animal', 150.00, N'Consulta', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(12, N'VAC-001', N'Vacunación Múltiple', N'Aplicación de vacunas esenciales (parvovirus, distemper, hepatitis, parainfluenza)', 280.00, N'Prevención', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(13, N'DESP-001', N'Desparasitación Interna', N'Tratamiento antiparasitario interno con medicamentos de amplio espectro', 120.00, N'Prevención', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(14, N'CAST-001', N'Castración Macho', N'Procedimiento quirúrgico de esterilización para machos', 800.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(15, N'CAST-002', N'Castración Hembra', N'Procedimiento quirúrgico de esterilización para hembras (ovariohisterectomía)', 1200.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(16, N'RX-001', N'Radiografía Simple', N'Estudio radiográfico simple para diagnóstico de fracturas o problemas internos', 350.00, N'Diagnóstico por Imagen', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(17, N'ECO-001', N'Ecografía Abdominal', N'Estudio ecográfico para evaluación de órganos abdominales', 450.00, N'Diagnóstico por Imagen', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(18, N'LAB-001', N'Hemograma Completo', N'Análisis sanguíneo completo para evaluación del estado general', 200.00, N'Laboratorio', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(19, N'LAB-002', N'Perfil Bioquímico', N'Análisis bioquímico de sangre para evaluación de función orgánica', 300.00, N'Laboratorio', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(20, N'CIR-001', N'Cirugía Menor', N'Procedimientos quirúrgicos menores como extracción de tumores pequeños', 600.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(21, N'CIR-002', N'Cirugía Mayor', N'Procedimientos quirúrgicos complejos que requieren hospitalización', 1500.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(22, N'DENT-001', N'Limpieza Dental', N'Profilaxis dental con remoción de sarro y pulido', 400.00, N'Odontología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(23, N'DENT-002', N'Extracción Dental', N'Extracción de piezas dentales dañadas o infectadas', 250.00, N'Odontología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(24, N'URG-001', N'Consulta de Urgencia', N'Atención veterinaria de emergencia fuera de horario normal', 300.00, N'Urgencias', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(25, N'HOSP-001', N'Hospitalización Día', N'Internación diurna para observación y tratamiento', 200.00, N'Hospitalización', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(26, N'HOSP-002', N'Hospitalización Nocturna', N'Internación nocturna con monitoreo continuo', 350.00, N'Hospitalización', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(27, N'MICRO-001', N'Implante de Microchip', N'Colocación de microchip de identificación', 180.00, N'Identificación', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(28, N'GROOM-001', N'Baño y Corte', N'Servicio completo de baño, secado y corte de pelo', 200.00, N'Estética', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(29, N'GROOM-002', N'Corte de Uñas', N'Recorte de uñas y limado', 50.00, N'Estética', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(30, N'VAC-002', N'Vacuna Antirrábica', N'Aplicación de vacuna contra la rabia', 100.00, N'Prevención', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(31, N'DESP-002', N'Desparasitación Externa', N'Tratamiento contra pulgas, garrapatas y ácaros', 150.00, N'Prevención', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(32, N'CURA-001', N'Curación de Heridas', N'Limpieza y tratamiento de heridas superficiales', 100.00, N'Tratamiento', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(33, N'SUTU-001', N'Sutura Simple', N'Sutura de heridas que requieren cierre quirúrgico', 200.00, N'Cirugía Menor', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(34, N'ANES-001', N'Anestesia Local', N'Aplicación de anestesia local para procedimientos menores', 80.00, N'Anestesia', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(35, N'ANES-002', N'Anestesia General', N'Anestesia general para cirugías mayores', 300.00, N'Anestesia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(36, N'OFTAL-001', N'Examen Oftalmológico', N'Evaluación completa de ojos y estructuras oculares', 180.00, N'Oftalmología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(37, N'OFTAL-002', N'Cirugía de Cataratas', N'Procedimiento quirúrgico para remoción de cataratas', 2500.00, N'Oftalmología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(38, N'CARDIO-001', N'Electrocardiograma', N'Estudio de la actividad eléctrica del corazón', 220.00, N'Cardiología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(39, N'CARDIO-002', N'Ecocardiografía', N'Estudio ecográfico del corazón y grandes vasos', 400.00, N'Cardiología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(40, N'NEURO-001', N'Evaluación Neurológica', N'Examen completo del sistema nervioso', 350.00, N'Neurología', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(41, N'ORTOP-001', N'Evaluación Ortopédica', N'Examen del sistema musculoesquelético', 250.00, N'Ortopedia', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(42, N'ORTOP-002', N'Cirugía de Fractura', N'Reducción y fijación quirúrgica de fracturas', 2000.00, N'Ortopedia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(43, N'REPRO-001', N'Control Reproductivo', N'Evaluación del estado reproductivo', 200.00, N'Reproducción', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(44, N'REPRO-002', N'Asistencia al Parto', N'Atención durante el proceso de parto', 500.00, N'Reproducción', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(45, N'REPRO-003', N'Cesárea', N'Procedimiento quirúrgico para extracción de crías', 1500.00, N'Reproducción', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(46, N'GAST-001', N'Endoscopía Digestiva', N'Examen endoscópico del tracto digestivo', 800.00, N'Gastroenterología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(47, N'GAST-002', N'Lavado Gástrico', N'Procedimiento de lavado estomacal en casos de intoxicación', 300.00, N'Gastroenterología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(48, N'DERMA-001', N'Biopsia de Piel', N'Toma de muestra de tejido cutáneo para análisis', 280.00, N'Dermatología', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(49, N'DERMA-002', N'Tratamiento Dermatológico', N'Tratamiento especializado para afecciones cutáneas', 200.00, N'Dermatología', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(50, N'FISIO-001', N'Sesión de Fisioterapia', N'Terapia física para rehabilitación', 150.00, N'Fisioterapia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(51, N'NUTRI-001', N'Consulta Nutricional', N'Evaluación y plan nutricional personalizado', 120.00, N'Nutrición', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(52, N'COMP-001', N'Evaluación Conductual', N'Análisis del comportamiento animal', 250.00, N'Comportamiento', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(53, N'PAIN-001', N'Tratamiento del Dolor', N'Manejo especializado del dolor crónico', 180.00, N'Medicina del Dolor', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(54, N'GERI-001', N'Consulta Geriátrica', N'Atención especializada para animales senior', 180.00, N'Geriatría', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(55, N'PEDI-001', N'Consulta Pediátrica', N'Atención especializada para cachorros', 160.00, N'Pediatría', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(56, N'ONCO-001', N'Consulta Oncológica', N'Evaluación y tratamiento de cáncer', 400.00, N'Oncología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(57, N'ONCO-002', N'Quimioterapia', N'Tratamiento oncológico con agentes quimioterápicos', 800.00, N'Oncología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(58, N'RADIO-001', N'Radioterapia', N'Tratamiento con radiaciones ionizantes', 1200.00, N'Oncología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(59, N'INMU-001', N'Test de Inmunodeficiencia', N'Prueba para detectar deficiencias inmunológicas', 180.00, N'Inmunología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(60, N'ENDO-001', N'Evaluación Endocrina', N'Estudio del sistema endocrino', 300.00, N'Endocrinología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(61, N'NEFRO-001', N'Evaluación Renal', N'Estudio completo de la función renal', 250.00, N'Nefrología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(62, N'HEPAT-001', N'Evaluación Hepática', N'Estudio de la función hepática', 220.00, N'Hepatología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(63, N'PULM-001', N'Evaluación Pulmonar', N'Estudio del sistema respiratorio', 200.00, N'Neumología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(64, N'HEMATO-001', N'Evaluación Hematológica', N'Estudio completo del sistema hematológico', 280.00, N'Hematología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(65, N'TOXIC-001', N'Tratamiento Intoxicación', N'Manejo de casos de intoxicación aguda', 400.00, N'Toxicología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(66, N'INFECT-001', N'Tratamiento Infeccioso', N'Manejo de enfermedades infecciosas', 180.00, N'Infectología', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(67, N'PARASI-001', N'Diagnóstico Parasitario', N'Identificación de parásitos internos y externos', 120.00, N'Parasitología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(68, N'MICOL-001', N'Diagnóstico Micológico', N'Identificación de hongos patógenos', 150.00, N'Micología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(69, N'VIROL-001', N'Diagnóstico Virológico', N'Identificación de agentes virales', 200.00, N'Virología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(70, N'BACT-001', N'Cultivo Bacteriano', N'Identificación y antibiograma bacteriano', 180.00, N'Bacteriología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(71, N'CITOL-001', N'Estudio Citológico', N'Análisis citológico de muestras', 160.00, N'Citología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(72, N'HISTO-001', N'Estudio Histopatológico', N'Análisis histopatológico de tejidos', 350.00, N'Histopatología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(73, N'CRYO-001', N'Crioterapia', N'Tratamiento con frío extremo para lesiones', 250.00, N'Crioterapia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(74, N'LASER-001', N'Terapia Láser', N'Tratamiento con láser terapéutico', 200.00, N'Terapia Láser', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(75, N'ACUP-001', N'Acupuntura Veterinaria', N'Tratamiento de acupuntura para el dolor', 180.00, N'Medicina Alternativa', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(76, N'HOMEO-001', N'Tratamiento Homeopático', N'Medicina homeopática veterinaria', 120.00, N'Medicina Alternativa', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(77, N'FLOR-001', N'Terapia Floral', N'Tratamiento con esencias florales', 100.00, N'Medicina Alternativa', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(78, N'HIDRO-001', N'Hidroterapia', N'Terapia acuática para rehabilitación', 150.00, N'Fisioterapia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(79, N'ULTRA-001', N'Ultrasonido Terapéutico', N'Terapia con ultrasonido para lesiones', 120.00, N'Fisioterapia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(80, N'ELECTR-001', N'Electroterapia', N'Estimulación eléctrica terapéutica', 100.00, N'Fisioterapia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(81, N'REHAB-001', N'Programa de Rehabilitación', N'Plan integral de rehabilitación', 300.00, N'Rehabilitación', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(82, N'MAGNET-001', N'Magnetoterapia', N'Terapia con campos magnéticos', 150.00, N'Fisioterapia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(83, N'OXIG-001', N'Oxigenoterapia', N'Terapia con oxígeno hiperbárico', 200.00, N'Terapia Respiratoria', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(84, N'NEBUL-001', N'Nebulización', N'Tratamiento por inhalación de medicamentos', 80.00, N'Terapia Respiratoria', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(85, N'TRANS-001', N'Transfusión Sanguínea', N'Administración de sangre o hemoderivados', 600.00, N'Hematología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(86, N'PLASM-001', N'Plasmaféresis', N'Separación y filtrado del plasma sanguíneo', 800.00, N'Hematología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(87, N'HEMOD-001', N'Hemodiálisis', N'Filtrado artificial de la sangre', 1000.00, N'Nefrología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(88, N'PERIT-001', N'Diálisis Peritoneal', N'Diálisis a través de la cavidad peritoneal', 800.00, N'Nefrología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(89, N'GASTRO-001', N'Gastrostomía', N'Colocación de sonda de alimentación gástrica', 500.00, N'Gastroenterología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(90, N'TRAQ-001', N'Traqueostomía', N'Apertura quirúrgica de la tráquea', 800.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(91, N'CISTO-001', N'Cistostomía', N'Drenaje quirúrgico de la vejiga', 600.00, N'Urología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(92, N'TORA-001', N'Toracocentesis', N'Drenaje de líquido pleural', 300.00, N'Neumología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(93, N'ABDO-001', N'Abdominocentesis', N'Drenaje de líquido abdominal', 250.00, N'Gastroenterología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(94, N'PERICAR-001', N'Pericardiocentesis', N'Drenaje del saco pericárdico', 400.00, N'Cardiología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(95, N'ARTRO-001', N'Artroscopia', N'Cirugía mínimamente invasiva de articulaciones', 1200.00, N'Ortopedia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(96, N'LAPARO-001', N'Laparoscopia', N'Cirugía mínimamente invasiva abdominal', 1500.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(97, N'TORAS-001', N'Toracoscopia', N'Cirugía mínimamente invasiva torácica', 1800.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(98, N'CRIPTO-001', N'Cirugía de Criptorquidia', N'Corrección quirúrgica de testículos no descendidos', 800.00, N'Urología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(99, N'HERNI-001', N'Reparación de Hernia', N'Corrección quirúrgica de hernias', 700.00, N'Cirugía', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(100, N'PROTE-001', N'Colocación de Prótesis', N'Implantación de prótesis ortopédicas', 3000.00, N'Ortopedia', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(101, N'MARCA-001', N'Implante de Marcapasos', N'Colocación de marcapasos cardíaco', 5000.00, N'Cardiología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(102, N'STENT-001', N'Colocación de Stent', N'Implantación de stent vascular', 2500.00, N'Cardiología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(103, N'BYPASS-001', N'Cirugía de Bypass', N'Creación de derivación vascular', 4000.00, N'Cirugía Cardiovascular', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(104, N'TRANSPLANT-001', N'Trasplante de Órgano', N'Procedimiento de trasplante', 10000.00, N'Trasplantes', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(105, N'BANCO-001', N'Banco de Sangre', N'Servicio de banco de sangre veterinario', 100.00, N'Hematología', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(106, N'GENE-001', N'Análisis Genético', N'Estudio del material genético', 400.00, N'Genética', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(107, N'CRIOCON-001', N'Crioconservación', N'Preservación de material biológico a bajas temperaturas', 300.00, N'Reproducción', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(108, N'INSEM-001', N'Inseminación Artificial', N'Procedimiento de reproducción asistida', 400.00, N'Reproducción', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(109, N'EMBRIO-001', N'Transferencia de Embriones', N'Implantación de embriones', 800.00, N'Reproducción', 1, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2)),
(110, N'TELEMEDI-001', N'Consulta Telemedicina', N'Consulta veterinaria a distancia', 80.00, N'Telemedicina', 0, 1, CAST(N'2025-09-01T14:30:00.000' AS DateTime2), CAST(N'2025-09-01T14:30:00.000' AS DateTime2));

SET IDENTITY_INSERT [dbo].[diagnostico] OFF
GO

-- Mensaje de confirmación
PRINT '100 registros de diagnósticos insertados exitosamente (IDs 11-110)';
PRINT 'Total de categorías incluidas: 30+ especialidades veterinarias';
GO
