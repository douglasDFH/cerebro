<!DOCTYPE html>
<html lang="<?php echo $this->session->getLanguage(); ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- 02/03/2025: Cambiado nombre de banco de "Banco Camba" a "Banco Mercantil" -->
    <title><?php echo $lang['app_name']; ?> - <?php echo isset($pageTitle) ? $pageTitle : ''; ?></title>
    <link rel="stylesheet" href="assets/css/styles.css">
    <link rel="manifest" href="manifest.json">
    <meta name="theme-color" content="#056f1f">
</head>
<body>
    <div class="header">
        <div class="logo"><?php echo $lang['app_name']; ?></div>
        <div class="user-info">
            <div class="language-selector">
                <label for="language" style="color: white; margin-right: 5px;">Idioma:</label>
                <select id="language" style="padding: 5px; margin-right: 15px; border-radius: 4px;">
                    <option value="es" <?php echo $this->session->getLanguage() == 'es' ? 'selected' : ''; ?>>Español</option>
                    <option value="en" <?php echo $this->session->getLanguage() == 'en' ? 'selected' : ''; ?>>English</option>
                </select>
            </div>
            <div class="user-name"><?php echo $lang['welcome']; ?>, <span id="username"><?php echo isset($_SESSION['nombre']) ? $_SESSION['nombre'] : 'Usuario'; ?></span></div>
            <a href="index.php?controller=usuario&action=cerrarSesion" class="btn btn-secondary logout-btn"><?php echo $lang['logout']; ?></a>
        </div>
    </div>
    
    <div class="main-container">
        <div class="sidebar">
            <ul class="sidebar-menu">
                <li class="sidebar-menu-item <?php echo $controller == 'dashboard' ? 'active' : ''; ?>">
                    <a href="index.php?controller=dashboard&action=index"><?php echo $lang['dashboard']; ?></a>
                </li>
                <li class="sidebar-menu-item <?php echo $controller == 'cliente' ? 'active' : ''; ?>">
                    <a href="index.php?controller=cliente&action=listar"><?php echo $lang['clients']; ?></a>
                </li>
                <li class="sidebar-menu-item <?php echo $controller == 'cuenta' ? 'active' : ''; ?>">
                    <a href="index.php?controller=cuenta&action=listar"><?php echo $lang['accounts']; ?></a>
                </li>
                <li class="sidebar-menu-item <?php echo $controller == 'transaccion' ? 'active' : ''; ?>">
                    <a href="index.php?controller=transaccion&action=listar"><?php echo $lang['transactions']; ?></a>
                </li>
                <li class="sidebar-menu-item <?php echo $controller == 'reporte' ? 'active' : ''; ?>">
                    <a href="index.php?controller=reporte&action=index"><?php echo $lang['reports']; ?></a>
                </li>
                <li class="sidebar-menu-item <?php echo $controller == 'oficina' ? 'active' : ''; ?>">
                    <a href="index.php?controller=oficina&action=listar"><?php echo $lang['branches']; ?></a>
                </li>
                <li class="sidebar-menu-item <?php echo $controller == 'atm' ? 'active' : ''; ?>">
                    <a href="index.php?controller=atm&action=listar"><?php echo $lang['atm']; ?></a>
                </li>
            </ul>
        </div>
        
        <div class="content">
            <?php
            // Mostrar mensaje flash si existe
            $flashMessage = $this->session->getFlashMessage();
            if ($flashMessage) {
                echo '<div class="alert alert-' . $flashMessage['type'] . '">' . $flashMessage['message'] . '</div>';
            }
            ?>
            
            <!-- Contenido de la página -->
            <?php include $contentView; ?>
        </div>
    </div>
    
    <script src="assets/js/main.js"></script>
</body>
</html>