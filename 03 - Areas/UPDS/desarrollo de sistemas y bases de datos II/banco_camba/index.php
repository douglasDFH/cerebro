<?php
/**
 * 
 * Banco Camba - Punto de entrada principal
 */
// Al inicio del archivo index.php
// 02/03/2025: Configurado para ocultar errores en producción
error_reporting(0);
//ini_set('display_errors', 0);
// Iniciar sesión
session_start();

// Incluir archivos de configuración
require_once 'config/database.php';
require_once 'utils/Session.php';
require_once 'utils/Auth.php';
require_once 'utils/Autoloader.php';
////require_once 'langs/en.php';
//require_once 'langs/es.php';

// Cargar modelos
require_once 'models/Oficina.php';
require_once 'models/Cliente.php';
require_once 'models/Cuenta.php';
require_once 'models/Transaccion.php';
require_once 'models/ATM.php';
require_once 'models/Tarjeta.php';
require_once 'models/Usuario.php';

// Cargar el idioma seleccionado
$session = new Session();
$lang_code = $session->getLanguage();
require_once "langs/{$lang_code}.php";

// Determinar qué controlador y acción usar
$controller = isset($_GET['controller']) ? $_GET['controller'] : 'usuario';
$action = isset($_GET['action']) ? $_GET['action'] : 'login';

// Verificar si el usuario ha iniciado sesión para acciones protegidas
$publicActions = ['login', 'autenticar', 'cambiarIdioma'];

if (!isset($_SESSION['user_id']) && !in_array($action, $publicActions) && $controller != 'api') {
    // Redirigir al login si no ha iniciado sesión y está intentando acceder a páginas protegidas
    header('Location: index.php?controller=usuario&action=login');
    exit;
}

// Cargar el controlador apropiado
$controllerName = ucfirst($controller) . 'Controller';
$controllerFile = "controllers/{$controllerName}.php";

// Verificar si el archivo del controlador existe
if (file_exists($controllerFile)) {
    require_once $controllerFile;
    
    // Crear instancia del controlador
    $controllerInstance = new $controllerName();
    
    // Verificar si el método de acción existe
    if (method_exists($controllerInstance, $action)) {
        // Ejecutar la acción
        $controllerInstance->$action();
    } else {
        // Acción no encontrada
        include 'views/error.php';
    }
} else {
    // Controlador no encontrado
    include 'views/error.php';
}
?>