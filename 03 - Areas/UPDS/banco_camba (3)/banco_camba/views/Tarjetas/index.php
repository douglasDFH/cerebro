<?php
// Asegurar que las variables necesarias estén definidas
if (!isset($lang)) {
    echo "Error: Variables de idioma no definidas.";
    exit;
}

// Mensaje de error o éxito
$errorMsg = '';
$successMsg = '';

if (isset($_GET['error'])) {
    switch ($_GET['error']) {
        case 'pins_no_coinciden':
            $errorMsg = 'Los PINs no coinciden. Por favor, inténtelo de nuevo.';
            break;
        case 'db_error':
            $errorMsg = 'Hubo un error al procesar su solicitud. Por favor, inténtelo más tarde.';
            break;
        default:
            $errorMsg = 'Ocurrió un error. Por favor, inténtelo de nuevo.';
    }
}

if (isset($_GET['success'])) {
    switch ($_GET['success']) {
        case 'tarjeta_creada':
            $successMsg = 'Su tarjeta ha sido creada exitosamente.';
            break;
        case 'tarjeta_creada_demo':
            $successMsg = 'Demostración: Su tarjeta ha sido creada exitosamente (modo demo).';
            break;
        default:
            $successMsg = 'Operación completada con éxito.';
    }
}
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['request_card']; ?> - Banco CAMBA</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css">
</head>
<body>
    <div class="container mt-4">
        <h1><?php echo $lang['request_card']; ?></h1>
        
        <?php if ($errorMsg): ?>
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <?php echo $errorMsg; ?>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <?php endif; ?>
        
        <?php if ($successMsg): ?>
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <?php echo $successMsg; ?>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <?php endif; ?>

        <div class="card">
            <div class="card-header">
                <h3><?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno); ?></h3>
                <p><?php echo $lang['account_number']; ?>: <strong><?php echo htmlspecialchars($cuenta->nroCuenta); ?></strong></p>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <form method="POST" action="index.php?controller=Tarjetas&action=crearTarjeta&id=<?php echo $cuenta->idCuenta; ?>" class="needs-validation">
                            <div class="form-group mb-3">
                                <label for="pin" class="form-label"><?php echo $lang['pin']; ?> <span class="text-danger">*</span></label>
                                <input type="password" class="form-control" id="pin" name="pin" pattern="[0-9]{4}" maxlength="4" required>
                                <small class="form-text text-muted"><?php echo $lang['pin_must_be_4_digits']; ?></small>
                            </div>
                            
                            <div class="form-group mb-3">
                                <label for="pin_confirmacion" class="form-label"><?php echo $lang['confirm_pin']; ?> <span class="text-danger">*</span></label>
                                <input type="password" class="form-control" id="pin_confirmacion" name="pin_confirmacion" pattern="[0-9]{4}" maxlength="4" required>
                            </div>
                            
                            <div class="alert alert-info mt-3">
                                <strong><?php echo $lang['important']; ?>:</strong> <?php echo $lang['card_pin_info']; ?>
                            </div>
                            
                            <div class="form-group mt-3">
                                <button type="submit" class="btn btn-primary"><?php echo $lang['request_card']; ?></button>
                                <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuenta->idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
                            </div>
                        </form>
                    </div>
                    <div class="col-md-6">
                        <div class="card-preview" style="background: linear-gradient(45deg, #056f1f, #0a4d14); color: white; padding: 20px; border-radius: 10px; margin-bottom: 20px; min-height: 200px;">
                            <div style="font-size: 1.2rem; margin-bottom: 20px; letter-spacing: 2px;">
                                **** **** **** ****
                            </div>
                            <div style="display: flex; justify-content: space-between;">
                                <div>
                                    <div style="font-size: 0.8rem; text-transform: uppercase;"><?php echo $lang['cardholder']; ?></div>
                                    <div><?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno); ?></div>
                                </div>
                                <div>
                                    <div style="font-size: 0.8rem; text-transform: uppercase;"><?php echo $lang['expiration_date']; ?></div>
                                    <div>--/--</div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="card bg-light">
                            <div class="card-body">
                                <h5><?php echo $lang['card_benefits']; ?></h5>
                                <ul>
                                    <li><?php echo $lang['card_benefit_1']; ?></li>
                                    <li><?php echo $lang['card_benefit_2']; ?></li>
                                    <li><?php echo $lang['card_benefit_3']; ?></li>
                                    <li><?php echo $lang['card_benefit_4']; ?></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <?php
// Este es un fragmento que muestra cómo incluir correctamente el controlador de Tarjetas
// desde el controlador principal o archivo index.php

// Detectar el controlador solicitado
$controller = isset($_GET['controller']) ? $_GET['controller'] : 'bienvenida';
$action = isset($_GET['action']) ? $_GET['action'] : 'index';

// Convertir el nombre del controlador a formato adecuado
// Aseguramos que la primera letra sea mayúscula y el resto en minúsculas
$controllerName = ucfirst(strtolower($controller)) . 'Controller';
$controllerFile = 'controllers/' . $controllerName . '.php';

// Verificar si el archivo del controlador existe
if (file_exists($controllerFile)) {
    require_once $controllerFile;
    
    // Crear una instancia del controlador
    $controller = new $controllerName();
    
    // Verificar si el método de acción existe
    if (method_exists($controller, $action)) {
        // Ejecutar el método de acción
        $controller->$action();
    } else {
        // Si el método no existe, mostrar error
        echo "Error: La acción '$action' no existe en el controlador '$controllerName'.";
    }
} else {
    // Si el controlador no existe, mostrar error
    echo "Error: El controlador '$controllerName' no existe.";
}
?>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
    document.addEventListener('DOMContentLoaded', function() {
        // Validación del formulario
        const form = document.querySelector('.needs-validation');
        form.addEventListener('submit', function(event) {
            const pin = document.getElementById('pin').value;
            const pinConfirmacion = document.getElementById('pin_confirmacion').value;
            
            if (pin !== pinConfirmacion) {
                event.preventDefault();
                alert('<?php echo $lang['pins_dont_match']; ?>');
                return false;
            }
            
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
        
        // Restringir entrada a solo números
        const pinInputs = document.querySelectorAll('input[type="password"]');
        pinInputs.forEach(input => {
            input.addEventListener('input', function(e) {
                this.value = this.value.replace(/[^0-9]/g, '');
                if (this.value.length > 4) {
                    this.value = this.value.slice(0, 4);
                }
            });
            
            input.addEventListener('keypress', function(e) {
                if (!/[0-9]/.test(e.key)) {
                    e.preventDefault();
                }
            });
        });
    });
    </script>
</body>
</html>