<?php
// Incluir archivo de conexión a la base de datos
include("bd.php");

// Obtener parámetros de filtros
$categoria = isset($_GET['categoria']) ? (int)$_GET['categoria'] : 0;
$precio_min = isset($_GET['precio_min']) ? (float)$_GET['precio_min'] : 0;
$precio_max = isset($_GET['precio_max']) ? (float)$_GET['precio_max'] : 0;
$orden = isset($_GET['orden']) ? $_GET['orden'] : 'nombre_asc';
$mostrar = isset($_GET['mostrar']) ? (int)$_GET['mostrar'] : 12;
$pagina = isset($_GET['pagina']) ? (int)$_GET['pagina'] : 1;

// Construir consulta de productos
$sql = "SELECT product_id, product_name, description, price, stock, foto, model_year, tags 
        FROM products WHERE 1=1";
$parametros = [];

// Aplicar filtros
if ($precio_min > 0) {
    $sql .= " AND price >= :precio_min";
    $parametros[':precio_min'] = $precio_min;
}

if ($precio_max > 0) {
    $sql .= " AND price <= :precio_max";
    $parametros[':precio_max'] = $precio_max;
}

// Aplicar ordenamiento
switch($orden) {
    case 'nombre_desc':
        $sql .= " ORDER BY product_name DESC";
        break;
    case 'precio_asc':
        $sql .= " ORDER BY price ASC";
        break;
    case 'precio_desc':
        $sql .= " ORDER BY price DESC";
        break;
    case 'nuevo':
        $sql .= " ORDER BY product_id DESC";
        break;
    default:
        $sql .= " ORDER BY product_name ASC";
}

// Calcular paginación
$offset = ($pagina - 1) * $mostrar;
$sql .= " LIMIT :mostrar OFFSET :offset";

// Ejecutar consulta
$sentencia = $conexion->prepare($sql);
foreach ($parametros as $param => $valor) {
    $sentencia->bindValue($param, $valor);
}
$sentencia->bindValue(':mostrar', $mostrar, PDO::PARAM_INT);
$sentencia->bindValue(':offset', $offset, PDO::PARAM_INT);
$sentencia->execute();
$productos = $sentencia->fetchAll(PDO::FETCH_ASSOC);

// Contar total de productos para paginación
$sql_count = str_replace("SELECT product_id, product_name, description, price, stock, foto, model_year, tags FROM products", "SELECT COUNT(*) FROM products", explode(" LIMIT", $sql)[0]);
$count_stmt = $conexion->prepare($sql_count);
foreach ($parametros as $param => $valor) {
    $count_stmt->bindValue($param, $valor);
}
$count_stmt->execute();
$total_productos = $count_stmt->fetchColumn();
$total_paginas = ceil($total_productos / $mostrar);

// Función para obtener la ruta correcta de la imagen
function getImagePath($foto) {
    if (empty($foto)) {
        return 'uploads/products/default-product.png';
    }
    
    if (strpos($foto, 'uploads/products/') === 0) {
        return $foto;
    }
    
    return 'uploads/products/' . $foto;
}

// Función para obtener el estado del stock
function getStockStatus($stock) {
    if ($stock <= 0) return 'sin_stock';
    if ($stock <= 5) return 'stock_bajo';
    if ($stock <= 20) return 'stock_medio';
    return 'stock_alto';
}
?>

<?php include("templates/header.php"); ?>

<!-- Script Model Viewer para soporte 3D -->
<script type="module" src="https://unpkg.com/@google/model-viewer/dist/model-viewer.min.js"></script>

<!-- 
    ESTILOS CSS ESPECÍFICOS PARA EL ECOMMERCE
    NUEVA PALETA DE COLORES: NEGRO, ROJO Y BLANCO
-->
<style>
    /* Variables CSS para el ecommerce - PALETA NEGRO, ROJO Y BLANCO */
    :root {
        --primary-color: #DC143C;      /* Rojo crimson */
        --secondary-color: #000000;     /* Negro */
        --accent-color: #FF0000;        /* Rojo puro */
        --success-color: #198754;       /* Verde para estados de éxito */
        --warning-color: #FFC107;       /* Amarillo para advertencias */
        --dark-color: #000000;          /* Negro */
        --light-color: #FFFFFF;         /* Blanco */
        --gray-light: #F8F9FA;          /* Gris muy claro */
        --gray-medium: #6C757D;         /* Gris medio */
        --red-dark: #8B0000;            /* Rojo oscuro */
        --red-light: #FF6B6B;           /* Rojo claro */
    }

    /* Estilos específicos para el ecommerce */
    .ecommerce-container {
        font-family: 'Inter', -apple-system, BlinkMacSystemFont, sans-serif;
        line-height: 1.6;
        color: var(--dark-color);
        /* Imagen de fondo */
        background-image: url('uploads/products/product_683eea6f0b183_img1.png');
        
        /* Transparencia del fondo (sin afectar al contenido) */
        background-color: rgba(14, 13, 13, 0.3); /* Fondo blanco al 30% de opacidad */
        background-blend-mode: overlay; /* Mezcla el color con la imagen */
        
        /* Ajustes de visualización */
        background-size: cover; /* Cubre todo el área */
        background-position: center; /* Centra la imagen */
        background-attachment: fixed; /* Fija la imagen al desplazarse */
        
        /* Asegurar que el contenido sea legible */
        color: #333; /* Color de texto oscuro para contraste */
        padding: 50px;
    }

    /* HERO BANNER - MODIFICADO PARA OCUPAR TODO EL ANCHO */
    .hero-banner {
        background: linear-gradient(135deg, var(--dark-color) 0%, var(--primary-color) 100%);
        color: var(--light-color);
        padding: -15px 0;
        position: relative;
        overflow: hidden;
        width: 100%;
        margin: 0;
    }

    .hero-banner::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: url('data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1000 1000"><polygon fill="%23ffffff10" points="0,1000 1000,0 1000,1000"/></svg>');
        background-size: cover;
    }

    .hero-content {
        position: relative;
        z-index: 2;
        max-width: 5000px;
        margin: 0 ;
        padding: -150px;
    }

    .hero-content .btn-light {
        background-color: var(--light-color);
        color: var(--dark-color);
        border: 2px solid var(--light-color);
        font-weight: 600;
        transition: all 0.3s ease;
    }

    .hero-content .btn-light:hover {
        background-color: transparent;
        color: var(--light-color);
        transform: translateY(-2px);
    }

    /* FEATURED PRODUCTS CAROUSEL - MODIFICADO PARA OCUPAR TODO EL ANCHO */
    .featured-carousel {
        width: 100%;
        margin: 0 auto;
        padding: 0;
    }

    .carousel-container {
        width: 100%;
        max-width: 2000px;
        margin: 0 ;
        padding: 0 30px;
    }

    .carousel-item img {
        height: 400px;
        width: 400px;
        object-fit: center;
    }

  
    /* CATEGORY CARDS */
    .category-card {
        background: var(--light-color);
        border-radius: 20px;
        padding: 2rem;
        text-align: center;
        box-shadow: 0 10px 30px rgba(0,0,0,0.1);
        transition: all 0.4s ease;
        border: 2px solid transparent;
        height: 100%;
    }

    .category-card:hover {
        transform: translateY(-10px);
        box-shadow: 0 20px 40px rgba(0,0,0,0.15);
        border-color: var(--primary-color);
    }

    .category-icon {
        font-size: 3rem;
        color: var(--primary-color);
        margin-bottom: 1rem;
        transition: all 0.3s ease;
    }

    .category-card:hover .category-icon {
        transform: scale(1.2);
    }

    /* PRODUCT CARDS */
    .product-card {
        background: var(--light-color);
        border-radius: 20px;
        overflow: hidden;
        box-shadow: 0 8px 25px rgba(0,0,0,0.1);
        transition: all 0.4s ease;
        border: 2px solid transparent;
        height: 100%;
        position: relative;
    }

    .product-card:hover {
        transform: translateY(-8px);
        box-shadow: 0 15px 35px rgba(0,0,0,0.2);
        border-color: var(--primary-color);
    }

    .product-image {
        position: relative;
        overflow: hidden;
        height: 250px;
        background: var(--gray-light);
    }

    .product-image img {
        object-position: center 20%;
        width: 100%;
        height: 100%;
        object-fit: contain;
        transition: transform 0.4s ease;
    }

    .product-card:hover .product-image img {
        transform: scale(1.1);
    }

    /* Indicador 3D en las tarjetas */
    .badge-3d {
        position: absolute;
        top: 10px;
        right: 10px;
        background: rgba(0, 0, 0, 0.8);
        color: white;
        padding: 5px 10px;
        border-radius: 20px;
        font-size: 0.75rem;
        z-index: 10;
    }

    .product-info {
        padding: 1.5rem;
    }
    .product-info .d-flex {
    align-items: stretch;
    }

    .product-title {
        font-size: 1.1rem;
        font-weight: 600;
        color: var(--dark-color);
        margin-bottom: 0.5rem;
        display: -webkit-box;
        -webkit-line-clamp: 2;
        line-clamp: 2;
        -webkit-box-orient: vertical;
        overflow: hidden;
    }

    /* ===== CÓDIGO AGREGADO: Estilos para categoría en tarjetas ===== */
    .product-category {
        font-size: 0.85rem;
        color: var(--gray-medium);
        margin-bottom: 0.5rem;
    }
    /* ===== FIN CÓDIGO AGREGADO ===== */

    .product-price {
        font-size: 1.3rem;
        font-weight: 700;
        color: var(--primary-color);
        margin-bottom: 1rem;
    }

    .btn-add-cart {
    background: var(--primary-color);
    border: none;
    border-radius: 50px;
    padding: 0.8rem 1.5rem;
    color: var(--light-color);
    font-weight: 600;
    transition: all 0.3s ease;
    }

    .btn-add-cart:hover {
    background: var(--red-dark);
    transform: translateY(-2px);
    box-shadow: 0 8px 20px rgba(220, 20, 60, 0.3);
    color: var(--light-color);
    }

    .btn-outline-primary.rounded-circle {
    border: 2px solid var(--primary-color);
    color: var(--primary-color);
    display: flex;
    align-items: center;
    justify-content: center;
    transition: all 0.3s ease;
    }

   .btn-outline-primary.rounded-circle:hover {
    background-color: var(--primary-color);
    color: var(--light-color);
    transform: scale(1.1);
   }

    /* Ajuste para que las etiquetas no se superpongan */
    .badge:nth-child(2) {
    margin-top: 40px !important;
   }

   /* Responsive para móviles */
   @media (max-width: 768px) {
    .product-info .d-flex {
        flex-direction: column;
        gap: 10px !important;
    }
    
    .btn-outline-primary.rounded-circle {
        width: 100% !important;
        height: 40px !important;
        border-radius: 50px !important;
    }
   }

    /* FILTERS */
    .filters-section {
        background: var(--light-color);
        border-radius: 15px;
        padding: 1.5rem;
        box-shadow: 0 5px 15px rgba(0,0,0,0.08);
        margin-bottom: 2rem;
        border: 1px solid var(--gray-light);
    }

    .filter-group {
        margin-bottom: 1rem;
    }

    .filter-group label {
        font-weight: 600;
        color: var(--dark-color);
        margin-bottom: 0.5rem;
    }

    .form-select, .form-control {
        border: 2px solid var(--gray-light);
        transition: all 0.3s ease;
    }

    .form-select:focus, .form-control:focus {
        border-color: var(--primary-color);
        box-shadow: 0 0 0 0.2rem rgba(220, 20, 60, 0.25);
    }

    .btn-primary {
        background-color: var(--primary-color);
        border-color: var(--primary-color);
    }

    .btn-primary:hover {
        background-color: var(--red-dark);
        border-color: var(--red-dark);
    }

    .btn-outline-secondary {
        color: var(--dark-color);
        border-color: var(--dark-color);
    }

    .btn-outline-secondary:hover {
        background-color: var(--dark-color);
        border-color: var(--dark-color);
        color: var(--light-color);
    }

    /* PAGINATION */
    .pagination {
        justify-content: center;
        margin-top: 3rem;
    }

    .page-link {
        border: none;
        color: var(--dark-color);
        padding: 0.8rem 1.2rem;
        margin: 0 0.2rem;
        border-radius: 10px;
        transition: all 0.3s ease;
        background-color: var(--light-color);
        border: 2px solid var(--gray-light);
    }

    .page-link:hover {
        background: var(--primary-color);
        color: var(--light-color);
        transform: translateY(-2px);
        border-color: var(--primary-color);
    }

    .page-item.active .page-link {
        background: var(--primary-color);
        color: var(--light-color);
        border-color: var(--primary-color);
    }

    /* RESPONSIVE */
    @media (max-width: 768px) {
        .hero-banner {
            padding: 50px 0;
        }
        
        .product-image {
            height: 200px;
        }
        
        .category-card {
            padding: 1rem;
        }

        .carousel-item img {
            height: 300px;
        }
    }

   .stock-indicator {
    position: absolute;
    top: 15px;
    left: 15px;
    background: rgba(255,255,255,0.95);
    border-radius: 50px;
    padding: 0.3rem 0.8rem;
    font-size: 0.8rem;
    font-weight: 600;
    border: 2px solid;
    z-index: 5;
   }

    .stock-low { 
        color: var(--primary-color); 
        border-color: var(--primary-color);
    }
    .stock-medium { 
        color: var(--dark-color); 
        border-color: var(--dark-color);
    }
    .stock-high { 
        color: var(--success-color); 
        border-color: var(--success-color);
    }

    /* LOADING ANIMATION */
    .loading {
        display: none;
        text-align: center;
        padding: 2rem;
    }

    .spinner-border {
        color: var(--primary-color);
    }

    /* FLOATING ACTION BUTTON */
    .fab-cart {
        position: fixed;
        bottom: 30px;
        right: 30px;
        background: var(--primary-color);
        color: var(--light-color);
        border: none;
        border-radius: 50%;
        width: 60px;
        height: 60px;
        font-size: 1.5rem;
        box-shadow: 0 8px 20px rgba(0,0,0,0.3);
        transition: all 0.3s ease;
        z-index: 1000;
    }

    .fab-cart:hover {
        background: var(--red-dark);
        transform: scale(1.1);
        box-shadow: 0 12px 25px rgba(0,0,0,0.4);
        color: var(--light-color);
    }

    .fab-cart .badge {
        background-color: var(--dark-color);
        border: 2px solid var(--light-color);
    }

    /* MODALS */
    .modal-content {
        border: none;
        border-radius: 20px;
    }

    .modal-header {
        background-color: var(--gray-light);
        border-radius: 20px 20px 0 0;
        border-bottom: 2px solid var(--primary-color);
    }

    .modal-title {
        color: var(--dark-color);
        font-weight: 700;
    }

    /* BADGES */
    .badge {
        font-weight: 600;
        padding: 0.5rem 1rem;
        border-radius: 50px;
    }

    .bg-danger {
        background-color: var(--primary-color) !important;
    }

    /* QUICK VIEW BUTTON */
    .btn-light.rounded-circle {
        background-color: var(--light-color);
        border: 2px solid var(--dark-color);
        transition: all 0.3s ease;
    }

    .btn-light.rounded-circle:hover {
        background-color: var(--dark-color);
        color: var(--light-color);
        transform: scale(1.2);
    }

    /* TOAST NOTIFICATIONS */
    .toast {
        border-radius: 10px;
    }

    .bg-success {
        background-color: var(--dark-color) !important;
    }

    /* SECTION BACKGROUNDS */
    section {
        position: relative;
    }

    .bg-light {
        background-color: var(--gray-light) !important;
    }

    /* TEXT STYLES */
    .text-muted {
        color: var(--gray-medium) !important;
    }

    .text-primary {
        color: var(--primary-color) !important;
    }

    /* HOVER EFFECTS FOR LINKS */
    a {
        color: var(--primary-color);
        text-decoration: none;
        transition: all 0.3s ease;
    }

    a:hover {
        color: var(--red-dark);
    }

    /* CART MODAL STYLES */
    .list-group-item {
        border-left: 4px solid transparent;
        transition: all 0.3s ease;
    }

    .list-group-item:hover {
        border-left-color: var(--primary-color);
        background-color: var(--gray-light);
    }

    .btn-outline-danger {
        color: var(--primary-color);
        border-color: var(--primary-color);
    }

    .btn-outline-danger:hover {
        background-color: var(--primary-color);
        border-color: var(--primary-color);
        color: var(--light-color);
    }

    /* ANIMATIONS */
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translateY(30px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }

    .fade-in-up {
        animation: fadeInUp 0.6s ease forwards;
    }

    /* CUSTOM SCROLLBAR */
    ::-webkit-scrollbar {
        width: 10px;
    }

    ::-webkit-scrollbar-track {
        background: var(--gray-light);
    }

    ::-webkit-scrollbar-thumb {
        background: var(--primary-color);
        border-radius: 5px;
    }

    ::-webkit-scrollbar-thumb:hover {
        background: var(--red-dark);
    }
    
    /* ESTILO PARA ROTACIÓN 360° 3D */
    #quickViewModal img {
        transition: transform 8s ease-in-out;
        cursor: pointer;
        transform-style: preserve-3d;
    }

    #quickViewModal .rotating {
        animation: rotate360 8s linear infinite;
    }

    @keyframes rotate360 {
        0% { transform: rotateY(0deg); }
        100% { transform: rotateY(360deg); }
    }

    /* Estilos para el visor 3D */
    model-viewer {
        --progress-bar-color: var(--primary-color);
        --progress-bar-height: 5px;
        --poster-color: var(--gray-light);
        border-radius: 8px;
        background-color: var(--gray-light);
    }

    /* Controles del visor 3D */
    .model-controls {
        margin-top: 1rem;
        display: flex;
        gap: 10px;
        justify-content: center;
    }

    .model-controls button {
        padding: 5px 15px;
        border-radius: 20px;
        border: 1px solid var(--primary-color);
        background: white;
        color: var(--primary-color);
        cursor: pointer;
        transition: all 0.3s ease;
    }

    .model-controls button:hover {
        background: var(--primary-color);
        color: white;
    }

    #btnImage1.active, #btnImage2.active {
    font-weight: bold;
    transform: scale(1.05);
    box-shadow: 0 2px 8px rgba(0,0,0,0.15);
   }

   .btn-group button.active {
    z-index: 1;
   }

    /* Animación de rotación para imágenes 2D */
     #productImageContainer img.rotating {
    animation: rotate360 8s linear infinite;
   }

   @keyframes rotate360 {
    0% { transform: rotateY(0deg); }
    100% { transform: rotateY(360deg); }
   }

   /* Transición suave para el cambio de imagen */
    #productImageContainer {
    transition: all 0.3s ease;
    min-height: 400px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
   }

    /* Estilos para el contenedor de botones */
   .btn-group {
    border-radius: 10px;
    overflow: hidden;
    box-shadow: 0 2px 10px rgba(0,0,0,0.1);
   }

    .btn-group .btn {
    border-radius: 0;
    transition: all 0.3s ease;
  }

   .btn-group .btn:first-child {
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
  }

  .btn-group .btn:last-child {
    border-top-right-radius: 10px;
    border-bottom-right-radius: 10px;
  }

  /* Efectos hover para los botones */
  .btn-group .btn:hover {
    transform: translateY(-2px);
    box-shadow: 0 4px 12px rgba(0,0,0,0.15);
  }

  /* Indicador de carga para modelos 3D */
  model-viewer::part(default-progress-bar) {
    display: block;
    height: 4px;
    background-color: var(--primary-color);
    border-radius: 2px;
 }

   /* Animación para el cambio de contenido */
  .fade-in {
    animation: fadeIn 0.3s ease-in;
  }

  @keyframes fadeIn {
    from { opacity: 0; transform: translateY(-10px); }
    to { opacity: 1; transform: translateY(0); }
  }

  /* Responsive para dispositivos móviles */
 @media (max-width: 768px) {
    .btn-group {
        width: 100%;
    }
    
    .btn-group .btn {
        font-size: 0.85rem;
        padding: 0.5rem 1rem;
    }
    
    #productImageContainer {
        min-height: 300px;
    }
    
    model-viewer {
        height: 300px !important;
    }
  }
</style>

<!-- 
    SCRIPTS ADICIONALES PARA AOS Y ANIMACIONES
    Se cargan las librerías necesarias para las animaciones del ecommerce
-->
<link href="https://cdn.jsdelivr.net/npm/aos@2.3.4/dist/aos.css" rel="stylesheet">

<!-- 
    CONTENIDO PRINCIPAL DEL ECOMMERCE
    Conserva toda la funcionalidad original pero usando el header común del sistema
-->
<div class="ecommerce-container">
    
   <!-- 3. CATEGORÍAS PRINCIPALES -->
<section class="categories-section py-4">
    <div class="container">
        <div class="row">
            <div class="col-12 text-center mb-4">
                <!-- Agregar text-white al h2 -->
                <h3 class="fw-bold text-white" data-aos="fade-up">Categorías Principales</h3>
                <!-- Cambiar text-muted por text-white en el párrafo -->
                <p class="text-white" data-aos="fade-up" data-aos-delay="100">Encuentra lo que buscas por categoría</p>
            </div>
        </div>
        <div class="row g-3">
            <!-- Las categorías se pueden agregar aquí -->
        </div>
    </div>
</section>

 <!-- BARRA DE BENEFICIOS -->
        <section class="benefits-bar py-3 bg-dark text-white">
        <div class="container">
            <div class="row text-center">
                <div class="col-md-3 col-6">
                    <i class="bi bi-truck fs-3"></i>
                    <p class="mb-0 small">Envío Gratis<br><small>En categoria Bicicletas</small></p>
                </div>
                <div class="col-md-3 col-6">
                    <i class="bi bi-shield-check fs-3"></i>
                    <p class="mb-0 small">Garantía<br><small>En todos los productos</small></p>
                </div>
                <div class="col-md-3 col-6">
                    <i class="bi bi-credit-card fs-3"></i>
                    <p class="mb-0 small">Pago Seguro<br><small>Múltiples métodos de pago</small></p>
                </div>
                <div class="col-md-3 col-6">
                    <i class="bi bi-headset fs-3"></i>
                    <p class="mb-0 small">Call center 24/7<br><small>Atención al cliente</small></p>
                </div>
            </div>
        </div>
    </section>
     
    
    <!--BANNER  CARRUSEL-->
     <section class="hero-section">
    <div class="featured-carousel mb-0">
        <div class="carousel-container">
            <div id="featuredCarousel" class="carousel slide" data-bs-ride="carousel">
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="banner/banner01.png" class="d-block w-100" 
                             alt="Banner 1">
                    </div>
                    <div class="carousel-item">
                        <img src="banner/banner02.png" class="d-block w-100" 
                             alt="Banner 2">
                    </div>
                    <div class="carousel-item">
                        <img src="banner/banner3.png" class="d-block w-100" 
                             alt="Banner 3">
                    </div>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#featuredCarousel" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon"></span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#featuredCarousel" data-bs-slide="next">
                    <span class="carousel-control-next-icon"></span>
                </button>
            </div>
        </div>
    </div>
</section>

<!-- 4. BANNER PROMOCIONAL -->
        <section class="promotional-banner py-5 bg-light">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <h3 class="fw-bold">🔥 Ofertas Especiales de Temporada</h3>
                    <p class="mb-0">Hasta 50% de descuento en productos seleccionados</p>
                </div>
                <div class="col-md-4 text-md-end mt-3 mt-md-0">
                    <a href="?categoria=0&orden=precio_asc" class="btn btn-danger btn-lg">
                        Ver Ofertas <i class="bi bi-arrow-right"></i>
                    </a>
                </div>
            </div>
        </div>
    </section>
    
    <!-- PRODUCTS SECTION -->
    <section class="py-5" id="productos">
        <div class="container">
            
               <!-- 5. SECCIÓN PRINCIPAL DE PRODUCTOS -->
    <section class="products-section py-4" id="productos">
        <div class="container">
            
            <!-- Filtros en diseño horizontal compacto -->
            <div class="filters-wrapper mb-4">
                <div class="filters-section" data-aos="fade-up">
                    <form method="GET" action="" id="filtersForm">
                        <div class="row g-2 align-items-end">
                            <div class="col-lg-2 col-md-4 col-6">
                                <select class="form-select form-select-sm" name="categoria" onchange="submitFilters()">
                                    <option value="0">Todas las categorías</option>
                                </select>
                            </div>
                            
                            <div class="col-lg-2 col-md-4 col-6">
                                <div class="input-group input-group-sm">
                                    <input type="number" class="form-control" name="precio_min" 
                                           value="<?php echo $precio_min > 0 ? $precio_min : ''; ?>" 
                                           placeholder="Min" min="0" step="0.01">
                                    <span class="input-group-text">-</span>
                                    <input type="number" class="form-control" name="precio_max" 
                                           value="<?php echo $precio_max > 0 ? $precio_max : ''; ?>" 
                                           placeholder="Max" min="0" step="0.01">
                                </div>
                            </div>
                            
                            <div class="col-lg-2 col-md-4 col-6">
                                <select class="form-select form-select-sm" name="orden" onchange="submitFilters()">
                                    <option value="nombre_asc" <?php echo $orden == 'nombre_asc' ? 'selected' : ''; ?>>Nombre A-Z</option>
                                    <option value="nombre_desc" <?php echo $orden == 'nombre_desc' ? 'selected' : ''; ?>>Nombre Z-A</option>
                                    <option value="precio_asc" <?php echo $orden == 'precio_asc' ? 'selected' : ''; ?>>Menor precio</option>
                                    <option value="precio_desc" <?php echo $orden == 'precio_desc' ? 'selected' : ''; ?>>Mayor precio</option>
                                    <option value="nuevo" <?php echo $orden == 'nuevo' ? 'selected' : ''; ?>>Más nuevos</option>
                                </select>
                            </div>
                            
                            <div class="col-lg-2 col-md-4 col-6">
                                <select class="form-select form-select-sm" name="mostrar" onchange="submitFilters()">
                                    <option value="12" <?php echo $mostrar == 12 ? 'selected' : ''; ?>>12 productos</option>
                                    <option value="24" <?php echo $mostrar == 24 ? 'selected' : ''; ?>>24 productos</option>
                                    <option value="36" <?php echo $mostrar == 36 ? 'selected' : ''; ?>>36 productos</option>
                                </select>
                            </div>
                            
                            <div class="col-lg-2 col-md-4 col-6">
                                <div class="d-flex gap-1">
                                    <button type="submit" class="btn btn-primary btn-sm flex-fill">
                                        <i class="bi bi-funnel"></i> Filtrar
                                    </button>
                                    <a href="Ecommerce.php" class="btn btn-outline-secondary btn-sm">
                                        <i class="bi bi-arrow-clockwise"></i>
                                    </a>
                                </div>
                            </div>
                            
                            <div class="col-lg-2 col-md-4 col-6">
                                <div class="d-flex align-items-center justify-content-end">
                                    <small class="text-muted">
                                        <strong><?php echo $total_productos; ?></strong> productos
                                    </small>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>

            <!-- PRODUCTS GRID -->
            <div class="row">
                <div class="col-12">
                    <div class="d-flex justify-content-between align-items-center mb-4">
                        <h3 class="fw-bold" data-aos="fade-right">
                            Todos los Productos
                        </h3>
                        <span class="text-muted" data-aos="fade-left">
                            <?php echo $total_productos; ?> productos encontrados
                        </span>
                    </div>
                    
                    <!-- Loading indicator -->
                    <div class="loading" id="loading">
                        <div class="spinner-border" role="status">
                            <span class="visually-hidden">Cargando...</span>
                        </div>
                        <p class="mt-2">Cargando productos...</p>
                    </div>
                    
                    <?php if (count($productos) > 0): ?>
                        <div class="row g-4">
                            <?php foreach ($productos as $producto): ?>
                                <div class="col-lg-3 col-md-4 col-sm-6 col-12" data-aos="fade-up">
                                    <div class="product-card">
                                        <!-- Imagen del producto -->
                                        <div class="product-image">
                                            <?php
                                            $stock_status = getStockStatus($producto['stock']);
                                            $stock_class = '';
                                            $stock_text = '';
                                            
                                            switch($stock_status) {
                                                case 'sin_stock':
                                                    $stock_class = 'stock-low';
                                                    $stock_text = 'Sin stock';
                                                    break;
                                                case 'stock_bajo':
                                                    $stock_class = 'stock-low';
                                                    $stock_text = 'Stock bajo';
                                                    break;
                                                case 'stock_medio':
                                                    $stock_class = 'stock-medium';
                                                    $stock_text = 'Disponible';
                                                    break;
                                                case 'stock_alto':
                                                    $stock_class = 'stock-high';
                                                    $stock_text = 'En stock';
                                                    break;
                                            }
                                            ?>
                                            
                                            <span class="stock-indicator <?php echo $stock_class; ?>">
                                                <?php echo $stock_text; ?>
                                            </span>
                                            
                                            <img src="<?php echo getImagePath($producto['foto']); ?>" 
                                                 alt="<?php echo htmlspecialchars($producto['product_name']); ?>"
                                                 onerror="this.src='uploads/products/default-product.png';">
                                            
                                            <!-- Botón de vista rápida -->
                                            <div class="position-absolute top-0 end-0 m-3">
                                                <button class="btn btn-light rounded-circle" 
                                                        onclick="quickView(<?php echo htmlspecialchars(json_encode($producto)); ?>)"
                                                        title="Vista rápida">
                                                    <i class="bi bi-eye"></i>
                                                </button>
                                            </div>
                                        </div>
                                        
                                        <!-- Información del producto -->
                                        <div class="product-info">
                                            <h5 class="product-title"><?php echo htmlspecialchars($producto['product_name']); ?></h5>
                                            <div class="product-price">Bs. <?php echo number_format($producto['price'], 2); ?></div>
                                            
                                            <div class="d-flex justify-content-between align-items-center gap-2">
                                                <button class="btn btn-add-cart flex-grow-1" 
                                                        onclick="addToCart(<?php echo $producto['product_id']; ?>, '<?php echo addslashes($producto['product_name']); ?>', <?php echo $producto['price']; ?>)"
                                                        <?php echo $producto['stock'] <= 0 ? 'disabled' : ''; ?>>
                                                    <i class="bi bi-cart-plus me-1"></i>
                                                    <?php echo $producto['stock'] <= 0 ? 'Sin stock' : 'Agregar'; ?>
                                                </button>
                                                
                                                <button class="btn btn-outline-primary rounded-circle" 
                                                        onclick="quickView(<?php echo htmlspecialchars(json_encode($producto)); ?>)"
                                                        title="Vista rápida"
                                                        style="width: 45px; height: 45px;">
                                                    <i class="bi bi-eye"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <?php endforeach; ?>
                        </div>
                        
                        <!-- Paginación -->
                        <?php if ($total_paginas > 1): ?>
                            <nav aria-label="Navegación de productos" class="mt-5">
                                <ul class="pagination justify-content-center">
                                    <!-- Página anterior -->
                                    <?php if ($pagina > 1): ?>
                                        <li class="page-item">
                                            <a class="page-link" href="?pagina=<?php echo $pagina-1; ?>&categoria=<?php echo $categoria; ?>&precio_min=<?php echo $precio_min; ?>&precio_max=<?php echo $precio_max; ?>&orden=<?php echo $orden; ?>&mostrar=<?php echo $mostrar; ?>">
                                                <i class="bi bi-chevron-left"></i>
                                            </a>
                                        </li>
                                    <?php endif; ?>
                                    
                                    <!-- Números de página -->
                                    <?php
                                    $inicio_paginacion = max(1, $pagina - 2);
                                    $fin_paginacion = min($total_paginas, $pagina + 2);
                                    
                                    for ($i = $inicio_paginacion; $i <= $fin_paginacion; $i++):
                                    ?>
                                        <li class="page-item <?php echo $i == $pagina ? 'active' : ''; ?>">
                                            <a class="page-link" href="?pagina=<?php echo $i; ?>&categoria=<?php echo $categoria; ?>&precio_min=<?php echo $precio_min; ?>&precio_max=<?php echo $precio_max; ?>&orden=<?php echo $orden; ?>&mostrar=<?php echo $mostrar; ?>">
                                                <?php echo $i; ?>
                                            </a>
                                        </li>
                                    <?php endfor; ?>
                                    
                                    <!-- Página siguiente -->
                                    <?php if ($pagina < $total_paginas): ?>
                                        <li class="page-item">
                                            <a class="page-link" href="?pagina=<?php echo $pagina+1; ?>&categoria=<?php echo $categoria; ?>&precio_min=<?php echo $precio_min; ?>&precio_max=<?php echo $precio_max; ?>&orden=<?php echo $orden; ?>&mostrar=<?php echo $mostrar; ?>">
                                                <i class="bi bi-chevron-right"></i>
                                            </a>
                                        </li>
                                    <?php endif; ?>
                                </ul>
                            </nav>
                        <?php endif; ?>
                        
                    <?php else: ?>
                        <div class="text-center py-5" data-aos="fade-up">
                            <i class="bi bi-search display-1 text-muted"></i>
                            <h4 class="text-muted mt-3">No se encontraron productos</h4>
                            <p class="text-muted">Intenta modificar los filtros o términos de búsqueda</p>
                            <a href="Ecommerce.php" class="btn btn-primary">Ver todos los productos</a>
                        </div>
                    <?php endif; ?>
                </div>
            </div>
        </div>
    </section>
      <!-- 6. SECCIÓN DE NEWSLETTER -->
    <section class="newsletter-section py-5 bg-dark text-white">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-md-6">
                    <h4 class="fw-bold mb-2">📧 Suscríbete a nuestro Newsletter</h4>
                    <p class="mb-0">Recibe las mejores ofertas y novedades en tu correo</p>
                </div>
                <div class="col-md-6">
                    <form class="d-flex gap-2 mt-3 mt-md-0">
                        <input type="email" class="form-control" placeholder="Tu correo electrónico" required>
                        <button type="submit" class="btn btn-light">Suscribir</button>
                    </form>
                </div>
            </div>
        </div>
    </section>
    <!-- FLOATING CART BUTTON -->
    <button class="fab-cart" onclick="toggleCart()" title="Ver carrito">
        <i class="bi bi-cart3"></i>
        <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" id="cart-count">0</span>
    </button>

    <!-- QUICK VIEW MODAL (MODIFICADO PARA 3D) -->
    <div class="modal fade" id="quickViewModal" tabindex="-1">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title">Vista Rápida</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="quickViewContent">
                    <!-- Content loaded dynamically -->
                </div>
            </div>
        </div>
    </div>

    <!-- CART MODAL -->
    <div class="modal fade" id="cartModal" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <i class="bi bi-cart3 me-2"></i>Carrito de Compras
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="cartContent">
                    <div class="text-center py-4">
                        <i class="bi bi-cart-x display-4 text-muted"></i>
                        <p class="mt-3 text-muted">Tu carrito está vacío</p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Seguir comprando</button>
                    <button type="button" class="btn btn-primary" onclick="checkout()" disabled id="checkoutBtn">
                        <i class="bi bi-credit-card me-2"></i>Proceder al pago
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- 
    SCRIPTS JAVASCRIPT DEL ECOMMERCE
    Se mantienen todas las funcionalidades originales del carrito y animaciones
-->
<script src="https://cdn.jsdelivr.net/npm/aos@2.3.4/dist/aos.js"></script>

<script>
    // Initialize AOS
    AOS.init({
        duration: 800,
        once: true,
        offset: 100
    });

    // Función para verificar si es un archivo 3D
    function is3DFile(filename) {
        if (!filename) return false;
        const extension = filename.split('.').pop().toLowerCase();
        const allowed3DFormats = ['glb', 'gltf', 'obj'];
        return allowed3DFormats.includes(extension);
    }

    // Cart functionality - Conserva toda la funcionalidad del carrito original
    let cart = JSON.parse(localStorage.getItem('bikeStoreCart')) || [];
    
    function updateCartCount() {
        const cartCount = cart.reduce((total, item) => total + item.quantity, 0);
        const cartCountElement = document.getElementById('cart-count');
        if (cartCountElement) {
            cartCountElement.textContent = cartCount;
            cartCountElement.style.display = cartCount > 0 ? 'inline' : 'none';
        }
    }

    function addToCart(productId, productName, price) {
        const existingItem = cart.find(item => item.id === productId);
        
        if (existingItem) {
            existingItem.quantity += 1;
        } else {
            cart.push({
                id: productId,
                name: productName,
                price: price,
                quantity: 1
            });
        }
        
        localStorage.setItem('bikeStoreCart', JSON.stringify(cart));
        updateCartCount();
        showToast('Producto agregado al carrito', 'success');
    }

    function removeFromCart(productId) {
        cart = cart.filter(item => item.id !== productId);
        localStorage.setItem('bikeStoreCart', JSON.stringify(cart));
        updateCartCount();
        updateCartModal();
    }

    function updateQuantity(productId, newQuantity) {
        const item = cart.find(item => item.id === productId);
        if (item) {
            if (newQuantity <= 0) {
                removeFromCart(productId);
            } else {
                item.quantity = newQuantity;
                localStorage.setItem('bikeStoreCart', JSON.stringify(cart));
                updateCartCount();
                updateCartModal();
            }
        }
    }

    function toggleCart() {
        updateCartModal();
        const cartModal = new bootstrap.Modal(document.getElementById('cartModal'));
        cartModal.show();
    }

    function updateCartModal() {
        const cartContent = document.getElementById('cartContent');
        const checkoutBtn = document.getElementById('checkoutBtn');
        
        if (cart.length === 0) {
            cartContent.innerHTML = `
                <div class="text-center py-4">
                    <i class="bi bi-cart-x display-4 text-muted"></i>
                    <p class="mt-3 text-muted">Tu carrito está vacío</p>
                </div>
            `;
            checkoutBtn.disabled = true;
        } else {
            let total = 0;
            let cartHTML = '<div class="list-group list-group-flush">';
            
            cart.forEach(item => {
                const itemTotal = item.price * item.quantity;
                total += itemTotal;
                
                cartHTML += `
                    <div class="list-group-item d-flex justify-content-between align-items-center">
                        <div>
                            <h6 class="mb-1">${item.name}</h6>
                            <small class="text-muted">Bs. ${item.price.toFixed(2)} c/u</small>
                        </div>
                        <div class="d-flex align-items-center">
                            <button class="btn btn-sm btn-outline-secondary" onclick="updateQuantity(${item.id}, ${item.quantity - 1})">
                                <i class="bi bi-dash"></i>
                            </button>
                            <span class="mx-3">${item.quantity}</span>
                            <button class="btn btn-sm btn-outline-secondary" onclick="updateQuantity(${item.id}, ${item.quantity + 1})">
                                <i class="bi bi-plus"></i>
                            </button>
                            <button class="btn btn-sm btn-outline-danger ms-3" onclick="removeFromCart(${item.id})">
                                <i class="bi bi-trash"></i>
                            </button>
                        </div>
                    </div>
                `;
            });
            
            cartHTML += '</div>';
            cartHTML += `
                <div class="mt-3 p-3 bg-light rounded">
                    <div class="d-flex justify-content-between">
                        <strong>Total: Bs. ${total.toFixed(2)}</strong>
                    </div>
                </div>
            `;
            
            cartContent.innerHTML = cartHTML;
            checkoutBtn.disabled = false;
        }
    }

    // Función quickView con soporte 3D mejorado
    function quickView(product) {
        const modal = document.getElementById('quickViewModal');
        const content = document.getElementById('quickViewContent');
        
        // Guardar el producto en una variable global para el cambio de imagen
        window.currentProduct = product;
        
        function getImagePath(foto) {
            if (!foto || foto.trim() === '') {
                return 'uploads/products/default-product.png';
            }
            
            if (foto.indexOf('uploads/products/') === 0) {
                return foto;
            }
            
            return 'uploads/products/' + foto;
        }
        
        content.innerHTML = `
            <div class="row">
                <div class="col-md-6">
                    <div id="productImageContainer">
                        <img src="${getImagePath(product.foto)}" 
                             alt="${product.product_name}" 
                             class="img-fluid rounded" 
                             style="max-height: 400px; object-fit: cover; width: 100%; cursor: pointer;"
                             onclick="toggleRotation(this)"
                             onerror="this.src='uploads/products/default-product.png';">
                    </div>
                </div>
                <div class="col-md-6">
                    <h4>${product.product_name}</h4>
                    <p class="text-muted">${product.description || 'Sin descripción disponible'}</p>
                    <h3 class="text-primary">Bs. ${parseFloat(product.price).toFixed(2)}</h3>
                    <p><strong>Stock:</strong> ${product.stock} unidades</p>
                    <p><strong>Año:</strong> ${product.model_year}</p>
                    
                    <button class="btn btn-primary btn-lg mt-3" onclick="addToCart(${product.product_id}, '${product.product_name}', ${product.price}); bootstrap.Modal.getInstance(document.getElementById('quickViewModal')).hide();">
                        <i class="bi bi-cart-plus me-2"></i>Agregar al Carrito
                    </button>
                </div>
            </div>
        `;
        
        const quickViewModal = new bootstrap.Modal(modal);
        quickViewModal.show();
    }

    // Función para alternar la rotación de imagen 2D
    function toggleRotation(imageElement) {
        if (!imageElement) {
            imageElement = document.querySelector('#productImageContainer img');
        }
        if (imageElement) {
            imageElement.classList.toggle('rotating');
        }
    }

    function checkout() {
        if (cart.length === 0) return;
        
        alert('Función de checkout en desarrollo. Los productos seleccionados se enviarán al sistema de pedidos.');
        // Aquí integrarías con el sistema de pedidos existente
    }

    function submitFilters() {
        document.getElementById('filtersForm').submit();
    }

    function showToast(message, type = 'success') {
        const toastContainer = document.querySelector('.toast-container') || createToastContainer();
        
        const toast = document.createElement('div');
        toast.className = `toast align-items-center text-white bg-${type} border-0`;
        toast.setAttribute('role', 'alert');
        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        `;
        
        toastContainer.appendChild(toast);
        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();
        
        toast.addEventListener('hidden.bs.toast', () => {
            toast.remove();
        });
    }

    function createToastContainer() {
        const container = document.createElement('div');
        container.className = 'toast-container position-fixed bottom-0 end-0 p-3';
        container.style.zIndex = '1080';
        document.body.appendChild(container);
        return container;
    }

    // Initialize cart count on page load
    document.addEventListener('DOMContentLoaded', function() {
        updateCartCount();
        
        // Smooth scrolling for anchor links
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start'
                    });
                }
            });
        });
    });
</script>

<?php include("templates/footer.php"); ?>