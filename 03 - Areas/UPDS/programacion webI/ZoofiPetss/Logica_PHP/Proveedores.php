<?php

require_once "conexion.php"; // Asegúrate de tener la conexión a la base de datos.

// Recibiendo los valores de los campos del formulario
$Razon_Social = $_POST['Razon_Social']; // Razón social del proveedor
$Categoria = $_POST['Categoria'];         // Categoría del proveedor
$Telefono = $_POST['Telefono'];           // Teléfono del proveedor
$Email = $_POST['Email'];                 // Email del proveedor

// Comenzamos una transacción para asegurar que todos los datos se insertan correctamente.
$conex->beginTransaction();

try {
    // Inserción en la tabla Proveedores
    $sql_proveedor = "INSERT INTO Proveedores (Razon_Social, Categoria) VALUES ('$Razon_Social', '$Categoria')";
    $conex->query($sql_proveedor);

    // Obtenemos el ID del proveedor insertado
    $Cod_Proveedores = $conex->lastInsertId();

    // Inserción en la tabla Teléfono Proveedores
    $sql_telefono = "INSERT INTO Telefono_Proveedores (Cod_Proveedores, Telefono) VALUES ('$Cod_Proveedores', '$Telefono')";
    $conex->query($sql_telefono);

    // Inserción en la tabla Email Proveedores
    $sql_email = "INSERT INTO Email_Proveedores (Cod_Proveedores, Email) VALUES ('$Cod_Proveedores', '$Email')";
    $conex->query($sql_email);

    // Si todo va bien, confirmamos la transacción
    $conex->commit();

    // Redirigir al index o a la página deseada
    header('location: index.html');

} catch (Exception $e) {
    // En caso de error, revertimos la transacción
    $conex->rollBack();
    echo "Error: " . $e->getMessage();
}

?>
