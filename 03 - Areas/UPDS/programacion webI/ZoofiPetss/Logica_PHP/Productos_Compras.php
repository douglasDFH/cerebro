<?php

require_once "conexion.php"; // Incluye la conexión a la base de datos

// Verificar si se ha enviado un ID de compra para mostrar los detalles
if (isset($_GET['Cod_Compras'])) {
    $Cod_Compras = $_GET['Cod_Compras']; // Obtener el ID de la compra

    // Consultar los productos asociados a esta compra en la tabla Productos_Compras
    $sql = "SELECT pc.Cod_Productos, p.Nombre_Producto, pc.Cantidad, pc.Precio_Unitario, pc.Sub_Total
            FROM Productos_Compras pc
            JOIN Productos p ON pc.Cod_Productos = p.Cod_Productos
            WHERE pc.Cod_Compras = '$Cod_Compras'";

    $result = $conex->query($sql);

    if ($result->num_rows > 0) {
        echo "<h2>Detalles de la Compra (Compra N° $Cod_Compras)</h2>";
        echo "<table border='1'>";
        echo "<tr>
                <th>Producto</th>
                <th>Cantidad</th>
                <th>Precio Unitario</th>
                <th>Sub Total</th>
              </tr>";

        // Mostrar los detalles de los productos en la compra
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
        echo "No se encontraron detalles para esta compra.";
    }
} else {
    echo "ID de compra no proporcionado.";
}

?>
