<?php
// Incluir la conexión a la base de datos
require_once "conexion.php";

// Verificar si el formulario fue enviado
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Recibir los datos del formulario
    $Descuento = $_POST['Descuento']; // Descuento aplicado en la compra
    $Estado = $_POST['Estado']; // Estado de la compra (Ej: "Pendiente", "Finalizada", etc.)
    $Fecha_Compra = $_POST['Fecha_Compra']; // Fecha de la compra
    $Fecha_Ingreso = $_POST['Fecha_Ingreso']; // Fecha de ingreso al inventario
    $Glosa = $_POST['Glosa']; // Descripción o glosa de la compra
    $Monto = $_POST['Monto']; // Monto de la compra antes de descuento
    $Monto_Total = $_POST['Monto_Total']; // Monto total después de descuento
    $Cod_Proveedores = $_POST['Cod_Proveedores']; // Código del proveedor
    $Cod_Trabajador = $_POST['Cod_Trabajador']; // Código del trabajador que realiza la compra

    // Preparar la consulta SQL para insertar los datos en la tabla Compras
    $sql = "INSERT INTO Compras 
            (Descuento, Estado, Fecha_Compra, Fecha_Ingreso, Glosa, Monto, Monto_Total, Cod_Proveedores, Cod_Trabajador) 
            VALUES 
            ('$Descuento', '$Estado', '$Fecha_Compra', '$Fecha_Ingreso', '$Glosa', '$Monto', '$Monto_Total', '$Cod_Proveedores', '$Cod_Trabajador')";

    // Ejecutar la consulta
    if ($conex->query($sql) === TRUE) {
        echo "Compra registrada con éxito.";
    } else {
        echo "Error al registrar la compra: " . $conex->error;
    }

    // Cerrar la conexión
    $conex->close();
}
?>


