<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- 02/03/2025: Cambiado nombre de banco de "Banco Camba" a "Banco Mercantil" -->
    <title>Error - Banco Mercantil</title>
    <link rel="stylesheet" href="assets/css/styles.css">
    <link rel="manifest" href="manifest.json">
    <meta name="theme-color" content="#056f1f">
</head>
<body>
    <div class="container">
        <div class="error-container" style="text-align: center; margin-top: 100px;">
            <h1 style="color: #dc3545;">¡Oops! Ha ocurrido un error</h1>
            <p>Lo sentimos, la página que estás buscando no existe o no está disponible en este momento.</p>
            <p>Por favor, verifica la URL o regresa a la página de inicio.</p>
            <a href="index.php" class="btn btn-primary" style="margin-top: 20px;">Volver al inicio</a>
        </div>
    </div>
</body>
</html>