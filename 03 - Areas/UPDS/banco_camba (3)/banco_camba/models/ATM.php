<?php
/**
 * Modelo ATM (Cajero Automático)
 */
class ATM {
    // Conexión a la base de datos
    private $conn;
    
    // Propiedades del ATM
    public $idATM;
    public $nombreATM;
    public $ubicacion;
    public $saldoActual;
    public $estado; // 1: Activo, 0: Inactivo
    
    /**
     * Constructor con DB
     * @param PDO $db
     */
    public function __construct($db) {
        $this->conn = $db;
    }
    
    /**
     * Obtener todos los ATMs
     * @return PDOStatement
     */
    public function obtenerTodos() {
        $query = "SELECT * FROM atm ORDER BY nombreATM ASC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener ATMs activos
     * @return PDOStatement
     */
    public function obtenerActivos() {
        $query = "SELECT * FROM atm WHERE estado = 1 ORDER BY nombreATM ASC";
        
        $stmt = $this->conn->prepare($query);
        $stmt->execute();
        
        return $stmt;
    }
    
    /**
     * Obtener un ATM específico
     * @return boolean
     */
    public function obtenerUno() {
        $query = "SELECT * FROM atm WHERE idATM = :id";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':id', $this->idATM);
        $stmt->execute();
        
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        
        if ($row) {
            $this->nombreATM = $row['nombreATM'];
            $this->ubicacion = $row['ubicacion'];
            $this->saldoActual = $row['saldoActual'];
            $this->estado = $row['estado'];
            return true;
        }
        
        return false;
    }
    
    /**
     * Crear un nuevo ATM
     * @return boolean
     */
    public function crear() {
        $query = "INSERT INTO atm (nombreATM, ubicacion, saldoActual, estado) 
                 VALUES (:nombreATM, :ubicacion, :saldoActual, :estado)";
        
        $stmt = $this->conn->prepare($query);
        
        // Limpiar datos
        $this->nombreATM = htmlspecialchars(strip_tags($this->nombreATM));
        $this->ubicacion = htmlspecialchars(strip_tags($this->ubicacion));
        
        // Vincular datos
        $stmt->bindParam(':nombreATM', $this->nombreATM);
        $stmt->bindParam(':ubicacion', $this->ubicacion);
        $stmt->bindParam(':saldoActual', $this->saldoActual);
        $stmt->bindParam(':estado', $this->estado);
        
        if ($stmt->execute()) {
            $this->idATM = $this->conn->lastInsertId();
            return true;
        }
        
        return false;
    }
    
    /**
     * Actualizar un ATM existente
     * @return boolean
     */
    public function actualizar() {
        $query = "UPDATE atm
                SET nombreATM = :nombreATM, 
                    ubicacion = :ubicacion, 
                    saldoActual = :saldoActual, 
                    estado = :estado
                WHERE idATM = :idATM";
        
        $stmt = $this->conn->prepare($query);
        
        // Limpiar datos
        $this->nombreATM = htmlspecialchars(strip_tags($this->nombreATM));
        $this->ubicacion = htmlspecialchars(strip_tags($this->ubicacion));
        
        // Vincular datos
        $stmt->bindParam(':nombreATM', $this->nombreATM);
        $stmt->bindParam(':ubicacion', $this->ubicacion);
        $stmt->bindParam(':saldoActual', $this->saldoActual);
        $stmt->bindParam(':estado', $this->estado);
        $stmt->bindParam(':idATM', $this->idATM);
        
        return $stmt->execute();
    }
    
    /**
     * Actualizar saldo del ATM
     * @param float $monto Monto a actualizar
     * @param boolean $esIncremento Si es true, incrementa; si es false, decrementa
     * @return boolean
     */
    public function actualizarSaldo($monto, $esIncremento = true) {
        // Obtener saldo actual
        $this->obtenerUno();
        
        // Calcular nuevo saldo
        if ($esIncremento) {
            $nuevoSaldo = $this->saldoActual + $monto;
        } else {
            // Verificar si hay saldo suficiente
            if ($this->saldoActual < $monto) {
                return false;
            }
            $nuevoSaldo = $this->saldoActual - $monto;
        }
        
        // Actualizar saldo en la base de datos
        $query = "UPDATE atm SET saldoActual = :saldoActual WHERE idATM = :idATM";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':saldoActual', $nuevoSaldo);
        $stmt->bindParam(':idATM', $this->idATM);
        
        if ($stmt->execute()) {
            $this->saldoActual = $nuevoSaldo;
            return true;
        }
        
        return false;
    }
    
    /**
     * Eliminar un ATM
     * @return boolean
     */
    public function eliminar() {
        $query = "DELETE FROM atm WHERE idATM = :idATM";
        
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':idATM', $this->idATM);
        
        return $stmt->execute();
    }
}
?>