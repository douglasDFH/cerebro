<?php
/**
 * Modelo Cuenta
 */
class Cuenta {
    // Conexión a la base de datos
    private $conn;
    
    // Propiedades de la cuenta
    public $idCuenta;
    public $tipoCuenta; // 1: Cuenta de ahorro, 2: Cuenta corriente
    public $tipoMoneda; // 1: Bolivianos, 2: Dólares
    public $fechaApertura;
    public $estado; // 1: Activa, 2: Inactiva
    public $nroCuenta;
    public $saldo;
    public $idPersona;
    // Agregar propiedad hash
    public $hash;
    
    /**
     * Constructor con DB
     * @param PDO $db
     */
    public function __construct($db) {
        $this->conn = $db;
    }
    
    /**
     * Obtener todas las cuentas
     * @return PDOStatement
     */
    public function obtenerTodas() {
        $query = "SELECT c.*, 
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM cuenta c
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  ORDER BY c.fechaApertura DESC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener una cuenta
     * @return boolean
     */
    public function obtenerUna() {
        $query = "SELECT c.*, 
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM cuenta c
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  WHERE c.idCuenta = :id";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':id', $this->idCuenta);
        $stmt->execute();
        
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        
        if ($row) {
            $this->tipoCuenta = $row['tipoCuenta'];
            $this->tipoMoneda = $row['tipoMoneda'];
            $this->fechaApertura = $row['fechaApertura'];
            $this->estado = $row['estado'];
            $this->nroCuenta = $row['nroCuenta'];
            $this->saldo = $row['saldo'];
            $this->idPersona = $row['idPersona'];
            $this->hash = $row['hash']; // Agregar hash
            return true;
        }
        
        return false;
    }
    
    /**
     * Obtener cuentas por cliente
     * @param int $idPersona
     * @return PDOStatement
     */
    public function obtenerPorCliente($idPersona) {
        $query = "SELECT * FROM cuenta WHERE idPersona = :idPersona ORDER BY fechaApertura DESC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':idPersona', $idPersona);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Crear una cuenta
     * @return boolean
     */
    public function crear() {
        // Generar número de cuenta único
        $this->nroCuenta = $this->generarNumeroCuenta();
        
        // Generar hash si no existe
        if (empty($this->hash)) {
            $this->hash = bin2hex(random_bytes(16));
        }
        
        $query = "INSERT INTO cuenta
                 (tipoCuenta, tipoMoneda, fechaApertura, estado, nroCuenta, saldo, idPersona, hash)
                 VALUES
                 (:tipoCuenta, :tipoMoneda, :fechaApertura, :estado, :nroCuenta, :saldo, :idPersona, :hash)";
        
        $stmt = $this->conn->prepare($query);
        
        // Limpiar datos
        $this->nroCuenta = htmlspecialchars(strip_tags($this->nroCuenta));
        
        // Si no se proporciona fecha de apertura, usar la fecha actual
        if (empty($this->fechaApertura)) {
            $this->fechaApertura = date('Y-m-d');
        }
        
        // Estado por defecto: Activa (1)
        if (empty($this->estado)) {
            $this->estado = 1;
        }
        
        // Saldo inicial
        if (empty($this->saldo)) {
            $this->saldo = 0;
        }
        
        // Vincular datos
        $stmt->bindParam(':tipoCuenta', $this->tipoCuenta);
        $stmt->bindParam(':tipoMoneda', $this->tipoMoneda);
        $stmt->bindParam(':fechaApertura', $this->fechaApertura);
        $stmt->bindParam(':estado', $this->estado);
        $stmt->bindParam(':nroCuenta', $this->nroCuenta);
        $stmt->bindParam(':saldo', $this->saldo);
        $stmt->bindParam(':idPersona', $this->idPersona);
        $stmt->bindParam(':hash', $this->hash);
        
        // Ejecutar consulta
        if ($stmt->execute()) {
            $this->idCuenta = $this->conn->lastInsertId();
            return true;
        }
        
        return false;
    }
    
    /**
     * Actualizar cuenta
     * @return boolean
     */
    public function actualizar() {
        $query = "UPDATE cuenta SET
                tipoCuenta = :tipoCuenta,
                estado = :estado
                WHERE idCuenta = :idCuenta";
        
        $stmt = $this->conn->prepare($query);
        
        // Vincular datos
        $stmt->bindParam(':idCuenta', $this->idCuenta);
        $stmt->bindParam(':tipoCuenta', $this->tipoCuenta);
        $stmt->bindParam(':estado', $this->estado);
        
        // Ejecutar consulta
        if ($stmt->execute()) {
            return true;
        }
        
        return false;
    }
    
    /**
     * Actualizar saldo de la cuenta
     * @param float $monto El monto a añadir o restar
     * @param boolean $esDeposito True si es depósito, false si es retiro
     * @return boolean
     */
    public function actualizarSaldo($monto, $esDeposito = true) {
        // Si es un retiro, verificar que haya saldo suficiente
        if (!$esDeposito && $this->saldo < $monto) {
            return false;
        }
        
        // Calcular nuevo saldo
        $nuevoSaldo = $esDeposito ? $this->saldo + $monto : $this->saldo - $monto;
        
        $query = "UPDATE cuenta SET saldo = :saldo WHERE idCuenta = :idCuenta";
        
        $stmt = $this->conn->prepare($query);
        
        // Vincular datos
        $stmt->bindParam(':idCuenta', $this->idCuenta);
        $stmt->bindParam(':saldo', $nuevoSaldo);
        
        // Ejecutar consulta
        if ($stmt->execute()) {
            $this->saldo = $nuevoSaldo;
            return true;
        }
        
        return false;
    }
    
    /**
     * Generar número de cuenta único
     * @return string
     */
    private function generarNumeroCuenta() {
        // Formato: AAAA-MM-DD-XXXX (donde XXXX es un número aleatorio)
        $fecha = date('Ymd');
        $aleatorio = mt_rand(1000, 9999);
        return $fecha . '-' . $aleatorio;
    }
    
    /**
     * Contar cuentas activas
     * @return int
     */
    public function contarCuentasActivas() {
        $query = "SELECT COUNT(*) as total FROM cuenta WHERE estado = 1";
        
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        
        return $row['total'];
    }
    
    /**
     * Obtener cuenta por número de cuenta
     * @param string $nroCuenta
     * @return boolean
     */
    public function obtenerPorNumeroCuenta($nroCuenta) {
        $query = "SELECT c.*, 
                  CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as cliente_nombre
                  FROM cuenta c
                  INNER JOIN persona p ON c.idPersona = p.idPersona
                  WHERE c.nroCuenta = :nroCuenta";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':nroCuenta', $nroCuenta);
        $stmt->execute();
        
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        
        if ($row) {
            $this->idCuenta = $row['idCuenta'];
            $this->tipoCuenta = $row['tipoCuenta'];
            $this->tipoMoneda = $row['tipoMoneda'];
            $this->fechaApertura = $row['fechaApertura'];
            $this->estado = $row['estado'];
            $this->nroCuenta = $row['nroCuenta'];
            $this->saldo = $row['saldo'];
            $this->idPersona = $row['idPersona'];
            $this->hash = $row['hash'];
            return true;
        }
        
        return false;
    }
    
    /**
     * Verificar si una cuenta está activa
     * @return boolean
     */
    public function estaActiva() {
        return $this->estado == 1;
    }
}
?>