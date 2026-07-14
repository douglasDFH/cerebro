<?php
/**
 * Modelo Transacción Mejorado
 * Gestiona las operaciones con transacciones en la base de datos
 * Fecha de actualización: 09/03/2025
 */
class Transaccion {
    // Conexión a la base de datos
    private $conn;
    
    // Propiedades de la transacción
    public $idTransaccion;
    public $tipoTransaccion; // 1: Retiro, 2: Depósito, 3: Transferencia recibida, 4: Transferencia enviada
    public $fecha;
    public $hora;
    public $descripcion;
    public $monto;
    public $idCuenta;
    public $saldoResultante; 
    public $cuentaOrigen;    
    public $cuentaDestino;   
    public $hash;            
    
    /**
     * Constructor con DB
     * @param PDO $db
     */
    public function __construct($db) {
        $this->conn = $db;
    }
    
    /**
     * Obtener todas las transacciones
     * @return PDOStatement
     */
    public function obtenerTodas() {
        $query = "SELECT t.*, c.nroCuenta, 
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM transaccion t
                  INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  ORDER BY t.fecha DESC, t.hora DESC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener una transacción
     * @return boolean
     */
    public function obtenerUna() {
        $query = "SELECT t.*, c.nroCuenta,
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM transaccion t
                  INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  WHERE t.idTransaccion = :id";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':id', $this->idTransaccion);
        $stmt->execute();
        
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        
        if ($row) {
            $this->tipoTransaccion = $row['tipoTransaccion'];
            $this->fecha = $row['fecha'];
            $this->hora = $row['hora'];
            $this->descripcion = $row['descripcion'];
            $this->monto = $row['monto'];
            $this->idCuenta = $row['idCuenta'];
            $this->saldoResultante = isset($row['saldoResultante']) ? $row['saldoResultante'] : true;
            $this->cuentaOrigen = isset($row['cuentaOrigen']) ? $row['cuentaOrigen'] : true;
            $this->cuentaDestino = isset($row['cuentaDestino']) ? $row['cuentaDestino'] : true;
            $this->hash = isset($row['hash']) ? $row['hash'] : true;
            return true;
        }
        
        return false;
    }
    
    /**
     * Obtener transacciones por cuenta
     * @param int $idCuenta
     * @return PDOStatement
     */
    public function obtenerPorCuenta($idCuenta) {
        $query = "SELECT * FROM transaccion 
                  WHERE idCuenta = :idCuenta 
                  ORDER BY fecha DESC, hora DESC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener transacciones por cuenta y rango de fechas
     * @param int $idCuenta
     * @param string $fechaInicio
     * @param string $fechaFin
     * @return PDOStatement
     */
    public function obtenerPorRangoFechas($idCuenta, $fechaInicio, $fechaFin) {
        $query = "SELECT * FROM transaccion 
                  WHERE idCuenta = :idCuenta 
                  AND fecha BETWEEN :fechaInicio AND :fechaFin
                  ORDER BY fecha DESC, hora DESC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener transacciones por rango de fechas global (todos los usuarios)
     * @param string $fechaInicio
     * @param string $fechaFin
     * @return PDOStatement
     */
    public function obtenerPorRangoFechasGlobal($fechaInicio, $fechaFin) {
        $query = "SELECT t.*, c.nroCuenta, 
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM transaccion t
                  INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  WHERE t.fecha BETWEEN :fechaInicio AND :fechaFin
                  ORDER BY t.fecha DESC, t.hora DESC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':fechaInicio', $fechaInicio);
        $stmt->bindParam(':fechaFin', $fechaFin);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Crear una transacción
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
            
            // Crear la transacción
            $query = "INSERT INTO transaccion
                     (tipoTransaccion, fecha, hora, descripcion, monto, idCuenta, 
                      saldoResultante, cuentaOrigen, cuentaDestino, hash)
                     VALUES
                     (:tipoTransaccion, :fecha, :hora, :descripcion, :monto, :idCuenta,
                      :saldoResultante, :cuentaOrigen, :cuentaDestino, :hash)";
            
            $stmt = $this->conn->prepare($query);
            
            // Limpiar datos
            $this->descripcion = htmlspecialchars(strip_tags($this->descripcion));
            
            // Vincular datos
            $stmt->bindParam(':tipoTransaccion', $this->tipoTransaccion);
            $stmt->bindParam(':fecha', $this->fecha);
            $stmt->bindParam(':hora', $this->hora);
            $stmt->bindParam(':descripcion', $this->descripcion);
            $stmt->bindParam(':monto', $this->monto);
            $stmt->bindParam(':idCuenta', $this->idCuenta);
            $stmt->bindParam(':saldoResultante', $this->saldoResultante);
            $stmt->bindParam(':cuentaOrigen', $this->cuentaOrigen);
            $stmt->bindParam(':cuentaDestino', $this->cuentaDestino);
            $stmt->bindParam(':hash', $this->hash);
            
            // Ejecutar consulta
            $stmt->execute();
            
            // Obtener el ID de la transacción creada
            $this->idTransaccion = $this->conn->lastInsertId();
            return true;
            
        } catch (Exception $e) {
            error_log("Error al crear transacción: " . $e->getMessage());
            return false;
        }
    }
    
    /**
     * Crear transacción en una transacción de DB
     * Para usarse cuando se manejan varias operaciones en una sola transacción
     * @return boolean
     */
    public function crearEnTransaccion() {
        try {
            // Si no se proporciona fecha y hora, usar la fecha y hora actual
            if (empty($this->fecha)) {
                $this->fecha = date('Y-m-d');
            }
            
            if (empty($this->hora)) {
                $this->hora = date('H:i:s');
            }
            
            // Crear la transacción
            $query = "INSERT INTO transaccion
                     (tipoTransaccion, fecha, hora, descripcion, monto, idCuenta, 
                      saldoResultante, cuentaOrigen, cuentaDestino, hash)
                     VALUES
                     (:tipoTransaccion, :fecha, :hora, :descripcion, :monto, :idCuenta,
                      :saldoResultante, :cuentaOrigen, :cuentaDestino, :hash)";
            
            $stmt = $this->conn->prepare($query);
            
            // Limpiar datos
            $this->descripcion = htmlspecialchars(strip_tags($this->descripcion));
            
            // Vincular datos
            $stmt->bindParam(':tipoTransaccion', $this->tipoTransaccion);
            $stmt->bindParam(':fecha', $this->fecha);
            $stmt->bindParam(':hora', $this->hora);
            $stmt->bindParam(':descripcion', $this->descripcion);
            $stmt->bindParam(':monto', $this->monto);
            $stmt->bindParam(':idCuenta', $this->idCuenta);
            $stmt->bindParam(':saldoResultante', $this->saldoResultante);
            $stmt->bindParam(':cuentaOrigen', $this->cuentaOrigen);
            $stmt->bindParam(':cuentaDestino', $this->cuentaDestino);
            $stmt->bindParam(':hash', $this->hash);
            
            // Ejecutar consulta
            $stmt->execute();
            
            // Obtener el ID de la transacción creada
            $this->idTransaccion = $this->conn->lastInsertId();
            return true;
            
        } catch (Exception $e) {
            throw new Exception("Error al crear transacción: " . $e->getMessage());
        }
    }
    
    /**
     * Realiza un depósito en la cuenta
     * @param int $idCuenta ID de la cuenta
     * @param float $monto Monto a depositar
     * @param string $descripcion Descripción de la transacción
     * @return boolean
     */
    public function realizarDeposito($idCuenta, $monto, $descripcion = 'Depósito') {
        // Iniciar transacción en la base de datos
        $this->conn->beginTransaction();
        
        try {
            // Obtener cuenta
            $cuenta = new Cuenta($this->conn);
            $cuenta->idCuenta = $idCuenta;
            
            if (!$cuenta->obtenerUna()) {
                throw new Exception("Cuenta no encontrada.");
            }
            
            // Verificar que la cuenta esté activa
          //  if ($cuenta->estado = 1) {
          //      throw new Exception("La cuenta no está activa.");
           // }
            
            // Actualizar saldo de la cuenta
            $nuevoSaldo = $cuenta->saldo + $monto;
            
            $query = "UPDATE cuenta SET saldo = :saldo WHERE idCuenta = :idCuenta";
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':saldo', $nuevoSaldo);
            $stmt->bindParam(':idCuenta', $idCuenta);
            $stmt->execute();
            
            // Crear transacción
            $this->idCuenta = $idCuenta;
            $this->tipoTransaccion = 2; // Depósito
            $this->monto = $monto;
            $this->descripcion = $descripcion;
            $this->fecha = date('Y-m-d');
            $this->hora = date('H:i:s');
            $this->saldoResultante = $nuevoSaldo;
            $this->hash = bin2hex(random_bytes(16)); // Generar hash único
            
            if (!$this->crearEnTransaccion()) {
                throw new Exception("Error al registrar el depósito.");
            }
            
            // Confirmar transacción
            $this->conn->commit();
            return true;
            
        } catch (Exception $e) {
            // Revertir transacción en caso de error
            $this->conn->rollBack();
            error_log("Error en realizarDeposito: " . $e->getMessage());
            throw $e;
        }
    }
    
    /**
     * Realiza un retiro de la cuenta
     * @param int $idCuenta ID de la cuenta
     * @param float $monto Monto a retirar
     * @param string $descripcion Descripción de la transacción
     * @return boolean
     */
    public function realizarRetiro($idCuenta, $monto, $descripcion = 'Retiro') {
        // Iniciar transacción en la base de datos
        $this->conn->beginTransaction();
        
        try {
            // Obtener cuenta
            $cuenta = new Cuenta($this->conn);
            $cuenta->idCuenta = $idCuenta;
            
            if (!$cuenta->obtenerUna()) {
                throw new Exception("Cuenta no encontrada.");
            }
            
            // Verificar que la cuenta esté activa
            //if ($cuenta->estado != 1) {
          //      throw new Exception("La cuenta no está activa.");
          //  }
            
            // Verificar saldo suficiente
            if ($cuenta->saldo < $monto) {
                throw new Exception("Saldo insuficiente.");
            }
            
            // Actualizar saldo de la cuenta
            $nuevoSaldo = $cuenta->saldo - $monto;
            
            $query = "UPDATE cuenta SET saldo = :saldo WHERE idCuenta = :idCuenta";
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':saldo', $nuevoSaldo);
            $stmt->bindParam(':idCuenta', $idCuenta);
            $stmt->execute();
            
            // Crear transacción
            $this->idCuenta = $idCuenta;
            $this->tipoTransaccion = 1; // Retiro
            $this->monto = $monto;
            $this->descripcion = $descripcion;
            $this->fecha = date('Y-m-d');
            $this->hora = date('H:i:s');
            $this->saldoResultante = $nuevoSaldo;
            $this->hash = bin2hex(random_bytes(16)); // Generar hash único
            
            if (!$this->crearEnTransaccion()) {
                throw new Exception("Error al registrar el retiro.");
            }
            
            // Confirmar transacción
            $this->conn->commit();
            return true;
            
        } catch (Exception $e) {
            // Revertir transacción en caso de error
            $this->conn->rollBack();
            error_log("Error en realizarRetiro: " . $e->getMessage());
            throw $e;
        }
    }
    
    /**
     * Realiza una transferencia entre cuentas
     * @param int $idCuentaOrigen ID de la cuenta origen
     * @param int $idCuentaDestino ID de la cuenta destino
     * @param float $monto Monto a transferir
     * @param string $descripcion Descripción de la transacción
     * @return boolean
     */
    public function realizarTransferencia($idCuentaOrigen, $idCuentaDestino, $monto, $descripcion = 'Transferencia') {
        // Iniciar transacción en la base de datos
        $this->conn->beginTransaction();
        
        try {
            // Obtener cuenta origen
            $cuentaOrigen = new Cuenta($this->conn);
            $cuentaOrigen->idCuenta = $idCuentaOrigen;
            
            if (!$cuentaOrigen->obtenerUna()) {
                throw new Exception("Cuenta origen no encontrada.");
            }
            
            // Verificar que la cuenta origen esté activa
            if ($cuentaOrigen->estado != 1) {
                throw new Exception("La cuenta origen no está activa.");
            }
            
            // Verificar saldo suficiente
            if ($cuentaOrigen->saldo < $monto) {
                throw new Exception("Saldo insuficiente en la cuenta origen.");
            }
            
            // Obtener cuenta destino
            $cuentaDestino = new Cuenta($this->conn);
            $cuentaDestino->idCuenta = $idCuentaDestino;
            
            if (!$cuentaDestino->obtenerUna()) {
                throw new Exception("Cuenta destino no encontrada.");
            }
            
            // Verificar que la cuenta destino esté activa
          //  if ($cuentaDestino->estado != 1) {
            //    throw new Exception("La cuenta destino no está activa.");
            //}
            
            // Verificar que las monedas sean compatibles
            if ($cuentaOrigen->tipoMoneda != $cuentaDestino->tipoMoneda) {
                throw new Exception("Las monedas de las cuentas no son compatibles.");
            }
            
            // Actualizar saldo de la cuenta origen (restar)
            $nuevoSaldoOrigen = $cuentaOrigen->saldo - $monto;
            
            $queryOrigen = "UPDATE cuenta SET saldo = :saldo WHERE idCuenta = :idCuenta";
            $stmtOrigen = $this->conn->prepare($queryOrigen);
            $stmtOrigen->bindParam(':saldo', $nuevoSaldoOrigen);
            $stmtOrigen->bindParam(':idCuenta', $idCuentaOrigen);
            $stmtOrigen->execute();
            
            // Actualizar saldo de la cuenta destino (sumar)
            $nuevoSaldoDestino = $cuentaDestino->saldo + $monto;
            
            $queryDestino = "UPDATE cuenta SET saldo = :saldo WHERE idCuenta = :idCuenta";
            $stmtDestino = $this->conn->prepare($queryDestino);
            $stmtDestino->bindParam(':saldo', $nuevoSaldoDestino);
            $stmtDestino->bindParam(':idCuenta', $idCuentaDestino);
            $stmtDestino->execute();
            
            // Generar hash único para ambas transacciones
            $hashTransaccion = bin2hex(random_bytes(16));
            
            // Crear transacción para la cuenta origen (salida de dinero)
            $this->idCuenta = $idCuentaOrigen;
            $this->tipoTransaccion = 4; // Transferencia enviada
            $this->monto = $monto;
            $this->descripcion = $descripcion;
            $this->fecha = date('Y-m-d');
            $this->hora = date('H:i:s');
            $this->saldoResultante = $nuevoSaldoOrigen;
            $this->cuentaDestino = $cuentaDestino->nroCuenta;
            $this->hash = $hashTransaccion;
            
            if (!$this->crearEnTransaccion()) {
                throw new Exception("Error al registrar la transferencia (origen).");
            }
            
            // Crear transacción para la cuenta destino (entrada de dinero)
            $transaccionDestino = new Transaccion($this->conn);
            $transaccionDestino->idCuenta = $idCuentaDestino;
            $transaccionDestino->tipoTransaccion = 3; // Transferencia recibida
            $transaccionDestino->monto = $monto;
            $transaccionDestino->descripcion = $descripcion;
            $transaccionDestino->fecha = date('Y-m-d');
            $transaccionDestino->hora = date('H:i:s');
            $transaccionDestino->saldoResultante = $nuevoSaldoDestino;
            $transaccionDestino->cuentaOrigen = $cuentaOrigen->nroCuenta;
            $transaccionDestino->hash = $hashTransaccion;
            
            if (!$transaccionDestino->crearEnTransaccion()) {
                throw new Exception("Error al registrar la transferencia (destino).");
            }
            
            // Confirmar transacción
            $this->conn->commit();
            return true;
            
        } catch (Exception $e) {
            // Revertir transacción en caso de error
            $this->conn->rollBack();
            error_log("Error en realizarTransferencia: " . $e->getMessage());
            throw $e;
        }
    }
    
    /**
     * Contar transacciones del día
     * @return int
     */
    public function contarTransaccionesHoy() {
        $hoy = date('Y-m-d');
        $query = "SELECT COUNT(*) as total FROM transaccion WHERE fecha = :fecha";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':fecha', $hoy);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        
        return $row['total'];
    }
    
    /**
     * Obtener transacciones recientes
     * @param int $limit Número de transacciones a obtener
     * @return PDOStatement
     */
    public function obtenerRecientes($limit = 10) {
        $query = "SELECT t.*, c.nroCuenta, 
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM transaccion t
                  INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  ORDER BY t.fecha DESC, t.hora DESC
                  LIMIT :limit";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':limit', $limit, PDO::PARAM_INT);
        $stmt->execute();
        
        return $stmt;
    }
}