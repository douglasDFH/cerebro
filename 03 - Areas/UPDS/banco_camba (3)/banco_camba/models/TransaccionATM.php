<?php
/**
 * Modelo TransaccionATM
 * 
 * Este modelo gestiona todas las operaciones relacionadas con transacciones en ATMs
 */
class TransaccionATM {
    // Conexión a la base de datos
    private $conn;
    
    // Propiedades de la transacción ATM
    public $idTransaccionATM;
    public $hash;
    public $tipoTransaccion; // 'retiro', 'deposito', 'consulta'
    public $fecha;
    public $hora;
    public $descripcion;
    public $monto;
    public $idCuenta;
    public $cuentaOrigen;
    public $cuentaDestino;
    public $saldoResultante;
    public $idATM;
    public $idTarjeta;
    
    /**
     * Constructor con DB
     * @param PDO $db
     */
    public function __construct($db) {
        $this->conn = $db;
    }
    
    /**
     * Obtener todas las transacciones ATM
     * @return PDOStatement
     */
    public function obtenerTodas() {
        try {
            // Primero, verificar si la tabla atm tiene la columna 'nombre'
            $checkQuery = "SHOW COLUMNS FROM atm LIKE 'nombre'";
            $checkStmt = $this->conn->prepare($checkQuery);
            $checkStmt->execute();
            $column = $checkStmt->fetch(PDO::FETCH_ASSOC);

            // Si no existe la columna 'nombre', podría ser otro nombre
            if (!$column) {
                // Consulta sin join a atm o usar el campo adecuado
                $query = "SELECT ta.*, c.nroCuenta, 
                        CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) AS cliente_nombre
                        FROM transaccion_atm ta
                        INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                        INNER JOIN persona p ON c.idPersona = p.idPersona
                        ORDER BY ta.fecha DESC, ta.hora DESC";
            } else {
                // La columna existe, usar la consulta completa
                $query = "SELECT ta.*, c.nroCuenta, 
                        a.nombre AS nombreATM, a.ubicacion,
                        CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) AS cliente_nombre
                        FROM transaccion_atm ta
                        INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                        INNER JOIN atm a ON ta.idATM = a.idATM
                        INNER JOIN persona p ON c.idPersona = p.idPersona
                        ORDER BY ta.fecha DESC, ta.hora DESC";
            }
            
            $stmt = $this->conn->prepare($query);
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            // Log del error para depuración
            error_log('Error en obtenerTodas: ' . $e->getMessage());
            
            // Consulta de respaldo simplificada sin joins problemáticos
            $query = "SELECT * FROM transaccion_atm ORDER BY fecha DESC, hora DESC";
            $stmt = $this->conn->prepare($query);
            $stmt->execute();
            
            return $stmt;
        }
    }
    
    /**
     * Obtener una transacción ATM
     * @return boolean
     */
    public function obtenerUna() {
        try {
            $query = "SELECT ta.*, c.nroCuenta, a.nombre as nombreATM, a.ubicacion,
                    CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                    FROM transaccion_atm ta
                    INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                    INNER JOIN atm a ON ta.idATM = a.idATM
                    INNER JOIN persona p ON c.idPersona = p.idPersona
                    WHERE ta.idTransaccionATM = :id";
            
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':id', $this->idTransaccionATM);
            $stmt->execute();
            
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
            
            if ($row) {
                $this->hash = $row['hash'];
                $this->tipoTransaccion = $row['tipoTransaccion'];
                $this->fecha = $row['fecha'];
                $this->hora = $row['hora'];
                $this->descripcion = $row['descripcion'];
                $this->monto = $row['monto'];
                $this->idCuenta = $row['idCuenta'];
                $this->cuentaOrigen = $row['cuentaOrigen'];
                $this->cuentaDestino = $row['cuentaDestino'];
                $this->saldoResultante = $row['saldoResultante'];
                $this->idATM = $row['idATM'];
                $this->idTarjeta = $row['idTarjeta'];
                return true;
            }
            
            return false;
        } catch (Exception $e) {
            error_log('Error en obtenerUna: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Obtener transacciones por ATM
     * @param int $idATM
     * @return PDOStatement
     */
    public function obtenerPorATM($idATM) {
        try {
            $query = "SELECT ta.*, c.nroCuenta, a.nombre as nombreATM, a.ubicacion,
                    CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                    FROM transaccion_atm ta
                    INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                    INNER JOIN atm a ON ta.idATM = a.idATM
                    INNER JOIN persona p ON c.idPersona = p.idPersona
                    WHERE ta.idATM = :idATM
                    ORDER BY ta.fecha DESC, ta.hora DESC";
            
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':idATM', $idATM);
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            error_log('Error en obtenerPorATM: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Obtener transacciones por cuenta
     * @param int $idCuenta
     * @return PDOStatement
     */
    public function obtenerPorCuenta($idCuenta) {
        try {
            $query = "SELECT ta.*, c.nroCuenta, a.nombre as nombreATM, a.ubicacion,
                    CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                    FROM transaccion_atm ta
                    INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                    INNER JOIN atm a ON ta.idATM = a.idATM
                    INNER JOIN persona p ON c.idPersona = p.idPersona
                    WHERE ta.idCuenta = :idCuenta
                    ORDER BY ta.fecha DESC, ta.hora DESC";
            
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':idCuenta', $idCuenta);
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            error_log('Error en obtenerPorCuenta: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Obtener transacciones por tarjeta
     * @param int $idTarjeta
     * @return PDOStatement
     */
    public function obtenerPorTarjeta($idTarjeta) {
        try {
            $query = "SELECT ta.*, c.nroCuenta, a.nombre as nombreATM, a.ubicacion,
                    CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                    FROM transaccion_atm ta
                    INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                    INNER JOIN atm a ON ta.idATM = a.idATM
                    INNER JOIN persona p ON c.idPersona = p.idPersona
                    WHERE ta.idTarjeta = :idTarjeta
                    ORDER BY ta.fecha DESC, ta.hora DESC";
            
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':idTarjeta', $idTarjeta);
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            error_log('Error en obtenerPorTarjeta: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Buscar transacciones ATM
     * @param array $criterios
     * @return PDOStatement
     */
    public function buscar($criterios) {
        try {
            $condiciones = [];
            $parametros = [];
            
            // Construir condiciones según criterios
            if (isset($criterios['idATM']) && $criterios['idATM'] > 0) {
                $condiciones[] = "ta.idATM = :idATM";
                $parametros[':idATM'] = $criterios['idATM'];
            }
            
            if (isset($criterios['idCuenta']) && $criterios['idCuenta'] > 0) {
                $condiciones[] = "ta.idCuenta = :idCuenta";
                $parametros[':idCuenta'] = $criterios['idCuenta'];
            }
            
            if (isset($criterios['idTarjeta']) && $criterios['idTarjeta'] > 0) {
                $condiciones[] = "ta.idTarjeta = :idTarjeta";
                $parametros[':idTarjeta'] = $criterios['idTarjeta'];
            }
            
            if (isset($criterios['tipoTransaccion']) && $criterios['tipoTransaccion'] != '') {
                $condiciones[] = "ta.tipoTransaccion = :tipoTransaccion";
                $parametros[':tipoTransaccion'] = $criterios['tipoTransaccion'];
            }
            
            if (isset($criterios['fechaInicio']) && $criterios['fechaInicio'] != '') {
                $condiciones[] = "ta.fecha >= :fechaInicio";
                $parametros[':fechaInicio'] = $criterios['fechaInicio'];
            }
            
            if (isset($criterios['fechaFin']) && $criterios['fechaFin'] != '') {
                $condiciones[] = "ta.fecha <= :fechaFin";
                $parametros[':fechaFin'] = $criterios['fechaFin'];
            }
            
            // Construir la consulta SQL base
            $query = "SELECT ta.*, c.nroCuenta, a.nombre as nombreATM, a.ubicacion,
                    CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                    FROM transaccion_atm ta
                    INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                    INNER JOIN atm a ON ta.idATM = a.idATM
                    INNER JOIN persona p ON c.idPersona = p.idPersona";
            
            // Agregar condiciones si existen
            if (!empty($condiciones)) {
                $query .= " WHERE " . implode(' AND ', $condiciones);
            }
            
            // Agregar orden
            $query .= " ORDER BY ta.fecha DESC, ta.hora DESC";
            
            // Preparar y ejecutar consulta
            $stmt = $this->conn->prepare($query);
            
            // Vincular parámetros
            foreach ($parametros as $key => &$valor) {
                $stmt->bindParam($key, $valor);
            }
            
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            error_log('Error en buscar: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Crear una transacción ATM
     * @return boolean
     */
    public function crear() {
        try {
            // Si no se proporciona fecha y hora, usar la fecha y hora actual
            if (empty($this->fecha)) {
                $this->fecha = date('Y-m-d');
            }
            
            if (empty($this->hora)) {
                $this->hora = date('H:i:s');
            }
            
            // Generar hash único si no existe
            if (empty($this->hash)) {
                $this->hash = bin2hex(random_bytes(16));
            }
            
            $query = "INSERT INTO transaccion_atm
                    (hash, tipoTransaccion, fecha, hora, descripcion, monto, idCuenta, 
                    cuentaOrigen, cuentaDestino, saldoResultante, idATM, idTarjeta)
                    VALUES
                    (:hash, :tipoTransaccion, :fecha, :hora, :descripcion, :monto, :idCuenta, 
                    :cuentaOrigen, :cuentaDestino, :saldoResultante, :idATM, :idTarjeta)";
            
            $stmt = $this->conn->prepare($query);
            
            // Limpiar datos
            $this->descripcion = htmlspecialchars(strip_tags($this->descripcion));
            
            // Vincular datos
            $stmt->bindParam(':hash', $this->hash);
            $stmt->bindParam(':tipoTransaccion', $this->tipoTransaccion);
            $stmt->bindParam(':fecha', $this->fecha);
            $stmt->bindParam(':hora', $this->hora);
            $stmt->bindParam(':descripcion', $this->descripcion);
            $stmt->bindParam(':monto', $this->monto);
            $stmt->bindParam(':idCuenta', $this->idCuenta);
            $stmt->bindParam(':cuentaOrigen', $this->cuentaOrigen);
            $stmt->bindParam(':cuentaDestino', $this->cuentaDestino);
            $stmt->bindParam(':saldoResultante', $this->saldoResultante);
            $stmt->bindParam(':idATM', $this->idATM);
            $stmt->bindParam(':idTarjeta', $this->idTarjeta);
            
            // Ejecutar consulta
            if ($stmt->execute()) {
                $this->idTransaccionATM = $this->conn->lastInsertId();
                return true;
            }
            
            return false;
        } catch (Exception $e) {
            error_log('Error en crear: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Actualizar una transacción ATM
     * @return boolean
     */
    public function actualizar() {
        try {
            $query = "UPDATE transaccion_atm SET
                    hash = :hash,
                    tipoTransaccion = :tipoTransaccion,
                    fecha = :fecha,
                    hora = :hora,
                    descripcion = :descripcion,
                    monto = :monto,
                    idCuenta = :idCuenta,
                    cuentaOrigen = :cuentaOrigen,
                    cuentaDestino = :cuentaDestino,
                    saldoResultante = :saldoResultante,
                    idATM = :idATM,
                    idTarjeta = :idTarjeta
                    WHERE idTransaccionATM = :idTransaccionATM";
            
            $stmt = $this->conn->prepare($query);
            
            // Limpiar datos
            $this->descripcion = htmlspecialchars(strip_tags($this->descripcion));
            
            // Vincular datos
            $stmt->bindParam(':idTransaccionATM', $this->idTransaccionATM);
            $stmt->bindParam(':hash', $this->hash);
            $stmt->bindParam(':tipoTransaccion', $this->tipoTransaccion);
            $stmt->bindParam(':fecha', $this->fecha);
            $stmt->bindParam(':hora', $this->hora);
            $stmt->bindParam(':descripcion', $this->descripcion);
            $stmt->bindParam(':monto', $this->monto);
            $stmt->bindParam(':idCuenta', $this->idCuenta);
            $stmt->bindParam(':cuentaOrigen', $this->cuentaOrigen);
            $stmt->bindParam(':cuentaDestino', $this->cuentaDestino);
            $stmt->bindParam(':saldoResultante', $this->saldoResultante);
            $stmt->bindParam(':idATM', $this->idATM);
            $stmt->bindParam(':idTarjeta', $this->idTarjeta);
            
            // Ejecutar consulta
            if ($stmt->execute()) {
                return true;
            }
            
            return false;
        } catch (Exception $e) {
            error_log('Error en actualizar: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Eliminar una transacción ATM
     * @return boolean
     */
    public function eliminar() {
        try {
            $query = "DELETE FROM transaccion_atm WHERE idTransaccionATM = :idTransaccionATM";
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':idTransaccionATM', $this->idTransaccionATM);
            
            if ($stmt->execute()) {
                return true;
            }
            
            return false;
        } catch (Exception $e) {
            error_log('Error en eliminar: ' . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Contar transacciones ATM del día para un ATM específico
     * @param int $idATM
     * @return int
     */
    public function contarTransaccionesHoy($idATM) {
        try {
            $hoy = date('Y-m-d');
            $query = "SELECT COUNT(*) as total 
                    FROM transaccion_atm
                    WHERE idATM = :idATM AND fecha = :fecha";
            
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':idATM', $idATM);
            $stmt->bindParam(':fecha', $hoy);
            $stmt->execute();
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
            
            return $row['total'];
        } catch (Exception $e) {
            error_log('Error en contarTransaccionesHoy: ' . $e->getMessage());
            return 0;
        }
    }
    
    /**
     * Obtener transacciones ATM recientes
     * @param int $limit Número de transacciones a obtener
     * @return PDOStatement
     */
    public function obtenerRecientes($limit = 10) {
        try {
            $query = "SELECT ta.*, c.nroCuenta, a.nombre as nombreATM, a.ubicacion,
                    CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                    FROM transaccion_atm ta
                    INNER JOIN cuenta c ON ta.idCuenta = c.idCuenta
                    INNER JOIN atm a ON ta.idATM = a.idATM
                    INNER JOIN persona p ON c.idPersona = p.idPersona
                    ORDER BY ta.fecha DESC, ta.hora DESC
                    LIMIT :limit";
            
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':limit', $limit, PDO::PARAM_INT);
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            error_log('Error en obtenerRecientes: ' . $e->getMessage());
            return false;
        }
    }
}
?>