<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Procesar eliminación de pedidos si se recibe el parámetro txtID
/* if(isset($_GET['txtID'])){
    $txtID = (int)$_GET['txtID'];
    
    try {
        // Iniciar transacción para eliminar pedido y sus elementos
        $conexion->beginTransaction();
        
        // Eliminar elementos del pedido primero
        $stmt_items = $conexion->prepare("DELETE FROM order_items WHERE order_id = :id");
        $stmt_items->bindParam(":id", $txtID);
        $stmt_items->execute();
        
        // Luego eliminar el pedido
        $stmt_order = $conexion->prepare("DELETE FROM orders WHERE order_id = :id");
        $stmt_order->bindParam(":id", $txtID);
        $stmt_order->execute();
        
        // Confirmar transacción
        $conexion->commit();
        
        $mensaje = "Pedido eliminado correctamente";
        header("Location:index.php?mensaje=" . urlencode($mensaje));
        exit();
    } catch (Exception $e) {
        // Revertir transacción en caso de error
        $conexion->rollBack();
        $error = "Error al eliminar el pedido: " . $e->getMessage();
        header("Location:index.php?error=" . urlencode($error));
        exit();
    }
}*/

// Obtener parámetros de búsqueda y filtros
$busqueda = isset($_GET['busqueda']) ? trim($_GET['busqueda']) : '';
$filtro_fecha = isset($_GET['filtro_fecha']) ? trim($_GET['filtro_fecha']) : '';

// Construir la consulta base con información del cliente y totales
$sql = "SELECT 
    o.order_id,
    o.order_date,
    c.customer_id,
    CONCAT(c.first_name, ' ', c.last_name) as cliente_nombre,
    c.email,
    c.city,
    COUNT(oi.order_item_id) as total_items,
    COALESCE(SUM(oi.quantity * oi.price * (1 - oi.discount)), 0) as total_pedido
    FROM orders o
    INNER JOIN customers c ON o.customer_id = c.customer_id
    LEFT JOIN order_items oi ON o.order_id = oi.order_id
    WHERE 1=1";

$parametros = [];

// Aplicar filtro de búsqueda (cliente)
if (!empty($busqueda)) {
    $sql .= " AND (c.first_name LIKE :busqueda OR c.last_name LIKE :busqueda OR c.email LIKE :busqueda)";
    $parametros[':busqueda'] = '%' . $busqueda . '%';
}

// Aplicar filtro de fecha
if (!empty($filtro_fecha)) {
    $sql .= " AND DATE(o.order_date) = :filtro_fecha";
    $parametros[':filtro_fecha'] = $filtro_fecha;
}

$sql .= " GROUP BY o.order_id, o.order_date, c.customer_id, c.first_name, c.last_name, c.email, c.city
          ORDER BY o.order_date DESC, o.order_id DESC";

// Ejecutar consulta principal
$sentencia = $conexion->prepare($sql);
foreach ($parametros as $param => $valor) {
    $sentencia->bindValue($param, $valor);
}
$sentencia->execute();
$lista_pedidos = $sentencia->fetchAll(PDO::FETCH_ASSOC);

// Obtener estadísticas
$stats = $conexion->prepare("SELECT 
    COUNT(DISTINCT o.order_id) as total_pedidos,
    COUNT(DISTINCT o.customer_id) as clientes_unicos,
    COALESCE(SUM(oi.quantity * oi.price * (1 - oi.discount)), 0) as ventas_totales,
    COALESCE(AVG(oi.quantity * oi.price * (1 - oi.discount)), 0) as promedio_pedido
    FROM orders o
    LEFT JOIN order_items oi ON o.order_id = oi.order_id");
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
                <i class="bi bi-cart-check-fill"></i> Gestión de Pedidos
            </h2>
            <p class="text-muted">Administre todos los pedidos realizados en el sistema</p>
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
                            <h6 class="card-title">Total Pedidos</h6>
                            <h2 class="mb-0"><?php echo number_format($estadisticas['total_pedidos']); ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-cart-check-fill fs-1"></i>
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
                            <h6 class="card-title">Ventas Totales</h6>
                            <h2 class="mb-0">Bs. <?php echo number_format($estadisticas['ventas_totales'], 2); ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-currency-dollar fs-1"></i>
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
                            <h6 class="card-title">Promedio por Pedido</h6>
                            <h2 class="mb-0">Bs. <?php echo number_format($estadisticas['promedio_pedido'], 2); ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-graph-up fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6 mb-3">
            <div class="card bg-info text-white h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title">Clientes Únicos</h6>
                            <h2 class="mb-0"><?php echo number_format($estadisticas['clientes_unicos']); ?></h2>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-people-fill fs-1"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Tarjeta principal -->
    <div class="card shadow-sm border-0">
        <!-- Encabezado con botón de nuevo pedido -->
        <div class="card-header bg-light py-3">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-list-ul me-2"></i>
                        Lista de Pedidos
                    </h5>
                </div>
                <div class="col-md-6 text-md-end">
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Nuevo Pedido
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
                               placeholder="Buscar por cliente..."
                               value="<?php echo htmlspecialchars($busqueda); ?>">
                    </div>
                </div>
                <div class="col-md-3">
                    <input type="date" 
                           class="form-control" 
                           name="filtro_fecha" 
                           value="<?php echo htmlspecialchars($filtro_fecha); ?>">
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

        <!-- Tabla de pedidos -->
        <div class="card-body p-0">
            <?php if (empty($lista_pedidos)): ?>
                <div class="text-center py-5">
                    <i class="bi bi-cart-x display-4 text-muted"></i>
                    <h5 class="text-muted mt-3">No se encontraron pedidos</h5>
                    <p class="text-muted">No hay pedidos que coincidan con los filtros aplicados.</p>
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Crear primer pedido
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
                                <th scope="col" width="150">
                                    <i class="bi bi-calendar-event me-1"></i>Fecha
                                </th>
                                <th scope="col" width="100">
                                    <i class="bi bi-box me-1"></i>Items
                                </th>
                                <th scope="col" width="150">
                                    <i class="bi bi-currency-dollar me-1"></i>Total
                                </th>
                                <th scope="col" width="200" class="text-center">
                                    <i class="bi bi-gear me-1"></i>Acciones
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach($lista_pedidos as $pedido): ?>
                            <tr>
                                <td class="fw-semibold">
                                    #<?php echo str_pad($pedido['order_id'], 4, '0', STR_PAD_LEFT); ?>
                                </td>
                                <td>
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-circle me-2">
                                            <i class="bi bi-person-fill"></i>
                                        </div>
                                        <div>
                                            <strong><?php echo htmlspecialchars($pedido['cliente_nombre']); ?></strong>
                                            <br>
                                            <small class="text-muted"><?php echo htmlspecialchars($pedido['email']); ?></small>
                                            <br>
                                            <small class="text-muted">
                                                <i class="bi bi-geo-alt me-1"></i>
                                                <?php echo htmlspecialchars($pedido['city']); ?>
                                            </small>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <strong><?php echo date('d/m/Y', strtotime($pedido['order_date'])); ?></strong>
                                        <br>
                                        <small class="text-muted"><?php echo date('H:i', strtotime($pedido['order_date'])); ?></small>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <span class="badge bg-primary fs-6">
                                        <?php echo $pedido['total_items']; ?>
                                    </span>
                                </td>
                                <td>
                                    <strong class="text-success fs-5">
                                        Bs. <?php echo number_format($pedido['total_pedido'], 2); ?>
                                    </strong>
                                </td>
                                <td class="text-center">
                                    <div class="btn-group" role="group">
                                        <a href="../order_items/index.php?order_id=<?php echo $pedido['order_id']; ?>" 
                                           class="btn btn-sm btn-outline-info" 
                                           title="Ver detalles">
                                            <i class="bi bi-eye"></i>
                                        </a>
                                        <a href="editar.php?txtID=<?php echo $pedido['order_id']; ?>" 
                                           class="btn btn-sm btn-outline-warning" 
                                           title="Editar pedido">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                        <button type="button" 
                                                class="btn btn-sm btn-outline-danger" 
                                                onclick="confirmarEliminacion(<?php echo $pedido['order_id']; ?>, '<?php echo htmlspecialchars($pedido['cliente_nombre']); ?>')"
                                                title="inactivo">
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
                    Mostrando <?php echo count($lista_pedidos); ?> pedido(s)
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
</style>

<!-- Script para confirmar eliminación -->
<script>
/*function confirmarEliminacion(id, cliente) {
    if (confirm(`¿Está seguro de que desea eliminar el pedido #${String(id).padStart(4, '0')} del cliente "${cliente}"?\n\nEsta acción eliminará también todos los elementos del pedido y no se puede deshacer.`)) {
        window.location.href = `index.php?txtID=${id}`;
    }
}*/

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