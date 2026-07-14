/**
 * Banco Camba - Archivo JavaScript principal
 */

document.addEventListener('DOMContentLoaded', function() {
    // Selector de idioma
    const languageSelector = document.getElementById('language');
    if (languageSelector) {
        // MODIFICACIÓN: Corregido el manejador de eventos para el selector de idioma
        // para garantizar que el cambio de idioma funcione correctamente
        languageSelector.addEventListener('change', function() {
            window.location.href = `index.php?controller=usuario&action=cambiarIdioma&lang=${this.value}`;
        });
    }

    // Registrar Service Worker para PWA
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('/service-worker.js')
            .then(function(registration) {
                console.log('Service Worker registrado con éxito:', registration.scope);
            })
            .catch(function(error) {
                console.log('Error al registrar el Service Worker:', error);
            });
    }

    // Confirmar eliminaciones
    const deleteButtons = document.querySelectorAll('.delete-button');
    if (deleteButtons) {
        deleteButtons.forEach(button => {
            button.addEventListener('click', function(event) {
                if (!confirm('¿Está seguro que desea eliminar este elemento?')) {
                    event.preventDefault();
                }
            });
        });
    }

    // Mostrar/ocultar contraseña
    const togglePasswordButtons = document.querySelectorAll('.toggle-password');
    if (togglePasswordButtons) {
        togglePasswordButtons.forEach(button => {
            button.addEventListener('click', function() {
                const passwordInput = document.querySelector(this.getAttribute('toggle'));
                if (passwordInput.type === 'password') {
                    passwordInput.type = 'text';
                    this.classList.replace('icon-eye', 'icon-eye-off');
                } else {
                    passwordInput.type = 'password';
                    this.classList.replace('icon-eye-off', 'icon-eye');
                }
            });
        });
    }

    // Validación de formularios
    const forms = document.querySelectorAll('.needs-validation');
    if (forms) {
        forms.forEach(form => {
            form.addEventListener('submit', function(event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }

    // Datepickers
    const datepickers = document.querySelectorAll('.datepicker');
    if (datepickers.length > 0) {
        datepickers.forEach(input => {
            // Aquí iría la inicialización de datepicker si se está usando una librería
        });
    }

    // Cerrar alertas automáticamente
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
    if (alerts.length > 0) {
        alerts.forEach(alert => {
            setTimeout(() => {
                alert.style.opacity = '0';
                setTimeout(() => {
                    alert.style.display = 'none';
                }, 500);
            }, 5000);
        });
    }
});

// Función para formatear moneda
function formatCurrency(amount, currency = 'BOB') {
    return new Intl.NumberFormat('es-BO', {
        style: 'currency',
        currency: currency
    }).format(amount);
}

// Función para formatear fecha
function formatDate(dateString) {
    const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(dateString).toLocaleDateString('es-BO', options);
}

// Función para enviar transacciones pendientes cuando hay conexión
function syncTransactions() {
    if ('serviceWorker' in navigator && 'SyncManager' in window) {
        navigator.serviceWorker.ready
            .then(function(registration) {
                return registration.sync.register('sync-transactions');
            })
            .catch(function(err) {
                console.log('Error al registrar sync:', err);
            });
    }
}







// JavaScript para la funcionalidad del encabezado manteniendo los nombres originales

document.addEventListener('DOMContentLoaded', function() {
    // Selector de idioma mejorado
    const languageDropdown = document.querySelector('.language-dropdown');
    const languageOptions = document.querySelectorAll('.language-option');
    const languageSelect = document.getElementById('language');
    const currentLanguageText = document.getElementById('current-language');
    
    // Manejador para cada opción de idioma
    languageOptions.forEach(option => {
        option.addEventListener('click', function() {
            const selectedValue = this.getAttribute('data-value');
            const selectedText = this.querySelector('span').textContent;
            
            // Actualizar el texto visible
            currentLanguageText.textContent = selectedText;
            
            // Actualizar el select original (mantenemos la compatibilidad)
            languageSelect.value = selectedValue;
            
            // Lanzar evento de cambio para activar cualquier listener existente
            const event = new Event('change');
            languageSelect.dispatchEvent(event);
            
            // Efecto visual de cambio
            document.body.style.transition = 'opacity 0.2s ease';
            document.body.style.opacity = '0.8';
            
            setTimeout(() => {
                document.body.style.opacity = '1';
                
                // Aquí puedes agregar código para recargar o actualizar la página
                // window.location.href = `index.php?controller=language&action=change&lang=${selectedValue}`;
            }, 300);
        });
    });
    
    // Efecto visual para notificaciones
    const badges = document.querySelectorAll('.badge');
    badges.forEach(badge => {
        // Ya tiene animación CSS, pero podemos agregar efectos adicionales
        badge.addEventListener('mouseover', function() {
            this.style.transform = 'scale(1.2)';
        });
        
        badge.addEventListener('mouseout', function() {
            this.style.transform = 'scale(1)';
        });
    });
    
    // Tooltips para los iconos del header
    const headerIcons = document.querySelectorAll('.header-icon');
    headerIcons.forEach(icon => {
        const title = icon.getAttribute('title');
        if (title) {
            const tooltip = document.createElement('div');
            tooltip.className = 'tooltip';
            tooltip.textContent = title;
            icon.appendChild(tooltip);
            
            // Añadimos los estilos para el tooltip
            const style = document.createElement('style');
            style.textContent = `
                .tooltip {
                    position: absolute;
                    bottom: -30px;
                    left: 50%;
                    transform: translateX(-50%) translateY(10px);
                    background-color: rgba(0,0,0,0.8);
                    color: white;
                    padding: 5px 10px;
                    border-radius: 4px;
                    font-size: 12px;
                    white-space: nowrap;
                    opacity: 0;
                    pointer-events: none;
                    transition: all 0.3s ease;
                    z-index: 1000;
                }
                
                .tooltip:before {
                    content: '';
                    position: absolute;
                    top: -5px;
                    left: 50%;
                    transform: translateX(-50%);
                    border-left: 5px solid transparent;
                    border-right: 5px solid transparent;
                    border-bottom: 5px solid rgba(0,0,0,0.8);
                }
                
                .header-icon:hover .tooltip {
                    opacity: 1;
                    transform: translateX(-50%) translateY(0);
                }
            `;
            document.head.appendChild(style);
        }
    });
    
    // Mejora para el menú de usuario en dispositivos móviles
    if (window.innerWidth <= 576) {
        const userProfile = document.querySelector('.user-profile');
        
        userProfile.addEventListener('click', function(e) {
            // Toggle de clase para mostrar/ocultar en móvil
            this.classList.toggle('active-mobile');
            
            // Añadimos estilos para el modo móvil
            if (!document.getElementById('mobile-styles')) {
                const mobileStyle = document.createElement('style');
                mobileStyle.id = 'mobile-styles';
                mobileStyle.textContent = `
                    @media (max-width: 576px) {
                        .user-profile.active-mobile .dropdown-content {
                            opacity: 1;
                            visibility: visible;
                            transform: translateY(0);
                        }
                    }
                `;
                document.head.appendChild(mobileStyle);
            }
        });
        
        // Cerrar dropdown al hacer clic fuera
        document.addEventListener('click', function(e) {
            if (!userProfile.contains(e.target)) {
                userProfile.classList.remove('active-mobile');
            }
        });
    }
    
    // Efecto de resaltado al pasar sobre el logo
    const logoContainer = document.querySelector('.logo-container');
    logoContainer.addEventListener('mouseenter', function() {
        this.style.filter = 'brightness(1.1)';
    });
    
    logoContainer.addEventListener('mouseleave', function() {
        this.style.filter = 'brightness(1)';
    });
});
