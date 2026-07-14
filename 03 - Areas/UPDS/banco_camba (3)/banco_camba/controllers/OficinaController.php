<?php

class OficinaController {
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
        $this->model = new Oficina($this->db);
    }
    
    // Método para mostrar todas las oficinas
    public function listar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Inicializar la variable oficinas como un array vacío
        $oficinas = [];
        
        try {
            // Obtener todas las oficinas
            $resultado = $this->model->obtenerTodas();
            
            if ($resultado && $resultado->rowCount() > 0) {
                $oficinas = $resultado->fetchAll(PDO::FETCH_ASSOC);
                
                // Depuración: Imprimir los valores del primer registro si existe
                if (count($oficinas) > 0) {
                    error_log("Primera oficina recuperada: " . json_encode($oficinas[0]));
                }
                
                error_log("Encontradas " . count($oficinas) . " oficinas");
            } else {
                error_log("No se encontraron oficinas o hubo un error en la consulta");
            }
        } catch (Exception $e) {
            // Registrar cualquier excepción
            error_log("Excepción al obtener oficinas: " . $e->getMessage());
        }
        
        // Definir el título de la página
        $pageTitle = $lang['branch_list'] ?? 'Lista de Oficinas';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'oficina';
        
        // Incluir la vista
        $contentView = 'views/oficina/listar.php';
        include_once 'views/main.php';
    }
    
    // Método para mostrar el formulario de creación de oficina
    public function crear() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Definir el título de la página
        $pageTitle = $lang['new_branch'] ?? 'Nueva Oficina';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'oficina';
        
        // Incluir la vista
        $contentView = 'views/oficina/crear.php';
        include_once 'views/main.php';
    }
    
    // Método para guardar una nueva oficina
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
            $central = isset($_POST['central']) ? (int)$_POST['central'] : 0;
            $nombre = isset($_POST['nombre']) ? trim($_POST['nombre']) : '';
            $direccion = isset($_POST['direccion']) ? trim($_POST['direccion']) : '';
            $telefono = isset($_POST['telefono']) ? trim($_POST['telefono']) : '';
            $ciudad = isset($_POST['ciudad']) ? trim($_POST['ciudad']) : 'Santa Cruz de la Sierra';
            $departamento = isset($_POST['departamento']) ? trim($_POST['departamento']) : 'Santa Cruz';
            $pais = isset($_POST['pais']) ? trim($_POST['pais']) : 'Bolivia';
            $horarioAtencion = isset($_POST['horarioAtencion']) ? trim($_POST['horarioAtencion']) : '';
            $gerenteEncargado = isset($_POST['gerenteEncargado']) ? trim($_POST['gerenteEncargado']) : '';
            $fechaInauguracion = isset($_POST['fechaInauguracion']) ? trim($_POST['fechaInauguracion']) : null;
            $estado = isset($_POST['estado']) ? $_POST['estado'] : 'activa';
            
            // Validar datos
            if (empty($nombre) || empty($direccion)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required'] ?? 'Todos los campos son requeridos');
                header('Location: index.php?controller=oficina&action=crear');
                exit;
            }
            
            // Asignar datos al modelo
            $this->model->central = $central;
            $this->model->nombre = $nombre;
            $this->model->direccion = $direccion;
            $this->model->telefono = $telefono;
            $this->model->ciudad = $ciudad;
            $this->model->departamento = $departamento;
            $this->model->pais = $pais;
            $this->model->horarioAtencion = $horarioAtencion;
            $this->model->gerenteEncargado = $gerenteEncargado;
            $this->model->fechaInauguracion = $fechaInauguracion;
            $this->model->estado = $estado;
            
            // Generar hash único
            $this->model->hash = bin2hex(random_bytes(16));
            
            // Guardar oficina
            if ($this->model->crear()) {
                $this->session->setFlashMessage('success', $lang['branch_saved'] ?? 'Oficina guardada con éxito');
                header('Location: index.php?controller=oficina&action=ver&id=' . $this->model->idOficina);
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['branch_save_error'] ?? 'Error al guardar la oficina');
                header('Location: index.php?controller=oficina&action=crear');
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al formulario
            header('Location: index.php?controller=oficina&action=crear');
            exit;
        }
    }
    
    // Método para ver detalles de una oficina
    public function ver() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['branch_id_not_specified'] ?? 'ID de oficina no especificado');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la oficina
        $this->model->idOficina = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['branch_not_found'] ?? 'Oficina no encontrada');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        // Contar ATMs asociados a esta oficina
        $totalATMs = $this->model->contarATMs();
        
        // Contar personas asociadas a esta oficina
        $totalPersonas = $this->model->contarPersonas();
        
        // Definir el título de la página
        $pageTitle = $lang['branch_details'] ?? 'Detalles de Oficina';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'oficina';
        
        // Incluir la vista
        $contentView = 'views/oficina/ver.php';
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
            $this->session->setFlashMessage('error', $lang['branch_id_not_specified'] ?? 'ID de oficina no especificado');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la oficina
        $this->model->idOficina = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['branch_not_found'] ?? 'Oficina no encontrada');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        // Definir el título de la página
        $pageTitle = $lang['edit_branch'] ?? 'Editar Oficina';
        
        // Determinar el controlador actual para marcar el menú
        $controller = 'oficina';
        
        // Incluir la vista
        $contentView = 'views/oficina/editar.php';
        include_once 'views/main.php';
    }
    
    // Método para actualizar una oficina
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
            $idOficina = isset($_POST['idOficina']) ? (int)$_POST['idOficina'] : 0;
            $central = isset($_POST['central']) ? (int)$_POST['central'] : 0;
            $nombre = isset($_POST['nombre']) ? trim($_POST['nombre']) : '';
            $direccion = isset($_POST['direccion']) ? trim($_POST['direccion']) : '';
            $telefono = isset($_POST['telefono']) ? trim($_POST['telefono']) : '';
            $ciudad = isset($_POST['ciudad']) ? trim($_POST['ciudad']) : 'Santa Cruz de la Sierra';
            $departamento = isset($_POST['departamento']) ? trim($_POST['departamento']) : 'Santa Cruz';
            $pais = isset($_POST['pais']) ? trim($_POST['pais']) : 'Bolivia';
            $horarioAtencion = isset($_POST['horarioAtencion']) ? trim($_POST['horarioAtencion']) : '';
            $gerenteEncargado = isset($_POST['gerenteEncargado']) ? trim($_POST['gerenteEncargado']) : '';
            $fechaInauguracion = isset($_POST['fechaInauguracion']) ? trim($_POST['fechaInauguracion']) : null;
            $estado = isset($_POST['estado']) ? $_POST['estado'] : 'activa';
            
            // Validar datos
            if ($idOficina <= 0 || empty($nombre) || empty($direccion)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required'] ?? 'Todos los campos son requeridos');
                header('Location: index.php?controller=oficina&action=editar&id=' . $idOficina);
                exit;
            }
            
            // Obtener datos actuales de la oficina
            $this->model->idOficina = $idOficina;
            if (!$this->model->obtenerUna()) {
                $this->session->setFlashMessage('error', $lang['branch_not_found'] ?? 'Oficina no encontrada');
                header('Location: index.php?controller=oficina&action=listar');
                exit;
            }
            
            // Asignar nuevos datos al modelo
            $this->model->central = $central;
            $this->model->nombre = $nombre;
            $this->model->direccion = $direccion;
            $this->model->telefono = $telefono;
            $this->model->ciudad = $ciudad;
            $this->model->departamento = $departamento;
            $this->model->pais = $pais;
            $this->model->horarioAtencion = $horarioAtencion;
            $this->model->gerenteEncargado = $gerenteEncargado;
            $this->model->fechaInauguracion = $fechaInauguracion;
            $this->model->estado = $estado;
            
            // Actualizar oficina
            if ($this->model->actualizar()) {
                $this->session->setFlashMessage('success', $lang['branch_updated'] ?? 'Oficina actualizada con éxito');
                header('Location: index.php?controller=oficina&action=ver&id=' . $idOficina);
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['branch_update_error'] ?? 'Error al actualizar la oficina');
                header('Location: index.php?controller=oficina&action=editar&id=' . $idOficina);
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al listado
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
    }
    
    // Método para desactivar una oficina
    public function desactivar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['branch_id_not_specified'] ?? 'ID de oficina no especificado');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la oficina
        $this->model->idOficina = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['branch_not_found'] ?? 'Oficina no encontrada');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        // Verificar que la oficina no esté ya inactiva
        if ($this->model->estado == 'inactiva') {
            $this->session->setFlashMessage('error', $lang['branch_already_inactive'] ?? 'Oficina ya está inactiva');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        }
        
        // Desactivar oficina
        if ($this->model->desactivar()) {
            $this->session->setFlashMessage('success', $lang['branch_deactivated'] ?? 'Oficina desactivada con éxito');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['branch_deactivation_error'] ?? 'Error al desactivar la oficina');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        }
    }
    
    // Método para activar una oficina
    public function activar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['branch_id_not_specified'] ?? 'ID de oficina no especificado');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la oficina
        $this->model->idOficina = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['branch_not_found'] ?? 'Oficina no encontrada');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        // Verificar que la oficina no esté ya activa
        if ($this->model->estado == 'activa') {
            $this->session->setFlashMessage('error', $lang['branch_already_active'] ?? 'Oficina ya está activa');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        }
        
        // Activar oficina
        if ($this->model->activar()) {
            $this->session->setFlashMessage('success', $lang['branch_activated'] ?? 'Oficina activada con éxito');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['branch_activation_error'] ?? 'Error al activar la oficina');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        }
    }
    
    // Método para eliminar definitivamente una oficina
    public function eliminar() {
        global $lang;
        
        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        
        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['branch_id_not_specified'] ?? 'ID de oficina no especificado');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        $id = (int)$_GET['id'];
        
        // Obtener datos de la oficina
        $this->model->idOficina = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['branch_not_found'] ?? 'Oficina no encontrada');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        }
        
        // Contar ATMs asociados a esta oficina
        $totalATMs = $this->model->contarATMs();
        
        // Contar personas asociadas a esta oficina
        $totalPersonas = $this->model->contarPersonas();
        
        // Verificar que no haya dependencias
        if ($totalATMs > 0 || $totalPersonas > 0) {
            $this->session->setFlashMessage('error', $lang['branch_has_dependencies'] ?? 'No se puede eliminar la oficina porque tiene ATMs o personas asociadas');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        }
        
        // Eliminar oficina
        if ($this->model->eliminar()) {
            $this->session->setFlashMessage('success', $lang['branch_deleted'] ?? 'Oficina eliminada con éxito');
            header('Location: index.php?controller=oficina&action=listar');
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['branch_deletion_error'] ?? 'Error al eliminar la oficina');
            header('Location: index.php?controller=oficina&action=ver&id=' . $id);
            exit;
        }
    }
}
?>