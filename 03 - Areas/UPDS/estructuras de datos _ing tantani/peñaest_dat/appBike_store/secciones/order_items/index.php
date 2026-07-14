<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Obtener el ID del pedido
$order_id = isset($_GET['order_id']) ? (int)$_GET['order_id'] : 0;

if ($order_id <= 0) {
    header("Location:../orders/index.php?error=" . urlencode("ID de pedido no válido"));
    exit();
}

// Procesar eliminación de items si se recibe el parámetro txtID
if(isset($_GET['txtID'])){
    $txtID = (int)$_GET['txtID'];
    
    try {
        // Verificar que el item pertenece al pedido especificado
        $verificar = $conexion->prepare("SELECT COUNT(*) FROM order_items WHERE order_item_id = :id AND order_id = :order_id");
        $verificar->bindParam(":id", $txtID);
        $verificar->bindParam(":order_id", $order_id);
        $verificar->execute();
        
        if ($verificar->fetchColumn() == 0) {
            $error = "El item especificado no pertenece a este pedido";
            header("Location:index.php?order_id=$order_id&error=" . urlencode($error));
            exit();
        }

        // Eliminar el item
        $sentencia = $conexion->prepare("DELETE FROM order_items WHERE order_item_id = :id");
        $sentencia->bindParam(":id", $txtID);
        $sentencia->execute();
        
        $mensaje = "Item eliminado correctamente del pedido";
        header("Location:index.php?order_id=$order_id&mensaje=" . urlencode($mensaje));
        exit();
    } catch (Exception $e) {
        $error = "Error al eliminar el item: " . $e->getMessage();
        header("Location:index.php?order_id=$order_id&error=" . urlencode($error));
        exit();
    }
}

// Obtener información del pedido y cliente
$stmt_order = $conexion->prepare("SELECT o.order_id, o.order_date, 
                                 c.customer_id, c.first_name, c.last_name, c.email, c.phone, c.city, c.state, c.street
                                 FROM orders o 
                                 INNER JOIN customers c ON o.customer_id = c.customer_id 
                                 WHERE o.order_id = :order_id");
$stmt_order->bindParam(':order_id', $order_id);
$stmt_order->execute();
$pedido_info = $stmt_order->fetch(PDO::FETCH_ASSOC);

if (!$pedido_info) {
    header("Location:../orders/index.php?error=" . urlencode("Pedido no encontrado"));
    exit();
}

// Obtener items del pedido con información del producto
$stmt_items = $conexion->prepare("SELECT oi.order_item_id, oi.product_id, oi.quantity, oi.price, oi.discount,
                                 p.product_name, p.foto, p.model_year,
                                 (oi.quantity * oi.price) as subtotal,
                                 (oi.quantity * oi.price * oi.discount) as descuento_total,
                                 (oi.quantity * oi.price * (1 - oi.discount)) as total_item
                                 FROM order_items oi 
                                 INNER JOIN products p ON oi.product_id = p.product_id 
                                 WHERE oi.order_id = :order_id 
                                 ORDER BY oi.order_item_id");
$stmt_items->bindParam(':order_id', $order_id);
$stmt_items->execute();
$items_pedido = $stmt_items->fetchAll(PDO::FETCH_ASSOC);

// Calcular totales del pedido
$total_items = count($items_pedido);
$total_cantidad = 0;
$subtotal_pedido = 0;
$descuento_total_pedido = 0;
$total_pedido = 0;

foreach ($items_pedido as $item) {
    $total_cantidad += $item['quantity'];
    $subtotal_pedido += $item['subtotal'];
    $descuento_total_pedido += $item['descuento_total'];
    $total_pedido += $item['total_item'];
}
?>

<?php include("../../templates/header.php"); ?>

<!-- Contenedor principal -->
<div class="container-fluid py-4">
    <!-- Título y navegación -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="d-flex justify-content-between align-items-center">
                <div>
                    <h2 class="text-primary fw-bold mb-1">
                        <i class="bi bi-receipt"></i> Detalles del Pedido #<?php echo str_pad($order_id, 4, '0', STR_PAD_LEFT); ?>
                    </h2>
                    <p class="text-muted mb-0">Información completa y items del pedido</p>
                </div>
                <div>
                    <a href="../orders/index.php" class="btn btn-outline-secondary">
                        <i class="bi bi-arrow-left me-1"></i>
                        Volver a Pedidos
                    </a>
                </div>
            </div>
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

    <!-- Información del pedido y cliente -->
    <div class="row mb-4">
        <!-- Información del cliente -->
        <div class="col-lg-6 mb-4">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-header bg-primary text-white">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-person-circle me-2"></i>
                        Información del Cliente
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <h5 class="fw-bold text-primary">
                                <?php echo htmlspecialchars($pedido_info['first_name'] . ' ' . $pedido_info['last_name']); ?>
                            </h5>
                            <div class="mb-2">
                                <i class="bi bi-envelope text-muted me-2"></i>
                                <a href="mailto:<?php echo htmlspecialchars($pedido_info['email']); ?>" class="text-decoration-none">
                                    <?php echo htmlspecialchars($pedido_info['email']); ?>
                                </a>
                            </div>
                            <div class="mb-2">
                                <i class="bi bi-telephone text-muted me-2"></i>
                                <a href="tel:<?php echo htmlspecialchars($pedido_info['phone']); ?>" class="text-decoration-none">
                                    <?php echo htmlspecialchars($pedido_info['phone']); ?>
                                </a>
                            </div>
                            <div class="mb-2">
                                <i class="bi bi-house text-muted me-2"></i>
                                <?php echo htmlspecialchars($pedido_info['street']); ?>
                            </div>
                            <div class="mb-2">
                                <i class="bi bi-geo-alt text-muted me-2"></i>
                                <?php echo htmlspecialchars($pedido_info['city'] . ', ' . $pedido_info['state']); ?>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Resumen del pedido -->
        <div class="col-lg-6 mb-4">
            <div class="card shadow-sm border-0 h-100">
                <div class="card-header bg-success text-white">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-calculator me-2"></i>
                        Resumen del Pedido
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row text-center mb-3">
                        <div class="col-4">
                            <div class="bg-light rounded p-3">
                                <h4 class="text-primary mb-1"><?php echo $total_items; ?></h4>
                                <small class="text-muted">Items</small>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="bg-light rounded p-3">
                                <h4 class="text-info mb-1"><?php echo number_format($total_cantidad, 2); ?></h4>
                                <small class="text-muted">Cantidad Total</small>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="bg-light rounded p-3">
                                <h4 class="text-warning mb-1"><?php echo date('d/m/Y', strtotime($pedido_info['order_date'])); ?></h4>
                                <small class="text-muted">Fecha</small>
                            </div>
                        </div>
                    </div>
                    
                    <div class="border rounded p-3">
                        <div class="d-flex justify-content-between mb-2">
                            <span>Subtotal:</span>
                            <span>Bs. <?php echo number_format($subtotal_pedido, 2); ?></span>
                        </div>
                        <div class="d-flex justify-content-between mb-2">
                            <span>Descuento:</span>
                            <span class="text-danger">-Bs. <?php echo number_format($descuento_total_pedido, 2); ?></span>
                        </div>
                        <hr>
                        <div class="d-flex justify-content-between fw-bold fs-5">
                            <span>Total:</span>
                            <span class="text-success">Bs. <?php echo number_format($total_pedido, 2); ?></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Items del pedido -->
    <div class="card shadow-sm border-0">
        <div class="card-header bg-light py-3">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-list-ul me-2"></i>
                        Items del Pedido
                    </h5>
                </div>
                <div class="col-md-6 text-md-end">
                    <a href="exportar_pdf.php?order_id=<?php echo $order_id; ?>" class="btn btn-danger btn-sm me-2" target="_blank">
    <i class="bi bi-file-earmark-pdf me-1"></i>
    Exportar a PDF
</a>
                </div>
            </div>
        </div>

        <div class="card-body p-0">
            <?php if (empty($items_pedido)): ?>
                <div class="text-center py-5">
                    <i class="bi bi-cart-x display-4 text-muted"></i>
                    <h5 class="text-muted mt-3">No hay items en este pedido</h5>
                    <p class="text-muted">Agregue productos a este pedido para comenzar.</p>
                    <a href="crear.php?order_id=<?php echo $order_id; ?>" class="btn btn-success">
                        <i class="bi bi-plus-lg me-1"></i>
                        Agregar primer item
                    </a>
                </div>
            <?php else: ?>
                <div class="table-responsive">
                    <table class="table table-hover mb-0">
                        <thead class="table-dark">
                            <tr>
                                <th scope="col" width="60">
                                    <i class="bi bi-hash me-1"></i>ID
                                </th>
                                <th scope="col" width="80">
                                    <i class="bi bi-image me-1"></i>Foto
                                </th>
                                <th scope="col">
                                    <i class="bi bi-box-seam me-1"></i>Producto
                                </th>
                                <th scope="col" width="100">
                                    <i class="bi bi-123 me-1"></i>Cantidad
                                </th>
                                <th scope="col" width="120">
                                    <i class="bi bi-currency-dollar me-1"></i>Precio Unit.
                                </th>
                                <th scope="col" width="100">
                                    <i class="bi bi-percent me-1"></i>Descuento
                                </th>
                                <th scope="col" width="120">
                                    <i class="bi bi-calculator me-1"></i>Subtotal
                                </th>
                                <th scope="col" width="120">
                                    <i class="bi bi-cash me-1"></i>Total
                                </th>
                                <th scope="col" width="120" class="text-center">
                                    <i class="bi bi-gear me-1"></i>estado
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach($items_pedido as $item): ?>
                            <tr>
                                <td class="fw-semibold">
                                    #<?php echo $item['order_item_id']; ?>
                                </td>
                                <td>
                                    <img src="<?php echo htmlspecialchars($item['foto']); ?>" 
                                         alt="<?php echo htmlspecialchars($item['product_name']); ?>" 
                                         class="img-thumbnail"
                                         style="width: 50px; height: 50px; object-fit: cover;"
                                         onerror="this.src='data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNTAiIGhlaWdodD0iNTAiIHZpZXdCb3g9IjAgMCA1MCA1MCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPHJlY3Qgd2lkdGg9IjUwIiBoZWlnaHQ9IjUwIiBmaWxsPSIjRjNGNEY2Ii8+CjxwYXRoIGQ9Ik0xNSAxNUgzNVYzNUgxNVYxNVoiIGZpbGw9IiNEMUQ1REIiLz4KPC9zdmc+';">
                                </td>
                                <td>
                                    <div>
                                        <strong><?php echo htmlspecialchars($item['product_name']); ?></strong>
                                        <br>
                                        <small class="text-muted">Año: <?php echo $item['model_year']; ?></small>
                                    </div>
                                </td>
                                <td class="text-center">
                                    <span class="badge bg-info fs-6">
                                        <?php echo number_format($item['quantity'], 2); ?>
                                    </span>
                                </td>
                                <td>
                                    <strong>Bs. <?php echo number_format($item['price'], 2); ?></strong>
                                </td>
                                <td class="text-center">
                                    <?php if ($item['discount'] > 0): ?>
                                        <span class="badge bg-warning text-dark">
                                            <?php echo number_format($item['discount'] * 100, 1); ?>%
                                        </span>
                                    <?php else: ?>
                                        <span class="text-muted">-</span>
                                    <?php endif; ?>
                                </td>
                                <td>
                                    <span class="text-muted">Bs. <?php echo number_format($item['subtotal'], 2); ?></span>
                                </td>
                                <td>
                                    <strong class="text-success">Bs. <?php echo number_format($item['total_item'], 2); ?></strong>
                                </td>
                                <td class="text-center">
                                    <div class="btn-group" role="group">
                                       <!-- <a href="editar.php?txtID=<?php echo $item['order_item_id']; ?>&order_id=<?php echo $order_id; ?>" 
                                           class="btn btn-sm btn-outline-warning" 
                                           title="Editar item">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                        <button type="button" 
                                                class="btn btn-sm btn-outline-danger" 
                                                onclick="confirmarEliminacion(<?php echo $item['order_item_id']; ?>, '<?php echo htmlspecialchars($item['product_name']); ?>')"
                                                title="Elimi">
                                            <i class="bi bi-trash3"></i>
                                        </button>-->
                                    </div>
                                </td>
                            </tr>
                            <?php endforeach; ?>
                        </tbody>
                    </table>
                </div>
            <?php endif; ?>
        </div>

        <!-- Footer con totales -->
        <?php if (!empty($items_pedido)): ?>
        <div class="card-footer bg-light">
            <div class="row">
                <div class="col-md-8">
                    <small class="text-muted">
                        <i class="bi bi-info-circle me-1"></i>
                        <?php echo $total_items; ?> item(s) - <?php echo number_format($total_cantidad, 2); ?> unidades totales
                    </small>
                </div>
                <div class="col-md-4 text-md-end">
                    <strong class="text-success fs-5">
                        Total: Bs. <?php echo number_format($total_pedido, 2); ?>
                    </strong>
                </div>
            </div>
        </div>
        <?php endif; ?>
    </div>
</div>

<!-- Estilos personalizados -->
<style>
.table-hover tbody tr:hover {
    background-color: rgba(0, 123, 255, 0.05);
}

.card {
    transition: transform 0.2s ease-in-out;
}

.card:hover {
    transform: translateY(-2px);
}
</style>

<!-- Script para confirmar eliminación -->
<script>
/*function confirmarEliminacion(id, producto) {
    if (confirm(`¿Está seguro de que desea eliminar "${producto}" del pedido?\n\nEsta acción no se puede deshacer.`)) {
        window.location.href = `index.php?order_id=<?php echo $order_id; ?>&txtID=${id}`;
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