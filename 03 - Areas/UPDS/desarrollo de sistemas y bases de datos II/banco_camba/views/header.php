<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Banco Mercantil</title>
    <!-- Estilos CSS -->
    <link rel="stylesheet" href="assets/css/styles.css">
    <!-- Puedes incluir aquí tus estilos adicionales o librerías externas -->
</head>
<body>
    <header>
        <div class="header-main">
            <div class="header-title">
                <h1>Banco Mercantil</h1>
            </div>
            <div class="header-actions">
                <!-- Selector de idioma -->
                <div class="language-selector">
                    <a href="index.php?controller=language&action=change&lang=es" class="<?php echo !isset($_SESSION['lang']) || $_SESSION['lang'] == 'es' ? 'active' : ''; ?>">ES</a> | 
                    <a href="index.php?controller=language&action=change&lang=en" class="<?php echo isset($_SESSION['lang']) && $_SESSION['lang'] == 'en' ? 'active' : ''; ?>">EN</a>
                </div>
                
                <?php if (isset($_SESSION['usuario'])): ?>
                    <div class="user-info">
                        <span>Usuario: <?php echo $_SESSION['usuario']; ?></span>
                        <a href="index.php?controller=auth&action=logout" class="btn-logout">Cerrar Sesión</a>
                    </div>
                <?php endif; ?>
            </div>
        </div>
        
        <!-- Menú de navegación -->
        <?php if (isset($_SESSION['usuario'])): ?>
            <nav class="main-menu">
                <ul>
                    <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'dashboard' ? 'active' : ''; ?>">
                        <a href="index.php?controller=dashboard&action=index">Dashboard</a>
                    </li>
                    <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'cliente' ? 'active' : ''; ?>">
                        <a href="index.php?controller=cliente&action=listar">Clientes</a>
                    </li>
                    <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'cuenta' ? 'active' : ''; ?>">
                        <a href="index.php?controller=cuenta&action=listar">Cuentas</a>
                    </li>
                    <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'transaccion' ? 'active' : ''; ?>">
                        <a href="index.php?controller=transaccion&action=listar">Transacciones</a>
                    </li>
                </ul>
            </nav>
        <?php endif; ?>
    </header>
    
    <main class="container">
        <!-- El contenido de cada página se insertará aquí -->