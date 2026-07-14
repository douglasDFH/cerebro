

<?php 

// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Variables para almacenar los datos del producto
$txtID = "";
$product_name = "";
$description = "";
$foto = "";
$tags = "";
$model_year = "";
$price = "";
$stock = "";
$errores = [];

// Configuración para subida de archivos
$upload_dir = "../../uploads/products/";
$allowed_extensions = ['jpg', 'jpeg', 'png', 'gif', 'webp', 'bmp'];
$max_file_size = 5 * 1024 * 1024; // 5MB máximo


// Verificar si se ha recibido el parámetro 'txtID' por URL
if(isset($_GET['txtID'])){
    $txtID = (int)$_GET['txtID'];
    
    // Consultar los datos del producto
    $sentencia = $conexion->prepare("SELECT * FROM products WHERE product_id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    $producto = $sentencia->fetch(PDO::FETCH_ASSOC);

    // Si el producto no existe, redirigir al listado de productos
    if (!$producto) {
        header("Location:index.php?mensaje=" . urlencode("Producto no encontrado"));
        exit();
    }

    // Obtener los datos del producto para el formulario
    $product_name = $producto['product_name'];
    $description = $producto['description'] ?? '';
    $foto = $producto['foto'];
    $tags = $producto['tags'] ?? '';
    $model_year = $producto['model_year'];
    $price = $producto['price'];
    $stock = $producto['stock'] ?? 0;
    
    // Convertir tags string a array
    $tags_array = !empty($tags) ? explode(',', $tags) : [];
} else {
    // Si no hay ID, redirigir al listado
    header("Location:index.php?mensaje=" . urlencode("ID de producto no válido"));
    exit();
}

// Actualizar el producto cuando el formulario se envíe
if ($_POST) {
    // Recolectar y validar los datos del formulario
    $product_name = trim($_POST['product_name']);
    $description = trim($_POST['description']);
    $model_year = (int)$_POST['model_year'];
    $price = (float)$_POST['price'];
    $stock = (int)$_POST['stock'];
    $tags_post = isset($_POST['tags']) ? $_POST['tags'] : [];
    
    // Convertir array de etiquetas a string
    $tags_string = !empty($tags_post) ? implode(',', $tags_post) : '';

    // Validaciones básicas
    if (empty($product_name)) {
        $errores[] = "El nombre del producto es obligatorio";
    } elseif (strlen($product_name) < 3) {
        $errores[] = "El nombre del producto debe tener al menos 3 caracteres";
    } elseif (strlen($product_name) > 200) {
        $errores[] = "El nombre del producto no puede exceder 200 caracteres";
    }
    
    if (!empty($description) && strlen($description) > 1000) {
        $errores[] = "La descripción no puede exceder 1000 caracteres";
    }
    
    if (empty($model_year) || $model_year < 1900 || $model_year > (date('Y') + 2)) {
        $errores[] = "El año del modelo debe estar entre 1900 y " . (date('Y') + 2);
    }
    
    if ($price <= 0) {
        $errores[] = "El precio debe ser mayor a 0";
    } elseif ($price > 999999.99) {
        $errores[] = "El precio no puede exceder 999,999.99";
    }
    
    if ($stock < 0) {
        $errores[] = "La cantidad en stock no puede ser negativa";
    } elseif ($stock > 99999) {
        $errores[] = "La cantidad en stock no puede exceder 99,999 unidades";
    }

    // Verificar si el nombre del producto ya existe (excluyendo el producto actual)
    if (empty($errores)) {
        $verificar_producto = $conexion->prepare("SELECT COUNT(*) FROM products WHERE product_name = :product_name AND product_id != :id");
        $verificar_producto->bindParam(":product_name", $product_name);
        $verificar_producto->bindParam(":id", $txtID);
        $verificar_producto->execute();
        
        if ($verificar_producto->fetchColumn() > 0) {
            $errores[] = "Ya existe otro producto con ese nombre";
        }
    }

    // Procesar la subida de imagen si se seleccionó una nueva
    $nueva_foto = null;
    if (isset($_FILES['foto']) && $_FILES['foto']['error'] !== UPLOAD_ERR_NO_FILE) {
        if ($_FILES['foto']['error'] === UPLOAD_ERR_OK) {
            $file_tmp = $_FILES['foto']['tmp_name'];
            $file_name = $_FILES['foto']['name'];
            $file_size = $_FILES['foto']['size'];
            $file_ext = strtolower(pathinfo($file_name, PATHINFO_EXTENSION));
            
            // Validar extensión
            if (!in_array($file_ext, $allowed_extensions)) {
                $errores[] = "Formato de imagen no válido. Formatos permitidos: " . implode(', ', $allowed_extensions);
            }
            
            // Validar tamaño
            if ($file_size > $max_file_size) {
                $errores[] = "El archivo es demasiado grande. Tamaño máximo: 5MB";
            }
            
            // Generar nombre único para el archivo
            if (empty($errores)) {
                $unique_name = uniqid('product_', true) . '.' . $file_ext;
                $upload_path = $upload_dir . $unique_name;
                
                if (move_uploaded_file($file_tmp, $upload_path)) {
                    $nueva_foto = "uploads/products/" . $unique_name;
                    
                    // Eliminar imagen anterior si existe y no es una URL externa
                    if (!empty($foto) && strpos($foto, 'uploads/products/') === 0) {
                        $old_file = "../../" . $foto;
                        if (file_exists($old_file)) {
                            unlink($old_file);
                        }
                    }
                } else {
                    $errores[] = "Error al subir la nueva imagen";
                }
            }
        } else {
            $errores[] = "Error en la subida del archivo: " . $_FILES['foto']['error'];
        }
    }

    // Si no hay errores, proceder con la actualización
    if (empty($errores)) {
        try {
            // Usar la nueva foto si se subió una, sino mantener la actual
            $foto_final = $nueva_foto ? $nueva_foto : $foto;
            
            // Preparar la consulta SQL para actualizar el producto
            $sentencia = $conexion->prepare("UPDATE products SET 
                                            product_name = :product_name,
                                            description = :description, 
                                            foto = :foto,
                                            tags = :tags, 
                                            model_year = :model_year, 
                                            price = :price,
                                            stock = :stock 
                                            WHERE product_id = :id");
            $sentencia->bindParam(":product_name", $product_name);
            $sentencia->bindParam(":description", $description);
            $sentencia->bindParam(":foto", $foto_final);
            $sentencia->bindParam(":tags", $tags_string);
            $sentencia->bindParam(":model_year", $model_year);
            $sentencia->bindParam(":price", $price);
            $sentencia->bindParam(":stock", $stock);
            $sentencia->bindParam(":id", $txtID);

            // Ejecutar la consulta de actualización
            $sentencia->execute();

            $mensaje = "Producto actualizado correctamente - ID #" . $txtID;
            header("Location:index.php?mensaje=" . urlencode($mensaje));
            exit();
        } catch (Exception $e) {
            $errores[] = "Error al actualizar el producto: " . $e->getMessage();
        }
    }
}

// Etiquetas predefinidas disponibles
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

<!-- Contenedor principal con espaciado -->
<div class="container-fluid py-4">
    <!-- Título de la página -->
    <div class="row mb-4">
        <div class="col-12">
            <h2 class="text-primary fw-bold">
                <i class="bi bi-pencil-square"></i> Editar Producto
            </h2>
            <p class="text-muted">Modifique los datos del producto #<?php echo str_pad($txtID, 4, '0', STR_PAD_LEFT); ?></p>
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

    <!-- Información actual del producto -->
    <div class="row mb-4">
        <div class="col-12">
            <div class="alert alert-warning">
                <i class="bi bi-box-seam me-2"></i>
                <strong>Producto Actual:</strong> <?php echo htmlspecialchars($product_name); ?> - 
                <strong>Año:</strong> <?php echo $model_year; ?> - 
                <strong>Precio:</strong> Bs. <?php echo number_format($price, 2); ?> -
                <strong>Stock:</strong> <?php echo $stock; ?> unidades
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
                        <i class="bi bi-box-seam me-2"></i>
                        Información del Producto
                    </h5>
                </div>
                
                <!-- Cuerpo de la tarjeta -->
                <div class="card-body p-4">
                    <!-- Información del ID (solo lectura) -->
                    <div class="alert alert-info mb-4">
                        <i class="bi bi-info-circle me-2"></i>
                        <strong>ID del Producto:</strong> <?php echo htmlspecialchars($txtID); ?>
                        <span class="ms-3">
                            <strong>Creado:</strong> <?php echo date('Y'); ?>
                        </span>
                    </div>

                    <form action="" method="post" enctype="multipart/form-data" novalidate>
                        <div class="row">
                            <!-- Columna izquierda -->
                            <div class="col-md-6">
                                <!-- Campo Nombre del Producto -->
                                <div class="mb-4">
                                    <label for="product_name" class="form-label fw-semibold">
                                        <i class="bi bi-tag me-1"></i>
                                        Nombre del Producto <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" 
                                           class="form-control form-control-lg" 
                                           name="product_name" 
                                           id="product_name" 
                                           placeholder="Ej: Bicicleta Mountain Bike Pro"
                                           value="<?php echo htmlspecialchars($product_name); ?>"
                                           maxlength="200"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Modifique el nombre descriptivo del producto (3-200 caracteres)
                                    </div>
                                </div>

                                <!-- Campo Descripción -->
                                <div class="mb-4">
                                    <label for="description" class="form-label fw-semibold">
                                        <i class="bi bi-text-paragraph me-1"></i>
                                        Descripción <span class="text-muted">(opcional)</span>
                                    </label>
                                    <textarea class="form-control" 
                                              name="description" 
                                              id="description" 
                                              rows="4"
                                              placeholder="Descripción detallada del producto..."
                                              maxlength="1000"><?php echo htmlspecialchars($description); ?></textarea>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Descripción detallada del producto (máximo 1000 caracteres)
                                        <span id="char-count" class="float-end text-muted"><?php echo strlen($description); ?>/1000</span>
                                    </div>
                                </div>

                                <!-- Campo Imagen -->
                                <div class="mb-4">
                                    <label for="foto" class="form-label fw-semibold">
                                        <i class="bi bi-image me-1"></i>
                                        Imagen del Producto
                                    </label>
                                    <input type="file" 
                                           class="form-control form-control-lg" 
                                           name="foto" 
                                           id="foto" 
                                           accept="image/*">
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Seleccione una nueva imagen para reemplazar la actual (JPG, PNG, GIF, WEBP, BMP - máximo 5MB)
                                    </div>
                                    
                                    <!-- Imagen actual -->
                                    <div id="current-image" class="mt-3">
                                        <p class="small text-muted mb-2">Imagen actual:</p>
                                        <img src="<?php echo htmlspecialchars($foto); ?>" 
                                             alt="Imagen actual del producto" 
                                             class="img-thumbnail" 
                                             style="max-width: 300px; max-height: 200px;"
                                             onerror="this.style.display='none'; this.nextElementSibling.style.display='block';">
                                        <div style="display: none;" class="alert alert-warning mt-2">
                                            <i class="bi bi-exclamation-triangle me-1"></i>
                                            No se pudo cargar la imagen actual
                                        </div>
                                    </div>
                                    
                                    <!-- Vista previa de la nueva imagen -->
                                    <div id="image-preview" class="mt-3" style="display: none;">
                                        <p class="small text-muted mb-2">Nueva imagen:</p>
                                        <img id="preview-img" src="" alt="Vista previa" class="img-thumbnail" style="max-width: 300px; max-height: 200px;">
                                        <button type="button" class="btn btn-sm btn-outline-danger ms-2" onclick="clearImagePreview()">
                                            <i class="bi bi-x-lg"></i> Quitar
                                        </button>
                                    </div>
                                </div>

                                <!-- Campo Año del Modelo -->
                                <div class="mb-4">
                                    <label for="model_year" class="form-label fw-semibold">
                                        <i class="bi bi-calendar-event me-1"></i>
                                        Año del Modelo <span class="text-danger">*</span>
                                    </label>
                                    <input type="number" 
                                           class="form-control form-control-lg" 
                                           name="model_year" 
                                           id="model_year" 
                                           placeholder="<?php echo date('Y'); ?>"
                                           value="<?php echo htmlspecialchars($model_year); ?>"
                                           min="1900" 
                                           max="<?php echo date('Y') + 2; ?>"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Año del modelo del producto (1900 - <?php echo date('Y') + 2; ?>)
                                    </div>
                                </div>
                            </div>

                            <!-- Columna derecha -->
                            <div class="col-md-6">
                                <!-- Campo Precio -->
                                <div class="mb-4">
                                    <label for="price" class="form-label fw-semibold">
                                        <i class="bi bi-currency-dollar me-1"></i>
                                        Precio <span class="text-danger">*</span>
                                    </label>
                                    <div class="input-group input-group-lg">
                                        <span class="input-group-text">Bs.</span>  
                                        <input type="number" 
                                               step="0.01" 
                                               class="form-control" 
                                               name="price" 
                                               id="price" 
                                               placeholder="0.00"
                                               value="<?php echo htmlspecialchars($price); ?>"
                                               min="0.01"
                                               max="999999.99"
                                               required>
                                    </div>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Precio del producto en bolivianos (máximo 999,999.99)
                                    </div>
                                </div>

                                <!-- Campo Stock/Cantidad -->
                                <div class="mb-4">
                                    <label for="stock" class="form-label fw-semibold">
                                        <i class="bi bi-boxes me-1"></i>
                                        Cantidad en Stock <span class="text-danger">*</span>
                                    </label>
                                    <div class="input-group input-group-lg">
                                        <input type="number" 
                                               class="form-control" 
                                               name="stock" 
                                               id="stock" 
                                               placeholder="0"
                                               value="<?php echo htmlspecialchars($stock); ?>"
                                               min="0"
                                               max="99999"
                                               required>
                                        <span class="input-group-text">unidades</span>
                                    </div>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Cantidad disponible en inventario (0-99,999 unidades)
                                    </div>
                                    
                                    <!-- Indicador visual de stock -->
                                    <div id="stock-indicator" class="mt-2">
                                        <span id="stock-status" class="badge bg-secondary">Stock actual</span>
                                    </div>
                                </div>

                                <!-- Campo Etiquetas -->
                                <div class="mb-4">
                                    <label class="form-label fw-semibold">
                                        <i class="bi bi-tags me-1"></i>
                                        Etiquetas del Producto <span class="text-muted">(opcional)</span>
                                    </label>
                                    <div class="row g-2">
                                        <?php foreach ($etiquetas_disponibles as $key => $label): ?>
                                        <div class="col-md-6">
                                            <div class="form-check">
                                                <input class="form-check-input" 
                                                       type="checkbox" 
                                                       name="tags[]" 
                                                       value="<?php echo $key; ?>" 
                                                       id="tag_<?php echo $key; ?>"
                                                       <?php echo in_array($key, $tags_array) ? 'checked' : ''; ?>>
                                                <label class="form-check-label" for="tag_<?php echo $key; ?>">
                                                    <?php echo htmlspecialchars($label); ?>
                                                </label>
                                            </div>
                                        </div>
                                        <?php endforeach; ?>
                                    </div>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Seleccione las etiquetas que describan mejor su producto
                                    </div>
                                    
                                    <!-- Vista previa de etiquetas seleccionadas -->
                                    <div id="tags-preview" class="mt-2">
                                        <small class="text-muted">Etiquetas seleccionadas:</small>
                                        <div id="selected-tags" class="mt-1">
                                            <!-- Las etiquetas se mostrarán aquí dinámicamente -->
                                        </div>
                                    </div>
                                    
                                    <!-- Mostrar etiquetas actuales -->
                                    <?php if (!empty($tags_array)): ?>
                                    <div class="mt-2">
                                        <small class="text-info">
                                            <i class="bi bi-bookmark me-1"></i>
                                            <strong>Etiquetas actuales:</strong>
                                            <?php foreach ($tags_array as $tag): ?>
                                                <?php if (isset($etiquetas_disponibles[$tag])): ?>
                                                    <span class="badge bg-info text-dark me-1"><?php echo $etiquetas_disponibles[$tag]; ?></span>
                                                <?php endif; ?>
                                            <?php endforeach; ?>
                                        </small>
                                    </div>
                                    <?php endif; ?>
                                </div>

                                <!-- Resumen del producto -->
                                <div class="mb-4">
                                    <div class="card bg-light">
                                        <div class="card-body p-3">
                                            <h6 class="fw-bold text-muted mb-2">
                                                <i class="bi bi-eye me-1"></i>
                                                Vista Previa de Cambios
                                            </h6>
                                            <div id="product-summary" class="small text-muted">
                                                <div id="summary-name">Nombre: <span class="fw-semibold"><?php echo htmlspecialchars($product_name); ?></span></div>
                                                <div id="summary-year">Año: <span class="fw-semibold"><?php echo $model_year; ?></span></div>
                                                <div id="summary-price">Precio: <span class="fw-semibold">Bs. <?php echo number_format($price, 2); ?></span></div>
                                                <div id="summary-stock">Stock: <span class="fw-semibold"><?php echo $stock; ?> unidades</span></div>
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
                                        Actualizar Producto
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

<!-- Script para funcionalidades -->
<script>

    error_reporting(E_ALL);
ini_set('display_errors', 1);

document.addEventListener('DOMContentLoaded', function() {
    // Referencias a elementos del formulario
    const form = document.querySelector('form');
    const productNameInput = document.getElementById('product_name');
    const descriptionInput = document.getElementById('description');
    const fotoInput = document.getElementById('foto');
    const modelYearInput = document.getElementById('model_year');
    const priceInput = document.getElementById('price');
    const stockInput = document.getElementById('stock');
    const charCount = document.getElementById('char-count');
    const imagePreview = document.getElementById('image-preview');
    const previewImg = document.getElementById('preview-img');
    const currentImage = document.getElementById('current-image');
    
    // Referencias a elementos de resumen
    const summaryName = document.getElementById('summary-name').querySelector('.fw-semibold');
    const summaryYear = document.getElementById('summary-year').querySelector('.fw-semibold');
    const summaryPrice = document.getElementById('summary-price').querySelector('.fw-semibold');
    const summaryStock = document.getElementById('summary-stock').querySelector('.fw-semibold');
    const previewChanges = document.getElementById('preview-changes');
    
    // Referencias a indicadores
    const stockStatus = document.getElementById('stock-status');
    const selectedTags = document.getElementById('selected-tags');
    
    // Valores originales para detectar cambios
    const originalValues = {
        product_name: productNameInput.value,
        description: descriptionInput.value,
        model_year: modelYearInput.value,
        price: priceInput.value,
        stock: stockInput.value
    };
    
    // Contador de caracteres para descripción
    descriptionInput.addEventListener('input', function() {
        const count = this.value.length;
        charCount.textContent = `${count}/1000`;
        
        if (count > 800) {
            charCount.classList.add('text-warning');
        } else {
            charCount.classList.remove('text-warning');
        }
        
        if (count >= 1000) {
            charCount.classList.add('text-danger');
            charCount.classList.remove('text-warning');
        } else {
            charCount.classList.remove('text-danger');
        }
        
        updateSummary();
        checkForChanges();
    });
    
    // Vista previa de imagen
    fotoInput.addEventListener('change', function() {
        const file = this.files[0];
        if (file) {
            // Validar tipo de archivo
            const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif', 'image/webp', 'image/bmp'];
            if (!allowedTypes.includes(file.type)) {
                alert('Formato de archivo no válido. Use JPG, PNG, GIF, WEBP o BMP.');
                this.value = '';
                return;
            }
            
            // Validar tamaño (5MB)
            if (file.size > 5 * 1024 * 1024) {
                alert('El archivo es demasiado grande. Tamaño máximo: 5MB.');
                this.value = '';
                return;
            }
            
            // Mostrar vista previa
            const reader = new FileReader();
            reader.onload = function(e) {
                previewImg.src = e.target.result;
                imagePreview.style.display = 'block';
                currentImage.style.display = 'none';
            };
            reader.readAsDataURL(file);
        } else {
            imagePreview.style.display = 'none';
            currentImage.style.display = 'block';
        }
    });
    
    // Función para limpiar vista previa de imagen
    window.clearImagePreview = function() {
        fotoInput.value = '';
        imagePreview.style.display = 'none';
        currentImage.style.display = 'block';
    };
    
    // Actualizar indicador de stock
    stockInput.addEventListener('input', function() {
        const stock = parseInt(this.value) || 0;
        
        if (stock === 0) {
            stockStatus.textContent = 'Sin stock';
            stockStatus.className = 'badge bg-danger';
        } else if (stock <= 5) {
            stockStatus.textContent = 'Stock bajo';
            stockStatus.className = 'badge bg-warning text-dark';
        } else if (stock <= 20) {
            stockStatus.textContent = 'Stock medio';
            stockStatus.className = 'badge bg-info';
        } else {
            stockStatus.textContent = 'Stock disponible';
            stockStatus.className = 'badge bg-success';
        }
        
        updateSummary();
        checkForChanges();
    });
    
    // Actualizar vista previa de etiquetas
    const tagCheckboxes = document.querySelectorAll('input[name="tags[]"]');
    tagCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function() {
            updateTagsPreview();
            checkForChanges();
        });
    });
    
    function updateTagsPreview() {
        const selectedTagsArray = [];
        tagCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                const label = document.querySelector(`label[for="${checkbox.id}"]`).textContent;
                selectedTagsArray.push(label);
            }
        });
        
        if (selectedTagsArray.length > 0) {
            selectedTags.innerHTML = selectedTagsArray.map(tag => 
                `<span class="badge bg-primary me-1 mb-1">${tag}</span>`
            ).join('');
        } else {
            selectedTags.innerHTML = '<span class="text-muted">Ninguna etiqueta seleccionada</span>';
        }
    }
    
    // Detectar cambios en el formulario
    function checkForChanges() {
        let hasChanges = false;
        
        // Verificar cada campo
        const currentValues = {
            product_name: productNameInput.value,
            description: descriptionInput.value,
            model_year: modelYearInput.value,
            price: priceInput.value,
            stock: stockInput.value
        };
        
        for (let key in originalValues) {
            if (originalValues[key] !== currentValues[key]) {
                hasChanges = true;
                break;
            }
        }
        
        // Verificar si se seleccionó una nueva imagen
        if (fotoInput.files[0]) {
            hasChanges = true;
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
    
    // Actualizar resumen del producto
    function updateSummary() {
        const name = productNameInput.value.trim();
        const year = modelYearInput.value;
        const price = parseFloat(priceInput.value) || 0;
        const stock = parseInt(stockInput.value) || 0;
        
        summaryName.textContent = name || '-';
        summaryYear.textContent = year || '-';
        summaryPrice.textContent = price > 0 ? `Bs. ${price.toFixed(2)}` : '-';
        summaryStock.textContent = stock >= 0 ? `${stock} unidades` : '-';
    }
    
    // Event listeners para actualizar resumen y detectar cambios
    [productNameInput, modelYearInput, priceInput, stockInput].forEach(input => {
        input.addEventListener('input', function() {
            updateSummary();
            checkForChanges();
        });
    });
    
    // Formatear precio automáticamente
    priceInput.addEventListener('blur', function() {
        if (this.value) {
            this.value = parseFloat(this.value).toFixed(2);
        }
        updateSummary();
        checkForChanges();
    });
    
    // Validación en tiempo real
    const inputs = form.querySelectorAll('input[required]');
    inputs.forEach(function(input) {
        input.addEventListener('blur', function() {
            validateInput(this);
        });
        
        input.addEventListener('input', function() {
            this.classList.remove('is-invalid', 'is-valid');
        });
    });
    
    function validateInput(input) {
        const value = input.value.trim();
        let isValid = true;
        
        if (input.hasAttribute('required') && !value) {
            isValid = false;
        }
        
        // Validaciones específicas
        switch(input.name) {
            case 'product_name':
                if (value.length < 3 || value.length > 200) {
                    isValid = false;
                }
                break;
            case 'model_year':
                const year = parseInt(value);
                const currentYear = new Date().getFullYear();
                if (year < 1900 || year > currentYear + 2) {
                    isValid = false;
                }
                break;
            case 'price':
                const price = parseFloat(value);
                if (price <= 0 || price > 999999.99) {
                    isValid = false;
                }
                break;
            case 'stock':
                const stock = parseInt(value);
                if (stock < 0 || stock > 99999) {
                    isValid = false;
                }
                break;
        }
        
        if (isValid) {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        } else {
            input.classList.remove('is-valid');
            input.classList.add('is-invalid');
        }
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
                firstInvalid.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        } else {
            // Confirmar actualización si hay cambios
            let hasChanges = false;
            for (let key in originalValues) {
                if (originalValues[key] !== document.querySelector(`[name="${key}"]`).value) {
                    hasChanges = true;
                    break;
                }
            }
            
            if (hasChanges || fotoInput.files[0]) {
                const confirmUpdate = confirm('¿Está seguro de que desea actualizar la información del producto?\n\nEsto modificará los datos permanentemente.');
                if (!confirmUpdate) {
                    e.preventDefault();
                    return false;
                }
            }
            
            // Mostrar indicador de carga
            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;
            submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Actualizando...';
        }
    });
    
    // Inicializar indicador de stock
    const initialStock = parseInt(stockInput.value) || 0;
    if (initialStock === 0) {
        stockStatus.textContent = 'Sin stock';
        stockStatus.className = 'badge bg-danger';
    } else if (initialStock <= 5) {
        stockStatus.textContent = 'Stock bajo';
        stockStatus.className = 'badge bg-warning text-dark';
    } else if (initialStock <= 20) {
        stockStatus.textContent = 'Stock medio';
        stockStatus.className = 'badge bg-info';
    } else {
        stockStatus.textContent = 'Stock disponible';
        stockStatus.className = 'badge bg-success';
    }
    
    // Inicializar vista previa de etiquetas
    updateTagsPreview();
    
    // Enfocar el primer campo
    productNameInput.focus();
    
    console.log('Editor de productos inicializado correctamente');
    console.log('Producto ID:', <?php echo $txtID; ?>);
});

// Función global para debugging
function debugFormulario() {
    console.log('=== DEBUG FORMULARIO EDITAR PRODUCTO ===');
    console.log('ID Producto:', <?php echo $txtID; ?>);
    
    const formData = new FormData(document.querySelector('form'));
    for (let [key, value] of formData.entries()) {
        console.log(`${key}:`, value);
    }
    
    console.log('Validación de campos:');
    const inputs = document.querySelectorAll('input[required]');
    inputs.forEach(input => {
        console.log(`${input.name}: válido=${input.checkValidity()}, valor="${input.value}"`);
    });
    
    console.log('====================================');
}

// Hacer función debug disponible globalmente
window.debugFormulario = debugFormulario;
</script>

<!-- Estilos adicionales -->
<style>
/* Estilos base */
.form-control:focus, .form-select:focus {
    border-color: #ffc107;
    box-shadow: 0 0 0 0.2rem rgba(255, 193, 7, 0.25);
}

.is-valid {
    border-color: #198754 !important;
}

.is-invalid {
    border-color: #dc3545 !important;
}

/* Vista previa de imagen */
#image-preview, #current-image {
    border: 2px dashed #dee2e6;
    border-radius: 0.5rem;
    padding: 1rem;
    text-align: center;
    background-color: #f8f9fa;
    transition: all 0.3s ease;
}

#image-preview:hover, #current-image:hover {
    border-color: #ffc107;
    background-color: #fff9e6;
}

#preview-img {
    border-radius: 0.5rem;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}

/* Checkboxes */
.form-check-input:checked {
    background-color: #ffc107;
    border-color: #ffc107;
}

.form-check-input:focus {
    border-color: #ffc107;
    box-shadow: 0 0 0 0.2rem rgba(255, 193, 7, 0.25);
}

/* Etiquetas */
#selected-tags .badge {
    font-size: 0.75rem;
    padding: 0.375rem 0.75rem;
}

/* Indicador de stock */
#stock-status {
    transition: all 0.3s ease;
    animation: pulse 0.5s ease-in-out;
}

@keyframes pulse {
    0% { transform: scale(1); }
    50% { transform: scale(1.05); }
    100% { transform: scale(1); }
}

/* Resumen del producto */
#product-summary div {
    margin-bottom: 0.25rem;
    padding: 0.25rem 0;
    border-bottom: 1px solid #e9ecef;
}

#product-summary div:last-child {
    border-bottom: none;
}

#product-summary .fw-semibold {
    color: #ffc107;
}

/* Indicador de cambios */
#preview-changes {
    border-left: 3px solid #ffc107;
    padding-left: 0.5rem;
    margin-left: -0.5rem;
}

/* Efectos hover */
.card {
    transition: transform 0.2s ease-in-out;
}

.card:hover {
    transform: translateY(-2px);
}

.btn:hover {
    transform: translateY(-1px);
    transition: all 0.2s ease;
}

/* Área de descripción */
#description {
    resize: vertical;
    min-height: 100px;
}

/* Contador de caracteres */
#char-count {
    font-size: 0.8rem;
    font-weight: 500;
}

/* Responsive */
@media (max-width: 768px) {
    .form-control-lg, .input-group-lg .form-control {
        font-size: 1rem;
        padding: 0.75rem;
    }
    
    .btn-lg {
        font-size: 1rem;
        padding: 0.75rem 1.5rem;
    }
    
    #image-preview, #current-image {
        padding: 0.5rem;
    }
    
    #preview-img {
        max-width: 100%;
        height: auto;
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

/* Spinner de carga */
.spinner-border-sm {
    width: 1rem;
    height: 1rem;
}

/* Alertas mejoradas */
.alert {
    border-radius: 0.5rem;
    border: none;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}

.alert-warning {
    background: linear-gradient(45deg, #ffc107, #e0a800);
    color: #212529;
}

.alert-danger {
    background: linear-gradient(45deg, #dc3545, #c82333);
    color: white;
}

.alert-danger .btn-close {
    filter: brightness(0) invert(1);
}

/* Etiquetas actuales */
.text-info small {
    background-color: rgba(13, 202, 240, 0.1);
    padding: 0.25rem 0.5rem;
    border-radius: 0.25rem;
    border-left: 3px solid #0dcaf0;
}
</style>

<?php include("../../templates/footer.php"); ?>