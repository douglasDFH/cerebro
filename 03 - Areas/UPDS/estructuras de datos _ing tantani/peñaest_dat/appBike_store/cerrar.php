<?php
// Iniciar sesión para poder destruirla
session_start();

// Verificar si hay una sesión activa
if (isset($_SESSION['usuario_id'])) {
    // Guardar información para el log (opcional)
    $usuario_nombre = isset($_SESSION['usuario_nombre']) ? $_SESSION['usuario_nombre'] : 'Desconocido';
    $tiempo_sesion = isset($_SESSION['login_time']) ? (time() - $_SESSION['login_time']) : 0;
    
    // Opcional: Log de cierre de sesión (si quieres implementar un sistema de logs)
    
    try {
        include("bd.php");
        $stmt = $conexion->prepare("INSERT INTO logs_sesion (usuario, accion, tiempo_sesion, fecha) VALUES (?, 'logout', ?, NOW())");
        $stmt->execute([$usuario_nombre, $tiempo_sesion]);
    } catch (Exception $e) {
        // Silenciosamente ignorar errores de log
    }
    
}

// Destruir todas las variables de sesión
$_SESSION = array();

// Si se está usando cookies de sesión, eliminar también la cookie
if (ini_get("session.use_cookies")) {
    $params = session_get_cookie_params();
    setcookie(session_name(), '', time() - 42000,
        $params["path"], $params["domain"],
        $params["secure"], $params["httponly"]
    );
}

// Destruir la sesión completamente
session_destroy();

// Opcional: Limpiar cualquier cookie adicional que hayas establecido
if (isset($_COOKIE['remember_user'])) {
    setcookie('remember_user', '', time() - 3600, '/');
}

// Prevenir caché de la página
header("Cache-Control: no-cache, no-store, must-revalidate");
header("Pragma: no-cache");
header("Expires: 0");

// Redirigir al login con mensaje de confirmación
header("Location: login.php?logout=1");
exit();
?>