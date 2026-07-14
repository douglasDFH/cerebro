<?php
class TarjetasController {
    private $tarjetaModel;
    private $cliente;
    private $cuenta;
    private $lang;

    public function __construct() {
        // Activar la visualización de errores
        ini_set('display_errors', 1);
        ini_set('display_startup_errors', 1);
        error_reporting(E_ALL);
        
        // Configurar idioma
        $this->setupLanguage();
        
        // Simular datos de cliente y cuenta (reemplazar con lógica real en producción)
        $this->setupDemoData();
        
        // Intentar conectar con la base de datos e inicializar el modelo
        try {
            require_once 'models/Tarjeta.php';
            $this->tarjetaModel = new Tarjeta($this->getDB());
        } catch (Exception $e) {
            // En caso de error en la conexión, simplemente continuamos (para evitar pantalla blanca)
            // En producción, se debería manejar este error adecuadamente
            error_log("Error de conexión a la DB: " . $e->getMessage());
        }
    }

    // Método para obtener la conexión a la base de datos
    private function getDB() {
        try {
            $db = new PDO('mysql:host=localhost;dbname=banco_camba', 'usuario', 'contraseña');
            $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            return $db;
        } catch (PDOException $e) {
            // Loguear el error pero no detener la ejecución
            error_log("Error de conexión a la DB: " . $e->getMessage());
            return null;
        }
    }

    // Configurar datos de idioma
    private function setupLanguage() {
        $this->lang = [
            'request_card' => 'Solicitar Tarjeta',
            'account_number' => 'Número de Cuenta',
            'pin' => 'PIN',
            'confirm_pin' => 'Confirmar PIN',
            'pin_must_be_4_digits' => 'El PIN debe tener 4 dígitos numéricos',
            'important' => 'Importante',
            'card_pin_info' => 'Memorice su PIN y nunca lo comparta con nadie',
            'cancel' => 'Cancelar',
            'cardholder' => 'Titular',
            'expiration_date' => 'Fecha de expiración',
            'card_benefits' => 'Beneficios de la Tarjeta',
            'card_benefit_1' => 'Pago en comercios nacionales e internacionales',
            'card_benefit_2' => 'Retiro de efectivo en cajeros automáticos',
            'card_benefit_3' => 'Compras por internet seguras',
            'card_benefit_4' => 'Protección contra fraudes',
            'pins_dont_match' => 'Los PINs no coinciden'
        ];
    }

    // Configurar datos de demostración
    private function setupDemoData() {
        $this->cliente = (object) [
            'nombre' => 'Juan',
            'apellidoPaterno' => 'Pérez',
            'apellidoMaterno' => 'Gómez'
        ];

        $this->cuenta = (object) [
            'nroCuenta' => '123456789',
            'idCuenta' => 1
        ];
    }

    // Método para cargar la vista index de tarjetas
    public function index() {
        // Proporcionar variables para la vista
        $cliente = $this->cliente;
        $cuenta = $this->cuenta;
        $lang = $this->lang;
        
        // Cargar la vista
        require_once 'views/Tarjetas/index.php';    }

    // Método para procesar la solicitud de la tarjeta
    public function crearTarjeta() {
        if (isset($_POST['pin']) && isset($_POST['pin_confirmacion'])) {
            $pin = $_POST['pin'];
            $pinConfirmacion = $_POST['pin_confirmacion'];
            $idCuenta = isset($_GET['id']) ? $_GET['id'] : null;
            
            // Validar que los PINs coincidan
            if ($pin !== $pinConfirmacion) {
                // Redirigir con mensaje de error
                header("Location: index.php?controller=Tarjetas&action=index&error=pins_no_coinciden");
                exit;
            }
            
            // Guardar la tarjeta en la base de datos (si la conexión está disponible)
            if ($this->tarjetaModel) {
                try {
                    // Aquí iría la lógica para guardar la tarjeta
                    // $this->tarjetaModel->crearTarjeta($idCuenta, $pin);
                    
                    // Redirigir con mensaje de éxito
                    header("Location: index.php?controller=cuenta&action=ver&id=$idCuenta&success=tarjeta_creada");
                    exit;
                } catch (Exception $e) {
                    // Redirigir con mensaje de error
                    header("Location: index.php?controller=Tarjetas&action=index&error=db_error");
                    exit;
                }
            } else {
                // Mostrar un mensaje simulando éxito (para demostración)
                header("Location: index.php?controller=Tarjetas&action=index&success=tarjeta_creada_demo");
                exit;
            }
        } else {
            // Si no hay datos de formulario, redirigir a la vista principal
            header("Location: index.php?controller=Tarjetas&action=index");
            exit;
        }
    }
}
?>