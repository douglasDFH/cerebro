<?php
$url_base="http://localhost:8080/appbike_store/";
?>
<!doctype html>
<html lang="es">
    <head>
        <title>App Bike Store</title>
        <!-- Required meta tags -->
        <meta charset="utf-8" />
        <meta
            name="viewport"
            content="width=device-width, initial-scale=1, shrink-to-fit=no"
        />

        <!-- Bootstrap CSS v5.2.1 -->
        <link
            href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css"
            rel="stylesheet"
            integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN"
            crossorigin="anonymous"
        />
    </head>

    <body>
        <header>
            <!-- place navbar here -->
             <nav class="navbar navbar-expand navbar-light bg-light">
                <ul class="nav navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link active" href="<?php echo $url_base;?>" aria-current="page"
                            >Sistema Web <span class="visually-hidden">(current)</span></a
                        >
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="<?php echo $url_base;?>secciones/customers/index.php">Clientes</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="<?php echo $url_base;?>secciones/products/index.php">Productos</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="<?php echo $url_base;?>secciones/orders/index.php">Pedidos</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="<?php echo $url_base;?>secciones/usuarios/index.php">Usuarios</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="cerrar.php">Cerrar Sesi&oacute;n</a>
                    </li>
                </ul>
             </nav>
        </header>
        <main class="container">