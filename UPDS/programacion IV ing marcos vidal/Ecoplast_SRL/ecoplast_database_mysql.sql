-- ============================================
-- ECOPLAST SRL - SISTEMA DE GESTIÓN DE PRODUCCIÓN
-- Base de Datos: MySQL 8.0+
-- Empresa: Plásticos Biodegradables
-- ============================================

-- Configuración inicial
SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
SET SQL_MODE = 'NO_AUTO_VALUE_ON_ZERO';
SET time_zone = '+00:00';

-- Crear base de datos
DROP DATABASE IF EXISTS ecoplast_produccion;
CREATE DATABASE ecoplast_produccion 
  DEFAULT CHARACTER SET utf8mb4 
  DEFAULT COLLATE utf8mb4_unicode_ci;

USE ecoplast_produccion;

-- ============================================
-- MÓDULO 1: USUARIOS Y ROLES
-- ============================================

CREATE TABLE roles (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_rol VARCHAR(50) NOT NULL UNIQUE,
    descripcion TEXT,
    nivel_acceso ENUM('basico', 'intermedio', 'avanzado', 'total') DEFAULT 'basico',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_nombre_rol (nombre_rol)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE usuarios (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_completo VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    rol_id INT UNSIGNED NOT NULL,
    telefono VARCHAR(20),
    foto_perfil VARCHAR(255),
    activo BOOLEAN DEFAULT TRUE,
    ultimo_acceso TIMESTAMP NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (rol_id) REFERENCES roles(id) ON DELETE RESTRICT,
    INDEX idx_email (email),
    INDEX idx_activo (activo),
    INDEX idx_rol_id (rol_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE turnos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_turno VARCHAR(50) NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    INDEX idx_activo (activo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE asignacion_turnos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT UNSIGNED NOT NULL,
    turno_id INT UNSIGNED NOT NULL,
    fecha_asignacion DATE NOT NULL,
    observaciones TEXT,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (turno_id) REFERENCES turnos(id) ON DELETE RESTRICT,
    UNIQUE KEY unique_asignacion (usuario_id, fecha_asignacion),
    INDEX idx_fecha_asignacion (fecha_asignacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 2: MATERIAS PRIMAS BIODEGRADABLES
-- ============================================

CREATE TABLE categorias_insumos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_categoria VARCHAR(100) NOT NULL,
    descripcion TEXT,
    es_biodegradable BOOLEAN DEFAULT TRUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE insumos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    codigo_insumo VARCHAR(50) NOT NULL UNIQUE,
    nombre_insumo VARCHAR(150) NOT NULL,
    categoria_id INT UNSIGNED NOT NULL,
    tipo_material ENUM('PLA', 'PHA', 'PBS', 'PBAT', 'Almidon', 'Celulosa', 'Aditivo', 'Pigmento', 'Otro') NOT NULL,
    unidad_medida ENUM('kg', 'ton', 'litro', 'unidad') DEFAULT 'kg',
    densidad DECIMAL(6,3) NULL COMMENT 'g/cm³',
    temperatura_fusion DECIMAL(5,1) NULL COMMENT '°C',
    certificacion_biodegradable VARCHAR(100) NULL COMMENT 'Ej: EN 13432, ASTM D6400',
    proveedor VARCHAR(150),
    precio_unitario DECIMAL(10,2) NOT NULL,
    stock_minimo DECIMAL(10,2) NOT NULL,
    stock_actual DECIMAL(10,2) NOT NULL DEFAULT 0,
    fecha_caducidad_lote DATE NULL,
    activo BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (categoria_id) REFERENCES categorias_insumos(id) ON DELETE RESTRICT,
    INDEX idx_codigo_insumo (codigo_insumo),
    INDEX idx_activo (activo),
    INDEX idx_stock_actual (stock_actual),
    INDEX idx_tipo_material (tipo_material)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE movimientos_inventario_insumos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    insumo_id INT UNSIGNED NOT NULL,
    tipo_movimiento ENUM('entrada', 'salida', 'ajuste', 'desperdicio') NOT NULL,
    cantidad DECIMAL(10,2) NOT NULL,
    lote VARCHAR(50),
    fecha_vencimiento DATE NULL,
    costo_unitario DECIMAL(10,2) NULL,
    usuario_id INT UNSIGNED NOT NULL,
    motivo TEXT,
    fecha_movimiento TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (insumo_id) REFERENCES insumos(id) ON DELETE RESTRICT,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_fecha_movimiento (fecha_movimiento),
    INDEX idx_insumo_id (insumo_id),
    INDEX idx_tipo_movimiento (tipo_movimiento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 3: FÓRMULAS Y MEZCLAS
-- ============================================

CREATE TABLE formulaciones (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    codigo_formula VARCHAR(50) NOT NULL UNIQUE,
    nombre_formula VARCHAR(150) NOT NULL,
    descripcion TEXT,
    version VARCHAR(20) DEFAULT '1.0',
    tipo_producto_destino VARCHAR(100) COMMENT 'Ej: Bolsas compostables, Vasos PLA',
    temperatura_procesamiento_min DECIMAL(5,1) COMMENT '°C',
    temperatura_procesamiento_max DECIMAL(5,1) COMMENT '°C',
    tiempo_degradacion_estimado INT COMMENT 'días en condiciones de compostaje',
    certificaciones TEXT COMMENT 'Certificaciones que cumple el producto final',
    aprobado BOOLEAN DEFAULT FALSE,
    fecha_aprobacion TIMESTAMP NULL,
    usuario_aprueba_id INT UNSIGNED NULL,
    activo BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_aprueba_id) REFERENCES usuarios(id) ON DELETE SET NULL,
    INDEX idx_codigo_formula (codigo_formula),
    INDEX idx_aprobado (aprobado),
    INDEX idx_activo (activo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE componentes_formulacion (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    formulacion_id INT UNSIGNED NOT NULL,
    insumo_id INT UNSIGNED NOT NULL,
    porcentaje DECIMAL(5,2) NOT NULL COMMENT 'Porcentaje en peso',
    cantidad_base DECIMAL(10,3) NOT NULL COMMENT 'Cantidad para 100kg de mezcla',
    orden_adicion TINYINT DEFAULT 1,
    notas TEXT,
    FOREIGN KEY (formulacion_id) REFERENCES formulaciones(id) ON DELETE CASCADE,
    FOREIGN KEY (insumo_id) REFERENCES insumos(id) ON DELETE RESTRICT,
    UNIQUE KEY unique_componente (formulacion_id, insumo_id),
    INDEX idx_formulacion_id (formulacion_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 4: MAQUINARIA
-- ============================================

CREATE TABLE tipos_maquina (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_tipo VARCHAR(100) NOT NULL,
    descripcion TEXT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE maquinas (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    codigo_maquina VARCHAR(50) NOT NULL UNIQUE,
    nombre_maquina VARCHAR(150) NOT NULL,
    tipo_maquina_id INT UNSIGNED NOT NULL,
    marca VARCHAR(100),
    modelo VARCHAR(100),
    año_fabricacion YEAR,
    capacidad_produccion DECIMAL(10,2) COMMENT 'unidades o kg por hora',
    unidad_capacidad VARCHAR(20) DEFAULT 'unidades/hora',
    consumo_energia_kwh DECIMAL(8,2) COMMENT 'kWh por hora',
    temp_min_operacion DECIMAL(5,1) COMMENT '°C',
    temp_max_operacion DECIMAL(5,1) COMMENT '°C',
    presion_max_bar DECIMAL(6,2) COMMENT 'Bar',
    velocidad_max_rpm DECIMAL(8,2) COMMENT 'RPM',
    fuerza_cierre_ton DECIMAL(8,2) NULL COMMENT 'Toneladas (para inyectoras)',
    diametro_husillo_mm DECIMAL(6,2) NULL COMMENT 'mm (para extrusoras)',
    fecha_instalacion DATE,
    vida_util_años INT DEFAULT 15,
    ubicacion VARCHAR(100),
    estado_actual ENUM('operativa', 'mantenimiento', 'parada', 'averia') DEFAULT 'operativa',
    activo BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (tipo_maquina_id) REFERENCES tipos_maquina(id) ON DELETE RESTRICT,
    INDEX idx_codigo_maquina (codigo_maquina),
    INDEX idx_estado_actual (estado_actual),
    INDEX idx_activo (activo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE mantenimientos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    maquina_id INT UNSIGNED NOT NULL,
    tipo_mantenimiento ENUM('preventivo', 'correctivo', 'predictivo') NOT NULL,
    descripcion TEXT NOT NULL,
    fecha_programada DATETIME NOT NULL,
    fecha_inicio DATETIME NULL,
    fecha_fin DATETIME NULL,
    duracion_horas DECIMAL(6,2) NULL,
    costo DECIMAL(10,2) NULL,
    tecnico_id INT UNSIGNED NULL,
    estado ENUM('programado', 'en_proceso', 'completado', 'cancelado') DEFAULT 'programado',
    prioridad ENUM('baja', 'media', 'alta', 'critica') DEFAULT 'media',
    observaciones TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maquina_id) REFERENCES maquinas(id) ON DELETE RESTRICT,
    FOREIGN KEY (tecnico_id) REFERENCES usuarios(id) ON DELETE SET NULL,
    INDEX idx_maquina_id (maquina_id),
    INDEX idx_fecha_programada (fecha_programada),
    INDEX idx_estado (estado),
    INDEX idx_tipo_mantenimiento (tipo_mantenimiento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE paros_maquina (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    maquina_id INT UNSIGNED NOT NULL,
    tipo_paro ENUM('averia', 'mantenimiento', 'cambio_molde', 'falta_material', 'falta_personal', 'ajuste_calidad', 'otros') NOT NULL,
    fecha_inicio DATETIME NOT NULL,
    fecha_fin DATETIME NULL,
    duracion_minutos INT NULL,
    descripcion TEXT,
    causa_raiz TEXT NULL,
    accion_correctiva TEXT NULL,
    operador_id INT UNSIGNED NOT NULL,
    impacto_produccion DECIMAL(10,2) NULL COMMENT 'unidades no producidas',
    costo_estimado DECIMAL(10,2) NULL,
    FOREIGN KEY (maquina_id) REFERENCES maquinas(id) ON DELETE RESTRICT,
    FOREIGN KEY (operador_id) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_maquina_id (maquina_id),
    INDEX idx_fecha_inicio (fecha_inicio),
    INDEX idx_tipo_paro (tipo_paro)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 5: PRODUCTOS BIODEGRADABLES
-- ============================================

CREATE TABLE categorias_productos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_categoria VARCHAR(100) NOT NULL,
    descripcion TEXT,
    aplicacion VARCHAR(200) COMMENT 'Ej: Food service, Retail bags, Agricultura'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE productos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    codigo_producto VARCHAR(50) NOT NULL UNIQUE,
    nombre_producto VARCHAR(150) NOT NULL,
    categoria_producto_id INT UNSIGNED NOT NULL,
    descripcion TEXT,
    material_principal ENUM('PLA', 'PHA', 'PBS', 'PBAT', 'Almidon', 'Mixto') NOT NULL,
    certificacion_compostable VARCHAR(200) COMMENT 'Ej: OK Compost, Seedling, BPI',
    tiempo_compostaje_dias INT COMMENT 'Días para degradación en compost industrial',
    capacidad_carga_kg DECIMAL(8,2) NULL COMMENT 'Para bolsas/contenedores',
    peso_unitario_gramos DECIMAL(8,2) NOT NULL,
    dimensiones VARCHAR(100) COMMENT 'Ej: 30x40cm, Diámetro 8cm',
    color VARCHAR(50) DEFAULT 'natural',
    espesor_micras INT NULL,
    formulacion_id INT UNSIGNED NULL,
    tiempo_ciclo_segundos INT NULL,
    piezas_por_ciclo INT DEFAULT 1,
    temperatura_proceso DECIMAL(5,1) NULL,
    precio_venta DECIMAL(10,2) NOT NULL,
    unidad_venta ENUM('unidad', 'paquete', 'caja', 'kg') DEFAULT 'unidad',
    unidades_por_paquete INT DEFAULT 1,
    stock_minimo INT NOT NULL DEFAULT 0,
    stock_actual INT NOT NULL DEFAULT 0,
    imagen_producto VARCHAR(255),
    activo BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (categoria_producto_id) REFERENCES categorias_productos(id) ON DELETE RESTRICT,
    FOREIGN KEY (formulacion_id) REFERENCES formulaciones(id) ON DELETE SET NULL,
    INDEX idx_codigo_producto (codigo_producto),
    INDEX idx_activo (activo),
    INDEX idx_stock_actual (stock_actual)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE movimientos_inventario_productos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    producto_id INT UNSIGNED NOT NULL,
    tipo_movimiento ENUM('entrada_produccion', 'salida_venta', 'ajuste', 'merma', 'devolucion') NOT NULL,
    cantidad INT NOT NULL,
    lote_produccion VARCHAR(50),
    fecha_fabricacion DATE NULL,
    fecha_vencimiento DATE NULL,
    usuario_id INT UNSIGNED NOT NULL,
    referencia VARCHAR(100) COMMENT 'ID de orden de producción o venta',
    motivo TEXT,
    fecha_movimiento TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (producto_id) REFERENCES productos(id) ON DELETE RESTRICT,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_producto_id (producto_id),
    INDEX idx_fecha_movimiento (fecha_movimiento),
    INDEX idx_tipo_movimiento (tipo_movimiento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 6: ÓRDENES DE PRODUCCIÓN
-- ============================================

CREATE TABLE ordenes_produccion (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    numero_orden VARCHAR(50) NOT NULL UNIQUE,
    producto_id INT UNSIGNED NOT NULL,
    cantidad_planificada INT NOT NULL,
    cantidad_producida INT DEFAULT 0,
    cantidad_conforme INT DEFAULT 0,
    cantidad_defectuosa INT DEFAULT 0,
    formulacion_id INT UNSIGNED NOT NULL,
    maquina_id INT UNSIGNED NOT NULL,
    turno_id INT UNSIGNED NOT NULL,
    fecha_programada DATETIME NOT NULL,
    fecha_inicio DATETIME NULL,
    fecha_fin DATETIME NULL,
    operador_id INT UNSIGNED NULL,
    supervisor_id INT UNSIGNED NULL,
    estado ENUM('pendiente', 'en_proceso', 'pausada', 'completada', 'cancelada') DEFAULT 'pendiente',
    prioridad ENUM('baja', 'normal', 'alta', 'urgente') DEFAULT 'normal',
    notas_produccion TEXT,
    observaciones_calidad TEXT,
    creado_por INT UNSIGNED NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (producto_id) REFERENCES productos(id) ON DELETE RESTRICT,
    FOREIGN KEY (formulacion_id) REFERENCES formulaciones(id) ON DELETE RESTRICT,
    FOREIGN KEY (maquina_id) REFERENCES maquinas(id) ON DELETE RESTRICT,
    FOREIGN KEY (turno_id) REFERENCES turnos(id) ON DELETE RESTRICT,
    FOREIGN KEY (operador_id) REFERENCES usuarios(id) ON DELETE SET NULL,
    FOREIGN KEY (supervisor_id) REFERENCES usuarios(id) ON DELETE SET NULL,
    FOREIGN KEY (creado_por) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_numero_orden (numero_orden),
    INDEX idx_estado (estado),
    INDEX idx_fecha_programada (fecha_programada),
    INDEX idx_maquina_id (maquina_id),
    INDEX idx_producto_id (producto_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE lotes_produccion (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    numero_lote VARCHAR(50) NOT NULL UNIQUE,
    orden_id INT UNSIGNED NOT NULL,
    cantidad INT NOT NULL,
    fecha_fabricacion DATETIME NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    trazabilidad_insumos JSON COMMENT 'JSON con lotes de insumos utilizados',
    estado_lote ENUM('cuarentena', 'aprobado', 'rechazado', 'distribuido') DEFAULT 'cuarentena',
    ubicacion_almacen VARCHAR(100),
    FOREIGN KEY (orden_id) REFERENCES ordenes_produccion(id) ON DELETE RESTRICT,
    INDEX idx_numero_lote (numero_lote),
    INDEX idx_orden_id (orden_id),
    INDEX idx_estado_lote (estado_lote)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 7: REGISTROS DE PRODUCCIÓN (Tiempo Real)
-- ============================================

CREATE TABLE registros_produccion (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    orden_id INT UNSIGNED NOT NULL,
    maquina_id INT UNSIGNED NOT NULL,
    operador_id INT UNSIGNED NOT NULL,
    fecha_hora DATETIME NOT NULL,
    piezas_producidas INT NOT NULL DEFAULT 0,
    piezas_conformes INT NOT NULL DEFAULT 0,
    piezas_defectuosas INT NOT NULL DEFAULT 0,
    tipo_defecto VARCHAR(100) NULL,
    temperatura_zona1 DECIMAL(5,1) NULL COMMENT '°C',
    temperatura_zona2 DECIMAL(5,1) NULL COMMENT '°C',
    temperatura_zona3 DECIMAL(5,1) NULL COMMENT '°C',
    temperatura_zona4 DECIMAL(5,1) NULL COMMENT '°C',
    presion_inyeccion DECIMAL(6,2) NULL COMMENT 'Bar',
    velocidad_husillo DECIMAL(7,2) NULL COMMENT 'RPM',
    tiempo_ciclo_real DECIMAL(6,2) NULL COMMENT 'segundos',
    consumo_energia_kwh DECIMAL(8,3) NULL,
    consumo_material_kg DECIMAL(10,3) NULL,
    scrap_kg DECIMAL(10,3) NULL COMMENT 'Material recuperable',
    observaciones TEXT,
    alerta_calidad BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (orden_id) REFERENCES ordenes_produccion(id) ON DELETE RESTRICT,
    FOREIGN KEY (maquina_id) REFERENCES maquinas(id) ON DELETE RESTRICT,
    FOREIGN KEY (operador_id) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_fecha_hora (fecha_hora),
    INDEX idx_orden_maquina (orden_id, maquina_id),
    INDEX idx_alerta_calidad (alerta_calidad)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 8: CONTROL DE CALIDAD
-- ============================================

CREATE TABLE inspecciones_calidad (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    orden_id INT UNSIGNED NOT NULL,
    lote_id INT UNSIGNED NULL,
    tipo_inspeccion ENUM('primera_pieza', 'proceso', 'final', 'auditoria') NOT NULL,
    fecha_hora DATETIME NOT NULL,
    inspector_id INT UNSIGNED NOT NULL,
    peso_promedio_gramos DECIMAL(8,3) NULL,
    desviacion_peso DECIMAL(6,3) NULL,
    espesor_promedio_micras DECIMAL(7,2) NULL,
    resistencia_traccion_mpa DECIMAL(7,2) NULL,
    test_biodegradacion BOOLEAN NULL,
    dias_compostaje_prueba INT NULL,
    manchas INT DEFAULT 0,
    deformaciones INT DEFAULT 0,
    rebabas INT DEFAULT 0,
    burbujas INT DEFAULT 0,
    fisuras INT DEFAULT 0,
    otros_defectos TEXT,
    piezas_inspeccionadas INT NOT NULL,
    piezas_aprobadas INT NOT NULL,
    piezas_rechazadas INT NOT NULL,
    resultado ENUM('aprobado', 'aprobado_condicional', 'rechazado') NOT NULL,
    observaciones TEXT,
    acciones_correctivas TEXT,
    FOREIGN KEY (orden_id) REFERENCES ordenes_produccion(id) ON DELETE RESTRICT,
    FOREIGN KEY (lote_id) REFERENCES lotes_produccion(id) ON DELETE SET NULL,
    FOREIGN KEY (inspector_id) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_orden_id (orden_id),
    INDEX idx_lote_id (lote_id),
    INDEX idx_fecha_hora (fecha_hora),
    INDEX idx_resultado (resultado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE defectos_calidad (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    codigo_defecto VARCHAR(20) NOT NULL UNIQUE,
    nombre_defecto VARCHAR(100) NOT NULL,
    descripcion TEXT,
    severidad ENUM('critico', 'mayor', 'menor') NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    INDEX idx_codigo_defecto (codigo_defecto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE registro_defectos (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    inspeccion_id INT UNSIGNED NOT NULL,
    defecto_id INT UNSIGNED NOT NULL,
    cantidad INT NOT NULL,
    ubicacion_pieza VARCHAR(100) NULL,
    imagen_evidencia VARCHAR(255) NULL,
    FOREIGN KEY (inspeccion_id) REFERENCES inspecciones_calidad(id) ON DELETE CASCADE,
    FOREIGN KEY (defecto_id) REFERENCES defectos_calidad(id) ON DELETE RESTRICT,
    INDEX idx_inspeccion_id (inspeccion_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 9: KPIs CALCULADOS
-- ============================================

CREATE TABLE kpis_diarios (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    fecha DATE NOT NULL,
    maquina_id INT UNSIGNED NOT NULL,
    turno_id INT UNSIGNED NOT NULL,
    unidades_planificadas INT NOT NULL,
    unidades_producidas INT NOT NULL,
    unidades_conformes INT NOT NULL,
    unidades_defectuosas INT NOT NULL,
    scrap_kg DECIMAL(10,2) DEFAULT 0,
    tiempo_planificado INT NOT NULL COMMENT 'minutos',
    tiempo_operacion INT NOT NULL COMMENT 'minutos',
    tiempo_paradas INT NOT NULL COMMENT 'minutos',
    tiempo_setup INT DEFAULT 0 COMMENT 'minutos',
    disponibilidad DECIMAL(5,2) NOT NULL COMMENT 'Tiempo operación / Tiempo planificado * 100',
    rendimiento DECIMAL(5,2) NOT NULL COMMENT 'Producción real / Producción teórica * 100',
    calidad DECIMAL(5,2) NOT NULL COMMENT 'Piezas conformes / Piezas producidas * 100',
    oee DECIMAL(5,2) NOT NULL COMMENT 'Disponibilidad * Rendimiento * Calidad / 100',
    consumo_energia_kwh DECIMAL(10,2) DEFAULT 0,
    consumo_material_kg DECIMAL(10,2) DEFAULT 0,
    eficiencia_material DECIMAL(5,2) NULL COMMENT '%',
    costo_produccion DECIMAL(12,2) NULL,
    tasa_defectos DECIMAL(5,2) NOT NULL COMMENT 'PPM o %',
    first_pass_yield DECIMAL(5,2) NULL,
    calculado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maquina_id) REFERENCES maquinas(id) ON DELETE RESTRICT,
    FOREIGN KEY (turno_id) REFERENCES turnos(id) ON DELETE RESTRICT,
    UNIQUE KEY unique_kpi_diario (fecha, maquina_id, turno_id),
    INDEX idx_fecha (fecha),
    INDEX idx_maquina_id (maquina_id),
    INDEX idx_oee (oee)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE kpis_mensuales (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    año YEAR NOT NULL,
    mes TINYINT NOT NULL,
    maquina_id INT UNSIGNED NULL COMMENT 'NULL para KPIs globales',
    total_unidades_producidas BIGINT NOT NULL,
    total_unidades_conformes BIGINT NOT NULL,
    total_scrap_kg DECIMAL(12,2) DEFAULT 0,
    oee_promedio DECIMAL(5,2) NOT NULL,
    disponibilidad_promedio DECIMAL(5,2) NOT NULL,
    rendimiento_promedio DECIMAL(5,2) NOT NULL,
    calidad_promedio DECIMAL(5,2) NOT NULL,
    mtbf DECIMAL(10,2) NULL COMMENT 'Mean Time Between Failures (horas)',
    mttr DECIMAL(10,2) NULL COMMENT 'Mean Time To Repair (horas)',
    numero_paros INT DEFAULT 0,
    tiempo_total_paros_horas DECIMAL(10,2) DEFAULT 0,
    costo_total_produccion DECIMAL(15,2) NULL,
    costo_unitario DECIMAL(10,4) NULL,
    costo_energia DECIMAL(12,2) NULL,
    costo_material DECIMAL(12,2) NULL,
    costo_mantenimiento DECIMAL(12,2) NULL,
    porcentaje_material_biodegradable DECIMAL(5,2) NULL,
    cumplimiento_certificaciones DECIMAL(5,2) NULL,
    calculado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (maquina_id) REFERENCES maquinas(id) ON DELETE SET NULL,
    UNIQUE KEY unique_kpi_mensual (año, mes, maquina_id),
    INDEX idx_año_mes (año, mes)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 10: ALERTAS Y NOTIFICACIONES
-- ============================================

CREATE TABLE alertas (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    tipo_alerta ENUM('stock_bajo', 'maquina_parada', 'calidad_deficiente', 'mantenimiento_vencido', 'meta_no_cumplida', 'otro') NOT NULL,
    severidad ENUM('info', 'advertencia', 'critico') NOT NULL,
    titulo VARCHAR(200) NOT NULL,
    mensaje TEXT NOT NULL,
    entidad_tipo VARCHAR(50) COMMENT 'maquina, producto, insumo, orden',
    entidad_id INT UNSIGNED COMMENT 'ID de la entidad relacionada',
    usuario_destino_id INT UNSIGNED NULL COMMENT 'NULL para alertas globales',
    leida BOOLEAN DEFAULT FALSE,
    fecha_lectura TIMESTAMP NULL,
    accion_tomada TEXT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_destino_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    INDEX idx_usuario_destino (usuario_destino_id),
    INDEX idx_leida (leida),
    INDEX idx_severidad (severidad),
    INDEX idx_created_at (created_at)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- MÓDULO 11: CERTIFICACIONES Y AUDITORÍAS
-- ============================================

CREATE TABLE certificaciones (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nombre_certificacion VARCHAR(150) NOT NULL,
    tipo_certificacion ENUM('producto', 'proceso', 'empresa', 'ambiental') NOT NULL,
    organismo_certificador VARCHAR(150),
    numero_certificado VARCHAR(100),
    fecha_emision DATE NOT NULL,
    fecha_vencimiento DATE NOT NULL,
    estado ENUM('vigente', 'por_vencer', 'vencida', 'en_renovacion') NOT NULL,
    alcance TEXT COMMENT 'Productos o procesos cubiertos',
    documento_pdf VARCHAR(255),
    notas TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_fecha_vencimiento (fecha_vencimiento),
    INDEX idx_estado (estado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE auditorias (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    tipo_auditoria ENUM('interna', 'externa', 'certificacion', 'cliente') NOT NULL,
    fecha_auditoria DATE NOT NULL,
    auditor VARCHAR(150) NOT NULL,
    alcance TEXT NOT NULL,
    hallazgos TEXT,
    no_conformidades INT DEFAULT 0,
    observaciones INT DEFAULT 0,
    oportunidades_mejora INT DEFAULT 0,
    resultado ENUM('satisfactorio', 'condicional', 'no_satisfactorio') NOT NULL,
    plan_accion TEXT,
    documento_informe VARCHAR(255),
    usuario_responsable_id INT UNSIGNED NOT NULL,
    FOREIGN KEY (usuario_responsable_id) REFERENCES usuarios(id) ON DELETE RESTRICT,
    INDEX idx_fecha_auditoria (fecha_auditoria),
    INDEX idx_tipo_auditoria (tipo_auditoria)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================
-- VISTAS ÚTILES PARA DASHBOARDS
-- ============================================

-- Vista: Resumen producción del día
CREATE OR REPLACE VIEW v_produccion_hoy AS
SELECT 
    m.nombre_maquina,
    p.nombre_producto,
    op.numero_orden,
    op.cantidad_planificada,
    op.cantidad_producida,
    op.cantidad_conforme,
    op.cantidad_defectuosa,
    ROUND((op.cantidad_conforme / NULLIF(op.cantidad_producida, 0) * 100), 2) as porcentaje_calidad,
    op.estado,
    u.nombre_completo as operador,
    op.fecha_programada,
    op.fecha_inicio
FROM ordenes_produccion op
JOIN maquinas m ON op.maquina_id = m.id
JOIN productos p ON op.producto_id = p.id
LEFT JOIN usuarios u ON op.operador_id = u.id
WHERE DATE(op.fecha_programada) = CURDATE();

-- Vista: Stock bajo de insumos
CREATE OR REPLACE VIEW v_insumos_stock_bajo AS
SELECT 
    i.codigo_insumo,
    i.nombre_insumo,
    i.tipo_material,
    i.stock_actual,
    i.stock_minimo,
    i.unidad_medida,
    (i.stock_minimo - i.stock_actual) as cantidad_faltante,
    i.proveedor,
    i.precio_unitario
FROM insumos i
WHERE i.stock_actual < i.stock_minimo AND i.activo = TRUE
ORDER BY (i.stock_minimo - i.stock_actual) DESC;

-- Vista: OEE por máquina última semana
CREATE OR REPLACE VIEW v_oee_ultima_semana AS
SELECT 
    m.nombre_maquina,
    m.codigo_maquina,
    k.fecha,
    ROUND(AVG(k.oee), 2) as oee_promedio,
    ROUND(AVG(k.disponibilidad), 2) as disponibilidad_promedio,
    ROUND(AVG(k.rendimiento), 2) as rendimiento_promedio,
    ROUND(AVG(k.calidad), 2) as calidad_promedio,
    SUM(k.unidades_producidas) as total_unidades
FROM kpis_diarios k
JOIN maquinas m ON k.maquina_id = m.id
WHERE k.fecha >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
GROUP BY m.id, k.fecha
ORDER BY k.fecha DESC, m.nombre_maquina;

-- Vista: Máquinas con estado actual
CREATE OR REPLACE VIEW v_estado_maquinas AS
SELECT 
    m.id,
    m.codigo_maquina,
    m.nombre_maquina,
    m.estado_actual,
    op.numero_orden as orden_activa,
    u.nombre_completo as operador_actual,
    TIMESTAMPDIFF(MINUTE, op.fecha_inicio, NOW()) as minutos_en_produccion
FROM maquinas m
LEFT JOIN ordenes_produccion op ON m.id = op.maquina_id AND op.estado = 'en_proceso'
LEFT JOIN usuarios u ON op.operador_id = u.id
WHERE m.activo = TRUE;

-- Vista: Alertas no atendidas
CREATE OR REPLACE VIEW v_alertas_pendientes AS
SELECT 
    a.id,
    a.tipo_alerta,
    a.severidad,
    a.titulo,
    a.mensaje,
    a.entidad_tipo,
    a.entidad_id,
    u.nombre_completo as destinatario,
    TIMESTAMPDIFF(MINUTE, a.created_at, NOW()) as minutos_pendiente,
    a.created_at
FROM alertas a
LEFT JOIN usuarios u ON a.usuario_destino_id = u.id
WHERE a.leida = FALSE
ORDER BY a.severidad DESC, a.created_at ASC;

-- ============================================
-- DATOS INICIALES (SEED)
-- ============================================

-- Roles del sistema
INSERT INTO roles (nombre_rol, descripcion, nivel_acceso) VALUES
('Gerencia', 'Acceso total a reportes ejecutivos y toma de decisiones', 'total'),
('Científico de Datos', 'Análisis avanzado de KPIs y generación de insights', 'avanzado'),
('Administrador de Planta', 'Gestión de producción, inventarios y personal', 'avanzado'),
('Supervisor', 'Supervisión de turnos y calidad', 'intermedio'),
('Operador de Máquina', 'Operación de equipos y registro de producción', 'basico'),
('Inspector de Calidad', 'Control de calidad y certificaciones', 'intermedio'),
('Técnico de Mantenimiento', 'Mantenimiento preventivo y correctivo', 'basico');

-- Turnos de trabajo
INSERT INTO turnos (nombre_turno, hora_inicio, hora_fin, activo) VALUES
('Mañana', '06:00:00', '14:00:00', TRUE),
('Tarde', '14:00:00', '22:00:00', TRUE),
('Noche', '22:00:00', '06:00:00', TRUE);

-- Categorías de insumos
INSERT INTO categorias_insumos (nombre_categoria, descripcion, es_biodegradable) VALUES
('Biopolímeros Base', 'Resinas biodegradables principales', TRUE),
('Aditivos Biodegradables', 'Plastificantes, estabilizadores naturales', TRUE),
('Cargas Naturales', 'Almidón, celulosa, fibras', TRUE),
('Pigmentos Naturales', 'Colorantes de origen vegetal/mineral', TRUE),
('Auxiliares de Proceso', 'Desmoldantes, lubricantes biodegradables', TRUE);

-- Tipos de máquina
INSERT INTO tipos_maquina (nombre_tipo, descripcion) VALUES
('Inyectora', 'Máquina de inyección para piezas moldeadas'),
('Extrusora', 'Extrusora de film o lámina biodegradable'),
('Sopladora', 'Máquina de soplado para envases'),
('Termoformadora', 'Termoformado de bandejas y platos'),
('Molino', 'Molino para reciclaje de scrap'),
('Mezcladora', 'Mezcladora de compuestos y masterbatch');

-- Categorías de productos
INSERT INTO categorias_productos (nombre_categoria, descripcion, aplicacion) VALUES
('Bolsas Compostables', 'Bolsas biodegradables certificadas', 'Retail, Supermercados, Agricultura'),
('Envases Food Service', 'Vasos, platos, cubiertos desechables', 'Restaurantes, Catering, Eventos'),
('Contenedores Compostables', 'Cajas, bandejas para alimentos', 'Delivery, Takeaway, Empaques'),
('Film Biodegradable', 'Film stretch y shrink compostable', 'Embalaje, Agricultura'),
('Productos Agrícolas', 'Mulch film, macetas biodegradables', 'Agricultura, Horticultura');

-- Defectos de calidad típicos
INSERT INTO defectos_calidad (codigo_defecto, nombre_defecto, descripcion, severidad, activo) VALUES
('DEF-001', 'Mancha superficial', 'Contaminación visible en superficie', 'menor', TRUE),
('DEF-002', 'Deformación dimensional', 'Producto fuera de especificación dimensional', 'mayor', TRUE),
('DEF-003', 'Rebaba excesiva', 'Rebaba mayor a tolerancia permitida', 'menor', TRUE),
('DEF-004', 'Burbuja interna', 'Inclusión de aire en el material', 'menor', TRUE),
('DEF-005', 'Fisura o grieta', 'Ruptura parcial del material', 'critico', TRUE),
('DEF-006', 'Degradación térmica', 'Material quemado o degradado', 'critico', TRUE),
('DEF-007', 'Color no uniforme', 'Variación de color fuera de estándar', 'mayor', TRUE),
('DEF-008', 'Espesor irregular', 'Variación de espesor mayor a tolerancia', 'mayor', TRUE);

-- ============================================
-- PROCEDIMIENTOS ALMACENADOS
-- ============================================

DELIMITER //

-- Procedimiento para calcular OEE de una orden
CREATE PROCEDURE sp_calcular_oee_orden(IN p_orden_id INT)
BEGIN
    DECLARE v_tiempo_planificado INT;
    DECLARE v_tiempo_operacion INT;
    DECLARE v_tiempo_paros INT;
    DECLARE v_produccion_real INT;
    DECLARE v_produccion_teorica DECIMAL(10,2);
    DECLARE v_piezas_conformes INT;
    DECLARE v_piezas_producidas INT;
    DECLARE v_disponibilidad DECIMAL(5,2);
    DECLARE v_rendimiento DECIMAL(5,2);
    DECLARE v_calidad DECIMAL(5,2);
    DECLARE v_oee DECIMAL(5,2);
    
    -- Obtener datos de la orden
    SELECT 
        TIMESTAMPDIFF(MINUTE, fecha_inicio, COALESCE(fecha_fin, NOW())),
        cantidad_producida,
        cantidad_conforme
    INTO v_tiempo_planificado, v_piezas_producidas, v_piezas_conformes
    FROM ordenes_produccion
    WHERE id = p_orden_id;
    
    -- Calcular tiempo de paros
    SELECT COALESCE(SUM(duracion_minutos), 0)
    INTO v_tiempo_paros
    FROM paros_maquina pm
    JOIN ordenes_produccion op ON pm.maquina_id = op.maquina_id
    WHERE op.id = p_orden_id
    AND pm.fecha_inicio BETWEEN op.fecha_inicio AND COALESCE(op.fecha_fin, NOW());
    
    -- Tiempo de operación efectivo
    SET v_tiempo_operacion = v_tiempo_planificado - v_tiempo_paros;
    
    -- Calcular componentes del OEE
    SET v_disponibilidad = (v_tiempo_operacion / v_tiempo_planificado) * 100;
    
    -- Producción teórica (basada en tiempo ciclo estándar del producto)
    SELECT (v_tiempo_operacion * 60) / tiempo_ciclo_segundos
    INTO v_produccion_teorica
    FROM productos p
    JOIN ordenes_produccion op ON p.id = op.producto_id
    WHERE op.id = p_orden_id;
    
    SET v_rendimiento = (v_piezas_producidas / v_produccion_teorica) * 100;
    SET v_calidad = (v_piezas_conformes / v_piezas_producidas) * 100;
    SET v_oee = (v_disponibilidad * v_rendimiento * v_calidad) / 10000;
    
    -- Devolver resultados
    SELECT 
        v_disponibilidad as disponibilidad,
        v_rendimiento as rendimiento,
        v_calidad as calidad,
        v_oee as oee;
END//

DELIMITER ;

-- ============================================
-- TRIGGERS
-- ============================================

DELIMITER //

-- Trigger: Actualizar stock de insumos al registrar movimiento
CREATE TRIGGER tr_actualizar_stock_insumo
AFTER INSERT ON movimientos_inventario_insumos
FOR EACH ROW
BEGIN
    IF NEW.tipo_movimiento = 'entrada' THEN
        UPDATE insumos 
        SET stock_actual = stock_actual + NEW.cantidad
        WHERE id = NEW.insumo_id;
    ELSEIF NEW.tipo_movimiento IN ('salida', 'desperdicio') THEN
        UPDATE insumos 
        SET stock_actual = stock_actual - NEW.cantidad
        WHERE id = NEW.insumo_id;
    ELSEIF NEW.tipo_movimiento = 'ajuste' THEN
        UPDATE insumos 
        SET stock_actual = NEW.cantidad
        WHERE id = NEW.insumo_id;
    END IF;
END//

-- Trigger: Generar alerta si stock de insumo está bajo
CREATE TRIGGER tr_alerta_stock_bajo_insumo
AFTER UPDATE ON insumos
FOR EACH ROW
BEGIN
    IF NEW.stock_actual < NEW.stock_minimo AND OLD.stock_actual >= OLD.stock_minimo THEN
        INSERT INTO alertas (tipo_alerta, severidad, titulo, mensaje, entidad_tipo, entidad_id)
        VALUES (
            'stock_bajo',
            'advertencia',
            CONCAT('Stock bajo: ', NEW.nombre_insumo),
            CONCAT('El insumo ', NEW.nombre_insumo, ' tiene stock bajo. Actual: ', NEW.stock_actual, ' ', NEW.unidad_medida, '. Mínimo: ', NEW.stock_minimo, ' ', NEW.unidad_medida),
            'insumo',
            NEW.id
        );
    END IF;
END//

-- Trigger: Actualizar stock de productos al aprobar lote
CREATE TRIGGER tr_actualizar_stock_producto_lote
AFTER UPDATE ON lotes_produccion
FOR EACH ROW
BEGIN
    IF NEW.estado_lote = 'aprobado' AND OLD.estado_lote = 'cuarentena' THEN
        UPDATE productos p
        JOIN ordenes_produccion op ON p.id = op.producto_id
        SET p.stock_actual = p.stock_actual + NEW.cantidad
        WHERE op.id = NEW.orden_id;
    END IF;
END//

DELIMITER ;

-- ============================================
-- ÍNDICES ADICIONALES PARA OPTIMIZACIÓN
-- ============================================

CREATE INDEX idx_registros_fecha ON registros_produccion(fecha_hora);
CREATE INDEX idx_registros_maquina ON registros_produccion(maquina_id);
CREATE INDEX idx_ordenes_estado ON ordenes_produccion(estado);
CREATE INDEX idx_ordenes_fecha ON ordenes_produccion(fecha_programada);

-- ============================================
-- CONFIGURACIÓN FINAL
-- ============================================

SET FOREIGN_KEY_CHECKS = 1;

-- Otorgar permisos (ajustar según tu configuración)
-- GRANT ALL PRIVILEGES ON ecoplast_produccion.* TO 'ecoplast_user'@'localhost';
-- FLUSH PRIVILEGES;

SELECT 'Base de datos Ecoplast SRL creada exitosamente!' as mensaje;
SELECT COUNT(*) as total_tablas FROM information_schema.tables WHERE table_schema = 'ecoplast_produccion';

-- ============================================
-- FIN DEL SCRIPT
-- ============================================
