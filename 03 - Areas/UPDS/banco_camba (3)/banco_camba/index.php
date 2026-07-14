<?php
function dd(...$paran)
{
    echo "<pre>";
    var_dump(...$paran);
    echo "</pre>";
    die();
}

// Función para verificar si el usuario está autenticado correctamente
function isAuthenticated() {
    // Verificar que existan todas las variables de sesión necesarias
    if (!isset($_SESSION['user_id']) || 
        !isset($_SESSION['username']) || 
        !isset($_SESSION['is_logged_in']) || 
        !isset($_SESSION['login_time'])) {
        return false;
    }
    
    // Verificar que is_logged_in sea true
    if ($_SESSION['is_logged_in'] !== true) {
        return false;
    }
    
    // Verificar tiempo de expiración de la sesión (opcional)
    // Por ejemplo, 2 horas = 7200 segundos
    $sessionLifetime = 7200; 
    if (time() - $_SESSION['login_time'] > $sessionLifetime) {
        // La sesión ha expirado, limpiar variables
        session_unset();
        session_destroy();
        return false;
    }
    
    // Todo verificado correctamente
    return true;
}

// Función para redireccionar de manera segura con ruta absoluta
function redirect($path) {
    $base_url = (isset($_SERVER['HTTPS']) && $_SERVER['HTTPS'] === 'on' ? "https" : "http");
    $base_url .= "://" . $_SERVER['HTTP_HOST'];
    $base_url .= str_replace(basename($_SERVER['SCRIPT_NAME']), "", $_SERVER['SCRIPT_NAME']);
    
    header("Location: " . rtrim($base_url, '/') . '/' . ltrim($path, '/'));
    exit;
}

/* ---------- BANCO CAMBA - PUNTO DE ENTRADA PRINCIPAL ---------- */

// Iniciar buffer de salida para evitar problemas con redirecciones
ob_start();

// Implementación de cabeceras de seguridad HTTP
header("Strict-Transport-Security: max-age=31536000; includeSubDomains; preload");
header("X-XSS-Protection: 1; mode=block");
header("X-Content-Type-Options: nosniff");
header("X-Frame-Options: SAMEORIGIN");
header("Content-Security-Policy: default-src 'self'; script-src 'self' https://cdn.jsdelivr.net; style-src 'self' 'unsafe-inline';");
header("Referrer-Policy: strict-origin-when-cross-origin");

// Configuración para ocultar errores en producción
error_reporting(0);

// Iniciar sesión
session_start();

// Incluir archivos de configuración y utilidades
require_once 'config/database.php';
require_once 'utils/Session.php';
require_once 'utils/Auth.php';
require_once 'utils/Autoloader.php';

// Cargar modelos
require_once 'models/TransaccionATM.php';
require_once 'models/Oficina.php';
require_once 'models/Cliente.php';
require_once 'models/Cuenta.php';
require_once 'models/Transaccion.php';
require_once 'models/ATM.php';
require_once 'models/Tarjeta.php';
require_once 'models/Usuario.php';
require_once 'models/Bienvenida.php';
require_once 'models/Token.php';

// MODIFICACIÓN: Asegurarse de que el idioma está cargado correctamente
// Inicializar la sesión
$session = new Session();

// Obtener el idioma actual, si viene un idioma por GET, actualizarlo
if (isset($_GET['lang']) && in_array($_GET['lang'], ['es', 'en', 'pt'])) {
    $session->setLanguage($_GET['lang']);
    $_SESSION['lang'] = $_GET['lang'];
}

// Obtener el código de idioma 
$lang_code = $session->getLanguage();

// Verificar que el archivo de idioma existe
if (!file_exists("langs/{$lang_code}.php")) {
    $lang_code = 'es'; // Idioma por defecto si no existe el archivo
}

// Cargar el archivo de idioma
require_once "langs/{$lang_code}.php";

// Determinar controlador y acción por defecto
$controller = isset($_GET['controller']) ? $_GET['controller'] : 'usuario';
$action = isset($_GET['action']) ? $_GET['action'] : 'login';

// Si el usuario ya está autenticado y trata de acceder a login, redirigir al dashboard
if (isAuthenticated() && $controller == 'usuario' && $action == 'login') {
    redirect('index.php?controller=bienvenida&action=index');
}

// Lista de acciones públicas que no requieren autenticación
$publicActions = ['login', 'autenticar', 'cambiarIdioma'];

// Verificar si el usuario ha iniciado sesión para acciones protegidas
if (!isAuthenticated() && !in_array($action, $publicActions) && $controller != 'api') {
    // Redirigir al login si no ha iniciado sesión y está intentando acceder a páginas protegidas
    redirect('index.php?controller=usuario&action=login');
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
        try {
            $controllerInstance->$action();
        } catch (Exception $e) {
            // Log de errores en archivo para producción
            error_log("Error en {$controllerName}::{$action}: " . $e->getMessage());
            
            // En entorno de desarrollo mostrar detalles completos
            if ($_SERVER['SERVER_NAME'] == 'localhost' || strpos($_SERVER['SERVER_NAME'], '192.168.') === 0) {
                echo "<pre>";
                var_dump($e);
                echo "</pre>";
                echo "<pre>";
                var_dump($e->getLine());
                echo "</pre>";
                echo "<pre>";
                var_dump($e->getMessage());
                echo "</pre>";
                die();
            } else {
                // En producción mostrar un mensaje de error genérico
                include 'views/error.php';
                die("Ha ocurrido un error inesperado. Por favor contacte al administrador.");
            }
        }
    } else {
        // Acción no encontrada
        include 'views/error.php';
        die("La acción '$action' no existe en el controlador '$controllerName'.");
    }
} else {
    // Controlador no encontrado
    include 'views/error.php';
    die("El controlador '$controllerName' no existe.");
}

// Cerrar buffer de salida
ob_end_flush();