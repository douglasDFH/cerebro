<?php
// URL base de la aplicación - ajustar según tu configuración
$url_base = "http://localhost:/appBike_store/";
?>
<!doctype html>
<html lang="es">
<head>
    <!-- Meta tags requeridos -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    
    <!-- Título dinámico de la página -->
    <title>
        <?php 
        // Obtener el nombre del archivo actual para personalizar el título
        $current_page = basename($_SERVER['PHP_SELF'], '.php');
        switch($current_page) {
            case 'index':
                if (strpos($_SERVER['REQUEST_URI'], 'customers') !== false) {
                    echo "Clientes - ";
                } elseif (strpos($_SERVER['REQUEST_URI'], 'products') !== false) {
                    echo "Productos - ";
                } elseif (strpos($_SERVER['REQUEST_URI'], 'orders') !== false) {
                    echo "Pedidos - ";
                } elseif (strpos($_SERVER['REQUEST_URI'], 'usuarios') !== false) {
                    echo "Usuarios - ";
                } else {
                    echo "Dashboard - ";
                }
                break;
            case 'crear':
                echo "Crear Registro - ";
                break;
            case 'editar':
                echo "Editar Registro - ";
                break;
            default:
                echo "Sistema - ";
        }
        echo "Bike Store";
        ?>
    </title>
    
    <!-- Meta tags adicionales -->
    <meta name="description" content="Sistema de gestión para tienda de bicicletas - Bike Store">
    <meta name="author" content="UPDS - Universidad Privada Domingo Savio">
    
    <!-- Bootstrap CSS v5.3.2 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" 
          crossorigin="anonymous">
    
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
    
    <!-- Favicon -->
    <link rel="icon" type="image/x-icon" href="<?php echo $url_base; ?>assets/favicon.ico">
    
    <!-- Estilos personalizados -->
    <style>
        :root {
            --bs-primary: #0066cc;
            --bs-primary-dark: #004d99;
            --bs-secondary: #6c757d;
            --bs-success: #198754;
            --bs-warning: #ffc107;
            --bs-danger: #dc3545;
        }

        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .navbar-brand {
            font-weight: bold;
            font-size: 1.4rem;
        }

        .navbar {
            box-shadow: 0 2px 4px rgba(0,0,0,.1);
        }

        .nav-link {
            font-weight: 500;
            transition: all 0.3s ease;
            border-radius: 6px;
            margin: 0 2px;
        }

        .nav-link:hover {
            background-color: rgba(255,255,255,0.1);
            transform: translateY(-1px);
        }

        .nav-link.active {
            background-color: rgba(255,255,255,0.2);
            font-weight: 600;
        }

        .container-main {
            min-height: calc(100vh - 200px);
        }

        .dropdown-menu {
            border: none;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
        }

        @media (max-width: 991.98px) {
            .navbar-nav {
                padding-top: 1rem;
            }
            
            .nav-link {
                padding: 0.75rem 1rem;
                margin: 2px 0;
            }
        }
    </style>
</head>

<body>
    <header>
        <!-- Barra de navegación principal -->
        <nav class="navbar navbar-expand-lg bg-primary navbar-dark sticky-top">
            <div class="container-fluid">
                <!-- Logo/Marca de la aplicación -->
                <a class="navbar-brand d-flex align-items-center" href="<?php echo $url_base; ?>">
                    <i class="bi bi-bicycle me-2 fs-4"></i>
                    <span>Bike Store</span>
                </a>

                <!-- Botón de hamburguesa para móvil -->
                <button class="navbar-toggler" 
                        type="button" 
                        data-bs-toggle="collapse" 
                        data-bs-target="#navbarNav" 
                        aria-controls="navbarNav" 
                        aria-expanded="false" 
                        aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <!-- Enlaces de navegación -->
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <!-- Dashboard/Inicio -->
                        <li class="nav-item">
                            <a class="nav-link <?php echo (basename($_SERVER['PHP_SELF']) == 'index.php' && strpos($_SERVER['REQUEST_URI'], 'secciones') === false) ? 'active' : ''; ?>" 
                               href="<?php echo $url_base; ?>">
                                <i class="bi bi-house-door me-1"></i>
                                Dashboard
                            </a>
                        </li>

                        <!-- Clientes -->
                        <li class="nav-item">
                            <a class="nav-link <?php echo (strpos($_SERVER['REQUEST_URI'], 'customers') !== false) ? 'active' : ''; ?>" 
                               href="<?php echo $url_base; ?>secciones/customers/index.php">
                                <i class="bi bi-people me-1"></i>
                                Clientes
                            </a>
                        </li>

                        <!-- Productos -->
                        <li class="nav-item">
                            <a class="nav-link <?php echo (strpos($_SERVER['REQUEST_URI'], 'products') !== false) ? 'active' : ''; ?>" 
                               href="<?php echo $url_base; ?>secciones/products/index.php">
                                <i class="bi bi-box-seam me-1"></i>
                                Productos
                            </a>
                        </li>

                        <!-- Pedidos -->
                        <li class="nav-item">
                            <a class="nav-link <?php echo (strpos($_SERVER['REQUEST_URI'], 'orders') !== false) ? 'active' : ''; ?>" 
                               href="<?php echo $url_base; ?>secciones/orders/index.php">
                                <i class="bi bi-cart-check me-1"></i>
                                Pedidos
                            </a>
                        </li>

                        <!-- Usuarios -->
                        <li class="nav-item">
                            <a class="nav-link <?php echo (strpos($_SERVER['REQUEST_URI'], 'usuarios') !== false) ? 'active' : ''; ?>" 
                               href="<?php echo $url_base; ?>secciones/usuarios/index.php">
                                <i class="bi bi-person-gear me-1"></i>
                                Usuarios
                            </a>
                        </li>
                    </ul>

                    <!-- Menú de usuario -->
                    <ul class="navbar-nav">
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle d-flex align-items-center" 
                               href="#" 
                               role="button" 
                               data-bs-toggle="dropdown" 
                               aria-expanded="false">
                                <i class="bi bi-person-circle me-1 fs-5"></i>
                                <span class="d-none d-md-inline">Usuario</span>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-end">
                                <li>
                                    <h6 class="dropdown-header">
                                        <i class="bi bi-person me-1"></i>
                                        Mi Cuenta
                                    </h6>
                                </li>
                                <li>
                                    <a class="dropdown-item" href="<?php echo $url_base; ?>perfil.php">
                                        <i class="bi bi-person-lines-fill me-2"></i>
                                        Ver Perfil
                                    </a>
                                </li>
                                <li>
                                    <a class="dropdown-item" href="<?php echo $url_base; ?>configuracion.php">
                                        <i class="bi bi-gear me-2"></i>
                                        Configuración
                                    </a>
                                </li>
                                <li><hr class="dropdown-divider"></li>
                                <li>
                                    <a class="dropdown-item text-danger" 
                                       href="<?php echo $url_base; ?>cerrar.php"
                                       onclick="return confirm('¿Está seguro de que desea cerrar sesión?')">
                                        <i class="bi bi-box-arrow-right me-2"></i>
                                        Cerrar Sesión
                                    </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Breadcrumb opcional -->
        <?php
        $show_breadcrumb = true; // Cambiar a false si no quieres breadcrumb
        if ($show_breadcrumb && strpos($_SERVER['REQUEST_URI'], 'secciones') !== false):
        ?>
        <nav aria-label="breadcrumb" class="bg-light border-bottom">
            <div class="container-fluid">
                <ol class="breadcrumb mb-0 py-2">
                    <li class="breadcrumb-item">
                        <a href="<?php echo $url_base; ?>" class="text-decoration-none">
                            <i class="bi bi-house-door"></i> Inicio
                        </a>
                    </li>
                    <?php
                    $path_parts = explode('/', trim($_SERVER['REQUEST_URI'], '/'));
                    $current_section = '';
                    
                    if (in_array('customers', $path_parts)) {
                        $current_section = 'Clientes';
                        echo '<li class="breadcrumb-item"><i class="bi bi-people me-1"></i>' . $current_section . '</li>';
                    } elseif (in_array('products', $path_parts)) {
                        $current_section = 'Productos';
                        echo '<li class="breadcrumb-item"><i class="bi bi-box-seam me-1"></i>' . $current_section . '</li>';
                    } elseif (in_array('orders', $path_parts)) {
                        $current_section = 'Pedidos';
                        echo '<li class="breadcrumb-item"><i class="bi bi-cart-check me-1"></i>' . $current_section . '</li>';
                    } elseif (in_array('usuarios', $path_parts)) {
                        $current_section = 'Usuarios';
                        echo '<li class="breadcrumb-item"><i class="bi bi-person-gear me-1"></i>' . $current_section . '</li>';
                    }
                    
                    $current_page = basename($_SERVER['PHP_SELF'], '.php');
                    if ($current_page == 'crear') {
                        echo '<li class="breadcrumb-item active" aria-current="page"><i class="bi bi-plus-lg me-1"></i>Crear</li>';
                    } elseif ($current_page == 'editar') {
                        echo '<li class="breadcrumb-item active" aria-current="page"><i class="bi bi-pencil-square me-1"></i>Editar</li>';
                    }
                    ?>
                </ol>
            </div>
        </nav>
        <?php endif; ?>
    </header>

    <!-- Contenedor principal de la página -->
    <main class="container-main"><?php // Nota: El main se cierra en footer.php ?>