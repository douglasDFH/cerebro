<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Variables para almacenar los datos del formulario
$errores = [];

// Procesar el formulario cuando se envía
if($_POST){
    // Recolectar y validar los datos del método POST
    $first_name = (isset($_POST["first_name"]) ? trim($_POST["first_name"]) : "");
    $last_name = (isset($_POST["last_name"]) ? trim($_POST["last_name"]) : "");
    $phone = (isset($_POST["phone"]) ? trim($_POST["phone"]) : "");
    $email = (isset($_POST["email"]) ? trim($_POST["email"]) : "");
    $street = (isset($_POST["street"]) ? trim($_POST["street"]) : "");
    $city = (isset($_POST["city"]) ? trim($_POST["city"]) : "");
    $state = (isset($_POST["state"]) ? trim($_POST["state"]) : "");

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

    // Verificar si el email ya existe
    if (empty($errores)) {
        $verificar_email = $conexion->prepare("SELECT COUNT(*) FROM customers WHERE email = :email");
        $verificar_email->bindParam(":email", $email);
        $verificar_email->execute();
        
        if ($verificar_email->fetchColumn() > 0) {
            $errores[] = "El correo electrónico ya está registrado en el sistema";
        }
    }

    // Si no hay errores, proceder con la inserción
    if (empty($errores)) {
        try {
            // Preparar la inserción de los datos
            $sentencia = $conexion->prepare("INSERT INTO customers(customer_id, first_name, last_name, phone, email, street, city, state) 
                                           VALUES (null, :first_name, :last_name, :phone, :email, :street, :city, :state)");
            
            // Asignar los valores que tienen uso de :variable
            $sentencia->bindParam(":first_name", $first_name);
            $sentencia->bindParam(":last_name", $last_name);
            $sentencia->bindParam(":phone", $phone);
            $sentencia->bindParam(":email", $email);
            $sentencia->bindParam(":street", $street);
            $sentencia->bindParam(":city", $city);
            $sentencia->bindParam(":state", $state);
            
            // Ejecutar la consulta
            $sentencia->execute();
            
            $mensaje = "Cliente agregado correctamente con ID #" . $conexion->lastInsertId();
            // Redireccion al archivo index.php
            header("Location:index.php?mensaje=" . urlencode($mensaje));
            exit();
        } catch (Exception $e) {
            $errores[] = "Error al crear el cliente: " . $e->getMessage();
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
                <i class="bi bi-person-plus"></i> Crear Nuevo Cliente
            </h2>
            <p class="text-muted">Complete los datos para registrar un nuevo cliente en el sistema</p>
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

    <!-- Formulario principal -->
    <div class="row justify-content-center">
        <div class="col-lg-10 col-xl-8">
            <div class="card shadow-sm border-0">
                <!-- Encabezado de la tarjeta -->
                <div class="card-header bg-primary text-white py-3">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-person-fill me-2"></i>
                        Información del Cliente
                    </h5>
                </div>
                
                <!-- Cuerpo de la tarjeta -->
                <div class="card-body p-4">
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
                                           value="<?php echo isset($_POST['first_name']) ? htmlspecialchars($_POST['first_name']) : ''; ?>"
                                           maxlength="50"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Ingrese el/los nombres del cliente (2-50 caracteres)
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
                                           value="<?php echo isset($_POST['last_name']) ? htmlspecialchars($_POST['last_name']) : ''; ?>"
                                           maxlength="50"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Ingrese el/los apellidos del cliente (2-50 caracteres)
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
                                           value="<?php echo isset($_POST['phone']) ? htmlspecialchars($_POST['phone']) : ''; ?>"
                                           maxlength="15"
                                           pattern="[0-9+\-\s()]+"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Número de teléfono o celular (7-15 caracteres)
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
                                           value="<?php echo isset($_POST['email']) ? htmlspecialchars($_POST['email']) : ''; ?>"
                                           maxlength="100"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Correo electrónico válido (único en el sistema)
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
                                           value="<?php echo isset($_POST['street']) ? htmlspecialchars($_POST['street']) : ''; ?>"
                                           maxlength="100"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Dirección completa incluyendo calle, número y zona
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
                                           value="<?php echo isset($_POST['city']) ? htmlspecialchars($_POST['city']) : ''; ?>"
                                           maxlength="50"
                                           pattern="[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Ingrese el nombre de la ciudad (solo letras y espacios)
                                    </div>
                                    
                                    <!-- Lista de sugerencias para ayudar al usuario -->
                                    <div id="city-suggestions" class="mt-2">
                                        <small class="text-muted">
                                            <i class="bi bi-lightbulb me-1"></i>
                                            <strong>Sugerencias:</strong> La Paz, Cochabamba, Santa Cruz, Sucre, Oruro, Potosí, Tarija, Trinidad, Cobija
                                        </small>
                                    </div>
                                </div>

                                <!-- Campo Departamento -->
                                <div class="mb-4">
                                    <label for="state" class="form-label fw-semibold">
                                        <i class="bi bi-geo-alt me-1"></i>
                                        Departamento <span class="text-danger">*</span>
                                    </label>
                                    <select class="form-select form-select-lg" name="state" id="state" required>
                                        <option value="">Seleccione un departamento...</option>
                                        <option value="La Paz" <?php echo (isset($_POST['state']) && $_POST['state'] == 'La Paz') ? 'selected' : ''; ?>>La Paz</option>
                                        <option value="Cochabamba" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Cochabamba') ? 'selected' : ''; ?>>Cochabamba</option>
                                        <option value="Santa Cruz" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Santa Cruz') ? 'selected' : ''; ?>>Santa Cruz</option>
                                        <option value="Chuquisaca" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Chuquisaca') ? 'selected' : ''; ?>>Chuquisaca</option>
                                        <option value="Oruro" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Oruro') ? 'selected' : ''; ?>>Oruro</option>
                                        <option value="Potosí" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Potosí') ? 'selected' : ''; ?>>Potosí</option>
                                        <option value="Tarija" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Tarija') ? 'selected' : ''; ?>>Tarija</option>
                                        <option value="Beni" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Beni') ? 'selected' : ''; ?>>Beni</option>
                                        <option value="Pando" <?php echo (isset($_POST['state']) && $_POST['state'] == 'Pando') ? 'selected' : ''; ?>>Pando</option>
                                    </select>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Seleccione el departamento donde reside el cliente
                                    </div>
                                </div>

                                <!-- Vista previa de información -->
                                <div class="mb-4">
                                    <div class="card bg-light">
                                        <div class="card-body p-3">
                                            <h6 class="fw-bold text-muted mb-2">
                                                <i class="bi bi-eye me-1"></i>
                                                Vista Previa
                                            </h6>
                                            <div id="preview-info" class="small text-muted">
                                                <div id="preview-name">Nombre: <span class="fw-semibold">-</span></div>
                                                <div id="preview-email">Email: <span class="fw-semibold">-</span></div>
                                                <div id="preview-location">Ubicación: <span class="fw-semibold">-</span></div>
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
                                    <button type="submit" class="btn btn-primary btn-lg" id="submitBtn">
                                        <i class="bi bi-check-lg me-1"></i>
                                        Registrar Cliente
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
    
    // Autocompletar sugerencias basadas en departamento seleccionado
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
        const suggestionsDiv = document.getElementById('city-suggestions');
        
        if (selectedState && citySuggestions[selectedState]) {
            suggestionsDiv.innerHTML = `
                <small class="text-muted">
                    <i class="bi bi-lightbulb me-1"></i>
                    <strong>Ciudades en ${selectedState}:</strong> ${citySuggestions[selectedState].join(', ')}
                </small>
            `;
        } else {
            suggestionsDiv.innerHTML = `
                <small class="text-muted">
                    <i class="bi bi-lightbulb me-1"></i>
                    <strong>Sugerencias:</strong> La Paz, Cochabamba, Santa Cruz, Sucre, Oruro, Potosí, Tarija, Trinidad, Cobija
                </small>
            `;
        }
        
        updatePreview();
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
    });
    
    // Formatear teléfono
    phoneInput.addEventListener('input', function() {
        // Permitir solo números, espacios, guiones y paréntesis
        this.value = this.value.replace(/[^0-9\s\-()]/g, '');
    });
    
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
    
    // Event listeners para actualizar vista previa
    [firstNameInput, lastNameInput, emailInput, cityInput].forEach(input => {
        input.addEventListener('input', updatePreview);
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
        }
    });
    
    // Enfocar el primer campo al cargar
    firstNameInput.focus();
    
    // Inicializar vista previa
    updatePreview();
});
</script>

<!-- Estilos adicionales -->
<style>
.form-control:focus, .form-select:focus {
    border-color: #0d6efd;
    box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);
}

.is-valid {
    border-color: #198754 !important;
}

.is-invalid {
    border-color: #dc3545 !important;
}

#city-suggestions {
    transition: all 0.3s ease;
}

#preview-info div {
    margin-bottom: 0.25rem;
}

.card {
    transition: transform 0.2s ease-in-out;
}

.card:hover {
    transform: translateY(-2px);
}

/* Animación suave para los campos */
.form-control, .form-select {
    transition: all 0.3s ease;
}

/* Mejorar la apariencia de la vista previa */
#preview-info .fw-semibold {
    color: #0d6efd;
}
</style>

<?php include("../../templates/footer.php"); ?>