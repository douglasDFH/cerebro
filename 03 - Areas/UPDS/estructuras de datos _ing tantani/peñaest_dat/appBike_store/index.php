<?php 
// Incluir archivo de conexión a la base de datos
include("bd.php");

// Obtener estadísticas generales del sistema
try {
    // Estadísticas de clientes
    $stmt_customers = $conexion->prepare("SELECT COUNT(*) as total_clientes FROM customers");
    $stmt_customers->execute();
    $stats_customers = $stmt_customers->fetch(PDO::FETCH_ASSOC);

    // Estadísticas de productos
    $stmt_products = $conexion->prepare("SELECT 
        COUNT(*) as total_productos,
        AVG(price) as precio_promedio,
        MIN(price) as precio_minimo,
        MAX(price) as precio_maximo
        FROM products");
    $stmt_products->execute();
    $stats_products = $stmt_products->fetch(PDO::FETCH_ASSOC);

    // Estadísticas de pedidos
    $stmt_orders = $conexion->prepare("SELECT 
        COUNT(*) as total_pedidos,
        SUM(oi.quantity * oi.price * (1 - oi.discount)) as ventas_totales
        FROM orders o
        LEFT JOIN order_items oi ON o.order_id = oi.order_id");
    $stmt_orders->execute();
    $stats_orders = $stmt_orders->fetch(PDO::FETCH_ASSOC);

    // Estadísticas de usuarios
    $stmt_usuarios = $conexion->prepare("SELECT 
        COUNT(*) as total_usuarios,
        SUM(CASE WHEN r.nombre_rol = 'administrador' THEN 1 ELSE 0 END) as total_admins
        FROM usuarios u 
        LEFT JOIN roles r ON u.rol_id = r.rol_id");
    $stmt_usuarios->execute();
    $stats_usuarios = $stmt_usuarios->fetch(PDO::FETCH_ASSOC);

    // Últimos clientes registrados
    $stmt_recent_customers = $conexion->prepare("SELECT customer_id, first_name, last_name, email, city 
                                                FROM customers 
                                                ORDER BY customer_id DESC 
                                                LIMIT 5");
    $stmt_recent_customers->execute();
    $recent_customers = $stmt_recent_customers->fetchAll(PDO::FETCH_ASSOC);

    // Productos más recientes
    $stmt_recent_products = $conexion->prepare("SELECT product_id, product_name, price, model_year 
                                               FROM products 
                                               ORDER BY product_id DESC 
                                               LIMIT 5");
    $stmt_recent_products->execute();
    $recent_products = $stmt_recent_products->fetchAll(PDO::FETCH_ASSOC);

    // Productos por año
    $stmt_products_year = $conexion->prepare("SELECT model_year, COUNT(*) as cantidad 
                                             FROM products 
                                             GROUP BY model_year 
                                             ORDER BY model_year DESC 
                                             LIMIT 5");
    $stmt_products_year->execute();
    $products_by_year = $stmt_products_year->fetchAll(PDO::FETCH_ASSOC);

    // Clientes por ciudad
    $stmt_customers_city = $conexion->prepare("SELECT city, COUNT(*) as cantidad 
                                              FROM customers 
                                              GROUP BY city 
                                              ORDER BY cantidad DESC 
                                              LIMIT 5");
    $stmt_customers_city->execute();
    $customers_by_city = $stmt_customers_city->fetchAll(PDO::FETCH_ASSOC);

} catch (Exception $e) {
    // En caso de error, inicializar variables con valores por defecto
    $stats_customers = ['total_clientes' => 0];
    $stats_products = ['total_productos' => 0, 'precio_promedio' => 0, 'precio_minimo' => 0, 'precio_maximo' => 0];
    $stats_orders = ['total_pedidos' => 0, 'ventas_totales' => 0];
    $stats_usuarios = ['total_usuarios' => 0, 'total_admins' => 0];
    $recent_customers = [];
    $recent_products = [];
    $products_by_year = [];
    $customers_by_city = [];
}
?>

<?php include("templates/header.php"); ?>

<!-- Contenedor principal del dashboard -->
<div class="container-fluid py-4">
    <!-- Header del dashboard -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="d-flex justify-content-between align-items-center">
                <div>
                    <h1 class="text-primary fw-bold mb-1">
                        <i class="bi bi-speedometer2"></i> Dashboard
                    </h1>
                    <p class="text-muted mb-0">Bienvenido al sistema de gestión Bike Store</p>
                </div>
                <div class="text-end">
                    <small class="text-muted">
                        <i class="bi bi-calendar-event me-1"></i>
                        <?php echo date('l, j \d\e F \d\e Y'); ?>
                    </small><br>
                    <small class="text-muted">
                        <i class="bi bi-clock me-1"></i>
                        <?php echo date('H:i'); ?>
                    </small>
                </div>
            </div>
        </div>
    </div>

    <!-- Tarjetas de estadísticas principales -->
    <div class="row mb-4">
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card bg-primary text-white shadow-sm h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h6 class="card-title text-uppercase fw-bold opacity-75">Clientes</h6>
                            <h2 class="mb-0 fw-bold"><?php echo number_format($stats_customers['total_clientes']); ?></h2>
                            <p class="card-text opacity-75 mb-0">
                                <small>Registrados en el sistema</small>
                            </p>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-people-fill display-4 opacity-25"></i>
                        </div>
                    </div>
                </div>
                <div class="card-footer bg-white bg-opacity-10">
                    <a href="secciones/customers/index.php" class="text-white text-decoration-none">
                        <i class="bi bi-arrow-right me-1"></i>
                        Ver todos los clientes
                    </a>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card bg-success text-white shadow-sm h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h6 class="card-title text-uppercase fw-bold opacity-75">Productos</h6>
                            <h2 class="mb-0 fw-bold"><?php echo number_format($stats_products['total_productos']); ?></h2>
                            <p class="card-text opacity-75 mb-0">
                                <small>En el catálogo</small>
                            </p>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-box-seam-fill display-4 opacity-25"></i>
                        </div>
                    </div>
                </div>
                <div class="card-footer bg-white bg-opacity-10">
                    <a href="secciones/products/index.php" class="text-white text-decoration-none">
                        <i class="bi bi-arrow-right me-1"></i>
                        Ver todos los productos
                    </a>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card bg-warning text-dark shadow-sm h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h6 class="card-title text-uppercase fw-bold opacity-75">Pedidos</h6>
                            <h2 class="mb-0 fw-bold"><?php echo number_format($stats_orders['total_pedidos']); ?></h2>
                            <p class="card-text opacity-75 mb-0">
                                <small>Procesados</small>
                            </p>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-cart-check-fill display-4 opacity-25"></i>
                        </div>
                    </div>
                </div>
                <div class="card-footer bg-white bg-opacity-25">
                    <a href="secciones/orders/index.php" class="text-dark text-decoration-none">
                        <i class="bi bi-arrow-right me-1"></i>
                        Ver todos los pedidos
                    </a>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card bg-info text-white shadow-sm h-100">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-center">
                        <div>
                            <h6 class="card-title text-uppercase fw-bold opacity-75">Ventas</h6>
                            <h2 class="mb-0 fw-bold">Bs. <?php echo number_format($stats_orders['ventas_totales'], 2); ?></h2>
                            <p class="card-text opacity-75 mb-0">
                                <small>Ingresos totales</small>
                            </p>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-currency-dollar display-4 opacity-25"></i>
                        </div>
                    </div>
                </div>
                <div class="card-footer bg-white bg-opacity-10">
                    <a href="secciones/orders/index.php" class="text-white text-decoration-none">
                        <i class="bi bi-arrow-right me-1"></i>
                        Ver reporte de ventas
                    </a>
                </div>
            </div>
        </div>
    </div>

    <!-- Información adicional del sistema -->
    <div class="row mb-4">
        <div class="col-md-6 mb-4">
            <div class="card shadow-sm h-100">
                <div class="card-header bg-light">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-graph-up text-success me-2"></i>
                        Información de Productos
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row text-center">
                        <div class="col-6">
                            <h4 class="text-success">Bs. <?php echo number_format($stats_products['precio_promedio'], 2); ?></h4>
                            <small class="text-muted">Precio Promedio</small>
                        </div>
                        <div class="col-6">
                            <h4 class="text-info">
                                Bs. <?php echo number_format($stats_products['precio_minimo'], 2); ?> - 
                                <?php echo number_format($stats_products['precio_maximo'], 2); ?>
                            </h4>
                            <small class="text-muted">Rango de Precios</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-6 mb-4">
            <div class="card shadow-sm h-100">
                <div class="card-header bg-light">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-shield-check text-danger me-2"></i>
                        Sistema de Usuarios
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row text-center">
                        <div class="col-6">
                            <h4 class="text-primary"><?php echo number_format($stats_usuarios['total_usuarios']); ?></h4>
                            <small class="text-muted">Total Usuarios</small>
                        </div>
                        <div class="col-6">
                            <h4 class="text-danger"><?php echo number_format($stats_usuarios['total_admins']); ?></h4>
                            <small class="text-muted">Administradores</small>
                        </div>
                    </div>
                    <div class="mt-3">
                        <a href="secciones/usuarios/index.php" class="btn btn-outline-primary btn-sm w-100">
                            <i class="bi bi-person-gear me-1"></i>
                            Gestionar Usuarios
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Sección de datos recientes y análisis -->
    <div class="row">
        <!-- Clientes recientes -->
        <div class="col-lg-6 mb-4">
            <div class="card shadow-sm h-100">
                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-person-plus text-primary me-2"></i>
                        Clientes Recientes
                    </h5>
                    <a href="secciones/customers/index.php" class="btn btn-sm btn-outline-primary">Ver todos</a>
                </div>
                <div class="card-body p-0">
                    <?php if (empty($recent_customers)): ?>
                        <div class="text-center py-4">
                            <i class="bi bi-person-x display-4 text-muted"></i>
                            <p class="text-muted mt-2">No hay clientes registrados</p>
                            <a href="secciones/customers/crear.php" class="btn btn-primary btn-sm">
                                <i class="bi bi-plus-lg me-1"></i>
                                Agregar primer cliente
                            </a>
                        </div>
                    <?php else: ?>
                        <div class="list-group list-group-flush">
                            <?php foreach ($recent_customers as $customer): ?>
                                <div class="list-group-item d-flex justify-content-between align-items-center">
                                    <div class="d-flex align-items-center">
                                        <div class="avatar-circle me-3">
                                            <i class="bi bi-person-fill"></i>
                                        </div>
                                        <div>
                                            <h6 class="mb-1"><?php echo htmlspecialchars($customer['first_name'] . ' ' . $customer['last_name']); ?></h6>
                                            <small class="text-muted"><?php echo htmlspecialchars($customer['email']); ?></small>
                                        </div>
                                    </div>
                                    <div class="text-end">
                                        <small class="text-muted"><?php echo htmlspecialchars($customer['city']); ?></small>
                                    </div>
                                </div>
                            <?php endforeach; ?>
                        </div>
                    <?php endif; ?>
                </div>
            </div>
        </div>

        <!-- Productos recientes -->
        <div class="col-lg-6 mb-4">
            <div class="card shadow-sm h-100">
                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-box-seam text-success me-2"></i>
                        Productos Recientes
                    </h5>
                    <a href="secciones/products/index.php" class="btn btn-sm btn-outline-success">Ver todos</a>
                </div>
                <div class="card-body p-0">
                    <?php if (empty($recent_products)): ?>
                        <div class="text-center py-4">
                            <i class="bi bi-box display-4 text-muted"></i>
                            <p class="text-muted mt-2">No hay productos registrados</p>
                            <a href="secciones/products/crear.php" class="btn btn-success btn-sm">
                                <i class="bi bi-plus-lg me-1"></i>
                                Agregar primer producto
                            </a>
                        </div>
                    <?php else: ?>
                        <div class="list-group list-group-flush">
                            <?php foreach ($recent_products as $product): ?>
                                <div class="list-group-item d-flex justify-content-between align-items-center">
                                    <div class="d-flex align-items-center">
                                        <div class="product-icon me-3">
                                            <i class="bi bi-box-seam text-success"></i>
                                        </div>
                                        <div>
                                            <h6 class="mb-1"><?php echo htmlspecialchars($product['product_name']); ?></h6>
                                            <small class="text-muted">Año: <?php echo $product['model_year']; ?></small>
                                        </div>
                                    </div>
                                    <div class="text-end">
                                        <strong class="text-success">Bs. <?php echo number_format($product['price'], 2); ?></strong>
                                    </div>
                                </div>
                            <?php endforeach; ?>
                        </div>
                    <?php endif; ?>
                </div>
            </div>
        </div>
    </div>

    <!-- Análisis por categorías -->
    <div class="row">
        <!-- Productos por año -->
      <!--  <div class="col-lg-6 mb-4">
            <div class="card shadow-sm">
                <div class="card-header bg-light">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-calendar-event text-info me-2"></i>
                        Productos por Año
                    </h5>
                </div>
                <div class="card-body">
                    <?php if (empty($products_by_year)): ?>
                        <p class="text-muted text-center">No hay datos disponibles</p>
                    <?php else: ?>
                        <?php foreach ($products_by_year as $item): ?>
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <span class="fw-semibold"><?php echo $item['model_year']; ?></span>
                                <div class="d-flex align-items-center">
                                    <div class="progress me-2" style="width: 100px; height: 20px;">
                                        <div class="progress-bar bg-info" 
                                             style="width: <?php echo ($item['cantidad'] / max(array_column($products_by_year, 'cantidad'))) * 100; ?>%"></div>
                                    </div>
                                    <span class="badge bg-info"><?php echo $item['cantidad']; ?></span>
                                </div>
                            </div>
                        <?php endforeach; ?>
                    <?php endif; ?>
                </div>
            </div>
        </div>-->

        <!-- Clientes por ciudad -->
       <!-- <div class="col-lg-6 mb-4">
            <div class="card shadow-sm">
                <div class="card-header bg-light">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-geo-alt text-warning me-2"></i>
                        Clientes por Ciudad
                    </h5>
                </div>
                <div class="card-body">
                    <?php if (empty($customers_by_city)): ?>
                        <p class="text-muted text-center">No hay datos disponibles</p>
                    <?php else: ?>
                        <?php foreach ($customers_by_city as $item): ?>
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <span class="fw-semibold"><?php echo htmlspecialchars($item['city']); ?></span>
                                <div class="d-flex align-items-center">
                                    <div class="progress me-2" style="width: 100px; height: 20px;">
                                        <div class="progress-bar bg-warning" 
                                             style="width: <?php echo ($item['cantidad'] / max(array_column($customers_by_city, 'cantidad'))) * 100; ?>%"></div>
                                    </div>
                                    <span class="badge bg-warning text-dark"><?php echo $item['cantidad']; ?></span>
                                </div>
                            </div>
                        <?php endforeach; ?>
                    <?php endif; ?>
                </div>
            </div>
        </div>
    </div>-->

    <!-- Enlaces rápidos -->
    <div class="row">
        <div class="col-12">
            <div class="card shadow-sm">
                <div class="card-header bg-primary text-white">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-lightning-charge me-2"></i>
                        Acciones Rápidas
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row g-3">
                        <div class="col-md-3">
                            <a href="secciones/customers/crear.php" class="btn btn-outline-primary w-100 py-3">
                                <i class="bi bi-person-plus display-6 d-block mb-2"></i>
                                Nuevo Cliente
                            </a>
                        </div>
                        <div class="col-md-3">
                            <a href="secciones/products/crear.php" class="btn btn-outline-success w-100 py-3">
                                <i class="bi bi-plus-square display-6 d-block mb-2"></i>
                                Nuevo Producto
                            </a>
                        </div>
                        <div class="col-md-3">
                            <a href="secciones/orders/crear.php" class="btn btn-outline-warning w-100 py-3">
                                <i class="bi bi-cart-plus display-6 d-block mb-2"></i>
                                Nuevo Pedido
                            </a>
                        </div>
                        <div class="col-md-3">
                            <a href="secciones/usuarios/crear.php" class="btn btn-outline-info w-100 py-3">
                                <i class="bi bi-person-gear display-6 d-block mb-2"></i>
                                Nuevo Usuario
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   </div>

<!-- Estilos personalizados -->
<style>
.avatar-circle {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    background: linear-gradient(45deg, #007bff, #0056b3);
    display: flex;
    align-items: center;
    justify-content: center;
    color: white;
    font-size: 16px;
}

.product-icon {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    background: linear-gradient(45deg, #28a745, #20c997);
    display: flex;
    align-items: center;
    justify-content: center;
    color: white;
    font-size: 18px;
}

.card:hover {
    transform: translateY(-2px);
    transition: transform 0.2s ease-in-out;
}

.progress {
    border-radius: 10px;
}

.progress-bar {
    border-radius: 10px;
}

@media (max-width: 768px) {
    .display-4 {
        font-size: 2rem;
    }
}
</style>

<!-- Script para actualización de tiempo en vivo -->
<script>
document.addEventListener('DOMContentLoaded', function() {
    // Actualizar hora cada minuto
    setInterval(function() {
        const now = new Date();
        const timeElements = document.querySelectorAll('[data-time]');
        timeElements.forEach(function(element) {
            element.textContent = now.toLocaleTimeString('es-ES', {
                hour: '2-digit',
                minute: '2-digit'
            });
        });
    }, 60000);
    
    // Animación de las tarjetas al cargar
    const cards = document.querySelectorAll('.card');
    cards.forEach(function(card, index) {
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';
        
        setTimeout(function() {
            card.style.transition = 'all 0.5s ease';
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }, index * 100);
    });
});
</script>

<?php include("templates/footer.php"); ?>