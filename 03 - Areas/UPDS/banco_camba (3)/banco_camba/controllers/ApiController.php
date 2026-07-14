<?php

class ApiController
{
    public $data = [];
    public $conn;
    public function __construct()
    {
        header('Access-Control-Allow-Origin: *');
        header('Access-Control-Allow-Methods: GET, POST, PUT, DELETE');
        header('Access-Control-Allow-Headers: Content-Type');
        header('Content-Type: application/json');
        $this->data = json_decode(file_get_contents('php://input'), true);
        $database = new Database();
        $db = $database->connect();
        $this->conn = $db;
    }
    public function login()
    {
        $numberCard = $this->data['numberCard'];
        $pin = $this->data['pin'];
        if (!$numberCard || !$pin) {
            return $this->error('Number card and pin are required');
        }


        $tarjeta = (new Tarjeta($this->conn))->login($numberCard, $pin);
        if ($tarjeta === false) {
            return $this->error('Number card or pin are incorrect');
        }

        $token = $tarjeta->NewToken();
        $usario = $tarjeta->getUsuario();
        $this->success([
            'numberCard' => $numberCard,
            'token' => $token,
            'user' => $usario
        ]);
    }
    public function retirarMoney()
    {
        $token = $this->validateToken();
        if (!$token) {
            return $this->error('Token is invalid', 401);
        }
        $account_id = $this->data['account_id'];
        $amount = $this->data['amount'];
        if (!$account_id || !$amount) {
            return $this->error('Account id and amount are required');
        }
        if ($amount < 1) {
            return $this->error('Amount must be greater than 0');
        }
        $cuentas = $token->getCuentas();
        $cuenta = null;
        foreach ($cuentas as $c) {
            if ($c->idCuenta == $account_id) {
                $cuenta = $c;
                break;
            }
        }
        if (!$cuenta) {
            return $this->error('Account not found');
        }
        $cuenta = new Cuenta($this->conn);
        $cuenta->idCuenta = $account_id;
        if (!$cuenta->obtenerUna()) {
            return $this->error('Account not found');
        }
        if ($cuenta->saldo < $amount) {
            return $this->error('Insufficient funds');
        }
        $model = new Transaccion($this->conn);
        $model->realizarRetiro($cuenta->idCuenta, $amount, "Rertiro ATM");
        $this->success(['message' => 'Withdrawal successful']);
    }
    public function accounts()
    {
        $token = $this->validateToken();
        if (!$token) {
            return $this->error('Token is invalid', 401);
        }
        $cuentas  = $token->getCuentas();
        $this->success($cuentas);
    }
    /**
     * @return Token|null
     */
    protected function validateToken()
    {
        // vemos en la peticion actual el token en autorization o X-Mi-Token
        $token = null;
        $allHeader=getallheaders();
        if (isset($allHeader['Authorization'])) {
            $token = $allHeader['Authorization'];
        } elseif (isset($allHeader['X-Mi-Token'])) {
            $token = $allHeader['X-Mi-Token'];
        }
        // eliminar Bearer 
        $token = str_replace('Bearer ', '', $token);
        return Token::validate($token, $this->conn);
    }
    protected function success($data = [], $statusCode = 200)
    {
        return self::json([
            'success' => true,
            'data' => $data
        ], $statusCode);
    }


    protected function error($data = [], $statusCode = 400)
    {
        return self::json([
            'success' => false,
            'data' => $data
        ], $statusCode);
    }
    public static function json($data, $statusCode = 200, $headers = ['Content-Type' => 'application/json'])
    {
        http_response_code($statusCode);
        foreach ($headers as $name => $value) {
            header("$name: $value");
        }
        echo json_encode($data);
        exit;
    }
}
