<!DOCTYPE html>
<html lang="<?php echo $this->session->getLanguage(); ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Título de la página dinámico -->
    <title><?php echo $lang['app_name']; ?> - <?php echo isset($pageTitle) ? $pageTitle : ''; ?></title>
    <!-- Enlace a la hoja de estilos principal -->
    <link rel="stylesheet" href="assets/css/Styles.css">
    <link rel="stylesheet" href="assets/css/StyleMain.css">
    <!-- Font Awesome para iconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <!-- Manifest para PWA (Progressive Web App) -->
    <link rel="manifest" href="manifest.json">
    <!-- Color del tema para navegadores móviles -->
    <meta name="theme-color" content="#056f1f">
</head>
<body>
    <!-- Encabezado de la página conservando los nombres de clases originales -->
<div class="header">
    <div class="logo-container">
        <!-- Logo del Banco Mercantil -->
        <img src="assets/img/logo.png" alt="Logo Banco Mercantil" class="logo-image">
        <div class="logo"><?php echo $lang['app_name']; ?></div>
    </div>

    <div class="user-info">
    <!-- Selector de idioma -->
    <div class="language-selector">
        <label for="language" class="user-name"><?php echo isset($lang['language']) ? $lang['language'] : 'Idioma'; ?> 🌐:</label>
        <div class="language-dropdown">
            <div class="language-selected">
                <i class="fas fa-globe"></i>
                <span id="current-language" class="user-name"><?php echo isset($lang['language_' . $this->session->getLanguage()]) ? $lang['language_' . $this->session->getLanguage()] : ($this->session->getLanguage() == 'es' ? 'Español' : 'English'); ?></span>
                <i class="fas fa-chevron-down"></i>
            </div>
            <div class="language-options">
                <div class="language-option" data-value="es">
                    <img src="assets/img/spanish.png" alt="Español" class="flag-icon">
                    <span><?php echo isset($lang['language_es']) ? $lang['language_es'] : 'Español'; ?></span>
                </div>
                <div class="language-option" data-value="en">
                    <img src="assets/img/ingles.png" alt="English" class="flag-icon">
                    <span><?php echo isset($lang['language_en']) ? $lang['language_en'] : 'English'; ?></span>
                </div>
            </div>
        </div>
        <select id="language" class="language-select">
            <option value="es" <?php echo $this->session->getLanguage() == 'es' ? 'selected' : ''; ?>><?php echo isset($lang['language_es']) ? $lang['language_es'] : 'Español'; ?></option>
            <option value="en" <?php echo $this->session->getLanguage() == 'en' ? 'selected' : ''; ?>><?php echo isset($lang['language_en']) ? $lang['language_en'] : 'English'; ?></option>
        </select>
    </div>
    <!-- Usuario y botón de cerrar sesión -->
    <div class="user-profile">
        <img src="assets/img/welcome.png" alt="Profile" class="user-avatar">
        <div class="user-details">
            <div class="user-name"><?php echo $lang['welcome']; ?>, <span id="username"><?php echo isset($_SESSION['nombre']) ? $_SESSION['nombre'] : 'Usuario'; ?></span></div>
            <div class="user-role"><?php echo isset($_SESSION['rol']) ? $_SESSION['rol'] : 'Administrador'; ?></div>
        </div>
        <div class="user-dropdown">
            <i class="fas fa-chevron-down"></i>
            <div class="dropdown-content">
                <a href="#" class="dropdown-item">
                    <img src="assets/img/perfil.png" alt="Perfil" class="menu-icon"> <?php echo isset($lang['profile']) ? $lang['profile'] : 'Mi Perfil'; ?>
                </a>
                <a href="#" class="dropdown-item">
                    <img src="assets/img/config.png" alt="Configuración" class="menu-icon"> <?php echo isset($lang['settings']) ? $lang['settings'] : 'Configuración'; ?>
                </a>
                <div class="dropdown-divider"></div>
                <a href="index.php?controller=usuario&action=cerrarSesion" class="logout-btn">
                    <img src="assets/img/cerrar.png" alt="Cerrar Sesión" class="menu-icon"> <?php echo $lang['logout']; ?>
                </a>
            </div>
        </div>
    </div>
</div>
</div>

<!-- Contenedor principal -->
<div class="main-container">
    <!-- Barra lateral (sidebar) mejorada -->
    <div class="sidebar">
        <div class="sidebar-header">
            <h3><?php echo isset($lang['financial_panel']) ? $lang['financial_panel'] : 'Panel Financiero'; ?></h3>
        </div>
        <ul class="sidebar-menu">
            <!-- Enlace a la página de bienvenida -->
            <li class="sidebar-menu-item <?php echo $controller == 'bienvenida' ? 'active' : ''; ?>">
                <a href="index.php?controller=bienvenida&action=index">
                    <i class="fas fa-home"></i>
                    <span><?php echo $lang['home']; ?></span>
                </a>
            </li>
            <!-- Enlace a tarjetas -->
            <li class="sidebar-menu-item <?php echo (isset($controller) && $controller == 'tarjeta') ? 'active' : ''; ?>">
                <a href="index.php?controller=tarjeta&action=listar">
                    <i class="fas fa-credit-card"></i>
                    <span><?php echo isset($lang['cards']) ? $lang['cards'] : 'Tarjetas'; ?></span>
                </a>
            </li>
            <!-- Enlace a la gestión de clientes -->
            <li class="sidebar-menu-item <?php echo $controller == 'cliente' ? 'active' : ''; ?>">
                <a href="index.php?controller=cliente&action=listar">
                    <i class="fas fa-user-friends"></i>
                    <span><?php echo $lang['clients']; ?></span>
                    <?php if(isset($clientesNuevos) && $clientesNuevos > 0): ?>
                        <div class="badge"><?php echo $clientesNuevos; ?></div>
                    <?php endif; ?>
                </a>
            </li>
            <!-- Enlace a la gestión de cuentas -->
            <li class="sidebar-menu-item <?php echo $controller == 'cuenta' ? 'active' : ''; ?>">
                <a href="index.php?controller=cuenta&action=listar">
                    <i class="fas fa-piggy-bank"></i>
                    <span><?php echo $lang['accounts']; ?></span>
                </a>
            </li>
            <!-- Enlace a la gestión de transacciones -->
            <li class="sidebar-menu-item <?php echo $controller == 'transaccion' ? 'active' : ''; ?>">
                <a href="index.php?controller=transaccion&action=listar">
                    <i class="fas fa-exchange-alt"></i>
                    <span><?php echo $lang['transactions']; ?></span>
                </a>
            </li>
            <!-- Enlace a la gestión de reportes -->
            <li class="sidebar-menu-item <?php echo $controller == 'reporte' ? 'active' : ''; ?>">
                <a href="index.php?controller=reporte&action=index">
                    <i class="fas fa-chart-bar"></i>
                    <span><?php echo $lang['reports']; ?></span>
                </a>
            </li>
            <!-- Enlace a la gestión de oficinas -->
            <li class="sidebar-menu-item <?php echo $controller == 'oficina' ? 'active' : ''; ?>">
                <a href="index.php?controller=oficina&action=listar">
                    <i class="fas fa-building"></i>
                    <span><?php echo $lang['branches']; ?></span>
                </a>
            </li>
            <!-- Enlace a las transacciones de cajeros automáticos (ATM) -->
            <li class="sidebar-menu-item <?php echo $controller == 'transaccionatm' ? 'active' : ''; ?>">
                <a href="index.php?controller=transaccionatm&action=listar">
                    <i class="fas fa-money-bill-wave"></i>
                    <span><?php echo $lang['atm']; ?></span>
                </a>
            </li>
        </ul>
        <div class="sidebar-footer">
            <div class="version"><?php echo isset($lang['version']) ? $lang['version'] : 'v2.5.3'; ?></div>
        </div>
    </div>
    
    <!-- Contenido principal de la página -->
    <div class="content">
        <?php
        // Mostrar mensaje flash si existe
        $flashMessage = $this->session->getFlashMessage();
        if ($flashMessage) {
            echo '<div class="alert alert-' . $flashMessage['type'] . '">' . $flashMessage['message'] . '</div>';
        }
        ?>
            
        <!-- Incluir la vista dinámica del contenido -->
        <?php include $contentView; ?>
    </div>
</div>

<style>
    /* ---------- ESTILOS GENERALES DEL NAVBAR ---------- */

/* Contenedor principal del Navbar */
.navbar {
    background-color: var(--primary-color); /* Color de fondo usando variable */
    color: white; /* Color del texto */
    padding: 10px 20px; /* Espaciado interno */
    display: flex; /* Flexbox para alinear elementos */
    justify-content: space-between; /* Espacio entre elementos */
    align-items: center; /* Centrado vertical */
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); /* Sombra suave */
}

/* Estilos para la marca del Navbar (logo y nombre) */
.navbar-brand {
    display: flex; /* Flexbox para alinear elementos */
    align-items: center; /* Centrado vertical */
    font-size: 24px; /* Tamaño de fuente */
    font-weight: bold; /* Texto en negrita */
}

/* Estilos para la imagen del logo en el Navbar */
.navbar-brand img {
    height: 40px; /* Altura de la imagen */
    margin-right: 10px; /* Margen derecho */
}

/* Contenedor para elementos del lado derecho del Navbar */
.navbar-right {
    display: flex; /* Flexbox para alinear elementos */
    align-items: center; /* Centrado vertical */
}

/* Estilos para el selector de idioma */
.language-selector {
    margin-right: 20px; /* Margen derecho */
    display: flex; /* Flexbox para alinear elementos */
    align-items: center; /* Centrado vertical */
}

/* Estilos para el texto del selector de idioma */
.language-selector span {
    margin-right: 5px; /* Margen derecho */
}

/* Estilos para el select (desplegable) del selector de idioma */
.language-selector select {
    background-color: rgba(255, 255, 255, 0.2); /* Fondo semitransparente */
    border: none; /* Sin borde */
    color: white; /* Color del texto */
    padding: 5px 10px; /* Espaciado interno */
    border-radius: var(--border-radius); /* Bordes redondeados usando variable */
}

/* Estilos para la información del usuario */
.user-info {
    display: flex; /* Flexbox para alinear elementos */
    align-items: center; /* Centrado vertical */
    background-color: rgba(255, 255, 255, 0.1); /* Fondo semitransparente */
    padding: 5px 15px; /* Espaciado interno */
    border-radius: 20px; /* Bordes redondeados */
}

/* Estilos para la imagen del usuario */
.user-info img {
    height: 30px; /* Altura de la imagen */
    width: 30px; /* Ancho de la imagen */
    border-radius: 50%; /* Forma circular */
    margin-right: 10px; /* Margen derecho */
}
</style>

<!-- Enlace al archivo JavaScript principal -->
<script src="assets/js/main.js"></script>
</body>
</html>