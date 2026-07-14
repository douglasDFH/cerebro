<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Procesar eliminación de clientes si se recibe el parámetro txtID
if(isset($_GET['txtID'])){
    $txtID = (int)$_GET['txtID'];
    
    try {
        // Verificar si el cliente tiene pedidos asociados
        $verificar_pedidos = $conexion->prepare("SELECT COUNT(*) FROM orders WHERE customer_id = :id");
        $verificar_pedidos->bindParam(":id", $txtID);
        $verificar_pedidos->execute();
        $tiene_pedidos = $verificar_pedidos->fetchColumn();

        if ($tiene_pedidos > 0) {
            $error = "No se puede eliminar el cliente porque tiene pedidos asociados";
            header("Location:index.php?error=" . urlencode($error));
            exit();
        }

        // Proceder con la eliminación
        $sentencia = $conexion->prepare("DELETE FROM customers WHERE customer_id = :id");
        $sentencia->bindParam(":id", $txtID);
        $sentencia->execute();
        
        $mensaje = "Cliente eliminado correctamente";
        header("Location:index.php?mensaje=" . urlencode($mensaje));
        exit();
    } catch (Exception $e) {
        $error = "Error al eliminar el cliente: " . $e->getMessage();
        header("Location:index.php?error=" . urlencode($error));
        exit();
    }
}

// Obtener parámetros de búsqueda y filtros
$busqueda = isset($_GET['busqueda']) ? trim($_GET['busqueda']) : '';
$filtro_city = isset($_GET['filtro_city']) ? trim($_GET['filtro_city']) : '';
$filtro_state = isset($_GET['filtro_state']) ? trim($_GET['filtro_state']) : '';

// Construir la consulta base
$sql = "SELECT customer_id, first_name, last_name, phone, email, street, city, state 
        FROM customers 
        WHERE 1=1";

$parametros = [];

// Aplicar filtro de búsqueda
if (!empty($busqueda)) {
    $sql .= " AND (first_name LIKE :busqueda OR last_name LIKE :busqueda OR email LIKE :busqueda OR phone LIKE :busqueda)";
    $parametros[':busqueda'] = '%' . $busqueda . '%';
}

// Aplicar filtro de ciudad
if (!empty($filtro_city)) {
    $sql .= " AND city = :filtro_city";
    $parametros[':filtro_city'] = $filtro_city;
}

// Aplicar filtro de departamento
if (!empty($filtro_state)) {
    $sql .= " AND state = :filtro_state";
    $parametros[':filtro_state'] = $filtro_state;
}

$sql .= " ORDER BY first_name ASC, last_name ASC";

// Ejecutar consulta principal
$sentencia = $conexion->prepare($sql);
foreach ($parametros as $param => $valor) {
    $sentencia->bindValue($param, $valor);
}
$sentencia->execute();
$lista_clientes = $sentencia->fetchAll(PDO::FETCH_ASSOC);

// Obtener listas únicas para los filtros
$ciudades = $conexion->prepare("SELECT DISTINCT city FROM customers WHERE city IS NOT NULL AND city != '' ORDER BY city");
$ciudades->execute();
$lista_ciudades = $ciudades->fetchAll(PDO::FETCH_ASSOC);

$departamentos = $conexion->prepare("SELECT DISTINCT state FROM customers WHERE state IS NOT NULL AND state != '' ORDER BY state");
$departamentos->execute();
$lista_departamentos = $departamentos->fetchAll(PDO::FETCH_ASSOC);

// Obtener estadísticas
$stats = $conexion->prepare("SELECT 
    COUNT(*) as total_clientes,
    COUNT(DISTINCT city) as total_ciudades,
    COUNT(DISTINCT state) as total_departamentos
    FROM customers");
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
                <i class="bi bi-people-fill"></i> Gestión de Clientes
            </h2>
            <p class="text-muted">Administre la información de los clientes registrados en el sistema</p>
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
        <div class="col-xl-4 col-md-6 mb-3">
            <div class="card bg-primary text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Total Clientes</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_clientes']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-people-fill fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-4 col-md-6 mb-3">
            <div class="card bg-success text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Ciudades</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_ciudades']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-building fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-4 col-md-6 mb-3">
            <div class="card bg-info text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Departamentos</h6>
                            <h2 class="mb-0"><?php echo $estadisticas['total_departamentos']; ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-geo-alt-fill fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Tarjeta principal -->
    <div class="card shadow-sm border-0">
        <!-- Encabezado con botón de nuevo cliente -->
        <div class="card-header bg-light py-3">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-list-ul me-2"></i>
                        Lista de Clientes
                    </h5>
                </div>
                <div class="col-md-6 text-md-end">
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Nuevo Cliente
                    </a>
                </div>
            </div>
        </div>

        <!-- Formulario de filtros -->
        <div class="card-body border-bottom">
            <form method="GET" class="row g-3">
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-text">
                            <i class="bi bi-search"></i>
                        </span>
                        <input type="text" 
                               class="form-control" 
                               name="busqueda" 
                               placeholder="Buscar por nombre, email o teléfono..."
                               value="<?php echo htmlspecialchars($busqueda); ?>">
                    </div>
                </div>
                <div class="col-md-3">
                    <select class="form-select" name="filtro_city">
                        <option value="">Todas las ciudades</option>
                        <?php foreach ($lista_ciudades as $ciudad): ?>
                            <option value="<?php echo htmlspecialchars($ciudad['city']); ?>" 
                                    <?php echo ($filtro_city == $ciudad['city']) ? 'selected' : ''; ?>>
                                <?php echo htmlspecialchars($ciudad['city']); ?>
                            </option>
                        <?php endforeach; ?>
                    </select>
                </div>
                <div class="col-md-3">
                    <select class="form-select" name="filtro_state">
                        <option value="">Todos los departamentos</option>
                        <?php foreach ($lista_departamentos as $departamento): ?>
                            <option value="<?php echo htmlspecialchars($departamento['state']); ?>" 
                                    <?php echo ($filtro_state == $departamento['state']) ? 'selected' : ''; ?>>
                                <?php echo htmlspecialchars($departamento['state']); ?>
                            </option>
                        <?php endforeach; ?>
                    </select>
                </div>
                <div class="col-md-2">
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

        <!-- Tabla de clientes -->
        <div class="card-body p-0">
            <?php if (empty($lista_clientes)): ?>
                <div class="text-center py-5">
                    <i class="bi bi-inbox display-4 text-muted"></i>
                    <h5 class="text-muted mt-3">No se encontraron clientes</h5>
                    <p class="text-muted">No hay clientes que coincidan con los filtros aplicados.</p>
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Agregar primer cliente
                    </a>
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
                                    <i class="bi bi-person me-1"></i>Cliente
                                </th>
                                <th scope="col">
                                    <i class="bi bi-telephone me-1"></i>Teléfono
                                </th>
                                <th scope="col">
                                    <i class="bi bi-envelope me-1"></i>Email
                                </th>
                                <th scope="col">
                                    <i class="bi bi-house me-1"></i>Dirección
                                </th>
                                <th scope="col">
                                    <i class="bi bi-geo-alt me-1"></i>Ubicación
                                </th>
                                <th scope="col" width="200" class="text-center">
                                    <i class="bi bi-gear me-1"></i>Acciones
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach($lista_clientes as $registro): ?>
                            <tr>
                                <td class="fw-semibold">
                                    <?php echo $registro['customer_id']; ?>
                                </td>
                                <td>
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-circle me-2">
                                            <i class="bi bi-person-fill"></i>
                                        </div>
                                        <div>
                                            <strong><?php echo htmlspecialchars($registro['first_name'] . ' ' . $registro['last_name']); ?></strong>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <a href="tel:<?php echo htmlspecialchars($registro['phone']); ?>" 
                                       class="text-decoration-none">
                                        <i class="bi bi-telephone me-1"></i>
                                        <?php echo htmlspecialchars($registro['phone']); ?>
                                    </a>
                                </td>
                                <td>
                                    <a href="mailto:<?php echo htmlspecialchars($registro['email']); ?>" 
                                       class="text-decoration-none">
                                        <?php echo htmlspecialchars($registro['email']); ?>
                                    </a>
                                </td>
                                <td>
                                    <small class="text-muted">
                                        <?php echo htmlspecialchars($registro['street']); ?>
                                    </small>
                                </td>
                                <td>
                                    <div>
                                        <small><strong><?php echo htmlspecialchars($registro['city']); ?></strong></small>
                                        <br>
                                        <small class="text-muted"><?php echo htmlspecialchars($registro['state']); ?></small>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <div class="btn-group" role="group">
                                        <a href="editar.php?txtID=<?php echo $registro['customer_id']; ?>" 
                                           class="btn btn-sm btn-outline-warning" 
                                           title="Editar cliente">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                        <button type="button" 
                                                class="btn btn-sm btn-outline-danger" 
                                                onclick="confirmarEliminacion(<?php echo $registro['customer_id']; ?>, '<?php echo htmlspecialchars($registro['first_name'] . ' ' . $registro['last_name']); ?>')"
                                                title="Eliminar cliente">
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
                    Mostrando <?php echo count($lista_clientes); ?> cliente(s)
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
    background: linear-gradient(45deg, #28a745, #20c997);
    display: flex;
    align-items: center;
    justify-content: center;
    color: white;
    font-size: 14px;
}

.table-hover tbody tr:hover {
    background-color: rgba(40, 167, 69, 0.05);
}

.badge {
    font-size: 0.75em;
    padding: 0.35em 0.65em;
}
</style>

<!-- Script para confirmar eliminación -->
<script>
function confirmarEliminacion(id, cliente) {
    if (confirm(`¿Está seguro de que desea eliminar al cliente "${cliente}"?\n\nEsta acción no se puede deshacer.`)) {
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