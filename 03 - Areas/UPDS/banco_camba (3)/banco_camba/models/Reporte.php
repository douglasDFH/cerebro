<?php
/**
 * Modelo para Actividad de Cuenta
 */
class ActividadCuenta {
    private $db;
    
    /**
     * Constructor
     */
    public function __construct($database) {
        $this->db = $database;
    }
    
    /**
     * Obtener todas las cuentas
     */
    public function obtenerTodasCuentas() {
        $query = "SELECT c.idCuenta, c.nroCuenta, 
                 CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre,
                 c.saldo, c.fechaApertura
                 FROM cuenta c
                 INNER JOIN persona p ON c.idPersona = p.idPersona
                 ORDER BY p.nombre, c.fechaApertura DESC";
        
        $stmt = $this->db->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener actividad de una cuenta en un rango de fechas
     */
    public function obtenerActividadCuenta($idCuenta, $fechaInicio, $fechaFin) {
        $query = "SELECT t.idTransaccion, t.fecha, t.hora, t.monto, t.tipoTransaccion, 
                  t.descripcion, t.saldoResultante
                  FROM transaccion t
                  WHERE t.idCuenta = :idCuenta 
                  AND t.fecha BETWEEN :fechaInicio AND :fechaFin
                  ORDER BY t.fecha DESC, t.hora DESC";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener resumen de actividad de cuenta
     */
    public function obtenerResumenActividad($idCuenta, $fechaInicio, $fechaFin) {
        $query = "SELECT 
                  COUNT(*) as total_transacciones,
                  SUM(CASE WHEN tipoTransaccion = 1 THEN 1 ELSE 0 END) as total_retiros,
                  SUM(CASE WHEN tipoTransaccion = 2 THEN 1 ELSE 0 END) as total_depositos,
                  SUM(CASE WHEN tipoTransaccion = 1 THEN monto ELSE 0 END) as monto_retiros,
                  SUM(CASE WHEN tipoTransaccion = 2 THEN monto ELSE 0 END) as monto_depositos,
                  MAX(fecha) as ultima_transaccion
                  FROM transaccion 
                  WHERE idCuenta = :idCuenta
                  AND fecha BETWEEN :fechaInicio AND :fechaFin";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt->fetch(PDO::FETCH_ASSOC);
    }
    
    /**
     * Obtener detalles de una cuenta
     */
    public function obtenerDetalleCuenta($idCuenta) {
        $query = "SELECT c.idCuenta, c.nroCuenta, c.saldo, c.tipoMoneda, c.fechaApertura,
                  p.idPersona, p.nombre, p.apellidoPaterno, p.apellidoMaterno, p.ci
                  FROM cuenta c
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  WHERE c.idCuenta = :idCuenta";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->execute();
        
        return $stmt->fetch(PDO::FETCH_ASSOC);
    }
}

/**
 * Modelo para Resumen Ejecutivo
 */
class ResumenEjecutivo {
    private $db;
    
    /**
     * Constructor
     */
    public function __construct($database) {
        $this->db = $database;
    }
    
    /**
     * Obtener estadísticas generales
     */
    public function obtenerEstadisticasGenerales($fechaInicio, $fechaFin) {
        $query = "SELECT
                  (SELECT COUNT(*) FROM persona WHERE tipo = 'cliente') as total_clientes,
                  (SELECT COUNT(*) FROM cuenta) as total_cuentas,
                  (SELECT SUM(saldo) FROM cuenta WHERE tipoMoneda = 1) as total_saldo_bolivianos,
                  (SELECT SUM(saldo) FROM cuenta WHERE tipoMoneda = 2) as total_saldo_dolares,
                  (SELECT COUNT(*) FROM transaccion WHERE fecha BETWEEN :fechaInicio AND :fechaFin) as total_transacciones,
                  (SELECT SUM(monto) FROM transaccion WHERE tipoTransaccion = 1 AND fecha BETWEEN :fechaInicio AND :fechaFin) as total_retiros,
                  (SELECT SUM(monto) FROM transaccion WHERE tipoTransaccion = 2 AND fecha BETWEEN :fechaInicio AND :fechaFin) as total_depositos";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt->fetch(PDO::FETCH_ASSOC);
    }
    
    /**
     * Obtener transacciones por día en un rango de fechas
     */
    public function obtenerTransaccionesPorDia($fechaInicio, $fechaFin) {
        $query = "SELECT 
                  fecha,
                  COUNT(*) as total,
                  SUM(CASE WHEN tipoTransaccion = 1 THEN 1 ELSE 0 END) as retiros,
                  SUM(CASE WHEN tipoTransaccion = 2 THEN 1 ELSE 0 END) as depositos,
                  SUM(CASE WHEN tipoTransaccion = 1 THEN monto ELSE 0 END) as monto_retiros,
                  SUM(CASE WHEN tipoTransaccion = 2 THEN monto ELSE 0 END) as monto_depositos
                  FROM transaccion
                  WHERE fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY fecha
                  ORDER BY fecha DESC";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener las oficinas con más transacciones
     */
    public function obtenerOficinasTopTransacciones($fechaInicio, $fechaFin, $limite = 5) {
        $query = "SELECT o.idOficina, o.nombre,
                  COUNT(t.idTransaccion) as total_transacciones
                  FROM oficina o
                  INNER JOIN persona p ON o.idOficina = p.idOficina
                  INNER JOIN cuenta c ON p.idPersona = c.idPersona
                  INNER JOIN transaccion t ON c.idCuenta = t.idCuenta
                  WHERE t.fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY o.idOficina, o.nombre
                  ORDER BY total_transacciones DESC
                  LIMIT :limite";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->bindParam(':limite', $limite, PDO::PARAM_INT);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener los ATM con más transacciones
     */
    public function obtenerATMsTopTransacciones($fechaInicio, $fechaFin, $limite = 5) {
        $query = "SELECT a.idATM, a.ubicacion,
                  COUNT(ta.idTransaccion) as total_transacciones
                  FROM atm a
                  INNER JOIN transaccion_atm ta ON a.idATM = ta.idATM
                  INNER JOIN transaccion t ON ta.idTransaccion = t.idTransaccion
                  WHERE t.fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY a.idATM, a.ubicacion
                  ORDER BY total_transacciones DESC
                  LIMIT :limite";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->bindParam(':limite', $limite, PDO::PARAM_INT);
        $stmt->execute();
        
        return $stmt;
    }
}

/**
 * Modelo para Rendimiento por Producto
 */
class RendimientoProducto {
    private $db;
    
    /**
     * Constructor
     */
    public function __construct($database) {
        $this->db = $database;
    }
    
    /**
     * Obtener estadísticas de todos los productos
     */
    public function obtenerEstadisticasProductos() {
        // Asumiendo que tienes una tabla Producto
        $query = "SELECT p.idProducto, p.nombre, p.tipo,
                  COUNT(c.idCuenta) as total_cuentas,
                  SUM(c.saldo) as saldo_total,
                  AVG(c.saldo) as saldo_promedio,
                  MIN(c.fechaApertura) as primera_apertura,
                  MAX(c.fechaApertura) as ultima_apertura
                  FROM producto p
                  LEFT JOIN cuenta c ON p.idProducto = c.idProducto
                  GROUP BY p.idProducto, p.nombre, p.tipo
                  ORDER BY p.tipo, p.nombre";
        
        $stmt = $this->db->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener transacciones por producto en un rango de fechas
     */
    public function obtenerTransaccionesPorProducto($fechaInicio, $fechaFin) {
        $query = "SELECT p.idProducto, p.nombre, p.tipo,
                  COUNT(t.idTransaccion) as total_transacciones,
                  SUM(CASE WHEN t.tipoTransaccion = 1 THEN 1 ELSE 0 END) as total_retiros,
                  SUM(CASE WHEN t.tipoTransaccion = 2 THEN 1 ELSE 0 END) as total_depositos,
                  SUM(CASE WHEN t.tipoTransaccion = 1 THEN t.monto ELSE 0 END) as monto_retiros,
                  SUM(CASE WHEN t.tipoTransaccion = 2 THEN t.monto ELSE 0 END) as monto_depositos
                  FROM producto p
                  LEFT JOIN cuenta c ON p.idProducto = c.idProducto
                  LEFT JOIN transaccion t ON c.idCuenta = t.idCuenta
                  WHERE t.fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY p.idProducto, p.nombre, p.tipo
                  ORDER BY p.tipo, p.nombre";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener tendencias de crecimiento por producto
     */
    public function obtenerTendenciasCrecimiento($mesesAtras = 6) {
        $query = "SELECT p.idProducto, p.nombre, p.tipo,
                  YEAR(c.fechaApertura) as anio,
                  MONTH(c.fechaApertura) as mes,
                  COUNT(c.idCuenta) as nuevas_cuentas
                  FROM producto p
                  LEFT JOIN cuenta c ON p.idProducto = c.idProducto
                  WHERE c.fechaApertura >= DATE_SUB(CURDATE(), INTERVAL :mesesAtras MONTH)
                  GROUP BY p.idProducto, p.nombre, p.tipo, YEAR(c.fechaApertura), MONTH(c.fechaApertura)
                  ORDER BY p.tipo, p.nombre, anio, mes";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':mesesAtras', $mesesAtras, PDO::PARAM_INT);
        $stmt->execute();
        
        return $stmt;
    }
}

/**
 * Modelo para Auditoría del Sistema
 */
class AuditoriaSistema {
    private $db;
    
    /**
     * Constructor
     */
    public function __construct($database) {
        $this->db = $database;
    }
    
    /**
     * Obtener registros de auditoría en un rango de fechas
     */
    public function obtenerRegistrosAuditoria($fechaInicio, $fechaFin, $idUsuario = null, $tipoAccion = null) {
        $query = "SELECT a.idAuditoria, a.idUsuario, a.fechaHora, a.accion, a.tabla, 
                  a.idRegistro, a.detalles, u.usuario as nombre_usuario
                  FROM auditoria a
                  LEFT JOIN usuario u ON a.idUsuario = u.idUsuario
                  WHERE DATE(a.fechaHora) BETWEEN :fechaInicio AND :fechaFin";
        
        // Agregar filtros opcionales
        if ($idUsuario) {
            $query .= " AND a.idUsuario = :idUsuario";
        }
        
        if ($tipoAccion) {
            $query .= " AND a.accion = :tipoAccion";
        }
        
        $query .= " ORDER BY a.fechaHora DESC";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        
        if ($idUsuario) {
            $stmt->bindParam(':idUsuario', $idUsuario);
        }
        
        if ($tipoAccion) {
            $stmt->bindParam(':tipoAccion', $tipoAccion);
        }
        
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener usuarios del sistema
     */
    public function obtenerUsuarios() {
        $query = "SELECT idUsuario, usuario, nombre, rol
                  FROM usuario
                  ORDER BY usuario";
        
        $stmt = $this->db->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener tipos de acciones de auditoría
     */
    public function obtenerTiposAcciones() {
        $query = "SELECT DISTINCT accion
                  FROM auditoria
                  ORDER BY accion";
        
        $stmt = $this->db->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener estadísticas de auditoría
     */
    public function obtenerEstadisticasAuditoria($fechaInicio, $fechaFin) {
        $query = "SELECT 
                  COUNT(*) as total_registros,
                  COUNT(DISTINCT idUsuario) as total_usuarios,
                  accion, COUNT(*) as cantidad
                  FROM auditoria
                  WHERE DATE(fechaHora) BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY accion
                  ORDER BY cantidad DESC";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
}

/**
 * Modelo para Métricas de Servicio
 */
class MetricasServicio {
    private $db;
    
    /**
     * Constructor
     */
    public function __construct($database) {
        $this->db = $database;
    }
    
    /**
     * Obtener tiempo promedio de atención por oficina
     */
    public function obtenerTiempoPromedioAtencion($fechaInicio, $fechaFin) {
        // Asumiendo que tienes una tabla de Atencion o similar
        $query = "SELECT o.idOficina, o.nombre,
                  AVG(a.tiempoAtencion) as tiempo_promedio,
                  COUNT(a.idAtencion) as total_atenciones
                  FROM oficina o
                  LEFT JOIN atencion a ON o.idOficina = a.idOficina
                  WHERE a.fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY o.idOficina, o.nombre
                  ORDER BY o.nombre";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener satisfacción de clientes por oficina
     */
    public function obtenerSatisfaccionClientes($fechaInicio, $fechaFin) {
        // Asumiendo que tienes una tabla de Encuesta o similar
        $query = "SELECT o.idOficina, o.nombre,
                  AVG(e.calificacion) as satisfaccion_promedio,
                  COUNT(e.idEncuesta) as total_encuestas
                  FROM oficina o
                  LEFT JOIN encuesta e ON o.idOficina = e.idOficina
                  WHERE e.fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY o.idOficina, o.nombre
                  ORDER BY o.nombre";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener eficiencia de cajeros
     */
    public function obtenerEficienciaCajeros($fechaInicio, $fechaFin) {
        // Asumiendo que tienes una relación entre Usuario y Transaccion
        $query = "SELECT u.idUsuario, u.usuario, u.nombre,
                  COUNT(t.idTransaccion) as total_transacciones,
                  AVG(t.tiempo_proceso) as tiempo_promedio
                  FROM usuario u
                  LEFT JOIN transaccion t ON u.idUsuario = t.idUsuario
                  WHERE u.rol = 'cajero' AND t.fecha BETWEEN :fechaInicio AND :fechaFin
                  GROUP BY u.idUsuario, u.usuario, u.nombre
                  ORDER BY total_transacciones DESC";
        
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener métricas generales de servicio
     */
    public function obtenerMetricasGenerales($fechaInicio, $fechaFin) {
        // Métricas simuladas - adaptar según la estructura real de la base de datos
        $metricas = [
            [
                'nombre' => 'Tiempo promedio de espera',
                'valor' => '8 minutos',
                'cambio' => '-0.5',
                'tendencia' => 'mejora'
            ],
            [
                'nombre' => 'Satisfacción del cliente',
                'valor' => '4.2/5',
                'cambio' => '+0.3',
                'tendencia' => 'mejora'
            ],
            [
                'nombre' => 'Tiempo promedio de transacción',
                'valor' => '3.5 minutos',
                'cambio' => '-0.2',
                'tendencia' => 'mejora'
            ],
            [
                'nombre' => 'Transacciones por hora',
                'valor' => '18',
                'cambio' => '+2',
                'tendencia' => 'mejora'
            ],
            [
                'nombre' => 'Tasa de errores',
                'valor' => '0.8%',
                'cambio' => '-0.3',
                'tendencia' => 'mejora'
            ],
            [
                'nombre' => 'Tasa de resolución en primer contacto',
                'valor' => '85%',
                'cambio' => '+5',
                'tendencia' => 'mejora'
            ]
        ];
        
        return $metricas;
    }
}
?>