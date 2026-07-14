<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Procesar eliminación de productos si se recibe el parámetro txtID
if(isset($_GET['txtID'])){
    $txtID = (int)$_GET['txtID'];
    
    try {
        // Verificar si el producto tiene órdenes asociadas
        $verificar_ordenes = $conexion->prepare("SELECT COUNT(*) FROM order_items WHERE product_id = :id");
        $verificar_ordenes->bindParam(":id", $txtID);
        $verificar_ordenes->execute();
        $tiene_ordenes = $verificar_ordenes->fetchColumn();

        if ($tiene_ordenes > 0) {
            $error = "No se puede eliminar el producto porque tiene órdenes asociadas";
            header("Location:index.php?error=" . urlencode($error));
            exit();
        }

        // Obtener información del producto para eliminar la imagen
        $stmt_product = $conexion->prepare("SELECT foto FROM products WHERE product_id = :id");
        $stmt_product->bindParam(":id", $txtID);
        $stmt_product->execute();
        $producto = $stmt_product->fetch(PDO::FETCH_ASSOC);

        // Proceder con la eliminación
        $sentencia = $conexion->prepare("DELETE FROM products WHERE product_id = :id");
        $sentencia->bindParam(":id", $txtID);
        $sentencia->execute();
        
        // Eliminar imagen del servidor si es una imagen subida
        if ($producto && !empty($producto['foto']) && strpos($producto['foto'], 'uploads/products/') === 0) {
            $image_path = "uploads/products/ " . $producto['foto'];
            if (file_exists($image_path)) {
                unlink($image_path);
            }
        }
        
        $mensaje = "Producto eliminado correctamente";
        header("Location:index.php?mensaje=" . urlencode($mensaje));
        exit();
    } catch (Exception $e) {
        $error = "Error al eliminar el producto: " . $e->getMessage();
        header("Location:index.php?error=" . urlencode($error));
        exit();
    }
}

// Obtener parámetros de búsqueda y filtros
$busqueda = isset($_GET['busqueda']) ? trim($_GET['busqueda']) : '';
$filtro_year = isset($_GET['filtro_year']) ? (int)$_GET['filtro_year'] : 0;
$filtro_tag = isset($_GET['filtro_tag']) ? trim($_GET['filtro_tag']) : '';
$filtro_stock = isset($_GET['filtro_stock']) ? trim($_GET['filtro_stock']) : '';
$precio_min = isset($_GET['precio_min']) ? (float)$_GET['precio_min'] : 0;
$precio_max = isset($_GET['precio_max']) ? (float)$_GET['precio_max'] : 0;

// Construir la consulta base incluyendo las nuevas columnas
$sql = "SELECT product_id, product_name, description, foto, tags, model_year, price, stock 
        FROM products 
        WHERE 1=1";

$parametros = [];

// Aplicar filtro de búsqueda
if (!empty($busqueda)) {
    $sql .= " AND (product_name LIKE :busqueda OR description LIKE :busqueda)";
    $parametros[':busqueda'] = '%' . $busqueda . '%';
}

// Aplicar filtro de año
if ($filtro_year > 0) {
    $sql .= " AND model_year = :filtro_year";
    $parametros[':filtro_year'] = $filtro_year;
}

// Aplicar filtro de etiqueta
if (!empty($filtro_tag)) {
    $sql .= " AND (tags LIKE :filtro_tag OR tags IS NULL)";
    $parametros[':filtro_tag'] = '%' . $filtro_tag . '%';
}

// Aplicar filtro de stock
if (!empty($filtro_stock)) {
    switch($filtro_stock) {
        case 'sin_stock':
            $sql .= " AND (stock = 0 OR stock IS NULL)";
            break;
        case 'stock_bajo':
            $sql .= " AND stock > 0 AND stock <= 5";
            break;
        case 'stock_medio':
            $sql .= " AND stock > 5 AND stock <= 20";
            break;
        case 'stock_alto':
            $sql .= " AND stock > 20";
            break;
    }
}

// Aplicar filtro de precio mínimo
if ($precio_min > 0) {
    $sql .= " AND price >= :precio_min";
    $parametros[':precio_min'] = $precio_min;
}

// Aplicar filtro de precio máximo
if ($precio_max > 0) {
    $sql .= " AND price <= :precio_max";
    $parametros[':precio_max'] = $precio_max;
}

$sql .= " ORDER BY product_name ASC";

// Ejecutar consulta principal
$sentencia = $conexion->prepare($sql);
foreach ($parametros as $param => $valor) {
    $sentencia->bindValue($param, $valor);
}
$sentencia->execute();
$lista_products = $sentencia->fetchAll(PDO::FETCH_ASSOC);

// Obtener años únicos para el filtro
$years = $conexion->prepare("SELECT DISTINCT model_year FROM products WHERE model_year IS NOT NULL ORDER BY model_year DESC");
$years->execute();
$lista_years = $years->fetchAll(PDO::FETCH_ASSOC);

// Obtener estadísticas actualizadas
$stats = $conexion->prepare("SELECT 
    COUNT(*) as total_productos,
    AVG(price) as precio_promedio,
    MIN(price) as precio_minimo,
    MAX(price) as precio_maximo,
    COUNT(DISTINCT model_year) as total_años,
    SUM(stock) as stock_total,
    COUNT(CASE WHEN stock = 0 OR stock IS NULL THEN 1 END) as productos_sin_stock,
    COUNT(CASE WHEN stock > 0 AND stock <= 5 THEN 1 END) as productos_stock_bajo
    FROM products");
$stats->execute();
$estadisticas = $stats->fetch(PDO::FETCH_ASSOC);

// Etiquetas disponibles para filtro
$etiquetas_disponibles = [
    'nuevo' => 'Nuevo Producto',
    'oferta' => 'Oferta Especial',
    'ultimaUnidad' => 'Última Unidad',
    'oportunidad' => 'Oportunidad Única',
    'descuento' => 'Con Descuento',
    'popular' => 'Más Popular',
    'recomendado' => 'Recomendado',
    'limitado' => 'Edición Limitada'
];
?>

<?php include("../../templates/header.php"); ?>

<!-- Contenedor principal -->
<div class="container-fluid py-4">
    <!-- Título y descripción -->
    <div class="row mb-4">
        <div class="col-12">
            <h2 class="text-primary fw-bold mb-2">
                <i class="bi bi-box-seam-fill"></i> Gestión de Productos
            </h2>
            <p class="text-muted">Administre el catálogo de productos con control de inventario y etiquetas</p>
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

    <!-- Estadísticas mejoradas -->
    <div class="row mb-4">
        <div class="col-xl-2 col-md-4 col-6 mb-3">
            <div class="card bg-primary text-white h-100">
                <div class="card-body p-3">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title small">Total Productos</h6>
                            <h3 class="mb-0"><?php echo $estadisticas['total_productos']; ?></h3>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-box-seam-fill fs-2"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-2 col-md-4 col-6 mb-3">
            <div class="card bg-success text-white h-100">
                <div class="card-body p-3">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title small">Precio Promedio</h6>
                            <h4 class="mb-0">Bs. <?php echo number_format($estadisticas['precio_promedio'], 0); ?></h4>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-currency-dollar fs-2"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-2 col-md-4 col-6 mb-3">
            <div class="card bg-info text-white h-100">
                <div class="card-body p-3">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title small">Stock Total</h6>
                            <h3 class="mb-0"><?php echo number_format($estadisticas['stock_total']); ?></h3>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-boxes fs-2"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-2 col-md-4 col-6 mb-3">
            <div class="card bg-warning text-dark h-100">
                <div class="card-body p-3">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title small">Stock Bajo</h6>
                            <h3 class="mb-0"><?php echo $estadisticas['productos_stock_bajo']; ?></h3>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-exclamation-triangle fs-2"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-2 col-md-4 col-6 mb-3">
            <div class="card bg-danger text-white h-100">
                <div class="card-body p-3">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title small">Sin Stock</h6>
                            <h3 class="mb-0"><?php echo $estadisticas['productos_sin_stock']; ?></h3>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-x-circle fs-2"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-2 col-md-4 col-6 mb-3">
            <div class="card bg-secondary text-white h-100">
                <div class="card-body p-3">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h6 class="card-title small">Años Modelo</h6>
                            <h3 class="mb-0"><?php echo $estadisticas['total_años']; ?></h3>
                        </div>
                        <div class="align-self-center">
                            <i class="bi bi-calendar-event fs-2"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Tarjeta principal -->
    <div class="card shadow-sm border-0">
        <!-- Encabezado con botón de nuevo producto -->
        <div class="card-header bg-light py-3">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-list-ul me-2"></i>
                        Catálogo de Productos
                    </h5>
                </div>
                <div class="col-md-6 text-md-end">
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Nuevo Producto
                    </a>
                </div>
            </div>
        </div>

        <!-- Formulario de filtros mejorado -->
        <div class="card-body border-bottom">
            <form method="GET" class="row g-3">
                <div class="col-md-3">
                    <div class="input-group">
                        <span class="input-group-text">
                            <i class="bi bi-search"></i>
                        </span>
                        <input type="text" 
                               class="form-control" 
                               name="busqueda" 
                               placeholder="Buscar productos..."
                               value="<?php echo htmlspecialchars($busqueda); ?>">
                    </div>
                </div>
                <div class="col-md-2">
                    <select class="form-select" name="filtro_year">
                        <option value="0">Todos los años</option>
                        <?php foreach ($lista_years as $year): ?>
                            <option value="<?php echo $year['model_year']; ?>" 
                                    <?php echo ($filtro_year == $year['model_year']) ? 'selected' : ''; ?>>
                                <?php echo $year['model_year']; ?>
                            </option>
                        <?php endforeach; ?>
                    </select>
                </div>
                <div class="col-md-2">
                    <select class="form-select" name="filtro_tag">
                        <option value="">Todas las etiquetas</option>
                        <?php foreach ($etiquetas_disponibles as $key => $label): ?>
                            <option value="<?php echo $key; ?>" 
                                    <?php echo ($filtro_tag == $key) ? 'selected' : ''; ?>>
                                <?php echo $label; ?>
                            </option>
                        <?php endforeach; ?>
                    </select>
                </div>
                <div class="col-md-2">
                    <select class="form-select" name="filtro_stock">
                        <option value="">Todo el stock</option>
                        <option value="sin_stock" <?php echo ($filtro_stock == 'sin_stock') ? 'selected' : ''; ?>>Sin stock</option>
                        <option value="stock_bajo" <?php echo ($filtro_stock == 'stock_bajo') ? 'selected' : ''; ?>>Stock bajo (1-5)</option>
                        <option value="stock_medio" <?php echo ($filtro_stock == 'stock_medio') ? 'selected' : ''; ?>>Stock medio (6-20)</option>
                        <option value="stock_alto" <?php echo ($filtro_stock == 'stock_alto') ? 'selected' : ''; ?>>Stock alto (21+)</option>
                    </select>
                </div>
                <div class="col-md-1">
                    <input type="number" 
                           class="form-control" 
                           name="precio_min" 
                           placeholder="Min."
                           step="0.01"
                           value="<?php echo $precio_min > 0 ? $precio_min : ''; ?>">
                </div>
                <div class="col-md-1">
                    <input type="number" 
                           class="form-control" 
                           name="precio_max" 
                           placeholder="Max."
                           step="0.01"
                           value="<?php echo $precio_max > 0 ? $precio_max : ''; ?>">
                </div>
                <div class="col-md-1">
                    <div class="d-grid d-md-flex gap-1">
                        <button type="submit" class="btn btn-outline-primary btn-sm">
                            <i class="bi bi-funnel"></i>
                        </button>
                        <a href="index.php" class="btn btn-outline-secondary btn-sm">
                            <i class="bi bi-arrow-clockwise"></i>
                        </a>
                    </div>
                </div>
            </form>
        </div>

        <!-- Contenido de productos -->
        <div class="card-body p-0">
            <?php if (empty($lista_products)): ?>
                <div class="text-center py-5">
                    <i class="bi bi-inbox display-4 text-muted"></i>
                    <h5 class="text-muted mt-3">No se encontraron productos</h5>
                    <p class="text-muted">No hay productos que coincidan con los filtros aplicados.</p>
                    <a href="crear.php" class="btn btn-primary">
                        <i class="bi bi-plus-lg me-1"></i>
                        Agregar primer producto
                    </a>
                </div>
            <?php else: ?>
                <!-- Vista de tabla para pantallas grandes -->
                <div class="table-responsive d-none d-lg-block">
                    <table class="table table-hover mb-0">
                        <thead class="table-dark">
                            <tr>
                                <th scope="col" width="60">
                                    <i class="bi bi-hash me-1"></i>ID
                                </th>
                                <th scope="col" width="80">
                                    <i class="bi bi-image me-1"></i>Imagen
                                </th>
                                <th scope="col">
                                    <i class="bi bi-tag me-1"></i>Producto
                                </th>
                                <th scope="col" width="100">
                                    <i class="bi bi-calendar-event me-1"></i>Año
                                </th>
                                <th scope="col" width="120">
                                    <i class="bi bi-currency-dollar me-1"></i>Precio
                                </th>
                                <th scope="col" width="80">
                                    <i class="bi bi-boxes me-1"></i>Stock
                                </th>
                                <th scope="col" width="150">
                                    <i class="bi bi-tags me-1"></i>Etiquetas
                                </th>
                                <th scope="col" width="120" class="text-center">
                                    <i class="bi bi-gear me-1"></i>Acciones
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach($lista_products as $registro): ?>
                            <?php
                            // Procesar etiquetas
                            $tags_array = !empty($registro['tags']) ? explode(',', $registro['tags']) : [];
                            
                            // Determinar clase de stock
                            $stock = (int)$registro['stock'];
                            if ($stock == 0) {
                                $stock_class = 'bg-danger';
                                $stock_text = 'Sin stock';
                            } elseif ($stock <= 5) {
                                $stock_class = 'bg-warning text-dark';
                                $stock_text = 'Stock bajo';
                            } elseif ($stock <= 20) {
                                $stock_class = 'bg-info';
                                $stock_text = 'Stock medio';
                            } else {
                                $stock_class = 'bg-success';
                                $stock_text = 'Stock disponible';
                            }
                            ?>
                            <tr>
                                <td class="fw-semibold">
                                    <?php echo $registro['product_id']; ?>
                                </td>
                                <td>
                                    <img src="<?php echo htmlspecialchars($registro['foto']); ?>" 
                                         alt="<?php echo htmlspecialchars($registro['product_name']); ?>" 
                                         class="img-thumbnail"
                                         style="width: 60px; height: 60px; object-fit: cover; cursor: pointer;"
                                         onclick="showImageModal('<?php echo htmlspecialchars($registro['foto']); ?>', '<?php echo htmlspecialchars($registro['product_name']); ?>')"
                                         onerror="this.src='data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNjAiIGhlaWdodD0iNjAiIHZpZXdCb3g9IjAgMCA2MCA2MCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPHJlY3Qgd2lkdGg9IjYwIiBoZWlnaHQ9IjYwIiBmaWxsPSIjRjNGNEY2Ii8+CjxwYXRoIGQ9Ik0yMCAyMEg0MFY0MEgyMFYyMFoiIGZpbGw9IiNEMUQ1REIiLz4KPC9zdmc+';">
                                </td>
                                <td>
                                    <div>
                                        <strong><?php echo htmlspecialchars($registro['product_name']); ?></strong>
                                        <?php if (!empty($registro['description'])): ?>
                                            <br><small class="text-muted"><?php echo htmlspecialchars(substr($registro['description'], 0, 50)); ?><?php echo strlen($registro['description']) > 50 ? '...' : ''; ?></small>
                                        <?php endif; ?>
                                    </div>
                                </td>
                                <td>
                                    <span class="badge bg-secondary">
                                        <?php echo $registro['model_year']; ?>
                                    </span>
                                </td>
                                <td>
                                    <strong class="text-success">
                                        Bs. <?php echo number_format($registro['price'], 2); ?>
                                    </strong>
                                </td>
                                <td>
                                    <span class="badge <?php echo $stock_class; ?>" title="<?php echo $stock_text; ?>">
                                        <?php echo $stock; ?>
                                    </span>
                                </td>
                                <td>
                                    <?php if (!empty($tags_array)): ?>
                                        <?php foreach (array_slice($tags_array, 0, 2) as $tag): ?>
                                            <?php if (isset($etiquetas_disponibles[$tag])): ?>
                                                <span class="badge bg-primary me-1 mb-1 small"><?php echo $etiquetas_disponibles[$tag]; ?></span>
                                            <?php endif; ?>
                                        <?php endforeach; ?>
                                        <?php if (count($tags_array) > 2): ?>
                                            <span class="badge bg-light text-dark small">+<?php echo count($tags_array) - 2; ?></span>
                                        <?php endif; ?>
                                    <?php else: ?>
                                        <span class="text-muted small">Sin etiquetas</span>
                                    <?php endif; ?>
                                </td>
                                <td class="text-center">
                                    <div class="btn-group" role="group">
                                        <a href="editar.php?txtID=<?php echo $registro['product_id']; ?>" 
                                           class="btn btn-sm btn-outline-warning" 
                                           title="Editar producto">
                                            <i class="bi bi-pencil-square"></i>
                                        </a>
                                        <button type="button" 
                                                class="btn btn-sm btn-outline-info" 
                                                onclick="showProductModal(<?php echo htmlspecialchars(json_encode($registro)); ?>)"
                                                title="Ver detalles">
                                            <i class="bi bi-eye"></i>
                                        </button>
                                        <button type="button" 
                                                class="btn btn-sm btn-outline-danger" 
                                                onclick="confirmarEliminacion(<?php echo $registro['product_id']; ?>, '<?php echo htmlspecialchars($registro['product_name']); ?>')"
                                                title="Eliminar producto">
                                            <i class="bi bi-trash3"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <?php endforeach; ?>
                        </tbody>
                    </table>
                </div>

                <!-- Vista de cards para pantallas pequeñas -->
                <div class="d-lg-none p-3">
                    <div class="row g-3">
                        <?php foreach($lista_products as $registro): ?>
                        <?php
                        $tags_array = !empty($registro['tags']) ? explode(',', $registro['tags']) : [];
                        $stock = (int)$registro['stock'];
                        if ($stock == 0) {
                            $stock_class = 'bg-danger';
                        } elseif ($stock <= 5) {
                            $stock_class = 'bg-warning text-dark';
                        } elseif ($stock <= 20) {
                            $stock_class = 'bg-info';
                        } else {
                            $stock_class = 'bg-success';
                        }
                        ?>
                        <div class="col-12 col-sm-6">
                            <div class="card h-100">
                                <div class="row g-0">
                                    <div class="col-4">
                                        <img src="../../uploads/products/<? echo htmlspecialchars($registro['foto']); ?>" 
                                             alt="<?php echo htmlspecialchars($registro['product_name']); ?>" 
                                             class="img-fluid rounded-start h-100"
                                             style="object-fit: cover; cursor: pointer;"
                                             onclick="showImageModal('<?php echo htmlspecialchars($registro['foto']); ?>', '<?php echo htmlspecialchars($registro['product_name']); ?>')"
                                             onerror="this.src='data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMTAwIiBoZWlnaHQ9IjEwMCIgdmlld0JveD0iMCAwIDEwMCAxMDAiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+CjxyZWN0IHdpZHRoPSIxMDAiIGhlaWdodD0iMTAwIiBmaWxsPSIjRjNGNEY2Ii8+CjxwYXRoIGQ9Ik0zMCAzMEg3MFY3MEgzMFYzMFoiIGZpbGw9IiNEMUQ1REIiLz4KPC9zdmc+';">
                                    </div>
                                    <div class="col-8">
                                        <div class="card-body p-2">
                                            <h6 class="card-title"><?php echo htmlspecialchars($registro['product_name']); ?></h6>
                                            <p class="card-text small">
                                                <span class="text-muted">Año: <?php echo $registro['model_year']; ?></span><br>
                                                <strong class="text-success">Bs. <?php echo number_format($registro['price'], 2); ?></strong><br>
                                                <span class="badge <?php echo $stock_class; ?> small">Stock: <?php echo $stock; ?></span>
                                            </p>
                                            <?php if (!empty($tags_array)): ?>
                                                <div class="mb-2">
                                                    <?php foreach (array_slice($tags_array, 0, 2) as $tag): ?>
                                                        <?php if (isset($etiquetas_disponibles[$tag])): ?>
                                                            <span class="badge bg-primary me-1 small"><?php echo $etiquetas_disponibles[$tag]; ?></span>
                                                        <?php endif; ?>
                                                    <?php endforeach; ?>
                                                </div>
                                            <?php endif; ?>
                                            <div class="btn-group btn-group-sm w-100">
                                                <a href="editar.php?txtID=<?php echo $registro['product_id']; ?>" 
                                                   class="btn btn-outline-warning">
                                                    <i class="bi bi-pencil-square"></i>
                                                </a>
                                                <button type="button" 
                                                        class="btn btn-outline-info" 
                                                        onclick="showProductModal(<?php echo htmlspecialchars(json_encode($registro)); ?>)">
                                                    <i class="bi bi-eye"></i>
                                                </button>
                                                <button type="button" 
                                                        class="btn btn-outline-danger" 
                                                        onclick="confirmarEliminacion(<?php echo $registro['product_id']; ?>, '<?php echo htmlspecialchars($registro['product_name']); ?>')">
                                                    <i class="bi bi-trash3"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <?php endforeach; ?>
                    </div>
                </div>
            <?php endif; ?>
        </div>

        <!-- Footer con información adicional -->
        <div class="card-footer bg-light">
            <div class="d-flex justify-content-between align-items-center">
                <small class="text-muted">
                    <i class="bi bi-info-circle me-1"></i>
                    Mostrando <?php echo count($lista_products); ?> producto(s) - Stock total: <?php echo number_format($estadisticas['stock_total']); ?> unidades
                </small>
                <small class="text-muted">
                    <i class="bi bi-clock me-1"></i>
                    Última actualización: <?php echo date('d/m/Y H:i'); ?>
                </small>
            </div>
        </div>
    </div>
</div>

<!-- Modal para ver imagen en grande -->
<div class="modal fade" id="imageModal" tabindex="-1" aria-labelledby="imageModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="imageModalLabel">Imagen del Producto</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body text-center">
                <img id="modalImage" src="" alt="Imagen del producto" class="img-fluid rounded">
            </div>
        </div>
    </div>
</div>

<!-- Modal para ver detalles del producto -->
<div class="modal fade" id="productModal" tabindex="-1" aria-labelledby="productModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="productModalLabel">Detalles del Producto</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body" id="productModalBody">
                <!-- Contenido se carga dinámicamente -->
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                <a href="#" id="editProductBtn" class="btn btn-primary">Editar Producto</a>
            </div>
        </div>
    </div>
</div>

<!-- Estilos personalizados -->
<style>
.table-hover tbody tr:hover {
    background-color: rgba(13, 110, 253, 0.05);
}

.card:hover {
    transform: translateY(-2px);
    transition: transform 0.2s ease-in-out;
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
}

/* Mejorar la apariencia de las etiquetas */
.badge {
    font-size: 0.7rem;
}

.badge.small {
    font-size: 0.6rem;
}

/* Cursor pointer para imágenes */
img[onclick] {
    transition: transform 0.2s ease;
}

img[onclick]:hover {
    transform: scale(1.05);
}

/* Mejorar el aspecto de los botones de acción */
.btn-group .btn {
    border-radius: 0;
}

.btn-group .btn:first-child {
    border-top-left-radius: 0.375rem;
    border-bottom-left-radius: 0.375rem;
}

.btn-group .btn:last-child {
    border-top-right-radius: 0.375rem;
    border-bottom-right-radius: 0.375rem;
}

/* Responsive adjustments */
@media (max-width: 768px) {
    .card-body.p-3 h6.card-title.small {
        font-size: 0.8rem;
    }
    
    .card-body.p-3 h3,
    .card-body.p-3 h4 {
        font-size: 1.2rem;
    }
}

/* Animaciones */
.fade-in {
    animation: fadeIn 0.3s ease-in;
}

@keyframes fadeIn {
    from { opacity: 0; transform: translateY(-10px); }
    to { opacity: 1; transform: translateY(0); }
}

/* Modal improvements */
.modal-body img {
    max-height: 70vh;
    object-fit: contain;
}

#productModalBody {
    max-height: 70vh;
    overflow-y: auto;
}
</style>

<!-- Scripts -->
<script>
// Función para confirmar eliminación
function confirmarEliminacion(id, producto) {
    if (confirm(`¿Está seguro de que desea eliminar el producto "${producto}"?\n\nEsta acción eliminará también la imagen del servidor y no se puede deshacer.`)) {
        window.location.href = `index.php?txtID=${id}`;
    }
}

// Función para mostrar imagen en modal
function showImageModal(imageSrc, productName) {
    document.getElementById('modalImage').src = imageSrc;
    document.getElementById('imageModalLabel').textContent = productName;
    const imageModal = new bootstrap.Modal(document.getElementById('imageModal'));
    imageModal.show();
}

// Función para mostrar detalles del producto
function showProductModal(product) {
    const etiquetasDisponibles = <?php echo json_encode($etiquetas_disponibles); ?>;
    
    // Procesar etiquetas
    let tagsHtml = '';
    if (product.tags) {
        const tags = product.tags.split(',');
        tags.forEach(tag => {
            if (etiquetasDisponibles[tag]) {
                tagsHtml += `<span class="badge bg-primary me-1 mb-1">${etiquetasDisponibles[tag]}</span>`;
            }
        });
    }
    if (!tagsHtml) {
        tagsHtml = '<span class="text-muted">Sin etiquetas</span>';
    }
    
    // Determinar estado de stock
    let stockStatus = '';
    let stockClass = '';
    const stock = parseInt(product.stock) || 0;
    
    if (stock === 0) {
        stockStatus = 'Sin stock';
        stockClass = 'bg-danger';
    } else if (stock <= 5) {
        stockStatus = 'Stock bajo';
        stockClass = 'bg-warning text-dark';
    } else if (stock <= 20) {
        stockStatus = 'Stock medio';
        stockClass = 'bg-info';
    } else {
        stockStatus = 'Stock disponible';
        stockClass = 'bg-success';
    }
    
    // Crear contenido del modal
    const modalContent = `
        <div class="row">
            <div class="col-md-5">
                <img src="${product.foto}" alt="${product.product_name}" class="img-fluid rounded mb-3" style="max-height: 300px; object-fit: cover; width: 100%;"> 
            </div>
            <div class="col-md-7">
                <h4 class="text-primary">${product.product_name}</h4>
                <p class="text-muted mb-3">${product.description || 'Sin descripción disponible'}</p>
                
                <div class="row mb-3">
                    <div class="col-6">
                        <strong>ID:</strong> #${product.product_id}
                    </div>
                    <div class="col-6">
                        <strong>Año:</strong> <span class="badge bg-secondary">${product.model_year}</span>
                    </div>
                </div>
                
                <div class="row mb-3">
                    <div class="col-6">
                        <strong>Precio:</strong><br>
                        <span class="text-success fs-4">Bs. ${parseFloat(product.price).toLocaleString('es-BO', {minimumFractionDigits: 2})}</span>
                    </div>
                    <div class="col-6">
                        <strong>Stock:</strong><br>
                        <span class="badge ${stockClass} fs-6">${stock} unidades</span><br>
                        <small class="text-muted">${stockStatus}</small>
                    </div>
                </div>
                
                <div class="mb-3">
                    <strong>Etiquetas:</strong><br>
                    ${tagsHtml}
                </div>
                
                <div class="mb-3">
                    <strong>Valor en inventario:</strong><br>
                    <span class="text-info fs-5">Bs. ${(stock * parseFloat(product.price)).toLocaleString('es-BO', {minimumFractionDigits: 2})}</span>
                </div>
            </div>
        </div>
    `;
    
    document.getElementById('productModalBody').innerHTML = modalContent;
    document.getElementById('productModalLabel').textContent = `Producto #${product.product_id}`;
    document.getElementById('editProductBtn').href = `editar.php?txtID=${product.product_id}`;
    
    const productModal = new bootstrap.Modal(document.getElementById('productModal'));
    productModal.show();
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
    
    // Agregar animación fade-in a las cards
    const cards = document.querySelectorAll('.card');
    cards.forEach((card, index) => {
        setTimeout(() => {
            card.classList.add('fade-in');
        }, index * 50);
    });
});
</script>

<?php include("../../templates/footer.php"); ?>