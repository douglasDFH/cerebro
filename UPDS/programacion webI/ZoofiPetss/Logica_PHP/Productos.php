<?php

require_once "conexion.php";

// Verificar si se ha enviado el formulario
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    
    // Recibiendo los valores de los campos del formulario
    $Nombre = $_POST['Nombre'];            // Nombre del producto
    $Stock = $_POST['Stock'];              // Cantidad disponible del producto
    $Proveedor = $_POST['Proveedor'];      // Nombre del proveedor del producto
    $Categoria = $_POST['Categoria'];      // Categoría del producto
    $Tipo = $_POST['Tipo'];                // Tipo de producto

    // Validación de datos (en caso de que los campos estén vacíos)
    if (empty($Nombre) || empty($Stock) || empty($Proveedor) || empty($Categoria) || empty($Tipo)) {
        echo "Todos los campos son obligatorios.";
        exit;
    }

    // Escapar los valores para evitar inyección SQL
    $Nombre = $conex->real_escape_string($Nombre);
    $Stock = $conex->real_escape_string($Stock);
    $Proveedor = $conex->real_escape_string($Proveedor);
    $Categoria = $conex->real_escape_string($Categoria);
    $Tipo = $conex->real_escape_string($Tipo);

    // Inserción en la base de datos
    $sql = "INSERT INTO Productos 
            (Nombre, Stock, Proveedor, Categoria, Tipo) 
            VALUES 
            ('$Nombre', '$Stock', '$Proveedor', '$Categoria', '$Tipo')";

    if ($conex->query($sql) === TRUE) {
        // Redirigir al index después de realizar la operación
        header('location: index.html');
        exit;
    } else {
        echo "Error al insertar el producto: " . $conex->error;
    }
}
?>
