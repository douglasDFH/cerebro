<?php

/**
 * Modelo Tarjeta
 */
class Tarjeta
{
    // Conexión a la base de datos
    private $conn;

    // Propiedades de la tarjeta
    public $idTarjeta;
    public $hash;
    public $estado;
    public $tipoTarjeta;
    public $nroTarjeta;
    public $cvv;
    public $fechaExpiracion;
    public $pin;
    public $idCuenta;

    /**
     * Constructor con DB
     * @param PDO $db
     */
    public function __construct($db)
    {
        $this->conn = $db;
    }

    /**
     * Obtener todas las tarjetas
     * @return PDOStatement
     */
    public function obtenerTodas()
    {
        $query = "SELECT t.*, c.nroCuenta, p.nombre, p.apellidoPaterno, p.apellidoMaterno 
                 FROM tarjeta t
                 INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                 INNER JOIN persona p ON c.idPersona = p.idPersona
                 ORDER BY t.idTarjeta DESC";

        $stmt = $this->conn->prepare($query);
        $stmt->execute();

        return $stmt;
    }

    /**
     * Obtener una tarjeta
     * @return boolean
     */
    public function obtenerUna()
    {
        $query = "SELECT t.*, c.nroCuenta, p.nombre, p.apellidoPaterno, p.apellidoMaterno, p.idPersona, c.tipoMoneda
                 FROM tarjeta t
                 INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                 INNER JOIN persona p ON c.idPersona = p.idPersona
                 WHERE t.idTarjeta = :id";

        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':id', $this->idTarjeta);
        $stmt->execute();

        $row = $stmt->fetch(PDO::FETCH_ASSOC);

        if ($row) {
            $this->hash = $row['hash'];
            $this->estado = $row['estado'];
            $this->tipoTarjeta = $row['tipoTarjeta'];
            $this->nroTarjeta = $row['nroTarjeta'];
            $this->cvv = $row['cvv'];
            $this->fechaExpiracion = $row['fechaExpiracion'];
            $this->pin = $row['pin'];
            $this->idCuenta = $row['idCuenta'];
            return true;
        }

        return false;
    }

    /**
     * Obtener tarjetas por cuenta
     * @param int $idCuenta
     * @return PDOStatement
     */
    public function obtenerPorCuenta($idCuenta)
    {
        $query = "SELECT * FROM tarjeta 
                 WHERE idCuenta = :idCuenta
                 ORDER BY idTarjeta DESC";

        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->execute();

        return $stmt;
    }

    /**
     * Crear una tarjeta
     * @return boolean
     */
    public function crear()
    {
        // Generar hash único si no existe
        if (empty($this->hash)) {
            $this->hash = bin2hex(random_bytes(16));
        }

        $query = "INSERT INTO tarjeta
                 (hash, estado, tipoTarjeta, nroTarjeta, cvv, fechaExpiracion, pin, idCuenta)
                 VALUES
                 (:hash, :estado, :tipoTarjeta, :nroTarjeta, :cvv, :fechaExpiracion, :pin, :idCuenta)";

        $stmt = $this->conn->prepare($query);

        // Limpiar datos
        $this->nroTarjeta = htmlspecialchars(strip_tags($this->nroTarjeta));
        $this->cvv = htmlspecialchars(strip_tags($this->cvv));
        $this->fechaExpiracion = htmlspecialchars(strip_tags($this->fechaExpiracion));
        $this->pin = htmlspecialchars(strip_tags($this->pin));

        // Vincular datos
        $stmt->bindParam(':hash', $this->hash);
        $stmt->bindParam(':estado', $this->estado);
        $stmt->bindParam(':tipoTarjeta', $this->tipoTarjeta);
        $stmt->bindParam(':nroTarjeta', $this->nroTarjeta);
        $stmt->bindParam(':cvv', $this->cvv);
        $stmt->bindParam(':fechaExpiracion', $this->fechaExpiracion);
        $stmt->bindParam(':pin', $this->pin);
        $stmt->bindParam(':idCuenta', $this->idCuenta);

        // Ejecutar consulta
        if ($stmt->execute()) {
            $this->idTarjeta = $this->conn->lastInsertId();
            return true;
        }

        return false;
    }

    /**
     * Actualizar tarjeta
     * @return boolean
     */
    public function actualizar()
    {
        $query = "UPDATE tarjeta SET
                estado = :estado,
                tipoTarjeta = :tipoTarjeta,
                nroTarjeta = :nroTarjeta,
                cvv = :cvv,
                fechaExpiracion = :fechaExpiracion,
                pin = :pin,
                idCuenta = :idCuenta
                WHERE idTarjeta = :idTarjeta";

        $stmt = $this->conn->prepare($query);

        // Limpiar datos
        $this->nroTarjeta = htmlspecialchars(strip_tags($this->nroTarjeta));
        $this->cvv = htmlspecialchars(strip_tags($this->cvv));
        $this->fechaExpiracion = htmlspecialchars(strip_tags($this->fechaExpiracion));
        $this->pin = htmlspecialchars(strip_tags($this->pin));

        // Vincular datos
        $stmt->bindParam(':idTarjeta', $this->idTarjeta);
        $stmt->bindParam(':estado', $this->estado);
        $stmt->bindParam(':tipoTarjeta', $this->tipoTarjeta);
        $stmt->bindParam(':nroTarjeta', $this->nroTarjeta);
        $stmt->bindParam(':cvv', $this->cvv);
        $stmt->bindParam(':fechaExpiracion', $this->fechaExpiracion);
        $stmt->bindParam(':pin', $this->pin);
        $stmt->bindParam(':idCuenta', $this->idCuenta);

        // Ejecutar consulta
        if ($stmt->execute()) {
            return true;
        }

        return false;
    }

    /**
     * Cambiar estado de tarjeta (activar/desactivar)
     * @return boolean
     */
    public function cambiarEstado($nuevoEstado)
    {
        $query = "UPDATE tarjeta SET
                estado = :estado
                WHERE idTarjeta = :idTarjeta";

        $stmt = $this->conn->prepare($query);

        // Vincular datos
        $stmt->bindParam(':idTarjeta', $this->idTarjeta);
        $stmt->bindParam(':estado', $nuevoEstado);

        // Ejecutar consulta
        if ($stmt->execute()) {
            return true;
        }

        return false;
    }

    /**
     * Eliminar tarjeta
     * @return boolean
     */
    public function eliminar()
    {
        $query = "DELETE FROM tarjeta WHERE idTarjeta = :idTarjeta";
        $stmt = $this->conn->prepare($query);
        $stmt->bindParam(':idTarjeta', $this->idTarjeta);

        if ($stmt->execute()) {
            return true;
        }

        return false;
    }

    /**
     * Verificar si existe un número de tarjeta
     * @param string $nroTarjeta
     * @return boolean
     */
    public function existeNumeroTarjeta($nroTarjeta, $idTarjeta = null)
    {
        if ($idTarjeta) {
            $query = "SELECT COUNT(*) as total FROM tarjeta WHERE nroTarjeta = :nroTarjeta AND idTarjeta != :idTarjeta";
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':nroTarjeta', $nroTarjeta);
            $stmt->bindParam(':idTarjeta', $idTarjeta);
        } else {
            $query = "SELECT COUNT(*) as total FROM tarjeta WHERE nroTarjeta = :nroTarjeta";
            $stmt = $this->conn->prepare($query);
            $stmt->bindParam(':nroTarjeta', $nroTarjeta);
        }

        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);

        return ($row['total'] > 0);
    }

    /**
     * Generar número de tarjeta aleatorio
     * @return string
     */
    public function generarNumeroTarjeta()
    {
        // Formato ejemplo: 5412 7534 8932 1452
        $prefijo = "5412"; // Prefijo para tarjetas de ejemplo
        $numero = $prefijo;

        // Generar 12 dígitos restantes
        for ($i = 0; $i < 12; $i++) {
            $numero .= mt_rand(0, 9);
        }

        // Formatear con espacios cada 4 dígitos
        $numeroFormateado = substr($numero, 0, 4) . ' ' . substr($numero, 4, 4) . ' ' .
            substr($numero, 8, 4) . ' ' . substr($numero, 12, 4);

        return $numeroFormateado;
    }

    /**
     * Generar CVV aleatorio
     * @return string
     */
    public function generarCVV()
    {
        // Generar 3 dígitos aleatorios
        $cvv = "";
        for ($i = 0; $i < 3; $i++) {
            $cvv .= mt_rand(0, 9);
        }

        return $cvv;
    }

    /**
     * Generar fecha de expiración
     * @return string
     */
    public function generarFechaExpiracion()
    {
        // Formato MM/YY, válida por 3 años desde la fecha actual
        $month = date('m');
        $year = date('y', strtotime('+3 years'));

        return $month . '/' . $year;
    }
    /**
     * login
     * @return Tarjeta|boolean
     */
    public function login($numberCard, $pin)
    {
        $sql = "SELECT * FROM tarjeta WHERE nroTarjeta = :nroTarjeta AND pin = :pin";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':nroTarjeta', $numberCard);
        $stmt->bindParam(':pin', $pin);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        if ($row) {
            $this->idTarjeta = $row['idTarjeta'];
            $this->hash = $row['hash'];
            $this->estado = $row['estado'];
            $this->tipoTarjeta = $row['tipoTarjeta'];
            $this->nroTarjeta = $row['nroTarjeta'];
            $this->cvv = $row['cvv'];
            $this->fechaExpiracion = $row['fechaExpiracion'];
            $this->pin = $row['pin'];
            $this->idCuenta = $row['idCuenta'];
            return $this;
        }
        return false;
    }
    /**
     * NewToken
     * @return string
     */
    public function NewToken()
    {

        $tokenStr = bin2hex(random_bytes(32));
        $token = new Token($this->conn);
        $token->token = $tokenStr;
        $token->idTarjeta = $this->idTarjeta;
        $token->fechaCreacion = date('Y-m-d H:i:s');
        $token->fechaExpiracion = date('Y-m-d H:i:s', strtotime('+1 hour'));

        if (!$token->crear()) {
            throw new Exception('Error al crear token');
        }
        return $token;
    }
    public function getUsuario()
    {
        $sql = "SELECT p.idPersona, p.nombre, p.apellidoPaterno, p.apellidoMaterno FROM tarjeta t
                INNER JOIN cuenta c ON t.idCuenta = c.idCuenta
                INNER JOIN persona p ON c.idPersona = p.idPersona
                WHERE t.idTarjeta = :idTarjeta";
        $stmt = $this->conn->prepare($sql);
        $stmt->bindParam(':idTarjeta', $this->idTarjeta);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        return [
            'idPersona' => $row['idPersona'],
            'usarname' => $row['nombre'] . ' ' . $row['apellidoPaterno'] . ' ' . $row['apellidoMaterno'],

        ];
    }
}
