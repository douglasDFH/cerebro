<?php

class TransaccionController {
 // Propiedades
 private $db;
 private $session;
 private $model;
 
 // Constructor
 public function __construct() {
     // Inicializar conexión a la base de datos
     $database = new Database();
     $this->db = $database->connect();
     
     // Inicializar sesión
     $this->session = new Session();
     
     // Inicializar modelo
     $this->model = new Transaccion($this->db);
 }
 
 // Método para listar todas las transacciones
 public function listar() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     // Obtener todas las transacciones
     $resultado = $this->model->obtenerTodas();
     $transacciones = $resultado->fetchAll(PDO::FETCH_ASSOC);
     
     // Definir el título de la página
     $pageTitle = $lang['transaction_list'];
     
     // Determinar el controlador actual para marcar el menú
     $controller = 'transaccion';
     
     // Incluir la vista
     $contentView = 'views/transacciones/listar.php';
     include_once 'views/main.php';
 }
 
 // Método para mostrar el formulario de depósito
 public function depositar() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     // Verificar si se proporcionó un ID de cuenta
     if (isset($_GET['idCuenta']) && !empty($_GET['idCuenta'])) {
         $idCuenta = (int)$_GET['idCuenta'];
         
         // Obtener datos de la cuenta
         $cuenta = new Cuenta($this->db);
         $cuenta->idCuenta = $idCuenta;
         
         if (!$cuenta->obtenerUna()) {
             $this->session->setFlashMessage('error', $lang['account_not_found']);
             header('Location: index.php?controller=cuenta&action=listar');
             exit;
         }
         
         // Verificar si la cuenta está activa
        // if ($cuenta->estado != 1) {
          //   $this->session->setFlashMessage('error', $lang['inactive_account_error']);
          //   header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
          //   exit;
       //  }
         
         // Obtener datos del cliente
         $cliente = new Cliente($this->db);
         $cliente->idPersona = $cuenta->idPersona;
         $cliente->obtenerUno();
     }
     
     // Definir título de la página
     $pageTitle = $lang['deposit_funds'];
     
     // Determinar el controlador actual para marcar el menú
     $controller = 'transaccion';
     
     // Definir vista a incluir
     $contentView = 'views/transacciones/depositar.php';
     
     // Mostrar plantilla principal
     include_once 'views/main.php';
 }
 
 // Método para buscar cuenta para depósito
 public function buscarCuentaDeposito() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['nroCuenta']) || empty($_POST['nroCuenta'])) {
         $this->session->setFlashMessage('error', $lang['account_number_required']);
         header('Location: index.php?controller=transaccion&action=depositar');
         exit;
     }
     
     $nroCuenta = trim($_POST['nroCuenta']);
     
     // Buscar la cuenta por número
     $cuentaModel = new Cuenta($this->db);
     $resultado = $cuentaModel->obtenerPorNumeroCuenta($nroCuenta);
     
     if ($resultado) {
         $idCuenta = $cuentaModel->idCuenta;
         
         // Redirigir al formulario de depósito con el ID de la cuenta
         header('Location: index.php?controller=transaccion&action=depositar&idCuenta=' . $idCuenta);
         exit;
     } else {
         $this->session->setFlashMessage('error', $lang['account_not_found']);
         header('Location: index.php?controller=transaccion&action=depositar');
         exit;
     }
 }
 
 // Método para procesar el depósito
 public function procesarDeposito() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['idCuenta']) || empty($_POST['idCuenta']) || !isset($_POST['monto']) || empty($_POST['monto'])) {
         $this->session->setFlashMessage('error', $lang['missing_required_fields']);
         header('Location: index.php?controller=transaccion&action=depositar');
         exit;
     }
     
     $idCuenta = (int)$_POST['idCuenta'];
     $monto = (float)$_POST['monto'];
     $descripcion = isset($_POST['descripcion']) ? trim($_POST['descripcion']) : $lang['deposit'];
     
     // Validar monto
     if ($monto <= 0) {
         $this->session->setFlashMessage('error', $lang['invalid_amount']);
         header('Location: index.php?controller=transaccion&action=depositar&idCuenta=' . $idCuenta);
         exit;
     }
     
     try {
         // Realizar el depósito usando el modelo actualizado
         $this->model->realizarDeposito($idCuenta, $monto, $descripcion);
         
         $this->session->setFlashMessage('success', $lang['deposit_success']);
         header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
         exit;
     } catch (Exception $e) {
         $this->session->setFlashMessage('error', $e->getMessage());
         header('Location: index.php?controller=transaccion&action=depositar&idCuenta=' . $idCuenta);
         exit;
     }
 }
 
 // Método para mostrar el formulario de retiro
 public function retirar() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     // Verificar si se proporcionó un ID de cuenta
     if (isset($_GET['idCuenta']) && !empty($_GET['idCuenta'])) {
         $idCuenta = (int)$_GET['idCuenta'];
         
         // Obtener datos de la cuenta
         $cuenta = new Cuenta($this->db);
         $cuenta->idCuenta = $idCuenta;
         
         if (!$cuenta->obtenerUna()) {
             $this->session->setFlashMessage('error', $lang['account_not_found']);
             header('Location: index.php?controller=cuenta&action=listar');
             exit;
         }
         
         // Verificar si la cuenta está activa
//if ($cuenta->estado != 1) {
          //   $this->session->setFlashMessage('error', $lang['inactive_account_error']);
          //   header('Location: index.php?controller=transaccion&action=depositar&idCuenta=' . $idCuenta);
          //   exit;
         //}//
         
         // Obtener datos del cliente
         $cliente = new Cliente($this->db);
         $cliente->idPersona = $cuenta->idPersona;
         $cliente->obtenerUno();
     }
     
     // Definir título de la página
     $pageTitle = $lang['withdraw_funds'];
     
     // Determinar el controlador actual para marcar el menú
     $controller = 'transaccion';
     
     // Definir vista a incluir
     $contentView = 'views/transacciones/retirar.php';
     
     // Mostrar plantilla principal
     include_once 'views/main.php';
 }
 
 // Método para buscar cuenta para retiro
 public function buscarCuentaRetiro() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['nroCuenta']) || empty($_POST['nroCuenta'])) {
         $this->session->setFlashMessage('error', $lang['account_number_required']);
         header('Location: index.php?controller=transaccion&action=retirar');
         exit;
     }
     
     $nroCuenta = trim($_POST['nroCuenta']);
     
     // Buscar la cuenta por número
     $cuentaModel = new Cuenta($this->db);
     $resultado = $cuentaModel->obtenerPorNumeroCuenta($nroCuenta);
     
     if ($resultado) {
         $idCuenta = $cuentaModel->idCuenta;
         
         // Redirigir al formulario de retiro con el ID de la cuenta
         header('Location: index.php?controller=transaccion&action=retirar&idCuenta=' . $idCuenta);
         exit;
     } else {
         $this->session->setFlashMessage('error', $lang['account_not_found']);
         header('Location: index.php?controller=transaccion&action=retirar');
         exit;
     }
 }
 
 // Método para procesar el retiro
 public function procesarRetiro() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['idCuenta']) || empty($_POST['idCuenta']) || !isset($_POST['monto']) || empty($_POST['monto'])) {
         $this->session->setFlashMessage('error', $lang['missing_required_fields']);
         header('Location: index.php?controller=transaccion&action=retirar');
         exit;
     }
     
     $idCuenta = (int)$_POST['idCuenta'];
     $monto = (float)$_POST['monto'];
     $descripcion = isset($_POST['descripcion']) ? trim($_POST['descripcion']) : $lang['withdrawal'];
     
     // Validar monto
     if ($monto <= 0) {
         $this->session->setFlashMessage('error', $lang['invalid_amount']);
         header('Location: index.php?controller=transaccion&action=retirar&idCuenta=' . $idCuenta);
         exit;
     }
     
     try {
         // Realizar el retiro usando el modelo actualizado
         $this->model->realizarRetiro($idCuenta, $monto, $descripcion);
         
         $this->session->setFlashMessage('success', $lang['withdrawal_success']);
         header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
         exit;
     } catch (Exception $e) {
         $this->session->setFlashMessage('error', $e->getMessage());
         header('Location: index.php?controller=transaccion&action=retirar&idCuenta=' . $idCuenta);
         exit;
     }
 }
 
 // Método para mostrar el formulario de transferencia
 public function transferir() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     // Variables para almacenar datos de cuenta de origen y destino
     $cuentaOrigen = true;
     $cuentaDestino = true;
     
     // Verificar si se proporcionó un ID de cuenta de origen
     if (isset($_GET['idCuentaOrigen']) && !empty($_GET['idCuentaOrigen'])) {
         $idCuentaOrigen = (int)$_GET['idCuentaOrigen'];
         
         // Obtener datos de la cuenta de origen
         $cuentaModel = new Cuenta($this->db);
         $cuentaModel->idCuenta = $idCuentaOrigen;
         
         if ($cuentaModel->obtenerUna()) {
             // Verificar si la cuenta está activa
             if ($cuentaModel->estado != 1) {
                 $this->session->setFlashMessage('error', $lang['inactive_account_error']);
                 header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuentaOrigen);
                 exit;
             }
             
             $cuentaOrigen = [
                 'idCuenta' => $cuentaModel->idCuenta,
                 'nroCuenta' => $cuentaModel->nroCuenta,
                 'tipoCuenta' => $cuentaModel->tipoCuenta,
                 'tipoMoneda' => $cuentaModel->tipoMoneda,
                 'saldo' => $cuentaModel->saldo,
                 'estado' => $cuentaModel->estado
             ];
             
             // Obtener datos del cliente
             $clienteModel = new Cliente($this->db);
             $clienteModel->idPersona = $cuentaModel->idPersona;
             $clienteModel->obtenerUno();
             
             // Añadir datos del cliente a la cuenta
             $cuentaOrigen['cliente_nombre'] = $clienteModel->nombre . ' ' . $clienteModel->apellidoPaterno . ' ' . $clienteModel->apellidoMaterno;
             $cuentaOrigen['cliente_ci'] = $clienteModel->ci;
             $cuentaOrigen['cliente_telefono'] = $clienteModel->telefono;
             $cuentaOrigen['cliente_email'] = $clienteModel->email;
         }
     }
     
     // Verificar si se proporcionó un ID de cuenta de destino
     if (isset($_GET['idCuentaDestino']) && !empty($_GET['idCuentaDestino'])) {
         $idCuentaDestino = (int)$_GET['idCuentaDestino'];
         
         // Obtener datos de la cuenta de destino
         $cuentaModel = new Cuenta($this->db);
         $cuentaModel->idCuenta = $idCuentaDestino;
         
         if ($cuentaModel->obtenerUna()) {
             // Verificar si la cuenta está activa
             if ($cuentaModel->estado != 1) {
                 $this->session->setFlashMessage('error', $lang['inactive_account_error']);
                 header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen);
                 exit;
             }
             
             $cuentaDestino = [
                 'idCuenta' => $cuentaModel->idCuenta,
                 'nroCuenta' => $cuentaModel->nroCuenta,
                 'tipoCuenta' => $cuentaModel->tipoCuenta,
                 'tipoMoneda' => $cuentaModel->tipoMoneda,
                 'estado' => $cuentaModel->estado
             ];
             
             // Obtener datos del cliente
             $clienteModel = new Cliente($this->db);
             $clienteModel->idPersona = $cuentaModel->idPersona;
             $clienteModel->obtenerUno();
             
             // Añadir datos del cliente a la cuenta
             $cuentaDestino['cliente_nombre'] = $clienteModel->nombre . ' ' . $clienteModel->apellidoPaterno . ' ' . $clienteModel->apellidoMaterno;
             $cuentaDestino['cliente_ci'] = $clienteModel->ci;
             $cuentaDestino['cliente_telefono'] = $clienteModel->telefono;
             $cuentaDestino['cliente_email'] = $clienteModel->email;
         }
     }
     
     // Definir título de la página
     $pageTitle = $lang['transfer_funds'];
     
     // Determinar el controlador actual para marcar el menú
     $controller = 'transaccion';
     
     // Definir vista a incluir
     $contentView = 'views/transacciones/transferir.php';
     
     // Mostrar plantilla principal
     include_once 'views/main.php';
 }
 
 // Método para buscar cuenta de origen
 public function buscarCuentaOrigen() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['nroCuentaOrigen']) || empty($_POST['nroCuentaOrigen'])) {
         $this->session->setFlashMessage('error', $lang['account_number_required']);
         header('Location: index.php?controller=transaccion&action=transferir');
         exit;
     }
     
     $nroCuentaOrigen = trim($_POST['nroCuentaOrigen']);
     
     // Buscar la cuenta por número
     $cuentaModel = new Cuenta($this->db);
     $resultado = $cuentaModel->obtenerPorNumeroCuenta($nroCuentaOrigen);
     
     if ($resultado) {
         $idCuentaOrigen = $cuentaModel->idCuenta;
         
         // Redirigir al formulario de transferencia con el ID de la cuenta de origen
         header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen);
         exit;
     } else {
         $this->session->setFlashMessage('error', $lang['account_not_found']);
         header('Location: index.php?controller=transaccion&action=transferir');
         exit;
     }
 }
 
 // Método para buscar cuenta de destino
 public function buscarCuentaDestino() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['idCuentaOrigen']) || empty($_POST['idCuentaOrigen']) || !isset($_POST['nroCuentaDestino']) || empty($_POST['nroCuentaDestino'])) {
         $this->session->setFlashMessage('error', $lang['missing_required_fields']);
         header('Location: index.php?controller=transaccion&action=transferir');
         exit;
     }
     
     $idCuentaOrigen = (int)$_POST['idCuentaOrigen'];
     $nroCuentaDestino = trim($_POST['nroCuentaDestino']);
     
     // Verificar que la cuenta de destino no sea la misma que la de origen
     $cuentaOrigen = new Cuenta($this->db);
     $cuentaOrigen->idCuenta = $idCuentaOrigen;
     $cuentaOrigen->obtenerUna();
     
     if ($cuentaOrigen->nroCuenta == $nroCuentaDestino) {
         $this->session->setFlashMessage('error', $lang['same_account_error']);
         header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen);
         exit;
     }
     
     // Buscar la cuenta de destino por número
     $cuentaDestino = new Cuenta($this->db);
     $resultado = $cuentaDestino->obtenerPorNumeroCuenta($nroCuentaDestino);
     
     if ($resultado) {
         $idCuentaDestino = $cuentaDestino->idCuenta;
         
         // Redirigir al formulario de transferencia con ambos IDs de cuenta
         header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen . '&idCuentaDestino=' . $idCuentaDestino);
         exit;
     } else {
         $this->session->setFlashMessage('error', $lang['destination_account_not_found']);
         header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen);
         exit;
     }
 }
 
 // Método para procesar la transferencia
 public function procesarTransferencia() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     if ($_SERVER['REQUEST_METHOD'] != 'POST' || !isset($_POST['idCuentaOrigen']) || empty($_POST['idCuentaOrigen']) || !isset($_POST['idCuentaDestino']) || empty($_POST['idCuentaDestino']) || !isset($_POST['monto']) || empty($_POST['monto'])) {
         $this->session->setFlashMessage('error', $lang['missing_required_fields']);
         header('Location: index.php?controller=transaccion&action=transferir');
         exit;
     }
     
     $idCuentaOrigen = (int)$_POST['idCuentaOrigen'];
     $idCuentaDestino = (int)$_POST['idCuentaDestino'];
     $monto = (float)$_POST['monto'];
     $descripcion = isset($_POST['descripcion']) ? trim($_POST['descripcion']) : $lang['transfer'];
     
     // Validar monto
     if ($monto <= 0) {
         $this->session->setFlashMessage('error', $lang['invalid_amount']);
         header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen . '&idCuentaDestino=' . $idCuentaDestino);
         exit;
     }
     
     try {
         // Realizar la transferencia usando el modelo actualizado
         $this->model->realizarTransferencia($idCuentaOrigen, $idCuentaDestino, $monto, $descripcion);
         
         $this->session->setFlashMessage('success', $lang['transfer_success']);
         header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuentaOrigen);
         exit;
     } catch (Exception $e) {
         $this->session->setFlashMessage('error', $e->getMessage());
         header('Location: index.php?controller=transaccion&action=transferir&idCuentaOrigen=' . $idCuentaOrigen . '&idCuentaDestino=' . $idCuentaDestino);
         exit;
     }
 }
 
 // Método para ver detalles de una transacción
 public function ver() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     // Verificar si se proporcionó un ID
     if (!isset($_GET['id']) || empty($_GET['id'])) {
         $this->session->setFlashMessage('error', $lang['transaction_id_not_specified']);
         header('Location: index.php?controller=transaccion&action=listar');
         exit;
     }
     
     $id = (int)$_GET['id'];
     
     // Obtener datos de la transacción
     $this->model->idTransaccion = $id;
     if (!$this->model->obtenerUna()) {
         $this->session->setFlashMessage('error', $lang['transaction_not_found']);
         header('Location: index.php?controller=transaccion&action=listar');
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
     
     // Definir título de la página
     $pageTitle = $lang['transaction_details'];
     
     // Determinar el controlador actual para marcar el menú
     $controller = 'transaccion';
     
     // Definir vista a incluir
     $contentView = 'views/transacciones/ver.php';
     
     // Mostrar plantilla principal
     include_once 'views/main.php';
 }
 
 // Método para buscar transacciones (filtrar por diferentes criterios)
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
     $tipoTransaccion = isset($_GET['tipoTransaccion']) ? (int)$_GET['tipoTransaccion'] : 0; // 0 = Todas
     $idCuenta = isset($_GET['idCuenta']) ? (int)$_GET['idCuenta'] : 0; // 0 = Todas
     
     // Obtener transacciones filtradas
     $transacciones = [];
     
     if ($idCuenta > 0) {
         // Buscar por cuenta específica
         if ($fechaInicio && $fechaFin) {
             // Con rango de fechas
             $resultado = $this->model->obtenerPorRangoFechas($idCuenta, $fechaInicio, $fechaFin);
         } else {
             // Sin rango de fechas
             $resultado = $this->model->obtenerPorCuenta($idCuenta);
         }
     } else {
         // Buscar en todas las cuentas
         if ($fechaInicio && $fechaFin) {
             // Con rango de fechas
             $resultado = $this->model->obtenerPorRangoFechasGlobal($fechaInicio, $fechaFin);
         } else {
             // Sin rango de fechas, mostrar todas las transacciones
             $resultado = $this->model->obtenerTodas();
         }
     }
     
     // Filtrar por tipo de transacción si es necesario
     if ($resultado && $tipoTransaccion > 0) {
         $transaccionesTodas = $resultado->fetchAll(PDO::FETCH_ASSOC);
         foreach ($transaccionesTodas as $transaccion) {
             if ($transaccion['tipoTransaccion'] == $tipoTransaccion) {
                 $transacciones[] = $transaccion;
             }
         }
     } else if ($resultado) {
         $transacciones = $resultado->fetchAll(PDO::FETCH_ASSOC);
     }
     
     // Obtener cuentas para el selector
     $cuentaModel = new Cuenta($this->db);
     $resultado = $cuentaModel->obtenerTodas();
     $cuentas = $resultado->fetchAll(PDO::FETCH_ASSOC);
     
     // Definir título de la página
     $pageTitle = $lang['search_transactions'];
     
     // Determinar el controlador actual para marcar el menú
     $controller = 'transaccion';
     
     // Definir vista a incluir
     $contentView = 'views/transacciones/buscar.php';
     
     // Mostrar plantilla principal
     include_once 'views/main.php';
 }
 
 // Método para generar comprobante de transacción
 public function comprobante() {
     global $lang;
     
     // Verificar si el usuario ha iniciado sesión
     if (!isset($_SESSION['user_id'])) {
         header('Location: index.php?controller=usuario&action=login');
         exit;
     }
     
     // Verificar si se proporcionó un ID
     if (!isset($_GET['id']) || empty($_GET['id'])) {
         $this->session->setFlashMessage('error', $lang['transaction_id_not_specified']);
         header('Location: index.php?controller=transaccion&action=listar');
         exit;
     }
     
     $id = (int)$_GET['id'];
     
     // Obtener datos de la transacción
     $this->model->idTransaccion = $id;
     if (!$this->model->obtenerUna()) {
         $this->session->setFlashMessage('error', $lang['transaction_not_found']);
         header('Location: index.php?controller=transaccion&action=listar');
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
     
     // Definir título de la página
     $pageTitle = $lang['transaction_receipt'];
     
     // Determinar el controlador actual para marcar el menú
     $controller ='transaccion';
      // Definir vista a incluir
      $contentView = 'views/transacciones/comprobante.php';
        
      // Mostrar plantilla principal
      include_once 'views/main.php';
  }
  
  /**
   * Mostrar movimientos de una cuenta con saldo inicial y final
   * @return void
   */
  public function movimientos() {
      global $lang;
      
      // Verificar si el usuario ha iniciado sesión
      if (!isset($_SESSION['user_id'])) {
          header('Location: index.php?controller=usuario&action=login');
          exit;
      }
      
      // Verificar si se proporcionó un ID de cuenta
      if (!isset($_GET['idCuenta']) || empty($_GET['idCuenta'])) {
          $this->session->setFlashMessage('error', $lang['account_id_not_specified']);
          header('Location: index.php?controller=cuenta&action=listar');
          exit;
      }
      
      $idCuenta = (int)$_GET['idCuenta'];
      
      // Obtener datos de la cuenta
      $cuenta = new Cuenta($this->db);
      $cuenta->idCuenta = $idCuenta;
      
      if (!$cuenta->obtenerUna()) {
          $this->session->setFlashMessage('error', $lang['account_not_found']);
          header('Location: index.php?controller=cuenta&action=listar');
          exit;
      }
      
      // Obtener datos del cliente
      $cliente = new Cliente($this->db);
      $cliente->idPersona = $cuenta->idPersona;
      $cliente->obtenerUno();
      
      // Obtener fechas para filtrar
      $fechaFin = isset($_GET['fechaFin']) ? $_GET['fechaFin'] : date('Y-m-d');
      $fechaInicio = isset($_GET['fechaInicio']) ? $_GET['fechaInicio'] : date('Y-m-01', strtotime($fechaFin)); // Primer día del mes
      
      // Calcular saldo inicial (saldo en la fecha de inicio)
      $saldoInicial = $this->calcularSaldoInicial($idCuenta, $fechaInicio);
      
      // Obtener movimientos en el rango de fechas
      $resultado = $this->model->obtenerPorRangoFechas($idCuenta, $fechaInicio, $fechaFin);
      $movimientos = $resultado->fetchAll(PDO::FETCH_ASSOC);
      
      // Calcular totales
      $totalIngresos = 0;
      $totalEgresos = 0;
      
      foreach ($movimientos as $movimiento) {
          if ($movimiento['tipoTransaccion'] == 2 || $movimiento['tipoTransaccion'] == 3) {
              // Depósitos y transferencias recibidas
              $totalIngresos += $movimiento['monto'];
          } else {
              // Retiros y transferencias enviadas
              $totalEgresos += $movimiento['monto'];
          }
      }
      
      // Calcular saldo final
      $saldoFinal = $saldoInicial + $totalIngresos - $totalEgresos;
      
      // Definir título de la página
      $pageTitle = $lang['account_movements'];
      
      // Determinar el controlador actual para marcar el menú
      $controller = 'transaccion';
      
      // Definir vista a incluir
      $contentView = 'views/transacciones/movimientos.php';
      
      // Mostrar plantilla principal
      include_once 'views/main.php';
  }
  
  /**
   * Calcular saldo inicial de una cuenta en una fecha específica
   * @param int $idCuenta ID de la cuenta
   * @param string $fecha Fecha para calcular el saldo inicial (YYYY-MM-DD)
   * @return float Saldo inicial
   */
  private function calcularSaldoInicial($idCuenta, $fecha) {
      // Obtener la cuenta
      $cuenta = new Cuenta($this->db);
      $cuenta->idCuenta = $idCuenta;
      $cuenta->obtenerUna();
      
      // Obtener todas las transacciones anteriores a la fecha
      $query = "SELECT 
                  SUM(CASE WHEN tipoTransaccion IN (2, 3) THEN monto ELSE 0 END) as total_ingresos,
                  SUM(CASE WHEN tipoTransaccion IN (1, 4) THEN monto ELSE 0 END) as total_egresos
                FROM Transaccion 
                WHERE idCuenta = :idCuenta AND fecha < :fecha";
      
      $stmt = $this->db->prepare($query);
      $stmt->bindParam(':idCuenta', $idCuenta);
      $stmt->bindParam(':fecha', $fecha);
      $stmt->execute();
      
      $resultado = $stmt->fetch(PDO::FETCH_ASSOC);
      
      // Calcular saldo inicial
      $totalIngresos = $resultado['total_ingresos'] ?: 0;
      $totalEgresos = $resultado['total_egresos'] ?: 0;
      
      // Si la cuenta se abrió después de la fecha de inicio, el saldo inicial debe ser 0
      if (strtotime($cuenta->fechaApertura) > strtotime($fecha)) {
          return 0;
      }
      
      // Calcular saldo inicial restando transacciones anteriores al saldo actual
      $saldoInicial = $cuenta->saldo - $totalIngresos + $totalEgresos;
      
      return $saldoInicial;
  }
  
  /**
   * Imprimir movimientos de cuenta
   * @return void
   */
  public function imprimirMovimientos() {
      global $lang;
      
      // Verificar si el usuario ha iniciado sesión
      if (!isset($_SESSION['user_id'])) {
          header('Location: index.php?controller=usuario&action=login');
          exit;
      }
      
      // Verificar si se proporcionaron los parámetros necesarios
      if (!isset($_GET['idCuenta']) || empty($_GET['idCuenta'])) {
          echo $lang['account_id_not_specified'];
          exit;
      }
      
      $idCuenta = (int)$_GET['idCuenta'];
      $fechaFin = isset($_GET['fechaFin']) ? $_GET['fechaFin'] : date('Y-m-d');
      $fechaInicio = isset($_GET['fechaInicio']) ? $_GET['fechaInicio'] : date('Y-m-01', strtotime($fechaFin));
      
      // Obtener datos de la cuenta
      $cuenta = new Cuenta($this->db);
      $cuenta->idCuenta = $idCuenta;
      
      if (!$cuenta->obtenerUna()) {
          echo $lang['account_not_found'];
          exit;
      }
      
      // Obtener datos del cliente
      $cliente = new Cliente($this->db);
      $cliente->idPersona = $cuenta->idPersona;
      $cliente->obtenerUno();
      
      // Calcular saldo inicial
      $saldoInicial = $this->calcularSaldoInicial($idCuenta, $fechaInicio);
      
      // Obtener movimientos
      $resultado = $this->model->obtenerPorRangoFechas($idCuenta, $fechaInicio, $fechaFin);
      $movimientos = $resultado->fetchAll(PDO::FETCH_ASSOC);
      
      // Calcular totales
      $totalIngresos = 0;
      $totalEgresos = 0;
      
      foreach ($movimientos as $movimiento) {
          if ($movimiento['tipoTransaccion'] == 2 || $movimiento['tipoTransaccion'] == 3) {
              // Depósitos y transferencias recibidas
              $totalIngresos += $movimiento['monto'];
          } else {
              // Retiros y transferencias enviadas
              $totalEgresos += $movimiento['monto'];
          }
      }
      
      // Calcular saldo final
      $saldoFinal = $saldoInicial + $totalIngresos - $totalEgresos;
      
      // Mostrar la vista para impresión
      include 'views/transacciones/imprimir_movimientos.php';
  }
}
     
        
?>
   