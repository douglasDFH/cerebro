<?php

class Token
{
    private $conn;

    public $idToken;
    public $token;
    public $fechaCreacion;
    public $fechaExpiracion;
    public $idTarjeta;
    /**
     * Constructor con DB
     * @param PDO $db
     */
    public function __construct($db)
    {
        $this->conn = $db;
    }
    public function crear()
    {
        if (empty($this->token) || empty($this->fechaCreacion) || empty($this->fechaExpiracion) || empty($this->idTarjeta)) {
            return false;
        }
        $query = 'INSERT INTO token SET token = :token, fechaCreacion = :fechaCreacion, fechaExpiracion = :fechaExpiracion, idTarjeta = :idTarjeta';
        $stmt = $this->conn->prepare($query);
        $this->token = htmlspecialchars(strip_tags($this->token));
        $this->fechaCreacion = htmlspecialchars(strip_tags($this->fechaCreacion));
        $this->fechaExpiracion = htmlspecialchars(strip_tags($this->fechaExpiracion));
        $this->idTarjeta = htmlspecialchars(strip_tags($this->idTarjeta));
        $stmt->bindParam(':token', $this->token);
        $stmt->bindParam(':fechaCreacion', $this->fechaCreacion);
        $stmt->bindParam(':fechaExpiracion', $this->fechaExpiracion);
        $stmt->bindParam(':idTarjeta', $this->idTarjeta);

        if ($stmt->execute()) {
            $this->idToken = $this->conn->lastInsertId();
            return true;
        }
        return false;
    }
    public static function validate($token, $conn)
    {
        $query = 'SELECT * FROM token WHERE token = :token';
        $stmt = $conn->prepare($query);
        $stmt->bindParam(':token', $token);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        if ($row && strtotime($row['fechaExpiracion']) > time()) {
            $newToken = new Token($conn);
            $newToken->idToken = $row['idToken'];
            $newToken->token = $row['token'];
            $newToken->fechaCreacion = $row['fechaCreacion'];
            $newToken->fechaExpiracion = $row['fechaExpiracion'];
            $newToken->idTarjeta = $row['idTarjeta'];
            return $newToken;
        }
        return false;
    }
    public function getCuentas()
    {
        // Obtener el idTarjeta usando el token
        $select = 'SELECT idTarjeta FROM token WHERE token = :token';
        $stmt = $this->conn->prepare($select);
        $token = ($this->token);
        $stmt->bindParam(':token', $token);
        $stmt->execute();
        $result = $stmt->fetch(PDO::FETCH_ASSOC);
        $idTarjeta = $result['idTarjeta'];

        // Usar el idTarjeta para obtener el idCuenta
        $select = 'SELECT idCuenta FROM tarjeta WHERE idTarjeta = :idTarjeta';
        $stmt = $this->conn->prepare($select);
        $stmt->bindParam(':idTarjeta', $idTarjeta);
        $stmt->execute();
        $result = $stmt->fetch(PDO::FETCH_ASSOC);
        $idCuenta = $result['idCuenta'];

        // Usar el idCuenta para obtener el idPersona
        $select = 'SELECT idPersona FROM cuenta WHERE idCuenta = :idCuenta';
        $stmt = $this->conn->prepare($select);
        $stmt->bindParam(':idCuenta', $idCuenta);
        $stmt->execute();
        $result = $stmt->fetch(PDO::FETCH_ASSOC);
        $idPersona = $result['idPersona'];

        // Finalmente, obtener todas las cuentas de la persona
        $select = 'SELECT * FROM cuenta WHERE idPersona = :idPersona';
        $stmt = $this->conn->prepare($select);
        $stmt->bindParam(':idPersona', $idPersona);
        $stmt->execute();
        $cuentas = [];
        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $cuenta = new Cuenta($this->conn);
            $cuenta->idCuenta = $row['idCuenta'];
            $cuenta->saldo = $row['saldo'];
            $cuenta->tipoCuenta = $row['tipoCuenta'];
            $cuenta->tipoMoneda = $row['tipoMoneda'];
            $cuenta->fechaApertura = $row['fechaApertura'];
            $cuenta->estado = $row['estado'];
            $cuenta->nroCuenta = $row['nroCuenta'];
            $cuenta->idPersona = $row['idPersona'];
            $cuenta->hash = $row['hash'];
            $cuentas[] = $cuenta;
        }
        return $cuentas;
    }
    public function getTarjeta()
    {
        $select = 'SELECT * FROM tarjeta WHERE idTarjeta = :idTarjeta';
        $stmt = $this->conn->prepare($select);
        $stmt->bindParam(':idTarjeta', $this->idTarjeta);
        $stmt->execute();
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        die(var_dump($row));
        $tarjeta = new Tarjeta($this->conn);
        $tarjeta->idTarjeta = $row['idTarjeta'];
        $tarjeta->hash = $row['hash'];
        $tarjeta->estado = $row['estado'];
        $tarjeta->tipoTarjeta = $row['tipoTarjeta'];
        $tarjeta->nroTarjeta = $row['nroTarjeta'];
        $tarjeta->cvv = $row['cvv'];
        $tarjeta->fechaExpiracion = $row['fechaExpiracion'];
        $tarjeta->pin = $row['pin'];
        $tarjeta->idCuenta = $row['idCuenta'];
        return $tarjeta;
    }
}
