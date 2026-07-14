<?php
/**
 * Controlador TransaccionatmController
 * 
 * Este controlador gestiona todas las operaciones relacionadas con transacciones en ATMs
 */
class TransaccionatmController {
    // Propiedades
    private $db;
    private $session;
    private $model;
    private $atmModel;
    private $cuentaModel;
    private $tarjetaModel;
    
    // Constructor
    public function __construct() {
        // Inicializar conexión a la base de datos
        $database = new Database();
        $this->db = $database->connect();
        
        // Inicializar sesión
        $this->session = new Session();
        
        // Inicializar modelos
        $this->model = new TransaccionATM($this->db);
        $this->atmModel = new ATM($this->db);
        $this->cuentaModel = new Cuenta($this->db);
        $this->tarjetaModel = new Tarjeta($this->db);
    }
    
    /**
     * Método para listar todas las transacciones ATM
     */
    public function listar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Inicializar la variable transaccionesATM como un array vacío
        $transaccionesATM = [];
        
        try {
            // Obtener todas las transacciones ATM
            $resultado = $this->model->obtenerTodas();
            
            if ($resultado && $resultado->rowCount() > 0) {
                $transaccionesATM = $resultado->fetchAll(PDO::FETCH_ASSOC);
                
                // Depuración: Imprimir los valores del primer registro si existe
                if (count($transaccionesATM) > 0) {
                    error_log("Primera transacción recuperada: " . json_encode($transaccionesATM[0]));
                }
                
                error_log("Encontradas " . count($transaccionesATM) . " transacciones ATM");
            } else {
                error_log("No se encontraron transacciones ATM o hubo un error en la consulta");
            }
        } catch (Exception $e) {
            // Registrar cualquier excepción
            error_log("Excepción al obtener transacciones ATM: " . $e->getMessage());
        }
        
        // Definir el título de la página
        $pageTitle = $lang['atm_transaction_list'] ?? 'Listado de Transacciones ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/listar.php';
        include_once 'views/main.php';
    }
    
    /**
     * Método para mostrar detalles de una transacción ATM
     */
    public function ver() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['transaction_id_not_specified'] ?? 'ID de transacción no especificado');
            header('Location: index.php?controller=transaccionatm&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la transacción ATM
        $this->model->idTransaccionATM = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['transaction_not_found'] ?? 'Transacción no encontrada');
            header('Location: index.php?controller=transaccionatm&action=listar');
            exit;
        }
        
        // Obtener datos de la cuenta
        $cuenta = new Cuenta($this->db);
        $cuenta->idCuenta = $this->model->idCuenta;
        $cuenta->obtenerUna();
        
        // Obtener datos del cliente
        $cliente = new Cliente($this->db);
        $cliente->idPersona = $cuenta->idPersona;
        $cliente->obtenerUno();
        
        // Obtener datos del ATM
        $atm = new ATM($this->db);
        $atm->idATM = $this->model->idATM;
        $atm->obtenerUno();
        
        // Obtener datos de la tarjeta
        $tarjeta = new Tarjeta($this->db);
        $tarjeta->idTarjeta = $this->model->idTarjeta;
        $tarjeta->obtenerUna();
        
        // Definir el título de la página
        $pageTitle = $lang['atm_transaction_details'] ?? 'Detalles de Transacción ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/ver.php';
        include_once 'views/main.php';
    }
    
    /**
     * Método para mostrar el formulario de depósito ATM
     */
    public function depositar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Obtener lista de ATMs activos
        $atms = [];
        try {
            $resultadoATMs = $this->atmModel->obtenerActivos();
            if ($resultadoATMs && $resultadoATMs->rowCount() > 0) {
                $atms = $resultadoATMs->fetchAll(PDO::FETCH_ASSOC);
            }
        } catch (Exception $e) {
            error_log("Error al obtener ATMs activos: " . $e->getMessage());
        }
        
        // Inicializar variables
        $cuenta = null;
        $cliente = null;
        $tarjetas = [];
        
        // Verificar si se proporcionó un ID de cuenta
        if (isset($_GET['idCuenta']) && !empty($_GET['idCuenta'])) {
            $idCuenta = (int)$_GET['idCuenta'];
            
            // Obtener datos de la cuenta
            $cuenta = new Cuenta($this->db);
            $cuenta->idCuenta = $idCuenta;
            
            if (!$cuenta->obtenerUna()) {
                $this->session->setFlashMessage('error', $lang['account_not_found'] ?? 'Cuenta no encontrada');
                header('Location: index.php?controller=cuenta&action=listar');
                exit;
            }
            
            // Verificar si la cuenta está activa
            if ($cuenta->estado != 1) {
                $this->session->setFlashMessage('error', $lang['inactive_account_error'] ?? 'La cuenta no está activa');
                header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
                exit;
            }
            
            // Obtener datos del cliente
            $cliente = new Cliente($this->db);
            $cliente->idPersona = $cuenta->idPersona;
            $cliente->obtenerUno();
            
            // Obtener tarjetas asociadas a la cuenta
            try {
                $tarjeta = new Tarjeta($this->db);
                $resultado = $tarjeta->obtenerPorCuenta($idCuenta);
                if ($resultado && $resultado->rowCount() > 0) {
                    $tarjetas = $resultado->fetchAll(PDO::FETCH_ASSOC);
                }
            } catch (Exception $e) {
                error_log("Error al obtener tarjetas: " . $e->getMessage());
            }
        }
        
        // Definir título de la página
        $pageTitle = $lang['atm_deposit'] ?? 'Depósito en ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/depositar.php';
        include_once 'views/main.php';
    }
    
    /**
     * Método para buscar cuenta para depósito ATM
     */
    public function buscarCuentaDeposito() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['nroCuenta']) || empty($_POST['nroCuenta'])) {
            $this->session->setFlashMessage('error', $lang['account_number_required'] ?? 'Número de cuenta requerido');
            header('Location: index.php?controller=transaccionatm&action=depositar');
            exit;
        }
        
        $nroCuenta = trim($_POST['nroCuenta']);
        
        // Buscar la cuenta por número
        $cuentaModel = new Cuenta($this->db);
        $resultado = $cuentaModel->obtenerPorNumeroCuenta($nroCuenta);
        
        if ($resultado) {
            $idCuenta = $cuentaModel->idCuenta;
            
            // Redirigir al formulario de depósito con el ID de la cuenta
            header('Location: index.php?controller=transaccionatm&action=depositar&idCuenta=' . $idCuenta);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['account_not_found'] ?? 'Cuenta no encontrada');
            header('Location: index.php?controller=transaccionatm&action=depositar');
            exit;
        }
    }
    
    /**
     * Método para procesar depósito ATM
     */
    public function procesarDeposito() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        if ($_SERVER['REQUEST_METHOD'] != 'POST' || 
            !isset($_POST['idCuenta']) || empty($_POST['idCuenta']) || 
            !isset($_POST['monto']) || empty($_POST['monto']) ||
            !isset($_POST['idATM']) || empty($_POST['idATM']) ||
            !isset($_POST['idTarjeta']) || empty($_POST['idTarjeta'])) {
            $this->session->setFlashMessage('error', $lang['missing_required_fields'] ?? 'Campos requeridos faltantes');
            header('Location: index.php?controller=transaccionatm&action=depositar');
            exit;
        }
        
        $idCuenta = (int)$_POST['idCuenta'];
        $monto = (float)$_POST['monto'];
        $idATM = (int)$_POST['idATM'];
        $idTarjeta = (int)$_POST['idTarjeta'];
        $descripcion = isset($_POST['descripcion']) ? trim($_POST['descripcion']) : ($lang['atm_deposit'] ?? 'Depósito ATM');
        
        // Validar monto
        if ($monto <= 0) {
            $this->session->setFlashMessage('error', $lang['invalid_amount'] ?? 'Monto inválido');
            header('Location: index.php?controller=transaccionatm&action=depositar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Obtener cuenta
        $cuenta = new Cuenta($this->db);
        $cuenta->idCuenta = $idCuenta;
        
        if (!$cuenta->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found'] ?? 'Cuenta no encontrada');
            header('Location: index.php?controller=transaccionatm&action=depositar');
            exit;
        }
        
        // Verificar si la cuenta está activa
        if ($cuenta->estado != 1) {
            $this->session->setFlashMessage('error', $lang['inactive_account_error'] ?? 'La cuenta no está activa');
            header('Location: index.php?controller=transaccionatm&action=depositar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Verificar disponibilidad del ATM
        $atm = new ATM($this->db);
        $atm->idATM = $idATM;
        if (!$atm->obtenerUno() || $atm->estado != 1) {
            $this->session->setFlashMessage('error', $lang['atm_not_available'] ?? 'ATM no disponible');
            header('Location: index.php?controller=transaccionatm&action=depositar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Verificar tarjeta
        $tarjeta = new Tarjeta($this->db);
        $tarjeta->idTarjeta = $idTarjeta;
        if (!$tarjeta->obtenerUna() || $tarjeta->estado != 1) {
            $this->session->setFlashMessage('error', $lang['card_not_available'] ?? 'Tarjeta no disponible');
            header('Location: index.php?controller=transaccionatm&action=depositar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Iniciar transacción en la base de datos
        $this->db->beginTransaction();
        
        try {
            // Actualizar saldo de la cuenta
            $saldoOriginal = $cuenta->saldo;
            $nuevoSaldo = $saldoOriginal + $monto;
            $cuenta->saldo = $nuevoSaldo;
            
            if (!$cuenta->actualizarSaldo($monto, true)) {
                throw new Exception($lang['update_balance_error'] ?? 'Error al actualizar el saldo');
            }
            
            // Actualizar saldo del ATM
            if (!$atm->actualizarSaldo($monto, true)) {
                throw new Exception($lang['update_atm_balance_error'] ?? 'Error al actualizar el saldo del ATM');
            }
            
            // Registrar transacción ATM
            $this->model->tipoTransaccion = 'deposito';
            $this->model->fecha = date('Y-m-d');
            $this->model->hora = date('H:i:s');
            $this->model->descripcion = $descripcion;
            $this->model->monto = $monto;
            $this->model->idCuenta = $idCuenta;
            $this->model->cuentaOrigen = null;
            $this->model->cuentaDestino = $cuenta->nroCuenta;
            $this->model->saldoResultante = $nuevoSaldo;
            $this->model->idATM = $idATM;
            $this->model->idTarjeta = $idTarjeta;
            
            if (!$this->model->crear()) {
                throw new Exception($lang['transaction_error'] ?? 'Error en la transacción');
            }
            
            // Confirmar transacción
            $this->db->commit();
            
            $this->session->setFlashMessage('success', $lang['deposit_success'] ?? 'Depósito realizado con éxito');
            header('Location: index.php?controller=transaccionatm&action=comprobante&id=' . $this->model->idTransaccionATM);
            exit;
        } catch (Exception $e) {
            // Revertir transacción en caso de error
            $this->db->rollBack();
            $this->session->setFlashMessage('error', $e->getMessage());
            header('Location: index.php?controller=transaccionatm&action=depositar&idCuenta=' . $idCuenta);
            exit;
        }
    }
    
    /**
     * Método para mostrar el formulario de retiro ATM
     */
    public function retirar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Obtener lista de ATMs activos
        $atms = [];
        try {
            $resultadoATMs = $this->atmModel->obtenerActivos();
            if ($resultadoATMs && $resultadoATMs->rowCount() > 0) {
                $atms = $resultadoATMs->fetchAll(PDO::FETCH_ASSOC);
            }
        } catch (Exception $e) {
            error_log("Error al obtener ATMs activos: " . $e->getMessage());
        }
        
        // Inicializar variables
        $cuenta = null;
        $cliente = null;
        $tarjetas = [];
        
        // Verificar si se proporcionó un ID de cuenta
        if (isset($_GET['idCuenta']) && !empty($_GET['idCuenta'])) {
            $idCuenta = (int)$_GET['idCuenta'];
            
            // Obtener datos de la cuenta
            $cuenta = new Cuenta($this->db);
            $cuenta->idCuenta = $idCuenta;
            
            if (!$cuenta->obtenerUna()) {
                $this->session->setFlashMessage('error', $lang['account_not_found'] ?? 'Cuenta no encontrada');
                header('Location: index.php?controller=cuenta&action=listar');
                exit;
            }
            
            // Verificar si la cuenta está activa
            if ($cuenta->estado != 1) {
                $this->session->setFlashMessage('error', $lang['inactive_account_error'] ?? 'La cuenta no está activa');
                header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
                exit;
            }
            
            // Obtener datos del cliente
            $cliente = new Cliente($this->db);
            $cliente->idPersona = $cuenta->idPersona;
            $cliente->obtenerUno();
            
            // Obtener tarjetas asociadas a la cuenta
            try {
                $tarjeta = new Tarjeta($this->db);
                $resultado = $tarjeta->obtenerPorCuenta($idCuenta);
                if ($resultado && $resultado->rowCount() > 0) {
                    $tarjetas = $resultado->fetchAll(PDO::FETCH_ASSOC);
                }
            } catch (Exception $e) {
                error_log("Error al obtener tarjetas: " . $e->getMessage());
            }
        }
        
        // Definir título de la página
        $pageTitle = $lang['atm_withdraw'] ?? 'Retiro en ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/retirar.php';
        include_once 'views/main.php';
    }
    
    /**
     * Método para buscar cuenta para retiro ATM
     */
    public function buscarCuentaRetiro() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['nroCuenta']) || empty($_POST['nroCuenta'])) {
            $this->session->setFlashMessage('error', $lang['account_number_required'] ?? 'Número de cuenta requerido');
            header('Location: index.php?controller=transaccionatm&action=retirar');
            exit;
        }
        
        $nroCuenta = trim($_POST['nroCuenta']);
        
        // Buscar la cuenta por número
        $cuentaModel = new Cuenta($this->db);
        $resultado = $cuentaModel->obtenerPorNumeroCuenta($nroCuenta);
        
        if ($resultado) {
            $idCuenta = $cuentaModel->idCuenta;
            
            // Redirigir al formulario de retiro con el ID de la cuenta
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['account_not_found'] ?? 'Cuenta no encontrada');
            header('Location: index.php?controller=transaccionatm&action=retirar');
            exit;
        }
    }
    
    /**
     * Método para procesar retiro ATM
     */
    public function procesarRetiro() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        if ($_SERVER['REQUEST_METHOD'] != 'POST' || 
            !isset($_POST['idCuenta']) || empty($_POST['idCuenta']) || 
            !isset($_POST['monto']) || empty($_POST['monto']) ||
            !isset($_POST['idATM']) || empty($_POST['idATM']) ||
            !isset($_POST['idTarjeta']) || empty($_POST['idTarjeta'])) {
            $this->session->setFlashMessage('error', $lang['missing_required_fields'] ?? 'Campos requeridos faltantes');
            header('Location: index.php?controller=transaccionatm&action=retirar');
            exit;
        }
        
        $idCuenta = (int)$_POST['idCuenta'];
        $monto = (float)$_POST['monto'];
        $idATM = (int)$_POST['idATM'];
        $idTarjeta = (int)$_POST['idTarjeta'];
        $descripcion = isset($_POST['descripcion']) ? trim($_POST['descripcion']) : ($lang['atm_withdrawal'] ?? 'Retiro ATM');
        
        // Validar monto
        if ($monto <= 0) {
            $this->session->setFlashMessage('error', $lang['invalid_amount'] ?? 'Monto inválido');
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Obtener cuenta
        $cuenta = new Cuenta($this->db);
        $cuenta->idCuenta = $idCuenta;
        
        if (!$cuenta->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found'] ?? 'Cuenta no encontrada');
            header('Location: index.php?controller=transaccionatm&action=retirar');
            exit;
        }
        
        // Verificar si la cuenta está activa
        if ($cuenta->estado != 1) {
            $this->session->setFlashMessage('error', $lang['inactive_account_error'] ?? 'La cuenta no está activa');
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Verificar si hay saldo suficiente
        if ($cuenta->saldo < $monto) {
            $this->session->setFlashMessage('error', $lang['insufficient_funds'] ?? 'Fondos insuficientes');
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Verificar disponibilidad del ATM
        $atm = new ATM($this->db);
        $atm->idATM = $idATM;
        if (!$atm->obtenerUno() || $atm->estado != 1) {
            $this->session->setFlashMessage('error', $lang['atm_not_available'] ?? 'ATM no disponible');
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Verificar si el ATM tiene saldo suficiente
        if ($atm->saldoActual < $monto) {
            $this->session->setFlashMessage('error', $lang['atm_insufficient_funds'] ?? 'El ATM no tiene suficiente dinero');
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Verificar tarjeta
        $tarjeta = new Tarjeta($this->db);
        $tarjeta->idTarjeta = $idTarjeta;
        if (!$tarjeta->obtenerUna() || $tarjeta->estado != 1) {
            $this->session->setFlashMessage('error', $lang['card_not_available'] ?? 'Tarjeta no disponible');
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
        
        // Iniciar transacción en la base de datos
        $this->db->beginTransaction();
        
        try {
            // Actualizar saldo de la cuenta
            $saldoOriginal = $cuenta->saldo;
            $nuevoSaldo = $saldoOriginal - $monto;
            $cuenta->saldo = $nuevoSaldo;
            
            if (!$cuenta->actualizarSaldo($monto, false)) {
                throw new Exception($lang['update_balance_error'] ?? 'Error al actualizar el saldo');
            }
            
            // Actualizar saldo del ATM
            if (!$atm->actualizarSaldo($monto, false)) {
                throw new Exception($lang['update_atm_balance_error'] ?? 'Error al actualizar el saldo del ATM');
            }
            
            // Registrar transacción ATM
            $this->model->tipoTransaccion = 'retiro';
            $this->model->fecha = date('Y-m-d');
            $this->model->hora = date('H:i:s');
            $this->model->descripcion = $descripcion;
            $this->model->monto = $monto;
            $this->model->idCuenta = $idCuenta;
            $this->model->cuentaOrigen = $cuenta->nroCuenta;
            $this->model->cuentaDestino = null;
            $this->model->saldoResultante = $nuevoSaldo;
            $this->model->idATM = $idATM;
            $this->model->idTarjeta = $idTarjeta;
            
            if (!$this->model->crear()) {
                throw new Exception($lang['transaction_error'] ?? 'Error en la transacción');
            }
            
            // Confirmar transacción
            $this->db->commit();
            
            $this->session->setFlashMessage('success', $lang['withdrawal_success'] ?? 'Retiro realizado con éxito');
            header('Location: index.php?controller=transaccionatm&action=comprobante&id=' . $this->model->idTransaccionATM);
            exit;
        } catch (Exception $e) {
            // Revertir transacción en caso de error
            $this->db->rollBack();
            $this->session->setFlashMessage('error', $e->getMessage());
            header('Location: index.php?controller=transaccionatm&action=retirar&idCuenta=' . $idCuenta);
            exit;
        }
    }
    
    /**
     * Método para generar comprobante de transacción ATM
     */
    public function comprobante() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['transaction_id_not_specified'] ?? 'ID de transacción no especificado');
            header('Location: index.php?controller=transaccionatm&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la transacción ATM
        $this->model->idTransaccionATM = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['transaction_not_found'] ?? 'Transacción no encontrada');
            header('Location: index.php?controller=transaccionatm&action=listar');
            exit;
        }
        
        // Obtener datos de la cuenta
        $cuenta = new Cuenta($this->db);
        $cuenta->idCuenta = $this->model->idCuenta;
        $cuenta->obtenerUna();
        
        // Obtener datos del cliente
        $cliente = new Cliente($this->db);
        $cliente->idPersona = $cuenta->idPersona;
        $cliente->obtenerUno();
        
        // Obtener datos del ATM
        $atm = new ATM($this->db);
        $atm->idATM = $this->model->idATM;
        $atm->obtenerUno();
        
        // Obtener datos de la tarjeta
        $tarjeta = new Tarjeta($this->db);
        $tarjeta->idTarjeta = $this->model->idTarjeta;
        $tarjeta->obtenerUna();
        
        // Definir título de la página
        $pageTitle = $lang['atm_transaction_receipt'] ?? 'Comprobante de Transacción ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/comprobante.php';
        include_once 'views/main.php';
    }
    
    /**
     * Método para buscar transacciones ATM
     */
    public function buscar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Inicializar variables de búsqueda
        $fechaInicio = isset($_GET['fechaInicio']) ? $_GET['fechaInicio'] : date('Y-m-01'); // Primer día del mes actual
        $fechaFin = isset($_GET['fechaFin']) ? $_GET['fechaFin'] : date('Y-m-d'); // Hoy
        $tipoTransaccion = isset($_GET['tipoTransaccion']) ? $_GET['tipoTransaccion'] : '';
        $idATM = isset($_GET['idATM']) ? (int)$_GET['idATM'] : 0;
        
        // Construir criterios de búsqueda
        $criterios = [];
        
        if (!empty($fechaInicio)) {
            $criterios['fechaInicio'] = $fechaInicio;
        }
        
        if (!empty($fechaFin)) {
            $criterios['fechaFin'] = $fechaFin;
        }
        
        if (!empty($tipoTransaccion)) {
            $criterios['tipoTransaccion'] = $tipoTransaccion;
        }
        
        if ($idATM > 0) {
            $criterios['idATM'] = $idATM;
        }
        
        // Inicializar arrays
        $transaccionesATM = [];
        $atms = [];
        
        try {
            // Obtener resultados de la búsqueda
            $resultado = $this->model->buscar($criterios);
            if ($resultado && $resultado->rowCount() > 0) {
                $transaccionesATM = $resultado->fetchAll(PDO::FETCH_ASSOC);
            }
            
            // Obtener ATMs para el selector
            $resultadoATMs = $this->atmModel->obtenerTodos();
            if ($resultadoATMs && $resultadoATMs->rowCount() > 0) {
                $atms = $resultadoATMs->fetchAll(PDO::FETCH_ASSOC);
            }
        } catch (Exception $e) {
            error_log("Error en la búsqueda de transacciones: " . $e->getMessage());
        }
        
        // Definir título de la página
        $pageTitle = $lang['search_atm_transactions'] ?? 'Buscar Transacciones ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/buscar.php';
        include_once 'views/main.php';
    }
    
    /**
     * Método para mostrar el dashboard de transacciones ATM
     */
    public function dashboard() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Obtener estadísticas
        try {
            // Transacciones recientes
            $transaccionesRecientes = [];
            $resultadoRecientes = $this->model->obtenerRecientes(5);
            if ($resultadoRecientes && $resultadoRecientes->rowCount() > 0) {
                $transaccionesRecientes = $resultadoRecientes->fetchAll(PDO::FETCH_ASSOC);
            }
            
            // ATMs activos
            $atmsActivos = [];
            $resultadoATMs = $this->atmModel->obtenerActivos();
            if ($resultadoATMs && $resultadoATMs->rowCount() > 0) {
                $atmsActivos = $resultadoATMs->fetchAll(PDO::FETCH_ASSOC);
            }
            
            // Estadísticas por tipo de transacción (ejemplo)
            $estadisticasPorTipo = [
                'depositos' => 0,
                'retiros' => 0,
                'consultas' => 0
            ];
            
            // Aquí se podrían calcular más estadísticas
            
        } catch (Exception $e) {
            error_log("Error al cargar el dashboard: " . $e->getMessage());
        }
        
        // Definir título de la página
        $pageTitle = $lang['atm_transactions_dashboard'] ?? 'Dashboard de Transacciones ATM';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'transaccionatm';
        
        // Incluir la vista
        $contentView = 'views/cajero/dashboard.php';
        include_once 'views/main.php';
    }
}