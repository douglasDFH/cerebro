<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Procesar eliminación de usuarios si se recibe el parámetro txtID
if(isset($_GET['txtID'])){
    $txtID = (int)$_GET['txtID'];
    
    try {
        // Verificar que no sea el último administrador
        $verificar_admin = $conexion->prepare("SELECT COUNT(*) FROM usuarios u 
                                               INNER JOIN roles r ON u.rol_id = r.rol_id 
                                               WHERE r.nombre_rol = 'administrador'");
        $verificar_admin->execute();
        $total_admins = $verificar_admin->fetchColumn();

        // Verificar si el usuario a eliminar es admin
        $es_admin = $conexion->prepare("SELECT COUNT(*) FROM usuarios u 
                                        INNER JOIN roles r ON u.rol_id = r.rol_id 
                                        WHERE u.user_id = :id AND r.nombre_rol = 'administrador'");
        $es_admin->bindParam(":id", $txtID);
        $es_admin->execute();
        $usuario_es_admin = $es_admin->fetchColumn();

        // Si es el último admin, no permitir eliminación
        if ($usuario_es_admin && $total_admins <= 1) {
            $mensaje = "No se puede eliminar el último administrador del sistema";
            header("Location:index.php?error=" . urlencode($mensaje));
            exit();
        }

        // Proceder con la eliminación
        $sentencia = $conexion->prepare("DELETE FROM usuarios WHERE user_id = :id");
        $sentencia->bindParam(":id", $txtID);
        $sentencia->execute();
        
        $mensaje = "Usuario eliminado correctamente";
        header("Location:index.php?mensaje=" . urlencode($mensaje));
        exit();
    } catch (Exception $e) {
        $error = "Error al eliminar el usuario: " . $e->getMessage();
        header("Location:index.php?error=" . urlencode($error));
        exit();
    }
}

// Obtener parámetros de búsqueda y filtros
$busqueda = isset($_GET['busqueda']) ? trim($_GET['busqueda']) : '';
$filtro_rol = isset($_GET['filtro_rol']) ? (int)$_GET['filtro_rol'] : 0;

// Construir la consulta base
$sql = "SELECT u.user_id, u.usuario, u.email, r.nombre_rol, r.descripcion as rol_descripcion 
        FROM usuarios u 
        LEFT JOIN roles r ON u.rol_id = r.rol_id 
        WHERE 1=1";

$parametros = [];

// Aplicar filtro de búsqueda
if (!empty($busqueda)) {
    $sql .= " AND (u.usuario LIKE :busqueda OR u.email LIKE :busqueda)";
    $parametros[':busqueda'] = '%' . $busqueda . '%';
}

// Aplicar filtro de rol
if ($filtro_rol > 0) {
    $sql .= " AND u.rol_id = :filtro_rol";
    $parametros[':filtro_rol'] = $filtro_rol;
}

$sql .= " ORDER BY u.usuario ASC";

// Ejecutar consulta principal
$sentencia = $conexion->prepare($sql);
foreach ($parametros as $param => $valor) {
    $sentencia->bindValue($param, $valor);
}
$sentencia->execute();
$lista_usuarios = $sentencia->fetchAll(PDO::FETCH_ASSOC);

// Obtener lista de roles para el filtro
$sentencia_roles = $conexion->prepare("SELECT rol_id, nombre_rol FROM roles WHERE activo = 1 ORDER BY nombre_rol");
$sentencia_roles->execute();
$lista_roles = $sentencia_roles->fetchAll(PDO::FETCH_ASSOC);

// Obtener estadísticas
$stats = $conexion->prepare("SELECT 
    COUNT(*) as total_usuarios,
    SUM(CASE WHEN r.nombre_rol = 'administrador' THEN 1 ELSE 0 END) as total_admins,
    SUM(CASE WHEN r.nombre_rol = 'usuario' THEN 1 ELSE 0 END) as total_usuarios_normales,
    SUM(CASE WHEN r.nombre_rol = 'moderador' THEN 1 ELSE 0 END) as total_moderadores
    FROM usuarios u 
    LEFT JOIN roles r ON u.rol_id = r.rol_id");
$stats->execute();
$estadisticas = $stats->fetch(PDO::FETCH_ASSOC);
?>

<?php include("../../templates/header.php"); ?>

<!-- Contenedor principal -->
<div class="container-fluid py-4">
    <!-- Título y descripción -->
    <div class="row mb-4">
        <div class="col-12">
            <h2 class="text-primary fw-bold mb-2">
                <i class="bi bi-people-fill"></i> Gestión de Usuarios
            </h2>
            <p class="text-muted">Administre los usuarios del sistema y sus roles</p>
        </div>
    </div>

    <!-- Mostrar mensajes -->
    <?php if (isset($_GET['mensaje'])): ?>
    <div class="row mb-4">
        <div class="col-12">
            <div class="alert alert-success alert-dismissible fade show" role="alert">
                <i class="bi bi-check-circle-fill me-2"></i>
                <?php echo htmlspecialchars($_GET['mensaje']); ?>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        </div>
    </div>
    <?php endif; ?>

    <?php if (isset($_GET['error'])): ?>
    <div class="row mb-4">
        <div class="col-12">
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                <?php echo htmlspecialchars($_GET['error']); ?>
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        </div>
    </div>
    <?php endif; ?>

    <!-- Estadísticas -->
    <div class="row mb-4">
        <div class="col-xl-3 col-md-6 mb-3">
            <div class="card bg-primary text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Total Usuarios</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_usuarios']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-people-fill fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6 mb-3">
            <div class="card bg-danger text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Administradores</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_admins']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-shield-check fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6 mb-3">
            <div class="card bg-warning text-dark h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Moderadores</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_moderadores']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-shield-fill-check fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6 mb-3">
            <div class="card bg-success text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Usuarios Normales</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_usuarios_normales']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-person-check fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Tarjeta principal -->
    <div class="card shadow-sm border-0">
        <!-- Encabezado con botón de nuevo usuario -->
        <div class="card-header bg-light py-3">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-list-ul me-2"></i>
                        Lista de Usuarios
                    </h5>
                </div>
                <div class="col-md-6 text-md-end">
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Nuevo Usuario
                    </a>
                </div>
            </div>
        </div>

        <!-- Formulario de filtros -->
        <div class="card-body border-bottom">
            <form method="GET" class="row g-3">
                <div class="col-md-5">
                    <div class="input-group">
                        <span class="input-group-text">
                            <i class="bi bi-search"></i>
                        </span>
                        <input type="text" 
                               class="form-control" 
                               name="busqueda" 
                               placeholder="Buscar por usuario o email..."
                               value="<?php echo htmlspecialchars($busqueda); ?>">
                    </div>
                </div>
                <div class="col-md-3">
                    <select class="form-select" name="filtro_rol">
                        <option value="0">Todos los roles</option>
                        <?php foreach ($lista_roles as $rol): ?>
                            <option value="<?php echo $rol['rol_id']; ?>" 
                                    <?php echo ($filtro_rol == $rol['rol_id']) ? 'selected' : ''; ?>>
                                <?php echo htmlspecialchars(ucfirst($rol['nombre_rol'])); ?>
                            </option>
                        <?php endforeach; ?>
                    </select>
                </div>
                <div class="col-md-4">
                    <div class="d-grid d-md-flex gap-2">
                        <button type="submit" class="btn btn-outline-primary">
                            <i class="bi bi-funnel me-1"></i>
                            Filtrar
                        </button>
                        <a href="index.php" class="btn btn-outline-secondary">
                            <i class="bi bi-arrow-clockwise me-1"></i>
                            Limpiar
                        </a>
                    </div>
                </div>
            </form>
        </div>

        <!-- Tabla de usuarios -->
        <div class="card-body p-0">
            <?php if (empty($lista_usuarios)): ?>
                <div class="text-center py-5">
                    <i class="bi bi-inbox display-4 text-muted"></i>
                    <h5 class="text-muted mt-3">No se encontraron usuarios</h5>
                    <p class="text-muted">No hay usuarios que coincidan con los filtros aplicados.</p>
                </div>
            <?php else: ?>
                <div class="table-responsive">
                    <table class="table table-hover mb-0">
                        <thead class="table-dark">
                            <tr>
                                <th scope="col" width="80">
                                    <i class="bi bi-hash me-1"></i>ID
                                </th>
                                <th scope="col">
                                    <i class="bi bi-person me-1"></i>Usuario
                                </th>
                                <th scope="col">
                                    <i class="bi bi-envelope me-1"></i>Email
                                </th>
                                <th scope="col" width="150">
                                    <i class="bi bi-shield-lock me-1"></i>Rol
                                </th>
                                <th scope="col" width="200" class="text-center">
                                    <i class="bi bi-gear me-1"></i>Acciones
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach($lista_usuarios as $registro): ?>
                            <tr>
                                <td class="fw-semibold">
                                    <?php echo $registro['user_id']; ?>
                                </td>
                                <td>
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-circle me-2">
                                            <i class="bi bi-person-fill"></i>
                                        </div>
                                        <strong><?php echo htmlspecialchars($registro['usuario']); ?></strong>
                                    </div>
                                </td>
                                <td>
                                    <a href="mailto:<?php echo htmlspecialchars($registro['email']); ?>" 
                                       class="text-decoration-none">
                                        <?php echo htmlspecialchars($registro['email']); ?>
                                    </a>
                                </td>
                                <td>
                                    <?php
                                    $rol_class = '';
                                    $rol_icon = '';
                                    switch(strtolower($registro['nombre_rol'])) {
                                        case 'administrador':
                                            $rol_class = 'bg-danger';
                                            $rol_icon = 'bi-shield-check';
                                            break;
                                        case 'moderador':
                                            $rol_class = 'bg-warning';
                                            $rol_icon = 'bi-shield-fill-check';
                                            break;
                                        default:
                                            $rol_class = 'bg-success';
                                            $rol_icon = 'bi-person-check';
                                    }
                                    ?>
                                    <span class="badge <?php echo $rol_class; ?> text-white">
                                        <i class="<?php echo $rol_icon; ?> me-1"></i>
                                        <?php echo htmlspecialchars(ucfirst($registro['nombre_rol'])); ?>
                                    </span>
                                </td>
                                <td class="text-center">
                                    <div class="btn-group" role="group">
                                        <a href="editar.php?txtID=<?php echo $registro['user_id']; ?>" 
                                           class="btn btn-sm btn-outline-warning" 
                                           title="Editar usuario">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                        <button type="button" 
                                                class="btn btn-sm btn-outline-danger" 
                                                onclick="confirmarEliminacion(<?php echo $registro['user_id']; ?>, '<?php echo htmlspecialchars($registro['usuario']); ?>')"
                                                title="Eliminar usuario">
                                            <i class="bi bi-trash3"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <?php endforeach; ?>
                        </tbody>
                    </table>
                </div>
            <?php endif; ?>
        </div>

        <!-- Footer con información adicional -->
        <div class="card-footer bg-light">
            <div class="d-flex justify-content-between align-items-center">
                <small class="text-muted">
                    <i class="bi bi-info-circle me-1"></i>
                    Mostrando <?php echo count($lista_usuarios); ?> usuario(s)
                </small>
                <small class="text-muted">
                    <i class="bi bi-clock me-1"></i>
                    Última actualización: <?php echo date('d/m/Y H:i'); ?>
                </small>
            </div>
        </div>
    </div>
</div>

<!-- Estilos personalizados -->
<style>
.avatar-circle {
    width: 35px;
    height: 35px;
    border-radius: 50%;
    background: linear-gradient(45deg, #007bff, #0056b3);
    display: flex;
    align-items: center;
    justify-content: center;
    color: white;
    font-size: 14px;
}

.table-hover tbody tr:hover {
    background-color: rgba(0, 123, 255, 0.05);
}

.badge {
    font-size: 0.75em;
    padding: 0.35em 0.65em;
}
</style>

<!-- Script para confirmar eliminación -->
<script>
function confirmarEliminacion(id, usuario) {
    if (confirm(`¿Está seguro de que desea eliminar al usuario "${usuario}"?\n\nEsta acción no se puede deshacer.`)) {
        window.location.href = `index.php?txtID=${id}`;
    }
}

// Auto-ocultar alertas después de 5 segundos
document.addEventListener('DOMContentLoaded', function() {
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(function(alert) {
        setTimeout(function() {
            const bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }, 5000);
    });
});
</script>

<?php include("../../templates/footer.php"); ?>