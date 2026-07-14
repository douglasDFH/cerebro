<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Obtener la lista de roles activos para el select
$sentencia_roles = $conexion->prepare("SELECT rol_id, nombre_rol, descripcion FROM roles WHERE activo = 1 ORDER BY nombre_rol");
$sentencia_roles->execute();
$lista_roles = $sentencia_roles->fetchAll(PDO::FETCH_ASSOC);

// Procesar el formulario cuando se envía
if ($_POST) {
    // Recolectar y validar los datos del método POST
    $usuario = (isset($_POST["usuario"])) ? trim($_POST["usuario"]) : "";  
    $password = (isset($_POST["password"])) ? $_POST["password"] : "";
    $email = (isset($_POST["email"])) ? trim($_POST["email"]) : "";
    $rol_id = (isset($_POST["rol_id"])) ? (int)$_POST["rol_id"] : 1; // Por defecto rol usuario

    // Validaciones básicas
    $errores = [];
    
    if (empty($usuario)) {
        $errores[] = "El nombre de usuario es obligatorio";
    }
    
    if (empty($password)) {
        $errores[] = "La contraseña es obligatoria";
    } elseif (strlen($password) < 6) {
        $errores[] = "La contraseña debe tener al menos 6 caracteres";
    }
    
    if (empty($email)) {
        $errores[] = "El correo electrónico es obligatorio";
    } elseif (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        $errores[] = "El correo electrónico no es válido";
    }

    // Verificar si el usuario o email ya existe
    if (empty($errores)) {
        $verificar = $conexion->prepare("SELECT COUNT(*) FROM usuarios WHERE usuario = :usuario OR email = :email");
        $verificar->bindParam(":usuario", $usuario);
        $verificar->bindParam(":email", $email);
        $verificar->execute();
        
        if ($verificar->fetchColumn() > 0) {
            $errores[] = "El usuario o correo electrónico ya existe";
        }
    }

    // Si no hay errores, proceder con la inserción
    if (empty($errores)) {
        try {
            // Cifrar la contraseña con password_hash (más seguro)
            $hashed_password = password_hash($password, PASSWORD_DEFAULT);

            // Preparar la inserción de los datos
            $sentencia = $conexion->prepare("INSERT INTO usuarios(user_id, usuario, password, email, rol_id) 
                                             VALUES (null, :usuario, :password, :email, :rol_id)");
            
            // Asignar los valores a los parámetros
            $sentencia->bindParam(":usuario", $usuario);  
            $sentencia->bindParam(":password", $hashed_password); 
            $sentencia->bindParam(":email", $email);
            $sentencia->bindParam(":rol_id", $rol_id);
            
            // Ejecutar la consulta
            $sentencia->execute();

            $mensaje = "Usuario agregado correctamente";
            // Redirección al archivo index.php con mensaje de éxito
            header("Location:index.php?mensaje=" . urlencode($mensaje));
            exit();
        } catch (Exception $e) {
            $errores[] = "Error al crear el usuario: " . $e->getMessage();
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
                <i class="bi bi-person-plus"></i> Crear Nuevo Usuario
            </h2>
            <p class="text-muted">Complete los datos para crear un nuevo usuario en el sistema</p>
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
        <div class="col-lg-8 col-xl-6">
            <div class="card shadow-sm border-0">
                <!-- Encabezado de la tarjeta -->
                <div class="card-header bg-primary text-white py-3">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-person-fill me-2"></i>
                        Información del Usuario
                    </h5>
                </div>
                
                <!-- Cuerpo de la tarjeta -->
                <div class="card-body p-4">
                    <form action="" method="post" novalidate>
                        <!-- Campo Usuario -->
                        <div class="mb-4">
                            <label for="usuario" class="form-label fw-semibold">
                                <i class="bi bi-person me-1"></i>
                                Nombre de Usuario <span class="text-danger">*</span>
                            </label>
                            <input type="text" 
                                   class="form-control form-control-lg" 
                                   name="usuario" 
                                   id="usuario" 
                                   placeholder="Ingrese el nombre de usuario"
                                   value="<?php echo isset($_POST['usuario']) ? htmlspecialchars($_POST['usuario']) : ''; ?>"
                                   required>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                El nombre de usuario debe ser único en el sistema
                            </div>
                        </div>

                        <!-- Campo Contraseña -->
                        <div class="mb-4">
                            <label for="password" class="form-label fw-semibold">
                                <i class="bi bi-lock me-1"></i>
                                Contraseña <span class="text-danger">*</span>
                            </label>
                            <div class="input-group">
                                <input type="password" 
                                       class="form-control form-control-lg" 
                                       name="password" 
                                       id="password" 
                                       placeholder="Ingrese la contraseña"
                                       minlength="6"
                                       required>
                                <button class="btn btn-outline-secondary" type="button" id="togglePassword">
                                    <i class="bi bi-eye" id="eyeIcon"></i>
                                </button>
                            </div>
                            <div class="form-text">
                                <i class="bi bi-shield-check me-1"></i>
                                La contraseña debe tener al menos 6 caracteres
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
                                   placeholder="usuario@ejemplo.com"
                                   value="<?php echo isset($_POST['email']) ? htmlspecialchars($_POST['email']) : ''; ?>"
                                   required>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                Ingrese un correo electrónico válido
                            </div>
                        </div>

                        <!-- Campo Rol -->
                        <div class="mb-4">
                            <label for="rol_id" class="form-label fw-semibold">
                                <i class="bi bi-shield-lock me-1"></i>
                                Rol del Usuario <span class="text-danger">*</span>
                            </label>
                            <select class="form-select form-select-lg" name="rol_id" id="rol_id" required>
                                <option value="">Seleccione un rol...</option>
                                <?php foreach ($lista_roles as $rol): ?>
                                    <option value="<?php echo $rol['rol_id']; ?>" 
                                            <?php echo (isset($_POST['rol_id']) && $_POST['rol_id'] == $rol['rol_id']) ? 'selected' : ''; ?>>
                                        <?php echo htmlspecialchars(ucfirst($rol['nombre_rol'])); ?>
                                        <?php if ($rol['descripcion']): ?>
                                            - <?php echo htmlspecialchars($rol['descripcion']); ?>
                                        <?php endif; ?>
                                    </option>
                                <?php endforeach; ?>
                            </select>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                Seleccione el rol que tendrá el usuario en el sistema
                            </div>
                        </div>

                        <!-- Botones de acción -->
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <a href="index.php" class="btn btn-outline-secondary btn-lg me-md-2">
                                <i class="bi bi-arrow-left me-1"></i>
                                Cancelar
                            </a>
                            <button type="submit" class="btn btn-primary btn-lg">
                                <i class="bi bi-check-lg me-1"></i>
                                Crear Usuario
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Script para mostrar/ocultar contraseña -->
<script>
document.getElementById('togglePassword').addEventListener('click', function() {
    const password = document.getElementById('password');
    const eyeIcon = document.getElementById('eyeIcon');
    
    if (password.type === 'password') {
        password.type = 'text';
        eyeIcon.className = 'bi bi-eye-slash';
    } else {
        password.type = 'password';
        eyeIcon.className = 'bi bi-eye';
    }
});
</script>

<?php include("../../templates/footer.php"); ?>