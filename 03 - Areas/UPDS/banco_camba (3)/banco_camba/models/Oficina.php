<?php

class Oficina {
    // Propiedades
    public $idOficina;
    public $hash;
    public $central;
    public $nombre;
    public $direccion;
    public $telefono;
    public $ciudad;
    public $departamento;
    public $pais;
    public $horarioAtencion;
    public $gerenteEncargado;
    public $fechaInauguracion;
    public $estado;
    
    // Conexión a la base de datos
    private $conn;
    
    // Constructor
    public function __construct($db) {
        $this->conn = $db;
    }
    
    // Método para obtener todas las oficinas
    public function obtenerTodas() {
        try {
            $query = "SELECT idOficina, hash, central, nombre, direccion, telefono, ciudad, departamento, pais, horarioAtencion, gerenteEncargado, fechaInauguracion, estado FROM oficina ORDER BY nombre ASC";
            $stmt = $this->conn->prepare($query);
            $stmt->execute();
            
            // Verificar si hay datos
            $count = $stmt->rowCount();
            error_log("Oficina::obtenerTodas() - Filas encontradas: " . $count);
            
            return $stmt;
        } catch (PDOException $e) {
            // Registrar el error
            error_log("Error en Oficina::obtenerTodas(): " . $e->getMessage());
            return false;
        }
    }
    
    // Método para obtener una oficina específica
    public function obtenerUna() {
        try {
            $query = "SELECT * FROM oficina WHERE idOficina = ?";
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(1, $this->idOficina);
            $stmt->execute();
            
            if ($stmt->rowCount() > 0) {
                $row = $stmt->fetch(PDO::FETCH_ASSOC);
                
                // Asignar valores a las propiedades del objeto
                $this->hash = $row['hash'];
                $this->central = $row['central'];
                $this->nombre = $row['nombre'];
                $this->direccion = $row['direccion'];
                $this->telefono = $row['telefono'];
                $this->ciudad = $row['ciudad'];
                $this->departamento = $row['departamento'];
                $this->pais = $row['pais'];
                $this->horarioAtencion = $row['horarioAtencion'];
                $this->gerenteEncargado = $row['gerenteEncargado'];
                $this->fechaInauguracion = $row['fechaInauguracion'];
                $this->estado = $row['estado'];
                
                return true;
            }
            
            return false;
        } catch (PDOException $e) {
            error_log("Error en Oficina::obtenerUna(): " . $e->getMessage());
            return false;
        }
    }
    
    // Método para crear una nueva oficina
    public function crear() {
        try {
            $query = "INSERT INTO oficina (hash, central, nombre, direccion, telefono, ciudad, 
                        departamento, pais, horarioAtencion, gerenteEncargado, fechaInauguracion, estado) 
                      VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            
            $stmt = $this->conn->prepare($query);
            
            // Sanear datos
            $this->hash = htmlspecialchars(strip_tags($this->hash));
            $this->central = htmlspecialchars(strip_tags($this->central));
            $this->nombre = htmlspecialchars(strip_tags($this->nombre));
            $this->direccion = htmlspecialchars(strip_tags($this->direccion));
            $this->telefono = htmlspecialchars(strip_tags($this->telefono));
            $this->ciudad = htmlspecialchars(strip_tags($this->ciudad));
            $this->departamento = htmlspecialchars(strip_tags($this->departamento));
            $this->pais = htmlspecialchars(strip_tags($this->pais));
            $this->horarioAtencion = htmlspecialchars(strip_tags($this->horarioAtencion));
            $this->gerenteEncargado = htmlspecialchars(strip_tags($this->gerenteEncargado));
            $this->fechaInauguracion = htmlspecialchars(strip_tags($this->fechaInauguracion));
            $this->estado = htmlspecialchars(strip_tags($this->estado));
            
            // Vincular parámetros
            $stmt->bindParam(1, $this->hash);
            $stmt->bindParam(2, $this->central);
            $stmt->bindParam(3, $this->nombre);
            $stmt->bindParam(4, $this->direccion);
            $stmt->bindParam(5, $this->telefono);
            $stmt->bindParam(6, $this->ciudad);
            $stmt->bindParam(7, $this->departamento);
            $stmt->bindParam(8, $this->pais);
            $stmt->bindParam(9, $this->horarioAtencion);
            $stmt->bindParam(10, $this->gerenteEncargado);
            $stmt->bindParam(11, $this->fechaInauguracion);
            $stmt->bindParam(12, $this->estado);
            
           // Ejecutar consulta
           if ($stmt->execute()) {
            // Obtener el ID generado
            $this->idOficina = $this->conn->lastInsertId();
            return true;
        }
        
        return false;
    } catch (PDOException $e) {
        error_log("Error en Oficina::crear(): " . $e->getMessage());
        return false;
    }
}

// Método para actualizar una oficina
public function actualizar() {
    try {
        $query = "UPDATE oficina 
                  SET central = ?, 
                      nombre = ?, 
                      direccion = ?, 
                      telefono = ?, 
                      ciudad = ?, 
                      departamento = ?, 
                      pais = ?, 
                      horarioAtencion = ?, 
                      gerenteEncargado = ?, 
                      fechaInauguracion = ?, 
                      estado = ? 
                  WHERE idOficina = ?";
        
        $stmt = $this->conn->prepare($query);
        
        // Sanear datos
        $this->central = htmlspecialchars(strip_tags($this->central));
        $this->nombre = htmlspecialchars(strip_tags($this->nombre));
        $this->direccion = htmlspecialchars(strip_tags($this->direccion));
        $this->telefono = htmlspecialchars(strip_tags($this->telefono));
        $this->ciudad = htmlspecialchars(strip_tags($this->ciudad));
        $this->departamento = htmlspecialchars(strip_tags($this->departamento));
        $this->pais = htmlspecialchars(strip_tags($this->pais));
        $this->horarioAtencion = htmlspecialchars(strip_tags($this->horarioAtencion));
        $this->gerenteEncargado = htmlspecialchars(strip_tags($this->gerenteEncargado));
        $this->fechaInauguracion = htmlspecialchars(strip_tags($this->fechaInauguracion));
        $this->estado = htmlspecialchars(strip_tags($this->estado));
        
        // Vincular parámetros
        $stmt->bindParam(1, $this->central);
        $stmt->bindParam(2, $this->nombre);
        $stmt->bindParam(3, $this->direccion);
        $stmt->bindParam(4, $this->telefono);
        $stmt->bindParam(5, $this->ciudad);
        $stmt->bindParam(6, $this->departamento);
        $stmt->bindParam(7, $this->pais);
        $stmt->bindParam(8, $this->horarioAtencion);
        $stmt->bindParam(9, $this->gerenteEncargado);
        $stmt->bindParam(10, $this->fechaInauguracion);
        $stmt->bindParam(11, $this->estado);
        $stmt->bindParam(12, $this->idOficina);
        
        // Ejecutar consulta
        if ($stmt->execute()) {
            return true;
        }
        
        return false;
    } catch (PDOException $e) {
        error_log("Error en Oficina::actualizar(): " . $e->getMessage());
        return false;
    }
}

// Método para eliminar una oficina (lógicamente cambiando su estado)
public function desactivar() {
    try {
        $query = "UPDATE oficina SET estado = 'inactiva' WHERE idOficina = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(1, $this->idOficina);
        
        if ($stmt->execute()) {
            return true;
        }
        
        return false;
    } catch (PDOException $e) {
        error_log("Error en Oficina::desactivar(): " . $e->getMessage());
        return false;
    }
}

// Método para activar una oficina
public function activar() {
    try {
        $query = "UPDATE oficina SET estado = 'activa' WHERE idOficina = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(1, $this->idOficina);
        
        if ($stmt->execute()) {
            return true;
        }
        
        return false;
    } catch (PDOException $e) {
        error_log("Error en Oficina::activar(): " . $e->getMessage());
        return false;
    }
}

// Método para eliminar definitivamente una oficina
public function eliminar() {
    try {
        // Primero verificamos si hay dependencias (personas o ATMs)
        if ($this->contarPersonas() > 0 || $this->contarATMs() > 0) {
            return false; // No se puede eliminar si hay dependencias
        }
        
        $query = "DELETE FROM oficina WHERE idOficina = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(1, $this->idOficina);
        
        if ($stmt->execute()) {
            return true;
        }
        
        return false;
    } catch (PDOException $e) {
        error_log("Error en Oficina::eliminar(): " . $e->getMessage());
        return false;
    }
}

// Método para contar cuántos ATMs tiene una oficina
public function contarATMs() {
    try {
        $query = "SELECT COUNT(*) as total FROM atm WHERE idOficina = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(1, $this->idOficina);
        $stmt->execute();
        
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        return $row['total'];
    } catch (PDOException $e) {
        error_log("Error en Oficina::contarATMs(): " . $e->getMessage());
        return 0;
    }
}

// Método para contar cuántas personas están asignadas a una oficina
public function contarPersonas() {
    try {
        $query = "SELECT COUNT(*) as total FROM persona WHERE idOficina = ?";
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(1, $this->idOficina);
        $stmt->execute();
        
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        return $row['total'];
    } catch (PDOException $e) {
        error_log("Error en Oficina::contarPersonas(): " . $e->getMessage());
        return 0;
    }
}
}
?>