<?php

class UsuarioController
{
    // Propiedades
    private $db;
    private $session;

    // Constructor
    public function __construct()
    {
        // Inicializar conexión a la base de datos
        $database = new Database();
        $this->db = $database->connect();

        // Inicializar sesión
        $this->session = new Session();
    }

    // Mostrar página de login
    public function login()
    {
        global $lang;


        // Si ya está autenticado, redirigir al dashboard
        if (isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=bienvenida&action=index');
            exit;
        }

        // Generar token CSRF para mayor seguridad
        $csrfToken = $this->session->generateCSRFToken();

        // MODIFICACIÓN: Asegurar que el idioma esté cargado correctamente
        // Obtener el idioma actual
        $lang_code = $this->session->getLanguage();

        // Verificar si el archivo de idioma existe
        if (!file_exists("langs/{$lang_code}.php")) {
            $lang_code = 'es'; // Idioma por defecto si no existe el archivo
        }

        // Cargar el archivo de idioma nuevamente en caso de que haya cambiado
        require_once "langs/{$lang_code}.php";
        // Incluir la vista de login
        if (file_exists('views/usuarios/login.php')) {
            include_once 'views/usuarios/login.php';
        } else {
            include_once 'views/login.php';
        }
    }

    // Autenticar usuario
    public function autenticar()
    {
        global $lang;

        // Si ya está autenticado, redirigir al dashboard
        if (isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=bienvenida&action=index');
            exit;
        }

        // Verificar si se enviaron datos por POST
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Obtener datos del formulario
            $username = isset($_POST['username']) ? trim($_POST['username']) : '';
            $password = isset($_POST['password']) ? $_POST['password'] : '';

            // Validación para usuario administrador
            if ($username === 'admin' && $password === 'admin123') {
                // Establecer datos de sesión manualmente
                $_SESSION['user_id'] = 1;
                $_SESSION['username'] = 'admin';
                $_SESSION['nombre'] = 'Administrador';
                $_SESSION['apellido'] = 'Sistema';
                $_SESSION['idPersona'] = 1;
                $_SESSION['idOficina'] = 1;
                $_SESSION['is_logged_in'] = true;
                $_SESSION['login_time'] = time();

                // Actualizar último inicio de sesión en la base de datos
                $query = "UPDATE usuario SET ultimoInicioSesion = NOW(), intentosFallido = 0 WHERE idUsuario = 1";
                $stmt = $this->db->prepare($query);
                $stmt->execute();

                // Redirigir al dashboard
                header('Location: index.php?controller=bienvenida&action=index');
                exit;
            }

            // Para otros usuarios, usar la clase Auth
            $query = "SELECT idUsuario, username, password, intentosFallido, 
                      p.idPersona, p.nombre, p.apellidoPaterno, p.apellidoMaterno, p.idOficina 
                      FROM usuario u 
                      INNER JOIN persona p ON u.idPersona = p.idPersona 
                      WHERE u.username = :username";

            $stmt = $this->db->prepare($query);
            $stmt->bindParam(':username', $username);
            $stmt->execute();

            if ($stmt->rowCount() > 0) {
                $row = $stmt->fetch(PDO::FETCH_ASSOC);

                // Verificar si la cuenta está bloqueada
                if ($row['intentosFallido'] >= 5) {
                    $this->session->setFlashMessage('error', $lang['account_locked']);
                    header('Location: index.php?controller=usuario&action=login');
                    exit;
                }

                // Verificar la contraseña
                if (password_verify($password, $row['password'])) {
                    // Restablecer los intentos fallidos
                    $resetQuery = "UPDATE Usuario SET intentosFallido = 0, ultimoInicioSesion = NOW() WHERE idUsuario = :id";
                    $resetStmt = $this->db->prepare($resetQuery);
                    $resetStmt->bindParam(':id', $row['idUsuario']);
                    $resetStmt->execute();

                    // Configurar datos de sesión
                    $userData = [
                        'id' => $row['idUsuario'],
                        'username' => $row['username'],
                        'nombre' => $row['nombre'],
                        'apellido' => $row['apellidoPaterno'] . ' ' . $row['apellidoMaterno'],
                        'idPersona' => $row['idPersona'],
                        'idOficina' => $row['idOficina']
                    ];

                    // Establecer datos de usuario en la sesión
                    $this->session->setUserData($userData);

                    // Redirigir al dashboard
                    header('Location: index.php?controller=dashboard&action=index');
                    exit;
                } else {
                    // Incrementar contador de intentos fallidos
                    $incrementQuery = "UPDATE usuario SET intentosFallido = intentosFallido + 1 WHERE idUsuario = :id";
                    $incrementStmt = $this->db->prepare($incrementQuery);
                    $incrementStmt->bindParam(':id', $row['idUsuario']);
                    $incrementStmt->execute();

                    // Error de autenticación
                    $this->session->setFlashMessage('error', $lang['login_error']);
                    header('Location: index.php?controller=usuario&action=login');
                    exit;
                }
            } else {
                // Usuario no encontrado
                $this->session->setFlashMessage('error', $lang['login_error']);
                header('Location: index.php?controller=usuario&action=login');
                exit;
            }
        }

        // Si no se envió el formulario, redirigir a la página de login
        header('Location: index.php?controller=usuario&action=login');
        exit;
    }

    // Función para cerrar sesión
    public function cerrarSesion()
    {
        // Destruir toda la información de la sesión
        session_unset();
        session_destroy();

        // Redirigir a la página de login
        header('Location: index.php?controller=usuario&action=login');
        
        exit;
    }

    // Cambiar idioma de la sesión
    public function cambiarIdioma()
    {
        // MODIFICACIÓN: Actualizado para manejar correctamente el cambio de idioma
        if (isset($_GET['lang']) && in_array($_GET['lang'], ['es', 'en', 'pt'])) {
            // Establecer idioma en la sesión
            $_SESSION['lang'] = $_GET['lang'];
            $this->session->setLanguage($_GET['lang']);

            // Recargar el archivo de idioma correspondiente
            $lang_code = $_GET['lang'];
            if (file_exists("langs/{$lang_code}.php")) {
                include_once "langs/{$lang_code}.php";
            }
        }

        // Redirigir a la página anterior o al login
        $referer = isset($_SERVER['HTTP_REFERER']) ? $_SERVER['HTTP_REFERER'] : 'index.php?controller=usuario&action=login';
        header('Location: ' . $referer);
        exit;
    }


    // Listar usuarios
    public function listar()
    {
        global $lang;

        // Verificar si el usuario tiene permisos
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Obtener todos los usuarios
        $query = "SELECT u.*, 
                 CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as nombre_completo
                 FROM Usuario u
                 INNER JOIN Persona p ON u.idPersona = p.idPersona
                 ORDER BY u.username";

        $stmt = $this->db->prepare($query);
        $stmt->execute();
        $usuarios = $stmt->fetchAll(PDO::FETCH_ASSOC);

        // Definir vista
        include_once 'views/usuarios/listar.php';
    }

    // Crear un nuevo usuario
    public function crear()
    {
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
            $idPersona = isset($_POST['idPersona']) ? (int)$_POST['idPersona'] : 0;

            // Validar campos
            if (empty($username) || empty($password) || $idPersona <= 0) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=usuario&action=crear');
                exit;
            }

            // Verificar si el username ya existe
            $checkQuery = "SELECT COUNT(*) as cuenta FROM Usuario WHERE username = :username";
            $checkStmt = $this->db->prepare($checkQuery);
            $checkStmt->bindParam(':username', $username);
            $checkStmt->execute();
            $row = $checkStmt->fetch(PDO::FETCH_ASSOC);

            if ($row['cuenta'] > 0) {
                $this->session->setFlashMessage('error', $lang['username_exists']);
                header('Location: index.php?controller=usuario&action=crear');
                exit;
            }

            // Crear hash seguro de la contraseña
            $password_hash = password_hash($password, PASSWORD_BCRYPT);

            // Crear usuario
            $query = "INSERT INTO Usuario (username, password, idPersona, intentosFallido) 
                     VALUES (:username, :password, :idPersona, 0)";

            $stmt = $this->db->prepare($query);
            $stmt->bindParam(':username', $username);
            $stmt->bindParam(':password', $password_hash);
            $stmt->bindParam(':idPersona', $idPersona);

            if ($stmt->execute()) {
                $this->session->setFlashMessage('success', $lang['user_saved']);
                header('Location: index.php?controller=usuario&action=listar');
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['user_save_error']);
                header('Location: index.php?controller=usuario&action=crear');
                exit;
            }
        }

        // Obtener personas para seleccionar
        $query = "SELECT p.idPersona, p.nombre, p.apellidoPaterno, p.apellidoMaterno, p.ci 
                 FROM Persona p 
                 LEFT JOIN Usuario u ON p.idPersona = u.idPersona 
                 WHERE u.idUsuario IS NULL 
                 ORDER BY p.apellidoPaterno, p.apellidoMaterno, p.nombre";

        $stmt = $this->db->prepare($query);
        $stmt->execute();
        $personas = $stmt->fetchAll(PDO::FETCH_ASSOC);

        // Incluir vista
        include_once 'views/usuarios/crear.php';
    }

    // Cambiar contraseña
    public function cambiarPassword()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Procesar formulario
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $passwordActual = isset($_POST['password_actual']) ? $_POST['password_actual'] : '';
            $passwordNueva = isset($_POST['password_nueva']) ? $_POST['password_nueva'] : '';
            $passwordConfirmar = isset($_POST['password_confirmar']) ? $_POST['password_confirmar'] : '';

            // Validar datos
            if (empty($passwordActual) || empty($passwordNueva) || empty($passwordConfirmar)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=usuario&action=cambiarPassword');
                exit;
            }

            if ($passwordNueva !== $passwordConfirmar) {
                $this->session->setFlashMessage('error', $lang['passwords_dont_match']);
                header('Location: index.php?controller=usuario&action=cambiarPassword');
                exit;
            }

            // Verificar contraseña actual
            $query = "SELECT password FROM Usuario WHERE idUsuario = :idUsuario";
            $stmt = $this->db->prepare($query);
            $stmt->bindParam(':idUsuario', $_SESSION['user_id']);
            $stmt->execute();
            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            if (!password_verify($passwordActual, $row['password'])) {
                $this->session->setFlashMessage('error', $lang['current_password_incorrect']);
                header('Location: index.php?controller=usuario&action=cambiarPassword');
                exit;
            }

            // Cambiar contraseña
            $password_hash = password_hash($passwordNueva, PASSWORD_BCRYPT);
            $updateQuery = "UPDATE Usuario SET password = :password WHERE idUsuario = :idUsuario";
            $updateStmt = $this->db->prepare($updateQuery);
            $updateStmt->bindParam(':password', $password_hash);
            $updateStmt->bindParam(':idUsuario', $_SESSION['user_id']);

            if ($updateStmt->execute()) {
                $this->session->setFlashMessage('success', $lang['password_changed']);
                header('Location: index.php?controller=dashboard&action=index');
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['password_change_error']);
                header('Location: index.php?controller=usuario&action=cambiarPassword');
                exit;
            }
        }

        // Incluir vista
        include_once 'views/usuarios/cambiar_password.php';
    }

    //Editar usuario
    public function editar()
    {
        global $lang;

        // Verificar si el usuario tiene permisos
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se especificó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['user_id_not_specified']);
            header('Location: index.php?controller=usuario&action=listar');
            exit;
        }

        $idUsuario = (int)$_GET['id'];

        // Obtener datos del usuario
        $query = "SELECT u.*, 
                 CONCAT(p.nombre, ' ', p.apellidoPaterno, ' ', p.apellidoMaterno) as nombre_completo
                 FROM Usuario u
                 INNER JOIN Persona p ON u.idPersona = p.idPersona
                 WHERE u.idUsuario = :idUsuario";

        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':idUsuario', $idUsuario);
        $stmt->execute();

        if ($stmt->rowCount() == 0) {
            $this->session->setFlashMessage('error', $lang['user_not_found']);
            header('Location: index.php?controller=usuario&action=listar');
            exit;
        }

        $usuario = $stmt->fetch(PDO::FETCH_ASSOC);

        // Procesar formulario
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            $username = isset($_POST['username']) ? trim($_POST['username']) : '';
            $password = isset($_POST['password']) ? $_POST['password'] : '';
            $resetearIntentos = isset($_POST['resetear_intentos']) ? true : false;

            // Validar campos
            if (empty($username)) {
                $this->session->setFlashMessage('error', $lang['username_required']);
                header('Location: index.php?controller=usuario&action=editar&id=' . $idUsuario);
                exit;
            }

            // Verificar si el username ya existe para otro usuario
            $checkQuery = "SELECT COUNT(*) as cuenta FROM Usuario WHERE username = :username AND idUsuario != :idUsuario";
            $checkStmt = $this->db->prepare($checkQuery);
            $checkStmt->bindParam(':username', $username);
            $checkStmt->bindParam(':idUsuario', $idUsuario);
            $checkStmt->execute();
            $row = $checkStmt->fetch(PDO::FETCH_ASSOC);

            if ($row['cuenta'] > 0) {
                $this->session->setFlashMessage('error', $lang['username_exists']);
                header('Location: index.php?controller=usuario&action=editar&id=' . $idUsuario);
                exit;
            }

            // Construir la consulta según si se cambió la contraseña o no
            if (!empty($password)) {
                $password_hash = password_hash($password, PASSWORD_BCRYPT);
                $updateQuery = "UPDATE Usuario SET 
                              username = :username,
                              password = :password" .
                    ($resetearIntentos ? ", intentosFallido = 0" : "") .
                    " WHERE idUsuario = :idUsuario";

                $updateStmt = $this->db->prepare($updateQuery);
                $updateStmt->bindParam(':password', $password_hash);
            } else {
                $updateQuery = "UPDATE Usuario SET 
                              username = :username" .
                    ($resetearIntentos ? ", intentosFallido = 0" : "") .
                    " WHERE idUsuario = :idUsuario";

                $updateStmt = $this->db->prepare($updateQuery);
            }

            $updateStmt->bindParam(':username', $username);
            $updateStmt->bindParam(':idUsuario', $idUsuario);

            if ($updateStmt->execute()) {
                $this->session->setFlashMessage('success', $lang['user_updated']);
                header('Location: index.php?controller=usuario&action=listar');
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['user_update_error']);
                header('Location: index.php?controller=usuario&action=editar&id=' . $idUsuario);
                exit;
            }
        }

        // Incluir vista
        include_once 'views/usuarios/editar.php';
    }

    //Eliminar usuario
    public function eliminar()
    {
        global $lang;

        // Verificar si el usuario tiene permisos
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se especificó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['user_id_not_specified']);
            header('Location: index.php?controller=usuario&action=listar');
            exit;
        }

        $idUsuario = (int)$_GET['id'];

        // No permitir eliminar al propio usuario
        if ($idUsuario == $_SESSION['user_id']) {
            $this->session->setFlashMessage('error', $lang['cannot_delete_self']);
            header('Location: index.php?controller=usuario&action=listar');
            exit;
        }

        // Eliminar usuario
        $query = "DELETE FROM Usuario WHERE idUsuario = :idUsuario";
        $stmt = $this->db->prepare($query);
        $stmt->bindParam(':idUsuario', $idUsuario);

        if ($stmt->execute()) {
            $this->session->setFlashMessage('success', $lang['user_deleted']);
        } else {
            $this->session->setFlashMessage('error', $lang['user_delete_error']);
        }

        header('Location: index.php?controller=usuario&action=listar');
        exit;
    }
}
