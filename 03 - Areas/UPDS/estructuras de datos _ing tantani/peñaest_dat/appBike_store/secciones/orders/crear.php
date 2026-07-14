<?php 
// Incluir archivo de conexión a la base de datos
include("../../bd.php");

// Variables para el formulario
$errores = [];

// Obtener lista de clientes para el select
$stmt_customers = $conexion->prepare("SELECT customer_id, first_name, last_name, email, city 
                                     FROM customers 
                                     ORDER BY first_name ASC, last_name ASC");
$stmt_customers->execute();
$lista_clientes = $stmt_customers->fetchAll(PDO::FETCH_ASSOC);

// Obtener lista de productos para agregar al pedido
$stmt_products = $conexion->prepare("SELECT product_id, product_name, price, foto 
                                    FROM products 
                                    ORDER BY product_name ASC");
$stmt_products->execute();
$lista_productos = $stmt_products->fetchAll(PDO::FETCH_ASSOC);

// Procesar el formulario cuando se envía
if ($_POST) {
    // DEBUG: Mostrar lo que se recibe
    error_log("POST recibido: " . print_r($_POST, true));
    
    // Obtener datos del formulario
    $customer_id = isset($_POST['customer_id']) ? (int)$_POST['customer_id'] : 0;
    $order_date = isset($_POST['order_date']) ? $_POST['order_date'] : '';
    $productos_seleccionados = isset($_POST['productos']) ? $_POST['productos'] : [];

    // Validaciones básicas
    if (empty($customer_id)) {
        $errores[] = "Debe seleccionar un cliente";
    }
    
    if (empty($order_date)) {
        $errores[] = "La fecha del pedido es obligatoria";
    } elseif (strtotime($order_date) > time()) {
        $errores[] = "La fecha del pedido no puede ser futura";
    }
    
    if (empty($productos_seleccionados)) {
        $errores[] = "Debe agregar al menos un producto al pedido";
    }

    // Validar productos seleccionados
    if (!empty($productos_seleccionados)) {
        foreach ($productos_seleccionados as $index => $producto) {
            $product_id = isset($producto['product_id']) ? (int)$producto['product_id'] : 0;
            $quantity = isset($producto['quantity']) ? (float)$producto['quantity'] : 0;
            $price = isset($producto['price']) ? (float)$producto['price'] : 0;
            $discount = isset($producto['discount']) ? (float)$producto['discount'] : 0;

            if ($product_id <= 0) {
                $errores[] = "Producto #" . ($index + 1) . ": Debe seleccionar un producto válido";
            }
            
            if ($quantity <= 0) {
                $errores[] = "Producto #" . ($index + 1) . ": La cantidad debe ser mayor a 0";
            }
            
            if ($price <= 0) {
                $errores[] = "Producto #" . ($index + 1) . ": El precio debe ser mayor a 0";
            }
            
            if ($discount < 0 || $discount >= 1) {
                $errores[] = "Producto #" . ($index + 1) . ": El descuento debe estar entre 0% y 99%";
            }
        }
    } else {
        $errores[] = "DEBUG: Array de productos recibido: " . print_r($productos_seleccionados, true);
    }

    // Si no hay errores, proceder con la creación del pedido
    if (empty($errores)) {
        try {
            // Iniciar transacción
            $conexion->beginTransaction();

            // Insertar el pedido
            $stmt_order = $conexion->prepare("INSERT INTO orders (order_id, customer_id, order_date) 
                                             VALUES (null, :customer_id, :order_date)");
            $stmt_order->bindParam(':customer_id', $customer_id);
            $stmt_order->bindParam(':order_date', $order_date);
            $stmt_order->execute();
            
            // Obtener el ID del pedido creado
            $order_id = $conexion->lastInsertId();

            // Insertar los elementos del pedido
            $stmt_item = $conexion->prepare("INSERT INTO order_items (order_item_id, order_id, product_id, quantity, price, discount) 
                                            VALUES (null, :order_id, :product_id, :quantity, :price, :discount)");

            foreach ($productos_seleccionados as $producto) {
                $stmt_item->bindParam(':order_id', $order_id);
                $stmt_item->bindParam(':product_id', $producto['product_id']);
                $stmt_item->bindParam(':quantity', $producto['quantity']);
                $stmt_item->bindParam(':price', $producto['price']);
                $stmt_item->bindParam(':discount', $producto['discount']);
                $stmt_item->execute();
            }

            // Confirmar transacción
            $conexion->commit();

            $mensaje = "Pedido creado correctamente con ID #" . str_pad($order_id, 4, '0', STR_PAD_LEFT);
            header("Location:index.php?mensaje=" . urlencode($mensaje));
            exit();

        } catch (Exception $e) {
            // Revertir transacción en caso de error
            $conexion->rollBack();
            $errores[] = "Error al crear el pedido: " . $e->getMessage();
        }
    }
}
?>

<?php include("../../templates/header.php"); ?>

<!-- Contenedor principal -->
<div class="container-fluid py-4">
    <!-- Título de la página -->
    <div class="row mb-4">
        <div class="col-12">
            <h2 class="text-primary fw-bold">
                <i class="bi bi-cart-plus"></i> Crear Nuevo Pedido
            </h2>
            <p class="text-muted">Complete la información para crear un nuevo pedido</p>
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
    <form action="" method="post" id="orderForm">
        <div class="row">
            <!-- Información del pedido -->
            <div class="col-lg-4 mb-4">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-header bg-primary text-white">
                        <h5 class="card-title mb-0">
                            <i class="bi bi-info-circle me-2"></i>
                            Información del Pedido
                        </h5>
                    </div>
                    <div class="card-body">
                        <!-- Cliente -->
                        <div class="mb-4">
                            <label for="customer_id" class="form-label fw-semibold">
                                <i class="bi bi-person me-1"></i>
                                Cliente <span class="text-danger">*</span>
                            </label>
                            <select class="form-select form-select-lg" name="customer_id" id="customer_id" required>
                                <option value="">Seleccione un cliente...</option>
                                <?php foreach ($lista_clientes as $cliente): ?>
                                    <option value="<?php echo $cliente['customer_id']; ?>" 
                                            data-email="<?php echo htmlspecialchars($cliente['email']); ?>"
                                            data-city="<?php echo htmlspecialchars($cliente['city']); ?>"
                                            <?php echo (isset($_POST['customer_id']) && $_POST['customer_id'] == $cliente['customer_id']) ? 'selected' : ''; ?>>
                                        <?php echo htmlspecialchars($cliente['first_name'] . ' ' . $cliente['last_name']); ?>
                                    </option>
                                <?php endforeach; ?>
                            </select>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                Seleccione el cliente para este pedido
                            </div>
                            
                            <!-- Información del cliente seleccionado -->
                            <div id="customer-info" class="mt-3" style="display: none;">
                                <div class="alert alert-info">
                                    <strong>Cliente seleccionado:</strong>
                                    <br><span id="customer-email"></span>
                                    <br><span id="customer-city"></span>
                                </div>
                            </div>
                        </div>

                        <!-- Fecha del pedido -->
                        <div class="mb-4">
                            <label for="order_date" class="form-label fw-semibold">
                                <i class="bi bi-calendar-event me-1"></i>
                                Fecha del Pedido <span class="text-danger">*</span>
                            </label>
                            <input type="datetime-local" 
                                   class="form-control form-control-lg" 
                                   name="order_date" 
                                   id="order_date" 
                                   value="<?php echo isset($_POST['order_date']) ? $_POST['order_date'] : date('Y-m-d\TH:i'); ?>"
                                   max="<?php echo date('Y-m-d\TH:i'); ?>"
                                   required>
                            <div class="form-text">
                                <i class="bi bi-info-circle me-1"></i>
                                Fecha y hora del pedido
                            </div>
                        </div>

                        <!-- Resumen del pedido -->
                        <div class="card bg-light">
                            <div class="card-body">
                                <h6 class="fw-bold">Resumen del Pedido</h6>
                                <div class="d-flex justify-content-between">
                                    <span>Total de productos:</span>
                                    <span id="total-items">0</span>
                                </div>
                                <div class="d-flex justify-content-between">
                                    <span>Subtotal:</span>
                                    <span id="subtotal">Bs. 0.00</span>
                                </div>
                                <div class="d-flex justify-content-between">
                                    <span>Descuento:</span>
                                    <span id="total-discount" class="text-danger">Bs. 0.00</span>
                                </div>
                                <hr>
                                <div class="d-flex justify-content-between fw-bold fs-5">
                                    <span>Total:</span>
                                    <span id="total-amount" class="text-success">Bs. 0.00</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Productos del pedido -->
            <div class="col-lg-8 mb-4">
                <div class="card shadow-sm border-0">
                    <div class="card-header bg-success text-white d-flex justify-content-between align-items-center">
                        <h5 class="card-title mb-0">
                            <i class="bi bi-box-seam me-2"></i>
                            Productos del Pedido
                        </h5>
                        <button type="button" class="btn btn-light btn-sm" onclick="agregarProducto()">
                            <i class="bi bi-plus-lg me-1"></i>
                            Agregar Producto
                        </button>
                    </div>
                    <div class="card-body">
                        <div id="productos-container">
                            <!-- Los productos se agregan dinámicamente aquí -->
                        </div>
                        
                        <!-- Mensaje cuando no hay productos -->
                        <div id="no-products-message" class="text-center py-4 text-muted">
                            <i class="bi bi-box display-4"></i>
                            <p class="mt-2">No hay productos agregados al pedido</p>
                            <button type="button" class="btn btn-success" onclick="agregarProducto()">
                                <i class="bi bi-plus-lg me-1"></i>
                                Agregar primer producto
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Botones de acción -->
        <div class="row">
            <div class="col-12">
                <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                    <a href="index.php" class="btn btn-outline-secondary btn-lg me-md-2">
                        <i class="bi bi-arrow-left me-1"></i>
                        Cancelar
                    </a>
                    <button type="submit" class="btn btn-primary btn-lg" id="submitBtn" disabled>
                        <i class="bi bi-check-lg me-1"></i>
                        Crear Pedido
                    </button>
                </div>
            </div>
        </div>
    </form>
</div>

<!-- Template para productos -->
<div id="producto-template" style="display: none;">
    <div class="producto-item border rounded p-3 mb-3">
        <div class="row align-items-center">
            <div class="col-md-4">
                <select class="form-select producto-select" name="productos[INDEX][product_id]" required>
                    <option value="">Seleccionar producto...</option>
                    <?php foreach ($lista_productos as $producto): ?>
                        <option value="<?php echo $producto['product_id']; ?>" 
                                data-price="<?php echo $producto['price']; ?>"
                                data-name="<?php echo htmlspecialchars($producto['product_name']); ?>">
                            <?php echo htmlspecialchars($producto['product_name']); ?> - Bs. <?php echo number_format($producto['price'], 2); ?>
                        </option>
                    <?php endforeach; ?>
                </select>
            </div>
            <div class="col-md-2">
                <input type="number" 
                       class="form-control cantidad-input" 
                       name="productos[INDEX][quantity]" 
                       placeholder="Cant." 
                       min="0.01" 
                       step="0.01" 
                       value="1"
                       required>
            </div>
            <div class="col-md-2">
                <input type="number" 
                       class="form-control precio-input" 
                       name="productos[INDEX][price]" 
                       placeholder="Precio" 
                       min="0.01" 
                       step="0.01"
                       required>
            </div>
            <div class="col-md-2">
                <input type="number" 
                       class="form-control descuento-input" 
                       name="productos[INDEX][discount]" 
                       placeholder="Desc. %" 
                       min="0" 
                       max="0.99" 
                       step="0.01" 
                       value="0">
            </div>
            <div class="col-md-1">
                <span class="fw-bold text-success total-producto">Bs. 0.00</span>
            </div>
            <div class="col-md-1 text-end">
                <button type="button" class="btn btn-outline-danger btn-sm" onclick="eliminarProducto(this)">
                    <i class="bi bi-trash3"></i>
                </button>
            </div>
        </div>
    </div>
</div>

<script>
let productoIndex = 0;

// Funciones globales (fuera de DOMContentLoaded)
function agregarProducto() {
    console.log('Función agregarProducto() llamada');
    
    try {
        const container = document.getElementById('productos-container');
        const template = document.getElementById('producto-template');
        
        if (!container) {
            console.error('No se encontró el contenedor de productos');
            alert('Error: No se encontró el contenedor de productos');
            return;
        }
        
        if (!template) {
            console.error('No se encontró el template de productos');
            alert('Error: No se encontró el template de productos');
            return;
        }
        
        console.log('Template encontrado, clonando...');
        
        // Clonar el template
        const clone = template.cloneNode(true);
        clone.style.display = 'block';
        clone.id = 'producto-item-' + productoIndex;
        
        // Reemplazar INDEX con el índice actual
        clone.innerHTML = clone.innerHTML.replace(/INDEX/g, productoIndex);
        
        console.log('Agregando al contenedor...');
        
        // Agregar al contenedor
        container.appendChild(clone);
        
        console.log('Configurando event listeners...');
        
        // Configurar event listeners para el nuevo producto
        const newProducto = container.lastElementChild;
        configurarEventListenersProducto(newProducto);
        
        // Ocultar mensaje de no productos
        const noProductsMessage = document.getElementById('no-products-message');
        if (noProductsMessage) {
            noProductsMessage.style.display = 'none';
        }
        
        productoIndex++;
        console.log('Producto agregado exitosamente. Índice actual:', productoIndex);
        
        validarFormulario();
        
    } catch (error) {
        console.error('Error al agregar producto:', error);
        alert('Error al agregar producto: ' + error.message);
    }
}

function eliminarProducto(button) {
    console.log('Eliminando producto...');
    
    const container = document.getElementById('productos-container');
    const productoItem = button.closest('.producto-item');
    
    if (productoItem) {
        productoItem.remove();
    }
    
    // Mostrar mensaje si no hay productos
    if (container && container.children.length === 0) {
        const noProductsMessage = document.getElementById('no-products-message');
        if (noProductsMessage) {
            noProductsMessage.style.display = 'block';
        }
    }
    
    calcularResumen();
    validarFormulario();
}

function configurarEventListenersProducto(productoElement) {
    try {
        // Event listener para el select de producto
        const productoSelect = productoElement.querySelector('.producto-select');
        if (productoSelect) {
            productoSelect.addEventListener('change', function() {
                const precioInput = this.closest('.producto-item').querySelector('.precio-input');
                if (this.value && precioInput) {
                    const selectedOption = this.options[this.selectedIndex];
                    precioInput.value = selectedOption.dataset.price;
                } else if (precioInput) {
                    precioInput.value = '';
                }
                calcularTotalProducto(this.closest('.producto-item'));
                calcularResumen();
                validarFormulario();
            });
        }
        
        // Event listeners para cantidad, precio y descuento
        const inputs = productoElement.querySelectorAll('.cantidad-input, .precio-input, .descuento-input');
        inputs.forEach(function(input) {
            input.addEventListener('input', function() {
                calcularTotalProducto(this.closest('.producto-item'));
                calcularResumen();
                validarFormulario();
            });
        });
    } catch (error) {
        console.error('Error configurando event listeners:', error);
    }
}

function calcularTotalProducto(productoElement) {
    try {
        const cantidadInput = productoElement.querySelector('.cantidad-input');
        const precioInput = productoElement.querySelector('.precio-input');
        const descuentoInput = productoElement.querySelector('.descuento-input');
        const totalSpan = productoElement.querySelector('.total-producto');
        
        if (!cantidadInput || !precioInput || !descuentoInput || !totalSpan) {
            return;
        }
        
        const cantidad = parseFloat(cantidadInput.value) || 0;
        const precio = parseFloat(precioInput.value) || 0;
        const descuento = parseFloat(descuentoInput.value) || 0;
        
        const subtotal = cantidad * precio;
        const total = subtotal * (1 - descuento);
        
        totalSpan.textContent = 'Bs. ' + total.toFixed(2);
    } catch (error) {
        console.error('Error calculando total producto:', error);
    }
}

function calcularResumen() {
    try {
        const productos = document.querySelectorAll('#productos-container .producto-item');
        let totalItems = 0;
        let subtotal = 0;
        let totalDescuento = 0;
        
        productos.forEach(function(producto) {
            const cantidadInput = producto.querySelector('.cantidad-input');
            const precioInput = producto.querySelector('.precio-input');
            const descuentoInput = producto.querySelector('.descuento-input');
            
            if (cantidadInput && precioInput && descuentoInput) {
                const cantidad = parseFloat(cantidadInput.value) || 0;
                const precio = parseFloat(precioInput.value) || 0;
                const descuento = parseFloat(descuentoInput.value) || 0;
                
                if (cantidad > 0 && precio > 0) {
                    totalItems += cantidad;
                    const subtotalProducto = cantidad * precio;
                    subtotal += subtotalProducto;
                    totalDescuento += subtotalProducto * descuento;
                }
            }
        });
        
        const total = subtotal - totalDescuento;
        
        const totalItemsElement = document.getElementById('total-items');
        const subtotalElement = document.getElementById('subtotal');
        const totalDiscountElement = document.getElementById('total-discount');
        const totalAmountElement = document.getElementById('total-amount');
        
        if (totalItemsElement) totalItemsElement.textContent = totalItems.toFixed(2);
        if (subtotalElement) subtotalElement.textContent = 'Bs. ' + subtotal.toFixed(2);
        if (totalDiscountElement) totalDiscountElement.textContent = 'Bs. ' + totalDescuento.toFixed(2);
        if (totalAmountElement) totalAmountElement.textContent = 'Bs. ' + total.toFixed(2);
    } catch (error) {
        console.error('Error calculando resumen:', error);
    }
}

function validarFormulario() {
    try {
        const customerIdElement = document.getElementById('customer_id');
        const orderDateElement = document.getElementById('order_date');
        const productos = document.querySelectorAll('#productos-container .producto-item');
        const submitBtn = document.getElementById('submitBtn');
        
        if (!customerIdElement || !orderDateElement || !submitBtn) {
            console.error('No se encontraron elementos del formulario');
            return;
        }
        
        const customerId = customerIdElement.value;
        const orderDate = orderDateElement.value;
        
        let isValid = customerId && orderDate && productos.length > 0;
        
        // Validar que todos los productos estén completos
        if (isValid && productos.length > 0) {
            productos.forEach(function(producto) {
                const productoSelect = producto.querySelector('.producto-select');
                const cantidadInput = producto.querySelector('.cantidad-input');
                const precioInput = producto.querySelector('.precio-input');
                
                if (productoSelect && cantidadInput && precioInput) {
                    const productoId = productoSelect.value;
                    const cantidad = parseFloat(cantidadInput.value) || 0;
                    const precio = parseFloat(precioInput.value) || 0;
                    
                    if (!productoId || cantidad <= 0 || precio <= 0) {
                        isValid = false;
                    }
                } else {
                    isValid = false;
                }
            });
        }
        
        submitBtn.disabled = !isValid;
    } catch (error) {
        console.error('Error validando formulario:', error);
    }
}

// Función de test
window.testAgregarProducto = function() {
    console.log('Test: Agregando producto...');
    agregarProducto();
};

// Cuando el DOM se carga
document.addEventListener('DOMContentLoaded', function() {
    console.log('DOM cargado, inicializando...');
    
    try {
        // Manejar selección de cliente
        const customerSelect = document.getElementById('customer_id');
        if (customerSelect) {
            customerSelect.addEventListener('change', function() {
                const customerInfo = document.getElementById('customer-info');
                const emailSpan = document.getElementById('customer-email');
                const citySpan = document.getElementById('customer-city');
                
                if (this.value && emailSpan && citySpan) {
                    const selectedOption = this.options[this.selectedIndex];
                    emailSpan.textContent = selectedOption.dataset.email;
                    citySpan.textContent = selectedOption.dataset.city;
                    if (customerInfo) customerInfo.style.display = 'block';
                } else {
                    if (customerInfo) customerInfo.style.display = 'none';
                }
                validarFormulario();
            });
        }
        
        // Event listeners para validación
        const orderDateElement = document.getElementById('order_date');
        if (customerSelect) customerSelect.addEventListener('change', validarFormulario);
        if (orderDateElement) orderDateElement.addEventListener('change', validarFormulario);
        
        // Validación inicial
        validarFormulario();
        
        // Debug: Agregar listener al formulario
        const orderForm = document.getElementById('orderForm');
        if (orderForm) {
            orderForm.addEventListener('submit', function(e) {
                const formData = new FormData(this);
                console.log('Datos del formulario a enviar:');
                for (let [key, value] of formData.entries()) {
                    console.log(key, value);
                }
            });
        }
        
        console.log('Inicialización completada');
        
        // Test de disponibilidad de función
        console.log('Función agregarProducto disponible:', typeof agregarProducto);
        
    } catch (error) {
        console.error('Error en inicialización:', error);
    }
});
</script>

<?php include("../../templates/footer.php"); ?>