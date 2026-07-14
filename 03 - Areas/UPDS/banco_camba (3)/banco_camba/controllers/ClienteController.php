<?php

class ClienteController {
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
        $this->model = new Cliente($this->db);
    }
    
    // Método para mostrar el formulario de registro de clientes
    public function crear() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Obtener las oficinas para el selector
        $oficina = new Oficina($this->db);
        $resultado = $oficina->obtenerTodas();
        $oficinas = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['new_client'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cliente';
        
        // Incluir la vista para crear clientes
        $contentView = 'views/clientes/crear.php';
        include_once 'views/main.php';
    }
    
    // Método para procesar el registro de clientes
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
            $nombre = isset($_POST['nombre']) ? trim($_POST['nombre']) : '';
            $apellidoPaterno = isset($_POST['apellidoPaterno']) ? trim($_POST['apellidoPaterno']) : '';
            $apellidoMaterno = isset($_POST['apellidoMaterno']) ? trim($_POST['apellidoMaterno']) : '';
            $direccion = isset($_POST['direccion']) ? trim($_POST['direccion']) : '';
            $telefono = isset($_POST['telefono']) ? trim($_POST['telefono']) : '';
            $email = isset($_POST['email']) ? trim($_POST['email']) : '';
            $fechaNacimiento = isset($_POST['fechaNacimiento']) ? trim($_POST['fechaNacimiento']) : '';
            $ci = isset($_POST['ci']) ? trim($_POST['ci']) : '';
            $idOficina = isset($_POST['idOficina']) ? (int)$_POST['idOficina'] : 0;
            
            // Validar datos
            if (empty($nombre) || empty($apellidoPaterno) || empty($ci) || empty($idOficina)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=cliente&action=crear');
                exit;
            }
            
            // Verificar si ya existe un cliente con el mismo CI
            $query = "SELECT COUNT(*) as cuenta FROM persona WHERE ci = :ci";
            $stmt = $this->db->prepare($query);
            $stmt->bindParam(':ci', $ci);
            $stmt->execute();
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
            
            if ($row['cuenta'] > 0) {
                $this->session->setFlashMessage('error', $lang['ci_already_exists']);
                header('Location: index.php?controller=cliente&action=crear');
                exit;
            }
            
            // Asignar datos al modelo
            $this->model->nombre = $nombre;
            $this->model->apellidoPaterno = $apellidoPaterno;
            $this->model->apellidoMaterno = $apellidoMaterno;
            $this->model->direccion = $direccion;
            $this->model->telefono = $telefono;
            $this->model->email = $email;
            $this->model->fechaNacimiento = $fechaNacimiento;
            $this->model->ci = $ci;
            $this->model->idOficina = $idOficina;
            
            // Generar hash para seguridad
            $this->model->hash = bin2hex(random_bytes(16)); // Genera un hash único
            
            // Guardar cliente
            if ($this->model->crear()) {
                $this->session->setFlashMessage('success', $lang['client_saved']);
                header('Location: index.php?controller=cliente&action=listar');
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['client_save_error']);
                header('Location: index.php?controller=cliente&action=crear');
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al formulario
            header('Location: index.php?controller=cliente&action=crear');
            exit;
        }
    }
    
    // Método para listar clientes
    public function listar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Obtener clientes
        $resultado = $this->model->obtenerTodos();
        $clientes = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['client_list'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cliente';
        
        // Incluir la vista de listado
        $contentView = 'views/clientes/listar.php';
        include_once 'views/main.php';
    }
    
    // Método para mostrar detalles de un cliente
    public function ver() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['client_id_not_specified']);
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos del cliente
        $this->model->idPersona = $id;
        if (!$this->model->obtenerUno()) {
            $this->session->setFlashMessage('error', $lang['client_not_found']);
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
        
        // Obtener cuentas del cliente
        $cuenta = new Cuenta($this->db);
        $resultado = $cuenta->obtenerPorCliente($id);
        $cuentas = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['client_details'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cliente';
        
        // Incluir la vista de detalles
        $contentView = 'views/clientes/ver.php';
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
            $this->session->setFlashMessage('error', $lang['client_id_not_specified']);
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos del cliente
        $this->model->idPersona = $id;
        if (!$this->model->obtenerUno()) {
            $this->session->setFlashMessage('error', $lang['client_not_found']);
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
        
        // Obtener oficinas para el selector
        $oficina = new Oficina($this->db);
        $resultado = $oficina->obtenerTodas();
        $oficinas = $resultado->fetchAll(PDO::FETCH_ASSOC);
        
        // Definir el título de la página
        $pageTitle = $lang['edit_client'];
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'cliente';
        
        // Incluir la vista de edición
        $contentView = 'views/clientes/editar.php';
        include_once 'views/main.php';
    }
    
    // Método para procesar la actualización
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
            $idPersona = isset($_POST['idPersona']) ? (int)$_POST['idPersona'] : 0;
            $nombre = isset($_POST['nombre']) ? trim($_POST['nombre']) : '';
            $apellidoPaterno = isset($_POST['apellidoPaterno']) ? trim($_POST['apellidoPaterno']) : '';
            $apellidoMaterno = isset($_POST['apellidoMaterno']) ? trim($_POST['apellidoMaterno']) : '';
            $direccion = isset($_POST['direccion']) ? trim($_POST['direccion']) : '';
            $telefono = isset($_POST['telefono']) ? trim($_POST['telefono']) : '';
            $email = isset($_POST['email']) ? trim($_POST['email']) : '';
            $fechaNacimiento = isset($_POST['fechaNacimiento']) ? trim($_POST['fechaNacimiento']) : '';
            $ci = isset($_POST['ci']) ? trim($_POST['ci']) : '';
            $idOficina = isset($_POST['idOficina']) ? (int)$_POST['idOficina'] : 0;
            
            // Validar datos
            if (empty($idPersona) || empty($nombre) || empty($apellidoPaterno) || empty($ci) || empty($idOficina)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=cliente&action=editar&id=' . $idPersona);
                exit;
            }
            
            // Verificar si ya existe otro cliente con el mismo CI
            $query = "SELECT COUNT(*) as cuenta FROM Persona WHERE ci = :ci AND idPersona != :idPersona";
            $stmt = $this->db->prepare($query);
            $stmt->bindParam(':ci', $ci);
            $stmt->bindParam(':idPersona', $idPersona);
            $stmt->execute();
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
            
            if ($row['cuenta'] > 0) {
                $this->session->setFlashMessage('error', $lang['ci_already_exists']);
                header('Location: index.php?controller=cliente&action=editar&id=' . $idPersona);
                exit;
            }
            
            // Asignar datos al modelo
            $this->model->idPersona = $idPersona;
            $this->model->nombre = $nombre;
            $this->model->apellidoPaterno = $apellidoPaterno;
            $this->model->apellidoMaterno = $apellidoMaterno;
            $this->model->direccion = $direccion;
            $this->model->telefono = $telefono;
            $this->model->email = $email;
            $this->model->fechaNacimiento = $fechaNacimiento;
            $this->model->ci = $ci;
            $this->model->idOficina = $idOficina;
            
            // Actualizar cliente
            if ($this->model->actualizar()) {
                $this->session->setFlashMessage('success', $lang['client_updated']);
                header('Location: index.php?controller=cliente&action=ver&id=' . $idPersona);
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['client_update_error']);
                header('Location: index.php?controller=cliente&action=editar&id=' . $idPersona);
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al listado
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
    }
    
    // Método para eliminar cliente
    public function eliminar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['client_id_not_specified']);
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Verificar si el cliente existe
        $this->model->idPersona = $id;
        if (!$this->model->obtenerUno()) {
            $this->session->setFlashMessage('error', $lang['client_not_found']);
            header('Location: index.php?controller=cliente&action=listar');
            exit;
        }
        
        // Eliminar cliente
        if ($this->model->eliminar()) {
            $this->session->setFlashMessage('success', $lang['client_deleted']);
        } else {
            $this->session->setFlashMessage('error', $lang['client_delete_error']);
        }
        
        header('Location: index.php?controller=cliente&action=listar');
        exit;
    }
}
?>