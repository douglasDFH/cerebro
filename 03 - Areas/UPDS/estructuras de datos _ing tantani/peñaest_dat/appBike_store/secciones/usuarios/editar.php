<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Obtener la lista de roles activos para el select
$sentencia_roles = $conexion->prepare("SELECT rol_id, nombre_rol, descripcion FROM roles WHERE activo = 1 ORDER BY nombre_rol");
$sentencia_roles->execute();
$lista_roles = $sentencia_roles->fetchAll(PDO::FETCH_ASSOC);

// Variables para almacenar los datos del usuario
$txtID = "";
$usuario_name = "";
$email = "";
$rol_id = 1;
$errores = [];

// Verificar si el ID de usuario es recibido por GET
if (isset($_GET['txtID'])) {
    $txtID = (int)$_GET['txtID'];

    // Consulta para obtener los datos del usuario con su rol
    $sentencia = $conexion->prepare("SELECT u.*, r.nombre_rol 
                                     FROM usuarios u 
                                     LEFT JOIN roles r ON u.rol_id = r.rol_id 
                                     WHERE u.user_id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    $usuario = $sentencia->fetch(PDO::FETCH_ASSOC);

    // Si no existe el usuario, redirigir al listado
    if (!$usuario) {
        header("Location:index.php?mensaje=" . urlencode("Usuario no encontrado"));
        exit();
    }

    // Variables para llenar el formulario con los datos del usuario
    $usuario_name = $usuario['usuario'];
    $email = $usuario['email'];
    $rol_id = $usuario['rol_id'];
} else {
    // Si no hay ID, redirigir al listado
    header("Location:index.php?mensaje=" . urlencode("ID de usuario no válido"));
    exit();
}

// Procesar el formulario de actualización
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Obtener los datos del formulario
    $usuario_name = trim($_POST['usuario']);
    $email = trim($_POST['email']);
    $new_password = $_POST['password'];
    $rol_id = (int)$_POST['rol_id'];

    // Validaciones básicas
    if (empty($usuario_name)) {
        $errores[] = "El nombre de usuario es obligatorio";
    }
    
    if (empty($email)) {
        $errores[] = "El correo electrónico es obligatorio";
    } elseif (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        $errores[] = "El correo electrónico no es válido";
    }

    // Validar contraseña solo si se proporciona una nueva
    if (!empty($new_password) && strlen($new_password) < 6) {
        $errores[] = "La contraseña debe tener al menos 6 caracteres";
    }

    // Verificar si el usuario o email ya existe (excluyendo el usuario actual)
    if (empty($errores)) {
        $verificar = $conexion->prepare("SELECT COUNT(*) FROM usuarios WHERE (usuario = :usuario OR email = :email) AND user_id != :id");
        $verificar->bindParam(":usuario", $usuario_name);
        $verificar->bindParam(":email", $email);
        $verificar->bindParam(":id", $txtID);
        $verificar->execute();
        
        if ($verificar->fetchColumn() > 0) {
            $errores[] = "El usuario o correo electrónico ya existe";
        }
    }

    // Si no hay errores, proceder con la actualización
    if (empty($errores)) {
        try {
            // Preparar la consulta de actualización
            if (!empty($new_password)) {
                // Si se proporciona nueva contraseña, cifrarla e incluirla en la actualización
                $hashed_password = password_hash($new_password, PASSWORD_DEFAULT);
                $sentencia_update = $conexion->prepare("UPDATE usuarios SET usuario = :usuario, password = :password, email = :email, rol_id = :rol_id WHERE user_id = :id");
                $sentencia_update->bindParam(":password", $hashed_password);
            } else {
                // Si no se proporciona nueva contraseña, no actualizarla
                $sentencia_update = $conexion->prepare("UPDATE usuarios SET usuario = :usuario, email = :email, rol_id = :rol_id WHERE user_id = :id");
            }
            
            // Asignar parámetros comunes
            $sentencia_update->bindParam(":usuario", $usuario_name);
            $sentencia_update->bindParam(":email", $email);
            $sentencia_update->bindParam(":rol_id", $rol_id);
            $sentencia_update->bindParam(":id", $txtID);
            
            // Ejecutar la actualización
            $sentencia_update->execute();

            // Redirigir con un mensaje de éxito
            $mensaje = "Usuario actualizado correctamente";
            header("Location:index.php?mensaje=" . urlencode($mensaje));
            exit();
        } catch (Exception $e) {
            $errores[] = "Error al actualizar el usuario: " . $e->getMessage();
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
                <i class="bi bi-person-gear"></i> Editar Usuario
            </h2>
            <p class="text-muted">Modifique los datos del usuario seleccionado</p>
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
                <div class="card-header bg-warning text-dark py-3">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-person-fill me-2"></i>
                        Información del Usuario
                    </h5>
                </div>
                
                <!-- Cuerpo de la tarjeta -->
                <div class="card-body p-4">
                    <!-- Información del ID (solo lectura) -->
                    <div class="alert alert-info mb-4">
                        <i class="bi bi-info-circle me-2"></i>
                        <strong>ID del Usuario:</strong> <?php echo htmlspecialchars($txtID); ?>
                    </div>

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
                                   value="<?php echo htmlspecialchars($usuario_name); ?>"
                                   required>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                El nombre de usuario debe ser único en el sistema
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
                                   value="<?php echo htmlspecialchars($email); ?>"
                                   required>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                Ingrese un correo electrónico válido
                            </div>
                        </div>

                        <!-- Campo Contraseña -->
                        <div class="mb-4">
                            <label for="password" class="form-label fw-semibold">
                                <i class="bi bi-lock me-1"></i>
                                Nueva Contraseña <span class="text-muted">(opcional)</span>
                            </label>
                            <div class="input-group">
                                <input type="password" 
                                       class="form-control form-control-lg" 
                                       name="password" 
                                       id="password" 
                                       placeholder="Dejar en blanco para mantener la actual"
                                       minlength="6">
                                <button class="btn btn-outline-secondary" type="button" id="togglePassword">
                                    <i class="bi bi-eye" id="eyeIcon"></i>
                                </button>
                            </div>
                            <div class="form-text">
                                <i class="bi bi-shield-check me-1"></i>
                                Deje en blanco si no desea cambiar la contraseña. Si ingresa una nueva, debe tener al menos 6 caracteres
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
                                            <?php echo ($rol_id == $rol['rol_id']) ? 'selected' : ''; ?>>
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
                            <button type="submit" class="btn btn-warning btn-lg text-dark">
                                <i class="bi bi-check-lg me-1"></i>
                                Actualizar Usuario
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