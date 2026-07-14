<?php

class TarjetaController
{
    // Propiedades
    private $db;
    private $session;
    private $model;

    // Constructor
    public function __construct()
    {
        // Inicializar conexión a la base de datos
        $database = new Database();
        $this->db = $database->connect();

        // Inicializar sesión
        $this->session = new Session();

        // Inicializar modelo
        $this->model = new Tarjeta($this->db);
    }

    // Método para listar tarjetas
    public function listar()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Obtener tarjetas
        $resultado = $this->model->obtenerTodas();
        $tarjetas = $resultado->fetchAll(PDO::FETCH_ASSOC);

        // Definir el título de la página
        $pageTitle = $lang['card_list'];

        // Determinar el controlador actual para marcar el menú
        $controller = 'tarjeta';

        // Incluir la vista de listado
        $contentView = 'views/tarjetas/listar.php';
        include_once 'views/main.php';
    }

    // Método para mostrar el formulario de registro de tarjetas
    public function crear()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se proporciona un ID de cuenta
        $idCuenta = isset($_GET['idCuenta']) ? (int)$_GET['idCuenta'] : 0;

        // Si se proporciona un ID de cuenta, obtener datos de la cuenta
        $datosCliente = [];
        if ($idCuenta > 0) {
            $cuenta = new Cuenta($this->db);
            $cuenta->idCuenta = $idCuenta;
            if ($cuenta->obtenerUna()) {
                // Obtener datos del cliente propietario de la cuenta
                $cliente = new Cliente($this->db);
                $cliente->idPersona = $cuenta->idPersona;
                if ($cliente->obtenerUno()) {
                    $datosCliente = [
                        'idPersona' => $cliente->idPersona,
                        'nombre' => $cliente->nombre,
                        'apellidoPaterno' => $cliente->apellidoPaterno,
                        'apellidoMaterno' => $cliente->apellidoMaterno,
                        'idCuenta' => $idCuenta,
                        'nroCuenta' => $cuenta->nroCuenta,
                        'tipoMoneda' => $cuenta->tipoMoneda
                    ];
                }
            }
        }

        // Si no se proporciona un ID de cuenta o no se encuentra, obtener todas las cuentas para el selector
        if (empty($datosCliente)) {
            $cuenta = new Cuenta($this->db);
            $resultado = $cuenta->obtenerTodas();
            $cuentas = $resultado->fetchAll(PDO::FETCH_ASSOC);
        } else {
            $cuentas = [];
        }

        // Generar número de tarjeta aleatorio
        $numeroTarjeta = $this->model->generarNumeroTarjeta();
        $cvv = $this->model->generarCVV();
        $fechaExpiracion = $this->model->generarFechaExpiracion();

        // Definir el título de la página
        $pageTitle = $lang['new_card'];

        // Determinar el controlador actual para marcar el menú
        $controller = 'tarjeta';
        // Incluir la vista para crear tarjetas
        $contentView = 'views/tarjetas/crear.php';
        include_once 'views/main.php';
    }

    // Método para procesar el registro de tarjetas
    public function guardar()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }
        // Verificar si se enviaron datos por POST
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Obtener datos del formulario
            $tipoTarjeta = isset($_POST['tipoTarjeta']) ? trim($_POST['tipoTarjeta']) : '';
            $nroTarjeta = isset($_POST['nroTarjeta']) ? trim($_POST['nroTarjeta']) : '';
            $cvv = isset($_POST['cvv']) ? trim($_POST['cvv']) : '';
            $fechaExpiracion = isset($_POST['fechaExpiracion']) ? trim($_POST['fechaExpiracion']) : '';
            $pin = isset($_POST['pin']) ? trim($_POST['pin']) : '';
            $idCuenta = isset($_POST['idCuenta']) ? (int)$_POST['idCuenta'] : 0;
            $estado = isset($_POST['estado']) ? trim($_POST['estado']) : 'activa';
            // Validar datos
            if (empty($tipoTarjeta) || empty($nroTarjeta) || empty($cvv) || empty($fechaExpiracion) || empty($pin) || empty($idCuenta)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=tarjeta&action=crear');
                exit;
            }

            // Verificar si ya existe una tarjeta con el mismo número
            if ($this->model->existeNumeroTarjeta($nroTarjeta)) {
                $this->session->setFlashMessage('error', $lang['card_number_already_exists']);
                header('Location: index.php?controller=tarjeta&action=crear');
                exit;
            }

            // Asignar datos al modelo
            $this->model->tipoTarjeta = $tipoTarjeta;
            $this->model->nroTarjeta = $nroTarjeta;
            $this->model->cvv = $cvv;
            $this->model->fechaExpiracion = $fechaExpiracion;
            $this->model->pin = $pin;
            $this->model->idCuenta = $idCuenta;
            $this->model->estado = $estado;

            // Generar hash para seguridad
            $this->model->hash = bin2hex(random_bytes(16)); // Genera un hash único

            // Guardar tarjeta
            if ($this->model->crear()) {
                $this->session->setFlashMessage('success', $lang['card_saved']);

                // Redirigir a la vista de la cuenta asociada
                header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['card_save_error']);
                header('Location: index.php?controller=tarjeta&action=crear');
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al formulario
            header('Location: index.php?controller=tarjeta&action=crear');
            exit;
        }
    }

    // Método para mostrar detalles de una tarjeta
    public function ver()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['card_id_not_specified']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        $id = (int)$_GET['id'];

        // Obtener datos de la tarjeta
        $this->model->idTarjeta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['card_not_found']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        // Obtener información de la cuenta asociada
        $cuenta = new Cuenta($this->db);
        $cuenta->idCuenta = $this->model->idCuenta;
        $cuenta->obtenerUna();

        // Definir el título de la página
        $pageTitle = $lang['card_details'];

        // Determinar el controlador actual para marcar el menú
        $controller = 'tarjeta';

        // Incluir la vista de detalles
        $contentView = 'views/tarjetas/ver.php';
        include_once 'views/main.php';
    }

    // Método para mostrar el formulario de edición
    public function editar()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['card_id_not_specified']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        $id = (int)$_GET['id'];

        // Obtener datos de la tarjeta
        $this->model->idTarjeta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['card_not_found']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        // Obtener todas las cuentas para el selector
        $cuenta = new Cuenta($this->db);
        $resultado = $cuenta->obtenerTodas();
        $cuentas = $resultado->fetchAll(PDO::FETCH_ASSOC);

        // Definir el título de la página
        $pageTitle = $lang['edit_card'];

        // Determinar el controlador actual para marcar el menú
        $controller = 'tarjeta';

        // Incluir la vista de edición
        $contentView = 'views/tarjetas/editar.php';
        include_once 'views/main.php';
    }

    // Método para procesar la actualización
    public function actualizar()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se enviaron datos por POST
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
            // Obtener datos del formulario
            $idTarjeta = isset($_POST['idTarjeta']) ? (int)$_POST['idTarjeta'] : 0;
            $tipoTarjeta = isset($_POST['tipoTarjeta']) ? trim($_POST['tipoTarjeta']) : '';
            $nroTarjeta = isset($_POST['nroTarjeta']) ? trim($_POST['nroTarjeta']) : '';
            $cvv = isset($_POST['cvv']) ? trim($_POST['cvv']) : '';
            $fechaExpiracion = isset($_POST['fechaExpiracion']) ? trim($_POST['fechaExpiracion']) : '';
            $pin = isset($_POST['pin']) ? trim($_POST['pin']) : '';
            $idCuenta = isset($_POST['idCuenta']) ? (int)$_POST['idCuenta'] : 0;
            $estado = isset($_POST['estado']) ? trim($_POST['estado']) : 'activa';

            // Validar datos
            if (empty($idTarjeta) || empty($tipoTarjeta) || empty($nroTarjeta) || empty($cvv) || empty($fechaExpiracion) || empty($pin) || empty($idCuenta)) {
                $this->session->setFlashMessage('error', $lang['all_fields_required']);
                header('Location: index.php?controller=tarjeta&action=editar&id=' . $idTarjeta);
                exit;
            }

            // Verificar si ya existe otra tarjeta con el mismo número
            if ($this->model->existeNumeroTarjeta($nroTarjeta, $idTarjeta)) {
                $this->session->setFlashMessage('error', $lang['card_number_already_exists']);
                header('Location: index.php?controller=tarjeta&action=editar&id=' . $idTarjeta);
                exit;
            }

            // Asignar datos al modelo
            $this->model->idTarjeta = $idTarjeta;
            $this->model->tipoTarjeta = $tipoTarjeta;
            $this->model->nroTarjeta = $nroTarjeta;
            $this->model->cvv = $cvv;
            $this->model->fechaExpiracion = $fechaExpiracion;
            $this->model->pin = $pin;
            $this->model->idCuenta = $idCuenta;
            $this->model->estado = $estado;

            // Actualizar tarjeta
            if ($this->model->actualizar()) {
                $this->session->setFlashMessage('success', $lang['card_updated']);
                header('Location: index.php?controller=tarjeta&action=ver&id=' . $idTarjeta);
                exit;
            } else {
                $this->session->setFlashMessage('error', $lang['card_update_error']);
                header('Location: index.php?controller=tarjeta&action=editar&id=' . $idTarjeta);
                exit;
            }
        } else {
            // Si no se enviaron datos por POST, redirigir al listado
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }
    }

    // Método para cambiar el estado (activar/desactivar) de una tarjeta
    public function cambiarEstado()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se proporcionaron los parámetros necesarios
        if (!isset($_GET['id']) || empty($_GET['id']) || !isset($_GET['estado']) || empty($_GET['estado'])) {
            $this->session->setFlashMessage('error', $lang['missing_parameters']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        $id = (int)$_GET['id'];
        $nuevoEstado = $_GET['estado'] === 'activa' ? 'activa' : 'inactiva';

        // Verificar si la tarjeta existe
        $this->model->idTarjeta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['card_not_found']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        // Cambiar el estado de la tarjeta
        if ($this->model->cambiarEstado($nuevoEstado)) {
            $mensaje = $nuevoEstado === 'activa' ? $lang['card_activated'] : $lang['card_deactivated'];
            $this->session->setFlashMessage('success', $mensaje);
        } else {
            $this->session->setFlashMessage('error', $lang['card_status_change_error']);
        }

        // Redirigir a la vista de detalles
        header('Location: index.php?controller=tarjeta&action=ver&id=' . $id);
        exit;
    }

    // Método para eliminar tarjeta
    public function eliminar()
    {
        global $lang;

        // Verificar si el usuario ha iniciado sesión
        if (!isset($_SESSION['user_id'])) {
            header('Location: index.php?controller=usuario&action=login');
            exit;
        }

        // Verificar si se proporcionó un ID
        if (!isset($_GET['id']) || empty($_GET['id'])) {
            $this->session->setFlashMessage('error', $lang['card_id_not_specified']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        $id = (int)$_GET['id'];

        // Verificar si la tarjeta existe
        $this->model->idTarjeta = $id;
        if (!$this->model->obtenerUna()) {
            $this->session->setFlashMessage('error', $lang['card_not_found']);
            header('Location: index.php?controller=tarjeta&action=listar');
            exit;
        }

        // Guardar el ID de la cuenta para redireccionar después
        $idCuenta = $this->model->idCuenta;

        // Eliminar tarjeta
        if ($this->model->eliminar()) {
            $this->session->setFlashMessage('success', $lang['card_deleted']);

            // Redireccionar a la vista de la cuenta asociada
            header('Location: index.php?controller=cuenta&action=ver&id=' . $idCuenta);
            exit;
        } else {
            $this->session->setFlashMessage('error', $lang['card_delete_error']);
            header('Location: index.php?controller=tarjeta&action=ver&id=' . $id);
            exit;
        }
    }
}
