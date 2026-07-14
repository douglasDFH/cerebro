<?php
    // CONFIGURACIÓN DE LOS PARÁMETROS DE CONEXIÓN
    $host = "localhost"; // Dirección del servidor (localhost para servidor local)
    $usuario = "root"; // Usuario de la base de datos
    $password = ""; // Contraseña del usuario (vacío en configuraciones locales)
    $db = "zoofipetss"; // Nombre de la base de datos

    // CREACIÓN DE LA CONEXIÓN A LA BASE DE DATOS
    $conex = new mysqli($host, $usuario, $password, $db);

    // VERIFICAR SI LA CONEXIÓN FUE EXITOSA
    if ($conex == null) {
        // Mensaje de error si la conexión falla
        echo "Error de Conexión";
    } else {
        // Mensaje de éxito si la conexión es exitosa
        echo "Conexión exitosa";
    }
?>
