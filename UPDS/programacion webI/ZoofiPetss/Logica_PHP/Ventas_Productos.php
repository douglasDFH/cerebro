<?php

require_once "conexion.php"; // Incluye la conexión a la base de datos

// Verificar si se ha enviado un ID de venta para mostrar los detalles
if (isset($_GET['Cod_Ventas'])) {
    $Cod_Ventas = $_GET['Cod_Ventas']; // Obtener el ID de la venta

    // Consultar los productos asociados a esta venta en la tabla Ventas_Productos
    $sql = "SELECT vp.Cod_Productos, p.Nombre_Producto, vp.Cantidad, vp.Precio_Unitario, vp.Sub_Total
            FROM Ventas_Productos vp
            JOIN Productos p ON vp.Cod_Productos = p.Cod_Productos
            WHERE vp.Cod_Ventas = '$Cod_Ventas'";

    $result = $conex->query($sql);

    if ($result->num_rows > 0) {
        echo "<h2>Detalles de la Venta (Factura N° $Cod_Ventas)</h2>";
        echo "<table border='1'>";
        echo "<tr>
                <th>Producto</th>
                <th>Cantidad</th>
                <th>Precio Unitario</th>
                <th>Sub Total</th>
              </tr>";

        // Mostrar los detalles de los productos en la venta
        while ($row = $result->fetch_assoc()) {
            echo "<tr>
                    <td>" . $row['Nombre_Producto'] . "</td>
                    <td>" . $row['Cantidad'] . "</td>
                    <td>" . $row['Precio_Unitario'] . "</td>
                    <td>" . $row['Sub_Total'] . "</td>
                  </tr>";
        }

        echo "</table>";

    } else {
        echo "No se encontraron detalles para esta venta.";
    }
} else {
    echo "ID de venta no proporcionado.";
}

?>
