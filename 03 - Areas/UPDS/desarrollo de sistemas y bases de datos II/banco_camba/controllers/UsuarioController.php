<?php
/**
 * Controlador de Usuario
 */
class UsuarioController {
    // Propiedades
    private $db;
    private $session;
    
    /**
     * Constructor
     */
    public function __construct() {
        // Inicializar conexión a la base de datos
        $database = new Database();
        $this->db = $database->connect();
        
        // Inicializar sesión
        $this->session = new Session();
    }
    
    /**
     * Mostrar página de login
     */
    public function login() {
        global $lang;
        
        // Si ya está autenticado, redirigir al dashboard
        if (isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=dashboard&action=index');
            exit;
        }
        
        // Incluir directamente la vista de login
        include_once 'views/usuarios/login.php';
    }
    
    /**
     * Autenticar usuario
     */
    public function autenticar() {
        global $lang;

        // Si ya está autenticado, redirigir al dashboard
        if (isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=dashboard&action=index');
            exit;
        }

        // Verificar si se enviaron datos por POST
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Obtener datos del formulario
            $username = isset($_POST['username']) ? trim($_POST['username']) : '';
            $password = isset($_POST['password']) ? $_POST['password'] : '';

            // Validación de usuario de prueba
            if ($username === 'admin' && $password === 'admin123') {
                // Establecer datos de sesión manualmente
                $_SESSION['user_id'] = 1;
                $_SESSION['username'] = 'admin';
                $_SESSION['nombre'] = 'Administrador';
                $_SESSION['apellido'] = 'Sistema';
                $_SESSION['idPersona'] = 1;
                $_SESSION['idOficina'] = 1;
                $_SESSION['is_logged_in'] = true;

                // Redirigir al dashboard
                header('Location: index.php?controller=dashboard&action=index');
                exit;
            }

            // Autenticar usando la clase Auth para otros usuarios
            $auth = new Auth();
            $userData = $auth->login($username, $password);

            if ($userData) {
                // Establecer datos de usuario en la sesión
                $this->session->setUserData($userData);

                // Redirigir al dashboard
                header('Location: index.php?controller=dashboard&action=index');
                exit;
            } else {
                // Error de autenticación
                $this->session->setFlashMessage('error', $lang['login_error']);
                header('Location: index.php?controller=usuario&action=login');
                exit;
            }
        }

        // Si no se envió el formulario, redirigir a la página de login
        header('Location: index.php?controller=usuario&action=login');
        exit;
    }

    /**
     * Cerrar sesión
     */
    public function cerrarSesion() {
        // Destruir toda la información de la sesión
        session_unset();
        session_destroy();
        
        // Redirigir a la página de login
        header('Location: index.php?controller=usuario&action=login');
        exit;
    }

    /**
     * Cambiar idioma
     * 02/03/2025: Verificado y corregido el método para asegurar que el cambio de idioma
     * funcione correctamente con la sesión
     */
    public function cambiarIdioma() {
        // Verificar si se especificó un idioma
        if (isset($_GET['lang']) && in_array($_GET['lang'], ['es', 'en'])) {
            // Depuración para ver qué está sucediendo
            error_log("Cambiando idioma a: " . $_GET['lang']);
            
            // Establecer idioma en la sesión
            $_SESSION['lang'] = $_GET['lang'];
            $this->session->setLanguage($_GET['lang']);
        }
        
        // Redirigir a la página anterior o al login
        $referer = isset($_SERVER['HTTP_REFERER']) ? $_SERVER['HTTP_REFERER'] : 'index.php?controller=usuario&action=login';
        header('Location: ' . $referer);
        exit;
    }

    /**
     * Listar usuarios
     */
    public function listar() {
        global $lang;

        // Verificar si el usuario tiene permisos
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Obtener todos los usuarios
        $usuario = new Usuario($this->db);
        $result = $usuario->obtenerTodos();
        $usuarios = $result->fetchAll(PDO::FETCH_ASSOC);

        // Definir vista
        include_once 'views/usuarios/listar.php';
    }

    /**
     * Crear usuario
     */
    public function crear() {
        global $lang;

        // Verificar si el usuario tiene permisos
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Procesar formulario
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Obtener datos del formulario
            $username = isset($_POST['username']) ? trim($_POST['username']) : '';
            $password = isset($_POST['password']) ? $_POST['password'] : '';

            // Validar campos
            if (empty($username) || empty($password)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=usuario&action=crear');
                exit;
            }

            // Crear usuario
            $usuario = new Usuario($this->db);
            $usuario->username = $username;
            $usuario->password = password_hash($password, PASSWORD_BCRYPT);

            if ($usuario->crear()) {
                $this->session->setFlashMessage('success', $lang['user_saved']);
                header('Location: index.php?controller=usuario&action=listar');
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['username_exists']);
                header('Location: index.php?controller=usuario&action=crear');
                exit;
            }
        }

        // Incluir vista
        include_once 'views/usuarios/crear.php';
    }

    /**
     * Cambiar contraseña
     */
    public function cambiarPassword() {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Procesar formulario
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $passwordActual = $_POST['password_actual'];
            $passwordNueva = $_POST['password_nueva'];
            $passwordConfirmar = $_POST['password_confirmar'];

            if ($passwordNueva !== $passwordConfirmar) {
                $this->session->setFlashMessage('error', $lang['passwords_dont_match']);
                header('Location: index.php?controller=usuario&action=cambiarPassword');
                exit;
            }

            // Obtener usuario
            $usuario = new Usuario($this->db);
            $usuario->idUsuario = $_SESSION['user_id'];
            $usuario->password = password_hash($passwordNueva, PASSWORD_BCRYPT);

            if ($usuario->actualizar()) {
                $this->session->setFlashMessage('success', $lang['password_changed']);
                header('Location: index.php?controller=dashboard&action=index');
                exit;
            }
        }

        // Incluir vista
        include_once 'views/usuarios/cambiar_password.php';
    }
}
?>