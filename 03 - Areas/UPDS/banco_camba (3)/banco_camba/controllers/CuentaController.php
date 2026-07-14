<?php

class CuentaController {
    // Propiedades
    private $db;
    private $session;
    private $model;
    private $hash ;
    
    // Constructor
    public function __construct() {
        // Inicializar conexión a la base de datos
        $database = new Database();
        $this->db = $database->connect();
        
        // Inicializar sesión
        $this->session = new Session();
        
        // Inicializar modelo
        $this->model = new Cuenta($this->db);
    }

    /**
 * Imprimir extracto de cuenta
 * Fecha de implementación: 08/03/2025
 * @return void
 */
public function imprimirExtracto() {
    global $lang;
    
    // Verificar si el usuario ha iniciado sesión
    if (!isset($_SESSION['user_id'])) {
        header('Location: index.php?controller=usuario&action=login');
        exit;
    }
    
    // Verificar si se proporcionó un ID
    if (!isset($_GET['id']) || empty($_GET['id'])) {
        $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
        header('Location: index.php?controller=cuenta&action=listar');
        exit;
    }
    
    $id = (int)$_GET['id'];
    
    // Obtener datos de la cuenta
    $this->model->idCuenta = $id;
    if (!$this->model->obtenerUna()) {
        $this->session->setFlashMessage('error', $lang['account_not_found']);
        header('Location: index.php?controller=cuenta&action=listar');
        exit;
    }
    
    // Obtener datos del cliente
    $clienteModel = new Cliente($this->db);
    $clienteModel->idPersona = $this->model->idPersona;
    $clienteModel->obtenerUno();
    
    // Procesar fechas si se enviaron
    $fechaInicio = isset($_GET['fechaInicio']) ? $_GET['fechaInicio'] : date('Y-m-01'); // Primer día del mes actual
    $fechaFin = isset($_GET['fechaFin']) ? $_GET['fechaFin'] : date('Y-m-d'); // Hoy
    
    // Obtener transacciones para el rango de fechas
    $transaccionModel = new Transaccion($this->db);
    $resultado = $transaccionModel->obtenerPorRangoFechas($id, $fechaInicio, $fechaFin);
    $transacciones = $resultado->fetchAll(PDO::FETCH_ASSOC);
    
    // Calcular totales
    $totalDepositos = 0;
    $totalRetiros = 0;
    $totalTransferenciasSalientes = 0;
    $totalTransferenciasEntrantes = 0;
    
    foreach ($transacciones as $transaccion) {
        switch($transaccion['tipoTransaccion']) {
            case 1: // Retiro
                $totalRetiros += $transaccion['monto'];
                break;
            case 2: // Depósito
                $totalDepositos += $transaccion['monto'];
                break;
            case 3: // Transferencia entrante
                $totalTransferenciasEntrantes += $transaccion['monto'];
                break;
            case 4: // Transferencia saliente
                $totalTransferenciasSalientes += $transaccion['monto'];
                break;
        }
    }
    
    // Cargar la vista específica para impresión
    include 'views/cuentas/imprimir_extracto.php';
}
    
    // Método para mostrar todas las cuentas
    public function listar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Obtener todas las cuentas
        $resultado = $this->model->obtenerTodas();
        $cuentas = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['account_list'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cuenta';
        
        // Incluir la vista
        $contentView = 'views/cuentas/listar.php';
        include_once 'views/main.php';
    }
    
    // Método para mostrar el formulario de creación de cuenta
    public function crear() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID de cliente
        $idPersona = isset($_GET['idPersona']) ? (int)$_GET['idPersona'] : 0;
        $cliente = null;
        
        if ($idPersona > 0) {
            // Obtener datos del cliente
            $clienteModel = new Cliente($this->db);
            $clienteModel->idPersona = $idPersona;
            
            if ($clienteModel->obtenerUno()) {
                $cliente = [
                    'idPersona' => $clienteModel->idPersona,
                    'nombre' => $clienteModel->nombre,
                    'apellidoPaterno' => $clienteModel->apellidoPaterno,
                    'apellidoMaterno' => $clienteModel->apellidoMaterno,
                    'ci' => $clienteModel->ci
                ];
            }
        }
        
        // Si no se proporcionó un cliente o no se encontró, obtener la lista de clientes
        $clientes = [];
        if (!$cliente) {
            $clienteModel = new Cliente($this->db);
            $resultado = $clienteModel->obtenerTodos();
            $clientes = $resultado->fetchAll(PDO::FETCH_ASSOC);
        }
        
        // Definir el título de la página
        $pageTitle = $lang['new_account'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cuenta';
        
        // Incluir la vista
        $contentView = 'views/cuentas/crear.php';
        include_once 'views/main.php';
    }
    
  /**
 * Método para guardar una nueva cuenta
 * @return void
 */
public function guardar() {
    global $lang;
    
    // Verificar si el usuario ha iniciado sesión
    if (!isset($_SESSION['user_id'])) {
        header('Location: index.php?controller=usuario&action=login');
        exit;
    }
    
    // Verificar si se enviaron datos por POST
    if ($_SERVER['REQUEST_METHOD'] == 'POST') {
        // Obtener datos del formulario
        $idPersona = isset($_POST['idPersona']) ? (int)$_POST['idPersona'] : 0;
        $tipoCuenta = isset($_POST['tipoCuenta']) ? (int)$_POST['tipoCuenta'] : 0;
        $tipoMoneda = isset($_POST['tipoMoneda']) ? (int)$_POST['tipoMoneda'] : 0;
        $saldoInicial = isset($_POST['saldoInicial']) ? (float)$_POST['saldoInicial'] : 0;
        
        // Validar datos
        if ($idPersona <= 0 || $tipoCuenta <= 0 || $tipoMoneda <= 0) {
            $this->session->setFlashMessage('error', $lang['all_fields_required']);
            header('Location: index.php?controller=cuenta&action=crear');
            exit;
        }
        
        // Verificar saldo inicial no negativo
        if ($saldoInicial < 0) {
            $this->session->setFlashMessage('error', $lang['negative_balance_not_allowed']);
            header('Location: index.php?controller=cuenta&action=crear');
            exit;
        }
        
        // Verificar si el cliente existe
        $clienteModel = new Cliente($this->db);
        $clienteModel->idPersona = $idPersona;
        if (!$clienteModel->obtenerUno()) {
            $this->session->setFlashMessage('error', $lang['client_not_found']);
            header('Location: index.php?controller=cuenta&action=crear');
            exit;
        }
        
        // Asignar datos al modelo
        $this->model->idPersona = $idPersona;
        $this->model->tipoCuenta = $tipoCuenta;
        $this->model->tipoMoneda = $tipoMoneda;
        $this->model->fechaApertura = date('Y-m-d');
        $this->model->estado = 1 ; // Activa por defecto
        $this->model->saldo = $saldoInicial;
        
        // Generar hash único
        $this->model->hash = bin2hex(random_bytes(16));
        
        // Guardar cuenta
        if ($this->model->crear()) {
            // Si hay saldo inicial, registrar como transacción de depósito
            if ($saldoInicial > 0) {
                $transaccionModel = new Transaccion($this->db);
                $transaccionModel->idCuenta = $this->model->idCuenta;
                $transaccionModel->tipoTransaccion = 2; // Depósito
                $transaccionModel->fecha = date('Y-m-d');
                $transaccionModel->hora = date('H:i:s');
                $transaccionModel->descripcion = $lang['initial_deposit'];
                $transaccionModel->monto = $saldoInicial;
                $transaccionModel->saldoResultante = $saldoInicial;
                $transaccionModel->hash = bin2hex(random_bytes(16));
                $transaccionModel->crear();
            }
            
            $this->session->setFlashMessage('success', $lang['account_saved']);
            header('Location: index.php?controller=cuenta&action=ver&id=' . $this->model->idCuenta);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['account_save_error']);
            header('Location: index.php?controller=cuenta&action=crear');
            exit;
        }
    } else {
        // Si no se enviaron datos por POST, redirigir al formulario
        header('Location: index.php?controller=cuenta&action=crear');
        exit;
    }
}
    
    // Método para ver detalles de una cuenta
    public function ver() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la cuenta
        $this->model->idCuenta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        // Obtener datos del cliente
        $clienteModel = new Cliente($this->db);
        $clienteModel->idPersona = $this->model->idPersona;
        $clienteModel->obtenerUno();
        
        // Obtener transacciones de la cuenta
        $transaccionModel = new Transaccion($this->db);
        $resultado = $transaccionModel->obtenerPorCuenta($id);
        $transacciones = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['account_details'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cuenta';
        
        // Incluir la vista
        $contentView = 'views/cuentas/ver.php';
        include_once 'views/main.php';
    }
    
    // Método para mostrar el formulario de edición
    public function editar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la cuenta
        $this->model->idCuenta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        // Obtener datos del cliente
        $clienteModel = new Cliente($this->db);
        $clienteModel->idPersona = $this->model->idPersona;
        $clienteModel->obtenerUno();
        
        // Definir el título de la página
        $pageTitle = $lang['edit_account'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cuenta';
        
        // Incluir la vista
        $contentView = 'views/cuentas/editar.php';
        include_once 'views/main.php';
    }
    
    // Método para actualizar una cuenta
    public function actualizar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se enviaron datos por POST
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Obtener datos del formulario
            $idCuenta = isset($_POST['idCuenta']) ? (int)$_POST['idCuenta'] : 0;
            $tipoCuenta = isset($_POST['tipoCuenta']) ? (int)$_POST['tipoCuenta'] : 0;
            $estado = isset($_POST['estado']) ? (int)$_POST['estado'] : 0;
            
            // Validar datos
            if ($idCuenta <= 0 || $tipoCuenta <= 0 || $estado <= 0) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=cuenta&action=editar&id=' . $idCuenta);
                exit;
            }
            
            // Obtener datos actuales de la cuenta
            $this->model->idCuenta = $idCuenta;
            if (!$this->model->obtenerUna()) {
                $this->session->setFlashMessage('error', $lang['account_not_found']);
                header('Location: index.php?controller=cuenta&action=listar');
                exit;
            }
            
            // Asignar nuevos datos al modelo
            $this->model->tipoCuenta = $tipoCuenta;
            $this->model->estado = $estado;
            
            // Actualizar cuenta
            if ($this->model->actualizar()) {
                $this->session->setFlashMessage('success', $lang['account_updated']);
                header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['account_update_error']);
                header('Location: index.php?controller=cuenta&action=editar&id=' . $idCuenta);
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al listado
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
    }
    
    // Método para cerrar/bloquear una cuenta
    public function cerrar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la cuenta
        $this->model->idCuenta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        // Verificar que la cuenta no esté ya cerrada
        if ($this->model->estado != 1) {
            $this->session->setFlashMessage('error', $lang['account_already_closed']);
            header('Location: index.php?controller=cuenta&action=ver&id=' . $id);
            exit;
        }
        
        // Actualizar el estado a inactivo (2)
        $this->model->estado = 2;
        
        // Actualizar cuenta
        if ($this->model->actualizar()) {
            $this->session->setFlashMessage('success', $lang['account_closed']);
            header('Location: index.php?controller=cuenta&action=ver&id=' . $id);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['account_close_error']);
            header('Location: index.php?controller=cuenta&action=ver&id=' . $id);
            exit;
        }
    }
    
    // Método para reactivar una cuenta cerrada
    public function reactivar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la cuenta
        $this->model->idCuenta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        // Verificar que la cuenta no esté ya activa
        if ($this->model->estado == 1) {
            $this->session->setFlashMessage('error', $lang['account_already_active']);
            header('Location: index.php?controller=cuenta&action=ver&id=' . $id);
            exit;
        }
        
        // Actualizar el estado a activo (1)
        $this->model->estado = 1;
        
        // Actualizar cuenta
       if ($this->model->actualizar()) {
           $this->session->setFlashMessage('success', $lang['account_reactivated']);
           header('Location: index.php?controller=cuenta&action=ver&id=' . $id);
            exit;
       } else {
           $this->session->setFlashMessage('error', $lang['account_reactivation_error']);
           header('Location: index.php?controller=cuenta&action=ver&id=' . $id);
           exit;
       }
    }
    
    // Método para generar extracto de cuenta (historial de transacciones por rango de fechas)
    public function extracto() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la cuenta
        $this->model->idCuenta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['account_not_found']);
            header('Location: index.php?controller=cuenta&action=listar');
            exit;
        }
        
        // Obtener datos del cliente
        $clienteModel = new Cliente($this->db);
        $clienteModel->idPersona = $this->model->idPersona;
        $clienteModel->obtenerUno();
        
        // Procesar fechas si se enviaron
        $fechaInicio = isset($_GET['fechaInicio']) ? $_GET['fechaInicio'] : date('Y-m-01'); // Primer día del mes actual
        $fechaFin = isset($_GET['fechaFin']) ? $_GET['fechaFin'] : date('Y-m-d'); // Hoy
        
        // Obtener transacciones para el rango de fechas
        $transaccionModel = new Transaccion($this->db);
        $resultado = $transaccionModel->obtenerPorRangoFechas($id, $fechaInicio, $fechaFin);
        $transacciones = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['account_statement'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cuenta';
        
        // Incluir la vista
        $contentView = 'views/cuentas/extracto.php';
        include_once 'views/main.php';
    }
}
?>