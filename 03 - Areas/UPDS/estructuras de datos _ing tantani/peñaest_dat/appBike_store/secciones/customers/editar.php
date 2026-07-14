<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Variables para almacenar los datos del cliente
$txtID = "";
$first_name = "";
$last_name = "";
$phone = "";
$email = "";
$street = "";
$city = "";
$state = "";
$errores = [];

// Verificar si el ID del cliente es recibido por GET
if (isset($_GET['txtID'])) {
    $txtID = (int)$_GET['txtID'];

    // Consulta para obtener los datos del cliente
    $sentencia = $conexion->prepare("SELECT * FROM customers WHERE customer_id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    $cliente = $sentencia->fetch(PDO::FETCH_ASSOC);

    // Si no existe el cliente, redirigir al listado
    if (!$cliente) {
        header("Location:index.php?mensaje=" . urlencode("Cliente no encontrado"));
        exit();
    }

    // Variables para llenar el formulario con los datos del cliente
    $first_name = $cliente['first_name'];
    $last_name = $cliente['last_name'];
    $phone = $cliente['phone'];
    $email = $cliente['email'];
    $street = $cliente['street'];
    $city = $cliente['city'];
    $state = $cliente['state'];
} else {
    // Si no hay ID, redirigir al listado
    header("Location:index.php?mensaje=" . urlencode("ID de cliente no válido"));
    exit();
}

// Procesar el formulario de actualización
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Obtener los datos del formulario
    $first_name = trim($_POST['first_name']);
    $last_name = trim($_POST['last_name']);
    $phone = trim($_POST['phone']);
    $email = trim($_POST['email']);
    $street = trim($_POST['street']);
    $city = trim($_POST['city']);
    $state = trim($_POST['state']);

    // Validaciones básicas
    if (empty($first_name)) {
        $errores[] = "El nombre es obligatorio";
    } elseif (strlen($first_name) < 2) {
        $errores[] = "El nombre debe tener al menos 2 caracteres";
    } elseif (strlen($first_name) > 50) {
        $errores[] = "El nombre no puede exceder 50 caracteres";
    }
    
    if (empty($last_name)) {
        $errores[] = "El apellido es obligatorio";
    } elseif (strlen($last_name) < 2) {
        $errores[] = "El apellido debe tener al menos 2 caracteres";
    } elseif (strlen($last_name) > 50) {
        $errores[] = "El apellido no puede exceder 50 caracteres";
    }
    
    if (empty($phone)) {
        $errores[] = "El teléfono es obligatorio";
    } elseif (!preg_match('/^[0-9+\-\s()]{7,15}$/', $phone)) {
        $errores[] = "El formato del teléfono no es válido (7-15 caracteres, solo números, espacios, guiones y paréntesis)";
    }
    
    if (empty($email)) {
        $errores[] = "El correo electrónico es obligatorio";
    } elseif (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        $errores[] = "El correo electrónico no es válido";
    } elseif (strlen($email) > 100) {
        $errores[] = "El correo electrónico no puede exceder 100 caracteres";
    }
    
    if (empty($street)) {
        $errores[] = "La dirección es obligatoria";
    } elseif (strlen($street) < 5) {
        $errores[] = "La dirección debe tener al menos 5 caracteres";
    } elseif (strlen($street) > 100) {
        $errores[] = "La dirección no puede exceder 100 caracteres";
    }
    
    if (empty($city)) {
        $errores[] = "La ciudad es obligatoria";
    } elseif (strlen($city) < 2) {
        $errores[] = "La ciudad debe tener al menos 2 caracteres";
    } elseif (strlen($city) > 50) {
        $errores[] = "La ciudad no puede exceder 50 caracteres";
    } elseif (!preg_match('/^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/', $city)) {
        $errores[] = "La ciudad solo puede contener letras y espacios";
    }
    
    if (empty($state)) {
        $errores[] = "El departamento es obligatorio";
    }

    // Verificar si el email ya existe (excluyendo el cliente actual)
    if (empty($errores)) {
        $verificar_email = $conexion->prepare("SELECT COUNT(*) FROM customers WHERE email = :email AND customer_id != :id");
        $verificar_email->bindParam(":email", $email);
        $verificar_email->bindParam(":id", $txtID);
        $verificar_email->execute();
        
        if ($verificar_email->fetchColumn() > 0) {
            $errores[] = "El correo electrónico ya está registrado por otro cliente";
        }
    }

    // Si no hay errores, proceder con la actualización
    if (empty($errores)) {
        try {
            // Preparar la consulta de actualización
            $sentencia_update = $conexion->prepare("UPDATE customers SET 
                                                   first_name = :first_name, 
                                                   last_name = :last_name, 
                                                   phone = :phone, 
                                                   email = :email, 
                                                   street = :street, 
                                                   city = :city, 
                                                   state = :state 
                                                   WHERE customer_id = :id");
            
            // Asignar parámetros
            $sentencia_update->bindParam(":first_name", $first_name);
            $sentencia_update->bindParam(":last_name", $last_name);
            $sentencia_update->bindParam(":phone", $phone);
            $sentencia_update->bindParam(":email", $email);
            $sentencia_update->bindParam(":street", $street);
            $sentencia_update->bindParam(":city", $city);
            $sentencia_update->bindParam(":state", $state);
            $sentencia_update->bindParam(":id", $txtID);
            
            // Ejecutar la actualización
            $sentencia_update->execute();

            // Redirigir con un mensaje de éxito
            $mensaje = "Cliente actualizado correctamente - ID #" . $txtID;
            header("Location:index.php?mensaje=" . urlencode($mensaje));
            exit();
        } catch (Exception $e) {
            $errores[] = "Error al actualizar el cliente: " . $e->getMessage();
        }
    }
}
?>

<?php include("../../templates/header.php"); ?>

<!-- Contenedor principal con espaciado -->
<div class="container-fluid py-4">
    <!-- Título de la página -->
    <div class="row mb-4">
        <div class="col-12">
            <h2 class="text-primary fw-bold">
                <i class="bi bi-person-gear"></i> Editar Cliente
            </h2>
            <p class="text-muted">Modifique los datos del cliente #<?php echo str_pad($txtID, 4, '0', STR_PAD_LEFT); ?></p>
        </div>
    </div>

    <!-- Mostrar errores si existen -->
    <?php if (!empty($errores)): ?>
    <div class="row mb-4">
        <div class="col-12">
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                <strong>¡Atención!</strong> Se encontraron los siguientes errores:
                <ul class="mb-0 mt-2">
                    <?php foreach ($errores as $error): ?>
                        <li><?php echo htmlspecialchars($error); ?></li>
                    <?php endforeach; ?>
                </ul>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        </div>
    </div>
    <?php endif; ?>

    <!-- Información actual del cliente -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="alert alert-warning">
                <i class="bi bi-person-check me-2"></i>
                <strong>Cliente Actual:</strong> <?php echo htmlspecialchars($first_name . ' ' . $last_name); ?> - 
                <strong>Email:</strong> <?php echo htmlspecialchars($email); ?> - 
                <strong>Ubicación:</strong> <?php echo htmlspecialchars($city . ', ' . $state); ?>
            </div>
        </div>
    </div>

    <!-- Formulario principal -->
    <div class="row justify-content-center">
        <div class="col-lg-10 col-xl-8">
            <div class="card shadow-sm border-0">
                <!-- Encabezado de la tarjeta -->
                <div class="card-header bg-warning text-dark py-3">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-person-fill me-2"></i>
                        Información del Cliente
                    </h5>
                </div>
                
                <!-- Cuerpo de la tarjeta -->
                <div class="card-body p-4">
                    <!-- Información del ID (solo lectura) -->
                    <div class="alert alert-info mb-4">
                        <i class="bi bi-info-circle me-2"></i>
                        <strong>ID del Cliente:</strong> <?php echo htmlspecialchars($txtID); ?>
                        <span class="ms-3">
                            <strong>Registro desde:</strong> <?php echo date('Y'); ?>
                        </span>
                    </div>

                    <form action="" method="post" novalidate>
                        <div class="row">
                            <!-- Columna izquierda -->
                            <div class="col-md-6">
                                <!-- Campo Nombres -->
                                <div class="mb-4">
                                    <label for="first_name" class="form-label fw-semibold">
                                        <i class="bi bi-person me-1"></i>
                                        Nombres <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" 
                                           class="form-control form-control-lg" 
                                           name="first_name" 
                                           id="first_name" 
                                           placeholder="Ej: Juan Carlos"
                                           value="<?php echo htmlspecialchars($first_name); ?>"
                                           maxlength="50"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Modifique el/los nombres del cliente (2-50 caracteres)
                                    </div>
                                </div>

                                <!-- Campo Apellidos -->
                                <div class="mb-4">
                                    <label for="last_name" class="form-label fw-semibold">
                                        <i class="bi bi-person me-1"></i>
                                        Apellidos <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" 
                                           class="form-control form-control-lg" 
                                           name="last_name" 
                                           id="last_name" 
                                           placeholder="Ej: Pérez García"
                                           value="<?php echo htmlspecialchars($last_name); ?>"
                                           maxlength="50"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Modifique el/los apellidos del cliente (2-50 caracteres)
                                    </div>
                                </div>

                                <!-- Campo Teléfono -->
                                <div class="mb-4">
                                    <label for="phone" class="form-label fw-semibold">
                                        <i class="bi bi-telephone me-1"></i>
                                        Teléfono <span class="text-danger">*</span>
                                    </label>
                                    <input type="tel" 
                                           class="form-control form-control-lg" 
                                           name="phone" 
                                           id="phone" 
                                           placeholder="Ej: 78137892 o (591) 4-4211515"
                                           value="<?php echo htmlspecialchars($phone); ?>"
                                           maxlength="15"
                                           pattern="[0-9+\-\s()]+"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Actualice el número de teléfono o celular
                                    </div>
                                </div>

                                <!-- Campo Email -->
                                <div class="mb-4">
                                    <label for="email" class="form-label fw-semibold">
                                        <i class="bi bi-envelope me-1"></i>
                                        Correo Electrónico <span class="text-danger">*</span>
                                    </label>
                                    <input type="email" 
                                           class="form-control form-control-lg" 
                                           name="email" 
                                           id="email" 
                                           placeholder="cliente@ejemplo.com"
                                           value="<?php echo htmlspecialchars($email); ?>"
                                           maxlength="100"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Correo electrónico válido (debe ser único en el sistema)
                                    </div>
                                </div>
                            </div>

                            <!-- Columna derecha -->
                            <div class="col-md-6">
                                <!-- Campo Dirección -->
                                <div class="mb-4">
                                    <label for="street" class="form-label fw-semibold">
                                        <i class="bi bi-house me-1"></i>
                                        Dirección <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" 
                                           class="form-control form-control-lg" 
                                           name="street" 
                                           id="street" 
                                           placeholder="Ej: Av. España #123, Zona Central"
                                           value="<?php echo htmlspecialchars($street); ?>"
                                           maxlength="100"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Actualice la dirección completa del cliente
                                    </div>
                                </div>

                                <!-- Campo Ciudad - CAMBIADO A INPUT DE TEXTO -->
                                <div class="mb-4">
                                    <label for="city" class="form-label fw-semibold">
                                        <i class="bi bi-building me-1"></i>
                                        Ciudad <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" 
                                           class="form-control form-control-lg" 
                                           name="city" 
                                           id="city" 
                                           placeholder="Ej: Santa Cruz de la Sierra"
                                           value="<?php echo htmlspecialchars($city); ?>"
                                           maxlength="50"
                                           pattern="[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Modifique el nombre de la ciudad (solo letras y espacios)
                                    </div>
                                    
                                    <!-- Lista de sugerencias para ayudar al usuario -->
                                    <div id="city-suggestions" class="mt-2">
                                        <small class="text-muted">
                                            <i class="bi bi-lightbulb me-1"></i>
                                            <strong>Sugerencias:</strong> <span id="suggestions-text">La Paz, Cochabamba, Santa Cruz, Sucre, Oruro, Potosí, Tarija, Trinidad, Cobija</span>
                                        </small>
                                    </div>
                                    
                                    <!-- Mostrar valor actual si es diferente -->
                                    <?php if (!empty($city)): ?>
                                    <div class="mt-2">
                                        <small class="text-info">
                                            <i class="bi bi-pin-map me-1"></i>
                                            <strong>Valor actual:</strong> <?php echo htmlspecialchars($city); ?>
                                        </small>
                                    </div>
                                    <?php endif; ?>
                                </div>

                                <!-- Campo Departamento -->
                                <div class="mb-4">
                                    <label for="state" class="form-label fw-semibold">
                                        <i class="bi bi-geo-alt me-1"></i>
                                        Departamento <span class="text-danger">*</span>
                                    </label>
                                    <select class="form-select form-select-lg" name="state" id="state" required>
                                        <option value="">Seleccione un departamento...</option>
                                        <option value="La Paz" <?php echo ($state == 'La Paz') ? 'selected' : ''; ?>>La Paz</option>
                                        <option value="Cochabamba" <?php echo ($state == 'Cochabamba') ? 'selected' : ''; ?>>Cochabamba</option>
                                        <option value="Santa Cruz" <?php echo ($state == 'Santa Cruz') ? 'selected' : ''; ?>>Santa Cruz</option>
                                        <option value="Chuquisaca" <?php echo ($state == 'Chuquisaca') ? 'selected' : ''; ?>>Chuquisaca</option>
                                        <option value="Oruro" <?php echo ($state == 'Oruro') ? 'selected' : ''; ?>>Oruro</option>
                                        <option value="Potosí" <?php echo ($state == 'Potosí') ? 'selected' : ''; ?>>Potosí</option>
                                        <option value="Tarija" <?php echo ($state == 'Tarija') ? 'selected' : ''; ?>>Tarija</option>
                                        <option value="Beni" <?php echo ($state == 'Beni') ? 'selected' : ''; ?>>Beni</option>
                                        <option value="Pando" <?php echo ($state == 'Pando') ? 'selected' : ''; ?>>Pando</option>
                                    </select>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Seleccione el departamento donde reside el cliente
                                    </div>
                                </div>

                                <!-- Vista previa de cambios -->
                                <div class="mb-4">
                                    <div class="card bg-light">
                                        <div class="card-body p-3">
                                            <h6 class="fw-bold text-muted mb-2">
                                                <i class="bi bi-eye me-1"></i>
                                                Vista Previa de Cambios
                                            </h6>
                                            <div id="preview-info" class="small text-muted">
                                                <div id="preview-name">Nombre: <span class="fw-semibold"><?php echo htmlspecialchars($first_name . ' ' . $last_name); ?></span></div>
                                                <div id="preview-email">Email: <span class="fw-semibold"><?php echo htmlspecialchars($email); ?></span></div>
                                                <div id="preview-location">Ubicación: <span class="fw-semibold"><?php echo htmlspecialchars($city . ', ' . $state); ?></span></div>
                                                <div id="preview-changes" class="mt-2 text-warning" style="display: none;">
                                                    <i class="bi bi-exclamation-triangle me-1"></i>
                                                    <strong>Hay cambios pendientes</strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Botones de acción -->
                        <div class="row mt-4">
                            <div class="col-12">
                                <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                                    <a href="index.php" class="btn btn-outline-secondary btn-lg me-md-2">
                                        <i class="bi bi-arrow-left me-1"></i>
                                        Cancelar
                                    </a>
                                    <button type="submit" class="btn btn-warning btn-lg text-dark" id="submitBtn">
                                        <i class="bi bi-check-lg me-1"></i>
                                        Actualizar Cliente
                                    </button>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Script para validación y experiencia de usuario -->
<script>
document.addEventListener('DOMContentLoaded', function() {
    // Referencias a elementos del formulario
    const form = document.querySelector('form');
    const firstNameInput = document.getElementById('first_name');
    const lastNameInput = document.getElementById('last_name');
    const emailInput = document.getElementById('email');
    const cityInput = document.getElementById('city');
    const stateSelect = document.getElementById('state');
    const phoneInput = document.getElementById('phone');
    
    // Referencias a elementos de vista previa
    const previewName = document.getElementById('preview-name').querySelector('.fw-semibold');
    const previewEmail = document.getElementById('preview-email').querySelector('.fw-semibold');
    const previewLocation = document.getElementById('preview-location').querySelector('.fw-semibold');
    const previewChanges = document.getElementById('preview-changes');
    
    // Valores originales para detectar cambios
    const originalValues = {
        first_name: firstNameInput.value,
        last_name: lastNameInput.value,
        email: emailInput.value,
        city: cityInput.value,
        state: stateSelect.value,
        phone: phoneInput.value,
        street: document.getElementById('street').value
    };
    
    // Sugerencias de ciudades por departamento
    const citySuggestions = {
        'La Paz': ['La Paz', 'El Alto', 'Viacha', 'Achocalla', 'Mecapaca'],
        'Cochabamba': ['Cochabamba', 'Quillacollo', 'Sacaba', 'Tiquipaya', 'Colcapirhua'],
        'Santa Cruz': ['Santa Cruz de la Sierra', 'Montero', 'Warnes', 'La Guardia', 'Cotoca'],
        'Chuquisaca': ['Sucre', 'Yotala', 'Poroma', 'Yamparáez'],
        'Oruro': ['Oruro', 'Caracollo', 'Challapata', 'Machacamarca'],
        'Potosí': ['Potosí', 'Uyuni', 'Villazón', 'Tupiza'],
        'Tarija': ['Tarija', 'Yacuiba', 'Villamontes', 'Bermejo'],
        'Beni': ['Trinidad', 'Riberalta', 'Guayaramerín', 'San Borja'],
        'Pando': ['Cobija', 'Porvenir', 'Puerto Rico', 'Filadelfia']
    };
    
    // Actualizar sugerencias de ciudad basadas en departamento
    stateSelect.addEventListener('change', function() {
        const selectedState = this.value;
        const suggestionsText = document.getElementById('suggestions-text');
        
        if (selectedState && citySuggestions[selectedState]) {
            suggestionsText.textContent = citySuggestions[selectedState].join(', ');
        } else {
            suggestionsText.textContent = 'La Paz, Cochabamba, Santa Cruz, Sucre, Oruro, Potosí, Tarija, Trinidad, Cobija';
        }
        
        updatePreview();
        checkForChanges();
    });
    
    // Validación en tiempo real para ciudad (solo letras y espacios)
    cityInput.addEventListener('input', function() {
        // Permitir solo letras, espacios y caracteres especiales del español
        this.value = this.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
        
        // Capitalizar primera letra de cada palabra
        this.value = this.value.replace(/\b\w/g, function(letter) {
            return letter.toUpperCase();
        });
        
        updatePreview();
        checkForChanges();
    });
    
    // Formatear teléfono
    phoneInput.addEventListener('input', function() {
        // Permitir solo números, espacios, guiones y paréntesis
        this.value = this.value.replace(/[^0-9\s\-()]/g, '');
        checkForChanges();
    });
    
    // Detectar cambios en todos los campos
    function checkForChanges() {
        let hasChanges = false;
        
        // Verificar cada campo
        const currentValues = {
            first_name: firstNameInput.value,
            last_name: lastNameInput.value,
            email: emailInput.value,
            city: cityInput.value,
            state: stateSelect.value,
            phone: phoneInput.value,
            street: document.getElementById('street').value
        };
        
        for (let key in originalValues) {
            if (originalValues[key] !== currentValues[key]) {
                hasChanges = true;
                break;
            }
        }
        
        // Mostrar/ocultar indicador de cambios
        if (hasChanges) {
            previewChanges.style.display = 'block';
            document.getElementById('submitBtn').classList.add('btn-warning');
            document.getElementById('submitBtn').classList.remove('btn-secondary');
        } else {
            previewChanges.style.display = 'none';
            document.getElementById('submitBtn').classList.remove('btn-warning');
            document.getElementById('submitBtn').classList.add('btn-secondary');
        }
    }
    
    // Actualizar vista previa en tiempo real
    function updatePreview() {
        const firstName = firstNameInput.value.trim();
        const lastName = lastNameInput.value.trim();
        const email = emailInput.value.trim();
        const city = cityInput.value.trim();
        const state = stateSelect.value;
        
        // Actualizar nombre
        if (firstName || lastName) {
            previewName.textContent = `${firstName} ${lastName}`.trim();
        } else {
            previewName.textContent = '-';
        }
        
        // Actualizar email
        previewEmail.textContent = email || '-';
        
        // Actualizar ubicación
        if (city || state) {
            const location = [city, state].filter(Boolean).join(', ');
            previewLocation.textContent = location || '-';
        } else {
            previewLocation.textContent = '-';
        }
    }
    
    // Event listeners para actualizar vista previa y detectar cambios
    [firstNameInput, lastNameInput, emailInput, cityInput, document.getElementById('street')].forEach(input => {
        input.addEventListener('input', function() {
            updatePreview();
            checkForChanges();
        });
    });
    
    // Validación en tiempo real
    const inputs = form.querySelectorAll('input[required], select[required]');
    
    inputs.forEach(function(input) {
        input.addEventListener('blur', function() {
            validateInput(this);
        });
        
        input.addEventListener('input', function() {
            // Limpiar estado de validación mientras escribe
            this.classList.remove('is-invalid', 'is-valid');
        });
    });
    
    // Función de validación individual
    function validateInput(input) {
        const value = input.value.trim();
        let isValid = true;
        
        if (input.hasAttribute('required') && !value) {
            isValid = false;
        }
        
        // Validaciones específicas
        switch(input.type) {
            case 'email':
                const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (value && !emailRegex.test(value)) {
                    isValid = false;
                }
                break;
            case 'tel':
                const phoneRegex = /^[0-9+\-\s()]{7,15}$/;
                if (value && !phoneRegex.test(value)) {
                    isValid = false;
                }
                break;
        }
        
        // Validación específica para ciudad
        if (input.name === 'city') {
            const cityRegex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
            if (value && !cityRegex.test(value)) {
                isValid = false;
            }
        }
        
        // Aplicar clases de validación
        if (isValid) {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        } else {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');
        }
        
        return isValid;
    }
    
    // Validar formulario antes de enviar
    form.addEventListener('submit', function(e) {
        let formIsValid = true;
        
        inputs.forEach(function(input) {
            if (!validateInput(input)) {
                formIsValid = false;
            }
        });
        
        if (!formIsValid) {
            e.preventDefault();
            alert('Por favor, corrija los errores en el formulario antes de continuar.');
            
            // Enfocar el primer campo con error
            const firstInvalid = form.querySelector('.is-invalid');
            if (firstInvalid) {
                firstInvalid.focus();
            }
            return false;
        }
        
        // Confirmar actualización si hay cambios significativos
        let hasChanges = false;
        for (let key in originalValues) {
            const currentInput = document.querySelector(`[name="${key}"]`);
            if (currentInput && originalValues[key] !== currentInput.value) {
                hasChanges = true;
                break;
            }
        }
        
        if (hasChanges) {
            const confirmUpdate = confirm('¿Está seguro de que desea actualizar la información del cliente?\n\nEsto modificará los datos permanentemente.');
            if (!confirmUpdate) {
                e.preventDefault();
                return false;
            }
        }
    });
    
    // Botón para restaurar valores originales
    function addRestoreButton() {
        const buttonContainer = document.querySelector('.d-grid.gap-2');
        const restoreButton = document.createElement('button');
        restoreButton.type = 'button';
        restoreButton.className = 'btn btn-outline-info btn-sm me-2';
        restoreButton.innerHTML = '<i class="bi bi-arrow-clockwise me-1"></i>Restaurar Original';
        restoreButton.style.display = 'none';
        restoreButton.id = 'restoreBtn';
        
        restoreButton.addEventListener('click', function() {
            if (confirm('¿Desea restaurar todos los campos a sus valores originales?')) {
                // Restaurar valores originales
                firstNameInput.value = originalValues.first_name;
                lastNameInput.value = originalValues.last_name;
                emailInput.value = originalValues.email;
                cityInput.value = originalValues.city;
                stateSelect.value = originalValues.state;
                phoneInput.value = originalValues.phone;
                document.getElementById('street').value = originalValues.street;
                
                // Actualizar vista previa
                updatePreview();
                checkForChanges();
                
                // Limpiar validaciones
                inputs.forEach(input => {
                    input.classList.remove('is-valid', 'is-invalid');
                });
            }
        });
        
        // Insertar el botón antes del botón de cancelar
        const cancelButton = buttonContainer.querySelector('a');
        buttonContainer.insertBefore(restoreButton, cancelButton);
        
        // Mostrar/ocultar botón de restaurar basado en cambios
        const originalCheckForChanges = checkForChanges;
        checkForChanges = function() {
            originalCheckForChanges();
            const hasChanges = previewChanges.style.display === 'block';
            restoreButton.style.display = hasChanges ? 'inline-block' : 'none';
        };
    }
    
    // Agregar botón de restaurar
    addRestoreButton();
    
    // Enfocar el primer campo al cargar
    firstNameInput.focus();
    
    // Inicializar vista previa
    updatePreview();
    
    // Mensaje de bienvenida en consola para debugging
    console.log('Editor de cliente inicializado correctamente');
    console.log('Cliente ID:', <?php echo $txtID; ?>);
    console.log('Valores originales:', originalValues);
});

// Función global para debugging
function debugFormulario() {
    console.log('=== DEBUG FORMULARIO EDITAR CLIENTE ===');
    console.log('ID Cliente:', <?php echo $txtID; ?>);
    
    const campos = ['first_name', 'last_name', 'email', 'city', 'state', 'phone', 'street'];
    campos.forEach(campo => {
        const input = document.querySelector(`[name="${campo}"]`);
        if (input) {
            console.log(`${campo}:`, {
                original: '<?php echo addslashes($first_name); ?>',
                actual: input.value,
                valido: input.checkValidity()
            });
        }
    });
    console.log('================================');
}

// Hacer función debug disponible globalmente
window.debugFormulario = debugFormulario;