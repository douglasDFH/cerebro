<?php
// Definir DEVELOPMENT_MODE si no está definido
if (!defined('DEVELOPMENT_MODE')) {
    define('DEVELOPMENT_MODE', false);
}
?>
</main>

    <!-- Footer de la aplicación -->
    <footer class="bg-dark text-light mt-5">
        <div class="container-fluid py-4">
            <div class="row">
                <!-- Información de la institución -->
                <div class="col-lg-4 col-md-6 mb-3">
                    <h5 class="fw-bold text-primary mb-3">
                        <i class="bi bi-building me-2"></i>
                        UPDS
                    </h5>
                    <p class="mb-2">Universidad Privada Domingo Savio</p>
                    <p class="text-muted small mb-0">
                        Formando profesionales con excelencia académica y valores humanos
                    </p>
                </div>

                <!-- Enlaces rápidos -->
                <div class="col-lg-2 col-md-6 mb-3">
                    <h6 class="fw-semibold mb-3 text-white">Enlaces Rápidos</h6>
                    <ul class="list-unstyled">
                        <li class="mb-1">
                            <a href="<?php echo $url_base; ?>" class="text-light text-decoration-none small">
                                <i class="bi bi-house-door me-1"></i> Dashboard
                            </a>
                        </li>
                        <li class="mb-1">
                            <a href="<?php echo $url_base; ?>secciones/customers/index.php" class="text-light text-decoration-none small">
                                <i class="bi bi-people me-1"></i> Clientes
                            </a>
                        </li>
                        <li class="mb-1">
                            <a href="<?php echo $url_base; ?>secciones/products/index.php" class="text-light text-decoration-none small">
                                <i class="bi bi-box-seam me-1"></i> Productos
                            </a>
                        </li>
                        <li class="mb-1">
                            <a href="<?php echo $url_base; ?>secciones/orders/index.php" class="text-light text-decoration-none small">
                                <i class="bi bi-cart-check me-1"></i> Pedidos
                            </a>
                        </li>
                    </ul>
                </div>

                <!-- Información del sistema -->
                <div class="col-lg-3 col-md-6 mb-3">
                    <h6 class="fw-semibold mb-3 text-white">Sistema</h6>
                    <ul class="list-unstyled small">
                        <li class="mb-1 text-muted">
                            <i class="bi bi-bicycle me-1"></i> Bike Store v1.0
                        </li>
                        <li class="mb-1 text-muted">
                            <i class="bi bi-code-square me-1"></i> PHP & MySQL
                        </li>
                        <li class="mb-1 text-muted">
                            <i class="bi bi-bootstrap me-1"></i> Bootstrap 5.3.2
                        </li>
                        <li class="mb-1 text-muted">
                            <i class="bi bi-calendar-event me-1"></i> <?php echo date('Y'); ?>
                        </li>
                    </ul>
                </div>

                <!-- Información de contacto -->
                <div class="col-lg-3 col-md-6 mb-3">
                    <h6 class="fw-semibold mb-3 text-white">Contacto</h6>
                    <ul class="list-unstyled small">
                        <li class="mb-1 text-muted">
                            <i class="bi bi-geo-alt me-1"></i> Cochabamba, Bolivia
                        </li>
                        <li class="mb-1">
                            <a href="mailto:info@upds.edu.bo" class="text-light text-decoration-none">
                                <i class="bi bi-envelope me-1"></i> info@upds.edu.bo
                            </a>
                        </li>
                        <li class="mb-1">
                            <a href="tel:+59144211515" class="text-light text-decoration-none">
                                <i class="bi bi-telephone me-1"></i> +591 (4) 4211515
                            </a>
                        </li>
                        <li class="mb-1">
                            <a href="https://www.upds.edu.bo" target="_blank" class="text-light text-decoration-none">
                                <i class="bi bi-globe me-1"></i> www.upds.edu.bo
                            </a>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- Línea divisoria -->
            <hr class="border-secondary my-3">

            <!-- Copyright y información adicional -->
            <div class="row align-items-center">
                <div class="col-md-6">
                    <p class="text-muted small mb-0">
                        &copy; <?php echo date('Y'); ?> Universidad Privada Domingo Savio. Todos los derechos reservados.
                    </p>
                </div>
                <div class="col-md-6 text-md-end">
                    <div class="d-flex justify-content-md-end align-items-center">
                        <span class="text-muted small me-3">Síguenos:</span>
                        <div class="social-links">
                            <a href="#" class="text-light me-2" title="Facebook">
                                <i class="bi bi-facebook"></i>
                            </a>
                            <a href="#" class="text-light me-2" title="Instagram">
                                <i class="bi bi-instagram"></i>
                            </a>
                            <a href="#" class="text-light me-2" title="LinkedIn">
                                <i class="bi bi-linkedin"></i>
                            </a>
                            <a href="#" class="text-light" title="YouTube">
                                <i class="bi bi-youtube"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Información técnica adicional (solo en modo desarrollo) -->
        <?php if (isset($_GET['debug']) || (defined('DEVELOPMENT_MODE') && DEVELOPMENT_MODE)): ?>
        <div class="bg-secondary py-2">
            <div class="container-fluid">
                <small class="text-muted d-block text-center">
                    <i class="bi bi-bug me-1"></i>
                    Modo Debug - PHP <?php echo phpversion(); ?> | 
                    Servidor: <?php echo $_SERVER['SERVER_SOFTWARE']; ?> | 
                    Memoria: <?php echo round(memory_get_usage(true)/1024/1024, 2); ?>MB |
                    Tiempo: <?php echo number_format((microtime(true) - $_SERVER['REQUEST_TIME_FLOAT']) * 1000, 2); ?>ms
                </small>
            </div>
        </div>
        <?php endif; ?>
    </footer>

    <!-- Scripts de Bootstrap y funcionalidades adicionales -->
    <!-- Popper.js (requerido para dropdowns, tooltips, etc.) -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" 
            integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" 
            crossorigin="anonymous"></script>

    <!-- Bootstrap JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" 
            integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" 
            crossorigin="anonymous"></script>

    <!-- Script personalizado para funcionalidades globales -->
    <script>
        // Inicializar tooltips de Bootstrap
        document.addEventListener('DOMContentLoaded', function() {
            // Habilitar todos los tooltips
            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
            var tooltipList = tooltipTriggerList.map(function(tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl);
            });

            // Habilitar todos los popovers
            var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
            var popoverList = popoverTriggerList.map(function(popoverTriggerEl) {
                return new bootstrap.Popover(popoverTriggerEl);
            });

            // Auto-cerrar alertas después de 5 segundos
            setTimeout(function() {
                var alerts = document.querySelectorAll('.alert-dismissible');
                alerts.forEach(function(alert) {
                    var bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
                    if (bsAlert) {
                        bsAlert.close();
                    }
                });
            }, 5000);

            // Confirmación global para enlaces de eliminación
            var deleteLinks = document.querySelectorAll('a[href*="txtID"], a[onclick*="eliminar"]');
            deleteLinks.forEach(function(link) {
                if (link.classList.contains('btn-danger') || link.textContent.toLowerCase().includes('eliminar')) {
                    link.addEventListener('click', function(e) {
                        if (!confirm('¿Está seguro de que desea eliminar este registro? Esta acción no se puede deshacer.')) {
                            e.preventDefault();
                            return false;
                        }
                    });
                }
            });
        });

        // Función global para mostrar notificaciones toast
        function showToast(message, type = 'info') {
            // Crear elemento toast dinámicamente
            var toastHtml = `
                <div class="toast align-items-center text-white bg-${type} border-0" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="d-flex">
                        <div class="toast-body">
                            ${message}
                        </div>
                        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                    </div>
                </div>
            `;
            
            // Agregar al contenedor de toasts (crear si no existe)
            var toastContainer = document.getElementById('toast-container');
            if (!toastContainer) {
                toastContainer = document.createElement('div');
                toastContainer.id = 'toast-container';
                toastContainer.className = 'toast-container position-fixed bottom-0 end-0 p-3';
                document.body.appendChild(toastContainer);
            }
            
            toastContainer.insertAdjacentHTML('beforeend', toastHtml);
            
            // Mostrar el toast
            var toastElement = toastContainer.lastElementChild;
            var toast = new bootstrap.Toast(toastElement);
            toast.show();
            
            // Remover el elemento del DOM después de que se oculte
            toastElement.addEventListener('hidden.bs.toast', function() {
                toastElement.remove();
            });
        }

        // Función para validar formularios con feedback visual
        function validateForm(formElement) {
            var isValid = true;
            var inputs = formElement.querySelectorAll('input[required], select[required], textarea[required]');
            
            inputs.forEach(function(input) {
                if (!input.value.trim()) {
                    input.classList.add('is-invalid');
                    isValid = false;
                } else {
                    input.classList.remove('is-invalid');
                    input.classList.add('is-valid');
                }
            });
            
            return isValid;
        }
    </script>

    <!-- Script adicional para mejorar la experiencia de usuario -->
    <style>
        .social-links a {
            transition: all 0.3s ease;
            font-size: 1.1rem;
        }
        
        .social-links a:hover {
            transform: translateY(-2px);
            color: #0d6efd !important;
        }
        
        footer {
            border-top: 3px solid #0d6efd;
        }
        
        .toast-container {
            z-index: 1080;
        }
    </style>
</body>
</html>