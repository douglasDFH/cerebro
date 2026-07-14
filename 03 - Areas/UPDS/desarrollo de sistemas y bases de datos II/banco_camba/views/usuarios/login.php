<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Banco Mercantil - <?php echo $lang['login']; ?></title>
    <link rel="stylesheet" href="assets/css/styles.css">
    <link rel="manifest" href="manifest.json">
    <meta name="theme-color" content="#056f1f">
</head>
<body>
    <!-- Imagen de fondo -->
    <img src="../../BANCO_CAMBA/assets/img/logo.jpg" alt="Fondo" class="background-image">
    
    <div class="language-selector">
        <select id="language">
            <option value="es" <?php echo (isset($_SESSION['lang']) && $_SESSION['lang'] == 'es') ? 'selected' : ''; ?>>Español</option>
            <option value="en" <?php echo (isset($_SESSION['lang']) && $_SESSION['lang'] == 'en') ? 'selected' : ''; ?>>English</option>
        </select>
    </div>
    
    <div class="login-container">
        <div class="login-logo">Banco Mercantil</div>
        <h2><?php echo $lang['login']; ?></h2>
        
        <?php
        // Mostrar mensaje flash si existe
        $flashMessage = $this->session->getFlashMessage();
        if ($flashMessage) {
            echo '<div class="alert alert-' . $flashMessage['type'] . '">' . $flashMessage['message'] . '</div>';
        }
        ?>
        
        <form id="login-form" method="POST" action="index.php?controller=usuario&action=autenticar">
            <div class="form-group">
                <label class="form-label"><?php echo $lang['username']; ?></label>
                <input type="text" class="form-control" id="username" name="username" required>
            </div>
            
            <div class="form-group">
                <label class="form-label"><?php echo $lang['password']; ?></label>
                <input type="password" class="form-control" id="password" name="password" required>
            </div>
            
            <button type="submit" class="btn btn-primary btn-block"><?php echo $lang['login']; ?></button>
            
            <a href="#" class="forgot-password"><?php echo $lang['forgot_password']; ?></a>
        </form>
    </div>
    
    <script>
        document.addEventListener('DOMContentLoaded', function() {
            // Selector de idioma
            const languageSelector = document.getElementById('language');
            if (languageSelector) {
                languageSelector.addEventListener('change', function() {
                    window.location.href = `index.php?controller=usuario&action=cambiarIdioma&lang=${this.value}`;
                });
            }
        });
    </script>
</body>
</html>