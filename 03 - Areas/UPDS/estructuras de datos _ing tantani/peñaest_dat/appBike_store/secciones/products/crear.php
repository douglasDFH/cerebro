<?php 
// Habilitar reporte de errores


// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Variables para almacenar los datos del formulario
$errores = [];

// *************** MODIFICACIONES PRINCIPALES *************** //
// Configuración para subida de archivos - RUTAS CORREGIDAS
$upload_dir = realpath(__DIR__ . '/../../uploads/products/') . DIRECTORY_SEPARATOR;
$upload_url = 'uploads/products/'; // Ruta relativa consistente

// Crear directorio con permisos recursivos
// Verificar y crear directorio
if (!file_exists($upload_dir)) {
    mkdir($upload_dir, 0755, true);
}

// Verificar permisos de escritura
if (!is_writable($upload_dir)) {
    die("Error crítico: El directorio de uploads no tiene permisos de escritura");
}
// *************** FIN MODIFICACIONES *************** //

// Procesar el formulario cuando se envía
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Validar y sanitizar entradas
    $product_name = trim($_POST["product_name"] ?? '');
    $description = trim($_POST["description"] ?? '');
    $model_year = filter_input(INPUT_POST, 'model_year', FILTER_VALIDATE_INT, [
        'options' => [
            'min_range' => 1900,
            'max_range' => date('Y') + 2
        ]
    ]);
    
    // *************** CORRECCIÓN PARA PRECIO *************** //
    $price = filter_input(INPUT_POST, 'price', FILTER_SANITIZE_NUMBER_FLOAT, FILTER_FLAG_ALLOW_FRACTION);
    $price = number_format((float)$price, 2, '.', '');
    
    $stock = filter_input(INPUT_POST, 'stock', FILTER_VALIDATE_INT, [
        'options' => [
            'min_range' => 0,
            'max_range' => 99999
        ]
    ]);
    
    $tags = isset($_POST["tags"]) ? array_map('trim', $_POST["tags"]) : [];
    $tags_string = !empty($tags) ? implode(',', $tags) : '';

    // Validaciones
    if (empty($product_name)) {
        $errores[] = "El nombre del producto es obligatorio";
    } elseif (mb_strlen($product_name) < 3 || mb_strlen($product_name) > 200) {
        $errores[] = "El nombre debe tener entre 3 y 200 caracteres";
    }

    if (mb_strlen($description) > 1000) {
        $errores[] = "La descripción no puede exceder 1000 caracteres";
    }

    if (!$model_year) {
        $errores[] = "Año del modelo inválido";
    }

    if ($price <= 0 || $price > 999999.99) {
        $errores[] = "Precio inválido";
    }

    if ($stock === false) {
        $errores[] = "Cantidad en stock inválida";
    }

    // *************** MEJORA EN VERIFICACIÓN DE NOMBRE DUPLICADO *************** //
    if (empty($errores)) {
        $verificar_producto = $conexion->prepare("SELECT product_id FROM products WHERE product_name = ? LIMIT 1");
        $verificar_producto->execute([$product_name]);
        
        if ($verificar_producto->fetch()) {
            $errores[] = "Ya existe un producto con ese nombre";
        }
    }

    // *************** MEJORA EN MANEJO DE ARCHIVOS *************** //
    $foto_path = true;
    if (isset($_FILES['foto']) && $_FILES['foto']['error'] !== UPLOAD_ERR_NO_FILE) {
        if ($_FILES['foto']['error'] === UPLOAD_ERR_OK) {
            $file = $_FILES['foto'];
            
            // Validar tipo MIME real
            $finfo = new finfo(FILEINFO_MIME_TYPE);
            $mime = $finfo->file($file['tmp_name']);
            $allowed_mimes = [
                'image/jpeg' => 'jpg',
                'image/png' => 'png',
                'image/gif' => 'gif',
                'image/webp' => 'webp',
                'image/bmp' => 'bmp'
            ];
            
            if (!array_key_exists($mime, $allowed_mimes)) {
                $errores[] = "Formato de imagen no válido";
            } elseif ($file['size'] > 5 * 1024 * 1024) {
                $errores[] = "El archivo excede los 5MB permitidos";
            } else {
                $extension = $allowed_mimes[$mime];
                $unique_name = bin2hex(random_bytes(16)) . ".$extension";
                $target_path = $upload_dir . $unique_name;
                
                if (move_uploaded_file($file['tmp_name'], $target_path)) {
                    $foto_path = $upload_url . $unique_name;
                } else {
                    $errores[] = "Error al guardar la imagen";
                }
            }
        } else {
            $errores[] = match ($_FILES['foto']['error']) {
                UPLOAD_ERR_INI_SIZE, UPLOAD_ERR_FORM_SIZE => "Archivo demasiado grande",
                UPLOAD_ERR_PARTIAL => "Subida interrumpida",
                default => "Error desconocido al subir archivo"
            };
        }
    } else {
        $foto_path = $upload_url . 'default-product.png';
    }

    // Insertar en base de datos si no hay errores
    if (empty($errores)) {
        try {
            $sentencia = $conexion->prepare("INSERT INTO products 
                (product_name, description, foto, tags, model_year, price, stock)
                VALUES (?, ?, ?, ?, ?, ?, ?)");
            
            $sentencia->execute([
                $product_name,
                $description,
                $foto_path,
                $tags_string,
                $model_year,
                $price,
                $stock
            ]);
            
            $mensaje = "Producto creado exitosamente (ID: " . $conexion->lastInsertId() . ")";
            header("Location: index.php?mensaje=" . urlencode($mensaje));
            exit();
            
        } catch (PDOException $e) {
            // Eliminar imagen si falla la inserción
            if ($foto_path && $foto_path !== $upload_url . 'default-product.png') {
                @unlink($upload_dir . basename($foto_path));
            }
            $errores[] = "Error de base de datos: " . $e->getMessage();
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
                <i class="bi bi-plus-square"></i> Crear Nuevo Producto
            </h2>
            <p class="text-muted">Complete la información para agregar un nuevo producto al catálogo</p>
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
                <div class="card-header bg-success text-white py-3">
                    <h5 class="card-title mb-0">
                        <i class="bi bi-box-seam me-2"></i>
                        Información del Producto
                    </h5>
                </div>
                
                <!-- Cuerpo de la tarjeta -->
                <div class="card-body p-4">
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
                                           value="<?php echo isset($_POST['product_name']) ? htmlspecialchars($_POST['product_name']) : ''; ?>"
                                           maxlength="200"
                                           required>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Nombre descriptivo y único del producto (3-200 caracteres)
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
                                              placeholder="Descripción detallada del producto, características, especificaciones..."
                                              maxlength="1000"><?php echo isset($_POST['description']) ? htmlspecialchars($_POST['description']) : ''; ?></textarea>
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Descripción detallada del producto (máximo 1000 caracteres)
                                        <span id="char-count" class="float-end text-muted">0/1000</span>
                                    </div>
                                </div>

                                <!-- Campo Imagen - MEJORADO -->
                                <div class="mb-4">
                                    <label for="foto" class="form-label fw-semibold">
                                        <i class="bi bi-image me-1"></i>
                                        Imagen del Producto
                                    </label>
                                    <input type="file" 
                                           class="form-control form-control-lg" 
                                           name="foto" 
                                           id="foto" 
                                           accept="image/jpeg, image/jpg, image/png, image/gif, image/webp, image/bmp">
                                    <div class="form-text">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Formatos permitidos: JPG, PNG, GIF, WEBP, BMP (máximo 5MB)
                                    </div>
                                    
                                    <!-- Vista previa de la imagen - MEJORADA -->
                                    <div id="image-preview" class="mt-3" style="display: none;">
                                        <div class="border rounded p-3 bg-light text-center">
                                            <p class="small text-muted mb-2">
                                                <i class="bi bi-eye me-1"></i>Vista previa:
                                            </p>
                                            <img id="preview-img" 
                                                 src="../../uploads/products/default-product.png" 
                                                 alt="Vista previa" 
                                                 class="img-thumbnail shadow-sm" 
                                                 style="max-width: 300px; max-height: 200px; object-fit: cover;">
                                            <div class="mt-2">
                                                <button type="button" 
                                                        class="btn btn-sm btn-outline-danger" 
                                                        onclick="clearImagePreview()">
                                                    <i class="bi bi-x-lg me-1"></i> Quitar imagen
                                                </button>
                                            </div>
                                        </div>
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
                                           value="<?php echo isset($_POST['model_year']) ? $_POST['model_year'] : date('Y'); ?>"
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
                                               value="<?php echo isset($_POST['price']) ? $_POST['price'] : ''; ?>"
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
                                               value="<?php echo isset($_POST['stock']) ? $_POST['stock'] : '0'; ?>"
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
                                        <span id="stock-status" class="badge bg-secondary">Sin stock</span>
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
                                                       <?php echo (isset($_POST['tags']) && in_array($key, $_POST['tags'])) ? 'checked' : ''; ?>>
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
                                </div>

                                <!-- Resumen del producto -->
                                <div class="mb-4">
                                    <div class="card bg-light">
                                        <div class="card-body p-3">
                                            <h6 class="fw-bold text-muted mb-2">
                                                <i class="bi bi-eye me-1"></i>
                                                Resumen del Producto
                                            </h6>
                                            <div id="product-summary" class="small text-muted">
                                                <div id="summary-name">Nombre: <span class="fw-semibold">-</span></div>
                                                <div id="summary-year">Año: <span class="fw-semibold">-</span></div>
                                                <div id="summary-price">Precio: <span class="fw-semibold">-</span></div>
                                                <div id="summary-stock">Stock: <span class="fw-semibold">-</span></div>
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
                                    <button type="submit" class="btn btn-success btn-lg" id="submitBtn">
                                        <i class="bi bi-check-lg me-1"></i>
                                        Crear Producto
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

<!-- Script para funcionalidades - MEJORADO -->
<script>

    // Habilitar reporte de errores
/*error_reporting(E_ALL);
ini_set('display_errors', 1);*/
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
    
    // Referencias a elementos de resumen
    const summaryName = document.getElementById('summary-name').querySelector('.fw-semibold');
    const summaryYear = document.getElementById('summary-year').querySelector('.fw-semibold');
    const summaryPrice = document.getElementById('summary-price').querySelector('.fw-semibold');
    const summaryStock = document.getElementById('summary-stock').querySelector('.fw-semibold');
    
    // Referencias a indicadores
    const stockStatus = document.getElementById('stock-status');
    const selectedTags = document.getElementById('selected-tags');
    
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
    });
    
    // Vista previa de imagen - MEJORADA
    fotoInput.addEventListener('change', function() {
        const file = this.files[0];
        if (file) {
            // Validar tipo de archivo
            const allowedTypes = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif', 'image/webp', 'image/bmp'];
            const fileType = file.type.toLowerCase();
            
            if (!allowedTypes.includes(fileType)) {
                alert('Formato de archivo no válido. Use JPG, PNG, GIF, WEBP o BMP.');
                this.value = ' ';
                imagePreview.style.display = 'none';
                return;
            }
            
            // Validar tamaño (5MB)
            if (file.size > 5 * 1024 * 1024) {
                alert('El archivo es demasiado grande. Tamaño máximo: 5MB.');
                this.value = '';
                imagePreview.style.display = 'none';
                return;
            }
            
            // Mostrar vista previa con animación
            const reader = new FileReader();
            reader.onload = function(e) {
                previewImg.src = e.target.result;
                imagePreview.style.display = 'block';
                imagePreview.classList.add('fade-in');
                
                // Mostrar información del archivo
                const fileInfo = `Archivo: ${file.name} (${(file.size / 1024 / 1024).toFixed(2)} MB)`;
                console.log(fileInfo);
            };
            reader.readAsDataURL(file);
        } else {
            imagePreview.style.display = 'none';
        }
    });
    
    // Función para limpiar vista previa de imagen
    window.clearImagePreview = function() {
        fotoInput.value = '';
        imagePreview.style.display = 'none';
        previewImg.src = ' ../../uploads/products/default-product.png';
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
    });
    
    // Actualizar vista previa de etiquetas
    const tagCheckboxes = document.querySelectorAll('input[name="tags[]"]');
    tagCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', updateTagsPreview);
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
    
    // Event listeners para actualizar resumen
    [productNameInput, modelYearInput, priceInput, stockInput].forEach(input => {
        input.addEventListener('input', updateSummary);
    });
    
    // Formatear precio automáticamente
    priceInput.addEventListener('blur', function() {
        if (this.value) {
            this.value = parseFloat(this.value).toFixed(2);
        }
        updateSummary();
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
            case 'foto':
                if (input.files.length > 0) {
                    const file = input.files[0];
                    const allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp', 'image/bmp'];
                    if (!allowedTypes.includes(file.type) || file.size > 5 * 1024 * 1024) {
                        isValid = false;
                    }
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
        
        // Validar todos los campos requeridos
        inputs.forEach(function(input) {
            if (!validateInput(input)) {
                formIsValid = false;
            }
        });
        
        if (!formIsValid) {
            e.preventDefault();
            alert('Por favor, complete todos los campos requeridos correctamente.');
            
            // Enfocar el primer campo con error
            const firstInvalid = form.querySelector('.is-invalid');
            if (firstInvalid) {
                firstInvalid.focus();
                firstInvalid.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
        } else {
            // Mostrar indicador de carga
            const submitBtn = document.getElementById('submitBtn');
            submitBtn.disabled = true;
            submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Creando producto...';
        }
    });
    
    // Inicializar contadores y vistas previas
    updateSummary();
    updateTagsPreview();
    
    // Inicializar contador de caracteres
    if (descriptionInput.value) {
        charCount.textContent = `${descriptionInput.value.length}/1000`;
    }
    
    // Enfocar el primer campo
    productNameInput.focus();
});
</script>

<!-- Estilos CSS adicionales -->
<style>
.form-control:focus, .form-select:focus {
    border-color: #198754;
    box-shadow: 0 0 0 0.2rem rgba(25, 135, 84, 0.25);
}

.is-valid {
    border-color: #198754 !important;
}

.is-invalid {
    border-color: #dc3545 !important;
}

#image-preview {
    transition: all 0.3s ease;
}

.fade-in {
    animation: fadeIn 0.5s ease-in;
}

@keyframes fadeIn {
    from { opacity: 0; transform: translateY(-10px); }
    to { opacity: 1; transform: translateY(0); }
}

#product-summary div {
    margin-bottom: 0.25rem;
}

#product-summary .fw-semibold {
    color: #198754;
}

.card:hover {
    transform: translateY(-2px);
    transition: transform 0.2s ease-in-out;
}

/* Mejorar la apariencia de la vista previa de imagen */
#image-preview {
    border: 2px dashed #dee2e6;
    border-radius: 0.5rem;
    transition: all 0.3s ease;
}

#image-preview:hover {
    border-color: #198754;
    background-color: #f8f9fa;
}

/* Spinner de carga */
.spinner-border-sm {
    width: 1rem;
    height: 1rem;
}

/* Responsive improvements */
@media (max-width: 768px) {
    .form-control-lg, .input-group-lg .form-control {
        font-size: 1rem;
        padding: 0.75rem;
    }
    
    .btn-lg {
        font-size: 1rem;
        padding: 0.75rem 1.5rem;
    }
    
    #image-preview {
        padding: 1rem;
    }
}
</style>

<?php include("../../templates/footer.php"); ?>