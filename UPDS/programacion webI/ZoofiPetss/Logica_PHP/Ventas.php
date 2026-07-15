<?php

require_once "conexion.php"; // Incluye la conexión a la base de datos

// Verificar si los datos fueron enviados por el formulario
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Recibiendo los valores del formulario
    $Nro_Factura = $_POST['Nro_Factura']; // Número de factura
    $Fecha_Venta = $_POST['Fecha_Venta']; // Fecha de la venta
    $Monto = $_POST['Monto'];             // Monto de la venta
    $Descuento = $_POST['Descuento'];     // Descuento aplicado
    $Monto_Total = $_POST['Monto_Total']; // Monto total después del descuento
    $Metodo_Pago = $_POST['Metodo_Pago']; // Método de pago
    $Cod_Clientes = $_POST['Cod_Clientes']; // Código de cliente
    $Cod_Trabajador = $_POST['Cod_Trabajador']; // Código de trabajador

    // Inserción en la base de datos
    $sql = "INSERT INTO Ventas (Nro_Factura, Fecha_Venta, Monto, Descuento, Monto_Total, Metodo_Pago, Cod_Clientes, Cod_Trabajador)
            VALUES ('$Nro_Factura', '$Fecha_Venta', '$Monto', '$Descuento', '$Monto_Total', '$Metodo_Pago', '$Cod_Clientes', '$Cod_Trabajador')";

    if ($conex->query($sql) === TRUE) {
        echo "Venta registrada correctamente.";
    } else {
        echo "Error al registrar la venta: " . $conex->error;
    }

    // Redirigir al formulario de ventas o a cualquier otra página después de insertar
    header('Location: ventas_formulario.php');
    exit;
}

?>
