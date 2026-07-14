<!DOCTYPE html>
<html lang="<?php echo $lang_code ?? 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['app_name'] ?? 'Banco Mercantil'; ?></title>
    <!-- Enlace a la hoja de estilos principal -->
    <link rel="stylesheet" href="assets/css/StyleBienvenida.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
</head>
<body>
    <!-- Título con fondo verde y elementos decorativos - ALTURA REDUCIDA -->
    <div class="titulo-valores-container">
        <div class="decoracion-izq"></div>
        <div class="decoracion-der"></div>
        
        <div class="titulo-content">
            <div class="titulo-icon"><i class="fas fa-university"></i></div>
            <h2 class="valores-heading"><?php echo $lang['our_values']; ?></h2>
            <div class="titulo-decoration">
                <span class="titulo-line"></span>
                <i class="fas fa-star"></i>
                <span class="titulo-line"></span>
            </div>
            <p class="valores-descripcion"><?php echo $lang['values_description']; ?></p>
        </div>
    </div>

    <!-- Sección de iconos/tarjetas - ALTURA REDUCIDA -->
    <div class="icons-section">
        <div class="movie-grid">
            <!-- Tarjeta 1: Liderazgo -->
            <div class="movie-card">
                <img src="assets/img/Liderazgo.jpg" alt="<?php echo $lang['leadership']; ?>">
                <div class="card-overlay">
                    <h3 class="card-title"><?php echo $lang['leadership']; ?></h3>
                    <p class="card-description"><?php echo $lang['leadership_desc']; ?></p>
                </div>
            </div>

            <!-- Tarjeta 2: Servicio al cliente -->
            <div class="movie-card">
                <img src="assets/img/servicio.jpg" alt="<?php echo $lang['customer_service']; ?>">
                <div class="card-overlay">
                    <h3 class="card-title"><?php echo $lang['customer_service']; ?></h3>
                    <p class="card-description"><?php echo $lang['customer_service_desc']; ?></p>
                </div>
            </div>

            <!-- Tarjeta 3: Calidad y confiabilidad -->
            <div class="movie-card">
                <img src="assets/img/calidad_confiabilidad.jpeg" alt="<?php echo $lang['reliability']; ?>">
                <div class="card-overlay">
                    <h3 class="card-title"><?php echo $lang['reliability']; ?></h3>
                    <p class="card-description"><?php echo $lang['reliability_desc']; ?></p>
                </div>
            </div>

            <!-- Tarjeta 4: Integridad -->
            <div class="movie-card">
                <img src="assets/img/integridad.jpeg" alt="<?php echo $lang['integrity']; ?>">
                <div class="card-overlay">
                    <h3 class="card-title"><?php echo $lang['integrity']; ?></h3>
                    <p class="card-description"><?php echo $lang['integrity_desc']; ?></p>
                </div>
            </div>

            <!-- Tarjeta 5: Excelencia y profesionalismo -->
            <div class="movie-card">
                <img src="assets/img/excelencia_profesionalismo.jpeg" alt="<?php echo $lang['professionalism']; ?>">
                <div class="card-overlay">
                    <h3 class="card-title"><?php echo $lang['professionalism']; ?></h3>
                    <p class="card-description"><?php echo $lang['professionalism_desc']; ?></p>
                </div>
            </div>
        </div>
    </div>

    <!-- Sección de cierre mejorada - ALTURA REDUCIDA -->
    <div class="cierre-bienvenida-mejorado">
        <div class="separador-decorativo-animado">
            <span class="linea-animada"></span>
            <span class="emoji-animado"><i class="fas fa-handshake"></i></span>
            <span class="linea-animada"></span>
        </div>
        
        <div class="mensaje-compromiso-mejorado">
            <h3 class="titulo-compromiso"><?php echo $lang['our_commitment']; ?></h3>
            <p class="subtitulo-compromiso"><?php echo $lang['commitment_subtitle']; ?></p>
            
            <!-- Insertar los iconos de compromiso aquí -->
            <div class="compromiso-iconos-fila">
                <div class="compromiso-icono-item">
                    <i class="fas fa-headset"></i>
                    <span><?php echo $lang['support_24_7']; ?></span>
                </div>
                <div class="compromiso-icono-item">
                    <i class="fas fa-shield-alt"></i>
                    <span><?php echo $lang['maximum_security']; ?></span>
                </div>
                <div class="compromiso-icono-item">
                    <i class="fas fa-handshake"></i>
                    <span><?php echo $lang['expert_advice']; ?></span>
                </div>
                <div class="compromiso-icono-item">
                    <i class="fas fa-mobile-alt"></i>
                    <span><?php echo $lang['mobile_banking']; ?></span>
                </div>
            </div>

            <div class="compromiso-detalles">
                <div class="compromiso-item">
                    <i class="fa fa-check-circle"></i>
                    <span><?php echo $lang['personalized_attention']; ?></span>
                </div>
                <div class="compromiso-item">
                    <i class="fa fa-lock"></i>
                    <span><?php echo $lang['guaranteed_security']; ?></span>
                </div>
                <div class="compromiso-item">
                    <i class="fa fa-chart-line"></i>
                    <span><?php echo $lang['sustainable_growth']; ?></span>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Footer con fondo verde - ALTURA REDUCIDA -->
    <div class="footer-bienvenida-mejorado">
        <p>
            © 2025 <?php echo $lang['app_name'] ?? 'Banco Mercantil Santa Cruz S.A.'; ?> | 
            <a href="#" class="footer-link"><?php echo $lang['terms_conditions']; ?></a> | 
            <a href="#" class="footer-link"><?php echo $lang['privacy_policy']; ?></a> |
            <span class="version-info"><?php echo $lang['version_info'] ?? 'Versión: 1.0.0'; ?></span>
        </p>
    </div>

    <script>
    document.addEventListener('DOMContentLoaded', function() {
        // Función para animar elementos al hacer scroll
        function animateOnScroll() {
            const cards = document.querySelectorAll('.movie-card');
            cards.forEach((card, index) => {
                const rect = card.getBoundingClientRect();
                const isVisible = rect.top < window.innerHeight && rect.bottom >= 0;
                
                if (isVisible) {
                    setTimeout(() => {
                        card.style.opacity = '1';
                        card.style.transform = 'translateY(0)';
                    }, index * 100);
                }
            });
        }
        
        // Iniciar animaciones al cargar
        animateOnScroll();
        window.addEventListener('scroll', animateOnScroll);
    });
    </script>
</body>
</html>