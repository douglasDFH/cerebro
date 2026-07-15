<?php
// Incluir el archivo de conexión a la base de datos
require_once "conexion.php";

// Recibir los valores del formulario
$Visualizar = isset($_POST['Visualizar']) ? 1 : 0;  // Si se marca, es 1 (TRUE), si no, 0 (FALSE)
$Modificar = isset($_POST['Modificar']) ? 1 : 0;
$Eliminar = isset($_POST['Eliminar']) ? 1 : 0;
$Agregar = isset($_POST['Agregar']) ? 1 : 0;

// Inserción de datos en la tabla Permisos
$sql = "INSERT INTO Permisos (Visualizar, Modificar, Eliminar, Agregar)
        VALUES ('$Visualizar', '$Modificar', '$Eliminar', '$Agregar')";

// Ejecutar la consulta
if ($conex->query($sql) === TRUE) {
    echo "Permiso registrado exitosamente.";
} else {
    echo "Error al registrar el permiso: " . $conex->error;
}

// Redirigir a una página de éxito o índice
header('Location: index.html');
exit();
?>
